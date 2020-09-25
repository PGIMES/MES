<%@ Page Title="【文件收发查询】" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Dcc_Report_Query.aspx.cs" Inherits="Forms_Document_Dcc_Report_Query" %>

<%@ Register Assembly="DevExpress.Web.v17.2, Version=17.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <script type="text/javascript" src="../../Content/js/jquery.min.js"></script>
    <script type="text/javascript" src="../../Content/js/jquery.cookie.min.js"></script>
    <script src="../../Content/js/layer/layer.js"></script>

    <script type="text/javascript">
        $(document).ready(function () {
            $("#mestitle").text("【文件收发查询】");

            $('#btn_add').click(function () {
                window.open('/Platform/WorkFlowRun/Default.aspx?flowid=a46c47ad-1e1b-47c3-a7b2-6859ea45b7d7&appid=')
            });

            $('#btn_edit').click(function () {
                if (grid.GetSelectedRowCount() <= 0) { layer.alert("请选择一条记录!"); return; }
//                string part,string type,string filetype, string formno,string filename
                grid.GetSelectedFieldValues('part;type;FileType;formno;filename', function GetVal(values) {
                    var formno = values[0][3];
                    var part = values[0][0];
                    var type = values[0][1];
                    var filetype = values[0][2];
                    var filename = values[0][4];
                 

                    $.ajax({
                        type: "post",
                        url: "Dcc_Report_Query.aspx/CheckData",
                        data: "{'part':'" + part + "','type':'" + type + "','filetype':'" + filetype + "','formno':'" + formno + "','filename':'" + filename + "'}",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        async: false, //默认是true，异步；false为同步，此方法执行完在执行下面代码
                        success: function (data) {
                            var obj = eval(data.d);

                            if (obj[0].re_flag != "") {
                                layer.alert(obj[0].re_flag);
                            } else {
                                window.open('/Platform/WorkFlowRun/Default.aspx?flowid=a46c47ad-1e1b-47c3-a7b2-6859ea45b7d7&appid=&state=edit&formno=' + formno);
                            }
                        }

                    });

                });

            });

            setHeight();

            $(window).resize(function () {
                setHeight();
            });

        });


        function setHeight() {
            $("div[class=dxgvCSD]").css("height", ($(window).height() - $("#div_p").height() - 210) + "px");

            $("#MainContent_gv").css("width", ($(window).width() - 10) + "px");
            $("div[class=dxgvCSD]").css("width", ($(window).width() - 10) + "px");
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

    <style>
        .btn{
            padding:6px 6px;
        }
    </style>

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    
    <div class="panel-body" id="div_p">
        <div class="col-sm-12">
            <table style="line-height:40px">
            <tr>
                <td style="width:45px;">项目号</td>
                <td style="width:115px;">
                    <asp:TextBox ID="txt_part" class="form-control input-s-sm" runat="server" Width="110px"></asp:TextBox>
                </td>
                <td style="width:70px;">顾客零件号</td>
                <td style="width:125px;">
                    <asp:TextBox ID="txt_custpart" class="form-control input-s-sm" runat="server" Width="120px"></asp:TextBox>
                </td>    
                <td style="width:80px;">文件类型</td>
                   <td >
                                     <dx:ASPxDropDownEdit ClientInstanceName="checkComboBox" ID="ASPxDropDownEdit1" Width="200px" runat="server" AnimationType="None" CssClass="form-control input-s-md ">
                                        <DropDownWindowStyle BackColor="#EDEDED" />
                                        <DropDownWindowTemplate>
                                            <dx:ASPxListBox Width="100%" ID="listBox" ClientInstanceName="checkListBox" SelectionMode="CheckColumn"
                                                runat="server" Height="200" EnableSelectAll="true">
                                                <FilteringSettings ShowSearchUI="true"/>
                                                <Border BorderStyle="None" />
                                                <BorderBottom BorderStyle="Solid" BorderWidth="1px" BorderColor="#DCDCDC" />
                                                <Items> 
                                   
                                                </Items>
                                                <ClientSideEvents SelectedIndexChanged="updateText" />
                                            </dx:ASPxListBox>
                                            <table style="width: 100%">
                                                <tr>
                                                    <td style="padding: 4px">
                                                        <dx:ASPxButton ID="ASPxButton1" AutoPostBack="False" runat="server" Text="Close" style="float: right">
                                                            <ClientSideEvents Click="function(s, e){ checkComboBox.HideDropDown(); }" />
                                                        </dx:ASPxButton>
                                                    </td>
                                                </tr>
                                            </table>
                                        </DropDownWindowTemplate>
                                        <ClientSideEvents TextChanged="synchronizeListBoxValues" DropDown="synchronizeListBoxValues" />
                                    </dx:ASPxDropDownEdit>
                                </td>
                <td id="td_btn">  
                    <button id="btn_search" type="button" class="btn btn-primary btn-large" runat="server" onserverclick="btn_search_Click"><i class="fa fa-search fa-fw"></i>&nbsp;查询</button>    
                    <button id="btn_add" type="button" class="btn btn-primary btn-large"><i class="fa fa-plus fa-fw"></i>&nbsp;新增</button>  
                    <button id="btn_edit" type="button" class="btn btn-primary btn-large"><i class="fa fa-pencil-square-o fa-fw"></i>&nbsp;编辑</button> 
                    <button id="btn_import" type="button" class="btn btn-primary btn-large" runat="server" onserverclick="btn_import_Click"><i class="fa fa-download fa-fw"></i>&nbsp;导出</button>
                    <%--<asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="/file/2009110018-0001/test.pdf">HyperLink</asp:HyperLink>--%>
                </td>
            </tr>                      
        </table>
       </div>            
    </div>

    <div id="DIV1" style="margin-left: 5px; margin-right: 5px; margin-bottom: 10px">
        <table>
            <tr>
                <td>
                    <dx:ASPxGridView ID="gv" runat="server" KeyFieldName="formno" AutoGenerateColumns="False" Width="1000px" OnPageIndexChanged="gv_PageIndexChanged"  
                        ClientInstanceName="grid" OnCustomSummaryCalculate="gv_CustomSummaryCalculate" OnHtmlRowCreated="gv_HtmlRowCreated">
                        <ClientSideEvents EndCallback="function(s, e) { setHeight(); }" />
                        <SettingsBehavior AllowDragDrop="TRUE" AllowFocusedRow="false" AllowSelectByRowClick="false" ColumnResizeMode="Control" AutoExpandAllGroups="true" MergeGroupsMode="Always" SortMode="Value" />
                        <SettingsPager PageSize="100"></SettingsPager>
                        <Settings ShowFilterRow="True" ShowFilterRowMenu="True"   VerticalScrollBarMode="Visible" VerticalScrollBarStyle="Standard" VerticalScrollableHeight="500"
                                 ShowFilterRowMenuLikeItem="True"  ShowFooter="True"  HorizontalScrollBarMode="Auto" />
                        <Columns>        
                            <dx:GridViewCommandColumn  ShowClearFilterButton="true" ShowSelectCheckbox="true" Name="Sel" Width="40" VisibleIndex="0"  SelectAllCheckboxMode="Page"  />
                            <dx:GridViewDataDateColumn Caption="申请日期" FieldName="ApplyDate" Width="90px" VisibleIndex="1" >
                                <PropertiesDateEdit DisplayFormatString="yyyy/MM/dd"></PropertiesDateEdit>
                            </dx:GridViewDataDateColumn>     
                              <dx:GridViewDataTextColumn Caption="申请人" FieldName="ApplyName" Width="60px" VisibleIndex="2" />  
                             <dx:GridViewDataTextColumn Caption="表单号" FieldName="formno" Width="150px" VisibleIndex="2" />       
                            <dx:GridViewDataTextColumn Caption="PGI项目号" FieldName="part" Width="70px" VisibleIndex="3" >
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="顾客零件号" FieldName="Pn" Width="120px" VisibleIndex="4" />
                            <dx:GridViewDataTextColumn Caption="申请类别" FieldName="type" Width="70px" VisibleIndex="5"/>
                           <dx:GridViewDataTextColumn Caption="通知发放的部门(电子档)" FieldName="deliver_dept" Width="150px" VisibleIndex="6"/>
                            <dx:GridViewDataTextColumn Caption="文件类型" FieldName="FileType" Width="150px" VisibleIndex="7"/>
                            <dx:GridViewDataTextColumn Caption="文件编号" FieldName="File_Number" Width="150px" VisibleIndex="8"/>
                              <dx:GridViewDataTextColumn Caption="文件版本号" FieldName="Verno" Width="150px" VisibleIndex="8"/>
                            <dx:GridViewDataTextColumn Caption="文件名称" FieldName="filename" Width="150px" VisibleIndex="9"/>
                            <dx:GridViewDataTextColumn Caption="pdf路径" FieldName="pdfFile" Width="120px" VisibleIndex="10" />
                            <dx:GridViewDataTextColumn Caption="失效日期" FieldName="expiry_date" Width="150px" VisibleIndex="11"/>
                            <dx:GridViewDataTextColumn Caption="文件语言" FieldName="File_Language" Width="120px" VisibleIndex="12" />
                            <dx:GridViewDataTextColumn Caption="文件检索来源" FieldName="File_Source" Width="150px" VisibleIndex="13"/>
                            <dx:GridViewDataTextColumn Caption="法律法规目录" FieldName="Law_List" Width="120px" VisibleIndex="14" />
                            <dx:GridViewDataTextColumn Caption="发布部门" FieldName="Law_Dept" Width="150px" VisibleIndex="15"/>
                            <dx:GridViewDataTextColumn Caption="大分类" FieldName="B_fenlei" Width="120px" VisibleIndex="16" />
                            <dx:GridViewDataTextColumn Caption="中分类" FieldName="M_fenlei" Width="120px" VisibleIndex="17"/>
                            <dx:GridViewDataTextColumn Caption="小分类" FieldName="S_fenlei" Width="120px" VisibleIndex="18" />
                            <dx:GridViewDataTextColumn Caption="发布日期" FieldName="deliver_date" Width="80px" VisibleIndex="19" />
                            <dx:GridViewDataTextColumn Caption="实施日期" FieldName="material_date" Width="80px" VisibleIndex="20"/>
                            <dx:GridViewDataTextColumn Caption="收集日期" FieldName="collect_date" Width="8px" VisibleIndex="21" />
                       


                           <dx:GridViewDataTextColumn Caption="GroupID" FieldName="GroupID" VisibleIndex="97" Width="0px"
                           HeaderStyle-CssClass="hidden" CellStyle-CssClass="hidden" FooterCellStyle-CssClass="hidden"></dx:GridViewDataTextColumn>
                          
                         <dx:GridViewDataTextColumn FieldName="File_Serialno" Width="0px">
                                            <HeaderStyle CssClass="hidden" />
                                            <CellStyle CssClass="hidden"></CellStyle>
                                            <FooterCellStyle CssClass="hidden"></FooterCellStyle>
                                        </dx:GridViewDataTextColumn>

                     
                        </Columns>
           
                        <Styles>
                            <Header BackColor="#99CCFF"></Header>
                            <FocusedRow BackColor="#99CCFF" ForeColor="#0000CC"></FocusedRow>
                            <Footer HorizontalAlign="Right" ForeColor="Red"></Footer>
                        </Styles>
                    </dx:ASPxGridView>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>

