using DevExpress.Mvvm;
using DevExpress.XtraScheduler;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Globalization;
using System.Linq;
using WpfApp3.Model;
using WpfApp3.Util;
using System.Data.SqlClient;


namespace WpfApp3
{
    class WindowGanttScheduleViewModel : ViewModelBase
    {
        static string connectionString = @"Server=165.243.45.197,1433; Database=lscnssm; uid=hscmuser; pwd=hscm!q2w3e4r;";
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter adapter;
        DataSet ds;

        public DelegateCommand ChangeVersionDateCommand { get; private set; }
        public DelegateCommand ChangeVersionModelsCommand { get; private set; }

        public virtual DateTime Start { get; set; }
        ///public IEnumerable<WorkCalendar> Calendars { get { return WorkData.Calendars; } }
        ///public IEnumerable<WorkAppointment> Appointments { get { return WorkData.Appointments; } }
        ///

        public VersionModel SelectedVersionDate
        {
            get { return GetValue<VersionModel>(nameof(SelectedVersionDate)); }
            set { SetValue(value, nameof(SelectedVersionDate)); }
        }
        public IEnumerable<VersionModel> VersionDate
        {
            get { return GetValue<IEnumerable<VersionModel>>(nameof(VersionDate)); }
            set { SetValue(value, nameof(VersionDate)); }
        }

        public VersionModel SelectedVersion
        {
            get { return GetValue<VersionModel>(nameof(SelectedVersion)); }
            set { SetValue(value, nameof(SelectedVersion)); }
        } 

        public IEnumerable<VersionModel> VersionModels
        {
            get { return GetValue<IEnumerable<VersionModel>>(nameof(VersionModels)); }
            set { SetValue(value, nameof(VersionModels)); }
        }
        /// Appointments
        public IEnumerable<ModelAppointment> ModelAppointments
        {
            get { return GetValue<IEnumerable<ModelAppointment>>(nameof(ModelAppointments)); }
            set { SetValue(value,nameof(ModelAppointments)); }
        }
        /// Resources 
        public IEnumerable<ModelResource> ModelResources
        {
            get { return GetValue<IEnumerable<ModelResource>>(nameof(ModelResources)); }
            set { SetValue(value, nameof(ModelResources)); }
        }

        void ChangeVersionDateExecute()
        {
            GenVersions();
        }
        void ChangeVersionModelsExecute()
        {
            GenSchedule();
        }

        public WindowGanttScheduleViewModel()
        {           
            Start = DateTime.Today.Date;

            ChangeVersionDateCommand = new DelegateCommand(ChangeVersionDateExecute);
            ChangeVersionModelsCommand = new DelegateCommand(ChangeVersionModelsExecute);

            GenVersionDates();
        }
        private void GenVersionDates()
        {
            List<VersionModel> versionDates = new List<VersionModel>();
            con = new SqlConnection(connectionString);
            cmd = new SqlCommand();
            cmd.Connection = con;
            con.Open();
            cmd.CommandText = @"SELECT [PLAN_STARTDATE]
                                FROM [lscnssm].[dbo].[MPI_VERSION]
                                GROUP BY PLAN_STARTDATE
                                ORDER BY PLAN_STARTDATE DESC";

            SqlDataReader sdr = cmd.ExecuteReader();

            while (sdr.Read())
            {
                VersionModel vm = new VersionModel()
                {
                    PlanStartDate = sdr["PLAN_STARTDATE"].ToString()
                };
                versionDates.Add(vm);
            }

            VersionDate = versionDates;
        }
        private void GenVersions()
        {
            
            string sDate = SelectedVersionDate.PlanStartDate;
            List<VersionModel> versions = new List<VersionModel>();
            con = new SqlConnection(connectionString);
            cmd = new SqlCommand();
            cmd.Connection = con;
            con.Open();
            cmd.CommandText = @"SELECT [VERSION]
                                  ,[VERSION_DESCR]
                                  ,[PLAN_STARTDATE]
                                  ,[PLAN_TYPE]
                              FROM [lscnssm].[dbo].[MPI_VERSION]
                              WHERE PLAN_STARTDATE = '"+ sDate +@"'
                              ORDER BY VERSION ASC";

            adapter = new SqlDataAdapter(cmd.CommandText, con);
            ds = new DataSet();
            adapter.Fill(ds);

            DataTable dt = ds.Tables[0];
            dt.DefaultView.Sort = "VERSION";
            foreach (DataRow dr in dt.DefaultView.ToTable().Rows)
            {
                VersionModel vm = new VersionModel()
                {
                    Version = dr["VERSION"].ToString(),
                    VersionDescr = dr["VERSION_DESCR"].ToString(),
                    PlanStartDate = dr["PLAN_STARTDATE"].ToString(),
                    PlanType = dr["PLAN_TYPE"].ToString()
                };
                versions.Add(vm);
            }

            VersionModels = versions;

        }
        private void GenSchedule()
        {
            try
            {
                object se = SelectedVersion.Version;
                var sv = SelectedVersion.Version;
                con = new SqlConnection(connectionString);
                cmd = new SqlCommand();
                cmd.Connection = con;
                con.Open();
                cmd.CommandText = @"
                                SELECT [VERSION]
                                      ,[SEQ_NO]
                                      ,[PLANT]
                                      ,[WC]
                                      ,[EQUIPMENT]
                                      ,[EQUIPMENT_DESCR]
                                      ,[EQUIPMENT_TYPE]
                                      ,[EQUIPMENT_GROUP]
                                      ,[SORT_NO]
                                      ,[UTILIZATION]
                                      ,[GANTT_DISPLAY_FLAG]
                                      ,[CREATED_BY]
                                      ,[CREATED_AT]
                                      ,[RES_CAPA_VALUE]
                                      ,[TT_INNER_DIAMETER]
                                      ,[TT_FULL_DIAMETER]
                                      ,[TT_HEIGHT]
                                  FROM [lscnssm].[dbo].[MPI_INPUT_EQUIPMENT] as ie
                                  where version = '" + sv + @"';
                                 SELECT  [VERSION]
                                    ,[PLANT]
                                    ,[WC]
                                    ,[EQUIPMENT]
                                    ,[LOT]
                                    ,[PRODUCT]
                                    ,[DEMAND_ID]
                                    ,[PLAN_QTY]
                                    ,[OPERATION]
                                    ,[START_TIME]
                                    ,[END_TIME]
                                    ,[EVENT]
							   FROM
							   (
							   SELECT [VERSION]
                                    ,[PLANT]
                                    ,[WC]
                                    ,[EQUIPMENT]
                                    ,[LOT]
                                    ,[PRODUCT]
                                    ,[DEMAND_ID]
                                    ,[PLAN_QTY]
                                    ,[OPERATION]
                                    ,[START_TIME]
                                    ,[END_TIME]
                                    ,[EVENT]
                                FROM [lscnssm].[dbo].[MPO_CONST_EQUIP_SCHEDULE]
								UNION
								 SELECT [VERSION]
                                    ,[PLANT]
                                    ,[WC]
                                    ,[EQUIPMENT]
                                    ,[LOT]
                                    ,[PRODUCT]
                                    ,[DEMAND_ID]
                                    ,[PLAN_QTY]
                                    ,[OPERATION]
                                    ,[START_TIME]
                                    ,[END_TIME]
                                    ,[EVENT]
                                FROM [lscnssm].[dbo].[MPO_EQUIP_SCHEDULE]
								) AS ap
                                WHERE ap.VERSION = '"+ sv + "';";

                adapter = new SqlDataAdapter(cmd.CommandText, con);
                ds = new DataSet();
                adapter.Fill(ds);

                DataTable dt = ds.Tables[0];
                DataTable dt1 = ds.Tables[1];

                con.Close();

                List<ModelAppointment> appointments = new List<ModelAppointment>();
                List<ModelResource> resources = new List<ModelResource>();

                dt.DefaultView.Sort = "EQUIPMENT";

                foreach (DataRow dr in dt.DefaultView.ToTable().Rows)
                {
                    ModelResource mr = new ModelResource()
                    {
                        Id = dr["EQUIPMENT"].ToString(),
                        Name = dr["EQUIPMENT_DESCR"].ToString(),
                        IsVisible = true,
                        Group = dr["PLANT"].ToString(),
                        Tag = dr["VERSION"].ToString()
                    };
                    resources.Add(mr);
                }


                dt1.DefaultView.Sort = "DEMAND_ID";
                int cnt1 = 1;
                var vc1 = "";

                foreach (DataRow dr in dt1.DefaultView.ToTable().Rows)
                {
                    if (!vc1.Equals(dr["DEMAND_ID"].ToString()))
                    {
                        cnt1++;
                    }
                    if (cnt1 > 24) { cnt1 = 1; }
                    ModelAppointment ma = new ModelAppointment()
                    {
                        Id = dr["EQUIPMENT"].ToString(),
                        AppointmentType = (int)AppointmentType.Normal,
                        AllDay = false,
                        Start = Convert.ToDateTime(dr["START_TIME"].ToString()),
                        End = Convert.ToDateTime(dr["END_TIME"].ToString()).AddSeconds(-1),
                        Subject = dr["DEMAND_ID"].ToString(),
                        Description = dr["PRODUCT"].ToString(),
                        CalendarId = dr["EQUIPMENT"].ToString(),
                        Label = cnt1.ToString()
                    };
                    vc1 = dr["DEMAND_ID"].ToString();
                    appointments.Add(ma);
                }
                ModelAppointments = appointments;
                ModelResources = resources;
            } 
            catch (NullReferenceException e)
            {
                Console.WriteLine(e.StackTrace);
            }
           
        }
    }
}
