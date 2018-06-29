<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="GYLX_Report_Query.aspx.cs" Inherits="Forms_PgiOp_GYLX_Report_Query" %>

<%@ Register Assembly="DevExpress.Web.v17.2, Version=17.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <script type="text/javascript" src="/Content/js/jquery.min.js"></script>
    <script type="text/javascript" src="/Content/js/jquery.cookie.min.js"></script>
    <script src="/Content/js/layer/layer.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#mestitle").text("【工艺路线查询】");

            $('#btn_add').click(function () {
                window.open('/Platform/WorkFlowRun/Default.aspx?flowid=ee59e0b3-d6a1-4a30-a3b4-65d188323134&appid=BDDCD717-2DD6-4D83-828C-71C92FFF6AE4')
            });

            $('#btn_edit').click(function () {
                if (grid.GetSelectedRowCount() != 1) { layer.alert("请选择一条记录!"); return; }

                grid.GetSelectedFieldValues("formno;pgi_no", function GetVal(values) {
                    //for (var i = 0; i < values.length; i++) { values[i][1]}
                    var formno = values[0][0];
                    var pgi_no = values[0][1];

                    $.ajax({
                        type: "post",
                        url: "GYLX_Report_Query.aspx/CheckData",
                        data: "{'pgi_no':'" + pgi_no + "'}",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        async: false,//默认是true，异步；false为同步，此方法执行完在执行下面代码
                        success: function (data) {
                            var obj = eval(data.d);

                            if (obj[0].re_flag != "") {
                                layer.alert(obj[0].re_flag);
                            } else {
                                window.open('/Platform/WorkFlowRun/Default.aspx?flowid=ee59e0b3-d6a1-4a30-a3b4-65d188323134&appid=BDDCD717-2DD6-4D83-828C-71C92FFF6AE4&state=edit&formno=' + formno + '&pgi_no=' + pgi_no);
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
            $("#MainContent_gv_DXMainTable tr[class*=DataRow]").each(function (index, item) {
                var rowspans = $(item).find("td:eq(1)").attr("rowspan");


                if (rowspans != undefined) {
                    $(item).find("td:first").attr("rowspan", rowspans);
                    for (var i = 1; i < rowspans; i++) {
                        $($("#MainContent_gv_DXMainTable tr[class*=DataRow]")[index + i]).find("td:first").hide();
                    }
                }
                
                //if (rowspans != undefined) {
                //    rows1 = $(item).find("td").length;
                //    rowsnext = $($("#MainContent_gv_DXMainTable tr[class*=DataRow]")[index + 1]).find("td").length;
                //    $(item).find("td:first").attr("rowspan", rowspans);
                //}
                //else {
                //    rowsnext = $(item).find("td").length;
                //    if (rows1 != rowsnext && index>0) {
                //        $(item).find("td:first").hide();
                //    }
                //}
            })            
        }

        function setHeight() {

            //alert($("div[class=dxgvCSD]").css("height"));
            //alert($(window).height());
            //alert($("#div_p").height());

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
            for(var i = 0; i < texts.length; i++) {
                item = checkListBox.FindItemByText(texts[i]);
                if(item != null)
                    actualValues.push(item.value);
            }
            return actualValues;
        }
    </script>

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    
    <div class="col-md-12" id="div_p" >
        <div class="row  row-container">            
            <div class="panel panel-info">
                <div class="panel-heading" data-toggle="collapse" data-target="#CPXX">
                    <strong>工艺工时查询</strong>
                </div>
                <div class="panel-body collapse in" id="CPXX" >
                    <div class="col-xs-12 col-sm-12  col-md-12 col-lg-12" style="font-size:13px;">
                        <table class="tblCondition" style=" border-collapse: collapse;">
                            <tr>
                                <td style="width:70px;">PGI项目号</td>
                                <td style="width:100px;">
                                    <asp:TextBox ID="txt_pgi_no" class="form-control input-s-sm" runat="server" Width="90px"></asp:TextBox>
                                </td>
                                <td style="width:70px;">客户零件号</td>
                                <td style="width:130px;">
                                    <asp:TextBox ID="txt_pn" class="form-control input-s-sm" runat="server" Width="120px"></asp:TextBox>
                                </td>    
                                <td style="width:70px;">当前版本</td>
                                <td style="width:90px;"> 
                                    <asp:DropDownList ID="ddl_ver" runat="server" class="form-control input-s-md " Width="80px">
                                        <asp:ListItem Value="">ALL</asp:ListItem>
                                        <asp:ListItem Value="当前" Selected="True">当前</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td style="width:70px;">工艺段</td>
                                <td style="width:90px;"> 
                                    <asp:DropDownList ID="ddl_typeno" runat="server" class="form-control input-s-md " Width="80px">
                                        <asp:ListItem Value="">ALL</asp:ListItem>
                                        <asp:ListItem Value="压铸">压铸</asp:ListItem>
                                        <asp:ListItem Value="机加">机加</asp:ListItem>
                                    </asp:DropDownList>
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
                </div>
            </div>            
        </div>
    </div>

    <div class="col-sm-12">
        <table>
            <tr>
                <td><%-- OnHtmlDataCellPrepared="gv_HtmlDataCellPrepared"--%>
                    <dx:ASPxGridView ID="gv" runat="server" KeyFieldName="id_dtl" AutoGenerateColumns="False" Width="1965px" OnPageIndexChanged="gv_PageIndexChanged"  ClientInstanceName="grid" 
                          OnCustomCellMerge="gv_CustomCellMerge">
                        <ClientSideEvents EndCallback="function(s, e) {           //if(MainContent_gv_DXMainTable.cpPageChanged == 1)     //grid为控件的客户端id
            	                   // window.alert('Page changed!');
                                    mergecells();setHeight();
        	                    }"  />
                        <SettingsPager PageSize="100" ></SettingsPager>
                        <Settings ShowFilterRow="True" ShowGroupPanel="false" ShowFilterRowMenu="True" ShowFilterRowMenuLikeItem="True" AutoFilterCondition="Contains" 
                            VerticalScrollBarMode="Visible" VerticalScrollBarStyle="Standard" VerticalScrollableHeight="600"  />
                        <SettingsBehavior AllowFocusedRow="True" ColumnResizeMode="Control"  />
                        <Columns>
                            <dx:GridViewCommandColumn   ShowClearFilterButton="true" ShowSelectCheckbox="true" Name="Sel" Width="40" VisibleIndex="0"    >
                                
                            </dx:GridViewCommandColumn>                           
                            <dx:GridViewDataTextColumn Caption="项目号" FieldName="pgi_no" Width="80px" VisibleIndex="1" >
                                <Settings AllowCellMerge="True" /> 
                                <DataItemTemplate>
                                    <dx:ASPxHyperLink ID="hpl_pgi_no" runat="server" Text='<%# Eval("pgi_no")%>' Cursor="pointer" ClientInstanceName='<%# "pgi_no"+Container.VisibleIndex.ToString() %>'
                                         NavigateUrl='<%# "/Forms/PgiOp/Rpt_ProductBom_Query.aspx?domain="+ Eval("domain")+"&pgino_pn="+ Eval("pgi_no")+"/"+ Eval(HttpUtility.UrlEncode("pn")) %>'  
                                         Target="_blank"
                                        >                                        
                                    </dx:ASPxHyperLink>
                                </DataItemTemplate> 
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="零件号" FieldName="pn" Width="110px" VisibleIndex="2">
                                <Settings AllowCellMerge="True" />
                            </dx:GridViewDataTextColumn>
                            
                            <dx:GridViewDataTextColumn Caption="工艺路<br />线版本" FieldName="ver" Width="50px" VisibleIndex="2"><Settings AllowCellMerge="True" /> </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="域" FieldName="domain" Width="40px" VisibleIndex="2"><Settings AllowCellMerge="True" /></dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="工艺流程" FieldName="pgi_no_t" Width="80px" VisibleIndex="3">
                                <Settings AllowCellMerge="True" />  
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="工序号" FieldName="op" Width="60px" VisibleIndex="4"></dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="工序名称" FieldName="op_desc" Width="130px" VisibleIndex="5"></dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="工序说明" FieldName="op_remark" Width="130px" VisibleIndex="6"></dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="设备<br />(工作中心名称)" FieldName="gzzx_desc" Width="100px" VisibleIndex="7"></dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="工作中<br />心代码" FieldName="gzzx" Width="55px" VisibleIndex="8"></dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="是否报<br />工(Y/N)" FieldName="IsBg" Width="55px" VisibleIndex="9"></dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="每次加<br />工数量" FieldName="JgNum" Width="50px" VisibleIndex="10"></dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="加工时<br />长(秒)" FieldName="JgSec" Width="50px" VisibleIndex="12"></dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="设备等待<br />时间(秒)" FieldName="WaitSec" Width="55px" VisibleIndex="12"></dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="装夹时<br />间(秒)" FieldName="ZjSecc" Width="50px" VisibleIndex="13"></dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="机器<br />台数" FieldName="JtNum" Width="40px" VisibleIndex="14"></dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="单台单件<br />工序工时(秒)" FieldName="TjOpSec" Width="70px" VisibleIndex="15">
                                <PropertiesTextEdit DisplayFormatString="{0:N2}"></PropertiesTextEdit>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="单件工<br />时(秒)" FieldName="JSec" Width="50px" VisibleIndex="16">
                                <PropertiesTextEdit DisplayFormatString="{0:N2}"></PropertiesTextEdit>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="单件工时<br />(小时)" FieldName="JHour" Width="65px" VisibleIndex="17">
                                    <PropertiesTextEdit DisplayFormatString="{0:N5}"></PropertiesTextEdit>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="单台需<br />要人数" FieldName="col1" Width="55px" VisibleIndex="18"></dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="本工序一人<br />操作台数" FieldName="col2" Width="65px" VisibleIndex="19"></dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="本产品设<br />备占用率" FieldName="EquipmentRate" Width="65px" VisibleIndex="19"></dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="单台83%<br />产量" FieldName="col3" Width="55px" VisibleIndex="20"></dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="一人83%<br />产量" FieldName="col4" Width="55px" VisibleIndex="21"></dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="83%班<br />产量" FieldName="col5" Width="50px" VisibleIndex="22"></dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="单人报<br />工数量" FieldName="col6" Width="50px" VisibleIndex="23"></dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="单人产<br />出工时" FieldName="col7" Width="50px" VisibleIndex="24">
                                <PropertiesTextEdit DisplayFormatString="{0:N2}"></PropertiesTextEdit>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="产品工程师" FieldName="product_user" Width="90px" VisibleIndex="25">
                                <Settings AllowCellMerge="True" />
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="表单编号" FieldName="formno" Width="90px" VisibleIndex="26">
                                <Settings AllowCellMerge="True" />
                            </dx:GridViewDataTextColumn>
                            <%--
                            <dx:GridViewDataTextColumn Caption="创建时间" FieldName="createdate" Width="130px" VisibleIndex="26"></dx:GridViewDataTextColumn>--%>
                            <dx:GridViewDataTextColumn Caption="id_dtl" FieldName="id_dtl" VisibleIndex="99"
                                 HeaderStyle-CssClass="hidden" CellStyle-CssClass="hidden" FooterCellStyle-CssClass="hidden"></dx:GridViewDataTextColumn>
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

