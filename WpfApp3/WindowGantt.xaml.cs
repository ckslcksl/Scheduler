using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using DevExpress.Xpf.Core;
using DevExpress.Xpf.Diagram;
using WpfApp3.Model;

using DevExpress.Mvvm.UI.Interactivity;
using DevExpress.Xpf.Diagram.Themes;
using System.Windows.Markup;
using DevExpress.Mvvm;
using System.Globalization;

namespace WpfApp3
{
    /// <summary>
    /// WindowGantt.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class WindowGantt : BaseWindow
    {
        public WindowGantt(string versionNm)
        { 
            InitializeComponent();
        }
    }

    public class StripLineTemplateSelector : DataTemplateSelector
    {
        public DataTemplate StripLineTemplate { get; set; }
        public DataTemplate WeeklyStripLineTemplate { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (item is GanttStripLine)
                return StripLineTemplate;
            if (item is WeeklyStripLine)
                return WeeklyStripLineTemplate;
            return base.SelectTemplate(item, container);
        }
    }

    public class DayOfWeekToDaysOfWeekConverter : MarkupExtension, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Enum.Parse(typeof(DaysOfWeek), Enum.GetName(typeof(DayOfWeek), value));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }
}
