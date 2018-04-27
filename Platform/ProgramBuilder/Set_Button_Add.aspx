<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Set_Button_Add.aspx.cs" Inherits="WebForm.Platform.ProgramBuilder.Set_Button_Add" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <% 
        var AppButton = new RoadFlow.Platform.AppLibraryButtons();
        string buttonJson = AppButton.GetAllJson();    
    %>
    <script type="text/javascript">
        var buttonJson=<%=buttonJson%>;
    </script>
    <table cellpadding="0" cellspacing="1" border="0" width="99%" class="formtable" style="margin-top:10px;">
        <tr>
            <th>按钮库：</th>
            <td><select class="myselect" id="buttonid" name="buttonid" onchange="buttonchange(this)">
                <%=AppButton.GetOptions(pbb==null?"":pbb.ButtonID.ToString()) %>
                </select></td>
        </tr>
        <tr>
            <th style="width: 80px;">按钮名称：</th>
            <td><input type="text" class="mytext" id="buttonname" name="buttonname" value="" runat="server" validate="empty" style="width:80%" /></td>
        </tr>
        <tr>
            <th style="width: 80px;">执行脚本：</th>
            <td><textarea class="mytextarea" style="width:99%;height:150px; font-family:Verdana; font-size:14px;" name="ClientScript" id="ClientScript" runat="server"></textarea></td>
        </tr>
        <tr>
            <th>图标：</th>
            <td><input type="text" name="Ico" id="Ico" class="myico" runat="server" source="/Images/ico" value="" style="width:350px"/></td>
        </tr>
        <tr>
            <th>显示类型：</th>
            <td><select class="myselect" name="showtype" id="showtype" ><%=AppButton.GetShowTypeOptions(pbb==null?"":pbb.ShowType.ToString()) %></select></td>
        </tr>
        <tr>
            <th style="width: 80px;">权限控制：</th>
            <td><%=new RoadFlow.Platform.Dictionary().GetRadiosByCode("yesno","IsValidateShow",value:(pbb==null? "1":pbb.IsValidateShow.ToString())) %> //如果不控制则不需要授权,每个人都能操作此按钮</td>
        </tr>
        <tr>
            <th style="width: 80px;">显示顺序：</th>
            <td><input type="text" class="mytext" id="Sort" name="Sort" runat="server" value="@Model.Sort"/></td>
        </tr>
    </table>

    <div class="buttondiv">
        <asp:Button ID="Button1" runat="server" Text=" 保 存 " CssClass="mybutton" OnClientClick="return new RoadUI.Validate().validateForm(document.forms[0]);" OnClick="Button1_Click"/>
        <input type="button" class="mybutton" onclick="window.location = 'Set_Button.aspx<%=Request.Url.Query%>';" value=" 返 回 " />
    </div>
    </div>
    </form>
    <script type="text/javascript">
        $(function(){
           
        });
        function buttonchange(sel)
        {
            for(var j=0;j<buttonJson.length;j++)
            {
                if(buttonJson[j].id==$(sel).val())
                {
                    var json = buttonJson[j];
                    $("#buttonname").val(json.name);
                    $("#ClientScript").val(json.events);
                    $("#Ico").val(json.ico);
                
                    new RoadUI.SelectIco().setIco($("#Ico"));
                }
            }
        }
</script>
</body>
</html>
