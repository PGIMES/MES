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
                        <asp:Button ID="btnimport" runat="server" Text="导出Excel"  class="btn btn-primary" Font-Size="12px" OnClick="btnimport_Click" />
                    </td>
                </tr>
            </table>
        </div>
    </div>

    <div>
        <table>
            <tr>
                <td>
                    <dx:ASPxGridView ID="gv_tr_list" runat="server" KeyFieldName="tr_part" AutoGenerateColumns="False" Width="1750px" OnPageIndexChanged="gv_tr_list_PageIndexChanged" OnHtmlDataCellPrepared="gv_tr_list_HtmlDataCellPrepared">
                        <SettingsBehavior AllowDragDrop="TRUE" AllowFocusedRow="True" AllowSelectByRowClick="True" ColumnResizeMode="Control" AutoExpandAllGroups="True" MergeGroupsMode="Always" SortMode="Value" />
                        <SettingsPager PageSize="1000"></SettingsPager>
                        <Settings ShowFilterRow="True" ShowFilterRowMenu="True" ShowFilterRowMenuLikeItem="True" ShowGroupPanel="True" ShowFooter="True" ShowGroupedColumns="True" 
                            AutoFilterCondition="Contains" />
                        <SettingsSearchPanel Visible="True" />
                        <SettingsFilterControl AllowHierarchicalColumns="True"></SettingsFilterControl>
                        <Columns>
                            <dx:GridViewDataTextColumn Caption="序号" FieldName="" Width="40px"></dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="物料编号" FieldName="tr_part" Width="80px"></dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="地点" FieldName="tr_domain" Width="60px"></dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="库位" FieldName="tr_loc" Width="60px"></dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="参考号" FieldName="tr_ref" Width="80px"></dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="价格" FieldName="sct_mtl_tl" Width="60px"></dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="10数量" FieldName="qty1" Width="60px"></dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="10金额" FieldName="amount1" Width="60px"></dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="10-20数量" FieldName="qty2" Width="70px"></dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="10-20金额" FieldName="amount2" Width="70px"></dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="20-30数量" FieldName="qty3" Width="70px"></dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="20-30金额" FieldName="amount3" Width="70px"></dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="30-60数量" FieldName="qty4" Width="70px"></dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="30-60金额" FieldName="amount4" Width="70px"></dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="60-90数量" FieldName="qty5" Width="70px"></dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="60-90金额" FieldName="amount5" Width="70px"></dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="90-180数量" FieldName="qty6" Width="90px"></dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="90-180金额" FieldName="amount6" Width="90px"></dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="180-360数量" FieldName="qty7" Width="90px"></dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="180-360金额" FieldName="amount7" Width="90px"></dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="360以上数量" FieldName="qty8" Width="90px"></dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="360以上金额" FieldName="amount8"  Width="90px"></dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="库存" FieldName="ld_qty_oh" Width="60px"></dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="库存金额" FieldName="ld_qty_oh_amount" Width="80px"></dx:GridViewDataTextColumn>
                        </Columns>
                        <Styles>
                            <Header BackColor="#99CCFF"></Header>
                            <FocusedRow BackColor="#99CCFF" ForeColor="#0000CC"></FocusedRow>
                            <Footer HorizontalAlign="Right"></Footer>
                        </Styles>
                    </dx:ASPxGridView>
                    <dx:ASPxGridViewExporter ID="ASPxGridViewExporter1" runat="server">
                    </dx:ASPxGridViewExporter>
                </td>
            </tr>
        </table>
    </div>

</asp:Content>

