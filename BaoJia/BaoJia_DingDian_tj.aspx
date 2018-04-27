<%@ Page Title="报价系统【定点项目分析】" Language="C#" MasterPageFile="~/Site.master"
    AutoEventWireup="true" CodeFile="baojia_dingdian_tj.aspx.cs"
    Inherits="baojia_dingdian_tj" EnableViewState="True"
    MaintainScrollPositionOnPostback="true" %>

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
        
          td {
            padding-left: 5px;
            padding-right: 5px;
        }
        .auto-style1 {
            width: 100px;
        }
        .tblCondition td{ white-space:nowrap }
    </style>
      <script src="../Content/js/jquery.min.js" type="text/javascript"></script>
    <script src="../Content/js/bootstrap.min.js"></script>
    <script src="../Content/js/plugins/layer/laydate/laydate.js"></script>
    <script src="../Content/js/plugins/layer/layer.min.js"></script>
    <link href="../Content/js/plugins/bootstrap-select/css/bootstrap-select.min.css" rel="stylesheet" />
    <script src="../Content/js/plugins/bootstrap-select/js/bootstrap-select.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#mestitle").text("【定点项目分析】");

            jQuery.fn.rowspan = function (colIdx) {//封装jQuery小插件用于合并相同内容单元格(列)  
                return this.each(function () {
                    var that;
                    $('tr', this).each(function (row) {
                        $('td:eq(' + colIdx + ')', this).filter(':visible').each(function (col) {
                            if (that != null && $(this).html() == $(that).html()) {
                                rowspan = $(that).attr("rowSpan");
                                if (rowspan == undefined) {
                                    $(that).attr("rowSpan", 1);
                                    rowspan = $(that).attr("rowSpan");
                                }
                                rowspan = Number(rowspan) + 1;
                                $(that).attr("rowSpan", rowspan);
                                $(this).hide();
                            } else {
                                that = this;
                            }
                        });
                    });
                });
            }

            $("input[id*='txtmonth']").css("display", "none");
            $("input[id*='txtsale']").css("display", "none");
            $("input[id*='txtCustomer']").css("display", "none");
            // $("a[id*='_LinkBtn']").css("display", "none");

            $("a[name='month']").click(function () {

                // $("input[id*='txtmonth']").val($(this).attr("month"));
                $("input[id*='txtmonth']").val(this.textContent);
            })
            $("a[name='sale']").click(function () {

                $("input[id*='txtsale']").val(this.textContent);
            })
            $("a[name='Customer']").click(function () {
                $("input[id*='txtCustomer']").val(this.textContent);
            })



            //单元格变色
            $("[id*=gv_] td").bind("click", function () {
                var row = $(this).parent();
                $("[id*=gv_] td").each(function () {
                    if ($(this)[0] != row[0]) {
                        $("td", this).removeClass("selected_row");
                    }
                });
                $("td", row).each(function () {
                    if (!$(this).hasClass("selected_row")) {
                        $(this).addClass("selected_row");
                    } else {
                        $(this).removeClass("selected_row");
                    }
                });
            });

            $("td[allowClick=true]").click(function () {
                $("td").css("background", "");  //其他td为无色
                $(this).css("background", "orange"); //点击变色。
            })

        })//endready

//        function ReadyFunction() {
//            $("[param]").click(function () {
//                param = $(this).attr('param');
//                layer.open({
//                    type: 2,
//                    skin: 'layui-layer-demo', //样式类名
//                    closeBtn: 1, //显示关闭按钮
//                    anim: 2,
//                    title: ['报价跟踪记录', false],
//                    area: ['800px', '650px'],
//                    shadeClose: true, //开启遮罩关闭
//                    content: 'BaoJia_Remark_flow.aspx' + param,
//                    end: function () { }
//                });

//            });

        function OpenMsg(ele, condition) {
            param = $(ele).attr('param');
            layer.open({
                type: 2,
                skin: 'layui-layer-demo', //样式类名
                closeBtn: 1, //显示关闭按钮
                anim: 2,
                title: ['报价跟踪记录', false],
                area: ['800px', '650px'],
                shadeClose: true, //开启遮罩关闭
                content: 'BaoJia_Remark_flow.aspx' + param,
                end: function () {
                    if (condition == "M") {
                        __doPostBack('ctl00$MainContent$LinkBtn', '');
                    }
                    if (condition == "C") {
                        __doPostBack('ctl00$MainContent$LinkCustomer', '');
                    }
                    if (condition == "S") {
                        __doPostBack('ctl00$MainContent$LinkSale', '');
                    }
                }
            });
        }

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
                    定点日期
                </td>
                <td>
                    <asp:DropDownList ID="dropYear" runat="server" class="form-control input-s-sm">
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:Button ID="btnQuery" runat="server" Text="查询" class="btn btn-lg btn-primary"
                        OnClick="btnQuery_Click" Width="92px" Height="45px" />
                    &nbsp;&nbsp;&nbsp; &nbsp;
                    <asp:TextBox ID="txtmonth" runat="server" />
                    <asp:TextBox ID="txtsale" runat="server" />
                    <asp:TextBox ID="txtCustomer" runat="server" />
                </td>
            </tr>
        </table>
    </div>
    <%--年--%>
  
         <div class=" panel panel-info col-lg-4 ">
        <div class="panel panel-heading">
            <asp:Label ID="lblMstS" runat="server" Text="按月份统计年销售额(万)"></asp:Label>
        </div>
       
        <div class="panel panel-body" style="  overflow:scroll">
            <div style="float: left">
                <asp:Chart ID="ChartByMonth" runat="server" BackColor="#F3DFC1"
                    BackGradientStyle="TopBottom" BorderColor="181, 64, 1" BorderDashStyle="Solid"
                    BorderWidth="2" Height="200px" ImageLocation="~/TempImages/ChartPic_#SEQ(300,3)"
                    ImageType="Png" Palette="none" Width="450px">
                    <Legends>
                        <asp:Legend BackColor="Transparent" Font="Trebuchet MS, 8.25pt, style=Bold"
                            IsTextAutoFit="False" MaximumAutoSize="0" Name="Default"
                            TitleAlignment="Center" Docking="Top" 
                            IsDockedInsideChartArea="False" >
                        </asp:Legend>
                    </Legends>
                    <BorderSkin SkinStyle="Emboss" />
                    <Series>
                        <asp:Series BorderWidth="3" ChartArea="ChartArea1" LabelBorderWidth="9"
                            Legend="Default" LegendText="" MarkerSize="8" Name="年销售额"
                            ShadowOffset="3">
                        </asp:Series>
                    </Series>
                    <ChartAreas>
                        <asp:ChartArea BackColor="64, 165, 191, 228" BackGradientStyle="TopBottom"
                            BackSecondaryColor="White" BorderColor="64, 64, 64, 64" BorderDashStyle="Solid"
                            Name="ChartArea1" ShadowColor="Transparent">
                            <Area3DStyle Inclination="15" IsClustered="False" IsRightAngleAxes="False"
                                Perspective="10" Rotation="10" WallWidth="0" />
                            <AxisY LineColor="64, 64, 64, 64" TitleAlignment="Far">
                                <LabelStyle Font="Trebuchet MS, 8.25pt, style=Bold" />
                                <MajorGrid LineColor="64, 64, 64, 64" />
                            </AxisY>
                            <AxisY2>
                                <LabelStyle Font="Trebuchet MS, 18.25pt, style=Bold" />
                                <MajorGrid LineColor="4, 64, 4, 64" />
                            </AxisY2>
                            <AxisX Interval="1" LineColor="64, 64, 64, 64">
                                <LabelStyle Font="Trebuchet MS, 8.25pt, style=Bold" />
                                <MajorGrid LineColor="64, 64, 64, 64" />
                            </AxisX>
                        </asp:ChartArea>
                    </ChartAreas>
                </asp:Chart>
                
                        <asp:GridView ID="gv" BorderColor="lightgray" runat="server"
                            AutoGenerateColumns="true" PageSize="200" 
                            onrowcreated="gv_RowCreated" 
                            onrowdatabound="gv_RowDataBound">
                            <EmptyDataTemplate>
                                查无资料</EmptyDataTemplate>
                        </asp:GridView>
                   
            </div>
            </div>
            </div>
              <div class=" panel panel-info col-lg-4 ">
        <div class="panel panel-heading">
            <asp:Label ID="Label1" runat="server" Text="按销售统计年销售额(万)"></asp:Label>
        </div>
        <div class="panel panel-body">
               <div style="float: left">
                <asp:Chart ID="Chart1" runat="server" BackColor="#F3DFC1"
                    BackGradientStyle="TopBottom" BorderColor="181, 64, 1" BorderDashStyle="Solid"
                    BorderWidth="2" Height="200px" ImageLocation="~/TempImages/ChartPic_#SEQ(300,3)"
                    ImageType="Png" Palette="none" Width="450px">
                    <Legends>
                        <asp:Legend BackColor="Transparent" Font="Trebuchet MS, 8.25pt, style=Bold"
                            IsTextAutoFit="False" MaximumAutoSize="0" Name="Default"
                            TitleAlignment="Center" Docking="Top" Title="">
                        </asp:Legend>
                    </Legends>
                    <BorderSkin SkinStyle="Emboss" />
                    <Series>
                        <asp:Series BorderWidth="3" ChartArea="ChartArea1" LabelBorderWidth="9"
                            Legend="Default" LegendText="" MarkerSize="8" Name="年销售额"
                            ShadowOffset="3">
                        </asp:Series>
                    </Series>
                    <ChartAreas>
                        <asp:ChartArea BackColor="64, 165, 191, 228" BackGradientStyle="TopBottom"
                            BackSecondaryColor="White" BorderColor="64, 64, 64, 64" BorderDashStyle="Solid"
                            Name="ChartArea1" ShadowColor="Transparent">
                            <Area3DStyle Inclination="15" IsClustered="False" IsRightAngleAxes="False"
                                Perspective="10" Rotation="10" WallWidth="0" />
                            <AxisY LineColor="64, 64, 64, 64" TitleAlignment="Far">
                                <LabelStyle Font="Trebuchet MS, 8.25pt, style=Bold" />
                                <MajorGrid LineColor="64, 64, 64, 64" />
                            </AxisY>
                            <AxisY2>
                                <LabelStyle Font="Trebuchet MS, 18.25pt, style=Bold" />
                                <MajorGrid LineColor="4, 64, 4, 64" />
                            </AxisY2>
                            <AxisX Interval="1" LineColor="64, 64, 64, 64">
                                <LabelStyle Font="Trebuchet MS, 8.25pt, style=Bold" />
                                <MajorGrid LineColor="64, 64, 64, 64" />
                            </AxisX>
                        </asp:ChartArea>
                    </ChartAreas>
                </asp:Chart>
                
                
                        <asp:GridView ID="gv_sale" BorderColor="lightgray" runat="server"
                            AutoGenerateColumns="true" PageSize="200" 
                            onrowcreated="gv_sale_RowCreated" 
                            onrowdatabound="gv_sale_RowDataBound">
                            <EmptyDataTemplate>
                                查无资料</EmptyDataTemplate>
                        </asp:GridView>
            </div>
            </div></div>
              <div class=" panel panel-info col-lg-4 ">
        <div class="panel panel-heading">
            <asp:Label ID="Label2" runat="server" Text="按直接顾客统计年销售额(万)"></asp:Label>
        </div>
        <div class="panel panel-body">
               <div style="float: left">
                <asp:Chart ID="Chart2" runat="server" BackColor="#F3DFC1"
                    BackGradientStyle="TopBottom" BorderColor="181, 64, 1" BorderDashStyle="Solid"
                    BorderWidth="2" Height="200px" ImageLocation="~/TempImages/ChartPic_#SEQ(300,3)"
                    ImageType="Png" Palette="none" Width="450px">
                    <Legends>
                        <asp:Legend BackColor="Transparent" Font="Trebuchet MS, 8.25pt, style=Bold"
                            IsTextAutoFit="False" MaximumAutoSize="0" Name="Default"
                            TitleAlignment="Center" Docking="Top" Title="">
                        </asp:Legend>
                    </Legends>
                    <BorderSkin SkinStyle="Emboss" />
                    <Series>
                        <asp:Series BorderWidth="3" ChartArea="ChartArea1" LabelBorderWidth="9"
                            Legend="Default" LegendText="" MarkerSize="8" Name="年销售额"
                            ShadowOffset="3">
                        </asp:Series>
                    </Series>
                    <ChartAreas>
                        <asp:ChartArea BackColor="64, 165, 191, 228" BackGradientStyle="TopBottom"
                            BackSecondaryColor="White" BorderColor="64, 64, 64, 64" BorderDashStyle="Solid"
                            Name="ChartArea1" ShadowColor="Transparent">
                            <Area3DStyle Inclination="15" IsClustered="False" IsRightAngleAxes="False"
                                Perspective="10" Rotation="10" WallWidth="0" />
                            <AxisY LineColor="64, 64, 64, 64" TitleAlignment="Far">
                                <LabelStyle Font="Trebuchet MS, 8.25pt, style=Bold" />
                                <MajorGrid LineColor="64, 64, 64, 64" />
                            </AxisY>
                            <AxisY2>
                                <LabelStyle Font="Trebuchet MS, 18.25pt, style=Bold" />
                                <MajorGrid LineColor="4, 64, 4, 64" />
                            </AxisY2>
                            <AxisX Interval="1" LineColor="64, 64, 64, 64">
                                <LabelStyle Font="Trebuchet MS, 8.25pt, style=Bold" />
                                <MajorGrid LineColor="64, 64, 64, 64" />
                            </AxisX>
                        </asp:ChartArea>
                    </ChartAreas>
                </asp:Chart>
                <asp:UpdatePanel runat="server" ID="UpdatePanel2" UpdateMode="Conditional">
                    <ContentTemplate>
                
                <asp:GridView ID="gv_customer" BorderColor="lightgray" runat="server"
                            AutoGenerateColumns="true" PageSize="200" 
                       onrowcreated="gv_customer_RowCreated" 
                       onrowdatabound="gv_customer_RowDataBound">
                            <EmptyDataTemplate>
                                查无资料</EmptyDataTemplate>
                        </asp:GridView>
                       </ContentTemplate>
                </asp:UpdatePanel>

         </div></div>
            </div>
            
        
    
    <%--明细--%>
    <div class="panel panel-info  col-lg-12">
        <asp:UpdatePanel runat="server" ID="UpdatePanel1" UpdateMode="Conditional">
            <ContentTemplate>
            <div class="panel panel-heading" style="background-color:#d9edf7">
            <asp:Label ID="lblDays" runat="server" Text="定点项目"  Font-Bold="true"/></div>
        
                <asp:LinkButton ID="LinkBtn" runat="server" OnClick="LinkBtn_Click">binding月明细</asp:LinkButton>
                <asp:LinkButton ID="LinkSale" runat="server" 
                    onclick="LinkSale_Click" >binding销售明细</asp:LinkButton>
                <asp:LinkButton ID="LinkCustomer" runat="server" 
                    onclick="LinkCustomer_Click">binding客户明细</asp:LinkButton>
                <asp:GridView ID="gvdetail" BorderColor="LightGray" runat="server"
                    AutoGenerateColumns="False" OnRowDataBound="gvdetail_RowDataBound"
                    OnRowCreated="gvdetail_RowCreated" ShowFooter="True" 
                    onpageindexchanging="gvdetail_PageIndexChanging">
                    <Columns>
                        <asp:BoundField HeaderText="序号" DataField="Baojia_no"/>
                        <asp:BoundField DataField="Baojia_no" HeaderText="报价号">
                                <HeaderStyle  Width="80px" Wrap="false" />
                            </asp:BoundField>
                             <asp:TemplateField HeaderText="路径">                                
                                <ItemTemplate>  
                                                            
                                             <a class="fa fa-folder-open" href='<%# Eval("baojia_file_path")%>' target="_blank"></a>                            
                                </ItemTemplate>
                                <HeaderStyle Width="40px" Wrap="False" />
                            </asp:TemplateField>
                        <asp:BoundField DataField="baojia_start_date" DataFormatString="{0:yyyy-MM-dd}"
                            HeaderText="项目开始日期" >
                             <HeaderStyle  Wrap="false" /> </asp:BoundField>
                        <asp:BoundField DataField="turns" HeaderText="轮数" >
                             <HeaderStyle  Wrap="false" />
                              </asp:BoundField>
                        <asp:BoundField DataField="end_customer_name" 
                            HeaderText="直接顾客" >
                             <HeaderStyle  Wrap="false" /> </asp:BoundField>
                        <asp:BoundField DataField="customer_project" HeaderText="项目" />
                        <asp:BoundField DataField="ljh" HeaderText="零件号" >
                             <HeaderStyle  Wrap="false" /> </asp:BoundField>
                          <asp:BoundField DataField="lj_name" HeaderText="零件名称">
                                <HeaderStyle  Wrap="false" />
                                <ItemStyle />
                            </asp:BoundField>
                        <asp:BoundField DataField="ship_from"  
                            HeaderText="ship_from" />
                        <asp:BoundField DataField="ship_to" HeaderText="ship_to" />
                        <asp:BoundField DataField="quantity_year" HeaderText="年用量" 
                            DataFormatString="{0:N0}" >
                             <HeaderStyle  Wrap="false" /> 
                            <FooterStyle HorizontalAlign="Right" />
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="yj_per_price" HeaderText="单价" 
                            DataFormatString="{0:f2}">
                            <FooterStyle HorizontalAlign="Right" />
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="price_year" HeaderText="年销售额(万)" 
                            DataFormatString="{0:N0}">
                              <HeaderStyle  Wrap="false" /> 
                            <FooterStyle HorizontalAlign="Right" />
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="年销售额合计(万)" 
                            DataFormatString="{0:N0}" >
                              <HeaderStyle  Wrap="false" /> 
                        <FooterStyle HorizontalAlign="Right" />
                        <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="mj_price" 
                            DataFormatString="{0:N0}" HeaderText="模具价格(万)">
                              <HeaderStyle  Wrap="false" /> 
                        <FooterStyle HorizontalAlign="Right" />
                        <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="dingdian_date" HeaderText="定点日期" 
                            DataFormatString="{0:yyyy-MM-dd}" >
                        <HeaderStyle Wrap="False" />
                        </asp:BoundField>
                        <asp:BoundField DataField="pc_date" 
                            DataFormatString="{0:yyyy-MM-dd}" HeaderText="批产日期"  >
                             <HeaderStyle  Wrap="false" />
                              </asp:BoundField>
                        <asp:BoundField DataField="hetong_complet_date" HeaderText="结束时间"
                            DataFormatString="{0:yyyy-MM-dd}" >
                             <HeaderStyle  Wrap="false" />
                              </asp:BoundField>
                            <asp:TemplateField HeaderText="跟进" >
                                <ItemTemplate>
                                    <i class="fa fa-comments-o fa-lg" aria-hidden="true" style="cursor: pointer" param='<%# "?requestid="+Eval("requestid")+"&baojia_no="+Eval("Baojia_No")+"&turns="+Eval("turns")%>'></i>
                                          <span><%# Eval("remarks") %></span>                                  
                                </ItemTemplate>
                                <HeaderStyle  Width="150px" Wrap="False" />
                               
                            </asp:TemplateField>
                        <asp:BoundField DataField="requestid">
                        <ControlStyle CssClass="hidden" Width="0px" />
                        <FooterStyle CssClass="hidden" Width="0px" />
                        <HeaderStyle CssClass="hidden" Width="0px" />
                        <ItemStyle CssClass="hidden" Width="0px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="create_date" HeaderText="更新时间" 
                            DataFormatString="{0:yyyy-MM-dd}" >
                             <HeaderStyle  Wrap="false" />
                              </asp:BoundField>
                        <asp:BoundField DataField="create_by_name" 
                            HeaderText="更新人" >
                             <HeaderStyle  Wrap="false" />
                              </asp:BoundField>
                        <asp:BoundField DataField="sales_name" HeaderText="销售负责">
                             <HeaderStyle  Wrap="false" />
                              </asp:BoundField>
                    </Columns>
                    
                </asp:GridView>
           
    </div> </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
