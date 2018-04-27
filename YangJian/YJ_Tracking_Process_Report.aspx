<%@ Page Title="样件系统【追踪进度报表】" Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.master" CodeFile="YJ_Tracking_Process_Report.aspx.cs" Inherits="YangJian_YJ_Tracking_Process_Report" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <style type="text/css">
        td {
            padding-left: 5px;
            padding-right: 5px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <script src="../Content/js/jquery.min.js" type="text/javascript"></script>
    <script src="../Content/js/plugins/layer/layer.min.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            $("#tst").click(function () {
            });
        });
        $("#mestitle").text("【样件任务查询】");
    </script>
    <script src="../Content/js/plugins/layer/layer.min.js"></script>
    <script src="../Content/js/plugins/layer/laydate/laydate.js"></script>
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
                                <td>年度:
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlyear" runat="server" class="form-control input-s-sm " Width="130px">
                                        <asp:ListItem Value="2017">2017</asp:ListItem>
                                    </asp:DropDownList>
                                <td>要货时间:
                                </td>
                                <td>
                                    <asp:TextBox ID="txt_startdate" runat="server" Width="100" />
                                    <ajaxToolkit:CalendarExtender ID="txt_startdate_CalendarExtender"
                                        runat="server" PopupButtonID="Image2" Format="yyyy/MM/dd"
                                        TargetControlID="txt_startdate" />
                                    ~&nbsp;<asp:TextBox ID="txt_enddate" runat="server"
                                        Width="100" />
                                    <ajaxToolkit:CalendarExtender ID="txt_enddate_CalendarExtender"
                                        runat="server" PopupButtonID="Image2" Format="yyyy/MM/dd"
                                        TargetControlID="txt_enddate" />
                                </td>
                                <td>当前阶段:
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlStauts" runat="server" class="form-control input-s-sm " Width="130px">
                                        <asp:ListItem Value="">All</asp:ListItem>
                                        <asp:ListItem Value="0">尚未确认</asp:ListItem>
                                        <asp:ListItem Value="1">备货尚未完成</asp:ListItem>
                                        <asp:ListItem Value="2">检验尚未完成</asp:ListItem>
                                        <asp:ListItem Value="3">未发货</asp:ListItem>
                                        <asp:ListItem Value="4">未到货</asp:ListItem>
                                        <asp:ListItem Value="5">已到货</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td>完成状态:
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlsign_status" runat="server" class="form-control input-s-sm " Width="130px">
                                        <asp:ListItem Value="0">未完成</asp:ListItem>
                                        <asp:ListItem Value="1">已完成</asp:ListItem>
                                        <asp:ListItem Value="-1">All</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td>任务要求完成时间:
                                </td>
                                <td class="auto-style1">
                                    <input id="txt_date" class="form-control input-s-sm" style="height: 35px; width: 130px" runat="server" 
                                        onclick="laydate({choose: function (dates) { setValue();}})"
                                        autopostback="True" ontextchanged="txt_date_TextChanged" onchange="txt_date_TextChanged" />
                                    <asp:RequiredFieldValidator ID="yz19" runat="server" ControlToValidate="txt_date" ValidationGroup="request" ForeColor="Red"></asp:RequiredFieldValidator>
                                </td>
                                 <td>操作事项:
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddl_czsx" runat="server" class="form-control input-s-sm " Width="160px">
                                        <asp:ListItem Value="-1">All</asp:ListItem>
                                        <asp:ListItem Value="0">产品状态确认</asp:ListItem>
                                        <asp:ListItem Value="1">包装确认</asp:ListItem>
                                         <asp:ListItem Value="2">备货日期确认</asp:ListItem>
                                        <asp:ListItem Value="3">检验日期确认</asp:ListItem>
                                        <asp:ListItem Value="4">质量检验结果确认</asp:ListItem>
                                         <asp:ListItem Value="5">销售发货时间确认</asp:ListItem>
                                        <asp:ListItem Value="6">生成QAD订单</asp:ListItem>
                                        <asp:ListItem Value="7">订舱申请</asp:ListItem>
                                         <asp:ListItem Value="8">订舱处理</asp:ListItem>
                                        <asp:ListItem Value="9">包装备货完成</asp:ListItem>
                                        <asp:ListItem Value="10">产品备货完成</asp:ListItem>
                                         <asp:ListItem Value="11">仓库送检</asp:ListItem>
                                        <asp:ListItem Value="12">检验组检验</asp:ListItem>
                                        <asp:ListItem Value="13">质量上传检验报告</asp:ListItem>
                                         <asp:ListItem Value="14">仓库检验后取回</asp:ListItem>
                                        <asp:ListItem Value="15">质量确认检验要求</asp:ListItem>
                                        <asp:ListItem Value="16">客户特殊要求确认</asp:ListItem>
                                         <asp:ListItem Value="17">仓库发货确认</asp:ListItem>
                                        <asp:ListItem Value="18">销售到货确认</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>制造工厂:</td>
                                <td>
                                    <asp:DropDownList ID="ddlzzgc" runat="server" class="form-control input-s-sm " Width="130px">
                                        <asp:ListItem Value=""></asp:ListItem>
                                        <asp:ListItem Value="100">上海工厂</asp:ListItem>
                                        <asp:ListItem Value="200">昆山工厂</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td>要求发运日期:
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="txtstart_date21" Width="100" />
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender7" runat="server" TargetControlID="txtstart_date21"
                                        PopupButtonID="Image2" Format="yyyy/MM/dd" />
                                    ~
								<asp:TextBox runat="server" ID="txtstart_date22" Width="100" />
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender8" runat="server" TargetControlID="txtstart_date22"
                                        PopupButtonID="Image2" Format="yyyy/MM/dd" />
                                </td>
                                <td>相关部门:
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddl_depart" runat="server" class="form-control input-s-sm " Width="130px" OnSelectedIndexChanged="ddlDepart_SelectedIndexChanged" AutoPostBack="true">
                                    </asp:DropDownList>
                                </td>
                                <td>相关人员:
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddl_update_user" runat="server" class="form-control input-s-sm " Width="130px">
                                    </asp:DropDownList>
                                </td>
                                <td>是否逾时:
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddl_ISDelay" runat="server" class="form-control input-s-sm " Width="130px">
                                        <asp:ListItem Value="-1">All</asp:ListItem>
                                        <asp:ListItem Value="0">逾时</asp:ListItem>
                                        <asp:ListItem Value="1">未逾时</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                 <td>是否生成QAD订单:
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddl_iserp" runat="server" class="form-control input-s-sm " Width="160px">
                                        <asp:ListItem Value="-1">All</asp:ListItem>
                                        <asp:ListItem Value="0">未生成</asp:ListItem>
                                        <asp:ListItem Value="1">已生成</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td colspan="5" align="right">
                                    <asp:Button ID="Button1" runat="server" Text="查询"
                                        class="btn btn-large btn-primary"
                                        OnClick="Button1_Click" Width="100px" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div style="color: red">
        系统说明: 
                                   【完成状态】 红色底色:表示尚未处理，且已经逾时；
                                   【完成状态】 黄色底色:表示尚未处理，没有逾时，但是距要求完成时间三天内，即将逾时；
    </div>
    <div runat="server" id="DIV1" style="margin-left: 5px; margin-right: 5px">
        <table width="100%">
            <tr>
                <td valign="top">
                    <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" OnRowDataBound="GridView1_RowDataBound" PageSize="100" Width="100%" OnPageIndexChanging="GridView1_PageIndexChanging" GridLines="Both" BorderColor="LightGray" OnSorting="GridView1_Sorting">
                        <Columns>
                            <asp:BoundField DataField="ID" HeaderText="ID">
                                <HeaderStyle BackColor="#C1E2EB" Width="20px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="gkddh" HeaderText="顾客订单号">
                                <HeaderStyle BackColor="#C1E2EB" />
                            </asp:BoundField>
                            <asp:BoundField DataField="requestID" HeaderText="RequestID">
                                <HeaderStyle BackColor="#C1E2EB" />
                            </asp:BoundField>
                             <asp:BoundField DataField="dc_requestid" HeaderText="dc_requestid">
                                <HeaderStyle BackColor="#C1E2EB" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="订单号">
                                <ItemTemplate>
                                    <asp:HyperLink ID="HyperLink1" runat="server" Text='<%#Eval("code") %>' Width="130px"></asp:HyperLink>
                                </ItemTemplate>

                                <HeaderStyle BackColor="#C1E2EB" />
                            </asp:TemplateField>
                             <asp:BoundField DataField="qadso" HeaderText="QAD订单号">
                                <HeaderStyle BackColor="#C1E2EB" Wrap="false" />
                            </asp:BoundField>
                              <asp:BoundField DataField="gysdm" HeaderText="供应商代码">
                                <HeaderStyle BackColor="#C1E2EB" Wrap="false" />
                            </asp:BoundField>
                              <asp:BoundField DataField="fhz" HeaderText="发货至">
                                <HeaderStyle BackColor="#C1E2EB" Wrap="false" />
                            </asp:BoundField>
                            <asp:BoundField DataField="zzgc" HeaderText="制造工厂">
                                <HeaderStyle BackColor="#C1E2EB" Wrap="false" />
                            </asp:BoundField>
                            <asp:BoundField DataField="xmh" HeaderText="项目号">
                                <HeaderStyle BackColor="#C1E2EB" />
                            </asp:BoundField>
                            <asp:BoundField DataField="ljh" HeaderText="零件号">
                                <HeaderStyle BackColor="#C1E2EB" />
                            </asp:BoundField>
                            <asp:BoundField DataField="yhsl" HeaderText="要货数量">
                                <HeaderStyle BackColor="#C1E2EB" />
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="yqdh_date" HeaderText="要货时间" DataFormatString="{0:yyyy-MM-dd}">
                                <HeaderStyle BackColor="#C1E2EB" />
                                <ItemStyle />
                            </asp:BoundField>
                            <asp:BoundField DataField="yqfy_date" HeaderText="要求发运日期" DataFormatString="{0:yyyy-MM-dd}">
                                <HeaderStyle BackColor="#C1E2EB" />
                                <ItemStyle />
                            </asp:BoundField>
                            <asp:BoundField DataField="status" HeaderText="当前阶段">
                                <HeaderStyle BackColor="#C1E2EB" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Update_Engineer_MS" HeaderText="操作事项">
                                <HeaderStyle BackColor="#C1E2EB" />
                            </asp:BoundField>
                            <asp:BoundField DataField="RequireDate" HeaderText="任务要求完成时间" DataFormatString="{0:yyyy-MM-dd}" SortExpression="RequireDate"
                                ReadOnly="True">
                                <HeaderStyle BackColor="#C1E2EB" Wrap="false" />
                                <ItemStyle />
                            </asp:BoundField>

                            <asp:BoundField DataField="lastname" HeaderText="操作人">
                                <HeaderStyle BackColor="#C1E2EB" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Receive_time" HeaderText="接收时间" DataFormatString="{0:yyyy-MM-dd}">
                                <HeaderStyle BackColor="#C1E2EB" />
                            </asp:BoundField>
                            <asp:BoundField DataField="tracking_time" HeaderText="已耗时">
                                <HeaderStyle BackColor="#C1E2EB" />
                            </asp:BoundField>
                            <asp:BoundField DataField="sign_status" HeaderText="完成状态">
                                <HeaderStyle BackColor="#C1E2EB" />
                            </asp:BoundField>
                        </Columns>
                        <PagerSettings FirstPageText="首页" LastPageText="尾页" Mode="NextPreviousFirstLast" NextPageText="下页" PreviousPageText="上页" />
                        <PagerStyle ForeColor="Red" />
                    </asp:GridView>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>


