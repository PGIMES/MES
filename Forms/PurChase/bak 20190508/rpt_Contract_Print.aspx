<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rpt_Contract_Print.aspx.cs" Inherits="Forms_PurChase_rpt_Contract_Print" %>

<%@ Register Assembly="FastReport.Web, Version=2016.1.0.0, Culture=neutral, PublicKeyToken=db7e5ce63278458c" Namespace="FastReport.Web" TagPrefix="cc1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <cc1:WebReport ID="WebReport1" runat="server" />
    </div>
    </form>
</body>
</html>
