using System;
using System.Windows;
using System.Windows.Input;
using DevExpress.Xpf.Core.Commands;
using System.Collections.ObjectModel;
using WpfApp3.Model;

namespace WpfApp3
{
    public class WindowGanttScheduleViewModel
    {
        public ObservableCollection<ModelAppointment> Appointments { get; private set; }
        public ObservableCollection<ModelResource> Resources { get; private set; }

        public ICommand AddNewAppointmentCommand { get; private set; }
        public ICommand GetSourceObjectCommand { get; private set; }

        public WindowGanttScheduleViewModel()
        {
            Appointments = new ObservableCollection<ModelAppointment>();
            Resources = new ObservableCollection<ModelResource>();

            AddNewAppointmentCommand = new DevExpress.Mvvm.DelegateCommand<object>(AddNewAppointmentCommandExecute);
            GetSourceObjectCommand = new DevExpress.Mvvm.DelegateCommand<object>(GetSourceObjectCommandExecute);

            AddTestData();
        }

        private void AddNewAppointmentCommandExecute(object parameter)
        {
            DateTime baseTime = DateTime.Today;

            ModelAppointment apt = new ModelAppointment()
            {
                StartTime = baseTime.AddHours(3),
                EndTime = baseTime.AddHours(4),
                Subject = "Test3",
                Location = "Office",
                Description = "Test procedure",
                Price = 20m
            };

            Appointments.Add(apt);
        }

        private void GetSourceObjectCommandExecute(object parameter)
        {
            DevExpress.Xpf.Scheduler.SchedulerStorage storage = (DevExpress.Xpf.Scheduler.SchedulerStorage)parameter;

            if (storage.AppointmentStorage.Count > 0)
            {
                ModelAppointment apt = (ModelAppointment)storage.AppointmentStorage[0].GetSourceObject(storage.GetCoreStorage());
                // Alternative: ModelAppointment apt = (ModelAppointment)storage.GetObjectRow(storage.AppointmentStorage[0]);
                MessageBox.Show("First Appointment Price: " + apt.Price.ToString());
            }
        }

        private void AddTestData()
        {
            ModelResource res1 = new ModelResource()
            {
                Id = 0,
                Name = "Resource1",
                Color = ToRgb(System.Drawing.Color.LightGray)
            };

            ModelResource res2 = new ModelResource()
            {
                Id = 1,
                Name = "Resource2",
                Color = ToRgb(System.Drawing.Color.White)
            };

            ModelResource res3 = new ModelResource()
            {
                Id = 2,
                Name = "Resource3",
                Color = ToRgb(System.Drawing.Color.LightGray)
            };

            ModelResource res4 = new ModelResource()
            {
                Id = 3,
                Name = "Resource4",
                Color = ToRgb(System.Drawing.Color.White)
            };

            Resources.Add(res1);
            Resources.Add(res2);
            Resources.Add(res3);
            Resources.Add(res4);

            DateTime baseTime = DateTime.Today;

            ModelAppointment apt1 = new ModelAppointment()
            {
                StartTime = baseTime.AddHours(1),
                EndTime = baseTime.AddHours(2),
                Subject = "Test",
                Location = "Office",
                Description = "Test procedure",
                Price = 10m
            };

            ModelAppointment apt2 = new ModelAppointment()
            {
                StartTime = baseTime.AddHours(2),
                EndTime = baseTime.AddHours(3),
                Subject = "Test2",
                Location = "Office",
                Description = "Test procedure",
                ResourceId = "<ResourceIds>\r\n<ResourceId Type=\"System.Int32\" Value=\"0\" />\r\n<ResourceId Type=\"System.Int32\" Value=\"1\" />\r\n</ResourceIds>"
            };

            Appointments.Add(apt1);
            Appointments.Add(apt2);
        }

        private int ToRgb(System.Drawing.Color color)
        {
            return color.B << 16 | color.G << 8 | color.R;
        }
    }
}