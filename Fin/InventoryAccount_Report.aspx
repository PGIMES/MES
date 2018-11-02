<%@ Page Title="【库存会计事务浏览】" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="InventoryAccount_Report.aspx.cs" Inherits="Fin_InventoryAccount_Report" %>

<%@ Register Assembly="DevExpress.Web.v17.2, Version=17.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <script type="text/javascript" src="/Content/js/jquery.min.js"></script>
    <script src="/Content/js/layer/layer.js"></script>
    <script src="/Content/js/plugins/layer/laydate/laydate.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#mestitle").text("【库存会计事务浏览】");
            setHeight();

            $(window).resize(function () {
                setHeight();
            });

        });

        function setHeight() {
            $("div[class=dxgvCSD]").css("height", ($(window).height() - $("#div_p").height() - 250) + "px");
        }

        function Get_site() {
            var url = "/select/select_site.aspx?domain=" +$("#div_p select[id*='ddl_domain']").val();

            layer.open({
                title: '地点选择',
                type: 2,
                area: ['600px', '600px'],
                fixed: false, //不固定
                maxmin: true,
                content: url
            });
        }

        function setvalue_site(ls_site) {
            $("#div_p input[id=*txt_site]").val(ls_site);
        }
    </script>

    <script type="text/javascript">
        var textSeparator = ";";
        function updateText() {
            var selectedItems = checkListBox.GetSelectedItems();
            checkComboBox.SetValue(getSelectedItemsValues(selectedItems));
        }
        function synchronizeListBoxValues(dropDown, args) {
            checkListBox.UnselectAll();
            var texts = dropDown.GetText().split(textSeparator);
            checkListBox.SelectValues(texts);
            updateText(); // for remove non-existing texts
        }
        function getSelectedItemsValues(items) {
            var values = [];
            for (var i = 0; i < items.length; i++)
                values.push(items[i].value);
            return values.join(textSeparator);
        }
    </script>
    <script type="text/javascript">
        var textSeparator2 = ";";
        function updateText2() {
            var selectedItems = checkListBox2.GetSelectedItems();
            checkComboBox2.SetValue(getSelectedItemsValues2(selectedItems));
        }
        function synchronizeListBoxValues2(dropDown, args) {
            checkListBox2.UnselectAll();
            var texts = dropDown.GetText().split(textSeparator2);
            checkListBox2.SelectValues(texts);
            updateText2(); // for remove non-existing texts
        }
        function getSelectedItemsValues2(items) {
            var values = [];
            for (var i = 0; i < items.length; i++)
                values.push(items[i].value);
            return values.join(textSeparator2);
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

    <div class="col-md-12" id="div_p"  style="margin-bottom:5px">
        <table style=" border-collapse: collapse;">
            <tr>
                <td style="width:35px;">类别</td>
                <td style="width:125px;"> 
                    <asp:DropDownList ID="ddl_typeno" runat="server" class="form-control input-s-md " Width="120px">
                        <asp:ListItem Value="A">原版报表</asp:ListItem>
                        <asp:ListItem Value="B">修改报表</asp:ListItem>
                        <asp:ListItem Value="C">透视表</asp:ListItem>
                    </asp:DropDownList>
                </td> 
                <td style="width:65px;">申请公司</td>
                <td style="width:125px;">
                    <asp:DropDownList ID="ddl_domain" runat="server" class="form-control input-s-md " Width="120px" AutoPostBack="true">
                        <asp:ListItem Value="200">昆山工厂</asp:ListItem>
                        <asp:ListItem Value="100">上海工厂</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td style="width:65px;">生效日期</td>
                <td style="width:125px;">
                    <asp:TextBox ID="StartDate" runat="server" class="form-control" Width="120px" 
                        onclick="laydate({type: 'date',format: 'YYYY/MM/DD',choose: function(dates){}});" />
                </td>
                <td style="width:15px;">~</td>
                <td style="width:125px;">
                    <asp:TextBox ID="EndDate" runat="server" class="form-control" Width="120px"
                        onclick="laydate({type: 'date',format: 'YYYY/MM/DD',choose: function(dates){}});" />
                </td>  
                <td style="width:40px;">地点</td>
                <td style="width:95px;">
                    <asp:DropDownList ID="ddl_fuhao2" runat="server" class="form-control input-s-md " Width="90px">
                        <asp:ListItem Value="1">等于</asp:ListItem>
                        <asp:ListItem Value="0">不等于</asp:ListItem>
                    </asp:DropDownList>
                </td> 
                <td style="width:155px;"> 
                    <dx:ASPxDropDownEdit ClientInstanceName="checkComboBox2" ID="ASPxDropDownEdit2" Width="150px" runat="server" AnimationType="None" CssClass="form-control input-s-md ">
                        <DropDownWindowStyle BackColor="#EDEDED" />
                        <DropDownWindowTemplate>
                            <dx:ASPxListBox Width="100%" ID="listBox2" ClientInstanceName="checkListBox2" SelectionMode="CheckColumn"
                                runat="server" Height="200" EnableSelectAll="true">
                                <FilteringSettings ShowSearchUI="true"/>
                                <Border BorderStyle="None" />
                                <BorderBottom BorderStyle="Solid" BorderWidth="1px" BorderColor="#DCDCDC" />
                                <Items> 
                                    <%--<dx:ListEditItem Text="Chrome" Value="0" />
                                    <dx:ListEditItem Text="Firefox" Value="1" />
                                    <dx:ListEditItem Text="IE" Value="2" />
                                    <dx:ListEditItem Text="Opera" Value="3" />
                                    <dx:ListEditItem Text="Safari" Value="4" />--%>
                                </Items>
                                <ClientSideEvents SelectedIndexChanged="updateText2" />
                            </dx:ASPxListBox>
                            <table style="width: 100%">
                                <tr>
                                    <td style="padding: 4px">
                                        <dx:ASPxButton ID="ASPxButton2" AutoPostBack="False" runat="server" Text="Close" style="float: right">
                                            <ClientSideEvents Click="function(s, e){ checkComboBox2.HideDropDown(); }" />
                                        </dx:ASPxButton>
                                    </td>
                                </tr>
                            </table>
                        </DropDownWindowTemplate>
                        <ClientSideEvents TextChanged="synchronizeListBoxValues2" DropDown="synchronizeListBoxValues2" />
                    </dx:ASPxDropDownEdit>
                </td>        
                <td style="width:60px;">产品类</td>
                <td style="width:95px;">
                    <asp:DropDownList ID="ddl_fuhao1" runat="server" class="form-control input-s-md " Width="90px">
                        <asp:ListItem Value="1">等于</asp:ListItem>
                        <asp:ListItem Value="0">不等于</asp:ListItem>
                    </asp:DropDownList>
                </td> 
                <td style="width:155px;"> 
                    <dx:ASPxDropDownEdit ClientInstanceName="checkComboBox" ID="ASPxDropDownEdit1" Width="150px" runat="server" AnimationType="None" CssClass="form-control input-s-md ">
                        <DropDownWindowStyle BackColor="#EDEDED" />
                        <DropDownWindowTemplate>
                            <dx:ASPxListBox Width="100%" ID="listBox" ClientInstanceName="checkListBox" SelectionMode="CheckColumn"
                                runat="server" Height="200" EnableSelectAll="true">
                                <FilteringSettings ShowSearchUI="true"/>
                                <Border BorderStyle="None" />
                                <BorderBottom BorderStyle="Solid" BorderWidth="1px" BorderColor="#DCDCDC" />
                                <Items> 
                                    <%--<dx:ListEditItem Text="Chrome" Value="0" />
                                    <dx:ListEditItem Text="Firefox" Value="1" />
                                    <dx:ListEditItem Text="IE" Value="2" />
                                    <dx:ListEditItem Text="Opera" Value="3" />
                                    <dx:ListEditItem Text="Safari" Value="4" />--%>
                                </Items>
                                <ClientSideEvents SelectedIndexChanged="updateText" />
                            </dx:ASPxListBox>
                            <table style="width: 100%">
                                <tr>
                                    <td style="padding: 4px">
                                        <dx:ASPxButton ID="ASPxButton1" AutoPostBack="False" runat="server" Text="Close" style="float: right">
                                            <ClientSideEvents Click="function(s, e){ checkComboBox.HideDropDown(); }" />
                                        </dx:ASPxButton>
                                    </td>
                                </tr>
                            </table>
                        </DropDownWindowTemplate>
                        <ClientSideEvents TextChanged="synchronizeListBoxValues" DropDown="synchronizeListBoxValues" />
                    </dx:ASPxDropDownEdit>
                </td>        
                <td id="td_btn">  
                    <button id="btn_search" type="button" class="btn btn-primary btn-large" runat="server" onserverclick="btn_search_Click"><i class="fa fa-search fa-fw"></i>&nbsp;查询</button> 
                    <button id="btn_export" type="button" class="btn btn-primary btn-large" runat="server" onserverclick="btn_export_Click"><i class="fa fa-download fa-fw"></i>&nbsp;导出</button>
                    <%--<button id="btn_export_t" type="button" class="btn btn-primary btn-large" runat="server" onserverclick="btn_export_t_Click"><i class="fa fa-download fa-fw"></i>&nbsp;导出透视表</button>--%>
                </td>
            </tr>                      
        </table>                   
    </div>

    <div class="col-sm-12">
        <table>
            <tr>
                <td>
                    <dx:ASPxPageControl ID="ASPxPageControl1" runat="server" ActiveTabIndex="0">
                        <TabPages>
                            <dx:TabPage Name="TabPage0" Text="查询结果">
                                <ContentCollection>
                                    <dx:ContentControl>
                                        <dx:ASPxGridView ID="gv" runat="server" KeyFieldName="" AutoGenerateColumns="False" OnPageIndexChanged="gv_PageIndexChanged"  ClientInstanceName="grid">
                                            <ClientSideEvents EndCallback="function(s, e) {setHeight();}"  />
                                            <SettingsPager PageSize="1000" ></SettingsPager>
                                            <Settings ShowFilterRow="True" ShowGroupPanel="false" ShowFilterRowMenu="True" ShowFilterRowMenuLikeItem="True" AutoFilterCondition="Contains" 
                                                VerticalScrollBarMode="Visible" VerticalScrollBarStyle="Standard" VerticalScrollableHeight="600" ShowFooter="true" />
                                            <SettingsBehavior AllowFocusedRow="True" ColumnResizeMode="Control" />
                                            <Columns> 
                                            </Columns>
                                            <TotalSummary>
                                            </TotalSummary>
                                            <Styles>
                                                <Header BackColor="#99CCFF"></Header>
                                                <FocusedRow BackColor="#99CCFF" ForeColor="#0000CC"></FocusedRow>
                                                <Footer HorizontalAlign="Right"></Footer>
                                            </Styles>
                                            <SettingsExport EnableClientSideExportAPI="true" ExcelExportMode="WYSIWYG" />
                                        </dx:ASPxGridView>
                                        <dx:ASPxGridViewExporter ID="gv_export" runat="server" GridViewID="gv">
                                        </dx:ASPxGridViewExporter>
                                    </dx:ContentControl>
                                </ContentCollection>
                            </dx:TabPage>
                            <dx:TabPage Name="TabPage1" Text="GL">
                                <ContentCollection>
                                    <dx:ContentControl>
                                        <dx:ASPxGridView ID="ASPxGridView1" runat="server" KeyFieldName="gl_acct" AutoGenerateColumns="False">
                                            <ClientSideEvents EndCallback="function(s, e) {setHeight();}"  />
                                            <SettingsPager PageSize="1000" ></SettingsPager>
                                            <Settings ShowFilterRow="True" ShowGroupPanel="false" ShowFilterRowMenu="True" ShowFilterRowMenuLikeItem="True" AutoFilterCondition="Contains" 
                                                VerticalScrollBarMode="Visible" VerticalScrollBarStyle="Standard" VerticalScrollableHeight="600"  />
                                            <SettingsBehavior AllowFocusedRow="True" ColumnResizeMode="Control"  />
                                            <Columns> 
                                                <dx:GridViewDataTextColumn Caption="总账账户" FieldName="gl_acct" Width="100px" VisibleIndex="1"></dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn Caption="描述" FieldName="gl_acct_desc" Width="250px" VisibleIndex="2"></dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn Caption="周转计算系数" FieldName="gl_zzxs" Width="120px" VisibleIndex="3"></dx:GridViewDataTextColumn>                                                
                                                <dx:GridViewDataTextColumn Caption="类别" FieldName="gl_type" Width="170px" VisibleIndex="4"></dx:GridViewDataTextColumn>
                                            </Columns>
                                            <Styles>
                                                <Header BackColor="#99CCFF"></Header>
                                                <FocusedRow BackColor="#99CCFF" ForeColor="#0000CC"></FocusedRow>
                                                <Footer HorizontalAlign="Right"></Footer>
                                            </Styles>
                                        </dx:ASPxGridView>
                                    </dx:ContentControl>
                                </ContentCollection>
                            </dx:TabPage>
                            <dx:TabPage Name="TabPage2" Text="地点">
                                <ContentCollection>
                                    <dx:ContentControl>
                                        <dx:ASPxGridView ID="ASPxGridView2" runat="server" KeyFieldName="" AutoGenerateColumns="False">
                                            <ClientSideEvents EndCallback="function(s, e) {setHeight();}"  />
                                            <SettingsPager PageSize="1000" ></SettingsPager>
                                            <Settings ShowFilterRow="True" ShowGroupPanel="false" ShowFilterRowMenu="True" ShowFilterRowMenuLikeItem="True" AutoFilterCondition="Contains" 
                                                VerticalScrollBarMode="Visible" VerticalScrollBarStyle="Standard" VerticalScrollableHeight="600"  />
                                            <SettingsBehavior AllowFocusedRow="True" ColumnResizeMode="Control"  />
                                            <Columns> 
                                                <dx:GridViewDataTextColumn Caption="地点" FieldName="si_site" Width="100px" VisibleIndex="1"></dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn Caption="描述" FieldName="si_desc" Width="200px" VisibleIndex="2"></dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn Caption="会计单位" FieldName="si_entity" Width="100px" VisibleIndex="3"></dx:GridViewDataTextColumn>    
                                                <dx:GridViewDataTextColumn Caption="默认库存状态" FieldName="si_status" Width="130px" VisibleIndex="4"></dx:GridViewDataTextColumn>                                                
                                                <dx:GridViewDataTextColumn Caption="地点类型" FieldName="si_type" Width="120px" VisibleIndex="5"></dx:GridViewDataTextColumn>
                                            </Columns>
                                            <Styles>
                                                <Header BackColor="#99CCFF"></Header>
                                                <FocusedRow BackColor="#99CCFF" ForeColor="#0000CC"></FocusedRow>
                                                <Footer HorizontalAlign="Right"></Footer>
                                            </Styles>
                                        </dx:ASPxGridView>
                                    </dx:ContentControl>
                                </ContentCollection>
                            </dx:TabPage>
                            <dx:TabPage Name="TabPage3" Text="库位">
                                <ContentCollection>
                                    <dx:ContentControl>
                                        <dx:ASPxGridView ID="ASPxGridView3" runat="server" KeyFieldName="" AutoGenerateColumns="False">
                                            <ClientSideEvents EndCallback="function(s, e) {setHeight();}"  />
                                            <SettingsPager PageSize="1000" ></SettingsPager>
                                            <Settings ShowFilterRow="True" ShowGroupPanel="false" ShowFilterRowMenu="True" ShowFilterRowMenuLikeItem="True" AutoFilterCondition="Contains" 
                                                VerticalScrollBarMode="Visible" VerticalScrollBarStyle="Standard" VerticalScrollableHeight="600"  />
                                            <SettingsBehavior AllowFocusedRow="True" ColumnResizeMode="Control"  />
                                            <Columns> 
                                                <%--<dx:GridViewDataTextColumn Caption="索引号" FieldName="loc_site_loc" Width="100px" VisibleIndex="1"></dx:GridViewDataTextColumn>--%>
                                                <dx:GridViewDataTextColumn Caption="地点" FieldName="loc_site" Width="100px" VisibleIndex="2"></dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn Caption="库位" FieldName="loc_loc" Width="100px" VisibleIndex="3"></dx:GridViewDataTextColumn>    
                                                <dx:GridViewDataTextColumn Caption="描述" FieldName="loc_desc" Width="300px" VisibleIndex="4"></dx:GridViewDataTextColumn>                                                
                                                <dx:GridViewDataTextColumn Caption="状态" FieldName="loc_status" Width="120px" VisibleIndex="5"></dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn Caption="库位类型" FieldName="loc_type" Width="120px" VisibleIndex="6"></dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn Caption="物料号是否R开头" FieldName="part_is_r" Width="150px" VisibleIndex="7"></dx:GridViewDataTextColumn>
                                            </Columns>
                                            <Styles>
                                                <Header BackColor="#99CCFF"></Header>
                                                <FocusedRow BackColor="#99CCFF" ForeColor="#0000CC"></FocusedRow>
                                                <Footer HorizontalAlign="Right"></Footer>
                                            </Styles>
                                        </dx:ASPxGridView>
                                    </dx:ContentControl>
                                </ContentCollection>
                            </dx:TabPage>
                            <dx:TabPage Name="TabPage4" Text="产品类">
                                <ContentCollection>
                                    <dx:ContentControl>
                                        <dx:ASPxGridView ID="ASPxGridView4" runat="server" KeyFieldName="" AutoGenerateColumns="False">
                                            <ClientSideEvents EndCallback="function(s, e) {setHeight();}"  />
                                            <SettingsPager PageSize="1000"></SettingsPager>
                                            <Settings ShowFilterRow="True" ShowGroupPanel="false" ShowFilterRowMenu="True" ShowFilterRowMenuLikeItem="True" AutoFilterCondition="Contains" 
                                                VerticalScrollBarMode="Visible" VerticalScrollBarStyle="Standard" VerticalScrollableHeight="600"  />
                                            <SettingsBehavior AllowFocusedRow="True" ColumnResizeMode="Control"  />
                                            <Columns> 
                                                <dx:GridViewDataTextColumn Caption="产品类编码" FieldName="pl_prod_line" Width="100px" VisibleIndex="1"></dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn Caption="产品类名称" FieldName="pl_desc" Width="200px" VisibleIndex="2"></dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn Caption="产品类_物流归类" FieldName="pl_type" Width="200px" VisibleIndex="3"></dx:GridViewDataTextColumn>  
                                            </Columns>
                                            <Styles>
                                                <Header BackColor="#99CCFF"></Header>
                                                <FocusedRow BackColor="#99CCFF" ForeColor="#0000CC"></FocusedRow>
                                                <Footer HorizontalAlign="Right"></Footer>
                                            </Styles>
                                        </dx:ASPxGridView>
                                    </dx:ContentControl>
                                </ContentCollection>
                            </dx:TabPage>
                        </TabPages>
                    </dx:ASPxPageControl>

                </td>
            </tr>
        </table>
    </div>
</asp:Content>

