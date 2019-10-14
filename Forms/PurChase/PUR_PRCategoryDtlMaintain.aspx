<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PUR_PRCategoryDtlMaintain.aspx.cs" MaintainScrollPositionOnPostback="true"  Inherits="Forms_PurChase_PUR_PRCategoryDtlMaintain" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <style>
        body{font-size:9pt}
        td{font-size:9pt}
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource1" OnRowCommand="GridView1_RowCommand" CellPadding="4" ForeColor="#333333" GridLines="None" >
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:BoundField DataField="ID" HeaderText="ID" SortExpression="ID" />
                <asp:BoundField DataField="Class_ID" HeaderText="Class_ID" SortExpression="Class_ID" />
                <asp:BoundField DataField="Class" HeaderText="Class" SortExpression="Class" ItemStyle-Width="100px" >
<ItemStyle Width="100px"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="type" HeaderText="type" SortExpression="type"  ItemStyle-Width="100px" >
<ItemStyle Width="100px"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="type2" HeaderText="type2" SortExpression="type2"  ItemStyle-Width="100px" >
<ItemStyle Width="100px"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="yn" HeaderText="yn" SortExpression="yn"  Visible="false"/>
                <asp:TemplateField HeaderText="成本中心代码" SortExpression="deptcode">
                    <EditItemTemplate>
                         
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:TextBox ID="txtdeptcode"  TextMode="MultiLine"   Width="300px" Height="40px" runat="server" Text='<%# Bind("deptcode") %>'></asp:TextBox>
                        <asp:Label ID="lbldeptcode" runat="server" Text='<%# Bind("deptcode") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="部门名称" SortExpression="deptname">
                    <EditItemTemplate>
                      
                    </EditItemTemplate>
                    <ItemTemplate>
                         <asp:TextBox ID="txtdeptname" TextMode="MultiLine"  Width="400px" Height="40px"  runat="server" Text='<%# Bind("deptname") %>'></asp:TextBox>
                          <asp:Label ID="lbldeptname" runat="server" Text='<%# Bind("deptname") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ShowHeader="False"    >
                    <ItemTemplate>
                        <asp:button ID="btnSave" runat="server" CausesValidation="false" CommandName="save" CommandArgument='<%# Container.DataItemIndex %>' Text="Save" OnClick="btnSave_Click"></asp:button>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <EditRowStyle BackColor="#2461BF" />
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
            <RowStyle BackColor="#EFF3FB" />
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <SortedAscendingCellStyle BackColor="#F5F7FB" />
            <SortedAscendingHeaderStyle BackColor="#6D95E1" />
            <SortedDescendingCellStyle BackColor="#E9EBEF" />
            <SortedDescendingHeaderStyle BackColor="#4870BE" />
        </asp:GridView>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ApplicationServices %>" 
            SelectCommand="SELECT * FROM [PUR_PR_Category_dtl] where yn='Y' ORDER BY [ID]" 
            UpdateCommand="UPDATE PUR_PR_Category_dtl SET deptcode = @deptcode, deptname = @deptname WHERE (ID = @ID)">
            <UpdateParameters>
                <asp:Parameter Name="deptcode" />
                <asp:Parameter Name="deptname" />
                <asp:Parameter Name="ID" />
            </UpdateParameters>
        </asp:SqlDataSource>
    </div>
    </form>
</body>
</html>
