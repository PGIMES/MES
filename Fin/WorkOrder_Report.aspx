<%@ Page Title="【工单 会计事务/发料会计事务/历史记录 查询】" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="WorkOrder_Report.aspx.cs" Inherits="Fin_WorkOrder_Report" %>

<%@ Register Assembly="DevExpress.Web.v17.2, Version=17.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <script type="text/javascript" src="/Content/js/jquery.min.js"></script>
    <script src="/Content/js/layer/layer.js"></script>
    <script src="/Content/js/plugins/layer/laydate/laydate.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#mestitle").text("【工单 会计事务/发料会计事务/历史记录 查询】");//工单 会计事务/发料会计事务/历史记录 查询
            setHeight();

            $(window).resize(function () {
                setHeight();
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
                <td style="width:35px;">类别</td>
                <td style="width:165px;"> 
                    <asp:DropDownList ID="ddl_typeno" runat="server" class="form-control input-s-md " Width="160px">
                        <asp:ListItem Value="A">工单会计事务</asp:ListItem>
                        <asp:ListItem Value="B">工单发料会计事务</asp:ListItem>
                        <asp:ListItem Value="C">工单历史记录</asp:ListItem>
                    </asp:DropDownList>
                </td> 
                <td style="width:65px;">申请公司</td>
                <td style="width:125px;">
                    <asp:DropDownList ID="ddl_domain" runat="server" class="form-control input-s-md " Width="120px">
                        <asp:ListItem Value="200">昆山工厂</asp:ListItem>
                        <asp:ListItem Value="100">上海工厂</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td style="width:65px;">生效日期</td>
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
                </td>
            </tr>                      
        </table>                   
    </div>

    <div class="col-sm-12">
        <table>
            <tr>
                <td>
                    <dx:ASPxGridView ID="gv" runat="server" KeyFieldName="id" AutoGenerateColumns="False" OnPageIndexChanged="gv_PageIndexChanged"  ClientInstanceName="grid">
                        <ClientSideEvents EndCallback="function(s, e) {setHeight();}"  />
                        <SettingsPager PageSize="1000" ></SettingsPager>
                        <Settings ShowFilterRow="True" ShowGroupPanel="false" ShowFilterRowMenu="True" ShowFilterRowMenuLikeItem="True" AutoFilterCondition="Contains" 
                            VerticalScrollBarMode="Visible" VerticalScrollBarStyle="Standard" VerticalScrollableHeight="600"  />
                        <SettingsBehavior AllowFocusedRow="True" ColumnResizeMode="Control"  />
                        <Columns> 
                        </Columns>
                        <Styles>
                            <Header BackColor="#99CCFF"></Header>
                            <FocusedRow BackColor="#99CCFF" ForeColor="#0000CC"></FocusedRow>
                            <Footer HorizontalAlign="Right"></Footer>
                        </Styles>
                    </dx:ASPxGridView>
                    <dx:ASPxGridViewExporter ID="gv_export" runat="server" GridViewID="gv">
                    </dx:ASPxGridViewExporter>
                </td>
            </tr>
        </table>
    </div>

</asp:Content>
