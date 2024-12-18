using System.Collections.Generic;
using System.IO;
using TetrioStats.Data.Domain;

namespace TetrioStats.Data.Seeding;

internal class CountryCodesSeeder
{
	public IEnumerable<Country> GetCountries()
	{
		var fileInfo = new FileInfo(
			@"Y:\CountryDataTable.csv");

		fileInfo.Refresh();

		if (!fileInfo.Exists)
			throw new FileNotFoundException();

		var _fileStream = fileInfo.OpenText();

		var line = _fileStream.ReadLine();

		while (line != null)
		{
			var columns = line.Split(',');

			yield return new Country
			{
				Name = columns[0],
				NumericCode = columns[1],
				CountryCode = columns[2]
			};

			line = _fileStream.ReadLine();
		}
	}
}