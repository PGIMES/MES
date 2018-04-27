<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="PW_Add_Query.aspx.cs" Inherits="PW_PW_Add_Query" %>



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
          $("#mestitle").text("【钢丸加料查询】");
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
                <td>
                    月份
                </td>
                <td>
                    <asp:DropDownList ID="dropMonth" runat="server" class="form-control input-s-sm">
                    </asp:DropDownList>
                </td>
                <td>
                    日期
                </td>
                  <td ><asp:TextBox ID="txtDateFrom" runat="server" class="form-control input-s-sm datepicker" style="display:inline" Width="150px"></asp:TextBox><ajaxtoolkit:calendarextender ID="txt_startdate_CalendarExtender" 
                 runat="server" PopupButtonID="Image2"  Format="yyyy/MM/dd"
                 TargetControlID="txtDateFrom" />
                    <asp:TextBox ID="txtDateTo" runat="server" class="form-control input-s-sm datepicker" style="display:inline" Width="150px"></asp:TextBox><ajaxtoolkit:calendarextender ID="Calendarextender1" 
                 runat="server" PopupButtonID="Image2"  Format="yyyy/MM/dd"
                 TargetControlID="txtDateTo"  />
                    </td>
                <td>班别
                </td>
                <td>
                    <asp:DropDownList ID="dropBanBie" runat="server" class="form-control input-s-sm">
                        <asp:ListItem Value=""></asp:ListItem>
                        <asp:ListItem Value="白班"></asp:ListItem>
                        <asp:ListItem Value="晚班"></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td rowspan="2">
                    <asp:Button ID="btnQuery" runat="server" Text="查询" class="btn btn-primary btn-lg"
                        OnClick="btnQuery_Click" Height="50px" Width="100px" />
                    <a href="../Default.aspx" class="btn btn-primary btn-lg" 
                        style="color: white; height: 50px; width: 100px; vertical-align:middle">返回</a>
                </td>
            </tr>
            <tr>
                <td>
                    钢丸种类
                </td>
                <td>
                    <asp:DropDownList ID="Droptype" runat="server" class="form-control input-s-sm">
                        <asp:ListItem></asp:ListItem>
                        <asp:ListItem>0.4mm</asp:ListItem>
                        <asp:ListItem>0.6mm</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    操作工</td>
                <td>
                    <asp:TextBox ID="txtEmpName" runat="server" class="form-control input-s-sm" placeholder="工号或姓名"></asp:TextBox>
                </td>
                <td>
                    设备</td>
                <td>
                    <asp:DropDownList ID="dropShebei" runat="server" 
                        class="form-control input-s-sm">
                    </asp:DropDownList></td>
               
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
        Width="800" BorderColor="#CCCCCC">
        <EmptyDataTemplate>
            查无数据，请选择条件重新查询.</EmptyDataTemplate>
        <Columns>
            <asp:BoundField DataField="add_date" HeaderText="日期" 
                DataFormatString="{0:yyyy-MM-dd}" >
<HeaderStyle BackColor="#C1E2EB" />
<ItemStyle Width="100px"> </ItemStyle>
            </asp:BoundField>
            <asp:BoundField DataField="emp_banbie" HeaderText="班别"  
                ItemStyle-Width="40px">
                <HeaderStyle BackColor="#C1E2EB" />

<ItemStyle Width="40px"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField DataField="emp_no" HeaderText="工号" 
                HtmlEncode="False"  ItemStyle-Width="40px">
                <HeaderStyle BackColor="#C1E2EB" />

<ItemStyle Width="40px"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField DataField="emp_name" HeaderText="姓名"  
                ItemStyle-Width="60px">
                <HeaderStyle BackColor="#C1E2EB" />

<ItemStyle Width="60px"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField DataField="emp_banzhu" HeaderText="班组"  
                ItemStyle-Width="40px">
                <HeaderStyle BackColor="#C1E2EB" />

<ItemStyle Width="40px"></ItemStyle>
            </asp:BoundField>
             <asp:BoundField DataField="equip_name" HeaderText="设备" >
                <HeaderStyle BackColor="#C1E2EB" />
            </asp:BoundField>
            <asp:BoundField DataField="wl" HeaderText="钢丸种类" >
                <HeaderStyle BackColor="#C1E2EB" />
            </asp:BoundField>
            <asp:BoundField DataField="zl" HeaderText="重量"
                ItemStyle-Width="100px">
                <HeaderStyle BackColor="#C1E2EB" />

<ItemStyle Width="100px"></ItemStyle>
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


