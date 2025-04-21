<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="DeviceManagementSystem.Default1" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1" />
    <title> LogIn</title>
    <link rel="shortcut icon" href="images/pageicon.png" />
    <link href="css/new-style.css" rel="stylesheet" type="text/css" />

    <script lang="javascript" type="text/javascript">

        function ValidateEmail() {
            if (document.getElementById("txtUserId").value == "") {
                alert('Please Enter User Id !!');
                document.getElementById("txtUserId").focus();
                return false;
            }
            if (document.getElementById("txtPassword").value == "") {
                alert('Please Enter Password !!');
                document.getElementById("txtPassword").focus();
                return false;
            }
            if (document.getElementById("txtCaptcha").value == "") {
                alert('Please Enter Captcha !!');
                document.getElementById("txtCaptcha").focus();
                return false;
            }
        }
    </script>

    <script type="text/javascript">
        function blinkFont() {
            document.getElementById("blink1").style.color = "#333333"
            setTimeout("setblinkFont()", 500)
        }

        function setblinkFont() {
            document.getElementById("blink1").style.color = "#B21423"
            setTimeout("blinkFont()", 500)
        }
    </script>
    <link href="slider/css/style.css" rel="stylesheet" />
    <script src="slider/js/jquery.js"></script>
    <style>
        body {
            font-family: Arial, Helvetica, sans-serif;        
            background: #f5f5f5;

        }
        .loginMainPanel {         
            background: #ffff;
        }
        .loginMainPanel h2 {
  
    text-align: center;
}.loginMainPanel h2 span {
    padding: 5px 10px;
    background: white;
    display: inline-block;
    color: black;
}

        /* The Modal (background) */
        .modal {
            display: none; /* Hidden by default */
            position: fixed; /* Stay in place */
            z-index: 90; /* Sit on top */
            padding-top: 100px; /* Location of the box */
            left: 0;
            top: 0;
            width: 100%; /* Full width */
            height: 100%; /* Full height */
            overflow: auto; /* Enable scroll if needed */
            background-color: rgb(0,0,0); /* Fallback color */
            background-color: rgba(0,0,0,0.4); /* Black w/ opacity */
        }

        /* Modal Content */
        .modal-content {
            background-color: #f5f5f5;
            margin: auto;
            padding: 20px;
            padding-left: 45px;
            border: 1px solid #888;
            width: 26%;
            height: 330px;
        }

        /* The Close Button */
        .close {
            color: #aaaaaa;
            float: right;
            font-size: 28px;
            font-weight: bold;
        }

            .close:hover,
            .close:focus {
                color: #000;
                text-decoration: none;
                cursor: pointer;
            }
    </style>
    <link href="css/bootstrap.min.css" rel="stylesheet" >

</head>
<body>
    <form id="form1" runat="server">
 
       
     
        
        <script type="text/javascript" src="slider/js/script.js"></script>
        <!-- End WOWSlider.com BODY section -->










        <div class="cleardiv">
        </div>
        <div class="footer">
            <div class="wrapper">
               
            </div>
        </div>

        <!-- The Modal -->
        <!--<div id="myModal" class="modal">

            <!-- Modal content --
            <div class="modal-content">
                <span class="close">&times;</span>-->
                <div id="panelLogin" style="width:500px;margin: 0 auto;padding-top:30px;" onkeypress="javascript:return WebForm_FireDefaultButton(event, 'btnLogin')">

                    <div class="fr loginMainPanel">
                       
                        
                        <h2 class="margine15top">
                            <span>LOGIN</span></h2>
                        <div>
                            <span id="lblMessage"></span>
                        </div>
                          <!-- <div class="margine10top">
                          <label>Login User:</label>
                    
                        <asp:DropDownList ID="ddl_User_Type" runat="server" class="input-sm form-control w-sm inline v-middle" Width="100%">                            
                            <asp:ListItem Value="Manager">Manager</asp:ListItem>
                            <asp:ListItem Value="Employee">Employee</asp:ListItem>
                            
                        </asp:DropDownList>
                    </div>-->
                        <div class="margine10top">
                            <label>
                                Email <span style="color: Red">*</span></label>
                            <input name="txtUserId" id="txtUserId" class="form-control" runat="server" type="text" value="" placeholder="Enter UserName" required="" />
                        </div>
                        <div class="margine10top">
                            <label>
                                Password <span style="color: Red">*</span></label>
                            <input name="txtPassword" id="txtPassword" class="form-control" runat="server" type="Password" placeholder="Enter Password" required="" />
                        </div><br />
                        <!--<div class="check">
                            <label class="checkbox w3l">
                                <input type="checkbox" onclick="myFunction()" />
                                <i></i>show password
                            </label>
                        </div>-->
                        <!-- script for show password -->
                        <script>
                            function myFunction() {
                                var x = document.getElementById("txtPassword");
                                if (x.type === "password") {
                                    x.type = "text";
                                } else {
                                    x.type = "password";
                                }
                            }
                        </script>

                        <%--<div class="margine10top">
                            <table border="0" cellpadding="3" cellspacing="0">
                                <tr>
                                    <td colspan="3">
                                        <input name="txtcaptcha" id="txt_captcha" class="txt_bx" runat="server" type="text" value="" placeholder="Enter Below Captcha" required="" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <cc1:CaptchaControl ID="Captcha1" runat="server" CaptchaBackgroundNoise="Low" CaptchaLength="5"
                                            CaptchaHeight="60" CaptchaWidth="200" CaptchaMinTimeout="5" CaptchaMaxTimeout="240"
                                            FontColor="#D20B0C" NoiseColor="#B1B1B1" />
                                    </td>
                                    <td>
                                        <asp:ImageButton ImageUrl="~/refresh.png" runat="server" CausesValidation="false" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:CustomValidator ErrorMessage="Invalid. Please try again." OnServerValidate="ValidateCaptcha"
                                            runat="server" />
                                    </td>
                                    <td align="right">
                                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" />
                                    </td>
                                    <td></td>
                                </tr>
                            </table>
                        </div>--%>
                        <div class="d-grid gap-2" style="text-align: center;" class="margine10top">
                            <asp:Button ID="btn_Login" runat="server" Text="Login" class="btn btn-primary" OnClick="btn_Login_Click" />
                            <asp:Button ID="btn_Reset" runat="server" Text="Reset" class="reset_btn" OnClick="btn_Reset_Click" Visible="false" />
                        </div><br />
                    </div>

                </div>
            <!--</div>

        </div>-->

        <script>
            // Get the modal
            var modal = document.getElementById("myModal");

            // Get the button that opens the modal
            var btn = document.getElementById("myBtn");

            // Get the <span> element that closes the modal
            var span = document.getElementsByClassName("close")[0];

            // When the user clicks the button, open the modal 
            btn.onclick = function () {
                modal.style.display = "block";
            }

            // When the user clicks on <span> (x), close the modal
            span.onclick = function () {
                modal.style.display = "none";
            }

            // When the user clicks anywhere outside of the modal, close it
            window.onclick = function (event) {
                if (event.target == modal) {
                    modal.style.display = "none";
                }
            }
        </script>
    </form>
</body>
</html>
