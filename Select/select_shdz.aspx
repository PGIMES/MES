<%@ Page Language="C#" AutoEventWireup="true" CodeFile="select_shdz.aspx.cs" Inherits="Select_select_shdz" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body  style="background: url(../images/bg.jpg) repeat-x;">
    <form id="form1" runat="server">
    <div>
      <table style="width:100%">
                          <tr>
                             <td >
                                 收货地址</td>
                             <td >
                                 <asp:TextBox ID="txt_shdz" runat="server" Width="300px"></asp:TextBox>
                             </td>
                             <td >
                                 收货人信息</td>
                             <td >
                                 <asp:TextBox ID="txt_shxx" runat="server" Width="300px"></asp:TextBox>
                             </td>
                              <td align="right">
                                <asp:Button ID="BtnStartSearch" runat="server" Text="查询"  OnClick="BtnStartSearch_Click"  />
                              </td>
                          </tr>
                          <tr>
                             <td align="center" colspan="5">
                                 
                                     <asp:Label ID="lb_msg" runat="server" Text="" ForeColor="Red"></asp:Label>
                                        <asp:GridView ID="GridView1"  Width="100%"
                                        AllowMultiColumnSorting="True" AllowPaging="True"
                                    AllowSorting="True" AutoGenerateColumns="False" 
                                    
                                     OnPageIndexChanging="GridView1_PageIndexChanging"
                                     OnRowDataBound="GridView1_RowDataBound"
                                          runat="server" 
                                         onselectedindexchanged="GridView1_SelectedIndexChanged" Font-Size="Small" 
                                          
                                          >
                                           <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                             <PagerSettings FirstPageText="首页" LastPageText="尾页" 
                                Mode="NextPreviousFirstLast" NextPageText="下页" PreviousPageText="上页" />
                           <PagerStyle ForeColor="Red" />
                                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                <EditRowStyle BackColor="#999999" />
                                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />    
                                
                                 <Columns>
                                      <asp:BoundField DataField="requestid" HeaderText="requestid" 
                                       ReadOnly="True" >  
                                       <HeaderStyle Wrap="True" />
                                        <ItemStyle Width="1%" />
                                    </asp:BoundField>
                                        <asp:BoundField DataField="shdz" HeaderText="收货地址" 
                                       ReadOnly="True" >  
                                       <HeaderStyle Wrap="True" />
                                        <ItemStyle Width="45%" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="shrxx" HeaderText="收货人信息" 
                                       ReadOnly="True" >  
                                       <HeaderStyle Wrap="True" />
                                        <ItemStyle Width="45%" />
                                    </asp:BoundField>
                                     <asp:CommandField ButtonType="Button" SelectText="选择" 
                                         ShowSelectButton="True" HeaderText="选择" >
                                     <ItemStyle Width="10%" />
                                     </asp:CommandField>
                                </Columns>
                                        </asp:GridView>
                                        
                             </td>
                          </tr>
                        
                </table>
    </div>
    </form>
</body>
</html>
