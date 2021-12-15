using Newtonsoft.Json;

namespace TetrioStats.Api.Domain.Json.Users
{
	public class UserData
		: ITetrioResponsePayload<UserStatistics>
	{
		[JsonProperty("user")]
		public UserStatistics Payload { get; set; }
	}
}