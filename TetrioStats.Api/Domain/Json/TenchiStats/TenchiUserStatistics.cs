using System;
using Newtonsoft.Json;

// ReSharper disable StringLiteralTypo

namespace TetrioStats.Api.Domain.Json.TenchiStats
{
	public class TenchiUserStatistics
		: IEquatable<TenchiUserStatistics>
	{
		[JsonProperty("_id")]
		public string UserID { get; set; }

		[JsonProperty("username")]
		public string Username { get; set; }

		[JsonProperty("role")]
		public string Role { get; set; }

		[JsonProperty("xp")]
		public double? XP { get; set; }

		[JsonProperty("country")]
		public string Country { get; set; }

		[JsonProperty("supporter")]
		public bool? IsSupporter { get; set; }

		[JsonProperty("supporter_tier")]
		public int? SupporterTier { get; set; }

		[JsonProperty("league")]
		public TetraLeagueHistoricalStatistics League { get; set; }


		public bool Equals(TenchiUserStatistics other)
		{
			if (ReferenceEquals(null, other))
				return false;
			if (ReferenceEquals(this, other))
				return true;

			return UserID == other.UserID
				&& Username == other.Username
				&& Role == other.Role
				&& Nullable.Equals(XP, other.XP)
				&& Country == other.Country
				&& IsSupporter == other.IsSupporter
				&& SupporterTier == other.SupporterTier
				&& Equals(League, other.League);
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj))
				return false;

			if (ReferenceEquals(this, obj))
				return true;

			if (obj.GetType() != GetType())
				return false;

			return Equals((TenchiUserStatistics)obj);
		}

		public override int GetHashCode()
		{
			return HashCode.Combine(
				UserID,
				Username,
				Role,
				XP,
				Country,
				IsSupporter,
				SupporterTier,
				League);
		}
	}
}