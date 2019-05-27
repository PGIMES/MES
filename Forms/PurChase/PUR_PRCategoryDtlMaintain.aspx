<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PUR_PRCategoryDtlMaintain.aspx.cs" MaintainScrollPositionOnPostback="true"  Inherits="Forms_PurChase_PUR_PRCategoryDtlMaintain" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource1" OnRowCommand="GridView1_RowCommand">
            <Columns>
                <asp:BoundField DataField="ID" HeaderText="ID" SortExpression="ID" />
                <asp:BoundField DataField="Class_ID" HeaderText="Class_ID" SortExpression="Class_ID" />
                <asp:BoundField DataField="Class" HeaderText="Class" SortExpression="Class" />
                <asp:BoundField DataField="type" HeaderText="type" SortExpression="type" />
                <asp:BoundField DataField="type2" HeaderText="type2" SortExpression="type2" />
                <asp:BoundField DataField="yn" HeaderText="yn" SortExpression="yn" />
                <asp:TemplateField HeaderText="deptcode" SortExpression="deptcode">
                    <EditItemTemplate>
                        <%--<asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("deptcode") %>'></asp:TextBox>--%>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:TextBox ID="txtdeptcode"  TextMode="MultiLine"   Width="400px" Height="60px" runat="server" Text='<%# Bind("deptcode") %>'></asp:TextBox>
                        <%--<asp:Label ID="Label1" runat="server" Text='<%# Bind("deptcode") %>'></asp:Label>--%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="deptname" SortExpression="deptname">
                    <EditItemTemplate>
                       <asp:Label ID="Label2" runat="server" Text='<%# Bind("deptname") %>'></asp:Label>
                    </EditItemTemplate>
                    <ItemTemplate>
                         <asp:TextBox ID="txtdeptname" TextMode="MultiLine"  Width="400px" Height="60px"  runat="server" Text='<%# Bind("deptname") %>'></asp:TextBox>
                        
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ShowHeader="False">
                    <ItemTemplate>
                        <asp:button ID="btnSave" runat="server" CausesValidation="false" CommandName="save" CommandArgument='<%# Container.DataItemIndex %>' Text="Save" OnClick="btnSave_Click"></asp:button>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ApplicationServices %>" 
            SelectCommand="SELECT * FROM [PUR_PR_Category_dtl] ORDER BY [ID]" 
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
