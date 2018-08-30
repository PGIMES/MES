<%@ Page Language="C#" AutoEventWireup="true" CodeFile="select.aspx.cs" Inherits="select" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body >
    <form id="form1" runat="server">
        <div>
            <table  >
                <tr>
                    <td  >关键字：                        
                        <asp:TextBox ID="txtKeywords" runat="server" Width="200px"></asp:TextBox>
                        <asp:Button ID="BtnStartSearch" runat="server" Text="查询" OnClick="BtnStartSearch_Click" />
                    </td>
                    <td align="right">&nbsp;</td>
                </tr>
                <tr>
                    <td align="center" colspan="2">

                        <asp:Label ID="lb_msg" runat="server" Text="" ForeColor="Red"></asp:Label>
                        <asp:GridView ID="GridView1"  Width="100%"
                            AllowMultiColumnSorting="True" AllowPaging="True" AutoGenerateColumns="true"
                            OnPageIndexChanging="GridView1_PageIndexChanging"
                            OnRowDataBound="GridView1_RowDataBound"
                            runat="server"
                            OnSelectedIndexChanged="GridView1_SelectedIndexChanged" Font-Size="Small" >
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
                            </Columns>
                            <%--<PagerTemplate >
                                <table width="100%" style="display:none" >
                                    <tr>
                                        <td width="75%">
                                            <asp:Button ID="imgBtnFirst" runat="server" CommandArgument="First" CommandName="Page" Text="First" OnClick="imgBtnFirst_Click"
                                                ToolTip="第一页" />
                                            <asp:Button ID="imgBtnPrev" runat="server" CommandArgument="Prev" CommandName="Page" Text="Pre" OnClick="imgBtnPrev_Click"
                                                ToolTip="上一页" />
                                            <asp:TextBox ID="txtPageNumber" runat="server" Width="60px"></asp:TextBox>
                                            <asp:Button ID="imgBtnNext" runat="server" CommandArgument="Next" CommandName="Page" Text="Next" OnClick="imgBtnNext_Click"
                                                ToolTip="下一页" />
                                            <asp:Button ID="imgBtnLast" runat="server" CommandArgument="Last" CommandName="Page" Text="Last" OnClick="imgBtnLast_Click"
                                                ToolTip="最后页" />
                                        </td>
                                        <td align="right" width="25%">页数：<asp:Label ID="lblCurrentPage" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </PagerTemplate>--%>
                        </asp:GridView>
                         
                         
                        
                    </td>
                </tr>

            </table>
        </div>
    </form>
</body>
</html>
