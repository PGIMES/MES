<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Set_Attr.aspx.cs" Inherits="WebForm.Platform.ProgramBuilder.Set_Attr" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <table cellpadding="0" cellspacing="1" border="0" width="99%" class="formtable" style="margin-top:10px;">
        <tr>
            <th style="width: 80px;">应用名称：</th>
            <td><input type="text" id="Title1" name="Title1" class="mytext" runat="server" validate="empty" style="width: 85%"/></td>
        </tr>
        <tr>
            <th style="width: 80px;">应用分类：</th>
            <td><select class="myselect" name="Type" validate="empty">
                <asp:Literal ID="TypeOptions" runat="server"></asp:Literal>
                </select>
                &nbsp;&nbsp;数据连接：<select class="myselect" name="DBConnID">
                    <asp:Literal ID="DbConnOptions" runat="server"></asp:Literal>
                                 </select>
                &nbsp;&nbsp;按钮显示位置：<select class="myselect" name="ButtonLocation" id="ButtonLocation" runat="server">
                    <option value="0">新行</option>
                    <option value="1">查询后面</option>
                                   </select>
                &nbsp;&nbsp;是否分页：<select class="myselect" name="IsPager">
                    <asp:Literal ID="IsPagerOptions" runat="server"></asp:Literal>
                                 </select>
                &nbsp;&nbsp;列表样式：<select class="myselect" name="TableStyle" id="TableStyle" runat="server">
                    <option value="listtable">常规样式</option>
                    <option value="reporttable">报表样式</option>
                                   </select>
            </td>
        </tr>
        <tr>
            <th>表单：</th>
            <td>
                <select class="myselect" style="width:130px; max-height:200px;" onchange="form_types_change(this.value);" id="form_types">
                    <option value=""></option>
                    <asp:Literal ID="TypeOptions1" runat="server"></asp:Literal>
                </select>
                <select class="myselect" id="form_forms" name="form_forms" runat="server"></select>
                &nbsp;&nbsp;编辑方式：<select id="form_editmodel" onchange="form_editmodel_change(this.value)" class="myselect" name="form_editmodel" runat="server">
                    <option value="0">当前窗口</option>
                    <option value="1">弹出层</option>
                                 </select> 
                <span id="form_editmodel_span">&nbsp;&nbsp;弹出层宽度：<input type="text" class="mytext" id="form_editmodel_width" name="form_editmodel_width" runat="server" style="width:60px" />
                    高度：<input type="text" id="form_editmodel_height" name="form_editmodel_height" runat="server" class="mytext" style="width:60px" />
                </span>
            </td>
        </tr>
        <tr>
            <th style="width: 80px;">查询SQL：</th>
            <td><textarea style="width:99%; height:280px; font-family:Verdana; font-size:14px;" id="sql" name="sql" validate="empty" class="mytextarea" runat="server"></textarea></td>
        </tr>
        <tr>
            <th style="width: 80px;">页面脚本：</th>
            <td><textarea style="width:99%; height:100px; font-family:Verdana; font-size:14px;" id="ClientScript" name="ClientScript" class="mytextarea" runat="server"></textarea></td>
        </tr>
        <tr>
            <th style="width: 80px;">列表表头：</th>
            <td><textarea style="width:99%; height:100px; font-family:Verdana; font-size:14px;" id="TableHead" name="TableHead" class="mytextarea" runat="server"></textarea></td>
        </tr>
        <tr>
            <th style="width: 80px;">导出：</th>
            <td>模板：<input type="text" id="ExportTemplate" name="ExportTemplate" class="myfile" runat="server" />
                &nbsp;&nbsp;表头：<input type="text" class="mytext" id="ExportHeaderText" name="ExportHeaderText" runat="server" style="width:400px" />
                &nbsp;&nbsp;文件名：<input type="text" class="mytext" id="ExportFileName" name="ExportFileName" runat="server" style="width:200px" />
            </td>
        </tr>
    </table>
    <div class="buttondiv">
        <asp:Button ID="Button1" runat="server" Text=" 保 存 " OnClientClick="return new RoadUI.Validate().validateForm(document.forms[0]);" CssClass="mybutton" OnClick="Button1_Click" />
    </div>
    </form>
    <script type="text/javascript">
        $(function ()
        {
            form_types_change($("#form_types").val());
            form_editmodel_change($("#form_editmodel").val());
        });
        function form_types_change(value)
        {
            $.ajax({
                url: "../Menu/GetApps.ashx", data: { type: value }, async: false, type: "post", success: function (txt)
                {
                    $("#form_forms").html('<option value=""></option>' + txt);
                    $("#form_forms").val('<%=formid%>');
                }
            });
        }
        function form_editmodel_change(value)
        {
            if ("1" == value)
            {
                $("#form_editmodel_span").show();
            }
            else
            {
                $("#form_editmodel_span").hide();
            }
        }
    </script>
</body>
</html>
