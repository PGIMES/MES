<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebForm.Applications.WeiXin.Documents.Default" %>

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
    <form id="form1" runat="server" action="Search.aspx" method="post">
        <div style="text-align:center; margin:10px 0 0 0;"><input class="ext-input" id="searchkey" name="searchkey" placeholder="输入文档标题可查询" runat="server" style="margin-right:8px;width:70%;"/><asp:Button ID="Button1" CssClass="ext-button" runat="server" OnClientClick="return checks();" Text="&nbsp;&nbsp;查询&nbsp;&nbsp;" OnClick="Button1_Click" /></div>
        <% 
            long noReadCount;
            System.Data.DataTable DocDt = bdoc.GetList(out noReadCount, 1000, 1, "", userID.ToString(), "", "", "", true);
            if(DocDt.Rows.Count>0)
            {   
        %>
            <div class="weui-cells__title" style="font-weight:bold;"><a class="weui-cell_access">未读文档(<%=noReadCount %>)</a></div>
            <div class="weui-cells">
                <%foreach(System.Data.DataRow dr in DocDt.Rows){
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
        <%} %>
        <% 
            foreach (var dict in dicts)
            {
                long count;
                var docs = bdoc.GetList(out count, 5, 1, dict.Key.ToString(), userID.ToString());
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
        <% }}%>

        <script src="../Scripts/jquery-1.11.1.min.js"></script>
        <script src="../Scripts/jquery-weui.min.js"></script>
        <script type="text/javascript">
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
            function checks()
            {
                if ($.trim($("#searchkey").val()).length == 0)
                {
                    alert('请输入要查询的关键字');
                    return false;
                }
                return true;
            }
        </script>
    </form>
</body>
</html>
