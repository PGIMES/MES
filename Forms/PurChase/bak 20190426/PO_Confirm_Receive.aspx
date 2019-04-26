<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PO_Confirm_Receive.aspx.cs" Inherits="Select_select_pt_mstr" %>

<%@ Register Assembly="DevExpress.Web.v17.2, Version=17.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>【合同类收货确认】</title>
    <script src="/Content/js/jquery.min.js" type="text/javascript"></script>
    <script src="/Content/js/layer/layer.js"></script>
    <script type="text/javascript">
        var uploadedFiles = [];
        function onFileUploadComplete(s, e) {
            if(e.callbackData) {
                var fileData = e.callbackData.split('|');uploadedFiles.push(fileData);$("#<%=ip_filelist.ClientID%>").val(uploadedFiles.join(";"));
                var fileName = fileData[0],
                    fileUrl = fileData[1],
                    fileSize = fileData[2];                
                var eqno=uploadedFiles.length-1;

                var tbody_tr='<tr id="tr_'+eqno+'"><td Width="260px"><a href="'+fileUrl+'" target="_blank">'+fileName+'</a></td>'
                        +'<td Width="60px">'+fileSize+'</td>'
                        +'<td><span style="color:blue;cursor:pointer" id="tbl_delde" onclick ="del_data(tr_'+eqno+','+eqno+')" >删除</span></td>'
                        +'</tr>';

               $('#tbl_filelist').append(tbody_tr);
            }
        }


        function del_data(a,eno){
            $(a).remove();
            uploadedFiles[eno]=null;
           $("#<%=ip_filelist.ClientID%>").val(uploadedFiles.join(";"));
        }

        function validate() {
            if (ActualReceiveDate_C.GetValue() == "" || ActualReceiveDate_C.GetValue()==null) {
                layer.alert("【收货日期】不可为空");
                return false;
            }
            return true;
        }
        
    </script>
    <style>
        .lineread{
            /*font-size:9px;*/ border:none; border-bottom:1px solid #ccc;
        }
        .linewrite{
            /*font-size:9px;*/ border:none; border-bottom:1px solid #ccc;background-color:#FDF7D9;/*EFEFEF*/
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div>
        <div class="row  row-container">
            <div class="col-md-12" >
                <div class="panel panel-info">
                    <div class="panel-heading" data-toggle="collapse" data-target="#CPXX">
                         <%--<strong>合同类收货确认</strong>--%>
                    </div>
                    <div class="panel-body " id="XSGCS">
                        <div class="col-xs-12 col-sm-12  col-md-12 col-lg-12" style="width:490px;">
                            <div>
                                <table style="width:100%; font-size:12px; line-height:35px;" border="0" id="tblWLLeibie">
                                    <tr>
                                        <td>系统合同号</td>
                                        <td><asp:TextBox ID="txt_SysContractNo" runat="server" ReadOnly="true" CssClass="lineread" Width="120px" Height="27px"></asp:TextBox></td>
                                        <td>域</td>
                                        <td><asp:TextBox ID="txt_domain" runat="server" ReadOnly="true" CssClass="lineread" Width="120px" Height="27px"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td>采购单号</td>
                                        <td><asp:TextBox ID="txt_PoNo" runat="server" ReadOnly="true" CssClass="lineread" Width="120px" Height="27px"></asp:TextBox></td>
                                        <td>采购行号</td>
                                        <td><asp:TextBox ID="txt_rowid" runat="server" ReadOnly="true" CssClass="lineread" Width="120px" Height="27px"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td>总数量</td>
                                        <td><asp:TextBox ID="txt_PurQty" runat="server" ReadOnly="true" CssClass="lineread" Width="120px" Height="27px" Text="1"></asp:TextBox></td>
                                        <td>待收数量</td>
                                        <td><asp:TextBox ID="txt_qty_dsh" runat="server" ReadOnly="true" CssClass="lineread" Width="120px" Height="27px" Text="1"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td><span style="color:red;font-weight:800;">*</span>收货日期</td>
                                        <td>
                                            <dx:ASPxDateEdit ID="ActualReceiveDate" runat="server" EditFormat="Custom" Width="120px" Height="27px" UseMaskBehavior="true" EditFormatString="yyyy/MM/dd"
                                                ClientInstanceName="ActualReceiveDate_C" 
                                                Border-BorderStyle="None" BorderBottom-BorderStyle="Solid" ButtonStyle-BorderBottom-BorderColor="#ccc" BackColor="#FDF7D9" >
                                                <CalendarProperties>
                                                    <FastNavProperties DisplayMode="Inline" />
                                                </CalendarProperties>
                                            </dx:ASPxDateEdit>
                                        </td>
                                        <td>收货数量</td>
                                        <td><asp:TextBox ID="txt_qty" runat="server" ReadOnly="true" CssClass="lineread" Width="120px" Height="27px" Text="1"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td>上传附件</td>
                                        <td colspan="3">
                                            <dx:aspxuploadcontrol ID="uploadcontrol" runat="server" Width="420px" BrowseButton-Text="浏览"  Visible="true" ClientInstanceName="UploadControl" 
                                                 ShowAddRemoveButtons="True" RemoveButton-Text="删除" UploadMode="Advanced"   AutoStartUpload="true" ShowUploadButton="false" ShowProgressPanel="true"
                                                 onfileuploadcomplete="uploadcontrol_FileUploadComplete" >
                                                 <AdvancedModeSettings EnableDragAndDrop="True" EnableFileList="True" EnableMultiSelect="True">
                                                 </AdvancedModeSettings>
                                                 <ClientSideEvents FileUploadComplete="onFileUploadComplete" /> 
                                            </dx:aspxuploadcontrol>
                                            <input type="hidden" id="ip_filelist" name="ip_filelist" runat="server" /> 
                                        </td>
                                    </tr>
                                    <tr>
                                        <td></td>
                                        <td colspan="3">
                                           <table id="tbl_filelist"  Width="380px">  
                                            </table> 
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4" style="text-align:center;">
                                            <asp:Button ID="btn_save" runat="server" Text="确认收货" class="btn btn-large btn-primary" OnClientClick="if(validate()==false)return false;" OnClick="btn_save_Click" Width="100px" /> 
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>       

    </div>
    </form>
</body>
</html>
