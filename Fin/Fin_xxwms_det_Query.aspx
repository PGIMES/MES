<%@ Page Title="【物流仓储运费浏览】" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Fin_xxwms_det_Query.aspx.cs" Inherits="Fin_Fin_xxwms_det_Query" %>

<%@ Register Assembly="DevExpress.XtraCharts.v17.2.Web, Version=17.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraCharts.Web" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.v17.2, Version=17.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

    <script src="../Content/js/jquery.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#mestitle").text("【物流仓储运费浏览】");
        });

    </script>


    <div class="col-sm-12" id="div_p" style="margin-bottom:5px"> 
        <table style="line-height:40px;">
            <tr>
                <td id="td_year">&nbsp;&nbsp;年份：</td>
                <td>
                    <asp:DropDownList ID="ddl_year" runat="server" class="form-control input-s-sm ">
                        <asp:ListItem Value="2019">2019</asp:ListItem>
                        <asp:ListItem Value="2020">2020</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>&nbsp;&nbsp;域：</td>
                <td>
                    <asp:DropDownList ID="ddl_comp" runat="server" class="form-control input-s-sm ">
                        <asp:ListItem Value="">全部</asp:ListItem>
                        <asp:ListItem Value="100">100</asp:ListItem>
                        <asp:ListItem Value="200">200</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>&nbsp;&nbsp;
                    <asp:Button ID="Bt_select" runat="server" Text="查询" class="btn btn-large btn-primary" Width="100px" OnClick="Bt_select_Click" />
                </td>
            </tr>
        </table>
    </div>

    <div class="col-sm-12">
        <dx:WebChartControl ID="Chart" runat="server" CrosshairEnabled="True" Width="1170px" Height="300px"> 
        </dx:WebChartControl>

        <p></p>
        
        <dx:ASPxGridView ID="gv" runat="server" OnHtmlRowCreated="gv_HtmlRowCreated">
            <SettingsBehavior AllowFocusedRow="false" AllowSelectByRowClick="false" SortMode="Value"/>
            <SettingsPager PageSize="1000"></SettingsPager>
            <Columns></Columns>
            <Styles>
                <Header BackColor="#99CCFF"></Header>
                <FocusedRow BackColor="#99CCFF" ForeColor="#0000CC"></FocusedRow>
                <Footer HorizontalAlign="Right"></Footer>
            </Styles>
        </dx:ASPxGridView>

    </div>
</asp:Content>

