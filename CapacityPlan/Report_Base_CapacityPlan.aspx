<%@ Page Title="【人员&产能核查】" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Report_Base_CapacityPlan.aspx.cs" Inherits="CapacityPlan_Report_Base_CapacityPlan" %>

<%@ Register Assembly="DevExpress.Web.v17.2, Version=17.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <script src="/Content/js/jquery.min.js" type="text/javascript"></script>
    <script src="/Content/js/layer/layer.js"></script>

    <div class="panel-body">
        <div class="col-sm-12">
            <table class="tblCondition">
                <tr>
                    <td>类别：</td>
                    <td>
                        <asp:DropDownList ID="ddl_type" runat="server" class="form-control input-s-sm ">
                            <asp:ListItem Value="emp">人员</asp:ListItem>
                            <asp:ListItem Value="cap">产能</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td>工厂：</td>
                    <td>
                        <asp:DropDownList ID="ddl_comp" runat="server" class="form-control input-s-sm ">
                            <asp:ListItem Value="">ALL</asp:ListItem>
                            <asp:ListItem Value="100">上海工厂</asp:ListItem>
                            <asp:ListItem Value="200">昆山工厂</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td>工序：</td>
                    <td>
                        <asp:TextBox ID="txt_op" class="form-control" runat="server" Width="150px"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Button ID="Bt_select" runat="server" Text="查询" class="btn btn-large btn-primary" Width="100px" OnClick="Bt_select_Click" />
                        <asp:Button ID="btn_export" runat="server" Text="导出Excel"  class="btn btn-primary" Width="100px" OnClick="btn_export_Click" />
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
                        <dx:ASPxGridView ID="gv" runat="server" KeyFieldName="" AutoGenerateColumns="False" Width="1750px" OnPageIndexChanged="gv_PageIndexChanged" OnHtmlDataCellPrepared="gv_HtmlDataCellPrepared">
                            <SettingsPager PageSize="1000" ></SettingsPager>
                            <Settings ShowFilterRow="True" ShowGroupPanel="false" ShowFilterRowMenu="True" ShowFilterRowMenuLikeItem="True" AutoFilterCondition="Contains"  />
                            <SettingsBehavior AllowFocusedRow="True" ColumnResizeMode="Control" />
                            <Columns></Columns>
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
    </div>

</asp:Content>

