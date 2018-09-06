<%@ Page Title="费用报销申请单" Language="C#" AutoEventWireup="true" CodeFile="Fin_FeiYongBX.aspx.cs" Inherits="Fin_FeiYongBX" MaintainScrollPositionOnPostback="True" ValidateRequest="true" %>



<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script src="../../../Content/js/jquery.min.js"></script>
    <script src="../../../Content/js/bootstrap.min.js"></script>    
    <script src="../../Content/js/layer/layer.js"></script>
    <script src="../../Content/js/plugins/layer/laydate/laydate.js"></script>
    <link href="../../Content/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../../../Content/js/plugins/bootstrap-select/css/bootstrap-select.min.css" rel="stylesheet" />
    <link href="../../Content/css/custom.css" rel="stylesheet" />
    <style>
        hidden {
            display: none;
        }
        .input {border-left:none ;border-right:none;border-top:none;   height:25px;padding-left:5px}
        .input-edit{ border-bottom:1px solid gray;    }
        .input-readonly { border-bottom:1px solid  lightgray }
        .select{border-left:none ;border-right:none;border-top:none;border-bottom:1px solid gray  ;height:25px}
        .name{width:80px}
        .bordernone{border:none; }
        .alpha100{ background:rgba(0, 0, 0, 0); }
        .width100 { width:100px  }.width50 { width:50px  }
    </style>
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
               // SetControlStatus(fieldSet);
                
            }       
            //初始化主管
            if(paramMap.instanceid==undefined){
                getDeptLeaderbyEmp();
            }
            //初始化明细总价 
            getTotalMoney();
             

        })// end ready
        
        function validUpdate(value){
            if($("[id*=pt_part]").val()=="")
                alert(value);
            
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
                        $("#"+id).attr("readonly","readonly");
                    }
                    else if( ( paramMap.display==1||statu.indexOf("1_")!="-1") && ( ctype=="checkbox"||ctype=="radio"||ctype=="select"||ctype=="file" ) ){
                        $("#"+id).attr("disabled","disabled");                 
                    }

                    if( statu.indexOf("1_")!="-1" ||  paramMap.display==1){
                        //如果是显示模式或只读模式
                        
                        //设定treelist 编辑性
                        $('table[id=grid] th:last').hide();
                        $('table[id=grid] tbody tr').each(function(index,item)
                        {
                            $(item).find("td:last").hide();
                        })
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

    </script>




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
        function validate(id){             
            if($("#deptm").val()==""){
                layer.alert("直属主管未获取到，请尝试重新打开申请单。请联系IT设定.");
                return false;
            }
            if($("#projector").val()==""){
                layer.alert("项目负责人未获取到，请尝试重新打开申请单。请联系IT设定.");
                return false;
            }
            <%=ValidScript%>
            var flag=true;
            var msg="";             
            // 费用说明
            $("#input[id*=pgino]").each(function (){
                if( $(this).val()==""){
                    msg+="【项目号】不可为空.";
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

            if(flag==false){  
                layer.alert(msg);
                return false;
            }
            if($("#upload").val()==""&& $("#link_upload").text()==""){
                layer.alert("请选择【附件】");
                return false;
            }           

        }

    
    </script>

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
                    
                }
            });

            
        }

        //选择费用项目
        function selCostCate(obj){               
            var clickid=$(obj).attr("id");             
            var ctrl0=clickid.replace("costcateid","costcateid");
            var ctrl1=clickid.replace("costcateid","costcatename");
            var ctrl2=clickid.replace("costcateid","instanceid");
            var ctrl3="apldomain";      
            var _aplid=$("#aplid").val();
            var _domain=$("#apldomain").val();
            var _costcateid=$("#"+ctrl0).val();
            var _acptctrl=clickid.replace("costcateid","feedate");
            layer.open({
                shade: [0.5, '#000', false],
                type: 2,
                offset: '100px',
                area: ['600px', '450px'],
                fix: false, //不固定
                maxmin: false,
                title: ['<i class="fa fa-dedent"></i> 选择费用项目', false],
                closeBtn: 1,
                content: '/forms/open/select.aspx?windowid=fin_feiyongbx_form&changekey='+ctrl0+'&ctrl0='+ctrl0+'&ctrl1='+ctrl1+'&ctrl2='+ctrl2+'&ctrl3='+ctrl3,
                end: function () {
                    if($(obj).val()!=""){
                        var _feedateid=clickid.replace("costcateid","feedate");
                        var ul=$("#"+_feedateid)[0].nextElementSibling;
                        setdate(ul,_domain,_costcateid,_aplid,_acptctrl);                     
                    }

                }
            });
        }
        //选择预算来源
        function selBudgetSource(obj){       //obj 是 instanceid   
            if("部门预算，个人额度".indexOf($(obj).val()) != -1){
                return false
            }
            var id=$(obj).attr("id");             
            var ctrl0=id.replace("instanceid","budgetsour"); 
            var ctrl1=id
            var ctrl2=""
            var ctrl3="";//apldomain
            var _domain=$("#apldomain").val();
            var flag=$("#"+ctrl0).val();
            var _aplid=$("#aplid").val();
            //var _costcateid=$("#"+ctrl0).val();
            // var _acptctrl=id.replace("costcateid","feedate");
            layer.open({
                shade: [0.5, '#000', false],
                type: 2,
                offset: '100px',
                area: ['700px', '450px'],
                fix: false, //不固定
                maxmin: false,
                title: ['<i class="fa fa-dedent"></i> 选择预算来源', false],
                closeBtn: 1,
                content: '/forms/fin/selectform.aspx?windowid=fin_feiyongbx_form&aplid='+_aplid+'&changekey='+ctrl0+'&ctrl0='+ctrl0+'&ctrl1='+ctrl1+'&ctrl2='+ctrl2+'&ctrl3='+ctrl3,
                end: function () {
                    if($(obj).val()!=""){  
                        var wfid=$("#"+ctrl0).val() ; //budgetsour val
                        if(wfid!=""&&flag!=$("#"+ctrl0).val())
                        {
                            var src="/Platform/WorkFlowRun/Default.aspx?flowid="+wfid+"&instanceid="+$(obj).val()+"&display=1";
                            $("#"+ctrl0).val(src);
                            var link=$("#"+ctrl0)[0].nextElementSibling;
                            $(link).attr("href",src);
                        }
                                            
                    }

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

        function setFeenote(obj){               
            var objid=$(obj).attr("id");  //feedateid  
            var feenoteid=objid.replace("feedate","feenote");           
            var showmsg="";

            if($(obj).val().indexOf("Q")>=0){
                showmsg="L4(含L4)以上人员"+$(obj).val().replace("Q","-")+"季度团建费";                
            }
            else if($(obj).val().indexOf("H")>=0){
                showmsg="L5人员"+$(obj).val().replace("H1","上半").replace("H2","下半")+"年度团建费";
            }
            //alert(showmsg)
            $("#"+feenoteid).val(showmsg);        
             
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
                    var arr=eval(data.d);
                    if (arr == undefined) {                      
                       
                        $("#"+_acptctrl).removeAttr("data-toggle").attr("onclick","laydate()");
                    }
                    else {  
                        $("#"+_acptctrl).removeAttr("onclick").attr("data-toggle","dropdown");
                        //var arr=eval(data.d)
                        $(ul).find("li").remove();
                        for(var i = 0; i<arr.length; i++){
                            $(ul).append(" <li value='"+arr[i].dateT+"' onclick=\"$('#"+_acptctrl+"').val('"+arr[i].dateT+"');$('#"+_acptctrl+"').change();\"   >"+arr[i].dateT+"</li>");                            
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
                        }                   
                    },
                    error: function (err) {
                        layer.alert(err);
                    }
                });
            }
        }
        //获取部门领导和分管副总
        function getDeptLeaderbyEmp(){
            var _emp=$("#aplid").val();
            var _domain=$("#apldomain").val();
            // 部门主管
            $.ajax({
                type: "Post",async: false,
                url: "Fin_FeiYongBX.aspx/getDeptLeaderByEmp" , 
                //方法传参的写法一定要对，str为形参的名字,str2为第二个形参的名字
                //P1:wlh P2： 
                data: "{'domain':'"+_domain+"','emp':'"+_emp+"'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {//返回的数据用data.d获取内容//                      
                        if (data.d == "") {
                            $("[id*=aplid]").val("");
                            layer.msg("未获取到申请人部门经理，请重试.");  
                            
                        }
                        else {                             
                            $("[id*=deptm]").val(data.d)
                        }                   
                },
                error: function (err) {
                    layer.alert(err);
                }
            });
            //分管领导
            $.ajax({
                type: "Post",async: false,
                url: "Fin_FeiYongBX.aspx/getChargeLeaderByUser" , 
                //方法传参的写法一定要对，str为形参的名字,str2为第二个形参的名字
                //P1:wlh P2： 
                data: "{'domain':'"+_domain+"','userid':'"+_emp+"'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {//返回的数据用data.d获取内容//                      
                    if (data.d == "") {
                        
                        $("[id*=aplid]").val("");
                        layer.msg("未获取到申请人分管领导.");  
                            
                    }
                    else {     
                        alert(data.d);
                        $("[id*=deptmfg]").val(data.d)
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
                var rowval=$("tr td input[id*=amount_"+i+"]").val();
                rowval= (rowval==""||rowval=="NaN")? 0 : rowval;                
                totalMoney=totalMoney+parseFloat(rowval)
                $("#totalfee").val(totalMoney);
                
            })
            //grid底部total值更新
            $('table[id*=grid] tr td span[id*=total]').each(function (i) {
               // if($.trim($(this).text())==""){
                    $(this).text("合计:"+fmoney(totalMoney,2));
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
    </script>

    <form id="form1" runat="server"  >
       
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
                        </ContentTemplate>
                    </asp:UpdatePanel>

                </div>
            </div>
        </div>
        <asp:TextBox ID="txtInstanceID" runat="server" CssClass=" hidden" ToolTip="0|0" Width="40" />
        <div class="col-md-12">
            <div class="row row-container" style="background-color:">
                <div class="col-md-12">
                    <div class="panel panel-info">
                        <div class="panel-heading" data-toggle="collapse" data-target="#SQXX">
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
                                            <table style="height: 35px; width: 100%">
                                                <tr>
                                                    <td>申请单号：</td>
                                                    <td>
                                                        <asp:TextBox ID="aplno" runat="server" class="input input-readonly"   placeholder="自动产生"  ReadOnly="true" Width="247px" ToolTip="1|0" />
                                                    </td>
                                                    <td>申请日期：</td>
                                                    <td>
                                                        <asp:TextBox ID="createdate"  runat="server" class="input input-readonly" Style=" width: 200px" ReadOnly="True"  />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td >填单人：</td>
                                                    <td>
                                                        <div class="form-inline">
                                                            <asp:TextBox runat="server" ID="createid" class="input input-readonly"  Style=" width: 70px" ReadOnly="True"></asp:TextBox>
                                                            <asp:TextBox runat="server" ID="createname"  class="input input-readonly" Style=" width: 70px" ReadOnly="True"></asp:TextBox>                                                            
                                                        </div>
                                                    </td>
                                                    <td  >当前登陆人：</td>
                                                    <td  ><div class="form-inline">
                                                            <input id="txt_LogUserId" class="input input-readonly" style=" width: 70px" runat="server" readonly="True" />
                                                            <input id="txt_LogUserName" class="input input-readonly"  style=" width: 70px" runat="server" readonly="True" />
                                                            <input id="txt_LogUserDept"  class="input input-readonly" style=" width: 100px" runat="server" readonly="True" />
                                                        </div>
                                                        </td>
                                                </tr>
                                                <tr>
                                                    <td>申请人：</td>
                                                    <td>
                                                        <div class="form-inline">
                                                            <asp:TextBox runat="server" ID="aplid" class="input input-edit"   Style=" width: 70px" ReadOnly="True" onchange="getDeptLeaderbyEmp()"></asp:TextBox>
                                                            <asp:TextBox runat="server" ID="aplname"  class="input input-readonly" Style=" width: 70px" ReadOnly="True"></asp:TextBox>
                                                            <asp:TextBox runat="server" ID="apljob" class="input input-readonly"  Style=" width: 100px" ReadOnly="True" />
                                                        </div>
                                                    </td>
                                                    <td >电话（分机）：
                                                    </td>
                                                    <td >
                                                        <asp:TextBox ID="aplphone" runat="server" class="input input-readonly" Style=" width: 200px"  />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>申请人公司：</td>
                                                    <td>
                                                        <div class="form-inline">
                                                            <asp:TextBox ID="apldomain" runat="server" class="input input-readonly" Style=" width: 100px" ReadOnly="True" />
                                                                                                                    
                                                        </div>
                                                    </td>
                                                    <td style="display: ">申请人部门：
                                                    </td>
                                                    <td style="display: ">
                                                        <asp:TextBox ID="apldept" runat="server" class="input input-readonly readonly" Style=" width: 200px"  ReadOnly="True"/>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td></td>
                                                    <td>                                                         
                                                    </td>
                                                    <td hidden></td>
                                                    <td hidden>  签核相关人
                                                        <asp:TextBox ID="deptm"  runat="server" />
                                                        <asp:TextBox ID="deptmfg"  runat="server" Width="89px" />
                                                        <asp:TextBox ID="supervisor"  runat="server" />
                                                        <asp:TextBox ID="totalfee" runat="server"   Text="0"></asp:TextBox>
                                                        <asp:TextBox ID="onlyflag" runat="server"   Text="0"></asp:TextBox>
                                                        <asp:TextBox ID="costcenter" runat="server"   Text="0"></asp:TextBox>
                                                        <asp:TextBox ID="deptcode" runat="server"   Text="0"></asp:TextBox>
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
                        <div class="panel-heading" data-toggle="collapse" data-target="#PB">
                            <strong style="padding-right: 100px">费用报销明细</strong>
                        </div>
                        <div class="panel-body  collapse in" id="PB">
                            <div class="col-xs-12 col-sm-12  col-md-12 col-lg-12" >
                                <div>
                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <script type="text/jscript">
                                                var prm = Sys.WebForms.PageRequestManager.getInstance();
                                                prm.add_endRequest(function () {
                                               
                                                   SetControlStatus2(<%=fieldStatus%>,"fin_feiyongbx_dtl_form");
                                                });
                                            </script>
                                            <asp:GridView ID="grid" runat="server" CellPadding="4" ForeColor="black" GridLines="None" ShowHeaderWhenEmpty="True" Width="100%" AutoGenerateColumns="False" ShowFooter="True" 
                                                EmptyDataText="请点添加增加记录"  EmptyDataRowStyle-HorizontalAlign="Center" OnRowCommand="grid_RowCommand" OnRowDataBound="grid_RowDataBound">
                                                <AlternatingRowStyle BackColor="White" />
                                                <Columns>                                                    
                                                    <asp:TemplateField Visible="false">                                                         
                                                        <ItemTemplate>
                                                            <asp:Label ID="id2" runat="server" Text='<%# Bind("id2") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="aplno" Visible="false"/>
                                                    <asp:BoundField DataField="rowno"  HeaderText="序号"/>
                                                    <asp:TemplateField HeaderText="费用代码">                                                        
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="costcateid" runat="server" Text='<%# Bind("costcateid") %>' onclick="selCostCate(this);" onchange="setLimit(this);" Width="60"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="类别和费用项目">                                                        
                                                        <ItemTemplate>
                                                             <asp:TextBox ID="costcatename" runat="server" Text='<%# Bind("costcatename") %>'  ReadOnly="true"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="预算来源">                                                        
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="instanceid" runat="server" Text='<%# Bind("instanceid") %>'  onclick="selBudgetSource(this)"></asp:TextBox>
                                                            <asp:TextBox ID="budgetsour" runat="server" Text='<%# Bind("budgetsour") %>' Width="0px" ></asp:TextBox>
                                                            <asp:hyperlink  NavigateUrl='<%# Eval("budgetsour")%>' id="lnkbudgetsour" runat="server" target="_blank"  Text="查看"></asp:hyperlink>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="费用发生日期/区间">
                                                        <ItemTemplate>
                                                            <div class="btn-group" >
                                                                <asp:TextBox ID="feedate" runat="server" data-toggle="dropdown" Text='<%# Bind("feedate") %>' onfocus="setFeeDate(this);" onchange="setLimit(this);setFeenote(this);" ></asp:TextBox>
                                                                <ul id="ulfeedate" class="dropdown-menu">                                                                    
                                                                </ul>
                                                            </div>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="费用说明">                                                        
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="feenote" runat="server" Text='<%# Bind("feenote") %>'></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="额度">
                                                        <ItemTemplate>                                                            
                                                            <asp:TextBox ID="limit" runat="server" Text='<%# Bind("limit") %>' Width="80px" ReadOnly="true"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="报销金额">                                                        
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="amount" runat="server" Text='<%# Bind("amount") %>' onchange="getTotalMoney();setBackColor(this);" ></asp:TextBox>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                           <asp:label ID="total" runat="server" ></asp:label>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField ShowHeader="False">
                                                        <HeaderTemplate>
                                                            <asp:button ID="btnAdd"  runat="server" Text="添加"  CommandName="add"  CssClass="btn btn-success btn-sm" />
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:button ID="btnDel"  runat="server" Text="删除"  CommandName="del"  CssClass="btn btn-warning btn-sm" Height="25px"/>
                                                        </ItemTemplate>
                                                        <FooterTemplate><asp:button ID="btnAdd"  runat="server" Text="添加"  CommandName="add"  CssClass="btn btn-success btn-sm"  /></FooterTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                                <EditRowStyle BackColor="#2461BF" />  
                                                <EmptyDataRowStyle HorizontalAlign="Center" />
                                                <FooterStyle BackColor="#E4EFFA" Font-Bold="True" ForeColor="" /> <%--#507CD1--%>
                                                <HeaderStyle BackColor="#E4EFFA" Font-Bold="True" ForeColor="#333" />
                                                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                                <RowStyle BackColor="#F0F0F0"  />
                                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                                <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                                <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                                <SortedDescendingHeaderStyle BackColor="#4870BE" />
                                            </asp:GridView>

                                            <asp:GridView ID="HZgrid" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" ShowFooter="True" 
                                                EmptyDataText="无差旅费用报销记录"  EmptyDataRowStyle-HorizontalAlign="Center" OnRowCommand="grid_RowCommand" Caption="差旅费汇总"  Width="500px" >
                                                <AlternatingRowStyle BackColor="White" />
                                                <Columns>                                                    
                                                    <asp:TemplateField HeaderText="预算来源">                                                        
                                                        <ItemTemplate>                                           
                                                            <asp:HyperLink ID="budgetsour" runat="server" NavigateUrl='<%# Eval("budgetsour") %>' Target="_blank" Text='<%# Eval("instanceid") %>'></asp:HyperLink>
                                                            <%--<a href='<%# Eval("budgetsour") %>' id="a" ><%# Eval("instanceid") %></a>--%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField> 
                                                    <asp:TemplateField HeaderText="类别和费用项目">                                                        
                                                        <ItemTemplate>
                                                             <asp:label ID="costcatename" runat="server" Text='<%# Bind("costcatename") %>'></asp:label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>                                                    
                                                    <asp:TemplateField HeaderText="预算合计">
                                                        <ItemTemplate>                                                            
                                                            <asp:label ID="limit" runat="server" Text='<%# Bind("limit") %>'></asp:label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="报销金额">                                                        
                                                        <ItemTemplate>
                                                            <asp:label ID="amount" runat="server" Text='<%# Bind("amount") %>' onchange="getTotalMoney()" ></asp:label>
                                                        </ItemTemplate>                                                        
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="差异金额">
                                                        <ItemTemplate>                                                            
                                                            <asp:Label ID="chayi" runat="server" Text='<%# Bind("chayi") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                                <EditRowStyle BackColor="#2461BF" />
                                                <EmptyDataRowStyle HorizontalAlign="left" />
                                                <FooterStyle BackColor="#E4EFFA" Font-Bold="True" ForeColor="" />
                                                <HeaderStyle BackColor="#E4EFFA" Font-Bold="True" ForeColor="" />
                                                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                                <RowStyle BackColor="#EFF3FB" />
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
                                        <textarea id="comment" placeholder="请在此处输入处理意见" class="form-control" onchange="setComment(this.value)"></textarea>


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

