<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GoTo.aspx.cs" Inherits="WebForm.Platform.WorkFlowTasks.GoTo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table class="listtable">
            <thead>
                <tr>
                    <th><input type="checkbox" onclick="$('[name=\'step\']').prop('checked', this.checked);" style="vertical-align:middle;" id="checkall" /><label for="checkall">步骤</label></th>
                    <th>处理人员</th>
                </tr>
            </thead>
            <tbody>
                <% 
                foreach(var step in nextSteps)
                {    
                    string selectType;
                    string selectRange;
                    string defaultMember = BTask.GetDefultMember(Task.FlowID, step.ID, Task.GroupID, Task.StepID, Task.InstanceID, out selectType, out selectRange);
                %>
                <tr>
                    <td><input type="checkbox" style="vertical-align:middle;" name="step" id="step_<%=step.ID %>" value="<%=step.ID %>" /><label style="vertical-align:middle;" for="step_<%=step.ID %>"><%=step.Name %></label></td>
                    <td><input type="text" <%=selectType %> value="<%=defaultMember %>" class="mymember" id="member_<%=step.ID %>" name="member_<%=step.ID %>" /></td>
                </tr>
                <%} %>
            </tbody>
        </table>
        <div class="buttondiv" style="margin-top:10px;">
            <asp:Button ID="Button1" CssClass="mybutton" runat="server" Text="确认跳转" OnClientClick="return checkgoto();" OnClick="Button1_Click" />
            <input type="button" class="mybutton" value="关闭窗口" onclick="new RoadUI.Window().close();" />
        </div>
        
    </div>
    </form>
    <script type="text/javascript">
        function checkgoto()
        {
            if ($(":checked[name='step']").size()==0)
            {
                alert("您没有选择要跳转的步骤!");
                return false;
            }
            return true;
        }
    </script>
</body>
</html>
