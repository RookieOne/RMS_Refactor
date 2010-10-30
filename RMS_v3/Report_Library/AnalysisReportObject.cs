using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using Excel;

using RMS_DALObjects;
using RMS_BusinessObjects;

namespace Report_Library
{
	/// <summary>
	/// Summary description for AnalysisReportObject.
	/// </summary>
	public class AnalysisReportObject
	{
		#region "Variables"

		string datePulled;
		string dataTimeFrame;
		string dataName;

		AnalysisBO var_Analysis;

		Excel.Workbook workBook;

		baseDALObject data;

		#endregion

		#region "Constructors"

		public AnalysisReportObject()
		{	data = new baseDALObject();	}

		#endregion

		#region "Properties"

		public AnalysisBO Analysis
		{
			get{ return var_Analysis;	}
			set{ var_Analysis = value; }
		}

		#endregion

		#region "Methods"

		public void createReport(string filePath, DataSet analyzedDataSet, string contractTitle, string rateSchedulesAnalyzed, string dataSetID, string inChgInc, string outChgInc, string costInc, string filterEntity, string filterInsurancePlanCode, string baseComments)
		{
			SqlDataReader sqlDataRdr = data.getDataReader("SELECT * FROM DataSet WHERE DatasetSeqNum=" + dataSetID);

			if(sqlDataRdr.Read())
			{
				datePulled = "Data pulled from EG : " + sqlDataRdr["PulledDate"];
				dataTimeFrame = String.Format("{0:MMM-yy}", sqlDataRdr["startDate"]) + " thru " + String.Format("{0:MMM-yy}", sqlDataRdr["endDate"]);
				dataName = sqlDataRdr["DatasetName"].ToString();
			}

			Excel.Application report = new Excel.ApplicationClass();
			Excel.Workbooks workbooks = report.Workbooks;

			workBook = workbooks.Add(Excel.XlWBATemplate.xlWBATWorksheet);

			Excel.Worksheet summarySheet;


			// **** SUMMARY
      summarySheet = this.getSummaryWorkSheet();
			workbooks.Add(summarySheet);

      report.Visible = true;

			string saveFile = filePath + "\\" + contractTitle + " using " + dataName + ".xls";

			if (System.IO.File.Exists(saveFile))
			{	System.IO.File.Delete(saveFile);	}

			report.Save(saveFile);

      report.Visible = true;
			// report.Quit();
			}



		#region "Excel Functions"

		private Excel.Worksheet createReportWorkSheet(string workSheetTitle)
		{
			Excel.Worksheet newWorkSheet = (Excel.Worksheet) workBook.Worksheets.Add(Type.Missing, Type.Missing, Type.Missing, Type.Missing);

			newWorkSheet.PageSetup.LeftFooter = "THR Confidential";
			newWorkSheet.PageSetup.CenterFooter = DateTime.Today.ToString();
			newWorkSheet.PageSetup.Orientation = Excel.XlPageOrientation.xlLandscape;
			newWorkSheet.PageSetup.FitToPagesWide = 1;
			newWorkSheet.PageSetup.FitToPagesTall = 1;

			newWorkSheet.Name = workSheetTitle;

			newWorkSheet.Cells[1, 1] = this.Analysis.ContractTitle + " " + workSheetTitle;

			Excel.Range sheetTitlesRange = newWorkSheet.get_Range(newWorkSheet.Cells[1, 1], newWorkSheet.Cells[1, 10]);

			//sheetTitlesRange.Merge();
			sheetTitlesRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
			sheetTitlesRange.Font.Bold = true;
			sheetTitlesRange.Font.Size = 12;


			newWorkSheet.Cells[2, 1] = this.Analysis.RateSchedulesAnalyzed;

			( (Range) newWorkSheet.Cells[2, 1]).HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
			( (Range) newWorkSheet.Cells[2, 1]).Font.Bold = true;
			( (Range) newWorkSheet.Cells[2, 1]).Font.Size = 10;

			return newWorkSheet;
		}


		private void setRangeCurrency(Excel.Range range)
		{
			range.Style = "Currency";
			range.NumberFormat = "_($* #,##0_);_($* (#,##0);_($* \"\"-\"\"??_);_(@_)";
		}

		private void setRangeComma(Excel.Range range)
		{
			range.Style = "Comma";
			range.NumberFormat = "_(* #,##0_);_(* (#,##0);_(* \"\"-\"\"??_);_(@_)";
		}

		private void setRangePercent(Excel.Range range)
		{
			range.Style = "Percent";
			range.NumberFormat = "0.00%";
		}


		#endregion


		private Excel.Worksheet getSummaryWorkSheet()
		{
			Excel.Worksheet worksheet = createReportWorkSheet("Patient Type Summary");

			System.Data.DataTable summaryTable = this.Analysis.getPatientTypeSummaryTable();

			int excelRow = 3;
			
			worksheet.Cells[excelRow, 1] = "Patient Type";
			worksheet.Cells[excelRow, 2] = "Cases";
			worksheet.Cells[excelRow, 3] = "Charges";
			worksheet.Cells[excelRow, 4] = "NetRev";
			worksheet.Cells[excelRow, 5] = "Model";
			worksheet.Cells[excelRow, 6] = "POC";
			worksheet.Cells[excelRow, 7] = "% Var";
			worksheet.Cells[excelRow, 8] = "Cost";
			worksheet.Cells[excelRow, 9] = "NI";
			worksheet.Cells[excelRow, 10] = "NI %";

			Excel.Range columnsTitlesRange = worksheet.get_Range(worksheet.Cells[excelRow, 1], worksheet.Cells[excelRow, 10]);

			columnsTitlesRange.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlMedium, Excel.XlColorIndex.xlColorIndexAutomatic, null);
			columnsTitlesRange.Borders[Excel.XlBordersIndex.xlInsideVertical].LineStyle = Excel.XlLineStyle.xlContinuous;
			columnsTitlesRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
			columnsTitlesRange.Interior.ColorIndex = 15;
			columnsTitlesRange.Interior.Pattern = Excel.XlPattern.xlPatternSolid;

			int topRow = excelRow + 1;

			excelRow += 1;

			foreach(DataRow dRow in summaryTable.Rows)
			{
				worksheet.Cells[excelRow, 1] = dRow["Title"];
				worksheet.Cells[excelRow, 2] = dRow["Cases"];
				worksheet.Cells[excelRow, 3] = dRow["Charges"];
				worksheet.Cells[excelRow, 4] = dRow["NetRev"];
				worksheet.Cells[excelRow, 5] = dRow["Model"];

				worksheet.get_Range(worksheet.Cells[excelRow, 6], worksheet.Cells[excelRow, 6]).FormulaR1C1 = "=R" + excelRow + "C5/R" + excelRow + "C3";			 // POC = Model/Charges

				worksheet.get_Range(worksheet.Cells[excelRow, 7], worksheet.Cells[excelRow, 7]).FormulaR1C1 = "=(R" + excelRow + "C5/R" + excelRow + "C4) - 1";			 // %Var Model/NetRev -1

				worksheet.Cells[excelRow, 8] = dRow["Cost"];

				worksheet.get_Range(worksheet.Cells[excelRow, 9], worksheet.Cells[excelRow, 9]).FormulaR1C1 = "=R" + excelRow + "C5-R" + excelRow + "C8";			 // NI Model-Cost

				worksheet.get_Range(worksheet.Cells[excelRow, 10], worksheet.Cells[excelRow, 10]).FormulaR1C1 = "=R" + excelRow + "C9/R" + excelRow + "C5";			 // NI % NI/Model

				excelRow += 1;
			}

			int bottomRow = excelRow - 1;
			worksheet.Cells[excelRow, 2] = "=SUM(B" + topRow + ":B" + bottomRow + ")";	// Cases
			worksheet.Cells[excelRow, 3] = "=SUM(C" + topRow + ":C" + bottomRow + ")";	// Charges
			worksheet.Cells[excelRow, 4] = "=SUM(D" + topRow + ":D" + bottomRow + ")";	// NetRev
			worksheet.Cells[excelRow, 5] = "=SUM(E" + topRow + ":E" + bottomRow + ")";	// Model

			worksheet.get_Range(worksheet.Cells[excelRow, 6], worksheet.Cells[excelRow, 6]).FormulaR1C1 = "=R" + excelRow + "C5/R" + excelRow + "C3";	// POC Model/Charges
			worksheet.get_Range(worksheet.Cells[excelRow, 7], worksheet.Cells[excelRow, 7]).FormulaR1C1 = "=(R" + excelRow + "C5/R" + excelRow + "C4) - 1";	// %Var Model/NetRev -1

			worksheet.Cells[excelRow, 8] = "=SUM(H" + topRow + ":H" + bottomRow + ")";	// Cost

			worksheet.get_Range(worksheet.Cells[excelRow, 9], worksheet.Cells[excelRow, 9]).FormulaR1C1 = "=R" + excelRow + "C5-R" + excelRow + "C8";	// NI Model-Cost
			worksheet.get_Range(worksheet.Cells[excelRow, 10], worksheet.Cells[excelRow, 10]).FormulaR1C1 = "=R" + excelRow + "C9/R" + excelRow + "C5"; // NI % NI/Model


			this.setRangeComma(worksheet.get_Range(worksheet.Cells[topRow, 2], worksheet.Cells[excelRow, 2]));	// Cases
			this.setRangeCurrency(worksheet.get_Range(worksheet.Cells[topRow, 3], worksheet.Cells[excelRow, 3]));	// Charges
			this.setRangeCurrency(worksheet.get_Range(worksheet.Cells[topRow, 4], worksheet.Cells[excelRow, 4]));	// NetRev
			this.setRangeCurrency(worksheet.get_Range(worksheet.Cells[topRow, 5], worksheet.Cells[excelRow, 5]));	// Model
			this.setRangePercent(worksheet.get_Range(worksheet.Cells[topRow, 6], worksheet.Cells[excelRow, 6]));	// POC
			this.setRangePercent(worksheet.get_Range(worksheet.Cells[topRow, 7], worksheet.Cells[excelRow, 7]));	// %Var
			this.setRangeCurrency(worksheet.get_Range(worksheet.Cells[topRow, 8], worksheet.Cells[excelRow, 8]));	// Cost
			this.setRangeCurrency(worksheet.get_Range(worksheet.Cells[topRow, 9], worksheet.Cells[excelRow, 9]));	// NI
			this.setRangePercent(worksheet.get_Range(worksheet.Cells[topRow, 10], worksheet.Cells[excelRow, 10]));	// NI %

			Excel.Range myRange;
			
			int columnsCount = 10;
			
			myRange = worksheet.get_Range(worksheet.Cells[topRow - 1, 2], worksheet.Cells[excelRow, columnsCount]);

			myRange.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlMedium, Excel.XlColorIndex.xlColorIndexAutomatic, null);
			myRange.Borders[Excel.XlBordersIndex.xlInsideVertical].LineStyle = Excel.XlLineStyle.xlContinuous;

			worksheet.get_Range(worksheet.Cells[excelRow, 2], worksheet.Cells[excelRow, columnsCount]).Borders[Excel.XlBordersIndex.xlEdgeTop].LineStyle = Excel.XlLineStyle.xlDouble;


			myRange = worksheet.get_Range("A1", "Z1");
			myRange.EntireColumn.AutoFit();



			worksheet.get_Range(worksheet.Cells[excelRow + 2, 1], worksheet.Cells[excelRow + 2, 3]).Borders[Excel.XlBordersIndex.xlEdgeTop].LineStyle = Excel.XlLineStyle.xlContinuous;

      
			worksheet.Cells[excelRow + 3, 1] = "Dataset used : " + dataName;
			worksheet.Cells[excelRow + 4, 1] = datePulled;
			worksheet.Cells[excelRow + 5, 1] = dataTimeFrame;

			worksheet.Cells[excelRow + 6, 1] = "Inpatient Charge Inc:" + string.Format("{0:P2}", this.Analysis.InpatientChargeIncrease - 1);
			worksheet.Cells[excelRow + 7, 1] = "Outpatient Charge Inc:" + string.Format("{0:P2}", this.Analysis.OutpatientChargeIncrease - 1);
			worksheet.Cells[excelRow + 8, 1] = "Cost Inc:" + string.Format("{0:P2}", this.Analysis.CostIncrease -1);

			worksheet.Cells[excelRow + 9, 1] = "Filter Company Code:" + this.Analysis.FilterEntity;
			worksheet.Cells[excelRow + 10, 1] = "Filter Insurance Plan Code:" + this.Analysis.FilterInsurancePlanCode;
			// worksheet.Cells[excelRow + 11, 1] = baseComments;

			myRange = 	worksheet.get_Range(worksheet.Cells[topRow, 1], worksheet.Cells[excelRow, columnsCount]);
			
			myRange.Sort(worksheet.Cells[topRow, 1], Excel.XlSortOrder.xlAscending, null, null, Excel.XlSortOrder.xlAscending, null, Excel.XlSortOrder.xlAscending, Excel.XlYesNoGuess.xlNo, null, null, 				Excel.XlSortOrientation.xlSortColumns,
				Excel.XlSortMethod.xlPinYin,
				Excel.XlSortDataOption.xlSortNormal,
				Excel.XlSortDataOption.xlSortNormal,
				Excel.XlSortDataOption.xlSortNormal);

			return worksheet;
		}
			


		#endregion

	}
}
