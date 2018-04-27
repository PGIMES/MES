<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Set.aspx.cs" Inherits="WebForm.Applications.WeiXin.Agents.Set" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <br />
    <table cellpadding="0" cellspacing="1" border="0" width="99%" class="formtable">
        <tr>
            <th style="width: 80px;">应用ID：</th>
            <td><input type="text" id="agentid" name="agentid" class="mytext" runat="server" readonly="readonly"/></td>
        </tr>
        <tr>
            <th>应用名称：</th>
            <td><input type="text" id="name" name="name" class="mytext" runat="server"/></td>
        </tr>
        <tr>
            <th>地理位置上报：</th>
            <td><input type="text" id="Text1" name="name" class="mytext" runat="server"/></td>
        </tr>
        <tr>
            <th>地理位置上报：</th>
            <td><input type="text" id="Text2" name="name" class="mytext" runat="server"/></td>
        </tr>
    </table>
    </form>
</body>
</html>
