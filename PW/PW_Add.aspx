<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="PW_Add.aspx.cs" Inherits="PW_PW_Add" %>

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
    <script src="../Content/js/jquery.min.js" type="text/javascript"></script>
    <script src="../Content/js/plugins/layer/layer.min.js" type="text/javascript"></script>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <script type="text/javascript">
        $(document).ready(function () {

            $("#mestitle").text("【钢丸加料记录】");
            //$("input[id*='btnA']").prop("disabled", true);

//            $("input[id*='txt_bs1']").attr("readonly", "true");
//            $("input[id*='txt_bs2']").attr("readonly", "true");

            $("input[id*='btnA']").click(function () {

                $("input[id*='txt_bs2']").focus();
                var result = $("input[id*='btnA']").val();
                $("input[id*='txtSheBeiJianCheng']").val(result);
            })

            $("input[id*='btnB']").click(function () {
                var result = $("input[id*='btnB']").val();
                $("input[id*='txtSheBeiJianCheng']").val(result);
            })

            $("input[id*='btnC']").click(function () {
                var result = $("input[id*='btnC']").val();
                $("input[id*='txtSheBeiJianCheng']").val(result);
            })

            $("input[id*='btnD']").click(function () {
                var result = $("input[id*='btnD']").val();
                $("input[id*='txtSheBeiJianCheng']").val(result);
            })

            $("input[id*='btnE']").click(function () {
                var result = $("input[id*='btnE']").val();
                $("input[id*='txtSheBeiJianCheng']").val(result);
            })

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
                                    <asp:DropDownList ID="dropGongHao" class="form-control input-s-sm" runat="server" BackColor="Yellow" 
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
                            <tr hidden="hidden">
                                <td>
                                    设备号：
                                 
                                    <asp:TextBox ID="txt" runat="server" Visible="false"  class="form-control input-s-sm"></asp:TextBox>
                                </td>
                                <td>
                                    <input id="txtSheBeiHao" class="form-control input-s-sm" runat="server" />
                                </td>
                                <td>
                                    设备简称：
                                </td>
                                <td>
                                    <input id="txtSheBeiJianCheng" class="form-control input-s-sm" runat="server" />
                                </td>
                                
                            </tr>
                            </table>
                    </div>
                  
                </div>
            </div>
        </div>
    </div>
    <div class="row" style="margin: 0px 2px 1px 2px">
        <div class="col-sm-12">
            <div class="panel panel-info">
                <%--<div class="panel-heading">
                    <strong>投料1</strong>
                </div>--%>
                <div class="panel-body">
                    <div class="row">
                        <div class="col-sm-12">
                            <div class="panel panel-info">
                                <div class="panel-heading">
                                    <strong>第一步：选择设备</strong>
                                </div>
                                <div class="panel-body">
                                    <div class="area_block">
                                        <div class="btn btn-large btn-success">
                                            <div class="btn-group">
                                                <asp:Button ID="btnA" runat="server" Text="新东抛丸机(M1025)" 
                                                    class="btn btn-primary" onclick="btnA_Click"   >
                                                </asp:Button>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="area_block">
                                        <div class="btn btn-large btn-success">
                                            <div class="btn-group">
                                                <asp:Button ID="btnB" runat="server" Text="新东抛丸机(M1026)" class="btn btn-primary" OnClick="B_Click">
                                                </asp:Button>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="area_block">
                                        <div class="btn btn-large btn-success">
                                            <div class="btn-group">
                                                <asp:Button ID="btnC" runat="server" Text="康利抛丸机(M1104)" class="btn btn-primary" OnClick="C_Click">
                                                </asp:Button>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="area_block">
                                        <div class="btn btn-large btn-success">
                                            <div class="btn-group">
                                                <asp:Button ID="btnD" runat="server" Text="康利抛丸机(M1142)" 
                                                    class="btn btn-primary" onclick="btnD_Click"  >
                                                </asp:Button>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="area_block">
                                        <div class="btn btn-large btn-success">
                                            <div class="btn-group">
                                                <asp:Button ID="btnE" runat="server" Text="康利抛丸机(M1252)" 
                                                    class="btn btn-primary" onclick="btnE_Click"  >
                                                </asp:Button>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-12">
                            <div class="panel panel-info">
                                <div class="panel-heading">
                                    <strong>第二步:选择要添加的丸粒及重量</strong>
                                </div>
                                <div class="panel-body">
                                    <table width="30%">
                        <tr>
                            <td>
                             
                                <input id="btn4" type="button" value="0.4mm" class="btn btn-success" />
                            </td>
                            <td>
                                <asp:TextBox ID="txt_bs1" runat="server" class="form-control input-s-sm"></asp:TextBox>
                            </td>
                            <td>包</td>
                            
                        </tr>

                        <tr>
                        <td>
                        <input id="Button1" type="button" value="0.6mm" class="btn btn-success" />
                        </td>
                         <td>
                            <asp:TextBox ID="txt_bs2" runat="server" class="form-control input-s-sm"></asp:TextBox>
                              
                            </td>
                            <td>包</td>
                        </tr>
                       
                    </table>

                    <div class="col-sm-4">
                                                    <div style="text-align: right;padding-bottom:5px; border-bottom:1px solid lightblue; margin-bottom:4px">
                                                       
                                                        <div class="input-group-btn">
                                                            <asp:Button ID="Button2" class="btn btn btn-primary" 
                                                                Text="确认" runat="server" Width="100px" onclick="Button2_Click"
                                                                />
                                                        </div>
                                                    </div>
                                                 
                                                </div>
                                </div>

                                 
                            
                      <%--          <div class="row">
                                    <div class="col-sm-12 disabled">
                                        <div class="panel panel-info">
                                            <div class="panel-heading">
                                                <strong>第三步：确认</strong>
                                            </div>
                                            <div class="panel-body">
                                                <div class="col-sm-3">
                                                    <div class="input-group">
                                                       
                                                        <div class="input-group-btn">
                                                            <asp:Button ID="btn_confirm" class="btn btn btn-primary" Text="确认" runat="server" Width="100px"
                                                                />
                                                        </div>
                                                    </div>
                                                    <asp:Label ID="lblMaoZhong_confirm_time" runat="server" Text=""></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                 
                                </div>--%>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

