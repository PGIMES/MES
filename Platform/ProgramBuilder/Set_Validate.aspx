<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Set_Validate.aspx.cs" Inherits="WebForm.Platform.ProgramBuilder.Set_Validate" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div style="margin:5px 0; padding-left:5px;">
            将所有字段设置为：<select class="myselect" onchange="$('select[name^=\'valdate_\']').val(this.value);"><option></option><%=getValidateOptions(-1) %></select>
        </div>
        <table class="listtable">
            <thead>
                <tr>
                    <th style="width:25%;">表名</th>
                    <th style="width:25%;">字段名</th>
                    <th style="width:30%;">说明</th>
                    <th style="width:20%;">验证类型</th>
                </tr>
            </thead>
            <tbody>
                <%
                foreach(var validate in validateList)
                { 
                %>            
                <tr>
                    <td><%=validate.TableName %></td>
                    <td><%=validate.FieldName %></td>
                    <td><%=validate.FieldNote %></td>
                    <td><select class="myselect" name="valdate_<%=validate.TableName %>_<%=validate.FieldName %>"><%=getValidateOptions(validate.Validate) %></select></td>
                </tr>
                <%}%>    
            </tbody>
        </table>
        <div class="buttondiv">
            <asp:Button ID="Button1" runat="server" CssClass="mybutton" Text="保存设置" OnClick="Button1_Click" />
        </div>
    </form>
</body>
</html>
