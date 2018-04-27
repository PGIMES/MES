<%@ Page Title="MES【员工交接班查询】" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="HandOverQuery.aspx.cs" Inherits="HandOverQuery" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <link href="Content/css/plugins/datapicker/datepicker3.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        td
        {
            padding-left: 5px;
            padding-right: 5px;
            padding-top: 5px;
        }
    </style>
    <script type="text/javascript">
        $(document).ready(function () {

            $("select[id*='selHeJin']").change(function () {


            })

            $("#mestitle").text("【员工交接班查询】");
            //时间选择
            $(function () {
                $(".datepicker").datepicker({
                    autoclose: true,
                    todayBtn: true,
                    todayHighlight: true,
                    initialDate: new Date()
                });
            });

        })//endready

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="panel panel-info" style="padding-left: 5px; padding-right: 5px">
        <table>
            <tr>
                <td>
                    年份
                </td>
                <td>
                    <asp:DropDownList ID="dropYear" runat="server" class="form-control input-s-sm">
                    </asp:DropDownList>
                </td>
                <td>
                    月份
                </td>
                <td>
                    <asp:DropDownList ID="dropMonth" runat="server" class="form-control input-s-sm">
                    </asp:DropDownList>
                </td>
                <td>
                    日期
                </td>
                <td ><asp:TextBox ID="txtDateFrom" runat="server" class="form-control input-s-sm datepicker" style="display:inline" Width="90px"></asp:TextBox><ajaxtoolkit:calendarextender ID="txt_startdate_CalendarExtender" 
                 runat="server" PopupButtonID="Image2"  Format="yyyy/MM/dd"
                 TargetControlID="txtDateFrom" />
                    <asp:TextBox ID="txtDateTo" runat="server" class="form-control input-s-sm datepicker" style="display:inline" Width="90px"></asp:TextBox><ajaxtoolkit:calendarextender ID="Calendarextender1" 
                 runat="server" PopupButtonID="Image2"  Format="yyyy/MM/dd"
                 TargetControlID="txtDateTo"  />
                    </td>
                <td>班别
                </td>
                <td>
                    <asp:DropDownList ID="dropBanBie" runat="server" class="form-control input-s-sm">
                        <asp:ListItem Value=""></asp:ListItem>
                        <asp:ListItem Value="白班"></asp:ListItem>
                        <asp:ListItem Value="晚班"></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td rowspan="2">
                    <asp:Button ID="btnQuery" runat="server" Text="查询" class="btn btn-primary btn-lg"
                        OnClick="btnQuery_Click" Height="50px" Width="100px" />
                    <a href="Default.aspx" class="btn btn-primary btn-lg" 
                        style="color: white; height: 50px; width: 100px; vertical-align:middle">返回</a>
                </td>
            </tr>
            <tr>
                <td>
                    车间
                </td>
                <td>
                    <asp:DropDownList ID="dropCheJian" runat="server" class="form-control input-s-sm">
                        <asp:ListItem Value="0">-请选择-</asp:ListItem>
                        <asp:ListItem>压铸区</asp:ListItem>
                        <asp:ListItem>铁机加区</asp:ListItem>
                        <asp:ListItem>铝机加区</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    区域
                </td>
                <td>
                    <asp:DropDownList ID="dropGangWei" runat="server" 
                        class="form-control input-s-sm" placeholder="-选择-" AutoPostBack="True" 
                        onselectedindexchanged="dropGangWei_SelectedIndexChanged"></asp:DropDownList>
                </td>
                <td>
                    设备</td>
                <td>
                    <asp:DropDownList ID="dropShebei" runat="server" 
                        class="form-control input-s-sm">
                    </asp:DropDownList></td>
                <td>员工
                </td>
                <td>
                    <asp:TextBox ID="txtEmpName" runat="server" class="form-control input-s-sm" placeholder="工号或姓名"></asp:TextBox>
                </td>
            </tr>
        </table>
    </div>
    <hr />
    <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AllowSorting="True"
        AutoGenerateColumns="False" 
        OnPageIndexChanging="GridView1_PageIndexChanging" PageSize="200"
        Width="100%" BorderColor="#CCCCCC">
        <EmptyDataTemplate>
            查无数据，请选择条件重新查询.</EmptyDataTemplate>
        <Columns>
            <asp:BoundField DataField="日期" DataFormatString="{0:yyyy-MM-dd}" HeaderText="日期"
                ItemStyle-Width="100px">
                <HeaderStyle BackColor="#C1E2EB" />
            </asp:BoundField>
            <asp:BoundField DataField="班别" HeaderText="班别"  ItemStyle-Width="40px">
                <HeaderStyle BackColor="#C1E2EB" />
            </asp:BoundField>
            <asp:BoundField DataField="工号" HeaderText="工号" HtmlEncode="False"  ItemStyle-Width="40px">
                <HeaderStyle BackColor="#C1E2EB" />
            </asp:BoundField>
            <asp:BoundField DataField="姓名" HeaderText="姓名"  ItemStyle-Width="60px">
                <HeaderStyle BackColor="#C1E2EB" />
            </asp:BoundField>
            <asp:BoundField DataField="班组" HeaderText="班组"  ItemStyle-Width="40px">
                <HeaderStyle BackColor="#C1E2EB" />
            </asp:BoundField>
             <asp:BoundField DataField="车间" HeaderText="车间" >
                <HeaderStyle BackColor="#C1E2EB" />
            </asp:BoundField>
            <asp:BoundField DataField="区域" HeaderText="区域" >
                <HeaderStyle BackColor="#C1E2EB" />
            </asp:BoundField>
            <asp:BoundField DataField="岗位" HeaderText="岗位" >
                <HeaderStyle BackColor="#C1E2EB" />
            </asp:BoundField>
            <asp:BoundField DataField="设备" HeaderText="设备" >
                <HeaderStyle BackColor="#C1E2EB" />
            </asp:BoundField>
            <asp:BoundField DataField="上班交接状态" HeaderText="上班交接状态" >
                <HeaderStyle BackColor="#C1E2EB" />
            </asp:BoundField>
            <asp:BoundField DataField="登入留言" HeaderText="登入留言">
                <HeaderStyle BackColor="#C1E2EB" />
            </asp:BoundField>
            <asp:BoundField DataField="登入时间" HeaderText="登入时间" ItemStyle-Width="160px">
                <HeaderStyle BackColor="#C1E2EB" />
            </asp:BoundField>
            <asp:BoundField DataField="下班交接状态" HeaderText="下班交接状态" >
                <HeaderStyle BackColor="#C1E2EB" />
            </asp:BoundField>
            <asp:BoundField DataField="登出留言" HeaderText="登出留言">
                <HeaderStyle BackColor="#C1E2EB" />
            </asp:BoundField>
            <asp:BoundField DataField="登出时间" HeaderText="登出时间" ItemStyle-Width="160px">
                <HeaderStyle BackColor="#C1E2EB"  />
            </asp:BoundField>
            <asp:BoundField DataField="时长" HeaderText="时长(H)">
                <HeaderStyle BackColor="#C1E2EB" />
            </asp:BoundField>
        </Columns>
        <PagerSettings FirstPageText="首页" LastPageText="尾页" Mode="NextPreviousFirstLast"
            NextPageText="下页" PreviousPageText="上页" />
        <PagerStyle ForeColor="Red" />
    </asp:GridView>
    </asp:Content>
