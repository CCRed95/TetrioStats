using System;
using System.Globalization;
using Newtonsoft.Json;

namespace TetrioStats.Api.Domain.Converters;

public class Iso8601DateTimeConverter 
	: JsonConverter
{
	public override bool CanConvert(Type objectType)
	{
		return objectType == typeof(DateTime) || objectType == typeof(DateTime?);
	}

	public override object ReadJson(
		JsonReader reader, 
		Type objectType, 
		object existingValue,
		JsonSerializer serializer)
	{
		if (reader.TokenType == JsonToken.Null)
		{
			if (objectType == typeof(DateTime?))
				return null;

			throw new JsonSerializationException(
				"Cannot convert null value to DateTime.");
		}

		if (reader.TokenType == JsonToken.String)
		{
			var dateString = reader.Value?.ToString();

			if (DateTime.TryParse(dateString, null, DateTimeStyles.RoundtripKind, out var dateTime))
			{
				return dateTime;
			}

			throw new JsonSerializationException(
				$"Invalid date format: {dateString}");
		}

		throw new JsonSerializationException(
			$"Unexpected token parsing DateTime. Expected String, got {reader.TokenType}.");
	}

	public override void WriteJson(
		JsonWriter writer,
		object value,
		JsonSerializer serializer)
	{
		if (value is DateTime dateTime)
		{
			writer.WriteValue(dateTime.ToString("o")); // 'o' is the round-trip format specifier
		}
		else
		{
			throw new JsonSerializationException(
				"Expected DateTime object value.");
		}
	}
}