using System;
using System.Configuration;
using System.Globalization;

namespace NasUtilities.Utils
{
    public static class DateTimeExtensions
    {          
        public static DateTime StartOfWeek(this DateTime dt, DayOfWeek startOfWeek)
        {
            int diff = dt.DayOfWeek - startOfWeek;
            if (diff < 0)
            {
                diff += 7;
            }
            return dt.AddDays(-1 * diff).Date;
        }

        public static string GetShortestDayName(this DateTime dateTime)
        {
            string cultureinfo = ConfigurationManager.AppSettings["DefaultCulture"];
            CultureInfo ci = new CultureInfo("en-US");
            DateTimeFormatInfo dateTimeInfo = ci.DateTimeFormat;
            return dateTimeInfo.GetShortestDayName(dateTime.DayOfWeek);
        }
    }
}
