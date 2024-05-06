// See https://aka.ms/new-console-template for more information
using AX.ST.Semantic;
using AX.ST.Syntax.Parser;
using AX.ST.Syntax.Tree;
using AX.Text;
using AX.Text.Diagnostics;
using AXSharp.Compiler;
using AXSharp.ixc_doc;
using System;
using CommandLine;
using System.Reflection;
using System.Text;
using CliWrap;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;
using AX.ST.Semantic.Model;
using AXSharp.ixr_doc;
using Microsoft.CodeAnalysis;
using Serilog.Parsing;
using static System.Net.Mime.MediaTypeNames;
using System.Text.RegularExpressions;


const string Logo =
@"     ___      ___   ___    _  _   
    /   \     \  \ /  /  _| || |_ 
   /  ^  \     \  V  /  |_  __  _|
  /  /_\  \     >   <    _| || |_ 
 /  _____  \   /  .  \  |_  __  _|
/__/     \__\ /__/ \__\   |_||_| 
";


Console.WriteLine(Logo);
LegalAcrobatics.LegalComplianceAcrobatics(new FileInfo(Assembly.GetExecutingAssembly().Location).Directory.FullName).Wait();
Console.WriteLine("Ixr compiler - compiles plc localized strings resources to resx");
Console.WriteLine($"Version: {GitVersionInformation.SemVer}");
Parser.Default.ParseArguments<Options>(args)
            .WithParsed(o =>
            {
                var recoverCurrentDirectory = Environment.CurrentDirectory;
                try
                {
                   Generate(o);
                   Console.WriteLine("Done.");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
                finally
                {
                    Environment.CurrentDirectory = recoverCurrentDirectory;
                }
            });



void Generate(Options o)
{
    var axProjectFolder = string.IsNullOrEmpty(o.AxSourceProjectFolder)
        ? Environment.CurrentDirectory
        : o.AxSourceProjectFolder;

    var axProject = new AxProject(axProjectFolder);
    var axProjectConfig =
        AXSharpConfig.RetrieveAXSharpConfig(Path.Combine(axProject.ProjectFolder, AXSharpConfig.CONFIG_FILE_NAME));

    (string folder, string file) output = string.IsNullOrEmpty(axProjectConfig.OutputProjectFolder)
        ? (string.Empty, o.OutputProjectFolder)
        : (Path.GetFullPath(Path.Combine(axProject.ProjectFolder, axProjectConfig.OutputProjectFolder, "Resources")), "PlcStringResources.resx");

    Console.WriteLine($"Compiling project {axProject.ProjectInfo.Name}...");

    var projectSources = axProject.Sources.Select(p => (parseTree: STParser.ParseTextAsync(p).Result, source: p));

    var syntaxTrees = projectSources.Select(p => p.parseTree);

    var lw = new LocalizedStringWrapper();

    //iterate all syntax trees from project
    foreach (var syntaxTree in syntaxTrees)
    {
        IterateSyntaxTreeForStringLiterals(syntaxTree.GetRoot(),lw, Path.GetRelativePath(axProjectFolder, syntaxTree.Filename));

        IterateSyntaxTreeForPragmas(syntaxTree.GetRoot(), lw, Path.GetRelativePath(axProjectFolder, syntaxTree.Filename));
    }

    //add resources from dictionary to resx file
    ResxManager.AddResourcesFromDictionary(output.folder, output.file, lw.LocalizedStringsDictionary);
}

void IterateSyntaxTreeForStringLiterals(ISyntaxNode root, LocalizedStringWrapper lw, string fileName)
{
    foreach (var literalSyntax in GetChildNodesRecursive(root).OfType<ILiteralSyntax>())
    {
        var token = literalSyntax.Tokens.First();
        //literalSyntax.Location
        AddToDictionaryIfLocalizedString(token,lw,fileName);
    }
}

void IterateSyntaxTreeForPragmas(ISyntaxNode root, LocalizedStringWrapper lw, string fileName)
{
    foreach (var pragmaSyntax in GetChildNodesRecursive(root).OfType<IPragmaSyntax>())
    {
        var token = pragmaSyntax.PragmaToken;
        if(lw.IsAttributeNamePragmaToken(token.Text))
        { 
            AddToDictionaryIfLocalizedString(token,lw,fileName);
        }
    }
}

void AddToDictionaryIfLocalizedString(ISyntaxToken token, LocalizedStringWrapper lw, string fileName)
{
    // if is valid token
    if(IsStringToken(token) || IsPragmaToken(token))
    {
        // try to acquire localized string
        var localizedStringList = lw.TryToGetLocalizedStrings(token.Text);

        if(localizedStringList == null) 
        {
            return;
        }

        foreach (string localizedString in localizedStringList)
        {
            //get raw text from localized string
            var rawText = lw.GetRawTextFromLocalizedString(localizedString);

            //create id
            var id = AXSharp.Connector.Localizations.LocalizationHelper.CreateId(rawText);

            //check if identifier is valid
            if(lw.IsValidId(id))
            { 
                var pos = token.Location.GetLineSpan().StartLinePosition;
                var wrapper = new StringValueWrapper(rawText, fileName, pos.Line);
                // add id and wrapper to dictionary
                lw.LocalizedStringsDictionary.TryAdd(id, wrapper);
            }
        }   
    }
}
bool IsPragmaToken(ISyntaxToken token)
{ 
    if(token.SyntaxKind == SyntaxKind.PragmaToken) 
    { 
        return true;
    }
    return false; 
}

bool IsStringToken(ISyntaxToken token)
{ 
    if(token.SyntaxKind == SyntaxKind.TypedStringDToken ||
        token.SyntaxKind == SyntaxKind.TypedStringSToken ||
        token.SyntaxKind == SyntaxKind.UntypedStringDToken ||
        token.SyntaxKind == SyntaxKind.UntypedStringSToken) 
    { 
        return true;
    }
    return false; 
}

IEnumerable<ISyntaxNode> GetChildNodesRecursive(ISyntaxNode syntaxNode)
{
    yield return syntaxNode;

    foreach (ISyntaxNode node in syntaxNode.ChildNodes)
    {
        foreach (ISyntaxNode childNode in GetChildNodesRecursive(node))
        {
            yield return childNode;
        }
    }
}