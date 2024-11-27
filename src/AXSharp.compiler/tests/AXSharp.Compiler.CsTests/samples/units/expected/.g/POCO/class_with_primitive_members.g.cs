using System;
using AXSharp.Abstractions.Presentation;
using AXSharp.Connector;

namespace ClassWithPrimitiveTypesNamespace
{
    namespace Pocos
    {
        public partial class ClassWithPrimitiveTypes : AXSharp.Connector.IPlain
        {
            public ClassWithPrimitiveTypes()
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
            public DateOnly myDATE { get; set; } = default(DateOnly);
            public DateOnly myLDATE { get; set; } = default(DateOnly);
            public TimeSpan myTIME_OF_DAY { get; set; } = default(TimeSpan);
            public TimeSpan myLTIME_OF_DAY { get; set; } = default(TimeSpan);
            public DateTime myDATE_AND_TIME { get; set; } = default(DateTime);
            public DateTime myLDATE_AND_TIME { get; set; } = default(DateTime);
            public Char myCHAR { get; set; }

            public Char myWCHAR { get; set; }

            public string mySTRING { get; set; } = string.Empty;
            public string myWSTRING { get; set; } = string.Empty;
        }
    }
}