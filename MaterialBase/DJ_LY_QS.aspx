<%@ Page Title="【刀具领用趋势】" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="DJ_LY_QS.aspx.cs" Inherits="MaterialBase_DJ_LY_QS" %>
  <%@ Register Assembly="DevExpress.XtraCharts.v17.2.Web, Version=17.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.XtraCharts.Web" TagPrefix="dx" %>
<%@ Register assembly="DevExpress.XtraCharts.v17.2, Version=17.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.XtraCharts" tagprefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
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
    </style>
    <script src="../Content/js/jquery.min.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
 <script type="text/javascript">
        $("#mestitle").text("【刀具领用趋势】");
        </script>
    <div class="panel panel-info ">
                <%--      <dx:GridViewDataHyperLinkColumn Caption="物料号" FieldName="物料号"  VisibleIndex="8"  PropertiesHyperLinkEdit-NavigateUrlFormatString="Forproducts.aspx?wlh={0}"  PropertiesHyperLinkEdit-Target="_blank"> 

                            </dx:GridViewDataHyperLinkColumn>--%>

                <div class="panel-body">
                    <div class="col-sm-12">
                        <table class="tblCondition" >
                            <tr>
                                <td>工厂：</td>
                                <td> <asp:DropDownList ID="ddl_comp" runat="server" class="form-control input-s-sm ">
                                          <asp:ListItem Value="">全部</asp:ListItem>
                                          <asp:ListItem Value="200">昆山</asp:ListItem>
                                          <asp:ListItem Value="100">上海</asp:ListItem>
                                           </asp:DropDownList>
                                                        </td>
                                                        <td>
                                    周期:
                                </td>
                                 <td >
                                     <asp:DropDownList ID="drop_period" runat="server" class="form-control input-s-sm ">
                                        
                                          <asp:ListItem Value="W">周</asp:ListItem>
                                          <asp:ListItem Value="D">日</asp:ListItem>
                                           <asp:ListItem Value="M">月</asp:ListItem>
                                           </asp:DropDownList></td>
                                
                                <td>
                                    使用部门:
                                </td>
                                 <td >
                                     <asp:DropDownList ID="drop_dept" runat="server" class="form-control input-s-sm ">
                                        
                                          <asp:ListItem Value="">全部</asp:ListItem>
                                          <asp:ListItem>工程</asp:ListItem>
                                           <asp:ListItem>生产</asp:ListItem>
                                           </asp:DropDownList></td>
                                <td>
                                    <asp:Button ID="Bt_select" runat="server" Text="查询" class="btn btn-large btn-primary" OnClick="Button1_Click" Width="100px" />   </td>
                                    
                           
                            </tr>
                          
                            </table>
                    </div>
                </div>
            </div>
               <div  class="col-lg-12 ">
        <div class=" panel panel-info col-lg-6 ">
            <div class="panel panel-heading">
                <asp:Label ID="lblMstS" runat="server" Text="金额"></asp:Label>
            </div>
            <div class="panel panel-body">
                <div style="float: left">
                   <dx:WebChartControl ID="ChartA" runat="server" CrosshairEnabled="True" 
                    Height="300px" Width="900px">
                </dx:WebChartControl>
                  
                </div>
            </div>
        </div>
    <%--按刀具类型统计--%>
                   <div class=" panel panel-info col-lg-6 ">
                       <div class="panel panel-heading">
                           <asp:Label ID="Label1" runat="server" Text="一年内不同刀具类型刀具领用金额"></asp:Label>
                       </div>
                       <div class="panel panel-body">
                           <div style="float: left">
                             <dx:WebChartControl ID="ChartB" runat="server" CrosshairEnabled="True"
                    Height="300px" Width="1000px">
                </dx:WebChartControl>
                           </div>
                       </div>
                   </div>
               </div>
      <div  class="col-lg-12 ">
        <div class=" panel panel-info col-lg-6 ">
            <div class="panel panel-heading">
                <asp:Label ID="Label2" runat="server" Text="一年内不同产品大类刀具领用金额"></asp:Label>
            </div>
            <div class="panel panel-body">
                <div style="float: left">
                    <dx:WebChartControl ID="ChartC" runat="server" CrosshairEnabled="True"
                     
                    Height="300px" Width="900px">
                </dx:WebChartControl>
                  
                </div>
            </div>
        </div>
    <%--按供应商统计--%>
        <div class=" panel panel-info col-lg-6 ">
            <div class="panel panel-heading">
                <asp:Label ID="Label3" runat="server" Text="一年内不同供应商刀具领用金额"></asp:Label>
            </div>
            <div class="panel panel-body">
                <div style="float: left">
                   
                    <dx:WebChartControl ID="ChartD" runat="server" CrosshairEnabled="True"
                    Height="300px" Width="1000px" 
                       >
                </dx:WebChartControl>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
