using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Interface.SCM
{
    public interface IDrug
    {
        DataSet GetPharmacyDispenseMasters(Int32 thePatientId, Int32 theStoreId);
        DataTable SavePharmacyDispense(Int32 thePatientId, Int32 theLocationId, Int32 theStoreId, Int32 theUserId, DateTime theDispDate,
        Int32 theOrderType, Int32 theProgramId, string theRegimen, Int32 theOrderId, DataTable theDT, DateTime PharmacyRefillDate);
        DataTable GetPharmacyExistingRecord(Int32 thePatientId, Int32 theStoreId);
        DataSet GetPharmacyExistingRecordDetails(Int32 theOrderId);
        void SavePharmacyReturn(Int32 thePatientId, Int32 theLocationId, Int32 theStoreId, DateTime theReturnDate, Int32 theUserId, Int32 thePharmacyId, DataTable theDT);
        DataSet GetPharmacyDetailsByDespenced(Int32 theOrderId);
        DataSet GetDrugTypeID(Int32 ItemID);
        DataSet SaveArtData(Int32 PatientID, DateTime dispencedDate);
        DataSet CheckDispencedDate(Int32 thePatientId, Int32 LocationID, DateTime theDispDate, Int32 theOrderId);
        DataSet SaveHivTreatementPharmacyField(Int32 theOrderId, string weight, string height, int Program, int PeriodTaken, int Provider, int RegimenLine, DateTime NxtAppDate, int Reason);
        DataSet GetPharmacyPrescriptionDetails(int PharmacyID, int PatientId, int IQCareFlag);
        DataTable GetPersonDispensingDrugs(string UserName);
        DataTable CheckPaperlessClinic(int LocationID);

        //KK. 06-Feb-2015
        DataSet GetPharmacyVitals(int PatientID);
        DataSet GetPharmacyDrugList_Web(int StoreID);
        DataTable ReturnDatatableQuery(string theQuery);
        DataTable SavePharmacyDispense_Web(Int32 PatientId, Int32 LocationId, Int32 StoreId, int OrderedBy, string OrderedByDate,
            Int32 DispensedBy, string DisensedByDate, Int32 OrderType, Int32 ProgramId, string Regimen, Int32 OrderId, DataTable theDT,
            DateTime PharmacyRefillDate, Int32 DataStatus, int UserID);
        DataSet GetPharmacyExistingRecordDetails_Web(Int32 VisitID);
        DataSet GetPharmacyDrugHistory_Web(int PatientID);
        void SavePharmacyRefill_Web(DataTable dt, int iserId, DateTime DispensedByDate, int patientID, int locationID, string appointmentDate, int userID, int empID);
        DataSet MarkOrderAsFullyDispensed(Int32 orderID, string Reason);
        void LockpatientForDispensing(int PatientId, int OrderId, string UserName, DateTime StartDate, bool LockPatient);
        DataSet GetDrugBatchDetails(int DrugID, int StoreID);
        DataSet GetSelectedDrugDetails(int DrugID, int StoreID);
        DataTable GetStoreSupplier();
    }
}
