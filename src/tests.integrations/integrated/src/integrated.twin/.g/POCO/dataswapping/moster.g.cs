using System;
using AXSharp.Abstractions.Presentation;
using AXSharp.Connector;

namespace MonsterData
{
    namespace Pocos
    {
        public partial class MonsterBase : AXSharp.Connector.IPlain
        {
            public MonsterBase()
            {
#pragma warning disable CS0612
                AXSharp.Connector.BuilderHelpers.Arrays.InstantiateArray(ArrayOfDrives, () => new MonsterData.Pocos.DriveBase(), new[] { (0, 3) });
#pragma warning restore CS0612
            }

            public string Description { get; set; } = string.Empty;
            public UInt64 Id { get; set; }

            public Byte[] ArrayOfBytes { get; set; } = new Byte[4];
            public MonsterData.Pocos.DriveBase[] ArrayOfDrives { get; set; } = new MonsterData.Pocos.DriveBase[4];
            [IgnoreOnPocoOperation()]
            public MonsterData.Pocos.DriveBase DriveBase_tobeignoredbypocooperations { get; set; } = new MonsterData.Pocos.DriveBase();
            [IgnoreOnPocoOperation()]
            public string Description_tobeignoredbypocooperations { get; set; } = string.Empty;
        }
    }

    namespace Pocos
    {
        public partial class Monster : MonsterData.Pocos.MonsterBase, AXSharp.Connector.IPlain
        {
            public Monster() : base()
            {
            }

            public MonsterData.Pocos.DriveBase DriveA { get; set; } = new MonsterData.Pocos.DriveBase();
        }
    }

    namespace Pocos
    {
        public partial class DriveBase : AXSharp.Connector.IPlain
        {
            public DriveBase()
            {
            }

            public Double Position { get; set; }

            public Double Velo { get; set; }

            public Double Acc { get; set; }

            public Double Dcc { get; set; }
        }
    }
}