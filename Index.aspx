<%@ Page Title="PGI����ϵͳ" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
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
            //$("div[id='divImg']").html("<img src='Content/img/pgi_logo.jpg' /><font class='head'>����ƽ̨</font>");
            // $("#mestitle").text("PGI����ƽ̨");  
            $("div[id='divImg']").html("<img src='Content/img/pgi_logo.jpg' /><font class='head'>����ƽ̨</font>");
            $("div[id='headTitle']").text("").removeClass("h3");
            $("div[id='divLine']").text("").css("height", "10px").css("background-color", "yellow").css("padding-bottom", "5px").css("margin-left", "-10px");

        })//end ready


    </script>
    <%-- ��������--%> 

    <div class="row row-container" style="padding-top: 5px">
        <div class="col-sm-12">
            <div class="panel panel-info">
                <div class="panel-heading ">
                    <strong>��������</strong>
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
                                        һ����������
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
                                        ������������
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
                                        ��������ѹ��
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
                                        �ĳ���������
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
                                        ʵ����
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
                                        ģ��ά����
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
                                        ��������
                                    </div>
                                    <ul class="dropdown-menu" style="color: Black" role="menu">
                                        <li><a href="http://172.16.5.6:8080/zhiliang/pn_fpl_list.aspx" target="_blank">��Ʒ��</a></li>
                                        <li><a href="http://172.16.5.6:8080/zhiliang/pn_fp_list.aspx" target="_blank">��Ʒԭ��</a></li>
                                        <li class="divider"></li>
                                        <li><a href="http://172.16.5.6:8080/production/gd_listmx.aspx" target="_blank">���Ϸ���ϸ</a></li>
                                        <li><a href="http://172.16.5.6:8080/production/fgd_list.aspx" target="_blank">��������ϸ</a></li>
                                        <li><a href="http://172.16.5.6:8080/production/gd_list.aspx" target="_blank">������ϸ</a></li>
                                        <li><a href="http://172.16.5.6:8080/production/gx_list.aspx" target="_blank">������ϸ</a></li>
                                        <li><a href="http://172.16.5.6:8080/production/m_report_qad.aspx" target="_blank">�����������(QAD)</a></li>
                                         <li><a href="/production/Production_Report.aspx" target="_blank">��������(NEW)</a></li>
                                         <li><a href="/ProductionData/Rpt_OpQuery.aspx" target="_blank">������ϸ(NEW)</a></li>
                                         <li><a href="/ProductionData/Rpt_GDQuery.aspx" target="_blank">������ϸ(NEW)</a></li>
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
    <%-- ������--%>
    <div class="row row-container">
        <div class="col-sm-12">
            <div class="panel panel-info">
                <div class="panel-heading">
                    <strong>����</strong>
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
                                        �Ŀ�����
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
                                        ������
                                    </div>
                                    <ul class="dropdown-menu" style="color: Black" role="menu">
                                        <li><a href="/Review/Review.aspx" target="_blank">�½�����</a></li>
                                        <li><a href="/Review/Review_Query.aspx" target="_blank">�����ѯ</a></li>
                                        <li><a href="/Review/Review_TJ.aspx" target="_blank">����ͳ��</a></li>
                                        
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
                                        IT��
                                    </div>
                                    <ul class="dropdown-menu" style="color: Black" role="menu">
                                        <li><a href="http://172.16.5.26:8020/begin.aspx" style="display:none">IT����ϵͳ(SMS)</a></li>
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
                                       ����
                                    </div>
                                    <ul class="dropdown-menu" style="color: Black" role="menu">
                                        <%--<li><a href="/Platform/WorkFlowRun/Default.aspx?flowid=24f321ee-b4e3-4c2c-a0a4-f51cafdf526f&appid=90A30D34-F40F-4A0B-BCFD-3AD9786FF757" target="_blank">�������뵥(����)</a></li>
                                        <li><a href="/Platform/WorkFlowRun/Default.aspx?flowid=3f8de2dd-9229-4517-90a6-c13cb10a5c07&appid=A9EE1086-F066-41FD-A5C5-0BF50F272EB2" target="_blank">˽���������뵥(����)</a></li>
                                        <li class="divider"></li> 
                                        <li><a href="/Forms/Finance/T_CA_Report_Query.aspx" target="_blank">����/˽���������뵥��ѯ(����)</a></li>
                                        <li><a href="/Forms/Finance/OES_Report_Query.aspx" target="_blank">���ñ�������ѯ(����)</a></li>--%>
                                        <li  class="dropdown-submenu" >
                                            <a href="javascript:void(0)">���ñ���</a>
                                            <ul class="dropdown-menu">
                                                <li><a href="/Platform/WorkFlowRun/Default.aspx?flowid=24f321ee-b4e3-4c2c-a0a4-f51cafdf526f&appid=90A30D34-F40F-4A0B-BCFD-3AD9786FF757" target="_blank">�������뵥(����)</a></li>
                                                <li><a href="/Platform/WorkFlowRun/Default.aspx?flowid=3f8de2dd-9229-4517-90a6-c13cb10a5c07&appid=A9EE1086-F066-41FD-A5C5-0BF50F272EB2" target="_blank">˽���������뵥(����)</a></li>
                                                <li><a href="/Platform/WorkFlowRun/Default.aspx?flowid=4D085987-9001-48F7-B189-FFEE43A7DA71" target="_blank">���ñ�����(����)</a></li>
                                                <li class="divider"></li> 
                                                <li><a href="/Forms/Finance/T_CA_Report_Query.aspx" target="_blank">����/˽���������뵥��ѯ(����)</a></li>
                                                <li><a href="/Forms/Finance/OES_Report_Query.aspx" target="_blank">���ñ�������ѯ(����)</a></li>                                        
                                            </ul>
                                        </li>
                                        <li class="divider"></li> 
                                        <li><a href="/Fin/Fin_WG1_Report.aspx">�깤����һ</a></li>
                                        <li><a href="/Fin/Fin_WG2_Report.aspx">�깤�����</a></li>
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
                                       ���²�
                                    </div>
                                    <ul class="dropdown-menu" style="color: Black" role="menu">
                                      <li><a href="javascript:void(0)" target="_blank">����</a></li>   
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
    <%--��������--%>
    <div class="row  row-container">
        <div class="col-md-12">
            <div class="panel panel-info">
                <div class="panel-heading">
                    <strong>���۹���</strong> 
                   <%-- <div class="btn-group">
                        <div class="area_drop" data-toggle="dropdown">
                            <span class="caret"></span>
                        </div>
                        <ul class="dropdown-menu" style="color: black" role="menu">
                            <li><span href="#" id="loginsale" name="sale"   title="sale" style="padding-left:20px">����</span></li>
                            <li><span href="#" id="logoutsale" name="����"  title="����" style="padding-left:20px">�ǳ�</span></li>                            
                        </ul>
                    </div>--%>
                </div>
                <div class="panel-body">
                    <div class="col-xs-12 col-sm-12  col-md-12 col-lg-12">
                        <div class="area_block">
                            <div class="btn btn-padding-s btn-success " name="frm_BJ">
                                <div class="btn-group">
                                    <div class="btn btn-primary " data-toggle="dropdown"  name="btn_BJ">
                                        ���۹���</div>
                                    <ul class="dropdown-menu" style="color: Black" role="menu">
                                        <li><a href="baojia/baojia.aspx" target="_blank" style="display: inline">�½�����</a></li> 
                                        <li><a href="baojia/Baojia_Query.aspx" target="_blank" style="display: inline">���۲�ѯ</a>|<a href="Baojia/Baojia_history_UpdateBaojia.aspx" target="_blank" style="display: inline">����</a></li>                                       
                                        <li class="divider"></li> 
                                        <li><a href="baojia/Baojia_Progress_Query.aspx" target="_blank">���۽��ȸ���</a></li>
                                        
                                        <li><a href="baojia/Baojia_Task_Query.aspx" target="_blank">���������ѯ</a></li>
                                        <li class="divider"></li> 
                                        <li><a href="baojia/baojia_fenxi_tj.aspx" target="_blank">����ͳ��</a></li>
                                        
                                        <li><a href="baojia/baojia_zhongdianzhengqu_tj.aspx" target="_blank">�ص���ȡ��Ŀ����</a></li>
                                        <li><a href="baojia/baojia_dingdian_tj.aspx" target="_blank">������Ŀ����</a></li>
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
                                        ��ͬ����</div>
                                    <ul class="dropdown-menu" style="color: Black" role="menu">                                        
                                        <li><a href="http://172.16.5.8:8080/workflow/request/AddRequest.jsp?workflowid=158&isagent=0&beagenter=0" target="_blank">�½���ͬ</a></li>
                                        <li><a href="http://172.16.5.6:8080/oa/Contract_sale_list.aspx?" target="_blank">��ͬ��ѯ</a></li>                                        
                                        <li class="divider"></li>
                                        <li><a href="http://172.16.5.8:8080/workflow/request/AddRequest.jsp?workflowid=170&isagent=0&beagenter=0"  target="_blank">�½�ģ���տ�</a></li>
                                        <li><a href="http://172.16.5.6:8080/oa/Contract_gathering_list.aspx" target="_blank">ģ���տ��ѯ</a></li>
                                        <li><a href="http://172.16.5.6:8080/oa/Mjkp_TJ.aspx" target="_blank">ģ���տ����ͳ��</a></li>
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
                                        �ͻ�����</div>
                                    <ul class="dropdown-menu" style="color: Black" role="menu">
                                        <li><a href="sales/customer_QAD.aspx" target="_blank">�½��ͻ�</a></li>                                        
                                        <li class="divider"></li> 
                                        <li><a href="sales/Customer_report.aspx" target="_blank" style="display: inline">�ͻ���ѯ</a>|<a href="sales/Customer_Update_list.aspx" target="_blank" style="display: inline">����</a></li>
                                        <li class="divider"></li> 
                                        <li><a href="sales/Customer_Update_work.aspx" target="_blank">�ͻ������ѯ</a></li>
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
                                        ��Ʒ����</div>
                                    <ul class="dropdown-menu" style="color: Black" role="menu">                                        
                                        <li><a href="product/product.aspx"  target="_blank">�½���Ʒ��Ŀ��</a></li>
                                        <li><a href="product/chanpin_query.aspx"  target="_blank">��Ʒ��ѯ</a></li> 
                                        <li class="divider"></li> 
                                        <li><a href="product/Chanpin_Fenxi_ByCPFZR.aspx"  target="_blank">��Ʒͳ��</a></li> 
                                       <%-- <li><a href="product/Chanpin_Fenxi_ByCPFZR.aspx"  target="_blank">��Ʒ������</a></li> --%>
                                                                         
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
                                        ���۹���</div>
                                    <ul class="dropdown-menu" style="color: Black" role="menu">
                                        <li><a href="javascript:void(0)" target="_blank">����</a></li>                                        
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
                                        ��������</div>
                                    <ul class="dropdown-menu" style="color: Black" role="menu">
                                        <li><a href="/yangjian/Yangjian.aspx" target="_blank">��������ά��</a></li>                                        
                                        <li><a href="/dingchang/DC_Apply.aspx" target="_blank">����</a></li> 
                                        <li class="divider"></li>    
                                        <li><a href="/yangjian/YJ_Query_Report.aspx" target="_blank">���������ѯ</a></li>   
                                        <li><a href="/yangjian/YJ_Tracking_Process_Report.aspx" target="_blank">���������ѯ</a></li>
                                        <li><a href="/dingchang/Dingchang_Progress_Query.aspx" target="_blank">���������ѯ</a></li> 
                                        <li><a href="/yangjian/YangJianDelay_TJ.aspx" target="_blank">������������ͳ��</a></li>
                                        <li><a href="/yangjian/YangJianFinishRate_TJ.aspx" target="_blank" style="display:none">�����ͳ�Ʊ���</a></li>
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
                                        ��������</div>
                                    <ul class="dropdown-menu" style="color: Black" role="menu">
                                                                              
                                        <li><a href="/sales/Sale_DetailQuery.aspx" target="_blank">��Ʒ���۲�ѯ</a></li>
                                        <li><a href="/sales/customer_Query.aspx" target="_blank">����ͳ��</a></li>
                                        <li class="divider"></li> 
                                        <li><a href="product/chanpin_saleforecast.aspx"  target="_blank">������Ԥ��</a></li> 
                                        <li><a href="product/Chanpin_ForcastByMonth.aspx"  target="_blank">������Ԥ��</a></li>   

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
     <%--��������--%>
    <div class="row  row-container">
        <div class="col-md-12">
            <div class="panel panel-info">
                <div class="panel-heading">
                    <strong>���̲�</strong> 
                   <%-- <div class="btn-group">
                        <div class="area_drop" data-toggle="dropdown">
                            <span class="caret"></span>
                        </div>
                        <ul class="dropdown-menu" style="color: black" role="menu">
                            <li><span href="#" id="loginsale" name="sale"   title="sale" style="padding-left:20px">����</span></li>
                            <li><span href="#" id="logoutsale" name="����"  title="����" style="padding-left:20px">�ǳ�</span></li>                            
                        </ul>
                    </div>--%>
                </div>
                <div class="panel-body">
                    <div class="col-xs-12 col-sm-12  col-md-12 col-lg-12">
                        <div class="area_block">
                            <div class="btn btn-padding-s btn-success " name="frm_GC">
                                <div class="btn-group">
                                    <div class="btn btn-primary " data-toggle="dropdown"  name="btn_GC">
                                        ����</div>
                                    <ul class="dropdown-menu" style="color: Black" role="menu"> 
                                        <li><a href="/gongcheng/Product_GroupMatain.aspx"  target="_blank">��Ʒ�����ά��</a></li>                                                                               
                                        <li><a href="/gongcheng/DJ_JZ.aspx" target="_blank">���߽�ת</a></li>
                                        <li class="divider"></li>                                        
                                        <li><a href="http://172.16.5.6:8060/Report/DJList_Query.aspx"  target="_blank">�����嵥��ѯ</a></li>
                                        <li><a href="/gongcheng/DJLY_Detail.aspx"  target="_blank">����������ϸ��ѯ</a></li>     
                                        <li><a href="/gongcheng/DJ_analyse_bydj.aspx"  target="_blank">���߷�������(����)</a></li>     
                                        <li><a href="/gongcheng/DJ_analyse_byproduct.aspx"  target="_blank">���߷�������(��Ʒ)</a></li>  
                                                                        
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
                                        ���߹���</div>                                    
                                    <ul class="dropdown-menu" style="color: Black" role="menu"> 
                                        <li><a href="/Platform/WorkFlowRun/Default.aspx?flowid=f794a8ea-df18-421e-b394-9f0f709b027f&appid=3ee6c6db-d798-4416-b817-2cc95f11aa6f"  target="_blank">������������</a></li>
                                        <li><a href="/Platform/WorkFlowRun/Default.aspx?flowid=c53677dd-f0c7-4b21-9d6c-04c3034c5f18&appid=fe887b93-b243-4a39-970b-8172a7f6ad12"  target="_blank">��Ʒ�����嵥����</a></li>
                                        <li class="divider"></li>
                                        <li><a href="/MaterialBase/SZ_report_dev.aspx"  target="_blank">���߲�ѯ </a></li>  
                                        <li><a href="/MaterialBase/DJ_LY_Query.aspx"  target="_blank">�������ò�ѯ </a></li>  
                                         <li><a href="/Forms/MaterialBase/Daoju_List.aspx"  target="_blank">��Ʒ�����嵥��ѯ </a></li> 
                                         <li><a href="/MaterialBase/Product_Dj_CBQuery.aspx"  target="_blank">��Ʒ���߳ɱ� </a></li>                                    
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
                                        ��������</div>
                                    <ul class="dropdown-menu" style="color: Black" role="menu">
                                        <li><a href="/Platform/WorkFlowRun/Default.aspx?flowid=d9cb9476-13f9-48ec-a87e-5b84ca0790b0&appid=" target="_blank">��������������</a></li>
										<li><a href="/Platform/WorkFlowRun/Default.aspx?flowid=ee59e0b3-d6a1-4a30-a3b4-65d188323134&appid=BDDCD717-2DD6-4D83-828C-71C92FFF6AE4" target="_blank">����·������</a></li>
                                        <li><a href="/Platform/WorkFlowRun/Default.aspx?flowid=4A901BC7-EA83-43B1-80B6-5B14708DEDE9&appid=" target="_blank">BOM����</a></li>
                                        <li class="divider"></li>
                                        <li><a href="/Forms/MaterialBase/WuLiao_Report_Query.aspx" target="_blank">���������ϲ�ѯ</a></li>
										<li><a href="/Forms/PgiOp/GYLX_Report_Query.aspx" target="_blank">����·�߲�ѯ</a></li>
                                        <li><a href="/Forms/Bom/bom_query.aspx" target="_blank">BOM��ѯ</a></li>
                                        <li><a href="/Forms/PgiOp/Rpt_GYBom_Query.aspx" target="_blank">����.����.BOM����״̬��ѯ</a></li>
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
    <%--��Ŀ����--%>
    <div class="row  row-container">
        <div class="col-md-12">
            <div class="panel panel-info">
                <div class="panel-heading">
                    <strong>��Ŀ��</strong> 
                </div>
                <div class="panel-body">
                    <div class="col-xs-12 col-sm-12  col-md-12 col-lg-12">
                        <div class="area_block">
                            <div class="btn btn-padding-s btn-success " name="frm_bule2-1">
                                <div class="btn-group">
                                    <div class="btn btn-primary " data-toggle="dropdown"  name="btn_bule2-1">
                                        APQP</div>

                                    <ul class="dropdown-menu" style="color: Black" role="menu">                                        
                                        <li><a href="http://172.16.5.6:8080/source1/track_list.aspx" target="_blank">��Ŀ���ȸ���</a></li>
                                        <li><a href="/XM/APQP_TJ_Report.aspx"  target="_blank">��Ŀ����ͳ��</a></li>
                                        <li><a href="/XM/APQP_Rate_TJ.aspx"  target="_blank">��Ŀ���������ͳ��</a></li>   
                                         <li><a href="/XM/XM_TJ_Report.aspx"  target="_blank">��Ŀͳ��</a></li>                                 
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
                                        �Ų��ƻ�</div>
                                    <ul class="dropdown-menu" style="color: Black" role="menu">                                        
                                        <li><a href="javascript:void(0)" target="_blank">������....</a></li>
                                        
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
                                        ��������</div>
                                    <ul class="dropdown-menu" style="color: Black" role="menu">                                        
                                        <li><a href="javascript:void(0)" target="_blank">������....</a></li>
                                
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
                                        ��Ŀ�ƽ�</div>
                                    <ul class="dropdown-menu" style="color: Black" role="menu">                                        
                                        <li><a href="javascript:void(0)" target="_blank">������...</a></li>
                                
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
   <%--����������--%>
    <div class="row  row-container">
        <div class="col-md-12">
            <div class="panel panel-info">
                <div class="panel-heading">
                    <strong>������</strong>                   
                </div>
                <div class="panel-body">
                    <div class="col-xs-12 col-sm-12  col-md-12 col-lg-12">
                        <div class="area_block">
                            <div class="btn btn-padding-s btn-success " name="frm_bule2-1">
                                <div class="btn-group">
                                    <div class="btn btn-primary " data-toggle="dropdown"  name="btn_bule2-1">
                                        �ƻ�</div>
                                    <ul class="dropdown-menu" style="color: Black" role="menu">   
                                        <li><a href="javascript:void(0)"  target="_blank">����(TBD)</a></li>
                                        <li><a href="javascript:void(0)"  target="_blank">���(TBD)</a></li>                                  
                                        <li><a href="/wuliu/SemiFinishedKanban.aspx" target="_blank">���Ʒ���ݿ���(OnGoing)</a></li>
                                         <li><a href="/wuliu/FH_OverMnth.aspx"  target="_blank">��һ�����޽�������</a></li>
                                         <li><a href="/wuliu/UnFH_Remind.aspx"  target="_blank">©��������</a></li> 
                                         <li><a href="/Product/ForcastByMnth_Forwuliu.aspx"  target="_blank">�����¹���Ԥ��</a></li>   
                                         <li><a href="/kanban/Qad_WorkOrder_List.aspx"  target="_blank">������ת״̬������������</a></li>  
                                         <li><a href="/wuliu/Qad_Report_tr_hist_Sum.aspx"  target="_blank">������ܱ���</a></li>
                                         <li><a href="/wuliu/Qad_Report_tr_hist.aspx"  target="_blank">������ϸ����</a></li>
                                         <li><a href="/Wuliu/Edi_BMW_Query.aspx"  target="_blank">BMW Edi�Ա�</a></li>   
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
                                        ����</div>
                                    <ul class="dropdown-menu" style="color: Black" role="menu">                                       
                                         <li><a href="http://172.16.5.6:8080/wms/shipment_list.aspx"  target="_blank">�������ٲ�ѯ</a></li>  
                                         <li><a href="http://172.16.5.6:8080/wms/shipment_report.aspx"  target="_blank">������ͳ�Ʊ���</a></li> 
                                           <li class="divider"></li>
                                        <li><a href="/wuliu/wuliu_container_rate_report.aspx"  target="_blank">��װ��������</a></li> 
                                         <li><a href="/wuliu/FH_Query.aspx"  target="_blank">���˼�¼��ѯ</a></li>  
                                                                               
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
                                        �ֿ�</div>
                                    <ul class="dropdown-menu" style="color: Black" role="menu">                                        
                                        <li><a href="http://172.16.5.6:8080/dj/Sftnum_Query.aspx" target="_blank">��ȫ��汨��</a></li>
                                        <li><a href="http://172.16.5.6:8080/dj/Sftnum_Unapply.aspx"  target="_blank">�а�ȫ���δ�빺����</a></li> 
                                         <li><a href="http://172.16.5.6:8080/dj/Jiance_Data.aspx"  target="_blank">���������ͨʱ�䱨��</a></li>                                       
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
                                        ��װ</div>
                                    <ul class="dropdown-menu" style="color: Black" role="menu">                                        
                                        <li><a href="javascript:void(0)" target="_blank">�ɱ���TBD��</a></li>
                                        <li><a href="javascript:void(0)"  target="_blank">�����嵥(TBD)</a></li>
                                        <li><a href="http://172.16.5.8:8080/workflow/request/AddRequest.jsp?workflowid=149" target="_blank">��װ�������뵥</a></li>
                                        <li><a href="http://172.16.5.6:8080/oa/BZFASQ_LIST.aspx" target="_blank">��װ��������</a></li>                                  
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
        <%--������--%>
        <div class="row  row-container">
        <div class="col-md-12">
            <div class="panel panel-info">
                <div class="panel-heading">
                    <strong>������</strong>                   
                </div>
                <div class="panel-body">
                    <div class="col-xs-12 col-sm-12  col-md-12 col-lg-12">
                        <div class="area_block">
                            <div class="btn btn-padding-s btn-success " name="frm_ShenHe">
                                <div class="btn-group">
                                    <div class="btn btn-primary " data-toggle="dropdown"  name="btn_ShenHe">
                                        ���</div>
                                    <ul class="dropdown-menu" style="color: Black" role="menu">                                        
                                        <li><a href="javascript:void(0)" target="_blank">��˼ƻ��ƶ�</a></li>
                                        <li><a href="javascript:void(0)" target="_blank">��˼ƻ��鿴</a></li>   
                                        <li><a href="javascript:void(0)" target="_blank">��������ƶ�</a></li> 
                                        <li><a href="javascript:void(0)" target="_blank">���ͳ��</a></li>                                     
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
    <%--�ɹ���--%>
        <div class="row  row-container">
        <div class="col-md-12">
            <div class="panel panel-info">
                <div class="panel-heading">
                    <strong>�ɹ���</strong>                   
                </div>
                <div class="panel-body">
                    <div class="col-xs-12 col-sm-12  col-md-12 col-lg-12">
                        <div class="area_block">
                            <div class="btn btn-padding-s btn-success " name="frm_bule2-1">
                                <div class="btn-group">
                                    <div class="btn btn-primary " data-toggle="dropdown"  name="btn_bule2-1">
                                        �ɹ�����</div>
                                    <ul class="dropdown-menu" style="color: Black" role="menu">

                                        <li><a href="/Platform/WorkFlowRun/Default.aspx?flowid=ea7e5f10-96e5-432c-9dd5-5ecc16d5eb92&appid=7d6cf334-0227-4fcd-9faf-c2536d10cf8e" target="_blank">�빺���뵥(PR)</a></li>
                                        <li><a href="/Platform/WorkFlowRun/Default.aspx?flowid=ce701853-e13b-4c39-9cd6-b97e18656d31&appid=a4d66e5b-0456-47ce-b9aa-ef783f504583" target="_blank">�ɹ����뵥(PO)</a></li>
                                        <li class="divider"></li>
                                        <li><a href="/Forms/PurChase/PR_Report_Query.aspx" target="_blank">�빺����ѯ </a></li>
                                        <li><a href="/Forms/PurChase/PO_Report_Query.aspx" target="_blank">�ɹ�����ѯ </a></li>
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
    <%-- �ݲ�����������=============================================================================================================================================================--%>
    <div style="display: none">
      


    </div>
</asp:Content>
