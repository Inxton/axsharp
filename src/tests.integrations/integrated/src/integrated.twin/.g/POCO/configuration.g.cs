using System;
using AXSharp.Abstractions.Presentation;
using AXSharp.Connector;
using Pocos.RealMonsterData;

namespace Pocos
{
    [AXSharp.Connector.SourceFileAttribute(@"configuration.st")]
    public partial class Pokus : AXSharp.Connector.IPlain
    {
        public Pokus()
        {
        }
    }

    [AXSharp.Connector.SourceFileAttribute(@"configuration.st")]
    public partial class Nested : AXSharp.Connector.IPlain
    {
        public Nested()
        {
        }
    }
}