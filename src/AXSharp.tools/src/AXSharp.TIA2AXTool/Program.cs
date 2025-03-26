// See https://aka.ms/new-console-template for more information


using CommandLine;
using AXSharp.TIA2AXTool;
using AXSharp.Connector.S71500.WebApi;
using AXSharp.TIA2AXSharp;
using System.Diagnostics;
using Siemens.Simatic.S7.Webserver.API.Exceptions;
using System.Runtime.CompilerServices;
using System.Text;

Parser.Default.ParseArguments<Options>(args)
.WithParsed(o =>
{
    Main(o);
});

string TransformSymbol(string symbol)
{
    var splitted = symbol.Split('.');
    var sb = new StringBuilder();
    foreach (var item in splitted)
    {
        // if it is array, add quoation marks only to text part for correct syntax for webapi
        // two dimensional array are not supported yet
        if (item.Last() == ']')
        {
            var arraySplitted = item.Split('[');
            sb.AppendDelim($"\"{arraySplitted[0]}\"[{arraySplitted[1]}");
        }
        else
        {
            sb.AppendDelim($"\"{item}\"");
        }
    }

   
    return sb.ToString();   
}


void Main(Options o)
{
    Console.WriteLine("** TIA2AX **");
    Console.WriteLine("Generator of TwinObjects for TIA projects");
    Console.WriteLine("------------------------------------------");
    Console.WriteLine($"Connecting to {o.Ip}...");
  
    Stopwatch sw = new Stopwatch();
    sw.Start();
    var connector = new WebApiConnector(o.Ip, o.Username, o.Password, true, dbName:string.Empty);
    Console.WriteLine("Connected.");
    Console.WriteLine("Generating...");

    TIARootObject root;

    try
    {
        if (o.Symbol != null)
        {
            var requestedSymbol = TransformSymbol(o.Symbol);
            Console.WriteLine($"Requested symbol: {requestedSymbol}");
            root = TIA2AXSharpAdapter.CreateTIARootObject(connector, requestedSymbol).Result;
        }
        else
        {
            root = TIA2AXSharpAdapter.CreateTIARootObject(connector, o.DataBlocks?.ToArray()).Result;
        }
    }
    catch (Exception ex)
    {
        Console.Error.WriteLine($"Cannot create adapter! \n -------------------- \n {ex}");
        Environment.ExitCode = -1;
        return;
    }
    

    try
    {
        TIA2AXSharpSerializer.Serialize(root, o.Output);
    }  
    catch (Exception ex) 
    {
        Console.Error.WriteLine($"Serialization error occured! \n -------------------- \n {ex}");
        Environment.ExitCode = -1;
        return;
    }


    sw.Stop();

    Console.WriteLine("Done.");
    Console.WriteLine("------------------------------------------");
    Console.WriteLine($"Returned {root.TIABrowseElements.Count} TIABrowseElements.");
    Console.WriteLine($"Location: {o.Output}");
    Console.WriteLine($"Elapsed time: {sw.Elapsed}");
    Environment.ExitCode = 0;
    return;

}

static class StringBuilderExtensions
{
    public static void AppendDelim(this StringBuilder sb, string text, string delim = ".", bool writeEmptyString = true)
    {
        if (writeEmptyString || !String.IsNullOrWhiteSpace(text))
        {
            if (sb.Length != 0)
            {
                sb.Append(delim + text);
            }
            else
            {
                sb.Append(text);
            }
        }
    }
}