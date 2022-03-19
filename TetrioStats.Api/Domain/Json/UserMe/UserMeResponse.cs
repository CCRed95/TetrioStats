using Newtonsoft.Json;

namespace TetrioStats.Api.Domain.Json.UserMe
{
	public class UserMeResponse
		: ITetrioApiResponse
	{
		[JsonProperty("success")]
		public bool WasSuccessful { get; set; }

		[JsonProperty("user")]
		public LocalUserStorageData Content { get; set; }

		[JsonIgnore]
		CacheInfo ITetrioApiResponse.CacheInfo { get; set; }
	}
}