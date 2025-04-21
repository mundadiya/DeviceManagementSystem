using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Xml;
using System.Collections;
using System.Diagnostics;
using System.Configuration;


namespace DeviceManagementSystem.Models.Utitlity
{
    public class DataLayer
    {
        #region
        //First: ADO .NET objects needed for data access are defined here
        string myConnectionString;
        SqlConnection myConnection;
        SqlCommand myCommand;
        SqlDataReader myDataReader;
        SqlDataAdapter mySQLDataAdapter;
        DataSet myDataSet;

        //We are going to handle parameters internally and ArrayList 'privateParameterList' is going to hold them
        ArrayList privateParameterList = new ArrayList();
        const string privateExceptionMessage = "Data Application Error.";

        public enum SQLDataType
        {
            SQLString,
            SQLChar,
            SQLInteger,
            SQLBit,
            SQLDateTime,
            SQLDecimal,
            SQLMoney,
            SQLImage,
            SQLTableType,
            SQLNVarchar
        }

        #endregion

        #region
        //Public Constructors"
        //public DataLayer(string user = null)
        //{
        //    DBConnection objDBC = new DBConnection();
        //    string ConnectionString = null;
        //    if (user == null)
        //    {
        //        ConnectionString = objDBC.GetDBConnection();
        //    }
        //    else
        //    {
        //        ConnectionString = objDBC.GetDBConnection(user);
        //    }

        //    myConnectionString = ConnectionString;
        //}
        //public DataLayer(string ConnectionString)
        //{
        //    myConnectionString = ConnectionString;
        //}
        #endregion

        #region

        public void BuildParameter(string ParameterName, object Value, SQLDataType SQLType, int Size, ParameterDirection Direction)
        {

            // Direction = ParameterDirection.Input;
            SqlDbType buildDataType = SqlDbType.VarChar;
            GeneralParameter buildParameter = null;
            switch (SQLType)
            {
                case SQLDataType.SQLString:
                    buildDataType = SqlDbType.VarChar;
                    break;
                case SQLDataType.SQLChar:
                    buildDataType = SqlDbType.Char;
                    break;
                case SQLDataType.SQLInteger:
                    buildDataType = SqlDbType.Int;
                    break;
                case SQLDataType.SQLBit:
                    buildDataType = SqlDbType.Bit;
                    break;
                case SQLDataType.SQLDateTime:
                    buildDataType = SqlDbType.DateTime;
                    break;
                case SQLDataType.SQLDecimal:
                    buildDataType = SqlDbType.Decimal;
                    break;
                case SQLDataType.SQLMoney:
                    buildDataType = SqlDbType.Money;
                    break;
                case SQLDataType.SQLImage:
                    buildDataType = SqlDbType.Image;
                    break;
                case SQLDataType.SQLTableType:
                    buildDataType = SqlDbType.Structured;
                    break;
                case SQLDataType.SQLNVarchar:
                    buildDataType = SqlDbType.NVarChar;
                    break;
            }

            buildParameter = new GeneralParameter(ParameterName, Value, buildDataType, Size, Direction);

            privateParameterList.Add(buildParameter);

        }

        public class GeneralParameter
        {
            public string Name;
            public object Value;
            public SqlDbType DataType;
            public int Size;
            public ParameterDirection DirectionUsed;

            public GeneralParameter(string pName, object pValue, SqlDbType pSQLType, int pSize, ParameterDirection pDirection)
            {
                Name = pName;
                Value = pValue;
                DataType = pSQLType;
                Size = pSize;
                DirectionUsed = pDirection;

            }
        }

        private SqlParameter ConvertToSqlParameters(GeneralParameter passedParameter)
        {
            SqlParameter returnSQLParameter = new SqlParameter();
            returnSQLParameter.ParameterName = passedParameter.Name;
            returnSQLParameter.Value = passedParameter.Value;
            returnSQLParameter.SqlDbType = passedParameter.DataType;
            returnSQLParameter.Size = passedParameter.Size;
            returnSQLParameter.Direction = passedParameter.DirectionUsed;
            return returnSQLParameter;

        }
        public void MakeClear()
        {
            privateParameterList.Clear();

        }
        #endregion

        #region "Execute a Select procedure and return a datatable"

        public DataTable returnDataTable(string SPName)
        {
            GeneralParameter privateUsedParameter;
            SqlParameter privateParameter;
            IEnumerator usedEnumerator = privateParameterList.GetEnumerator();
            try
            {
                myConnection = new SqlConnection(myConnectionString);
                DataTable privateDataTable = new DataTable();
                myCommand = new SqlCommand(SPName, myConnection);
                myCommand.CommandType = CommandType.StoredProcedure;
                while (usedEnumerator.MoveNext())
                {
                    privateUsedParameter = null;
                    privateUsedParameter = ((GeneralParameter)usedEnumerator.Current);
                    privateParameter = ConvertToSqlParameters(privateUsedParameter);
                    myCommand.Parameters.Add(privateParameter);
                }
                mySQLDataAdapter = new SqlDataAdapter(myCommand);
                //if (TableName == null)
                //{
                mySQLDataAdapter.Fill(privateDataTable);
                //}
                //else
                //{
                //    mySQLDataAdapter.Fill(privateDataSet, TableName);
                //}
                return privateDataTable;
            }
            catch (Exception e)
            {
                throw new Exception(privateExceptionMessage, e);
            }
            finally
            {
                myConnection.Close();
            }
        }

        #endregion

        #region

        public DataSet returnDataSet(string SPName, string TableName)
        {
            GeneralParameter privateUsedParameter;
            SqlParameter privateParameter;
            IEnumerator usedEnumerator = privateParameterList.GetEnumerator();
            try
            {
                myConnection = new SqlConnection(myConnectionString);
                DataSet privateDataSet = new DataSet();
                myCommand = new SqlCommand(SPName, myConnection);
                myCommand.CommandType = CommandType.StoredProcedure;
                while (usedEnumerator.MoveNext())
                {
                    privateUsedParameter = null;
                    privateUsedParameter = ((GeneralParameter)usedEnumerator.Current);
                    privateParameter = ConvertToSqlParameters(privateUsedParameter);
                    myCommand.Parameters.Add(privateParameter);
                }
                mySQLDataAdapter = new SqlDataAdapter(myCommand);
                if (TableName == null)
                {
                    mySQLDataAdapter.Fill(privateDataSet);
                }
                else
                {
                    mySQLDataAdapter.Fill(privateDataSet, TableName);
                }
                return privateDataSet;
            }
            catch (Exception e)
            {
                throw new Exception(privateExceptionMessage + e.Message.ToString(), e);
            }
            finally
            {
                myConnection.Close();
            }
        }
        #endregion

        #region "returnArrayList - A Stored Procedure is run and returns an ArrayList"
        public ArrayList returnArrayList(string SPName)
        {
            //Setting the objects to handle parameters
            GeneralParameter privateUsedParameter;        //will return the specific parameter in the privateParameterList
            SqlParameter privateParameter;         //will contain the converted SQLParameter
                                                   //The usedEnumerator makes it easy to step through the list of parameters in the privateParameterList
            IEnumerator usedEnumerator = privateParameterList.GetEnumerator();
            ArrayList outputParameters = new ArrayList(); //We need this arraylist to hold output parameters
                                                          //SqlParameter privateParameterOut;           //Helps to create the output parameter array
            try
            {
                //Set a new connection and DataSet
                myConnection = new SqlConnection(myConnectionString);
                //Define the command object and set commandtype to process Stored Procedure
                myCommand = new SqlCommand(SPName, myConnection);
                myCommand.CommandType = CommandType.StoredProcedure;
                //Move through the privateParameterList with the help of the enumerator
                while (usedEnumerator.MoveNext())
                {
                    privateUsedParameter = null;
                    //Get parameter in privateParameterList
                    privateUsedParameter = ((GeneralParameter)usedEnumerator.Current);
                    //Convert paramter to SQLParameter
                    privateParameter = ConvertToSqlParameters(privateUsedParameter);
                    //Add converted parameter to the myCommand object that imports data through the DataAdapter
                    myCommand.Parameters.Add(privateParameter);
                }
                myConnection.Open();
                myCommand.ExecuteNonQuery();
                //Iterate through all output parameters and return values
                //Iterate through all output parameters and return values
                foreach (SqlParameter privateParameterOut in myCommand.Parameters)
                {
                    if ((privateParameterOut.Direction == ParameterDirection.Output) || (privateParameterOut.Direction == ParameterDirection.ReturnValue))
                    {
                        //Add each output and return value to our output paramterlist
                        outputParameters.Add(privateParameterOut.Value);
                    }
                }
                //Return the array list of output parameter values
                return outputParameters;
            }
            catch (Exception e)
            {
                //The exception is passed to the calling code
                throw new Exception(privateExceptionMessage, e);
                // MessageBox.Show(e.Message, privateExceptionMessage, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                //Always close the connection as soon as possible(only then will object be allowed to go out of scope)
                myConnection.Close();
            }
        }
        #endregion

        #region "runSPOutput - A Stored Procedure is run with / without Parameters and returns a DataReader"
        //The runSPOutput function accepts a Stored Procedure that is required
        public ArrayList runSPOutput(string SPName)
        {
            //Setting the objects to handle parameters
            GeneralParameter privateUsedParameter;     //will return the specific parameter in the privateParameterList
            SqlParameter privateParameter;      //will contain the converted SQLParameter
                                                //The usedEnumerator makes it easy to step through the list of parameters in the privateParameterList
            IEnumerator usedEnumerator = privateParameterList.GetEnumerator();
            //We need this arraylist to hold output parameters
            ArrayList outputParameters = new ArrayList();
            //SqlParameter privateParameterOut;  //Helps to create the output parameter array
            try
            {


                myConnection = new SqlConnection(myConnectionString);
                //Define the command object and set commandtype to process Stored Procedure
                myCommand = new SqlCommand(SPName, myConnection);
                myCommand.CommandType = CommandType.StoredProcedure;
                //Move through the privateParameterList with the help of the enumerator
                while (usedEnumerator.MoveNext())
                {
                    privateUsedParameter = null;
                    //Get parameter in privateParameterList
                    privateUsedParameter = ((GeneralParameter)usedEnumerator.Current);
                    //Convert paramter to SQLParameter
                    privateParameter = ConvertToSqlParameters(privateUsedParameter);
                    //Add converted parameter to the myCommand object that imports data through the DataAdapter
                    myCommand.Parameters.Add(privateParameter);
                }
                myConnection.Open();
                myCommand.ExecuteNonQuery();
                //Iterate through all output parameters and return values
                foreach (SqlParameter privateParameterOut in myCommand.Parameters)
                {
                    if ((privateParameterOut.Direction == ParameterDirection.Output) || (privateParameterOut.Direction == ParameterDirection.ReturnValue))
                    {
                        //Add each output and return value to our output paramterlist
                        outputParameters.Add(privateParameterOut.Value);
                    }
                }
                //Return the array list of output parameter values
                return outputParameters;
            }
            catch (Exception e)
            {
                //The exception is passed to the calling code
                throw new Exception(privateExceptionMessage, e);
            }
            finally
            {
                //Always close the connection as soon as possible(only then will object be allowed to go out of scope)
                myConnection.Close();
            }
        }
        #endregion

        #region "runTransaction - A Stored Procedure is run and returns ArrayList"
        public ArrayList runTransaction(String SPName)
        {
            GeneralParameter privateUsedParameter;     //will return the specific parameter in the privateParameterList
            SqlParameter privateParameter;             //will contain the converted SQLParameter
                                                       //The usedEnumerator makes it easy to step through the list of parameters in the privateParameterList
            IEnumerator usedEnumerator = privateParameterList.GetEnumerator();
            //We need this arraylist to hold output parameters
            ArrayList outputParameters = new ArrayList();
            //SqlParameter privateParameterOut;          //Helps to create the output parameter array
            try
            {
                //Check to see if this object has already been disposed
                //If privateDisposedBoolean = True Then
                //Throw New ObjectDisposedException(privateModuleName, "This object has already been disposed. You cannot reuse it")
                //End If
                //Set a new connection and DataSet
                myConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString);
                //Define the command object and set commandtype to process Stored Procedure
                myCommand = new SqlCommand(SPName, myConnection);
                myCommand.CommandType = CommandType.StoredProcedure;
                //Move through the privateParameterList with the help of the enumerator
                while (usedEnumerator.MoveNext())
                {
                    privateUsedParameter = null;
                    //Get parameter in privateParameterList
                    privateUsedParameter = ((GeneralParameter)usedEnumerator.Current);
                    //Convert paramter to SQLParameter
                    privateParameter = ConvertToSqlParameters(privateUsedParameter);
                    //Add converted parameter to the myCommand object that imports data through the DataAdapter
                    myCommand.Parameters.Add(privateParameter);
                }
                myConnection.Open();
                myCommand.ExecuteNonQuery();
                //Iterate through all output parameters and return values
                foreach (SqlParameter privateParameterOut in myCommand.Parameters)
                {
                    if ((privateParameterOut.Direction == ParameterDirection.Output) || (privateParameterOut.Direction == ParameterDirection.ReturnValue))
                    {
                        //Add each output and return value to our output paramterlist
                        outputParameters.Add(privateParameterOut.Value);
                    }
                }
                //Return the array list of output parameter values
                return outputParameters;
            }
            catch (Exception e)
            {
                //The exception is passed to the calling code
                throw new Exception(privateExceptionMessage + " - " + e.Message.ToString(), e);
            }
            finally
            {
                //Always close the connection as soon as possible(only then will object be allowed to go out of scope)
                myConnection.Close();
            }
        }

        public ArrayList runTransaction1(String SPName, out int errCnt, out string errMsg)
        {
            errCnt = 0;
            errMsg = null;
            // eid = null;,out string eid
            GeneralParameter privateUsedParameter;     //will return the specific parameter in the privateParameterList
            SqlParameter privateParameter;             //will contain the converted SQLParameter
                                                       //The usedEnumerator makes it easy to step through the list of parameters in the privateParameterList
            IEnumerator usedEnumerator = privateParameterList.GetEnumerator();
            //We need this arraylist to hold output parameters
            ArrayList outputParameters = new ArrayList();
            //SqlParameter privateParameterOut;          //Helps to create the output parameter array
            try
            {
                //Check to see if this object has already been disposed
                //If privateDisposedBoolean = True Then
                //Throw New ObjectDisposedException(privateModuleName, "This object has already been disposed. You cannot reuse it")
                //End If
                //Set a new connection and DataSet
                myConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString);
                //Define the command object and set commandtype to process Stored Procedure
                myCommand = new SqlCommand(SPName, myConnection);
                myCommand.CommandType = CommandType.StoredProcedure;
                //Move through the privateParameterList with the help of the enumerator
                while (usedEnumerator.MoveNext())
                {
                    privateUsedParameter = null;
                    //Get parameter in privateParameterList
                    privateUsedParameter = ((GeneralParameter)usedEnumerator.Current);
                    //Convert paramter to SQLParameter
                    privateParameter = ConvertToSqlParameters(privateUsedParameter);
                    //Add converted parameter to the myCommand object that imports data through the DataAdapter
                    myCommand.Parameters.Add(privateParameter);
                }
                myConnection.Open();
                try
                {
                    myCommand.ExecuteNonQuery();
                }
                catch (SqlException exe)
                {
                    errCnt++;
                    errMsg = exe.Message;

                }
                //Iterate through all output parameters and return values
                foreach (SqlParameter privateParameterOut in myCommand.Parameters)
                {
                    if ((privateParameterOut.Direction == ParameterDirection.Output) || (privateParameterOut.Direction == ParameterDirection.ReturnValue))
                    {
                        //Add each output and return value to our output paramterlist
                        outputParameters.Add(privateParameterOut.Value);
                    }
                }
                //Return the array list of output parameter values
                return outputParameters;
            }
            catch (Exception e)
            {
                //The exception is passed to the calling code
                errCnt++;
                errMsg = e.Message;
                throw new Exception(privateExceptionMessage + " - " + e.Message.ToString(), e);
            }
            finally
            {
                //Always close the connection as soon as possible(only then will object be allowed to go out of scope)
                myConnection.Close();
            }
        }
        public DataSet returnDataSet(string SPName, string TableName, out int errCnt, out string errMsg)
        {
            errCnt = 0;
            errMsg = null;
            GeneralParameter privateUsedParameter;
            SqlParameter privateParameter;
            IEnumerator usedEnumerator = privateParameterList.GetEnumerator();
            try
            {
                myConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString);
                DataSet privateDataSet = new DataSet();
                myCommand = new SqlCommand(SPName, myConnection);
                myCommand.CommandType = CommandType.StoredProcedure;
                while (usedEnumerator.MoveNext())
                {
                    privateUsedParameter = null;
                    privateUsedParameter = ((GeneralParameter)usedEnumerator.Current);
                    privateParameter = ConvertToSqlParameters(privateUsedParameter);
                    myCommand.Parameters.Add(privateParameter);
                }
                mySQLDataAdapter = new SqlDataAdapter(myCommand);
                if (TableName == null)
                {
                    mySQLDataAdapter.Fill(privateDataSet);
                }
                else
                {
                    mySQLDataAdapter.Fill(privateDataSet, TableName);
                }
                return privateDataSet;
            }
            catch (Exception e)
            {
                errCnt++;
                errMsg = e.Message;
                throw new Exception(privateExceptionMessage + e.Message.ToString(), e);
            }
            finally
            {
                myConnection.Close();
            }
        }
        #endregion
    }
}