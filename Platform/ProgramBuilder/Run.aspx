<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Run.aspx.cs" Inherits="WebForm.Platform.ProgramBuilder.Run" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title><%=PBModel.Program.Name %></title>
</head>
<body>
    <form id="form1" runat="server">
        <%=buttonHtmlDicts[0] %>
        <%if(!buttonHtmlDicts[0].IsNullOrEmpty()){%>
            <div style="height:35px;"></div>
        <%}%>    
        <%if(PBModel.Querys.Count>0 && PBModel.Buttons.Count>0){%>
        <div class="querybar">
            <table cellpadding="0" cellspacing="0" border="0" width="100%">
                <tr>
                    <td>
                        <%=PBQ.GetQueryShowHtml(PBModel.Querys) %>
                        <%if(1==PBModel.Program.ButtonLocation){%>
                        <%=PBQ.GetQueryButtonHtml(PBModel.Program) %><%=buttonHtmlDicts[1] %>
                        <%}%>  
                    </td>
                </tr>
            </table>
            <%if(1!=PBModel.Program.ButtonLocation){%>
            <table cellpadding="0" cellspacing="0" border="0" width="100%" style="margin-top:8px;">
                <tr>
                    <td style="text-align:center">
                        <%=PBQ.GetQueryButtonHtml(PBModel.Program)%><%=buttonHtmlDicts[1] %>
                    </td>
                </tr>
            </table>
            <%}%>
        </div>
        <%}%>  
        <table class="<%=PBModel.Program.TableStyle.IsNullOrEmpty()?"listtable":PBModel.Program.TableStyle %>"<%="reporttable"==PBModel.Program.TableStyle?" cellpadding=\"1\" cellspacing=\"0\" border=\"1\"":"" %>>
            <thead>
                <%if(PBModel.Program.TableHead.IsNullOrEmpty()){ %>
                <tr>
                    <%
                    foreach (var field in PBModel.Fields)
                    {
                        if (field.ShowType == 7 && buttonHtmlDicts[2].IsNullOrEmpty())
                        {
                            continue;//如果按钮列没有按钮则不显示该列
                        }
                     %>
                    <th style="text-align:<%=field.Align%>;<%=(field.Width.IsNullOrEmpty() ? "" : "width:" + field.Width + ";") %>"><label><%=field.ShowTitle %></label></th>
                    <%}%>
                </tr>
                <%}else{%>
                <%=PBModel.Program.TableHead %>
                <%} %>
            </thead>
            <tbody>
            <% 
                int index = 1;
                foreach (System.Data.DataRow dr in Dt.Rows)
                {
            %>
                <tr>
                    <%foreach (var field in PBModel.Fields)
                    {
                        if (field.ShowType == 7 && buttonHtmlDicts[2].IsNullOrEmpty())
                        {
                            continue;
                        }
                        string text = string.Empty;
                        object obj = field.Field.IsNullOrEmpty() ? "" : dr[field.Field];
                        switch (field.ShowType)
                        {
                            case 0://直接输出
                                text = obj.ToString();
                                break;
                            case 1://序号
                                text = index.ToString();
                                break;
                            case 2://日期时间
                                text = obj.ToString().ToDateTime().ToString(field.ShowFormat);
                                break;
                            case 3://数字
                                text = obj.ToString().ToDecimal().ToString(field.ShowFormat);
                                break;
                            case 4://数据字典ID显示为标题
                                text = BDict.GetTitle(obj.ToString().ToGuid());
                                break;
                            case 5://组织机构ID显示为名称
                                text = BOrganize.GetNames(obj.ToString());
                                break;
                            case 6://自定义
                                text = field.CustomString;
                                break;
                            case 7://按钮列
                                text = RoadFlow.Platform.Wildcard.FilterWildcard(buttonHtmlDicts[2], "", dr);
                                break;
                            case 8://附件显示不换行
                                text = RoadFlow.Platform.Files.GetFilesShowString(obj.ToString(), newRow: false);
                                break;
                            case 9://附件显示换行
                                text = RoadFlow.Platform.Files.GetFilesShowString(obj.ToString());
                                break;
                        }
                    %>
                    <td style="text-align:<%=field.Align%>">
                       <%=text %>
                    </td>
                    <%}%>
                </tr>
               <%index++;}%>
            </tbody>
        </table>
        <div class="buttondiv">
            <asp:Literal ID="PagerText" runat="server"></asp:Literal>
        </div>
    </form>
    <script type="text/javascript">
        var query = '<%=Query%>';
        var prevurl = '<%=PrevUrl%>';
        var tabid = '<%=Request.QueryString["tabid"]%>';
        var apptitle = '<%=PBModel.Program.Name%>';
        var editmodel = '<%=(PBModel.Program.EditModel.HasValue ? PBModel.Program.EditModel.Value : 0)%>';
        var formid = '<%=PBModel.Program.FormID%>';
        var programid = '<%=pid%>';
        
        function add(id, title, isShow)
        {
            var url = '/Platform/WorkFlowRun/SubTableEdit.aspx?secondtableeditform=' + formid + "&editmodel=" + editmodel + "&instanceid=" + (id || "") + "&display=" + (isShow ? "1" : "0") + query + "&prevurl=" + prevurl;
            if ('0' == editmodel)
            {
                window.location = url;
            }
            else
            {
                var width = "<%=PBModel.Program.Width%>";
                var height = "<%=PBModel.Program.Height%>";
                if (isNaN(width))
                {
                    width = 800;
                }
                if (isNaN(height))
                {
                    height = 500;
                }
                new RoadUI.Window().open({ url: url, width: width, height: height, opener: window, openerid: tabid, title: title || "新增" });
            }
            return true;
        }
        function edit(id)
        {
            add(id, "编辑");
            return true;
        }
        function del(id)
        {
            if (!confirm("您确定要删除该条数据吗?"))
            {
                return false;
            }
            var url = 'RunDelete.aspx?secondtableeditform=' + formid + "&editmodel=" + editmodel + "&instanceid=" + (id || "") + query + "&prevurl=" + prevurl;
            window.location = url;
            return true;
        }
        function view(id)
        {
            add(id, "查看", true);
            return true;
        }
        function outToExcel()
        {
            window.location = "OutToExcel.ashx?programid=" + programid;
        }
        <%=PBModel.Program.ClientScript.FilterWildcard()%>
    </script>
</body>
</html>
