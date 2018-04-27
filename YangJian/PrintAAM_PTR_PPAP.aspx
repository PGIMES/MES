<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PrintAAM_PTR_PPAP.aspx.cs" Inherits="PrintAAM_PTR_PPAP" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>打印-AAM PTR/PPAP标签</title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div>
        <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" Font-Size="8pt" Height="800px" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" Width="" ZoomMode="PageWidth">
            <LocalReport ReportPath="YangJian\printAAM_PTR_PPAP.rdlc">
                <DataSources>
                    <rsweb:ReportDataSource DataSourceId="ObjectDataSource1" Name="DataSetAAM_PTR_PPAP" />
                </DataSources>
            </LocalReport>
        </rsweb:ReportViewer>
    
        <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetData" TypeName="MESDataSetTableAdapters.print_YJ_AAM_PTR_PPAPTableAdapter">
            <SelectParameters>
                <asp:Parameter Name="requestid" Type="String" />

                <asp:Parameter Name="type" Type="String" />

            </SelectParameters>
            
        </asp:ObjectDataSource>
    </div>
    </form>
</body>
</html>
