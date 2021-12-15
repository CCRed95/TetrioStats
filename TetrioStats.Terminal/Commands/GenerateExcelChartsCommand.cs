using System.Drawing;
using System.IO;
using System.Linq;
using Ccr.Std.Core.Extensions;
using Ccr.Terminal.Application;
using OfficeOpenXml;
using OfficeOpenXml.Drawing.Chart;
using TetrioStats.Api;
using TetrioStats.Data.Context;
using TetrioStats.Data.Domain;
using TetrioStats.Terminal.Extensions;
using static Ccr.Terminal.ExtendedConsole;

namespace TetrioStats.Terminal.Commands
{
	public class GenerateExcelChartsCommand
		: TerminalCommand<string>
	{
		private static readonly TetrioApiClient _client = new TetrioApiClient();
		private static readonly CoreContext _coreContext = new CoreContext();


		public override string CommandName => "gen-excel";

		public override string ShortCommandName => "xl";


		private static double rounded;

		public override void Execute(string args)
		{
			var extendedStats = XConsole.PromptYesNo("Query extended stats?");

			XConsole.Write(" Enter a username or userID to track: ", Color.MediumTurquoise);

			var userName = XConsole.ReadLine();
			userName = userName.ToLower();

			var userInfo = _client.ResolveUser(userName);

			var userData = _coreContext.TLStatsEntries
				.Where(t => t.UserID == userInfo.TetrioUserID)
				.OrderBy(t => t.DateTimeUtc)
				.ToList();

			var templateFile = new FileInfo(
				extendedStats
					? @"Y:/template-complex.xlsx" 
					: @"Y:/template.xlsx");

			var file = templateFile.CopyTo($@"Y:\{userName}-tetriostats{(extendedStats ? "-extended" : "")}.xlsx", true);

			ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

			using var pck = new ExcelPackage(file);
			var workbook = pck.Workbook;
			var dataWorksheet = workbook.Worksheets["data"];

			var tlDataTable = dataWorksheet.Tables["TLData"];

			var dataCells = dataWorksheet.Cells[$"data!$A$2:$B$20000"];
			dataCells.Clear();

			var graphsWorksheet = workbook.Worksheets["graphs"];

			var titleCell = graphsWorksheet.Cells[$"F6"];
			titleCell.Value = $"tetr.io - trending statistics - {userName}";

			var row = 0;

			var startCell = dataWorksheet.Cells["A2"];

			var commonXInterval = userData.Count / 20d;
			rounded = commonXInterval.Floor();

			if (rounded == 0)
			{
				commonXInterval = userData.Count / 10d;
				rounded = commonXInterval.Floor();
			}


			foreach (var dataRecord in userData)
			{
				startCell.Offset(row, 0).Value = $"{dataRecord.DateTimeUtc:yyyy-MM-dd HH}";

				if (dataRecord.TR.HasValue)
					startCell.Offset(row, 1).Value = dataRecord.TR.Value.Round(2);

				if (dataRecord.GP.HasValue)
					startCell.Offset(row, 2).Value = dataRecord.GP.Value;

				if (dataRecord.GW.HasValue)
					startCell.Offset(row, 3).Value = dataRecord.GW.Value;

				if (dataRecord.GP.HasValue && dataRecord.GW.HasValue)
				{
					var gw_d = (double)dataRecord.GP.Value;
					var wp = (dataRecord.GW.Value / gw_d) * 100;

					startCell.Offset(row, 4).Value = wp.Round(2);
				}

				if (dataRecord.Glicko.HasValue)
					startCell.Offset(row, 5).Value = dataRecord.Glicko.Value;

				startCell.Offset(row, 6).Value = dataRecord.UserRank.ToUpper();

				if (dataRecord.APM.HasValue)
					startCell.Offset(row, 7).Value = dataRecord.APM.Value;

				if (dataRecord.PPS.HasValue)
					startCell.Offset(row, 8).Value = dataRecord.PPS.Value;

				if (dataRecord.VS.HasValue)
					startCell.Offset(row, 9).Value = dataRecord.VS.Value;

				if (extendedStats)
				{
					var dataWrapper = new TLStatsEntryWrapper(dataRecord);
					var sheetBotCalc = new SheetBotExtendedStatsCalculator(dataWrapper);

					startCell.Offset(row, 10).Value = sheetBotCalc.AttackPerPiece;//.Round(2);

					if (dataWrapper.HasVS)
					{
						startCell.Offset(row, 11).Value = sheetBotCalc.DownStackPerSecond;//.Round(4);
						startCell.Offset(row, 12).Value = sheetBotCalc.DownStackPerPiece;//.Round(4);
						startCell.Offset(row, 13).Value = sheetBotCalc.DownStackAndAttackPerPiece;//.Round(6);
						startCell.Offset(row, 14).Value = sheetBotCalc.CheeseIndex;//.Round(4);
						startCell.Offset(row, 15).Value = sheetBotCalc.GarbageEfficiency;//.Round(4);
						startCell.Offset(row, 16).Value = sheetBotCalc.Area;//.Round(4);
						startCell.Offset(row, 17).Value = sheetBotCalc.NyaAttacksPerPiece;//.Round(4);
					}
				}
				row++;
			}


			var lastIndex = userData.Count + 1;

			ConfigureChart(workbook, "TRChart", "A", "B", lastIndex, Color.FromArgb(246, 58, 170));
			ConfigureChart(workbook, "PPSChart", "A", "I", lastIndex, Color.FromArgb(91, 155, 213));
			ConfigureChart(workbook, "VSChart", "A", "J", lastIndex, Color.FromArgb(240, 64, 108));
			ConfigureChart(workbook, "GlickoChart", "A", "F", lastIndex, Color.FromArgb(255, 192, 0));
			ConfigureChart(workbook, "APMChart", "A", "H", lastIndex, Color.FromArgb(110, 254, 158));
			ConfigureChart(workbook, "WRChart", "A", "E", lastIndex, Color.FromArgb(189, 46, 218));
			
			if (extendedStats)
			{
				ConfigureChart(workbook, "APPChart", "A", "K", lastIndex, Color.FromArgb(255, 192, 0));
				ConfigureChart(workbook, "DSPSChart", "A", "L", lastIndex, Color.FromArgb(110, 254, 158));
				ConfigureChart(workbook, "DSPPChart", "A", "M", lastIndex, Color.FromArgb(189, 46, 218));

				ConfigureChart(workbook, "DSAPPPPChart", "A", "N", lastIndex, Color.FromArgb(255, 192, 0));
				ConfigureChart(workbook, "CIChart", "A", "O", lastIndex, Color.FromArgb(110, 254, 158));
				ConfigureChart(workbook, "GEChart", "A", "P", lastIndex, Color.FromArgb(189, 46, 218));

				ConfigureChart(workbook, "AreaChart", "A", "Q", lastIndex, Color.FromArgb(255, 192, 0));
				ConfigureChart(workbook, "NAPPChart", "A", "R", lastIndex, Color.FromArgb(110, 254, 158));
			}

			static void ConfigureChart(
				ExcelWorkbook workbook,
				string chartName,
				string xColumn,
				string yColumn,
				int lastIndex,
				Color lineColor)
			{
				var chartsWorksheet = workbook.Worksheets["graphs"];

				var trChartBase = chartsWorksheet.Drawings[chartName];
				var trChart = trChartBase.As<ExcelLineChart>();

				if (trChart.Series.Any())
					trChart.Series.Delete(0);

				//=graphs!$A$2:$B$330
				var yRange = $"data!{yColumn}2:{yColumn}{lastIndex}";
				var xRange = $"data!{xColumn}2:{xColumn}{lastIndex}";

				var rangeSeries = trChart.Series.Add(yRange, xRange);

				rangeSeries.Border.Fill.Color = lineColor;
				rangeSeries.Border.Width = 1.5;
				rangeSeries.Effect.Glow.Radius = 0;
				//rangeSeries.Effect.SetPresetGlow(ePresetExcelGlowType.None);
				//rangeSeries.Effect.SetPresetShadow(ePresetExcelShadowType.None);
				//rangeSeries.Effect.SetPresetSoftEdges(ePresetExcelSoftEdgesType.None);
				//rangeSeries.Effect.SetPresetReflection(ePresetExcelReflectionType.None);


				rangeSeries.Smooth = true;

				var dataWorksheet = workbook.Worksheets["data"];

				var yCells = dataWorksheet.Cells[yRange];
				var xCells = dataWorksheet.Cells[xRange];

				var yCellValueList = yCells.Select(t => t.GetCellValue<double?>(0, 0)).ToList();
				//var xCellValueList = xCells.ToList();

				var min = yCellValueList.Min();
				var max = yCellValueList.Max();

				min *= 0.95;
				max *= 1.05;

				var absoluteYAxisRange = max - min;
				var yAxisStep = absoluteYAxisRange / 10d;

				trChart.YAxis.MinValue = min;
				trChart.YAxis.MaxValue = max;

				trChart.YAxis.MajorUnit = yAxisStep;

				trChart.XAxis.MinValue = null;
				trChart.XAxis.MaxValue = null;

				trChart.XAxis.MajorUnit = rounded;
			}
			pck.Save();
		}


		public override void Dispose()
		{
			_client.Dispose();
			_coreContext.Dispose();

			base.Dispose();
		}
	}
}