using System;
using System.Collections.Generic;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm.Gantt;
using DevExpress.Mvvm.POCO;

namespace WpfApp3
{
    public class WindowGanttViewModel
    {
        public List<GanttTask> Items { get; private set; }
        public IEnumerable<GanttStripLineBase> StripLines { get; private set; }
        public List<GanttResource> Resources { get; private set; }

        readonly DateTime ProjectStart = new DateTime(2018, 10, 25, 8, 0, 0);
        readonly DateTime ProjectFinish = new DateTime(2019, 4, 16, 12, 0, 0);

        public WindowGanttViewModel()
        {
            this.Items = CreateData();
            this.Resources = CreateResources();
            this.StripLines = CreateStripLinesData();
        }

        List<GanttTask> CreateData()
        {
            var tasks = new List<GanttTask>();
            #region Data generation
            tasks.Add(new GanttTask { Id = 0, Name = "New Business", StartDate = new DateTime(2018, 10, 25, 8, 0, 0), FinishDate = new DateTime(2018, 10, 31, 12, 0, 0), BaselineStartDate = new DateTime(2018, 10, 25, 8, 0, 0), BaselineFinishDate = new DateTime(2018, 10, 30, 17, 0, 0) });
            tasks.Add(new GanttTask { Id = 1, ParentId = 0, Name = "Phase 1 - Strategic Plan", StartDate = new DateTime(2018, 10, 25, 8, 0, 0), FinishDate = new DateTime(2018, 10, 27, 12, 0, 0), BaselineStartDate = new DateTime(2018, 10, 25, 8, 0, 0), BaselineFinishDate = new DateTime(2018, 10, 26, 17, 0, 0) });
            tasks.Add(new GanttTask { Id = 2, ParentId = 0, Name = "Phase 1 - Strategic Plan", StartDate = new DateTime(2018, 10, 28, 8, 0, 0), FinishDate = new DateTime(2018, 10, 30, 12, 0, 0), BaselineStartDate = new DateTime(2018, 10, 28, 8, 0, 0), BaselineFinishDate = new DateTime(2018, 10, 30, 17, 0, 0) });

            tasks[1].PredecessorLinks.Add(new GanttPredecessorLink() { PredecessorTaskId = 0 });
            tasks[2].PredecessorLinks.Add(new GanttPredecessorLink() { PredecessorTaskId = 0 });
           
            tasks[1].ResourceLinks.Add(new GanttResourceLink() { ResourceId = 1 });
            tasks[2].ResourceLinks.Add(new GanttResourceLink() { ResourceId = 2 });
            
            #endregion
            return tasks;
        }
        List<GanttResource> CreateResources()
        {
            var resources = new List<GanttResource>();
            #region Data generation
            resources.Add(new GanttResource { Name = "Business Advisor", Id = 1 });
            resources.Add(new GanttResource { Name = "Peers", Id = 2 });
            resources.Add(new GanttResource { Name = "Lawyer", Id = 3 });
            resources.Add(new GanttResource { Name = "Government Agency", Id = 4 });
            resources.Add(new GanttResource { Name = "Manager", Id = 5 });
            resources.Add(new GanttResource { Name = "Owners", Id = 6 });
            resources.Add(new GanttResource { Name = "Accountant", Id = 7 });
            resources.Add(new GanttResource { Name = "Banker", Id = 8 });
            resources.Add(new GanttResource { Name = "Information Services", Id = 9 });

            #endregion
            return resources;
        }
        IEnumerable<GanttStripLineBase> CreateStripLinesData()
        {
            return new GanttStripLineBase[] {
                GanttStripLine.Create(ProjectStart, TimeSpan.Zero, "Project Start"),
                GanttStripLine.Create(ProjectFinish, TimeSpan.Zero, "Project Finish"),
                WeeklyStripLine.Create(DayOfWeek.Wednesday, ProjectStart, TimeSpan.FromHours(15), ProjectFinish, TimeSpan.FromMinutes(90), "Weekly meeting"),
            };
        }
    }
    public abstract class GanttStripLineBase
    {
        public virtual DateTime StartDate { get; set; }
        public virtual TimeSpan Duration { get; set; }
        public virtual string Caption { get; set; }

        protected GanttStripLineBase(DateTime startDate, TimeSpan duration, string caption)
        {
            StartDate = startDate;
            Duration = duration;
            Caption = caption;
        }
    }

    public class GanttStripLine : GanttStripLineBase
    {
        protected GanttStripLine(DateTime startDate, TimeSpan duration, string caption)
            : base(startDate, duration, caption) { }

        public static GanttStripLine Create(DateTime startDate, TimeSpan duration, string caption)
        {
            return ViewModelSource.Create(() => new GanttStripLine(startDate, duration, caption));
        }
    }
    public class WeeklyStripLine : GanttStripLineBase
    {
        public virtual DateTime FinishDate { get; set; }
        public virtual DayOfWeek DayOfWeek { get; set; }
        public virtual TimeSpan StartTime { get; set; }

        protected WeeklyStripLine(DayOfWeek dayOfWeek, DateTime startDate, TimeSpan startTime, DateTime finishDate, TimeSpan duration, string caption)
            : base(startDate, duration, caption)
        {
            DayOfWeek = dayOfWeek;
            FinishDate = finishDate;
            StartTime = startTime;
        }

        public static WeeklyStripLine Create(DayOfWeek dayOfWeek, DateTime startDate, TimeSpan startTime, DateTime finishDate, TimeSpan duration, string caption)
        {
            return ViewModelSource.Create(() => new WeeklyStripLine(dayOfWeek, startDate, startTime, finishDate, duration, caption));
        }
    }
}