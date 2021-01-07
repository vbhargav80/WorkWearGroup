using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkWearGroup.API
{
    public static class StringExtensions
    {
        public static bool EqualsCaseInsensitive(this string val, string compareTo)
        {
            return string.Equals(val, compareTo, StringComparison.OrdinalIgnoreCase);
        }
    }
}
