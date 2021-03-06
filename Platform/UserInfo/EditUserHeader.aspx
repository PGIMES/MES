﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditUserHeader.aspx.cs" Inherits="WebForm.Platform.UserInfo.EditUserHeader" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <link href="../Files/WebUploader/webuploader.css" rel="stylesheet" />
    <script src="../Files/WebUploader/webuploader.min.js"></script>
    <div id="div_headimg" style="width:98%; margin:8px auto 0 auto;" title="&nbsp;&nbsp;头像&nbsp;&nbsp;">
            <table cellpadding="0" cellspacing="1" border="0" width="96%" style="margin-top:15px;">
                <tr>
                    <td style="width: 140px;vertical-align:middle;"><div id="picker" style="float:left;">选择文件</div>
                        <div class="uploadpress">
                            <div id="uploadpressdiv"></div>
                        </div>
                    </td>
                    <td style="vertical-align:top;">
                        <div id="queue111" style="margin:0 auto 5px 0; display:none;"></div>
                        <input type="button" class="mybutton" style="height:32px;width:80px;" value="保存头像" onclick="saveimg();" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <input type="hidden" id="x" name="x" />
                        <input type="hidden" id="y" name="y" />
                        <input type="hidden" id="x2" name="x2" />
                        <input type="hidden" id="y2" name="y2" />
                        <input type="hidden" id="w" name="w" />
                        <input type="hidden" id="h" name="h" />
                        <img src="" id="HeadImg" name="HeadImg" alt="" runat="server" />
                        <div class="clearfix"></div>
                    </td>
                </tr>
            </table>
        </div>
    </form>
    <script src="js/jquery.Jcrop.min.js"></script>
    <link href="css/jquery.Jcrop.min.css" rel="stylesheet" />
    
    <script type="text/javascript">
        var uploadDir = "<%=new RoadFlow.Platform.Files().GetUploadPath().DesEncrypt()%>";
        $(function ()
        {
            var api;
            $('#HeadImg').Jcrop({
                onChange: showCoords,
                onSelect: showCoords
            }, function ()
            {
                api = this;
            });

            var uploader = WebUploader.create({
                // swf文件路径
                swf: '../Files/WebUploader/Uploader.swf',
                // 文件接收服务端。
                server: '../Files/FileUpload.ashx',
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
                accept: {
                    title: 'Images',
                    extensions: 'jpg,jpeg,bmp,png',
                    mimeTypes: 'image/*'
                },
                //其他参数
                formData: { "dir": uploadDir, "userid": "<%=CurrentUserID%>" }
            });

            uploader.on('uploadSuccess', function (file, response)
            {
                if (response.error)
                {
                    alert(response.error);
                }
                else
                {
                    $('#HeadImg').attr("src", "../Files/Show.ashx?id=" + response.id + "");
                    $('#HeadImg').attr("data-img", response.id);
                    api.setImage("../Files/Show.ashx?id=" + response.id + "");
                    api.setSelect([0, 0, 200, 200]);
                    api.ui.selection.addClass('jcrop-selection');
                }
            });
        });

        function showCoords(c)
        {
            $('#x').val(c.x);
            $('#y').val(c.y);
            $('#x2').val(c.x2);
            $('#y2').val(c.y2);
            $('#w').val(c.w);
            $('#h').val(c.h);
        }
        function saveimg()
        {
            var imgdata = { "x": $('#x').val(), "y": $('#y').val(), "x2": $('#x2').val(), "y2": $('#y2').val(), "w": $('#w').val(), "h": $('#h').val(), "img": $('#HeadImg').attr("data-img") };
            if (imgdata.x.length == 0 || imgdata.y.length == 0 || imgdata.w.length == 0 || imgdata.h.length == 0 || imgdata.img.length == 0)
            {
                alert('请选择图片!');
                return;
            }
            $.ajax({
                url: "SaveUserHead.ashx", data: imgdata, type: "post", success: function (txt)
                {
                    alert(txt);
                }
            });
        }
    </script>
</body>
</html>
