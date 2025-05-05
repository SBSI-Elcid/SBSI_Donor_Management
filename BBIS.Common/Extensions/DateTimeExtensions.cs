namespace BBIS.Common.Extensions
{
    public static class DateTimeExtensions
    {
        public static DateTime GetPHDateTimeFromUtc(this DateTime utcTime)
        {
            TimeZoneInfo timeInfo = TimeZoneInfo.FindSystemTimeZoneById("Singapore Standard Time");
            DateTime userTime = TimeZoneInfo.ConvertTimeFromUtc(utcTime, timeInfo);
            return userTime;
        }

        /// <summary>
        /// Since we are saving the utc date time, this is to make sure that we are also converting the date filter from local time to universal time and that its time 
        /// is at the start of the day to make sure that the correct date is retrieved. For example, a data created at 2023-02-25 16:00:00.000000 UTC SHOULD BE RETURNED 
        /// if the value of the DATE FROM filter selected in the UI is 26/02/2023 since the UTC date time if converted to Philippine Standard Time will be 2023-02-26 00:00:00.000000 PHT.
        /// </summary>
        /// <param name="dateFrom"></param>
        /// <returns></returns>
        public static DateTime? ConvertFilterToUtcStartOfDay(this DateTime? dateFrom)
        {
            return dateFrom == null ? dateFrom : dateFrom.Value.Date.ToUniversalTime();
        }

        /// <summary>
        /// Since we are saving the utc date time, this is to make sure that we are also converting the date filter from local time to universal time and that its time 
        /// is at the end of the day to make sure that the correct date is retrieved. For example, a data created at 2023-02-26 16:00:00.000000 UTC SHOULD NOT BE RETURNED 
        /// if the value of the DATE TO filter selected in the UI is 26/02/2023 since the UTC date time if converted to Philippine Standard Time will be 2023-02-27 00:00:00.000000 PHT.
        /// </summary>
        /// <param name="dateTo"></param>
        /// <returns></returns>
        public static DateTime? ConvertFilterToUtcEndOfDay(this DateTime? dateTo)
        {
            return dateTo == null ? dateTo : dateTo.Value.Date.ToUniversalTime().AddDays(1).AddTicks(-1);
        }
    }
}
