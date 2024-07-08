using System;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Runtime.CompilerServices;
using DVLD_DataAccess;

namespace DVLD_Buisness
{
    public class clsPatientTest
    {

        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;
        //public enum enTestType { VisionTest = 1, WrittenTest = 2, StreetTest = 3 };

        //public clsTestType.enTestType ID { set; get; }
        public int ID { set; get; }
        public int TestListID { set; get; }
        public string Result { set; get; }
        public clsPatient PatientInfo;
        public int PatientID { set; get; }
       // public int LicenseID { set; get; }
        public clsPatientTest()
        {
            this.ID = -1;
            this.TestListID=-1;
            this.PatientID = -1;
            this.Result = "";
        }
        public clsPatientTest(int ID, int TestListID, int PatientID,string Result)

        {
            this.ID = ID;
            this.TestListID = TestListID;
            this.Result = Result;
            this.PatientID = PatientID;
            this.PatientInfo = clsPatient.FindByPatientID(PatientID);
            Mode = enMode.Update;
        }

        private bool _AddNewTest()
        {
            //call DataAccess Layer 

            // this.ID=clsPatientTestData.AddNewTest(1036,this.TestListID,this.PatientID);
          return  clsPatientTestData.AddNewTest(this.ID, this.TestListID, this.PatientID);

           // return (this.ID != -1);
        }

        private bool _UpdateTest()
        {
            //call DataAccess Layer 
            
            return clsPatientTestData.UpdateTest((int)this.ID, this.TestListID,  this.PatientID,this.Result);
        }
        public bool Update()
        {
            return clsPatientTestData.UpdateTest((int)this.ID, this.TestListID, this.PatientID, this.Result);

        }
        public static clsPatientTest Find(int ID)
        {
            int TestListID = -1; int PatientID = -1;string Result = "";

            if (clsPatientTestData.GetTestInfoByID((int)ID, ref TestListID, ref PatientID,ref Result))

                return new clsPatientTest(ID, TestListID, PatientID,Result);
            else
                return null;

        }

        public static DataTable GetAllTests(int ID)
        {
            return clsPatientTestData.GetAllTests(ID);

        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewTest())
                    {

                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:

                    return _UpdateTest();

            }

            return false;
        }

    }
}
