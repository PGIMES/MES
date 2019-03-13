<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rptPoPay.aspx.cs" Inherits="rptPoPay" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" Font-Size="8pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" Width="768px" Height="628px">
            <LocalReport ReportPath="Forms\PurChase\rptPoPay.rdlc">
                <DataSources>
                    <rsweb:ReportDataSource DataSourceId="ObjectDataSource1" Name="DataSet1" />
                     
                </DataSources>
            </LocalReport>
        </rsweb:ReportViewer>
        <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetData" TypeName="PoDataSetTableAdapters.rpt_Pur_PoPayTableAdapter">
            <SelectParameters>
                <asp:Parameter DefaultValue="" Name="PoNo" Type="String" />
                <%--<asp:Parameter DefaultValue="main" Name="tbl" Type="String" />--%>
            </SelectParameters>
        </asp:ObjectDataSource>
         
    </div>
    </form>
</body>
</html>
