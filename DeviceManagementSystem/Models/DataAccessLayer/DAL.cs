using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using DeviceManagementSystem.Models;
using DeviceManagementSystem.Models.BusinessObjectLayer;
using DeviceManagementSystem.Models.Utitlity;
using System.Collections;

namespace DeviceManagementSystem.Models.DataAccessLayer
{
    public class DAL
    {
        public void GetAdminLoginDetails(string User_Type,BO objBO, out DataSet objDataSet, out int errCnt, out string errMsg)
        {
            errCnt = 0;
            errMsg = null;
            objDataSet = new DataSet();

            String ParamsStoredProcedure = "";
            DataLayer localOutPutServer = new DataLayer();
            localOutPutServer.BuildParameter("@Email", objBO.UserName, DataLayer.SQLDataType.SQLString, 500, ParameterDirection.Input);
            localOutPutServer.BuildParameter("@Password", objBO.Password, DataLayer.SQLDataType.SQLString, 500, ParameterDirection.Input);
            localOutPutServer.BuildParameter("@errCount", errCnt, DataLayer.SQLDataType.SQLInteger, 11, ParameterDirection.Output);
            localOutPutServer.BuildParameter("@errMsg", errMsg, DataLayer.SQLDataType.SQLString, 4000, ParameterDirection.Output);
            if (User_Type == "Manager")
            {
                ParamsStoredProcedure = "SP_Get_Manager_Login";
            }
            if (User_Type == "Employee")
            {
                ParamsStoredProcedure = "SP_Get_Employee_Login";
            }
            //localDSOutput = localOutPutServer.runTransaction(ParamsStoredProcedure);
            objDataSet = localOutPutServer.returnDataSet(ParamsStoredProcedure, User_Type, out errCnt, out errMsg);

            errCnt = Convert.ToInt32(objDataSet.Tables[0].Rows[0][0].ToString());
            errMsg = objDataSet.Tables[0].Rows[0][1].ToString();
        }
        public void BindForAll(string SQuery, out DataSet objDataSet)
        {
            DataLayer objDataServer = new DataLayer();
            objDataSet = new DataSet();
            //string sQuery = "select APID, AppointFor from tbl_Appoint_For where Status=1";
            int iErrCnt = 0;
            string sErrMsg = null;
            String sParamsStoredProcedure;
            objDataServer.BuildParameter("@SQL", SQuery, DataLayer.SQLDataType.SQLString, 8000, ParameterDirection.Input);
            sParamsStoredProcedure = "USP_SqlExecute";
            objDataSet = objDataServer.returnDataSet(sParamsStoredProcedure, "Sync", out iErrCnt, out sErrMsg);
        }

        #region State
        public void Get_State_For_Active_Inactive(out DataSet objDataSet)
        {
            DataLayer objDataServer = new DataLayer();
            objDataSet = new DataSet();
            string sQuery = "select st.StateID, ct.CountryName, st.StateName, st.Status, st.CountryStatus from State_Table st inner join Country_Table ct on st.CountryID=ct.CountryID order by st.StateID asc";
            int iErrCnt = 0;
            string sErrMsg = null;
            String sParamsStoredProcedure;
            objDataServer.BuildParameter("@SQL", sQuery, DataLayer.SQLDataType.SQLString, 8000, ParameterDirection.Input);
            sParamsStoredProcedure = "USP_SqlExecute";
            objDataSet = objDataServer.returnDataSet(sParamsStoredProcedure, "Sync", out iErrCnt, out sErrMsg);
        }
        public void Get_State_For_Active_Inactive_ForSearch(BO objBO, out DataSet objDataSet)
        {
            DataLayer objDataServer = new DataLayer();
            objDataSet = new DataSet();
            string sQuery = "select * from State_Table where StateName LIKE '%" + objBO.Name + "%'";
            int iErrCnt = 0;
            string sErrMsg = null;
            String sParamsStoredProcedure;
            objDataServer.BuildParameter("@SQL", sQuery, DataLayer.SQLDataType.SQLString, 8000, ParameterDirection.Input);
            sParamsStoredProcedure = "USP_SqlExecute";
            objDataSet = objDataServer.returnDataSet(sParamsStoredProcedure, "Sync", out iErrCnt, out sErrMsg);
        }
        public void Update_State_Status(BO objBO, out DataSet objArrayList, out int errCnt, out string errMsg)
        {
            errCnt = 0;
            errMsg = null;
            objArrayList = new DataSet();

            ArrayList localDSOutput;
            String ParamsStoredProcedure;
            DataLayer localOutPutServer = new DataLayer();
            localOutPutServer.BuildParameter("@Operation", objBO.Operation, DataLayer.SQLDataType.SQLString, 100, ParameterDirection.Input);
            localOutPutServer.BuildParameter("@StateID", objBO.ID, DataLayer.SQLDataType.SQLInteger, 11, ParameterDirection.Input);
            localOutPutServer.BuildParameter("@errCount", errCnt, DataLayer.SQLDataType.SQLInteger, 11, ParameterDirection.Output);
            localOutPutServer.BuildParameter("@errMsg", errMsg, DataLayer.SQLDataType.SQLString, 4000, ParameterDirection.Output);

            ParamsStoredProcedure = "SP_UPDATE_State_STATUS";
            objArrayList = localOutPutServer.returnDataSet(ParamsStoredProcedure, "UPDATE_State_STATUS", out errCnt, out errMsg);

            errCnt = Convert.ToInt32(objArrayList.Tables[0].Rows[0][0].ToString());
            errMsg = objArrayList.Tables[0].Rows[0][1].ToString();
        }

        #endregion
        #region District

        public void BindState(out DataSet objDataSet)
        {
            DataLayer objDataServer = new DataLayer();
            objDataSet = new DataSet();
            string sQuery = "select StateID, StateName from State_Table where Status='Y'";
            int iErrCnt = 0;
            string sErrMsg = null;
            String sParamsStoredProcedure;
            objDataServer.BuildParameter("@SQL", sQuery, DataLayer.SQLDataType.SQLString, 8000, ParameterDirection.Input);
            sParamsStoredProcedure = "USP_SqlExecute";
            objDataSet = objDataServer.returnDataSet(sParamsStoredProcedure, "Sync", out iErrCnt, out sErrMsg);
        }
        public void BindDistrict(BO objBO, out DataSet objDataSet)
        {
            DataLayer objDataServer = new DataLayer();
            objDataSet = new DataSet();
            string sQuery = "select DistID, DistrictName from District_Table where StateID=" + objBO.StateID + " and Status='Y'";
            int iErrCnt = 0;
            string sErrMsg = null;
            String sParamsStoredProcedure;
            objDataServer.BuildParameter("@SQL", sQuery, DataLayer.SQLDataType.SQLString, 8000, ParameterDirection.Input);
            sParamsStoredProcedure = "USP_SqlExecute";
            objDataSet = objDataServer.returnDataSet(sParamsStoredProcedure, "Sync", out iErrCnt, out sErrMsg);
        }
        public void GetDistrictDetails(out DataSet objDataSet)
        {
            DataLayer objDataServer = new DataLayer();
            objDataSet = new DataSet();
            string sQuery = "select c.StateID, s.StateName, c.DistID, c.DistrictName, c.Status from District_Table c inner join State_Table s on c.StateID=s.StateID";
            int iErrCnt = 0;
            string sErrMsg = null;
            String sParamsStoredProcedure;
            objDataServer.BuildParameter("@SQL", sQuery, DataLayer.SQLDataType.SQLString, 8000, ParameterDirection.Input);
            sParamsStoredProcedure = "USP_SqlExecute";
            objDataSet = objDataServer.returnDataSet(sParamsStoredProcedure, "Sync", out iErrCnt, out sErrMsg);
        }
        public void IUD_District_Table(BO objBO, out DataSet objArrayList, out int errCnt, out string errMsg)
        {
            errCnt = 0;
            errMsg = null;
            objArrayList = new DataSet();

            ArrayList localDSOutput;
            String ParamsStoredProcedure;
            DataLayer localOutPutServer = new DataLayer();
            localOutPutServer.BuildParameter("@Operation", objBO.Operation, DataLayer.SQLDataType.SQLString, 100, ParameterDirection.Input);
            localOutPutServer.BuildParameter("@DistID", objBO.DistID, DataLayer.SQLDataType.SQLInteger, 11, ParameterDirection.Input);
            localOutPutServer.BuildParameter("@StateID", objBO.StateID, DataLayer.SQLDataType.SQLInteger, 11, ParameterDirection.Input);
            localOutPutServer.BuildParameter("@DistrictName", objBO.DistrictName, DataLayer.SQLDataType.SQLString, 100, ParameterDirection.Input);
            localOutPutServer.BuildParameter("@errCount", errCnt, DataLayer.SQLDataType.SQLInteger, 11, ParameterDirection.Output);
            localOutPutServer.BuildParameter("@errMsg", errMsg, DataLayer.SQLDataType.SQLString, 4000, ParameterDirection.Output);

            ParamsStoredProcedure = "SP_IUD_District_Table";
            objArrayList = localOutPutServer.returnDataSet(ParamsStoredProcedure, "Insert_District_Table", out errCnt, out errMsg);

            errCnt = Convert.ToInt32(objArrayList.Tables[0].Rows[0][0].ToString());
            errMsg = objArrayList.Tables[0].Rows[0][1].ToString();
        }
        public void Get_DistrictForSearch(BO objBO, out DataSet objDataSet)
        {
            DataLayer objDataServer = new DataLayer();
            objDataSet = new DataSet();
            string sQuery = "select c.StateID, s.StateName, c.DistID, c.DistrictName, c.Status from District_Table c inner join State_Table s on c.StateID=s.StateID where s.StateName LIKE '%" + objBO.DistrictName + "%' or c.DistrictName LIKE '%" + objBO.DistrictName + "%'";
            int iErrCnt = 0;
            string sErrMsg = null;
            String sParamsStoredProcedure;
            objDataServer.BuildParameter("@SQL", sQuery, DataLayer.SQLDataType.SQLString, 8000, ParameterDirection.Input);
            sParamsStoredProcedure = "USP_SqlExecute";
            objDataSet = objDataServer.returnDataSet(sParamsStoredProcedure, "Sync", out iErrCnt, out sErrMsg);
        }
        public void Get_DistrictByID(BO objBO, out DataSet objDataSet)
        {
            DataLayer objDataServer = new DataLayer();
            objDataSet = new DataSet();
            string sQuery = "select * from District_Table where DistID=" + objBO.DistID + "";
            int iErrCnt = 0;
            string sErrMsg = null;
            String sParamsStoredProcedure;
            objDataServer.BuildParameter("@SQL", sQuery, DataLayer.SQLDataType.SQLString, 8000, ParameterDirection.Input);
            sParamsStoredProcedure = "USP_SqlExecute";
            objDataSet = objDataServer.returnDataSet(sParamsStoredProcedure, "Sync", out iErrCnt, out sErrMsg);
        }
        public void GetDistrictByStateID(BO objBO, out DataSet objDataSet)
        {
            DataLayer objDataServer = new DataLayer();
            objDataSet = new DataSet();
            string sQuery = "select  DistID as StateID, DistrictName as StateName from District_Table where StateID=" + objBO.StateID + "";
            int iErrCnt = 0;
            string sErrMsg = null;
            String sParamsStoredProcedure;
            objDataServer.BuildParameter("@SQL", sQuery, DataLayer.SQLDataType.SQLString, 8000, ParameterDirection.Input);
            sParamsStoredProcedure = "USP_SqlExecute";
            objDataSet = objDataServer.returnDataSet(sParamsStoredProcedure, "Sync", out iErrCnt, out sErrMsg);
        }

        #endregion
    }
}