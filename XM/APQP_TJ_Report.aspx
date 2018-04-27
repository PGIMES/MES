<%@ Page Title="【项目任务统计报表】" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="APQP_TJ_Report.aspx.cs" Inherits="XM_APQP_TJ_Report"  EnableViewState="True"
    MaintainScrollPositionOnPostback="true"  %>


    <%@ Register Assembly="DevExpress.XtraCharts.v17.2.Web, Version=17.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.XtraCharts.Web" TagPrefix="dx" %>


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
    <script src="../Content/js/jquery.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#mestitle").text("【项目任务统计报表】");
            $("input[id*='txtmonth']").css("display", "none");
            $("input[id*='txtday']").css("display", "none");
            $("input[id*='txtdelayreason']").css("display", "none");
            $("input[id*='txtdelaymonth']").css("display", "none");
            $("a[id*='_LinkBtn']").css("display", "none");

            $("a[name='mon']").click(function () {
                $("input[id*='txtmonth']").val(this.textContent);
            })
            $("a[name='day']").click(function () {
                $("input[id*='txtday']").val(this.textContent);
            })

            $("a[name='delay']").click(function () {                
                $("input[id*='txtdelayreason']").val($(this).attr("Delaydays"));
                $("input[id*='txtdelaymonth']").val($(this).attr("mon"));
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
                    <asp:DropDownList ID="dropMonth" runat="server" class="form-control input-s-sm" AutoPostBack="True"
                        OnSelectedIndexChanged="dropMonth_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
                <td>
                    月
                </td>
                <td>
                   产品
                </td>
                                        <td>
                                          <asp:DropDownList ID="txtpgi_no" runat="server" class="form-control input-s-sm" >
                    </asp:DropDownList>
                                           </td>
       
                <td>
                    负责人
                </td>
                <td>
                    <input id="txt_zrr" type="text" runat="server" class="form-control input-s-sm"  style="width:120px"/>
                </td>
                <td>
                    部门</td>
                <td>
                      <asp:DropDownList ID="ddl_dept" runat="server" class="form-control input-s-sm" >
                    </asp:DropDownList></td>
                <td>项目工程师</td>
                <td><asp:DropDownList ID="ddl_projectuser" runat="server" class="form-control input-s-sm" >
                    </asp:DropDownList></td>

                <td>
                    <asp:Button ID="btnQuery" runat="server" Text="查询" class="btn btn-lg btn-primary"
                        OnClick="btnQuery_Click" Width="92px" Height="45px" />
                    &nbsp;&nbsp;&nbsp;
                    <asp:Button ID="Button1" runat="server" Text="未完成事项查询" class="btn btn-lg btn-primary"
                        OnClick="Button1_Click" Width="160px" Height="45px" />
                    &nbsp;  <a href="../index.aspx" class="btn btn-lg btn-primary" style="color: white">
                        返回</a><asp:LinkButton ID="LinkBtn" runat="server" OnClick="LinkBtn_Click">binding月明细</asp:LinkButton>
                    <asp:LinkButton ID="LinkBtnDays" runat="server" OnClick="LinkBtnDays_Click">binding日明细</asp:LinkButton>
                     <asp:LinkButton ID="LinkBtnDelay" runat="server" OnClick="LinkBtnDelay_Click">binding延期明细</asp:LinkButton>
                    &nbsp;
                    <asp:TextBox ID="txtmonth" runat="server" />
                    <asp:TextBox ID="txtday" runat="server" />

                     <asp:TextBox ID="txtdelayreason" runat="server" />
                      <asp:TextBox ID="txtdelaymonth" runat="server" />

                </td>
            </tr>
        </table>
    </div>
    <%--年--%>
    <div class=" panel panel-info col-lg-5 " >
        <div class="panel panel-heading">
            <asp:Label ID="lblYear" runat="server" Text="年" ></asp:Label>
        </div>
        <div class="panel panel-body">
            <div style="float: left">
              
                <dx:webchartcontrol ID="WebChartControl1" 
                    runat="server" CrosshairEnabled="True" Height="200px"  
                    Width="600px">
                </dx:webchartcontrol>

              


                <dx:webchartcontrol ID="WebChartControl2" 
                    runat="server" CrosshairEnabled="True" Height="200px" 
                    Width="600px">
                </dx:webchartcontrol>

                <asp:GridView ID="GridViewYear" BorderColor="lightgray" runat="server"  BorderWidth="2px" OnRowCreated="GridViewYear_RowCreated" Font-Size="12px">
                </asp:GridView>
            </div>
            <div>
            </div>
        </div>
    </div>
    <%--月--%>
    <div class=" panel panel-info  col-lg-7 ">
        <div class="panel panel-heading">
            <asp:Label ID="lblMonth" runat="server" Text=""></asp:Label>
        </div>
        <div class="panel panel-body">
          
            <dx:webchartcontrol ID="WebChartControl3" 
                runat="server" CrosshairEnabled="True" Height="200px" 
                Width="600px">
            </dx:webchartcontrol>
            <dx:webchartcontrol ID="WebChartControl4" 
                runat="server" CrosshairEnabled="True" Height="200px" 
                Width="600px">
            </dx:webchartcontrol>
            <asp:GridView ID="GridViewMonth" runat="server" BorderColor="lightgray"  BorderWidth="2px" OnRowCreated="GridViewMonth_RowCreated" Font-Size="12px">
            </asp:GridView>
        </div>
    </div>
    <%--日--%>
    <div class="panel panel-info  col-lg-12">
        <div class="panel panel-heading">
            <asp:Label ID="lblDays" runat="server" Text="明细:" /></div>
        <asp:GridView ID="GridViewDay" BorderColor="lightgray"  BorderWidth="2px" runat="server" AutoGenerateColumns="true"  Font-Size="12px"
            PageSize="200" OnRowDataBound="GridViewDay_RowDataBound">
           
            <EmptyDataTemplate>no data found</EmptyDataTemplate>
        </asp:GridView>
      
    </div>
</asp:Content>


