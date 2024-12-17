using System.Collections.Generic;
using TetrioStats.Api.Infrastructure;

namespace TetrioStats.Api.Http.Parameters;

/// <summary>
/// Represents a query for personal records.
/// </summary>
public abstract class PersonalRecordsQuery
{
	/// <summary>
	/// Converts the query into a list of key-value pairs for query parameters.
	/// </summary>
	/// <returns>
	/// A list of query parameters as key-value pairs.
	/// </returns>
	public abstract List<KeyValuePair<string, string>> AsQueryParams();


	/// <summary>
	/// Represents a query with an "after" bound.
	/// </summary>
	public class After : PersonalRecordsQuery
	{
		public Prisecter AfterValue { get; set; }

		public long? Limit { get; set; }


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

			return result;
		}
	}
	
	/// <summary>
	/// Represents a query with a "before" bound.
	/// </summary>
	public class Before : PersonalRecordsQuery
	{
		public Prisecter BeforeValue { get; set; }

		public long? Limit { get; set; }


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

			return result;
		}
	}
	
	/// <summary>
	/// Represents a query without any bounds.
	/// </summary>
	public class NotBound : PersonalRecordsQuery
	{
		public long? Limit { get; set; }


		public override List<KeyValuePair<string, string>> AsQueryParams()
		{
			var result = new List<KeyValuePair<string, string>>();

			if (Limit.HasValue)
			{
				result.Add(new("limit", Limit.Value.ToString()));
			}

			return result;
		}
	}


	/// <summary>
	/// Represents a query with no parameters.
	/// </summary>
	public class None : PersonalRecordsQuery
	{
		public override List<KeyValuePair<string, string>> AsQueryParams()
		{
			return[];
		}
	}
}