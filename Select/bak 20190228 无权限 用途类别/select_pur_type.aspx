<%@ Page Language="C#" AutoEventWireup="true" CodeFile="select_pur_type.aspx.cs" Inherits="Select_select_pur_type" %>

<%@ Register Assembly="DevExpress.Web.v17.2, Version=17.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>【辅料查询】</title>
    <script src="/Content/js/jquery.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        function rerurn_data(){
            grid.GetSelectedFieldValues('typedesc', function GetVal(values) {
                if (values.length > 0) {
                    var lstypedesc = values[0];
                    parent.setvalue_wltype(<%=nid.ToString()%>, lstypedesc);
                    var index = parent.layer.getFrameIndex(window.name);
                    parent.layer.close(index);
                }
            });
        }
        function rerurn_data2(){
            grid2.GetSelectedFieldValues('typedesc;typedesc2', function GetVal(values) {
                if (values.length > 0) {
                    var lstypedesc = values[0][0];
                    var lstypedesc2 = values[0][1];
                    parent.setvalue_wltype2(<%=nid.ToString()%>, lstypedesc, lstypedesc2);
                    var index = parent.layer.getFrameIndex(window.name);
                    parent.layer.close(index);
                }
            });
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <dx:ASPxGridView ID="GV_PART" runat="server" KeyFieldName="typedesc" Width="250px" ClientInstanceName="grid" Visible="false" >
            <SettingsBehavior AllowDragDrop="TRUE" AllowFocusedRow="True" AllowSelectByRowClick="True" ColumnResizeMode="Control" />
            <ClientSideEvents RowDblClick="function(s, e) {rerurn_data();}" /> 
            <SettingsPager PageSize="100"></SettingsPager>
            <Settings VerticalScrollBarMode="Visible" VerticalScrollBarStyle="Standard" VerticalScrollableHeight="250"
                    ShowFilterRow="True" ShowFilterRowMenu="True" ShowFilterRowMenuLikeItem="True"  ShowFooter="True"/>
            <SettingsSearchPanel Visible="True" />
            <SettingsFilterControl AllowHierarchicalColumns="True"></SettingsFilterControl>
            <Columns>
                <dx:GridViewDataTextColumn Caption="类别" FieldName="typedesc" Width="200px" VisibleIndex="1"></dx:GridViewDataTextColumn>
            </Columns>
            <Styles>
                <Header BackColor="#99CCFF"></Header>
                <Footer HorizontalAlign="Right"></Footer>
            </Styles>
        </dx:ASPxGridView>

        <dx:ASPxGridView ID="GV_PART2" runat="server" KeyFieldName="id" Width="400px" ClientInstanceName="grid2"  Visible="false">
            <SettingsBehavior AllowDragDrop="TRUE" AllowFocusedRow="True" AllowSelectByRowClick="True" ColumnResizeMode="Control" AllowEllipsisInText="true" />
            <ClientSideEvents RowDblClick="function(s, e) {rerurn_data2();}" /> 
            <SettingsPager PageSize="100"></SettingsPager>
            <Settings VerticalScrollBarMode="Visible" VerticalScrollBarStyle="Standard" VerticalScrollableHeight="250"
                    ShowFilterRow="True" ShowFilterRowMenu="True" ShowFilterRowMenuLikeItem="True"  ShowFooter="True"/>
            <SettingsSearchPanel Visible="True" />
            <SettingsFilterControl AllowHierarchicalColumns="True"></SettingsFilterControl>
            <Columns>
                <dx:GridViewDataTextColumn Caption="类别" FieldName="typedesc" Width="200px" VisibleIndex="1"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="用途类别" FieldName="typedesc2" Width="200px" VisibleIndex="2"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="id" Width="0px" >
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
