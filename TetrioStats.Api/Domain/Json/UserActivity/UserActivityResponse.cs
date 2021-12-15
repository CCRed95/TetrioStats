using System;

namespace TetrioStats.Api.Domain.Json.UserActivity
{
	public class UserActivityResponse
	{
		public UserInfo UserInfo { get; }

		public UserOnlineState OnlineState { get; }
		
		public DateTime? LastLoginTimeUtc { get; }

		public TimeSpan? CurrentSessionDuration { get; }
	}
}