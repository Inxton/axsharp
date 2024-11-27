// exploratory.consoleapp
// Copyright (c) 2023 Peter Kurhajec (PTKu), MTS,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/ix-ax/axsharp/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/ix-ax/axsharp/blob/dev/LICENSE
// Third party licenses: https://github.com/ix-ax/axsharp/blob/master/notices.md

using System.Diagnostics;
using AXSharp.Connector;
using AXSharp.Connector.S71500.WebApi;

namespace exploratory.consoleapp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Entry.Plc.Connector.BuildAndStart();

            byte value = 0;
            while (true)
            {
                var sw = new Stopwatch();
                sw.Restart();
                Entry.Plc.myBYTE.SetAsync(value++).Wait();
                Console.WriteLine(Entry.Plc.myBYTE.GetAsync().Result);
                Console.WriteLine(sw.ElapsedTicks);
            }
        }
    }
}