﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebForm.Controls.UploadFiles.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <% 
    string extName = Request.QueryString["filetype"];
    RoadFlow.Platform.Files BFiles = new RoadFlow.Platform.Files();
    string rootPath = BFiles.GetRootPath();
    %>
    <div id="tabdiv">
        <div id="div_base" title="本地上传">
        <div style="height:8px;"></div>
            <link href="../../Platform/Files/WebUploader/webuploader.css" rel="stylesheet" />
            <script src="../../Platform/Files/WebUploader/webuploader.min.js"></script>
            <table cellpadding="0" cellspacing="1" border="0" width="98%" align="center" style="margin-top:8px;">
                <tr>
                    <td style="height:40px;" id="uploadtable"><div id="picker" style="float:left;">选择文件</div>
                        <div class="uploadpress">
                            <div id="uploadpressdiv"></div>
                        </div>
                    </td>
                    <td align="right" style="padding-right:20px;">
                        <input type="button" class="mybutton" value="&nbsp;确&nbsp;&nbsp;认&nbsp;" onclick="confirm1();" />
                        <input type="button" class="mybutton" value="&nbsp;关&nbsp;&nbsp;闭&nbsp;" onclick="closewin();" />
                    </td>
                </tr>
            </table>
            <div id="queue" style="margin:0 auto 5px 0;"></div>
            <div id="filelist">
                <table cellpadding="0" cellspacing="1" border="0" id="filetable" width="98%" class="listtable" style="width:98%; margin:0 auto;">
                    <thead>
                        <tr>
                            <th style="width:70%">文件</th>
                            <th style="width:20%;">大小</th>
                            <th></th>
                        </tr>
                    </thead>
                    <tbody>
                    <%
                        string files = Request.QueryString["files"];
                        if (!files.IsNullOrEmpty())
                        {
                            string[] filesArray = files.Split('|');
                            foreach (string file in filesArray)
                            {
                                string file1 = file.DesDecrypt();
                                System.IO.FileInfo fi = new System.IO.FileInfo(System.IO.Path.Combine(rootPath, file1));
                                if (!fi.Exists)
                                {
                                    continue;
                                }
                                string id = fi.FullName.DesEncrypt();
                                int type = file1.StartsWith("UploadFiles", StringComparison.CurrentCultureIgnoreCase) ? 0 : 1;
                     %>
                        <tr id="<%=id %>">
                            <td style="background:#ffffff;"><a target="_blank" href="../../Platform/Files/Show.ashx?id=<%=id %>" style="background:url(../../Images/ico/doc_stand.png) no-repeat; padding-left:22px;"><%=fi.Name %></a></td>
                            <td style="background:#ffffff;text-align:right; padding-right:3px;"><%=decimal.Round((fi.Length/1024),0) %>KB</td>
                            <td style="background:#ffffff;"><input type="hidden" name="delfile" value="<%=file %>" /><a class="deletelink" href="javascript:void(0);" onclick="delFile('<%=id %>',<%=type %>);">删除</a></td>
                        </tr>
                     <%
                            }
                        }    
                     %>
                    </tbody>
                </table>
            </div>
	        <script type="text/javascript">
	            var win = new RoadUI.Window();
	            var eid = '<%=Request.QueryString["eid"]%>';
	            var uploadDir = "<%=BFiles.GetUploadPath().DesEncrypt()%>";
	            $(function ()
	            {
	                var uploader = WebUploader.create({
	                    // swf文件路径
	                    swf: '../../Platform/Files/WebUploader/Uploader.swf',
	                    // 文件接收服务端。
	                    server: '../../Platform/Files/FileUpload.ashx',
	                    // 选择文件的按钮。可选。
	                    // 内部根据当前运行是创建，可能是input元素，也可能是flash.
	                    pick: '#picker',
	                    // 不压缩image, 默认如果是jpeg，文件上传前会压缩一把再上传！
	                    resize: false,
	                    //选择文件后自动上传
	                    auto: true,
	                    //分片上传大文件
	                    //chunked: true,
	                    // 只允许选择图片文件。
	                    //accept: {
	                    //    title: 'Images',
	                    //    extensions: 'gif,jpg,jpeg,bmp,png',
	                    //    mimeTypes: 'image/*'
	                    //},
	                    //其他参数
	                    formData: { "dir": uploadDir, "userid": "<%=CurrentUserID%>" }
	                });
	                // 当有文件被添加进队列的时候
	                uploader.on('fileQueued', function (file)
	                {
	                    $(".uploadpress").show();
	                });

	                // 文件上传过程中创建进度条实时显示。
	                uploader.on('uploadProgress', function (file, percentage)
	                {
	                    var width = Math.round(percentage * 100);
	                    $("#uploadpressdiv").text(width + '%').css("width", Math.round(280 / (percentage < 1 ? percentage * 100 : percentage)).toString() + "px");
	                });

	                uploader.on('uploadSuccess', function (file, response)
	                {
	                    if (response.error)
	                    {
	                        alert(response.error);
	                    }
	                    else
	                    {
	                        addFile(file.name, response.id, response.id1, response.size, 0);
	                    }
	                });
	                //所有文件上传完成发生
	                uploader.on('uploadFinished', function ()
	                {
	                    $(".uploadpress").hide();
	                });
	            });
                // id 完整路径 id1 不包含根路径 type(0上传的文件 1我的文件中选择的文件)
	            function addFile(name, id , id1, size, type)
	            {
	                if ($("#" + id).size() > 0)
	                {
	                    return false;
	                }
	                var tr = '<tr id="' + id + '">';
	                tr += '<td style="background:#ffffff;">';
	                tr += '<a href="../../Platform/Files/Show.ashx?id=' + id + '" target="_blank" style="background:url(../../Images/ico/doc_stand.png) no-repeat; padding-left:22px;">';
	                tr += name
	                tr += '</a>';
	                tr += '</td>';
	                tr += '<td style="background:#ffffff;text-align:right; padding-right:3px;">';
	                tr += size
	                tr += '</td>';
	                tr += '<td style="background:#ffffff;">';
	                tr += '<input type="hidden" value="' + id1 + '" name="delfile"/><a class="deletelink" href="javascript:void(0);" onclick="delFile(\'' + id + '\',' + type + ')">删除</a>';
	                tr += '</td>';
	                tr += '</tr>';
	                $("#filetable tbody").append(tr);
	                return false;
	            }
	            function delFile(id)
	            {
	                $("#" + id).remove();
	            }
	            function checkallbox(box)
	            {
	                $("input[name='delfile']").prop("checked", $(box).prop("checked"));
	            }
	            function confirm1()
	            {
	                var title = [];
	                var value = [];
	                $("#filetable tbody tr").each(function ()
	                {
	                    var filename = $("td:eq(0)", $(this)).text();
	                    var filepathname = $("input[name='delfile']", $(this)).val();
	                    title.push(filename);
	                    value.push(filepathname);
	                });
	                var ele = win.getOpenerElement(eid);
	                var ele1 = win.getOpenerElement(eid + "_text");
	                if (ele1 != null && ele1.size() > 0)
	                {
	                    if (value.length == 0)
	                    {
	                        ele1.val('');
	                    }
	                    else
	                    {
	                        ele1.val('共' + value.length + '个文件');
	                    }
	                }
	                if (ele != null && ele.size() > 0)
	                {
	                    ele.val(value.join('|'));
	                }
	                else
	                {
	                    ele.val('');
	                }
	                closewin();
	            }
	            function closewin()
	            {
	                try
	                {
	                    $('#file_upload').uploadify('destroy');
	                } catch (e)
	                {

	                }
	                win.close();
	            }
	            function delselect()
	            {
	                
	            }
	        </script>

        </div>
        <div id="div_title" title="选择我的文件"> 
            <iframe src="../../Platform/Files/Default.aspx?isselect=1<%=query %>" style="border:0;width:100%;height:420px;margin:0;padding:0;" frameborder="0" scrolling="auto" ></iframe>
        </div>
    </div>
    </form>

    <script type="text/javascript">
        $(function ()
        {
            new RoadUI.Tab({ id: "tabdiv", replace: true, contextmenu: false, dblclickclose: false });
        });
    </script>
</body>
</html>
