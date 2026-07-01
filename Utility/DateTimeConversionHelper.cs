using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility
{

    public class DateTimeConversionHelper
    {
        public static DateTime GetIndiaDateTimeNow()
        {
            //TimeZoneInfo indiaTimeZone = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
            //DateTime indiaTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, indiaTimeZone);
            //return indiaTime;
            string timeZoneId = OperatingSystem.IsWindows() ? "India Standard Time" : "Asia/Kolkata";
            var indiaZone = TimeZoneInfo.FindSystemTimeZoneById(timeZoneId);
            var indiaTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, indiaZone);

            return DateTime.SpecifyKind(indiaTime, DateTimeKind.Unspecified);
        }
    }
}
