<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PUR_PO_Dtl_Category.aspx.cs" Inherits="Forms_PurChase_PUR_PO_Dtl_Category" %>

<%@ Register Assembly="DevExpress.Web.v17.2, Version=17.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>【采购类别】</title>
    <script src="/Content/js/jquery.min.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div>

        <dx:ASPxGridView ID="GV_PART2" runat="server" KeyFieldName="ID" Width="500px" ClientInstanceName="grid2"  Visible="false">
            <SettingsBehavior AllowDragDrop="TRUE" AllowFocusedRow="True" AllowSelectByRowClick="True" ColumnResizeMode="Control" AllowEllipsisInText="true" />
            <SettingsPager PageSize="100"></SettingsPager>
            <Settings  VerticalScrollBarMode="Visible" VerticalScrollBarStyle="Standard" VerticalScrollableHeight="300"
                ShowFilterRow="True" ShowFilterRowMenu="True" ShowFilterRowMenuLikeItem="True"  ShowFooter="True"/>
            <SettingsSearchPanel Visible="True" />
            <SettingsFilterControl AllowHierarchicalColumns="True"></SettingsFilterControl>
            <Columns>
                <dx:GridViewDataTextColumn Caption="类别" FieldName="LB" Width="200px" VisibleIndex="1"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="描述CODE" FieldName="MS_CODE" Width="100px" VisibleIndex="2"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="描述" FieldName="MS" Width="200px" VisibleIndex="2"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="ID" Width="0px" >
                        <HeaderStyle CssClass="hidden" />
                        <CellStyle CssClass="hidden"></CellStyle>
                        <FooterCellStyle CssClass="hidden"></FooterCellStyle>
                </dx:GridViewDataTextColumn>
            </Columns>
            <Styles>
                <Header BackColor="#99CCFF"></Header>
                <Footer HorizontalAlign="Right"></Footer>
            </Styles>
        </dx:ASPxGridView>

    </div>
    </form>
</body>
</html>
