<%@ Page Language="C#" AutoEventWireup="true" CodeFile="YST_Maintain_Modify.aspx.cs" Inherits="YaSheTou_YST_Maintain_Modify" %>

<%@ Register Assembly="DevExpress.Web.v17.2, Version=17.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>【压射头维护】</title>
    <link href="/Content/css/bootstrap.min.css" rel="stylesheet" />
    <script src="/Content/js/jquery.min.js"></script>
    <script src="/Content/js/bootstrap.min.js"></script>
    <script src="/Content/js/layer/layer.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
           
        });
        function validate() {
            if (zj.GetValue() == "" || zj.GetValue() == null) {
                layer.alert("【压射头直径】不可为空");
                return false;
            }
            if (!ASPxClientEdit.ValidateGroup("ValueValidationGroup")) {
                layer.alert("【压射头直径】格式必须正确");
                return false;
            }
            if (mc.GetValue() == "" || mc.GetValue() == null) {
                layer.alert("【额定模次】不可为空");
                return false;
            }
            if ($("#ddl_gys").val() == "") {
                layer.alert("【供应商】不可为空");
                return false;
            }
            return true;
        }

    </script>
     <style>
        .lineread{
            /*font-size:9px;*/ border:none; border-bottom:1px solid #ccc;
        }
        .linewrite{
            /*font-size:9px;*/ border:none; border-bottom:1px solid #ccc;background-color:#FDF7D9;/*EFEFEF*/
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    
    <div class="col-md-12" >

        <div class="row  row-container">
            <div class="col-xs-12 col-sm-12  col-md-12 col-lg-12" >
                <div>
                    <table style="width:100%; font-size:12px; line-height:35px;" border="0" id="tblWLLeibie">
                        <tr>
                            <td>压射头直径</td>
                            <td>
                                <span style="float:left;">
                                    <dx:ASPxTextBox ID="lbl_zj" Width="15px" Height="27px"  runat="server" ClientInstanceName="lbl_zj" CssClass="lineread" Text="D">
                                    </dx:ASPxTextBox>
                                </span>
                                <span style="float:left;">
                                    <dx:ASPxTextBox ID="txt_zj" Width="140px" Height="27px"  runat="server" ClientInstanceName="zj" CssClass="linewrite" BackColor="#FDF7D9">
                                        <ValidationSettings ValidationGroup="ValueValidationGroup" Display="Dynamic" ErrorTextPosition="Bottom">
                                            <RegularExpression ErrorText="请输入正整数！" ValidationExpression="^[1-9]+[0-9]*$" />
                                        </ValidationSettings>
                                    </dx:ASPxTextBox>
                                </span>
                            </td>
                        </tr>
                        <tr>
                            <td>额定模次</td>
                            <td>
                                <dx:ASPxTextBox ID="txt_mc" Width="150px" Height="27px"  runat="server" ClientInstanceName="mc" CssClass="linewrite" BackColor="#FDF7D9">
                                    <ValidationSettings ValidationGroup="ValueValidationGroup" Display="Dynamic" ErrorTextPosition="Bottom">
                                        <RegularExpression ErrorText="请输入正整数！" ValidationExpression="^[1-9]+[0-9]*$" />
                                    </ValidationSettings>
                                </dx:ASPxTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>供应商</td>
                            <td>
                                <asp:DropDownList ID="ddl_gys" runat="server" class="form-control input-s-sm " CssClass="linewrite" Height="27px" Width="150px">
                                    <asp:ListItem></asp:ListItem>
                                    <asp:ListItem Value="A" Text="铸泰"></asp:ListItem>
                                    <asp:ListItem Value="B" Text="宜龙"></asp:ListItem>
                                </asp:DropDownList>
                            </td> 
                        </tr>
                        <tr>
                            <td colspan="2" style="text-align:center;">
                                <asp:Button ID="btn_save" runat="server" Text="保存" class="btn btn-large btn-primary" OnClientClick="if(validate()==false)return false;" Width="50px"
                                    OnClick="btn_save_Click" /> 
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>



        

    </div>
    </form>
</body>
</html>
