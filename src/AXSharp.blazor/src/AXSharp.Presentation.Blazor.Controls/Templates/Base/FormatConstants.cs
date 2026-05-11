using System;
using System.Collections.Generic;
using System.Text;

namespace AXSharp.Presentation.Blazor.Controls.Templates.Base
{
    public static class FormatConstants
    {
        public static readonly string[] TimeFormats =
            [
                // MOST SPECIFIC -> LEAST SPECIFIC

                // days
                @"d\.h\:m\:s\.fffffff",
                @"d\.h\:m\:s\.ffffff",
                @"d\.h\:m\:s\.fffff",
                @"d\.h\:m\:s\.ffff",
                @"d\.h\:m\:s\.fff",
                @"d\.h\:m\:s\.ff",
                @"d\.h\:m\:s\.f",
                @"d\.h\:m\:s\.",

                @"d\.h\:m\:s",

                // hours
                @"h\:m\:s\.fffffff",
                @"h\:m\:s\.ffffff",
                @"h\:m\:s\.fffff",
                @"h\:m\:s\.ffff",
                @"h\:m\:s\.fff",
                @"h\:m\:s\.ff",
                @"h\:m\:s\.f",
                @"h\:m\:s\.",

                @"h\:m\:s",

                // minutes
                @"m\:s\.fffffff",
                @"m\:s\.ffffff",
                @"m\:s\.fffff",
                @"m\:s\.ffff",
                @"m\:s\.fff",
                @"m\:s\.ff",
                @"m\:s\.f",
                @"m\:s\.",

                @"m\:s",

                // seconds

                @"s\.fffffff",
                @"s\.ffffff",
                @"s\.fffff",
                @"s\.ffff",
                @"s\.fff",
                @"s\.ff",
                @"s\.f",
                @"s\.",

                @"s",

                // fractions

                @"\.fffffff",
                @"\.ffffff",
                @"\.fffff",
                @"\.ffff",
                @"\.fff",
                @"\.ff",
                @"\.f",

                @"c"
            ];
    }
}
