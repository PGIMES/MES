<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PR_syr_maintain.aspx.cs" Inherits="Forms_PurChase_PR_syr_maintain" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
     <title>【修改合同计划】</title>
    <link href="/Content/css/bootstrap.min.css" rel="stylesheet" />
    <script src="/Content/js/jquery.min.js"></script>
    <script src="/Content/js/bootstrap.min.js"></script>
    <script src="/Content/js/layer/layer.js"></script>
    <script type="text/javascript">

        $(document).ready(function () {
            grid_read();
        });
        function validate() {
            if ($("#txt_PRNo").val()=="") {
                layer.alert("【请购单号】不可为空");
                return false;
            }
            if ($("#txt_rowid").val() == "") {
                layer.alert("【请购行号】不可为空");
                return false;
            }
            if ($("#txt_wlmc").val() == "") {
                layer.alert("【物料名称】不可为空");
                return false;
            }
            return true;
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
         .hidden { display:none;}
        .hidden1 {
            border: 0px;
            overflow: hidden;
        }
    </style>
    <style>
        .lineread{
            /*font-size:9px;*/ border:none; border-bottom:1px solid #ccc;
        }
        .linewrite{
            /*font-size:9px;*/ border:none; border-bottom:1px solid #ccc;background-color:#FDF7D9;/*EFEFEF*/
        }
        .dxeTextBox_read{
            border:none !important ; background-color:transparent;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div>
        <div class="col-xs-12 col-sm-12  col-md-12 col-lg-12">
            <div>
                <table style="width:100%; font-size:12px; line-height:35px;" border="0" id="tblWLLeibie">
                    <tr>
                        <td>请购单号</td>
                        <td><asp:TextBox ID="txt_PRNo" runat="server"  CssClass="linewrite" Width="100px" Height="27px" 
                            OnTextChanged="txt_PRNo_TextChanged" AutoPostBack="true"></asp:TextBox></td>
                        <td>请购行号</td>
                        <td><asp:TextBox ID="txt_rowid" runat="server" CssClass="linewrite" Width="100px" Height="27px" 
                            OnTextChanged="txt_PRNo_TextChanged" AutoPostBack="true"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>请购类别</td>
                        <td><asp:TextBox ID="txt_PRType" runat="server" ReadOnly="true" CssClass="lineread" Width="100px" Height="27px"></asp:TextBox></td>                    
                        <td>物料号</td>
                        <td><asp:TextBox ID="txt_wlh" runat="server" ReadOnly="true" CssClass="lineread" Width="100px" Height="27px"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>物料名称</td>
                        <td colspan="3"><asp:TextBox ID="txt_wlmc" runat="server" ReadOnly="true" CssClass="lineread" Width="350px" Height="27px"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>物料描述</td>
                        <td colspan="3"><asp:TextBox ID="txt_wlms" runat="server" ReadOnly="true" CssClass="lineread" Width="350px" Height="27px"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>使用人</td>
                        <td colspan="3"><asp:TextBox ID="txt_syr" runat="server"  CssClass="linewrite" Width="350px" Height="27px"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td colspan="4" style="text-align:center;">
                             <asp:Button ID="Button1" runat="server" Text="保存" class="btn btn-large btn-primary" OnClientClick="if(validate()==false)return false;" Width="50px"
                                    OnClick="btn_save_Click" /> 
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
