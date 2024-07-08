using System;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using DVLD_DataAccess;

namespace DVLD_Buisness
{
    public class clsEmployee
    {

        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public clsPerson PersonInfo;

        public int EmployeeID { set; get; }
        public int PersonID { set; get; }
        public int CreatedByUserID { set; get; }
        public float Salary { set; get; }
        public string Position { set; get; }
        public string DepartmentID { set; get; }
        public string QualificationID { set; get; }
        public DateTime CreatedDate { get; }
        
        public clsEmployee()

        {
            this.EmployeeID = -1;
            this.PersonID = -1;
            this.CreatedByUserID = -1;
            this.Salary = -1;
            this.Position = "";
            this.DepartmentID = "";
            this.QualificationID = "";
            this.CreatedDate = DateTime.Now;
            Mode = enMode.AddNew;

        }

        public clsEmployee(int EmployeeID, int PersonID, int CreatedByUserID,
            float Salary,string Position,string DepartmentID,string QualificationID, DateTime CreatedDate)

        {
            this.EmployeeID = EmployeeID;
            this.PersonID = PersonID;
            this.CreatedByUserID = CreatedByUserID;
            this.Salary = Salary;
            this.Position = Position;
            this.DepartmentID = DepartmentID;
            this.QualificationID = QualificationID;
            this.CreatedDate = CreatedDate;
            this.PersonInfo = clsPerson.Find(PersonID);

            Mode = enMode.Update;
        }

        private bool _AddNewEmployee()
        {
            //call DataAccess Layer 

            this.EmployeeID = clsEmployeeData.AddNewEmployee(PersonID, CreatedByUserID,Salary,Position,DepartmentID,QualificationID);


            return (this.EmployeeID != -1);
        }

        private bool _UpdateEmployee()
        {
            //call DataAccess Layer 

            return clsEmployeeData.UpdateEmployee(this.EmployeeID, this.PersonID, this.CreatedByUserID,this.Salary,this.Position,this.DepartmentID,this.QualificationID);
        }

        public static clsEmployee FindByEmployeeID(int EmployeeID)
        {

            int PersonID = -1; int CreatedByUserID = -1; DateTime CreatedDate = DateTime.Now;
            float Salary = -1; string Position = ""; string DepartmentID = "";string QualificationID = "";

            if (clsEmployeeData.GetEmployeeInfoByEmployeeID(EmployeeID, ref PersonID, ref CreatedByUserID,ref Salary,ref Position,ref DepartmentID,ref QualificationID, ref CreatedDate))

                return new clsEmployee(EmployeeID, PersonID, CreatedByUserID,Salary,Position,DepartmentID,QualificationID, CreatedDate);
            else
                return null;

        }

        public static clsEmployee FindByPersonID(int PersonID)
        {

            int EmployeeID = -1; int CreatedByUserID = -1; DateTime CreatedDate = DateTime.Now;
            float Salary = -1; string Position = ""; string DepartmentID = ""; string QualificationID = "";

            if (clsEmployeeData.GetEmployeeInfoByPersonID(PersonID, ref EmployeeID, ref CreatedByUserID,ref Salary,ref Position,ref DepartmentID,ref QualificationID, ref CreatedDate))

                return new clsEmployee(EmployeeID, PersonID, CreatedByUserID,Salary,Position,DepartmentID,QualificationID, CreatedDate);
            else
                return null;

        }

        public static DataTable GetAllEmployees()
        {
            return clsEmployeeData.GetAllEmployees();

        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewEmployee())
                    {

                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:

                    return _UpdateEmployee();

            }

            return false;
        }

        //public static DataTable GetLicenses(int PatientID)
        //{
        //    return clsLicense.GetEmployeeLicenses(PatientID);
        //}

        //public static DataTable GetInternationalLicenses(int PatientID)
        //{
        //    return clsInternationalLicense.GetEmployeeInternationalLicenses(PatientID);
        //}
        public static bool isEmployeeExist(int EmployeeID)
        {
            return clsEmployeeData.IsEmployeeExist(EmployeeID);
        }

        public static bool isEmployeeExist(string EmployeeName)
        {
            return clsEmployeeData.IsEmployeeExist(EmployeeName);
        }

        public static bool isEmployeeExistForPersonID(int PersonID)
        {
            return clsEmployeeData.IsEmployeeExistForPersonID(PersonID);
        }

    }
}
