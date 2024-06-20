using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
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

    private ReCodeItRemapper Remapper { get; }
    public CrossCompilerSettings Settings => DataProvider.Settings.CrossCompiler;

    public CrossCompilerProjectModel ActiveProject => ProjectManager.ActiveProject;

    private int _identifiersChanged = 0;

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

    public void StartCrossCompile()
    {
        ProjectManager.CopyVisualStudioProject(ActiveProject);
        ProjectManager.MoveOriginalReference();

        AnalyzeSourceFiles();

        StartBuild();
        MoveResult();
    }

    private void AnalyzeSourceFiles()
    {
        foreach (var file in ProjectManager.AllProjectSourceFiles)
        {
            AnalyzeSourcefile(file);
        }
    }

    private void AnalyzeSourcefile(string file)
    {
        _identifiersChanged = 0;

        var source = File.ReadAllText(file);
        var syntaxTree = CSharpSyntaxTree.ParseText(source);
        var root = syntaxTree.GetCompilationUnitRoot();

        // Get the things we want to change
        var identifiers = root.DescendantNodes()
                .OfType<IdentifierNameSyntax>()
                .Where(id => ActiveProject.ChangedTypes.ContainsKey(id.Identifier.Text));

        if (!identifiers.Any()) { return; }

        _identifiersChanged += identifiers.Count();

        Logger.Log($"changing {_identifiersChanged} identifiers in file {Path.GetFileName(file)}", ConsoleColor.Green);

        // Do Black Voodoo Magic
        var newRoot = root.ReplaceNodes(identifiers, (oldNode, newNode) =>
                SyntaxFactory.IdentifierName(ActiveProject.ChangedTypes[oldNode.Identifier.Text])
                    .WithLeadingTrivia(oldNode.GetLeadingTrivia())
                    .WithTrailingTrivia(oldNode.GetTrailingTrivia()));

        File.WriteAllText(file, newRoot.ToFullString());
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
        Logger.Log("-----------------------------------------------", ConsoleColor.Green);
        Logger.Log($"Successfully Cross Compiled Project {ActiveProject.SolutionName}", ConsoleColor.Green);
        Logger.Log($"Reversed {_identifiersChanged} Remaps", ConsoleColor.Green);
        Logger.Log($"Original assembly path: {ActiveProject.OriginalAssemblyPath}", ConsoleColor.Green);
        Logger.Log($"Original assembly hash: {ActiveProject.OriginalAssemblyHash}", ConsoleColor.Green);
        Logger.Log($"Final build directory: {ActiveProject.BuildDirectory}", ConsoleColor.Green);
        //Logger.Log($"Original patched assembly hash: {ActiveProject.RemappedAssemblyHash}", ConsoleColor.Green);
        Logger.Log("-----------------------------------------------", ConsoleColor.Green);
    }
}