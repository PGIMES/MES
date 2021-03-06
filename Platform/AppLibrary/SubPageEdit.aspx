﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SubPageEdit.aspx.cs" Inherits="WebForm.Platform.AppLibrary.SubPageEdit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>

<body>
    <form id="form1" runat="server">
    <br />
    <% 
        var AppButton = new RoadFlow.Platform.AppLibraryButtons();
        string buttonOptions = AppButton.GetOptions();
        string buttonJson = AppButton.GetAllJson();
        string buttonShowTypeOptions = AppButton.GetShowTypeOptions();    
    %>
    <script type="text/javascript">
        var buttonJson=<%=buttonJson%>
    </script>
    <table cellpadding="0" cellspacing="1" border="0" width="99%" class="formtable">
        <tr>
            <th style="width: 80px;">名称：</th>
            <td><input type="text" name="Title" id="Title1" class="mytext" value="" runat="server" validate="empty" style="width: 75%"/></td>
        </tr>
        <tr>
            <th>地址：</th>
            <td><input type="text" name="Address" id="Address" class="mytext" value="" runat="server" validate="empty" style="width: 75%"/></td>
        </tr>
        <tr>
            <th>按钮：</th>
            <td>
                <table cellpadding="0" cellspacing="1" border="0" width="100%" style="width:100%" class="listtable" id="button_listtable">
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
                            var subbuttons = new RoadFlow.Platform.AppLibraryButtons1().GetAllByAppID(sub == null ? Guid.Empty : sub.ID);
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
                        <%} %>
                    </tbody>
                </table>
                <script type="text/javascript">
                    function addbutton()
                    {
                        var $table = $("#button_listtable");
                        var index = $("tbody tr", $table).size() + 1;
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
                        tr += '<select class="myselect" style="width:80px" name="showtype_'+index+'" id="showtype_'+index+'"><%=buttonShowTypeOptions%></select>';
                        tr += '</td>';
                        tr += '<td>';
                        tr += '<input type="text" class="mytext" style="width:90%" name="buttonsort_' + index + '"/>';
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
            </td>
        </tr>
    </table>
    <div class="buttondiv">
        <asp:Button ID="Button1" runat="server" CssClass="mybutton" Text="确定保存" OnClientClick="return checkform(this);" OnClick="Button1_Click" />
        <input type="button" class="mybutton" value="取消返回" style="margin-left: 5px;" onclick="window.location='SubPages.aspx'+'<%=Request.Url.Query%>';" />
    </div>
    </form>
    <script type="text/javascript">
        $(function(){
            $("#button_listtable tbody tr").each(function ()
            {
                var $sel = $("select[id^='button_']");
                $sel.val($sel.attr("data-val"));
                var $showtype = $("select[id^='showtype_']");
                $showtype.val($showtype.attr("data-val"));
            });
        });
        function checkform(but)
        {
            var f = document.forms[0];
            var flag = new RoadUI.Validate().validateForm(f);
            return flag;
        }
    </script>
</body>
</html>
