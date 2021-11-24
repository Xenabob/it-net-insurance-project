using System;
namespace InsuranceCompany.Utils
{
    public static class DateHelpers
    {
        public static int ToUnixTimestamp(DateTime target)
        {
            var date = new DateTime(1970, 1, 1, 0, 0, 0, target.Kind);
            var unixTimestamp = System.Convert.ToInt64((target - date).TotalSeconds);
            return (int)unixTimestamp;
        }

        public static DateTime UnixTimestampToDateTime(int timestamp)
        {
            DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dateTime = dateTime.AddSeconds(timestamp).ToLocalTime();
            return dateTime;
        }

    }

}
