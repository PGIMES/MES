<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="EData_Query_Modify.aspx.cs" Inherits="E_Data_EData_Query_Modify" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
<script src="../Content/js/jquery.min.js" type="text/javascript"></script>
    <script src="../Content/js/plugins/layer/layer.min.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            $("#mestitle").text("【机台数据查询】");


        });
       
        
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
                                    <asp:TextBox ID="txt_startdate" runat="server" Width="100" />
                                    <ajaxToolkit:CalendarExtender ID="txt_startdate_CalendarExtender" runat="server"
                                        PopupButtonID="Image2" Format="yyyy/MM/dd" TargetControlID="txt_startdate" />
                                    ~&nbsp;<asp:TextBox ID="txt_enddate" runat="server" Width="100" />
                                    <ajaxToolkit:CalendarExtender ID="txt_enddate_CalendarExtender" runat="server" PopupButtonID="Image2"
                                        Format="yyyy/MM/dd" TargetControlID="txt_enddate" />
                                </td>
                                <td align="right">
                                    <asp:Button ID="Button1" runat="server" Text="查询" class="btn btn-large btn-primary"
                                        OnClick="Button1_Click" Width="100px" />
                                   
                                   
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
        </div>
    </div>
   
    <div  id="DIV1" style="margin-left: 4px">
       
                        <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AllowSorting="True"
                            AutoGenerateColumns="False" OnPageIndexChanging="GridView1_PageIndexChanging"
                            PageSize="100" Width="1300px"   BorderColor="LightBlue">
                            <Columns>
                                <asp:BoundField DataField="日期" HeaderText="日期"  DataFormatString="{0:yyyy/MM/dd}">
                                    <HeaderStyle BackColor="#C1E2EB" />
                                </asp:BoundField>
                                <asp:BoundField DataField="时间" HeaderText="时间"  DataFormatString="{0:HH:mm}">
                                    <HeaderStyle BackColor="#C1E2EB" />
                                </asp:BoundField>
                                <asp:BoundField DataField="射出号码" HeaderText="射出号码" DataFormatString="{0:0}">
                                    <HeaderStyle BackColor="#C1E2EB" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="循环时间(sec)">     
                                    <ItemTemplate>
                                        <asp:Label  ID="Label1" runat="server" Text='<%# Bind("循环时间", "{0:N1}") %>'   ></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle BackColor="#C1E2EB" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="低速速度(m/s)">
                                    
                                    <ItemTemplate>
                                        <asp:Label ID="Label2" runat="server" Text='<%# Bind("低速速度", "{0:N2}") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle BackColor="#C1E2EB" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="高速速度(m/s)">
                                   
                                    <ItemTemplate>
                                        <asp:Label ID="Label3" runat="server" Text='<%# Bind("高速速度", "{0:N2}") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle BackColor="#C1E2EB" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="速度开始(mm)">
                                  
                                    <ItemTemplate>
                                        <asp:Label ID="Label4" runat="server" Text='<%# Bind("速度开始", "{0:N1}") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle BackColor="#C1E2EB" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="高速区间(mm)">
                                 
                                    <ItemTemplate>
                                        <asp:Label ID="Label5" runat="server" Text='<%# Bind("高速区间", "{0:N1}") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle BackColor="#C1E2EB" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="料饼厚度(mm)">
                                   
                                    <ItemTemplate>
                                        <asp:Label ID="Label6" runat="server" Text='<%# Bind("料饼厚度", "{0:N1}") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle BackColor="#C1E2EB" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="铸造压力(MPa)">
                                   
                                    <ItemTemplate>
                                        <asp:Label ID="Label7" runat="server" Text='<%# Bind("铸造压力", "{0:N1}") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle BackColor="#C1E2EB" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="升压时间(ms)">
                                   
                                    <ItemTemplate>
                                        <asp:Label ID="Label8" runat="server" Text='<%# Bind("升压时间", "{0:N0}") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle BackColor="#C1E2EB" />
                                </asp:TemplateField>

                            </Columns>
                            <PagerSettings FirstPageText="首页" LastPageText="尾页" Mode="NextPreviousFirstLast" 
                                NextPageText="下页" PreviousPageText="上页"  />
                            <PagerStyle ForeColor="Red" />
                        </asp:GridView>
            
    </div>
</asp:Content>
