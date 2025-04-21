using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DeviceManagementSystem.Models.Utitlity;
using DeviceManagementSystem.Models.BusinessObjectLayer;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.ComponentModel.Design;

namespace DeviceManagementSystem.manager
{
    public partial class devicemst : System.Web.UI.Page
    {
        BO objBO = new BO();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserName"] == null)
                Response.Redirect("~/default.aspx");

            if (!IsPostBack)
            {
                ShowGrid();
                BindMenu();
            }
        }
        public void BindMenu()
        {
            if (Session["UserName"] != null)
            {
                objBO.UserName = Session["UserName"].ToString();                
            }
            else
            {
                Response.Redirect("~/default.aspx");
            }
        }
        public void ShowGrid()
        {
            DataTable dt = DBUtility.returndata(" select * from Device inner join Manager on Device.ManagerID = Manager.ID ");
            gv_Show.DataSource = dt;
            gv_Show.DataBind();
        }
        public void Clear()
        {

            txt_name.Text = string.Empty;
            txt_location.Text = string.Empty;
            active.Checked = false;
            btn_Submit.Text = "Submit";
            ShowGrid();
        }
        private string Encrypt(string clearText)
        {
            string EncryptionKey = "CITBIT993M022ANC";
            byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    clearText = Convert.ToBase64String(ms.ToArray());
                }
            }
            return clearText;
        }
        private string Decrypt(string cipherText)
        {
            string EncryptionKey = "CITBIT993M022ANC";
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    cipherText = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return cipherText;
        }
        protected void btn_Cancel_Click(object sender, EventArgs e)
        {
            Clear();
        }
        protected void btn_Search_Click(object sender, EventArgs e)
        {
            if (txt_Search.Text.Trim() != string.Empty)
            {
                DataTable dTable = DBUtility.returndata("select  * from device where name LIKE '%" + txt_Search.Text.Trim() + "%' or location LIKE '%" + txt_Search.Text.Trim() + "%'");
                if (dTable.Rows.Count > 0)
                {
                    gv_Show.DataSource = dTable;
                    gv_Show.DataBind();
                }
                else
                {
                    //Response.Write("<script>alert('No Record Found with the Given Search Input!!!')</script>");
                    ShowGrid();
                }
            }
            else
            {
                ShowGrid();
            }
        }
        
        protected void btn_Submit_Click(object sender, EventArgs e)
        {
            try
            {

                if (txt_name.Text != string.Empty)
                    {

                    string location = txt_location.Text;// ddl_location.SelectedItem.ToString();
                    int activeint = 0;
                    if (active.Checked) activeint = 1;
                    string str = string.Empty;
                    string msg = string.Empty;
                    string ManagerID = DBUtility.GetManagerID(Session["UserName"].ToString());
                    if (btn_Submit.Text == "Submit")
                                {
                                    str = "insert into Device (Name, Location, IsActive,ManagerID) values ( '" + txt_name.Text.Trim() + "', '" + location + "', " + activeint + ","+ ManagerID + ")";
                                    msg = "Device Added Successfully...";
                                }
                                else
                                {
                                    str = "update Device set  Name='" + txt_name.Text.Trim() + "',Location='" + location+ "',IsActive=" + activeint+ " where ID=" + lbl_AID.Text + "";
                                    msg = "Device Updated Successfully...";
                                }
                                int i = DBUtility.returnExecuteNonQuery(str);
                                if (i > 0)
                                {
                                    Response.Write("<script>alert('" + msg + "')</script>");
                                    Clear();
                                }
                                else
                                    Response.Write("<script>alert('Transaction Failed!!!')</script>");
                           
                       
                    }
                    else
                    {
                        Response.Write("<script>alert('Enter Name!!!')</script>");
                        //txt_email.Focus();
                    }
                
               
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "')</script>");
            }
        }
        public void BindGridData(int ID)
        {
            DataTable dt = DBUtility.returndata("select * from device where ID=" + ID + "");
            if (dt.Rows.Count > 0)
            {
                bool verifyb = false;
                if (dt.Rows[0]["isactive"].ToString() == "True") verifyb = true;                
                active.Checked = verifyb;
                txt_name.Text = dt.Rows[0]["name"].ToString();
                //ddl_location. = Decrypt(dt.Rows[0]["Password"].ToString());
                //ddl_location.SelectedValue = dt.Rows[0]["location"].ToString();
                txt_location.Text = dt.Rows[0]["location"].ToString();
                lbl_AID.Text = dt.Rows[0]["ID"].ToString();                
                btn_Submit.Text = "Update";
                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(),"document.getElementById('myModal').style.display = 'block';", true);
            }
        }
        protected void gv_Show_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EditRow")
            {
                try
                {
                    var clickedButton = e.CommandSource as Button;
                    var clickedRow = clickedButton.NamingContainer as GridViewRow;
                    int rowIndex = int.Parse(e.CommandArgument.ToString());
                    BindGridData(Convert.ToInt32(gv_Show.DataKeys[rowIndex].Value.ToString()));
                }
                catch (Exception ex)
                {
                    Response.Write("<script>alert('" + ex.Message + "')</script>");
                }
            }
            else if (e.CommandName == "DeleteRow")
            {
                try
                {
                    var clickedButton = e.CommandSource as Button;
                    var clickedRow = clickedButton.NamingContainer as GridViewRow;
                    int rowIndex = int.Parse(e.CommandArgument.ToString());
                    int i = DBUtility.returnExecuteNonQuery("delete device where ID=" + gv_Show.DataKeys[rowIndex].Value.ToString() + "");
                    if (i > 0)
                    {
                        Response.Write("<script>alert('Record deleted Successfully...')</script>");
                        ShowGrid();
                    }
                    else
                    {
                        Response.Write("<script>alert('Transaction Failed!!!')</script>");
                    }
                }
                catch (Exception ex)
                {
                    Response.Write("<script>alert('" + ex.Message + "')</script>");
                }
            }
        }
        protected void gv_Show_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblStatus = e.Row.FindControl("lbl_Grid_Status") as Label;
                Image img1 = e.Row.FindControl("Image1") as Image;
                Image img2 = e.Row.FindControl("Image2") as Image;
                Button ib1 = e.Row.FindControl("imgb_Grid_Edit") as Button;
                Button ib2 = e.Row.FindControl("imgb_Grid_Delete") as Button;
                Label pwd = e.Row.FindControl("lbl_Grid_Password") as Label;
                if (lblStatus.Text == "True")
                {
                    img1.Visible = true;
                    img2.Visible = false;
                }
                else
                {
                    img2.Visible = true;
                    img1.Visible = false;
                }
                ib1.Visible = true;
                ib2.Visible = true;
                // pwd.Text = Decrypt(pwd.Text);

            }
        }
        
    }
}