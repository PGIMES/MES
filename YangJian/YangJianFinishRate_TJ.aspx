<%@ Page Title="MES【样件完成率统计】" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="YangJianFinishRate_TJ.aspx.cs" Inherits="YangJianFinishRate_TJ" EnableViewState="True"
    MaintainScrollPositionOnPostback="true" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
 
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">

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
    </style>
    
   <script src="../Content/js/jquery.min.js"></script>   
    <script type="text/javascript">
        $(document).ready(function () {
           
            $("#mestitle").text("【样件完成率统计】");
            $("input[id*='txtmonth']").css("display", "none");
            $("input[id*='txtday']").css("display", "none");
            $("input[id*='txtYearOrder']").css("display", "none");
            $("input[id*='txtOrder']").css("display", "none");
            $("a[id*='_LinkBtn']").css("display", "none");

            $("a[name='mon']").click(function () {
                $("input[id*='txtmonth']").val(this.textContent);
            })
            $("a[name='day']").click(function () {
                $("input[id*='txtday']").val(this.textContent);
            })

            $("a[name='order']").click(function () {
                $("input[id*='txtOrder']").val($(this).text());
            })
            $("a[name='yearorder']").click(function () {
                $("input[id*='txtYearOrder']").val($(this).text());
            })
        })//endready
        function getMonth() {
            $("#txtmonth").val(this.textContent);
        }
        
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="panel panel-info" >
       
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
                        OnSelectedIndexChanged="dropMonth_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
                <td>
                    月
                </td>               
                <td style="display:none">
                    产品
                </td>
                <td style="display:none">
                    <asp:TextBox ID="txtPart" runat="server" class="form-control input-s-sm"/>
                </td>            
                <td>
                    负责人
                </td>
                <td>
                     <asp:TextBox ID="txtWho" runat="server" class="form-control input-s-sm"/>
                </td>
                <td>
                    部门
                </td>
                <td>
                    <asp:DropDownList ID="dropDept" runat="server" class="form-control input-s-sm">
                        <asp:ListItem Value=""></asp:ListItem>                        
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:Button ID="btnQuery" runat="server" Text="查询" class="btn btn-lg btn-primary"
                        OnClick="btnQuery_Click" Width="92px" Height="45px" />
                    &nbsp;&nbsp;&nbsp; <a href="../Default.aspx" class="btn btn-lg btn-primary" style="color: white">
                        返回</a><asp:LinkButton ID="LinkBtn" runat="server" OnClick="LinkBtn_Click">binding月明细</asp:LinkButton>
 
                    &nbsp;
                    <asp:LinkButton ID="LinkBtnYearOrder" runat="server" OnClick="LinkBtnYearOrder_Click" >年排序</asp:LinkButton>
                    <asp:LinkButton ID="LinkBtnMonthOrder" runat="server" OnClick="LinkBtnMonthOrder_Click" >月排序</asp:LinkButton>
                    <asp:TextBox ID="txtmonth" runat="server" />
                    <asp:TextBox ID="txtday" runat="server" />
                    <asp:TextBox ID="txtYearOrder" runat="server" text="及时率"/>
                    <asp:TextBox ID="txtOrder" runat="server" text="及时率"/>
                </td>
            </tr>
        </table>
    </div>
    
    <%--完成率年--%>
    <div class=" panel panel-info col-lg-6 " >
        <div class="panel panel-heading">
            <asp:Label ID="lblYearFinish" runat="server" Text="年"></asp:Label>
        </div>
        <div class="panel panel-body">
            <div style="float: left">
                <asp:Chart ID="ChartYearTimesFinish" runat="server" BackColor="#F3DFC1" BackGradientStyle="TopBottom"
                    BorderColor="181, 64, 1" BorderDashStyle="Solid" BorderWidth="2" Height="200px"
                    ImageLocation="~/TempImages/ChartPic_#SEQ(300,3)" ImageType="Png" Palette="none"
                    Width="700px">
                    <Legends>
                        <asp:Legend BackColor="Transparent" Font="Trebuchet MS, 8.25pt, style=Bold" IsTextAutoFit="False"
                            MaximumAutoSize="20" Name="Default" TitleAlignment="Center" Alignment="Center" Docking="Top" >
                        </asp:Legend>
                    </Legends>
                    <BorderSkin SkinStyle="Emboss" />
                    <Series  >
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
                <div style="color:red">说明：完成率=完成数/总任务数；及时率=完成数(及时)/完成数</div>
                <div style="min-height:250px;overflow-y:auto;max-height:300px;">
                    <asp:GridView ID="GridViewYearFinish" BorderColor="LightGray" runat="server" OnRowCreated="GridViewYearFinish_RowCreated" AutoGenerateColumns="false">
                        <Columns>
                            <asp:BoundField DataField="责任人" HeaderText="责任人" />
                            <asp:BoundField DataField="职称" HeaderText="职称" />
                            <asp:BoundField DataField="年份" HeaderText="年份" />
                            <asp:BoundField DataField="总任务数" HeaderText="总任务数" />
                            <asp:BoundField DataField="完成数" HeaderText="完成数" />
                            <asp:BoundField DataField="及时完成数" HeaderText="完成数(及时)"  HeaderStyle-Width="60px"/>
                            <asp:BoundField DataField="逾时完成" HeaderText="完成数(逾时)"  HeaderStyle-Width="60px"/>
                            <asp:BoundField DataField="未逾时未完成" HeaderText="未完成数(未逾时)"  HeaderStyle-Width="70px"/>
                            <asp:BoundField DataField="逾时未完成" HeaderText="未完成数(逾时)" HeaderStyle-Width="70px" />                        
                            <asp:BoundField DataField="完成率" DataFormatString="{0:P0}" HeaderText="完成率" />
                            <asp:BoundField DataField="及时率" DataFormatString="{0:P0}" HeaderText="及时率" />
                        </Columns>
                    </asp:GridView>
                 </div>
            </div>
            <div>
            </div>
        </div>
    </div>
    <%--完成率月--%>
    <div class=" panel panel-info  col-lg-6 ">
        <div class="panel panel-heading">
            <asp:Label ID="lblMonthFinish" runat="server" Text=""></asp:Label>
        </div>
        <div class="panel panel-body">
            
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
            <div style="color:red">说明：完成率=完成数/总任务数；及时率=完成数(及时)/完成数</div>
            <div style="min-height:250px;overflow-y:auto;max-height:300px;">
             <asp:GridView ID="GridViewMonthFinish" runat="server" BorderColor="lightgray" OnRowCreated="GridViewMonthFinish_RowCreated" AutoGenerateColumns="false" AllowSorting="True">
                <Columns>
                    <asp:BoundField DataField="责任人" HeaderText="责任人" />
                    <asp:BoundField DataField="职称" HeaderText="职称" />
                    <asp:BoundField DataField="月份" HeaderText="月份" />
                    <asp:BoundField DataField="总任务数" HeaderText="总任务数" />
                    <asp:BoundField DataField="完成数" HeaderText="完成数" />
                    <asp:BoundField DataField="及时完成数" HeaderText="完成数(及时)"  HeaderStyle-Width="60px">
<HeaderStyle Width="60px"></HeaderStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="逾时完成" HeaderText="完成数(逾时)"  HeaderStyle-Width="60px">
<HeaderStyle Width="60px"></HeaderStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="未逾时未完成" HeaderText="未完成数(未逾时)"  HeaderStyle-Width="70px">
<HeaderStyle Width="70px"></HeaderStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="逾时未完成" HeaderText="未完成数(逾时)" HeaderStyle-Width="70px" >                    
<HeaderStyle Width="70px"></HeaderStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="完成率" DataFormatString="{0:P0}" HeaderText="完成率" />
                    <asp:BoundField DataField="及时率" DataFormatString="{0:P0}" HeaderText="及时率" />
                </Columns>
                <SortedAscendingCellStyle BackColor="#0099FF" />
                <SortedAscendingHeaderStyle BackColor="#0099FF" />
                <SortedDescendingCellStyle BackColor="#0099FF" />
             </asp:GridView>
           </div>

        </div>
    </div>

     
    
    
</asp:Content>
