using System;

namespace BBL.Helpers
{
	public static class DateTimeHelper
	{
		public static string ConvertToDateTimeOffsetToStringDate(this long unixMilliseconds)
		{
			var dateTime = unixMilliseconds.ConvertToDateTimeOffset();
			return dateTime.ToString("MM/dd/yy hh:mm:ss");
		}

		public static DateTimeOffset ConvertToDateTimeOffset(this long unixMilliseconds)
		{
			System.DateTimeOffset dtDateTime = new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero);
			dtDateTime = dtDateTime.AddMilliseconds(unixMilliseconds).ToUniversalTime();

			return dtDateTime;
		}
	}
}
