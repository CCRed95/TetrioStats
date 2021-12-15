using System;
using Newtonsoft.Json;

namespace TetrioStats.Api.Domain.Json.Converters
{
	public class UnixTimeStampConverter
		: JsonConverter
	{
		public override bool CanConvert(Type t)
		{
			return t == typeof(TimeSpan) 
				|| t == typeof(TimeSpan?);
		}

		public override object ReadJson(
			JsonReader reader, 
			Type t, 
			object existingValue,
			JsonSerializer serializer)
		{
			if (reader.TokenType == JsonToken.Null)
				return null;

			var value = serializer.Deserialize<double>(reader);
			var timeSpan = TimeSpan.FromSeconds(value);

			return timeSpan;
		}

		public override void WriteJson(
			JsonWriter writer,
			object untypedValue,
			JsonSerializer serializer)
		{
			switch (untypedValue)
			{
				case TimeSpan timeSpan:
				{
					serializer.Serialize(writer, timeSpan.TotalSeconds);
					break;
				}
				case null:
				{
					serializer.Serialize(writer, null);
					return;
				}
			}
			throw new Exception("Cannot marshal type TimeSpan");
		}

		public static readonly UnixTimeStampConverter Instance 
			= new UnixTimeStampConverter();
	}
}