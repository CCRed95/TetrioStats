using Newtonsoft.Json;

namespace TetrioStats.Api.Domain.Json.Streams
{
	public class UserInfo
	{
		[JsonProperty("_id")]
		public string UserId { get; set; }

		[JsonProperty("username")]
		public string Username { get; set; }
	}
}