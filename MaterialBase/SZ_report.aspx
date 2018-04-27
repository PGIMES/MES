<%@ Page Title="物料系统【丝锥查询】" Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.master" CodeFile="SZ_report.aspx.cs" Inherits="SZ_report" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <style type="text/css">
        td {
            padding-left: 5px;
            padding-right: 5px;
        }
        .tblCondition td{ white-space:nowrap }
        .auto-style1 {
            width: 14px;
        }
        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <script src="../Content/js/jquery.min.js" type="text/javascript"></script>
    <script src="../Content/js/bootstrap.min.js"></script>
    <script src="../Content/js/plugins/layer/layer.min.js" type="text/javascript"></script>
    <script src="../Content/js/plugins/layer/laydate/laydate.js"></script>
    <script type="text/javascript">
        $("#mestitle").text("【丝锥查询】");
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
                    《丝锥》查询</div>
                <div class="panel-body">
                    <div class="col-sm-12">
                        <table class="tblCondition" >
                            <tr>
                                <td>物料号:
                                </td>
                                <td>
                                                        <input id="txtwlh" class="form-control input-s-sm" style="height: 35px; width: 110px" runat="server"  /></td>
                                <td>描述:
                                </td>
                                <td>
                                                        <input id="txtms" class="form-control input-s-sm" style="height: 35px; width: 110px" runat="server"  /></td>
                                 <td>&nbsp;涂层</td>
                                <td >
                                                    <input id="txttc" class="form-control input-s-sm" style="height: 35px; width: 110px" runat="server" /></td>
                                <td >
                                                    品牌：</td>
                                <td>
                                                    <input id="txtpp" class="form-control input-s-sm" style="height: 35px; width: 110px" runat="server" /></td>
                                 <td >
                                     品牌编号描述：</td>
                                 <td >
                                                    <input id="txtppms" class="form-control input-s-sm" style="height: 35px; width: 110px" runat="server" /></td>
                                 <td >
                                     供应商：</td>
                                 <td >
                                                    <input id="txtgys" class="form-control input-s-sm" style="height: 35px; width: 110px" runat="server" /></td>
                         
                                 <td >
                                     &nbsp;</td>
                            </tr>
                          
                            <tr>
                                <td>被加工材质：</td>
                                <td>
                                                <asp:DropDownList ID="DDL_jgcz" class="form-control input-s-sm" Style="height: 35px; width: 110px" runat="server">
                                                </asp:DropDownList>
                                </td>
                                <td>丝锥类型：</td>
                                <td>
                                                <asp:DropDownList ID="DDL_szlx" class="form-control input-s-sm" Style="height: 35px; width: 110px" runat="server">
                                                </asp:DropDownList>
                                </td>
                                 <td>旋向：</td>
                                <td >
                                                <asp:DropDownList ID="DDL_xx" class="form-control input-s-sm" Style="height: 35px; width: 110px" runat="server">
                                                </asp:DropDownList>
                                </td>
                                <td >
                                                    槽型：</td>
                                <td>
                                                <asp:DropDownList ID="DDL_cx" class="form-control input-s-sm" Style="height: 35px; width: 110px" runat="server">
                                                </asp:DropDownList>
                                </td>
                                 <td >
                                     丝锥材质:</td>
                                 <td >
                                                <asp:DropDownList ID="DDL_sccz" class="form-control input-s-sm" Style="height: 35px; width: 110px" runat="server">
                                                </asp:DropDownList>
                                </td>
                                 <td >
                                     是否内冷：</td>
                                 <td >
                                                <asp:DropDownList ID="DDL_isnl" class="form-control input-s-sm" Style="height: 35px; width: 110px" runat="server">
                                                </asp:DropDownList>
                                </td>
                        
                                 <td >
                                    <asp:Button ID="Bt_select" runat="server" Text="查询" class="btn btn-large btn-primary" OnClick="Button1_Click" Width="100px" /></td>
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
                    <asp:GridView ID="gv_pt" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" BorderColor="LightGray" OnRowDataBound="gv_pt_RowDataBound" OnPageIndexChanging="gv_pt_PageIndexChanging">
                        <Columns> 
                              <asp:BoundField DataField="物料号" HeaderText="No.">
                                <HeaderStyle BackColor="#C1E2EB"  />
                            </asp:BoundField>  
                                 <asp:BoundField DataField="物料号" HeaderText="物料号">
                                <HeaderStyle BackColor="#C1E2EB" Width="50px" Wrap="false" />
                                <ItemStyle />
                            </asp:BoundField>    
                             <asp:BoundField DataField="描述2" HeaderText="描述2">
                                <HeaderStyle BackColor="#C1E2EB" Width="150px" Wrap="false" />
                                <ItemStyle />
                            </asp:BoundField>  
                            <asp:BoundField DataField="螺纹尺寸" HeaderText="螺纹尺寸">
                                <HeaderStyle BackColor="#C1E2EB" Width="90px"  Wrap="false" />
                            </asp:BoundField>
                           <asp:BoundField DataField="全长" HeaderText="全长">
                                <HeaderStyle BackColor="#C1E2EB"  Width="60px" Wrap="false" />
                            </asp:BoundField>
                            <asp:BoundField DataField="精度" HeaderText="精度">
                                <HeaderStyle BackColor="#C1E2EB"   Width="60px" Wrap="false" />
                            </asp:BoundField>
                            <asp:BoundField DataField="切削锥长" HeaderText="切削锥长">
                                <HeaderStyle BackColor="#C1E2EB" Width="80px" Wrap="false" />
                                <ItemStyle />
                            </asp:BoundField>
                               <asp:BoundField DataField="被加工材质" HeaderText="被加工材质">
                                <HeaderStyle BackColor="#C1E2EB" Width="80px" Wrap="false" />
                                <ItemStyle />
                            </asp:BoundField>      
                             <asp:BoundField DataField="丝锥类型" HeaderText="丝锥类型">
                                <HeaderStyle BackColor="#C1E2EB"  Width="100px" Wrap="false" />
                            </asp:BoundField>    
                                  <asp:BoundField DataField="旋向" HeaderText="旋向">
                                <HeaderStyle BackColor="#C1E2EB" Width="60px" Wrap="false" />
                                <ItemStyle />
                            </asp:BoundField>   
                                  <asp:BoundField DataField="槽型" HeaderText="槽型">
                                <HeaderStyle BackColor="#C1E2EB" Width="100px" Wrap="false" />
                                <ItemStyle />
                            </asp:BoundField>      
                                  <asp:BoundField DataField="是否内冷" HeaderText="是否内冷">
                                <HeaderStyle BackColor="#C1E2EB" Width="60px" Wrap="false" />
                                <ItemStyle />
                            </asp:BoundField>   
                                  <asp:BoundField DataField="丝锥材质" HeaderText="丝锥材质">
                                <HeaderStyle BackColor="#C1E2EB" Width="100px" Wrap="false" />
                                <ItemStyle />
                            </asp:BoundField> 
                                  <asp:BoundField DataField="涂层" HeaderText="涂层">
                                <HeaderStyle BackColor="#C1E2EB" Width="60px" Wrap="false" />
                                <ItemStyle />
                            </asp:BoundField>      
                                  <asp:BoundField DataField="品牌" HeaderText="品牌">
                                <HeaderStyle BackColor="#C1E2EB" Width="60px" Wrap="false" />
                                <ItemStyle />
                            </asp:BoundField>       
                            <asp:BoundField DataField="品牌编号描述" HeaderText="品牌编号描述">
                                <HeaderStyle BackColor="#C1E2EB" Width="100px" Wrap="false" />
                                <ItemStyle />
                            </asp:BoundField> 
                         
                             <asp:BoundField DataField="供应商" HeaderText="供应商">
                                <HeaderStyle BackColor="#C1E2EB" Width="100px" Wrap="false" />
                                <ItemStyle />
                            </asp:BoundField>    
                             <asp:BoundField DataField="价格" HeaderText="价格" DataFormatString="{0:F}">
                                <HeaderStyle BackColor="#C1E2EB" Width="50px" Wrap="false" />
                                <ItemStyle />
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
