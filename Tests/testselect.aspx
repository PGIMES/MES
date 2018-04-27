<%@ Page Language="C#" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <%=WebForm.Common.Tools.IncludeFiles %>
</head>
<body>
    <form id="form1" runat="server">
    <div class="querybar">
        <table cellpadding="0" cellspacing="0" border="0" width="100%">
            <tr>
                <td>
                    名称：<input type="text" class="mytext" id="Title1" name="Title1" runat="server" style="width:150px" />
                    <asp:Button ID="Button2" runat="server" Text="&nbsp;&nbsp;查&nbsp;询&nbsp;&nbsp;" CssClass="mybutton" />
                    <input type="button" class="mybutton" onclick="ok();" value="确定选择" />
                </td>
            </tr>
        </table>
    </div>
    <table class="listtable">
        <thead>
            <tr>
                <th width="3%"><input type="checkbox" onclick="$('[name=\'ck\']').prop('checked',this.checked);" style="vertical-align:middle;" /></th>
                <th>名称</th>
                <th>型号</th>
                <th>单位</th>
                <th>数量</th>
            </tr>
        </thead>
        <tbody>
            <% 
                System.Data.DataTable dt = new RoadFlow.Data.MSSQL.DBHelper().GetDataTable("select * from TempTest_PurchaseList");
                foreach (System.Data.DataRow dr in dt.Rows)
                {   
            %>
            <tr>
                <td><input type="checkbox" name="ck" value="{'name':'<%=dr["Name"] %>','model':'<%=dr["Model"] %>','unit':'<%=dr["Unit"] %>','quantity':'<%=dr["Quantity"] %>'}" /></td>
                <td><%=dr["Name"] %></td>
                <td><%=dr["Model"] %></td>
                <td><%=dr["Unit"] %></td>
                <td><%=dr["Quantity"] %></td>
            </tr>
            <%  } %>
        </tbody>
    </table>
    </form>
    <script type="text/javascript">
        var openerid = "<%=Request.QueryString["openerid"]%>";
        function ok()
        {
            var iframes = top.frames;
            for (var i = 0; i < iframes.length; i++)
            {
                if (iframes[i].name == openerid + "_iframe")
                {
                    frame = iframes[i]; break;
                }
            }
            
            if (frame == null) return;
            var values = [];
            $(":checked[name='ck']").each(function ()
            {
                values.push($(this).val());
            });
            frame.addrow("[" + values.join(',') + "]");
        }
    </script>
</body>
</html>