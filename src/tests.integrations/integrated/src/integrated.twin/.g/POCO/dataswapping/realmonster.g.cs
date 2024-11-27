using System;
using AXSharp.Abstractions.Presentation;
using AXSharp.Connector;

namespace RealMonsterData
{
    namespace Pocos
    {
        public partial class RealMonsterBase : AXSharp.Connector.IPlain
        {
            public RealMonsterBase()
            {
#pragma warning disable CS0612
                AXSharp.Connector.BuilderHelpers.Arrays.InstantiateArray(ArrayOfDrives, () => new RealMonsterData.Pocos.DriveBaseNested(), new[] { (0, 3) });
#pragma warning restore CS0612
            }

            public string Description { get; set; } = string.Empty;
            public UInt64 Id { get; set; }

            public DateOnly TestDate { get; set; } = default(DateOnly);
            public DateTime TestDateTime { get; set; } = default(DateTime);
            public TimeSpan TestTimeSpan { get; set; } = default(TimeSpan);
            public Byte[] ArrayOfBytes { get; set; } = new Byte[4];
            public RealMonsterData.Pocos.DriveBaseNested[] ArrayOfDrives { get; set; } = new RealMonsterData.Pocos.DriveBaseNested[4];
        }
    }

    namespace Pocos
    {
        public partial class RealMonster : RealMonsterData.Pocos.RealMonsterBase, AXSharp.Connector.IPlain
        {
            public RealMonster() : base()
            {
            }

            public RealMonsterData.Pocos.DriveBaseNested DriveA { get; set; } = new RealMonsterData.Pocos.DriveBaseNested();
        }
    }

    namespace Pocos
    {
        public partial class DriveBaseNested : AXSharp.Connector.IPlain
        {
            public DriveBaseNested()
            {
            }

            public Double Position { get; set; }

            public Double Velo { get; set; }

            public Double Acc { get; set; }

            public Double Dcc { get; set; }

            public RealMonsterData.Pocos.NestedLevelOne NestedLevelOne { get; set; } = new RealMonsterData.Pocos.NestedLevelOne();
        }
    }

    namespace Pocos
    {
        public partial class NestedLevelOne : AXSharp.Connector.IPlain
        {
            public NestedLevelOne()
            {
            }

            public Double Position { get; set; }

            public Double Velo { get; set; }

            public Double Acc { get; set; }

            public Double Dcc { get; set; }

            public RealMonsterData.Pocos.NestedLevelTwo NestedLevelTwo { get; set; } = new RealMonsterData.Pocos.NestedLevelTwo();
        }
    }

    namespace Pocos
    {
        public partial class NestedLevelTwo : AXSharp.Connector.IPlain
        {
            public NestedLevelTwo()
            {
            }

            public Double Position { get; set; }

            public Double Velo { get; set; }

            public Double Acc { get; set; }

            public Double Dcc { get; set; }

            public RealMonsterData.Pocos.NestedLevelThree NestedLevelThree { get; set; } = new RealMonsterData.Pocos.NestedLevelThree();
        }
    }

    namespace Pocos
    {
        public partial class NestedLevelThree : AXSharp.Connector.IPlain
        {
            public NestedLevelThree()
            {
            }

            public Double Position { get; set; }

            public Double Velo { get; set; }

            public Double Acc { get; set; }

            public Double Dcc { get; set; }
        }
    }
}