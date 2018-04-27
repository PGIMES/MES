<%@ Page Title="【刀具分析--产品】" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="DJ_analyse_byproduct.aspx.cs" Inherits="GongCheng_DJ_analyse_byproduct" MaintainScrollPositionOnPostback="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
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
            overflow:scroll;
           
           
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

    <style>
    .gvHeader th{    background: #C1E2EB;    color:  Brown;    border: solid 1px #333333;    padding:0px 5px 0px 5px; font-size:12px;}
   .bootstrap-select > .dropdown-toggle {
            width: 150px;
        }

        .bootstrap-select:not([class*=col-]):not([class*=form-control]):not(.input-group-btn) {
            width: 120px;
        }
</style>
          <script src="../Content/js/jquery.min.js" type="text/javascript"></script>
    <script src="../Content/js/bootstrap.min.js"></script>
    <script src="../Content/js/plugins/layer/laydate/laydate.js"></script>
    <script src="../Content/js/plugins/layer/layer.min.js"></script>
    <link href="../Content/js/plugins/bootstrap-select/css/bootstrap-select.min.css" rel="stylesheet" />
    <script src="../Content/js/plugins/bootstrap-select/js/bootstrap-select.min.js"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">


 <script type="text/javascript">
     $("#mestitle").text("【刀具分析--产品】");
     $(document).ready(function () {
         $('.selectpicker').change(function () 
         {
             if (this.id.indexOf("Status") >= 0) 
             {
                 $("input[id*='txt_product_status']").val($("[class='selectpicker A']").val());

             }
            
         });
     });
    </script>







    <div class="row row-container">
        <div class="col-sm-12 ">
            <div class="panel panel-info">
                <div class="panel-heading">
                    <strong>刀具分析</strong>
                    <asp:ScriptManager ID="ScriptManager1" runat="server">
                    </asp:ScriptManager>
                </div>
              
                <div class="panel-body"  >
                    <div class="col-sm-12">
                       
                                <table>
                                    
                                    <tr>
                                      <td>公司: </td>
                                      <td> 
                                      <asp:DropDownList ID="ddl_comp" runat="server" class="form-control input-s-sm ">
                                          <asp:ListItem>200</asp:ListItem>
                                          <asp:ListItem>100</asp:ListItem>
                                           </asp:DropDownList></td>  
                                        <td>
                                            年:
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="txt_syear" runat="server" class="form-control input-s-sm " Width="100px" >
                                            </asp:DropDownList>
                                        </td>
                                          <td > 月: </td>
                                        <td >
                    <asp:DropDownList ID="txt_smnth" runat="server"  Width="100px"
                        class="form-control input-s-sm" >
                    </asp:DropDownList>
                </td>
                                        <td>
                                            &nbsp;~年:</td>
                                        <td>
                                            <asp:DropDownList ID="txt_eyear" runat="server" 
                                                class="form-control input-s-sm " Width="100px">
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            月:
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="txt_emnth" runat="server" Width="100px"
                                                class="form-control input-s-sm">
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            产品大类:</td>
                                        <td>
                                            <asp:DropDownList ID="txt_cpdl" runat="server" 
                                                class="form-control input-s-sm">
                                                <asp:ListItem></asp:ListItem>
                                                <asp:ListItem>铁</asp:ListItem>
                                                <asp:ListItem>铝</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td style=" display:none">
                                            产品状态:</td>
                                        <td style=" display:none">
                                            <asp:DropDownList ID="status" runat="server" Width="100px"
                                                class="form-control input-s-sm">
                                            </asp:DropDownList>
                                        </td>
                     
                                    </tr>

                                     
                                    <tr>
                                        <td>
                                            产品类别: </td>
                                        <td>
                                            <asp:DropDownList ID="txt_type" runat="server" 
                                                class="form-control input-s-sm">
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            刀具类型:</td>
                                        <td>
                                            <asp:DropDownList ID="txt_djlx" runat="server" 
                                                class="form-control input-s-sm">
                                                <asp:ListItem>消耗品</asp:ListItem>
                                                <asp:ListItem>非消耗品</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            项目号:</td>
                                        <td>
                                            <input id="txt_xmh" class="form-control input-s-sm"   style=" width:100px"
                                                runat="server" />
                                        </td>
                                        <td >
                                            刀具物料号:</td>
                                        <td>
                                            <input id="txt_part" class="form-control input-s-sm"  style=" width:100px;"
                                                runat="server" />
                                        </td>
                                        <td>
                                            显示项目数:</td>
                                        <td>
                                            <asp:DropDownList ID="txt_bs" runat="server" 
                                                class="form-control input-s-sm" Width="100px">
                                                <asp:ListItem>10</asp:ListItem>
                                                <asp:ListItem>20</asp:ListItem>
                                                <asp:ListItem>30</asp:ListItem>
                                                <asp:ListItem>40</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        
                                        <td>
                                            <asp:Button ID="Button2" runat="server" Text="查询"  
                                              class="btn btn-large btn-primary" 
                                              Width="100px" onclick="Button2_Click" />
                                        </td>
                                        <td>
                                            <asp:HyperLink ID="HyperLink1" runat="server" 
                                                NavigateUrl="~/GongCheng/刀具报表计算逻辑.xlsx">计算公式</asp:HyperLink>
                                        </td>
                                    </tr>

                                     
                                    <tr>
                                        <td>
                                            查询类别:</td>
                                        <td>
                                            <asp:DropDownList ID="txt_lb" runat="server" 
                                                class="form-control input-s-sm">
                                                <asp:ListItem>产品</asp:ListItem>
                                                <asp:ListItem>产品组</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            产品组:</td>
                                        <td>
                                            <asp:DropDownList ID="txt_cpz" runat="server" 
                                                class="form-control input-s-sm">
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            组织:</td>
                                        <td>
                                            <asp:DropDownList ID="txt_zuzhi" runat="server" 
                                                class="form-control input-s-sm" Width="100px" 
                                                AutoPostBack="True" onselectedindexchanged="txt_zuzhi_SelectedIndexChanged"  
                                               >
                                                <asp:ListItem></asp:ListItem>
                                                <asp:ListItem>产品一组</asp:ListItem>
                                                <asp:ListItem>产品二组</asp:ListItem>
                                                <asp:ListItem>产品三组</asp:ListItem>
                                                <asp:ListItem>产品四组</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td >
                                            产品状态:</td>
                                     <td>
                                    <asp:TextBox ID="txt_product_status" class="form-control input-s-sm" runat="server"  Style="display: none; "  ></asp:TextBox>
                                    <select id="selectPStatus" name="selectPStatus" class="selectpicker A" multiple data-live-search="true" runat="server" style="width: 50px">
                                    </select>
                                </td>
                                      <td>
                                            工程师:</td>
                                        <td>
                                            <asp:DropDownList ID="txt_gcs" runat="server" 
                                                class="form-control input-s-sm" >
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                    </tr>

                                     
                                </table>
                                </div>
                     
                    </div>
                   
                </div>
            </div>
        </div>
       
        <div class=" panel panel-info col-lg-6 " >
        <div class="panel panel-heading">
            <asp:Label ID="lblYear" runat="server" Text="总标准成本"></asp:Label>
        </div>
        <div class="panel panel-body" >
            <div style="float: left; width:50%">
               <asp:Chart ID="Chartzcb" runat="server" BackColor="#F3DFC1" BackGradientStyle="TopBottom"
                    BorderColor="181, 64, 1" BorderDashStyle="Solid" BorderWidth="2" Height="200px"
                    ImageLocation="~/TempImages/ChartPic_#SEQ(300,3)" ImageType="Png" Palette="none"
                    Width="700PX">
                    <Legends>
                        <asp:Legend BackColor="Transparent" 
                            Font="Trebuchet MS, 8.25pt, style=Bold" IsTextAutoFit="False"
                            MaximumAutoSize="20" Name="Default" 
                            TitleAlignment="Center" Docking="Top" >
                        </asp:Legend>
                    </Legends>
                    <BorderSkin SkinStyle="Emboss" />
                    <Series>
                        <asp:Series BorderWidth="3" ChartArea="ChartArea1" 
                            ChartType="Column" LabelBorderWidth="9"
                            Legend="Default" LegendText="总标准成本"   MarkerSize="8" Name="总标准成本"
                            >
                        </asp:Series>
                        <asp:Series ChartArea="ChartArea1" Legend="Default" 
                            LegendText="总弹性成本" Name="总弹性成本">
                        </asp:Series>
                        <asp:Series ChartArea="ChartArea1" Legend="Default" 
                            LegendText="总实际成本" Name="总实际成本">
                        </asp:Series>
                    </Series>
                    <ChartAreas>
                        <asp:ChartArea BackColor="64, 165, 191, 228" BackGradientStyle="TopBottom" BackSecondaryColor="White"
                            BorderColor="64, 64, 64, 64" BorderDashStyle="Solid" Name="ChartArea1" ShadowColor="Transparent">
                            <Area3DStyle Inclination="15" IsClustered="False" IsRightAngleAxes="False" Perspective="10"
                                Rotation="10" WallWidth="0" />
                            <AxisY LineColor="64, 64, 64, 64" TitleAlignment="Far">
                                <LabelStyle Font="Trebuchet MS, 8.25pt, style=Bold" />
                                <MajorGrid LineColor="64, 64, 64, 64" Enabled="False" />
                            </AxisY>
                            
                            <AxisX Interval="1" LineColor="64, 64, 64, 64">
                                <LabelStyle Font="Trebuchet MS, 8.25pt, style=Bold" />
                                <MajorGrid LineColor="64, 64, 64, 64" Enabled="False" />
                            </AxisX>
                        </asp:ChartArea>
                    </ChartAreas>
                </asp:Chart>
            
            </div>
            <div>
            </div>
        </div>
    </div>
    <%--月--%>
   <div class=" panel panel-info  col-lg-6 ">
        <div class="panel panel-heading">
            <asp:Label ID="lblMonth" runat="server" Text="单位标准成本"></asp:Label>
        </div>
        <div class="panel panel-body">
            
              <asp:Chart ID="Chartdwcb" runat="server" BackColor="#F3DFC1" BackGradientStyle="TopBottom"
                    BorderColor="181, 64, 1" BorderDashStyle="Solid" BorderWidth="2" Height="200px"
                    ImageLocation="~/TempImages/ChartPic_#SEQ(300,3)" ImageType="Png" Palette="none"
                    Width="700px">
                    <Legends>
                        <asp:Legend BackColor="Transparent" 
                            Font="Trebuchet MS, 8.25pt, style=Bold" IsTextAutoFit="False"
                            MaximumAutoSize="20" Name="Default" 
                            TitleAlignment="Center" Docking="Top">
                        </asp:Legend>
                    </Legends>
                    <BorderSkin SkinStyle="Emboss" />
                    <Series>
                        <asp:Series BorderWidth="3" ChartArea="ChartArea1" LabelBorderWidth="9"
                            Legend="Default" LegendText="单位成本" MarkerSize="8" Name="单位成本"
                            >
                        </asp:Series>
                        <asp:Series ChartArea="ChartArea1" Legend="Default" 
                            LegendText="弹性单位成本" Name="弹性单位成本">
                        </asp:Series>
                        <asp:Series ChartArea="ChartArea1" Legend="Default" 
                            LegendText="单位实际成本" Name="单位实际成本">
                        </asp:Series>
                    </Series>
                    <ChartAreas>
                        <asp:ChartArea BackColor="64, 165, 191, 228" BackGradientStyle="TopBottom" BackSecondaryColor="White"
                            BorderColor="64, 64, 64, 64" BorderDashStyle="Solid" Name="ChartArea1" ShadowColor="Transparent">
                            <Area3DStyle Inclination="15" IsClustered="False" IsRightAngleAxes="False" Perspective="10"
                                Rotation="10" WallWidth="0" />
                            <AxisY LineColor="64, 64, 64, 64" TitleAlignment="Far">
                                <LabelStyle Font="Trebuchet MS, 8.25pt, style=Bold" />
                                <MajorGrid LineColor="64, 64, 64, 64" Enabled="False" />
                            </AxisY>
                            <AxisY2>
                                <LabelStyle Font="Trebuchet MS, 18.25pt, style=Bold" />
                                <MajorGrid LineColor="4, 64, 4, 64" />
                            </AxisY2>
                            <AxisX Interval="1" LineColor="64, 64, 64, 64">
                                <LabelStyle Font="Trebuchet MS, 8.25pt, style=Bold" />
                                <MajorGrid LineColor="64, 64, 64, 64" Enabled="False" />
                            </AxisX>
                        </asp:ChartArea>
                    </ChartAreas>
                </asp:Chart>
        
        </div>
    </div>
    <%--日--%>
    <div class="panel panel-info  col-lg-12"  style=" overflow:scroll" >
        <div class="panel panel-heading">
            <asp:Label ID="lblDays" runat="server" Text="Detail:" /></div>
      <asp:GridView ID="GridView1" runat="server" 
                                                    
            AllowPaging="True"  CssClass="gvHeader"
                                                  
                                                     Width="100%" AutoGenerateColumns="False" 
            OnRowCreated="GridView1_RowCreated" 
            onpageindexchanging="GridView1_PageIndexChanging" 
            onrowdatabound="GridView1_RowDataBound" 
            onsorting="GridView1_Sorting" AllowSorting="True" 
            ShowFooter="True" >
             <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
               <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                                    <Columns>
                                                        <asp:BoundField HeaderText="序号" >
                                                        <HeaderStyle BackColor="#C1E2EB" ForeColor="Brown" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="xmh" HeaderText="项目号" >
                                                        <HeaderStyle BackColor="#C1E2EB" ForeColor="Brown" />
                                                        </asp:BoundField>
<asp:BoundField DataField="ljh" HeaderText="零件号">
</asp:BoundField>
                                                        <asp:BoundField DataField="MS1" HeaderText="描述" >
                                                        <HeaderStyle BackColor="#C1E2EB" ForeColor="Brown" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="TCSL" HeaderText="投产量" 
                                                            SortExpression="TCSL" DataFormatString="{0:N0}" >
                                                        <FooterStyle HorizontalAlign="Right" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="zbzcb" HeaderText="总标准&lt;br&gt;成本" 
                                                            SortExpression="zbzcb" DataFormatString="{0:N0}" 
                                                            HtmlEncode="False" >
                                                        <FooterStyle HorizontalAlign="Right" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="ztxcb" HeaderText="总弹性标准&lt;br&gt;成本" 
                                                            SortExpression="ztxcb" DataFormatString="{0:N0}" 
                                                            HtmlEncode="False" >
                                                        <FooterStyle HorizontalAlign="Right" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="zsjcb" HeaderText="总实际&lt;br&gt;成本" 
                                                            SortExpression="zsjcb" DataFormatString="{0:N0}" 
                                                            HtmlEncode="False" >
                                                        <FooterStyle HorizontalAlign="Right" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        </asp:BoundField>
                                                        <asp:BoundField HeaderText="差异_实际&lt;br&gt;VS标准" DataField="diff1" 
                                                            SortExpression="diff1" DataFormatString="{0:N0}" 
                                                            HtmlEncode="False" >
                                                        <FooterStyle HorizontalAlign="Right" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="bzlys" HeaderText="标准领用&lt;br&gt;数量" 
                                                            SortExpression="bzlys" DataFormatString="{0:f2}" 
                                                            HtmlEncode="False" >
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="sjlys" HeaderText="实际领用&lt;br&gt;数量" 
                                                            SortExpression="sjlys" DataFormatString="{0:N0}" 
                                                            HtmlEncode="False" >
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="dwbzcb" HeaderText="单位标准&lt;br&gt;成本" 
                                                            SortExpression="dwbzcb" DataFormatString="{0:f3}" 
                                                            HtmlEncode="False" >
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="dwtxcb" HeaderText="弹性单位标准&lt;br&gt;成本" 
                                                            SortExpression="dwtxcb" DataFormatString="{0:f3}" 
                                                            HtmlEncode="False" >
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="dwsjcb" HeaderText="单位实际&lt;br&gt;成本" 
                                                            SortExpression="dwsjcb" DataFormatString="{0:f3}" 
                                                            HtmlEncode="False" >
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="diff4" 
                                                            HeaderText="差异率_&lt;br&gt;弹性单位标准&lt;br&gt;VS单位标准(%)" HtmlEncode="False" 
                                                            SortExpression="diff4" DataFormatString="{0:P0}">
                                                        <FooterStyle HorizontalAlign="Right" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="diff2" 
                                                            HeaderText="差异_单位实际&lt;br&gt;VS单位标准" 
                                                            SortExpression="diff2" DataFormatString="{0:f3}" 
                                                            HtmlEncode="False" >
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="diff3" 
                                                            HeaderText="差异率_&lt;br&gt;单位实际&lt;br&gt;VS单位标准(%)" 
                                                            SortExpression="diff3" 
                                                            HtmlEncode="False" DataFormatString="{0:P0}" >
                                                        <FooterStyle HorizontalAlign="Right" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        </asp:BoundField>
                                                        <asp:TemplateField HeaderText="累计成本">
                                                            <ItemTemplate>
                                                                <asp:Button ID="Button3" runat="server" Text="累计成本" 
                                                                    onclick="Button3_Click" />
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="成本趋势">
                                                            <ItemTemplate>
                                                                <asp:Button ID="Button4" runat="server" Text="成本趋势" 
                                                                    onclick="Button4_Click" />
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <PagerSettings FirstPageText="首页" LastPageText="尾页" 
                                                        Mode="NextPreviousFirstLast" NextPageText="下页" 
                                                        PreviousPageText="上页" />
                                                    <PagerStyle ForeColor="Red" />
                                                   
                                                </asp:GridView>
        <asp:GridView ID="GridView4" runat="server" 
                                                    
            AllowPaging="True"  CssClass="gvHeader"
                                                  
                                                     Width="100%" AutoGenerateColumns="False" 
            OnRowCreated="GridView4_RowCreated" 
            onpageindexchanging="GridView1_PageIndexChanging" 
            onrowdatabound="GridView1_RowDataBound" 
            onsorting="GridView4_Sorting" AllowSorting="True" 
            ShowFooter="True" >
             <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
               <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                                    <Columns>
                                                        <asp:BoundField HeaderText="序号" >
                                                        <HeaderStyle BackColor="#C1E2EB" ForeColor="Brown" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="dj_group" HeaderText="群组描述" >
                                                        <HeaderStyle BackColor="#C1E2EB" ForeColor="Brown" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="TCSL" HeaderText="投产量" 
                                                            SortExpression="TCSL" DataFormatString="{0:N0}" >
                                                        <FooterStyle HorizontalAlign="Right" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="zbzcb" HeaderText="总标准&lt;br&gt;成本" 
                                                            SortExpression="zbzcb" DataFormatString="{0:N0}" 
                                                            HtmlEncode="False" >
                                                        <FooterStyle HorizontalAlign="Right" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="ztxcb" HeaderText="总弹性标准&lt;br&gt;成本" 
                                                            SortExpression="ztxcb" DataFormatString="{0:N0}" 
                                                            HtmlEncode="False" >
                                                        <FooterStyle HorizontalAlign="Right" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="zsjcb" HeaderText="总实际&lt;br&gt;成本" 
                                                            SortExpression="zsjcb" DataFormatString="{0:N0}" 
                                                            HtmlEncode="False" >
                                                        <FooterStyle HorizontalAlign="Right" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        </asp:BoundField>
                                                        <asp:BoundField HeaderText="差异_实际&lt;br&gt;VS标准" DataField="diff1" 
                                                            SortExpression="zsjcb" DataFormatString="{0:N0}" 
                                                            HtmlEncode="False" >
                                                        <FooterStyle HorizontalAlign="Right" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="bzlys" HeaderText="标准领用&lt;br&gt;数量" 
                                                            SortExpression="bzlys" DataFormatString="{0:f2}" 
                                                            HtmlEncode="False" >
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="sjlys" HeaderText="实际领用&lt;br&gt;数量" 
                                                            SortExpression="sjlys" DataFormatString="{0:N0}" 
                                                            HtmlEncode="False" >
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="dwbzcb" HeaderText="单位标准&lt;br&gt;成本" 
                                                            SortExpression="dwbzcb" DataFormatString="{0:f3}" 
                                                            HtmlEncode="False" >
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="dwtxcb" HeaderText="弹性单位标准&lt;br&gt;成本" 
                                                            SortExpression="dwtxcb" DataFormatString="{0:f3}" 
                                                            HtmlEncode="False" >
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="dwsjcb" HeaderText="单位实际&lt;br&gt;成本" 
                                                            SortExpression="dwsjcb" DataFormatString="{0:f3}" 
                                                            HtmlEncode="False" >
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="diff4" 
                                                            HeaderText="差异率_&lt;br&gt;弹性单位标准&lt;br&gt;VS单位标准(%)" HtmlEncode="False" 
                                                            SortExpression="diff4" DataFormatString="{0:P0}">
                                                        <FooterStyle HorizontalAlign="Right" />
                                                        <HeaderStyle HorizontalAlign="Right" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="diff2" 
                                                            HeaderText="差异_单位实际&lt;br&gt;VS单位标准" 
                                                            SortExpression="diff2" DataFormatString="{0:f3}" 
                                                            HtmlEncode="False" >
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="diff3" 
                                                            HeaderText="差异率_&lt;br&gt;单位实际&lt;br&gt;VS单位标准(%)" 
                                                            SortExpression="diff3" 
                                                            HtmlEncode="False" DataFormatString="{0:P0}" >
                                                        <FooterStyle HorizontalAlign="Right" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        </asp:BoundField>
                                                        <asp:TemplateField HeaderText="累计成本">
                                                            <ItemTemplate>
                                                                <asp:Button ID="qz_ljcb" runat="server" Text="累计成本" onclick="qz_ljcb_Click" 
                                                                    />
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="成本趋势">
                                                            <ItemTemplate>
                                                                <asp:Button ID="qz_cbqs" runat="server" Text="成本趋势" onclick="qz_cbqs_Click" 
                                                                    />
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <PagerSettings FirstPageText="首页" LastPageText="尾页" 
                                                        Mode="NextPreviousFirstLast" NextPageText="下页" 
                                                        PreviousPageText="上页" />
                                                    <PagerStyle ForeColor="Red" />
                                                   
                                                </asp:GridView>
    </div>
       
       <div class=" panel panel-info col-lg-6 " id="zbz"  runat="server">
        <div class="panel panel-heading">
            <asp:Label ID="Label1" runat="server" Text="总标准成本"></asp:Label>
        </div>
        <div class="panel panel-body">
            <div style="float: left">
               <asp:Chart ID="ChartByProduct_zcb" runat="server" BackColor="#F3DFC1" BackGradientStyle="TopBottom"
                    BorderColor="181, 64, 1" BorderDashStyle="Solid" BorderWidth="2" Height="200px"
                    ImageLocation="~/TempImages/ChartPic_#SEQ(300,3)" ImageType="Png" Palette="none"
                    Width="700px">
                    <Legends>
                        <asp:Legend BackColor="Transparent" 
                            Font="Trebuchet MS, 8.25pt, style=Bold" IsTextAutoFit="False"
                            MaximumAutoSize="20" Name="Default" 
                            TitleAlignment="Center" Docking="Top" >
                        </asp:Legend>
                    </Legends>
                    <BorderSkin SkinStyle="Emboss" />
                    <Series>
                        <asp:Series  ChartArea="ChartArea1" 
                            ChartType="Column" 
                            Legend="Default" LegendText="总标准成本"  Name="总标准成本"
                            >
                        </asp:Series>
                        <asp:Series ChartArea="ChartArea1" Legend="Default" 
                            LegendText="总弹性成本" Name="总弹性成本">
                        </asp:Series>
                        <asp:Series ChartArea="ChartArea1" Legend="Default" 
                            LegendText="总实际成本" Name="总实际成本">
                        </asp:Series>
                    </Series>
                    <ChartAreas>
                        <asp:ChartArea BackColor="64, 165, 191, 228" BackGradientStyle="TopBottom" BackSecondaryColor="White"
                            BorderColor="64, 64, 64, 64" BorderDashStyle="Solid" Name="ChartArea1" ShadowColor="Transparent">
                            <Area3DStyle Inclination="15" IsClustered="False" IsRightAngleAxes="False" Perspective="10"
                                Rotation="10" WallWidth="0" />
                            <AxisY LineColor="64, 64, 64, 64" TitleAlignment="Far">
                                <LabelStyle Font="Trebuchet MS, 8.25pt, style=Bold" />
                                <MajorGrid LineColor="64, 64, 64, 64" Enabled="False" />
                            </AxisY>
                            
                            <AxisX Interval="1" LineColor="64, 64, 64, 64">
                                <LabelStyle Font="Trebuchet MS, 8.25pt, style=Bold" />
                                <MajorGrid LineColor="64, 64, 64, 64" Enabled="False" />
                            </AxisX>
                        </asp:ChartArea>
                    </ChartAreas>
                </asp:Chart>
            
            </div>
            <div>
            </div>
        </div>
    </div>
    <%--月--%>
   <div class=" panel panel-info  col-lg-6 " id="dwbz" runat="server">
        <div class="panel panel-heading">
            <asp:Label ID="Label2" runat="server" Text="单位标准成本"></asp:Label>
        </div>
        <div class="panel panel-body">
            
              <asp:Chart ID="ChartByProduct_dwcb" runat="server" BackColor="#F3DFC1" BackGradientStyle="TopBottom"
                    BorderColor="181, 64, 1" BorderDashStyle="Solid" BorderWidth="2" Height="200px"
                    ImageLocation="~/TempImages/ChartPic_#SEQ(300,3)" ImageType="Png" Palette="none"
                    Width="700px">
                    <Legends>
                        <asp:Legend BackColor="Transparent" 
                            Font="Trebuchet MS, 8.25pt, style=Bold" IsTextAutoFit="False"
                            MaximumAutoSize="20" Name="Default" 
                            TitleAlignment="Center" Docking="Top">
                        </asp:Legend>
                    </Legends>
                    <BorderSkin SkinStyle="Emboss" />
                    <Series>
                        <asp:Series  ChartArea="ChartArea1"  Legend="Default" 
                        LegendText="单位成本" Name="单位成本"
                           >
                        </asp:Series>
                        <asp:Series ChartArea="ChartArea1" Legend="Default" 
                            LegendText="弹性单位成本" Name="弹性单位成本">
                        </asp:Series>
                        <asp:Series ChartArea="ChartArea1" Legend="Default" 
                            LegendText="单位实际成本" Name="单位实际成本">
                        </asp:Series>
                    </Series>
                    <ChartAreas>
                        <asp:ChartArea BackColor="64, 165, 191, 228" BackGradientStyle="TopBottom" BackSecondaryColor="White"
                            BorderColor="64, 64, 64, 64" BorderDashStyle="Solid" Name="ChartArea1" ShadowColor="Transparent">
                            <Area3DStyle Inclination="15" IsClustered="False" IsRightAngleAxes="False" Perspective="10"
                                Rotation="10" WallWidth="0" />
                            <AxisY LineColor="64, 64, 64, 64" TitleAlignment="Far">
                                <LabelStyle Font="Trebuchet MS, 8.25pt, style=Bold" />
                                <MajorGrid LineColor="64, 64, 64, 64" Enabled="False" />
                            </AxisY>
                            <AxisY2>
                                <LabelStyle Font="Trebuchet MS, 18.25pt, style=Bold" />
                                <MajorGrid LineColor="4, 64, 4, 64" />
                            </AxisY2>
                            <AxisX Interval="1" LineColor="64, 64, 64, 64">
                                <LabelStyle Font="Trebuchet MS, 8.25pt, style=Bold" />
                                <MajorGrid LineColor="64, 64, 64, 64" Enabled="False" />
                            </AxisX>
                        </asp:ChartArea>
                    </ChartAreas>
                </asp:Chart>
        
        </div>
    </div>
          <div class="panel panel-info  col-lg-12" runat="server" id="xmh" style=" overflow:scroll" >
        <div class="panel panel-heading">
            <asp:Label ID="Label4" runat="server" Text="Detail:" /></div>
      <asp:GridView ID="GridView3" runat="server"  CssClass="gvHeader"
                                                    AllowPaging="True" 
                                                  
                                                     Width="100%" 
                  AutoGenerateColumns="False" 
                  onrowdatabound="GridView3_RowDataBound" PageSize="100" 
                  onrowcreated="GridView3_RowCreated" ShowFooter="True" >
              <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
               <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                                    <Columns>
                                                        <asp:BoundField HeaderText="序号" />
                                                        <asp:TemplateField HeaderText="项目号">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="LinkButton4" runat="server" 
                                                                    Text='<%#Eval("xmh") %>' onclick="LinkButton4_Click">LinkButton</asp:LinkButton>
                                                            </ItemTemplate>
                                                            <HeaderStyle BackColor="#C1E2EB" />
                                                            <ItemStyle Width="160px" />
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="ljh" HeaderText="零件号" />
                                                        <asp:BoundField DataField="MS1" 
                                                            HeaderText="项目号描述" />
                                                        <asp:TemplateField HeaderText="刀具物料号">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="LinkButton1" runat="server" 
                                                                    Text='<%#Eval("part") %>' onclick="LinkButton1_Click">LinkButton</asp:LinkButton>
                                                            </ItemTemplate>
                                                            <HeaderStyle BackColor="#C1E2EB" />
                                                            <ItemStyle Width="160px" />
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="pt_desc1" HeaderText="刀具描述" />
                                                        <asp:BoundField DataField="TCSL" HeaderText="投产量" 
                                                            DataFormatString="{0:N0}" >
                                                        <FooterStyle HorizontalAlign="Right" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="zbzcb" 
                                                            HeaderText="总标准&lt;br&gt;成本" DataFormatString="{0:N0}" 
                                                            HtmlEncode="False" >
                                                        <FooterStyle HorizontalAlign="Right" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="ztxcb" 
                                                            HeaderText="总弹性标准&lt;br&gt;成本" DataFormatString="{0:N0}" 
                                                            HtmlEncode="False" >
                                                        <FooterStyle HorizontalAlign="Right" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="zsjcb" 
                                                            HeaderText="总实际&lt;br&gt;成本" DataFormatString="{0:N0}" 
                                                            HtmlEncode="False" >
                                                        <FooterStyle HorizontalAlign="Right" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        </asp:BoundField>
                                                        <asp:BoundField HeaderText="差异_实际&lt;br&gt;VS标准" 
                                                            DataField="diff1" DataFormatString="{0:N0}" 
                                                            HtmlEncode="False" >
                                                        <FooterStyle HorizontalAlign="Right" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="bzlys" 
                                                            HeaderText="标准领用&lt;br&gt;数量" DataFormatString="{0:f2}" 
                                                            HtmlEncode="False" >
                                                        <FooterStyle HorizontalAlign="Right" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        </asp:BoundField>
                                                       <asp:TemplateField HeaderText="实际领用&lt;br&gt;数量">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="LinkButton2" runat="server" 
                                                                    Text='<%#Eval("sjlys") %>' onclick="LinkButton2_Click">LinkButton</asp:LinkButton>
                                                            </ItemTemplate>
                                                            <FooterStyle HorizontalAlign="Right" />
                                                            <HeaderStyle BackColor="#C1E2EB" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="dwbzcb" 
                                                            HeaderText="单位标准&lt;br&gt;成本" DataFormatString="{0:f3}" 
                                                            HtmlEncode="False" >
                                                        <FooterStyle HorizontalAlign="Right" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="dwtxcb" 
                                                            HeaderText="弹性单位标准&lt;br&gt;成本" DataFormatString="{0:f3}" 
                                                            HtmlEncode="False" >
                                                        <FooterStyle HorizontalAlign="Right" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="dwsjcb" 
                                                            HeaderText="单位实际&lt;br&gt;成本" DataFormatString="{0:f3}" 
                                                            HtmlEncode="False" >
                                                        <FooterStyle HorizontalAlign="Right" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="diff4" 
                                                            HeaderText="差异率_弹性单位标准&lt;br&gt;单位标准(%)" HtmlEncode="False" 
                                                            SortExpression="diff4" DataFormatString="{0:P0}">
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="diff2" 
                                                            HeaderText="差异_单位实际&lt;br&gt;VS单位标准" 
                                                            DataFormatString="{0:f3}" HtmlEncode="False" >
                                                        <FooterStyle HorizontalAlign="Right" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="diff3" 
                                                            HeaderText="差异率_单位实际&lt;br&gt;VS单位标准(%)" 
                                                            HtmlEncode="False" DataFormatString="{0:P0}" >
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        </asp:BoundField>
                                                    </Columns>
                                                    <PagerSettings FirstPageText="首页" LastPageText="尾页" 
                                                        Mode="NextPreviousFirstLast" NextPageText="下页" 
                                                        PreviousPageText="上页" />
                                                    <PagerStyle ForeColor="Red" />
                                                   
                                                </asp:GridView>


                                                <asp:GridView ID="GridView5" runat="server"  CssClass="gvHeader"
                                                    AllowPaging="True" 
                                                  
                                                     Width="100%" 
                  AutoGenerateColumns="False" 
                  onrowdatabound="GridView5_RowDataBound" PageSize="100" 
                  onrowcreated="GridView5_RowCreated" ShowFooter="True" >
              <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
               <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                                    <Columns>
                                                        <asp:BoundField HeaderText="序号" />
                                                        <asp:BoundField DataField="dj_group" HeaderText="群组描述" />
                                                        <asp:TemplateField HeaderText="刀具物料号">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lbtn" runat="server" 
                                                                    Text='<%#Eval("part") %>' onclick="lbtn_Click" >LinkButton</asp:LinkButton>
                                                            </ItemTemplate>
                                                            <HeaderStyle BackColor="#C1E2EB" />
                                                            <ItemStyle Width="160px" />
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="pt_desc1" HeaderText="刀具描述" />
                                                        <asp:BoundField DataField="TCSL" HeaderText="投产量" 
                                                            DataFormatString="{0:N0}" >
                                                        <FooterStyle HorizontalAlign="Right" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="zbzcb" 
                                                            HeaderText="总标准&lt;br&gt;成本" DataFormatString="{0:N0}" 
                                                            HtmlEncode="False" >
                                                        <FooterStyle HorizontalAlign="Right" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="ztxcb" 
                                                            HeaderText="总弹性标准&lt;br&gt;成本" DataFormatString="{0:N0}" 
                                                            HtmlEncode="False" >
                                                        <FooterStyle HorizontalAlign="Right" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="zsjcb" 
                                                            HeaderText="总实际&lt;br&gt;成本" DataFormatString="{0:N0}" 
                                                            HtmlEncode="False" >
                                                        <FooterStyle HorizontalAlign="Right" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        </asp:BoundField>
                                                        <asp:BoundField HeaderText="差异_实际&lt;br&gt;VS标准" 
                                                            DataField="diff1" DataFormatString="{0:N0}" 
                                                            HtmlEncode="False" >
                                                        <FooterStyle HorizontalAlign="Right" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="bzlys" 
                                                            HeaderText="标准领用&lt;br&gt;数量" DataFormatString="{0:f2}" 
                                                            HtmlEncode="False" >
                                                        <FooterStyle HorizontalAlign="Right" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        </asp:BoundField>
                                                       <asp:TemplateField HeaderText="实际领用&lt;br&gt;数量">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lbtn2" runat="server" 
                                                                    Text='<%#Eval("sjlys") %>' onclick="lbtn2_Click" >LinkButton</asp:LinkButton>
                                                            </ItemTemplate>
                                                            <FooterStyle HorizontalAlign="Right" />
                                                            <HeaderStyle BackColor="#C1E2EB" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="dwbzcb" 
                                                            HeaderText="单位标准&lt;br&gt;成本" DataFormatString="{0:f3}" 
                                                            HtmlEncode="False" >
                                                        <FooterStyle HorizontalAlign="Right" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="dwtxcb" 
                                                            HeaderText="弹性单位标准&lt;br&gt;成本" DataFormatString="{0:f3}" 
                                                            HtmlEncode="False" >
                                                        <FooterStyle HorizontalAlign="Right" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="dwsjcb" 
                                                            HeaderText="单位实际&lt;br&gt;成本" DataFormatString="{0:f3}" 
                                                            HtmlEncode="False" >
                                                        <FooterStyle HorizontalAlign="Right" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="diff4" 
                                                            HeaderText="差异率_弹性单位标准&lt;br&gt;单位标准(%)" HtmlEncode="False" 
                                                            SortExpression="diff4" DataFormatString="{0:P0}">
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="diff2" 
                                                            HeaderText="差异_单位实际&lt;br&gt;VS单位标准" 
                                                            DataFormatString="{0:f3}" HtmlEncode="False" >
                                                        <FooterStyle HorizontalAlign="Right" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="diff3" 
                                                            HeaderText="差异率_单位实际&lt;br&gt;VS单位标准(%)" 
                                                            HtmlEncode="False" DataFormatString="{0:P0}" >
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        </asp:BoundField>
                                                    </Columns>
                                                    <PagerSettings FirstPageText="首页" LastPageText="尾页" 
                                                        Mode="NextPreviousFirstLast" NextPageText="下页" 
                                                        PreviousPageText="上页" />
                                                    <PagerStyle ForeColor="Red" />
                                                   
                                                </asp:GridView>
       
    </div>
  


       <div class=" panel panel-info col-lg-12 " id="qs"  runat="server">
        <div class="panel panel-heading">
            <asp:Label ID="Label3" runat="server" Text="趋势图"></asp:Label>
        </div>
        <div class="panel panel-body">
            <div style="float: left">
                <asp:GridView ID="GridView2" runat="server"  CssClass="gvHeader"
                    AutoGenerateColumns="False" >
                     <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
               <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                    <Columns>
                        <asp:BoundField DataField="xmh" HeaderText="项目号" />
                        <asp:BoundField DataField="mnth" HeaderText="月份" />
                        <asp:BoundField DataField="dwbzcb" HeaderText="单位标准成本" 
                            DataFormatString="{0:f3}" >
                        <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="dwtxcb" HeaderText="弹性单位标准成本" 
                            DataFormatString="{0:f3}" >
                        <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="dwsjcb" HeaderText="单位实际成本" 
                            DataFormatString="{0:f3}" >
                        <HeaderStyle HorizontalAlign="Right" />
                        <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="diff1" HeaderText="差异_实际vs标准" 
                            DataFormatString="{0:f3}" >
                        <HeaderStyle HorizontalAlign="Right" />
                        <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="diff2" 
                            HeaderText="差异率_实际vs标准(%)" DataFormatString="{0:P0}" >
                        <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                    </Columns>
                </asp:GridView>
            
            </div>
             
            <div>  <asp:Chart ID="ChartByDJ" runat="server" BackColor="#F3DFC1" BackGradientStyle="TopBottom"
                    BorderColor="181, 64, 1" BorderDashStyle="Solid" BorderWidth="2" Height="180px"
                    ImageLocation="~/TempImages/ChartPic_#SEQ(300,3)" ImageType="Png" Palette="none"
                    Width="700px">
                    <Legends>
                        <asp:Legend BackColor="Transparent" 
                            Font="Trebuchet MS, 8.25pt, style=Bold" IsTextAutoFit="False"
                            MaximumAutoSize="20" Name="Default" 
                            TitleAlignment="Center" Docking="Top" >
                        </asp:Legend>
                    </Legends>
                    <BorderSkin SkinStyle="Emboss" />
                    <Series>
                        <asp:Series ChartArea="ChartArea1" 
                            ChartType="Line" 
                            Legend="Default" LegendText="单位标准成本"  Name="单位标准成本"
                            MarkerStyle="Circle" BorderWidth="3" 
                            LabelBorderWidth="9" MarkerSize="8">
                        </asp:Series>
                        <asp:Series ChartArea="ChartArea1" 
                            ChartType="Line" Legend="Default" 
                            LegendText="弹性单位标准成本"  Name="弹性单位标准成本" 
                            MarkerStyle="Circle" BorderWidth="3" MarkerSize="8">
                        </asp:Series>
                        <asp:Series ChartArea="ChartArea1" 
                            ChartType="Line" Legend="Default" 
                            LegendText="单位实际成本"  Name="单位实际成本" 
                           MarkerStyle="Circle" BorderWidth="3" 
                            LabelBorderWidth="9" MarkerSize="8">
                        </asp:Series>
                    </Series>
                    <ChartAreas>
                        <asp:ChartArea BackColor="64, 165, 191, 228" BackGradientStyle="TopBottom" BackSecondaryColor="White"
                            BorderColor="64, 64, 64, 64" BorderDashStyle="Solid" Name="ChartArea1" ShadowColor="Transparent">
                            <Area3DStyle Inclination="15" IsClustered="False" IsRightAngleAxes="False" Perspective="10"
                                Rotation="10" WallWidth="0" />
                            <AxisY LineColor="64, 64, 64, 64" TitleAlignment="Far">
                                <LabelStyle Font="Trebuchet MS, 8.25pt, style=Bold" />
                                <MajorGrid LineColor="64, 64, 64, 64" Enabled="False" />
                            </AxisY>
                            
                            <AxisX Interval="1" LineColor="64, 64, 64, 64">
                                <LabelStyle Font="Trebuchet MS, 8.25pt, style=Bold" />
                                <MajorGrid LineColor="64, 64, 64, 64" Enabled="False" />
                            </AxisX>
                        </asp:ChartArea>
                    </ChartAreas>
                </asp:Chart></div>
            <div>
            </div>
        </div>
    </div>
   
</asp:Content>