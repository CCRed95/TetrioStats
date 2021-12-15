using System;
using TetrioStats.Data.Maps;

namespace TetrioStats.Data.Domain
{
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
	/// Calculates SheetBot's extended statistics based on a <see cref="TLStatsEntry"/> object.
	/// </summary>
	public class SheetBotExtendedStatsCalculator
	{
		private readonly TLStatsEntryWrapper d;


		public SheetBotExtendedStatsCalculator(
			TLStatsEntryWrapper wrapper)
		{
			d = wrapper;
		}


		/// <summary>
		/// Attack per Piece (APP)
		/// </summary>
		/// <remarks>
		/// APM/(PPS*60)
		/// </remarks>
		public double AttackPerPiece
		{
			get => d.APM / (d.PPS * 60.0);
		}

		/// <summary>
		/// DS / Second = Downstack per Second, measures how many garbage lines you clear in a second
		/// </summary>
		/// <remarks>
		/// (VS/100)-(APM/60)
		/// </remarks>
		public double DownStackPerSecond
		{
			get => d.VS / 100.0 - d.APM / 60.0;
		}

		/// <summary>
		/// DS / Piece = Downstack per piece, does the same thing as DS / Second but per piece instead of
		/// per second
		/// </summary>
		/// <remarks>
		/// ((VS/100)-(APM/60))/PPS
		/// </remarks>
		public double DownStackPerPiece
		{
			get => DownStackPerSecond / d.PPS;
		}

		/// <summary>
		/// DS + APP / Piece = Downstack + Attack Per piece, just combine the Attack Per Piece and
		/// Downstack per piece values together.
		/// </summary>
		/// <remarks>
		/// (((VS/100)-(APM/60))/PPS) + APM/(PPS*60)
		/// </remarks>
		public double DownStackAndAttackPerPiece
		{
			get => (d.VS / 100d - d.APM / 60d) / d.PPS
				+ d.APM / (d.PPS * 60d);
		}

		/// <summary>
		/// Cheese Index: Is an approximation how much clean / cheese a player sends,
		/// lower = more clean, higher = more cheese.
		/// </summary>
		/// <remarks>
		/// (((DS/Piece * 100) + (((VS/APM)-2)*150) + (0.6-APP)*150)*(PPS/2.75)) 
		/// Invented by Kerrmunism (explorat0ri).
		/// </remarks>
		public double CheeseIndex
		{
			get => (DownStackPerPiece * 100d
				+ (d.VS / d.APM - 2d) * 150d
				+ (0.6d - AttackPerPiece) * 150d)
				* (d.PPS / 2.75d);
		}

		/// <summary>
		/// Garbage Efficiency: Measures how well a player uses their garbage, higher = better or they
		/// use their garbage more, lower = they mostly send their garbage back at cheese or rarely
		/// clear garbage.
		/// </summary>
		/// <remarks>
		/// ((app*dssecond)/pps)*2 
		/// Invented by Zepheniah and Dragonboy.
		/// </remarks>
		public double GarbageEfficiency
		{
			get => AttackPerPiece * DownStackPerSecond / d.PPS * 2d;
		}

		/// <summary>
		/// Area = How much space your shape takes up on the !vs graph, if you exclude the cheese
		/// and vs/apm sections.
		/// </summary>
		/// <remarks>
		/// apm + pps * 45 + vs * 0.444 + app * 185 + dssecond * 175 + dspiece * 450 + dsapppiece * 140 + garbageEffi * 315 
		/// </remarks>
		public double Area
		{
			get => d.APM
				+ d.PPS * 45d
				+ d.VS * 0.444d
				+ AttackPerPiece * 185d
				+ DownStackPerSecond * 175d
				+ DownStackPerPiece * 450d
				+ DownStackAndAttackPerPiece * 140d
				+ GarbageEfficiency * 315d;
		}

		/// <summary>
		/// Essentially, a measure of your ability to send cheese while still maintaining a high APP.
		/// </summary>
		/// <remarks>
		/// APP + tan(((CheeseIndex/7)+1)*Pi/180)
		/// Invented by Wertj.
		/// </remarks>
		public double NyaAttacksPerPiece
		{
			get => AttackPerPiece
				+ Math.Tan((CheeseIndex / 7d + 1d) * Math.PI / 180d);

			// AttackPerPiece + Math.Tan(CheeseIndex / 7.0d + 1);
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
}