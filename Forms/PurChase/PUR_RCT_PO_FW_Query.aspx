<%@ Page Title="未匹配收货报表-费用服务" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="PUR_RCT_PO_FW_Query.aspx.cs" Inherits="Forms_PurChase_PUR_RCT_PO_FW_Query" %>

<%@ Register Assembly="DevExpress.Web.v17.2, Version=17.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <script src="/Content/js/jquery.min.js"  type="text/javascript"></script>
    <script src="/Content/js/bootstrap.min.js"></script>
    <script src="/Content/js/plugins/layer/layer.min.js" type="text/javascript"></script>
    <script src="/Content/js/plugins/layer/laydate/laydate.js"></script>
    <script src="/Content/js/layer/layer.js"></script>
    <script type="text/javascript">
        var UserId = '<%=UserId%>';
        var UserName = '<%=UserName%>';
        var DeptName = '<%=DeptName%>';

        $(document).ready(function () {
            $("#mestitle").text("【未匹配收货报表-费用服务】");

            if (DeptName.indexOf("采购") != -1 || DeptName.indexOf("IT") != -1) {
                $('#btn_po').show();
            } else {
                $('#btn_po').hide();
            }
            if (DeptName.indexOf("财务") != -1 || DeptName.indexOf("IT") != -1) {
                $('#btn_fw').show();
            } else {
                $('#btn_fw').hide();
            }

            $('#btn_po').click(function () {
                if (grid.GetSelectedRowCount() <= 0) { layer.alert("请选择一条记录!"); return; }

                grid.GetSelectedFieldValues('rctno;PONo;OptionType', function GetVal(values) {

                    var ls_rctnos = ""; var msg = "";
                    for (var i = 0; i < values.length; i++) {
                        var ls_rctno = values[i][0];//== null ? "" : values[0][0];
                        var ls_OptionType = values[i][2] == null ? "" : values[i][2];

                        if (ls_OptionType != "") {
                            msg = msg + "验收单" + values[i][0] + ls_OptionType + "，不能重复确认！<br />";
                        } else {
                            ls_rctnos = ls_rctnos + ls_rctno + "|";
                        }
                    }
                    if (msg != "") {
                        layer.alert(msg);
                        return;
                    }

                    $.ajax({
                        type: "post",
                        url: "PUR_RCT_PO_FW_Query.aspx/po_deal",
                        data: "{'rctno':'" + ls_rctnos + "'}",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        async: false,//默认是true，异步；false为同步，此方法执行完在执行下面代码
                        success: function (data) {
                            var obj = eval(data.d);
                            layer.alert(obj[0].re_flag, function (index) { location.reload(); })
                            return;
                        }

                    });                   

                });
            });

            $('#btn_fw').click(function () {
                if (grid.GetSelectedRowCount() <= 0) { layer.alert("请选择一条记录!"); return; }

                grid.GetSelectedFieldValues('rctno;PONo;OptionType', function GetVal(values) {
                    var ls_rctnos = ""; var msg = "";
                    for (var i = 0; i < values.length; i++) {
                        var ls_rctno = values[i][0];//== null ? "" : values[0][0];
                        var ls_OptionType = values[i][2] == null ? "" : values[i][2];

                        if (ls_OptionType == "已匹配") {
                            msg = msg + "验收单" + values[i][0] + ls_OptionType + ",不能重复确认！<br />";
                        } else if (ls_OptionType == "") {
                            msg = msg + "验收单" + values[i][0] + ",采购还未确认,不能确认！<br />";
                        } else {
                            ls_rctnos = ls_rctnos + ls_rctno + "|";
                        }
                    }
                    if (msg != "") {
                        layer.alert(msg);
                        return;
                    }
                    $.ajax({
                        type: "post",
                        url: "PUR_RCT_PO_FW_Query.aspx/fw_deal",
                        data: "{'rctno':'" + ls_rctnos + "',domain:'" + $("#MainContent_PoDomain").val() + "'}",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        async: false,//默认是true，异步；false为同步，此方法执行完在执行下面代码
                        success: function (data) {
                            var obj = eval(data.d);
                            layer.alert(obj[0].re_flag, function (index) { location.reload(); })
                            return;
                        }

                    });

                });
            });

            setHeight();
            $(window).resize(function () {
                setHeight();
            });
        });

        function setHeight() {
            $("div[class=dxgvCSD]").css("height", ($(window).height() - $("#div_p").height() - 250) + "px");

            $("#MainContent_GV_PART").css("width", ($(window).width() - 10) + "px")
            $("div[class=dxgvCSD]").css("width", ($(window).width() - 10) + "px");
        }
        	
    </script>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

    <div class="panel-body" id="div_p">
        <div class="col-sm-12">
            <table style="line-height:40px">
                <tr>
                    <td style="text-align:right;">域</td>
                    <td>
                        <asp:DropDownList ID="PoDomain" runat="server" AutoPostBack="true" OnTextChanged="PoDomain_TextChanged" Width="95px"  Height="32px"  class="form-control input-s-md ">
                            <asp:ListItem Text="200" Value="200"></asp:ListItem>
                            <asp:ListItem Text="100" Value="100"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td style="text-align:right;">&nbsp;&nbsp;供应商</td>
                    <td colspan="3">
                        <dx:ASPxComboBox ID="PoVendorId" runat="server" ValueType="System.String" Width="250px" Height="32px"  class="form-control input-s-md " DropDownStyle="DropDown">
                        </dx:ASPxComboBox>
                    </td>                   
                    <td style="text-align:right;">&nbsp;&nbsp;类型</td>
                    <td>
                        <asp:DropDownList ID="ddlType" runat="server" Width="140px"  Height="32px"  class="form-control input-s-md ">
                            <asp:ListItem Text="All" Value="All"></asp:ListItem>
                            <asp:ListItem Text="未开票" Value=""></asp:ListItem>
                            <asp:ListItem Text="已开票" Value="已开票"></asp:ListItem>
                            <asp:ListItem Text="未匹配" Value="未匹配"></asp:ListItem>
                            <asp:ListItem Text="已匹配" Value="已匹配"></asp:ListItem>
                        </asp:DropDownList>
                    </td>  
                     <td></td> <td></td>  
                </tr>
                <tr>
                    <td style="text-align:right;">&nbsp;&nbsp;订单号</td>
                    <td>
                        <asp:TextBox ID="txtPoNo" class="form-control" runat="server" Width="95px"></asp:TextBox>
                    </td>
                    <td style="text-align:right;">&nbsp;&nbsp;验收日期</td>
                    <td >
                        <asp:TextBox ID="txtCheckDateFrom" class="form-control" onclick="laydate()" runat="server" Width="110px"></asp:TextBox>
                    </td>
                    <td>~</td>
                    <td>
                        <asp:TextBox ID="txtCheckDateTo" class="form-control" onclick="laydate()" runat="server" Width="110px"></asp:TextBox>
                    </td>    
                    <td style="text-align:right;">&nbsp;&nbsp;验收单号</td>
                    <td>
                        <asp:TextBox ID="txtRCTNo" class="form-control" runat="server" Width="140px"></asp:TextBox>
                    </td>
                                           
                    <td> 
                        &nbsp;
                        <asp:Button ID="Bt_select" runat="server" Text="查询" class="btn btn-large btn-primary" OnClick="Bt_select_Click" Width="70px" /> 
                        &nbsp;
                        <asp:Button ID="Bt_Export" runat="server" class="btn btn-large btn-primary" OnClick="Bt_Export_Click" Text="导出" Width="70px" />
                        &nbsp;
                        <button id="btn_po" type="button" class="btn btn-primary btn-large"><i class="fa fa-check fa-fw"></i>&nbsp;采购确认</button>   
                        &nbsp;
                        <button id="btn_fw" type="button" class="btn btn-primary btn-large"><i class="fa fa-check fa-fw"></i>&nbsp;财务确认</button>   
                    </td> 
                </tr>
                </table>
        </div>
    </div>
    <div runat="server" id="DIV1" style="margin-left: 5px; margin-right: 5px; margin-bottom: 10px">
        <table>
            <tr>
                <td>
                    <dx:ASPxGridView ID="GV_PART" ClientInstanceName="grid" runat="server" KeyFieldName="rctno" AutoGenerateColumns="False"  
                             OnPageIndexChanged="GV_PART_PageIndexChanged" OnHtmlRowCreated="GV_PART_HtmlRowCreated" Width="1000px"><%--3030--%>
                        <ClientSideEvents EndCallback="function(s, e) { setHeight(); }" />
                        <SettingsBehavior AllowDragDrop="TRUE" AllowFocusedRow="false" AllowSelectByRowClick="false" ColumnResizeMode="Control" AutoExpandAllGroups="true" MergeGroupsMode="Always" SortMode="Value" />
                        <SettingsPager PageSize="100"></SettingsPager>
                        <Settings ShowFilterRow="True" ShowFilterRowMenu="True"   VerticalScrollBarMode="Visible" VerticalScrollBarStyle="Standard" VerticalScrollableHeight="500"
                                 ShowFilterRowMenuLikeItem="True"  ShowFooter="True"  HorizontalScrollBarMode="Auto" />
                        <Columns>
                            <dx:GridViewCommandColumn   ShowClearFilterButton="true" ShowSelectCheckbox="true" Name="Sel" Width="40" VisibleIndex="0"  SelectAllCheckboxMode="Page"  >
                            </dx:GridViewCommandColumn> 
                            <dx:GridViewDataDateColumn Caption="采购提交日期" FieldName="PO_tj_time" Width="90px" VisibleIndex="1">
                                <PropertiesDateEdit DisplayFormatString="yyyy/MM/dd"></PropertiesDateEdit>
                            </dx:GridViewDataDateColumn>
                            <dx:GridViewDataTextColumn Caption="类型" FieldName="OptionType" Width="60px" VisibleIndex="2" />
                            <dx:GridViewDataDateColumn Caption="财务确认日期" FieldName="fw_qr_time" Width="90px" VisibleIndex="3" >
                                <PropertiesDateEdit DisplayFormatString="yyyy/MM/dd"></PropertiesDateEdit>
                            </dx:GridViewDataDateColumn>
                            <dx:GridViewDataTextColumn Caption="QAD发票号" FieldName="qad_fp_no" Width="80px" VisibleIndex="4" />
                            <dx:GridViewDataTextColumn Caption="供应商" FieldName="PoVendorId" Width="60px" VisibleIndex="5" />
                            <dx:GridViewDataTextColumn Caption="供应商名称" FieldName="PoVendorName" Width="180px" VisibleIndex="6" />
                            <dx:GridViewDataTextColumn Caption="订单" FieldName="PONo" Width="70px" VisibleIndex="7" >                                
                                <DataItemTemplate>
                                    <dx:ASPxHyperLink ID="hpl_PONo" runat="server" Text='<%# Eval("PONo")%>' Cursor="pointer" ClientInstanceName='<%# "PONo_"+Container.VisibleIndex.ToString() %>'
                                         NavigateUrl='<%# "/Platform/WorkFlowRun/Default.aspx?flowid=ce701853-e13b-4c39-9cd6-b97e18656d31&instanceid="+ Eval("PONo")+"&groupid="+ Eval("po_GroupID")+"&display=1" %>'  
                                            Target="_blank">                                        
                                    </dx:ASPxHyperLink>
                                </DataItemTemplate> 
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="采购订单项" FieldName="porowid" Width="70px" VisibleIndex="8" />
                            <dx:GridViewDataTextColumn Caption="验收单号" FieldName="rctno" Width="100px" VisibleIndex="9" >                                
                                <DataItemTemplate>
                                    <dx:ASPxHyperLink ID="hpl_rctno" runat="server" Text='<%# Eval("rctno")%>' Cursor="pointer" ClientInstanceName='<%# "rctno_"+Container.VisibleIndex.ToString() %>'
                                         NavigateUrl='<%# "/Platform/WorkFlowRun/Default.aspx?flowid=97b862f1-0fd8-4626-80dc-5a8afc57f61a&instanceid="+ Eval("rctno")+"&groupid="+ Eval("rct_GroupID")+"&display=1" %>'  
                                            Target="_blank">                                        
                                    </dx:ASPxHyperLink>
                                </DataItemTemplate> 
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="验收日期" FieldName="checkdate" Width="80px" VisibleIndex="10" />
                            <dx:GridViewDataTextColumn Caption="M类物料名称" FieldName="wlmc" Width="150px" VisibleIndex="11" />
                            <dx:GridViewDataTextColumn Caption="M类物料描述" FieldName="wlms" Width="180px" VisibleIndex="12" />
                            <dx:GridViewDataTextColumn Caption="订单数量" FieldName="PurQty" Width="60px" VisibleIndex="13" />
                            <%--<dx:GridViewDataTextColumn Caption="累计收货数量" FieldName="" Width="60px" VisibleIndex="14" />--%>
                            <dx:GridViewDataTextColumn Caption="验收数量" FieldName="checkqty" Width="60px" VisibleIndex="15" />
                            <dx:GridViewDataTextColumn Caption="已匹配数量" FieldName="pipei_qty" Width="60px" VisibleIndex="16" />
                            <dx:GridViewDataTextColumn Caption="未匹配数量" FieldName="no_pipei_qty" Width="60px" VisibleIndex="17" />
                            <dx:GridViewDataTextColumn Caption="单价" FieldName="NoTaxPrice" Width="70px" VisibleIndex="18" />
                            <dx:GridViewDataTextColumn Caption="采购金额合计" FieldName="notax_TotalPrice" Width="80px" VisibleIndex="19" />
                            <dx:GridViewDataTextColumn Caption="税金额N" FieldName="TaxRatePrice" Width="80px" VisibleIndex="20" />
                            <dx:GridViewDataTextColumn Caption="税款合计N" FieldName="TotalPrice" Width="80px" VisibleIndex="21" />
                            <dx:GridViewDataTextColumn Caption="总账账户" FieldName="kjkm_code" Width="80px" VisibleIndex="22" />
                            <dx:GridViewDataTextColumn Caption="总账描述" FieldName="kjkm_name" Width="250px" VisibleIndex="23" />
                            <dx:GridViewDataTextColumn Caption="请购类别" FieldName="PRType" Width="80px" VisibleIndex="24" /> 
                            <dx:GridViewDataTextColumn Caption="物料类别" FieldName="wltype" Width="100px" VisibleIndex="25" />
                            <dx:GridViewDataTextColumn Caption="物料用途" FieldName="note" Width="100px" VisibleIndex="26" />
                            <dx:GridViewDataTextColumn Caption="外币金额" FieldName="ExchangeRatePrice" Width="70px" VisibleIndex="27" />
                            <dx:GridViewDataTextColumn Caption="兑换率2" FieldName="ExchangeRate" Width="50px" VisibleIndex="28" />
                            <dx:GridViewDataTextColumn Caption="货币" FieldName="currency" Width="40px" VisibleIndex="29" />
                            <dx:GridViewDataTextColumn Caption="应纳税" FieldName="ishs" Width="45px" VisibleIndex="30" />
                            <dx:GridViewDataTextColumn Caption="税率N" FieldName="TaxRate" Width="45px" VisibleIndex="31" />
                            <dx:GridViewDataTextColumn Caption="请购申请部门" FieldName="DeptName" Width="80px" VisibleIndex="32" />
                            <dx:GridViewDataTextColumn Caption="请购成本部门" FieldName="costcentre_pr" Width="80px" VisibleIndex="32" />
                            <dx:GridViewDataTextColumn Caption="请购成本部门" FieldName="costcentre_pr_name" Width="130px" VisibleIndex="32" />
                            <dx:GridViewDataTextColumn Caption="请购申请人" FieldName="CreateByName" Width="70px" VisibleIndex="33" />
                            <dx:GridViewDataTextColumn Caption="验收人" FieldName="checkbyname" Width="70px" VisibleIndex="34" />
                            <dx:GridViewDataTextColumn Caption="请购归属部门" FieldName="applydept" Width="130px" VisibleIndex="35" />
                            <dx:GridViewDataTextColumn Caption="用于产品/项目" FieldName="usefor" Width="150px" VisibleIndex="36" />
                            
                            <%--<dx:GridViewDataTextColumn Caption="项目编码" FieldName="" Width="60px" VisibleIndex="36" />
                            <dx:GridViewDataTextColumn Caption="直属组织" FieldName="" Width="60px" VisibleIndex="37" />
                            <dx:GridViewDataTextColumn Caption="负责车间" FieldName="" Width="60px" VisibleIndex="38" />
                            <dx:GridViewDataTextColumn Caption="XMBH" FieldName="" Width="60px" VisibleIndex="39" />
                            <dx:GridViewDataTextColumn Caption="XMMS" FieldName="" Width="60px" VisibleIndex="40" />
                            <dx:GridViewDataTextColumn Caption="XMBZ" FieldName="" Width="60px" VisibleIndex="41" />--%>
                            <%--<dx:GridViewDataTextColumn Caption="po_GroupID" FieldName="po_GroupID" VisibleIndex="98"
                                 HeaderStyle-CssClass="hidden" CellStyle-CssClass="hidden" FooterCellStyle-CssClass="hidden" FilterCellStyle-CssClass="hidden">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="rct_GroupID" FieldName="rct_GroupID" VisibleIndex="99"
                                 HeaderStyle-CssClass="hidden" CellStyle-CssClass="hidden" FooterCellStyle-CssClass="hidden" FilterCellStyle-CssClass="hidden">
                            </dx:GridViewDataTextColumn>--%>
                        </Columns>
                        <TotalSummary>
                            <dx:ASPxSummaryItem DisplayFormat="<font color='red' Size='2'>{0:N2}</font>" FieldName="notax_TotalPrice" ShowInColumn="notax_TotalPrice" ShowInGroupFooterColumn="notax_TotalPrice" SummaryType="Sum" />
                             <dx:ASPxSummaryItem DisplayFormat="<font color='red' Size='2'>{0:N2}</font>" FieldName="TaxRatePrice" ShowInColumn="TaxRatePrice" ShowInGroupFooterColumn="TaxRatePrice" SummaryType="Sum" />
                             <dx:ASPxSummaryItem DisplayFormat="<font color='red' Size='2'>{0:N2}</font>" FieldName="TotalPrice" ShowInColumn="TotalPrice" ShowInGroupFooterColumn="TotalPrice" SummaryType="Sum" />
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

