﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="select_pack_part.aspx.cs" Inherits="select_pack_part" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body style="background: url(../images/bg.jpg) repeat-x;">
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
                        <asp:GridView ID="GridView1" Width="780px"
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

                                <asp:BoundField DataField="sod_domain" HeaderText="域" ReadOnly="True">
                                    <HeaderStyle Wrap="True" />
                                </asp:BoundField>
                                <asp:BoundField DataField="sod_part" HeaderText="PGI_零件号" ReadOnly="True">
                                    <HeaderStyle Wrap="True" />
                                </asp:BoundField>
                                <asp:BoundField DataField="sod_site" HeaderText="发自" ReadOnly="True">
                                    <HeaderStyle Wrap="True" />
                                </asp:BoundField>  
                                <asp:BoundField DataField="so_ship" HeaderText="发至" ReadOnly="True">
                                    <HeaderStyle Wrap="True" />
                                </asp:BoundField> 
                                <asp:BoundField DataField="ad_name" HeaderText="顾客" ReadOnly="True">
                                    <HeaderStyle Wrap="True" />
                                </asp:BoundField>
                                <asp:BoundField DataField="sod_custpart" HeaderText="顾客零件号" ReadOnly="True">
                                    <HeaderStyle Wrap="True" />
                                </asp:BoundField>
                                <asp:BoundField DataField="pt_net_wt" HeaderText="零件重量" ReadOnly="True">
                                    <HeaderStyle Wrap="True" />
                                </asp:BoundField>
                                <asp:BoundField DataField="maxnum1" HeaderText="年用量" ReadOnly="True">
                                    <HeaderStyle Wrap="True" />
                                </asp:BoundField>
                                <asp:BoundField DataField="dj" HeaderText="销售价格" ReadOnly="True">
                                    <HeaderStyle Wrap="True" />
                                </asp:BoundField>   
                                <asp:CommandField ButtonType="Button" SelectText="选择" ItemStyle-HorizontalAlign="Center"
                                    ShowSelectButton="True" HeaderText="选择" />
                            </Columns>
                        </asp:GridView>

                    </td>
                </tr>

            </table>
        </div>
    </form>
</body>
</html>
