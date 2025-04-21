using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DeviceManagementSystem.Models;
using DeviceManagementSystem.Models.BusinessObjectLayer;
using DeviceManagementSystem.Models.DataAccessLayer;
using DeviceManagementSystem.Models.Utitlity;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace DeviceManagementSystem
{
    public partial class gtrslt : System.Web.UI.Page
    {
        BO objBO = new BO();
        DAL objDAL = new DAL();
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["UserName"] == null)
                Response.Redirect("~/default.aspx");

            ShowGrid();
        }
        public void ShowGrid()
        {
            string EmpolyeeID = DBUtility.GetEmpolyeeID(Session["UserName"].ToString());
            DataTable dt = DBUtility.returndata("select Device.Name as name,Duration,StartDate,location,DeviceUsage.ID as ID  from DeviceUsage inner join Device on DeviceUsage.DeviceID=Device.Id inner join Employee on Employee.ID=DeviceUsage.EmployeeID  where Employee.ID ='" + EmpolyeeID + "'");
            gv_Show.DataSource = dt;
            gv_Show.DataBind();
        }
        protected void lb_Logout_Click(object sender, EventArgs e)
        {
            Session.Abandon();
            Session.Clear();
            Response.Redirect("~/default.aspx");
        }

    }
}