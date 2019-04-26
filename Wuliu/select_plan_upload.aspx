<%@ Page Language="C#" AutoEventWireup="true" CodeFile="select_plan_upload.aspx.cs" Inherits="CapacityPlan_select_plan_upload" %>
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
             if (e.callbackData) {
                 layer.alert("上传失败！");
             } else {
                 layer.alert("上传成功！");
                 //parent.location.reload(); // 父页面刷新
                 //window.opener.grid.PerformCallback(e.callbackData);
             }
         }

    </script>
</head>
<body>
    <form id="form1" runat="server">

    <div class="col-md-12  ">
        <div class="row row-container">
            <table style="margin:10px 10px;">
               <%-- <tr style="height:50px;">
                    <td>
                       <asp:DropDownList ID="ddl_comp" runat="server" Width="110px"  class="form-control" AutoPostBack="true">
                            <asp:ListItem Value="200">昆山工厂</asp:ListItem>
                            <asp:ListItem Value="100">上海工厂</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>--%>
                <tr>
                    <td>
                        <dx:aspxuploadcontrol ID="uploadcontrol" runat="server" Width="450px" BrowseButton-Text="浏览"  Visible="true" ClientInstanceName="UploadControl" 
                            ShowAddRemoveButtons="True" RemoveButton-Text="删除" UploadMode="Advanced"   AutoStartUpload="true" ShowUploadButton="false" ShowProgressPanel="true"
                            onfileuploadcomplete="uploadcontrol_FileUploadComplete" >
                            <AdvancedModeSettings EnableDragAndDrop="True" EnableFileList="True" EnableMultiSelect="false"></AdvancedModeSettings>
                            <ValidationSettings AllowedFileExtensions=".xls,.xlsx"></ValidationSettings>
                            <ClientSideEvents FileUploadComplete="onFileUploadComplete" /> 
                        </dx:aspxuploadcontrol>
                    </td>
                </tr>
                <tr>
                    <td>
                        <dx:ASPxLabel ID="lbl_msg" runat="server" Text=""></dx:ASPxLabel>
                    </td>
                </tr>
            </table>
        </div>
    </div>


    </form>
</body>
</html>
