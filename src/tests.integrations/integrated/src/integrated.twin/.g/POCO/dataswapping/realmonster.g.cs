using System;
using AXSharp.Abstractions.Presentation;

namespace Pocos
{
    namespace RealMonsterData
    {
        public partial class RealMonsterBase : AXSharp.Connector.IPlain
        {
            public RealMonsterBase()
            {
#pragma warning disable CS0612
                AXSharp.Connector.BuilderHelpers.Arrays.InstantiateArray(ArrayOfDrives, () => new RealMonsterData.DriveBaseNested(), new[] { (0, 3) });
#pragma warning restore CS0612
            }

            public string Description { get; set; } = string.Empty;
            public UInt64 Id { get; set; }

            public DateOnly TestDate { get; set; } = default(DateOnly);
            public DateTime TestDateTime { get; set; } = default(DateTime);
            public TimeSpan TestTimeSpan { get; set; } = default(TimeSpan);
            public Byte[] ArrayOfBytes { get; set; } = new Byte[4];
            public RealMonsterData.DriveBaseNested[] ArrayOfDrives { get; set; } = new RealMonsterData.DriveBaseNested[4];
        }

        public partial class RealMonster : RealMonsterData.RealMonsterBase, AXSharp.Connector.IPlain
        {
            public RealMonster() : base()
            {
            }

            public RealMonsterData.DriveBaseNested DriveA { get; set; } = new RealMonsterData.DriveBaseNested();
        }

        public partial class DriveBaseNested : AXSharp.Connector.IPlain
        {
            public DriveBaseNested()
            {
            }

            public Double Position { get; set; }

            public Double Velo { get; set; }

            public Double Acc { get; set; }

            public Double Dcc { get; set; }

            public RealMonsterData.NestedLevelOne NestedLevelOne { get; set; } = new RealMonsterData.NestedLevelOne();
        }

        public partial class NestedLevelOne : AXSharp.Connector.IPlain
        {
            public NestedLevelOne()
            {
            }

            public Double Position { get; set; }

            public Double Velo { get; set; }

            public Double Acc { get; set; }

            public Double Dcc { get; set; }

            public RealMonsterData.NestedLevelTwo NestedLevelTwo { get; set; } = new RealMonsterData.NestedLevelTwo();
        }

        public partial class NestedLevelTwo : AXSharp.Connector.IPlain
        {
            public NestedLevelTwo()
            {
            }

            public Double Position { get; set; }

            public Double Velo { get; set; }

            public Double Acc { get; set; }

            public Double Dcc { get; set; }

            public RealMonsterData.NestedLevelThree NestedLevelThree { get; set; } = new RealMonsterData.NestedLevelThree();
        }

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