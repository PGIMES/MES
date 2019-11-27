<%@ Page Title="客户日程申请单" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="CustomerSchedule.aspx.cs" Inherits="Forms_Sale_CustomerSchedule" %>

<%@ Register assembly="DevExpress.Web.v17.2, Version=17.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web" tagprefix="dx" %>

<asp:Content ID="HeaderContent" ContentPlaceHolderID="HeadContent" Runat="Server">
    <link href="/Content/css/bootstrap.min.css" rel="stylesheet" />
    <script src="/Content/js/jquery.min.js"></script>
    <script src="/Content/js/bootstrap.min.js"></script>
    <script src="/Content/js/layer/layer.js"></script>
    <script src="/Content/js/plugins/layer/laydate/laydate.js"></script>
    <link href="/Content/css/custom.css?t=20190516" rel="stylesheet" />
    <script type="text/javascript">
        var UserId = '<%=UserId%>';
        var js_SQ_StepID='<%=SQ_StepID%>';

        function getQueryString(name) {
            var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)", "i");
            var r = decodeURI(window.location.search).substr(1).match(reg);
            if (r != null) return unescape(r[2]); return null;
        }
        var stepid = getQueryString("stepid");
        var state = getQueryString("state");

        $(document).ready(function () {
            $("#mestitle").html("【客户日程申请单】");//<a href='/userguide/TGuide.pps' target='_blank' class='h5' style='color:red'>使用说明</a>

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

            $("#wlXX input[id*='domain']").change(function () {  
                if($("[id$=gv] input[type!=hidden][id*=delivery_mode]").length>0){
                    $("#wlXX input[id*='domain']").val($("#wlXX input[id*='hd_domain']").val());
                    layer.alert("客户日程明细已存在，不可修改申请工厂");
                    return flag;
                }
            });

            $("#wlXX input[id*='cust_part']").change(function () {  
                var cust_part=$("#wlXX input[id*='cust_part']").val();
                $("#wlXX input[id*='cust_partd']").val(cust_part);
            });
            
        });

        //提出自定流程 JS 
        function setComment(val) {
            $('#comment', parent.document).val(val);
        }

        function gv_SelectionChanged(s, e) {
            //gv_color();
        }
        function gv_color(){
            $("[id$=gv] tr[class*=DataRow]").each(function (index, item) { 

                var class_checked=$.trim($(item).find("td:eq(0) span:first").attr("class"));                        
                if(class_checked.indexOf("CheckBoxChecked")>-1){                    
                    $(item).find("table[id*=domain_"+index+"]").css("background-color","#FDF7D9");
                    $(item).find("input[id*=domain_"+index+"]").css("background-color","#FDF7D9");

                    $(item).find("table[id*=bm_"+index+"]").css("background-color","#FDF7D9");
                    $(item).find("input[id*=bm_"+index+"]").css("background-color","#FDF7D9");

                    $(item).find("table[id*=mc_"+index+"]").css("background-color","#FDF7D9");
                    $(item).find("input[id*=mc_"+index+"]").css("background-color","#FDF7D9");

                    $(item).find("table[id*=bclb_"+index+"]").css("background-color","#FDF7D9");
                    $(item).find("input[id*=bclb_"+index+"]").css("background-color","#FDF7D9");

                    $(item).find("table[id*=djyl_"+index+"]").css("background-color","#FDF7D9");
                    $(item).find("input[id*=djyl_"+index+"]").css("background-color","#FDF7D9");

                    $(item).find("table[id*=cl_"+index+"]").css("background-color","#FDF7D9");
                    $(item).find("input[id*=cl_"+index+"]").css("background-color","#FDF7D9");

                    $(item).find("table[id*=cc_"+index+"]").css("background-color","#FDF7D9");
                    $(item).find("input[id*=cc_"+index+"]").css("background-color","#FDF7D9");

                    $(item).find("table[id*=dz_"+index+"]").css("background-color","#FDF7D9");
                    $(item).find("input[id*=dz_"+index+"]").css("background-color","#FDF7D9");

                    $(item).find("table[id*=zz_"+index+"]").css("background-color","#FDF7D9");
                    $(item).find("input[id*=zz_"+index+"]").css("background-color","#FDF7D9");

                    $(item).find("table[id*=dj_"+index+"]").css("background-color","#FDF7D9");
                    $(item).find("input[id*=dj_"+index+"]").css("background-color","#FDF7D9");

                    $(item).find("table[id*=zj_"+index+"]").css("background-color","#FDF7D9");
                    $(item).find("input[id*=zj_"+index+"]").css("background-color","#FDF7D9");

                    $(item).find("table[id*=gys_"+index+"]").css("background-color","#FDF7D9");
                    $(item).find("input[id*=gys_"+index+"]").css("background-color","#FDF7D9");
                }else {
                    $(item).find("table[id*=domain_"+index+"]").css("background-color","#FFFFFF");
                    $(item).find("input[id*=domain_"+index+"]").css("background-color","#FFFFFF");

                    $(item).find("table[id*=bm_"+index+"]").css("background-color","#FFFFFF");
                    $(item).find("input[id*=bm_"+index+"]").css("background-color","#FFFFFF");

                    $(item).find("table[id*=mc_"+index+"]").css("background-color","#FFFFFF");
                    $(item).find("input[id*=mc_"+index+"]").css("background-color","#FFFFFF");

                    $(item).find("table[id*=bclb_"+index+"]").css("background-color","#FFFFFF");
                    $(item).find("input[id*=bclb_"+index+"]").css("background-color","#FFFFFF");

                    $(item).find("table[id*=djyl_"+index+"]").css("background-color","#FFFFFF");
                    $(item).find("input[id*=djyl_"+index+"]").css("background-color","#FFFFFF");

                    $(item).find("table[id*=cl_"+index+"]").css("background-color","#FFFFFF");
                    $(item).find("input[id*=cl_"+index+"]").css("background-color","#FFFFFF");

                    $(item).find("table[id*=cc_"+index+"]").css("background-color","#FFFFFF");
                    $(item).find("input[id*=cc_"+index+"]").css("background-color","#FFFFFF");

                    $(item).find("table[id*=dz_"+index+"]").css("background-color","#FFFFFF");
                    $(item).find("input[id*=dz_"+index+"]").css("background-color","#FFFFFF");

                    $(item).find("table[id*=zz_"+index+"]").css("background-color","#FFFFFF");
                    $(item).find("input[id*=zz_"+index+"]").css("background-color","#FFFFFF");

                    $(item).find("table[id*=dj_"+index+"]").css("background-color","#FFFFFF");
                    $(item).find("input[id*=dj_"+index+"]").css("background-color","#FFFFFF");

                    $(item).find("table[id*=zj_"+index+"]").css("background-color","#FFFFFF");
                    $(item).find("input[id*=zj_"+index+"]").css("background-color","#FFFFFF");

                    $(item).find("table[id*=gys_"+index+"]").css("background-color","#FFFFFF");
                    $(item).find("input[id*=gys_"+index+"]").css("background-color","#FFFFFF");
                }

                if($(item).find("input[id*=sl_"+index+"]").attr("readOnly")){
                    var class_checked=$.trim($(item).find("td:eq(0) span:first").attr("class"));                        
                    if(class_checked.indexOf("CheckBoxChecked")>-1){                    
                        $(item).find("table[id*=sl_"+index+"]").css("background-color","#FDF7D9");
                        $(item).find("input[id*=sl_"+index+"]").css("background-color","#FDF7D9");
                    }else {
                        $(item).find("table[id*=sl_"+index+"]").css("background-color","#FFFFFF");
                        $(item).find("input[id*=sl_"+index+"]").css("background-color","#FFFFFF");
                    }
                }else {
                    
                }
            });
        }

        //设定表字段状态（可编辑性）
        var tabName="pgi_customerschedule_main_form";//表名
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
        var tabName2="pgi_customerschedule_dtl_form";//表名
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

            $("#DQXX input[id*='ApplyId']").val(workcode);
            $("#DQXX input[id*='ApplyName']").val(lastname);
            $("#DQXX input[id*='ApplyTelephone']").val(telephone);
            $("#DQXX input[id*='ApplyDeptName']").val(dept_name);
            
        }

        function Get_part() 
        {
            var url = "/select/select_CustomerSchedule_part.aspx";

            layer.open({
                title:'PGI零件号选择',
                type: 2,
                area: ['650px', '500px'],
                fixed: false, //不固定
                maxmin: true,
                content: url
            }); 
        }

        function setvalue_part(domain, part, wlmc) 
        {    
            $("#wlXX input[id*='typeno']").val('新增');

            $("#wlXX input[id*='domain']").val(domain);
            $("#wlXX input[id*='part']").val(part);
            //$("#<%=part.ClientID%>").val(part);
            $("#wlXX input[id*='cust_part']").val(wlmc);
            $("#wlXX input[id*='cust_partd']").val(wlmc);
        }
        function Ondelivery_modeChanged(cmbDelivery,vi){
            var domain=$("#wlXX input[id*='domain']").val();

            var site = eval('site' + vi);
            site.PerformCallback(domain+'|'+cmbDelivery.GetValue().toString());
            
            var ship = eval('ship' + vi);
            ship.PerformCallback(domain+'|'+cmbDelivery.GetValue().toString()+'|'+site.GetText());            
        }
        function OnSiteChanged(cmbSite,vi){  
            var domain=$("#wlXX input[id*='domain']").val();

            var delivery_mode = eval('delivery_mode' + vi);   
       
            var ship = eval('ship' + vi);
            ship.PerformCallback(domain+'|'+delivery_mode.GetText()+'|'+cmbSite.GetValue().toString());               
            
            var ysk_site = eval('ysk_site' + vi);      
            ysk_site.SetText(cmbSite.GetValue().toString());
        }
        function OnShipChanged(cmbShip,vi){
            var nbr = eval('nbr' + vi);
            nbr.SetText(cmbShip.GetValue().toString());

            var domain=$("#wlXX input[id*='domain']").val();

            var delivery_mode = eval('delivery_mode' + vi); 
            var site = eval('site' + vi);
                        
            var shipname = eval('shipname' + vi);
            var bill = eval('bill' + vi);
            var curr = eval('curr' + vi);
            var pr_list = eval('pr_list' + vi);
            var taxable = eval('taxable' + vi);
            var taxc = eval('taxc' + vi);

            $.ajax({
                    type: "post",
                    url: "CustomerSchedule.aspx/GetDataByShip",
                    data: "{'delivery_mode':'" + delivery_mode.GetText() + "','site':'" + site.GetText() + "','ship':'" + cmbShip.GetValue().toString() + "','domain':'" + domain + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    async: false,//默认是true，异步；false为同步，此方法执行完在执行下面代码
                    success: function (data) {
                        var obj=eval(data.d);

                        shipname.SetText(obj[0].shipname);
                        bill.SetText(obj[0].bill);
                        curr.SetText(obj[0].curr);
                        pr_list.SetText(obj[0].pr_list);
                        taxable.SetText(obj[0].taxable);
                        taxc.SetText(obj[0].taxc);
                    }

                });
            
        }
    </script>

    <script type="text/javascript">
        var uploadedFiles = [];
        function onFileUploadComplete(s, e) {
            if(e.callbackData) {
                var fileData = e.callbackData.split('|');uploadedFiles.push(fileData);$("#<%=ip_filelist.ClientID%>").val(uploadedFiles.join(";"));               
                bind_table(fileData);
            }
        }
        
        function bind_table(fileData){
             var fileName = fileData[0],
                 fileUrl = fileData[1],
                 fileSize = fileData[2];    

            var eqno=uploadedFiles.length-1;

            var tbody_tr='<tr id="tr_'+eqno+'"><td Width="400px"><a href="'+fileUrl+'" target="_blank">'+fileName+'</a></td>'
                    +'<td Width="60px">'+fileSize+'</td>'
                    +'<td><span style="color:blue;cursor:pointer" id="tbl_delde" onclick ="del_data(tr_'+eqno+','+eqno+')" >删除</span></td>'
                    +'</tr>';

            $('#tbl_filelist').append(tbody_tr);
        }


        function del_data(a,eno){
            $(a).remove();
            uploadedFiles[eno]=null;
           $("#<%=ip_filelist.ClientID%>").val(uploadedFiles.join(";"));
        }
        
    </script>

    <script type="text/javascript">

        function validate(action){
            var flag=true;var msg="";
            
            <%=ValidScript%>
                
            //申请步骤验证
            if(stepid==null || stepid.toLowerCase()==js_SQ_StepID.toLowerCase()){
            
                if($("#DQXX input[id*='ApplyId']").val()=="" || $("#DQXX input[id*='ApplyName']").val()==""){
                    msg+="【申请人】不可为空.<br />";
                }
                if($("#DQXX input[id*='ApplyDeptId']").val()=="" || $("#DQXX input[id*='ApplyDeptName']").val()==""){
                    msg+="【申请人部门】不可为空.<br />";
                }

                if(msg!=""){  
                    flag=false;
                    layer.alert(msg);
                    return flag;
                }

                var typeno=$("#wlXX input[id*='typeno']").val();
                var domain=$("#wlXX input[id*='domain']").val();
                var part=$("#<%=part.ClientID%>").val();
                var cust_part=$("#wlXX input[id*='cust_part']").val();

                if(action=='submit'){   
                    if(typeno==""){
                        msg+="【申请类型】不可为空.<br />";
                    }             
                    if (domain=="") {
                        msg+="请选择【申请工厂】<br />";
                    }else if(domain!="100" && domain!="200"){
                        msg+="【申请工厂】只能是100、200.<br />";                    
                    }
                    if (part=="") {
                        msg+="请选择【PGI_零件号】.<br />";
                    }else if(part.length<=5){
                        msg+="【PGI_零件号】长度必须大于5位.<br />";
                    }
                    if (cust_part=="") {
                        msg+="请输入【客户物料号】.<br />";
                    }
                    
                    if(msg!=""){  
                        flag=false;
                        layer.alert(msg);
                        return flag;
                    }

                    //----------------------------------------------------验证申请中的单子
                    var applyid=$("#DQXX input[id*='ApplyId']").val();
                    var formno=$("#DQXX input[id*='FormNo']").val();

                    $.ajax({
                        type: "post",
                        url: "CustomerSchedule.aspx/CheckData",
                        data: "{'applyid':'" + applyid + "','formno':'" + formno + "','part':'" + part + "','domain':'" + domain 
                                + "','cust_part':'" + cust_part + "','typeno':'" + typeno + "'}",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        async: false,//默认是true，异步；false为同步，此方法执行完在执行下面代码
                        success: function (data) {
                            var obj=eval(data.d);

                            if(obj[0].manager_flag!=""){ msg+=obj[0].manager_flag; }
                            if(obj[0].part_flag!=""){ msg+=obj[0].part_flag; }

                            if(msg!=""){  
                                flag=false;
                                layer.alert(msg);
                                return flag;
                            }
                        }

                    });

                    //----------------------------------------------------验证明细
                    if($("[id$=gv] input[type!=hidden][id*=delivery_mode]").length==0){
                        msg+="【客户日程明细】不可为空.<br />";
                    }else {
                        $("[id$=gv] tr[class*=DataRow]").each(function (index, item) { 
                            var delivery_mode = eval('delivery_mode' + index);var site = eval('site' + index);
                            var ship = eval('ship' + index);var shipname = eval('shipname' + index);var nbr = eval('nbr' + index);
                            var taxable = eval('taxable' + index);var taxc = eval('taxc' + index);     
                            var consignment = eval('consignment' + index);var consignment_loc = eval('consignment_loc' + index);
                             
                            if (delivery_mode.GetText()=="") { msg+="【客户日程明细】-第"+(index+1)+"行【发货方式】不可为空.<br />"; }
                            if (site.GetText()=="") { msg+="【客户日程明细】-第"+(index+1)+"行【发货自】不可为空.<br />"; }
                            if (ship.GetText()=="") { msg+="【客户日程明细】-第"+(index+1)+"行【发货至】不可为空.<br />"; }
                            if (shipname.GetText()=="") { msg+="【客户日程明细】-第"+(index+1)+"行【发货至名称】不可为空.<br />"; }
                            if (nbr.GetText()=="") { msg+="【客户日程明细】-第"+(index+1)+"行【销售订单】不可为空.<br />"; }
        
                            if (taxable.GetText()=="") { 
                                if(taxc.GetText()!=""){msg+="【客户日程明细】-第"+(index+1)+"行【应纳税】为空,【税率】必须为空.<br />"; }                
                            }
                            else if (taxable.GetText()=="no") { 
                                if(taxc.GetText()!="0"){msg+="【客户日程明细】-第"+(index+1)+"行【应纳税】为no,【税率】必须为0.<br />"; }                
                            }
                            else if (taxable.GetText()=="yes") { 
                                if(taxc.GetText()==""){msg+="【客户日程明细】-第"+(index+1)+"行【应纳税】为yes,【税率】不可为空.<br />"; }                                    
                            }

                            if (consignment.GetText()=="") { msg+="【客户日程明细】-第"+(index+1)+"行【寄售】不可为空.<br />"; }
                            else if (consignment.GetText()=="no") { 
                                if(consignment_loc.GetText()!=""){msg+="【客户日程明细】-第"+(index+1)+"行【寄售】为no,【寄售地点】不可有值.<br />"; }                                    
                            }
                            else if (consignment.GetText()=="yes") { 
                                if(consignment_loc.GetText()==""){msg+="【客户日程明细】-第"+(index+1)+"行【寄售】为yes,【寄售地点】不可为空.<br />"; }                                    
                            }

                            if (msg!="") {
                                return false;
                            }
                        });
                        /*
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
                        }*/
                    }

                    if(msg!=""){  
                        flag=false;
                        layer.alert(msg);
                        return flag;
                    }
                        
                    //若是中转库发，且 发货自等于域的话，发货至必须存在在地点表里    
                    //模型年的check:相同的 客户物料号，发货自，发货至，不同的PGI零件号，必须要有模型年，否则导入不进去qad                    
                    //销售订单，发货自，发货至，票据开往，物料号，客户项目号，模型年 必须唯一
                    $("[id$=gv] tr[class*=DataRow]").each(function (index, item) { 
                        var delivery_mode = eval('delivery_mode' + index);var site = eval('site' + index);
                        var ship = eval('ship' + index);var nbr = eval('nbr' + index);
                        var bill = eval('bill' + index);
                        var curr = eval('curr' + index);
                        var pr_list = eval('pr_list' + index);var line = eval('line' + index);
                        var modelyr = eval('modelyr' + index);
                                
                       $.ajax({
                            type: "post",
                            url: "CustomerSchedule.aspx/CheckData_dtl",
                            data: "{'formno':'" + formno + "','part':'" + part + "','domain':'" + domain + "','cust_part':'" + cust_part + "','typeno':'" + typeno
                                    + "','site':'" + site.GetText() + "','ship':'" + ship.GetText() + "','bill':'" + bill.GetText() + "','curr':'" + curr.GetText() 
                                    + "','pr_list':'" + pr_list.GetText() + "','modelyr':'" + modelyr.GetText() + "','nbr':'" + nbr.GetText() + "','delivery_mode':'" + delivery_mode.GetText() 
                                    + "','line':'" + line.GetText() + "','index':'" + (index+1) + "'}",
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            async: false,//默认是true，异步；false为同步，此方法执行完在执行下面代码
                            success: function (data) {
                                var obj=eval(data.d);

                                if(obj[0].flag!=""){ msg+=obj[0].flag; }

                                if(msg!=""){  
                                    flag=false;
                                    layer.alert(msg);
                                    return flag;
                                }
                            }

                        });
                    });
                    
                }

                if(action=='submit'){
                    if(!parent.checkSign()){
                        flag=false;return flag;
                    }
                }

            }

             
            

            return flag;
        }
    </script>

    <script>//20181108 add heguiqin
        function Add_check(){ 
            var part=$("#<%=part.ClientID%>").val();
            var domain=$("#wlXX input[id*='domain']").val();
            var msg="";
            
            if (part=="") {
                msg+="请选择【PGI_零件号】<br />";
            }
            if (domain=="") {
                msg+="请选择【申请工厂】<br />";
            }else if(domain!="100" && domain!="200"){
                msg+="【申请工厂】只能是100、200.<br />";                    
            }

            if(msg!=""){  
                layer.alert(msg);
                return false;
            }
            return true;
        }
        function con_sure(){
            if (gv.GetSelectedRowCount() <= 0) { layer.alert("请选择要删除的记录!"); return false; }
            //询问框
            return confirm('确认要删除吗？');
        }
    </script>

    <style>
        .lineread {
            /*font-size:9px;*/ 
            height: 25px;
            padding-left: 5px;
            border: none;
            border-bottom: 1px solid #ccc;
        }

        .linewrite {
            /*font-size:9px;*/ 
             height: 25px;
            padding-left: 5px;
            border: none;
            border-bottom: 1px solid #ccc;
            background-color: #FDF7D9 !important; /*EFEFEF*/
        }
        .dxeButtonDisabled {
            display: none;
        }
        .i_hidden{
            display:none;
        }
         .i_show{
            display:inline-block;
        } 
         .dxeTextBox_read{
            border:none !important ;
        }  

         .dxeTextBox_form_table_read{
            border:none !important ;border-bottom: 1px solid #ccc !important;background-color:#ffffff !important;
        } 
         .dxeTextBox_form_input_read{
            border:none !important ;background-color:#ffffff !important;
        }

         .dxeTextBox_form_table_write{
            border:none !important ;border-bottom: 1px solid #ccc !important;background-color:#FDF7D9 !important;
        } 
          .dxeTextBox_form_input_write{
            border:none !important ;background-color:#FDF7D9 !important;
        }
    </style>
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
                <input id="btntaskEnd" type="button" value="终止" onclick="parent.taskEnd(true);" class="btn btn-default btn-xs btntaskEnd" />
                <input id="btnshowProcess" type="button" value="查看流程" onclick="parent.showProcess(true);" class="btn btn-default btn-xs btnshowProcess" />
            </div>
        </div>
    </div>
    
    <div class="col-md-12" >

        <div class="row row-container">
            <div class="panel panel-infos">
                <div class="panel-headings" data-toggle="collapse" data-target="#DQXX">
                    <strong>申请基础信息</strong>
                </div>
                <div class="panel-body" id="DQXX">
                    <div class="col-xs-12 col-sm-12  col-md-12 col-lg-12" style="width:1200px;">
                        <table style="width: 100%; font-size: 12px;" border="0">
                            <tr>
                                <td style="width:100px;">申请单号</td>
                                <td><asp:TextBox ID="FormNo" runat="server" class="lineread"  ReadOnly="true" placeholder="自动产生" Width="210px" ToolTip="1|0" /></td>
                                <td style="width:100px;"><font color="red">&nbsp;</font>申请日期</td>
                                <td><asp:TextBox ID="ApplyDate"  runat="server" class="lineread" ReadOnly="True" Width="210px" /></td>
                                <td style="width:130px;"><font color="red">&nbsp;</font>填单人</td>
                                <td>
                                    <div class="form-inline">
                                        <asp:TextBox runat="server" ID="CreateId" class="lineread" ReadOnly="True" Width="60px"></asp:TextBox>
                                        <asp:TextBox runat="server" ID="CreateName"  class="lineread" ReadOnly="True" Width="148px"></asp:TextBox>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td><font color="red">*</font>申请人</td>
                                <td>
                                    <div class="form-inline">
                                        <asp:TextBox runat="server" ID="ApplyId" class="lineread" ReadOnly="True" Width="60px"></asp:TextBox>
                                        <asp:TextBox runat="server" ID="ApplyName"  class="lineread" ReadOnly="True" Width="140px"></asp:TextBox>
                                        <i id="ApplyId_i" class="fa fa-search <% =ViewState["ApplyId_i"].ToString() == "Y" ? "i_hidden" : "i_show" %>" onclick="Get_ApplyId()"></i>
                                    </div>
                                </td>
                                <td><font color="red">&nbsp;</font>申请人部门</td>
                                <td>
                                    <asp:TextBox runat="server" ID="ApplyDeptName"  class="lineread" ReadOnly="True" Width="210px"></asp:TextBox>
                                </td>
                                <td><font color="red">&nbsp;</font>电话(分机)</td>
                                <td>
                                    <asp:TextBox runat="server" ID="ApplyTelephone" class="lineread" Width="210px"/>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
        </div>

        <div class="row row-container">
            <div class="panel panel-infos">
                <div class="panel-headings" data-toggle="collapse" data-target="#wlXX">
                    <strong>客户物料信息</strong>
                </div>
                <div class="panel-body" id="wlXX">
                    <div class="col-xs-12 col-sm-12  col-md-12 col-lg-12" style="width:1200px;">
                        <table style="width: 100%; font-size: 12px;" border="0" >
                             <tr>
                                <td style="width:100px;"><font color="red">*</font>PGI_零件号</td>
                                <td style="width:292px;">
                                    <div class="form-inline">
                                        <asp:TextBox ID="part" runat="server" class="linewrite" Width="200px" />
                                        <i id="part_i" class="fa fa-search <% =ViewState["ApplyId_i"].ToString() == "Y" ? "i_hidden" : "i_show" %>" 
                                            onclick="Get_part()"></i>
                                    </div>
                                </td>
                                <td style="width:100px;"><font color="red">*</font>申请工厂</td>
                                <td style="width:292px;">
                                    <asp:TextBox ID="domain"  runat="server" class="linewrite" Width="210px" />
                                    <input type="text" runat="server" hidden="hidden" id="hd_domain" />
                                </td> 
                                <td style="width:100px;"><font color="red">*</font>申请类型</td>
                                <td style="width:292px;">
                                    <asp:TextBox ID="typeno"  runat="server" class="lineread" ReadOnly="true" Width="210px" />
                                </td>                   
                            </tr>
                            <tr>                                
                                <td style="width:100px;"><font color="red">*</font>客户物料号</td>
                                <td style="width:292px;">
                                    <asp:TextBox ID="cust_part"  runat="server" class="linewrite" Width="210px" />
                                </td>   
                                <td style="width:130px;"><font color="red">*</font>显示客户物料号</td>
                                <td>
                                    <asp:TextBox ID="cust_partd"  runat="server" class="lineread" ReadOnly="true" Width="210px" />
                                </td>      
                                <td style="width:100px;"></td>
                                <td style="width:292px;">
                                </td>                   
                            </tr>
                            <tr> 
                                <td style="width:100px;"><font color="red">&nbsp;</font>说明</td>
                                <td colspan="3">
                                    <asp:TextBox ID="comment"  runat="server" class="linewrite" Width="628px" />
                                </td>                            
                                <td style="width:130px;"><font color="red">&nbsp;</font></td>
                                <td>
                                    
                                </td>                    
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
        </div>

        <div class="row row-container">
            <div class="panel panel-infos">
                <div class="panel-headings" data-toggle="collapse" data-target="#bzclXX">
                    <strong>客户日程信息</strong>
                </div>
                <div class="panel-body" id="bzclXX">
                    <div class="col-xs-12 col-sm-12  col-md-12 col-lg-12" style="width:1200px;">
                        <div style="padding: 2px 5px 5px 0px">                
                            
                        </div>
                        <asp:UpdatePanel runat="server" ID="p1" UpdateMode="Conditional" >
                            <ContentTemplate>
                                
                                <asp:Button ID="btnadd" runat="server" Text="新增" class="btn btn-default btn-sm"  OnClick="btnadd_Click" OnClientClick="return Add_check()"/>
                                <asp:Button ID="btndel" runat="server" Text="删除" class="btn btn-default btn-sm"  OnClick="btndel_Click" OnClientClick="return con_sure()" />

                                 <dx:aspxgridview ID="gv" runat="server" AutoGenerateColumns="False" KeyFieldName="numid" Theme="MetropolisBlue" 
                                     ClientInstanceName="gv"  EnableTheming="True" OnDataBound="gv_DataBound">
                                    <ClientSideEvents SelectionChanged="gv_SelectionChanged" />
                                    <SettingsPager PageSize="1000"></SettingsPager>
                                    <Settings ShowFooter="True" />
                                    <SettingsBehavior AllowSelectByRowClick="false" AllowDragDrop="False" AllowSort="False" />
                                    <Columns>
                                        <dx:GridViewCommandColumn SelectAllCheckboxMode="Page" ShowClearFilterButton="true" ShowSelectCheckbox="true" Name="Sel" Width="30" VisibleIndex="0"></dx:GridViewCommandColumn>
                                        <dx:GridViewDataTextColumn  Caption="#" FieldName="numid" Width="30px" VisibleIndex="0"></dx:GridViewDataTextColumn>  
                                        <dx:GridViewDataTextColumn Caption="发货方式" FieldName="delivery_mode" Width="80px" VisibleIndex="1">
                                            <Settings AllowCellMerge="False" />
                                            <DataItemTemplate>
                                                <dx:ASPxComboBox ID="delivery_mode" runat="server" ValueType="System.String"
                                                    Width="80px" ClientInstanceName='<%# "delivery_mode"+Container.VisibleIndex.ToString() %>'
                                                    ClientSideEvents-SelectedIndexChanged='<%# "function(s,e){Ondelivery_modeChanged(s,"+Container.VisibleIndex+");}" %>'
                                                    Border-BorderStyle="None" BorderBottom-BorderStyle="Solid" ButtonStyle-BorderBottom-BorderColor="#ccc" BackColor="#FDF7D9"
                                                    DisabledStyle-BackColor="Transparent" DisabledStyle-BorderBottom-BorderStyle="None">
                                                </dx:ASPxComboBox>    
                                            </DataItemTemplate>
                                        </dx:GridViewDataTextColumn>                                         
                                        <dx:GridViewDataTextColumn Caption="发货自" FieldName="site" Width="80px" VisibleIndex="1">
                                            <Settings AllowCellMerge="False"/>
                                            <DataItemTemplate>
                                                <dx:ASPxComboBox ID="site" runat="server" ValueType="System.String" OnCallback="site_Callback"
                                                    Width="80px" ClientInstanceName='<%# "site"+Container.VisibleIndex.ToString() %>' 
                                                    ClientSideEvents-SelectedIndexChanged='<%# "function(s,e){OnSiteChanged(s,"+Container.VisibleIndex+");}" %>'
                                                    Border-BorderStyle="None" BorderBottom-BorderStyle="Solid" ButtonStyle-BorderBottom-BorderColor="#ccc" BackColor="#FDF7D9"
                                                    DisabledStyle-BackColor="Transparent" DisabledStyle-BorderBottom-BorderStyle="None">
                                                </dx:ASPxComboBox>
                                            </DataItemTemplate>        
                                        </dx:GridViewDataTextColumn>  
                                        <dx:GridViewDataTextColumn Caption="发货至" FieldName="ship" Width="80px" VisibleIndex="2">
                                            <Settings AllowCellMerge="False" />
                                            <DataItemTemplate>
                                                <dx:ASPxComboBox ID="ship" runat="server" ValueType="System.String" OnCallback="ship_Callback" 
                                                    Width="80px" ClientInstanceName='<%# "ship"+Container.VisibleIndex.ToString() %>'
                                                    ClientSideEvents-SelectedIndexChanged='<%# "function(s,e){OnShipChanged(s,"+Container.VisibleIndex+");}" %>'
                                                    Border-BorderStyle="None" BorderBottom-BorderStyle="Solid" ButtonStyle-BorderBottom-BorderColor="#ccc" BackColor="#FDF7D9"
                                                    DisabledStyle-BackColor="Transparent" DisabledStyle-BorderBottom-BorderStyle="None"
                                                    DropDownStyle="DropDown">
                                                </dx:ASPxComboBox>    
                                            </DataItemTemplate>
                                        </dx:GridViewDataTextColumn>                                           
                                        <dx:GridViewDataTextColumn Caption="发货至名称" FieldName="shipname" Width="150px" VisibleIndex="2"> 
                                            <Settings AllowCellMerge="False"/>
                                            <DataItemTemplate>
                                                <dx:ASPxTextBox ID="shipname" Width="150px" runat="server" Value='<%# Eval("shipname")%>' 
                                                    ClientInstanceName='<%# "shipname"+Container.VisibleIndex.ToString() %>'>
                                                </dx:ASPxTextBox>
                                            </DataItemTemplate>        
                                        </dx:GridViewDataTextColumn>                                    
                                        <dx:GridViewDataTextColumn Caption="销售订单" FieldName="nbr" Width="80px" VisibleIndex="3">
                                            <Settings AllowCellMerge="False"/>
                                            <DataItemTemplate>
                                                <dx:ASPxTextBox ID="nbr" Width="80px" runat="server" Value='<%# Eval("nbr")%>' 
                                                    ClientInstanceName='<%# "nbr"+Container.VisibleIndex.ToString() %>'> 
                                                </dx:ASPxTextBox>
                                            </DataItemTemplate>        
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="票据开往" FieldName="bill" Width="60px" VisibleIndex="4">
                                            <Settings AllowCellMerge="False"/>
                                            <DataItemTemplate>
                                                <dx:ASPxTextBox ID="bill" Width="60px" runat="server" Value='<%# Eval("bill")%>' 
                                                    ClientInstanceName='<%# "bill"+Container.VisibleIndex.ToString() %>' Border-BorderWidth="0" ReadOnly="true">
                                                </dx:ASPxTextBox>
                                            </DataItemTemplate>        
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="货币" FieldName="curr" Width="50px" VisibleIndex="5">
                                            <Settings AllowCellMerge="False"/>
                                            <DataItemTemplate>
                                                <%--<dx:ASPxTextBox ID="curr" Width="50px" runat="server" Value='<%# Eval("curr")%>' 
                                                    ClientInstanceName='<%# "curr"+Container.VisibleIndex.ToString() %>' Border-BorderWidth="0" ReadOnly="true">
                                                </dx:ASPxTextBox>--%>
                                                <dx:ASPxComboBox ID="curr" runat="server" ValueType="System.String"
                                                    Width="50px" ClientInstanceName='<%# "curr"+Container.VisibleIndex.ToString() %>'
                                                    Border-BorderStyle="None" BorderBottom-BorderStyle="Solid" ButtonStyle-BorderBottom-BorderColor="#ccc" BackColor="#FDF7D9"
                                                    DisabledStyle-BackColor="Transparent" DisabledStyle-BorderBottom-BorderStyle="None">
                                                </dx:ASPxComboBox>    
                                            </DataItemTemplate>        
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="价目表" FieldName="pr_list" Width="60px" VisibleIndex="6">
                                            <Settings AllowCellMerge="False"/>
                                            <DataItemTemplate>
                                                <dx:ASPxTextBox ID="pr_list" Width="60px" runat="server" Value='<%# Eval("pr_list")%>' 
                                                    ClientInstanceName='<%# "pr_list"+Container.VisibleIndex.ToString() %>'>
                                                </dx:ASPxTextBox>
                                            </DataItemTemplate>        
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="应纳税" FieldName="taxable" Width="50px" VisibleIndex="7">
                                            <Settings AllowCellMerge="False" />
                                            <DataItemTemplate>
                                                <dx:ASPxComboBox ID="taxable" runat="server" ValueType="System.String"
                                                    Width="50px" ClientInstanceName='<%# "taxable"+Container.VisibleIndex.ToString() %>'
                                                    Border-BorderStyle="None" BorderBottom-BorderStyle="Solid" ButtonStyle-BorderBottom-BorderColor="#ccc" BackColor="#FDF7D9"
                                                    DisabledStyle-BackColor="Transparent" DisabledStyle-BorderBottom-BorderStyle="None">
                                                </dx:ASPxComboBox>    
                                            </DataItemTemplate>
                                        </dx:GridViewDataTextColumn> 
                                        <dx:GridViewDataTextColumn Caption="税率" FieldName="taxc" Width="50px" VisibleIndex="8">
                                            <Settings AllowCellMerge="False" />
                                            <DataItemTemplate>
                                                <dx:ASPxComboBox ID="taxc" runat="server" ValueType="System.String"
                                                    Width="50px" ClientInstanceName='<%# "taxc"+Container.VisibleIndex.ToString() %>'
                                                    Border-BorderStyle="None" BorderBottom-BorderStyle="Solid" ButtonStyle-BorderBottom-BorderColor="#ccc" BackColor="#FDF7D9"
                                                    DisabledStyle-BackColor="Transparent" DisabledStyle-BorderBottom-BorderStyle="None">
                                                </dx:ASPxComboBox>    
                                            </DataItemTemplate>
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="库位" FieldName="loc" Width="55px" VisibleIndex="9">
                                            <Settings AllowCellMerge="False" />
                                            <DataItemTemplate>
                                                <dx:ASPxComboBox ID="loc" runat="server" ValueType="System.String"
                                                    Width="55px" ClientInstanceName='<%# "loc"+Container.VisibleIndex.ToString() %>'
                                                    Border-BorderStyle="None" BorderBottom-BorderStyle="Solid" ButtonStyle-BorderBottom-BorderColor="#ccc" BackColor="#FDF7D9"
                                                    DisabledStyle-BackColor="Transparent" DisabledStyle-BorderBottom-BorderStyle="None">
                                                </dx:ASPxComboBox>    
                                            </DataItemTemplate>
                                        </dx:GridViewDataTextColumn>  
                                        <dx:GridViewDataTextColumn Caption="寄售" FieldName="consignment" Width="50px" VisibleIndex="10">
                                            <Settings AllowCellMerge="False" />
                                            <DataItemTemplate>
                                                <dx:ASPxComboBox ID="consignment" runat="server" ValueType="System.String"
                                                    Width="50px" ClientInstanceName='<%# "consignment"+Container.VisibleIndex.ToString() %>'
                                                    Border-BorderStyle="None" BorderBottom-BorderStyle="Solid" ButtonStyle-BorderBottom-BorderColor="#ccc" BackColor="#FDF7D9"
                                                    DisabledStyle-BackColor="Transparent" DisabledStyle-BorderBottom-BorderStyle="None">
                                                </dx:ASPxComboBox>    
                                            </DataItemTemplate>
                                        </dx:GridViewDataTextColumn> 
                                        <dx:GridViewDataTextColumn Caption="寄售地点" FieldName="consignment_loc" Width="60px" VisibleIndex="11">
                                            <Settings AllowCellMerge="False"/>
                                            <DataItemTemplate>  
                                                <dx:ASPxComboBox ID="consignment_loc" runat="server" ValueType="System.String"
                                                    Width="60px" ClientInstanceName='<%# "consignment_loc"+Container.VisibleIndex.ToString() %>'
                                                    Border-BorderStyle="None" BorderBottom-BorderStyle="Solid" ButtonStyle-BorderBottom-BorderColor="#ccc" BackColor="#FDF7D9"
                                                    DisabledStyle-BackColor="Transparent" DisabledStyle-BorderBottom-BorderStyle="None">
                                                </dx:ASPxComboBox>                                             
                                            </DataItemTemplate>        
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="模型年" FieldName="modelyr" Width="70px" VisibleIndex="12">
                                            <Settings AllowCellMerge="False" />
                                            <DataItemTemplate>
                                                <dx:ASPxComboBox ID="modelyr" runat="server" ValueType="System.String"
                                                    Width="70px" ClientInstanceName='<%# "modelyr"+Container.VisibleIndex.ToString() %>'
                                                    Border-BorderStyle="None" BorderBottom-BorderStyle="Solid" ButtonStyle-BorderBottom-BorderColor="#ccc" BackColor="#FDF7D9"
                                                    DisabledStyle-BackColor="Transparent" DisabledStyle-BorderBottom-BorderStyle="None">
                                                </dx:ASPxComboBox>    
                                            </DataItemTemplate>
                                        </dx:GridViewDataTextColumn> 
                                        <dx:GridViewDataTextColumn Caption="行" FieldName="line" Width="30px" VisibleIndex="13"> 
                                            <Settings AllowCellMerge="False"/>
                                            <DataItemTemplate>
                                                <dx:ASPxTextBox ID="line" Width="30px" runat="server" Value='<%# Eval("line")%>' 
                                                    ClientInstanceName='<%# "line"+Container.VisibleIndex.ToString() %>' Border-BorderWidth="0" ReadOnly="true">
                                                </dx:ASPxTextBox>
                                            </DataItemTemplate>        
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="应收款地点" FieldName="ysk_site" Width="70px" VisibleIndex="13"> 
                                            <Settings AllowCellMerge="False"/>
                                            <DataItemTemplate>
                                                <dx:ASPxTextBox ID="ysk_site" Width="70px" runat="server" Value='<%# Eval("ysk_site")%>' 
                                                    ClientInstanceName='<%# "ysk_site"+Container.VisibleIndex.ToString() %>' Border-BorderWidth="0" ReadOnly="true">
                                                </dx:ASPxTextBox>
                                            </DataItemTemplate>        
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="日记账集" FieldName="rjzj" Width="50px" VisibleIndex="14"> 
                                            <Settings AllowCellMerge="False"/>
                                            <DataItemTemplate>
                                                <dx:ASPxTextBox ID="rjzj" Width="50px" runat="server" Value='<%# Eval("rjzj")%>' 
                                                    ClientInstanceName='<%# "rjzj"+Container.VisibleIndex.ToString() %>' Border-BorderWidth="0" ReadOnly="true">
                                                </dx:ASPxTextBox>
                                            </DataItemTemplate>        
                                        </dx:GridViewDataTextColumn>
                                        <%--<dx:GridViewDataTextColumn Caption="离岸价格" FieldName="lajg" Width="50px" VisibleIndex="15">
                                            <Settings AllowCellMerge="False"/>
                                            <DataItemTemplate>
                                                <dx:ASPxTextBox ID="lajg" Width="50px" runat="server" Value='<%# Eval("lajg")%>' 
                                                    ClientInstanceName='<%# "lajg"+Container.VisibleIndex.ToString() %>' Border-BorderWidth="0" ReadOnly="true">
                                                </dx:ASPxTextBox>
                                            </DataItemTemplate>        
                                        </dx:GridViewDataTextColumn>--%>
                                        <dx:GridViewDataTextColumn Caption="渠道" FieldName="channel" Width="40px" VisibleIndex="16"> 
                                            <Settings AllowCellMerge="False"/>
                                            <DataItemTemplate>
                                                <dx:ASPxTextBox ID="channel" Width="40px" runat="server" Value='<%# Eval("channel")%>' 
                                                    ClientInstanceName='<%# "channel"+Container.VisibleIndex.ToString() %>' Border-BorderWidth="0" ReadOnly="true">
                                                </dx:ASPxTextBox>
                                            </DataItemTemplate>        
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="备注" FieldName="remark" Width="130px" VisibleIndex="17"> 
                                            <Settings AllowCellMerge="False"/>
                                            <DataItemTemplate>
                                                <dx:ASPxTextBox ID="remark" Width="130px" runat="server" Value='<%# Eval("remark")%>' 
                                                    ClientInstanceName='<%# "remark"+Container.VisibleIndex.ToString() %>'>
                                                </dx:ASPxTextBox>
                                            </DataItemTemplate>        
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="id" Width="0px">
                                            <HeaderStyle CssClass="hidden" />
                                            <CellStyle CssClass="hidden"></CellStyle>
                                            <FooterCellStyle CssClass="hidden"></FooterCellStyle>
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="CSNo" Width="0px">
                                            <HeaderStyle CssClass="hidden" />
                                            <CellStyle CssClass="hidden"></CellStyle>
                                            <FooterCellStyle CssClass="hidden"></FooterCellStyle>
                                        </dx:GridViewDataTextColumn>
                                    </Columns>       
                                    <TotalSummary>
                                        <dx:ASPxSummaryItem DisplayFormat="合计{0:N0}" FieldName="bm" SummaryType="Sum" />
                                        <dx:ASPxSummaryItem DisplayFormat="{0:N4}" FieldName="zz" SummaryType="Sum" />
                                        <dx:ASPxSummaryItem DisplayFormat="{0:N2}" FieldName="zj" SummaryType="Sum" />
                                    </TotalSummary>                                            
                                    <Styles> 
                                        <Header BackColor="#31708f" Font-Bold="True" ForeColor="white" Border-BorderStyle="None" HorizontalAlign="Left" VerticalAlign="Top"></Header>    
                                        <SelectedRow BackColor="#FDF7D9"></SelectedRow>      
                                        <Footer Font-Bold="true" ForeColor="Red" HorizontalAlign="Right"></Footer>
                                    </Styles>                                          
                                </dx:aspxgridview>

                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
        </div>

        <div class="row row-container <% =ViewState["ApplyId_i"].ToString() != "" ? "i_show" : "i_hidden" %>">
            <div class="panel panel-infos">
                <div class="panel-headings" data-toggle="collapse" data-target="#qrXX">
                    <strong>QAD确认完成</strong>
                </div>
                <div class="panel-body" id="qrXX">
                    <div class="col-xs-12 col-sm-12  col-md-12 col-lg-12" style="width:1200px;">
                        <table style="width: 80%; height: 100px" border="0" id="Table1">
                            <tr>
                                <td style="width: 150px; color: Red;">
                                    QAD存在，会默认选中 :
                                </td>
                                <td>
                                    <asp:CheckBoxList ID="ddlopinion" runat="server" CssClass="form-control" RepeatDirection="Vertical" Width="200px" Height="130px">
                                        <asp:ListItem Text="PGI_零件号" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="发货至" Value="2"></asp:ListItem>
                                        <asp:ListItem Text="价目表" Value="3"></asp:ListItem>
                                        <asp:ListItem Text="预测量" Value="4"></asp:ListItem>
                                    </asp:CheckBoxList>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
        </div>
        
        <div class="row row-container">
            <div class="panel panel-infos">
                <div class="panel-headings" data-toggle="collapse" data-target="#fjXX">
                    <strong>附件信息</strong>
                </div>
                <div class="panel-body" id="fjXX">
                    <div class="col-xs-12 col-sm-12  col-md-12 col-lg-12" style="width:1200px;">
                        <dx:ASPxUploadControl ID="uploadcontrol" runat="server" Width="500px" BrowseButton-Text="浏览" Visible="true" ClientInstanceName="UploadControl"
                            ShowAddRemoveButtons="True" RemoveButton-Text="删除" UploadMode="Advanced" AutoStartUpload="true" ShowUploadButton="false" ShowProgressPanel="true"
                            OnFileUploadComplete="uploadcontrol_FileUploadComplete">
                            <AdvancedModeSettings EnableDragAndDrop="True" EnableFileList="True" EnableMultiSelect="True">
                            </AdvancedModeSettings>
                            <ClientSideEvents FileUploadComplete="onFileUploadComplete" />
                        </dx:ASPxUploadControl>                       
                        <input type="hidden" id="ip_filelist" name="ip_filelist" runat="server" />
                        <table id="tbl_filelist" width="500px">
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
            </div>
        </div>

        <div class="row row-container">
            <div class="panel panel-infos">
                <div class="panel-headings" data-toggle="collapse" data-target="#CZRZ"> 
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

