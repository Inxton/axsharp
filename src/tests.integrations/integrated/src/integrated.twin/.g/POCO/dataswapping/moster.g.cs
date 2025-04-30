using System;
using AXSharp.Abstractions.Presentation;
using AXSharp.Connector;

namespace Pocos
{
    namespace MonsterData
    {
        public partial class MonsterBase : AXSharp.Connector.IPlain
        {
            public MonsterBase()
            {
#pragma warning disable CS0612
                AXSharp.Connector.BuilderHelpers.Arrays.InstantiateArray(ArrayOfDrives, () => new global::Pocos.MonsterData.DriveBase(), new[] { (0, 3) });
#pragma warning restore CS0612
            }

            public string Description { get; set; } = string.Empty;
            public UInt64 Id { get; set; }
            public Byte[] ArrayOfBytes { get; set; } = new Byte[4];
            public MonsterData.DriveBase[] ArrayOfDrives { get; set; } = new MonsterData.DriveBase[4];
            public MonsterData.DriveBase DriveBase_tobeignoredbypocooperations { get; set; } = new MonsterData.DriveBase();
            public string Description_tobeignoredbypocooperations { get; set; } = string.Empty;
        }

        public partial class Monster : MonsterData.MonsterBase, AXSharp.Connector.IPlain
        {
            public Monster() : base()
            {
            }

            public MonsterData.DriveBase DriveA { get; set; } = new MonsterData.DriveBase();
        }

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