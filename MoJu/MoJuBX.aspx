<%@ Page Title="MES【模具报修.维修.确认】" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    ViewStateMode="Enabled" MaintainScrollPositionOnPostback="true" CodeFile="MoJuBX.aspx.cs"
    Inherits="MoJuBX" %>

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
        .nowrap
        {
            white-space: nowrap;
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
            $("#mestitle").text("【模具报修.维修.确认】");          

            //报修
            $("input[id*='btn_Start']").click(function () {                
                   // layer.msg("处理中，请稍后！");
                    //layer.load(1, {
                    //    shade: [0.1, '#fff'] //0.1透明度的白色背景
                //})
              //  layer.load('处理中，请稍后！', 3);
                layer.load('加载中…');
                $("input[id*='btn_Start']").val('处理中…');
            })
            //开始维修
            $("input[id*='btnStart']").click(function () {
                if ($("select[id*='dropWXGongHao']").val() == "") {
                    layer.msg("请选择【维修工号】！");
                    $("select[id*='dropWXGongHao']").focus();
                    return false;
                }
                $("input[id*='btnStart']").css("display", "none");
                $("#spanMsg").css("display", "");
            })
            //确认按钮验证
            $("input[id*='btnConfirm']").click(function () {
                $("input[id*='btnConfirm']").val("处理中...");
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
                        $("input[id*='txtMo_Down_cs']").focus();
                        return false;
                    }
                }

                $("input[id*='btnEnd']").val("处理中...");
            })

        })//endready

        function QRCheck() {
            if ($("select[id*='dropQr_gh']").val() == "" || $("input[id*='txtQr_Name']").val() == "") {
                layer.msg("请选择正确的【确认工号】。");
                $("select[id*='dropQr_gh']").focus();
                return false;
            }
        }

        function setenableWXStart(bln) {
            if (bln == true) {
                $("select[id*='dropWXGongHao']").addClass("input-edit");               
            }
            else {
                $("select[id*='dropWXGongHao']").removeClass("input-edit");
            }
        }
        function setenableWXFinish(bln) {
            if (bln == true) {
                $("input[id*='txtWX_CS']").addClass("input-edit");
                $("select[id*='dropResult']").addClass("input-edit");
                $("input[id*='txtMo_Down_cs']").addClass("input-edit");
            }
            else {
                $("input[id*='txtWX_CS']").removeClass("input-edit");
                $("select[id*='dropResult']").removeClass("input-edit");
                $("input[id*='txtMo_Down_cs']").removeClass("input-edit");
            }
        }
        function setenableWXConfirm(bln) {
            if (bln == true) {
                $("select[id*='dropQr_gh']").addClass("input-edit");
                $("textarea[id*='txtQr_Remark']").addClass("input-edit");
            }
            else {
                $("select[id*='dropQr_gh']").removeClass("input-edit");
                $("textarea[id*='txtQr_Remark']").removeClass("input-edit");
            }
        }
    </script>
    <div class="row" style="margin: 0px 2px 1px 2px">
        <div class="col-sm-12  col-md-12">
            <div class="panel panel-info">
                <div class="panel-heading">
                    <strong>基本信息</strong>
                </div>
                <div class="panel-body">
                    <div class="col-sm-12 col-md-12">
                        <table style="width: 100%">
                            <tr>
                                <td>
                                    日期：
                                </td>
                                <td>
                                    <input id="txtRiQi" class="form-control input-s-sm" runat="server" readonly="True" />
                                </td>
                                <td>
                                    时间：
                                </td>
                                <td>
                                    <input id="txtShiJian" class="form-control input-s-sm" runat="server" readonly="True" />
                                </td>
                                <td>
                                    班别：
                                </td>
                                <td>
                                    <input id="txtBanBie" class="form-control input-s-sm" runat="server" readonly="True" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    工号：
                                </td>
                                <td>
                                    <asp:DropDownList ID="dropGongHao" class="form-control input-s-sm" runat="server"
                                        BackColor="Yellow" AutoPostBack="True" OnSelectedIndexChanged="dropGongHao_SelectedIndexChanged" />
                                </td>
                                <td>
                                    姓名：
                                </td>
                                <td>
                                    <input id="txtXingMing" class="form-control input-s-sm" runat="server" readonly="True" />
                                </td>
                                <td>
                                    班组：
                                </td>
                                <td>
                                    <input id="txtBanZu" class="form-control input-s-sm" runat="server" readonly="True" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="row" style="margin: 0px 2px 1px 2px">
        <div class="col-sm-12 col-md-12">
            <div class="panel panel-info">
                <div class="panel-heading">
                    <strong>一.报修</strong>
                </div>
                <div class="panel-body">
                    <table>
                        <tr>
                            <td>
                                模具号：
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlMoJuHao" runat="server" BackColor="Yellow" AutoPostBack="True"
                                    class="form-control input-s-sm" OnSelectedIndexChanged="ddlMoJuHao_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                            <td>
                                设备简称：
                            </td>
                            <td>
                                <asp:TextBox ID="txtSbname" class="form-control " runat="server" ReadOnly="true" />
                            </td>
                            <td>
                                是否停机：
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlstop" runat="server" BackColor="Yellow" class="form-control input-s-sm">
                                    <asp:ListItem Value="">--请选择--</asp:ListItem>
                                    <asp:ListItem Value="Y">是</asp:ListItem>
                                    <asp:ListItem Value="N">否</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                模具类型：
                            </td>
                            <td>
                                <asp:TextBox ID="txtMoJutype" class="form-control " runat="server" ReadOnly="true"></asp:TextBox>
                            </td>
                            <td>
                                零件号：
                            </td>
                            <td>
                                <asp:TextBox ID="txtMoJuljh" class="form-control " runat="server" ReadOnly="true"></asp:TextBox>
                            </td>
                            <td>
                                模号:
                            </td>
                            <td>
                                <asp:TextBox ID="txtMono" class="form-control " runat="server" ReadOnly="true"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                故障类型：
                            </td>
                            <td colspan="5">
                                <asp:DropDownList ID="ddl_gz" runat="server" class="form-control " BackColor="Yellow">
                                </asp:DropDownList>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                故障描述：
                            </td>
                            <td colspan="5">
                                <asp:TextBox ID="ddl_gz_ms" class="form-control " BackColor="Yellow" runat="server"
                                    TextMode="MultiLine"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                    <div class="col-sm-8 col-md-6">
                        <div class="">
                            <div class="">
                                <div class="form-group">
                                    <div  style="text-align: center">
                                        <asp:Button ID="btn_Start" class="btn btn-primary" Style="height: 35px; width: 100px"
                                            Text="报修" runat="server" OnClick="btn_Start_Click" /><asp:Label ID="lblStart_time"
                                                runat="server" Text=""></asp:Label>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="col-sm-12 col-md-12">
        <div class="panel panel-info">
            <div class="panel-heading">
                <strong>待维修 / 确认清单</strong><font>(选择报修单,方可进行开始维修或维修确认)</font>
            </div>
            <div class="panel-body">
                <asp:GridView ID="GridView1" runat="server" Width="100%" CssClass="datable" border="0"
                    CellPadding="2" CellSpacing="1" AutoGenerateColumns="False" autopostback="true"
                    OnSelectedIndexChanged="GridView1_SelectedIndexChanged" OnRowDataBound="GridView1_RowDataBound">
                    <RowStyle CssClass="lupbai" />
                    <HeaderStyle CssClass="lup" />
                    <AlternatingRowStyle CssClass="trnei" />
                    <Columns>
                        <asp:CommandField ShowSelectButton="True">
                            <HeaderStyle Width="10px" />
                        </asp:CommandField>
                        <asp:BoundField DataField="bx_dh" HeaderText="报修单号">
                            <HeaderStyle Width="100px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="bx_date" HeaderText="报修时间">
                            <HeaderStyle Width="100px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="bx_sbname" HeaderText="设备号">
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
                    <EmptyDataRowStyle BackColor="White" />
                    <EmptyDataTemplate>
                        模具暂时运行状况良好，暂未查询到新的报修数据.  可尝试稍后点<asp:LinkButton ID="lbtn" runat="server" style=" font-weight:bold" OnClick="lbtn_Click">刷新数据</asp:LinkButton>获取报修记录</EmptyDataTemplate>
                    <PagerSettings FirstPageText="首页" LastPageText="尾页" Mode="NextPreviousFirstLast"
                        NextPageText="下页" PreviousPageText="上页" />
                    <PagerStyle ForeColor="Red" />
                </asp:GridView>
                <asp:HiddenField ID="txtHidden" runat="server" />
            </div>
        </div>
    </div>
    <div class="row" style="margin: 0px 2px 1px 2px">
        <div class="col-sm-6 col-md-6">
            <div class="panel panel-info">
                <div class="panel-heading">
                    <strong>二.维修</strong>
                </div>
                <div class="panel-body">
                    <table width="100%">
                        <tr>
                            <td>
                                维修工号:
                            </td>
                            <td>
                                <asp:DropDownList ID="dropWXGongHao" class="form-control input-s-sm " runat="server"
                                    AutoPostBack="True" Width="140px" OnSelectedIndexChanged="dropWXGongHao_SelectedIndexChanged" />
                            </td>
                            <td>
                                姓名:
                            </td>
                            <td>
                                <input id="txtWXXingMing" class="form-control input-s-sm" width="60px" runat="server"
                                    readonly="True" />
                            </td>
                            <td>
                                班组:
                            </td>
                            <td>
                                <input id="txtWXBanZu" class="form-control input-s-sm" width="60px" runat="server"
                                    readonly="True" />
                            </td>
                        </tr>
                    </table>
                    <div style="text-align: right;padding-bottom:5px; border-bottom:1px solid lightblue; margin-bottom:4px" >
                        <asp:Button ID="btnStart" class="btn btn-primary" Style="height: 35px; width: 100px"
                            Text="开始维修" runat="server" OnClick="btnStart_Click" />
                    </div>
                    <table>
                        <tr>
                            <td class="nowrap">
                                维修措施：
                            </td>
                            <td colspan="5">
                                <input type="text" id="txtWX_CS" class="form-control " runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                维修结果：
                            </td>
                            <td>
                                <asp:DropDownList ID="dropResult" runat="server" class="form-control " AutoPostBack="false" 
                                    OnSelectedIndexChanged="dropResult_SelectedIndexChanged">
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
                                <input type="text" id="txtMo_Down_cs" class="form-control"   runat="server" />
                            </td>
                        </tr>
                    </table>
                    <div class="form-group">
                        <div  style="text-align: right">
                            <asp:Button ID="btnEnd" class="btn btn-primary" Style="height: 35px; width: 100px"
                                Text="维修完成" runat="server" OnClick="btnEnd_Click" />
                            <asp:Button ID="btnNext" runat="server" Text="Next" OnClick="btnNext_Click" Style="display: none" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-sm-6 ">
            <div class="panel panel-info">
                <div class="panel-heading">
                    <strong>三.确认维修</strong>
                </div>
                <div class="panel-body">
                    <table width="100%">
                        <tr>
                            <td class="nowrap">
                                确认工号：
                            </td>
                            <td>
                                <asp:DropDownList ID="dropQr_gh" class="form-control input-s-sm" runat="server" AutoPostBack="True" 
                                    Width="160" OnSelectedIndexChanged="dropQr_gh_SelectedIndexChanged" />
                            </td>
                            <td class="nowrap">
                                姓名：
                            </td>
                            <td>
                                <input type="text" id="txtQr_Name" class="form-control "  disabled="disabled" runat="server" />
                            </td>
                        </tr>
                    </table>
                    对维修结果的补充说明：<span style="color: red">(若维修结果仍有问题，请勿确认；请通知维修人员继续维修。)</span>
                    <textarea type="text" id="txtQr_Remark" rows="3" class="form-control"  runat="server"></textarea>
                    <div class="form-group">
                        <div  style="text-align: right;padding-top:15px">
                            <asp:Button ID="btnConfirm" class="btn btn-primary" Style="height: 35px; width: 100px"
                                Text="确认完成" runat="server" OnClick="btnConfirm_Click" OnClientClick="return QRCheck();" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
