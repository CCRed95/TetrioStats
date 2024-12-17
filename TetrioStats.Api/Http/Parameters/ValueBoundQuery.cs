using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Ccr.Std.Core.Extensions;
using Ccr.Std.Core.Extensions.NumericExtensions;
using JetBrains.Annotations;
using TetrioStats.Api.Infrastructure;

namespace TetrioStats.Api.Http.Parameters;


public class UserLeaderboardParameterBuilder
{
	/// <summary>
	/// The upper bound. Use this to paginate downwards: take the lowest seen prisecter and pass
	/// that back through this field to continue scrolling.
	/// </summary>
	[CanBeNull]
	private Prisecter? _after;

	/// <summary>
	/// The lower bound. Use this to paginate upwards: take the highest seen prisecter and pass that
	/// back through this field to continue scrolling. If set, the search order is reversed
	/// (returning the lowest items that match the query)
	/// </summary>
	[CanBeNull]
	private Prisecter? _before;

	/// <summary>
	/// The amount of entries to return, between 1 and 100. 25 by default.
	/// </summary>
	[CanBeNull]
	private int? _limit;

	/// <summary>
	/// The ISO 3166-1 country code to filter to. Leave unset to not filter by country.
	/// </summary>
	[CanBeNull]
	private string _countryCode;
		

	public static UserLeaderboardParameterBuilder Builder
	{
		get => new();
	}


	private UserLeaderboardParameterBuilder()
	{
	}


	public UserLeaderboardParameterBuilder WithUpperBoundConstraint(
		Prisecter after)
	{
		if (_before != null)
			throw new NotSupportedException(
				$"Cannot set Upper Bound Constraint Prisecter to value '{after}' because a Lower Bound " +
				$"Constraint Prisecter value of '{_before}' has already been set on this builder. Both " +
				$"Upper and Lower Bound Constraint Prisecter values cannot be combined.");

		if (_after != null)
			throw new NotSupportedException(
				$"Cannot set Upper Bound Constraint Prisecter to value '{after}' because an existing value " +
				$"of '{_after}' has already been set on this builder.");

		_after = after;

		return this;
	}

	public UserLeaderboardParameterBuilder WithLowerBoundConstraint(
		Prisecter before)
	{
		if (_after != null)
			throw new NotSupportedException(
				$"Cannot set Upper Bound Constraint Prisecter to value '{before}' because an Upper Bound " +
				$"Constraint Prisecter value of '{_after}' has already been set on this builder. Both " +
				$"Upper and Lower Bound Constraint Prisecter values cannot be combined.");

		if (_before != null)
			throw new NotSupportedException(
				$"Cannot set Lower Bound Constraint Prisecter to value '{before}' because an existing value " +
				$"of '{_before}' has already been set on this builder.");


		_before = before;

		return this;
	}

	public UserLeaderboardParameterBuilder WithLimit(
		[ValueRange(1, 100)] int limit)
	{
		if (limit.IsNotWithin((1, 100)))
			throw new ArgumentOutOfRangeException(
				nameof(limit), limit,
				$"Cannot set Limit to value '{limit}' because the limit value must be between 1 and 100.");

		if (_before != null)
			throw new NotSupportedException(
				$"Cannot set Limit to value '{limit}' because an existing value {_limit}' has already been " +
				$"set on this builder.");
		
		_limit = limit;

		return this;
	}

	public UserLeaderboardParameterBuilder WithCountryConstraint(
		[NotNull] string countryCode)
	{
		if (_countryCode != null)
			throw new NotSupportedException(
				$"Cannot set Country Code Constraint to value '{countryCode}' because an existing value " +
				$"{_countryCode}' has already been set on this builder.");

		try
		{
			var regionInfo = new RegionInfo(countryCode);
			countryCode = regionInfo.TwoLetterISORegionName;
		}
		catch (ArgumentException ex)
		{
			throw new ArgumentException(
				$"The provided {nameof(countryCode).SQuote()} parameter value {countryCode.Quote()} is not a " +
				$"valid two-letter code defined in ISO 3166 for country/region.", 
				nameof(countryCode), ex);
		}

		_countryCode = countryCode;
		return this;
	}


	public string Build()
	{
		var sb = new StringBuilder();

		if (_after != null)
			sb.Append($"&after={_after}");
		
		if (_before != null)
			sb.Append($"&before={_before}");

		if (_limit != null)
			sb.Append($"&limit={_limit}");

		if (_countryCode != null)
			sb.Append($"&country={_countryCode}");

		var str = sb.ToString();

		var modifiedStr = str.IsNullOrEmptyEx()
			? string.Empty
			: $"?{str.TrimStart('&')}";

		return modifiedStr;
	}
}



/// <summary>
/// Represents a query with value bounds.
/// </summary>
public abstract class ValueBoundQuery
{
	/// <summary>
	/// Converts the query into a list of query parameters.
	/// </summary>
	/// <returns>
	/// A list of key-value pairs representing query parameters.
	/// </returns>
	public abstract List<KeyValuePair<string, string>> AsQueryParams();


	/// <summary>
	/// Represents a query with an "after" bound.
	/// </summary>
	public class After : ValueBoundQuery
	{
		public Prisecter AfterValue { get; set; }

		public long? Limit { get; set; }

		public string Country { get; set; }


		public override List<KeyValuePair<string, string>> AsQueryParams()
		{
			var result = new List<KeyValuePair<string, string>>
			{
				new("after", AfterValue.ToString())
			};

			if (Limit.HasValue)
			{
				result.Add(new("limit", Limit.Value.ToString()));
			}

			if (!string.IsNullOrEmpty(Country))
			{
				result.Add(new("country", Country));
			}

			return result;
		}
	}

	/// <summary>
	/// Represents a query with a "before" bound.
	/// </summary>
	public class Before : ValueBoundQuery
	{
		public Prisecter BeforeValue { get; set; }

		public long? Limit { get; set; }

		public string Country { get; set; }


		public override List<KeyValuePair<string, string>> AsQueryParams()
		{
			var result = new List<KeyValuePair<string, string>>
			{
				new("before", BeforeValue.ToString())
			};

			if (Limit.HasValue)
			{
				result.Add(new("limit", Limit.Value.ToString()));
			}

			if (!string.IsNullOrEmpty(Country))
			{
				result.Add(new("country", Country));
			}

			return result;
		}
	}

	/// <summary>
	/// Represents a query without any bounds.
	/// </summary>
	public class NotBound : ValueBoundQuery
	{
		public long? Limit { get; set; }

		public string Country { get; set; }


		public override List<KeyValuePair<string, string>> AsQueryParams()
		{
			var result = new List<KeyValuePair<string, string>>();

			if (Limit.HasValue)
			{
				result.Add(new("limit", Limit.Value.ToString()));
			}

			if (!string.IsNullOrEmpty(Country))
			{
				result.Add(new("country", Country));
			}

			return result;
		}
	}

	/// <summary>
	/// Represents a query with no parameters.
	/// </summary>
	public class None : ValueBoundQuery
	{
		public override List<KeyValuePair<string, string>> AsQueryParams()
		{
			return [];
		}
	}
}