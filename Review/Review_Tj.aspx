<%@ Page Title="MES【问题解决分析统计】" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="Review_Tj.aspx.cs" Inherits="Review_TJ" EnableViewState="True"
    MaintainScrollPositionOnPostback="true" %>


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

        .bootstrap-select > .dropdown-toggle {
            width: 150px;
        }

        .bootstrap-select:not([class*=col-]):not([class*=form-control]):not(.input-group-btn) {
            width: 120px;
        }
        .d_width{
            width:100px;

        }
    </style>
     <script src="../Content/js/jquery.min.js" type="text/javascript"></script>
    <script src="../Content/js/bootstrap.min.js"></script>
    <script src="../Content/js/plugins/layer/laydate/laydate.js"></script>
    <script src="../Content/js/plugins/layer/layer.min.js"></script>
    <link href="../Content/js/plugins/bootstrap-select/css/bootstrap-select.min.css" rel="stylesheet" />
    <script src="../Content/js/plugins/bootstrap-select/js/bootstrap-select.min.js"></script>

    <script type="text/javascript">       

        $(document).ready(function () {
            $("#mestitle").text("【问题解决分析】");
            $('.selectpicker').change(function () {
               
                if (this.id.indexOf("prob") >= 0) {
                    $("input[id*='txtprobfrom']").val($("[class='selectpicker A']").val());

                }

                if (this.id.indexOf("duty") >= 0) {
                    $("input[id*='txtdutydept']").val($("[class='selectpicker B']").val());

                }

            });

            $("a[name='C']").click(function () {
                $("input[id*='txtName']").val($(this).attr("names"));
                $(" input[id*='txtType']").val($(this).attr("types"));
            })
        })//endready


    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>    
    <div class="panel panel-info ">
        <div >
            <table>
                <tr>
                    <td style="width: 60px; white-space: nowrap">问题来源:
                    </td>
                    <td> <asp:TextBox ID="txtprobfrom" class="form-control input-s-sm" runat="server" style="display:none"></asp:TextBox>
                                    <select id="selprobfrom" name="selprobfrom" class="selectpicker A" multiple  data-live-search="true" runat="server" style="width:80px" >                                          
                                    </select>
                    </td>
                    <td style="width: 60px; white-space: nowrap">统计方式:
                    </td>
                    <td>
                        <asp:DropDownList ID="txtdate_type" runat="server" class="form-control input-s-sm" style="width: 120px; white-space: nowrap">
                            <asp:ListItem Value="PROBDATE">提出日期</asp:ListItem>
                             <asp:ListItem Value="REQCLOSEDATE">要求关闭日期</asp:ListItem>
                        </asp:DropDownList></td>
                    <td style="width: 60px; white-space: nowrap">年份:
                    </td>
                    <td >
                        <asp:DropDownList ID="ddlyear" runat="server" class="form-control input-s-sm" style="width: 80px; white-space: nowrap">
                        </asp:DropDownList>
                    </td>
                    <td style="width: 60px; white-space: nowrap">月份:
                    </td>
                    <td >
                        <asp:DropDownList ID="ddlMonth" runat="server" class="form-control input-s-sm" style="width: 70px; white-space: nowrap">
                        </asp:DropDownList>
                    </td>
                     <td style="width: 60px; white-space: nowrap">提出人:
                    </td>
                    <td >
                        <asp:DropDownList ID="ddlempname" runat="server" class="form-control input-s-sm" style="width: 100px; white-space: nowrap">
                        </asp:DropDownList>
                    </td>

                    <td style="width: 60px; white-space: nowrap">责任部门
                    </td>
                    <td>
                       <asp:TextBox ID="txtdutydept" class="form-control input-s-sm" runat="server" style="display:none"></asp:TextBox>
                                    <select id="seldutydept" name="seldutydept" class="selectpicker B" multiple  data-live-search="true" runat="server" style="width:80px" > 
                        <option Value="工程一部">工程一部</option>
                        <option Value="工程二部">工程二部</option>
                        <option Value="产品一组">&nbsp;&nbsp;&nbsp;&nbsp;产品一组</option>
                        <option Value="产品三组">&nbsp;&nbsp;&nbsp;&nbsp;产品三组</option>
                        <option Value="调试组">&nbsp;&nbsp;&nbsp;&nbsp;调试组</option>
                        <option Value="工程三部">工程三部</option>
                        <option Value="产品二组">&nbsp;&nbsp;&nbsp;&nbsp;产品二组</option>
                        <option Value="产品四组">&nbsp;&nbsp;&nbsp;&nbsp;产品四组</option>
                        <option Value="项目管理部">项目管理部</option>
                        <option Value="项目一组">&nbsp;&nbsp;&nbsp;&nbsp;项目一组</option>
                        <option Value="项目二组">&nbsp;&nbsp;&nbsp;&nbsp;项目二组</option>
                        <option Value="项目三组">&nbsp;&nbsp;&nbsp;&nbsp;项目三组</option>
                        <option Value="生产一部">生产一部</option>
                         <option Value="生产二部">生产二部</option>
                         <option Value="生产三部（压铸）">生产三部（压铸）</option>
                         <option Value="压铸技术部">压铸技术部</option>
                        <option Value="质量二部">质量二部</option>
                        <option Value="物流二部">物流二部</option>
                        <option Value="销售二部">销售二部</option>
                        <option Value="客户一组">&nbsp;&nbsp;&nbsp;&nbsp;客户一组</option>
                        <option Value="客户二组">&nbsp;&nbsp;&nbsp;&nbsp;客户二组</option>
                                         <option Value="设备一部">设备一部</option>  
                                         <option Value="设备二部">设备二部</option>  
                                     
                                         <option Value="人事部">人事部</option>
                                         <option Value="IT部">IT部</option>                               
                                    </select>
                    </td>
                    <td style="width: 50px; white-space: nowrap">客户</td>
                    <td>
                        <asp:DropDownList ID="ddlcustomer" runat="server" class="form-control input-s-sm " Width="80px">
                                        <asp:ListItem Value="">全部</asp:ListItem>
                                       
                                    </asp:DropDownList>            

                    </td>
                     <td style="width: 50px; white-space: nowrap">产品</td>
                    <td>
             <asp:TextBox ID="txtProduct" class="form-control input-s-sm" runat="server" Width="100px" ></asp:TextBox>
                    </td>
                     <td style="width: 70px; white-space: nowrap">问题描述</td>
                    <td>
             <asp:TextBox ID="txtProdDesc" class="form-control input-s-sm" runat="server" Width="100px" ></asp:TextBox>
                    </td>
                    <td>

                          <div style="width: 100%; text-align: center">
                                                <asp:Button ID="Button1" runat="server" Text="查询" class="btn btn-large btn-primary" OnClick="btnQuery_Click" Width="100px" />
                                            </div>
                    </td>
                </tr>
            </table>
        </div>

    </div>
    <div class="col-lg-12">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <%--A--%>
            <div class=" panel panel-info col-lg-6 " style="display:block">
                <div class="panel panel-heading" >
                    全部年份问题数                   
                </div>
                <div class="panel panel-body" style="width:100%;overflow:scroll;">
                    <div style="float: left">
                        <asp:Chart ID="ChartA" runat="server" BackColor="#F3DFC1" BackGradientStyle="TopBottom"
                            BorderColor="181, 64, 1" BorderDashStyle="Solid" BorderWidth="2" Height="250px"
                            ImageLocation="~/TempImages/ChartPic_#SEQ(300,3)" ImageType="Png" Palette="none" 
                            Width="600px">
                            <Legends>
                                <asp:Legend BackColor="Transparent" Font="Trebuchet MS, 8pt, style=Bold" IsTextAutoFit="False" 
                                    MaximumAutoSize="20" Name="Default" TitleAlignment="Center">
                                </asp:Legend>
                            </Legends>
                            <BorderSkin SkinStyle="Emboss" />
                            <Series>
                                <asp:Series BorderWidth="3" ChartArea="ChartArea1" ChartType="StackedColumn" LabelBorderWidth="9" 
                                    Legend="Default" LegendText="已关闭" MarkerSize="8" MarkerStyle="Circle" Name="A1"     
                                    ShadowOffset="3" LabelBackColor="255, 192, 192" ShadowColor="0, 0, 0, 0" >
                                </asp:Series>
                               
                                <asp:Series ChartArea="ChartArea1" Legend="Default" LegendText="未关闭" Name="A2" ChartType="StackedColumn" ShadowColor="0, 0, 0, 0">
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
                       
                        <div>
                        </div>

                        <asp:GridView ID="GridViewA" BorderColor="lightgray" runat="server" >
                            <EmptyDataTemplate>暂无数据</EmptyDataTemplate>
                        </asp:GridView>
                    </div>
                </div>
            </div>

            <%--B--%>
            <div class=" panel panel-info  col-lg-6 " style="display:block">
                <div class="panel panel-heading">
                    <asp:Label ID="lblB" runat="server" Text="2018"></asp:Label>年问题数
                </div>
                <div class="panel panel-body">

                    <asp:Chart ID="ChartB" runat="server" BackColor="#F3DFC1" BackGradientStyle="TopBottom"
                        BorderColor="181, 64, 1" BorderDashStyle="Solid" BorderWidth="2" Height="250px"
                        ImageLocation="~/TempImages/ChartPic_#SEQ(300,3)" ImageType="Png" Palette="none"
                        Width="650px">
                        <Legends>
                            <asp:Legend BackColor="Transparent" Font="Trebuchet MS, 8.25pt, style=Bold" IsTextAutoFit="False"
                                MaximumAutoSize="20" Name="Default" TitleAlignment="Near">
                            </asp:Legend>
                        </Legends>
                        <BorderSkin SkinStyle="Emboss" />
                        <Series>
                            <asp:Series BorderWidth="3" ChartArea="ChartArea1" ChartType="StackedColumn" LabelBorderWidth="9"
                                Legend="Default" LegendText="已关闭 " MarkerSize="8" MarkerStyle="Circle" Name="B1"
                                ShadowOffset="3" ShadowColor="0, 0, 0, 0">
                            </asp:Series>                            
                            <asp:Series ChartArea="ChartArea1" ChartType="StackedColumn" Legend="Default" LegendText="未关闭" Name="B2" ShadowColor="0, 0, 0, 0">
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
                    <asp:GridView ID="GridViewB" runat="server" BorderColor="lightgray" >
                        <EmptyDataTemplate>暂无数据</EmptyDataTemplate>
                    </asp:GridView>
                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>
    </div>

    <%--C--%>
    <div class=" panel panel-info  col-lg-6 " style="display:block">
        <div class="panel panel-heading">
            <asp:Label ID="lblC" runat="server" Text=""></asp:Label>年部门未完成行动任务数
        </div>
        <div class="panel panel-body">

            <asp:Chart ID="ChartC" runat="server" BackColor="#F3DFC1" BackGradientStyle="TopBottom"
                BorderColor="181, 64, 1" BorderDashStyle="Solid" BorderWidth="2" Height="250px"
                ImageLocation="~/TempImages/ChartPic_#SEQ(300,3)" ImageType="Png" Palette="none"
                Width="650px">
                <Legends>
                    <asp:Legend BackColor="Transparent" Font="Trebuchet MS, 8.25pt, style=Bold" IsTextAutoFit="False"
                        MaximumAutoSize="20" Name="Default" TitleAlignment="Near" Docking="Bottom">
                    </asp:Legend>
                </Legends>
                <BorderSkin SkinStyle="Emboss" />
                <Series>
                    <asp:Series ChartArea="ChartArea1" ChartType="StackedColumn"  LegendText="其他" Name="C6" IsVisibleInLegend="False" ShadowColor="0, 0, 0, 0">
                    </asp:Series>
                    <asp:Series ChartArea="ChartArea1" ChartType="StackedColumn"  LegendText="管理评审" Name="C5" IsVisibleInLegend="False" ShadowColor="0, 0, 0, 0">
                    </asp:Series>
                    <asp:Series ChartArea="ChartArea1" ChartType="StackedColumn"  LegendText="内审" Name="C4" IsVisibleInLegend="False" ShadowColor="0, 0, 0, 0">
                    </asp:Series>
                    <asp:Series ChartArea="ChartArea1" ChartType="StackedColumn"  LegendText="过程审核" Name="C3" IsVisibleInLegend="False" ShadowColor="0, 0, 0, 0">
                    </asp:Series>
                     <asp:Series ChartArea="ChartArea1" ChartType="StackedColumn" Legend="Default" LegendText="产品审核" Name="C2" IsVisibleInLegend="False" ShadowColor="0, 0, 0, 0">
                        </asp:Series>
                        <asp:Series BorderWidth="3" ChartArea="ChartArea1" ChartType="StackedColumn" LabelBorderWidth="9"
                            Legend="Default" LegendText="客户投诉" MarkerSize="8" MarkerStyle="Circle" Name="C1"
                            ShadowOffset="3" IsVisibleInLegend="False" ShadowColor="0, 0, 0, 0">
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
             <div style=" height:250px; width:100%;  margin: 20px 0;margin-left:10px;overflow-y:auto; overflow-x:auto;">
            <asp:GridView ID="GridViewC" runat="server" BorderColor="lightgray" >
                <EmptyDataTemplate>暂无数据</EmptyDataTemplate>
            </asp:GridView>
                 </div>
        </div>
    </div>
    <%--D--%>

    <div class=" panel panel-info col-lg-6 " style="display:block">
        <div class="panel panel-heading">
            <asp:Label ID="lblD" runat="server" Text="2018"></asp:Label>年员工未完成行动任务数
        </div>
        <div class="panel panel-body">
            <div style="float: left">
               
                <asp:Chart ID="ChartD" runat="server" BackColor="#F3DFC1" BackGradientStyle="TopBottom"
                    BorderColor="181, 64, 1" BorderDashStyle="Solid" BorderWidth="2" Height="250px"
                    ImageLocation="~/TempImages/ChartPic_#SEQ(300,3)" ImageType="Png" Palette="none"
                    Width="650px">
                    <Legends>
                        <asp:Legend BackColor="Transparent" Font="Trebuchet MS, 8.25pt, style=Bold" IsTextAutoFit="False"
                            MaximumAutoSize="20" Name="Default" TitleAlignment="Center" Docking="Bottom" >
                        </asp:Legend>
                    </Legends>
                    <BorderSkin SkinStyle="Emboss" />
                    <Series>
                        <asp:Series ChartArea="ChartArea1" ChartType="StackedColumn" Legend="Default" LegendText="客户投诉" Name="D6" IsVisibleInLegend="False" ShadowColor="0, 0, 0, 0">
                        </asp:Series>
                        <asp:Series ChartArea="ChartArea1" ChartType="StackedColumn" Legend="Default" LegendText="产品审核" Name="D5" IsVisibleInLegend="False" ShadowColor="0, 0, 0, 0">
                        </asp:Series>
                        <asp:Series ChartArea="ChartArea1" ChartType="StackedColumn" Legend="Default" LegendText="过程审核" Name="D4" IsVisibleInLegend="False" ShadowColor="0, 0, 0, 0">
                        </asp:Series>
                        <asp:Series ChartArea="ChartArea1" ChartType="StackedColumn" Legend="Default" LegendText="内审" Name="D3" IsVisibleInLegend="False" ShadowColor="0, 0, 0, 0">
                        </asp:Series>
                        <asp:Series ChartArea="ChartArea1" ChartType="StackedColumn" Legend="Default" LegendText="管理评审" Name="D2" IsVisibleInLegend="False" ShadowColor="0, 0, 0, 0">
                        </asp:Series>
                        <asp:Series BorderWidth="3" ChartArea="ChartArea1" ChartType="StackedColumn" LabelBorderWidth="9"
                            Legend="Default" LegendText="其他" MarkerSize="8" MarkerStyle="Circle" Name="D1"
                            ShadowOffset="3" IsVisibleInLegend="False" ShadowColor="0, 0, 0, 0">
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
              
                <div style=" height:250px; width:900px;  margin: 20px 0;margin-left:10px;overflow-y:auto; overflow-x:auto;">
                <asp:GridView ID="GridViewD" BorderColor="lightgray" runat="server"  EnableViewState="true">
                    <EmptyDataTemplate>暂无数据</EmptyDataTemplate>
                </asp:GridView> </div>
            </div>
            <div>
            </div>
        </div>
    </div>

     <div class=" panel panel-info  col-lg-12 ">
        <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div class="panel panel-heading">
                    <asp:Label ID="lbldetail" runat="server" Text="明细" BorderColor="lightgray"></asp:Label>
                </div>
                <asp:LinkButton ID="LinkDtl" runat="server"
                    OnClick="LinkDtl_Click"  CssClass="hidden">binding</asp:LinkButton>
                <asp:TextBox ID="txtName" runat="server"  CssClass="hidden"/>
                    <asp:TextBox ID="txtType" runat="server" CssClass="hidden" />

                <div class="panel panel-body">

                   <asp:GridView ID="gv1" runat="server" CssClass="GridView1" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" BorderColor="LightGray" PageSize="1000" Width="1900px" OnRowDataBound="gv1_RowDataBound">
                        <Columns>
                             <asp:BoundField DataField="requestid" HeaderText="requestid" ShowHeader="false">
                                <HeaderStyle BackColor="#C1E2EB" Width="0px" Wrap="false" CssClass="hidden" />
                                  <ItemStyle CssClass="hidden" />
                            </asp:BoundField>
                            <asp:BoundField DataField="RequestId" HeaderText="序号">
                                <HeaderStyle BackColor="#C1E2EB" Width="40px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="probdate" HeaderText="问题提<br>出日期" DataFormatString="{0:yyyy/MM/dd}" HtmlEncode="false">
                                <HeaderStyle BackColor="#C1E2EB" Width="60px" Wrap="false"  />
                            </asp:BoundField>
                           
                            <asp:BoundField DataField="probemp" HeaderText="提出人" >
                                <HeaderStyle BackColor="#C1E2EB" Width="60px" Wrap="false" />
                                <ItemStyle />
                            </asp:BoundField>
                            <asp:BoundField DataField="dh" HeaderText="单号">
                                <HeaderStyle BackColor="#C1E2EB" Width="60px" Wrap="false" />
                            </asp:BoundField>
                            <asp:BoundField DataField="domain" HeaderText="公司">
                                <HeaderStyle BackColor="#C1E2EB" Width="50px" Wrap="false" />
                            </asp:BoundField>
                            <asp:BoundField DataField="ProbFrom" HeaderText="问题来源">
                                <HeaderStyle BackColor="#C1E2EB" Width="60px" Wrap="false" />
                            </asp:BoundField>
                            <asp:BoundField DataField="ProdDesc" HeaderText="问题描述">
                                <HeaderStyle BackColor="#C1E2EB" Width="300px" Wrap="false" />
                            </asp:BoundField>
                            <asp:BoundField DataField="CustClass" HeaderText="客户">
                                <HeaderStyle BackColor="#C1E2EB" Width="80px" Wrap="false" />
                                <ItemStyle />
                            </asp:BoundField>
                            <asp:BoundField DataField="ljh" HeaderText="零件号">
                                <HeaderStyle BackColor="#C1E2EB" Width="100px" Wrap="false" />
                                <ItemStyle />
                            </asp:BoundField>

                            <asp:BoundField DataField="ImproveTarget" HeaderText="改善措施及要求"  >
                                <HeaderStyle BackColor="#C1E2EB" Width="200px" Wrap="false" />
                                <ItemStyle />
                            </asp:BoundField>

                              <asp:BoundField DataField="ReqFinishDate" HeaderText="要求完<br>成日期"  DataFormatString="{0:yyyy/MM/dd}"  HtmlEncode="false">
                                <HeaderStyle BackColor="#C1E2EB" Width="80px" Wrap="false" />
                                <ItemStyle />
                            </asp:BoundField>

                              <asp:BoundField DataField="actionplan" HeaderText="计划采取的行动"  >
                                <HeaderStyle BackColor="#C1E2EB" Width="500px" Wrap="false" />
                                <ItemStyle />
                            </asp:BoundField>

                            <asp:BoundField DataField="lastname" HeaderText="问题责<br>任人"  HtmlEncode="false" >
                                <HeaderStyle BackColor="#C1E2EB" Width="60px" Wrap="false" />
                            </asp:BoundField>

                            <asp:BoundField DataField="DutyDept" HeaderText="责任部门" HtmlEncode="false">
                                <HeaderStyle BackColor="#C1E2EB" Width="80px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="ConfirmDate" HeaderText="实际关<br>闭日期" DataFormatString="{0:yyyy/MM/dd}" HtmlEncode="false">
                                <HeaderStyle BackColor="#C1E2EB" Width="60px" Wrap="false" />
                                <ItemStyle />
                            </asp:BoundField>
                            <asp:BoundField DataField="d" HeaderText="用时" >
                                <HeaderStyle BackColor="#C1E2EB" Width="40px" />
                                <ItemStyle HorizontalAlign="Right"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="s1" HeaderText="任务<br>状态" HtmlEncode="false" >
                                <HeaderStyle BackColor="#C1E2EB" Width="70px" Wrap="false" />
                                <ItemStyle />
                            </asp:BoundField>
                           
                             

                        </Columns>
                        <EmptyDataTemplate>no data found</EmptyDataTemplate>
                        <PagerSettings FirstPageText="首页" LastPageText="尾页" Mode="NextPreviousFirstLast" NextPageText="下页" PreviousPageText="上页" />
                        <PagerStyle ForeColor="lightblue" />
                    </asp:GridView>
                    <div style="font-size:12px; color:white">按时完成率=（按时完成数+未关闭(未逾时)）/总数量</div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
     
</asp:Content>
