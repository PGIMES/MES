<%@ Page Title="生产性物料查询" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="WuLiao_Report_Query.aspx.cs" Inherits="Forms_MaterialBase_WuLiao_Report_Query" %>

<%@ Register Assembly="DevExpress.Web.v17.2, Version=17.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <script src="../../Content/js/jquery.min.js" type="text/javascript"></script>
    <script src="../../Content/js/jquery.cookie.min.js" type="text/javascript"></script>
    <script src="../../Content/js/layer/layer.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#mestitle").text("【生产性物料查询】");

            $('#btn_add').click(function () {
                window.open('/Platform/WorkFlowRun/Default.aspx?flowid=d9cb9476-13f9-48ec-a87e-5b84ca0790b0&appid=33C8FB5D-CB37-4667-AE06-69A87A23542E')
            });

            $('#btn_edit').click(function () {
                if (grid.GetSelectedRowCount() != 1) { layer.alert("请选择一条记录!"); return; }

                grid.GetSelectedFieldValues("wlh;formno;gc_version;bz_version;comp;part_no", function GetVal(values) {
                    //for (var i = 0; i < values.length; i++) { values[i][1]}
                    var wlh = values[0][0];
                    var formno = values[0][1];
                    var gc_version = values[0][2];
                    var bz_version = values[0][3];
                    var domain = values[0][4];
                    var part_no = values[0][5];

                    $.ajax({
                        type: "post",
                        url: "WuLiao_Report_Query.aspx/CheckData",
                        data: "{'wlh':'" + wlh + "','gc_version':'" + gc_version + "', 'bz_version':'" + bz_version + "','comp':'" + domain + "','part_no':'" + part_no + "'}",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        async: false, //默认是true，异步；false为同步，此方法执行完在执行下面代码
                        success: function (data) {
                            var obj = eval(data.d);

                            if (obj[0].re_flag != "") {
                                layer.alert(obj[0].re_flag);
                            } else {
                                // window.open('/mes/Platform/WorkFlowRun/Default.aspx?flowid=d9cb9476-13f9-48ec-a87e-5b84ca0790b0&appid=33C8FB5D-CB37-4667-AE06-69A87A23542E&state=edit&formno=' + formno + '&wlh=' + wlh + '&gc_version=' + gc_version & +'&bz_version=' + bz_version);
                                window.open('/Platform/WorkFlowRun/Default.aspx?flowid=d9cb9476-13f9-48ec-a87e-5b84ca0790b0&appid=33C8FB5D-CB37-4667-AE06-69A87A23542E&state=edit&formid=' + formno + '&wlh=' + wlh + '&gc=' + gc_version + '&bz=' + bz_version + '&domain=' + domain + '&part_no=' + part_no);

                            }

                        }

                    });
                });
            });

            setHeight();

            $(window).resize(function () {
                setHeight();
            });

            mergecells();

        });
        var rows1 = ""; var rowsnext = "";
        function mergecells() {

               var gvid = "";
//            if ($("#MainContent_ddl_typeno").val() == "机加") {
                gvid = "MainContent_gv_DXMainTable";
//            }
//            if ($("#MainContent_ddl_typeno").val() == "压铸") {
//                gvid = "MainContent_gv_yz_DXMainTable";
//            }

            $("#" + gvid + " tr[class*=DataRow]").each(function (index, item) {
                var rowspans = $(item).find("td:eq(1)").attr("rowspan");


                if (rowspans != undefined) {
                    $(item).find("td:first").attr("rowspan", rowspans);
                    for (var i = 1; i < rowspans; i++) {
                        $($("#" + gvid + " tr[class*=DataRow]")[index + i]).find("td:first").hide();
                    }
                }

               
            })
        }

        function setHeight() {
            $("div[class=dxgvCSD]").css("height", ($(window).height() - $("#div_p").height() - 180) + "px");
        }
         	
    </script>

    <script type="text/javascript">
        var textSeparator = ";";
        function updateText() {
            var selectedItems = checkListBox.GetSelectedItems();
            checkComboBox.SetText(getSelectedItemsText(selectedItems));
        }
        function synchronizeListBoxValues(dropDown, args) {
            checkListBox.UnselectAll();
            var texts = dropDown.GetText().split(textSeparator);
            var values = getValuesByTexts(texts);
            checkListBox.SelectValues(values);
            updateText(); // for remove non-existing texts
        }
        function getSelectedItemsText(items) {
            var texts = [];
            for (var i = 0; i < items.length; i++)
                texts.push(items[i].text);
            return texts.join(textSeparator);
        }
        function getValuesByTexts(texts) {
            var actualValues = [];
            var item;
            for (var i = 0; i < texts.length; i++) {
                item = checkListBox.FindItemByText(texts[i]);
                if (item != null)
                    actualValues.push(item.value);
            }
            return actualValues;
        }
    </script>

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    
    <div class="col-md-12" id="div_p"  style="margin-bottom:5px">
        <table style=" border-collapse: collapse;">
            <tr>
             <td style="width:70px;">工厂:</td>
                                <td style="width:100px;"> 
                                    <asp:DropDownList ID="ddl_comp" runat="server" class="form-control input-s-md " Width="120px">
                                        <asp:ListItem Value="">ALL</asp:ListItem>
                                        <asp:ListItem Value="200">昆山工厂</asp:ListItem>
                                        <asp:ListItem Value="100">上海工厂</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                <td style="width:70px;">物料号</td>
                <td style="width:120px;">
                    <asp:TextBox ID="txt_pgi_no" class="form-control input-s-sm" runat="server" Width="110px"></asp:TextBox>
                </td>
                <td style="width:70px;">客户零件号</td>
                <td style="width:130px;">
                    <asp:TextBox ID="txt_pn" class="form-control input-s-sm" runat="server" Width="120px"></asp:TextBox>
                </td>    
            
                                      
                <td style="width:70px;">产品类</td>
                <td style="width:150px;"> 
                    <dx:ASPxDropDownEdit ClientInstanceName="checkComboBox" ID="ASPxDropDownEdit1" Width="150px" runat="server" AnimationType="None" CssClass="form-control input-s-md ">
                        <DropDownWindowStyle BackColor="#EDEDED" />
                        <DropDownWindowTemplate>
                            <dx:ASPxListBox Width="100%" ID="listBox" ClientInstanceName="checkListBox" SelectionMode="CheckColumn"
                                runat="server" Height="200" EnableSelectAll="true">
                                <FilteringSettings ShowSearchUI="true"/>
                                <Border BorderStyle="None" />
                                <BorderBottom BorderStyle="Solid" BorderWidth="1px" BorderColor="#DCDCDC" />
                                <Items> 
                                   
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
                <td>  
                    &nbsp;&nbsp; <%--runat="server" onserverclick="btn_edit_Click"--%>
                    <button id="btn_search" type="button" class="btn btn-primary btn-large" runat="server" onserverclick="btn_search_Click"><i class="fa fa-search fa-fw"></i>&nbsp;查询</button>    
                    <button id="btn_add" type="button" class="btn btn-primary btn-large"><i class="fa fa-plus fa-fw"></i>&nbsp;新增</button>  
                    <button id="btn_edit" type="button" class="btn btn-primary btn-large"><i class="fa fa-pencil-square-o fa-fw"></i>&nbsp;编辑</button> 
                    <button id="btn_import" type="button" class="btn btn-primary btn-large" runat="server" onserverclick="btn_import_Click"><i class="fa fa-download fa-fw"></i>&nbsp;导出</button>
                </td>
            </tr>                      
        </table>
                   
    </div>

    <div class="col-sm-12">
        <table>
            <tr>
                <td><%-- OnHtmlDataCellPrepared="gv_HtmlDataCellPrepared"--%>
                    <dx:ASPxGridView ID="gv" runat="server" KeyFieldName="id" AutoGenerateColumns="False" Width="1900px" OnPageIndexChanged="gv_PageIndexChanged"  ClientInstanceName="grid" 
                          OnCustomCellMerge="gv_CustomCellMerge">
                        <ClientSideEvents EndCallback="function(s, e) {           //if(MainContent_gv_DXMainTable.cpPageChanged == 1)     //grid为控件的客户端id
            	                   // window.alert('Page changed!');
                                    mergecells();setHeight();
        	                    }"  />
                        <SettingsPager PageSize="20" ></SettingsPager>
                        <Settings ShowFilterRow="True" ShowGroupPanel="false" ShowFilterRowMenu="True" ShowFilterRowMenuLikeItem="True" AutoFilterCondition="Contains" 
                            VerticalScrollBarMode="Visible" VerticalScrollBarStyle="Standard" VerticalScrollableHeight="600"  />
                        <SettingsBehavior AllowFocusedRow="True" ColumnResizeMode="Control"  />
                        <Columns>
                            <dx:GridViewCommandColumn   ShowClearFilterButton="true" ShowSelectCheckbox="true" Name="Sel" Width="40" VisibleIndex="0"    >
                                
                            </dx:GridViewCommandColumn>   
           
                                           
                            <dx:GridViewDataTextColumn Caption="表单号" FieldName="formno" Width="80px" VisibleIndex="1" >
                                <Settings AllowCellMerge="True" /> 
                          <DataItemTemplate>
                                    <dx:ASPxHyperLink ID="hpl_FormNo" runat="server" Text='<%# Eval("FormNo")%>' Cursor="pointer" ClientInstanceName='<%# "FormNo"+Container.VisibleIndex.ToString() %>'  
                                         NavigateUrl='<%# "/Platform/WorkFlowRun/Default.aspx?flowid=d9cb9476-13f9-48ec-a87e-5b84ca0790b0&display=1&state=edit&formid="+ Eval("FormNo")+"&gc="+ Eval("gc_version")+"&bz="+ Eval("bz_version") +"&domain="+ Eval("comp")+"&wlh="+ Eval("wlh")%>'
                                         Target="_blank"
                                        >                                        
                                    </dx:ASPxHyperLink>
                                </DataItemTemplate> 
                            </dx:GridViewDataTextColumn>
                             <dx:GridViewDataTextColumn Caption="物料号" FieldName="part_no" Width="80px" VisibleIndex="2">
                                <Settings AllowCellMerge="True" />
                            </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="项目号" FieldName="wlh" Width="80px" VisibleIndex="3">
                                <Settings AllowCellMerge="True" />
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="状态" FieldName="status" Width="80px" VisibleIndex="4">
                                <Settings AllowCellMerge="True" />
                            </dx:GridViewDataTextColumn>
                             <dx:GridViewDataTextColumn Caption="工程版本" FieldName="gc_version" Width="60px" VisibleIndex="5">
                                <Settings AllowCellMerge="True" />
                            </dx:GridViewDataTextColumn>
                             <dx:GridViewDataTextColumn Caption="包装版本" FieldName="bz_version" Width="60px" VisibleIndex="6">
                                <Settings AllowCellMerge="True" />
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="描述1" FieldName="wlmc" Width="100px" VisibleIndex="7">
                                <Settings AllowCellMerge="True" />
                            </dx:GridViewDataTextColumn>
                            
                            <dx:GridViewDataTextColumn Caption="描述2" FieldName="ms" Width="100px" VisibleIndex="8"><Settings AllowCellMerge="True" /> </dx:GridViewDataTextColumn>
                       <%--     <dx:GridViewDataTextColumn Caption="域" FieldName="comp" Width="40px" VisibleIndex="9"><Settings AllowCellMerge="True" /></dx:GridViewDataTextColumn>--%>
                            <dx:GridViewDataTextColumn Caption="地点" FieldName="site" Width="60px" VisibleIndex="10"><Settings AllowCellMerge="True" /></dx:GridViewDataTextColumn>
                        <%--    <dx:GridViewDataTextColumn Caption="分销点代码" FieldName="fxcode" Width="70px" VisibleIndex="2"><Settings AllowCellMerge="True" /></dx:GridViewDataTextColumn>--%>
                            <dx:GridViewDataTextColumn Caption="组" FieldName="cpgroup" Width="65px" VisibleIndex="11">
                                <Settings AllowCellMerge="True" />  
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="发运重量" FieldName="fyweight" Width="80px" VisibleIndex="12"></dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="发运单位" FieldName="fyunit" Width="60px" VisibleIndex="13"></dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="净重" FieldName="jzweight" Width="80px" VisibleIndex="14"></dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="净重单位" FieldName="jzunit" Width="60px" VisibleIndex="15"></dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="订货方法" FieldName="dhff" Width="65px" VisibleIndex="16"></dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="订单数量" FieldName="ddsl" Width="65px" VisibleIndex="17"></dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="订货周期" FieldName="dhperiod" Width="65px" VisibleIndex="18"></dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="采购提前期" FieldName="purchase_days" Width="80px" VisibleIndex="19"></dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="制造提前期" FieldName="make_days" Width="80px" VisibleIndex="20"></dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="采购员/计划员" FieldName="buyer_planner" Width="110px" VisibleIndex="21"></dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="采购/制造" FieldName="pt_pm_code" Width="60px" VisibleIndex="22"></dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="最小订单量" FieldName="quantity_min" Width="75px" VisibleIndex="23">
                               <%-- <PropertiesTextEdit DisplayFormatString="{0:N2}"></PropertiesTextEdit>--%>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="最大订单量" FieldName="quantity_max" Width="70px" VisibleIndex="24">
                               <%-- <PropertiesTextEdit DisplayFormatString="{0:N2}"></PropertiesTextEdit>--%>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="订单倍数" FieldName="ddbs" Width="65px" VisibleIndex="25">
                             <%--       <PropertiesTextEdit DisplayFormatString="{0:N2}"></PropertiesTextEdit>--%>
                            </dx:GridViewDataTextColumn>
                           <dx:GridViewDataTextColumn Caption="产品大类" FieldName="dl" Width="80px" VisibleIndex="26">
                            </dx:GridViewDataTextColumn>
                            
                        </Columns>
                        <Styles>
                            <Header BackColor="#99CCFF"></Header>
                            <FocusedRow BackColor="#99CCFF" ForeColor="#0000CC"></FocusedRow>
                            <Footer HorizontalAlign="Right"></Footer>
                        </Styles>
                    </dx:ASPxGridView>
                    <dx:ASPxGridViewExporter ID="ASPxGridViewExporter1" runat="server">
                    </dx:ASPxGridViewExporter>
                </td>
            </tr>
        </table>
    </div>

  

</asp:Content>

