<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="ForcastByMnth_Forwuliu.aspx.cs" Inherits="Product_ForcastByMnth_Forwuliu" %>

<%@ Register Assembly="DevExpress.XtraCharts.v17.2.Web, Version=17.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.XtraCharts.Web" TagPrefix="dx" %>

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
        .tdstyle
        {
            background-color:LightBlue;
            text-align:center;
        }
    </style>
    <script src="../Content/js/jquery.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#mestitle").text("【月销售预测】");

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
                    <asp:DropDownList ID="dropsite" runat="server" class="form-control input-s-sm">
                         <asp:ListItem Value="">全部</asp:ListItem>
                        <asp:ListItem>昆山工厂</asp:ListItem>
                        <asp:ListItem>上海工厂</asp:ListItem>

                    </asp:DropDownList>
                </td>
                <td>
                    统计项：
                </td>
                <td>
                    <asp:DropDownList ID="droptype" runat="server" class="form-control input-s-sm">
                        <asp:ListItem Value="0">销售数量</asp:ListItem>
                        <asp:ListItem Value="1">销售金额</asp:ListItem>
                    </asp:DropDownList>
                </td>
                 <td>
                    发货点：
                </td>
                <td>
                    <asp:DropDownList ID="dropfrom" runat="server" class="form-control input-s-sm">  
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:Button ID="btnQuery" runat="server" Text="查询" class="btn btn-lg btn-primary"
                        OnClick="btnQuery_Click" Width="92px" Height="45px" />
                  
                   
                </td>

                  <td>
                    <asp:Button ID="btnexport" runat="server" Text="导出" class="btn btn-lg btn-primary"
                        Width="92px" Height="45px" onclick="btnexport_Click" />
                  
                   
                </td>
                  
            </tr>
        </table>
    </div>
   
    
    <%--明细--%>
    <div class="panel panel-info  col-lg-12">
        <asp:UpdatePanel runat="server" ID="UpdatePanel1" UpdateMode="Conditional">
            <ContentTemplate>
                
                <div class="panel panel-heading" style="background-color: #d9edf7">
                    <asp:Label ID="lblDays" runat="server" Text="明细" Font-Bold="true" /></div>
              
              
                </div>
                  <asp:GridView ID="gv_month" BorderColor="lightgray"   style=" word-break:keep-all " 
                    runat="server" BorderWidth=1
                        AutoGenerateColumns="true" PageSize="200" OnRowCreated="gv_month_RowCreated"
                        OnRowDataBound="gv_month_RowDataBound" 
                    ShowFooter="True">
                        <EmptyDataTemplate>
                            查无资料</EmptyDataTemplate>
                    </asp:GridView>
            </ContentTemplate>
        </asp:UpdatePanel>  
       
    </div>
</asp:Content>
 



