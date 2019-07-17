<%@ Page Title="【产品税率】" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Fin_Base_QGSF.aspx.cs" Inherits="Fin_Fin_Base_QGSF" %>

<%@ Register Assembly="DevExpress.Web.v17.2, Version=17.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <script src="/Content/js/jquery.min.js"  type="text/javascript"></script>
    <script src="/Content/js/bootstrap.min.js"></script>
    <script src="/Content/js/layer/layer.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#mestitle").text("【产品税率】");

            setHeight();
            $(window).resize(function () {
                setHeight();
            });

            $('#btn_add').click(function () {
                var url = '/Fin/Fin_Base_QGSF_Add.aspx';
                layer.open({
                    title: '新增基率&清关税<font color="red">【Base Rate】【301 Rate】请填写小数.</font>',
                    closeBtn: 2,
                    type: 2,
                    area: ['750px', '300px'],
                    fixed: false, //不固定
                    maxmin: true, //开启最大化最小化按钮
                    content: url,
                    cancel: function (index, layero) {//取消事件
                    },
                    end: function () {//无论是确认还是取消，只要层被销毁了，end都会执行，不携带任何参数。layer.open关闭事件
                        location.reload();　　//layer.open关闭刷新
                    }

                });
            });
            $('#btn_del').click(function () {
                if (grid.GetSelectedRowCount() < 1) { layer.alert("请至少选择一条记录!"); return; }

                grid.GetSelectedFieldValues('domain;wlh', function GetVal(values) {

                    var ls_key = ""; 
                    for (var i = 0; i < values.length; i++) {
                        var ls_domain = values[i][0];
                        var ls_wlh = values[i][1];

                        ls_key = ls_key + ls_wlh + "|" + ls_domain + ";";
                    }

                    $.ajax({
                        type: "post",
                        url: "Fin_Base_QGSF.aspx/del_data",
                        data: "{'keys':'" + ls_key + "'}",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        async: false,//默认是true，异步；false为同步，此方法执行完在执行下面代码
                        success: function (data) {
                            var obj = eval(data.d);
                            layer.alert(obj[0].re_flag, function (index) { location.reload(); });
                        }
                    });

                });
            });
        });

        function setHeight() {
            $("div[class=dxgvCSD]").css("height", ($(window).height() - $("#div_p").height() - 200) + "px");
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
                    <td style="text-align:right;">物料号：</td>
                    <td>
                        <asp:TextBox ID="txt_wlh" class="form-control input-s-md " runat="server"></asp:TextBox>
                    </td>
                    <td> 
                        &nbsp;
                        <asp:Button ID="Bt_select" runat="server" Text="查询" class="btn btn-large btn-primary" OnClick="Bt_select_Click" Width="70px" />
                        &nbsp;
                        <button id="btn_add" type="button" class="btn btn-primary btn-large"><i class="fa fa-plus fa-fw"></i>&nbsp;新增</button> 
                        &nbsp;
                        <button id="btn_del" type="button" class="btn btn-primary btn-large"><i class="fa fa-remove fa-fw"></i>&nbsp;删除</button> 
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
                    <dx:ASPxGridView ID="GV_PART" ClientInstanceName="grid" runat="server" KeyFieldName="domain;wlh" AutoGenerateColumns="False"  
                             OnPageIndexChanged="GV_PART_PageIndexChanged" ><%--3030--%>
                        <ClientSideEvents EndCallback="function(s, e) { setHeight(); }" />
                        <SettingsBehavior AllowDragDrop="TRUE" AllowFocusedRow="false" AllowSelectByRowClick="false" ColumnResizeMode="Control" AutoExpandAllGroups="true" MergeGroupsMode="Always" SortMode="Value" />
                        <SettingsPager PageSize="1000"></SettingsPager>
                        <Settings ShowFilterRow="True" ShowFilterRowMenu="True"   VerticalScrollBarMode="Visible" VerticalScrollBarStyle="Standard" VerticalScrollableHeight="500"
                                 ShowFilterRowMenuLikeItem="True"  ShowFooter="True" />
                        <Columns>
                            <dx:GridViewCommandColumn   ShowClearFilterButton="true" ShowSelectCheckbox="true" Name="Sel" Width="40" VisibleIndex="0"  SelectAllCheckboxMode="Page"  >
                            </dx:GridViewCommandColumn> 
                            <dx:GridViewDataTextColumn Caption="域" FieldName="domain" Width="40px" VisibleIndex="1" />
                            <dx:GridViewDataTextColumn Caption="物料号" FieldName="wlh" Width="120px" VisibleIndex="2" />
                            <dx:GridViewDataTextColumn Caption="Customer Part #" FieldName="pt_desc1" Width="200px" VisibleIndex="3" />
                            <dx:GridViewDataTextColumn Caption="描述" FieldName="com_desc" Width="150px" VisibleIndex="3" />
                            <dx:GridViewDataTextColumn Caption="产品类#" FieldName="pt_prod_line" Width="60px" VisibleIndex="3" />
                            <dx:GridViewDataTextColumn Caption="产品类名称" FieldName="pl_desc" Width="150px" VisibleIndex="3" />
                            <dx:GridViewDataTextColumn Caption="HS Code_US" FieldName="com_comm_code" Width="150px" VisibleIndex="4" />
                            <dx:GridViewDataTextColumn Caption="301 Code" FieldName="301code" Width="130px" VisibleIndex="5" />
                            <dx:GridViewDataTextColumn Caption="Base Rate" FieldName="BaseRate" Width="90px" VisibleIndex="6" >
                                <PropertiesTextEdit DisplayFormatString="{0:P1}"></PropertiesTextEdit>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="301 Rate" FieldName="301Rate" Width="90px" VisibleIndex="7" >
                                <PropertiesTextEdit DisplayFormatString="{0:P1}"></PropertiesTextEdit>
                            </dx:GridViewDataTextColumn>

                            <%--<dx:GridViewDataTextColumn Caption="po_GroupID" FieldName="po_GroupID" VisibleIndex="98"
                                 HeaderStyle-CssClass="hidden" CellStyle-CssClass="hidden" FooterCellStyle-CssClass="hidden" FilterCellStyle-CssClass="hidden">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="rct_GroupID" FieldName="rct_GroupID" VisibleIndex="99"
                                 HeaderStyle-CssClass="hidden" CellStyle-CssClass="hidden" FooterCellStyle-CssClass="hidden" FilterCellStyle-CssClass="hidden">
                            </dx:GridViewDataTextColumn>--%>
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

