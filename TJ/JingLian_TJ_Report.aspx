<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="JingLian_TJ_Report.aspx.cs" Inherits="TJ_JingLian_TJ_Report" %>
<%@ Register assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" namespace="System.Web.UI.DataVisualization.Charting" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <script src="../Content/js/jquery.min.js" type="text/javascript"></script>
    <script src="../Content/js/plugins/layer/layer.min.js" type="text/javascript"></script>

<style>
    .gvHeader th{    background: #C1E2EB;    color:  Brown;    border: solid 1px #333333;    padding:0px 5px 0px 5px;font-size:12px;}
   
</style>
 <script type="text/javascript" language="javascript">
     $(document).ready(function () {
         $("input[id*='txtmonth']").css("display", "none");
         $("input[id*='txt_bymonth']").css("display", "none");
         $("a[id*='LinkBtn']").css("display", "none");
         $("a[name='mon']").click(function () {
             $("input[id*='txtmonth']").val(this.textContent);
         })
         $("a[name='mnth']").click(function () {
             $("input[id*='txt_bymonth']").val(this.textContent);
         })

     })//endready
     function getMonth() {
         $("#txtmonth").val(this.textContent);
         $("#txt_bymonth").val(this.textContent);
     }
     // $("div[class='h3']").text($("div[class='h3']").text() + "【转运包清理记录查询】");
     $("#mestitle").text("【测氢数据统计分析】");

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
                                                           <asp:UpdatePanel runat="server" ID="UpdatePanel1" EnablePartialRendering="true">
                 <ContentTemplate>
                    <div class="col-sm-12">
                       
                                <table>
                        
                                    <tr>
                                        <td>
                                            年度:
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="txt_year" runat="server" class="form-control input-s-sm " >
                                            </asp:DropDownList>
                                        </td>
           
                                        <td>
                                            月份:
                                        </td>
                                        
                                        <td>
                                           <asp:DropDownList ID="txt_month" runat="server" 
                                                class="form-control input-s-sm " AutoPostBack="True" 
                                                onselectedindexchanged="txt_month_SelectedIndexChanged">
                                           </asp:DropDownList>
                                        </td>
                                        <td>日：</td>
                                        <td>
                                           <asp:TextBox ID="txt_date" runat="server" Width="100" />
             <ajaxtoolkit:calendarextender ID="txt_startdate_CalendarExtender" 
                 runat="server" PopupButtonID="Image2"  Format="yyyy-MM-dd"
                 TargetControlID="txt_date" />
                                        </td>
                                       
                                         <td>
                                            班组:
                                        </td>
                                        <td>
                                           <asp:DropDownList ID="ddl_banzhu" runat="server" class="form-control input-s-sm ">
                                               <asp:ListItem></asp:ListItem>
                                               <asp:ListItem>C</asp:ListItem>
                                               <asp:ListItem>D</asp:ListItem>
                                           </asp:DropDownList>
                                        </td>
                                        <td>
                                        <asp:LinkButton ID="LinkBtn" runat="server"                 
                onclick="LinkBtn_Click">日明细</asp:LinkButton>
                                                <asp:LinkButton ID="LinkBtnMonth" runat="server"
                                                onclick="LinkBtnMonth_Click">binding月明细</asp:LinkButton>
                    <asp:TextBox ID="txt_bymonth" runat="server"  />
                                        </td>
                                         <td colspan=2 align="right">
                                            <asp:Button ID="Button1" runat="server" Text="查询"  
                                              class="btn btn-large btn-primary" 
                                              onclick="Button1_Click" Width="100px" />
                                        </td>
                                        <td colspan="2">
                                            &nbsp;
                                            <asp:Button ID="Button2" runat="server" Text="返回"  
                                              class="btn btn-large btn-primary" 
                                               Width="100px" onclick="Button2_Click" />
                                        </td>
                                        </tr>
                                   
                                     
                                </table>
                               
                                </div>
                       </ContentTemplate>
                     
                                                               <Triggers>
                                                                   <asp:PostBackTrigger ControlID="Button1" />
                                                                   <asp:PostBackTrigger ControlID="LinkBtnMonth" />
                                                                   <asp:PostBackTrigger ControlID="LinkBtn" />
                                                               </Triggers>
                     
                 </asp:UpdatePanel>
                    </div>
                    
                </div>
            </div>
        </div>
              <div class=" panel panel-info col-lg-4">
        <div class="panel panel-heading">
            <asp:Label ID="lblYear" runat="server" Text="年"></asp:Label>
     
        </div>
                                <asp:Chart ID="C2" runat="server" BackColor="#F3DFC1" 
                                                                                    BackGradientStyle="TopBottom" BorderColor="181, 64, 1" 
                                                                                    BorderDashStyle="Solid" BorderWidth="2" Height="200px" 
                                                                                    ImageLocation="~/TempImages/ChartPic_#SEQ(300,3)" 
                                                                                    ImageType="Png" Palette="none" Width="600px">
                                                                                    <legends>
                                                                                        <asp:Legend BackColor="Transparent" 
                                                                                            Font="Trebuchet MS, 8.25pt, style=Bold" 
                                                                                            IsTextAutoFit="False" MaximumAutoSize="20" Name="Default" 
                                                                                            TitleAlignment="Center">
                                                                                        </asp:Legend>
                                                                                    </legends>
                                                                                    <borderskin SkinStyle="Emboss" />
                                                                                    <series>
                                                                                        <asp:Series BorderWidth="3" ChartArea="ChartArea1" 
                                                                                            ChartType="Line" Legend="Default" LegendText="密度" 
                                                                                            MarkerSize="8" MarkerStyle="Circle" Name="Series2" 
                                                                                            ShadowOffset="3">
                                                                                        </asp:Series>
                                                                                    </series>
                                                                                    <ChartAreas>
                                                                                        <asp:ChartArea BackColor="64, 165, 191, 228" 
                                                                                            BackGradientStyle="TopBottom" BackSecondaryColor="White" 
                                                                                            BorderColor="64, 64, 64, 64" BorderDashStyle="Solid" 
                                                                                            Name="ChartArea1" ShadowColor="Transparent">
                                                                                            <Area3DStyle Inclination="15" IsClustered="False" 
                                                                                                IsRightAngleAxes="False" Perspective="10" Rotation="10" 
                                                                                                WallWidth="0" />
                                                                                            <AxisY LineColor="64, 64, 64, 64" TitleAlignment="Far">
                                                                                                <LabelStyle Font="Trebuchet MS, 8.25pt, style=Bold" />
                                                                                                <MajorGrid LineColor="64, 64, 64, 64" />
                                                                                                <StripLines>
                               
                                   <asp:StripLine BorderColor="Red" IntervalOffset="2.65" 
                                    IntervalOffsetType="Number" IntervalType="Number" Text="2.65" 
                                    ToolTip="2.65" />
                                <asp:StripLine BorderColor="Yellow" IntervalOffset="2.68" 
                                    IntervalOffsetType="Number" IntervalType="Number" Text="2.68" 
                                    ToolTip="2.68" TextLineAlignment="Far" />
                              
                               
                            </StripLines>
                                                                                            </AxisY>
                                                                                            <AxisX Interval="1" LineColor="64, 64, 64, 64">
                                                                                                <LabelStyle Font="Trebuchet MS, 8.25pt, style=Bold" />
                                                                                                <MajorGrid LineColor="64, 64, 64, 64" />
                                                                                            </AxisX>
                                                                                        </asp:ChartArea>
                                                                                    </ChartAreas>
                                                                                </asp:Chart>
                                                                  
                                                                        <%--图表二开始--%>

                                                                               <asp:Chart ID="C1" runat="server" BackColor="#F3DFC1" 
                                                                                   BackGradientStyle="TopBottom" BorderColor="181, 64, 1" 
                                                                                   BorderDashStyle="Solid" BorderWidth="2" Height="200px" 
                                                                                   ImageLocation="~/TempImages/ChartPic_#SEQ(300,3)" 
                                                                                   ImageType="Png" Palette="none" Width="600px" 
                                                                                   >
                                                                                   <legends>
                                                                                       <asp:Legend BackColor="Transparent" 
                                                                                           Font="Trebuchet MS, 8.25pt, style=Bold" 
                                                                                           IsTextAutoFit="False" MaximumAutoSize="20" Name="Default" 
                                                                                           TitleAlignment="Center">
                                                                                       </asp:Legend>
                                                                                   </legends>
                                                                                   <borderskin SkinStyle="Emboss" />
                                                                                   <series>
                                                                                       <asp:Series BorderWidth="3" ChartArea="ChartArea1" 
                                                                                           ChartType="Line" LabelBorderWidth="9" Legend="Default" 
                                                                                           LegendText="温度" MarkerSize="8" MarkerStyle="Circle" 
                                                                                           Name="Series1" ShadowOffset="3">
                                                                                       </asp:Series>
                                                                                   </series>
                                                                                   <ChartAreas>
                                                                                       <asp:ChartArea BackColor="64, 165, 191, 228" 
                                                                                           BackGradientStyle="TopBottom" BackSecondaryColor="White" 
                                                                                           BorderColor="64, 64, 64, 64" BorderDashStyle="Solid" 
                                                                                           Name="ChartArea1" ShadowColor="Transparent">
                                                                                           <Area3DStyle Inclination="15" IsClustered="False" 
                                                                                               IsRightAngleAxes="False" Perspective="10" Rotation="10" 
                                                                                               WallWidth="0" />
                                                                                           <AxisY LineColor="64, 64, 64, 64" TitleAlignment="Far">
                                                                                               <LabelStyle Font="Trebuchet MS, 8.25pt, style=Bold" />
                                                                                               <MajorGrid LineColor="64, 64, 64, 64" />

                                                                                                 <StripLines>
                               
                                   <asp:StripLine BorderColor="Red" IntervalOffset="680" 
                                    IntervalOffsetType="Number" IntervalType="Number" Text="680" 
                                    ToolTip="680" />
                                <asp:StripLine BorderColor="Yellow" IntervalOffset="690" 
                                    IntervalOffsetType="Number" IntervalType="Number" Text="690" 
                                    ToolTip="690" TextLineAlignment="Far" />
                              
                               
                            </StripLines>
                                                                                           </AxisY>
                                                                                           <AxisX Interval="1" LineColor="64, 64, 64, 64">
                                                                                               <LabelStyle Font="Trebuchet MS, 8.25pt, style=Bold" />
                                                                                               <MajorGrid LineColor="64, 64, 64, 64" />
                                                                                           </AxisX>
                                                                                       </asp:ChartArea>
                                                                                   </ChartAreas>
                                                                               </asp:Chart>
                                                                     
                                                                           <%--图表二结束--%>
                                                                          
        <asp:GridView ID="gv_bymonth" runat="server"  CssClass="gvHeader"
             onrowdatabound="gv_bymonth_RowDataBound" 
                      onrowcreated="gv_bymonth_RowCreated">
        </asp:GridView>
    </div>
   
      <div class=" panel panel-info col-lg-8">
        <div class="panel panel-heading">
            <asp:Label ID="lblMonth" runat="server" Text=""></asp:Label></div>
        <asp:Chart ID="C3" runat="server" BackColor="#F3DFC1" BackGradientStyle="TopBottom"
            BorderColor="181, 64, 1" BorderDashStyle="Solid" BorderWidth="2" Height="200px"
            ImageLocation="~/TempImages/ChartPic_#SEQ(300,3)" ImageType="Png" Palette="none"
            Width="900px">
            <Legends>
                <asp:Legend BackColor="Transparent" Font="Trebuchet MS, 8.25pt, style=Bold" IsTextAutoFit="False"
                    MaximumAutoSize="20" Name="Default" TitleAlignment="Center">
                </asp:Legend>
            </Legends>
            <%-- <Titles>
                        <asp:Title Font="微软雅黑, 10pt" Name="TitleMonth" Text="">
                        </asp:Title>
                    </Titles>--%>
            <BorderSkin SkinStyle="Emboss" />
            <Series>
                <asp:Series BorderWidth="3" ChartArea="ChartArea1" 
                    ChartType="Line" LabelBorderWidth="9"
                    Legend="Default" LegendText="密度" MarkerSize="8" 
                    MarkerStyle="Circle" Name="密度"
                    ShadowOffset="3" XValueMember="Days" 
                    YValueMembers="weight">
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
                         <StripLines>
                               
                                   <asp:StripLine BorderColor="Red" IntervalOffset="2.65" 
                                    IntervalOffsetType="Number" IntervalType="Number" Text="2.65" 
                                    ToolTip="2.65" />
                                <asp:StripLine BorderColor="Yellow" IntervalOffset="2.68" 
                                    IntervalOffsetType="Number" IntervalType="Number" Text="2.68" 
                                    ToolTip="2.68" TextLineAlignment="Far" />
                              
                               
                            </StripLines>
                    </AxisY>
                    <AxisX Interval="1" LineColor="64, 64, 64, 64">
                        <LabelStyle Font="Trebuchet MS, 8.25pt, style=Bold" />
                        <MajorGrid LineColor="64, 64, 64, 64" />
                    </AxisX>
                </asp:ChartArea>
            </ChartAreas>
        </asp:Chart>
        <asp:Chart ID="C4" runat="server" BackColor="#F3DFC1" BackGradientStyle="TopBottom"
            BorderColor="181, 64, 1" BorderDashStyle="Solid" BorderWidth="2" Height="200px"
            ImageLocation="~/TempImages/ChartPic_#SEQ(300,3)" ImageType="Png" Palette="none"
            Width="900px">
            <Legends>
                <asp:Legend BackColor="Transparent" Font="Trebuchet MS, 8.25pt, style=Bold" IsTextAutoFit="False"
                    MaximumAutoSize="20" Name="Default" TitleAlignment="Center">
                </asp:Legend>
            </Legends>
            <%-- <Titles>
                        <asp:Title Font="微软雅黑, 10pt" Name="TitleMonth" Text="">
                        </asp:Title>
                    </Titles>--%>
            <BorderSkin SkinStyle="Emboss" />
            <Series>
                <asp:Series BorderWidth="3" ChartArea="ChartArea1" 
                    ChartType="Line" LabelBorderWidth="9"
                    Legend="Default" LegendText="温度" MarkerSize="8" 
                    MarkerStyle="Circle" Name="温度"
                    ShadowOffset="3" XValueMember="Days" 
                    YValueMembers="weight">
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
                         <StripLines>
                               
                                  <asp:StripLine BorderColor="Red" IntervalOffset="680" 
                                    IntervalOffsetType="Number" IntervalType="Number" Text="680" 
                                    ToolTip="680" />
                                <asp:StripLine BorderColor="Yellow" IntervalOffset="690" 
                                    IntervalOffsetType="Number" IntervalType="Number" Text="690" 
                                    ToolTip="690" TextLineAlignment="Far" />
                              
                               
                            </StripLines>
                    </AxisY>
                    <AxisX Interval="1" LineColor="64, 64, 64, 64">
                        <LabelStyle Font="Trebuchet MS, 8.25pt, style=Bold" />
                        <MajorGrid LineColor="64, 64, 64, 64" />
                    </AxisX>
                </asp:ChartArea>
            </ChartAreas>
        </asp:Chart>
        <asp:GridView ID="gv1" runat="server" 
                onrowcreated="gv1_RowCreated"  CssClass="gvHeader" onrowdatabound="gv1_RowDataBound"  
            >
        </asp:GridView>
    </div>
      
    <div class=" panel panel-info col-lg-12">
        <div class="panel panel-heading">
            <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
                                            
                                                </div>
     <table><tr><td  style=" width:650">
        
     <asp:textbox   id="txtmonth" runat="server"  style=" border:none"/>
         <asp:GridView ID="gv_hj" runat="server"  CssClass="gvHeader"
             AutoGenerateColumns="False" Width="600px">
             <Columns>
                 <asp:BoundField DataField="hd_hejin" HeaderText="合金" />
<asp:BoundField DataField="HD_baohao" HeaderText="转运包号"></asp:BoundField>
                 <asp:BoundField DataField="bs" HeaderText="包数" >
                 <HeaderStyle HorizontalAlign="Right" />
                 <ItemStyle HorizontalAlign="Right" />
                 </asp:BoundField>
                 <asp:BoundField DataField="jz" HeaderText="净重" 
                     DataFormatString="{0:N}" >
                 <HeaderStyle HorizontalAlign="Right" />
                 <ItemStyle HorizontalAlign="Right" />
                 </asp:BoundField>
                 <asp:BoundField DataField="dbzl" HeaderText="单包重量" >
                 <HeaderStyle HorizontalAlign="Right" />
                 <ItemStyle HorizontalAlign="Right" />
                 </asp:BoundField>
             </Columns>
        </asp:GridView>
        </td>
      
        </tr></table>
    </div>
   
  
</asp:Content>


