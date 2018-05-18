<%@ Page Title="产量分析" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="BMW_CELOU_Query.aspx.cs" Inherits="ProductionData_BMW_CELOU_Query" %>

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
    <%--<style>
        .gvHeader th {
            text-align: center;
            background: Silver;
            
            border: solid 1px #333333;
            padding: 0px 5px 0px 5px;
            font-size: 12px;
            width:100px;
            height:20px;
        }
         td{           
            padding: 0px 4px 0px 4px;
            
        }--%>
   <%-- </style>--%>
    <style type="text/css">
         .gvHeader th {
            text-align: center;
            background: Silver;
         }
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

  
    <script src="../Content/js/plugins/layer/layer.min.js" type="text/javascript"></script>
    <script src="../Content/js/jquery.min.js" type="text/javascript"></script>
    <script src="../Content/js/bootstrap.min.js" type="text/javascript"></script>
    <script src="../Content/js/layer/layer.js" type="text/javascript"></script>
    <link href="../Content/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../Content/js/plugins/bootstrap-select/css/bootstrap-select.min.css" rel="stylesheet" />
    <link href="../Content/css/custom.css" rel="stylesheet" />
    <script src="../Content/js/plugins/bootstrap-select/js/bootstrap-select.min.js"type="text/javascript"></script>
     <script type="text/javascript">
         $(document).ready(function () {
             $("#mestitle").text("【BMW/<%=this.location%>/<%=this.m_sorder_id %>统计】");

             $("input[id*='txtDL']").css("display", "none");
             $("select[id*='DDL_OP']").change(function () {
                 bindSelect("DDL_loc");

             })

             ReadyFunction();

             $("td[allowClick=true]").click(function () {
                 $("td").css("background", "");  //其他td为无色
                 $(this).css("background", "orange"); //点击变色。
             })

         })//endready
         function ReadyFunction() {
            
             $("a[name='DL']").click(function () {
                 $("input[id*='txtDL']").val($(this).attr("DL"));
                 $("input[id*='txtDL']").css("display", "none");
             })
         }

         function bindSelect(sel) {
             var OP = $("select[id*=DDL_OP]").val();
             $.ajax({
                 type: "Post", async: false,
                 url: "BMW_CELOU_Query.aspx/GetLocByOP",
                 //方法传参的写法一定要对，str为形参的名字,str2为第二个形参的名字
                 //P1:wlh P2： 
                 data: "{'P1':'" + OP + "','P2':'" + "" + "'}",
                 contentType: "application/json; charset=utf-8",
                 dataType: "json",
                 success: function (data) {//返回的数据用data.d获取内容//                        
                     //alert(data.d)
                     $("select[id*=DDL_loc]").empty();
                     $("select[id*=DDL_loc]").append($("<option>").val("").text("全部"));
                     $.each(eval(data.d), function (i, item) {
                         if (data.d == "") {
                             layer.msg("未获取到工位.");
                         }
                         else {
                             var option = $("<option>").val(item.value).text(item.text);
                             $("select[id*=DDL_loc]").append(option);
                         }
                     })
                 },
                 error: function (err) {
                     layer.alert(err);
                 }
             });

         }

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

                                       <td>生产线: </td>
                                <td> <asp:DropDownList ID="DDL_Line" class="form-control input-s-sm" Style="height: 35px" runat="server" > 
                                    <asp:ListItem Value="">全部</asp:ListItem>
                                    <asp:ListItem>BMW</asp:ListItem>
                                    </asp:DropDownList>
                                                        </td>
                                        <td>
                                    工段:
                                </td>
                                 <td><asp:DropDownList ID="DDL_OP" runat="server" class="form-control input-s-sm ">
                                           </asp:DropDownList></td>

                                           <td>工位: </td>
                                        <td><asp:DropDownList ID="DDL_loc" runat="server" class="form-control input-s-sm ">
                                           </asp:DropDownList></td>
                                        
                                        <td>
                                    员工
                                </td>
                                 <td >
                                      <td> <asp:TextBox ID="txt_emp"  runat="server"  class="form-control input-s-sm " Width="80px"/>

                                  <td>车间: </td>
                                        <td><asp:DropDownList ID="DDL_workshop" runat="server" class="form-control input-s-sm ">
                                            <asp:ListItem Value="">全部</asp:ListItem>
                                            <asp:ListItem>一车间</asp:ListItem>
                                            <asp:ListItem>二车间</asp:ListItem>
                                            <asp:ListItem>三车间</asp:ListItem>
                                           </asp:DropDownList></td>
                                         <td>

                                           <td>班组: </td>
                                        <td><asp:DropDownList ID="DDL_Shift" runat="server" class="form-control input-s-sm ">
                                            <asp:ListItem Value="">全部</asp:ListItem>
                                            <asp:ListItem>C</asp:ListItem>
                                            <asp:ListItem>D</asp:ListItem>
                                           </asp:DropDownList></td>
                                         <td>
                                    <asp:Button ID="Bt_select" runat="server" Text="查询" class="btn btn-large btn-primary" OnClick="Button1_Click" Width="100px" />   
                                             &nbsp;
                                             </td>
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
    <div  class="col-lg-12 ">
        <div class=" panel panel-info col-lg-6 ">
            <div class="panel panel-heading">
                <asp:Label ID="lblMstS" runat="server" Text="生产数量"></asp:Label>
            </div>
            <div class="panel panel-body">
                <div style="float: left">
                   <dx:WebChartControl ID="ChartA" runat="server" CrosshairEnabled="True" 
                    Height="200px" Width="900px">
                </dx:WebChartControl>
                  
                </div>
            </div>
        </div>
     <div class=" panel panel-info col-lg-6 ">
            <div class="panel panel-heading">
                <asp:Label ID="Label2" runat="server" Text="效率"></asp:Label>
            </div>
            <div class="panel panel-body">
                <div style="float: left">
                    <dx:WebChartControl ID="ChartC" runat="server" CrosshairEnabled="True"
                     
                    Height="200px" Width="900px">
                </dx:WebChartControl>
                  
                </div>
            </div>
        </div>
                 
               </div>
      <div  class="col-lg-12 ">
    
       <div class=" panel panel-info col-lg-6 ">
                 <div class="panel panel-heading" style="background-color: #d9edf7">
                    <asp:Label ID="Label4" runat="server" Text="测漏数据" Font-Bold="true" /></div>
               <div class="panel panel-body" style="  overflow:scroll">
                 <asp:GridView ID="gvdetail"  CssClass="gvHeader"  BorderWidth="1"
                     runat="server" 
                     OnRowDataBound="gvdetail_RowDataBound" 
                    OnRowCreated="gvdetail_RowCreated">
                    <EmptyDataTemplate>
            查无数据!</EmptyDataTemplate>
                </asp:GridView>
                </div>

                </div>
                


          <div class=" panel panel-info col-lg-6 ">
                       <div class="panel panel-heading">
                           <asp:Label ID="Label1" runat="server" Text="废品率"></asp:Label>
                       </div>
                       <div class="panel panel-body">
                           <div style="float: left">
                             <dx:WebChartControl ID="ChartB" runat="server" CrosshairEnabled="True"
                    Height="200px" Width="900px">
                </dx:WebChartControl>
                           </div>
                       </div>
                   </div>
   
    </div>
   
     <div class="panel panel-info  col-lg-12">
       

    <div class="panel panel-info  col-lg-12">
     <asp:UpdatePanel runat="server" ID="UpdatePanel2" UpdateMode="Conditional">
            <ContentTemplate>
                 <div class="panel panel-heading" style="background-color: #d9edf7">
                    <asp:Label ID="Label3" runat="server" Text="明细" Font-Bold="true" /></div>
                  <asp:LinkButton ID="LinkBtn" runat="server" OnClick="LinkBtn_Click">binding生产数量明细</asp:LinkButton>
                      <asp:TextBox ID="txtDL" runat="server" />                       
                </div>

                 <asp:GridView ID="GridView1"  CssClass="gvHeader"
                     runat="server" AutoGenerateColumns="False" 
                     onrowcreated="GridView1_RowCreated" ShowFooter="True" 
                     >
                     <Columns>
                         <asp:BoundField DataField="create_date" HeaderText="日期" />
                         <asp:BoundField DataField="emp" HeaderText="工号" />
                         <asp:BoundField DataField="truename" HeaderText="姓名" />
                         <asp:BoundField DataField="shift" HeaderText="班组" />
                         <asp:BoundField DataField="login_date" HeaderText="登入时间" />
                         <asp:BoundField DataField="logout_date" HeaderText="登出时间" />
                         <asp:BoundField DataField="cq_hour" HeaderText="出勤工时">
                         <ItemStyle HorizontalAlign="Right" />
                         </asp:BoundField>
                         <asp:BoundField DataField="line" HeaderText="生产线" />
                         <asp:BoundField DataField="location" HeaderText="工位" />
                         <asp:BoundField DataField="pn" HeaderText="产品" />
                         <asp:BoundField DataField="op" HeaderText="工序" />
                         <asp:BoundField DataField="sc_cs" HeaderText="生产次数">
                         <FooterStyle HorizontalAlign="Right" />
                         <ItemStyle HorizontalAlign="Right" />
                         </asp:BoundField>
                         <asp:BoundField DataField="product_sl" HeaderText="生产数量">
                         <FooterStyle HorizontalAlign="Right" />
                         <ItemStyle HorizontalAlign="Right" />
                         </asp:BoundField>
                         <asp:BoundField DataField="qty_hg" HeaderText="合格数">
                         <FooterStyle HorizontalAlign="Right" />
                         <ItemStyle HorizontalAlign="Right" />
                         </asp:BoundField>
                         <asp:BoundField DataField="qty_fp" HeaderText="不合格数">
                         <FooterStyle HorizontalAlign="Right" />
                         <ItemStyle HorizontalAlign="Right" />
                         </asp:BoundField>
                         <asp:BoundField DataField="fpl" DataFormatString="{0:N0}" 
                             HeaderText="废品率%">
                         <ItemStyle HorizontalAlign="Right" />
                         </asp:BoundField>
                         <asp:BoundField DataField="djgs" HeaderText="单件工时">
                         <ItemStyle HorizontalAlign="Right" />
                         </asp:BoundField>
                         <asp:BoundField DataField="sc_ys" HeaderText="生产用时">
                         <HeaderStyle HorizontalAlign="Right" />
                         <ItemStyle HorizontalAlign="Right" />
                         </asp:BoundField>
                         <asp:BoundField DataField="wc_gs" HeaderText="合计完成工时间">
                         <ItemStyle HorizontalAlign="Right" />
                         </asp:BoundField>
                         <asp:BoundField DataField="sc_xl" DataFormatString="{0:N0}" 
                             HeaderText="生产效率%">
                         <ItemStyle HorizontalAlign="Right" />
                         </asp:BoundField>
                         <asp:BoundField DataField="OEE" HeaderText="OEE">
                         <ItemStyle HorizontalAlign="Right" />
                         </asp:BoundField>
                     </Columns>
                </asp:GridView>
         
       </ContentTemplate>
        </asp:UpdatePanel>  
    </div>
   
    <br />
   
    </div>
</asp:Content>



