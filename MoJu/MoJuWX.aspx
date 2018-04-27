<%@ Page Title="MES【模具维修】" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    ViewStateMode="Enabled" MaintainScrollPositionOnPostback="true" CodeFile="MoJuWX.aspx.cs"
    Inherits="MoJuWX" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script src="../Content/js/jquery.min.js" type="text/javascript"></script>
    <script src="../Content/js/plugins/layer/layer.min.js" type="text/javascript"></script>
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
        .classdiv
        {
            display: inline;
        }
    </style>
    <style type="text/css">
        .datable
        {
            background-color: #9FD6FF;
            color: #333333;
            font-size: 12px;
        }
        .datable tr
        {
            height: 20px;
        }
        .datable .lup
        {
            background-color: #C8E1FB;
            font-size: 12px;
            color: #014F8A;
        }
        .datable .lup th
        {
            border-top: 1px solid #FFFFFF;
            border-left: 1px solid #FFFFFF;
            font-weight: normal;
        }
        .datable .lupbai
        {
            background-color: #FFFFFF;
        }
        .datable .trnei
        {
            background-color: #F2F9FF;
        }
        .datable td
        {
            border-top: 1px solid #FFFFFF;
            border-left: 1px solid #FFFFFF;
        }
    </style>
    <script src="../Content/js/jquery-ui-1.10.4.min.js" type="text/javascript"></script>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#mestitle").text("【模具维修】");
            //开始维修
            $("input[id*='btnStart']").click(function () {
                if ($("select[id*='dropGongHao']").val() == "") {
                    layer.msg("请选择基本信息【维修工号】！");
                    $("input[id*='txtWX_CS']").focus();
                    return false;
                }
            })
            //确认按钮验证
            $("input[id*='btnConfirm']").click(function () {
                return QRCheck();
                $("input[id*='btnConfirm']").prop("disabled", true);
            })
            //维修结束按钮
            $("input[id*='btnEnd']").click(function () {
                var msg = "";
                if ($("input[id*='txtWX_CS']").val() == "") {
                    layer.msg("请输入【维修措施】！");
                    $("input[id*='txtWX_CS']").focus();
                    return false;
                }
                if ($("select[id*='dropResult']").val() == "") {
                    layer.msg("请选择【维修结果】！");
                    $("select[id*='dropResult']").focus();
                    return false;
                }
                if ($("select[id*='dropResult']").val() != "恢复正常") {
                    if ($("input[id*='txtMo_Down_cs']").val() == "") {
                        layer.msg("请输入【还需下模后还需争取的措施】！");
                        return false;
                    }
                }

            })


        })//endready
        function QRCheck() {
            if ($("input[id*='txtQr_gh']").val() == "" || $("input[id*='txtQr_Name']").val() == "") {
                layer.msg("请选择正确的【确认工号】。");
                $("input[id*='txtQr_gh']").focus();
                return false;                
            }
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
                                    维修工号：
                                </td>
                                <td>
                                    <asp:DropDownList ID="dropGongHao" class="form-control input-s-sm" runat="server"
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
                </div>
            </div>
        </div>
    </div>
    <div class="col-sm-12 col-md-10">
        <div class="panel panel-info">
            <div class="panel-heading">
                <strong>第一步：选择报修单</strong>
            </div>
         
            <div class="panel-body">
                <asp:GridView ID="GridView1" runat="server" Width="100%" CssClass="datable" border="0"
                    CellPadding="2" CellSpacing="1"   AutoGenerateColumns="False"  autopostback="true"
                    onselectedindexchanged="GridView1_SelectedIndexChanged" 
                    onrowdatabound="GridView1_RowDataBound" >
                    <RowStyle CssClass="lupbai" />
                    <HeaderStyle CssClass="lup" />
                    <AlternatingRowStyle CssClass="trnei" />
                    <Columns>
                        <asp:CommandField ShowSelectButton="True" ><HeaderStyle Width="10px" /></asp:CommandField>                       
                        <asp:BoundField DataField="bx_dh" HeaderText="报修单号">
                            <HeaderStyle Width="100px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="bx_date" HeaderText="报修时间">
                            <HeaderStyle Width="100px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="bx_moju_no" HeaderText="模具号" HtmlEncode="False">
                            <HeaderStyle Width="120px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="bx_moju_type" HeaderText="模具类型">
                            <HeaderStyle Width="120px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="bx_part" HeaderText="零件号">
                            <HeaderStyle Width="120px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="bx_mo_no" HeaderText="模号">
                            <HeaderStyle Width="50px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="bx_gz_type" HeaderText="故障类型">
                            <HeaderStyle Width="40px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="bx_gz_desc" HeaderText="故障描述">
                            <HeaderStyle Width="80px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="bx_shichang" HeaderText="报修时长">
                            <HeaderStyle Width="40px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="bx_name" HeaderText="报修人">
                            <HeaderStyle Width="40px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="status" HeaderText="状态">
                            <HeaderStyle Width="40px" />
                        </asp:BoundField>
                    </Columns>
                </asp:GridView>
                <asp:HiddenField id="txtHidden" runat="server"/>
            </div>
            <div style="text-align: center">
                <asp:Button ID="btnStart" class="btn btn-primary" Style="height: 35px; width: 100px"
                    Text="开始维修" runat="server" onclick="btnStart_Click" />
            </div>
        </div>
    </div>
    <div class="row" style="margin: 0px 2px 1px 2px">
        <div class="col-sm-5 col-md-5">
            <div class="panel panel-info">
                <div class="panel-heading">
                    <strong>第二步：维修</strong>
                </div>
                <div class="panel-body">
                    <table width="100%">
                        <tr>
                            <td>
                                维修措施：
                            </td>
                            <td colspan="5">
                                <input type=text  ID="txtWX_CS" class="form-control " runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                维修结果：
                            </td>
                            <td>
                                <asp:DropDownList ID="dropResult" runat="server" class="form-control " 
                                    AutoPostBack="True" onselectedindexchanged="dropResult_SelectedIndexChanged">
                                    <asp:ListItem Value="" Selected="True">-请选择维修结果-</asp:ListItem>
                                    <asp:ListItem Value="恢复正常">恢复正常</asp:ListItem>
                                    <asp:ListItem Value="暂时恢复正常">暂时恢复正常</asp:ListItem>
                                    <asp:ListItem Value="需下模维修">需下模维修</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td style="text-align: right">
                                下模后还需争取的措施：
                            </td>
                            <td colspan="3">
                               <input type=text  ID="txtMo_Down_cs" class="form-control" runat="server"/>
                            </td>
                        </tr>
                    </table>
                    <div class="form-group">
                        <div class="input-group-btn" style="text-align: right">
                            <asp:Button ID="btnEnd" class="btn btn-primary" Style="height: 35px; width: 100px"
                                Text="维修完成" runat="server" onclick="btnEnd_Click" />
                                 <asp:Button ID="btnNext" runat="server" Text="Next" 
                onclick="btnNext_Click" style=" display:none"    />
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-sm-5 ">
            <div class="panel panel-info">
                <div class="panel-heading">
                    <strong>第三步：确认维修</strong>
                </div>
                <div class="panel-body">
                    <table width="100%">
                        <tr>
                            <td>
                                确认工号：
                            </td>
                            <td> <asp:DropDownList ID="dropQr_gh" class="form-control input-s-sm" runat="server"
                                        AutoPostBack="True" 
                                    onselectedindexchanged="dropQr_gh_SelectedIndexChanged"   />
                                 
                            </td>
                            <td>
                                姓名：
                            </td>
                            <td>
                                <input type=text  ID="txtQr_Name" class="form-control " runat="server" />
                            </td>
                        </tr>
                    </table>
                    对维修结果的补充说明：
                    <input type=text  ID="txtQr_Remark" class="form-control" runat="server"></asp:TextBox>
                    <div class="form-group">
                        <div class="input-group-btn" style="text-align: right">
                            
                            <asp:Button ID="btnConfirm" class="btn btn-primary" Style="height: 35px; width: 100px"
                                Text="确认完成" runat="server" onclick="btnConfirm_Click" 
                                onclientclick="return QRCheck();" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="input-group-btn" style="text-align: center">
        <asp:Button ID="Button3" class="btn btn-primary" Style="height: 35px; width: 100px"
            Text="返 回" runat="server" onclick="Button3_Click" />
    </div>
</asp:Content>
