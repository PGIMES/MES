<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Fin_Base_QGSF_V1_Maintain.aspx.cs" Inherits="Fin_Fin_Base_QGSF_V1_Maintain" %>

<%@ Register Assembly="DevExpress.Web.v17.2, Version=17.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>【新增基率&清关税】</title>
    <link href="/Content/css/bootstrap.min.css" rel="stylesheet" />
    <script src="/Content/js/jquery.min.js"></script>
    <script src="/Content/js/bootstrap.min.js"></script>
    <script src="/Content/js/layer/layer.js"></script>

    <script type="text/javascript">

        $(document).ready(function () {
        });

        function wlh_change(s) {
            $.ajax({
                type: "post",
                url: "Fin_Base_QGSF_V1_Maintain.aspx/GetData_ByWlh",
                data: "{'wlh_domain':'" + s.GetValue()+ "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,//默认是true，异步；false为同步，此方法执行完在执行下面代码
                success: function (data) {
                    var obj = eval(data.d);
                    comdesc.SetValue(obj[0].comdesc);
                    hscode.SetValue(obj[0].hscode);

                    baserate.SetValue(obj[0].baserate);
                    qgcode.SetValue(obj[0].qgcode);
                    qgrate.SetValue(obj[0].qgrate);
                    immunity.SetValue(obj[0].immunity);
                    if (obj[0].immunity == "Y") {
                        $("#div_grid").css("display", "");
                        grid.PerformCallback("init");
                    } else {
                        $("#div_grid").css("display","none");
                    }
                }
            });

            
        }

        function validate() {
            if (wlh_i.GetValue() == null || wlh_i.GetValue() == "") {
                layer.alert("【物料号】不可为空！");
                return false;
            }
            if (baserate.GetValue() == null || baserate.GetValue() == "") {
                layer.alert("【Base Rate】不可为空！");
                return false;
            }
            if (qgcode.GetValue() == null || qgcode.GetValue() == "") {
                layer.alert("【301 Code】不可为空！");
                return false;
            }
            if (qgrate.GetValue() == null || qgrate.GetValue() == "") {
                layer.alert("【301 Rate】不可为空！");
                return false;
            }
            if (immunity.GetValue() == null || immunity.GetValue() == "") {
                layer.alert("【是否豁免】不可为空！");
                return false;
            }
            if (!ASPxClientEdit.ValidateGroup("ValueValidationGroup")) {
                layer.alert("【税率】格式必须正确.");
                return false;
            }
            return true;
        }

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
         .hidden { display:none;}
        .hidden1 {
            border: 0px;
            overflow: hidden;
        }
    </style>
    
    <style>
        .lineread{
            /*font-size:9px;*/ border:none; border-bottom:1px solid #ccc;
        }
        .linewrite{
            /*font-size:9px;*/ border:none; border-bottom:1px solid #ccc;background-color:#FDF7D9;/*EFEFEF*/
        }
        .dxeTextBox_read{
            border:none !important ; background-color:transparent;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

        
    <div class="col-md-12" >

        <div class="row  row-container">
            <div class="panel panel-info">
                <div class="panel-heading" data-toggle="collapse" data-target="#CPXX">
                    <strong>税率</strong>
                </div>
                <div class="panel-body" id="CPXX">
                    <div class="col-xs-12 col-sm-12  col-md-12 col-lg-12" style="width:490px;">
                        <div>
                            <table style="width:100%; font-size:12px; line-height:35px;" border="0" id="tblWLLeibie">
                                <tr>
                                    <td>物料号</td>
                                    <td>
                                        <dx:ASPxComboBox ID="wlh" ClientInstanceName="wlh_i" runat="server" ValueType="System.String" CssClass="linewrite" Width="100px" Height="27px" BackColor="#FDF7D9" ForeColor="#31708f">
                                            <ClientSideEvents ValueChanged="function(s, e) {wlh_change(s);}" />
                                        </dx:ASPxComboBox>
                                    </td>
                                    <td>HTS</td>
                                    <td><dx:ASPxTextBox ID="txt_com_comm_code" ClientInstanceName="hscode" runat="server" ReadOnly="true" CssClass="lineread" Width="100px" Height="27px"></dx:ASPxTextBox></td>
                                    <td>HTS描述</td>
                                    <td><dx:ASPxTextBox ID="txt_com_desc" ClientInstanceName="comdesc" runat="server" ReadOnly="true" CssClass="lineread" Width="100px" Height="27px"></dx:ASPxTextBox></td>
                                    <td></td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td>Base Rate</td>
                                    <td>
                                        <dx:ASPxTextBox ID="txt_BaseRate" ClientInstanceName="baserate" runat="server" CssClass="linewrite" Width="100px" Height="27px" BackColor="#FDF7D9">
                                            <ValidationSettings ValidationGroup="ValueValidationGroup" Display="Dynamic" ErrorTextPosition="Bottom">
                                                <RegularExpression ErrorText="请输入0~1之间的小数！" ValidationExpression="^([01](\.0+)?|0\.[0-9]+)$" />
                                            </ValidationSettings>
                                        </dx:ASPxTextBox>

                                    </td>
                                    <td>301 Code</td>
                                    <td>
                                        <dx:ASPxTextBox ID="txt_301code" ClientInstanceName="qgcode" runat="server" CssClass="linewrite" Width="100px" Height="27px" BackColor="#FDF7D9">
                                        </dx:ASPxTextBox>
                                    </td>
                                    <td>301 Rate</td>
                                    <td>
                                        <dx:ASPxTextBox ID="txt_301Rate" ClientInstanceName="qgrate" runat="server" CssClass="linewrite" Width="100px" Height="27px" BackColor="#FDF7D9">
                                            <ValidationSettings ValidationGroup="ValueValidationGroup" Display="Dynamic" ErrorTextPosition="Bottom">
                                                <RegularExpression ErrorText="请输入0~1之间的小数！" ValidationExpression="^([01](\.0+)?|0\.[0-9]+)$" />
                                            </ValidationSettings>
                                        </dx:ASPxTextBox>
                                    </td>
                                    <td>是否豁免</td>
                                    <td>
                                        <dx:ASPxComboBox ID="cmb_immunity" ClientInstanceName="immunity" runat="server" ValueType="System.String" CssClass="linewrite" Width="100px" Height="27px" BackColor="#FDF7D9" ForeColor="#31708f">
                                        </dx:ASPxComboBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="8" style="text-align:center;">
                                        <asp:Button ID="btn_save" runat="server" Text="保存" class="btn btn-large btn-primary" OnClientClick="if(validate()==false)return false;" Width="50px"
                                           OnClick="btn_save_Click" /> 
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <%--<div class="row  row-container">
            <div class="panel panel-info">
                <div class="panel-heading" data-toggle="collapse" data-target="#CPXX1">
                    <strong>生效日期</strong>
                </div>
                <div class="panel-body " id="CPXX1">
                    <div class="col-xs-12 col-sm-12  col-md-12 col-lg-12">
                        <asp:UpdatePanel runat="server" ID="p1" UpdateMode="Conditional" >
                            <ContentTemplate>

                                <asp:Button ID="btnadd" runat="server" Text="新增" class="btn btn-default btn-primary" Width="50px" OnClick="btnadd_Click" />
                                <asp:Button ID="btndel" runat="server" Text="删除" class="btn btn-default btn-primary" Width="50px"  OnClick="btndel_Click" />

                                <dx:aspxgridview ID="gv" runat="server" AutoGenerateColumns="False"  KeyFieldName="numid"  Theme="MetropolisBlue"
                                    ClientInstanceName="gv"  EnableTheming="True" Border-BorderColor="#DCDCDC">
                                    <SettingsPager PageSize="1000" />
                                    <Settings ShowFooter="True"/>
                                    <SettingsBehavior AllowSelectByRowClick="True" AllowDragDrop="False" AllowSort="False" />
                                    <Columns>
                                        <dx:GridViewCommandColumn SelectAllCheckboxMode="Page" ShowClearFilterButton="true" ShowSelectCheckbox="true" Name="Sel" Width="30" VisibleIndex="0"></dx:GridViewCommandColumn>
                                        <dx:GridViewDataTextColumn Caption="生效日期" FieldName="Effective_date" Width="100px" VisibleIndex="1">
                                            <Settings AllowCellMerge="False" />
                                            <DataItemTemplate>                
                                                <dx:ASPxDateEdit ID="Effective_date" runat="server" EditFormat="Custom" Width="110px"  UseMaskBehavior="true" EditFormatString="yyyy/MM/dd"
                                                    ClientInstanceName='<%# "Effective_date"+Container.VisibleIndex.ToString() %>' 
                                                    Border-BorderStyle="None" BorderBottom-BorderStyle="Solid" ButtonStyle-BorderBottom-BorderColor="#ccc" BackColor="#FDF7D9"
                                                    DisabledStyle-BackColor="Transparent" DisabledStyle-BorderBottom-BorderStyle="None">
                                                    <CalendarProperties><FastNavProperties DisplayMode="Inline" /></CalendarProperties>
                                                </dx:ASPxDateEdit>          
                                            </DataItemTemplate>        
                                        </dx:GridViewDataTextColumn> 
                                        <dx:GridViewDataTextColumn Caption="截止日期" FieldName="End_date" Width="100px" VisibleIndex="2">
                                            <Settings AllowCellMerge="False" />
                                            <DataItemTemplate>                
                                                <dx:ASPxDateEdit ID="End_date" runat="server" EditFormat="Custom" Width="110px"  UseMaskBehavior="true" EditFormatString="yyyy/MM/dd"
                                                    ClientInstanceName='<%# "End_date"+Container.VisibleIndex.ToString() %>'
                                                    Border-BorderStyle="None" BorderBottom-BorderStyle="Solid" ButtonStyle-BorderBottom-BorderColor="#ccc" BackColor="#FDF7D9" 
                                                    DisabledStyle-BackColor="Transparent" DisabledStyle-BorderBottom-BorderStyle="None">
                                                    <CalendarProperties><FastNavProperties DisplayMode="Inline" /></CalendarProperties>
                                                </dx:ASPxDateEdit>          
                                            </DataItemTemplate>        
                                        </dx:GridViewDataTextColumn>
                                                                                
                                        <dx:GridViewDataTextColumn FieldName="numid" Width="0px" >
                                             <HeaderStyle CssClass="hidden" />
                                             <CellStyle CssClass="hidden"></CellStyle>
                                             <FooterCellStyle CssClass="hidden"></FooterCellStyle>
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="id" Width="0px">
                                             <HeaderStyle CssClass="hidden" />
                                             <CellStyle CssClass="hidden"></CellStyle>
                                             <FooterCellStyle CssClass="hidden"></FooterCellStyle>
                                        </dx:GridViewDataTextColumn>
                                    </Columns>
                                    <Styles>
                                        <Header BackColor="#E4EFFA" Border-BorderColor="#DCDCDC" HorizontalAlign="Left" VerticalAlign="Top"></Header>   
                                        <SelectedRow BackColor="#FDF7D9"></SelectedRow>   
                                        <AlternatingRow BackColor="#f2f3f2"></AlternatingRow>
                                        <Cell Border-BorderColor="#DCDCDC" BorderLeft-BorderWidth="0"  BorderRight-BorderWidth="0" BorderTop-BorderWidth="0"></Cell>
                                        <CommandColumn Border-BorderColor="#DCDCDC" BorderRight-BorderStyle="None"></CommandColumn>
                                    </Styles>
                                </dx:aspxgridview>
                               
                             </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
        </div>--%>

        <div class="row  row-container" id="div_grid" style="display:none;">
            <div class="panel panel-info">
                <div class="panel-heading" data-toggle="collapse" data-target="#CPXX1">
                    <strong>生效日期</strong>
                </div>
                <div class="panel-body " id="CPXX1">
                    <div class="col-xs-12 col-sm-12  col-md-12 col-lg-12">
                        <%--<asp:UpdatePanel runat="server" ID="p1" UpdateMode="Conditional" >
                            <ContentTemplate>--%>

                                <dx:aspxgridview ID="gv" runat="server"  ClientInstanceName="grid" AutoGenerateColumns="False"  KeyFieldName="id"  Theme="MetropolisBlue"
                                    OnCustomCallback="gv_CustomCallback" Width="730px"
                                    OnRowValidating="gv_RowValidating" OnRowUpdating="gv_RowUpdating" OnRowInserting="gv_RowInserting" OnRowDeleting="gv_RowDeleting" 
                                    EnableTheming="True" Border-BorderColor="#DCDCDC" >
                                    <SettingsPager PageSize="1000" />
                                    <Toolbars>
                                        <dx:GridViewToolbar ItemAlign="Right" EnableAdaptivity="true">
                                            <Items>
                                                <dx:GridViewToolbarItem Command="New" Text="新增" />
                                                <dx:GridViewToolbarItem Command="Edit" Text="修改"  />
                                                <dx:GridViewToolbarItem Command="Delete" Text="删除" />
                                            </Items>
                                        </dx:GridViewToolbar>
                                    </Toolbars>
                                    <Settings ShowFilterRow="false" ShowGroupPanel="false"
                                        VerticalScrollBarMode="Visible" VerticalScrollBarStyle="Standard" VerticalScrollableHeight="200" ShowFooter="True" />
                                    <SettingsBehavior AllowFocusedRow="True" ColumnResizeMode="Control"  />
                                    <Columns> 
                                        <dx:GridViewDataDateColumn Caption="生效日期" FieldName="Effective_date" VisibleIndex="1" Width="210px">
                                            <PropertiesDateEdit Width="150px">
                                            </PropertiesDateEdit>
                                        </dx:GridViewDataDateColumn>
                                        <dx:GridViewDataDateColumn Caption="截止日期" FieldName="End_date" VisibleIndex="2" Width="210px">
                                             <PropertiesDateEdit Width="150px">
                                            </PropertiesDateEdit>
                                        </dx:GridViewDataDateColumn>
                                        <dx:GridViewDataTextColumn Caption="id" FieldName="id" Width="0px" VisibleIndex="99"                          
                                            HeaderStyle-CssClass="hidden" CellStyle-CssClass="hidden" FooterCellStyle-CssClass="hidden"></dx:GridViewDataTextColumn>  
                                    </Columns>
                                    <EditFormLayoutProperties ColCount="2">
                                        <Items>
                                            <dx:GridViewColumnLayoutItem ColumnName="Effective_date" />
                                            <dx:GridViewColumnLayoutItem ColumnName="End_date" />
                                            <dx:EditModeCommandLayoutItem ColSpan="2" HorizontalAlign="right" />
                                        </Items>
                                    </EditFormLayoutProperties>
                                    <Styles>
                                        <Header BackColor="#E4EFFA" Border-BorderColor="#DCDCDC" HorizontalAlign="Left" VerticalAlign="Top"></Header>   
                                        <SelectedRow BackColor="#FDF7D9"></SelectedRow>   
                                        <AlternatingRow BackColor="#f2f3f2"></AlternatingRow>
                                        <Cell Border-BorderColor="#DCDCDC" BorderLeft-BorderWidth="0"  BorderRight-BorderWidth="0" BorderTop-BorderWidth="0">
                                            <BorderLeft BorderWidth="0px" />
                                            <BorderTop BorderWidth="0px" />
                                            <BorderRight BorderWidth="0px" />
                                        </Cell>
                                        <FocusedRow BackColor="#99CCFF" ForeColor="#0000CC"></FocusedRow>
                                        <Footer HorizontalAlign="Right"></Footer>
                                    </Styles>
                                    <Border BorderColor="Gainsboro" />
                                </dx:aspxgridview>
                               
                             <%--</ContentTemplate>
                        </asp:UpdatePanel>--%>
                    </div>
                </div>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
