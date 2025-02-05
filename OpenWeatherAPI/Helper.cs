using System;
using System.Runtime.CompilerServices;

namespace OpenWeatherAPI
{
	 	
	internal static class Helper
	{
		internal static DateTime convertUnixToDateTime(double unixTime)
		{
			DateTime dt = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
			return dt.AddSeconds(unixTime).ToLocalTime();
		}
	}
}
