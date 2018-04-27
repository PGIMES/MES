<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InstanceManage.aspx.cs" Inherits="WebForm.Platform.WorkFlowTasks.InstanceManage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <% 
        RoadFlow.Platform.WorkFlowTask bworkFlowTask = new RoadFlow.Platform.WorkFlowTask();
        RoadFlow.Platform.WorkFlow bworkFlow = new RoadFlow.Platform.WorkFlow();
        string flowid = Request.QueryString["flowid1"];
        string groupid = Request.QueryString["groupid"];
        var wfInstall = bworkFlow.GetWorkFlowRunModel(flowid);
        var tasks = bworkFlowTask.GetTaskList(flowid.ToGuid(), groupid.ToGuid()).OrderBy(p => p.Sort);    
    %>
    <table cellpadding="0" cellspacing="1" border="0" class="listtable" style="width:100%; margin-top:1px;">
    <thead>
        <tr>
            <th>步骤名称</th>
            <th>发送人</th>
            <th>接收时间</th>
            <th>处理人</th>
            <th>完成时间</th>
            <th>状态</th>
            <th>意见</th>
            <th></th>
        </tr>
    </thead>
    <tbody>
    <%foreach (var task in tasks){  %>
        <tr>
            <td><%=bworkFlow.GetStepName(task.StepID, wfInstall) %></td>
            <td><%=task.SenderName %></td>
            <td><%=task.ReceiveTime.ToDateTimeStringS() %></td>
            <td><%=task.ReceiveName %></td>
            <td><%=task.CompletedTime1.HasValue?task.CompletedTime1.Value.ToDateTimeStringS():"" %></td>
            <td><%=bworkFlowTask.GetStatusTitle(task.Status) %></td>
            <td><%=task.Comment %></td>
            <td>
            <a style="background:url(../../Images/ico/system-tick-alt-03.png) no-repeat left center; padding-left:17px;" href="javascript:void(0);" onclick="cngStatus('<%=task.ID %>');">状态</a>
            <%if (task.Status.In(0,1)){ %>
                <a style="background:url(../../Images/ico/arrow_medium_lower_left.png) no-repeat left center; padding-left:16px;" href="javascript:void(0);" onclick="designate('<%=task.ID %>');">指派</a>
                <a style="background:url(../../Images/ico/arrow_medium_lower_right.png) no-repeat left center; padding-left:16px;" href="javascript:void(0);" onclick="goTo('<%=task.ID %>');">跳转</a>
            <%}%>
            </td>
        </tr>
    <%}%>
    </tbody>
</table>
<script type="text/javascript">
    var iframeid = '<%=Request.QueryString["iframeid"]%>';
    function back(taskid)
    {
        if (confirm("您真的要将该任务退回吗?"))
        {
            $.ajax({
                url: "Back.ashx?taskid=" + taskid, async: false, cache: false, success: function (txt)
                {
                    alert(txt);
                    window.location = window.location;
                }
            });
        }
    }
    function designate(taskid)
    {
        new RoadUI.Window().open({
            url: '/Platform/WorkFlowTasks/Designate.aspx?taskid=' + taskid,
            width: 500, height: 200, title: "任务指派", openerid: iframeid, opener:parent, resize:false
        });
    }
    function goTo(taskid)
    {
        new RoadUI.Window().open({
            url: '/Platform/WorkFlowTasks/GoTo.aspx?taskid=' + taskid,
            width: 600, height: 400, title: "跳转任务", openerid: iframeid, opener: parent, resize: false
        });
    }
    function cngStatus(taskid)
    {
        new RoadUI.Window().open({
            url: '/Platform/WorkFlowTasks/ChangeStatus.aspx?taskid=' + taskid,
            width: 400, height: 200, title: "改变任务状态", openerid: iframeid, opener: parent, resize: false
        });
    }
</script>
</body>
</html>
