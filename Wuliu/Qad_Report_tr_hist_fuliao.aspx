<%@ Page Title="【辅料库龄明细报表】" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Qad_Report_tr_hist_fuliao.aspx.cs" Inherits="Wuliu_Qad_Report_tr_hist_fuliao" %>

<%@ Register Assembly="DevExpress.Web.v17.2, Version=17.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <script src="../Content/js/jquery.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#mestitle").text("【辅料库龄明细报表】");
            setHeight();

            $(window).resize(function () {
                setHeight();
            });
           
            $("#div_p select[id*='ddl_comp']").on('change', function () {
                $("#div_p input[id*='txt_site']").val($(this).val());
            });

        });

        function setHeight() {            
            $("div[class=dxgvCSD]").css("height", ($(window).height() - $("#div_p").height() - 180) + "px");
        }

        
        //function Get_Site() {
        //    var url = "/select/select_site.aspx?domain=" + $("#div_p select[id*='ddl_comp']").val();

        //    layer.open({
        //        title: '地点',
        //        type: 2,
        //        area: ['600px', '600px'],
        //        fixed: false, //不固定
        //        maxmin: true,
        //        content: url
        //    });
        //}

        //function setvalue_site(ls_site) {
        //    $("#div_p input[id*='txt_site']").val(ls_site);
        //}
       

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
                        <asp:ListItem Value="10">10</asp:ListItem><asp:ListItem Value="11">11</asp:ListItem><asp:ListItem Value="12">12</asp:ListItem>
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
                <%--<td><i class="fa fa-search" onclick="Get_Site()"></i></td>--%>
                <td>&nbsp;&nbsp;物料编码：</td>
                <td>
                    <asp:TextBox ID="txt_tr_part_start" class="form-control" runat="server" Width="150px"></asp:TextBox>
                </td>
                <td>&nbsp;&nbsp;
                    <asp:Button ID="Bt_select" runat="server" Text="查询" class="btn btn-large btn-primary" Width="100px" OnClick="Bt_select_Click" />
                    <asp:Button ID="btnimport" runat="server" Text="导出Excel"  class="btn btn-primary" Width="100px" OnClick="btnimport_Click" />
                </td>
            </tr>
        </table>
    </div>

    <div class="col-sm-12">
        <table>
            <tr>
                <td>
                    <dx:ASPxGridView ID="gv_tr_list" runat="server" KeyFieldName="tr_part" AutoGenerateColumns="False" Width="1900px" OnPageIndexChanged="gv_tr_list_PageIndexChanged" OnHtmlDataCellPrepared="gv_tr_list_HtmlDataCellPrepared">
                        <ClientSideEvents EndCallback="function(s, e) { setHeight(); }"  />
                        <SettingsBehavior AllowDragDrop="TRUE" AllowFocusedRow="True" AllowSelectByRowClick="True" ColumnResizeMode="Control" AutoExpandAllGroups="True" MergeGroupsMode="Always" SortMode="Value" />
                        <SettingsPager PageSize="1000"></SettingsPager>
                        <Settings ShowFilterRow="True" ShowFilterRowMenu="True" ShowFilterRowMenuLikeItem="True"  ShowFooter="True" ShowGroupedColumns="True" 
                            AutoFilterCondition="Contains" VerticalScrollBarMode="Visible" VerticalScrollBarStyle="Standard" VerticalScrollableHeight="600" />
                        <SettingsFilterControl AllowHierarchicalColumns="True"></SettingsFilterControl>
                        <Columns>
                            <dx:GridViewDataTextColumn Caption="序号" FieldName="" Width="40px"></dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="物料编码" FieldName="tr_part" Width="100px"></dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="物料描述" FieldName="pt_desc1" Width="150px"></dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="单位" FieldName="pt_um" Width="40px"></dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="价格" FieldName="sct_cst_tot" Width="80px"></dx:GridViewDataTextColumn>                            
                            <dx:GridViewDataTextColumn Caption="0-30数量" FieldName="qty1" Width="70px"></dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="0-30金额" FieldName="amount1" Width="70px"></dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="30-60数量" FieldName="qty2" Width="70px"></dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="30-60金额" FieldName="amount2" Width="70px"></dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="60-90数量" FieldName="qty3" Width="70px"></dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="60-90金额" FieldName="amount3" Width="70px"></dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="90-180数量" FieldName="qty4" Width="90px"></dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="90-180金额" FieldName="amount4" Width="90px"></dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="180-360数量" FieldName="qty5" Width="90px"></dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="180-360金额" FieldName="amount5" Width="90px"></dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="360-720数量" FieldName="qty6" Width="90px"></dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="360-720金额" FieldName="amount6"  Width="90px"></dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="720以上数量" FieldName="qty7" Width="90px"></dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="720以上金额" FieldName="amount7"  Width="90px"></dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="库存" FieldName="ld_qty_oh" Width="60px"></dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="库存金额" FieldName="ld_qty_oh_amount" Width="80px"></dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="零件状态" FieldName="pt_status" Width="60px"></dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="产品线" FieldName="pt_prod_line" Width="50px">
                                <%--<Settings AllowAutoFilterTextInputTimer="False" />--%>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="刀具类型" FieldName="pt_dsgn_grp" Width="80px"></dx:GridViewDataTextColumn>
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

</asp:Content>

