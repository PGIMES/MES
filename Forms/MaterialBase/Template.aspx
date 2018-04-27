<%@ Page Title="MES生产管理系统" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="Template.aspx.cs" Inherits="Template" MaintainScrollPositionOnPostback="True" ValidateRequest="true" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <link href="../Content/css/bootstrap.min.css" rel="stylesheet" />
    <script src="../Content/js/jquery.min.js"></script>
    <script src="../Content/js/bootstrap.min.js"></script>
    <script src="../Content/js/layer/layer.js"></script>
    <script src="../Content/js/plugins/layer/laydate/laydate.js"></script>
    <link href="../Content/js/plugins/bootstrap-select/css/bootstrap-select.min.css" rel="stylesheet" />
    <script src="../Content/js/plugins/bootstrap-select/js/bootstrap-select.min.js"></script>

    <script type="text/javascript">
        $(document).ready(function () {
            $("#mestitle").html("【刀具申请-01钻头】<a href='/userguide/reviewGuide.pptx' target='_blank' class='h4' style='display:none'>使用说明</a>");


        })
    </script>
    <script type="text/javascript">
        var popupwindow = null;
        function GetXMH() {
            popupwindow = window.open('../Select/select_XMLJ.aspx', '_blank', 'height=500,width=800,resizable=no,menubar=no,scrollbars =no,location=no');
        }
        function GetEmp() {
            popupwindow = window.open('../Select/select_Emp.aspx?ctrl1=txtProbEmp', '_blank', 'height=500,width=800,resizable=no,menubar=no,scrollbars =no,location=no');
        }
        function setvalue(ctrl0, keyValue0, ctrl1, keyValue1, ctrl2, keyValue2) {

            $("input[id*='" + ctrl0 + "']").val(keyValue0);
            $("input[id*='" + ctrl1 + "']").val(keyValue1);
            $("input[id*='" + ctrl2 + "']").val(keyValue2);
            popupwindow.close();
            $("input[id*='" + ctrl0 + "']").change();
        }

    </script>

    <style type="text/css">
        .row {
            margin-right: 2px;
            margin-left: 2px;
        }

        .textalign {
            text-align: right;
        }

        .alignRight {
            padding-right: 4px;
            text-align: right;
        }

        .row-container {
            padding-left: 2px;
            padding-right: 2px;
        }

        fieldset {
            background: rgba(255,255,255,.3);
            border-color: lightblue;
            border-style: solid;
            border-width: 2px;
            -moz-border-radius: 5px;
            -webkit-border-radius: 5px;
            -khtml-border-radius: 5px;
            border-radius: 5px;
            /*line-height: 30px;*/
            list-style: none;
            padding: 5px 10px;
            margin-bottom: 2px;
        }

        legend {
            color: #302A2A;
            font: bold 16px/2 Verdana, Geneva, sans-serif;
            font-weight: bold;
            text-align: left;
            /*text-shadow: 2px 2px 2px rgb(88, 126, 156);*/
        }

        fieldset {
            padding: .35em .625em .75em;
            margin: 0 2px;
            border: 1px solid lightblue;
        }

        legend {
            padding: 5px;
            border: 0;
            width: auto;
            margin-bottom: 2px;
        }

        .panel {
            margin-bottom: 5px;
        }

        .panel-heading {
            padding: 5px 5px 5px 5px;
        }

        .panel-body {
            padding: 5px 5px 5px 5px;
        }

        body {
            margin-left: 5px;
            margin-right: 5px;
        }

        .col-lg-1, .col-lg-10, .col-lg-11, .col-lg-12, .col-lg-2, .col-lg-3, .col-lg-4, .col-lg-5, .col-lg-6, .col-lg-7, .col-lg-8, .col-lg-9, .col-md-1, .col-md-10, .col-md-11, .col-md-12, .col-md-2, .col-md-3, .col-md-4, .col-md-5, .col-md-6, .col-md-7, .col-md-8, .col-md-9, .col-sm-1, .col-sm-10, .col-sm-11, .col-sm-12, .col-sm-2, .col-sm-3, .col-sm-4, .col-sm-5, .col-sm-6, .col-sm-7, .col-sm-8, .col-sm-9, .col-xs-1, .col-xs-10, .col-xs-11, .col-xs-12, .col-xs-2, .col-xs-3, .col-xs-4, .col-xs-5, .col-xs-6, .col-xs-7, .col-xs-8, .col-xs-9 {
            position: relative;
            min-height: 1px;
            padding-right: 1px;
            padding-left: 1px;
            margin-top: 0px;
            top: 0px;
            left: 0px;
        }

        td {
            /* vertical-align: top;
            font-weight: 600;*/
            font-size: 12px;
            padding-bottom: 5px;
            padding-left: 3px;
            white-space: nowrap;
        }

        p.MsoListParagraph {
            margin-bottom: .0001pt;
            text-align: justify;
            text-justify: inter-ideograph;
            text-indent: 21.0pt;
            font-size: 10.5pt;
            font-family: "Calibri","sans-serif";
            margin-left: 0cm;
            margin-right: 0cm;
            margin-top: 0cm;
        }

        .his {
            padding-left: 8px;
            padding-right: 8px;
        }

        .tbl td {
            border: 1px solid black;
            padding-left: 3px;
            padding-right: 3px;
            padding-top: 3px;
        }

        .wrap {
            word-break: break-all;
            white-space: normal;
        }
    </style>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div class="col-md-10">
        <div class="row row-container">
            <div class="col-md-12">
                <div class="panel panel-info">
                    <div class="panel-heading" data-toggle="collapse" data-target="#SQXX">
                        <strong>申请人信息</strong>
                    </div>
                    <div class="panel-body <% =ViewState["lv"].ToString() == "SQXX" ? "" : "collapse" %>" id="SQXX">
                        <div class="col-xs-12 col-sm-12  col-md-12 col-lg-12">
                            <div class="">
                                <asp:UpdatePanel ID="UpdatePanel_request" runat="server">
                                    <ContentTemplate>
                                        <table style="height: 35px; width: 100%">
                                            <tr>
                                                <td>申请人：
                                                </td>
                                                <td>
                                                    <div class="form-inline">
                                                        <input id="txt_CreateById" class="form-control input-s-sm" style="height: 35px; width: 100px" runat="server" readonly="True" />
                                                        <input id="txt_CreateByName" class="form-control input-s-sm" style="height: 35px; width: 100px" runat="server" readonly="True" />
                                                        <input id="txt_CreateByAd" class="form-control input-s-sm" style="height: 35px; width: 100px; display: none" runat="server" readonly="True" />
                                                    </div>
                                                </td>
                                                <td>部门：
                                                </td>
                                                <td>
                                                    <input id="txt_CreateByDept" class="form-control input-s-sm" style="height: 35px; width: 100px" runat="server" readonly="True" />
                                                </td>
                                                <td style="display: none">部门经理：
                                                </td>
                                                <td>
                                                    <div class="form-inline" style="display: none">
                                                        <input id="txt_managerid" class="form-control input-s-sm" style="height: 35px; width: 100px" runat="server" readonly="True" />
                                                        <input id="txt_manager" class="form-control input-s-sm" style="height: 35px; width: 100px" runat="server" readonly="True" />
                                                        <input id="txt_manager_AD" class="form-control input-s-sm" style="height: 35px; width: 100px" runat="server" readonly="True" />
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>当前登陆人员</td>
                                                <td>
                                                    <div class="form-inline">
                                                        <input id="txt_LogUserId" class="form-control input-s-sm" style="height: 35px; width: 100px" runat="server" readonly="True" />
                                                        <input id="txt_LogUserName" class="form-control input-s-sm" style="height: 35px; width: 100px" runat="server" readonly="True" />/
                                                        <input id="txt_LogUserJob" class="form-control input-s-sm" style="height: 35px; width: 100px" runat="server" readonly="True" />
                                                        <input id="txt_LogUserDept" class="form-control input-s-sm" style="height: 35px; width: 100px" runat="server" readonly="True" />
                                                    </div>
                                                </td>
                                                <td>申请日期：</td>
                                                <td>
                                                    <input id="txt_CreateDate" class="form-control input-s-sm" style="height: 35px; width: 100px" runat="server" readonly="True" />
                                                </td>
                                                <td></td>
                                                <td>
                                                    <input id="txt_Code" class="form-control input-s-sm" style="height: 35px; width: 200px; display: none" runat="server" readonly="True" />
                                                </td>
                                            </tr>
                                        </table>

                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="row  row-container">
            <div class="col-md-12">
                <div class="panel panel-info">
                    <div class="panel-heading" data-toggle="collapse" data-target="#CPXX">
                        <strong>物料类别</strong>
                    </div>
                    <div class="panel-body " id="XSGCS">
                        <div class="col-xs-12 col-sm-12  col-md-12 col-lg-12">
                            <div>
                                <asp:Table Style="width: 100%" border="0" runat="server" ID="tblWLLeibie">
                                </asp:Table>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="row  row-container">
            <div class="col-md-12">
                <div class="panel panel-info">
                    <div class="panel-heading" data-toggle="collapse" data-target="#gscs">
                        <strong style="padding-right: 100px">物料属性</strong>
                    </div>
                    <div class="panel-body  collapse in" id="gscs">
                        <div class="col-xs-12 col-sm-12  col-md-12 col-lg-12">
                            <div>
                                <asp:Table Style="width: 100%" border="0" runat="server" ID="tblWLShuXing">
                                </asp:Table>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="row  row-container">
            <div class="col-md-12">
                <div class="panel panel-info">
                    <div class="panel-heading" data-toggle="collapse" data-target="#gsjg">
                        <strong style="padding-right: 100px">物料数据/计划数据</strong>
                    </div>
                    <div class="panel-body   collapse in" id="gsjg">
                        <div class="col-xs-12 col-sm-12  col-md-12 col-lg-12">

                            <div>
                                <asp:Table Style="width: 100%" border="0" runat="server" ID="tblWLShuJu">
                                </asp:Table>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="row  row-container">
            <div class="col-md-12">
                <div class="panel panel-info">
                    <div class="panel-heading" data-toggle="collapse" data-target="#jgqr">
                        <strong>物料价格数据</strong>
                    </div>
                    <div class="panel-body  collapse in" id="jgqr">
                        <div class="col-xs-12 col-sm-12  col-md-12 col-lg-12">
                            <div>
                                <asp:Table Style="width: 100%" border="0" runat="server" ID="tblWLJiaGe">
                                </asp:Table>
                            </div>
                            <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <script type="text/javascript">
                                        //// function load() {
                                        // Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
                                        // //}

                                        // function EndRequestHandler() {
                                        //     AddSolution();
                                        // }
                                    </script>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div class="row  row-container" style="display: none">
            <div class="col-md-12">
                <div class="panel panel-info">
                    <div class="panel-heading" data-toggle="collapse" data-target="#CZRZ">
                        <strong>操作日志  <span class="caret"></span></strong>
                    </div>
                    <div class="panel-body collapse" id="CZRZ">
                        <div class="col-xs-12 col-sm-12  col-md-12 col-lg-12">
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div class="col-md-2 ">
        <div class="panel panel-info">
            <div class="panel-heading">
                <strong>日志:</strong>
            </div>
            <div class="panel-body  " id="DDXX">
                <div>
                    <div class="">
                        <asp:GridView ID="gv_rz2" Width="100%"
                            AllowMultiColumnSorting="True" AllowPaging="True"
                            AllowSorting="True" AutoGenerateColumns="False"
                            runat="server" Font-Size="Small">
                            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <PagerSettings FirstPageText="首页" LastPageText="尾页"
                                Mode="NextPreviousFirstLast" NextPageText="下页" PreviousPageText="上页" />
                            <PagerStyle ForeColor="Red" />
                            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                            <EditRowStyle BackColor="#999999" />
                            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />

                            <Columns>
                                <asp:BoundField DataField="Update_username" HeaderText="姓名">
                                    <HeaderStyle Wrap="False" />
                                    <ItemStyle Width="10%" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Update_content" HeaderText="操作事项">
                                    <HeaderStyle Wrap="False" />
                                    <ItemStyle Width="10%" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Commit_time" HeaderText="操作时间" DataFormatString="{0:yy/MM/dd}">
                                    <HeaderStyle Wrap="False" />
                                    <ItemStyle Width="30%" />
                                </asp:BoundField>
                            </Columns>
                        </asp:GridView>


                        <br />
                    </div>
                </div>
            </div>
        </div>
    </div>



</asp:Content>
