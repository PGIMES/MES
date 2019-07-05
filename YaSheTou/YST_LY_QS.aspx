<%@ Page Title="【压射头领用统计】" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="YST_LY_QS.aspx.cs" Inherits="YaSheTou_YST_LY_QS" %>

<%@ Register Assembly="DevExpress.XtraCharts.v17.2.Web, Version=17.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraCharts.Web" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.v17.2, Version=17.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <script type="text/javascript">
        $("#mestitle").text("【压射头领用统计】");
    </script>

    <div class="col-sm-12" id="div_p" style="margin-bottom:5px"> 
        <table style="line-height:40px;">
            <tr>
                <td>&nbsp;&nbsp;直径：</td>
                <td> 
                    <asp:DropDownList ID="ddl_zj" runat="server" class="form-control input-s-sm ">
                        <asp:ListItem Value="" Text="All"></asp:ListItem>
                        <asp:ListItem Value="D50" Text="D50"></asp:ListItem>
                        <asp:ListItem Value="D60" Text="D60"></asp:ListItem>
                        <asp:ListItem Value="D70" Text="D70"></asp:ListItem>
                        <asp:ListItem Value="D80" Text="D80"></asp:ListItem>
                        <asp:ListItem Value="D90" Text="D90"></asp:ListItem>
                        <asp:ListItem Value="D100" Text="D100"></asp:ListItem>
                        <asp:ListItem Value="D110" Text="D110"></asp:ListItem>
                        <asp:ListItem Value="D120" Text="D120"></asp:ListItem>
                        <asp:ListItem Value="D130" Text="D130"></asp:ListItem>
                        <asp:ListItem Value="D140" Text="D140"></asp:ListItem>
                        <asp:ListItem Value="D150" Text="D150"></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>&nbsp;&nbsp;物料号:</td>
                <td> 
                    <asp:TextBox ID="txt_wlh"  runat="server"  class="form-control input-s-sm " />
                </td>
                <td>&nbsp;&nbsp;
                    <asp:Button ID="Bt_select" runat="server" Text="查询" class="btn btn-large btn-primary" Width="100px"  OnClick="Bt_select_Click"/>   
                </td>
            </tr>                          
        </table>
    </div>

    <div class=" panel panel-info col-md-6 ">
        <div class="panel panel-heading">
            <asp:Label ID="Label1" runat="server" Text="按日统计压射头领用金额"></asp:Label>
        </div>
        <div class="panel panel-body" style="  overflow:scroll;height:500px;">
            <div style="float: left">
                <dx:WebChartControl ID="ChartA" runat="server" CrosshairEnabled="True" Height="300px" Width="700px">
                </dx:WebChartControl>
                <dx:ASPxGridView ID="gv_tr_list_A" runat="server">
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
        </div>
    </div>
    <div class=" panel panel-info col-md-6 ">
        <div class="panel panel-heading">
            <asp:Label ID="Label2" runat="server" Text="按周统计压射头领用金额"></asp:Label>
        </div>
        <div class="panel panel-body" style="  overflow:scroll; height:500px;">
            <div style="float: left">
                <dx:WebChartControl ID="ChartB" runat="server" CrosshairEnabled="True" Height="300px" Width="700px">
                </dx:WebChartControl>
                <dx:ASPxGridView ID="gv_tr_list_B" runat="server">
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
        </div>
    </div>

    <div class=" panel panel-info col-md-6 ">
        <div class="panel panel-heading">
            <asp:Label ID="Label3" runat="server" Text="按月统计压射头领用金额"></asp:Label>
        </div>
        <div class="panel panel-body" style="  overflow:scroll;height:500px;">
            <div style="float: left">
                <dx:WebChartControl ID="ChartC" runat="server" CrosshairEnabled="True" Height="300px" Width="700px">
                </dx:WebChartControl>
                <dx:ASPxGridView ID="gv_tr_list_C" runat="server">
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
        </div>
    </div>
    <div class=" panel panel-info col-md-6 ">
        <div class="panel panel-heading">
            <asp:Label ID="Label4" runat="server" Text="按年统计压射头领用金额"></asp:Label>
        </div>
        <div class="panel panel-body" style="  overflow:scroll; height:500px;">
            <div style="float: left">
                <dx:WebChartControl ID="ChartD" runat="server" CrosshairEnabled="True" Height="300px" Width="700px">
                </dx:WebChartControl>
                <dx:ASPxGridView ID="gv_tr_list_D" runat="server">
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
        </div>
    </div>


</asp:Content>

