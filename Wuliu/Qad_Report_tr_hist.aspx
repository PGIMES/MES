<%@ Page Title="【库龄报表】" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Qad_Report_tr_hist.aspx.cs" Inherits="Wuliu_Qad_Report_tr_hist" %>

<%@ Register Assembly="DevExpress.Web.v17.2, Version=17.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <script src="../Content/js/jquery.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        $("#mestitle").text("【库龄报表】");
    </script>

    <div class="panel-body">
        <div class="col-sm-12">
            <table class="tblCondition">
                <tr>
                    <td>工厂：</td>
                    <td>
                        <asp:DropDownList ID="ddl_comp" runat="server" class="form-control input-s-sm ">
                            <asp:ListItem Value="">ALL</asp:ListItem>
                            <asp:ListItem Value="100">上海工厂</asp:ListItem>
                            <asp:ListItem Value="200">昆山工厂</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td>物料号：</td>
                    <td>
                        <asp:TextBox ID="txt_tr_part_start" class="form-control" runat="server" Width="150px"></asp:TextBox>
                    </td>
                    <%--<td>至</td>
                    <td>
                        <asp:TextBox ID="txt_tr_part_end" class="form-control" runat="server" Width="150px"></asp:TextBox>
                    </td>--%>
                    <td>
                        <asp:Button ID="Bt_select" runat="server" Text="查询" class="btn btn-large btn-primary" Width="100px" OnClick="Bt_select_Click" />
                    </td>
                </tr>
            </table>
        </div>
    </div>

    <div>
        <table>
            <tr>
                <td>
                    <dx:ASPxGridView ID="gv_tr_list" runat="server" KeyFieldName="tr_part" AutoGenerateColumns="False">
                        <SettingsBehavior AllowDragDrop="TRUE" AllowFocusedRow="True" AllowSelectByRowClick="True" ColumnResizeMode="Control" />
                        <SettingsPager PageSize="1000">
                        </SettingsPager>
                        <Settings ShowFilterRow="True" ShowFilterRowMenu="True"
                            ShowFilterRowMenuLikeItem="True" ShowGroupPanel="True"
                            ShowFooter="True" />
                        <SettingsSearchPanel Visible="True" />

                        <Settings ShowFilterRow="True" ShowFilterRowMenu="True"
                            ShowFilterRowMenuLikeItem="True" ShowGroupPanel="True"
                            ShowFooter="True" ShowGroupedColumns="True"></Settings>

                        <SettingsBehavior ColumnResizeMode="Control" AllowFocusedRow="True"
                            AllowSelectByRowClick="True" AutoExpandAllGroups="True"
                            MergeGroupsMode="Always" SortMode="Value"></SettingsBehavior>

                        <SettingsSearchPanel Visible="True"></SettingsSearchPanel>

                        <SettingsFilterControl AllowHierarchicalColumns="True"></SettingsFilterControl>
                        <Columns></Columns>
                        <Styles>
                            <Header BackColor="#99CCFF"></Header>
                            <FocusedRow BackColor="#99CCFF" ForeColor="#0000CC"></FocusedRow>
                            <Footer HorizontalAlign="Right"></Footer>
                        </Styles>
                    </dx:ASPxGridView>
                </td>
            </tr>
        </table>
    </div>

</asp:Content>

