using System;
using DataAccess.Common;
using DataAccess.Entity;
using DataAccess.Base;
using Interface.Administration;
using System.Data;

namespace BusinessProcess.Administration
{
        /// <summary>
        /// Error class used to write errors to the Event Viewer that IQCare is installed on
        /// </summary>
        /// 
    

    public class BErrorLogging : ProcessBase, IErrorLogging
        {
            /// <summary>
            /// Method that writes errors to the Event Viewer on the server that IQCare is insalled on
            /// </summary>
            /// <param name="sSource">string - e.g. Use "Presentation" or "DataAccess"</param>
            /// <param name="sEvent">string - The error or event name as a string</param>
            /// 
        ClsUtility oUtility = new ClsUtility();

            public bool LogError(string sSource, string sEvent, ErrorType tEntryType)
            {
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@Source", SqlDbType.VarChar, sSource);
                oUtility.AddParameters("@ErrorMessage", SqlDbType.Int, sEvent);
                oUtility.AddParameters("@ErrorType", SqlDbType.Int, tEntryType.ToString());
                ClsObject RecordMgr = new ClsObject();
                RecordMgr.ReturnObject(oUtility.theParams, "pr_Touch_LogError", ClsUtility.ObjectEnum.ExecuteNonQuery);

                return true;
            }
        }
        
        //public class ErrorType
        //{
            
        //    public string Error { get { return "Error"; } }
        //    public string Critical { get { return "Critical"; } }
        //    public string Warning { get { return "Warning"; } }
        //    public string Information { get { return "Information"; } }
        //}

}
