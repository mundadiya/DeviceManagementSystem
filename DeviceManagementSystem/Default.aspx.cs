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
    public partial class Default : System.Web.UI.Page
    {
        BO objBO = new BO();
        DAL objDAL = new DAL();
        protected void Page_Load(object sender, EventArgs e)
        {
            //Response.Write("<script>alert('" + Decrypt("KTwU+C5ZAbPE+9jW/pfXCdh7T3wGnqZtR2+CNQflFnQ=") + "')</script>");
            //Response.Write("<script>alert('" + Decrypt("4qL1sWkb+ObDOiw/smOJgA==") + "')</script>");
        }
       
        protected void btn_Login_Click(object sender, EventArgs e)
        {
           
        }
        protected void btn_Reset_Click(object sender, EventArgs e)
        {
        }
       
    }
}