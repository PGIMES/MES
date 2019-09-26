<%@ Page Title="【包装方案查询】" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Pack_Report_Query.aspx.cs" Inherits="Forms_Pack_Pack_Report_Query" %>

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
            $("#mestitle").text("【包装方案查询】");

            $('#btn_add').click(function () {
                window.open('/Platform/WorkFlowRun/Default.aspx?flowid=6fe4a501-d522-458b-a46c-0baa6162d8d3&appid=')
            });

            $('#btn_edit').click(function () {
                if (grid.GetSelectedRowCount() <= 0) { layer.alert("请选择一条记录!"); return; }

                grid.GetSelectedFieldValues('FormNo;part;domain;site;ship', function GetVal(values) {
                    var formno = values[0][0];
                    var part = values[0][1];
                    var domain = values[0][2];
                    var site = values[0][3];
                    var ship = values[0][4];

                    $.ajax({
                        type: "post",
                        url: "Pack_Report_Query.aspx/CheckData",
                        data: "{'part':'" + part + "','domain':'" + domain + "','site':'" + site + "','ship':'" + ship + "','formno':'" + formno + "'}",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        async: false,//默认是true，异步；false为同步，此方法执行完在执行下面代码
                        success: function (data) {
                            var obj = eval(data.d);

                            if (obj[0].re_flag != "") {
                                layer.alert(obj[0].re_flag);
                            } else {
                                window.open('/Platform/WorkFlowRun/Default.aspx?flowid=6fe4a501-d522-458b-a46c-0baa6162d8d3&appid=&state=edit&formno=' + formno);
                            }
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
            $("div[class=dxgvCSD]").css("height", ($(window).height() - $("#div_p").height() - 170) + "px");

            $("#MainContent_gv").css("width", ($(window).width() - 10) + "px");
            $("div[class=dxgvCSD]").css("width", ($(window).width() - 10) + "px");
        }	
    </script>

    <style>
        .btn{
            padding:6px 6px;
        }
    </style>

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    
    <div class="panel-body" id="div_p">
        <div class="col-sm-12">
            <table style="line-height:40px">
            <tr>
                <td style="width:45px;">物料号</td>
                <td style="width:115px;">
                    <asp:TextBox ID="txt_part" class="form-control input-s-sm" runat="server" Width="110px"></asp:TextBox>
                </td>
                <td style="width:70px;">顾客零件号</td>
                <td style="width:125px;">
                    <asp:TextBox ID="txt_custpart" class="form-control input-s-sm" runat="server" Width="120px"></asp:TextBox>
                </td>    
                <td style="width:30px;">版本</td>
                <td style="width:85px;"> 
                    <asp:DropDownList ID="ddl_ver" runat="server" class="form-control input-s-md " Width="80px">
                        <asp:ListItem Value="">ALL</asp:ListItem>
                        <asp:ListItem Value="当前" Selected="True">当前</asp:ListItem>
                    </asp:DropDownList>
                </td>    
                <td id="td_btn">  
                    <button id="btn_search" type="button" class="btn btn-primary btn-large" runat="server" onserverclick="btn_search_Click"><i class="fa fa-search fa-fw"></i>&nbsp;查询</button>    
                    <button id="btn_add" type="button" class="btn btn-primary btn-large"><i class="fa fa-plus fa-fw"></i>&nbsp;新增</button>  
                    <button id="btn_edit" type="button" class="btn btn-primary btn-large"><i class="fa fa-pencil-square-o fa-fw"></i>&nbsp;编辑</button> 
                    <button id="btn_import" type="button" class="btn btn-primary btn-large" runat="server" onserverclick="btn_import_Click"><i class="fa fa-download fa-fw"></i>&nbsp;导出</button>
                </td>
            </tr>                      
        </table>
       </div>            
    </div>

    <div id="DIV1" style="margin-left: 5px; margin-right: 5px; margin-bottom: 10px">
        <table>
            <tr>
                <td>
                    <dx:ASPxGridView ID="gv" runat="server" KeyFieldName="FormNo" AutoGenerateColumns="False" Width="1000px" OnPageIndexChanged="gv_PageIndexChanged"  
                        ClientInstanceName="grid" OnCustomSummaryCalculate="gv_CustomSummaryCalculate">
                        <ClientSideEvents EndCallback="function(s, e) { setHeight(); }" />
                        <SettingsBehavior AllowDragDrop="TRUE" AllowFocusedRow="false" AllowSelectByRowClick="false" ColumnResizeMode="Control" AutoExpandAllGroups="true" MergeGroupsMode="Always" SortMode="Value" />
                        <SettingsPager PageSize="100"></SettingsPager>
                        <Settings ShowFilterRow="True" ShowFilterRowMenu="True"   VerticalScrollBarMode="Visible" VerticalScrollBarStyle="Standard" VerticalScrollableHeight="500"
                                 ShowFilterRowMenuLikeItem="True"  ShowFooter="True"  HorizontalScrollBarMode="Auto" />
                        <Columns>        
                            <dx:GridViewCommandColumn  ShowClearFilterButton="true" ShowSelectCheckbox="true" Name="Sel" Width="40" VisibleIndex="0"  SelectAllCheckboxMode="Page"  />
                            <dx:GridViewDataDateColumn Caption="申请日期" FieldName="ApplyDate" Width="90px" VisibleIndex="1" >
                                <PropertiesDateEdit DisplayFormatString="yyyy/MM/dd"></PropertiesDateEdit>
                            </dx:GridViewDataDateColumn>            
                            <dx:GridViewDataTextColumn Caption="PGI零件号" FieldName="part" Width="70px" VisibleIndex="1" >
                                <Settings AllowCellMerge="True" /> 
                                <DataItemTemplate>
                                    <dx:ASPxHyperLink ID="hpl_part" runat="server" Text='<%# Eval("part")%>' Cursor="pointer" ClientInstanceName='<%# "part"+Container.VisibleIndex.ToString() %>'
                                         NavigateUrl='<%# "/Platform/WorkFlowRun/Default.aspx?flowid=6fe4a501-d522-458b-a46c-0baa6162d8d3&instanceid="+ Eval("FormNo")+"&groupid="+ Eval("GroupID")+"&display=1" %>'  
                                         Target="_blank"
                                        >                                        
                                    </dx:ASPxHyperLink>
                                </DataItemTemplate> 
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="顾客零件号" FieldName="custpart" Width="120px" VisibleIndex="2" />
                            <dx:GridViewDataTextColumn Caption="顾客" FieldName="custname" Width="260px" VisibleIndex="2"/>
                            <dx:GridViewDataTextColumn Caption="发往工厂" FieldName="ship" Width="80px" VisibleIndex="2"/>
                            <dx:GridViewDataTextColumn Caption="单件重量" FieldName="ljzl" Width="80px" VisibleIndex="2"/>
                            <dx:GridViewDataTextColumn Caption="整托数量" FieldName="bzx_sl_t" Width="70px" VisibleIndex="3"/>
                            <dx:GridViewDataTextColumn Caption="整托尺寸(L)" FieldName="bzx_t_l" Width="90px" VisibleIndex="4"></dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="整托尺寸(W)" FieldName="bzx_t_w" Width="90px" VisibleIndex="4"></dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="整托尺寸(H)" FieldName="bzx_t_h" Width="90px" VisibleIndex="4"></dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="整托净重" FieldName="bzx_jz_t" Width="70px" VisibleIndex="5"></dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="整托毛重" FieldName="bzx_mz_t" Width="70px" VisibleIndex="6"></dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="整托成本" FieldName="cbfx_cb_t_total" Width="70px" VisibleIndex="7"></dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="单件成本" FieldName="cbfx_sj_j" Width="60px" VisibleIndex="8"></dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="年用量" FieldName="nyl" Width="80px" VisibleIndex="9">
                                <PropertiesTextEdit DisplayFormatString="{0:N0}"></PropertiesTextEdit>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="年包装成本" FieldName="nzj" Width="80px" VisibleIndex="10">
                                <PropertiesTextEdit DisplayFormatString="{0:N2}"></PropertiesTextEdit>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="销售价格" FieldName="cbfx_xs_price" Width="60px" VisibleIndex="12"></dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="包装成本率" FieldName="cbfx_cb_rate" Width="70px" VisibleIndex="12"></dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="包装箱规格" FieldName="bzx_gg" Width="150px" VisibleIndex="13"></dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="包装类别" FieldName="bzlb" Width="100px" VisibleIndex="14"></dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="版本" FieldName="ver" Width="40px" VisibleIndex="15" />
                            <dx:GridViewDataTextColumn Caption="发自" FieldName="site" Width="40px" VisibleIndex="16" />
                            <%--<dx:GridViewDataTextColumn Caption="表单编号" FieldName="FormNo" Width="130px" VisibleIndex="26"/>
                            <dx:GridViewDataTextColumn Caption="导入QAD" FieldName="isftp" Width="60px" VisibleIndex="28"></dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="id_dtl" FieldName="id_dtl" VisibleIndex="99"
                                 HeaderStyle-CssClass="hidden" CellStyle-CssClass="hidden" FooterCellStyle-CssClass="hidden"></dx:GridViewDataTextColumn>--%>
                        </Columns>
                        <TotalSummary>
                            <dx:aspxsummaryitem DisplayFormat="合计:{0:N0}" FieldName="custname" ShowInColumn="custname" ShowInGroupFooterColumn="custname" SummaryType="Sum" />
                            <dx:ASPxSummaryItem DisplayFormat="{0:N2}" FieldName="nzj" ShowInColumn="nzj" ShowInGroupFooterColumn="nzj" SummaryType="Sum" />
                            <dx:ASPxSummaryItem DisplayFormat="{0:N2}" FieldName="cbfx_cb_rate" ShowInColumn="cbfx_cb_rate" ShowInGroupFooterColumn="cbfx_cb_rate" SummaryType="Custom" />
                        </TotalSummary>
                        <Styles>
                            <Header BackColor="#99CCFF"></Header>
                            <FocusedRow BackColor="#99CCFF" ForeColor="#0000CC"></FocusedRow>
                            <Footer HorizontalAlign="Right" Font-Bold="true" ForeColor="Red"></Footer>
                        </Styles>
                    </dx:ASPxGridView>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>

