<%@ Page Title="MES【转运包清理记录查询】" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="ZYB_Query.aspx.cs" Inherits="JingLian_ZYB_Query" %>

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
     $("#mestitle").text("【转运包清理记录查询】");
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
                                            <asp:DropDownList ID="txt_year" runat="server" class="form-control input-s-sm ">
                                            </asp:DropDownList>
                                        </td>
                                       <td>月</td>
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
                                            班别:
                                        </td>
                                        <td>

                                          <asp:DropDownList ID="txt_banbie" runat="server"  class="form-control input-s-sm ">
                                             <asp:ListItem></asp:ListItem>
                                             <asp:ListItem>白班</asp:ListItem>
                                             <asp:ListItem>晚班</asp:ListItem>
                                         </asp:DropDownList>
                                           
                                        </td>
                                    </tr>
                                     
                                    <tr>
                                        
                                        <td colspan=5 align="right">
                                            <asp:Button ID="Button1" runat="server" Text="查询"  
                                              class="btn btn-large btn-primary" 
                                              onclick="Button1_Click" Width="100px" />
                                        </td>
                                        <td>
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
         <div  runat="server" id="DIV1" style=" margin:20px"  >
        
                       
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
                                                    Width="1300px" >
                                                    <Columns>
                                                        <asp:BoundField DataField="Checkdate" 
                                                            DataFormatString="{0:yyyy-MM-dd}" HeaderText="日期" 
                                                            SortExpression="Checkdate">
                                                        <HeaderStyle BackColor="#C1E2EB" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="emp_banbie" 
                                                            HeaderText="班别">
                                                        <HeaderStyle BackColor="#C1E2EB" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="checkdate" HeaderText="清理时间" 
                                                            DataFormatString="{0:HH:mm:ss}" 
                                                            SortExpression="checkdate">
                                                        <HeaderStyle BackColor="#C1E2EB" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="equip_no" 
                                                            HeaderText="转运包" HtmlEncode="False">
                                                        <HeaderStyle BackColor="#C1E2EB" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="check1" 
                                                            HeaderText="包内壁清理">
                                                        <HeaderStyle BackColor="#C1E2EB" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="check1demo" HeaderText="说明项">
                                                        <HeaderStyle BackColor="#C1E2EB" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="check2" 
                                                            HeaderText="包底部清理">
                                                        <HeaderStyle BackColor="#C1E2EB" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="check2demo" HeaderText="说明项">
                                                        <HeaderStyle BackColor="#C1E2EB" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="check3" 
                                                            HeaderText="转运包检查">
                                                        <HeaderStyle BackColor="#C1E2EB" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="check3demo" HeaderText="说明项">
                                                        <HeaderStyle BackColor="#C1E2EB" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="check4" 
                                                            HeaderText="转运包被履" HtmlEncode="False">
                                                        <HeaderStyle BackColor="#C1E2EB" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="check4demo" HeaderText="说明项">
                                                        <HeaderStyle BackColor="#C1E2EB" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="weight" 
                                                            HeaderText="称重" HtmlEncode="False">
                                                        <HeaderStyle BackColor="#C1E2EB" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="emp_name" HeaderText="员工">
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

