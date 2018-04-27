<%@ Page Language="C#" AutoEventWireup="true" CodeFile="~/select/SZ_report_dev_select.aspx.cs" Inherits="SZ_report_dev_select" %>

<%@ Register Assembly="DevExpress.Web.v17.2, Version=17.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
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
        .auto-style1 {
            font-size: x-small;
        }
    </style>

    <script src="../Content/js/jquery.min.js" type="text/javascript"></script>
    <script src="../Content/js/bootstrap.min.js"></script>
    <script src="../Content/js/plugins/layer/layer.min.js" type="text/javascript"></script>
    <script src="../Content/js/plugins/layer/laydate/laydate.js"></script>
    <script type="text/javascript">
        $("#mestitle").text("【刀具查询】");
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

        function GetSelectedFieldValues_Callback(result) {
           // var msg = "";
            if (result.length > 0) {

                //for (var i = 0; i < result.length; i++) {
                //    msg = msg + result[i].toString();
                //}
                var lstr = result[0].toString();
                var ldata = lstr.split(",");
                var lsvlh = ldata[0].toString();
                var lsms = ldata[1].toString();
               // var lslx = ldata[2].toString();
                var lslx= $('#DDL_type').val();
                var lsjs = ldata[3].toString();
                if (lsjs=="") {
                    lsjs="1";
                }
                var lsdjedsm = ldata[4].toString();
                var lspp =ldata[5].toString();
                var lsgys =ldata[6].toString();
                var lsxyph =ldata[7].toString();
                if (lsxyph=="死亡" || lsxyph=="停用") {
                    alert("选用偏号为停用的刀具无法选择!");
                    return;
                }
                //if (ldata[2].toString()=="消耗品" && lsdjedsm=="") {
                //    alert("消耗品物料额定寿命不能为空!");
                //    return;
                //}
                window.opener.setvalue_dj(<%=nid.ToString()%>, lsvlh, lsms,lslx, lsjs, lsdjedsm, lspp, lsgys);
               
            }
            //alert(msg);
        }
    </script>
</head>
<body >
    <form runat="server" id="form1">
    <div >
        <div >
            <div >
          
                <div class="panel-body">
                    <div class="col-sm-12">
                        <table class="tblCondition"  >
                            <tr>
                                <td>刀具类型：</td>
                                <td> <asp:DropDownList ID="DDL_type" class="form-control input-s-sm" Style="height: 35px; width: 150px" runat="server" OnSelectedIndexChanged="DDL_type_SelectedIndexChanged" AutoPostBack="True"> </asp:DropDownList>
                                                        </td>
                                <td> 
                                                        <input id="txtBASE" class="form-control input-s-sm" style="height: 35px; width: 100px" runat="server" visible="false"  /><input id="txtValue" class="form-control input-s-sm" style="height: 35px; width: 100px" runat="server" visible="false"  /></td>
                                <td> 用于零件号</td>
                                <td> 
                                                        <input id="txtljh" class="form-control input-s-sm" style="height: 35px; width: 150px" runat="server"  /></td>
                                <td> 
                                                        用于工厂</td>
                                <td> 
                                                        <asp:DropDownList ID="DDL_domain" class="form-control input-s-sm" Style="height: 35px; width: 110px" runat="server">
                                                             <asp:ListItem Value="200">昆山工厂</asp:ListItem>
                                                            <asp:ListItem Value="100">上海工厂</asp:ListItem>
                                                           
                                                        </asp:DropDownList>
                                </td>
                                <td>
                                    <asp:Button ID="Bt_select" runat="server" Text="查询" class="btn btn-large btn-primary" OnClick="Button1_Click" Width="100px" /></td>
                           
                            </tr>
                            <tr>
                              
                                <td colspan="6">&nbsp;&nbsp;<font color="red"><span class="auto-style1">选择方式：1、双击选择；2、点击选中行后点击首列"选择"链接。</span></font></td>
                            </tr>
                          
                            </table>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div runat="server" id="DIV1" style="margin-left: 5px; margin-right: 5px; margin-bottom: 10px">
        <table>
            <tr>
                <td>

                    <dx:ASPxGridView ID="GV_PART" runat="server" KeyFieldName="wlh" Width="1000px" ClientInstanceName="grid" OnHtmlRowCreated="GV_PART_HtmlRowCreated">
                         <SettingsBehavior AllowDragDrop="TRUE" AllowFocusedRow="True" AllowSelectByRowClick="True" ColumnResizeMode="Control" />
                         <ClientSideEvents  CustomButtonClick="function(s, e) {
	grid.GetSelectedFieldValues('wlh;ms;djlx;djjs;edsm;pp;gys;xyph', GetSelectedFieldValues_Callback);
}" RowDblClick="function(s, e) {
	grid.GetSelectedFieldValues('wlh;ms;djlx;djjs;edsm;pp;gys;xyph', GetSelectedFieldValues_Callback);
}"  />
              <SettingsPager PageSize="20">
                     
                </SettingsPager>
                  <Settings ShowFilterRow="True" ShowFilterRowMenu="True" ShowFilterRowMenuLikeItem="True"  ShowFooter="True"/>
                         <SettingsCommandButton>
                             <SelectButton ButtonType="Button" RenderMode="Button">
                             </SelectButton>
                         </SettingsCommandButton>
            <SettingsSearchPanel Visible="True" />
            <SettingsFilterControl AllowHierarchicalColumns="True">
            </SettingsFilterControl>
            <Columns>
          
            </Columns>
                             <TotalSummary>
                                  <dx:ASPxSummaryItem DisplayFormat="{0:N0}" 
                                      FieldName="year_jg_xd"  SummaryType="Sum" />
                                  <dx:ASPxSummaryItem DisplayFormat="{0:N0}" 
                                      FieldName="year_jg_xmd" SummaryType="Sum" />
                                     <dx:ASPxSummaryItem DisplayFormat="{0:N0}" 
                                      FieldName="year_jg" SummaryType="Sum" />
                                    <dx:ASPxSummaryItem DisplayFormat="{0:N0}" 
                                      FieldName="zpjyl" SummaryType="Sum" />
                                  <dx:ASPxSummaryItem DisplayFormat="{0:N0}" 
                                      FieldName="lycs_xd" SummaryType="Sum" />
                                  <dx:ASPxSummaryItem DisplayFormat="{0:N0}" 
                                      FieldName="lycs_xmd" SummaryType="Sum" />
                                 
                         </TotalSummary>
                              <Styles>
                <Header BackColor="#99CCFF">
                </Header>
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
                    
                   </td>
            </tr>
        </table>
    </div>
    </form>
</body></html>