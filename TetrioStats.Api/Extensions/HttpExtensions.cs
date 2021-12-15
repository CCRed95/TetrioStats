using System.Net.Http;

namespace TetrioStats.Api.Extensions
{
	public static class HttpExtensions
	{
		public static HttpRequestMessage WithHeader(
			this HttpRequestMessage @this, 
			string key, 
			string value)
		{
			@this.Headers.TryAddWithoutValidation(key, value);
			return @this;
		}
		
		public static HttpClient WithDefaultRequestHeader(
			this HttpClient @this, 
			string key, 
			string value)
		{
			@this.DefaultRequestHeaders.Add(key, value);
			return @this;
		}
	}
}