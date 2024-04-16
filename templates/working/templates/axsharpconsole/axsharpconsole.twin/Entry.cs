// axsharpconsole
// Copyright (c) 2023 Peter Kurhajec (PTKu), MTS,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/ix-ax/axsharp/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/ix-ax/axsharp/blob/dev/LICENSE
// Third party licenses: https://github.com/ix-ax/axsharp/blob/master/notices.md

using AXSharp.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AXSharp.Connector.S71500.WebApi;

namespace axsharpconsole
{
    public static class Entry
    {
        private const string TargetIp = "192.168.0.4"; // <- replace by your IP 
        private const string UserName = "Anonymous"; //<- replace by user name you have set up in your WebAPI settings
        private const string Pass = ""; // <- Pass in the password that you have set up for the user. NOT AS PLAIN TEXT! Use user secrets instead.
        private const bool IgnoreSslErrors = true; // <- When you have your certificates in order set this to false.

        public static axsharpconsoleTwinController Plc { get; } 
            = new (ConnectorAdapterBuilder.Build()
                .CreateWebApi(TargetIp, UserName, Pass, IgnoreSslErrors));
    }
}