using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using System.Reflection;
using System.Data.SqlClient;
using System.Configuration;

namespace PresentationApp
{
    public partial class frmReadCSVFile : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //readCSV();
        }

        public void readCD4(string fileName)
        {
            var reader = new StreamReader(File.OpenRead(fileName));
            //var reader = new StreamReader(File.OpenRead(@"C:\csvFolder\211114.csv"));
            //List<string> listA = new List<string>();
            //List<string> listB = new List<string>();
            DataTable dt = new DataTable();
            dt.Columns.Add("PatientID",typeof(string));
            dt.Columns.Add("Date", typeof(string));
            dt.Columns.Add("CD4%", typeof(string));
            dt.Columns.Add("CD4 Count", typeof(string));
            dt.Columns.Add("User", typeof(string));
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                var values = line.Split(',');
                DataRow dr = dt.NewRow();
                if (values[4].ToString() != "Sample ID")
                {
                    dr[0] = values[4];
                    string date = values[6] + " " + values[7] + " " + values[8];
                    dr[1] = Convert.ToDateTime(date.Replace("\"", ""));
                    dr[2] = values[11];
                    dr[3] = values[12];
                    dr[4] = values[2];
                    dt.Rows.Add(dr);
                }

                //listA.Add(values[4]);
                //listB.Add(values[1]);
            }

            string constr = "data source=.\\mssqlserver2012;uid=sa; pwd=1;initial catalog=IQCare"; //clsUtil.Decrypt(((NameValueCollection)ConfigurationSettings.GetConfig("appSettings"))["ConnectionString"]);
            SqlConnection cnBKTest= new SqlConnection(constr);
            SqlCommand cmd;
            //CD4 Percent
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                //cmd = new SqlCommand("pr_LabSystemInteration", cnBKTest);
                cmd = new SqlCommand("Pr_Lab_ImportLabResults", cnBKTest);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@IPNumber", SqlDbType.VarChar, 50).Value = dt.Rows[i][0].ToString();
                cmd.Parameters.Add("@labName", SqlDbType.NVarChar, 50).Value = "CD4 Percent";
                cmd.Parameters.Add("@result ", SqlDbType.NVarChar, 50).Value = dt.Rows[i][2].ToString();
                cmd.Parameters.Add("@dateAnalysed ", SqlDbType.NVarChar, 50).Value = dt.Rows[i][1].ToString();
                cmd.Parameters.Add("@userName", SqlDbType.NVarChar, 50).Value = dt.Rows[i][4].ToString();
                //int theTimeOut = Convert.ToInt32(((NameValueCollection)ConfigurationSettings.GetConfig("appSettings"))["CommandTimeOut"]);
                //cmd.CommandTimeout = theTimeOut;
                cnBKTest.Open();
                cmd.ExecuteNonQuery();
                cnBKTest.Close();
            }

            //CD4 Count
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                //cmd = new SqlCommand("pr_LabSystemInteration", cnBKTest);
                cmd = new SqlCommand("Pr_Lab_ImportLabResults", cnBKTest);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@IPNumber", SqlDbType.VarChar, 50).Value = dt.Rows[i][0].ToString();
                cmd.Parameters.Add("@labName", SqlDbType.NVarChar, 50).Value = "CD4 Count";
                cmd.Parameters.Add("@result ", SqlDbType.NVarChar, 50).Value = dt.Rows[i][3].ToString();
                cmd.Parameters.Add("@dateAnalysed ", SqlDbType.NVarChar, 50).Value = dt.Rows[i][1].ToString();
                cmd.Parameters.Add("@userName", SqlDbType.NVarChar, 50).Value = dt.Rows[i][4].ToString();
                //int theTimeOut = Convert.ToInt32(((NameValueCollection)ConfigurationSettings.GetConfig("appSettings"))["CommandTimeOut"]);
                //cmd.CommandTimeout = theTimeOut;
                cnBKTest.Open();
                cmd.ExecuteNonQuery();
                cnBKTest.Close();
            }

            GridView1.DataSource = dt;
            GridView1.DataBind();
        }

        public void readALT(string fileName)
        {
            var reader = new StreamReader(File.OpenRead(fileName));
            //var reader = new StreamReader(File.OpenRead(@"C:\csvFolder\211114.csv"));
            //List<string> listA = new List<string>();
            //List<string> listB = new List<string>();
            DataTable dt = new DataTable();
            dt.Columns.Add("PatientID", typeof(string));
            dt.Columns.Add("Test", typeof(string));
            dt.Columns.Add("Result", typeof(string));
            dt.Columns.Add("Resultime", typeof(string));
            //dt.Columns.Add("User", typeof(string));
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                var values = line.Split(';');
                DataRow dr = dt.NewRow();
                if (values[1].ToString() != "Patient Id")
                {
                    dr[0] = values[1];
                    string date = values[6] + " " + values[7] + " " + values[8];
                    //dr[1] = Convert.ToDateTime(date.Replace("\"", ""));
                    dr[1] = values[2];
                    dr[2] = values[4].Replace("\"","");
                    dr[3] = Convert.ToDateTime(String.Format("{0:d/M/yyyy HH:mm:ss}", values[10].Replace("\"", "")));
                    dt.Rows.Add(dr);
                }

                //listA.Add(values[4]);
                //listB.Add(values[1]);
            }

            string constr = "data source=.\\mssqlserver2012;uid=sa; pwd=1;initial catalog=IQCare"; //clsUtil.Decrypt(((NameValueCollection)ConfigurationSettings.GetConfig("appSettings"))["ConnectionString"]);
            SqlConnection cnBKTest = new SqlConnection(constr);
            SqlCommand cmd;
            //cnBKTest = new SqlConnection(constr);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                cmd = new SqlCommand("Pr_Lab_ImportLabResults", cnBKTest);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@IPNumber", SqlDbType.VarChar, 50).Value = dt.Rows[i][0].ToString();
                cmd.Parameters.Add("@labName", SqlDbType.NVarChar, 50).Value = dt.Rows[i][1].ToString();
                cmd.Parameters.Add("@result ", SqlDbType.NVarChar, 50).Value = dt.Rows[i][2].ToString();
                cmd.Parameters.Add("@dateAnalysed ", SqlDbType.NVarChar, 50).Value = dt.Rows[i][3].ToString();
                //cmd.Parameters.Add("@User", SqlDbType.NVarChar, 50).Value = dt.Rows[0][4].ToString();
                //int theTimeOut = Convert.ToInt32(((NameValueCollection)ConfigurationSettings.GetConfig("appSettings"))["CommandTimeOut"]);
                //cmd.CommandTimeout = theTimeOut;
                cnBKTest.Open();
                cmd.ExecuteNonQuery();
                cnBKTest.Close();
            }

            GridView1.DataSource = dt;
            GridView1.DataBind();
        }


        protected void Button1_Click(object sender, EventArgs e)
        {
            if (FileUpload1.HasFile)
            {
                string FileName = Path.GetFileName(FileUpload1.PostedFile.FileName);
                string Extension = Path.GetExtension(FileUpload1.PostedFile.FileName);
                string FolderPath = ConfigurationManager.AppSettings["FolderPath"];

                string FilePath = Server.MapPath(FolderPath + FileName);
                FileUpload1.SaveAs(FilePath);
                if (RadioButtonList1.SelectedValue == "CD4")
                    readCD4(FilePath);
                else if (RadioButtonList1.SelectedValue == "ALT")
                    readALT(FilePath);
                else
                    System.Windows.Forms.MessageBox.Show("Please select Test");
            }
        }
    }
}