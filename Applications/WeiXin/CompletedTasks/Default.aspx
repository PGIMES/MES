<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebForm.Applications.WeiXin.CompletedTasks.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta name="viewport" content="width=device-width,initial-scale=1,user-scalable=0"/>
    <title>已办事项</title>
    <link href="../Scripts/weui.min.css" rel="stylesheet" />
    <link href="../Scripts/ext.css?v=2" rel="stylesheet" />
    <link href="../Scripts/jquery-weui.min.css" rel="stylesheet" />
    <style>
        .date {
            color:#999; font-size:12px;
        }
    </style>
</head>
<body>
    <!-- body 顶部加上如下代码 -->
    <div class="weui-pull-to-refresh__layer">
        <div class='weui-pull-to-refresh__arrow'></div>
        <div class='weui-pull-to-refresh__preloader'></div>
        <div class="down">下拉刷新</div>
        <div class="up">释放刷新</div>
        <div class="refresh">正在刷新</div>
    </div>
    <form id="form1" runat="server">
    <div style="text-align:center; margin:10px 0 0 0;"><input class="ext-input" id="searchkey" name="searchkey" runat="server" style="margin-right:8px;width:70%;"/><asp:Button ID="Button1" CssClass="ext-button" runat="server" Text="&nbsp;&nbsp;查询&nbsp;&nbsp;" OnClick="Button1_Click" /></div>
    <div class="weui-cells" style="overflow-y:auto; overflow-x:hidden;">
        <%
        foreach(var task in TasksList)
        {
            var address = RoadFlow.Utility.Config.BaseUrl + "/Platform/WorkFlowRun/Default_App.aspx?flowid=" + task.FlowID
                + "&stepid=" + task.StepID + "&instanceid=" + task.InstanceID + "&taskid=" + task.ID + "&groupid=" + task.GroupID + "&ismobile=1&display=1";
        %>
        <a class="weui-cell weui-cell_access" href="<%=address %>">
            <div class="weui-cell__bd">
                <p><%=task.Title %><p class="date">完成时间:<%=task.CompletedTime1.HasValue ? task.CompletedTime1.Value.ToDateTimeString() : "" %> 发送人:<%=task.SenderName %></p></p>
            </div>
            <div class="weui-cell__ft">
            </div>
        </a>
        <% 
        }    
        %>
    </div>
    <div class="weui-loadmore" id="loaddata">
        <i class="weui-loading"></i>
        <span class="weui-loadmore__tips">正在加载</span>
    </div>
    <div class="weui-loadmore weui-loadmore_line" id="nodata" style="display:none;">
        <span class="weui-loadmore__tips">没有更多数据了</span>
    </div>
    </form>
    <script src="../Scripts/jquery-1.11.1.min.js"></script>
    <script src="../Scripts/jquery-weui.min.js"></script>
    <script type="text/javascript">
        var pagenumber = 1;
        var loading = false;  //状态标记
        $(function ()
        {
            $(document.body).infinite(50);
            $(document.body).pullToRefresh();
            $(document.body).on("pull-to-refresh", function ()
            {
                //do something
                window.location = window.location;
                $(document.body).pullToRefreshDone();
            });
            $("#loaddata").hide();
            $(document.body).infinite().on("infinite", function ()
            {
                if (loading) return;
                loading = true;
                $("#loaddata").show();
                $.ajax({
                    url: "GetTasks.ashx?pagenumber=" + (++pagenumber).toString() + "&pagesize=15&SearchTitle=<%=SearchTitle%>", async: false,
                    cache: false, dataType: "json", success: function (json)
                    {
                        if (json.length == 0)
                        {
                            $("#loaddata").hide();
                            $("#nodata").show();
                            $(document.body).destroyInfinite();
                            return;
                        }
                        var html = '';
                        for (var i = 0; i < json.length; i++)
                        {
                            var address = "<%=RoadFlow.Utility.Config.BaseUrl%>" + "/Platform/WorkFlowRun/Default_App.aspx?flowid=" + json[i]["flowid"]
                            + "&stepid=" + json[i]["stepid"] + "&instanceid=" + json[i]["instanceid"] + "&taskid=" + json[i]["id"] + "&groupid=" + json[i]["groupid"] + "&ismobile=1&display=1";
                            html += '<a class="weui-cell weui-cell_access" href="' + address + '">';
                            html += '<div class="weui-cell__bd">';
                            html += '<p>' + json[i]["title"] + '<p class="date">完成时间:' + json[i]["time"] + ' 发送人:' + json[i].sender + '</span></p>';
                            html += '</div>';
                            html += '<div class="weui-cell__ft">';
                            html += '</div>';
                            html += '</a>';
                        }
                        $(".weui-cells").append(html);
                        loading = false;
                        $("#loaddata").hide();
                    }
                });

            });
        });
</script>
</body>
</html>
