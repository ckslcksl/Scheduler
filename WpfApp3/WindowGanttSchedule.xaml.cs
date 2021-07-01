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

using DevExpress.Xpf.Scheduler;
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
            for (int i = 1; i < 12; i++)
            {
                this.timelineView.ZoomOut();
            }

        }

        private void scheduler_DropAppointment(object sender, DevExpress.Xpf.Scheduling.DropAppointmentEventArgs e)
        {
            DevExpress.Xpf.Scheduling.SchedulerControl scc = (DevExpress.Xpf.Scheduling.SchedulerControl)e.Source;
            

            string res1 = (string)e.DragAppointments[0].ResourceId;
            string res2 = (string)scc.SelectedAppointments[0].ResourceId;

            if (res1.Substring(0,5) == res2.Substring(0,5))
            {
                e.Cancel = false;
            }
            else
            {
                e.Cancel = true;
            }

        }
        private void scheduler_DragAppointmentOver(object sender, DevExpress.Xpf.Scheduling.DragAppointmentOverEventArgs e)
        {
            if (e.DragAppointments.Count > 1)
            {
                scheduler.AllowAppointmentDragBetweenResources = false;
            }
            else
            {
                scheduler.AllowAppointmentDragBetweenResources = true;
            }
        }
        #region #About_ItemClick
        void About_ItemClick(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {
            DXMessageBox.Show(@"This example demonstrates how to customize the WPF Scheduler's integrated ribbon UI. Use the Scheduler's RibbonActions collection to create, remove or modify ribbon elements.",
                "Scheduler Ribbon Example", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        #endregion #About_ItemClick
    }

}
