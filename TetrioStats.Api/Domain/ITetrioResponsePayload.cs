namespace TetrioStats.Api.Domain
{
	public interface ITetrioResponsePayload<out TResponsePayload>
	{
		public TResponsePayload Payload { get; }
	}
}