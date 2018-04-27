<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="File_Add.aspx.cs" Inherits="WebForm.Platform.Files.File_Add" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <link href="WebUploader/webuploader.css" rel="stylesheet" />
    <script src="WebUploader/webuploader.min.js"></script>
</head>
<body style="overflow:hidden;">
   
    <div style="margin:6px;">
        <div id="uploader">
            <div>
                <div id="picker" style="float:left;">选择文件</div>

                <button id="ctlBtn" class="uploadbut">开始上传</button>
                <%if ("1" != Request.QueryString["isselect"]){ %>
                <button class="uploadbut" style="margin-left:2px;" onclick="new RoadUI.Window().reloadOpener();new RoadUI.Window().close();">关闭窗口</button>
                <%}else{ %>
                <button class="uploadbut" style="margin-left:2px;" onclick="window.location='List.aspx<%=Request.Url.Query %>';">返回列表</button>
                <%} %>
            </div>
            <div id="thelist" class="filelist"></div>
        </div>
    </div>
   
    <script type="text/javascript">
        $(function ()
        {
            var uploader = WebUploader.create({
                // swf文件路径
                swf: 'WebUploader/Uploader.swf',
                // 文件接收服务端。
                server: 'FileUpload.ashx',
                // 选择文件的按钮。可选。
                // 内部根据当前运行是创建，可能是input元素，也可能是flash.
                pick: '#picker',
                // 不压缩image, 默认如果是jpeg，文件上传前会压缩一把再上传！
                resize: false,
                //选择文件后自动上传
                auto: false,
                //分片上传大文件
                //chunked: true,
                // 只允许选择图片文件。
                //accept: {
                //    title: 'Images',
                //    extensions: 'gif,jpg,jpeg,bmp,png',
                //    mimeTypes: 'image/*'
                //},
                //其他参数
                formData: { "dir": "<%=Request.QueryString["id"]%>", "userid": "<%=CurrentUserID%>" }
            });
            $("#ctlBtn").bind('click', function () { uploader.upload(); });
            // 当有文件被添加进队列的时候
            uploader.on('fileQueued', function (file)
            {
                if (isImgFile(file.name))
                {
                    var $item = $('<div id="' + file.id + '" class="fileitem">' +
                        '<img src="" width="98" height="68" />' +
                        '<div class="fileitemstate"><div class="fileitemstatebg"></div><div class="fileitemstateword">等待上传</div></div>' +
                    '</div>');
                    uploader.makeThumb(file, function (error, src)
                    {
                        if (!error)
                        {
                            $item.find("img").attr("src", src);
                        }
                    }, 98, 68);
                    $("#thelist").append($item);
                }
                else
                {
                    var $item = $('<div id="' + file.id + '" class="fileitem">' +
                        '<div class="fileitemname">' + file.name + '</div>' +
                        '<div class="fileitemstate"><div class="fileitemstatebg"></div><div class="fileitemstateword">等待上传</div></div>' +
                    '</div>');
                    $("#thelist").append($item);
                }

            });

            // 文件上传过程中创建进度条实时显示。
            uploader.on('uploadProgress', function (file, percentage)
            {
                $("#" + file.id).find('.fileitemstateword').text('已上传' + Math.round(percentage * 100) + '%');
            });

            uploader.on('uploadSuccess', function (file, response)
            {
                if (response.error)
                {
                    $("#" + file.id).find('.fileitemstateword').html('<span style="color:red;font-weight:bold;">'+response.error+'</span>');
                }
                else
                {
                    $("#" + file.id).find('.fileitemstateword').text('上传成功');
                }
                //new RoadUI.Window().reloadOpener();
                //new RoadUI.Window().close();
            });
            uploader.on('error', function (type)
            {
                //alert(type);
            });
            uploader.on('uploadError', function (file)
            {
                $("#" + file.id).find('.fileitemstateword').text('上传出错');
            });

            uploader.on('uploadComplete', function (file)
            {
                //$('#' + file.id).find('.progress').fadeOut();
            });
            //所有文件上传完成发生
            uploader.on('uploadFinished', function ()
            {
                
            });
        });
        //判断是否为图片文件
        function isImgFile(fileName)
        {
            if (!fileName || fileName.indexOf('.') < 0)
            {
                return false;
            }
            var fileExtName = fileName.substr(fileName.lastIndexOf('.')).toLowerCase();
            if (fileExtName == ".jpg" || fileExtName == ".png" || fileExtName == ".gif" || fileExtName == ".jpeg" || fileExtName == ".bmp")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    </script>
</body>
</html>
