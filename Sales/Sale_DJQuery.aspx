<%@ Page Title="【刀具领用趋势】" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Sale_DJQuery.aspx.cs" Inherits="Sales_Sale_DJQuery" %>

<%@ Register Assembly="DevExpress.Web.v17.2, Version=17.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>

 <%@ Register Assembly="DevExpress.XtraCharts.v17.2.Web, Version=17.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.XtraCharts.Web" TagPrefix="dx" %>


<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>

<%@ Register assembly="DevExpress.XtraCharts.v17.2, Version=17.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.XtraCharts" tagprefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <style>
        .gvHeader th {
            text-align: center;
            background: #C1E2EB;
            color: Brown;
            border: solid 1px #333333;
            padding: 0px 5px 0px 5px;
            font-size: 12px;
        }
         td{           
            padding: 0px 4px 0px 4px;
            
        }
    </style>
    <script src="../Content/js/jquery.min.js" type="text/javascript"></script>
    <script src="../Content/js/plugins/layer/layer.min.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript">

        $("#mestitle").text("【刀具领用趋势】");
    </script>
    <div class="row row-container">
        <div class="col-sm-12 ">
            <div class="panel panel-info">
                <div class="panel-heading">
                    <strong>查询</strong>
                    <asp:ScriptManager ID="ScriptManager1" runat="server">
                    </asp:ScriptManager>
                </div>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <div class="panel-body">
                            <div class="col-sm-12">

                                <table>

                                    <tr>
                                        <td>年份:
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="txt_year" runat="server" class="form-control input-s-sm ">
                                            </asp:DropDownList>
                                        </td>
                                        

                                      

                                        <td>&nbsp;工厂:&nbsp;</td>
                                        <td>
                                            <asp:DropDownList ID="ddl_comp" runat="server" class="form-control input-s-sm ">
                                                <asp:ListItem Value="">全部</asp:ListItem>
                                                <asp:ListItem Value="200">昆山</asp:ListItem>
                                                <asp:ListItem Value="100">上海</asp:ListItem>
                                            </asp:DropDownList></td>

                                       <td>刀具类型: </td>
                                <td> <asp:DropDownList ID="DDL_type" class="form-control input-s-sm" Style="height: 35px; width: 150px" runat="server" > </asp:DropDownList>
                                                        </td>
                                        <td>
                                    产品大类:
                                </td>
                                 <td><asp:DropDownList ID="dropdl" runat="server" class="form-control input-s-sm ">
                                           </asp:DropDownList></td>

                                           <td>供应商: </td>
                                        <td> <asp:TextBox ID="txt_gys"  runat="server"  class="form-control input-s-sm "/>
                                        
                                        <td>
                                    使用部门:
                                </td>
                                 <td >
                                     <asp:DropDownList ID="drop_dept" runat="server" class="form-control input-s-sm ">
                                        
                                          <asp:ListItem Value="">全部</asp:ListItem>
                                          <asp:ListItem>工程</asp:ListItem>
                                           <asp:ListItem>生产</asp:ListItem>
                                           </asp:DropDownList></td>

                                  <td>物料号: </td>
                                        <td> <asp:TextBox ID="txt_wlh"  runat="server"  class="form-control input-s-sm " />
                                         <td>
                                    <asp:Button ID="Bt_select" runat="server" Text="查询" class="btn btn-large btn-primary" OnClick="Button1_Click" Width="100px" />   </td>
                                    </tr>

                                     

                                    


                                </table>
                            </div>

                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger ControlID="Bt_select" />

                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
    <div>
        <%--刀具占销售额比率--%>
        <div class=" panel panel-info col-md-12 ">
            <div class="panel panel-heading">
                <asp:Label ID="lblMstMonth" runat="server" Text="按【月】统计"></asp:Label>
            </div>
            <div class="panel panel-body" style="  overflow:scroll">
                <div style="float: left">
                    <dx:WebChartControl ID="ChartA"
                        runat="server" CrosshairEnabled="True" Height="200px"
                        Width="1300px" PaletteName="Civic">
                    </dx:WebChartControl>

                    <dx:ASPxGridView ID="gv_month" runat="server"   
                        onhtmlrowprepared="gv_month_HtmlRowPrepared" 
                        onhtmlrowcreated="gv_month_HtmlRowCreated">
                        <SettingsBehavior AllowSort="False" />
                    </dx:ASPxGridView>

                   <%-- <asp:GridView ID="gv_month" BorderColor="lightgray" BorderWidth="2px" CssClass="gvHeader th" 
                        runat="server" OnRowDataBound="gv_month_RowDataBound">
--%>

                       <%-- <EmptyDataTemplate>
                            查无资料
                        </EmptyDataTemplate>
                    </asp:GridView>--%>
                </div>
            </div>
        </div>
       
    </div>
   
    <br />
   
</asp:Content>



