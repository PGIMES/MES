<%@ Page Title="物料系统【刀具查询】" Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.master" CodeFile="Forproducts.aspx.cs" Inherits="Forproducts" %>


<%@ Register Assembly="DevExpress.Web.v17.2, Version=17.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>



<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <style type="text/css">
        td {
            padding-left: 5px;
            padding-right: 5px;
        }
        .tblCondition td{ white-space:nowrap }
        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <script src="../Content/js/jquery.min.js" type="text/javascript"></script>
    <script src="../Content/js/bootstrap.min.js"></script>
    <script src="../Content/js/plugins/layer/layer.min.js" type="text/javascript"></script>
    <script src="../Content/js/plugins/layer/laydate/laydate.js"></script>
    <script type="text/javascript">
        $("#mestitle").text("【用于产品】");
        jQuery.fn.rowspan = function (colIdx) {//封装jQuery小插件用于合并相同内容单元格(列)  
            return this.each(function () {
                var that;
                $('tr', this).each(function (row) {
                    $('td:eq(' + colIdx + ')', this).filter(':visible').each(function (col) {
                        if (that != null && $(this).html() == $(that).html()) {
                            rowspan = $(that).attr("rowSpan");
                            if (rowspan == undefined) {
                                $(that).attr("rowSpan", 1);
                                rowspan = $(that).attr("rowSpan");
                            }
                            rowspan = Number(rowspan) + 1;
                            $(that).attr("rowSpan", rowspan);
                            $(this).hide();
                        } else {
                            that = this;
                        }
                    });
                });
            });
        }

        $(function () {//第一列内容相同的进行合并  
            // $("#MainContent_GridView1").rowspan(1);//传入的参数是对应的列数从0开始，哪一列有相同的内容就输入对应的列数值  
        });
    </script>
    <div >
        <div >
            <div >
<%--                <div class="panel-heading">
                    《刀具》查询</div>--%>
                <div class="panel-body">
                    <div class="col-sm-12">
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div runat="server" id="DIV1" style="margin-left: 5px; margin-right: 5px; margin-bottom: 10px">
        <table>
            <tr>
                <td>
                     <dx:ASPxGridView ID="gv_pt" runat="server" AutoGenerateColumns="False" >
                    <SettingsPager PageSize="1000" >
            </SettingsPager>
        <%--    <Settings ShowFilterRow="True" ShowFilterRowMenu="True" ShowFilterRowMenuLikeItem="True"  />--%>
            <SettingsSearchPanel Visible="True" />
            <SettingsFilterControl AllowHierarchicalColumns="True">
            </SettingsFilterControl>
                         <Columns>
                             <dx:GridViewDataTextColumn Caption="序号" FieldName="序号" VisibleIndex="0" Width="5px">
                                 <Settings AllowAutoFilter="False" />
                             </dx:GridViewDataTextColumn>
                             <dx:GridViewDataTextColumn Caption="物料号" FieldName="pt_part" VisibleIndex="1" Width="60px">
                             </dx:GridViewDataTextColumn>
                      
                             <dx:GridViewDataTextColumn Caption="工序号" FieldName="xzgxh" VisibleIndex="3" Width="60px">
                             </dx:GridViewDataTextColumn>
                             <dx:GridViewDataTextColumn Caption="工序名称" FieldName="gxmc" VisibleIndex="4" Width="50px">
                                    </dx:GridViewDataTextColumn>
                             <dx:GridViewDataTextColumn Caption="用于产品" FieldName="pgixmh2" VisibleIndex="4" Width="50px">
                                    </dx:GridViewDataTextColumn>
                             <dx:GridViewDataTextColumn Caption="零件号" FieldName="ljh" VisibleIndex="4" Width="50px">
                                    </dx:GridViewDataTextColumn>
                             <dx:GridViewDataTextColumn Caption="零件名称" FieldName="LJMC" VisibleIndex="4" Width="50px">
                             </dx:GridViewDataTextColumn>
                              <dx:GridViewDataTextColumn Caption="寿命调整系数" FieldName="smtzxs" VisibleIndex="4" Width="50px">
                             </dx:GridViewDataTextColumn>
                         </Columns>
                <Styles>
                <Header BackColor="#99CCFF">
                </Header>
            </Styles>
        </dx:ASPxGridView> 
                </td>
            </tr>
                <tr>
                <td>
                    &nbsp;</td>
            </tr>

        </table>
    </div>
</asp:Content>
