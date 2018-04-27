<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DefaultRF.aspx.cs" Inherits="WebForm.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>我的事项-PGI管理系统</title>
    <style type="text/css">
        html,body {overflow:hidden; }
    </style>
    <script src="Scripts/jquery.signalR-1.2.2.min.js"></script>
    <script src="signalr/hubs"></script>
    <script type="text/javascript">
        
        $(function ()
        {
            //var proxy = $.connection.signalRHub;
            //proxy.client.receiveMessage = function (message)
            //{
            //    alert(message);
            //};
            //$.connection.hub.start();
            var connection = $.connection('roadflow');
            connection.start().done(function (){});
            connection.received(function (data)
            {
                showMessage(data);
            });
        });
        function showMessage(data)
        {
            var json = JSON.parse(data);
            var id = json.id || "";
            if (json.title && json.title.length > 0)
            {
                $("#messagetitle").text(json.title);
            }
            var html = '<div>' + json.contents + '</div>';
            if (json.count && json.count > 1)
            {
                html += '<div style="margin-top:8px;"><a class="blue1" href="javascript:void(0);" onclick="openApp(\'/Platform/Info/ShortMessage/NoRead.aspx\',0,\'未读消息\',\'noreadmessage\');closeMessage(\'' + id + '\');return false;">您还有' + json.count + '条未读消息，点击查看。</a></div>';
            }
            $("#messagecontent").html(html);
            $("#message").hide().slideDown(800);
        }
        function closeMessage(id)
        {
            $("#message").hide(400);
            if (id && id.length > 0)
            {
                $.ajax({ url: "Platform/Info/ShortMessage/UpdateStatus.ashx?id=" + id });
            }
        }
    </script>
</head>
<body>
<div class="homemsgdiv" id="message">
    <div class="homemsgdivtitlediv">
        <div class="homemsgdivtitlediv1"></div>
        <div class="homemsgdivtitlediv1bg">
            <div class="homemsgdivtitlediv1bgtitle" id="messagetitle">消息提醒</div>
            <div class="homemsgdivtitlediv1bgclose" onclick="closeMessage();return false;"></div>
        </div>
    </div>
    <div class="homemsgdivmsg" id="messagecontent">

    </div>
</div>
<form id="form1" runat="server">
<div class="mainTop" style="display:none">
    <div class="mainTopLeft">y</div>
    <div class="mainTopRight">
        <div style="padding-right:10px; padding-top:9px;">
            <table cellpadding="0" cellspacing="0" border="0">
                <tr>
                    <td rowspan="2" valign="middle" style="vertical-align:middle; padding-right:10px;">
                        <img src="" width="38" height="38" id="UserHeadImg" style="vertical-align:middle;border-radius:38px" runat="server" />
                    </td>
                    <td style="text-align:left;">
                        <div>
                            <span>
                                欢迎您：<asp:Literal ID="UserName" runat="server"></asp:Literal>
                            </span>
                            <span style="margin-right:6px;"></span>
                            <span style="margin-right:6px;">今天是：<span id="CurrentDateTimeSpan"><asp:Literal ID="CurrentTime" runat="server"></asp:Literal></span></span>
                            <span style="">主题：</span>
                            <span class="mainTheme_blue" onclick="changeTheme('Blue', true);"></span>
                            <span class="mainTheme_green" onclick="changeTheme('Green', true);"></span>
                            <span class="mainTheme_gray" onclick="changeTheme('Gray', true);"></span>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td style="text-align:left;">
                        <div style="margin-top:6px;">
                            <span style="margin-right:4px;"><a href="http://www.cqroad.cn" class="white" target="_blank">官方网站</a></span>
                            <span style="margin-right:6px;">|</span>
                            <span style="margin-right:6px;"><a href="javascript:void(0);" onclick="openApp('Platform/Home/Default.aspx',0,'首页','index'); return false;" class="white" >平台首页</a></span>
                            <span style="margin-right:6px;">|</span>
                            <span style="margin-right:6px;"><a href="javascript:void(0);" onclick="openApp('Platform/UserInfo/EditPass.aspx',2,'修改密码','index_editpass',500,210); return false;" class="white" >修改密码</a></span>
                            <span style="margin-right:6px;">|</span>
                            <span style="margin-right:4px;"><a href="javascript:void(0);" onclick="if(confirm('您真的要退出系统吗?')){window.location='Logout.ashx';} return false;" class="white" >退出系统</a></span>
                        </div>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <div style="clear:both;"></div>
</div>
<div class="mainDiv">
<table cellpadding="0" cellspacing="0" width="100%" border="0">
    <tr>
        <td class="mainMenutd" id="mainMenutd" style="display:none">
            <div class="menuDiv">
                <div class="menuDivRightTitle">
                    <div style="padding-top:9px !important;"><i class="fa fa-desktop" style="font-size:14px; padding-left:2px;"></i><span style="padding-left:5px;">管理菜单</span></div>
                </div>
                <div class="menuDivRight" style="clear:both;">
                    <div style="padding:5px 1px 1px 4px; overflow:auto;">
                        <div id="treeDiv" style="margin:0; overflow:auto;"></div>
                    </div>
                </div>
            </div>
        </td>
        <td class="mainSplittd" id="mainSplittd" style="display:none">
            <div class="mainSplittdImg">
                <div onclick="switchMenu(this);" class="menuDivRightTitleIco"></div>
            </div>
        </td>
        <td style="vertical-align:top;">
            <div class="tab_top"></div>
            <div id="mainTabDiv" class="mainTabDiv"></div>
        </td>
    </tr>
</table>
</div>
</form>
<script type="text/javascript">
    var mainTab = null;
    var mainTree = null;
    var mainDialog = new RoadUI.Window();
    var currentDateTimeSpan = $("#CurrentDateTimeSpan");
    var userID = '<%=CurrentUserID%>';
    var rootdir = '<%=WebForm.Common.Tools.BaseUrl%>';
    var currentWindow = null;//当前操作窗口对象
    var lastURL = "";//最后操作的页面地址
     
    $(function ()
    {
        $.cookies.set("rootdir", rootdir, { expiresAt: new Date(2099, 1, 1) });
        var windowheight=$(window).height()-64;
        $('#mainTabDiv').height(windowheight-3);
        $('.menuDivLeft').height(windowheight);
        $('.menuDivRight').height(windowheight);
        $('.menuDivRight>div>div').height(windowheight-38);

        $(window).bind('resize', function ()
        {
            var height=$(window).height()-58;
            $('#mainTabDiv').height(height-3);
            $('.menuDivLeft').height(height);
            $('.menuDivRight').height(height);
            $('.menuDivRight>div>div').height(height-38);
            mainTab.topResize(height);
        });

        mainTab = new RoadUI.Tab({ id: "mainTabDiv", replace: true });
        initMenu();
        openApp("/Platform/Home/Default.aspx", 0, "我的待办", "index");

        //初始化主题按钮样式
        var theme = $.cookies.get("theme_platform") || "Blue";
        changeTheme(theme, false);
        //updateInfo();
        //window.setInterval("updateInfo()", 60000);

        //初始显示未读消息
        try
        {
            var noReadMsgJson = JSON.stringify(<%=NoReadMsgJson%>);
            if (noReadMsgJson && noReadMsgJson.length>0)
            {
                showMessage(noReadMsgJson);
            }
        }
        catch (e) { }
    });

    function treeClick(json)
    {
        if (json)
        {
            openApp(json.link, json.model, json.title, json.id, parseInt(json.width), parseInt(json.height), true);
        }
    }

    function openApp(url, model, title, id, width, height, isAppendParams)
    {
        if (!url || url.toString().length == 0)
        {
            return;
        }
        if (!id)
        {
            id = RoadUI.Core.query("tabid", url);
            if (id)
            {
                id = id.replace("tab_", "");
            }
        }
        if (!id)
        {
            id = RoadUI.Core.newid();
        }
        if (width == 0) width = undefined;
        if (height == 0) height = undefined;
        if (isAppendParams)
        {
            url += url.indexOf('?') >= 0 ? "&appid=" + id : "?appid=" + id;
        }
        switch (parseInt(model))
        {
            case 0:
                mainTab.addTab({ id: "tab_" + id.replaceAll('-', ''), title: title, src: url });
                break;
            case 1:
                mainDialog.open({ id: "window_" + id.replaceAll('-', ''), title: title, url: url, width: width || 800, height: height || 460, ismodal: false });
                break;
            case 2:
                mainDialog.open({ id: "window_" + id.replaceAll('-', ''), title: title, url: url, width: width || 800, height: height || 460, ismodal: true });
                break;
            case 3:
                url = $.trim(url).substr(0, 1) == "/" ? rootdir + url : url;
                RoadUI.Core.open(url, width || 800, height || 460, title);
                break;
            case 4:
                url = $.trim(url).substr(0, 1) == "/" ? rootdir + url : url;
                window.showModalDialog(url, null, "dialogWidth=" + (width || 800) + "px;dialogHeight=" + (height || 460) + "px;center=1");
                break;
            case 5:
                url = $.trim(url).substr(0, 1) == "/" ? rootdir + url : url;
                window.open(url);
                break;
        }
    }

    function switchMenu(div)
    {
        var flag="menuDivRightTitleIco"==$(div).attr("class");
        if (flag)
        {
            $("#mainMenutd").hide(200);
            $(div).removeClass().addClass("menuDivRightTitleIco1");
        }
        else
        {
            $("#mainMenutd").show(200);
            $(div).removeClass().addClass("menuDivRightTitleIco");
        }
    }

    function initMenu()
    {
        mainTree = new RoadUI.Tree({
            id: "treeDiv", path: "Platform/Home/Menu.ashx",
            refreshpath: "Platform/Home/MenuRefresh.ashx",
            showroot: false, showline: true, onclick: treeClick
        });
    }

    var isloadTree = false;
    function loadMenu(id, roleID, div)
    {
        if($(div).attr('class')=="menuDivLeft1") 
        {
            return false;
        }
        if(isloadTree) return false;
        isloadTree = true;
        $("#menuDiv0>div,#menuDiv1>div").each(function(){
            $(this).removeClass();
        });
        $(div).removeClass().addClass("menuDivLeft1");
        $("#treeDiv").html('<span class="loadmenu">正在加载...</span>');
        mainTree = new RoadUI.Tree({ id: "treeDiv", path: "Platform/Home/MenuRefresh.ashx?roleid=" + roleID + "&userid=" + userID + "&refreshid=" + id, refreshpath: "Platform/Home/MenuRefresh.ashx?roleid=" + roleID + "&userid=" + userID, showroot: false, showline:true, onclick: treeClick, loadcompleted:changeLoadStatus });
    }

    function changeLoadStatus()
    {
        isloadTree = false;
    }

    function changeTheme(themeName, isCng)
    {
        if (!themeName || themeName.toString().trim().length == 0)
        {
            themeName = $.cookies.get("theme_platform")
        }

        $("span[class^='mainTheme_']").each(function ()
        {
            var cssName = $(this).attr("class");
            $(this).removeClass().addClass(cssName.replace("1", ""));
            
        });
        try
        {
            themeName=themeName.toLowerCase();
            var current=$(".mainTheme_" + themeName)||$(".mainTheme_" + themeName+"1");
            current.removeClass().addClass("mainTheme_" + themeName + "1");
        }
        catch(e){}
        if(isCng)
        {
            RoadUI.Core.allFrames = [];
            RoadUI.Core.getAllFrames();
            for (var i = 0; i < RoadUI.Core.allFrames.length; i++)
            {
                $("#style_style", RoadUI.Core.allFrames[i].document).attr("href", rootdir + "/Themes/" + themeName + "/Style/style.css");
                $("#style_ui", RoadUI.Core.allFrames[i].document).attr("href", rootdir + "/Themes/" + themeName + "/Style/ui.css");
            }
            $.cookies.set("theme_platform", themeName, { expiresAt: new Date(2099, 1, 1) });
        }
    }

    function login()
    {
        openApp(rootdir + "/Login1.aspx?session=1", 1, "用户登录", "login", 400, 230);
    }

    //刷新一个页面
    function refreshPage(tabID)
    {
        if (!tabID)
        {
            tabID = 'tab_index';
        }
        mainTab.refresh(tabID);
        tabID = 'tab_edc4881b0ce1422e9e942808d47559e7';
        mainTab.refresh(tabID);
    }
</script>
</body>
</html>
