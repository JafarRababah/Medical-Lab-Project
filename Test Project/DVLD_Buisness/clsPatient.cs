using System;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using DVLD_DataAccess;

namespace DVLD_Buisness
{
    public class clsPatient
    {

        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public clsPerson PersonInfo;

        public int PatientID { set; get; }
        public int PersonID { set; get; }
        public int CreatedByUserID { set; get; }
        public DateTime CreatedDate {  get; }

        public clsPatient()

        {
            this.PatientID = -1;
            this.PersonID = -1;
            this.CreatedByUserID = -1;
            this.CreatedDate=DateTime.Now;
            Mode = enMode.AddNew;

        }
        clsPatient(int PatientID, int PersonID,int CreatedByUserID, DateTime CreatedDate)

        {
            this.PatientID = PatientID;
            this.PersonID = PersonID;
            this.CreatedByUserID = CreatedByUserID;
            this.CreatedDate = CreatedDate;
            this.PersonInfo = clsPerson.Find(PersonID);

            Mode = enMode.Update;
        }

        private bool _AddNewPatient()
        {
            //call DataAccess Layer 

            this.PatientID = clsPatientData.AddNewPatient( PersonID,  CreatedByUserID);
              

            return (this.PatientID != -1);
        }

        private bool _UpdatePatient()
        {
            //call DataAccess Layer 

            return clsPatientData.UpdatePatient(this.PatientID, this.PersonID,this.CreatedByUserID);
        }

        public static clsPatient FindByPatientID(int PatientID)
        {
            
            int PersonID = -1; int CreatedByUserID = -1;DateTime CreatedDate= DateTime.Now; 

            if (clsPatientData.GetPatientInfoByPatientID(PatientID, ref PersonID,ref CreatedByUserID,ref CreatedDate))

                return new clsPatient(PatientID,  PersonID,  CreatedByUserID,  CreatedDate);
            else
                return null;

        }

        public static clsPatient FindByPersonID(int PersonID)
        {

            int PatientID = -1; int CreatedByUserID = -1; DateTime CreatedDate = DateTime.Now;

            if (clsPatientData.GetPatientInfoByPersonID( PersonID, ref PatientID,  ref CreatedByUserID, ref CreatedDate))

                return new clsPatient(PatientID, PersonID, CreatedByUserID, CreatedDate);
            else
                return null;

        }

        public static DataTable GetAllPatients()
        {
            return clsPatientData.GetAllPatients();

        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewPatient())
                    {

                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:

                    return _UpdatePatient();

            }

            return false;
        }

        public static DataTable GetLicenses(int PatientID)
        {
            //return null;
            return clsLicense.GetPatientLicenses(PatientID);
        }

        public static DataTable GetInternationalLicenses(int PatientID)
        {
            return clsInternationalLicense.GetPatientInternationalLicenses(PatientID);
        }

    }
}
