using System;
using System.Data;
using DVLD_DataAccess;

namespace DVLD_Buisness
{
    public class clsDepartment
    {

        public string DepartmentID { set; get; }
        public string DepartmentName { set; get; }

        public clsDepartment()

        {
            this.DepartmentID = ""; ;
            this.DepartmentName = "";

        }

        private clsDepartment(string DepartmentID, string DepartmentName)

        {
            this.DepartmentID = DepartmentID;
            this.DepartmentName = DepartmentName;
        }

        public static clsDepartment Find(string ID)
        {
            string DepartmentName = "";

            if (clsDepartmentData.GetDepartmentsInfoByID(ID, ref DepartmentName))

                return new clsDepartment(ID, DepartmentName);
            else
                return null;

        }

        public static clsDepartment FindByName(string DepartmentName)
        {

            string ID = "";

            if (clsDepartmentData.GetDepartmentsInfoByName(DepartmentName, ref ID))

                return new clsDepartment(ID, DepartmentName);
            else
                return null;

        }

        public static DataTable GetAllDepartments()
        {
            return clsDepartmentData.GetAllDepartments();

        }

    }
}
