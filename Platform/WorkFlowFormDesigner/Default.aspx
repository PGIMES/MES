<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebForm.Platform.WorkFlowFormDesigner.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <script src="ueditor/ueditor.config.js"></script>
    <script src="ueditor/ueditor.all.min.js"></script>
    <script src="ueditor/lang/zh-cn/zh-cn.js"></script>
    <script src="ueditor/plugins/plugins.js"></script>
    <style type="text/css">
        .toolbar a span{padding:0 8px; display:inline-block; cursor:default;}
    </style>
    <div class="toolbar" style="margin-top:0; border-top:none 0;">
        <a href="javascript:void(0);" id="newbut" onclick="execCommend('formadd'); return false;" title="新建">
            <span style="background:url(ueditor/plugins/images/new.png) no-repeat center;">&nbsp;</span>
        </a>
        <a href="javascript:void(0);" onclick="execCommend('formopen'); return false;" title="打开">
            <span style="background:url(ueditor/plugins/images/open.png) no-repeat center;">&nbsp;</span>
        </a>
        <a href="javascript:void(0);" onclick="execCommend('formattribute'); return false;" title="属性">
            <span style="background:url(ueditor/plugins/images/setting.gif) no-repeat center;">&nbsp;</span>
        </a>
        <span class="toolbarsplit">&nbsp;</span>
        <a href="javascript:void(0);" onclick="execCommend('formtext'); return false;" title="文本框">
            <span style="background:url(ueditor/plugins/images/input.gif) no-repeat center;">&nbsp;</span>
        </a>
        <a href="javascript:void(0);" onclick="execCommend('formtextarea'); return false;" title="文本域">
            <span style="background:url(ueditor/plugins/images/textarea.gif) no-repeat center;">&nbsp;</span>
        </a>
        <a href="javascript:void(0);" onclick="execCommend('formhtml'); return false;" title="Html编辑器">
            <span style="background:url(ueditor/plugins/images/html.gif) no-repeat center;">&nbsp;</span>
        </a>
        <a href="javascript:void(0);" onclick="execCommend('formradio'); return false;" title="单选按钮组">
            <span style="background:url(ueditor/plugins/images/radio.gif) no-repeat center;">&nbsp;</span>
        </a>
        <a href="javascript:void(0);" onclick="execCommend('formcheckbox'); return false;" title="复选按钮组">
            <span style="background:url(ueditor/plugins/images/checkbox.gif) no-repeat center;">&nbsp;</span>
        </a>
        <a href="javascript:void(0);" onclick="execCommend('formhidden'); return false;" title="隐藏域">
            <span style="background:url(ueditor/plugins/images/hidden.gif) no-repeat center;">&nbsp;</span>
        </a>
        <a href="javascript:void(0);" onclick="execCommend('formselect'); return false;" title="下拉列表框">
            <span style="background:url(ueditor/plugins/images/select.gif) no-repeat center;">&nbsp;</span>
        </a>
        <a href="javascript:void(0);" onclick="execCommend('formlabel'); return false;" title="Label标签">
            <span style="background:url(ueditor/plugins/images/label.gif) no-repeat center;">&nbsp;</span>
        </a>
        <a href="javascript:void(0);" onclick="execCommend('formbutton'); return false;" title="按钮">
            <span style="background:url(ueditor/plugins/images/button.gif) no-repeat center;">&nbsp;</span>
        </a>
        <a href="javascript:void(0);" onclick="execCommend('formcombox'); return false;" title="下拉组合框">
            <span style="background:url(ueditor/plugins/images/combox.png) no-repeat center;">&nbsp;</span>
        </a>
        <a href="javascript:void(0);" onclick="execCommend('formsumtext'); return false;" title="计算字段">
            <span style="background:url(ueditor/plugins/images/sum.png) no-repeat center;">&nbsp;</span>
        </a>
        <a href="javascript:void(0);" onclick="execCommend('formorg'); return false;" title="组织机构选择框">
            <span style="background:url(ueditor/plugins/images/org.gif) no-repeat center;">&nbsp;</span>
        </a>
        <a href="javascript:void(0);" onclick="execCommend('formdictionary'); return false;" title="左右选择框">
            <span style="background:url(ueditor/plugins/images/selectlr.png) no-repeat center;">&nbsp;</span>
        </a>
        <a href="javascript:void(0);" onclick="execCommend('formdatetime'); return false;" title="日期时间选择">
            <span style="background:url(ueditor/plugins/images/datetime.gif) no-repeat center;">&nbsp;</span>
        </a>
        <a href="javascript:void(0);" onclick="execCommend('formfiles'); return false;" title="附件上传">
            <span style="background:url(ueditor/plugins/images/attachment.gif) no-repeat center;">&nbsp;</span>
        </a>
        <a href="javascript:void(0);" onclick="execCommend('formsubtable'); return false;" title="子表">
            <span style="background:url(ueditor/plugins/images/subtable.gif) no-repeat center;">&nbsp;</span>
        </a>
        <a href="javascript:void(0);" onclick="execCommend('formgrid'); return false;" title="数据表格">
            <span style="background:url(ueditor/plugins/images/database_info.gif) no-repeat center;">&nbsp;</span>
        </a>
        <a href="javascript:void(0);" onclick="execCommend('formsignature'); return false;" title="签章">
            <span style="background:url(ueditor/plugins/images/edit.gif) no-repeat center;">&nbsp;</span>
        </a>
        <a href="javascript:void(0);" onclick="execCommend('formselectdiv'); return false;" title="弹出层选择">
            <span style="background:url(ueditor/plugins/images/selectdiv.gif) no-repeat center;">&nbsp;</span>
        </a>
        <a href="javascript:void(0);" onclick="execCommend('formserialnumber'); return false;" title="流水号">
            <span style="background:url(ueditor/plugins/images/formserialnumber.png) no-repeat center;">&nbsp;</span>
        </a>
   
        <span class="toolbarsplit">&nbsp;</span>
        <a href="javascript:void(0);" onclick="execCommend('formsave'); return false;" title="保存当前表单">
            <span style="background:url(ueditor/plugins/images/save.gif) no-repeat center;">&nbsp;</span>
        </a>
        <a href="javascript:void(0);" onclick="execCommend('formsaveas'); return false;" title="表单另存为">
            <span style="background:url(ueditor/plugins/images/saveas.gif) no-repeat center;">&nbsp;</span>
        </a>
        <a href="javascript:void(0);" onclick="execCommend('formcompile'); return false;" title="发布表单">
            <span style="background:url(ueditor/plugins/images/page_code.png) no-repeat center;">&nbsp;</span>
        </a>
    </div>
    <div id="editordiv" style="overflow:auto;">
        <script id="editor" name="editor" type="text/plain" style="width:98%; height:300px;"></script>
    </div>
    <script type="text/javascript">
        var formattributeJSON = { hasEditor: "0" };
        var formsubtabs = [];
        var formEvents = [];
        var myUE = null;
        var formid = "<%=Request.QueryString["formid"]%>";
        var isnewform = "<%=Request.QueryString["isnewform"]%>";
        $(function ()
        {
            resize();
            //$(window).bind('resize', function () { resize(); });
        });
        
        function resize()
        {
            $("#editor").height($(window).height() - 140);
            $("#editor").width($(window).width() - 2);
            myUE = UE.getEditor('editor', { wordCount: false, maximumWords: 1000000000, autoHeightEnabled: false });
            myUE.addListener('ready', function (editor)
            {
                if (formid.length > 0)
                {
                    openform(formid);
                }
            });
        }

        function execCommend(method)
        {
            myUE.execCommand(method);
        }

        function openform(id)
        {
            $.ajax({
                url: "GetHtml.ashx?id=" + id, async: false, cache: false,
                success: function (html)
                {
                    //$("#editor").html(html);
                    myUE.setContent(html, false);
                    //myUE = UE.getEditor('editor', { wordCount: false, maximumWords: 1000000000, autoHeightEnabled: false });
                }
            });
            $.ajax({
                url: "GetAttribute.ashx?id=" + id, dataType: "JSON", async: false, cache: false,
                success: function (json)
                {
                    formattributeJSON = json;
                }
            });
            $.ajax({
                url: "Getsubtable.ashx?id=" + id, dataType: "JSON", async: false, cache: false,
                success: function (json)
                {
                    formsubtabs = json;
                }
            });
            $.ajax({
                url: "GetEvents.ashx?id=" + id, dataType: "JSON", async: false, cache: false,
                success: function (json)
                {
                    formEvents = json;
                }
            });
        }

        
    </script>
</body>
</html>
