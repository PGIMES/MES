<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Product_Byxmh.aspx.cs" Inherits="GongCheng_Product_Byxmh" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
     <form id="form1" runat="server">
    <div>
    
        <asp:Panel ID="Panel1" runat="server"  Height="100%"

            GroupingText="领用数量及原因" Width="1000px" 
            Font-Size="Small">
              <table style=" height:100%" >
                                      
                                        <tr>
                                        <td rowspan="2"  valign="top">
                                        <div id="Div2"  style=" overflow: scroll;  width:1000px">
                                                                     
            <asp:GridView ID="GridView1" runat="server">
            </asp:GridView>
           
                                                                    <asp:Button ID="Button1" 
                                                runat="server" Text="导出" onclick="Button1_Click" />
           
                                                                    </div>
                                                    
                                                                                    </td></tr>
                                                                                    </table>
          
        </asp:Panel>
    
    </div>
    </form>
</body>
</html>