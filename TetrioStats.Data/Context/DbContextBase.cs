using System.IO;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace TetrioStats.Data.Context
{
	public abstract class DbContextBase
		: DbContext
	{
		public abstract string DatabaseName { get; }


		protected DbContextBase()
		{
			Database.Migrate();
		}


		protected override void OnConfiguring(
			DbContextOptionsBuilder optionsBuilder)
		{
			base.OnConfiguring(optionsBuilder);

			const string databaseFolderPath = @"C:\Tetris\Data\";

			var databaseDirectoryInfo = new DirectoryInfo(databaseFolderPath);

			if (!databaseDirectoryInfo.Exists)
				databaseDirectoryInfo.Create();

			var connectionStringBuilder = new SqliteConnectionStringBuilder
			{
				DataSource = $@"{databaseDirectoryInfo.FullName}\{DatabaseName}.db"
			};

			var connectionString = connectionStringBuilder.ToString();

			optionsBuilder.UseSqlite(connectionString);
		}
	}
}