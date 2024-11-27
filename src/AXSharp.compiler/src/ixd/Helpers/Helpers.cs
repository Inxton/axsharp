using AX.ST.Semantic.Model.Declarations;
using AX.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace AXSharp.ixc_doc.Helpers
{
    internal static class Helpers
    {
        public static string GetBaseUid(IDeclaration declaration) => $"plc.{declaration.FullyQualifiedName}";

        public static string GetBaseUid(string @namespace) => $"plc.{@namespace}";

        public static string GetBaseUid(IFunctionDeclaration functionDeclaration)
        {
            var inputParamsDeclaration = functionDeclaration.Variables.Where(v => v.Section == Section.Input).ToList();
            var declaration = GetMethodTypeDeclaration(inputParamsDeclaration);
            return $"plc.{functionDeclaration.FullyQualifiedName}({declaration})";
        }

        public static string GetBaseUid(IMethodDeclaration methodDeclaration)
        {
            var inputParamsDeclaration = methodDeclaration.Variables.Where(v => v.Section == Section.Input).ToList();
            var declaration = GetMethodTypeDeclaration(inputParamsDeclaration);
            return $"plc.{methodDeclaration.FullyQualifiedName}({declaration})";
        }


        public static string GetBaseUid(IMethodPrototypeDeclaration methodDeclaration)
        {
            var inputParamsDeclaration = methodDeclaration.Variables.Where(v => v.Section == Section.Input).ToList();
            var declaration = GetMethodTypeDeclaration(inputParamsDeclaration);
            return $"plc.{methodDeclaration.FullyQualifiedName}({declaration.ToString()})";
        }


        private static string GetMethodTypeDeclaration(IList<IVariableDeclaration> inputParams)
        {
            StringBuilder typeDeclaration = new StringBuilder();

            foreach (var p in inputParams)
            {
                typeDeclaration.Append(p.Type);
                if (inputParams.Last() != p)
                {
                    typeDeclaration.Append(",");
                }
            }
            return typeDeclaration.ToString();
        }


        public static string? FindGitRepositoryRoot(string directoryPath)
        {
            DirectoryInfo? directory = new DirectoryInfo(directoryPath);

            while (directory != null)
            {
                if (Directory.Exists(Path.Combine(directory.FullName, ".git")))
                {
                    return directory.FullName;
                }

                directory = directory.Parent;
            }

            return null;
        }

        public static (string?, string?) GetGitBranchAndRepo(string gitPath)
        {
            LibGit2Sharp.Branch? currentBranch = null;
            string? remoteUrl = null;

            using (var repo = new LibGit2Sharp.Repository(gitPath))
            {
                currentBranch = repo.Head;

                if (repo.Network.Remotes.Count() > 0)
                {
                    remoteUrl = repo.Network.Remotes["origin"].Url;
                }
            }

            return (currentBranch.FriendlyName, remoteUrl);
        }

        public static int GetLineNumber(int characterCount, TextLineCollection lines)
        {
            int totalCharacters = 0;
            int lineNumber = 0;

            foreach (TextLine line in lines)
            {
                totalCharacters += line.Span.Length;

                if (totalCharacters >= characterCount)
                {
                    return lineNumber;
                }

                lineNumber++;
            }

            return lineNumber;
        }
    }
}