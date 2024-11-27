// AXSharp.Connector.S71500.WebAPITests
// Copyright (c) 2023 Peter Kurhajec (PTKu), MTS,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/ix-ax/axsharp/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/ix-ax/axsharp/blob/dev/LICENSE
// Third party licenses: https://github.com/ix-ax/axsharp/blob/master/notices.md

using System.Collections.Generic;
using System.Linq;
using Xunit;
using Xunit.Abstractions;


[assembly: TestCollectionOrderer("AXSharp.Connector.Sax.WebAPITests.DisplayNameOrderer", "AXSharp.Connector.Sax.WebAPITests")]
[assembly: CollectionBehavior(DisableTestParallelization = true)]

namespace AXSharp.Connector.Sax.WebAPITests;

public class DisplayNameOrderer : ITestCollectionOrderer
{
    public IEnumerable<ITestCollection> OrderTestCollections(IEnumerable<ITestCollection> testCollections)
    {
        return testCollections.OrderBy(collection => collection.DisplayName);
    }
}