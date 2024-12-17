using Newtonsoft.Json;

namespace TetrioStats.Api.Domain.Users;

/// <summary>
/// Represents a user search result.
/// </summary>
public class UserSearch
{
	/// <summary>
	/// The user's unique ID.
	/// </summary>
	[JsonProperty("_id")]
	public string Id { get; set; }

	/// <summary>
	/// The user's username.
	/// </summary>
	[JsonProperty("username")]
	public string Username { get; set; }
}


/// <summary>
/// Represents the data contained in a user search packet.
/// </summary>
public class UserSearchResponseData
{
	/// <summary>
	/// The user search result.
	/// </summary>
	[JsonProperty("user")]
	public UserSearch User { get; set; }
}


/// <summary>
/// Represents a user search packet.
/// </summary>
public class UserSearchResponse : TetrioApiResponse<UserSearchResponseData>;