using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Interface.Clinical
{
    public interface IKNHPsychosocialAdherence
    {
        DataSet SaveUpdateKNHPsychosocialAdherence_ProfileTab(Hashtable hashTable, DataTable dtMultiSelectValues, int signature, int UserId);
        DataSet SaveUpdateKNHPsychosocialAdherence_AssessmentTab(Hashtable hashTable, DataTable dtMultiSelectValues, int signature, int UserId);
        DataSet SaveUpdateKNHPsychosocialAdherence_ManagementTab(Hashtable hashTable, DataTable dtMultiSelectValues, int signature, int UserId);
        DataSet GetKNHPsychosocialAdherenceData(int ptn_pk, int visitpk);
    }
}
