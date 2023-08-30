using System;

namespace OpenWeatherAPI
{
	public class TemperatureObj
	{
		public double CelsiusCurrent { get; }
		public double FahrenheitCurrent { get; }
		public double KelvinCurrent { get; }
		public double CelsiusMinimum { get; }
		public double CelsiusMaximum { get; }
		public double FahrenheitMinimum { get; }
		public double FahrenheitMaximum { get; }
		public double KelvinMinimum { get; }
		public double KelvinMaximum { get; }

		public double CelsiusFeelslike { get; }
		public double FahrenheitFeelslike { get; }
		public double KelvinFeelslike { get; }

		public TemperatureObj(double temp, double min, double max, double feelslike)
		{
			KelvinCurrent = temp;
			KelvinMaximum = max;
			KelvinMinimum = min;
			KelvinFeelslike = feelslike;

			CelsiusCurrent = ConvertToCelsius(KelvinCurrent);
			CelsiusMaximum = ConvertToCelsius(KelvinMaximum);
			CelsiusMinimum = ConvertToCelsius(KelvinMinimum);

			FahrenheitCurrent = ConvertToFahrenheit(CelsiusCurrent);
			FahrenheitMaximum = ConvertToFahrenheit(CelsiusMaximum);
			FahrenheitMinimum = ConvertToFahrenheit(CelsiusMinimum);
		}

		private static double ConvertToFahrenheit(double celsius)
		{
			return Math.Round(((9.0 / 5.0) * celsius) + 32, 3);
		}

		private static double ConvertToCelsius(double kelvin)
		{
			return Math.Round(kelvin - 273.15, 3);
		}
	}
}
