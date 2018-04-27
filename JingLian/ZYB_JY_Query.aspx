<%@ Page Title="MES【转运包加液记录查询】" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="ZYB_JY_Query.aspx.cs" Inherits="JingLian_ZYB_JY_Query" %>

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
     $("#mestitle").text("【转运包加液记录查询】");
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
                                            <asp:DropDownList ID="txt_year" runat="server" class="form-control input-s-sm " Width="100px">
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
                                         <td>
                                             操作人:
                                        </td>
                                        <td>

                                          <asp:DropDownList ID="txt_czr" runat="server"  class="form-control input-s-sm ">
                                         </asp:DropDownList>
                                           
                                        </td>
                                    </tr>
                                    <tr><td>转运包号:</td>
                                        <td>
                                            <asp:DropDownList ID="ddl_zyb" runat="server" class="form-control input-s-sm ">
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            班别：</td>
                                        <td>
                                            <asp:DropDownList ID="ddl_hejin" runat="server" class="form-control input-s-sm ">
                                                <asp:ListItem></asp:ListItem>
                                                <asp:ListItem>白班</asp:ListItem>
                                                <asp:ListItem>晚班</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            转运包序列号:</td>
                                        <td>
                                            <asp:TextBox ID="txt_zyb" class="form-control input-s-sm" runat="server"
                                               ></asp:TextBox>
                                        </td>
                                        <td>保温炉号:</td>
                                        <td>
                                            <asp:DropDownList ID="ddl_luhao" runat="server" class="form-control input-s-sm ">
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
        <div id=DIV2 runat=server><span style="color: #FF0000; font-weight: bold; font-size: large;">
            *两次加液间隔大于2小时显示黃色</span><br /><span style="color: #FF0000; font-weight: bold; font-size: large;">
            *两次加液间隔大于4小时显示红色</span>
            </div>
         <div  runat="server" id="DIV1" style=" margin:10px"  >
        
                       
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
                                                     Width="1148px" >
                                                    <Columns>
                                                        <asp:BoundField DataField="inputdate" 
                                                            DataFormatString="{0:yyyy-MM-dd}" HeaderText="日期" 
                                                            SortExpression="Checkdate">
                                                        <HeaderStyle BackColor="#C1E2EB" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="emp_banbie" HeaderText="班别">
                                                        <HeaderStyle BackColor="#C1E2EB" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="MaterialNo" 
                                                            HeaderText="转运包序列号" SortExpression="MaterialNo">
                                                        <HeaderStyle BackColor="#C1E2EB" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="zybh" 
                                                            HeaderText="转运包" HtmlEncode="False">
                                                        <HeaderStyle BackColor="#C1E2EB" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="hejing" 
                                                            HeaderText="合金">
                                                        <HeaderStyle BackColor="#C1E2EB" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="HD_time" HeaderText="精炼完成时间">
                                                        <HeaderStyle BackColor="#C1E2EB" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="inputdate" 
                                                            HeaderText="加液时间" 
                                                            DataFormatString="{0: yyyy-MM-dd HH:mm:ss}" 
                                                            SortExpression="MaterialNo">
                                                        <HeaderStyle BackColor="#C1E2EB" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="timer" 
                                                            HeaderText="时长&lt;br&gt;(Min)" HtmlEncode="False">
                                                        <HeaderStyle BackColor="#C1E2EB" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="jg" HeaderText="两次加液间隔(H)">
                                                        <HeaderStyle BackColor="#C1E2EB" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="equip_no" 
                                                            HeaderText="加至保温炉">
                                                        <HeaderStyle BackColor="#C1E2EB" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="emp_name" HeaderText="操作人">
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

