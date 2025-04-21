using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using DeviceManagementSystem.Models;
using DeviceManagementSystem.Models.BusinessLogicLayer;
using DeviceManagementSystem.Models.BusinessObjectLayer;
using DeviceManagementSystem.Models.DataAccessLayer;
using DeviceManagementSystem.Models.Utitlity;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace DeviceManagementSystem.manager
{
    public partial class admin : System.Web.UI.MasterPage
    {
        BO objBO = new BO();
        BAL objBAL = new BAL();
        DAL objDAL = new DAL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserName"] != null)
            {
                if (!IsPostBack)
                {
                    lbl_SessionUserName.Text = Session["UserName"].ToString();
                    MenuSubMenu();
                }
            }
            else
                Response.Redirect("~/default.aspx");
        }
        public void MenuSubMenu()
        {
            if (Session["UserName"] != null)
            {
                objBO.UserName = Session["UserName"].ToString();
                if (objBO.UserName == "Admin" || objBO.UserName == "admin" || objBO.UserName == "ADMIN")
                {
                    li_Menu_Dashboard.Visible = true;
                    li_Menu_Master.Visible = true;
                  //  li_SM_Admission.Visible = true;
                    li_SM_User.Visible = true;

                   // li_Menu_Report.Visible = true;
                   // li_SR_Admission.Visible = true;
                   // li_SR_ReportPrint.Visible = true;
                }
                else
                {
                   /* DataTable dt = DBUtility.returndata("select UserType from Manager where Email='" + Session["UserName"].ToString() + "'");
                    string utype = dt.Rows[0][0].ToString();
                    if (utype == "User")
                    {
                        li_Menu_Dashboard.Visible = true;
                        li_Menu_Master.Visible = true;
                        li_SM_Admission.Visible = true;
                        li_SM_User.Visible = false;

                        li_Menu_Report.Visible = true;
                        li_SR_Admission.Visible = true;
                        li_SR_ReportPrint.Visible = true;
                    }
                    else if (utype == "RTO")
                    {
                        li_Menu_Dashboard.Visible = true;
                        li_Menu_Master.Visible = false;
                        li_SM_Admission.Visible = false;
                        li_SM_User.Visible = false;

                        li_Menu_Report.Visible = true;
                        li_SR_Admission.Visible = true;
                        li_SR_ReportPrint.Visible = false;
                    }*/
                }
            }
            else
            {
                Response.Redirect("~/default.aspx");
            }
        }
        protected void lb_Logout_Click(object sender, EventArgs e)
        {
            Session.Abandon();
            Session.Clear();
            Response.Redirect("~/default.aspx");
        }
    }
}