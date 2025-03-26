using System;
using AXSharp.Abstractions.Presentation;
using AXSharp.Connector;

namespace Pocos
{
    namespace MultilinePragmas
    {
        public partial class Extendee2 : AXSharp.Connector.IPlain
        {
            public Extendee2()
            {
            }

            [AXSharp.Connector.AddedPropertiesAttribute("PlcTextList", @"[1]:'<#Messenger 1: message text for message code 1#>':'<#Messenger 1: help text for message code 1#>';
                                    [2]:'<#Messenger 1: message text for message code 2#>':'<#Messenger 1: help text for message code 2#>';
                                    [3]:'<#Messenger 1: message text for message code 3#>':'<#Messenger 1: help text for message code 3#>';
                                    [4]:'<#Messenger 1: message text for message code 4#>':'<#Messenger 1: help text for message code 4#>';
                                    [5]:'<#Messenger 1: message text for message code 5#>':'<#Messenger 1: help text for message code 5#>';
                                    [6]:'<#Messenger 1: message text for message code 6#>':'<#Messenger 1: help text for message code 6#>';
                                    [7]:'<#Messenger 1: message text for message code 7#>':'<#Messenger 1: help text for message code 7#>';
                                    [8]:'<#Messenger 1: message text for message code 8#>':'<#Messenger 1: help text for message code 8#>';
                                    [9]:'<#Messenger 1: message text for message code 9#>':'<#Messenger 1: help text for message code 9#>';
                                    [10]:'<#Messenger 1: message text for message code 10#>':'<#Messenger 1: help text for message code 10#>';
                                    [11]:'<#Messenger 1: message text for message code 11#>':'<#Messenger 1: help text for message code 11#>'")]
            public Int16 _messge { get; set; }
        }
    }
}