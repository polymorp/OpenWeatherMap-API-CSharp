using System;
using Newtonsoft.Json;
using System.Diagnostics;
using Newtonsoft.Json.Linq;
using Xunit;

namespace OpenWeatherAPI.Tests
{
	/// <summary>
	/// Tests <see cref="OpenWeatherApiClient"/>
	/// </summary>
	public class APITests
	{
		[Fact()]
		
		public void QueryTest_Success()
		{
			//Arrange
			var api = new OpenWeatherApiClient("YOURAPIKEYHERE"); //No good solution here to have safe and valid OpenWeather API keys in a test

			//Act
			var actual = api.Query("London, GB");

			//Assert
			Assert.True(actual.ValidRequest);

			Trace.WriteLine(JsonConvert.SerializeObject(actual, Formatting.Indented));
		}

		[Fact()]
		public void QueryTest_UpdateKeySuccess()
		{
			//Arrange
			var api = new API(""); //No good solution here to have safe and valid OpenWeather API keys in a test
			api.UpdateAPIKey("YOURAPIKEYHERE");

			//Act
			var actual = api.Query("London, GB");

			//Assert
			Assert.True(actual.ValidRequest);

			Trace.WriteLine(JsonConvert.SerializeObject(actual, Formatting.Indented));
		}

		[Fact()]
		public void QueryTest_Fail()
		{
			//Arrange
			var api = new API("aaaa"); //No good solution here to have safe and valid OpenWeather API keys in a test
	
			//Act
			var actual = api.Query("London, GB");

			//Assert
			Assert.Null(actual);

			Trace.WriteLine(JsonConvert.SerializeObject(actual, Formatting.Indented));
		}


		[Theory]
		[InlineData(0.1, Wind.BeaufortWindEnum.Calm)]
		[InlineData(1.5, Wind.BeaufortWindEnum.LightAir)]
		[InlineData(3.3, Wind.BeaufortWindEnum.LightBreeze)]
		[InlineData(5.5, Wind.BeaufortWindEnum.GentleBreeze)]
		[InlineData(7.0, Wind.BeaufortWindEnum.ModerateBreeze)]
		[InlineData(10.7, Wind.BeaufortWindEnum.FreshBreeze)]
		[InlineData(13.8, Wind.BeaufortWindEnum.StrongBreeze)]
		[InlineData(17.1, Wind.BeaufortWindEnum.HighWind)]
		[InlineData(20.7, Wind.BeaufortWindEnum.Gale)]
		[InlineData(24.4, Wind.BeaufortWindEnum.StrongSevereGale)]
		[InlineData(28.4, Wind.BeaufortWindEnum.Storm)]
		[InlineData(32.6, Wind.BeaufortWindEnum.ViolentStorm)]
		[InlineData(32.7, Wind.BeaufortWindEnum.HurricaneForce)]
		[InlineData(-1, Wind.BeaufortWindEnum.Unknown)]
		[InlineData(101, Wind.BeaufortWindEnum.Unknown)]

		public void testwind(double speed, Wind.BeaufortWindEnum expected)
		{
			// arrange

			JToken windToken = JToken.Parse($"{{\r\n  \"speed\": {speed},\r\n  \"deg\": 297,\r\n  \"gust\": 8.2\r\n}}");
			
			// act
			var wind0 = new Wind(windToken);

			// assert
			Assert.True(wind0.WindSpeed == expected);

		}


		
		[Fact()]
		public void testvarious()
		{
			// arrage
			var json =
				"{\r\n  \"coord\": {\r\n    \"lon\": -1.2,\r\n    \"lat\": 53.0333\r\n  },\r\n  \"weather\": [\r\n    {\r\n      \"id\": 804,\r\n      \"main\": \"Clouds\",\r\n      \"description\": \"overcast clouds\",\r\n      \"icon\": \"04d\"\r\n    }\r\n  ],\r\n  \"base\": \"stations\",\r\n  \"main\": {\r\n    \"temp\": 282.99,\r\n    \"feels_like\": 280.78,\r\n    \"temp_min\": 282.59,\r\n    \"temp_max\": 283.71,\r\n    \"pressure\": 1026,\r\n    \"humidity\": 84\r\n  },\r\n  \"visibility\": 10000,\r\n  \"wind\": {\r\n    \"speed\": 2.24,\r\n    \"deg\": 210,\r\n    \"gust\": 4.92\r\n  },\r\n  \"clouds\": {\r\n    \"all\": 98\r\n  },\r\n  \"dt\": 1616260200,\r\n  \"sys\": {\r\n    \"type\": 3,\r\n    \"id\": 2012283,\r\n    \"country\": \"GB\",\r\n    \"sunrise\": 1616220427,\r\n    \"sunset\": 1616264240\r\n  },\r\n  \"timezone\": 0,\r\n  \"id\": 2646460,\r\n  \"name\": \"Hucknall Torkard\",\r\n  \"cod\": 200\r\n}";
			var jsonData = JObject.Parse(json);

			//act
			var wind = new Wind(jsonData.SelectToken("wind"));


			//assert
			Assert.True(wind.WindSpeed == Wind.BeaufortWindEnum.LightBreeze);
			Assert.True(wind.DirectionInitials == "SSE");

		}


		[Theory]
		[InlineData(1616950829, "Sunday, 28 March 2021 17:00:29")]
		[InlineData(946688461, "2000/01/01 01:01:01")]
		public void TestUnixToDateTime(double epoch,DateTime dt)
		{
			// arrange

			// act
			var dtConvertUnixToDateTime = Helper.convertUnixToDateTime(epoch);

			// assert
			Assert.True(dtConvertUnixToDateTime == dt);

		}
	}


}
