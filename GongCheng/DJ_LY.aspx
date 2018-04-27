<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DJ_LY.aspx.cs" Inherits="GongCheng_DJ_LY" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
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
           
                                                                    </div>
                                                    
                                                                                    </td></tr>
                                                                                    </table>
          
        </asp:Panel>
    
    </div>
    </form>
</body>
</html>