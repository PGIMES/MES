<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Set_Query_Add.aspx.cs" Inherits="WebForm.Platform.ProgramBuilder.Set_Query_Add" %>

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
            <th style="width: 80px;">字段：</th>
            <td><select class="myselect" name="Field" id="Field" onchange="setcontrolname(this.value);">
                <option value=""></option>
                <asp:Literal ID="FieldOptions" runat="server"></asp:Literal>
                </select></td>
        </tr>
        <tr>
            <th style="width: 80px;">显示标题：</th>
            <td><textarea class="mytextarea" style="width:80%;height:60px;" validate="empty" name="ShowTitle" id="ShowTitle" runat="server"></textarea></td>
        </tr>
        <tr>
            <th style="width: 80px;">控件名称：</th>
            <td><input type="text" class="mytext" style="width:300px;" id="ControlName" name="ControlName" runat="server" />
                <input type="hidden" id="ControlHidden" name="ControlHidden" runat="server" />
            </td>
        </tr>
        <tr>
            <th style="width: 80px;">匹配类型：</th>
            <td><select class="myselect" id="Operators" name="Operators">
                <asp:Literal ID="OperatorsOptions" runat="server"></asp:Literal>
                </select></td>
        </tr>
        <tr>
            <th style="width: 80px;">输入类型：</th>
            <td><select class="myselect" id="InputType" name="InputType" onchange="inptyTypeChange(this.value);">
                <asp:Literal ID="InputTypeOptions" runat="server"></asp:Literal>
                </select>
            </td>
        </tr>
        <tr id="ds_select" style="display:none;">
            <th>数据来源：</th>
            <td>
                <table cellpadding="0" cellspacing="0" border="0" width="100%">
                    <tr>
                        <td style="text-align:left; height:35px;">
                            <asp:Literal ID="DataSource" runat="server"></asp:Literal>
                            <span id="DataSource_String_SQL_Link" style="display:none; vertical-align:middle;">数据连接：
                                <select class="myselect" name="DataSource_String_SQL_Link"><option value=""></option><asp:Literal ID="DataSource_String_SQL_LinkOptions" runat="server"></asp:Literal></select></span>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align:left;">
                            <span id="DataSource_String_Span" style="display:none;"><textarea class="mytextarea" id="DataSource_String" name="DataSource_String" runat="server" style="width:99%;height:80px;"></textarea></span>
                            <span id="DataSource_Dict_Span" style="display:none;"><input type="text" class="mydict" id="DataSource_Dict" name="DataSource_Dict" runat="server" style="width:280px;" /></span>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr id="ds_organize" style="display:none;">
            <th>数据来源：</th>
            <td>
                <table cellpadding="0" cellspacing="0" border="0" width="100%">
                    <tr>
                        <td style="text-align:left;">
                            选择范围：<input type="text" runat="server" class="mymember" id="DataSource_Organize_Range" name="DataSource_Organize_Range" style="width:180px;" />
                            选择类型：<input type="checkbox" runat="server" value="1" name="DataSource_Organize_Type_Unit" id="DataSource_Organize_Type_Unit" style="vertical-align:middle;" /><label style="vertical-align:middle;" for="DataSource_Organize_Type_Unit">单位</label>
                            <input type="checkbox" runat="server" value="1" name="DataSource_Organize_Type_Dept" id="DataSource_Organize_Type_Dept" style="vertical-align:middle;" /><label style="vertical-align:middle;" for="DataSource_Organize_Type_Dept">部门</label>
                            <input type="checkbox" runat="server" value="1" name="DataSource_Organize_Type_Station" id="DataSource_Organize_Type_Station" style="vertical-align:middle;" /><label style="vertical-align:middle;" for="DataSource_Organize_Type_Station">岗位</label>
                            <input type="checkbox" runat="server" value="1" name="DataSource_Organize_Type_WorkGroup" id="DataSource_Organize_Type_WorkGroup" style="vertical-align:middle;" /><label style="vertical-align:middle;" for="DataSource_Organize_Type_WorkGroup">工作组</label>
                            <input type="checkbox" runat="server" value="1" name="DataSource_Organize_Type_Role" id="DataSource_Organize_Type_Role" style="vertical-align:middle;" /><label style="vertical-align:middle;" for="DataSource_Organize_Type_Role">角色</label>
                            <input type="checkbox" runat="server" value="1" name="DataSource_Organize_Type_User" id="DataSource_Organize_Type_User" style="vertical-align:middle;" /><label style="vertical-align:middle;" for="DataSource_Organize_Type_User">人员</label>
                            <input type="checkbox" runat="server" value="1" name="DataSource_Organize_Type_More" id="DataSource_Organize_Type_More" style="vertical-align:middle;" /><label style="vertical-align:middle;" for="DataSource_Organize_Type_More">多选</label>
                            <input type="checkbox" runat="server" value="1" name="DataSource_Organize_Type_QueryUsers" id="DataSource_Organize_Type_QueryUsers" style="vertical-align:middle;" /><label style="vertical-align:middle;" for="DataSource_Organize_Type_QueryUsers">查询时转换为人员</label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr id="ds_dict" style="display:none;">
            <th>数据来源：</th>
            <td>
                <table cellpadding="0" cellspacing="0" border="0" width="100%">
                    <tr>
                        <td style="text-align:left;"><input type="text" class="mydict" runat="server" id="DataSource_Dict_Value" name="DataSource_Dict_Value" style="width:280px;" /></td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <th style="width: 80px;">显示样式：</th>
            <td><input type="text" class="mytext" id="Width" name="Width" runat="server" /></td>
        </tr>
        <tr>
            <th style="width: 80px;">显示顺序：</th>
            <td><input type="text" class="mytext" id="Sort" name="Sort" runat="server" /></td>
        </tr>
    </table>
    <div class="buttondiv">
        <asp:Button ID="Button1" runat="server" Text=" 保 存 " CssClass="mybutton" OnClientClick="return new RoadUI.Validate().validateForm(document.forms[0]);" OnClick="Button1_Click"/>
        <input type="button" class="mybutton" onclick="window.location = 'Set_Query.aspx<%=Request.Url.Query%>';" value=" 返 回 " />
    </div>
    </form>
    <script type="text/ecmascript">
        $(function(){
            inptyTypeChange($("#InputType").val());
        });
        function setcontrolname(v)
        {
            $("#ControlName").val(v+"_"+$("#ControlHidden").val());
        }
        function inptyTypeChange(v)
        {
            $("#ds_select").hide();
            $("#ds_organize").hide();
            $("#ds_dict").hide();
            switch(v)
            {
                case "6":
                    $("#ds_select").show();
                    dataSourceChange($(":checked[name='DataSource']").val());
                    break;
                case "7":
                    $("#ds_organize").show();
                    break;
                case "8":
                    $("#ds_dict").show();
                    break;
            }
        }
        function dataSourceChange(v)
        {
            $("#DataSource_String_SQL_Link").hide();
            if("1"==v)
            {
                $("#DataSource_String_Span").show();
                $("#DataSource_Dict_Span").hide();
            }
            else if("3"==v)
            {
                $("#DataSource_String_Span").show();
                $("#DataSource_Dict_Span").hide();
                $("#DataSource_String_SQL_Link").show();
            }
            else if("2"==v)
            {
                $("#DataSource_String_Span").hide();
                $("#DataSource_Dict_Span").show();
            }
        }
    </script>
</body>
</html>
