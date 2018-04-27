<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="YZGX_XJ.aspx.cs" Inherits="shenhe_YZGX_XJ" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <script src="../Content/js/jquery.min.js" type="text/javascript"></script>
    <script src="../Content/js/plugins/layer/layer.min.js" type="text/javascript"></script>
   <%--  <script type="text/javascript">

         $(document).ready(function () {
             $("#btnBack").click(function () {
                 closelogin();
             })
         })
        


    </script>--%>



    <script type="text/javascript" language="javascript">
    function updateStock(id) {
        //iframe层
        parent.layer.open({
            type: 2,
            title: '修改',
            shadeClose: false, //点击遮罩关闭
            shade: 0.8,
            area: ['30%', '45%'],
            maxmin: true,
            closeBtn: 1,
            content: ['Moju_RK.aspx?id=' + id, 'yes'], //iframe的url，yes是否有滚动条
            //yes: function (index, layero) {
            //    alert(index);
            //    alert(layero);
            //},
            end: function () {
                location.reload();
            }

        });
    </script>

    <style>
        td
        {
            padding-left: 5px;
            padding-bottom: 3px;
        }
        .font-10
        {
            font-size: 14px;
            color: Red;
            font-weight: bold;
        }
        .classdiv
        {
            display: inline;
        }
    </style>  
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <script src="../Content/js/jquery.min.js" type="text/javascript"></script>
    <script src="../Content/js/plugins/layer/layer.min.js"></script>
     <script type="text/javascript">
         function clicked() {
             var title = '点检内容及处理方法';

             layer.open({
                 shade: [0.5, '#000', false],
                 type: 2,
                 offset: '20px',
                 area: ['700px', '550px'],
                 fix: false,
                 maxmin: false,
                 title: ['<i class="fa fa-dedent-"></i> ' + title, false],
                 closeBtn: 1,
                 content: 'YZCheck_Detail.aspx',
                 end: function () { }
             })
         }
        
       


</script>
    <div class="row" style="margin: 0px 2px 1px 2px">
        <div class="col-sm-12  col-md-10">
            <div class="panel panel-info">
                <div class="panel-heading">
                    <strong>基本信息</strong>
                </div>
                <div class="panel-body">
                    <div class="col-sm-12 col-md-10">
                        <table style="width: 100%">
                            <tr>
                                <td>
                                    日期：
                                </td>
                                <td>
                                    <input id="txtRiQi" class="form-control input-s-sm"   ReadOnly="True" runat="server" />
                                </td>
                                <td>
                                    时间：
                                </td>
                                <td>
                                    <input id="txtShiJian" class="form-control input-s-sm"  ReadOnly="True"  runat="server" />
                                </td>
                                <td>
                                    班别：
                                </td>
                                <td>
                                    <input id="txtBanBie" class="form-control input-s-sm"   ReadOnly="True" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    工号：
                                </td>
                                <td>
                                    <asp:DropDownList ID="dropGongHao" class="form-control input-s-sm" runat="server"
                                        AutoPostBack="True" 
                                        OnSelectedIndexChanged="dropGongHao_SelectedIndexChanged" BackColor="Yellow" />
                                </td>
                                <td>
                                    姓名：
                                </td>
                                <td>
                                    <input id="txtXingMing" class="form-control input-s-sm"  ReadOnly="True"  runat="server" />
                                </td>
                                <td>
                                    班组：
                                </td>
                                <td>
                                    <input id="txtBanZu" class="form-control input-s-sm"   ReadOnly="True" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    设备号：
                                </td>
                                <td>
                                    <input id="txtSheBeiHao" class="form-control input-s-sm" runat="server"   />
                                </td>
                                <td>
                                    设备简称：
                                </td>
                                <td>
                                    <input id="txtSheBeiJianCheng" class="form-control input-s-sm"    runat="server" />
                                </td>
                                <td>
                                    
                                </td>
                                <td>
                                    
                                </td>
                            </tr>
                            </table>
                    </div>
                </div>
            </div>
        </div>
    </div>
   <div class="row" style="margin: 0px 2px 1px 2px">
        <div class="col-sm-12  col-md-10">
            <div class="panel panel-info">
                <div class="panel-heading">
                    <strong>零件</strong>
                </div>
                <div class="panel-body">
                    <div class="col-sm-6 col-md-6">
                        <table style="width: 100%">
                            <tr>
                                <td>
                                    零件号：
                                </td>
                                <td>
                                    <input id="txtljh" class="form-control input-s-sm "   ReadOnly="True" runat="server" />
                                </td>
                                <td>
                                    模具编号：
                                </td>
                                <td>
                                    <asp:DropDownList ID="dropmoju_no" 
                                        class="form-control input-s-sm" runat="server"
                                        AutoPostBack="True" 
                                        />
                                </td>
                                <td>
                                    &nbsp;</td>
                              
                            </tr>
                            <tr>
                                <td>
                                            <asp:Button ID="btn_shoujian" runat="server" class="btn btn-primary" 
                                                Style="height: 35px; width: 100px" Text="首件" onclick="btn_shoujian_Click" 
                                                 />
                                            <asp:Button ID="btnNext" runat="server" 
                                                onclick="btnNext_Click" style="display: none" Text="Next" />
                                        </td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                </td>
                            </tr>
                            </table>
                    </div>
                </div>
            </div>
        </div>
    </div>
     <div class="row" style="margin: 0px 2px 1px 2px">
        <div class="col-sm-12  col-md-10">
            <div class="panel panel-info">
                <div class="panel-heading">
                    <strong>检查记录</strong>
                </div>
                <div class="panel-body">
                    <div class="col-sm-12 col-md-10">
                        <asp:GridView ID="GridView1" runat="server" 
                            AutoGenerateColumns="False" 
                            onrowdatabound="GridView1_RowDataBound">
                             <Columns>
                                 <asp:BoundField DataField="jcdatetime" HeaderText="时间" 
                                     DataFormatString="{0:HH:mm}" >
                                 <HeaderStyle BackColor="#C1E2EB" />
                                 </asp:BoundField>
                                 <asp:BoundField DataField="jclb" HeaderText="检测类别" >
                                 <HeaderStyle BackColor="#C1E2EB" />
                                 </asp:BoundField>
                                 <asp:BoundField DataField="ljh" HeaderText="零件号" >
                                 <HeaderStyle BackColor="#C1E2EB" />
                                 </asp:BoundField>
                                 <asp:BoundField DataField="moju_no" HeaderText="模具编号" >
                                 <HeaderStyle BackColor="#C1E2EB" />
                                 </asp:BoundField>
                                 <asp:TemplateField>
                                     <ItemTemplate>
                                         <asp:Button ID="btn_check" runat="server" Text="检查" onclick="btn_check_Click" 
                                            />
                                     </ItemTemplate>
                                 </asp:TemplateField>
                                 <asp:TemplateField>
                                     <ItemTemplate>
                                         <asp:Button ID="btn_close" runat="server" Text="关闭" 
                                             onclick="btn_close_Click" />
                                     </ItemTemplate>
                                 </asp:TemplateField>
                                 <asp:BoundField DataField="id" HeaderText="id">
                                 <ControlStyle CssClass="hidden" Width="0px" />
                                 <FooterStyle CssClass="hidden" Width="0px" />
                                 <HeaderStyle CssClass="hidden" Width="0px" />
                                 <ItemStyle CssClass="hidden" Width="0px" />
                                 </asp:BoundField>
                                 <asp:BoundField DataField="isok" HeaderText="isok">
                                 <ControlStyle CssClass="hidden" Width="0px" />
                                 <FooterStyle CssClass="hidden" Width="0px" />
                                 <HeaderStyle CssClass="hidden" Width="0px" />
                                 <ItemStyle CssClass="hidden" Width="0px" />
                                 </asp:BoundField>
                             </Columns>
                             <EmptyDataTemplate>暂无数据,请先执行首件作业</EmptyDataTemplate>
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>


