<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Open_List1.aspx.cs" Inherits="WebForm.Platform.WorkFlowDesigner.Open_List1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <table cellpadding="0" cellspacing="1" border="0" width="99%" align="center">
        <tr>
            <td align="left" height="35">
                名称：<input type="text" class="mytext" style="width:160px;" id="flow_name" runat="server" name="flow_name" />
                <asp:Button ID="Button1" runat="server" CssClass="mybutton" Text=" 查 询 " OnClick="Button1_Click" />
                <input type="button" class="mybutton" onclick="newflow();" value="新建流程" />
                <input type="button" class="mybutton" onclick="ImportFlow();" value="导入流程" />
            </td>
        </tr>
    </table>

    <table class="listtable">
        <thead>
        <tr>
            <th width="38%">流程名称</th>
            <th width="18%">创建时间</th>
            <th width="12%">创建人</th>
            <th width="11%">状态</th>
            <th width="15%" sort="0"></th>
        </tr>
        </thead>
        <tbody>
        <%
        foreach (var flow in flows.OrderBy(p => p.Name))
        {
         %>
            <tr>
                <td><%=flow.Name %></td>
                <td><%=flow.CreateDate.ToDateTimeString() %></td>
                <td><%=busers.GetName(flow.CreateUserID) %></td>
                <td><%=bwf.GetStatusTitle(flow.Status) %></td>
                <td>
                    <a class="editlink" href="javascript:void(0);" onclick="openflow('<%=flow.ID %>','<%=flow.Name %>');return false;">
                        <span style="vertical-align:middle;">编辑</span>
                    </a>
                    <a class="deletelink" href="javascript:void(0);" onclick="delflow('<%=flow.ID %>'); return false;">
                        <span style="vertical-align:middle;">删除</span>
                    </a>
                    <a href="javascript:void(0);" onclick="ExportFlow('<%=flow.ID %>'); return false;">
                        <span style="vertical-align:middle; background:url(../../Images/ico/arrow_medium_right.png) no-repeat;padding-left:18px;">导出</span>
                    </a>
                </td>
            </tr>
        <%}%>       
        </tbody>
    </table>
    <div class="buttondiv">
        <asp:Literal ID="PaerText" runat="server"></asp:Literal>
    </div>
    </form>   
    <script type="text/javascript">
        var frame = null;
        var openerid = '<%=Request.QueryString["openerid"]%>';
        var iframeid = "<%=Request.QueryString["iframeid"]%>";
        $(window).load(function ()
        {
            var iframes = top.frames;
            for (var i = 0; i < iframes.length; i++)
            {
                var fname = "";
                try
                {
                    fname = iframes[i].name;
                }
                catch (e)
                {
                    fname = "";
                }
                if (fname == openerid + "_iframe")
                {
                    frame = iframes[i]; break;
                }
            }
            if (frame == null) return;
        });
        function typechange(type)
        {

        }
        function openflow(id, flowName)
        {
            top.openApp("/Platform/WorkFlowDesigner/Default.aspx?flowid=" + id + "<%=query%>", 0, "编辑" + flowName + "流程", id);
        }
        function newflow()
        {
            top.openApp("/Platform/WorkFlowDesigner/Default.aspx?isnewflow=1<%=query%>", 0, "新建流程", RoadUI.Core.newid(false));
        }
        function delflow(id)
        {
            if (!confirm('您真的要删除流程吗?'))
            {
                return false;
            }
            var url = "UnInstall.ashx?appid=<%=Request.QueryString["openerid"]%>&id=" + id + "&type=1";
            $.ajax({
                url: url, async: false, cache: false, success: function (txt)
                {
                    if ("1" == txt)
                    {
                        alert("删除成功!");
                        window.location = window.location;
                    }
                }
            })
        }
        function ExportFlow(flowID)
        {
            var url = "Export.aspx?flowid=" + flowID + "<%=query%>";
            window.location = url;
        }

        function ImportFlow()
        {
            var url = "/Platform/WorkFlowDesigner/Import.aspx?1=1<%=query%>";
            new RoadUI.Window().open({ title: "导入流程", width: 400, height: 200, url: url, opener: window, openerid: iframeid, resize: false });
        }

    </script>
</body>
</html>