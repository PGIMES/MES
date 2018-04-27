<%@ Page Title="MES【模具报修查询】" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="MojuBX_Query.aspx.cs" Inherits="MoJu_MojuBX_Query" %>

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
     $("#mestitle").text("【模具报修查询】");
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
                                            月份:
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
                                      <td>报修人：</td>
                                      <td> 
                                            <asp:TextBox ID="txt_bxr" 
                                              class="form-control input-s-sm" runat="server"
                                               ></asp:TextBox>
                                        </td>  
                                        <td>维修人：</td>
                                      <td> 
                                            <asp:TextBox ID="txt_wxr" 
                                              class="form-control input-s-sm" runat="server"
                                               ></asp:TextBox>
                                        </td>  
                                    </tr>
                                    <tr><td>模具号:</td>
                                        <td>
                                            <asp:TextBox ID="txt_mojuno" class="form-control input-s-sm" runat="server"
                                               ></asp:TextBox>
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
                                        <td>
                                            零件号：</td>
                                        <td>
                                            <asp:TextBox ID="txt_ljmc" class="form-control input-s-sm" runat="server"
                                               ></asp:TextBox>
                                        </td>
                                        <td>设备简称:</td>
                                        <td>
                                            <asp:TextBox ID="txt_sbjc" class="form-control input-s-sm" runat="server"
                                               ></asp:TextBox>
                                        </td>
                                        <td>
                                            维修结果:</td>
                                        <td>
                                           <asp:DropDownList ID="txt_result" runat="server" 
                                                class="form-control input-s-sm ">
                                               <asp:ListItem></asp:ListItem>
                                               <asp:ListItem>恢复正常</asp:ListItem>
                                               <asp:ListItem>暂时恢复正常</asp:ListItem>
                                               <asp:ListItem>需下模维修</asp:ListItem>
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
        
                       
                                <asp:Panel ID="Panel2" runat="server" Height="100%"  Width="100%">
                                    <table style=" background-color: #FFFFFF;" 
                                      >
                                      
                                        <tr>
                                            <td valign="top">
                                                <asp:GridView ID="GridView1" runat="server" 
                                                    AllowPaging="True" AllowSorting="True" 
                                                    AutoGenerateColumns="False" 
                                                    onpageindexchanging="GridView1_PageIndexChanging" PageSize="100" 
                                                     Width="1500px" 
                                                    onrowdatabound="GridView1_RowDataBound" >
                                                    <Columns>
                                                        <asp:BoundField HeaderText="序号">
                                                        <HeaderStyle BackColor="#C1E2EB" />
                                                        </asp:BoundField>
                                                        <asp:TemplateField HeaderText="报修单号">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="LinkButton1" runat="server" 
                                                                    Text='<%#Eval("bx_dh") %>' onclick="LinkButton1_Click">LinkButton</asp:LinkButton>
                                                            </ItemTemplate>
                                                            <HeaderStyle BackColor="#C1E2EB" />
                                                            <ItemStyle Width="160px" />
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="bx_date" HeaderText="报修时间">
                                                        <HeaderStyle BackColor="#C1E2EB" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="wx_end_date" HeaderText="维修完成时间">
                                                        <HeaderStyle BackColor="#C1E2EB" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="timer" HeaderText="维修时长" 
                                                            HtmlEncode="False">
                                                        <HeaderStyle BackColor="#C1E2EB" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="qr_date" HeaderText="确认时间">
                                                        <HeaderStyle BackColor="#C1E2EB" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="bx_moju_no" HeaderText="模具号">
                                                        <HeaderStyle BackColor="#C1E2EB" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="bx_part" 
                                                            HeaderText="零件号">
                                                        <HeaderStyle BackColor="#C1E2EB" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="bx_mo_no" 
                                                            HeaderText="模号" HtmlEncode="False">
                                                        <HeaderStyle BackColor="#C1E2EB" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="bx_sbname" 
                                                            HeaderText="设备简称">
                                                        <HeaderStyle BackColor="#C1E2EB" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="bx_moju_type" HeaderText="模具类型">
                                                        <HeaderStyle BackColor="#C1E2EB" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="bx_gz_type" HeaderText="故障类型">
                                                        <HeaderStyle BackColor="#C1E2EB" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="bx_gz_desc" HeaderText="故障描述">
                                                        <HeaderStyle BackColor="#C1E2EB" />
                                                        <ItemStyle Width="150px" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="wx_result" HeaderText="维修结果">
                                                        <HeaderStyle BackColor="#C1E2EB" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="bx_name" HeaderText="报修人">
                                                        <HeaderStyle BackColor="#C1E2EB" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="wx_name" 
                                                            HeaderText="维修人">
                                                        <HeaderStyle BackColor="#C1E2EB" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="qr_name" 
                                                            HeaderText="确认人">
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

