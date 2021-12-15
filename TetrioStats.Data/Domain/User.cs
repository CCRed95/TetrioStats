namespace TetrioStats.Data.Domain
{
	public class User
	{
		public int UserID { get; set; }

		public string TetrioUserID { get; set; }

		public string Username { get; set; }


		public User()
		{
		}

		public User(
			string tetrioUserId,
			string username) : this()
		{
			TetrioUserID = tetrioUserId;
			Username = username;
		}
	}
}