// AXSharp.Connector.S71500.WebAPITests
// Copyright (c) 2023 Peter Kurhajec (PTKu), MTS,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/ix-ax/axsharp/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/ix-ax/axsharp/blob/dev/LICENSE
// Third party licenses: https://github.com/ix-ax/axsharp/blob/master/notices.md

using Xunit;
using AXSharp.Connector.S71500.WebApi;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AXSharp.Connector.S71500.WebAPITests.Primitives;

namespace AXSharp.Connector.S71500.WebApi.Tests.Issues
{
    public class GH_PTKu_ix_56
    {
        
        protected static WebApiConnector Connector { get; } = TestConnector.TestApiConnector as WebApiConnector;
        [Fact()]
        public async Task reproduction()
        {
            try
            {
                TestConnector.TestApiConnector.ReadWriteCycleDelay = 2;
                var baseTypeMember = new WebApiString(Connector, "", $"GH_PKTu_ix_56_SecondInheritance.$base.$base.baseMember");
                await baseTypeMember.GetAsync();

                var firstInheritanceTypeMember = new WebApiString(Connector, "", $"GH_PKTu_ix_56_SecondInheritance.$base.FirstInheritanceMember");
                await firstInheritanceTypeMember.GetAsync();

                var secondInheritanceTypeMember = new WebApiString(Connector, "", $"GH_PKTu_ix_56_SecondInheritance.SecondInheritanceMember");
                await secondInheritanceTypeMember.GetAsync();

                var baseComplexMember = new WebApiString(Connector, "", $"GH_PKTu_ix_56_SecondInheritance.$base.$base.baseComplexMember.Counter");
                await baseComplexMember.GetAsync();

                var firstInheritanceComplexMember = new WebApiString(Connector, "", $"GH_PKTu_ix_56_SecondInheritance.$base.FirstInheritanceComplexMember.Counter");
                await firstInheritanceComplexMember.GetAsync();

                var secondInheritanceComplexMember = new WebApiString(Connector, "", $"GH_PKTu_ix_56_SecondInheritance.SecondInheritanceComplexMember.Counter");
                await secondInheritanceComplexMember.GetAsync();
            }
            catch (Exception e)
            {
               return;
            }

            throw new Exception("We should not be here");
        }

        [Fact()]
        public async Task reproduction_no_base()
        {
            TestConnector.TestApiConnector.ReadWriteCycleDelay = 2;
            var baseTypeMember = new WebApiString(Connector, "", $"GH_PKTu_ix_56_SecondInheritance.baseMember");
            await baseTypeMember.GetAsync();

            var firstInheritanceTypeMember = new WebApiString(Connector, "", $"GH_PKTu_ix_56_SecondInheritance.FirstInheritanceMember");
            await firstInheritanceTypeMember.GetAsync();

            var secondInheritanceTypeMember = new WebApiString(Connector, "", $"GH_PKTu_ix_56_SecondInheritance.SecondInheritanceMember");
            await secondInheritanceTypeMember.GetAsync();

            var baseComplexMember = new WebApiString(Connector, "", $"GH_PKTu_ix_56_SecondInheritance.baseComplexMember.Counter");
            await baseComplexMember.GetAsync();

            var firstInheritanceComplexMember = new WebApiString(Connector, "", $"GH_PKTu_ix_56_SecondInheritance.FirstInheritanceComplexMember.Counter");
            await firstInheritanceComplexMember.GetAsync();

            var secondInheritanceComplexMember = new WebApiString(Connector, "", $"GH_PKTu_ix_56_SecondInheritance.SecondInheritanceComplexMember.Counter");
            await secondInheritanceComplexMember.GetAsync();
        }

        [Fact()]
        public async Task run_on_twin_connector()
        {
            var twin = new ax_test_projectTwinController(ConnectorAdapterBuilder.Build()
                .CreateWebApi(Environment.GetEnvironmentVariable("AX_WEBAPI_TARGET"), "Everybody", Environment.GetEnvironmentVariable("AX_TARGET_PWD"), true));

            var primitives = twin.GH_PKTu_ix_56_SecondInheritance.RetrievePrimitives().Select(p => p.Symbol).ToList();

            await twin.GH_PKTu_ix_56_SecondInheritance.ReadAsync();
        }
    }
}