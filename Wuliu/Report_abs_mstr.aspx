<%@ Page Title="【货运单浏览】" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Report_abs_mstr.aspx.cs" Inherits="Wuliu_Report_abs_mstr" %>

<%@ Register Assembly="DevExpress.Web.v17.2, Version=17.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <script type="text/javascript" src="/Content/js/jquery.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#mestitle").text("【货运单浏览】");
             setHeight();

             $(window).resize(function () {
                 setHeight();
             });
                 
        });

        function setHeight() {
            $("div[class=dxgvCSD]").css("height", ($(window).height() - $("#div_p").height() - 150) + "px");
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

    <div class="col-md-12" id="div_p"  style="margin-bottom:5px">
        <table style=" border-collapse: collapse;">
            <tr>
                <td style="width:30px;">域</td>
                <td style="width:125px;">
                    <asp:DropDownList ID="ddl_domain" runat="server" class="form-control input-s-md " Width="120px">
                        <asp:ListItem Value="200">200</asp:ListItem>
                        <asp:ListItem Value="100">100</asp:ListItem>
                    </asp:DropDownList>
                </td>     
                <td id="td_btn">  
                    <button id="btn_search" type="button" class="btn btn-primary btn-large" runat="server" onserverclick="btn_search_Click"><i class="fa fa-search fa-fw"></i>&nbsp;查询</button>  
                    <button id="btn_export" type="button" class="btn btn-primary btn-large" runat="server" onserverclick="btn_export_Click"><i class="fa fa-download fa-fw"></i>&nbsp;导出</button>
                </td>
            </tr>                      
        </table>                   
    </div>
    
    <div class="col-sm-12">
        <table>
            <tr>
                <td>
                    <dx:ASPxGridView ID="gv" runat="server" KeyFieldName="id" AutoGenerateColumns="False" Width="1580px" OnPageIndexChanged="gv_PageIndexChanged"  ClientInstanceName="grid">
                        <ClientSideEvents EndCallback="function(s, e) {setHeight();}"  />
                        <SettingsPager PageSize="100" ></SettingsPager>
                        <Settings ShowFilterRow="True" ShowGroupPanel="false" ShowFilterRowMenu="True" ShowFilterRowMenuLikeItem="True" AutoFilterCondition="Contains" 
                            VerticalScrollBarMode="Visible" VerticalScrollBarStyle="Standard" VerticalScrollableHeight="600"  />
                        <SettingsBehavior AllowFocusedRow="True" ColumnResizeMode="Control" />
                        <Columns>                        
                            <dx:GridViewDataTextColumn Caption="销售货运单号" FieldName="abs_par_id" Width="140px" VisibleIndex="1" ></dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="货运单号" FieldName="abs_par_id_2" Width="130px" VisibleIndex="2"></dx:GridViewDataTextColumn>                             
                            <dx:GridViewDataDateColumn Caption="出库日期" FieldName="abs_shp_date_2" Width="80px" VisibleIndex="3">
                                <PropertiesDateEdit DisplayFormatString="yyyy/MM/dd"></PropertiesDateEdit>
                            </dx:GridViewDataDateColumn>                          
                            <dx:GridViewDataDateColumn Caption="发货日期" FieldName="abs_shp_date" Width="80px" VisibleIndex="4">
                                <PropertiesDateEdit DisplayFormatString="yyyy/MM/dd"></PropertiesDateEdit>
                            </dx:GridViewDataDateColumn> 
                            <dx:GridViewDataTextColumn Caption="毛重" FieldName="abs_gwt" Width="70px" VisibleIndex="5">
                                <PropertiesTextEdit DisplayFormatString="{0:N2}"></PropertiesTextEdit>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="净重" FieldName="abs_nwt" Width="70px" VisibleIndex="6">
                                <PropertiesTextEdit DisplayFormatString="{0:N2}"></PropertiesTextEdit>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="订单号" FieldName="abs_order" Width="70px" VisibleIndex="7"></dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="发票号" FieldName="abs_inv_nbr" Width="180px" VisibleIndex="8"></dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="物料号" FieldName="abs_item" Width="80px" VisibleIndex="9"></dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="客户物料号" FieldName="pt_desc1" Width="150px" VisibleIndex="9"></dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="发货数量" FieldName="abs_qty" Width="80px" VisibleIndex="10">
                                <PropertiesTextEdit DisplayFormatString="{0:N1}"></PropertiesTextEdit>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="地点" FieldName="abs_site" Width="60px" VisibleIndex="11"></dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="库位" FieldName="abs_loc" Width="60px" VisibleIndex="12"></dx:GridViewDataTextColumn>
                            <%--<dx:GridViewDataTextColumn Caption="域" FieldName="abs_domain" Width="50px" VisibleIndex="13"></dx:GridViewDataTextColumn>--%>
                            <dx:GridViewDataTextColumn Caption="shipfrom" FieldName="abs_shipfrom" Width="80px" VisibleIndex="14"></dx:GridViewDataTextColumn>
                            <%--<dx:GridViewDataTextColumn Caption="shipto" FieldName="abs_shipto" Width="110px" VisibleIndex="15"></dx:GridViewDataTextColumn>--%>
                            <dx:GridViewDataTextColumn Caption="客户名称" FieldName="DebtorShipToName" Width="250px" VisibleIndex="15"></dx:GridViewDataTextColumn>
                        </Columns>
                        <Styles>
                            <Header BackColor="#99CCFF"></Header>
                            <FocusedRow BackColor="#99CCFF" ForeColor="#0000CC"></FocusedRow>
                            <Footer HorizontalAlign="Right"></Footer>
                        </Styles>
                    </dx:ASPxGridView>
                    <%--<dx:ASPxGridViewExporter ID="ASPxGridViewExporter1" runat="server" GridViewID="gv">
                    </dx:ASPxGridViewExporter>--%>
                </td>
            </tr>
        </table>
    </div>

</asp:Content>

