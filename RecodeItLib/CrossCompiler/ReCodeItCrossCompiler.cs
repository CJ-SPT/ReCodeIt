using Microsoft.Build.Locator;
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
        SW.Reset();
        SW.Start();

        var workspace = MSBuildWorkspace.Create();

        Logger.Log("Loading Solution...", ConsoleColor.Yellow);

        var solution = await Task.Run(() => LoadSolutionAsync(workspace, ActiveProject.VisualStudioSolutionPath));

        Project newProject;

        // Make sure we loop over the Id's instead of projects, because they are immutable
        foreach (var projId in solution.ProjectIds)
        {
            newProject = solution.GetProject(projId);

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

            newProject = ChangeReferencePath(newProject);

            await CompileProject(newProject);
        }
    }

    private Project ChangeReferencePath(Project project)
    {
        foreach (var reference in project.MetadataReferences)
        {
            Logger.Log(reference.Display);

            if (reference.Display.Contains(ActiveProject.OriginalAssemblyDllName))
            {
                Logger.Log("Removing old reference...", ConsoleColor.Yellow);

                // Remove the reference from the project
                project = project.RemoveMetadataReference(reference);
                break;
            }
        }

        Logger.Log("Creating new reference...", ConsoleColor.Yellow);
        var newRef = MetadataReference.CreateFromFile(ActiveProject.OriginalAssemblyPath);
        project = project.AddMetadataReference(newRef);

        return project;
    }

    private async Task CompileProject(Project project)
    {
        Logger.Log("Compiling Project...", ConsoleColor.Yellow);

        var comp = await project.GetCompilationAsync();

        foreach (var diag in comp.GetDiagnostics())
        {
            Logger.Log(diag.ToString());
        }

        WriteAssembly(comp);
    }

    private void WriteAssembly(Compilation comp)
    {
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
}