<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="List.aspx.cs" Inherits="WebForm.Applications.WeiXin.Documents.List" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta name="viewport" content="width=device-width,initial-scale=1,user-scalable=0"/>
    <title>文档中心</title>
    <link href="../Scripts/weui.min.css" rel="stylesheet" />
    <link href="../Scripts/ext.css" rel="stylesheet" />
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
        <div style="text-align:center; margin:10px 0 0 0;"><input class="ext-input" id="searchkey" name="searchkey" placeholder="输入文档标题可查询" runat="server" style="margin-right:8px;width:70%;"/><asp:Button ID="Button1" CssClass="ext-button" runat="server" Text="&nbsp;&nbsp;查询&nbsp;&nbsp;" OnClick="Button1_Click" /></div>
        <% 
            RoadFlow.Platform.DocumentDirectory docDir = new RoadFlow.Platform.DocumentDirectory();
            RoadFlow.Platform.Documents bdoc = new RoadFlow.Platform.Documents();
            Guid userID = RoadFlow.Platform.Users.CurrentUserID;
            string dirID = Request.QueryString["dirid"];
            Dictionary<Guid, string> dicts = new Dictionary<Guid, string>();
            dicts.Add(dirID.ToGuid(), docDir.GetAllParentsName(dirID.ToGuid(), true, false));
            foreach (var dict in dicts)
            {
                long count;
                var docs = bdoc.GetList(out count, 15, 1, dict.Key.ToString(), userID.ToString(), searchText);
                if (docs.Rows.Count > 0)
                {
        %>
        <div class="weui-cells__title" style="font-weight:bold;"><a class="weui-cell_access" href="List.aspx?ismobile=1&dirid=<%=dict.Key %>"><%=dict.Value%>(<%=count %>)</a></div>
        <div class="weui-cells">
            <% 
                    foreach (System.Data.DataRow dr in docs.Rows)
                    {
                        string address = "../../Documents/DocRead.aspx?ismobile=1&docid=" + dr["id"].ToString();
            %>
            <a class="weui-cell weui-cell_access" href="<%=address %>">
                <div class="weui-cell__bd">
                  <p><%=dr["Title"]%></p><p class="date">发布时间:<%=dr["WriteTime"].ToString().ToDateTime().ToDateTimeString() %> 发布人:<%=dr["WriteUserName"].ToString() %></p>
                </div>
                <div class="weui-cell__ft">
                </div>
            </a>
            <%} %>
        </div>
        <% }
            }%>
        <div class="weui-loadmore" id="loaddata">
            <i class="weui-loading"></i>
            <span class="weui-loadmore__tips">正在加载</span>
        </div>
        <div class="weui-loadmore weui-loadmore_line" id="nodata" style="display:none;">
            <span class="weui-loadmore__tips">没有更多数据了</span>
        </div>
        <script src="../Scripts/jquery-1.11.1.min.js"></script>
        <script src="../Scripts/jquery-weui.min.js"></script>
        <script type="text/javascript">
            var pagenumber = 1;
            var loading = false;  //状态标记
            $(function ()
            {
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
                        url: "GetDocs.ashx?&dirid=<%=dirID%>&pagenumber=" + (++pagenumber).toString() + "&pagesize=15&SearchTitle=<%=searchText%>", async: false,
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
                            var address = "<%=RoadFlow.Utility.Config.BaseUrl%>" + "/Applications/Documents/DocRead.aspx?ismobile=1&docid=" + json[i]["id"];
                            html += '<a class="weui-cell weui-cell_access" href="' + address + '">';
                            html += '<div class="weui-cell__bd">';
                            html += '<p>' + json[i]["title"] + '<p class="date">发布时间:' + json[i]["writetime"] + ' 发布人:' + json[i]["writeuser"] + '</span></p>';
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
    </form>
</body>
</html>
