<%@ Page Language="C#" AutoEventWireup="true" CodeFile="select_product_m.aspx.cs" Inherits="Select_select_product_m" %>

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
                                 PGI项目号</td>
                             <td >
                                 <asp:TextBox ID="txtpgi_no" runat="server" Width="180px"></asp:TextBox>
                             </td>
                             <td >
                                 客户零件号</td>
                             <td >
                                 <asp:TextBox ID="txtpn" runat="server" Width="180px"></asp:TextBox>
                              </td>
                              <td align="right">
                                <asp:Button ID="BtnStartSearch" runat="server" Text="查询"  OnClick="BtnStartSearch_Click"  />
                              </td>
                          </tr>
                          <tr>
                             <td align="center" colspan="7">
                                 
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
                                      <asp:BoundField DataField="pgino" HeaderText="PGI项目号" 
                                       ReadOnly="True" >  
                                       <HeaderStyle Wrap="True" />
                                        <ItemStyle Width="5%" />
                                    </asp:BoundField>
                                        <asp:BoundField DataField="productcode" HeaderText="客户零件号" 
                                       ReadOnly="True" >  
                                       <HeaderStyle Wrap="True" />
                                        <ItemStyle Width="8%" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="productname" HeaderText="零件名称" 
                                       ReadOnly="True" >  
                                       <HeaderStyle Wrap="True" />
                                        <ItemStyle Width="10%" />
                                    </asp:BoundField>
                                     <asp:BoundField DataField="make_factory" HeaderText="生产地点" 
                                       ReadOnly="True" >  
                                       <HeaderStyle Wrap="True" />
                                        <ItemStyle Width="4%" />
                                    </asp:BoundField>

                                     <asp:BoundField DataField="version" HeaderText="工程版本号" 
                                       ReadOnly="True" >  
                                       <HeaderStyle Wrap="True" />
                                        <ItemStyle Width="6%" />
                                    </asp:BoundField>

                                     <%--<asp:BoundField DataField="domain" HeaderText="备注" 
                                       ReadOnly="True" >  
                                       <HeaderStyle Wrap="True" />
                                        <ItemStyle Width="10%" />
                                    </asp:BoundField>--%>

                                     <asp:CommandField ButtonType="Button" SelectText="选择" 
                                         ShowSelectButton="True" HeaderText="选择" >
                                     <ItemStyle Width="6%" />
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
