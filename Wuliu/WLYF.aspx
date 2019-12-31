<%@ Page Title="物流运费" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="WLYF.aspx.cs" Inherits="Wuliu_WLYF" %>

<%@ Register Assembly="DevExpress.Web.v17.2, Version=17.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script src="/Content/js/jquery.min.js"  type="text/javascript"></script>
    <script src="/Content/js/bootstrap.min.js"></script>
    <script src="/Content/js/layer/layer.js"></script>
    <script src="/Content/js/plugins/layer/laydate/laydate.js"></script>
    <script type="text/javascript">

        $(document).ready(function () {
            $("#mestitle").text("【物流运费查询】");

             setHeight();
            $(window).resize(function () {
                setHeight();
            });


        });

        function setHeight() {
             $("div[class=dxgvCSD]").css("height", ($(window).height() - $("#div_p").height() - 220) + "px");

            $("#MainContent_GV_PART").css("width", ($(window).width() - 10) + "px");
            $("div[class=dxgvCSD]").css("width", ($(window).width() - 10) + "px");

            $("#MainContent_GV_PART_DK").css("width", ($(window).width() - 10) + "px");
            $("#MainContent_GV_PART_YK").css("width", ($(window).width() - 10) + "px");
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
                    <td>&nbsp;
                        <button id="Bt_select" type="button" class="btn btn-primary btn-large" runat="server" onserverclick="Bt_select_Click"><i class="fa fa-search fa-fw"></i>&nbsp;查询</button>    
                        <button id="Bt_Export" type="button" class="btn btn-primary btn-large" runat="server" onserverclick="Bt_Export_Click"><i class="fa fa-download fa-fw"></i>&nbsp;导出</button>
                    </td>                           
                </tr>                          
            </table>
        </div>
    </div>

    <div runat="server" id="DIV1" style="margin-left: 5px; margin-right: 5px; margin-bottom: 10px">
        <table>
            <tr>
                <td>
                     <dx:ASPxGridView ID="GV_PART" runat="server" KeyFieldName="tr_part" AutoGenerateColumns="False" 
                         OnPageIndexChanged="GV_PART_PageIndexChanged" Width="1000px">
                        <SettingsBehavior AllowDragDrop="TRUE" AllowFocusedRow="false" AllowSelectByRowClick="false" ColumnResizeMode="Control" AutoExpandAllGroups="true" MergeGroupsMode="Always" SortMode="Value" />
                        <SettingsPager PageSize="100"></SettingsPager>
                        <Settings ShowFilterRow="True" ShowFilterRowMenu="True"   VerticalScrollBarMode="Visible" VerticalScrollBarStyle="Standard" VerticalScrollableHeight="500"
                                 ShowFilterRowMenuLikeItem="True"  ShowFooter="True"  HorizontalScrollBarMode="Auto" />
                        <Columns>
               
                        </Columns>
                        <TotalSummary>
                            <dx:ASPxSummaryItem DisplayFormat="{0:N2}" FieldName="USD金额"  SummaryType="Sum" />
                            <dx:ASPxSummaryItem DisplayFormat="{0:N2}" FieldName="CNY金额" SummaryType="Sum" />                                    
                        </TotalSummary>      
                        <Styles>
                            <Header BackColor="#99CCFF"></Header>
                            <FocusedRow BackColor="#99CCFF" ForeColor="#0000CC"></FocusedRow>
                            <Footer HorizontalAlign="Right"></Footer>
                        </Styles>
                    </dx:ASPxGridView>
                    <dx:ASPxGridViewExporter ID="ASPxGridViewExporter1" runat="server">
                    </dx:ASPxGridViewExporter>
                </td>
            </tr>
        </table>
    </div>

</asp:Content>


