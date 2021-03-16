using Newtonsoft.Json;
using System.Diagnostics;
using Newtonsoft.Json.Linq;
using Xunit;

namespace OpenWeatherAPI.Tests
{
	/// <summary>
	/// Tests <see cref="API"/>
	/// </summary>
	public class APITests
	{
		[Fact()]
		public void QueryTest_Success()
		{
			//Arrange
			var api = new API("YOURAPIKEYHERE"); //No good solution here to have safe and valid OpenWeather API keys in a test

			//Act
			var actual = api.Query("London, GB");

			//Assert
			Assert.True(actual.ValidRequest);

			Trace.WriteLine(JsonConvert.SerializeObject(actual, Formatting.Indented));
		}

		[Fact()]
		public void testwindCalm()
		{
			// arrange

			JToken windToken = JToken.Parse("{\r\n  \"speed\": 0,\r\n  \"deg\": 297,\r\n  \"gust\": 8.2\r\n}");
			
			// act
			var wind0 = new Wind(windToken);

			// assert
			Assert.True(wind0.WindSpeed == Wind.BeaufortWindEnum.Calm);

		}

		[Fact()]
		public void testwindLightAir()
		{
			// arrange

			JToken windToken = JToken.Parse("{\r\n  \"speed\": 1.5,\r\n  \"deg\": 297,\r\n  \"gust\": 8.2\r\n}");

			// act
			var wind = new Wind(windToken);

			// assert
			Assert.True(wind.WindSpeed == Wind.BeaufortWindEnum.LightAir) ;

		}
		[Fact()]
		public void testwindLightbreeze()
		{
			// arrange

			JToken windToken = JToken.Parse("{\r\n  \"speed\": 2.5,\r\n  \"deg\": 297,\r\n  \"gust\": 8.2\r\n}");

			// act
			var wind = new Wind(windToken);

			// assert
			Assert.True(wind.WindSpeed == Wind.BeaufortWindEnum.LightBreeze);

		}

		[Fact()]
		public void testwindHurrican()
		{
			// arrange

			JToken windToken = JToken.Parse("{\r\n  \"speed\": 33,\r\n  \"deg\": 297,\r\n  \"gust\": 8.2\r\n}");

			// act
			var wind = new Wind(windToken);

			// assert
			Assert.True(wind.WindSpeed == Wind.BeaufortWindEnum.HurricaneForce);

		}

		[Fact()]
		public void testwindError()
		{
			// arrange

			JToken windToken = JToken.Parse("{\r\n  \"speed\": -1,\r\n  \"deg\": 297,\r\n  \"gust\": 8.2\r\n}");

			// act
			var wind = new Wind(windToken);

			// assert
			Assert.True(wind.WindSpeed == Wind.BeaufortWindEnum.Unknown);

		}

		[Fact()]
		public void testwindErrorAbove()
		{
			// arrange

			JToken windToken = JToken.Parse("{\r\n  \"speed\": 101,\r\n  \"deg\": 297,\r\n  \"gust\": 8.2\r\n}");

			// act
			var wind = new Wind(windToken);

			// assert
			Assert.True(wind.WindSpeed == Wind.BeaufortWindEnum.Unknown);

		}
	}
}
