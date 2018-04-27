<%@ Page Title="报价系统【已有报价更新报价】" Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.master" CodeFile="Baojia_history_UpdateBaojia.aspx.cs" Inherits="BaoJia_Baojia_history_UpdateBaojia" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <style type="text/css">
        td {
            padding-left: 5px;
            padding-right: 5px;
        }
        .auto-style1 {
            width: 100px;
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
        $("#mestitle").text("【已有报价更新报价】");
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
    <div class="row row-container">
        <div class="col-sm-12 ">
            <div class="panel panel-info">
                <div class="panel-heading">
                    <strong>已有报价更新报价</strong>
                </div>
                <div class="panel-body">
                    <div class="col-sm-12">
                        <table class="tblCondition" >
                            <tr>
                                <td>报价号:
                                </td>
                                <td>
                                    <asp:TextBox ID="txtBaojia_no" class="form-control input-s-sm" runat="server"  Wrap="false"  Width="200px"></asp:TextBox>
                                </td>
                                <td>顾客:
                                </td>
                                <td>
                                    <asp:TextBox ID="txtCustomer_name" class="form-control input-s-sm" runat="server" Width="200px"></asp:TextBox>
                                </td>
                                 <td>报价人:
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddl_update_user" runat="server" class="form-control input-s-sm " Width="130px">
                                    </asp:DropDownList>
                                </td>
                                 <td colspan="12" style="text-align:right;padding-top:10px;padding-right:30px" >
                                    <asp:Button ID="Button1" runat="server" Text="查询" class="btn btn-large btn-primary" OnClick="Button1_Click" Width="100px" /></td>
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
                <td valign="top">
                    <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" BorderColor="LightGray" OnRowDataBound="GridView1_RowDataBound" OnPageIndexChanging="GridView1_PageIndexChanging">
                        <Columns> 
                            <asp:BoundField DataField="baojia_no" HeaderText="No.">
                                <HeaderStyle BackColor="#C1E2EB"  />
                            </asp:BoundField>                     
   
                           <asp:BoundField DataField="baojia_no" HeaderText="报价号">
                                <HeaderStyle BackColor="#C1E2EB"  Wrap="false" />
                            </asp:BoundField>
                              <asp:TemplateField HeaderText="路径">                                
                                <ItemTemplate>  
                                                            
                                             <a class="fa fa-folder-open" href='<%# Eval("baojia_file_path")%>' target="_blank"></a>                            
                                </ItemTemplate>
                                <HeaderStyle BackColor="#C1E2EB" Width="40px" Wrap="False" />
                            </asp:TemplateField>                 
                            <asp:BoundField DataField="turns" HeaderText="轮数">
                                <HeaderStyle BackColor="#C1E2EB"  Wrap="false" />
                            </asp:BoundField>
                            <asp:BoundField DataField="customer_name" HeaderText="顾客">
                                <HeaderStyle BackColor="#C1E2EB"  Wrap="false" />
                            </asp:BoundField>
                            <asp:BoundField DataField="customer_project" HeaderText="项目">
                                <HeaderStyle BackColor="#C1E2EB" Wrap="false" />
                                <ItemStyle />
                            </asp:BoundField>
                               <asp:BoundField DataField="ljh" HeaderText="零件号">
                                <HeaderStyle BackColor="#C1E2EB" Width="150px" Wrap="false" />
                                <ItemStyle />
                            </asp:BoundField>
                            <asp:BoundField DataField="lj_name" HeaderText="零件名称">
                                <HeaderStyle BackColor="#C1E2EB" Wrap="false" />
                                <ItemStyle />
                            </asp:BoundField>
                               <asp:BoundField DataField="quantity_year" HeaderText="年用量" DataFormatString="{0:N0}" ItemStyle-HorizontalAlign="Right">
                                <HeaderStyle BackColor="#C1E2EB" Wrap="false" />
                            </asp:BoundField>
                            <asp:BoundField DataField="baojia_end_date"  HeaderText="报出时间">
                                <HeaderStyle BackColor="#C1E2EB" />
                            </asp:BoundField>                   
                            <asp:BoundField DataField="lastname" HeaderText="报价人">
                                <HeaderStyle BackColor="#C1E2EB"  Wrap="false" />
                                <ItemStyle />
                            </asp:BoundField>
                             <asp:BoundField DataField="requestid" HeaderText="requestid">
                                <HeaderStyle BackColor="#C1E2EB"  Wrap="false" />
                            </asp:BoundField>  
                        </Columns>
                        <EmptyDataTemplate>no data found</EmptyDataTemplate>
                        <PagerSettings FirstPageText="首页" LastPageText="尾页" Mode="NextPreviousFirstLast" NextPageText="下页" PreviousPageText="上页" />
                        <PagerStyle ForeColor="Red" />
                    </asp:GridView>
                </td>
            </tr>


        </table>
    </div>
</asp:Content>
