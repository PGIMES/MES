<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="UnFH_Remind.aspx.cs" Inherits="Wuliu_UnFH_Remind" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
<script src="../Content/js/jquery.min.js" type="text/javascript"></script>
    <script src="../Content/js/plugins/layer/layer.min.js" type="text/javascript"></script>
     <script src="../Content/js/plugins/layer/laydate/laydate.js"></script>
    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            $("#mestitle").text("【漏发货提醒】");


        });
       
        
    </script>
    <style>
    .gvHeader th{    background: #C1E2EB;    color:  Brown;    border: solid 1px #333333;    padding:0px 5px 0px 5px;font-size:13px;}
   
</style>
    <div class="row row-container">
        <div class="col-sm-12 ">
            <div class="panel panel-info">
                <div class="panel-heading">
                    <strong>查询</strong>
                    <asp:ScriptManager ID="ScriptManager1" runat="server">
                    </asp:ScriptManager>
                </div>
                <div class="panel-body">
                    <div class="col-sm-12">
                        <table>
                            <tr>
                                <td>
                                    公司别
                                </td>
                                <td>
                                    <asp:DropDownList ID="dropcomp" runat="server" class="form-control input-s-sm ">
                                        <asp:ListItem>100</asp:ListItem>
                                        <asp:ListItem>200</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    日期:
                                </td>
                                <td >
                                   <asp:TextBox ID="txtCreate_dateT" class="form-control" onclick="laydate()" runat="server" Width="100px"></asp:TextBox>
                                </td>
                               
                               <td style=" width:30px"></td>
                               
                                
                                <td align="right">
                                    <asp:Button ID="Button1" runat="server" Text="查询" class="btn btn-large btn-primary"
                                        OnClick="Button1_Click" Width="80px" />
                                   
                                   
                                </td>
                                <td style=" width:20px">  </td>
                                <td align="right">
                                    &nbsp;</td>
                            </tr>
                            </table>
                    </div>
                </div>
            </div>
        </div>
    </div>
   
    <div  id="DIV1" style="margin-left: 4px">
       
                        <asp:GridView ID="GridView1" runat="server"  CssClass="gvHeader"
                             OnPageIndexChanging="GridView1_PageIndexChanging"
                            PageSize="100" Width="800px"   
                            BorderColor="LightBlue" 
                            onrowdatabound="GridView1_RowDataBound">
                             <EmptyDataTemplate>
            查无数据，请选择条件重新查询.</EmptyDataTemplate>
                            <PagerSettings FirstPageText="首页" LastPageText="尾页" Mode="NextPreviousFirstLast" 
                                NextPageText="下页" PreviousPageText="上页"  />
                            <PagerStyle ForeColor="Red" />
                        </asp:GridView>
            
    </div>
</asp:Content>

