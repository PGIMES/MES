<%@ Page Title="【刀具领用查询】" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="DJ_LY_Query.aspx.cs" Inherits="MaterialBase_DJ_LY_Query" %>

<%@ Register Assembly="DevExpress.Web.v17.2, Version=17.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>






<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <style type="text/css">
        /*td {
            padding-left: 5px;
            padding-right: 5px;
        }*/
        .tblCondition td{ white-space:nowrap }
        /*.dxgvHeader td {
            white-space:normal; 
        }*/
        .dx-wrap{
     white-space:  inherit; 
    line-height: normal;
    padding: 0;
}
        /*自动隐藏文字*/=====1行
.public-ellipsis-1 {
    overflow: hidden;
    white-space: nowrap;
    text-overflow: ellipsis;
}
/*自动隐藏文字*/=====2行
.public-ellipsis-2 {
    display: -webkit-box !important;
    overflow: hidden !important;
    text-overflow: ellipsis !important;
    word-wrap: break-word !important;
    white-space: normal !important;
    -webkit-line-clamp: 2 !important;
    -webkit-box-orient: vertical !important;
}
/*自动隐藏文字*/=====3行
.public-ellipsis-3 {
    display: -webkit-box !important;
    overflow: hidden !important;
    text-overflow: ellipsis !important;
    word-wrap: break-word !important;
    white-space: normal !important;
    -webkit-line-clamp: 3 !important;
    -webkit-box-orient: vertical !important;

}
          border{border:solid 1px red}        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">

    <script src="../Content/js/jquery.min.js" type="text/javascript"></script>
    <script src="../Content/js/bootstrap.min.js"></script>
    <script src="../Content/js/plugins/layer/layer.min.js" type="text/javascript"></script>
    <script src="../Content/js/plugins/layer/laydate/laydate.js"></script>
    <script type="text/javascript">
        $("#mestitle").text("【刀具领用查询】");
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
            <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
     
    <div >
        <div >
            <div >
                <%--      <dx:GridViewDataHyperLinkColumn Caption="物料号" FieldName="物料号"  VisibleIndex="8"  PropertiesHyperLinkEdit-NavigateUrlFormatString="Forproducts.aspx?wlh={0}"  PropertiesHyperLinkEdit-Target="_blank"> 

                            </dx:GridViewDataHyperLinkColumn>--%>
                <div class="panel-body">
                    <div class="col-sm-12">
                        <table class="tblCondition" >
                            <tr>
                                <td>工厂：</td>
                                <td> <asp:DropDownList ID="ddl_comp" runat="server" class="form-control input-s-sm " Width="100px">
                                          <asp:ListItem Value="">ALL</asp:ListItem>
                                          <asp:ListItem Value="100">上海工厂</asp:ListItem>
                                          <asp:ListItem Value="200">昆山工厂</asp:ListItem>
                                           </asp:DropDownList>
                                </td>
                                <td>部门：</td>
                                <td> <asp:DropDownList ID="ddl_dept" runat="server" class="form-control input-s-sm " Width="90px">
                                          <asp:ListItem Value="">ALL</asp:ListItem>
                                          <asp:ListItem Value="一车间">一车间</asp:ListItem>
                                          <asp:ListItem Value="二车间">二车间</asp:ListItem>
                                          <asp:ListItem Value="三车间">三车间</asp:ListItem>
                                          <asp:ListItem Value="四车间">四车间</asp:ListItem>
                                           </asp:DropDownList>
                                </td>
                                <td>
                                    物料号:
                                </td>
                                 <td >
                                   <asp:TextBox ID="txtwlh" class="form-control"  runat="server" Width="100px"></asp:TextBox>
                                </td>
                                                        <td>
                                    领用日期:
                                </td>
                                 <td >
                                   <asp:TextBox ID="txtDateFrom" class="form-control" onclick="laydate()" runat="server" Width="100px"></asp:TextBox>
                                </td>
                                <td>~</td>
                                <td >
                                   <asp:TextBox ID="txtDateTo" class="form-control" onclick="laydate()" runat="server" Width="100px"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Button ID="Bt_select" runat="server" Text="查询" class="btn btn-large btn-primary" OnClick="Button1_Click" />   </td>
                                   
                                  
                                     <td> &nbsp;
                                    <asp:LinkButton ID="btn_tj" runat="server" Text="查看领用趋势"  style="color: white"
                                            class="btn btn-large btn-primary"  Width="110px" 
                                             href="/sales/Sale_DJQuery.aspx"  target="_blank" />
                                    </td>
                                   
                                    <td> &nbsp;
                                    <asp:LinkButton ID="Bt_qs" runat="server" Text="查看领用统计"  style="color: white"
                                            class="btn btn-large btn-primary"  Width="110px" 
                                             href="/MaterialBase/DJ_LY_QS.aspx"  target="_blank" />
                                    </td>
                                    
                                    <td>&nbsp;
                                    <asp:LinkButton ID="Bt_amt" runat="server" Text="Top领用金额"  style="color: white"
                                            class="btn btn-large btn-primary"  Width="110px" 
                                             href="/MaterialBase/DJLY_BYTop.aspx"  target="_blank" />
                                   
                                    </td>
                                    
                                     <td>&nbsp;
                                    <asp:LinkButton ID="Btn_CBSS" runat="server" Text="Top成本损失"  style="color: white"
                                            class="btn btn-large btn-primary"  Width="110px" 
                                             href="/MaterialBase/DJ_LY_CBSS.aspx"  target="_blank" />
                                    </td>
                                    
                                     <td>&nbsp;
                                    <asp:LinkButton ID="Btn_YC" runat="server" Text="Top异常"  style="color: white"
                                            class="btn btn-large btn-primary"  Width="110px" 
                                             href="/MaterialBase/DJ_LY_YC.aspx"  target="_blank" />

                                    </td>
                           
                            </tr>
                          
                            </table>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div>
          <table>
            <tr>
                <td>

                 

                     <dx:ASPxGridView ID="GV_PART" runat="server" KeyFieldName="tr_part" AutoGenerateColumns="False"  OnHtmlDataCellPrepared="GV_PART_HtmlDataCellPrepared" OnHtmlRowPrepared="GV_PART_HtmlRowPrepared" OnHtmlRowCreated="GV_PART_HtmlRowCreated">
                         <SettingsBehavior AllowDragDrop="TRUE" AllowFocusedRow="True" AllowSelectByRowClick="True" ColumnResizeMode="Control" />
              <SettingsPager PageSize="1000">
                     
                </SettingsPager>
                  <Settings ShowFilterRow="True" ShowFilterRowMenu="True" 
                             ShowFilterRowMenuLikeItem="True"   ShowGroupPanel="True" 
                             ShowFooter="True"/>
            <SettingsSearchPanel Visible="True" />

<Settings ShowFilterRow="True" ShowFilterRowMenu="True" 
                             ShowFilterRowMenuLikeItem="True" ShowGroupPanel="True" 
                             ShowFooter="True" showgroupedcolumns="True"></Settings>

<SettingsBehavior ColumnResizeMode="Control" AllowFocusedRow="True" 
                             AllowSelectByRowClick="True" autoexpandallgroups="True" 
                             mergegroupsmode="Always" sortmode="Value"></SettingsBehavior>

<SettingsSearchPanel Visible="True"></SettingsSearchPanel>

            <SettingsFilterControl AllowHierarchicalColumns="True">
            </SettingsFilterControl>
            <Columns>
               
            </Columns>
                              <TotalSummary>
                                  <dx:ASPxSummaryItem DisplayFormat="{0:N0}" FieldName="amt" 
                                      SummaryType="Sum" />
                                  <dx:ASPxSummaryItem DisplayFormat="{0:N0}" 
                                      FieldName="sjxds" SummaryType="Sum" />
                         </TotalSummary>
                              <Styles>
                <Header BackColor="#99CCFF">
                </Header>
                                  <FocusedRow BackColor="#99CCFF" ForeColor="#0000CC">
                                  </FocusedRow>
                                  <Footer HorizontalAlign="Right">
                                  </Footer>
            </Styles>
        </dx:ASPxGridView>
                </td>
            </tr>
                <tr>
                <td>
                    &nbsp;</td>
            </tr>
               <tr>
                <td>
                    &nbsp;</td>
            </tr>
        </table>
    
    </div>
</asp:Content>

