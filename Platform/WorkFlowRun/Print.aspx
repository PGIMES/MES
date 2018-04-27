<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Print.aspx.cs" Inherits="WebForm.Platform.WorkFlowRun.Print" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta name="viewport" content="width=device-width,initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=no"/>
    <title></title>
    <link href="Scripts/Forms/flowform_print.css" rel="stylesheet" />
    <style type="text/css" media="print">
        .Noprint { display: none; }
    </style>
</head>
<body>
    <div id="buttondiv" class="Noprint" style="margin:15px 0 0 15px; z-index:1000; position:absolute">
        <button onclick="print1();" class="mybutton" style="margin-right:4px;">确认打印</button>
        <%if("1" != Request.QueryString["ismobile"]){ %>
        <button class="mybutton" onclick="window.close();">取消关闭</button>
        <%} %>
    </div>
    <div style="width:98%; margin:0px auto 0 auto; z-index:1;">
    <%
        string flowid = Request.QueryString["flowid"];
        string stepid = Request.QueryString["stepid"];
        string instanceid = Request.QueryString["instanceid"];
        string taskid = Request.QueryString["taskid"];
        string groupid = Request.QueryString["groupid"];
        bool isMobile = "1" == Request.QueryString["ismobile"];
        if (instanceid.IsNullOrEmpty())
        {
            instanceid = Request.QueryString["instanceid1"];
        }

        RoadFlow.Platform.WorkFlow bworkFlow = new RoadFlow.Platform.WorkFlow();
        RoadFlow.Platform.WorkFlowTask btask = new RoadFlow.Platform.WorkFlowTask();

        RoadFlow.Data.Model.WorkFlowInstalled wfInstalled = bworkFlow.GetWorkFlowRunModel(flowid);
        if (wfInstalled == null)
        {
            Response.Write("未找到流程运行实体");
            Response.End();
        }
        var steps = wfInstalled.Steps.Where(p => p.ID == stepid.ToGuid());
        if (steps.Count() == 0)
        {
            Response.Write("未找到当前步骤");
            Response.End();
        }
        var currentStep = steps.First();
        if (currentStep.Forms.Count() == 0)
        {
            Response.Write("当前步骤没有表单");
            Response.End();
        }
        Guid formID = Guid.Empty;
        var formSet = currentStep.Forms.First();
        if (!isMobile)
        {
            formID = formSet.ID;
        }
        else
        {
            formID = formSet.IDApp.IsEmptyGuid() ? formSet.ID : formSet.IDApp;
        }
        var form = new RoadFlow.Platform.AppLibrary().Get(formID);
        if (form != null)
        {
            string src = form.Address;
            if (!src.IsNullOrEmpty())
            {
                if (src.IndexOf('?') > 0)
                {
                    src += "&"+Request.Url.Query.TrimStart('?');
                }
                else
                {
                    src += "?" + Request.Url.Query.TrimStart('?');
                }
                src = src.Trim1().StartsWith("/") ? WebForm.Common.Tools.BaseUrl + src : src;
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                System.IO.TextWriter tw = new System.IO.StringWriter(sb);
                try
                {
                    Server.Execute(src, tw);
                    Response.Write(sb.ToString().RemovePageTag());
                }
                catch (Exception err)
                {
                    Response.Write(err.Message);
                }
            }
        }
    %>
    </div>
    <div style="margin-top:-10px;">
        <%Server.Execute("ShowComment.aspx"); %>
    </div>
    <script type="text/javascript">
        function print1()
        {
            window.print();
        }
    </script>
</body>
</html>
