<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="User.aspx.cs" Inherits="WebForm.Platform.Members.User" %>

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
            <td><input type="text" id="Name" name="Name" class="mytext" runat="server" validate="empty,min,max" max="50" style="width:160px;" /></td>
        </tr>
        <tr>
            <th style="width:80px;">帐号：</th>
            <td><input type="text" id="Account" name="Account" class="mytext" runat="server" validate="empty,max,ajax" max="20" style="width:160px;" /></td>
        </tr>
        <tr>
            <th style="width:80px;">状态：</th>
            <td><asp:Literal runat="server" ID="StatusRadios"></asp:Literal></td>
        </tr>
        <tr>
            <th style="width:80px;">性别：</th>
            <td><asp:Literal runat="server" ID="SexRadios"></asp:Literal></td>
        </tr>
        <tr>
            <th style="width:80px;">备注：</th>
            <td><textarea id="Note" name="Note" class="mytext" style="width:90%; height:50px;" runat="server"></textarea></td>
        </tr>
        <tr>
            <th style="width:80px;">所在组织：</th>
            <td><asp:Literal ID="ParentString" runat="server"></asp:Literal></td>
        </tr>
        <tr>
            <th style="width:80px;">所在角色组：</th>
            <td><asp:Literal ID="RoleString" runat="server"></asp:Literal></td>
        </tr>
        <tr id="StationMove_tr" style="display:none;">
            <th style="width:80px;">调往组织：</th>
            <td>
            <table cellpadding="0" cellspacing="1" border="0"><tr>
                <td><input type="text" style="width:180px;" title="选择要调往的组织：" class="mymember" id="movetostation" name="movetostation" more="false" user="false" station="true" dept="true" unit="true" runat="server"/>
                    <input type="checkbox" name="movetostationjz" id="movetostationjz" style="vertical-align:middle;" value="1" runat="server"/><label for="movetostationjz" style="vertical-align:middle;">兼任</label>
                </td>
                <td><asp:Button ID="Button3" runat="server" Text="确定调动" CssClass="mybutton" OnClientClick="return stationMove1();" OnClick="Button3_Click"/></td>
            </tr></table>
            </td>
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
        <asp:Button ID="Button1" runat="server" Text=" 保 存 " CssClass="mybutton" OnClientClick="return validate.validateForm(document.forms[0]);" OnClick="Button1_Click" />
        <input type="button" class="mybutton" value="设置菜单" onclick="setMenu();" />
        <input type="button" class="mybutton" value="查看已分配菜单" onclick="showMenu();" />
        <input type="button" value=" 调 动 " class="mybutton" onclick="stationMove();" />
        <input type="button" class="mybutton" value=" 排 序 " id="sort" onclick="sort1('@id');" runat="server" />
        <asp:Button ID="Button4" runat="server" Text="初始密码" CssClass="mybutton" OnClientClick="return confirm('您真的要初始化密码吗?');" OnClick="Button4_Click" />
        <asp:Button ID="Button2" runat="server" Text=" 删 除 " CssClass="mybutton" OnClientClick="return confirm('您真的要删除该用户吗?');" OnClick="Button2_Click" />
    </div>
    </form>
    <script type="text/javascript">
        $(function(){
            
        });
        function stationMove()
        {
            $('#StationMove_tr').toggle();
        }

        function stationMove1()
        {
            if ($.trim($("#movetostation").val()).length == 0)
            {
                alert("请选择要调往的组织!");
                return false;
            }
            return true;
        }
        
        function sort1()
        {
            window.location = 'SortUsers.aspx' + '<%=Request.Url.Query%>';
        }

        function setMenu()
        {
            var url = "SetMenu.aspx?prev=<%=("User.aspx"+Request.Url.Query).UrlEncode()%><%=query%>";
            //top.mainDialog.open({url: url,width:900,height:550,title:"设置菜单",opener:parent});
            window.location = url;
            return false;
        }
        function showMenu()
        {
            var url = "ShowMenu.aspx?prev=<%=("User.aspx"+Request.Url.Query).UrlEncode()%><%=query%>";
            //top.mainDialog.open({url: url,width:900,height:550,title:"设置菜单",opener:parent});
            window.location = url;
            return false;
        }
    </script>
</body>
</html>
