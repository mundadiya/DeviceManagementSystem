using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace DeviceManagementSystem.Models.Utitlity
{
    public class DBUtility
    {
        static public SqlConnection conn()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString);

            con.Open();
            return con;
        }
        public static int returnscl(string sql)
        {
            SqlCommand com = new SqlCommand(sql, conn());
            int id = 0;
            try
            {
                id = Convert.ToInt32(com.ExecuteScalar());

            }
            catch
            {
                id = 0;
            }
            return id;
        }
        public static int returnExecuteNonQuery(string sql)
        {
            SqlCommand com = new SqlCommand(sql, conn());
            int id = 0;
            try
            {
                id = com.ExecuteNonQuery();
            }
            catch { id = 0; }
            return id;
        }
        public static DataTable returndata(string sql1)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(sql1, conn());
            da.Fill(dt);
            return dt;
        }

        public static string GetEmpolyeeID(string email)
        {
            string EmployeeID = "0";
            DataTable dtemp = returndata("select ID from Employee where Email='" + email.Trim() + "'");
            if (dtemp.Rows.Count > 0)
            {
                EmployeeID = dtemp.Rows[0]["ID"].ToString();
            }
            return EmployeeID;
        }
        public static string GetManagerID(string email)
        {
            string ManagerID = "0";
            DataTable dtemp = returndata("select ID from manager where Email='" + email.Trim() + "'");
            if (dtemp.Rows.Count > 0)
            {
                ManagerID = dtemp.Rows[0]["ID"].ToString();
            }
            return ManagerID;
        }
    }
}