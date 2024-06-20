﻿using Microsoft.Build.Locator;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Emit;
using Microsoft.CodeAnalysis.MSBuild;
using ReCodeIt.Models;
using ReCodeIt.ReMapper;
using ReCodeIt.Utils;
using System.Diagnostics;

namespace ReCodeIt.CrossCompiler;

public class ReCodeItCrossCompiler
{
    public ReCodeItCrossCompiler()
    {
        Remapper = new(this);
    }

    public CrossCompilerProjectModel ActiveProject => ProjectManager.ActiveProject;

    private CrossCompilerSettings Settings => DataProvider.Settings.CrossCompiler;
    private ReCodeItRemapper Remapper { get; }
    private Stopwatch SW { get; } = new();

    public void StartRemap()
    {
        ActiveProject.ChangedTypes.Clear();

        if (ActiveProject == null)
        {
            Logger.Log("ERROR: No Cross Compiler Project is loaded, create or load one first.", ConsoleColor.Red);
            return;
        }

        if (ActiveProject.VisualStudioClonedSolutionDirectory == string.Empty)
        {
            Logger.Log("ERROR: No ReCodeIt Project directory is set. (Project Creation Failed)", ConsoleColor.Red);
            return;
        }

        Remapper.InitializeRemap(
            ActiveProject.RemapModels,
            ActiveProject.OriginalAssemblyPath,
            ActiveProject.VisualStudioDependencyPath,
            true);

        Logger.Log("-----------------------------------------------", ConsoleColor.Green);
        Logger.Log($"Generated Cross Mapped DLL for project {ActiveProject.SolutionName}", ConsoleColor.Green);
        Logger.Log($"Built to: {Path.Combine(ActiveProject.VisualStudioDependencyPath, ActiveProject.OriginalAssemblyDllName)}", ConsoleColor.Green);
        Logger.Log($"Changed {ActiveProject.ChangedTypes.Count} types", ConsoleColor.Green);
        Logger.Log($"Original assembly path: {ActiveProject.OriginalAssemblyPath}", ConsoleColor.Green);
        Logger.Log($"Original assembly hash: {ActiveProject.OriginalAssemblyHash}", ConsoleColor.Green);
        Logger.Log($"Original patched assembly path: {ActiveProject.VisualStudioDependencyPath}", ConsoleColor.Green);
        //Logger.Log($"Original patched assembly hash: {ActiveProject.RemappedAssemblyHash}", ConsoleColor.Green);
        Logger.Log("-----------------------------------------------", ConsoleColor.Green);
    }

    public async Task StartCrossCompile()
    {
        ProjectManager.LoadProjectCC(ActiveProject);

        SW.Reset();
        SW.Start();

        var workspace = MSBuildWorkspace.Create();

        Logger.Log("Loading Solution...", ConsoleColor.Yellow);

        var solution = await Task.Run(() => LoadSolutionAsync(workspace, ActiveProject.VisualStudioClonedSolutionPath));

        Project newProject;

        // Make sure we loop over the Id's instead of projects, because they are immutable
        foreach (var projId in solution.ProjectIds)
        {
            newProject = solution.GetProject(projId);

            // Skip the ReCodeIt project if it exists
            if (newProject!.Name == "ReCodeIt")
            {
                continue;
            }

            Logger.Log("Reversing Identifier Changes...", ConsoleColor.Yellow);

            foreach (var docId in newProject.DocumentIds)
            {
                var doc = newProject.GetDocument(docId);

                // Remove the document from the project
                newProject = newProject.RemoveDocument(docId);

                // We only want C# source code
                if (doc.SourceCodeKind != SourceCodeKind.Regular) { continue; }

                var syntaxTree = await doc.GetSyntaxTreeAsync();
                var syntaxRoot = syntaxTree!.GetCompilationUnitRoot();
                syntaxRoot = FindAndChangeIdentifiers(syntaxRoot);

                var newDoc = newProject.AddDocument(doc.Name, syntaxRoot.GetText());

                newProject = newDoc.Project;
            }

            Logger.Log("Compiling Project...", ConsoleColor.Yellow);

            var comp = await newProject.GetCompilationAsync();

            foreach (var diag in comp.GetDiagnostics())
            {
                Logger.Log(diag.ToString());
            }

            using (var ms = new MemoryStream())
            {
                EmitResult emitResult = comp.Emit(ms);

                // Check if the compilation was successful
                if (emitResult.Success)
                {
                    var assemblyPath = $"{ActiveProject.BuildDirectory}\\{ActiveProject.ProjectDllName}";
                    using (var fs = new FileStream(assemblyPath, FileMode.Create, FileAccess.Write))
                    {
                        ms.Seek(0, SeekOrigin.Begin);
                        ms.CopyTo(fs);
                    }

                    Logger.Log($"Compilation succeeded. Time ({SW.Elapsed.TotalSeconds:F1}) seconds, Assembly written to: {assemblyPath}", ConsoleColor.Green);
                    SW.Stop();
                }
                else
                {
                    Logger.Log("Compilation failed.");
                    foreach (var diagnostic in emitResult.Diagnostics)
                    {
                        Logger.Log(diagnostic.ToString());
                    }

                    SW.Stop();
                }
            }
        }
    }

    private async Task<Solution> LoadSolutionAsync(MSBuildWorkspace workspace, string solutionPath)
    {
        if (!MSBuildLocator.IsRegistered) MSBuildLocator.RegisterDefaults();

        using (var w = MSBuildWorkspace.Create())
        {
            return await w.OpenSolutionAsync(solutionPath);
        }
    }

    private CompilationUnitSyntax FindAndChangeIdentifiers(CompilationUnitSyntax syntax)
    {
        // Get the things we want to change
        var identifiers = syntax.DescendantNodes()
                .OfType<IdentifierNameSyntax>()
                .Where(id => ActiveProject.ChangedTypes.ContainsKey(id.Identifier.Text));

        // Do Black Voodoo Magic
        var newSyntax = syntax.ReplaceNodes(identifiers, (oldNode, newNode) =>
                SyntaxFactory.IdentifierName(ActiveProject.ChangedTypes[oldNode.Identifier.Text])
                    .WithLeadingTrivia(oldNode.GetLeadingTrivia())
                    .WithTrailingTrivia(oldNode.GetTrailingTrivia()));

        return newSyntax;
    }

    /// <summary>
    /// Starts the build process for the active project.
    /// </summary>
    private void StartBuild()
    {
        var arguements = $"build {ActiveProject.VisualStudioClonedSolutionPath} " +
            $"/p:Configuration=Debug " +
            $"/p:Platform=\"Any CPU\"";

        var path = ActiveProject.VisualStudioClonedSolutionDirectory;

        // clean the project first
        ExecuteDotnetCommand("clean", path);

        // Restore packages
        ExecuteDotnetCommand("restore", path);

        // Then build the project
        ExecuteDotnetCommand(arguements, path);
    }

    private static void ExecuteDotnetCommand(string arguments, string workingDirectory)
    {
        ProcessStartInfo startInfo = new ProcessStartInfo
        {
            FileName = "dotnet",
            Arguments = arguments,
            WorkingDirectory = workingDirectory,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
            CreateNoWindow = true
        };

        using (Process process = new Process())
        {
            process.StartInfo = startInfo;

            process.OutputDataReceived += (sender, e) => Logger.Log(e.Data);

            process.Start();
            process.BeginOutputReadLine();
            process.BeginErrorReadLine();
            process.WaitForExit();

            int exitCode = process.ExitCode;
            Logger.Log($"dotnet {arguments} exited with code {exitCode}");
        }
    }

    private void MoveResult()
    {
        var builtDll = Directory
            .GetFiles(ActiveProject.VisualStudioClonedSolutionDirectory, "*.dll", SearchOption.AllDirectories)
            .FirstOrDefault(file => file.Contains(ActiveProject.ProjectDllName));

        if (builtDll == null)
        {
            Logger.Log($"ERROR: No {ActiveProject.ProjectDllName} found at path {ActiveProject.VisualStudioClonedSolutionDirectory}, build failed.", ConsoleColor.Red);
            //CleanUp();
            return;
        }

        var dest = Path.Combine(ActiveProject.BuildDirectory, ActiveProject.ProjectDllName);

        // Create it if it doesnt exist
        if (!Directory.Exists(ActiveProject.BuildDirectory))
        {
            Directory.CreateDirectory(ActiveProject.BuildDirectory);
        }

        File.Copy(builtDll, dest, true);

        Logger.Log($"Copying {ActiveProject.ProjectDllName} to {dest}", ConsoleColor.Yellow);
        Logger.Log($"Successfully Cross Compiled Project {ActiveProject.SolutionName}", ConsoleColor.Green);
        //CleanUp();
    }

    private void CleanUp()
    {
        if (Path.Exists(ActiveProject.VisualStudioClonedSolutionDirectory))
        {
            Logger.Log("Cleaning up cloned project files", ConsoleColor.Yellow);
            Directory.Delete(ActiveProject.VisualStudioClonedSolutionDirectory, true);
        }
    }
}