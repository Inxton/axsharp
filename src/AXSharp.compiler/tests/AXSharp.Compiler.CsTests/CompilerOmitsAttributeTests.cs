// AXSharp.ConnectorLegacyTests
// Copyright (c) 2023 Peter Kurhajec (PTKu), MTS,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/ix-ax/axsharp/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/ix-ax/axsharp/blob/dev/LICENSE
// Third party licenses: https://github.com/ix-ax/axsharp/blob/master/notices.md

using Xunit;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AXSharp.Connector;

namespace AXSharp.Compiler.CsTests
{
 
    public class CompilerOmitsAttributeLegacyTests
    {
        [Fact]
        public void CompilerOmitsAttributeCtorTest()
        {
            //-- Act

            var actual = new CompilerOmitsAttribute(CompilerOmissionGroups.BuilderOnliner, 
                                                    CompilerOmissionGroups.BuilderPlainer,
                                                    CompilerOmissionGroups.BuilderOnlinerInterface,
                                                    CompilerOmissionGroups.BuilderShadowerInterface);

            //-- Assert
            Assert.Equal(actual.Omissions.Count(), 4);
            Assert.Equal("BuilderOnliner", actual.Omissions.ToArray()[0]);
            Assert.Equal("BuilderPlainer", actual.Omissions.ToArray()[1]);
            Assert.Equal("BuilderOnlinerInterface", actual.Omissions.ToArray()[2]);
            Assert.Equal("BuilderShadowerInterface", actual.Omissions.ToArray()[3]);

        }
    }
}
