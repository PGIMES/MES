<%@ Page Title="客户系统【已有客户更新】" Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.master" CodeFile="Customer_Update_list.aspx.cs" Inherits="Customer_Update_list" %>

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
        $("#mestitle").text("【已有客户更新】");
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
                    <strong>已有客户更新</strong>
                </div>
                <div class="panel-body">
                    <div class="col-sm-12">
                        <table class="tblCondition" >
                            <tr>
                                <td>客户大类:
                                </td>
                                <td>
                                                <asp:DropDownList ID="DDL_cmClassName" class="form-control input-s-sm" Style="height: 35px; width: 150px" runat="server">
                                                </asp:DropDownList>
                                </td>
                                <td>客户代码:
                                </td>
                                <td>
                                                        <input id="txtBusinessRelationCode" class="form-control input-s-sm" style="height: 35px; width: 150px" runat="server"  /></td>
                                 <td>客户名称:
                                </td>
                                <td >
                                                    <input id="txtBusinessRelationName1" class="form-control input-s-sm" style="height: 35px; width: 150px" runat="server" /></td>
                                <td >
                                                    销售负责人</td>
                                <td>
                                                <asp:DropDownList ID="DDL_UserName" class="form-control input-s-sm" Style="height: 35px; width: 150px" runat="server">
                                                </asp:DropDownList>
                                </td>
                                 <td >
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
                              <asp:BoundField DataField="BusinessRelationCode" HeaderText="No.">
                                <HeaderStyle BackColor="#C1E2EB"  />
                            </asp:BoundField>  
                           
                            <asp:BoundField DataField="BusinessRelationCode" HeaderText="客户代码">
                                <HeaderStyle BackColor="#C1E2EB"  Wrap="false" />
                            </asp:BoundField>
                           <asp:BoundField DataField="cmClassName" HeaderText="客户大类">
                                <HeaderStyle BackColor="#C1E2EB"  Wrap="false" />
                            </asp:BoundField>
       
                            <asp:BoundField DataField="BusinessRelationName1" HeaderText="客户名称">
                                <HeaderStyle BackColor="#C1E2EB"  Wrap="false" />
                            </asp:BoundField>
                            <asp:BoundField DataField="AddressSearchName" HeaderText="搜索名称">
                                <HeaderStyle BackColor="#C1E2EB" Wrap="false" />
                                <ItemStyle />
                            </asp:BoundField>
                               <asp:BoundField DataField="UserName" HeaderText="销售负责人">
                                <HeaderStyle BackColor="#C1E2EB" Width="150px" Wrap="false" />
                                <ItemStyle />
                            </asp:BoundField>      
                             <asp:BoundField DataField="requestid" HeaderText="requestid">
                                <HeaderStyle BackColor="#C1E2EB"  />
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
