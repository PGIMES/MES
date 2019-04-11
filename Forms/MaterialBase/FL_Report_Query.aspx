<%@ Page Title="辅料查询" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="FL_Report_Query.aspx.cs" Inherits="Forms_MaterialBase_FL_Report_Query" %>

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
            $("#mestitle").text("【辅料查询】");

            $('#btn_add').click(function () {
                window.open('/Platform/WorkFlowRun/Default.aspx?flowid=9d591dd9-b615-4e8f-b2f8-d3a7161af952&appid=')
            });

            $('#btn_edit').click(function () {
                if (selList.GetItemCount() != 1) { layer.alert("请选择一条记录!"); return; }

                var item = selList.GetItem(0);
                grid.GetRowValues(parseInt(item.value), 'formno;domain;wlh', OnGetRowValues);

            });

//            $('#btn_edit').click(function () {
//                window.open('/Platform/WorkFlowRun/Default.aspx?flowid=9d591dd9-b615-4e8f-b2f8-d3a7161af952&state=edit&formno=FL8120001&domain=200&wlh=Z10014912')
//            });

            setHeight();

            $(window).resize(function () {
                setHeight();
            });


        });


        function setHeight() {
            $("div[class=dxgvCSD]").css("height", ($(window).height() - $("#div_p").height() - 180) + "px");
        }

        function RefreshRow(vi) {
            var chk = eval('chk' + vi);
            selList.BeginUpdate();
            try {
                var count = selList.GetItemCount();
                for (var i = count - 1; i >= 0; i--) {
                    var item = selList.GetItem(i);
                    if (item.text == vi.toString() && item.value == vi.toString()) {
                        selList.RemoveItem(i);
                    }
                }

                if (chk.GetValue()) {
                    selList.AddItem(vi.toString(), vi.toString());
                }

            } finally {
                selList.EndUpdate();
            }
        }

        function OnGetRowValues(values) {
            var formno = values[0];
            var comp = values[1];
            var part_no = values[2];
      

            $.ajax({
                type: "post",
                url: "FL_Report_Query.aspx/CheckData",
                data: "{'part_no':'" + part_no + "','comp':'" + comp + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false, //默认是true，异步；false为同步，此方法执行完在执行下面代码
                success: function (data) {
                    var obj = eval(data.d);

                    if (obj[0].re_flag != "") {
                        layer.alert(obj[0].re_flag);
                    } else {
                        window.open('/Platform/WorkFlowRun/Default.aspx?flowid=9d591dd9-b615-4e8f-b2f8-d3a7161af952&state=edit&formno=' + formno + '&domain=' + comp + '&wlh=' + part_no);
                    }
                }

            });

        }


        function clear() {
            selList.BeginUpdate();
            try {
                selList.ClearItems();

            } finally {
                selList.EndUpdate();
            }
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
                <td style="width:70px;">描述</td>
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

        <div style="display:none;"><dx:ASPxListBox ID="ASPxListBox1" ClientInstanceName="selList" runat="server" Height="150px" Width="500px" ValueType="System.String"></dx:ASPxListBox></div>
    <div class="col-sm-12">
        <table>
            <tr>
                <td><%-- OnHtmlDataCellPrepared="gv_HtmlDataCellPrepared"--%>
                    <dx:ASPxGridView ID="gv" runat="server" KeyFieldName="id" AutoGenerateColumns="False" Width="1500px" OnPageIndexChanged="gv_PageIndexChanged"  ClientInstanceName="grid" 
                          OnCustomCellMerge="gv_CustomCellMerge">
                        <ClientSideEvents EndCallback="function(s, e) {           //if(MainContent_gv_DXMainTable.cpPageChanged == 1)     //grid为控件的客户端id
            	                   // window.alert('Page changed!');
                                    setHeight();
        	                    }"  />
                        <SettingsPager PageSize="200" ></SettingsPager>
                        <Settings ShowFilterRow="True" ShowGroupPanel="false" ShowFilterRowMenu="True" ShowFilterRowMenuLikeItem="True" AutoFilterCondition="Contains" 
                            VerticalScrollBarMode="Visible" VerticalScrollBarStyle="Standard" VerticalScrollableHeight="600"  />
                        <SettingsBehavior AllowFocusedRow="True" ColumnResizeMode="Control"  />
                        <Columns>
           
                              <dx:GridViewDataTextColumn FieldName="" Width="40px" VisibleIndex="0" Name="chk">
                                <Settings  />
                                <DataItemTemplate>
                                    <dx:ASPxCheckBox ID="ASPxCheckBox1" runat="server" ClientInstanceName='<%# "chk"+Container.VisibleIndex.ToString() %>'
                                         ClientSideEvents-CheckedChanged='<%# "function(s,e){RefreshRow("+Container.VisibleIndex+");}" %>'></dx:ASPxCheckBox>
                                </DataItemTemplate>
                            </dx:GridViewDataTextColumn>                   
                          <%--  <dx:GridViewDataTextColumn Caption="表单号" FieldName="formno" Width="130px" VisibleIndex="1"   >
                               
                          <DataItemTemplate>--%>
                            <%--        <dx:ASPxHyperLink ID="hpl_FormNo" runat="server" Text='<%# Eval("FormNo")%>' Cursor="pointer" ClientInstanceName='<%# "FormNo"+Container.VisibleIndex.ToString() %>'  
                                         NavigateUrl='<%# "/Platform/WorkFlowRun/Default.aspx?flowid=9d591dd9-b615-4e8f-b2f8-d3a7161af952&state=edit&formno="+ Eval("FormNo")+"&domain="+ Eval("domain")+"&wlh="+ Eval("wlh")%>'
                                         Target="_blank"
                                        >                                        
                                    </dx:ASPxHyperLink>--%>

                                    
                           <%--     </DataItemTemplate> 
                            </dx:GridViewDataTextColumn>--%>
                             
                                <dx:GridViewDataTextColumn Caption="表单号" FieldName="formno" Width="80px" VisibleIndex="2">
                            </dx:GridViewDataTextColumn>

                             <dx:GridViewDataTextColumn Caption="物料号" FieldName="wlh" Width="80px" VisibleIndex="2">
                            </dx:GridViewDataTextColumn>
                               <dx:GridViewDataTextColumn Caption="公司别" 
                                FieldName="domain" Width="60px" VisibleIndex="3">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="状态" FieldName="status" Width="50px" VisibleIndex="4">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="描述1" FieldName="wlmc" Width="100px" VisibleIndex="5">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="描述2" FieldName="ms" Width="100px" VisibleIndex="6"></dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="净重" FieldName="jzweight" Width="60px" VisibleIndex="7">
                            <PropertiesTextEdit DisplayFormatString="{0:N6}"></PropertiesTextEdit>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="净重单位" FieldName="jzunit" Width="60px" VisibleIndex="8"></dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="订单数量" FieldName="ddsl" Width="65px" VisibleIndex="9">
                            <PropertiesTextEdit DisplayFormatString="{0:N2}"></PropertiesTextEdit>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="订货周期" FieldName="dhperiod" Width="65px" VisibleIndex="10"></dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="采购提前期" FieldName="purchase_days" Width="70px" VisibleIndex="11"></dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="采购员" FieldName="buyer_planner" Width="60px" VisibleIndex="12"></dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="最小订单量" FieldName="quantity_min" Width="75px" VisibleIndex="13">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="最大订单量" FieldName="quantity_max" Width="70px" VisibleIndex="14">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="订单倍数" FieldName="ddbs" Width="65px" VisibleIndex="15">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="安全库存" FieldName="aqkc" 
                                Width="65px" VisibleIndex="16">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="产品线" FieldName="line" 
                                Width="100px" VisibleIndex="17">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="材料一" FieldName="cailiao1" 
                                Width="80px" VisibleIndex="18">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="材料二" FieldName="cailiao2" 
                                Width="80px" VisibleIndex="19">
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

