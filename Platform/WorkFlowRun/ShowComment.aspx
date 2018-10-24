<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ShowComment.aspx.cs" Inherits="WebForm.Platform.WorkFlowRun.ShowComment" %>
    <% 
        string groupid=Request.QueryString["groupid"];
        string stepid = Request.QueryString["stepid"];
        string flowid = Request.QueryString["flowid"]; 
            
        RoadFlow.Platform.WorkFlowTask bwfTask = new RoadFlow.Platform.WorkFlowTask();
        RoadFlow.Platform.WorkFlow bwf = new RoadFlow.Platform.WorkFlow();
        //var taskList = bwfTask.GetTaskList(flowid.ToGuid(), groupid.ToGuid()).Where(p => !p.Comment.IsNullOrEmpty() && p.CompletedTime1.HasValue).OrderBy(p => p.Sort);
        var taskLists = bwfTask.GetTaskList(flowid.ToGuid(), groupid.ToGuid());//
        var deltasklist = taskLists.Where(p => p.CompletedTime1.HasValue == false && p.Status == 2).ToList();
        foreach (var dtask in deltasklist)
        {
            taskLists.Remove(dtask);
        }
        var taskList = taskLists.OrderBy(p => p.Sort);
    %>
    <style type="text/css">
        .commenttable { margin:12px auto 0 auto; width:97%; background:#ededee; }
        .commenttable tr th { text-align:left; height:25px; background:#e7ecf5; font-weight:normal;}
        .commenttable tr td { height:28px; background:#ffffff;}
    </style>
    <%
        if (taskList.Count() > 0) {  %>
            <table cellpadding="1" cellspacing="1" border="0" class="commenttable">
        <tr>
            <th style="width:10%;">&nbsp;步骤</th>
            <th style="width:10%;">&nbsp;处理人</th>
           <%-- <th style="width:10%;">&nbsp;收件时间</th>--%>
            <th style="width:10%;">&nbsp;完成时间</th>
            <th style="width:5%;">&nbsp;耗时(工作日)</th>
            <th style="width:25%;">&nbsp;处理意见</th>
           
        </tr>
<% 
        }        
    %>
    <% 


        foreach (var task in taskList)
        {
            string signSrc=string.Empty;
            if (task.IsSign.HasValue && task.IsSign == 1)
            {
                signSrc = string.Concat("../../Files/UserSigns/", task.ReceiveID, ".gif");
            }
            string st = task.Type.ToString().Replace("5"," [抄送]").Replace("4"," [退回]") ;
            st = st.Length <= 1 ? "" : st;
            st = "";
            //耗时
            DateTime DateTime1,
            DateTime2 = task.SenderTime;//现在时间  
            DateTime1 = Convert.ToDateTime(task.CompletedTime1.HasValue ? task.CompletedTime1 : DateTime.Now); //设置要求的减的时间  
            string dateDiff = null;
            if (task.CompletedTime1.HasValue==true)
            {
             //   TimeSpan ts1 = new TimeSpan(DateTime1.Ticks);
            //    TimeSpan ts2 = new TimeSpan(DateTime2.Ticks);
            //    TimeSpan ts = ts1.Subtract(ts2).Duration();
                //显示时间  
                // dateDiff = ts.Days.ToString() + "天" + ts.Hours.ToString() + "小时";
                // dateDiff = ts.Days==0&&ts.Hours==0  ? "< 1小时" : dateDiff;
                //获取除去节假日使用分钟数
                int mins = Convert.ToInt32(Maticsoft.DBUtility.DbHelperSQL.GetSingle(string.Format("select dbo.get_usedtime('{0}','{1}') ", DateTime2, DateTime1)));

                var time =mins;
                if (   time>0) {
                    if (time > 1 && time < 60 ) {
                        dateDiff = "<1小时"; // Convert.ToInt32(time / 60.0).ToString() + "分钟" +  (Convert.ToInt32((Convert.ToSingle(time / 60.0) - Convert.ToInt32(time / 60.0)) * 60)).ToString() + "秒";
                    }
                    else if (time >= 60  && time < 60  * 24) {
                        dateDiff =  Math.Floor((time % 1440)/60.0).ToString()+"小时";
                       // dateDiff = Convert.ToInt32(time / 3600.0).ToString() + "小时"+ (Convert.ToInt32((Convert.ToSingle(time / 3600.0) - Convert.ToInt32(time / 3600.0)) * 60)).ToString() + "分";
                        // +(Convert.ToInt32((Convert.ToSingle((Convert.ToSingle(time / 3600.0) -  Convert.ToInt32(time / 3600.0)) * 60) - Convert.ToInt32((Convert.ToSingle(time / 3600.0) -  Convert.ToInt32(time / 3600.0)) * 60)) * 60)).ToString() + "秒";
                    } else if (time >= 60  * 24) {
                        dateDiff = Math.Floor(time*0.1/(1440*0.1)).ToString()+"天"+ Math.Floor((time % 1440)/60.0).ToString()+"小时";
                        //dateDiff = Convert.ToInt32(time / 3600.0 / 24) + "天" + Convert.ToInt32((Convert.ToSingle(time / 3600.0 / 24) -  Convert.ToInt32(time / 3600.0 / 24)) * 24) + "小时";
                        // + Convert.ToInt32((Convert.ToSingle(time / 3600.0) -  Convert.ToInt32(time / 3600.0)) * 60) + "分钟";
                        //  + Convert.ToInt32((Convert.ToSingle((Convert.ToSingle(time / 3600.0) -  Convert.ToInt32(time / 3600.0)) * 60) - Convert.ToInt32((Convert.ToSingle(time / 3600.0) -  Convert.ToInt32(time / 3600.0)) * 60)) * 60) + "秒";
                    }
                    else {
                        dateDiff =  "<1小时"; // Convert.ToInt32(time) + "秒";
                    }
                }

            }


    %>
    
        <tr>
            <td style="width:10%;">&nbsp;<%=bwf.GetStepName(task.StepID, task.FlowID) %></td>
            <td style="width:10%;">&nbsp;<%=task.ReceiveName   %></td>
            <%--<td style="width:10%;">&nbsp;<%=task.ReceiveTime.ToDateTimeStringS() %></td>--%>
            <td style="width:10%;">&nbsp;<%=task.CompletedTime1.HasValue?task.CompletedTime1.Value.ToDateTimeStringS():"" %></td>
            <td style="width:5%;">&nbsp;<%=dateDiff %></td>
            <td style="width:25%;">&nbsp;
                <div style="float:left; height:26px; padding:2px 0 0 2px; "> <%=task.Comment %> </div>
                <div style="float:left; height:26px; width:77px; margin:1px 2px 0 1px; background:url(<%=signSrc%>) no-repeat left center;">&nbsp;</div></td>
        </tr>        
        <%if(!task.Files.IsNullOrEmpty()){ %>
        <tr>
            <td colspan="6" style="padding:9px 6px;">
                <%=RoadFlow.Platform.Files.GetFilesShowString(task.Files, "", true) %>
            </td>
        </tr>
        <%} %>
  
    <%} %>

    <% if (taskList.Count() > 0) {  %>
        </table>
<% } %>