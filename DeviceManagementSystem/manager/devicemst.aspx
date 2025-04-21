<%@ Page Title="" Language="C#" MasterPageFile="~/manager/admin.Master" AutoEventWireup="true" CodeBehind="devicemst.aspx.cs" Inherits="DeviceManagementSystem.manager.devicemst" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <style>
        td, th {
    padding: 10px;
    font-size: 20px;
}

        th {
    background-color: #f25223 !important;
}
          .table-agile-info {
      background: #f0f0f0;
  }
        </style>
    <script type="text/javascript">
        function confirmation() {
            if (confirm('Are you Sure to Perform this Action???')) {
                return true;
            } else {
                return false;
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="table-agile-info">
        <div class="input-sm" style="font-size: 13px; margin-top: -25px;">
            <a href="home.aspx">
                <i class="fa fa-home"></i>
            </a>/ Master Setup / Device List
        </div>
                           <div id="myModal"  class="modal" >
                         <div class="modal-dialog">
             
           <!-- Modal content -->
           <div class="modal-content">
                                       <div class="modal-header">
  <h5 class="modal-title" id="exampleModalLabel">Charger Detail<span class="close">&times;</span></h5>
  
</div> <div class="modal-body">
            <div class="row w3-res-tb">
                <div class="col-sm-3">
                </div>
                <div class="col-sm-12">
                    <div class="col-sm-4">Name:</div>
                    <div class="col-sm-8">
                        <asp:TextBox ID="txt_name" runat="server" class="input-sm form-control" placeHolder="Enter Name" AutoPostBack="false"></asp:TextBox>
                        <asp:Label ID="Label1" runat="server" Visible="false"></asp:Label>
                        <asp:Label ID="lbl_AID" runat="server" Visible="false"></asp:Label>
                    </div>
                    <div class="col-sm-12" style="height: 10px;"></div>
                           
        <div class="col-sm-4">Location:</div>
<div class="col-sm-8">
    <asp:TextBox ID="txt_location" runat="server" class="input-sm form-control" placeHolder="Enter Name" AutoPostBack="false"></asp:TextBox>
    <!--<asp:DropDownList ID="ddl_location" runat="server" AutoPostBack="false" class="input-sm form-control w-sm inline v-middle" Width="100%">                            
        <asp:ListItem Value="loction1">Loction 1</asp:ListItem>
        <asp:ListItem Value="loction2">Loction 2</asp:ListItem>
        
    </asp:DropDownList>-->
</div>
                    <div class="col-sm-12" style="height: 10px;"></div>
                          <div class="col-sm-12" style="height: 10px;"></div>
                    <div class="col-sm-4">Active:</div>
<div class="col-sm-8">
    <asp:CheckBox ID="active" runat="server" AutoPostBack="false"  />

</div>
<div class="col-sm-12" style="height: 10px;"></div>
                    <div class="col-sm-12" style="text-align: right;">
                        <asp:Button ID="btn_Submit" runat="server" class="btn btn-sm btn-success" Text="Submit" OnClick="btn_Submit_Click" />
                        <asp:Button ID="btn_Cancel" runat="server" class="btn btn-sm btn-danger" Text="Cancel" OnClick="btn_Cancel_Click" />

                    </div>
                </div>
                <div class="col-sm-3">
                </div>
            </div>
              </div>
      
          </div>
       </div>  
                   </div>  
    

                 <h3><b>Device List</b>                <button type="button" ID="btnaddnew" class="btn btn-large btn-success pull-right" >
Add New
</button></h3>
                 
<br />
            <div class="row w3-res-tb">
               
                
                <div class="col-sm-6">
                    <div class="input-group">
                        <asp:TextBox ID="txt_Search" runat="server" class="input-sm form-control" placeholder="Search"></asp:TextBox>
                        <span class="input-group-btn">
                            <asp:Button ID="btn_Search" runat="server" class="btn btn-sm btn-default" Text="Search" OnClick="btn_Search_Click" />
                        </span>
                    </div>
                </div>
                <div class="col-sm-6">
</div>
            </div>
            <div class="table-responsive">
                
                <asp:GridView ID="gv_Show" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" Width="100%" 
                   DataKeyNames="ID" OnRowDataBound="gv_Show_RowDataBound" OnRowCommand="gv_Show_RowCommand">
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
                
                        <asp:TemplateField>
                            <HeaderTemplate>#</HeaderTemplate>
                            <ItemTemplate>
                                <%# Eval("Id")%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderTemplate>Name</HeaderTemplate>
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
                            <HeaderTemplate>Active</HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lbl_Grid_Status" runat="server" Text='<%# Eval("isactive")%>' ></asp:Label>
                                <asp:Image ID="Image1" runat="server" Height="15px" ImageUrl="~/manager/images/tick.png" />
                                <asp:Image ID="Image2" runat="server" Height="15px" ImageUrl="~/manager/images/wrong.png" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField Visible="false">
                            <HeaderTemplate>Edit</HeaderTemplate>
                            <ItemTemplate>
                            
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderTemplate>Action</HeaderTemplate>
                            <ItemTemplate>
                                <asp:Button ID="imgb_Grid_Edit" runat="server" CommandName="EditRow" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" Class="btn btn-info"  Text="Edit" />
                                <asp:Button ID="imgb_Grid_Delete" runat="server" CommandName="DeleteRow" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" Class="btn btn-danger" Text="Delete" OnClientClick="return confirmation();" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
    <script>
        // Get the modal
        var modal = document.getElementById("myModal");

        // Get the button that opens the modal
        var btn = document.getElementById("btnaddnew");

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
</asp:Content>
