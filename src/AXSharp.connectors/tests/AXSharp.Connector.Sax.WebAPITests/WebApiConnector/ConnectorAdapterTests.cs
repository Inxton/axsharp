// AXSharp.Connector.S71500.WebAPITests
// Copyright (c) 2023 Peter Kurhajec (PTKu), MTS,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/ix-ax/axsharp/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/ix-ax/axsharp/blob/dev/LICENSE
// Third party licenses: https://github.com/ix-ax/axsharp/blob/master/notices.md

using System;
using AXSharp.Connector.S71500.WebApi;
using Xunit;

namespace AXSharp.Connector.S71500.WebAPITests
{
    public class ConnectorAdapterTests
    {
        [Fact]
        public void should_create_new_instance_of_adapter_with_webapi_factory()
        {
            var connector = new ConnectorAdapter(typeof(WebApiConnectorFactory));
            Assert.IsType<WebApiConnectorFactory>(connector.AdapterFactory);
        }

        [Fact]
        public void should_create_new_instance_of_adapter_with_webapi_factory_via_extension_method()
        {
            var adapter = ConnectorAdapterBuilder.Build().CreateWebApi(Environment.GetEnvironmentVariable("AX_WEBAPI_TARGET"), 
                                                            "Everyone", 
                                                            string.Empty, 
                                                            true);

            Assert.IsType<WebApiConnectorFactory>(adapter.AdapterFactory);
        }

        [Fact]
        public void should_create_new_instance_of_adapter_with_webapi_factory_via_extension_method_2()
        {
            var adapter = ConnectorAdapterBuilder.Build()
                .CreateWebApi(Environment.GetEnvironmentVariable("AX_WEBAPI_TARGET"), 
                                "Everyone", string.Empty, 
                                 ((message, certificate2, arg3, arg4) => true));

            Assert.IsType<WebApiConnectorFactory>(adapter.AdapterFactory);
        }
    }
}