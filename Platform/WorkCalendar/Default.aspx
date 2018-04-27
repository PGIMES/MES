<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebForm.Platform.WorkCalendar.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <% 
            var workDateList = BCal.GetAll(Year1);    
        %>
        <div class="toolbar" style="margin-top:0; border-top:none 0; position:fixed; top:0; left:0; right:0; margin-left:auto; z-index:999; width:100%; margin-right:auto; height:26px;">
            <div>
                <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click">
                    <span style="background:url(../../Images/ico/save.gif) no-repeat left center;">保存设置</span>
                </asp:LinkButton>
                <span class="toolbarsplit">&nbsp;</span>
                年份：<asp:DropDownList ID="DropDownList1" AutoPostBack="true" runat="server" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged"></asp:DropDownList>
                <a style="margin-left:6px;">共有工作日 <%=workDateList.Count %> 天</a>
                <a style="margin-left:6px;">说明：选中单元格背景为灰色表示是节假日</a>
                
            </div>
        </div>
        <br />
        <input type="hidden" name="year1" value="<%=Year1 %>" />
        <table style="width:100%;">
            <% 
                int year = Year1;
                int month = 1;
                for(int m=0;m<3;m++)
                { 
            %>
            <tr>
                <%for(int n=0;n<4;n++){ %>
                <td style="width:25%; text-align:center; vertical-align:top; padding:0 10px;">
                    <table border="1" cellpadding="0" cellspacing="1" width="100%" align="center" bordercolor="#ccc" style="border:1px solid #ccc;border-collapse:collapse; margin-top:30px; background:url(Images/<%=month%>.png) no-repeat center;">
                        <tr>
                            <td style="height:24px;">一</td>
                            <td>二</td>
                            <td>三</td>
                            <td>四</td>
                            <td>五</td>
                            <td>六</td>
                            <td>日</td>
                        </tr>
                        <% 
                            var date = (year + "-" + month + "-1").ToDateTime();
                            var date1 = (year + "-" + month + "-1").ToDateTime();
                            int week = date.DayOfWeek.ToString("d").ToInt();
                            if (week == 0)
                            {
                                week = 7;
                            }
                            int lastDay = date.AddMonths(1).AddDays(-1).Day;
                            var maxDate = (year + "-" + month + "-" + lastDay).ToDateTime();
                            int trs = (lastDay + week-1) % 7 == 0 ? (lastDay + week-1) / 7 : (lastDay + week-1) / 7 + 1;
                    
                            for (int i = 1; i <= trs; i++)
                            {
                        %>
                        <tr style="height:24px;">
                            <%
                                for (int j = 1; j <= 7; j++)
                                {
                                    int week1 = date1.DayOfWeek.ToString("d").ToInt();
                                    if (week1 == 0)
                                    {
                                        week1 = 7;
                                    }
                                    
                                    if (i == 1)
                                    {
                                        string background = date1.Month == month && (week1 == 6 || week1 == 7) && j >= week ? "#e8e8e8" : "";
                                        
                                        string v = "" == background ? date1.ToString("yyyy-MM-dd") : "";
                                        if (workDateList.Count > 0 && workDateList.Find(p => p.WorkDate == date1) != null)
                                        {
                                            v = date1.ToString("yyyy-MM-dd");
                                            background = "";
                                        }
                                        else if (j >= week && workDateList.Count > 0 && workDateList.Find(p => p.WorkDate == date1) == null)
                                        {
                                            v = "";
                                            background = "#e8e8e8";
                                        }
                                        Response.Write("<td onclick='setwork(\""+date1.ToString("yyyy-MM-dd")+"\", this);' style='background-color:" + background + "'>");
                                        if(j >= week)
                                        {
                                            Response.Write(date1.Day.ToString());
                                            Response.Write("<input type='hidden' value='" + v + "' name='workdate'/>");
                                            date1 = date1.AddDays(1);
                                        }
                                        Response.Write("</td>");
                                    }
                                    else
                                    {
                                        string background = date1.Month == month && (week1 == 6 || week1 == 7) ? "#e8e8e8" : "";
                                        string v = "" == background ? date1.ToString("yyyy-MM-dd") : "";
                                        if (workDateList.Count > 0 && workDateList.Find(p => p.WorkDate == date1) != null)
                                        {
                                            v = date1.ToString("yyyy-MM-dd");
                                            background = "";
                                        }
                                        else if (date1 <= maxDate && workDateList.Count > 0 && workDateList.Find(p => p.WorkDate == date1) == null)
                                        {
                                            v = "";
                                            background = "#e8e8e8";
                                        }
                                        Response.Write(string.Format("<td onclick='setwork(\"" + date1.ToString("yyyy-MM-dd") + "\", this);' style='background-color:" + background + "'>{0}", date1 > maxDate ? "" : date1.Day.ToString()));
                                        if (date1 <= maxDate)
                                        {
                                            Response.Write("<input type='hidden' value='" + v + "' name='workdate'/>");
                                        }
                                        Response.Write("</td>");
                                        date1 = date1.AddDays(1);
                                    }
                                }
                                
                            %>
                        </tr>
                        <%
                            
                        } 
                          
                      for (int x = 0; x < 6-trs; x++)
                      {
                          Response.Write("<tr style=\"height:24px;\"><td>&nbsp;</td><td></td><td></td><td></td><td></td><td></td><td></td></tr>");
                      }
                        %>
                    </table>

                </td>

                <%
                      month++;
                } %>
            </tr>

            <% 
                }    
            %>
        </table>

        <script type="text/javascript">
            function setwork(d, td)
            {
                if ($(td).text().length == 0)
                {
                    return;
                }
                var $hid = $("input[type='hidden']", $(td));
                if ($hid.size() > 0)
                {
                    if ($hid.val() && $hid.val().length > 0)
                    {
                        $hid.val("");
                        $(td).css("background-color", "#e8e8e8");
                    }
                    else
                    {
                        $hid.val(d);
                        $(td).css("background-color", "");
                    }
                }
            }
        </script>
    </form>
</body>
</html>