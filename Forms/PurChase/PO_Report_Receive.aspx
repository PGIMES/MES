<%@ Page Title="未收货查询" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="PO_Report_Receive.aspx.cs" Inherits="Forms_PurChase_PO_Report_Receive" %>

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
        $(document).ready(function () {
            $("#mestitle").text("【未收货查询】");

            $('#btn_sure_receive').click(function () {
                if (grid.GetSelectedRowCount() != 1) { layer.alert("请选择一条记录!"); return; }

                grid.GetSelectedFieldValues('PoNo;po_rowid;PurQty;qty_dsh;SysContractNo', function GetVal(values) {
                    if (values.length > 0) {
                        var ls_pono = values[0][0] == null ? "" : values[0][0];
                        var ls_rowid = values[0][1] == null ? "" : values[0][1];
                        var ls_purqty = values[0][2];
                        var ls_qty_dsh = values[0][3];
                        var ls_syscontractno = values[0][4];

                        var url = "PO_Confirm_Receive.aspx?domain=" + $("#MainContent_ddl_domain").val()
                            + "&pono=" + ls_pono + "&rowid=" + ls_rowid + "&purqty=" + ls_purqty + "&qty_dsh=" + ls_qty_dsh
                            + "&syscontractno=" + ls_syscontractno;
                        //alert(url);
                        layer.open({
                            title: '合同类收货确认',
                            closeBtn: 2,
                            type: 2,
                            area: ['500px', '500px'],
                            fixed: false, //不固定
                            maxmin: true, //开启最大化最小化按钮
                            content: url
                        });
                    }
                });
            });

            setHeight();
            $(window).resize(function () {
                setHeight();
            });
        });
        function setHeight() {
            $("div[class=dxgvCSD]").css("height", ($(window).height() - $("#div_p").height() - 250) + "px");
        }
        	
    </script>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
     
    <div class="panel-body" id="div_p">
        <div class="col-sm-12">
            <table>
                <tr>
                    <td>域</td>
                    <td >
                        <asp:DropDownList ID="ddl_domain" runat="server" class="form-control input-s-md " Width="100px">
                            <asp:ListItem Value="200">200</asp:ListItem>
                            <asp:ListItem Value="100">100</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <%--<td>创建日期：</td>
                    <td >
                        <asp:TextBox ID="txtDateFrom" class="form-control" onclick="laydate()" runat="server" Width="100px"></asp:TextBox>
                    </td>
                    <td>~</td>
                    <td >
                        <asp:TextBox ID="txtDateTo" class="form-control" onclick="laydate()" runat="server" Width="100px"></asp:TextBox>
                    </td>      --%>      
                    <td> 
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="Bt_select" runat="server" Text="查询" class="btn btn-large btn-primary" OnClick="Button1_Click" Width="100px" /> 
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="Bt_Export" runat="server" class="btn btn-large btn-primary" OnClick="Bt_Export_Click" Text="导出" Width="100px" />
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;  
                        <button id="btn_sure_receive" type="button" class="btn btn-primary btn-large"><i class="fa fa-check fa-fw"></i>&nbsp;确认收货</button>   
                    </td>
                </tr>
            </table>
        </div>
    </div>

    <div runat="server" id="DIV1" style="margin-left: 5px; margin-right: 5px; margin-bottom: 10px">
        <table>
            <tr>
                <td>
                    <dx:ASPxGridView ID="GV_PART" ClientInstanceName="grid" runat="server" KeyFieldName="SysContractNo;PoNo;po_rowid" AutoGenerateColumns="False"  
                             OnPageIndexChanged="GV_PART_PageIndexChanged" OnHtmlRowCreated="GV_PART_HtmlRowCreated" Width="1810px">
                        <ClientSideEvents EndCallback="function(s, e) { setHeight(); }" />
                        <SettingsBehavior AllowDragDrop="TRUE" AllowFocusedRow="false" AllowSelectByRowClick="false" ColumnResizeMode="Control" AutoExpandAllGroups="true" MergeGroupsMode="Always" SortMode="Value" />
                        <SettingsPager PageSize="100"></SettingsPager>
                        <Settings ShowFilterRow="True" ShowFilterRowMenu="True"   VerticalScrollBarMode="Visible" VerticalScrollBarStyle="Standard" VerticalScrollableHeight="500"
                                 ShowFilterRowMenuLikeItem="True"  ShowFooter="True"  />
                        <Columns>
                            <dx:GridViewCommandColumn   ShowClearFilterButton="true" ShowSelectCheckbox="true" Name="Sel" Width="40" VisibleIndex="0"    >
                            </dx:GridViewCommandColumn> 
                            <dx:GridViewDataTextColumn Caption="系统合同号" FieldName="SysContractNo" Width="100px" VisibleIndex="1" />
                            <dx:GridViewDataTextColumn Caption="合同名称" FieldName="ContractName" Width="300px" VisibleIndex="2" />
                            <dx:GridViewDataTextColumn Caption="采购单号" FieldName="PoNo" Width="70px" VisibleIndex="3" >
                                <%--<DataItemTemplate>
                                    <dx:ASPxHyperLink ID="hpl_pgi_no" runat="server" Text='<%# Eval("PoNo")%>' Cursor="pointer" ClientInstanceName='<%# "PoNo_"+Container.VisibleIndex.ToString() %>'
                                         NavigateUrl='<%# "/Platform/WorkFlowRun/Default.aspx?flowid=ce701853-e13b-4c39-9cd6-b97e18656d31&appid=7d6cf334-0227-4fcd-9faf-c2536d10cf8e&instanceid="+ Eval("PoNo")+"&groupid="+ Eval("GroupID")+"&display=1" %>'  
                                            Target="_blank">                                        
                                    </dx:ASPxHyperLink>
                                </DataItemTemplate> --%>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="采购行号" FieldName="po_rowid" Width="60px" VisibleIndex="4" />
                            <dx:GridViewDataTextColumn Caption="名称" FieldName="wlmc" Width="300px" VisibleIndex="5" />
                            <dx:GridViewDataTextColumn Caption="描述" FieldName="wlms" Width="200px" VisibleIndex="6" />
                            <dx:GridViewDataTextColumn Caption="总数量" FieldName="PurQty" Width="50px" VisibleIndex="7" />
                            <dx:GridViewDataTextColumn Caption="待收货数量" FieldName="qty_dsh" Width="70px" VisibleIndex="8" />
                            <dx:GridViewDataTextColumn Caption="采购负责部门" FieldName="DeptName" Width="80px" VisibleIndex="9" />
                            <dx:GridViewDataTextColumn Caption="采购负责人" FieldName="CreateByName" Width="80px" VisibleIndex="10" />
                            <dx:GridViewDataTextColumn Caption="供应商" FieldName="gys" Width="280px" VisibleIndex="11" />
                            <dx:GridViewDataTextColumn Caption="要求到货日期" FieldName="deliveryDate" Width="80px" VisibleIndex="12" />
                            <dx:GridViewDataTextColumn Caption="计划到货日期" FieldName="PlanReceiveDate" Width="80px" VisibleIndex="13" />
                            <dx:GridViewDataTextColumn Caption="采购类别" FieldName="PoType" Width="60px" VisibleIndex="14" />
                            <dx:GridViewDataTextColumn Caption="GroupID" FieldName="GroupID" VisibleIndex="99"
                                 HeaderStyle-CssClass="hidden" CellStyle-CssClass="hidden" FooterCellStyle-CssClass="hidden" FilterCellStyle-CssClass="hidden">
                            </dx:GridViewDataTextColumn>
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

