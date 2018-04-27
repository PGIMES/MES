<%@ Page Title="采购单查询" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="PO_Report_Query.aspx.cs" Inherits="Forms_PurChase_PO_Report_Query" %>

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

    <script src="../../Content/js/jquery.min.js"  type="text/javascript"></script>
    <script src="../../Content/js/bootstrap.min.js"></script>
    <script src="../../Content/js/plugins/layer/layer.min.js" type="text/javascript"></script>
    <script src="../../Content/js/plugins/layer/laydate/laydate.js"></script>
    <script type="text/javascript">
        $("#mestitle").text("【采购单查询】");
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
                                        <td>采购类别:
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="drop_type" runat="server" class="form-control input-s-sm ">
                                                <asp:ListItem>存货</asp:ListItem>
                                                <asp:ListItem>设备</asp:ListItem>
                                                <asp:ListItem>设施</asp:ListItem>
                                                <asp:ListItem>工夹模具</asp:ListItem>
                                                <asp:ListItem>IT硬件/软件</asp:ListItem>
                                                <asp:ListItem>服务及其他</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        

                                      

                                        <td>创建日期;</td>
                                        <td >
                                   <asp:TextBox ID="txtDateFrom" class="form-control" onclick="laydate()" runat="server" Width="100px"></asp:TextBox>
                                </td>
                                <td>~</td>
                                <td >
                                   <asp:TextBox ID="txtDateTo" class="form-control" onclick="laydate()" runat="server" Width="100px"></asp:TextBox>
                                </td>
                                      
                                       
                                        <td> 
                                            &nbsp;<td>
                                    <asp:Button ID="Bt_select" runat="server" Text="查询" class="btn btn-large btn-primary" OnClick="Button1_Click" Width="100px" />   
                                               
                                            </td>
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

                 

                     <dx:ASPxGridView ID="GV_PART" runat="server" 
                         KeyFieldName="PRNo" AutoGenerateColumns="False"  
                         
                         OnHtmlRowPrepared="GV_PART_HtmlRowPrepared" 
                         OnHtmlRowCreated="GV_PART_HtmlRowCreated" 
                      
                         onhtmldatacellprepared="GV_PART_HtmlDataCellPrepared">
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
                                  <dx:ASPxSummaryItem DisplayFormat="{0:N0}" FieldName="targetzj" 
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


