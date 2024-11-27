using CliWrap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace AXSharp.Compiler
{
   public static class LegalAcrobatics
{ 
    public static async Task LegalComplianceAcrobatics(string entryAssemblyLocation)
    {
        /*
         We need this to comply with the Siemens legal requirement to allow only users that have access
         to simatic-ax to some APIs.
        */
        
        var stcapipath = Path.GetFullPath(Path.Combine(entryAssemblyLocation, $".apax//.apax//packages//@ax//{GetStcNameByPlatform()}//bin//"));

        if(!Directory.Exists(stcapipath))
        {
            await ApaxInstallLegalAcrobatics(entryAssemblyLocation);
        }
        
        SetupAssemblyResolverLegalAcrobatics(entryAssemblyLocation);
    }

    static IEnumerable<Assembly> AXAssemblies { get; set; }


    static void SetupAssemblyResolverLegalAcrobatics(string entryAssemblyLocation)
    {
        var axAssemblies = new List<Assembly>();

        foreach (var assemblyFile in Directory.EnumerateFiles(
                     Path.GetFullPath(Path.Combine(entryAssemblyLocation, $".apax//.apax//packages//@ax//{GetStcNameByPlatform()}//bin//")), "*.dll"))
        {
            try
            {
                axAssemblies.Add(Assembly.LoadFile(assemblyFile));
            }
            catch (System.BadImageFormatException)
            {
                // We just ignore this...there might be some libraries that we just cannot load, but we do not need them.
            }
        }

        AXAssemblies = axAssemblies;

        AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;
    }

    static async Task ApaxInstallLegalAcrobatics(string entryAssemblyLocation)
    {
        var stdOutBuffer = new StringBuilder();
        var stdErrBuffer = new StringBuilder();
        try
        {
            Directory.CreateDirectory(Path.Combine(entryAssemblyLocation, ".apax"));
            Log.Logger.Information("Restoring apax packages.\n" +
                                   "You need have access to apax registry.\n" +
                                   "use 'apax login' if you did not do it yet.");
            var result = await Cli.Wrap("apax")
                .WithArguments("install")
                .WithWorkingDirectory(Path.Combine(entryAssemblyLocation, ".apax"))
                .WithValidation(CommandResultValidation.None)
                .WithStandardOutputPipe(PipeTarget.ToStringBuilder(stdOutBuffer))
                .WithStandardErrorPipe(PipeTarget.ToStringBuilder(stdErrBuffer))
                .ExecuteAsync();
        }
        catch(Exception ex)
        {
            Log.Logger.Error("There was a problem restoring apax packages.", ex);
        }
        finally
        {
            Log.Logger.Information(stdOutBuffer.ToString());
            Log.Logger.Information(stdErrBuffer.ToString());
        }
    }

    static Assembly? CurrentDomain_AssemblyResolve(object? sender, ResolveEventArgs args)
    {
        return AXAssemblies.FirstOrDefault(p => p.FullName == args.Name);
    }

  static string GetStcNameByPlatform()
       => RuntimeInformation.IsOSPlatform(OSPlatform.Linux) ? "stc-linux-x64" : "stc-win-x64";
}
}
