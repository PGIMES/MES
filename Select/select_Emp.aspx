<%@ Page Language="C#" AutoEventWireup="true" CodeFile="select_Emp.aspx.cs" Inherits="select_Emp" %>

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
                    <td  >关键字：                        
                        <asp:TextBox ID="txtKeywords" runat="server" Width="100px"></asp:TextBox>
                        <asp:Button ID="BtnStartSearch" runat="server" Text="查询" OnClick="BtnStartSearch_Click" />
                    </td>
                    <td align="right">&nbsp;</td>
                </tr>
                <tr>
                    <td align="center" colspan="2">

                        <asp:Label ID="lb_msg" runat="server" Text="" ForeColor="Red"></asp:Label>
                        <asp:GridView ID="GridView1" Width="400px"
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

                                <asp:BoundField DataField="workcode" HeaderText="工号"
                                    ReadOnly="True">
                                    <HeaderStyle Wrap="True" />
                                </asp:BoundField>
                                <asp:BoundField DataField="lastname" HeaderText="姓名"
                                    ReadOnly="True">
                                    <HeaderStyle Wrap="True" />
                                </asp:BoundField>
                                <asp:BoundField DataField="dept_name" HeaderText="部门">
                                    <HeaderStyle Wrap="False" />
                                    <ItemStyle Width="100px" />
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
