<%@ Page Title="报价系统【报价任务查询】" Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.master" CodeFile="Baojia_Task_Query.aspx.cs" Inherits="BaoJia_Baojia_Task_Query" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <style type="text/css">
        td {
            padding-left: 5px;
            padding-right: 5px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <script src="../Content/js/jquery.min.js" type="text/javascript"></script>
    <script src="../Content/js/plugins/layer/layer.min.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            $("#tst").click(function () {
            });
        });
        $("#mestitle").text("【报价任务查询】");
    </script>
    <script src="../Content/js/plugins/layer/layer.min.js"></script>
    <script src="../Content/js/plugins/layer/laydate/laydate.js"></script>
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
                                <td>年度:
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlyear" runat="server" class="form-control input-s-sm " Width="130px">
                                        <asp:ListItem Value="2017">2017</asp:ListItem>
                                    </asp:DropDownList>
                                <td>报价项目开始时间:
                                </td>
                                 <td style="width:220px;float:left;white-space:nowrap">
                                   <div style=" float:left;white-space:nowrap"> <asp:TextBox ID="txt_startdate"  class="form-control "  onclick="laydate()" runat="server" Width="100px"></asp:TextBox></div>
                                    <div style=" float:left;white-space:nowrap">~</div>
                                   <div style=" float:left;white-space:nowrap"><asp:TextBox ID="txt_enddate"  class="form-control"  onclick="laydate()"  runat="server" Width="100px"></asp:TextBox></div>
                                </td>
                                <td>报价状态:
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlBaojiaStatus" runat="server" class="form-control input-s-sm " Width="130px">
                                        <asp:ListItem Value="">All</asp:ListItem>
                                        <asp:ListItem Value="报价中">报价中</asp:ListItem>
                                        <asp:ListItem Value="已报出">已报出</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td>完成状态:
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlsign_status" runat="server" class="form-control input-s-sm " Width="130px">
                                        <asp:ListItem Value="0">未完成</asp:ListItem>
                                        <asp:ListItem Value="1">已完成</asp:ListItem>
                                        <asp:ListItem Value="-1">All</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td>任务要求完成时间:
                                </td>
                                <td class="auto-style1">
                                    <input id="txt_date" class="form-control input-s-sm" style="height: 35px; width: 130px" runat="server" 
                                        onclick="laydate({choose: function (dates) { setValue();}})"
                                        autopostback="True" ontextchanged="txt_date_TextChanged" onchange="txt_date_TextChanged" />
                                    <asp:RequiredFieldValidator ID="yz19" runat="server" ControlToValidate="txt_date" ValidationGroup="request" ForeColor="Red"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>申请工厂:</td>
                                <td>
                                    <asp:DropDownList ID="ddlzzgc" runat="server" class="form-control input-s-sm " Width="130px">
                                        <asp:ListItem Value=""></asp:ListItem>
                                        <asp:ListItem Value="上海工厂">上海工厂</asp:ListItem>
                                        <asp:ListItem Value="昆山工厂">昆山工厂</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td>相关部门:
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddl_depart" runat="server" class="form-control input-s-sm " Width="130px" OnSelectedIndexChanged="ddlDepart_SelectedIndexChanged" AutoPostBack="true">
                                    </asp:DropDownList>
                                </td>
                                <td>相关人员:
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddl_update_user" runat="server" class="form-control input-s-sm " Width="130px">
                                    </asp:DropDownList>
                                </td>
                                <td>是否逾时:
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddl_ISDelay" runat="server" class="form-control input-s-sm " Width="130px">
                                        <asp:ListItem Value="-1">All</asp:ListItem>
                                        <asp:ListItem Value="0">逾时</asp:ListItem>
                                        <asp:ListItem Value="1">未逾时</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td colspan="5" align="right">
                                    <asp:Button ID="Button1" runat="server" Text="查询"
                                        class="btn btn-large btn-primary"
                                        OnClick="Button1_Click" Width="100px" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div style="color: red">
        系统说明: 
                                   【完成状态】 红色底色:表示尚未处理，且已经逾时；
                                   【完成状态】 黄色底色:表示尚未处理，没有逾时，要求完成日期为当天，即将逾时；
    </div>
    <div runat="server" id="DIV1" style="margin-left: 5px; margin-right: 5px">
        <table width="100%">
            <tr>
                <td valign="top">
                    <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" OnRowDataBound="GridView1_RowDataBound" PageSize="100" Width="100%" OnPageIndexChanging="GridView1_PageIndexChanging" GridLines="Both" BorderColor="LightGray">
                        <Columns>
                             <asp:BoundField DataField="Baojia_no" HeaderText="No.">
                                <HeaderStyle BackColor="#C1E2EB"  />
                            </asp:BoundField>
                            <asp:BoundField DataField="Baojia_no" HeaderText="报价号">
                                <HeaderStyle BackColor="#C1E2EB" Wrap="false" />
                            </asp:BoundField>
                             <asp:TemplateField HeaderText="路径">                                
                                <ItemTemplate>  
                                                            
                                             <a class="fa fa-folder-open" href='<%# Eval("baojia_file_path")%>' target="_blank"></a>                            
                                </ItemTemplate>
                                <HeaderStyle BackColor="#C1E2EB" Width="40px" Wrap="False" />
                            </asp:TemplateField>        
                          
                             <asp:BoundField DataField="turns" HeaderText="第几轮">
                                <HeaderStyle BackColor="#C1E2EB" />
                            </asp:BoundField>
                                 <asp:BoundField DataField="domain" HeaderText="申请工厂">
                                <HeaderStyle BackColor="#C1E2EB" />
                            </asp:BoundField>
                             <asp:BoundField DataField="customer_name" HeaderText="直接顾客">
                                <HeaderStyle BackColor="#C1E2EB" Wrap="false" />
                            </asp:BoundField>                    
                              <asp:BoundField DataField="customer_project" HeaderText="顾客项目">
                                <HeaderStyle BackColor="#C1E2EB" Wrap="false" />
                            </asp:BoundField>
                            <asp:BoundField DataField="project_level" HeaderText="争取级别">
                                <HeaderStyle BackColor="#C1E2EB" />
                            </asp:BoundField>     
                              <asp:BoundField DataField="lastname" HeaderText="操作人">
                                <HeaderStyle BackColor="#C1E2EB" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Flow" HeaderText="操作部门">
                                <HeaderStyle BackColor="#C1E2EB" />
                            </asp:BoundField>
                            <asp:BoundField DataField="step" HeaderText="操作角色">
                                <HeaderStyle BackColor="#C1E2EB" />
                            </asp:BoundField>
                            <asp:BoundField DataField="receive_date" HeaderText="接收时间" DataFormatString="{0:yyyy-MM-dd}">
                                <HeaderStyle BackColor="#C1E2EB" />
                                <ItemStyle />
                            </asp:BoundField>
                            <asp:BoundField DataField="require_date" HeaderText="任务要求完成时间" DataFormatString="{0:yyyy-MM-dd}">
                                <HeaderStyle BackColor="#C1E2EB" />
                                <ItemStyle />
                            </asp:BoundField>
                             <asp:BoundField DataField="Operation_time" HeaderText="已耗时">
                                <HeaderStyle BackColor="#C1E2EB" />
                            </asp:BoundField>                    
                               <asp:BoundField DataField="ljh" HeaderText="零件号">
                                <HeaderStyle BackColor="#C1E2EB" Wrap="false" />
                            </asp:BoundField>
                            <asp:BoundField DataField="lj_name" HeaderText="零件名称">
                                <HeaderStyle BackColor="#C1E2EB" />
                            </asp:BoundField>
                            <asp:BoundField DataField="quantity_year" HeaderText="年用量" DataFormatString="{0:N0}" ItemStyle-HorizontalAlign="Right">
                                <HeaderStyle BackColor="#C1E2EB" />
                            </asp:BoundField>
                            
                            <asp:BoundField DataField="sign_status" HeaderText="完成状态">
                                <HeaderStyle BackColor="#C1E2EB" />
                            </asp:BoundField>
                              <asp:BoundField DataField="requestid" HeaderText="requestid">
                                <HeaderStyle BackColor="#C1E2EB" />
                            </asp:BoundField>
                        </Columns>
                        <PagerSettings FirstPageText="首页" LastPageText="尾页" Mode="NextPreviousFirstLast" NextPageText="下页" PreviousPageText="上页" />
                        <PagerStyle ForeColor="Red" />
                    </asp:GridView>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>


