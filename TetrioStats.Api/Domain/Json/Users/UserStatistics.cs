using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using TetrioStats.Api.Domain.Json.Converters;
using TetrioStats.Api.Domain.Users;

// ReSharper disable StringLiteralTypo

namespace TetrioStats.Api.Domain.Json.Users
{
	public class UserStatistics
	{
		[JsonProperty("_id")]
		public string UserID { get; set; }

		[JsonProperty("username")]
		public string Username { get; set; }

		[JsonProperty("role")]
		public UserRole Role { get; set; }

		[JsonProperty("ts")]
		public DateTime TimeStamp { get; set; }

		[JsonProperty("badges")]
		public List<object> Badges { get; set; }

		[JsonProperty("xp")]
		public double XP { get; set; }

		[JsonProperty("gamesplayed")]
		public int GamesPlayed { get; set; }

		[JsonProperty("gameswon")]
		public int GamesWon { get; set; }

		[JsonProperty("gametime")]
		[JsonConverter(typeof(UnixTimeStampConverter))]
		public TimeSpan TotalGamePlayDuration { get; set; }

		[JsonProperty("country")]
		public string Country { get; set; }

		[JsonProperty("supporter")]
		public bool IsSupporter { get; set; }

		[JsonProperty("supporter_tier")]
		public int SupporterTier { get; set; }

		[JsonProperty("verified")]
		public bool IsVerified { get; set; }

		[JsonProperty("friend_count")]
		public int FriendCount { get; set; }

		[JsonProperty("league")]
		public TetraLeagueStatistics TetraLeagueStats { get; set; }
	}
}