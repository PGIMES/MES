<%@ Page Title="【压射头更换记录】" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="YST_Record.aspx.cs" Inherits="YaSheTou_YST_Record" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <script type="text/javascript" src="/Content/js/jquery.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#mestitle").html("【压射头更换记录】");


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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

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
                                    <input id="txtRiQi" class="form-control input-s-sm"   readonly="True" runat="server" />
                                </td>
                                <td>
                                    时间：
                                </td>
                                <td>
                                    <input id="txtShiJian" class="form-control input-s-sm"  readonly="True"  runat="server" />
                                </td>
                                <td>
                                    班别：
                                </td>
                                <td>
                                    <input id="txtBanBie" class="form-control input-s-sm"   readonly="True" runat="server" />
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
                                    <input id="txtXingMing" class="form-control input-s-sm"  readonly="True"  runat="server" />
                                </td>
                                <td>
                                    班组：
                                </td>
                                <td>
                                    <input id="txtBanZu" class="form-control input-s-sm"   readonly="True" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    设备号：
                                </td>
                                <td>
                                    <input id="txtSheBeiHao" class="form-control input-s-sm" runat="server"   readonly="True"/>
                                </td>
                                <td>
                                    设备简称：
                                </td>
                                <td>
                                    <input id="txtSheBeiJianCheng" class="form-control input-s-sm"  readonly="True" runat="server" />
                                </td>
                                <td></td>
                                <td></td>
                            </tr>
                            <tr>
                               
                                <td>
                                   换压射头类别：
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddl_change_type"  class="form-control input-s-sm" runat="server" BackColor="Yellow" 
                                        OnSelectedIndexChanged="ddl_change_type_SelectedIndexChanged" AutoPostBack="true">
                                        <asp:ListItem Value ="">--请选择--</asp:ListItem>
                                        <asp:ListItem Value ="仅上">仅上</asp:ListItem>
                                        <asp:ListItem Value ="仅下">仅下</asp:ListItem>
                                        <asp:ListItem Value ="先下再上">先下再上</asp:ListItem>

                                    </asp:DropDownList>

                                </td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div class="row" style="margin: 0px 2px 1px 2px">
        <div class="row">
            <div class="col-sm-12">
                <div class="col-sm-6 col-md-5" id="divXiaMo" runat ="server"  >
                    <div class="panel panel-info">
                        <div class="panel-heading">
                            <strong>下压缩头（卸）</strong>
                        </div>
                        <div class="panel-body">
                            <div class="form-group">
                                <span class="col-sm-4">压射头编码：</span>
                                <div class="col-sm-8">
                                    
                                        <asp:DropDownList ID="ddl_code" runat ="server" AutoPostBack="True"  class="form-control input-s-sm" BackColor="Yellow"
                                            onselectedindexchanged="ddl_code_SelectedIndexChanged"  ></asp:DropDownList>
                                </div>
                            </div>
                             <div class="form-group">
                                <span class="col-sm-4">额定模次：</span>
                                <div class="col-sm-8">
                                    <asp:TextBox ID="txt_mc" class="form-control" runat="server" ReadOnly="True"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group">
                                <span class="col-sm-4">供应商：</span>
                                <div class="col-sm-8">
                                    <asp:TextBox ID="txt_gys_Name" class="form-control" runat="server"  ReadOnly="True"></asp:TextBox>
                                    <asp:TextBox ID="txt_gys" class="form-control" runat="server"  ReadOnly="True" Visible="false"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group">
                                <span class="col-sm-4">直径：</span>
                                <div class="col-sm-8">
                                    <asp:TextBox ID="txt_zj" class="form-control" runat="server"  ReadOnly="True"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group">
                                <span class="col-sm-4">本次使用模次：</span>
                                <div class="col-sm-8">
                                    <asp:TextBox ID="txt_kaishi" class="form-control" runat="server"  ReadOnly="True" Visible="false"></asp:TextBox>
                                    <asp:TextBox ID="txt_deal_mc" class="form-control" runat="server" BackColor="Yellow" AutoPostBack="true" OnTextChanged="txt_deal_mc_TextChanged"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group">
                                <span class="col-sm-4">结束使用模次：</span>
                                <div class="col-sm-8">
                                    <asp:TextBox ID="txt_end_mc" class="form-control" runat="server" ReadOnly="True"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group">
                                <span class="col-sm-4">状态：</span>
                                <div class="col-sm-8">
                                    <asp:DropDownList ID="ddl_status" class="form-control input-s-sm" 
                                        runat="server" BackColor="Yellow">
                                        <asp:ListItem Value="正常">正常</asp:ListItem>
                                        <asp:ListItem Value="异常">异常</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="form-group">
                                <span class="col-sm-4" style="padding-right:2px">备注：</span>
                                <div class="col-sm-8">
                                    <asp:TextBox ID="txt_remark" class="form-control" runat="server" 
                                        BackColor="Yellow"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-sm-6  col-md-5" id="divShangMo"  runat ="server" >
                    <div class="panel panel-info">
                        <div class="panel-heading">
                            <strong>上压缩头（装）</strong>
                        </div>
                        <div class="panel-body">
                            <div class="form-group">
                                <span class="col-sm-4">压射头编码：</span>
                                <div class="col-sm-8">
                                    
                                        <asp:DropDownList ID="ddl_code_S" runat ="server" AutoPostBack="True"  class="form-control input-s-sm" BackColor="Yellow"
                                            onselectedindexchanged="ddl_code_S_SelectedIndexChanged" ></asp:DropDownList>
                                </div>
                            </div>
                             <div class="form-group">
                                <span class="col-sm-4">额定模次：</span>
                                <div class="col-sm-8">
                                    <asp:TextBox ID="txt_mc_S" class="form-control" runat="server" ReadOnly="True"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group">
                                <span class="col-sm-4">供应商：</span>
                                <div class="col-sm-8">
                                    <asp:TextBox ID="txt_gys_Name_S" class="form-control" runat="server"  ReadOnly="True"></asp:TextBox>
                                    <asp:TextBox ID="txt_gys_S" class="form-control" runat="server"  ReadOnly="True" Visible="false"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group">
                                <span class="col-sm-4">直径：</span>
                                <div class="col-sm-8">
                                    <asp:TextBox ID="txt_zj_S" class="form-control" runat="server"  ReadOnly="True"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group">
                                <span class="col-sm-4">开始使用模次：</span>
                                <div class="col-sm-8">
                                    <asp:TextBox ID="txt_start_mc" class="form-control" runat="server" ReadOnly="True"></asp:TextBox>
                                </div>
                            </div>                            
                            <div class="form-group">
                                <span class="col-sm-4">状态：</span>
                                <div class="col-sm-8">
                                    <asp:DropDownList ID="ddl_status_S" class="form-control input-s-sm" 
                                        runat="server" BackColor="Yellow">
                                        <asp:ListItem Value="正常">正常</asp:ListItem>
                                        <asp:ListItem Value="异常">异常</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="form-group">
                                <span class="col-sm-4" style="padding-right:2px">备注：</span>
                                <div class="col-sm-8">
                                    <asp:TextBox ID="txt_remark_S" class="form-control" runat="server" 
                                        BackColor="Yellow"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="col-sm-12 col-md-10">
                    <div class="row row-container">
                        <div class="col-sm-12">
                            <div id="Div3" runat="server" class="col-sm-6" style="padding: 0;
                                position: relative; float: left; top: 0px; left: 0px; width: 200px;
                                height: 70px;">
                                <asp:Button ID="btn_Save" runat="server" Font-Size="X-Large"
                                    class="btn btn-primary" Style="position: absolute; left: -1;
                                    right: 1px; width: 200px; top: -1px; height: 70px;" Text="确认" onclick="btn_Save_Click"
                                        />
                            </div>
                            <div id="Div7" runat="server" class="col-sm-6  col-md-offset-2" style="padding: 0;
                                position: relative; float: left; top: 0px; left: 0px; width: 200px;
                                height: 70px;">
                                <asp:Button ID="btn_Return" runat="server" Font-Size="X-Large"
                                    class="btn btn-primary" Style="width: 200px; top: -1px; height: 70px;" onclick="btnReturn_Click"
                                    Text="返回" 
                                    />
                            </div>
                        </div>
                    </div>
                </div>

            </div>
        </div>
    </div>


</asp:Content>

