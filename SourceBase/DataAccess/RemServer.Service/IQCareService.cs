using System;
using System.Data;
using System.Data.SqlClient;
using System.ServiceProcess;
using System.Diagnostics;

using System.Runtime;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
using System.Runtime.Serialization;

using System.Collections;
using System.Collections.Specialized;
using System.Configuration;
using Application.BusinessProcess;
using System.Windows.Forms;
using Application.Common;

//for auto backup service
using System.Threading;
using System.Runtime.InteropServices;
using System.Text;
using System.IO;

namespace RemServer.Service
{
    public class IQCareService : ServiceBase 
    {
        private System.ComponentModel.Container theContainer = null;
        private static EventLog theLog = new EventLog();
        public static string theSRV_Name = "IQCare";
        SqlConnection cnBKTest;
        public DateTime dtBackupTime;
        public string strBackupDrive;
        public string constr;
        System.Threading.Timer oTimer;

        public IQCareService()
        {
            this.ServiceName = theSRV_Name;
        }

        static void Main()
        {
            try
            {
                //theLog.Log = "IQCare";
                //theLog.MaximumKilobytes = 4096;
                theLog.Source = theSRV_Name;
                theLog.WriteEntry(string.Format("{0} Initializing", theSRV_Name));
                ServiceBase.Run(new IQCareService());
            }
            catch(Exception err)
            {
                theLog.WriteEntry(err.ToString());
            }
        }

        private void InitializeComponent()
        {
            theContainer = new System.ComponentModel.Container();
            this.ServiceName = "IQCare";
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (theContainer != null)
                {
                    theContainer.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        protected override void OnStart(string[] args)
        {
            try
            {
                FileWatcherLab();

                Process theProc = Process.GetCurrentProcess();
                string Config = theProc.MainModule.FileName;
                Config = Config + ".config";

                #region "Connection Parameters"
                Utility clsUtil = new Utility();
                constr = clsUtil.Decrypt(((NameValueCollection)ConfigurationSettings.GetConfig("appSettings"))["ConnectionString"]);
                if (constr.Trim() == "")
                {
                    frmConnection frm = new frmConnection();
                    frm.ShowDialog();
                    constr = ((NameValueCollection)ConfigurationSettings.GetConfig("appSettings"))["ConnectionString"];
                    if (constr == "")
                    {
                        Environment.Exit(1);
                    }
                }
                #endregion
                DoDelayedTasks();
                CreateNutritionXML();
                
                RemotingConfiguration.Configure(Config);
                RemotingConfiguration.RegisterWellKnownServiceType(typeof(BusinessServerFactory), "BusinessProcess.rem", WellKnownObjectMode.Singleton);
                //theLog.WriteEntry(string.Format("{0} Started", theSRV_Name));
            }
            catch(Exception err)  
            {
                theLog.WriteEntry(err.Message);
            }
        }

        /// <summary>
        /// This process will pick the backup time and backdrive from database.
        /// </summary>
        public void DoDelayedTasks()
        {
            cnBKTest = new SqlConnection(constr);
            const Int32 iTIME_INTERVAL = 50000; //    ' 50 seconds.
            TimerCallback timerDelegate = new TimerCallback(DBEntry);
            oTimer = new System.Threading.Timer(timerDelegate, null, 0, iTIME_INTERVAL);
        }

        //creates a nutrition XML from NHP tool
        public void CreateNutritionXML()
        {
            cnBKTest = new SqlConnection(constr);
            Int32 nutritionTIME_INTERVAL = Convert.ToInt32(((NameValueCollection)ConfigurationSettings.GetConfig("appSettings"))["NutritionInterval"]); // 30000; //    ' 30 seconds.
            TimerCallback NutritiontimerDelegate = new TimerCallback(NutritionXML);
            oTimer = new System.Threading.Timer(NutritiontimerDelegate, null, 0, nutritionTIME_INTERVAL);
        }

        protected override void OnStop()
        {
            try
            {
                //theLog.WriteEntry(string.Format("{0} Stopped.", theSRV_Name));
            }
            catch(Exception err)
            {
                theLog.WriteEntry(err.Message); 
            }
        }

        /// <summary>
        /// For automated backup service.
        /// This function takes the backup when server time matches with user specified time.
        /// </summary>
        /// <param name="Message"></param>

        public void DBEntry(Object Message)
        {
            try
            {
                SqlCommand cmdTest;
                cmdTest = new SqlCommand("pr_SystemAdmin_GetBackupTime_Constella", cnBKTest);
                cmdTest.CommandType = CommandType.StoredProcedure;
                int theTimeOut = Convert.ToInt32(((NameValueCollection)ConfigurationSettings.GetConfig("appSettings"))["CommandTimeOut"]);
                cmdTest.CommandTimeout = theTimeOut;
                cnBKTest.Open();
                SqlDataReader readerBackupDetail;
                readerBackupDetail = cmdTest.ExecuteReader();
                if (readerBackupDetail.HasRows)
                {
                    readerBackupDetail.Read();
                    if (readerBackupDetail["BackupTime"].ToString() != "" || readerBackupDetail.IsDBNull(0) != true)
                        dtBackupTime = (DateTime)readerBackupDetail["BackupTime"];
                    if (readerBackupDetail["BackupDrive"].ToString() != "" || readerBackupDetail.IsDBNull(1) != true)
                        strBackupDrive = (string)readerBackupDetail["BackupDrive"];

                }
                cnBKTest.Close();

                if (dtBackupTime.ToString("hh:mm") == DateTime.Now.ToString("hh:mm"))
                {
                    //this.RequestAdditionalTime(50000);
                    cmdTest = new SqlCommand("pr_SystemAdmin_Backup_Constella", cnBKTest);
                    cmdTest.CommandType = CommandType.StoredProcedure;
                    cmdTest.Parameters.Add(new SqlParameter("@FileName", SqlDbType.VarChar, 500));
                    cmdTest.Parameters["@FileName"].Value = strBackupDrive + "\\IQCareDBBackup";
                    cmdTest.Parameters.Add("@Deidentified", SqlDbType.Int).Value = 0;
                    cmdTest.Parameters.Add("@LocationId", SqlDbType.Int).Value = 0;
                    cmdTest.Parameters.Add("@dbKey", SqlDbType.VarChar).Value = ApplicationAccess.DBSecurity.ToString(); 
                    cmdTest.CommandTimeout = theTimeOut;
                    cnBKTest.Open();
                    cmdTest.ExecuteNonQuery();
                }
                cnBKTest.Close();
            }
            catch (Exception err)
            {
                cnBKTest.Close();
                theLog.WriteEntry(err.Message);
            }
        }

        public void NutritionXML(Object Message)
        {
            try
            {
                SqlCommand cmd;
                cmd = new SqlCommand("pr_CreateNutritionXML", cnBKTest);
                cmd.CommandType = CommandType.StoredProcedure;
                int theTimeOut = Convert.ToInt32(((NameValueCollection)ConfigurationSettings.GetConfig("appSettings"))["CommandTimeOut"]);
                cmd.CommandTimeout = theTimeOut;
                cnBKTest.Open();
                cmd.ExecuteNonQuery();
                cnBKTest.Close();
            }
            catch (Exception err)
            {
                cnBKTest.Close();
                theLog.WriteEntry(err.Message);
            }
        }

        public void FileWatcherLab()
        {
            theLog.WriteEntry("filewatcher" + DateTime.Now);
            FileSystemWatcher m_Watcher = new FileSystemWatcher("C:\\csvFolder", "*.*");
            m_Watcher.NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite
                                     | NotifyFilters.FileName | NotifyFilters.DirectoryName;
            //m_Watcher.Changed += new FileSystemEventHandler(OnChanged);
            m_Watcher.Created += new FileSystemEventHandler(OnChanged);
            //m_Watcher.Deleted += new FileSystemEventHandler(OnChanged);
            //m_Watcher.Renamed += new RenamedEventHandler(OnRenamed);
            m_Watcher.EnableRaisingEvents = true;
        }

        private void OnChanged(object sender, FileSystemEventArgs e)
        {
            if (e.ChangeType.ToString() == "Created")
            {
                //MessageBox.Show(Path.GetDirectoryName(e.FullPath) + "\\" + Path.GetFileNameWithoutExtension(e.FullPath));// Path.GetFileNameWithoutExtension(e.FullPath));
                try
                {
                    string fileName = Path.GetDirectoryName(e.FullPath) + "\\" + Path.GetFileNameWithoutExtension(e.FullPath);
                    File.Move(e.FullPath, fileName + "_Processed" + Path.GetExtension(e.FullPath));
                }
                catch (Exception ex)
                {
                }
            }
        }

        public void readCSV()
        {
            var reader = new StreamReader(File.OpenRead(@"C:\csvFolder\211114.csv"));
            DataTable dt = new DataTable();
            dt.Columns.Add("PatientID", typeof(string));
            dt.Columns.Add("Date", typeof(string));
            dt.Columns.Add("CD4%", typeof(string));
            dt.Columns.Add("CD4 Count", typeof(string));
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                var values = line.Split(',');
                DataRow dr = dt.NewRow();
                if (values[4].ToString() != "Sample ID")
                {
                    dr[0] = values[4];
                    dr[1] = values[6] + " " + values[7] + " " + values[8];
                    dr[2] = values[11];
                    dr[3] = values[12];
                    dt.Rows.Add(dr);
                }
            }
        }

        


    }
}
