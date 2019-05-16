﻿<%@ Page Title="【应付类合同执行进度查询】" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="xxcontract_Report.aspx.cs" Inherits="Fin_xxcontract_Report" %>

<%@ Register Assembly="DevExpress.Web.v17.2, Version=17.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <script type="text/javascript" src="/Content/js/jquery.min.js"></script>
    <script src="/Content/js/layer/layer.js"></script>
    <script src="/Content/js/plugins/layer/laydate/laydate.js"></script>
    <script type="text/javascript">
        var UserId = '<%=UserId%>';
        var UserName = '<%=UserName%>';

        $(document).ready(function () {
            $("#mestitle").html("【应付类合同执行进度查询】");
            setHeight();

            $(window).resize(function () {
                setHeight();
            });

            $('#btn_print').click(function () {

                if (grid.GetSelectedRowCount() != 1) { layer.alert("请选择一条记录!"); return; }

                grid.GetSelectedFieldValues('syscontractno;contractline;fkrate;fkamt;checkdate;payclause_rate', function GetVal(values) {
                    if (values.length > 0) {
                        var ls_nbr = values[0][0];
                        var ls_line = values[0][1];
                        var fkrate = values[0][2].replace("%", "");//累计付款比例
                        var fkamt = values[0][3];//付款金额(原币)
                        var checkdate = values[0][4];//验收日期
                        var payclause_rate = values[0][5];//条款摘要

                        //累计付款比例100%，不能付款了
                        if (Number(fkrate)>=100) {
                            layer.alert("合同号" + ls_nbr + "，已付款完成，不能打印！");
                            return;
                        }

                        //合同行已经付款了，不能付款了
                        if (Number(fkamt)>0) {
                            layer.alert("合同号" + ls_nbr + "，行号" + ls_line + ",已付款，不能打印！");
                            return;
                        }

                        if (payclause_rate.indexOf("验收款") >= 0) {
                            if (checkdate == "") {
                                layer.alert("合同号" + ls_nbr + "，还未验收，不能打印！");
                                return;
                            }
                        }

                        var bf = true;
                        $.ajax({
                            type: "post",
                            url: "xxcontract_Report.aspx/check_data",
                            data: "{'nbr':'" + ls_nbr + "','domain':'" + $("#MainContent_ddl_domain").val() + "'}",
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            async: false,//默认是true，异步；false为同步，此方法执行完在执行下面代码
                            success: function (data) {
                                var obj = eval(data.d);
                                if (obj[0].re_flag != "") {
                                    layer.alert("合同号" + ls_nbr + "，已" + obj[0].re_flag + "，不能打印！");
                                    bf = false;
                                }
                            }

                        });
                        if (bf) {
                            //window.open('/Forms/PurChase/rpt_Contract_Print.aspx?nbr=' + ls_nbr + '&line=' + ls_line);
                            window.open('/Forms/PurChase/rpt_Contract_Print_single.aspx?nbr=' + ls_nbr + '&line=' + ls_line);
                        }
                    }
                });
            });

            $('#btn_modify_plan').click(function () {
                if (grid.GetSelectedRowCount() != 1) { layer.alert("请选择一条记录!"); return; }

                grid.GetSelectedFieldValues('syscontractno;contractline;signdate;fkamt;ori_total_amount', function GetVal(values) {
                    if (values.length > 0) {
                        var ls_nbr = values[0][0];
                        var ls_line = values[0][1];
                        var signdate = values[0][2] == null ? "" : values[0][2].format("yyyy/MM/dd");
                        var fkamt = values[0][3];
                        var ori_total_amount = values[0][4];

                        var url = '/Forms/PurChase/PO_Contract_Modify.aspx?domain=' + $("#MainContent_ddl_domain").val()
                            + '&nbr=' + ls_nbr + '&line=' + ls_line + '&signdate=' + signdate + '&fkamt=' + fkamt + '&ori_total_amount=' + ori_total_amount;

                        //alert(url);
                        layer.open({
                            title: '修改合同计划-系统合同号【' + ls_nbr + '】-合同总金额(原币)【' + ori_total_amount + '】',
                            closeBtn: 2,
                            type: 2,
                            area: ['1200px', '580px'],
                            fixed: false, //不固定
                            maxmin: true, //开启最大化最小化按钮
                            content: url
                        });

                    }
                });
            });

            $('#btn_zf').click(function () {
                if (grid.GetSelectedRowCount() != 1) { layer.alert("请选择一条记录!"); return; }

                grid.GetSelectedFieldValues('syscontractno;fkrate', function GetVal(values) {
                    if (values.length > 0) {
                        var ls_nbr = values[0][0];
                        var fkrate = values[0][1].replace("%", "");//累计付款比例

                        //累计付款比例100%，不能付款了
                        if (Number(fkrate) > 0) {
                            layer.alert("合同号" + ls_nbr + "，已付款，不能作废！");
                            return;
                        }


                        $.ajax({
                            type: "post",
                            url: "xxcontract_Report.aspx/deal_data",
                            data: "{'action':'作废','nbr':'" + ls_nbr + "','domain':'" + $("#MainContent_ddl_domain").val() + "','UserId':'" + UserId + "','UserName':'" + UserName + "'}",
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            async: false,//默认是true，异步；false为同步，此方法执行完在执行下面代码
                            success: function (data) {
                                var obj = eval(data.d);
                                layer.alert(obj[0].re_flag);
                                return;
                            }

                        });

                    }
                });
            });

            $('#btn_wc').click(function () {
                if (grid.GetSelectedRowCount() != 1) { layer.alert("请选择一条记录!"); return; }

                grid.GetSelectedFieldValues('syscontractno;fkrate', function GetVal(values) {
                    if (values.length > 0) {
                        var ls_nbr = values[0][0];
                        var fkrate = values[0][1].replace("%", "");//累计付款比例

                        //累计付款比例100%，不能付款了
                        if (Number(fkrate) < 90) {
                            layer.alert("合同号" + ls_nbr + "，累计付款比例还未达到90%，不能关闭！");
                            return;
                        }

                        $.ajax({
                            type: "post",
                            url: "xxcontract_Report.aspx/deal_data",
                            data: "{'action':'关闭','nbr':'" + ls_nbr + "','domain':'" + $("#MainContent_ddl_domain").val() + "','UserId':'" + UserId + "','UserName':'" + UserName + "'}",
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            async: false,//默认是true，异步；false为同步，此方法执行完在执行下面代码
                            success: function (data) {
                                var obj = eval(data.d);
                                layer.alert(obj[0].re_flag);
                                return;
                            }

                        });

                    }
                });
            });

        });

        function setHeight() {
            $("div[class=dxgvCSD]").css("height", ($(window).height() - $("#div_p").height() - 160) + "px");
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

    <div class="col-md-12" id="div_p"  style="margin-bottom:5px">
        <table style=" border-collapse: collapse;">
            <tr>
                <td style="width:35px;">域</td>
                <td style="width:125px;">
                    <asp:DropDownList ID="ddl_domain" runat="server" class="form-control input-s-md " Width="120px">
                        <asp:ListItem Value="200">昆山工厂</asp:ListItem>
                        <asp:ListItem Value="100">上海工厂</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td style="width:65px;">签订日期</td>
                <td style="width:125px;">
                    <asp:TextBox ID="StartDate" runat="server" class="form-control" Width="120px" 
                        onclick="laydate({type: 'date',format: 'YYYY/MM/DD',choose: function(dates){}});" />
                </td>
                <td style="width:15px;">~</td>
                <td style="width:125px;">
                    <asp:TextBox ID="EndDate" runat="server" class="form-control" Width="120px"
                        onclick="laydate({type: 'date',format: 'YYYY/MM/DD',choose: function(dates){}});" />
                </td>               
                <td id="td_btn">  
                    <button id="btn_search" type="button" class="btn btn-primary btn-large" runat="server" onserverclick="btn_search_Click"><i class="fa fa-search fa-fw"></i>&nbsp;查询</button> 
                    <button id="btn_export" type="button" class="btn btn-primary btn-large" runat="server" onserverclick="btn_export_Click"><i class="fa fa-download fa-fw"></i>&nbsp;导出</button>
                    <button id="btn_modify_plan" type="button" class="btn btn-primary btn-large"><i class="fa fa-edit fa-fw"></i>&nbsp;修改合同计划</button>
                    <button id="btn_print" type="button" class="btn btn-primary btn-large"><i class="fa fa-print fa-fw"></i>&nbsp;打印付款申请单</button>                    
                    <button id="btn_zf" type="button" class="btn btn-primary btn-large"><i class="fa fa-delicious fa-fw"></i>&nbsp;作废合同</button>
                    <button id="btn_wc" type="button" class="btn btn-primary btn-large"><i class="fa fa-comment fa-fw"></i>&nbsp;关闭合同</button>
                </td>
            </tr>                      
        </table>                   
    </div>

    <div class="col-sm-12">
        <table>
            <tr>
                <td>
                    <dx:ASPxGridView ID="gv" runat="server" KeyFieldName="syscontractno;contractline" 
                        AutoGenerateColumns="False" Width="3615px" OnPageIndexChanged="gv_PageIndexChanged"  ClientInstanceName="grid"
                        OnExportRenderBrick="gv_ExportRenderBrick" OnHtmlRowCreated="gv_HtmlRowCreated">
                        <ClientSideEvents EndCallback="function(s, e) {setHeight();}"  />
                        <SettingsPager PageSize="100" ></SettingsPager>
                        <Settings ShowFilterRow="True" ShowGroupPanel="false" ShowFilterRowMenu="True" ShowFilterRowMenuLikeItem="True" AutoFilterCondition="Contains" 
                            VerticalScrollBarMode="Visible" VerticalScrollBarStyle="Standard" VerticalScrollableHeight="600"  />
                        <SettingsBehavior AllowFocusedRow="false" AllowSelectByRowClick="false"  ColumnResizeMode="Control" AllowEllipsisInText="false"/>
                        <Columns>     
                            <dx:GridViewCommandColumn   ShowClearFilterButton="true" ShowSelectCheckbox="true" Name="Sel" Width="40" VisibleIndex="0">
                            </dx:GridViewCommandColumn>    
                            <dx:GridViewDataTextColumn Caption="合同状态" FieldName="contractstatus" Width="55px" VisibleIndex="0" ></dx:GridViewDataTextColumn>                
                            <dx:GridViewDataTextColumn Caption="合同类型" FieldName="contracttype" Width="55px" VisibleIndex="1" ></dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="系统合同号" FieldName="syscontractno" Width="80px" VisibleIndex="2"></dx:GridViewDataTextColumn>   
                            <dx:GridViewDataTextColumn Caption="行号" FieldName="contractline" Width="40px" VisibleIndex="3"></dx:GridViewDataTextColumn>   
                            <dx:GridViewDataTextColumn Caption="实际合同号" FieldName="actualcontractno" Width="80px" VisibleIndex="4"></dx:GridViewDataTextColumn>     
                            <dx:GridViewDataTextColumn Caption="供应商编码" FieldName="gys_code" Width="65px" VisibleIndex="5"></dx:GridViewDataTextColumn>     
                            <dx:GridViewDataTextColumn Caption="合同对方单位" FieldName="gys_name" Width="210px" VisibleIndex="6"></dx:GridViewDataTextColumn>   
                            <dx:GridViewDataTextColumn Caption="合同名称" FieldName="contractname" Width="210px" VisibleIndex="7"></dx:GridViewDataTextColumn>     
                            <dx:GridViewDataTextColumn Caption="产品信息" FieldName="productinfor" Width="220px" VisibleIndex="8"></dx:GridViewDataTextColumn>  
                            <dx:GridViewDataTextColumn Caption="模具属性" FieldName="assetattribute" Width="150px" VisibleIndex="9"></dx:GridViewDataTextColumn>     
                            <dx:GridViewDataTextColumn Caption="条款摘要" FieldName="payclause_rate" Width="100px" VisibleIndex="10"></dx:GridViewDataTextColumn>    
                            <dx:GridViewDataTextColumn Caption="用途类别" FieldName="wltype" Width="100px" VisibleIndex="11"></dx:GridViewDataTextColumn>    
                            <dx:GridViewDataTextColumn Caption="用途类别说明" FieldName="note" Width="100px" VisibleIndex="12"></dx:GridViewDataTextColumn> 
                            <dx:GridViewDataDateColumn Caption="签订日期" FieldName="signdate" Width="130px" VisibleIndex="13">
                                <PropertiesDateEdit DisplayFormatString="yyyy/MM/dd"></PropertiesDateEdit>
                            </dx:GridViewDataDateColumn>                          
                            <dx:GridViewDataTextColumn Caption="计划到货日期" FieldName="planreceivedate" Width="130px" VisibleIndex="14" PropertiesTextEdit-EncodeHtml="false">
                            </dx:GridViewDataTextColumn> 
                            <dx:GridViewDataTextColumn Caption="实际到货日期" FieldName="actaulreceivedate" Width="130px" VisibleIndex="15" PropertiesTextEdit-EncodeHtml="false">
                            </dx:GridViewDataTextColumn> 
                            <dx:GridViewDataTextColumn Caption="合同币种" FieldName="currency" Width="55px" VisibleIndex="16"></dx:GridViewDataTextColumn> 
                            <dx:GridViewDataTextColumn Caption="汇率" FieldName="taxrate" Width="70px" VisibleIndex="17">
                                <PropertiesTextEdit DisplayFormatString="{0:N5}"></PropertiesTextEdit>
                            </dx:GridViewDataTextColumn> 
                            <dx:GridViewDataTextColumn Caption="合同原币总金额" FieldName="ori_total_amount" Width="90px" VisibleIndex="18">
                                <PropertiesTextEdit DisplayFormatString="{0:N2}"></PropertiesTextEdit>
                            </dx:GridViewDataTextColumn> 
                            <dx:GridViewDataTextColumn Caption="合同计划原币总金额" FieldName="ori_plan_amount" Width="130px" VisibleIndex="19">
                                <PropertiesTextEdit DisplayFormatString="{0:N3}"></PropertiesTextEdit>
                            </dx:GridViewDataTextColumn> 
                            <dx:GridViewDataDateColumn Caption="计划付款日期" FieldName="plan_pay_date" Width="130px" VisibleIndex="20">
                                <PropertiesDateEdit DisplayFormatString="yyyy/MM/dd"></PropertiesDateEdit>
                            </dx:GridViewDataDateColumn> 
                            <dx:GridViewDataTextColumn Caption="计划付款金额(本币)" FieldName="fkamt_plan_cur" Width="130px" VisibleIndex="21">
                                <PropertiesTextEdit DisplayFormatString="{0:N6}"></PropertiesTextEdit>
                            </dx:GridViewDataTextColumn>  
                            <dx:GridViewDataTextColumn Caption="计划付款比例" FieldName="plan_pay_rate" Width="90px" VisibleIndex="22"></dx:GridViewDataTextColumn> 
                            <dx:GridViewDataDateColumn Caption="实际付款日期" FieldName="fkdate" Width="130px" VisibleIndex="23">
                                <PropertiesDateEdit DisplayFormatString="yyyy/MM/dd"></PropertiesDateEdit>
                            </dx:GridViewDataDateColumn> 
                            <dx:GridViewDataTextColumn Caption="付款金额(原币)" FieldName="fkamt" Width="100px" VisibleIndex="24">
                                <PropertiesTextEdit DisplayFormatString="{0:N2}"></PropertiesTextEdit>
                            </dx:GridViewDataTextColumn> 
                            <dx:GridViewDataTextColumn Caption="付款金额(本币)" FieldName="fkamt_cur" Width="100px" VisibleIndex="25">
                                <PropertiesTextEdit DisplayFormatString="{0:N2}"></PropertiesTextEdit>
                            </dx:GridViewDataTextColumn> 
                            <dx:GridViewDataTextColumn Caption="付款比例" FieldName="fkamt_rate" Width="70px" VisibleIndex="26"></dx:GridViewDataTextColumn>  
                            <dx:GridViewDataTextColumn Caption="累计付款比例" FieldName="fkrate" Width="70px" VisibleIndex="27"></dx:GridViewDataTextColumn> 
                            <dx:GridViewDataTextColumn Caption="余额(原币)" FieldName="ye_oricur" Width="100px" VisibleIndex="28">
                                <PropertiesTextEdit DisplayFormatString="{0:N2}"></PropertiesTextEdit>
                            </dx:GridViewDataTextColumn> 
                            <dx:GridViewDataTextColumn Caption="余额(本币)" FieldName="ye_cur" Width="100px" VisibleIndex="29">
                                <PropertiesTextEdit DisplayFormatString="{0:N2}"></PropertiesTextEdit>
                            </dx:GridViewDataTextColumn> 
                            <dx:GridViewDataTextColumn Caption="余额比例" FieldName="yerate" Width="70px" VisibleIndex="30"></dx:GridViewDataTextColumn> 
                            <dx:GridViewDataTextColumn Caption="验收日期" FieldName="checkdate" Width="130px" VisibleIndex="31" PropertiesTextEdit-EncodeHtml="false">
                            </dx:GridViewDataTextColumn> 
                            <dx:GridViewDataDateColumn Caption="收到发票日期" FieldName="fpdate" Width="130px" VisibleIndex="32">
                                <PropertiesDateEdit DisplayFormatString="yyyy/MM/dd"></PropertiesDateEdit>
                            </dx:GridViewDataDateColumn> 
                            <dx:GridViewDataTextColumn Caption="发票金额(原币)" FieldName="fpamount" Width="100px" VisibleIndex="33">
                                <PropertiesTextEdit DisplayFormatString="{0:N2}"></PropertiesTextEdit>
                            </dx:GridViewDataTextColumn>
                        </Columns>
                        <Styles>
                            <Header BackColor="#99CCFF"></Header>
                            <Footer HorizontalAlign="Right"></Footer>
                            <SelectedRow BackColor="#FDF7D9" ForeColor="Black"></SelectedRow>   
                        </Styles>
                    </dx:ASPxGridView>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>

