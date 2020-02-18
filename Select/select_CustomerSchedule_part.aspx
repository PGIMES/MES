<%@ Page Language="C#" AutoEventWireup="true" CodeFile="select_CustomerSchedule_part.aspx.cs" Inherits="select_CustomerSchedule_part" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body> <%--style="background: url(../images/bg.jpg) repeat-x;"--%>
    <form id="form1" runat="server">
        <div>
            <table  >
                <tr>
                    <td>PGI_零件号：                        
                        <asp:TextBox ID="txtPart" runat="server" Width="300px"></asp:TextBox>
                        <asp:Button ID="BtnStartSearch" runat="server" Text="查询" OnClick="BtnStartSearch_Click" />
                    </td>
                    <td align="right">&nbsp;</td>
                </tr>
                <tr>
                    <td align="center" colspan="2">

                        <asp:Label ID="lb_msg" runat="server" Text="" ForeColor="Red"></asp:Label>
                        <asp:GridView ID="GridView1" Width="730px"
                            AllowMultiColumnSorting="True" AllowPaging="True"
                            AllowSorting="True" AutoGenerateColumns="False"
                            OnPageIndexChanging="GridView1_PageIndexChanging"
                            OnRowDataBound="GridView1_RowDataBound"
                            runat="server"
                            OnSelectedIndexChanged="GridView1_SelectedIndexChanged" Font-Size="Small">
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
                                <asp:CommandField ButtonType="Button" SelectText="选择" ItemStyle-HorizontalAlign="Center"
                                    ShowSelectButton="True" HeaderText="选择" />
                                <asp:BoundField DataField="comp" HeaderText="申请工厂" ReadOnly="True">
                                    <HeaderStyle Wrap="True" />
                                </asp:BoundField>
                                <asp:BoundField DataField="part_no" HeaderText="PGI_零件号" ReadOnly="True">
                                    <HeaderStyle Wrap="True" />
                                </asp:BoundField>
                                <asp:BoundField DataField="wlmc" HeaderText="名称" ReadOnly="True">
                                    <HeaderStyle Wrap="True" />
                                </asp:BoundField>  
                                <asp:BoundField DataField="ms" HeaderText="描述" ReadOnly="True">
                                    <HeaderStyle Wrap="True" />
                                </asp:BoundField> 
                                <asp:BoundField DataField="cust_eco" HeaderText="安全特性" ReadOnly="True">
                                    <HeaderStyle Wrap="True" />
                                </asp:BoundField> 
                                <asp:BoundField DataField="status" HeaderText="状态" ReadOnly="True">
                                    <HeaderStyle Wrap="True" />
                                </asp:BoundField> 
                            </Columns>
                        </asp:GridView>

                    </td>
                </tr>

            </table>
        </div>
    </form>
</body>
</html>

