<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Fin_WG2_Report.aspx.cs" Inherits="Fin_Fin_WG2_Report" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">

<style>
    .gvHeader th{    background: #C1E2EB;    color:  Brown;    border: solid 1px #333333;    padding:0px 5px 0px 5px;font-size:12px; word-break:keep-all;}
   
</style>

  <script src="../Content/js/jquery.min.js" type="text/javascript"></script>
    <script src="../Content/js/bootstrap.min.js"></script>
    <script src="../Content/js/plugins/layer/layer.min.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            $("#Button2").click(function () {
            });
        });
        $("#mestitle").text("【完工报表二】");
    </script>

     <script type="text/javascript">
         $(document).ready(function () {
             $("input[id*='Button2']").click(function () {
                 var filename = $("input[id*='File1']").val();
                 if (filename == '') {
                     alert('请选择上传的EXCEL文件');
                     return false;
                 }
                 else {
                     var exec = (/[.]/.exec(filename)) ? /[^.]+$/.exec(filename.toLowerCase()) : '';
                     if (!(exec == "xlsx" || exec == "xls")) {
                         alert("文件格式不对，请上传Excel文件!");
                         return false;
                     }
                 } return true;
             });

             $("a[name='A']").click(function () {
                 year = $("select[id*='dropYear']").val();
                 month = $(this).attr("value"); //$("input[id*='txtmonth']").val();
                 var title = "人工过账明细";
                 comp = $(this).attr("comp");
                 $("[id*=GridViewYear_] td").click();

                 layer.open({
                     shade: [0.5, '#000', false],
                     type: 2,
                     offset: '20px',
                     area: ['1200px', '700px'],
                     fix: false, //不固定
                     maxmin: false,
                     title: ['<i class="fa fa-dedent-"></i> ' + title, false],
                     closeBtn: 1,
                     //                 content: 'Fin_RG_GZDetail.aspx',
                     content: 'Fin_RG_GZDetail.aspx?&year=' + year + '&month=' + month + '&comp=' + comp + '&title=' + title,
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
                    年：
                </td>
                <td>
                    <asp:DropDownList ID="dropYear" runat="server" class="form-control input-s-sm">
                    </asp:DropDownList>
                </td>
                <td>
                    月：
                </td>
                <td>
                    <asp:DropDownList ID="dropMnth" runat="server" class="form-control input-s-sm">

                    </asp:DropDownList>
                </td>
                <td>
                    工厂：
                </td>
                <td>
                    <asp:DropDownList ID="dropcomp" runat="server" class="form-control input-s-sm">
                        <asp:ListItem>200</asp:ListItem>
                        <asp:ListItem>100</asp:ListItem>
                        
                    </asp:DropDownList>
                </td>
                 <td align="right">&nbsp;&nbsp;</td>
                <td>
                    <asp:Button ID="btnQuery" runat="server" Text="查询" class="btn btn-lg btn-primary"
                        OnClick="btnQuery_Click" Width="92px" Height="45px" />
                  
                   
                </td>
                  <td align="right">&nbsp;&nbsp;</td>
                <td ><asp:Button ID="Button1" runat="server" Text="数据汇入"  
                        class="btn btn-lg btn-primary" Width="180px" 
                        Height="45px" onclick="Button1_Click" 
                       /></td>
                         <td align="right">&nbsp;&nbsp;</td>
                         <td ><asp:Button ID="export" runat="server" Text="导出Excel" 
                        class="btn btn-lg btn-primary" Width="120px" Height="45px" 
                        onclick="export_Click"/></td>
                                           </tr>
                        </table>
                    </div>
                </div>
            </div>
        </div>
    </div>


    <div class="row row-container" runat="server" id="Div_fp">
        <div class="col-sm-12 ">
            <div class="panel panel-info">
                
                <div class="panel-body">
                    <div class="col-sm-12">
                        <table>
                           <tr>

                 <td >     
                  <input id="File1" style ="width: 300px"   type="file" runat="server" />
                     <asp:Button ID="Button2" runat="server" Text="上传"  
                         class="btn btn-lg btn-primary" Width="80px" Height="45px" 
                         onclick="Button2_Click"  />
                               <asp:HyperLink ID="HyperLink1" runat="server" 
                         NavigateUrl="~/Fin/模具和刀具汇入格式.xlsx">模具和刀具汇入格式</asp:HyperLink>
                               </td>

                                           </tr>
                        </table>
                    </div>
                </div>
            </div>
        </div>
    </div>


    <div class="panel panel-info">
        <div class="panel-heading" >
            <strong>数据显示：</strong>
           
        </div>
        
    </div>



    <div runat="server" id="DIV1" style="margin-left: 5px; margin-right: 5px; margin-bottom: 10px">
        <asp:GridView ID="GridView1" runat="server"  CssClass="gvHeader"
            onrowdatabound="GridView1_RowDataBound" 
            AllowPaging="True" 
            onpageindexchanging="GridView1_PageIndexChanging" 
            PageSize="100">
         <EmptyDataTemplate>
                            查无资料</EmptyDataTemplate>
         <PagerSettings FirstPageText="首页" LastPageText="尾页" Mode="NextPreviousFirstLast" NextPageText="下页" PreviousPageText="上页" />
        </asp:GridView>
    </div>
</asp:Content>
