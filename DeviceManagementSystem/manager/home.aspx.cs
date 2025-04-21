using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DeviceManagementSystem.Models.Utitlity;

namespace DeviceManagementSystem.manager
{
    public partial class home : System.Web.UI.Page
    {
        public DataTable dtStaff;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserName"] == null)
                Response.Redirect("~/default.aspx");

            if (!IsPostBack)
            {
              
            }
        }
    }
}