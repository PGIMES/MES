<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CompletedList.aspx.cs" Inherits="WebForm.Platform.WorkFlowTasks.CompletedList" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div class="querybar">
        <table cellpadding="0" cellspacing="1" border="0" width="100%">
            <tr>
                <td>
                    名称：<input type="text" class="mytext" id="Title1" name="Title1" runat="server" />
                    所属流程：<select class="myselect" style="width:150px;" id="FlowID" name="FlowID"><option value=""></option><asp:Literal ID="flowOptions" runat="server"></asp:Literal></select>
                    发送人：<input type="text" class="mymember" id="SenderID" unit="0" dept="0" station="0" user="1" group="0" more="0" name="SenderID" runat="server"/>
                    接收时间：<input type="text" class="mycalendar" style="width:90px;" id="Date1" name="Date1" runat="server" /> 至 <input type="text" style="width:90px;" class="mycalendar" id="Date2" name="Date2" runat="server" />
                    <asp:Button ID="Button1" runat="server" CssClass="mybutton" Text="&nbsp;&nbsp;查&nbsp;询&nbsp;&nbsp;" OnClick="Button1_Click" />
                </td>
            </tr>
        </table>
    </div>

    <table class="listtable">
        <thead>
            <tr>
                <th width="24%">任务标题</th>
                <th width="10%">流程</th>
                <th width="10%">步骤</th>
                <th width="8%">发送人</th>
                <th width="13%">接收时间</th>
                <th width="13%">完成时间</th>
                <th width="8%">状态</th>
                <th width="16%"></th>
            </tr>
        </thead>
        <tbody>
        <%foreach (var task in taskList)
        {
            string flowName;
            string stepName = bworkFlow.GetStepName(task.StepID, task.FlowID, out flowName);
            string query1 = string.Format("flowid={0}&stepid={1}&instanceid={2}&taskid={3}&groupid={4}&appid={5}&display=1",
                task.FlowID, task.StepID, task.InstanceID, task.ID, task.GroupID, Request.QueryString["appid"]
                );
            bool isHasten = false;
            var applibary = bapplibary.GetByCode(task.FlowID.ToString());
            int openModel = 0;
            int width = 1000;
            int height = 500;
            if (applibary != null)
            {
                openModel = applibary.OpenMode;
                width = applibary.Width.HasValue ? applibary.Width.Value : 1000;
                height = applibary.Height.HasValue ? applibary.Height.Value : 500;
            }
         %>
            <tr>
                <td><a href="javascript:void(0);" onclick="openTask('/Platform/WorkFlowRun/Default.aspx?<%=query1 %>','<%=task.Title %>','<%=task.ID %>',<%=openModel %>,<%=width %>,<%=height %>);return false;" class="blue"><%=task.Title %></a></td>
                <td><%=flowName %></td>
                <td><%=stepName %></td>
                <td><%=task.SenderName %></td>
                <td><%=task.ReceiveTime.ToString().ToDateTimeStringS() %></td>
                <td><%=task.CompletedTime1.HasValue?task.CompletedTime1.Value.ToDateTimeStringS():"" %></td>
                <td><%=bworkFlowTask.GetStatusTitle(task.Status) %></td>
                <td>
                    <a class="viewlink" href="javascript:void(0);" onclick="detail('<%=task.FlowID %>','<%=task.GroupID %>');">查看</a>
                    <%if (task.Status==2 && bworkFlowTask.HasWithdraw(task.ID, out isHasten)){ %>
                    <a style="background:url(../../Images/ico/arrow_medium_left.png) no-repeat left center; padding-left:18px;" href="javascript:void(0);" onclick="withdraw('<%=task.ID %>');return false;">收回</a>
                    <%}%>
                    <%if(isHasten){ %>
                    <a style="background:url(../../Images/ico/role.gif) no-repeat left center; padding-left:18px;" href="javascript:void(0);" onclick="hasten('<%=task.ID %>');return false;">催办</a>
                    <%} %>
                </td>
            </tr>
        <%}%>
        </tbody>
    </table>
    <div class="buttondiv"><asp:Literal ID="Pager" runat="server"></asp:Literal></div>
    </form>
    <script type="text/javascript">
        function openTask(url, title, id, openModel, width, height)
        {
            top.openApp(url, openModel, title, id, width, height, false);
        }
        function detail(flowid, groupid)
        {
            top.mainDialog.open({
                url: '/Platform/WorkFlowTasks/Detail.aspx?flowid1=' + flowid + "&groupid=" + groupid + '<%=query%>',
                width: 1024, height: 550, title: "查看流程处理过程", opener:window
            });
        }
        function withdraw(taskID)
        {
            if (confirm("您真的要收回该任务吗?"))
            {
                $.ajax({
                    url: "Withdraw.aspx?taskid=" + taskID + '<%=query%>', cache: false, async: false, success: function (txt)
                    {
                        alert(txt);
                        window.location = window.location;
                    }
                });
            }
        }
        function hasten(taskID)
        {
            top.mainDialog.open({
                url: '/Platform/WorkFlowTasks/Hasten.aspx?taskid=' + taskID + '<%=query%>',
                width: 600, height: 300, title: "任务催办", opener: window, openerid:"<%=Request.QueryString["tabid"]%>"
            });
        }
    </script>
</body>
</html>
