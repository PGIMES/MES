<%@ Page Title="MES【样件发货统计】" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="YangJianDelay_TJ.aspx.cs" Inherits="YangJianDelay_TJ" EnableViewState="True"
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
    <script src="/Content/js/bootstrap.min.js"></script>
       <link href="../Content/js/plugins/bootstrap-select/css/bootstrap-select.min.css" rel="stylesheet" />
    <script src="../Content/js/plugins/bootstrap-select/js/bootstrap-select.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#mestitle").text("【样件发货统计】");
            $("input[id*='txtmonth']").css("display", "none");
            $("input[id*='txtday']").css("display", "none");
            $("input[id*='txtPerson']").css("display", "none");
            $("input[id*='txtYearOrder']").css("display", "none");
            $("input[id*='txtOrder']").css("display", "none");
            $("a[id*='_LinkBtn']").css("display", "none");

            $('.selectpicker').change(function () {
                $("input[id*='txtDepart']").val($(".selectpicker").val());
            });

            $("a[name='mon']").click(function () {
                $("input[id*='txtmonth']").val(this.textContent);
            })
            $("a[name='day']").click(function () {
                $("input[id*='txtday']").val(this.textContent);
            })
            //发货批次 Lot
            $("a[name='Lot']").click(function () {
                year = $("select[id*='dropYear']").val();
                month = $("select[id*='dropMonth']").val();
                day = $(this).attr("value");
                fac = $("select[id*='dropFac']").val();
                type = $(this).attr("type"); //type=1:计划发货批次 2:计划发货数量 3:按时发货批次 4:未按时发货批次 5:发货及时率

                if (this.id.indexOf("lbtnLotYear") > -1) {//如果为月按钮,则不传day 参数
                    month = day;
                    day = "";
                }
                var title = type.replace("1", "计划发货批次").replace("2", "计划发货数量").replace("3", "按时发货批次").replace("4", "未按时发货批次").replace("5", "发货及时率") + "明细";
                $("[id*=GridViewYear_] td").click();

                layer.open({
                    shade: [0.5, '#000', false],
                    type: 2,
                    offset: '20px',
                    area: ['1200px', '700px'],
                    fix: false, //不固定
                    maxmin: false,
                    title: ['<i class="fa fa-dedent-"></i> ' + title, false],
                    closeBtn: 1,
                    content: 'Yangjian_DelayShip_WASFH_Details.aspx?&year=' + year + '&month=' + month + '&fac=' + fac + '&day=' + day + '&name=' + name + '&type=' + type,
                    end: function () {

                    }
                });
            })
            //完成率
            BindEvents();

            //超时次数统计by Date
            $("a[name='OverTime']").click(function () {
                year = $("select[id*='dropYear']").val();
                month = $("select[id*='dropMonth']").val();
                day = $(this).attr("value");
                fac = $("select[id*='dropFac']").val();
                type = $(this).attr("type"); //type=1:完成(逾时) 2:未完成(逾时) 3:未完成(未逾时)
                name = $(this).attr("value");
                if (this.id.indexOf("lbtnOvertimeMonth") != -1) {//如果为年按钮,则不传 day 参数
                    month = day;//取实际value
                    day = "";
                }
                //$("[id*=GridView][id$='Finish'] td").click();
                var title = type.replace("1", "完成(逾时)").replace("2", "未完成(逾时)").replace("3", "未完成(未逾时)") + "明细";
                layer.open({
                    shade: [0.5, '#000', false],
                    type: 2,
                    offset: '20px',
                    area: ['1200px', '700px'],
                    fix: false, //不固定
                    maxmin: false,
                    title: ['<i class="fa fa-dedent"></i> ' + title, false],
                    closeBtn: 1,
                    content: 'open_DelayDetailByDate.aspx?&year=' + year + '&month=' + month + '&fac=' + fac + '&day=' + day + '&type=' + type,
                    end: function () {
                    }
                });
            })

            $("a[name='order']").click(function () {
                $("input[id*='txtOrder']").val($(this).text());
            })
            $("a[name='yearorder']").click(function () {
                $("input[id*='txtYearOrder']").val($(this).text());
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

        function BindEvents() {
            $("a[name='Person']").click(function () {
                $("input[id*='txtPerson']").val($(this).attr("value"));

                year = $("select[id*='dropYear']").val();
                month = $("select[id*='dropMonth']").val();
                fac = $("select[id*='dropFac']").val();
                personNo = $(this).attr("value");
                name = $(this).text();
                if (this.id.indexOf("lbtnMonth") == -1) {//如果为年按钮,则不传month 参数
                    month = "";
                }

                $("[id*=GridView][id$='Finish'] td").click();
                layer.open({
                    shade: [0.5, '#000', false],
                    type: 2,
                    offset: '20px',
                    area: ['1200px', '700px'],
                    fix: false, //不固定
                    maxmin: false,
                    title: ['<i class="fa fa-dedent"></i> 明细', false],
                    closeBtn: 1,
                    content: 'open_DelayDetailByPerson.aspx?&year=' + year + '&month=' + month + '&fac=' + fac + '&personNo=' + personNo + '&name=' + name,
                    end: function () {

                    }
                });
            })
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="panel panel-info">
        
            <table>
                <tr>
                    <td>年
                    </td>
                    <td>
                        <asp:DropDownList ID="dropYear" runat="server" class="form-control input-s-sm">
                        </asp:DropDownList>
                    </td>
                    <td>月
                    </td>
                    <td>
                        <asp:DropDownList ID="dropMonth" runat="server" AutoPostBack="true" class="form-control input-s-sm"
                            OnSelectedIndexChanged="dropMonth_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>

                    <td>发货工厂
                    </td>
                    <td>
                        <asp:DropDownList ID="dropFac" runat="server" class="form-control input-s-sm">
                            <asp:ListItem Value=""></asp:ListItem>
                            <asp:ListItem Value="100" Text="上海工厂"> </asp:ListItem>
                            <asp:ListItem Value="200" Text="昆山工厂"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td>部门
                    </td>
                    <td>
                        <asp:TextBox ID="txtDepart" class="form-control input-s-sm" runat="server" Style="display: none"></asp:TextBox>
                        <select id="selectDepart" name="selectDepart" class="selectpicker " multiple data-live-search="true" runat="server" style="width: 100px">
                        </select>
                    </td>
                    <td style="width: 300px">
                        <asp:Button ID="btnQuery" runat="server" Text="查询" class="btn btn-lg btn-primary"
                            OnClick="btnQuery_Click" Width="92px" Height="45px" />
                        &nbsp;&nbsp;&nbsp; <asp:LinkButton ID="LinkBtn" runat="server" OnClick="LinkBtn_Click">binding月明细</asp:LinkButton>
                        <asp:LinkButton ID="LinkBtnDays" runat="server" OnClick="LinkBtnDays_Click">binding明细</asp:LinkButton>
                        <asp:LinkButton ID="LinkBtnPerson" runat="server" OnClick="LinkBtnPerson_Click">人员月超时明细</asp:LinkButton>
                        <asp:LinkButton ID="LinkBtnPersonYear" runat="server" OnClick="LinkBtnPersonYear_Click">人员年超时明细</asp:LinkButton>

                        &nbsp;
                        <asp:LinkButton ID="LinkBtnYearOrder" runat="server" OnClick="LinkBtnYearOrder_Click">年排序</asp:LinkButton>
                        <asp:LinkButton ID="LinkBtnMonthOrder" runat="server" OnClick="LinkBtnMonthOrder_Click">月排序</asp:LinkButton>

                        <asp:TextBox ID="txtmonth" runat="server" />
                        <asp:TextBox ID="txtday" runat="server" />
                        <asp:TextBox ID="txtPerson" runat="server" />

                        <asp:TextBox ID="txtYearOrder" runat="server" Text="逾时未完成 desc" />
                        <asp:TextBox ID="txtOrder" runat="server" Text="逾时未完成 desc" />
                    </td>
                </tr>
            </table>
         
    </div>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <%--年--%>
            <div class=" panel panel-info col-lg-5 ">
                <div class="panel panel-heading">
                    <asp:Label ID="lblYear" runat="server" Text="年"></asp:Label>发货统计(月)
                </div>
                <div class="panel panel-body">
                    <div style="float: left">
                        <asp:Chart ID="ChartYearTimes" runat="server" BackColor="#F3DFC1" BackGradientStyle="TopBottom"
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
                                <asp:Series BorderWidth="3" ChartArea="ChartArea1" ChartType="Line" LabelBorderWidth="9"
                                    Legend="Default" LegendText="计划发货批次" MarkerSize="8" MarkerStyle="Circle" Name="计划发货批次"
                                    ShadowOffset="3">
                                </asp:Series>
                                <asp:Series BorderWidth="3" ChartArea="ChartArea1" ChartType="Line" LabelBorderWidth="9"
                                    Legend="Default" LegendText="未按时发货批次" MarkerSize="8" MarkerStyle="Circle" Name="未按时发货批次"
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

                        <asp:GridView ID="GridViewYear" BorderColor="lightgray" runat="server" OnRowCreated="GridViewYear_RowCreated" OnRowDataBound="GridViewYear_RowDataBound">
                            <EmptyDataTemplate>暂无数据</EmptyDataTemplate>
                        </asp:GridView>
                    </div>
                    <div>
                    </div>
                </div>
            </div>

            <%--月--%>
            <div class=" panel panel-info  col-lg-7 ">
                <div class="panel panel-heading">
                    <asp:Label ID="lblMonth" runat="server" Text=""></asp:Label>发货统计(日)
                </div>
                <div class="panel panel-body">

                    <asp:Chart ID="ChartMonth" runat="server" BackColor="#F3DFC1" BackGradientStyle="TopBottom"
                        BorderColor="181, 64, 1" BorderDashStyle="Solid" BorderWidth="2" Height="200px"
                        ImageLocation="~/TempImages/ChartPic_#SEQ(300,3)" ImageType="Png" Palette="none"
                        Width="1000px">
                        <Legends>
                            <asp:Legend BackColor="Transparent" Font="Trebuchet MS, 8.25pt, style=Bold" IsTextAutoFit="False"
                                MaximumAutoSize="20" Name="Default" TitleAlignment="Near">
                            </asp:Legend>
                        </Legends>
                        <BorderSkin SkinStyle="Emboss" />
                        <Series>
                            <asp:Series BorderWidth="3" ChartArea="ChartArea1" ChartType="Line" LabelBorderWidth="9"
                                Legend="Default" LegendText="计划发货批次" MarkerSize="8" MarkerStyle="Circle" Name="计划发货批次"
                                ShadowOffset="3">
                            </asp:Series>
                            <asp:Series BorderWidth="3" ChartArea="ChartArea1" ChartType="Line" LabelBorderWidth="9"
                                Legend="Default" LegendText="未按时发货批次" MarkerSize="8" MarkerStyle="Circle" Name="未按时发货批次"
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
                    <asp:GridView ID="GridViewMonth" runat="server" BorderColor="lightgray" OnRowCreated="GridViewMonth_RowCreated" OnRowDataBound="GridViewMonth_RowDataBound">
                        <EmptyDataTemplate>暂无数据</EmptyDataTemplate>
                    </asp:GridView>
                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>



    <div class=" panel panel-info col-lg-5 ">
        <div class="panel panel-heading">
            <asp:Label ID="lblYearCS" runat="server" Text="年"></asp:Label>逾时次数统计(月)
        </div>
        <div class="panel panel-body">
            <div style="float: left">
                <asp:Chart ID="ChartYearCS" runat="server" BackColor="#F3DFC1" BackGradientStyle="TopBottom"
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
                        <asp:Series BorderWidth="3" ChartArea="ChartArea1" ChartType="Line" LabelBorderWidth="9"
                            Legend="Default" LegendText="完成(逾时)" MarkerSize="8" MarkerStyle="Circle" Name="超时次数"
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

                <asp:GridView ID="GridViewYearCS" BorderColor="lightgray" runat="server" OnRowCreated="GridViewYearCS_RowCreated" EnableViewState="true" OnRowDataBound="GridViewYearCS_RowDataBound">
                    <EmptyDataTemplate>暂无数据</EmptyDataTemplate>
                </asp:GridView>
            </div>
            <div>
            </div>
        </div>
    </div>
    <%--月--%>
    <div class=" panel panel-info  col-lg-7 ">
        <div class="panel panel-heading">
            <asp:Label ID="lblMonthCS" runat="server" Text=""></asp:Label>月逾时次数统计(日)
        </div>
        <div class="panel panel-body">

            <asp:Chart ID="ChartMonthCS" runat="server" BackColor="#F3DFC1" BackGradientStyle="TopBottom"
                BorderColor="181, 64, 1" BorderDashStyle="Solid" BorderWidth="2" Height="200px"
                ImageLocation="~/TempImages/ChartPic_#SEQ(300,3)" ImageType="Png" Palette="none"
                Width="1000px">
                <Legends>
                    <asp:Legend BackColor="Transparent" Font="Trebuchet MS, 8.25pt, style=Bold" IsTextAutoFit="False"
                        MaximumAutoSize="20" Name="Default" TitleAlignment="Near">
                    </asp:Legend>
                </Legends>
                <BorderSkin SkinStyle="Emboss" />
                <Series>
                    <asp:Series BorderWidth="3" ChartArea="ChartArea1" ChartType="Line" LabelBorderWidth="9"
                        Legend="Default" LegendText="完成(逾时)" MarkerSize="8" MarkerStyle="Circle" Name="超时次数"
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
            <asp:GridView ID="GridViewMonthCS" runat="server" BorderColor="lightgray" OnRowCreated="GridViewMonthCS_RowCreated" OnRowDataBound="GridViewMonthCS_RowDataBound">
                <EmptyDataTemplate>暂无数据</EmptyDataTemplate>
            </asp:GridView>
        </div>
    </div>


    <%--完成率年--%>
    <div class=" panel panel-info col-lg-6 ">
        <div class="panel panel-heading">
            <asp:Label ID="lblYearFinish" runat="server" Text="年"></asp:Label>完成率统计
        </div>
        <div class="panel panel-body">
            <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <div style="float: left">
                        <asp:Chart ID="ChartYearTimesFinish" runat="server" BackColor="#F3DFC1" BackGradientStyle="TopBottom"
                            BorderColor="181, 64, 1" BorderDashStyle="Solid" BorderWidth="2" Height="200px"
                            ImageLocation="~/TempImages/ChartPic_#SEQ(300,3)" ImageType="Png" Palette="none"
                            Width="700px" EnableViewState="True" ViewStateMode="Enabled">
                            <Legends>
                                <asp:Legend BackColor="Transparent" Font="Trebuchet MS, 8.25pt, style=Bold" IsTextAutoFit="False"
                                    MaximumAutoSize="20" Name="Default" TitleAlignment="Center" Alignment="Center" Docking="Top">
                                </asp:Legend>
                            </Legends>
                            <BorderSkin SkinStyle="Emboss" />
                            <Series>
                                <asp:Series BorderWidth="3" ChartArea="ChartArea1" ChartType="Line" LabelBorderWidth="9"
                                    Legend="Default" LegendText="完成率" MarkerSize="8" MarkerStyle="Circle" Name="完成率"
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
                        <div style="color: red">说明：完成率=完成数/总任务数；及时率=完成数(及时)/完成数; 点击姓名看详单,点击字段排序</div>
                        <div style="min-height: 250px; overflow-y: auto; max-height: 360px;">
                            <asp:GridView ID="GridViewYearFinish" BorderColor="LightGray" runat="server" OnRowCreated="GridViewYearFinish_RowCreated" AutoGenerateColumns="False" OnRowDataBound="GridViewYearFinish_RowDataBound" OnPageIndexChanging="GridViewYearFinish_PageIndexChanging" AllowPaging="True" ShowFooter="True">
                                <Columns>
                                    <asp:TemplateField HeaderText="责任人">

                                        <ItemTemplate>
                                            <asp:LinkButton ID="lbtnYear" runat="server" Text='<%# Bind("责任人") %>'></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="职称" HeaderText="职称" />
                                    <asp:BoundField DataField="年份" HeaderText="年份" />
                                    <asp:BoundField DataField="总任务数" HeaderText="总任务数" />
                                    <asp:BoundField DataField="完成数" HeaderText="完成数" />
                                    <asp:BoundField DataField="及时完成数" HeaderText="完成数(及时)" HeaderStyle-Width="60px">
                                        <HeaderStyle Width="60px"></HeaderStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="逾时完成" HeaderText="完成数(逾时)" HeaderStyle-Width="60px">
                                        <HeaderStyle Width="60px"></HeaderStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="未逾时未完成" HeaderText="未完成数(未逾时)" HeaderStyle-Width="70px">
                                        <HeaderStyle Width="70px"></HeaderStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="逾时未完成" HeaderText="未完成数(逾时)" HeaderStyle-Width="70px">
                                        <HeaderStyle Width="70px"></HeaderStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="完成率" DataFormatString="{0:P0}" HeaderText="完成率" />
                                    <asp:BoundField DataField="及时率" DataFormatString="{0:P0}" HeaderText="及时率" />
                                    <asp:BoundField DataField="workcode" HeaderText="工号" />
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>

                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
    <%--完成率月--%>
    <div class=" panel panel-info  col-lg-6 ">
        <div class="panel panel-heading">
            <asp:Label ID="lblMonthFinish" runat="server" Text=""></asp:Label>完成率统计
        </div>
        <div class="panel panel-body">
            <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <script type="text/jscript">
                        var prm = Sys.WebForms.PageRequestManager.getInstance();
                        prm.add_endRequest(function () {
                            // re-bind your jquery events here
                            BindEvents();
                        });
                    </script>
                    <asp:Chart ID="ChartMonthFinish" runat="server" BackColor="#F3DFC1" BackGradientStyle="TopBottom"
                        BorderColor="181, 64, 1" BorderDashStyle="Solid" BorderWidth="2" Height="200px"
                        ImageLocation="~/TempImages/ChartPic_#SEQ(300,3)" ImageType="Png" Palette="none"
                        Width="1000px">
                        <Legends>
                            <asp:Legend BackColor="Transparent" Font="Trebuchet MS, 8.25pt, style=Bold" IsTextAutoFit="False"
                                MaximumAutoSize="20" Name="Default" TitleAlignment="Near" Alignment="Center" Docking="Top">
                            </asp:Legend>
                        </Legends>
                        <BorderSkin SkinStyle="Emboss" />
                        <Series>
                            <asp:Series BorderWidth="3" ChartArea="ChartArea1" ChartType="Line" LabelBorderWidth="9"
                                Legend="Default" LegendText="完成率2" MarkerSize="8" MarkerStyle="Circle" Name="完成率"
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
                    <div style="color: red">说明：完成率=完成数/总任务数；及时率=完成数(及时)/完成数; 点击姓名看详单,点击字段排序</div>
                    <div style="min-height: 250px; overflow-y: auto; max-height: 360px;">
                        <asp:GridView ID="GridViewMonthFinish" runat="server" BorderColor="LightGray" OnRowCreated="GridViewMonthFinish_RowCreated" AutoGenerateColumns="False" AllowSorting="True" OnRowDataBound="GridViewMonthFinish_RowDataBound" ViewStateMode="Enabled" AllowPaging="True" ShowFooter="True" OnPageIndexChanging="GridViewMonthFinish_PageIndexChanging">
                            <Columns>
                                <asp:TemplateField HeaderText="责任人">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lbtnMonth" runat="server" Text='<%# Bind("责任人") %>'></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="职称" HeaderText="职称" />
                                <asp:BoundField DataField="月份" HeaderText="月份" />
                                <asp:BoundField DataField="总任务数" HeaderText="总任务数" />
                                <asp:BoundField DataField="完成数" HeaderText="完成数" />
                                <asp:BoundField DataField="及时完成数" HeaderText="完成数(及时)" HeaderStyle-Width="60px">
                                    <HeaderStyle Width="60px"></HeaderStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="逾时完成" HeaderText="完成数(逾时)" HeaderStyle-Width="60px">
                                    <HeaderStyle Width="60px"></HeaderStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="未逾时未完成" HeaderText="未完成数(未逾时)" HeaderStyle-Width="70px">
                                    <HeaderStyle Width="70px"></HeaderStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="逾时未完成" HeaderText="未完成数(逾时)" HeaderStyle-Width="70px">
                                    <HeaderStyle Width="70px"></HeaderStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="完成率" DataFormatString="{0:P0}" HeaderText="完成率" />
                                <asp:BoundField DataField="及时率" DataFormatString="{0:P0}" HeaderText="及时率" />
                                <asp:BoundField DataField="workcode" HeaderText="工号" />
                            </Columns>
                            <SelectedRowStyle BackColor="#FFCC99" />
                            <SortedAscendingCellStyle BackColor="#0099FF" />
                            <SortedAscendingHeaderStyle BackColor="#0099FF" />
                            <SortedDescendingCellStyle BackColor="#0099FF" />
                        </asp:GridView>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>



    <%--人员Delay明细--%>
    <div class=" panel panel-info  col-lg-12 " style="display: none">
        <div class="panel panel-heading">
            <asp:Label ID="Label1" runat="server" Text=""></asp:Label>人员超时明细
        </div>
        <div class="panel panel-body">

            <asp:GridView ID="gvDetail" runat="server" BorderColor="lightgray" OnRowDataBound="gvDetail_RowDataBound">
                <EmptyDataTemplate>暂无数据</EmptyDataTemplate>
            </asp:GridView>
        </div>
    </div>
</asp:Content>
