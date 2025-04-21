using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using DeviceManagementSystem.Models;
using DeviceManagementSystem.Models.DataAccessLayer;
using DeviceManagementSystem.Models.BusinessObjectLayer;

namespace DeviceManagementSystem.Models.BusinessLogicLayer
{
    public class BAL
    {
        DAL objDAL = new DAL();
        public void BindQualification(out DataSet objDataSet)
        {
            string sQuery = "select QID, Qualification from tbl_Qualification where Status=1";
            objDAL.BindForAll(sQuery, out objDataSet);
        }
        public void GetPassword(BO objBO, out DataSet objDataSet)
        {
            string sQuery = "select Password from AMines_Login_Table where UserName='" + objBO.UserName + "'";
            objDAL.BindForAll(sQuery, out objDataSet);
        }
    }
}