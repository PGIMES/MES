<%@ Page Title="报价系统【重点争取项目分析】" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Baojia_ZhongdianZhengqu_TJ.aspx.cs" Inherits="Baojia_ZhongdianZhengqu_TJ" %>


<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <link href="../Content/css/bootstrap.min.css" rel="stylesheet" />
    <script src="../Content/js/jquery.min.js"></script>
    <script src="../Content/js/bootstrap.min.js"></script>
    <script src="../Content/js/plugins/layer/layer.min.js"></script>
    <script src="../Content/js/plugins/layer/laydate/laydate.js"></script>
    <script type="text/javascript">
        
        $(document).ready(function () {
            $("#mestitle").text("【重点争取项目分析】");
            $("input[id*='txtsale']").css("display", "none");
            $("input[id*='txtCustomer']").css("display", "none");
            $("a[id*='_Link']").css("display", "none");
            $("a[name='sale']").click(function () {
                $("input[id*='txtsale']").val(this.textContent);
            })
            $("a[name='Customer']").click(function () {
                $("input[id*='txtCustomer']").val(this.textContent);
            })

            //整行变色
            $("[id*=GridView] td").bind("click", function () {
                var row = $(this).parent();
                $("[id*=GridView] td").each(function () {
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
            //单元格变色
            $("td[allowClick=true]").click(function () {
                $("td").css("background", "");  //其他td为无色
                $(this).css("background", "orange"); //点击变色。
            })       

        });//EndReady
        ///开启报价跟进维护界面
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
                    if (condition == "S") {
                        __doPostBack('ctl00$MainContent$LinkSale', '');
                    }
                    if (condition == "C") {
                        __doPostBack('ctl00$MainContent$LinkCustomer', '');
                    }
                }
            });
        }

    </script>
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
    </style>
    <script src="../Content/js/jquery.min.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="panel panel-info">
        <table>
            <tr>
                <td>争取级别：
                </td>
                <td>
                    <asp:DropDownList ID="ddl_project_level" runat="server" class="form-control input-s-sm" Width="97px">
                        <asp:ListItem Value="1">重点</asp:ListItem>
                        <asp:ListItem Value="2">一般</asp:ListItem>
                        <asp:ListItem Value="3">全部</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:Button ID="btnQuery" runat="server" Text="确定" class="btn btn-lg btn-primary"
                        Width="92px" Height="45px" OnClick="btnQuery_Click" />
                     <asp:TextBox ID="txtsale" runat="server" />
                    <asp:TextBox ID="txtCustomer" runat="server" />
                </td>

            </tr>
        </table>
    </div>
    <div style="color: red">
        系统说明: 本分析报表只统计所有【争取】中的报价
    </div>
    <div>
    <%--By 销售人员的统计--%>
    <div class=" panel panel-info col-md-6 ">
        <div class="panel panel-heading">
            <asp:Label ID="lblMstS" runat="server" Text="统计"></asp:Label>
        </div>
        <div class="panel panel-body">
            <div style="float: left">
                <asp:Chart ID="ChartA" runat="server" BackColor="#F3DFC1" BackGradientStyle="TopBottom"
                    BorderColor="181, 64, 1" BorderDashStyle="Solid" BorderWidth="2" Height="200px"
                    ImageLocation="~/TempImages/ChartPic_#SEQ(300,3)" ImageType="Png" Palette="none"
                    Width="661px">
                    <Legends>
                        <asp:Legend BackColor="Transparent" Font="Trebuchet MS, 8.25pt, style=Bold" IsTextAutoFit="False"
                            MaximumAutoSize="20" Name="Default" TitleAlignment="Center">
                        </asp:Legend>
                    </Legends>
                    <BorderSkin SkinStyle="Emboss" />
                    <Series>
                        <asp:Series BorderWidth="3" ChartArea="ChartArea1" ChartType="Column" LabelBorderWidth="9"
                            Legend="Default" LegendText="年销售额(万)" MarkerSize="8" MarkerStyle="Circle" Name="A1"
                            ShadowOffset="3">
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
                 <asp:UpdatePanel runat="server" ID="UpdatePanel4" UpdateMode="Conditional">
                    <ContentTemplate>
                       
                        <asp:GridView ID="gv_sale" BorderColor="lightgray" runat="server"
                            AutoGenerateColumns="true" PageSize="200" 
                            onrowcreated="gv_sale_RowCreated" 
                            onrowdatabound="gv_sale_RowDataBound">
                            <EmptyDataTemplate>
                                查无资料</EmptyDataTemplate>
                        </asp:GridView>
                      
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
      <%--By 客户的统计--%>
    <div class=" panel panel-info col-md-6 ">
        <div class="panel panel-heading">
            <asp:Label ID="lblMstC" runat="server" Text="统计"></asp:Label>
        </div>
        <div class="panel panel-body">
            <div style="float: left">
                <asp:Chart ID="ChartB" runat="server" BackColor="#F3DFC1" BackGradientStyle="TopBottom"
                    BorderColor="181, 64, 1" BorderDashStyle="Solid" BorderWidth="2" Height="200px"
                    ImageLocation="~/TempImages/ChartPic_#SEQ(300,3)" ImageType="Png" Palette="none"
                    Width="661px">
                    <Legends>
                        <asp:Legend BackColor="Transparent" Font="Trebuchet MS, 8.25pt, style=Bold" IsTextAutoFit="False"
                            MaximumAutoSize="20" Name="Default" TitleAlignment="Center">
                        </asp:Legend>
                    </Legends>
                    <BorderSkin SkinStyle="Emboss" />
                    <Series>
                        <asp:Series BorderWidth="3" ChartArea="ChartArea1" ChartType="Column" LabelBorderWidth="9"
                            Legend="Default" LegendText="年销售额(万)" MarkerSize="8" MarkerStyle="Circle" Name="B1"
                            ShadowOffset="3">
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
                 <asp:GridView ID="gv_customer" BorderColor="lightgray" runat="server"
                            AutoGenerateColumns="true" PageSize="200" 
                       onrowcreated="gv_customer_RowCreated" 
                       onrowdatabound="gv_customer_RowDataBound">
                            <EmptyDataTemplate>
                                查无资料</EmptyDataTemplate>
                        </asp:GridView>

            </div>
        </div>
    </div>

    </div>
    <div class=" panel panel-info  col-lg-12 ">
          <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
        <div class="panel panel-heading">
            <asp:Label ID="lbldetail" runat="server" Text="明细" BorderColor="lightgray"></asp:Label>
        </div>
      <asp:LinkButton ID="LinkSale" runat="server" 
                    onclick="LinkSale_Click" >binding销售明细</asp:LinkButton>
                <asp:LinkButton ID="LinkCustomer" runat="server" 
                    onclick="LinkCustomer_Click">binding客户明细</asp:LinkButton>
                <div class="panel panel-body">
                    
                    <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" BorderColor="LightGray" OnRowDataBound="GridView1_RowDataBound" OnPageIndexChanging="GridView1_PageIndexChanging" >
                        <Columns>
                            <asp:BoundField DataField="Baojia_no" HeaderText="No.">
                                <HeaderStyle BackColor="#C1E2EB"  />
                            </asp:BoundField>
                            <asp:BoundField DataField="Baojia_no" HeaderText="报价号">
                                <HeaderStyle BackColor="#C1E2EB" Wrap="false" />
                            </asp:BoundField>
                              <asp:TemplateField HeaderText="路径">                                
                                <ItemTemplate>  
                                                            
                                             <a class="fa fa-folder-open" href='<%# Eval("baojia_file_path")%>' target="_blank"></a>                            
                                </ItemTemplate>
                                <HeaderStyle BackColor="#C1E2EB" Width="40px" Wrap="False" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="baojia_start_date" HeaderText="项目开始日期" DataFormatString="{0:yyyy-MM-dd}">
                                <HeaderStyle BackColor="#C1E2EB"  Wrap="false" />
                                <ItemStyle />
                            </asp:BoundField>
                            <asp:BoundField DataField="turns" HeaderText="轮次">
                                <HeaderStyle BackColor="#C1E2EB"  Wrap="false" />
                            </asp:BoundField>
                            <asp:BoundField DataField="customer_name" HeaderText="直接顾客">
                                <HeaderStyle BackColor="#C1E2EB" Wrap="false" />
                            </asp:BoundField>
                            <asp:BoundField DataField="customer_project" HeaderText="项目">
                                <HeaderStyle BackColor="#C1E2EB"  Wrap="false" />
                            </asp:BoundField>
                            <asp:BoundField DataField="ljh" HeaderText="零件号">
                                <HeaderStyle BackColor="#C1E2EB"  Wrap="false" />
                                <ItemStyle />
                            </asp:BoundField>
                            <asp:BoundField DataField="lj_name" HeaderText="零件名称">
                                <HeaderStyle BackColor="#C1E2EB"  Wrap="false" />
                                <ItemStyle />
                            </asp:BoundField>
                            <asp:BoundField DataField="ship_from" HeaderText="ship_from">
                                <HeaderStyle BackColor="#C1E2EB"  Wrap="false" />
                                <ItemStyle />
                            </asp:BoundField>
                             <asp:BoundField DataField="ship_to" HeaderText="ship_to">
                                <HeaderStyle BackColor="#C1E2EB"  Wrap="false" />
                                <ItemStyle />
                            </asp:BoundField>
                            <asp:BoundField DataField="quantity_year" HeaderText="年用量" DataFormatString="{0:N0}" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                <HeaderStyle BackColor="#C1E2EB"  />
                            </asp:BoundField>
                            <asp:BoundField DataField="pc_per_price" HeaderText="批产单价" DataFormatString="{0:N2}" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                <HeaderStyle BackColor="#C1E2EB"  Wrap="false" />
                                <ItemStyle />
                            </asp:BoundField>
                            <asp:BoundField DataField="yj_per_price" HeaderText="样件单价" DataFormatString="{0:N2}" ItemStyle-HorizontalAlign="Right">
                                <HeaderStyle BackColor="#C1E2EB" Wrap="false" />
                            </asp:BoundField>

                            <asp:BoundField DataField="price_year" HeaderText="年销售额(万)" DataFormatString="{0:N0}" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                <HeaderStyle BackColor="#C1E2EB" Wrap="false" />
                            </asp:BoundField>
                              <asp:BoundField DataField="total_price_year" HeaderText="年销售额(合计)" DataFormatString="{0:N0}" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                <HeaderStyle BackColor="#C1E2EB" Wrap="false" />
                            </asp:BoundField>
                            <asp:BoundField DataField="pc_date" HeaderText="批产日期" DataFormatString="{0:yyyy-MM-dd}" >
                                <HeaderStyle BackColor="#C1E2EB" Wrap="false" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="报价跟进">
                                <ItemTemplate>
                                    <i class="fa fa-comments-o fa-lg" aria-hidden="true" param='<%# "?requestid="+Eval("requestid")+"&baojia_no="+Eval("Baojia_No")+"&turns="+Eval("turns")%>'></i>
                                    <span><%# Eval("remarks") %></span>
                                    <%--<img  style="width:30px;height:30px;" src="../Images/start.ico" param='<%# "?requestid="+Eval("requestid")+"&baojia_no="+Eval("Baojia_No")+"&turns="+Eval("turns") %>'/>--%>
                                </ItemTemplate>
                                <HeaderStyle BackColor="#C1E2EB" Width="400px" Wrap="False" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="updateDate" HeaderText="更新时间" DataFormatString="{0:yyyy-MM-dd}">
                                <HeaderStyle BackColor="#C1E2EB"  Wrap="false" />
                                <ItemStyle />
                            </asp:BoundField>
                            <asp:BoundField DataField="updatebyname" HeaderText="更新人">
                                <HeaderStyle BackColor="#C1E2EB"  Wrap="false" />
                            </asp:BoundField>
                            <asp:BoundField DataField="requestid" HeaderText="requestid" ShowHeader="false">
                                <HeaderStyle BackColor="#C1E2EB"  Wrap="false" />
                            </asp:BoundField>
                        </Columns>
                        <PagerSettings FirstPageText="首页" LastPageText="尾页" Mode="NextPreviousFirstLast" NextPageText="下页" PreviousPageText="上页" />
                        <PagerStyle ForeColor="Red" />
                    </asp:GridView>
                </div>
           
    </ContentTemplate>
        </asp:UpdatePanel>
    </div>

</asp:Content>
