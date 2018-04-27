<%@ Page Title="MES��������ϵͳ" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="DefaultSale.aspx.cs" Inherits="DefaultSale" MaintainScrollPositionOnPostback="True" %>
  
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
        .dropdown-menu>li>a:focus, .dropdown-menu>li>a:hover {
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
           
            show();
            ////��ȡ���豸������Ϣ����
            setInterval(show, 20000); //20sˢ��һ��
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

                    
                    
             
                });



            }
            function show() {                 
                //����
                //$.post("InitDefaultInfoHandler.ashx?flag=BULE&shebei=2-1", { shebei: "" }, function (result) {
                //    if (result != "") { $("div[id*='div_bule2-1']").html(result); }
                //    setable(result, "frm_bule2-1", "btn_bule2-1"); warningset("2-1", "frm_bule2-1");
                //});
                //$.post("InitDefaultInfoHandler.ashx?flag=BULE&shebei=2-2", { shebei: "" }, function (result) {
                //    if (result != "") { $("div[id*='div_bule2-2']").html(result); }
                //    setable(result, "frm_bule2-2", "btn_bule2-2"); warningset("2-2", "frm_bule2-2");
                //});
                //$.post("InitDefaultInfoHandler.ashx?flag=BULE&shebei=2-3", { shebei: "" }, function (result) {
                //    if (result != "") { $("div[id*='div_bule2-3']").html(result); }
                //    setable(result, "frm_bule2-3", "btn_bule2-3"); warningset("2-3", "frm_bule2-3");
                //});         
               
               
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
   
   
    <%--��������--%>
    <div class="row  row-container">
        <div class="col-md-12">
            <div class="panel panel-info">
                <div class="panel-heading">
                    <strong>���۹���</strong> 
                    <div class="btn-group">
                        <div class="area_drop" data-toggle="dropdown">
                            <span class="caret"></span>
                        </div>
                        <ul class="dropdown-menu" style="color: black" role="menu">
                            <li><span href="#" id="loginZZ" name="yz2q"   title="yz2q" style="padding-left:20px">����</span></li>
                            <li><span href="#" id="logoutZZ" name="����"  title="����" style="padding-left:20px">�ǳ�</span></li>                            
                        </ul>
                    </div>
                </div>
                <div class="panel-body">
                    <div class="col-xs-12 col-sm-12  col-md-12 col-lg-12">
                        <div class="area_block">
                            <div class="btn btn-padding-s btn-success " name="frm_bule2-1">
                                <div class="btn-group">
                                    <div class="btn btn-primary " data-toggle="dropdown"  name="btn_bule2-1">
                                        ��Ʒ����</div>
                                    <ul class="dropdown-menu" style="color: Black" role="menu">                                        
                                        <li><a href="javascript:void(0)" target="_blank">��Ŀ�������뵥</a></li>
                                        <li><a href="javascript:void(0)"  target="_blank">��Ʒ�嵥</a></li>                                       
                                    </ul>
                                </div>
                                <div class="area" id="div_bule2-1">
                                </div>
                            </div>
                        </div>
                        <div class="area_block">
                            <div class="btn btn-padding-s btn-success " name="frm_bule2-2">
                                <div class="btn-group">
                                    <div class="btn btn-primary " data-toggle="dropdown"  name="btn_bule2-2">
                                        ���۹���</div>
                                    <ul class="dropdown-menu" style="color: Black" role="menu">
                                        <li><a href="javascript:void(0)" target="_blank">����</a></li>                                        
                                        <li class="divider"></li> 
                                    </ul>
                                </div>
                                <div class="area" id="div_bule2-2">
                                </div>
                            </div>
                        </div>
                        <div class="area_block">
                            <div class="btn btn-padding-s btn-success " name="frm_bule2-3">
                                <div class="btn-group">
                                    <div class="btn btn-primary " data-toggle="dropdown"  name="btn_bule2-3">
                                        ��ͬ����</div>
                                    <ul class="dropdown-menu" style="color: Black" role="menu">
                                        
                                        <li><a href="http://172.16.5.8:8080/workflow/request/AddRequest.jsp?workflowid=158&isagent=0&beagenter=0" target="_blank">���ۺ�ͬ����</a></li>
                                        <li><a href="http://172.16.5.8:8080/workflow/request/AddRequest.jsp?workflowid=170&isagent=0&beagenter=0"  target="_blank">��Ʊ�տ����뵥</a></li>
                                        <li class="divider"></li>
                                        <li><a href="http://172.16.5.6:8080/oa/Contract_sale_list.aspx?" target="_blank">���ۺ�ͬ��ѯ</a></li>
                                        <li><a href="http://172.16.5.6:8080/oa/Contract_gathering_list.aspx" target="_blank">����ģ���տ��ѯ</a></li>
                                       <li><a href="http://172.16.5.6:8080/oa/Mjkp_TJ.aspx" target="_blank">����ģ���տ����ͳ��</a></li>
                                    </ul>
                                </div>
                                <div class="area" id="div_bule2-3">
                                </div>
                            </div>
                        </div>
                        <div class="area_block">
                            <div class="btn btn-padding-s btn-success " name="frm_bule2-4">
                                <div class="btn-group">
                                    <div class="btn btn-primary " data-toggle="dropdown"  name="btn_bule2-4">
                                        ���۹���</div>
                                    <ul class="dropdown-menu" style="color: Black" role="menu">
                                        <li><a href="javascript:void(0)" target="_blank">����</a></li>                                        
                                        <li class="divider"></li> 
                                    </ul>
                                </div>
                                <div class="area" id="div_bule2-4">
                                </div>
                            </div>
                        </div>
                        <div class="area_block">
                            <div class="btn btn-padding-s btn-success " name="frm_bule2-5">
                                <div class="btn-group">
                                    <div class="btn btn-primary " data-toggle="dropdown"  name="btn_bule2-5">
                                        ���ܹ���</div>
                                    <ul class="dropdown-menu" style="color: Black" role="menu">
                                                                              
                                        <li><a href="javascript:void(0)" target="_blank">����</a></li>                                        
                                        <li class="divider"></li>                                       
                                       
                                    </ul>
                                </div>
                                <div class="area" id="div_bule2-5">
                                </div>
                            </div>
                        </div>
                        <div class="area_block">
                            <div class="btn btn-padding-s btn-success " name="frm_bule2-5">
                                <div class="btn-group">
                                    <div class="btn btn-primary " data-toggle="dropdown"  name="btn_bule2-5">
                                        ��������</div>
                                    <ul class="dropdown-menu" style="color: Black" role="menu">
                                        <li><a href="javascript:void(0)" target="_blank">����</a></li>                                        
                                        <li class="divider"></li>        
                                    </ul>
                                </div>
                                <div class="area" id="div_bule2-5">
                                </div>
                            </div>
                        </div>
                        <div class="area_block">
                            <div class="btn btn-padding-s btn-success " name="frm_bule2-5">
                                <div class="btn-group">
                                    <div class="btn btn-primary " data-toggle="dropdown"  name="btn_bule2-5">
                                        ��������</div>
                                    <ul class="dropdown-menu" style="color: Black" role="menu">
                                                                              
                                        <li><a href="/sales/Sale_DetailQuery.aspx" target="_blank">���۲�����ѯ</a></li>
                                        <li><a href="/sales/customer_Query.aspx" target="_blank">��������ͳ��</a></li>
                                        
                                    </ul>
                                </div>
                                <div class="area" id="div_bule2-5">
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
