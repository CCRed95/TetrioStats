using System;
using Newtonsoft.Json;

namespace TetrioStats.Api.Domain.Converters
{
	public class MillisecondsToTimeStampConverter
		: JsonConverter
	{
		public static readonly MillisecondsToTimeStampConverter Instance = new();


		public override bool CanConvert(Type type)
		{
			return type == typeof(TimeSpan) || type == typeof(TimeSpan?);
		}

		public override object ReadJson(
			JsonReader reader,
			Type type,
			object existingValue,
			JsonSerializer serializer)
		{
			if (reader.TokenType == JsonToken.Null)
				return null;

			var value = serializer.Deserialize<double>(reader);
			var timeSpan = TimeSpan.FromMilliseconds(value);

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
					serializer.Serialize(writer, timeSpan.TotalMilliseconds);
					break;
				}
				case null:
				{
					serializer.Serialize(writer, null);
					return;
				}
			}
			throw new("Cannot marshal type TimeSpan");
		}
	}
}