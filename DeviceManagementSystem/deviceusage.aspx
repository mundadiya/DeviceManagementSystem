<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="deviceusage.aspx.cs" Inherits="DeviceManagementSystem.gtrslt" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Device Usages</title>
    <link rel="shortcut icon" href="images/pageicon.png" />
    <link href="css/new-style.css" rel="stylesheet" type="text/css" />
    <link href="css/bootstrap.min.css" rel="stylesheet" type="text/css" />
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
        .auto-style1 {
            width: 154px;
            font-weight: bold;
        }
        .auto-style2 {
            width: 154px;
            font-weight: bold;
        }
        td, th {
    padding: 10px;
    font-size: 20px;
        
}
        th {
    background-color: #f25223 !important;
}
    </style>
</head>
<body style="background-color: #f5f5f5;">
    <form id="form1" runat="server">


        <div class="top_header">
        </div>
        <div class="header">
            <div class="wrapper">
                <div class="fl">
                    <div class="margine30top fs22" style="color: #f5830c;">
                        <a href="Default.aspx" style="color: #f5830c; text-decoration: none;"><strong>Device Management System</strong></a>
                    </div>
                </div>
                <div class="fr">

                    <%--<button id="myBtn">Open Modal</button>--%>
                    <a href="Default.aspx"><img src="images/pageicon.png" alt="a" style="height: 50px;" /></a>                    
                   <asp:LinkButton ID="lb_Logout" runat="server" OnClick="lb_Logout_Click" style="background-color: #f25223; color: white; cursor: pointer; font-weight: bold; padding-top: 3px; padding-bottom: 3px; margin-left: 20px;"><i class="fa fa-key"></i>Logout</asp:LinkButton>
                </div>
                <div class="cleardiv">
                </div>
            </div>
        </div>
        <div class="menu_m">
            <div class="wrapper">
                <div class="fr">
                    <div class="fl">
                        <img src="images/clock_icon.jpg" alt="" align="absmiddle" />
                    </div>
                    <div class="fl pad5top">
                        <span id="clock"></span>
                    </div>
                    <div class="cleardiv">
                    </div>
                </div>
                <div class="cleardiv">
                </div>
            </div>
        </div>
       

        <div class="col-sm-12" style="width: 100%; padding-top: 10px; padding-left: 30px; ">
                <h3>My Usage</h3>
 <hr />
            Hello, <%=Session["UserName"].ToString()  %>
            <div class="fr loginMainPanel" >
                <h5>My Usage</h5>
              <br />
                    <div class="margine10top" id="div1" runat="server">
                           <asp:GridView ID="gv_Show" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" Width="100%" 
      DataKeyNames="ID" >
       <AlternatingRowStyle BackColor="White" />
       <FooterStyle BackColor="#1cc7d0" Font-Bold="True" ForeColor="White" />
       <HeaderStyle BackColor="#1cc7d0" Font-Bold="True" ForeColor="White" Font-Size="11pt" />
       <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
       <RowStyle BackColor="#E3EAEB" Font-Size="10pt" />
       <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
       <SortedAscendingCellStyle BackColor="#F8FAFA" />
       <SortedAscendingHeaderStyle BackColor="#246B61" />
       <SortedDescendingCellStyle BackColor="#D4DFE1" />
       <SortedDescendingHeaderStyle BackColor="#15524A" />
       <Columns>
        
           <asp:TemplateField Visible="false">
               <HeaderTemplate>SlNo</HeaderTemplate>
               <ItemTemplate>
                   <%# Eval("Id")%>
               </ItemTemplate>
           </asp:TemplateField>
            
             <asp:TemplateField>
      <HeaderTemplate>Date</HeaderTemplate>
      <ItemTemplate>
        <%#  String.Format("{0:MMMM dd, yyyy hh:mm tt}",Eval("StartDate"))%>
    </ItemTemplate>
                  </asp:TemplateField>
           <asp:TemplateField>
               <HeaderTemplate>Device</HeaderTemplate>
               <ItemTemplate>
                   <%# Eval("name")%>
               </ItemTemplate>
           </asp:TemplateField>                       
           <asp:TemplateField>
               <HeaderTemplate>Location</HeaderTemplate>
               <ItemTemplate>
                   <%# Eval("location")%>
               </ItemTemplate>
           </asp:TemplateField>
         
         
                     <asp:TemplateField>
              <HeaderTemplate>Duration</HeaderTemplate>
              <ItemTemplate>
    <%# Eval("Duration")%> Minutes
</ItemTemplate>
          </asp:TemplateField>


           
       </Columns>
   </asp:GridView>
                        
                </div>
            </div>
        </div>



        <div class="cleardiv">
        </div>
        <div class="footer">
            <div class="wrapper">
                <div>
                    <div class="fl">
                        &copy; 2021-<%=DateTime.Now.ToString("yyyy") %>. All Rights Reserved.
                    </div>
                  
                    <div class="cleardiv">
                    </div>
                </div>
                <hr />
            </div>
        </div>


        
    </form>
</body>
</html>
