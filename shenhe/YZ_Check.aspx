<%@ Page Language="C#" AutoEventWireup="true" CodeFile="YZ_Check.aspx.cs" Inherits="shenhe_YZ_Check" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <%--<script>
        function clicked(i, o) {
            document.getElementById('info' + i).innerHTML += o.tagName + ' Tag Clicked.<br />';
        }
    </script>--%>
    <script src="../Content/js/jquery.min.js" type="text/javascript"></script>
    <script src="../Content/js/plugins/layer/layer.min.js"></script>
     <script type="text/javascript">
     function clicked(i, o) {
         var title = '点检内容及处理方法';

         layer.open({
             shade: [0.5, '#000', false],
             type: 2,
             offset: '20px',
             area: ['700px', '550px'],
             fix: false,
             maxmin: false,
             title: ['<i class="fa fa-dedent-"></i> ' + title, false],
             closeBtn: 1,
             content: 'YZCheck_Detail.aspx?&id=' + i + '',
             end: function(){}
         })
     }
        
       


</script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <img src="../Images/yz_B1.JPG" border="0" usemap="#Map1" />
        <map name="Map1">
            <area shape="circle" coords="37,71,20" onclick="clicked(1, this)"   >
            <area shape="circle" coords="134,35,23" onclick="clicked(16, this)">
        </map>
    </div>
    <div id="info1">
    </div>
    </form>
</body>
</html>
