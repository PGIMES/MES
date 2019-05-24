<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PO_Print_his.aspx.cs" Inherits="Forms_PurChase_PO_Print_his" %>

<%@ Register Assembly="DevExpress.Web.v17.2, Version=17.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>【打印历史记录】</title>
    <link href="/Content/css/bootstrap.min.css" rel="stylesheet" />
    <script src="/Content/js/jquery.min.js"></script>
    <script src="/Content/js/bootstrap.min.js"></script>
    <script src="/Content/js/layer/layer.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

        <dx:ASPxGridView ID="gv_his" runat="server" KeyFieldName="id"
            AutoGenerateColumns="False" Width="450px" OnPageIndexChanged="gv_his_PageIndexChanged"  ClientInstanceName="gv_his">
            <SettingsPager PageSize="100" ></SettingsPager>
            <Settings ShowFilterRow="True" ShowGroupPanel="false" ShowFilterRowMenu="True" ShowFilterRowMenuLikeItem="True" AutoFilterCondition="Contains" 
                VerticalScrollBarMode="Visible" VerticalScrollBarStyle="Standard" VerticalScrollableHeight="400"  />
            <SettingsBehavior AllowFocusedRow="false" AllowSelectByRowClick="false"  ColumnResizeMode="Control"/>
            <Columns>   
                <dx:GridViewDataTextColumn Caption="采购单号" FieldName="PoNo" Width="100px" VisibleIndex="1"></dx:GridViewDataTextColumn>       
                <dx:GridViewDataTextColumn Caption="打印人工号" FieldName="PrintById" Width="100px" VisibleIndex="2"></dx:GridViewDataTextColumn> 
                <dx:GridViewDataTextColumn Caption="打印人姓名" FieldName="PrintByName" Width="100px" VisibleIndex="3"></dx:GridViewDataTextColumn>  
                <dx:GridViewDataDateColumn Caption="打印时间" FieldName="PrintTime" Width="150px" VisibleIndex="4">
                    <PropertiesDateEdit DisplayFormatString="yyyy/MM/dd HH:mm:ss"></PropertiesDateEdit>
                </dx:GridViewDataDateColumn> 
                <dx:GridViewDataTextColumn Caption="id" FieldName="id" VisibleIndex="99"
                    HeaderStyle-CssClass="hidden" CellStyle-CssClass="hidden" FooterCellStyle-CssClass="hidden"></dx:GridViewDataTextColumn>
            </Columns>
            <Styles>
                <Header BackColor="#99CCFF"></Header>
                <FocusedRow BackColor="#99CCFF" ForeColor="#0000CC"></FocusedRow>
                <Footer HorizontalAlign="Right"></Footer>
            </Styles>
        </dx:ASPxGridView>                    

    </form>
</body>
</html>
