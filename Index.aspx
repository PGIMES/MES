<%@ Page Title="PGI管理系统" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="Index.aspx.cs" Inherits="Index" MaintainScrollPositionOnPostback="True" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <style>
        .head {
            font-size: 25px;
            text-align: left;
            vertical-align: bottom;
        }

        img {
            height: 47px;
            width: 150px;
        }
    </style>
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
            //$("div[id='headTitle']").text("").css("height", "15px").css("background-color", "yellow").removeClass("h3").css("padding-bottom", "5px").css("margin-left","-10px");
            //$("div[id='divImg']").html("<img src='Content/img/pgi_logo.jpg' /><font class='head'>管理平台</font>");
            // $("#mestitle").text("PGI管理平台");  
            $("div[id='divImg']").html("<img src='Content/img/pgi_logo.jpg' /><font class='head'>管理平台</font>");
            $("div[id='headTitle']").text("").removeClass("h3");
            $("div[id='divLine']").text("").css("height", "10px").css("background-color", "yellow").css("padding-bottom", "5px").css("margin-left", "-10px");

        })//end ready


    </script>
    <%-- 生产制造--%> 

    <div class="row row-container" style="padding-top: 5px">
        <div class="col-sm-12">
            <div class="panel panel-info">
                <div class="panel-heading ">
                    <strong>生产制造</strong>
                    <div class="btn-group">
                        <div class="area_drop" data-toggle="dropdown">
                        </div>
                    </div>
                </div>
                <div class="panel-body">
                    <div class="col-sm-12 ">
                        <div class="area_block">
                            <div class="btn btn-large btn-gray" name="frm_B">
                                <div class="btn-group">
                                    <div class="btn btn-primary disabled" data-toggle="" name="btn_B">
                                        一车间铁机加
                                    </div>
                                    <ul class="dropdown-menu" style="color: Black" role="menu">
                                        <li class="divider"></li>
                                    </ul>
                                </div>
                                <div class="area" id="div_B" name="B">
                                </div>
                            </div>
                        </div>
                        <div class="area_block">
                            <div class="btn btn-large btn-success" name="frm_A">
                                <a href="DefaultFe2.aspx" target="_blank" class="btn-group">
                                    <div class="btn btn-primary " data-toggle="" name="btn_A">
                                        二车间铁机加
                                    </div>
                                    <ul class="dropdown-menu" style="color: Black" role="menu">
                                        <li><a href=""></a></li>
                                        <li class="divider"></li>
                                    </ul>
                                </a>
                                <div class="area" id="div_A" name="A">
                                </div>
                            </div>
                        </div>
                        <div class="area_block">
                            <div class="btn btn-large btn-success" name="frm_A">
                                <a href="Default.aspx" target="_blank" class="btn-group">
                                    <div class="btn btn-primary " data-toggle="" name="btn_A">
                                        三车间铝压铸
                                    </div>
                                    <ul class="dropdown-menu" style="color: Black" role="menu">
                                        <li><a href=""></a></li>
                                        <li class="divider"></li>
                                    </ul>
                                </a>
                                <div class="area" id="div_A" name="A">
                                </div>
                            </div>
                        </div>
                        
                        <div class="area_block">
                            <div class="btn btn-large btn-success " name="frm_C">
                                <a href="DefaultAL.aspx" target="_blank" class="btn-group">
                                    <div class="btn btn-primary " data-toggle="" name="btn_C">
                                        四车间铝机加
                                    </div>
                                    <ul class="dropdown-menu" style="color: Black" role="menu">
                                        <li><a href=""></a></li>
                                        <li class="divider"></li>
                                    </ul>
                                </a>
                                <div class="area" id="div_C" name="C">
                                </div>
                            </div>
                        </div>
                        <div class="area_block">
                            <div class="btn btn-large btn-success" name="frm_A">
                                <a href="DefaultYZSYS.aspx" class="btn-group">
                                    <div class="btn btn-primary " data-toggle="" name="btn_sys">
                                        实验室
                                    </div>
                                    <ul class="dropdown-menu" style="color: Black" role="menu">
                                        <li><a href=""></a></li>

                                    </ul>
                                </a>
                                <div class="area" id="div1" name="A">
                                </div>
                            </div>
                        </div>
                        <div class="area_block">
                            <div class="btn btn-large btn-gray disabled" name="frm_A">
                                <a href="#" class="btn-group">
                                    <div class="btn btn-primary disabled" data-toggle="" name="btn_sys">
                                        模具维修区
                                    </div>
                                    <ul class="dropdown-menu" style="color: Black" role="menu">
                                        <li><a href=""></a></li>

                                    </ul>
                                </a>
                                <div class="area" id="div1" name="A">
                                </div>
                            </div>
                        </div>
                 

                           <div class="area_block">
                            <div class="btn btn-large   btn-success" name="frm_A">
                                <div class="btn-group">
                                    <div class="btn btn-primary " data-toggle="dropdown" name="btn_pro">
                                        生产报表
                                    </div>
                                    <ul class="dropdown-menu" style="color: Black" role="menu">
                                        <li><a href="http://172.16.5.6:8080/zhiliang/pn_fpl_list.aspx" target="_blank">废品率</a></li>
                                        <li><a href="http://172.16.5.6:8080/zhiliang/pn_fp_list.aspx" target="_blank">废品原因</a></li>
                                        <li class="divider"></li>
                                        <li><a href="http://172.16.5.6:8080/production/gd_listmx.aspx" target="_blank">工料废明细</a></li>
                                        <li><a href="http://172.16.5.6:8080/production/fgd_list.aspx" target="_blank">父工单明细</a></li>
                                        <li><a href="http://172.16.5.6:8080/production/gd_list.aspx" target="_blank">工单明细</a></li>
                                        <li><a href="http://172.16.5.6:8080/production/gx_list.aspx" target="_blank">工序明细</a></li>
                                        <li><a href="http://172.16.5.6:8080/production/m_report_qad.aspx" target="_blank">生产报表分析(QAD)</a></li>
                                         <li><a href="/production/Production_Report.aspx" target="_blank">生产报表(NEW)</a></li>
                                         <li><a href="/ProductionData/Rpt_OpQuery.aspx" target="_blank">工序明细(NEW)</a></li>
                                         <li><a href="/ProductionData/Rpt_GDQuery.aspx" target="_blank">工单明细(NEW)</a></li>
                                    </ul>
                                </div>
                                <div class="area" id="div1" name="A">
                                </div>
                            </div>
                        </div>

                    </div>
                </div>
            </div>
        </div>
    </div>
    <%-- 管理区--%>
    <div class="row row-container">
        <div class="col-sm-12">
            <div class="panel panel-info">
                <div class="panel-heading">
                    <strong>管理</strong>
                    <div class="btn-group">
                        <div class="area_drop" data-toggle="dropdown">
                        </div>
                    </div>
                </div>
                <div class="panel-body">
                    <div class="col-md-12">
                        <div class="area_block ">
                            <div class="btn btn-success" name="frm_WK">
                                <div class="btn-group">
                                    <div class="btn btn-primary" data-toggle="dropdown" name="btn_WK">
                                        文控中心
                                    </div>
                                    <ul class="dropdown-menu" style="color: Black" role="menu">
                                         
                                    </ul>
                                </div>
                                <div class="area" id="div_JL" name="div_WK">
                                </div>
                            </div>
                        </div>
                        <div class="area_block ">
                            <div class="btn btn-success" name="frm_PRB">
                                <div class="btn-group">
                                    <div class="btn btn-primary" data-toggle="dropdown" name="btn_PRB">
                                        问题解决
                                    </div>
                                    <ul class="dropdown-menu" style="color: Black" role="menu">
                                        <li><a href="/Review/Review.aspx" target="_blank">新建问题</a></li>
                                        <li><a href="/Review/Review_Query.aspx" target="_blank">问题查询</a></li>
                                        <li><a href="/Review/Review_TJ.aspx" target="_blank">问题统计</a></li>
                                        
                                    </ul>
                                </div>
                                <div class="area" id="div_PRB" name="div_PRB">
                                </div>
                            </div>
                        </div>
                        <div class="area_block ">
                            <div class="btn btn-success" name="frm_ITB">
                                <div class="btn-group">
                                    <div class="btn btn-primary" data-toggle="dropdown" name="btn_ITB">
                                        IT部
                                    </div>
                                    <ul class="dropdown-menu" style="color: Black" role="menu">
                                        <li><a href="http://172.16.5.26:8020/begin.aspx" style="display:none">IT服务系统(SMS)</a></li>
                                    </ul>
                                </div>
                                <div class="area" id="div_ITB" name="div_ITB">
                                </div>
                            </div>
                        </div>


                          <div class="area_block ">
                            <div class="btn btn-success" name="frm_Fin">
                                <div class="btn-group">
                                    <div class="btn btn-primary" data-toggle="dropdown" name="btn_Fin">
                                       财务部
                                    </div>
                                    <ul class="dropdown-menu" style="color: Black" role="menu">
                                        <%--<li><a href="/Platform/WorkFlowRun/Default.aspx?flowid=24f321ee-b4e3-4c2c-a0a4-f51cafdf526f&appid=90A30D34-F40F-4A0B-BCFD-3AD9786FF757" target="_blank">差旅申请单(测试)</a></li>
                                        <li><a href="/Platform/WorkFlowRun/Default.aspx?flowid=3f8de2dd-9229-4517-90a6-c13cb10a5c07&appid=A9EE1086-F066-41FD-A5C5-0BF50F272EB2" target="_blank">私车公用申请单(测试)</a></li>
                                        <li class="divider"></li> 
                                        <li><a href="/Forms/Finance/T_CA_Report_Query.aspx" target="_blank">差旅/私车公用申请单查询(测试)</a></li>
                                        <li><a href="/Forms/Finance/OES_Report_Query.aspx" target="_blank">费用报销单查询(测试)</a></li>--%>
                                        <li  class="dropdown-submenu" >
                                            <a href="javascript:void(0)">费用报销</a>
                                            <ul class="dropdown-menu">
                                                <li><a href="/Platform/WorkFlowRun/Default.aspx?flowid=24f321ee-b4e3-4c2c-a0a4-f51cafdf526f&appid=90A30D34-F40F-4A0B-BCFD-3AD9786FF757" target="_blank">差旅申请单(测试)</a></li>
                                                <li><a href="/Platform/WorkFlowRun/Default.aspx?flowid=3f8de2dd-9229-4517-90a6-c13cb10a5c07&appid=A9EE1086-F066-41FD-A5C5-0BF50F272EB2" target="_blank">私车公用申请单(测试)</a></li>
                                                <li><a href="/Platform/WorkFlowRun/Default.aspx?flowid=4D085987-9001-48F7-B189-FFEE43A7DA71" target="_blank">费用报销单(测试)</a></li>
                                                <li class="divider"></li> 
                                                <li><a href="/Forms/Finance/T_CA_Report_Query.aspx" target="_blank">差旅/私车公用申请单查询(测试)</a></li>
                                                <li><a href="/Forms/Finance/OES_Report_Query.aspx" target="_blank">费用报销单查询(测试)</a></li>                                        
                                            </ul>
                                        </li>
                                        <li class="divider"></li> 
                                        <li><a href="/Fin/Fin_WG1_Report.aspx">完工报表一</a></li>
                                        <li><a href="/Fin/Fin_WG2_Report.aspx">完工报表二</a></li>
                                    </ul>
                                </div>
                                <div class="area" id="div_Fin" name="div_Fin">
                                </div>
                            </div>
                        </div>

                         <div class="area_block ">
                            <div class="btn btn-success" name="frm_HR">
                                <div class="btn-group">
                                    <div class="btn btn-primary" data-toggle="dropdown" name="btn_HR">
                                       人事部
                                    </div>
                                    <ul class="dropdown-menu" style="color: Black" role="menu">
                                      <li><a href="javascript:void(0)" target="_blank">待定</a></li>   
                                    </ul>
                                </div>
                                <div class="area" id="div_HR" name="div_HR">
                                </div>
                            </div>
                        </div>


                    </div>
                </div>
            </div>
        </div>
    </div>
    <%--销售区域--%>
    <div class="row  row-container">
        <div class="col-md-12">
            <div class="panel panel-info">
                <div class="panel-heading">
                    <strong>销售管理</strong> 
                   <%-- <div class="btn-group">
                        <div class="area_drop" data-toggle="dropdown">
                            <span class="caret"></span>
                        </div>
                        <ul class="dropdown-menu" style="color: black" role="menu">
                            <li><span href="#" id="loginsale" name="sale"   title="sale" style="padding-left:20px">登入</span></li>
                            <li><span href="#" id="logoutsale" name="销售"  title="销售" style="padding-left:20px">登出</span></li>                            
                        </ul>
                    </div>--%>
                </div>
                <div class="panel-body">
                    <div class="col-xs-12 col-sm-12  col-md-12 col-lg-12">
                        <div class="area_block">
                            <div class="btn btn-padding-s btn-success " name="frm_BJ">
                                <div class="btn-group">
                                    <div class="btn btn-primary " data-toggle="dropdown"  name="btn_BJ">
                                        报价管理</div>
                                    <ul class="dropdown-menu" style="color: Black" role="menu">
                                        <li><a href="baojia/baojia.aspx" target="_blank" style="display: inline">新建报价</a></li> 
                                        <li><a href="baojia/Baojia_Query.aspx" target="_blank" style="display: inline">报价查询</a>|<a href="Baojia/Baojia_history_UpdateBaojia.aspx" target="_blank" style="display: inline">更新</a></li>                                       
                                        <li class="divider"></li> 
                                        <li><a href="baojia/Baojia_Progress_Query.aspx" target="_blank">报价进度跟踪</a></li>
                                        
                                        <li><a href="baojia/Baojia_Task_Query.aspx" target="_blank">报价任务查询</a></li>
                                        <li class="divider"></li> 
                                        <li><a href="baojia/baojia_fenxi_tj.aspx" target="_blank">报价统计</a></li>
                                        
                                        <li><a href="baojia/baojia_zhongdianzhengqu_tj.aspx" target="_blank">重点争取项目分析</a></li>
                                        <li><a href="baojia/baojia_dingdian_tj.aspx" target="_blank">定点项目分析</a></li>
                                    </ul>
                                </div>
                                <div class="area" id="div_BJ">
                                </div>
                            </div>
                        </div>
                        <div class="area_block">
                            <div class="btn btn-padding-s btn-success " name="frm_HT">
                                <div class="btn-group">
                                    <div class="btn btn-primary " data-toggle="dropdown"  name="btn_HT">
                                        合同管理</div>
                                    <ul class="dropdown-menu" style="color: Black" role="menu">                                        
                                        <li><a href="http://172.16.5.8:8080/workflow/request/AddRequest.jsp?workflowid=158&isagent=0&beagenter=0" target="_blank">新建合同</a></li>
                                        <li><a href="http://172.16.5.6:8080/oa/Contract_sale_list.aspx?" target="_blank">合同查询</a></li>                                        
                                        <li class="divider"></li>
                                        <li><a href="http://172.16.5.8:8080/workflow/request/AddRequest.jsp?workflowid=170&isagent=0&beagenter=0"  target="_blank">新建模具收款</a></li>
                                        <li><a href="http://172.16.5.6:8080/oa/Contract_gathering_list.aspx" target="_blank">模具收款查询</a></li>
                                        <li><a href="http://172.16.5.6:8080/oa/Mjkp_TJ.aspx" target="_blank">模具收款余额统计</a></li>
                                    </ul>
                                </div>
                                <div class="area" id="div_HT">
                                </div>
                            </div>
                        </div>
                        <div class="area_block">
                            <div class="btn btn-padding-s btn-success " name="frm_KH">
                                <div class="btn-group">
                                    <div class="btn btn-primary " data-toggle="dropdown"  name="btn_KH">
                                        客户管理</div>
                                    <ul class="dropdown-menu" style="color: Black" role="menu">
                                        <li><a href="sales/customer_QAD.aspx" target="_blank">新建客户</a></li>                                        
                                        <li class="divider"></li> 
                                        <li><a href="sales/Customer_report.aspx" target="_blank" style="display: inline">客户查询</a>|<a href="sales/Customer_Update_list.aspx" target="_blank" style="display: inline">更新</a></li>
                                        <li class="divider"></li> 
                                        <li><a href="sales/Customer_Update_work.aspx" target="_blank">客户任务查询</a></li>
                                    </ul>
                                </div>
                                <div class="area" id="div_KH">
                                </div>
                            </div>
                        </div>
                        <div class="area_block">
                            <div class="btn btn-padding-s btn-success " name="frm_CP">
                                <div class="btn-group">
                                    <div class="btn btn-primary " data-toggle="dropdown"  name="btn_CP">
                                        产品管理</div>
                                    <ul class="dropdown-menu" style="color: Black" role="menu">                                        
                                        <li><a href="product/product.aspx"  target="_blank">新建产品项目号</a></li>
                                        <li><a href="product/chanpin_query.aspx"  target="_blank">产品查询</a></li> 
                                        <li class="divider"></li> 
                                        <li><a href="product/Chanpin_Fenxi_ByCPFZR.aspx"  target="_blank">产品统计</a></li> 
                                       <%-- <li><a href="product/Chanpin_Fenxi_ByCPFZR.aspx"  target="_blank">产品负责人</a></li> --%>
                                                                         
                                    </ul>

                                </div>
                                <div class="area" id="div_CP">
                                </div>
                            </div>
                        </div>

                        <div class="area_block">
                            <div class="btn btn-padding-s btn-success " name="frm_TJ">
                                <div class="btn-group">
                                    <div class="btn btn-primary " data-toggle="dropdown"  name="btn_TJ">
                                        调价管理</div>
                                    <ul class="dropdown-menu" style="color: Black" role="menu">
                                        <li><a href="javascript:void(0)" target="_blank">待定</a></li>                                        
                                        <li class="divider"></li> 
                                    </ul>
                                </div>
                                <div class="area" id="div_TJ">
                                </div>
                            </div>
                        </div>

                        <div class="area_block">
                            <div class="btn btn-padding-s btn-success " name="frm_YJ" >
                                <div class="btn-group">
                                    <div class="btn btn-primary " data-toggle="dropdown"  name="btn_YJ">
                                        样件管理</div>
                                    <ul class="dropdown-menu" style="color: Black" role="menu">
                                        <li><a href="/yangjian/Yangjian.aspx" target="_blank">样件订单维护</a></li>                                        
                                        <li><a href="/dingchang/DC_Apply.aspx" target="_blank">订舱</a></li> 
                                        <li class="divider"></li>    
                                        <li><a href="/yangjian/YJ_Query_Report.aspx" target="_blank">样件报表查询</a></li>   
                                        <li><a href="/yangjian/YJ_Tracking_Process_Report.aspx" target="_blank">样件任务查询</a></li>
                                        <li><a href="/dingchang/Dingchang_Progress_Query.aspx" target="_blank">订舱任务查询</a></li> 
                                        <li><a href="/yangjian/YangJianDelay_TJ.aspx" target="_blank">样件订单发货统计</a></li>
                                        <li><a href="/yangjian/YangJianFinishRate_TJ.aspx" target="_blank" style="display:none">完成率统计报表</a></li>
                                    </ul>
                                </div>
                                <div class="area" id="div_YJ">
                                </div>
                            </div>
                        </div>
                        <div class="area_block">
                            <div class="btn btn-padding-s btn-success " name="frm_XS">
                                <div class="btn-group">
                                    <div class="btn btn-primary " data-toggle="dropdown"  name="btn_XS">
                                        销售数据</div>
                                    <ul class="dropdown-menu" style="color: Black" role="menu">
                                                                              
                                        <li><a href="/sales/Sale_DetailQuery.aspx" target="_blank">产品销售查询</a></li>
                                        <li><a href="/sales/customer_Query.aspx" target="_blank">销售统计</a></li>
                                        <li class="divider"></li> 
                                        <li><a href="product/chanpin_saleforecast.aspx"  target="_blank">年销售预测</a></li> 
                                        <li><a href="product/Chanpin_ForcastByMonth.aspx"  target="_blank">月销售预测</a></li>   

                                    </ul>
                                </div>
                                <div class="area" id="div_XS">
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
     <%--工程区域--%>
    <div class="row  row-container">
        <div class="col-md-12">
            <div class="panel panel-info">
                <div class="panel-heading">
                    <strong>工程部</strong> 
                   <%-- <div class="btn-group">
                        <div class="area_drop" data-toggle="dropdown">
                            <span class="caret"></span>
                        </div>
                        <ul class="dropdown-menu" style="color: black" role="menu">
                            <li><span href="#" id="loginsale" name="sale"   title="sale" style="padding-left:20px">登入</span></li>
                            <li><span href="#" id="logoutsale" name="销售"  title="销售" style="padding-left:20px">登出</span></li>                            
                        </ul>
                    </div>--%>
                </div>
                <div class="panel-body">
                    <div class="col-xs-12 col-sm-12  col-md-12 col-lg-12">
                        <div class="area_block">
                            <div class="btn btn-padding-s btn-success " name="frm_GC">
                                <div class="btn-group">
                                    <div class="btn btn-primary " data-toggle="dropdown"  name="btn_GC">
                                        工程</div>
                                    <ul class="dropdown-menu" style="color: Black" role="menu"> 
                                        <li><a href="/gongcheng/Product_GroupMatain.aspx"  target="_blank">产品组分类维护</a></li>                                                                               
                                        <li><a href="/gongcheng/DJ_JZ.aspx" target="_blank">刀具结转</a></li>
                                        <li class="divider"></li>                                        
                                        <li><a href="http://172.16.5.6:8060/Report/DJList_Query.aspx"  target="_blank">刀具清单查询</a></li>
                                        <li><a href="/gongcheng/DJLY_Detail.aspx"  target="_blank">刀具领用明细查询</a></li>     
                                        <li><a href="/gongcheng/DJ_analyse_bydj.aspx"  target="_blank">刀具分析报表(刀具)</a></li>     
                                        <li><a href="/gongcheng/DJ_analyse_byproduct.aspx"  target="_blank">刀具分析报表(产品)</a></li>  
                                                                        
                                    </ul>
                                </div>
                                <div class="area" id="div_GC">
                                </div>
                            </div>
                        </div>                        
                        <div class="area_block">
                            <div class="btn btn-padding-s btn-success " name="frm_DJ">
                                <div class="btn-group">
                                    <div class="btn btn-primary " data-toggle="dropdown"  name="btn_DJ">
                                        刀具管理</div>                                    
                                    <ul class="dropdown-menu" style="color: Black" role="menu"> 
                                        <li><a href="/Platform/WorkFlowRun/Default.aspx?flowid=f794a8ea-df18-421e-b394-9f0f709b027f&appid=3ee6c6db-d798-4416-b817-2cc95f11aa6f"  target="_blank">刀具物料申请</a></li>
                                        <li><a href="/Platform/WorkFlowRun/Default.aspx?flowid=c53677dd-f0c7-4b21-9d6c-04c3034c5f18&appid=fe887b93-b243-4a39-970b-8172a7f6ad12"  target="_blank">产品刀具清单申请</a></li>
                                        <li class="divider"></li>
                                        <li><a href="/MaterialBase/SZ_report_dev.aspx"  target="_blank">刀具查询 </a></li>  
                                        <li><a href="/MaterialBase/DJ_LY_Query.aspx"  target="_blank">刀具领用查询 </a></li>  
                                         <li><a href="/Forms/MaterialBase/Daoju_List.aspx"  target="_blank">产品刀具清单查询 </a></li> 
                                         <li><a href="/MaterialBase/Product_Dj_CBQuery.aspx"  target="_blank">产品刀具成本 </a></li>                                    
                                    </ul>
                                </div>
                                <div class="area" id="div_DJ">
                                </div>
                            </div>
                        </div> 
                        <div class="area_block">
                            <div class="btn btn-padding-s btn-success " name="frm_GC">
                                <div class="btn-group">
                                    <div class="btn btn-primary " data-toggle="dropdown"  name="btn_GC">
                                        工艺数据</div>
                                    <ul class="dropdown-menu" style="color: Black" role="menu">
                                        <li><a href="/Platform/WorkFlowRun/Default.aspx?flowid=d9cb9476-13f9-48ec-a87e-5b84ca0790b0&appid=" target="_blank">生产性物料申请</a></li>
										<li><a href="/Platform/WorkFlowRun/Default.aspx?flowid=ee59e0b3-d6a1-4a30-a3b4-65d188323134&appid=BDDCD717-2DD6-4D83-828C-71C92FFF6AE4" target="_blank">工艺路线申请</a></li>
                                        <li><a href="/Platform/WorkFlowRun/Default.aspx?flowid=4A901BC7-EA83-43B1-80B6-5B14708DEDE9&appid=" target="_blank">BOM申请</a></li>
                                        <li class="divider"></li>
                                        <li><a href="/Forms/MaterialBase/WuLiao_Report_Query.aspx" target="_blank">生产性物料查询</a></li>
										<li><a href="/Forms/PgiOp/GYLX_Report_Query.aspx" target="_blank">工艺路线查询</a></li>
                                        <li><a href="/Forms/Bom/bom_query.aspx" target="_blank">BOM查询</a></li>
                                        <li><a href="/Forms/PgiOp/Rpt_GYBom_Query.aspx" target="_blank">物料.工艺.BOM流程状态查询</a></li>
                                    </ul>
                                </div>
                                <div class="area" id="div_GC">
                                </div>
                            </div>
                        </div>                       
                    </div>
                </div>
            </div>
        </div>
    </div>
    <%--项目区域--%>
    <div class="row  row-container">
        <div class="col-md-12">
            <div class="panel panel-info">
                <div class="panel-heading">
                    <strong>项目部</strong> 
                </div>
                <div class="panel-body">
                    <div class="col-xs-12 col-sm-12  col-md-12 col-lg-12">
                        <div class="area_block">
                            <div class="btn btn-padding-s btn-success " name="frm_bule2-1">
                                <div class="btn-group">
                                    <div class="btn btn-primary " data-toggle="dropdown"  name="btn_bule2-1">
                                        APQP</div>

                                    <ul class="dropdown-menu" style="color: Black" role="menu">                                        
                                        <li><a href="http://172.16.5.6:8080/source1/track_list.aspx" target="_blank">项目进度跟踪</a></li>
                                        <li><a href="/XM/APQP_TJ_Report.aspx"  target="_blank">项目任务统计</a></li>
                                        <li><a href="/XM/APQP_Rate_TJ.aspx"  target="_blank">项目任务完成率统计</a></li>   
                                         <li><a href="/XM/XM_TJ_Report.aspx"  target="_blank">项目统计</a></li>                                 
                                    </ul>
                                </div>
                                <div class="area" id="div_bule2-1">
                                </div>
                            </div>
                        </div> 
                        <div class="area_block">
                            <div class="btn btn-padding-s btn-success " name="frm_bule2-1">
                                <div class="btn-group">
                                    <div class="btn btn-primary " data-toggle="dropdown"  name="btn_bule2-1">
                                        排产计划</div>
                                    <ul class="dropdown-menu" style="color: Black" role="menu">                                        
                                        <li><a href="javascript:void(0)" target="_blank">待完善....</a></li>
                                        
                                    </ul>
                                </div>
                                <div class="area" id="div_bule2-1">
                                </div>
                            </div>
                       </div>
                       <div class="area_block">
                            <div class="btn btn-padding-s btn-success " name="frm_bule2-1">
                                <div class="btn-group">
                                    <div class="btn btn-primary " data-toggle="dropdown"  name="btn_bule2-1">
                                        样件交付</div>
                                    <ul class="dropdown-menu" style="color: Black" role="menu">                                        
                                        <li><a href="javascript:void(0)" target="_blank">待完善....</a></li>
                                
                                    </ul>
                                </div>
                                <div class="area" id="div_bule2-1">
                                </div>
                            </div>
                       </div>
                       <div class="area_block">
                            <div class="btn btn-padding-s btn-success " name="frm_bule2-1">
                                <div class="btn-group">
                                    <div class="btn btn-primary " data-toggle="dropdown"  name="btn_bule2-1">
                                        项目移交</div>
                                    <ul class="dropdown-menu" style="color: Black" role="menu">                                        
                                        <li><a href="javascript:void(0)" target="_blank">待完善...</a></li>
                                
                                    </ul>
                                </div>
                                <div class="area" id="div_bule2-1">
                                </div>
                            </div>
                       </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
   <%--物流部区域--%>
    <div class="row  row-container">
        <div class="col-md-12">
            <div class="panel panel-info">
                <div class="panel-heading">
                    <strong>物流部</strong>                   
                </div>
                <div class="panel-body">
                    <div class="col-xs-12 col-sm-12  col-md-12 col-lg-12">
                        <div class="area_block">
                            <div class="btn btn-padding-s btn-success " name="frm_bule2-1">
                                <div class="btn-group">
                                    <div class="btn btn-primary " data-toggle="dropdown"  name="btn_bule2-1">
                                        计划</div>
                                    <ul class="dropdown-menu" style="color: Black" role="menu">   
                                        <li><a href="javascript:void(0)"  target="_blank">工单(TBD)</a></li>
                                        <li><a href="javascript:void(0)"  target="_blank">库存(TBD)</a></li>                                  
                                        <li><a href="/wuliu/SemiFinishedKanban.aspx" target="_blank">半成品数据看板(OnGoing)</a></li>
                                         <li><a href="/wuliu/FH_OverMnth.aspx"  target="_blank">超一个月无交货报表</a></li>
                                         <li><a href="/wuliu/UnFH_Remind.aspx"  target="_blank">漏发货提醒</a></li> 
                                         <li><a href="/Product/ForcastByMnth_Forwuliu.aspx"  target="_blank">三个月滚动预测</a></li>   
                                         <li><a href="/kanban/Qad_WorkOrder_List.aspx"  target="_blank">工单流转状态及父工单看板</a></li>  
                                         <li><a href="/wuliu/Qad_Report_tr_hist_Sum.aspx"  target="_blank">库龄汇总报表</a></li>
                                         <li><a href="/wuliu/Qad_Report_tr_hist.aspx"  target="_blank">库龄明细报表</a></li>
                                         <li><a href="/Wuliu/Edi_BMW_Query.aspx"  target="_blank">BMW Edi对比</a></li>   
                                        <li><a href="javascript:void(0)"  target="_blank"></a></li>                                       
                                    </ul>
                                </div>
                                <div class="area" id="div_bule2-1">
                                </div>
                            </div>
                        </div>                       
                        <div class="area_block">
                            <div class="btn btn-padding-s btn-success " name="frm_bule2-1">
                                <div class="btn-group">
                                    <div class="btn btn-primary " data-toggle="dropdown"  name="btn_bule2-1">
                                        物流</div>
                                    <ul class="dropdown-menu" style="color: Black" role="menu">                                       
                                         <li><a href="http://172.16.5.6:8080/wms/shipment_list.aspx"  target="_blank">出货跟踪查询</a></li>  
                                         <li><a href="http://172.16.5.6:8080/wms/shipment_report.aspx"  target="_blank">出货量统计报表</a></li> 
                                           <li class="divider"></li>
                                        <li><a href="/wuliu/wuliu_container_rate_report.aspx"  target="_blank">集装箱利用率</a></li> 
                                         <li><a href="/wuliu/FH_Query.aspx"  target="_blank">发运记录查询</a></li>  
                                                                               
                                    </ul>
                                </div>
                                <div class="area" id="div_bule2-1">
                                </div>
                            </div>
                        </div> 
                        <div class="area_block">
                            <div class="btn btn-padding-s btn-success " name="frm_bule2-1">
                                <div class="btn-group">
                                    <div class="btn btn-primary " data-toggle="dropdown"  name="btn_bule2-1">
                                        仓库</div>
                                    <ul class="dropdown-menu" style="color: Black" role="menu">                                        
                                        <li><a href="http://172.16.5.6:8080/dj/Sftnum_Query.aspx" target="_blank">安全库存报表</a></li>
                                        <li><a href="http://172.16.5.6:8080/dj/Sftnum_Unapply.aspx"  target="_blank">有安全库存未请购报表</a></li> 
                                         <li><a href="http://172.16.5.6:8080/dj/Jiance_Data.aspx"  target="_blank">监测物流流通时间报表</a></li>                                       
                                    </ul>
                                </div>
                                <div class="area" id="div_bule2-1">
                                </div>
                            </div>
                        </div> 
                       <div class="area_block">
                            <div class="btn btn-padding-s btn-success " name="frm_bule2-1">
                                <div class="btn-group">
                                    <div class="btn btn-primary " data-toggle="dropdown"  name="btn_bule2-1">
                                        包装</div>
                                    <ul class="dropdown-menu" style="color: Black" role="menu">                                        
                                        <li><a href="javascript:void(0)" target="_blank">成本（TBD）</a></li>
                                        <li><a href="javascript:void(0)"  target="_blank">任务清单(TBD)</a></li>
                                        <li><a href="http://172.16.5.8:8080/workflow/request/AddRequest.jsp?workflowid=149" target="_blank">包装方案申请单</a></li>
                                        <li><a href="http://172.16.5.6:8080/oa/BZFASQ_LIST.aspx" target="_blank">包装方案报表</a></li>                                  
                                    </ul>
                                </div>
                                <div class="area" id="div_bule2-1">
                                </div>
                            </div>
                        </div> 
                    </div>
                </div>
            </div>
        </div>
    </div>
        <%--质量部--%>
        <div class="row  row-container">
        <div class="col-md-12">
            <div class="panel panel-info">
                <div class="panel-heading">
                    <strong>质量部</strong>                   
                </div>
                <div class="panel-body">
                    <div class="col-xs-12 col-sm-12  col-md-12 col-lg-12">
                        <div class="area_block">
                            <div class="btn btn-padding-s btn-success " name="frm_ShenHe">
                                <div class="btn-group">
                                    <div class="btn btn-primary " data-toggle="dropdown"  name="btn_ShenHe">
                                        审核</div>
                                    <ul class="dropdown-menu" style="color: Black" role="menu">                                        
                                        <li><a href="javascript:void(0)" target="_blank">审核计划制定</a></li>
                                        <li><a href="javascript:void(0)" target="_blank">审核计划查看</a></li>   
                                        <li><a href="javascript:void(0)" target="_blank">审核内容制定</a></li> 
                                        <li><a href="javascript:void(0)" target="_blank">审核统计</a></li>                                     
                                    </ul>
                                </div>
                                <div class="area" id="div_ShenHe">
                                </div>
                            </div>
                        </div>                        
                        
                        
                       
                    </div>
                </div>
            </div>
        </div>
    </div>
    <%--采购部--%>
        <div class="row  row-container">
        <div class="col-md-12">
            <div class="panel panel-info">
                <div class="panel-heading">
                    <strong>采购部</strong>                   
                </div>
                <div class="panel-body">
                    <div class="col-xs-12 col-sm-12  col-md-12 col-lg-12">
                        <div class="area_block">
                            <div class="btn btn-padding-s btn-success " name="frm_bule2-1">
                                <div class="btn-group">
                                    <div class="btn btn-primary " data-toggle="dropdown"  name="btn_bule2-1">
                                        采购管理</div>
                                    <ul class="dropdown-menu" style="color: Black" role="menu">

                                        <li><a href="/Platform/WorkFlowRun/Default.aspx?flowid=ea7e5f10-96e5-432c-9dd5-5ecc16d5eb92&appid=7d6cf334-0227-4fcd-9faf-c2536d10cf8e" target="_blank">请购申请单(PR)</a></li>
                                        <li><a href="/Platform/WorkFlowRun/Default.aspx?flowid=ce701853-e13b-4c39-9cd6-b97e18656d31&appid=a4d66e5b-0456-47ce-b9aa-ef783f504583" target="_blank">采购申请单(PO)</a></li>
                                        <li class="divider"></li>
                                        <li><a href="/Forms/PurChase/PR_Report_Query.aspx" target="_blank">请购单查询 </a></li>
                                        <li><a href="/Forms/PurChase/PO_Report_Query.aspx" target="_blank">采购单查询 </a></li>
                                    </ul>
                                </div>
                                <div class="area" id="div_bule2-1">
                                </div>
                            </div>
                        </div>                        
                        
                        
                       
                    </div>
                </div>
            </div>
        </div>
    </div>
    <%-- 暂不用隐藏区域=============================================================================================================================================================--%>
    <div style="display: none">
      


    </div>
</asp:Content>
