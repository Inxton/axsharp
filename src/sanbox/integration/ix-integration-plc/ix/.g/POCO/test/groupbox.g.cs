using System;
using AXSharp.Abstractions.Presentation;
using AXSharp.Connector;

namespace Pocos
{
    [AXSharp.Connector.SourceFileAttribute(@"test/groupbox.st")]
    public partial class groupbox : AXSharp.Connector.IPlain
    {
        public groupbox()
        {
        }

        [AXSharp.Connector.AddedPropertiesAttribute("AttributeName", @"<#Integer From PLC#>")]
        public Int16 testInteger { get; set; }

        [AXSharp.Connector.AddedPropertiesAttribute("AttributeName", @"<#UInteger From PLC#>")]
        public Int16 testUInteger { get; set; }

        [AXSharp.Connector.AddedPropertiesAttribute("AttributeName", @"<#STRING From PLC#>")]
        public string testString { get; set; } = string.Empty;

        [AXSharp.Connector.AddedPropertiesAttribute("AttributeName", @"<#WORD From PLC#>")]
        public UInt16 testWord { get; set; }

        [AXSharp.Connector.AddedPropertiesAttribute("AttributeName", @"<#BYTE From PLC#>")]
        public Byte testByte { get; set; }

        [AXSharp.Connector.AddedPropertiesAttribute("AttributeName", @"<#REAL From PLC#>")]
        public Single testReal { get; set; }

        [AXSharp.Connector.AddedPropertiesAttribute("AttributeName", @"<#LREAL From PLC#>")]
        public Double testLReal { get; set; }

        [AXSharp.Connector.AddedPropertiesAttribute("AttributeName", @"<#BOOL From PLC#>")]
        public Boolean testBool { get; set; }

        [AXSharp.Connector.AddedPropertiesAttribute("AttributeName", @"<#DATE From PLC#>")]
        public DateOnly TestDate { get; set; } = new DateOnly(1970, 1, 1);

        [AXSharp.Connector.AddedPropertiesAttribute("AttributeName", @"<#DATE_AND_TIME From PLC#>")]
        public DateTime TestDateTime { get; set; } = new DateTime(1970, 1, 1);

        [AXSharp.Connector.AddedPropertiesAttribute("AttributeName", @"<#TIME_OF_DAY From PLC#>")]
        public TimeSpan TestTimeOfDay { get; set; } = default(TimeSpan);

        [AXSharp.Connector.AddedPropertiesAttribute("AttributeName", @"<#ENUM Station status#>")]
        [AXSharp.Connector.EnumeratorDiscriminatorAttribute(typeof(global::enumStationStatus))]
        public global::enumStationStatus Status { get; set; }
    }
}