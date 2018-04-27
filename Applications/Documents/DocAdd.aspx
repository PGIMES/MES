<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DocAdd.aspx.cs" Inherits="WebForm.Applications.Documents.DocAdd" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <script src="../../Scripts/Ueditor/ueditor.config.js"></script>
    <script src="../../Scripts/Ueditor/ueditor.all.min.js"></script>
    <script src="../../Scripts/Ueditor/ueditor.parse.min.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="toolbar" style="margin-top:0; border-top:none 0; position:fixed; top:0; left:0; right:0; margin-left:auto; z-index:999; width:100%; margin-right:auto; height:26px;">
        <a href="javascript:void(0);" onclick="window.location='List.aspx<%=Request.Url.Query %>';return false;"><span style="background-image:url(../../Images/ico/arrow_medium_left.png);">返回列表</span></a>
        <span class="toolbarsplit">&nbsp;</span>
        <asp:LinkButton ID="LinkButton1" OnClientClick="return checkf();" runat="server" OnClick="LinkButton1_Click"><span style="background-image:url(../../Images/ico/save.gif);">保存文档</span></asp:LinkButton>
    </div>
    
    <table cellpadding="0" cellspacing="1" border="0" width="99%;" class="formtable" style="margin-top:36px;">
        <tr>
            <th style="width: 80px;">栏目：</th>
            <td>
                <input type="hidden" name="DirectoryID" id="DirectoryID" value="<%=DirID %>" />
                <%=DocDir.GetName(DirID.ToGuid()) %>
            </td>
        </tr>
        <tr>
            <th style="width: 80px;">标题：</th>
            <td><input type="text" id="Title1" name="Title1" validate="empty" class="mytext" runat="server"  style="width: 85%"/></td>
        </tr>
        <tr>
            <th style="width: 80px;">阅读人员：</th>
            <td><input type="text" id="ReadUsers" name="ReadUsers" class="mymember" runat="server" style="width: 200px;"/>
                &nbsp;&nbsp;来源：<input type="text" class="mytext" id="Source" name="Source" runat="server" />
            </td>
        </tr>
        <tr>
            <th style="width: 80px;">内容：</th>
            <td style="padding-right:4px;">
                <textarea class="mytextarea" id="Contents" name="Contents" validate="editor" runat="server" model="html" style="width:100%;height:300px;"></textarea>
            </td>
        </tr>
        <tr>
            <th style="width: 80px;">附件：</th>
            <td><input type="text" id="Files" name="Files" class="myfile" runat="server" style="width: 85%"/></td>
        </tr>
    </table>
        <br /><br />
    </form>
    <script type="text/javascript">
        function checkf()
        {
            if ($("#Title1").val().trim().length==0)
            {
                alert("标题不能为空!");
                return false;
            }
            if (!UE.getEditor("Contents").hasContents())
            {
                alert("内容不能为空!");
                return false;
            }
        }
    </script>
</body>
</html>
