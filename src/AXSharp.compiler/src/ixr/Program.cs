// See https://aka.ms/new-console-template for more information
using AX.ST.Semantic;
using AX.ST.Semantic.Model;
using AX.ST.Semantic.Model.Declarations;
using AX.ST.Semantic.Pragmas;
using AX.ST.Syntax.Parser;
using AX.ST.Syntax.Tree;
using AX.Text;
using AX.Text.Diagnostics;
using AXSharp.Compiler;
using AXSharp.ixc_doc;
using AXSharp.ixr_doc;
using CliWrap;
using CommandLine;
using Microsoft.CodeAnalysis;
using Serilog.Parsing;
using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using static System.Net.Mime.MediaTypeNames;


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
        AddToDictionaryIfLocalizedStringInLiterals(literalSyntax, lw, fileName);
    }
}

void IterateSyntaxTreeForPragmas(ISyntaxNode root, LocalizedStringWrapper lw, string fileName)
{
    foreach (var storage in GetChildNodesRecursive(root).OfType<IVariableDeclarationSyntax>())
    {
        storage.GetLeadingPragmas().ToList().ForEach(pragmaSyntax =>
        {
            var token = pragmaSyntax as PragmaSyntax;
            if (lw.IsAttributeNamePragmaToken(pragmaSyntax.Content))
            {
                AddToDictionaryIfLocalizedStringInPragmas(token, lw, fileName);
            }
        });        
    }
    foreach (var storage in GetChildNodesRecursive(root).OfType<IDeclarationSyntax>())
    {
        storage.GetLeadingPragmas().ToList().ForEach(pragmaSyntax =>
        {
            var token = pragmaSyntax;
            if (lw.IsAttributeNamePragmaToken(pragmaSyntax.Content))
            {
                AddToDictionaryIfLocalizedStringInPragmas(token, lw, fileName);
            }
        });
    }
}

void AddToDictionaryIfLocalizedStringInPragmas(PragmaSyntax token, LocalizedStringWrapper lw, string fileName)
{
    
        // try to acquire localized string
        var localizedStringList = lw.TryToGetLocalizedStrings(token.PragmaContent);

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
                var pos = token.SourceText.GetLineSpan(token.Span).StartLinePosition;
                var wrapper = new StringValueWrapper(rawText, fileName, pos.Line);
                // add id and wrapper to dictionary
                lw.LocalizedStringsDictionary.TryAdd(id, wrapper);
            }
        }       
}

void AddToDictionaryIfLocalizedStringInLiterals(ILiteralSyntax literal, LocalizedStringWrapper lw, string fileName)
{
    // if is valid token
    // if (IsStringLiteral(literal) || true)
    {
        foreach (var token in literal.Tokens)
        {


            // try to acquire localized string
            var localizedStringList = lw.TryToGetLocalizedStrings(token.FullText);

            if(localizedStringList == null)
            {
                continue; // ✓ Skip to next token
            }

            foreach (string localizedString in localizedStringList)
            {
                //get raw text from localized string
                var rawText = lw.GetRawTextFromLocalizedString(localizedString);

                //create id
                var id = AXSharp.Connector.Localizations.LocalizationHelper.CreateId(rawText);

                //check if identifier is valid
                if (lw.IsValidId(id))
                {
                    var pos = token.Location.GetLineSpan().StartLinePosition;
                    var wrapper = new StringValueWrapper(rawText, fileName, pos.Line);
                    // add id and wrapper to dictionary
                    lw.LocalizedStringsDictionary.TryAdd(id, wrapper);
                }
            }
        }
    }
}
bool IsPragmaToken(PragmaSyntax token)
{    
    return true; 
}

bool IsStringToken(PragmaSyntax token)
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


bool IsStringLiteral(ILiteralSyntax literal)
{
    if (literal.SyntaxKind == SyntaxKind.TypedStringDToken ||
        literal.SyntaxKind == SyntaxKind.TypedStringSToken ||
        literal.SyntaxKind == SyntaxKind.UntypedStringDToken ||
        literal.SyntaxKind == SyntaxKind.UntypedStringSToken)
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