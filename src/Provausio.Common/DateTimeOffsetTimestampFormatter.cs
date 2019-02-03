using System;
using Provausio.Common.Ext;

namespace Provausio.Common
{
    internal class DateTimeOffsetTimestampFormatter : IObjectStringFormatter
    {
        public string ToString(object input)
        {
            var dt = (DateTimeOffset) input;
            return dt.ToUnixTimeMilliseconds().ToString();
        }
    }
}