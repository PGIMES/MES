<%@ Page Title="年销售预测" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="chanpin_saleforecast.aspx.cs" Inherits="Product_chanpin_saleforecast" %>

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
            $("#mestitle").text("【年销售预测】");
          
            // $("a[id*='_LinkBtn']").css("display", "none");

            //            $("a[name='decrib']").click(function () {

            //                // $("input[id*='txtmonth']").val($(this).attr("month"));
            //                $("input[id*='txtmonth']").val(this.textContent);
            //            })
           // ReadyFunction();

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
                    年度：
                </td>
                <td>
                    <asp:DropDownList ID="dropYear" runat="server" class="form-control input-s-sm">
                    </asp:DropDownList>
                </td>
                <td>
                    生产地点：
                </td>
                <td>
                   

                    <asp:DropDownList ID="dropsite" runat="server" class="form-control input-s-md " Width="120px">
                                    </asp:DropDownList>

                </td>
                <td>
                    统计项：
                </td>
                <td>
                    <asp:DropDownList ID="droptype" runat="server" class="form-control input-s-sm">
                        <asp:ListItem Value="1">销售金额</asp:ListItem>
                        <asp:ListItem Value="0">销售数量</asp:ListItem>
                        <asp:ListItem Value="2">金额占比</asp:ListItem>
                        
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:Button ID="btnQuery" runat="server" Text="查询" class="btn btn-lg btn-primary"
                        OnClick="btnQuery_Click" Width="92px" Height="45px" />
                  
                   
                </td>
                  <td><asp:Button ID="btn_export"  runat="server" Text="导出" 
                        class="btn btn-lg btn-primary"  Width="92px" Height="45px" 
                        onclick="btn_export_Click" /> </td>
            </tr>
        </table>
    </div>
    <%--按年度统计销售预测--%>
    <div  class="col-lg-12 ">
        <div class=" panel panel-info col-lg-5 ">
            <div class="panel panel-heading">
                <asp:Label ID="lblMstS" runat="server" Text="按年统计"></asp:Label>
            </div>
            <div class="panel panel-body">
                <div style="float: left">
                    <asp:Chart ID="Chart_ByYear" runat="server" BackColor="#F3DFC1"
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
                         


                           
                            <asp:Series ChartArea="ChartArea1" Legend="Default" LegendText="生产中"
                                Name="生产中" ChartType="StackedColumn">
                            </asp:Series>
                            <asp:Series BorderWidth="3" ChartArea="ChartArea1" ChartType="StackedColumn"
                                LabelBorderWidth="9" Legend="Default" LegendText="开发中"
                                MarkerSize="8" Name="开发中">
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
                                <AxisX Interval="1" LineColor="64, 64, 64, 64" >
                                    <LabelStyle Font="Trebuchet MS, 8.25pt, style=Bold" />
                                    <MajorGrid LineColor="64, 64, 64, 64" Enabled="False" />
                                </AxisX>
                            </asp:ChartArea>
                        </ChartAreas>
                    </asp:Chart>
                    <asp:GridView ID="gv_year" BorderColor="lightgray" runat="server"  OnRowDataBound="gv_year_RowDataBound"
                        AutoGenerateColumns="true" PageSize="200" OnRowCreated="gv_year_RowCreated">
                        <EmptyDataTemplate>
                            查无资料</EmptyDataTemplate>
                    </asp:GridView>
                </div>
            </div>
        </div>
    <%--按月份统计--%>
        <div class=" panel panel-info col-lg-7 ">
            <div class="panel panel-heading">
                <asp:Label ID="Label1" runat="server" Text="按地区统计"></asp:Label>
            </div>
            <div class="panel panel-body">
                <div style="float: left">
                    <asp:Chart ID="Chart_ByMonth" runat="server" BackColor="#F3DFC1" BackGradientStyle="TopBottom"
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
                            <asp:Series BorderWidth="3" ChartArea="ChartArea1" ChartType="Line"
                                LabelBorderWidth="9" Legend="Default" LegendText="T1" MarkerSize="8"
                                MarkerStyle="Circle" Name="T1">
                            </asp:Series>
                            <asp:Series BorderWidth="3" ChartArea="ChartArea1" ChartType="Line"
                                LabelBorderWidth="9" Legend="Default" LegendText="T2" MarkerSize="8"
                                MarkerStyle="Circle" Name="T2">
                            </asp:Series>
                            <asp:Series BorderWidth="3" ChartArea="ChartArea1" ChartType="Line"
                                LabelBorderWidth="9" Legend="Default" LegendText="T3" MarkerSize="8"
                                MarkerStyle="Circle" Name="T3">
                            </asp:Series>
                            <asp:Series BorderWidth="3" ChartArea="ChartArea1" ChartType="Line"
                                LabelBorderWidth="9" Legend="Default" LegendText="T4" MarkerSize="8"
                                MarkerStyle="Circle" Name="T4">
                            </asp:Series>
                            <asp:Series BorderWidth="3" ChartArea="ChartArea1" ChartType="Line"
                                LabelBorderWidth="9" Legend="Default" LegendText="T5" MarkerSize="8"
                                MarkerStyle="Circle" Name="T5">
                            </asp:Series>
                            <asp:Series BorderWidth="3" ChartArea="ChartArea1" ChartType="Line"
                                LabelBorderWidth="9" Legend="Default" LegendText="T6" MarkerSize="8"
                                MarkerStyle="Circle" Name="T6">
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
                    <asp:GridView ID="gv_month" BorderColor="lightgray" runat="server"
                        AutoGenerateColumns="true" PageSize="200" OnRowCreated="gv_month_RowCreated"
                        OnRowDataBound="gv_month_RowDataBound">
                        <EmptyDataTemplate>
                            查无资料</EmptyDataTemplate>
                    </asp:GridView>
                    <%-- </div>--%>
                </div>
            </div>
        </div>
    </div>
    <%--按客户统计--%>
    <div class="col-lg-12 ">
        <div class=" panel panel-info col-lg-5 ">
            <div class="panel panel-heading">
                <asp:Label ID="Label2" runat="server" Text="按客户统计Top5"></asp:Label>
            </div>
            <div class="panel panel-body">
                <div style="float: left">
                    <asp:Chart ID="Chart_KH" runat="server" BackColor="#F3DFC1" BackGradientStyle="TopBottom"
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
                            <asp:Series BorderWidth="3" ChartArea="ChartArea1" ChartType="Line"
                                LabelBorderWidth="9" Legend="Default" LegendText="T1" MarkerSize="8"
                                MarkerStyle="Circle" Name="T1">
                            </asp:Series>
                            <asp:Series BorderWidth="3" ChartArea="ChartArea1" ChartType="Line"
                                LabelBorderWidth="9" Legend="Default" LegendText="T2" MarkerSize="8"
                                MarkerStyle="Circle" Name="T2">
                            </asp:Series>
                            <asp:Series BorderWidth="3" ChartArea="ChartArea1" ChartType="Line"
                                LabelBorderWidth="9" Legend="Default" LegendText="T3" MarkerSize="8"
                                MarkerStyle="Circle" Name="T3">
                            </asp:Series>
                            <asp:Series BorderWidth="3" ChartArea="ChartArea1" ChartType="Line"
                                LabelBorderWidth="9" Legend="Default" LegendText="T4" MarkerSize="8"
                                MarkerStyle="Circle" Name="T4">
                            </asp:Series>
                            <asp:Series BorderWidth="3" ChartArea="ChartArea1" ChartType="Line"
                                LabelBorderWidth="9" Legend="Default" LegendText="T5" MarkerSize="8"
                                MarkerStyle="Circle" Name="T5">
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

                    <asp:GridView ID="gv_KH" BorderColor="lightgray" runat="server"
                        AutoGenerateColumns="true" PageSize="200" OnRowCreated="gv_KH_RowCreated"
                        OnRowDataBound="gv_KH_RowDataBound">
                        <EmptyDataTemplate>
                            查无资料</EmptyDataTemplate>
                    </asp:GridView>
                </div>
            </div>
        </div>
       <%-- 按产品大类统计--%>
        <div class=" panel panel-info col-lg-7 ">
            <div class="panel panel-heading">
                <asp:Label ID="Label3" runat="server" Text="按产品大类Top5"></asp:Label>
            </div>
            <div class="panel panel-body">
                <div style="float: left">
                    <asp:Chart ID="Chartdl" runat="server" BackColor="#F3DFC1" BackGradientStyle="TopBottom"
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
                            <asp:Series BorderWidth="3" ChartArea="ChartArea1" ChartType="Line"
                                LabelBorderWidth="9" Legend="Default" LegendText="T1" MarkerSize="8"
                                MarkerStyle="Circle" Name="T1">
                            </asp:Series>
                            <asp:Series BorderWidth="3" ChartArea="ChartArea1" ChartType="Line"
                                LabelBorderWidth="9" Legend="Default" LegendText="T2" MarkerSize="8"
                                MarkerStyle="Circle" Name="T2">
                            </asp:Series>
                            <asp:Series BorderWidth="3" ChartArea="ChartArea1" ChartType="Line"
                                LabelBorderWidth="9" Legend="Default" LegendText="T3" MarkerSize="8"
                                MarkerStyle="Circle" Name="T3">
                            </asp:Series>
                            <asp:Series BorderWidth="3" ChartArea="ChartArea1" ChartType="Line"
                                LabelBorderWidth="9" Legend="Default" LegendText="T4" MarkerSize="8"
                                MarkerStyle="Circle" Name="T4">
                            </asp:Series>
                            <asp:Series BorderWidth="3" ChartArea="ChartArea1" ChartType="Line"
                                LabelBorderWidth="9" Legend="Default" LegendText="T5" MarkerSize="8"
                                MarkerStyle="Circle" Name="T5">
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
                    <asp:GridView ID="Gv_DL" BorderColor="lightgray" runat="server"
                        AutoGenerateColumns="true" PageSize="200" OnRowCreated="Gv_DL_RowCreated"
                        OnRowDataBound="Gv_DL_RowDataBound">
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
               <%-- <asp:LinkButton ID="LinkBtn" runat="server" OnClick="LinkBtn_Click">binding月明细</asp:LinkButton>
                <asp:LinkButton ID="LinkSale" runat="server" OnClick="LinkSale_Click">binding销售明细</asp:LinkButton>
                <asp:LinkButton ID="LinkCustomer" runat="server" OnClick="LinkCustomer_Click">binding客户明细</asp:LinkButton>
                <asp:LinkButton ID="LinkDL" runat="server" OnClick="LinkDL_Click">binding产品大类明细</asp:LinkButton>--%>
              
                </div>

                 <asp:GridView ID="GridView2" runat="server" CssClass="GridView2"  AllowPaging="true"
                        AutoGenerateColumns="true" BorderColor="LightGray" HeaderStyle-BackColor="LightBlue" OnRowDataBound="gv2_RowDataBound" OnPageIndexChanging="GridView2_PageIndexChanging" ShowFooter="true">
                        <EmptyDataTemplate>no data found</EmptyDataTemplate>
                        <PagerSettings FirstPageText="首页" LastPageText="尾页" Mode="NextPreviousFirstLast" NextPageText="下页" PreviousPageText="上页" />
                        
                    </asp:GridView>
                    <%--按占比统计--%> 
                     <asp:GridView ID="GridView1" runat="server" CssClass="GridView2"   AllowPaging="true"
                        AutoGenerateColumns="true" BorderColor="LightGray" HeaderStyle-BackColor="LightBlue"  ShowFooter="true" OnRowDataBound="gv1_RowDataBound">
                        <EmptyDataTemplate>no data found</EmptyDataTemplate>
                        <PagerSettings FirstPageText="首页" LastPageText="尾页" Mode="NextPreviousFirstLast" NextPageText="下页" PreviousPageText="上页" />
                        
                    </asp:GridView>

                    <%--按金额统计--%> 
                      <asp:GridView ID="GridView3" runat="server" CssClass="GridView3"  AllowPaging="true"
                                AutoGenerateColumns="true" BorderColor="LightGray" HeaderStyle-BackColor="LightBlue" OnRowDataBound="gv3_RowDataBound" OnPageIndexChanging="GridView3_PageIndexChanging" ShowFooter="true" AllowSorting="true" OnSorting="GridView3_Sorting" OnRowCreated="GridView3_RowCreated" Width="2200px">
                                <EmptyDataTemplate>no data found</EmptyDataTemplate>
                                <PagerSettings FirstPageText="首页" LastPageText="尾页" Mode="NextPreviousFirstLast" NextPageText="下页" PreviousPageText="上页" />
                               
                            </asp:GridView>


            </ContentTemplate>
        </asp:UpdatePanel>  
       
    </div>
</asp:Content>
 

