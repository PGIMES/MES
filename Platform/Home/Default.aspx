<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebForm.Platform.Home.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div class="homemessage" style="margin-top:15px;">
    <table cellpadding="0" cellspacing="0" border="0">
        <tr>
            <td>
                <%
                foreach(var item in ItemsList.FindAll(p=>p.Type==0))
                {
                    string bgcolor = item.BgColor.IsNullOrEmpty() ? "#27a9e3" : item.BgColor;
                    string ico = item.Ico.IsNullOrEmpty() ? "fa-coffee" : item.Ico;
                    string count = BHI.GetDataSourceString(item);
                %>
                <a class="homemsglink" <%=item.LinkURL.IsNullOrEmpty()?"":"href=\"javascript:void(0);\" onclick=\"top.openApp('"+item.LinkURL.FilterWildcard()+"',0,'"+item.Title+"','');\""%>>
                <div class="homemessagediv" style="background:<%=bgcolor%>;">
                    <div>
                        <div class="homemessagenumber">
                            <%if(ico.IsFontIco()){ %>
                            <div class="fa <%=ico%>" style="font-size:38px;margin-left:20px;float:left; padding-top:12px;"></div><div style="float:right; padding-top:10px; vertical-align:middle;"><%=count %></div>
                            <%}else{ %>
                            <div style="margin-left:15px;float:left; padding-top:12px;"><img src="<%=ico %>" border="0" style="vertical-align:middle;" /></div><div style="float:right; padding-top:10px; vertical-align:middle;"><%=count %></div>
                            <%} %>
                        </div>
                    </div>
                    <div class="homemessagetext">
                        <div class="homemessagetextbg"></div>
                        <div class="homemessagetextword" style="color:#fff;"><%=item.Title %></div>
                    </div>
                </div>
                </a>
                <%} %>
            </td>
        </tr>
    </table>
    </div>
    <div style="width:96%; margin:18px auto 0 auto;">
    <table cellpadding="0" cellspacing="0" border="0" style="width:100%;">
        <tr>
            <td style="width:65%; vertical-align:top;">
                <% 
                foreach(var item in ItemsList.FindAll(p=>p.Type==1))
                {
                %>
                <div class="homelistdiv">
                    <div style="height:35px; font-size:14px;">
                    <% 
                    if (!item.Ico.IsNullOrEmpty())
                    {
                        if (item.Ico.IsFontIco())
                        {
                            Response.Write("<div class='fa " + item.Ico + "' style='font-size:18px;float:left;margin-right:6px;padding-top:12px;'></div>");
                            Response.Write("<div style=\"padding-top:10px; float:left; font-weight:bold;\">" + item.Title + "</div>");
                        }
                        else
                        {
                            Response.Write("<div style=\"padding-top:10px; float:left; font-weight:bold;background:url(" + item.Ico + ") no-repeat left 12px; padding-left:20px;\">" + item.Title + "</div>");
                        }
                    }  
                    %>
                    <% 
                    if(!item.LinkURL.IsNullOrEmpty()){
                    %>
                    <div style="padding-top:10px; float:right; margin-right:10px;">
                        <a href="javascript:void(0);" onclick="top.openApp('<%=item.LinkURL.FilterWildcard() %>',0,'<%=item.Title %>','l_<%=item.ID.ToString("N") %>');">
                            <i class="fa fa-angle-double-right"></i><span style="font-size:12px; margin-left:3px;">更多...</span>
                        </a>
                    </div>
                    <%} %>
                    </div>
                    <div>
                        <%=BHI.GetDataSourceString(item) %>
                    </div>
                </div>
                <%} %>
            </td>
            <td style="vertical-align:top; padding:0 0 0 30px;">
                <%foreach(var item in ItemsList.FindAll(p=>p.Type==2)){ %>
                <div class="homelistdiv">
                    <div style="height:35px; font-size:14px;">
                        <% 
                        if (!item.Ico.IsNullOrEmpty())
                        {
                            if (item.Ico.IsFontIco())
                            {
                                Response.Write("<div class='fa " + item.Ico + "' style='font-size:18px;float:left;margin-right:6px;padding-top:12px;'></div>");
                                Response.Write("<div style=\"padding-top:10px; float:left; font-weight:bold;\">" + item.Title + "</div>");
                            }
                            else
                            {
                                Response.Write("<div style=\"padding-top:10px; float:left; font-weight:bold;background:url(" + item.Ico + ") no-repeat left 12px; padding-left:20px;\">" + item.Title + "</div>");
                            }
                        }  
                        %>
                        
                        <% 
                        if(!item.LinkURL.IsNullOrEmpty()){
                        %>
                        <div style="padding-top:10px; float:right; margin-right:10px;">
                            <a href="javascript:void(0);" onclick="top.openApp('<%=item.LinkURL.FilterWildcard() %>',0,'<%=item.Title %>','l_<%=item.ID.ToString("N") %>');">
                                <i class="fa fa-angle-double-right"></i><span style="font-size:12px; margin-left:3px;">更多...</span>
                            </a>
                        </div>
                        <%} %>
                    </div>
                    <%=BHI.GetDataSourceString(item) %>
                </div>
                <%} %>
            </td>
        </tr>
    </table>
    </div>
    </form>
</body>
</html>
