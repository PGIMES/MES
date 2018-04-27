<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserAdd.aspx.cs" Inherits="WebForm.Platform.Members.UserAdd" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <script type="text/javascript">
        var win = new RoadUI.Window();
        var validate = new RoadUI.Validate();
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <br />
    <table cellpadding="0" cellspacing="1" border="0" width="95%" class="formtable">
        <tr>
            <th style="width:80px;">姓名：</th>
            <td><input type="text" id="Name" name="Name" class="mytext" runat="server" onchange="getPy(this.value);" validate="empty,min,max" max="50" style="width:160px;" /></td>
        </tr>
        <tr>
            <th style="width:80px;">帐号：</th>
            <td><input type="text" id="Account" name="Account" class="mytext" runat="server" validate="empty,max,ajax" max="20" style="width:160px;" /></td>
        </tr>
        <tr>
            <th style="width:80px;">状态：</th>
            <td><asp:Literal ID="StatusRadios" runat="server"></asp:Literal></td>
        </tr>
        <tr>
            <th style="width:80px;">性别：</th>
            <td><asp:Literal runat="server" ID="SexRadios"></asp:Literal></td>
        </tr>
        <tr>
            <th style="width:80px;">备注：</th>
            <td><textarea id="Note" name="Note" class="mytext" style="width:90%; height:50px;" runat="server"></textarea></td>
        </tr>
        
    </table>
    <div style="width:95%; margin:8px auto;">
        联系信息(<label style="color:red; font-weight:bold;">与人员微信关联的微信号、手机、邮箱三者之一必须填一个，否则无法使用微信功能</label>)：
    </div>
    <table cellpadding="0" cellspacing="1" border="0" width="95%" class="formtable">
        <tr>
            <th style="width:120px;">办公电话：</th>
            <td><input type="text" id="Tel" name="Tel" class="mytext" runat="server" style="width:70%;"/></td>
            <th style="width:120px;">手机：</th>
            <td><input type="text" id="Mobile" name="Mobile" class="mytext" runat="server" style="width:70%;"/></td>
        </tr>
        <tr>
            <th>微信号：</th>
            <td><input type="text" id="WeiXin" name="WeiXin" class="mytext" runat="server" style="width:70%;"/></td>
            <th>邮箱：</th>
            <td><input type="text" id="Email" name="Email" class="mytext" validate="canempty,email" runat="server" style="width:70%;"/></td>
        </tr>
        <tr>
            <th>传真：</th>
            <td><input type="text" id="Fax" name="Fax" class="mytext" runat="server" style="width:70%;"/></td>
            <th>QQ：</th>
            <td><input type="text" id="QQ" name="QQ" class="mytext" runat="server" style="width:70%;"/></td>
        </tr>
        <tr>
            <th>其它联系方式：</th>
            <td colspan="3"><input type="text" id="OtherTel" name="OtherTel" class="mytext" runat="server" style="width:95%;"/></td>
        </tr>
    </table>
    <div style="width:95%; margin:10px auto 10px auto; text-align:center;">
        <asp:Button ID="Button1" runat="server" Text="保存" CssClass="mybutton" OnClientClick="return validate.validateForm(document.forms[0]);" OnClick="Button1_Click" />
        <input type="button" class="mybutton" onclick="window.location='Body.aspx'+'<%=Request.Url.Query%>';" value="返回" />
    </div>
    </form>
    <script type="text/javascript">
        $(function(){
           
        });
        function getPy(v)
        {
            $.ajax({ url: 'GetPy.ashx', data: { 'name': v }, dataType: 'text', type: 'post', cache: false, success: function (txt)
            {
                $('#Account').val(txt);
            }
            });
        }
    </script>
</body>
</html>
