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

namespace DeviceManagementSystem.manager
{
    public partial class employeemst : System.Web.UI.Page
    {
        BO objBO = new BO();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserName"] == null)
                Response.Redirect("~/default.aspx");

            if (!IsPostBack)
            {
                ShowGrid();
                ShowDeviceGrid();
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
        public void ShowDeviceGrid()
        {
            DataTable dt = DBUtility.returndata(" select Device.[ID],Manager.Name as Mname,Device.Name as Dname,[Location],[IsActive] from Device inner join Manager on Device.ManagerID = Manager.ID ");
            GridViewDevice.DataSource = dt;
            GridViewDevice.DataBind();
        }
        public void ShowGrid(string search ="")
        {
            string where = "";
            if (search != "") where = " where email like '%" + search + "' or  name like '%"+ search+"' ";

            DataTable dt = DBUtility.returndata("select * from Employee "+ where);
            gv_Show.DataSource = dt;
            gv_Show.DataBind();
        }
        public void Clear()
        {
            
            txt_email.Text = string.Empty;
            txt_Password.Text = string.Empty;
            txt_Search.Text = string.Empty;
            lbl_AID.Text = string.Empty;
            txt_name.Text = string.Empty;
            verify.Checked = false;
            txt_email.ReadOnly = false;
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
            ShowGrid(txt_Search.Text);
        }
        public string CheckUserName()
        {
            string check = string.Empty;
            if (btn_Submit.Text == "Submit")
            {
                DataTable dt = DBUtility.returndata("select count(*) from Employee where email='" + txt_email.Text.Trim() + "'");
                if (Convert.ToInt32(dt.Rows[0][0].ToString()) > 0)
                    check = "E";
                else
                    check = "N";
            }
            else
                check = "N";
            return check;
        }
        protected void btn_Submit_Click(object sender, EventArgs e)
        {
            try
            {

                if (txt_email.Text != string.Empty)
                {
                    if (txt_Password.Text != string.Empty)
                    {
                        string check = CheckUserName();
                        int varifyint = 0;
                        if (verify.Checked) varifyint = 1;
                        if (check == "N")
                        {
                            string str = string.Empty;
                            string msg = string.Empty;


                            string ManagerID = DBUtility.GetManagerID(Session["UserName"].ToString());
                            if (btn_Submit.Text == "Submit")
                            {
                                str = "insert into Employee (email, pass, verified,name,ManagerID,Salt) values ( '" + txt_email.Text.Trim() + "', '" + Encrypt(txt_Password.Text.Trim()) + "', " + varifyint + ",'" + txt_name.Text.Trim() + "',"+ ManagerID + ",'')";
                                msg = "Employee Created Successfully...";
                            }
                            else
                            {
                                str = "update Employee set verified='" + varifyint + "',name='" + txt_name.Text.Trim() + "',pass='" + Encrypt(txt_Password.Text.Trim()) + "' where ID=" + lbl_AID.Text + "";
                                msg = "Employee Updated Successfully...";
                            }
                            int i = DBUtility.returnExecuteNonQuery(str);
                            


                            foreach (GridViewRow row in GridViewDevice.Rows)
                            {
                                CheckBox chk = (CheckBox)row.FindControl("chkSelect");

                                if (chk != null && chk.Checked)
                                {
                                    // Example: get ID and Name from selected row
                                    string DeviceID = GridViewDevice.DataKeys[row.RowIndex].Value.ToString();
                                    // string name = row.Cells[2].Text; // Adjust index based on your layout

                                    // Do something with selected data
                                    // e.g., save to database or process selection
                                    string EmployeeID = DBUtility.GetEmpolyeeID(txt_email.Text.Trim());                                   
                                    if (EmployeeID != "0")
                                    {                                        
                                        string strUsage = "insert into DeviceUsage  (DeviceID , EmployeeID , StartDate ,Duration ) values ( " + DeviceID + ", " + EmployeeID + ",getdate(),3)";
                                        int ii = DBUtility.returnExecuteNonQuery(strUsage);
                                    }
                                }
                            }

                            if (i > 0)
                            {
                                Response.Write("<script>alert('" + msg + "')</script>");
                                Clear();
                            }
                            else
                            {
                                Response.Write("<script>alert('Transaction Failed!!!')</script>");
                            }

                        }
                        else
                        {
                            Response.Write("<script>alert('Entered UserName is Already Exists!!!')</script>");
                            txt_email.Focus();
                        }
                    }
                    else
                    {
                        Response.Write("<script>alert('Enter Password!!!')</script>");
                        txt_Password.Focus();
                    }
                }
                else
                {
                    Response.Write("<script>alert('Enter Email!!!')</script>");
                    txt_email.Focus();
                }
                
               
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "')</script>");
            }
        }
        public void BindGridData(int ID)
        {
            DataTable dt = DBUtility.returndata("select * from Employee where ID=" + ID + "");
            if (dt.Rows.Count > 0)
            {
                bool verifyb = false;
                if (dt.Rows[0]["verified"].ToString() == "True") verifyb = true;
                txt_email.Text = dt.Rows[0]["email"].ToString();
                txt_email.ReadOnly = true;
                txt_name.Text = dt.Rows[0]["name"].ToString();
                //txt_Password.Attributes["type"] = "text";
                txt_Password.Text = Decrypt(dt.Rows[0]["pass"].ToString());
                verify.Checked = verifyb;
                //txt_Password.Attributes["type"] = "password";
                lbl_AID.Text = dt.Rows[0]["ID"].ToString();                
                btn_Submit.Text = "Update";
                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "document.getElementById('myModal').style.display = 'block';", true);
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
                    int i = DBUtility.returnExecuteNonQuery("delete Employee where ID=" + gv_Show.DataKeys[rowIndex].Value.ToString() + "");
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
                pwd.Text = Decrypt(pwd.Text);
            }
        }
        protected void txt_Email_TextChanged(object sender, EventArgs e)
        {
            if (txt_email.Text != string.Empty)
            {
                string check = CheckUserName();
                if (check == "E")
                    Response.Write("<script>alert('Entered Email is Already Exist!!!')</script>");
                else
                    txt_Password.Focus();
            }
            else
            {
                Response.Write("<script>alert('Enter email!!!')</script>");
                txt_email.Focus();
            }
        }
    }
}