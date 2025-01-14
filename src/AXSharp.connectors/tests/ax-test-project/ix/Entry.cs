// ax_test_project
// Copyright (c) 2023 MTS spol. s r.o.,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/inxton/axsharp/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/inxton/axsharp/blob/dev/LICENSE
// Third party licenses: https://github.com/inxton/axsharp/blob/master/notices.md

using AXSharp.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AXSharp.Connector.S71500.WebApi;


namespace exploratory
{
    public static class Entry
    {
        public static ax_test_projectTwinController Plc { get; } = new ax_test_projectTwinController(ConnectorAdapterBuilder.Build().CreateWebApi("10.10.10.100", "Everybody", "", true));
    }
}
