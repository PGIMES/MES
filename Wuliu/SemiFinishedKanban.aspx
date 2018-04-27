<%@ Page Title="物流【半成品数据看板】" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    ViewStateMode="Enabled" CodeFile="SemiFinishedKanban.aspx.cs" Inherits="Wuliu_SemiFinishedKanban" %>

<%@ Register Assembly="DevExpress.Web.v17.2, Version=17.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.XtraCharts.v17.2.Web, Version=17.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.XtraCharts.Web" TagPrefix="dx" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <script src="../Content/js/jquery.min.js" type="text/javascript"></script>
    <script src="../Content/js/bootstrap.min.js"></script>
    <script src="../Content/js/plugins/layer/layer.min.js" type="text/javascript"></script>

    <script type="text/javascript"  language="javascript">
        $(document).ready(function () {
            $("input[id*='txt_planqty']").change(function () {
                var str = $("input[id*='txt_planqty']").val();
                if (str.indexOf('.')>-1) {
                    layer.alert("夹具排产数量必须为整数!");
                    return false;
                }
                if($("input[id*='txt_fixtureqty']").val()<$("input[id*='txt_planqty']").val()){
                    layer.alert("夹具排产数量必须小于等于夹具数量!");
                }
            });

        });
        $("#mestitle").text("【半成品数据看板】");
    </script>
    <div class="row row-container">
        <div class="col-sm-12 ">
            <div class="panel panel-info">
                 <div class="panel-heading" data-toggle="collapse" data-target="#CKBZ">
                    <strong>半成品排产夹具</strong>
                      <span class="caret"></span>
                </div>
                <div class="panel-body collapse" id="CKBZ">
                    <div class="col-xs-12 col-sm-12  col-md-12 col-lg-12">
                        <div class="area_block">

                        <table>
                            <tr>
                                <td>公司别:
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddl_site" runat="server" class="form-control input-s-sm ">
                                        <asp:ListItem>200</asp:ListItem>
                                        <asp:ListItem>100</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td>物料号:
                                </td>
                                <td>
                                    <asp:TextBox ID="txt_partno" class="form-control input-s-sm" runat="server" AutoPostBack="True" OnTextChanged="txt_partno_TextChanged"></asp:TextBox>
                                </td>
                                <td>名称:
                                </td>
                                <td>
                                    <asp:TextBox ID="txt_partname" class="form-control input-s-sm" runat="server"></asp:TextBox>
                                </td>
                                <td>各工位夹具数量:
                                </td>
                                <td>
                                    <asp:TextBox ID="txt_fixtureqty" class="form-control input-s-sm" runat="server"></asp:TextBox>
                                </td>
                                 <td>工位数:
                                </td>
                                <td>
                                    <asp:TextBox ID="txt_gw_qty" class="form-control input-s-sm" runat="server"></asp:TextBox>
                                </td>
                                <td>排产夹具数量:
                                </td>
                                <td>
                                    <asp:TextBox ID="txt_planqty" class="form-control input-s-sm" BackColor="Yellow" runat="server"></asp:TextBox>
                                </td>
                                <td><font size="3" color="red">**不可超过【夹具数量】</font></td>
                            </tr>
                            <tr>
                                <td colspan="11" style="text-align: center">
                                    <asp:Button ID="btn_confirm" runat="server" Text="确认维护"
                                        class="btn btn-large btn-primary"
                                        Width="100px" OnClick="btn_confirm_Click" />

                                </td>
                            </tr>
                        </table>

                    </div>

                </div>
            </div>
           </div>
        </div>
    </div>
    <div class="row row-container">
        <div class="col-sm-12 ">
            <div class="panel panel-info">
                <div class="panel-heading">
                    <strong>半成品数据查询</strong>
                </div>
                <div class="panel-body">
                    <div class="col-sm-12">
                        <table>
                            <tr>
                                <td>公司别:
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlSite2" runat="server" class="form-control input-s-sm ">
                                        <asp:ListItem>ALL</asp:ListItem>
                                        <asp:ListItem>200</asp:ListItem>
                                        <asp:ListItem>100</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td>物料号:
                                </td>
                                <td>
                                    <asp:TextBox ID="txtPartno2" class="form-control input-s-sm" runat="server"></asp:TextBox>
                                </td>
                                <td>名称:
                                </td>
                                <td>
                                    <asp:TextBox ID="txtPartname2" class="form-control input-s-sm" runat="server"></asp:TextBox>
                                </td>
                                <td>状态:
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddl_status" runat="server" class="form-control input-s-sm ">
                                        <asp:ListItem>ALL</asp:ListItem>
                                        <asp:ListItem>生产中</asp:ListItem>
                                        <asp:ListItem>未排产</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="10" align="center">
                                    <asp:Button ID="Button1" runat="server" Text="查询"
                                        class="btn btn-large btn-primary"
                                        Width="100px" OnClick="Button1_Click" /> &nbsp;&nbsp;
                                     <asp:Button ID="Button2" runat="server" Text="导出Excel"
                                        class="btn btn-large btn-primary"
                                        Width="100px" OnClick="Button2_Click"  />

                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <dx:ASPxGridView ID="ASPxGridView1" runat="server" OnHtmlDataCellPrepared="ASPxGridView1_HtmlDataCellPrepared" OnPageIndexChanged="ASPxGridView1_PageIndexChanged">
        <SettingsPager PageSize="100">
        </SettingsPager>
<Styles AdaptiveDetailButtonWidth="22"></Styles>
    </dx:ASPxGridView>

   



    <br />
    <div runat="server" id="DIV1" style="margin: 20px">


        <asp:Panel ID="Panel2" runat="server" Height="100%">
            <table style="background-color: #FFFFFF;">

                <tr>
                    <td  valign="top">&nbsp;</td>
                </tr>
            </table>
        </asp:Panel>
    </div>
</asp:Content>
