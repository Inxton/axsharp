// hello.world.console
// Copyright (c) 2023 Peter Kurhajec (PTKu), MTS,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/ix-ax/axsharp/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/ix-ax/axsharp/blob/dev/LICENSE
// Third party licenses: https://github.com/ix-ax/axsharp/blob/master/notices.md

using System.Threading.Channels;
using AXSharp.Connector;
using AXSharp.Connector.S71500.WebApi;

namespace hello.world.console
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            // Print out fancy text.
            Console.WriteLine("░▒█░▒█░█▀▀░█░░█░░▄▀▀▄░░░▀█▀░▀▄░▄▀\r\n░▒█▀▀█░█▀▀░█░░█░░█░░█░░░▒█░░░▒█░░\r\n░▒█░▒█░▀▀▀░▀▀░▀▀░░▀▀░░░░▄█▄░▄▀▒▀▄\r\n");

            // Print our plc code.
            Console.WriteLine(
                @"CONFIGURATION MyConfiguration
    TASK Main(Interval := T#1000ms, Priority := 1);
    PROGRAM P1 WITH Main: MyProgram;

    VAR_GLOBAL
        Counter : ULINT;
        HelloWorld : STRING := 'Hello world';
    END_VAR
END_CONFIGURATION");


            // Creates twin connecting to actual PLC.
            // You may need to change the IP address and credentials.
            var twin = new hello_world_console_plcTwinController(ConnectorAdapterBuilder.Build()
                .CreateWebApi("192.168.0.1", "Everybody", "", true));

            // Start PLC twin operations
            twin.Connector.BuildAndStart();

            // If on windows you can run speech synthesizer that will say the value of the counter when value changes.
            // Uncomment if on windows
            /*
            var ss = new System.Speech.Synthesis.SpeechSynthesizer() { Rate = 5 };
            twin.Counter.Subscribe( (a,b) => ss.SpeakAsync(twin.Counter.Cyclic.ToString()));
            */

            while (true)
            {
                Console.WriteLine("HelloString is: " + await twin.HelloWorld.GetAsync());
                Console.WriteLine("Counter is: " + await twin.Counter.GetAsync());
                Console.Write("Write something to the plc and press enter \n(write RESET-COUNTER to zero counter, write 'quit' to terminate):");
                var answer = Console.ReadLine();

                if (answer == "quit")
                {
                    break;
                }

                await twin.HelloWorld.SetAsync(answer);
            }
        }
    }
}