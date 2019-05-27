<%@ Page Title="请购申请单(PR)" Language="C#" AutoEventWireup="true" CodeFile="PUR_PR.aspx.cs" Inherits="PUR_PR" MaintainScrollPositionOnPostback="True" ValidateRequest="true" %>
 
<%@ Register assembly="DevExpress.Web.v17.2, Version=17.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web" tagprefix="dx" %>
 
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script src="../../../Content/js/jquery.min.js"></script>
    <script src="../../../Content/js/bootstrap.min.js"></script>
    <script src="../../Content/js/layer/layer.js"></script>
    <link href="../../Content/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../../../Content/js/plugins/bootstrap-select/css/bootstrap-select.min.css" rel="stylesheet" />
    <link href="../../Content/css/custom.css?t=20190516" rel="stylesheet" />
    <link href="/Content/css/font-awesome.min.css" rel="stylesheet" type="text/css" />
    <style>
        body {
            overflow-x: auto;
            overflow-y: hidden;
        }
        hidden {
            display: none;
        }
    </style>

    <script src="../../../Content/js/plugins/bootstrap-select/js/bootstrap-select.min.js"></script>
    <script src="../../Scripts/RFlow.js"></script>
    <script type="text/javascript">
        var paramMap;
        $(document).ready(function () {
            $("#mestitle").html("【请购申请单(PR)】<a href='/userguide/PRGuide.pps' target='_blank' class='h5' style='color:red'>使用说明</a>"); 
             
            SetButtons();

            getTotalPrice();                                                
            //绑定物料信息
            getMatInfo();
            //工厂选择更改
            $("select[id*='domain']").change(function(){
                bindSelect("applydept");
                $("#deptm").val("");
                $("#deptmfg").val("");
            });

            //申请部门
            $("select[id*='applydept']").change(function(){
                var domain=$("#domain").val();
                var dept=$("#applydept").val();
                getDeptLeader(domain,dept);              
            });

            //采购类别
            $("select[id*='prtype']").change(function(){
                var prtype=$("#prtype").val();
                grid.PerformCallback(prtype);
            });
                        
            //获取参数名值对集合Json格式
            var url =window.parent.document.URL;
            var viewmode="";
            paramMap = getURLParams(url); 
            if(paramMap.mode!=NaN&&paramMap.mode!=""&&paramMap.mode!=undefined)
            {
                viewmode=paramMap.mode; 
                //$("#btnAddDetl").css("display","none");
                //$("#btnDelete").css("display","none");                
                DisableButtons();//禁用流程按钮
                SetControlStatus(fieldSet);
                
            }  
            
            appendSearch();
            
        });// end ready

        //添加选取、查看历史单价功能
        function appendSearch(){
            $.each($("[id*=_historyprice]"), function (i, obj) {                 
                if($(obj).parent().attr("id")!="div"+i){

                    var ctype="";
                    var id=$(obj).attr("id");               
               
                    $(obj).after("<div id='div"+i+"' width='100px' class='input-group'></div>");
                    //$(obj).css("width","50px").css("padding","0px 0px 0px 0px").addClass('form-control input-sm').appendTo($("#div"+i));
                    $(obj).css("width","50px").css("padding","0px 0px 0px 0px").addClass('input-sm').appendTo($("#div"+i));
                    //$(obj).after("<span  class='input-group-addon input-sm' onclick='openwind(\""+id+"\")' value='?' title=\"查看历史单价\">");
                    $(obj).after("<label class='input-group-addon input-sm' style='background-color:transparent;border:none;' onclick='openwind(\""+id+"\")'  title=\"查看历史单价\"><i class=\"fa fa-search\"></i></label>");
                }
            });
        }
        //查看or选择历史单价
        function openwind(id){  
            var ctrl0="";
            var ctrl1="";
            var ctrl2=id;
            debugger;
            var wlmc=$("#"+id).parent().parent().parent().find("input[id*=wlmc]").val();//id.replace("notax_historyprice","wlmc");
            var ms=$("#"+id).parent().parent().parent().find("input[id*=ms]").val()//id.replace("notax_historyprice","ms");
            var readonly=$("#"+id).parent().parent().parent().find("input[id*=wlh]");
            
            var keywords=wlmc+","+ms;
            if(keywords.trim)
                var domain=$("#domain").val();
            // window.open( '/forms/open/select.aspx?windowid=bom&ctrl0='+ctrl0+'&ctrl1='+ctrl1+'&ctrl2='+ctrl2);
            layer.open({
                shade: [0.5, '#000', false],
                type: 2,
                offset: '100px',
                area: ['600px', '450px'],
                fix: false, //不固定
                maxmin: false,
                title: ['<i class="fa fa-dedent"></i> 历史价格', false],    
                closeBtn: 1,
                content: '/forms/open/selHisPrice.aspx?windowid=historyprice&needreturn='+readonly+'&domain='+domain+'&keywords='+keywords+'&ctrl0='+ctrl0+'&ctrl1='+ctrl1+'&ctrl2='+ctrl2,
                end: function(e) {
                    
                }
            });            
        }


        function enableGroup(oname)
        {   
            var objs=$("[id]")
            var o = c.parentNode;
            while (o && o.tagName != "TR")
            {
                o = o.parentNode;
            }
            if (o)
            {
                var is = o.getElementsByTagName("INPUT");
                for (var i = 0, n = is.length; i < n; i++)
                {
                    is[i].disabled = !c.checked;
                }
            } 
        }

        
        //设定表字段状态（可编辑性）
        var tabName="pur_pr_main_form";//表名
        function SetControlStatus(fieldStatus)
        {  // tabName_columnName:1_0
            var flag=true;
            for(var item in fieldStatus){
                var id=""+item.replace(tabName.toLowerCase()+"_","");
                
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
                        $("#btnAddDetl").css("display","none");
                        $("#btnDelete").css("display","none");
                    }
                }
            }
        }
        var tabName2="pur_pr_dtl_form";//表名
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
                        $(obj).removeAttr("onclick");$(obj).removeAttr("ondblclick");
                        $(obj).css("border","none");
                    }
                    else if( statu.indexOf("1_")!="-1" && ( ctype=="checkbox"||ctype=="radio"||ctype=="select"||ctype=="file"  ) ){
                        $(obj).attr("disabled","disabled");
                    }
                    else if(statu.indexOf("1_")!="-1" && ( ctype=="input" ) ){
                        $(obj).attr("type","hidden");
                    }

                    if (IsRead=="N") {

                        var p3=$("#prtype").val();
                        if(p3=="费用服务类" || p3=="合同类"){    
                            if($(obj).attr("id").indexOf("_wlmc_")!="-1"){
                                var wltype= $(obj).parent().parent().find("input[id*=wltype]");
                                var note= $(obj).parent().parent().find("input[id*=note]");

                                wltype.attr("onclick","Getwltype(this);");wltype.css("border",""); wltype.css("background-color","white");
                                note.attr("onclick","Getwltype(this);");note.css("border",""); note.css("background-color","white");note.attr("readonly","readonly");
                            }
                        }else{

                            if($(obj).attr("id").indexOf("_wlh_")!="-1"){                        
                                var wlmc= $(obj).parent().parent().find("input[id*=wlmc]");
                                var wlms= $(obj).parent().parent().find("input[id*=wlms]");
                                var wltype= $(obj).parent().parent().find("input[id*=wltype]");
                                var wlsubtype= $(obj).parent().parent().find("input[id*=wlsubtype]");              
                                var notax_historyprice=$(obj).parent().parent().find("input[id*=notax_historyprice]");
                       
                                if($(obj).val()!="无"){
                                    wlmc.attr("readonly","readonly");
                                    wlms.attr("readonly","readonly");

                                    wlmc.css("border-style","none");   
                                    wlmc.css("background-color","Transparent");
                                    wlms.css("border-style","none");   
                                    wlms.css("background-color","Transparent");

                                    if(p3=="刀具类"){wlsubtype.removeAttr("onclick");wlsubtype.css("border","none");wlsubtype.css("background-color","Transparent");}
                                    if(p3!="刀具类"){wltype.removeAttr("onclick");wltype.css("border","none");wltype.css("background-color","Transparent");}
                                }else{                            
                                    wlmc.removeAttr("readonly");                 
                                    wlmc.css("border-style","inset");   
                                    wlmc.css("background-color","white");

                                    wlms.removeAttr("readonly");                
                                    wlms.css("border-style","inset");   
                                    wlms.css("background-color","white"); 

                                    if(p3=="刀具类"){wlsubtype.attr("onclick","Getwltype(this);");wlsubtype.css("border",""); wlsubtype.css("background-color","white");}
                                    if(p3!="刀具类"){wltype.attr("onclick","Getwltype(this);");wltype.css("border","");wltype.css("background-color","white"); }
                                }
                            }
                        }
                    }

                });

            }
        }

    </script>
   
    <style>
        #SQXX label{
            font-weight:400;
        }
        .lineread{
            /*font-size:9px;*/ border:none; border-bottom:1px solid #ccc;
        }
        .linewrite{
            /*font-size:9px;*/ border:none; border-bottom:1px solid #ccc;background-color:#FDF7D9;/*EFEFEF*/
        }
        /*.dxeTextBoxDefaultWidthSys, .dxeButtonEditSys{
            width:120px;
        }*//*后台方法loadControll里最后面for循环设定申请时的宽度的*/
        .dxeButtonDisabled{
            display:none;
        }
    </style>
</head>
<body>
    <script type="text/javascript">
	   <%-- var initData = <%=BWorkFlow.GetFormDataJsonString(initData)%>;--%>
        var fieldStatus = "1"=="<%=Request.QueryString["isreadonly"]%>"? {} : <%=fieldStatus%>;
        var fieldSet=<%=fieldStatus%>;
        var displayModel = '<%=DisplayModel%>';
        var IsRead = '<%=IsRead%>'
        $(window).load(function (){

            SetControlStatus(<%=fieldStatus%>);
            SetControlStatus2(<%=fieldStatus%>);                      
                
        });

        //验证
        function validate(id){
            if($("#applydept").val()==""){
                layer.alert("请选择归属公司及部门.");
                return false;
            }
            if($("#deptm").val()=="" || $("#deptmfg").val()==""){
                layer.alert("部门经理(或分管副总)未设定，请联系IT设定.");
                return false;
            }
            <%=ValidScript%>
            var flag=true;
            var msg="";

            if($("#gvdtl input[id*=wlmc]").length==0){
                layer.alert("请添加明细信息再提交发送.");
                return false;
            }

            if ($("#prtype").val()=="费用服务类" || $("#prtype").val()=="合同类") {

                $("#gvdtl input[id*=wlmc]").each(function (){
                    if($(this).val()==""){
                        msg+="【名称】不可为空.<br />";
                        flag=false;
                        return false;
                    }
                });
                $("#gvdtl input[id*=wlms]").each(function (){
                    if($(this).val()==""){
                        msg+="【描述】不可为空.<br />";
                        flag=false;
                        return false;
                    }
                });
                $("#gvdtl input[id*=wltype]").each(function (){
                    if($(this).val()==""){
                        msg+="【类别】不可为空.<br />";
                        flag=false;
                        return false;
                    }else if($(this).val()=="模具" || $(this).val()=="夹具" || $(this).val()=="量检具"){
                        var assetattributedesc=$(this).parent().parent().find("input[type=text][id*=assetattributedesc]");
                        if(assetattributedesc.val()==""){
                            msg+="【"+$(this).val()+"】对应的【资产属性】不可为空.<br />";
                            flag=false;
                            return false;
                        }
                    }
                });
            }else {
                $("#gvdtl").find("tr td input[id*=wlh]").each(function () {
                    if($(this).val()==""){
                        msg+="【物料号】不可为空.<br />";
                        flag=false;
                        return false;
                    }else {
                        var wlmc= $(this).parent().parent().find("input[id*=wlmc]");
                        var wlms= $(this).parent().parent().find("input[id*=wlms]");
                        var wlType= $(this).parent().parent().find("input[id*=wltype]");
                        var wlSubType= $(this).parent().parent().find("input[id*=wlsubtype]"); 

                        if(wlms.val()==""){
                            msg+="【物料名称】不可为空.<br />";
                            flag=false;
                            return false;
                        }
                        if(wlms.val()==""){
                            msg+="【物料描述】不可为空.<br />";
                            flag=false;
                            return false;
                        }
                        if($("#prtype").val()!="刀具类" ){
                            if(wlType.val()==""){
                                msg+="【物料类别】不可为空.<br />";
                                flag=false;
                                return false;
                            }
                        }
                        if($("#prtype").val()=="刀具类" ){
                            if(wlSubType.val()==""){
                                msg+="【物料子类别】不可为空.<br />";
                                flag=false;
                                return false;
                            }
                        }
                       
                    }
                });
            }

            //validate wlh
            $("#gvdtl input[id*=usefor]").each(function (){
                if( $(this).val()==""){
                    msg+="【用于产品/项目】不可为空.<br />";
                    flag=false;
                    return false;
                }
            });
            $("#gvdtl input[id*=unit]").each(function (){
                if( $(this).val()==""){
                    msg+="【单位】不可为空.<br />";
                    flag=false;
                    return false;
                }
            });
            $("#gvdtl input[id*=currency]").each(function (){
                if( $(this).val()==""){
                    msg+="【币别】不可为空.<br />";
                    flag=false;
                    return false;
                }
            });
            $("#gvdtl input[id*=notax_historyprice]").each(function (){
                if( $(this).val()==""){
                    msg+="【历史最低单价(未税)】不可为空.<br />";
                    flag=false;
                    return false;
                }
            });
            $("#gvdtl input[id*=notax_targetprice]").each(function (){
                if( $(this).val()==""){
                    msg+="【目标价】不可为空.<br />";
                    flag=false;
                    return false;
                }
            });
            //validate qty
            $("#gvdtl input[id*=qty]").each(function (){
                if( $(this).val()==""){
                    msg+="【数量】不可为空.<br />";
                    flag=false;
                    return false;
                }
            });
            //validate deliverydate
            $("#gvdtl input[id*=deliverydate]").each(function (){
                if( $(this).val()==""){
                    msg+="【要求到货日期】不可为空.<br />";
                    flag=false;
                    return false;
                }
            });

            if(flag==false){  
                layer.alert(msg);
                return false;
            }
            if($("#upload").val()==""&& $("#link_upload").text()==""){
                layer.alert("请选择【附件】");
                return false;
            }    
            
            if(!parent.checkSign()){
                return false;
            }

        }
        function getHisToryPrice_By_mc_ms(ctrl,wlmc,wlms){
            //var ss = e.id.split("_");// 在每个逗号(,)处进行分解。
            //var wlmc=$("input[id*='gvdtl_cell" + id + "_7_wlmc_" + id + "']");
            //var wlms=$("input[id*='gvdtl_cell" + id + "_7_wlms_" + id + "']");
            var p2=$("#domain").val();
            $.ajax({
                type: "Post",async: false,
                url: "PUR_PR.aspx/getHisToryPrice_By_mc_ms" , 
                data: "{'mc':'"+wlmc.val()+"','ms':'"+wlms.val()+"','p2':'"+p2+"'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {//返回的数据用data.d获取内容//                             
                    if (data.d == "") {
                        layer.alert("未获取到历史最低价.");  
                        $("#"+ctrl).val("");
                    }
                    else {
                        var reg = /([0-9]+\.[0-9]{4})[0-9]*/;
                        aNew = (data.d).replace(reg,"$1");
                        $("#"+ctrl).val(aNew);
                    }                   
                },
                error: function (err) {
                    layer.alert(err);
                }
            });
        }

        //GetHistroyPrice
        function getHisToryPrice(p1,ctrl){
            var p2=$("#domain").val();
            $.ajax({
                type: "Post",async: false,
                url: "PUR_PR.aspx/GetHistoryPrice" , 
                data: "{'P1':'"+p1+"','P2':'"+p2+"'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {//返回的数据用data.d获取内容//                             
                    if (data.d == "") {
                        layer.alert("未获取到历史最低价.");  
                        $("#"+ctrl).val("");
                    }
                    else {
                        var reg = /([0-9]+\.[0-9]{4})[0-9]*/;
                        aNew = (data.d).replace(reg,"$1");
                        $("#"+ctrl).val(aNew);
                    }                   
                },
                error: function (err) {
                    layer.alert(err);
                }
            });
        }

        function getDaoJuMatInfo(p1,wltype,wlsubtype,wlmc,wlms,attachments,attachments_name,wlh,notax_historyprice){
            var p2=$("#domain").val();
            var p3=$("#prtype").val();
            if(p2==""){layer.alert("请选择【申请工厂】");return false;}
            $.ajax({
                type: "Post",
                url: "PUR_PR.aspx/GetDaoJuMatInfo" , 
                async: false,
                data: "{'P1':'"+p1+"','P2':'"+p2+"','P3':'"+p3+"'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {//返回的数据用data.d获取内容//      
                    if (data.d == "") {
                        layer.msg("未获取到该物料信息,请确认是否有此物料且状态为AC.");  
                        
                        $(wlh).val("");
                        $(wlmc).val("");
                        $(wlms).val("");
                        $(wlsubtype).val("");
                        $(wltype).val("");
                        $(notax_historyprice).val("");
                    }else {
                        $.each(eval(data.d), function (i, item) {    
                            if(item.wlmc==""){//||item.ms==""
                                layer.alert(item.wlh+"请维护完整物料信息后再申请.");

                                $(wlh).val("");
                                $(wlmc).val("");
                                $(wlms).val("");
                                $(wlsubtype).val("");
                                $(wltype).val("");
                                $(notax_historyprice).val("");
                                return false;
                            }else{
                                $(wltype).val(item.class);    
                                $(wlsubtype).val(item.type);
                                $(wlmc).val(item.wlmc);
                                $(wlms).val(item.ms);
                                $(attachments).val(item.upload);

                                if(p3 == "刀具类"){
                                    $(attachments_name[0]).prop("href",item.upload); 
                                }else {
                                    $(attachments_name[0]).text("无");
                                }
                            }
                        });  
                    }

                    $(wlmc).attr("readonly","readonly");
                    $(wlmc).css("border-style","none");   
                    $(wlmc).css("background-color","Transparent");

                    $(wlms).attr("readonly","readonly");
                    $(wlms).css("border-style","none");   
                    $(wlms).css("background-color","Transparent");

                    if(p3=="刀具类"){$(wlsubtype).removeAttr("onclick");wlsubtype.css("border","none");wlsubtype.css("background-color","Transparent");}
                    if(p3!="刀具类"){$(wltype).removeAttr("onclick");wltype.css("border","none");wltype.css("background-color","Transparent");}
                },
                error: function (err) {
                    layer.alert(err);
                }
            });
        }

        function getDeptLeader(domain,dept){     
            var createdept=$("#DeptName").val();        
            $.ajax({
                type: "Post",async: false,
                url: "PUR_PR.aspx/getDeptLeaderByDept" , 
                //方法传参的写法一定要对，str为形参的名字,str2为第二个形参的名字
                //P1:wlh P2： 
                data: "{'domain':'"+domain+"','dept':'"+dept+"','createdept':'"+createdept+"'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {//返回的数据用data.d获取内容//                        
                    var obj = eval(data.d); 

                    if (obj[0].re_flag=="Y") {
                        layer.alert("未获取到部门主管,请联系IT确认.");     
                        $("#deptm").val("");
                        $("#deptmfg").val("");
                    }
                    else {                        
                        $("#deptm").val(obj[0].manager_id);
                        $("#deptmfg").val(obj[0].fz_id);
                    }                   
                },
                error: function (err) {
                    layer.alert(err);
                }
            });
        };
                       
        function bindSelect(sel){
            var domain=$("#domain").val();
            var applyid=$("#CreateById").val();
            $.ajax({
                type: "Post",async: false,
                url: "PUR_PR.aspx/GetDeptByDomain" , 
                data: "{'domain':'"+domain+"','applyid':'"+applyid+"'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {//返回的数据用data.d获取内容//                        
                    //alert(data.d)
                    $("#applydept").empty();  
                    $("#applydept").append($("<option>").val("").text(""));
                    $.each(eval(data.d), function (i, item) {                                
                        if (data.d == "") {
                            layer.msg("未获取到部门.");                            
                        }
                        else {   
                            var option = $("<option>").val(item.Dept_Name).text(item.Dept_Name);
                            $("#applydept").append(option); 
                        }
                    })                    
                },
                error: function (err) {
                    layer.alert(err);
                }
            });
            
        }
    </script>
     
    <script type="text/javascript">
        var popupwindow = null;

        function Getdaoju(e)
        {  
            var ss = e.id.split("_");// 在每个逗号(,)处进行分解。
            var prtype=$("#prtype").val();
            var url = "/select/select_pt_mstr.aspx?id=" + ss[4] + "&domain=" + $("select[id*='domain']").val()+ "&prtype=" + encodeURIComponent(prtype);

            layer.open({
                title: prtype+'&nbsp;&nbsp;&nbsp;&nbsp;<font color="red">选择方式:双击行</font>',
                closeBtn: 2,
                type: 2,
                area: ['900px', '500px'],
                fixed: false, //不固定
                maxmin: true, //开启最大化最小化按钮
                content: url
            });
        }
        function setvalue_dj(id, lswlh, lsms,lswlmc)
        {   
            var p3=$("#prtype").val();
            var wlh= $("input[id*='gvdtl_cell" + id + "_4_wlh_" + id+ "']");
            var wlmc= $("input[id*='gvdtl_cell" + id + "_5_wlmc_" + id + "']");
            var wlms= $("input[id*='gvdtl_cell" + id + "_6_wlms_" + id + "']");
            var wlType= $("input[id*='gvdtl_cell" + id + "_7_wltype_" + id + "']");
            var wlSubType= $("input[id*='gvdtl_cell" + id + "_8_wlsubtype_" + id + "']");
            var notax_historyprice= $("input[id*='gvdtl_cell" + id + "_13_notax_historyprice_" + id + "']");
           
            if (wlh!="无") {
                wlh.val(lswlh);
                wlh.change();
            }else {
                wlh.val(lswlh);
                wlmc.val("");
                wlms.val("");
                notax_historyprice.val("");

                wlmc.removeAttr("readonly");               
                wlmc.css("border-style","inset");   
                wlmc.css("background-color","white");

                wlms.removeAttr("readonly");          
                wlms.css("border-style","inset");   
                wlms.css("background-color","white"); 

                if(p3=="刀具类"){wlSubType.attr("onclick","Getwltype(this);");wlSubType.css("border",""); wlSubType.css("background-color","white");wlSubType.val("");}
                if(p3!="刀具类"){wlType.attr("onclick","Getwltype(this);");wlType.css("border",""); wlType.css("background-color","white");wlType.val("");}
            }
        }

        function Getwltype(e){
            var ss = e.id.split("_");// 在每个逗号(,)处进行分解。
            var prtype=$("#prtype").val();
            var createdept=$("#DeptName").val();
            var url = "/select/select_pur_type.aspx?id=" + ss[4] + "&domain=" + $("select[id*='domain']").val()+ "&prtype=" + encodeURIComponent(prtype)+"&createdept=" + encodeURIComponent(createdept);

            var width='300px';
            if(prtype=="费用服务类" || prtype=="合同类"){  width='500px'  }

            layer.open({
                title: prtype+'&nbsp;<font color="red">选择方式:双击行</font>',
                closeBtn: 2,
                type: 2,
                area: [width, '500px'],
                fixed: false, //不固定
                maxmin: true, //开启最大化最小化按钮
                content: url
            });
        }

        function setvalue_wltype(id,typedesc){
            var p3=$("#prtype").val();
            //用途类别在描述后面
            //var wlType= $("input[id*='gvdtl_cell" + id + "_7_wltype_" + id + "']");
            //var wlSubType= $("input[id*='gvdtl_cell" + id + "_8_wlsubtype_" + id + "']");

            //用途类别在说明前面
            var wlType= $("input[id*='gvdtl_cell" + id + "_19_wltype_" + id + "']");
            var wlSubType= $("input[id*='gvdtl_cell" + id + "_19_wlsubtype_" + id + "']");

            if(p3=="刀具类"){wlSubType.val(typedesc);}
            if(p3!="刀具类"){wlType.val(typedesc);}
        }

        function setvalue_wltype2(id,typedesc,typedesc2){
            //用途类别在描述后面
            //var wlType= $("input[id*='gvdtl_cell" + id + "_7_wltype_" + id + "']");
            //var note= $("input[id*='gvdtl_cell" + id + "_20_note_" + id + "']");

            //用途类别在说明前面
            var wlType= $("input[id*='gvdtl_cell" + id + "_19_wltype_" + id + "']");
            var note= $("input[id*='gvdtl_cell" + id + "_20_note_" + id + "']");

            wlType.val(typedesc);note.val(typedesc2);
        }
        
        //计算总价 
        function getTotalPrice(){                                                      
            $("#gvdtl").find("tr td input[id*=qty],tr td input[id*=notax_targetprice]").each(function () { 
                $(this).bind("change", function () {                                                          
                    var price = $(this).parent().parent().find("input[id*=notax_targetprice]").val(); 
                    var qty = $(this).parent().parent().find("input[id*=qty]").val();  
                    price= (price==""||price=="NaN")? 0 : price;
                    qty= (qty==""||qty=="NaN")? 0 : qty;
                    if(price!=null&&qty!="")
                    {   
                        var result = (parseFloat(price) * parseFloat(qty)) ; 
                        $(this).parent().parent().find("input[id*=notax_targettotal]").val(result); 
                    }else{  
                        $(this).parent().parent().find("input[id*=notax_targettotal]").val("");
                    }
                    //计算所有明细总价                                 
                    get_notax_TotalMoney();
                }); 
            });
        }
        function get_notax_TotalMoney(){
            //计算所有明细总价            
            var notax_totalMoney=0;
            $("#gvdtl").find("tr td input[id*=notax_targettotal]").each(function (i) {
                var rowval=$("tr td input[id*=notax_targettotal_"+i+"]").val();
                rowval= (rowval==""||rowval=="NaN")? 0 : rowval;                
                notax_totalMoney=notax_totalMoney+parseFloat(rowval)
                $("#notax_totalMoney").val(notax_totalMoney);

                //grid底部total值更新
                $('table[id*=gvdtl] tr[id*=DXFooterRow]').find('td').each(function () {
                    //if($.trim($(this).text())!=""){
                    //    $(this).text("合计:"+fmoney(notax_totalMoney,2));
                    //}   
                    if ($.trim($(this).text())!="") {
                        $(this).html("<font color='red' Size='2'>合计:"+fmoney(notax_totalMoney,2)+"</font>");
                    }
                });
                
            })
        }
        //格式化千分位
        function fmoney(s, n)   
        {   
            n = n > 0 && n <= 20 ? n : 2;   
            s = parseFloat((s + "").replace(/[^\d\.-]/g, "")).toFixed(n) + "";   
            var l = s.split(".")[0].split("").reverse(),   
            r = s.split(".")[1];   
            t = "";   
            for(i = 0; i < l.length; i ++ )   
            {   
                t += l[i] + ((i + 1) % 3 == 0 && (i + 1) != l.length ? "," : "");   
            }   
            return t.split("").reverse().join("") + "." + r;   
        }
        //绑定物料信息
        function getMatInfo(){            
            var p3=$("#prtype").val();

            //没有物料号字段
            if(p3=="费用服务类" || p3=="合同类"){    
                $("#gvdtl").find("tr td input[id*=wltype]").each(function () {
                    var wltype= $(this).parent().parent().find("input[id*=wltype]");
                    var note= $(this).parent().parent().find("input[id*=note]"); 

                    wltype.attr("onclick","Getwltype(this);");wltype.css("border",""); wltype.css("background-color","white");wltype.attr("readonly","readonly");
                    note.attr("onclick","Getwltype(this);");note.css("border",""); note.css("background-color","white");note.attr("readonly","readonly");
                });
            }else {
                //有料号字段
                $("#gvdtl").find("tr td input[id*=wlh]").each(function () {
                    $(this).bind("change", function () { 
                        //物料信息   
                        var wlh=this.id;
                        var ss=wlh.split("_");

                        var wlType= $(this).parent().parent().find("input[id*=wltype]");
                        var wlSubType= $(this).parent().parent().find("input[id*=wlsubtype]"); 
                        var wlmc= $(this).parent().parent().find("input[id*=wlmc]");
                        var wlms= $(this).parent().parent().find("input[id*=wlms]");
                        var attachments= $(this).parent().parent().find("input[id*=attachments]");
                        var attachments_name= $(this).parent().parent().find("a[id*=attachments_name]");                    
                        var notax_historyprice=$(this).parent().parent().find("input[id*=notax_historyprice]");  
                    
                        if ($(this).val()!="无") {      
                            //赋历史采购价
                            getHisToryPrice($(this).val(),notax_historyprice[0].id); 
                            getDaoJuMatInfo($(this).val(),wlType,wlSubType,wlmc,wlms,attachments,attachments_name,$(this),notax_historyprice);
                        }else {
                            wlmc.val("");
                            wlms.val("");
                            notax_historyprice.val("");

                            wlmc.removeAttr("readonly");       
                            wlmc.css("border-style","inset");   
                            wlmc.css("background-color","white");

                            wlms.removeAttr("readonly");              
                            wlms.css("border-style","inset");   
                            wlms.css("background-color","white");

                            if(p3=="刀具类"){wlSubType.attr("onclick","Getwltype(this);");wlSubType.css("border",""); wlSubType.css("background-color","white");wlSubType.val("");}
                            if(p3!="刀具类"){wlType.attr("onclick","Getwltype(this);");wlType.css("border",""); wlType.css("background-color","white");wlType.val("");}
                        }
                    }); 
                });
            }

            $("#gvdtl").find("tr td input[id*=wlmc]").each(function () {
                $(this).bind("change", function () { 
                    var wlmc= $(this).parent().parent().find("input[id*=wlmc]");
                    var wlms= $(this).parent().parent().find("input[id*=wlms]");                 
                    var notax_historyprice=$(this).parent().parent().find("input[id*=notax_historyprice]");  
                    getHisToryPrice_By_mc_ms(notax_historyprice[0].id,wlmc,wlms);
                }); 
            });
            $("#gvdtl").find("tr td input[id*=wlms]").each(function () {
                $(this).bind("change", function () { 
                    var wlmc= $(this).parent().parent().find("input[id*=wlmc]");
                    var wlms= $(this).parent().parent().find("input[id*=wlms]");                 
                    var notax_historyprice=$(this).parent().parent().find("input[id*=notax_historyprice]");  
                    getHisToryPrice_By_mc_ms(notax_historyprice[0].id,wlmc,wlms);
                }); 
            });
        }


    </script>

    <script>//20181108 add heguiqin
        function Add_check(){
            var domain=$("#domain").val();
            var dept=$("#applydept").val();

            var prtype=$("#prtype").val();

            if (domain=="" || dept=="") {
                layer.alert("请选择【申请公司】");
                return false;
            }

            if(prtype==""){
                layer.alert("请选择【采购类别】");
                return false;
            }

            return true;
        }
    </script>

    <script type="text/javascript">
        var uploadedFiles = [];
        function onFileUploadComplete(s, e) {
            if(e.callbackData) {
                var fileData = e.callbackData.split(',');uploadedFiles.push(fileData);$("#<%=ip_filelist.ClientID%>").val(uploadedFiles.join("|"));
                var fileName = fileData[0],
                    fileUrl = fileData[1],
                    fileSize = fileData[2];                
                var eqno=uploadedFiles.length-1;

                var tbody_tr='<tr id="tr_'+eqno+'"><td Width="400px"><a href="'+fileUrl+'" target="_blank">'+fileName+'</a></td>'
                        +'<td Width="60px">'+fileSize+'</td>'
                        +'<td><span style="color:blue;cursor:pointer" id="tbl_delde" onclick ="del_data(tr_'+eqno+','+eqno+')" >删除</span></td>'
                        +'</tr>';

               $('#tbl_filelist').append(tbody_tr);
                //alert(fileName);
                //DXUploadedFilesContainer.AddFile(fileName, fileUrl, fileSize);
            }
        }


        function del_data(a,eno){
            $(a).remove();
            uploadedFiles[eno]=null;
           $("#<%=ip_filelist.ClientID%>").val(uploadedFiles.join("|"));
        }
        
    </script>
    
    <form id="form1" runat="server" enctype="multipart/form-data">

        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <div class="h4" style="margin-left: 10px" id="headTitle">
            PGI管理系统<div class="btn-group">
                <div class="area_drop" data-toggle="dropdown">
                    <span class="caret"></span>
                </div>               
            </div>
            <span id="mestitle"></span>

            <div style="float: right; margin-right: 10px; font-size: 10px">
                <label id="logUser"><%=Session["UserAD"].ToString() %></label>
                <label id="logUser">(<%=Session["UserId"].ToString() %>)</label>
            </div>

        </div>
        <div class="col-md-12  ">
            <div class="col-md-12  ">
                <div class="form-inline " style="text-align: right">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <script type="text/jscript">
                                var prm = Sys.WebForms.PageRequestManager.getInstance();
                                prm.add_endRequest(function () {
                                    // re-bind your jquery events here
                                    SetButtons();
                                   
                                });
                            </script>
                            <asp:Button ID="btnSave" runat="server" Text="保存" CssClass="btn btn-default btn-xs btnSave" OnClientClick="" OnClick="btnSave_Click" ToolTip="临时保存此流程"  UseSubmitBehavior="false"   />
                            <asp:Button ID="btnflowSend" runat="server" Text="发送" CssClass="btn btn-default btn-xs btnflowSend" OnClientClick="if(validate()==false)return false;" OnClick="btnflowSend_Click" UseSubmitBehavior="false"  />
                            <input id="btnaddWrite" type="button" value="加签" onclick="parent.addWrite(true);" class="btn btn-default btn-xs btnaddWrite" />
                            <input id="btnflowBack" type="button" value="退回" onclick="parent.flowBack(true);" class="btn btn-default btn-xs btnflowBack" />
                            <input id="btnflowCompleted" type="button" value="完成" onclick="parent.flowCompleted(true);" class="btn btn-default btn-xs btnflowCompleted" />
                            <input id="btnshowProcess" type="button" value="查看流程" onclick="parent.showProcess(true);" class="btn btn-default btn-xs btnshowProcess" />
                            <input id="btntaskEnd" type="button" value="终止流程" onclick="parent.taskEnd(true);" class="btn btn-default btn-xs btntaskEnd" />
                        </ContentTemplate>
                    </asp:UpdatePanel>

                </div>
            </div>
        </div>
        <asp:TextBox ID="txtInstanceID" runat="server" CssClass=" hidden" ToolTip="0|0" Width="40"  />
        <div class="col-md-12">
            <div class="row row-container">
                <div class="col-md-12">
                    <div class="panel panel-infos">
                        <div class="panel-headings" data-toggle="collapse" data-target="#SQXX">
                            <strong>申请记录基础信息</strong>
                        </div>
                        <div class="panel-body collapse" id="SQXX">
                            <div class="col-xs-12 col-sm-12  col-md-12 col-lg-12" style="width:1200px;">
                                <div class="">
                                    <asp:UpdatePanel ID="UpdatePanel_request" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <script type="text/jscript">
                                                var prm = Sys.WebForms.PageRequestManager.getInstance();
                                                prm.add_endRequest(function () {
                                                    // 部门主管
                                                    var domain=$("#domain").val();
                                                    var dept=$("#applydept").val();
                                                    getDeptLeader(domain,dept);  
                                                 
                                                });
                                            </script>
                                            <table style="height: 35px; width: 100%">
                                                <tr>
                                                    <td style="width:8%;">请购单号：</td>
                                                    <td style="width:62%;"><%--CssClass="form-control input-s-sm"--%>
                                                        <asp:TextBox ID="PRNo" runat="server" CssClass="lineread" readonly="true" Width="41%" ToolTip="1|0"   />
                                                    </td>
                                                    <td style="width:5%;">申请日期：</td>
                                                    <td style="width:25%;"><%--CssClass="form-control input-s-sm"--%>
                                                        <asp:TextBox ID="CreateDate" CssClass="lineread" Style="height: 27px; width: 100%" runat="server" ReadOnly="True" />
                                                    </td>                                                    
                                                   <%-- <td>当前登陆人：</td>--%><%--CssClass="form-control input-s-sm"--%>
                                                    <%--<td>
                                                        <div class="form-inline">
                                                            <input id="txt_LogUserId" class="lineread" style="height: 27px; width:40px" runat="server" readonly="True" />
                                                            <input id="txt_LogUserName" class="lineread" style="height: 27px; width: 60px" runat="server" readonly="True" />
                                                            <input id="txt_LogUserDept" class="lineread" style="height: 27px; width: 90px" runat="server" readonly="True" />                                                           
                                                        </div>
                                                    </td> --%>
                                                </tr>
                                                <tr>
                                                    <td style="width:8%;">申请人：</td>
                                                    <td style="width:62%;">
                                                        <div class="form-inline"><%--CssClass="form-control input-s-sm"--%>
                                                            <asp:TextBox runat="server" ID="CreateById" CssClass="lineread" Style="height: 27px; width: 5%" ReadOnly="True"></asp:TextBox>
                                                            <asp:TextBox runat="server" ID="CreateByName" CssClass="lineread" Style="height: 27px; width: 15%" ReadOnly="True"></asp:TextBox>                                                           
                                                            <asp:TextBox runat="server" id="DeptName" cssclass="lineread" style="height: 27px; width: 20%" readonly="True" />
                                                        </div>
                                                    </td>                                                    
                                                    <td style="width:5%;">电话（分机）：
                                                    </td>
                                                    <td style="width:25%;"><%--CssClass="form-control input-s-sm"--%>                                                        
                                                        <asp:TextBox id="phone" class="lineread" style="height: 27px; width: 100%" runat="server"  />                                                            
                                                    </td>
                                                </tr>
                                            </table>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row  row-container">
                <div class="col-md-12">
                    <div class="panel panel-infos">
                        <div class="panel-headings" data-toggle="collapse" data-target="#PA">
                            <strong>用途信息</strong>
                        </div>
                        <div class="panel-body " id="PA">
                            <div class="col-xs-12 col-sm-12  col-md-12 col-lg-12" style="width:1200px;">
                                <div><%--<asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional"><ContentTemplate>--%>
                                    <table style="" border="0" runat="server" id="tblWLLeibie">
                                        <tr>
                                            <td style="width:5%;">归属公司/部门：</td>
                                            <td style="width:65%;">
                                                 <div style="float:left;Width:15%;"><%--CssClass="form-control input-s-sm"--%>
                                                    <asp:DropDownList ID="domain" CssClass="linewrite" runat="server" Width="100%" Height="27px" ToolTip="0|1"  >
                                                        <asp:ListItem Value="" Text=""></asp:ListItem>
                                                        <asp:ListItem Value="200" Text="昆山工厂"></asp:ListItem>
                                                        <asp:ListItem Value="100" Text="上海工厂"></asp:ListItem>
                                                    </asp:DropDownList></div>  
                                                <div style="float:left;Width:85%;"><%--CssClass="form-control input-s-sm"--%>
                                                    <asp:DropDownList ID="applydept" CssClass="linewrite" runat="server" Width="30%" Height="27px" ToolTip="0|1" >
                                                    </asp:DropDownList>
                                                </div>
                                                <asp:TextBox id="deptm"  style=" width: 20px;display:none" runat="server"  />
                                                <asp:TextBox id="deptmfg"  style=" width: 20px;display:none" runat="server"  />   
                                            </td>
                                            <td style="width:5%;">采购类别：</td>
                                            <td style="width:25%;"><%--CssClass="form-control"--%>
                                                <%--<asp:DropDownList ID="prtype" runat="server" CssClass="linewrite" ToolTip="0|1" Width="200px" Height="27px"
                                                    AutoPostBack="True" OnSelectedIndexChanged="prtype_SelectedIndexChanged"></asp:DropDownList>--%>
                                                <asp:DropDownList ID="prtype" runat="server" CssClass="linewrite" ToolTip="0|1" Width="100%" Height="27px"></asp:DropDownList>
                                            </td>
                                        </tr>                                        
                                        <tr>
                                            <td style="width:5%;">申请原因描述：</td>
                                            <td style="width:95%" colspan="3"><%--CssClass="form-control input-s-sm" --%>
                                                <asp:textbox ID="prreason" TextMode="MultiLine" runat="server" CssClass="linewrite" Width="100%" ToolTip="0|0"   />                                               
                                            </td>
                                        </tr>
                                    </table>
                                  <%-- </ContentTemplate></asp:UpdatePanel>--%>
                                     <asp:TextBox ID="notax_totalMoney" runat="server"  Width="80px" ToolTip="0|0" CssClass=" hidden" />   
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row  row-container">
                <div class="col-md-12">
                    <div class="panel panel-infos">
                        <div class="panel-headings" data-toggle="collapse" data-target="#PB">
                            <strong style="padding-right: 100px">请购物品信息<span id="warning" style="color: red; display: none"> </span></strong>
                        </div>
                        <div class="panel-body  collapse in" id="PB">
                            <div class="col-xs-12 col-sm-12  col-md-12 col-lg-12">
                                <div>                                    
                                     <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional"><ContentTemplate>
                                        <script type="text/jscript">
                                            var prm = Sys.WebForms.PageRequestManager.getInstance();
                                            prm.add_endRequest(function () {
                                                //总价 totalprice
                                                getTotalPrice();
                                                appendSearch();
                                                //绑定物料信息
                                                getMatInfo();
                                                //OnClientClick="return Add_check()"
                                                get_notax_TotalMoney();
                                               SetControlStatus2(<%=fieldStatus%>);
                                            });
                                        </script>
                                        
                                                                                      
                                    <asp:Button ID="btnAddDetl" runat="server" Text="添加" CssClass="btn btn-default btn-sm" OnClientClick="return Add_check()"   OnClick="btnAddDetl_Click"/>
                                    <asp:Button ID="btnDelete" runat="server" Text="删除"  CssClass="btn btn-default btn-sm"   OnClick="btnDelete_Click"/><%--btn btn-primary btn-sm--%>

                                    <%--Border-BorderColor="#DCDCDC"--%>
                                        <dx:ASPxGridView ID="gvdtl" runat="server" Width="1200px" ClientInstanceName="grid" KeyFieldName="rowid" EnableTheming="True" Theme="MetropolisBlue" 
                                                Border-BorderColor="#F0F0F0" OnCustomCallback="gvdtl_CustomCallback">
                                            <ClientSideEvents EndCallback="function(s, e) { appendSearch();getMatInfo();}" />
                                            <SettingsBehavior AllowDragDrop="false" AllowFocusedRow="false" AllowSelectByRowClick="false" AllowSort="false"  />
                                            <SettingsPager PageSize="1000">
                                            </SettingsPager>
                                            <Settings ShowFilterRow="false" ShowFilterRowMenu="false" ShowFilterRowMenuLikeItem="false" ShowFooter="True" />
                                            <SettingsCommandButton>
                                                <SelectButton ButtonType="Button" RenderMode="Button">
                                                </SelectButton>
                                            </SettingsCommandButton>
                                            <SettingsSearchPanel Visible="false" />
                                            <SettingsFilterControl AllowHierarchicalColumns="True">
                                            </SettingsFilterControl>
                                            <Styles>
                                                <%--<Header BackColor="#E4EFFA"  Border-BorderColor="#DCDCDC"></Header>  --%> 
                                                <Header BackColor="#31708f" Font-Bold="True" ForeColor="white" Border-BorderStyle="None"></Header>    <%--Font-Size="11pt"--%>

                                                <SelectedRow BackColor="#FDF7D9"></SelectedRow> 
                                                <AlternatingRow BackColor="#f2f3f2"></AlternatingRow>       
                                                                                   
                                                <%--<Footer HorizontalAlign="Right"></Footer>--%>
                                                <Footer HorizontalAlign="Right" BackColor="#cfcfcf" Font-Bold="True" ForeColor="red" Font-Size="11pt"></Footer>

                                                <%--<Cell Border-BorderColor="#DCDCDC"></Cell>--%> 
                                                <Cell Border-BorderStyle="None"></Cell>

                                                <CommandColumn  Border-BorderStyle="None"></CommandColumn>
                                            </Styles>
                                            <Columns>
                                                <dx:GridViewDataTextColumn FieldName="SelectAll" VisibleIndex="0" Caption="选择">
                                                    <Settings AllowCellMerge="False" />
                                                    <DataItemTemplate>
                                                        <asp:CheckBox ID="txtcb" runat="server" />
                                                    </DataItemTemplate>
                                                </dx:GridViewDataTextColumn>
                                            </Columns>
                                            <TotalSummary>
                                                <dx:ASPxSummaryItem DisplayFormat="<font color='red' Size='2'>合计:{0:N2}</font>" FieldName="notax_targettotal" ShowInColumn="notax_targettotal" ShowInGroupFooterColumn="notax_targettotal" SummaryType="Sum" />
                                            </TotalSummary>
                                        </dx:ASPxGridView>

                                      </ContentTemplate></asp:UpdatePanel>

                                </div>
                                <%--<div class="marks">
                                    <asp:Panel id="filecontainer" runat="server" GroupingText="附件">
                                        <div style="margin-top:10px">
                                            <asp:FileUpload runat="server" ID="file" AllowMultiple="true"  />
                                        </div>
                                    </asp:Panel>
                                    

                                </div>--%>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
           

            <div class="row  row-container">
                <div class="auto-style1" >
                    <div class="panel panel-infos">
                        <div class="panel-headings" data-toggle="collapse" data-target="#FJSC">
                             <strong>附件</strong>
                        </div>
                        <div class="panel-body collapse in" id="FJSC">
                            <div class="col-xs-12 col-sm-12  col-md-12 col-lg-12" style="width:1000px;">
                                <div>
                                     <dx:aspxuploadcontrol ID="uploadcontrol" runat="server" Width="500px" BrowseButton-Text="浏览"  Visible="true" ClientInstanceName="UploadControl" 
                                         ShowAddRemoveButtons="True" RemoveButton-Text="删除" UploadMode="Advanced"   AutoStartUpload="true" ShowUploadButton="false" ShowProgressPanel="true"
                                         onfileuploadcomplete="uploadcontrol_FileUploadComplete" >
                                         <AdvancedModeSettings EnableDragAndDrop="True" EnableFileList="True" EnableMultiSelect="True">
                                         </AdvancedModeSettings>
                                         <ClientSideEvents FileUploadComplete="onFileUploadComplete" />
                                    </dx:aspxuploadcontrol>
                                    <input type="hidden" id="ip_filelist" name="ip_filelist" runat="server" />                              
                                    <table id="tbl_filelist"  Width="500px">  
                                    </table>

                                    <asp:UpdatePanel runat="server" ID="p11" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <textarea id="ip_filelist_db" name="ip_filelist" runat="server" cols="200" rows="2" visible="false"></textarea>
                                            <asp:Table ID="tab1" Width="500px" runat="server">
                                            </asp:Table>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                            <br />
                        </div>
                    </div>
                </div>
            </div>

            <div class="row  row-container" style="display: ">
                <div class="col-md-12">
                    <div class="panel panel-infos">
                        <div class="panel-headings" data-toggle="collapse" data-target="#CZRZ">
                        </div>
                        <div class="panel-body ">
                            <table border="0" width="1200px" class="bg-info-">
                                <tr>
                                    <td width="100px">
                                        <label>处理意见：</label></td>
                                    <td>
                                        <textarea id="comment"  placeholder="请在此处输入处理意见" class="form-control" onchange="setComment(this.value)" ></textarea>

                                        
                                    </td>
                                </tr>

                            </table>
                        </div>
                    </div>
                </div>
            </div>


        </div>

        

    </form>
</body>
</html>

