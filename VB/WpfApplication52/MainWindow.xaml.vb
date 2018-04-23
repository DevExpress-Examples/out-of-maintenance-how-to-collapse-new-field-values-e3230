Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Data
Imports System.Windows.Documents
Imports System.Windows.Input
Imports System.Windows.Media
Imports System.Windows.Media.Imaging
Imports System.Windows.Navigation
Imports System.Windows.Shapes
Imports System.Data
Imports DevExpress.Xpf.PivotGrid

Namespace WpfApplication52
	''' <summary>
	''' Interaction logic for MainWindow.xaml
	''' </summary>
	Partial Public Class MainWindow
		Inherits Window
		Public Sub New()
			InitializeComponent()
		End Sub

		Private Sub Window_Loaded(ByVal sender As Object, ByVal e As RoutedEventArgs)
			pivotGridControl1.DataSource = CreateDataSource()
			pivotGridControl1.BeginUpdate()
			pivotGridControl1.Fields.Add("Date", DevExpress.Xpf.PivotGrid.FieldArea.ColumnArea).GroupInterval = DevExpress.Xpf.PivotGrid.FieldGroupInterval.Date
			pivotGridControl1.Fields.Add("Name", DevExpress.Xpf.PivotGrid.FieldArea.RowArea)
			pivotGridControl1.Fields.Add("Value", DevExpress.Xpf.PivotGrid.FieldArea.DataArea)

			Dim fieldYear As PivotGridField = pivotGridControl1.Fields.Add("Date", DevExpress.Xpf.PivotGrid.FieldArea.RowArea)
			fieldYear.GroupInterval = DevExpress.Xpf.PivotGrid.FieldGroupInterval.DateYear
			fieldYear.Caption = "Year"
			fieldYear.AreaIndex = 0

			pivotGridControl1.EndUpdate()



		End Sub

		Private Function CreateDataSource() As DataTable
			Dim myTable As New DataTable()
			myTable.Columns.Add("Name", GetType(String))
			myTable.Columns.Add("Date", GetType(DateTime))
			myTable.Columns.Add("Value", GetType(Decimal))

			myTable.Rows.Add(New Object() { "Aaa", DateTime.Today, 7 })
			myTable.Rows.Add(New Object() { "Aaa", DateTime.Today.AddDays(1), 4 })
			myTable.Rows.Add(New Object() { "Bbb", DateTime.Today, 12 })
			myTable.Rows.Add(New Object() { "Bbb", DateTime.Today.AddDays(1), 14 })
			myTable.Rows.Add(New Object() { "Ccc", DateTime.Today, 11 })
			myTable.Rows.Add(New Object() { "Ccc", DateTime.Today.AddDays(1), 10 })

			myTable.Rows.Add(New Object() { "Aaa", DateTime.Today.AddYears(1), 4 })
			myTable.Rows.Add(New Object() { "Aaa", DateTime.Today.AddYears(1).AddDays(1), 2 })
			myTable.Rows.Add(New Object() { "Bbb", DateTime.Today.AddYears(1), 3 })
			myTable.Rows.Add(New Object() { "Bbb", DateTime.Today.AddDays(1).AddYears(1), 1 })
			myTable.Rows.Add(New Object() { "Ccc", DateTime.Today.AddYears(1), 8 })
			myTable.Rows.Add(New Object() { "Ccc", DateTime.Today.AddDays(1).AddYears(1), 22 })
			Return myTable

		End Function

		Private visibleValues As New Dictionary(Of PivotGridField, List(Of Object))()
		Private Sub pivotGridControl1_FieldFilterChanging(ByVal sender As Object, ByVal e As PivotFieldFilterChangingEventArgs)
			Dim pivot As PivotGridControl =CType(sender, PivotGridControl)
			For Each field As PivotGridField In pivot.Fields
				If field.Visible = False OrElse field.Area = FieldArea.DataArea OrElse field.Area = FieldArea.FilterArea Then
					Continue For
				End If
				visibleValues(field) = New List(Of Object)(field.GetVisibleValues())

			Next field
		End Sub

		Private Sub pivotGridControl1_FieldFilterChanged(ByVal sender As Object, ByVal e As PivotFieldEventArgs)
			Dim pivot As PivotGridControl = TryCast(sender, PivotGridControl)
			pivot.BeginUpdate()
			For Each field As PivotGridField In pivot.Fields
				If field.Visible = False OrElse field.Area = FieldArea.DataArea OrElse field.Area = FieldArea.FilterArea Then
					Continue For
				End If
				CollapseAllNewValues(visibleValues(field), field.GetVisibleValues(), field)
			Next field
			pivot.EndUpdate()
		End Sub

		Private Sub CollapseAllNewValues(ByVal oldValues As List(Of Object), ByVal newValues As List(Of Object), ByVal field As PivotGridField)
			For Each val As Object In newValues
				If (Not oldValues.Contains(val)) Then
					field.CollapseValue(val)
				End If
			Next val
		End Sub

	End Class
End Namespace
