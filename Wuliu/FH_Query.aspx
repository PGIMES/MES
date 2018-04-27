<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="FH_Query.aspx.cs" Inherits="Wuliu_FH_Query" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
<script src="../Content/js/jquery.min.js" type="text/javascript"></script>
    <script src="../Content/js/plugins/layer/layer.min.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            $("#mestitle").text("【发货记录查询】");


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
                                    公司别
                                </td>
                                <td>
                                    <asp:DropDownList ID="dropcomp" runat="server" class="form-control input-s-sm ">
                                        <asp:ListItem>100</asp:ListItem>
                                        <asp:ListItem>200</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    发运单号:
                                </td>
                                <td >
                                    <asp:TextBox ID="txt_id" class="form-control input-s-sm" runat="server"></asp:TextBox>
                                </td>
                                <td>
                                    ship_from
                                </td>
                                <td>
                                     <asp:TextBox ID="txt_shipfrom" class="form-control input-s-sm" runat="server"></asp:TextBox>
                                </td>
                               <td style=" width:30px"></td>
                               
                                
                                <td align="right">
                                    <asp:Button ID="Button1" runat="server" Text="查询" class="btn btn-large btn-primary"
                                        OnClick="Button1_Click" Width="80px" />
                                   
                                   
                                </td>
                                <td style=" width:20px">  </td>
                                <td align="right">
                                    <asp:Button ID="Button2" runat="server" Text="导出" class="btn btn-large btn-primary"
                                        Width="80px" onclick="Button2_Click" />
                                   
                                   
                                </td>
                            </tr>
                            <tr>
                                 <td>产品号</td>
                                <td> <asp:TextBox ID="txt_xmh" class="form-control input-s-sm" runat="server"></asp:TextBox></td>
                                  
                                <td>发运日期</td>
                                <td>
                                    <asp:TextBox ID="txt_startdate" runat="server" Width="100" />
                                    <ajaxToolkit:CalendarExtender ID="txt_startdate_CalendarExtender" runat="server"
                                        PopupButtonID="Image2" Format="yyyy/MM/dd" TargetControlID="txt_startdate" />
                                    ~&nbsp;<asp:TextBox ID="txt_enddate" runat="server" Width="100" />
                                    <ajaxToolkit:CalendarExtender ID="txt_enddate_CalendarExtender" runat="server" PopupButtonID="Image2"
                                        Format="yyyy/MM/dd" TargetControlID="txt_enddate" />
                                </td>
                                 <td>
                                    ship_to
                                </td>
                                <td>
                                    <asp:TextBox ID="txt_shipto" class="form-control input-s-sm" runat="server"></asp:TextBox>
                                </td>
                               
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
                            PageSize="100" Width="1300px"   BorderColor="LightBlue">
                             <EmptyDataTemplate>
            查无数据，请选择条件重新查询.</EmptyDataTemplate>
                            <Columns>
                                <asp:BoundField DataField="abs_id" HeaderText="货运单号">
                                    <HeaderStyle BackColor="#C1E2EB" />
                                </asp:BoundField>
                                <asp:BoundField DataField="abs_inv_nbr" HeaderText="发票号">
                                <HeaderStyle BackColor="#C1E2EB" />
                                </asp:BoundField>
                                <asp:BoundField DataField="abs_item" HeaderText="集装箱项目号">
                                <HeaderStyle BackColor="#C1E2EB" />
                                </asp:BoundField>
                                <asp:BoundField DataField="pt_desc1" HeaderText="描述">
                                <HeaderStyle BackColor="#C1E2EB" />
                                </asp:BoundField>
                                <asp:BoundField DataField="abs_shp_date" HeaderText="发运日期"  
                                    DataFormatString="{0:yyyy-MM-dd}">
                                    <HeaderStyle BackColor="#C1E2EB" />
                                </asp:BoundField>

                                <asp:BoundField DataField="abs_qty" HeaderText="发货数量">
                                <HeaderStyle BackColor="#C1E2EB" />
                                <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="abs_shipfrom" 
                                    HeaderText="ship_from">
                                <HeaderStyle BackColor="#C1E2EB" />
                                </asp:BoundField>
                                <asp:BoundField DataField="abs_shipto" HeaderText="ship_to">
                                <HeaderStyle BackColor="#C1E2EB" />
                                </asp:BoundField>
                                <asp:BoundField DataField="abs_nwt" HeaderText="净重">
                                <HeaderStyle BackColor="#C1E2EB" />
                                <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="abs_gwt" HeaderText="毛重">
                                <HeaderStyle BackColor="#C1E2EB" />
                                <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="abs_wt_um" HeaderText="计量单位">
                                <HeaderStyle BackColor="#C1E2EB" />
                                </asp:BoundField>
                                <asp:BoundField DataField="abs_order" HeaderText="订单">
                                <HeaderStyle BackColor="#C1E2EB" />
                                </asp:BoundField>
                                <asp:BoundField DataField="abs_line" HeaderText="订单行">
                                <HeaderStyle BackColor="#C1E2EB" />
                                <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>

                            </Columns>
                            <PagerSettings FirstPageText="首页" LastPageText="尾页" Mode="NextPreviousFirstLast" 
                                NextPageText="下页" PreviousPageText="上页"  />
                            <PagerStyle ForeColor="Red" />
                        </asp:GridView>
            
    </div>
</asp:Content>
