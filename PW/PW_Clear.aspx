<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="PW_Clear.aspx.cs" Inherits="PW_PW_Clear" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
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
        
    </style>
    
    <script src="Content/js/jquery-ui-1.10.4.min.js" type="text/javascript"></script>
    <script src="../Content/js/jquery.min.js" type="text/javascript"></script>
    <script src="../Content/js/plugins/layer/layer.min.js" type="text/javascript"></script>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <script type="text/javascript">
        $(document).ready(function () {

            $("#mestitle").text("【抛丸机清理】");

        })//endready


         

    </script>
    <div class="row" style="margin: 0px 2px 1px 2px">
        <div class="col-sm-12 ">
            <div class="panel panel-info">
                <div class="panel-heading">
                    <strong>基本信息</strong>
                </div>
                <div class="panel-body">
                    <div class="col-sm-10">
                        <table style="width: 100%">
                            <tr>
                                <td>
                                    日期：
                                </td>
                                <td>
                                    <input id="txtRiQi" class="form-control input-s-sm" runat="server" />
                                </td>
                                <td>
                                    时间：
                                </td>
                                <td>
                                    <input id="txtShiJian" class="form-control input-s-sm" runat="server" />
                                </td>
                                <td>
                                    班别：
                                </td>
                                <td>
                                    <input id="txtBanBie" class="form-control input-s-sm" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    工号：
                                </td>
                                <td>
                                    <asp:DropDownList ID="dropGongHao" class="form-control input-s-sm" runat="server"   BackColor="Yellow" 
                                        AutoPostBack="True" OnSelectedIndexChanged="dropGongHao_SelectedIndexChanged" />
                                </td>
                                <td>
                                    姓名：
                                </td>
                                <td>
                                    <input id="txtXingMing" class="form-control input-s-sm" runat="server" />
                                </td>
                                <td>
                                    班组：
                                </td>
                                <td>
                                    <input id="txtBanZu" class="form-control input-s-sm" runat="server" />
                                </td>
                            </tr>
                            </table>
                    </div>
                    <div class="col-sm-2" style="float: left">
                        <div class="area area_border_gray" style="width: 100%; height: 100%">
                            <label class="control-label">
                                设备简称</label>
                            <div style="width: 100%; height: 100%">
                               
                                    <asp:Label ID="sbname" runat =server style="font-size: x-large" ></asp:Label>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="row" style="margin: 0px 2px 1px 2px">
                        <div class="col-sm-12">
                            <div class="panel panel-info">
                                <div class="panel-heading">
                                    <strong>抛丸机清理</strong>
                                </div>
                                <div class="panel-body">
                                   <div class="col-sm-12">
                        <asp:GridView ID="GridView1" runat="server"  Width="100%" 
                            AutoGenerateColumns="False" >
                            <Columns>
                                <asp:TemplateField HeaderText="清理事项">
                                    <ItemTemplate>
                                        <asp:Label ID="lb_item" runat="server" 
                                            Text='<%# Bind("CheckDescription") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle BackColor="#C8E1FB" Height="30px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="清理结果">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddl_result" runat="server"   class="form-control input-s-sm"
                                            >
                                            <asp:ListItem></asp:ListItem>
                                            <asp:ListItem>OK</asp:ListItem>
                                            <asp:ListItem>NG</asp:ListItem>
                                        </asp:DropDownList>
                                    </ItemTemplate>
                                    <HeaderStyle BackColor="#C8E1FB" />
                                    <ItemStyle  />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="说明">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txt_remark" runat="server"  class="form-control input-s-sm"></asp:TextBox>
                                    </ItemTemplate>
                                    <HeaderStyle BackColor="#C8E1FB" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="操作" ShowHeader="False">
                                    <ItemTemplate>
                                        <asp:Button ID="btn_clear" runat="server" Text="确认清理"   
                                            class="btn btn-primary " Font-Size="X-Large" 
                                            onclick="btn_clear_Click"/>
                                    </ItemTemplate>
                                    <HeaderStyle BackColor="#C8E1FB" />
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                       </div>
                     <%--    <div class="col-sm-12">
                                        <div >
                                            <div class="panel-body">
                                                <div class="form-group">
                                                    <div >
                                                        <div style="text-align: right;padding-bottom:5px; border-bottom:1px ; margin-bottom:-1px" >
                        <asp:Button ID="btn_return" class="btn btn-primary" Style="height: 55px; width: 100px"
                            Text="返回" runat="server" href="../index.aspx" />
                    </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>--%>
                                </div>
                            </div>
                        </div>
             

     
    </div>
      
</asp:Content>


