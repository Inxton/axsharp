using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommandLine;

namespace AXSharp.TIA2AXTool
{

    public class Options
    {
    
        [Option('i', "ipaddress", Required = true, HelpText = "ip address for connector")]
        public string Ip { get; set; }

        [Option('u', "username", Default = "Everybody", HelpText = "username")]
        public string Username { get; set; }

        [Option('p', "password", Default = "", HelpText = "password")]
        public string Password { get; set; }

        [Option('d', "datablocks", Required = false, HelpText = "list of datablocks which should be used")]
        public IEnumerable<string> DataBlocks { get; set; }

        [Option('o', "output", Required = true, HelpText = "output serialized adapter")]
        public string? Output{ get; set; }

        [Option('s', "symbol", HelpText = "symbol from which scanning will occur")]
        public string? Symbol { get; set; }

    }

}
