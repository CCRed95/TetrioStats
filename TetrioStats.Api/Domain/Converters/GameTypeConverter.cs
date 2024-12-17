using System;
using Newtonsoft.Json;
using TetrioStats.Data.Enums;

namespace TetrioStats.Api.Domain.Converters;

public class GameTypeConverter
		: JsonConverter
{
	public static readonly GameTypeConverter Singleton = new();


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

			_ => throw new("Cannot unmarshal type GameType")
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

			_ => throw new("Cannot marshal type GameType")
		};

		serializer.Serialize(writer, valueStr);

		throw new("Cannot marshal type GameType");
	}
}

///// <summary>
///// If the <see cref="LeagueFlowUserResponse"/> request was successful, contains the requested data structure.
///// </summary>
//public class UserLeagueFlowData
//{
//    /// <summary>
//    /// The timestamp of the oldest record found.
//    /// </summary>
//    [JsonProperty("startTime")]
//    [JsonConverter(typeof(CustomUnixDateTimeConverter))]
//    public DateTime StartTime { get; set; }

//    /// <summary>
//    /// The data points in the LeagueFlow chart.
//    /// </summary>
//    [JsonProperty("points")]
//    public List<UserTRDataPoint> Points { get; set; }
//}

/////// <summary>
/////// Converts a <see cref="UserTRDataPoint"/> to and from a fixed array of 4 elements.
/////// </summary>
////public class UserTRDataPointListConverter
////    : JsonConverter<List<UserTRDataPoint>>
////{
////    public override void WriteJson(
////        JsonWriter writer,
////        List<UserTRDataPoint> value,
////        JsonSerializer serializer)
////    {
////        if (value == null)
////        {
////            serializer.Serialize(writer, null);
////            return;
////        }

////        writer.WriteStartArray();

////        writer.WriteEndArray();
////    }

////    public override List<UserTRDataPoint> ReadJson(
////        JsonReader reader,
////        Type objectType,
////        List<UserTRDataPoint> existingValue,
////        bool hasExistingValue,
////        JsonSerializer serializer)
////    {
////        throw new NotImplementedException();

////    }
////}


///// <summary>
///// Converts a <see cref="UserTRDataPoint"/> to and from a fixed array of 4 elements.
///// </summary>
//public class UserTRDataPointConverter
//    : JsonConverter<UserTRDataPoint>
//{
//    public override void WriteJson(
//        JsonWriter writer,
//        UserTRDataPoint value,
//        JsonSerializer serializer)
//    {
//        if (value == null)
//        {
//            serializer.Serialize(writer, null);
//            return;
//        }

//        writer.WriteStartArray();

//        writer.WriteValue(value.TimeStampOffset);
//        writer.WriteValue((int)value.GameResult);
//        writer.WriteValue(value.UserTrAfterMatch);
//        writer.WriteValue(value.OpponentTrBeforeMatch);

//        writer.WriteEndArray();
//    }
//    //[55597875,2,8214,8214],
//    private static readonly Regex _innerArrayRegex = new(
//            @"^\[\s*(?<time>[\d]+)\s*,\s*(?<result>[\d]+)\s*,\s*(?<userTr>[\d]+)\*,\*(?<opponentTr>[\d]+)\*\]\s*\]");


//    public override UserTRDataPoint ReadJson(
//            JsonReader reader,
//            Type objectType,
//            UserTRDataPoint existingValue,
//            bool hasExistingValue,
//            JsonSerializer serializer)
//    {
//        if (reader.TokenType != JsonToken.StartArray)
//            throw new JsonSerializationException(
//                    $"Unexpected token type {reader.TokenType}. Expected 'StartArray'.");

//        var sb = new StringBuilder();

//        var hasMore = reader.Read();


//        sb.Append('[');

//        while (hasMore)
//        {
//            sb.Append(reader.Value);

//            hasMore = reader.Read();

//            if (reader.TokenType == JsonToken.EndArray)
//                hasMore = false;
//            else
//                sb.Append(',');
//        }

//        sb.Append(reader.Value);
//        sb.Append(']');
//        sb.Append(',');

//        var jsonArrayStr = sb.ToString();

//        var numbers = jsonArrayStr
//            .Trim('[', ']', ' ', '\t', ',')
//            .Split(',')
//            .Select(t => int.Parse(t.Trim()))
//            .ToArray();


//        // Move to the first element inside the array
//        //reader.Read();

//        // Read each of the 4 array elements (without calling Read() between them)
//        var timeStampOffset = numbers[0];
//        var gameResult = numbers[1];
//        var userTrAfterMatch = numbers[2];
//        var opponentTrBeforeMatch = numbers[3];

//        // After reading all elements, move  the reader to the next token
//        //reader.Read(); // To move past the EndArray

//        return new()
//        {
//            TimeStampOffset = timeStampOffset,
//            GameResult = (LeagueFlowMatchResult)gameResult,
//            UserTrAfterMatch = userTrAfterMatch,
//            OpponentTrBeforeMatch = opponentTrBeforeMatch
//        };
//    }
//}