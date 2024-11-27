// ix-integration-plc
// Copyright (c) 2023 Peter Kurhajec (PTKu), MTS,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/ix-ax/axsharp/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/ix-ax/axsharp/blob/dev/LICENSE
// Third party licenses: https://github.com/ix-ax/axsharp/blob/master/notices.md

//#define dummy

using AXSharp.Connector;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using AXSharp.Connector.S71500.WebApi;

namespace ix_integration_plc
{
    public static class Entry
    {
#if !dummy
        public static ix_integration_plcTwinController Plc { get; } = new (ConnectorAdapterBuilder.Build().CreateWebApi(Environment.GetEnvironmentVariable("AXTARGET"), "Everybody", "", true));
#else
        public static ix_integration_plcTwinController Plc { get; } = new(ConnectorAdapterBuilder.Build().CreateDummy());
#endif
    }


    //public static class PlcResources
    //{
    //    private static System.Resources.ResourceManager _resourceManger;
    //    private static volatile object mutex = new object();
    //    internal static System.Resources.ResourceManager ResourceManager
    //    {
    //        get
    //        {
    //            lock (mutex)
    //            {
    //                if (_resourceManger == null)
    //                {
    //                    var defaultResourceType = Assembly.GetAssembly(typeof(PlcResources))
    //                        .GetType("Resources.PlcStringResources");
    //                    if (defaultResourceType != null)
    //                    {
    //                        _resourceManger = new ResourceManager(defaultResourceType);
    //                    }
    //                }

    //                return _resourceManger;
    //            }
    //        }
    //    }
    //}
}
