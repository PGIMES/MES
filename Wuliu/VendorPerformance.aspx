<%@ Page Title="供应商交货查询" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="VendorPerformance.aspx.cs" Inherits="VendorPerformance" %>

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
            ///开启供应商performance明细
            $("a[name='vendor']").click(function () {
                var year = $("select[id*='dropYear']").val();
                var week = $(this).attr("week");
                var domain = $("select[id*='dropcomp']").val();
                var vendor= $(this).attr("vendor"); 
                var title =  "performance明细";// 
               // $("[id*=GridViewYear_] td").click();

                layer.open({
                    shade: [0.5, '#000', false],
                    type: 2,
                    offset: '20px',
                    area: ['1200px', '700px'],
                    fix: false, //不固定
                    maxmin: false,
                    title: ['<i class="fa fa-dedent-"></i> ' + title, false],
                    closeBtn: 1,
                    content: 'VendorPerformance_dtl_byweek.aspx?&year=' + year + '&week=' + week + '&vendor=' + vendor + '&title=' + title + '&domain=' + domain ,
                    end: function () {

                    }
                });
            })
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
                                    年:
                                </td>
                                <td>
                                    <asp:DropDownList ID="dropYear" runat="server" class="form-control input-s-sm ">                                                                       
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    公司别:
                                </td>
                                <td>
                                    <asp:DropDownList ID="dropcomp" runat="server" class="form-control input-s-sm ">
                                        <asp:ListItem Value="200">200</asp:ListItem>
                                        <asp:ListItem Value="100">100</asp:ListItem>                                        
                                    </asp:DropDownList>
                                </td>
                                                               
                               
                               <td >供应商:</td>
                               <td >
                                    <asp:TextBox ID="txtNbr" class="form-control input-s-sm"  Width="80" runat="server"></asp:TextBox>
                               </td>
                                <td >负责人:</td>
                               <td >
                                    <asp:TextBox ID="txtCharger" class="form-control input-s-sm" Width="80"  runat="server"></asp:TextBox>
                               </td>
                               <td class="form-inline">
                                    显示近<asp:TextBox ID="weeks" class="form-control input-s-sm"  Width="50" Text="5" runat="server"></asp:TextBox>周
                               </td>
                                <td >
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
       
                        <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AllowSorting="false"
                            AutoGenerateColumns="true" OnPageIndexChanging="GridView1_PageIndexChanging" OnRowDataBound="GridView1_RowDataBound" OnDataBound="GridView1_DataBound"
                            PageSize="999"    BorderColor="">
                             <EmptyDataTemplate>  查无数据，请选择条件重新查询. </EmptyDataTemplate>
                            <Columns>
                               
                            </Columns>
                            <PagerSettings FirstPageText="首页" LastPageText="尾页" Mode="NextPreviousFirstLast" 
                                NextPageText="下页" PreviousPageText="上页"  />
                            <PagerStyle ForeColor="Red" />
                        </asp:GridView>
            
    </div>
</asp:Content>
