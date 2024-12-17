using System.Collections.Generic;
using System.Text.Json;
using Newtonsoft.Json;

namespace TetrioStats.Api.Domain.News;

public enum TypeEnum
{
	/// <summary>
	/// Personal Best News
	/// </summary>
    PersonalBest,

	/// <summary>
	/// Rank Up News
	/// </summary>
    RankUp
};

/// <summary>
/// Represents a single news item.
/// </summary>
public class LatestNews
{
	/// <summary>
	/// The unique identifier of the news item.
	/// </summary>
	[JsonProperty("_id")]
	public string Id { get; set; }

	/// <summary>
	/// The stream associated with the news item.
	/// </summary>
	[JsonProperty("stream")]
	public string Stream { get; set; }

	/// <summary>
	/// The type of the news item.
	/// </summary>
	[JsonProperty("type")]
	public string ItemType { get; set; }

    /// <summary>
    /// The timestamp of the news item.
    /// </summary>
    [JsonProperty("ts")]
    public string Timestamp { get; set; }

    /// <summary>
    /// The data payload of the news item.
    /// </summary>
    [JsonProperty("data")]
	public JsonElement Data { get; set; }

}


/// <summary>
/// Represents the data packet containing news items.
/// </summary>
public class LatestNewsResponseData
{
	/// <summary>
	/// The list of news items.
	/// </summary>
	[JsonProperty("news")]
	public List<LatestNews> News { get; set; }
}


/// <summary>
/// Represents a packet containing the latest news data.
/// </summary>
public class LatestNewsResponse : TetrioApiResponse<LatestNewsResponseData>;