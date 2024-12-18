using System;
using Newtonsoft.Json;

namespace TetrioStats.Api.Domain.Converters;

public class SecondsToTimeSpanConverter
	: JsonConverter
{
	public override bool CanConvert(Type objectType)
	{
		return objectType == typeof(TimeSpan) || objectType == typeof(TimeSpan?);
	}

	public override object ReadJson(
		JsonReader reader, 
		Type objectType, 
		object existingValue,
		JsonSerializer serializer)
	{
		if (reader.TokenType == JsonToken.Null)
		{
			if (objectType == typeof(TimeSpan?))
				return null;

			throw new JsonSerializationException(
				"Cannot convert null value to TimeSpan.");
		}

		if (reader.TokenType is JsonToken.Float or JsonToken.Integer)
		{
			var seconds = Convert.ToDouble(reader.Value);
			return TimeSpan.FromSeconds(seconds);
		}

		throw new JsonSerializationException(
			$"Unexpected token parsing TimeSpan. Expected Float or Integer, got {reader.TokenType}.");
	}

	public override void WriteJson(
		JsonWriter writer,
		object value, 
		JsonSerializer serializer)
	{
		if (value is TimeSpan timeSpan)
		{
			writer.WriteValue(timeSpan.TotalSeconds);
		}
		else
		{
			throw new JsonSerializationException(
				"Expected TimeSpan object value.");
		}
	}
}