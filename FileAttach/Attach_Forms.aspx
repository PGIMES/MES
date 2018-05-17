<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Attach_Forms.aspx.cs" Inherits="FileAttach_Attach_Forms" MaintainScrollPositionOnPostback="True" %>

<%@ Register assembly="DevExpress.Web.v17.2, Version=17.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web" tagprefix="dx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <%--
        lpname:文件夹路径，存取路径为：UploadFile\lpname\formno\文件名称
        formid:表单网页名称路径
        stepid:步骤id，申请发送前apply，其他步骤为步骤的guid
        formno:表单编号 唯一码的no
        option:view,edit
    --%>

    <link href="../../Content/css/bootstrap.min.css" rel="stylesheet" />
    <script src="../../Content/js/jquery.min.js"></script>
    <script src="../../Content/js/bootstrap.min.js"></script>
    <script src="../Content/js/plugins/layer/layer.min.js" type="text/javascript"></script>

    <script type="text/javascript">
       var uploadedFiles = [];
        function onFileUploadComplete(s, e) {
            if (e.callbackData) {
                grid.PerformCallback(e.callbackData);
            }
        }
        
        
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div class="row row-container" style="margin:5px 15px 5px 15px;">
            <div class="panel panel-info">
                <div class="panel-heading" data-toggle="collapse" data-target="#FJSC">
                    <strong>文件上传，预览</strong>
                </div>
                <div class="panel-body collapse in" id="FJSC">
                    <table Width="540px">
                        <tr>
                            <td Width="490px">
                                <dx:aspxuploadcontrol ID="uploadcontrol" runat="server" Width="490px" BrowseButton-Text="浏览"  Visible="true" ClientInstanceName="UploadControl" 
                                        ShowAddRemoveButtons="True" RemoveButton-Text="删除" UploadMode="Advanced"   AutoStartUpload="true" ShowUploadButton="false" ShowProgressPanel="true"
                                        onfileuploadcomplete="uploadcontrol_FileUploadComplete" >
                                        <AdvancedModeSettings EnableDragAndDrop="True" EnableFileList="True" EnableMultiSelect="True">
                                        </AdvancedModeSettings>
                                        <ClientSideEvents FileUploadComplete="onFileUploadComplete" /> 
                                </dx:aspxuploadcontrol>
                            </td>
                            <td Width="40px"><dx:ASPxButton ID="btn_del" runat="server" Text="删除" OnClick="btn_del_Click"></dx:ASPxButton></td>
                        </tr>
                    </table>                   
                    <br />
                    <dx:ASPxGridView ID="gv_list" runat="server" KeyFieldName="rownum" ClientInstanceName="grid" AutoGenerateColumns="False" Width="540px" OnHtmlDataCellPrepared="gv_list_HtmlDataCellPrepared">
                        <SettingsBehavior AllowDragDrop="TRUE" AllowFocusedRow="True" AllowSelectByRowClick="True" ColumnResizeMode="Control"  SortMode="Value" />
                        <SettingsPager PageSize="100"></SettingsPager>
                        <Settings ShowFooter="True" />
                        <Columns>      
                            <dx:GridViewCommandColumn ShowSelectCheckbox="true" VisibleIndex="0" Width="40px">
                                <HeaderTemplate>
                                    <dx:ASPxCheckBox ID="DchkAll" runat="server" ClientSideEvents-CheckedChanged="function(s,e){grid.SelectAllRowsOnPage(s.GetChecked());}">
                                    </dx:ASPxCheckBox>
                                </HeaderTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                            </dx:GridViewCommandColumn>       
                            <dx:GridViewDataTextColumn Caption="序号"  VisibleIndex="1" FieldName="" Width="50px"></dx:GridViewDataTextColumn>
                            <dx:GridViewDataHyperLinkColumn Caption="文件名称" VisibleIndex="2" FieldName="FilePath" Width="300px">
                                <PropertiesHyperLinkEdit TextField="OriginalFile" />
                            </dx:GridViewDataHyperLinkColumn>
                            <dx:GridViewDataTextColumn Caption="创建时间" VisibleIndex="5"  FieldName="CreateDate" Width="150px"></dx:GridViewDataTextColumn>                  
                        </Columns>
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
    </form>
</body>
</html>
