﻿<%@ Page Title="请购申请单(PR)" Language="C#" AutoEventWireup="true" CodeFile="PUR_PR.aspx.cs" Inherits="PUR_PR" MaintainScrollPositionOnPostback="True" ValidateRequest="true" %>
 
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
    <link href="../../Content/css/custom.css" rel="stylesheet" />
    <style>        hidden { display:none
        }
    </style>
    <script src="../../../Content/js/plugins/bootstrap-select/js/bootstrap-select.min.js"></script>
    <script src="../../Scripts/RFlow.js"></script>
    <script type="text/javascript">
        var paramMap;
        $(document).ready(function () {
            $("#mestitle").html("【请购申请单(PR)】<a href='/userguide/reviewGuide.pptx' target='_blank' class='h4' style='display:none'>使用说明</a>");// 
             
            SetButtons();

            getTotalPrice();                                                
            //绑定物料信息
            getMatInfo();
            //工厂选择更改
            $("select[id*='domain']").change(function(){
                bindSelect("applydept");
                $("#deptm").val("");
            })
            //申请部门
            $("select[id*='applydept']").change(function(){
                var domain=$("#domain").val();
                var dept=$("#applydept").val();
                getDeptLeader(domain,dept);              
            })
                        
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
            
        });// end ready

        

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

                });

            }
        }

    </script>
   
    <style>
        #SQXX label{
            font-weight:400;
        }
        .lineread{
            font-size:9px; border:none; border-bottom:1px solid #ccc;
        }
        .linewrite{
            font-size:9px; border:none; border-bottom:1px solid #ccc;background-color:#FDF7D9;/*EFEFEF*/
        }
    </style>
</head>
<body>
    <script type="text/javascript">
	   <%-- var initData = <%=BWorkFlow.GetFormDataJsonString(initData)%>;--%>
        var fieldStatus = "1"=="<%=Request.QueryString["isreadonly"]%>"? {} : <%=fieldStatus%>;
        var fieldSet=<%=fieldStatus%>;
        var displayModel = '<%=DisplayModel%>';
        $(window).load(function (){

            SetControlStatus(<%=fieldStatus%>);
            SetControlStatus2(<%=fieldStatus%>);                      
                
        });

        //验证
        function validate(id){
            if($("#applydept").val()==""){
                layer.alert("请选择申请公司及部门.");
                return false;
            }
            if($("#deptm").val()==""){
                layer.alert("部门经理(或分管副总)未设定，请联系IT设定.");
                return false;
            }
            <%=ValidScript%>
            var flag=true;
            var msg="";
            if($("#gvdtl input[id*=wlh]").length==0){
                layer.alert("请添加物料后再提交发送.");
                return false;
            }
            //validate wlh
            $("#gvdtl input[id*=wlh]").each(function (){
                if( $(this).val()==""){
                    msg+="【物料号】不可为空.";
                    flag=false;
                    return false;
                }
            })
            //validate wlh
            $("#gvdtl input[id*=usefor]").each(function (){
                if( $(this).val()==""){
                    msg+="【用于产品/项目】不可为空.";
                    flag=false;
                    return false;
                }
            })
            //validate date
            $("#gvdtl input[id*=targetprice]").each(function (){
                if( $(this).val()==""){
                    msg+="【目标价】不可为空.";
                    flag=false;
                    return false;
                }
            })
            //validate qty
            $("#gvdtl input[id*=qty]").each(function (){
                if( $(this).val()==""){
                    msg+="【数量】不可为空.";
                    flag=false;
                    return false;
                }
            })
            //validate deliverydate
            $("#gvdtl input[id*=deliverydate]").each(function (){
                if( $(this).val()==""){
                    msg+="【要求到货日期】不可为空.";
                    flag=false;
                    return false;
                }
            })

            if(flag==false){  
                layer.alert(msg);
                return false;
            }
            if($("#upload").val()==""&& $("#link_upload").text()==""){
                layer.alert("请选择【附件】");
                return false;
            }           

        }
        //GetHistroyPrice
        function getHisToryPrice(p1,ctrl){
            var p2=$("#domain").val();
            
            $.ajax({
                type: "Post",async: false,
                url: "PUR_PR.aspx/GetHistoryPrice" , 
                //方法传参的写法一定要对，str为形参的名字,str2为第二个形参的名字
                //P1:wlh P2： 
                data: "{'P1':'"+p1+"','P2':'"+p2+"'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {//返回的数据用data.d获取内容//                        
                    // alert(data.d)
                    //$.each(eval(data.d), function (i, item) {  })                              
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
        function getDaoJuMatInfo(p1,wltype,wlsubtype,wlmc,wlms,attachments,attachments_name,wlh,historyprice){
            var p2=$("#domain").val();
            var p3=$("#prtype").val();
            if(p2==""){layer.alert("请选择【申请工厂】");return false;}
            $.ajax({
                type: "Post",
                url: "PUR_PR.aspx/GetDaoJuMatInfo" , 
                async: false,
                //方法传参的写法一定要对，str为形参的名字,str2为第二个形参的名字
                //P1:wlh P2： domaim
                data: "{'P1':'"+p1+"','P2':'"+p2+"','P3':'"+p3+"'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {//返回的数据用data.d获取内容//                        
                    //alert(data.d)
                    if (data.d == "") {
                        layer.msg("未获取到该物料信息,请确认是否有此物料且状态为AC.");   
                        $(wlh).val("");
                        $(wlmc).val(""); 
                        $(wlms).val("");
                        $(historyprice).val("");//add heguiqin
                    }
                    $.each(eval(data.d), function (i, item) {                                
                        if (data.d == "") {
                            layer.msg("未获取到该物料信息,请确认是否有此物料且状态为AC.");   
                            $(wlh).val("");
                            $(wlmc).val(""); 
                            $(wlms).val("");
                            $(historyprice).val("");//add heguiqin
                        }
                        else {
                            if(item.ispodsched=="1"){
                                layer.alert(item.wlh+" 为日程物料号，无需提请购单.");
                                $(wlh).val("");
                                $(wlmc).val(""); 
                                $(wlms).val("");
                                $(historyprice).val("");//add heguiqin
                                return false;
                            }
                            else{
                                if(item.wlmc==""||item.ms==""){
                                    layer.alert(item.wlh+"请维护完整物料信息后再申请.");
                                    $(wlh).val("");
                                    $(wlmc).val(""); 
                                    $(wlms).val("");
                                    $(historyprice).val("");//add heguiqin
                                    return false;
                                }else{
                                    $(wltype).val(item.class);//$(wltype).attr("readonly","readonly")
                                    $(wlsubtype).val(item.type);//$(wlsubtype).attr("readonly","readonly");                           
                                    $(wlmc).val(item.wlmc);//$(wlmc).attr("readonly","readonly")
                                    $(wlms).val(item.ms);//$(wlms).attr("readonly","readonly")
                                    $(attachments).val(item.upload);//$(attachments).attr("readonly","readonly");

                                    if(p3 == "存货(刀具类)"){
                                        $(attachments_name[0]).prop("href",item.upload); 
                                    }else {
                                        $(attachments_name[0]).text("无");
                                    }
                                }
                            }
                        } 
                    })                  
                },
                error: function (err) {
                    layer.alert(err);
                }
            });
        }

        function getDeptLeader(domain,dept){             
            $.ajax({
                type: "Post",async: false,
                url: "PUR_PR.aspx/getDeptLeaderByDept" , 
                //方法传参的写法一定要对，str为形参的名字,str2为第二个形参的名字
                //P1:wlh P2： 
                data: "{'domain':'"+domain+"','dept':'"+dept+"'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {//返回的数据用data.d获取内容//                        
                     //alert(data.d)
                    //$.each(eval(data.d), function (i, item) {  })                              
                    if (data.d == "") {
                        layer.alert("未获取到部门主管,请联系IT确认.");                            
                    }
                    else {                        
                        $("#deptm").val(data.d );
                    }                   
                },
                error: function (err) {
                    layer.alert(err);
                }
            });
        };
                       
        function bindSelect(sel){
            var domain=$("#domain").val();
            $.ajax({
                type: "Post",async: false,
                url: "PUR_PR.aspx/GetDeptByDomain" , 
                //方法传参的写法一定要对，str为形参的名字,str2为第二个形参的名字
                //P1:wlh P2： 
                data: "{'P1':'"+domain+"','P2':'"+""+"'}",
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
                                var option = $("<option>").val(item.value).text(item.text); 
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
        function GetXMH() {
            popupwindow = window.open('../Select/select_XMLJ.aspx', '_blank', 'height=500,width=800,resizable=no,menubar=no,scrollbars =no,location=no');
        }
        function GetEmp() {
            popupwindow = window.open('../Select/select_Emp.aspx?ctrl1=txtProbEmp', '_blank', 'height=500,width=800,resizable=no,menubar=no,scrollbars =no,location=no');
        }
        function setvalue(ctrl0, keyValue0, ctrl1, keyValue1, ctrl2, keyValue2) {

            $("input[id*='" + ctrl0 + "']").val(keyValue0);
            $("input[id*='" + ctrl1 + "']").val(keyValue1);
            $("input[id*='" + ctrl2 + "']").val(keyValue2);
            popupwindow.close();
            $("input[id*='" + ctrl0 + "']").change();
        }
        
        function Getdaoju(e)
        {   
            //var ss = e.id.split("_");// 在每个逗号(,)处进行分解。
            //var url = "../../select/sz_report_dev_select.aspx?id=" + ss[4] + "&domain=" + $("select[id*='domain']").val()           
            //popupwindow = window.open(url, '_blank', 'height=500,width=1000,resizable=yes,menubar=no,scrollbars =yes,location=no');
            
            var ss = e.id.split("_");// 在每个逗号(,)处进行分解。
            var url="";
            var prtype=$("#prtype").val();
            if (prtype=="存货(刀具类)") {
                url = "/select/sz_report_dev_select.aspx?id=" + ss[4] + "&domain=" + $("select[id*='domain']").val();
            }else {
                url = "/select/select_pt_mstr.aspx?id=" + ss[4] + "&domain=" + $("select[id*='domain']").val();
            }                      
            popupwindow = window.open(url, '_blank', 'height=500,width=1000,resizable=yes,menubar=no,scrollbars =yes,location=no');
        }
        function setvalue_dj(id, wlh, ms, lx, js, sm, pp, gys,wlmc)
        { //gvdtl_cell0_5_wlh_0
            $("input[id*='gvdtl_cell" + id + "_4_wlh_" + id+ "']").val(wlh);
            $("input[id*='gvdtl_cell" + id + "_5_wlmc_" + id + "']").val(wlmc);
            $("input[id*='gvdtl_cell" + id + "_6_wlms_" + id + "']").val(ms);
            popupwindow.close();
            $("input[id*='gvdtl_cell" + id + "_4_wlh_" + id+ "']").change();
                
        }
        //计算总价 
        function getTotalPrice(){                                                      
            $("#gvdtl").find("tr td input[id*=qty],tr td input[id*=targetprice]").each(function () { 
                $(this).bind("change", function () {                                                          
                    var price = $(this).parent().parent().find("input[id*=targetprice]").val(); 
                    var qty = $(this).parent().parent().find("input[id*=qty]").val();  
                    price= (price==""||price=="NaN")? 0 : price;
                    qty= (qty==""||qty=="NaN")? 0 : qty;
                    if(price!=null&&qty!="")
                    {   
                        var result = (parseFloat(price) * parseFloat(qty)) ; 
                        $(this).parent().parent().find("input[id*=targettotal]").val(result); 
                    }else{  
                        $(this).parent().parent().find("input[id*=targettotal]").val("");
                    }
                    //计算所有明细总价                                 
                    getTotalMoney();
                }); 
            });
        }
        function getTotalMoney(){
            //计算所有明细总价            
            var totalMoney=0;
            $("#gvdtl").find("tr td input[id*=targettotal]").each(function (i) {
                var rowval=$("tr td input[id*=targettotal_"+i+"]").val();
                rowval= (rowval==""||rowval=="NaN")? 0 : rowval;                
                totalMoney=totalMoney+parseFloat(rowval)
                $("#totalMoney").val(totalMoney);

                //grid底部total值更新
                $('table[id*=gvdtl] tr[id*=DXFooterRow]').find('td').each(function () {
                    if($.trim($(this).text())!=""){
                        $(this).text("合计:"+fmoney(totalMoney,2));
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
            $("#gvdtl").find("tr td input[id*=wlh]").each(function () { 
                $(this).bind("change", function () {  
                    wlh=this.id;
                    var obj=$(this).parent().parent().find("input[id*=historyprice]");                                                         
                    //赋历史采购价
                    getHisToryPrice($(this).val(),obj[0].id); 
                    //物料信息                                                      
                    var wlType= $(this).parent().parent().find("input[id*=wltype]");
                    var wlSubType= $(this).parent().parent().find("input[id*=wlsubtype]");
                    var wlmc= $(this).parent().parent().find("input[id*=wlmc]");
                    var wlms= $(this).parent().parent().find("input[id*=wlms]");
                    var attachments= $(this).parent().parent().find("input[id*=attachments]");
                    var attachments_name= $(this).parent().parent().find("a[id*=attachments_name]");
                    getDaoJuMatInfo($(this).val(),wlType,wlSubType,wlmc,wlms,attachments,attachments_name,$(this),obj);                                                       
                }); 
            });
        }
    </script>

    <script>//20181108 add heguiqin
        function Add_check(){
            var domain=$("#domain").val();
            var dept=$("#applydept").val();

            if (domain=="" || dept=="") {
                layer.alert("请选择【申请公司】");
                return false;
            }

            return true;
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
                    <div class="panel panel-info">
                        <div class="panel-heading" data-toggle="collapse" data-target="#SQXX">
                            <strong>申请记录基础信息</strong>
                        </div>
                        <div class="panel-body collapse in" id="SQXX">
                            <div class="col-xs-12 col-sm-12  col-md-12 col-lg-12" style="width:1000px;">
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
                                                    <td>请购单号：</td>
                                                    <td><%--CssClass="form-control input-s-sm"--%>
                                                        <asp:TextBox ID="PRNo" runat="server" CssClass="lineread" readonly="true" Width="247px" ToolTip="1|0"   />
                                                    </td>
                                                    <td>申请日期：</td>
                                                    <td><%--CssClass="form-control input-s-sm"--%>
                                                        <asp:TextBox ID="CreateDate" CssClass="lineread" Style="height: 27px; width: 200px" runat="server" ReadOnly="True" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>申请人：</td>
                                                    <td>
                                                        <div class="form-inline"><%--CssClass="form-control input-s-sm"--%>
                                                            <asp:TextBox runat="server" ID="CreateById" CssClass="lineread" Style="height: 27px; width: 70px" ReadOnly="True"></asp:TextBox>
                                                            <asp:TextBox runat="server" ID="CreateByName" CssClass="lineread" Style="height: 27px; width: 70px" ReadOnly="True"></asp:TextBox>                                                           
                                                            <asp:TextBox runat="server" id="DeptName" cssclass="lineread" style="height: 27px; width: 100px" readonly="True" />
                                                        </div>
                                                    </td>                                                    
                                                    <td>电话（分机）：
                                                    </td>
                                                    <td><%--CssClass="form-control input-s-sm"--%>                                                        
                                                        <asp:TextBox id="phone" class="linewrite" style="height: 27px; width: 200px" runat="server"  />                                                            
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>当前登陆人：</td>
                                                    <td>
                                                        <div class="form-inline"><%--CssClass="form-control input-s-sm"--%>
                                                            <input id="txt_LogUserId" class="lineread" style="height: 27px; width: 70px" runat="server" readonly="True" />
                                                            <input id="txt_LogUserName" class="lineread" style="height: 27px; width: 70px" runat="server" readonly="True" />
                                                            <input id="txt_LogUserDept" class="lineread" style="height: 27px; width: 100px" runat="server" readonly="True" />                                                           
                                                        </div>
                                                    </td>                                                    
                                                    <td>申请公司：</td>
                                                    <td>   
                                                        <div style="float:left"><%--CssClass="form-control input-s-sm"--%>
                                                            <asp:DropDownList ID="domain" CssClass="linewrite" runat="server" Width="100px" Height="27px" ToolTip="0|1"  >
                                                                <asp:ListItem Value="" Text=""></asp:ListItem>
                                                                <asp:ListItem Value="200" Text="昆山工厂"></asp:ListItem>
                                                                <asp:ListItem Value="100" Text="上海工厂"></asp:ListItem>
                                                            </asp:DropDownList></div>  
                                                        <div style="float:left"><%--CssClass="form-control input-s-sm"--%>
                                                            <asp:DropDownList ID="applydept" CssClass="linewrite" runat="server" Width="100px" Height="27px" ToolTip="0|1" >
                                                            </asp:DropDownList>
                                                        </div>

                                                         <asp:TextBox id="deptm"  style=" width: 20px;display:none" runat="server"  />
                                                         <asp:TextBox id="deptmfg"  style=" width: 20px;display:none" runat="server"  />                                              
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
                    <div class="panel panel-info">
                        <div class="panel-heading" data-toggle="collapse" data-target="#PA">
                            <strong>用途信息</strong>
                        </div>
                        <div class="panel-body " id="PA">
                            <div class="col-xs-12 col-sm-12  col-md-12 col-lg-12" style="width:1000px;">
                                <div>
                                    <table style="" border="0" runat="server" id="tblWLLeibie">
                                        <tr>
                                            <td>采购类别：</td>
                                            <td ><%--CssClass="form-control"--%>
                                                <asp:DropDownList ID="prtype" runat="server" CssClass="linewrite" ToolTip="0|1" Width="200px" Height="27px"
                                                    AutoPostBack="True" OnSelectedIndexChanged="prtype_SelectedIndexChanged"></asp:DropDownList>
                                            </td>
                                        </tr>                                        
                                        <tr>
                                            <td>申请原因描述：</td>
                                            <td style="width:800px"><%--CssClass="form-control input-s-sm"--%>
                                                <asp:textbox ID="prreason" TextMode="MultiLine" runat="server" CssClass="linewrite" Width="100%" ToolTip="0|0"   />                                               
                                            </td>
                                        </tr>
                                    </table>
                                     <asp:TextBox ID="totalMoney" runat="server"  Width="80px" ToolTip="0|0" CssClass=" hidden"  />   
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row  row-container">
                <div class="col-md-12">
                    <div class="panel panel-info">
                        <div class="panel-heading" data-toggle="collapse" data-target="#PB">
                            <strong style="padding-right: 100px">采购物品信息<span id="warning" style="color: red; display: none"> </span></strong>
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
                                                
                                                //绑定物料信息
                                                getMatInfo();
                                                //
                                                getTotalMoney();
                                               SetControlStatus2(<%=fieldStatus%>);
                                            });
                                        </script>
                                        
                                                                                      
                                    <asp:Button ID="btnAddDetl" runat="server" Text="添加" CssClass="btn btn-primary btn-sm" OnClientClick="return Add_check()"  OnClick="btnAddDetl_Click"/>
                                    <asp:Button ID="btnDelete" runat="server" Text="删除"  CssClass="btn btn-primary btn-sm"   OnClick="btnDelete_Click"/>
                                    
                                        <dx:ASPxGridView ID="gvdtl" runat="server" Width="1200px" ClientInstanceName="grid" KeyFieldName="rowid" EnableTheming="True" Theme="MetropolisBlue">
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
                                                <Header BackColor="#E4EFFA"></Header>   
                                                <SelectedRow BackColor="#FDF7D9"></SelectedRow>                                               
                                                <Footer HorizontalAlign="Right"></Footer>
                                            </Styles>
                                            <Styles>   
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
                                                <dx:ASPxSummaryItem DisplayFormat="合计:{0:N2}" FieldName="targettotal" ShowInColumn="targettotal" ShowInGroupFooterColumn="targettotal" SummaryType="Sum" />
                                                
                                            </TotalSummary>
                                        </dx:ASPxGridView>

                                      </ContentTemplate></asp:UpdatePanel>

                                </div>
                                <div class="marks">
                                    <asp:Panel id="filecontainer" runat="server" GroupingText="附件">
                                        <div style="margin-top:10px">
                                            <asp:FileUpload runat="server" ID="file" AllowMultiple="true"  />
                                        </div>
                                    </asp:Panel>
                                    

                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
           

            <div class="row  row-container" style="display: ">
                <div class="col-md-12">
                    <div class="panel panel-info">
                        <div class="panel-heading" data-toggle="collapse" data-target="#CZRZ">
                        </div>
                        <div class="panel-body ">
                            <table border="0" width="100%" class="bg-info">
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

