Option Strict On

Imports CGridTestProject.KnowDotNet.KDNGrid
Imports System.Data.OleDb

Public Class Form1
   Inherits System.Windows.Forms.Form

#Region " Windows Form Designer generated code "

   Public Sub New()
      MyBase.New()

      'This call is required by the Windows Form Designer.
      InitializeComponent()

      'Add any initialization after the InitializeComponent() call

   End Sub

   'Form overrides dispose to clean up the component list.
   Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
      If disposing Then
         If Not (components Is Nothing) Then
            components.Dispose()
         End If
      End If
      MyBase.Dispose(disposing)
   End Sub

   'Required by the Windows Form Designer
   Private components As System.ComponentModel.IContainer

   'NOTE: The following procedure is required by the Windows Form Designer
   'It can be modified using the Windows Form Designer.  
   'Do not modify it using the code editor.
   Friend WithEvents DataGrid1 As System.Windows.Forms.DataGrid
   Friend WithEvents btnFillGrid As System.Windows.Forms.Button
   Friend WithEvents btnClose As System.Windows.Forms.Button
   Friend WithEvents LinkLabel1 As System.Windows.Forms.LinkLabel
   Friend WithEvents lblRowClicked As System.Windows.Forms.Label
   Friend WithEvents Label1 As System.Windows.Forms.Label
   Friend WithEvents Label2 As System.Windows.Forms.Label
   Friend WithEvents lblCellClicked As System.Windows.Forms.Label
   Friend WithEvents Label3 As System.Windows.Forms.Label
   Friend WithEvents lblValue As System.Windows.Forms.Label
   Friend WithEvents btnFillGridFromDB As System.Windows.Forms.Button
   Friend WithEvents chkMultiLineText As System.Windows.Forms.CheckBox
   <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
      Me.DataGrid1 = New System.Windows.Forms.DataGrid
      Me.btnFillGrid = New System.Windows.Forms.Button
      Me.btnClose = New System.Windows.Forms.Button
      Me.btnFillGridFromDB = New System.Windows.Forms.Button
      Me.chkMultiLineText = New System.Windows.Forms.CheckBox
      Me.LinkLabel1 = New System.Windows.Forms.LinkLabel
      Me.lblRowClicked = New System.Windows.Forms.Label
      Me.Label1 = New System.Windows.Forms.Label
      Me.Label2 = New System.Windows.Forms.Label
      Me.lblCellClicked = New System.Windows.Forms.Label
      Me.Label3 = New System.Windows.Forms.Label
      Me.lblValue = New System.Windows.Forms.Label
      CType(Me.DataGrid1, System.ComponentModel.ISupportInitialize).BeginInit()
      Me.SuspendLayout()
      '
      'DataGrid1
      '
      Me.DataGrid1.AlternatingBackColor = System.Drawing.Color.Green
      Me.DataGrid1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                  Or System.Windows.Forms.AnchorStyles.Left) _
                  Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
      Me.DataGrid1.DataMember = ""
      Me.DataGrid1.HeaderForeColor = System.Drawing.SystemColors.ControlText
      Me.DataGrid1.Location = New System.Drawing.Point(8, 8)
      Me.DataGrid1.Name = "DataGrid1"
      Me.DataGrid1.Size = New System.Drawing.Size(584, 200)
      Me.DataGrid1.TabIndex = 0
      '
      'btnFillGrid
      '
      Me.btnFillGrid.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
      Me.btnFillGrid.Location = New System.Drawing.Point(360, 248)
      Me.btnFillGrid.Name = "btnFillGrid"
      Me.btnFillGrid.Size = New System.Drawing.Size(161, 33)
      Me.btnFillGrid.TabIndex = 4
      Me.btnFillGrid.Text = "Fill Grid From Memory Data"
      '
      'btnClose
      '
      Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
      Me.btnClose.Location = New System.Drawing.Point(528, 248)
      Me.btnClose.Name = "btnClose"
      Me.btnClose.Size = New System.Drawing.Size(64, 34)
      Me.btnClose.TabIndex = 5
      Me.btnClose.Text = "Close"
      '
      'btnFillGridFromDB
      '
      Me.btnFillGridFromDB.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
      Me.btnFillGridFromDB.Location = New System.Drawing.Point(240, 248)
      Me.btnFillGridFromDB.Name = "btnFillGridFromDB"
      Me.btnFillGridFromDB.Size = New System.Drawing.Size(112, 33)
      Me.btnFillGridFromDB.TabIndex = 3
      Me.btnFillGridFromDB.Text = "Database Fill Grid"
      '
      'chkMultiLineText
      '
      Me.chkMultiLineText.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
      Me.chkMultiLineText.Location = New System.Drawing.Point(16, 210)
      Me.chkMultiLineText.Name = "chkMultiLineText"
      Me.chkMultiLineText.Size = New System.Drawing.Size(96, 32)
      Me.chkMultiLineText.TabIndex = 1
      Me.chkMultiLineText.Text = "Use MultiLine Text Column"
      '
      'LinkLabel1
      '
      Me.LinkLabel1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
      Me.LinkLabel1.LinkBehavior = System.Windows.Forms.LinkBehavior.AlwaysUnderline
      Me.LinkLabel1.Location = New System.Drawing.Point(16, 248)
      Me.LinkLabel1.Name = "LinkLabel1"
      Me.LinkLabel1.Size = New System.Drawing.Size(120, 32)
      Me.LinkLabel1.TabIndex = 2
      Me.LinkLabel1.TabStop = True
      Me.LinkLabel1.Text = "To Make a Donation Go to KnowDotNet"
      '
      'lblRowClicked
      '
      Me.lblRowClicked.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
      Me.lblRowClicked.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
      Me.lblRowClicked.Location = New System.Drawing.Point(186, 215)
      Me.lblRowClicked.Name = "lblRowClicked"
      Me.lblRowClicked.Size = New System.Drawing.Size(40, 20)
      Me.lblRowClicked.TabIndex = 11
      Me.lblRowClicked.TextAlign = System.Drawing.ContentAlignment.MiddleRight
      '
      'Label1
      '
      Me.Label1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
      Me.Label1.AutoSize = True
      Me.Label1.Location = New System.Drawing.Point(114, 218)
      Me.Label1.Name = "Label1"
      Me.Label1.Size = New System.Drawing.Size(67, 16)
      Me.Label1.TabIndex = 10
      Me.Label1.Text = "Row Clicked"
      '
      'Label2
      '
      Me.Label2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
      Me.Label2.AutoSize = True
      Me.Label2.Location = New System.Drawing.Point(242, 218)
      Me.Label2.Name = "Label2"
      Me.Label2.Size = New System.Drawing.Size(64, 16)
      Me.Label2.TabIndex = 0
      Me.Label2.Text = "Cell Clicked"
      '
      'lblCellClicked
      '
      Me.lblCellClicked.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
      Me.lblCellClicked.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
      Me.lblCellClicked.Location = New System.Drawing.Point(314, 214)
      Me.lblCellClicked.Name = "lblCellClicked"
      Me.lblCellClicked.Size = New System.Drawing.Size(83, 20)
      Me.lblCellClicked.TabIndex = 1
      Me.lblCellClicked.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
      '
      'Label3
      '
      Me.Label3.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
      Me.Label3.AutoSize = True
      Me.Label3.Location = New System.Drawing.Point(400, 218)
      Me.Label3.Name = "Label3"
      Me.Label3.Size = New System.Drawing.Size(33, 16)
      Me.Label3.TabIndex = 2
      Me.Label3.Text = "Value"
      '
      'lblValue
      '
      Me.lblValue.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
      Me.lblValue.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
      Me.lblValue.Location = New System.Drawing.Point(440, 214)
      Me.lblValue.Name = "lblValue"
      Me.lblValue.Size = New System.Drawing.Size(144, 19)
      Me.lblValue.TabIndex = 3
      Me.lblValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
      '
      'Form1
      '
      Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
      Me.ClientSize = New System.Drawing.Size(600, 285)
      Me.Controls.Add(Me.lblValue)
      Me.Controls.Add(Me.Label3)
      Me.Controls.Add(Me.Label2)
      Me.Controls.Add(Me.Label1)
      Me.Controls.Add(Me.lblCellClicked)
      Me.Controls.Add(Me.lblRowClicked)
      Me.Controls.Add(Me.LinkLabel1)
      Me.Controls.Add(Me.chkMultiLineText)
      Me.Controls.Add(Me.btnFillGridFromDB)
      Me.Controls.Add(Me.btnClose)
      Me.Controls.Add(Me.btnFillGrid)
      Me.Controls.Add(Me.DataGrid1)
      Me.MinimumSize = New System.Drawing.Size(608, 312)
      Me.Name = "Form1"
      Me.Text = "KnowDotNet  KDNGrid  Demo Form"
      CType(Me.DataGrid1, System.ComponentModel.ISupportInitialize).EndInit()
      Me.ResumeLayout(False)

   End Sub

#End Region


   Private Sub LoadGrid()
      'This method creates a DataTable to use for the Grid

      Dim dt As New DataTable("MyFirstTable")
      BuildDataTable(dt)

      'Create a TableStyle to contain the columnstyles for the grid.
      '(An alternate method for doing this is at the end of this method)
      Dim ts As DataGridTableStyle = CGrid.GetTableStyle(dt)

      'Create the ColumnStyles and add each one to the TableStyle.
      'Normally, we would build all the ColumnStyles then add 
      'them all to the tablestyle, but because the Multiline style
      'may or may not be created, each ColumnStyle is added individually.

      'CheckBox Column
      Dim cs1 As New CGridCheckBoxStyle("Column1", 60, _
                                        HorizontalAlignment.Center, False, _
                                        "Select", "", "N", "Y", False, "")
      CGrid.AddColumn(ts, cs1)

      'TextBox Column
      Dim cs2 As New CGridTextBoxStyle("Column2", 80, _
                                        HorizontalAlignment.Left, False, _
                                        "Editable", String.Empty, "")
      CGrid.AddColumn(ts, cs2)

      'DateTimePicker Column
      Dim cs3 As New CGridDateTimePickerStyle("Column3", 220, _
                                              False, "MyDate", _
                                              DateTimePickerFormat.Custom, _
                                              "F", "MM/dd/yyyy hh:mm:ss tt")
      CGrid.AddColumn(ts, cs3)

      'TextBox Column - ReadOnly
      Dim cs4 As New CGridTextBoxStyle("Column4", 100, _
                                       HorizontalAlignment.Left, True, _
                                       "Non Editable", "", "")
      CGrid.AddColumn(ts, cs4)

      'ComboBox Column
      Dim Items() As String = {"Yes", "No", "Maybe", "Depends"}
      Dim cs5 As New CGridComboBoxStyle("Column5", 80, _
                                        HorizontalAlignment.Left, _
                                        "Your Pick", "(null)", _
                                        Items, ComboBoxStyle.DropDownList)
      CGrid.AddColumn(ts, cs5)

      'NumericUpDown Column
      Dim cs6 As New CGridNumericUpDownStyle("Column6", 60, "Count", 0, 100, _
                                             0, 1, LeftRightAlignment.Right, _
                                             0, "#,##0")
      CGrid.AddColumn(ts, cs6)

      'Multiline TextBox Column
      If Me.chkMultiLineText.Checked Then
         Dim cs7 As New CGridMultiLineTextBoxStyle("Column7", 200, _
                                                   HorizontalAlignment.Left, _
                                                   False, "MultiLine Column", "")
         CGrid.AddColumn(ts, cs7)
      Else
         Dim cs7 As New CGridTextBoxStyle("column7", 150, _
                                          HorizontalAlignment.Left, False, _
                                          "ML Text", "", "")
         CGrid.AddColumn(ts, cs7)
      End If


      'Set an AlternatingBackColor - just for looks
      ts.AlternatingBackColor = Color.LightGoldenrodYellow

      'Set the TableStyle for the Grid
      CGrid.SetGridStyle(Me.DataGrid1, dt, ts)

      'Uncomment this line to prevent the user from adding rows to the grid
      'CGrid.DisableAddNew(DataGrid1, Me)

      'Turn off the title bar for the grid
      Me.DataGrid1.CaptionVisible = False

      'ALTERNATE METHOD - Create a strongly typed collection and 
      '                   add the created ColumnStyles to it
      'Dim col As New KnowDotNet.KDNGrid.DataGridColumnStyleCollection()
      'col.Add(cs1)
      'col.Add(cs2)
      'col.Add(cs3)
      'col.Add(cs4)
      'col.Add(cs5)
      'col.Add(cs6)
      'col.Add(cs7)
      ''Build the Grid
      'cg.SetGridStyle(Me.DataGrid1, dt, col)

   End Sub

   Private Sub LoadGridFromDB()
      'This method loads the grid from a Microsoft Access Database

      Dim dt As New DataTable("dt")
      Dim appPath As String = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly.Location())
      Dim connString As String = "Provider=Microsoft.JET.OLEDB.4.0;data source=" & _
                                 appPath & "\cgridtest.mdb" '
      Dim sqlString As String = "SELECT * FROM cgridtesttable"
      Dim dataAdapter As OleDbDataAdapter
      Dim connection As OleDbConnection

      Try
         'Create Connection
         connection = New OleDbConnection(connString)

         'Create DataAdapter to obtain data
         dataAdapter = New OleDbDataAdapter(sqlString, connection)

         'Fill the DataTable
         dataAdapter.Fill(dt)

      Catch ex As Exception
         MessageBox.Show("Error Accessing Database:" & Environment.NewLine & ex.ToString)
         Exit Sub
      Finally
         'Close the connection
         connection.Close()
      End Try

      'Create a TableStyle to which ColumnStyles will be added
      Dim ts As DataGridTableStyle = CGrid.GetTableStyle(dt)

      'CheckBox Column
      Dim cs1 As New CGridCheckBoxStyle("Column1", 60, _
                                        HorizontalAlignment.Center, False, _
                                        "Select", "", False, True, False, False)
      CGrid.AddColumn(ts, cs1)

      'TextBox Column
      Dim cs2 As New CGridTextBoxStyle("Column2", 80, _
                                       HorizontalAlignment.Left, False, _
                                       "Editable", String.Empty, "")
      CGrid.AddColumn(ts, cs2)

      'DateTimePicker Column
      Dim cs3 As New CGridDateTimePickerStyle("Column3", 220, False, "MyDate", _
                                              DateTimePickerFormat.Custom, "F", _
                                              "MM/dd/yyyy hh:mm:ss tt")
      CGrid.AddColumn(ts, cs3)

      'TextBox Column - ReadOnly
      Dim cs4 As New CGridTextBoxStyle("Column4", 100, HorizontalAlignment.Left, _
                                       True, "Non Editable", "", "")
      CGrid.AddColumn(ts, cs4)

      'ComboBox Column
      Dim Items() As String = {"Yes", "No", "Maybe"}
      Dim cs5 As New CGridComboBoxStyle("Column5", 60, HorizontalAlignment.Left, _
                                        "Your Pick", "No", _
                                        Items, ComboBoxStyle.DropDownList)
      CGrid.AddColumn(ts, cs5)

      'NumericUpDown Column
      Dim cs6 As New CGridNumericUpDownStyle("Column6", 60, "Count", 0, 100, 0, _
                                             1, LeftRightAlignment.Right, 0, _
                                             "#,##0")
      CGrid.AddColumn(ts, cs6)

      'Multiline TextBox Column
      If Me.chkMultiLineText.Checked Then
         Dim cs7 As New CGridMultiLineTextBoxStyle("Column7", 200, _
                                                   HorizontalAlignment.Left, _
                                                   False, "MultiLine Column", "")
         CGrid.AddColumn(ts, cs7)
      Else
         Dim cs7 As New CGridTextBoxStyle("column7", 150, _
                                          HorizontalAlignment.Left, False, _
                                          "Text", "", "")
         CGrid.AddColumn(ts, cs7)
      End If

      'TextBox Column
      Dim cs8 As New CGridTextBoxStyle("column8", 60, HorizontalAlignment.Left, _
                                       False, "Names", "", "")
      CGrid.AddColumn(ts, cs8)

      'Set an AlternatingBackColor - just for looks
      ts.AlternatingBackColor = Color.LightGoldenrodYellow

      'Set the TableStyle for the Grid
      CGrid.SetGridStyle(Me.DataGrid1, dt, ts)

      'Prevent the user from adding rows to the grid
      CGrid.DisableAddNew(DataGrid1, Me)

      'Turn off the title bar for the grid
      Me.DataGrid1.CaptionVisible = False

      'ALTERNATE METHOD - Create a strongly typed collection and 
      '                   add the created ColumnStyles to it
      'Dim col As New KnowDotNet.KDNGrid.DataGridColumnStyleCollection()
      'col.Add(cs1)
      'col.Add(cs2)
      'col.Add(cs3)
      'col.Add(cs4)
      'col.Add(cs5)
      'col.Add(cs6)
      'col.Add(cs7)
      'col.Add(cs8)
      ''Build the Grid
      'cg.SetGridStyle(Me.DataGrid1, dt, col)

   End Sub

   Private Sub BuildDataTable(ByRef dt As DataTable)
      'This creates a DataTable with 7 columns and 4 rows

      dt.Columns.Add("column1")
      dt.Columns.Add("column2")
      dt.Columns.Add("column3")
      dt.Columns.Add("column4")
      dt.Columns.Add("column5")
      dt.Columns.Add("column6")
      dt.Columns.Add("column7")

      Dim ar() As Object = {"Y", "TestString1", "2/23/2004 08:20:30 AM", _
                            "TestString12", "Yes", System.DBNull.Value, _
                            "Now is the time for all good men to come to the aid of their country.  The quick brown fox jumped over the lazy dog."}
      CGrid.AddRowToTable(dt, ar)

      Dim ar2() As Object = {"Y", "TestString2", "2/23/2004 08:20:30 AM", _
                             "TestString22", "No", 2, _
                             "to come to the aid of their country."}
      CGrid.AddRowToTable(dt, ar2)

      Dim ar3() As Object = {"Y", "TestString3", "2/23/2004 08:20:30 AM", _
                             "TestString32", "no", 3, "Testing data"}
      CGrid.AddRowToTable(dt, ar3)

      Dim ar4() As Object = {"N", "TestString4", "2/23/2004 08:20:30 AM", _
                             "TestString42", "Yes", 4, _
                             "Testing data also in this column"}
      CGrid.AddRowToTable(dt, ar4)

   End Sub


#Region " Event Handlers "

   Private Sub LinkLabel1_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
      Try
         System.Diagnostics.Process.Start("www.KnowDotNet.com/articles/kdngrid.html")
      Catch
      End Try
   End Sub

   Private Sub btnFillGridFromDB_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFillGridFromDB.Click
      Try

         'Clear any existing TableStyles
         CGrid.ClearTableStyles(DataGrid1)

         'Fill the Grid
         Me.LoadGridFromDB()

      Catch ex As System.Exception
         MessageBox.Show(ex.ToString, "KDNGrid Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
      End Try
   End Sub

   Private Sub btnFillGrid_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFillGrid.Click
      Try

         'Clear any existing TableStyles
         CGrid.ClearTableStyles(DataGrid1)

         'Fill the grid
         LoadGrid()

      Catch ex As System.Exception
         MessageBox.Show(ex.ToString, "KDNGrid Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
      End Try
   End Sub

   Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
      Me.Close()
   End Sub

   Private Sub DataGrid1_MouseWheel(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles DataGrid1.MouseWheel
      Me.DataGrid1.Focus()
   End Sub

   Private Sub DataGrid1_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles DataGrid1.MouseUp
      'This code will handle changing the value of the CheckBox column in 
      'column1 on a single click, rather than having to click once to activate,
      'then again to change the value.  It also will select the entire row
      'when column1 is clicked.

      Dim ClickedRowIndex As Integer
      Dim bChecked As Boolean
      Dim ClickedColumnName As String
      Dim result As Object = Nothing

      'Determine what was clicked
      ClickedRowIndex = CGrid.GetClickedCellAndRow(CType(DataGrid1.DataSource, DataTable), Me.DataGrid1, ClickedColumnName, result, False)

      If ClickedRowIndex > -1 AndAlso ClickedColumnName.ToLower = "column1" Then
         'Column1 was clicked - Toggle the value and select the row
         ClickedRowIndex = CGrid.SelectCheckBoxRow(CType(DataGrid1.DataSource, DataTable), Me.DataGrid1, e, "Column1", bChecked, 0, True)
         result = bChecked
      End If

      If ClickedRowIndex > -1 Then
         'Update the UI
         Me.lblCellClicked.Text = ClickedColumnName
         Me.lblRowClicked.Text = ClickedRowIndex.ToString
         If Not result Is Nothing Then
            Me.lblValue.Text = result.ToString
         End If
      End If

   End Sub

#End Region


End Class
