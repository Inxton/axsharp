// AXSharp.CompilerTests
// Copyright (c) 2023 MTS spol. s r.o.,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/inxton/axsharp/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/inxton/axsharp/blob/dev/LICENSE
// Third party licenses: https://github.com/inxton/axsharp/blob/master/notices.md

using Xunit.Abstractions;
using Xunit.Sdk;

[assembly: TestCollectionOrderer("AXSharp.CompilerTests.DisplayNameOrderer", "AXSharp.CompilerTests")]
[assembly: CollectionBehavior(DisableTestParallelization = true)]

namespace AXSharp.CompilerTests;

public class DisplayNameOrderer : ITestCollectionOrderer
{
    public IEnumerable<ITestCollection> OrderTestCollections(IEnumerable<ITestCollection> testCollections)
    {
        return testCollections.OrderByDescending(collection => collection.DisplayName);
    }
}