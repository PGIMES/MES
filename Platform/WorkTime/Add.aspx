<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Add.aspx.cs" Inherits="WebForm.Platform.WorkTime.Add" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div style="margin-top:10px;">
        <table cellpadding="0" cellspacing="1" border="0" width="97%" class="formtable">
            <tr>
                <th style="width: 120px;">年份：</th>
                <td><input type="text" id="Year1" name="Year1" class="mytext" runat="server" validate="empty" /></td>
            </tr>
            <tr>
                <th>开始时间：</th>
                <td><input type="text" id="Date1" name="Date1" class="mycalendar" runat="server" validate="empty" style="width:70%" /></td>
            </tr>
            <tr>
                <th>结束时间：</th>
                <td><input type="text" id="Date2" name="Date2" class="mycalendar" runat="server" validate="empty" style="width:70%"/></td>
            </tr>
            <tr>
                <th>上午上班时间：</th>
                <td><select class="myselect" id="AmTime1_H" name="AmTime1_H" style="width:80px;" runat="server"></select>&nbsp;时&nbsp;<select class="myselect" id="AmTime1_M" name="AmTime1_M" style="width:80px;" runat="server"></select>&nbsp;分</td>
            </tr>
            <tr>
                <th>上午下班时间：</th>
                <td><select class="myselect" id="AmTime2_H" name="AmTime2_H" style="width:80px;" runat="server"></select>&nbsp;时&nbsp;<select class="myselect" id="AmTime2_M" name="AmTime2_M" style="width:80px;" runat="server"></select>&nbsp;分</td>
            </tr>
            <tr>
                <th>下午上班时间：</th>
                <td><select class="myselect" id="PmTime1_H" name="PmTime1_H" style="width:80px;" runat="server"></select>&nbsp;时&nbsp;<select class="myselect" id="PmTime1_M" name="PmTime1_M" style="width:80px;" runat="server"></select>&nbsp;分</td>
            </tr>
            <tr>
                <th>下午下班时间：</th>
                <td><select class="myselect" id="PmTime2_H" name="PmTime2_H" style="width:80px;" runat="server"></select>&nbsp;时&nbsp;<select class="myselect" id="PmTime2_M" name="PmTime2_M" style="width:80px;" runat="server"></select>&nbsp;分</td>
            </tr>
        </table>
        <div class="buttondiv">
            <asp:Button ID="Button1" runat="server" Text="确定保存" OnClientClick="return new RoadUI.Validate().validateForm(document.forms[0]);" CssClass="mybutton" OnClick="Button1_Click" />
            <input type="button" class="mybutton" value="取消关闭" style="margin-left: 5px;" onclick="new RoadUI.Window().close();" />
        </div>
    </div>
    </form>
</body>
</html>
