﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default_App.aspx.cs" Inherits="WebForm.Controls.SelectDictionary.Default_App" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta name="viewport" content="width=device-width,initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=no"/>
    <title></title>
    <style type="text/css">
        .SelectBorder{border:1px solid #cccccc; padding:3px 3px 3px 3px;}
        body { overflow:hidden;}
    </style>
    <script type="text/javascript">
        var win = new RoadUI.Window();
    </script>
</head>
<body>
    <table border="0" cellpadding="0" cellspacing="0" align="center" style="margin-top:4px;">
        <tr>
            <td valign="top">
                <div id="dict" style="width:140px; height:300px; overflow:auto;" class="SelectBorder"></div>
            </td>
            <td align="center" style="padding:0px 6px;" valign="middle">
                <div style="margin-bottom:12px;"><button class="mybutton" onclick="add();">添加</button></div>
                <div style="margin-bottom:12px;"><button class="mybutton" onclick="del();">删除</button></div>
                <div style="margin-bottom:12px;"><button class="mybutton" onclick="confirm1();">确定</button></div>
                <div><button class="mybutton" onclick="win.close();">取消</button></div>
            </td>
            <td valign="top">
                <div id="SelectNote" class="SelectBorder" style="width:100px; height:40px; overflow:auto; margin-bottom:5px;">
                    <span style="color:#ccc;">单击已选择项可显示该项详细信息</span>
                </div>
                <div id="SelectDiv" style="width:100px; height:248px; overflow:auto;" class="SelectBorder">
                   <%=defaultValuesString %>
                </div>
            </td>
        </tr>
    </table>
    <script type="text/javascript">
        var AppID = '<%=Request.QueryString["appid"]%>';
        var ismore = '<%=Request.QueryString["ismore"]%>';//是否可以多选
        var isparent = '<%=Request.QueryString["isparent"]%>';//是否可以选择有子节点的节点
        var ischild = '<%=Request.QueryString["ischild"]%>';//是否加载所有下级节点
        var isroot = '<%=Request.QueryString["isroot"]%>';//是否可以选择根节点
        var root = '<%=Request.QueryString["root"]%>';
        var eid = '<%=Request.QueryString["eid"]%>';
        var datasource = '<%=Request.QueryString["datasource"] ?? "0"%>';
        var roadTree = null;
        $(function ()
        {
            switch (datasource)
            {
                case "0":
                default:
                    roadTree = new RoadUI.Tree({
                        id: "dict", path: "../../Platform/Dictionary/Tree1.ashx?root=" + root + "&ischild=" + ischild,
                        refreshpath: "../../Platform/Dictionary/TreeRefresh.ashx", onclick: click, ondblclick: dblclick
                    });
                    break;
                case "1": //SQL
                    var dbconn = '<%=Request.QueryString["dbconn"]%>';
                    var sql = encodeURI("<%=Request.QueryString["sql"]%>");
                    roadTree = new RoadUI.Tree({
                        id: "dict", path: "GetJson_SQL.ashx?dbconn=" + dbconn + "&sql=" + sql,
                        refreshpath: "", onclick: click, ondblclick: dblclick
                    });
                    break;
                case "2": //URL
                    var url0 = decodeURI("<%=Request.QueryString["url0"]%>");
                    var url1 = decodeURI("<%=Request.QueryString["url1"]%>");
                    roadTree = new RoadUI.Tree({
                        id: "dict", path: url0,
                        refreshpath: url1, onclick: click, ondblclick: dblclick
                    });
                    break;
                case "3": //表
                    var dbconn = encodeURI("<%=Request.QueryString["dbconn"]%>");
                    var dbtable = encodeURI("<%=Request.QueryString["dbtable"]%>");
                    var valuefield = encodeURI("<%=Request.QueryString["valuefield"]%>");
                    var titlefield = encodeURI("<%=Request.QueryString["titlefield"]%>");
                    var parentfield = encodeURI("<%=Request.QueryString["parentfield"]%>");
                    var where = encodeURI("<%=Request.QueryString["where"]%>");

                    roadTree = new RoadUI.Tree({
                        id: "dict", path: "GetJson_Table.ashx?dbconn=" + dbconn + "&dbtable=" + dbtable
                        + "&valuefield=" + valuefield + "&titlefield=" + titlefield + "&parentfield=" + parentfield + "&where=" + where,
                        refreshpath: "GetJson_TableRefresh.ashx?dbconn=" + dbconn + "&dbtable=" + dbtable
                        + "&valuefield=" + valuefield + "&titlefield=" + titlefield + "&parentfield=" + parentfield + "&where=" + where, onclick: click, ondblclick: dblclick
                    });
                    break;
            }
        });

        function click(json)
        {
            current = json;
        }
        function dblclick(json)
        {
            click(json);
            add();
        }
        function add()
        {
            if (!current)
            {
                alert("没有选择要添加的项"); return;
            }

            if (("0" == ismore || "false" == ismore.toLowerCase()) && $("#SelectDiv").children("div").size() >= 1)
            {
                alert("当前设置最多只能选择一项!"); return;
            }

            if ($("#SelectDiv div[value$='" + current.id + "']").size() > 0)
            {
                alert(current.title + "已经选择了!"); return;
            }
            var value = current.id;
            var type = current.type;
            if ("0" == type && "0" == isroot)
            {
                alert("当前设置不允许选择根节点!"); return;
            }
            if ("1" == type && "0" == isparent)
            {
                alert("当前设置不允许选有下级节点的节点!"); return;
            }

            $("#SelectDiv").append('<div onclick="currentDel=this;showinfo(\'' + value + '\');" class="selectorDiv" ondblclick="currentDel=this;del();" value="' + value + '">' + current.title + '</div>');
        }
        function showinfo(id)
        {
            //$.ajax({
            //    url: 'GetNote.ashx?id=' + id, async: true, cache: true, success: function (txt)
            //    {
            //        $("#SelectNote").html(txt);
            //    }
            //});
        }
        function del()
        {
            if (!currentDel)
            {
                alert("没有选择要删除的项");
            }
            $(currentDel).remove();
            window.setTimeout('$("#SelectNote").html(\'<span style="color:#ccc;">单击已选择项可显示该项详细信息</span>\')', 1);
        }
        function confirm1()
        {
            var value = [];
            var title = [];
            var objs = $("#SelectDiv div");
            for (var i = 0; i < objs.size() ; i++)
            {
                value.push(objs.eq(i).attr("value"));
                title.push(objs.eq(i).text());
            }

            var ele = win.getOpenerElement(eid);
            var ele1 = win.getOpenerElement(eid + "_text");
            if (ele1 != null && ele1.size() > 0)
            {
                ele1.val(title.join(','));
            }
            if (ele != null && ele.size() > 0)
            {
                ele.val(value.join(','));
            }
            win.close();
        }
    </script>

</body>
</html>
