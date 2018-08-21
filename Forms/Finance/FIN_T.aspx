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

        $(document).ready(function () {
            $("#mestitle").html("【差旅申请单】");

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

        });

        function Ini_Set_IsHrReserve(){
            var ini_bf=false;
            $("[id$=gv_d] tr[class*=DataRow]").each(function (index, item) { 
                if($(item).children("td:last-child").text()=="T001" || $(item).children("td:last-child").text()=="T002"){
                    var IsHrReserve = eval('IsHrReserve' + index);                  
                    if (IsHrReserve.GetValue()=="是") {
                        //BudgetTotalCost.SetEnabled(false);

                        $("#MainContent_gv_d_cell"+index+"_2_BudgetTotalCost_"+index).addClass("dxeTextBox_read");
                        $("#MainContent_gv_d_cell"+index+"_2_BudgetTotalCost_"+index+"_I").attr("readOnly","readOnly").addClass("dxeTextBox_read");          
                        
                        ini_bf=true;      
                    }else {
                        //BudgetTotalCost.SetEnabled(true);

                        $("#MainContent_gv_d_cell"+index+"_2_BudgetTotalCost_"+index).removeClass("dxeTextBox_read");
                        $("#MainContent_gv_d_cell"+index+"_2_BudgetTotalCost_"+index+"_I").removeAttr("readOnly").removeClass("dxeTextBox_read");
                    }                    
                }
                if($(item).children("td:last-child").text()=="T007"){
                    var BudgetTotalCost = eval('BudgetTotalCost' + index);
                    BudgetTotalCost.SetText(0)
                }
            });
            if (ini_bf) {
                $("#CJJH input[id*='IsHrReserveByForm']").val("是");

                $("#div_dtl_hr").css("display","block");
                gv_d_hr.PerformCallback("clear");
            }else {
                $("#CJJH input[id*='IsHrReserveByForm']").val("否");

                $("#div_dtl_hr").css("display","none");
                gv_d_hr.PerformCallback("clear");
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
                layer.alert("【预计结束时间】必须大于等于【预计出发时间】.");
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
                    var BudgetTotalCost = eval('BudgetTotalCost' + index);
                    BudgetTotalCost.SetText(BTC_T007);
                    RefreshRow(index);
                }
            });

        }

        //提出自定流程 JS 
        function setComment(val) {
            $('#comment', parent.document).val(val);
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
        #div_dtl td{
             padding-bottom:3px;
        }
        #div_dtl_hr td{
             padding-bottom:3px;
        }
        .dxeButtonEdit td{
             padding-bottom:0px;
        }
    </style>

    <script type="text/javascript">

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

        function RefreshRow(vi) {
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
                    $("#CJJH input[id*='BudgetTotalCostByForm']").val(fmoney(BTC,2));
                }   
            });
        }

        function RefreshRow_HR(vi) {
            var BC=0;
            $("[id$=gv_d_hr] tr[class*=DataRow]").each(function (index, item) { 
                var BudgetCost = eval('BudgetCost' + index);
                if (BudgetCost.GetText()!="") {
                    BC=BC+Number(BudgetCost.GetText());
                }
            });
            //grid底部total值更新
            $("[id$=gv_d_hr] tr[id*=DXFooterRow]").find('td').each(function () {
                if(($.trim($(this).text())).indexOf("预算合计")>-1){
                    $(this).html("<b>预算合计:"+fmoney(BC,2)+"</b>");//$(this).text("<b>合计:"+fmoney(BC,2)+"</b>");
                }   
            });
        }

        function RefreshRow_HR_AC(vi) {
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
                    $(this).html("<b>实际合计:"+fmoney(AC,2)+"</b>");//$(this).text("<b>合计:"+fmoney(AC,2)+"</b>");
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
            var IsHrReserve = eval('IsHrReserve' + vi);
            var BudgetTotalCost = eval('BudgetTotalCost' + vi);
            BudgetTotalCost.SetText("");
            if (IsHrReserve.GetValue()=="是") {
                //BudgetTotalCost.SetEnabled(false);

                $("#MainContent_gv_d_cell"+vi+"_2_BudgetTotalCost_"+vi).addClass("dxeTextBox_read");
                $("#MainContent_gv_d_cell"+vi+"_2_BudgetTotalCost_"+vi+"_I").attr("readOnly","readOnly").addClass("dxeTextBox_read");                
            }else {
                //BudgetTotalCost.SetEnabled(true);

                $("#MainContent_gv_d_cell"+vi+"_2_BudgetTotalCost_"+vi).removeClass("dxeTextBox_read");
                $("#MainContent_gv_d_cell"+vi+"_2_BudgetTotalCost_"+vi+"_I").removeAttr("readOnly").removeClass("dxeTextBox_read");
            }
            var bf=false;
            $("[id$=gv_d] tr[class*=DataRow]").each(function (index, item) { 
                if($(item).children("td:last-child").text()=="T001" || $(item).children("td:last-child").text()=="T002"){
                    var IsHrReserve = eval('IsHrReserve' + index);
                    if (IsHrReserve.GetValue()=="是") {
                        bf=true;
                        return false;
                    }
                }
            });
            if (bf) {
                $("#CJJH input[id*='IsHrReserveByForm']").val("是");

                $("#div_dtl_hr").css("display","block");
                gv_d_hr.PerformCallback("clear");
            }else {
                $("#CJJH input[id*='IsHrReserveByForm']").val("否");

                $("#div_dtl_hr").css("display","none");
                gv_d_hr.PerformCallback("clear");
            }

        }

        function Get_Traveler(vi){
            var url = "/select/select_PlanAttendant_dtl.aspx?vi="+vi+"&PA="+$("#CJJH input[id*='PlanAttendant']").val();

            layer.open({
                title:'随行人员明细选择',
                type: 2,
                area: ['450px', '550px'],
                fixed: false, //不固定
                maxmin: true,
                content: url
            });  
        }

        function setvalue_Traveler(vi,workcode,workname){
            var TravelerName= eval('TravelerName' + vi);var TravelerId= eval('TravelerId' + vi);
            TravelerName.SetText(workcode);TravelerId.SetText(workname);
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
            if($("#CJJH input[id*='PlanAttendant']").val()==""){
                msg+="【随行人员】不可为空.<br />";
            }
            if($("#CJJH input[id*='TravelPlace']").val()==""){
                msg+="【出差地点】不可为空.<br />";
            }
            if($("#CJJH input[id*='TravelReason']").val()==""){
                msg+="【出差事由】不可为空.<br />";
            }
            
            if(msg!=""){  
                flag=false;
                layer.alert(msg);
                return flag;
            }
            
            //----------------------------------------------------------------------------逻辑验证
            //验证 预计出发时间 预计结束时间 大小关系   
            if((new Date())>(new Date(Date.parse($("#CJJH input[id*='PlanStartTime']").val())))){
                msg+="【预计出发时间】必须大于等于【当前时间】.<br />";
            }
            if(compareDate($("#CJJH input[id*='PlanStartTime']").val(),$("#CJJH input[id*='PlanEndTime']").val())){
                msg+="【预计结束时间】必须大于等于【预计出发时间】.<br />";
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

            return flag;
        }

        function compareDate(s1,s2){
            return ((new Date(s1.replace(/-/g,"\/")))>(new Date(s2.replace(/-/g,"\/"))));
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

    <div class="col-md-12" >  

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
                                <td><asp:TextBox ID="FormNo" runat="server" class="lineread"  ReadOnly="true" placeholder="自动产生" Width="260px" /></td>
                                <td><font color="red">&nbsp;</font>申请日期</td>
                                <td><asp:TextBox ID="ApplyDate"  runat="server" class="lineread" ReadOnly="True" Width="260px" /></td>
                            </tr>
                            <tr>
                                <td><font color="red">&nbsp;</font>申请人</td>
                                <td>
                                    <div class="form-inline">
                                        <asp:TextBox runat="server" ID="ApplyId" class="lineread" ReadOnly="True" Width="50px"></asp:TextBox>
                                        <asp:TextBox runat="server" ID="ApplyName"  class="lineread" ReadOnly="True" Width="82px"></asp:TextBox>
                                        <asp:TextBox runat="server" ID="ApplyTelephone" class="lineread" Width="120px"/>
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
                                    onclick="laydate({type: 'date',format: 'YYYY/MM/DD',min:laydate.now(1),max:$('#CJJH input[id*=\'PlanEndTime\']').val(),choose: function(dates){Auto_Calculate();}});" /></td>
                                <td><font color="red">*</font>预计结束日期</td>
                                <td><asp:TextBox ID="PlanEndTime"  runat="server" class="linewrite" ReadOnly="True" Width="260px" 
                                    onclick="laydate({type: 'date',format: 'YYYY/MM/DD',min:$('#CJJH input[id*=\'PlanStartTime\']').val(),choose: function(dates){Auto_Calculate();}});" /></td>
                            </tr>
                            <tr>
                                <td><font color="red">&nbsp;</font>预计出差天数</td>
                                <td>
                                    <asp:TextBox ID="PlanDays" runat="server" class="lineread" ReadOnly="True" Width="260px" Text="0" />
                                </td>
                                <td><font color="red">*</font>随行人员</td>
                                <td><asp:TextBox runat="server" ID="PlanAttendant" class="lineread" Width="240px" ReadOnly="True"></asp:TextBox>
                                    <i id="PlanAttendant_i" class="fa fa-search" onclick="Get_PlanAttendant()"></i>
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
                                ClientInstanceName="gv_d"  OnHtmlRowCreated="gv_d_HtmlRowCreated"> 
                            <SettingsPager PageSize="1000"></SettingsPager>
                            <Settings ShowFooter="True" />
                            <SettingsBehavior AllowSelectByRowClick="True" AllowDragDrop="False" AllowSort="False" />
                            <Columns>
                                <dx:GridViewDataTextColumn Caption="#" FieldName="numid" Width="40px" VisibleIndex="0"></dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="费用类型" FieldName="CostCodeDesc" Width="220px" VisibleIndex="1"></dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="预算金额合计" FieldName="BudgetTotalCost" Width="100px" VisibleIndex="2">
                                    <Settings AllowCellMerge="False"/>
                                    <DataItemTemplate>
                                        <dx:ASPxTextBox ID="BudgetTotalCost" Width="100px" runat="server" Value='<%# Eval("BudgetTotalCost")%>'
                                            ClientSideEvents-ValueChanged='<%# "function(s,e){RefreshRow("+Container.VisibleIndex+");}" %>' 
                                            ClientInstanceName='<%# "BudgetTotalCost"+Container.VisibleIndex.ToString() %>' HorizontalAlign="Right">
                                            <ValidationSettings ValidationGroup="ValueValidationGroup" Display="Dynamic" ErrorTextPosition="Bottom">
                                                <RegularExpression ErrorText="请输入正数！" ValidationExpression="^[+]{0,1}(\d+)$|^[+]{0,1}(\d+\.\d+)$" />
                                            </ValidationSettings>
                                        </dx:ASPxTextBox>
                                    </DataItemTemplate>        
                                </dx:GridViewDataTextColumn>                                                
                                <dx:GridViewDataTextColumn Caption="人事预定并结算" FieldName="IsHrReserve" Width="100px" VisibleIndex="3">
                                    <Settings AllowCellMerge="False" />
                                    <DataItemTemplate>       
                                        <dx:ASPxComboBox ID="IsHrReserve" runat="server" ValueType="System.String" Width="100px"
                                            ClientSideEvents-SelectedIndexChanged='<%# "function(s,e){Get_IsHrReserve("+Container.VisibleIndex+");}" %>'   
                                            ClientInstanceName='<%# "IsHrReserve"+Container.VisibleIndex.ToString() %>'>
                                            <Items>
                                                <dx:ListEditItem Text="是"  Value="是" Selected="true" />
                                                <dx:ListEditItem Text="否"  Value="否" />
                                            </Items>
                                        </dx:ASPxComboBox>
                                    </DataItemTemplate>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="预算说明" FieldName="BudgetRemark" Width="350px" VisibleIndex="4">
                                    <Settings AllowCellMerge="False" />
                                    <DataItemTemplate>                
                                        <dx:ASPxTextBox ID="BudgetRemark" Width="350px" runat="server" Value='<%# Eval("BudgetRemark")%>' ></dx:ASPxTextBox>                
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
                                <asp:Button ID="btndel" runat="server" Text="删除" class="btn btn-default" style="width:60px; height:32px;"  OnClick="btndel_Click" />

                                 <dx:aspxgridview ID="gv_d_hr" runat="server" AutoGenerateColumns="False" KeyFieldName="numid" Theme="MetropolisBlue" 
                                     ClientInstanceName="gv_d_hr"  EnableTheming="True" OnCustomCallback="gv_d_hr_CustomCallback"><%--OnHtmlRowCreated="gv_d_hr_HtmlRowCreated" --%>
                                    <SettingsPager PageSize="1000"></SettingsPager>
                                    <Settings ShowFooter="True" />
                                    <SettingsBehavior AllowSelectByRowClick="True" AllowDragDrop="False" AllowSort="False" />
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
                                                <%--<dx:ASPxComboBox ID="TravelerName" runat="server" ValueType="System.String" Width="100px"  
                                                    ClientInstanceName='<%# "TravelerName"+Container.VisibleIndex.ToString() %>' >
                                                </dx:ASPxComboBox>--%>
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
                                        <dx:GridViewDataTextColumn Caption="出发日期" FieldName="StartDate" Width="100px" VisibleIndex="4">
                                            <Settings AllowCellMerge="False"/>
                                            <DataItemTemplate>
                                                <%--<dx:ASPxTextBox ID="StartDate" Width="100px" runat="server" Value='<%# Eval("StartDate")%>' 
                                                    ClientInstanceName='<%# "StartDate"+Container.VisibleIndex.ToString() %>' Border-BorderWidth="0"   ReadOnly="true"
                                                    onclick="laydate({type: 'date',format: 'YYYY/MM/DD'})">
                                                </dx:ASPxTextBox>--%>
                                                <dx:ASPxDateEdit ID="StartDate" runat="server" Width="100px" DisplayFormatString="yyyy/MM/dd" EditFormatString="yyyy/MM/dd"></dx:ASPxDateEdit>
                                            </DataItemTemplate>        
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="出发时间" FieldName="StartTime" Width="80px" VisibleIndex="5">
                                            <Settings AllowCellMerge="False"/>
                                            <DataItemTemplate>
                                                <dx:ASPxTimeEdit ID="StartTime" runat="server" Width="80px" DisplayFormatString="HH:mm" EditFormatString="HH:mm"></dx:ASPxTimeEdit>
                                            </DataItemTemplate>        
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="预算费用" FieldName="BudgetCost" Width="80px" VisibleIndex="6">
                                            <Settings AllowCellMerge="False"/>
                                            <DataItemTemplate>
                                                <dx:ASPxTextBox ID="BudgetCost" Width="80px" runat="server" Value='<%# Eval("BudgetCost")%>' 
                                                    ClientSideEvents-ValueChanged='<%# "function(s,e){RefreshRow_HR("+Container.VisibleIndex+");}" %>'
                                                    ClientInstanceName='<%# "BudgetCost"+Container.VisibleIndex.ToString() %>'  HorizontalAlign="Right">
                                                    <ValidationSettings ValidationGroup="ValueValidationGroup" Display="Dynamic" ErrorTextPosition="Bottom">
                                                        <RegularExpression ErrorText="请输入正数！" ValidationExpression="^[+]{0,1}(\d+)$|^[+]{0,1}(\d+\.\d+)$" />
                                                    </ValidationSettings>
                                                </dx:ASPxTextBox>
                                            </DataItemTemplate>        
                                        </dx:GridViewDataTextColumn> 
                                        <dx:GridViewDataTextColumn Caption="交通工具" FieldName="Vehicle" Width="60px" VisibleIndex="7">
                                            <Settings AllowCellMerge="False"/>
                                            <DataItemTemplate>
                                                <dx:ASPxComboBox ID="Vehicle" runat="server" ValueType="System.String" Width="60px" 
                                                    ClientInstanceName='<%# "Vehicle"+Container.VisibleIndex.ToString() %>'>
                                                    <Items>
                                                        <dx:ListEditItem Text="飞机"  Value="飞机" />
                                                        <dx:ListEditItem Text="火车"  Value="火车" />
                                                    </Items>
                                                </dx:ASPxComboBox>
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
                                        <dx:GridViewDataTextColumn Caption="预定班次" FieldName="ScheduledFlight" Width="100px" VisibleIndex="9">
                                            <Settings AllowCellMerge="False"/>
                                            <DataItemTemplate>
                                                <dx:ASPxTextBox ID="ScheduledFlight" Width="100px" runat="server" Value='<%# Eval("ScheduledFlight")%>' 
                                                    ClientInstanceName='<%# "ScheduledFlight"+Container.VisibleIndex.ToString() %>'>
                                                </dx:ASPxTextBox>
                                            </DataItemTemplate>        
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="实际费用" FieldName="ActualCost" Width="80px" VisibleIndex="10">
                                            <Settings AllowCellMerge="False"/>
                                            <DataItemTemplate>
                                                <dx:ASPxTextBox ID="ActualCost" Width="80px" runat="server" Value='<%# Eval("ActualCost")%>' 
                                                    ClientSideEvents-ValueChanged='<%# "function(s,e){RefreshRow_HR_AC("+Container.VisibleIndex+");}" %>'
                                                    ClientInstanceName='<%# "ActualCost"+Container.VisibleIndex.ToString() %>' HorizontalAlign="Right">
                                                    <ValidationSettings ValidationGroup="ValueValidationGroup" Display="Dynamic" ErrorTextPosition="Bottom">
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
                                        <dx:ASPxSummaryItem DisplayFormat="<b>实际合计:{0:N2}</b>" FieldName="ActualCost" SummaryType="Sum" />
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

    </div>

</asp:Content>

