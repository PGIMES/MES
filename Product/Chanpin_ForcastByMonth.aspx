<%@ Page Title="月销售预测" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Chanpin_ForcastByMonth.aspx.cs" Inherits="Product_Chanpin_ForcastByMonth" %>

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
                        <asp:ListItem></asp:ListItem>
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
                    <asp:Button ID="btnQuery" runat="server" Text="查询" class="btn btn-lg btn-primary"
                        OnClick="btnQuery_Click" Width="92px" Height="45px" />
                  
                   
                </td>
                   <td>
                    <asp:Button ID="Button1" runat="server" Text="导出" class="btn btn-lg btn-primary"
                        Width="92px" Height="45px" onclick="Button1_Click" />
                  
                   
                </td>  
            </tr>
        </table>
    </div>
    <%--按年度统计销售预测--%>
    <div  class="col-lg-12 ">
        
    <%--按月份统计--%>
        <div class=" panel panel-info col-lg-12 ">
            <div class="panel panel-heading">
                <asp:Label ID="Label1" runat="server" Text="按月统计"></asp:Label>
            </div>
            <div >
                <div style="float: left">
                 <dx:WebChartControl ID="ChartA"
                    runat="server" CrosshairEnabled="True" Height="300px" Width="1000px"
                    PaletteName="Civic">
                </dx:WebChartControl>
                   
                   <asp:GridView ID="gv_month" BorderColor="lightgray" runat="server" BorderWidth=1
                        AutoGenerateColumns="true" PageSize="200" OnRowCreated="gv_month_RowCreated"
                        OnRowDataBound="gv_month_RowDataBound">
                        <EmptyDataTemplate>
                            查无资料</EmptyDataTemplate>
                    </asp:GridView>
                
                  
                </div>

               
            </div>
        </div>
    </div>
    
    <%--明细--%>
    <div class="panel panel-info  col-lg-12">
        <asp:UpdatePanel runat="server" ID="UpdatePanel1" UpdateMode="Conditional">
            <ContentTemplate>
                
                <div class="panel panel-heading" style="background-color: #d9edf7">
                    <asp:Label ID="lblDays" runat="server" Text="明细" Font-Bold="true" /></div>
              
              
                </div>

                 <asp:GridView ID="GridView2" runat="server" CssClass="GridView2" 
                        AutoGenerateColumns="False" 
                    BorderColor="LightGray" HeaderStyle-BackColor="LightBlue" 
                    OnRowDataBound="GridView2_RowDataBound" 
                    OnPageIndexChanging="GridView2_PageIndexChanging">
                        <Columns>
                            <asp:BoundField DataField="pgino" HeaderText="序号" />
                            <asp:BoundField DataField="product_status" 
                                HeaderText="产品状态" />
                            <asp:BoundField DataField="pgino" HeaderText="项目号" />
                            <asp:BoundField DataField="version" HeaderText="版本号" />
                            <asp:BoundField DataField="QAD_pt_part" HeaderText="物料号" />
                            <asp:BoundField DataField="productcode" HeaderText="零件号" />
                            <asp:BoundField DataField="customer_name" 
                                HeaderText="客户名称" />
                            <asp:BoundField DataField="customer_project" 
                                HeaderText="顾客项目" />
                            <asp:BoundField DataField="ship_from" 
                                HeaderText="ship_from" />
                            <asp:BoundField DataField="ship_to" HeaderText="ship_to" />
                            <asp:BoundField DataField="khdm" HeaderText="客户代码" />
                            <asp:BoundField DataField="pc_date" 
                                DataFormatString="{0:yyyy/MM/dd}" HeaderText="批产日期" />
                            <asp:BoundField DataField="end_date" 
                                DataFormatString="{0:yyyy/MM/dd}" HeaderText="停产日期" />
                            <asp:BoundField DataField="pc_dj_qad" HeaderText="单价" />
                            <asp:BoundField DataField="quantity_year" 
                                HeaderText="最大年用量" DataFormatString="{0:N0}" />

                            <asp:BoundField DataField="requestid" >

                            <ControlStyle CssClass="hidden" Width="0px" />
                            <FooterStyle CssClass="hidden" Width="0px" />
                            <HeaderStyle CssClass="hidden" Width="0px" />
                            <ItemStyle CssClass="hidden" Width="0px" />
                            </asp:BoundField>

                            <asp:TemplateField HeaderText="1月">
                              
                                <FooterStyle />
                                <ItemStyle />
                                <HeaderTemplate>
                                    <table align="center" width="100%">
                                       
                                                <asp:Label ID="Label_M1" runat="server" Text="1月"></asp:Label>
                                         
                                        <tr>
                                            <td class="tdstyle" runat="server" id="LA1">
                                                <asp:Label ID="lb_A_1" runat="server" Text="A" Width="60px"></asp:Label>
                                                </td>
                                                <td  class="tdstyle"  runat="server" id="LF1">
                                                <asp:Label ID="lb_F_1" runat="server" Text="F" Width="60px"></asp:Label>
                                                </td>
                                                <td  class="tdstyle"  runat="server" id="LRF1">
                                                <asp:Label ID="lb_RF_1" runat="server" Text="RF" Width="60px"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <table width="100%">
                                        <tr>
                                            <td id="A1" runat="server">
                                                <asp:Label ID="QTY_A_1" runat="server" style=" text-align:right"
                                                    ReadOnly="true" Text='<%#DataBinder.Eval(Container,"DataItem.A1","{0:N0}") %>'
                                                    Width="60px"></asp:Label>
                                                   </td>
                                                   <td id="F1" runat="server">
                                                <asp:Label ID="QTY_F_1" runat="server"  style=" text-align:right"
                                                     Text='<%#DataBinder.Eval(Container,"DataItem.F1","{0:N0}") %>'
                                                    Width="60px"></asp:Label>
                                                    </td><td id="RF1" runat="server">
                                                <asp:Label ID="QTY_RF_1" runat="server" 
                                                    style=" text-align:right" 
                                                    Text='<%#DataBinder.Eval(Container,"DataItem.RF1","{0:N0}") %>'
                                                    Width="60px"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="2月">
                               
                               
                                <FooterStyle />
                                <ItemStyle />
                                <HeaderTemplate>
                                    <table align="center" width="100%">
                                       
                                                <asp:Label ID="Label_M2" runat="server" Text="2月"></asp:Label>
                                          
                                        <tr>
                                            <td  class="tdstyle"  runat="server" id="LA2">
                                                <asp:Label ID="lb_A_2" runat="server" Text="A" Width="60px"></asp:Label>
                                                </td>
                                                <td  class="tdstyle"  runat="server" id="LF2">
                                                <asp:Label ID="lb_F_2" runat="server" Text="F" Width="60px"></asp:Label>
                                                </td>
                                                 <td  class="tdstyle"  runat="server" id="LRF2">
                                                <asp:Label ID="lb_RF_2" runat="server" Text="RF" Width="60px"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <table width="100%">
                                        <tr>
                                             <td id="A2" runat="server">
                                                <asp:Label ID="QTY_A_2" runat="server" style=" text-align:right"
                                                    ReadOnly="true" Text='<%#DataBinder.Eval(Container,"DataItem.A2","{0:N0}") %>'
                                                    Width="60px"></asp:Label>
                                                    </td>
                                                     <td id="F2" runat="server">
                                                <asp:Label ID="QTY_F_2" runat="server"  style=" text-align:right"
                                                     Text='<%#DataBinder.Eval(Container,"DataItem.F2","{0:N0}") %>'
                                                    Width="60px"></asp:Label>
                                                    </td>
                                                     <td id="RF2" runat="server">
                                                <asp:Label ID="QTY_RF_2" runat="server" 
                                                    style=" text-align:right"
                                                    Text='<%#DataBinder.Eval(Container,"DataItem.RF2","{0:N0}") %>'
                                                    Width="60px"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                            </asp:TemplateField>

                             <asp:TemplateField HeaderText="3月">
                            
                               
                                <FooterStyle />
                                <ItemStyle />
                                <HeaderTemplate>
                                    <table align="center" width="100%">
                                      
                                                <asp:Label ID="Label_M3" runat="server" Text="3月"></asp:Label>
                                            
                                        <tr>
                                             <td class="tdstyle"  runat="server" id="LA3">
                                                <asp:Label ID="lb_A_3" runat="server" Text="A" Width="60px"></asp:Label>
                                                </td>
                                                  <td class="tdstyle"  runat="server" id="LF3">
                                                <asp:Label ID="lb_F_3" runat="server" Text="F" Width="60px"></asp:Label>
                                                </td>
                                                 <td class="tdstyle"  runat="server" id="LRF3">
                                                <asp:Label ID="lb_RF_3" runat="server" Text="RF" Width="60px"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <table width="100%">
                                        <tr>
                                            <td id="A3" runat="server">
                                                <asp:Label ID="QTY_A_3" runat="server" style=" text-align:right"
                                                    ReadOnly="true" Text='<%#DataBinder.Eval(Container,"DataItem.A3","{0:N0}") %>'
                                                    Width="60px"></asp:Label>
                                                    </td>
                                                   <td id="F3" runat="server">
                                                <asp:Label ID="QTY_F_3" runat="server"  style=" text-align:right"
                                                     Text='<%#DataBinder.Eval(Container,"DataItem.F3","{0:N0}") %>'
                                                    Width="60px"></asp:Label>
                                                    </td>
                                                    <td id="RF3" runat="server">
                                                <asp:Label ID="QTY_RF_3" runat="server" 
                                                    style=" text-align:right"
                                                    Text='<%#DataBinder.Eval(Container,"DataItem.RF3","{0:N0}") %>'
                                                    Width="60px"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="4月">
                              
                               
                                <FooterStyle />
                                <ItemStyle />
                                <HeaderTemplate>
                                    <table align="center" width="100%">
                                       
                                                <asp:Label ID="Label_M4" runat="server" Text="4月"></asp:Label>
                                            
                                        <tr>
                                             <td class="tdstyle"  runat="server" id="LA4">
                                                <asp:Label ID="lb_A_4" runat="server" Text="A" Width="60px"></asp:Label>
                                                </td>
                                                <td class="tdstyle"   runat="server" id="LF4">
                                                <asp:Label ID="lb_F_4" runat="server" Text="F" Width="60px"></asp:Label>
                                                </td>
                                                 <td class="tdstyle"  runat="server" id="LRF4">
                                                <asp:Label ID="lb_RF_4" runat="server" Text="RF" Width="60px"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <table width="100%">
                                        <tr>
                                             <td id="A4" runat="server">
                                                <asp:Label ID="QTY_A_4" runat="server" style=" text-align:right"
                                                    ReadOnly="true" Text='<%#DataBinder.Eval(Container,"DataItem.A4","{0:N0}") %>'
                                                    Width="60px"></asp:Label></td>
                                                   <td id="F4" runat="server">
                                                <asp:Label ID="QTY_F_4" runat="server" style=" text-align:right"
                                                     Text='<%#DataBinder.Eval(Container,"DataItem.F4","{0:N0}") %>'
                                                    Width="60px"></asp:Label></td>
                                                     <td id="RF4" runat="server">
                                                <asp:Label ID="QTY_RF_4" runat="server" 
                                                   style=" text-align:right"
                                                    Text='<%#DataBinder.Eval(Container,"DataItem.RF4","{0:N0}") %>'
                                                    Width="60px"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                            </asp:TemplateField>

                             <asp:TemplateField HeaderText="5月">
                               <%-- <FooterTemplate>
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Lab_QTY_A_5" runat="server" BackColor="#CCCCCC"
                                                    CssClass="textalign" ForeColor="Red" ReadOnly="True" Width="60px"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="Lab_QTY_F_5" runat="server" BackColor="#CCCCCC"
                                                    CssClass="textalign" ForeColor="Red" ReadOnly="True" Width="60px"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="Lab_QTY_RF_5" runat="server" BackColor="#CCCCCC"
                                                    CssClass="textalign" ForeColor="Red" ReadOnly="True" Width="60px"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </FooterTemplate>--%>
                               
                                <FooterStyle />
                                <ItemStyle />
                                <HeaderTemplate>
                                    <table align="center" width="100%">
                                       
                                                <asp:Label ID="Label_M5" runat="server" Text="5月"></asp:Label>
                                            
                                        <tr>
                                              <td class="tdstyle"  runat="server" id="LA5">
                                                <asp:Label ID="lb_A_5" runat="server" Text="A" Width="60px"></asp:Label>
                                                </td>
                                                 <td class="tdstyle"  runat="server" id="LF5">
                                                <asp:Label ID="lb_F_5" runat="server" Text="F" Width="60px"></asp:Label>
                                                </td>
                                                  <td class="tdstyle"  runat="server" id="LRF5">
                                                <asp:Label ID="lb_RF_5" runat="server" Text="RF" Width="60px"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <table width="100%">
                                        <tr>
                                            <td id="A5" runat="server">
                                                <asp:Label ID="QTY_A_5" runat="server" style=" text-align:right"
                                                    ReadOnly="true" Text='<%#DataBinder.Eval(Container,"DataItem.A5","{0:N0}") %>'
                                                    Width="60px"></asp:Label>
                                                    </td>
                                                     <td id="F5" runat="server">
                                                <asp:Label ID="QTY_F_5" runat="server"  style=" text-align:right"
                                                     Text='<%#DataBinder.Eval(Container,"DataItem.F5","{0:N0}") %>'
                                                    Width="60px"></asp:Label>
                                                    </td>
                                                    <td id="RF5" runat="server">
                                                <asp:Label ID="QTY_RF_5" runat="server" 
                                                   style=" text-align:right"
                                                    Text='<%#DataBinder.Eval(Container,"DataItem.RF5","{0:N0}") %>'
                                                    Width="60px"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="6月">
                              
                               
                                <FooterStyle />
                                <ItemStyle />
                                <HeaderTemplate>
                                    <table align="center" width="100%" >
                                      
                                                <asp:Label ID="Label_M6" runat="server" Text="6月"></asp:Label>
                                            
                                        <tr>
                                            <td class="tdstyle"  runat="server" id="LA6">
                                                <asp:Label ID="lb_A_6" runat="server" Text="A" Width="60px"></asp:Label>
                                                </td>
                                                <td  class="tdstyle"  runat="server" id="LF6">
                                                <asp:Label ID="lb_F_6" runat="server" Text="F" Width="60px"></asp:Label>
                                                </td>
                                               <td class="tdstyle"  runat="server" id="LRF6">
                                                <asp:Label ID="lb_RF_6" runat="server" Text="RF" Width="60px"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <table width="100%">
                                        <tr>
                                           <td id="A6" runat="server">
                                                <asp:Label ID="QTY_A_6" runat="server" style=" text-align:right"
                                                    ReadOnly="true" Text='<%#DataBinder.Eval(Container,"DataItem.A6","{0:N0}") %>'
                                                    Width="60px"></asp:Label></td>
                                           <td id="F6" runat="server">
                                                <asp:Label ID="QTY_F_6" runat="server"  style=" text-align:right"
                                                     Text='<%#DataBinder.Eval(Container,"DataItem.F6","{0:N0}") %>'
                                                    Width="60px"></asp:Label></td>
                                          <td id="RF6" runat="server">
                                                <asp:Label ID="QTY_RF_6" runat="server" 
                                                   style=" text-align:right"
                                                    Text='<%#DataBinder.Eval(Container,"DataItem.RF6","{0:N0}") %>'
                                                    Width="60px"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="7月">
                             
                               
                                <FooterStyle />
                                <ItemStyle />
                                <HeaderTemplate>
                                    <table align="center" width="100%">
                                        
                                                <asp:Label ID="Label_M7" runat="server" Text="7月"></asp:Label>
                                            
                                        <tr>
                                             <td class="tdstyle"  runat="server" id="LA7">
                                                <asp:Label ID="lb_A_7" runat="server" Text="A" Width="60px"></asp:Label>
                                                </td>
                                                <td  class="tdstyle"  runat="server" id="LF7">

                                                <asp:Label ID="lb_F_7" runat="server" Text="F" Width="60px"></asp:Label>
                                                </td>
                                               <td class="tdstyle"  runat="server" id="LRF7">
                                                <asp:Label ID="lb_RF_7" runat="server" Text="RF" Width="60px"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <table width="100%">
                                        <tr>
                                             <td id="A7" runat="server">
                                                <asp:Label ID="QTY_A_7" runat="server" style=" text-align:right"
                                                    ReadOnly="true" Text='<%#DataBinder.Eval(Container,"DataItem.A7","{0:N0}") %>'
                                                    Width="60px"></asp:Label></td>
                                              <td id="F7" runat="server">
                                                <asp:Label ID="QTY_F_7" runat="server"  style=" text-align:right"
                                                     Text='<%#DataBinder.Eval(Container,"DataItem.F7","{0:N0}") %>'
                                                    Width="60px"></asp:Label>
                                                    </td>
                                             <td id="RF7" runat="server">
                                                <asp:Label ID="QTY_RF_7" runat="server" 
                                                    style=" text-align:right"
                                                    Text='<%#DataBinder.Eval(Container,"DataItem.RF7","{0:N0}") %>'
                                                    Width="60px"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="8月">
                              
                               
                                <FooterStyle />
                                <ItemStyle />
                                <HeaderTemplate>
                                    <table align="center" width="100%">
                                       
                                                <asp:Label ID="Label_M8" runat="server" Text="8月"></asp:Label>
                                           
                                        <tr>
                                            <td class="tdstyle"  runat="server" id="LA8">
                                                <asp:Label ID="lb_A_8" runat="server" Text="A" Width="60px"></asp:Label>
                                                </td>
                                                <td  class="tdstyle" runat="server" id="LF8">
                                                <asp:Label ID="lb_F_8" runat="server" Text="F" Width="60px"></asp:Label>
                                                </td>
                                             <td class="tdstyle" runat="server" id="LRF8">
                                                <asp:Label ID="lb_RF_8" runat="server" Text="RF" Width="60px"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <table width="100%">
                                        <tr>
                                             <td id="A8" runat="server">
                                                <asp:Label ID="QTY_A_8" runat="server" style=" text-align:right"
                                                    ReadOnly="true" Text='<%#DataBinder.Eval(Container,"DataItem.A8","{0:N0}") %>'
                                                    Width="60px"></asp:Label></td>
                                             <td id="F8" runat="server">
                                                <asp:Label ID="QTY_F_8" runat="server"  style=" text-align:right"
                                                     Text='<%#DataBinder.Eval(Container,"DataItem.F8","{0:N0}") %>'
                                                    Width="60px"></asp:Label></td>
                                             <td id="RF8" runat="server">
                                                <asp:Label ID="QTY_RF_8" runat="server" 
                                                    style=" text-align:right"
                                                    Text='<%#DataBinder.Eval(Container,"DataItem.RF8","{0:N0}") %>'
                                                    Width="60px"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                            </asp:TemplateField>

                             <asp:TemplateField HeaderText="9月">
                               
                                <FooterStyle />
                                <ItemStyle />
                                <HeaderTemplate>
                                    <table align="center" width="100%">
                                        
                                                <asp:Label ID="Label_M9" runat="server" Text="9月"></asp:Label>
                                            
                                        <tr>
                                            <td class="tdstyle" runat="server" id="LA9">
                                                <asp:Label ID="lb_A_9" runat="server" Text="A" Width="60px"></asp:Label>
                                                </td>
                                                <td  class="tdstyle" runat="server" id="LF9">
                                                <asp:Label ID="lb_F_9" runat="server" Text="F" Width="60px"></asp:Label>
                                                </td>
                                                <td class="tdstyle" runat="server" id="LRF9">
                                                <asp:Label ID="lb_RF_9" runat="server" Text="RF" Width="60px"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <table width="100%">
                                        <tr>
                                             <td id="A9" runat="server">
                                                <asp:Label ID="QTY_A_9" runat="server" style=" text-align:right"
                                                    ReadOnly="true" Text='<%#DataBinder.Eval(Container,"DataItem.A9","{0:N0}") %>'
                                                    Width="60px"></asp:Label></td>
                                             <td id="F9" runat="server">
                                                <asp:Label ID="QTY_F_9" runat="server"  style=" text-align:right"
                                                     Text='<%#DataBinder.Eval(Container,"DataItem.F9","{0:N0}") %>'
                                                    Width="60px"></asp:Label></td>
                                             <td id="RF9" runat="server">
                                                <asp:Label ID="QTY_RF_9" runat="server" 
                                                    style=" text-align:right"
                                                    Text='<%#DataBinder.Eval(Container,"DataItem.RF9","{0:N0}") %>'
                                                    Width="60px"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="10月">
                               <%-- <FooterTemplate>
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Lab_QTY_A_10" runat="server" BackColor="#CCCCCC"
                                                    CssClass="textalign" ForeColor="Red" ReadOnly="True" Width="60px"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="Lab_QTY_F_10" runat="server" BackColor="#CCCCCC"
                                                    CssClass="textalign" ForeColor="Red" ReadOnly="True" Width="60px"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="Lab_QTY_RF_10" runat="server" BackColor="#CCCCCC"
                                                    CssClass="textalign" ForeColor="Red" ReadOnly="True" Width="60px"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </FooterTemplate>--%>
                               
                                <FooterStyle />
                                <ItemStyle />
                                <HeaderTemplate>
                                    <table align="center" width="100%">
                                      
                                                <asp:Label ID="Label_M10" runat="server" Text="10月"></asp:Label>
                                            
                                        <tr>
                                            <td class="tdstyle" runat="server" id="LA10">
                                                <asp:Label ID="lb_A_10" runat="server" Text="A" Width="60px"></asp:Label>
                                                </td>
                                                <td class="tdstyle" runat="server" id="LF10">
                                                <asp:Label ID="lb_F_10" runat="server" Text="F" Width="60px"></asp:Label>
                                                </td>
                                                 <td class="tdstyle" runat="server" id="LRF10">
                                                <asp:Label ID="lb_RF_10" runat="server" Text="RF" Width="60px"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <table width="100%">
                                        <tr>
                                             <td id="A10" runat="server">
                                                <asp:Label ID="QTY_A_10" runat="server" style=" text-align:right"
                                                    ReadOnly="true" Text='<%#DataBinder.Eval(Container,"DataItem.A10","{0:N0}") %>'
                                                    Width="60px"></asp:Label></td>
                                             <td id="F10" runat="server">
                                                <asp:Label ID="QTY_F_10" runat="server"  style=" text-align:right"
                                                     Text='<%#DataBinder.Eval(Container,"DataItem.F10","{0:N0}") %>'
                                                    Width="60px"></asp:Label></td>
                                             <td id="RF10" runat="server">
                                                <asp:Label ID="QTY_RF_10" runat="server" 
                                                    style=" text-align:right"
                                                    Text='<%#DataBinder.Eval(Container,"DataItem.RF10","{0:N0}") %>'
                                                    Width="60px"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="11月">
                               
                                <FooterStyle />
                                <ItemStyle />
                                <HeaderTemplate>
                                    <table align="center" width="100%">
                                       
                                                <asp:Label ID="Label_M11" runat="server" Text="11月"></asp:Label>
                                           
                                        <tr>
                                            <td class="tdstyle" runat="server" id="LA11">
                                                <asp:Label ID="lb_A_11" runat="server" Text="A" Width="60px"></asp:Label>
                                                </td>
                                                <td class="tdstyle" runat="server" id="LF11">
                                                <asp:Label ID="lb_F_11" runat="server" Text="F" Width="60px"></asp:Label></td>
                                                 <td class="tdstyle" runat="server" id="LRF11">
                                                <asp:Label ID="lb_RF_11" runat="server" Text="RF" Width="60px"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <table width="100%">
                                        <tr>
                                              <td id="A11" runat="server">
                                                <asp:Label ID="QTY_A_11" runat="server" style=" text-align:right"
                                                    ReadOnly="true" Text='<%#DataBinder.Eval(Container,"DataItem.A11","{0:N0}") %>'
                                                    Width="60px"></asp:Label></td>
                                               <td id="F11" runat="server">
                                                <asp:Label ID="QTY_F_11" runat="server"  style=" text-align:right"
                                                     Text='<%#DataBinder.Eval(Container,"DataItem.F11","{0:N0}") %>'
                                                    Width="60px"></asp:Label></td>
                                                <td id="RF11" runat="server"> 
                                                <asp:Label ID="QTY_RF_11" runat="server" 
                                                   style=" text-align:right"
                                                    Text='<%#DataBinder.Eval(Container,"DataItem.RF11","{0:N0}") %>'
                                                    Width="60px"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                            </asp:TemplateField>

                             <asp:TemplateField HeaderText="12月">
                              
                                <FooterStyle />
                                <ItemStyle />
                                <HeaderTemplate>
                                    <table align="center" width="100%">
                                       
                                                <asp:Label ID="Label_M12" runat="server" Text="12月"></asp:Label>
                                           
                                        <tr>
                                            <td class="tdstyle" runat="server" id="LA12">
                                                <asp:Label ID="lb_A_12" runat="server" Text="A" Width="60px"></asp:Label>
                                                </td>
                                           <td class="tdstyle" runat="server" id="LF12">
                                                <asp:Label ID="lb_F_12" runat="server" Text="F" Width="60px"></asp:Label>
                                                </td>
                                           <td class="tdstyle" runat="server" id="LRF12">
                                                <asp:Label ID="lb_RF_12" runat="server" Text="RF" Width="60px"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <table width="100%">
                                        <tr>
                                             <td id="A12" runat="server">
                                                <asp:Label ID="QTY_A_12" runat="server"  style=" text-align:right"
                                                    ReadOnly="true" Text='<%#DataBinder.Eval(Container,"DataItem.A12","{0:N0}") %>  '
                                                    Width="60px"></asp:Label>
                                                    </td>
                                               <td id="F12" runat="server">
                                                <asp:Label ID="QTY_F_12" runat="server"  style=" text-align:right"
                                                     Text='<%#DataBinder.Eval(Container,"DataItem.F12","{0:N0}") %>'
                                                    Width="60px"></asp:Label>
                                                    </td>
                                                 <td id="RF12" runat="server">
                                                <asp:Label ID="QTY_RF_12" runat="server"  style=" text-align:right"
                                                     
                                                    Text='<%#DataBinder.Eval(Container,"DataItem.RF12","{0:N0}") %>'
                                                    Width="60px"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <EmptyDataTemplate>no data found</EmptyDataTemplate>
                        <HeaderStyle BackColor="LightBlue" />
                        <PagerSettings FirstPageText="首页" LastPageText="尾页" Mode="NextPreviousFirstLast" NextPageText="下页" PreviousPageText="上页" />
                        
                    </asp:GridView>

            </ContentTemplate>
        </asp:UpdatePanel>  
       
    </div>
</asp:Content>
 


