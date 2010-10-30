Option Strict On
Imports System.Reflection

Namespace KnowDotNet.KDNGrid


#Region " TextBox Column "

   Public Class CGridTextBoxStyle
      Inherits DataGridTextBoxColumn

      Public Sub New(ByVal MappingName As String)
         MyBase.New()
         Me.MappingName = MappingName
      End Sub

      Public Sub New(ByVal MappingName As String, _
                     ByVal Width As Integer, _
                     ByVal Alignment As HorizontalAlignment, _
                     ByVal [ReadOnly] As Boolean, _
                     ByVal HeaderText As String, _
                     ByVal NullText As String, _
                     ByVal Format As String)
         Me.New(MappingName)
         Me.Format = Format
         Me.Alignment = Alignment
         Me.Width = Width
         Me.ReadOnly = [ReadOnly]
         Me.HeaderText = HeaderText
         Me.NullText = NullText
      End Sub

   End Class

#End Region


#Region " CheckBox Column "

   Public Class CGridCheckBoxStyle
      Inherits DataGridBoolColumn

      Public Sub New(ByVal MappingName As String)
         MyBase.New()
         Me.MappingName = MappingName
      End Sub

      Public Sub New(ByVal MappingName As String, _
                     ByVal Width As Integer, _
                     ByVal Alignment As HorizontalAlignment, _
                     ByVal [ReadOnly] As Boolean, _
                     ByVal HeaderText As String, _
                     ByVal NullText As String, _
                     ByVal FalseValue As Object, _
                     ByVal TrueValue As Object, _
                     ByVal AllowNull As Boolean, _
                     ByVal NullValue As Object)
         Me.New(MappingName)
         Me.Alignment = Alignment
         Me.Width = Width
         Me.ReadOnly = [ReadOnly]
         Me.HeaderText = HeaderText
         Me.FalseValue = FalseValue
         Me.TrueValue = TrueValue
         Me.NullText = NullText
         Me.NullValue = NullValue
         Me.AllowNull = AllowNull
      End Sub

   End Class

#End Region


#Region " DateTimePicker Column "

   Public Class DataGridDateTimePicker
      Inherits DateTimePicker

      Private bIgnoreNextMessage As Boolean = False

      Protected Overrides Sub OnGotFocus(ByVal e As System.EventArgs)
         Me.bIgnoreNextMessage = True
      End Sub

      Protected Overrides Function ProcessKeyMessage(ByRef m As System.Windows.Forms.Message) As Boolean
         If Me.bIgnoreNextMessage Then
            Me.bIgnoreNextMessage = False
            Return True
         Else
            Return MyBase.ProcessKeyMessage(m)
         End If
      End Function
   End Class

   Public Class CGridDateTimePickerStyle
      Inherits DataGridColumnStyle

      Public TimePicker As New DataGridDateTimePicker
      Public Formatter As String
      Private isEditing As Boolean

      Public Sub New(ByVal MappingName As String)
         MyBase.New()
         TimePicker.Visible = False
         Me.MappingName = MappingName

         AddHandler Me.TimePicker.Leave, AddressOf HideControl

      End Sub

      Public Sub New(ByVal MappingName As String, _
                     ByVal Width As Integer, _
                     ByVal [ReadOnly] As Boolean, _
                     ByVal HeaderText As String, _
                     ByVal DTPFormatStyle As DateTimePickerFormat, _
                     ByVal FormatString As String, _
                     ByVal DateTimePickerCustomFormat As String)
         Me.New(MappingName)
         Me.Width = Width
         Me.ReadOnly = [ReadOnly]
         Me.HeaderText = HeaderText
         Me.TimePicker.Format = DTPFormatStyle
         Me.Formatter = FormatString
         'Me.NullText = DateTimePicker.MinDateTime.ToString(Formatter)
         Me.NullText = TimePicker.MinDate.ToString(Formatter)
         Me.TimePicker.CustomFormat = DateTimePickerCustomFormat
      End Sub

      Protected Overrides Sub Abort(ByVal rowNum As Integer)
         isEditing = False
         RemoveHandler TimePicker.ValueChanged, AddressOf TimePickerValueChanged
         Invalidate()
      End Sub

      Protected Overrides Function Commit(ByVal dataSource As CurrencyManager, _
                                          ByVal rowNum As Integer) As Boolean
         TimePicker.Bounds = Rectangle.Empty

         RemoveHandler TimePicker.ValueChanged, AddressOf TimePickerValueChanged

         If Not isEditing Then
            Return True
         End If
         isEditing = False

         Dim value As Date = TimePicker.Value
         'If value = DateTimePicker.MinDateTime Then
         If value = TimePicker.MinDate Then
            SetColumnValueAtRow(dataSource, rowNum, System.DBNull.Value)
         Else
            SetColumnValueAtRow(dataSource, rowNum, value)
         End If

         Invalidate()

         Return True

      End Function

      Protected Overloads Overrides Sub Edit(ByVal [source] As CurrencyManager, _
                                             ByVal rowNum As Integer, _
                                             ByVal bounds As Rectangle, _
                                             ByVal [readOnly] As Boolean, _
                                             ByVal instantText As String, _
                                             ByVal cellIsVisible As Boolean)


         RemoveHandler TimePicker.ValueChanged, AddressOf TimePickerValueChanged

         Dim value As Date
         If IsDBNull(GetColumnValueAtRow([source], rowNum)) Then
            'value = DateTimePicker.MinDateTime
            value = TimePicker.MinDate
         Else
            value = CType(GetColumnValueAtRow([source], rowNum), Date)
         End If

         If cellIsVisible Then
            TimePicker.Bounds = New Rectangle _
            (bounds.X + 2, bounds.Y + 2, bounds.Width - 4, _
            bounds.Height - 4)

            If TimePicker.Value <> value Then
               TimePicker.Value = value
            End If
            TimePicker.Visible = True
            AddHandler TimePicker.ValueChanged, AddressOf TimePickerValueChanged
         Else
            If TimePicker.Value <> value Then
               TimePicker.Value = value
            End If
            TimePicker.Visible = False
         End If

         If TimePicker.Visible Then
            DataGridTableStyle.DataGrid.Invalidate(bounds)
         End If

         TimePicker.Focus()

      End Sub

      Protected Overrides Function GetPreferredSize(ByVal g As Graphics, _
                                                    ByVal value As Object) As Size
         Return New Size(100, TimePicker.PreferredHeight + 4)
      End Function

      Protected Overrides Function GetMinimumHeight() As Integer
         Return TimePicker.PreferredHeight + 4
      End Function

      Protected Overrides Function GetPreferredHeight(ByVal g As Graphics, _
                                                      ByVal value As Object) As Integer
         Return TimePicker.PreferredHeight + 4
      End Function

      Protected Overloads Overrides Sub Paint(ByVal g As Graphics, _
                                              ByVal bounds As Rectangle, _
                                              ByVal [source] As CurrencyManager, _
                                              ByVal rowNum As Integer)
         Paint(g, bounds, [source], rowNum, False)
      End Sub

      Protected Overloads Overrides Sub Paint(ByVal g As Graphics, _
                                              ByVal bounds As Rectangle, _
                                              ByVal [source] As CurrencyManager, _
                                              ByVal rowNum As Integer, _
                                              ByVal alignToRight As Boolean)
         Paint(g, bounds, [source], rowNum, Brushes.Red, Brushes.Blue, alignToRight)
      End Sub

      Protected Overloads Overrides Sub Paint(ByVal g As Graphics, _
                                              ByVal bounds As Rectangle, _
                                              ByVal [source] As CurrencyManager, _
                                              ByVal rowNum As Integer, _
                                              ByVal backBrush As Brush, _
                                              ByVal foreBrush As Brush, _
                                              ByVal alignToRight As Boolean)

         'Retrieve value from DataSource
         Dim o As Object = Me.GetColumnValueAtRow([source], rowNum)
         Dim value As Date
         If IsDBNull(o) Then
            'value = DateTimePicker.MinDateTime
            value = TimePicker.MinDate
         Else
            value = CDate(o)
         End If

         'Draw cell with value
         Dim rect As Rectangle = bounds
         g.FillRectangle(backBrush, rect)
         rect.Offset(0, 2)
         rect.Height -= 2
         'If value <> DateTimePicker.MinDateTime Then
         If value <> TimePicker.MinDate Then
            g.DrawString(value.ToString(Formatter), Me.DataGridTableStyle.DataGrid.Font, foreBrush, RectangleF.FromLTRB(rect.X, rect.Y, rect.Right, rect.Bottom))
         End If

      End Sub

      Protected Overrides Sub SetDataGridInColumn(ByVal value As DataGrid)
         MyBase.SetDataGridInColumn(value)
         If Not (TimePicker.Parent Is Nothing) Then
            TimePicker.Parent.Controls.Remove(TimePicker)
         End If
         If Not (value Is Nothing) Then
            value.Controls.Add(TimePicker)
         End If
      End Sub

      Private Sub TimePickerValueChanged(ByVal sender As Object, _
                                         ByVal e As EventArgs)
         If Not Me.isEditing Then
            Me.isEditing = True
            RemoveHandler TimePicker.ValueChanged, AddressOf TimePickerValueChanged
            MyBase.ColumnStartedEditing(TimePicker)
            AddHandler TimePicker.ValueChanged, AddressOf TimePickerValueChanged
         End If
      End Sub

      Private Sub HideControl(ByVal sender As Object, _
                              ByVal e As EventArgs)

         Me.TimePicker.Bounds = Rectangle.Empty

      End Sub

      Protected Overrides Sub EnterNullValue()
         'If TimePicker.Value <> DateTimePicker.MinDateTime Then
         If TimePicker.Value <> TimePicker.MinDate Then
            'Me.TimePicker.Value = DateTimePicker.MinDateTime
            Me.TimePicker.Value = TimePicker.MinDate
         End If
      End Sub

   End Class

#End Region


#Region " ComboBox Column "

   Public Class CGridComboBoxStyle
      Inherits DataGridColumnStyle

      Public cgCombo As New ComboBox
      Private isEditing As Boolean

      Public Sub New(ByVal MappingName As String)
         Me.cgCombo.Visible = False
         Me.MappingName = MappingName
         Me.NullText = String.Empty

         AddHandler Me.cgCombo.Leave, AddressOf HideControl

      End Sub

      Public Sub New(ByVal MappingName As String, _
                     ByVal Width As Integer, _
                     ByVal Alignment As HorizontalAlignment, _
                     ByVal HeaderText As String, _
                     ByVal NullText As String, _
                     ByVal Items() As String, _
                     ByVal ListDrop As ComboBoxStyle)
         Me.New(MappingName)
         Me.Width = Width
         Me.Alignment = Alignment
         Me.HeaderText = HeaderText
         Me.NullText = NullText
         If Items.Length > 0 Then
            Me.cgCombo.Items.AddRange(Items)
         End If
      End Sub

      Protected Overrides Sub Abort(ByVal rowNum As Integer)

         isEditing = False
         RemoveHandler cgCombo.TextChanged, AddressOf ComboBoxSelectedValueChanged
         Invalidate()

      End Sub

      Protected Overrides Function Commit(ByVal dataSource As CurrencyManager, _
                                          ByVal rowNum As Integer) As Boolean
         Me.cgCombo.Bounds = Rectangle.Empty

         RemoveHandler Me.cgCombo.TextChanged, AddressOf ComboBoxSelectedValueChanged

         If Not isEditing Then
            Return True
         End If
         isEditing = False

         Dim value As String = Me.cgCombo.Text
         If value.CompareTo(NullText) = 0 Then
            SetColumnValueAtRow(dataSource, rowNum, System.DBNull.Value)
         Else
            SetColumnValueAtRow(dataSource, rowNum, value)
         End If

         Invalidate()

         Return True

      End Function

      Protected Overloads Overrides Sub Edit(ByVal [source] As CurrencyManager, _
                                             ByVal rowNum As Integer, _
                                             ByVal bounds As Rectangle, _
                                             ByVal [readOnly] As Boolean, _
                                             ByVal instantText As String, _
                                             ByVal cellIsVisible As Boolean)

         RemoveHandler Me.cgCombo.TextChanged, AddressOf ComboBoxSelectedValueChanged

         Dim value As String
         If IsDBNull(GetColumnValueAtRow([source], rowNum)) Then
            value = Me.NullText
         Else
            value = CStr(GetColumnValueAtRow([source], rowNum))
         End If

         If cellIsVisible Then
            Me.cgCombo.Bounds = New Rectangle _
            (bounds.X + 2, bounds.Y + 2, bounds.Width - 4, _
            bounds.Height - 4)

            Me.cgCombo.Text = value
            Me.cgCombo.Visible = True
            AddHandler Me.cgCombo.TextChanged, AddressOf ComboBoxSelectedValueChanged
         Else
            Me.cgCombo.Text = value
            Me.cgCombo.Visible = False
         End If

         If Me.cgCombo.Visible Then
            DataGridTableStyle.DataGrid.Invalidate(bounds)
         End If

         'Focus the ComboBox so that user can scroll values
         Me.cgCombo.Focus()

      End Sub

      Protected Overrides Function GetPreferredSize(ByVal g As Graphics, _
                                                    ByVal value As Object) As Size
         Return New Size(100, Me.cgCombo.PreferredHeight + 4)
      End Function

      Protected Overrides Function GetMinimumHeight() As Integer
         Return Me.cgCombo.PreferredHeight + 4
      End Function

      Protected Overrides Function GetPreferredHeight(ByVal g As Graphics, _
                                                      ByVal value As Object) As Integer
         Return Me.cgCombo.PreferredHeight + 4
      End Function

      Protected Overloads Overrides Sub Paint(ByVal g As Graphics, _
                                              ByVal bounds As Rectangle, _
                                              ByVal [source] As CurrencyManager, _
                                              ByVal rowNum As Integer)
         Paint(g, bounds, [source], rowNum, False)
      End Sub

      Protected Overloads Overrides Sub Paint(ByVal g As Graphics, _
                                              ByVal bounds As Rectangle, _
                                              ByVal [source] As CurrencyManager, _
                                              ByVal rowNum As Integer, _
                                              ByVal alignToRight As Boolean)
         Paint(g, bounds, [source], rowNum, Brushes.Red, Brushes.Blue, alignToRight)
      End Sub

      Protected Overloads Overrides Sub Paint(ByVal g As Graphics, _
                                              ByVal bounds As Rectangle, _
                                              ByVal [source] As CurrencyManager, _
                                              ByVal rowNum As Integer, _
                                              ByVal backBrush As Brush, _
                                              ByVal foreBrush As Brush, _
                                              ByVal alignToRight As Boolean)

         Dim o As Object = Me.GetColumnValueAtRow([source], rowNum)
         Dim value As String

         If IsDBNull(o) Then
            value = Me.NullText
         Else
            value = CStr(o)
         End If

         Dim rect As Rectangle = bounds
         g.FillRectangle(backBrush, rect)
         rect.Offset(0, 2)
         rect.Height -= 2
         g.DrawString(value, Me.DataGridTableStyle.DataGrid.Font, foreBrush, RectangleF.FromLTRB(rect.X, rect.Y, rect.Right, rect.Bottom))

      End Sub

      Protected Overrides Sub SetDataGridInColumn(ByVal value As DataGrid)
         MyBase.SetDataGridInColumn(value)
         If Not (Me.cgCombo.Parent Is Nothing) Then
            Me.cgCombo.Parent.Controls.Remove(Me.cgCombo)
         End If
         If Not (value Is Nothing) Then
            value.Controls.Add(Me.cgCombo)
         End If
      End Sub

      Private Sub ComboBoxSelectedValueChanged(ByVal sender As Object, _
                                               ByVal e As EventArgs)
         If Not Me.isEditing Then
            Me.isEditing = True
            RemoveHandler Me.cgCombo.TextChanged, AddressOf ComboBoxSelectedValueChanged
            MyBase.ColumnStartedEditing(Me.cgCombo)
            AddHandler Me.cgCombo.TextChanged, AddressOf ComboBoxSelectedValueChanged
         End If
      End Sub

      Private Sub HideControl(ByVal sender As Object, _
                              ByVal e As EventArgs)

         Me.cgCombo.Bounds = Rectangle.Empty

      End Sub

      Protected Overrides Sub EnterNullValue()
         Me.cgCombo.Text = Me.NullText
      End Sub

   End Class

#End Region


#Region " NumericUpDown Column "

   Public Class DataGridNumericUpDown
      Inherits NumericUpDown

      Private WM_KEYUP As Integer = &H101

      Protected Overrides Sub WndProc(ByRef m As System.Windows.Forms.Message)
         If m.Msg = WM_KEYUP Then
            Return
         End If
         MyBase.WndProc(m)
      End Sub

   End Class

   Public Class CGridNumericUpDownStyle
      Inherits DataGridColumnStyle

      Friend nudNumericUpDown As New DataGridNumericUpDown
      Friend Formatter As String = String.Empty
      Private isEditing As Boolean

      Public Sub New(ByVal MappingName As String)
         MyBase.New()
         Me.nudNumericUpDown.Visible = False
         Me.MappingName = MappingName
         Me.NullText = "0"
         Me.nudNumericUpDown.InterceptArrowKeys = True
         Me.nudNumericUpDown.ThousandsSeparator = True
         Me.nudNumericUpDown.Hexadecimal = False

         AddHandler Me.nudNumericUpDown.Leave, AddressOf HideControl

      End Sub

      Public Sub New(ByVal MappingName As String, _
                     ByVal Width As Integer, _
                     ByVal HeaderText As String, _
                     ByVal Minimum As Decimal, _
                     ByVal Maximum As Decimal, _
                     ByVal DecimalPlaces As Integer, _
                     ByVal Increment As Decimal, _
                     ByVal UpDownAlignment As LeftRightAlignment, _
                     ByVal NullValue As Decimal, _
                     ByVal Formatter As String)

         Me.New(MappingName)
         Me.Width = Width
         Me.HeaderText = HeaderText
         Me.nudNumericUpDown.DecimalPlaces = DecimalPlaces
         Me.nudNumericUpDown.Increment = Increment
         Me.nudNumericUpDown.Maximum = Maximum
         Me.nudNumericUpDown.Minimum = Minimum
         Me.nudNumericUpDown.UpDownAlign = UpDownAlignment
         Me.NullText = NullValue.ToString(Formatter)
         Me.Formatter = Formatter
      End Sub

      Protected Overrides Sub Abort(ByVal rowNum As Integer)
         isEditing = False
         RemoveHandler nudNumericUpDown.ValueChanged, AddressOf NumericUpDownValueChanged
         Invalidate()
      End Sub

      Protected Overrides Function Commit(ByVal dataSource As CurrencyManager, _
                                          ByVal rowNum As Integer) As Boolean

         Me.nudNumericUpDown.Bounds = Rectangle.Empty

         RemoveHandler Me.nudNumericUpDown.ValueChanged, AddressOf NumericUpDownValueChanged

         If Not isEditing Then
            Return True
         End If
         isEditing = False

         Dim value As Decimal = Me.nudNumericUpDown.Value
         If value = Decimal.Parse(Me.NullText) Then
            SetColumnValueAtRow(dataSource, rowNum, System.DBNull.Value)
         Else
            SetColumnValueAtRow(dataSource, rowNum, value)
         End If

         Invalidate()

         Return True

      End Function

      Protected Overloads Overrides Sub Edit(ByVal [source] As CurrencyManager, _
                                             ByVal rowNum As Integer, _
                                             ByVal bounds As Rectangle, _
                                             ByVal [readOnly] As Boolean, _
                                             ByVal instantText As String, _
                                             ByVal cellIsVisible As Boolean)

         RemoveHandler Me.nudNumericUpDown.ValueChanged, AddressOf NumericUpDownValueChanged

         Dim value As Decimal
         If IsDBNull(GetColumnValueAtRow([source], rowNum)) Then
            value = Decimal.Parse(Me.NullText)
         Else
            value = CDec(GetColumnValueAtRow([source], rowNum))
         End If

         If cellIsVisible Then
            Me.nudNumericUpDown.Bounds = New Rectangle _
            (bounds.X + 2, bounds.Y + 2, bounds.Width - 4, _
            bounds.Height - 4)

            Me.nudNumericUpDown.Value = value
            Me.nudNumericUpDown.Visible = True
            AddHandler Me.nudNumericUpDown.ValueChanged, AddressOf NumericUpDownValueChanged
         Else
            Me.nudNumericUpDown.Value = value
            Me.nudNumericUpDown.Visible = False
         End If

         If Me.nudNumericUpDown.Visible Then
            DataGridTableStyle.DataGrid.Invalidate(bounds)
         End If

         Me.nudNumericUpDown.Focus()

      End Sub

      Protected Overrides Function GetPreferredSize(ByVal g As Graphics, _
                                                    ByVal value As Object) As Size
         Return New Size(100, Me.nudNumericUpDown.PreferredHeight + 4)
      End Function

      Protected Overrides Function GetMinimumHeight() As Integer
         Return Me.nudNumericUpDown.PreferredHeight + 4
      End Function

      Protected Overrides Function GetPreferredHeight(ByVal g As Graphics, _
                                                      ByVal value As Object) As Integer
         Return Me.nudNumericUpDown.PreferredHeight + 4
      End Function

      Protected Overloads Overrides Sub Paint(ByVal g As Graphics, _
                                              ByVal bounds As Rectangle, _
                                              ByVal [source] As CurrencyManager, _
                                              ByVal rowNum As Integer)
         Paint(g, bounds, [source], rowNum, False)
      End Sub

      Protected Overloads Overrides Sub Paint(ByVal g As Graphics, _
                                              ByVal bounds As Rectangle, _
                                              ByVal [source] As CurrencyManager, _
                                              ByVal rowNum As Integer, _
                                              ByVal alignToRight As Boolean)
         Paint(g, bounds, [source], rowNum, Brushes.Red, Brushes.Blue, alignToRight)
      End Sub

      Protected Overloads Overrides Sub Paint(ByVal g As Graphics, _
                                              ByVal bounds As Rectangle, _
                                              ByVal [source] As CurrencyManager, _
                                              ByVal rowNum As Integer, _
                                              ByVal backBrush As Brush, _
                                              ByVal foreBrush As Brush, _
                                              ByVal alignToRight As Boolean)

         Dim o As Object = Me.GetColumnValueAtRow([source], rowNum)
         Dim value As Decimal

         If IsDBNull(o) Then
            value = Decimal.Parse(Me.NullText)
         Else
            value = CDec(o)
         End If

         g.FillRectangle(backBrush, bounds)
         bounds.Offset(0, 2)
         bounds.Height -= 2
         g.DrawString(value.ToString(Formatter), Me.DataGridTableStyle.DataGrid.Font, foreBrush, RectangleF.FromLTRB(bounds.X, bounds.Y, bounds.Right, bounds.Bottom))

      End Sub

      Protected Overrides Sub SetDataGridInColumn(ByVal value As DataGrid)
         MyBase.SetDataGridInColumn(value)
         If Not (Me.nudNumericUpDown.Parent Is Nothing) Then
            Me.nudNumericUpDown.Parent.Controls.Remove(Me.nudNumericUpDown)
         End If
         If Not (value Is Nothing) Then
            value.Controls.Add(Me.nudNumericUpDown)
         End If
      End Sub

      Private Sub NumericUpDownValueChanged(ByVal sender As Object, _
                                            ByVal e As EventArgs)
         If Not Me.isEditing Then
            Dim value As Decimal = Me.nudNumericUpDown.Value
            Me.isEditing = True
            RemoveHandler Me.nudNumericUpDown.ValueChanged, AddressOf NumericUpDownValueChanged
            MyBase.ColumnStartedEditing(Me.nudNumericUpDown)
            AddHandler Me.nudNumericUpDown.ValueChanged, AddressOf NumericUpDownValueChanged
            Me.nudNumericUpDown.Value = value
         End If
      End Sub

      Protected Overrides Sub EnterNullValue()
         Me.nudNumericUpDown.Value = Decimal.Parse(Me.NullText)
      End Sub

      Private Sub HideControl(ByVal sender As Object, _
                              ByVal e As EventArgs)

         Me.nudNumericUpDown.Bounds = Rectangle.Empty

      End Sub

   End Class

#End Region


#Region " MultiLineTextBox Column "

   Public Class CGridMultiLineTextBoxStyle
      Inherits DataGridTextBoxColumn

      ' This class creates and handles the MultiLine
      ' textbox column, no scroll bars, column height
      ' grows as text increases

      'This class has several known issues.
      'It is provided AS-IS and should be used 
      'with caution.  It has issues with painting
      'rows in the wrong place when a DataGrid 
      'allows new rows to be added.  It also will fail
      'to size the rows properly unless the Multiline 
      'column is visible when the grid is displayed.

      Private HAlignment As HorizontalAlignment
      Private DrawFormat As New StringFormat
      Private AdjustHeight As Boolean = True
      Private dg As DataGrid
      Private Heights As ArrayList

      Public Property DataAlignment() As HorizontalAlignment
         Get
            Return HAlignment
         End Get
         Set(ByVal Value As HorizontalAlignment)
            HAlignment = Value
            If HAlignment = HorizontalAlignment.Center Then
               DrawFormat.Alignment = StringAlignment.Center
            ElseIf HAlignment = HorizontalAlignment.Right Then
               DrawFormat.Alignment = StringAlignment.Far
            Else
               DrawFormat.Alignment = StringAlignment.Near
            End If
         End Set
      End Property
      Public Property AutoAdjustHeight() As Boolean
         Get
            Return AdjustHeight
         End Get
         Set(ByVal Value As Boolean)
            AdjustHeight = Value
            dg.Invalidate()
         End Set
      End Property


      Public Sub New(ByVal MappingName As String)
         MyBase.new()
         Me.MappingName = MappingName
         HAlignment = HorizontalAlignment.Left
         DrawFormat.Alignment = StringAlignment.Near
         MyBase.TextBox.TextAlign = HAlignment
         MyBase.TextBox.Multiline = AdjustHeight
      End Sub

      Public Sub New(ByVal MappingName As String, _
                     ByVal Width As Integer, _
                     ByVal Alignment As HorizontalAlignment, _
                     ByVal [ReadOnly] As Boolean, _
                     ByVal HeaderText As String, _
                     ByVal NullText As String)
         Me.New(MappingName)
         Me.Alignment = Alignment
         Me.Width = Width
         Me.ReadOnly = [ReadOnly]
         Me.HeaderText = HeaderText
         Me.NullText = NullText
      End Sub


      Private Sub FillHeightArrayList()
         Dim mi As MethodInfo = _
            dg.GetType().GetMethod("get_DataGridRows", _
            BindingFlags.FlattenHierarchy Or _
            BindingFlags.IgnoreCase Or _
            BindingFlags.Instance Or _
            BindingFlags.NonPublic Or _
            BindingFlags.Public Or _
            BindingFlags.Static)
         Dim dgRowArray As Array = CType(mi.Invoke(Me.dg, Nothing), Array)
         Heights = New ArrayList
         Dim dgRowHeight As Object
         For Each dgRowHeight In dgRowArray
            If dgRowHeight.ToString().EndsWith _
            ("DataGridRelationshipRow") = True _
            Then
               Heights.Add(dgRowHeight)
            End If
         Next
      End Sub

      Protected Overloads Overrides Sub Edit(ByVal source As System.Windows.Forms.CurrencyManager, ByVal rowNum As Integer, _
                                             ByVal bounds As System.Drawing.Rectangle, _
                                             ByVal [readOnly] As Boolean, _
                                             ByVal instantText As String, _
                                             ByVal cellIsVisible As Boolean)

         MyBase.Edit(source, rowNum, bounds, [readOnly], instantText, cellIsVisible)
         MyBase.TextBox.TextAlign = HAlignment
         MyBase.TextBox.Multiline = AdjustHeight

      End Sub

      Protected Overloads Overrides Sub Paint(ByVal g As System.Drawing.Graphics, _
                                              ByVal bounds As System.Drawing.Rectangle, _
                                              ByVal source As System.Windows.Forms.CurrencyManager, _
                                              ByVal rowNum As Integer, ByVal backBrush As System.Drawing.Brush, _
                                              ByVal foreBrush As System.Drawing.Brush, _
                                              ByVal alignToRight As Boolean)

         Static bPainted As Boolean = False
         If Not bPainted Then
            dg = Me.DataGridTableStyle.DataGrid
            FillHeightArrayList()
         End If

         'clear the cell
         g.FillRectangle(backBrush, bounds)

         'draw the value
         Dim o As Object = Me.GetColumnValueAtRow([source], rowNum)
         Dim s As String
         If IsDBNull(o) Then
            s = Me.NullText
         Else
            s = CStr(Me.GetColumnValueAtRow([source], rowNum))
         End If

         Dim r As New RectangleF(bounds.X, bounds.Y, bounds.Width, bounds.Height)

         r.Inflate(0, -1)

         ' get the height column should be
         Dim sDraw As SizeF = g.MeasureString(s, Me.TextBox.Font, Me.Width, DrawFormat)
         Dim h As Integer = CInt(sDraw.Height + 15)

         If AdjustHeight Then

            FillHeightArrayList()

            Dim pi As PropertyInfo = Heights(rowNum).GetType().GetProperty("Height")
            Dim curHeight As Integer = CInt(pi.GetValue(Heights(rowNum), Nothing))

            If h > curHeight Then
               pi.SetValue(Heights(rowNum), h, Nothing)
            End If

         End If

         g.DrawString(s, MyBase.TextBox.Font, foreBrush, r, DrawFormat)
         bPainted = True

      End Sub

   End Class

#End Region


#Region " CGrid Class "

   Public Class CGrid

      'Populates the Datagrid, formatting its columns according
      ' to the passed collection of ColumnStyles.
      Public Overloads Shared Sub SetGridStyle(ByRef dg As DataGrid, _
                                               ByRef dt As DataTable, _
                                               ByRef coll As DataGridColumnStyleCollection)
         Dim colm As DataGridColumnStyle
         Dim ts As New DataGridTableStyle

         For Each colm In coll
            ts.GridColumnStyles.Add(colm)
         Next
         SetGridStyle(dg, dt, ts)
      End Sub

      'Populates the Datagrid, adding the passed TableStyle
      Public Overloads Shared Sub SetGridStyle(ByRef dg As DataGrid, _
                                               ByRef dt As DataTable, _
                                               ByRef ts As DataGridTableStyle)
         ts.MappingName = dt.TableName
         dg.TableStyles.Add(ts)
         dg.SetDataBinding(dt, "")
      End Sub

      'Returns a DataGridTableStyle for the passed DataTable
      ' NOTE: GridColumnStyles collection will be empty.
      Public Overloads Shared Function GetTableStyle(ByRef dt As DataTable) As DataGridTableStyle
         Dim ts As New DataGridTableStyle
         ts.GridColumnStyles.Clear()
         ts.MappingName = dt.TableName
         Return ts
      End Function

      'Returns a DataGridTableStyle for the passed DataTable, filling 
      'its GridColumnStyles with the passed collection of columns.
      Public Overloads Shared Function GetTableStyle(ByRef dt As DataTable, _
                                                     ByRef coll As DataGridColumnStyleCollection) As DataGridTableStyle
         Dim ts As New DataGridTableStyle
         ts.GridColumnStyles.Clear()
         ts.MappingName = dt.TableName
         Dim colm As DataGridColumnStyle
         For Each colm In coll
            ts.GridColumnStyles.Add(colm)
         Next
         Return ts
      End Function

      'Returns the row index as well as giving the column name and value
      'for the current cell.
      '
      'If a valid cell in the grid was clicked: 
      '                    ColumnMappingName = name of the column clicked
      '                    Value = value of the cell clicked
      '                    Returns index of clicked row
      '                    Selects the clicked row if SelectRow is True
      'If an error occurs: 
      '                    ColumnMappingName = "" 
      '                    Value = Nothing 
      '                    Returns -1
      '
      'On entry, ColumnMappingName and Value need not be set and will be ignored.
      '
      Public Overloads Shared Function GetClickedCellAndRow(ByRef dt As DataTable, _
                                                            ByRef dg As DataGrid, _
                                                            ByRef ColumnMappingName As String, _
                                                            ByRef Value As Object, _
                                                            ByVal SelectRow As Boolean) As Integer

         Dim row As Integer
         Dim ts As DataGridTableStyle

         If dg.TableStyles.Count < 1 Then Return -1

         ts = dg.TableStyles(0)
         row = CInt(dg.CurrentCell.RowNumber)
         Dim cs As DataGridColumnStyle = ts.GridColumnStyles(dg.CurrentCell.ColumnNumber)
         ColumnMappingName = cs.MappingName
         If dt.Rows.Count > dg.CurrentRowIndex Then
            Value = dt.Rows(dg.CurrentRowIndex).Item(ColumnMappingName)
            If SelectRow Then
               dg.Select(dg.CurrentRowIndex)
            End If
            Return row
         Else
            ColumnMappingName = String.Empty
            Value = Nothing
            Return -1
         End If

      End Function

      'This method will toggle the value of the current DataGridBoolColumn cell
      '
      'If SelectRow is true, then the entire row will be selected
      '
      'If the value in the CheckBox cell is null, then it is set to True
      '
      Public Overloads Shared Function SelectCheckBoxRow(ByRef dt As DataTable, _
                                                         ByRef dg As DataGrid, _
                                                         ByVal ColumnName As String, _
                                                         ByRef Checked As Boolean, _
                                                         ByVal SelectRow As Boolean) As Integer

         Dim col As Integer = dg.CurrentCell.ColumnNumber
         Dim row As Integer = dg.CurrentCell.RowNumber

         'If there are no TableStyles, then get out
         If dg.TableStyles.Count < 1 Then Return -1

         'If this is not a DataGridBoolColumn, then get out
         If Not TypeOf dg.TableStyles(0).GridColumnStyles(ColumnName) Is DataGridBoolColumn Then Return -1

         'Determine what values correspond to True and False
         Dim TrueValue As Object = CType(dg.TableStyles(0).GridColumnStyles(ColumnName), DataGridBoolColumn).TrueValue
         Dim FalseValue As Object = CType(dg.TableStyles(0).GridColumnStyles(ColumnName), DataGridBoolColumn).FalseValue

         'Retrieve the cell's value
         Dim o As Object = dt.Rows(dg.CurrentRowIndex).Item(ColumnName)

         If IsDBNull(o) Then
            'If null, set to True
            dt.Rows(dg.CurrentRowIndex).Item(ColumnName) = TrueValue
            Checked = True
         Else
            If o.Equals(TrueValue) Then
               'If True, set to False
               dt.Rows(dg.CurrentRowIndex).Item(ColumnName) = FalseValue
               Checked = False
            Else
               'If False, set to True
               dt.Rows(dg.CurrentRowIndex).Item(ColumnName) = TrueValue
               Checked = True
            End If
         End If

         If SelectRow Then
            'Select the clicked row
            dg.Select(row)
         End If

         Return row

      End Function

      'This method will toggle the value of a DataGridBoolColumn cell
      'that is clicked (as determined by e).
      '
      'If SelectRow is true, then the entire row will be selected
      '
      'If the value in the CheckBox cell is null, then it is set to True
      '
      Public Overloads Shared Function SelectCheckBoxRow(ByRef dt As DataTable, _
                                                         ByRef dg As DataGrid, _
                                                         ByVal e As System.Windows.Forms.MouseEventArgs, _
                                                         ByVal ColumnName As String, _
                                                         ByRef Checked As Boolean, _
                                                         ByVal ColNum As Integer, _
                                                         ByVal SelectRow As Boolean) As Integer

         'Find out which cell was clicked
         Dim pt As System.Drawing.Point = New Point(e.X, e.Y)
         Dim hti As DataGrid.HitTestInfo = dg.HitTest(pt)

         If hti.Type = DataGrid.HitTestType.Cell Then
            If ColNum = hti.Column Then
               dg.CurrentCell = New DataGridCell(hti.Row, hti.Column)
               Return CGrid.SelectCheckBoxRow(dt, dg, ColumnName, Checked, SelectRow)
            Else
               Return -1
            End If
         Else
            Return -1
         End If

      End Function

      'Adds the given ColumnStyle to the TableStyle's GridColumnStyles
      Public Shared Sub AddColumn(ByRef ts As DataGridTableStyle, _
                                  ByVal col As DataGridColumnStyle)
         ts.GridColumnStyles.Add(col)
      End Sub

      'Disables the addition of rows in the given DataGrid
      Public Shared Sub DisableAddNew(ByRef dg As DataGrid, _
                                      ByRef Frm As Form)
         ' Disable addnew capability on the grid.
         ' Note that AllowEdit and AllowDelete can be disabled
         ' by adding or changing the "AllowNew" property to 
         ' AllowDelete or AllowEdit.
         Dim cm As CurrencyManager = _
            CType(Frm.BindingContext(dg.DataSource, dg.DataMember), _
                  CurrencyManager)
         CType(cm.List, DataView).AllowNew = False
      End Sub

      'Creates a DataRow from the given values and adds 
      'it to the given DataTable
      Public Shared Sub AddRowToTable(ByRef dt As DataTable, _
                                      ByVal ParamArray Values() As Object)
         Dim i As Integer

         Dim newRow As DataRow = dt.NewRow
         For i = 0 To UBound(Values)
            newRow(i) = Values(i)
         Next

         dt.Rows.Add(newRow)

      End Sub

      'Clears any existing TableStyles for the DataGrid
      Public Shared Sub ClearTableStyles(ByRef dg As DataGrid)
         dg.TableStyles.Clear()
      End Sub

   End Class

#End Region


#Region " DataGridColumnStyleCollection "

   Public Class DataGridColumnStyleCollection
      Inherits CollectionBase

      Default Public Property Item(ByVal index As Integer) As DataGridColumnStyle
         Get
            Return CType(Me.List(index), DataGridColumnStyle)
         End Get
         Set(ByVal Value As DataGridColumnStyle)
            Me.List(index) = Value
         End Set
      End Property

      Public Sub Add(ByVal item As DataGridColumnStyle)
         Me.List.Add(item)
      End Sub

      Public Function Contains(ByVal value As DataGridColumnStyle) As Boolean
         Return Me.List.Contains(value)
      End Function

      Public Sub CopyTo(ByVal array As System.Array, _
                        ByVal index As Integer)
         Me.List.CopyTo(array, index)
      End Sub

      Public Function IndexOf(ByVal value As DataGridColumnStyle) As Integer
         Return Me.List.IndexOf(value)
      End Function

      Public Sub Insert(ByVal index As Integer, _
                        ByVal value As DataGridColumnStyle)
         Me.List.Insert(index, value)
      End Sub

      Public Sub Remove(ByVal value As DataGridColumnStyle)
         Me.Remove(value)
      End Sub

   End Class

#End Region


End Namespace
