<%@ Page Language="C#" AutoEventWireup="true" CodeFile="select_pack_part_bc.aspx.cs" Inherits="select_pack_part_bc" %>

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
                    <td>包装箱编码：                        
                        <asp:TextBox ID="txtPart" runat="server" Width="300px"></asp:TextBox>
                        <asp:Button ID="BtnStartSearch" runat="server" Text="查询" OnClick="BtnStartSearch_Click" />
                    </td>
                    <td align="right">&nbsp;</td>
                </tr>
                <tr>
                    <td align="center" colspan="2">

                        <asp:Label ID="lb_msg" runat="server" Text="" ForeColor="Red"></asp:Label>
                        <asp:GridView ID="GridView1" Width="980px"
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
                                <asp:BoundField DataField="pt_domain" HeaderText="域" ReadOnly="True">
                                    <HeaderStyle Wrap="True" />
                                </asp:BoundField>
                                <asp:BoundField DataField="pt_part" HeaderText="包装箱编码" ReadOnly="True">
                                    <HeaderStyle Wrap="True" />
                                </asp:BoundField>
                                <asp:BoundField DataField="pt_desc1" HeaderText="包装箱名称" ReadOnly="True">
                                    <HeaderStyle Wrap="True" />
                                </asp:BoundField> 
                                <asp:BoundField DataField="pt_desc2" HeaderText="尺寸" ReadOnly="True">
                                    <HeaderStyle Wrap="True" />
                                </asp:BoundField> 
                                <asp:BoundField DataField="pt_net_wt" HeaderText="单重2" ReadOnly="True">
                                    <HeaderStyle Wrap="True" />
                                </asp:BoundField>
                                <asp:BoundField DataField="pt_drwg_loc" HeaderText="材料" ReadOnly="True">
                                    <HeaderStyle Wrap="True" />
                                </asp:BoundField>
                                <asp:BoundField DataField="ad_name" HeaderText="供应商" ReadOnly="True">
                                    <HeaderStyle Wrap="True" />
                                </asp:BoundField>
                                <asp:BoundField DataField="JG" HeaderText="单价" ReadOnly="True">
                                    <HeaderStyle Wrap="True" />
                                </asp:BoundField>  
                                <asp:BoundField DataField="bclb" HeaderText="包材类别" ReadOnly="True">
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

