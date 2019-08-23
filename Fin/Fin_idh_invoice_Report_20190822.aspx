<%@ Page Title="【国内客户开票通知】" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Fin_idh_invoice_Report_20190822.aspx.cs" Inherits="Fin_Fin_idh_invoice_Report_20190822" %>

<%@ Register Assembly="DevExpress.Web.v17.2, Version=17.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <script src="/Content/js/jquery.min.js"  type="text/javascript"></script>
    <script src="/Content/js/bootstrap.min.js"></script>
    <script src="/Content/js/layer/layer.js"></script>
    <script src="/Content/js/plugins/layer/laydate/laydate.js"></script>
    <script type="text/javascript">
        <%--var UserId = '<%=UserId%>';
        var UserName = '<%=UserName%>';
        var DeptName = '<%=DeptName%>';--%>

        $(document).ready(function () {
            $("#mestitle").text("【国内客户开票通知】");

            setHeight();
            $(window).resize(function () {
                setHeight();
            });

            $('#btn_upload').click(function () {
                var url = "/Fin/Fin_idh_invoice_upload.aspx";

                layer.open({
                    title: '上传开票通知单&nbsp;&nbsp;<a href="/userguide/invoice_upload_format.xlsx" target="_blank" style="color:red">upload format</a>',
                    closeBtn: 2,
                    type: 2,
                    area: ['500px', '550px'],
                    fixed: false, //不固定
                    maxmin: true, //开启最大化最小化按钮
                    content: url
                });
            });


        });

        function setHeight() {
            $("div[class=dxgvCSD]").css("height", ($(window).height() - $("#div_p").height() - 250) + "px");

            $("#MainContent_GV_PART").css("width", ($(window).width() - 10) + "px")
            $("div[class=dxgvCSD]").css("width", ($(window).width() - 10) + "px");
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
                    <td style="text-align:right;">域：</td>
                    <td>
                        <asp:DropDownList ID="ddl_comp" runat="server" class="form-control input-s-md ">
                            <asp:ListItem Text="200" Value="200"></asp:ListItem>
                            <asp:ListItem Text="100" Value="100"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td style="text-align:right;">&nbsp;&nbsp;生效日期：</td>
                    <td>
                        <asp:TextBox ID="StartDate" runat="server" class="form-control" Width="120px" 
                            onclick="laydate({type: 'date',format: 'YYYY/MM/DD',choose: function(dates){}});" />
                    </td>
                    <td>~</td>
                    <td>
                        <asp:TextBox ID="EndDate" runat="server" class="form-control" Width="120px"
                            onclick="laydate({type: 'date',format: 'YYYY/MM/DD',choose: function(dates){}});" />
                    </td>                        
                    <td style="text-align:right;">&nbsp;&nbsp;状态：</td>
                    <td>
                        <asp:DropDownList ID="ddl_status" runat="server" class="form-control input-s-sm ">
                            <asp:ListItem Value="未开票">未开票</asp:ListItem>
                            <asp:ListItem Value="待开票">待开票</asp:ListItem>
                            <asp:ListItem Value="已开票">已开票</asp:ListItem>
                        </asp:DropDownList>
                    </td> 
                    <td> 
                        &nbsp;
                        <asp:Button ID="Bt_select" runat="server" Text="查询" class="btn btn-large btn-primary" OnClick="Bt_select_Click" Width="70px" /> 
                        &nbsp;
                        <asp:Button ID="Bt_Export" runat="server" class="btn btn-large btn-primary" OnClick="Bt_Export_Click" Text="导出" Width="70px" /> 
                        &nbsp;
                        <button id="btn_upload" type="button" class="btn btn-primary btn-large"><i class="fa fa-upload fa-fw"></i>&nbsp;上传开票通知单</button>
                    </td> 
                </tr>
            </table>
        </div>
    </div>
    <div runat="server" id="DIV1" style="margin-left: 5px; margin-right: 5px; margin-bottom: 10px">
        <table>
            <tr>
                <td>
                    <dx:ASPxGridView ID="GV_PART" ClientInstanceName="grid" runat="server" KeyFieldName="ih_inv_nbr;ih_ship;idh_part" AutoGenerateColumns="False"  
                             OnPageIndexChanged="GV_PART_PageIndexChanged" Width="1000px"><%--3030--%>
                        <ClientSideEvents EndCallback="function(s, e) { setHeight(); }" />
                        <SettingsBehavior AllowDragDrop="TRUE" AllowFocusedRow="false" AllowSelectByRowClick="false" ColumnResizeMode="Control" AutoExpandAllGroups="true" MergeGroupsMode="Always" SortMode="Value" />
                        <SettingsPager PageSize="100"></SettingsPager>
                        <Settings ShowFilterRow="True" ShowFilterRowMenu="True"   VerticalScrollBarMode="Visible" VerticalScrollBarStyle="Standard" VerticalScrollableHeight="500"
                                 ShowFilterRowMenuLikeItem="True"  ShowFooter="True"  HorizontalScrollBarMode="Auto" />
                        <Columns>
                            <dx:GridViewCommandColumn   ShowClearFilterButton="true" ShowSelectCheckbox="true" Name="Sel" Width="40" VisibleIndex="0"  SelectAllCheckboxMode="Page"  >
                            </dx:GridViewCommandColumn> 
                            <dx:GridViewDataDateColumn Caption="生效日期" FieldName="ih_eff_date" Width="90px" VisibleIndex="1" >
                                <PropertiesDateEdit DisplayFormatString="yyyy/MM/dd"></PropertiesDateEdit>
                            </dx:GridViewDataDateColumn>
                            <dx:GridViewDataTextColumn Caption="发票号" FieldName="ih_inv_nbr" Width="180px" VisibleIndex="2" />
                            <dx:GridViewDataTextColumn Caption="票据开往" FieldName="ih_bill" Width="60px" VisibleIndex="3" />
                            <dx:GridViewDataTextColumn Caption="票据开往名称" FieldName="ih_bill_name" Width="200px" VisibleIndex="4" />
                            <dx:GridViewDataTextColumn Caption="货运单" FieldName="ih_bol" Width="120px" VisibleIndex="5" />
                            <dx:GridViewDataTextColumn Caption="发货至" FieldName="ih_ship" Width="90px" VisibleIndex="6" />
                            <dx:GridViewDataTextColumn Caption="物料号" FieldName="idh_part" Width="80px" VisibleIndex="7" />
                            <dx:GridViewDataTextColumn Caption="客户项目" FieldName="idh_custpart" Width="100px" VisibleIndex="8" />
                            <dx:GridViewDataTextColumn Caption="备注" FieldName="cp_comment" Width="250px" VisibleIndex="9"/>  
                            <dx:GridViewDataTextColumn Caption="计量单位" FieldName="idh_um" Width="50px" VisibleIndex="10" />  
                            <dx:GridViewDataTextColumn Caption="总数量" FieldName="idh_qty_inv" Width="65px" VisibleIndex="11" > 
                                <PropertiesTextEdit DisplayFormatString="{0:N2}"></PropertiesTextEdit>
                            </dx:GridViewDataTextColumn>                             
                            <dx:GridViewDataTextColumn Caption="已开数量" FieldName="yksl" Width="65px" VisibleIndex="12"> 
                                <PropertiesTextEdit DisplayFormatString="{0:N2}"></PropertiesTextEdit>
                            </dx:GridViewDataTextColumn>                             
                            <dx:GridViewDataTextColumn Caption="未开数量" FieldName="wksl" Width="65px" VisibleIndex="13" >
                                <PropertiesTextEdit DisplayFormatString="{0:N2}"></PropertiesTextEdit>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="价目表价格" FieldName="idh_list_pr" Width="65px" VisibleIndex="14"> 
                                <PropertiesTextEdit DisplayFormatString="{0:N2}"></PropertiesTextEdit>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="开票价格" FieldName="idh_price_inv" Width="65px" VisibleIndex="15">
                                <PropertiesTextEdit DisplayFormatString="{0:N2}"></PropertiesTextEdit>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="税率" FieldName="idh_taxc_new" Width="40px" VisibleIndex="16">
                                <PropertiesTextEdit DisplayFormatString="{0:N2}"></PropertiesTextEdit>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="发票额" FieldName="inv_fpe" Width="65px" VisibleIndex="17"  
                                ToolTip="开票价格*已开票数量">
                                <PropertiesTextEdit DisplayFormatString="{0:N2}"></PropertiesTextEdit>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="PGI发票额" FieldName="list_fpe" Width="65px" VisibleIndex="18"  
                                ToolTip="价目表价格*数量">
                                <PropertiesTextEdit DisplayFormatString="{0:N2}"></PropertiesTextEdit>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="差额(未税)" FieldName="chae" Width="65px" VisibleIndex="19"  
                                ToolTip="已开票数量*(开票价格-价目表价格)">
                                <PropertiesTextEdit DisplayFormatString="{0:N2}"></PropertiesTextEdit>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="税金额(开票)" FieldName="inv_tax" Width="75px" VisibleIndex="20"  
                                ToolTip="开票价格*已开票数量*税率">
                                <PropertiesTextEdit DisplayFormatString="{0:N2}"></PropertiesTextEdit>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="税金额(价目表)" FieldName="list_tax" Width="95px" VisibleIndex="21"  
                                ToolTip="价目表价格*已开票数量*税率">
                                <PropertiesTextEdit DisplayFormatString="{0:N2}"></PropertiesTextEdit>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="税款合计(开票)" FieldName="inv_tax_sum" Width="95px" VisibleIndex="22"  
                                ToolTip="开票价格*已开票数量+开票价格*已开票数量*税率">
                                <PropertiesTextEdit DisplayFormatString="{0:N2}"></PropertiesTextEdit>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="税款合计(价目表)" FieldName="list_tax_sum" Width="100px" VisibleIndex="23"  
                                ToolTip="价目表价格*已开票数量+价目表价格*已开票数量*税率">
                                <PropertiesTextEdit DisplayFormatString="{0:N2}"></PropertiesTextEdit>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="文件名称" FieldName="ori_filename" Width="380px" VisibleIndex="24">
                                <DataItemTemplate>
                                    <dx:ASPxHyperLink ID="hpl_ori_filename" runat="server" Text='<%# Eval("ori_filename")%>' Cursor="pointer"
                                        NavigateUrl='<%# "/UploadFile/Fin/invoice/"+ Eval("new_filename") %>'  
                                        Target="_blank">                                        
                                    </dx:ASPxHyperLink>
                                </DataItemTemplate> 
                                <Settings AllowAutoFilterTextInputTimer="False" />
                            </dx:GridViewDataTextColumn> 
                            <%--<dx:GridViewDataTextColumn Caption="new_filename" FieldName="new_filename" VisibleIndex="99"
                                    HeaderStyle-CssClass="hidden" CellStyle-CssClass="hidden" FooterCellStyle-CssClass="hidden"></dx:GridViewDataTextColumn>--%>
                        </Columns>
                        <%--<TotalSummary>
                            <dx:ASPxSummaryItem DisplayFormat="${0:N2}" FieldName="yksl" ShowInColumn="yksl" ShowInGroupFooterColumn="yksl" SummaryType="Sum" />
                            <dx:ASPxSummaryItem DisplayFormat="${0:N2}" FieldName="TotalPrice_USD_YF" ShowInColumn="TotalPrice_USD_YF" ShowInGroupFooterColumn="TotalPrice_USD_YF" SummaryType="Sum" />
                            <dx:ASPxSummaryItem DisplayFormat="${0:N2}" FieldName="TotalPrice_Base_USD" ShowInColumn="TotalPrice_Base_USD" ShowInGroupFooterColumn="TotalPrice_Base_USD" SummaryType="Sum" />
                            <dx:ASPxSummaryItem DisplayFormat="${0:N2}" FieldName="TotalPrice_QG_USD" ShowInColumn="TotalPrice_QG_USD" ShowInGroupFooterColumn="TotalPrice_QG_USD" SummaryType="Sum" />
                            <dx:ASPxSummaryItem DisplayFormat="${0:N2}" FieldName="base_qg_usd" ShowInColumn="base_qg_usd" ShowInGroupFooterColumn="base_qg_usd" SummaryType="Sum" />
                        </TotalSummary>--%>
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

