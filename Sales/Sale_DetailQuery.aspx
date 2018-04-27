<%@ Page Title="【产品销售查询】" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Sale_DetailQuery.aspx.cs" Inherits="Sale_Sale_DetailQuery" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <style>
    .gvHeader th{    background: #C1E2EB;    color:  Brown;    border: solid 1px #333333;    padding:0px 5px 0px 5px; font-size:12px;}
   
</style>
    <script src="../Content/js/jquery.min.js" type="text/javascript"></script>
    <script src="../Content/js/plugins/layer/layer.min.js" type="text/javascript"></script>
 <script type="text/javascript" language="javascript">

     $("#mestitle").text("【产品销售查询】");
        </script>
    <div class="row row-container">
        <div class="col-sm-12 ">
            <div class="panel panel-info">
                <div class="panel-heading">
                    <strong>查询</strong>
                    <asp:ScriptManager ID="ScriptManager1" runat="server">
                    </asp:ScriptManager>
                </div>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                <div class="panel-body">
                    <div class="col-sm-12">
                       
                                <table>
                                    
                                    <tr>
                                        <td>
                                            年份:
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="txt_year" runat="server" class="form-control input-s-sm " >
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            统计项：</td>
                                        <td>
                                            <asp:DropDownList ID="ddl_item" runat="server" 
                                                class="form-control input-s-sm "
                                                onselectedindexchanged="ddl_item_SelectedIndexChanged" 
                                                AutoPostBack="True">
                                                <asp:ListItem Value="0">金额</asp:ListItem>
                                                <asp:ListItem Value="1">数量</asp:ListItem>
                                                <asp:ListItem Value="2">单价</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                   

                                     <td>生产公司</td>
                                      <td> 
                                      <asp:DropDownList ID="ddl_comp" runat="server" class="form-control input-s-sm ">
                                          <asp:ListItem Value=" ">ALL</asp:ListItem>
                                          <asp:ListItem Value="100">上海帕捷</asp:ListItem>
                                          <asp:ListItem Value="200">昆山帕捷</asp:ListItem>
                                           </asp:DropDownList></td>  

                                            <td>
                                                <asp:Label ID="lb_type" runat="server" Text="类别"></asp:Label>
                                        </td>
                                      <td> 
                                      <asp:DropDownList ID="ddl_type" runat="server" class="form-control input-s-sm ">
                                          <asp:ListItem Value="3">ALL</asp:ListItem>
                                          <asp:ListItem Value="0">产品收入</asp:ListItem>
                                          <asp:ListItem Value="1">模具收入</asp:ListItem>
                                           </asp:DropDownList></td>  
                                    <td>零件号：</td>
                                        <td> <asp:TextBox ID="txt_ljh"  runat="server"  class="form-control input-s-sm "/>
                                          <td>客户：</td>
                                        <td> 
                                            <asp:DropDownList ID="ddl_kh" runat="server" 
                                                class="form-control input-s-sm ">
                                            </asp:DropDownList>
                                         <td>产品类别：</td>
                                    <td> 
                                      <asp:DropDownList ID="ddl_productid" runat="server" class="form-control input-s-sm ">
                                           </asp:DropDownList></td> 
                                    </tr>
                                     
                              
                                     
                                    <tr>
                                        
                                        <td colspan=2 align="right">
                                            <asp:Button ID="Button1" runat="server" Text="查询"  
                                              class="btn btn-large btn-primary" 
                                              onclick="Button1_Click" Width="100px" />
                                        </td>
                                        <td colspan="2">
                                            &nbsp;
                                            <a class="btn btn-large btn-primary" 
                                                href="../index.aspx" style="color: white">返回</a></td>
                                    </tr>
                                   
                                     
                                </table>
                                </div>
                     
                    </div>
                    </ContentTemplate>
                  <Triggers>
                                                                   <asp:PostBackTrigger ControlID="Button1" />
                                                                 
                                                               </Triggers>
                </asp:UpdatePanel>
                </div>
            </div>
        </div>
        <br />
           <div  runat="server" id="DIV1"  style=" margin-left:5px" >
        
                       
                                <asp:Panel ID="Panel2" runat="server" Height="100%" >
                                    <table style=" background-color: #FFFFFF;" 
                                      >
                                      
                                        <tr>
                                            <td valign="top">
                                                <asp:GridView ID="GridView1" runat="server"  
                                                    CssClass="gvHeader" 
                                                  
                                                     Width="100%" 
                                                    ShowFooter="True" 
                                                    PageSize="100" 
                                                    onsorting="GridView1_Sorting" 
                                                    onrowdatabound="GridView1_RowDataBound" 
                                                    onrowcreated="GridView1_RowCreated" 
                                                    onpageindexchanging="GridView1_PageIndexChanging" >
                                                    <PagerSettings FirstPageText="首页" LastPageText="尾页" 
                                                        Mode="NextPreviousFirstLast" NextPageText="下页" 
                                                        PreviousPageText="上页" />
                                                    <PagerStyle ForeColor="Red" />
                                                    <EmptyDataTemplate>no data found</EmptyDataTemplate>
                                                </asp:GridView>
                                                <br />
                                                <br />
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                                </div>
</asp:Content>



