<%@ Page Title="产品系统【产品统计】" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Chanpin_Fenxi_ByCPFZR.aspx.cs" Inherits="Product_Chanpin_Fenxi_ByCPFZR" %>



      <%@ Register Assembly="DevExpress.XtraCharts.v17.2.Web, Version=17.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.XtraCharts.Web" TagPrefix="dx" %>


<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <link href="../Content/css/bootstrap.min.css" rel="stylesheet" />
    <script src="../Content/js/jquery.min.js"></script>
    <script src="../Content/js/bootstrap.min.js"></script>
    <script src="../Content/js/plugins/layer/layer.min.js"></script>
    <script src="../Content/js/plugins/layer/laydate/laydate.js"></script>
    <link href="../Content/js/plugins/bootstrap-select/css/bootstrap-select.min.css" rel="stylesheet" />
    <script src="../Content/js/plugins/bootstrap-select/js/bootstrap-select.min.js"></script>
    <script type="text/javascript">

        $(document).ready(function () {
            $("#mestitle").text("【产品统计】");
            //隐藏界面上的四个Textbox 和LinkBtn
            $("input[id*='txtName']").css("display", "none");
            $("input[id*='txtType']").css("display", "none");
            $("a[id*='_Link']").css("display", "none");
            $("a[name='sale']").click(function () {
                $("input[id*='txtName']").val($(this).attr("names"));
                $("input[id*='txtType']").val($(this).attr("types"));
            })
            //整行变色
            $("[id*=GridView] td").bind("click", function () {
                var row = $(this).parent();
                $("[id*=GridView] td").each(function () {
                    if ($(this)[0] != row[0]) {
                        $("td", this).removeClass("selected_row");
                    }
                });
                $("td", row).each(function () {
                    if (!$(this).hasClass("selected_row")) {
                        $(this).addClass("selected_row");
                    } else {
                        $(this).removeClass("selected_row");
                    }
                });
            });
            //单元格变色
            $("td[allowClick=true]").click(function () {
                $("td").css("background", "");  //其他td为无色
                $(this).css("background", "orange"); //点击变色。
            })
            $('.selectpicker').change(function () {
                if (this.id.indexOf("Status") >= 0) {
                    $("input[id*='txt_product_status']").val($("[class='selectpicker A']").val());
                }
                else if (this.id.indexOf("Cust") >= 0) {
                    $("input[id*='txtCustomer_name']").val($("[class='selectpicker C']").val());
                }
                else if (this.id.indexOf("Leibie") >= 0) {
                    $("input[id*='txt_p_leibie']").val($("[class='selectpicker B']").val());
                }
            });
        });//EndReady
        ///开启修改负责人界面
        function OpenMsg(ele, condition) {
            param = $(ele).attr('param');
            layer.open({
                type: 2,
                skin: 'layui-layer-demo', //样式类名
                closeBtn: 1, //显示关闭按钮
                anim: 2,
                title: ['报价跟踪记录', false],
                area: ['800px', '650px'],
                shadeClose: true, //开启遮罩关闭
                content: 'product.aspx' + param,
                end: function () {
                    if (condition == "S") {
                        __doPostBack('ctl00$MainContent$LinkSale', '');
                    }
                    if (condition == "C") {
                        __doPostBack('ctl00$MainContent$LinkCustomer', '');
                    }
                }
            });
        }
    </script>
    <style type="text/css">
        td {
            padding-left: 5px;
            padding-right: 5px;
            padding-top: 5px;
        }

        th {
            text-align: center;
            padding-left: 5px;
            padding-right: 5px;
        }

        .selected_row {
            background-color: #A1DCF2;
        }

        .panel {
            margin-bottom: 5px;
        }

        .panel-heading {
            padding: 5px 5px 5px 5px;
        }

        .panel-body {
            padding: 5px 5px 5px 5px;
        }

        body {
            margin-left: 5px;
            margin-right: 5px;
        }

        .col-lg-1, .col-lg-10, .col-lg-11, .col-lg-12, .col-lg-2, .col-lg-3, .col-lg-4, .col-lg-5, .col-lg-6, .col-lg-7, .col-lg-8, .col-lg-9, .col-md-1, .col-md-10, .col-md-11, .col-md-12, .col-md-2, .col-md-3, .col-md-4, .col-md-5, .col-md-6, .col-md-7, .col-md-8, .col-md-9, .col-sm-1, .col-sm-10, .col-sm-11, .col-sm-12, .col-sm-2, .col-sm-3, .col-sm-4, .col-sm-5, .col-sm-6, .col-sm-7, .col-sm-8, .col-sm-9, .col-xs-1, .col-xs-10, .col-xs-11, .col-xs-12, .col-xs-2, .col-xs-3, .col-xs-4, .col-xs-5, .col-xs-6, .col-xs-7, .col-xs-8, .col-xs-9 {
            position: relative;
            min-height: 1px;
            padding-right: 1px;
            padding-left: 1px;
            margin-top: 0px;
        }
    </style>
    <script src="../Content/js/jquery.min.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="panel panel-info">
        <table>
            <tr>
                <td>
                    统计类别：</td>
                <td>
                    <asp:DropDownList ID="ddl_type" runat="server" 
                        class="form-control input-s-sm " Width="120px" 
                        AutoPostBack="True" onselectedindexchanged="ddl_type_SelectedIndexChanged" 
                        >
                        <asp:ListItem Value="0">负责人</asp:ListItem>
                        <asp:ListItem Value="1">客户</asp:ListItem>
                        <asp:ListItem Value="2">产品大类</asp:ListItem>
                        <asp:ListItem Value="3">生产地点</asp:ListItem>
                        <asp:ListItem Value="4">发运地点</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>产品大类:
                                </td>
                                <td>
                                    <asp:TextBox ID="txt_p_leibie" class="form-control input-s-sm" runat="server" style="display:none"></asp:TextBox>
                                    <select id="selectPLeibie" name="selectPLeibie" class="selectpicker B" multiple  data-live-search="true" runat="server" style="width:100px" >                                          
                                    </select>
                                </td>
                <td>产品状态：
                </td>
                <td>
                    <asp:TextBox ID="txt_product_status" class="form-control input-s-sm" runat="server" Style="display: none"></asp:TextBox>
                    <select id="selectPStatus" name="selectPStatus" class="selectpicker A" multiple data-live-search="true" runat="server" style="width: 100px">
                    </select>
                </td>
                <td>组织：
                </td>
                <td>
                    <asp:DropDownList ID="ddl_dept" runat="server" 
                        class="form-control input-s-sm " Width="120px" 
                        AutoPostBack="True" 
                        onselectedindexchanged="ddl_dept_SelectedIndexChanged">
                        <%--<asp:ListItem Value="全部">全部</asp:ListItem>--%>
                        <asp:ListItem Value="销售二部">销售二部</asp:ListItem>
                        <asp:ListItem Value="项目管理部">项目管理部</asp:ListItem>
                        <asp:ListItem Value="工程一部">工程一部</asp:ListItem>
                        <asp:ListItem Value="工程二部">工程二部</asp:ListItem>
                        <asp:ListItem Value="产品一组">&nbsp;&nbsp;&nbsp;&nbsp;产品一组</asp:ListItem>
                       <%-- <asp:ListItem Value="产品二组">&nbsp;&nbsp;&nbsp;&nbsp;产品二组</asp:ListItem>--%>
                        <asp:ListItem Value="产品三组">&nbsp;&nbsp;&nbsp;&nbsp;产品三组</asp:ListItem>
                        <asp:ListItem Value="工程三部">工程三部</asp:ListItem>
                        <asp:ListItem Value="产品二组">&nbsp;&nbsp;&nbsp;&nbsp;产品二组</asp:ListItem>
                        <asp:ListItem Value="产品四组">&nbsp;&nbsp;&nbsp;&nbsp;产品四组</asp:ListItem>
                        <asp:ListItem Value="压铸技术部">压铸技术部</asp:ListItem>
                        <asp:ListItem Value="质量二部">质量二部</asp:ListItem>
                        <asp:ListItem Value="物流二部">物流二部</asp:ListItem>
                        <asp:ListItem Value="采购二部">采购二部</asp:ListItem>
                        <asp:ListItem Value="物流一部">物流一部</asp:ListItem>
                       
                    </asp:DropDownList>
                </td>
                <td>岗位：
                </td>
                <td>
                    <asp:DropDownList ID="ddl_GCS" runat="server" class="form-control input-s-sm " Width="120px">
                        <asp:ListItem Value="销售工程师">销售工程师</asp:ListItem>
                        <asp:ListItem Value="项目工程师">项目工程师</asp:ListItem>
                        <asp:ListItem Value="产品工程师">产品工程师</asp:ListItem>
                        <asp:ListItem Value="压铸工程师">压铸工程师</asp:ListItem>
                        <asp:ListItem Value="模具工程师">模具工程师</asp:ListItem>
                        <asp:ListItem Value="质量工程师">质量工程师</asp:ListItem>
                        <asp:ListItem Value="供应商质量工程师">供应商质量工程师</asp:ListItem>
                        <asp:ListItem Value="物流工程师">物流工程师</asp:ListItem>
                        <asp:ListItem Value="采购工程师">采购工程师</asp:ListItem>
                        <asp:ListItem Value="包装工程师">包装工程师</asp:ListItem>
                        <asp:ListItem Value="计划工程师">计划工程师</asp:ListItem>
                    </asp:DropDownList>
                </td>
                 <td>直接客户：
                </td>
                <td>
                    <asp:TextBox ID="txtCustomer_name" class="form-control input-s-sm" runat="server" Style="display: none"></asp:TextBox>
                    <select id="selectCust" name="selectCust" class="selectpicker C" multiple data-live-search="true" runat="server" style="width: 100px">
                    </select>
                </td>
                <td>
                    <asp:Button ID="btnQuery" runat="server" Text="确定" class="btn btn-lg btn-primary"
                        Width="92px" Height="45px" OnClick="btnQuery_Click" />
                    <asp:TextBox ID="txtName" runat="server" />
                    <asp:TextBox ID="txtType" runat="server" />
                </td>
            </tr>
        </table>
    </div>
    <div style="color: red">
        系统说明: 当负责人尚未分配时，此报表统计为【空白】
    </div>
    <div>
        <%--By 客户的统计--%>

        <div class="panel panel-heading">
            <asp:Label ID="lblMstC" runat="server" Text="统计"></asp:Label>
        </div>
        <div class="panel panel-body">
            <div style="float: left">
                <dx:WebChartControl ID="ChartA"
                    runat="server" CrosshairEnabled="True" Height="300px" Width="1000px"
                    PaletteName="Civic">
                </dx:WebChartControl>


                <asp:GridView ID="gv_CPFZR" BorderColor="lightgray" BorderWidth="2px" CssClass="gvHeader th"
                    runat="server" OnRowDataBound="gv_mst_RowDataBound">
                    <EmptyDataTemplate>
                        查无资料
                    </EmptyDataTemplate>
                </asp:GridView>
            </div>
        </div>
    </div>
    <div class=" panel panel-info  col-lg-12 ">
        <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div class="panel panel-heading">
                    <asp:Label ID="lbldetail" runat="server" Text="明细" BorderColor="lightgray"></asp:Label>
                </div>
                <asp:LinkButton ID="LinkDtl" runat="server"
                    OnClick="LinkDtl_Click">binding明细</asp:LinkButton>
                <div class="panel panel-body">

                    <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" BorderColor="LightGray" OnRowDataBound="gv1_RowDataBound">
                        <Columns>
                            <asp:BoundField DataField="pgino" HeaderText="序号">
                                <HeaderStyle BackColor="#C1E2EB" />
                            </asp:BoundField>
                              <asp:BoundField DataField="product_leibie" HeaderText="产品大类">
                                <HeaderStyle BackColor="#C1E2EB" Wrap="false" />
                            </asp:BoundField>
                            <asp:BoundField DataField="product_status" HeaderText="产品状态">
                                <HeaderStyle BackColor="#C1E2EB" Wrap="false" />
                            </asp:BoundField>
                            <asp:BoundField DataField="pgino" HeaderText="PGI项目号">
                                <HeaderStyle BackColor="#C1E2EB" Wrap="false" />
                            </asp:BoundField>
                            <asp:BoundField DataField="productcode" HeaderText="零件号">
                                <HeaderStyle BackColor="#C1E2EB" Wrap="false" />
                            </asp:BoundField>
                            <asp:BoundField DataField="productname" HeaderText="零件名称">
                                <HeaderStyle BackColor="#C1E2EB" Wrap="false" />
                            </asp:BoundField>
                            <asp:BoundField DataField="customer_name" HeaderText="客户名称">
                                <HeaderStyle BackColor="#C1E2EB" Wrap="false" />
                            </asp:BoundField>
                            <asp:BoundField DataField="end_customer_name" HeaderText="最终客户">
                                <HeaderStyle BackColor="#C1E2EB" Wrap="false" />
                            </asp:BoundField>
                            <asp:BoundField DataField="customer_project" HeaderText="顾客项目">
                                <HeaderStyle BackColor="#C1E2EB" Wrap="false" />
                                <ItemStyle />
                            </asp:BoundField>
                            <asp:BoundField DataField="sale" HeaderText="销售工程师">
                                <HeaderStyle BackColor="#C1E2EB" Wrap="false" />
                            </asp:BoundField>
                            <asp:BoundField DataField="project_user" HeaderText="项目工程师">
                                <HeaderStyle BackColor="#C1E2EB" Wrap="false" />
                            </asp:BoundField>
                            <asp:BoundField DataField="product_user" HeaderText="产品工程师">
                                <HeaderStyle BackColor="#C1E2EB" Wrap="false" />
                                <ItemStyle />
                            </asp:BoundField>
                            <asp:BoundField DataField="yz_user" HeaderText="压铸工程师">
                                <HeaderStyle BackColor="#C1E2EB" Wrap="false" />
                                <ItemStyle />
                            </asp:BoundField>
                            <asp:BoundField DataField="moju_user" HeaderText="模具工程师">
                                <HeaderStyle BackColor="#C1E2EB" Wrap="false" />
                                <ItemStyle />
                            </asp:BoundField>
                            <asp:BoundField DataField="zl_user" HeaderText="质量工程师">
                                <HeaderStyle BackColor="#C1E2EB" Wrap="false" />
                            </asp:BoundField>
                             <asp:BoundField DataField="sqe_user1" HeaderText="SQE1">
                                <HeaderStyle BackColor="#C1E2EB" Wrap="false" />
                            </asp:BoundField>
                            <asp:BoundField DataField="sqe_user2" HeaderText="SQE2">
                            <HeaderStyle BackColor="#C1E2EB" />
                            </asp:BoundField>
                            <asp:BoundField DataField="caigou" HeaderText="采购工程师">
                            <HeaderStyle BackColor="#C1E2EB" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="bz_user" HeaderText="包装工程师">
                                <HeaderStyle BackColor="#C1E2EB" Wrap="false" />
                            </asp:BoundField>
                             <asp:BoundField DataField="wl_user" HeaderText="物流工程师">
                                <HeaderStyle BackColor="#C1E2EB" Wrap="false" />
                            </asp:BoundField>
                            <asp:BoundField DataField="更改负责人" HeaderText="更改负责人">
                                <HeaderStyle BackColor="#C1E2EB" Wrap="false" />
                            </asp:BoundField>

                            <asp:BoundField DataField="requestid" HeaderText="requestid" ShowHeader="false">
                                <HeaderStyle BackColor="#C1E2EB" Wrap="false" />
                            </asp:BoundField>
                        </Columns>
                        <PagerSettings FirstPageText="首页" LastPageText="尾页" Mode="NextPreviousFirstLast" NextPageText="下页" PreviousPageText="上页" />
                        <PagerStyle ForeColor="Red" />
                    </asp:GridView>


                    <asp:GridView ID="GridView2" runat="server" CssClass="GridView1" AllowPaging="false" 
                                AutoGenerateColumns="true" BorderColor="LightGray" HeaderStyle-BackColor="LightBlue" OnRowDataBound="GridView2_RowDataBound" OnPageIndexChanging="GridView2_PageIndexChanging" AllowSorting="true" Width="1900px" OnSorting="GridView2_Sorting" OnRowCreated="GridView2_RowCreated" ShowFooter="True">
                                <EmptyDataTemplate>no data found</EmptyDataTemplate>
                                <PagerSettings FirstPageText="首页" LastPageText="尾页" Mode="NextPreviousFirstLast" NextPageText="下页" PreviousPageText="上页" />
                                <PagerStyle ForeColor="lightblue" />
                            </asp:GridView>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>

</asp:Content>
