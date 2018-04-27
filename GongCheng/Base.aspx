<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Base.aspx.cs" Inherits="GongCheng_Base" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
</head>
<body>
     <form id="form1" runat="server">
     <ajaxToolkit:ToolkitScriptManager runat="Server" EnablePartialRendering="true" ID="ScriptManager1" />
    <div>
    
        <asp:Panel ID="Panel1" runat="server" 
            GroupingText="基础参数" Width="1000px" 
            Font-Size="Small">
            <asp:GridView ID="GridView1" runat="server">
            </asp:GridView>
          
        </asp:Panel>
    
    </div>
    </form>
</body>
</html>
