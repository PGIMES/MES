<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Search.aspx.cs" Inherits="WebForm.Applications.WeiXin.Documents.Search" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
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
        <% 
            RoadFlow.Platform.DocumentDirectory docDir = new RoadFlow.Platform.DocumentDirectory();
            RoadFlow.Platform.Documents bdoc = new RoadFlow.Platform.Documents();
            Guid userID = RoadFlow.Platform.Users.CurrentUserID;
            string dirID = Request.QueryString["dirid"];
            Dictionary<Guid, string> dicts = new Dictionary<Guid, string>();
            dicts.Add(docDir.GetRoot().ID, searchText);
            foreach (var dict in dicts)
            {
                long count;
                string dirIdString = new RoadFlow.Platform.DocumentDirectory().GetAllChildIdString(dict.Key, userID);
                var docs = bdoc.GetList(out count, 10000, 1, dirIdString, userID.ToString(), searchText);
                
        %>
        <div class="weui-cells__title" style="font-weight:bold;"><a class="weui-cell_access" href="List.aspx?ismobile=1&dirid=<%=dict.Key %>">共搜索到<%=count %>篇文档</a></div>
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
           %>
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
            });
        </script>
    </form>
</body>
</html>
