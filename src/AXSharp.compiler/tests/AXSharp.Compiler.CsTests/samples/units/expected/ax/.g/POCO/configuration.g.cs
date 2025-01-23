using System;
using AXSharp.Abstractions.Presentation;
using AXSharp.Connector;

namespace Pocos
{
    public partial class ComplexForConfig : AXSharp.Connector.IPlain
    {
        public ComplexForConfig()
        {
        }

        public Boolean myBOOL { get; set; }
        public Byte myBYTE { get; set; }
        public UInt16 myWORD { get; set; }
        public UInt32 myDWORD { get; set; }
        public UInt64 myLWORD { get; set; }
        public SByte mySINT { get; set; }
        public Int16 myINT { get; set; }
        public Int32 myDINT { get; set; }
        public Int64 myLINT { get; set; }
        public Byte myUSINT { get; set; }
        public UInt16 myUINT { get; set; }
        public UInt32 myUDINT { get; set; }
        public UInt64 myULINT { get; set; }
        public Single myREAL { get; set; }
        public Double myLREAL { get; set; }
        public TimeSpan myTIME { get; set; } = default(TimeSpan);
        public TimeSpan myLTIME { get; set; } = default(TimeSpan);
        public DateOnly myDATE { get; set; } = new DateOnly(1970, 1, 1);
        public DateOnly myLDATE { get; set; } = new DateOnly(1970, 1, 1);
        public TimeSpan myTIME_OF_DAY { get; set; } = default(TimeSpan);
        public TimeSpan myLTIME_OF_DAY { get; set; } = default(TimeSpan);
        public DateTime myDATE_AND_TIME { get; set; } = new DateTime(1970, 1, 1);
        public DateTime myLDATE_AND_TIME { get; set; } = new DateTime(1970, 1, 1);
        public Char myCHAR { get; set; }
        public Char myWCHAR { get; set; }
        public string mySTRING { get; set; } = string.Empty;
        public string myWSTRING { get; set; } = string.Empty;
        public Motor myMotor { get; set; } = new Motor();
    }

    public partial class Motor : AXSharp.Connector.IPlain
    {
        public Motor()
        {
        }

        public Boolean isRunning { get; set; }
    }

    public partial class Vehicle : AXSharp.Connector.IPlain
    {
        public Vehicle()
        {
        }

        public Motor m { get; set; } = new Motor();
        public Int16 displacement { get; set; }
    }
}