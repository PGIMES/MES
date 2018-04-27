<%@ Page Title="MES【产品统计】" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="Chanpin_TJ.aspx.cs" Inherits="ChanPin_TJ" EnableViewState="True"
    MaintainScrollPositionOnPostback="true" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
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
    <script src="../Content/js/plugins/layer/layer.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {

            $("#mestitle").text("【产品统计】");
            $("input[id*='txtmonth']").css("display", "none");
            $("input[id*='txtyear']").css("display", "none");
            $("input[id*='txtcondition']").css("display", "none");
            $("input[id*='txttype']").css("display", "none");
            $("input[id*='txttitle']").css("display", "none");
            ReadyFunction();
             
            // A
            $("a[name='A']").click(function () {
               var year = $("select[id*='dropYear']").val();
               var month = $(this).attr("value");//$("input[id*='txtmonth']").val();
               var condition = $("select[id*='dropCondition']").val();
               var type = $(this).attr("type"); //type=1:报价次数;2:报价项目数

                var title = type.replace("A1", "开发中").replace("A3", "生产中").replace("A5", "停产") + "明细";//报价次数
                $("[id*=GridViewYear_] td").click();

                $("input[id*=txtyear]").val(year);
                $("input[id*=txtmonth]").val(month);
                $("input[id*=txtcondition]").val(condition);
                $("input[id*=txttitle]").val(title);
                $("input[id*=txttype]").val(type);
                //layer.open({
                //    shade: [0.5, '#000', false],
                //    type: 2,
                //    offset: '20px',
                //    area: ['1200px', '700px'],
                //    fix: false, //不固定
                //    maxmin: false,
                //    title: ['<i class="fa fa-dedent-"></i> ' + title, false],
                //    closeBtn: 1,
                //    content: 'open_DetailByCondition.aspx?&year=' + year + '&month=' + month + '&condition=' + condition + '&title=' + title + '&type=' + type,
                //    end: function () {

                //    }
                //});
            })
            // B
            $("a[name='B']").click(function () {
                year = $("select[id*='dropYear']").val();
                month = $(this).attr("value");//$("input[id*='txtmonth']").val();
                condition = $("select[id*='dropCondition']").val();
                type = $(this).attr("type"); // 

                var title = type.replace("B1", "开发中").replace("B3", "生产中").replace("B5", "停产").replace("B6", "合计") + "明细";
                $("[id*=GridViewYear_] td").click();

                $("input[id*=txtyear]").val(year);
                $("input[id*=txtmonth]").val(month);
                $("input[id*=txtcondition]").val(condition);
                $("input[id*=txttitle]").val(title);
                $("input[id*=txttype]").val(type);
                //layer.open({
                //    shade: [0.5, '#000', false],
                //    type: 2,
                //    offset: '20px',
                //    area: ['1200px', '700px'],
                //    fix: false, //不固定
                //    maxmin: false,
                //    title: ['<i class="fa fa-dedent-"></i> ' + title, false],
                //    closeBtn: 1,
                //    content: 'open_DetailByCondition.aspx?&year=' + year + '&month=' + month + '&condition=' + condition + '&title=' + title + '&type=' + type,
                //    end: function () {

                //    }
                //});
            })
            // C
            $("a[name='C']").click(function () {
                year = $("select[id*='dropYear']").val();
                month = $(this).attr("value");//$("input[id*='txtmonth']").val();
                condition = $("select[id*='dropCondition']").val();
                type = $(this).attr("type"); // 

                var title = type.replace("C1", "开发中").replace("C3", "生产中").replace("C5", "停产").replace("C6", "合计") + "明细";
                $("[id*=GridViewYear_] td").click();

                $("input[id*=txtyear]").val(year);
                $("input[id*=txtmonth]").val(month);
                $("input[id*=txtcondition]").val(condition);
                $("input[id*=txttitle]").val(title);
                $("input[id*=txttype]").val(type);
                //layer.open({
                //    shade: [0.5, '#000', false],
                //    type: 2,
                //    offset: '20px',
                //    area: ['1200px', '700px'],
                //    fix: false, //不固定
                //    maxmin: false,
                //    title: ['<i class="fa fa-dedent-"></i> ' + title, false],
                //    closeBtn: 1,
                //    content: 'open_DetailByCondition.aspx?&year=' + year + '&month=' + month + '&condition=' + condition + '&title=' + title + '&type=' + type,
                //    end: function () {

                //    }
                //});
            })


            //完成率行变色
            $("[id*=GridView][id$='Finish'] td").bind("click", function () {//input[id][name$='ball']
                var row = $(this).parent();
                $("[id*=GridView][id$='Finish'] tr").each(function () {//[id*=GridView][id$='Finish']表示包含Gridview 以Finish结尾
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
            $("[id*=GridViewYear_] td").bind("click", function () {
                var row = $(this).parent();
                $("[id*=GridViewYear_] td").each(function () {
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
        function ReadyFunction() { $("a[id*='MainContent_LinkBtn']").css("display", "none"); }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="panel panel-info row">
        <div class=" col-lg-7">
            <table>
                <tr>
                    <td>年份:
                    </td>
                    <td>
                        <asp:DropDownList ID="dropYear" runat="server" class="form-control input-s-sm">
                        </asp:DropDownList>
                    </td>

                    <td>生产地点
                    </td>
                    <td>
                        <asp:DropDownList ID="dropCondition" runat="server" class="form-control input-s-sm">
                            <asp:ListItem Value=""></asp:ListItem>
                            <asp:ListItem Value="昆山工厂" Text="昆山工厂"> </asp:ListItem>
                            <asp:ListItem Value="上海工厂" Text="上海工厂"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td></td>
                    <td style="padding-left: 30px">
                        <asp:Button ID="btnQuery" runat="server" Text="查询" class="btn btn-lg btn-primary"
                            OnClick="btnQuery_Click" Width="92px" Height="45px" />
                        <asp:TextBox ID="txtyear" runat="server" />
                        <asp:TextBox ID="txtmonth" runat="server" />
                        <asp:TextBox ID="txtcondition" runat="server" />
                        <asp:TextBox ID="txttitle" runat="server" />
                        <asp:TextBox ID="txttype" runat="server" />
                    </td>
                </tr>
            </table>
        </div>

    </div>
     <div class="col-lg-12">
         <%--D  --%>
        <div class=" panel panel-info col-lg-6 " style="display: block">
            <div class="panel panel-heading">
                <asp:Label ID="lblD" runat="server" Text="年"></asp:Label>当前数量统计                    
            </div>
            <div class="panel panel-body">
                <div style="float: left">
                    <asp:Chart ID="ChartD" runat="server" BackColor="#F3DFC1" BackGradientStyle="TopBottom"
                        BorderColor="181, 64, 1" BorderDashStyle="Solid" BorderWidth="2" Height="200px"
                        ImageLocation="~/TempImages/ChartPic_#SEQ(300,3)" ImageType="Png" Palette="none"
                        Width="700px">
                        <Legends>
                            <asp:Legend BackColor="Transparent" Font="Trebuchet MS, 8pt, style=Bold" IsTextAutoFit="False"
                                MaximumAutoSize="20" Name="Default" TitleAlignment="Center">
                            </asp:Legend>
                        </Legends>
                        <BorderSkin SkinStyle="Emboss" />
                        <Series>
                            <asp:Series BorderWidth="3" ChartArea="ChartArea1" ChartType="Pie" LabelBorderWidth="9"
                                Legend="Default" LegendText="" MarkerSize="8" MarkerStyle="Circle" Name="A1"
                                ShadowOffset="3">
                            </asp:Series>
                            
                        </Series>
                        <ChartAreas>
                            <asp:ChartArea BackColor="64, 165, 191, 228" BackGradientStyle="TopBottom" BackSecondaryColor="White"
                                BorderColor="64, 64, 64, 64" BorderDashStyle="Solid" Name="ChartArea1" ShadowColor="Transparent">
                                <Area3DStyle Inclination="15" IsClustered="False" IsRightAngleAxes="False" Perspective="10"
                                    Rotation="10" WallWidth="0" />
                                <AxisY LineColor="64, 64, 64, 64" TitleAlignment="Near">
                                    <LabelStyle Font="Trebuchet MS, 8.25pt, style=Bold" />
                                    <MajorGrid LineColor="64, 64, 64, 64" />
                                </AxisY>

                                <AxisX Interval="1" LineColor="64, 64, 64, 64">
                                    <LabelStyle Font="Trebuchet MS, 8.25pt, style=Bold" />
                                    <MajorGrid LineColor="64, 64, 64, 64" />
                                </AxisX>

                            </asp:ChartArea>
                        </ChartAreas>
                    </asp:Chart>

                    <asp:GridView ID="GridViewD" BorderColor="lightgray" runat="server"  OnRowCreated="GridViewD_RowCreated" >
                        <EmptyDataTemplate>暂无数据</EmptyDataTemplate>
                    </asp:GridView>
                </div>
                <div>
                </div>
            </div>
        </div>
         <%--E  by Year--%>
        <div class=" panel panel-info col-lg-6 " style="display: block">
            <div class="panel panel-heading">
                <asp:Label ID="LblE" runat="server" Text="年"></asp:Label>逐年统计                    
            </div>
            <div class="panel panel-body">
                <div style="float: left">
                    <asp:Chart ID="ChartE" runat="server" BackColor="#F3DFC1" BackGradientStyle="TopBottom"
                        BorderColor="181, 64, 1" BorderDashStyle="Solid" BorderWidth="2" Height="200px"
                        ImageLocation="~/TempImages/ChartPic_#SEQ(300,3)" ImageType="Png" Palette="none"
                        Width="700px">
                        <Legends>
                            <asp:Legend BackColor="Transparent" Font="Trebuchet MS, 8pt, style=Bold" IsTextAutoFit="False"
                                MaximumAutoSize="20" Name="Default" TitleAlignment="Center">
                            </asp:Legend>
                        </Legends>
                        <BorderSkin SkinStyle="Emboss" />
                        <Series>
                            <asp:Series BorderWidth="3" ChartArea="ChartArea1" ChartType="Line" LabelBorderWidth="9"
                                Legend="Default" LegendText="开发中" MarkerSize="8" MarkerStyle="Circle" Name="A1"
                                ShadowOffset="3">
                            </asp:Series>
                            <asp:Series BorderWidth="3" ChartArea="ChartArea1" ChartType="Line" LabelBorderWidth="9"
                                Legend="Default" LegendText="生产中" MarkerSize="8" MarkerStyle="Circle" Name="A2"
                                ShadowOffset="3">
                            </asp:Series>
                        </Series>
                        <ChartAreas>
                            <asp:ChartArea BackColor="64, 165, 191, 228" BackGradientStyle="TopBottom" BackSecondaryColor="White"
                                BorderColor="64, 64, 64, 64" BorderDashStyle="Solid" Name="ChartArea1" ShadowColor="Transparent">
                                <Area3DStyle Inclination="15" IsClustered="False" IsRightAngleAxes="False" Perspective="10"
                                    Rotation="10" WallWidth="0" />
                                <AxisY LineColor="64, 64, 64, 64" TitleAlignment="Near">
                                    <LabelStyle Font="Trebuchet MS, 8.25pt, style=Bold" />
                                    <MajorGrid LineColor="64, 64, 64, 64" />
                                </AxisY>

                                <AxisX Interval="1" LineColor="64, 64, 64, 64">
                                    <LabelStyle Font="Trebuchet MS, 8.25pt, style=Bold" />
                                    <MajorGrid LineColor="64, 64, 64, 64" />
                                </AxisX>

                            </asp:ChartArea>
                        </ChartAreas>
                    </asp:Chart>

                    <asp:GridView ID="GridViewE" BorderColor="lightgray" runat="server" OnRowCreated="GridViewE_RowCreated" >
                        <EmptyDataTemplate>暂无数据</EmptyDataTemplate>
                    </asp:GridView>
                </div>
                <div>
                </div>
            </div>
        </div>
     </div>
    <div class="col-lg-12">
        <%--A--%>
        <div class=" panel panel-info col-lg-6 " style="display: block">
            <div class="panel panel-heading">
                <asp:Label ID="lblA" runat="server" Text="年"></asp:Label>按月统计                    
            </div>
            <div class="panel panel-body">
                <div style="float: left">
                    <asp:Chart ID="ChartA" runat="server" BackColor="#F3DFC1" BackGradientStyle="TopBottom"
                        BorderColor="181, 64, 1" BorderDashStyle="Solid" BorderWidth="2" Height="200px"
                        ImageLocation="~/TempImages/ChartPic_#SEQ(300,3)" ImageType="Png" Palette="none"
                        Width="700px">
                        <Legends>
                            <asp:Legend BackColor="Transparent" Font="Trebuchet MS, 8pt, style=Bold" IsTextAutoFit="False"
                                MaximumAutoSize="20" Name="Default" TitleAlignment="Center">
                            </asp:Legend>
                        </Legends>
                        <BorderSkin SkinStyle="Emboss" />
                        <Series>
                            <asp:Series BorderWidth="3" ChartArea="ChartArea1" ChartType="Line" LabelBorderWidth="9"
                                Legend="Default" LegendText="开发中" MarkerSize="8" MarkerStyle="Circle" Name="A1"
                                ShadowOffset="3">
                            </asp:Series>
                            <asp:Series BorderWidth="3" ChartArea="ChartArea1" ChartType="Line" LabelBorderWidth="9"
                                Legend="Default" LegendText="生产中" MarkerSize="8" MarkerStyle="Circle" Name="A2"
                                ShadowOffset="3">
                            </asp:Series>
                        </Series>
                        <ChartAreas>
                            <asp:ChartArea BackColor="64, 165, 191, 228" BackGradientStyle="TopBottom" BackSecondaryColor="White"
                                BorderColor="64, 64, 64, 64" BorderDashStyle="Solid" Name="ChartArea1" ShadowColor="Transparent">
                                <Area3DStyle Inclination="15" IsClustered="False" IsRightAngleAxes="False" Perspective="10"
                                    Rotation="10" WallWidth="0" />
                                <AxisY LineColor="64, 64, 64, 64" TitleAlignment="Near">
                                    <LabelStyle Font="Trebuchet MS, 8.25pt, style=Bold" />
                                    <MajorGrid LineColor="64, 64, 64, 64" />
                                </AxisY>

                                <AxisX Interval="1" LineColor="64, 64, 64, 64">
                                    <LabelStyle Font="Trebuchet MS, 8.25pt, style=Bold" />
                                    <MajorGrid LineColor="64, 64, 64, 64" />
                                </AxisX>

                            </asp:ChartArea>
                        </ChartAreas>
                    </asp:Chart>

                    <asp:GridView ID="GridViewA" BorderColor="lightgray" runat="server" OnRowCreated="GridViewA_RowCreated" OnRowDataBound="GridViewA_RowDataBound">
                        <EmptyDataTemplate>暂无数据</EmptyDataTemplate>
                    </asp:GridView>
                </div>
                <div>
                </div>
            </div>
        </div>
        <%--C--%>

        <div class=" panel panel-info col-lg-6 " style="display: block">
            <div class="panel panel-heading">
                <asp:Label ID="lblC" runat="server" Text="年"></asp:Label>按产品类别统计
            </div>
            <div class="panel panel-body">
                <div style="float: left">
                    <asp:Chart ID="ChartC" runat="server" BackColor="#F3DFC1" BackGradientStyle="TopBottom"
                        BorderColor="181, 64, 1" BorderDashStyle="Solid" BorderWidth="2" Height="200px"
                        ImageLocation="~/TempImages/ChartPic_#SEQ(300,3)" ImageType="Png" Palette="none"
                        Width="700px">
                        <Legends>
                            <asp:Legend BackColor="Transparent" Font="Trebuchet MS, 8.25pt, style=Bold" IsTextAutoFit="False"
                                MaximumAutoSize="20" Name="Default" TitleAlignment="Center">
                            </asp:Legend>
                        </Legends>
                        <BorderSkin SkinStyle="Emboss" />
                        <Series>
                            <asp:Series BorderWidth="3" ChartArea="ChartArea1" ChartType="StackedColumn" LabelBorderWidth="9"
                                Legend="Default" LegendText="开发中" MarkerSize="8" MarkerStyle="Circle" Name="C1"
                                ShadowOffset="3">
                            </asp:Series>
                        </Series>
                        <Series>
                            <asp:Series BorderWidth="3" ChartArea="ChartArea1" ChartType="StackedColumn" LabelBorderWidth="9"
                                Legend="Default" LegendText="生产中" MarkerSize="8" MarkerStyle="Circle" Name="C2"
                                ShadowOffset="3">
                            </asp:Series>
                        </Series>
                        <ChartAreas>
                            <asp:ChartArea BackColor="64, 165, 191, 228" BackGradientStyle="TopBottom" BackSecondaryColor="White"
                                BorderColor="64, 64, 64, 64" BorderDashStyle="Solid" Name="ChartArea1" ShadowColor="Transparent">
                                <Area3DStyle Inclination="15" IsClustered="False" IsRightAngleAxes="False" Perspective="10"
                                    Rotation="10" WallWidth="0" />
                                <AxisY LineColor="64, 64, 64, 64" TitleAlignment="Far">
                                    <LabelStyle Font="Trebuchet MS, 8.25pt, style=Bold" />
                                    <MajorGrid LineColor="64, 64, 64, 64" />
                                </AxisY>

                                <AxisX Interval="1" LineColor="64, 64, 64, 64">
                                    <LabelStyle Font="Trebuchet MS, 8.25pt, style=Bold" />
                                    <MajorGrid LineColor="64, 64, 64, 64" />
                                </AxisX>
                            </asp:ChartArea>
                        </ChartAreas>
                    </asp:Chart>

                    <asp:GridView ID="GridViewC" BorderColor="lightgray" runat="server" OnRowCreated="GridViewC_RowCreated" EnableViewState="true" OnRowDataBound="GridViewC_RowDataBound">
                        <EmptyDataTemplate>暂无数据</EmptyDataTemplate>
                    </asp:GridView>
                </div>
                <div>
                </div>
            </div>
        </div>
        <%--B--%>
        <div class=" panel panel-info  col-lg-12 " style="display: block">
            <div class="panel panel-heading">
                <asp:Label ID="lblB" runat="server" Text=""></asp:Label>按客户统计
            </div>
            <div class="panel panel-body">

                <asp:Chart ID="ChartB" runat="server" BackColor="#F3DFC1" BackGradientStyle="TopBottom"
                    BorderColor="181, 64, 1" BorderDashStyle="Solid" BorderWidth="2" Height="200px"
                    ImageLocation="~/TempImages/ChartPic_#SEQ(300,3)" ImageType="Png" Palette="none"
                    Width="700px">
                    <Legends>
                        <asp:Legend BackColor="Transparent" Font="Trebuchet MS, 8.25pt, style=Bold" IsTextAutoFit="False"
                            MaximumAutoSize="20" Name="Default" TitleAlignment="Near">
                        </asp:Legend>
                    </Legends>
                    <BorderSkin SkinStyle="Emboss" />
                    <Series>
                        <asp:Series BorderWidth="3" ChartArea="ChartArea1" ChartType="StackedColumn" LabelBorderWidth="9"
                            Legend="Default" LegendText="开发中" MarkerSize="8" MarkerStyle="Circle" Name="B1"
                            ShadowOffset="3">
                        </asp:Series>
                    </Series>
                    <Series>
                        <asp:Series BorderWidth="3" ChartArea="ChartArea1" ChartType="StackedColumn" LabelBorderWidth="9"
                            Legend="Default" LegendText="生产中" MarkerSize="8" MarkerStyle="Circle" Name="B2"
                            ShadowOffset="3">
                        </asp:Series>
                    </Series>
                    <ChartAreas>
                        <asp:ChartArea BackColor="64, 165, 191, 228" BackGradientStyle="TopBottom" BackSecondaryColor="White"
                            BorderColor="64, 64, 64, 64" BorderDashStyle="Solid" Name="ChartArea1" ShadowColor="Transparent">
                            <Area3DStyle Inclination="15" IsClustered="False" IsRightAngleAxes="False" Perspective="10"
                                Rotation="10" WallWidth="0" />
                            <AxisY LineColor="64, 64, 64, 64" TitleAlignment="Far">
                                <LabelStyle Font="Trebuchet MS, 8.25pt, style=Bold" />
                                <MajorGrid LineColor="64, 64, 64, 64" />
                            </AxisY>
                            <AxisX Interval="1" LineColor="64, 64, 64, 64">
                                <LabelStyle Font="Trebuchet MS, 8.25pt, style=Bold" />
                                <MajorGrid LineColor="64, 64, 64, 64" />
                            </AxisX>
                        </asp:ChartArea>
                    </ChartAreas>
                </asp:Chart>
                <asp:GridView ID="GridViewB" runat="server" BorderColor="lightgray" OnRowCreated="GridViewB_RowCreated" OnRowDataBound="GridViewB_RowDataBound">
                    <EmptyDataTemplate>暂无数据</EmptyDataTemplate>
                </asp:GridView>
            </div>
        </div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <script type="text/javascript">
                    function load() {
                        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
                    }

                    function EndRequestHandler() {
                        ReadyFunction();
                       // $("[id*='LinkBtn']").css("display", "none");
                       
                    }
                </script>
                <div class="panel panel-heading" style="background-color: #d9edf7">
                    <asp:Label ID="lblName" runat="server" Text="数量明细" Font-Bold="true" />
                </div>

                <asp:LinkButton ID="LinkBtn" runat="server" OnClick="LinkBtn_Click" >binding明细</asp:LinkButton>

                <asp:GridView ID="gvdetail" BorderColor="LightGray" runat="server"
                    AutoGenerateColumns="true"
                    OnRowDataBound="gvdetail_RowDataBound" ShowFooter="false">
                </asp:GridView>

                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>


</asp:Content>
