<%@ Page Title="【项目统计】" Language="C#" MasterPageFile="~/Site.master"
    AutoEventWireup="true" CodeFile="XM_TJ_Report.aspx.cs" Inherits="XM_XM_TJ_Report"  MaintainScrollPositionOnPostback="true" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent"
    runat="Server">
    <style type="text/css">
        .hidden
        {
            display: none;
        }
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
        
        td
        {
            padding-left: 5px;
            padding-right: 5px;
        }
        .auto-style1
        {
            width: 100px;
        }
        .tblCondition td
        {
            white-space: nowrap;
        }
    </style>
    <script src="../Content/js/jquery.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#mestitle").text("【项目统计】");
            $("input[id*='txtmonth']").css("display", "none");
            $("input[id*='txtsale']").css("display", "none");
            $("input[id*='txtCustomer']").css("display", "none");
            $("input[id*='txtbymnth']").css("display", "none");
            $("input[id*='txtDL']").css("display", "none");
            // $("a[id*='_LinkBtn']").css("display", "none");

            //            $("a[name='decrib']").click(function () {

            //                // $("input[id*='txtmonth']").val($(this).attr("month"));
            //                $("input[id*='txtmonth']").val(this.textContent);
            //            })
            ReadyFunction();

            //单元格变色
            //            $("[id*=gv_] td").bind("click", function () {
            //                var row = $(this).parent();
            //                $("[id*=gv_] td").each(function () {
            //                    if ($(this)[0] != row[0]) {
            //                        $("td", this).removeClass("selected_row");
            //                    }
            //                });
            //                $("td", row).each(function () {
            //                    if (!$(this).hasClass("selected_row")) {
            //                        $(this).addClass("selected_row");
            //                    } else {
            //                        $(this).removeClass("selected_row");
            //                    }
            //                });
            //            });

            $("td[allowClick=true]").click(function () {
                $("td").css("background", "");  //其他td为无色
                $(this).css("background", "orange"); //点击变色。
            })

        })//endready

        function ReadyFunction() {
            $("a[name='decrib']").click(function () {
                $("input[id*='txtbymnth']").val($(this).attr("decrib_mnth"));
                $("input[id*='txtmonth']").val($(this).attr("mon"));
            })
            $("a[name='Customer']").click(function () {
                $("input[id*='txtCustomer']").val($(this).attr("customerid"));
            })
            $("a[name='DL']").click(function () {
                $("input[id*='txtDL']").val($(this).attr("DL"));
            })

            $("a[name='sale']").click(function () {

                $("input[id*='txtsale']").val($(this).attr("sale"));
            })
        }

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
                    年度
                </td>
                <td>
                    <asp:DropDownList ID="dropYear" runat="server" class="form-control input-s-sm">
                    </asp:DropDownList>
                </td>
                <td>
                    按
                </td>
                <td>
                    <asp:DropDownList ID="dropengineer" runat="server" class="form-control input-s-sm">
                        <asp:ListItem>产品工程师</asp:ListItem>
                        <asp:ListItem>质量工程师</asp:ListItem>
                        <asp:ListItem>项目工程师</asp:ListItem>
                        <asp:ListItem>压铸工程师</asp:ListItem>
                        <asp:ListItem>模具工程师</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    部门
                </td>
                <td>
                    <asp:DropDownList ID="dropdept" runat="server" class="form-control input-s-sm">
                        <asp:ListItem></asp:ListItem>
                        <asp:ListItem>工程二部</asp:ListItem>
                        <asp:ListItem>工程三部</asp:ListItem>
                        <asp:ListItem>工程一部</asp:ListItem>
                        <asp:ListItem>设备二部</asp:ListItem>
                        <asp:ListItem>生产二部</asp:ListItem>
                        <asp:ListItem>项目管理部</asp:ListItem>
                        <asp:ListItem>压铸技术部</asp:ListItem>
                        <asp:ListItem>质量二部</asp:ListItem>
                        <asp:ListItem>质量一部</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:Button ID="btnQuery" runat="server" Text="查询" class="btn btn-lg btn-primary"
                        OnClick="btnQuery_Click" Width="92px" Height="45px" />
                    &nbsp;&nbsp;&nbsp; &nbsp;
                    <asp:TextBox ID="txtmonth" runat="server" />
                    <asp:TextBox ID="txtsale" runat="server" />
                    <asp:TextBox ID="txtCustomer" runat="server" />
                    <asp:TextBox ID="txtbymnth" runat="server" />
                    <asp:TextBox ID="txtDL" runat="server" />
                </td>
            </tr>
        </table>
    </div>
    <%--按月份统计项目数--%>
    <div  class="col-lg-12 ">
        <div class=" panel panel-info col-lg-6 ">
            <div class="panel panel-heading">
                <asp:Label ID="lblMstS" runat="server" Text="按月份统计"></asp:Label>
            </div>
            <div class="panel panel-body">
                <div style="float: left">
                    <asp:Chart ID="ChartByMonth" runat="server" BackColor="#F3DFC1"
                        BackGradientStyle="TopBottom" BorderColor="181, 64, 1" BorderDashStyle="Solid"
                        BorderWidth="2" Height="200px" ImageLocation="~/TempImages/ChartPic_#SEQ(300,3)"
                        ImageType="Png" Palette="none" Width="650PX">
                        <Legends>
                            <asp:Legend BackColor="Transparent" Font="Trebuchet MS, 8.25pt, style=Bold"
                                IsTextAutoFit="False" MaximumAutoSize="20" Name="Default"
                                TitleAlignment="Center" Docking="Top">
                            </asp:Legend>
                        </Legends>
                        <BorderSkin SkinStyle="Emboss" />
                        <Series>
                            <asp:Series BorderWidth="3" ChartArea="ChartArea1" ChartType="Column"
                                LabelBorderWidth="9" Legend="Default" LegendText="本月新启动"
                                MarkerSize="8" Name="本月新启动">
                            </asp:Series>
                            <asp:Series ChartArea="ChartArea1" Legend="Default" LegendText="本月PPAP"
                                Name="本月PPAP">
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
                                    <MajorGrid LineColor="64, 64, 64, 64" Enabled="False" />
                                </AxisY>
                                <AxisX Interval="1" LineColor="64, 64, 64, 64" Minimum="0.5" Maximum="12.5">
                                    <LabelStyle Font="Trebuchet MS, 8.25pt, style=Bold" />
                                    <MajorGrid LineColor="64, 64, 64, 64" Enabled="False" />
                                </AxisX>
                            </asp:ChartArea>
                        </ChartAreas>
                    </asp:Chart>
                  <asp:Chart ID="C2" runat="server" 
                        BackColor="#F3DFC1" 
                                                                                               
                        BackGradientStyle="TopBottom" BorderColor="181, 64, 1" 
                        BorderDashStyle="Solid" 
                                                                                               
                        BorderWidth="2" 
                        Height="200px" 
                                                                                               
                        ImageLocation="~/TempImages/ChartPic_#SEQ(300,3)" 
                        ImageType="Png" 
                                                                                               
                        Palette="none" Width="160px">
                                                                                               <series>
                                                                                                   <asp:Series BorderWidth="0" ChartArea="ChartArea1" ChartType="Pie" 
                                                                                                       Legend="Default" Name="Series1" >
                                                                                                   </asp:Series>
                                                                                               </series>
                                                                                               <ChartAreas>
                                                                                                   <asp:ChartArea BackColor="64, 165, 191, 228" BackGradientStyle="TopBottom" 
                                                                                                       BackSecondaryColor="White" BorderColor="64, 64, 64, 64" BorderDashStyle="Solid" 
                                                                                                       Name="ChartArea1" ShadowColor="Transparent" BackImageWrapMode="TileFlipY">
                                                                                                       <AxisY LineColor="64, 64, 64, 64" Title="" 
                                                                                                           TitleAlignment="Far">
                                                                                                           <MajorGrid LineColor="64, 64, 64, 64" />
                                                                                                           <LabelStyle Font="Trebuchet MS, 8.25pt, style=Bold" />
                                                                                                       </AxisY>
                                                                                                       <AxisX Interval="1" LineColor="64, 64, 64, 64" 
                                                                                                           TitleAlignment="Far">
                                                                                                           <MajorGrid LineColor="64, 64, 64, 64" />
                                                                                                           <MajorTickMark Enabled="False" LineColor="White" 
                                                                                                               LineDashStyle="NotSet" />
                                                                                                           <LabelStyle Font="Trebuchet MS, 8.25pt, style=Bold" />
                                                                                                       </AxisX>
                                                                                                       <Area3DStyle Inclination="15" IsRightAngleAxes="False" 
                                                                                                           Perspective="10" Rotation="10" WallWidth="0" />
                                                                                                   </asp:ChartArea>
                                                                                               </ChartAreas>
                                                                                               <Legends>
                                                                                                   <asp:Legend BackColor="Transparent" 
                                                                                                       Font="Trebuchet MS, 8.25pt, style=Bold" 
                                                                                                       IsDockedInsideChartArea="False" IsTextAutoFit="False" 
                                                                                                       MaximumAutoSize="40" Name="Default" Docking="Top">
                                                                                                   </asp:Legend>
                                                                                               </Legends>
                                                                                               <Titles>
                                                                                                   <asp:Title Alignment="BottomRight" 
                                                                                                       BackImageAlignment="Bottom" Docking="Bottom" 
                                                                                                       Font="Microsoft Sans Serif, 8.25pt, style=Bold" 
                                                                                                       Name="Title1">
                                                                                                   </asp:Title>
                                                                                               </Titles>
                                                                                               <BorderSkin SkinStyle="Emboss" />
                                                                                           </asp:Chart>
                    <asp:GridView ID="gv" BorderColor="lightgray" runat="server"
                        AutoGenerateColumns="true" PageSize="200" OnRowCreated="gv_RowCreated">
                        <EmptyDataTemplate>
                            查无资料</EmptyDataTemplate>
                    </asp:GridView>
                </div>
            </div>
        </div>
    <%--按客户统计--%>
        <div class=" panel panel-info col-lg-6 ">
            <div class="panel panel-heading">
                <asp:Label ID="Label1" runat="server" Text="按客户统计"></asp:Label>
            </div>
            <div class="panel panel-body">
                <div style="float: left">
                    <asp:Chart ID="Chart1" runat="server" BackColor="#F3DFC1" BackGradientStyle="TopBottom"
                        BorderColor="181, 64, 1" BorderDashStyle="Solid" BorderWidth="2"
                        Height="200px" ImageLocation="~/TempImages/ChartPic_#SEQ(300,3)"
                        ImageType="Png" Palette="none" Width="900px">
                        <Legends>
                            <asp:Legend BackColor="Transparent" Font="Trebuchet MS, 8.25pt, style=Bold"
                                IsTextAutoFit="False" MaximumAutoSize="50" Name="Default"
                                TitleAlignment="Center" Docking="Top" Title="">
                            </asp:Legend>
                        </Legends>
                        <BorderSkin SkinStyle="Emboss" />
         
                           <Series>
                            
                             <asp:Series ChartArea="ChartArea1" Legend="Default" 
                                Name="批产" ChartType="StackedColumn" Color="LightSteelBlue">
                            </asp:Series>
                            <asp:Series ChartArea="ChartArea1" Legend="Default" 
                                Name="爬坡" ChartType="StackedColumn" Color="IndianRed">
                            </asp:Series>
                            <asp:Series ChartArea="ChartArea1" Legend="Default" 
                                Name="开发" ChartType="StackedColumn" 
                                Color="DarkKhaki">
                            </asp:Series>
                            <asp:Series ChartArea="ChartArea1"  Legend="Default"
                            Name="未启动工装"  ChartType="StackedColumn" Color="DodgerBlue">
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
                   
                    <%--
                     <%-- <div style="overflow:scroll; width:600px" id="Div2" >--%>
                    <asp:GridView ID="gv_customer" BorderColor="lightgray" runat="server"
                        AutoGenerateColumns="true" PageSize="200" OnRowCreated="gv_customer_RowCreated"
                        OnRowDataBound="gv_customer_RowDataBound">
                        <EmptyDataTemplate>
                            查无资料</EmptyDataTemplate>
                    </asp:GridView>
                    <%-- </div>--%>
                </div>
            </div>
        </div>
    </div>
    <%--按产品大类统计--%>
    <div class="col-lg-12 ">
        <div class=" panel panel-info col-lg-6 ">
            <div class="panel panel-heading">
                <asp:Label ID="Label2" runat="server" Text="按产品大类统计"></asp:Label>
            </div>
            <div class="panel panel-body">
                <div style="float: left">
                    <asp:Chart ID="Chart2" runat="server" BackColor="#F3DFC1" BackGradientStyle="TopBottom"
                        BorderColor="181, 64, 1" BorderDashStyle="Solid" BorderWidth="2"
                        Height="200px" ImageLocation="~/TempImages/ChartPic_#SEQ(300,3)"
                        ImageType="Png" Palette="none" Width="650px">
                        <Legends>
                            <asp:Legend BackColor="Transparent" Font="Trebuchet MS, 8.25pt, style=Bold"
                                IsTextAutoFit="False" MaximumAutoSize="50" Name="Default"
                                TitleAlignment="Center" Docking="Top" Title="">
                            </asp:Legend>
                        </Legends>
                        <BorderSkin SkinStyle="Emboss" />
         
                           <Series>
                            
                             <asp:Series ChartArea="ChartArea1" Legend="Default" 
                                Name="批产" ChartType="StackedColumn" Color="LightSteelBlue">
                            </asp:Series>
                            <asp:Series ChartArea="ChartArea1" Legend="Default" 
                                Name="爬坡" ChartType="StackedColumn" Color="IndianRed">
                            </asp:Series>
                            <asp:Series ChartArea="ChartArea1" Legend="Default" 
                                Name="开发" ChartType="StackedColumn" 
                                Color="DarkKhaki">
                            </asp:Series>
                            <asp:Series ChartArea="ChartArea1"  Legend="Default"
                            Name="未启动工装"  ChartType="StackedColumn" Color="DodgerBlue">
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
                    <asp:UpdatePanel runat="server" ID="UpdatePanel2" UpdateMode="Conditional">
                        <ContentTemplate>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <asp:GridView ID="gv_dl" BorderColor="lightgray" runat="server"
                        AutoGenerateColumns="true" PageSize="200" OnRowCreated="gv_dl_RowCreated"
                        OnRowDataBound="gv_dl_RowDataBound">
                        <EmptyDataTemplate>
                            查无资料</EmptyDataTemplate>
                    </asp:GridView>
                </div>
            </div>
        </div>
       <%-- 按工程师统计--%>
        <div class=" panel panel-info col-lg-6 ">
            <div class="panel panel-heading">
                <asp:Label ID="Label3" runat="server" Text="按工程师统计"></asp:Label>
            </div>
            <div class="panel panel-body">
                <div style="float: left">
                    <asp:Chart ID="Chart3" runat="server" BackColor="#F3DFC1" BackGradientStyle="TopBottom"
                        BorderColor="181, 64, 1" BorderDashStyle="Solid" BorderWidth="2"
                        Height="200px" ImageLocation="~/TempImages/ChartPic_#SEQ(300,3)"
                        ImageType="Png" Palette="none" Width="900px">
                        <Legends>
                            <asp:Legend BackColor="Transparent" Font="Trebuchet MS, 8.25pt, style=Bold"
                                IsTextAutoFit="False" MaximumAutoSize="50" Name="Default"
                                TitleAlignment="Center" Docking="Top" IsDockedInsideChartArea="False">
                            </asp:Legend>
                        </Legends>
                        <BorderSkin SkinStyle="Emboss" />
                       
                              <Series>
                            
                             <asp:Series ChartArea="ChartArea1" Legend="Default" 
                                Name="批产" ChartType="StackedColumn" Color="LightSteelBlue">
                            </asp:Series>
                            <asp:Series ChartArea="ChartArea1" Legend="Default" 
                                Name="爬坡" ChartType="StackedColumn" Color="IndianRed">
                            </asp:Series>
                            <asp:Series ChartArea="ChartArea1" Legend="Default" 
                                Name="开发" ChartType="StackedColumn" 
                                Color="DarkKhaki">
                            </asp:Series>
                            <asp:Series ChartArea="ChartArea1"  Legend="Default"
                            Name="未启动工装"  ChartType="StackedColumn" Color="DodgerBlue">
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
                    <asp:UpdatePanel runat="server" ID="UpdatePanel3" UpdateMode="Conditional">
                        <ContentTemplate>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <asp:GridView ID="Gv_Product" BorderColor="lightgray" runat="server"
                        AutoGenerateColumns="true" PageSize="200" OnRowCreated="Gv_Product_RowCreated"
                        OnRowDataBound="Gv_Product_RowDataBound">
                        <EmptyDataTemplate>
                            查无资料</EmptyDataTemplate>
                    </asp:GridView>
                </div>
            </div>
        </div>
    </div>
    <%--明细--%>
    <div class="panel panel-info  col-lg-12">
        <asp:UpdatePanel runat="server" ID="UpdatePanel1" UpdateMode="Conditional">
            <ContentTemplate>
                <%--<script type="text/javascript">
                    function load() {
                        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
                    }

                    function EndRequestHandler() {
                        ReadyFunction();

                    }
                </script>--%>
                <div class="panel panel-heading" style="background-color: #d9edf7">
                    <asp:Label ID="lblDays" runat="server" Text="明细" Font-Bold="true" /></div>
                <asp:LinkButton ID="LinkBtn" runat="server" OnClick="LinkBtn_Click">binding月明细</asp:LinkButton>
                <asp:LinkButton ID="LinkSale" runat="server" OnClick="LinkSale_Click">binding销售明细</asp:LinkButton>
                <asp:LinkButton ID="LinkCustomer" runat="server" OnClick="LinkCustomer_Click">binding客户明细</asp:LinkButton>
                <asp:LinkButton ID="LinkDL" runat="server" OnClick="LinkDL_Click">binding产品大类明细</asp:LinkButton>
              
                </div>

                 <asp:GridView ID="gvdetail" BorderColor="LightGray" runat="server" AllowSorting="True" 
                    AutoGenerateColumns="False" OnRowDataBound="gvdetail_RowDataBound" onsorting="gvdetail_Sorting" 
                    OnRowCreated="gvdetail_RowCreated">
                    <Columns>
                        <asp:BoundField HeaderText="序号" />
                        <asp:BoundField DataField="pgi_no" HeaderText="项目号"  SortExpression="pgi_no"/>
                        <asp:BoundField DataField="pn" HeaderText="零件号"  SortExpression="pn" />
                        <asp:BoundField DataField="pn_name" HeaderText="零件名称"   SortExpression="pn_name"/>
                        <asp:BoundField DataField="product_dl" HeaderText="产品类别" SortExpression="product_dl" />
                        <%-- <asp:BoundField DataField="year_num" DataFormatString="{0:N0}"
                            HeaderText="年用量" />--%>
                        <asp:BoundField DataField="year_num" HeaderText="年用量" DataFormatString="{0:N0}" SortExpression="year_num">
                            <FooterStyle HorizontalAlign="Right" />
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="customer" HeaderText="顾客"   SortExpression="customer"/>
                        <asp:BoundField DataField="status" HeaderText="项目状态"  SortExpression="status" />
                        <asp:BoundField DataField="start_date" DataFormatString="{0:yyyy-MM-dd}"
                            HeaderText="启动日期" SortExpression="start_date"  />
                        <asp:BoundField DataField="ppap_date" DataFormatString="{0:yyyy-MM-dd}" SortExpression="ppap_date"
                            HeaderText="PPAP日期" />
                        <asp:BoundField DataField="movedate" DataFormatString="{0:yyyy-MM-dd}" SortExpression="movedate"
                            HeaderText="移交日期" />
                        <asp:BoundField DataField="sop_date" DataFormatString="{0:yyyy-MM-dd}" SortExpression="sop_date"
                            HeaderText="SOP日期" />
                        <asp:BoundField DataField="track_id">
                            <ControlStyle CssClass="hidden" Width="0px" />
                            <FooterStyle CssClass="hidden" Width="0px" />
                            <HeaderStyle CssClass="hidden" Width="0px" />
                            <ItemStyle CssClass="hidden" Width="0px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="product" HeaderText="产品工程师" SortExpression="product" />
                        <asp:BoundField DataField="project" HeaderText="项目工程师" SortExpression="project" />
                        <asp:BoundField DataField="zl" HeaderText="质量工程师"  SortExpression="zl"/>
                        <asp:BoundField DataField="yz" HeaderText="压铸工程师" SortExpression="yz" />
                        <asp:BoundField DataField="moju" HeaderText="模具工程师"   SortExpression="moju"/>
                        <asp:BoundField DataField="bz" HeaderText="包装工程师"   SortExpression="bz"/>
                        <asp:BoundField DataField="sale" HeaderText="销售工程师"   SortExpression="sale"/>
                        <asp:BoundField DataField="project_name" HeaderText="顾客项目"   SortExpression="project_name"/>
                    </Columns>
                </asp:GridView>
            </ContentTemplate>
        </asp:UpdatePanel>  
       
    </div>
</asp:Content>
 
