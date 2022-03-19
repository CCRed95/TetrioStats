using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using TetrioStats.Api.Domain.Users;

namespace TetrioStats.Api.Domain.Json.UserMe
{
	public class LocalUserStorageData
  {
    [JsonProperty("_id")]
    public string Id { get; set; }

    [JsonProperty("username")]
    public string Username { get; set; }

    [JsonProperty("email")]
    public string Email { get; set; }

    [JsonProperty("role")]
    public UserRole Role { get; set; }

    [JsonProperty("ts")]
    public DateTimeOffset TimeStamp { get; set; }

    [JsonProperty("badges")]
    public List<object> Badges { get; set; }

    [JsonProperty("xp")]
    public long XP { get; set; }

    [JsonProperty("banlist")]
    public List<object> BanList { get; set; }

    [JsonProperty("warnings")]
    public List<object> Warnings { get; set; }

    [JsonProperty("bannedstatus")]
    public string BannedStatus { get; set; }

    [JsonProperty("privacy_showwon")]
    public bool PrivacyShowWon { get; set; }

    [JsonProperty("privacy_showplayed")]
    public bool PrivacyShowPlayed { get; set; }

    [JsonProperty("privacy_showgametime")]
    public bool PrivacyShowGameTime { get; set; }

    [JsonProperty("privacy_showcountry")]
    public bool PrivacyShowCountry { get; set; }

    [JsonProperty("privacy_privatemode")]
    public string PrivacyPrivateMode { get; set; }

    [JsonProperty("privacy_status_shallow")]
    public string PrivacyStatusShallow { get; set; }

    [JsonProperty("privacy_status_deep")]
    public string PrivacyStatusDeep { get; set; }

    [JsonProperty("privacy_status_exact")]
    public string PrivacyStatusExact { get; set; }

    [JsonProperty("privacy_dm")]
    public string PrivacyDm { get; set; }

    [JsonProperty("privacy_invite")]
    public string PrivacyInvite { get; set; }

    [JsonProperty("country")]
    public string Country { get; set; }

    [JsonProperty("thanked")]
    public bool Thanked { get; set; }
  }
}
