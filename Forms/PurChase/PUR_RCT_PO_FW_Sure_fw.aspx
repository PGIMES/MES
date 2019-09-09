<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PUR_RCT_PO_FW_Sure_fw.aspx.cs" Inherits="Forms_PurChase_PUR_RCT_PO_FW_Sure_fw" %>

<%@ Register Assembly="DevExpress.Web.v17.2, Version=17.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>【财务确认】</title>
    <link href="/Content/css/bootstrap.min.css" rel="stylesheet" />
    <script src="/Content/js/jquery.min.js"></script>
    <script src="/Content/js/bootstrap.min.js"></script>
    <script src="/Content/js/layer/layer.js"></script>
    <script type="text/javascript">

        $(document).ready(function () {
            
        });

        //function RefreshRow(vi) {
        //    var po_notax_TotalPrice = eval('po_notax_TotalPrice' + vi);
        //    var FPAmount = eval('FPAmount' + vi);
        //    var chayi = eval('chayi' + vi);

        //    var po_notax_TotalPrice_value = Number($.trim(po_notax_TotalPrice.GetText()) == "" ? 0 : $.trim(po_notax_TotalPrice.GetText()));//采购金额合计
        //    var FPAmount_value = Number($.trim(FPAmount.GetText()) == "" ? 0 : $.trim(FPAmount.GetText()));//发票金额
        //    var chayi_value = 0;
        //    chayi_value = (FPAmount_value - po_notax_TotalPrice_value);
        //    chayi.SetText(chayi_value.toFixed(2));//chayi_value.toFixed(2)
        //}

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
            <div class="col-xs-12 col-sm-12  col-md-12 col-lg-12">
                <asp:UpdatePanel runat="server" ID="p1" UpdateMode="Conditional" >
                    <ContentTemplate>
                        <dx:aspxgridview ID="gv" runat="server" AutoGenerateColumns="False"  KeyFieldName="ContractLine"  Theme="MetropolisBlue"
                            ClientInstanceName="gv"  EnableTheming="True" Border-BorderColor="#DCDCDC">
                            <SettingsPager PageSize="1000" />
                            <Settings ShowFooter="True" VerticalScrollBarMode="Visible" VerticalScrollBarStyle="Standard" VerticalScrollableHeight="350"/>
                            <SettingsBehavior AllowSelectByRowClick="True" AllowDragDrop="False" AllowSort="False" />
                            <Columns>
                                <dx:GridViewDataTextColumn Caption="验收单号" FieldName="rctno" Width="100px" VisibleIndex="1"></dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="订单" FieldName="PONo" Width="70px" VisibleIndex="2"></dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="采购订单项" FieldName="PORowid" Width="70px" VisibleIndex="3"></dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="采购金额合计" FieldName="po_notax_TotalPrice" Width="100px" VisibleIndex="4">
                                    <Settings AllowCellMerge="False"/>
                                    <DataItemTemplate>                                        
                                        <dx:ASPxTextBox ID="po_notax_TotalPrice" Width="90px" runat="server" Value='<%# Eval("po_notax_TotalPrice")%>' 
                                            ClientInstanceName='<%# "po_notax_TotalPrice"+Container.VisibleIndex.ToString() %>' 
                                            Border-BorderWidth="0" ReadOnly="true" BackColor="Transparent" HorizontalAlign="Right">
                                        </dx:ASPxTextBox> 
                                    </DataItemTemplate>      
                                </dx:GridViewDataTextColumn> 
                                <dx:GridViewDataTextColumn Caption="发票金额" FieldName="FPAmount" Width="100px" VisibleIndex="5">
                                    <Settings AllowCellMerge="False"/>
                                    <DataItemTemplate> 
                                        <%-- ClientSideEvents-ValueChanged='<%# "function(s,e){RefreshRow("+Container.VisibleIndex+");}" %>' --%>
                                        <%-- OnTextChanged="FPAmount_TextChanged" AutoPostBack="true"    --%>
                                        <dx:ASPxTextBox ID="FPAmount" Width="90px" runat="server" Value='<%# Eval("FPAmount")%>' 
                                            ClientInstanceName='<%# "FPAmount"+Container.VisibleIndex.ToString() %>'
                                            OnTextChanged="FPAmount_TextChanged" AutoPostBack="true"                                
                                            Border-BorderStyle="None" BorderBottom-BorderStyle="Solid" ButtonStyle-BorderBottom-BorderColor="#ccc" BackColor="#FDF7D9"
                                            HorizontalAlign="Right">
                                        </dx:ASPxTextBox> 
                                    </DataItemTemplate>        
                                </dx:GridViewDataTextColumn>  
                                <dx:GridViewDataTextColumn Caption="差异金额" FieldName="chayi" Width="70px" VisibleIndex="6">
                                    <Settings AllowCellMerge="False"/>
                                    <DataItemTemplate>                                        
                                        <dx:ASPxTextBox ID="chayi" Width="60px" runat="server" Value='<%# Eval("chayi")%>' 
                                            ClientInstanceName='<%# "chayi"+Container.VisibleIndex.ToString() %>' 
                                            Border-BorderWidth="0" ReadOnly="true" BackColor="Transparent" HorizontalAlign="Right">
                                        </dx:ASPxTextBox> 
                                    </DataItemTemplate>        
                                </dx:GridViewDataTextColumn> 
                                <dx:GridViewDataTextColumn Caption="总账账户" FieldName="kjkm_code" Width="330px" VisibleIndex="7">
                                    <Settings AllowCellMerge="False"/>
                                    <DataItemTemplate>
                                        <dx:ASPxComboBox ID="kjkm_code" runat="server" ValueType="System.String"
                                            Width="320px" ClientInstanceName='<%# "kjkm_code"+Container.VisibleIndex.ToString() %>'
                                            Border-BorderStyle="None" BorderBottom-BorderStyle="Solid" ButtonStyle-BorderBottom-BorderColor="#ccc" BackColor="#FDF7D9">
                                        </dx:ASPxComboBox>
                                    </DataItemTemplate>        
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="domain" FieldName="domain" VisibleIndex="99" Width="0px"                                
                                    HeaderStyle-CssClass="hidden" CellStyle-CssClass="hidden" FooterCellStyle-CssClass="hidden"></dx:GridViewDataTextColumn>
                            </Columns>
                            <TotalSummary>
                                <dx:aspxsummaryitem DisplayFormat="小计:" FieldName="PORowid" ShowInColumn="PORowid" ShowInGroupFooterColumn="PORowid" SummaryType="Sum" />
                                <dx:aspxsummaryitem DisplayFormat="{0:N2}" FieldName="po_notax_TotalPrice" ShowInColumn="po_notax_TotalPrice" 
                                    ShowInGroupFooterColumn="po_notax_TotalPrice" SummaryType="Sum" />
                                <dx:aspxsummaryitem DisplayFormat="{0:N2}" FieldName="FPAmount" ShowInColumn="FPAmount" ShowInGroupFooterColumn="FPAmount" SummaryType="Sum" />
                                <dx:aspxsummaryitem DisplayFormat="{0:N2}" FieldName="chayi" ShowInColumn="chayi" ShowInGroupFooterColumn="chayi" SummaryType="Sum" />
                            </TotalSummary>
                            <Styles>
                                <Header BackColor="#E4EFFA" Border-BorderColor="#DCDCDC" HorizontalAlign="Left" VerticalAlign="Top"></Header>   
                                <SelectedRow BackColor="#FDF7D9"></SelectedRow>   
                                <AlternatingRow BackColor="#f2f3f2"></AlternatingRow>
                                <Cell Border-BorderColor="#DCDCDC" BorderLeft-BorderWidth="0"  BorderRight-BorderWidth="0" BorderTop-BorderWidth="0"></Cell>
                                <CommandColumn Border-BorderColor="#DCDCDC" BorderRight-BorderStyle="None"></CommandColumn>
                                <Footer ForeColor="Red" Font-Bold="true"></Footer>
                            </Styles>
                        </dx:aspxgridview>


                                
                        <asp:Button ID="btnsave" runat="server" Text="保存" class="btn btn-large btn-primary" Width="50px" OnClick="btnsave_Click"/> 
                               
                        </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
