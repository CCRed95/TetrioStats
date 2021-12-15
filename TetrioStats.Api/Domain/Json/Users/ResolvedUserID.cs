using Newtonsoft.Json;

namespace TetrioStats.Api.Domain.Json.Users
{
	internal class ResolvedUserID
	{
		[JsonProperty("success")]
		public bool WasSuccessful { get; set; }

		[JsonProperty("_id")]
		public string UserID { get; set; }
	}
}