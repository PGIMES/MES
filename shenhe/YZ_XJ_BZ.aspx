<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="YZ_XJ_BZ.aspx.cs" Inherits="shenhe_YZ_XJ_BZ" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
 <script src="../Content/js/jquery.min.js" type="text/javascript"></script>
    <script src="../Content/js/plugins/layer/layer.min.js" type="text/javascript"></script>   
    <div class=" panel panel-info  col-lg-12 ">
        <div class="panel panel-heading">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
                    </asp:ScriptManager>
         <asp:Label ID="lblName" runat="server" Text="请填写关闭说明："></asp:Label>   
        </div>
         <div class="panel-body">
                    <div class="col-sm-6 col-md-6">
                        <table style="width: 50%">
                            <tr>
                               
                                <td>
                                       <asp:TextBox ID="txtRemarks" runat="server"  Width= "400px" Height="50px"
                                           TextMode="MultiLine"></asp:TextBox>
                               </td>
                               <td>
                            <asp:Button ID="btnSave" class="btn btn-primary" Style="height: 35px; width: 100px" OnClick="btnSave_Click"
                                Text="保 存" runat="server" />
                        </td>
                            </tr>
                          
                            </table>
                    </div>

                </div>
                </div>
</asp:Content>

