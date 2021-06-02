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

using DevExpress.XtraScheduler;

namespace WpfApp3
{
    /// <summary>
    /// WindowGanttSchedule.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class WindowGanttSchedule : BaseWindow
    {
        public WindowGanttSchedule(string versionNm)
        {
            InitializeComponent();
            MyCustomScale myScale = new MyCustomScale();
            schedulerControl1.TimelineView.Scales.Add(myScale);
            ///schedulerControl1.TimelineView.Scales.Last().Width = 3;
            TimeScaleFixedInterval scale1Minutes = new TimeScaleFixedInterval(TimeSpan.FromMinutes(1));
            schedulerControl1.TimelineView.Scales.Add(scale1Minutes);
            schedulerControl1.TimelineView.Scales.GetById(scale1Minutes).Width = 1;
        }
    }
    public class MyCustomScale : TimeScaleHour
    {
        public MyCustomScale() { }

        public override string DisplayName { get => "Custom Work Hours"; set => base.DisplayName = value; }
        public override string MenuCaption { get => "Custom Work Hours"; set => base.MenuCaption = value; }

        public override string FormatCaption(DateTime start, DateTime end)
        {
            if (start.Hour <= 12) return start.Hour.ToString() + " AM";
            else return (start.Hour - 12).ToString() + " PM";
        }
        public override bool IsDateVisible(DateTime date)
        {
            if (date.Hour >= 8 && date.Hour <= 18)
                return !(date.Hour == 14);
            else return false;
        }
    }

}
