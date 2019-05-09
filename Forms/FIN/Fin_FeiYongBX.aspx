<%@ Page Title="费用报销申请单" Language="C#" AutoEventWireup="true" CodeFile="Fin_FeiYongBX.aspx.cs" Inherits="Fin_FeiYongBX" MaintainScrollPositionOnPostback="True" ValidateRequest="true" %>


<%@ Register Assembly="DevExpress.Web.v17.2, Version=17.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script src="../../../Content/js/jquery.min.js"></script>
    <script src="../../../Content/js/bootstrap.min.js"></script>
    <script src="../../Content/js/layer/layer.js"></script>
    <script src="../../Content/js/plugins/layer/laydate/laydate.js"></script>
    <script src="../../Content/js/plugins/validate/jquery.validate.min.js"></script>
    <script src="../../Content/js/plugins/validate/messages_zh.min.js"></script>
    <link href="../../Content/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../../../Content/js/plugins/bootstrap-select/css/bootstrap-select.min.css" rel="stylesheet" />
    <link href="../../Content/css/custom.css" rel="stylesheet" />
   
    <script src="../../../Content/js/plugins/bootstrap-select/js/bootstrap-select.min.js"></script>
    <%--<script src="../Content/js/plugins/layer/laydate/laydate.js"></script>--%>
    <script src="../../Scripts/RFlow.js"></script>
    <script type="text/javascript">
        var paramMap;
        $(document).ready(function () {
            $("#mestitle").html("【费用报销申请单】<a href='/userguide/FeiYongBXGuide.pptx' target='_blank' class='h5' style='color:red'>使用说明</a>");// 
             
            SetButtons();
            
            ////申请部门
            //$("select[id*='applydept']").change(function(){
            //    var domain=$("#domain").val();
            //    var dept=$("#applydept").val();
            //    getDeptLeader(domain,dept);              
            //})
                       
            //获取参数名值对集合Json格式
            var url =window.parent.document.URL;
            var viewmode="";
            paramMap = getURLParams(url); 
            if(paramMap.mode!=NaN&&paramMap.mode!=""&&paramMap.mode!=undefined)
            {
                viewmode=paramMap.mode; 
                $("#btnAddDetl").css("display","none");
                $("#btnDelete").css("display","none"); 
           
                DisableButtons();//禁用流程按钮
                                
            }       
            //初始化主管
            if(paramMap.instanceid==undefined){
                getDeptLeaderbyEmp();  
            }
            if($("#costcenter").val()==""||$("#deptcode").val()==""){
                getEmpCost($("#aplid").val());
            }
            //初始化明细总价 
            getTotalMoney();
            //取工厂名称
            setDomainName();
            
            //打印
            if(paramMap.display==1){
                $("#btnPrint").css("display","");  
            }
            //初始出纳
            setChuNa($("#apldomain").val());

        })// end ready

        //设定财务处理人员
        function setChuNa(domain){
            // select 'u_'+cast(id as varchar(100)) users from RoadFlowWebForm.dbo.users  where account='01714'
            if(domain=="200"){
                $("#chuna").val("u_69A9E98B-4F5D-40D6-BFFF-B792DC6E7355")  //02168季毅红
            }else{
                $("#chuna").val("u_8E182582-0F1B-4937-9E79-AB53479B2142") //01714王会敏
            }
        }
        //判断 手机费，汽车费油费是否区间重复申请
        function hasRepeat(objId,columnIndex){
            // debugger
            var arr = [];
            $("#"+objId+" tbody tr:not(:eq(0))").each(function(index,element){//!eq(0)去除标题
                //  debugger
                var costcate,feedate;
                
                costcate=$(element).find("input[id*=costcateid]").val()
                feedate=$(element).find("input[id*=feedate]").val()
                if(costcate=="P001"||costcate=="P002"){
                    arr.push( costcate+feedate );
                }

            });
            var len=arr.length;
            if( len==$.unique( arr ).length ){
                return false;
            }else{
                return true;
            }
        }
         

        //设定表字段状态（可编辑性）       
        function SetControlStatus(fieldStatus,tabName)
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
                    if(  ( paramMap.display==1||statu.indexOf("1_")!="-1") && (ctype=="text"||ctype=="textarea") ){
                        $("#"+id).attr("readonly","readonly").removeAttr("onclick").removeAttr("ondblclick").removeAttr("onfocus").attr("data-toggle","");
                    }
                    else if( ( paramMap.display==1||statu.indexOf("1_")!="-1") && ( ctype=="checkbox"||ctype=="radio"||ctype=="select"||ctype=="file" ) ){
                        $("#"+id).attr("disabled","disabled");                 
                    }

                    if( statu.indexOf("1_")!="-1" ||  paramMap.display==1){
                        //如果是显示模式或只读模式
                        
                        //设定 编辑性
                        $('table[id=grid] th:last').hide();
                        $('table[id=grid] tbody tr').each(function(index,item)
                        {
                            $(item).find("td:last").hide();
                        })

                        $("#blockinfo").addClass("hidden");//签核中隐藏查看未报销项目
                        $("#blockhint").addClass("hidden");//签核中隐藏红色提醒项目
                        $("#uploadcontrol").addClass("hidden");//签核中隐藏文件上传 
                    }
                    
                }
            }
            

        }
        // table中每一行设定可编辑性  
        function SetControlStatus2(fieldStatus,tabName2)
        {  // tabName_columnName:1_0
            var flag=true;
            for(var item in fieldStatus){
                var id=""+item.replace(tabName2.toLowerCase()+"_","");
                var objs=$("table[id=grid]").find("input[id*="+id+"]");
                $.each(objs, function (i, obj) {                
                    
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
                    if( ( paramMap.display==1||statu.indexOf("1_")!="-1") && (ctype=="text"||ctype=="textarea") ){
                        $(obj).attr("readonly","readonly");
                        $(obj).removeAttr("onclick").removeAttr("ondblclick").removeAttr("onfocus").attr("data-toggle","");
                        $(obj).css("border","none").css("background","transparent");
                    }
                    else if( ( paramMap.display==1||statu.indexOf("1_")!="-1") && ( ctype=="checkbox"||ctype=="radio"||ctype=="select"||ctype=="file"  ) ){
                        $(obj).attr("disabled","disabled");
                    }
                    else if(statu.indexOf("1_")!="-1" && ( ctype=="input" ) ){
                        $(obj).attr("type","hidden");
                    }

                });

            }
        }

        function validInputData(){
            // 在键盘按下并释放及提交后验证提交表单
            $("#").validate({
                rules: {                                        
                    amount: {
                        required: true,                        
                        number:true
                    },     
                },
                messages: {                     
                    amount: {
                        required: "请输入数字"	                         
                    } 
                }
            });
        }

    </script>
 <style>
        hidden {
            display: none;
        }

        .input {
            border-left: none;
            border-right: none;
            border-top: none;
            height: 25px;
            padding-left: 5px;
            border-radius:0px;
        }

        .input-edit {
            border-bottom: 1px solid gray;border-radius:0px
        }

        .input-readonly {
            border-bottom: 1px solid lightgray;border-radius:0px
        }

        .select {
            border-left: none;
            border-right: none;
            border-top: none;
            border-bottom: 1px solid gray;
            height: 25px;
        }

        .name {
            width: 80px;
        }

        .bordernone {
            border: none;
            font-size: 11pt;
        }

        .alpha100 {
            background: rgba(0, 0, 0, 0);
        }

        .width100 {
            width: 100px;
        }

        .width50 {
            width: 50px;
        }

        table[id=grid] tbody tr td input[type=text] {
            border: 1px solid gray;
            height: 25px;
            font-size: 11pt;
            background-color: #FDF7D9;
        }
        table[id=tab1] tbody tr td {font-size:11pt}

        table[id=grid] tbody tr {
            height: 30px;
        }

        .th_right {
            text-align: right;
            margin-right: 5px;
            padding-right: 10px;
        }
        .panel-info {
            border-color: none; 
        }
        .panel-infos {
            color: #31708f; 
            background-color:none;
            /*border-color: #bce8f1;*/ 
            border-bottom:0px solid white;
            -webkit-box-shadow: 0 1px 1px rgba(0,0,0,.00);
        }
        .panel-headings{
            background-color:none;
            border-bottom:1px solid #777
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
            SetControlStatus(<%=fieldStatus%>,"fin_feiyongbx_main_form");
            SetControlStatus2(<%=fieldStatus%>,"fin_feiyongbx_dtl_form");  
                            
        });

        //验证
        function validate(){             
            if($("#deptm").val()==""){
                layer.alert("部门主管未获取到，请尝试重新打开申请单。请联系IT设定.");
                return false;
            }
            if($("#deptmfg").val()==""){
                layer.alert("分管副总未获取到，请尝试重新打开申请单。请联系IT设定.");
                return false;
            }
            if($("#chuna").val()==""){
                layer.alert("财务处理人员未获取到，请尝试关闭此单重新打开。或请IT协助确认.");
                return false;
            }
            <%=ValidScript%>
            var flag=true;
            var msg="";
            //validate 不可无明细  
            debugger;
            if( $("input[id*=grid_costcateid]").length ==0){
                msg="请添加报销费用项目.";
                layer.alert(msg);
                return false;
            }
            else{

            }
            //validate 费用代码
            var rows=0; //有效行标识
            
            $.each($("input[id*=grid_costcateid]"), function (i,obj){
                var id=$(obj).attr("id");
                debugger;
                
                if( $(obj).val()==""){
                    //isnullflag=false;
                    //msg+="【类别及费用项目】";
                    //$(obj).css("background-color","orange");
                    //flag=false;
                    //return false;
                }  
                else{
                    rows=rows+1;
                    var _instanceid= $("#"+id.replace("costcateid","instanceid"));
                    var _feedate= $("#"+id.replace("costcateid","feedate"));
                    var _feenote= $("#"+id.replace("costcateid","feenote"));
                    var _amount= $("#"+id.replace("costcateid","amount"));
                    if( $(_instanceid).val()=="-"||$(_instanceid).val()==""||$(_instanceid).val()=="."){
                        msg+="【预算来源】"; 
                        flag=false; 
                        $(_instanceid).css("background-color","orange")                       
                    }
                    
                    if( $(_feedate).val()==""){
                        msg+="【费用发生日期/区间】";                  
                        flag=false;
                        $(_feedate).css("background-color","orange")                        
                    }
                    if( $(_feenote).val()==""){
                        msg+="【费用说明】";                                     
                        flag=false;     
                        $(_feenote).css("background-color","orange")
                    } 
                    if( $(_amount).val()==""){
                        msg+="【报销金额】";                                     
                        flag=false;
                        $(_amount).css("background-color","orange")                  
                    } 
                    if(msg!=""){
                        //$($(obj)[0].parentNode.parentNode).css("background-color","red");
                        layer.alert(msg+"不可为空");
                        return false
                    }
                   
                }

                
            })      

            if(rows==0){
                layer.alert(" 报销明细行未填写 或 添加报销类别和费用项目。");
                return false
            }
            
            if(msg!=""){layer.alert(msg+"不可为空");return false}
            //P001,P002申请是否重复验证
            if(hasRepeat("grid",1)==1){   
                layer.alert("【手机费】或【员工车辆费用】费用发生区间不可重复，请检查修正.");
                return false;
            } 
            //valid 处理意见
            if($("#comment").val()==""){
                msg="【处理意见】请填写";             
                flag=false;
                layer.alert(msg);
                return false;                
            }
              
            if($("#upload").val()==""&& $("#link_upload").text()==""){
                layer.alert("请选择【附件】");
                return false;
            }           

            
        }

    

       
             
    
    </script>
    <%-- funcitons--%>
    <script type="text/javascript">
        function setvalue(ctrl0, keyValue0, ctrl1, keyValue1, ctrl2, keyValue2) {

            $("input[id*='" + ctrl0 + "']").val(keyValue0);
            $("input[id*='" + ctrl1 + "']").val(keyValue1);
            $("input[id*='" + ctrl2 + "']").val(keyValue2);
            popupwindow.close();
            $("input[id*='" + ctrl0 + "']").change();
        }
        
        //选择人员
        function selectemp(){  
            var ctrl0="aplid";
            var ctrl1="aplname";
            var ctrl2="apldept";
            var ctrl3="apljob";
            var ctrl4="apldomain";
            // window.open( '/forms/open/select.aspx?windowid=bom&ctrl0='+ctrl0+'&ctrl1='+ctrl1+'&ctrl2='+ctrl2);
            layer.open({
                shade: [0.5, '#000', false],
                type: 2,
                offset: '100px',
                area: ['600px', '450px'],
                fix: false, //不固定
                maxmin: false,
                title: ['<i class="fa fa-dedent"></i> 请选择申请人', false],
                closeBtn: 1,
                content: '/forms/open/select.aspx?windowid=emp&changekey='+ctrl0+'&ctrl0='+ctrl0+'&ctrl1='+ctrl1+'&ctrl2='+ctrl2+"&ctrl3="+ctrl3+"&ctrl4="+ctrl4 ,
                end: function(e) {
                    setDomainName();//取工厂名称
                }
            });

            
        }

        //选择费用项目
        function selCostCate(obj){               
            var clickid=$(obj).attr("id");  
            var arr=$(obj).attr("id").split("_");//获取选中目标ctrl的id
            // var _costcateid=$(obj).attr("id").replace(arr[1],"costcateid");

            var ctrl0=clickid.replace(arr[1],"costcateid");
            var ctrl1=clickid.replace(arr[1],"costcatename");
            var ctrl2=clickid.replace(arr[1],"instanceid");
            var ctrl3="apldomain";      
            var _aplid=$("#aplid").val();
            var _domain=$("#apldomain").val();
            var _costcateid=$("#"+ctrl0).val();
            var _acptctrl=clickid.replace(arr[1],"feedate");
            var hiddenColoums="1";//隐藏列
            layer.open({
                shade: [0.5, '#000', false],
                type: 2,
                offset: '100px',
                area: ['600px', '450px'],
                fix: false, //不固定
                maxmin: false,
                title: ['<i class="fa fa-dedent"></i> 选择费用项目', false],
                closeBtn: 1,
                content: '/forms/open/select.aspx?windowid=fin_feiyongbx_form&hiddenColoums='+hiddenColoums+'&changekey='+ctrl0+'&ctrl0='+ctrl0+'&ctrl1='+ctrl1+'&ctrl2='+ctrl2+'&ctrl3='+ctrl3,
                end: function () {
                    if($(obj).val()!=""){
                        var _feedateid=clickid.replace(arr[1],"feedate");
                        var ul=$("#"+_feedateid)[0].nextElementSibling;
                        setdate(ul,_domain,_costcateid,_aplid,_acptctrl); 
                        //如果是出差申请单,私车公用申请，则改为空
                        if($("#"+ctrl2).val().indexOf("申请")>=0){
                            $("#"+ctrl2).val("").attr("placeholder","点此选择预算来源");
                        }
                    }

                }
            });
        }
        //选择预算来源
        function selBudgetSource(obj){       //obj 是 instanceid   
            var instanceValue=$(obj).val()==""?".":$(obj).val();

            var id=$(obj).attr("id"); 
            var _costcateid=$("#"+id.replace("instanceid","costcateid")).val();            
            if("部门预算，个人额度".indexOf(instanceValue) != -1|| _costcateid==""){
                return false
            }        
            var ctrl0=id.replace("instanceid","budgetsour"); 
            var ctrl1=id
            var ctrl2=""
            var ctrl3="";//apldomain
            var ctrlN=id.replace("instanceid","limit"); ;//额度控件id
            var ctrlN2=id.replace("instanceid","feenote"); ;//说明id
            var _domain=$("#apldomain").val();
            var flag=$("#"+ctrl0).val();
            var _aplid=$("#aplid").val();

            var aplno=$("#aplno").val();
            // var _acptctrl=id.replace("costcateid","feedate");
            //汇总私车公用单号
            var formno="";
            $.each($("input[id*=grid_costcateid]"), function (i,obj){
                if( $(obj).val()=="T009"){                                                       
                    var instanceCtrlid=$(obj).attr("id").replace("costcateid","instanceid");
                    formno=formno+$("#"+instanceCtrlid).val()+",";
                }               
            })              


            layer.open({
                shade: [0.5, '#000', false],
                type: 2,
                offset: '100px',
                area: ['900px', '500px'],
                fix: false, //不固定
                maxmin: false,
                title: ['<i class="fa fa-dedent"></i> 选择预算来源', false],
                closeBtn: 1,
                content: '/forms/fin/selectform.aspx?windowid=fin_feiyongbx_form&aplno='+aplno+'&formno='+formno+'&costcateid='+_costcateid+'&aplid='+_aplid+'&changekey='+ctrl0+'&ctrl0='+ctrl0+'&ctrl1='+ctrl1+'&ctrl2='+ctrl2+'&ctrl3='+ctrl3+'&ctrlN='+ctrlN+'&ctrlN2='+ctrlN2,
                end: function () {
                    if($(obj).val()!=""){  
                        var wfid=$("#"+ctrl0).val() ; //budgetsour val
                        if(wfid!=""&&flag!=$("#"+ctrl0).val())
                        {
                            var src="/Platform/WorkFlowRun/Default.aspx?flowid="+wfid+"&instanceid="+$(obj).val()+"&display=1";
                            $("#"+ctrl0).val(src);
                            var link=$("#"+ctrl0)[0].nextElementSibling;
                            $(link).attr("href",src);
                            //如果是差旅单，号临存txt
                            if(_costcateid=="T009"){
                                $("#txtFin_CA_No").val($(obj).val());
                                $("#btnCar").click();
                            }
                        }
                                            
                    }

                }
            });
        }
        
        //机票费，火车票如果人事预定则不可报销
        function validTicket(obj){
            var arr=$(obj).attr("id").split("_");//获取选中目标ctrl的id
            var _costcateid=$(obj).attr("id").replace(arr[1],"costcateid"); 
            var _instanceid=$(obj).attr("id").replace(arr[1],"instanceid");
            var costid=$("#"+_costcateid).val();
            var instanceid=$("#"+_instanceid).val();
            if("T001，T002".indexOf($("#"+_costcateid).val()) == -1||instanceid=="."||instanceid==""){
                return false
            }

            $.ajax({
                type: "Post",async: false,
                url: "Fin_FeiYongBX.aspx/ValidTicket" , 
                //方法传参的写法一定要对，str为形参的名字,str2为第二个形参的名字
                //P1:wlh P2： 
                data: "{'costcateid':'"+costid+"','aplno':'"+instanceid+"'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {//返回的数据用data.d获取内容//                                      
                    if (data.d == "1") {                       
                        layer.alert("预算单【"+instanceid+"】的["+costid+"]票务已由人事代为购买，不可再次报销."); 
                        $("#"+_costcateid).val("");
                        $("#"+_instanceid).val("");
                    }
                    else {                             
                    }                   
                },
                error: function (err) {
                    layer.alert(err);
                }
            });
            
        }
        //费用发生日期、区间选择
        function setFeeDate(obj){               
            var objid=$(obj).attr("id");  //feedateid           
            var costcateid=objid.replace("feedate","costcateid");       
            var _aplid=$("#aplid").val();
            var _domain=$("#apldomain").val();
            var _costcateid=$("#"+costcateid).val();
            var _acptctrl=objid;
            var ul=$("#"+objid)[0].nextElementSibling;
            setdate(ul,_domain,_costcateid,_aplid,_acptctrl);
        }
        //根据费用发生区间（针对H,Q）自动解析费用说明
        function setFeenote(obj){               
            var objid=$(obj).attr("id");  //feedateid 
            var arr=objid.split("_");//获取选中目标ctrl的id
            var _costcateid=objid.replace(arr[1],"costcateid");
            var _feedate=objid.replace(arr[1],"feedate");
            var feenoteid=objid.replace(arr[1],"feenote");   
            var showmsg="";            
            if("D001".indexOf( $("#"+_costcateid).val() )>=0){
                if($("#"+_feedate).val().indexOf("Q")>=0){
                    showmsg="L4(含L4)以上人员"+$(obj).val().replace("Q","-")+"季度团建费";                
                }
                else if($("#"+_feedate).val().indexOf("H")>=0){
                    showmsg="L5人员"+$(obj).val().replace("H1","上半").replace("H2","下半")+"年度团建费";
                }            
                $("#"+feenoteid).val(showmsg);        
            }else if("P001,P002".indexOf( $("#"+_costcateid).val() )>=0){// costcateid Change event
                var _costcatename=objid.replace(arr[1],"costcatename");
                showmsg=$("#"+_feedate).val()+$("#"+_costcatename).val()                          
                $("#"+feenoteid).val(showmsg);
            }
            
        }        
        //费用发生日期、区间选择
        function setdate(ul,_domain,_costcateid,_aplid,_acptctrl){
            $.ajax({
                type: "Post",async: false,
                url: "fin_feiyongbx.aspx/getFindate" , 
                //方法传参的写法一定要对，str为形参的名字,str2为第二个形参的名字
                //P1:wlh P2： 
                data: "{'domain':'"+_domain+"','costcateid':'"+_costcateid+"','aplid':'"+_aplid+"'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {//返回的数据用data.d获取内容// 
                    //  debugger;
                    var arr=eval(data.d);
                    if (arr == undefined) {                      
                       
                        $("#"+_acptctrl).removeAttr("data-toggle").attr("onclick","laydate()");
                    }
                    else {  
                        $("#"+_acptctrl).removeAttr("onclick").attr("data-toggle","dropdown");
                        //var arr=eval(data.d)
                        $(ul).find("li").remove();
                        for(var i = 0; i<arr.length; i++){
                            $(ul).append(" <li value='"+arr[i].dateT+"' onclick=\"$('#"+_acptctrl+"').val('"+arr[i].dateT+"');$('#"+_acptctrl+"').change();\"  style=\"background-color:wheat\" >"+arr[i].dateT+"</li>");                            
                        }
                    }                   
                },
                error: function (err) {
                    layer.alert(err);
                }
            });
            
        }
        //获取额度
        function setLimit(obj){
            var arr=$(obj).attr("id").split("_");
            var _costcateid=$("#"+$(obj).attr("id").replace(arr[1],"costcateid")).val();
            var _domain=$("#apldomain").val();           
            var _aplid=$("#aplid").val();             
            var _feedate=$("#"+$(obj).attr("id").replace(arr[1],"feedate")).val(); 
            if(_feedate!=""&&_costcateid!=""){
                $.ajax({
                    type: "Post",async: false,
                    url: "Fin_FeiYongBX.aspx/getLimit" , 
                    //方法传参的写法一定要对，str为形参的名字,str2为第二个形参的名字
                    //P1:wlh P2： 
                    data: "{'domain':'"+_domain+"','costcateid':'"+_costcateid+"','aplid':'"+_aplid+"','feedate':'"+_feedate+"','aplno':''}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (data) {//返回的数据用data.d获取内容// 
                        var strid=$(obj).attr("id").replace(arr[1],"limit");//获取额度clientId
                        var strbxje=$(obj).attr("id").replace(arr[1],"amount");//获取报销金额clientId                       
                        if (data.d == "") {                       
                            layer.msg("未获取到预算额度."); 
                            $("#"+strid).val("");
                            $("#"+strbxje).val("");
                        }
                        else {     

                            $("#"+strid).val(data.d);
                            $("#"+strbxje).val(data.d);
                            $("#"+strbxje).css("color","");//取消文字样式
                            getTotalMoney();//重新计算合计
                        }                   
                    },
                    error: function (err) {
                        layer.alert(err);
                    }
                });
            }
        }
        //清除行填资料
        function clearRowData(obj){
            var objid=$(obj).attr("id");  //feedateid 
            var arr=objid.split("_");//获取选中目标ctrl的id
            
            $("#"+objid.replace(arr[1],"feedate")).val("");
            $("#"+objid.replace(arr[1],"feenote")).val("");            
            $("#"+objid.replace(arr[1],"limit")).val("");
            $("#"+objid.replace(arr[1],"amount")).val("");        
        }
        //获取部门领导和分管副总
        function getDeptLeaderbyEmp(){
            var _emp=$("#aplid").val();
            var _domain=$("#apldomain").val();
            var _dept=$("#apldept").val();
            // 部门主管
            $.ajax({
                type: "Post",
                async: false,
                url: "Fin_FeiYongBX.aspx/getDeptLeaderByDept" , //getDeptLeaderByDept
                //方法传参的写法一定要对，str为形参的名字,str2为第二个形参的名字
                //P1:wlh P2： 
                data: "{'domain':'"+_domain+"','dept':'"+_emp+"'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {//返回的数据用data.d获取内容//                      
                    if (data.d == "") {
                        //debugger;
                        $("[id*=aplid]").val("");
                        layer.alert("未获取到申请人部门经理，请重试.");  
                            
                    }
                    else {                             
                        $("[id*=deptm]").val(data.d)
                    }                   
                },
                error: function (err) {
                    layer.alert(err);
                }
            });
            //分管副总
            $.ajax({
                type: "Post",async: false,
                url: "Fin_FeiYongBX.aspx/getChargeLeaderByUser" , 
                //方法传参的写法一定要对，str为形参的名字,str2为第二个形参的名字
                //P1:wlh P2： 
                data: "{'domain':'"+_domain+"','userid':'"+_dept+"'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {//返回的数据用data.d获取内容//                      
                    if (data.d == "") {
                        
                        $("[id*=aplid]").val("");
                        layer.alert("未获取到申请人分管领导.请关闭从开试试.");  
                            
                    }
                    else {     
                        //alert(data.d);
                        $("[id*=deptmfg]").val(data.d)
                    }                   
                },
                error: function (err) {
                    layer.alert(err);
                }
            });

        }
        //获取部门领导和分管副总
        function getEmpCost(empid){           
                         
            $.ajax({
                type: "Post",
                async: false,
                url: "fin_feiyongbx.aspx/getEmpCost" , 
                //方法传参的写法一定要对，str为形参的名字,str2为第二个形参的名字
                //P1:wlh P2： 
                data: "{'empid':'"+empid+"'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {//返回的数据用data.d获取内容// 
                    // debugger;
                    var arr=eval(data.d);
                    if (arr == undefined) {                      
                        layer.alert("未获取到申请人成本中心");
                    }
                    else {                         
                        //var arr=eval(data.d)
                        $("#costcenter").val(arr[0].costcenter);
                        $("#deptcode").val(arr[0].deptcode);

                    }                   
                },
                error: function (err) {
                    layer.alert(err);
                }
            });

        }
        //计算所有明细总价 
        function setBackColor(obj){
            var _amount=$(obj).val();
            var limit_id=$(obj).attr("id").replace("amount","limit");
            var _limit=$("#"+limit_id).val();
            if(_amount!="" && _limit!="" &&  parseFloat(_amount)>parseFloat(_limit)){
                $(obj).css("color","red");
            }else{
                $(obj).css("color","");
            }

        }
        function getTotalMoney(){                       
            var totalMoney=0;
            $("#grid").find("tr td input[id*=amount]").each(function (i) {
                var rowval=$("tr td input[id*=amount_"+i+"]").val().replace(",","");
                rowval= (rowval==""||rowval=="NaN")? 0 : rowval;                
                totalMoney=totalMoney+parseFloat(rowval)
                $("#totalfee").val(totalMoney);
                
            })
            //grid底部total值更新
            $('table[id*=grid] tr td span[id*=total]').each(function (i) {
                // if($.trim($(this).text())==""){//
                $(this).text("合计:"+fmoney(totalMoney,1));
                // }   
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
        //清除grid input 内容
        function clearDetail(){
            $("input[id*=grid_][type=text]").val("");
            $("table[id*=HZgrid]").css("display","none");
            $("table[id*=HZ_UnGrid]").css("display","none");
        }
        function setDomainName(){
            var domain=$("#apldomain").val();
            if(domain=="200"){
                $("#apldomainname").val("昆山工厂");
            }
            else if(domain=="100"){
                $("#apldomainname").val("上海工厂");
            }
        }
    </script>
    
    <script type="text/javascript">
        var uploadedFiles = [];
        function onFileUploadComplete(s, e) {
            if(e.callbackData) {
                var fileData = e.callbackData.split('|');uploadedFiles.push(fileData);$("#<%=ip_filelist.ClientID%>").val(uploadedFiles.join(";"));
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
            $("#<%=ip_filelist.ClientID%>").val(uploadedFiles.join(";"));
        }
        
    </script>

    <form id="form1" runat="server">

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
                            <asp:Button ID="btnSave" runat="server" Text="保存" CssClass="btn btn-default btn-xs btnSave" OnClientClick="" OnClick="btnSave_Click" ToolTip="临时保存此流程" UseSubmitBehavior="false" />
                            <asp:Button ID="btnflowSend" runat="server" Text="批准" CssClass="btn btn-default btn-xs btnflowSend" OnClientClick="if(validate()==false)return false;" OnClick="btnflowSend_Click" UseSubmitBehavior="false" />
                            <input id="btnflowCompleted" type="button" value="完成" onclick="parent.flowCompleted(true);" class="btn btn-default btn-xs btnflowCompleted" />
                            <input id="btnaddWrite" type="button" value="加签" onclick="parent.addWrite(true);" class="btn btn-default btn-xs btnaddWrite" />
                            <input id="btnflowBack" type="button" value="退回" onclick="parent.flowBack(true);" class="btn btn-default btn-xs btnflowBack" />

                            <input id="btnshowProcess" type="button" value="查看流程" onclick="parent.showProcess(true);" class="btn btn-default btn-xs btnshowProcess" />
                            <input id="btntaskEnd" type="button" value="终止流程" onclick="parent.taskEnd(true);" class="btn btn-default btn-xs btntaskEnd" />
                            <input id="btnPrint" type="button" value="打印" onclick="parent.formPrint(true);" class="btn btn-default btn-xs btnPrint" />
                        </ContentTemplate>
                    </asp:UpdatePanel>

                </div>
            </div>
        </div>
        <asp:TextBox ID="txtInstanceID" runat="server" CssClass=" hidden" ToolTip="0|0" Width="40" />
        <div class="col-md-12">
            <div class="row row-container" style="background-color: ">
                <div class="col-md-12">
                    <div class="panel panel-infos">
                        <div class="panel-headings" data-toggle="collapse" data-target="#SQXX">
                            <strong>申请基础信息</strong>
                        </div>
                        <div class="panel-body collapse in" id="SQXX">
                            <div class="col-xs-12 col-sm-12  col-md-12 col-lg-12">
                                <div class="">
                                    <asp:UpdatePanel ID="UpdatePanel_request" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <script type="text/jscript">
                                                var prm = Sys.WebForms.PageRequestManager.getInstance();
                                                prm.add_endRequest(function () {  
                                                    //初始化明细总价 
                                                    getTotalMoney();
                                                });
                                            </script>
                                            <table style="width: 1200px" border="0">
                                                <tr>
                                                    <td>申请单号：</td>
                                                    <td style="">
                                                        <asp:TextBox ID="aplno" runat="server" class="input input-readonly" placeholder="自动产生" ReadOnly="true" Width="247px" ToolTip="1|0" />
                                                    </td>
                                                    <td>申请日期：</td>
                                                    <td>
                                                        <asp:TextBox ID="createdate" runat="server" class="input input-readonly" Style="width: 200px" ReadOnly="True" />
                                                    </td>
                                                    <td>填单人：</td>
                                                    <td>
                                                        <div class="form-inline">
                                                            <asp:TextBox runat="server" ID="createid" class="input  input-readonly" Style="width: 20%" ReadOnly="True"></asp:TextBox>
                                                            <asp:TextBox runat="server" ID="createname" class="input  input-readonly " Style="width: 79%" ReadOnly="True"></asp:TextBox>
                                                        </div>
                                                    </td>
                                                </tr>

                                                <tr>
                                                    <td>申请人：</td>
                                                    <td>
                                                        <div class="form-inline">
                                                            <asp:TextBox runat="server" ID="aplid" class="input input-edit" Style="width: 70px" ReadOnly="True" onchange="getDeptLeaderbyEmp();getEmpCost($('#aplid').val());clearDetail();setChuNa($('#apldomain').val());"></asp:TextBox>
                                                            <asp:TextBox runat="server" ID="aplname" class="input input-readonly" Style="width: 70px" ReadOnly="True"></asp:TextBox>
                                                            <asp:TextBox runat="server" ID="apljob" class="input input-readonly" Style="width: 100px" ReadOnly="True" />
                                                        </div>
                                                    </td>
                                                    <td>申请人部门：
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="costcenter" runat="server" class="input input-readonly readonly " Width="80px" ReadOnly="True"></asp:TextBox>
                                                        <asp:TextBox ID="apldept" runat="server" class="input input-readonly readonly" Style="width: 120px" ReadOnly="True" />
                                                    </td>
                                                    <td>电话（分机）：</td>
                                                    <td>
                                                        <asp:TextBox ID="aplphone" runat="server" class="input  input-readonly" Style="width: 100%" />
                                                    </td>
                                                </tr>
                                                <tr class="hidden">
                                                    <td class="hidden">申请人公司：</td>
                                                    <td class="hidden">
                                                        <div class="form-inline">
                                                            <asp:TextBox ID="apldomain" runat="server" class="input input-readonly" Style="width: 70px" ReadOnly="True" />
                                                            <asp:TextBox ID="apldomainname" runat="server" class="input input-readonly" Style="width: 170px" ReadOnly="True" />
                                                        </div>
                                                    </td>
                                                    <td class="hidden">当前登陆人：<div class="form-inline">
                                                        <input id="txt_LogUserId" class="input input-readonly" style="width: 70px" runat="server" readonly="True" />
                                                        <input id="txt_LogUserName" class="input input-readonly" style="width: 70px" runat="server" readonly="True" />
                                                        <input id="txt_LogUserDept" class="input input-readonly" style="width: 100px" runat="server" readonly="True" />
                                                    </div>
                                                    </td>
                                                    <td class="hidden"></td>
                                                    <td class="hidden"></td>
                                                    <td class="hidden">签核相关人
                                                        <asp:TextBox ID="deptm" runat="server" />
                                                        <asp:TextBox ID="deptmfg" runat="server" Width="89px" />
                                                        <asp:TextBox ID="supervisor" runat="server" />
                                                        <asp:TextBox ID="totalfee" runat="server" Text="0"></asp:TextBox>
                                                        <asp:TextBox ID="onlyflag" runat="server" Text="0"></asp:TextBox>
                                                        <asp:TextBox ID="deptcode" runat="server" Text="0"></asp:TextBox>
                                                        <input id="chuna" runat="server" width="60px" />
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
            <div class="row  row-container" id="blockinfo">
                <div class="col-md-12">
                    <div class="panel panel-infos">
                        <div class="panel-headings" data-toggle="collapse" data-target="#un">
                            <strong style="padding-right: 100px">未报销明细（不包括在流程中的）</strong>
                        </div>
                        <div class="panel-body  collapse in" id="un">
                            <div class="col-xs-12 col-sm-12  col-md-12 col-lg-12">
                                <div>
                                    <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <script type="text/jscript">
                                                var prm = Sys.WebForms.PageRequestManager.getInstance();
                                                prm.add_endRequest(function () {                                               
                                                    
                                                    
                                                });
                                            </script>
                                            <asp:Button ID="btnSearchReImbursed" runat="server" Text="点此查看未报销项" OnClick="btnSearchReImbursed_Click" CssClass="btn btn-link" />
                                            <asp:GridView ID="HZ_UnGrid" runat="server" CellPadding="4" ForeColor="#333333" GridLines="both" BorderColor="#F0F0F0" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False"
                                                EmptyDataText="无尚未报销记录" EmptyDataRowStyle-HorizontalAlign="Center">
                                                <AlternatingRowStyle BackColor="White" />
                                                <Columns>
                                                    <asp:BoundField HeaderText="未报销项目" DataField="typedesc" HeaderStyle-Width="100px" />

                                                    <asp:TemplateField HeaderText="预算来源">
                                                        <ItemTemplate>
                                                            <asp:HyperLink ID="col2" runat="server" NavigateUrl='<%# Eval("linkaddress") %>' Target="_blank" Text='<%# Eval("con_desc") %>'></asp:HyperLink>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>

                                                <EmptyDataRowStyle HorizontalAlign="left" />
                                                <FooterStyle BackColor="#E4EFFA" Font-Bold="True" ForeColor="" />
                                                <HeaderStyle BackColor="#E4EFFA" Font-Bold="True" ForeColor="" />
                                                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                                <RowStyle BorderColor="#F0F0f0" />
                                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333" />
                                                <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                                <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                                <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                                <SortedDescendingHeaderStyle BackColor="#4870BE" />
                                            </asp:GridView>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                    <div>
                                    </div>
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
                            <strong style="padding-right: 100px">费用报销明细</strong>
                        </div>
                        <div class="panel-body  collapse in" id="PB">
                            <div class="col-xs-12 col-sm-12  col-md-12 col-lg-12">
                                <div>
                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <script type="text/jscript">
                                                var prm = Sys.WebForms.PageRequestManager.getInstance();
                                                prm.add_endRequest(function () {                                               
                                                    SetControlStatus2(<%=fieldStatus%>,"fin_feiyongbx_dtl_form");                                                    
                                                });
                                            </script>
                                            <span style="color: red; font-weight: bold" id="blockhint">注意:已请人事预定的机票,火车票，汽车票请勿重复申请报销.</span>
                                            <asp:GridView ID="grid" runat="server" CellPadding="4" ForeColor="Black" GridLines="None" ShowHeaderWhenEmpty="True" Width="1200px" AutoGenerateColumns="False" ShowFooter="True"
                                                EmptyDataText="请点添加增加记录" EmptyDataRowStyle-HorizontalAlign="Center" OnRowCommand="grid_RowCommand" OnRowDataBound="grid_RowDataBound" Font-Size="11pt">
                                                <AlternatingRowStyle BackColor="White" />
                                                <Columns>
                                                    <asp:TemplateField Visible="false" >
                                                        <ItemTemplate >
                                                            <asp:Label ID="id2" runat="server" Text='<%# Bind("id2") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="aplno" Visible="false" />
                                                    <asp:BoundField DataField="rowno" HeaderText="序号"  ItemStyle-CssClass="text-right"/>
                                                    <asp:TemplateField HeaderText="">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="costcateid" runat="server" Text='<%# Bind("costcateid") %>' onclick="selCostCate(this);" onchange="setLimit(this);clearRowData(this);" Width="60" Style="display: none"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="类别和费用项目">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="costcatename" runat="server" Width="200px" Text='<%# Bind("costcatename") %>' onclick="selCostCate(this);" onchange="setLimit(this);clearRowData(this);" ReadOnly="true"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="预算来源">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="instanceid" runat="server" Text='<%# Bind("instanceid") %>' onclick="selBudgetSource(this)" ReadOnly="true"></asp:TextBox>
                                                            <asp:TextBox ID="budgetsour" runat="server" Text='<%# Bind("budgetsour") %>' onchange="" Width="0px" Style="display: none"></asp:TextBox>
                                                            <asp:HyperLink NavigateUrl='<%# Eval("budgetsour")%>' ID="lnkbudgetsour" runat="server" Target="_blank" Text="查看"></asp:HyperLink>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="费用发生日期/区间">
                                                        <ItemTemplate>
                                                            <div class="btn-group">
                                                                <asp:TextBox ID="feedate" runat="server" CssClass="dropdown-toggle" data-toggle="dropdown" ReadOnly="true" Text='<%# Bind("feedate") %>' onfocus="setFeeDate(this);" onchange="setLimit(this);setFeenote(this);" Width="120px"></asp:TextBox>

                                                                <ul id="ulfeedate" class="dropdown-menu">
                                                                </ul>
                                                            </div>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="费用说明">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="feenote" runat="server" Text='<%# Bind("feenote") %>' Width="400px"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="额度" HeaderStyle-CssClass="th_right">

                                                        <ItemTemplate>
                                                            <asp:TextBox ID="limit" runat="server" Text='<%# Eval("limit").ToString()==""? "":string.Format("{0:n1}",Convert.ToDecimal(Eval("limit")) ) %>' Width="80px" Style="text-align: right; margin-right: 5px"></asp:TextBox>

                                                        </ItemTemplate>

                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="报销金额" HeaderStyle-CssClass="th_right" ItemStyle-CssClass="text-right">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="amount" runat="server" Text='<%# string.Format("{0:n1}",Eval("amount")) %>' onchange="getTotalMoney();setBackColor(this);$('#refresh').click();" Width="90px" Style="text-align: right"></asp:TextBox>

                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="total" runat="server" Font-Size="11pt" ForeColor="Red"></asp:Label>
                                                        </FooterTemplate>
                                                        <HeaderStyle HorizontalAlign="Right"  Width="90px"/>
                                                        <FooterStyle  CssClass="text-right"   />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField ShowHeader="False" HeaderStyle-CssClass="text-right" FooterStyle-CssClass="text-right">
                                                        <HeaderTemplate>
                                                            <asp:Button ID="btnAdd" runat="server" Text="添加" CommandName="add" CssClass="btn btn-default btn-sm" />
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Button ID="btnDel" runat="server" Text="删除" CommandName="del" CssClass="btn btn-default btn-sm" Height="25px" />
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Button ID="btnAdd" runat="server" Text="添加" CommandName="add" CssClass="btn btn-default btn-sm" /></FooterTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                                <EditRowStyle BackColor="#2461BF" />
                                                <EmptyDataRowStyle HorizontalAlign="Center" />
                                                <FooterStyle BackColor="#cfcfcf" Font-Bold="True" ForeColor="" />
                                                <HeaderStyle BackColor="#31708f" Font-Bold="True" ForeColor="white" />
                                                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                                <RowStyle BackColor="#F0F0F0" />
                                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                                <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                                <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                                <SortedDescendingHeaderStyle BackColor="#4870BE" />
                                            </asp:GridView>
                                            <asp:Button ID="refresh" runat="server" Text="刷新汇总数据" OnClick="refresh_Click" Style="display: none"></asp:Button>
                                            <asp:HiddenField ID="txtFin_CA_No" Value="临时存放私车公用单号" runat="server"></asp:HiddenField>
                                            <asp:Button ID="btnCar" runat="server" Text="获取选择私车公用单号下所有记录写入grid" OnClick="btnCar_Click" Style="display: none"></asp:Button>
                                            <asp:GridView ID="HZgrid" runat="server" CellPadding="4" ForeColor="#333333" GridLines="both" BorderColor="#F0F0F0" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False"
                                                EmptyDataText="无差旅费用报销记录" EmptyDataRowStyle-HorizontalAlign="Center" OnRowCommand="grid_RowCommand" Caption="差旅费汇总" Width="500px" OnRowDataBound="HZgrid_RowDataBound">
                                                <AlternatingRowStyle BackColor="White" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="预算来源">
                                                        <ItemTemplate>
                                                            <asp:HyperLink ID="budgetsour" runat="server" NavigateUrl='<%# Bind("budgetsour") %>' Target="_blank" Text='<%# Eval("instanceid") %>'></asp:HyperLink>
                                                            <%--<a href='<%# Eval("budgetsour") %>' id="a" ><%# Eval("instanceid") %></a>--%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="类别和费用项目">
                                                        <ItemTemplate>
                                                            <asp:Label ID="costcatename" runat="server" Text='<%# Bind("costcatename") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="预算金额" ItemStyle-HorizontalAlign="right">
                                                        <ItemTemplate>
                                                            <asp:Label ID="limit" runat="server" Text='<%# String.Format("{0:n1}",Eval("limit")) %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="报销金额" ItemStyle-HorizontalAlign="right">
                                                        <ItemTemplate>
                                                            <asp:Label ID="amount" runat="server" Text='<%# String.Format("{0:n1}",Eval("amount")) %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="差异金额" ItemStyle-HorizontalAlign="right">
                                                        <ItemTemplate>
                                                            <asp:Label ID="chayi" runat="server" Text='<%# String.Format("{0:n1}",Eval("chayi")) %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>

                                                <EmptyDataRowStyle HorizontalAlign="left" />
                                                <FooterStyle BackColor="#E4EFFA" Font-Bold="True" ForeColor="" />
                                                <HeaderStyle BackColor="#E4EFFA" Font-Bold="True" ForeColor="" />
                                                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                                <RowStyle BorderColor="#F0F0f0" />
                                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333" />
                                                <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                                <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                                <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                                <SortedDescendingHeaderStyle BackColor="#4870BE" />
                                            </asp:GridView>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                    <div>
                                    </div>
                                </div>
                                <div class="marks" hidden>
                                    <%--<asp:TextBox ID="files" runat="server"></asp:TextBox>--%>
                                    <asp:Panel ID="filecontainer" runat="server" GroupingText="附件">
                                        <div style="margin-top: 10px">
                                            <asp:FileUpload runat="server" ID="file" AllowMultiple="true" />
                                        </div>
                                    </asp:Panel>
                                </div>
                                <div class="panel- panel-info-">
                                    <div class="panel-heading" data-toggle="collapse" data-target="#FJSC">
                                        <strong>附件</strong>
                                    </div>
                                    <div class="panel-body collapse in" id="FJSC">
                                        <div class="col-xs-12 col-sm-12  col-md-12 col-lg-12" style="width: 1000px;">                                            
                                                <dx:ASPxUploadControl ID="uploadcontrol" runat="server" Width="500px" BrowseButton-Text="浏览" Visible="true" ClientInstanceName="UploadControl"
                                                    ShowAddRemoveButtons="True" RemoveButton-Text="删除" UploadMode="Advanced" AutoStartUpload="true" ShowUploadButton="false" ShowProgressPanel="true"
                                                    OnFileUploadComplete="uploadcontrol_FileUploadComplete">
                                                    <AdvancedModeSettings EnableDragAndDrop="True" EnableFileList="True" EnableMultiSelect="True">
                                                    </AdvancedModeSettings>
                                                    <ClientSideEvents FileUploadComplete="onFileUploadComplete" />
                                                </dx:ASPxUploadControl>
                                                <input type="hidden" id="ip_filelist" name="ip_filelist" runat="server" />
                                                <table id="tbl_filelist" width="500px" >
                                                </table>

                                                <asp:UpdatePanel runat="server" ID="p11" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <textarea id="ip_filelist_db" name="ip_filelist" runat="server" cols="200" rows="2" visible="false"></textarea>
                                                        <asp:Table ID="tab1" Width="500px" runat="server" style="font-size:11pt">
                                                        </asp:Table>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>                                            
                                        </div>                                        
                                    </div>
                                </div>
                            </div>
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
                                    <td width="60px">
                                        <label>处理意见：</label></td>
                                    <td>
                                        <textarea id="comment" placeholder="请在此处输入处理意见" class="form-control"  onchange="setComment(this.value)" ></textarea>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <%--<div style="margin-top:-10px;">
        <%Server.Execute("/platform/workflowrun/ShowComment.aspx"); %>
    </div>--%>
    </form>
</body>
</html>

