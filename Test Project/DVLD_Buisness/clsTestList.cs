using System;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using DVLD_DataAccess;

namespace DVLD_Buisness
{
    public class clsTestList
    {

        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;
      public enum enTestList { VisionTest = 1, WrittenTest = 2, StreetTest = 3 };

        public clsTestList.enTestList ID { set; get; }
        public string Title { set; get; }
        public string Description { set; get; }
        public float Fees { set; get; }
        public clsTestList()

        {
            this.ID = clsTestList.enTestList.VisionTest;
            this.Title = "";
            this.Description = "";
            this.Fees = 0;
            Mode = enMode.AddNew;

        }

        public clsTestList(clsTestList.enTestList ID, string TestListTitel, string Description, float TestListFees)

        {
            this.ID = ID;
            this.Title = TestListTitel;
            this.Description = Description;

            this.Fees = TestListFees;
            Mode = enMode.Update;
        }

        private bool _AddNewTestList()
        {
            //call DataAccess Layer 

            this.ID = (clsTestList.enTestList)clsTestListData.AddNewTestList(this.Title, this.Description, this.Fees);

            return (this.Title != "");
        }

        private bool _UpdateTestList()
        {
            //call DataAccess Layer 

            return clsTestListData.UpdateTestList((int)this.ID, this.Title, this.Description, this.Fees);
        }

        public static clsTestList Find(clsTestList.enTestList TestListID)
        {
            string Title = "", Description = ""; float Fees = 0;

            if (clsTestListData.GetTestListInfoByID((int)TestListID, ref Title, ref Description, ref Fees))

                return new clsTestList(TestListID, Title, Description, Fees);
            else
                return null;

        }

        public static DataTable GetAllTestLists()
        {
            return clsTestListData.GetAllTestLists();

        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewTestList())
                    {

                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:

                    return _UpdateTestList();

            }

            return false;
        }

    }
}
