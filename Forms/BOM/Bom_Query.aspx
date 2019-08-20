<%@ Page Title="Bom数据查询" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Bom_Query.aspx.cs" Inherits="Bom_Query" %>

<%@ Register Assembly="DevExpress.Web.v17.2, Version=17.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxTreeList.v17.2, Version=17.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxTreeList" TagPrefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <script type="text/javascript" src="/Content/js/jquery.min.js"></script>
    <script type="text/javascript" src="/Content/js/jquery.cookie.min.js"></script>
    <script src="/Content/js/plugins/layer/laydate/laydate.js"></script>

    <script type="text/javascript">
        $(document).ready(function () {
            $("#mestitle").text("【Bom数据查询】");
            setHeight();
            $(window).resize(function () {
                setHeight();
            });

        });


        function setHeight() {
            $("div[class=dxgvCSD]").css("height", ($(window).height() - $("#div_p").height() - 300) + "px");
        }

    </script>
    <script>
        function OnFocusedNodeChange(s, e) {
            $("#btnEdit").attr("href", "javascript:void(0)").addClass("disabled");//提前disabled,免间隙延迟导致产生链接错误
            var nodeKey = s.GetFocusedNodeKey();
            // var nodeState = s.GetNodeState(nodeKey);
            ////Expanded or Collapsed
            //if (nodeState === "Collapsed")
            //    s.ExpandNode(nodeKey);
            //else if (nodeState === "Expanded")
            //    s.CollapseNode(nodeKey);

            // var row = s.GetRowByNodeKey(nodeKey);
            // var pt_part = "";
            //  alert(row.cells[1].innerText);//表格中的数据 
            s.GetNodeValues(nodeKey, "pid", function (a) {//返回数据源中Tel的值                
                if (a == null) {
                    var domain = "";
                    s.GetNodeValues(nodeKey, "domain", function (_domain) {
                        domain = _domain
                    })
                    if (domain == "") {
                        s.GetNodeValues(nodeKey, "pt_part", function (b) {
                            $("#btnEdit").removeClass("disabled").attr("href", "/Platform/WorkFlowRun/Default.aspx?flowid=4a901bc7-ea83-43b1-80b6-5b14708dede9&pgino=" + b.substring(0, 7) + "&domain=" + domain + "&appid=b09b450e-6c02-4941-ad24-08ab046d68c7&tabid=tab_b09b450e6c024941ad2408ab046d68c7");
                        })
                    }


                } else {
                    $("#btnEdit").attr("href", "javascript:void(0)").addClass("disabled");
                }

            });

        };
    </script>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

    <div class="col-md-12">
        <div class="row  row-container">
            <div class="panel panel-info">
                <div class="panel-heading">
                    <strong>查询</strong>
                </div>
                <div class="panel-body collapse in" id="CPXX">
                    <div class="col-xs-12 col-sm-12  col-md-12 col-lg-12" style="font-size: 13px;">
                        <table class="tblCondition" style="border-collapse: collapse;">
                            <tr>
                                <td style="width: 60px;">物料号：</td>
                                <td style="width: 120px;">
                                    <asp:TextBox ID="pgino" class="form-control input-s-sm" runat="server" Width="110px"></asp:TextBox>
                                </td>

                                <td style="width: 40px;">工厂:</td>
                                <td style="width: 130px;">
                                    <asp:DropDownList ID="domain" runat="server" class="form-control input-s-md " Width="120px">
                                        <asp:ListItem Value="">ALL</asp:ListItem>
                                        <asp:ListItem Value="200">昆山工厂</asp:ListItem>
                                        <asp:ListItem Value="100">上海工厂</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td style="width: 40px;">版本：</td>
                                <td style="width: 240px;">
                                    <asp:RadioButtonList ID="IsLatest" class=" input-s-sm" runat="server" Width="164px"  RepeatDirection="Horizontal" Height="20px" >
                                        <asp:ListItem Value="" Text="最新版本" selected="True"></asp:ListItem>
                                        <asp:ListItem Value="ALL" Text="所有版本"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                                <td>&nbsp;&nbsp;
                                    <button id="btn_search" type="button" class="btn btn-primary btn-large" runat="server" onserverclick="btn_search_Click"><i class="fa fa-search fa-fw"></i>查询</button>
                                    <a id="btnEdit" class="btn btn-primary btn-large" href="javascript:void(0)" style="color: white" target="_blank"><i class="fa fa-search fa-edit"></i>修改</a>
                                    <%--<button id="btnExport" type="button" class="btn btn-primary btn-large" onserverclick="btnExport_Click" runat="server"><i class="fa fa-search fa-fw"></i>导出Excel</button>--%>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div class="row-container">
        <div class="panel panel-info">
            <div class="panel-body ">
                <dx:ASPxTreeList ID="bomtree" runat="server" AutoGenerateColumns="False" Width="100%"
                    KeyFieldName="id" ParentFieldName="PID" ViewStateMode="Enabled" SettingsBehavior-AllowDragDrop="true" OnHtmlRowPrepared="bomtree_HtmlRowPrepared">
                    <ClientSideEvents EndCallback="function(s, e) {
	                                                    $('table[id*=treeList_D] th:last a').find('span').remove();                                                      
                                                    }
                                                    "
                        Init="" FocusedNodeChanged="OnFocusedNodeChange" />
                    <Settings GridLines="Both" ShowTreeLines="false" />
                    <SettingsBehavior ExpandCollapseAction="NodeDblClick" AllowFocusedNode="True" AllowDragDrop="true" FocusNodeOnLoad="false" />
                    <SettingsCustomizationWindow PopupHorizontalAlign="RightSides" PopupVerticalAlign="BottomSides" />
                    <SettingsEditing Mode="EditFormAndDisplayNode" />
                    <SettingsPopupEditForm Width="500" />
                    <SettingsText />
                    <SettingsDataSecurity AllowDelete="False" AllowEdit="False" AllowInsert="False" />
                    <SettingsPopup>
                        <EditForm VerticalOffset="-1" Width="500px">
                        </EditForm>
                    </SettingsPopup>
                    <SettingsExport EnableClientSideExportAPI="True"  ExpandAllNodes="true" ExportAllPages="true" />
                    <SettingsPager Mode="ShowPager" AlwaysShowPager="true" EnableAdaptivity="true" NumericButtonCount="50" PageSize="50">
                        <PageSizeItemSettings Items="100, 200, 500,1000" Visible="true" />
                    </SettingsPager>
                    <Styles>
                        <PagerTopPanel>
                            <BorderBottom BorderStyle="Solid" />
                        </PagerTopPanel>
                        <PagerBottomPanel>
                            <BorderTop BorderStyle="Solid" />
                        </PagerBottomPanel>
                        <AlternatingNode Enabled="True" />
                    </Styles>
                    <Columns>
                        <dx:TreeListTextColumn FieldName="pt_part" Caption="物料号" EditCellStyle-CssClass="" VisibleIndex="0">
                            <EditFormSettings VisibleIndex="0" ColumnSpan="1" />
                        </dx:TreeListTextColumn>
                        <dx:TreeListTextColumn FieldName="pt_desc1" Caption="描述(零件号)" VisibleIndex="1">
                            <EditFormSettings VisibleIndex="1" ColumnSpan="1" />
                        </dx:TreeListTextColumn>
                        <dx:TreeListDataColumn FieldName="pt_desc2" Caption="描述2(零件名称)" VisibleIndex="2">
                            <EditFormSettings VisibleIndex="2" ColumnSpan="1" />
                        </dx:TreeListDataColumn>
                        <dx:TreeListTextColumn FieldName="drawno" Caption="图纸号" VisibleIndex="3">
                            <EditFormSettings VisibleIndex="3" ColumnSpan="1" />
                        </dx:TreeListTextColumn>
                        <dx:TreeListTextColumn FieldName="pt_net_wt" Caption="单件重量" VisibleIndex="4">
                            <EditFormSettings VisibleIndex="4" ColumnSpan="1" />
                        </dx:TreeListTextColumn>
                        <dx:TreeListTextColumn FieldName="ps_qty_per" Caption="单件用量" VisibleIndex="5" PropertiesTextEdit-ValidationSettings-RequiredField-IsRequired="true">
                            <PropertiesTextEdit>
                                <ValidationSettings>
                                    <RequiredField IsRequired="True"></RequiredField>
                                </ValidationSettings>
                            </PropertiesTextEdit>
                            <EditFormSettings VisibleIndex="5" ColumnSpan="1" />
                        </dx:TreeListTextColumn>
                        <dx:TreeListTextColumn FieldName="unit" Caption="单位" VisibleIndex="6">
                            <EditFormSettings VisibleIndex="6" ColumnSpan="1" />
                        </dx:TreeListTextColumn>
                        <dx:TreeListTextColumn FieldName="material" Caption="材料" VisibleIndex="7">
                            <EditFormSettings VisibleIndex="7" ColumnSpan="1" />
                        </dx:TreeListTextColumn>
                        <dx:TreeListTextColumn FieldName="vendor" Caption="供应商" VisibleIndex="8">
                            <EditFormSettings VisibleIndex="8" ColumnSpan="1" />
                        </dx:TreeListTextColumn>
                        <dx:TreeListTextColumn FieldName="ps_op" Caption="消耗工序" VisibleIndex="9">
                            <EditFormSettings VisibleIndex="9" ColumnSpan="1" />
                        </dx:TreeListTextColumn>
                        <dx:TreeListTextColumn FieldName="note" Caption="备注" VisibleIndex="10">
                            <EditFormSettings VisibleIndex="10" ColumnSpan="1" />
                        </dx:TreeListTextColumn>
                        <dx:TreeListTextColumn FieldName="domain" Caption="工厂" VisibleIndex="11">
                            <EditFormSettings VisibleIndex="11" ColumnSpan="1" />
                        </dx:TreeListTextColumn>
                        <dx:TreeListTextColumn FieldName="pt_status" Caption="状态" VisibleIndex="12">
                            <EditFormSettings VisibleIndex="12" ColumnSpan="1" />
                        </dx:TreeListTextColumn>
                        <dx:TreeListTextColumn FieldName="product_user" Caption="产品负责人" VisibleIndex="13">
                            <EditFormSettings VisibleIndex="13" ColumnSpan="1" />
                        </dx:TreeListTextColumn>
                        <dx:TreeListTextColumn FieldName="bz_user" Caption="包装负责人" VisibleIndex="14">
                            <EditFormSettings VisibleIndex="14" ColumnSpan="1" />
                        </dx:TreeListTextColumn>
                        <dx:TreeListTokenBoxColumn FieldName="PID" Caption="pid" Visible="false">
                            <PropertiesTokenBox AllowMouseWheel="True" Tokens="">
                            </PropertiesTokenBox>
                            <EditFormSettings VisibleIndex="15" Visible="False" />
                        </dx:TreeListTokenBoxColumn>
                        <dx:TreeListTokenBoxColumn FieldName="id" Caption="id" Visible="false" VisibleIndex="16">
                            <PropertiesTokenBox AllowMouseWheel="True" Tokens="">
                            </PropertiesTokenBox>
                            <EditFormSettings VisibleIndex="16" Visible="False" />
                        </dx:TreeListTokenBoxColumn>


                    </Columns>
                    <Toolbars>
                        <dx:TreeListToolbar EnableAdaptivity="true">
                            <Items>
                                <%--<dx:TreeListToolbarItem Command="ExportToPdf" />--%>
                                <dx:TreeListToolbarItem Command="ExportToXls"    Text="导出Excel" />
                                <%--<dx:TreeListToolbarItem Command="ExportToXlsx" />
                                <dx:TreeListToolbarItem Command="ExportToDocx" />
                                <dx:TreeListToolbarItem Command="ExportToRtf" />--%>
                            </Items>
                        </dx:TreeListToolbar>
                    </Toolbars>
                </dx:ASPxTreeList>

            </div>
        </div>
    </div>




</asp:Content>

