using System;
using System.IO;
using Newtonsoft.Json;

namespace TetrioStats.Core.Settings
{
	[Serializable]
	public class TetrioStatsSettings
	{
		private const string path = @"C:\TetrioStats\Settings.json";

		private static TetrioStatsSettings _instance;
		
		private string _authorizationBearerKey;


		[JsonIgnore]
		public static TetrioStatsSettings I
		{
			get => _instance ??= ReadFromFile();
		}

		public string AuthorizationBearerKey
		{
			get => _authorizationBearerKey;
			set
			{
				_authorizationBearerKey = value;
				SerializeToFile(this);
			}
		}


		private TetrioStatsSettings()
		{
		}


		public static void SerializeToFile(
			TetrioStatsSettings @this)
		{
			var json = JsonConvert.SerializeObject(@this);
			var fileInfo = new FileInfo(path);

			if (fileInfo.Directory is { Exists: false })
			{
				fileInfo.Directory.Create();
			}

			using var writer = File.Exists(path)
				? File.OpenWrite(path)
				: File.Create(path);

			using var streamWriter = new StreamWriter(writer);

			streamWriter.WriteLine(json);
		}

		public static TetrioStatsSettings ReadFromFile()
		{
			if (File.Exists(path))
			{
				var json = File.ReadAllText(path);

				return JsonConvert.DeserializeObject<TetrioStatsSettings>(json);
			}

			var instance = new TetrioStatsSettings();
			SerializeToFile(instance);

			return instance;
		}
	}
}