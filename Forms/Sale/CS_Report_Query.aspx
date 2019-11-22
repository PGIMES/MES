<%@ Page Title="【客户日程单浏览】" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="CS_Report_Query.aspx.cs" Inherits="Forms_Sale_CS_Report_Query" %>

<%@ Register Assembly="DevExpress.Web.v17.2, Version=17.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <script src="/Content/js/jquery.min.js"  type="text/javascript"></script>
    <script src="/Content/js/bootstrap.min.js"></script>
    <script type="text/javascript">
        <%--var UserId = '<%=UserId%>';
        var UserName = '<%=UserName%>';
        var DeptName = '<%=DeptName%>';--%>

        $(document).ready(function () {
            $("#mestitle").text("【客户日程单浏览】");

            setHeight();
            $(window).resize(function () {
                setHeight();
            });


        });

        function setHeight() {
            $("div[class=dxgvCSD]").css("height", ($(window).height() - $("#div_p").height() - 250) + "px");

            //$("#MainContent_GV_PART").css("width", ($(window).width() - 10) + "px")
            //$("div[class=dxgvCSD]").css("width", ($(window).width() - 10) + "px");
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
                            <%--<asp:ListItem Text="All" Value=""></asp:ListItem>--%>
                            <asp:ListItem Text="200" Value="200"></asp:ListItem>
                            <asp:ListItem Text="100" Value="100"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td style="text-align:right;">&nbsp;&nbsp;物料号：</td>
                    <td colspan="3">
                        <asp:TextBox ID="txt_wlh" class="form-control input-s-md " runat="server"></asp:TextBox>
                    </td>  
                    <td> 
                        &nbsp;
                        <asp:Button ID="Bt_select" runat="server" Text="查询" class="btn btn-large btn-primary" OnClick="Bt_select_Click" Width="70px" /> 
                        &nbsp;
                        <asp:Button ID="Bt_Export" runat="server" class="btn btn-large btn-primary" OnClick="Bt_Export_Click" Text="导出" Width="70px" /> 
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
                             OnPageIndexChanged="GV_PART_PageIndexChanged" Width="1260px"><%--3030--%>
                        <ClientSideEvents EndCallback="function(s, e) { setHeight(); }" />
                        <SettingsBehavior AllowDragDrop="TRUE" AllowFocusedRow="false" AllowSelectByRowClick="false" ColumnResizeMode="Control" AutoExpandAllGroups="true" MergeGroupsMode="Always" SortMode="Value" />
                        <SettingsPager PageSize="100"></SettingsPager>
                        <Settings ShowFilterRow="True" ShowFilterRowMenu="True"   VerticalScrollBarMode="Visible" VerticalScrollBarStyle="Standard" VerticalScrollableHeight="500"
                                 ShowFilterRowMenuLikeItem="True"  ShowFooter="True"  HorizontalScrollBarMode="Auto" />
                        <Columns>
                            <dx:GridViewCommandColumn   ShowClearFilterButton="true" ShowSelectCheckbox="true" Name="Sel" Width="40" VisibleIndex="0"  SelectAllCheckboxMode="Page"  >
                            </dx:GridViewCommandColumn> 
                            <dx:GridViewDataTextColumn Caption="域" FieldName="so_domain" Width="40px" VisibleIndex="1" />
                            <dx:GridViewDataTextColumn Caption="销售订单" FieldName="so_nbr" Width="70px" VisibleIndex="2" />
                            <dx:GridViewDataTextColumn Caption="发货自" FieldName="so_site" Width="70px" VisibleIndex="3" />
                            <dx:GridViewDataTextColumn Caption="发货至" FieldName="so_ship" Width="70px" VisibleIndex="4" />
                            <dx:GridViewDataTextColumn Caption="票据开往" FieldName="so_bill" Width="60px" VisibleIndex="5" />
                            <dx:GridViewDataTextColumn Caption="行" FieldName="sod_line" Width="40px" VisibleIndex="6" />
                            <dx:GridViewDataTextColumn Caption="寄售" FieldName="sod_consignment" Width="40px" VisibleIndex="7" />
                            <dx:GridViewDataTextColumn Caption="物料号" FieldName="sod_part" Width="70px" VisibleIndex="8" />
                            <dx:GridViewDataTextColumn Caption="客户项目" FieldName="sod_custpart" Width="120px" VisibleIndex="9" />
                            <dx:GridViewDataTextColumn Caption="货币" FieldName="so_curr" Width="40px" VisibleIndex="10" />
                            <dx:GridViewDataTextColumn Caption="价目表价格" FieldName="sod_list_pr" Width="80px" VisibleIndex="11" >
                                <PropertiesTextEdit DisplayFormatString="{0:N2}"></PropertiesTextEdit>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="价目表" FieldName="sod_pr_list" Width="80px" VisibleIndex="12" />
                            <dx:GridViewDataTextColumn Caption="应纳税" FieldName="sod_taxable" Width="45px" VisibleIndex="13" />
                            <dx:GridViewDataTextColumn Caption="纳税级别" FieldName="sod_taxc" Width="65px" VisibleIndex="14" />
                            <dx:GridViewDataTextColumn Caption="库位" FieldName="sod_loc" Width="50px" VisibleIndex="15"/>  
                            <dx:GridViewDataTextColumn Caption="客户参考号" FieldName="sod_custref" Width="120px" VisibleIndex="16" /> 
                            <dx:GridViewDataDateColumn Caption="结束有效日" FieldName="enddate" Width="90px" VisibleIndex="17" >
                                <PropertiesDateEdit DisplayFormatString="yyyy/MM/dd"></PropertiesDateEdit>
                            </dx:GridViewDataDateColumn>
                            <dx:GridViewDataTextColumn Caption="模型年" FieldName="sod_modelyr" Width="50px" VisibleIndex="18" /> 
                        </Columns>
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

