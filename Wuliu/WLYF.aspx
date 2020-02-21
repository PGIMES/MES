<%@ Page Title="物流运费" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="WLYF.aspx.cs" Inherits="Wuliu_WLYF" %>

<%@ Register Assembly="DevExpress.Web.v17.2, Version=17.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script src="/Content/js/jquery.min.js"  type="text/javascript"></script>
    <script src="/Content/js/bootstrap.min.js"></script>
    <script src="/Content/js/layer/layer.js"></script>
    <script src="/Content/js/plugins/layer/laydate/laydate.js"></script>
    <script type="text/javascript">
        var UserId = '<%=UserId%>';
        var UserName = '<%=UserName%>';
        var DeptName = '<%=DeptName%>';

        $(document).ready(function () {
            $("#mestitle").text("【物流运费查询】");

            if (DeptName.indexOf("财务") != -1 || DeptName.indexOf("IT") != -1) {
                $('#btn_sure').show();
            } else {
                $('#btn_sure').hide();
            }

             setHeight();
            $(window).resize(function () {
                setHeight();
            });
            
            $('#btn_upload_multi').click(function () {
                var url = "/wuliu/WLYF_upload_multi.aspx";

                layer.open({
                    title: '上传请款通知',
                    closeBtn: 2,
                    type: 2,
                    area: ['500px', '350px'],
                    fixed: false, //不固定
                    maxmin: true, //开启最大化最小化按钮
                    content: url
                });
            });

            $('#btn_sure').click(function () {
                if (grid_part.GetSelectedRowCount() <= 0) { layer.alert("请选择一条记录!"); return; }

                grid_part.GetSelectedFieldValues('id;ship_to', function GetVal(values) {

                    var ls_ids = "";
                    for (var i = 0; i < values.length; i++) {
                        var ls_id = values[i][0];//== null ? "" : values[0][0];
                        ls_ids = ls_ids + ls_id + ",";
                    }
                    ls_ids = ls_ids.substr(0, ls_ids.length - 1);

                    $.ajax({
                        type: "post",
                        url: "WLYF.aspx/deal",
                        data: "{'ids':'" + ls_ids + "'}",
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
        }
        	
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
     
   <div class="panel-body" id="div_p">
        <div class="col-sm-12">
            <table style="line-height:40px">
                <tr>
                    <td style="text-align:right;">工厂：</td>
                    <td> 
                        <asp:DropDownList ID="ddl_comp" runat="server" class="form-control input-s-sm ">
                            <asp:ListItem Value="ALL">ALL</asp:ListItem>
                            <asp:ListItem Value="100">上海工厂</asp:ListItem>
                            <asp:ListItem Value="200">昆山工厂</asp:ListItem>
                        </asp:DropDownList>
                    </td>                                                          
                    <td style="text-align:right;">&nbsp;&nbsp;生效日期:</td>
                        <td >
                        <asp:TextBox ID="txtDateFrom" class="form-control" onclick="laydate()" runat="server" Width="100px"></asp:TextBox>
                    </td>
                    <td>~</td>
                    <td>
                        <asp:TextBox ID="txtDateTo" class="form-control" onclick="laydate()" runat="server" Width="100px"></asp:TextBox>
                    </td>                        
                    <td style="text-align:right;">&nbsp;&nbsp;状态：</td>
                    <td>
                        <asp:DropDownList ID="ddl_status" runat="server" class="form-control input-s-sm ">
                            <asp:ListItem Value="未请款">未请款</asp:ListItem>
                            <asp:ListItem Value="已请款">已请款</asp:ListItem>
                            <asp:ListItem Value="已付款">已付款</asp:ListItem>
                        </asp:DropDownList>
                    </td> 
                    <td>&nbsp;
                        <button id="Bt_select" type="button" class="btn btn-primary btn-large" runat="server" onserverclick="Bt_select_Click"><i class="fa fa-search fa-fw"></i>&nbsp;查询</button>    
                        <button id="Bt_Export" type="button" class="btn btn-primary btn-large" runat="server" onserverclick="Bt_Export_Click"><i class="fa fa-download fa-fw"></i>&nbsp;导出</button>
                        &nbsp;
                        <button id="btn_upload_multi" type="button" class="btn btn-primary btn-large"><i class="fa fa-upload fa-fw"></i>&nbsp;上传请款通知</button>
                        &nbsp;
                        <button id="btn_sure" type="button" class="btn btn-primary btn-large"><i class="fa fa-check fa-fw"></i>&nbsp;确认付款</button>
                        &nbsp;&nbsp;<a href="/UserGuide/wlyf_upload_format.xls" target="_blank" style="color:red">upload format</a>
                    </td>                           
                </tr>                          
            </table>
        </div>
    </div>

    <div runat="server" id="DIV1" style="margin-left: 5px; margin-right: 5px; margin-bottom: 10px">
        <table>
            <tr>
                <td>
                     <dx:ASPxGridView ID="GV_PART" runat="server" KeyFieldName="id" AutoGenerateColumns="False" 
                         OnPageIndexChanged="GV_PART_PageIndexChanged" Width="1000px" ClientInstanceName="grid_part">
                         <ClientSideEvents EndCallback="function(s, e) { setHeight(); }" />
                        <SettingsBehavior AllowDragDrop="TRUE" AllowFocusedRow="false" AllowSelectByRowClick="false" ColumnResizeMode="Control" AutoExpandAllGroups="true" MergeGroupsMode="Always" SortMode="Value" />
                        <SettingsPager PageSize="100"></SettingsPager>
                        <Settings ShowFilterRow="True" ShowFilterRowMenu="True"   VerticalScrollBarMode="Visible" VerticalScrollBarStyle="Standard" VerticalScrollableHeight="500"
                                 ShowFilterRowMenuLikeItem="True"  ShowFooter="True"  HorizontalScrollBarMode="Auto" />
                        <Columns>
                            <dx:GridViewCommandColumn   ShowClearFilterButton="true" ShowSelectCheckbox="true" Name="Sel" Width="40" VisibleIndex="0"  SelectAllCheckboxMode="Page"  />
                            <dx:GridViewDataDateColumn Caption="发运日期" FieldName="tr_effdate" Width="90px" VisibleIndex="1" >
                                <PropertiesDateEdit DisplayFormatString="yyyy/MM/dd"></PropertiesDateEdit>
                            </dx:GridViewDataDateColumn>
                            <dx:GridViewDataTextColumn Caption="集装箱" FieldName="tr_part" Width="90px" VisibleIndex="2" />
                            <dx:GridViewDataTextColumn Caption="地点" FieldName="tr_domain" Width="40px" VisibleIndex="3" />
                            <dx:GridViewDataTextColumn Caption="集装箱描述" FieldName="descs" Width="200px" VisibleIndex="4" />
                            <dx:GridViewDataTextColumn Caption="货运单" FieldName="hyd" Width="120px" VisibleIndex="5" />
                            <dx:GridViewDataTextColumn Caption="柜号" FieldName="tr_nbr" Width="120px" VisibleIndex="6" />
                            <dx:GridViewDataTextColumn Caption="ship_to" FieldName="ship_to" Width="80px" VisibleIndex="7" />
                            <dx:GridViewDataTextColumn Caption="USD金额" FieldName="USD_amount" Width="90px" VisibleIndex="8" />
                            <dx:GridViewDataTextColumn Caption="CNY金额" FieldName="CNY_amount" Width="90px" VisibleIndex="9" />
                            <dx:GridViewDataTextColumn Caption="到货日期" FieldName="tr_effdate_dh" Width="80px" VisibleIndex="10" />
                            <dx:GridViewDataTextColumn Caption="USD固定金额" FieldName="USD_fixed_amount" Width="90px" VisibleIndex="11">
                                <HeaderStyle BackColor="#F0E68C" />
                            </dx:GridViewDataTextColumn> 
                            <dx:GridViewDataTextColumn Caption="CNY固定金额" FieldName="CNY_fixed_amount" Width="90px" VisibleIndex="12">
                                <HeaderStyle BackColor="#F0E68C" />
                            </dx:GridViewDataTextColumn> 
                            <dx:GridViewDataTextColumn Caption="USD不固定金额" FieldName="USD_no_fixed_amount" Width="90px" VisibleIndex="13">
                                <HeaderStyle BackColor="#F0E68C" />
                            </dx:GridViewDataTextColumn> 
                            <dx:GridViewDataTextColumn Caption="CNY不固定金额" FieldName="CNY_no_fixed_amount" Width="90px" VisibleIndex="14">
                                <HeaderStyle BackColor="#F0E68C" />
                            </dx:GridViewDataTextColumn> 
                             <dx:GridViewDataDateColumn Caption="上传日期" FieldName="uploadtime" Width="90px" VisibleIndex="15" >
                                <PropertiesDateEdit DisplayFormatString="yyyy/MM/dd"></PropertiesDateEdit>
                                <HeaderStyle BackColor="#F0E68C" />
                            </dx:GridViewDataDateColumn>
                             <dx:GridViewDataDateColumn Caption="确认日期" FieldName="sure_date" Width="90px" VisibleIndex="16" >
                                <PropertiesDateEdit DisplayFormatString="yyyy/MM/dd"></PropertiesDateEdit>
                                <HeaderStyle BackColor="#F0E68C" />
                            </dx:GridViewDataDateColumn>
                            <dx:GridViewDataTextColumn Caption="文件名称" FieldName="ori_filename" Width="380px" VisibleIndex="17">
                                <DataItemTemplate>
                                    <dx:ASPxHyperLink ID="hpl_ori_filename" runat="server" Text='<%# Eval("ori_filename")%>' Cursor="pointer"
                                        NavigateUrl='<%# "/UploadFile/wuliu/wlfy/"+ Eval("new_filename") %>'  
                                        Target="_blank">                                        
                                    </dx:ASPxHyperLink>
                                </DataItemTemplate> 
                                <Settings AllowAutoFilterTextInputTimer="False" />
                                <HeaderStyle BackColor="#F0E68C" />
                            </dx:GridViewDataTextColumn> 
                            <dx:GridViewDataTextColumn Caption="id" FieldName="id" VisibleIndex="99" Width="0px"                                
                                 HeaderStyle-CssClass="hidden" CellStyle-CssClass="hidden" FooterCellStyle-CssClass="hidden"></dx:GridViewDataTextColumn>
                        </Columns>
                        <TotalSummary>
                            <dx:ASPxSummaryItem DisplayFormat="{0:N2}" FieldName="USD_amount"  SummaryType="Sum" />
                            <dx:ASPxSummaryItem DisplayFormat="{0:N2}" FieldName="CNY_amount" SummaryType="Sum" />    
                            <dx:ASPxSummaryItem DisplayFormat="{0:N2}" FieldName="USD_fixed_amount"  SummaryType="Sum" />
                            <dx:ASPxSummaryItem DisplayFormat="{0:N2}" FieldName="CNY_fixed_amount" SummaryType="Sum" />      
                            <dx:ASPxSummaryItem DisplayFormat="{0:N2}" FieldName="USD_no_fixed_amount"  SummaryType="Sum" />
                            <dx:ASPxSummaryItem DisplayFormat="{0:N2}" FieldName="CNY_no_fixed_amount" SummaryType="Sum" />                                 
                        </TotalSummary>      
                        <Styles>
                            <Header BackColor="#99CCFF"></Header>
                            <FocusedRow BackColor="#99CCFF" ForeColor="#0000CC"></FocusedRow>
                            <Footer HorizontalAlign="Right" ForeColor="Red"></Footer>
                        </Styles>
                    </dx:ASPxGridView>
                </td>
            </tr>
        </table>
    </div>

</asp:Content>


