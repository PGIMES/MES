<%@ Page Title="夹具清单" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="JiaJu_Detail.aspx.cs" Inherits="JiaJu_JiaJu_Detail" %>
<%@ Register Assembly="DevExpress.Web.v17.2, Version=17.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <script type="text/javascript" src="/Content/js/jquery.min.js"></script>
    <script src="/Content/js/layer/layer.js"></script>
    <script src="/Content/js/plugins/layer/laydate/laydate.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#mestitle").html("【夹具清单】");
            setHeight();

            $(window).resize(function () {
                setHeight();
            });

        });

        function setHeight() {
            $("div[class=dxgvCSD]").css("height", ($(window).height() - $("#div_p").height() - 250) + "px");
        }



    </script>


    <script type="text/javascript">
        function OnToolbarItemClick(s, e) {
            if (IsCustomExportToolbarCommand(e.item.name)) {
                e.processOnServer = true;
                e.usePostBack = true;
            }
        }
        function IsCustomExportToolbarCommand(command) {
            return command == "CustomExportToXLS" || command == "CustomExportToXLSX";
        }
    </script>
   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

   <div class="row" style="margin: 0px 2px 1px 2px"  id="div_p">
        <div class="col-sm-12  col-md-12">
            <div class="panel panel-info">
                <div class="panel-heading">
                    <strong>夹具清单查询</strong>
                </div>
                <div class="panel-body">
                    <div class="col-sm-12 col-md-12">
                        <table style="width: 1000px">
                            <tr>
                              <td>
                                    域：
                                </td>
                                <td>
                               <asp:DropDownList ID="Drop_comp" class="form-control input-s-sm" 
                                        runat="server" Width="150px" >
                                   <asp:ListItem>--请选择--</asp:ListItem>
                                   <asp:ListItem>100</asp:ListItem>
                                   <asp:ListItem>200</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    夹具号：
                                </td>
                                <td>
                                    <input id="txt_jiajuno" class="form-control input-s-sm" runat="server"   style=" width:150px"  />
                                </td>
                               <td>
                                    夹具类型：
                                </td>
                                <td>
                                   <%-- <asp:DropDownList ID="Drop_type" class="form-control input-s-sm" runat="server" Width="150px" />--%>
                                     <input id="txt_type" class="form-control input-s-sm" runat="server"  style=" width:150px"/>
                                </td>
                               
                                <td>
                                    项目号：
                                </td>
                                <td>
                                    <input id="txt_pn" class="form-control input-s-sm" runat="server"  style=" width:150px"/>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    夹具状态：
                                </td>
                                <td>
                                    <asp:DropDownList ID="Drop_status" class="form-control input-s-sm" runat="server"  Width="150px" />
                                </td>
                                <td>
                                    客户：
                                </td>
                                <td>
                                  <input id="txt_customer" class="form-control input-s-sm" runat="server"   style=" width:150px"  />
                                </td>
                                <td>
                                    库位：
                                </td>
                                <td>
                                   <input id="txt_loc" class="form-control input-s-sm" runat="server"   style=" width:150px"  />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                  </td>
                                <td><button id="btn_search" runat="server"  class="btn btn-primary btn-large"  onserverclick="btn_search_Click" type="button">
                                <i class="fa fa-search fa-fw"></i>&nbsp;查询</button>  &nbsp; &nbsp; &nbsp;
                              </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div class="col-sm-12">
        <table>
            <tr>
                <td>
                                        <dx:ASPxGridView ID="ASPxGridView3" ClientInstanceName="Grid3" runat="server" KeyFieldName="jiajuno" AutoGenerateColumns="False" 
                                            OnToolbarItemClick="ASPxGridView3_ToolbarItemClick" OnCellEditorInitialize="ASPxGridView3_CellEditorInitialize"
                                            OnRowValidating="ASPxGridView3_RowValidating" OnRowUpdating="ASPxGridView3_RowUpdating" OnRowInserting="ASPxGridView3_RowInserting"
                                            OnRowDeleting="ASPxGridView3_RowDeleting">
                                            <Toolbars>
                                                <dx:GridViewToolbar ItemAlign="Right" EnableAdaptivity="true">
                                                    <Items>
                                                        <dx:GridViewToolbarItem Command="New" Text="新增" />
                                                        <dx:GridViewToolbarItem Command="Edit" Text="修改"  />
                                                        <dx:GridViewToolbarItem Command="Delete" Text="删除" />
                                                        <dx:GridViewToolbarItem Text="导出" Image-IconID="actions_download_16x16office2013" BeginGroup="true">
                                                            <Items>
                                                                <dx:GridViewToolbarItem Name="CustomExportToXLSX" 
                                                                    Text="Export to XLSX(夹具清单)" 
                                                                    Image-IconID="export_exporttoxlsx_16x16office2013" >
<Image IconID="export_exporttoxlsx_16x16office2013"></Image>
                                                                </dx:GridViewToolbarItem>
                                                            </Items>

<Image IconID="actions_download_16x16office2013"></Image>
                                                        </dx:GridViewToolbarItem>
                                                        <dx:GridViewToolbarItem BeginGroup="true">
                                                            <Template>
                                                                <dx:ASPxButtonEdit ID="tbToolbarSearch3" runat="server" NullText="Search..." Height="100%">
                                                                    <Buttons>
                                                                        <dx:SpinButtonExtended Image-IconID="find_find_16x16gray" />
                                                                    </Buttons>
                                                                </dx:ASPxButtonEdit>
                                                            </Template>
                                                        </dx:GridViewToolbarItem>
                                                    </Items>
                                                </dx:GridViewToolbar>
                                            </Toolbars>
                                            <SettingsSearchPanel CustomEditorID="tbToolbarSearch3" />
                                            <ClientSideEvents EndCallback="function(s, e) {setHeight();}"  ToolbarItemClick="OnToolbarItemClick" />
                                            <SettingsPager PageSize="1000" ></SettingsPager>
                                            <Settings ShowFilterRow="True" ShowGroupPanel="false" ShowFilterRowMenu="True" ShowFilterRowMenuLikeItem="True" AutoFilterCondition="Contains" 
                                                VerticalScrollBarMode="Visible" VerticalScrollBarStyle="Standard" VerticalScrollableHeight="600"  />
                                            <SettingsBehavior AllowFocusedRow="True" ColumnResizeMode="Control"  />
                                            <SettingsExport EnableClientSideExportAPI="true" ExcelExportMode="DataAware" />
                                            <Columns> 
                                            <dx:GridViewDataComboBoxColumn Caption="域" FieldName="comp" Width="60px" VisibleIndex="1">
                                                    <PropertiesComboBox TextField="codevalue" ValueField="codevalue" EnableSynchronization="false" IncrementalFilteringMode="StartsWith">
                                                    </PropertiesComboBox>
                                                </dx:GridViewDataComboBoxColumn>
                                                    <%--<dx:GridViewDataComboBoxColumn Caption="夹具类型" FieldName="type" Width="80px" VisibleIndex="1">
                                                    <PropertiesComboBox TextField="codevalue" ValueField="codevalue" EnableSynchronization="false" IncrementalFilteringMode="StartsWith">
                                                    </PropertiesComboBox>
                                                </dx:GridViewDataComboBoxColumn>--%>
                                                   
                                                <dx:GridViewDataTextColumn Caption="夹具号" FieldName="jiajuno" Width="80px" VisibleIndex="2"></dx:GridViewDataTextColumn>  
                                                <dx:GridViewDataTextColumn Caption="夹具名称" FieldName="type" Width="80px" VisibleIndex="3"></dx:GridViewDataTextColumn>                                                
                                                <dx:GridViewDataTextColumn Caption="项目号" FieldName="pn" Width="250px" VisibleIndex="4"></dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn Caption="零件号" FieldName="pn_name" Width="200px" VisibleIndex="5"></dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn Caption="工序号" FieldName="gxh" Width="80px" VisibleIndex="6"></dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn Caption="产线" FieldName="line" Width="80px" VisibleIndex="7"></dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn Caption="工位" FieldName="gongwei" Width="80px" VisibleIndex="8"></dx:GridViewDataTextColumn>
                                                <dx:GridViewDataComboBoxColumn Caption="归属部门" FieldName="zc_bel" Width="80px" VisibleIndex="9">
                                                    <PropertiesComboBox TextField="codevalue" ValueField="codevalue" EnableSynchronization="false" IncrementalFilteringMode="StartsWith"  DropDownStyle="DropDown">
                                                    </PropertiesComboBox>
                                                </dx:GridViewDataComboBoxColumn>
                                             <dx:GridViewDataTextColumn Caption="库位" FieldName="loc" Width="100px" VisibleIndex="10"></dx:GridViewDataTextColumn>
                                             <dx:GridViewDataTextColumn Caption="客户" FieldName="customer" Width="100px" VisibleIndex="11"></dx:GridViewDataTextColumn>
                                              <dx:GridViewDataTextColumn Caption="供应商" FieldName="supplier" Width="100px" VisibleIndex="12"></dx:GridViewDataTextColumn>
                                             <dx:GridViewDataTextColumn Caption="数量" FieldName="quantity" Width="60px" VisibleIndex="13"></dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="状态" FieldName="status" Width="60px" VisibleIndex="14"></dx:GridViewDataTextColumn>
                                            </Columns>
                                            <EditFormLayoutProperties ColCount="5">
                                                <Items>
                                                  <dx:GridViewColumnLayoutItem ColumnName="comp" />
                                                    <dx:GridViewColumnLayoutItem ColumnName="type" />
                                                    <dx:GridViewColumnLayoutItem ColumnName="jiajuno" />
                                                    <dx:GridViewColumnLayoutItem ColumnName="pn" />
                                                    <dx:GridViewColumnLayoutItem ColumnName="pn_name" />
                                                    <dx:GridViewColumnLayoutItem ColumnName="gxh" />
                                                    <dx:GridViewColumnLayoutItem ColumnName="line" />
                                                    <dx:GridViewColumnLayoutItem ColumnName="gongwei" />
                                                    <dx:GridViewColumnLayoutItem ColumnName="zc_bel" />
                                                    <dx:GridViewColumnLayoutItem ColumnName="loc" />
                                                    <dx:GridViewColumnLayoutItem ColumnName="customer" />
                                                    <dx:GridViewColumnLayoutItem ColumnName="supplier" />
                                                    <dx:GridViewColumnLayoutItem ColumnName="quantity" />
                                                    <dx:GridViewColumnLayoutItem ColumnName="status" />
                                                    <dx:EditModeCommandLayoutItem ColSpan="4" HorizontalAlign="right" />
                                                </Items>
                                            </EditFormLayoutProperties>
                                            <Styles>
                                                <Header BackColor="#99CCFF"></Header>
                                                <FocusedRow BackColor="#99CCFF" ForeColor="#0000CC"></FocusedRow>
                                                <Footer HorizontalAlign="Right"></Footer>
                                            </Styles>
                                        </dx:ASPxGridView>
                                  <dx:ASPxGridViewExporter ID="gv_export" runat="server" GridViewID="gv">
                                        </dx:ASPxGridViewExporter>
                  

                </td>
            </tr>
        </table>
    </div>
</asp:Content>


