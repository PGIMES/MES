<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PUR_PR_Upload_dj_x.aspx.cs" Inherits="Forms_PurChase_PUR_PR_Upload_dj_x" %>

<%@ Register Assembly="DevExpress.Web.v17.2, Version=17.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <link href="/Content/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <script src="/Content/js/jquery.min.js"></script>
    <script src="/Content/js/bootstrap.min.js" type="text/javascript"></script>
    <script src="/Content/js/layer/layer.js" type="text/javascript"></script>

    <script type="text/javascript">
         var uploadedFiles = [];
         function onFileUploadComplete(s, e) {
             var callbackData = e.callbackData.split("|");
             var isSubmissionExpired = callbackData[0];
             var msg = callbackData[1];

             if (isSubmissionExpired == "Y") {
                 layer.alert("上传失败：<br />" + msg);
             } else {
                 //layer.alert("上传成功！", function (index) {
                 //    parent.grid_dj_x.PerformCallback();
                 //    //window.opener.grid.PerformCallback(e.callbackData);
                 //    layer.close(index);
                 //});
                 parent.grid_dj_x.PerformCallback();   
                 parent.layer.close(parent.layer.index);
             }
         }

    </script>
</head>
<body>
    <form id="form1" runat="server">

    <div class="col-md-12  ">
        <div class="row row-container">
            <table style="margin:10px 10px;">
                <tr>
                    <td><asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/UserGuide/pr_daoju_x_uploadformat.xlsx" Target="_blank">upload format</asp:HyperLink></td>
                </tr>
                <tr>
                    <td>
                        <%--<asp:Label ID="lbl_domain" runat="server" Text="" Visible="false"></asp:Label>--%>
                        <dx:aspxuploadcontrol ID="uploadcontrol" runat="server" Width="430px" BrowseButton-Text="浏览"  Visible="true" ClientInstanceName="UploadControl" 
                            ShowAddRemoveButtons="True" RemoveButton-Text="删除" UploadMode="Advanced"   AutoStartUpload="true" ShowUploadButton="false" ShowProgressPanel="true"
                            onfileuploadcomplete="uploadcontrol_FileUploadComplete" >
                            <AdvancedModeSettings EnableDragAndDrop="True" EnableFileList="True" EnableMultiSelect="false"></AdvancedModeSettings>
                            <ValidationSettings AllowedFileExtensions=".xls,.xlsx"></ValidationSettings>
                            <ClientSideEvents FileUploadComplete="onFileUploadComplete" /> 
                        </dx:aspxuploadcontrol>
                    </td>
                </tr>
            </table>
        </div>
    </div>

    </form>
</body>
</html>
