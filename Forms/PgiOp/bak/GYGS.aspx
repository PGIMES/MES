<%@ Page Title="【工艺工时审批单】" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="GYGS.aspx.cs" Inherits="Forms_PgiOp_GYGS" 
     MaintainScrollPositionOnPostback="True" ValidateRequest="false"  enableEventValidation="false" %>

<%@ Register assembly="DevExpress.Web.v17.2, Version=17.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web" tagprefix="dx" %>

<asp:Content ID="HeaderContent" ContentPlaceHolderID="HeadContent" Runat="Server">
    <link href="../../Content/css/bootstrap.min.css" rel="stylesheet" />
    <script src="/Content/js/jquery.min.js"></script>
    <script src="../../Content/js/bootstrap.min.js"></script>
    <script src="../../Content/js/layer/layer.js"></script>

    <script type="text/javascript">
        function getQueryString(name) {
            var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)", "i");
            var r = decodeURI(window.location.search).substr(1).match(reg);
            if (r != null) return unescape(r[2]); return null;
        }
        var state = getQueryString("state");

        $(document).ready(function () {
            $("#mestitle").html("【工艺工时审批单】");

            //提出自定流程 JS 
            if ($('#3B271F67-0433-4082-AD1A-8DF1B967B879', parent.document).length == 0) {
                //alert("保存")
                $("input[id*=btnSave]").hide();
            }
            if ($('#8982B97C-ADBA-4A3A-AFD9-9A3EF6FF12D8', parent.document).length == 0) {
                //alert("发送")
                $("input[id*=btnflowSend]").hide();
            }
            if ($('#9A9C93B2-F77E-4EEE-BDF1-0E7D1A855B8D', parent.document).length == 0) {
                //alert("加签")
                $("#btnaddWrite").hide();
            }
            if ($('#86B7FA6C-891F-4565-9309-81672D3BA80A', parent.document).length == 0) {
                // alert("退回");
                $("#btnflowBack").hide();
            }
            if ($('#B8A7AF17-7AD5-4699-B679-D421691DD737', parent.document).length == 0) {
                //alert("查看流程");
                //  $("#btnshowProcess").hide();
            }
            if ($('#347B811C-7568-4472-9A61-6C31F66980AE', parent.document).length == 0) {
                //alert("转交");
                $("#btnflowRedirect").hide();
            }
            if ($('#954EFFA8-03B8-461A-AAA8-8727D090DCB9', parent.document).length == 0) {
                //alert("完成");
                $("#btnflowCompleted").hide();
            }
            if ($('#BC3172E5-08D2-449A-BFF0-BD6F4DE797B0', parent.document).length == 0) {
                //alert("终止");
                $("#btntaskEnd").hide();
            }

            var IsGrid_pro = '<%=IsGrid_pro%>'; var IsGrid_yz = '<%=IsGrid_yz%>';
            var IsRead = '<%=IsRead%>'; var IsRead_yz = '<%=IsRead_yz%>';  
            
            if(IsGrid_pro=="Y"){
                $("#div_product").css("display","inline-block");
            }
            if(IsGrid_yz=="Y"){
                $("#div_yz").css("display","inline-block");
            }

            if(IsRead=="Y"){
                $("[id$=gv_d] [id*=gzzx_i_]").each(function (){
                    $(this).css("display","none");
                });
                $("[id$=gv_d] [id*=pgi_no_i_]").each(function (){
                    $(this).css("display","none");
                });
            }
            if(IsRead_yz=="Y"){
                $("[id$=gv_d_yz] [id*=gzzx_i_yz_]").each(function (){
                    $(this).css("display","none");
                });
                $("[id$=gv_d_yz] [id*=pgi_no_i_yz_]").each(function (){
                    $(this).css("display","none");
                });
            }

            $("#CPXX input[id*='projectno']").change(function () {  
                $("#CPXX input[id*='pn']").val("");
                $("#CPXX input[id*='pn_desc']").val("");
                $("#CPXX input[id*='domain']").val("");
                $("#CPXX input[id*='yz_user']").val("");
                $("#CPXX input[id*='zl_user']").val("");
                $("#CPXX input[id*='product_user']").val("");
                $("#CPXX input[id*='ver']").val("");

                $("#CPXX input[id*='projectno']").val("");
            });
            
            if(state=="edit"){
                $("#CPXX input[id*='typeno']").each(function (){
                    if($(this).val()=="机加"){
                        if($("#SQXX input[id*='txt_CreateById']").val()==($("#CPXX input[id*='product_user']").val()).substr(0,5)){ $(this).prop("checked", true);}
                        else{ $(this).prop("checked", false);}  
                    }
                    if($(this).val()=="压铸"){
                        if($("#SQXX input[id*='txt_CreateById']").val()==($("#CPXX input[id*='yz_user']").val()).substr(0,5)){ $(this).prop("checked", true);}
                        else{ $(this).prop("checked", false);}  
                    }
                });
            }

            $("#CPXX input[id*='typeno']").change(function () {  
                $("#CPXX input[id*='typeno']").each(function (){      
                    if($(this).val()=="机加" && $("#SQXX input[id*='txt_CreateById']").val()==($("#CPXX input[id*='product_user']").val()).substr(0,5)){
                        if($(this).prop("checked")){
                            $("#div_product").css("display","inline-block");
                            gv_d.PerformCallback();
                        }else{
                            $("#div_product").css("display","none");
                        }
                    }
                    if($(this).val()=="压铸" && $("#SQXX input[id*='txt_CreateById']").val()==($("#CPXX input[id*='yz_user']").val()).substr(0,5)){
                        if($(this).prop("checked")){
                            $("#div_yz").css("display","inline-block");
                            gv_d_yz.PerformCallback();
                        }else{
                            $("#div_yz").css("display","none");
                        }
                    }
                });
            }); 

            //测试
            //layer.prompt({title: '输入任何口令，并确认', formType: 1}, function(pass, index){
            //    layer.close(index);
            //    layer.prompt({title: '填写工号，并确认', formType: 2}, function(text, index){
            //        $("#SQXX input[id*='txt_CreateById']").val(text);
            //        layer.close(index);
            //    });
                
            //});

        });

        //提出自定流程 JS 
        function setComment(val) {
            $('#comment', parent.document).val(val);
        }

        //设定表字段状态（可编辑性）
        var tabName="PGI_GYGS_Main_Form";//表名
        function SetControlStatus(fieldStatus)
        {  // tabName_columnName:1_0
            var flag=true;
            for(var item in fieldStatus){
                var id="MainContent_"+item.replace(tabName.toLowerCase()+"_","");
                
                if($("#"+id).length>0){
                    var ctype="";
                    if( $("#"+id).prop("tagName").toLowerCase()=="select"){
                        ctype="select"
                    }else if( $("#"+id).prop("tagName").toLowerCase() =="textarea"){
                        ctype="textarea"
                    }else if( $("#"+id).prop("tagName").toLowerCase() =="input"){
                        ctype=$("#"+id).prop("type");
                        
                    }

                    //ctype=(ctype).toLowerCase();

                    var statu=fieldStatus[item];
                    if( statu.indexOf("1_")!="-1" && (ctype=="text"||ctype=="textarea") ){
                        $("#"+id).attr("readonly","readonly");
                    }
                    else if( statu.indexOf("1_")!="-1" && ( ctype=="checkbox"||ctype=="radio"||ctype=="select"||ctype=="file" ) ){
                        $("#"+id).attr("disabled","disabled");
                    }
                }
            }
        }
        var tabName2="PGI_GYGS_Dtl_Form";//表名
        function SetControlStatus2(fieldStatus)
        {  // tabName_columnName:1_0
            var flag=true;
            for(var item in fieldStatus){
                var id=""+item.replace(tabName2.toLowerCase()+"_","");
                
                $.each($("[id*="+id+"]"), function (i, obj) {                


                    var ctype="";
                    if( $(obj).prop("tagName").toLowerCase()=="select"){
                        ctype="select"
                    }else if( $(obj).prop("tagName").toLowerCase() =="textarea"){
                        ctype="textarea"
                    }else if( $(obj).prop("tagName").toLowerCase() =="input"){
                        ctype=$(obj).prop("type");
                        
                    }

                    //ctype=(ctype).toLowerCase();

                    var statu=fieldStatus[item];
                    if( statu.indexOf("1_")!="-1" && (ctype=="text"||ctype=="textarea") ){
                        $(obj).attr("readonly","readonly");
                        $(obj).removeAttr("onclick");
                    }
                    else if( statu.indexOf("1_")!="-1" && ( ctype=="checkbox"||ctype=="radio"||ctype=="select"||ctype=="file"  ) ){
                        $(obj).attr("disabled","disabled");
                    }
                    else if(statu.indexOf("1_")!="-1" && ( ctype=="input" ) ){
                        $(obj).attr("type","hidden");
                    }

                });

            }
        }

        var fieldStatus = "1"=="<%=Request.QueryString["isreadonly"]%>"? {} : <%=fieldStatus%>;
        var displayModel = '<%=DisplayModel%>';
        $(window).load(function (){

            SetControlStatus(<%=fieldStatus%>);
            SetControlStatus2(<%=fieldStatus%>);       

	    });

    </script>
    <script type="text/javascript">
        function GetPgi_Product() 
        {
            var url = "/select/select_product_m.aspx";

            layer.open({
                title:'产品信息选择',
                type: 2,
                area: ['900px', '600px'],
                fixed: false, //不固定
                maxmin: true,
                content: url
            }); 
        }

        function setvalue_product(lspgino, lsproductcode, lsproductname, lsmake_factory, lsver, lszl_user, lsyz_user,lsproduct_user) 
        {
            
            $("#CPXX input[id*='pn']").val(lsproductcode);
            $("#CPXX input[id*='pn_desc']").val(lsproductname);
            $("#CPXX input[id*='domain']").val(lsmake_factory);
            $("#CPXX input[id*='yz_user']").val(lsyz_user);
            $("#CPXX input[id*='zl_user']").val(lszl_user);
            $("#CPXX input[id*='product_user']").val(lsproduct_user);
            $("#CPXX input[id*='projectno']").val(lspgino);
            
            var bf_product=false,bf_yz=false;
            $("#CPXX input[id*='typeno']").each(function (){
                if($(this).val()=="机加"){
                    if(lsproduct_user==""){ $(this).prop("checked", false);}
                    else{ $(this).prop("checked", true);bf_product=true;}  
                }
                if($(this).val()=="压铸"){
                    if(lsyz_user==""){ $(this).prop("checked", false);}
                    else{ $(this).prop("checked", true);bf_yz=true;}  
                }
            });

            if (bf_product) {
                if($("#SQXX input[id*='txt_CreateById']").val()==lsproduct_user.substr(0,5)){
                    $("#div_product").css("display","inline-block");
                    gv_d.PerformCallback();
                }
            }
            if (bf_yz) {
                if($("#SQXX input[id*='txt_CreateById']").val()==lsyz_user.substr(0,5)){
                    $("#div_yz").css("display","inline-block");
                    gv_d_yz.PerformCallback();
                }
            }

            $.ajax({
                type: "post",
                url: "GYGS.aspx/GetVer",
                data: "{'lspgino':'" + lspgino + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    var obj=eval(data.d);
                    $("#CPXX input[id*='ver']").val(obj[0].ver);
                }

            });
        }

        function GetPgi_Product_D(vi,ty)
        {
            var url = "/select/select_product_d.aspx?pgi_no="+$("#CPXX input[id*='projectno']").val()+"&domain="+$("#CPXX input[id*='domain']").val()+"&vi="+vi+"&ty="+ty;

            layer.open({
                title:'工艺流程选择',
                type: 2,
                area: ['800px', '600px'],
                fixed: false, //不固定
                maxmin: true,
                content: url
            }); 
        }

        function setvalue_product_d(lspgino, vi,ty) 
        {
            //20180620此处需要修改：需要判断项目号是否有申请过，开窗申请版本必须是A
            if (ty=="") {
                var pgi_no= eval('pgi_no' + vi);var pgi_no_t= eval('pgi_no_t' + vi);
                pgi_no.SetText(lspgino.substr(0,7));pgi_no_t.SetText(lspgino);
            }
            if (ty=="yz") {
                var pgi_no_yz= eval('pgi_no_yz' + vi);var pgi_no_t_yz= eval('pgi_no_t_yz' + vi);
                pgi_no_yz.SetText(lspgino.substr(0,7));pgi_no_t_yz.SetText(lspgino);
            }
        }

        function Get_wkzx(vi,ty){
            var url = "/select/select_wkzx.aspx?domain="+$("#CPXX input[id*='domain']").val()+"&vi="+vi+"&ty="+ty;

            layer.open({
                title:'工作中心选择',
                type: 2,
                area: ['600px', '600px'],
                fixed: false, //不固定
                maxmin: true,
                content: url
            });          
        }

        function setvalue_wkzx(ls_gzzx,ls_gzzx_desc,vi,ty){
            if (ty=="") {
                var gzzx_desc= eval('gzzx_desc' + vi);var gzzx= eval('gzzx' + vi);
                gzzx_desc.SetText(ls_gzzx_desc);gzzx.SetText(ls_gzzx);
            }
            if (ty=="yz") {
                var gzzx_desc_yz= eval('gzzx_desc_yz' + vi);var gzzx_yz= eval('gzzx_yz' + vi);
                gzzx_desc_yz.SetText(ls_gzzx_desc);gzzx_yz.SetText(ls_gzzx);
            }
        }

        function RefreshRow(vi) {
            var JgNum = eval('JgNum' + vi); var JgSec = eval('JgSec' + vi); var WaitSec = eval('WaitSec' + vi); var ZjSecc = eval('ZjSecc' + vi);var JtNum = eval('JtNum' + vi); 
            var col1 = eval('col1' + vi); var col2 = eval('col2' + vi);var EquipmentRate = eval('EquipmentRate' + vi);var col6 = eval('col6' + vi);

            var JgNum_value = Number($.trim(JgNum.GetText()) == "" ? 0 : $.trim(JgNum.GetText()));//每次加工数量
            var JgSec_value = Number($.trim(JgSec.GetText()) == "" ? 0 : $.trim(JgSec.GetText()));//加工时长(秒)
            var WaitSec_value = Number($.trim(WaitSec.GetText()) == "" ? 0 : $.trim(WaitSec.GetText()));//设备等待时间(秒)
            var ZjSecc_value = Number($.trim(ZjSecc.GetText()) == "" ? 0 : $.trim(ZjSecc.GetText()));//装夹时间(秒)
            var JtNum_value = Number($.trim(JtNum.GetText()) == "" ? 0 : $.trim(JtNum.GetText()));//机器台数
            var col1_value = Number($.trim(col1.GetText()) == "" ? 0 : $.trim(col1.GetText()));//单台需要人数
            var col2_value = Number($.trim(col2.GetText()) == "" ? 0 : $.trim(col2.GetText()));//本工序一人操作台数
            var EquipmentRate_value = Number($.trim(EquipmentRate.GetText()) == "" ? 0 : $.trim(EquipmentRate.GetText()));//本产品设备占用率
            var col6_value = Number($.trim(col6.GetText()) == "" ? 0 : $.trim(col6.GetText()));//单人报工数量

            var TjOpSec = eval('TjOpSec' + vi); var JSec = eval('JSec' + vi);var JHour = eval('JHour' + vi);
            var col3 = eval('col3' + vi); var col4 = eval('col4' + vi);var col5 = eval('col5' + vi);var col7 = eval('col7' + vi);

            //单台单件工序工时(秒)TjOpSec
            var TjOpSec_value = 0;
            if (JgNum_value != 0) { TjOpSec_value = (JgSec_value + WaitSec_value + ZjSecc_value) / JgNum_value; }
            TjOpSec.SetText(TjOpSec_value.toFixed(2));

            //单件工时(秒)
            var JSec_value = 0;
            if (JtNum_value != 0) { JSec_value = TjOpSec_value / JtNum_value; }
            JSec.SetText(JSec_value.toFixed(2));

            //单件工时(时)
            var JHour_value = TjOpSec_value/3600;
            JHour.SetText(JHour_value.toFixed(5));

            //单台83%产量
            var col3_value=0;
            if(TjOpSec_value!=0){ col3_value = (12 * 60 * 60 / TjOpSec_value) * 0.83 * EquipmentRate_value; }
            col3.SetText(col3_value.toFixed(0));

            //一人83%产量
            var col4_value=col2_value*col3_value;
            col4.SetText(col4_value.toFixed(0));

            //整线班产量
            var col5_value=0;
            if(JSec_value!=0){col5_value =(12 * 60 * 60 / JSec_value) * 0.83 * EquipmentRate_value;}
            col5.SetText(col5_value.toFixed(0));

            //单人报工数量
            if(col6_value<=0){
                col6.SetText(col4.GetText());
                col6_value = Number($.trim(col6.GetText()) == "" ? 0 : $.trim(col6.GetText()));//单人报工数量
            }            
            
            //单人产出工时
            var col7_value=(TjOpSec_value*col1_value*col6_value)/3600;
            col7.SetText(col7_value.toFixed(2));
        }

        function RefreshRow_yz(vi) {
            var JgNum = eval('JgNum_yz' + vi); var JgSec = eval('JgSec_yz' + vi); var WaitSec = eval('WaitSec_yz' + vi); var ZjSecc = eval('ZjSecc_yz' + vi);var JtNum = eval('JtNum_yz' + vi); 
            var col1 = eval('col1_yz' + vi); var col2 = eval('col2_yz' + vi);var EquipmentRate = eval('EquipmentRate_yz' + vi);var col6 = eval('col6_yz' + vi);

            var JgNum_value = Number($.trim(JgNum.GetText()) == "" ? 0 : $.trim(JgNum.GetText()));//每次加工数量
            var JgSec_value = Number($.trim(JgSec.GetText()) == "" ? 0 : $.trim(JgSec.GetText()));//加工时长(秒)
            var WaitSec_value = Number($.trim(WaitSec.GetText()) == "" ? 0 : $.trim(WaitSec.GetText()));//设备等待时间(秒)
            var ZjSecc_value = Number($.trim(ZjSecc.GetText()) == "" ? 0 : $.trim(ZjSecc.GetText()));//装夹时间(秒)
            var JtNum_value = Number($.trim(JtNum.GetText()) == "" ? 0 : $.trim(JtNum.GetText()));//机器台数
            var col1_value = Number($.trim(col1.GetText()) == "" ? 0 : $.trim(col1.GetText()));//单台需要人数
            var col2_value = Number($.trim(col2.GetText()) == "" ? 0 : $.trim(col2.GetText()));//本工序一人操作台数
            var EquipmentRate_value = Number($.trim(EquipmentRate.GetText()) == "" ? 0 : $.trim(EquipmentRate.GetText()));//本产品设备占用率
            var col6_value = Number($.trim(col6.GetText()) == "" ? 0 : $.trim(col6.GetText()));//单人报工数量

            var TjOpSec = eval('TjOpSec_yz' + vi); var JSec = eval('JSec_yz' + vi);var JHour = eval('JHour_yz' + vi);
            var col3 = eval('col3_yz' + vi); var col4 = eval('col4_yz' + vi);var col5 = eval('col5_yz' + vi);var col7 = eval('col7_yz' + vi);

            //单台单件工序工时(秒)TjOpSec
            var TjOpSec_value = 0;
            if (JgNum_value != 0) { TjOpSec_value = (JgSec_value + WaitSec_value + ZjSecc_value) / JgNum_value; }
            TjOpSec.SetText(TjOpSec_value.toFixed(2));

            //单件工时(秒)
            var JSec_value = 0;
            if (JtNum_value != 0) { JSec_value = TjOpSec_value / JtNum_value; }
            JSec.SetText(JSec_value.toFixed(2));

            //单件工时(时)
            var JHour_value = TjOpSec_value/3600;
            JHour.SetText(JHour_value.toFixed(5));

            //单台83%产量
            var col3_value=0;
            if(TjOpSec_value!=0){ col3_value = (12 * 60 * 60 / TjOpSec_value) * 0.83 * EquipmentRate_value; }
            col3.SetText(col3_value.toFixed(0));

            //一人83%产量
            var col4_value=col2_value*col3_value;
            col4.SetText(col4_value.toFixed(0));

            //整线班产量
            var col5_value=0;
            if(JSec_value!=0){col5_value =(12 * 60 * 60 / JSec_value) * 0.83 * EquipmentRate_value;}
            col5.SetText(col5_value.toFixed(0));

            //单人报工数量
            if(col6_value<=0){
                col6.SetText(col4.GetText());
                col6_value = Number($.trim(col6.GetText()) == "" ? 0 : $.trim(col6.GetText()));//单人报工数量
            }
            
            //单人产出工时
            var col7_value=(TjOpSec_value*col1_value*col6_value)/3600;
            col7.SetText(col7_value.toFixed(2));
        }

        function validate(action){
            var flag=true;var msg="";
            
            <%=ValidScript%>

            if($("#CPXX input[id*='projectno']").val()==""){
                msg+="【PGI项目号】不可为空.<br />";
            }

            if($("#CPXX input[id*='ver']").val()==""){
                msg+="【工艺路线版本】不可为空.<br />";
            }

            var bf=false;
             $("#CPXX input[id*='typeno']").each(function (){                
                if($(this).prop("checked")){
                    bf=true;
                    if($(this).val()=="机加"){
                        if($("#CPXX input[id*='product_user']").val()==""){msg+="【产品工程师】为空，不可勾选【机加】.<br />";}
                    }
                    if($(this).val()=="压铸"){
                        if($("#CPXX input[id*='yz_user']").val()==""){msg+="【压铸工程师】为空，不可勾选【压铸】.<br />";}
                    }
                }
            });

            if(bf==false){msg+="【会签工艺段】不可为空.<br />";}

            //if($("[id$=gv_d] input[id*=op]").length==0){
            //    msg+="【工艺工时信息】不可为空.<br />";
            //}


            if(action=='submit'){
                if($('#div_product').css('display')=='inline-block'){
                    $("[id$=gv_d] input[id*=pgi_no]").each(function (){
                        if( $(this).val()==""){
                            msg+="【项目号】不可为空.<br />";
                            return false;
                        }
                    });
                    $("[id$=gv_d] input[id*=pgi_no_t]").each(function (){
                        if( $(this).val()==""){
                            msg+="【工艺流程】不可为空.<br />";
                            return false;
                        }
                    });
                    $("[id$=gv_d] input[id*=op]").each(function (){
                        if( $(this).val()==""){
                            msg+="【工序号】不可为空.<br />";
                            return false;
                        }
                    });
                    $("[id$=gv_d] input[id*=op_desc]").each(function (){
                        if( $(this).val()==""){
                            msg+="【工序名称】不可为空.<br />";
                            return false;
                        }
                    });
                    $("[id$=gv_d] input[id*=op_remark]").each(function (){
                        if( $(this).val()==""){
                            msg+="【工序说明】不可为空.<br />";
                            return false;
                        }
                    });
                    $("[id$=gv_d] input[id*=gzzx_desc]").each(function (){
                        if( $(this).val()==""){
                            msg+="【设备】不可为空.<br />";
                            return false;
                        }
                    });
                    $("[id$=gv_d] input[id*=gzzx]").each(function (){
                        if( $(this).val()==""){
                            msg+="【工作中心代码】不可为空.<br />";
                            return false;
                        }
                    });
                    $("[id$=gv_d] input[id*=JgNum]").each(function (){
                        if( $(this).val()==""){
                            msg+="【每次加工数量】不可为空.<br />";
                            return false;
                        }
                    });
                    $("[id$=gv_d] input[id*=JgSec]").each(function (){
                        if( $(this).val()==""){
                            msg+="【加工时长】不可为空.<br />";
                            return false;
                        }
                    });
                    $("[id$=gv_d] input[id*=WaitSec]").each(function (){
                        if( $(this).val()==""){
                            msg+="【设备等待时间】不可为空.<br />";
                            return false;
                        }
                    });
                    $("[id$=gv_d] input[id*=ZjSecc]").each(function (){
                        if( $(this).val()==""){
                            msg+="【装夹时间】不可为空.<br />";
                            return false;
                        }
                    });
                    $("[id$=gv_d] input[id*=JtNum]").each(function (){
                        if( $(this).val()==""){
                            msg+="【机器台数】不可为空.<br />";
                            return false;
                        }
                    });
                    $("[id$=gv_d] input[id*=col1]").each(function (){
                        if( $(this).val()==""){
                            msg+="【单台需要人数】不可为空.<br />";
                            return false;
                        }
                    });
                    $("[id$=gv_d] input[id*=col2]").each(function (){
                        if( $(this).val()==""){
                            msg+="【本工序一人操作台数】不可为空.<br />";
                            return false;
                        }
                    });
                    $("[id$=gv_d] input[id*=EquipmentRate]").each(function (){
                        if( $(this).val()==""){
                            msg+="【本产品设备占用率】不可为空.<br />";
                            return false;
                        }
                    });
                    $("[id$=gv_d] input[id*=col6]").each(function (){
                        if( $(this).val()==""){
                            msg+="【单人报工数量】不可为空.<br />";
                            return false;
                        }
                    });
                }

                if($('#div_yz').css('display')=='inline-block'){
                    $("[id$=gv_d_yz] input[id*=pgi_no]").each(function (){
                        if( $(this).val()==""){
                            msg+="【项目号】不可为空.<br />";
                            return false;
                        }
                    });
                    $("[id$=gv_d_yz] input[id*=pgi_no_t]").each(function (){
                        if( $(this).val()==""){
                            msg+="【工艺流程】不可为空.<br />";
                            return false;
                        }
                    });
                    $("[id$=gv_d_yz] input[id*=op]").each(function (){
                        if( $(this).val()==""){
                            msg+="【工序号】不可为空.<br />";
                            return false;
                        }
                    });
                    $("[id$=gv_d_yz] input[id*=op_desc]").each(function (){
                        if( $(this).val()==""){
                            msg+="【工序名称】不可为空.<br />";
                            return false;
                        }
                    });
                    $("[id$=gv_d_yz] input[id*=op_remark]").each(function (){
                        if( $(this).val()==""){
                            msg+="【工序说明】不可为空.<br />";
                            return false;
                        }
                    });
                    $("[id$=gv_d_yz] input[id*=gzzx_desc]").each(function (){
                        if( $(this).val()==""){
                            msg+="【设备】不可为空.<br />";
                            return false;
                        }
                    });
                    $("[id$=gv_d_yz] input[id*=gzzx]").each(function (){
                        if( $(this).val()==""){
                            msg+="【工作中心代码】不可为空.<br />";
                            return false;
                        }
                    });
                    $("[id$=gv_d_yz] input[id*=weights]").each(function (){
                        if( $(this).val()==""){
                            msg+="【压铸每模重量(kg)】不可为空.<br />";
                            return false;
                        }
                    });
                    $("[id$=gv_d_yz] input[id*=acupoints]").each(function (){
                        if( $(this).val()==""){
                            msg+="【每模穴数】不可为空.<br />";
                            return false;
                        }
                    });
                    $("[id$=gv_d_yz] input[id*=capacity]").each(function (){
                        if( $(this).val()==""){
                            msg+="【每小时设备产能(kg)】不可为空.<br />";
                            return false;
                        }
                    });
                    $("[id$=gv_d_yz] input[id*=JgNum]").each(function (){
                        if( $(this).val()==""){
                            msg+="【每次加工数量】不可为空.<br />";
                            return false;
                        }
                    });
                    $("[id$=gv_d_yz] input[id*=JgSec]").each(function (){
                        if( $(this).val()==""){
                            msg+="【加工时长】不可为空.<br />";
                            return false;
                        }
                    });
                    $("[id$=gv_d_yz] input[id*=WaitSec]").each(function (){
                        if( $(this).val()==""){
                            msg+="【设备等待时间】不可为空.<br />";
                            return false;
                        }
                    });
                    $("[id$=gv_d_yz] input[id*=ZjSecc]").each(function (){
                        if( $(this).val()==""){
                            msg+="【装夹时间】不可为空.<br />";
                            return false;
                        }
                    });
                    $("[id$=gv_d_yz] input[id*=JtNum]").each(function (){
                        if( $(this).val()==""){
                            msg+="【机器台数】不可为空.<br />";
                            return false;
                        }
                    });
                    $("[id$=gv_d_yz] input[id*=col1]").each(function (){
                        if( $(this).val()==""){
                            msg+="【单台需要人数】不可为空.<br />";
                            return false;
                        }
                    });
                    $("[id$=gv_d_yz] input[id*=col2]").each(function (){
                        if( $(this).val()==""){
                            msg+="【本工序一人操作台数】不可为空.<br />";
                            return false;
                        }
                    });
                    $("[id$=gv_d_yz] input[id*=EquipmentRate]").each(function (){
                        if( $(this).val()==""){
                            msg+="【本产品设备占用率】不可为空.<br />";
                            return false;
                        }
                    });
                    $("[id$=gv_d_yz] input[id*=col6]").each(function (){
                        if( $(this).val()==""){
                            msg+="【单人报工数量】不可为空.<br />";
                            return false;
                        }
                    });
                }
            }

            if(msg!=""){  
                flag=false;
                layer.alert(msg);
            }
           return flag;
        }

    </script>

    <style type="text/css">
        .row {
            margin-right: 2px;
            margin-left: 2px;
        }

        .textalign {
            text-align: right;
        }

        .alignRight {
            padding-right: 4px;
            text-align: right;
        }

        .row-container {
            padding-left: 2px;
            padding-right: 2px;
        }

        fieldset {
            background: rgba(255,255,255,.3);
            border-color: lightblue;
            border-style: solid;
            border-width: 2px;
            -moz-border-radius: 5px;
            -webkit-border-radius: 5px;
            -khtml-border-radius: 5px;
            border-radius: 5px;
            /*line-height: 30px;*/
            list-style: none;
            padding: 5px 10px;
            margin-bottom: 2px;
        }

        legend {
            color: #302A2A;
            font: bold 16px/2 Verdana, Geneva, sans-serif;
            font-weight: bold;
            text-align: left;
            /*text-shadow: 2px 2px 2px rgb(88, 126, 156);*/
        }

        fieldset {
            padding: .35em .625em .75em;
            margin: 0 2px;
            border: 1px solid lightblue;
        }

        legend {
            padding: 5px;
            border: 0;
            width: auto;
            margin-bottom: 2px;
        }

        .panel {
            margin-bottom: 5px;
        }

        .panel-heading {
            padding: 5px 5px 5px 5px;
        }

        .panel-body {
            padding: 5px 5px 5px 5px;
        }

        body {
            margin-left: 5px;
            margin-right: 5px;
        }

        .col-lg-1, .col-lg-10, .col-lg-11, .col-lg-12, .col-lg-2, .col-lg-3, .col-lg-4, .col-lg-5, .col-lg-6, .col-lg-7, .col-lg-8, .col-lg-9, .col-md-1, .col-md-10, .col-md-11, .col-md-12, .col-md-2, .col-md-3, .col-md-4, .col-md-5, .col-md-6, .col-md-7, .col-md-8, .col-md-9, .col-sm-1, .col-sm-10, .col-sm-11, .col-sm-12, .col-sm-2, .col-sm-3, .col-sm-4, .col-sm-5, .col-sm-6, .col-sm-7, .col-sm-8, .col-sm-9, .col-xs-1, .col-xs-10, .col-xs-11, .col-xs-12, .col-xs-2, .col-xs-3, .col-xs-4, .col-xs-5, .col-xs-6, .col-xs-7, .col-xs-8, .col-xs-9 {
            position: relative;
            min-height: 1px;
            padding-right: 1px;
            padding-left: 1px;
            margin-top: 0px;
            top: 0px;
            left: 0px;
        }

        td {
            /* vertical-align: top;
            font-weight: 600;*/
            font-size: 12px;
            padding-bottom: 5px;
            padding-left: 3px;
            white-space: nowrap;
        }

        p.MsoListParagraph {
            margin-bottom: .0001pt;
            text-align: justify;
            text-justify: inter-ideograph;
            text-indent: 21.0pt;
            font-size: 10.5pt;
            font-family: "Calibri","sans-serif";
            margin-left: 0cm;
            margin-right: 0cm;
            margin-top: 0cm;
        }

        .his {
            padding-left: 8px;
            padding-right: 8px;
        }

        .tbl td {
            border: 1px solid black;
            padding-left: 3px;
            padding-right: 3px;
            padding-top: 3px;
        }

        .wrap {
            word-break: break-all;
            white-space: normal;
        }
         .hidden { display:none;}
		     .hidden1
            {
            	border:0px; 
            	overflow:hidden
            	
            	}
    </style>
    <style>
          .btnSave{ background:url(/Images/ico/save.gif) no-repeat  0.3em;
                  padding-left:24px;
                  border:0
        }
        .btnflowSend{ background:url(/Images/ico/arrow_medium_right.png) no-repeat  0.3em;
                  padding-left:24px;
                  border:0
        }
        .btnaddWrite{ background:url(/Images/ico/edit.gif) no-repeat  0.3em;
                  padding-left:24px;
                  border:0
        }
        .btnflowBack{ background:url(/Images/ico/arrow_medium_left.png) no-repeat  0.3em;
                  padding-left:24px;
                  border:0
        }
        .btnflowCompleted{ background:url(/Images/ico/arrow_medium_lower_right.png) no-repeat  0.3em;
                  padding-left:24px;
                  border:0
        }
        .btnshowProcess{ background:url(/Images/ico/search.png) no-repeat  0.3em;
                  padding-left:24px;
                  border:0
        }     

    </style>
    <style>
        #CPXX label{
            font-weight:400;
        }
        .lineread{
            font-size:9px; border:none; border-bottom:1px solid #ccc;
        }
        .linewrite{
            font-size:9px; border:none; border-bottom:1px solid #ccc;background-color:#FDF7D9;/*EFEFEF*/
        }
        /*.dxeTextBox .dxeEditArea{
            background-color:#FDF7D9;
        }*/
    </style>
</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div class="col-md-12  ">
        <div class="col-md-10  ">
            <div class="form-inline " style="text-align:right">
                <asp:Button ID="btnSave" runat="server" Text="保存" CssClass="btn btn-default btn-xs btnSave" OnClientClick="return validate();" OnClick="btnSave_Click" />
                <asp:Button ID="btnflowSend" runat="server" Text="发送" CssClass="btn btn-default btn-xs btnflowSend"  OnClientClick="return validate('submit');" OnClick="btnflowSend_Click" />
                <input id="btnaddWrite" type="button" value="加签" onclick="parent.addWrite(true);" class="btn btn-default btn-xs btnaddWrite" />
                <input id="btnflowBack" type="button" value="退回" onclick="parent.flowBack(true);" class="btn btn-default btn-xs btnflowBack" />
                <input id="btnflowCompleted" type="button" value="完成" onclick="parent.flowCompleted(true);" class="btn btn-default btn-xs btnflowCompleted" />
                <input id="btnshowProcess" type="button" value="查看流程" onclick="parent.showProcess(true);" class="btn btn-default btn-xs btnshowProcess" />
            </div>
        </div>
    </div>

    <div class="col-md-12" >  

        <div class="row row-container">
            <div class="panel panel-info">
                <div class="panel-heading" data-toggle="collapse" data-target="#SQXX">
                    <strong>申请人信息</strong>
                </div>
                <div class="panel-body <% =ViewState["lv"].ToString() == "SQXX" ? "" : "collapse" %>" id="SQXX">
                    <div class="col-xs-12 col-sm-12  col-md-12 col-lg-12" style="width:1000px;">
                        <asp:UpdatePanel ID="UpdatePanel_request" runat="server">
                            <ContentTemplate>
                                <table style="height: 30px; width: 100%">
                                    <tr>
                                        <td style="width:80px">申请人</td><%--class="form-control input-s-sm"--%>
                                        <td style="width:250px">
                                            <div class="form-inline">
                                                <input id="txt_CreateById" class="lineread"  style="height: 25px; width: 90px;font-size:12px;" runat="server" readonly="True"  />
                                                <input id="txt_CreateByName" class="lineread" style="height: 25px; width: 90px;font-size:12px;" runat="server" readonly="True" />
                                            </div>
                                        </td>
                                        <td style="width:80px">申请部门</td>
                                        <td style="width:250px">
                                            <div class="form-inline">                                                
                                                <input id="txt_CreateByDept" class="lineread" style="height: 25px; width: 180px;font-size:12px;" runat="server" readonly="True" />
                                            </div>
                                        </td>
                                        <td style="width:80px">申请时间</td>
                                        <td style="width:250px">
                                            <input id="txt_CreateDate" class="lineread" style="height: 25px; width: 180px;font-size:12px;" runat="server" readonly="True" />
                                        </td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
        </div>


        <div class="row  row-container">
            <div class="panel panel-info">
                <div class="panel-heading" data-toggle="collapse" data-target="#CPXX">
                        <strong>产品信息</strong>
                </div>
                <div class="panel-body " id="CPXX">
                    <div class="col-xs-12 col-sm-12  col-md-12 col-lg-12" style="width:1000px;">
                        <div>
                            <asp:TextBox ID="txt_domain" runat="server" style="display:none;"></asp:TextBox>
                            <asp:Table Style="width: 100%;" border="0" runat="server" ID="tblCPXX" Font-Size="12px" > 
                            </asp:Table>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div id="div_product" class="row  row-container" style="display:none;">
            <div class="panel panel-info">
                <div class="panel-heading" data-toggle="collapse" data-target="#GYGS">
                        <strong>工艺工时信息<font color="blue">-机加</font></strong>
                </div>
                <div class="panel-body " id="GYGS">
                    <div class="col-xs-12 col-sm-12  col-md-12 col-lg-12">
                        <div style="padding: 2px 5px 5px 0px">                
                            
                        </div>
                        <asp:UpdatePanel runat="server" ID="p1" UpdateMode="Conditional" >
                            <ContentTemplate>
                                <asp:Button ID="btndel" runat="server" Text="删除" class="btn btn-default" style="width:60px; height:32px;"  OnClick="btndel_Click" />

                                 <dx:aspxgridview ID="gv_d" runat="server" AutoGenerateColumns="False" KeyFieldName="numid" Theme="MetropolisBlue" OnCustomCallback="gv_d_CustomCallback" 
                                      OnRowCommand="gv_d_RowCommand" ClientInstanceName="gv_d"  EnableTheming="True"  >                                   
                                    <SettingsPager PageSize="1000"></SettingsPager>
                                    <Settings ShowFooter="True" />
                                    <SettingsBehavior AllowSelectByRowClick="True" AllowDragDrop="False" AllowSort="False" />
                                    <Columns>
                                        <dx:GridViewCommandColumn SelectAllCheckboxMode="Page" ShowClearFilterButton="true" ShowSelectCheckbox="true" Name="Sel" Width="30" VisibleIndex="1"></dx:GridViewCommandColumn>
                                        <dx:GridViewDataTextColumn Caption="项目号" FieldName="pgi_no" Width="75px" VisibleIndex="3">
                                            <Settings AllowCellMerge="False" />
                                            <DataItemTemplate>     
                                                 <table>
                                                    <tr>
                                                        <td>           
                                                            <dx:ASPxTextBox ID="pgi_no" Width="75px" runat="server" Value='<%# Eval("pgi_no")%>' 
                                                                ClientInstanceName='<%# "pgi_no"+Container.VisibleIndex.ToString() %>' ></dx:ASPxTextBox>   
                                                        </td>
                                                        <td><i id="pgi_no_i_<%#Container.VisibleIndex.ToString() %>" class="fa fa-search" onclick="GetPgi_Product_D(<%# Container.VisibleIndex %>,'')"></i></td>
                                                    </tr>
                                                </table>                  
                                            </DataItemTemplate>   
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="工艺流程" FieldName="pgi_no_t" Width="75px" VisibleIndex="3">
                                            <Settings AllowCellMerge="False" />
                                            <DataItemTemplate>     
                                                 <dx:ASPxTextBox ID="pgi_no_t" Width="75px" runat="server" Value='<%# Eval("pgi_no_t")%>' 
                                                                ClientInstanceName='<%# "pgi_no_t"+Container.VisibleIndex.ToString() %>' ></dx:ASPxTextBox>                    
                                            </DataItemTemplate>   
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="工序号" FieldName="op" Width="45px" VisibleIndex="4">
                                            <Settings AllowCellMerge="False" />
                                            <DataItemTemplate>                
                                                <dx:ASPxTextBox ID="op" Width="45px" runat="server" Value='<%# Eval("op")%>' ></dx:ASPxTextBox>                
                                            </DataItemTemplate>   
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="工序名称" FieldName="op_desc" Width="130px" VisibleIndex="5">
                                            <Settings AllowCellMerge="False" />
                                            <DataItemTemplate>                
                                                <dx:ASPxTextBox ID="op_desc" Width="130px" runat="server" Value='<%# Eval("op_desc")%>' ></dx:ASPxTextBox>                
                                            </DataItemTemplate>        
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="工序说明" FieldName="op_remark" Width="130px" VisibleIndex="6">
                                            <Settings AllowCellMerge="False" />
                                            <DataItemTemplate>                
                                                <dx:ASPxTextBox ID="op_remark" Width="130px" runat="server" Value='<%# Eval("op_remark")%>' ></dx:ASPxTextBox>                
                                            </DataItemTemplate>        
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="设备<br />(工作中心名称)" FieldName="gzzx_desc" Width="100px" VisibleIndex="7">
                                            <Settings AllowCellMerge="False" />
                                            <DataItemTemplate>
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <dx:ASPxTextBox ID="gzzx_desc" Width="100px" runat="server" Value='<%# Eval("gzzx_desc")%>' 
                                                                ClientInstanceName='<%# "gzzx_desc"+Container.VisibleIndex.ToString() %>' Border-BorderWidth="0"   ReadOnly="true">
                                                            </dx:ASPxTextBox>
                                                        </td>
                                                        <td><i id="gzzx_i_<%#Container.VisibleIndex.ToString() %>" class="fa fa-search" onclick="Get_wkzx(<%# Container.VisibleIndex %>,'')"></i></td>
                                                    </tr>
                                                </table>       
                                            </DataItemTemplate>
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="工作中<br />心代码" FieldName="gzzx" Width="40px" VisibleIndex="8">
                                             <DataItemTemplate>
                                                <dx:ASPxTextBox ID="gzzx" Width="40px" runat="server" Value='<%# Eval("gzzx")%>' 
                                                    ClientInstanceName='<%# "gzzx"+Container.VisibleIndex.ToString() %>' Border-BorderWidth="0" ReadOnly="true">
                                                </dx:ASPxTextBox> 
                                            </DataItemTemplate>
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="是否报<br />工(Y/N)" FieldName="IsBg" Width="35px" VisibleIndex="9"></dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="每次加<br />工数量" FieldName="JgNum" Width="40px" VisibleIndex="10">
                                            <Settings AllowCellMerge="False"/>
                                            <DataItemTemplate>
                                                <dx:ASPxTextBox ID="JgNum" Width="40px" runat="server" Value='<%# Eval("JgNum")%>' 
                                                    ClientSideEvents-ValueChanged='<%# "function(s,e){RefreshRow("+Container.VisibleIndex+");}" %>' 
                                                    ClientInstanceName='<%# "JgNum"+Container.VisibleIndex.ToString() %>'>
                                                     <ValidationSettings ValidationGroup="ValueValidationGroup" Display="Dynamic" ErrorTextPosition="Bottom">
                                                        <RegularExpression ErrorText="请输入数字！" ValidationExpression="^-?[1-9]+(\.\d+)?$|^-?0(\.\d+)?$|^-?[1-9]+[0-9]*(\.\d+)?$" />
                                                    </ValidationSettings>
                                                </dx:ASPxTextBox>
                                            </DataItemTemplate>        
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="加工时<br />长(秒)" FieldName="JgSec" Width="40px" VisibleIndex="12">
                                            <Settings AllowCellMerge="False" />
                                            <DataItemTemplate>                
                                                <dx:ASPxTextBox ID="JgSec" Width="40px" runat="server" Value='<%# Eval("JgSec")%>'
                                                    ClientSideEvents-ValueChanged='<%# "function(s,e){RefreshRow("+Container.VisibleIndex+");}" %>' 
                                                    ClientInstanceName='<%# "JgSec"+Container.VisibleIndex.ToString() %>'>
                                                     <ValidationSettings ValidationGroup="ValueValidationGroup" Display="Dynamic" ErrorTextPosition="Bottom">
                                                        <RegularExpression ErrorText="请输入数字！" ValidationExpression="^-?[1-9]+(\.\d+)?$|^-?0(\.\d+)?$|^-?[1-9]+[0-9]*(\.\d+)?$" />
                                                    </ValidationSettings>
                                                </dx:ASPxTextBox>                
                                            </DataItemTemplate>        
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="设备等待<br />时间(秒)" FieldName="WaitSec" Width="50px" VisibleIndex="12">
                                            <Settings AllowCellMerge="False" />
                                            <DataItemTemplate>                
                                                <dx:ASPxTextBox ID="WaitSec" Width="50px" runat="server" Value='<%# Eval("WaitSec")%>'
                                                    ClientSideEvents-ValueChanged='<%# "function(s,e){RefreshRow("+Container.VisibleIndex+");}" %>' 
                                                    ClientInstanceName='<%# "WaitSec"+Container.VisibleIndex.ToString() %>'>
                                                     <ValidationSettings ValidationGroup="ValueValidationGroup" Display="Dynamic" ErrorTextPosition="Bottom">
                                                        <RegularExpression ErrorText="请输入数字！" ValidationExpression="^-?[1-9]+(\.\d+)?$|^-?0(\.\d+)?$|^-?[1-9]+[0-9]*(\.\d+)?$" />
                                                    </ValidationSettings>
                                                </dx:ASPxTextBox>                
                                            </DataItemTemplate>        
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="装夹时<br />间(秒)" FieldName="ZjSecc" Width="40px" VisibleIndex="13">
                                            <Settings AllowCellMerge="False" />
                                            <DataItemTemplate>                
                                                <dx:ASPxTextBox ID="ZjSecc" Width="40px" runat="server" Value='<%# Eval("ZjSecc")%>'
                                                    ClientSideEvents-ValueChanged='<%# "function(s,e){RefreshRow("+Container.VisibleIndex+");}" %>' 
                                                    ClientInstanceName='<%# "ZjSecc"+Container.VisibleIndex.ToString() %>'>
                                                     <ValidationSettings ValidationGroup="ValueValidationGroup" Display="Dynamic" ErrorTextPosition="Bottom">
                                                        <RegularExpression ErrorText="请输入数字！" ValidationExpression="^-?[1-9]+(\.\d+)?$|^-?0(\.\d+)?$|^-?[1-9]+[0-9]*(\.\d+)?$" />
                                                    </ValidationSettings>
                                                </dx:ASPxTextBox>                
                                            </DataItemTemplate>        
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="机器<br />台数" FieldName="JtNum" Width="40px" VisibleIndex="14">
                                            <Settings AllowCellMerge="False" />
                                            <DataItemTemplate>                
                                                <dx:ASPxTextBox ID="JtNum" Width="40px" runat="server" Value='<%# Eval("JtNum")%>'
                                                    ClientSideEvents-ValueChanged='<%# "function(s,e){RefreshRow("+Container.VisibleIndex+");}" %>' 
                                                    ClientInstanceName='<%# "JtNum"+Container.VisibleIndex.ToString() %>'>
                                                     <ValidationSettings ValidationGroup="ValueValidationGroup" Display="Dynamic" ErrorTextPosition="Bottom">
                                                        <RegularExpression ErrorText="请输入数字！" ValidationExpression="^-?[1-9]+(\.\d+)?$|^-?0(\.\d+)?$|^-?[1-9]+[0-9]*(\.\d+)?$" />
                                                    </ValidationSettings>
                                                </dx:ASPxTextBox>                               
                                            </DataItemTemplate>        
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="单台单件<br />工序工时(秒)" FieldName="TjOpSec" Width="50px" VisibleIndex="15">
                                            <DataItemTemplate>
                                                <dx:ASPxTextBox ID="TjOpSec" Width="50px" runat="server" Value='<%# Eval("TjOpSec")%>' 
                                                    ClientInstanceName='<%# "TjOpSec"+Container.VisibleIndex.ToString() %>' Border-BorderWidth="0" ReadOnly="true">
                                                </dx:ASPxTextBox> 
                                            </DataItemTemplate>
                                            <PropertiesTextEdit DisplayFormatString="{0:N2}"></PropertiesTextEdit>
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="单件工<br />时(秒)" FieldName="JSec" Width="40px" VisibleIndex="16">
                                            <DataItemTemplate>
                                                <dx:ASPxTextBox ID="JSec" Width="40px" runat="server" Value='<%# Eval("JSec")%>' 
                                                    ClientInstanceName='<%# "JSec"+Container.VisibleIndex.ToString() %>' Border-BorderWidth="0" ReadOnly="true">
                                                </dx:ASPxTextBox> 
                                            </DataItemTemplate>
                                            <PropertiesTextEdit DisplayFormatString="{0:N2}"></PropertiesTextEdit>
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="单件工<br />时(小时)" FieldName="JHour" Width="55px" VisibleIndex="17">
                                            <DataItemTemplate>
                                                <dx:ASPxTextBox ID="JHour" Width="55px" runat="server" Value='<%# Eval("JHour")%>' 
                                                    ClientInstanceName='<%# "JHour"+Container.VisibleIndex.ToString() %>' Border-BorderWidth="0" ReadOnly="true">
                                                </dx:ASPxTextBox> 
                                            </DataItemTemplate>
                                             <PropertiesTextEdit DisplayFormatString="{0:N5}"></PropertiesTextEdit>
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="单台需<br />要人数" FieldName="col1" Width="40px" VisibleIndex="18">
                                            <Settings AllowCellMerge="False" />
                                            <DataItemTemplate>                
                                                <dx:ASPxTextBox ID="col1" Width="40px" runat="server" Value='<%# Eval("col1")%>'
                                                    ClientSideEvents-ValueChanged='<%# "function(s,e){RefreshRow("+Container.VisibleIndex+");}" %>' 
                                                    ClientInstanceName='<%# "col1"+Container.VisibleIndex.ToString() %>'>
                                                     <ValidationSettings ValidationGroup="ValueValidationGroup" Display="Dynamic" ErrorTextPosition="Bottom">
                                                        <RegularExpression ErrorText="请输入数字！" ValidationExpression="^-?[1-9]+(\.\d+)?$|^-?0(\.\d+)?$|^-?[1-9]+[0-9]*(\.\d+)?$" />
                                                    </ValidationSettings>
                                                </dx:ASPxTextBox>                                             
                                            </DataItemTemplate>   
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="本工序一人<br />操作台数" FieldName="col2" Width="60px" VisibleIndex="19">
                                            <Settings AllowCellMerge="False" />
                                            <DataItemTemplate>                
                                                <dx:ASPxTextBox ID="col2" Width="60px" runat="server" Value='<%# Eval("col2")%>'
                                                    ClientSideEvents-ValueChanged='<%# "function(s,e){RefreshRow("+Container.VisibleIndex+");}" %>' 
                                                    ClientInstanceName='<%# "col2"+Container.VisibleIndex.ToString() %>'>
                                                     <ValidationSettings ValidationGroup="ValueValidationGroup" Display="Dynamic" ErrorTextPosition="Bottom">
                                                        <RegularExpression ErrorText="请输入数字！" ValidationExpression="^-?[1-9]+(\.\d+)?$|^-?0(\.\d+)?$|^-?[1-9]+[0-9]*(\.\d+)?$" />
                                                    </ValidationSettings>
                                                </dx:ASPxTextBox>                                                    
                                            </DataItemTemplate>   
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="本产品设<br />备占用率" FieldName="EquipmentRate" Width="60px" VisibleIndex="19">
                                            <Settings AllowCellMerge="False" />
                                            <DataItemTemplate>                
                                                <dx:ASPxTextBox ID="EquipmentRate" Width="60px" runat="server" Value='<%# Eval("EquipmentRate")%>'
                                                    ClientSideEvents-ValueChanged='<%# "function(s,e){RefreshRow("+Container.VisibleIndex+");}" %>' 
                                                    ClientInstanceName='<%# "EquipmentRate"+Container.VisibleIndex.ToString() %>'>
                                                     <ValidationSettings ValidationGroup="ValueValidationGroup" Display="Dynamic" ErrorTextPosition="Bottom">
                                                        <RegularExpression ErrorText="请输入数字！" ValidationExpression="^-?[1-9]+(\.\d+)?$|^-?0(\.\d+)?$|^-?[1-9]+[0-9]*(\.\d+)?$" />
                                                    </ValidationSettings>
                                                </dx:ASPxTextBox>                                                    
                                            </DataItemTemplate>   
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="单台85%<br />产量" FieldName="col3" Width="40px" VisibleIndex="20">
                                            <DataItemTemplate>
                                                <dx:ASPxTextBox ID="col3" Width="40px" runat="server" Value='<%# Eval("col3")%>' 
                                                    ClientInstanceName='<%# "col3"+Container.VisibleIndex.ToString() %>' Border-BorderWidth="0" ReadOnly="true">
                                                </dx:ASPxTextBox> 
                                            </DataItemTemplate>
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="一人85%<br />产量" FieldName="col4" Width="40px" VisibleIndex="21">
                                            <DataItemTemplate>
                                                <dx:ASPxTextBox ID="col4" Width="40px" runat="server" Value='<%# Eval("col4")%>' 
                                                    ClientInstanceName='<%# "col4"+Container.VisibleIndex.ToString() %>' Border-BorderWidth="0" ReadOnly="true">
                                                </dx:ASPxTextBox> 
                                            </DataItemTemplate>
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="83%班<br />产量" FieldName="col5" Width="40px" VisibleIndex="22">
                                            <DataItemTemplate>
                                                <dx:ASPxTextBox ID="col5" Width="40px" runat="server" Value='<%# Eval("col5")%>' 
                                                    ClientInstanceName='<%# "col5"+Container.VisibleIndex.ToString() %>' Border-BorderWidth="0" ReadOnly="true">
                                                </dx:ASPxTextBox> 
                                            </DataItemTemplate>
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="单人报<br />工数量" FieldName="col6" Width="40px" VisibleIndex="23">
                                            <Settings AllowCellMerge="False" />
                                            <DataItemTemplate>                
                                                <dx:ASPxTextBox ID="col6" Width="40px" runat="server" Value='<%# Eval("col6")%>'
                                                    ClientSideEvents-ValueChanged='<%# "function(s,e){RefreshRow("+Container.VisibleIndex+");}" %>' 
                                                    ClientInstanceName='<%# "col6"+Container.VisibleIndex.ToString() %>'>
                                                     <ValidationSettings ValidationGroup="ValueValidationGroup" Display="Dynamic" ErrorTextPosition="Bottom">
                                                        <RegularExpression ErrorText="请输入数字！" ValidationExpression="^-?[1-9]+(\.\d+)?$|^-?0(\.\d+)?$|^-?[1-9]+[0-9]*(\.\d+)?$" />
                                                    </ValidationSettings>
                                                </dx:ASPxTextBox>                                                    
                                            </DataItemTemplate>   
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="单人产<br />出工时" FieldName="col7" Width="40px" VisibleIndex="24">
                                            <DataItemTemplate>
                                                <dx:ASPxTextBox ID="col7" Width="40px" runat="server" Value='<%# Eval("col7")%>' 
                                                    ClientInstanceName='<%# "col7"+Container.VisibleIndex.ToString() %>' Border-BorderWidth="0" ReadOnly="true">
                                                </dx:ASPxTextBox> 
                                            </DataItemTemplate>
                                            <PropertiesTextEdit DisplayFormatString="{0:N2}"></PropertiesTextEdit>
                                        </dx:GridViewDataTextColumn>
                                                                                
                                        <dx:GridViewDataTextColumn FieldName="numid" Width="0px" >
                                             <HeaderStyle CssClass="hidden" />
                                             <CellStyle CssClass="hidden"></CellStyle>
                                             <FooterCellStyle CssClass="hidden"></FooterCellStyle>
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="id" Width="0px">
                                             <HeaderStyle CssClass="hidden" />
                                             <CellStyle CssClass="hidden"></CellStyle>
                                             <FooterCellStyle CssClass="hidden"></FooterCellStyle>
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="GYGSNo" Width="0px">
                                             <HeaderStyle CssClass="hidden" />
                                             <CellStyle CssClass="hidden"></CellStyle>
                                             <FooterCellStyle CssClass="hidden"></FooterCellStyle>
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="UpdateById" Width="0px">
                                             <HeaderStyle CssClass="hidden" />
                                             <CellStyle CssClass="hidden"></CellStyle>
                                             <FooterCellStyle CssClass="hidden"></FooterCellStyle>
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="UpdateByName" Width="0px">
                                             <HeaderStyle CssClass="hidden" />
                                             <CellStyle CssClass="hidden"></CellStyle>
                                             <FooterCellStyle CssClass="hidden"></FooterCellStyle>
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="UpdateDate" Width="0px"> 
                                             <HeaderStyle CssClass="hidden" />
                                             <CellStyle CssClass="hidden"></CellStyle>
                                             <FooterCellStyle CssClass="hidden"></FooterCellStyle>
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="typeno" Width="0px" >
                                             <HeaderStyle CssClass="hidden" />
                                             <CellStyle CssClass="hidden"></CellStyle>
                                             <FooterCellStyle CssClass="hidden"></FooterCellStyle>
                                        </dx:GridViewDataTextColumn>

                                        <dx:GridViewDataTextColumn FieldName="" Caption=" " VisibleIndex="99" >
                                            <Settings AllowCellMerge="False" />
                                            <DataItemTemplate>                
                                                <dx:ASPxButton ID="btn" runat="server" Text="新增行"   CommandName="Add" ></dx:ASPxButton>          
                                            </DataItemTemplate>                        
                                        </dx:GridViewDataTextColumn>
                                    </Columns>                                                
                                    <Styles>
                                        <Header BackColor="#E4EFFA"  ></Header>        
                                        <SelectedRow BackColor="#FDF7D9"></SelectedRow>      
                                    </Styles>                                          
                                </dx:aspxgridview>

                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
        </div>

        <div id="div_yz" class="row  row-container" style="display:none;">
            <div class="panel panel-info">
                <div class="panel-heading" data-toggle="collapse" data-target="#GYGS_YZ">
                        <strong>工艺工时信息<font color="blue">-压铸</font></strong>
                </div>
                <div class="panel-body " id="GYGS_YZ">
                    <div class="col-xs-12 col-sm-12  col-md-12 col-lg-12">
                        <div style="padding: 2px 5px 5px 0px">                
                            
                        </div>
                        <asp:UpdatePanel runat="server" ID="p2" UpdateMode="Conditional" >
                            <ContentTemplate>
                                <asp:Button ID="btn_del_yz" runat="server" Text="删除" class="btn btn-default" style="width:60px; height:32px;"  OnClick="btn_del_yz_Click" />

                                <dx:aspxgridview ID="gv_d_yz" runat="server" AutoGenerateColumns="False" KeyFieldName="numid" Theme="MetropolisBlue" OnCustomCallback="gv_d_yz_CustomCallback" 
                                      OnRowCommand="gv_d_yz_RowCommand" ClientInstanceName="gv_d_yz"  EnableTheming="True"  >                                   
                                    <SettingsPager PageSize="1000"></SettingsPager>
                                    <Settings ShowFooter="True" />
                                    <SettingsBehavior AllowSelectByRowClick="True" AllowDragDrop="False" AllowSort="False" />
                                    <Columns>
                                        <dx:GridViewCommandColumn SelectAllCheckboxMode="Page" ShowClearFilterButton="true" ShowSelectCheckbox="true" Name="Sel" Width="30" VisibleIndex="1"></dx:GridViewCommandColumn>
                                        <dx:GridViewDataTextColumn Caption="项目号" FieldName="pgi_no" Width="75px" VisibleIndex="3">
                                            <Settings AllowCellMerge="False" />
                                            <DataItemTemplate>     
                                                 <table>
                                                    <tr>
                                                        <td>           
                                                            <dx:ASPxTextBox ID="pgi_no" Width="75px" runat="server" Value='<%# Eval("pgi_no")%>' 
                                                                ClientInstanceName='<%# "pgi_no_yz"+Container.VisibleIndex.ToString() %>' ></dx:ASPxTextBox>   
                                                        </td>
                                                        <td><i id="pgi_no_i_yz_<%#Container.VisibleIndex.ToString() %>" class="fa fa-search" onclick="GetPgi_Product_D(<%# Container.VisibleIndex %>,'yz')"></i></td>
                                                    </tr>
                                                </table>                  
                                            </DataItemTemplate>   
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="工艺流程" FieldName="pgi_no_t" Width="75px" VisibleIndex="3">
                                            <Settings AllowCellMerge="False" />
                                            <DataItemTemplate>     
                                                 <dx:ASPxTextBox ID="pgi_no_t" Width="75px" runat="server" Value='<%# Eval("pgi_no_t")%>' 
                                                                ClientInstanceName='<%# "pgi_no_t_yz"+Container.VisibleIndex.ToString() %>' ></dx:ASPxTextBox>                    
                                            </DataItemTemplate>   
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="工序号" FieldName="op" Width="45px" VisibleIndex="4">
                                            <Settings AllowCellMerge="False" />
                                            <DataItemTemplate>                
                                                <dx:ASPxTextBox ID="op" Width="45px" runat="server" Value='<%# Eval("op")%>' ></dx:ASPxTextBox>                
                                            </DataItemTemplate>   
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="工序名称" FieldName="op_desc" Width="130px" VisibleIndex="5">
                                            <Settings AllowCellMerge="False" />
                                            <DataItemTemplate>                
                                                <dx:ASPxTextBox ID="op_desc" Width="130px" runat="server" Value='<%# Eval("op_desc")%>' ></dx:ASPxTextBox>                
                                            </DataItemTemplate>        
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="工序说明" FieldName="op_remark" Width="130px" VisibleIndex="6">
                                            <Settings AllowCellMerge="False" />
                                            <DataItemTemplate>                
                                                <dx:ASPxTextBox ID="op_remark" Width="130px" runat="server" Value='<%# Eval("op_remark")%>' ></dx:ASPxTextBox>                
                                            </DataItemTemplate>        
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="设备<br />(工作中心名称)" FieldName="gzzx_desc" Width="100px" VisibleIndex="7">
                                            <Settings AllowCellMerge="False" />
                                            <DataItemTemplate>
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <dx:ASPxTextBox ID="gzzx_desc" Width="100px" runat="server" Value='<%# Eval("gzzx_desc")%>' 
                                                                ClientInstanceName='<%# "gzzx_desc_yz"+Container.VisibleIndex.ToString() %>' Border-BorderWidth="0"   ReadOnly="true">
                                                            </dx:ASPxTextBox>
                                                        </td>
                                                        <td><i id="gzzx_i_yz_<%#Container.VisibleIndex.ToString() %>" class="fa fa-search" onclick="Get_wkzx(<%# Container.VisibleIndex %>,'yz')"></i></td>
                                                    </tr>
                                                </table>       
                                            </DataItemTemplate>
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="工作中<br />心代码" FieldName="gzzx" Width="40px" VisibleIndex="8">
                                             <DataItemTemplate>
                                                <dx:ASPxTextBox ID="gzzx" Width="40px" runat="server" Value='<%# Eval("gzzx")%>' 
                                                    ClientInstanceName='<%# "gzzx_yz"+Container.VisibleIndex.ToString() %>' Border-BorderWidth="0" ReadOnly="true">
                                                </dx:ASPxTextBox> 
                                            </DataItemTemplate>
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="是否报<br />工(Y/N)" FieldName="IsBg" Width="35px" VisibleIndex="9"></dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="压铸每模<br />重量(kg)" FieldName="weights" Width="60px" VisibleIndex="10">
                                            <Settings AllowCellMerge="False"/>
                                            <DataItemTemplate>
                                                <dx:ASPxTextBox ID="weights" Width="60px" runat="server" Value='<%# Eval("weights")%>' 
                                                    ClientInstanceName='<%# "weights_yz"+Container.VisibleIndex.ToString() %>'>
                                                     <ValidationSettings ValidationGroup="ValueValidationGroup" Display="Dynamic" ErrorTextPosition="Bottom">
                                                        <RegularExpression ErrorText="请输入数字！" ValidationExpression="^-?[1-9]+(\.\d+)?$|^-?0(\.\d+)?$|^-?[1-9]+[0-9]*(\.\d+)?$" />
                                                    </ValidationSettings>
                                                </dx:ASPxTextBox>
                                            </DataItemTemplate>        
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="每模<br />穴数" FieldName="acupoints" Width="40px" VisibleIndex="10">
                                            <Settings AllowCellMerge="False"/>
                                            <DataItemTemplate>
                                                <dx:ASPxTextBox ID="acupoints" Width="40px" runat="server" Value='<%# Eval("acupoints")%>' 
                                                    ClientInstanceName='<%# "acupoints_yz"+Container.VisibleIndex.ToString() %>'>
                                                     <ValidationSettings ValidationGroup="ValueValidationGroup" Display="Dynamic" ErrorTextPosition="Bottom">
                                                        <RegularExpression ErrorText="请输入数字！" ValidationExpression="^-?[1-9]+(\.\d+)?$|^-?0(\.\d+)?$|^-?[1-9]+[0-9]*(\.\d+)?$" />
                                                    </ValidationSettings>
                                                </dx:ASPxTextBox>
                                            </DataItemTemplate>        
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="每小时设<br />备产能(kg)" FieldName="capacity" Width="60px" VisibleIndex="10">
                                            <Settings AllowCellMerge="False"/>
                                            <DataItemTemplate>
                                                <dx:ASPxTextBox ID="capacity" Width="60px" runat="server" Value='<%# Eval("capacity")%>' 
                                                    ClientInstanceName='<%# "capacity_yz"+Container.VisibleIndex.ToString() %>'>
                                                     <ValidationSettings ValidationGroup="ValueValidationGroup" Display="Dynamic" ErrorTextPosition="Bottom">
                                                        <RegularExpression ErrorText="请输入数字！" ValidationExpression="^-?[1-9]+(\.\d+)?$|^-?0(\.\d+)?$|^-?[1-9]+[0-9]*(\.\d+)?$" />
                                                    </ValidationSettings>
                                                </dx:ASPxTextBox>
                                            </DataItemTemplate>        
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="每次加<br />工数量" FieldName="JgNum" Width="40px" VisibleIndex="10">
                                            <Settings AllowCellMerge="False"/>
                                            <DataItemTemplate>
                                                <dx:ASPxTextBox ID="JgNum" Width="40px" runat="server" Value='<%# Eval("JgNum")%>' 
                                                    ClientSideEvents-ValueChanged='<%# "function(s,e){RefreshRow_yz("+Container.VisibleIndex+");}" %>' 
                                                    ClientInstanceName='<%# "JgNum_yz"+Container.VisibleIndex.ToString() %>'>
                                                     <ValidationSettings ValidationGroup="ValueValidationGroup" Display="Dynamic" ErrorTextPosition="Bottom">
                                                        <RegularExpression ErrorText="请输入数字！" ValidationExpression="^-?[1-9]+(\.\d+)?$|^-?0(\.\d+)?$|^-?[1-9]+[0-9]*(\.\d+)?$" />
                                                    </ValidationSettings>
                                                </dx:ASPxTextBox>
                                            </DataItemTemplate>        
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="加工时<br />长(秒)" FieldName="JgSec" Width="40px" VisibleIndex="12">
                                            <Settings AllowCellMerge="False" />
                                            <DataItemTemplate>                
                                                <dx:ASPxTextBox ID="JgSec" Width="40px" runat="server" Value='<%# Eval("JgSec")%>'
                                                    ClientSideEvents-ValueChanged='<%# "function(s,e){RefreshRow_yz("+Container.VisibleIndex+");}" %>' 
                                                    ClientInstanceName='<%# "JgSec_yz"+Container.VisibleIndex.ToString() %>'>
                                                     <ValidationSettings ValidationGroup="ValueValidationGroup" Display="Dynamic" ErrorTextPosition="Bottom">
                                                        <RegularExpression ErrorText="请输入数字！" ValidationExpression="^-?[1-9]+(\.\d+)?$|^-?0(\.\d+)?$|^-?[1-9]+[0-9]*(\.\d+)?$" />
                                                    </ValidationSettings>
                                                </dx:ASPxTextBox>                
                                            </DataItemTemplate>        
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="设备等待<br />时间(秒)" FieldName="WaitSec" Width="50px" VisibleIndex="12">
                                            <Settings AllowCellMerge="False" />
                                            <DataItemTemplate>                
                                                <dx:ASPxTextBox ID="WaitSec" Width="50px" runat="server" Value='<%# Eval("WaitSec")%>'
                                                    ClientSideEvents-ValueChanged='<%# "function(s,e){RefreshRow_yz("+Container.VisibleIndex+");}" %>' 
                                                    ClientInstanceName='<%# "WaitSec_yz"+Container.VisibleIndex.ToString() %>'>
                                                     <ValidationSettings ValidationGroup="ValueValidationGroup" Display="Dynamic" ErrorTextPosition="Bottom">
                                                        <RegularExpression ErrorText="请输入数字！" ValidationExpression="^-?[1-9]+(\.\d+)?$|^-?0(\.\d+)?$|^-?[1-9]+[0-9]*(\.\d+)?$" />
                                                    </ValidationSettings>
                                                </dx:ASPxTextBox>                
                                            </DataItemTemplate>        
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="装夹时<br />间(秒)" FieldName="ZjSecc" Width="40px" VisibleIndex="13">
                                            <Settings AllowCellMerge="False" />
                                            <DataItemTemplate>                
                                                <dx:ASPxTextBox ID="ZjSecc" Width="40px" runat="server" Value='<%# Eval("ZjSecc")%>'
                                                    ClientSideEvents-ValueChanged='<%# "function(s,e){RefreshRow_yz("+Container.VisibleIndex+");}" %>' 
                                                    ClientInstanceName='<%# "ZjSecc_yz"+Container.VisibleIndex.ToString() %>'>
                                                     <ValidationSettings ValidationGroup="ValueValidationGroup" Display="Dynamic" ErrorTextPosition="Bottom">
                                                        <RegularExpression ErrorText="请输入数字！" ValidationExpression="^-?[1-9]+(\.\d+)?$|^-?0(\.\d+)?$|^-?[1-9]+[0-9]*(\.\d+)?$" />
                                                    </ValidationSettings>
                                                </dx:ASPxTextBox>                
                                            </DataItemTemplate>        
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="机器<br />台数" FieldName="JtNum" Width="40px" VisibleIndex="14">
                                            <Settings AllowCellMerge="False" />
                                            <DataItemTemplate>                
                                                <dx:ASPxTextBox ID="JtNum" Width="40px" runat="server" Value='<%# Eval("JtNum")%>'
                                                    ClientSideEvents-ValueChanged='<%# "function(s,e){RefreshRow_yz("+Container.VisibleIndex+");}" %>' 
                                                    ClientInstanceName='<%# "JtNum_yz"+Container.VisibleIndex.ToString() %>'>
                                                     <ValidationSettings ValidationGroup="ValueValidationGroup" Display="Dynamic" ErrorTextPosition="Bottom">
                                                        <RegularExpression ErrorText="请输入数字！" ValidationExpression="^-?[1-9]+(\.\d+)?$|^-?0(\.\d+)?$|^-?[1-9]+[0-9]*(\.\d+)?$" />
                                                    </ValidationSettings>
                                                </dx:ASPxTextBox>                               
                                            </DataItemTemplate>        
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="单台单件<br />工序工时(秒)" FieldName="TjOpSec" Width="50px" VisibleIndex="15">
                                            <DataItemTemplate>
                                                <dx:ASPxTextBox ID="TjOpSec" Width="50px" runat="server" Value='<%# Eval("TjOpSec")%>' 
                                                    ClientInstanceName='<%# "TjOpSec_yz"+Container.VisibleIndex.ToString() %>' Border-BorderWidth="0" ReadOnly="true">
                                                </dx:ASPxTextBox> 
                                            </DataItemTemplate>
                                            <PropertiesTextEdit DisplayFormatString="{0:N2}"></PropertiesTextEdit>
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="单件工<br />时(秒)" FieldName="JSec" Width="40px" VisibleIndex="16">
                                            <DataItemTemplate>
                                                <dx:ASPxTextBox ID="JSec" Width="40px" runat="server" Value='<%# Eval("JSec")%>' 
                                                    ClientInstanceName='<%# "JSec_yz"+Container.VisibleIndex.ToString() %>' Border-BorderWidth="0" ReadOnly="true">
                                                </dx:ASPxTextBox> 
                                            </DataItemTemplate>
                                            <PropertiesTextEdit DisplayFormatString="{0:N2}"></PropertiesTextEdit>
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="单件工<br />时(小时)" FieldName="JHour" Width="55px" VisibleIndex="17">
                                            <DataItemTemplate>
                                                <dx:ASPxTextBox ID="JHour" Width="55px" runat="server" Value='<%# Eval("JHour")%>' 
                                                    ClientInstanceName='<%# "JHour_yz"+Container.VisibleIndex.ToString() %>' Border-BorderWidth="0" ReadOnly="true">
                                                </dx:ASPxTextBox> 
                                            </DataItemTemplate>
                                             <PropertiesTextEdit DisplayFormatString="{0:N5}"></PropertiesTextEdit>
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="单台需<br />要人数" FieldName="col1" Width="40px" VisibleIndex="18">
                                            <Settings AllowCellMerge="False" />
                                            <DataItemTemplate>                
                                                <dx:ASPxTextBox ID="col1" Width="40px" runat="server" Value='<%# Eval("col1")%>'
                                                    ClientSideEvents-ValueChanged='<%# "function(s,e){RefreshRow_yz("+Container.VisibleIndex+");}" %>' 
                                                    ClientInstanceName='<%# "col1_yz"+Container.VisibleIndex.ToString() %>'>
                                                     <ValidationSettings ValidationGroup="ValueValidationGroup" Display="Dynamic" ErrorTextPosition="Bottom">
                                                        <RegularExpression ErrorText="请输入数字！" ValidationExpression="^-?[1-9]+(\.\d+)?$|^-?0(\.\d+)?$|^-?[1-9]+[0-9]*(\.\d+)?$" />
                                                    </ValidationSettings>
                                                </dx:ASPxTextBox>                                             
                                            </DataItemTemplate>   
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="本工序一人<br />操作台数" FieldName="col2" Width="60px" VisibleIndex="19">
                                            <Settings AllowCellMerge="False" />
                                            <DataItemTemplate>                
                                                <dx:ASPxTextBox ID="col2" Width="60px" runat="server" Value='<%# Eval("col2")%>'
                                                    ClientSideEvents-ValueChanged='<%# "function(s,e){RefreshRow_yz("+Container.VisibleIndex+");}" %>' 
                                                    ClientInstanceName='<%# "col2_yz"+Container.VisibleIndex.ToString() %>'>
                                                     <ValidationSettings ValidationGroup="ValueValidationGroup" Display="Dynamic" ErrorTextPosition="Bottom">
                                                        <RegularExpression ErrorText="请输入数字！" ValidationExpression="^-?[1-9]+(\.\d+)?$|^-?0(\.\d+)?$|^-?[1-9]+[0-9]*(\.\d+)?$" />
                                                    </ValidationSettings>
                                                </dx:ASPxTextBox>                                                    
                                            </DataItemTemplate>   
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="本产品设<br />备占用率" FieldName="EquipmentRate" Width="50px" VisibleIndex="19">
                                            <Settings AllowCellMerge="False" />
                                            <DataItemTemplate>                
                                                <dx:ASPxTextBox ID="EquipmentRate" Width="50px" runat="server" Value='<%# Eval("EquipmentRate")%>'
                                                    ClientSideEvents-ValueChanged='<%# "function(s,e){RefreshRow_yz("+Container.VisibleIndex+");}" %>' 
                                                    ClientInstanceName='<%# "EquipmentRate_yz"+Container.VisibleIndex.ToString() %>'>
                                                     <ValidationSettings ValidationGroup="ValueValidationGroup" Display="Dynamic" ErrorTextPosition="Bottom">
                                                        <RegularExpression ErrorText="请输入数字！" ValidationExpression="^-?[1-9]+(\.\d+)?$|^-?0(\.\d+)?$|^-?[1-9]+[0-9]*(\.\d+)?$" />
                                                    </ValidationSettings>
                                                </dx:ASPxTextBox>                                                    
                                            </DataItemTemplate>   
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="单台83%<br />产量" FieldName="col3" Width="40px" VisibleIndex="20">
                                            <DataItemTemplate>
                                                <dx:ASPxTextBox ID="col3" Width="40px" runat="server" Value='<%# Eval("col3")%>' 
                                                    ClientInstanceName='<%# "col3_yz"+Container.VisibleIndex.ToString() %>' Border-BorderWidth="0" ReadOnly="true">
                                                </dx:ASPxTextBox> 
                                            </DataItemTemplate>
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="一人83%<br />产量" FieldName="col4" Width="40px" VisibleIndex="21">
                                            <DataItemTemplate>
                                                <dx:ASPxTextBox ID="col4" Width="40px" runat="server" Value='<%# Eval("col4")%>' 
                                                    ClientInstanceName='<%# "col4_yz"+Container.VisibleIndex.ToString() %>' Border-BorderWidth="0" ReadOnly="true">
                                                </dx:ASPxTextBox> 
                                            </DataItemTemplate>
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="83%班<br />产量" FieldName="col5" Width="40px" VisibleIndex="22">
                                            <DataItemTemplate>
                                                <dx:ASPxTextBox ID="col5" Width="40px" runat="server" Value='<%# Eval("col5")%>' 
                                                    ClientInstanceName='<%# "col5_yz"+Container.VisibleIndex.ToString() %>' Border-BorderWidth="0" ReadOnly="true">
                                                </dx:ASPxTextBox> 
                                            </DataItemTemplate>
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="单人报<br />工数量" FieldName="col6" Width="50px" VisibleIndex="23">
                                            <Settings AllowCellMerge="False" />
                                            <DataItemTemplate>                
                                                <dx:ASPxTextBox ID="col6" Width="50px" runat="server" Value='<%# Eval("col6")%>'
                                                    ClientSideEvents-ValueChanged='<%# "function(s,e){RefreshRow_yz("+Container.VisibleIndex+");}" %>' 
                                                    ClientInstanceName='<%# "col6_yz"+Container.VisibleIndex.ToString() %>'>
                                                     <ValidationSettings ValidationGroup="ValueValidationGroup" Display="Dynamic" ErrorTextPosition="Bottom">
                                                        <RegularExpression ErrorText="请输入数字！" ValidationExpression="^-?[1-9]+(\.\d+)?$|^-?0(\.\d+)?$|^-?[1-9]+[0-9]*(\.\d+)?$" />
                                                    </ValidationSettings>
                                                </dx:ASPxTextBox>                                                    
                                            </DataItemTemplate>   
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="单人产<br />出工时" FieldName="col7" Width="40px" VisibleIndex="24">
                                            <DataItemTemplate>
                                                <dx:ASPxTextBox ID="col7" Width="40px" runat="server" Value='<%# Eval("col7")%>' 
                                                    ClientInstanceName='<%# "col7_yz"+Container.VisibleIndex.ToString() %>' Border-BorderWidth="0" ReadOnly="true">
                                                </dx:ASPxTextBox> 
                                            </DataItemTemplate>
                                            <PropertiesTextEdit DisplayFormatString="{0:N2}"></PropertiesTextEdit>
                                        </dx:GridViewDataTextColumn>
                                                                                
                                        <dx:GridViewDataTextColumn FieldName="numid" Width="0px" >
                                             <HeaderStyle CssClass="hidden" />
                                             <CellStyle CssClass="hidden"></CellStyle>
                                             <FooterCellStyle CssClass="hidden"></FooterCellStyle>
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="id" Width="0px">
                                             <HeaderStyle CssClass="hidden" />
                                             <CellStyle CssClass="hidden"></CellStyle>
                                             <FooterCellStyle CssClass="hidden"></FooterCellStyle>
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="GYGSNo" Width="0px">
                                             <HeaderStyle CssClass="hidden" />
                                             <CellStyle CssClass="hidden"></CellStyle>
                                             <FooterCellStyle CssClass="hidden"></FooterCellStyle>
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="UpdateById" Width="0px">
                                             <HeaderStyle CssClass="hidden" />
                                             <CellStyle CssClass="hidden"></CellStyle>
                                             <FooterCellStyle CssClass="hidden"></FooterCellStyle>
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="UpdateByName" Width="0px">
                                             <HeaderStyle CssClass="hidden" />
                                             <CellStyle CssClass="hidden"></CellStyle>
                                             <FooterCellStyle CssClass="hidden"></FooterCellStyle>
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="UpdateDate" Width="0px"> 
                                             <HeaderStyle CssClass="hidden" />
                                             <CellStyle CssClass="hidden"></CellStyle>
                                             <FooterCellStyle CssClass="hidden"></FooterCellStyle>
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="typeno" Width="0px" >
                                             <HeaderStyle CssClass="hidden" />
                                             <CellStyle CssClass="hidden"></CellStyle>
                                             <FooterCellStyle CssClass="hidden"></FooterCellStyle>
                                        </dx:GridViewDataTextColumn>

                                        <dx:GridViewDataTextColumn FieldName="" Caption=" " VisibleIndex="99" >
                                            <Settings AllowCellMerge="False" />
                                            <DataItemTemplate>                
                                                <dx:ASPxButton ID="btn" runat="server" Text="新增行"   CommandName="Add" ></dx:ASPxButton>          
                                            </DataItemTemplate>                        
                                        </dx:GridViewDataTextColumn>
                                    </Columns>                                                
                                    <Styles>
                                        <Header BackColor="#E4EFFA"  ></Header>        
                                        <SelectedRow BackColor="#FDF7D9"></SelectedRow>      
                                    </Styles>                                          
                                </dx:aspxgridview>

                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
        </div>

        <div class="row row-container" >
            <div class="panel panel-info">
                <div class="panel-heading" data-toggle="collapse" data-target="#bgjl"> 
                    <%--<strong>变更说明</strong>--%>
                </div>
                <div class="panel-body " id="bgjl">
                    <table border="0"  width="100%"  >
                        <tr>
                            <td width="100px" ><label>变更说明：</label></td>
                            <td>
                                <textarea id="comment" cols="20" rows="2"  class="form-control" onchange="setComment(this.value)" ></textarea>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>

        <div class="row row-container">
            <div class="panel panel-info">
                <div class="panel-heading" data-toggle="collapse" data-target="#CZRZ"> 
                </div>
                <div class="panel-body ">
                    <table border="0"  width="100%"  >
                        <tr>
                            <td width="100px" ><label>处理意见：</label></td>
                            <td>
                                <textarea id="comment" cols="20" rows="2" placeholder="请在此处输入处理意见" class="form-control" onchange="setComment(this.value)" ></textarea>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>

    </div>

</asp:Content>

<%--<dx:aspxgridview ID="Aspxgridview1" runat="server" AutoGenerateColumns="False" KeyFieldName="numid" Theme="MetropolisBlue" OnCustomCallback="gv_d_CustomCallback" 
                                      OnRowCommand="gv_d_RowCommand" ClientInstanceName="gv_d"  EnableTheming="True"  >                                   
                                    <SettingsPager PageSize="1000"></SettingsPager>
                                    <Settings ShowFooter="True" />
                                    <SettingsBehavior AllowSelectByRowClick="True" AllowDragDrop="False" AllowSort="False" />
                                    <Columns>
                                        <dx:GridViewCommandColumn SelectAllCheckboxMode="Page" ShowClearFilterButton="true" ShowSelectCheckbox="true" Name="Sel" Width="40" VisibleIndex="1"></dx:GridViewCommandColumn>
                                        <dx:GridViewDataTextColumn Caption="工艺段" FieldName="typeno" Width="60px" VisibleIndex="2"></dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="工艺流程" FieldName="pgi_no" Width="80px" VisibleIndex="3"></dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="工序号" FieldName="op" Width="60px" VisibleIndex="4">
                                            <Settings AllowCellMerge="False" />
                                            <DataItemTemplate>                
                                                <dx:ASPxTextBox ID="op" Width="60px" runat="server" Value='<%# Eval("op")%>' ></dx:ASPxTextBox>                
                                            </DataItemTemplate>   
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="工序名称" FieldName="op_desc" Width="140px" VisibleIndex="5">
                                            <Settings AllowCellMerge="False" />
                                            <DataItemTemplate>                
                                                <dx:ASPxTextBox ID="op_desc" Width="140px" runat="server" Value='<%# Eval("op_desc")%>' ></dx:ASPxTextBox>                
                                            </DataItemTemplate>        
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="工序说明" FieldName="op_remark" Width="140px" VisibleIndex="6">
                                            <Settings AllowCellMerge="False" />
                                            <DataItemTemplate>                
                                                <dx:ASPxTextBox ID="op_remark" Width="140px" runat="server" Value='<%# Eval("op_remark")%>' ></dx:ASPxTextBox>                
                                            </DataItemTemplate>        
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="设备" FieldName="gzzx_desc" Width="80px" VisibleIndex="7"></dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="工作中心<br />代码" FieldName="gzzx" Width="50px" VisibleIndex="8"></dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="是否报工<br />(Y/N)" FieldName="IsBg" Width="50px" VisibleIndex="9"></dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="每次加工<br />数量" FieldName="JgNum" Width="50px" VisibleIndex="10">
                                            <Settings AllowCellMerge="False"/>
                                            <DataItemTemplate>
                                                <dx:ASPxTextBox ID="JgNum" Width="50px" runat="server" Value='<%# Eval("JgNum")%>' AutoPostBack="true" OnValueChanged="JgNum_ValueChanged"></dx:ASPxTextBox>
                                            </DataItemTemplate>        
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="加工时长<br />(秒)" FieldName="JgSec" Width="50px" VisibleIndex="12">
                                            <Settings AllowCellMerge="False" />
                                            <DataItemTemplate>                
                                                <dx:ASPxTextBox ID="JgSec" Width="50px" runat="server" Value='<%# Eval("JgSec")%>' AutoPostBack="true" OnValueChanged="JgNum_ValueChanged" ></dx:ASPxTextBox>                
                                            </DataItemTemplate>        
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="设备等待<br />时间(秒)" FieldName="WaitSec" Width="50px" VisibleIndex="12">
                                            <Settings AllowCellMerge="False" />
                                            <DataItemTemplate>                
                                                <dx:ASPxTextBox ID="WaitSec" Width="50px" runat="server" Value='<%# Eval("WaitSec")%>' AutoPostBack="true" OnValueChanged="JgNum_ValueChanged" ></dx:ASPxTextBox>                
                                            </DataItemTemplate>        
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="装夹时间<br />(秒)" FieldName="ZjSecc" Width="50px" VisibleIndex="13">
                                            <Settings AllowCellMerge="False" />
                                            <DataItemTemplate>                
                                                <dx:ASPxTextBox ID="ZjSecc" Width="50px" runat="server" Value='<%# Eval("ZjSecc")%>' AutoPostBack="true" OnValueChanged="JgNum_ValueChanged" ></dx:ASPxTextBox>                
                                            </DataItemTemplate>        
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="机器台数" FieldName="JtNum" Width="50px" VisibleIndex="14">
                                            <Settings AllowCellMerge="False" />
                                            <DataItemTemplate>                
                                                <dx:ASPxTextBox ID="JtNum" Width="50px" runat="server" Value='<%# Eval("JtNum")%>' AutoPostBack="true" OnValueChanged="JgNum_ValueChanged" ></dx:ASPxTextBox>                
                                            </DataItemTemplate>        
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="单台单件<br />工序工时(秒)" FieldName="TjOpSec" Width="60px" VisibleIndex="15">
                                            <PropertiesTextEdit DisplayFormatString="{0:N2}"></PropertiesTextEdit>
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="单件工时<br />(秒)" FieldName="JSec" Width="50px" VisibleIndex="16">
                                            <PropertiesTextEdit DisplayFormatString="{0:N2}"></PropertiesTextEdit>
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="单件工时<br />(小时)" FieldName="JHour" Width="50px" VisibleIndex="17">
                                             <PropertiesTextEdit DisplayFormatString="{0:N5}"></PropertiesTextEdit>
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="单台<br />需要人数" FieldName="col1" Width="50px" VisibleIndex="18">
                                            <Settings AllowCellMerge="False" />
                                            <DataItemTemplate>                
                                                <dx:ASPxTextBox ID="col1" Width="50px" runat="server" Value='<%# Eval("col1")%>' AutoPostBack="true"  OnValueChanged="JgNum_ValueChanged"></dx:ASPxTextBox>                
                                            </DataItemTemplate>   
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="本工序一人<br />操作台数" FieldName="col2" Width="60px" VisibleIndex="19">
                                            <Settings AllowCellMerge="False" />
                                            <DataItemTemplate>                
                                                <dx:ASPxTextBox ID="col2" Width="60px" runat="server" Value='<%# Eval("col2")%>' AutoPostBack="true"  OnValueChanged="JgNum_ValueChanged"></dx:ASPxTextBox>                
                                            </DataItemTemplate>   
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="单台85%<br />产量" FieldName="col3" Width="60px" VisibleIndex="20"> </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="一人85%<br />产量" FieldName="col4" Width="60px" VisibleIndex="21"></dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="整线班产量" FieldName="col5" Width="60px" VisibleIndex="22"></dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="完工工时" FieldName="FinishHour" Width="50px" VisibleIndex="23">
                                            <PropertiesTextEdit DisplayFormatString="{0:N2}"></PropertiesTextEdit>
                                        </dx:GridViewDataTextColumn>
                                        
                                        

                                        <dx:GridViewDataTextColumn FieldName="" Caption=" " VisibleIndex="30" >
                                            <Settings AllowCellMerge="False" />
                                            <DataItemTemplate>                
                                                <dx:ASPxButton ID="btn" runat="server" Text="新增行"   CommandName="Add"  ></dx:ASPxButton>          
                                            </DataItemTemplate>                        
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="numid" Width="0px">
                                             <HeaderStyle CssClass="hidden" />
                                             <CellStyle CssClass="hidden"></CellStyle>
                                             <FooterCellStyle CssClass="hidden"></FooterCellStyle>
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="id" Width="0px">
                                             <HeaderStyle CssClass="hidden" />
                                             <CellStyle CssClass="hidden"></CellStyle>
                                             <FooterCellStyle CssClass="hidden"></FooterCellStyle>
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="GYGSNo" Width="0px">
                                             <HeaderStyle CssClass="hidden" />
                                             <CellStyle CssClass="hidden"></CellStyle>
                                             <FooterCellStyle CssClass="hidden"></FooterCellStyle>
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="UpdateById" Width="0px">
                                             <HeaderStyle CssClass="hidden" />
                                             <CellStyle CssClass="hidden"></CellStyle>
                                             <FooterCellStyle CssClass="hidden"></FooterCellStyle>
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="UpdateByName" Width="0px">
                                             <HeaderStyle CssClass="hidden" />
                                             <CellStyle CssClass="hidden"></CellStyle>
                                             <FooterCellStyle CssClass="hidden"></FooterCellStyle>
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="UpdateDate" Width="0px"> 
                                             <HeaderStyle CssClass="hidden" />
                                             <CellStyle CssClass="hidden"></CellStyle>
                                             <FooterCellStyle CssClass="hidden"></FooterCellStyle>
                                        </dx:GridViewDataTextColumn>
                                    </Columns>                                                
                                    <Styles>
                                        <Header BackColor="#E4EFFA"  ></Header>        
                                        <SelectedRow BackColor="#FDF7D9"></SelectedRow>      
                                    </Styles>                                          
                                </dx:aspxgridview>--%>

<%--<dx:GridViewDataTextColumn Caption="单台100%<br />产量" FieldName="TSumNum" Width="70px" VisibleIndex="18"></dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="单台80%<br />产量" FieldName="TPec8Num" Width="70px" VisibleIndex="19"></dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="操作人数" FieldName="OpNum" Width="70px" VisibleIndex="25">
                                            <Settings AllowCellMerge="False" />
                                            <DataItemTemplate>                
                                                <dx:ASPxTextBox ID="OpNum" Width="70px" runat="server" Value='<%# Eval("OpNum")%>' ></dx:ASPxTextBox>                
                                            </DataItemTemplate>        
                                        </dx:GridViewDataTextColumn>--%>