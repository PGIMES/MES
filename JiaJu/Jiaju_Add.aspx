<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Jiaju_Add.aspx.cs" Inherits="JiaJu_Jiaju_Add" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="../Content/js/jquery.min.js" type="text/javascript"></script>
    <script src="../Content/js/plugins/layer/layer.min.js" type="text/javascript"></script>
    <link href="../Content/css/bootstrap.min.css" rel="stylesheet" />
    <script src="../Content/js/jquery.min.js"></script>
    <script src="../Content/js/bootstrap.min.js"></script>
    <script src="../Content/js/layer/layer.js"></script>
    <script src="../Content/js/plugins/layer/laydate/laydate.js"></script>
    <link href="../Content/js/plugins/bootstrap-select/css/bootstrap-select.min.css" rel="stylesheet" />
    <script src="../Content/js/plugins/bootstrap-select/js/bootstrap-select.min.js"></script>
    <link href="../Content/css/font-awesome.min.css" rel="stylesheet" type="text/css" />


        <script type="text/javascript">
            $(document).ready(function () {
                //保存
                $("#btn_save").click(function () {
                    if ($("select[id*='Drop_type']").val() == "") {
                      parent.layer.msg("请选择【夹具类型】！");
                        return false;
                    }
                    if ($("input[id*='txtRiQi']").val() == "") {
                        parent.layer.msg("请输入【夹具号】！");
                        return false;
                    }
                    if ($("input[id*='txt_pn']").val() == "") {
                        parent.layer.msg("请输入【零件号】！");
                        return false;
                    }
                    if ($("input[id*='txt_pnname']").val() == "") {
                        parent.layer.msg("请输入【零件名称】！");
                        return false;
                    }
                    if ($("select[id*='Drop_zc']").val() == "") {
                        parent.layer.msg("请选择【资产所属】！");
                        return false;
                    }
                    if ($("select[id*='Drop_status']").val() == "") {
                        parent.layer.msg("请选择【状态】！");
                        return false;
                    }
                    if ($("input[id*='txt_loc']").val() == "") {
                        parent.layer.msg("请输入【库位】！");
                        return false;
                    }
                })

            })

            function closewin() {
                var index = parent.layer.getFrameIndex(window.name); //获取窗口索引                       
                parent.layer.close(index);
            }
        </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div class="col-sm-12  col-md-12">
            <div class="panel panel-info">
                <div class="panel-heading">
                    <strong>夹具新增</strong>
                </div>
                <div class="panel-body">
                    <div class="col-sm-12 col-md-12">
                        <table style="width: 900px">
                            <tr>
                                <td>
                                    夹具类型：
                                </td>
                                <td>
                                    <asp:DropDownList ID="Drop_type" class="form-control input-s-sm"
                                        runat="server" Width="150px">
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    夹具号：
                                </td>
                                <td>
                                    <input id="txt_jiajuno" class="form-control input-s-sm" runat="server"
                                        style="width: 150px" />
                                </td>
                                <td>
                                    零件号：
                                </td>
                                <td>
                                    <input id="txt_pn" class="form-control input-s-sm" runat="server"
                                        style="width: 150px" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    零件名称：
                                </td>
                                <td>
                                    <input id="txt_pnname" class="form-control input-s-sm" runat="server"
                                        style="width: 150px" />
                                </td>
                                <td>
                                    客户：
                                </td>
                                <td>
                                    <input id="txt_Customer" class="form-control input-s-sm" runat="server"
                                        style="width: 150px" />
                                </td>
                                <td>
                                    资产所属：
                                </td>
                                <td>
                                    <asp:DropDownList ID="Drop_zc" class="form-control input-s-sm"
                                        runat="server" Width="150px">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    状态：
                                </td>
                                <td>
                                    <asp:DropDownList ID="Drop_status" class="form-control input-s-sm"
                                        runat="server" Width="150px">
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    库位：
                                </td>
                                <td>
                                    <input id="txt_loc" class="form-control input-s-sm" runat="server"
                                        style="width: 150px" />
                                </td>
                                <td>
                                    创建日期：
                                </td>
                                <td>
                                    <input id="txt_date" class="form-control input-s-sm " runat="server"
                                        style="width: 150px" readonly="readonly" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                </td>
                                <td>
                                    &nbsp; &nbsp; &nbsp;
                                </td>
                                <td>
                                    <button id="btn_save" runat="server" class="btn btn-primary btn-large"
                                        type="button" onserverclick="btn_save_click">
                                        <i class="fa fa-save fa-fw"></i>&nbsp;保存</button>
                                </td>
                                  <td>  <button id="btnBack" runat="server" class="btn btn-primary btn-large"
                                        type="button"  onclick="closewin();" >
                                        <i class="fa fa-window-close fa-fw"></i>&nbsp;关闭</button>
                                  </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
        </div>
    
    </div>
    </form>
</body>
</html>
