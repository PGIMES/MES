﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebForm.Controls.SelectIco.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <style type="text/css">
        .fileItem1 {text-align:center; float:left; display:inline-block; margin:5px 7px 5px 7px; cursor:pointer; width:93px; height:40px; overflow:hidden;}
        .fileItem1 span {line-height:25px; padding:2px 2px 2px 2px; -moz-user-select:none; color:#555;}
        .fileItem2 {text-align:center; }
        .fileItem2 span {background:#ccc; padding:2px 2px 2px 2px; color:#fff;}
        .fontItem1 { }
        .fontItem1 span { color:#333; }
        .fontItem2 {text-align:left; }
        .fontItem2 span {background:#ccc; padding:2px 2px 2px 2px; color:#fff;}
        .fontItem2 i { color:red;}
    </style>
</head>
<body style="padding:0px; overflow:hidden;">
    <div id="tabdiv">
        <div id="div_font" title="字体图标">
            <div style="height:474px;overflow:auto; padding:10px;">
             <table width="100%" cellpadding="0" cellspacing="0" border="0" align="center">
                <tr>
                    <td>
                        <% 
                            RoadFlow.Platform.Dictionary bdict = new RoadFlow.Platform.Dictionary();
                            var icotypes = bdict.GetChilds("fontawesome");  
                            foreach(var type in icotypes)
                            {  
                        %>
                        <div>
                            <div style="font-size:14px; font-weight:bold; margin-top:6px;"><%=type.Title %></div>
                            <div style="border:0px solid #ccc; padding-top:6px; overflow:auto; width:100%; text-align:center;">
                            <table width="100%" cellpadding="0" cellspacing="0" border="0">
                            <% 
                                var icons = bdict.GetChilds(type.ID).OrderBy(p=>p.Title).ToList();
                                for (int j = 0; j < icons.Count; j++)
                                {
                            %>
                                <tr style="">
                                    <%for (int i = 0; i < 5; i++)
                                      {
                                          j++;
                                          if (j >= icons.Count)
                                          {
                                              if (i > 0)
                                              {
                                                  for (int m = 0; m < 5 - i; m++)
                                                  {
                                                      Response.Write("<td style='width:20%;'></td>");
                                                  }
                                              }
                                              break;
                                          }
                                          var icon = icons[j];      
                                    %>
                                    <td style="text-align:left; width:20%; height:26px; overflow:hidden;">
                                        <div class="fontItem1" data-type="icon" style="cursor:pointer;" onclick="$('[data-type=\'icon\']',$('#div_font')).removeClass('fontItem2');$(this).addClass('fontItem2');curSelectName='<%=icon.Title%>';curSelectPath='<%=icon.Title%>';curType=1;">
                                            <i class="fa <%=icon.Title%>" style="font-size:16px; margin-right:4px;"></i>
                                            <span><%=icon.Title%></span>
                                        </div>
                                    </td>
                                    <%
                                          
                                    } %>
                                </tr>
                            <%} %>
                            </table>
                            </div>
                        </div>
                        <%} %>
                    </td>
                </tr>
            </table>
            </div>
        </div>
        <div id="div_img" title="图片图标">
            <table width="100%" cellpadding="0" cellspacing="0" border="0" align="center">
                <tr>
                    <td>
                        <div id="selectList" style="padding-top:6px; overflow:auto; height: 488px; width:100%; text-align:center;"></div>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <div class="buttondiv">
        <input type="button" id="btnOK" onclick="OK_Click();" value=" 确 定 " class="mybutton" />
        <input type="button" id="btnCancel" onclick="new RoadUI.Window().close();" value=" 取 消 " class="mybutton" />
    </div>
    <script type="text/javascript">
        var path = '<%=Request.QueryString["source"]%>';
        var id = '<%=Request.QueryString["id"]%>';
        var curSelectName = '';
        var curSelectPath = '';
        var curType = 0;
        var icoTree = null;
        $(function ()
        {
            new RoadUI.Tab({ id: "tabdiv", replace: true, contextmenu: false, dblclickclose: false });
            getFiles(path);
        });
        function OK_Click()
        {
            var win = new RoadUI.Window();
            var ele = win.getOpenerElement(id);
            if (ele != null && ele.size() > 0)
            {
                $(ele).val(curSelectPath);
                if (1 == curType)
                {
                    $(ele).css({ "padding-left": "3px", "background-image": ""});
                    //$(ele).text('<i class="fa ' + curSelectPath + '"></i>');
                    //$(ele).before();
                }
                else
                {
                    $(ele).css({ "background-image": "url(" + RoadUI.Core.rooturl() + curSelectPath + ")", "padding-left": "23px" });
                }
            }
            win.close();
        }

        function getFiles(folderValue)
        {
            $.ajax({
                type: "get", url: "File.ashx?path=" + folderValue, dataType: "xml", async: true, cache: false,
                success: function (xml) { showFiles(xml); }
            });
        }
        var showFiles = function (xmlDom)
        {
            $element = $("#selectList");
            $element.children().remove();
            if (xmlDom == null || xmlDom.documentElement.childNodes.length == 0)
            {
                return;
            }
            nodeList = xmlDom.documentElement.childNodes;
            for (var i = 0; i < nodeList.length; i++)
            {
                var title = getNodeAtt(nodeList[i], "title");
                var path = getNodeAtt(nodeList[i], "path");
                var path1 = getNodeAtt(nodeList[i], "path1");
                if (path == "") { continue; }

                var html = '<div class="fileItem1" title="' + title + '"'
                + ' onclick="$(\'#selectList\').children().removeClass(\'fileItem2\');$(this).addClass(\'fileItem2\');curSelectName=\'' + title + '\';curSelectPath=\'' + path1 + '\';" '
                + ' ondblclick="OK_Click();" '
                + '><img src="' + path + '" border="0" /><br/><span onselectstart="return false" >' + title + '</span></div>';
                $element.append(html);
            }
        }
        var getNodeAtt = function (node, att)
        {
            try { return $.trim(node.attributes.getNamedItem(att).nodeValue); } catch (e) { return ''; }
        }
    </script>
</body>
</html>
