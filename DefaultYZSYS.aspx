<%@ Page Title="MES��������ϵͳ" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="DefaultYZSYS.aspx.cs" Inherits="DefaultYZSYS" MaintainScrollPositionOnPostback="True" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
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
                        //                        $("#table_list_2").jqGrid('setGridParam', {
                        //                            datatype: 'json', postData: { 'YaoXinId': $("#YaoXinId").val() }, //��������
                        //                            page: 1  }).trigger("reloadGrid");
                        //                         
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
            setInterval(show, 10000);
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
            function warningset(equipno, frm) {
                $.post("InitDefaultInfoHandler.ashx?flag=equipstatus&shebei=" + equipno, { shebei: "" }, function (result) {

                    var json = eval(result);

                    if (json.color == "") { //remove

                        $("div[name*='" + frm + "']").removeClass("btn-red").removeClass("btn-yellow");
                    }
                    else if (result.color != "") { //exists or add
                        $("div[name*='" + frm + "']").removeClass("btn-red").removeClass("btn-yellow");
                        $("div[name*='" + frm + "']").addClass("btn-" + json.color);


                    }

                });



            }
            function show() {

                //                $.post("InitDefaultInfoHandler.ashx?flag=RL&shebei=D", { shebei: "" }, function (result) {
                //                    if (result != "") { $("div[id*='div_D']").html(result); }
                //                    setable(result, "frm_D", "btn_D"); warningset("D", "frm_D");
                //                });


                //ѹ��ʵ����
                $.post("Default_jl_Handler.ashx?flag=GP&shebei=GP", { shebei: "" }, function (result) {
                    //����              
                    if (result != "") { $("div[id*='div_GP']").html(result); }
                    setable(result, "frm_GP", "btn_GP"); warningset("����", "frm_GP");
                });
                //Equator������
                $.post("InitDefaultInfoHandler.ashx?flag=Eqcls&shebei=M0978", { shebei: "" }, function (result) {
                               
                    if (result != "") { $("div[id*='div_M0978']").html(result); }
                    setable(result, "frm_M0978", "btn_M0978"); warningset("M0978", "frm_M0978");
                });

                $.post("InitDefaultInfoHandler.ashx?flag=Eqcls&shebei=M0979", { shebei: "" }, function (result) {
                                  
                    if (result != "") { $("div[id*='div_M0979']").html(result); }
                    setable(result, "frm_M0979", "btn_M0979"); warningset("M0979", "frm_M0979");
                });
                //���Ӳ�����
                $.post("InitDefaultInfoHandler.ashx?flag=jjcls&shebei=M0407", { shebei: "" }, function (result) {

                    if (result != "") { $("div[id*='div_M0407']").html(result); }
                    setable(result, "frm_M0407", "btn_M0407"); warningset("M0407", "frm_M0407");
                });

                $.post("InitDefaultInfoHandler.ashx?flag=jjcls&shebei=M0193", { shebei: "" }, function (result) {

                    if (result != "") { $("div[id*='div_M0193']").html(result); }
                    setable(result, "frm_M0193", "btn_M0193"); warningset("M0193", "frm_M0193");
                });
                $.post("InitDefaultInfoHandler.ashx?flag=jjcls&shebei=M0811", { shebei: "" }, function (result) {

                    if (result != "") { $("div[id*='div_M0811']").html(result); }
                    setable(result, "frm_M0811", "btn_M0811"); warningset("M0811", "frm_M0811");
                });

                $.post("InitDefaultInfoHandler.ashx?flag=jjcls&shebei=M0202", { shebei: "" }, function (result) {

                    if (result != "") { $("div[id*='div_M0202']").html(result); }
                    setable(result, "frm_M0202", "btn_M0202"); warningset("M0202", "frm_M0202");
                });
                $.post("InitDefaultInfoHandler.ashx?flag=jjcls&shebei=M0018", { shebei: "" }, function (result) {

                    if (result != "") { $("div[id*='div_M0018']").html(result); }
                    setable(result, "frm_M0018", "btn_M0018"); warningset("M0018", "frm_M0018");
                });

                $.post("InitDefaultInfoHandler.ashx?flag=jjcls&shebei=M0524", { shebei: "" }, function (result) {

                    if (result != "") { $("div[id*='div_M0524']").html(result); }
                    setable(result, "frm_M0524", "btn_M0524"); warningset("M0524", "frm_M0524");
                });
                $.post("InitDefaultInfoHandler.ashx?flag=jjcls&shebei=M0808", { shebei: "" }, function (result) {

                    if (result != "") { $("div[id*='div_M0808']").html(result); }
                    setable(result, "frm_M0808", "btn_M0808"); warningset("M0808", "frm_M0808");
                });
                $.post("InitDefaultInfoHandler.ashx?flag=jjcls&shebei=M0347", { shebei: "" }, function (result) {

                    if (result != "") { $("div[id*='div_M0347']").html(result); }
                    setable(result, "frm_M0347", "btn_M0347"); warningset("M0347", "frm_M0347");
                });
                $.post("InitDefaultInfoHandler.ashx?flag=jjcls&shebei=M0473", { shebei: "" }, function (result) {

                    if (result != "") { $("div[id*='div_M0473']").html(result); }
                    setable(result, "frm_M0473", "btn_M0473"); warningset("M0473", "frm_M0473");
                });
                $.post("InitDefaultInfoHandler.ashx?flag=jjcls&shebei=M0224", { shebei: "" }, function (result) {

                    if (result != "") { $("div[id*='div_M0224']").html(result); }
                    setable(result, "frm_M0224", "btn_M0224"); warningset("M0224", "frm_M0224");
                });
                $.post("InitDefaultInfoHandler.ashx?flag=jjcls&shebei=M1297", { shebei: "" }, function (result) {

                    if (result != "") { $("div[id*='div_M1297']").html(result); }
                    setable(result, "frm_M1297", "btn_M1297"); warningset("M1297", "frm_M1297");
                });
               

            }


        })//end ready

        
        
        
        
    </script>
    <%--ѹ��  ������--%>
    <div class="row  row-container">
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
                            <li><a href="HandOverQuery.aspx?quyu=yzsys&gongwei=ѹ��ʵ����" name="ѹ��ʵ����" target="_blank">
                                ���Ӱ��ѯ</a></li>
                            <li><a href="TJ/Measure_Status_Report.aspx?quyu=yzsys&gongwei=ѹ��ʵ����" name="ѹ��ʵ����"
                                target="_blank">����ͳ�Ʊ���</a></li>
                            <li><a href="TJ/Measure_LYL_Report.aspx?quyu=yzsys&gongwei=ѹ��ʵ����" name="ѹ��ʵ����"
                                target="_blank">���������ʱ���</a></li>
                        </ul>
                    </div>
                </div>
                <div class="panel-body">
                    <div class="col-xs-12 col-sm-12  col-md-12 col-lg-12">
                        <div class="area_block">
                            <div class="btn  btn-success " name="frm_GP">
                                <div class="btn-group">
                                    <div class="btn btn-primary " data-toggle="dropdown" name="btn_GP">
                                        ����</div>
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
                                        X �� 1</div>
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
                                        X �� 2</div>
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
                                        ���������</div>
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
                                        Ӳ�ȼ�</div>
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
                                        M1091(����-�и��)</div>
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
                                        M1091(����-ĥ����)</div>
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
                                        M1088(����-�׹��)</div>
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
                                        M1089(����-��Ƕ��)</div>
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
    <%--Equator  ������--%>
    <div class="row  row-container">
        <div class="col-md-12">
            <div class="panel panel-info">
                <div class="panel-heading">
                    <strong>Equator������</strong>
                    <div class="btn-group">
                        <div class="area_drop" data-toggle="dropdown">
                            <span class="caret"></span>
                        </div>
                        <ul class="dropdown-menu" style="color: red" role="menu">
                            <li><a href="#" id="loginEQCLS" name="Eqcls">����</a></li>
                            <li><a href="#" id="logoutEQCLS" name="Equator������">�ǳ�</a></li>
                            <li class="divider"></li>
                            <li><a href="HandOverQuery.aspx?quyu=Eqcls&gongwei=Equator������" name="ѹ��ʵ����" target="_blank">
                                ���Ӱ��ѯ</a></li>
                        </ul>
                    </div>
                </div>
                <div class="panel-body">
                    <div class="col-xs-12 col-sm-12  col-md-12 col-lg-12">
                        <div class="area_block">
                            <div class="btn  btn-gray disabled" name="frm_M0978">
                                <div class="btn-group">
                                    <div class="btn btn-primary disabled " data-toggle="dropdown" name="btn_M0978">
                                        M0978(�Ա���)</div>
                                    <ul class="dropdown-menu" style="color: Black" role="menu">
                                        <li><a href="http://172.16.5.26:8060/HOME/DJ_Detail.aspx" target="_blank">�����嵥</a></li>
                                        <li class="divider"></li>
                                        <li><a href="#" target="_blank"></a></li>
                                    </ul>
                                </div>
                                <div class="area" id="div_M0978">
                                </div>
                            </div>
                        </div>
                        <div class="area_block">
                            <div class="btn  btn-gray disabled" name="frm_M0979">
                                <div class="btn-group">
                                    <div class="btn btn-primary disabled" data-toggle="dropdown" name="btn_M0979">
                                        M0979(�Ա���)</div>
                                    <ul class="dropdown-menu" style="color: Black" role="menu">
                                       <li><a href="http://172.16.5.26:8060/HOME/DJ_Detail.aspx" target="_blank">�����嵥</a></li>
                                        <li class="divider"></li>
                                        <li><a href="#"></a></li>
                                    </ul>
                                </div>
                                <div class="area" id="div_M0979">
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <%--����  ������--%>
    <div class="row  row-container">
        <div class="col-md-12">
            <div class="panel panel-info">
                <div class="panel-heading">
                    <strong>���Ӳ�����</strong>
                    <div class="btn-group">
                        <div class="area_drop" data-toggle="dropdown">
                            <span class="caret"></span>
                        </div>
                        <ul class="dropdown-menu" style="color: red" role="menu">
                            <li><a href="#" id="loginjjcls" name="jjcls">����</a></li>
                            <li><a href="#" id="logoutjjcls" name="���Ӳ�����">�ǳ�</a></li>
                            <li class="divider"></li>
                            <li><a href="HandOverQuery.aspx?quyu=jjcls&gongwei=���Ӳ�����" name="���Ӳ�����" target="_blank">
                                ���Ӱ��ѯ</a></li>
                        </ul>
                    </div>
                </div>
                <div class="panel-body">
                    <div class="col-xs-12 col-sm-12  col-md-12 col-lg-12">
                        <div class="area_block">
                            <div class="btn  btn-gray disabled" name="frm_M0407">
                                <div class="btn-group">
                                    <div class="btn btn-primary disabled" data-toggle="dropdown" name="btn_M0407">
                                        M0407(�ֲڶ���)</div>
                                    <ul class="dropdown-menu" style="color: Black" role="menu">
                                       <li><a href="http://172.16.5.26:8060/HOME/DJ_Detail.aspx" target="_blank">�����嵥</a></li>
                                        <li class="divider"></li>
                                        <li><a href="#"></a></li>
                                    </ul>
                                </div>
                                <div class="area" id="div_M0407">
                                </div>
                            </div>
                        </div>
                        <div class="area_block">
                            <div class="btn  btn-gray disabled" name="frm_M0193">
                                <div class="btn-group">
                                    <div class="btn btn-primary disabled" data-toggle="dropdown" name="btn_M0193">
                                        M0193(ͶӰ��)</div>
                                    <ul class="dropdown-menu" style="color: Black" role="menu">
                                       <li><a href="http://172.16.5.26:8060/HOME/DJ_Detail.aspx" target="_blank">�����嵥</a></li>
                                        <li class="divider"></li>
                                        <li><a href="#"></a></li>
                                    </ul>
                                </div>
                                <div class="area" id="div_M0193">
                                </div>
                            </div>
                        </div>
                        <div class="area_block">
                            <div class="btn  btn-gray disabled" name="frm_M0811">
                                <div class="btn-group">
                                    <div class="btn btn-primary disabled" data-toggle="dropdown" name="btn_M0811">
                                        M0811(�߶���)</div>
                                    <ul class="dropdown-menu" style="color: Black" role="menu">
                                        <li><a href="http://172.16.5.26:8060/HOME/DJ_Detail.aspx" target="_blank">�����嵥</a></li>
                                        <li class="divider"></li>
                                        <li><a href="#"></a></li>
                                    </ul>
                                </div>
                                <div class="area" id="div_M0811">
                                </div>
                            </div>
                        </div>
                        <div class="area_block">
                            <div class="btn  btn-gray disabled" name="frm_M0202">
                                <div class="btn-group">
                                    <div class="btn btn-primary disabled" data-toggle="dropdown" name="btn_M0202">
                                        M0202(��������)</div>
                                    <ul class="dropdown-menu" style="color: Black" role="menu">
                                        <li><a href="http://172.16.5.26:8060/HOME/DJ_Detail.aspx" target="_blank">�����嵥</a></li>
                                        <li class="divider"></li>
                                        <li><a href="#"></a></li>
                                    </ul>
                                </div>
                                <div class="area" id="div_M0202">
                                </div>
                            </div>
                        </div>
                        <div class="area_block">
                            <div class="btn  btn-gray disabled" name="frm_M0018">
                                <div class="btn-group">
                                    <div class="btn btn-primary disabled " data-toggle="dropdown" name="btn_M0018">
                                        M0018(CMM)</div>
                                    <ul class="dropdown-menu" style="color: Black" role="menu">
                                       <li><a href="http://172.16.5.26:8060/HOME/DJ_Detail.aspx" target="_blank">�����嵥</a></li>
                                        <li class="divider"></li>
                                        <li><a href="#" target="_blank"></a></li>
                                    </ul>
                                </div>
                                <div class="area" id="div_M0018">
                                </div>
                            </div>
                        </div>
                        <div class="area_block">
                            <div class="btn  btn-gray disabled" name="frm_M0524">
                                <div class="btn-group">
                                    <div class="btn btn-primary disabled" data-toggle="dropdown" name="btn_M0524">
                                        M0524(CMM)</div>
                                    <ul class="dropdown-menu" style="color: Black" role="menu">
                                        <li><a href="http://172.16.5.26:8060/HOME/DJ_Detail.aspx" target="_blank">�����嵥</a></li>
                                        <li class="divider"></li>
                                        <li><a href="#"></a></li>
                                    </ul>
                                </div>
                                <div class="area" id="div_M0524">
                                </div>
                            </div>
                        </div>
                        <div class="area_block">
                            <div class="btn  btn-gray disabled" name="frm_M0808">
                                <div class="btn-group">
                                    <div class="btn btn-primary disabled" data-toggle="dropdown" name="btn_M0808">
                                        M0808(CMM)</div>
                                    <ul class="dropdown-menu" style="color: Black" role="menu">
                                        <li><a href="http://172.16.5.26:8060/HOME/DJ_Detail.aspx" target="_blank">�����嵥</a></li>
                                        <li class="divider"></li>
                                        <li><a href="#"></a></li>
                                    </ul>
                                </div>
                                <div class="area" id="div_M0808">
                                </div>
                            </div>
                        </div>
                        <div class="area_block">
                            <div class="btn  btn-gray disabled" name="frm_M0347">
                                <div class="btn-group">
                                    <div class="btn btn-primary disabled" data-toggle="dropdown" name="btn_M0347">
                                        M0347(CMM)</div>
                                    <ul class="dropdown-menu" style="color: Black" role="menu">
                                        <li><a href="http://172.16.5.26:8060/HOME/DJ_Detail.aspx" target="_blank">�����嵥</a></li>
                                        <li class="divider"></li>
                                        <li><a href="#"></a></li>
                                    </ul>
                                </div>
                                <div class="area" id="div_M0347">
                                </div>
                            </div>
                        </div>
                        <div class="area_block">
                            <div class="btn  btn-gray disabled" name="frm_M0473">
                                <div class="btn-group">
                                    <div class="btn btn-primary disabled" data-toggle="dropdown" name="btn_M0473">
                                        M0473(CMM)</div>
                                    <ul class="dropdown-menu" style="color: Black" role="menu">
                                       <li><a href="http://172.16.5.26:8060/HOME/DJ_Detail.aspx" target="_blank">�����嵥</a></li>
                                        <li class="divider"></li>
                                        <li><a href="#"></a></li>
                                    </ul>
                                </div>
                                <div class="area" id="div_M0473">
                                </div>
                            </div>
                        </div>
                        <div class="area_block">
                            <div class="btn  btn-gray disabled" name="frm_M0224">
                                <div class="btn-group">
                                    <div class="btn btn-primary disabled" data-toggle="dropdown" name="btn_M0224">
                                        M0224(CMM)</div>
                                    <ul class="dropdown-menu" style="color: Black" role="menu">
                                       <li><a href="http://172.16.5.26:8060/HOME/DJ_Detail.aspx" target="_blank">�����嵥</a></li>
                                        <li class="divider"></li>
                                        <li><a href="#"></a></li>
                                    </ul>
                                </div>
                                <div class="area" id="div_M0224">
                                </div>
                            </div>
                        </div>

                        <div class="area_block">
                            <div class="btn  btn-gray disabled" name="frm_M1297">
                                <div class="btn-group">
                                    <div class="btn btn-primary disabled" data-toggle="dropdown" name="btn_M1297">
                                        M01297(CMM)</div>
                                    <ul class="dropdown-menu" style="color: Black" role="menu">
                                       <li><a href="http://172.16.5.26:8060/HOME/DJ_Detail.aspx" target="_blank">�����嵥</a></li>
                                        <li class="divider"></li>
                                        <li><a href="#"></a></li>
                                    </ul>
                                </div>
                                <div class="area" id="div_M1297">
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
