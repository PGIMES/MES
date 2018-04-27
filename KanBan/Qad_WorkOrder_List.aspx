<%@ Page Title="MES生产管理系统" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="Qad_WorkOrder_List.aspx.cs" Inherits="KanBan_Qad_WorkOrder_List" MaintainScrollPositionOnPostback="True" ValidateRequest="true" %>

<%@ Register Assembly="DevExpress.XtraCharts.v17.2.Web, Version=17.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraCharts.Web" TagPrefix="dx" %>

<%@ Register assembly="DevExpress.XtraCharts.v17.2, Version=17.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.XtraCharts" tagprefix="dx" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
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
   <%-- <link href="../Content/css/bootstrap.min.css" rel="stylesheet" />
    <script src="../Content/js/jquery.min.js"></script>
    <script src="../Content/js/bootstrap.min.js"></script>
    <script src="../Content/js/layer/layer.js"></script>
    <script src="../Content/js/plugins/layer/laydate/laydate.js"></script>
    <link href="../Content/js/plugins/bootstrap-select/css/bootstrap-select.min.css" rel="stylesheet" />
    <script src="../Content/js/plugins/bootstrap-select/js/bootstrap-select.min.js"></script>--%>

     <script src="../Content/js/jquery.min.js" type="text/javascript"></script>
    <script src="../Content/js/bootstrap.min.js"></script>
    <script src="../Content/js/plugins/layer/laydate/laydate.js"></script>
    <script src="../Content/js/plugins/layer/layer.min.js"></script>
    <link href="../Content/js/plugins/bootstrap-select/css/bootstrap-select.min.css" rel="stylesheet" />
    <script src="../Content/js/plugins/bootstrap-select/js/bootstrap-select.min.js"></script>

    <script type="text/javascript">
        $(document).ready(function () {
            $("#mestitle").html("【工单流转状态及父工单看板】<a href='/userguide/reviewGuide.pptx' target='_blank' class='h4' style='display:none'>使用说明</a>");


        })
    </script>
    <script type="text/javascript">
        var popupwindow = null;
        function GetXMH() {
            popupwindow = window.open('../Select/select_XMLJ.aspx', '_blank', 'height=500,width=800,resizable=no,menubar=no,scrollbars =no,location=no');
        }
        function GetEmp() {
            popupwindow = window.open('../Select/select_Emp.aspx?ctrl1=txtProbEmp', '_blank', 'height=500,width=800,resizable=no,menubar=no,scrollbars =no,location=no');
        }
        function setvalue(ctrl0, keyValue0, ctrl1, keyValue1, ctrl2, keyValue2) {

            $("input[id*='" + ctrl0 + "']").val(keyValue0);
            $("input[id*='" + ctrl1 + "']").val(keyValue1);
            $("input[id*='" + ctrl2 + "']").val(keyValue2);
            popupwindow.close();
            $("input[id*='" + ctrl0 + "']").change();
        }

    </script>
    <script type="text/javascript">       

        $(document).ready(function () {
          
               
              
            $("a[name='C']").click(function () {
                $("input[id*='txtName']").val($(this).attr("names"));
                $(" input[id*='txtType']").val($(this).attr("types"));
            })
        })//endready


    </script>
    <style type="text/css">
.Grid
{
    border-collapse: collapse;
    border: solid 1px ;
  
}
.Grid td
{
    border-collapse: collapse;
    border: solid 1px ;
   
}
.Grid th
{
    border-collapse: collapse;
    border: solid 1px ;
  
}
</style>
   <%-- <style type="text/css">
        .row {
            margin-right: 2px;
            margin-left: 2px;
        }

        .textalign {
            text-align: right;
        }

        .alignRight {
            padding-right: 4px;
            text-align: right;
        }

        .row-container {
            padding-left: 2px;
            padding-right: 2px;
        }

        fieldset {
            background: rgba(255,255,255,.3);
            border-color: lightblue;
            border-style: solid;
            border-width: 2px;
            -moz-border-radius: 5px;
            -webkit-border-radius: 5px;
            -khtml-border-radius: 5px;
            border-radius: 5px;
            /*line-height: 30px;*/
            list-style: none;
            padding: 5px 10px;
            margin-bottom: 2px;
        }

        legend {
            color: #302A2A;
            font: bold 16px/2 Verdana, Geneva, sans-serif;
            font-weight: bold;
            text-align: left;
            /*text-shadow: 2px 2px 2px rgb(88, 126, 156);*/
        }

        fieldset {
            padding: .35em .625em .75em;
            margin: 0 2px;
            border: 1px solid lightblue;
        }

        legend {
            padding: 5px;
            border: 0;
            width: auto;
            margin-bottom: 2px;
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
            top: 0px;
            left: 0px;
        }

        td {
            /* vertical-align: top;
            font-weight: 600;*/
            font-size: 12px;
            padding-bottom: 5px;
            padding-left: 3px;
            white-space: nowrap;
        }

        p.MsoListParagraph {
            margin-bottom: .0001pt;
            text-align: justify;
            text-justify: inter-ideograph;
            text-indent: 21.0pt;
            font-size: 10.5pt;
            font-family: "Calibri","sans-serif";
            margin-left: 0cm;
            margin-right: 0cm;
            margin-top: 0cm;
        }

        .his {
            padding-left: 8px;
            padding-right: 8px;
        }

        .tbl td {
            border: 1px solid black;
            padding-left: 3px;
            padding-right: 3px;
            padding-top: 3px;
        }

        .wrap {
            word-break: break-all;
            white-space: normal;
        }
    </style>--%>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
     <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
     <div class="col-md-12" >
     <div class="row  row-container">
            <div class="col-md-12">
                <div class="panel panel-info">
                    <div class="panel-heading" data-toggle="collapse" data-target="#CPXX">
                        <strong>查询条件</strong>
                    </div>
                    <div class="panel-body  collapse in" id="CPXX" >
                         <table>
                <tr>
                    <td style="width: 60px; white-space: nowrap">区域:</td>
                    <td style="width: 80px; white-space: nowrap">
                        <asp:DropDownList ID="txtdomain" runat="server" CssClass="form-control input-s-sm">
                        <asp:ListItem Value="200">200</asp:ListItem>
                        <asp:ListItem Value="100">100</asp:ListItem>
                        </asp:DropDownList>
                      </td>
                    <td style="width: 60px; white-space: nowrap">
                        产品线:</td>
                    <td style="width: 120px; white-space: nowrap">
                                                                                                 <asp:DropDownList ID="txtline" runat="server" CssClass="form-control input-s-sm">
                                                                                                    <asp:ListItem Value="">ALL</asp:ListItem>
                                                                                                          <asp:ListItem Value="1060">铁(上海)</asp:ListItem>
                                                                                                         <asp:ListItem Value="2040">压铸</asp:ListItem>
                                                                                                         <asp:ListItem Value="2050">铝(昆山)</asp:ListItem>
                                                                                                         <asp:ListItem Value="2060">铁(昆山)</asp:ListItem>
                                                                                                      
                                                                                                    </asp:DropDownList> </td>
                    <td style="width: 100px; white-space: nowrap">
                           <asp:Button ID="btnsearch" runat="server" Text="查询" class="btn btn-large btn-primary"  Width="100px" OnClick="btnsearch_Click" />
                    </td></tr></table>

                        </div></div></div></div></div>


<%--   
     <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>--%>
    <div class="col-md-12">
      
        <div class="row  row-container">
            <div class="col-md-12">
                <div class="panel panel-info">
                    <div class="panel-heading" data-toggle="collapse" data-target="#CPXX">
                        <strong>工单流转状态</strong>
                    </div>
                    <div class="panel-body " id="XSGCS">
                        <div class="col-xs-12 col-sm-12  col-md-12 col-lg-12">
                            <div class="panel panel-body">
                             <div>

                                          <asp:GridView ID="gv" runat="server" AutoGenerateColumns="False" Font-Bold="True"   CssClass="Grid">
                                              <Columns>
                                                  <asp:BoundField DataField="ms">
                                                  <ItemStyle Width="250px" Font-Size="14px" />
                                                  </asp:BoundField>
                                                  <asp:BoundField DataField="p" HeaderText="生产" HeaderStyle-Font-Size="14px">
                                                  <ItemStyle Width="120px" Font-Size="14px" />
                                                  </asp:BoundField>
                                                  <asp:BoundField DataField="q" HeaderText="质量" HeaderStyle-Font-Size="14px">
                                                  <ItemStyle Width="120px" Font-Size="14px" />
                                                  </asp:BoundField>
                                                  <asp:BoundField DataField="s" HeaderText="合计" HeaderStyle-Font-Size="14px">
                                                  <ItemStyle Width="120px"  Font-Size="14px"/>
                                                  </asp:BoundField>
                                              </Columns>
                                              <RowStyle Font-Bold="True" />
                                          </asp:GridView>
                                         
                                    </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="row  row-container">
            <div class="col-md-12">
                <div class="panel panel-info">
                    <div class="panel-heading" data-toggle="collapse" data-target="#gscs">
                        <strong style="padding-right: 100px">本周父工单完成情况看板</strong>
                    </div>
                    <div class="panel-body  collapse in" id="gscs">
                        <div class="col-xs-12 col-sm-12  col-md-12 col-lg-12">
                            <div class="panel panel-body">
                               
                                <dx:WebChartControl ID="chart1" runat="server" Height="250px"
        Width="700px" ClientInstanceName="chart"
         CrosshairEnabled="True" 
        SeriesDataMember="t">
        <SeriesTemplate ArgumentDataMember="c" ValueDataMembersSerializable="qty" 
           >
            <ViewSerializable>
                <dx:StackedBarSeriesView></dx:StackedBarSeriesView>
            </ViewSerializable>
            <LabelSerializable>
                <%--<dx:StackedBarSeriesLabel Font="Tahoma, 8pt, style=Bold"  >
                </dx:StackedBarSeriesLabel>--%>

                  <dx:SideBySideBarSeriesLabel Position="Top">
                </dx:SideBySideBarSeriesLabel>

            </LabelSerializable>
        </SeriesTemplate>
        <Legend Direction="BottomToTop">
        </Legend>
        <BorderOptions Visibility="False" />
        <Titles>
           
        </Titles>
        <DiagramSerializable>
            <dx:XYDiagram Rotated="True">
                <AxisX VisibleInPanesSerializable="-1">
                </AxisX>
                <%--<AxisY Title-Text="Millions" Title-Visibility="True" VisibleInPanesSerializable="-1">
                    <Label TextPattern="{V:0,,}"/>
                    <GridLines MinorVisible="True"></GridLines>
                </AxisY>--%>
               <%-- <defaultpane>
                    <stackedbartotallabel textpattern="Total
{TV:0,,.0}" visible="True">
                    </stackedbartotallabel>
                </defaultpane>--%>
            </dx:XYDiagram>
        </DiagramSerializable>
    </dx:WebChartControl>

                               <%-- <dx:WebChartControl ID="chart1" runat="server" Height="250px"
        Width="700px" ClientInstanceName="chart" 
        SeriesDataMember="t" CrosshairEnabled="True"  ToolTipEnabled="False">
        <SeriesTemplate ArgumentDataMember="c" ValueDataMembersSerializable="qty" LabelsVisibility="True"
            LegendName="Default Legend">
            <ViewSerializable>
              
            </ViewSerializable>
            <LabelSerializable>
                
                <dx:SideBySideBarSeriesLabel Position="Top">
                </dx:SideBySideBarSeriesLabel>
                
            </LabelSerializable>
        </SeriesTemplate>
        <Legend Direction="BottomToTop">
        </Legend>
        <BorderOptions Visibility="False" />
        <Titles>
          
        </Titles>
        <DiagramSerializable>
           
<dx:XYDiagram Rotated="True">
<AxisX VisibleInPanesSerializable="-1"></AxisX>

<AxisY VisibleInPanesSerializable="-1"></AxisY>
</dx:XYDiagram>
        </DiagramSerializable>
    </dx:WebChartControl>--%>
                                <br>
                                
                                 <asp:GridView ID="gv2" runat="server" AutoGenerateColumns="False" Font-Bold="True" CssClass="Grid" >
                                              <Columns>
                                                 
                                                  <asp:BoundField DataField="sum_qty" HeaderText="开始数量" HeaderStyle-Font-Size="14px">
                                                  <ItemStyle Width="120px" Font-Size="14px" />
                                                  </asp:BoundField>
                                                  <asp:BoundField DataField="rk_qty" HeaderText="入库数量" HeaderStyle-Font-Size="14px">
                                                  <ItemStyle Width="120px" Font-Size="14px" />
                                                  </asp:BoundField>
                                                  <asp:BoundField DataField="bf_Qty" HeaderText="报废数量" HeaderStyle-Font-Size="14px">
                                                  <ItemStyle Width="120px" Font-Size="14px" />
                                                  </asp:BoundField>
                                                  <asp:BoundField DataField="wwc_qty" HeaderText="未完成数量" HeaderStyle-Font-Size="14px">
                                                  <ItemStyle Width="120px"  Font-Size="14px"/>
                                                  </asp:BoundField>
                                              </Columns>
                                              <RowStyle Font-Bold="True" />
                                          </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
                <asp:TextBox ID="txtName" runat="server"  CssClass="hidden"/>
                    <asp:TextBox ID="txtType" runat="server" CssClass="hidden" />
     <%--   </ContentTemplate>
           </asp:UpdatePanel>--%>
          <asp:LinkButton ID="LinkDtl" runat="server"
                    OnClick="LinkDtl_Click"  CssClass="hidden">binding</asp:LinkButton>
                                       
    </div>
</asp:Content>
