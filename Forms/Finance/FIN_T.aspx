<%@ Page Title="差旅申请单" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="FIN_T.aspx.cs" Inherits="Forms_Finance_FIN_T" 
     MaintainScrollPositionOnPostback="True" ValidateRequest="false"  enableEventValidation="false" %>

<%@ Register assembly="DevExpress.Web.v17.2, Version=17.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web" tagprefix="dx" %>

<asp:Content ID="HeaderContent" ContentPlaceHolderID="HeadContent" Runat="Server">
    <link href="/Content/css/bootstrap.min.css" rel="stylesheet" />
    <script src="/Content/js/jquery.min.js"></script>
    <script src="/Content/js/bootstrap.min.js"></script>
    <script src="/Content/js/layer/layer.js"></script>
    <script src="/Content/js/plugins/layer/laydate/laydate.js"></script>

    <script type="text/javascript">
        function getQueryString(name) {
            var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)", "i");
            var r = decodeURI(window.location.search).substr(1).match(reg);
            if (r != null) return unescape(r[2]); return null;
        }
        var stepid = getQueryString("stepid");

        $(document).ready(function () {
            $("#mestitle").html("【差旅申请单】<a href='/userguide/TGuide.pps' target='_blank' class='h5' style='color:red'>使用说明</a>");

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

            Ini_Set_IsHrReserve();//初始化部分行金额控件是否只读

            $("#CJJH select[id*='TravelType']").change(function () {  
                Auto_Calculate_T007();
            });
            is_hr_yz_set();

            SetGirdDateDrop_Read();
        });

        function Ini_Set_IsHrReserve(){
            var ini_bf=false;
            $("[id$=gv_d] tr[class*=DataRow]").each(function (index, item) { 

                if($(item).children("td:last-child").text()!="T001" && $(item).children("td:last-child").text()!="T002" && $(item).children("td:last-child").text()!="T000"){
                    $(item).find("table[id*=IsHrReserve]").css("display","none");
                    $("#IsHrReserve_i_"+index).css("display","none");
                }

                if($(item).children("td:last-child").text()=="T001" || $(item).children("td:last-child").text()=="T002" || $(item).children("td:last-child").text()=="T000"){
                    var IsHrReserve = eval('IsHrReserve' + index);                  
                    if (IsHrReserve.GetText()=="是") {                   
                        $(item).find("table[id*=BudgetStandardCost]").addClass("dxeTextBox_read");
                        $(item).find("input[id*=BudgetStandardCost]").attr("readOnly","readOnly").addClass("dxeTextBox_read");

                        $(item).find("table[id*=BudgetCount]").addClass("dxeTextBox_read");
                        $(item).find("input[id*=BudgetCount]").attr("readOnly","readOnly").addClass("dxeTextBox_read");

                        $(item).find("table[id*=BudgetPersonCount]").addClass("dxeTextBox_read");
                        $(item).find("input[id*=BudgetPersonCount]").attr("readOnly","readOnly").addClass("dxeTextBox_read");

                        $(item).find("table[id*=BudgetOtherCost]").addClass("dxeTextBox_read");
                        $(item).find("input[id*=BudgetOtherCost]").attr("readOnly","readOnly").addClass("dxeTextBox_read");

                        //$(item).find("table[id*=BudgetTotalCost]").addClass("dxeTextBox_read");
                        //$(item).find("input[id*=BudgetTotalCost]").attr("readOnly","readOnly").addClass("dxeTextBox_read");
                        
                        ini_bf=true;      
                    }else {
                        $(item).find("table[id*=BudgetStandardCost]").removeClass("dxeTextBox_read");
                        $(item).find("input[id*=BudgetStandardCost]").removeAttr("readOnly").removeClass("dxeTextBox_read");

                        $(item).find("table[id*=BudgetCount]").removeClass("dxeTextBox_read");
                        $(item).find("input[id*=BudgetCount]").removeAttr("readOnly").removeClass("dxeTextBox_read");

                        $(item).find("table[id*=BudgetPersonCount]").removeClass("dxeTextBox_read");
                        $(item).find("input[id*=BudgetPersonCount]").removeAttr("readOnly").removeClass("dxeTextBox_read");

                        $(item).find("table[id*=BudgetOtherCost]").removeClass("dxeTextBox_read");
                        $(item).find("input[id*=BudgetOtherCost]").removeAttr("readOnly").removeClass("dxeTextBox_read");

                        //$(item).find("table[id*=BudgetTotalCost]").removeClass("dxeTextBox_read");
                        //$(item).find("input[id*=BudgetTotalCost]").removeAttr("readOnly").removeClass("dxeTextBox_read");
                    }                    
                }
                if($(item).children("td:last-child").text()=="T007"){
                    //var BudgetStandardCost = eval('BudgetStandardCost' + index); var BudgetCount = eval('BudgetCount' + index); var BudgetPersonCount = eval('BudgetPersonCount' + index);
                    //var BudgetTotalCost = eval('BudgetTotalCost' + index);

                    $(item).find("table[id*=BudgetStandardCost]").addClass("dxeTextBox_read");
                    $(item).find("input[id*=BudgetStandardCost]").attr("readOnly","readOnly").addClass("dxeTextBox_read");

                    $(item).find("table[id*=BudgetCount]").addClass("dxeTextBox_read");
                    $(item).find("input[id*=BudgetCount]").attr("readOnly","readOnly").addClass("dxeTextBox_read");

                    $(item).find("table[id*=BudgetPersonCount]").addClass("dxeTextBox_read");
                    $(item).find("input[id*=BudgetPersonCount]").attr("readOnly","readOnly").addClass("dxeTextBox_read");

                    $(item).find("table[id*=BudgetOtherCost]").addClass("dxeTextBox_read");
                    $(item).find("input[id*=BudgetOtherCost]").attr("readOnly","readOnly").addClass("dxeTextBox_read");

                    //$(item).find("table[id*=BudgetTotalCost]").addClass("dxeTextBox_read");
                    //$(item).find("input[id*=BudgetTotalCost]").attr("readOnly","readOnly").addClass("dxeTextBox_read");
                }
                if($(item).children("td:last-child").text()=="T003"){//出租车/公交/地铁等
                    var BudgetPersonCount = eval('BudgetPersonCount' + index);
                    BudgetPersonCount.SetText(1);

                    $(item).find("table[id*=BudgetPersonCount]").addClass("dxeTextBox_read_2");
                    $(item).find("input[id*=BudgetPersonCount]").attr("readOnly","readOnly").addClass("dxeTextBox_read_2");
                } 
                if($(item).children("td:last-child").text()=="T008"){//自驾费用
                    var BudgetStandardCost = eval('BudgetStandardCost' + index);
                    BudgetStandardCost.SetText(1);

                    $(item).find("table[id*=BudgetStandardCost]").addClass("dxeTextBox_read");
                    $(item).find("input[id*=BudgetStandardCost]").attr("readOnly","readOnly").addClass("dxeTextBox_read");
                }

            });
            if (ini_bf) {
                $("#CJJH input[id*='IsHrReserveByForm']").val("是");

                $("#div_dtl_hr").css("display","block");
            }else {
                $("#CJJH input[id*='IsHrReserveByForm']").val("否");

                $("#div_dtl_hr").css("display","none");
            }
            
        }

        function Auto_Calculate(){//计算预计出差天数
            var PlanStartTime=$("#CJJH input[id*='PlanStartTime']").val();
            var PlanEndTime=$("#CJJH input[id*='PlanEndTime']").val();
            var PlanDays=0;
            if(PlanStartTime!="" && PlanEndTime!=""){
                var s1 = new Date(PlanStartTime);
                var s2 = new Date(PlanEndTime);

                var days = s2.getTime() - s1.getTime();
                PlanDays = parseInt(days / (1000 * 60 * 60 * 24));
            } 
            if(PlanDays<0){
                layer.alert("【预计结束日期】必须大于等于【预计出发日期】.");
            }
            $("#CJJH input[id*='PlanDays']").val(PlanDays);
            Auto_Calculate_T007();
        }       
        
        function Auto_Calculate_T007(){
            var BTC_T007=0;

            var PlanDays=parseInt($("#CJJH input[id*='PlanDays']").val());
            var PA_len=0;
            if($("#CJJH input[id*='PlanAttendant']").val()!=""){
                var PA_Arry=($("#CJJH input[id*='PlanAttendant']").val()).split(',');
                PA_len=PA_Arry.length;
            }

            var TravelType=$("#CJJH select[id*='TravelType']").val();
            var cost=0;
            if(TravelType=="国内"){cost=100;}
            if(TravelType=="国外"){cost=500;}

            BTC_T007=PlanDays*(PA_len+1)*cost;//随行人员+申请人  *  天数 * 每天费用

            $("[id$=gv_d] tr[class*=DataRow]").each(function (index, item) { 
                if($(item).children("td:last-child").text()=="T007"){
                    var BudgetStandardCost = eval('BudgetStandardCost' + index); var BudgetCount = eval('BudgetCount' + index); var BudgetPersonCount = eval('BudgetPersonCount' + index);
                    var BudgetTotalCost = eval('BudgetTotalCost' + index);

                    BudgetStandardCost.SetText(cost);BudgetCount.SetText(PlanDays);BudgetPersonCount.SetText(PA_len+1);
                    BudgetTotalCost.SetText(BTC_T007);
                    RefreshRow();
                }
            });

        }

        function is_hr_yz_set(){
            var is_hr_zy = '<%=is_hr_zy%>';

            if (is_hr_zy=="Y") {
                $("[id$=gv_d_hr] tr[class*=DataRow]").each(function (index, item) { 
                    $(item).find("table[id*=ScheduledFlight]").css("border-width","1px");
                    $(item).find("input[id*=ScheduledFlight]").removeAttr("readonly");
                    $(item).find("table[id*=ActualCost]").css("border-width","1px");
                    $(item).find("input[id*=ActualCost]").removeAttr("readonly");
                });
            }
            //else {
            //    $("[id$=gv_d_hr] tr[class*=DataRow]").each(function (index, item) { 
            //        $(item).find("table[id*=ScheduledFlight]").css("border-width","0px");
            //        $(item).find("input[id*=ScheduledFlight]").attr("readOnly","readOnly");
            //        $(item).find("table[id*=ActualCost]").css("border-width","1px");
            //        $(item).find("input[id*=ActualCost]").attr("readOnly","readOnly");
            //    });
            //}
        }

        function SetGirdDateDrop_Read(){
            $("[id$=gv_d_hr] tr[class*=dxgvDataRow]").each(function (index, item) { 
                var StartDateTime = eval('StartDateTime' + index);
                if (!StartDateTime.GetEnabled()) {
                    $(item).find("td[id*=StartDateTime]").css("display","none");
                }
            });
        }

        //提出自定流程 JS 
        function setComment(val) {
            $('#comment', parent.document).val(val);
        }

        function gv_d_hr_SelectionChanged(s, e) {

            gv_d_hr_color();

        }

        function gv_d_hr_color(){
            $("[id$=gv_d_hr] tr[class*=DataRow]").each(function (index, item) { 

                var class_checked=$.trim($(item).find("td:eq(0) span:first").attr("class"));                        
                if(class_checked.indexOf("CheckBoxChecked")>-1){                    
                    $(item).find("table[id*=TravelerName_"+index+"]").css("background-color","#FDF7D9");
                    $(item).find("input[id*=TravelerName_"+index+"]").css("background-color","#FDF7D9");

                    $(item).find("table[id*=TravelerId_"+index+"]").css("background-color","#FDF7D9");
                    $(item).find("input[id*=TravelerId_"+index+"]").css("background-color","#FDF7D9");

                    $(item).find("table[id*=Vehicle_"+index+"]").css("background-color","#FDF7D9");
                    $(item).find("input[id*=Vehicle_"+index+"]").css("background-color","#FDF7D9");
                }else {
                    $(item).find("table[id*=TravelerName_"+index+"]").css("background-color","#FFFFFF");
                    $(item).find("input[id*=TravelerName_"+index+"]").css("background-color","#FFFFFF");

                    $(item).find("table[id*=TravelerId_"+index+"]").css("background-color","#FFFFFF");
                    $(item).find("input[id*=TravelerId_"+index+"]").css("background-color","#FFFFFF");

                    $(item).find("table[id*=Vehicle_"+index+"]").css("background-color","#FFFFFF");
                    $(item).find("input[id*=Vehicle_"+index+"]").css("background-color","#FFFFFF");
                }

                if($(item).find("input[id*=StartDateTime_"+index+"]").attr("disabled")){
                    var class_checked=$.trim($(item).find("td:eq(0) span:first").attr("class"));                        
                    if(class_checked.indexOf("CheckBoxChecked")>-1){                    
                        $(item).find("table[id*=StartDateTime_"+index+"]").css("background-color","#FDF7D9");
                        $(item).find("input[id*=StartDateTime_"+index+"]").css("background-color","#FDF7D9");
                    }else {
                        $(item).find("table[id*=StartDateTime_"+index+"]").css("background-color","#FFFFFF");
                        $(item).find("input[id*=StartDateTime_"+index+"]").css("background-color","#FFFFFF");
                    }
                }else {
                    
                }
               
                if($(item).find("input[id*=StartFromPlace_"+index+"]").attr("readOnly")){
                    var class_checked=$.trim($(item).find("td:eq(0) span:first").attr("class"));                        
                    if(class_checked.indexOf("CheckBoxChecked")>-1){                    
                        $(item).find("table[id*=StartFromPlace_"+index+"]").css("background-color","#FDF7D9");
                        $(item).find("input[id*=StartFromPlace_"+index+"]").css("background-color","#FDF7D9");
                    }else {
                        $(item).find("table[id*=StartFromPlace_"+index+"]").css("background-color","#FFFFFF");
                        $(item).find("input[id*=StartFromPlace_"+index+"]").css("background-color","#FFFFFF");
                    }
                }else {
                    
                }

                if($(item).find("input[id*=EndToPlace_"+index+"]").attr("readOnly")){
                    var class_checked=$.trim($(item).find("td:eq(0) span:first").attr("class"));                        
                    if(class_checked.indexOf("CheckBoxChecked")>-1){                    
                        $(item).find("table[id*=EndToPlace_"+index+"]").css("background-color","#FDF7D9");
                        $(item).find("input[id*=EndToPlace_"+index+"]").css("background-color","#FDF7D9");
                    }else {
                        $(item).find("table[id*=EndToPlace_"+index+"]").css("background-color","#FFFFFF");
                        $(item).find("input[id*=EndToPlace_"+index+"]").css("background-color","#FFFFFF");
                    }
                }else {
                    
                }

                if($(item).find("input[id*=BudgetCost_"+index+"]").attr("readOnly")){
                    var class_checked=$.trim($(item).find("td:eq(0) span:first").attr("class"));                        
                    if(class_checked.indexOf("CheckBoxChecked")>-1){                    
                        $(item).find("table[id*=BudgetCost_"+index+"]").css("background-color","#FDF7D9");
                        $(item).find("input[id*=BudgetCost_"+index+"]").css("background-color","#FDF7D9");
                    }else {
                        $(item).find("table[id*=BudgetCost_"+index+"]").css("background-color","#FFFFFF");
                        $(item).find("input[id*=BudgetCost_"+index+"]").css("background-color","#FFFFFF");
                    }
                }else {
                    
                }

                if($(item).find("input[id*=Remark_"+index+"]").attr("readOnly")){
                    var class_checked=$.trim($(item).find("td:eq(0) span:first").attr("class"));                        
                    if(class_checked.indexOf("CheckBoxChecked")>-1){                    
                        $(item).find("table[id*=Remark_"+index+"]").css("background-color","#FDF7D9");
                        $(item).find("input[id*=Remark_"+index+"]").css("background-color","#FDF7D9");
                    }else {
                        $(item).find("table[id*=Remark_"+index+"]").css("background-color","#FFFFFF");
                        $(item).find("input[id*=Remark_"+index+"]").css("background-color","#FFFFFF");
                    }
                }else {
                    
                }


                if($(item).find("input[id*=ScheduledFlight_"+index+"]").attr("readOnly")){
                    var class_checked=$.trim($(item).find("td:eq(0) span:first").attr("class"));                        
                    if(class_checked.indexOf("CheckBoxChecked")>-1){                    
                        $(item).find("table[id*=ScheduledFlight_"+index+"]").css("background-color","#FDF7D9");
                        $(item).find("input[id*=ScheduledFlight_"+index+"]").css("background-color","#FDF7D9");
                    }else {
                        $(item).find("table[id*=ScheduledFlight_"+index+"]").css("background-color","#FFFFFF");
                        $(item).find("input[id*=ScheduledFlight_"+index+"]").css("background-color","#FFFFFF");
                    }
                }else {
                    
                }

                if($(item).find("input[id*=ActualCost_"+index+"]").attr("readOnly")){
                    var class_checked=$.trim($(item).find("td:eq(0) span:first").attr("class"));                        
                    if(class_checked.indexOf("CheckBoxChecked")>-1){                    
                        $(item).find("table[id*=ActualCost_"+index+"]").css("background-color","#FDF7D9");
                        $(item).find("input[id*=ActualCost_"+index+"]").css("background-color","#FDF7D9");
                    }else {
                        $(item).find("table[id*=ActualCost_"+index+"]").css("background-color","#FFFFFF");
                        $(item).find("input[id*=ActualCost_"+index+"]").css("background-color","#FFFFFF");
                    }
                }else {
                    
                }

                
            });
        }

        //设定表字段状态（可编辑性）
        var tabName="pgi_fin_t_main_form";//表名
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
        var tabName2="pgi_fin_t_dtl_form";//表名
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
        var tabName3="pgi_fin_t_dtl_hr_form";//表名
        function SetControlStatus3(fieldStatus)
        {  // tabName_columnName:1_0
            var flag=true;
            for(var item in fieldStatus){
                var id=""+item.replace(tabName3.toLowerCase()+"_","");
                
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
            SetControlStatus3(<%=fieldStatus%>); 

	    });

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
        
        .dxeTextBox {
            border:1px solid #ccc;
        }
        .dxeButtonEdit{
            border:1px solid #ccc;
        }
        .dxeButtonEditButton, .dxeCalendarButton, .dxeSpinIncButton, .dxeSpinDecButton, .dxeSpinLargeIncButton, .dxeSpinLargeDecButton, .dxeColorEditButton{
            border:1px solid #ccc;
        }

        #SQXX label{
            font-weight:400;padding-bottom:3px;
        }
        #CJJH label{
            font-weight:400;
        }
        .lineread{
            font-size:12px; border:none; border-bottom:1px solid #ccc; height: 23px;
        }
        .linewrite{
            font-size:12px; border:none; border-bottom:1px solid #ccc;background-color:#FDF7D9;height: 23px;
        }
        .dxeTextBox_read{
            border:none !important ;
        }
        .dxeTextBox_read_2{
            border:none !important ;color:white !important;
        }
        #div_dtl td{
             padding-bottom:3px;
        }
        #div_dtl_hr td{
             padding-bottom:3px;
        }
        .dxeButtonEdit td{
             padding-bottom:0px;
        }
        .hr{
            color:blue; font-weight:800;
        }
        .i_hidden{
            display:none;
        }
        .i_show{
            display:inline-block;
        }
        #div_T004 td{
            border:1px Solid #c0c0c0;
            width:25%
        }
    </style>

    <script type="text/javascript">
        function con_sure(){
            if (gv_d_hr.GetSelectedRowCount() <= 0) { layer.alert("请选择要删除的记录!"); return false; }
            //询问框
            return confirm('确认要删除吗？');
        }

        function Get_ApplyId(){
            var url = "/select/select_ApplyId.aspx?para=travel";

            layer.open({
                title:'申请人选择',
                type: 2,
                area: ['700px', '450px'],
                fixed: false, //不固定
                maxmin: true,
                content: url
            });         
        }

        function setvalue_ApplyId(workcode,lastname ,ITEMVALUE ,dept_name , domain ,gc ,jobtitlename ,telephone, car) {

            $("#SQXX input[id*='ApplyId']").val(workcode);
            $("#SQXX input[id*='ApplyName']").val(lastname);
            $("#SQXX input[id*='ApplyTelephone']").val(telephone);
            $("#SQXX input[id*='ApplyJobTitleName']").val(jobtitlename);
            $("#SQXX input[id*='ApplyDomain']").val(domain);
            $("#SQXX input[id*='ApplyDomainName']").val(gc);
            $("#SQXX input[id*='ApplyDeptId']").val(ITEMVALUE);
            $("#SQXX input[id*='ApplyDeptName']").val(dept_name);
            
        }

        function Get_PlanAttendant(){
           var url = "/select/select_PlanAttendant.aspx?ApplyId="+$("#SQXX input[id*='ApplyId']").val()+"&PA="+$("#CJJH input[id*='PlanAttendant']").val();

            layer.open({
                title:'随行人员选择',
                type: 2,
                area: ['450px', '550px'],
                fixed: false, //不固定
                maxmin: true,
                content: url
            });         
        }

        function setvalue_PlanAttendant(values) {
            /*//新选择的为空，就为空；否则就是原来选择的，加上新选择的：此种方法还需要去重，所以舍弃方法
            var oldstr=$("#CJJH input[id*='PlanAttendant']").val();
            if(oldstr!=""){oldstr=oldstr+',';}

            var newstr="";
            for (var i = 0; i < values.length; i++) { 
                newstr=newstr+values[i][0]+'('+values[i][1]+')';
                if(i!=values.length-1){newstr=newstr+',';}
            }
            
            var str="";
            if(newstr!=""){str=oldstr+newstr;}

            $("input[id*='PlanAttendant']").val(str);
            */

            var str="";
            for (var i = 0; i < values.length; i++) { 
                str=str+values[i][0]+'('+values[i][1]+')';
                if(i!=values.length-1){str=str+',';}
            }
            $("input[id*='PlanAttendant']").val(str);

            Auto_Calculate_T007();
        }

        /*function Show_limit(vi){
            var bf_T004=false;
            $("[id$=gv_d] tr[class*=DataRow]").each(function (index, item) { 
                if(vi==index){
                    if($(item).children("td:last-child").text()=="T004"){
                        bf_T004=true;
                        return;
                    }
                }
            });

            if(bf_T004){
                //页面层
                layer.open({
                    title:'<font color="red">附表S 国内旅行住宿标准（人民币）</font>',
                    type: 1,
                    area: ['550px', '260px'], //宽高
                    content: $('#div_T004'), //这里content是一个DOM，注意：最好该元素要存放在body最外层，否则可能被其它的相对元素所影响
                });
            }

        }*/

        function Show_limit(){
            //页面层
            layer.open({
                title:'<font color="red">附表S 国内旅行住宿标准（人民币）</font>',
                type: 1,
                area: ['550px', '260px'], //宽高
                content: $('#div_T004'), //这里content是一个DOM，注意：最好该元素要存放在body最外层，否则可能被其它的相对元素所影响
            });
        }

        function RefreshRow_vi(vi){
            var BudgetStandardCost = eval('BudgetStandardCost' + vi); var BudgetCount = eval('BudgetCount' + vi); var BudgetPersonCount = eval('BudgetPersonCount' + vi);
            var BudgetOtherCost= eval('BudgetOtherCost' + vi);
            var BudgetTotalCost = eval('BudgetTotalCost' + vi);

            //var BudgetStandardCost_value = Number($.trim(BudgetStandardCost.GetText()) == "" ? 0 : $.trim(BudgetStandardCost.GetText()));
            //var BudgetCount_value = Number($.trim(BudgetCount.GetText()) == "" ? 0 : $.trim(BudgetCount.GetText()));
            //var BudgetPersonCount_value = Number($.trim(BudgetPersonCount.GetText()) == "" ? 0 : $.trim(BudgetPersonCount.GetText()));
            var BudgetOtherCost_value = Number($.trim(BudgetOtherCost.GetText()) == "" ? 0 : $.trim(BudgetOtherCost.GetText()));

            //var BudgetTotalCost_value=BudgetStandardCost_value*BudgetCount_value*BudgetPersonCount_value;
            //BudgetTotalCost.SetText(BudgetTotalCost_value);

            if ($.trim(BudgetStandardCost.GetText()) != "" && $.trim(BudgetCount.GetText()) != "" && $.trim(BudgetPersonCount.GetText()) != "") {
                var BudgetStandardCost_value = Number($.trim(BudgetStandardCost.GetText()));
                var BudgetCount_value = Number($.trim(BudgetCount.GetText()));
                var BudgetPersonCount_value = Number($.trim(BudgetPersonCount.GetText()));

                var BudgetTotalCost_value=BudgetStandardCost_value*BudgetCount_value*BudgetPersonCount_value+BudgetOtherCost_value;
                BudgetTotalCost.SetText(BudgetTotalCost_value);
            }else {
                if($.trim(BudgetOtherCost.GetText()) == ""){
                    BudgetTotalCost.SetText("");
                }else {
                    BudgetTotalCost.SetText(BudgetOtherCost_value);
                }
                
            }   

            RefreshRow();
        }

        function RefreshRow() {
            var BTC=0;
            $("[id$=gv_d] tr[class*=DataRow]").each(function (index, item) { 
                var BudgetTotalCost = eval('BudgetTotalCost' + index);
                if (BudgetTotalCost.GetText()!="") {
                    BTC=BTC+Number(BudgetTotalCost.GetText());
                }
            });
            //grid底部total值更新
            $("[id$=gv_d] tr[id*=DXFooterRow]").find('td').each(function () {
                if($.trim($(this).text())!=""){
                    $(this).html("<b>合计:"+fmoney(BTC,2)+"</b>");//$(this).text("<b>合计:"+fmoney(BTC,2)+"</b>");
                    $("#CJJH input[id*='BudgetTotalCostByForm']").val(BTC.toFixed(2));
                }   
            });
        }

        function RefreshRow_HR() {
            var BC=0;var BC_feiji=0;var BC_huoche=0;var BC_qiche=0;
            $("[id$=gv_d_hr] tr[class*=DataRow]").each(function (index, item) { 
                var BudgetCost = eval('BudgetCost' + index);
                var Vehicle = eval('Vehicle' + index);

                if (BudgetCost.GetText()!="") {
                    BC=BC+Number(BudgetCost.GetText());

                    if (Vehicle.GetText()=="飞机") {
                        BC_feiji=BC_feiji+Number(BudgetCost.GetText());
                    }
                    if (Vehicle.GetText()=="火车") {
                        BC_huoche=BC_huoche+Number(BudgetCost.GetText());
                    }
                    if (Vehicle.GetText()=="长途汽车") {
                        BC_qiche=BC_qiche+Number(BudgetCost.GetText());
                    }
                }
                
            });
            //grid底部total值更新
            $("[id$=gv_d_hr] tr[id*=DXFooterRow]").find('td').each(function () {
                if(($.trim($(this).text())).indexOf("预算合计")>-1){
                    $(this).html("<b>预算合计:"+fmoney(BC,2)+"</b>");//$(this).text("<b>合计:"+fmoney(BC,2)+"</b>");
                }   
            });

            $("[id$=gv_d] tr[class*=DataRow]").each(function (index, item) { 
                if($(item).children("td:last-child").text()=="T001"){
                    var BudgetStandardCost = eval('BudgetStandardCost' + index); var BudgetCount = eval('BudgetCount' + index); var BudgetPersonCount = eval('BudgetPersonCount' + index);
                    var BudgetOtherCost = eval('BudgetOtherCost' + index);
                    var BudgetTotalCost = eval('BudgetTotalCost' + index);
                    
                    if(BC_feiji>0){
                        (eval('IsHrReserve' + index)).SetText("是");

                        $(item).find("table[id*=BudgetStandardCost]").addClass("dxeTextBox_read");
                        $(item).find("input[id*=BudgetStandardCost]").attr("readOnly","readOnly").addClass("dxeTextBox_read");

                        $(item).find("table[id*=BudgetCount]").addClass("dxeTextBox_read");
                        $(item).find("input[id*=BudgetCount]").attr("readOnly","readOnly").addClass("dxeTextBox_read");

                        $(item).find("table[id*=BudgetPersonCount]").addClass("dxeTextBox_read");
                        $(item).find("input[id*=BudgetPersonCount]").attr("readOnly","readOnly").addClass("dxeTextBox_read");

                        $(item).find("table[id*=BudgetOtherCost]").addClass("dxeTextBox_read");
                        $(item).find("input[id*=BudgetOtherCost]").attr("readOnly","readOnly").addClass("dxeTextBox_read");

                        BudgetStandardCost.SetText("");BudgetCount.SetText("");BudgetPersonCount.SetText("");BudgetOtherCost.SetText("");BudgetTotalCost.SetText("");
                        BudgetTotalCost.SetText(BC_feiji);  
                    }
                    else{
                        (eval('IsHrReserve' + index)).SetText("否");

                        $(item).find("table[id*=BudgetStandardCost]").removeClass("dxeTextBox_read");
                        $(item).find("input[id*=BudgetStandardCost]").removeAttr("readOnly").removeClass("dxeTextBox_read");

                        $(item).find("table[id*=BudgetCount]").removeClass("dxeTextBox_read");
                        $(item).find("input[id*=BudgetCount]").removeAttr("readOnly").removeClass("dxeTextBox_read");

                        $(item).find("table[id*=BudgetPersonCount]").removeClass("dxeTextBox_read");
                        $(item).find("input[id*=BudgetPersonCount]").removeAttr("readOnly").removeClass("dxeTextBox_read");

                        $(item).find("table[id*=BudgetOtherCost]").removeClass("dxeTextBox_read");
                        $(item).find("input[id*=BudgetOtherCost]").removeAttr("readOnly").removeClass("dxeTextBox_read");

                        if ($.trim(BudgetStandardCost.GetText()) == "" || $.trim(BudgetCount.GetText()) == "" || $.trim(BudgetPersonCount.GetText()) == "") {
                            if ($.trim(BudgetOtherCost.GetText()) == "") {    
                                BudgetTotalCost.SetText("");  
                            }else {    
                                BudgetTotalCost.SetText(BudgetOtherCost.GetText());  
                            }
                        }
                    }
                }
                if($(item).children("td:last-child").text()=="T002"){
                    var BudgetStandardCost = eval('BudgetStandardCost' + index); var BudgetCount = eval('BudgetCount' + index); var BudgetPersonCount = eval('BudgetPersonCount' + index);
                    var BudgetOtherCost = eval('BudgetOtherCost' + index);
                    var BudgetTotalCost = eval('BudgetTotalCost' + index);
                    
                    if(BC_huoche>0){
                        (eval('IsHrReserve' + index)).SetText("是");
                    
                        $(item).find("table[id*=BudgetStandardCost]").addClass("dxeTextBox_read");
                        $(item).find("input[id*=BudgetStandardCost]").attr("readOnly","readOnly").addClass("dxeTextBox_read");

                        $(item).find("table[id*=BudgetCount]").addClass("dxeTextBox_read");
                        $(item).find("input[id*=BudgetCount]").attr("readOnly","readOnly").addClass("dxeTextBox_read");

                        $(item).find("table[id*=BudgetPersonCount]").addClass("dxeTextBox_read");
                        $(item).find("input[id*=BudgetPersonCount]").attr("readOnly","readOnly").addClass("dxeTextBox_read");

                        $(item).find("table[id*=BudgetOtherCost]").addClass("dxeTextBox_read");
                        $(item).find("input[id*=BudgetOtherCost]").attr("readOnly","readOnly").addClass("dxeTextBox_read");

                        BudgetStandardCost.SetText("");BudgetCount.SetText("");BudgetPersonCount.SetText("");BudgetOtherCost.SetText("");BudgetTotalCost.SetText("");
                        BudgetTotalCost.SetText(BC_huoche);  
                    }
                    else{
                        (eval('IsHrReserve' + index)).SetText("否");

                        $(item).find("table[id*=BudgetStandardCost]").removeClass("dxeTextBox_read");
                        $(item).find("input[id*=BudgetStandardCost]").removeAttr("readOnly").removeClass("dxeTextBox_read");

                        $(item).find("table[id*=BudgetCount]").removeClass("dxeTextBox_read");
                        $(item).find("input[id*=BudgetCount]").removeAttr("readOnly").removeClass("dxeTextBox_read");

                        $(item).find("table[id*=BudgetPersonCount]").removeClass("dxeTextBox_read");
                        $(item).find("input[id*=BudgetPersonCount]").removeAttr("readOnly").removeClass("dxeTextBox_read");

                        $(item).find("table[id*=BudgetOtherCost]").removeClass("dxeTextBox_read");
                        $(item).find("input[id*=BudgetOtherCost]").removeAttr("readOnly").removeClass("dxeTextBox_read");

                        if ($.trim(BudgetStandardCost.GetText()) == "" || $.trim(BudgetCount.GetText()) == "" || $.trim(BudgetPersonCount.GetText()) == "") {
                            if ($.trim(BudgetOtherCost.GetText()) == "") {    
                                BudgetTotalCost.SetText("");  
                            }else {    
                                BudgetTotalCost.SetText(BudgetOtherCost.GetText());  
                            }
                        }
                    } 
                }
                if($(item).children("td:last-child").text()=="T000"){
                    var BudgetStandardCost = eval('BudgetStandardCost' + index); var BudgetCount = eval('BudgetCount' + index); var BudgetPersonCount = eval('BudgetPersonCount' + index);
                    var BudgetOtherCost = eval('BudgetOtherCost' + index);
                    var BudgetTotalCost = eval('BudgetTotalCost' + index);
                    
                    if(BC_qiche>0){
                        (eval('IsHrReserve' + index)).SetText("是");
                    
                        $(item).find("table[id*=BudgetStandardCost]").addClass("dxeTextBox_read");
                        $(item).find("input[id*=BudgetStandardCost]").attr("readOnly","readOnly").addClass("dxeTextBox_read");

                        $(item).find("table[id*=BudgetCount]").addClass("dxeTextBox_read");
                        $(item).find("input[id*=BudgetCount]").attr("readOnly","readOnly").addClass("dxeTextBox_read");

                        $(item).find("table[id*=BudgetPersonCount]").addClass("dxeTextBox_read");
                        $(item).find("input[id*=BudgetPersonCount]").attr("readOnly","readOnly").addClass("dxeTextBox_read");

                        $(item).find("table[id*=BudgetOtherCost]").addClass("dxeTextBox_read");
                        $(item).find("input[id*=BudgetOtherCost]").attr("readOnly","readOnly").addClass("dxeTextBox_read");

                        BudgetStandardCost.SetText("");BudgetCount.SetText("");BudgetPersonCount.SetText("");BudgetOtherCost.SetText("");BudgetTotalCost.SetText("");
                        BudgetTotalCost.SetText(BC_qiche);  
                    }
                    else{
                        (eval('IsHrReserve' + index)).SetText("否");

                        $(item).find("table[id*=BudgetStandardCost]").removeClass("dxeTextBox_read");
                        $(item).find("input[id*=BudgetStandardCost]").removeAttr("readOnly").removeClass("dxeTextBox_read");

                        $(item).find("table[id*=BudgetCount]").removeClass("dxeTextBox_read");
                        $(item).find("input[id*=BudgetCount]").removeAttr("readOnly").removeClass("dxeTextBox_read");

                        $(item).find("table[id*=BudgetPersonCount]").removeClass("dxeTextBox_read");
                        $(item).find("input[id*=BudgetPersonCount]").removeAttr("readOnly").removeClass("dxeTextBox_read");

                        $(item).find("table[id*=BudgetOtherCost]").removeClass("dxeTextBox_read");
                        $(item).find("input[id*=BudgetOtherCost]").removeAttr("readOnly").removeClass("dxeTextBox_read");

                        if ($.trim(BudgetStandardCost.GetText()) == "" || $.trim(BudgetCount.GetText()) == "" || $.trim(BudgetPersonCount.GetText()) == "") {
                            if ($.trim(BudgetOtherCost.GetText()) == "") {    
                                BudgetTotalCost.SetText("");  
                            }else {    
                                BudgetTotalCost.SetText(BudgetOtherCost.GetText());  
                            }
                        }
                    } 
                }
            });
            RefreshRow();
        }

        function RefreshRow_HR_AC() {
            var AC=0;
            $("[id$=gv_d_hr] tr[class*=DataRow]").each(function (index, item) { 
                var ActualCost = eval('ActualCost' + index);
                if (ActualCost.GetText()!="") {
                    AC=AC+Number(ActualCost.GetText());
                }
            });
            //grid底部total值更新
            $("[id$=gv_d_hr] tr[id*=DXFooterRow]").find('td').each(function () {
                if(($.trim($(this).text())).indexOf("实际合计")>-1){
                    $(this).html("<b><font color=blue>实际合计:"+fmoney(AC,2)+"</font></b>");//$(this).text("<b>合计:"+fmoney(AC,2)+"</b>");
                }   
            });
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

        function Get_IsHrReserve(vi){
            var BudgetTotalCost = eval('BudgetTotalCost' + vi);
            BudgetTotalCost.SetText("");

            var bf=false;
            $("[id$=gv_d] tr[class*=DataRow]").each(function (index, item) { 
                if($(item).children("td:last-child").text()=="T001" || $(item).children("td:last-child").text()=="T002" || $(item).children("td:last-child").text()=="T000"){
                    var IsHrReserve = eval('IsHrReserve' + index);
                    var BudgetStandardCost = eval('BudgetStandardCost' + index); var BudgetCount = eval('BudgetCount' + index); var BudgetPersonCount = eval('BudgetPersonCount' + index);
                    var BudgetOtherCost = eval('BudgetOtherCost' + index);
                    var BudgetTotalCost = eval('BudgetTotalCost' + index);

                    if (IsHrReserve.GetText()=="是") {    
                        $(item).find("table[id*=BudgetStandardCost]").addClass("dxeTextBox_read");
                        $(item).find("input[id*=BudgetStandardCost]").attr("readOnly","readOnly").addClass("dxeTextBox_read");

                        $(item).find("table[id*=BudgetCount]").addClass("dxeTextBox_read");
                        $(item).find("input[id*=BudgetCount]").attr("readOnly","readOnly").addClass("dxeTextBox_read");

                        $(item).find("table[id*=BudgetPersonCount]").addClass("dxeTextBox_read");
                        $(item).find("input[id*=BudgetPersonCount]").attr("readOnly","readOnly").addClass("dxeTextBox_read");

                        $(item).find("table[id*=BudgetOtherCost]").addClass("dxeTextBox_read");
                        $(item).find("input[id*=BudgetOtherCost]").attr("readOnly","readOnly").addClass("dxeTextBox_read");

                        //$(item).find("table[id*=BudgetTotalCost]").addClass("dxeTextBox_read");
                        //$(item).find("input[id*=BudgetTotalCost]").attr("readOnly","readOnly").addClass("dxeTextBox_read");
                        
                        bf=true;      
                    }else {
                        $(item).find("table[id*=BudgetStandardCost]").removeClass("dxeTextBox_read");
                        $(item).find("input[id*=BudgetStandardCost]").removeAttr("readOnly").removeClass("dxeTextBox_read");

                        $(item).find("table[id*=BudgetCount]").removeClass("dxeTextBox_read");
                        $(item).find("input[id*=BudgetCount]").removeAttr("readOnly").removeClass("dxeTextBox_read");

                        $(item).find("table[id*=BudgetPersonCount]").removeClass("dxeTextBox_read");
                        $(item).find("input[id*=BudgetPersonCount]").removeAttr("readOnly").removeClass("dxeTextBox_read");

                        $(item).find("table[id*=BudgetOtherCost]").removeClass("dxeTextBox_read");
                        $(item).find("input[id*=BudgetOtherCost]").removeAttr("readOnly").removeClass("dxeTextBox_read");

                        //$(item).find("table[id*=BudgetTotalCost]").removeClass("dxeTextBox_read");
                        //$(item).find("input[id*=BudgetTotalCost]").removeAttr("readOnly").removeClass("dxeTextBox_read");
                    }
                    if (vi==index) {//清除当前行
                        BudgetStandardCost.SetText("");BudgetCount.SetText("");BudgetPersonCount.SetText("");BudgetTotalCost.SetText("");BudgetTotalCost.SetText("");
                    }
                    
                }  
            });
            if (bf) {
                $("#CJJH input[id*='IsHrReserveByForm']").val("是");
                $("#div_dtl_hr").css("display","block");
                gv_d_hr.PerformCallback("add");
            }else {
                $("#CJJH input[id*='IsHrReserveByForm']").val("否");
                $("#div_dtl_hr").css("display","none");
                gv_d_hr.PerformCallback("clear");
            }

        }

        function Get_YN(vi){
            layer.open({
                title:'人事预定并结算选择',
                type: 1,
                closeBtn: 0,//默认1，展示关闭
                area: ['250px', '150px'],
                content: $('#div_YN'), //这里content是一个DOM，注意：最好该元素要存放在body最外层，否则可能被其它的相对元素所影响
                btn: ['确认', '取消'],
                yes: function(index, layero){//按钮【确认】的回调    
                    var YN_value=$("#div_YN input[id*='rdb_YN']:checked").val();
                    if (typeof(YN_value)=="undefined") {
                        layer.alert("请选择人事预定并结算！");
                        return false;
                    }
                    var IsHrReserve= eval('IsHrReserve' + vi);
                    if(IsHrReserve.GetText()!=YN_value){
                        IsHrReserve.SetText(YN_value);
                        Get_IsHrReserve(vi);
                    }

                    layer.close(index);

                },btn2: function(index, layero){ //按钮【取消】的回调                   
                    //alert(2);
                    //return false 开启该代码可禁止点击该按钮关闭
                }
                //,cancel: function(){ 
                //    //右上角关闭回调
                //    //alert(3);
                //    //return false 开启该代码可禁止点击该按钮关闭
                //}
            });
        }

        function Get_Traveler(vi){
            var pa=$("#SQXX input[id*='ApplyId']").val()+'('+$("#SQXX input[id*='ApplyName']").val()+'),'+$("#CJJH input[id*='PlanAttendant']").val();

            var url = "/select/select_PlanAttendant_dtl.aspx?vi="+vi+"&PA="+pa;

            layer.open({
                title:'随行人员明细选择',
                type: 2,
                area: ['450px', '450px'],
                fixed: false, //不固定
                maxmin: true,
                content: url
            });  
        }

        function setvalue_Traveler(vi,workcode,workname){
            var TravelerName= eval('TravelerName' + vi);var TravelerId= eval('TravelerId' + vi);
            TravelerName.SetText(workname);TravelerId.SetText(workcode);
        }
        
        function Get_Vehicle(vi){
            layer.open({
                title:'交通工具选择',
                type: 1,
                closeBtn: 0,//默认1，展示关闭
                area: ['250px', '150px'],
                content: $('#div_Vehicle'), //这里content是一个DOM，注意：最好该元素要存放在body最外层，否则可能被其它的相对元素所影响
                btn: ['确认', '取消'],
                yes: function(index, layero){//按钮【确认】的回调    
                    var Vehicle_value=$("#div_Vehicle input[id*='rdb_Vehicle']:checked").val();
                    if (typeof(Vehicle_value)=="undefined") {
                        layer.alert("请选择交通工具！");
                        return false;
                    }
                    var Vehicle= eval('Vehicle' + vi);
                    Vehicle.SetText(Vehicle_value);

                    RefreshRow_HR();

                    layer.close(index);

                },btn2: function(index, layero){ //按钮【取消】的回调                   
                    //alert(2);
                    //return false 开启该代码可禁止点击该按钮关闭
                }
                //,cancel: function(){ 
                //    //右上角关闭回调
                //    //alert(3);
                //    //return false 开启该代码可禁止点击该按钮关闭
                //}
            });
        }

        function validate(action){
            var flag=true;var msg="";
            
            <%=ValidScript%>

            //--------------------------------------------------------------------------------------非空验证
            if($("#SQXX input[id*='ApplyId']").val()=="" || $("#SQXX input[id*='ApplyName']").val()==""){
                msg+="【申请人】不可为空.<br />";
            }
            if($("#SQXX input[id*='ApplyDeptId']").val()=="" || $("#SQXX input[id*='ApplyDeptName']").val()==""){
                msg+="【申请人部门】不可为空.<br />";
            }

            if($("#CJJH input[id*='PlanStartTime']").val()==""){
                msg+="【预计出发日期】不可为空.<br />";
            }
            if($("#CJJH input[id*='PlanEndTime']").val()==""){
                msg+="【预计结束日期】不可为空.<br />";
            }
            if($("#CJJH input[id*='PlanDays']").val()==""){
                msg+="【预计出差天数】不可为空.<br />";
            }
            //if($("#CJJH input[id*='PlanAttendant']").val()==""){
            //    msg+="【随行人员】不可为空.<br />";
            //}
            if($("#CJJH input[id*='TravelPlace']").val()==""){
                msg+="【出差地点】不可为空.<br />";
            }
            if($("#CJJH input[id*='TravelReason']").val()==""){
                msg+="【出差事由】不可为空.<br />";
            }
            if (!ASPxClientEdit.ValidateGroup("ValueValidationGroup")) {
                msg+="【出差预算明细】格式必须正确.<br />";
            }else {
                $("[id$=gv_d] tr[class*=DataRow]").each(function (index, item) { 
                    var is_cal=false;//标记：是否自动计算每行金额统计
                                    
                    if($(item).children("td:last-child").text()!="T007" && $(item).children("td:last-child").text()!="T001" 
                        && $(item).children("td:last-child").text()!="T002" && $(item).children("td:last-child").text()!="T000"){                    
                        is_cal=true;
                    }
                    if ($(item).children("td:last-child").text()=="T001" || $(item).children("td:last-child").text()=="T002" || $(item).children("td:last-child").text()=="T000") {
                        var IsHrReserve = eval('IsHrReserve' + index);                  
                        if (IsHrReserve.GetText()=="否") {    
                            is_cal=true;
                        }
                    }
                    if (is_cal) {
                        var BudgetStandardCost = eval('BudgetStandardCost' + index); var BudgetCount = eval('BudgetCount' + index); var BudgetPersonCount = eval('BudgetPersonCount' + index);
                        var is_a=0;
                        if ($.trim(BudgetStandardCost.GetText()) != "") { is_a++; }
                        if ($.trim(BudgetCount.GetText()) != "") { is_a++; }
                        if ($.trim(BudgetPersonCount.GetText()) != "") { is_a++; }
                        
                        if ($(item).children("td:last-child").text()=="T003" || $(item).children("td:last-child").text()=="T008") {//出租车/公交/地铁等  自驾费用
                                if (is_a!=3 && is_a!=1) {
                                msg+="【出差预算明细】第"+(index+1)+"行，预算标准、预算数量1、预算数量2 不可为空.<br />";
                            }
                        }else{
                            if (is_a!=3 && is_a!=0) {
                                msg+="【出差预算明细】第"+(index+1)+"行，预算标准、预算数量1、预算数量2 不可为空.<br />";
                            }
                        }                        
                    }

                    if ($(item).children("td:last-child").text()=="T007"){//除了 补贴以外，都有其他金额
                        var BudgetOtherCost = eval('BudgetOtherCost' + index); 
                        var BudgetRemark = eval('BudgetRemark' + index); 
                        if ($.trim(BudgetOtherCost.GetText()) != "") {
                            if(Number($.trim(BudgetOtherCost.GetText())) != 0 && $.trim(BudgetRemark.GetText()) == ""){
                                msg+="【出差预算明细】第"+(index+1)+"行，预算其他金额 不为0时，预算说明 不可为空.<br />";
                            }                            
                        }
                    }
                    
                });
            }

            if ($("#CJJH input[id*='IsHrReserveByForm']").val()=="是" || $('#div_dtl_hr').css('display')=='inline-block') {
                if($("[id$=gv_d_hr] input[id*=TravelerName]").length==0){
                    msg+="【人事预定明细】不可为空.<br />";
                }else {
                    if (!ASPxClientEdit.ValidateGroup("ValueValidationGroup_HR")) {
                        msg+="【人事预定明细】格式必须正确.<br />";
                    }else {
                        if(action=='submit'){
                            $("[id$=gv_d_hr] tr[class*=DataRow]").each(function (index, item) { 
                                var TravelerName = eval('TravelerName' + index);var TravelerId = eval('TravelerId' + index);
                                var StartFromPlace = eval('StartFromPlace' + index);var EndToPlace = eval('EndToPlace' + index);
                                var StartDateTime = eval('StartDateTime' + index);
                                var BudgetCost = eval('BudgetCost' + index);
                                var Vehicle = eval('Vehicle' + index);
                                
                                if (TravelerName.GetText()=="" || TravelerId.GetText()=="") { msg+="【出行人】不可为空.<br />"; }
                                if (StartFromPlace.GetText()=="") { msg+="【出发地】不可为空.<br />"; }
                                if (EndToPlace.GetText()=="") { msg+="【目的地】不可为空.<br />"; }
                                if (StartDateTime.GetText()=="0100/01/01 00:00") { msg+="【出发时间】不可为空.<br />"; }
                                if (BudgetCost.GetText()=="") { msg+="【预算费用】不可为空.<br />"; }
                                if (Vehicle.GetText()=="") { msg+="【交通工具】不可为空.<br />"; }

                                if (msg!="") {
                                    return false;
                                }

                            });

                            if(stepid!=null){
                                if(stepid.toLowerCase()=="d2fa4155-af68-4dfa-8dee-cbc25d3d2bef"){
                                    $("[id$=gv_d_hr] input[id*=ScheduledFlight]").each(function (){
                                        if( $(this).val()==""){
                                            msg+="【预定班次】不可为空.<br />";
                                            return false;
                                        }
                                    });
                                    $("[id$=gv_d_hr] input[id*=ActualCost]").each(function (){
                                        if( $(this).val()==""){
                                            msg+="【实际费用】不可为空.<br />";
                                            return false;
                                        }
                                    });
                                }
                            }
                        
                        }
                    } 
                }
            }
            
            if(msg!=""){  
                flag=false;
                layer.alert(msg);
                return flag;
            }
            
            //----------------------------------------------------------------------------逻辑验证
            //验证 预计出发时间 预计结束时间 大小关系   
            //if((new Date())>(new Date(Date.parse($("#CJJH input[id*='PlanStartTime']").val())))){
            //    msg+="【预计出发日期】必须大于等于【当前日期】.<br />";
            //}
            if(compareDate($("#CJJH input[id*='PlanStartTime']").val(),$("#CJJH input[id*='PlanEndTime']").val())){
                msg+="【预计结束日期】必须大于【预计出发日期】.<br />";
            }
            if ($("#CJJH input[id*='IsHrReserveByForm']").val()=="是" || $('#div_dtl_hr').css('display')=='inline-block') {
                
                var Vehicle_feiji=false;var Vehicle_huoche=false;var Vehicle_qiche=false;
                $("[id$=gv_d_hr] tr[class*=DataRow]").each(function (index, item) { 
                    var Vehicle = eval('Vehicle' + index);
                    if (Vehicle.GetText()=="飞机") {
                        Vehicle_feiji=true;
                    }
                    if (Vehicle.GetText()=="火车") {
                        Vehicle_huoche=true;
                    } 
                    if (Vehicle.GetText()=="长途汽车") {
                        Vehicle_qiche=true;
                    } 
                });
                            
                $("[id$=gv_d] tr[class*=DataRow]").each(function (index, item) { 
                    var IsHrReserve = eval('IsHrReserve' + index);                  
                    if (IsHrReserve.GetText()=="否") {    
                        if ($(item).children("td:last-child").text()=="T001") {
                            if (Vehicle_feiji) {
                                msg+="【机票费】人事预定为“否”，请删除【人事预定明细】中【交通工具】为“飞机”的信息.<br />";    
                            }
                        }
                        if ($(item).children("td:last-child").text()=="T002") {
                            if (Vehicle_huoche) {
                                msg+="【火车票】人事预定为“否”，请删除【人事预定明细】中【交通工具】为“火车”的信息.<br />";    
                            }
                        }
                        if ($(item).children("td:last-child").text()=="T000") {
                            if (Vehicle_huoche) {
                                msg+="【长途汽车票】人事预定为“否”，请删除【人事预定明细】中【交通工具】为“长途汽车”的信息.<br />";    
                            }
                        }
                    }
                });


                if(action=='submit'){
                    $("[id$=gv_d_hr] tr[class*=DataRow]").each(function (index, item) {
                        var StartDateTime = eval('StartDateTime' + index);
                        var StartDate=(StartDateTime.GetText()).substring(0,10);
                        if((new Date(Date.parse(StartDate)))<(new Date(Date.parse($("#CJJH input[id*='PlanStartTime']").val())))){
                            msg+="【人事预定明细】-【出发日期】必须大于等于【预计出发日期】.<br />";
                            return false;
                        }
                    });
                }
            }

            if(msg!=""){  
                flag=false;
                layer.alert(msg);
                return flag;
            }
            
            //---------------------------------------------------------------------------签核意见验证
            if(!parent.checkSign()){
                flag=false;return flag;
            }
            if(flag){

                var ApplyId=$("#SQXX input[id*='ApplyId']").val();

                $.ajax({
                    type: "post",
                    url: "FIN_T.aspx/CheckData",
                    data: "{'appuserid':'" + ApplyId + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    async: false,//默认是true，异步；false为同步，此方法执行完在执行下面代码
                    success: function (data) {
                        var obj=eval(data.d);

                        if(obj[0].manager_flag!=""){ msg+=obj[0].manager_flag; }

                        if(msg!=""){  
                            flag=false;
                            layer.alert(msg);
                            return flag;
                        }
                    }

                });
            }
            return flag;
        }

        function compareDate(s1,s2){
            return ((new Date(s1.replace(/-/g,"\/")))>=(new Date(s2.replace(/-/g,"\/"))));
        }

    </script>

</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div class="col-md-12  ">
        <div class="col-md-10">
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

    <div id="div_YN" style="display:none;">
        <asp:RadioButtonList ID="rdb_YN" runat="server" RepeatDirection="Horizontal" Height="20px" Width="120px" style="margin-left:10px; margin-top:10px;">
            <asp:ListItem Text="是" Value="是"></asp:ListItem>
            <asp:ListItem Text="否" Value="否"></asp:ListItem>
        </asp:RadioButtonList>
    </div>

    <div id="div_Vehicle" style="display:none;">
        <asp:RadioButtonList ID="rdb_Vehicle" runat="server" RepeatDirection="Horizontal" Height="20px" Width="180px" style="margin-left:10px; margin-top:10px;">
            <asp:ListItem Text="飞机" Value="飞机"></asp:ListItem>
            <asp:ListItem Text="火车" Value="火车"></asp:ListItem>
            <asp:ListItem Text="长途汽车" Value="长途汽车"></asp:ListItem>
        </asp:RadioButtonList>
    </div>   
    
    <div id="div_T004" style="display:none;"> 
        <table style=" margin:15px 15px; border:1px solid #c0c0c0; line-height:25px; font-size:12px;">
            <tr style="background-color:#D9EDF7;">
                <td>职别</td>
                <td>北京</td>
                <td>其他自治区、沿海城市、<br />经济特区、省会城市、<br />直辖市</td>
                <td>其他地区</td>
            </tr>
            <tr style="border:1px solid #c0c0c0;">
                <td>总经理\副总经理 <font color="red">L0</font></td>
                <td>700</td>
                <td>600</td>
                <td>500</td>
            </tr>
            <tr style="border:1px solid #c0c0c0;">
                <td>部门经理 <font color="red">L1</font></td>
                <td>600</td>
                <td>500</td>
                <td>400</td>
            </tr>
            <tr style="border:1px solid #c0c0c0;">
                <td>其他员工 <font color="red">L2-L5</font></td>
                <td>400</td>
                <td>400</td>
                <td>300</td>
            </tr>
        </table>
    </div>    

    <div class="col-md-12" >  

        <div class="row row-container">
            <div class="panel panel-info">
                <div class="panel-heading" data-toggle="collapse" data-target="#DQXX">
                    <strong>填单人信息</strong>
                </div>
                <div class="panel-body collapse" id="DQXX">
                    <div class="col-xs-12 col-sm-12  col-md-12 col-lg-12" style="width:1000px;">
                        <table style="width: 100%">
                            <tr>
                                <td><font color="red">&nbsp;</font>填单人</td>
                                <td>
                                    <div class="form-inline">
                                        <asp:TextBox runat="server" ID="CreateId" class="lineread" ReadOnly="True" Width="50px"></asp:TextBox>
                                        <asp:TextBox runat="server" ID="CreateName"  class="lineread" ReadOnly="True" Width="82px"></asp:TextBox>
                                        <asp:TextBox runat="server" ID="CreateTelephone" class="lineread" Width="120px"/>
                                    </div>
                                </td>
                                <td><font color="red">&nbsp;</font>填单人职位</td>
                                <td><asp:TextBox runat="server" ID="CreateJobTitleName" class="lineread" ReadOnly="True" Width="260px"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td><font color="red">&nbsp;</font>填单人公司</td>
                                <td>
                                    <div class="form-inline">
                                        <asp:TextBox runat="server" ID="CreateDomain" class="lineread" ReadOnly="True" Width="60px"></asp:TextBox>
                                        <asp:TextBox runat="server" ID="CreateDomainName"  class="lineread" ReadOnly="True" Width="196px"></asp:TextBox>
                                    </div>
                                </td>
                                <td><font color="red">&nbsp;</font>填单人部门</td>
                                <td>
                                    <div class="form-inline">
                                        <asp:TextBox runat="server" ID="CreateDeptId" class="lineread" ReadOnly="True" Width="60px"></asp:TextBox>
                                        <asp:TextBox runat="server" ID="CreateDeptName"  class="lineread" ReadOnly="True" Width="196px"></asp:TextBox>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
        </div>

        <div class="row row-container">
            <div class="panel panel-info">
                <div class="panel-heading" data-toggle="collapse" data-target="#SQXX">
                    <strong>表单基本信息</strong>
                </div>
                <div class="panel-body" id="SQXX">
                    <div class="col-xs-12 col-sm-12  col-md-12 col-lg-12" style="width:1000px;">
                        <table style="width: 100%">
                            <tr>
                                <td><font color="red">&nbsp;</font>表单编号</td>
                                <td><asp:TextBox ID="FormNo" runat="server" class="lineread"  ReadOnly="true" placeholder="自动产生" Width="260px" ToolTip="1|0" /></td>
                                <td><font color="red">&nbsp;</font>申请日期</td>
                                <td><asp:TextBox ID="ApplyDate"  runat="server" class="lineread" ReadOnly="True" Width="260px" /></td>
                            </tr>
                            <tr>
                                <td><font color="red">*</font>申请人</td>
                                <td>
                                    <div class="form-inline">
                                        <asp:TextBox runat="server" ID="ApplyId" class="lineread" ReadOnly="True" Width="50px"></asp:TextBox>
                                        <asp:TextBox runat="server" ID="ApplyName"  class="lineread" ReadOnly="True" Width="82px"></asp:TextBox>
                                        <asp:TextBox runat="server" ID="ApplyTelephone" class="lineread" Width="120px"/>
                                        <i id="ApplyId_i" class="fa fa-search <% =ViewState["ApplyId_i"].ToString() == "Y" ? "i_hidden" : "i_show" %>" onclick="Get_ApplyId()"></i>
                                    </div>
                                </td>
                                <td><font color="red">&nbsp;</font>申请人职位</td>
                                <td><asp:TextBox runat="server" ID="ApplyJobTitleName" class="lineread" ReadOnly="True" Width="260px"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td><font color="red">&nbsp;</font>申请人公司</td>
                                <td>
                                    <div class="form-inline">
                                        <asp:TextBox runat="server" ID="ApplyDomain" class="lineread" ReadOnly="True" Width="60px"></asp:TextBox>
                                        <asp:TextBox runat="server" ID="ApplyDomainName"  class="lineread" ReadOnly="True" Width="196px"></asp:TextBox>
                                    </div>
                                </td>
                                <td><font color="red">&nbsp;</font>申请人部门</td>
                                <td>
                                    <div class="form-inline">
                                        <asp:TextBox runat="server" ID="ApplyDeptId" class="lineread" ReadOnly="True" Width="60px"></asp:TextBox>
                                        <asp:TextBox runat="server" ID="ApplyDeptName"  class="lineread" ReadOnly="True" Width="196px"></asp:TextBox>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
        </div>
        
        <div class="row  row-container">
            <div class="panel panel-info">
                <div class="panel-heading" data-toggle="collapse" data-target="#CJJH">
                        <strong>预计出差计划</strong>
                </div>
                <div class="panel-body" id="CJJH">
                    <div class="col-xs-12 col-sm-12  col-md-12 col-lg-12" style="width:1000px;">
                        <table style="width: 100%">
                            <tr>
                                <td><font color="red">*</font>预计出发日期</td>
                                <td><asp:TextBox ID="PlanStartTime" runat="server" class="linewrite" ReadOnly="True" Width="260px" 
                                    onclick="laydate({type: 'date',format: 'YYYY/MM/DD',start:laydate.now(1),min:laydate.now(1),max:$('#CJJH input[id*=\'PlanEndTime\']').val(),choose: function(dates){Auto_Calculate();}});" /></td>
                                <td><font color="red">*</font>预计结束日期</td>
                                <td><asp:TextBox ID="PlanEndTime"  runat="server" class="linewrite" ReadOnly="True" Width="260px" 
                                    onclick="laydate({type: 'date',format: 'YYYY/MM/DD',start:$('#CJJH input[id*=\'PlanStartTime\']').val(),min:$('#CJJH input[id*=\'PlanStartTime\']').val(),choose: function(dates){Auto_Calculate();}});" /></td>
                            </tr>
                            <tr>
                                <td><font color="red">&nbsp;</font>预计出差天数</td>
                                <td>
                                    <asp:TextBox ID="PlanDays" runat="server" class="lineread" ReadOnly="True" Width="260px" Text="0" />
                                </td>
                                <td><font color="red">&nbsp;</font>随行人员</td>
                                <td><asp:TextBox runat="server" ID="PlanAttendant" class="lineread" Width="240px" ReadOnly="True"></asp:TextBox>
                                    <i id="PlanAttendant_i" class="fa fa-search <% =ViewState["PlanAttendant_i"].ToString() == "Y" ? "i_hidden" : "i_show" %>" onclick="Get_PlanAttendant()"></i>
                                </td>
                            </tr>
                            <tr>
                                <td><font color="red">*</font>出差类型</td>
                                <td>
                                    <asp:DropDownList ID="TravelType" runat="server" class="linewrite"  style="width:260px" Height="23px">
                                        <asp:ListItem Text="市内" Value="市内"></asp:ListItem>
                                        <asp:ListItem Text="国内" Value="国内"></asp:ListItem>
                                        <asp:ListItem Text="国外" Value="国外"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td><font color="red">*</font>出差地点</td>
                                <td>
                                    <asp:TextBox ID="TravelPlace" runat="server" class="linewrite" Width="260px" />
                                </td>
                            </tr>
                            <tr>
                                <td><font color="red">*</font>出差事由</td>
                                <td colspan="3">
                                    <asp:TextBox ID="TravelReason" runat="server" TextMode="MultiLine" class="form-control" Width="770px" Font-Size="12px" BackColor="#FDF7D9"></asp:TextBox>
                                </td>
                            </tr>
                            <tr style="display:none;">
                                <td></td>
                                <td><asp:TextBox ID="BudgetTotalCostByForm" runat="server" class="lineread" ReadOnly="True" Width="260px" /></td>
                                <td></td>
                                <td><asp:TextBox ID="IsHrReserveByForm" runat="server" class="lineread" ReadOnly="True" Width="260px" /></td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
        </div>


        <div id="div_dtl" class="row  row-container">
            <div class="panel panel-info">
                <div class="panel-heading" data-toggle="collapse" data-target="#fin_t_dtl">
                    <strong>出差预算明细</strong>
                    &nbsp;&nbsp;<font style="color:red; font-size:9px;">in CNY</font>
                </div>
                <div class="panel-body " id="fin_t_dtl">
                    <div class="col-xs-12 col-sm-12  col-md-12 col-lg-12">

                        <dx:aspxgridview ID="gv_d" runat="server" AutoGenerateColumns="False" KeyFieldName="numid" Theme="MetropolisBlue" EnableTheming="True" 
                                ClientInstanceName="gv_d"> 
                            <SettingsPager PageSize="1000"></SettingsPager>
                            <Settings ShowFooter="True" />
                            <SettingsBehavior AllowSelectByRowClick="false" AllowDragDrop="False" AllowSort="False" />
                            <Columns>
                                <dx:GridViewDataTextColumn Caption="#" FieldName="numid" Width="20px" VisibleIndex="0"></dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="费用类型" FieldName="CostCodeDesc" Width="120px" VisibleIndex="1"></dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="人事预定<br />并结算" FieldName="IsHrReserve" Width="50px" VisibleIndex="2">
                                    <Settings AllowCellMerge="False" />
                                    <DataItemTemplate>       
                                        <table>
                                            <tr>
                                                <td>
                                                    <dx:ASPxTextBox ID="IsHrReserve" Width="40px" runat="server" Value='<%# Eval("IsHrReserve")%>' 
                                                        ClientInstanceName='<%# "IsHrReserve"+Container.VisibleIndex.ToString() %>' Border-BorderWidth="0"  ReadOnly="true">  
                                                    </dx:ASPxTextBox>
                                                </td>
                                                <td><i id="IsHrReserve_i_<%#Container.VisibleIndex.ToString() %>" 
                                                    class="fa fa-search <% =ViewState["IsHrReserve_i"].ToString() == "Y" ? "i_hidden" : "i_show" %>" 
                                                    onclick="Get_YN(<%# Container.VisibleIndex %>,'')"></i>
                                                </td>
                                            </tr>
                                        </table> 
                                    </DataItemTemplate>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="预算标准限额政策" FieldName="Standardlimit" Width="190px" VisibleIndex="3" CellStyle-ForeColor="Red">
                                    <Settings AllowCellMerge="False"/>
                                    <DataItemTemplate>
                                        <%--<dx:ASPxLabel ID="Standardlimit" Width="190px" runat="server" Value='<%# Eval("Standardlimit")%>' EncodeHtml="false"
                                            ClientInstanceName='<%# "Standardlimit"+Container.VisibleIndex.ToString() %>'
                                            ClientSideEvents-Click='<%# "function(s,e){Show_limit("+Container.VisibleIndex.ToString()+");}" %>'>
                                        </dx:ASPxLabel>--%>
                                        <dx:ASPxLabel ID="Standardlimit" Width="190px" runat="server" Value='<%# Eval("Standardlimit")%>' EncodeHtml="false">
                                        </dx:ASPxLabel>
                                    </DataItemTemplate>    
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="预算标准" FieldName="BudgetStandardCost" Width="125px" VisibleIndex="4">
                                    <Settings AllowCellMerge="False"/>
                                    <DataItemTemplate>
                                         <table>
                                            <tr>
                                                <td>
                                                    <dx:ASPxTextBox ID="BudgetStandardCost" Width="60px" runat="server" Value='<%# Eval("BudgetStandardCost")%>'
                                                        ClientSideEvents-ValueChanged='<%# "function(s,e){RefreshRow_vi("+Container.VisibleIndex.ToString()+");}" %>'  
                                                        ClientInstanceName='<%# "BudgetStandardCost"+Container.VisibleIndex.ToString() %>' HorizontalAlign="Right">
                                                        <ValidationSettings ValidationGroup="ValueValidationGroup" Display="Dynamic" ErrorTextPosition="Bottom">
                                                            <RegularExpression ErrorText="请输入正数或0！" ValidationExpression="^([1-9]\d*|0)(\.\d*[1-9])?$" />
                                                        </ValidationSettings>
                                                    </dx:ASPxTextBox>
                                                </td>
                                                <td>
                                                    <%--<dx:ASPxTextBox ID="cost_unit" Width="80px" runat="server" Value='<%# Eval("cost_unit")%>'  Border-BorderWidth="0"  ReadOnly="true">
                                                    </dx:ASPxTextBox>--%>
                                                    <dx:ASPxLabel ID="cost_unit" Width="65px" runat="server" Value='<%# Eval("cost_unit")%>'>
                                                    </dx:ASPxLabel>
                                                </td>
                                            </tr>
                                        </table>
                                    </DataItemTemplate>        
                                </dx:GridViewDataTextColumn> 
                                <dx:GridViewDataTextColumn Caption="预算数量1(次/顿/<br />间/天/公里等)" FieldName="BudgetCount" Width="90px" VisibleIndex="5">
                                    <Settings AllowCellMerge="False"/>
                                    <DataItemTemplate>
                                        <table>
                                            <tr>
                                                <td>
                                                    <dx:ASPxTextBox ID="BudgetCount" Width="60px" runat="server" Value='<%# Eval("BudgetCount")%>'
                                                        ClientSideEvents-ValueChanged='<%# "function(s,e){RefreshRow_vi("+Container.VisibleIndex.ToString()+");}" %>' 
                                                        ClientInstanceName='<%# "BudgetCount"+Container.VisibleIndex.ToString() %>' HorizontalAlign="Right">
                                                        <ValidationSettings ValidationGroup="ValueValidationGroup" Display="Dynamic" ErrorTextPosition="Bottom">
                                                            <RegularExpression ErrorText="请输入正数或0！" ValidationExpression="^([1-9]\d*|0)(\.\d*[1-9])?$" />
                                                        </ValidationSettings>
                                                    </dx:ASPxTextBox>
                                                </td>
                                                <td>
                                                   <%-- <dx:ASPxTextBox ID="BudgetCount_1_unit" Width="30px" runat="server" Value='<%# Eval("BudgetCount_1_unit")%>'  Border-BorderWidth="0"  ReadOnly="true">
                                                    </dx:ASPxTextBox>--%>
                                                    <dx:ASPxLabel ID="BudgetCount_1_unit" Width="30px" runat="server" Value='<%# Eval("BudgetCount_1_unit")%>'>
                                                    </dx:ASPxLabel>
                                                </td>
                                            </tr>
                                        </table>
                                    </DataItemTemplate>        
                                </dx:GridViewDataTextColumn> 
                                <dx:GridViewDataTextColumn Caption="预算数量2(人数/<br />房间数/趟数等)" FieldName="BudgetPersonCount" Width="90px" VisibleIndex="6">
                                    <Settings AllowCellMerge="False"/>
                                    <DataItemTemplate>
                                        <table>
                                            <tr>
                                                <td>
                                                    <dx:ASPxTextBox ID="BudgetPersonCount" Width="60px" runat="server" Value='<%# Eval("BudgetPersonCount")%>'
                                                        ClientSideEvents-ValueChanged='<%# "function(s,e){RefreshRow_vi("+Container.VisibleIndex.ToString()+");}" %>' 
                                                        ClientInstanceName='<%# "BudgetPersonCount"+Container.VisibleIndex.ToString() %>' HorizontalAlign="Right">
                                                        <ValidationSettings ValidationGroup="ValueValidationGroup" Display="Dynamic" ErrorTextPosition="Bottom">
                                                            <RegularExpression ErrorText="请输入正整数或0！" ValidationExpression="^[1-9]\d*$|[0]" />
                                                        </ValidationSettings>
                                                    </dx:ASPxTextBox>
                                                </td>
                                                <td>
                                                    <%--<dx:ASPxTextBox ID="BudgetCount_2_unit" Width="30px" runat="server" Value='<%# Eval("BudgetCount_2_unit")%>'  Border-BorderWidth="0"  ReadOnly="true">
                                                    </dx:ASPxTextBox>--%>
                                                    <dx:ASPxLabel ID="BudgetCount_2_unit" Width="30px" runat="server" Value='<%# Eval("BudgetCount_2_unit")%>'>
                                                    </dx:ASPxLabel>
                                                </td>
                                            </tr>
                                        </table>
                                    </DataItemTemplate>        
                                </dx:GridViewDataTextColumn> 
                                <dx:GridViewDataTextColumn Caption="预算<br />其他金额" FieldName="BudgetOtherCost" Width="60px" VisibleIndex="7">
                                    <Settings AllowCellMerge="False"/>
                                    <DataItemTemplate>
                                        <dx:ASPxTextBox ID="BudgetOtherCost" Width="60px" runat="server" Value='<%# Eval("BudgetOtherCost")%>'
                                            ClientSideEvents-ValueChanged='<%# "function(s,e){RefreshRow_vi("+Container.VisibleIndex.ToString()+");}" %>'  
                                            ClientInstanceName='<%# "BudgetOtherCost"+Container.VisibleIndex.ToString() %>' HorizontalAlign="Right">
                                            <ValidationSettings ValidationGroup="ValueValidationGroup" Display="Dynamic" ErrorTextPosition="Bottom">
                                                <RegularExpression ErrorText="请输入正数或0！" ValidationExpression="^([1-9]\d*|0)(\.\d*[1-9])?$" />
                                            </ValidationSettings>
                                        </dx:ASPxTextBox>
                                    </DataItemTemplate>        
                                </dx:GridViewDataTextColumn> 
                                <dx:GridViewDataTextColumn Caption="预算金额合计<br />(前三列相乘+其他)" FieldName="BudgetTotalCost" Width="60px" VisibleIndex="8">
                                    <Settings AllowCellMerge="False"/>
                                    <DataItemTemplate>
                                        <dx:ASPxTextBox ID="BudgetTotalCost" Width="60px" runat="server" Value='<%# Eval("BudgetTotalCost")%>'
                                            ClientInstanceName='<%# "BudgetTotalCost"+Container.VisibleIndex.ToString() %>' HorizontalAlign="Right" Border-BorderWidth="0" ReadOnly="true">
                                        </dx:ASPxTextBox>
                                    </DataItemTemplate>        
                                </dx:GridViewDataTextColumn>    
                                <dx:GridViewDataTextColumn Caption="预算说明" FieldName="BudgetRemark" Width="200px" VisibleIndex="9">
                                    <Settings AllowCellMerge="False" />
                                    <DataItemTemplate>                
                                        <dx:ASPxTextBox ID="BudgetRemark" Width="200px" runat="server" Value='<%# Eval("BudgetRemark")%>' 
                                             ClientInstanceName='<%# "BudgetRemark"+Container.VisibleIndex.ToString() %>'>
                                        </dx:ASPxTextBox>                
                                    </DataItemTemplate>        
                                </dx:GridViewDataTextColumn>

                                <dx:GridViewDataTextColumn FieldName="id" Width="0px">
                                        <HeaderStyle CssClass="hidden" />
                                        <CellStyle CssClass="hidden"></CellStyle>
                                        <FooterCellStyle CssClass="hidden"></FooterCellStyle>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="FIN_T_No" Width="0px">
                                        <HeaderStyle CssClass="hidden" />
                                        <CellStyle CssClass="hidden"></CellStyle>
                                        <FooterCellStyle CssClass="hidden"></FooterCellStyle>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="CostCode" Width="0px">
                                        <HeaderStyle CssClass="hidden" />
                                        <CellStyle CssClass="hidden"></CellStyle>
                                        <FooterCellStyle CssClass="hidden"></FooterCellStyle>
                                </dx:GridViewDataTextColumn>
                            </Columns>                      
                            <TotalSummary>
                                <dx:ASPxSummaryItem DisplayFormat="<b>预算合计:{0:N2}</b>" FieldName="BudgetTotalCost" SummaryType="Sum" ShowInColumn="BudgetTotalCost" ShowInGroupFooterColumn="BudgetTotalCost" />
                            </TotalSummary>                          
                            <Styles>
                                <Header BackColor="#E4EFFA"  ></Header>        
                                <SelectedRow BackColor="#FDF7D9"></SelectedRow>      
                            </Styles>                                          
                        </dx:aspxgridview>

                            
                    </div>
                </div>
            </div>
        </div>

        <div id="div_dtl_hr" class="row  row-container" style="display:none;">
            <div class="panel panel-info">
                <div class="panel-heading" data-toggle="collapse" data-target="#dtl_hr">
                    <strong>人事预定信息</strong>
                </div>
                <div class="panel-body " id="dtl_hr">
                    <div class="col-xs-12 col-sm-12  col-md-12 col-lg-12">
                        <div style="padding: 2px 5px 5px 0px">                
                            
                        </div>
                        <asp:UpdatePanel runat="server" ID="p1" UpdateMode="Conditional" >
                            <ContentTemplate>
                                
                                <asp:Button ID="btnadd" runat="server" Text="新增" class="btn btn-default" style="width:60px; height:32px;"  OnClick="btnadd_Click" />
                                <asp:Button ID="btndel" runat="server" Text="删除" class="btn btn-default" style="width:60px; height:32px;"  OnClick="btndel_Click" OnClientClick="return con_sure()" />

                                 <dx:aspxgridview ID="gv_d_hr" runat="server" AutoGenerateColumns="False" KeyFieldName="numid" Theme="MetropolisBlue" 
                                     ClientInstanceName="gv_d_hr"  EnableTheming="True" OnCustomCallback="gv_d_hr_CustomCallback" OnDataBound="gv_d_hr_DataBound"><%--OnHtmlRowCreated="gv_d_hr_HtmlRowCreated"--%>
                                     <ClientSideEvents SelectionChanged="gv_d_hr_SelectionChanged" />
                                    <SettingsPager PageSize="1000"></SettingsPager>
                                    <Settings ShowFooter="True" />
                                    <SettingsBehavior AllowSelectByRowClick="false" AllowDragDrop="False" AllowSort="False" />
                                    <Columns>
                                        <dx:GridViewCommandColumn SelectAllCheckboxMode="Page" ShowClearFilterButton="true" ShowSelectCheckbox="true" Name="Sel" Width="30" VisibleIndex="0"></dx:GridViewCommandColumn>
                                        <dx:GridViewDataTextColumn  Caption="#" FieldName="numid" Width="30px" VisibleIndex="0"></dx:GridViewDataTextColumn>    
                                        <dx:GridViewDataTextColumn Caption="出行人姓名" FieldName="TravelerName" Width="80px" VisibleIndex="1">
                                            <Settings AllowCellMerge="False" />
                                            <DataItemTemplate>
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <dx:ASPxTextBox ID="TravelerName" Width="80px" runat="server" Value='<%# Eval("TravelerName")%>' 
                                                                ClientInstanceName='<%# "TravelerName"+Container.VisibleIndex.ToString() %>' Border-BorderWidth="0"   ReadOnly="true">
                                                            </dx:ASPxTextBox>
                                                        </td>
                                                        <td><i id="Traveler_i_<%#Container.VisibleIndex.ToString() %>" 
                                                            class="fa fa-search <% =ViewState["Traveler_i"].ToString() == "Y" ? "i_hidden" : "i_show" %>" 
                                                            onclick="Get_Traveler(<%# Container.VisibleIndex %>,'')"></i>
                                                        </td>
                                                    </tr>
                                                </table>       
                                            </DataItemTemplate>
                                        </dx:GridViewDataTextColumn> 
                                        <dx:GridViewDataTextColumn Caption="出行人工号" FieldName="TravelerId" Width="80px" VisibleIndex="2">
                                            <Settings AllowCellMerge="False"/>
                                            <DataItemTemplate>
                                                <dx:ASPxTextBox ID="TravelerId" Width="80px" runat="server" Value='<%# Eval("TravelerId")%>' 
                                                    ClientInstanceName='<%# "TravelerId"+Container.VisibleIndex.ToString() %>' Border-BorderWidth="0"   ReadOnly="true">
                                                </dx:ASPxTextBox>
                                            </DataItemTemplate>        
                                        </dx:GridViewDataTextColumn>                                       
                                        <dx:GridViewDataTextColumn Caption="出发地" FieldName="StartFromPlace" Width="100px" VisibleIndex="2">
                                            <Settings AllowCellMerge="False"/>
                                            <DataItemTemplate>
                                                <dx:ASPxTextBox ID="StartFromPlace" Width="100px" runat="server" Value='<%# Eval("StartFromPlace")%>' 
                                                    ClientInstanceName='<%# "StartFromPlace"+Container.VisibleIndex.ToString() %>'>
                                                </dx:ASPxTextBox>
                                            </DataItemTemplate>        
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="目的地" FieldName="EndToPlace" Width="100px" VisibleIndex="3">
                                            <Settings AllowCellMerge="False"/>
                                            <DataItemTemplate>
                                                <dx:ASPxTextBox ID="EndToPlace" Width="100px" runat="server" Value='<%# Eval("EndToPlace")%>' 
                                                    ClientInstanceName='<%# "EndToPlace"+Container.VisibleIndex.ToString() %>'>
                                                </dx:ASPxTextBox>
                                            </DataItemTemplate>        
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="出发时间" FieldName="StartDateTime" Width="130px" VisibleIndex="4">
                                            <Settings AllowCellMerge="False"/>
                                            <DataItemTemplate>
                                                <dx:ASPxDateEdit ID="StartDateTime" runat="server" EditFormat="Custom" Width="130"  UseMaskBehavior="true" EditFormatString="yyyy/MM/dd HH:mm"
                                                    ClientInstanceName='<%# "StartDateTime"+Container.VisibleIndex.ToString() %>'
                                                    DisabledStyle-Border-BorderStyle="None" DisabledStyle-ForeColor="Black">
                                                    <TimeSectionProperties Visible="true">
                                                        <TimeEditProperties EditFormatString="HH:mm" />
                                                    </TimeSectionProperties>
                                                    <CalendarProperties><FastNavProperties DisplayMode="Inline" /></CalendarProperties>
                                                </dx:ASPxDateEdit>
                                            </DataItemTemplate>        
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="预算费用" FieldName="BudgetCost" Width="90px" VisibleIndex="6">
                                            <Settings AllowCellMerge="False"/>
                                            <DataItemTemplate>
                                                <dx:ASPxTextBox ID="BudgetCost" Width="90px" runat="server" Value='<%# Eval("BudgetCost")%>' 
                                                    ClientSideEvents-ValueChanged='<%# "function(s,e){RefreshRow_HR();}" %>'
                                                    ClientInstanceName='<%# "BudgetCost"+Container.VisibleIndex.ToString() %>'  HorizontalAlign="Right">
                                                    <ValidationSettings ValidationGroup="ValueValidationGroup_HR" Display="Dynamic" ErrorTextPosition="Bottom">
                                                        <RegularExpression ErrorText="请输入正数！" ValidationExpression="^[+]{0,1}(\d+)$|^[+]{0,1}(\d+\.\d+)$" />
                                                    </ValidationSettings>
                                                </dx:ASPxTextBox>
                                            </DataItemTemplate>        
                                        </dx:GridViewDataTextColumn> 
                                        <dx:GridViewDataTextColumn Caption="交通工具" FieldName="Vehicle" Width="60px" VisibleIndex="7">
                                            <Settings AllowCellMerge="False"/>
                                            <DataItemTemplate>
                                                <table>
                                                    <tr>
                                                        <td>
                                                           <dx:ASPxTextBox ID="Vehicle" Width="60px" runat="server" Value='<%# Eval("Vehicle")%>' 
                                                                ClientInstanceName='<%# "Vehicle"+Container.VisibleIndex.ToString() %>' Border-BorderWidth="0"  ReadOnly="true">
                                                            </dx:ASPxTextBox>
                                                        </td>
                                                        <td><i id="Vehicle_i_<%#Container.VisibleIndex.ToString() %>" 
                                                            class="fa fa-search <% =ViewState["Vehicle_i"].ToString() == "Y" ? "i_hidden" : "i_show" %>" 
                                                            onclick="Get_Vehicle(<%# Container.VisibleIndex %>,'')"></i>
                                                        </td>
                                                    </tr>
                                                </table> 
                                            </DataItemTemplate>        
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="备注" FieldName="Remark" Width="150px" VisibleIndex="8">
                                            <Settings AllowCellMerge="False"/>
                                            <DataItemTemplate>
                                                <dx:ASPxTextBox ID="Remark" Width="150px" runat="server" Value='<%# Eval("Remark")%>' 
                                                    ClientInstanceName='<%# "Remark"+Container.VisibleIndex.ToString() %>'>
                                                </dx:ASPxTextBox>
                                            </DataItemTemplate>        
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="预定班次(人事填写)" FieldName="ScheduledFlight" Width="100px" VisibleIndex="9" HeaderStyle-CssClass="hr">
                                            <Settings AllowCellMerge="False"/>
                                            <DataItemTemplate>
                                                <dx:ASPxTextBox ID="ScheduledFlight" Width="100px" runat="server" Value='<%# Eval("ScheduledFlight")%>' 
                                                    ClientInstanceName='<%# "ScheduledFlight"+Container.VisibleIndex.ToString() %>' Border-BorderWidth="0"   ReadOnly="true">
                                                </dx:ASPxTextBox>
                                            </DataItemTemplate>        
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="实际费用(人事填写)" FieldName="ActualCost" Width="90px" VisibleIndex="10" HeaderStyle-CssClass="hr">
                                            <Settings AllowCellMerge="False"/>
                                            <DataItemTemplate>
                                                <dx:ASPxTextBox ID="ActualCost" Width="90px" runat="server" Value='<%# Eval("ActualCost")%>' 
                                                    ClientSideEvents-ValueChanged='<%# "function(s,e){RefreshRow_HR_AC();}" %>'
                                                    ClientInstanceName='<%# "ActualCost"+Container.VisibleIndex.ToString() %>' HorizontalAlign="Right" Border-BorderWidth="0"   ReadOnly="true">
                                                    <ValidationSettings ValidationGroup="ValueValidationGroup_HR" Display="Dynamic" ErrorTextPosition="Bottom">
                                                        <RegularExpression ErrorText="请输入正数！" ValidationExpression="^[+]{0,1}(\d+)$|^[+]{0,1}(\d+\.\d+)$" />
                                                    </ValidationSettings>
                                                </dx:ASPxTextBox>
                                            </DataItemTemplate>        
                                        </dx:GridViewDataTextColumn>
                                                                             
                                        <dx:GridViewDataTextColumn FieldName="id" Width="0px">
                                            <HeaderStyle CssClass="hidden" />
                                            <CellStyle CssClass="hidden"></CellStyle>
                                            <FooterCellStyle CssClass="hidden"></FooterCellStyle>
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="FIN_T_No" Width="0px">
                                            <HeaderStyle CssClass="hidden" />
                                            <CellStyle CssClass="hidden"></CellStyle>
                                            <FooterCellStyle CssClass="hidden"></FooterCellStyle>
                                        </dx:GridViewDataTextColumn>
                                    </Columns>       
                                    <TotalSummary>
                                        <dx:ASPxSummaryItem DisplayFormat="<b>预算合计:{0:N2}</b>" FieldName="BudgetCost" SummaryType="Sum" />
                                        <dx:ASPxSummaryItem DisplayFormat="<b><font color=blue>实际合计:{0:N2}</font></b>" FieldName="ActualCost" SummaryType="Sum" />
                                    </TotalSummary>                                            
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

