<%@ Page Title="MES生产管理系统" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
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
                    fix: false, //不固定
                    maxmin: false,
                    title: ['<i class="fa fa-dedent"></i> 登入', false],
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
                    fix: false, //不固定
                    maxmin: false,
                    title: ['<i class="fa fa-dedent"></i> 登出', false],
                    closeBtn: 1,
                    content: 'Logout.aspx?gongwei=' + this.name,
                    end: function () {

                    }
                });
            });
           
            show();
            ////获取各设备生产信息看板
            setInterval(show, 20000); //20s刷新一次
            function setable(result, frm, btn) {
                var tag = "无人";
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
                //销售
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
            //开关机
            $("[deviceid]").click(function (e) {             
                var Equipno = $(this).attr("deviceid");
                var Logaction = $(this).text();
                var Actionmark = "";
                var Actionreason = "";
                $.ajax({
                    type: "post", //要用post方式                 
                    url: "default.aspx/InsertEquipLogStatus", //方法所在页面和方法名SayHello
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    data: "{equipno:'" + Equipno + "',logaction:'" + Logaction + "',actionmark:'" + Actionmark + "',actionreason:'" + Actionreason + "'}",
                    // data: "{'equipno':'" + Equipno + "','logaction':'" + Logaction + "','actionmark':'" + Actionmark +"','actionreason':'" + Actionreason+"' }",
                    success: function (data) {
                        if (data.d != "") //返回的数据用data.d获取内容Logaction + "成功."
                        {
                            layer.alert(data.d);
                        }
                        else {
                            layer.alert(Logaction + "失败.");
                        }
                    },
                    error: function (err) {
                        layer.alert(err);
                    }
                });
            });

        })//end ready

        
        
        
        
    </script>
   
   
    <%--销售区域--%>
    <div class="row  row-container">
        <div class="col-md-12">
            <div class="panel panel-info">
                <div class="panel-heading">
                    <strong>销售管理</strong> 
                    <div class="btn-group">
                        <div class="area_drop" data-toggle="dropdown">
                            <span class="caret"></span>
                        </div>
                        <ul class="dropdown-menu" style="color: black" role="menu">
                            <li><span href="#" id="loginZZ" name="yz2q"   title="yz2q" style="padding-left:20px">登入</span></li>
                            <li><span href="#" id="logoutZZ" name="铸造"  title="铸造" style="padding-left:20px">登出</span></li>                            
                        </ul>
                    </div>
                </div>
                <div class="panel-body">
                    <div class="col-xs-12 col-sm-12  col-md-12 col-lg-12">
                        <div class="area_block">
                            <div class="btn btn-padding-s btn-success " name="frm_bule2-1">
                                <div class="btn-group">
                                    <div class="btn btn-primary " data-toggle="dropdown"  name="btn_bule2-1">
                                        产品管理</div>
                                    <ul class="dropdown-menu" style="color: Black" role="menu">                                        
                                        <li><a href="javascript:void(0)" target="_blank">项目启动申请单</a></li>
                                        <li><a href="javascript:void(0)"  target="_blank">产品清单</a></li>                                       
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
                                        报价管理</div>
                                    <ul class="dropdown-menu" style="color: Black" role="menu">
                                        <li><a href="javascript:void(0)" target="_blank">待定</a></li>                                        
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
                                        合同管理</div>
                                    <ul class="dropdown-menu" style="color: Black" role="menu">
                                        
                                        <li><a href="http://172.16.5.8:8080/workflow/request/AddRequest.jsp?workflowid=158&isagent=0&beagenter=0" target="_blank">销售合同评审单</a></li>
                                        <li><a href="http://172.16.5.8:8080/workflow/request/AddRequest.jsp?workflowid=170&isagent=0&beagenter=0"  target="_blank">开票收款申请单</a></li>
                                        <li class="divider"></li>
                                        <li><a href="http://172.16.5.6:8080/oa/Contract_sale_list.aspx?" target="_blank">销售合同查询</a></li>
                                        <li><a href="http://172.16.5.6:8080/oa/Contract_gathering_list.aspx" target="_blank">销售模具收款查询</a></li>
                                       <li><a href="http://172.16.5.6:8080/oa/Mjkp_TJ.aspx" target="_blank">销售模具收款余额统计</a></li>
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
                                        调价管理</div>
                                    <ul class="dropdown-menu" style="color: Black" role="menu">
                                        <li><a href="javascript:void(0)" target="_blank">待定</a></li>                                        
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
                                        产能管理</div>
                                    <ul class="dropdown-menu" style="color: Black" role="menu">
                                                                              
                                        <li><a href="javascript:void(0)" target="_blank">待定</a></li>                                        
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
                                        样件管理</div>
                                    <ul class="dropdown-menu" style="color: Black" role="menu">
                                        <li><a href="javascript:void(0)" target="_blank">待定</a></li>                                        
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
                                        销售数据</div>
                                    <ul class="dropdown-menu" style="color: Black" role="menu">
                                                                              
                                        <li><a href="/sales/Sale_DetailQuery.aspx" target="_blank">销售产出查询</a></li>
                                        <li><a href="/sales/customer_Query.aspx" target="_blank">销售收入统计</a></li>
                                        
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
