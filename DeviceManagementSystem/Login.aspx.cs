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
using System.Drawing;

namespace DeviceManagementSystem
{
    public partial class Default1 : System.Web.UI.Page
    {
        BO objBO = new BO();
        DAL objDAL = new DAL();
        protected void Page_Load(object sender, EventArgs e)
        {
            //Response.Write("<script>alert('" + Decrypt("KTwU+C5ZAbPE+9jW/pfXCdh7T3wGnqZtR2+CNQflFnQ=") + "')</script>");
            //Response.Write("<script>alert('" + Decrypt("4qL1sWkb+ObDOiw/smOJgA==") + "')</script>");
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
        protected void btn_Login_Click(object sender, EventArgs e)
        {
            if (txtUserId.Value != string.Empty)
            {
                if (txtPassword.Value != string.Empty)
                {
                    DataSet objDataSet = new DataSet();
                    try
                    {
                        string ddl_UserType = "Employee";// ddl_User_Type.SelectedItem.ToString();
                        int errCnt = 0;
                        string errMsg = "";
                        objBO.UserName = txtUserId.Value;                        
                        objBO.Password = Encrypt(txtPassword.Value);
                        objDAL.GetAdminLoginDetails(ddl_UserType, objBO, out objDataSet, out errCnt, out errMsg);
                        if (errCnt == 0)
                        {
                            try
                            {
                                int CY = Convert.ToInt32(DateTime.Now.ToString("yyyy"));
                                string FY = CY.ToString() + "-" + (CY + 1).ToString();
                              
                            }
                            catch { }
                            Session["UserName"] = txtUserId.Value;
                            Session["UserID"] = txtUserId.Value;
                            if (ddl_UserType == "Manager")
                            {
                                Response.Redirect("~/manager/home.aspx");
                            }
                            if (ddl_UserType == "Employee")
                            {
                                Response.Redirect("~/deviceusage.aspx");
                           }
                        }
                        else
                            Response.Write("<script>alert('" + objDataSet.Tables[0].Rows[0][1].ToString() + "')</script>");
                    }
                    catch (Exception exe)
                    {
                        Response.Write("<script>alert('" + exe.Message + "')</script>");
                        //message = exe.Message;
                    }
                }
                else
                {
                    Response.Write("<script>alert('Enter Your Password!!!')</script>");
                    txtPassword.Focus();
                }
            }
            else
            {
                Response.Write("<script>alert('Enter Your UserName!!!')</script>");
                txtUserId.Focus();
            }
        }
        protected void btn_Reset_Click(object sender, EventArgs e)
        {
            txtUserId.Value = string.Empty;
            txtPassword.Value = string.Empty;
        }
        //protected void ValidateCaptcha(object sender, ServerValidateEventArgs e)
        //{
        //    Captcha1.ValidateCaptcha(txt_captcha.Value.Trim());
        //    e.IsValid = Captcha1.UserValidated;
        //    if (e.IsValid)
        //    {
        //        ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Valid Captcha!');", true);
        //    }
        //}
    }
}