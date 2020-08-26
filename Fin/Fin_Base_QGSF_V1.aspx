<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Fin_Base_QGSF_V1.aspx.cs" Inherits="Fin_Fin_Base_QGSF_V1" %>

<%@ Register Assembly="DevExpress.Web.v17.2, Version=17.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <script src="/Content/js/jquery.min.js"  type="text/javascript"></script>
    <script src="/Content/js/bootstrap.min.js"></script>
    <script src="/Content/js/layer/layer.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#mestitle").text("【产品税率】");

            setHeight();
            $(window).resize(function () {
                setHeight();
            });

            $('#btn_add').click(function () {
                //var url = '/Fin/Fin_Base_QGSF_Add.aspx';
                //layer.open({
                //    title: '新增基率&清关税<font color="red">【Base Rate】【301 Rate】请填写小数.</font>',
                //    closeBtn: 2,
                //    type: 2,
                //    area: ['750px', '300px'],
                //    fixed: false, //不固定
                //    maxmin: true, //开启最大化最小化按钮
                //    content: url,
                //    cancel: function (index, layero) {//取消事件
                //    },
                //    end: function () {//无论是确认还是取消，只要层被销毁了，end都会执行，不携带任何参数。layer.open关闭事件
                //        location.reload();　　//layer.open关闭刷新
                //    }

                //});
            });
        });

        function setHeight() {
            $("div[class=dxgvCSD]").css("height", ($(window).height() - $("#div_p").height() - 200) + "px");
        }
        	
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

    开发中
</asp:Content>

