Imports System.Data
Imports System.Data.SqlClient

Imports RMS_DALObjects
Imports RMS_BusinessObjects


Public Class RateScheduleReportObject

	Dim db As baseDALObject = New baseDALObject
	Dim codesData As CodesDAL
	Dim passThruData As PassThrusDAL
	Dim rateScheduleID As Integer

	Dim codesMngr As CodesManager


#Region "Constructors"

	Sub New(ByVal in_RateScheduleID As Integer)
		rateScheduleID = in_RateScheduleID

		codesData = New CodesDAL

		passThruData = New PassThrusDAL
		passThruData.loadRateSchedulePassThrus(rateScheduleID)
	End Sub


#End Region


#Region "Excel Formatting Functions"

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

#End Region


	Public Sub printRateSchedule()

		Dim report As Excel.Application = New Excel.Application
		Dim workbooks As Excel.Workbooks = report.Workbooks
		Dim workbook As Excel.Workbook = workbooks.Add(Excel.XlWBATemplate.xlWBATWorksheet)
		Dim sheet As Excel.Worksheet
		Dim range As Excel.Range
		Dim drow As DataRow
		Dim odataset As DataSet


		codesMngr = codesData.getCodesManager


		sheet = workbook.ActiveSheet
		With sheet
			.Name = "RateSchedule"
			odataset = db.getDataSet("SELECT * FROM Contrct_RateSched, ContrctID WHERE Contrct_RateSched.ContrctIDNum=ContrctID.ContrctIDNum AND Contrct_RateSched.RateSchedSeqNum=" & rateScheduleID)
			.Cells(1, 1) = "Contract: " & odataset.Tables(0).Rows(0).Item("ContrctIDDescr")
			With .Range(.Cells(1, 1), .Cells(1, 7))
				.Merge()
				.HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft
				.Font.Bold = True
				.Font.Size = 16
			End With

			odataset = db.getDataSet("SELECT * FROM RateSched WHERE RateSchedSeqNum=" & rateScheduleID)
			.Cells(2, 1) = "Rate Schedule: " & odataset.Tables(0).Rows(0).Item("RateSchedName")
			With .Range(.Cells(2, 1), .Cells(2, 7))
				.Merge()
				.HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft
				.Font.Bold = True
				.Font.Size = 14
			End With

			With .PageSetup
				.Orientation = Excel.XlPageOrientation.xlLandscape
				.FitToPagesWide = 1
				.FitToPagesTall = 1
			End With

			.Cells(4, 1) = "Inpatient"
			With .Range(.Cells(4, 1), .Cells(4, 1))
				.Merge()
				.HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft
				.Font.Bold = True
				.Font.Size = 14
			End With

			Dim row As Integer = 6
			Dim startRow As Integer

			row = printRates(sheet, row, "I", "Ignore", "")
			row = printRates(sheet, row, "I", "LessorOf", "")
			row = printRates(sheet, row, "I", "StopLoss", "")
			row = printRates(sheet, row, "I", "Ceiling", "")
			row = printRates(sheet, row, "I", "Floor", "")
			row = printRates(sheet, row, "I", "PerDiem", "")
			row = printRates(sheet, row, "I", "FFS", "CaseRate")
			row = printRates(sheet, row, "I", "FFS", "POC")
			row = printRates(sheet, row, "I", "PassThru", "")

			row = row + 2
			.Cells(row, 1) = "Outpatient"
			With .Range(.Cells(row, 1), .Cells(row, 1))
				.Merge()
				.HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft
				.Font.Bold = True
				.Font.Size = 14
			End With
			row = row + 2

			row = printRates(sheet, row, "O", "Ignore", "")
			row = printRates(sheet, row, "O", "LessorOf", "")
			row = printRates(sheet, row, "O", "StopLoss", "")
			row = printRates(sheet, row, "O", "Ceiling", "")
			row = printRates(sheet, row, "O", "Floor", "")
			row = printRates(sheet, row, "O", "FFS", "CaseRate")
			row = printRates(sheet, row, "O", "FFS", "POC")
			row = printRates(sheet, row, "O", "PassThru", "")

			.Columns("A:A").EntireColumn.AutoFit()
			.Columns("B:B").ColumnWidth = 40
			.Columns("C:C").ColumnWidth = 20
			.Columns("D:D").ColumnWidth = 15
			.Columns("E:E").ColumnWidth = 17

			Dim k = 1
			For k = 1 To row
				.Rows(k & ":" & k).EntireRow.AutoFit()
			Next

			.PageSetup.Orientation = Excel.XlPageOrientation.xlPortrait
			.PageSetup.Zoom = False
			.PageSetup.FitToPagesTall = 1
			.PageSetup.FitToPagesWide = 1
		End With

		report.Visible = True
	End Sub

	Function printRates(ByVal sheet As Excel.Worksheet, ByVal startRow As Integer, ByVal inout As String, ByVal rateCategory As String, ByVal rateType As String) As Integer
		'This function when passed the given parameters will insert and format a given rate section and then return the last row printed on the worksheet
		Dim odataset As DataSet
		Dim dRow As DataRow
		Dim row As Integer = startRow
		Dim col As Integer

		If rateType = "" Then
			odataset = db.getDataSet("Select * From Rate Where RateCatgryDescr='" & rateCategory & "' and InOutPatientInd='" & inout & "' and rateSchedSeqNum=" & rateScheduleID)
		Else
			odataset = db.getDataSet("Select * From Rate Where RateCatgryDescr='" & rateCategory & "' and RateTypeDescr='" & rateType & "' AND InOutPatientInd='" & inout & "' and rateSchedSeqNum=" & rateScheduleID)
		End If

		If odataset.Tables(0).Rows.Count = 0 Then
			Return startRow

		Else

			With sheet
				If rateType = "" Then
					.Cells(row, 1) = rateCategory
				Else
					.Cells(row, 1) = rateCategory & " - " & rateType
				End If

				With .Cells(row, 1)
					.Font.Bold = True
					.Font.Size = 10
					.Font.Underline = True
				End With

				row = row + 1

				.Cells(row, 1) = "Description"
				.Cells(row, 2) = "Code"
				col = 3
				Select Case rateCategory
					Case "StopLoss"
						.Cells(row, col) = "Type"
						col = col + 1
						.Cells(row, col) = "Threshold"
						col = col + 1
						.Cells(row, col) = "Rate"
						col = col + 1

					Case "PerDiem"
						.Cells(row, col) = "Per Diem Payment"
						col = col + 1
					Case "FFS"

						If inout = "I" And rateType = "CaseRate" Then
							.Cells(row, col) = "Per Case Payment"
							col = col + 1
							.Cells(row, col) = "Per Case Payment Day Threshold"
							col = col + 1
							.Cells(row, col) = "Per Diem Payment Following Day Threshold"
							col = col + 1
						Else
							.Cells(row, col) = "Rate"
							col = col + 1
						End If

						.Cells(row, col) = "Pass Thrus"
						col = col + 1

					Case "PassThru"
						.Cells(row, col) = "Type"
						col = col + 1
						.Cells(row, col) = "Rate"
						col = col + 1
				End Select

				With .Range(.Cells(row, 1), .Cells(row, col - 1))
					.Font.Bold = True
					.BorderAround(Excel.XlLineStyle.xlContinuous)
					.Borders(Excel.XlBordersIndex.xlInsideVertical).LineStyle = Excel.XlLineStyle.xlContinuous
					.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter
					.Interior.ColorIndex = 15
					.Interior.Pattern = Excel.XlPattern.xlPatternSolid
					.WrapText = True
				End With

				row = row + 1

				Dim codes As CodesBO
				Dim passThru As PassThrusBO

				For Each dRow In odataset.Tables(0).Rows

					With sheet
						.Cells(row, 1) = dRow.Item("RateName")

						codes = codesData.getCodes(codesMngr, dRow.Item("RateSeqNum"))

						.Cells(row, 2) = codes.ToString()

						Select Case dRow.Item("RateCatgryDescr")
							Case "LessorOf"

							Case "StopLoss"
								.Cells(row, 3) = dRow.Item("RateTypeDescr")
								.Cells(row, 4) = dRow.Item("RateValue")
								setRangeCurrency(.Cells(row, 4))

								.Cells(row, 5) = dRow.Item("ThreshldNum")
								setRangePercent(.Cells(row, 5))

								If dRow.Item("AddtnlDayRate") > 0 Then
									.Cells(row, 6) = dRow.Item("AddtnlDayRate")
									setRangeCurrency(.Cells(row, 6))
								End If

							Case "PerDiem"
								.Cells(row, 3) = dRow.Item("RateValue")
								setRangeCurrency(.Cells(row, 3))

							Case "FFS"
								Select Case dRow.Item("RateTypeDescr")
									Case "CaseRate"
										.Cells(row, 3) = dRow.Item("RateValue")
										setRangeCurrency(.Cells(row, 3))

										If inout = "I" And dRow.Item("RateTypeDescr") = "CaseRate" Then
											.Cells(row, 4) = dRow.Item("LOSNum")
											.Cells(row, 4).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter
											.Cells(row, 5).HorizontalAlignment = Excel.XlHAlign.xlHAlignRight
											If dRow.Item("AddtnlDayRate") > 0 Then
												.Cells(row, 5) = String.Format("{0:C0}", dRow.Item("AddtnlDayRate")) & " per Day"
											Else
												.Cells(row, 5) = "Applicable Per Diem"
											End If

											passThru = passThruData.getPassThrus(dRow.Item("RateSeqNum"))
											.Cells(row, 6) = passThru.ToString()
										Else

										End If

									Case "POC"
										.Cells(row, 3) = dRow.Item("RateValue")
										setRangePercent(.Cells(row, 3))

										passThru = passThruData.getPassThrus(dRow.Item("RateSeqNum"))
										.Cells(row, 4) = passThru.ToString()

								End Select

							Case "PassThru"
								.Cells(row, 3) = dRow.Item("RateTypeDescr")
								.Cells(row, 4) = dRow.Item("RateValue")

								If dRow.Item("RateValue") > 1 Then
									setRangeCurrency(.Cells(row, 4))
								Else
									setRangePercent(.Cells(row, 4))
								End If
						End Select
					End With

					With .Range(.Cells(row, 1), .Cells(row, col - 1))
						.BorderAround(Excel.XlLineStyle.xlContinuous)
						.Borders(Excel.XlBordersIndex.xlInsideVertical).LineStyle = Excel.XlLineStyle.xlContinuous
						.WrapText = True
					End With

					row = row + 1
				Next

			End With

			Return row + 2
		End If

	End Function



End Class
