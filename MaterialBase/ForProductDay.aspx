<%@ Page Title="【采购日程(刀具)查询】" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="ForProductDay.aspx.cs" Inherits="MaterialBase_ForProductDay" %>

<%@ Register Assembly="DevExpress.Web.v17.2, Version=17.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <script src="../Content/js/jquery.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        $("#mestitle").text("【采购日程(刀具)查询】");
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
                    <td>是否建立日程：</td>
                    <td>
                        <asp:DropDownList ID="ddl_isSchedule" runat="server" class="form-control input-s-sm ">
                            <asp:ListItem Value="">ALL</asp:ListItem>
                            <asp:ListItem Value="是">是</asp:ListItem>
                            <asp:ListItem Value="否" Selected="True">否</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:Button ID="Bt_select" runat="server" Text="查询" class="btn btn-large btn-primary" Width="100px" OnClick="Bt_select_Click" />
                    </td>
                </tr>
            </table>
        </div>
    </div>

    <div class="panel-body">
    <div class="col-sm-12">
        <table>
            <tr>
                <td>
                    <dx:ASPxGridView ID="gv_pt" runat="server" KeyFieldName="wlh" AutoGenerateColumns="False" Width="1230px" OnHtmlRowPrepared="gv_pt_HtmlRowPrepared" OnHtmlDataCellPrepared="gv_pt_HtmlDataCellPrepared" OnPageIndexChanged="gv_pt_PageIndexChanged">
                        <SettingsBehavior AllowDragDrop="TRUE" AllowFocusedRow="True" AllowSelectByRowClick="True" ColumnResizeMode="Control" AutoExpandAllGroups="True" MergeGroupsMode="Always" />
                        <SettingsPager PageSize="1000" />
                        <SettingsSearchPanel Visible="True" />
                        <Settings ShowFilterRow="True" ShowGroupPanel="false" ShowFilterRowMenu="True" ShowFilterRowMenuLikeItem="True" AutoFilterCondition="Contains"  />
                        <SettingsFilterControl AllowHierarchicalColumns="True" />
                        <Columns>
                            <dx:GridViewDataTextColumn Caption="序号" FieldName="" VisibleIndex="1" Width="40px"></dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="工厂" FieldName="domainname" VisibleIndex="1" Width="80px"></dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="物料号" FieldName="wlh" VisibleIndex="1" Width="100px"></dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="物料名称" FieldName="wlmc" VisibleIndex="2" Width="150px"></dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="刀具类型" FieldName="djlx" VisibleIndex="2" Width="80px"></dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="物料状态" FieldName="pt_status" VisibleIndex="2" Width="70px"></dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="安全库存" FieldName="aqkc" VisibleIndex="3" Width="80px"></dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="用于产品" FieldName="pgi_no" VisibleIndex="4" Width="120px"></dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="用于产品Sum" FieldName="pgi_no_sum" VisibleIndex="5" Width="200px"></dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="PPAP日期" FieldName="ppap_date2" VisibleIndex="6" Width="100px"></dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="剩余天数" FieldName="sydays" VisibleIndex="7" Width="80px"></dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="是否需要<br />建立日程" FieldName="isNeedSchedule" VisibleIndex="8" Width="80px"></dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="是否建立日程" FieldName="isSchedule" VisibleIndex="9" Width="80px"></dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="purchase_days" FieldName="purchase_days" VisibleIndex="10"
                                 HeaderStyle-CssClass="hidden" CellStyle-CssClass="hidden" FooterCellStyle-CssClass="hidden"></dx:GridViewDataTextColumn>
                        </Columns>
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
    </div>
</asp:Content>

