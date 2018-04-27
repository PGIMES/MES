<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SetAdd.aspx.cs" Inherits="WebForm.Platform.Home.SetAdd" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <table cellpadding="0" cellspacing="1" border="0" width="95%" class="formtable" style="margin-top:10px;">
        <tr>
            <th style="width: 80px;">模块名称：</th>
            <td><input type="text" class="mytext" validate="empty" id="Name1" name="Name1" runat="server" style="width:300px;" /></td>
        </tr>
        <tr>
            <th style="width: 80px;">显示标题：</th>
            <td><input type="text" class="mytext" validate="empty" id="Title1" name="Title1" runat="server" style="width:300px;" /></td>
        </tr>
        <tr>
            <th style="width: 80px;">类型：</th>
            <td><select id="Type" name="Type" class="myselect" validate="empty">
                <option></option>
                <asp:Literal ID="TypeOptions" runat="server"></asp:Literal>
                </select></td>
        </tr>
        <tr>
            <th style="width: 80px;">数据来源：</th>
            <td><select id="DataSourceType" name="DataSourceType" onchange="dstypecng(this.value);" class="myselect" validate="empty">
                <option></option>
                <asp:Literal ID="DataSourceTypeOptions" runat="server"></asp:Literal>
                </select>
                <span id="DBConnIDSpan" style="display:none;">
                    数据连接：
                    <select class="myselect" id="DBConnID" name="DBConnID">
                        <option></option>
                        <asp:Literal ID="DBConnIDOptions" runat="server"></asp:Literal>
                    </select>
                </span>
            </td>
        </tr>
        <tr>
            <th style="width: 80px;">来源：</th>
            <td><textarea class="mytextarea" validate="empty" style="width:99%;height:160px;" name="DataSource" id="DataSource" runat="server"></textarea></td>
        </tr>
        <tr>
            <th style="width: 80px;">连接地址：</th>
            <td><input type="text" class="mytext" id="LinkURL" name="LinkURL" runat="server" style="width:99%;" /></td>
        </tr>
        <tr>
            <th style="width: 80px;">图标：</th>
            <td><input type="text" class="myico" source="/Images/ico" id="Ico" name="Ico" runat="server" style="width:300px;" /></td>
        </tr>
        <tr>
            <th style="width: 80px;">背景颜色：</th>
            <td><input type="text" class="mytext" id="BgColor" name="BgColor" runat="server" style="width:300px;" /></td>
        </tr>
       
        <tr>
            <th style="width: 80px;">使用对象：</th>
            <td><input type="text" class="mymember" id="UseOrganizes" name="UseOrganizes" runat="server" style="width:90%;" /></td>
        </tr>
        
        <tr>
            <th style="width: 80px;">备注：</th>
            <td><input type="text" class="mytext" id="Note" name="Note" runat="server" style="width:99%;" /></td>
        </tr>
        <tr>
            <th style="width: 80px;">排序：</th>
            <td><input type="text" class="mytext" id="Sort" name="Sort" runat="server" style="" /></td>
        </tr>
    </table>
    <div class="buttondiv">
        <asp:Button ID="Button1" runat="server" CssClass="mybutton" Text=" 保 存 " OnClick="Button1_Click" OnClientClick="return new RoadUI.Validate().validateForm(document.forms[0]);" />
        <input type="button" class="mybutton" value=" 返 回 " onclick="window.location='SetList.aspx<%=Request.Url.Query%>'" />
    </div>
    </form>
    <script type="text/javascript">
        $(function(){
            dstypecng($("#DataSourceType").val());
        });
        function dstypecng(type)
        {
            if("0"==type)
            {
                $("#DBConnIDSpan").show();
            }
            else
            {
                $("#DBConnIDSpan").hide();
            }
        }
    </script>
</body>
</html>
