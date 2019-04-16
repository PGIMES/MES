<%@ Page Title="【库龄分析】" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Qad_Report_tr_hist_new_query.aspx.cs" Inherits="Wuliu_Qad_Report_tr_hist_new_query" %>

<%@ Register Assembly="DevExpress.XtraCharts.v17.2.Web, Version=17.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraCharts.Web" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.v17.2, Version=17.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<%@ Register assembly="DevExpress.XtraCharts.v17.2, Version=17.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.XtraCharts" tagprefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

    <script src="../Content/js/jquery.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#mestitle").text("【库龄分析】");

            $("#div_p select[id*='ddl_comp']").on('change', function () {
                $("#div_p input[id*='txt_site']").val($(this).val());
            });

        });

    </script>

    <div class="col-sm-12" id="div_p" style="margin-bottom:5px"> 
        <table style="line-height:40px;">
            <tr>
                <td id="td_year">&nbsp;&nbsp;年份：</td>
                <td>
                    <asp:DropDownList ID="ddl_year" runat="server" class="form-control input-s-sm ">
                        <asp:ListItem Value="2018">2018</asp:ListItem>
                        <asp:ListItem Value="2019">2019</asp:ListItem>
                        <asp:ListItem Value="2020">2020</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td id="td_month">&nbsp;&nbsp;月份：</td>
                <td>
                    <asp:DropDownList ID="ddl_month" runat="server" class="form-control input-s-sm ">
                        <asp:ListItem Value="01">1</asp:ListItem><asp:ListItem Value="02">2</asp:ListItem><asp:ListItem Value="03">3</asp:ListItem>   
                        <asp:ListItem Value="04">4</asp:ListItem><asp:ListItem Value="05">5</asp:ListItem><asp:ListItem Value="06">6</asp:ListItem>
                        <asp:ListItem Value="07">7</asp:ListItem><asp:ListItem Value="08">8</asp:ListItem><asp:ListItem Value="09">9</asp:ListItem>
                        <asp:ListItem Value="10">10</asp:ListItem><asp:ListItem Value="11">12</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>&nbsp;&nbsp;域：</td>
                <td>
                    <asp:DropDownList ID="ddl_comp" runat="server" class="form-control input-s-sm ">
                        <asp:ListItem Value="100">100</asp:ListItem>
                        <asp:ListItem Value="200">200</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>&nbsp;&nbsp;地点：</td>
                <td>
                    <asp:TextBox ID="txt_site" class="form-control" runat="server" Width="100px" Text="100"></asp:TextBox>
                </td>                
                <td>&nbsp;&nbsp;物料编码：</td>
                <td>
                    <asp:TextBox ID="txt_tr_part_start" class="form-control" runat="server" Width="150px"></asp:TextBox>
                </td>
                <td>&nbsp;&nbsp;
                    <asp:Button ID="Bt_select" runat="server" Text="查询" class="btn btn-large btn-primary" Width="100px" OnClick="Bt_select_Click" />
                </td>
            </tr>
        </table>
    </div>
   <div>
       <div class=" panel panel-info col-md-12 ">
            <div class="panel panel-heading">
                <asp:Label ID="lblMstMonth" runat="server" Text="库存库龄分析"></asp:Label>
            </div>
            <div class="panel panel-body" style="overflow:scroll">
                <dx:ASPxGridView ID="gv_tr_list" runat="server" KeyFieldName="tyepedesc" AutoGenerateColumns="False" Width="810px"
                     OnHtmlRowCreated="gv_tr_list_HtmlRowCreated">
                    <SettingsBehavior AllowFocusedRow="false" AllowSelectByRowClick="false" SortMode="Value" />
                    <SettingsPager PageSize="1000"></SettingsPager>
                    <SettingsFilterControl AllowHierarchicalColumns="True"></SettingsFilterControl>
                    <Columns>
                        <dx:GridViewDataTextColumn Caption="10以内金额" FieldName="amount1" Width="90px">
                            <PropertiesTextEdit DisplayFormatString="{0:N0}"></PropertiesTextEdit>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="10-20金额" FieldName="amount2" Width="90px">
                            <PropertiesTextEdit DisplayFormatString="{0:N0}"></PropertiesTextEdit>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="20-30金额" FieldName="amount3" Width="90px">
                            <PropertiesTextEdit DisplayFormatString="{0:N0}"></PropertiesTextEdit>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="30-60金额" FieldName="amount4" Width="90px">
                            <PropertiesTextEdit DisplayFormatString="{0:N0}"></PropertiesTextEdit>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="60-90金额" FieldName="amount5" Width="90px">
                            <PropertiesTextEdit DisplayFormatString="{0:N0}"></PropertiesTextEdit>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="90-180金额" FieldName="amount6" Width="90px">
                            <PropertiesTextEdit DisplayFormatString="{0:N0}"></PropertiesTextEdit>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="180-360金额" FieldName="amount7" Width="90px">
                            <PropertiesTextEdit DisplayFormatString="{0:N0}"></PropertiesTextEdit>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="360以上金额" FieldName="amount8"  Width="90px">
                            <PropertiesTextEdit DisplayFormatString="{0:N0}"></PropertiesTextEdit>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="库存金额" FieldName="ld_qty_oh_amount" Width="90px">
                            <PropertiesTextEdit DisplayFormatString="{0:N0}"></PropertiesTextEdit>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="tyepedesc" FieldName="tyepedesc" VisibleIndex="99"
                                HeaderStyle-CssClass="hidden" CellStyle-CssClass="hidden" FooterCellStyle-CssClass="hidden"></dx:GridViewDataTextColumn>
                    </Columns>
                    <Styles>
                        <Header BackColor="#99CCFF"></Header>
                        <FocusedRow BackColor="#99CCFF" ForeColor="#0000CC"></FocusedRow>
                        <Footer HorizontalAlign="Right"></Footer>
                    </Styles>
                </dx:ASPxGridView>

                <dx:WebChartControl ID="ChartA" runat="server" CrosshairEnabled="True" Height="300px" Width="500px" Visible="false">
                </dx:WebChartControl>

                <dx:WebChartControl ID="ChartA_1" runat="server" CrosshairEnabled="True" Height="300px" Width="810px">
                </dx:WebChartControl>
            </div>
        </div>

       <div class=" panel panel-info col-md-6 ">
            <div class="panel panel-heading">
                <asp:Label ID="Label1" runat="server" Text="库龄30-180天趋势图"></asp:Label>
            </div>
            <div class="panel panel-body" style="  overflow:scroll">
                <div style="float: left">
                    <dx:ASPxGridView ID="gv_tr_list_2" runat="server" OnHtmlRowCreated="gv_tr_list_2_HtmlRowCreated">
                        <SettingsBehavior AllowFocusedRow="false" AllowSelectByRowClick="false" SortMode="Value"/>
                        <SettingsPager PageSize="1000"></SettingsPager>
                        <Columns></Columns>
                        <Styles>
                            <Header BackColor="#99CCFF"></Header>
                            <FocusedRow BackColor="#99CCFF" ForeColor="#0000CC"></FocusedRow>
                            <Footer HorizontalAlign="Right"></Footer>
                        </Styles>
                    </dx:ASPxGridView>
                    <dx:WebChartControl ID="ChartB" runat="server" CrosshairEnabled="True" Height="300px" Width="500px">
                    </dx:WebChartControl>
                </div>
            </div>
       </div>
       <div class=" panel panel-info col-md-6 ">
            <div class="panel panel-heading">
                <asp:Label ID="Label2" runat="server" Text="库龄30-180天库龄分析"></asp:Label>
                <button id="btn_export" type="button" class="btn btn-primary btn-large" runat="server" onserverclick="btn_export_ServerClick" style="padding:0px 0px">
                    <i class="fa fa-download fa-fw"></i>&nbsp;导出清单
                </button>
            </div>
            <div class="panel panel-body" style="  overflow:scroll">
                <div style="float: left">
                    <dx:ASPxGridView ID="gv_tr_list_3" runat="server" OnHtmlRowCreated="gv_tr_list_3_HtmlRowCreated">
                        <SettingsBehavior AllowFocusedRow="false" AllowSelectByRowClick="false" SortMode="Value"/>
                        <SettingsPager PageSize="1000"></SettingsPager>
                        <Columns></Columns>
                        <Styles>
                            <Header BackColor="#99CCFF"></Header>
                            <FocusedRow BackColor="#99CCFF" ForeColor="#0000CC"></FocusedRow>
                            <Footer HorizontalAlign="Right"></Footer>
                        </Styles>
                    </dx:ASPxGridView>
                    <dx:WebChartControl ID="ChartC" runat="server" CrosshairEnabled="True" Height="300px" Width="500px">
                    </dx:WebChartControl>
                </div>
            </div>
        </div>

       <div class=" panel panel-info col-md-6 ">
            <div class="panel panel-heading">
                <asp:Label ID="Label3" runat="server" Text="超180天趋势图"></asp:Label>
            </div>
            <div class="panel panel-body" style="  overflow:scroll">
                <div style="float: left">
                    <dx:ASPxGridView ID="gv_tr_list_4" runat="server">
                        <SettingsBehavior AllowFocusedRow="false" AllowSelectByRowClick="false" SortMode="Value"/>
                        <SettingsPager PageSize="1000"></SettingsPager>
                        <Columns></Columns>
                        <Styles>
                            <Header BackColor="#99CCFF"></Header>
                            <FocusedRow BackColor="#99CCFF" ForeColor="#0000CC"></FocusedRow>
                            <Footer HorizontalAlign="Right"></Footer>
                        </Styles>
                    </dx:ASPxGridView>
                    <dx:WebChartControl ID="ChartD" runat="server" CrosshairEnabled="True" Height="300px" Width="500px">
                    </dx:WebChartControl>
                </div>
            </div>
       </div>
       <div class=" panel panel-info col-md-6 ">
            <div class="panel panel-heading">
                <asp:Label ID="Label4" runat="server" Text="超180天库龄分析"></asp:Label>
                <button id="btn_export2" type="button" class="btn btn-primary btn-large" runat="server" onserverclick="btn_export2_ServerClick" style="padding:0px 0px">
                    <i class="fa fa-download fa-fw"></i>&nbsp;导出清单
                </button>
            </div>
            <div class="panel panel-body" style="  overflow:scroll">
                <div style="float: left">
                    <dx:ASPxGridView ID="gv_tr_list_5" runat="server" OnHtmlRowCreated="gv_tr_list_5_HtmlRowCreated">
                        <SettingsBehavior AllowFocusedRow="false" AllowSelectByRowClick="false" SortMode="Value"/>
                        <SettingsPager PageSize="1000"></SettingsPager>
                        <Columns></Columns>
                        <Styles>
                            <Header BackColor="#99CCFF"></Header>
                            <FocusedRow BackColor="#99CCFF" ForeColor="#0000CC"></FocusedRow>
                            <Footer HorizontalAlign="Right"></Footer>
                        </Styles>
                    </dx:ASPxGridView>
                    <dx:WebChartControl ID="ChartE" runat="server" CrosshairEnabled="True" Height="300px" Width="500px">
                    </dx:WebChartControl>
                </div>
            </div>
        </div>
   </div>

</asp:Content>

