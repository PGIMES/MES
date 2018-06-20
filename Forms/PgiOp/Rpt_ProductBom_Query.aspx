<%@ Page Title="产品信息" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Rpt_ProductBom_Query.aspx.cs" Inherits="Product_Rpt_ProductBom_Query" %>

<%@ Register assembly="DevExpress.Web.v17.2, Version=17.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web" tagprefix="dx" %>

<%@ Register assembly="DevExpress.Web.ASPxTreeList.v17.2, Version=17.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxTreeList" tagprefix="dx" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <link href="../../Content/css/bootstrap.min.css" rel="stylesheet" />
    <script src="../../Content/js/jquery.min.js"></script>
    <script src="../../Content/js/bootstrap.min.js"></script>
    <script src="../../Content/js/plugins/layer/layer.min.js"></script>
    <script src="../../Content/js/plugins/layer/laydate/laydate.js"></script>
    <link href="../../Content/js/plugins/bootstrap-select/css/bootstrap-select.min.css" rel="stylesheet" />

    <script src="../../Content/js/plugins/bootstrap-select/js/bootstrap-select.min.js"></script>

    <script type="text/javascript">
        $("#mestitle").text("【产品信息】");
        $(document).ready(function () {
            $("input[id*='Button1']").click(function () {
                var val = $("[id*='ddl_ljh']").val();
                if (val == "") {
                    layer.alert("请选择零件号.");
                    return false;
                }
            })


        })
    </script>

  
    </script>

    <style type="text/css">
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
            font-size:12px;
            padding-bottom: 5px;
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
        .auto-style1 {
            height: 49px;
        }
        .auto-style2 {
            height: 54px;
        }
        .tbl td
{ border:1px solid black;
                 padding-left:3px;
                 padding-right:3px;
                 padding-top:3px;
        }

        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
 <div class="col-md-10">
   <div class="row  row-container">
            <div class="col-md-12">
                <div class="panel panel-info">
                    <div class="panel-heading" data-toggle="collapse" data-target="#CPXX">
                        <strong>查询</strong>
                    </div>
                   
                    <div class="panel-body " id="Div1">
                        <div class="col-xs-12 col-sm-12  col-md-12 col-lg-12">

                            <div>
                                <table style="width: 100%">
                                        <tr>
                                            <td style=" width:100px">生产工厂：</td>
                                            <td style=" width:120px">
                                                <div class="form-inline">
                                                
                                                <asp:DropDownList ID="ddl_comp" runat="server" class="form-control input-s-sm " Width="100px">
                                                 <asp:ListItem Value=""></asp:ListItem>
                                                 <asp:ListItem Value="100">100</asp:ListItem>
                                                 <asp:ListItem Value="200">200</asp:ListItem>
                                       
                                    </asp:DropDownList>
                                                </div>
                                            </td>
                                            <td style=" width:100px">
                                                零件号:</td>
                                            <td  style=" width:280px">
                                                  <div class="form-inline">
                                                <asp:DropDownList ID="ddl_ljh" runat="server" 
                                                    class="form-control input-s-sm " Width="250px">
                                       
                                    </asp:DropDownList>
                                    </div>
                                                </td>
                                            <td>
                                                <asp:Button ID="Button1" runat="server" Text="查询" 
                                                     class="btn btn-large btn-primary" OnClick="Button1_Click" 
                                                     Width="100px" />
                                            </td>
                                        </tr>
                                        </table>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
       </div>
        <div class="row  row-container">
            <div class="col-md-12">
                <div class="panel panel-info">
                    <div class="panel-heading" data-toggle="collapse" data-target="#CPXX">
                        <strong>产品基本信息</strong>
                    </div>
                   
                    <div class="panel-body " id="XSGCS">
                        <div class="col-xs-12 col-sm-12  col-md-12 col-lg-12">

                            <div>
                                <fieldset style="border-color: lightblue">
                                   <%-- <legend>一.基础信息</legend>--%>
                                    <table style="width: 100%">
                                        <tr>
                                            <td>项目号：</td>
                                            <td>
                                                <div class="form-inline">
                                                <input id="txt_pgino" class="form-control input-s-sm" style="height: 35px; width: 200px" runat="server" readonly="True" /></div>
                                            </td>
                                            <td rowspan="8"><asp:Panel ID=Panel4 runat =server GroupingText="图片信息"   Font-Size=Medium   BodyPadding="5">
                            <table>
                                            <tr>
                                            <td >
                                                <asp:Image ID="Image2" runat="server" Width="400px" Height="300px" 
                                                    />
                                            </td>
                                            </tr>
                                            </table>
                            
                            </asp:Panel>
                                                &nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td>零件号：</td>
                                            <td><div class="form-inline">
                                                <input id="txt_productcode" class="form-control input-s-sm" style="height: 35px; width: 200px" runat="server" readonly="True" /></div> </td>
                                        </tr>
                                        <tr>
                                            <td>零件名称：</td>
                                            <td><div class="form-inline">
                                                <input id="txt_productname" class="form-control input-s-sm" style="height: 35px; width: 200px" runat="server" readonly="True" /></div> </td>
                                        </tr>
                                        <tr>
                                            <td>最终客户：</td>
                                             <td><div class="form-inline">
                                                <input id="txt_EndCustName"  class="form-control input-s-sm" style="height: 35px; width: 200px" runat="server" readonly="True" /></div></td>
                                        </tr>
                                        <tr>
                                            <td>顾客项目：</td>
                                           <td><div class="form-inline">
                                                <input id="txt_CustProject" class="form-control input-s-sm" style="height: 35px; width: 200px" runat="server"  readonly="True" /></div> </td>
                                        </tr>
                                        <tr>
                                            <td>最大年用量：</td>
                                            <td> 
                                               <div class="form-inline">
                                                <input id="txt_MaxYearuseCounts" class="form-control input-s-sm" style="height: 35px; width: 200px" runat="server"  readonly="True" /></div></td>
                                        </tr>
                                        <tr>
                                            <td>批产日期：</td>
                                            <td> <div class="form-inline">
                                                 <input id="txt_PCDate" class="form-control input-s-sm" style="height: 35px; width: 200px" runat="server"  readonly="True" /></div></td>
                                        </tr>
                                        <tr>
                                            <td>停产日期：</td>
                                            <td> <div class="form-inline">
                                               <input id="txt_EndDate" class="form-control input-s-sm" style="height: 35px; width: 200px" runat="server"  readonly="True" /></div></td>
                                        </tr>
                                        </table>
                                    <table width="100%">
                                        


                                        <tr>
                                          
                                             <td align="right">

                                                 &nbsp;</td>
                                        </tr>



                                    </table>

                                </fieldset>
                                <table style="width: 100%">
                                     
                                    <tr>
                                        <td>
                                            <dx:ASPxPageControl ID="ASPxPageControl1" runat="server" 
                                                ActiveTabIndex="0">
                                                <TabPages>
                                                    <dx:TabPage Name="TabPage0" Text="工艺流程">
                                                        <ContentCollection>
                                                            <dx:ContentControl runat="server">
                                                                <dx:ASPxGridView ID="ASPxGridView0" runat="server" 
                                                                    AutoGenerateColumns="False">
                                                                    <SettingsPager Visible="False">
                                                                    </SettingsPager>
                                                                    <Columns>
                                                                        <dx:GridViewDataTextColumn FieldName="工序号" Name="ro_op" 
                                                                            ShowInCustomizationForm="True" VisibleIndex="0">
                                                                        </dx:GridViewDataTextColumn>
                                                                        <dx:GridViewDataTextColumn FieldName="工序名称" Name="ro_desc" 
                                                                            ShowInCustomizationForm="True" VisibleIndex="1">
                                                                        </dx:GridViewDataTextColumn>
                                                                        <dx:GridViewDataTextColumn FieldName="单台需要人数" Name="单台需要人数" 
                                                                            ShowInCustomizationForm="True" VisibleIndex="2">
                                                                        </dx:GridViewDataTextColumn>
                                                                        <dx:GridViewDataTextColumn FieldName="本工序1人操作台数" 
                                                                            Name="本工序1人操作台数" ShowInCustomizationForm="True" 
                                                                            VisibleIndex="3">
                                                                        </dx:GridViewDataTextColumn>
                                                                        <dx:GridViewDataTextColumn FieldName="总机台数" Name="总机台数" 
                                                                            ShowInCustomizationForm="True" VisibleIndex="4">
                                                                        </dx:GridViewDataTextColumn>
                                                                        <dx:GridViewDataTextColumn FieldName="单台单件工时(时)" 
                                                                            Name="单台单件工时(时)" ShowInCustomizationForm="True" 
                                                                            VisibleIndex="5">
                                                                        </dx:GridViewDataTextColumn>
                                                                        <dx:GridViewDataTextColumn FieldName="单台单件工时(秒)" 
                                                                            Name="单台单件工时(秒)" ShowInCustomizationForm="True" 
                                                                            VisibleIndex="6">
                                                                        </dx:GridViewDataTextColumn>
                                                                        <dx:GridViewDataTextColumn FieldName="单台班产量(83%)" 
                                                                            Name="单台班产量(83%)" ShowInCustomizationForm="True" 
                                                                            VisibleIndex="7">
                                                                        </dx:GridViewDataTextColumn>
                                                                        <dx:GridViewDataTextColumn FieldName="1人班产量(83%)" 
                                                                            Name="1人班产量(83%)" ShowInCustomizationForm="True" 
                                                                            VisibleIndex="8">
                                                                        </dx:GridViewDataTextColumn>
                                                                        <dx:GridViewDataTextColumn FieldName="整线班产量(83%)" 
                                                                            Name="整线班产量(83%)" ShowInCustomizationForm="True" 
                                                                            VisibleIndex="9">
                                                                        </dx:GridViewDataTextColumn>
                                                                    </Columns>
                                                                    <Styles>
                                                                        <Header BackColor="#99CCFF">
                                                                        </Header>
                                                                    </Styles>
                                                                </dx:ASPxGridView>
                                                            </dx:ContentControl>
                                                        </ContentCollection>
                                                    </dx:TabPage>
                                                    <dx:TabPage Name="TabPage1" Text="TabPage1">
                                                        <ContentCollection>
                                                            <dx:ContentControl runat="server">
                                                                <dx:ASPxGridView ID="ASPxGridView1" runat="server">
                                                                    <SettingsPager Visible="False">
                                                                    </SettingsPager>
                                                                    <Styles>
                                                                        <Header BackColor="#99CCFF">
                                                                        </Header>
                                                                    </Styles>
                                                                </dx:ASPxGridView>
                                                            </dx:ContentControl>
                                                        </ContentCollection>
                                                    </dx:TabPage>
                                                    <dx:TabPage Name="TabPage2" Text="TabPage2">
                                                        <ContentCollection>
                                                            <dx:ContentControl runat="server">
                                                                <dx:ASPxGridView ID="ASPxGridView2" runat="server">
                                                                    <SettingsPager Visible="False">
                                                                    </SettingsPager>
                                                                    <Styles>
                                                                        <Header BackColor="#99CCFF">
                                                                        </Header>
                                                                    </Styles>
                                                                </dx:ASPxGridView>
                                                            </dx:ContentControl>
                                                        </ContentCollection>
                                                    </dx:TabPage>
                                                    <dx:TabPage Name="TabPage3" Text="TabPage3">
                                                        <ContentCollection>
                                                            <dx:ContentControl runat="server">
                                                                <dx:ASPxGridView ID="ASPxGridView3" runat="server">
                                                                    <SettingsPager Visible="False">
                                                                    </SettingsPager>
                                                                    <Styles>
                                                                        <Header BackColor="#99CCFF">
                                                                        </Header>
                                                                    </Styles>
                                                                </dx:ASPxGridView>
                                                            </dx:ContentControl>
                                                        </ContentCollection>
                                                    </dx:TabPage>
                                                    <dx:TabPage Name="TabPage4" Text="TabPage4">
                                                        <ContentCollection>
                                                            <dx:ContentControl runat="server">
                                                                <dx:ASPxGridView ID="ASPxGridView4" runat="server">
                                                                    <SettingsPager Visible="False">
                                                                    </SettingsPager>
                                                                    <Styles>
                                                                        <Header BackColor="#99CCFF">
                                                                        </Header>
                                                                    </Styles>
                                                                </dx:ASPxGridView>
                                                            </dx:ContentControl>
                                                        </ContentCollection>
                                                    </dx:TabPage>
                                                    <dx:TabPage Name="TabPage5" Text="TabPage5">
                                                        <ContentCollection>
                                                            <dx:ContentControl runat="server">
                                                                <dx:ASPxGridView ID="ASPxGridView5" runat="server">
                                                                    <SettingsPager Visible="False">
                                                                    </SettingsPager>
                                                                    <Styles>
                                                                        <Header BackColor="#99CCFF">
                                                                        </Header>
                                                                    </Styles>
                                                                </dx:ASPxGridView>
                                                            </dx:ContentControl>
                                                        </ContentCollection>
                                                    </dx:TabPage>
                                                    <dx:TabPage Name="TabPage6" Text="BOM">
                                                        <ContentCollection>
                                                            <dx:ContentControl runat="server">
                                                                                                                               <dx:ASPxTreeList ID="treeList1" runat="server" 
                                                                    AutoGenerateColumns="False">
                                                                    <settingsbehavior allowautofilter="True" />
                                                                    <settingscustomizationwindow popuphorizontalalign="RightSides" 
                                                                        popupverticalalign="BottomSides" />
                                                                    <Columns>
                                                                        <dx:TreeListTextColumn AutoFilterCondition="Default" 
                                                                            FieldName="物料号" Name="物料号" ShowInCustomizationForm="True" 
                                                                            ShowInFilterControl="Default" VisibleIndex="1">
                                                                        </dx:TreeListTextColumn>
                                                                        <dx:TreeListTextColumn AutoFilterCondition="Default" 
                                                                            FieldName="物料描述" Name="物料描述" ShowInCustomizationForm="True" 
                                                                            ShowInFilterControl="Default" VisibleIndex="2">
                                                                        </dx:TreeListTextColumn>
                                                                        <dx:TreeListTextColumn AutoFilterCondition="Default" 
                                                                            FieldName="描述2" Name="描述2" ShowInCustomizationForm="True" 
                                                                            ShowInFilterControl="Default" VisibleIndex="3">
                                                                        </dx:TreeListTextColumn>
                                                                        <dx:TreeListTextColumn AutoFilterCondition="Default" 
                                                                            FieldName="工序" Name="工序" ShowInCustomizationForm="True" 
                                                                            ShowInFilterControl="Default" VisibleIndex="4">
                                                                        </dx:TreeListTextColumn>
                                                                        <dx:TreeListTextColumn AutoFilterCondition="Default" 
                                                                            FieldName="数量" Name="数量" ShowInCustomizationForm="True" 
                                                                            ShowInFilterControl="Default" VisibleIndex="5">
                                                                        </dx:TreeListTextColumn>
                                                                        <dx:TreeListTextColumn AutoFilterCondition="Default" 
                                                                            FieldName="重量" Name="重量" ShowInCustomizationForm="True" 
                                                                            ShowInFilterControl="Default" VisibleIndex="6">
                                                                        </dx:TreeListTextColumn>
                                                                        <dx:TreeListTextColumn AutoFilterCondition="Default" 
                                                                            FieldName="最小包装量" Name="最小包装量" 
                                                                            ShowInCustomizationForm="True" 
                                                                            ShowInFilterControl="Default" VisibleIndex="7">
                                                                        </dx:TreeListTextColumn>
                                                                    </Columns>
<SettingsBehavior AllowAutoFilter="True"></SettingsBehavior>

<SettingsCustomizationWindow PopupHorizontalAlign="RightSides" PopupVerticalAlign="BottomSides"></SettingsCustomizationWindow>

                                                                    <settingspopupeditform verticaloffset="-1">
                                                                    </settingspopupeditform>
                                                                    <settingspopup>
                                                                        <editform verticaloffset="-1">
                                                                        </editform>
                                                                    </settingspopup>
                                                                                                                                   <Styles>
                                                                                                                                       <Header BackColor="#99CCFF">
                                                                                                                                       </Header>
                                                                                                                                   </Styles>
                                                                </dx:ASPxTreeList>

                                                            </dx:ContentControl>
                                                        </ContentCollection>
                                                    </dx:TabPage>
                                                </TabPages>
                                            </dx:ASPxPageControl>
                                            
                                        </td>
                                    </tr>

                                    </table>
                            

                            </div>
                        </div>
                    </div>
                </div>
            </div>
   
        </div>

 
       
       
   
    </div>

 
       
       

 
       
       
   
</asp:Content>

