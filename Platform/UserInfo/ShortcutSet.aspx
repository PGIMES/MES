<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ShortcutSet.aspx.cs" Inherits="WebForm.Platform.UserInfo.ShortcutSet" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body style="margin:0; padding:0;">
    <form id="form1" runat="server">
    <table cellpadding="0" cellspacing="1" border="0" style="width:100%; margin:0 auto;">
        <tr>
            <td style="width:48%; vertical-align:top;">
                <div style="width:100%; margin:0 auto; ">
                    <div class="toolbar" style="margin-top:0; border-top:none 0; position:fixed; top:0; left:0; right:0; margin-left:auto; z-index:999; width:100%; margin-right:auto;">
                        <asp:LinkButton ID="LinkButton1" runat="server" OnClientClick="return checkform(this);" OnClick="LinkButton1_Click"><span style="background-image:url(../../Images/ico/save.gif);">保存选择</span></asp:LinkButton>
                        <asp:LinkButton ID="LinkButton2" runat="server" OnClick="LinkButton2_Click"><span style="background-image:url(../../Images/ico/saveas.gif);">保存排序</span></asp:LinkButton>
                    </div>
                     <table id="treeTable1" style="width:100%; margin-top:37px;" class="listtable">
                        <thead>
                            <tr>
                                <th style="width:26%;">标题</th>
                                <th style="width:7%;">图标</th>
                                <th style="width:7%; text-align:center"><input type="checkbox" id="checkall" onclick="$('input[type=\'checkbox\']').prop('checked', this.checked);" /></th>
                            </tr>
                        </thead>
                        <tbody>
                            <%=menuhtml %>
                        </tbody>
                    </table>
                </div>
            </td>
            <td>
                
            </td>
            <td style="width:48%; vertical-align:top; padding-top:5px;">
                <div style="width:82%; margin:40px auto 0  auto; height:auto;" id="sortdiv">
                    <%foreach (System.Data.DataRow dr in busDt.Rows){
                          var menu = bmenu.Get(dr["MenuID"].ToString().ToGuid());
                          if (menu == null)
                          {
                              continue;
                          }     
                    %>
                    <ul class="sortul">
                        <input type="hidden" value="<%=dr["ID"] %>" name="sort" />
                        <%=menu.Title %>
                    </ul>
                    <%} %>
                </div>
            </td>
        </tr>
    </table>
    
    </form>
    <script type="text/javascript">
        var json=<%=json%>;
        $(function ()
        {
            new RoadUI.DragSort($("#sortdiv"));
            new RoadUI.TreeTable().init({ id: "treeTable1" });
            $("#treeTable1 tbody tr").each(function ()
            {
                $("td:last", $(this)).hide();
            });
            for(var i=0;i<json.length;i++)
            {
                $("input[value='"+json[i].MenuID+"']").prop("checked",true);
            }
        });

        function appboxclick(box)
        {
            return;
        }
    </script>
</body>
</html>
