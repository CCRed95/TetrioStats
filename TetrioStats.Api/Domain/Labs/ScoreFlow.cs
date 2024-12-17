using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using TetrioStats.Api.Domain.Converters;

namespace TetrioStats.Api.Domain.Labs;

/// <summary>
/// Represents a single data point in the Score flow.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="ScoreFlowPoint"/> class.
/// </remarks>
public class ScoreFlowPoint
{
	/// <summary>
	/// The timestamp offset. Add startTime to get the true timestamp.
	/// </summary>
	[JsonProperty("value1")]
	[JsonConverter(typeof(UnixTimeStampConverter))]
	public DateTime TimeStamp { get; set; }

	/// <summary>
	/// Whether the score set was a PB.
	/// </summary>
	[JsonProperty("value2")]
	public bool WasPersonalBest { get; set; }

	/// <summary>
	/// The score achieved. (For 40 LINES, this is negative.)
	/// </summary>
	[JsonProperty("value3")]
	public int Score { get; set; }
}


/// <summary>
/// Represents the Score flow data.
/// </summary>
public class ScoreFlow
{
	/// <summary>
	/// The start time of the Score flow.
	/// </summary>
	[JsonProperty("startTime")]
	[JsonConverter(typeof(UnixTimeStampConverter))]
	public DateTime StartTime { get; set; }

	/// <summary>
	/// The collection of data points in the Score flow.
	/// </summary>
	[JsonProperty("points")]
	public List<ScoreFlowPoint> Points { get; set; }
}


/// <summary>
/// Represents a packet containing Score flow data.
/// </summary>
public class ScoreFlowResponse : TetrioApiResponse<ScoreFlow>;