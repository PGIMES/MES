<%@ Page Title="验收申请单" Language="C#" AutoEventWireup="true" CodeFile="Pur_RCT.aspx.cs" Inherits="Pur_RCT" MaintainScrollPositionOnPostback="True" ValidateRequest="true" %>



<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script src="../../../Content/js/jquery.min.js"></script>
    <script src="../../../Content/js/bootstrap.min.js"></script>
    <script src="../../Content/js/layer/layer.js"></script>
    <script src="../../Content/js/plugins/validate/jquery.validate.min.js"></script>
    <script src="../../Content/js/plugins/validate/messages_zh.min.js"></script>
    <link href="../../Content/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../../../Content/js/plugins/bootstrap-select/css/bootstrap-select.min.css" rel="stylesheet" />
    <link href="../../Content/css/custom.css" rel="stylesheet" />
    <style>
        hidden {
            display: none;
        }
        .input {padding-left:5px;
            border-left-style: none;
            border-left-color: inherit;
            border-left-width: medium;
            border-right-style: none;
            border-right-color: inherit;
            border-right-width: medium;
            border-top-style: none;
            border-top-color: inherit;
            border-top-width: medium;
        }
        .input-edit{ border-bottom:1px solid gray; background-color:#FDF7D9; height:25px   }
        .input-readonly { border-bottom:1px solid  lightgray ;height:25px}
        .select{border-left:none ;border-right:none;border-top:none;border-bottom:1px solid gray  ;height:25px}
        .name{width:80px}
        .bordernone{border:none; }
        .alpha100{ background:rgba(0, 0, 0, 0); }
        .width150 { width:150px  }.width100 { width:100px  }.width50 { width:50px  }
        td {    padding-top:3px; padding-bottom:3px    }

          .panel-info {
            border-color: none; 
        }
        .panel-infos {
            color: #31708f; 
            background-color:none;
             
            border-bottom:0px solid white;
            -webkit-box-shadow: 0 1px 1px rgba(0,0,0,.00);
        }
        .panel-headings{
            background-color:none;
            border-bottom:1px solid #777
        }
        textarea {
    display: flex;
    justify-content: center;
    align-items: center;
}
    </style>
    <script src="../../Content/js/plugins/layer/laydate/laydate.js"></script>
    <script src="../../Scripts/RFlow.js"></script>
    <script type="text/javascript">
        var paramMap;
        var ctl="pur_rct.aspx";
        $(document).ready(function () {
            $("#mestitle").html("【验收申请单】<a href='/userguide/RCTGuide.pptx' target='_blank' class='h5' style='color:red'>使用说明</a> ");
             
            SetButtons();                     
            //获取参数名值对集合Json格式
            var url =window.parent.document.URL;
            var viewmode="";
            paramMap = getURLParams(url); 
            
            if(paramMap.mode!=NaN&&paramMap.mode!=""&&paramMap.mode!=undefined)
            {
                viewmode=paramMap.mode;                                
                DisableButtons();//禁用流程按钮                               
            }  
            //debugger;
            //如果查询画面参数过来的就执行初始化
            if(paramMap.pono!=undefined){            
                //
                $("#lb").val(paramMap.mscode.substring(0,1));

                if(paramMap.mscode.indexOf("2")==0){ //2: 直接验收  3：到货确认-验收
                    //$("#obj").val("服务");
                    hideService(true);
                    $("#checkqty").val(paramMap.qty);//默认为1
                    
                }
                else{
                    $("#checkqty").val("1");//默认为1
                }
                if(paramMap.pono!=""){
                    $("#pono").val(paramMap.pono);
                }
                if(paramMap.rowid!=""){
                    $("#porowno").val(paramMap.rowid);
                }

                if(paramMap.domain!=""){
                    $("#domain").val(paramMap.domain);
                }

                if(paramMap.rid!=""){
                    $("#rid").val(paramMap.rid);
                }

                if(paramMap.mc!=""){
                    var mc=getUrlParam("mc");                     
                    $("#ms").val(mc);
                }

               

                //if(paramMap.lb!=""){}
                var lb=getUrlParam("lb");  
                    
                $("#obj").val(lb);
                 
                $("#obj").find("option[value='"+lb+"']").attr("selected",true);
                //if(lb=="QADContract"){
                //    $("#obj").val("设备（含测量设备/仪器）");
                //}
                
                if(paramMap.ms!=""){
                    $("#description").val(getUrlParam("ms"));
                }
                if(paramMap.contractno!=""){
                    $("#contractno").val(paramMap.contractno);
                }
                if(paramMap.date!=""){
                    $("#receivedate").val(getUrlParam("date"));
                }
                $("#checkdate").val()
            }

            hideService($("#lb").val()=="2"?true:false);

            //财务签核人
            getFinSignPerson();
            //
            
            if($("#deptm").val()=="" )
            {getDeptLeader();}

            IsExistsRct();
    
        })// end ready
        
        function hideService(bln){
            if(bln==true){
                var services=$(".service");
                services.hide();

                for(var i=0;i<services.length;i++){
                    var ctr=$(services[i]).find("input[type=text],select");
                    if(ctr.length>0){                        
                        $(ctr[0]).val("")                        
                    }
                }                

            }else{
                $(".service").show();
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
                   // debugger;
                    var statu=fieldStatus[item];
                    if(  ( paramMap.display==1||statu.indexOf("1_")!="-1") && (ctype=="text"||ctype=="textarea") ){
                        $("#"+id).attr("readonly","readonly").removeAttr("onclick");
                        $("#"+id).addClass("input-readonly").removeClass("input-edit");
                    }
                    else if( ( paramMap.display==1||statu.indexOf("1_")!="-1") && ( ctype=="checkbox"||ctype=="radio" ) ){                   

                        $("#"+id).attr("disabled","disabled");   
                        $("#"+id).addClass("input-readonly").removeClass("input-edit").removeAttr("onclick").removeAttr("onchange");
                    }else if(( paramMap.display==1||statu.indexOf("1_")!="-1") && (  ctype=="select")){
                        //$("#"+id).attr("disabled","disabled"); 
                        $("#"+id).addClass("input-readonly").removeClass("input-edit").attr("onclick","this.defaultIndex=this.selectedIndex;").attr("onchange","this.selectedIndex=this.defaultIndex;"); 
                        
                    }       
                    else if( ( paramMap.display==1||statu.indexOf("1_")!="-1") && ( ctype=="file" ) ){
                        $("#"+id).hide();
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
                        $(obj).removeAttr("onclick");$(obj).removeAttr("ondblclick");
                      //  $(obj).css("border","none");
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
            SetControlStatus(<%=fieldStatus%>,"pur_rct_main_form");             
        });

        //验证
        function validate(id){
             
            if($("#obj").val()==""){
                layer.alert("【验收对象】不可为空.");
                return false;
            }
            if($("#lb").val()!="2" && $("#type").val()==""){
                layer.alert("【验收类型】不可为空.");
                return false;
            }
            
            if($("#ms").val()==""){
                layer.alert("【名称描述】不可为空.");
                return false;
            }
            
            if($("#checkqty").val()==""){
                layer.alert("【验收数量】不可为空.");
                return false;
            }
            if($("#lb").val()!="2" && $("#equipmentno").val()==""){
                layer.alert("【设备编号】不可为空.");
                return false;
            }
            if($("#lb").val()!="2" && $("#usedept").val()==""){
                layer.alert("【使用部门】不可为空.");
                return false;
            }
            if($("#lb").val()!="2" && $("#position").val()==""){
                layer.alert("【存放位置】不可为空.");
                return false;
            }
            if($("#lb").val()!="2" && $("#receivedate").val()==""){
                layer.alert("【实际到货日期】不可为空.");
                return false;
            }
            if($("#isuseful").val()==""){
                layer.alert("【是否已达到预定可使用状态符合验收条件】不可为空.");
                return false;
            } 
            if($("#costcentre").val()==""){
                layer.alert("【成本归属单位】不可为空.");
                return false;
            }            
            if( $("#checkdate").val()==""){
                layer.alert("【验收日期】不可为空.");
                return false;
            }            
            if($("#lb").val()!="2" && $("#files").val()==""&& $("#link_file").text()==""){
                layer.alert("【验收附件】请上传.");
                return false;
            } 
            if($("#comment").val()==""){
                layer.alert("【处理意见】不可为空.");
                return false;
            } 
            <%=ValidScript%>
            var flag=true;
            var msg=""; 
            if(flag==false){  
                layer.alert(msg);
                return false;
            }            
            
        }    
    </script>

    <script type="text/javascript">
        //function setvalue(ctrl0, keyValue0, ctrl1, keyValue1, ctrl2, keyValue2) {

        //    $("input[id*='" + ctrl0 + "']").val(keyValue0);
        //    $("input[id*='" + ctrl1 + "']").val(keyValue1);
        //    $("input[id*='" + ctrl2 + "']").val(keyValue2);
        //    popupwindow.close();
        //    $("input[id*='" + ctrl0 + "']").change();
        //}
        
       //选择部门
        function openwind(){  
            var ctrl0="usedept";
            var ctrl1="";
            var ctrl2=""; 
            layer.open({
                shade: [0.5, '#000', false],
                type: 2,
                offset: '100px',
                area: ['600px', '450px'],
                fix: false, //不固定
                maxmin: false,
                title: ['<i class="fa fa-dedent"></i> 请选择部门', false],
                closeBtn: 1,
                content: '/forms/open/select.aspx?windowid=dept&ctrl0='+ctrl0+'&ctrl1='+ctrl1+'&ctrl2='+ctrl2,
                end: function(e) {
                    
                }
            });            
        }
        //选择归属单位
        function openCostCentre(){  
            var ctrl0="costcentre";
            var ctrl1="costcentrename";
            var ctrl2="";          
    
            layer.open({
                shade: [0.5, '#000', false],
                type: 2,
                offset: '100px',
                area: ['600px', '450px'],
                fix: false, //不固定
                maxmin: false,
                title: ['<i class="fa fa-dedent"></i> 请选择归属单位', false],
                closeBtn: 1,
                content: '/forms/open/select.aspx?windowid=costcentre&ctrl0='+ctrl0+'&ctrl1='+ctrl1,//+'&ctrl2='+ctrl2,
                end: function(e) {
                    
                }
            });            
        }
        //选择PO号
        function selPO(){  
            var ctrl0="pono";
            var ctrl1="porowno";
            var ctrl2="ms";
            var ctrl3="description";
            // window.open( '/forms/open/select.aspx?windowid=bom&ctrl0='+ctrl0+'&ctrl1='+ctrl1+'&ctrl2='+ctrl2);
            layer.open({
                shade: [0.5, '#000', false],
                type: 2,
                offset: '100px',
                area: ['600px', '450px'],
                fix: false, //不固定
                maxmin: false,
                title: ['<i class="fa fa-dedent"></i> 请选择请购单行号', false],
                closeBtn: 1,
                content: '/forms/open/select.aspx?windowid=selectpo&ctrl0='+ctrl0+'&ctrl1='+ctrl1+'&ctrl2='+ctrl2+'&ctrl3='+ctrl3,
                end: function () {
                    if($("#pgino").val()!=""){
                       // $("#btnFuZhu").click();
                    }

                }
            });
        }
        //获取财务签核人员
        function getFinSignPerson(){
            if( $("[id*=finsignperson]").val()==""){
             
                var obj=$("#obj").val(); 
                var mscode=$("#lb").val();
               // alert(mscode)
               
                $.ajax({
                    type: "Post",async: false,
                    url: "pur_rct.aspx/GetFinSignPerson" , 
                    //方法传参的写法一定要对，str为形参的名字,str2为第二个形参的名字
                    //P1:wlh P2： 
                    data: "{'type':'"+obj+"','mscode':'"+mscode+"'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (data) {//返回的数据用data.d获取内容//                      
                        if (data.d == "") {
                            $("[id*=finsignperson]").val("");
                            layer.alert("未获取到财务处理人员.");                              
                        }
                        else {      
                            // alert(data.d);
                            $("[id*=finsignperson]").val(data.d)
                        }                   
                    },
                    error: function (err) {
                        layer.alert(err);
                    }
                });
            }
        }   
        //获取部门领导和分管副总
        function getDeptLeader(){
          
            var _domain="";//$("#domain").val();
            var _dept=$("#DeptName").val();
            // 部门主管
            $.ajax({
                type: "Post",
                async: false,
                url: ctl+"/getDeptLeaderByDept" , //getDeptLeaderByDept
                //方法传参的写法一定要对，str为形参的名字,str2为第二个形参的名字
                //P1:wlh P2： 
                data: "{'domain':'"+_domain+"','dept':'"+_dept+"'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {//返回的数据用data.d获取内容//                      
                    if (data.d == "") {
                        //debugger;
                        //$("[id*=aplid]").val("");
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
            ////分管副总
            //$.ajax({
            //    type: "Post",async: false,
            //    url: ctl+"/getChargeLeaderByUser" , 
            //    //方法传参的写法一定要对，str为形参的名字,str2为第二个形参的名字
            //    //P1:wlh P2： 
            //    data: "{'domain':'"+_domain+"','userid':'"+_dept+"'}",
            //    contentType: "application/json; charset=utf-8",
            //    dataType: "json",
            //    success: function (data) {//返回的数据用data.d获取内容//                      
            //        if (data.d == "") {
                        
            //            $("[id*=aplid]").val("");
            //            layer.alert("未获取到申请人分管领导.请关闭从开试试.");  
                            
            //        }
            //        else {     
            //            //alert(data.d);
            //            $("[id*=deptmfg]").val(data.d)
            //        }                   
            //    },
            //    error: function (err) {
            //        layer.alert(err);
            //    }
            //});

        }  
        //判断此验收类型是否已验收
        function IsExistsRct(){
             
            //debugger;
            if($("#aplno").val()!="") return
            var pono=$("#pono").val(); 
            var porowno=$("#porowno").val();
            var contractno=$("#contractno").val();
            var type=$("#type").val();
                // alert(mscode)
               
            $.ajax({
                type: "Post",async: false,
                url: ctl+"/isExistsRct" , 
                //方法传参的写法一定要对，str为形参的名字,str2为第二个形参的名字
                //P1:wlh P2： 
                data: "{'pono':'"+pono+"','porowno':'"+porowno+"','contractno':'"+contractno+"','type':'"+type+"'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {//返回的数据用data.d获取内容//  
                        
                 //   debugger;
                    if (data.d=="1") {                            
                        layer.alert("此采购记录["+type+"]已验收，请勿重复验收."); 
                        $("#btnSave").prop("disabled","disabled");
                        $("#btnSend").prop("disabled","disabled")
                    }
                    else if (data.d=="0"){      
                        // alert(data.d);
                        layer.alert("此采购记录["+type+"]验收中，请勿重复验收.");
                    }
                    else{
                        $("#btnSave").removeAttr("disabled","disabled");
                        $("#btnSend").removeAttr("disabled","disabled")
                    }
                },
                error: function (err) {
                    layer.alert(err);
                }
            });            
        }
    </script>

    <form id="form1" runat="server" enctype="multipart/form-data"  class="width-1200"  >

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
            <div class="col-md-12   ">
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
        <div class="col-md-12" >
            <div class="row row-container"  >
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
                                                                                                      
                                                });
                                            </script>
                                            <table style="  width: 100%;border:0px solid red">
                                                <tr>
                                                    <td style="width:10%">申请单号：</td>
                                                    <td style="width:60%">
                                                        <asp:TextBox ID="aplno" runat="server" class="input input-readonly"   placeholder="自动产生"  ReadOnly="true" Width="247px" ToolTip="1|0" />
                                                    </td>
                                                    <td style="width:10%">申请日期：</td>
                                                    <td style="width:40%">
                                                        <asp:TextBox ID="CreateDate"  runat="server" class="input input-readonly width-100p"   ReadOnly="True"  />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td >申请人：</td>
                                                    <td>
                                                        <div class="form-inline">
                                                            <asp:TextBox runat="server" ID="CreateById" class="input input-readonly"  Style=" width: 70px" ReadOnly="True"></asp:TextBox>
                                                            <asp:TextBox runat="server" ID="CreateByName"  class="input input-readonly" Style=" width: 70px" ReadOnly="True"></asp:TextBox>
                                                            <asp:TextBox runat="server" ID="DeptName" class="input input-readonly"  Style=" width: 100px" ReadOnly="True" />
                                                        </div>
                                                    </td>
                                                    <td >电话（分机）：
                                                    </td>
                                                    <td >
                                                        <asp:TextBox ID="phone" runat="server" class="input input-readonly width-100p"    />
                                                    </td>
                                                </tr>
                                                <tr class="hidden">
                                                    <td>当前登陆人：</td>
                                                    <td>
                                                        <div class="form-inline">
                                                            <input id="txt_LogUserId" class="input input-readonly" style=" width: 70px" runat="server" readonly="True" />
                                                            <input id="txt_LogUserName" class="input input-readonly"  style=" width: 70px" runat="server" readonly="True" />
                                                            <input id="txt_LogUserDept"  class="input input-readonly" style=" width: 100px" runat="server" readonly="True" />
                                                        </div>
                                                    </td>
                                                    <td class="hidden">签核相关人</td>
                                                    <td class="">                                                        
                                                        <input type="text" id="finsignperson" style="width: 80px; " runat="server" />
                                                        <input type="text" id="deptm" style="width: 80px; " runat="server" />
                                                        <input type="text" id="deptmfg" style="width: 80px; " runat="server" />
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
                            <strong>验收对象基本信息</strong>
                        </div>
                        <div class="panel-body " id="PA">
                            <div class="col-xs-12 col-sm-12  col-md-12 col-lg-12">
                                <div class="form-inline">
                                    <input id="domain" runat="server" name="domain" readonly="true" class="input input-edit width100 hidden"   />
                                    <input id="rid" runat="server" name="rid" readonly="true" class="input input-edit width100 hidden"   />
                                    <input id="lb" runat="server" name="lb" readonly="true" class="input input-edit width100  hidden "   />
                                    <table class="width-100p" border="0" runat="server"  >
                                        <tr style="height:40px">
                                            <td>验收对象：</td>
                                            <td>                                                 
                                                <select   id="obj" runat="server"  name="obj"    class="input input-edit width-100p" >                                                     
                                                </select>
                                            </td>                                                                         
                                            <td class="service">验收类型：</td>
                                            <td class="service">                                                
                                                <select  id="type" runat="server"  readonly="true" name="type" onclick="this.defaultIndex=this.selectedIndex;" onchange="this.selectedIndex=this.defaultIndex;" class="input input-readonly width-100p" >
                                                    <option value=""></option>  
                                                    <option value="预验收">预验收</option>
                                                    <option value="到货验收" selected="selected">到货验收</option>
                                                    <option value="技术终验收">技术终验收</option>
                                                    <option value="质保到期验收">质保到期验收</option>
                                                </select>
                                            </td>
                                            <td> 采购单编号：</td>
                                            <td>                                               
                                                <input id="pono" runat="server" name="pono" readonly="true" class="input input-edit width-100p"   />
                                                <input type="button"  id="btnFuZhu"  onclick="selPO();$('#checkqty').val('1');" value="..." class="hidden"/>
                                            </td>
                                            <td> 采购单行号：</td>
                                            <td>                                               
                                                <input id="porowno" runat="server" name="porowno" readonly="true" class="input input-edit width-100p"   />                                                
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>名称描述：</td>
                                            <td>                                                 
                                                <textarea   id="ms" runat="server"  name="ms"   class="input input-edit width-100p" style="height:45px"  />&nbsp;&nbsp;
                                            </td>                                                                         
                                            <td>规格描述/详细配置：</td>
                                            <td  colspan="1">                                                
                                                <textarea  id="description"  name="description"  runat="server"   class="input input-edit  width-100p "  style="height:45px" />                                                
                                            </td>
                                            <td>合同编号：</td>
                                            <td  colspan="1">                                                
                                                <input  id="contractno"  name="contractno"  runat="server"    class="input input-edit width-100p"  />                                                
                                            </td>
                                            <td>验收数量：</td>
                                            <td><input type="text" id="checkqty" runat="server"  name="checkqty"  class="input input-edit width-100p" /> </td>
                                        </tr>
                                        <tr >
                                            <td  class="service">设备编号：</td>
                                            <td class="service">                                                 
                                                <input type="text" id="equipmentno" runat="server"  name="equipmentno"  class="input input-edit width-100p" />&nbsp;&nbsp;
                                            </td>                                                                         
                                            <td class="service">资产出厂序列号：</td>
                                            <td class="service">                                                
                                                <asp:TextBox  ID="sno"   runat="server"   name="sno" class="input input-edit width-100p"  ></asp:TextBox>
                                            </td>
                                            <td class="service"> 使用部门：</td>
                                            <td class="service">
                                                <input id="usedept" runat="server" readonly="true" name="usedept" class="input input-edit  "  style="width:80%" />
                                                <input type="button"  id="btnseldept"  onclick="openwind()" value="..." />
                                            </td>
                                            <td class="service"> 存放位置：</td>
                                            <td class="service">                                               
                                                
                                                <input id="position" runat="server"  class="input input-edit width-100p"  />
                                                
                                            </td>
                                        </tr>
                                    
                                        <tr style="height:40px">
                                            <td class="service">实际到货日期：</td>
                                            <td class="service">
                                                <input type="text" id="receivedate" runat="server" class="input input-edit width-100p" />&nbsp;&nbsp;
                                            </td>
                                            <td class="service">纸质验收已签收单据号：</td>
                                            <td class="service">
                                                <input type="text" id="isreceiveform" runat="server" class="input input-edit width-100p" />
                                            </td>
                                            <td >验收日期：</td>
                                            <td >
                                                <input type="text" id="checkdate" runat="server" onclick="laydate()" class="input input-edit width-100p" />&nbsp;&nbsp;
                                            </td>
                                            <td>是否已达到预定可使<br />
                                                用状态符合验收条件：</td>
                                            <td>
                                                <select id="isuseful" runat="server" readonly="true" class="input input-edit width-100p">
                                                    <option value=""></option>
                                                    <option value="是">是</option>
                                                    <option value="否">否</option>
                                                </select>
                                            </td>

                                        </tr>

                                        <tr style="height:40px">
                                            <td >成本归属单位：</td>
                                            <td >
                                                <input type="text" id="costcentre" runat="server"  readonly="true"  name="costcentre" class="input input-edit " style="width:40px;display:none"/>
                                                <input type="text" id="costcentrename" runat="server"  readonly="true" name="costcentrename"   class="input input-edit " style="width:80%" />
                                                <input type="button"  id="btnselcc"  onclick="openCostCentre()" value="..." />
                                            </td>
                                            <td >验收附件：</td>
                                            <td  colspan="5">          
                                                                                       
                                                 <asp:FileUpload id="files" runat="server"/>
                                                <asp:HyperLink ID="link_file" runat="server" />
                                            </td>                                                                  

                                        </tr>
                                        <tr style="border-top:1px solid lightgray;height:50px">
                                            <td   style="padding-top:10px;padding-bottom:10px">资产管理编号：</td>
                                            <td   colspan="7">                                                 
                                                <input type="text" id="assetno" runat="server"    class="input input-edit width150" />(财务填)
                                            </td>   
                                                                                                          

                                        </tr>
                                    </table>
                                </div>
                            </div>
                             
                        </div>
                    </div>
                </div>
            </div>
       
            
            <div class="row  row-container "  >
                <div class="col-md-12">
                    <div class="panel panel-infos">
                        <div class="panel-headings" data-toggle="collapse" data-target="#CZRZ">
                        </div>
                        <div class="panel-body ">
                            <table border="0" width="100%"  >
                                <tr>
                                    <td width="60">
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

