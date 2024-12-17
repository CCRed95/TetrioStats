using System.Net.Http;

namespace TetrioStats.Api.Utilities
{
	public static class HttpRequest
	{
		public static HttpRequestMessage Get(
			string url)
		{
			return new(new("GET"), url);
		}

		public static HttpRequestMessage Post(
			string url)
		{
			return new(new("POST"), url);
		}

		public static HttpRequestMessage Update(
			string url)
		{
			return new(new("UPDATE"), url);
		}
	}
}