using System;
using TetrioStats.Data.Maps;

namespace TetrioStats.Data.Domain;

/// <summary>
/// See <see cref="TLStatsEntryMap"/>
/// </summary>
public class TLStatsEntry
{
	public int TLStatsEntryID { get; set; }

	public string UserID { get; set; }

	public string Username { get; set; }

	public DateTime DateTimeUtc { get; set; }

	public double? XP { get; set; }

	public string Country { get; set; }

	public int? GP { get; set; }

	public int? GW { get; set; }

	public double? TR { get; set; }

	public double? Glicko { get; set; }

	public double? RD { get; set; }

	public string UserRank { get; set; }

	public double? APM { get; set; }

	public double? PPS { get; set; }

	public double? VS { get; set; }


	public TLStatsEntry()
	{
	}

	public TLStatsEntry(
		string userID,
		string username,
		DateTime dateTimeUtc,
		double? xp,
		string country,
		int? gp,
		int? gw,
		double? tr,
		double? glicko,
		double? rd,
		string userRank,
		double? apm,
		double? pps,
		double? vs) : this()
	{
		UserID = userID;
		Username = username;
		DateTimeUtc = dateTimeUtc;
		XP = xp;
		Country = country;
		GP = gp;
		GW = gw;
		TR = tr;
		Glicko = glicko;
		RD = rd;
		UserRank = userRank;
		APM = apm;
		PPS = pps;
		VS = vs;
	}
}


/// <summary>
/// See <see cref="TLStatsEntryMap"/>
/// </summary>
public class TLStatsEntryWrapper
{
	private readonly TLStatsEntry _source;

	public bool HasVS { get; }

	//public int TLStatsEntryID { get; }

	public string UserID { get; }

	public string Username { get; }

	public DateTime DateTimeUtc { get; }

	public double XP { get; }

	public string Country { get; }

	public int GP { get; }

	public int GW { get; }

	public double TR { get; }

	public double Glicko { get; }

	public double RD { get; }

	public string UserRank { get; }

	public double APM { get; }

	public double PPS { get; }

	public double VS { get; }


	public TLStatsEntryWrapper(
		TLStatsEntry source)
	{
		_source = source;

		HasVS = source.VS.HasValue;

		if (!source.GP.HasValue
		    || !source.GW.HasValue
		    || !source.TR.HasValue
		    || !source.Glicko.HasValue
		    || !source.RD.HasValue
		    || !source.APM.HasValue
		    || !source.PPS.HasValue)
		{
			throw new NotSupportedException();
		}

		UserID = source.UserID;
		Username = source.Username;
		DateTimeUtc = source.DateTimeUtc;
		XP = source.XP.GetValueOrDefault(-1);
		Country = source.Country;
		GP = source.GP.Value;
		GW = source.GW.Value;
		TR = source.TR.Value;
		Glicko = source.Glicko.Value;
		RD = source.RD.Value;
		UserRank = source.UserRank;
		APM = source.APM.Value;
		PPS = source.PPS.Value;
		VS = source.VS.GetValueOrDefault(-1);
	}
}