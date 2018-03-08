using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Interface.Clinical
{
    public interface IKNHPaed
    {
        int Save_Update_PaedsChecklist(int patientID, int VisitID, int LocationID, Hashtable ht, int userID);
    }
}
