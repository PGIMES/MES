<%@ Page Language="C#" AutoEventWireup="true" CodeFile="select_pt_mstr.aspx.cs" Inherits="Select_select_pt_mstr" %>

<%@ Register Assembly="DevExpress.Web.v17.2, Version=17.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>【辅料查询】</title>
    <script src="/Content/js/jquery.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        function rerurn_data(){
            grid.GetSelectedFieldValues('wlh;wlmc;ms', function GetVal(values) {
                if (values.length > 0) {
                    var lswlh = values[0][0];
                    var lswlmc = values[0][1];
                    var lsms = values[0][2];

                    parent.setvalue_dj(<%=nid.ToString()%>, lswlh, lsms,lswlmc);
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
        <dx:ASPxGridView ID="GV_PART" runat="server" KeyFieldName="wlh" Width="650px" ClientInstanceName="grid" >
            <SettingsBehavior AllowDragDrop="TRUE" AllowFocusedRow="True" AllowSelectByRowClick="True" ColumnResizeMode="Control" />
            <ClientSideEvents RowDblClick="function(s, e) {rerurn_data();}" /> 
            <SettingsPager PageSize="20"></SettingsPager>
            <Settings VerticalScrollBarMode="Visible" VerticalScrollBarStyle="Standard" VerticalScrollableHeight="250"
                    ShowFilterRow="True" ShowFilterRowMenu="True" ShowFilterRowMenuLikeItem="True"  ShowFooter="True"/>
            <SettingsSearchPanel Visible="True" />
            <SettingsFilterControl AllowHierarchicalColumns="True"></SettingsFilterControl>
            <Columns>
                <dx:GridViewDataTextColumn Caption="物料号" FieldName="wlh" Width="100px" VisibleIndex="2"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="物料名称" FieldName="wlmc" Width="140px" VisibleIndex="3"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="物料描述" FieldName="ms" Width="200px" VisibleIndex="4"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="产品类" FieldName="type" Width="100px" VisibleIndex="5"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="产品状态" FieldName="pt_status" Width="100px" VisibleIndex="5"></dx:GridViewDataTextColumn>
            </Columns>
            <Styles>
                <%--<Header BackColor="#99CCFF"></Header>--%>
                <Footer HorizontalAlign="Right"></Footer>
                <Header BackColor="#31708f" Font-Bold="True" ForeColor="white" Border-BorderStyle="None" HorizontalAlign="Left" VerticalAlign="Top"></Header>
                <Footer HorizontalAlign="Right" BackColor="#cfcfcf" Font-Bold="True" ForeColor="red" Font-Size="11pt"></Footer>
                <Cell Border-BorderColor="#DCDCDC" BorderLeft-BorderWidth="0"  BorderRight-BorderWidth="0" BorderTop-BorderWidth="0"></Cell>
            </Styles>
        </dx:ASPxGridView>

    </div>
    </form>
</body>
</html>
