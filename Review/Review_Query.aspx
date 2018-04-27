<%@ Page Title="问题解决【问题查询】" Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.master" CodeFile="Review_Query.aspx.cs" Inherits="Review_Query" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <style type="text/css">
        td {
            padding-left: 5px;
            padding-right: 5px;
        }

        .auto-style1 {
            width: 100px;
        }

        .tblCondition td {
            white-space: nowrap;
            padding-bottom: 2px;
        }
        .GridView td {
            word-wrap:break-word;
            padding-bottom: 2px;
            /*word-break: break-all;*/
        }
    </style>
   <%-- <style>
        .div1{
            float: left;
            height: 41px;
            background: #007448;
            width: 120px;
            position:relative;
            border-top-left-radius: 4px;
            border-top-right-radius: 4px;
            border-bottom-right-radius: 4px;
            border-bottom-left-radius: 4px;
            cursor: pointer;
        }
    .div2{
        text-align:center;
        padding-top:12px;
        font-size:15px;
        cursor: pointer;
    }
    .inputstyle{
        width: 120px;
        height: 41px;
        cursor: pointer;
        font-size: 30px;
        outline: medium none;
        position: absolute;
        filter:alpha(opacity=0);
        -moz-opacity:0;
        opacity:0; 
        left:0px;
        top: 0px;
    }
    </style>--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <script src="../Content/js/jquery.min.js" type="text/javascript"></script>
    <script src="../Content/js/bootstrap.min.js"></script>
    <script src="../Content/js/plugins/layer/laydate/laydate.js"></script>
    <script src="../Content/js/plugins/layer/layer.min.js"></script>

    <script type="text/javascript">
        $("#mestitle").text("【问题解决查询】");
        $(document).ready(function () {
          

        });   
      

    </script>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>



    <div class="row row-container">
        <div class="col-sm-12 ">
            <div class="panel panel-info">
                <div class="panel-heading">
                    <strong>查询</strong>
                </div>
                <div class="panel-body">
                    <div class="col-sm-12">
                        <table class="tblCondition" style="width: 100%">
                            <tr>
                                <td>公司:
                                </td>
                                <td style="width: 150px; float: left; white-space: nowrap">
                                    <asp:DropDownList ID="ddlCompany" runat="server" class="form-control input-s-sm " Width="130px">
                                        <asp:ListItem Value="">ALL</asp:ListItem>
                                        <asp:ListItem Value="上海工厂">上海工厂</asp:ListItem>
                                        <asp:ListItem Value="昆山工厂">昆山工厂</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td>责任部门:
                                </td>
                                <td class="auto-style1">
                                    <asp:DropDownList ID="ddlDept" runat="server" class="form-control input-s-md " Width="150px">
                                        <asp:ListItem Value="" Selected="True">ALL</asp:ListItem>
                                        <asp:ListItem Value="工程一部">工程一部</asp:ListItem>
                                        <asp:ListItem Value="工程二部">工程二部</asp:ListItem>
                                        <asp:ListItem Value="产品一组">&nbsp;&nbsp;&nbsp;&nbsp;产品一组</asp:ListItem>
                                        <asp:ListItem Value="产品三组">&nbsp;&nbsp;&nbsp;&nbsp;产品三组</asp:ListItem>
                                        <asp:ListItem Value="调试组">&nbsp;&nbsp;&nbsp;&nbsp;调试组</asp:ListItem>
                                        <asp:ListItem Value="工程三部">工程三部</asp:ListItem>
                                        <asp:ListItem Value="产品二组">&nbsp;&nbsp;&nbsp;&nbsp;产品二组</asp:ListItem>
                                        <asp:ListItem Value="产品四组">&nbsp;&nbsp;&nbsp;&nbsp;产品四组</asp:ListItem>
                                        <asp:ListItem Value="项目管理部">项目管理部</asp:ListItem>
                                        <asp:ListItem Value="项目一组">&nbsp;&nbsp;&nbsp;&nbsp;项目一组</asp:ListItem>
                                        <asp:ListItem Value="项目二组">&nbsp;&nbsp;&nbsp;&nbsp;项目二组</asp:ListItem>
                                        <asp:ListItem Value="项目三组">&nbsp;&nbsp;&nbsp;&nbsp;项目三组</asp:ListItem>
                                        <asp:ListItem Value="生产一部">生产一部</asp:ListItem>
                                        <asp:ListItem Value="生产二部">生产二部</asp:ListItem>
                                        <asp:ListItem Value="生产三部（压铸）">生产三部（压铸）</asp:ListItem>
                                        <asp:ListItem Value="压铸技术部">压铸技术部</asp:ListItem>
                                        <asp:ListItem Value="质量二部">质量二部</asp:ListItem>
                                        <asp:ListItem Value="物流二部">物流二部</asp:ListItem>
                                        <asp:ListItem Value="销售二部">销售二部</asp:ListItem>
                                        <asp:ListItem Value="客户一组">&nbsp;&nbsp;&nbsp;&nbsp;客户一组</asp:ListItem>
                                        <asp:ListItem Value="客户二组">&nbsp;&nbsp;&nbsp;&nbsp;客户二组</asp:ListItem>
                                         <asp:ListItem Value="设备一部">设备一部</asp:ListItem>
                                         <asp:ListItem Value="设备二部">设备二部</asp:ListItem>
                                        <asp:ListItem Value="人事部">人事部</asp:ListItem>
                                        <asp:ListItem Value="IT部">IT部</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td>责任人:
                                </td>
                                <td style="width: 100px">
                                    <asp:TextBox ID="txtzzr" class="form-control input-s-sm" runat="server" Width="100px"></asp:TextBox>
                                </td>
                                <td>提出人:
                                </td>
                                <td class="auto-style1">
                                    <asp:TextBox ID="txttcr" class="form-control input-s-sm" runat="server"></asp:TextBox>
                                </td>
                                <td>客户: </td>
                                <td>
                                    <asp:DropDownList ID="ddlcustomer" runat="server" class="form-control input-s-sm " Width="130px">
                                        <asp:ListItem Value="">全部</asp:ListItem>

                                    </asp:DropDownList>
                                </td>
                                <td>问题状态:</td>
                                <td>
                                    <asp:DropDownList ID="ddlstatus" runat="server" class="form-control input-s-sm " Width="130px">
                                        <asp:ListItem Value="">ALL</asp:ListItem>
                                        <asp:ListItem Value="未关闭" Selected>未关闭</asp:ListItem>
                                        <asp:ListItem Value="已关闭">已关闭</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>问题来源:</td>
                                <td style="width: 150px; float: left; white-space: nowrap">

                                    <asp:DropDownList ID="ddlsource" runat="server" class="form-control input-s-sm " Width="130px">
                                        <asp:ListItem Value="">ALL</asp:ListItem>
                                        <asp:ListItem Value="客户投诉">客户投诉</asp:ListItem>
                                        <asp:ListItem Value="产品审核">产品审核</asp:ListItem>
                                        <asp:ListItem Value="过程审核">过程审核</asp:ListItem>
                                        <asp:ListItem Value="内审">内审</asp:ListItem>
                                        <asp:ListItem Value="管理评审">管理评审</asp:ListItem>
                                        <asp:ListItem Value="客户审核">客户审核</asp:ListItem>
                                        <asp:ListItem Value="第三方审核">第三方审核</asp:ListItem>
                                        <asp:ListItem Value="5S检查">5S检查</asp:ListItem>
                                        <asp:ListItem Value="EHS检查">EHS检查</asp:ListItem>
                                        <asp:ListItem Value="持续改进">持续改进</asp:ListItem>
                                        <asp:ListItem Value="月会问题跟踪">月会问题跟踪</asp:ListItem>
                                        <asp:ListItem Value="周会问题跟踪">周会问题跟踪</asp:ListItem>
                                        <asp:ListItem Value="早会">早会</asp:ListItem>
                                        <asp:ListItem Value="LPA审核">LPA审核</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td>提出日期:
                                </td>
                                <td style="width: 220px; float: left; white-space: nowrap">
                                    <div style="float: left; white-space: nowrap">
                                        <asp:TextBox ID="txtcreate_date1" class="form-control " onclick="laydate()" runat="server" Width="100px"></asp:TextBox>
                                    </div>
                                    <div style="float: left; white-space: nowrap">~</div>
                                    <div style="float: left; white-space: nowrap">
                                        <asp:TextBox ID="txtcreate_date2" class="form-control" onclick="laydate()" runat="server" Width="100px"></asp:TextBox>
                                    </div>
                                </td>
                                <td>问题描述:
                                </td>
                                <td>
                                    <asp:TextBox ID="txtProddesc" class="form-control input-s-sm" runat="server" Width="100px"></asp:TextBox>
                                </td>
                                <td>产品:
                                </td>
                                <td colspan="1">
                                    <asp:TextBox ID="txtProduct" class="form-control input-s-sm" runat="server" Width="100px"></asp:TextBox>
                                </td>
                                 <td>措施状态:
                                </td>
                                <td colspan="1">
                                   <asp:DropDownList ID="txtstatus1" runat="server" class="form-control input-s-sm " Width="130px">
                                        <asp:ListItem Value="">ALL</asp:ListItem>
                                        <asp:ListItem Value="已关闭(逾时)">已关闭(逾时)</asp:ListItem>
                                        <asp:ListItem Value="已关闭(未逾时)">已关闭(未逾时)</asp:ListItem>
                                       <asp:ListItem Value="未关闭(逾时)">未关闭(逾时)</asp:ListItem>
                                        <asp:ListItem Value="未关闭(未逾时)">未关闭(未逾时)</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td colspan="2">

                                    <div style="width: 100%; text-align: center">
                                        <asp:Button ID="Button1" runat="server" Text="查询" class="btn btn-large btn-primary" OnClick="Button1_Click" Width="100px" />
                                        <asp:Button ID="btnMore" runat="server" Text="查询(详细)" class="btn btn-large btn-primary" OnClick="btnMore_Click" Width="100px"  Visible="false"/>
                                    </div>
                                    <%--<div class="div1 btn-success">
                                        <div class="div2 ">select file</div>
                                        <input type="file" class=" form-control inputstyle"/>
                                    </div>--%>
                                </td>
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
                    <asp:GridView ID="GridView1" runat="server"  Width="1850px" CssClass="GridView" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" BorderColor="LightGray" PageSize="20" OnRowDataBound="GridView1_RowDataBound" OnPageIndexChanging="GridView1_PageIndexChanging">
                        <Columns>
                           <%-- <asp:BoundField DataField="RequestId" HeaderText="序号">
                                <HeaderStyle BackColor="#C1E2EB"  Width="40px"  />
                            </asp:BoundField>
                            <asp:BoundField DataField="probdate" HeaderText="问题提出<br>日期" HtmlEncode="false" DataFormatString="{0:yyyy/MM/dd}">
                                <HeaderStyle BackColor="#C1E2EB" Width="80px" Wrap="false" />
                            </asp:BoundField>
                           
                            <asp:BoundField DataField="probemp" HeaderText="提出人" >
                                <HeaderStyle BackColor="#C1E2EB" Width="60px" Wrap="false" />
                                <ItemStyle />
                            </asp:BoundField>
                            <asp:BoundField DataField="dh" HeaderText="单号">
                                <HeaderStyle BackColor="#C1E2EB" Width="60px" Wrap="false" />
                            </asp:BoundField>
                            <asp:BoundField DataField="domain" HeaderText="公司">
                                <HeaderStyle BackColor="#C1E2EB" Width="90px" Wrap="false" />
                            </asp:BoundField>
                            <asp:BoundField DataField="ProbFrom" HeaderText="问题来源">
                                <HeaderStyle BackColor="#C1E2EB" Width="80px" Wrap="false" />
                            </asp:BoundField>
                            <asp:BoundField DataField="ProdDesc" HeaderText="问题描述">
                                <HeaderStyle BackColor="#C1E2EB" Width="200px" Wrap="false" />
                            </asp:BoundField>
                            <asp:BoundField DataField="CustClass" HeaderText="客户">
                                <HeaderStyle BackColor="#C1E2EB" Width="80px" Wrap="false" />
                                <ItemStyle Width="80px"  Wrap="false"/>
                            </asp:BoundField>
                            <asp:BoundField DataField="ljh" HeaderText="零件号">
                                <HeaderStyle BackColor="#C1E2EB" Width="100px" Wrap="false" />
                                <ItemStyle />
                            </asp:BoundField>

                            <asp:BoundField DataField="ImproveTarget" HeaderText="改善目标及要求"  >
                                <HeaderStyle BackColor="#C1E2EB" Width="280px" Wrap="false" />
                                <ItemStyle />
                            </asp:BoundField>

                              <asp:BoundField DataField="ReqFinishDate" HeaderText="要求完成<br>日期"  HtmlEncode="false" DataFormatString="{0:yyyy/MM/dd}" >
                                <HeaderStyle BackColor="#C1E2EB" Width="80px" Wrap="false" />
                                <ItemStyle />
                            </asp:BoundField>

                            <asp:BoundField DataField="lastname" HeaderText="问题<br>责任人" HtmlEncode="false"  >
                                <HeaderStyle BackColor="#C1E2EB" Width="70px" Wrap="false" />
                            </asp:BoundField>

                            <asp:BoundField DataField="DutyDept" HeaderText="责任部门">
                                <HeaderStyle BackColor="#C1E2EB" Width="80px" Wrap="false" />
                            </asp:BoundField>
                            <asp:BoundField DataField="ActualCloseDate" HeaderText="实际关闭<br>日期"  HtmlEncode="false" DataFormatString="{0:yyyy/MM/dd}">
                                <HeaderStyle BackColor="#C1E2EB" Width="80px" Wrap="false" />
                                <ItemStyle />
                            </asp:BoundField>
                            <asp:BoundField DataField="d" HeaderText="用时(天)" >
                                <HeaderStyle BackColor="#C1E2EB" Width="50px" />
                                <ItemStyle HorizontalAlign="Right"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="s" HeaderText="问题状态" >
                                <HeaderStyle BackColor="#C1E2EB" Width="80px" Wrap="false" />
                                <ItemStyle />
                            </asp:BoundField>
                           
                              <asp:BoundField DataField="requestid" HeaderText="requestid" ShowHeader="false">
                                <HeaderStyle BackColor="#C1E2EB" Width="70px" Wrap="false" />
                            </asp:BoundField>--%>

                             <asp:BoundField DataField="requestid" HeaderText="requestid" ShowHeader="false">
                                <HeaderStyle BackColor="#C1E2EB" Width="0px" Wrap="false" CssClass="hidden" />
                                  <ItemStyle CssClass="hidden" />
                            </asp:BoundField>
                            <asp:BoundField DataField="RequestId" HeaderText="序号">
                                <HeaderStyle BackColor="#C1E2EB" Width="60px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="probdate" HeaderText="问题提<br>出日期" DataFormatString="{0:yyyy/MM/dd}" HtmlEncode="false">
                                <HeaderStyle BackColor="#C1E2EB" Width="80px" Wrap="false"  />
                            </asp:BoundField>
                           
                            <asp:BoundField DataField="probemp" HeaderText="提出人" >
                                <HeaderStyle BackColor="#C1E2EB" Width="80px" Wrap="false" />
                                <ItemStyle />
                            </asp:BoundField>
                            <asp:BoundField DataField="dh" HeaderText="单号">
                                <HeaderStyle BackColor="#C1E2EB" Width="60px" Wrap="false" />
                            </asp:BoundField>
                            <asp:BoundField DataField="domain" HeaderText="公司">
                                <HeaderStyle BackColor="#C1E2EB" Width="80px" Wrap="false" />
                            </asp:BoundField>
                            <asp:BoundField DataField="ProbFrom" HeaderText="问题来源">
                                <HeaderStyle BackColor="#C1E2EB" Width="60px" Wrap="false" />
                            </asp:BoundField>
                            <asp:BoundField DataField="ProdDesc" HeaderText="问题描述">
                                <HeaderStyle BackColor="#C1E2EB" Width="300px" Wrap="false" />
                            </asp:BoundField>
                            <asp:BoundField DataField="CustClass" HeaderText="客户">
                                <HeaderStyle BackColor="#C1E2EB" Width="80px" Wrap="false" />
                                <ItemStyle />
                            </asp:BoundField>
                            <asp:BoundField DataField="ljh" HeaderText="零件号">
                                <HeaderStyle BackColor="#C1E2EB" Width="100px" Wrap="false" />
                                <ItemStyle />
                            </asp:BoundField>

                            <asp:BoundField DataField="ImproveTarget" HeaderText="改善措施及要求"  >
                                <HeaderStyle BackColor="#C1E2EB" Width="200px" Wrap="false" />
                                <ItemStyle />
                            </asp:BoundField>

                              <asp:BoundField DataField="ReqFinishDate" HeaderText="要求完<br>成日期"  DataFormatString="{0:yyyy/MM/dd}"  HtmlEncode="false">
                                <HeaderStyle BackColor="#C1E2EB" Width="80px" Wrap="false" />
                                <ItemStyle />
                            </asp:BoundField>

                              <asp:BoundField DataField="actionplan" HeaderText="计划采取的行动"  >
                                <HeaderStyle BackColor="#C1E2EB" Width="500px" Wrap="false" />
                                <ItemStyle />
                            </asp:BoundField>

                            <asp:BoundField DataField="lastname" HeaderText="问题责<br>任人"  HtmlEncode="false" >
                                <HeaderStyle BackColor="#C1E2EB" Width="80px" Wrap="false" />
                            </asp:BoundField>

                            <asp:BoundField DataField="DutyDept" HeaderText="责任部门" HtmlEncode="false">
                                <HeaderStyle BackColor="#C1E2EB" Width="80px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="ConfirmDate" HeaderText="实际关<br>闭日期" DataFormatString="{0:yyyy/MM/dd}" HtmlEncode="false">
                                <HeaderStyle BackColor="#C1E2EB" Width="80px" Wrap="false" />
                                <ItemStyle />
                            </asp:BoundField>
                            <asp:BoundField DataField="d" HeaderText="用时" >
                                <HeaderStyle BackColor="#C1E2EB" Width="60px" />
                                <ItemStyle HorizontalAlign="Right"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="s1" HeaderText="任务<br>状态" HtmlEncode="false" >
                                <HeaderStyle BackColor="#C1E2EB" Width="80px" Wrap="false" />
                                <ItemStyle />
                            </asp:BoundField>

                        </Columns>
                        <EmptyDataTemplate>no data found</EmptyDataTemplate>
                        <PagerSettings FirstPageText="首页" LastPageText="尾页" Mode="NextPreviousFirstLast" NextPageText="下页" PreviousPageText="上页" />
                        <PagerStyle ForeColor="lightblue" />
                    </asp:GridView>
                    <asp:GridView ID="gvMore" runat="server" CssClass="GridView" AllowPaging="True" AllowSorting="True"  OnRowDataBound="gvMore_RowDataBound"  OnPageIndexChanging="gvMore_PageIndexChanging" AutoGenerateColumns="False" BorderColor="LightGray" PageSize="20"  >
                        <Columns>
                            <asp:BoundField DataField="RequestId" HeaderText="序号">
                                <HeaderStyle BackColor="#C1E2EB"  Width="40px"  />
                            </asp:BoundField>
                            <asp:BoundField DataField="probdate" HeaderText="问题提出<br>日期" HtmlEncode="false" DataFormatString="{0:yyyy/MM/dd}">
                                <HeaderStyle BackColor="#C1E2EB" Width="80px" Wrap="false" />
                            </asp:BoundField>
                           
                            <asp:BoundField DataField="probemp" HeaderText="提出人" >
                                <HeaderStyle BackColor="#C1E2EB" Width="60px" Wrap="false" />
                                <ItemStyle />
                            </asp:BoundField>
                            <asp:BoundField DataField="dh" HeaderText="单号">
                                <HeaderStyle BackColor="#C1E2EB" Width="60px" Wrap="false" />
                            </asp:BoundField>
                            <asp:BoundField DataField="domain" HeaderText="公司">
                                <HeaderStyle BackColor="#C1E2EB" Width="90px" Wrap="false" />
                            </asp:BoundField>
                            <asp:BoundField DataField="ProbFrom" HeaderText="问题来源">
                                <HeaderStyle BackColor="#C1E2EB" Width="80px" Wrap="false" />
                            </asp:BoundField>
                            <asp:BoundField DataField="ProdDesc" HeaderText="问题描述">
                                <HeaderStyle BackColor="#C1E2EB" Width="200px" Wrap="false" />
                            </asp:BoundField>
                            <asp:BoundField DataField="CustClass" HeaderText="客户">
                                <HeaderStyle BackColor="#C1E2EB" Width="80px" Wrap="false" />
                                <ItemStyle Width="80px"  Wrap="false"/>
                            </asp:BoundField>
                            <asp:BoundField DataField="ljh" HeaderText="零件号">
                                <HeaderStyle BackColor="#C1E2EB" Width="100px" Wrap="false" />
                                <ItemStyle />
                            </asp:BoundField>

                            <asp:BoundField DataField="ImproveTarget" HeaderText="改善目标及要求"  >
                                <HeaderStyle BackColor="#C1E2EB" Width="280px" Wrap="false" />
                                <ItemStyle />
                            </asp:BoundField>

                           <asp:BoundField DataField="ReqFinishDate" HeaderText="要求完成<br>日期"  HtmlEncode="false" DataFormatString="{0:yyyy/MM/dd}" >
                                <HeaderStyle BackColor="#C1E2EB" Width="80px" Wrap="false" />
                                <ItemStyle />
                            </asp:BoundField>
                            <%--11--%>
                            <asp:BoundField DataField="lastname" HeaderText="问题<br>责任人" HtmlEncode="false"  >
                                <HeaderStyle BackColor="#C1E2EB" Width="70px" Wrap="false" />
                            </asp:BoundField>

                            <asp:BoundField DataField="DutyDept" HeaderText="责任部门">
                                <HeaderStyle BackColor="#C1E2EB" Width="80px" Wrap="false" />
                            </asp:BoundField>
                            <asp:BoundField DataField="ActualCloseDate" HeaderText="实际关闭<br>日期"  HtmlEncode="false" DataFormatString="{0:yyyy/MM/dd}">
                                <HeaderStyle BackColor="#C1E2EB" Width="80px" Wrap="false" />
                                <ItemStyle />
                            </asp:BoundField>

                            <asp:BoundField DataField="Cause" HeaderText="原因分析"  >
                                <HeaderStyle BackColor="#C1E2EB" Width="200px" Wrap="false" />
                                <ItemStyle />
                            </asp:BoundField>
                            <asp:BoundField DataField="ActionPlan" HeaderText="改善措施"  >
                                <HeaderStyle BackColor="#C1E2EB" Width="200px" Wrap="false" />
                                <ItemStyle />
                            </asp:BoundField>
                            <asp:BoundField DataField="SlnState" HeaderText="措施状态"  >
                                <HeaderStyle BackColor="#C1E2EB" Width="100px" Wrap="false" />
                                <ItemStyle />
                            </asp:BoundField>
                            <asp:BoundField DataField="d" HeaderText="用时(天)" >
                                <HeaderStyle BackColor="#C1E2EB" Width="50px" />
                                <ItemStyle HorizontalAlign="Right"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="s" HeaderText="问题状态" >
                                <HeaderStyle BackColor="#C1E2EB" Width="80px" Wrap="false" />
                                <ItemStyle />
                            </asp:BoundField>
                           
                              <asp:BoundField DataField="requestid" HeaderText="requestid" ShowHeader="false">
                                <HeaderStyle BackColor="#C1E2EB" Width="70px" Wrap="false" />
                            </asp:BoundField>

                        </Columns>
                        <EmptyDataTemplate>no data found</EmptyDataTemplate>
                        <PagerSettings FirstPageText="首页" LastPageText="尾页" Mode="NextPreviousFirstLast" NextPageText="下页" PreviousPageText="上页" />
                        <PagerStyle ForeColor="lightblue" />
                    </asp:GridView>
                </td>
            </tr>


        </table>
    </div>
</asp:Content>
