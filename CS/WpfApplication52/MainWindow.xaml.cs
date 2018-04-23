using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data;
using DevExpress.Xpf.PivotGrid;

namespace WpfApplication52
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            pivotGridControl1.DataSource = CreateDataSource();
            pivotGridControl1.BeginUpdate();
            pivotGridControl1.Fields.Add("Date", DevExpress.Xpf.PivotGrid.FieldArea.ColumnArea).GroupInterval = DevExpress.Xpf.PivotGrid.FieldGroupInterval.Date;
            pivotGridControl1.Fields.Add("Name", DevExpress.Xpf.PivotGrid.FieldArea.RowArea);
            pivotGridControl1.Fields.Add("Value", DevExpress.Xpf.PivotGrid.FieldArea.DataArea);

            PivotGridField fieldYear = pivotGridControl1.Fields.Add("Date", DevExpress.Xpf.PivotGrid.FieldArea.RowArea);
            fieldYear.GroupInterval  = DevExpress.Xpf.PivotGrid.FieldGroupInterval.DateYear;
            fieldYear.Caption = "Year";
            fieldYear.AreaIndex = 0;

            pivotGridControl1.EndUpdate();



        }

        private DataTable CreateDataSource()
        {
            DataTable myTable = new DataTable();
            myTable.Columns.Add("Name", typeof(string));
            myTable.Columns.Add("Date", typeof(DateTime));
            myTable.Columns.Add("Value", typeof(decimal));

            myTable.Rows.Add(new object[] { "Aaa", DateTime.Today, 7 });
            myTable.Rows.Add(new object[] { "Aaa", DateTime.Today.AddDays(1), 4 });
            myTable.Rows.Add(new object[] { "Bbb", DateTime.Today, 12 });
            myTable.Rows.Add(new object[] { "Bbb", DateTime.Today.AddDays(1), 14 });
            myTable.Rows.Add(new object[] { "Ccc", DateTime.Today, 11 });
            myTable.Rows.Add(new object[] { "Ccc", DateTime.Today.AddDays(1), 10 });

            myTable.Rows.Add(new object[] { "Aaa", DateTime.Today.AddYears(1), 4 });
            myTable.Rows.Add(new object[] { "Aaa", DateTime.Today.AddYears(1).AddDays(1), 2 });
            myTable.Rows.Add(new object[] { "Bbb", DateTime.Today.AddYears(1), 3 });
            myTable.Rows.Add(new object[] { "Bbb", DateTime.Today.AddDays(1).AddYears(1), 1 });
            myTable.Rows.Add(new object[] { "Ccc", DateTime.Today.AddYears(1), 8 });
            myTable.Rows.Add(new object[] { "Ccc", DateTime.Today.AddDays(1).AddYears(1), 22 });
            return myTable;

        }

        Dictionary<PivotGridField, List<object>> visibleValues = new Dictionary<PivotGridField,List<object>>();
        private void pivotGridControl1_FieldFilterChanging(object sender, PivotFieldFilterChangingEventArgs e)
        {
            PivotGridControl pivot =(PivotGridControl)sender;
            foreach (PivotGridField field in pivot.Fields )
            {
                if (field.Visible == false || field.Area == FieldArea.DataArea || field.Area == FieldArea.FilterArea) continue;
                visibleValues[field] = new  List<object>(field.GetVisibleValues());

            }
        }

        private void pivotGridControl1_FieldFilterChanged(object sender, PivotFieldEventArgs e)
        {
            PivotGridControl pivot = sender as PivotGridControl;
            pivot.BeginUpdate();
            foreach (PivotGridField field in pivot.Fields)
            {
                if (field.Visible == false || field.Area == FieldArea.DataArea || field.Area == FieldArea.FilterArea) continue;
                CollapseAllNewValues(visibleValues[field], field.GetVisibleValues(), field);
            }
            pivot.EndUpdate();
        }

        private void CollapseAllNewValues(List<object> oldValues, List<object> newValues, PivotGridField field)
        {
            foreach (object val in newValues)
            {
                if (!oldValues.Contains(val))
                    field.CollapseValue(val);
            }
        }

    }
}
