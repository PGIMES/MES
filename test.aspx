<%@ Page Language="C#" AutoEventWireup="true" CodeFile="test.aspx.cs" Inherits="test" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
 <script src="../Content/js/jquery.min.js" type="text/javascript"></script>
    <script src="../Content/js/plugins/layer/layer.min.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript">
        $(document).ready(function () {

            $("#tst").click(function () {

            });

        });
        $("div[class='h3']").text($("div[class='h3']").text() + "【精炼测氢】");
        function show_cur_times() {
            //获取当前日期
            var date_time = new Date();
            //年
            var year = date_time.getFullYear();
            //判断小于10，前面补0
            if (year < 10) {
                year = "0" + year;
            }
            //月
            var month = date_time.getMonth() + 1;
            //判断小于10，前面补0
            if (month < 10) {
                month = "0" + month;
            }
            //日
            var day = date_time.getDate();
            //判断小于10，前面补0
            if (day < 10) {
                day = "0" + day;
            }
            //时
            var hours = date_time.getHours();
            //判断小于10，前面补0
            if (hours < 10) {
                hours = "0" + hours;
            }
            //分
            var minutes = date_time.getMinutes();
            //判断小于10，前面补0
            if (minutes < 10) {
                minutes = "0" + minutes;
            }

            //秒
            var seconds = date_time.getSeconds();

            if (seconds < 10) {
                seconds = "0" + seconds;
            }

            var date_str = year + "-" + month + "-" + day + " " + hours + ":" + minutes + ":" + seconds + " ";

            $("span[id*='lb_end']").text(date_str);

        }



        function GPSGetDataTimer() {
            Referece = "data";  //获得GPS数据  
            $.post("test.aspx",
            { REF: encodeURI(Referece)
            },
            function (data) {
                alert(data);
            });
        }  

        //window.onload = 
        // setInterval("show_cur_times()", 100);
    </script>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:Button ID="Button1" runat="server" onclick="Button1_Click" Text="send" />
        <asp:Button ID="Button2" runat="server" onclick="Button2_Click" 
            Text="recevie" />
        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
        <asp:TextBox ID="TextBox2" runat="server" Height="324px" Width="573px"></asp:TextBox>


        <asp:TextBox ID="TextBox3" runat="server" OnTextChanged="TextBox3_TextChanged" AutoPostBack="true"></asp:TextBox>
        <asp:TextBox ID="TextBox4" runat="server" OnTextChanged="TextBox3_TextChanged" AutoPostBack="true"></asp:TextBox>
        <asp:TextBox ID="TextBox5" runat="server" OnTextChanged="TextBox3_TextChanged"></asp:TextBox>
    </div>
    </form>
</body>
</html>
