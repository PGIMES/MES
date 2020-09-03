<%@ Page Title="【验收池查询】" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Pur_AcceptPool.aspx.cs" Inherits="Pur_AcceptPool" %>

<%@ Register Assembly="DevExpress.Web.v17.2, Version=17.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script type="text/javascript" src="/Content/js/jquery.min.js"></script>
    <script src="/Content/js/layer/layer.js"></script>
    <script src="/Content/js/plugins/layer/laydate/laydate.js"></script>

    <script type="text/javascript">
        $(document).ready(function () {
            $("#mestitle").html("【验收池查询】<a href='/userguide/RCTGuide.pptx' target='_blank' class='h5' style='color:red'>使用说明</a>");
            setHeight();

            $(window).resize(function () {
                setHeight();
            });

            $("#lnkcheck").click(function () {


            })
        });

        function setHeight() {
            $("div[class=dxgvCSD]").css("height", ($(window).height() - $("#div_p").height() - 160) + "px");
        }

        var contractno = ""; var pono = ""; var rowid = ""; var mc = "";
        var ms = ""; var qty = ""; var date = ""; var mscode = ""; var domain = "";
        var rid = ""; var status = "", lb = ""; poqty = "";

        function OnGridFocusedRowChanged() {
            gv.GetRowValues(gv.GetFocusedRowIndex(), "合同号;采购单号;采购行号;名称;描述;到货数量;到货时间;MS_CODE;domain;rid;验收状态;LB;采购数量", OnGetRowValues);

        }

        function OnGetRowValues(values) {
            contractno = values[0] == null ? "" : values[0];
            pono = values[1] == null ? "" : values[1];
            rowid = values[2] == null ? "" : values[2];
            mc = values[3] == null ? "" : encodeURIComponent(values[3]);
            ms = values[4] == null ? "" : encodeURIComponent(values[4]);
            qty = values[5] == null ? "" : values[5];
            date = values[6];//== null ? "" : encodeURIComponent(values[6]);
            mscode = values[7] == null ? "" : values[7];
            domain = values[8] == null ? "" : values[8];
            rid = values[9] == null ? "" : values[9];
            status = values[10];
            lb = encodeURIComponent(values[11]);
            poqty = values[12];
            //debugger;
            qty = qty == "" || qty == 0 ? poqty : qty;

            if (date != null) {
                date = encodeURIComponent(dateFtt("yyyy-MM-dd", date));
            } else {
                date = "";
            }




            if (status != "未验收") {
                $("#lnkcheck").removeAttr("href").removeAttr("target").attr("disabled", "disabled");
            }
            else {
                var url = "/Platform/WorkFlowRun/Default.aspx?flowid=97B862F1-0FD8-4626-80DC-5A8AFC57F61A&appid=&contractno="
                   + contractno + "&pono=" + pono + "&rowid=" + rowid + "&mc=" + mc + "&ms=" + ms + "&qty=" + qty + "&date=" + date + "&mscode=" + mscode + "&domain=" + domain + "&lb=" + lb + "&rid=" + rid;

                $("#lnkcheck").attr("href", (url)).attr("target", "_blank").removeAttr("disabled");

            }

            //alert((mc))
        }
        //格式化日期
        function dateFtt(fmt, date) { //author: meizz   
            var o = {
                "M+": date.getMonth() + 1,                 //月份   
                "d+": date.getDate(),                    //日   
                "h+": date.getHours(),                   //小时   
                "m+": date.getMinutes(),                 //分   
                "s+": date.getSeconds(),                 //秒   
                "q+": Math.floor((date.getMonth() + 3) / 3), //季度   
                "S": date.getMilliseconds()             //毫秒   
            };
            if (/(y+)/.test(fmt))
                fmt = fmt.replace(RegExp.$1, (date.getFullYear() + "").substr(4 - RegExp.$1.length));
            for (var k in o)
                if (new RegExp("(" + k + ")").test(fmt))
                    fmt = fmt.replace(RegExp.$1, (RegExp.$1.length == 1) ? (o[k]) : (("00" + o[k]).substr(("" + o[k]).length)));
            return fmt;
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

    <div class="col-md-12" id="div_p" style="margin-bottom: 5px">
        <table style="border-collapse: collapse;">
            <tr>
                <%--<td style="width:35px;">域</td>
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
                </td>  --%>
                <td style="width: 65px;">关键字</td>
                <td style="width: 125px;">
                    <asp:TextBox ID="keyword" runat="server" class="form-control" Width="200px" holdplace="单号,名称, 描述" />
                </td>
                <td id="td_btn">
                    <button id="btn_search" type="button" class="btn btn-primary btn-large" runat="server" onserverclick="btn_search_Click"><i class="fa fa-search fa-fw"></i>&nbsp;查询</button>
                    <button id="btn_export" type="button" class="btn btn-primary btn-large" runat="server" onserverclick="btn_export_Click"><i class="fa fa-download fa-fw"></i>&nbsp;导出</button>
                    <a id="lnkcheck" class="btn btn-primary btn-large " style="color: white"><i class="fa fa-check fa-fw"></i>&nbsp;到货验收</a>
                    <a id="lnkCheckFree" class="btn btn-primary btn-large " style="color: white" href="Pur_RCT_Free.aspx" target="_blank"><i class="fa fa-check fa-fw"></i>其他验收</a>
                </td>
            </tr>
        </table>
    </div>

    <div class="col-sm-12-">
        <table>
            <tr>
                <td>
                    <dx:ASPxGridView ID="gv" runat="server" KeyFieldName="id" AutoGenerateColumns="False" Width="1300px" OnCustomCellMerge="gv_CustomCellMerge" OnPageIndexChanged="gv_PageIndexChanged" ClientInstanceName="gv">
                        <ClientSideEvents EndCallback="function(s, e) {setHeight();}" FocusedRowChanged="function(s, e) { OnGridFocusedRowChanged(); }" />
                        <SettingsPager PageSize="100"></SettingsPager>
                        <Settings ShowFilterRow="True" ShowGroupPanel="false" ShowFilterRowMenu="True" ShowFilterRowMenuLikeItem="True" AutoFilterCondition="Contains"
                            VerticalScrollBarMode="Visible" VerticalScrollBarStyle="Standard" VerticalScrollableHeight="600" />
                        <SettingsBehavior AllowFocusedRow="True" ColumnResizeMode="Control" />
                        <Columns>
                            <dx:GridViewDataTextColumn Caption="域" FieldName="domain" Width="30px" VisibleIndex="1"></dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="请购单号" FieldName="请购单号" Width="100px" VisibleIndex="2"></dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="请购行号" FieldName="请购行号" Width="45px" VisibleIndex="3" HeaderStyle-Wrap="True" Settings-ShowFilterRowMenu="False">
                                <Settings ShowFilterRowMenu="False" AllowHeaderFilter="False" AllowSort="False"></Settings>

                                <HeaderStyle Wrap="True"></HeaderStyle>
                            </dx:GridViewDataTextColumn>

                            <dx:GridViewDataTextColumn Caption="采购单号" FieldName="采购单号" Width="65px" VisibleIndex="4"></dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="采购行号" FieldName="采购行号" Width="45px" VisibleIndex="5" HeaderStyle-Wrap="True">
                                <HeaderStyle Wrap="True"></HeaderStyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="合同号" FieldName="合同号" Width="80px" VisibleIndex="6"></dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="名称" FieldName="名称" Width="150px" VisibleIndex="7"></dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="描述" FieldName="描述" Width="210px" VisibleIndex="8"></dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="采购数量" FieldName="采购数量" Width="45px" VisibleIndex="9" HeaderStyle-Wrap="True">
                                <HeaderStyle Wrap="True"></HeaderStyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="到货数量" FieldName="到货数量" Width="45px" VisibleIndex="10" HeaderStyle-Wrap="True">
                                <HeaderStyle Wrap="True"></HeaderStyle>
                            </dx:GridViewDataTextColumn>
                            <%--<dx:GridViewDataTextColumn Caption="验收数量" FieldName="验收数量" Width="45px" VisibleIndex="11" HeaderStyle-Wrap="True">
                            <HeaderStyle Wrap="True"></HeaderStyle>
                            </dx:GridViewDataTextColumn> --%>
                            <dx:GridViewDataDateColumn Caption="到货时间" FieldName="到货时间" Width="70px" VisibleIndex="12">
                                <PropertiesDateEdit DisplayFormatString="yyyy/MM/dd"></PropertiesDateEdit>
                            </dx:GridViewDataDateColumn>
                            <dx:GridViewDataTextColumn Caption="供应商" FieldName="VendorName" Width="100px" VisibleIndex="13"></dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="状态" FieldName="验收状态" Width="50px" VisibleIndex="13"></dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="类别" FieldName="LB" Width="100px" VisibleIndex="14"></dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="MS_CODE" FieldName="MS_CODE" Width="100px" VisibleIndex="15" Visible="false"></dx:GridViewDataTextColumn>

                            <dx:GridViewDataTextColumn Caption="rid" FieldName="rid" Width="40px" VisibleIndex="16" Visible="false"></dx:GridViewDataTextColumn>

                            <%--<dx:GridViewDataHyperLinkColumn Caption="验收单号" FieldName="aplno"  Width="140px"  ReadOnly="True" VisibleIndex="16" >
                                <DataItemTemplate>
                                   // <a href="/Platform/WorkFlowRun/Default.aspx?flowid=97B862F1-0FD8-4626-80DC-5A8AFC57F61A&instanceid=<%#Eval("aplno")%>&groupid=<%#Eval("Groupid")%>&display=1" target="_blank"><%#Eval("aplno")%></a>
                                </DataItemTemplate>
                            </dx:GridViewDataHyperLinkColumn>--%>
                            <dx:GridViewDataTextColumn Caption="验收单" FieldName="aplno" Width="40px" VisibleIndex="17">
                                <PropertiesTextEdit EncodeHtml="False">
                                </PropertiesTextEdit>
                            </dx:GridViewDataTextColumn>
                        </Columns>

                        <Styles>
                            <Header BackColor="#99CCFF"></Header>
                            <FocusedRow BackColor="#99CCFF" ForeColor="#0000CC"></FocusedRow>
                            <Footer HorizontalAlign="Right"></Footer>
                        </Styles>
                    </dx:ASPxGridView>

                </td>
            </tr>
        </table>
    </div>
</asp:Content>

