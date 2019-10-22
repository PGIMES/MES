<%@ Page Title="【客户日程单异常报表】" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Report_so.aspx.cs" Inherits="Wuliu_Report_so" %>

<%@ Register Assembly="DevExpress.Web.v17.2, Version=17.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <script src="/Content/js/jquery.min.js"  type="text/javascript"></script>
    <script src="/Content/js/bootstrap.min.js"></script>
    <script src="/Content/js/layer/layer.js"></script>
    <script src="/Content/js/plugins/layer/laydate/laydate.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#mestitle").html("【客户日程单异常报表】");

            setHeight();
            $(window).resize(function () {
                setHeight();
            });


        });
        
        function setHeight() {
            $("div[class=dxgvCSD]").css("height", ($(window).height() - $("#div_p").height() - 220) + "px");

            //$("#MainContent_GV_PART").css("width", ($(window).width() - 10) + "px");
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
                            <asp:ListItem Text="All" Value=""></asp:ListItem>
                            <asp:ListItem Text="200" Value="200"></asp:ListItem>
                            <asp:ListItem Text="100" Value="100"></asp:ListItem>
                        </asp:DropDownList>
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
                    <dx:ASPxGridView ID="GV_PART" ClientInstanceName="grid" runat="server" KeyFieldName="sortby" AutoGenerateColumns="False" 
                             OnPageIndexChanged="GV_PART_PageIndexChanged" Width="1120px"><%--Width="1000px"--%>
                        <ClientSideEvents EndCallback="function(s, e) { setHeight(); }" />
                        <SettingsBehavior AllowDragDrop="TRUE" AllowFocusedRow="false" AllowSelectByRowClick="false" ColumnResizeMode="Control" AutoExpandAllGroups="true" MergeGroupsMode="Always" SortMode="Value" />
                        <SettingsPager PageSize="100"></SettingsPager>
                        <Settings ShowFilterRow="True" ShowFilterRowMenu="True"   VerticalScrollBarMode="Visible" VerticalScrollBarStyle="Standard" VerticalScrollableHeight="500"
                                 ShowFilterRowMenuLikeItem="True"  ShowFooter="True"  HorizontalScrollBarMode="Auto" />
                        <Columns>
                            <dx:GridViewDataTextColumn Caption="域" FieldName="so_domain" Width="40px" VisibleIndex="1" />
                            <dx:GridViewDataTextColumn Caption="销售订单" FieldName="so_nbr" Width="90px" VisibleIndex="2" />
                            <dx:GridViewDataTextColumn Caption="发货自" FieldName="so_site" Width="70px" VisibleIndex="3" />
                            <dx:GridViewDataTextColumn Caption="发货至" FieldName="so_ship" Width="70px" VisibleIndex="4" />
                            <dx:GridViewDataTextColumn Caption="票据开往" FieldName="so_bill" Width="70px" VisibleIndex="5" />
                            <dx:GridViewDataTextColumn Caption="物料号" FieldName="sod_part" Width="90px" VisibleIndex="6" />
                            <dx:GridViewDataTextColumn Caption="客户项目" FieldName="sod_custpart" Width="120px" VisibleIndex="7" />
                            <dx:GridViewDataTextColumn Caption="价目表" FieldName="sod_pr_list" Width="120px" VisibleIndex="8" />
                            <dx:GridViewDataTextColumn Caption="库位" FieldName="sod_loc" Width="70px" VisibleIndex="9"/>  
                            <dx:GridViewDataTextColumn Caption="客户参考号" FieldName="sod_custref" Width="120px" VisibleIndex="10" /> 
                            <dx:GridViewDataDateColumn Caption="结束有效日" FieldName="enddate" Width="90px" VisibleIndex="11" >
                                <PropertiesDateEdit DisplayFormatString="yyyy/MM/dd"></PropertiesDateEdit>
                            </dx:GridViewDataDateColumn>
                            <dx:GridViewDataTextColumn Caption="异常类别" FieldName="exctype" Width="150px" VisibleIndex="12" /> 
                        </Columns>
                        <Styles>
                            <Header BackColor="#99CCFF"></Header>
                            <FocusedRow BackColor="#99CCFF" ForeColor="#0000CC"></FocusedRow>
                            <Footer HorizontalAlign="Right"></Footer>
                            <AlternatingRow Enabled="true" />
                            <Footer ForeColor="Red" Font-Bold="true"></Footer>
                        </Styles>
                    </dx:ASPxGridView>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>

