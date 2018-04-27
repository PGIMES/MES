<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ShowComment.aspx.cs" Inherits="WebForm.Platform.WorkFlowRun.ShowComment" %>
    <% 
        string groupid=Request.QueryString["groupid"];
        string stepid = Request.QueryString["stepid"];
        string flowid = Request.QueryString["flowid"];
    
        RoadFlow.Platform.WorkFlowTask bwfTask = new RoadFlow.Platform.WorkFlowTask();
        RoadFlow.Platform.WorkFlow bwf = new RoadFlow.Platform.WorkFlow();
        var taskList = bwfTask.GetTaskList(flowid.ToGuid(), groupid.ToGuid()).Where(p => !p.Comment.IsNullOrEmpty() && p.CompletedTime1.HasValue).OrderBy(p => p.Sort);
    %>
    <style type="text/css">
        .commenttable { margin:12px auto 0 auto; width:97%; background:#ededee; }
        .commenttable tr th { text-align:left; height:25px; background:#ffffff; font-weight:normal;}
        .commenttable tr td { height:28px; background:#ffffff;}
    </style>
    <% 
    foreach (var task in taskList)
    {
        string signSrc=string.Empty;
        if (task.IsSign.HasValue && task.IsSign == 1) 
        {
            signSrc = string.Concat("../../Files/UserSigns/", task.ReceiveID, ".gif"); 
        }
    %>
    <table cellpadding="1" cellspacing="1" border="0" class="commenttable">
        <tr>
            <th style="width:10%;">&nbsp;步骤：<%=bwf.GetStepName(task.StepID, task.FlowID) %></th>
            <th style="width:10%;">&nbsp;处理人：<%=task.ReceiveName %></th>
            <th style="width:10%;">&nbsp;收件时间：<%=task.ReceiveTime.ToDateTimeStringS() %></th>
            <th style="width:10%;">&nbsp;完成时间：<%=task.CompletedTime1.HasValue?task.CompletedTime1.Value.ToDateTimeStringS():"" %></th>
            <th style="width:25%;">&nbsp;<div style="float:left; height:26px; padding:2px 0 0 2px; ">
            处理意见：<%=task.Comment %>
            </div>
            <div style="float:left; height:26px; width:77px; margin:1px 2px 0 1px; background:url(<%=signSrc%>) no-repeat left center;">&nbsp;</div></th>
        </tr>        
        <%if(!task.Files.IsNullOrEmpty()){ %>
        <tr>
            <td colspan="5" style="padding:9px 6px;">
                <%=RoadFlow.Platform.Files.GetFilesShowString(task.Files, "", true) %>
            </td>
        </tr>
        <%} %>
    </table>
    <%} %>
