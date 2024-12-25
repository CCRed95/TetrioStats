namespace TetrioStats.Api.Infrastructure;

public class NewsStream
{
	private readonly string _userId;

	public static readonly NewsStream Global = new();


	private NewsStream(
		string userId = null)
	{
		_userId = userId;
	}

	
	public static NewsStream UserStream(
		string userId)
	{
		return new(userId.ToLower());
	}

	public override string ToString()
	{
		return _userId == null 
			? "global" 
			: $"user_{_userId}";
	}
}