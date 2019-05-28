<%@ Page Title="夹具出入库报表" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="JiaJu_Report_Query.aspx.cs" Inherits="JiaJu_JiaJu_Report_Query" %>
<%@ Register Assembly="DevExpress.Web.v17.2, Version=17.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <style type="text/css">
        .style1
        {
            width: 87px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <script src="../Content/js/jquery.min.js" type="text/javascript"></script>
    <script src="../Content/js/plugins/layer/layer.min.js" type="text/javascript"></script>
    <link href="../Content/css/bootstrap.min.css" rel="stylesheet" />
    <script src="../Content/js/jquery.min.js"></script>
    <script src="../Content/js/bootstrap.min.js"></script>
    <script src="../Content/js/layer/layer.js"></script>
    <script src="../Content/js/plugins/layer/laydate/laydate.js"></script>
    <link href="../Content/js/plugins/bootstrap-select/css/bootstrap-select.min.css" rel="stylesheet" />
 
    <script type="text/javascript">
        $(document).ready(function () {
            $("#mestitle").text("【夹具出入库明细查询】");
//            $("#MainContent_btn_search").click(function () {
//               
//                if ($("#MainContent_txtDateFrom").val() == "" || $("#MainContent_txtDateTo").val() == "") {
//                    alert("请选择【日期区间】！");
//                    return false;
//                }
//            });
            setHeight();

            $(window).resize(function () {
                setHeight();
            });


        });


        function setHeight() {
            $("div[class=dxgvCSD]").css("height", ($(window).height() - $("#div_p").height() - 180) + "px");
        }
         	
    </script>

    <script type="text/javascript">
        var textSeparator = ";";
        function updateText() {
            var selectedItems = checkListBox.GetSelectedItems();
            checkComboBox.SetText(getSelectedItemsText(selectedItems));
        }
        function synchronizeListBoxValues(dropDown, args) {
            checkListBox.UnselectAll();
            var texts = dropDown.GetText().split(textSeparator);
            var values = getValuesByTexts(texts);
            checkListBox.SelectValues(values);
            updateText(); // for remove non-existing texts
        }
        function getSelectedItemsText(items) {
            var texts = [];
            for (var i = 0; i < items.length; i++)
                texts.push(items[i].text);
            return texts.join(textSeparator);
        }
        function getValuesByTexts(texts) {
            var actualValues = [];
            var item;
            for (var i = 0; i < texts.length; i++) {
                item = checkListBox.FindItemByText(texts[i]);
                if (item != null)
                    actualValues.push(item.value);
            }
            return actualValues;
        }


    </script>

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    
    <div class="col-md-12" id="div_p"  style="margin-bottom:5px">
        <table style=" border-collapse: collapse; width: 893px;">
            <tr>
       
                <td>公司别：</td>
                <td>
                    <asp:DropDownList ID="Drop_comp" runat="server" 
                        class="form-control input-s-sm" Width="100px">
                        <asp:ListItem>200</asp:ListItem>
                        <asp:ListItem>100</asp:ListItem>
                    </asp:DropDownList>
                </td>
             <td>日期筛选：</td>
                    <td >
                        <asp:TextBox ID="txtDateFrom" class="form-control" onclick="laydate()" runat="server" Width="100px"></asp:TextBox>
                    </td>
                    <td>~</td>
                    <td>
                        <asp:TextBox ID="txtDateTo" class="form-control" onclick="laydate()" runat="server" Width="100px"></asp:TextBox>
                    </td>
                                       
                   <td >出入库类型：</td>
                <td style="width:130px;">
                  <asp:DropDownList ID="Drop_status" 
                        class="form-control input-s-sm" runat="server"  
                        Width="150px" >
                      <asp:ListItem></asp:ListItem>
                      <asp:ListItem Value="N">领用</asp:ListItem>
                      <asp:ListItem Value="Y">入库</asp:ListItem>
                    </asp:DropDownList>
                </td>        
            </tr>                      
            <tr>
       
      
                <td >零件号：</td>
                <td>
                    <asp:TextBox ID="txt_pn" class="form-control input-s-sm" runat="server" Width="100px"></asp:TextBox>
                </td>    
             <td>领用人：</td>
                <td>
                    <asp:TextBox ID="txt_lyuid" class="form-control input-s-sm" runat="server" Width="120px"></asp:TextBox>
                </td>    
                             <td>夹具号：</td>
                <td>
                    <asp:TextBox ID="txt_jiaju_no" class="form-control input-s-sm" runat="server" Width="110px"></asp:TextBox>
                </td>             
                     <td>  
                    <button id="btn_search" type="button" class="btn btn-primary btn-large" runat="server" onserverclick="btn_search_Click"><i class="fa fa-search fa-fw"></i>&nbsp;查询</button>    
                    &nbsp;    
                    <button id="btn_import" type="button" class="btn btn-primary btn-large" runat="server" onserverclick="btn_import_Click"><i class="fa fa-download fa-fw"></i>&nbsp;导出</button>
                </td>
           
            </tr>                      
        </table>
                   
    </div>

    <div class="col-sm-12">
        <table>
            <tr>
                <td><%-- OnHtmlDataCellPrepared="gv_HtmlDataCellPrepared"--%>
                   <%-- <dx:ASPxGridView ID="gv" runat="server" KeyFieldName="id" AutoGenerateColumns="False" Width="1000px" OnPageIndexChanged="gv_PageIndexChanged"  ClientInstanceName="grid" 
                          OnCustomCellMerge="gv_CustomCellMerge">
                        <SettingsPager PageSize="200" ></SettingsPager>
                        <Settings ShowFilterRow="True" ShowGroupPanel="false" ShowFilterRowMenu="True" ShowFilterRowMenuLikeItem="True" AutoFilterCondition="Contains" 
                            VerticalScrollBarMode="Visible" VerticalScrollBarStyle="Standard" VerticalScrollableHeight="600"  />
                        <SettingsBehavior AllowFocusedRow="True" ColumnResizeMode="Control"  />
                        <Columns>
                            <dx:GridViewDataTextColumn Caption="序号" FieldName="id" Width="80px" VisibleIndex="1"> </dx:GridViewDataTextColumn>
                             <dx:GridViewDataTextColumn Caption="夹具号" FieldName="jiajuno" Width="80px" VisibleIndex="2"> </dx:GridViewDataTextColumn>
                             <dx:GridViewDataTextColumn Caption="零件号"    FieldName="pn" Width="160px" VisibleIndex="3"></dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="零件名称" FieldName="pn_name" Width="150px" VisibleIndex="4"></dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="出库类型" FieldName="ck_type" Width="100px" VisibleIndex="5"> </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="出库日期" FieldName="ck_date" Width="120px" VisibleIndex="6"></dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="领用人" FieldName="ck_uid" Width="60px" VisibleIndex="7"></dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="领用机台号" FieldName="ly_sbno" Width="90px" VisibleIndex="8"></dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="入库类型" FieldName="rk_type" Width="85px" VisibleIndex="9"></dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="入库日期" FieldName="rk_date" Width="85px" VisibleIndex="10"></dx:GridViewDataTextColumn>
                        
                        </Columns>
                        <Styles>
                            <Header BackColor="#99CCFF"></Header>
                            <FocusedRow BackColor="#99CCFF" ForeColor="#0000CC"></FocusedRow>
                            <Footer HorizontalAlign="Right"></Footer>
                        </Styles>
                    </dx:ASPxGridView>--%>
                    <dx:ASPxGridView ID="ASPxGridView1" runat="server" 
                        onpageindexchanged="ASPxGridView1_PageIndexChanged">
                      <SettingsPager PageSize="200" ></SettingsPager>
                        <Settings ShowFilterRow="True" ShowGroupPanel="false" ShowFilterRowMenu="True" ShowFilterRowMenuLikeItem="True" AutoFilterCondition="Contains" 
                            VerticalScrollBarMode="Visible" VerticalScrollBarStyle="Standard" VerticalScrollableHeight="600"  />
                        <SettingsBehavior AllowFocusedRow="True" ColumnResizeMode="Control"  />
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


