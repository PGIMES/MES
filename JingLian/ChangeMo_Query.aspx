<%@ Page Title="MES【换模查询】" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="ChangeMo_Query.aspx.cs" Inherits="JingLian_ChangeMo_Query" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <script src="../Content/js/jquery.min.js" type="text/javascript"></script>
    <script src="../Content/js/plugins/layer/layer.min.js" type="text/javascript"></script>
 <script type="text/javascript" language="javascript">
     $(document).ready(function () {

         $("#tst").click(function () {

         });

     });
     // $("div[class='h3']").text($("div[class='h3']").text() + "【转运包清理记录查询】");
     $("#mestitle").text("【换模查询】");
        </script>
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
                                            年度:
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="txt_year" runat="server" class="form-control input-s-sm " >
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            月:
                                        </td>
                                        <td>
                                           <asp:DropDownList ID="txt_month" runat="server" class="form-control input-s-sm ">
                                           </asp:DropDownList>
                                        </td>
                                        <td>
                                            日期:
                                        </td>
                                        <td>
                                           <asp:TextBox ID="txt_startdate" runat="server" Width="100" />
             <ajaxtoolkit:calendarextender ID="txt_startdate_CalendarExtender" 
                 runat="server" PopupButtonID="Image2"  Format="yyyy/MM/dd"
                 TargetControlID="txt_startdate" />
             ~&nbsp;<asp:TextBox ID="txt_enddate" runat="server" 
                 Width="100" />
             <ajaxtoolkit:calendarextender ID="txt_enddate_CalendarExtender" 
                 runat="server" PopupButtonID="Image2"  Format="yyyy/MM/dd"
                 TargetControlID="txt_enddate" />
                                        </td>
                                      <td>模具类型</td>
                                      <td> 
                                      <asp:DropDownList ID="txt_moju_type" runat="server" class="form-control input-s-sm ">
                                          <asp:ListItem></asp:ListItem>
                                          <asp:ListItem>样件压铸模</asp:ListItem>
                                          <asp:ListItem>批产压铸模</asp:ListItem>
                                          <asp:ListItem>试样压铸模</asp:ListItem>
                                          <asp:ListItem>切边模</asp:ListItem>
                                           </asp:DropDownList></td>  
                                    </tr>
                                    <tr><td>设备简称:</td>
                                        <td>
                                            <asp:TextBox ID="txt_sbjc" class="form-control input-s-sm" runat="server"
                                               ></asp:TextBox>
                                        </td>
                                        <td>
                                            零件名称：</td>
                                        <td>
                                            <asp:TextBox ID="txt_ljmc" class="form-control input-s-sm" runat="server"
                                               ></asp:TextBox>
                                        </td>
                                        <td>
                                            原因类别:</td>
                                        <td>
                                           <asp:DropDownList ID="ddl_reason" runat="server" 
                                                class="form-control input-s-sm ">
                                               <asp:ListItem></asp:ListItem>
                                               <asp:ListItem>故障</asp:ListItem>
                                               <asp:ListItem>生产</asp:ListItem>
                                               <asp:ListItem>调试</asp:ListItem>
                                           </asp:DropDownList>
                                        </td>
                                        
                                    </tr>
                                     
                                    <tr>
                                        
                                        <td colspan=2 align="right">
                                            <asp:Button ID="Button1" runat="server" Text="查询"  
                                              class="btn btn-large btn-primary" 
                                              onclick="Button1_Click" Width="100px" />
                                        </td>
                                        <td colspan="2">
                                            &nbsp;
                                            <asp:Button ID="Button2" runat="server" Text="返回"  
                                              class="btn btn-large btn-primary" 
                                               Width="100px" onclick="Button2_Click" />
                                        </td>
                                    </tr>
                                   
                                     
                                </table>
                                </div>
                     
                    </div>
                </div>
            </div>
        </div>
        <br />
           <div  runat="server" id="DIV1" style=" margin:1px"  >
        
                       
                                <asp:Panel ID="Panel2" runat="server" Height="100%" >
                                    <table style=" background-color: #FFFFFF;" 
                                      >
                                      
                                        <tr>
                                            <td valign="top">
                                                <asp:GridView ID="GridView1" runat="server" 
                                                    AllowPaging="True" AllowSorting="True" 
                                                    AutoGenerateColumns="False" 
                                                    onpageindexchanging="GridView1_PageIndexChanging" 
                                                    onrowdatabound="GridView1_RowDataBound" 
                                                    onsorting="GridView1_Sorting" PageSize="100" 
                                                     Width="100%" onrowcreated="GridView1_RowCreated" 
                                                    ShowFooter="True" >
                                                    <Columns>
                                                        <asp:BoundField HeaderText="序号">
                                                        <HeaderStyle BackColor="#C1E2EB" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="change_startdate" 
                                                            DataFormatString="{0:yyyy-MM-dd}" HeaderText="日期">
                                                        <HeaderStyle BackColor="#C1E2EB" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="emp_banbie" HeaderText="班别">
                                                        <HeaderStyle BackColor="#C1E2EB" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="change_startdate" 
                                                            HeaderText="开始时间">
                                                        <HeaderStyle BackColor="#C1E2EB" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="change_enddate" 
                                                            HeaderText="结束时间" HtmlEncode="False">
                                                        <HeaderStyle BackColor="#C1E2EB" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="equip_name" 
                                                            HeaderText="设备简称">
                                                        <HeaderStyle BackColor="#C1E2EB" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="change_type" HeaderText="换模类型">
                                                        <HeaderStyle BackColor="#C1E2EB" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="timer" HeaderText="换模用时">
                                                        <HeaderStyle BackColor="#C1E2EB" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="part_up" HeaderText="上模零件号">
                                                        <HeaderStyle BackColor="#C1E2EB" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="mo_no_up" HeaderText="上模模号" 
                                                            HtmlEncode="False">
                                                        <HeaderStyle BackColor="#C1E2EB" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="moju_type_up" 
                                                            HeaderText="上模模具类型">
                                                        <HeaderStyle BackColor="#C1E2EB" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="part_down" HeaderText="下模零件号">
                                                        <HeaderStyle BackColor="#C1E2EB" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="mo_no_down" HeaderText="下模模号">
                                                        <HeaderStyle BackColor="#C1E2EB" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="moju_type_down" 
                                                            HeaderText="下模模具类型">
                                                        <HeaderStyle BackColor="#C1E2EB" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="moju_kw_up" HeaderText="上模库位">
                                                        <HeaderStyle BackColor="#C1E2EB" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="moju_kw_down" HeaderText="下模库位">
                                                        <HeaderStyle BackColor="#C1E2EB" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="jg" HeaderText="模具生产天数">
                                                        <HeaderStyle BackColor="#C1E2EB" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="moju_status_down" 
                                                            HeaderText="模具状态">
                                                        <HeaderStyle BackColor="#C1E2EB" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="moju_status_demo_down" 
                                                            HeaderText="模具状态说明">
                                                        <HeaderStyle BackColor="#C1E2EB" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="chang_reason" HeaderText="换模原因">
                                                        <HeaderStyle BackColor="#C1E2EB" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="reason_type" HeaderText="原因类别">
                                                        <HeaderStyle BackColor="#C1E2EB" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="emp_name" HeaderText="换模人">
                                                        <HeaderStyle BackColor="#C1E2EB" />
                                                        </asp:BoundField>
                                                    </Columns>
                                                    <PagerSettings FirstPageText="首页" LastPageText="尾页" 
                                                        Mode="NextPreviousFirstLast" NextPageText="下页" 
                                                        PreviousPageText="上页" />
                                                    <PagerStyle ForeColor="Red" />
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                                </div>
</asp:Content>

