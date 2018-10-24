<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FlowSend.aspx.cs" Inherits="WebForm.Platform.WorkFlowRun.FlowSend" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <table cellpadding="0" cellspacing="1" border="0" width="99%" align="center" style="margin-top:6px;">
        <tr>

            <td style="vertical-align:top;width:50%;" id="steptd">
                <fieldset style="padding:4px; min-height:200px; border:1px solid #e8e8e8;">
                    <legend>&nbsp;接收步骤和人员&nbsp;</legend>
    <%
        string flowid = Request.QueryString["flowid"];
        string stepid = Request.QueryString["stepid"];
        string groupid = Request.QueryString["groupid"];
        string taskid = Request.QueryString["taskid"];
        string instanceid = Request.QueryString["instanceid"];
        int tasktype = (Request.QueryString["tasktype"] ?? "0").ToInt(0);
        if (instanceid.IsNullOrEmpty())
        {
            instanceid = Request.QueryString["instanceid1"];
        }
        
        RoadFlow.Platform.WorkFlow bworkFlow = new RoadFlow.Platform.WorkFlow();
        RoadFlow.Platform.WorkFlowTask btask = new RoadFlow.Platform.WorkFlowTask();
        RoadFlow.Platform.Users busers = new RoadFlow.Platform.Users();
        RoadFlow.Data.Model.WorkFlowInstalled wfInstalled = bworkFlow.GetWorkFlowRunModel(flowid);
        if (wfInstalled == null)
        {
            Response.Write("未找到流程运行实体");
            Response.End();
        } 
    
        var steps = wfInstalled.Steps.Where(p => p.ID == stepid.ToGuid());
        if(steps.Count()==0)
        {
            Response.Write("未找到当前步骤");
            Response.End();
        }
        
        var currentStep = steps.First();
        //如果tasktype == 5是抄送任务，则将后续步骤集合为空，直接完成任务
        var nextSteps = tasktype == 5 ? new List<RoadFlow.Data.Model.WorkFlowInstalledSub.Step>() : tasktype == 7 ? new RoadFlow.Platform.WorkFlowTask().GetAddWriteSteps(taskid.ToGuid())
            : bworkFlow.GetNextSteps(wfInstalled.ID, currentStep.ID).OrderBy(p => p.Position_x).ThenBy(p => p.Position_y).ToList();
        int i = 0;
     %>
        <table cellpadding="0" cellspacing="1" border="0" width="95%" align="center" style="margin-top:0px;">
    <%if (!currentStep.Note.IsNullOrEmpty()){ %>
        <tr>
            <td style="padding:2px 0 0 0; color:#cc0000;"><%=currentStep.Note %></td>
        </tr>
    <%} %>
    <%
        //判断流转条件
        if (currentStep.Behavior.FlowType == 0 && nextSteps.Count() > 0 && tasktype != 7)
        {
            List<Guid> removeIDList = new List<Guid>();
            RoadFlow.Data.Model.WorkFlowCustomEventParams eventParams = new RoadFlow.Data.Model.WorkFlowCustomEventParams();
            eventParams.FlowID = flowid.ToGuid();
            eventParams.GroupID = groupid.ToGuid();
            eventParams.StepID = stepid.ToGuid();
            eventParams.TaskID = taskid.ToGuid();
            eventParams.InstanceID = instanceid;

            System.Text.StringBuilder nosubmitMsg = new System.Text.StringBuilder();
            foreach (var step in nextSteps)
            {
                var lines = wfInstalled.Lines.Where(p => p.ToID == step.ID && p.FromID == steps.First().ID);
                if (lines.Count() > 0)
                {
                    var line = lines.First();
                    if (!line.SqlWhere.IsNullOrEmpty())
                    {
                        if (wfInstalled.DataBases.Count() == 0)
                        {
                            removeIDList.Add(step.ID);
                            //nosubmitMsg.Append("流程未设置数据连接");
                            //nosubmitMsg.Append("\\n");
                        }
                        else
                        {
                            if (!btask.TestLineSql(wfInstalled.DataBases.First().LinkID, wfInstalled.DataBases.First().Table,
                                 wfInstalled.DataBases.First().PrimaryKey, instanceid, line.SqlWhere))
                            {
                                removeIDList.Add(step.ID);
                                //nosubmitMsg.Append(string.Concat("提交条件未满足"));
                                //nosubmitMsg.Append("\\n");
                            }
                        }
                    }
                    if (!line.CustomMethod.IsNullOrEmpty())
                    {
                        object obj = btask.ExecuteFlowCustomEvent(line.CustomMethod.Trim(), eventParams);
                        var objType = obj.GetType();
                        var boolType = typeof(Boolean);
                        if (objType != boolType && "1" != obj.ToString())
                        {
                            removeIDList.Add(step.ID);
                            nosubmitMsg.Append(obj.ToString());
                            nosubmitMsg.Append("\\n");
                        }
                        else if (objType == boolType && !(bool)obj)
                        {
                            removeIDList.Add(step.ID);
                            nosubmitMsg.Append(obj.ToString());
                            nosubmitMsg.Append("\\n");
                        }
                    }
                    #region 组织机构关系判断
                    Guid SenderID = RoadFlow.Platform.Users.CurrentUserID;
                    Guid sponserID = Guid.Empty;//发起者ID
                    RoadFlow.Platform.Organize borg = new RoadFlow.Platform.Organize();
                    if (currentStep.ID == wfInstalled.FirstStepID)//如果是第一步则发起者就是发送者
                    {
                        sponserID = SenderID;
                    }
                    else
                    {
                        sponserID = btask.GetFirstSnderID(eventParams.FlowID, eventParams.GroupID);
                    }
                    StringBuilder orgWheres = new StringBuilder();
                    if (!line.Organize.IsNullOrEmpty())
                    {
                        LitJson.JsonData orgJson = LitJson.JsonMapper.ToObject(line.Organize);
                        foreach (LitJson.JsonData json in orgJson)
                        {
                            if (orgJson.Count == 0) continue;
                            string usertype = json["usertype"].ToString();
                            string in1 = json.ContainsKey("in1") ? json["in1"].ToString() : "";
                            string users = json["users"].ToString();
                            string selectorganize = json["selectorganize"].ToString();
                            string tjand = json["tjand"].ToString();
                            string khleft = json["khleft"].ToString();
                            string khright = json["khright"].ToString();
                            Guid userid = "0" == usertype ? SenderID : sponserID;
                            string memberid = "";
                            bool isin = false;
                            if ("0" == users)
                            {
                                memberid = selectorganize;
                            }
                            else if ("1" == users)
                            {
                                memberid = busers.GetLeader(userid);
                            }
                            else if ("2" == users)
                            {
                                memberid = busers.GetChargeLeader(userid);
                            }
                            if ("0" == in1)
                            {
                                isin = busers.IsContains(userid, memberid);
                            }
                            else if ("1" == in1)
                            {
                                isin = !busers.IsContains(userid, memberid);
                            }
                            if (!khleft.IsNullOrEmpty())
                            {
                                orgWheres.Append(khleft);
                            }
                            orgWheres.Append(isin ? " true " : " false ");
                            if (!khright.IsNullOrEmpty())
                            {
                                orgWheres.Append(khright);
                            }
                            orgWheres.Append(tjand);
                        }
                        string orgCode = string.Concat("bool testbool=", orgWheres.ToString(), ";return testbool;");
                        object rogCodeResult = RoadFlow.Utility.Tools.ExecuteCsharpCode(orgCode);
                        if (rogCodeResult != null && !(bool)rogCodeResult)
                        {
                            removeIDList.Add(step.ID);
                        }
                    }
                    #endregion

                }
            }
            foreach (Guid rid in removeIDList)
            {
                nextSteps.RemoveAll(p => p.ID == rid);
            }

            if (nextSteps.Count == 0)
            {
                string alertMsg = nosubmitMsg.ToString();
                alertMsg = alertMsg.IsNullOrEmpty() ? "后续步骤条件均不符合,任务不能提交!" : alertMsg;
                Response.Write("<script>alert('" + alertMsg + "');top.mainDialog.close();</script>");
                Response.End();
            }
        }
        foreach (var step in nextSteps.OrderBy(p=>p.Position_y).ThenBy(p=>p.Position_x))
        {
            string checked1 = i++ == 0 ? "checked=\"checked\"" : "";//默认选中第一个步骤
            string disabled = step.Behavior.RunSelect == 0 || tasktype == 7 ? "disabled=\"disabled\"" : "";//是否允许运行时选择人员
            string selectRang = string.Empty;//选择范围
            string selectType = string.Empty;//选择类型
            string defaultMember = string.Empty;
            if (tasktype == 7)
            {
                var currTask = new RoadFlow.Platform.WorkFlowTask().Get(taskid.ToGuid());
                if (currTask != null)
                {
                    defaultMember = btask.GetAddWriteMembers(taskid.ToGuid());
                }
            }
            else
            {
                defaultMember = btask.GetDefultMember(flowid.ToGuid(), step.ID, groupid.ToGuid(), currentStep.ID, instanceid, out selectType, out selectRang);
            }
            if (!step.Behavior.SelectRange.IsNullOrEmpty())
            {
                selectRang = "rootid=\"" + step.Behavior.SelectRange.Trim() + "\"";
            }
     %>
        <tr>
            <td style="padding:0px 0 2px 0;">
            <input type="hidden" name="nextstepid" value="<%=step.ID %>" />
            <%if (currentStep.Behavior.FlowType == 1){ %>
            <input type="radio" <%=checked1 %> value="<%=step.ID %>" name="step" id="step_<%=step.ID %>" style="vertical-align:middle;" />
            <%}else if (currentStep.Behavior.FlowType == 2){ %>
            <input type="checkbox" <%=checked1 %> value="<%=step.ID %>" name="step" id="Checkbox1" style="vertical-align:middle;" />
            <%}else{%> 
            <input type="checkbox" checked="checked" disabled="disabled" value="<%=step.ID %>" name="step" id="Checkbox2" style="vertical-align:middle;" />
            <%}%> 
            <label for="step_<%=step.ID %>" style="vertical-align:middle;"><%=step.Name %></label>
            </td>
        </tr>
        <tr>
            <td style="padding:2px 0 4px 0;">
            <input type="text" class="mymember" <%=disabled %> <%=selectRang %> <%=selectType %> value="<%=defaultMember %>" id="user_<%=step.ID %>" name="user_<%=step.ID %>" style="width:75%;" title="选择处理人员" isChangeType="<%=selectRang.Length>0?"1":"0" %>" /> <!--span style="color:#999;">//选择处理人员</span-->
            </td>
        </tr>
        <tr><td style="height:6px; border-bottom:1px dashed #e8e8e8;"></td></tr>
    <%} %>
    </table>
                </fieldset>
            </td>
        </tr>
    </table>
    <div style="width:99%; margin:16px auto 0 auto; text-align:center;">
        <input type="button" class="mybutton" onclick="return confirm1();" value="&nbsp;确&nbsp;定&nbsp;" style="margin-right:5px;" />
        <input type="button" class="mybutton" value="&nbsp;取&nbsp;消&nbsp;" onclick="new RoadUI.Window().close();" style="display:none"/>
    </div>
    <script type="text/javascript">
        var frame = null;
        var openerid = '<%=Request.QueryString["openerid"]%>';
        var nextStepsCount = <%=nextSteps.Count()%>;
        var isAddWrite = "7"=="<%=tasktype%>";
        var iframeid = '<%=Request.QueryString["tabid"]%>';
        var iframeid1 = '<%=Request.QueryString["iframeid"]%>';
        var isDebug = false;
        var isCompleted = false;//是否完成
        var ismobile = "1"=='<%=Request.QueryString["ismobile"]%>';//是否是移动端
        $(function ()
        {
            var iframes = top.frames;
            for (var i = 0; i < iframes.length; i++)
            {
                var fname = "";
                try
                {
                    fname = iframes[i].name;
                }
                catch(e)
                {
                    fname="";
                }
                if (fname == openerid + "_iframe")
                {
                    frame = iframes[i]; break;
                }
            }
            if (frame == null)
            {
                frame = parent;
            }
            if (frame == null) return;
            if(nextStepsCount == 0)//如果后面没有步骤，则完成该流程实例
            {
                var options = {};
                options.type = "completed";
                options.steps = [];
                frame.formSubmit(options);
                new RoadUI.Window().close();
            }
            else if(nextStepsCount>2)
            {
                top.mainDialog.resize(ismobile ? 300 : 500,(nextStepsCount-2)*45+300);
            }
            isCompleted = nextStepsCount == 0;
            if(isCompleted)
            {
                $("#steptd").hide();
            }


            //added by fish:2018.05.15  自动递交
            confirm1();
        });
        function confirm1()
        {
            if(isCompleted)
            {
                var options = {};
                options.type = "completed";
                options.steps = [];
                frame.formSubmit(options);
                new RoadUI.Window().close();
            }

            var opts = {};
            opts.type = isAddWrite ? "addwrite": "submit";
            opts.steps = [];
            var isSubmit = true;
            
            $(":checked[name='step']").each(function ()
            {
                var step = $(this).val();
                var member = $("#user_" + step).val() || "";
                if ($.trim(member).length == 0)
                {
                    alert($(this).next().text() + " 没有选择处理人员!");
                    isSubmit = false;
                    return false;
                }
                opts.steps.push({ id: step, member: member });
            });
            if(!isSubmit)
            {
                return false;
            }
            if(opts.steps.length==0)
            {
                alert("没有选择要处理的步骤!");
                return false;
            }
            
            if (isSubmit)
            {
                frame.formSubmit(opts);
                new RoadUI.Window().close();
            }
        }
    </script>
    </form>
</body>
</html>
