using Newtonsoft.Json;
using TetrioStats.Api.Domain.Json;

namespace TetrioStats.Api.Domain
{
	public interface ITetrioApiResponse
	{
		public bool WasSuccessful { get; set; }
		
		public CacheInfo CacheInfo { get; set; }
	}

	public interface ITetrioApiResponse<out TResponse, out TPayloadContent>
		: ITetrioApiResponse
			where TResponse : ITetrioResponsePayload<TPayloadContent>
	{
		public TResponse Response { get; }
	}

	public abstract class TetrioApiResponseBase<TResponse, TPayloadContent>
		: ITetrioApiResponse<TResponse, TPayloadContent>
			where TResponse : ITetrioResponsePayload<TPayloadContent>
	{
		[JsonProperty("success")]
		public bool WasSuccessful { get; set; }

		[JsonProperty("data")]
		public TResponse Response { get; set; }
		
		[JsonProperty("cache")]
		public CacheInfo CacheInfo { get; set; }


		[JsonIgnore]
		public TPayloadContent Content
		{
			get => Response.Payload;
		}
	}

	public static class ITetrioApiResponseExtensions
	{
		//public static TPayloadContent GetContent<TResponse>(
		//	this TResponse @this)
		//		where TResponse : ITetrioApiResponse<TPayload>
		//		where TPayload : ITetrioResponsePayload<TPayloadContent>
		//{
		//	return @this.Response.Payload;
		//}
		//public static TPayloadContent GetContent<TResponse, TPayload, TPayloadContent>(
		//	this TResponse @this)
		//		where TResponse : ITetrioApiResponse<TPayload>
		//		where TPayload : ITetrioResponsePayload<TPayloadContent>
		//{
		//	return @this.Response.Payload;
		//}
	}
}
