<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master"
    AutoEventWireup="true" CodeFile="DingDian_Report.aspx.cs"
    Inherits="BaoJia_DingDian_Report" EnableViewState="True"
    MaintainScrollPositionOnPostback="true" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent"
    runat="Server">
    <style type="text/css">
        td
        {
            padding-left: 5px;
            padding-right: 5px;
            padding-top: 5px;
        }
        th
        {
            text-align: center;
            padding-left: 5px;
            padding-right: 5px;
        }
        .panel
        {
            margin-bottom: 5px;
        }
        .panel-heading
        {
            padding: 5px 5px 5px 5px;
        }
        .panel-body
        {
            padding: 5px 5px 5px 5px;
        }
        body
        {
            margin-left: 5px;
            margin-right: 5px;
        }
        .col-lg-1, .col-lg-10, .col-lg-11, .col-lg-12, .col-lg-2, .col-lg-3, .col-lg-4, .col-lg-5, .col-lg-6, .col-lg-7, .col-lg-8, .col-lg-9, .col-md-1, .col-md-10, .col-md-11, .col-md-12, .col-md-2, .col-md-3, .col-md-4, .col-md-5, .col-md-6, .col-md-7, .col-md-8, .col-md-9, .col-sm-1, .col-sm-10, .col-sm-11, .col-sm-12, .col-sm-2, .col-sm-3, .col-sm-4, .col-sm-5, .col-sm-6, .col-sm-7, .col-sm-8, .col-sm-9, .col-xs-1, .col-xs-10, .col-xs-11, .col-xs-12, .col-xs-2, .col-xs-3, .col-xs-4, .col-xs-5, .col-xs-6, .col-xs-7, .col-xs-8, .col-xs-9
        {
            position: relative;
            min-height: 1px;
            padding-right: 1px;
            padding-left: 1px;
            margin-top: 0px;
        }
        .selected_row
        {
            background-color: #A1DCF2;
        }
    </style>
    <script src="../Content/js/jquery.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#mestitle").text("【定点项目分析】");
            $("input[id*='txtmonth']").css("display", "none");

            // $("a[id*='_LinkBtn']").css("display", "none");

            $("a[name='month']").click(function () {

                $("input[id*='txtmonth']").val($(this).attr("month"));
            })

            //单元格变色
            $("[id*=gv_] td").bind("click", function () {
                var row = $(this).parent();
                $("[id*=gv_] td").each(function () {
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

            $("td[allowClick=true]").click(function () {
                $("td").css("background", "");  //其他td为无色
                $(this).css("background", "orange"); //点击变色。
            })

        })//endready
       

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent"
    runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="panel panel-info">
        <table>
            <tr>
                <td>
                    定点日期
                </td>
                <td>
                    <asp:DropDownList ID="dropYear" runat="server" class="form-control input-s-sm">
                    </asp:DropDownList>
                </td>
                <td>
                    分析纬度
                </td>
                <td>
                    <asp:DropDownList ID="dropMonth" runat="server" class="form-control input-s-sm"
                        A>
                        <asp:ListItem Value="0">月份</asp:ListItem>
                        <asp:ListItem Value="1">销售人员</asp:ListItem>
                        <asp:ListItem Value="2">客户</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:Button ID="btnQuery" runat="server" Text="查询" class="btn btn-lg btn-primary"
                        OnClick="btnQuery_Click" Width="92px" Height="45px" />
                    &nbsp;&nbsp;&nbsp; &nbsp;
                    <asp:TextBox ID="txtmonth" runat="server" />
                </td>
            </tr>
        </table>
    </div>
    <%--年--%>
    <div class=" panel panel-info col-lg-12 ">
        <div class="panel panel-heading">
            <asp:Label ID="lblYear" runat="server" Text=" 销售额"></asp:Label>
        </div>
        <div class="panel panel-body">
            <div style="float: left">
                <asp:Chart ID="ChartByMonth" runat="server" BackColor="#F3DFC1"
                    BackGradientStyle="TopBottom" BorderColor="181, 64, 1" BorderDashStyle="Solid"
                    BorderWidth="2" Height="200px" ImageLocation="~/TempImages/ChartPic_#SEQ(300,3)"
                    ImageType="Png" Palette="none" Width="1300px">
                    <Legends>
                        <asp:Legend BackColor="Transparent" Font="Trebuchet MS, 8.25pt, style=Bold"
                            IsTextAutoFit="False" MaximumAutoSize="20" Name="Default"
                            TitleAlignment="Center">
                        </asp:Legend>
                    </Legends>
                    <BorderSkin SkinStyle="Emboss" />
                    <Series>
                        <asp:Series BorderWidth="3" ChartArea="ChartArea1" LabelBorderWidth="9"
                            Legend="Default" LegendText="月份" MarkerSize="8" Name="年销售额"
                            ShadowOffset="3">
                        </asp:Series>
                    </Series>
                    <ChartAreas>
                        <asp:ChartArea BackColor="64, 165, 191, 228" BackGradientStyle="TopBottom"
                            BackSecondaryColor="White" BorderColor="64, 64, 64, 64" BorderDashStyle="Solid"
                            Name="ChartArea1" ShadowColor="Transparent">
                            <Area3DStyle Inclination="15" IsClustered="False" IsRightAngleAxes="False"
                                Perspective="10" Rotation="10" WallWidth="0" />
                            <AxisY LineColor="64, 64, 64, 64" TitleAlignment="Far">
                                <LabelStyle Font="Trebuchet MS, 8.25pt, style=Bold" />
                                <MajorGrid LineColor="64, 64, 64, 64" />
                            </AxisY>
                            <AxisY2>
                                <LabelStyle Font="Trebuchet MS, 18.25pt, style=Bold" />
                                <MajorGrid LineColor="4, 64, 4, 64" />
                            </AxisY2>
                            <AxisX Interval="1" LineColor="64, 64, 64, 64">
                                <LabelStyle Font="Trebuchet MS, 8.25pt, style=Bold" />
                                <MajorGrid LineColor="64, 64, 64, 64" />
                            </AxisX>
                        </asp:ChartArea>
                    </ChartAreas>
                </asp:Chart>
                <asp:UpdatePanel runat="server" ID="p1" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:GridView ID="gv" BorderColor="lightgray" runat="server"
                            AutoGenerateColumns="true" PageSize="200" OnRowCreated="gv_RowCreated">
                            <EmptyDataTemplate>
                                查无资料</EmptyDataTemplate>
                        </asp:GridView>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <div>
            </div>
        </div>
    </div>
    <%--日--%>
    <div class="panel panel-info  col-lg-12">
        <asp:UpdatePanel runat="server" ID="UpdatePanel1" UpdateMode="Conditional">
            <ContentTemplate>
            <div class="panel panel-heading" style="background-color:#d9edf7">
            <asp:Label ID="lblDays" runat="server" Text="定点项目"  Font-Bold="true"/></div>
        
                <asp:LinkButton ID="LinkBtn" runat="server" OnClick="LinkBtn_Click">binding月明细</asp:LinkButton>
                <asp:GridView ID="gvdetail" BorderColor="LightGray" runat="server"
                    AutoGenerateColumns="False" OnRowDataBound="gvdetail_RowDataBound"
                    OnRowCreated="gvdetail_RowCreated" ShowFooter="True">
                    <Columns>
                        <asp:BoundField HeaderText="序号" />
                        <asp:BoundField DataField="dingdian_date" HeaderText="定点日期" DataFormatString="{0:yyyy-MM-dd}" />
                        <asp:BoundField DataField="baojia_no" HeaderText="报价号" />
                        <asp:BoundField DataField="baojia_start_date" DataFormatString="{0:yyyy-MM-dd HH:mm}"
                            HeaderText="项目开始日期" />
                        <asp:BoundField DataField="customer_name" HeaderText="客户" />
                        <asp:BoundField DataField="customer_project" HeaderText="项目" />
                        <asp:BoundField DataField="ljh" HeaderText="零件号" />
                        <asp:BoundField DataField="lj_name" HeaderText="零件名称" />
                        <asp:BoundField DataField="quantity_year" HeaderText="年用量">
                            <FooterStyle HorizontalAlign="Right" />
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="yj_per_price" HeaderText="单价">
                            <FooterStyle HorizontalAlign="Right" />
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="price_year" HeaderText="年销售额">
                            <FooterStyle HorizontalAlign="Right" />
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="hetong_complet_date" HeaderText="合同完成日期"
                            DataFormatString="{0:yyyy-MM-dd}" />
                        <asp:BoundField DataField="sales_name" HeaderText="销售负责" />
                    </Columns>
                    <EmptyDataTemplate>
                        查无资料</EmptyDataTemplate>
                </asp:GridView>
           
    </div> </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
