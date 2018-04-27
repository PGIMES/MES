<%@ Page Title="MES【光谱查询】" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="GP_Query.aspx.cs" Inherits="JingLian_GP_Query" %>
<%@ Register assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" namespace="System.Web.UI.DataVisualization.Charting" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
<script src="../Content/js/jquery.min.js" type="text/javascript"></script>
    <script src="../Content/js/plugins/layer/layer.min.js" type="text/javascript"></script>
 <script type="text/javascript" language="javascript">
     $(document).ready(function () {

         $("#tst").click(function () {

         });

     });
     // $("div[class='h3']").text($("div[class='h3']").text() + "【转运包清理记录查询】");
     $("#mestitle").text("【光谱查询】");
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
                    <div class="col-sm-12">
                       
                                <table>
                                    
                                    <tr>
                                        <td>
                                            年度:
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="txt_year" runat="server" class="form-control input-s-sm " Width="100px">
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            月:
                                        </td>
                                        <td>
                                           <asp:DropDownList ID="txt_month" runat="server" class="form-control input-s-sm ">
                                           </asp:DropDownList>
                                        </td>
                                        <td>
                                            日期:
                                        </td>
                                        <td>
                                           <asp:TextBox ID="txt_startdate" runat="server" Width="100" />
             <ajaxtoolkit:calendarextender ID="txt_startdate_CalendarExtender" 
                 runat="server" PopupButtonID="Image2"  Format="yyyy/MM/dd"
                 TargetControlID="txt_startdate" />
             ~&nbsp;<asp:TextBox ID="txt_enddate" runat="server" 
                 Width="100" />
             <ajaxtoolkit:calendarextender ID="txt_enddate_CalendarExtender" 
                 runat="server" PopupButtonID="Image2"  Format="yyyy/MM/dd"
                 TargetControlID="txt_enddate" />
                                        </td>
                                      <td>批号：</td>
                                      <td>
                                            <asp:TextBox ID="txt_dh" runat="server" 
                                              class="form-control input-s-sm "></asp:TextBox>
                                        </td>
                                        <td>样件来自：</td>
                                        <td>
                                            <asp:DropDownList ID="ddl_source" runat="server" 
                                                class="form-control input-s-sm ">
                                                <asp:ListItem></asp:ListItem>
                                                <asp:ListItem>保温炉</asp:ListItem>
                                                <asp:ListItem>精炼机</asp:ListItem>
                                                <asp:ListItem>进货</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td>合金：</td>
                                        <td>
                                          <asp:DropDownList ID="ddl_hejin" runat="server" class="form-control input-small">
                                              <asp:ListItem></asp:ListItem>
                                            <asp:ListItem>A380</asp:ListItem>
                                            <asp:ListItem>EN46000</asp:ListItem>
                                            <asp:ListItem>ADC12</asp:ListItem>
                                            <asp:ListItem>EN47100</asp:ListItem>
                                        </asp:DropDownList>
                                        </td>
                                         <td>班别：</td>
                                        <td>
                                          <asp:DropDownList ID="txt_banbie" runat="server" class="form-control input-small">
                                              <asp:ListItem></asp:ListItem>
                                            <asp:ListItem>白班</asp:ListItem>
                                            <asp:ListItem>晚班</asp:ListItem>
                                        </asp:DropDownList>
                                        </td>
                                    </tr>
                                     
                                    <tr>
                                        
                                        <td colspan=6 align="right">
                                            <asp:Button ID="btn_query" runat="server" Text="查询"  
                                              class="btn btn-large btn-primary" 
                                              Width="100px" onclick="btn_query_Click" />
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
                     
                    </div>
                </div>
            </div>
        </div>
        <br />
         <div  runat="server" id="DIV1" style=" margin:10px"  >
        
                       
                                <asp:Panel ID="Panel2" runat="server" Height="100%" >
                                    <table style=" background-color: #FFFFFF;" >
                                      
                                        <tr>
                                        <td rowspan="2"  valign="top">
                                                                                    <div id="Div2" 
                                                                                                                    
                                                        style=" overflow: scroll; height: 1500px; width:1300px">
                                                                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                                            <ContentTemplate>
                                                                                <asp:GridView ID="gv1" runat="server"   Font-Size="12px" 
                                                                                    AutoGenerateColumns="False" 
                                                                                    onrowdatabound="gv1_RowDataBound">
                                                                                    <Columns>
                                                                                       <asp:BoundField HeaderText="序号">
                                                                                        <HeaderStyle BackColor="#C1E2EB" ForeColor="Brown" 
                                                                                            Width="45px" />
                                                                                        </asp:BoundField>
                                                                                        <asp:BoundField DataField="date" HeaderStyle-ForeColor="Brown" 
                                                                                            HeaderText="日期">
                                                                                        <HeaderStyle BackColor="#C1E2EB" />
                                                                                        <ItemStyle Width="40px" Wrap="false" />
                                                                                        </asp:BoundField>
                                                                                        <asp:BoundField DataField="dh" 
                                                                                            HeaderStyle-ForeColor="Brown" HeaderText="批号">
                                                                                        <HeaderStyle ForeColor="Brown" BackColor="#C1E2EB" 
                                                                                            Width="150px" />
                                                                                        <ItemStyle Width="100px" Wrap="True" />
                                                                                        </asp:BoundField>
                                                                                        <asp:BoundField DataField="sbno" HeaderText="设备号">
                                                                                        <HeaderStyle BackColor="#C1E2EB" ForeColor="Brown" 
                                                                                            Width="60px" />
                                                                                        </asp:BoundField>
                                                                                        <asp:BoundField DataField="create_time" HeaderText="取样时间">
                                                                                        <HeaderStyle BackColor="#C1E2EB" ForeColor="Brown" />
                                                                                        </asp:BoundField>
                                                                                        <asp:BoundField DataField="confirm_time" HeaderText="检测时间">
                                                                                        <HeaderStyle BackColor="#C1E2EB" ForeColor="Brown" />
                                                                                        </asp:BoundField>
                                                                                        <asp:BoundField DataField="timer" HeaderText="时长">
                                                                                        <HeaderStyle BackColor="#C1E2EB" ForeColor="Brown" 
                                                                                            Width="50px" />
                                                                                        <ItemStyle HorizontalAlign="Right" />
                                                                                        </asp:BoundField>
                                                                                        <asp:BoundField DataField="source" HeaderText="样件来自">
                                                                                        <HeaderStyle BackColor="#C1E2EB" ForeColor="Brown" 
                                                                                            Width="80px" />
                                                                                        <ItemStyle HorizontalAlign="Center" />
                                                                                        </asp:BoundField>
                                                                                        <asp:BoundField DataField="jgtime" 
                                                                                            HeaderText="间隔&lt;br&gt;时长" HtmlEncode="False">
                                                                                        <HeaderStyle BackColor="#C1E2EB" ForeColor="Brown" 
                                                                                            Wrap="False" />
                                                                                        <ItemStyle HorizontalAlign="Right" />
                                                                                        </asp:BoundField>
                                                                                        <asp:BoundField DataField="si" DataFormatString="{0:N3}" 
                                                                                            HeaderStyle-ForeColor="Brown" HeaderText="Si">
                                                                                        <FooterStyle HorizontalAlign="Right" />
                                                                                        <HeaderStyle ForeColor="Brown" BackColor="#C1E2EB" />
                                                                                        <ItemStyle HorizontalAlign="Right" Width="30px" 
                                                                                            Wrap="false" />
                                                                                        </asp:BoundField>
                                                                                        <asp:BoundField DataField="fe" DataFormatString="{0:N3}" 
                                                                                            HeaderStyle-ForeColor="Brown" HeaderText="Fe">
                                                                                        <FooterStyle HorizontalAlign="Right" />
                                                                                        <HeaderStyle ForeColor="Brown" BackColor="#C1E2EB" />
                                                                                        <ItemStyle HorizontalAlign="Right" Width="30px" 
                                                                                            Wrap="false" />
                                                                                        </asp:BoundField>
                                                                                        <asp:BoundField DataField="cu" DataFormatString="{0:N3}" 
                                                                                            HeaderStyle-ForeColor="Brown" HeaderText="Cu">
                                                                                        <FooterStyle HorizontalAlign="Right" />
                                                                                        <HeaderStyle ForeColor="Brown" BackColor="#C1E2EB" />
                                                                                        <ItemStyle HorizontalAlign="Right" Width="30px" 
                                                                                            Wrap="false" />
                                                                                        </asp:BoundField>
                                                                                        <asp:BoundField DataField="mg" DataFormatString="{0:N3}" 
                                                                                            HeaderStyle-ForeColor="Brown" HeaderText="Mg">
                                                                                        <FooterStyle HorizontalAlign="Right" />
                                                                                        <HeaderStyle ForeColor="Brown" BackColor="#C1E2EB" />
                                                                                        <ItemStyle HorizontalAlign="Right" Width="30px" 
                                                                                            Wrap="false" />
                                                                                        </asp:BoundField>
                                                                                        <asp:BoundField DataField="zn" DataFormatString="{0:N3}" 
                                                                                            HeaderStyle-ForeColor="Brown" HeaderText="Zn">
                                                                                        <FooterStyle HorizontalAlign="Right" />
                                                                                        <HeaderStyle ForeColor="Brown" BackColor="#C1E2EB" />
                                                                                        <ItemStyle HorizontalAlign="Right" Width="30px" 
                                                                                            Wrap="false" />
                                                                                        </asp:BoundField>
                                                                                        <asp:BoundField DataField="pb" DataFormatString="{0:N3}" 
                                                                                            HeaderStyle-ForeColor="Brown" HeaderText="Pb">
                                                                                        <FooterStyle HorizontalAlign="Right" />
                                                                                        <HeaderStyle ForeColor="Brown" BackColor="#C1E2EB" />
                                                                                        <ItemStyle HorizontalAlign="Right" Width="30px" 
                                                                                            Wrap="false" />
                                                                                        </asp:BoundField>
                                                                                        <asp:BoundField DataField="Cr" DataFormatString="{0:N3}" 
                                                                                            HeaderText="Cr">
                                                                                      <FooterStyle HorizontalAlign="Right" />
                                                                                        <HeaderStyle ForeColor="Brown" BackColor="#C1E2EB" />
                                                                                        <ItemStyle HorizontalAlign="Right" Width="30px" 
                                                                                            Wrap="false" />
                                                                                        </asp:BoundField>
                                                                                        <asp:BoundField DataField="Ni" DataFormatString="{0:N3}" 
                                                                                            HeaderText="Ni">
                                                                                         <FooterStyle HorizontalAlign="Right" />
                                                                                        <HeaderStyle ForeColor="Brown" BackColor="#C1E2EB" />
                                                                                        <ItemStyle HorizontalAlign="Right" Width="30px" 
                                                                                            Wrap="false" />
                                                                                        </asp:BoundField>
                                                                                        <asp:BoundField DataField="Ti" DataFormatString="{0:N3}" 
                                                                                            HeaderText="Ti">
                                                                                         <FooterStyle HorizontalAlign="Right" />
                                                                                        <HeaderStyle ForeColor="Brown" BackColor="#C1E2EB" />
                                                                                        <ItemStyle HorizontalAlign="Right" Width="30px" 
                                                                                            Wrap="false" />
                                                                                        </asp:BoundField>
                                                                                        <asp:BoundField DataField="Sn" DataFormatString="{0:N3}" 
                                                                                            HeaderText="Sn">
                                                                                         <FooterStyle HorizontalAlign="Right" />
                                                                                        <HeaderStyle ForeColor="Brown" BackColor="#C1E2EB" />
                                                                                        <ItemStyle HorizontalAlign="Right" Width="30px" 
                                                                                            Wrap="false" />
                                                                                        </asp:BoundField>
                                                                                        <asp:BoundField DataField="Al" DataFormatString="{0:N3}" 
                                                                                            HeaderText="Al">
                                                                                         <FooterStyle HorizontalAlign="Right" />
                                                                                        <HeaderStyle ForeColor="Brown" BackColor="#C1E2EB" />
                                                                                        <ItemStyle HorizontalAlign="Right" Width="30px" 
                                                                                            Wrap="false" />
                                                                                        </asp:BoundField>
                                                                                        <asp:BoundField DataField="Sf" DataFormatString="{0:N3}" 
                                                                                            HeaderText="Sf">
                                                                                         <FooterStyle HorizontalAlign="Right" />
                                                                                        <HeaderStyle ForeColor="Brown" BackColor="#C1E2EB" />
                                                                                        <ItemStyle HorizontalAlign="Right" Width="30px" 
                                                                                            Wrap="false" />
                                                                                        </asp:BoundField>
                                                                                        <asp:BoundField DataField="Mn" DataFormatString="{0:N3}" 
                                                                                            HeaderText="Mn">
                                                                                        <FooterStyle HorizontalAlign="Right" />
                                                                                        <HeaderStyle ForeColor="Brown" BackColor="#C1E2EB" />
                                                                                        <ItemStyle HorizontalAlign="Right" Width="30px" 
                                                                                            Wrap="false" />
                                                                                        </asp:BoundField>
                                                                                        <asp:BoundField DataField="Sr" DataFormatString="{0:N3}" 
                                                                                            HeaderText="Sr">
                                                                                        <FooterStyle HorizontalAlign="Right" />
                                                                                        <HeaderStyle ForeColor="Brown" BackColor="#C1E2EB" />
                                                                                        <ItemStyle HorizontalAlign="Right" Width="30px" 
                                                                                            Wrap="false" />
                                                                                        </asp:BoundField>
                                                                                        <asp:BoundField DataField="si_color" HeaderText="si_color">
                                                                                        <ControlStyle CssClass="hidden" Width="0px" />
                                                                                        <HeaderStyle CssClass="hidden" Width="0px" />
                                                                                        <ItemStyle CssClass="hidden" Width="0px" />
                                                                                        </asp:BoundField>
                                                                                            <asp:BoundField DataField="fe_color" HeaderText="fe_color">
                                                                                        <ControlStyle CssClass="hidden" Width="0px" />
                                                                                        <HeaderStyle CssClass="hidden" Width="0px" />
                                                                                        <ItemStyle CssClass="hidden" Width="0px" />
                                                                                        </asp:BoundField>
                                                                                            <asp:BoundField DataField="cu_color" HeaderText="cu_color">
                                                                                        <ControlStyle CssClass="hidden" Width="0px" />
                                                                                        <HeaderStyle CssClass="hidden" Width="0px" />
                                                                                        <ItemStyle CssClass="hidden" Width="0px" />
                                                                                        </asp:BoundField>
                                                                                        <asp:BoundField DataField="mg_color" HeaderText="mg_color">
                                                                                        <ControlStyle CssClass="hidden" Width="0px" />
                                                                                        <HeaderStyle CssClass="hidden" Width="0px" />
                                                                                        <ItemStyle CssClass="hidden" Width="0px" />
                                                                                        </asp:BoundField>
                                                                                            <asp:BoundField DataField="zn_color" HeaderText="zn_color">
                                                                                        <ControlStyle CssClass="hidden" Width="0px" />
                                                                                        <HeaderStyle CssClass="hidden" Width="0px" />
                                                                                        <ItemStyle CssClass="hidden" Width="0px" />
                                                                                        </asp:BoundField>
                                                                                            <asp:BoundField DataField="pb_color" HeaderText="pb_color">
                                                                                        <ControlStyle CssClass="hidden" Width="0px" />
                                                                                        <HeaderStyle CssClass="hidden" Width="0px" />
                                                                                        <ItemStyle CssClass="hidden" Width="0px" />
                                                                                        </asp:BoundField>
                                                                                            <asp:BoundField DataField="cr_color" HeaderText="cr_color">
                                                                                        <ControlStyle CssClass="hidden" Width="0px" />
                                                                                        <HeaderStyle CssClass="hidden" Width="0px" />
                                                                                        <ItemStyle CssClass="hidden" Width="0px" />
                                                                                        </asp:BoundField>
                                                                                        <asp:BoundField DataField="ni_color" HeaderText="ni_color">
                                                                                        <ControlStyle CssClass="hidden" Width="0px" />
                                                                                        <HeaderStyle CssClass="hidden" Width="0px" />
                                                                                        <ItemStyle CssClass="hidden" Width="0px" />
                                                                                        </asp:BoundField>
                                                                                            <asp:BoundField DataField="ti_color" HeaderText="ti_color">
                                                                                        <ControlStyle CssClass="hidden" Width="0px" />
                                                                                        <HeaderStyle CssClass="hidden" Width="0px" />
                                                                                        <ItemStyle CssClass="hidden" Width="0px" />
                                                                                        </asp:BoundField>
                                                                                        <asp:BoundField DataField="sn_color" HeaderText="sn_color">
                                                                                        <ControlStyle CssClass="hidden" Width="0px" />
                                                                                        <HeaderStyle CssClass="hidden" Width="0px" />
                                                                                        <ItemStyle CssClass="hidden" Width="0px" />
                                                                                        </asp:BoundField>
                                                                                            <asp:BoundField DataField="al_color" HeaderText="al_color">
                                                                                        <ControlStyle CssClass="hidden" Width="0px" />
                                                                                        <HeaderStyle CssClass="hidden" Width="0px" />
                                                                                        <ItemStyle CssClass="hidden" Width="0px" />
                                                                                        </asp:BoundField>
                                                                                        <asp:BoundField DataField="sf_color" HeaderText="sf_color">
                                                                                        <ControlStyle CssClass="hidden" Width="0px" />
                                                                                        <HeaderStyle CssClass="hidden" Width="0px" />
                                                                                        <ItemStyle CssClass="hidden" Width="0px" />
                                                                                        </asp:BoundField>
                                                                                        <asp:BoundField DataField="mn_color" HeaderText="mn_color">
                                                                                        <ControlStyle CssClass="hidden" Width="0px" />
                                                                                        <HeaderStyle CssClass="hidden" Width="0px" />
                                                                                        <ItemStyle CssClass="hidden" Width="0px" />
                                                                                        </asp:BoundField>
                                                                                        <asp:BoundField DataField="sr_color" HeaderText="sr_color">
                                                                                        <ControlStyle CssClass="hidden" Width="0px" />
                                                                                        <HeaderStyle CssClass="hidden" Width="0px" />
                                                                                        <ItemStyle CssClass="hidden" Width="0px" />
                                                                                        </asp:BoundField>
                                                                                        <asp:BoundField DataField="emp_name" HeaderText="测试人">
                                                                                        <HeaderStyle BackColor="#C1E2EB" ForeColor="Brown" 
                                                                                            Width="80px" />
                                                                                        </asp:BoundField>
                                                                                        <asp:BoundField DataField="hejin" HeaderText="合金">
                                                                                        <HeaderStyle BackColor="#C1E2EB" ForeColor="Brown" 
                                                                                            Width="60px" />
                                                                                        </asp:BoundField>
                                                                                    </Columns>
                                                                                </asp:GridView>
                                                                            </ContentTemplate>
                                                                        </asp:UpdatePanel>
                                                                    </div>
                                                    
                                                                                    </td>
                                            <td valign="top">
                                                <table bgcolor="#ffffff" border="0" cellpadding="0" 
                                                    cellspacing="0" width="100%">
                                                    <tr align="left" bgcolor="#ffffff">
                                                        <td valign="top">
                                                            <table width="100%">
                                                                <tr>
                                                                    <td>
                                                                        <div style=" margin-left:0px">
                                                                            <table>
                                                                                <tr>
                                                                                    <td valign="top">
                                                                                    <div id="divchart" runat="server">
                                                                                        <%-- 表开始--%><%--表结束--%>
                                                                                        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                                                                            <ContentTemplate>
                                                                                                <div style=" margin:0px;">
                                                                                                    <table style="width: 92%; height: 167px; border-left-style: none; border-left-color: inherit; border-left-width: 0;">
                                                                                                        </tr>
                                                                                                        <tr>
                                                                                                            <td>
                                                                                                               
                                                                                                          <td rowspan="2">
                                                                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                                                            <ContentTemplate>
                                                                                                <%-- 图表一开始--%>
                                                                                                   <asp:Chart id="C1" runat="server" Palette="None" BackColor="243, 223, 193" 
                                                                                 ImageLocation="~/TempImages/ChartPic_#SEQ(300,3)" Width="900px" 
                                                                                 BorderDashStyle="Solid" BackGradientStyle="TopBottom" 
                                                                                BorderWidth="2px" BorderColor="#B54001">
							                                                   <Titles>
                                                                                   <asp:Title Alignment="MiddleLeft" Name="Title1" Text="SI">
                                                                                   </asp:Title>
                                                                               </Titles>
							<borderskin SkinStyle="Emboss"></borderskin>
							<series>
								
							    <asp:Series BorderWidth="3" ChartArea="ChartArea1" 
                                    Name="Series1" ShadowOffset="2" ChartType="Line" 
                                    LabelBorderWidth="9" MarkerSize="8" MarkerStyle="Circle" 
                                    IsVisibleInLegend="False" YValuesPerPoint="2">
                                </asp:Series>
								
							</series>
                           
							 <ChartAreas>
                    <asp:ChartArea Name="ChartArea1" BorderColor="64, 64, 64, 64" BorderDashStyle="Solid"
                        BackSecondaryColor="White" BackColor="64, 165, 191, 228" ShadowColor="Transparent"
                        BackGradientStyle="TopBottom">
                        <Area3DStyle Rotation="10" Perspective="10" Inclination="15" IsRightAngleAxes="False"
                            WallWidth="0" IsClustered="False"></Area3DStyle>
                        <AxisY LineColor="64, 64, 64, 64" 
                           >
                            <StripLines>
                                 <asp:StripLine BorderColor="Red" />
                                <asp:StripLine BorderColor="Green" />
                                <asp:StripLine BorderColor="Green" />
                                <asp:StripLine BorderColor="Red" />
                            </StripLines>
                            <LabelStyle Font="Trebuchet MS, 8.25pt, style=Bold" />
                            <MajorGrid LineColor="64, 64, 64, 64" Enabled="False" />
                        </AxisY>
                       <AxisX LineColor="64, 64, 64, 64" Interval="1">
                            <LabelStyle Font="Trebuchet MS, 8.25pt, style=Bold" />
                            <MajorGrid LineColor="64, 64, 64, 64" />
                        </AxisX>
                    </asp:ChartArea>
                </ChartAreas>
						</asp:Chart>
                                                                            <%-- 图表一结束--%>
                                                                            <br>
                                                                                <%-- 图表二开始--%>
                                                                           <asp:Chart id="C2" runat="server" Palette="None" BackColor="243, 223, 193" 
                                                                                 ImageLocation="~/TempImages/ChartPic_#SEQ(300,3)" Width="900px" 
                                                                                 BorderDashStyle="Solid" BackGradientStyle="TopBottom" 
                                                                                BorderWidth="2px" BorderColor="#B54001">
							                                                   <Titles>
                                                                                   <asp:Title Alignment="MiddleLeft" Name="Title1" Text="Fe">
                                                                                   </asp:Title>
                                                                               </Titles>
							<borderskin SkinStyle="Emboss"></borderskin>
							<series>
								
							    <asp:Series BorderWidth="3" ChartArea="ChartArea1" 
                                    Name="Series1" ShadowOffset="2" ChartType="Line" 
                                    LabelBorderWidth="9" MarkerSize="8" MarkerStyle="Circle" 
                                    IsVisibleInLegend="False">
                                </asp:Series>
								
							</series>
                           
							 <ChartAreas>
                    <asp:ChartArea Name="ChartArea1" BorderColor="64, 64, 64, 64" BorderDashStyle="Solid"
                        BackSecondaryColor="White" BackColor="64, 165, 191, 228" ShadowColor="Transparent"
                        BackGradientStyle="TopBottom">
                        <Area3DStyle Rotation="10" Perspective="10" Inclination="15" IsRightAngleAxes="False"
                            WallWidth="0" IsClustered="False"></Area3DStyle>
                        <AxisY LineColor="64, 64, 64, 64" 
                           >
                            <StripLines>
                                <asp:StripLine BorderColor="Red" 
                                    IntervalOffsetType="Number" IntervalType="Number" />
                                <asp:StripLine BorderColor="Green" 
                                    IntervalOffsetType="Number" IntervalType="Number" />
                                <asp:StripLine BorderColor="Green" />
                                <asp:StripLine BorderColor="Red" />
                            </StripLines>
                            <LabelStyle Font="Trebuchet MS, 8.25pt, style=Bold" />
                            <MajorGrid LineColor="64, 64, 64, 64" Enabled="False" />
                        </AxisY>
                       <AxisX LineColor="64, 64, 64, 64" Interval="1">
                            <LabelStyle Font="Trebuchet MS, 8.25pt, style=Bold" />
                            <MajorGrid LineColor="64, 64, 64, 64" />
                        </AxisX>
                    </asp:ChartArea>
                </ChartAreas>
						</asp:Chart>
                                                                            <%-- 图表二结束--%>

                                                                             <br>
                                                                                <%-- 图表三开始--%>
                                                                           <asp:Chart id="C3" runat="server" Palette="None" BackColor="243, 223, 193" 
                                                                                 ImageLocation="~/TempImages/ChartPic_#SEQ(300,3)" Width="900px" 
                                                                                 BorderDashStyle="Solid" BackGradientStyle="TopBottom" 
                                                                                BorderWidth="2px" BorderColor="#B54001">
							                                                   <Titles>
                                                                                   <asp:Title Alignment="MiddleLeft" Name="Title1" Text="Cu">
                                                                                   </asp:Title>
                                                                               </Titles>
							<borderskin SkinStyle="Emboss"></borderskin>
							<series>
								
							    <asp:Series BorderWidth="3" ChartArea="ChartArea1" 
                                    Name="Series1" ShadowOffset="2" ChartType="Line" 
                                    LabelBorderWidth="9" MarkerSize="8" MarkerStyle="Circle" 
                                    IsVisibleInLegend="False">
                                </asp:Series>
								
							</series>
                           
							 <ChartAreas>
                    <asp:ChartArea Name="ChartArea1" BorderColor="64, 64, 64, 64" BorderDashStyle="Solid"
                        BackSecondaryColor="White" BackColor="64, 165, 191, 228" ShadowColor="Transparent"
                        BackGradientStyle="TopBottom">
                        <Area3DStyle Rotation="10" Perspective="10" Inclination="15" IsRightAngleAxes="False"
                            WallWidth="0" IsClustered="False"></Area3DStyle>
                        <AxisY LineColor="64, 64, 64, 64" 
                            >
                            <StripLines>
                                <asp:StripLine BorderColor="Red" 
                                    IntervalOffsetType="Number" IntervalType="Number" />
                                <asp:StripLine BorderColor="Green" 
                                    IntervalOffsetType="Number" IntervalType="Number" />
                                <asp:StripLine BorderColor="Green" />
                                <asp:StripLine BorderColor="Red" />
                            </StripLines>
                            <LabelStyle Font="Trebuchet MS, 8.25pt, style=Bold" />
                            <MajorGrid LineColor="64, 64, 64, 64" Enabled="False" />
                        </AxisY>
                       <AxisX LineColor="64, 64, 64, 64" Interval="1">
                            <LabelStyle Font="Trebuchet MS, 8.25pt, style=Bold" />
                            <MajorGrid LineColor="64, 64, 64, 64" />
                        </AxisX>
                    </asp:ChartArea>
                </ChartAreas>
						</asp:Chart>
                                                                            <%-- 图表三结束--%>

                                                                             <br>
                                                                                <%-- 图表四开始--%>
                                                                           <asp:Chart id="C4" runat="server" Palette="None" BackColor="243, 223, 193" 
                                                                                 ImageLocation="~/TempImages/ChartPic_#SEQ(300,3)" Width="900px" 
                                                                                 BorderDashStyle="Solid" BackGradientStyle="TopBottom" 
                                                                                BorderWidth="2px" BorderColor="#B54001">
							                                                   <Titles>
                                                                                   <asp:Title Alignment="MiddleLeft" Name="Title1" Text="Mg">
                                                                                   </asp:Title>
                                                                               </Titles>
							<borderskin SkinStyle="Emboss"></borderskin>
							<series>
								
							    <asp:Series BorderWidth="3" ChartArea="ChartArea1" 
                                    Name="Series1" ShadowOffset="2" ChartType="Line" 
                                    LabelBorderWidth="9" MarkerSize="8" MarkerStyle="Circle" 
                                    IsVisibleInLegend="False">
                                </asp:Series>
								
							</series>
                           
							 <ChartAreas>
                    <asp:ChartArea Name="ChartArea1" BorderColor="64, 64, 64, 64" BorderDashStyle="Solid"
                        BackSecondaryColor="White" BackColor="64, 165, 191, 228" ShadowColor="Transparent"
                        BackGradientStyle="TopBottom">
                        <Area3DStyle Rotation="10" Perspective="10" Inclination="15" IsRightAngleAxes="False"
                            WallWidth="0" IsClustered="False"></Area3DStyle>
                        <AxisY LineColor="64, 64, 64, 64" 
                            >
                            <StripLines>
                                <asp:StripLine BorderColor="Red" 
                                    IntervalOffsetType="Number" IntervalType="Number" />
                                <asp:StripLine BorderColor="Green" 
                                    IntervalOffsetType="Number" IntervalType="Number" />
                                <asp:StripLine BorderColor="Green" />
                                <asp:StripLine BorderColor="Red" />
                               
                            </StripLines>
                            <LabelStyle Font="Trebuchet MS, 8.25pt, style=Bold" />
                            <MajorGrid LineColor="64, 64, 64, 64" Enabled="False" />
                        </AxisY>
                       <AxisX LineColor="64, 64, 64, 64" Interval="1">
                            <LabelStyle Font="Trebuchet MS, 8.25pt, style=Bold" />
                            <MajorGrid LineColor="64, 64, 64, 64" />
                        </AxisX>
                    </asp:ChartArea>
                </ChartAreas>
						</asp:Chart>
                                                                            <%-- 图表四结束--%>

                                                                              <br>
                                                                                <%-- 图表五开始--%>
                                                                           <asp:Chart id="C5" runat="server" Palette="None" BackColor="243, 223, 193" 
                                                                                 ImageLocation="~/TempImages/ChartPic_#SEQ(300,3)" Width="900px" 
                                                                                 BorderDashStyle="Solid" BackGradientStyle="TopBottom" 
                                                                                BorderWidth="2px" BorderColor="#B54001">
							                                                   <Titles>
                                                                                   <asp:Title Alignment="MiddleLeft" Name="Title1" Text="Sf">
                                                                                   </asp:Title>
                                                                               </Titles>
							<borderskin SkinStyle="Emboss"></borderskin>
							<series>
								
							    <asp:Series BorderWidth="3" ChartArea="ChartArea1" 
                                    Name="Series1" ShadowOffset="2" ChartType="Line" 
                                    LabelBorderWidth="9" MarkerSize="8" MarkerStyle="Circle" 
                                    IsVisibleInLegend="False">
                                </asp:Series>
								
							</series>
                           
							 <ChartAreas>
                    <asp:ChartArea Name="ChartArea1" BorderColor="64, 64, 64, 64" BorderDashStyle="Solid"
                        BackSecondaryColor="White" BackColor="64, 165, 191, 228" ShadowColor="Transparent"
                        BackGradientStyle="TopBottom">
                        <Area3DStyle Rotation="10" Perspective="10" Inclination="15" IsRightAngleAxes="False"
                            WallWidth="0" IsClustered="False"></Area3DStyle>
                        <AxisY LineColor="64, 64, 64, 64" 
                          >
                            <StripLines>
                               
                                   <asp:StripLine BorderColor="Red" 
                                    IntervalOffsetType="Number" IntervalType="Number" />
                                <asp:StripLine BorderColor="Green" 
                                    IntervalOffsetType="Number" IntervalType="Number" />
                                <asp:StripLine BorderColor="Green" />
                                <asp:StripLine BorderColor="Red" />
                               
                            </StripLines>
                            <LabelStyle Font="Trebuchet MS, 8.25pt, style=Bold" />
                            <MajorGrid LineColor="64, 64, 64, 64" Enabled="False" />
                        </AxisY>
                       <AxisX LineColor="64, 64, 64, 64" Interval="1">
                            <LabelStyle Font="Trebuchet MS, 8.25pt, style=Bold" />
                            <MajorGrid LineColor="64, 64, 64, 64" />
                        </AxisX>
                    </asp:ChartArea>
                </ChartAreas>
						</asp:Chart>
                                                                            <%-- 图表五结束--%>

                                                                            </ContentTemplate></asp:UpdatePanel>

                                                                                                            </td>
                                                                                                        </tr>
                                                                                                        </tr>
                                                                                                    </table>
                                                                                                </div>
                                                                                            </ContentTemplate>
                                                                                        </asp:UpdatePanel>
                                                                                        </div>
                                                                                    </td>
                                                                                    
                                                                                </tr>
                                                                            </table>
                                                                        </div>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                                </div>
</asp:Content>

