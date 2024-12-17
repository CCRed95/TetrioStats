using System;

namespace TetrioStats.Data.Domain;

/// <summary>
/// Calculates SheetBot's extended statistics based on a <see cref="TLStatsEntry"/> object.
/// </summary>
public class SheetBotExtendedStatsCalculator(TLStatsEntryWrapper wrapper)
{
	/// <summary>
	/// Attack per Piece (APP)
	/// </summary>
	/// <remarks>
	/// APM/(PPS*60)
	/// </remarks>
	public double AttackPerPiece
	{
		get => wrapper.APM / (wrapper.PPS * 60.0);
	}

	/// <summary>
	/// DS / Second = Downstack per Second, measures how many garbage lines you clear in a second
	/// </summary>
	/// <remarks>
	/// (VS/100)-(APM/60)
	/// </remarks>
	public double DownStackPerSecond
	{
		get => wrapper.VS / 100.0 - wrapper.APM / 60.0;
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
		get => DownStackPerSecond / wrapper.PPS;
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
		get => (wrapper.VS / 100d - wrapper.APM / 60d) / wrapper.PPS
			+ wrapper.APM                          / (wrapper.PPS * 60d);
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
		get => (DownStackPerPiece             * 100d
				+ (wrapper.VS / wrapper.APM - 2d)             * 150d
				+ (0.6d         - AttackPerPiece) * 150d)
			* (wrapper.PPS / 2.75d);
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
		get => AttackPerPiece * DownStackPerSecond / wrapper.PPS * 2d;
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
		get => wrapper.APM
			+ wrapper.PPS                      * 45d
			+ wrapper.VS                       * 0.444d
			+ AttackPerPiece             * 185d
			+ DownStackPerSecond         * 175d
			+ DownStackPerPiece          * 450d
			+ DownStackAndAttackPerPiece * 140d
			+ GarbageEfficiency          * 315d;
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