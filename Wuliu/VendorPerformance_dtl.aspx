<%@ Page Title="供应商超欠交明细查询" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="VendorPerformance_dtl.aspx.cs" Inherits="VendorPerformance_dtl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <style>th ,td{padding-left:3px;padding-right:3px}
    </style>
<script src="../Content/js/jquery.min.js" type="text/javascript"></script>
    <script src="../Content/js/plugins/layer/layer.min.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            $("#mestitle").text("【<%=Title%>】");
        });  
    </script>
    <div class="row row-container">
        <div class="col-sm-12 ">
            <div class="panel panel-info">
                <div class="panel-heading">
                    <strong>查询</strong>
                    <asp:ScriptManager ID="ScriptManager1" runat="server">
                    </asp:ScriptManager>
                </div>
                <div class="panel-body">
                    <div class="col-sm-12">
                        <table>
                            <tr>
                                <td>
                                    公司别:
                                </td>
                                <td>
                                    <asp:DropDownList ID="dropcomp" runat="server" class="form-control input-s-sm ">
                                        <asp:ListItem Value="200">200</asp:ListItem>
                                        <asp:ListItem Value="100">100</asp:ListItem>

                                    </asp:DropDownList>
                                </td>
                                <td>
                                    欠/超交:
                                </td>
                                <td>
                                    <asp:DropDownList ID="dropPtype" runat="server" class="form-control input-s-sm ">
                                        <asp:ListItem Value="欠交">欠交</asp:ListItem>
                                        <asp:ListItem Value="超交">超交</asp:ListItem>
                                        <asp:ListItem Value="">所有</asp:ListItem>
                                    </asp:DropDownList>
                                </td>                                
                                <td>
                                    物料号：
                                </td>
                                <td>
                                     <asp:TextBox ID="txtPart" class="form-control input-s-sm" runat="server"></asp:TextBox>
                                </td>
                               <td >采购订单号:</td>
                               <td >
                                    <asp:TextBox ID="txtNbr" class="form-control input-s-sm" runat="server"></asp:TextBox>
                                </td>
                                
                                <td align="right">
                                    <asp:Button ID="Button1" runat="server" Text="查询" class="btn btn-large btn-primary"
                                        OnClick="Button1_Click" Width="80px" />                             
                                   
                                </td>
                                <td style=" width:20px">  </td>
                                 
                            </tr>
                            
                        </table>
                    </div>
                </div>
            </div>
        </div>
    </div>
   
    <div  id="DIV1" style="margin-left: 4px">
       
                        <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AllowSorting="True"
                            AutoGenerateColumns="False" OnPageIndexChanging="GridView1_PageIndexChanging"
                            PageSize="100" Width="1300px"   BorderColor="LightBlue" OnRowDataBound="GridView1_RowDataBound">
                             <EmptyDataTemplate>  查无数据，请选择条件重新查询. </EmptyDataTemplate>
                            <Columns>
                                <asp:BoundField DataField="" HeaderText="No">
                                    <HeaderStyle BackColor="#C1E2EB" />
                                </asp:BoundField>
                                <asp:BoundField DataField="域" HeaderText="域">
                                    <HeaderStyle BackColor="#C1E2EB" />
                                </asp:BoundField>
                                <asp:BoundField DataField="采购订单号" HeaderText="采购订单号">
                                <HeaderStyle BackColor="#C1E2EB" />
                                </asp:BoundField>
                                <asp:BoundField DataField="订单项" HeaderText="订单项" ItemStyle-HorizontalAlign="Right">
                                <HeaderStyle BackColor="#C1E2EB" />

                                <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="物料号" HeaderText="物料号">
                                <HeaderStyle BackColor="#C1E2EB" />
                                </asp:BoundField>
                                <asp:BoundField DataField="描述" HeaderText="描述"  >
                                    <HeaderStyle BackColor="#C1E2EB" />
                                </asp:BoundField>
                                <asp:BoundField DataField="状态" HeaderText="状态"  >
                                    <HeaderStyle BackColor="#C1E2EB" />
                                </asp:BoundField>
                                <asp:BoundField DataField="版本ID" HeaderText="版本ID">
                                <HeaderStyle BackColor="#C1E2EB" />                                
                                </asp:BoundField>
                                <asp:BoundField DataField="先前累计需求量"    HeaderText="先前累计需求量" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:N1}">
                                <HeaderStyle BackColor="#C1E2EB" />

                                <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="今天之前的计划累计" HeaderText="今天之前计划累计"   ItemStyle-HorizontalAlign="Right" DataFormatString="{0:N1}" >
                                <HeaderStyle BackColor="#C1E2EB" Width="100px" />

                                <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="累计收货" HeaderText="累计收货" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:N1}">
                                <HeaderStyle BackColor="#C1E2EB" />
                                <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="结果QAD" HeaderText="结果(QAD)" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:N1}">
                                <HeaderStyle BackColor="#C1E2EB" />
                                <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>

                                <asp:BoundField DataField="累计收货6004" HeaderText="累计收货(不含6004)" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:N1}" Visible="false">
                                <HeaderStyle BackColor="#C1E2EB" />
                                <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="累计退货6004" HeaderText="累计退货(6004)" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:N1}" Visible="false">
                                <HeaderStyle BackColor="#C1E2EB" />
                                <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="结果" HeaderText="结果" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:N1}" Visible="false">
                                <HeaderStyle BackColor="#C1E2EB" />
                                <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>

                                <asp:BoundField DataField="责任人" HeaderText="责任人">
                                <HeaderStyle BackColor="#C1E2EB" />
                                </asp:BoundField>
                                <asp:BoundField DataField="供应商" HeaderText="供应商">
                                <HeaderStyle BackColor="#C1E2EB" />
                                </asp:BoundField>
                               
                            </Columns>
                            <PagerSettings FirstPageText="首页" LastPageText="尾页" Mode="NextPreviousFirstLast" 
                                NextPageText="下页" PreviousPageText="上页"  />
                            <PagerStyle ForeColor="Red" />
                        </asp:GridView>
            
    </div>
</asp:Content>
