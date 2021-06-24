using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.Diagram;
using System.Windows;
using WpfApp3.Model;

using DevExpress.Mvvm.UI.Interactivity;
using DevExpress.Xpf.Diagram.Themes;

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
            for (int i = 1; i < 12; i++)
            {
                this.timelineView.ZoomOut();
            }

        }

        private void scheduler_CompleteAppointmentDragDrop(object sender, DevExpress.Xpf.Scheduling.CompleteAppointmentDragDropEventArgs e)
        {
            Console.WriteLine(e);
        }

        private void scheduler_DropAppointment(object sender, DevExpress.Xpf.Scheduling.DropAppointmentEventArgs e)
        {
            Console.WriteLine(e);
            System.Collections.Generic.IReadOnlyList<DevExpress.Xpf.Scheduling.AppointmentItem> aaa = e.DragAppointments;
            // DevExpress.Xpf.Scheduling.SchedulerControl bbb = e.OriginalSource;
            object res = aaa[0].ResourceId;
            Console.WriteLine(res);
            return;
        }
    }
    


}
