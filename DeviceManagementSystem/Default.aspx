<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="DeviceManagementSystem.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1" />
    <title>Device Management System</title>
    <link rel="shortcut icon" href="images/pageicon.png" />
    <link href="css/new-style.css" rel="stylesheet" type="text/css" />



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
 body {
  /*font: 22px Arial, sans-serif;*/
}

#container {
    position: absolute;
    top: 50%;
    left: 50%;
    transform: translateX(-50%) translateY(-50%);
}
/* Style The Dropdown Button */
.dropbtn {
   background-color: #e44a0c;
  color: white;
  padding-right: 25px;
  font-size: 16px;
  border: none;
  cursor: pointer;
  text-align: center;
  padding-left: 25px;
  padding-top: 10px;
  padding-bottom: 10px;
}

/* The container <div> - needed to position the dropdown content */
.dropdown {
  position: relative;
  display: inline-block;
}

/* Dropdown Content (Hidden by Default) */
.dropdown-content {
  display: none;
  position: absolute;
  background-color: #f9f9f9;
  min-width: 160px;
  box-shadow: 0px 8px 16px 0px rgba(0,0,0,0.2);
  z-index: 1;
}

/* Links inside the dropdown */
.dropdown-content a {
  color: black;
  padding: 12px 16px;
  text-decoration: none;
  display: block;
}

/* Change color of dropdown links on hover */
.dropdown-content a:hover {background-color: #f1f1f1}

/* Show the dropdown menu on hover */
.dropdown:hover .dropdown-content {
  display: block;
}

/* Change the background color of the dropdown button when the dropdown content is shown */
.dropdown:hover .dropbtn {
  background-color: #e44a0c;
}
    </style>
</head>
<body>
    <form id="form1" runat="server">


        <div class="top_header">
        </div>
        <div class="header">
            <div class="wrapper">
                <div class="fl">
                    <div class="margine30top fs22" style="color: #f5830c;">
                        <strong> Device Management System</strong>
                    </div>
                </div>
                <div class="fr">
                    <img src="images/pageicon.png" alt="AngulMines" style="height: 50px;" />
                    
                    <div class="dropdown" style="background-color: #f25223; color: white; cursor: pointer; font-weight: bold; padding-top: 3px; padding-bottom: 3px; margin-right: 50px;">
  <button class="dropbtn">Login</button>
  <div class="dropdown-content">
    <a href="login.aspx">Login As Employee</a>
    <a href="manager/login.aspx">Login As Manager</a>
    

  </div>
</div>
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
       
       
        
        <script type="text/javascript" src="slider/js/script.js"></script>
        


        <div id="container">
  <div id="content">
<h1>Welcome To Portal</h1>
  </div>
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
