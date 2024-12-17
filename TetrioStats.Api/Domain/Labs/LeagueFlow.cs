using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using TetrioStats.Api.Domain.Converters;

namespace TetrioStats.Api.Domain.Labs;

/// <summary>
/// Converts a <see cref="LeagueFlowPoint"/> to and from a fixed array of 4 elements.
/// </summary>
public class LeagueFlowPointConverter
	: JsonConverter<LeagueFlowPoint>
{
	public override void WriteJson(
		JsonWriter writer,
		LeagueFlowPoint value,
		JsonSerializer serializer)
	{
		if (value == null)
		{
			serializer.Serialize(writer, null);
			return;
		}

		writer.WriteStartArray();

		writer.WriteValue(value.TimeStampOffset);
		writer.WriteValue((int)value.GameResult);
		writer.WriteValue(value.UserTrAfterMatch);
		writer.WriteValue(value.OpponentTrBeforeMatch);

		writer.WriteEndArray();
	}

	//[55597875,2,8214,8214],
	private static readonly Regex _innerArrayRegex = new(
		@"^\[\s*(?<time>[\d]+)\s*,\s*(?<result>[\d]+)\s*,\s*(?<userTr>[\d]+)\*,\*(?<opponentTr>[\d]+)\*\]\s*\]");


	public override LeagueFlowPoint ReadJson(
		JsonReader reader,
		Type objectType,
		LeagueFlowPoint existingValue,
		bool hasExistingValue,
		JsonSerializer serializer)
	{
		if (reader.TokenType != JsonToken.StartArray)
			throw new JsonSerializationException(
				$"Unexpected token type {reader.TokenType}. Expected 'StartArray'.");

		var sb = new StringBuilder();

		var hasMore = reader.Read();


		sb.Append('[');

		while (hasMore)
		{
			sb.Append(reader.Value);

			hasMore = reader.Read();

			if (reader.TokenType == JsonToken.EndArray)
				hasMore = false;
			else
				sb.Append(',');
		}

		sb.Append(reader.Value);
		sb.Append(']');
		sb.Append(',');

		var jsonArrayStr = sb.ToString();

		var numbers = jsonArrayStr
			.Trim('[', ']', ' ', '\t', ',')
			.Split(',')
			.Select(t => int.Parse(t.Trim()))
			.ToArray();


		// Move to the first element inside the array
		//reader.Read();

		// Read each of the 4 array elements (without calling Read() between them)
		var timeStampOffset = numbers[0];
		var gameResult = numbers[1];
		var userTrAfterMatch = numbers[2];
		var opponentTrBeforeMatch = numbers[3];

		// After reading all elements, move  the reader to the next token
		//reader.Read(); // To move past the EndArray

		return new()
		{
			TimeStampOffset = timeStampOffset,
			GameResult = (LeagueFlowMatchResult)gameResult,
			UserTrAfterMatch = userTrAfterMatch,
			OpponentTrBeforeMatch = opponentTrBeforeMatch
		};
	}
}

//var startToken = reader.Read();

//var timeSpanOffsetEpoch = reader.ReadAsInt32();
//var gameResultInt = reader.ReadAsInt32();
//var userTrAfterMatch = reader.ReadAsInt32();
//var opponentTrBeforeMatch = reader.ReadAsInt32();


/// <summary>
/// Represents one data point in the LeagueFlow chart.
/// </summary>
[DebuggerDisplay("DebuggerString()")]
[JsonConverter(typeof(LeagueFlowPointConverter))]
public class LeagueFlowPoint
{
	/// <summary>
	/// The timestamp offset. Add <see cref="UserLeagueFlowData.StartTime"/> to get the true timestamp.
	/// </summary>
	[JsonProperty("timestampOffset")]
	public int TimeStampOffset { get; set; }

	/// <summary>
	/// The result of the match
	/// </summary>
	[JsonProperty("result")]
	[JsonConverter(typeof(LeagueFlowMatchResultConverter))]
	public LeagueFlowMatchResult GameResult { get; set; }

	/// <summary>
	/// The user's TR after the match.
	/// </summary>
	[JsonProperty("userTR")]
	public int UserTrAfterMatch { get; set; }

	/// <summary>
	/// The opponent's TR before the match. (If the opponent was unranked, same as <see cref="UserTrAfterMatch"/>)
	/// </summary>
	[JsonProperty("opponentTR")]
	public int OpponentTrBeforeMatch { get; set; }


	public string DebuggerString()
	{
		return $"[{TimeStampOffset},{GameResult:D},{UserTrAfterMatch},{OpponentTrBeforeMatch}],";
	}
}


public enum LeagueFlowMatchResult
{
	[Description("unknown")]
	Unknown = 0,

	[Description("victory")]
	Victory = 1,

	[Description("defeat")]
	Defeat = 2,

	[Description("victory by disqualification")]
	VictoryByDisqualification = 3,

	[Description("defeat by disqualification")]
	DefeatByDisqualification = 4,

	[Description("tie")]
	Tie = 5,

	[Description("no contest")]
	NoContest = 6,

	[Description("match nullified")]
	MatchNullified = 7
}


/// <summary>
/// If the <see cref="LeagueFlowResponse"/> request was successful, contains the requested data structure.
/// </summary>
public class LeagueFlow
{
	/// <summary>
	/// The timestamp of the oldest record found.
	/// </summary>
	[JsonProperty("startTime")]
	[JsonConverter(typeof(CustomUnixDateTimeConverter))]
	public DateTime StartTime { get; set; }

	/// <summary>
	/// The data points in the LeagueFlow chart.
	/// </summary>
	[JsonProperty("points")]
	public List<LeagueFlowPoint> Points { get; set; }
}


/// <summary>
/// Represents a packet containing League flow data.
/// </summary>
public class LeagueFlowResponse : TetrioApiResponse<LeagueFlow>;