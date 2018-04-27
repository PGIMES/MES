<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebForm.Applications.WeiXin.StartFlows.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta name="viewport" content="width=device-width,initial-scale=1,user-scalable=0"/>
    <title>发起流程</title>
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
    <div style="text-align:center; margin:10px 0 0 0;"><input class="ext-input" id="searchkey" name="searchkey" placeholder="输入流程关键字可查询" runat="server" style="margin-right:8px;width:70%;"/><asp:Button ID="Button1" CssClass="ext-button" runat="server" Text="&nbsp;&nbsp;查询&nbsp;&nbsp;" OnClick="Button1_Click" /></div>
     <% 
        var flows = StartFlows.GroupBy(p => p.Type).OrderBy(p=>p.Key);
        RoadFlow.Platform.Dictionary bdict = new RoadFlow.Platform.Dictionary();
        foreach(var type in flows)
        {            
     %>
        <div class="weui-cells__title" style="font-weight:bold;"><%=type.Key %></div>
        <div class="weui-cells">
      <% 
          foreach(var flow in type.OrderBy(p=>p.Name))
          {
              var address = RoadFlow.Utility.Config.BaseUrl + "/Platform/WorkFlowRun/Default_App.aspx?flowid=" + flow.ID.ToString() + "&ismobile=1";
      %>
          <a class="weui-cell weui-cell_access" href="<%=address %>">
            <div class="weui-cell__bd">
              <p><%=flow.Name %></p>
            </div>
            <div class="weui-cell__ft">
            </div>
          </a>
      <%} %>
      </div>
      <%} %>
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
    </script>
    </form>
    </body>
</html>
