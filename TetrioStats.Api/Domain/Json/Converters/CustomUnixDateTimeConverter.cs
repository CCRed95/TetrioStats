using System;
using Ccr.Std.Core.Extensions;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace TetrioStats.Api.Domain.Json.Converters
{
	/// <summary>w
	/// Converts a <see cref="DateTime"/> to and from Unix epoch time
	/// </summary>
	public class CustomUnixDateTimeConverter : DateTimeConverterBase
	{
		internal static readonly DateTime UnixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

		/// <summary>
		/// Writes the JSON representation of the object.
		/// </summary>
		/// <param name="writer">The <see cref="JsonWriter"/> to write to.</param>
		/// <param name="value">The value.</param>
		/// <param name="serializer">The calling serializer.</param>
		public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
		{
			long seconds;

			if (value is DateTime dateTime)
			{
				seconds = (long) (dateTime.ToUniversalTime() - UnixEpoch).TotalSeconds;
			}
			#if HAVE_DATE_TIME_OFFSET
      else if (value is DateTimeOffset dateTimeOffset)
      {
        seconds = (long)(dateTimeOffset.ToUniv+ersalTime() - UnixEpoch).TotalSeconds;
      }
			#endif
			else
			{
				throw new JsonSerializationException("Expected date object value.");
			}

			if (seconds < 0)
			{
				throw new JsonSerializationException(
					"Cannot convert date value that is before Unix epoch of 00:00:00 UTC on 1 January 1970.");
			}

			writer.WriteValue(seconds);
		}

		/// <summary>
		/// Reads the JSON representation of the object.
		/// </summary>
		/// <param name="reader">The <see cref="JsonReader"/> to read from.</param>
		/// <param name="objectType">Type of the object.</param>
		/// <param name="existingValue">The existing property value of the JSON that is being converted.</param>
		/// <param name="serializer">The calling serializer.</param>
		/// <returns>The object value.</returns>
		public override object? ReadJson(JsonReader reader,
			Type objectType,
			object? existingValue,
			JsonSerializer serializer)
		{

			bool nullable = objectType.IsGenericOf(typeof(Nullable<>));

			if (reader.TokenType == JsonToken.Null)
			{
				if (!nullable)
				{
					throw new NotSupportedException();
					//throw JsonSerializationException.Create(reader, "Cannot convert null value to {0}.".FormatWith(CultureInfo.InvariantCulture, objectType));
				}

				return null;
			}

			long seconds;

			if (reader.TokenType == JsonToken.Integer)
			{
				seconds = (long) reader.Value!;
			}
			else if (reader.TokenType == JsonToken.String)
			{
				if (!long.TryParse((string) reader.Value!, out seconds))
				{
					throw new NotSupportedException();
					//throw JsonSerializationException.Create(reader, "Cannot convert invalid value to {0}.".FormatWith(CultureInfo.InvariantCulture, objectType));
				}
			}
			else
			{
				throw new NotSupportedException();
				//throw JsonSerializationException.Create(reader, "Unexpected token parsing date. Expected Integer or String, got {0}.".FormatWith(CultureInfo.InvariantCulture, reader.TokenType));
			}

			if (seconds >= 0)
			{
				var secondsStr = seconds.ToString();
				if (secondsStr.Length > 10)
					seconds = int.Parse(secondsStr.Substring(0, 10));

				DateTime d = UnixEpoch.AddSeconds(seconds);

				#if HAVE_DATE_TIME_OFFSET
	      Type t = (nullable)
	          ? Nullable.GetUnderlyingType(objectType)
	          : objectType;
	      if (t == typeof(DateTimeOffset))
	      {
	          return new DateTimeOffset(d, TimeSpan.Zero);
	      }
				#endif
				return d;
			}
			else
			{
				throw new NotSupportedException();
				//throw JsonSerializationException.Create(reader, "Cannot convert value that is before Unix epoch of 00:00:00 UTC on 1 January 1970 to {0}.".FormatWith(CultureInfo.InvariantCulture, objectType));
			}
		}
	}
}