<%@ Page Title="【人员&产能核查上传资料】" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Upload_Base_CapacityPlan.aspx.cs" Inherits="CapacityPlan_Upload_Base_CapacityPlan" %>

<%@ Register Assembly="DevExpress.Web.v17.2, Version=17.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">    
    <link href="../Content/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <script src="../Content/js/jquery.min.js"></script>
    <script src="../Content/js/bootstrap.min.js" type="text/javascript"></script>
    <script src="/Content/js/layer/layer.js" type="text/javascript"></script>
    <script src="/Content/js/plugins/layer/laydate/laydate.js"></script>

    <script type="text/javascript">        
        $("#mestitle").text("【人员&产能核查上传资料】");

         function open_upload() {
             var url = "select_upload.aspx";

             layer.open({
                 title: '上传资料-生产计划',
                 closeBtn: 2,
                 type: 2,
                 area: ['500px', '400px'],
                 fixed: false, //不固定
                 maxmin: true, //开启最大化最小化按钮
                 content: url
             });
         }

    </script>

</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" Runat="Server" enctype="multipart/form-data">

     <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="col-md-12  ">
        <div class="row row-container">
            <div class="panel panel-info">
                <div class="panel-heading" data-toggle="collapse" data-target="#FJSC2">
                        <strong>查询条件&上传</strong>
                </div>
                <div class="panel-body collapse in" id="FJSC2">
                    <div class="col-xs-12 col-sm-12  col-md-12 col-lg-12" style="width:1100px;">
                        <table>
                            <tr style="line-height:52px;">
                                <td width="80px;">
                                    工艺代码：
                                </td>
                                <td width="110px;">
                                    <asp:TextBox ID="txt_pgi_no" class="form-control" runat="server" Width="100px"></asp:TextBox>
                                </td>
                                <td width="50px;">日期：</td>
                                <td width="110px;">
                                   <asp:TextBox ID="txtDateFrom" class="form-control" onclick="laydate()" runat="server" Width="100px"></asp:TextBox>
                                </td>
                                <td>~</td>
                                <td width="110px;">
                                   <asp:TextBox ID="txtDateTo" class="form-control" onclick="laydate()" runat="server" Width="100px"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Button ID="Bt_select" runat="server" Text="查询" class="btn btn-large btn-primary" Width="80px" OnClick="Bt_select_Click" />    
                                    <asp:Button ID="btn_export" runat="server" Text="导出Excel"  class="btn btn-primary" Width="100px" OnClick="btn_export_Click" />  
                                    <%--<asp:Button ID="btn_del" runat="server" Text="删除" class="btn btn-large btn-primary" Width="80px" 
                                        OnClick="btn_del_Click" OnClientClick="return confirm('确认要删除吗?');" />  --%>
                                    <input type="button"  value="上传" class="btn btn-large btn-primary" style="width:80px; height:35px;"  onclick="open_upload()" />                                     
                                </td>
                            </tr>
                        </table>
                     </div>
                </div>
            </div>      
        </div>
    </div>

    <div class="panel-body">
        <div class="col-sm-12">
            <table>
                <tr>
                    <td>
                        <dx:ASPxGridView ID="gv"  ClientInstanceName="grid" runat="server" KeyFieldName="pgi_no" AutoGenerateColumns="False" OnPageIndexChanged="gv_PageIndexChanged">
                            <SettingsPager PageSize="1000" ></SettingsPager>
                            <Settings ShowFilterRow="True" ShowGroupPanel="false" ShowFilterRowMenu="True" ShowFilterRowMenuLikeItem="True" AutoFilterCondition="Contains"  />
                            <SettingsBehavior AllowFocusedRow="True" ColumnResizeMode="Control" />
                            <Columns>
                                <%-- <dx:GridViewCommandColumn ShowSelectCheckbox="true" VisibleIndex="0" >
                                    <HeaderTemplate>
                                        <dx:ASPxCheckBox ID="DchkAll" runat="server" ClientSideEvents-CheckedChanged="function(s,e){grid.SelectAllRowsOnPage(s.GetChecked());}">
                                        </dx:ASPxCheckBox>
                                    </HeaderTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                </dx:GridViewCommandColumn>   --%>
                            </Columns>
                            <Styles>
                                <Header BackColor="#99CCFF"></Header>
                                <FocusedRow BackColor="#99CCFF" ForeColor="#0000CC"></FocusedRow>
                                <Footer HorizontalAlign="Right"></Footer>
                            </Styles>
                        </dx:ASPxGridView>
                         <dx:ASPxGridViewExporter ID="ASPxGridViewExporter1" runat="server">
                        </dx:ASPxGridViewExporter>
                    </td>
                </tr>
            </table>
        </div>
    </div>


</asp:Content>

