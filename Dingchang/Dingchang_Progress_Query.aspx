<%@ Page Title="【订舱状态追踪查询】" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Dingchang_Progress_Query.aspx.cs" Inherits="dingchang_Dingchang_Progress_Query" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <style type="text/css">
        td
        {
            padding-left: 5px;
            padding-right: 5px;
            padding-top: 5px;
        }
    </style>
    <script src="../Content/js/jquery.min.js" type="text/javascript"></script>
    <script src="../Content/js/plugins/layer/layer.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#mestitle").text("【订舱状态追踪查询】");
            $(function () {
                $(".datepicker").datepicker({
                    autoclose: true,
                    todayBtn: true,
                    todayHighlight: true,
                    initialDate: new Date()
                });
            });
        })//endready

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="panel panel-info" style="padding-left: 5px; padding-right: 5px">
        <table>
            <tr>
                <td>
                    年份
                </td>
                <td>
                    <asp:DropDownList ID="dropYear" runat="server" class="form-control input-s-sm">
                    </asp:DropDownList>
                </td>
                <td>发货单号</td>
                <td>
                    <asp:TextBox ID="txt_hyno" runat="server" class="form-control input-s-sm " />
                </td>
                <td>
                    当前节点
                </td>
                <td>
                    <asp:DropDownList ID="dropstatus" runat="server" 
                        class="form-control input-s-sm">
                        <asp:ListItem Value="">ALL</asp:ListItem>
                        <asp:ListItem Value="已申请">已申请</asp:ListItem>
                        <asp:ListItem Value="已订舱">已订舱</asp:ListItem>
                       
                    </asp:DropDownList>
                </td>
                <td>
                    要求发运日期
                </td>
                  <td ><asp:TextBox ID="txtfyrq" runat="server" class="form-control input-s-sm datepicker" style="display:inline" ></asp:TextBox><ajaxtoolkit:calendarextender ID="txtfyrq_CalendarExtender" 
                 runat="server" PopupButtonID="Image2"  Format="yyyy/MM/dd"
                 TargetControlID="txtfyrq" />
                    </td>
               
                
                <td rowspan="3">
                    <asp:Button ID="btnQuery" runat="server" Text="查询" class="btn btn-primary btn-lg"
                        OnClick="btnQuery_Click" Height="50px" Width="100px" />
                    <a href="../Index.aspx" class="btn btn-primary btn-lg" 
                        style="color: white; height: 50px; width: 100px; vertical-align:middle">返回</a>
                </td>
            </tr>
            <tr>
                <td>
                    状态</td>
                <td>
                    <asp:DropDownList ID="Droptype" runat="server" class="form-control input-s-sm">
                        <asp:ListItem Value="未完成">未完成</asp:ListItem>
                         <asp:ListItem Value="">ALL</asp:ListItem>
                        <asp:ListItem Value="已完成">已完成</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    待办人</td>
                <td>
                    <asp:DropDownList ID="dropUser" runat="server" 
                        class="form-control input-s-sm">
                    </asp:DropDownList>
                </td>
                <td>
                    办理时间</td>
                <td>
                    <asp:TextBox ID="txtfinish" runat="server" 
                        class="form-control input-s-sm datepicker" 
                        style="display:inline" Width="120px"></asp:TextBox>
                    <ajaxtoolkit:calendarextender ID="txtfinish_calendarextender" 
                 runat="server" PopupButtonID="Image2"  Format="yyyy/MM/dd"
                 TargetControlID="txtfinish" />
                    </td>
               
               
               
                <td>
                    预计外部提货日期</td>
                <td>
                    <asp:TextBox ID="txtthrq" runat="server" 
                        class="form-control input-s-sm datepicker" 
                        style="display:inline" ></asp:TextBox>
                    <ajaxtoolkit:calendarextender ID="txtthrq_calendarextender" 
                 runat="server" PopupButtonID="Image2"  Format="yyyy/MM/dd"
                 TargetControlID="txtthrq" />
                </td>
               
            </tr>
            <tr>
                <td>
                    样件订单号</td>
                <td >
                                      <asp:TextBox ID="txt_yjno" runat="server" 
                        class="form-control input-s-sm " />
                
                </td>
                <td>
                    QAD单号</td>
                <td>
                    <asp:TextBox ID="txt_qadno" runat="server" 
                        class="form-control input-s-sm " />
                </td>
                <td>
                    &nbsp;</td>
               
                <td style=" display:none">
                    &nbsp;</td>
                <td style=" display:none">
                    &nbsp;</td>
               
               
               
            </tr>
        </table>
    </div>
    <hr />
       <div  runat="server" id="DIV1" style=" margin:20px"  >
        
                       
                                <asp:Panel ID="Panel2" runat="server" Height="100%" >
                                    <table style=" background-color: #FFFFFF;" 
                                      >
                                      
                                        <tr>
                                            <td valign="top">
    <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AllowSorting="True"
        AutoGenerateColumns="False" 
        OnPageIndexChanging="GridView1_PageIndexChanging" PageSize="200"
        Width="1200px" BorderColor="#CCCCCC" onrowdatabound="GridView1_RowDataBound">
        <EmptyDataTemplate>
            查无数据，请选择条件重新查询.</EmptyDataTemplate>
        <Columns>
            <asp:BoundField DataField="requestid">
            <ControlStyle CssClass="hidden" Width="0px" />
            <FooterStyle CssClass="hidden" Width="0px" />
            <HeaderStyle CssClass="hidden" Width="0px" />
            <ItemStyle CssClass="hidden" Width="0px" />
            </asp:BoundField>
         <asp:TemplateField HeaderText="发货单号" >                   
                    <ItemTemplate>
                        <asp:HyperLink ID="HyperLink1" runat="server" Text='<%#Eval("hyno_wuliu") %>' Width="130"></asp:HyperLink>
                    </ItemTemplate>
                                                                                        
                                                            <HeaderStyle BackColor="#C1E2EB" />
                                                                                        
                       <ItemStyle Width="50px" Wrap="False"  ForeColor="Blue" CssClass="size1"/>
                </asp:TemplateField> 
            <asp:BoundField DataField="node" HeaderText="当前节点"  
                ItemStyle-Width="40px">
                <HeaderStyle BackColor="#C1E2EB" Width="100px" />

            <ItemStyle Width="100px" />
            </asp:BoundField>
            <asp:BoundField DataField="fyrq_sale" 
                DataFormatString="{0:yyyy-MM-dd}" HeaderText="要求发运日期" 
                HtmlEncode="False" ItemStyle-Width="40px">
            <HeaderStyle BackColor="#C1E2EB" Width="150px" />
            <ItemStyle Width="150px" />
            </asp:BoundField>
            <asp:BoundField DataField="yjthrq_wuliu" 
                DataFormatString="{0:yyyy-MM-dd}" HeaderText="预计外部提货日期" 
                ItemStyle-Width="60px">
            <HeaderStyle BackColor="#C1E2EB" Width="180px" />
            <ItemStyle Width="150px" />
            </asp:BoundField>
            <asp:BoundField DataField="qrch_ware" 
                DataFormatString="{0:yyyy-MM-dd}" HeaderText="确认出货已完成日期" 
                ItemStyle-Width="40px">
            <ControlStyle CssClass="hidden" Width="0px" />
            <FooterStyle CssClass="hidden" Width="0px" />
            <HeaderStyle BackColor="#C1E2EB" CssClass="hidden" 
                Width="0px" />
            <ItemStyle CssClass="hidden" Width="0px" />
            </asp:BoundField>
            <asp:BoundField DataField="solvetime" HeaderText="办理时间" 
                ItemStyle-Width="100px">
            <HeaderStyle BackColor="#C1E2EB" />
            <ItemStyle Width="100px" />
            </asp:BoundField>
             <asp:BoundField DataField="status" HeaderText="状态" >
                <HeaderStyle BackColor="#C1E2EB" />
            </asp:BoundField>
            <asp:BoundField DataField="solver" HeaderText="待办人" >
                <HeaderStyle BackColor="#C1E2EB" />
            </asp:BoundField>
        </Columns>
        <PagerSettings FirstPageText="首页" LastPageText="尾页" Mode="NextPreviousFirstLast"
            NextPageText="下页" PreviousPageText="上页" />
        <PagerStyle ForeColor="Red" />
    </asp:GridView>
    </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                                </div>
    </asp:Content>


