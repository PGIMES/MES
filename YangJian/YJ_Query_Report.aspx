<%@ Page Title="样件系统【报表查询】" Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.master" CodeFile="YJ_Query_Report.aspx.cs" Inherits="YangJian_YJ_Query_Report" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <style type="text/css">
        td {
            padding-left: 5px;
            padding-right: 5px;
        }

        .auto-style1 {
            width: 268px;
        }

        .auto-style2 {
            width: 517px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <script src="../Content/js/jquery.min.js" type="text/javascript"></script>
    <script src="../Content/js/bootstrap.min.js"></script>
    <script src="../Content/js/plugins/layer/layer.min.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            $("#tst").click(function () {
            });
        });
        $("#mestitle").text("【样件报表查询】");
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
                                <td>客户代码:
                                </td>
                                <td>
                                    <asp:TextBox ID="txt_gkdm" class="form-control input-s-sm" runat="server"></asp:TextBox>
                                </td>
                                <td>PGI零件号:
                                </td>
                                <td>
                                    <asp:TextBox ID="txt_xmh" class="form-control input-s-sm" runat="server"></asp:TextBox>
                                </td>
                                <td>产品状态:
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddltype" runat="server" class="form-control input-s-sm " Width="130px">
                                        <asp:ListItem Value="新产品">新产品</asp:ListItem>
                                        <asp:ListItem Value="量产件">量产件</asp:ListItem>
                                        <asp:ListItem Value="无产品状态">无产品状态</asp:ListItem>
                                    </asp:DropDownList>
                                <td>要求发运日期:
                                </td>
                                <td class="auto-style1">
                                    <asp:TextBox ID="txt_startdate" runat="server" Width="100" />
                                    <ajaxToolkit:CalendarExtender ID="txt_startdate_CalendarExtender"
                                        runat="server" PopupButtonID="Image2" Format="yyyy/MM/dd"
                                        TargetControlID="txt_startdate" />
                                    ~&nbsp;
                                    <asp:TextBox ID="txt_enddate" runat="server"
                                        Width="100" />
                                    <ajaxToolkit:CalendarExtender ID="txt_enddate_CalendarExtender"
                                        runat="server" PopupButtonID="Image2" Format="yyyy/MM/dd"
                                        TargetControlID="txt_enddate" />
                                </td>
                                <td>发货单号:
                                </td>
                                <td>
                                    <asp:TextBox ID="txt_fhdh" class="form-control input-s-sm" runat="server"></asp:TextBox>
                                </td>
                                <td>制造工厂:</td>
                                <td>
                                    <asp:DropDownList ID="ddlzzgc" runat="server" class="form-control input-s-sm " Width="130px">
                                        <asp:ListItem Value=""></asp:ListItem>
                                        <asp:ListItem Value="100">上海工厂</asp:ListItem>
                                        <asp:ListItem Value="200">昆山工厂</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>客户名称:
                                </td>
                                <td>
                                    <asp:TextBox ID="txt_gkmc" class="form-control input-s-sm" runat="server"></asp:TextBox>
                                </td>
                                <td>客户零件号:
                                </td>
                                <td>
                                    <asp:TextBox ID="txt_khljh" class="form-control input-s-sm" runat="server"></asp:TextBox>
                                </td>
                                <td>订单状态:
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlStauts" runat="server" class="form-control input-s-sm " Width="130px">
                                    </asp:DropDownList>
                                </td>
                                <td>通知发运日期:
                                </td>
                                <td colspan="1">
                                    <asp:TextBox runat="server" ID="txtstart_date21" Width="100" />
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender7" runat="server" TargetControlID="txtstart_date21"
                                        PopupButtonID="Image2" Format="yyyy/MM/dd" />
                                    ~
								<asp:TextBox runat="server" ID="txtstart_date22" Width="100" />
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender8" runat="server" TargetControlID="txtstart_date22"
                                        PopupButtonID="Image2" Format="yyyy/MM/dd" />
                                </td>
                                <td>顾客订单号:
                                </td>
                                <td>
                                    <asp:TextBox ID="txt_gkddh" class="form-control input-s-sm" runat="server"></asp:TextBox>
                                </td>
                                <td>人员检索:
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlxgry" runat="server" class="form-control input-s-sm " Width="130px">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>收货人信息:
                                </td>
                                <td>
                                    <asp:TextBox ID="txt_shrxx" class="form-control input-s-sm" runat="server"></asp:TextBox>
                                </td>
                                <td>收货人地址:
                                </td>
                                <td>
                                    <asp:TextBox ID="txt_shrdz" class="form-control input-s-sm" runat="server"></asp:TextBox>
                                </td>
                                <td>发货状态:</td>
                                <td>
                                    <asp:DropDownList ID="ddlfhStatus" runat="server" class="form-control input-s-sm " Width="130px">
                                        <asp:ListItem Value=""></asp:ListItem>
                                        <asp:ListItem Value="已发货">已发货</asp:ListItem>
                                        <asp:ListItem Value="未发货">未发货</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td>实际提货日期:
                                </td>
                                <td colspan="1">
                                    <asp:TextBox runat="server" ID="txtppap1" Width="100" />
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtppap1"
                                        PopupButtonID="Image2" Format="yyyy/MM/dd" />
                                    ~
								<asp:TextBox runat="server" ID="txtppap2" Width="100" />
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender4" runat="server" TargetControlID="txtppap2"
                                        PopupButtonID="Image2" Format="yyyy/MM/dd" />
                                </td>
                                <td>是否生成QAD订单:
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddl_iserp" runat="server" class="form-control input-s-sm " Width="130px">
                                        <asp:ListItem Value="-1">All</asp:ListItem>
                                        <asp:ListItem Value="0">未生成</asp:ListItem>
                                        <asp:ListItem Value="1">已生成</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td>部门检索:
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlDepart" runat="server" class="form-control input-s-sm " Width="130px" OnSelectedIndexChanged="ddlDepart_SelectedIndexChanged" AutoPostBack="true">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td>订舱状态:
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddl_IsDC" runat="server" class="form-control input-s-sm " Width="130px">
                                        <asp:ListItem Value="-1">All</asp:ListItem>
                                        <asp:ListItem Value="0">未申请</asp:ListItem>
                                        <asp:ListItem Value="1">已申请</asp:ListItem>
                                        <asp:ListItem Value="2">已订舱</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="10" align="right">
                                    <asp:Button ID="Button1" runat="server" Text="查询"
                                        class="btn btn-large btn-primary"
                                        OnClick="Button1_Click" Width="100px" />
                                </td>
                                <td align="right">&nbsp;</td>
                                <td align="right">
                                    <asp:Button ID="Button3" runat="server" Text="导出Excel(销售助理)"
                                        class="btn btn-large btn-primary" Width="155px" OnClick="Button3_Click" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div class="panel panel-info">
        <div class="panel-heading" data-toggle="collapse" data-target="#CKBZ">
            <strong>系统说明：</strong>
            <span class="caret"></span>
        </div>
        <div class="panel-body collapse" id="CKBZ">
            <div class="col-xs-12 col-sm-12  col-md-12 col-lg-12">
                <div class="area_block">
                    <div style="color: red">
                        <span>【所有任务状态栏位】： 红色底色：表示该任务待处理,且已经逾时；  黄色底色：表示该任务待处理，没有逾时,但距离“要求完成时间”三天内，即将逾时；   灰色底色：表示该任务已完成</span>
                    </div>
                    <div>
                        <span style="color: red">注：要求完成时间计算公式为(急单除外)：</span><br />
                        <span style="color: blue">【确认阶段】：确认阶段各相关工程师：要求发运日期-申请人日期>16天  要求完成时间 = 要求发运日期-16 ；要求发运日期-申请人日期<=16天 要求完成时间 = 销售输入订单当天
                        </span>
                        <br />
                        <span style="text-indent: 7em; color: blue; margin-left: 95px">销售助理发货日期确认：要求发运日期-申请人日期>15天   要求完成时间 = 要求发运日期-15 ；要求发运日期-申请人日期<=15天 要求完成时间 = 销售输入订单当天 </span>
                        <br />
                        <span style="color: blue">【实施阶段】：包装方案要求时间 = 要求发运日期-6 ； 备货要求时间 = 要求发运日期-5 ； 检验要求时间 = 要求发运日期-2；
                        </span>
                        <br />
                        <span style="color: blue; margin-left: 95px">客户特殊需求确认要求时间 = 要求发运日期 ；仓库发货要求时间 = 要求发运日期 ； 销售到货日期确认 = 要求到货日期；
                        </span>
                        <br />
                        <span style="color: blue; margin-left: 95px">销售QAD订单对接 =通知发运日期-5天 ； 销售订舱申请 = 通知发运日期-5天；物流订舱处理 = 销售申请日+3天(去除国假日)(注:比较小于3天，即为通知发运日期当天)
                        </span>
                    </div>

                </div>
            </div>
        </div>
    </div>



    <div runat="server" id="DIV1" style="margin-left: 5px; margin-right: 5px; margin-bottom: 10px">
        <table>
            <tr>
                <td valign="top">
                    <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" OnPageIndexChanging="GridView1_PageIndexChanging" BorderColor="LightGray" OnRowDataBound="GridView1_RowDataBound" ShowFooter="True" OnSorting="GridView1_Sorting">
                        <Columns>
                            <asp:BoundField DataField="ID" HeaderText="ID">
                                <HeaderStyle BackColor="#C1E2EB" Width="50px" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="样件单号">
                                <ItemTemplate>
                                    <asp:HyperLink ID="HyperLink1" runat="server" Text='<%#Eval("样件单号") %>' Width="125px"></asp:HyperLink>
                                </ItemTemplate>
                                <HeaderStyle BackColor="#C1E2EB" Width="125px" />
                            </asp:TemplateField>

                            <asp:BoundField DataField="QAD订单号" HeaderText="QAD订单号">
                                <HeaderStyle BackColor="#C1E2EB" Width="200px" Wrap="false" />
                            </asp:BoundField>
                            <asp:BoundField DataField="制造工厂" HeaderText="制造工厂">
                                <HeaderStyle BackColor="#C1E2EB" Width="200px" Wrap="false" />
                            </asp:BoundField>
                            <asp:BoundField DataField="订单收到日期" HeaderText="订单收到日期" DataFormatString="{0:yyyy-MM-dd}" SortExpression="订单收到日期"
                                ReadOnly="True">
                                <HeaderStyle BackColor="#C1E2EB" Width="200px" Wrap="false" />
                                <ItemStyle />
                            </asp:BoundField>
                            <asp:BoundField DataField="顾客订单号" HeaderText="顾客订单号">
                                <HeaderStyle BackColor="#C1E2EB" Width="200px" Wrap="false" />
                            </asp:BoundField>
                            <asp:BoundField DataField="顾客名称" HeaderText="顾客名称">
                                <HeaderStyle BackColor="#C1E2EB" Width="600px" Wrap="false" />
                            </asp:BoundField>
                            <asp:BoundField DataField="PGI零件号" HeaderText="PGI零件号">
                                <HeaderStyle BackColor="#C1E2EB" Width="200px" Wrap="false" />
                            </asp:BoundField>
                            <asp:BoundField DataField="客户零件号" HeaderText="客户零件号">
                                <HeaderStyle BackColor="#C1E2EB" Width="200px" Wrap="false" />
                                <ItemStyle />
                            </asp:BoundField>
                            <asp:BoundField DataField="要货数量" HeaderText="要货数量">
                                <HeaderStyle BackColor="#C1E2EB" Width="100px" Wrap="false" />
                                <ItemStyle />
                            </asp:BoundField>
                            <asp:BoundField DataField="要求到货日期" HeaderText="要求到货日期" DataFormatString="{0:yyyy-MM-dd}">
                                <HeaderStyle BackColor="#C1E2EB" Width="200px" Wrap="false" />
                                <ItemStyle />
                            </asp:BoundField>
                            <asp:BoundField DataField="要求发运日期" HeaderText="要求发运日期" DataFormatString="{0:yyyy-MM-dd}">
                                <HeaderStyle BackColor="#C1E2EB" Width="200px" Wrap="false" />
                                <ItemStyle />
                            </asp:BoundField>
                            <asp:BoundField DataField="产品状态" HeaderText="产品状态">
                                <HeaderStyle BackColor="#C1E2EB" Width="100px" Wrap="false" />
                                <ItemStyle />
                            </asp:BoundField>
                            <asp:BoundField DataField="订单状态" HeaderText="订单状态">
                                <HeaderStyle BackColor="#C1E2EB" Width="100px" Wrap="false" />
                            </asp:BoundField>
                            <asp:BoundField DataField="requestId" HeaderText="requestId">
                                <HeaderStyle BackColor="#C1E2EB" />
                            </asp:BoundField>
                            <asp:BoundField DataField="cp_status" HeaderText="cp_status">
                                <HeaderStyle BackColor="#C1E2EB" />
                            </asp:BoundField>
                            <asp:BoundField DataField="销售助理申请" HeaderText="销售订单输入">
                                <HeaderStyle BackColor="#C1E2EB" Width="200px" Wrap="false" />
                            </asp:BoundField>
                            <asp:BoundField DataField="项目工程师确认" HeaderText="项目产品状态">
                                <HeaderStyle BackColor="#C1E2EB" Width="200px" Wrap="false" />
                            </asp:BoundField>
                            <asp:BoundField DataField="包装工程师确认" HeaderText="包装方案确认">
                                <HeaderStyle BackColor="#C1E2EB" Width="200px" Wrap="false" />
                            </asp:BoundField>
                            <asp:BoundField DataField="产品工程师确认" HeaderText="产品备货交期确认">
                                <HeaderStyle BackColor="#C1E2EB" Width="200px" Wrap="false" />
                            </asp:BoundField>
                            <asp:BoundField DataField="质量工程师确认" HeaderText="质量检验交期确认">
                                <HeaderStyle BackColor="#C1E2EB" Width="200px" Wrap="false" />
                            </asp:BoundField>
                            <asp:BoundField DataField="销售助理确认发货日期" HeaderText="销售助理确认发货日期">
                                <HeaderStyle BackColor="#C1E2EB" Width="200px" Wrap="false" />
                            </asp:BoundField>
                            <asp:BoundField DataField="生成QAD订单" HeaderText="生成QAD订单">
                                <HeaderStyle BackColor="#C1E2EB" Width="200px" Wrap="false" />
                            </asp:BoundField>
                            <asp:BoundField DataField="订舱申请" HeaderText="订舱申请">
                                <HeaderStyle BackColor="#C1E2EB" Width="200px" Wrap="false" />
                            </asp:BoundField>
                            <asp:BoundField DataField="订舱处理" HeaderText="订舱处理">
                                <HeaderStyle BackColor="#C1E2EB" Width="200px" Wrap="false" />
                            </asp:BoundField>
                            <asp:BoundField DataField="包装工程师实施" HeaderText="包装包材备货">
                                <HeaderStyle BackColor="#C1E2EB" Width="200px" Wrap="false" />
                            </asp:BoundField>
                            <asp:BoundField DataField="产品工程师实施" HeaderText="产品工程师备货">
                                <HeaderStyle BackColor="#C1E2EB" Width="200px" Wrap="false" />
                            </asp:BoundField>
                            <asp:BoundField DataField="仓库班长送检" HeaderText="仓库班长送检">
                                <HeaderStyle BackColor="#C1E2EB" Width="200px" Wrap="false" />
                            </asp:BoundField>
                            <asp:BoundField DataField="检验班长检验" HeaderText="检验班长检验">
                                <HeaderStyle BackColor="#C1E2EB" Width="200px" Wrap="false" />
                            </asp:BoundField>
                            <asp:BoundField DataField="质量工程师检验报告" HeaderText="质量工程师检验报告">
                                <HeaderStyle BackColor="#C1E2EB" Width="200px" Wrap="false" />
                            </asp:BoundField>
                            <asp:BoundField DataField="仓库班长取货" HeaderText="仓库班长取货">
                                <HeaderStyle BackColor="#C1E2EB" Width="200px" Wrap="false" />
                            </asp:BoundField>
                            <asp:BoundField DataField="项目工程师包装发货" HeaderText="项目工程师包装发货">
                                <HeaderStyle BackColor="#C1E2EB" Width="200px" Wrap="false" />
                            </asp:BoundField>
                            <asp:BoundField DataField="仓库班长包装发货" HeaderText="仓库班长包装发货">
                                <HeaderStyle BackColor="#C1E2EB" Width="200px" Wrap="false" />
                            </asp:BoundField>
                            <asp:BoundField DataField="销售助理到货" HeaderText="销售助理到货">
                                <HeaderStyle BackColor="#C1E2EB" Width="200px" Wrap="false" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="货运单号">
                                <ItemTemplate>
                                    <asp:HyperLink ID="HyperLink2" runat="server" Text='<%#Eval("货运单号") %>' Width="150px"></asp:HyperLink>
                                </ItemTemplate>
                                <HeaderStyle BackColor="#C1E2EB" Width="150px" Wrap="false" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="requestId_dc" HeaderText="requestId_dc">
                                <HeaderStyle BackColor="#C1E2EB" />
                            </asp:BoundField>
                        </Columns>
                        <PagerSettings FirstPageText="首页" LastPageText="尾页" Mode="NextPreviousFirstLast" NextPageText="下页" PreviousPageText="上页" />
                        <PagerStyle ForeColor="Red" />
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td valign="top">
                    <asp:GridView ID="GridView2" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" OnPageIndexChanging="GridView2_PageIndexChanging" GridLines="Both" BorderColor="LightGray" OnRowDataBound="GridView2_RowDataBound" ShowFooter="True" OnSorting="GridView2_Sorting">
                        <Columns>
                            <asp:BoundField DataField="ID" HeaderText="ID">
                                <HeaderStyle BackColor="#C1E2EB" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="样件单号">
                                <ItemTemplate>
                                    <asp:HyperLink ID="HyperLink1" runat="server" Text='<%#Eval("样件单号") %>' Width="125px"></asp:HyperLink>
                                </ItemTemplate>
                                <HeaderStyle BackColor="#C1E2EB" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="QAD订单号" HeaderText="QAD订单号">
                                <HeaderStyle BackColor="#C1E2EB" Width="200px" Wrap="false" />
                            </asp:BoundField>
                            <asp:BoundField DataField="制造工厂" HeaderText="制造工厂">
                                <HeaderStyle BackColor="#C1E2EB" Width="200px" Wrap="false" />
                            </asp:BoundField>
                            <asp:BoundField DataField="订单收到日期" HeaderText="订单收到日期" DataFormatString="{0:yyyy-MM-dd}" SortExpression="订单收到日期"
                                ReadOnly="True">
                                <HeaderStyle BackColor="#C1E2EB" Width="200px" Wrap="false" />
                            </asp:BoundField>
                            <asp:BoundField DataField="顾客订单号" HeaderText="顾客订单号">
                                <HeaderStyle BackColor="#C1E2EB" Width="200px" Wrap="false" />
                            </asp:BoundField>
                            <asp:BoundField DataField="顾客名称" HeaderText="顾客名称">
                                <HeaderStyle BackColor="#C1E2EB" Width="600px" Wrap="false" />
                            </asp:BoundField>
                            <asp:BoundField DataField="PGI零件号" HeaderText="PGI零件号">
                                <HeaderStyle BackColor="#C1E2EB" Width="200px" Wrap="false" />
                            </asp:BoundField>
                            <asp:BoundField DataField="客户零件号" HeaderText="客户零件号">
                                <HeaderStyle BackColor="#C1E2EB" Width="200px" Wrap="false" />
                                <ItemStyle />
                            </asp:BoundField>
                            <asp:BoundField DataField="要货数量" HeaderText="要货数量">
                                <HeaderStyle BackColor="#C1E2EB" Width="100px" Wrap="false" />
                                <ItemStyle />
                            </asp:BoundField>
                            <asp:BoundField DataField="要求到货日期" HeaderText="要求到货日期" DataFormatString="{0:yyyy-MM-dd}">
                                <HeaderStyle BackColor="#C1E2EB" Width="200px" Wrap="false" />
                                <ItemStyle />
                            </asp:BoundField>
                            <asp:BoundField DataField="要求发运日期" HeaderText="要求发运日期" DataFormatString="{0:yyyy-MM-dd}">
                                <HeaderStyle BackColor="#C1E2EB" Width="200px" Wrap="false" />
                                <ItemStyle />
                            </asp:BoundField>
                            <asp:BoundField DataField="产品状态" HeaderText="产品状态">
                                <HeaderStyle BackColor="#C1E2EB" Width="100px" Wrap="false" />
                                <ItemStyle />
                            </asp:BoundField>
                            <asp:BoundField DataField="订单状态" HeaderText="订单状态">
                                <HeaderStyle BackColor="#C1E2EB" Width="200px" Wrap="false" />
                            </asp:BoundField>
                            <asp:BoundField DataField="requestId" HeaderText="requestId">
                                <HeaderStyle BackColor="#C1E2EB" Width="200px" Wrap="false" />
                            </asp:BoundField>
                            <asp:BoundField DataField="cp_status" HeaderText="cp_status">
                                <HeaderStyle BackColor="#C1E2EB" Width="200px" Wrap="false" />
                            </asp:BoundField>
                            <asp:BoundField DataField="销售助理申请" HeaderText="销售订单输入">
                                <HeaderStyle BackColor="#C1E2EB" Width="200px" Wrap="false" />
                            </asp:BoundField>
                            <asp:BoundField DataField="项目工程师确认" HeaderText="项目产品状态">
                                <HeaderStyle BackColor="#C1E2EB" Width="200px" Wrap="false" />
                            </asp:BoundField>
                            <asp:BoundField DataField="包装工程师确认" HeaderText="包装方案确认">
                                <HeaderStyle BackColor="#C1E2EB" Width="200px" Wrap="false" />
                            </asp:BoundField>
                            <asp:BoundField DataField="物流计划订单确认" HeaderText="计划备货交期确认">
                                <HeaderStyle BackColor="#C1E2EB" Width="200px" Wrap="false" />
                            </asp:BoundField>
                            <asp:BoundField DataField="质量工程师确认1" HeaderText="质量检验要求">
                                <HeaderStyle BackColor="#C1E2EB" Width="200px" Wrap="false" />
                            </asp:BoundField>
                            <asp:BoundField DataField="质量工程师确认2" HeaderText="质量检验交期确认">
                                <HeaderStyle BackColor="#C1E2EB" Width="200px" Wrap="false" />
                            </asp:BoundField>
                            <asp:BoundField DataField="销售助理确认发货日期" HeaderText="销售助理确认发货日期">
                                <HeaderStyle BackColor="#C1E2EB" Width="200px" Wrap="false" />
                            </asp:BoundField>
                            <asp:BoundField DataField="生成QAD订单" HeaderText="生成QAD订单">
                                <HeaderStyle BackColor="#C1E2EB" Width="200px" Wrap="false" />
                            </asp:BoundField>
                            <asp:BoundField DataField="订舱申请" HeaderText="订舱申请">
                                <HeaderStyle BackColor="#C1E2EB" Width="200px" Wrap="false" />
                            </asp:BoundField>
                            <asp:BoundField DataField="订舱处理" HeaderText="订舱处理">
                                <HeaderStyle BackColor="#C1E2EB" Width="200px" Wrap="false" />
                            </asp:BoundField>
                            <asp:BoundField DataField="包装工程师实施" HeaderText="包装包材备货">
                                <HeaderStyle BackColor="#C1E2EB" Width="200px" Wrap="false" />
                            </asp:BoundField>
                            <asp:BoundField DataField="物流计划备货" HeaderText="物流计划备货">
                                <HeaderStyle BackColor="#C1E2EB" Width="200px" Wrap="false" />
                            </asp:BoundField>
                            <asp:BoundField DataField="质量工程师送检" HeaderText="质量工程师送检">
                                <HeaderStyle BackColor="#C1E2EB" Width="200px" Wrap="false" />
                            </asp:BoundField>
                            <asp:BoundField DataField="仓库班长送检" HeaderText="仓库班长送检">
                                <HeaderStyle BackColor="#C1E2EB" Width="200px" Wrap="false" />
                            </asp:BoundField>
                            <asp:BoundField DataField="检验班长检验" HeaderText="检验班长检验">
                                <HeaderStyle BackColor="#C1E2EB" Width="200px" Wrap="false" />
                            </asp:BoundField>
                            <asp:BoundField DataField="质量工程师检验报告" HeaderText="质量工程师检验报告">
                                <HeaderStyle BackColor="#C1E2EB" Width="200px" Wrap="false" />
                            </asp:BoundField>
                            <asp:BoundField DataField="仓库班长取货" HeaderText="仓库班长取货">
                                <HeaderStyle BackColor="#C1E2EB" Width="200px" Wrap="false" />
                            </asp:BoundField>
                            <asp:BoundField DataField="项目工程师包装发货" HeaderText="项目工程师包装发货">
                                <HeaderStyle BackColor="#C1E2EB" Width="200px" Wrap="false" />
                            </asp:BoundField>
                            <asp:BoundField DataField="仓库班长包装发货" HeaderText="仓库班长包装发货">
                                <HeaderStyle BackColor="#C1E2EB" Width="200px" Wrap="false" />
                            </asp:BoundField>
                            <asp:BoundField DataField="销售助理到货" HeaderText="销售助理到货">
                                <HeaderStyle BackColor="#C1E2EB" Width="200px" Wrap="false" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="货运单号">
                                <ItemTemplate>
                                    <asp:HyperLink ID="HyperLink2" runat="server" Text='<%#Eval("货运单号") %>' Width="150px"></asp:HyperLink>
                                </ItemTemplate>
                                <HeaderStyle BackColor="#C1E2EB" Width="150px" Wrap="false" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="requestId_dc" HeaderText="requestId_dc">
                                <HeaderStyle BackColor="#C1E2EB" />
                            </asp:BoundField>
                        </Columns>
                        <PagerSettings FirstPageText="首页" LastPageText="尾页" Mode="NextPreviousFirstLast" NextPageText="下页" PreviousPageText="上页" />
                        <PagerStyle ForeColor="Red" />
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td valign="top">
                    <asp:GridView ID="GridView3" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" OnPageIndexChanging="GridView3_PageIndexChanging" GridLines="Both" BorderColor="LightGray" OnRowDataBound="GridView3_RowDataBound" ShowFooter="True" OnSorting="GridView3_Sorting">
                        <Columns>
                            <asp:BoundField DataField="ID" HeaderText="ID">
                                <HeaderStyle BackColor="#C1E2EB" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="样件单号">
                                <ItemTemplate>
                                    <asp:HyperLink ID="HyperLink1" runat="server" Text='<%#Eval("样件单号") %>' Width="125px"></asp:HyperLink>
                                </ItemTemplate>
                                <HeaderStyle BackColor="#C1E2EB" Width="125px" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="制造工厂" HeaderText="制造工厂">
                                <HeaderStyle BackColor="#C1E2EB" Width="200px" Wrap="false" />
                            </asp:BoundField>
                            <asp:BoundField DataField="订单收到日期" HeaderText="订单收到日期" DataFormatString="{0:yyyy-MM-dd}" SortExpression="订单收到日期"
                                ReadOnly="True">
                                <HeaderStyle BackColor="#C1E2EB" Width="200px" Wrap="false" />
                                <ItemStyle />
                            </asp:BoundField>
                            <asp:BoundField DataField="顾客订单号" HeaderText="顾客订单号">
                                <HeaderStyle BackColor="#C1E2EB" Width="200px" Wrap="false" />
                            </asp:BoundField>
                            <asp:BoundField DataField="顾客名称" HeaderText="顾客名称">
                                <HeaderStyle BackColor="#C1E2EB" Width="600px" Wrap="false" />
                            </asp:BoundField>
                            <asp:BoundField DataField="PGI零件号" HeaderText="PGI零件号">
                                <HeaderStyle BackColor="#C1E2EB" Width="200px" Wrap="false" />
                            </asp:BoundField>
                            <asp:BoundField DataField="客户零件号" HeaderText="客户零件号">
                                <HeaderStyle BackColor="#C1E2EB" Width="200px" Wrap="false" />
                                <ItemStyle />
                            </asp:BoundField>
                            <asp:BoundField DataField="要货数量" HeaderText="要货数量">
                                <HeaderStyle BackColor="#C1E2EB" Width="100px" Wrap="false" />
                                <ItemStyle />
                            </asp:BoundField>
                            <asp:BoundField DataField="要求到货日期" HeaderText="要求到货日期" DataFormatString="{0:yyyy-MM-dd}">
                                <HeaderStyle BackColor="#C1E2EB" Width="200px" Wrap="false" />
                                <ItemStyle />
                            </asp:BoundField>
                            <asp:BoundField DataField="要求发运日期" HeaderText="要求发运日期" DataFormatString="{0:yyyy-MM-dd}">
                                <HeaderStyle BackColor="#C1E2EB" Width="200px" Wrap="false" />
                                <ItemStyle />
                            </asp:BoundField>
                            <asp:BoundField DataField="产品状态" HeaderText="产品状态">
                                <HeaderStyle BackColor="#C1E2EB" Width="100px" Wrap="false" />
                                <ItemStyle />
                            </asp:BoundField>
                            <asp:BoundField DataField="订单状态" HeaderText="订单状态">
                                <HeaderStyle BackColor="#C1E2EB" Width="200px" Wrap="false" />
                            </asp:BoundField>
                            <asp:BoundField DataField="requestId" HeaderText="requestId">
                                <HeaderStyle BackColor="#C1E2EB" />
                            </asp:BoundField>
                            <asp:BoundField DataField="项目工程师" HeaderText="项目工程师">
                                <HeaderStyle BackColor="#C1E2EB" Width="200px" Wrap="false" />
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
