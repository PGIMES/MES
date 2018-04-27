<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="JurisdictionTest.aspx.cs" Inherits="WebForm.Tests.JurisdictionTest" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <%
        //得到有权限操作的按钮的HTML，Dictionary<int,string>，里面有3个元素，0:工具栏按钮 1:普通按钮 2:列表按钮
        Dictionary<int, string> buttonHtmlDicts = WebForm.Common.Tools.GetAppButtonHtml();     
    %>

    <!--这里显示的是工具栏按钮-->
    <%=buttonHtmlDicts[0] %>
    <%if(!buttonHtmlDicts[0].IsNullOrEmpty()){%>
        <div style="height:35px;"></div>
    <%}%>   
    
    <div class="querybar">
        <table cellpadding="0" cellspacing="0" border="0" width="100%">
            <tr>
                <td>
                    标题：<input type="text" class="mytext" id="Title1" name="Title1" runat="server" style="width:150px" />
                    <asp:Button ID="Button2" runat="server" Text="&nbsp;&nbsp;查&nbsp;询&nbsp;&nbsp;" CssClass="mybutton" OnClick="Button2_Click" />
                    <%=buttonHtmlDicts[1] %><!--这里显示分配的普通按钮-->
                </td>
            </tr>
        </table>
    </div>
    <table class="listtable">
        <thead>
            <tr>
                <th width="3%"><input type="checkbox" onclick="$('input[name=\'logid\']').prop('checked', this.checked);" style="vertical-align:middle;" /></th>
                <th>标题</th>
                <th>发生时间</th>
                <th>类别</th>
                <th>操作人员</th>
                <%if(!buttonHtmlDicts[2].IsNullOrEmpty()){ %>
                <th style="width:12%">操作<!--这里判断如果有列表按钮要加一列--></th>
                <%} %>
            </tr>
        </thead>
        <tbody>
        <% 
            string pager;
            query = "&appid=" + Request.QueryString["appid"] + "&tabid=" + Request.QueryString["tabid"] + "&s_title=" + s_title;
            System.Data.DataTable logs = new RoadFlow.Platform.Log().GetPagerData(out pager, query, s_title, "", "", "", ""); 
            foreach(System.Data.DataRow dr in logs.Rows)
            {
        %>
            <tr>
                <td><input type="checkbox" name="logid" value="<%=dr["ID"] %>" /></td>
                <td><%=dr["Title"] %></td>
                <td><%=dr["WriteTime"].ToString().ToDateTimeStringS() %></td>
                <td><%=dr["Type"] %></td>
                <td><%=dr["UserName"] %></td>
                <%if(!buttonHtmlDicts[2].IsNullOrEmpty()){ %>
                <td><%=RoadFlow.Platform.Wildcard.FilterWildcard(buttonHtmlDicts[2],"",dr) %><!--这里显示列表按钮--></td>
                <%} %>
            </tr>
        <% 
            }    
        %>
        </tbody>
    </table>
    <div class="buttondiv"><%=pager %></div>
    </form>
    <script>
        var query = '<%=query%>';
        var prevurl = RoadUI.Core.rooturl() + '/Tests/JurisdictionTest.aspx<%=Request.Url.Query%>';
        var tabid = '<%=Request.QueryString["tabid"]%>';
        function edit(id, title)
        {
            var url = '/Platform/WorkFlowRun/SubTableEdit.aspx?secondtableeditform=c561558c-ae0d-4083-893f-751d8d897179&editmodel=1&instanceid=' + (id || "") + "&display=0" + query + "&prevurl=" + prevurl;
            var width = 800;
            var height = 500;
            new RoadUI.Window().open({ url: url, width: width, height: height, opener: window, openerid: tabid, title: title || "新增" });
        }
        function add()
        {
            edit('', '');
        }
        function del(id)
        {
            if (!confirm("您确定要删除该条数据吗?"))
            {
                return false;
            }
            var url = '../Platform/ProgramBuilder/RunDelete.aspx?secondtableeditform=c561558c-ae0d-4083-893f-751d8d897179&editmodel=1&instanceid=' + (id || "") + query + "&prevurl=" + prevurl;
            window.location = url;
            return true;
        }
        function delselect()
        {
            if ($(":checked[name='logid']").size() == 0)
            {
                alert('您没有选择要删除的数据!');
                return false;
            }
            var idArray = [];
            $(":checked[name='logid']").each(function ()
            {
                idArray.push($(this).val());
            });
            del(idArray.join(','));
        }
    </script>
</body>
</html>
