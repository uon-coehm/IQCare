using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Interface.Clinical
{
    public interface IKNHHomeVisit
    {
        DataSet SaveUpdateHomeVisitData(Hashtable hashTable, int signature, int UserId);
        DataSet GetHomeVisitData(int ptn_pk, int visitpk);
    }
}
