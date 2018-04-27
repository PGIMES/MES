<%@ Page Title="【销售统计】" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Customer_Query.aspx.cs" Inherits="Sale_Customer_Query" %>

  <%@ Register Assembly="DevExpress.XtraCharts.v17.2.Web, Version=17.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.XtraCharts.Web" TagPrefix="dx" %>


<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <style>
        .gvHeader th {
            text-align: center;
            background: #C1E2EB;
            color: Brown;
            border: solid 1px #333333;
            padding: 0px 5px 0px 5px;
            font-size: 12px;
        }
         td{           
            padding: 0px 4px 0px 4px;
            
        }
    </style>
    <script src="../Content/js/jquery.min.js" type="text/javascript"></script>
    <script src="../Content/js/plugins/layer/layer.min.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript">

        $("#mestitle").text("【销售统计】");
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
                                        <td>年份:
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="txt_year" runat="server" class="form-control input-s-sm ">
                                            </asp:DropDownList>
                                        </td>
                                        <td>截止月：
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="txt_mnth" runat="server"
                                                class="form-control input-s-sm">
                                            </asp:DropDownList>
                                        </td>

                                        <td>统计项：</td>
                                        <td>
                                            <asp:DropDownList ID="ddl_item" runat="server"
                                                class="form-control input-s-sm "
                                                OnSelectedIndexChanged="ddl_item_SelectedIndexChanged"
                                                AutoPostBack="True">
                                                <asp:ListItem Value="1">金额</asp:ListItem>
                                                <asp:ListItem Value="0">数量</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>

                                        <td>分析维度</td>
                                        <td>
                                            <asp:DropDownList ID="ddl_wd" runat="server"
                                                class="form-control input-s-sm "
                                                OnSelectedIndexChanged="ddl_wd_SelectedIndexChanged"
                                                AutoPostBack="True">
                                                <asp:ListItem Value="0">客户</asp:ListItem>
                                                <asp:ListItem Value="1">地区</asp:ListItem>
                                                <asp:ListItem Value="2">产品类别</asp:ListItem>
                                            </asp:DropDownList></td>

                                        <td>公司</td>
                                        <td>
                                            <asp:DropDownList ID="ddl_comp" runat="server" class="form-control input-s-sm ">
                                                <asp:ListItem></asp:ListItem>
                                                <asp:ListItem>100</asp:ListItem>
                                                <asp:ListItem>200</asp:ListItem>
                                            </asp:DropDownList></td>

                                        <td>
                                            <asp:Label ID="lb_type" runat="server" Text="类别"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddl_type" runat="server" class="form-control input-s-sm ">
                                                <asp:ListItem Value="3">ALL</asp:ListItem>
                                                <asp:ListItem Value="0">产品收入</asp:ListItem>
                                                <asp:ListItem Value="1">模具收入</asp:ListItem>
                                                <asp:ListItem Value="2">发票调整</asp:ListItem>
                                                <asp:ListItem Value="4">其他收入</asp:ListItem>
                                            </asp:DropDownList></td>
                                    </tr>



                                    <tr>

                                        <td colspan="2" align="right">
                                            <asp:Button ID="Button1" runat="server" Text="查询"
                                                class="btn btn-large btn-primary"
                                                OnClick="Button1_Click" Width="100px" />
                                        </td>
                                        <td colspan="2">&nbsp;
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
    <div>
        <%--By 月份统计  显示当前年 和前一年的资料--%>
        <div class=" panel panel-info col-md-6 ">
            <div class="panel panel-heading">
                <asp:Label ID="lblMstMonth" runat="server" Text="按【月】统计"></asp:Label>
            </div>
            <div class="panel panel-body" style="  overflow:scroll">
                <div style="float: left">
                    <dx:WebChartControl ID="ChartA"
                        runat="server" CrosshairEnabled="True" Height="200px"
                        Width="600px" PaletteName="Civic">
                    </dx:WebChartControl>

                

                    <asp:GridView ID="gv_month" BorderColor="lightgray" BorderWidth="2px" CssClass="gvHeader th" 
                        runat="server" OnRowDataBound="gv_month_RowDataBound">
                        <EmptyDataTemplate>
                            查无资料
                        </EmptyDataTemplate>
                    </asp:GridView>
                </div>
            </div>
        </div>
        <%--By 年份  当前年 往前推十年--%>
        <div class=" panel panel-info col-md-6 ">
            <div class="panel panel-heading">
                <asp:Label ID="lblMstYear" runat="server" Text="按【年】统计"></asp:Label>
            </div>
            <div class="panel panel-body"  >
                <div style="float: left">
                    <dx:WebChartControl ID="ChartB"
                        runat="server" CrosshairEnabled="True" Height="200px"
                        Width="600px">
                    </dx:WebChartControl>
                    <asp:GridView ID="gv_year" BorderColor="lightgray" BorderWidth="2px" CssClass="gvHeader th" 
                        runat="server" OnRowDataBound="gv_year_RowDataBound">
                    </asp:GridView>
                </div>
            </div>
        </div>
    </div>
    <div class=" col-md-12 ">
        <%--By 客户统计  By Customer--%>
        <div class=" panel panel-info col-md-6 ">
            <div class="panel panel-heading">
                <asp:Label ID="lblCustomer" runat="server" Text="按【客户】统计"></asp:Label>
            </div>
            <div class="panel panel-body" style="  overflow:scroll">
                <div style="float: left">
                    <dx:WebChartControl ID="ChartC"
                        runat="server" CrosshairEnabled="True" Height="200px"
                        Width="600px">
                    </dx:WebChartControl>
                    <asp:GridView ID="gv_customer" BorderColor="lightgray" BorderWidth="2px" CssClass="gvHeader th" 
                        runat="server" OnRowDataBound="gv_customer_RowDataBound">
                    </asp:GridView>
                </div>
            </div>
        </div>
        <%--By 地区统计  By Area--%>
        <div class=" panel panel-info col-md-6 ">
            <div class="panel panel-heading">
                <asp:Label ID="lblArea" runat="server" Text="按【地区】统计"></asp:Label>
            </div>
            <div class="panel panel-body">
                <div style="float: left">
                    <dx:WebChartControl ID="ChartD"
                        runat="server" CrosshairEnabled="True" Height="200px"
                        Width="600px">
                    </dx:WebChartControl>
                    <asp:GridView ID="gv_area" BorderColor="lightgray" BorderWidth="2px" CssClass="gvHeader th" 
                        runat="server" OnRowDataBound="gv_area_RowDataBound">
                    </asp:GridView>
                </div>
            </div>
        </div>
    </div>
    <div class=" col-md-12 ">
        <%--By 材料统计  By Material--%>
        <div class=" panel panel-info col-md-6 ">
            <div class="panel panel-heading">
                <asp:Label ID="lblMaterial" runat="server" Text="按【材料】统计"></asp:Label>
            </div>
            <div class="panel panel-body" style="  overflow:scroll">
                <div style="float: left">
                    <dx:WebChartControl ID="ChartE"
                        runat="server" CrosshairEnabled="True" Height="200px"
                        Width="600px">
                    </dx:WebChartControl>
                    <asp:GridView ID="gv_material" BorderColor="lightgray" BorderWidth="2px" CssClass="gvHeader th" 
                        runat="server" OnRowDataBound="gv_material_RowDataBound">
                    </asp:GridView>
                </div>
                <div>
                </div>
            </div>
        </div>
        <%--By 产品类别统计  By CPLB--%>
        <div class=" panel panel-info col-md-6 ">
            <div class="panel panel-heading">
                <asp:Label ID="lblCPLB" runat="server" Text="按【产品类别】统计"></asp:Label>
            </div>
            <div class="panel panel-body">
                <div style="float: left">
                    <dx:WebChartControl ID="ChartF"
                        runat="server" CrosshairEnabled="True" Height="200px"
                        Width="600px">
                    </dx:WebChartControl>
                    <asp:GridView ID="gv_cplb" BorderColor="lightgray" BorderWidth="2px" CssClass="gvHeader th" 
                        runat="server" OnRowDataBound="gv_cplb_RowDataBound">
                    </asp:GridView>
                </div>
                <div>
                </div>
            </div>
        </div>
    </div>
    <br />
    <div runat="server" id="DIV1" style="margin-left: 5px" class="col-md-12">
       
                        <asp:GridView ID="GridView1" runat="server" CssClass="gvHeader th" 
                            AllowPaging="True"
                            BorderStyle="Solid" 
                            ShowFooter="True"
                            PageSize="100"
                            OnSorting="GridView1_Sorting"
                            OnRowDataBound="GridView1_RowDataBound"
                            OnRowCreated="GridView1_RowCreated" BorderWidth="1px">
                            <PagerSettings FirstPageText="首页" LastPageText="尾页"
                                Mode="NextPreviousFirstLast" NextPageText="下页"
                                PreviousPageText="上页" />
                            <PagerStyle ForeColor="Red" />
                              <EmptyDataTemplate>no data found</EmptyDataTemplate>
                        </asp:GridView>
                        <br />                  
     
    </div>
</asp:Content>



