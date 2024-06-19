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

    public void StartRemap()
    {
        ActiveProject.ChangedTypes.Clear();

        Remapper.InitializeRemap(
            ActiveProject.RemapModels,
            ActiveProject.OriginalAssemblyPath,
            ActiveProject.RemappedAssemblyPath,
            true);

        if (ActiveProject == null)
        {
            Logger.Log("ERROR: No Cross Compiler Project is loaded, create or load one first.", ConsoleColor.Red);
            return;
        }

        if (ActiveProject.ReCodeItProjectPath == string.Empty)
        {
            Logger.Log("ERROR: No ReCodeIt Project directory is set. (Project Creation Failed)", ConsoleColor.Red);
            return;
        }

        Logger.Log("-----------------------------------------------", ConsoleColor.Yellow);
        Logger.Log($"Cross patch remap result", ConsoleColor.Yellow);
        Logger.Log($"Changed {ActiveProject.ChangedTypes.Count} types", ConsoleColor.Yellow);
        Logger.Log($"Original assembly path: {ActiveProject.OriginalAssemblyPath}", ConsoleColor.Yellow);
        Logger.Log($"Original assembly hash: {ActiveProject.OriginalAssemblyHash}", ConsoleColor.Yellow);
        Logger.Log($"Original patched assembly path: {ActiveProject.RemappedAssemblyPath}", ConsoleColor.Yellow);
        Logger.Log($"Original patched assembly hash: {ActiveProject.RemappedAssemblyHash}", ConsoleColor.Yellow);
        Logger.Log("-----------------------------------------------", ConsoleColor.Yellow);
    }

    public void StartCrossCompile()
    {
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

        var fileName = Path.GetFileName(ActiveProject.OriginalAssemblyPath);
        var outPath = Path.Combine(ActiveProject.RemappedAssemblyPath, fileName);

        Logger.Log($"Placing original reference back into cloned build directory", ConsoleColor.Green);
        File.Copy(ActiveProject.OriginalAssemblyPath, outPath, true);
    }

    private void AnalyzeSourcefile(string file)
    {
        var source = File.ReadAllText(file);
        var syntaxTree = CSharpSyntaxTree.ParseText(source);
        var root = syntaxTree.GetCompilationUnitRoot();

        // Get the things we want to change
        var identifiers = root.DescendantNodes()
                .OfType<IdentifierNameSyntax>()
                .Where(id => ActiveProject.ChangedTypes.ContainsKey(id.Identifier.Text));

        if (!identifiers.Any()) { return; }

        Logger.Log($"changing {identifiers.Count()} identifiers in file {Path.GetFileName(file)}", ConsoleColor.Green);

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

        // clean the project first
        ExecuteDotnetCommand("clean", ActiveProject.VisualStudioClonedSolutionDirectory);

        // Restore packages
        ExecuteDotnetCommand("restore", ActiveProject.VisualStudioClonedSolutionDirectory);

        // Then build the project
        ExecuteDotnetCommand(arguements, ActiveProject.VisualStudioClonedSolutionDirectory);
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
    }
}