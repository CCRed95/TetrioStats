using Newtonsoft.Json;

namespace TetrioStats.Core.Data.Common.Users
{
	public class UserInfo
	{
		[JsonProperty("_id")]
		public string UserId { get; set; }

		[JsonProperty("username")]
		public string Username { get; set; }
	}
}