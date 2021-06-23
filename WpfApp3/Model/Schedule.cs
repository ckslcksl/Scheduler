using System;

namespace WpfApp3
{
    #region #models    
    public class ModelAppointment
    {
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Subject { get; set; }
        public int Status { get; set; }
        public string Description { get; set; }
        public long Label { get; set; }
        public string Location { get; set; }
        public bool AllDay { get; set; }
        public int EventType { get; set; }
        public string RecurrenceInfo { get; set; }
        public string ReminderInfo { get; set; }
        public object ResourceId { get; set; }
        public decimal Price { get; set; }

        public ModelAppointment()
        {
        }
    }

    public class ModelResource
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Color { get; set; }

        public ModelResource()
        {
        }
    }
    #endregion #models
}
