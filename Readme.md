<!-- default file list -->
*Files to look at*:

* [MainWindow.xaml](./CS/WpfApplication52/MainWindow.xaml) (VB: [MainWindow.xaml](./VB/WpfApplication52/MainWindow.xaml))
* [MainWindow.xaml.cs](./CS/WpfApplication52/MainWindow.xaml.cs) (VB: [MainWindow.xaml.vb](./VB/WpfApplication52/MainWindow.xaml.vb))
<!-- default file list end -->
# How to collapse new Field Values


<p>The easiest way to accomplish this task is to compare lists of visible values before and after data is customized. In the attached example, all values are collapsed after applying a filter. Following methods and events are used to accomplish this task:</p><p><a href="http://documentation.devexpress.com/#WPF/DevExpressXpfPivotGridPivotGridField_GetVisibleValuestopic">PivotGridField.GetVisibleValues</a>  return the list of visible values.<br />
<a href="http://documentation.devexpress.com/#WPF/DevExpressXpfPivotGridPivotGridField_CollapseValuetopic">PivotGridField.CollapseValue</a>  collapses a specific field value.<br />
<a href="http://documentation.devexpress.com/#WPF/DevExpressXpfPivotGridPivotGridControl_FieldFilterChangingtopic">PivotGridControl.FieldFilterChanging</a>  occurs before a filter is applied. <br />
<a href="http://documentation.devexpress.com/#WPF/DevExpressXpfPivotGridPivotGridControl_FieldFilterChangedtopic">PivotGridControl.FieldFilterChanged</a>  occurs after applying a filter.</p>

<br/>


