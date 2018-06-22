<%@ Page Language="C#" AutoEventWireup="true" CodeFile="select_yn.aspx.cs" Inherits="Select_select_yn" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table style="width:100%">
                <tr>
                    <td >
                        是否报工：</td>
                    <td >
                        <asp:RadioButtonList ID="RadioButtonList1" runat="server" AutoPostBack="true" RepeatDirection="Horizontal"
                            OnSelectedIndexChanged="RadioButtonList1_SelectedIndexChanged">
                            <asp:ListItem Text="Y" Value="Y"></asp:ListItem>
                            <asp:ListItem Text="N" Value="N"></asp:ListItem>
                        </asp:RadioButtonList>  
                    </td>
                </tr>                       
        </table>
    </div>
    </form>
</body>
</html>
