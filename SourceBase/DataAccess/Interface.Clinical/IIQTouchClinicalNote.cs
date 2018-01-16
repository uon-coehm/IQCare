using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Interface.Clinical
{
    public interface IIQTouchClinicalNote 
    {
        int SaveClinicalnote(string PatientID, string NoteDate, string Note, string LocationID);
        DataTable GetClinicalNote(string PatientID, string NoteID);
        int EditClinicalnote(string PatientID, string NoteID, string NoteDate, string Note, string LocationID);
    }
}
