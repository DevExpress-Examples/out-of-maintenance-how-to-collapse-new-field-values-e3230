Imports System.Collections.Generic
Imports System.Windows
Imports System.Windows.Controls
Imports System.Data
Imports DevExpress.Xpf.PivotGrid

Namespace WpfApplication52

    ''' <summary>
    ''' Interaction logic for MainWindow.xaml
    ''' </summary>
    Public Partial Class MainWindow
        Inherits Window

        Public Sub New()
            Me.InitializeComponent()
        End Sub

        Private Sub Window_Loaded(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Me.pivotGridControl1.DataSource = CreateDataSource()
            Me.pivotGridControl1.BeginUpdate()
            Me.pivotGridControl1.Fields.Add("Date", FieldArea.ColumnArea).GroupInterval = FieldGroupInterval.Date
            Me.pivotGridControl1.Fields.Add("Name", FieldArea.RowArea)
            Me.pivotGridControl1.Fields.Add("Value", FieldArea.DataArea)
            Dim fieldYear As PivotGridField = Me.pivotGridControl1.Fields.Add("Date", FieldArea.RowArea)
            fieldYear.GroupInterval = FieldGroupInterval.DateYear
            fieldYear.Caption = "Year"
            fieldYear.AreaIndex = 0
            Me.pivotGridControl1.EndUpdate()
        End Sub

        Private Function CreateDataSource() As DataTable
            Dim myTable As DataTable = New DataTable()
            myTable.Columns.Add("Name", GetType(String))
            myTable.Columns.Add("Date", GetType(Date))
            myTable.Columns.Add("Value", GetType(Decimal))
            myTable.Rows.Add(New Object() {"Aaa", Date.Today, 7})
            myTable.Rows.Add(New Object() {"Aaa", Date.Today.AddDays(1), 4})
            myTable.Rows.Add(New Object() {"Bbb", Date.Today, 12})
            myTable.Rows.Add(New Object() {"Bbb", Date.Today.AddDays(1), 14})
            myTable.Rows.Add(New Object() {"Ccc", Date.Today, 11})
            myTable.Rows.Add(New Object() {"Ccc", Date.Today.AddDays(1), 10})
            myTable.Rows.Add(New Object() {"Aaa", Date.Today.AddYears(1), 4})
            myTable.Rows.Add(New Object() {"Aaa", Date.Today.AddYears(1).AddDays(1), 2})
            myTable.Rows.Add(New Object() {"Bbb", Date.Today.AddYears(1), 3})
            myTable.Rows.Add(New Object() {"Bbb", Date.Today.AddDays(1).AddYears(1), 1})
            myTable.Rows.Add(New Object() {"Ccc", Date.Today.AddYears(1), 8})
            myTable.Rows.Add(New Object() {"Ccc", Date.Today.AddDays(1).AddYears(1), 22})
            Return myTable
        End Function

        Private visibleValues As Dictionary(Of PivotGridField, List(Of Object)) = New Dictionary(Of PivotGridField, List(Of Object))()

        Private Sub pivotGridControl1_FieldFilterChanging(ByVal sender As Object, ByVal e As PivotFieldFilterChangingEventArgs)
            Dim pivot As PivotGridControl = CType(sender, PivotGridControl)
            For Each field As PivotGridField In pivot.Fields
                If field.Visible = False OrElse field.Area = FieldArea.DataArea OrElse field.Area = FieldArea.FilterArea Then Continue For
                visibleValues(field) = New List(Of Object)(field.GetVisibleValues())
            Next
        End Sub

        Private Sub pivotGridControl1_FieldFilterChanged(ByVal sender As Object, ByVal e As PivotFieldEventArgs)
            Dim pivot As PivotGridControl = TryCast(sender, PivotGridControl)
            pivot.BeginUpdate()
            For Each field As PivotGridField In pivot.Fields
                If field.Visible = False OrElse field.Area = FieldArea.DataArea OrElse field.Area = FieldArea.FilterArea Then Continue For
                CollapseAllNewValues(visibleValues(field), field.GetVisibleValues(), field)
            Next

            pivot.EndUpdate()
        End Sub

        Private Sub CollapseAllNewValues(ByVal oldValues As List(Of Object), ByVal newValues As List(Of Object), ByVal field As PivotGridField)
            For Each val As Object In newValues
                If Not oldValues.Contains(val) Then field.CollapseValue(val)
            Next
        End Sub
    End Class
End Namespace
