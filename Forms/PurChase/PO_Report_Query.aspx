<%@ Page Title="采购单查询" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="PO_Report_Query.aspx.cs" Inherits="Forms_PurChase_PO_Report_Query" %>

<%@ Register Assembly="DevExpress.Web.v17.2, Version=17.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <script src="/Content/js/jquery.min.js"  type="text/javascript"></script>
    <script src="/Content/js/bootstrap.min.js"></script>
    <script src="/Content/js/layer/layer.js"></script>
    <script src="/Content/js/plugins/layer/laydate/laydate.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#mestitle").text("【采购单查询】");

            setHeight();
            $(window).resize(function () {
                setHeight();
            });
        });
        function setHeight() {
            $("div[class=dxgvCSD]").css("height", ($(window).height() - $("#div_p").height() - 300) + "px");
        }

        function show_his(PoNo) {
           
            var url = "/Forms/PurChase/PO_Print_his.aspx?PoNo=" + PoNo;

            layer.open({
                title: '打印记录',
                closeBtn: 2,
                type: 2,
                area: ['500px', '500px'],
                fixed: false, //不固定
                maxmin: true, //开启最大化最小化按钮
                content: url
            });
        }
        	
    </script>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
     
    <div class="panel-body" id="div_p">
        <div class="col-sm-12">
            <table>
                <tr>
                    <td>采购类别:</td>
                    <td>
                        <asp:DropDownList ID="drop_type" runat="server" class="form-control input-s-sm ">
                            <asp:ListItem>PO</asp:ListItem>
                            <asp:ListItem>合同</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td>创建日期;</td>
                    <td >
                        <asp:TextBox ID="txtDateFrom" class="form-control" onclick="laydate()" runat="server" Width="100px"></asp:TextBox>
                    </td>
                    <td>~</td>
                    <td >
                        <asp:TextBox ID="txtDateTo" class="form-control" onclick="laydate()" runat="server" Width="100px"></asp:TextBox>
                    </td>            
                    <td> 
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="Bt_select" runat="server" Text="查询" class="btn btn-large btn-primary" OnClick="Button1_Click" Width="100px" /> 
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="Bt_Export" runat="server" class="btn btn-large btn-primary" OnClick="Bt_Export_Click" Text="导出" Width="100px" />
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;  
                    </td>
                </tr>
            </table>
        </div>
    </div>

    <div runat="server" id="DIV1" style="margin-left: 5px; margin-right: 5px; margin-bottom: 10px">
        <table>
            <tr>
                <td>
                    <dx:ASPxGridView ID="GV_PART" ClientInstanceName="grid" runat="server" KeyFieldName="PoNo;po_rowid" AutoGenerateColumns="False"  
                        OnHtmlRowPrepared="GV_PART_HtmlRowPrepared" OnHtmlRowCreated="GV_PART_HtmlRowCreated" 
                        onhtmldatacellprepared="GV_PART_HtmlDataCellPrepared" onpageindexchanged="GV_PART_PageIndexChanged"
                        OnExportRenderBrick="GV_PART_ExportRenderBrick">
                        <ClientSideEvents EndCallback="function(s, e) { setHeight(); }" />
                        <SettingsBehavior AllowDragDrop="TRUE" AllowFocusedRow="True" AllowSelectByRowClick="True" ColumnResizeMode="Control" 
                            AutoExpandAllGroups="true" MergeGroupsMode="Always" SortMode="Value"/>
                        <SettingsPager PageSize="1000"></SettingsPager>
                        <Settings ShowFilterRow="True" ShowFilterRowMenu="True"   VerticalScrollBarMode="Visible" VerticalScrollBarStyle="Standard" VerticalScrollableHeight="500"
                                 ShowFilterRowMenuLikeItem="True"   ShowGroupPanel="True" ShowFooter="True" ShowGroupedColumns="true"/>
                        <SettingsSearchPanel Visible="True" />
                        <SettingsFilterControl AllowHierarchicalColumns="True"></SettingsFilterControl>
                        <Columns></Columns>
                        <TotalSummary>
                            <dx:ASPxSummaryItem DisplayFormat="{0:N4}" FieldName="TotalPrice" SummaryType="Sum" />             
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


