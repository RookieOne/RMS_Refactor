Imports System.Data
Imports System.Data.SqlClient

Imports RMS_DALObjects
Imports RMS_BusinessObjects

Public Class AnalysisReportObject

#Region "Constructors"

	Sub AnalysisReportObject()

	End Sub

#End Region

#Region "Excel Formating Functions"

	Sub setRangeCurrency(ByVal range As Excel.Range)
		With range
			.Style = "Currency"
			.NumberFormat = "_($* #,##0_);_($* (#,##0);_($* ""-""??_);_(@_)"
		End With
	End Sub

	Sub setRangeComma(ByVal range As Excel.Range)
		With range
			.Style = "Comma"
			.NumberFormat = "_(* #,##0_);_(* (#,##0);_(* ""-""??_);_(@_)"
		End With
	End Sub

	Sub setRangePercent(ByVal range As Excel.Range)
		With range
			.Style = "Percent"
			.NumberFormat = "0.00%"
		End With
	End Sub

	Sub setPageSetup(ByRef sheet As Excel.Worksheet, ByVal pageNum As String)
		With sheet.PageSetup
			.LeftFooter = "THR Confidential"
			.CenterFooter = Today
			.RightFooter = "Page " & pageNum
			.Orientation = Excel.XlPageOrientation.xlLandscape
			.FitToPagesWide = 1
			.FitToPagesTall = 1
		End With
	End Sub

	Sub setSheetTitle(ByVal sheet As Excel.Worksheet, ByVal sheetTitle As String)
		With sheet
			.Name = sheetTitle
			.Cells(1, 1) = analysis.ContractTitle & " " & sheetTitle
			With .Range(.Cells(1, 1), .Cells(1, 10))
				.Merge()
				.HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft
				.Font.Bold = True
				.Font.Size = 12
			End With

			.Cells(2, 1) = analysis.RateSchedulesAnalyzed
			With .Cells(2, 1)
				.HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft
				.Font.Bold = True
				.Font.Size = 10
			End With
		End With
	End Sub

	Sub setFooter(ByVal sheet As Excel.Worksheet, ByVal columnsCount As Integer, ByVal excelRow As Integer)
		With sheet
			Dim k As Integer
			For k = 1 To columnsCount
				.Columns(k).EntireColumn.AutoFit()
			Next

			With .Range(.Cells(excelRow + 2, 1), .Cells(excelRow + 2, 3))
				.Borders(Excel.XlBordersIndex.xlEdgeTop).LineStyle = Excel.XlLineStyle.xlContinuous
			End With

			.Cells(excelRow + 3, 1) = "Dataset used : " & dataName
			.Cells(excelRow + 4, 1) = datePulled
			.Cells(excelRow + 5, 1) = dataTimeFrame

			Dim inChg As Double = analysis.InpatientChargeIncrease - 1
			Dim outChg As Double = analysis.OutpatientChargeIncrease - 1
			Dim cost As Double = analysis.CostIncrease - 1
			.Cells(excelRow + 6, 1) = "Inpatient Charge Inc:" & String.Format("{0:P2}", inChg)
			.Cells(excelRow + 7, 1) = "Outpatient Charge Inc:" & String.Format("{0:P2}", outChg)
			.Cells(excelRow + 8, 1) = "Cost Inc:" & String.Format("{0:P2}", cost)
			.Cells(excelRow + 9, 1) = "Filter Company Code:" & analysis.FilterEntity
			.Cells(excelRow + 10, 1) = "Filter Insurance Plan Code:" & analysis.FilterInsurancePlanCode
			'.Cells(excelRow + 11, 1) = baseComments
		End With
	End Sub

	Sub setColumnTitleFormat(ByVal range As Excel.Range)
		With range
			.BorderAround(Excel.XlLineStyle.xlContinuous)
			.Borders(Excel.XlBordersIndex.xlInsideVertical).LineStyle = Excel.XlLineStyle.xlContinuous
			.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter
			.Interior.ColorIndex = 15
			.Interior.Pattern = Excel.XlPattern.xlPatternSolid
		End With
	End Sub

#End Region


#Region "Analysis Report"

	Dim data As baseDALObject = New baseDALObject
	Dim analysis As AnalysisBO

	Dim datePulled As String
	Dim dataTimeFrame As String
	Dim dataName As String



	Public Sub createReport(ByVal in_Analysis As AnalysisBO, ByVal filePath As String)

		analysis = in_Analysis

		Dim sqlDataRdr As SqlDataReader = data.getDataReader("SELECT * FROM DataSet WHERE DatasetSeqNum=" & analysis.DataSetID)

		If sqlDataRdr.Read() Then

			datePulled = "Data pulled from EG : " & sqlDataRdr("PulledDate")
			dataTimeFrame = String.Format("{0:MMM-yy}", sqlDataRdr("startDate")) & " thru " & String.Format("{0:MMM-yy}", sqlDataRdr("endDate"))
			dataName = sqlDataRdr("DatasetName")

		End If


		Dim report As Excel.Application = New Excel.Application
		Dim workbooks As Excel.Workbooks = report.Workbooks
		Dim workbook As Excel.Workbook = workbooks.Add(Excel.XlWBATemplate.xlWBATWorksheet)
		Dim summarySheet, DRGSheet, outpatientSheet, hospitalSheet, rateBreakdownSheet As Excel.Worksheet
		Dim range As Excel.Range

		'**** SUMMARY
		summarySheet = workbook.ActiveSheet
		fillAnalysisSheet(summarySheet, analysis.getPatientTypeSummaryTable(), "Summary", 1)


		'**** INPATIENT DRG BREAKDOWN
		DRGSheet = workbook.Sheets.Add
		fillAnalysisSheet(DRGSheet, analysis.getDRGTypeSummaryTable(), "DRG Type Summary", 2)

		'**** OUTPATIENT BREAKDOWN
		outpatientSheet = workbook.Sheets.Add
		fillAnalysisSheet(outpatientSheet, analysis.getOutpatientSummaryTable(), "Outpatient Summary", 3)

		'**** HOSPITAL BREAKDOWN
		hospitalSheet = workbook.Sheets.Add
		fillAnalysisSheet(hospitalSheet, analysis.getHospitalSummaryTable(), "Hospital Summary", 4)

		'**** RATE BREAKDOWN
		rateBreakdownSheet = workbook.Sheets.Add
		fillRateBreakdownSheet(rateBreakdownSheet, 5)

		report.Visible = True

		hospitalSheet.Move(rateBreakdownSheet)
		outpatientSheet.Move(hospitalSheet)
		DRGSheet.Move(outpatientSheet)
		summarySheet.Move(DRGSheet)

		Dim saveFile As String = filePath & "\" & analysis.ContractTitle & " using " & dataName & ".xls"
		If System.IO.File.Exists(saveFile) Then System.IO.File.Delete(saveFile)
		workbook.SaveAs(saveFile)

		report.Visible = True
		'report.Quit()
	End Sub



	Sub fillAnalysisRow(ByVal rowToFill As DataRow, ByVal rowWithData As DataRow)
		With rowToFill
			.Item("Cases") = .Item("Cases") + 1
			.Item("Model") = .Item("Model") + rowWithData.Item("Model")
			.Item("NetRev") = .Item("NetRev") + rowWithData.Item("NetRev")
			.Item("Charges") = .Item("Charges") + rowWithData.Item("Charges")
			.Item("Cost") = .Item("Cost") + rowWithData.Item("Cost")
			.Item("passThruModel") = .Item("passThruModel") + rowWithData.Item("passThruModel")
		End With
	End Sub

	Sub fillAnalysisSheet(ByVal sheet As Excel.Worksheet, ByVal dTable As DataTable, ByVal sheetTitle As String, ByVal pageNum As String)
		Dim excelRow, k As Integer
		Dim dRow As DataRow
		Dim columnsCount As Integer = 10


		With sheet
			setPageSetup(sheet, pageNum)
			setSheetTitle(sheet, sheetTitle)

			excelRow = 3
			Dim topRow As Integer = excelRow + 1
			.Cells(excelRow, 2) = "Cases"
			.Cells(excelRow, 3) = "Charges"
			.Cells(excelRow, 4) = "NetRev"
			.Cells(excelRow, 5) = "Model"
			.Cells(excelRow, 6) = "POC"
			.Cells(excelRow, 7) = "% Var"
			.Cells(excelRow, 8) = "Cost"
			.Cells(excelRow, 9) = "NI"
			.Cells(excelRow, 10) = "NI %"

			setColumnTitleFormat(.Range(.Cells(excelRow, 2), .Cells(excelRow, columnsCount)))

			excelRow = excelRow + 1
			For Each dRow In dTable.Rows
				.Cells(excelRow, 1) = dRow.Item("Title")
				.Cells(excelRow, 2) = dRow.Item("Cases")
				.Cells(excelRow, 3) = dRow.Item("Charges")
				.Cells(excelRow, 4) = dRow.Item("NetRev")
				.Cells(excelRow, 5) = dRow.Item("Model")
				.Cells(excelRow, 6) = dRow.Item("Model") / dRow.Item("Charges")
				.Cells(excelRow, 7) = (dRow.Item("Model") / dRow.Item("NetRev")) - 1
				.Cells(excelRow, 8) = dRow.Item("Cost")
				.Cells(excelRow, 9) = dRow.Item("Model") - dRow.Item("Cost")
				.Cells(excelRow, 10) = (dRow.Item("Model") - dRow.Item("Cost")) / dRow.Item("Model")
				excelRow = excelRow + 1
			Next

			.Cells(excelRow, 2) = "=SUM(B" & topRow & ":B" & excelRow - 1 & ")"			 'Cases
			.Cells(excelRow, 3) = "=SUM(C" & topRow & ":C" & excelRow - 1 & ")"			 'Charges
			.Cells(excelRow, 4) = "=SUM(D" & topRow & ":D" & excelRow - 1 & ")"			 'NetRev
			.Cells(excelRow, 5) = "=SUM(E" & topRow & ":E" & excelRow - 1 & ")"			 'Model
			.Range(.Cells(excelRow, 6), .Cells(excelRow, 6)).FormulaR1C1 = "=R" & excelRow & "C5/R" & excelRow & "C3"			 ' POC Model/Charges
			.Range(.Cells(excelRow, 7), .Cells(excelRow, 7)).FormulaR1C1 = "=(R" & excelRow & "C5/R" & excelRow & "C4) - 1"			 ' %Var Model/NetRev -1
			.Cells(excelRow, 8) = "=SUM(H" & topRow & ":H" & excelRow - 1 & ")"			 'Cost
			.Range(.Cells(excelRow, 9), .Cells(excelRow, 9)).FormulaR1C1 = "=R" & excelRow & "C5-R" & excelRow & "C8"			 ' NI Model-Cost
			.Range(.Cells(excelRow, 10), .Cells(excelRow, 10)).FormulaR1C1 = "=R" & excelRow & "C9/R" & excelRow & "C5"			 ' NI % NI/Model

			setRangeComma(.Range(.Cells(topRow, 2), .Cells(excelRow, 2)))			 'Cases
			setRangeCurrency(.Range(.Cells(topRow, 3), .Cells(excelRow, 3)))			 'Charges
			setRangeCurrency(.Range(.Cells(topRow, 4), .Cells(excelRow, 4)))			 'NetRev
			setRangeCurrency(.Range(.Cells(topRow, 5), .Cells(excelRow, 5)))			 'Model
			setRangePercent(.Range(.Cells(topRow, 6), .Cells(excelRow, 6)))			 'POC
			setRangePercent(.Range(.Cells(topRow, 7), .Cells(excelRow, 7)))			 '%Var
			setRangeCurrency(.Range(.Cells(topRow, 8), .Cells(excelRow, 8)))			 'Cost
			setRangeCurrency(.Range(.Cells(topRow, 9), .Cells(excelRow, 9)))			 'NI
			setRangePercent(.Range(.Cells(topRow, 10), .Cells(excelRow, 10)))			 'NI %

			With .Range(.Cells(topRow - 1, 2), .Cells(excelRow, columnsCount))
				.BorderAround(Excel.XlLineStyle.xlContinuous)
				.Borders(Excel.XlBordersIndex.xlInsideVertical).LineStyle = Excel.XlLineStyle.xlContinuous
			End With
			.Range(.Cells(excelRow, 2), .Cells(excelRow, columnsCount)).Borders(Excel.XlBordersIndex.xlEdgeTop).LineStyle = Excel.XlLineStyle.xlDouble

			With .Range(.Cells(excelRow + 2, 1), .Cells(excelRow + 2, 3))
				.Borders(Excel.XlBordersIndex.xlEdgeTop).LineStyle = Excel.XlLineStyle.xlContinuous
			End With

			setFooter(sheet, columnsCount, excelRow)

			.Range(sheet.Cells(topRow, 1), sheet.Cells(excelRow, columnsCount)).Sort(sheet.Cells(topRow, 1), Excel.XlSortOrder.xlAscending, , , , , , , , , Excel.XlSortOrientation.xlSortColumns)
		End With
	End Sub


#Region "RATE BREAKDOWN"


	Sub fillRateBreakdownSheet(ByVal sheet As Excel.Worksheet, ByVal pageNum As String)

		Dim excelRow, k As Integer
		Dim columnsCount As Integer = 10

		excelRow = 3

		With sheet
			setPageSetup(sheet, pageNum)
			setSheetTitle(sheet, "Rate Breakdown")

			printRateBreakdown(sheet, analysis.getRateBreakDownTable("I"), "Inpatient Rate Breakdown", excelRow)
			printRateBreakdown(sheet, analysis.getRateBreakDownTable("O"), "Outpatient Rate Breakdown", excelRow)

			setFooter(sheet, columnsCount, excelRow)
		End With
	End Sub

	Sub printRateBreakdown(ByVal sheet As Excel.Worksheet, ByVal dTable As DataTable, ByVal tableTitle As String, ByRef excelRow As Integer)
		Dim topRow As Integer = excelRow + 2
		Const columnsCount = 12



		With sheet
			.Cells(excelRow, 1) = tableTitle

			excelRow = excelRow + 1

			.Cells(excelRow, 1) = "Rate Category"
			.Cells(excelRow, 2) = "Rate Type"
			.Cells(excelRow, 3) = "Rate Name"
			.Cells(excelRow, 4) = "Cases"
			.Cells(excelRow, 5) = "Charges"
			.Cells(excelRow, 6) = "NetRev"
			.Cells(excelRow, 7) = "Model"
			.Cells(excelRow, 8) = "POC"
			.Cells(excelRow, 9) = "% Var"
			.Cells(excelRow, 10) = "Cost"
			.Cells(excelRow, 11) = "NI"
			.Cells(excelRow, 12) = "NI %"

			setColumnTitleFormat(.Range(.Cells(excelRow, 1), .Cells(excelRow, columnsCount)))

			Dim dRow As DataRow

			excelRow = excelRow + 1
			For Each dRow In dTable.Rows
				.Cells(excelRow, 1) = dRow.Item("RateCategory")
				.Cells(excelRow, 2) = dRow.Item("RateType")
				.Cells(excelRow, 3) = dRow.Item("RateName")
				.Cells(excelRow, 4) = dRow.Item("Cases")
				.Cells(excelRow, 5) = dRow.Item("Charges")
				.Cells(excelRow, 6) = dRow.Item("NetRev")
				.Cells(excelRow, 7) = dRow.Item("Model")
				.Cells(excelRow, 8) = dRow.Item("Model") / dRow.Item("Charges")
				.Cells(excelRow, 9) = (dRow.Item("Model") / dRow.Item("NetRev")) - 1
				.Cells(excelRow, 10) = dRow.Item("Cost")
				.Cells(excelRow, 11) = dRow.Item("Model") - dRow.Item("Cost")
				.Cells(excelRow, 12) = (dRow.Item("Model") - dRow.Item("Cost")) / dRow.Item("Model")
				excelRow = excelRow + 1
			Next


			.Cells(excelRow, 4) = "=SUM(D" & topRow & ":D" & excelRow - 1 & ")"			 'Cases
			.Cells(excelRow, 5) = "=SUM(E" & topRow & ":E" & excelRow - 1 & ")"			 'Charges
			.Cells(excelRow, 6) = "=SUM(F" & topRow & ":F" & excelRow - 1 & ")"			 'NetRev
			.Cells(excelRow, 7) = "=SUM(G" & topRow & ":G" & excelRow - 1 & ")"			 'Model
			.Range(.Cells(excelRow, 8), .Cells(excelRow, 8)).FormulaR1C1 = "=R" & excelRow & "C7/R" & excelRow & "C5"		 ' POC Model/Charges
			.Range(.Cells(excelRow, 9), .Cells(excelRow, 9)).FormulaR1C1 = "=(R" & excelRow & "C7/R" & excelRow & "C6) - 1"		 ' %Var Model/NetRev -1
			.Cells(excelRow, 10) = "=SUM(J" & topRow & ":J" & excelRow - 1 & ")"			 'Cost
			.Range(.Cells(excelRow, 11), .Cells(excelRow, 11)).FormulaR1C1 = "=R" & excelRow & "C7-R" & excelRow & "C10"			 ' NI Model-Cost
			.Range(.Cells(excelRow, 12), .Cells(excelRow, 12)).FormulaR1C1 = "=R" & excelRow & "C11/R" & excelRow & "C7"			 ' NI % NI/Model

			setRangeComma(.Range(.Cells(topRow, 4), .Cells(excelRow, 4)))			 'Cases
			setRangeCurrency(.Range(.Cells(topRow, 5), .Cells(excelRow, 5)))			 'Charges
			setRangeCurrency(.Range(.Cells(topRow, 6), .Cells(excelRow, 6)))			 'NetRev
			setRangeCurrency(.Range(.Cells(topRow, 7), .Cells(excelRow, 7)))			 'Model
			setRangePercent(.Range(.Cells(topRow, 8), .Cells(excelRow, 8)))			 'POC
			setRangePercent(.Range(.Cells(topRow, 9), .Cells(excelRow, 9)))			 '%Var
			setRangeCurrency(.Range(.Cells(topRow, 10), .Cells(excelRow, 10)))			 'Cost
			setRangeCurrency(.Range(.Cells(topRow, 11), .Cells(excelRow, 11)))			 'NI
			setRangePercent(.Range(.Cells(topRow, 12), .Cells(excelRow, 12)))			 'NI %

			With .Range(.Cells(topRow, 1), .Cells(excelRow, columnsCount))
				.BorderAround(Excel.XlLineStyle.xlContinuous)
				.Borders(Excel.XlBordersIndex.xlInsideVertical).LineStyle = Excel.XlLineStyle.xlContinuous
			End With
			.Range(.Cells(excelRow, 1), .Cells(excelRow, columnsCount)).Borders(Excel.XlBordersIndex.xlEdgeTop).LineStyle = Excel.XlLineStyle.xlDouble

			.Range(sheet.Cells(topRow, 1), sheet.Cells(excelRow, columnsCount)).Sort(sheet.Cells(topRow, 1), Excel.XlSortOrder.xlAscending, , , , , , , , , Excel.XlSortOrientation.xlSortColumns)
			'.Range(sheet.Cells(topRow, 1), sheet.Cells(excelRow, columnsCount)).Sort(sheet.Cells(topRow, 1), Excel.XlSortOrder.xlAscending, sheet.Cells(topRow, 2), , Excel.XlSortOrder.xlAscending, sheet.Cells(topRow, 3), Excel.XlSortOrder.xlAscending, , , , , Excel.XlSortOrientation.xlSortColumns)
			excelRow = excelRow + 3
		End With
	End Sub

	Sub printPassThruRateBreakdown(ByVal dTable As DataTable, ByVal tableTitle As String, ByVal sheet As Excel.Worksheet, ByRef excelRow As Double, ByVal totalModel As Double, ByVal totalNetRev As Double)
		Dim topRow As Integer = excelRow + 2
		Const columnsCount = 8

		With sheet
			.Cells(excelRow, 1) = tableTitle

			excelRow = excelRow + 1

			.Cells(excelRow, 1) = "Rate Name"
			.Cells(excelRow, 2) = "Rate Category"
			.Cells(excelRow, 3) = "Rate Type"
			.Cells(excelRow, 4) = "Cases"
			.Cells(excelRow, 5) = "Charges"
			.Cells(excelRow, 6) = "Model"
			.Cells(excelRow, 7) = "Model POC"
			.Cells(excelRow, 8) = "Model %"

			setColumnTitleFormat(.Range(.Cells(excelRow, 1), .Cells(excelRow, columnsCount)))

			Dim dRow As DataRow

			excelRow = excelRow + 1
			For Each dRow In dTable.Rows
				.Cells(excelRow, 1) = dRow.Item("RateName")
				.Cells(excelRow, 2) = dRow.Item("RateCategory")
				.Cells(excelRow, 3) = dRow.Item("RateType")
				.Cells(excelRow, 4) = dRow.Item("Cases")
				.Cells(excelRow, 5) = dRow.Item("Charges")
				.Cells(excelRow, 6) = dRow.Item("Model")
				.Cells(excelRow, 7) = dRow.Item("Model") / dRow.Item("Charges")
				.Cells(excelRow, 8) = dRow.Item("Model") / totalModel
				excelRow = excelRow + 1
			Next

			.Cells(excelRow, 4) = "=SUM(D" & topRow & ":D" & excelRow - 1 & ")"			 'Cases
			.Cells(excelRow, 5) = "=SUM(E" & topRow & ":E" & excelRow - 1 & ")"			 'Charges
			.Cells(excelRow, 6) = "=SUM(F" & topRow & ":F" & excelRow - 1 & ")"			 'Model
			.Range(.Cells(excelRow, 7), .Cells(excelRow, 7)).FormulaR1C1 = "=R" & excelRow & "C6/R" & excelRow & "C5"			 'Model POC
			.Cells(excelRow, 8) = "=SUM(K" & topRow & ":H" & excelRow - 1 & ")"			 'Model %

			setRangeComma(.Range(.Cells(topRow, 4), .Cells(excelRow, 4)))			 'Cases
			setRangeCurrency(.Range(.Cells(topRow, 5), .Cells(excelRow, 5)))			 'Charges
			setRangeCurrency(.Range(.Cells(topRow, 6), .Cells(excelRow, 6)))			 'Model
			setRangePercent(.Range(.Cells(topRow, 7), .Cells(excelRow, 7)))			 'Model POC
			setRangePercent(.Range(.Cells(topRow, 8), .Cells(excelRow, 8)))			 'Model %

			With .Range(.Cells(topRow, 1), .Cells(excelRow, columnsCount))
				.BorderAround(Excel.XlLineStyle.xlContinuous)
				.Borders(Excel.XlBordersIndex.xlInsideVertical).LineStyle = Excel.XlLineStyle.xlContinuous
			End With
			.Range(.Cells(excelRow, 1), .Cells(excelRow, columnsCount)).Borders(Excel.XlBordersIndex.xlEdgeTop).LineStyle = Excel.XlLineStyle.xlDouble

			.Range(sheet.Cells(topRow, 1), sheet.Cells(excelRow, columnsCount)).Sort(sheet.Cells(topRow, 1), Excel.XlSortOrder.xlAscending, , , , , , , , , Excel.XlSortOrientation.xlSortColumns)
			excelRow = excelRow + 3
		End With
	End Sub

#End Region

#End Region


End Class
