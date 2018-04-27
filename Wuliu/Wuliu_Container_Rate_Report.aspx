<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Wuliu_Container_Rate_Report.aspx.cs" Inherits="Wuliu_Wuliu_Container_Rate_Report" EnableViewState="True"
    MaintainScrollPositionOnPostback="true" %>


    <%@ Register Assembly="DevExpress.XtraCharts.v17.2.Web, Version=17.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.XtraCharts.Web" TagPrefix="dx" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <link href="../Content/css/bootstrap.min.css" rel="stylesheet" />
    <script src="../Content/js/jquery.min.js"></script>
    <script src="../Content/js/bootstrap.min.js"></script>
    <script src="../Content/js/plugins/layer/layer.min.js"></script>
    <script src="../Content/js/plugins/layer/laydate/laydate.js"></script>
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

        .panel {
            margin-bottom: 5px;
        }
         .selected_row {
            background-color: #A1DCF2;
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

        .auto-style1 {
            width: 51px;
        }

        .auto-style2 {
            width: 68px;
        }
    </style>
    <script src="../Content/js/jquery.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#mestitle").text("【集装箱利用率统计报表】");
            //隐藏界面上的四个Textbox 和LinkBtn
            $("input[id*='txtmonth']").css("display", "none");
            $("input[id*='txtmonjzx']").css("display", "none");
            $("input[id*='txtweek']").css("display", "none");
            $("input[id*='txtweekjzx']").css("display", "none");
            $("a[id*='_LinkBtn']").css("display", "none");

            $("a[name='weeks']").click(function () {
                $("input[id*='txtweekjzx']").val($(this).attr("jzx_week"));
                $("input[id*='txtweeks']").val($(this).attr("weeks"));
            })

            $("a[name='mon']").click(function () {
                $("input[id*='txtmonjzx']").val($(this).attr("jzx_month"));
                $("input[id*='txtmonth']").val($(this).attr("mon"));
            })


            $("td[allowClick=true]").click(function () {
                $("td").css("background", "");  //其他td为无色
                $(this).css("background", "orange"); //点击变色。
            })



        })//endready
        function getMonth() {
            //  $("#txtmonth").val(this.textContent);
        }
        //界面日历被挑选后，触发该方法将年周赋值给界面的Textbox
        function setValue() {
            var date = $("input[id*='txt_date']").val();
            $.ajax({
                type: "post", //要用post方式                 
                url: "Wuliu_Container_Rate_Report.aspx/GetYearWeek", //方法所在页面和方法名SayHello
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                data: "{date:'" + date + "'}",
                success: function (data) {
                    if (data.d != "") //返回的数据用data.d获取内容Logaction + "成功."
                    {
                        $("input[id*='txt_week']").val(data.d);
                    }
                    else {
                        layer.alert("失败.");
                    }
                },
                error: function (err) {
                    layer.alert(err);
                }
            });
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="panel panel-info">
        <table>
            <tr>
                <td class="auto-style2">日历：
                </td>
                <td class="auto-style1">
                    <input id="txt_date" class="form-control input-s-sm" style="height: 35px; width: 200px" runat="server" onclick="laydate({
    choose: function (dates) {
        setValue();
    }
})"
                        autopostback="True" ontextchanged="txt_date_TextChanged" onchange="txt_date_TextChanged" />
                    <asp:RequiredFieldValidator ID="yz19" runat="server" ControlToValidate="txt_date" ValidationGroup="request" ForeColor="Red"></asp:RequiredFieldValidator>
                </td>
                <td>年周：
                </td>
                <td>
                    <input id="txt_week" type="text" runat="server" class="form-control input-s-sm" style="width: 120px" readonly="readonly" />
                </td>
                <td>计算逻辑：
                </td>
                <td>
                    <asp:DropDownList ID="ddl_type" runat="server" class="form-control input-s-sm">
                        <asp:ListItem Value="1">含价格</asp:ListItem>
                        <asp:ListItem Value="2">不含价格</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:Button ID="btnQuery" runat="server" Text="查询" class="btn btn-lg btn-primary"
                        Width="92px" Height="45px" OnClick="btnQuery_Click" />
                    &nbsp;&nbsp;&nbsp; <a href="../index.aspx" class="btn btn-lg btn-primary" style="color: white">返回</a>

                    <asp:TextBox ID="txtmonth" runat="server" />
                    <asp:TextBox ID="txtweeks" runat="server" />
                    <asp:TextBox ID="txtweekjzx" runat="server" />
                    <asp:TextBox ID="txtmonjzx" runat="server" />
                </td>
            </tr>
        </table>
    </div>
    <%--周综合利用率--%>
    <div class=" panel panel-info col-lg-6 ">
        <div class="panel panel-heading">
            <asp:Label ID="lblYear" runat="server" Text="周综合利用率"></asp:Label>
        </div>
        <div class="panel panel-body">
            <div style="float: left">
                <dx:WebChartControl ID="WebChartControl1"
                    runat="server" CrosshairEnabled="True" Height="200px"
                    Width="600px">
                </dx:WebChartControl>
             

                <asp:GridView ID="GridViewWeek" BorderColor="lightgray" BorderWidth="2px"
                    runat="server" OnRowDataBound="GridViewWeek_RowDataBound">
                </asp:GridView>
            </div>
            <div>
            </div>
        </div>
    </div>
    <%--月综合利用率--%>
    <div class=" panel panel-info  col-lg-6 ">
        <div class="panel panel-heading">
            <asp:Label ID="lblMonth" runat="server" Text="月综合利用率"></asp:Label>
        </div>
        <div class="panel panel-body">
            <dx:WebChartControl ID="WebChartControl2"
                runat="server" CrosshairEnabled="True" Height="200px"
                Width="600px">
            </dx:WebChartControl>
            <asp:GridView ID="GridViewMonth" runat="server" BorderColor="lightgray" BorderWidth="2px" OnRowCreated="GridViewMonth_RowCreated" OnRowDataBound="GridViewMonth_RowDataBound">
            </asp:GridView>
        </div>
    </div>
    <%--集装箱明细1--%>
    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="panel panel-info  col-lg-6">
                <asp:LinkButton ID="LinkBtnWeek" runat="server" OnClick="LinkBtnWeek_Click">binding周明细</asp:LinkButton>
                <asp:LinkButton ID="LinkBtnMonth" runat="server" OnClick="LinkBtnMonth_Click">binding月明细</asp:LinkButton>
                &nbsp;
        <div class="panel panel-heading">
            <asp:Label ID="lblDays" runat="server" Text="集装箱明细:" />
        </div>
                <asp:GridView ID="GridViewSummarys" BorderColor="lightgray" runat="server" AutoGenerateColumns="true" BorderWidth="2px"
                    PageSize="200" OnRowDataBound="GridViewSummarys_RowDataBound">
                </asp:GridView>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <%--明细2--%>
    <div class="panel panel-info  col-lg-6">
        <div class="panel panel-heading">
            <asp:Label ID="lblPartDetails" runat="server" Text="物料号明细:" />
        </div>
        <asp:GridView ID="GridViewPartDetails" BorderColor="lightgray" runat="server" AutoGenerateColumns="true" BorderWidth="2px"
            PageSize="200">
        </asp:GridView>
    </div>
</asp:Content>
