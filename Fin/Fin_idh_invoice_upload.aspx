<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Fin_idh_invoice_upload.aspx.cs" Inherits="Fin_Fin_idh_invoice_upload" %>

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
                 layer.alert("上传成功！", function (index) {
                     gv_his.PerformCallback();
                     layer.close(index);
                 });
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
                <tr>
                    <td>
                        <dx:aspxuploadcontrol ID="uploadcontrol" runat="server" Width="630px" BrowseButton-Text="浏览"  Visible="true" ClientInstanceName="UploadControl" 
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
    <div class="col-md-12  ">
        <div class="row  row-container">
            <div class="panel panel-info">
                <div class="panel-heading" data-toggle="collapse" data-target="#CPXX2">
                    <strong>文件记录_待开票</strong>
                </div>
                <div class="panel-body " id="CPXX2">
                    <div class="col-xs-12 col-sm-12  col-md-12 col-lg-12">
                        <dx:ASPxGridView ID="gv_his" runat="server" KeyFieldName="new_filename"
                            AutoGenerateColumns="False" Width="630px" OnPageIndexChanged="gv_his_PageIndexChanged"  ClientInstanceName="gv_his" OnCustomCallback="gv_his_CustomCallback"
                            OnRowDeleting="gv_his_RowDeleting">
                            <SettingsPager PageSize="100" ></SettingsPager>
                            <Settings ShowFilterRow="True" ShowGroupPanel="false" ShowFilterRowMenu="True" ShowFilterRowMenuLikeItem="True" AutoFilterCondition="Contains" 
                                VerticalScrollBarMode="Visible" VerticalScrollBarStyle="Standard" VerticalScrollableHeight="250"  />
                            <SettingsBehavior AllowFocusedRow="false" AllowSelectByRowClick="false"  ColumnResizeMode="Control"/>
                            <Columns>                     
                                <dx:GridViewDataTextColumn Caption="文件名称" FieldName="ori_filename" Width="280px" VisibleIndex="2">
                                    <DataItemTemplate>
                                        <dx:ASPxHyperLink ID="hpl_ori_filename" runat="server" Text='<%# Eval("ori_filename")%>' Cursor="pointer"
                                            NavigateUrl='<%# "/UploadFile/Fin/invoice/"+ Eval("new_filename") %>'  
                                            Target="_blank">                                        
                                        </dx:ASPxHyperLink>
                                    </DataItemTemplate> 
                                    <Settings AllowAutoFilterTextInputTimer="False" />
                                </dx:GridViewDataTextColumn>    
                                <dx:GridViewDataTextColumn Caption="文件名称(新)" FieldName="new_filename" Width="300px"  VisibleIndex="3"></dx:GridViewDataTextColumn>
                                <dx:GridViewCommandColumn ShowDeleteButton="True" Width="50px" VisibleIndex="3" ButtonRenderMode="Image">
                                </dx:GridViewCommandColumn>
                                <dx:GridViewDataTextColumn Caption="createbyid" FieldName="createbyid" VisibleIndex="99" Width="0px"                                
                                 HeaderStyle-CssClass="hidden" CellStyle-CssClass="hidden" FooterCellStyle-CssClass="hidden"></dx:GridViewDataTextColumn>
                            </Columns>
                            <SettingsCommandButton>
                                <DeleteButton>
                                    <Image ToolTip="删除" Url="../Images/ico/cross.png"></Image>
                                </DeleteButton>
                            </SettingsCommandButton>
                            <Styles>
                                <Header BackColor="#99CCFF"></Header>
                                <FocusedRow BackColor="#99CCFF" ForeColor="#0000CC"></FocusedRow>
                                <Footer HorizontalAlign="Right"></Footer>
                            </Styles>
                        </dx:ASPxGridView>
                    </div>
                </div>
            </div>
        </div>
    </div>


    </form>
</body>
</html>
