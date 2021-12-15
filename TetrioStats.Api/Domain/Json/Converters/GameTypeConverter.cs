using System;
using Newtonsoft.Json;
using TetrioStats.Data.Enums;

namespace TetrioStats.Api.Domain.Json.Converters
{
	internal class GameTypeConverter 
		: JsonConverter
	{
		public override bool CanConvert(Type type)
		{
			return type == typeof(GameType) || type == typeof(GameType?);
		}


		public override object ReadJson(
			JsonReader reader,
			Type type, 
			object existingValue, 
			JsonSerializer serializer)
		{
			if (reader.TokenType == JsonToken.Null) 
				return null;

			var value = serializer.Deserialize<string>(reader);

			return value?.ToLower() switch
			{
				"40l" => GameType.Single40Lines,
				"zen" => GameType.Zen,
				"blitz" => GameType.Blitz,

				_ => throw new Exception("Cannot unmarshal type GameType")
			};
		}

		public override void WriteJson(
			JsonWriter writer, 
			object untypedValue, 
			JsonSerializer serializer)
		{
			if (untypedValue == null)
			{
				serializer.Serialize(writer, null);
				return;
			}

			var value = (GameType)untypedValue;

			var valueStr = value switch
			{
				GameType.Single40Lines => "40l",
				GameType.Zen => "Zen",
				GameType.Blitz => "Blitz",

				_ => throw new Exception("Cannot marshal type GameType")
			};

			serializer.Serialize(writer, valueStr);

			throw new Exception("Cannot marshal type GameType");
		}

		public static readonly GameTypeConverter Singleton = new GameTypeConverter();
	}
}