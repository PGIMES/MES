<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Set_Export_Add.aspx.cs" Inherits="WebForm.Platform.ProgramBuilder.Set_Export_Add" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <table cellpadding="0" cellspacing="1" border="0" width="99%" class="formtable" style="margin-top:10px;">
        <tr>
            <th style="width: 80px;">字段：</th>
            <td><select class="myselect" name="Field" id="Field">
                <option value=""></option>
                <asp:Literal ID="FieldOptions" runat="server"></asp:Literal>
                </select></td>
        </tr>
        <tr>
            <th style="width: 80px;">列标题：</th>
            <td><textarea class="mytextarea" style="width:80%;height:60px;" validate="empty" name="ShowTitle" id="ShowTitle" runat="server"></textarea></td>
        </tr>
        <tr>
            <th style="width: 80px;">单元格格式：</th>
            <td><select id="DataType" name="DataType" class="myselect">
                <asp:Literal ID="DataTypeOptions" runat="server"></asp:Literal>
                </select>
            </td>
        </tr>
        <tr>
            <th style="width: 80px;">显示类型：</th>
            <td><select id="ShowType" name="ShowType" class="myselect" onchange="showCustomTR(this.value);">
                <asp:Literal ID="ShowTypeOptions" runat="server"></asp:Literal>
                </select>
                <span id="showformatspan" style="margin-left:10px; display:none;">格式：<input type="text" class="mytext" runat="server" id="ShowFormat" name="ShowFormat" style="width:200px" /></span>
            </td>
        </tr>
        <tr>
            <th style="width: 80px;">对齐方式：</th>
            <td><select class="myselect" id="Align" name="Align">
                <asp:Literal ID="AlignOptions" runat="server"></asp:Literal>
                </select>
                &nbsp;&nbsp;列表宽度：<input type="text" class="mytext" name="Width" id="Width" runat="server" style="width:100px" />
                &nbsp;&nbsp;显示顺序：<input type="text" class="mytext" name="Sort" id="Sort" runat="server" style="width:100px" />
            </td>
        </tr>
        <tr id="customtr" style="display:none;">
            <th style="width: 80px;">自定义值：</th>
            <td><textarea class="mytextarea" style="width:99%;height:60px;" id="CustomString" name="CustomString" runat="server"></textarea></td>
        </tr>
    </table>
    </div>
    <div class="buttondiv">
        <asp:Button ID="Button1" runat="server" Text=" 保 存 " CssClass="mybutton" OnClientClick="return new RoadUI.Validate().validateForm(document.forms[0]);" OnClick="Button1_Click"/>
        <input type="button" class="mybutton" onclick="window.location = 'Set_Export.aspx<%=Request.Url.Query%>';" value=" 返 回 " />
    </div>
    </form>
    <script type="text/javascript">
        $(function(){
            $("#ShowType option[value='7']").remove();
            showCustomTR($("#ShowType").val());
        });
        function showCustomTR(v)
        {
            if("6"==v)
            {
                $("#customtr").show();
            }
            else
            {
                $("#customtr").hide();
            }

            if("2"==v || "3"==v)
            {
                $("#showformatspan").show();
            }
            else
            {
                $("#showformatspan").hide();
            }
        }
    </script>
</body>
</html>
