<%@ Page Title="【光谱成分统计】" Language="C#" MasterPageFile="~/Site.master"
    AutoEventWireup="true" CodeFile="GP_Element_TJ.aspx.cs" Inherits="TJ_GP_Element_TJ" MaintainScrollPositionOnPostback="true" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent"
    runat="Server">
    <style>
        .gvHeader th
        {
            background: #C1E2EB;
            color: Brown;
            border: solid 1px #333333;
            padding: 0px 5px 0px 5px;
        }
    </style>
    <style type="text/css">
        th
        {
            text-align: center;
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
        .col-lg-1, .col-lg-10, .col-lg-11, .col-lg-12, .col-lg-2, .col-lg-3, .col-lg-4, .col-lg-5, .col-lg-6, .col-lg-7, .col-lg-8, .col-lg-9, .col-md-1, .col-md-10, .col-md-11, .col-md-12, .col-md-2, .col-md-3, .col-md-4, .col-md-5, .col-md-6, .col-md-7, .col-md-8, .col-md-9, .col-sm-1, .col-sm-10, .col-sm-11, .col-sm-12, .col-sm-2, .col-sm-3, .col-sm-4, .col-sm-5, .col-sm-6, .col-sm-7, .col-sm-8, .col-sm-9, .col-xs-1, .col-xs-10, .col-xs-11, .col-xs-12, .col-xs-2, .col-xs-3, .col-xs-4, .col-xs-5, .col-xs-6, .col-xs-7, .col-xs-8, .col-xs-9 {
    position: relative;
    min-height: 1px;
    padding-right: 5px;
    padding-left: 5px;
}
    </style>
    <script src="../Content/js/jquery.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#mestitle").text("【光谱成分统计】");
            $("input[id*='txtmonth']").css("display", "none");
            $("input[id*='txtday']").css("display", "none");
            $("a[id*='_LinkBtn']").css("display", "none");

            $("a[name='mon']").click(function () {
                $("input[id*='txtmonth']").val(this.textContent);
            })
            $("a[name='day']").click(function () {
                $("input[id*='txtday']").val(this.textContent);
            })




        })//endready
        function getMonth() {
            $("#txtmonth").val(this.textContent);
        }
        //        function date() {
        //            var date = new Date;
        //            var month = date.getMonth() + 1;
        //            month = (month < 10 ? "0" + month : month);
        //            return date.getFullYear().toString() + month.toString();
        //            //var mydate = (year.toString() + month.toString());
        //            //alert(month); var year = date.getFullYear();
        //        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent"
    runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="panel panel-info">
        <asp:UpdatePanel runat="server" ID="UpdatePanel1" EnablePartialRendering="true">
            <ContentTemplate>
                <table>
                    <tr>
                        <td>
                            <asp:DropDownList ID="dropYear" runat="server" class="form-control input-s-sm">
                            </asp:DropDownList>
                        </td>
                        <td>
                            年
                        </td>
                        <td>
                            <asp:DropDownList ID="dropMonth" runat="server" class="form-control input-s-sm"
                                AutoPostBack="True" OnSelectedIndexChanged="dropMonth_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td>
                            月
                        </td>
                        <td>
                            <asp:DropDownList ID="dropDay" runat="server" class="form-control input-s-sm">
                            </asp:DropDownList>
                        </td>
                        <td>
                            日
                        </td>
                        <td>
                            班别
                        </td>
                        <td>
                            <asp:DropDownList ID="dropshift" runat="server" class="form-control input-s-sm">
                                <asp:ListItem></asp:ListItem>
                                <asp:ListItem>白班</asp:ListItem>
                                <asp:ListItem>晚班</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>
                            来自
                        </td>
                        <td>
                            <asp:DropDownList ID="dropsource" runat="server" class="form-control input-s-sm">
                                <asp:ListItem></asp:ListItem>
                                <asp:ListItem>精炼机</asp:ListItem>
                                <asp:ListItem>保温炉</asp:ListItem>
                                <asp:ListItem>进货</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>
                            合金：
                        </td>
                        <td>
                            <asp:DropDownList ID="selHeJin" runat="server" class="form-control input-s-sm">
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:Button ID="btnQuery" runat="server" Text="查询" class="btn btn-lg btn-primary"
                                OnClick="btnQuery_Click" Width="92px" Height="45px" />
                            &nbsp;&nbsp;&nbsp; <a href="../Default.aspx" class="btn btn-lg btn-primary"
                                style="color: white">返回</a><asp:LinkButton ID="LinkBtn" runat="server"
                                    OnClick="LinkBtn_Click">binding月明细</asp:LinkButton>
                            <asp:LinkButton ID="LinkBtnDays" runat="server" OnClick="LinkBtnDays_Click">binding日明细</asp:LinkButton>
                            &nbsp;
                            <asp:TextBox ID="txtmonth" runat="server" />
                            <asp:TextBox ID="txtday" runat="server" />
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnQuery" />
                <asp:PostBackTrigger ControlID="LinkBtnDays" />
                <asp:PostBackTrigger ControlID="LinkBtn" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
    <div class="">
        <div class="col-sm-7">
            <div class=" panel panel-info">
                <div class="panel panel-heading">
                    <asp:Label ID="lblYear" runat="server" Text="年"></asp:Label>
                </div>
                <table width="30%">
                    <tr>
                        <td>
                            <div class="panel panel-body">
                                <div style="float: left">
                                    <asp:Chart ID="ChartYear_Si" runat="server" BackColor="#F3DFC1"
                                        BackGradientStyle="TopBottom" BorderColor="181, 64, 1" BorderDashStyle="Solid"
                                        BorderWidth="2" Height="150px" ImageLocation="~/TempImages/ChartPic_#SEQ(300,3)"
                                        ImageType="Png" Palette="none" Width="650px">
                                        <Legends>
                                            <asp:Legend BackColor="Transparent" Font="Trebuchet MS, 8.25pt, style=Bold"
                                                IsTextAutoFit="False" MaximumAutoSize="20" Name="Default"
                                                TitleAlignment="Center">
                                            </asp:Legend>
                                        </Legends>
                                        <%--<Titles>
                        <asp:Title Font="微软雅黑, 10pt" Name="TitleYear" Text="">
                        </asp:Title>
                    </Titles>--%>
                                        <BorderSkin SkinStyle="Emboss" />
                                        <Series>
                                            <asp:Series BorderWidth="3" ChartArea="ChartArea1" ChartType="Line"
                                                LabelBorderWidth="9" Legend="Default" LegendText="Si" MarkerSize="8"
                                                MarkerStyle="Circle" Name="Si" ShadowOffset="3">
                                            </asp:Series>
                                        </Series>
                                        <ChartAreas>
                                            <asp:ChartArea BackColor="64, 165, 191, 228" BackGradientStyle="TopBottom"
                                                BackSecondaryColor="White" BorderColor="64, 64, 64, 64" BorderDashStyle="Solid"
                                                Name="ChartArea1" ShadowColor="Transparent">
                                                <Area3DStyle Inclination="15" IsClustered="False" IsRightAngleAxes="False"
                                                    Perspective="10" Rotation="10" WallWidth="0" />
                                                <AxisY IntervalType="Number" LineColor="64, 64, 64, 64" TitleAlignment="Far">
                                                    <StripLines>
                                                        <asp:StripLine BorderColor="Red" />
                                                        <asp:StripLine BorderColor="Green" TextLineAlignment="Far" />
                                                        <asp:StripLine BorderColor="Green" />
                                                        <asp:StripLine BorderColor="Red" TextLineAlignment="Far" />
                                                    </StripLines>
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
                                    <asp:Chart ID="ChartYear_Fe" runat="server" BackColor="#F3DFC1"
                                        BackGradientStyle="TopBottom" BorderColor="181, 64, 1" BorderDashStyle="Solid"
                                        BorderWidth="2" Height="150px" ImageLocation="~/TempImages/ChartPic_#SEQ(300,3)"
                                        ImageType="Png" Palette="none" Width="650px">
                                        <Legends>
                                            <asp:Legend BackColor="Transparent" Font="Trebuchet MS, 8.25pt, style=Bold"
                                                IsTextAutoFit="False" MaximumAutoSize="20" Name="Default"
                                                TitleAlignment="Center">
                                            </asp:Legend>
                                        </Legends>
                                        <%--<Titles>
                        <asp:Title Font="微软雅黑, 10pt" Name="TitleYear" Text="">
                        </asp:Title>
                    </Titles>--%>
                                        <BorderSkin SkinStyle="Emboss" />
                                        <Series>
                                            <asp:Series BorderWidth="3" ChartArea="ChartArea1" ChartType="Line"
                                                LabelBorderWidth="9" Legend="Default" LegendText="Fe" MarkerSize="8"
                                                MarkerStyle="Circle" Name="Fe" ShadowOffset="3">
                                            </asp:Series>
                                        </Series>
                                        <ChartAreas>
                                            <asp:ChartArea BackColor="64, 165, 191, 228" BackGradientStyle="TopBottom"
                                                BackSecondaryColor="White" BorderColor="64, 64, 64, 64" BorderDashStyle="Solid"
                                                Name="ChartArea1" ShadowColor="Transparent">
                                                <Area3DStyle Inclination="15" IsClustered="False" IsRightAngleAxes="False"
                                                    Perspective="10" Rotation="10" WallWidth="0" />
                                                <AxisY IntervalType="Number" LineColor="64, 64, 64, 64" TitleAlignment="Far">
                                                    <StripLines>
                                                        <asp:StripLine BorderColor="Red" />
                                                        <asp:StripLine BorderColor="Green" TextLineAlignment="Far" />
                                                        <asp:StripLine BorderColor="Green" />
                                                        <asp:StripLine BorderColor="Red" TextLineAlignment="Far" />
                                                    </StripLines>
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
                                    <asp:Chart ID="ChartYear_Cu" runat="server" BackColor="#F3DFC1"
                                        BackGradientStyle="TopBottom" BorderColor="181, 64, 1" BorderDashStyle="Solid"
                                        BorderWidth="2" Height="150px" ImageLocation="~/TempImages/ChartPic_#SEQ(300,3)"
                                        ImageType="Png" Palette="none" Width="650px">
                                        <Legends>
                                            <asp:Legend BackColor="Transparent" Font="Trebuchet MS, 8.25pt, style=Bold"
                                                IsTextAutoFit="False" MaximumAutoSize="20" Name="Default"
                                                TitleAlignment="Center">
                                            </asp:Legend>
                                        </Legends>
                                        <BorderSkin SkinStyle="Emboss" />
                                        <Series>
                                            <asp:Series BorderWidth="3" ChartArea="ChartArea1" ChartType="Line"
                                                LabelBorderWidth="9" Legend="Default" LegendText="Cu" MarkerSize="8"
                                                MarkerStyle="Circle" Name="Cu" ShadowOffset="3">
                                            </asp:Series>
                                        </Series>
                                        <ChartAreas>
                                            <asp:ChartArea BackColor="64, 165, 191, 228" BackGradientStyle="TopBottom"
                                                BackSecondaryColor="White" BorderColor="64, 64, 64, 64" BorderDashStyle="Solid"
                                                Name="ChartArea1" ShadowColor="Transparent">
                                                <Area3DStyle Inclination="15" IsClustered="False" IsRightAngleAxes="False"
                                                    Perspective="10" Rotation="10" WallWidth="0" />
                                                <AxisY IntervalType="Number" LineColor="64, 64, 64, 64" TitleAlignment="Far">
                                                    <StripLines>
                                                        <asp:StripLine BorderColor="Red" />
                                                        <asp:StripLine BorderColor="Green" 
                                                            TextLineAlignment="Far" />
                                                        <asp:StripLine BorderColor="Green" />
                                                        <asp:StripLine BorderColor="Red" TextLineAlignment="Far" />
                                                    </StripLines>
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
                                    <asp:Chart ID="ChartYear_Mg" runat="server" BackColor="#F3DFC1"
                                        BackGradientStyle="TopBottom" BorderColor="181, 64, 1" BorderDashStyle="Solid"
                                        BorderWidth="2" Height="150px" ImageLocation="~/TempImages/ChartPic_#SEQ(300,3)"
                                        ImageType="Png" Palette="none" Width="650px">
                                        <Legends>
                                            <asp:Legend BackColor="Transparent" Font="Trebuchet MS, 8.25pt, style=Bold"
                                                IsTextAutoFit="False" MaximumAutoSize="20" Name="Default"
                                                TitleAlignment="Center">
                                            </asp:Legend>
                                        </Legends>
                                        <%--<Titles>
                        <asp:Title Font="微软雅黑, 10pt" Name="TitleYear" Text="">
                        </asp:Title>
                    </Titles>--%>
                                        <BorderSkin SkinStyle="Emboss" />
                                        <Series>
                                            <asp:Series BorderWidth="3" ChartArea="ChartArea1" ChartType="Line"
                                                LabelBorderWidth="9" Legend="Default" LegendText="Mg" MarkerSize="8"
                                                MarkerStyle="Circle" Name="Mg" ShadowOffset="3">
                                            </asp:Series>
                                        </Series>
                                        <ChartAreas>
                                            <asp:ChartArea BackColor="64, 165, 191, 228" BackGradientStyle="TopBottom"
                                                BackSecondaryColor="White" BorderColor="64, 64, 64, 64" BorderDashStyle="Solid"
                                                Name="ChartArea1" ShadowColor="Transparent">
                                                <Area3DStyle Inclination="15" IsClustered="False" IsRightAngleAxes="False"
                                                    Perspective="10" Rotation="10" WallWidth="0" />
                                                <AxisY IntervalType="Number" LineColor="64, 64, 64, 64" TitleAlignment="Far">
                                                    <StripLines>
                                                        <asp:StripLine BorderColor="Red" />
                                                        <asp:StripLine BorderColor="Green" 
                                                            TextLineAlignment="Far" />
                                                        <asp:StripLine BorderColor="Green" />
                                                        <asp:StripLine BorderColor="Red" TextLineAlignment="Far" />
                                                    </StripLines>
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
                                    <asp:Chart ID="ChartYear_Sf" runat="server" BackColor="#F3DFC1"
                                        BackGradientStyle="TopBottom" BorderColor="181, 64, 1" BorderDashStyle="Solid"
                                        BorderWidth="2" Height="150px" ImageLocation="~/TempImages/ChartPic_#SEQ(300,3)"
                                        ImageType="Png" Palette="none" Width="650px">
                                        <Legends>
                                            <asp:Legend BackColor="Transparent" Font="Trebuchet MS, 8.25pt, style=Bold"
                                                IsTextAutoFit="False" MaximumAutoSize="20" Name="Default"
                                                TitleAlignment="Center">
                                            </asp:Legend>
                                        </Legends>
                                        <%--<Titles>
                        <asp:Title Font="微软雅黑, 10pt" Name="TitleYear" Text="">
                        </asp:Title>
                    </Titles>--%>
                                        <BorderSkin SkinStyle="Emboss" />
                                        <Series>
                                            <asp:Series BorderWidth="3" ChartArea="ChartArea1" ChartType="Line"
                                                LabelBorderWidth="9" Legend="Default" LegendText="Sf" MarkerSize="8"
                                                MarkerStyle="Circle" Name="Sf" ShadowOffset="3">
                                            </asp:Series>
                                        </Series>
                                        <ChartAreas>
                                            <asp:ChartArea BackColor="64, 165, 191, 228" BackGradientStyle="TopBottom"
                                                BackSecondaryColor="White" BorderColor="64, 64, 64, 64" BorderDashStyle="Solid"
                                                Name="ChartArea1" ShadowColor="Transparent">
                                                <Area3DStyle Inclination="15" IsClustered="False" IsRightAngleAxes="False"
                                                    Perspective="10" Rotation="10" WallWidth="0" />
                                                <AxisY IntervalType="Number" LineColor="64, 64, 64, 64" TitleAlignment="Far">
                                                    <StripLines>
                                                        <asp:StripLine BorderColor="Red" />
                                                        <asp:StripLine BorderColor="Green" 
                                                            TextLineAlignment="Far" />
                                                        <asp:StripLine BorderColor="Green" />
                                                        <asp:StripLine BorderColor="Red" TextLineAlignment="Far" />
                                                    </StripLines>
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
                                    <asp:GridView ID="GridViewYear" CssClass="gvHeader" runat="server"
                                        OnRowCreated="GridViewYear_RowCreated">
                                    </asp:GridView>
                                </div>
                            </div>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        <div class="col-sm-5">
            <div class=" panel panel-info">
                <div class="panel panel-heading">
                    <asp:Label ID="lblMonth" runat="server" Text=""></asp:Label>
                </div>
                <table width="30%">
                    <tr>
                        <td>
                            <div class="panel panel-body">
                                <div style="float: left">
                                    <asp:Chart ID="ChartMonth_Si" runat="server" BackColor="#F3DFC1"
                                        BackGradientStyle="TopBottom" BorderColor="181, 64, 1" BorderDashStyle="Solid"
                                        BorderWidth="2" Height="150px" ImageLocation="~/TempImages/ChartPic_#SEQ(300,3)"
                                        ImageType="Png" Palette="none" Width="750px">
                                        <Legends>
                                            <asp:Legend BackColor="Transparent" Font="Trebuchet MS, 8.25pt, style=Bold"
                                                IsTextAutoFit="False" MaximumAutoSize="20" Name="Default"
                                                TitleAlignment="Center">
                                            </asp:Legend>
                                        </Legends>
                                        <%--<Titles>
                        <asp:Title Font="微软雅黑, 10pt" Name="TitleYear" Text="">
                        </asp:Title>
                    </Titles>--%>
                                        <BorderSkin SkinStyle="Emboss" />
                                        <Series>
                                            <asp:Series BorderWidth="3" ChartArea="ChartArea1" ChartType="Line"
                                                LabelBorderWidth="9" Legend="Default" LegendText="Si" MarkerSize="8"
                                                MarkerStyle="Circle" Name="Si" ShadowOffset="3">
                                            </asp:Series>
                                        </Series>
                                        <ChartAreas>
                                            <asp:ChartArea BackColor="64, 165, 191, 228" BackGradientStyle="TopBottom"
                                                BackSecondaryColor="White" BorderColor="64, 64, 64, 64" BorderDashStyle="Solid"
                                                Name="ChartArea1" ShadowColor="Transparent">
                                                <Area3DStyle Inclination="15" IsClustered="False" IsRightAngleAxes="False"
                                                    Perspective="10" Rotation="10" WallWidth="0" />
                                                <AxisY IntervalType="Number" LineColor="64, 64, 64, 64" TitleAlignment="Far">
                                                    <StripLines>
                                                        <asp:StripLine BorderColor="Red" />
                                                        <asp:StripLine BorderColor="Green" TextLineAlignment="Far" />
                                                        <asp:StripLine BorderColor="Green" />
                                                        <asp:StripLine BorderColor="Red" TextLineAlignment="Far" />
                                                    </StripLines>
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
                                    <asp:Chart ID="ChartMonth_Fe" runat="server" BackColor="#F3DFC1"
                                        BackGradientStyle="TopBottom" BorderColor="181, 64, 1" BorderDashStyle="Solid"
                                        BorderWidth="2" Height="150px" ImageLocation="~/TempImages/ChartPic_#SEQ(300,3)"
                                        ImageType="Png" Palette="none" Width="750px">
                                        <Legends>
                                            <asp:Legend BackColor="Transparent" Font="Trebuchet MS, 8.25pt, style=Bold"
                                                IsTextAutoFit="False" MaximumAutoSize="20" Name="Default"
                                                TitleAlignment="Center">
                                            </asp:Legend>
                                        </Legends>
                                        <%--<Titles>
                        <asp:Title Font="微软雅黑, 10pt" Name="TitleYear" Text="">
                        </asp:Title>
                    </Titles>--%>
                                        <BorderSkin SkinStyle="Emboss" />
                                        <Series>
                                            <asp:Series BorderWidth="3" ChartArea="ChartArea1" ChartType="Line"
                                                LabelBorderWidth="9" Legend="Default" LegendText="Fe" MarkerSize="8"
                                                MarkerStyle="Circle" Name="Fe" ShadowOffset="3">
                                            </asp:Series>
                                        </Series>
                                        <ChartAreas>
                                            <asp:ChartArea BackColor="64, 165, 191, 228" BackGradientStyle="TopBottom"
                                                BackSecondaryColor="White" BorderColor="64, 64, 64, 64" BorderDashStyle="Solid"
                                                Name="ChartArea1" ShadowColor="Transparent">
                                                <Area3DStyle Inclination="15" IsClustered="False" IsRightAngleAxes="False"
                                                    Perspective="10" Rotation="10" WallWidth="0" />
                                                <AxisY IntervalType="Number" LineColor="64, 64, 64, 64" TitleAlignment="Far">
                                                    <StripLines>
                                                        <asp:StripLine BorderColor="Red" />
                                                        <asp:StripLine BorderColor="Green" 
                                                            TextLineAlignment="Far" />
                                                        <asp:StripLine BorderColor="Green" />
                                                        <asp:StripLine BorderColor="Red" TextLineAlignment="Far" />
                                                    </StripLines>
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
                                    <asp:Chart ID="ChartMonth_Cu" runat="server" BackColor="#F3DFC1"
                                        BackGradientStyle="TopBottom" BorderColor="181, 64, 1" BorderDashStyle="Solid"
                                        BorderWidth="2" Height="150px" ImageLocation="~/TempImages/ChartPic_#SEQ(300,3)"
                                        ImageType="Png" Palette="none" Width="750px">
                                        <Legends>
                                            <asp:Legend BackColor="Transparent" Font="Trebuchet MS, 8.25pt, style=Bold"
                                                IsTextAutoFit="False" MaximumAutoSize="20" Name="Default"
                                                TitleAlignment="Center">
                                            </asp:Legend>
                                        </Legends>
                                        <BorderSkin SkinStyle="Emboss" />
                                        <Series>
                                            <asp:Series BorderWidth="3" ChartArea="ChartArea1" ChartType="Line"
                                                LabelBorderWidth="9" Legend="Default" LegendText="Cu" MarkerSize="8"
                                                MarkerStyle="Circle" Name="Cu" ShadowOffset="3">
                                            </asp:Series>
                                        </Series>
                                        <ChartAreas>
                                            <asp:ChartArea BackColor="64, 165, 191, 228" BackGradientStyle="TopBottom"
                                                BackSecondaryColor="White" BorderColor="64, 64, 64, 64" BorderDashStyle="Solid"
                                                Name="ChartArea1" ShadowColor="Transparent">
                                                <Area3DStyle Inclination="15" IsClustered="False" IsRightAngleAxes="False"
                                                    Perspective="10" Rotation="10" WallWidth="0" />
                                                <AxisY IntervalType="Number" LineColor="64, 64, 64, 64" TitleAlignment="Far">
                                                    <StripLines>
                                                        <asp:StripLine BorderColor="Red" />
                                                        <asp:StripLine BorderColor="Green" 
                                                            TextLineAlignment="Far" />
                                                        <asp:StripLine BorderColor="Green" />
                                                        <asp:StripLine BorderColor="Red" TextLineAlignment="Far" />
                                                    </StripLines>
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
                                    <asp:Chart ID="ChartMonth_Mg" runat="server" BackColor="#F3DFC1"
                                        BackGradientStyle="TopBottom" BorderColor="181, 64, 1" BorderDashStyle="Solid"
                                        BorderWidth="2" Height="150px" ImageLocation="~/TempImages/ChartPic_#SEQ(300,3)"
                                        ImageType="Png" Palette="none" Width="750px">
                                        <Legends>
                                            <asp:Legend BackColor="Transparent" Font="Trebuchet MS, 8.25pt, style=Bold"
                                                IsTextAutoFit="False" MaximumAutoSize="20" Name="Default"
                                                TitleAlignment="Center">
                                            </asp:Legend>
                                        </Legends>
                                        <%--<Titles>
                        <asp:Title Font="微软雅黑, 10pt" Name="TitleYear" Text="">
                        </asp:Title>
                    </Titles>--%>
                                        <BorderSkin SkinStyle="Emboss" />
                                        <Series>
                                            <asp:Series BorderWidth="3" ChartArea="ChartArea1" ChartType="Line"
                                                LabelBorderWidth="9" Legend="Default" LegendText="Mg" MarkerSize="8"
                                                MarkerStyle="Circle" Name="Mg" ShadowOffset="3">
                                            </asp:Series>
                                        </Series>
                                        <ChartAreas>
                                            <asp:ChartArea BackColor="64, 165, 191, 228" BackGradientStyle="TopBottom"
                                                BackSecondaryColor="White" BorderColor="64, 64, 64, 64" BorderDashStyle="Solid"
                                                Name="ChartArea1" ShadowColor="Transparent">
                                                <Area3DStyle Inclination="15" IsClustered="False" IsRightAngleAxes="False"
                                                    Perspective="10" Rotation="10" WallWidth="0" />
                                                <AxisY IntervalType="Number" LineColor="64, 64, 64, 64" TitleAlignment="Far">
                                                    <StripLines>
                                                        <asp:StripLine BorderColor="Red" />
                                                        <asp:StripLine BorderColor="Green" 
                                                            TextLineAlignment="Far" />
                                                        <asp:StripLine BorderColor="Green" />
                                                        <asp:StripLine BorderColor="Red" TextLineAlignment="Far" />
                                                    </StripLines>
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
                                    <asp:Chart ID="ChartMonth_Sf" runat="server" BackColor="#F3DFC1"
                                        BackGradientStyle="TopBottom" BorderColor="181, 64, 1" BorderDashStyle="Solid"
                                        BorderWidth="2" Height="150px" ImageLocation="~/TempImages/ChartPic_#SEQ(300,3)"
                                        ImageType="Png" Palette="none" Width="750px">
                                        <Legends>
                                            <asp:Legend BackColor="Transparent" Font="Trebuchet MS, 8.25pt, style=Bold"
                                                IsTextAutoFit="False" MaximumAutoSize="20" Name="Default"
                                                TitleAlignment="Center">
                                            </asp:Legend>
                                        </Legends>
                                        <%--<Titles>
                        <asp:Title Font="微软雅黑, 10pt" Name="TitleYear" Text="">
                        </asp:Title>
                    </Titles>--%>
                                        <BorderSkin SkinStyle="Emboss" />
                                        <Series>
                                            <asp:Series BorderWidth="3" ChartArea="ChartArea1" ChartType="Line"
                                                LabelBorderWidth="9" Legend="Default" LegendText="Sf" MarkerSize="8"
                                                MarkerStyle="Circle" Name="Sf" ShadowOffset="3">
                                            </asp:Series>
                                        </Series>
                                        <ChartAreas>
                                            <asp:ChartArea BackColor="64, 165, 191, 228" BackGradientStyle="TopBottom"
                                                BackSecondaryColor="White" BorderColor="64, 64, 64, 64" BorderDashStyle="Solid"
                                                Name="ChartArea1" ShadowColor="Transparent">
                                                <Area3DStyle Inclination="15" IsClustered="False" IsRightAngleAxes="False"
                                                    Perspective="10" Rotation="10" WallWidth="0" />
                                                <AxisY IntervalType="Number" LineColor="64, 64, 64, 64" TitleAlignment="Far">
                                                    <StripLines>
                                                        <asp:StripLine BorderColor="Red" />
                                                        <asp:StripLine BorderColor="Green" 
                                                            TextLineAlignment="Far" />
                                                        <asp:StripLine BorderColor="Green" />
                                                        <asp:StripLine BorderColor="Red" TextLineAlignment="Far" />
                                                    </StripLines>
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
                                    <asp:GridView ID="GridViewMonth" runat="server" CssClass="gvHeader"
                                        OnRowCreated="GridViewMonth_RowCreated">
                                    </asp:GridView>
                                </div>
                            </div>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
    <div class="panel panel-info col-sm-12">
        <div class="panel panel-heading">
            <asp:Label ID="lblDays" runat="server" Text="Day:" /></div>
        <asp:GridView ID="GridViewDay" BorderColor="LightGray" CssClass="gvHeader"
            runat="server" AutoGenerateColumns="False" Width="1000px"
            OnRowDataBound="GridViewDay_RowDataBound" ShowFooter="True"
            OnRowCreated="GridViewDay_RowCreated">
            <Columns>
                <asp:BoundField DataField="dh" HeaderText="批号">
                    <HeaderStyle BackColor="#C1E2EB" />
                </asp:BoundField>
                <asp:BoundField DataField="sbno" HeaderText="设备号">
                    <HeaderStyle BackColor="#C1E2EB" />
                </asp:BoundField>
                <asp:BoundField DataField="create_time" HeaderText="取样时间">
                    <HeaderStyle BackColor="#C1E2EB" />
                </asp:BoundField>
                <asp:BoundField DataField="confirm_time" HeaderText="检测时间">
                    <FooterStyle HorizontalAlign="Right" />
                    <HeaderStyle BackColor="#C1E2EB" />
                    <ItemStyle></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="source" HeaderText="来自">
                    <HeaderStyle BackColor="#C1E2EB" />
                    <ItemStyle></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="si" HeaderText="Si" ItemStyle-HorizontalAlign="Right">
                    <FooterStyle HorizontalAlign="Right" />
                    <HeaderStyle BackColor="#C1E2EB" />
                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="fe" HeaderText="Fe" ItemStyle-HorizontalAlign="Right">
                    <FooterStyle HorizontalAlign="Right" />
                    <HeaderStyle BackColor="#C1E2EB" />
                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="cu" HeaderText="Cu" ItemStyle-HorizontalAlign="Right">
                    <FooterStyle HorizontalAlign="Right" />
                    <HeaderStyle BackColor="#C1E2EB" HorizontalAlign="Right" />
                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="mg" HeaderText="Mg" ItemStyle-HorizontalAlign="Right">
                    <FooterStyle HorizontalAlign="Right" />
                    <HeaderStyle BackColor="#C1E2EB" />
                    <ItemStyle></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="sf" HeaderText="Sf" ItemStyle-HorizontalAlign="Right">
                    <FooterStyle HorizontalAlign="Right" />
                    <HeaderStyle BackColor="#C1E2EB" />
                    <ItemStyle></ItemStyle>
                </asp:BoundField>
            </Columns>
            <EmptyDataTemplate>
                查无资料</EmptyDataTemplate>
        </asp:GridView>
    </div>
</asp:Content>
