using System;
using System.Text.RegularExpressions;
using Ccr.Std.Core.Extensions;
using JetBrains.Annotations;
using Newtonsoft.Json;

namespace TetrioStats.Api.Infrastructure;

/// <summary>
/// Represents a Prisecter value with primary, secondary, and tertiary components.
/// </summary>
public struct Prisecter
{
	private static readonly Regex _parserRegex = new(
		@"(?<pri>\d+(?:\.\d+)?):(?<sec>\d+(?:\.\d+)?):(?<ter>\d+(?:\.\d+)?)",
		RegexOptions.Compiled);


	/// <summary>
	/// Primary value.
	/// </summary>
	[JsonProperty("pri")]
	public double Pri { get; set; }

	/// <summary>
	/// Secondary value.
	/// </summary>
	[JsonProperty("sec")]
	public double Sec { get; set; }

	/// <summary>
	/// Tertiary value.
	/// </summary>
	[JsonProperty("ter")]
	public double Ter { get; set; }

	
	public Prisecter()
	{
	}

	public Prisecter(double pri, double sec, double ter) : this()
	{
		Pri = pri;
		Sec = sec;
		Ter = ter;
	}

	public Prisecter(
		(double pri, double sec, double ter) tuple) : this(
			tuple.pri, tuple.sec, tuple.ter)
	{
	}

	public static Prisecter Parse(
		[NotNull] string prisecterStr)
	{
		var match = _parserRegex.Match(prisecterStr);

		if (!match.Success)
			throw new FormatException(
				$@"Cannot parse Prisecter object from input string {prisecterStr.Quote()}.");

		var pri = double.Parse(match.Groups["pri"].Value);
		var sec = double.Parse(match.Groups["sec"].Value);
		var ter = double.Parse(match.Groups["ter"].Value);

		return new(pri, sec, ter);
	}


	/// <summary>
	/// Converts the Prisecter instance to a string in the format "pri:sec:ter".
	/// </summary>
	/// <returns>
	/// A string representation of the Prisecter.
	/// </returns>
	public readonly override string ToString()
	{
		return $"{Pri}:{Sec}:{Ter}";
	}
}