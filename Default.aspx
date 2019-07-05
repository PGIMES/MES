<%@ Page Title="PGI����ϵͳ" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="Default.aspx.cs" Inherits="_Default" MaintainScrollPositionOnPostback="True" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <style type="text/css">
        .dropdown-submenu {
            position: relative;
        }

            .dropdown-submenu > .dropdown-menu {
                top: 0;
                left: 100%;
                margin-top: -6px;
                margin-left: -1px;
                -webkit-border-radius: 0 6px 6px 6px;
                -moz-border-radius: 0 6px 6px;
                border-radius: 0 6px 6px 6px;
            }

            .dropdown-submenu:hover > .dropdown-menu {
                display: block;
            }

            .dropdown-submenu > a:after {
                display: block;
                content: " ";
                float: right;
                width: 0;
                height: 0;
                border-color: transparent;
                border-style: solid;
                border-width: 5px 0 5px 5px;
                border-left-color: #ccc;
                margin-top: 5px;
                margin-right: -10px;
            }

            .dropdown-submenu:hover > a:after {
                border-left-color: #fff;
            }

            .dropdown-submenu.pull-left {
                float: none;
            }

                .dropdown-submenu.pull-left > .dropdown-menu {
                    left: -100%;
                    margin-left: 10px;
                    -webkit-border-radius: 6px 0 6px 6px;
                    -moz-border-radius: 6px 0 6px 6px;
                    border-radius: 6px 0 6px 6px;
                }

        .dropdown-menu > li > a:focus, .dropdown-menu > li > a:hover {
            color: #262626;
            text-decoration: none;
            background-color: #red;
        }
    </style>

</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <script type="text/javascript">
        $(document).ready(function () {
            //Login
            $("a[id*='login']").click(function () {
                layer.open({
                    shade: [0.5, '#000', false],
                    type: 2,
                    offset: '100px',
                    area: ['600px', '550px'],
                    fix: false, //���̶�
                    maxmin: false,
                    title: ['<i class="fa fa-dedent"></i> ����', false],
                    closeBtn: 1,
                    content: 'Login.aspx?quyu=' + this.name,
                    end: function () {
                        //                        $("#table_list_2").jqGrid('setGridParam', {
                        //                            datatype: 'json', postData: { 'YaoXinId': $("#YaoXinId").val() }, //��������
                        //                            page: 1  }).trigger("reloadGrid");
                        //                         
                    }
                });
            });
            $("span[id*='login']").click(function () {
                layer.open({
                    shade: [0.5, '#000', false],
                    type: 2,
                    offset: '100px',
                    area: ['600px', '550px'],
                    fix: false, //���̶�
                    maxmin: false,
                    title: ['<i class="fa fa-dedent"></i> ����', false],
                    closeBtn: 1,
                    content: 'Login.aspx?quyu=' + this.title,
                    end: function () {

                    }
                });
            });
            //Logout
            $("a[id*='logout']").click(function () {
                layer.open({
                    shade: [0.5, '#000', false],
                    type: 2,
                    area: ['500px', '480px'],
                    fix: false, //���̶�
                    maxmin: false,
                    title: ['<i class="fa fa-dedent"></i> �ǳ�', false],
                    closeBtn: 1,
                    content: 'Logout.aspx?gongwei=' + this.name,
                    end: function () {

                    }
                });
            });
            $("span[id*='logout']").click(function () {
                layer.open({
                    shade: [0.5, '#000', false],
                    type: 2,
                    area: ['500px', '480px'],
                    fix: false, //���̶�
                    maxmin: false,
                    title: ['<i class="fa fa-dedent"></i> �ǳ�', false],
                    closeBtn: 1,
                    content: 'Logout.aspx?gongwei=' + this.title,
                    end: function () {

                    }
                });
            });
            show();
            ////��ȡ���豸������Ϣ����
            setInterval(show, 60000); //20sˢ��һ��
            function setable(result, frm, btn) {
                var tag = "����";
                if (result.indexOf(tag) > -1) {
                    $("div[name*='" + frm + "']").addClass("btn-gray disabled");
                    $("div[name*='" + btn + "']").addClass("disabled");
                } else {
                    $("div[name*='" + frm + "']").removeClass("btn-gray disabled");
                    $("div[name*='" + frm + "']").addClass("btn-success");
                    $("div[name*='" + btn + "']").removeClass("disabled");
                }

            }
            function warningset(equipno, frm, msgspan) {
                $.post("InitDefaultInfoHandler.ashx?flag=equipstatus&shebei=" + equipno, { shebei: "" }, function (result) {

                    var json = eval(result);

                    if (json.color == "") { //remove class

                        $("div[name*='" + frm + "']").removeClass("btn-red").removeClass("btn-yellow");
                    }
                    else if (json.color != "") { //exists or add css
                        $("div[name*='" + frm + "']").removeClass("btn-red").removeClass("btn-yellow");
                        $("div[name*='" + frm + "']").addClass("btn-" + json.color);

                        $("div[name*='" + frm + "']").children(".area").last().append(json.msg);

                    }
                    //ά����ʾ
                    //  $("div[name*='" + frm + "']").children(".area").children().last().remove();
                    //  $(json.msg).appendTo($("div[name*='" + frm + "']").children(".area").children().last());
                    //���ػ���ʾ
                    //         $("div[name*='" + frm + "']").children("#action").remove();
                    //         $(json.action).appendTo($("div[name*='" + frm + "']"));
                    //����ػ������ ��ɫ
                    if ($(json.action).text() == "�ػ�") {
                        $("div[name*='" + frm + "']").addClass("btn-gray ");
                    } else {
                        $("div[name*='" + frm + "']").removeClass("btn-gray ");
                        $("div[name*='" + frm + "']").addClass("btn-success");
                    }

                });



            }
            function show() {

                //// ����
                $.post("InitDefaultInfoHandler.ashx?flag=RL&shebei=A", { shebei: "" }, function (result) {

                    if (result != "") { $("div[id*='div_A']").html(result); }
                    setable(result, "frm_A", "btn_A"); warningset("A", "frm_A");
                });
                $.post("InitDefaultInfoHandler.ashx?flag=RL&shebei=B", { shebei: "" }, function (result) {
                    //alert(result);
                    if (result != "") { $("div[id*='div_B']").html(result); }
                    setable(result, "frm_B", "btn_B"); warningset("B", "frm_B");
                });
                $.post("InitDefaultInfoHandler.ashx?flag=RL&shebei=C", { shebei: "" }, function (result) {
                    if (result != "") { $("div[id*='div_C']").html(result); }
                    setable(result, "frm_C", "btn_C"); warningset("C", "frm_C");
                });
                $.post("InitDefaultInfoHandler.ashx?flag=RL&shebei=D", { shebei: "" }, function (result) {
                    if (result != "") { $("div[id*='div_D']").html(result); }
                    setable(result, "frm_D", "btn_D"); warningset("D", "frm_D");
                });
                $.post("InitDefaultInfoHandler.ashx?flag=RL&shebei=E", { shebei: "" }, function (result) {
                    if (result != "") { $("div[id*='div_E']").html(result); }
                    setable(result, "frm_E", "btn_E"); warningset("E", "frm_E");
                });
                $.post("InitDefaultInfoHandler.ashx?flag=RL&shebei=F", { shebei: "" }, function (result) {
                    if (result != "") { $("div[id*='div_F']").html(result); }
                    setable(result, "frm_F", "btn_F"); warningset("F", "frm_F");
                });
                //var sb=������1#
                $.post("Default_jl_Handler.ashx?flag=JL1&shebei=JL1", { shebei: "" }, function (result) {
                    if (result != "") { $("div[id*='div_JL1']").html(result); }
                    setable(result, "frm_JL1", "btn_JL1");// warningset("������,������1", "frm_JL1");
                });
                $.post("Default_jl_Handler.ashx?flag=JL2&shebei=JL2", { shebei: "" }, function (result) {
                    if (result != "") { $("div[id*='div_JL2']").html(result); }
                    setable(result, "frm_JL2", "btn_JL2"); // warningset("������,������2", "frm_JL2");
                });


                $.post("Default_jl_Handler.ashx?flag=ZY&shebei=ZYB", { shebei: "" }, function (result) {
                    //alert(result);
                    if (result != "") { $("div[id*='div_ZY']").html(result); }
                    setable(result, "frm_ZY", "btn_ZY");// warningset("ת�˰�", "frm_ZY");
                });
                //ѹ��ʵ����
                $.post("Default_jl_Handler.ashx?flag=GP&shebei=GP", { shebei: "" }, function (result) {
                    //����              
                    if (result != "") { $("div[id*='div_GP']").html(result); }
                    setable(result, "frm_GP", "btn_GP"); warningset("����", "frm_GP");
                });
                //ѹ�� 1 ����
                $.post("InitDefaultInfoHandler.ashx?flag=yazhu&shebei=1-1", { shebei: "" }, function (result) {
                    if (result != "") { $("div[id*='div_1-1']").html(result); }
                    setable(result, "frm_1-1", "btn_1-1"); warningset("1-1", "frm_1-1");
                });
                $.post("InitDefaultInfoHandler.ashx?flag=yazhu&shebei=1-2", { shebei: "" }, function (result) {
                    if (result != "") { $("div[id*='div_1-2']").html(result); }
                    setable(result, "frm_1-2", "btn_1-2"); warningset("1-2", "frm_1-2");
                });
                $.post("InitDefaultInfoHandler.ashx?flag=yazhu&shebei=1-3", { shebei: "" }, function (result) {
                    if (result != "") { $("div[id*='div_1-3']").html(result); }
                    setable(result, "frm_1-3", "btn_1-3"); warningset("1-3", "frm_1-3");
                });
                $.post("InitDefaultInfoHandler.ashx?flag=yazhu&shebei=1-4", { shebei: "" }, function (result) {
                    if (result != "") { $("div[id*='div_1-4']").html(result); }
                    setable(result, "frm_1-4", "btn_1-4"); warningset("1-4", "frm_1-4");
                });
                $.post("InitDefaultInfoHandler.ashx?flag=yazhu&shebei=1-5", { shebei: "" }, function (result) {
                    if (result != "") { $("div[id*='div_1-5']").html(result); }
                    setable(result, "frm_1-5", "btn_1-5"); warningset("1-5", "frm_1-5");
                });
                $.post("InitDefaultInfoHandler.ashx?flag=yazhu&shebei=1-6", { shebei: "" }, function (result) {
                    if (result != "") { $("div[id*='div_1-6']").html(result); }
                    setable(result, "frm_1-6", "btn_1-6"); warningset("1-6", "frm_1-6");
                });
                $.post("InitDefaultInfoHandler.ashx?flag=yazhu&shebei=1-7", { shebei: "" }, function (result) {
                    if (result != "") { $("div[id*='div_1-7']").html(result); }
                    setable(result, "frm_1-7", "btn_1-7"); warningset("1-7", "frm_1-7");
                });
                $.post("InitDefaultInfoHandler.ashx?flag=yazhu&shebei=1-8", { shebei: "" }, function (result) {
                    if (result != "") { $("div[id*='div_1-8']").html(result); }
                    setable(result, "frm_1-8", "btn_1-8"); warningset("1-8", "frm_1-8");
                });
                $.post("InitDefaultInfoHandler.ashx?flag=yazhu&shebei=1-9", { shebei: "" }, function (result) {
                    if (result != "") { $("div[id*='div_1-9']").html(result); }
                    setable(result, "frm_1-9", "btn_1-9"); warningset("1-9", "frm_1-9");
                });
                //ѹ��2�����ջ�
                $.post("InitDefaultInfoHandler.ashx?flag=BULE&shebei=2-1", { shebei: "" }, function (result) {
                    if (result != "") { $("div[id*='div_bule2-1']").html(result); }
                    setable(result, "frm_bule2-1", "btn_bule2-1"); warningset("2-1", "frm_bule2-1");
                });
                $.post("InitDefaultInfoHandler.ashx?flag=BULE&shebei=2-2", { shebei: "" }, function (result) {
                    if (result != "") { $("div[id*='div_bule2-2']").html(result); }
                    setable(result, "frm_bule2-2", "btn_bule2-2"); warningset("2-2", "frm_bule2-2");
                });
                $.post("InitDefaultInfoHandler.ashx?flag=BULE&shebei=2-3", { shebei: "" }, function (result) {
                    if (result != "") { $("div[id*='div_bule2-3']").html(result); }
                    setable(result, "frm_bule2-3", "btn_bule2-3"); warningset("2-3", "frm_bule2-3");
                });
                $.post("InitDefaultInfoHandler.ashx?flag=BULE&shebei=2-4", { shebei: "" }, function (result) {
                    if (result != "") { $("div[id*='div_bule2-4']").html(result); }
                    setable(result, "frm_bule2-4", "btn_bule2-4"); warningset("2-4", "frm_bule2-4");
                });
                $.post("InitDefaultInfoHandler.ashx?flag=BULE&shebei=2-5", { shebei: "" }, function (result) {
                    if (result != "") { $("div[id*='div_bule2-5']").html(result); }
                    setable(result, "frm_bule2-5", "btn_bule2-5"); warningset("2-5", "frm_bule2-5");
                });
                $.post("InitDefaultInfoHandler.ashx?flag=BULE&shebei=2-6", { shebei: "" }, function (result) {
                    if (result != "") { $("div[id*='div_bule2-6']").html(result); }
                    setable(result, "frm_bule2-6", "btn_bule2-6"); warningset("2-6", "frm_bule2-6");
                });
                $.post("InitDefaultInfoHandler.ashx?flag=BULE&shebei=2-7", { shebei: "" }, function (result) {
                    if (result != "") { $("div[id*='div_bule2-7']").html(result); }
                    setable(result, "frm_bule2-7", "btn_bule2-7"); warningset("2-7", "frm_bule2-7");
                });
                //ѹ��3����
                $.post("InitDefaultInfoHandler.ashx?flag=yazhu&shebei=3-1", { shebei: "" }, function (result) {
                    if (result != "") { $("div[id*='div_3-1']").html(result); }
                    setable(result, "frm_3-1", "btn_3-1"); warningset("3-1", "frm_3-1");
                });
                $.post("InitDefaultInfoHandler.ashx?flag=yazhu&shebei=3-2", { shebei: "" }, function (result) {
                    if (result != "") { $("div[id*='div_3-2']").html(result); }
                    setable(result, "frm_3-2", "btn_3-2"); warningset("3-2", "frm_3-2");
                });
                $.post("InitDefaultInfoHandler.ashx?flag=yazhu&shebei=3-3", { shebei: "" }, function (result) {
                    if (result != "") { $("div[id*='div_3-3']").html(result); }
                    setable(result, "frm_3-3", "btn_3-3"); warningset("3-3", "frm_3-3");
                });
                $.post("InitDefaultInfoHandler.ashx?flag=yazhu&shebei=3-4", { shebei: "" }, function (result) {
                    if (result != "") { $("div[id*='div_3-4']").html(result); }
                    setable(result, "frm_3-4", "btn_3-4"); warningset("3-4", "frm_3-4");
                });
                $.post("InitDefaultInfoHandler.ashx?flag=yazhu&shebei=3-5", { shebei: "" }, function (result) {
                    if (result != "") { $("div[id*='div_3-5']").html(result); }
                    setable(result, "frm_3-5", "btn_3-5"); warningset("3-5", "frm_3-5");
                });
                $.post("InitDefaultInfoHandler.ashx?flag=yazhu&shebei=3-6", { shebei: "" }, function (result) {
                    if (result != "") { $("div[id*='div_3-6']").html(result); }
                    setable(result, "frm_3-6", "btn_3-6"); warningset("3-6", "frm_3-6");
                });
                $.post("InitDefaultInfoHandler.ashx?flag=yazhu&shebei=3-7", { shebei: "" }, function (result) {
                    if (result != "") { $("div[id*='div_3-7']").html(result); }
                    setable(result, "frm_3-7", "btn_3-7"); warningset("3-7", "frm_3-7");
                });
                $.post("InitDefaultInfoHandler.ashx?flag=yazhu&shebei=3-8", { shebei: "" }, function (result) {
                    if (result != "") { $("div[id*='div_3-8']").html(result); }
                    setable(result, "frm_3-8", "btn_3-8"); warningset("3-8", "frm_3-8");
                });
                $.post("InitDefaultInfoHandler.ashx?flag=yazhu&shebei=3-9", { shebei: "" }, function (result) {
                    if (result != "") { $("div[id*='div_3-9']").html(result); }
                    setable(result, "frm_3-9", "btn_3-9"); warningset("3-9", "frm_3-9");
                });
                //ѹ��4����
                $.post("InitDefaultInfoHandler.ashx?flag=yazhu&shebei=4-1", { shebei: "" }, function (result) {
                    if (result != "") { $("div[id*='div_4-1']").html(result); }
                    setable(result, "frm_4-1", "btn_4-1"); warningset("4-1", "frm_4-1");
                });
                $.post("InitDefaultInfoHandler.ashx?flag=yazhu&shebei=4-2", { shebei: "" }, function (result) {
                    if (result != "") { $("div[id*='div_4-2']").html(result); }
                    setable(result, "frm_4-2", "btn_4-2"); warningset("4-2", "frm_4-2");
                });
                $.post("InitDefaultInfoHandler.ashx?flag=yazhu&shebei=4-3", { shebei: "" }, function (result) {
                    if (result != "") { $("div[id*='div_4-3']").html(result); }
                    setable(result, "frm_4-3", "btn_4-3"); warningset("4-3", "frm_4-3");
                });
                $.post("InitDefaultInfoHandler.ashx?flag=yazhu&shebei=4-4", { shebei: "" }, function (result) {
                    if (result != "") { $("div[id*='div_4-4']").html(result); }
                    setable(result, "frm_4-4", "btn_4-4"); warningset("4-4", "frm_4-4");
                });
                $.post("InitDefaultInfoHandler.ashx?flag=BULE&shebei=4-5", { shebei: "" }, function (result) {
                    if (result != "") { $("div[id*='div_bule4-5']").html(result); }
                    setable(result, "frm_bule4-5", "btn_bule4-5"); warningset("4-5", "frm_bule4-5");
                });
                $.post("InitDefaultInfoHandler.ashx?flag=BULE&shebei=4-6", { shebei: "" }, function (result) {
                    if (result != "") { $("div[id*='div_bule4-6']").html(result); }
                    setable(result, "frm_bule4-6", "btn_bule4-6"); warningset("4-6", "frm_bule4-6");
                });
                $.post("InitDefaultInfoHandler.ashx?flag=BULE&shebei=4-7", { shebei: "" }, function (result) {
                    if (result != "") { $("div[id*='div_bule4-7']").html(result); }
                    setable(result, "frm_bule4-7", "btn_bule4-7"); warningset("4-7", "frm_bule4-7");
                });
                $.post("InitDefaultInfoHandler.ashx?flag=BULE&shebei=4-8", { shebei: "" }, function (result) {
                    if (result != "") { $("div[id*='div_bule4-8']").html(result); }
                    setable(result, "frm_bule4-8", "btn_bule4-8"); warningset("4-8", "frm_bule4-8");
                });
                $.post("InitDefaultInfoHandler.ashx?flag=BULE&shebei=4-9", { shebei: "" }, function (result) {
                    if (result != "") { $("div[id*='div_bule4-9']").html(result); }
                    setable(result, "frm_bule4-9", "btn_bule4-9"); warningset("4-9", "frm_bule4-9");
                });
                //ѹ��5����
                $.post("InitDefaultInfoHandler.ashx?flag=yazhu&shebei=5-1", { shebei: "" }, function (result) {
                    if (result != "") { $("div[id*='div_5-1']").html(result); }
                    setable(result, "frm_5-1", "btn_5-1"); warningset("5-1", "frm_5-1");
                });
                $.post("InitDefaultInfoHandler.ashx?flag=yazhu&shebei=5-2", { shebei: "" }, function (result) {
                    if (result != "") { $("div[id*='div_5-2']").html(result); }
                    setable(result, "frm_5-2", "btn_5-2"); warningset("5-2", "frm_5-2");
                });
                $.post("InitDefaultInfoHandler.ashx?flag=yazhu&shebei=5-3", { shebei: "" }, function (result) {
                    if (result != "") { $("div[id*='div_5-3']").html(result); }
                    setable(result, "frm_5-3", "btn_5-3"); warningset("5-3", "frm_5-3");
                });
                //��������
                $.post("InitDefaultInfoHandler.ashx?flag=yazhu&shebei=PW01", { shebei: "" }, function (result) {
                    if (result != "") { $("div[id*='div_PW01']").html(result); }
                    setable(result, "frm_PW01", "btn_PW01"); warningset("PW01", "frm_PW01");
                });
                $.post("InitDefaultInfoHandler.ashx?flag=yazhu&shebei=PW02", { shebei: "" }, function (result) {
                    if (result != "") { $("div[id*='div_PW02']").html(result); }
                    setable(result, "frm_PW02", "btn_PW02"); warningset("PW02", "frm_PW02");
                });
                $.post("InitDefaultInfoHandler.ashx?flag=yazhu&shebei=PW03", { shebei: "" }, function (result) {
                    if (result != "") { $("div[id*='div_PW03']").html(result); }
                    setable(result, "frm_PW03", "btn_PW03"); warningset("PW03", "frm_PW03");
                });
                $.post("InitDefaultInfoHandler.ashx?flag=yazhu&shebei=PW04", { shebei: "" }, function (result) {
                    if (result != "") { $("div[id*='div_PW04']").html(result); }
                    setable(result, "frm_PW04", "btn_PW04"); warningset("PW04", "frm_PW04");
                });
                $.post("InitDefaultInfoHandler.ashx?flag=BULE&shebei=PW05", { shebei: "" }, function (result) {
                    if (result != "") { $("div[id*='div_PW05']").html(result); }
                    setable(result, "frm_PW05", "btn_PW05"); warningset("PW05", "frm_PW05");
                });
                $.post("InitDefaultInfoHandler.ashx?flag=BULE&shebei=PW06", { shebei: "" }, function (result) {
                    if (result != "") { $("div[id*='div_PW06']").html(result); }
                    setable(result, "frm_PW06", "btn_PW06"); warningset("PW06", "frm_PW06");
                });
                $.post("InitDefaultInfoHandler.ashx?flag=BULE&shebei=PW07", { shebei: "" }, function (result) {
                    if (result != "") { $("div[id*='div_PW07']").html(result); }
                    setable(result, "frm_PW07", "btn_PW07"); warningset("PW07", "frm_PW07");
                });
            }
            //���ػ�
            $("[deviceid]").click(function (e) {
                var Equipno = $(this).attr("deviceid");
                var Logaction = $(this).text();
                var Actionmark = "";
                var Actionreason = "";
                $.ajax({
                    type: "post", //Ҫ��post��ʽ                 
                    url: "default.aspx/InsertEquipLogStatus", //��������ҳ��ͷ�����SayHello
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    data: "{equipno:'" + Equipno + "',logaction:'" + Logaction + "',actionmark:'" + Actionmark + "',actionreason:'" + Actionreason + "'}",
                    // data: "{'equipno':'" + Equipno + "','logaction':'" + Logaction + "','actionmark':'" + Actionmark +"','actionreason':'" + Actionreason+"' }",
                    success: function (data) {
                        if (data.d != "") //���ص�������data.d��ȡ����Logaction + "�ɹ�."
                        {
                            layer.alert(data.d);
                        }
                        else {
                            layer.alert(Logaction + "ʧ��.");
                        }
                    },
                    error: function (err) {
                        layer.alert(err);
                    }
                });
            });

        })//end ready





    </script>
    <%-- ����--%>
    <div class="row row-container">
        <div class="col-sm-12">
            <div class="panel panel-info">
                <div class="panel-heading ">
                    <strong>��������</strong>
                    <div class="btn-group">
                        <div class="area_drop" data-toggle="dropdown">
                            <span class="caret"></span>
                        </div>
                        <ul class="dropdown-menu" style="color: black" role="menu">
                            <li><a href="#" id="loginRL" name="rlq">����</a></li>
                            <li><a href="#" id="logoutRL" name="����">�ǳ�</a></li>
                            <li class="divider"></li>
                            <li><a href="HandOverQuery.aspx?quyu=rlq&gongwei=����" name="����" target="_blank">���Ӱ��ѯ</a></li>
                            <li><a href="/kanban/Job_Monitor.aspx" name="">�豸ά�޿���</a></li>
                            <li><a href="/kanban/MoJuBX_List.aspx" name="">ģ��ά�޿���</a></li>
                            <li><a href="/kanban/DJ_List.aspx" name="">ʵ���Ҵ��쿴��</a></li>
                        </ul>
                    </div>
                </div>
                <div class="panel-body">
                    <div class="col-sm-12 ">
                        <div class="area_block">
                            <div class="btn btn-large  btn-success" name="frm_A">
                                <div class="btn-group">
                                    <div class="btn btn-primary " data-toggle="dropdown" name="btn_A">
                                        ����¯A(0.6T)
                                    </div>
                                    <ul class="dropdown-menu" style="color: Black" role="menu">
                                        <li><a href="javascript:void(0)" deviceid="A" style="display: inline">����</a>
                                            <a href="Javascript: void(0)" deviceid="A" style="display: inline">�ػ�</a></li>
                                        <li><a href="InputMat.aspx?deviceid=A&devicename=����¯A" target="_blank">Ͷ�ϼ�¼(�˹�����)</a></li>  
                                        <li><a href="/RongLian/InputMat.aspx?deviceid=A&devicename=����¯A" target="_blank">Ͷ�ϼ�¼(�Զ�����)</a></li>
                                      
                                        <li><a href="/jinglian/JingLian_DZ.aspx?deviceid=A&devicename=����¯A" target="_blank">��������������¼</a></li>
                                        <li class="divider"></li>
                                        <li><a href="/RL/InPutQuery_RL.aspx?deviceid=A" target="_blank">Ͷ�ϲ�ѯ</a></li>
                                        <li><a href="/RL/TouLiaoTongJi.aspx?deviceid=A" target="_blank">Ͷ��ͳ��</a></li>
                                        <li><a href="/jinglian/JingLian_DZ_Query.aspx?deviceid=A&devicename=����¯A" target="_blank">��������������¼��ѯ</a></li>

                                    </ul>
                                </div>
                                <div class="area " id="div_A" name="A">
                                </div>
                            </div>
                        </div>
                        <div class="area_block">
                            <div class="btn btn-large btn-success" name="frm_B">
                                <div class="btn-group">
                                    <div class="btn btn-primary" data-toggle="dropdown" name="btn_B">
                                        ����¯B(1.2T)
                                    </div>
                                    <ul class="dropdown-menu" style="color: Black" role="menu">
                                        <li><a href="javascript:void(0)" deviceid="B" style="display: inline" name="B">����</a>
                                            <a href="Javascript: void(0)" deviceid="B" style="display: inline" name="B">�ػ�</a></li>
                                        <li><a href="InputMat.aspx?deviceid=B&devicename=����¯B" target="_blank">Ͷ�ϼ�¼(�˹�����)</a></li>
                                        <li><a href="/RongLian/InputMat.aspx?deviceid=B&devicename=����¯B" target="_blank">Ͷ�ϼ�¼(�Զ�����)</a></li>                                        
                                        <li><a href="/jinglian/JingLian_DZ.aspx?deviceid=B&devicename=����¯B" target="_blank">��������������¼</a></li>
                                        <li class="divider"></li>

                                        <li><a href="/RL/InPutQuery_RL.aspx?deviceid=B" target="_blank">Ͷ�ϲ�ѯ</a></li>
                                        <li><a href="/RL/TouLiaoTongJi.aspx?deviceid=B" target="_blank">Ͷ��ͳ��</a></li>
                                        <li><a href="/jinglian/JingLian_DZ_Query.aspx?deviceid=B&devicename=����¯B" target="_blank">��������������¼��ѯ</a></li>
                                        <li class="dropdown-submenu" style="display: none">
                                            <a tabindex="-1" href="javascript:;">Menu�˵�</a>
                                            <ul class="dropdown-menu">
                                                <li><a tabindex="-1" href="javascript:;">�����˵�</a></li>
                                                <li class="divider"></li>
                                                <li class="dropdown-submenu">
                                                    <a href="javascript:;">�����˵�</a>
                                                    <ul class="dropdown-menu">
                                                        <li><a href="javascript:;">�����˵�</a></li>
                                                    </ul>
                                                </li>
                                            </ul>
                                        </li>

                                    </ul>
                                </div>
                                <div class="area" id="div_B" name="B">
                                </div>
                            </div>
                        </div>

                        <div class="area_block">
                            <div class="btn btn-large   btn-success" name="frm_C">
                                <div class="btn-group">
                                    <div class="btn btn-primary " data-toggle="dropdown" name="btn_C">
                                        ����¯C(2.5T)
                                    </div>
                                    <ul class="dropdown-menu" style="color: Black" role="menu">
                                        <li><a href="javascript:void(0)" deviceid="C" style="display: inline">����</a>
                                            <a href="Javascript: void(0)" deviceid="C" style="display: inline">�ػ�</a></li>
                                        <li><a href="InputMat.aspx?deviceid=C&devicename=����¯C" target="_blank">Ͷ�ϼ�¼(�˹�����)</a></li>
                                        <li><a href="/RongLian/InputMat.aspx?deviceid=C&devicename=����¯C" target="_blank">Ͷ�ϼ�¼(�Զ�����)</a></li>
                                        <li><a href="/jinglian/JingLian_DZ_C.aspx?deviceid=C&devicename=����¯C" target="_blank">��������������¼</a></li>
                                        <li class="divider"></li>
                                        <li><a href="/RL/InPutQuery_RL.aspx?deviceid=C" target="_blank">Ͷ�ϲ�ѯ</a></li>
                                        <li><a href="/RL/TouLiaoTongJi.aspx?deviceid=C" target="_blank">Ͷ��ͳ��</a></li>
                                        <li><a href="/jinglian/JingLian_DZ_Query.aspx?deviceid=C&devicename=����¯C" target="_blank">��������������¼��ѯ</a></li>
                                    </ul>
                                </div>
                                <div class="area" id="div_C" name="C">
                                </div>
                            </div>
                        </div>
                        <div class="area_block">
                            <div class="btn btn-success" name="frm_D">
                                <div class="btn-group">
                                    <div class="btn btn-primary" data-toggle="dropdown" name="btn_D">
                                        ����¯D(1.5T)
                                    </div>
                                    <ul class="dropdown-menu" style="color: Black" role="menu">
                                        <li><a href="javascript:void(0)" deviceid="D" style="display: inline">����</a>
                                            <a href="Javascript: void(0)" deviceid="D" style="display: inline">�ػ�</a></li>
                                        <li><a href="InputMat.aspx?deviceid=D&devicename=����¯D" target="_blank">Ͷ�ϼ�¼(�˹�����)</a></li>
                                        <li><a href="/RongLian/InputMat.aspx?deviceid=D&devicename=����¯D" target="_blank">Ͷ�ϼ�¼(�Զ�����)</a></li>
                                        <li><a href="/jinglian/JingLian_DZ.aspx?deviceid=D&devicename=����¯D" target="_blank">��������������¼</a></li>
                                        <li class="divider"></li>
                                        <li><a href="/RL/InPutQuery_RL.aspx?deviceid=D" target="_blank">Ͷ�ϲ�ѯ</a></li>
                                        <li><a href="/RL/TouLiaoTongJi.aspx?deviceid=D" target="_blank">Ͷ��ͳ��</a></li>
                                        <li><a href="/jinglian/JingLian_DZ_Query.aspx?deviceid=D&devicename=����¯D" target="_blank">��������������¼��ѯ</a></li>
                                    </ul>
                                </div>
                                <div class="area" id="div_D" name="D">
                                </div>
                            </div>
                        </div>
                        <div class="area_block">
                            <div class="btn btn-success" name="frm_E">
                                <div class="btn-group">
                                    <div class="btn btn-primary" data-toggle="dropdown" name="btn_E">
                                        ����¯E(0.8T)
                                    </div>
                                    <ul class="dropdown-menu" style="color: Black" role="menu">
                                        <li><a href="javascript:void(0)" deviceid="E" style="display: inline">����</a>
                                            <a href="Javascript: void(0)" deviceid="E" style="display: inline">�ػ�</a></li>
                                        <li><a href="InputMat.aspx?deviceid=E&devicename=����¯E" target="_blank">Ͷ�ϼ�¼(�˹�����)</a></li>
                                        <li><a href="/RongLian/InputMat.aspx?deviceid=E&devicename=����¯E" target="_blank">Ͷ�ϼ�¼(�Զ�����)</a></li>
                                        <li><a href="/jinglian/JingLian_DZ_E.aspx?deviceid=E&devicename=����¯E" target="_blank">��������������¼</a></li>
                                        <li class="divider"></li>
                                        <li><a href="/RL/InPutQuery_RL.aspx?deviceid=E" target="_blank">Ͷ�ϲ�ѯ</a></li>
                                        <li><a href="/RL/TouLiaoTongJi.aspx?deviceid=E" target="_blank">Ͷ��ͳ��</a></li>
                                        <li><a href="/jinglian/JingLian_DZ_Query.aspx?deviceid=C&devicename=����¯E" target="_blank">��������������¼��ѯ</a></li>
                                    </ul>
                                </div>
                                <div class="area" id="div_E" name="E">
                                </div>
                            </div>
                        </div>
                        <div class="area_block">
                            <div class="btn btn-large   btn-success" name="frm_F">
                                <div class="btn-group">
                                    <div class="btn btn-primary " data-toggle="dropdown" name="btn_F">
                                        ����¯F(0.8T)
                                    </div>
                                    <ul class="dropdown-menu" style="color: Black" role="menu">
                                        <li><a href="javascript:void(0)" deviceid="F" style="display: inline">����</a>
                                            <a href="Javascript: void(0)" deviceid="F" style="display: inline">�ػ�</a></li>
                                        <li><a href="InputMat.aspx?deviceid=F&devicename=����¯F" target="_blank">Ͷ�ϼ�¼(�˹�����)</a></li>
                                        <li><a href="/RongLian/InputMat.aspx?deviceid=F&devicename=����¯F" target="_blank">Ͷ�ϼ�¼(�Զ�����)</a></li>
                                        <li><a href="/jinglian/JingLian_DZ.aspx?deviceid=F&devicename=����¯F" target="_blank">��������������¼</a></li>
                                        <li class="divider"></li>
                                        <li><a href="/RL/InPutQuery_RL.aspx?deviceid=F" target="_blank">Ͷ�ϲ�ѯ</a></li>
                                        <li><a href="/RL/TouLiaoTongJi.aspx?deviceid=F" target="_blank">Ͷ��ͳ��</a></li>
                                        <li><a href="/jinglian/JingLian_DZ_Query.aspx?deviceid=F&devicename=����¯F" target="_blank">��������������¼��ѯ</a></li>
                                    </ul>
                                </div>
                                <div class="area" id="div_F" name="F">
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <%-- ������һ--%>
    <div class="row row-container">
        <div class="col-sm-12">
            <div class="panel panel-info">
                <div class="panel-heading">
                    <strong>������Һ����</strong>
                    <div class="btn-group">
                        <div class="area_drop" data-toggle="dropdown">
                            <span class="caret"></span>
                        </div>
                        <ul class="dropdown-menu" style="color: red" role="menu">
                            <li><a href="#" id="loginJL" name="jlq">����</a></li>
                            <li><a href="#" id="logoutJL" name="����">�ǳ�</a></li>
                            <li class="divider"></li>
                            <li><a href="HandOverQuery.aspx?quyu=jlq&gongwei=����" name="����" target="_blank">���Ӱ��ѯ</a></li>
                        </ul>
                    </div>
                </div>
                <div class="panel-body">
                    <div class="col-md-12">
                        <div class="area_block ">
                            <div class="btn btn-success" name="frm_JL1">
                                <div class="btn-group">
                                    <div class="btn btn-primary" data-toggle="dropdown" name="btn_JL1">
                                        ��������1<span class="caret"></span>
                                    </div>
                                    <ul class="dropdown-menu" style="color: Black" role="menu">
                                        <li><a href="javascript:void(0)" deviceid="������,������1" style="display: inline">����</a>
                                            <a href="Javascript:void(0)" deviceid="������,������1" style="display: inline">�ػ�</a></li>
                                        <li><a href="/jinglian/JinLian_Hydrogen.aspx?gongwei=<%=HttpUtility.UrlEncode("������1#") %>"target="_blank">��������1��¼</a></li>
                                        <li class="divider"></li>
                                        <li><a href="/jinglian/JinLian_Query.aspx?gongwei=<%=HttpUtility.UrlEncode("������1#") %>" target="_blank">��������1��ѯ</a></li>
                                        <li><a href="/TJ/JingLian_TJ_Report.aspx" target="_blank">��������ͳ�Ʒ���</a></li>

                                    </ul>
                                </div>
                                <div class="area" id="div_JL1" name="div_JL1">
                                </div>
                            </div>
                        </div>


                           <div class="area_block ">
                            <div class="btn btn-success" name="frm_JL2">
                                <div class="btn-group">
                                    <div class="btn btn-primary" data-toggle="dropdown" name="btn_JL2">
                                        ��������2<span class="caret"></span>
                                    </div>
                                    <ul class="dropdown-menu" style="color: Black" role="menu">
                                        <li><a href="javascript:void(0)" deviceid="������,������2" style="display: inline">����</a>
                                            <a href="Javascript:void(0)" deviceid="������,������2" style="display: inline">�ػ�</a></li>
                                        <li><a href="/jinglian/JinLian_Hydrogen.aspx?gongwei=<%=HttpUtility.UrlEncode("������2#") %>"target="_blank">��������2��¼</a></li>
                                        <li class="divider"></li>
                                        <li><a href="/jinglian/JinLian_Query.aspx?gongwei=<%=HttpUtility.UrlEncode("������2#") %>" target="_blank">��������2��ѯ</a></li>
                                        <li><a href="/TJ/JingLian_TJ_Report.aspx" target="_blank">��������ͳ�Ʒ���</a></li>

                                    </ul>
                                </div>
                                <div class="area" id="div_JL2" name="div_JL2">
                                </div>
                            </div>
                        </div>

                        <div class="area_block">
                            <div class="btn btn-success " name="frm_ZY">
                                <div class="btn-group">
                                    <div class="btn btn-primary " data-toggle="dropdown" name="btn_ZY">
                                        ת�˰�<span class="caret"></span>
                                    </div>
                                    <ul class="dropdown-menu" style="color: Black" role="menu">
                                        <li><a href="javascript:void(0)" deviceid="ת�˰�" style="display: inline">����</a>
                                            <a href="Javascript:void(0)" deviceid="ת�˰�" style="display: inline">�ػ�</a></li>
                                        <li><a href="/jinglian/ZYB_Clear.aspx" target="_blank">ת�˰�����</a></li>
                                        <li><a href="/jinglian/ZYB_JY.aspx" target="_blank">ת�˰���Һ</a></li>
                                        <li class="divider"></li>
                                        <li><a href="/jinglian/ZYB_Query.aspx" target="_blank">�����ѯ</a></li>
                                        <li><a href="/jinglian/ZYB_JY_Query.aspx" target="_blank">��Һ��ѯ</a></li>
                                    </ul>
                                </div>
                                <div class="area" id="div_ZY" name="div_ZY">
                                </div>
                            </div>
                        </div>
                        <div class="area_block">
                            <div class="btn btn-gray " name="frm_KB">
                                <div class="btn-group">
                                    <div class="btn btn-primary disabled" data-toggle="dropdown" name="btn_KB">
                                        ������
                                    </div>
                                    <ul class="dropdown-menu" style="color: Black" role="menu">
                                        <li><a href="#">Ͷ�ϼ�¼</a></li>
                                        <li class="divider"></li>
                                        <li><a href="#">Ͷ�ϲ�ѯ</a></li>
                                        <li><a href="#">Ͷ��ͳ��</a></li>
                                    </ul>
                                </div>
                                <div class="area">
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>



    <%--ѹ��  ʵ����--%>
    <div class="row  row-container" style="display: none">
        <div class="col-md-12">
            <div class="panel panel-info">
                <div class="panel-heading">
                    <strong>ѹ��ʵ����</strong>
                    <div class="btn-group">
                        <div class="area_drop" data-toggle="dropdown">
                            <span class="caret"></span>
                        </div>
                        <ul class="dropdown-menu" style="color: red" role="menu">
                            <li><a href="#" id="loginYZSYS" name="yzsys">����</a></li>
                            <li><a href="#" id="logoutYZSYS" name="ѹ��ʵ����">�ǳ�</a></li>
                            <li class="divider"></li>
                            <li><a href="HandOverQuery.aspx?quyu=yzsys&gongwei=ѹ��ʵ����" name="ѹ��ʵ����" target="_blank">���Ӱ��ѯ</a></li>
                        </ul>
                    </div>
                </div>
                <div class="panel-body">
                    <div class="col-xs-12 col-sm-12  col-md-12 col-lg-12">
                        <div class="area_block">
                            <div class="btn  btn-success " name="frm_GP">
                                <div class="btn-group">
                                    <div class="btn btn-primary " data-toggle="dropdown" name="btn_GP">
                                        ����
                                    </div>
                                    <ul class="dropdown-menu" style="color: Black" role="menu">
                                        <li><a href="/jinglian/GP_CS.aspx" target="_blank">���ײ�����¼</a></li>
                                        <li class="divider"></li>
                                        <li><a href="/jinglian/GP_detail_Query.aspx" target="_blank">���ײ�ѯ</a></li>
                                        <li><a href="/jinglian/GPTongJi.aspx" target="_blank">����ͳ��</a></li>
                                        <li><a href="/TJ/GP_Element_TJ.aspx" target="_blank">���׳ɷ�ͳ��</a></li>
                                    </ul>
                                </div>
                                <div class="area" id="div_GP">
                                </div>
                            </div>
                        </div>
                        <div class="area_block">
                            <div class="btn  btn-gray disabled" name="frm_X1">
                                <div class="btn-group">
                                    <div class="btn btn-primary disabled" data-toggle="dropdown" name="btn_X1">
                                        X �� 1
                                    </div>
                                    <ul class="dropdown-menu" style="color: Black" role="menu">
                                        <li><a href="#">X���¼</a></li>
                                        <li class="divider"></li>
                                        <li><a href="#">X���ѯ</a></li>
                                    </ul>
                                </div>
                                <div class="area">
                                </div>
                            </div>
                        </div>
                        <div class="area_block">
                            <div class="btn  btn-gray disabled">
                                <div class="btn-group">
                                    <div class="btn btn-primary disabled" data-toggle="dropdown">
                                        X �� 2
                                    </div>
                                    <ul class="dropdown-menu" style="color: Black" role="menu">
                                        <li><a href="#">X���¼</a></li>
                                        <li class="divider"></li>
                                        <li><a href="#">X���ѯ</a></li>

                                    </ul>
                                </div>
                                <div class="area">
                                </div>
                            </div>
                        </div>
                        <div class="area_block">
                            <div class="btn btn-gray disabled">
                                <div class="btn-group">
                                    <div class="btn btn-primary disabled" data-toggle="dropdown">
                                        ���������
                                    </div>
                                    <ul class="dropdown-menu" style="color: Black" role="menu">
                                        <li><a href="#">���������¼</a></li>
                                        <li class="divider"></li>
                                        <li><a href="#">���������ѯ</a></li>
                                    </ul>
                                </div>
                                <div class="area">
                                </div>
                            </div>
                        </div>
                        <div class="area_block">
                            <div class="btn btn-gray disabled">
                                <div class="btn-group">
                                    <div class="btn btn-primary disabled" data-toggle="dropdown">
                                        Ӳ�ȼ�
                                    </div>
                                    <ul class="dropdown-menu" style="color: Black" role="menu">
                                        <li><a href="#">Ӳ�Ȳ��Լ�¼</a></li>
                                        <li class="divider"></li>
                                        <li><a href="#">Ӳ�Ȳ��Բ�ѯ</a></li>
                                    </ul>
                                </div>
                                <div class="area">
                                </div>
                            </div>
                        </div>
                        <div class="area_block">
                            <div class="btn btn-gray disabled">
                                <div class="btn-group">
                                    <div class="btn btn-primary disabled" data-toggle="dropdown">
                                        �������
                                    </div>
                                    <ul class="dropdown-menu" style="color: Black" role="menu">
                                        <li><a href="#">���������¼</a></li>
                                        <li class="divider"></li>
                                        <li><a href="#">���������ѯ</a></li>
                                    </ul>
                                </div>
                                <div class="area">
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <%--ѹ��1����--%>
    <div class="row  row-container">
        <div class="col-md-12">
            <div class="panel panel-info">
                <div class="panel-heading">
                    <strong>ѹ��1����</strong>
                    <div class="btn-group">
                        <div class="area_drop" data-toggle="dropdown">
                            <span class="caret"></span>
                        </div>
                        <ul class="dropdown-menu" style="color: black" role="menu">
                            <li><span href="#" id="loginyz1q" name="yz1q" title="yz1q" style="padding-left: 20px">����</span></li>
                            <li><span href="#" id="logoutyz1q" name="����" title="����" style="padding-left: 20px">�ǳ�</span></li>
                            <li class="divider"></li>
                            <li><a href="HandOverQuery.aspx?quyu=yz1q&gongwei=����" name="����" target="_blank">���Ӱ��ѯ</a></li>
                        </ul>
                    </div>
                </div>
                <div class="panel-body">
                    <div class="area_block">
                        <div class="area_block">
                            <div class="btn btn-success " name="frm_1-1">
                                <div class="btn-group">
                                    <div class="btn btn-primary disabled" data-toggle="dropdown" name="btn_1-1">
                                        1-1(350T) ��֥
                                    </div>
                                    <ul class="dropdown-menu" style="color: Black" role="menu">
                                        <li><a href="javascript:void(0)" deviceid="1-1" style="display: inline" name="1-1">����</a>
                                            <a href="Javascript: void(0)" deviceid="1-1" style="display: inline" name="1-1">�ػ�</a></li>
                                        <li><a href="BuLeHuanMo/BuLeHuanMo.aspx?deviceid=1-1" target="_blank">��ģ��¼</a></li>
                                        <li><a href="MoJu/MoJuBX.aspx?deviceid=1-1 " target="_blank">ģ�߱���.ά��.ȷ��</a></li>

                                        <li><a href="http://172.16.5.7:8088/JobPortal/login.aspx" target="_blank">�豸����</a></li>
                                        <%--<li><a href="/YaSheTou/YST_Record.aspx?deviceid=1-1" target="_blank">ѹ��ͷ��¼</a></li>--%>
                                        <li class="divider"></li>
                                        <li><a href="JingLian/ChangeMo_Query.aspx?deviceid=1-1" target="_blank">��ģ��ѯ</a></li>
                                        <li><a href="MoJu/MojuBX_Query.aspx?deviceid=1-1 " target="_blank">ģ�߱��޲�ѯ</a></li>
                                        <li><a href="http://172.16.5.6:8080/SheBei/Job%20monitoring.aspx " target="_blank">�豸���޲�ѯ</a></li>
                                        <li><a href="TJ/Guzhang_Rate_Report.aspx?quyu=yz1q" target="_blank">�豸����ͳ��</a></li>
                                        <li><a href="TJ/ChangeMo_TJ_Report.aspx?quyu=yz1q" target="_blank">��ģͳ��</a></li>
                                        <li><a href="MoJu/Moju_BX_TongJi.aspx" target="_blank">ģ�߱���ͳ��</a></li>
                                        <li><a href="/YaSheTou/YST_Query.aspx?deviceid=1-1" target="_blank">ѹ��ͷ��ѯ</a></li>
                                         <li class="divider"></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuQuery.aspx" target="_blank">ģ���嵥</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuPJ_List.aspx" target="_blank">ģ�߱���</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuPJRK_List.aspx" target="_blank">ģ�߱������</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuPJLY_List.aspx" target="_blank">ģ�߱�������</a></li>
                                         <li><a href="Jiaju/JiaJu_List.aspx" target="_blank">�о�����</a></li>
                                           <li><a href="http://172.16.5.6:8080/wuliu/MoJuLY_List.aspx" target="_blank">ģ������</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuRK_List.aspx" target="_blank">ģ�����</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuCL_List.aspx" target="_blank">ģ�ߴ���</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuBY_List.aspx" target="_blank">ģ�߱���</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MojuWX_List.aspx" target="_blank">ģ������</a></li>
                                        <li><a href="/YaSheTou/YST_Maintain.aspx" target="_blank">ѹ��ͷ�嵥</a></li>
                                        <li><a href="/YaSheTou/YST_LY_QS.aspx" target="_blank">ѹ��ͷ����ͳ��</a></li>
                                        <li class="divider"></li>
                                         <li><a href="JingLian/YZQB_SH.aspx" target="_blank">���</a></li>
                                         <li><a href="shenhe/YZGX_XJ.aspx?deviceid=1-1" target="_blank">ѹ��������</a></li>
                                         <li class="divider"></li>
                                         <li><a href="shenhe/YZCheck_List.aspx" target="_blank">�豸���</a></li>
                                         <li class="divider"></li>
                                        <li><a href="E_data/EData_Query.aspx?deviceid=1-1 " target="_blank" style="display: inline">��̨���ݲ�ѯ</a>
                                            <a href="#" target="_blank" style="display: inline">����ͳ��</a></li>
                                    </ul>
                                </div>
                                <div class="area" id="div_1-1">
                                </div>
                            </div>
                        </div>
                        <div class="area_block">
                            <div class="btn btn-success " name="frm_1-2">
                                <div class="btn-group">
                                    <div class="btn btn-primary disabled" data-toggle="dropdown" name="btn_1-2">
                                        1-2(350T)��֥
                                    </div>
                                    <ul class="dropdown-menu" style="color: Black" role="menu">
                                        <li><a href="javascript:void(0)" deviceid="1-2" style="display: inline" name="1-2">����</a>
                                            <a href="Javascript: void(0)" deviceid="1-2" style="display: inline" name="1-2">�ػ�</a></li>
                                        <li><a href="BuLeHuanMo/BuLeHuanMo.aspx?deviceid=1-2" target="_blank">��ģ��¼</a></li>
                                        <li><a href="MoJu/MoJuBX.aspx?deviceid=1-2 " target="_blank">ģ�߱���.ά��.ȷ��</a></li>
                                        <li><a href="http://172.16.5.7:8088/JobPortal/login.aspx" target="_blank">�豸����</a></li>
                                        <%--<li><a href="/YaSheTou/YST_Record.aspx?deviceid=1-2" target="_blank">ѹ��ͷ��¼</a></li>--%>
                                        <li class="divider"></li>
                                        <li><a href="JingLian/ChangeMo_Query.aspx?deviceid=1-2" target="_blank">��ģ��ѯ</a></li>
                                        <li><a href="MoJu/MojuBX_Query.aspx?deviceid=1-2 " target="_blank">ģ�߱��޲�ѯ</a></li>
                                        <li><a href="http://172.16.5.6:8080/SheBei/Job%20monitoring.aspx " target="_blank">�豸���޲�ѯ</a></li>
                                        <li><a href="TJ/Guzhang_Rate_Report.aspx?quyu=yz1q" target="_blank">�豸����ͳ��</a></li>
                                        <li><a href="TJ/ChangeMo_TJ_Report.aspx?quyu=yz1q" target="_blank">��ģͳ��</a></li>
                                        <li><a href="MoJu/Moju_BX_TongJi.aspx" target="_blank">ģ�߱���ͳ��</a></li>
                                        <li><a href="/YaSheTou/YST_Query.aspx?deviceid=1-2" target="_blank">ѹ��ͷ��ѯ</a></li>
                                        <li class="divider"></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuQuery.aspx" target="_blank">ģ���嵥</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuPJ_List.aspx" target="_blank">ģ�߱���</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuPJRK_List.aspx" target="_blank">ģ�߱������</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuPJLY_List.aspx" target="_blank">ģ�߱�������</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuLY_List.aspx" target="_blank">ģ������</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuRK_List.aspx" target="_blank">ģ�����</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuCL_List.aspx" target="_blank">ģ�ߴ���</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuBY_List.aspx" target="_blank">ģ�߱���</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MojuWX_List.aspx" target="_blank">ģ������</a></li>
                                        <li><a href="/YaSheTou/YST_Maintain.aspx" target="_blank">ѹ��ͷ�嵥</a></li>
                                        <li class="divider"></li>
                                        <li><a href="E_data/EData_Query.aspx?deviceid=1-2" target="_blank">��̨���ݲ�ѯ</a></li>
                                        <li><a href="#" target="_blank">��̨����ͳ��</a></li>
                                    </ul>
                                </div>
                                <div class="area" id="div_1-2">
                                </div>
                            </div>
                        </div>
                        <div class="area_block">
                            <div class="btn btn-success " name="frm_1-3">
                                <div class="btn-group">
                                    <div class="btn btn-primary disabled" data-toggle="dropdown" name="btn_1-3">
                                        1-3(350T)��֥
                                    </div>
                                    <ul class="dropdown-menu" style="color: Black" role="menu">
                                        <li><a href="javascript:void(0)" deviceid="1-3" style="display: inline" name="1-3">����</a>
                                            <a href="Javascript: void(0)" deviceid="1-3" style="display: inline" name="1-3">�ػ�</a></li>
                                        <li><a href="BuLeHuanMo/BuLeHuanMo.aspx?deviceid=1-3" target="_blank">��ģ��¼</a></li>
                                        <li><a href="MoJu/MoJuBX.aspx?deviceid=1-3 " target="_blank">ģ�߱���.ά��.ȷ��</a></li>
                                        <li><a href="http://172.16.5.7:8088/JobPortal/login.aspx" target="_blank">�豸����</a></li>
                                        <%--<li><a href="/YaSheTou/YST_Record.aspx?deviceid=1-3" target="_blank">ѹ��ͷ��¼</a></li>--%>
                                        <li class="divider"></li>
                                        <li><a href="JingLian/ChangeMo_Query.aspx?deviceid=1-3" target="_blank">��ģ��ѯ</a></li>
                                        <li><a href="MoJu/MojuBX_Query.aspx?deviceid=1-3 " target="_blank">ģ�߱��޲�ѯ</a></li>
                                        <li><a href="http://172.16.5.6:8080/SheBei/Job%20monitoring.aspx " target="_blank">�豸���޲�ѯ</a></li>
                                        <li><a href="TJ/Guzhang_Rate_Report.aspx?quyu=yz1q" target="_blank">�豸����ͳ��</a></li>
                                        <li><a href="TJ/ChangeMo_TJ_Report.aspx?quyu=yz1q" target="_blank">��ģͳ��</a></li>
                                        <li><a href="MoJu/Moju_BX_TongJi.aspx" target="_blank">ģ�߱���ͳ��</a></li>
                                        <li><a href="/YaSheTou/YST_Query.aspx?deviceid=1-3" target="_blank">ѹ��ͷ��ѯ</a></li>
                                        <li class="divider"></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuQuery.aspx" target="_blank">ģ���嵥</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuPJ_List.aspx" target="_blank">ģ�߱���</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuPJRK_List.aspx" target="_blank">ģ�߱������</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuPJLY_List.aspx" target="_blank">ģ�߱�������</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuLY_List.aspx" target="_blank">ģ������</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuRK_List.aspx" target="_blank">ģ�����</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuCL_List.aspx" target="_blank">ģ�ߴ���</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuBY_List.aspx" target="_blank">ģ�߱���</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MojuWX_List.aspx" target="_blank">ģ������</a></li>
                                        <li><a href="/YaSheTou/YST_Maintain.aspx" target="_blank">ѹ��ͷ�嵥</a></li>
                                        <li class="divider"></li>
                                        <li><a href="E_data/EData_Query.aspx?deviceid=1-3" target="_blank">��̨���ݲ�ѯ</a></li>
                                        <li><a href="#" target="_blank">��̨����ͳ��</a></li>
                                    </ul>
                                </div>
                                <div class="area" id="div_1-3">
                                </div>
                            </div>
                        </div>
                        <div class="area_block">
                            <div class="btn btn-success " name="frm_1-4">
                                <div class="btn-group">
                                    <div class="btn btn-primary disabled" data-toggle="dropdown" name="btn_1-4">
                                        1-4(350T)��֥
                                    </div>
                                    <ul class="dropdown-menu" style="color: Black" role="menu">
                                        <li><a href="javascript:void(0)" deviceid="1-4" style="display: inline" name="1-4">����</a>
                                            <a href="Javascript: void(0)" deviceid="1-4" style="display: inline" name="1-4">�ػ�</a></li>
                                        <li><a href="BuLeHuanMo/BuLeHuanMo.aspx?deviceid=1-4" target="_blank">��ģ��¼</a></li>
                                        <li><a href="MoJu/MoJuBX.aspx?deviceid=1-4 " target="_blank">ģ�߱���.ά��.ȷ��</a></li>
                                        <li><a href="http://172.16.5.7:8088/JobPortal/login.aspx" target="_blank">�豸����</a></li>
                                        <%--<li><a href="/YaSheTou/YST_Record.aspx?deviceid=1-4" target="_blank">ѹ��ͷ��¼</a></li>--%>
                                        <li class="divider"></li>
                                        <li><a href="JingLian/ChangeMo_Query.aspx?deviceid=1-4" target="_blank">��ģ��ѯ</a></li>
                                        <li><a href="MoJu/MojuBX_Query.aspx?deviceid=1-4 " target="_blank">ģ�߱��޲�ѯ</a></li>
                                        <li><a href="http://172.16.5.6:8080/SheBei/Job%20monitoring.aspx " target="_blank">�豸���޲�ѯ</a></li>
                                        <li><a href="TJ/Guzhang_Rate_Report.aspx?quyu=yz1q" target="_blank">�豸����ͳ��</a></li>
                                        <li><a href="TJ/ChangeMo_TJ_Report.aspx?quyu=yz1q" target="_blank">��ģͳ��</a></li>
                                        <li><a href="MoJu/Moju_BX_TongJi.aspx" target="_blank">ģ�߱���ͳ��</a></li>
                                        <li><a href="/YaSheTou/YST_Query.aspx?deviceid=1-4" target="_blank">ѹ��ͷ��ѯ</a></li>
                                        <li class="divider"></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuQuery.aspx" target="_blank">ģ���嵥</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuPJ_List.aspx" target="_blank">ģ�߱���</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuPJRK_List.aspx" target="_blank">ģ�߱������</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuPJLY_List.aspx" target="_blank">ģ�߱�������</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuLY_List.aspx" target="_blank">ģ������</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuRK_List.aspx" target="_blank">ģ�����</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuCL_List.aspx" target="_blank">ģ�ߴ���</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuBY_List.aspx" target="_blank">ģ�߱���</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MojuWX_List.aspx" target="_blank">ģ������</a></li>
                                        <li><a href="/YaSheTou/YST_Maintain.aspx" target="_blank">ѹ��ͷ�嵥</a></li>
                                        <li class="divider"></li>
                                        <li><a href="#E_data/EData_Query.aspx?deviceid=1-4" target="_blank">��̨���ݲ�ѯ</a></li>
                                        <li><a href="#" target="_blank">��̨����ͳ��</a></li>
                                    </ul>
                                </div>
                                <div class="area" id="div_1-4">
                                </div>
                            </div>
                        </div>
                        <div class="area_block">
                            <div class="btn btn-success " name="frm_1-5">
                                <div class="btn-group">
                                    <div class="btn btn-primary disabled" data-toggle="dropdown" name="btn_1-5">
                                        1-5(350T)��֥
                                    </div>
                                    <ul class="dropdown-menu" style="color: Black" role="menu">
                                        <li><a href="javascript:void(0)" deviceid="1-5" style="display: inline" name="1-5">����</a>
                                            <a href="Javascript: void(0)" deviceid="1-5" style="display: inline" name="1-5">�ػ�</a></li>
                                        <li><a href="BuLeHuanMo/BuLeHuanMo.aspx?deviceid=1-5" target="_blank">��ģ��¼</a></li>
                                        <li><a href="MoJu/MoJuBX.aspx?deviceid=1-5 " target="_blank">ģ�߱���.ά��.ȷ��</a></li>
                                        <li><a href="http://172.16.5.7:8088/JobPortal/login.aspx" target="_blank">�豸����</a></li>
                                        <%--<li><a href="/YaSheTou/YST_Record.aspx?deviceid=1-5" target="_blank">ѹ��ͷ��¼</a></li>--%>
                                        <li class="divider"></li>
                                        <li><a href="JingLian/ChangeMo_Query.aspx?deviceid=1-5" target="_blank">��ģ��ѯ</a></li>
                                        <li><a href="MoJu/MojuBX_Query.aspx?deviceid=1-5 " target="_blank">ģ�߱��޲�ѯ</a></li>
                                        <li><a href="http://172.16.5.6:8080/SheBei/Job%20monitoring.aspx " target="_blank">�豸���޲�ѯ</a></li>
                                        <li><a href="TJ/Guzhang_Rate_Report.aspx?quyu=yz1q" target="_blank">�豸����ͳ��</a></li>
                                        <li><a href="TJ/ChangeMo_TJ_Report.aspx?quyu=yz1q" target="_blank">��ģͳ��</a></li>
                                        <li><a href="MoJu/Moju_BX_TongJi.aspx" target="_blank">ģ�߱���ͳ��</a></li>
                                        <li><a href="/YaSheTou/YST_Query.aspx?deviceid=1-5" target="_blank">ѹ��ͷ��ѯ</a></li>
                                        <li class="divider"></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuQuery.aspx" target="_blank">ģ���嵥</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuPJ_List.aspx" target="_blank">ģ�߱���</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuPJRK_List.aspx" target="_blank">ģ�߱������</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuPJLY_List.aspx" target="_blank">ģ�߱�������</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuLY_List.aspx" target="_blank">ģ������</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuRK_List.aspx" target="_blank">ģ�����</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuCL_List.aspx" target="_blank">ģ�ߴ���</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuBY_List.aspx" target="_blank">ģ�߱���</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MojuWX_List.aspx" target="_blank">ģ������</a></li>
                                        <li><a href="/YaSheTou/YST_Maintain.aspx" target="_blank">ѹ��ͷ�嵥</a></li>
                                        <li class="divider"></li>
                                        <li><a href="E_data/EData_Query.aspx?deviceid=1-5" target="_blank">��̨���ݲ�ѯ</a></li>
                                        <li><a href="#" target="_blank">��̨����ͳ��</a></li>
                                    </ul>
                                </div>
                                <div class="area" id="div_1-5">
                                </div>
                            </div>
                        </div>
                        <div class="area_block">
                            <div class="btn btn-success " name="frm_1-6">
                                <div class="btn-group">
                                    <div class="btn btn-primary disabled" data-toggle="dropdown" name="btn_1-6">
                                        1-6(350T)��֥
                                    </div>
                                    <ul class="dropdown-menu" style="color: Black" role="menu">
                                        <li><a href="javascript:void(0)" deviceid="1-6" style="display: inline" name="1-6">����</a>
                                            <a href="Javascript: void(0)" deviceid="1-6" style="display: inline" name="1-6">�ػ�</a></li>
                                        <li><a href="BuLeHuanMo/BuLeHuanMo.aspx?deviceid=1-6" target="_blank">��ģ��¼</a></li>
                                        <li><a href="MoJu/MoJuBX.aspx?deviceid=1-6 " target="_blank">ģ�߱���.ά��.ȷ��</a></li>
                                        <li><a href="http://172.16.5.7:8088/JobPortal/login.aspx" target="_blank">�豸����</a></li>
                                        <%--<li><a href="/YaSheTou/YST_Record.aspx?deviceid=1-6" target="_blank">ѹ��ͷ��¼</a></li>--%>
                                        <li class="divider"></li>
                                        <li><a href="JingLian/ChangeMo_Query.aspx?deviceid=1-6" target="_blank">��ģ��ѯ</a></li>
                                        <li><a href="MoJu/MojuBX_Query.aspx?deviceid=1-6 " target="_blank">ģ�߱��޲�ѯ</a></li>
                                        <li><a href="http://172.16.5.6:8080/SheBei/Job%20monitoring.aspx " target="_blank">�豸���޲�ѯ</a></li>
                                        <li><a href="TJ/Guzhang_Rate_Report.aspx?quyu=yz1q" target="_blank">�豸����ͳ��</a></li>
                                        <li><a href="TJ/ChangeMo_TJ_Report.aspx?quyu=yz1q" target="_blank">��ģͳ��</a></li>
                                        <li><a href="MoJu/Moju_BX_TongJi.aspx" target="_blank">ģ�߱���ͳ��</a></li>
                                        <li><a href="/YaSheTou/YST_Query.aspx?deviceid=1-6" target="_blank">ѹ��ͷ��ѯ</a></li>
                                        <li class="divider"></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuQuery.aspx" target="_blank">ģ���嵥</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuPJ_List.aspx" target="_blank">ģ�߱���</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuPJRK_List.aspx" target="_blank">ģ�߱������</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuPJLY_List.aspx" target="_blank">ģ�߱�������</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuLY_List.aspx" target="_blank">ģ������</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuRK_List.aspx" target="_blank">ģ�����</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuCL_List.aspx" target="_blank">ģ�ߴ���</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuBY_List.aspx" target="_blank">ģ�߱���</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MojuWX_List.aspx" target="_blank">ģ������</a></li>
                                        <li><a href="/YaSheTou/YST_Maintain.aspx" target="_blank">ѹ��ͷ�嵥</a></li>
                                        <li class="divider"></li>
                                        <li><a href="E_data/EData_Query.aspx?deviceid=1-6" target="_blank">��̨���ݲ�ѯ</a></li>
                                        <li><a href="#" target="_blank">��̨����ͳ��</a></li>
                                    </ul>
                                </div>
                                <div class="area" id="div_1-6">
                                </div>
                            </div>
                        </div>
                        <div class="area_block">
                            <div class="btn btn-success " name="frm_1-7">
                                <div class="btn-group">
                                    <div class="btn btn-primary disabled" data-toggle="dropdown" name="btn_1-7">
                                        1-7(350T)��֥
                                    </div>
                                    <ul class="dropdown-menu" style="color: Black" role="menu">
                                        <li><a href="javascript:void(0)" deviceid="1-7" style="display: inline" name="1-7">����</a>
                                            <a href="Javascript: void(0)" deviceid="1-7" style="display: inline" name="1-7">�ػ�</a></li>
                                        <li><a href="BuLeHuanMo/BuLeHuanMo.aspx?deviceid=1-7" target="_blank">��ģ��¼</a></li>
                                        <li><a href="MoJu/MoJuBX.aspx?deviceid=1-7 " target="_blank">ģ�߱���.ά��.ȷ��</a></li>
                                        <li><a href="http://172.16.5.7:8088/JobPortal/login.aspx" target="_blank">�豸����</a></li>
                                        <%--<li><a href="/YaSheTou/YST_Record.aspx?deviceid=1-7" target="_blank">ѹ��ͷ��¼</a></li>--%>
                                        <li class="divider"></li>
                                        <li><a href="JingLian/ChangeMo_Query.aspx?deviceid=1-7" target="_blank">��ģ��ѯ</a></li>
                                        <li><a href="MoJu/MojuBX_Query.aspx?deviceid=1-7 " target="_blank">ģ�߱��޲�ѯ</a></li>
                                        <li><a href="http://172.16.5.6:8080/SheBei/Job%20monitoring.aspx " target="_blank">�豸���޲�ѯ</a></li>
                                        <li><a href="TJ/Guzhang_Rate_Report.aspx?quyu=yz1q" target="_blank">�豸����ͳ��</a></li>
                                        <li><a href="TJ/ChangeMo_TJ_Report.aspx?quyu=yz1q" target="_blank">��ģͳ��</a></li>
                                        <li><a href="MoJu/Moju_BX_TongJi.aspx" target="_blank">ģ�߱���ͳ��</a></li>
                                        <li><a href="/YaSheTou/YST_Query.aspx?deviceid=1-7" target="_blank">ѹ��ͷ��ѯ</a></li>
                                        <li class="divider"></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuQuery.aspx" target="_blank">ģ���嵥</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuPJ_List.aspx" target="_blank">ģ�߱���</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuPJRK_List.aspx" target="_blank">ģ�߱������</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuPJLY_List.aspx" target="_blank">ģ�߱�������</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuLY_List.aspx" target="_blank">ģ������</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuRK_List.aspx" target="_blank">ģ�����</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuCL_List.aspx" target="_blank">ģ�ߴ���</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuBY_List.aspx" target="_blank">ģ�߱���</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MojuWX_List.aspx" target="_blank">ģ������</a></li>
                                        <li><a href="/YaSheTou/YST_Maintain.aspx" target="_blank">ѹ��ͷ�嵥</a></li>
                                        <li class="divider"></li>
                                        <li><a href="E_data/EData_Query.aspx?deviceid=1-7" target="_blank">��̨���ݲ�ѯ</a></li>
                                        <li><a href="#" target="_blank">��̨����ͳ��</a></li>
                                    </ul>
                                </div>
                                <div class="area" id="div_1-7">
                                </div>
                            </div>
                        </div>
                        <div class="area_block">
                            <div class="btn btn-success " name="frm_1-8">
                                <div class="btn-group">
                                    <div class="btn btn-primary disabled" data-toggle="dropdown" name="btn_1-8">
                                        1-8(350T)��֥
                                    </div>
                                    <ul class="dropdown-menu" style="color: Black" role="menu">
                                        <li><a href="javascript:void(0)" deviceid="1-8" style="display: inline" name="1-8">����</a>
                                            <a href="Javascript: void(0)" deviceid="1-8" style="display: inline" name="1-8">�ػ�</a></li>
                                        <li><a href="BuLeHuanMo/BuLeHuanMo.aspx?deviceid=1-8" target="_blank">��ģ��¼</a></li>
                                        <li><a href="MoJu/MoJuBX.aspx?deviceid=1-8 " target="_blank">ģ�߱���.ά��.ȷ��</a></li>
                                        <li><a href="http://172.16.5.7:8088/JobPortal/login.aspx" target="_blank">�豸����</a></li>
                                        <%--<li><a href="/YaSheTou/YST_Record.aspx?deviceid=1-8" target="_blank">ѹ��ͷ��¼</a></li>--%>
                                        <li class="divider"></li>
                                        <li><a href="JingLian/ChangeMo_Query.aspx?deviceid=1-8" target="_blank">��ģ��ѯ</a></li>
                                        <li><a href="MoJu/MojuBX_Query.aspx?deviceid=1-8 " target="_blank">ģ�߱��޲�ѯ</a></li>
                                        <li><a href="http://172.16.5.6:8080/SheBei/Job%20monitoring.aspx " target="_blank">�豸���޲�ѯ</a></li>
                                        <li><a href="TJ/Guzhang_Rate_Report.aspx?quyu=yz1q" target="_blank">�豸����ͳ��</a></li>
                                        <li><a href="TJ/ChangeMo_TJ_Report.aspx?quyu=yz1q" target="_blank">��ģͳ��</a></li>
                                        <li><a href="MoJu/Moju_BX_TongJi.aspx" target="_blank">ģ�߱���ͳ��</a></li>
                                        <li><a href="/YaSheTou/YST_Query.aspx?deviceid=1-8" target="_blank">ѹ��ͷ��ѯ</a></li>
                                        <li class="divider"></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuQuery.aspx" target="_blank">ģ���嵥</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuPJ_List.aspx" target="_blank">ģ�߱���</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuPJRK_List.aspx" target="_blank">ģ�߱������</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuPJLY_List.aspx" target="_blank">ģ�߱�������</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuLY_List.aspx" target="_blank">ģ������</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuRK_List.aspx" target="_blank">ģ�����</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuCL_List.aspx" target="_blank">ģ�ߴ���</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuBY_List.aspx" target="_blank">ģ�߱���</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MojuWX_List.aspx" target="_blank">ģ������</a></li>
                                        <li><a href="/YaSheTou/YST_Maintain.aspx" target="_blank">ѹ��ͷ�嵥</a></li>
                                        <li class="divider"></li>
                                        <li><a href="E_data/EData_Query.aspx?deviceid=1-8" target="_blank">��̨���ݲ�ѯ</a></li>
                                        <li><a href="#" target="_blank">��̨����ͳ��</a></li>
                                        <li class="dropdown-submenu" style="display: none">
                                            <a tabindex="-1" href="javascript:;">Menu�˵�</a>
                                            <ul class="dropdown-menu">
                                                <li><a tabindex="-1" href="javascript:;">�����˵�</a></li>
                                                <li class="divider"></li>
                                                <li class="dropdown-submenu">
                                                    <a href="javascript:;">�����˵�</a>
                                                    <ul class="dropdown-menu">
                                                        <li><a href="javascript:;">�����˵�</a></li>
                                                    </ul>
                                                </li>
                                            </ul>
                                        </li>
                                    </ul>
                                </div>
                                <div class="area" id="div_1-8">
                                </div>
                            </div>
                        </div>
                        <div class="area_block">
                            <div class="btn btn-success " name="frm_1-9">
                                <div class="btn-group">
                                    <div class="btn btn-primary disabled" data-toggle="dropdown" name="btn_1-9">
                                        1-9(350T)����
                                    </div>
                                    <ul class="dropdown-menu" style="color: Black" role="menu">
                                        <li><a href="javascript:void(0)" deviceid="1-9" style="display: inline" name="1-9">����</a>
                                            <a href="Javascript: void(0)" deviceid="1-9" style="display: inline" name="1-9">�ػ�</a></li>
                                        <li><a href="BuLeHuanMo/BuLeHuanMo.aspx?deviceid=1-9" target="_blank">��ģ��¼</a></li>
                                        <li><a href="MoJu/MoJuBX.aspx?deviceid=1-9 " target="_blank">ģ�߱���.ά��.ȷ��</a></li>
                                        <li><a href="http://172.16.5.7:8088/JobPortal/login.aspx" target="_blank">�豸����</a></li>
                                        <%--<li><a href="/YaSheTou/YST_Record.aspx?deviceid=1-9" target="_blank">ѹ��ͷ��¼</a></li>--%>
                                        <li class="divider"></li>
                                        <li><a href="JingLian/ChangeMo_Query.aspx?deviceid=1-9" target="_blank">��ģ��ѯ</a></li>
                                        <li><a href="MoJu/MojuBX_Query.aspx?deviceid=1-9 " target="_blank">ģ�߱��޲�ѯ</a></li>
                                        <li><a href="http://172.16.5.6:8080/SheBei/Job%20monitoring.aspx " target="_blank">�豸���޲�ѯ</a></li>
                                        <li><a href="TJ/Guzhang_Rate_Report.aspx?quyu=yz1q" target="_blank">�豸����ͳ��</a></li>
                                        <li><a href="TJ/ChangeMo_TJ_Report.aspx?quyu=yz1q" target="_blank">��ģͳ��</a></li>
                                        <li><a href="MoJu/Moju_BX_TongJi.aspx" target="_blank">ģ�߱���ͳ��</a></li>
                                        <li><a href="/YaSheTou/YST_Query.aspx?deviceid=1-9" target="_blank">ѹ��ͷ��ѯ</a></li>
                                        <li class="divider"></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuQuery.aspx" target="_blank">ģ���嵥</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuPJ_List.aspx" target="_blank">ģ�߱���</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuPJRK_List.aspx" target="_blank">ģ�߱������</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuPJLY_List.aspx" target="_blank">ģ�߱�������</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuLY_List.aspx" target="_blank">ģ������</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuRK_List.aspx" target="_blank">ģ�����</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuCL_List.aspx" target="_blank">ģ�ߴ���</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuBY_List.aspx" target="_blank">ģ�߱���</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MojuWX_List.aspx" target="_blank">ģ������</a></li>
                                        <li><a href="/YaSheTou/YST_Maintain.aspx" target="_blank">ѹ��ͷ�嵥</a></li>
                                        <li class="divider"></li>
                                        <li><a href="E_data/EData_Query.aspx?deviceid=1-9" target="_blank">��̨���ݲ�ѯ</a></li>
                                        <li><a href="#" target="_blank">��̨����ͳ��</a></li>
                                    </ul>
                                </div>
                                <div class="area" id="div_1-9">
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <%--ѹ��2����--%>
    <div class="row  row-container">
        <div class="col-md-12">
            <div class="panel panel-info">
                <div class="panel-heading">
                    <strong>ѹ��2����</strong>
                    <div class="btn-group">
                        <div class="area_drop" data-toggle="dropdown">
                            <span class="caret"></span>
                        </div>
                        <ul class="dropdown-menu" style="color: black" role="menu">
                            <li><span href="#" id="loginZZ" name="yz2q" title="yz2q" style="padding-left: 20px">����</span></li>
                            <li><span href="#" id="logoutZZ" name="����" title="����" style="padding-left: 20px">�ǳ�</span></li>
                            <li class="divider"></li>
                            <li><a href="HandOverQuery.aspx?quyu=yz2q&gongwei=����" name="����" target="_blank">���Ӱ��ѯ</a></li>
                        </ul>
                    </div>
                </div>
                <div class="panel-body">
                    <div class="col-xs-12 col-sm-12  col-md-12 col-lg-12">
                        <div class="area_block">
                            <div class="btn btn-padding-s btn-success " name="frm_bule2-1">
                                <div class="btn-group">
                                    <div class="btn btn-primary " data-toggle="dropdown" name="btn_bule2-1">
                                        2-1(800T)��֥
                                    </div>
                                    <ul class="dropdown-menu" style="color: Black" role="menu">
                                        <li><a href="javascript:void(0)" deviceid="2-1" style="display: inline">����</a>
                                            <a href="Javascript: void(0)" deviceid="2-1" style="display: inline">�ػ�</a></li>
                                            <li><a href="Jiaju/JiaJu_List.aspx" target="_blank">�о�����</a></li>
                                        <li><a href="BuLeHuanMo/BuLeHuanMo.aspx?deviceid=2-1" target="_blank">��ģ��¼</a></li>
                                        <li><a href="MoJu/MoJuBX.aspx?deviceid=2-1 " target="_blank">ģ�߱���.ά��.ȷ��</a></li>
                                        <li><a href="http://172.16.5.7:8088/JobPortal/login.aspx" target="_blank">�豸����</a></li>
                                        <%--<li><a href="/YaSheTou/YST_Record.aspx?deviceid=2-1" target="_blank">ѹ��ͷ��¼</a></li>--%>
                                        <li class="divider"></li>
                                        <li><a href="JingLian/ChangeMo_Query.aspx?deviceid=2-1" target="_blank">��ģ��ѯ</a></li>
                                        <li><a href="MoJu/MojuBX_Query.aspx?deviceid=2-1 " target="_blank">ģ�߱��޲�ѯ</a></li>
                                        <li><a href="http://172.16.5.6:8080/SheBei/Job%20monitoring.aspx " target="_blank">�豸���޲�ѯ</a></li>
                                        <li><a href="TJ/Guzhang_Rate_Report.aspx?quyu=yz2q" target="_blank">�豸����ͳ��</a></li>
                                        <li><a href="TJ/ChangeMo_TJ_Report.aspx?quyu=yz2q" target="_blank">��ģͳ��</a></li>
                                        <li><a href="MoJu/Moju_BX_TongJi.aspx" target="_blank">ģ�߱���ͳ��</a></li>
                                        <li><a href="/YaSheTou/YST_Query.aspx?deviceid=2-1" target="_blank">ѹ��ͷ��ѯ</a></li>
                                        <li class="divider"></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuQuery.aspx" target="_blank">ģ���嵥</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuPJ_List.aspx" target="_blank">ģ�߱���</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuPJRK_List.aspx" target="_blank">ģ�߱������</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuPJLY_List.aspx" target="_blank">ģ�߱�������</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuLY_List.aspx" target="_blank">ģ������</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuRK_List.aspx" target="_blank">ģ�����</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuCL_List.aspx" target="_blank">ģ�ߴ���</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuBY_List.aspx" target="_blank">ģ�߱���</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MojuWX_List.aspx" target="_blank">ģ������</a></li>
                                        <li><a href="/YaSheTou/YST_Maintain.aspx" target="_blank">ѹ��ͷ�嵥</a></li>
                                        <li class="divider"></li>
                                        <li><a href="E_data/EData_Query.aspx?deviceid=2-1" target="_blank">��̨���ݲ�ѯ</a></li>
                                        <li><a href="#" target="_blank">��̨����ͳ��</a></li>
                                    </ul>
                                </div>
                                <div class="area" id="div_bule2-1">
                                </div>
                            </div>
                        </div>
                        <div class="area_block">
                            <div class="btn btn-padding-s btn-success " name="frm_bule2-2">
                                <div class="btn-group">
                                    <div class="btn btn-primary " data-toggle="dropdown" name="btn_bule2-2">
                                        2-2(840T)����
                                    </div>
                                    <ul class="dropdown-menu" style="color: Black" role="menu">
                                        <li><a href="javascript:void(0)" deviceid="2-2" style="display: inline">����</a>
                                            <a href="Javascript: void(0)" deviceid="2-2" style="display: inline">�ػ�</a></li>
                                        <li><a href="BuLeHuanMo/BuLeHuanMo.aspx?deviceid=2-2" target="_blank">��ģ��¼</a></li>
                                        <li><a href="MoJu/MoJuBX.aspx?deviceid=2-2 " target="_blank">ģ�߱���.ά��.ȷ��</a></li>
                                        <li><a href="http://172.16.5.7:8088/JobPortal/login.aspx" target="_blank">�豸����</a></li>
                                        <%--<li><a href="/YaSheTou/YST_Record.aspx?deviceid=2-2" target="_blank">ѹ��ͷ��¼</a></li>--%>
                                        <li class="divider"></li>
                                        <li><a href="JingLian/ChangeMo_Query.aspx?deviceid=2-2" target="_blank">��ģ��ѯ</a></li>
                                        <li><a href="MoJu/MojuBX_Query.aspx?deviceid=2-2 " target="_blank">ģ�߱��޲�ѯ</a></li>
                                        <li><a href="http://172.16.5.6:8080/SheBei/Job%20monitoring.aspx " target="_blank">�豸���޲�ѯ</a></li>
                                        <li><a href="TJ/Guzhang_Rate_Report.aspx?quyu=yz2q" target="_blank">�豸����ͳ��</a></li>
                                        <li><a href="TJ/ChangeMo_TJ_Report.aspx?quyu=yz2q" target="_blank">��ģͳ��</a></li>
                                        <li><a href="MoJu/Moju_BX_TongJi.aspx" target="_blank">ģ�߱���ͳ��</a></li>
                                        <li><a href="/YaSheTou/YST_Query.aspx?deviceid=2-2" target="_blank">ѹ��ͷ��ѯ</a></li>
                                        <li class="divider"></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuQuery.aspx" target="_blank">ģ���嵥</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuPJ_List.aspx" target="_blank">ģ�߱���</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuPJRK_List.aspx" target="_blank">ģ�߱������</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuPJLY_List.aspx" target="_blank">ģ�߱�������</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuLY_List.aspx" target="_blank">ģ������</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuRK_List.aspx" target="_blank">ģ�����</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuCL_List.aspx" target="_blank">ģ�ߴ���</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuBY_List.aspx" target="_blank">ģ�߱���</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MojuWX_List.aspx" target="_blank">ģ������</a></li>
                                        <li><a href="/YaSheTou/YST_Maintain.aspx" target="_blank">ѹ��ͷ�嵥</a></li>
                                        <li class="divider"></li>
                                        <li><a href="E_data/EData_Query.aspx?deviceid=2-2" target="_blank">��̨���ݲ�ѯ</a></li>
                                        <li><a href="#" target="_blank">��̨����ͳ��</a></li>
                                    </ul>
                                </div>
                                <div class="area" id="div_bule2-2">
                                </div>
                            </div>
                        </div>
                        <div class="area_block">
                            <div class="btn btn-padding-s btn-success " name="frm_bule2-3">
                                <div class="btn-group">
                                    <div class="btn btn-primary " data-toggle="dropdown" name="btn_bule2-3">
                                        2-3(660T)����
                                    </div>
                                    <ul class="dropdown-menu" style="color: Black" role="menu">
                                        <li><a href="javascript:void(0)" deviceid="2-3" style="display: inline">����</a>
                                            <a href="Javascript: void(0)" deviceid="2-3" style="display: inline">�ػ�</a></li>
                                        <li><a href="BuLeHuanMo/BuLeHuanMo.aspx?deviceid=2-3" target="_blank">��ģ��¼</a></li>
                                        <li><a href="MoJu/MoJuBX.aspx?deviceid=2-3 " target="_blank">ģ�߱���.ά��.ȷ��</a></li>
                                        <li><a href="http://172.16.5.7:8088/JobPortal/login.aspx" target="_blank">�豸����</a></li>
                                        <%--<li><a href="/YaSheTou/YST_Record.aspx?deviceid=2-3" target="_blank">ѹ��ͷ��¼</a></li>--%>
                                        <li class="divider"></li>
                                        <li><a href="JingLian/ChangeMo_Query.aspx?deviceid=2-3" target="_blank">��ģ��ѯ</a></li>
                                        <li><a href="MoJu/MojuBX_Query.aspx?deviceid=2-3 " target="_blank">ģ�߱��޲�ѯ</a></li>
                                        <li><a href="http://172.16.5.6:8080/SheBei/Job%20monitoring.aspx " target="_blank">�豸���޲�ѯ</a></li>
                                        <li><a href="TJ/Guzhang_Rate_Report.aspx?quyu=yz2q" target="_blank">�豸����ͳ��</a></li>
                                        <li><a href="TJ/ChangeMo_TJ_Report.aspx?quyu=yz2q" target="_blank">��ģͳ��</a></li>
                                        <li><a href="MoJu/Moju_BX_TongJi.aspx" target="_blank">ģ�߱���ͳ��</a></li>
                                        <li><a href="/YaSheTou/YST_Query.aspx?deviceid=2-3" target="_blank">ѹ��ͷ��ѯ</a></li>
                                        <li class="divider"></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuQuery.aspx" target="_blank">ģ���嵥</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuPJ_List.aspx" target="_blank">ģ�߱���</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuPJRK_List.aspx" target="_blank">ģ�߱������</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuPJLY_List.aspx" target="_blank">ģ�߱�������</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuLY_List.aspx" target="_blank">ģ������</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuRK_List.aspx" target="_blank">ģ�����</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuCL_List.aspx" target="_blank">ģ�ߴ���</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuBY_List.aspx" target="_blank">ģ�߱���</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MojuWX_List.aspx" target="_blank">ģ������</a></li>
                                        <li><a href="/YaSheTou/YST_Maintain.aspx" target="_blank">ѹ��ͷ�嵥</a></li>
                                        <li class="divider"></li>
                                        <li><a href="E_data/EData_Query.aspx?deviceid=2-3" target="_blank">��̨���ݲ�ѯ</a></li>
                                        <li><a href="#" target="_blank">��̨����ͳ��</a></li>
                                    </ul>
                                </div>
                                <div class="area" id="div_bule2-3">
                                </div>
                            </div>
                        </div>
                        <div class="area_block">
                            <div class="btn btn-padding-s btn-success " name="frm_bule2-4">
                                <div class="btn-group">
                                    <div class="btn btn-primary " data-toggle="dropdown" name="btn_bule2-4">
                                        2-4(840T)����
                                    </div>
                                    <ul class="dropdown-menu" style="color: Black" role="menu">
                                        <li><a href="javascript:void(0)" deviceid="2-4" style="display: inline">����</a>
                                            <a href="Javascript: void(0)" deviceid="2-4" style="display: inline">�ػ�</a></li>
                                        <li><a href="BuLeHuanMo/BuLeHuanMo.aspx?deviceid=2-4" target="_blank">��ģ��¼</a></li>
                                        <li><a href="MoJu/MoJuBX.aspx?deviceid=2-4 " target="_blank">ģ�߱���.ά��.ȷ��</a></li>
                                        <li><a href="http://172.16.5.7:8088/JobPortal/login.aspx" target="_blank">�豸����</a></li>
                                        <%--<li><a href="/YaSheTou/YST_Record.aspx?deviceid=2-4" target="_blank">ѹ��ͷ��¼</a></li>--%>
                                        <li class="divider"></li>
                                        <li><a href="JingLian/ChangeMo_Query.aspx?deviceid=2-4" target="_blank">��ģ��ѯ</a></li>
                                        <li><a href="MoJu/MojuBX_Query.aspx?deviceid=2-4 " target="_blank">ģ�߱��޲�ѯ</a></li>
                                        <li><a href="http://172.16.5.6:8080/SheBei/Job%20monitoring.aspx " target="_blank">�豸���޲�ѯ</a></li>
                                        <li><a href="TJ/Guzhang_Rate_Report.aspx?quyu=yz2q" target="_blank">�豸����ͳ��</a></li>
                                        <li><a href="TJ/ChangeMo_TJ_Report.aspx?quyu=yz2q" target="_blank">��ģͳ��</a></li>
                                        <li><a href="MoJu/Moju_BX_TongJi.aspx" target="_blank">ģ�߱���ͳ��</a></li>
                                        <li><a href="/YaSheTou/YST_Query.aspx?deviceid=2-4" target="_blank">ѹ��ͷ��ѯ</a></li>
                                        <li class="divider"></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuQuery.aspx" target="_blank">ģ���嵥</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuPJ_List.aspx" target="_blank">ģ�߱���</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuPJRK_List.aspx" target="_blank">ģ�߱������</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuPJLY_List.aspx" target="_blank">ģ�߱�������</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuLY_List.aspx" target="_blank">ģ������</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuRK_List.aspx" target="_blank">ģ�����</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuCL_List.aspx" target="_blank">ģ�ߴ���</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuBY_List.aspx" target="_blank">ģ�߱���</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MojuWX_List.aspx" target="_blank">ģ������</a></li>
                                        <li><a href="/YaSheTou/YST_Maintain.aspx" target="_blank">ѹ��ͷ�嵥</a></li>
                                        <li class="divider"></li>
                                        <li><a href="E_data/EData_Query.aspx?deviceid=2-4" target="_blank">��̨���ݲ�ѯ</a></li>
                                        <li><a href="#" target="_blank">��̨����ͳ��</a></li>
                                    </ul>
                                </div>
                                <div class="area" id="div_bule2-4">
                                </div>
                            </div>
                        </div>
                        <div class="area_block">
                            <div class="btn btn-padding-s btn-success " name="frm_bule2-5">
                                <div class="btn-group">
                                    <div class="btn btn-primary " data-toggle="dropdown" name="btn_bule2-5">
                                        2-5(840T)����
                                    </div>
                                    <ul class="dropdown-menu" style="color: Black" role="menu">
                                        <li><a href="javascript:void(0)" deviceid="2-5" style="display: inline">����</a>
                                            <a href="Javascript: void(0)" deviceid="2-5" style="display: inline">�ػ�</a></li>
                                        <li><a href="BuLeHuanMo/BuLeHuanMo.aspx?deviceid=2-5" target="_blank">��ģ��¼</a></li>
                                        <li><a href="MoJu/MoJuBX.aspx?deviceid=2-5 " target="_blank">ģ�߱���.ά��.ȷ��</a></li>
                                        <li><a href="http://172.16.5.7:8088/JobPortal/login.aspx" target="_blank">�豸����</a></li>
                                        <%--<li><a href="/YaSheTou/YST_Record.aspx?deviceid=2-5" target="_blank">ѹ��ͷ��¼</a></li>--%>
                                        <li class="divider"></li>
                                        <li><a href="JingLian/ChangeMo_Query.aspx?deviceid=2-5" target="_blank">��ģ��ѯ</a></li>
                                        <li><a href="MoJu/MojuBX_Query.aspx?deviceid=2-5 " target="_blank">ģ�߱��޲�ѯ</a></li>
                                        <li><a href="http://172.16.5.6:8080/SheBei/Job%20monitoring.aspx " target="_blank">�豸���޲�ѯ</a></li>
                                        <li><a href="TJ/Guzhang_Rate_Report.aspx?quyu=yz2q" target="_blank">�豸����ͳ��</a></li>
                                        <li><a href="TJ/ChangeMo_TJ_Report.aspx?quyu=yz2q" target="_blank">��ģͳ��</a></li>
                                        <li><a href="MoJu/Moju_BX_TongJi.aspx" target="_blank">ģ�߱���ͳ��</a></li>
                                        <li><a href="/YaSheTou/YST_Query.aspx?deviceid=2-5" target="_blank">ѹ��ͷ��ѯ</a></li>
                                        <li class="divider"></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuQuery.aspx" target="_blank">ģ���嵥</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuPJ_List.aspx" target="_blank">ģ�߱���</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuPJRK_List.aspx" target="_blank">ģ�߱������</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuPJLY_List.aspx" target="_blank">ģ�߱�������</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuLY_List.aspx" target="_blank">ģ������</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuRK_List.aspx" target="_blank">ģ�����</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuCL_List.aspx" target="_blank">ģ�ߴ���</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuBY_List.aspx" target="_blank">ģ�߱���</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MojuWX_List.aspx" target="_blank">ģ������</a></li>
                                        <li><a href="/YaSheTou/YST_Maintain.aspx" target="_blank">ѹ��ͷ�嵥</a></li>
                                        <li class="divider"></li>
                                        <li><a href="E_data/EData_Query.aspx?deviceid=2-5" target="_blank">��̨���ݲ�ѯ</a></li>
                                        <li><a href="#" target="_blank">��̨����ͳ��</a></li>
                                    </ul>
                                </div>
                                <div class="area" id="div_bule2-5">
                                </div>
                            </div>
                        </div>
                        <div class="area_block">
                            <div class="btn btn-padding-s btn-success " name="frm_bule2-6">
                                <div class="btn-group">
                                    <div class="btn btn-primary " data-toggle="dropdown" name="btn_bule2-6">
                                        2-6(840T)����
                                    </div>
                                    <ul class="dropdown-menu" style="color: Black" role="menu">
                                        <li><a href="javascript:void(0)" deviceid="2-6" style="display: inline">����</a>
                                            <a href="Javascript: void(0)" deviceid="2-6" style="display: inline">�ػ�</a></li>
                                        <li><a href="BuLeHuanMo/BuLeHuanMo.aspx?deviceid=2-6" target="_blank">��ģ��¼</a></li>
                                        <li><a href="MoJu/MoJuBX.aspx?deviceid=2-6 " target="_blank">ģ�߱���.ά��.ȷ��</a></li>
                                        <li><a href="http://172.16.5.7:8088/JobPortal/login.aspx" target="_blank">�豸����</a></li>
                                        <%--<li><a href="/YaSheTou/YST_Record.aspx?deviceid=2-6" target="_blank">ѹ��ͷ��¼</a></li>--%>
                                        <li class="divider"></li>
                                        <li><a href="JingLian/ChangeMo_Query.aspx?deviceid=2-6" target="_blank">��ģ��ѯ</a></li>
                                        <li><a href="MoJu/MojuBX_Query.aspx?deviceid=2-6 " target="_blank">ģ�߱��޲�ѯ</a></li>
                                        <li><a href="http://172.16.5.6:8080/SheBei/Job%20monitoring.aspx " target="_blank">�豸���޲�ѯ</a></li>
                                        <li><a href="TJ/Guzhang_Rate_Report.aspx?quyu=yz2q" target="_blank">�豸����ͳ��</a></li>
                                        <li><a href="TJ/ChangeMo_TJ_Report.aspx?quyu=yz2q" target="_blank">��ģͳ��</a></li>
                                        <li><a href="MoJu/Moju_BX_TongJi.aspx" target="_blank">ģ�߱���ͳ��</a></li>
                                        <li><a href="/YaSheTou/YST_Query.aspx?deviceid=2-6" target="_blank">ѹ��ͷ��ѯ</a></li>
                                        <li class="divider"></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuQuery.aspx" target="_blank">ģ���嵥</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuPJ_List.aspx" target="_blank">ģ�߱���</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuPJRK_List.aspx" target="_blank">ģ�߱������</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuPJLY_List.aspx" target="_blank">ģ�߱�������</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuLY_List.aspx" target="_blank">ģ������</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuRK_List.aspx" target="_blank">ģ�����</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuCL_List.aspx" target="_blank">ģ�ߴ���</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuBY_List.aspx" target="_blank">ģ�߱���</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MojuWX_List.aspx" target="_blank">ģ������</a></li>
                                        <li><a href="/YaSheTou/YST_Maintain.aspx" target="_blank">ѹ��ͷ�嵥</a></li>
                                        <li class="divider"></li>
                                        <li><a href="E_data/EData_Query.aspx?deviceid=2-6" target="_blank">��̨���ݲ�ѯ</a></li>
                                        <li><a href="#" target="_blank">��̨����ͳ��</a></li>
                                    </ul>
                                </div>
                                <div class="area" id="div_bule2-6">
                                </div>
                            </div>
                        </div>
                        <div class="area_block">
                            <div class="btn btn-padding-s btn-success " name="frm_bule2-7">
                                <div class="btn-group">
                                    <div class="btn btn-primary " data-toggle="dropdown" name="btn_bule2-7">
                                        2-7(840T)����
                                    </div>
                                    <ul class="dropdown-menu" style="color: Black" role="menu">
                                        <li><a href="javascript:void(0)" deviceid="2-7" style="display: inline">����</a>
                                            <a href="Javascript: void(0)" deviceid="2-7" style="display: inline">�ػ�</a></li>
                                        <li><a href="BuLeHuanMo/BuLeHuanMo.aspx?deviceid=2-7" target="_blank">��ģ��¼</a></li>
                                        <li><a href="MoJu/MoJuBX.aspx?deviceid=2-7" target="_blank">ģ�߱���.ά��.ȷ��</a></li>
                                        <li><a href="http://172.16.5.7:8088/JobPortal/login.aspx" target="_blank">�豸����</a></li>
                                        <%--<li><a href="/YaSheTou/YST_Record.aspx?deviceid=2-7" target="_blank">ѹ��ͷ��¼</a></li>--%>
                                        <li class="divider"></li>
                                        <li><a href="JingLian/ChangeMo_Query.aspx?deviceid=2-7" target="_blank">��ģ��ѯ</a></li>
                                        <li><a href="MoJu/MojuBX_Query.aspx?deviceid=2-7 " target="_blank">ģ�߱��޲�ѯ</a></li>
                                        <li><a href="http://172.16.5.6:8080/SheBei/Job%20monitoring.aspx " target="_blank">�豸���޲�ѯ</a></li>
                                        <li><a href="TJ/Guzhang_Rate_Report.aspx?quyu=yz2q" target="_blank">�豸����ͳ��</a></li>
                                        <li><a href="TJ/ChangeMo_TJ_Report.aspx?quyu=yz2q" target="_blank">��ģͳ��</a></li>
                                        <li><a href="MoJu/Moju_BX_TongJi.aspx" target="_blank">ģ�߱���ͳ��</a></li>
                                        <li><a href="/YaSheTou/YST_Query.aspx?deviceid=2-7" target="_blank">ѹ��ͷ��ѯ</a></li>
                                        <li class="divider"></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuQuery.aspx" target="_blank">ģ���嵥</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuPJ_List.aspx" target="_blank">ģ�߱���</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuPJRK_List.aspx" target="_blank">ģ�߱������</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuPJLY_List.aspx" target="_blank">ģ�߱�������</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuLY_List.aspx" target="_blank">ģ������</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuRK_List.aspx" target="_blank">ģ�����</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuCL_List.aspx" target="_blank">ģ�ߴ���</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuBY_List.aspx" target="_blank">ģ�߱���</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MojuWX_List.aspx" target="_blank">ģ������</a></li>
                                        <li><a href="/YaSheTou/YST_Maintain.aspx" target="_blank">ѹ��ͷ�嵥</a></li>
                                        <li class="divider"></li>
                                        <li><a href="E_data/EData_Query.aspx?deviceid=2-7" target="_blank">��̨���ݲ�ѯ</a></li>
                                        <li><a href="#" target="_blank">��̨����ͳ��</a></li>
                                    </ul>
                                </div>
                                <div class="area" id="div_bule2-7">
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <%--ѹ��3����--%>
    <div class="row  row-container">
        <div class="col-md-12">
            <div class="panel panel-info">
                <div class="panel-heading">
                    <strong>ѹ��3����</strong>
                    <div class="btn-group">
                        <div class="area_drop" data-toggle="dropdown">
                            <span class="caret"></span>
                        </div>
                        <ul class="dropdown-menu" style="color: black" role="menu">
                            <li><span href="#" id="loginyz3q" name="yz3q" title="yz3q" style="padding-left: 20px">����</span></li>
                            <li><span href="#" id="logoutyz3q" name="����" title="����" style="padding-left: 20px">�ǳ�</span></li>
                            <li class="divider"></li>
                            <li><a href="HandOverQuery.aspx?quyu=yz3q&gongwei=����" name="����" target="_blank">���Ӱ��ѯ</a></li>
                        </ul>
                    </div>
                </div>
                <div class="panel-body">
                    <div class="col-xs-12 col-sm-12  col-md-12 col-lg-12">
                        <div class="area_block">
                            <div class="btn btn-success " name="frm_3-1">
                                <div class="btn-group">
                                    <div class="btn btn-primary disabled" data-toggle="dropdown" name="btn_3-1">
                                        3-1(500T)��֥
                                    </div>
                                    <ul class="dropdown-menu" style="color: Black" role="menu">
                                        <li><a href="javascript:void(0)" deviceid="3-1" style="display: inline">����</a>
                                            <a href="Javascript:void(0)" deviceid="3-1" style="display: inline">�ػ�</a></li>
                                        <li><a href="BuLeHuanMo/BuLeHuanMo.aspx?deviceid=3-1" target="_blank">��ģ��¼</a></li>
                                        <li><a href="MoJu/MoJuBX.aspx?deviceid=3-1 " target="_blank">ģ�߱���.ά��.ȷ��</a></li>
                                        <li><a href="http://172.16.5.7:8088/JobPortal/login.aspx" target="_blank">�豸����</a></li>
                                        <%--<li><a href="/YaSheTou/YST_Record.aspx?deviceid=3-1" target="_blank">ѹ��ͷ��¼</a></li>--%>
                                        <li class="divider"></li>
                                        <li><a href="JingLian/ChangeMo_Query.aspx?deviceid=3-1" target="_blank">��ģ��ѯ</a></li>
                                        <li><a href="MoJu/MojuBX_Query.aspx?deviceid=3-1 " target="_blank">ģ�߱��޲�ѯ</a></li>
                                        <li><a href="http://172.16.5.6:8080/SheBei/Job%20monitoring.aspx " target="_blank">�豸���޲�ѯ</a></li>
                                        <li><a href="TJ/Guzhang_Rate_Report.aspx?quyu=yz3q" target="_blank">�豸����ͳ��</a></li>
                                        <li><a href="TJ/ChangeMo_TJ_Report.aspx?quyu=yz3q" target="_blank">��ģͳ��</a></li>
                                        <li><a href="MoJu/Moju_BX_TongJi.aspx" target="_blank">ģ�߱���ͳ��</a></li>
                                        <li><a href="/YaSheTou/YST_Query.aspx?deviceid=3-1" target="_blank">ѹ��ͷ��ѯ</a></li>
                                        <li class="divider"></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuQuery.aspx" target="_blank">ģ���嵥</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuPJ_List.aspx" target="_blank">ģ�߱���</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuPJRK_List.aspx" target="_blank">ģ�߱������</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuPJLY_List.aspx" target="_blank">ģ�߱�������</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuLY_List.aspx" target="_blank">ģ������</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuRK_List.aspx" target="_blank">ģ�����</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuCL_List.aspx" target="_blank">ģ�ߴ���</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuBY_List.aspx" target="_blank">ģ�߱���</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MojuWX_List.aspx" target="_blank">ģ������</a></li>
                                        <li><a href="/YaSheTou/YST_Maintain.aspx" target="_blank">ѹ��ͷ�嵥</a></li>
                                        <li class="divider"></li>
                                        <li><a href="E_data/EData_Query_Modify.aspx?deviceid=3-1" target="_blank">��̨���ݲ�ѯ</a></li>
                                        <li><a href="#" target="_blank">��̨����ͳ��</a></li>
                                    </ul>
                                </div>
                                <div class="area" id="div_3-1">
                                </div>
                            </div>
                        </div>
                        <div class="area_block">
                            <div class="btn btn-success " name="frm_3-2">
                                <div class="btn-group">
                                    <div class="btn btn-primary disabled" data-toggle="dropdown" name="btn_3-2">
                                        3-2(500T)��֥
                                    </div>
                                    <ul class="dropdown-menu" style="color: Black" role="menu">
                                        <li><a href="javascript:void(0)" deviceid="3-2" style="display: inline">����</a>
                                            <a href="Javascript:void(0)" deviceid="3-2" style="display: inline">�ػ�</a></li>
                                        <li><a href="BuLeHuanMo/BuLeHuanMo.aspx?deviceid=3-2" target="_blank">��ģ��¼</a></li>
                                        <li><a href="MoJu/MoJuBX.aspx?deviceid=3-2 " target="_blank">ģ�߱���.ά��.ȷ��</a></li>
                                        <li><a href="http://172.16.5.7:8088/JobPortal/login.aspx" target="_blank">�豸����</a></li>
                                        <%--<li><a href="/YaSheTou/YST_Record.aspx?deviceid=3-2" target="_blank">ѹ��ͷ��¼</a></li>--%>
                                        <li class="divider"></li>
                                        <li><a href="JingLian/ChangeMo_Query.aspx?deviceid=3-2" target="_blank">��ģ��ѯ</a></li>
                                        <li><a href="MoJu/MojuBX_Query.aspx?deviceid=3-2 " target="_blank">ģ�߱��޲�ѯ</a></li>
                                        <li><a href="http://172.16.5.6:8080/SheBei/Job%20monitoring.aspx " target="_blank">�豸���޲�ѯ</a></li>
                                        <li><a href="TJ/Guzhang_Rate_Report.aspx?quyu=yz3q" target="_blank">�豸����ͳ��</a></li>
                                        <li><a href="TJ/ChangeMo_TJ_Report.aspx?quyu=yz3q" target="_blank">��ģͳ��</a></li>
                                        <li><a href="MoJu/Moju_BX_TongJi.aspx" target="_blank">ģ�߱���ͳ��</a></li>
                                        <li><a href="/YaSheTou/YST_Query.aspx?deviceid=3-2" target="_blank">ѹ��ͷ��ѯ</a></li>
                                        <li class="divider"></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuQuery.aspx" target="_blank">ģ���嵥</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuPJ_List.aspx" target="_blank">ģ�߱���</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuPJRK_List.aspx" target="_blank">ģ�߱������</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuPJLY_List.aspx" target="_blank">ģ�߱�������</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuLY_List.aspx" target="_blank">ģ������</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuRK_List.aspx" target="_blank">ģ�����</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuCL_List.aspx" target="_blank">ģ�ߴ���</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuBY_List.aspx" target="_blank">ģ�߱���</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MojuWX_List.aspx" target="_blank">ģ������</a></li>
                                        <li><a href="/YaSheTou/YST_Maintain.aspx" target="_blank">ѹ��ͷ�嵥</a></li>
                                        <li class="divider"></li>
                                        <li><a href="E_data/EData_Query_Modify.aspx?deviceid=3-2" target="_blank">��̨���ݲ�ѯ</a></li>
                                        <li><a href="#" target="_blank">��̨����ͳ��</a></li>
                                    </ul>
                                </div>
                                <div class="area" id="div_3-2">
                                </div>
                            </div>
                        </div>
                        <div class="area_block">
                            <div class="btn btn-success " name="frm_3-3">
                                <div class="btn-group">
                                    <div class="btn btn-primary disabled" data-toggle="dropdown" name="btn_3-3">
                                        3-3(500T)��֥
                                    </div>
                                    <ul class="dropdown-menu" style="color: Black" role="menu">
                                        <li><a href="javascript:void(0)" deviceid="3-3" style="display: inline">����</a>
                                            <a href="Javascript:void(0)" deviceid="3-3" style="display: inline">�ػ�</a></li>
                                        <li><a href="BuLeHuanMo/BuLeHuanMo.aspx?deviceid=3-3" target="_blank">��ģ��¼</a></li>
                                        <li><a href="MoJu/MoJuBX.aspx?deviceid=3-3 " target="_blank">ģ�߱���.ά��.ȷ��</a></li>
                                        <li><a href="http://172.16.5.7:8088/JobPortal/login.aspx" target="_blank">�豸����</a></li>
                                        <%--<li><a href="/YaSheTou/YST_Record.aspx?deviceid=3-3" target="_blank">ѹ��ͷ��¼</a></li>--%>
                                        <li class="divider"></li>
                                        <li><a href="JingLian/ChangeMo_Query.aspx?deviceid=3-3" target="_blank">��ģ��ѯ</a></li>
                                        <li><a href="MoJu/MojuBX_Query.aspx?deviceid=3-3 " target="_blank">ģ�߱��޲�ѯ</a></li>
                                        <li><a href="http://172.16.5.6:8080/SheBei/Job%20monitoring.aspx " target="_blank">�豸���޲�ѯ</a></li>
                                        <li><a href="TJ/Guzhang_Rate_Report.aspx?quyu=yz3q" target="_blank">�豸����ͳ��</a></li>
                                        <li><a href="TJ/ChangeMo_TJ_Report.aspx?quyu=yz3q" target="_blank">��ģͳ��</a></li>
                                        <li><a href="MoJu/Moju_BX_TongJi.aspx" target="_blank">ģ�߱���ͳ��</a></li>
                                        <li><a href="/YaSheTou/YST_Query.aspx?deviceid=3-3" target="_blank">ѹ��ͷ��ѯ</a></li>
                                        <li class="divider"></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuQuery.aspx" target="_blank">ģ���嵥</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuPJ_List.aspx" target="_blank">ģ�߱���</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuPJRK_List.aspx" target="_blank">ģ�߱������</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuPJLY_List.aspx" target="_blank">ģ�߱�������</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuLY_List.aspx" target="_blank">ģ������</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuRK_List.aspx" target="_blank">ģ�����</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuCL_List.aspx" target="_blank">ģ�ߴ���</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuBY_List.aspx" target="_blank">ģ�߱���</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MojuWX_List.aspx" target="_blank">ģ������</a></li>
                                        <li><a href="/YaSheTou/YST_Maintain.aspx" target="_blank">ѹ��ͷ�嵥</a></li>
                                        <li class="divider"></li>
                                        <li><a href="E_data/EData_Query_Modify.aspx?deviceid=3-3" target="_blank">��̨���ݲ�ѯ</a></li>
                                        <li><a href="#" target="_blank">��̨����ͳ��</a></li>
                                    </ul>
                                </div>
                                <div class="area" id="div_3-3">
                                </div>
                            </div>
                        </div>
                        <div class="area_block">
                            <div class="btn btn-success " name="frm_3-4">
                                <div class="btn-group">
                                    <div class="btn btn-primary disabled" data-toggle="dropdown" name="btn_3-4">
                                        3-4(500T)��֥
                                    </div>
                                    <ul class="dropdown-menu" style="color: Black" role="menu">
                                        <li><a href="javascript:void(0)" deviceid="3-4" style="display: inline">����</a>
                                            <a href="Javascript:void(0)" deviceid="3-4" style="display: inline">�ػ�</a></li>
                                        <li><a href="BuLeHuanMo/BuLeHuanMo.aspx?deviceid=3-4" target="_blank">��ģ��¼</a></li>
                                        <li><a href="MoJu/MoJuBX.aspx?deviceid=3-4 " target="_blank">ģ�߱���.ά��.ȷ��</a></li>
                                        <li><a href="http://172.16.5.7:8088/JobPortal/login.aspx" target="_blank">�豸����</a></li>
                                        <%--<li><a href="/YaSheTou/YST_Record.aspx?deviceid=3-4" target="_blank">ѹ��ͷ��¼</a></li>--%>
                                        <li class="divider"></li>
                                        <li><a href="JingLian/ChangeMo_Query.aspx?deviceid=3-4" target="_blank">��ģ��ѯ</a></li>
                                        <li><a href="MoJu/MojuBX_Query.aspx?deviceid=3-4 " target="_blank">ģ�߱��޲�ѯ</a></li>
                                        <li><a href="http://172.16.5.6:8080/SheBei/Job%20monitoring.aspx " target="_blank">�豸���޲�ѯ</a></li>
                                        <li><a href="TJ/Guzhang_Rate_Report.aspx?quyu=yz3q" target="_blank">�豸����ͳ��</a></li>
                                        <li><a href="TJ/ChangeMo_TJ_Report.aspx?quyu=yz3q" target="_blank">��ģͳ��</a></li>
                                        <li><a href="MoJu/Moju_BX_TongJi.aspx" target="_blank">ģ�߱���ͳ��</a></li>
                                        <li><a href="/YaSheTou/YST_Query.aspx?deviceid=3-4" target="_blank">ѹ��ͷ��ѯ</a></li>
                                        <li class="divider"></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuQuery.aspx" target="_blank">ģ���嵥</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuPJ_List.aspx" target="_blank">ģ�߱���</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuPJRK_List.aspx" target="_blank">ģ�߱������</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuPJLY_List.aspx" target="_blank">ģ�߱�������</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuLY_List.aspx" target="_blank">ģ������</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuRK_List.aspx" target="_blank">ģ�����</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuCL_List.aspx" target="_blank">ģ�ߴ���</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuBY_List.aspx" target="_blank">ģ�߱���</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MojuWX_List.aspx" target="_blank">ģ������</a></li>
                                        <li><a href="/YaSheTou/YST_Maintain.aspx" target="_blank">ѹ��ͷ�嵥</a></li>
                                        <li class="divider"></li>
                                        <li><a href="E_data/EData_Query_Modify.aspx?deviceid=3-4" target="_blank">��̨���ݲ�ѯ</a></li>
                                        <li><a href="#" target="_blank">��̨����ͳ��</a></li>
                                    </ul>
                                </div>
                                <div class="area" id="div_3-4">
                                </div>
                            </div>
                        </div>
                        <div class="area_block">
                            <div class="btn btn-success " name="frm_3-5">
                                <div class="btn-group">
                                    <div class="btn btn-primary disabled" data-toggle="dropdown" name="btn_3-5">
                                        3-5(500T)��֥
                                    </div>
                                    <ul class="dropdown-menu" style="color: Black" role="menu">
                                        <li><a href="javascript:void(0)" deviceid="3-5" style="display: inline">����</a>
                                            <a href="Javascript:void(0)" deviceid="3-5" style="display: inline">�ػ�</a></li>
                                        <li><a href="BuLeHuanMo/BuLeHuanMo.aspx?deviceid=3-5" target="_blank">��ģ��¼</a></li>
                                        <li><a href="MoJu/MoJuBX.aspx?deviceid=3-5 " target="_blank">ģ�߱���.ά��.ȷ��</a></li>
                                        <li><a href="http://172.16.5.7:8088/JobPortal/login.aspx" target="_blank">�豸����</a></li>
                                        <%--<li><a href="/YaSheTou/YST_Record.aspx?deviceid=3-5" target="_blank">ѹ��ͷ��¼</a></li>--%>
                                        <li class="divider"></li>
                                        <li><a href="JingLian/ChangeMo_Query.aspx?deviceid=3-5" target="_blank">��ģ��ѯ</a></li>
                                        <li><a href="MoJu/MojuBX_Query.aspx?deviceid=3-5 " target="_blank">ģ�߱��޲�ѯ</a></li>
                                        <li><a href="http://172.16.5.6:8080/SheBei/Job%20monitoring.aspx " target="_blank">�豸���޲�ѯ</a></li>
                                        <li><a href="TJ/Guzhang_Rate_Report.aspx?quyu=yz3q" target="_blank">�豸����ͳ��</a></li>
                                        <li><a href="TJ/ChangeMo_TJ_Report.aspx?quyu=yz3q" target="_blank">��ģͳ��</a></li>
                                        <li><a href="MoJu/Moju_BX_TongJi.aspx" target="_blank">ģ�߱���ͳ��</a></li>
                                        <li><a href="/YaSheTou/YST_Query.aspx?deviceid=3-5" target="_blank">ѹ��ͷ��ѯ</a></li>
                                        <li class="divider"></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuQuery.aspx" target="_blank">ģ���嵥</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuPJ_List.aspx" target="_blank">ģ�߱���</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuPJRK_List.aspx" target="_blank">ģ�߱������</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuPJLY_List.aspx" target="_blank">ģ�߱�������</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuLY_List.aspx" target="_blank">ģ������</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuRK_List.aspx" target="_blank">ģ�����</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuCL_List.aspx" target="_blank">ģ�ߴ���</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuBY_List.aspx" target="_blank">ģ�߱���</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MojuWX_List.aspx" target="_blank">ģ������</a></li>
                                        <li><a href="/YaSheTou/YST_Maintain.aspx" target="_blank">ѹ��ͷ�嵥</a></li>
                                        <li class="divider"></li>
                                        <li><a href="E_data/EData_Query_Modify.aspx?deviceid=3-5" target="_blank">��̨���ݲ�ѯ</a></li>
                                        <li><a href="#" target="_blank">��̨����ͳ��</a></li>
                                    </ul>
                                </div>
                                <div class="area" id="div_3-5">
                                </div>
                            </div>
                        </div>
                        <div class="area_block">
                            <div class="btn btn-success " name="frm_3-6">
                                <div class="btn-group">
                                    <div class="btn btn-primary disabled" data-toggle="dropdown" name="btn_3-6">
                                        3-6(500T)��֥
                                    </div>
                                    <ul class="dropdown-menu" style="color: Black" role="menu">
                                        <li><a href="javascript:void(0)" deviceid="3-6" style="display: inline">����</a>
                                            <a href="Javascript:void(0)" deviceid="3-6" style="display: inline">�ػ�</a></li>
                                        <li><a href="BuLeHuanMo/BuLeHuanMo.aspx?deviceid=3-6" target="_blank">��ģ��¼</a></li>
                                        <li><a href="MoJu/MoJuBX.aspx?deviceid=3-6 " target="_blank">ģ�߱���.ά��.ȷ��</a></li>
                                        <li><a href="http://172.16.5.7:8088/JobPortal/login.aspx" target="_blank">�豸����</a></li>
                                        <%--<li><a href="/YaSheTou/YST_Record.aspx?deviceid=3-6" target="_blank">ѹ��ͷ��¼</a></li>--%>
                                        <li class="divider"></li>
                                        <li><a href="JingLian/ChangeMo_Query.aspx?deviceid=3-6" target="_blank">��ģ��ѯ</a></li>
                                        <li><a href="MoJu/MojuBX_Query.aspx?deviceid=3-6 " target="_blank">ģ�߱��޲�ѯ</a></li>
                                        <li><a href="http://172.16.5.6:8080/SheBei/Job%20monitoring.aspx " target="_blank">�豸���޲�ѯ</a></li>
                                        <li><a href="TJ/Guzhang_Rate_Report.aspx?quyu=yz3q" target="_blank">�豸����ͳ��</a></li>
                                        <li><a href="TJ/ChangeMo_TJ_Report.aspx?quyu=yz3q" target="_blank">��ģͳ��</a></li>
                                        <li><a href="MoJu/Moju_BX_TongJi.aspx" target="_blank">ģ�߱���ͳ��</a></li>
                                        <li><a href="/YaSheTou/YST_Query.aspx?deviceid=3-6" target="_blank">ѹ��ͷ��ѯ</a></li>
                                        <li class="divider"></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuQuery.aspx" target="_blank">ģ���嵥</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuPJ_List.aspx" target="_blank">ģ�߱���</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuPJRK_List.aspx" target="_blank">ģ�߱������</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuPJLY_List.aspx" target="_blank">ģ�߱�������</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuLY_List.aspx" target="_blank">ģ������</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuRK_List.aspx" target="_blank">ģ�����</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuCL_List.aspx" target="_blank">ģ�ߴ���</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuBY_List.aspx" target="_blank">ģ�߱���</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MojuWX_List.aspx" target="_blank">ģ������</a></li>
                                        <li><a href="/YaSheTou/YST_Maintain.aspx" target="_blank">ѹ��ͷ�嵥</a></li>
                                        <li class="divider"></li>
                                        <li><a href="E_data/EData_Query_Modify.aspx?deviceid=3-6" target="_blank">��̨���ݲ�ѯ</a></li>
                                        <li><a href="#" target="_blank">��̨����ͳ��</a></li>
                                    </ul>
                                </div>
                                <div class="area" id="div_3-6">
                                </div>
                            </div>
                        </div>
                        <div class="area_block">
                            <div class="btn btn-success " name="frm_3-7">
                                <div class="btn-group">
                                    <div class="btn btn-primary disabled" data-toggle="dropdown" name="btn_3-7">
                                        3-7(500T)��֥
                                    </div>
                                    <ul class="dropdown-menu" style="color: Black" role="menu">
                                        <li><a href="javascript:void(0)" deviceid="3-7" style="display: inline">����</a>
                                            <a href="Javascript:void(0)" deviceid="3-7" style="display: inline">�ػ�</a></li>
                                        <li><a href="BuLeHuanMo/BuLeHuanMo.aspx?deviceid=3-7" target="_blank">��ģ��¼</a></li>
                                        <li><a href="MoJu/MoJuBX.aspx?deviceid=3-7 " target="_blank">ģ�߱���.ά��.ȷ��</a></li>
                                        <li><a href="http://172.16.5.7:8088/JobPortal/login.aspx" target="_blank">�豸����</a></li>
                                        <%--<li><a href="/YaSheTou/YST_Record.aspx?deviceid=3-7" target="_blank">ѹ��ͷ��¼</a></li>--%>
                                        <li class="divider"></li>
                                        <li><a href="JingLian/ChangeMo_Query.aspx?deviceid=3-7" target="_blank">��ģ��ѯ</a></li>
                                        <li><a href="MoJu/MojuBX_Query.aspx?deviceid=3-7 " target="_blank">ģ�߱��޲�ѯ</a></li>
                                        <li><a href="http://172.16.5.6:8080/SheBei/Job%20monitoring.aspx " target="_blank">�豸���޲�ѯ</a></li>
                                        <li><a href="TJ/Guzhang_Rate_Report.aspx?quyu=yz3q" target="_blank">�豸����ͳ��</a></li>
                                        <li><a href="TJ/ChangeMo_TJ_Report.aspx?quyu=yz3q" target="_blank">��ģͳ��</a></li>
                                        <li><a href="MoJu/Moju_BX_TongJi.aspx" target="_blank">ģ�߱���ͳ��</a></li>
                                        <li><a href="/YaSheTou/YST_Query.aspx?deviceid=3-7" target="_blank">ѹ��ͷ��ѯ</a></li>
                                        <li class="divider"></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuQuery.aspx" target="_blank">ģ���嵥</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuPJ_List.aspx" target="_blank">ģ�߱���</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuPJRK_List.aspx" target="_blank">ģ�߱������</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuPJLY_List.aspx" target="_blank">ģ�߱�������</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuLY_List.aspx" target="_blank">ģ������</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuRK_List.aspx" target="_blank">ģ�����</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuCL_List.aspx" target="_blank">ģ�ߴ���</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuBY_List.aspx" target="_blank">ģ�߱���</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MojuWX_List.aspx" target="_blank">ģ������</a></li>
                                        <li><a href="/YaSheTou/YST_Maintain.aspx" target="_blank">ѹ��ͷ�嵥</a></li>
                                        <li class="divider"></li>
                                        <li><a href="E_data/EData_Query_Modify.aspx?deviceid=3-7" target="_blank">��̨���ݲ�ѯ</a></li>
                                        <li><a href="#" target="_blank">��̨����ͳ��</a></li>
                                    </ul>
                                </div>
                                <div class="area" id="div_3-7">
                                </div>
                            </div>
                        </div>
                        <div class="area_block">
                            <div class="btn btn-success " name="frm_3-8">
                                <div class="btn-group">
                                    <div class="btn btn-primary disabled" data-toggle="dropdown" name="btn_3-8">
                                        3-8(500T)��֥
                                    </div>
                                    <ul class="dropdown-menu" style="color: Black" role="menu">
                                        <li><a href="javascript:void(0)" deviceid="3-8" style="display: inline">����</a>
                                            <a href="Javascript:void(0)" deviceid="3-8" style="display: inline">�ػ�</a></li>
                                        <li><a href="BuLeHuanMo/BuLeHuanMo.aspx?deviceid=3-8" target="_blank">��ģ��¼</a></li>
                                        <li><a href="MoJu/MoJuBX.aspx?deviceid=3-8 " target="_blank">ģ�߱���.ά��.ȷ��</a></li>
                                        <li><a href="http://172.16.5.7:8088/JobPortal/login.aspx" target="_blank">�豸����</a></li>
                                        <%--<li><a href="/YaSheTou/YST_Record.aspx?deviceid=3-8" target="_blank">ѹ��ͷ��¼</a></li>--%>
                                        <li class="divider"></li>
                                        <li><a href="JingLian/ChangeMo_Query.aspx?deviceid=3-8" target="_blank">��ģ��ѯ</a></li>
                                        <li><a href="MoJu/MojuBX_Query.aspx?deviceid=3-8 " target="_blank">ģ�߱��޲�ѯ</a></li>
                                        <li><a href="http://172.16.5.6:8080/SheBei/Job%20monitoring.aspx " target="_blank">�豸���޲�ѯ</a></li>
                                        <li><a href="TJ/Guzhang_Rate_Report.aspx?quyu=yz3q" target="_blank">�豸����ͳ��</a></li>
                                        <li><a href="TJ/ChangeMo_TJ_Report.aspx?quyu=yz3q" target="_blank">��ģͳ��</a></li>
                                        <li><a href="MoJu/Moju_BX_TongJi.aspx" target="_blank">ģ�߱���ͳ��</a></li>
                                        <li><a href="/YaSheTou/YST_Query.aspx?deviceid=3-8" target="_blank">ѹ��ͷ��ѯ</a></li>
                                        <li class="divider"></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuQuery.aspx" target="_blank">ģ���嵥</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuPJ_List.aspx" target="_blank">ģ�߱���</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuPJRK_List.aspx" target="_blank">ģ�߱������</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuPJLY_List.aspx" target="_blank">ģ�߱�������</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuLY_List.aspx" target="_blank">ģ������</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuRK_List.aspx" target="_blank">ģ�����</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuCL_List.aspx" target="_blank">ģ�ߴ���</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuBY_List.aspx" target="_blank">ģ�߱���</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MojuWX_List.aspx" target="_blank">ģ������</a></li>
                                        <li><a href="/YaSheTou/YST_Maintain.aspx" target="_blank">ѹ��ͷ�嵥</a></li>
                                        <li class="divider"></li>
                                        <li><a href="E_data/EData_Query_Modify.aspx?deviceid=3-8" target="_blank">��̨���ݲ�ѯ</a></li>
                                        <li><a href="#" target="_blank">��̨����ͳ��</a></li>
                                    </ul>
                                </div>
                                <div class="area" id="div_3-8">
                                </div>
                            </div>
                        </div>
                        <div class="area_block">
                            <div class="btn btn-success " name="frm_3-9">
                                <div class="btn-group">
                                    <div class="btn btn-primary disabled" data-toggle="dropdown" name="btn_3-9">
                                        3-9(500T)��֥
                                    </div>
                                    <ul class="dropdown-menu" style="color: Black" role="menu">
                                        <li><a href="javascript:void(0)" deviceid="3-9" style="display: inline">����</a>
                                            <a href="Javascript:void(0)" deviceid="3-9" style="display: inline">�ػ�</a></li>
                                        <li><a href="BuLeHuanMo/BuLeHuanMo.aspx?deviceid=3-9" target="_blank">��ģ��¼</a></li>
                                        <li><a href="MoJu/MoJuBX.aspx?deviceid=3-9 " target="_blank">ģ�߱���.ά��.ȷ��</a></li>
                                        <li><a href="http://172.16.5.7:8088/JobPortal/login.aspx" target="_blank">�豸����</a></li>
                                        <%--<li><a href="/YaSheTou/YST_Record.aspx?deviceid=3-9" target="_blank">ѹ��ͷ��¼</a></li>--%>
                                        <li class="divider"></li>
                                        <li><a href="JingLian/ChangeMo_Query.aspx?deviceid=3-9" target="_blank">��ģ��ѯ</a></li>
                                        <li><a href="MoJu/MojuBX_Query.aspx?deviceid=3-9 " target="_blank">ģ�߱��޲�ѯ</a></li>
                                        <li><a href="http://172.16.5.6:8080/SheBei/Job%20monitoring.aspx " target="_blank">�豸���޲�ѯ</a></li>
                                        <li><a href="TJ/Guzhang_Rate_Report.aspx?quyu=yz3q" target="_blank">�豸����ͳ��</a></li>
                                        <li><a href="TJ/ChangeMo_TJ_Report.aspx?quyu=yz3q" target="_blank">��ģͳ��</a></li>
                                        <li><a href="MoJu/Moju_BX_TongJi.aspx" target="_blank">ģ�߱���ͳ��</a></li>
                                        <li><a href="/YaSheTou/YST_Query.aspx?deviceid=3-9" target="_blank">ѹ��ͷ��ѯ</a></li>
                                        <li class="divider"></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuQuery.aspx" target="_blank">ģ���嵥</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuPJ_List.aspx" target="_blank">ģ�߱���</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuPJRK_List.aspx" target="_blank">ģ�߱������</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuPJLY_List.aspx" target="_blank">ģ�߱�������</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuLY_List.aspx" target="_blank">ģ������</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuRK_List.aspx" target="_blank">ģ�����</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuCL_List.aspx" target="_blank">ģ�ߴ���</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuBY_List.aspx" target="_blank">ģ�߱���</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MojuWX_List.aspx" target="_blank">ģ������</a></li>
                                        <li><a href="/YaSheTou/YST_Maintain.aspx" target="_blank">ѹ��ͷ�嵥</a></li>
                                        <li class="divider"></li>
                                        <li><a href="E_data/EData_Query_Modify.aspx?deviceid=3-9" target="_blank">��̨���ݲ�ѯ</a></li>
                                        <li><a href="#" target="_blank">��̨����ͳ��</a></li>
                                    </ul>
                                </div>
                                <div class="area" id="div_3-9">
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <%--ѹ��4����--%>
    <div class="row  row-container">
        <div class="col-md-12">
            <div class="panel panel-info">
                <div class="panel-heading">
                    <strong>ѹ��4����</strong>
                    <div class="btn-group">
                        <div class="area_drop" data-toggle="dropdown">
                            <span class="caret"></span>
                        </div>
                        <ul class="dropdown-menu" style="color: black" role="menu">
                            <li><span href="#" id="loginyz4q" name="yz4q" title="yz4q" style="padding-left: 20px">����</span></li>
                            <li><span href="#" id="logoutyz4q" name="����" title="����" style="padding-left: 20px">�ǳ�</span></li>
                            <li class="divider"></li>
                            <li><a href="HandOverQuery.aspx?quyu=yz4q&gongwei=����" name="����" target="_blank">���Ӱ��ѯ</a></li>
                        </ul>
                    </div>
                </div>
                <div class="panel-body">
                    <div class="col-sm-12  ">
                        <div class="area_block">
                            <div class="btn btn-success" name="frm_4-1">
                                <div class="btn-group">
                                    <div class="btn btn-primary disabled" data-toggle="dropdown" name="btn_4-1">
                                        4-1(350T)��֥
                                    </div>
                                    <ul class="dropdown-menu" style="color: Black" role="menu">
                                        <li><a href="javascript:void(0)" deviceid="4-1" style="display: inline">����</a>
                                            <a href="Javascript:void(0)" deviceid="4-1" style="display: inline">�ػ�</a></li>
                                        <li><a href="BuLeHuanMo/BuLeHuanMo.aspx?deviceid=4-1" target="_blank">��ģ��¼</a></li>
                                        <li><a href="MoJu/MoJuBX.aspx?deviceid=4-1 " target="_blank">ģ�߱���.ά��.ȷ��</a></li>
                                        <li><a href="http://172.16.5.7:8088/JobPortal/login.aspx" target="_blank">�豸����</a></li>
                                        <%--<li><a href="/YaSheTou/YST_Record.aspx?deviceid=4-1" target="_blank">ѹ��ͷ��¼</a></li>--%>
                                        <li class="divider"></li>
                                        <li><a href="JingLian/ChangeMo_Query.aspx?deviceid=4-1" target="_blank">��ģ��ѯ</a></li>
                                        <li><a href="MoJu/MojuBX_Query.aspx?deviceid=4-1 " target="_blank">ģ�߱��޲�ѯ</a></li>
                                        <li><a href="http://172.16.5.6:8080/SheBei/Job%20monitoring.aspx " target="_blank">�豸���޲�ѯ</a></li>
                                        <li><a href="TJ/Guzhang_Rate_Report.aspx?quyu=yz4q" target="_blank">�豸����ͳ��</a></li>
                                        <li><a href="TJ/ChangeMo_TJ_Report.aspx?quyu=yz4q" target="_blank">��ģͳ��</a></li>
                                        <li><a href="MoJu/Moju_BX_TongJi.aspx" target="_blank">ģ�߱���ͳ��</a></li>
                                        <li><a href="/YaSheTou/YST_Query.aspx?deviceid=4-1" target="_blank">ѹ��ͷ��ѯ</a></li>
                                        <li class="divider"></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuQuery.aspx" target="_blank">ģ���嵥</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuPJ_List.aspx" target="_blank">ģ�߱���</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuPJRK_List.aspx" target="_blank">ģ�߱������</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuPJLY_List.aspx" target="_blank">ģ�߱�������</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuLY_List.aspx" target="_blank">ģ������</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuRK_List.aspx" target="_blank">ģ�����</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuCL_List.aspx" target="_blank">ģ�ߴ���</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuBY_List.aspx" target="_blank">ģ�߱���</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MojuWX_List.aspx" target="_blank">ģ������</a></li>
                                        <li><a href="/YaSheTou/YST_Maintain.aspx" target="_blank">ѹ��ͷ�嵥</a></li>
                                        <li class="divider"></li>
                                        <li><a href="E_data/EData_Query.aspx?deviceid=4-1" target="_blank">��̨���ݲ�ѯ</a></li>
                                        <li><a href="#" target="_blank">��̨����ͳ��</a></li>
                                    </ul>
                                </div>
                                <div class="area" id="div_4-1">
                                </div>
                            </div>
                        </div>
                        <div class="area_block">
                            <div class="btn btn-success " name="frm_4-2">
                                <div class="btn-group">
                                    <div class="btn btn-primary " data-toggle="dropdown" name="btn_4-2">
                                        4-2(350T)��֥
                                    </div>
                                    <ul class="dropdown-menu" style="color: Black" role="menu">
                                        <li><a href="javascript:void(0)" deviceid="4-2" style="display: inline">����</a>
                                            <a href="Javascript:void(0)" deviceid="4-2" style="display: inline">�ػ�</a></li>
                                        <li><a href="BuLeHuanMo/BuLeHuanMo.aspx?deviceid=4-2" target="_blank">��ģ��¼</a></li>
                                        <li><a href="MoJu/MoJuBX.aspx?deviceid=4-2 " target="_blank">ģ�߱���.ά��.ȷ��</a></li>
                                        <li><a href="http://172.16.5.7:8088/JobPortal/login.aspx" target="_blank">�豸����</a></li>
                                        <%--<li><a href="/YaSheTou/YST_Record.aspx?deviceid=4-2" target="_blank">ѹ��ͷ��¼</a></li>--%>
                                        <li class="divider"></li>
                                        <li><a href="JingLian/ChangeMo_Query.aspx?deviceid=4-2" target="_blank">��ģ��ѯ</a></li>
                                        <li><a href="MoJu/MojuBX_Query.aspx?deviceid=4-2 " target="_blank">ģ�߱��޲�ѯ</a></li>
                                        <li><a href="http://172.16.5.6:8080/SheBei/Job%20monitoring.aspx " target="_blank">�豸���޲�ѯ</a></li>
                                        <li><a href="TJ/Guzhang_Rate_Report.aspx?quyu=yz4q" target="_blank">�豸����ͳ��</a></li>
                                        <li><a href="TJ/ChangeMo_TJ_Report.aspx?quyu=yz4q" target="_blank">��ģͳ��</a></li>
                                        <li><a href="MoJu/Moju_BX_TongJi.aspx" target="_blank">ģ�߱���ͳ��</a></li>
                                        <li><a href="/YaSheTou/YST_Query.aspx?deviceid=4-2" target="_blank">ѹ��ͷ��ѯ</a></li>
                                        <li class="divider"></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuQuery.aspx" target="_blank">ģ���嵥</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuPJ_List.aspx" target="_blank">ģ�߱���</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuPJRK_List.aspx" target="_blank">ģ�߱������</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuPJLY_List.aspx" target="_blank">ģ�߱�������</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuLY_List.aspx" target="_blank">ģ������</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuRK_List.aspx" target="_blank">ģ�����</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuCL_List.aspx" target="_blank">ģ�ߴ���</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuBY_List.aspx" target="_blank">ģ�߱���</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MojuWX_List.aspx" target="_blank">ģ������</a></li>
                                        <li><a href="/YaSheTou/YST_Maintain.aspx" target="_blank">ѹ��ͷ�嵥</a></li>
                                        <li class="divider"></li>
                                        <li><a href="E_data/EData_Query.aspx?deviceid=4-2" target="_blank">��̨���ݲ�ѯ</a></li>
                                        <li><a href="#" target="_blank">��̨����ͳ��</a></li>
                                    </ul>
                                </div>
                                <div class="area" id="div_4-2">
                                </div>
                            </div>
                        </div>
                        <div class="area_block">
                            <div class="btn btn-success " name="frm_4-3">
                                <div class="btn-group">
                                    <div class="btn btn-primary " data-toggle="dropdown" name="btn_4-3">
                                        4-3(500T)��֥
                                    </div>
                                    <ul class="dropdown-menu" style="color: Black" role="menu">
                                        <li><a href="javascript:void(0)" deviceid="4-3" style="display: inline">����</a>
                                            <a href="Javascript:void(0)" deviceid="4-3" style="display: inline">�ػ�</a></li>
                                        <li><a href="BuLeHuanMo/BuLeHuanMo.aspx?deviceid=4-3" target="_blank">��ģ��¼</a></li>
                                        <li><a href="MoJu/MoJuBX.aspx?deviceid=4-3 " target="_blank">ģ�߱���.ά��.ȷ��</a></li>
                                        <li><a href="http://172.16.5.7:8088/JobPortal/login.aspx" target="_blank">�豸����</a></li>
                                        <%--<li><a href="/YaSheTou/YST_Record.aspx?deviceid=4-3" target="_blank">ѹ��ͷ��¼</a></li>--%>
                                        <li class="divider"></li>
                                        <li><a href="JingLian/ChangeMo_Query.aspx?deviceid=4-3" target="_blank">��ģ��ѯ</a></li>
                                        <li><a href="MoJu/MojuBX_Query.aspx?deviceid=4-3 " target="_blank">ģ�߱��޲�ѯ</a></li>
                                        <li><a href="http://172.16.5.6:8080/SheBei/Job%20monitoring.aspx " target="_blank">�豸���޲�ѯ</a></li>
                                        <li><a href="TJ/Guzhang_Rate_Report.aspx?quyu=yz4q" target="_blank">�豸����ͳ��</a></li>
                                        <li><a href="TJ/ChangeMo_TJ_Report.aspx?quyu=yz4q" target="_blank">��ģͳ��</a></li>
                                        <li><a href="MoJu/Moju_BX_TongJi.aspx" target="_blank">ģ�߱���ͳ��</a></li>
                                        <li><a href="/YaSheTou/YST_Query.aspx?deviceid=4-3" target="_blank">ѹ��ͷ��ѯ</a></li>
                                        <li class="divider"></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuQuery.aspx" target="_blank">ģ���嵥</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuPJ_List.aspx" target="_blank">ģ�߱���</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuPJRK_List.aspx" target="_blank">ģ�߱������</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuPJLY_List.aspx" target="_blank">ģ�߱�������</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuLY_List.aspx" target="_blank">ģ������</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuRK_List.aspx" target="_blank">ģ�����</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuCL_List.aspx" target="_blank">ģ�ߴ���</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuBY_List.aspx" target="_blank">ģ�߱���</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MojuWX_List.aspx" target="_blank">ģ������</a></li>
                                        <li><a href="/YaSheTou/YST_Maintain.aspx" target="_blank">ѹ��ͷ�嵥</a></li>
                                        <li class="divider"></li>
                                        <li><a href="E_data/EData_Query.aspx?deviceid=4-3" target="_blank">��̨���ݲ�ѯ</a></li>
                                        <li><a href="#" target="_blank">��̨����ͳ��</a></li>
                                    </ul>
                                </div>
                                <div class="area" id="div_4-3">
                                </div>
                            </div>
                        </div>
                        <div class="area_block">
                            <div class="btn btn-success " name="frm_4-4">
                                <div class="btn-group">
                                    <div class="btn btn-primary disabled" data-toggle="dropdown" name="btn_4-4">
                                        4-4(500T)��֥
                                    </div>
                                    <ul class="dropdown-menu" style="color: Black" role="menu">
                                        <li><a href="javascript:void(0)" deviceid="4-4" style="display: inline">����</a>
                                            <a href="Javascript:void(0)" deviceid="4-4" style="display: inline">�ػ�</a></li>
                                        <li><a href="BuLeHuanMo/BuLeHuanMo.aspx?deviceid=4-4" target="_blank">��ģ��¼</a></li>
                                        <li><a href="MoJu/MoJuBX.aspx?deviceid=4-4 " target="_blank">ģ�߱���.ά��.ȷ��</a></li>
                                        <li><a href="http://172.16.5.7:8088/JobPortal/login.aspx" target="_blank">�豸����</a></li>
                                        <%--<li><a href="/YaSheTou/YST_Record.aspx?deviceid=4-4" target="_blank">ѹ��ͷ��¼</a></li>--%>
                                        <li class="divider"></li>
                                        <li><a href="JingLian/ChangeMo_Query.aspx?deviceid=4-4" target="_blank">��ģ��ѯ</a></li>
                                        <li><a href="MoJu/MojuBX_Query.aspx?deviceid=4-4" target="_blank">ģ�߱��޲�ѯ</a></li>
                                        <li><a href="http://172.16.5.6:8080/SheBei/Job%20monitoring.aspx " target="_blank">�豸���޲�ѯ</a></li>
                                        <li><a href="TJ/Guzhang_Rate_Report.aspx?quyu=yz4q" target="_blank">�豸����ͳ��</a></li>
                                        <li><a href="TJ/ChangeMo_TJ_Report.aspx?quyu=yz4q" target="_blank">��ģͳ��</a></li>
                                        <li><a href="MoJu/Moju_BX_TongJi.aspx" target="_blank">ģ�߱���ͳ��</a></li>
                                        <li><a href="/YaSheTou/YST_Query.aspx?deviceid=4-4" target="_blank">ѹ��ͷ��ѯ</a></li>
                                        <li class="divider"></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuQuery.aspx" target="_blank">ģ���嵥</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuPJ_List.aspx" target="_blank">ģ�߱���</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuPJRK_List.aspx" target="_blank">ģ�߱������</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuPJLY_List.aspx" target="_blank">ģ�߱�������</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuLY_List.aspx" target="_blank">ģ������</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuRK_List.aspx" target="_blank">ģ�����</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuCL_List.aspx" target="_blank">ģ�ߴ���</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuBY_List.aspx" target="_blank">ģ�߱���</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MojuWX_List.aspx" target="_blank">ģ������</a></li>
                                        <li><a href="/YaSheTou/YST_Maintain.aspx" target="_blank">ѹ��ͷ�嵥</a></li>
                                        <li class="divider"></li>
                                        <li><a href="E_data/EData_Query.aspx?deviceid=4-4" target="_blank">��̨���ݲ�ѯ</a></li>
                                        <li><a href="#" target="_blank">��̨����ͳ��</a></li>
                                    </ul>
                                </div>
                                <div class="area" id="div_4-4">
                                </div>
                            </div>
                        </div>
                        <div class="area_block">
                            <div class="btn btn-success" name="frm_bule4-5">
                                <div class="btn-group">
                                    <div class="btn btn-primary" data-toggle="dropdown" name="btn_bule4-5">
                                        4-5(660T)����
                                    </div>
                                    <ul class="dropdown-menu" style="color: Black" role="menu">
                                        <li><a href="javascript:void(0)" deviceid="4-5" style="display: inline">����</a>
                                            <a href="Javascript:void(0)" deviceid="4-5" style="display: inline">�ػ�</a></li>
                                        <li><a href="BuLeHuanMo/BuLeHuanMo.aspx?deviceid=4-5" target="_blank">��ģ��¼</a></li>
                                        <li><a href="MoJu/MoJuBX.aspx?deviceid=4-5 " target="_blank">ģ�߱���.ά��.ȷ��</a></li>
                                        <li><a href="http://172.16.5.7:8088/JobPortal/login.aspx" target="_blank">�豸����</a></li>
                                       <%-- <li><a href="/YaSheTou/YST_Record.aspx?deviceid=4-5" target="_blank">ѹ��ͷ��¼</a></li>--%>
                                        <li class="divider"></li>
                                        <li><a href="JingLian/ChangeMo_Query.aspx?deviceid=4-5" target="_blank">��ģ��ѯ</a></li>
                                        <li><a href="MoJu/MojuBX_Query.aspx?deviceid=4-5 " target="_blank">ģ�߱��޲�ѯ</a></li>
                                        <li><a href="http://172.16.5.6:8080/SheBei/Job%20monitoring.aspx " target="_blank">�豸���޲�ѯ</a></li>
                                        <li><a href="TJ/Guzhang_Rate_Report.aspx?quyu=yz4q" target="_blank">�豸����ͳ��</a></li>
                                        <li><a href="TJ/ChangeMo_TJ_Report.aspx?quyu=yz4q" target="_blank">��ģͳ��</a></li>
                                        <li><a href="MoJu/Moju_BX_TongJi.aspx" target="_blank">ģ�߱���ͳ��</a></li>
                                        <li><a href="/YaSheTou/YST_Query.aspx?deviceid=4-5" target="_blank">ѹ��ͷ��ѯ</a></li>
                                        <li class="divider"></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuQuery.aspx" target="_blank">ģ���嵥</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuPJ_List.aspx" target="_blank">ģ�߱���</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuPJRK_List.aspx" target="_blank">ģ�߱������</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuPJLY_List.aspx" target="_blank">ģ�߱�������</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuLY_List.aspx" target="_blank">ģ������</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuRK_List.aspx" target="_blank">ģ�����</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuCL_List.aspx" target="_blank">ģ�ߴ���</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuBY_List.aspx" target="_blank">ģ�߱���</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MojuWX_List.aspx" target="_blank">ģ������</a></li>
                                        <li><a href="/YaSheTou/YST_Maintain.aspx" target="_blank">ѹ��ͷ�嵥</a></li>
                                        <li class="divider"></li>
                                        <li><a href="E_data/EData_Query.aspx?deviceid=4-5" target="_blank">��̨���ݲ�ѯ</a></li>
                                        <li><a href="#" target="_blank">��̨����ͳ��</a></li>
                                    </ul>
                                </div>
                                <div class="area" id="div_bule4-5">
                                </div>
                            </div>
                        </div>
                        <div class="area_block">
                            <div class="btn btn-success" name="frm_bule4-6">
                                <div class="btn-group">
                                    <div class="btn btn-primary" data-toggle="dropdown" name="btn_bule4-6">
                                        4-6(660T)����
                                    </div>
                                    <ul class="dropdown-menu" style="color: Black" role="menu">
                                        <li><a href="javascript:void(0)" deviceid="4-6" style="display: inline">����</a>
                                            <a href="Javascript:void(0)" deviceid="4-6" style="display: inline">�ػ�</a></li>
                                        <li><a href="BuLeHuanMo/BuLeHuanMo.aspx?deviceid=4-6" target="_blank">��ģ��¼</a></li>
                                        <li><a href="MoJu/MoJuBX.aspx?deviceid=4-6 " target="_blank">ģ�߱���.ά��.ȷ��</a></li>
                                        <li><a href="http://172.16.5.7:8088/JobPortal/login.aspx" target="_blank">�豸����</a></li>
                                        <%--<li><a href="/YaSheTou/YST_Record.aspx?deviceid=4-6" target="_blank">ѹ��ͷ��¼</a></li>--%>
                                        <li class="divider"></li>
                                        <li><a href="JingLian/ChangeMo_Query.aspx?deviceid=4-6" target="_blank">��ģ��ѯ</a></li>
                                        <li><a href="MoJu/MojuBX_Query.aspx?deviceid=4-6 " target="_blank">ģ�߱��޲�ѯ</a></li>
                                        <li><a href="http://172.16.5.6:8080/SheBei/Job%20monitoring.aspx " target="_blank">�豸���޲�ѯ</a></li>
                                        <li><a href="TJ/Guzhang_Rate_Report.aspx?quyu=yz4q" target="_blank">�豸����ͳ��</a></li>
                                        <li><a href="TJ/ChangeMo_TJ_Report.aspx?quyu=yz4q" target="_blank">��ģͳ��</a></li>
                                        <li><a href="MoJu/Moju_BX_TongJi.aspx" target="_blank">ģ�߱���ͳ��</a></li>
                                        <li><a href="/YaSheTou/YST_Query.aspx?deviceid=4-6" target="_blank">ѹ��ͷ��ѯ</a></li>
                                        <li class="divider"></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuQuery.aspx" target="_blank">ģ���嵥</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuPJ_List.aspx" target="_blank">ģ�߱���</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuPJRK_List.aspx" target="_blank">ģ�߱������</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuPJLY_List.aspx" target="_blank">ģ�߱�������</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuLY_List.aspx" target="_blank">ģ������</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuRK_List.aspx" target="_blank">ģ�����</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuCL_List.aspx" target="_blank">ģ�ߴ���</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuBY_List.aspx" target="_blank">ģ�߱���</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MojuWX_List.aspx" target="_blank">ģ������</a></li>
                                        <li><a href="/YaSheTou/YST_Maintain.aspx" target="_blank">ѹ��ͷ�嵥</a></li>
                                        <li class="divider"></li>
                                        <li><a href="E_data/EData_Query.aspx?deviceid=4-6" target="_blank">��̨���ݲ�ѯ</a></li>
                                        <li><a href="#" target="_blank">��̨����ͳ��</a></li>
                                    </ul>
                                </div>
                                <div class="area" id="div_bule4-6">
                                </div>
                            </div>
                        </div>
                        <div class="area_block">
                            <div class="btn btn-success" name="frm_bule4-7">
                                <div class="btn-group">
                                    <div class="btn btn-primary" data-toggle="dropdown" name="btn_bule4-7">
                                        4-7(660T)����
                                    </div>
                                    <ul class="dropdown-menu" style="color: Black" role="menu">
                                        <li><a href="javascript:void(0)" deviceid="4-7" style="display: inline">����</a>
                                            <a href="Javascript:void(0)" deviceid="4-7" style="display: inline">�ػ�</a></li>
                                        <li><a href="BuLeHuanMo/BuLeHuanMo.aspx?deviceid=4-7" target="_blank">��ģ��¼</a></li>
                                        <li><a href="MoJu/MoJuBX.aspx?deviceid=4-7 " target="_blank">ģ�߱���.ά��.ȷ��</a></li>
                                        <li><a href="http://172.16.5.7:8088/JobPortal/login.aspx" target="_blank">�豸����</a></li>
                                        <%--<li><a href="/YaSheTou/YST_Record.aspx?deviceid=4-7" target="_blank">ѹ��ͷ��¼</a></li>--%>
                                        <li class="divider"></li>
                                        <li><a href="JingLian/ChangeMo_Query.aspx?deviceid=4-7" target="_blank">��ģ��ѯ</a></li>
                                        <li><a href="MoJu/MojuBX_Query.aspx?deviceid=4-7 " target="_blank">ģ�߱��޲�ѯ</a></li>
                                        <li><a href="http://172.16.5.6:8080/SheBei/Job%20monitoring.aspx " target="_blank">�豸���޲�ѯ</a></li>
                                        <li><a href="TJ/Guzhang_Rate_Report.aspx?quyu=yz4q" target="_blank">�豸����ͳ��</a></li>
                                        <li><a href="TJ/ChangeMo_TJ_Report.aspx?quyu=yz4q" target="_blank">��ģͳ��</a></li>
                                        <li><a href="MoJu/Moju_BX_TongJi.aspx" target="_blank">ģ�߱���ͳ��</a></li>
                                        <li><a href="/YaSheTou/YST_Query.aspx?deviceid=4-7" target="_blank">ѹ��ͷ��ѯ</a></li>
                                        <li class="divider"></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuQuery.aspx" target="_blank">ģ���嵥</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuPJ_List.aspx" target="_blank">ģ�߱���</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuPJRK_List.aspx" target="_blank">ģ�߱������</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuPJLY_List.aspx" target="_blank">ģ�߱�������</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuLY_List.aspx" target="_blank">ģ������</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuRK_List.aspx" target="_blank">ģ�����</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuCL_List.aspx" target="_blank">ģ�ߴ���</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuBY_List.aspx" target="_blank">ģ�߱���</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MojuWX_List.aspx" target="_blank">ģ������</a></li>
                                        <li><a href="/YaSheTou/YST_Maintain.aspx" target="_blank">ѹ��ͷ�嵥</a></li>
                                        <li class="divider"></li>
                                        <li><a href="E_data/EData_Query.aspx?deviceid=4-7" target="_blank">��̨���ݲ�ѯ</a></li>
                                        <li><a href="#" target="_blank">��̨����ͳ��</a></li>
                                    </ul>
                                </div>
                                <div class="area" id="div_bule4-7">
                                </div>
                            </div>
                        </div>
                        <div class="area_block">
                            <div class="btn btn-success" name="frm_bule4-8">
                                <div class="btn-group">
                                    <div class="btn btn-primary" data-toggle="dropdown" name="btn_bule4-8">
                                        4-8(660T)����
                                    </div>
                                    <ul class="dropdown-menu" style="color: Black" role="menu">
                                        <li><a href="javascript:void(0)" deviceid="4-8" style="display: inline">����</a>
                                            <a href="Javascript:void(0)" deviceid="4-8" style="display: inline">�ػ�</a></li>
                                        <li><a href="BuLeHuanMo/BuLeHuanMo.aspx?deviceid=4-8" target="_blank">��ģ��¼</a></li>
                                        <li><a href="MoJu/MoJuBX.aspx?deviceid=4-8 " target="_blank">ģ�߱���.ά��.ȷ��</a></li>
                                        <li><a href="http://172.16.5.7:8088/JobPortal/login.aspx" target="_blank">�豸����</a></li>
                                        <%--<li><a href="/YaSheTou/YST_Record.aspx?deviceid=4-8" target="_blank">ѹ��ͷ��¼</a></li>--%>
                                        <li class="divider"></li>
                                        <li><a href="JingLian/ChangeMo_Query.aspx?deviceid=4-8" target="_blank">��ģ��ѯ</a></li>
                                        <li><a href="MoJu/MojuBX_Query.aspx?deviceid=4-8 " target="_blank">ģ�߱��޲�ѯ</a></li>
                                        <li><a href="http://172.16.5.6:8080/SheBei/Job%20monitoring.aspx " target="_blank">�豸���޲�ѯ</a></li>
                                        <li><a href="TJ/Guzhang_Rate_Report.aspx?quyu=yz4q" target="_blank">�豸����ͳ��</a></li>
                                        <li><a href="TJ/ChangeMo_TJ_Report.aspx?quyu=yz4q" target="_blank">��ģͳ��</a></li>
                                        <li><a href="MoJu/Moju_BX_TongJi.aspx" target="_blank">ģ�߱���ͳ��</a></li>
                                        <li><a href="/YaSheTou/YST_Query.aspx?deviceid=4-8" target="_blank">ѹ��ͷ��ѯ</a></li>
                                        <li class="divider"></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuQuery.aspx" target="_blank">ģ���嵥</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuPJ_List.aspx" target="_blank">ģ�߱���</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuPJRK_List.aspx" target="_blank">ģ�߱������</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuPJLY_List.aspx" target="_blank">ģ�߱�������</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuLY_List.aspx" target="_blank">ģ������</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuRK_List.aspx" target="_blank">ģ�����</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuCL_List.aspx" target="_blank">ģ�ߴ���</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuBY_List.aspx" target="_blank">ģ�߱���</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MojuWX_List.aspx" target="_blank">ģ������</a></li>
                                        <li><a href="/YaSheTou/YST_Maintain.aspx" target="_blank">ѹ��ͷ�嵥</a></li>
                                        <li class="divider"></li>
                                        <li><a href="E_data/EData_Query.aspx?deviceid=4-8" target="_blank">��̨���ݲ�ѯ</a></li>
                                        <li><a href="#" target="_blank">��̨����ͳ��</a></li>
                                    </ul>
                                </div>
                                <div class="area" id="div_bule4-8">
                                </div>
                            </div>
                        </div>
                        <div class="area_block">
                            <div class="btn btn-success" name="frm_bule4-9">
                                <div class="btn-group">
                                    <div class="btn btn-primary" data-toggle="dropdown" name="btn_bule4-9">
                                        4-9(660T)����
                                    </div>
                                    <ul class="dropdown-menu" style="color: Black" role="menu">
                                        <li><a href="javascript:void(0)" deviceid="4-9" style="display: inline">����</a>
                                            <a href="Javascript:void(0)" deviceid="4-9" style="display: inline">�ػ�</a></li>
                                        <li><a href="BuLeHuanMo/BuLeHuanMo.aspx?deviceid=4-9" target="_blank">��ģ��¼</a></li>
                                        <li><a href="MoJu/MoJuBX.aspx?deviceid=4-9 " target="_blank">ģ�߱���.ά��.ȷ��</a></li>
                                        <li><a href="http://172.16.5.7:8088/JobPortal/login.aspx" target="_blank">�豸����</a></li>
                                        <%--<li><a href="/YaSheTou/YST_Record.aspx?deviceid=4-9" target="_blank">ѹ��ͷ��¼</a></li>--%>
                                        <li class="divider"></li>
                                        <li><a href="JingLian/ChangeMo_Query.aspx?deviceid=4-9" target="_blank">��ģ��ѯ</a></li>
                                        <li><a href="MoJu/MojuBX_Query.aspx?deviceid=4-9 " target="_blank">ģ�߱��޲�ѯ</a></li>
                                        <li><a href="http://172.16.5.6:8080/SheBei/Job%20monitoring.aspx " target="_blank">�豸���޲�ѯ</a></li>
                                        <li><a href="TJ/Guzhang_Rate_Report.aspx?quyu=yz4q" target="_blank">�豸����ͳ��</a></li>
                                        <li><a href="TJ/ChangeMo_TJ_Report.aspx?quyu=yz4q" target="_blank">��ģͳ��</a></li>
                                        <li><a href="MoJu/Moju_BX_TongJi.aspx" target="_blank">ģ�߱���ͳ��</a></li>
                                        <li><a href="/YaSheTou/YST_Query.aspx?deviceid=4-9" target="_blank">ѹ��ͷ��ѯ</a></li>
                                        <li class="divider"></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuQuery.aspx" target="_blank">ģ���嵥</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuPJ_List.aspx" target="_blank">ģ�߱���</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuPJRK_List.aspx" target="_blank">ģ�߱������</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuPJLY_List.aspx" target="_blank">ģ�߱�������</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuLY_List.aspx" target="_blank">ģ������</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuRK_List.aspx" target="_blank">ģ�����</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuCL_List.aspx" target="_blank">ģ�ߴ���</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuBY_List.aspx" target="_blank">ģ�߱���</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MojuWX_List.aspx" target="_blank">ģ������</a></li>
                                        <li><a href="/YaSheTou/YST_Maintain.aspx" target="_blank">ѹ��ͷ�嵥</a></li>
                                        <li class="divider"></li>
                                        <li><a href="E_data/EData_Query.aspx?deviceid=4-9" target="_blank">��̨���ݲ�ѯ</a></li>
                                        <li><a href="#" target="_blank">��̨����ͳ��</a></li>
                                    </ul>
                                </div>
                                <div class="area" id="div_bule4-9">
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <%--ѹ��5����--%>
    <div class="row  row-container">
        <div class="col-md-12">
            <div class="panel panel-info">
                <div class="panel-heading">
                    <strong>ѹ��5����</strong>
                    <div class="btn-group">
                        <div class="area_drop" data-toggle="dropdown">
                            <span class="caret"></span>
                        </div>
                        <ul class="dropdown-menu" style="color: black" role="menu">
                            <li><span href="#" id="loginyz5q" name="yz5q" title="yz5q" style="padding-left: 20px">����</span></li>
                            <li><span href="#" id="logoutyz5q" name="����" title="����" style="padding-left: 20px">�ǳ�</span></li>
                            <li class="divider"></li>
                            <li><a href="HandOverQuery.aspx?quyu=yz5q&gongwei=����" name="����" target="_blank">���Ӱ��ѯ</a></li>
                        </ul>
                    </div>
                </div>
                <div class="panel-body">
                    <div class="col-xs-12 col-sm-12  col-md-12 col-lg-12">
                        <div class="area_block">
                            <div class="btn btn-padding-s btn-success " name="frm_5-1">
                                <div class="btn-group">
                                    <div class="btn btn-primary " data-toggle="dropdown" name="btn_5-1">
                                        5-1(1650T)��֥
                                    </div>
                                    <ul class="dropdown-menu" style="color: Black" role="menu">
                                        <li><a href="javascript:void(0)" deviceid="5-1" style="display: inline">����</a>
                                            <a href="Javascript:void(0)" deviceid="5-1" style="display: inline">�ػ�</a></li>
                                        <li><a href="BuLeHuanMo/BuLeHuanMo.aspx?deviceid=5-1" target="_blank">��ģ��¼</a></li>
                                        <li><a href="MoJu/MoJuBX.aspx?deviceid=5-1 " target="_blank">ģ�߱���.ά��.ȷ��</a></li>
                                        <li><a href="http://172.16.5.7:8088/JobPortal/login.aspx" target="_blank">�豸����</a></li>
                                        <%--<li><a href="/YaSheTou/YST_Record.aspx?deviceid=5-1" target="_blank">ѹ��ͷ��¼</a></li>--%>
                                        <li class="divider"></li>
                                        <li><a href="JingLian/ChangeMo_Query.aspx?deviceid=5-1" target="_blank">��ģ��ѯ</a></li>
                                        <li><a href="MoJu/MojuBX_Query.aspx?deviceid=5-1 " target="_blank">ģ�߱��޲�ѯ</a></li>
                                        <li><a href="http://172.16.5.6:8080/SheBei/Job%20monitoring.aspx " target="_blank">�豸���޲�ѯ</a></li>
                                        <li><a href="TJ/Guzhang_Rate_Report.aspx?quyu=yz5q" target="_blank">�豸����ͳ��</a></li>
                                        <li><a href="TJ/ChangeMo_TJ_Report.aspx?quyu=yz5q" target="_blank">��ģͳ��</a></li>
                                        <li><a href="MoJu/Moju_BX_TongJi.aspx" target="_blank">ģ�߱���ͳ��</a></li>
                                        <li><a href="/YaSheTou/YST_Query.aspx?deviceid=5-1" target="_blank">ѹ��ͷ��ѯ</a></li>
                                        <li class="divider"></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuQuery.aspx" target="_blank">ģ���嵥</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuPJ_List.aspx" target="_blank">ģ�߱���</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuPJRK_List.aspx" target="_blank">ģ�߱������</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuPJLY_List.aspx" target="_blank">ģ�߱�������</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuLY_List.aspx" target="_blank">ģ������</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuRK_List.aspx" target="_blank">ģ�����</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuCL_List.aspx" target="_blank">ģ�ߴ���</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuBY_List.aspx" target="_blank">ģ�߱���</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MojuWX_List.aspx" target="_blank">ģ������</a></li>
                                        <li><a href="/YaSheTou/YST_Maintain.aspx" target="_blank">ѹ��ͷ�嵥</a></li>
                                        <li class="divider"></li>
                                        <li><a href="E_data/EData_Query.aspx?deviceid=5-1" target="_blank">��̨���ݲ�ѯ</a></li>
                                        <li><a href="#" target="_blank">��̨����ͳ��</a></li>
                                    </ul>
                                </div>
                                <div class="area" id="div_5-1">
                                </div>
                            </div>
                        </div>
                        <div class="area_block">
                            <div class="btn btn-padding-s btn-success " name="frm_5-2">
                                <div class="btn-group">
                                    <div class="btn btn-primary " data-toggle="dropdown" name="btn_5-2">
                                        5-2(1600T)�����
                                    </div>
                                    <ul class="dropdown-menu" style="color: Black" role="menu">
                                        <li><a href="javascript:void(0)" deviceid="5-2" style="display: inline">����</a>
                                            <a href="Javascript:void(0)" deviceid="5-2" style="display: inline">�ػ�</a></li>
                                        <li><a href="BuLeHuanMo/BuLeHuanMo.aspx?deviceid=5-2" target="_blank">��ģ��¼</a></li>
                                        <li><a href="MoJu/MoJuBX.aspx?deviceid=5-2 " target="_blank">ģ�߱���.ά��.ȷ��</a></li>
                                        <li><a href="http://172.16.5.7:8088/JobPortal/login.aspx" target="_blank">�豸����</a></li>
                                        <%--<li><a href="/YaSheTou/YST_Record.aspx?deviceid=5-2" target="_blank">ѹ��ͷ��¼</a></li>--%>
                                        <li class="divider"></li>
                                        <li><a href="JingLian/ChangeMo_Query.aspx?deviceid=5-2" target="_blank">��ģ��ѯ</a></li>
                                        <li><a href="MoJu/MojuBX_Query.aspx?deviceid=5-2 " target="_blank">ģ�߱��޲�ѯ</a></li>
                                        <li><a href="http://172.16.5.6:8080/SheBei/Job%20monitoring.aspx " target="_blank">�豸���޲�ѯ</a></li>
                                        <li><a href="TJ/Guzhang_Rate_Report.aspx?quyu=yz5q" target="_blank">�豸����ͳ��</a></li>
                                        <li><a href="TJ/ChangeMo_TJ_Report.aspx?quyu=yz5q" target="_blank">��ģͳ��</a></li>
                                        <li><a href="MoJu/Moju_BX_TongJi.aspx" target="_blank">ģ�߱���ͳ��</a></li>
                                        <li><a href="/YaSheTou/YST_Query.aspx?deviceid=5-2" target="_blank">ѹ��ͷ��ѯ</a></li>
                                        <li class="divider"></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuQuery.aspx" target="_blank">ģ���嵥</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuPJ_List.aspx" target="_blank">ģ�߱���</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuPJRK_List.aspx" target="_blank">ģ�߱������</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuPJLY_List.aspx" target="_blank">ģ�߱�������</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuLY_List.aspx" target="_blank">ģ������</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuRK_List.aspx" target="_blank">ģ�����</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuCL_List.aspx" target="_blank">ģ�ߴ���</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuBY_List.aspx" target="_blank">ģ�߱���</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MojuWX_List.aspx" target="_blank">ģ������</a></li>
                                        <li><a href="/YaSheTou/YST_Maintain.aspx" target="_blank">ѹ��ͷ�嵥</a></li>
                                        <li class="divider"></li>
                                        <li><a href="E_data/EData_Query.aspx?deviceid=5-2" target="_blank">��̨���ݲ�ѯ</a></li>
                                        <li><a href="#" target="_blank">��̨����ͳ��</a></li>
                                    </ul>
                                </div>
                                <div class="area" id="div_5-2">
                                </div>
                            </div>
                        </div>
                        <div class="area_block">
                            <div class="btn btn-padding-s btn-success " name="frm_5-3">
                                <div class="btn-group">
                                    <div class="btn btn-primary " data-toggle="dropdown" name="btn_5-3">
                                        5-3(2200T)�����
                                    </div>
                                    <ul class="dropdown-menu" style="color: Black" role="menu">
                                        <li><a href="javascript:void(0)" deviceid="5-3" style="display: inline">����</a>
                                            <a href="Javascript:void(0)" deviceid="5-3" style="display: inline">�ػ�</a></li>
                                        <li><a href="BuLeHuanMo/BuLeHuanMo.aspx?deviceid=5-3" target="_blank">��ģ��¼</a></li>
                                        <li><a href="MoJu/MoJuBX.aspx?deviceid=5-3 " target="_blank">ģ�߱���.ά��.ȷ��</a></li>
                                        <li><a href="http://172.16.5.7:8088/JobPortal/login.aspx" target="_blank">�豸����</a></li>
                                        <%--<li><a href="/YaSheTou/YST_Record.aspx?deviceid=5-3" target="_blank">ѹ��ͷ��¼</a></li>--%>
                                        <li class="divider"></li>
                                        <li><a href="JingLian/ChangeMo_Query.aspx?deviceid=5-3" target="_blank">��ģ��ѯ</a></li>
                                        <li><a href="MoJu/MojuBX_Query.aspx?deviceid=5-3 " target="_blank">ģ�߱��޲�ѯ</a></li>
                                        <li><a href="http://172.16.5.6:8080/SheBei/Job%20monitoring.aspx " target="_blank">�豸���޲�ѯ</a></li>
                                        <li><a href="TJ/Guzhang_Rate_Report.aspx?quyu=yz5q" target="_blank">�豸����ͳ��</a></li>
                                        <li><a href="TJ/ChangeMo_TJ_Report.aspx?quyu=yz5q" target="_blank">��ģͳ��</a></li>
                                        <li><a href="MoJu/Moju_BX_TongJi.aspx" target="_blank">ģ�߱���ͳ��</a></li>
                                        <li><a href="/YaSheTou/YST_Query.aspx?deviceid=5-3" target="_blank">ѹ��ͷ��ѯ</a></li>
                                        <li class="divider"></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuQuery.aspx" target="_blank">ģ���嵥</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuPJ_List.aspx" target="_blank">ģ�߱���</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuPJRK_List.aspx" target="_blank">ģ�߱������</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuPJLY_List.aspx" target="_blank">ģ�߱�������</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuLY_List.aspx" target="_blank">ģ������</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuRK_List.aspx" target="_blank">ģ�����</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuCL_List.aspx" target="_blank">ģ�ߴ���</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MoJuBY_List.aspx" target="_blank">ģ�߱���</a></li>
                                         <li><a href="http://172.16.5.6:8080/wuliu/MojuWX_List.aspx" target="_blank">ģ������</a></li>
                                        <li><a href="/YaSheTou/YST_Maintain.aspx" target="_blank">ѹ��ͷ�嵥</a></li>
                                        <li class="divider"></li>
                                        <li><a href="E_data/EData_Query.aspx?deviceid=5-3" target="_blank">��̨���ݲ�ѯ</a></li>
                                        <li><a href="#" target="_blank">��̨����ͳ��</a></li>
                                    </ul>
                                </div>
                                <div class="area" id="div_5-3">
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <%--��������^--%>
    <div class="row  row-container">
        <div class="col-md-12">
            <div class="panel panel-info">
                <div class="panel-heading">
                    <strong>����������</strong>
                    <div class="btn-group">
                        <div class="area_drop" data-toggle="dropdown">
                            <span class="caret"></span>
                        </div>
                        <ul class="dropdown-menu" style="color: black" role="menu">
                            <li><span href="#" id="loginpw" name="PW" title="PW" style="padding-left: 20px">����</span></li>
                            <li><span href="#" id="logoutpw" name="����" title="����" style="padding-left: 20px">�ǳ�</span></li>
                            <li class="divider"></li>
                            <li><a href="HandOverQuery.aspx?quyu=pw&gongwei=����" name="����" target="_blank">���Ӱ��ѯ</a></li>
                        </ul>
                    </div>
                </div>
                <div class="panel-body">
                    <div class="col-xs-12 col-sm-12  col-md-12 col-lg-12">
                        <div class="area_block">
                            <div class="btn btn-padding-s btn-success " name="frm_PW01">
                                <div class="btn-group">
                                    <div class="btn btn-primary " data-toggle="dropdown" name="btn_PW01">
                                        �¶������(M1025)
                                    </div>
                                    <ul class="dropdown-menu" style="color: Black" role="menu">
                                        <li><a href="javascript:void(0)" deviceid="PW01" style="display: inline">����</a>
                                            <a href="Javascript:void(0)" deviceid="PW01" style="display: inline">�ػ�</a></li>                                        
                                        <li><a href="http://172.16.5.7:8088/JobPortal/login.aspx" target="_blank">�豸����</a></li>
                                        <li><a href="PW/PW_Add.aspx?deviceid=PW01" target="_blank">������ϼ�¼</a></li>
                                         <li><a href="PW/PW_Clear.aspx?deviceid=PW01" target="_blank">���������</a></li>
                                         <li class="divider"></li>                                        
                                        <li><a href="http://172.16.5.6:8080/SheBei/Job%20monitoring.aspx " target="_blank">�豸���޲�ѯ</a></li>
                                        <li><a href="TJ/Guzhang_Rate_Report.aspx?quyu=yz5q" target="_blank">�豸����ͳ��</a></li>
                                          <li><a href="PW/PW_Add_Query.aspx" target="_blank">������Ӳ�ѯ</a></li>
                                         <li><a href="PW/PW_Clear_Query.aspx" target="_blank">����������ѯ</a></li>
                                           <li><a href="PW/PW_Add_TJReport.aspx" target="_blank">�������ͳ�Ʊ���</a></li>
                                    </ul>
                                </div>
                                <div class="area" id="div_PW01">
                                </div>
                            </div>
                        </div>
                        <div class="area_block">
                            <div class="btn btn-padding-s btn-success " name="frm_PW02">
                                <div class="btn-group">
                                    <div class="btn btn-primary " data-toggle="dropdown" name="btn_PW02">
                                        �¶������(M1026)
                                    </div>
                                    <ul class="dropdown-menu" style="color: Black" role="menu">
                                        <li><a href="javascript:void(0)" deviceid="PW02" style="display: inline">����</a>
                                            <a href="Javascript:void(0)" deviceid="PW02" style="display: inline">�ػ�</a></li>
                                        <li><a href="http://172.16.5.7:8088/JobPortal/login.aspx" target="_blank">�豸����</a></li>
                                        <li><a href="PW/PW_Add.aspx?deviceid=PW02" target="_blank">������ϼ�¼</a></li>
                                         <li><a href="PW/PW_Clear.aspx?deviceid=PW02" target="_blank">���������</a></li>
                                         <li class="divider"></li>                                        
                                        <li><a href="http://172.16.5.6:8080/SheBei/Job%20monitoring.aspx " target="_blank">�豸���޲�ѯ</a></li>
                                        <li><a href="TJ/Guzhang_Rate_Report.aspx?quyu=yz5q" target="_blank">�豸����ͳ��</a></li>
                                        <li><a href="PW/PW_Add_Query.aspx" target="_blank">������Ӳ�ѯ</a></li>
                                         <li><a href="PW/PW_Clear_Query.aspx" target="_blank">����������ѯ</a></li>
                                         <li><a href="PW/PW_Add_TJReport.aspx" target="_blank">�������ͳ�Ʊ���</a></li>

                                    </ul>
                                </div>
                                <div class="area" id="div_PW02">
                                </div>
                            </div>
                        </div>
                        <div class="area_block">
                            <div class="btn btn-padding-s btn-success " name="frm_PW03">
                                <div class="btn-group">
                                    <div class="btn btn-primary " data-toggle="dropdown" name="btn_PW03">
                                        ���������(M1104)
                                    </div>
                                    <ul class="dropdown-menu" style="color: Black" role="menu">
                                        <li><a href="javascript:void(0)" deviceid="PW03" style="display: inline">����</a>
                                            <a href="Javascript:void(0)" deviceid="PW03" style="display: inline">�ػ�</a></li>
                                        <li><a href="http://172.16.5.7:8088/JobPortal/login.aspx" target="_blank">�豸����</a></li>
                                        <li><a href="PW/PW_Add.aspx?deviceid=PW03" target="_blank">������ϼ�¼</a></li>
                                         <li><a href="PW/PW_Clear.aspx?deviceid=PW03" target="_blank">���������</a></li>
                                         <li class="divider"></li>                                        
                                        <li><a href="http://172.16.5.6:8080/SheBei/Job%20monitoring.aspx " target="_blank">�豸���޲�ѯ</a></li>
                                        <li><a href="TJ/Guzhang_Rate_Report.aspx?quyu=yz5q" target="_blank">�豸����ͳ��</a></li>
                                        <li><a href="PW/PW_Add_Query.aspx" target="_blank">������Ӳ�ѯ</a></li>
                                         <li><a href="PW/PW_Clear_Query.aspx" target="_blank">����������ѯ</a></li>
                                         <li><a href="PW/PW_Add_TJReport.aspx" target="_blank">�������ͳ�Ʊ���</a></li>

                                    </ul>
                                </div>
                                <div class="area" id="div_PW03">
                                </div>
                            </div>
                        </div>
                        <div class="area_block">
                            <div class="btn btn-padding-s btn-success " name="frm_PW04">
                                <div class="btn-group">
                                    <div class="btn btn-primary " data-toggle="dropdown" name="btn_PW04">
                                        ���������(M1142)
                                    </div>
                                    <ul class="dropdown-menu" style="color: Black" role="menu">
                                        <li><a href="javascript:void(0)" deviceid="PW04" style="display: inline">����</a>
                                            <a href="Javascript:void(0)" deviceid="PW04" style="display: inline">�ػ�</a></li>
                                        <li><a href="http://172.16.5.7:8088/JobPortal/login.aspx" target="_blank">�豸����</a></li>
                                        <li><a href="PW/PW_Add.aspx?deviceid=PW04" target="_blank">������ϼ�¼</a></li>
                                         <li><a href="PW/PW_Clear.aspx?deviceid=PW04" target="_blank">���������</a></li>
                                         <li class="divider"></li>                                        
                                        <li><a href="http://172.16.5.6:8080/SheBei/Job%20monitoring.aspx " target="_blank">�豸���޲�ѯ</a></li>
                                        <li><a href="TJ/Guzhang_Rate_Report.aspx?quyu=yz5q" target="_blank">�豸����ͳ��</a></li>
                                        <li><a href="PW/PW_Add_Query.aspx" target="_blank">������Ӳ�ѯ</a></li>
                                         <li><a href="PW/PW_Clear_Query.aspx" target="_blank">����������ѯ</a></li>
                                         <li><a href="PW/PW_Add_TJReport.aspx" target="_blank">�������ͳ�Ʊ���</a></li>
                                    </ul>
                                </div>
                                <div class="area" id="div_PW04">
                                </div>
                            </div>
                        </div>
                        <div class="area_block">
                            <div class="btn btn-padding-s btn-success " name="frm_PW05">
                                <div class="btn-group">
                                    <div class="btn btn-primary " data-toggle="dropdown" name="btn_PW05">
                                        ���������(M1252)
                                    </div>
                                    <ul class="dropdown-menu" style="color: Black" role="menu">
                                        <li><a href="javascript:void(0)" deviceid="PW05" style="display: inline">����</a>
                                            <a href="Javascript:void(0)" deviceid="PW05" style="display: inline">�ػ�</a></li>
                                        <li><a href="http://172.16.5.7:8088/JobPortal/login.aspx" target="_blank">�豸����</a></li>
                                        <li><a href="PW/PW_Add.aspx?deviceid=PW05" target="_blank">������ϼ�¼</a></li>
                                         <li><a href="PW/PW_Clear.aspx?deviceid=PW05" target="_blank">���������</a></li>
                                         <li class="divider"></li>                                        
                                        <li><a href="http://172.16.5.6:8080/SheBei/Job%20monitoring.aspx " target="_blank">�豸���޲�ѯ</a></li>
                                        <li><a href="TJ/Guzhang_Rate_Report.aspx?quyu=yz5q" target="_blank">�豸����ͳ��</a></li>
                                        <li><a href="PW/PW_Add_Query.aspx" target="_blank">������Ӳ�ѯ</a></li>
                                        <li><a href="PW/PW_Clear_Query.aspx" target="_blank">����������ѯ</a></li>
                                        <li><a href="PW/PW_Add_TJReport.aspx" target="_blank">�������ͳ�Ʊ���</a></li>

                                    </ul>
                                </div>
                                <div class="area" id="div_PW05">
                                </div>
                            </div>
                        </div>
                           <div class="area_block">
                            <div class="btn btn-padding-s btn-success " name="frm_PW06">
                                <div class="btn-group">
                                    <div class="btn btn-primary " data-toggle="dropdown" name="btn_PW06">
                                        ��������ʽ�����(M1448)
                                    </div>
                                    <ul class="dropdown-menu" style="color: Black" role="menu">
                                        <li><a href="javascript:void(0)" deviceid="PW06" style="display: inline">����</a>
                                            <a href="Javascript:void(0)" deviceid="PW06" style="display: inline">�ػ�</a></li>
                                        <li><a href="http://172.16.5.7:8088/JobPortal/login.aspx" target="_blank">�豸����</a></li>
                                        <li><a href="PW/PW_Add.aspx?deviceid=PW06" target="_blank">������ϼ�¼</a></li>
                                         <li><a href="PW/PW_Clear.aspx?deviceid=PW06" target="_blank">���������</a></li>
                                         <li class="divider"></li>                                        
                                        <li><a href="http://172.16.5.6:8080/SheBei/Job%20monitoring.aspx " target="_blank">�豸���޲�ѯ</a></li>
                                        <li><a href="TJ/Guzhang_Rate_Report.aspx?quyu=yz5q" target="_blank">�豸����ͳ��</a></li>
                                        <li><a href="PW/PW_Add_Query.aspx" target="_blank">������Ӳ�ѯ</a></li>
                                        <li><a href="PW/PW_Clear_Query.aspx" target="_blank">����������ѯ</a></li>
                                        <li><a href="PW/PW_Add_TJReport.aspx" target="_blank">�������ͳ�Ʊ���</a></li>

                                    </ul>
                                </div>
                                <div class="area" id="div_PW06">
                                </div>
                            </div>
                        </div>
                        <div class="area_block">
                            <div class="btn btn-padding-s btn-success " name="frm_PW07">
                                <div class="btn-group">
                                    <div class="btn btn-primary " data-toggle="dropdown" name="btn_PW07">
                                        ��������ʽ�����(M1449)
                                    </div>
                                    <ul class="dropdown-menu" style="color: Black" role="menu">
                                        <li><a href="javascript:void(0)" deviceid="PW07" style="display: inline">����</a>
                                            <a href="Javascript:void(0)" deviceid="PW07" style="display: inline">�ػ�</a></li>
                                        <li><a href="http://172.16.5.7:8088/JobPortal/login.aspx" target="_blank">�豸����</a></li>
                                        <li><a href="PW/PW_Add.aspx?deviceid=PW07" target="_blank">������ϼ�¼</a></li>
                                         <li><a href="PW/PW_Clear.aspx?deviceid=PW07" target="_blank">���������</a></li>
                                         <li class="divider"></li>                                        
                                        <li><a href="http://172.16.5.6:8080/SheBei/Job%20monitoring.aspx " target="_blank">�豸���޲�ѯ</a></li>
                                        <li><a href="TJ/Guzhang_Rate_Report.aspx?quyu=yz5q" target="_blank">�豸����ͳ��</a></li>
                                        <li><a href="PW/PW_Add_Query.aspx" target="_blank">������Ӳ�ѯ</a></li>
                                        <li><a href="PW/PW_Clear_Query.aspx" target="_blank">����������ѯ</a></li>
                                        <li><a href="PW/PW_Add_TJReport.aspx" target="_blank">�������ͳ�Ʊ���</a></li>

                                    </ul>
                                </div>
                                <div class="area" id="div_PW07">
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
