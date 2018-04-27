<%@ Page Title="【产品刀具成本】" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Product_Dj_CBQuery.aspx.cs" Inherits="MaterialBase_Product_Dj_CBQuery" %>

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
          border{border:solid 1px red}       

             .hidden { display:none;}
		     .hidden1
            {
            	border:0px; 
            	overflow:hidden
            	
            	}
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">

    <script src="../Content/js/jquery.min.js" type="text/javascript"></script>
    <script src="../Content/js/bootstrap.min.js"></script>
    <script src="../Content/js/plugins/layer/layer.min.js" type="text/javascript"></script>
    <script src="../Content/js/plugins/layer/laydate/laydate.js"></script>
    <script type="text/javascript">
        $("#mestitle").text("【产品刀具成本】");
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
     
     <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <div class="panel-body">
                            <div class="col-sm-12">

                                <table>

                                    <tr>
                                        <td>年份:
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="drop_year" runat="server" class="form-control input-s-sm ">
                                            </asp:DropDownList>
                                        </td>
                                        

                                      

                                        <td>&nbsp;工厂:&nbsp;</td>
                                        <td>
                                            <asp:DropDownList ID="ddl_comp" runat="server" class="form-control input-s-sm ">
                                                <asp:ListItem Value="">全部</asp:ListItem>
                                                <asp:ListItem Value="200">昆山</asp:ListItem>
                                                <asp:ListItem Value="100">上海</asp:ListItem>
                                            </asp:DropDownList></td>

                                      
                                        <td>
                                    产品大类:
                                </td>
                                 <td><asp:DropDownList ID="dropdl" runat="server" class="form-control input-s-sm ">
                                           </asp:DropDownList></td>

                                           <td>显示数量: </td>
                                        <td> 
                                            <asp:DropDownList ID="dropnum" runat="server" class="form-control input-s-sm ">
                                    <asp:ListItem Value="20">20</asp:ListItem>
                                    <asp:ListItem Value="50">50</asp:ListItem>
                                    <asp:ListItem Value="100">100</asp:ListItem>
                                    <asp:ListItem Value="全部">全部</asp:ListItem>
                                           </asp:DropDownList><td>
                                    <asp:Button ID="Bt_select" runat="server" Text="查询" class="btn btn-large btn-primary" OnClick="Button1_Click" Width="100px" />   </td>
                                    </tr>

                                     

                                    


                                </table>
                            </div>

                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger ControlID="Bt_select" />

                    </Triggers>
                </asp:UpdatePanel>

    <div runat="server" id="DIV1" style="margin-left: 5px; margin-right: 5px; margin-bottom: 10px">
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
                                  <dx:ASPxSummaryItem DisplayFormat="{0:N0}" FieldName="cb_per" 
                                      SummaryType="Sum" />
                                       <dx:ASPxSummaryItem DisplayFormat="{0:N0}" FieldName="ed_ncb" 
                                      SummaryType="Sum" />
                                      <dx:ASPxSummaryItem DisplayFormat="{0:N0}" FieldName="eddjcb" 
                                      SummaryType="Sum" />
                                       <dx:ASPxSummaryItem DisplayFormat="{0:N0}" FieldName="eddj_actnum" 
                                      SummaryType="Sum" />
                                      <dx:ASPxSummaryItem DisplayFormat="{0:N0}" FieldName="sjncb" 
                                      SummaryType="Sum" />
                                      <dx:ASPxSummaryItem DisplayFormat="{0:N0}" FieldName="year_cbss" 
                                      SummaryType="Sum" />
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



