using System;
using JetBrains.Annotations;
using Newtonsoft.Json;
using TetrioStats.Api.Domain.Converters;

namespace TetrioStats.Api.Domain;

public enum CacheStatus
{
	Hit,
	Miss,
	Awaited
}

public class CacheInfo
{
	[JsonProperty("status")]
	public CacheStatus Status { get; set; }

	[JsonConverter(typeof(CustomUnixDateTimeConverter))]
	[JsonProperty("cached_at")]
	public DateTime CachedAt { get; set; }

	[JsonConverter(typeof(CustomUnixDateTimeConverter))]
	[JsonProperty("cached_until")]
	public DateTime CachedUntil { get; set; }
}


public interface ITetrioApiResponse
{
	public bool WasSuccessful { get; set; }

	public CacheInfo CacheInfo { get; set; }
}


public interface ITetrioApiResponse<out TContent>
	: ITetrioApiResponse
{
	public TContent Data { get; }
}


public class TetrioApiResponseWithError<TContent>
	: TetrioApiResponse<TContent>
{
	/// <summary>
	/// If unsuccessful, the reason the request failed.
	/// </summary>
	[JsonProperty("success")]
	[CanBeNull]
	public object ErrorReason { get; set; }
}


public class TetrioApiResponse<TContent>
	: ITetrioApiResponse<TContent>
{
	[JsonProperty("success")]
	public bool WasSuccessful { get; set; }

	[JsonProperty("data")]
	public TContent Data { get; set; }

	[JsonProperty("cache")]
	public CacheInfo CacheInfo { get; set; }
}