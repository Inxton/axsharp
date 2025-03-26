using System;
using AXSharp.Abstractions.Presentation;
using AXSharp.Connector;
using Pocos.RealMonsterData;

namespace Pocos
{
    public partial class Pokus : AXSharp.Connector.IPlain
    {
        public Pokus()
        {
        }
    }

    public partial class Nested : AXSharp.Connector.IPlain
    {
        public Nested()
        {
        }
    }
}