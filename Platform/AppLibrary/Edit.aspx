<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Edit.aspx.cs" Inherits="WebForm.Platform.AppLibrary.Edit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div id="tabdiv">
        <div id="div_base" title="基本信息">
            <div style="height:8px;"></div>
            <table cellpadding="0" cellspacing="1" border="0" width="99%" class="formtable">
            <tr>
                <th style="width: 80px;">应用名称：</th>
                <td><input type="text" id="Title1" name="Title1" class="mytext" runat="server" validate="empty" style="width: 85%"/></td>
            </tr>
            <tr>
                <th>应用地址：</th>
                <td><textarea id="Address" name="Address" class="mytextarea" validate="empty" style="width: 85%; height:70px;" runat="server"></textarea></td>
            </tr>
            <tr>
                <th>应用分类：</th>
                <td>
                    <select name="Type" id="Type" class="myselect" validate="empty">
                        <asp:Literal ID="TypeOptions" runat="server"></asp:Literal>
                    </select>
                    <span style="msg"></span>
                </td>
            </tr>
            <tr>
                <th>打开方式：</th>
                <td>
                    <select name="OpenModel" id="OpenModel" class="myselect" onchange="openModelChange(this.value);">
                        <asp:Literal ID="OpenModelOptions" runat="server"></asp:Literal>
                    </select>
                </td>
            </tr>
            <tr id="winsizetr" style="display: none;">
                <th>窗口大小：</th>
                <td>
                    宽度：<input type="text" id="Width" name="Width" class="mytext" runat="server" validate="int,canempty" style="width: 80px;"/>
                    高度：<input type="text" id="Height" name="Height" class="mytext" runat="server" validate="int,canempty" style="width: 80px;"/>
                </td>
            </tr>
            <tr>
                <th>相关参数：</th>
                <td><input type="text" id="Params" name="Params" class="mytext" runat="server" style="width: 95%"/></td>
            </tr>
            <tr>
            <th>图标：</th>
            <td><input type="text" name="Ico" id="Ico" class="myico" source="/Images/ico" value="" runat="server" style="width: 70%"/></td>
        </tr>
            <tr>
                <th>备注说明：</th>
                <td><textarea class="mytext" id="Note" name="Note" cols="1" rows="1" runat="server" style="width: 95%; height: 50px;"></textarea></td>
            </tr>
            </table>
        </div>
        <div id="div_button" title="页面按钮">
            <div style="height:8px;"></div>
            <table cellpadding="0" cellspacing="1" border="0" width="99%" style="width:99%" class="listtable" id="button_listtable">
                <thead>
                    <tr>
                        <th style="width:14%">按钮库</th>
                        <th style="width:20%">名称</th>
                        <th style="width:20%">脚本</th>
                        <th style="width:20%">图标</th>
                        <th style="width:10%">类型</th>
                        <th style="width:10%">排序</th>
                        <th style="width:6%"><img onclick="addbutton();" title="添加一行" style="vertical-align:middle; cursor:pointer;" src="../../Images/Ico/table_add.gif" /></th>
                    </tr>
                </thead>
                <tbody>
                     <% 
                        var subbuttons = new RoadFlow.Platform.AppLibraryButtons1().GetAllByAppID(appLibrary == null ? Guid.Empty : appLibrary.ID);
                        foreach (var but in subbuttons.OrderBy(p=>p.Sort))
                        {
                     %>  
                     <tr>
                         <td>
                             <input type="hidden" name="buttonindex" value="<%=but.ID %>"/>
                             <select class="myselect" data-val="<%=but.ButtonID %>" onchange="buttonchange(this);" style="width:90%" id="button_<%=but.ID %>" name="button_<%=but.ID %>">
                                <%=buttonOptions %>
                             </select>
                         </td>
                         <td><input type="text" class="mytext" value="<%=but.Name %>" style="width:90%" name="buttonname_<%=but.ID %>"/></td>
                         <td><textarea class="mytextarea" style="width:90%;height:50px;" name="buttonevents_<%=but.ID %>"><%=but.Events %></textarea></td>
                         <td><input type="text" class="myico" value="<%=but.Ico %>" source="/Images/ico" style="width:60%" id="buttonico_<%=but.ID %>" name="buttonico_<%=but.ID %>"/></td>
                         <td><select data-val="<%=but.ShowType %>" class="myselect" style="width:80px" name="showtype_<%=but.ID %>" id="showtype_<%=but.ID %>"><%=buttonShowTypeOptions %></select></td>
                         <td><input type="text" class="mytext" value="<%=but.Sort %>" style="width:90%" name="buttonsort_<%=but.ID %>"/></td>
                         <td><img style="vertical-align:middle; cursor:pointer;" src="../../Images/Ico/table_del.gif" onclick="delbutton(this);"/></td>
                     </tr>
                    <% 
                        }
                    %>
                </tbody>
            </table>
            <script type="text/javascript">
                var buttonJson =<%=buttonJson%>
                function addbutton()
                {
                    var $table = $("#button_listtable");
                    var index = $("tbody tr", $table).size() + 1;
                    var maxsort = parseInt($("tbody tr:last td:eq(5) input", $table).val()) + 5;
                    var tr = '<tr>';
                    tr += '<td>';
                    tr += '<input type="hidden" name="buttonindex" value="' + index + '"/>';
                    tr += '<select class="myselect" onchange="buttonchange(this);" style="width:90%" id="button_' + index + '" name="button_' + index + '">';
                    tr += '<%=buttonOptions%>';
                    tr += '</select>';
                    tr += '</td>';
                    tr += '<td>';
                    tr += '<input type="text" class="mytext" style="width:90%" name="buttonname_' + index + '"/>';
                    tr += '</td>';
                    tr += '<td>';
                    tr += '<textarea class="mytextarea" style="width:90%;height:50px;" name="buttonevents_' + index + '"></textarea>';
                    tr += '</td>';
                    tr += '<td>';
                    tr += '<input type="text" class="myico" source="/Images/ico" style="width:60%" id="buttonico_' + index + '" name="buttonico_' + index + '"/>';
                    tr += '</td>';
                    tr += '<td>';
                    tr += '<select class="myselect" style="width:80px" name="showtype_' + index + '" id="showtype_' + index + '"><%=buttonShowTypeOptions%></select>';
                    tr += '</td>';
                    tr += '<td>';
                    tr += '<input type="text" class="mytext" style="width:90%" name="buttonsort_' + index + '" value="' + maxsort + '"/>';
                    tr += '</td>';
                    tr += '<td>';
                    tr += '<img style="vertical-align:middle; cursor:pointer;" src="../../Images/Ico/table_del.gif" onclick="delbutton(this);"/>';
                    tr += '</td>';
                    tr += '</tr>';
                    var $tr = $(tr);
                    $("tbody", $table).append($tr);

                    new RoadUI.Select().init($(".myselect", $tr));
                    new RoadUI.SelectIco({ obj: $(".myico", $tr) });
                    new RoadUI.Button().init($(".mybutton", $tr));
                    new RoadUI.Text().init($(".mytext", $tr));
                }
                function delbutton(img)
                {
                    $(img).parent().parent().remove();
                }
                function buttonchange(sel)
                {
                    var $tr = $(sel).parent().parent();
                    for (var j = 0; j < buttonJson.length; j++)
                    {
                        if (buttonJson[j].id == $(sel).val())
                        {
                            var json = buttonJson[j];
                            $("input[name^='buttonname_']", $tr).val(json.name);
                            $("textarea[name^='buttonevents_']", $tr).val(json.events);
                            $("input[name^='buttonico_']", $tr).val(json.ico);
                            $("input[name^='buttonsort_']", $tr).val(json.sort);
                            new RoadUI.SelectIco().setIco($("input[name^='buttonico_']", $tr));
                        }
                    }
                }
            </script>
        </div>
    </div>
    <div class="buttondiv">
        <asp:Button ID="Button1" runat="server" Text="确定保存" OnClientClick="return new RoadUI.Validate().validateForm(document.forms[0]);" CssClass="mybutton" OnClick="Button1_Click" />
        <input type="button" class="mybutton" value="取消关闭" style="margin-left: 5px;" onclick="closewin();" />
    </div>
    <script type="text/javascript">
        var win = new RoadUI.Window();
        $(window).load(function ()
        {
            new RoadUI.Tab({ id: "tabdiv", replace: true, contextmenu: false, dblclickclose: false });
            $("#OpenModel").change();
            $("#button_listtable tbody tr").each(function ()
            {
                var $sel = $("select[id^='button_']", $(this));
                $sel.val($sel.attr("data-val"));
                var $showtype = $("select[id^='showtype_']", $(this));
                $showtype.val($showtype.attr("data-val"));
            });
        });
        function openModelChange(value)
        {
            if ("0" == value)
            {
                $("#winsizetr").hide();
            }
            else
            {
                $("#winsizetr").show();
            }
        }
        function closewin()
        {
            win.close();
            return false;
        }
    </script>
    </form>
</body>
</html>
