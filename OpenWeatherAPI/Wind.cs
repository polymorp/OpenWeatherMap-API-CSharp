using Newtonsoft.Json.Linq;
using System.Globalization;

namespace OpenWeatherAPI
{
	public class Wind
	{
		public enum DirectionEnum
		{
			North,
			North_North_East,
			North_East,
			East_North_East,
			East,
			East_South_East,
			South_East,
			South_South_East,
			South,
			South_South_West,
			South_West,
			West_South_West,
			West,
			West_North_West,
			North_West,
			North_North_West,
			Unknown
		}

		public enum BeaufortWindEnum
		{
			Calm = 0,
			LightAir = 1,
			LightBreeze = 2,
			GentleBreeze = 3,
			ModerateBreeze = 4,
			FreshBreeze = 5,
			StrongBreeze = 6,
			HighWind = 7,
			Gale = 8,
			StrongSevereGale = 9,
			Storm = 10,
			ViolentStorm = 11,
			HurricaneForce = 12,
			Unknown = 13
		}

		public double SpeedMetersPerSecond { get; }

		public double SpeedFeetPerSecond { get; }

		public DirectionEnum Direction { get; }

		public BeaufortWindEnum WindSpeed { get; }

		public double Degree { get; }

		public double Gust { get; }

		public string DirectionInitials
		{
			get
			{
				return DirectionEnumToInitals(Direction);
			}

		}

		public Wind(JToken windData)
		{
			if (windData is null)
				throw new System.ArgumentNullException(nameof(windData));


			SpeedMetersPerSecond = double.Parse(windData.SelectToken("speed").ToString(), CultureInfo.InvariantCulture);
			SpeedFeetPerSecond = SpeedMetersPerSecond * 3.28084;
			Degree = double.Parse(windData.SelectToken("deg").ToString(), CultureInfo.InvariantCulture);
			Direction = assignDirection(Degree);

			WindSpeed = assignWindspeed(SpeedMetersPerSecond);

			if (windData.SelectToken("gust") != null)
				Gust = double.Parse(windData.SelectToken("gust").ToString(), CultureInfo.InvariantCulture);
		}

		public static string DirectionEnumToString(DirectionEnum dir)
		{
			switch (dir)
			{
				case DirectionEnum.East:
					return "East";
				case DirectionEnum.East_North_East:
					return "East North-East";
				case DirectionEnum.East_South_East:
					return "East South-East";
				case DirectionEnum.North:
					return "North";
				case DirectionEnum.North_East:
					return "North East";
				case DirectionEnum.North_North_East:
					return "North North-East";
				case DirectionEnum.North_North_West:
					return "North North-West";
				case DirectionEnum.North_West:
					return "North West";
				case DirectionEnum.South:
					return "South";
				case DirectionEnum.South_East:
					return "South East";
				case DirectionEnum.South_South_East:
					return "South South-East";
				case DirectionEnum.South_South_West:
					return "South South-West";
				case DirectionEnum.South_West:
					return "South West";
				case DirectionEnum.West:
					return "West";
				case DirectionEnum.West_North_West:
					return "West North-West";
				case DirectionEnum.West_South_West:
					return "West South-West";
				case DirectionEnum.Unknown:
					return "Unknown";
				default:
					return "Unknown";
			}
		}

		public static string DirectionEnumToInitals(DirectionEnum dir)
		{
			switch (dir)
			{
				case DirectionEnum.East:
					return "E";
				case DirectionEnum.East_North_East:
					return "ENE";
				case DirectionEnum.East_South_East:
					return "ESE";
				case DirectionEnum.North:
					return "N";
				case DirectionEnum.North_East:
					return "NE";
				case DirectionEnum.North_North_East:
					return "NNE";
				case DirectionEnum.North_North_West:
					return "NNW";
				case DirectionEnum.North_West:
					return "NW";
				case DirectionEnum.South:
					return "S";
				case DirectionEnum.South_East:
					return "SE";
				case DirectionEnum.South_South_East:
					return "SSE";
				case DirectionEnum.South_South_West:
					return "SSE";
				case DirectionEnum.South_West:
					return "SW";
				case DirectionEnum.West:
					return "W";
				case DirectionEnum.West_North_West:
					return "WNW";
				case DirectionEnum.West_South_West:
					return "WSW";
				case DirectionEnum.Unknown:
					return "Unknown";
				default:
					return "Unknown";
			}
		}

		private DirectionEnum assignDirection(double degree)
		{
			if (fB(degree, 348.75, 360))
				return DirectionEnum.North;
			if (fB(degree, 0, 11.25))
				return DirectionEnum.North;
			if (fB(degree, 11.25, 33.75))
				return DirectionEnum.North_North_East;
			if (fB(degree, 33.75, 56.25))
				return DirectionEnum.North_East;
			if (fB(degree, 56.25, 78.75))
				return DirectionEnum.East_North_East;
			if (fB(degree, 78.75, 101.25))
				return DirectionEnum.East;
			if (fB(degree, 101.25, 123.75))
				return DirectionEnum.East_South_East;
			if (fB(degree, 123.75, 146.25))
				return DirectionEnum.South_East;
			if (fB(degree, 168.75, 191.25))
				return DirectionEnum.South;
			if (fB(degree, 191.25, 213.75))
				return DirectionEnum.South_South_West;
			if (fB(degree, 213.75, 236.25))
				return DirectionEnum.South_West;
			if (fB(degree, 236.25, 258.75))
				return DirectionEnum.West_South_West;
			if (fB(degree, 258.75, 281.25))
				return DirectionEnum.West;
			if (fB(degree, 281.25, 303.75))
				return DirectionEnum.West_North_West;
			if (fB(degree, 303.75, 326.25))
				return DirectionEnum.North_West;
			if (fB(degree, 326.25, 348.75))
				return DirectionEnum.North_North_West;
			return DirectionEnum.Unknown;
		}

		private BeaufortWindEnum assignWindspeed(double WindSpeed)
		{
			if (fB(WindSpeed, 0, 0.5))
				return BeaufortWindEnum.Calm;
			if (fB(WindSpeed, 0.5, 1.5))
				return BeaufortWindEnum.LightAir;
			if (fB(WindSpeed, 1.5, 3.3))
				return BeaufortWindEnum.LightBreeze;
			if (fB(WindSpeed, 3.3, 5.5))
				return BeaufortWindEnum.GentleBreeze;
			if (fB(WindSpeed, 5.5, 7.9))
				return BeaufortWindEnum.ModerateBreeze;
			if (fB(WindSpeed, 7.9, 10.7))
				return BeaufortWindEnum.FreshBreeze;
			if (fB(WindSpeed, 10.7, 13.8))
				return BeaufortWindEnum.StrongBreeze;
			if (fB(WindSpeed, 13.8, 17.1))
				return BeaufortWindEnum.HighWind;
			if (fB(WindSpeed, 17.1, 20.7))
				return BeaufortWindEnum.Gale;
			if (fB(WindSpeed, 20.7, 24.4))
				return BeaufortWindEnum.StrongSevereGale;
			if (fB(WindSpeed, 24.4, 28.4))
				return BeaufortWindEnum.Storm;
			if (fB(WindSpeed, 28.4, 32.6))
				return BeaufortWindEnum.ViolentStorm;
			if (fB(WindSpeed, 32.6, 100))
				return BeaufortWindEnum.HurricaneForce;
			return BeaufortWindEnum.Unknown;
		}

		//fB = fallsBetween
		private static bool fB(double val, double min, double max)
		{
			if ((min <= val) && (val <= max))
				return true;
			return false;
		}
	}
}
