<%@ Page Title="【清关税费暂估报表】" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Fin_QGSF_Report.aspx.cs" Inherits="Fin_Fin_QGSF_Report" %>

<%@ Register Assembly="DevExpress.Web.v17.2, Version=17.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <script src="/Content/js/jquery.min.js"  type="text/javascript"></script>
    <script src="/Content/js/bootstrap.min.js"></script>
    <script type="text/javascript">
        <%--var UserId = '<%=UserId%>';
        var UserName = '<%=UserName%>';
        var DeptName = '<%=DeptName%>';--%>

        $(document).ready(function () {
            $("#mestitle").text("【清关税费暂估报表】");

            setHeight();
            $(window).resize(function () {
                setHeight();
            });

            $('#btn_QGRate').click(function () {
                window.open('/Fin/Fin_Base_QGSF.aspx');
            });


        });

        function setHeight() {
            $("div[class=dxgvCSD]").css("height", ($(window).height() - $("#div_p").height() - 250) + "px");

            $("#MainContent_GV_PART").css("width", ($(window).width() - 10) + "px")
            $("div[class=dxgvCSD]").css("width", ($(window).width() - 10) + "px");
        }
        	
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

    <div class="panel-body" id="div_p">
        <div class="col-sm-12">
            <table style="line-height:40px">
                <tr>                    
                    <td style="text-align:right;">域：</td>
                    <td>
                        <asp:DropDownList ID="ddl_comp" runat="server" class="form-control input-s-md ">
                            <asp:ListItem Text="200" Value="200"></asp:ListItem>
                            <asp:ListItem Text="100" Value="100"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td style="text-align:right;">&nbsp;&nbsp;年份：</td>
                    <td colspan="3">
                        <asp:DropDownList ID="ddl_year" runat="server" class="form-control input-s-sm ">
                            <%--<asp:ListItem Value="2018">2018</asp:ListItem>--%>
                            <asp:ListItem Value="2019">2019</asp:ListItem>
                            <asp:ListItem Value="2020">2020</asp:ListItem>
                        </asp:DropDownList>
                    </td>                   
                    <td style="text-align:right;">&nbsp;&nbsp;月份：</td>
                    <td>
                        <asp:DropDownList ID="ddl_month" runat="server" class="form-control input-s-sm ">
                            <asp:ListItem Value="01">1</asp:ListItem><asp:ListItem Value="02">2</asp:ListItem><asp:ListItem Value="03">3</asp:ListItem>   
                            <asp:ListItem Value="04">4</asp:ListItem><asp:ListItem Value="05">5</asp:ListItem><asp:ListItem Value="06">6</asp:ListItem>
                            <asp:ListItem Value="07">7</asp:ListItem><asp:ListItem Value="08">8</asp:ListItem><asp:ListItem Value="09">9</asp:ListItem>
                            <asp:ListItem Value="10">10</asp:ListItem><asp:ListItem Value="11">12</asp:ListItem>
                        </asp:DropDownList>
                    </td>  
                    <td> 
                        &nbsp;
                        <asp:Button ID="Bt_select" runat="server" Text="查询" class="btn btn-large btn-primary" OnClick="Bt_select_Click" Width="70px" /> 
                        &nbsp;
                        <asp:Button ID="Bt_Export" runat="server" class="btn btn-large btn-primary" OnClick="Bt_Export_Click" Text="导出" Width="70px" /> 
                        &nbsp;
                        <button id="btn_QGRate" type="button" class="btn btn-primary btn-large"><i class="fa fa-edit fa-fw"></i>&nbsp;产品税率</button>
                    </td> 
                </tr>
            </table>
        </div>
    </div>
    <div runat="server" id="DIV1" style="margin-left: 5px; margin-right: 5px; margin-bottom: 10px">
        <table>
            <tr>
                <td>
                    <dx:ASPxGridView ID="GV_PART" ClientInstanceName="grid" runat="server" KeyFieldName="tr_trnbr" AutoGenerateColumns="False"  
                             OnPageIndexChanged="GV_PART_PageIndexChanged" Width="1000px"><%--3030--%>
                        <ClientSideEvents EndCallback="function(s, e) { setHeight(); }" />
                        <SettingsBehavior AllowDragDrop="TRUE" AllowFocusedRow="false" AllowSelectByRowClick="false" ColumnResizeMode="Control" AutoExpandAllGroups="true" MergeGroupsMode="Always" SortMode="Value" />
                        <SettingsPager PageSize="100"></SettingsPager>
                        <Settings ShowFilterRow="True" ShowFilterRowMenu="True"   VerticalScrollBarMode="Visible" VerticalScrollBarStyle="Standard" VerticalScrollableHeight="500"
                                 ShowFilterRowMenuLikeItem="True"  ShowFooter="True"  HorizontalScrollBarMode="Auto" />
                        <Columns>
                            <dx:GridViewCommandColumn   ShowClearFilterButton="true" ShowSelectCheckbox="true" Name="Sel" Width="40" VisibleIndex="0"  SelectAllCheckboxMode="Page"  >
                            </dx:GridViewCommandColumn> 
                            <dx:GridViewDataTextColumn Caption="物料号" FieldName="tr_part" Width="80px" VisibleIndex="1" />
                            <dx:GridViewDataTextColumn Caption="状态" FieldName="pt_status" Width="50px" VisibleIndex="2" />
                            <dx:GridViewDataTextColumn Caption="物料描述" FieldName="pt_desc" Width="250px" VisibleIndex="3" />
                            <dx:GridViewDataTextColumn Caption="事务类型" FieldName="tr_type" Width="60px" VisibleIndex="4" />
                            <dx:GridViewDataDateColumn Caption="生效日期" FieldName="tr_effdate" Width="90px" VisibleIndex="5" >
                                <PropertiesDateEdit DisplayFormatString="yyyy/MM/dd"></PropertiesDateEdit>
                            </dx:GridViewDataDateColumn>
                            <dx:GridViewDataTextColumn Caption="地点" FieldName="tr_site" Width="80px" VisibleIndex="6" />
                            <dx:GridViewDataTextColumn Caption="库位" FieldName="tr_loc" Width="70px" VisibleIndex="7" />
                            <dx:GridViewDataTextColumn Caption="订单" FieldName="tr_nbr" Width="100px" VisibleIndex="8" />
                            <dx:GridViewDataTextColumn Caption="库位数量更改" FieldName="tr_qty_loc" Width="80px" VisibleIndex="9" >                                
                                <PropertiesTextEdit DisplayFormatString="{0:N0}"></PropertiesTextEdit>
                            </dx:GridViewDataTextColumn>                            
                            <dx:GridViewDataTextColumn Caption="备注" FieldName="tr_rmks" Width="90px" VisibleIndex="10" />    
                            <dx:GridViewDataTextColumn Caption="域" FieldName="tr_domain" Width="40px" VisibleIndex="11" />
                            <dx:GridViewDataTextColumn Caption="年月" FieldName="yymm" Width="60px" VisibleIndex="12" />                             
                            <dx:GridViewDataTextColumn Caption="Price Curr" FieldName="pc_curr" Width="60px" VisibleIndex="13" />
                            <dx:GridViewDataTextColumn Caption="ex.change rate" FieldName="ExchangeRate" Width="90px" VisibleIndex="14" />
                            <dx:GridViewDataTextColumn Caption="Price in USD" FieldName="price_USD" Width="80px" VisibleIndex="15"  >
                                <PropertiesTextEdit DisplayFormatString="${0:N2}"></PropertiesTextEdit>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="Value in O.C. in USD" FieldName="TotalPrice_USD" Width="120px" VisibleIndex="16"  >
                                <PropertiesTextEdit DisplayFormatString="${0:N2}"></PropertiesTextEdit>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="Warehouse Des" FieldName="site_desc" Width="120px" VisibleIndex="17" />
                            <dx:GridViewDataTextColumn Caption="HS Code_US" FieldName="HSCode" Width="90px" VisibleIndex="18" />
                            <dx:GridViewDataTextColumn Caption="General Duty Rate" FieldName="BaseRate" Width="100px" VisibleIndex="19" >
                                <PropertiesTextEdit DisplayFormatString="{0:P1}"></PropertiesTextEdit>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="General Duty Amount" FieldName="TotalPrice_Base_USD" Width="100px" VisibleIndex="20" >
                                <PropertiesTextEdit DisplayFormatString="${0:N2}"></PropertiesTextEdit>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="301 Duty Code" FieldName="301code" Width="90px" VisibleIndex="21" />
                            <dx:GridViewDataTextColumn Caption="301 Duty Rate" FieldName="301Rate" Width="90px" VisibleIndex="22" >
                                <PropertiesTextEdit DisplayFormatString="{0:P1}"></PropertiesTextEdit>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="301 Duty Amount" FieldName="TotalPrice_QG_USD" Width="100px" VisibleIndex="23" >
                                <PropertiesTextEdit DisplayFormatString="${0:N2}"></PropertiesTextEdit>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="Total Duty" FieldName="base_qg_usd" Width="100px" VisibleIndex="24" >
                                <PropertiesTextEdit DisplayFormatString="${0:N2}"></PropertiesTextEdit>
                            </dx:GridViewDataTextColumn>

                            <%--<dx:GridViewDataTextColumn Caption="po_GroupID" FieldName="po_GroupID" VisibleIndex="98"
                                 HeaderStyle-CssClass="hidden" CellStyle-CssClass="hidden" FooterCellStyle-CssClass="hidden" FilterCellStyle-CssClass="hidden">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="rct_GroupID" FieldName="rct_GroupID" VisibleIndex="99"
                                 HeaderStyle-CssClass="hidden" CellStyle-CssClass="hidden" FooterCellStyle-CssClass="hidden" FilterCellStyle-CssClass="hidden">
                            </dx:GridViewDataTextColumn>--%>
                        </Columns>
                        <TotalSummary>
                            <dx:ASPxSummaryItem DisplayFormat="{0:N2}" FieldName="TotalPrice_USD" ShowInColumn="TotalPrice_USD" ShowInGroupFooterColumn="TotalPrice_USD" SummaryType="Sum" />
                        </TotalSummary>
                        <Styles>
                            <Header BackColor="#99CCFF"></Header>
                            <FocusedRow BackColor="#99CCFF" ForeColor="#0000CC"></FocusedRow>
                            <Footer HorizontalAlign="Right"></Footer>
                            <AlternatingRow Enabled="true" />
                        </Styles>
                    </dx:ASPxGridView>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>

