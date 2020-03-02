<%@ Page Title="【签核完成的采购单处理】" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="PO_Deal_Form.aspx.cs" Inherits="Forms_PurChase_PO_Deal_Form" %>

<%@ Register Assembly="DevExpress.Web.v17.2, Version=17.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <script src="/Content/js/jquery.min.js"  type="text/javascript"></script>
    <script src="/Content/js/bootstrap.min.js"></script>
    <script src="/Content/js/layer/layer.js"></script>
    <script src="/Content/js/plugins/layer/laydate/laydate.js"></script>
    <script type="text/javascript">
        var UserId = '<%=UserId%>';
        var UserName = '<%=UserName%>';
        var DeptName = '<%=DeptName%>';

        $(document).ready(function () {
            $("#mestitle").html("【签核完成的采购单处理】");//<span class='h5' style='color:red'>10122 > 60天显示黄，> 75天显示红色 其他 > 30显示黄，> 45天显示红色</span>

            if (DeptName.indexOf("采购") != -1 || DeptName.indexOf("IT") != -1) {
                $('#btn_del').show();
            } else {
                $('#btn_del').hide();
            }

            setHeight();
            $(window).resize(function () {
                setHeight();
            });


            $('#btn_del').click(function () {
                if (grid_DK.GetSelectedRowCount() <= 0) { layer.alert("请选择一条记录!"); return; }

                grid_DK.GetSelectedFieldValues('po_id;PoNo;po_rowid', function GetVal(values) {

                    var ls_ids = "", ls_ponos = "";
                    for (var i = 0; i < values.length; i++) {
                        var ls_id = values[i][0];//== null ? "" : values[0][0];
                        var ls_pono = values[i][1];//== null ? "" : values[0][0];
                        ls_ids = ls_ids + ls_id + ",";
                        ls_ponos = ls_ids + "'" + ls_id + "',";
                    }
                    ls_ids = ls_ids.substr(0, ls_ids.length - 1);
                    ls_ponos = ls_ponos.substr(0, ls_ponos.length - 1);

                    $.ajax({
                        type: "post",
                        url: "PO_Deal_Form.aspx/deal",
                        data: "{'ids':'" + ls_ids + "','ponos':'" + ls_ponos + "'}",
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


        });
        
        function setHeight() {
            $("div[class=dxgvCSD]").css("height", ($(window).height() - $("#div_p").height() - 220) + "px");

            $("#MainContent_GV_PART").css("width", ($(window).width() - 10) + "px");
            $("div[class=dxgvCSD]").css("width", ($(window).width() - 10) + "px");

            $("#MainContent_GV_PART_DK").css("width", ($(window).width() - 10) + "px");
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
                    <td style="text-align:right;">采购工程师</td>
                    <td>
                        <asp:TextBox ID="txt_pur_user" runat="server" class="form-control" Width="120px" ReadOnly="true"></asp:TextBox>
                    </td>  
                    <td style="text-align:right;">&nbsp;&nbsp;创建日期：</td>
                    <td>
                        <asp:TextBox ID="StartDate" runat="server" class="form-control" Width="120px" 
                            onclick="laydate({type: 'date',format: 'YYYY/MM/DD',choose: function(dates){}});" />
                    </td>
                    <td>~</td>
                    <td>
                        <asp:TextBox ID="EndDate" runat="server" class="form-control" Width="120px"
                            onclick="laydate({type: 'date',format: 'YYYY/MM/DD',choose: function(dates){}});" />
                    </td>    
                    <td> 
                        &nbsp;
                        <asp:Button ID="Bt_select" runat="server" Text="查询" class="btn btn-large btn-primary" OnClick="Bt_select_Click" Width="70px" />                     
                        &nbsp;
                        <button id="btn_del" type="button" class="btn btn-primary btn-large"><i class="fa fa-check fa-fw"></i>&nbsp;作废</button>
                    </td> 
                </tr>
            </table>
        </div>
    </div>
    <div runat="server" id="DIV1" style="margin-left: 5px; margin-right: 5px; margin-bottom: 10px">
        <table>
            <tr>
                <td>
                    <dx:ASPxGridView ID="GV_PART_DK" ClientInstanceName="grid_DK" runat="server" KeyFieldName="po_id" AutoGenerateColumns="False"  
                             OnHtmlRowCreated="GV_PART_DK_HtmlRowCreated" OnPageIndexChanged="GV_PART_DK_PageIndexChanged" Width="1000px"><%--3030--%>
                        <ClientSideEvents EndCallback="function(s, e) { setHeight(); }" />
                        <SettingsBehavior AllowDragDrop="TRUE" AllowFocusedRow="false" AllowSelectByRowClick="false" ColumnResizeMode="Control" AutoExpandAllGroups="true" MergeGroupsMode="Always" SortMode="Value" />
                        <SettingsPager PageSize="100"></SettingsPager>
                        <Settings ShowFilterRow="True" ShowFilterRowMenu="True"   VerticalScrollBarMode="Visible" VerticalScrollBarStyle="Standard" VerticalScrollableHeight="500"
                                 ShowFilterRowMenuLikeItem="True"  ShowFooter="True"  HorizontalScrollBarMode="Auto" />
                        <Columns>
                            <dx:GridViewCommandColumn   ShowClearFilterButton="true" ShowSelectCheckbox="true" Name="Sel" Width="40" VisibleIndex="0"  SelectAllCheckboxMode="Page"  >
                            </dx:GridViewCommandColumn> 
                            <dx:GridViewDataDateColumn Caption="创建日期" FieldName="CreateDate" Width="90px" VisibleIndex="1" >
                                <PropertiesDateEdit DisplayFormatString="yyyy/MM/dd"></PropertiesDateEdit>
                            </dx:GridViewDataDateColumn>
                            <dx:GridViewDataTextColumn Caption="采购<br>类别" FieldName="PoType" Width="50px" VisibleIndex="2" />
                            <dx:GridViewDataTextColumn Caption="是否<br>已对接" FieldName="PZ_Status" Width="60px" VisibleIndex="2"/>
                            <dx:GridViewDataTextColumn Caption="QAD单号" FieldName="qad_pono" Width="80px" VisibleIndex="2"/>
                            <dx:GridViewDataTextColumn Caption="QAD<br>订单状态" FieldName="pod_status" Width="60px" VisibleIndex="2"/>
                            <dx:GridViewDataTextColumn Caption="PO<br>订单状态" FieldName="po_status" Width="60px" VisibleIndex="2"/>
                            <dx:GridViewDataTextColumn Caption="采购单号" FieldName="PoNo" Width="80px" VisibleIndex="3" >
                                <DataItemTemplate>
                                    <dx:ASPxHyperLink ID="hpl_pgi_no" runat="server" Text='<%# Eval("PoNo")%>' Cursor="pointer" ClientInstanceName='<%# "PoNo_"+Container.VisibleIndex.ToString() %>'
                                        NavigateUrl='<%# "/Platform/WorkFlowRun/Default.aspx?flowid=ce701853-e13b-4c39-9cd6-b97e18656d31&instanceid="+ Eval("PoNo")+"&groupid="+ Eval("GroupID")+"&display=1" %>'  
                                        Target="_blank">                                        
                                    </dx:ASPxHyperLink>
                                </DataItemTemplate> 
                                <Settings AllowAutoFilterTextInputTimer="False" />
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="行号" FieldName="po_rowid" Width="40" VisibleIndex="4" />
                            <dx:GridViewDataTextColumn Caption="供应商" FieldName="gys" Width="230px" VisibleIndex="5" />
                            <dx:GridViewDataTextColumn Caption="PR单号" FieldName="PRNo" Width="110px" VisibleIndex="7" > 
                                 <HeaderStyle BackColor="#F0E68C" />
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="PR<br>行号" FieldName="PRRowId" Width="40px" VisibleIndex="8" > 
                                 <HeaderStyle BackColor="#F0E68C" />
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="物料号" FieldName="wlh" Width="90px" VisibleIndex="9" > 
                                 <HeaderStyle BackColor="#F0E68C" />
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="物料名称" FieldName="wlmc" Width="150px" VisibleIndex="10" > 
                                 <HeaderStyle BackColor="#F0E68C" />
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="物料描述" FieldName="wlms" Width="200px" VisibleIndex="11" > 
                                 <HeaderStyle BackColor="#F0E68C" />
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="物料类别" FieldName="note2" Width="90px" VisibleIndex="12"  > 
                                 <HeaderStyle BackColor="#F0E68C" />
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="采购类别" FieldName="po_wltype" Width="90px" VisibleIndex="13" />
                            <dx:GridViewDataTextColumn Caption="币别" FieldName="currency" Width="50px" VisibleIndex="14" />
                            <dx:GridViewDataTextColumn Caption="目标单<br>价(未税)" FieldName="notax_targetPrice" Width="75px" VisibleIndex="15" > 
                                 <HeaderStyle BackColor="#F0E68C" />
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="数量" FieldName="PurQty" Width="50px" VisibleIndex="15"/>
                            <dx:GridViewDataTextColumn Caption="目标总<br>价(未税)" FieldName="notax_targetTotalPrice" Width="85px" VisibleIndex="15" > 
                                 <HeaderStyle BackColor="#F0E68C" />
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="推荐<br>供应商" FieldName="RecmdVendorName" Width="150px" VisibleIndex="16" > 
                                 <HeaderStyle BackColor="#F0E68C" />
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="历史单<br>价(未税)" FieldName="notax_historyPrice" Width="75px" VisibleIndex="17" > 
                                 <HeaderStyle BackColor="#F0E68C" />
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="采购单<br>价(未税)" FieldName="NoTaxPrice" Width="75px" VisibleIndex="18"> 
                                <PropertiesTextEdit DisplayFormatString="{0:N4}"></PropertiesTextEdit>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="采购总<br>价(未税)" FieldName="notax_TotalPrice" Width="85px" VisibleIndex="19"> 
                                <PropertiesTextEdit DisplayFormatString="{0:N4}"></PropertiesTextEdit>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="税率" FieldName="TaxRate" Width="40px" VisibleIndex="20"/>
                            <dx:GridViewDataTextColumn Caption="采购单<br>价(含税)" FieldName="TaxPrice" Width="75px" VisibleIndex="21"> 
                                <PropertiesTextEdit DisplayFormatString="{0:N4}"></PropertiesTextEdit>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="采购总<br>价(含税)" FieldName="TotalPrice" Width="85px" VisibleIndex="22">
                                <PropertiesTextEdit DisplayFormatString="{0:N4}"></PropertiesTextEdit>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="采购价格说明" FieldName="PriceDesc" Width="100px" VisibleIndex="23"/>
                            <dx:GridViewDataDateColumn Caption="要求<br>到货日期" FieldName="deliveryDate" Width="90px" VisibleIndex="24" >
                                <PropertiesDateEdit DisplayFormatString="yyyy/MM/dd"></PropertiesDateEdit>
                                <HeaderStyle BackColor="#F0E68C" />
                            </dx:GridViewDataDateColumn>
                            <dx:GridViewDataDateColumn Caption="计划到货日期" FieldName="PlanReceiveDate" Width="90px" VisibleIndex="25" >
                                <PropertiesDateEdit DisplayFormatString="yyyy/MM/dd"></PropertiesDateEdit>
                            </dx:GridViewDataDateColumn>
                            <dx:GridViewDataTextColumn Caption="采购工程师" FieldName="BuyerName" Width="70px" VisibleIndex="30" />
                            <dx:GridViewDataTextColumn Caption="po_id" FieldName="po_id" VisibleIndex="99" Width="0px"                                
                                 HeaderStyle-CssClass="hidden" CellStyle-CssClass="hidden" FooterCellStyle-CssClass="hidden"></dx:GridViewDataTextColumn>
                        </Columns>
                        <TotalSummary>
                            <dx:ASPxSummaryItem DisplayFormat="{0:N2}" FieldName="TotalPrice" ShowInColumn="TotalPrice" ShowInGroupFooterColumn="TotalPrice" SummaryType="Sum" />
                        </TotalSummary>
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

