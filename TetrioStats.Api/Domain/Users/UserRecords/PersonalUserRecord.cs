using System.Collections.Generic;
using Newtonsoft.Json;

namespace TetrioStats.Api.Domain.Users.UserRecords;

/// <summary>
/// Represents a collection of personal user records.
/// </summary>
/// <typeparam name="TRecord">
/// The type of record contained in the entries.
/// </typeparam>
public class PersonalUserRecord<TRecord>
{
	/// <summary>
	/// The list of entries representing personal user records.
	/// </summary>
	[JsonProperty("entries")]
	public List<TRecord> Entries { get; set; }
}