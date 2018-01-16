using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Collections;

using Interface.Clinical;
using DataAccess.Base;
using DataAccess.Common;
using DataAccess.Entity;
using Application.Common;


namespace BusinessProcess.Clinical
{
    public class BNonARTFollowUp : ProcessBase, INonARTFollowUp
    {
        #region "Constuctor"
        public BNonARTFollowUp()
        {

        }
        #endregion

        ClsUtility oUtility = new ClsUtility();

        DataSet theDSResult;
        public DataSet GetPatientNonARTFollowUp(int PatientID)
        {
            lock (this)
            {
                ClsObject NonARTManager = new ClsObject();
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@PatientId", SqlDbType.Int, PatientID.ToString());
                oUtility.AddParameters("@password", SqlDbType.VarChar, ApplicationAccess.DBSecurity);
                return (DataSet)NonARTManager.ReturnObject(oUtility.theParams, "pr_Clinical_GetNonARTFollowup_Constella", ClsUtility.ObjectEnum.DataSet);
            }

        }

        #region "Get Exsist Details"

        public DataSet GetExistVisitNonARTFollowUp(int PatientID)
        {
            lock (this)
            {
                ClsObject NonARTManager = new ClsObject();
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@PatientId", SqlDbType.Int, PatientID.ToString());
                return (DataSet)NonARTManager.ReturnObject(oUtility.theParams, "pr_Clinical_GetExisitNonARTFollowupVisit_Constella", ClsUtility.ObjectEnum.DataSet);
            }
        }

        public DataSet GetPatientExsistNonARTFollowUp(int PatientID, int VisitID)
        {
            lock (this)
            {
                ClsObject NonARTManager = new ClsObject();
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@patientID", SqlDbType.Int, PatientID.ToString());
                oUtility.AddParameters("@VisitID", SqlDbType.Int, VisitID.ToString());
                return (DataSet)NonARTManager.ReturnObject(oUtility.theParams, "pr_Clinical_GetNonARTFollowUpUpdate_Constella", ClsUtility.ObjectEnum.DataSet);
            }
        }

        public DataSet GetExistNonARTFollowUpDrugDetails(int PharmacyID)
        {
            lock (this)
            {
                ClsObject NonARTManager = new ClsObject();
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@PharmacyID", SqlDbType.Int, PharmacyID.ToString());
                return (DataSet)NonARTManager.ReturnObject(oUtility.theParams, "pr_Clinical_GetExistNonARTFollowUpPharmacyDetails", ClsUtility.ObjectEnum.DataSet);
            }

        }

        public DataSet GetExistNonARTFollowUpDetails(int PharmacyID, int VisitID, int PateintID)
        {
            lock (this)
            {
                ClsObject NonARTManager = new ClsObject();
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@PharmacyID", SqlDbType.Int, PharmacyID.ToString());
                oUtility.AddParameters("@Visit_pk", SqlDbType.Int, VisitID.ToString());
                oUtility.AddParameters("@Ptn_Pk", SqlDbType.Int, PateintID.ToString());
                return (DataSet)NonARTManager.ReturnObject(oUtility.theParams, "pr_Clinical_GetExistNonARTFollowUpDetails", ClsUtility.ObjectEnum.DataSet);
            }

        }

        public DataSet GetNonARTBoundaryValues()
        {
            lock (this)
            {
                ClsObject NonARTManager = new ClsObject();
                oUtility.Init_Hashtable();
                return (DataSet)NonARTManager.ReturnObject(oUtility.theParams, "pr_Laboratory_GetLabValues_constella", ClsUtility.ObjectEnum.DataSet);
            }
        }

        public DataSet GetExistPharmacyDetail(int PharmacyID)
        {
            lock (this)
            {
                ClsObject PharmacyManager = new ClsObject();
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@PharmacyID", SqlDbType.Int, PharmacyID.ToString());
                return (DataSet)PharmacyManager.ReturnObject(oUtility.theParams, "pr_Pharmacy_GetExistPharmacyDetails", ClsUtility.ObjectEnum.DataSet);
            }

        }

        #endregion

        public DataSet SaveNonARTFollowUp(int PatientID, int PharmacyID, int LocationID, int VisitID, DataSet theDS, DataTable theDT, Hashtable theHT, DateTime OrderedByDate, DateTime DispensedByDate, Boolean Signature, int EmployeeID, int UserID, Boolean flag, Boolean theHIVAssocDisease, int DataQualityFlag, DataTable theCustomFieldData)
        {
            ClsObject NonARTManager = new ClsObject();
            int theAffectedRows = 0;
            int PharmacyId = 0;
            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);

                NonARTManager.Connection = this.Connection;
                NonARTManager.Transaction = this.Transaction;

                oUtility.Init_Hashtable();
                oUtility.AddParameters("@Ptn_pk", SqlDbType.Int, PatientID.ToString());
                //Naveen-Added on 23-Aug-2010
                oUtility.AddParameters("@ptn_pharmacy_pk", SqlDbType.Int, PharmacyID.ToString());
                //
                oUtility.AddParameters("@LocationId", SqlDbType.Int, LocationID.ToString());
                //Naveen-Added on 23-Aug-2010
                oUtility.AddParameters("@Visit_pk", SqlDbType.Int, VisitID.ToString());
                //
                oUtility.AddParameters("@OrderedBy", SqlDbType.Int, theHT["OrderBy"].ToString());
                oUtility.AddParameters("@OrderedByDate", SqlDbType.DateTime, OrderedByDate.ToString());
                oUtility.AddParameters("@DispensedBy", SqlDbType.Int, theHT["DispensedBy"].ToString());
                oUtility.AddParameters("@DispensedByDate", SqlDbType.DateTime, DispensedByDate.ToString());
                oUtility.AddParameters("@Signature", SqlDbType.Bit, Signature.ToString());
                oUtility.AddParameters("@EmployeeID", SqlDbType.Int, EmployeeID.ToString());
                oUtility.AddParameters("@VisitType", SqlDbType.Int, theHT["VisitType"].ToString());
                oUtility.AddParameters("@VisitDate", SqlDbType.DateTime, theHT["VisitDate"].ToString());
                oUtility.AddParameters("@Temp", SqlDbType.Decimal, theHT["physTemp"].ToString());
                oUtility.AddParameters("@RR", SqlDbType.Decimal, theHT["physRR"].ToString());
                oUtility.AddParameters("@HR", SqlDbType.Decimal, theHT["physHR"].ToString());
                oUtility.AddParameters("@BPDiastolic", SqlDbType.Decimal, theHT["physBPDiastolic"].ToString());
                oUtility.AddParameters("@BPSystolic", SqlDbType.Decimal, theHT["physBPSystolic"].ToString());
                oUtility.AddParameters("@Height", SqlDbType.Int, theHT["physHeight"].ToString());
                oUtility.AddParameters("@Weight", SqlDbType.Decimal, theHT["physWeight"].ToString());
                oUtility.AddParameters("@Pain", SqlDbType.Int, theHT["phyPain"].ToString());
                oUtility.AddParameters("@WHOStage", SqlDbType.Int, theHT["physWHOStage"].ToString());
                oUtility.AddParameters("@WABStage", SqlDbType.Int, theHT["physWABStage"].ToString());
                oUtility.AddParameters("@ClinicNotes", SqlDbType.VarChar, theHT["ClinicalNotes"].ToString());
                oUtility.AddParameters("@Pregnant", SqlDbType.Int, theHT["Pregnant"].ToString());
                oUtility.AddParameters("@EDDDate", SqlDbType.Int, theHT["EDDDate"].ToString());
                oUtility.AddParameters("@Delivered", SqlDbType.Int, theHT["Delivered"].ToString());
                oUtility.AddParameters("@DelDate", SqlDbType.Int, theHT["DelDate"].ToString());
             
                oUtility.AddParameters("@LMP", SqlDbType.DateTime, theHT["LMP"].ToString());
                oUtility.AddParameters("@userId", SqlDbType.Int, UserID.ToString());
                oUtility.AddParameters("@OrderType", SqlDbType.Int, theHT["OrderType"].ToString());
                oUtility.AddParameters("@DataQuality", SqlDbType.Int, DataQualityFlag.ToString());
                oUtility.AddParameters("@AppExist", SqlDbType.VarChar, theHT["AppExist"].ToString());
                oUtility.AddParameters("@VisitIDApp", SqlDbType.VarChar, theHT["VisitIDApp"].ToString());
                oUtility.AddParameters("@appdate", SqlDbType.VarChar, theHT["AppDate"].ToString());
                oUtility.AddParameters("@appreason", SqlDbType.VarChar, theHT["AppReason"].ToString());
                oUtility.AddParameters("@signatureid", SqlDbType.VarChar, theHT["Signatureid"].ToString());
                oUtility.AddParameters("@Flag", SqlDbType.Int, theHT["Flag"].ToString());
                //oUtility.AddParameters("@UpdateMode", SqlDbType.Int, UpdateMode.ToString());
                ////if (flag == false)
                ////{
                ////    theDSResult = (DataSet)NonARTManager.ReturnObject(oUtility.theParams, "pr_Clinical_SaveNonARTFollowUP_Constella", ClsUtility.ObjectEnum.DataSet);
                ////    PharmacyId = Convert.ToInt32(theDSResult.Tables[0].Rows[0][0].ToString());

                ////}
                ////else if (flag == true)
                ////{

                    string strResult = string.Empty;
                    
                    int theResult = 0;
                    ////oUtility.AddParameters("@Visit_pkID", SqlDbType.Int, VisitID.ToString());
                    ////oUtility.AddParameters("@ptn_pharmacy_pk", SqlDbType.Int, PharmacyID.ToString());
                    theDSResult = (DataSet)NonARTManager.ReturnObject(oUtility.theParams, "pr_Clinical_SaveUpdateNonARTFollowUP_Constella", ClsUtility.ObjectEnum.DataSet);
                    strResult = Convert.ToString(theDSResult.Tables[0].Rows[0][0]);
                    VisitID = Convert.ToInt32(theDSResult.Tables[1].Rows[0][1]);
                    if (theDSResult.Tables[0].Rows.Count > 0)
                    {
                        if (strResult == "")
                        {
                            theResult = 0;
                        }
                        else
                        {
                            if (PharmacyID == 0)
                            {
                                theResult = Convert.ToInt32(theDSResult.Tables[0].Rows[0][0].ToString());
                                PharmacyID = Convert.ToInt32(theDSResult.Tables[0].Rows[0][0].ToString());
                            }
                        }
                    }
                ////}
                if (theDSResult.Tables[0].Rows[0][0].ToString() == "")
                {
                    MsgBuilder theBL = new MsgBuilder();
                    theBL.DataElements["MessageText"] = "Error in Saving Patient's Non-ART Follow-Up Details. Try Again..";
                    Exception ex = AppException.Create("#C1", theBL);
                    throw ex;
                }
                //Updating Appointment Status
                #region "Updating Appointment Status"

                if (Convert.ToInt32(theHT["AppExist"].ToString()) == 1)
                {
                    oUtility.Init_Hashtable();
                    oUtility.AddParameters("@patientid", SqlDbType.Int, PatientID.ToString());
                    oUtility.AddParameters("@locationid", SqlDbType.Int, LocationID.ToString());
                    oUtility.AddParameters("@Visit_pkAppID", SqlDbType.Int, VisitID.ToString());
                    oUtility.AddParameters("@signatureid", SqlDbType.Int, theHT["OrderBy"].ToString());
                    int RowsAffected = (int)NonARTManager.ReturnObject(oUtility.theParams, "pr_Clinical_UpdateIEAppointmentSignature_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);
                }

                #endregion

                #region "Patient's Symptoms"

                //if (flag == true)
                //{
                //    /***************** Delete Previous Symptoms Records *******************/
                //    oUtility.Init_Hashtable();
                //    oUtility.AddParameters("@Visit_pkID", SqlDbType.Int, VisitID.ToString());
                //    oUtility.AddParameters("@patientid", SqlDbType.Int, PatientID.ToString());
                //    oUtility.AddParameters("@locationid", SqlDbType.Int, LocationID.ToString());
                //    theAffectedRows = (int)NonARTManager.ReturnObject(oUtility.theParams, "pr_Clinical_DeleteFollowUP_PatientSymptoms_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);
                //    if (theAffectedRows == 0)
                //    {
                //        MsgBuilder theMsg = new MsgBuilder();
                //        theMsg.DataElements["MessageText"] = "Error in Updating Patient's Symptoms Details for Non-ART FollowUp. Try Again..";
                //        Exception ex = AppException.Create("#C1", theMsg);
                //        throw ex;
                //    }

                //}

                if (theDS.Tables[0].Rows.Count != 0)
                {
                    for (int i = 0; i < theDS.Tables[0].Rows.Count; i++)
                    {
                        theAffectedRows = 0;
                        oUtility.Init_Hashtable();
                        oUtility.AddParameters("@patientID", SqlDbType.Int, PatientID.ToString());
                        oUtility.AddParameters("@locationid", SqlDbType.Int, LocationID.ToString());
                        oUtility.AddParameters("@symptomID", SqlDbType.Int, theDS.Tables[0].Rows[i]["PresentComplaintsID"].ToString());
                        oUtility.AddParameters("@userID", SqlDbType.Int, UserID.ToString());
                        oUtility.AddParameters("@Visit_pkID", SqlDbType.Int, VisitID.ToString());
                        //oUtility.AddParameters("@createdate", SqlDbType.DateTime, "");
                        theAffectedRows = (int)NonARTManager.ReturnObject(oUtility.theParams, "pr_Clinical_UpdateComplaintsIE_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);

                        //if (flag == false)
                        //{
                        //    theAffectedRows = (int)NonARTManager.ReturnObject(oUtility.theParams, "pr_Clinical_SaveComplaintsIEFU_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);

                        //}
                        //else if (flag == true)
                        //{
                        //oUtility.AddParameters("@Visit_pkID", SqlDbType.Int, VisitID.ToString());
                        //oUtility.AddParameters("@createdate", SqlDbType.DateTime, "");
                        //theAffectedRows = (int)NonARTManager.ReturnObject(oUtility.theParams, "pr_Clinical_UpdateComplaintsIE_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);
                        //}
                        if (theAffectedRows == 0)
                        {
                            MsgBuilder theMsg = new MsgBuilder();
                            theMsg.DataElements["MessageText"] = "Error in Saving Patient's Symptoms Details for Non-ART Follow-Up. Try Again..";
                            Exception ex = AppException.Create("#C1", theMsg);
                            throw ex;
                        }
                    }
                }
                #endregion
                #region "Patient TB Symptoms"
                //if (flag == true)
                //{
                //    /***************** Delete Previous Symptoms Records *******************/
                //    oUtility.Init_Hashtable();
                //    oUtility.AddParameters("@Visit_pkID", SqlDbType.Int, VisitID.ToString());
                //    oUtility.AddParameters("@patientid", SqlDbType.Int, PatientID.ToString());
                //    oUtility.AddParameters("@locationid", SqlDbType.Int, LocationID.ToString());
                //    theAffectedRows = (int)NonARTManager.ReturnObject(oUtility.theParams, "pr_Clinical_DeleteFollowUP_PatientSymptoms_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);
                //    if (theAffectedRows == 0)
                //    {
                //        MsgBuilder theMsg = new MsgBuilder();
                //        theMsg.DataElements["MessageText"] = "Error in Updating Patient's TBSymptoms Details for Non-ART FollowUp. Try Again..";
                //        Exception ex = AppException.Create("#C1", theMsg);
                //        throw ex;
                //    }

                //}

                if (theDS.Tables[5].Rows.Count != 0)
                {
                    for (int i = 0; i < theDS.Tables[5].Rows.Count; i++)
                    {
                        theAffectedRows = 0;
                        oUtility.Init_Hashtable();
                        oUtility.AddParameters("@patientID", SqlDbType.Int, PatientID.ToString());
                        oUtility.AddParameters("@locationid", SqlDbType.Int, LocationID.ToString());
                        oUtility.AddParameters("@symptomID", SqlDbType.Int, theDS.Tables[5].Rows[i]["TBSymptomsID"].ToString());
                        oUtility.AddParameters("@userID", SqlDbType.Int, UserID.ToString());
                        oUtility.AddParameters("@Visit_pkID", SqlDbType.Int, VisitID.ToString());
                        //oUtility.AddParameters("@createdate", SqlDbType.DateTime, "");
                        theAffectedRows = (int)NonARTManager.ReturnObject(oUtility.theParams, "pr_Clinical_UpdateComplaintsIE_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);


                        //if (flag == false)
                        //{
                        //    theAffectedRows = (int)NonARTManager.ReturnObject(oUtility.theParams, "pr_Clinical_SaveComplaintsIEFU_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);

                        //}
                        //else if (flag == true)
                        //{
                        //    oUtility.AddParameters("@Visit_pkID", SqlDbType.Int, VisitID.ToString());
                        //    oUtility.AddParameters("@createdate", SqlDbType.DateTime, "");
                        //    theAffectedRows = (int)NonARTManager.ReturnObject(oUtility.theParams, "pr_Clinical_UpdateComplaintsIE_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);

                        //}
                        if (theAffectedRows == 0)
                        {
                            MsgBuilder theMsg = new MsgBuilder();
                            theMsg.DataElements["MessageText"] = "Error in Saving Patient's TB Symptoms Details for Non-ART Follow-Up. Try Again..";
                            Exception ex = AppException.Create("#C1", theMsg);
                            throw ex;
                        }
                    }
                }
                #endregion


                #region "Patient's HIV Associated Diseases"

                //if (flag == true)
                //{
                //    /******************* Delete Previous HIV Associated Diseases Details *******************/
                //    theAffectedRows = 0;
                //    oUtility.Init_Hashtable();
                //    oUtility.AddParameters("@patientID", SqlDbType.Int, PatientID.ToString());
                //    oUtility.AddParameters("@locationid", SqlDbType.Int, LocationID.ToString());
                //    oUtility.AddParameters("@Visit_pkID", SqlDbType.Int, VisitID.ToString());
                //    theAffectedRows = (int)NonARTManager.ReturnObject(oUtility.theParams, "pr_Clinical_DeleteMedicalHistoryIE_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);
                //    if (theAffectedRows == 0)
                //    {
                //        MsgBuilder theMsg = new MsgBuilder();
                //        theMsg.DataElements["MessageText"] = "Error in Updating Patient's HIV Associated Diseases Details for Non-ART FollowUp. Try Again..";
                //        Exception ex = AppException.Create("#C1", theMsg);
                //        throw ex;
                //    }
                //}

                if (theDS.Tables[2].Rows.Count != 0)
                {
                    for (int i = 0; i < theDS.Tables[2].Rows.Count; i++)
                    {
                        theAffectedRows = 0;
                        oUtility.Init_Hashtable();
                        oUtility.AddParameters("@patientID", SqlDbType.Int, PatientID.ToString());
                        oUtility.AddParameters("@locationid", SqlDbType.Int, LocationID.ToString());
                        oUtility.AddParameters("@HIVAssocDiseaseID", SqlDbType.Int, theDS.Tables[2].Rows[i][0].ToString());
                        oUtility.AddParameters("@DiseaseDesc", SqlDbType.VarChar, theDS.Tables[2].Rows[i][1].ToString());
                        oUtility.AddParameters("@HIVAssocDiseasePresent", SqlDbType.Bit, theDS.Tables[2].Rows[i][2].ToString());
                        oUtility.AddParameters("@userID", SqlDbType.Int, UserID.ToString());
                        oUtility.AddParameters("@Visit_pkID", SqlDbType.Int, VisitID.ToString());
                        //oUtility.AddParameters("@createdate", SqlDbType.DateTime, "");
                        theAffectedRows = (int)NonARTManager.ReturnObject(oUtility.theParams, "pr_Clinical_UpdateNonARTFollowUpHIVAssoCondition_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);
                  

                        //if (flag == false)
                        //{
                        //    theAffectedRows = (int)NonARTManager.ReturnObject(oUtility.theParams, "pr_Clinical_SaveHIVAssoConditionNonARTFollowUp_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);
                        //}
                        //else if (flag == true)
                        //{
                        //    oUtility.AddParameters("@Visit_pkID", SqlDbType.Int, VisitID.ToString());
                        //    oUtility.AddParameters("@createdate", SqlDbType.DateTime, "");
                        //    theAffectedRows = (int)NonARTManager.ReturnObject(oUtility.theParams, "pr_Clinical_UpdateNonARTFollowUpHIVAssoCondition_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);
                        //}

                        if (theAffectedRows == 0)
                        {
                            MsgBuilder theMsg = new MsgBuilder();
                            theMsg.DataElements["MessageText"] = "Error in Saving Patient's HIV Associated Diseases Details for Non-ART Follow-Up. Try Again..";
                            Exception ex = AppException.Create("#C1", theMsg);
                            throw ex;
                        }

                    }
                }

                if (theHIVAssocDisease == true)
                {
                    if (theDS.Tables[3].Rows.Count != 0)
                    {
                        for (int i = 0; i < theDS.Tables[3].Rows.Count; i++)
                        {
                            theAffectedRows = 0;
                            oUtility.Init_Hashtable();
                            oUtility.AddParameters("@PatientID", SqlDbType.Int, PatientID.ToString());
                            oUtility.AddParameters("@Locationid", SqlDbType.Int, LocationID.ToString());
                            oUtility.AddParameters("@HIVAssocDiseaseID", SqlDbType.Int, theDS.Tables[3].Rows[i][0].ToString());
                            oUtility.AddParameters("@DiseaseDesc", SqlDbType.VarChar, theDS.Tables[3].Rows[i][1].ToString());
                            oUtility.AddParameters("@HIVAssocDiseasePresent", SqlDbType.Bit, theDS.Tables[3].Rows[i][2].ToString());
                            oUtility.AddParameters("@UserID", SqlDbType.Int, UserID.ToString());
                            oUtility.AddParameters("@Visit_pkID", SqlDbType.Int, VisitID.ToString());
                            //oUtility.AddParameters("@createdate", SqlDbType.DateTime, "");
                            theAffectedRows = (int)NonARTManager.ReturnObject(oUtility.theParams, "pr_Clinical_UpdateNonARTFollowUpHIVAssoCondition_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);
                      

                            //if (flag == false)
                            //{
                            //    theAffectedRows = (int)NonARTManager.ReturnObject(oUtility.theParams, "pr_Clinical_SaveHIVAssoConditionNonARTFollowUp_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);
                            //}
                            //else if (flag == true)
                            //{
                            //    oUtility.AddParameters("@Visit_pkID", SqlDbType.Int, VisitID.ToString());
                            //    oUtility.AddParameters("@createdate", SqlDbType.DateTime, "");
                            //    theAffectedRows = (int)NonARTManager.ReturnObject(oUtility.theParams, "pr_Clinical_UpdateNonARTFollowUpHIVAssoCondition_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);
                            //}
                            
                            if (theAffectedRows == 0)
                            {
                                MsgBuilder theMsg = new MsgBuilder();
                                theMsg.DataElements["MessageText"] = "Error in Saving Patient's HIV Associated Diseases Details for Non-ART Follow-Up. Try Again..";
                                Exception ex = AppException.Create("#C1", theMsg);
                                throw ex;
                            }
                        }
                    }
                }
                #endregion

                #region "Assessments"

                if (flag == true)
                {
                    /****************** Delete Previous Assessments *******************/
                    theAffectedRows = 0;
                    oUtility.Init_Hashtable();
                    oUtility.AddParameters("@patientID", SqlDbType.Int, PatientID.ToString());
                    oUtility.AddParameters("@locationid", SqlDbType.Int, LocationID.ToString());
                    oUtility.AddParameters("@Visit_pkID", SqlDbType.Int, VisitID.ToString());
                    oUtility.AddParameters("@plannotes", SqlDbType.VarChar, "");
                    oUtility.AddParameters("@UserID", SqlDbType.Int, UserID.ToString());
                    theAffectedRows = (int)NonARTManager.ReturnObject(oUtility.theParams, "pr_Clinical_DeleteFollowUP_PatientAssessment_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);
                    if (theAffectedRows == 0)
                    {
                        MsgBuilder theMsg = new MsgBuilder();
                        theMsg.DataElements["MessageText"] = "Error in Updating Patient's Assessments Details for Non-ART FollowUp. Try Again..";
                        Exception ex = AppException.Create("#C1", theMsg);
                        throw ex;
                    }
                }

                if (theDS.Tables[1].Rows.Count != 0)
                {
                    for (int i = 0; i < theDS.Tables[1].Rows.Count; i++)
                    {
                        theAffectedRows = 0;
                        oUtility.Init_Hashtable();
                        oUtility.AddParameters("@patientID", SqlDbType.Int, PatientID.ToString());
                        oUtility.AddParameters("@locationid", SqlDbType.Int, LocationID.ToString());
                        oUtility.AddParameters("@AssessmentID", SqlDbType.Int, theDS.Tables[1].Rows[i]["AssessmentID"].ToString());
                        oUtility.AddParameters("@userID", SqlDbType.Int, UserID.ToString());

                        if (flag == false)
                        {
                            theAffectedRows = (int)NonARTManager.ReturnObject(oUtility.theParams, "pr_Clinical_SaveAssessmentFU_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);
                        }
                        else if (flag == true)
                        {
                            oUtility.AddParameters("@Visit_pkID", SqlDbType.Int, VisitID.ToString());
                            oUtility.AddParameters("@createdate", SqlDbType.DateTime, ""); //createdate.ToString());
                            //oUtility.AddParameters("@createdate", SqlDbType.DateTime, theHT["CreateDate"].ToString()); //createdate.ToString());
                            theAffectedRows = (int)NonARTManager.ReturnObject(oUtility.theParams, "pr_Clinical_UpdateAssessmentFU_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);

                        }

                        if (theAffectedRows == 0)
                        {
                            MsgBuilder theMsg = new MsgBuilder();
                            theMsg.DataElements["MessageText"] = "Error in Saving Patient's Assessments Details for Non-ART Follow-Up. Try Again..";
                            Exception ex = AppException.Create("#C1", theMsg);
                            throw ex;
                        }

                    }
                }
                #endregion

                #region "Drugs Details"
                if (flag == true)
                {
                    /***************** Delete Previous Drugs Records *******************/
                    //theAffectedRows = 0;
                    oUtility.Init_Hashtable();
                    oUtility.AddParameters("@ptn_pharmacy_pk", SqlDbType.Int, PharmacyID.ToString());
                    theAffectedRows = (int)NonARTManager.ReturnObject(oUtility.theParams, "pr_Pharmacy_DeletePharmacyDetail_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);
                    if (theAffectedRows == 0)
                    {
                        MsgBuilder theMsg = new MsgBuilder();
                        theMsg.DataElements["MessageText"] = "Error in Updating Patient's Pharmacy Details for Non-ART FollowUp. Try Again..";
                        Exception ex = AppException.Create("#C1", theMsg);
                        throw ex;
                    }
                }
                if (theDS.Tables[4].Rows.Count != 0)
                {
                    for (int i = 0; i < theDS.Tables[4].Rows.Count; i++)
                    {
                        theAffectedRows = 0;
                        int theFinanced = 99999;
                        int theUnit = 99999;
                        Decimal theDose = 99999;
                        oUtility.Init_Hashtable();

                        oUtility.AddParameters("@GenericID", SqlDbType.Int, theDS.Tables[4].Rows[i]["GenericID"].ToString());
                        oUtility.AddParameters("@Drug_Pk", SqlDbType.Int, theDS.Tables[4].Rows[i]["DrugID"].ToString());
                        oUtility.AddParameters("@StrengthID", SqlDbType.Int, theDS.Tables[4].Rows[i]["Strength"].ToString());
                        oUtility.AddParameters("@FrequencyID", SqlDbType.Int, theDS.Tables[4].Rows[i]["Frequency"].ToString());
                        oUtility.AddParameters("@Duration", SqlDbType.Decimal, theDS.Tables[4].Rows[i]["Duration"].ToString());
                        oUtility.AddParameters("@OrderedQuantity", SqlDbType.Decimal, theDS.Tables[4].Rows[i]["QtyPrescribed"].ToString());
                        oUtility.AddParameters("@DispensedQuantity", SqlDbType.Decimal, theDS.Tables[4].Rows[i]["QtyDispensed"].ToString());
                        oUtility.AddParameters("@Finance", SqlDbType.Int, theFinanced.ToString());
                        oUtility.AddParameters("@UnitId", SqlDbType.Int, theUnit.ToString());
                        oUtility.AddParameters("@Dose", SqlDbType.Decimal, theDose.ToString());
                        oUtility.AddParameters("@UserID", SqlDbType.Int, UserID.ToString());

                        if (flag == false)
                        {
                            oUtility.AddParameters("@ptn_pharmacy_pk", SqlDbType.Int, theDSResult.Tables[0].Rows[0][0].ToString());
                        }
                        else if (flag == true)
                        {
                            oUtility.AddParameters("@ptn_pharmacy_pk", SqlDbType.Int, PharmacyID.ToString());
                        }
                        theAffectedRows = (int)NonARTManager.ReturnObject(oUtility.theParams, "pr_Clinical_SaveNonARTDrugDetails_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);

                        if (theAffectedRows == 0)
                        {
                            MsgBuilder theMsg = new MsgBuilder();
                            theMsg.DataElements["MessageText"] = "Error in Saving Patient's Pharmacy Details for Non-ART FollowUp. Try Again..";
                            Exception ex = AppException.Create("#C1", theMsg);
                            throw ex;
                        }
                    }
                }

                /***************Save Other OI *****************/

                if (theDT.Rows.Count != 0)
                {
                    for (int i = 0; i < theDT.Rows.Count; i++)
                    {
                        theAffectedRows = 0;

                        oUtility.Init_Hashtable();

                        oUtility.AddParameters("@GenericID", SqlDbType.Int, theDT.Rows[i]["GenericID"].ToString());
                        oUtility.AddParameters("@Drug_Pk", SqlDbType.Int, theDT.Rows[i]["DrugID"].ToString());
                        oUtility.AddParameters("@StrengthID", SqlDbType.Int, theDT.Rows[i]["StrengthId"].ToString());
                        oUtility.AddParameters("@FrequencyID", SqlDbType.Int, theDT.Rows[i]["FrequencyId"].ToString());
                        oUtility.AddParameters("@Duration", SqlDbType.Int, theDT.Rows[i]["Duration"].ToString());
                        oUtility.AddParameters("@OrderedQuantity", SqlDbType.Int, theDT.Rows[i]["QtyPrescribed"].ToString());
                        oUtility.AddParameters("@DispensedQuantity", SqlDbType.Int, theDT.Rows[i]["QtyDispensed"].ToString());
                        oUtility.AddParameters("@Finance", SqlDbType.Int, theDT.Rows[i]["Financed"].ToString());
                        oUtility.AddParameters("@UnitId", SqlDbType.Int, theDT.Rows[i]["UnitId"].ToString());
                        oUtility.AddParameters("@Dose", SqlDbType.Decimal, theDT.Rows[i]["Dose"].ToString());
                        oUtility.AddParameters("@UserID", SqlDbType.Int, UserID.ToString());

                        if (flag == false)
                        {
                            oUtility.AddParameters("@ptn_pharmacy_pk", SqlDbType.Int, theDSResult.Tables[0].Rows[0][0].ToString());
                        }
                        else if (flag == true)
                        {
                            oUtility.AddParameters("@ptn_pharmacy_pk", SqlDbType.Int, PharmacyID.ToString());
                        }
                        theAffectedRows = (int)NonARTManager.ReturnObject(oUtility.theParams, "pr_Clinical_SaveNonARTDrugDetails_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);

                        if (theAffectedRows == 0)
                        {
                            MsgBuilder theMsg = new MsgBuilder();
                            theMsg.DataElements["MessageText"] = "Error in Saving Patient's Pharmacy Details for Non-ART FollowUp. Try Again..";
                            Exception ex = AppException.Create("#C1", theMsg);
                            throw ex;
                        }
                    }
                }

                #endregion


                #region "Custom Fields"
                //// Custom Fields //////////////
                ////////////PreSet Values Used/////////////////
                /// #99# --- Ptn_Pk
                /// #88# --- LocationId
                /// #77# --- Visit_Pk
                /// #66# --- Visit_Date
                /// #55# --- Ptn_Pharmacy_Pk
                /// #44# --- OrderedByDate
                /// #33# --- LabId
                /// #22# --- TrackingId
                /// #11# --- CareEndedId
                /// #00# --- HomeVisitId
                ///////////////////////////////////////////////

                //ClsObject theCustomManager = new ClsObject();
                //Generating VisitID from IE Form
                //String VisitIDNonART = "";
                //if (flag == false)
                //{
                //    string theSQL = string.Format("Select IDENT_CURRENT('ord_Visit')");
                //    oUtility.Init_Hashtable();
                //    DataTable DTVisitID = (DataTable)NonARTManager.ReturnObject(oUtility.theParams, theSQL, ClsUtility.ObjectEnum.DataTable);
                //    VisitIDNonART = DTVisitID.Rows[0][0].ToString();
                //}
                //if (flag == true)
                //{
                //    VisitIDNonART = VisitID.ToString();
                //}
                for (Int32 i = 0; i < theCustomFieldData.Rows.Count; i++)
                {
                    oUtility.Init_Hashtable();
                    string theQuery = theCustomFieldData.Rows[i]["Query"].ToString();
                    theQuery = theQuery.Replace("#99#", PatientID.ToString());
                    theQuery = theQuery.Replace("#88#", LocationID.ToString());
                    theQuery = theQuery.Replace("#77#", VisitID.ToString());
                    theQuery = theQuery.Replace("#66#", "'" + theHT["VisitDate"].ToString() + "'");
                    oUtility.AddParameters("@QryString", SqlDbType.VarChar, theQuery);
                    int RowsAffected = (Int32)NonARTManager.ReturnObject(oUtility.theParams, "pr_General_Dynamic_Insert", ClsUtility.ObjectEnum.ExecuteNonQuery);
                }
                ////////////////////////////////
                #endregion
                DataMgr.CommitTransaction(this.Transaction);
                DataMgr.ReleaseConnection(this.Connection);
                return theDSResult;
            }
            catch(Exception err)
            {
                DataMgr.RollBackTransation(this.Transaction);
                throw err;
                
            }
            finally
            {
                NonARTManager = null;
                if (this.Connection != null)
                    DataMgr.ReleaseConnection(this.Connection);
               
            }
        }


        #region "Delete Non-ART Form"
        public int DeleteNonARTForms(string FormName, int OrderNo, int PatientId, int UserID)
        {

            try
            {
                int theAffectedRows = 0;
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);

                ClsObject DeleteNonARTForm = new ClsObject();
                DeleteNonARTForm.Connection = this.Connection;
                DeleteNonARTForm.Transaction = this.Transaction;

                oUtility.Init_Hashtable();
                oUtility.AddParameters("@OrderNo", SqlDbType.Int, OrderNo.ToString());
                oUtility.AddParameters("@FormName", SqlDbType.VarChar, FormName);
                oUtility.AddParameters("@PatientId", SqlDbType.Int, PatientId.ToString());
                oUtility.AddParameters("@UserID", SqlDbType.Int, UserID.ToString());

                theAffectedRows = (int)DeleteNonARTForm.ReturnObject(oUtility.theParams, "pr_Clinical_DeletePatientForms_Constella", ClsUtility.ObjectEnum.ExecuteNonQuery);


                DataMgr.CommitTransaction(this.Transaction);
                DataMgr.ReleaseConnection(this.Connection);
                return theAffectedRows;
            }
            catch
            {
                DataMgr.RollBackTransation(this.Transaction);
                throw;
            }
            finally
            {
                if (this.Connection != null)
                    DataMgr.ReleaseConnection(this.Connection);
            }

        }
        #endregion
    }

}
