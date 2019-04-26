<%@ Page Title="【发货&计划达成率】" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Report_Planning.aspx.cs" Inherits="Wuliu_Report_Planning" %>

<%@ Register Assembly="DevExpress.Web.v17.2, Version=17.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
     <script type="text/javascript" src="/Content/js/jquery.min.js"></script>
    <script src="/Content/js/layer/layer.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#mestitle").text("【发货&计划达成率】");
             setHeight();

             $(window).resize(function () {
                 setHeight();
             });
                 
        });

        function setHeight() {
            $("div[class=dxgvCSD]").css("height", ($(window).height() - $("#div_p").height() - 150) + "px");

            $("#MainContent_gv").css("width", ($(window).width() - 100) + "px")
            $("div[class=dxgvCSD]").css("width", ($(window).width() - 100) + "px");
        }

        function open_upload() {
            var url = "select_plan_upload.aspx";

            layer.open({
                title: '上传计划发货数量：<font color=red>生产1部、生产2部、压铸部、生产4部</font>',
                closeBtn: 2,
                type: 2,
                area: ['500px', '400px'],
                fixed: false, //不固定
                maxmin: true, //开启最大化最小化按钮
                content: url
            });
        }

        function show_detail(dept, typedesc, week) {
            //alert();
            var dept_str = dept;
            if (dept_str == "生产2部") { dept_str = "二车间"; }
            if (dept_str == "生产4部") { dept_str = "四车间"; }
            if (dept_str == "生产1部") { dept_str = "一车间"; }
            if (dept_str == "压铸") { dept_str = "三车间"; }

            var url = "/wuliu/Report_Planning_dtl_new.aspx?dept=" + dept + "&dept_str=" + dept_str + "&typedesc=" + typedesc + "&week=" + week + "&year=" + $("select[name$='ddl_year'] option[selected]").val();

            layer.open({
                title: typedesc+'_明细',
                closeBtn: 2,
                type: 2,
                area: ['900px', '550px'],
                fixed: false, //不固定
                maxmin: true, //开启最大化最小化按钮
                content: url
            });
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

    <div class="col-md-12" id="div_p"  style="margin-bottom:5px">
        <table style=" border-collapse: collapse;">
            <tr>
                <td style="width:30px;">年</td>
                <td style="width:100px;">
                    <asp:DropDownList ID="ddl_year" runat="server" class="form-control input-s-md " Width="90px">
                        <asp:ListItem Value="2019">2019</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <%--<td style="width:30px;">域</td>
                <td style="width:100px;">
                    <asp:DropDownList ID="ddl_domain" runat="server" class="form-control input-s-md " Width="90px">
                        <asp:ListItem Value="200">200</asp:ListItem>
                        <asp:ListItem Value="100">100</asp:ListItem>
                    </asp:DropDownList>
                </td>   --%>  
                <td style="width:30px;">部门</td>
                <td style="width:120px;">
                    <asp:DropDownList ID="ddl_dept" runat="server" class="form-control input-s-md " Width="115px">
                        <asp:ListItem Value=""></asp:ListItem>
                        <asp:ListItem Value="一车间">生产1部</asp:ListItem>
                        <asp:ListItem Value="二车间">生产2部</asp:ListItem>
                        <asp:ListItem Value="三车间">压铸部</asp:ListItem>
                        <asp:ListItem Value="四车间">生产4部</asp:ListItem>
                    </asp:DropDownList>
                </td>   
                <td id="td_btn">  
                    <button id="btn_search" type="button" class="btn btn-primary btn-large" runat="server" onserverclick="btn_search_Click"><i class="fa fa-search fa-fw"></i>&nbsp;查询</button>  
                    <button id="btn_export" type="button" class="btn btn-primary btn-large" runat="server" onserverclick="btn_export_Click"><i class="fa fa-download fa-fw"></i>&nbsp;导出</button>
                    <input type="button"  value="上传计划发货数量" class="btn btn-large btn-primary" style="height:35px;"  onclick="open_upload()" />    
                   <a href='/userguide/uploadformat.xlsx' target='_blank' style='color:red'>upload format</a>
                </td>
            </tr>                      
        </table>                   
    </div>

    <div class="panel-body">
        <div class="col-sm-12">
            <table>
                <tr> <%-- --%>
                    <td>
                        <dx:ASPxGridView ID="gv"  ClientInstanceName="grid" runat="server" KeyFieldName="typedesc;dept" AutoGenerateColumns="False"
                            OnHtmlRowCreated="gv_HtmlRowCreated" OnExportRenderBrick="gv_ExportRenderBrick" Width="1000px">
                            <SettingsPager PageSize="1000" ></SettingsPager>
                            <SettingsBehavior AllowFocusedRow="false" ColumnResizeMode="Control" />
                            <Settings VerticalScrollBarMode="Visible" VerticalScrollBarStyle="Standard" VerticalScrollableHeight="350"
                                 HorizontalScrollBarMode="Visible" />
                            <Columns>
                            </Columns>
                            <Styles>
                                <Header BackColor="#99CCFF"></Header>
                                <FocusedRow BackColor="#99CCFF" ForeColor="#0000CC"></FocusedRow>
                                <Footer HorizontalAlign="Right"></Footer>
                                <AlternatingRow BackColor="#f2f3f2"></AlternatingRow>
                                <%--<FixedColumn BackColor="Yellow"></FixedColumn>--%>
                            </Styles>
                            <SettingsExport EnableClientSideExportAPI="true"></SettingsExport>
                        </dx:ASPxGridView>
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>

