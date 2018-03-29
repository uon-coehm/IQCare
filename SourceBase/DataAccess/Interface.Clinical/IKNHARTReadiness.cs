using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Interface.Clinical
{
    public interface IKNHARTReadiness
    {
        int SaveUpdateARTReadinessForm(int patientID, int VisitID, int LocationID, Hashtable ht, int userID);
        DataSet GetARTReadinessData(int ptn_pk, int visitpk);
    }
}
