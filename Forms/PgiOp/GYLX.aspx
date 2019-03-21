<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="GYLX.aspx.cs" Inherits="Forms_PgiOp_GYLX" 
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
            $("#mestitle").html("【工艺路线审批单】");

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
            
            if(IsGrid_pro=="Y"){
                $("#div_product").css("display","inline-block");
            }
            if(IsGrid_yz=="Y"){
                $("#div_yz").css("display","inline-block");
            }

            $("#CPXX input[id*='projectno']").change(function () {  
                $("#CPXX input[id*='pn']").val("");
                $("#CPXX input[id*='pn_desc']").val("");
                $("#CPXX input[id*='domain']").val("");
                $("#CPXX input[id*='yz_user']").val("");
                $("#CPXX input[id*='zl_user']").val("");
                $("#CPXX input[id*='product_user']").val("");
                $("#CPXX input[id*='bz_user']").val("");

                $("#CPXX input[id*='projectno']").val("");
                $("#CPXX input[id*='pgi_no_t']").val("");
                $("#CPXX input[id*='ver']").val("");

            });

            $("#CPXX input[id*='pgi_no_t']").change(function () {
                CheckVer();
            });

            $("#CPXX input[id*='containgp']").change(function () {
                var typeno=$("#CPXX input[id*='typeno']:checked").val();
                if (typeof(typeno)=="undefined") {
                    layer.alert("请先选择工艺段！");
                    $("#CPXX input[id*='containgp']").removeAttr('checked');
                    return false;
                }

                var op700_flag=false;
                if(typeno=="机加"){
                    $("[id$=gv_d] tr[class*=DataRow]").each(function (index, item) {                        
                        if((eval('op' + index)).GetText()=="OP700"){
                            op700_flag=true;
                        }
                    });
                }
                if(typeno=="压铸"){
                    $("[id$=gv_d_yz] tr[class*=DataRow]").each(function (index, item) {                        
                        if((eval('op_yz' + index)).GetText()=="OP700"){
                            op700_flag=true;
                        }
                    });
                }

                if($("#CPXX input[id*='containgp']:checked").val()=="Y"){
                    if(typeno=="机加" && op700_flag==false){
                        gv_d.PerformCallback("Y");
                    }
                    if(typeno=="压铸" && op700_flag==false){
                        gv_d_yz.PerformCallback("Y");
                    }
                }
                if($("#CPXX input[id*='containgp']:checked").val()=="N"){
                    if(typeno=="机加" && op700_flag==true){
                        gv_d.PerformCallback("N");
                    }
                    if(typeno=="压铸" && op700_flag==true){
                        gv_d_yz.PerformCallback("N");
                    }
                }
            });
            
            $("#CPXX input[id*='typeno']").change(function () {  
                var pgi_no=$("#CPXX input[id*='projectno']").val();
                var pgi_no_t=$("#CPXX input[id*='pgi_no_t']").val();
                var ver=$("#CPXX input[id*='ver']").val();

                if (pgi_no=="" || pgi_no_t=="" || ver=="") {
                    layer.alert("请先选择物料号、工艺流程！");
                    $("#CPXX input[id*='typeno']").removeAttr('checked');
                    return false;
                }

                $("#CPXX input[id*='containgp']").removeAttr('checked');

                if($("#CPXX input[id*='typeno']:checked").val()=="机加"){
                    $("#div_product").css("display","inline-block");
                    gv_d.PerformCallback("机加");
                }else {
                    $("#div_product").css("display","none");
                }

                if($("#CPXX input[id*='typeno']:checked").val()=="压铸"){
                    $("#div_yz").css("display","inline-block");
                    gv_d_yz.PerformCallback("压铸");
                }else {
                    $("#div_yz").css("display","none");
                }

            }); 
           
            set_modifygp_read();

            //$("select[id*='applytype']").change(function(){
            //    var applytype=$("select[id*='applytype']").val();
            //    if (applytype=="删除工艺") {
            //        if($("#CPXX input[id*='typeno']:checked").val()=="机加"){
            //            $("[id$=gv_d] tr[class*=DataRow]").each(function (index, item) {                 
            //                $("#gzzx_i_"+index).removeClass("i_show");$("#gzzx_i_"+index).addClass("i_hidden");  
            //                $("#IsBg_i_"+index).removeClass("i_show");$("#IsBg_i_"+index).addClass("i_hidden");
                       
            //            });
            //        }
            //        if($("#CPXX input[id*='typeno']:checked").val()=="压铸"){
            //            $("[id$=gv_d_yz] tr[class*=DataRow]").each(function (index, item) {                  
            //                $("#gzzx_i_yz_"+index).removeClass("i_show"); $("#gzzx_i_yz_"+index).addClass("i_hidden");   
            //                $("#IsBg_i_yz_"+index).removeClass("i_show"); $("#IsBg_i_yz_"+index).addClass("i_hidden");
                        
            //            });
            //        }
            //    }

            //});
        });

        function set_modifygp_read(){      
            if($("#CPXX input[id*='modifygp']:checked").val()=="Y"){
                if($("#CPXX input[id*='typeno']:checked").val()=="机加"){
                    $("[id$=gv_d] tr[class*=DataRow]").each(function (index, item) {
                        if($(item).find("input[id*=op]").val()!="OP700" && $(item).find("input[id*=op]").val()!="OP600"){                    
                            $("#gzzx_i_"+index).removeClass("i_show");$("#gzzx_i_"+index).addClass("i_hidden");  
                            $("#IsBg_i_"+index).removeClass("i_show");$("#IsBg_i_"+index).addClass("i_hidden");
                        }
                    });
                }
                if($("#CPXX input[id*='typeno']:checked").val()=="压铸"){
                    $("[id$=gv_d_yz] tr[class*=DataRow]").each(function (index, item) {
                        if($(item).find("input[id*=op]").val()!="OP700" && $(item).find("input[id*=op]").val()!="OP600"){                    
                            $("#gzzx_i_yz_"+index).removeClass("i_show"); $("#gzzx_i_yz_"+index).addClass("i_hidden");   
                            $("#IsBg_i_yz_"+index).removeClass("i_show"); $("#IsBg_i_yz_"+index).addClass("i_hidden");
                        }
                    });
                }
            }
        }

        function grid_read_700(){
            $("[id$=gv_d] tr[class*=DataRow]").each(function (index, item) {

                if($(item).find("input[id*=op]").val()=="OP700"){//工序号，工序名称，工序说明
                    $(item).find("table[id*=op]").addClass("dxeTextBox_read");
                    $(item).find("input[id*=op]").attr("readOnly","readOnly").addClass("dxeTextBox_read");

                    $(item).find("input[id*=op_desc]").val('GP12');
                    $(item).find("input[id*=op_remark]").val('GP12');
                }
            });
        }

        function grid_yz_read_700(){
            $("[id$=gv_d_yz] tr[class*=DataRow]").each(function (index, item) {

                if($(item).find("input[id*=op]").val()=="OP700"){//工序号，工序名称，工序说明
                    $(item).find("table[id*=op]").addClass("dxeTextBox_read");
                    $(item).find("input[id*=op]").attr("readOnly","readOnly").addClass("dxeTextBox_read");

                    $(item).find("input[id*=op_desc]").val('GP12');
                    $(item).find("input[id*=op_remark]").val('GP12');
                }
            });
        }

        function gird_keycode(){
            //add keydown事件
            $("#div_product input:text").not("[readonly]").bind("keydown",function(e){
                var theEvent = e || window.event;
                var code = theEvent.keyCode || theEvent.which || theEvent.charCode;
                if (code == 13) {
                    var inputs = $("#div_product").find(":text").not("[readonly]"); // 获取表单中的所有输入框  
                    var idx = inputs.index(this); // 获取当前焦点输入框所处的位置  

                    if (idx == inputs.length - 1) {// 判断是否是最后一个输入框  
                        $("#MainContent_modifyremark").focus();
                        $("#MainContent_modifyremark").select();
                    } else {  
                        inputs[idx + 1].focus(); // 设置焦点  
                        inputs[idx + 1].select(); // 选中文字  
                    }  
                    return false;// 取消默认的提交行为  

                }
            });
        }

        function gird_yz_keycode(){
            $("#div_yz input:text").not("[readonly]").bind("keydown",function(e){
                var theEvent = e || window.event;
                var code = theEvent.keyCode || theEvent.which || theEvent.charCode;
                if (code == 13) {
                    var inputs = $("#div_yz").find(":text").not("[readonly]"); // 获取表单中的所有输入框  
                    var idx = inputs.index(this); // 获取当前焦点输入框所处的位置  

                    if (idx == inputs.length - 1) {// 判断是否是最后一个输入框  
                        $("#MainContent_modifyremark").focus();
                        $("#MainContent_modifyremark").select();
                    } else {  
                        inputs[idx + 1].focus(); // 设置焦点  
                        inputs[idx + 1].select(); // 选中文字  
                    }  
                    return false;// 取消默认的提交行为  

                }
            });
        }

        //提出自定流程 JS 
        function setComment(val) {
            $('#comment', parent.document).val(val);
        }

        //设定表字段状态（可编辑性）
        var tabName="pgi_gylx_main_form";//表名
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
        var tabName2="pgi_gylx_dtl_form";//表名
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
            var url = "/select/select_pgino_gy.aspx";

            layer.open({
                title:'产品信息选择',
                type: 2,
                area: ['900px', '600px'],
                fixed: false, //不固定
                maxmin: true,
                content: url
            }); 
        }

        function setvalue_product(lspgino, lsproductcode, lsproductname, lsmake_factory, lsver, lszl_user, lsyz_user,lsproduct_user,lsbz_user) 
        {
            
            $("#CPXX input[id*='pn']").val(lsproductcode);
            $("#CPXX input[id*='pn_desc']").val(lsproductname);
            $("#CPXX input[id*='domain']").val(lsmake_factory);
            $("#CPXX input[id*='yz_user']").val(lsyz_user);
            $("#CPXX input[id*='zl_user']").val(lszl_user);
            $("#CPXX input[id*='product_user']").val(lsproduct_user);
            $("#CPXX input[id*='bz_user']").val(lsbz_user);

            $("#CPXX input[id*='pgi_no_t']").val(lspgino);
            $("#CPXX input[id*='projectno']").val(lspgino);
            $("#CPXX input[id*='ver']").val(lsver);

            //该条件仅作为测试使用
            //if($("#SQXX input[id*='txt_CreateByDept']").val()=="IT部"){
            //    lsproduct_user=$("#SQXX input[id*='txt_CreateById']").val()+"-"+$("#SQXX input[id*='txt_CreateByName']").val();
            //    $("#CPXX input[id*='yz_user']").val(lsproduct_user);
            //    $("#CPXX input[id*='product_user']").val(lsproduct_user); 
            //    $("#CPXX input[id*='bz_user']").val(lsproduct_user); 
            //}
        }

        function CheckVer(){
            var pgi_no=$("#CPXX input[id*='projectno']").val();
            var pgi_no_t=$("#CPXX input[id*='pgi_no_t']").val();
            var ver=$("#CPXX input[id*='ver']").val();
            var formno=$("#CPXX input[id*='formno']").val();

            if(pgi_no!=pgi_no_t){
                if(pgi_no_t.substr(pgi_no_t.length-3,3)!="-X1" && pgi_no_t.substr(pgi_no_t.length-3,3)!="-X2" && pgi_no_t.substr(pgi_no_t.length-3,3)!="-X3"
                     && pgi_no_t.substr(pgi_no_t.length-3,3)!="-X4" && pgi_no_t.substr(pgi_no_t.length-3,3)!="-X5"){
                    layer.alert("请填写正确的物料号、工艺流程代码");
                    $("#CPXX input[id*='pgi_no_t']").val("");
                    return false;
                }
            }

            $.ajax({
                type: "post",
                url: "GYLX.aspx/CheckVer",
                data: "{'pgi_no':'" + pgi_no + "','pgi_no_t':'" + pgi_no_t + "','ver':'" + ver+ "','formno':'" + formno+ "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,//默认是true，异步；false为同步，此方法执行完在执行下面代码
                success: function (data) {
                    var obj=eval(data.d);

                    if(obj[0].flag!=""){
                        layer.alert(obj[0].flag);
                        $("#CPXX input[id*='pgi_no_t']").val("");
                        return false;
                    }
                }

            });
        }
        
        function Get_wkzx(vi,ty){
            var url = "/select/select_wkzx.aspx?domain="+$("#CPXX input[id*='domain']").val()+"&userid="+$("#SQXX input[id*='txt_CreateById']").val()+"&vi="+vi+"&ty="+ty;

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

                RefreshRow_yz(vi);
            }
        }

        function Get_IsBg(vi,ty){
           
            var url = "/select/select_yn.aspx?vi="+vi+"&ty="+ty;

            layer.open({
                title:'是否报工选择',
                type: 2,
                area: ['250px', '150px'],
                fixed: false, //不固定
                maxmin: true,
                content: url
            }); 
        }

        function setvalue_IsBg(yn,vi,ty){
            if (ty=="") {
                var IsBg= eval('IsBg' + vi);
                IsBg.SetText(yn);
            }
            if (ty=="yz") {
               var IsBg= eval('IsBg_yz' + vi);
                IsBg.SetText(yn);
            }
        }

        function Get_IsXh_op(vi,ty){
           
            layer.open({
                title:'扣料工序选择',
                type: 1,
                closeBtn: 0,//默认1，展示关闭
                area: ['250px', '150px'],
                content: $('#div_TRUE_FALSE'), //这里content是一个DOM，注意：最好该元素要存放在body最外层，否则可能被其它的相对元素所影响
                btn: ['确认', '取消'],
                yes: function(index, layero){//按钮【确认】的回调    
                    var TF_value=$("#div_TRUE_FALSE input[id*='rdb_TF']:checked").val();
                    if (typeof(TF_value)=="undefined") {
                        layer.alert("请选择扣料工序选择！");
                        return false;
                    }
                    if (ty=="") {
                        var IsXh_op= eval('IsXh_op' + vi);
                        IsXh_op.SetText(TF_value);
                    }
                    if (ty=="yz") {
                        var IsXh_op= eval('IsXh_op_yz' + vi);
                        IsXh_op.SetText(TF_value);
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


        function RefreshRow(vi) {
            var op = eval('op' + vi);
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
            JHour.SetText(JHour_value.toFixed(10));

            //单台83%产量
            var col3_value=0;
            if(TjOpSec_value!=0){ col3_value = (12 * 60 * 60 / TjOpSec_value) * 0.83 * EquipmentRate_value; }
            col3.SetText(col3_value.toFixed(0));

            //一人83%产量
            var col4_value=col2_value*col3_value;
            //单人报工数量
            if(Number($.trim(col4.GetText()) == "" ? 0 : $.trim(col4.GetText()))!=col4_value.toFixed(0)){
                col6.SetText(col4_value.toFixed(0));
                col6_value = Number($.trim(col6.GetText()) == "" ? 0 : $.trim(col6.GetText()));//单人报工数量
            }
            col4.SetText(col4_value.toFixed(0));

            //整线班产量
            var col5_value=0;
            if(JSec_value!=0){col5_value =(12 * 60 * 60 / JSec_value) * 0.83 * EquipmentRate_value;}
            col5.SetText(col5_value.toFixed(0));
            
            //单人产出工时
            //var col7_value=(TjOpSec_value*col1_value*col6_value)/3600;

            var col7_value=0;
            if (op.GetText() == "OP600" || op.GetText() == "OP700") {
                col7_value=(TjOpSec_value*col6_value)/3600;
            }else {
                col7_value=(TjOpSec_value*col1_value*col6_value)/3600;
            }

            col7.SetText(col7_value.toFixed(2));
        }

        function RefreshRow_yz(vi) {
            var op = eval('op_yz' + vi);             
            var JgNum = eval('JgNum_yz' + vi); var JgSec = eval('JgSec_yz' + vi); var WaitSec = eval('WaitSec_yz' + vi); var ZjSecc = eval('ZjSecc_yz' + vi);var JtNum = eval('JtNum_yz' + vi); 
            var col1 = eval('col1_yz' + vi); var col2 = eval('col2_yz' + vi);var EquipmentRate = eval('EquipmentRate_yz' + vi);var col6 = eval('col6_yz' + vi);

            var JgNum_value = Number($.trim(JgNum.GetText()) == "" ? 0 : $.trim(JgNum.GetText()));//每次加工数量 
            var op20_vi=-1;//op10的工作中心改变，需要再次更新op20的数据，设备产能及每次加工数量会变化，其他数据也会发生变化
            if(op.GetText() == "OP10" || op.GetText() == "OP20"){

                var weights = eval('weights_yz' + vi); var acupoints = eval('acupoints_yz' + vi);var capacity = eval('capacity_yz' + vi); 
                if(op.GetText()=="OP10"){
                    var gzzx_desc_yz= eval('gzzx_desc_yz' + vi);
                    if(gzzx_desc_yz.GetText()=="A380熔炼炉"){capacity.SetText(1800);}
                    if(gzzx_desc_yz.GetText()=="EN46000熔炼炉"){capacity.SetText(1200);}
                    if(gzzx_desc_yz.GetText()=="EN47100熔炼炉" || gzzx_desc_yz.GetText()=="ADC12熔炼炉"){capacity.SetText(450);}

                    $("#MainContent_gv_d_yz_DXMainTable tr[class*=DataRow]").each(function (index, item) {
                        if((eval('op_yz'+index)).GetText()=="OP20"){op20_vi=index;return false;}
                    });
                }

                if(op.GetText()=="OP20"){
                    var op10_vi=-1;
                    $("#MainContent_gv_d_yz_DXMainTable tr[class*=DataRow]").each(function (index, item) {
                        if((eval('op_yz'+index)).GetText()=="OP10"){op10_vi=index;return false;}
                    });
                    if(op10_vi!=-1){
                        var gzzx_desc_yz= eval('gzzx_desc_yz'+op10_vi);
                        if(gzzx_desc_yz.GetText()=="A380熔炼炉" || gzzx_desc_yz.GetText()=="EN46000熔炼炉"){capacity.SetText(720);}
                        if(gzzx_desc_yz.GetText()=="EN47100熔炼炉" || gzzx_desc_yz.GetText()=="ADC12熔炼炉"){capacity.SetText(450);}
                        //weights.SetText((eval('weights_yz'+op10_vi)).GetText());
                        //acupoints.SetText((eval('acupoints_yz'+op10_vi)).GetText());
                    }
                }
                
                var weights_value = Number($.trim(weights.GetText()) == "" ? 0 : $.trim(weights.GetText()));//每模重量
                var acupoints_value = Number($.trim(acupoints.GetText()) == "" ? 0 : $.trim(acupoints.GetText()));//每模穴数
                var capacity_value = Number($.trim(capacity.GetText()) == "" ? 0 : $.trim(capacity.GetText()));//每小时设备产能

                if(weights_value != 0){ JgNum_value = capacity_value/weights_value*acupoints_value; }
                JgNum.SetText(JgNum_value.toFixed(10));//JgNum.SetText(JgNum_value.toFixed(0));
            }

            var JgSec_value = Number($.trim(JgSec.GetText()) == "" ? 0 : $.trim(JgSec.GetText()));//加工时长(秒)
            var WaitSec_value = Number($.trim(WaitSec.GetText()) == "" ? 0 : $.trim(WaitSec.GetText()));//设备等待时间(秒)
            var ZjSecc_value = Number($.trim(ZjSecc.GetText()) == "" ? 0 : $.trim(ZjSecc.GetText()));//装夹时间(秒)
            var JtNum_value = Number($.trim(JtNum.GetText()) == "" ? 0 : $.trim(JtNum.GetText()));//机器台数
            var col1_value = Number($.trim(col1.GetText()) == "" ? 0 : $.trim(col1.GetText()));//单台需要人数

            if(col1_value!=0){col2.SetText((1/col1_value).toFixed(0));}//本工序一人操作台数

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
            JHour.SetText(JHour_value.toFixed(10));

            //单台83%产量
            var col3_value=0;
            if(TjOpSec_value!=0){ col3_value = (12 * 60 * 60 / TjOpSec_value) * 0.83 * EquipmentRate_value; }
            col3.SetText(col3_value.toFixed(0));

            //一人83%产量
            var col4_value=col2_value*col3_value;
             //单人报工数量
            if(Number($.trim(col4.GetText()) == "" ? 0 : $.trim(col4.GetText()))!=col4_value.toFixed(0)){
                col6.SetText(col4_value.toFixed(0));
                col6_value = Number($.trim(col6.GetText()) == "" ? 0 : $.trim(col6.GetText()));//单人报工数量
            }
            col4.SetText(col4_value.toFixed(0));

            //整线班产量
            var col5_value=0;
            if(JSec_value!=0){col5_value =(12 * 60 * 60 / JSec_value) * 0.83 * EquipmentRate_value;}
            col5.SetText(col5_value.toFixed(0));

            //单人产出工时            
            //var col7_value=(TjOpSec_value*col1_value*col6_value)/3600;

            var col7_value=0;
            if (op.GetText() == "OP600" || op.GetText() == "OP700") {
                col7_value=(TjOpSec_value*col6_value)/3600;
            }else {
                col7_value=(TjOpSec_value*col1_value*col6_value)/3600;
            }
            col7.SetText(col7_value.toFixed(2));

            if(op20_vi!=-1){RefreshRow_yz(op20_vi);}
        }

        function validate(action){
            var flag=true;var msg="";
            
            <%=ValidScript%>

            if($("#CPXX input[id*='projectno']").val()==""){
                msg+="【物料号】不可为空.<br />";
            }
            if($("#CPXX input[id*='pgi_no_t']").val()==""){
                msg+="【工艺流程】不可为空.<br />";
            }
            if(typeof($("#CPXX input[id*='containgp']:checked").val())=="undefined"){
                msg+="【需要GP12】不可为空.<br />";
            }
            if(typeof($("#CPXX input[id*='typeno']:checked").val())=="undefined"){
                msg+="【工艺段】不可为空.<br />";
            }

            if($('#div_product').css('display')=='inline-block'){
                if($("[id$=gv_d] input[id*=op]").length==0){
                    msg+="【工艺工时信息】不可为空.<br />";
                }else {
                    if (!ASPxClientEdit.ValidateGroup("ValueValidationGroup")) {
                        msg+="【工艺工时信息】格式必须正确.<br />";
                    } 
                }
            }

            if($('#div_yz').css('display')=='inline-block'){
                if($("[id$=gv_d_yz] input[id*=op]").length==0){
                    msg+="【工艺工时信息】不可为空.<br />";
                }else {
                    if (!ASPxClientEdit.ValidateGroup("ValueValidationGroup")) {
                        msg+="【工艺工时信息】格式必须正确.<br />";
                    }
                }
            }

            if(msg!=""){  
                flag=false;
                layer.alert(msg);
                return flag;
            }

            var containgp=$("#CPXX input[id*='containgp']:checked").val();
            var typeno=$("#CPXX input[id*='typeno']:checked").val();
            var pgi_no_t=$.trim($("#CPXX input[id*='pgi_no_t']").val());

            if(typeno=="机加"){
                if($("#CPXX input[id*='product_user']").val()==""){msg+="【工艺段】为机加，【产品工程师】不可为空.<br />";}
            }

            if(typeno=="压铸"){
                if($("#CPXX input[id*='yz_user']").val()==""){msg+="【工艺段】为压铸，【压铸工程师】不可为空.<br />";}
            }

            var op700_flag=false;
            if(typeno=="机加"){
                $("[id$=gv_d] tr[class*=DataRow]").each(function (index, item) {                        
                    if((eval('op' + index)).GetText()=="OP700"){
                        op700_flag=true;
                    }
                    //if((eval('op' + index)).GetText()=="OP700" && (eval('pgi_no_t' + index)).GetText()!=pgi_no_t+"-GP12"){
                    //    msg+="工序号OP700对应的工艺流程必须是GP12.<br />";
                    //}
                    if((eval('op' + index)).GetText()=="OP700"){                        
                        if ((eval('pgi_no_t' + index)).GetText()!=pgi_no_t+"-GP12") {
                            msg+="【工序号】OP700对应的【工艺流程】必须是"+pgi_no_t+"-GP12.<br />";
                        }
                        if ($(item).find("input[id*=op_desc]").val()!="GP12") {
                            msg+="【工序号】OP700对应的【工序名称】必须是GP12.<br />";
                        }
                        if ($(item).find("input[id*=op_remark]").val()!="GP12") {
                            msg+="【工序号】OP700对应的【工序说明】必须是GP12.<br />";
                        }
                    }
                    if((eval('op' + index)).GetText()!="OP700" && (eval('pgi_no_t' + index)).GetText()!=pgi_no_t){
                        msg+="【工序号】"+(eval('op' + index)).GetText()+"对应的【工艺流程】必须是"+pgi_no_t+".<br />";
                    }
                });
            }
            if(typeno=="压铸"){
                $("[id$=gv_d_yz] tr[class*=DataRow]").each(function (index, item) {                        
                    if((eval('op_yz' + index)).GetText()=="OP700"){
                        op700_flag=true;
                    }
                    //if((eval('op_yz' + index)).GetText()=="OP700" && (eval('pgi_no_t_yz' + index)).GetText()!=pgi_no_t+"-GP12"){
                    //    msg+="工序号OP700对应的工艺流程必须是GP12.<br />";
                    //}
                    if((eval('op_yz' + index)).GetText()=="OP700"){
                        if ((eval('pgi_no_t_yz' + index)).GetText()!=pgi_no_t+"-GP12") {
                            msg+="【工序号】OP700对应的【工艺流程】必须是"+pgi_no_t+"-GP12.<br />";
                        }
                        if ($(item).find("input[id*=op_desc]").val()!="GP12") {
                            msg+="【工序号】OP700对应的【工序名称】必须是GP12.<br />";
                        }
                        if ($(item).find("input[id*=op_remark]").val()!="GP12") {
                            msg+="【工序号】OP700对应的【工序说明】必须是GP12.<br />";
                        }
                    }
                    if((eval('op_yz' + index)).GetText()!="OP700" && (eval('pgi_no_t_yz' + index)).GetText()!=pgi_no_t){
                        msg+="【工序号"+(eval('op_yz' + index)).GetText()+"对应的【工艺流程】必须是"+pgi_no_t+".<br />";
                    }
                });
            }   

            if(containgp=="Y"){
                if(!op700_flag){msg+="需要GP12 必须填写工序号OP700.<br />";}
            }
            if(containgp=="N"){
                if(op700_flag){msg+="不需要GP12 请删除工序号OP700.<br />";}
            }

            if(action=='submit'){
                if($('#div_product').css('display')=='inline-block'){

                    $("[id$=gv_d] input[id*=pgi_no_t]").each(function (){
                        if( $(this).val()==""){
                            msg+="【工艺流程】不可为空.<br />";
                            return false;
                        }
                    });
                    $("[id$=gv_d] input[id*=op]").each(function (){
                        if( $(this).val()=="" && this.id.indexOf('IsXh_op')==-1 ){
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

                    $("[id$=gv_d_yz] input[id*=pgi_no_t]").each(function (){
                        if( $(this).val()==""){
                            msg+="【工艺流程】不可为空.<br />";
                            return false;
                        }
                    });
                    $("[id$=gv_d_yz] input[id*=op]").each(function (index){
                        if( $(this).val()=="" && this.id.indexOf('IsXh_op')==-1 ){
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
                if($.trim($("#MainContent_applytype").val())==""){
                    msg+="【申请类别】不可为空.<br />";
                }

                if($.trim($("#MainContent_modifyremark").val())==""){
                    msg+="【申请(变更)说明】不可为空.<br />";
                }
            }

            if(msg!=""){  
                flag=false;
                layer.alert(msg);
                return flag;
            }

            
            if(!parent.checkSign()){
                flag=false;return flag;
            }

            if(flag){

                var formno=$("#CPXX input[id*='formno']").val();
                var typeno=$("#CPXX input[id*='typeno']:checked").val();
                var product_user=$("#CPXX input[id*='product_user']").val();
                var yz_user=$("#CPXX input[id*='yz_user']").val();
                var bz_user=$("#CPXX input[id*='bz_user']").val();

                var pgi_no=$("#CPXX input[id*='projectno']").val();
                var pgi_no_t=$("#CPXX input[id*='pgi_no_t']").val();
                var ver=$("#CPXX input[id*='ver']").val();
                var domain=$("#CPXX input[id*='domain']").val();

                $.ajax({
                    type: "post",
                    url: "GYLX.aspx/CheckData",
                    data: "{'typeno':'" + typeno + "','product_user':'" + product_user + "','yz_user':'" + yz_user + "','bz_user':'" + bz_user + "','pgi_no':'" + pgi_no 
                        + "','pgi_no_t':'" + pgi_no_t + "','ver':'" + ver + "','formno':'" + formno + "','domain':'" + domain + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    async: false,//默认是true，异步；false为同步，此方法执行完在执行下面代码
                    success: function (data) {
                        var obj=eval(data.d);

                        if(obj[0].manager_flag!=""){ msg+=obj[0].manager_flag; }
                        if(obj[0].pgino_flag!=""){ msg+=obj[0].pgino_flag; }

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
        .btntaskEnd{ background:url(/Images/ico/del.gif) no-repeat  0.3em;
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
        .i_hidden{
            display:none;
        }
         .i_show{
            display:inline-block;
        }         
        .dxeTextBox_read{
            border:none !important ;
        }
        input[type=checkbox], input[type=radio]{
            margin:7px 0px 0px;
        }
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
                <input id="btntaskEnd" type="button" value="终止" onclick="parent.taskEnd(true);" class="btn btn-default btn-xs btntaskEnd" />
                <input id="btnshowProcess" type="button" value="查看流程" onclick="parent.showProcess(true);" class="btn btn-default btn-xs btnshowProcess" />
            </div>
        </div>
    </div>

    <div id="div_TRUE_FALSE" style="display:none;">
        <asp:RadioButtonList ID="rdb_TF" runat="server" RepeatDirection="Horizontal" Height="20px" Width="120px" style="margin-left:10px; margin-top:10px;">
            <asp:ListItem Text="是" Value="是"></asp:ListItem>
            <asp:ListItem Text="否" Value=""></asp:ListItem>
        </asp:RadioButtonList>
    </div>

    <div class="col-md-12" >  

        <div class="row row-container">
            <div class="panel panel-info">
                <div class="panel-heading" data-toggle="collapse" data-target="#SQXX">
                    <strong>申请人信息</strong><%--&nbsp;&nbsp;<input id="btn_modify" type="button" value="IT修改工程师" />--%>
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
                            <asp:TextBox ID="txt_pn" runat="server" style="display:none;"></asp:TextBox>
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
                    &nbsp;&nbsp;<font style="color:red; font-size:9px;">提示：与上一版本比较，删除行（灰色），新增行（红色），修改行（黄色）</font>
                </div>
                <div class="panel-body " id="GYGS">
                    <div class="col-xs-12 col-sm-12  col-md-12 col-lg-12">
                        <div style="padding: 2px 5px 5px 0px">                
                            
                        </div>
                        <asp:UpdatePanel runat="server" ID="p1" UpdateMode="Conditional" >
                            <ContentTemplate>
                                <asp:Button ID="btndel" runat="server" Text="删除" class="btn btn-default" style="width:60px; height:32px;"  OnClick="btndel_Click" />

                                 <dx:aspxgridview ID="gv_d" runat="server" AutoGenerateColumns="False" KeyFieldName="numid" Theme="MetropolisBlue" OnCustomCallback="gv_d_CustomCallback" 
                                      OnRowCommand="gv_d_RowCommand" ClientInstanceName="gv_d"  EnableTheming="True"  OnDataBound="gv_d_DataBound" OnHtmlRowCreated="gv_d_HtmlRowCreated"> 
                                     <ClientSideEvents EndCallback="function(s, e) {  gird_keycode();grid_read_700();}"  />  
                                    <SettingsPager PageSize="1000"></SettingsPager>
                                    <Settings ShowFooter="True" />
                                    <SettingsBehavior AllowSelectByRowClick="False" AllowDragDrop="False" AllowSort="False" />
                                    <Columns>
                                        <dx:GridViewCommandColumn SelectAllCheckboxMode="Page" ShowClearFilterButton="true" ShowSelectCheckbox="true" Name="Sel" Width="30" VisibleIndex="1"></dx:GridViewCommandColumn>
                                        <dx:GridViewDataTextColumn Caption="工艺流程" FieldName="pgi_no_t" Width="110px" VisibleIndex="3">
                                            <Settings AllowCellMerge="False" />
                                            <DataItemTemplate>     
                                                 <dx:ASPxTextBox ID="pgi_no_t" Width="110px" runat="server" Value='<%# Eval("pgi_no_t")%>' 
                                                                ClientInstanceName='<%# "pgi_no_t"+Container.VisibleIndex.ToString() %>' Border-BorderWidth="0" ReadOnly="true" ></dx:ASPxTextBox>                    
                                            </DataItemTemplate>   
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="工序号" FieldName="op" Width="45px" VisibleIndex="4">
                                            <Settings AllowCellMerge="False" />
                                            <DataItemTemplate>                
                                                <dx:ASPxTextBox ID="op" Width="45px" runat="server" Value='<%# Eval("op")%>' 
                                                    ClientInstanceName='<%# "op"+Container.VisibleIndex.ToString() %>'>
                                                    <ValidationSettings ValidationGroup="ValueValidationGroup" Display="Dynamic" ErrorTextPosition="Bottom">
                                                        <RegularExpression ErrorText="请输入正确格式！" ValidationExpression="^[O]+[P]+\d{3}$" />
                                                    </ValidationSettings>
                                                </dx:ASPxTextBox>                
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
                                                        <td><i id="gzzx_i_<%#Container.VisibleIndex.ToString() %>" class="fa fa-search <% =ViewState["gzzx_i"].ToString() == "Y" ? "i_hidden" : "i_show" %>" 
                                                            onclick="Get_wkzx(<%# Container.VisibleIndex %>,'')"></i></td>
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
                                        <dx:GridViewDataTextColumn Caption="是否报<br />工(Y/N)" FieldName="IsBg" Width="35px" VisibleIndex="9">
                                            <Settings AllowCellMerge="False" />
                                            <DataItemTemplate>     
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <dx:ASPxTextBox ID="IsBg" Width="35px" runat="server" Value='<%# Eval("IsBg")%>' 
                                                                ClientInstanceName='<%# "IsBg"+Container.VisibleIndex.ToString() %>' Border-BorderWidth="0"   ReadOnly="true">
                                                            </dx:ASPxTextBox>
                                                        </td>
                                                        <td><i id="IsBg_i_<%#Container.VisibleIndex.ToString() %>" class="fa fa-search <% =ViewState["IsBg_i"].ToString() == "Y" ? "i_hidden" : "i_show" %>" 
                                                            onclick="Get_IsBg(<%# Container.VisibleIndex %>,'')"></i></td>
                                                    </tr>
                                                </table>              
                                            </DataItemTemplate>
                                        </dx:GridViewDataTextColumn>
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
                                        <dx:GridViewDataTextColumn Caption="单件工<br />时(小时)" FieldName="JHour" Width="90px" VisibleIndex="17"><%--Width="55px"--%>
                                            <DataItemTemplate>
                                                <dx:ASPxTextBox ID="JHour" Width="90px" runat="server" Value='<%# Eval("JHour")%>' 
                                                    ClientInstanceName='<%# "JHour"+Container.VisibleIndex.ToString() %>' Border-BorderWidth="0" ReadOnly="true">
                                                </dx:ASPxTextBox> 
                                            </DataItemTemplate>
                                             <PropertiesTextEdit DisplayFormatString="{0:N10}"></PropertiesTextEdit>
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="单台需<br />要人数" FieldName="col1" Width="40px" VisibleIndex="18">
                                            <Settings AllowCellMerge="False" />
                                            <DataItemTemplate>                
                                                <dx:ASPxTextBox ID="col1" Width="40px" runat="server" Value='<%# Eval("col1")%>'
                                                    ClientSideEvents-ValueChanged='<%# "function(s,e){RefreshRow("+Container.VisibleIndex+");}" %>' 
                                                    ClientInstanceName='<%# "col1"+Container.VisibleIndex.ToString() %>'>
                                                     <ValidationSettings ValidationGroup="ValueValidationGroup" Display="Dynamic" ErrorTextPosition="Bottom">
                                                        <RegularExpression ErrorText="请输入最多两位小数位的数字！" ValidationExpression="^\d+(\.\d{1,2})?$" />
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
                                                        <RegularExpression ErrorText="请输入0或1或0~1之间的小数！" ValidationExpression="^(0.\d+|0|1)$" />
                                                    </ValidationSettings>
                                                </dx:ASPxTextBox>                                                    
                                            </DataItemTemplate>   
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="单台83%<br />产量" FieldName="col3" Width="40px" VisibleIndex="20">
                                            <DataItemTemplate>
                                                <dx:ASPxTextBox ID="col3" Width="40px" runat="server" Value='<%# Eval("col3")%>' 
                                                    ClientInstanceName='<%# "col3"+Container.VisibleIndex.ToString() %>' Border-BorderWidth="0" ReadOnly="true">
                                                </dx:ASPxTextBox> 
                                            </DataItemTemplate>
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="一人83%<br />产量" FieldName="col4" Width="40px" VisibleIndex="21">
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
                                        <dx:GridViewDataTextColumn Caption="产出<br />工时" FieldName="col7" Width="40px" VisibleIndex="24">
                                            <DataItemTemplate>
                                                <dx:ASPxTextBox ID="col7" Width="40px" runat="server" Value='<%# Eval("col7")%>' 
                                                    ClientInstanceName='<%# "col7"+Container.VisibleIndex.ToString() %>' Border-BorderWidth="0" ReadOnly="true">
                                                </dx:ASPxTextBox> 
                                            </DataItemTemplate>
                                            <PropertiesTextEdit DisplayFormatString="{0:N2}"></PropertiesTextEdit>
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="包材扣<br />料工序" FieldName="IsXh_op" Width="35px" VisibleIndex="25">
                                            <DataItemTemplate>     
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <dx:ASPxTextBox ID="IsXh_op" Width="35px" runat="server" Value='<%# Eval("IsXh_op")%>' 
                                                                ClientInstanceName='<%# "IsXh_op"+Container.VisibleIndex.ToString() %>' Border-BorderWidth="0"   ReadOnly="true">
                                                            </dx:ASPxTextBox>
                                                        </td>
                                                        <td><i id="IsXh_op_i_<%#Container.VisibleIndex.ToString() %>" class="fa fa-search <% =ViewState["IsXh_op_i"].ToString() == "Y" ? "i_hidden" : "i_show" %>" 
                                                            onclick="Get_IsXh_op(<%# Container.VisibleIndex %>,'')"></i></td>
                                                    </tr>
                                                </table>              
                                            </DataItemTemplate>
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
                                        <dx:GridViewDataTextColumn FieldName="typeno" Width="0px" >
                                             <HeaderStyle CssClass="hidden" />
                                             <CellStyle CssClass="hidden"></CellStyle>
                                             <FooterCellStyle CssClass="hidden"></FooterCellStyle>
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="domain" Width="0px">
                                             <HeaderStyle CssClass="hidden" />
                                             <CellStyle CssClass="hidden"></CellStyle>
                                             <FooterCellStyle CssClass="hidden"></FooterCellStyle>
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="pn" Width="0px">
                                             <HeaderStyle CssClass="hidden" />
                                             <CellStyle CssClass="hidden"></CellStyle>
                                             <FooterCellStyle CssClass="hidden"></FooterCellStyle>
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="pgi_no" Width="0px">
                                             <HeaderStyle CssClass="hidden" />
                                             <CellStyle CssClass="hidden"></CellStyle>
                                             <FooterCellStyle CssClass="hidden"></FooterCellStyle>
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="ver" Width="0px">
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
                    &nbsp;&nbsp;<font style="color:red; font-size:9px;">提示：与上一版本比较，删除行（灰色），新增行（红色），修改行（黄色）</font>
                </div>
                <div class="panel-body " id="GYGS_YZ">
                    <div class="col-xs-12 col-sm-12  col-md-12 col-lg-12">
                        <div style="padding: 2px 5px 5px 0px">                
                            
                        </div>
                        <asp:UpdatePanel runat="server" ID="p2" UpdateMode="Conditional" >
                            <ContentTemplate>
                                <asp:Button ID="btn_del_yz" runat="server" Text="删除" class="btn btn-default" style="width:60px; height:32px;"  OnClick="btn_del_yz_Click" />

                                <dx:aspxgridview ID="gv_d_yz" runat="server" AutoGenerateColumns="False" KeyFieldName="numid" Theme="MetropolisBlue" OnCustomCallback="gv_d_yz_CustomCallback" 
                                      OnRowCommand="gv_d_yz_RowCommand" ClientInstanceName="gv_d_yz"  EnableTheming="True"  OnDataBound="gv_d_yz_DataBound" OnHtmlRowCreated="gv_d_yz_HtmlRowCreated">    
                                    <ClientSideEvents EndCallback="function(s, e) {  gird_yz_keycode();grid_yz_read_700();}"  />                                                                 
                                    <SettingsPager PageSize="1000"></SettingsPager>
                                    <Settings ShowFooter="True" />
                                    <SettingsBehavior AllowSelectByRowClick="False" AllowDragDrop="False" AllowSort="False" />
                                    <Columns>
                                        <dx:GridViewCommandColumn SelectAllCheckboxMode="Page" ShowClearFilterButton="true" ShowSelectCheckbox="true" Name="Sel" Width="30" VisibleIndex="1"></dx:GridViewCommandColumn>                                        
                                        <dx:GridViewDataTextColumn Caption="工艺流程" FieldName="pgi_no_t" Width="110px" VisibleIndex="3">
                                            <Settings AllowCellMerge="False" />
                                            <DataItemTemplate>     
                                                 <dx:ASPxTextBox ID="pgi_no_t" Width="110px" runat="server" Value='<%# Eval("pgi_no_t")%>' 
                                                                ClientInstanceName='<%# "pgi_no_t_yz"+Container.VisibleIndex.ToString() %>' Border-BorderWidth="0" ReadOnly="true" ></dx:ASPxTextBox>                    
                                            </DataItemTemplate>   
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="工序号" FieldName="op" Width="45px" VisibleIndex="4">
                                            <Settings AllowCellMerge="False" />
                                            <DataItemTemplate>                
                                                <dx:ASPxTextBox ID="op" Width="45px" runat="server" Value='<%# Eval("op")%>' 
                                                    ClientSideEvents-ValueChanged='<%# "function(s,e){RefreshRow_yz("+Container.VisibleIndex+");}" %>'
                                                     ClientInstanceName='<%# "op_yz"+Container.VisibleIndex.ToString() %>' >
                                                    <ValidationSettings ValidationGroup="ValueValidationGroup" Display="Dynamic" ErrorTextPosition="Bottom">
                                                        <RegularExpression ErrorText="请输入正确格式！" ValidationExpression="^[O]+[P]+\d{2,3}$" />
                                                    </ValidationSettings>
                                                </dx:ASPxTextBox>                
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
                                                        <td><i id="gzzx_i_yz_<%#Container.VisibleIndex.ToString() %>" class="fa fa-search <% =ViewState["gzzx_i_yz"].ToString() == "Y" ? "i_hidden" : "i_show" %>" onclick="Get_wkzx(<%# Container.VisibleIndex %>,'yz')"></i></td>
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
                                        <dx:GridViewDataTextColumn Caption="是否报<br />工(Y/N)" FieldName="IsBg" Width="35px" VisibleIndex="9">
                                            <Settings AllowCellMerge="False" />
                                            <DataItemTemplate>     
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <dx:ASPxTextBox ID="IsBg" Width="35px" runat="server" Value='<%# Eval("IsBg")%>' 
                                                                ClientInstanceName='<%# "IsBg_yz"+Container.VisibleIndex.ToString() %>' Border-BorderWidth="0"   ReadOnly="true">
                                                            </dx:ASPxTextBox>
                                                        </td>
                                                        <td><i id="IsBg_i_yz_<%#Container.VisibleIndex.ToString() %>" class="fa fa-search <% =ViewState["IsBg_i_yz"].ToString() == "Y" ? "i_hidden" : "i_show" %>" 
                                                            onclick="Get_IsBg(<%# Container.VisibleIndex %>,'yz')"></i></td>
                                                    </tr>
                                                </table>                  
                                            </DataItemTemplate>
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="压铸每模<br />重量(kg)" FieldName="weights" Width="60px" VisibleIndex="10">
                                            <Settings AllowCellMerge="False"/>
                                            <DataItemTemplate>
                                                <dx:ASPxTextBox ID="weights" Width="60px" runat="server" Value='<%# Eval("weights")%>' 
                                                    ClientSideEvents-ValueChanged='<%# "function(s,e){RefreshRow_yz("+Container.VisibleIndex+");}" %>'
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
                                                    ClientSideEvents-ValueChanged='<%# "function(s,e){RefreshRow_yz("+Container.VisibleIndex+");}" %>'
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
                                                    ClientSideEvents-ValueChanged='<%# "function(s,e){RefreshRow_yz("+Container.VisibleIndex+");}" %>'
                                                    ClientInstanceName='<%# "capacity_yz"+Container.VisibleIndex.ToString() %>' Border-BorderWidth="0" ReadOnly="true">
                                                </dx:ASPxTextBox>
                                            </DataItemTemplate>        
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="每次加<br />工数量" FieldName="JgNum" Width="80px" VisibleIndex="10">
                                            <Settings AllowCellMerge="False"/>
                                            <DataItemTemplate>
                                                <dx:ASPxTextBox ID="JgNum" Width="80px" runat="server" Value='<%# Eval("JgNum")%>' 
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
                                        <dx:GridViewDataTextColumn Caption="单件工<br />时(小时)" FieldName="JHour" Width="90px" VisibleIndex="17"><%--Width="55px"--%>
                                            <DataItemTemplate>
                                                <dx:ASPxTextBox ID="JHour" Width="90px" runat="server" Value='<%# Eval("JHour")%>' 
                                                    ClientInstanceName='<%# "JHour_yz"+Container.VisibleIndex.ToString() %>' Border-BorderWidth="0" ReadOnly="true">
                                                </dx:ASPxTextBox> 
                                            </DataItemTemplate>
                                             <PropertiesTextEdit DisplayFormatString="{0:N10}"></PropertiesTextEdit>
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="单台需<br />要人数" FieldName="col1" Width="40px" VisibleIndex="18">
                                            <Settings AllowCellMerge="False" />
                                            <DataItemTemplate>                
                                                <dx:ASPxTextBox ID="col1" Width="40px" runat="server" Value='<%# Eval("col1")%>'
                                                    ClientSideEvents-ValueChanged='<%# "function(s,e){RefreshRow_yz("+Container.VisibleIndex+");}" %>' 
                                                    ClientInstanceName='<%# "col1_yz"+Container.VisibleIndex.ToString() %>'>
                                                     <ValidationSettings ValidationGroup="ValueValidationGroup" Display="Dynamic" ErrorTextPosition="Bottom">
                                                        <RegularExpression ErrorText="请输入最多两位小数位的数字！" ValidationExpression="^\d+(\.\d{1,2})?$" />
                                                    </ValidationSettings>
                                                </dx:ASPxTextBox>                                             
                                            </DataItemTemplate>   
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="本工序一人<br />操作台数" FieldName="col2" Width="60px" VisibleIndex="19">
                                            <Settings AllowCellMerge="False" />
                                            <DataItemTemplate>                
                                                <dx:ASPxTextBox ID="col2" Width="60px" runat="server" Value='<%# Eval("col2")%>'
                                                    ClientSideEvents-ValueChanged='<%# "function(s,e){RefreshRow_yz("+Container.VisibleIndex+");}" %>' 
                                                    ClientInstanceName='<%# "col2_yz"+Container.VisibleIndex.ToString() %>' Border-BorderWidth="0" ReadOnly="true">
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
                                                        <RegularExpression ErrorText="请输入0或1或0~1之间的小数！" ValidationExpression="^(0.\d+|0|1)$" />
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
                                        <dx:GridViewDataTextColumn Caption="产出<br />工时" FieldName="col7" Width="40px" VisibleIndex="24">
                                            <DataItemTemplate>
                                                <dx:ASPxTextBox ID="col7" Width="40px" runat="server" Value='<%# Eval("col7")%>' 
                                                    ClientInstanceName='<%# "col7_yz"+Container.VisibleIndex.ToString() %>' Border-BorderWidth="0" ReadOnly="true">
                                                </dx:ASPxTextBox> 
                                            </DataItemTemplate>
                                            <PropertiesTextEdit DisplayFormatString="{0:N2}"></PropertiesTextEdit>
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="包材扣<br />料工序" FieldName="IsXh_op" Width="35px" VisibleIndex="25">
                                            <DataItemTemplate>     
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <dx:ASPxTextBox ID="IsXh_op" Width="35px" runat="server" Value='<%# Eval("IsXh_op")%>' 
                                                                ClientInstanceName='<%# "IsXh_op_yz"+Container.VisibleIndex.ToString() %>' Border-BorderWidth="0"   ReadOnly="true">
                                                            </dx:ASPxTextBox>
                                                        </td>
                                                        <td><i id="IsXh_op_i_yz_<%#Container.VisibleIndex.ToString() %>" class="fa fa-search <% =ViewState["IsXh_op_i_yz"].ToString() == "Y" ? "i_hidden" : "i_show" %>" 
                                                            onclick="Get_IsXh_op(<%# Container.VisibleIndex %>,'yz')"></i></td>
                                                    </tr>
                                                </table>              
                                            </DataItemTemplate>
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
                                        <dx:GridViewDataTextColumn FieldName="typeno" Width="0px" >
                                             <HeaderStyle CssClass="hidden" />
                                             <CellStyle CssClass="hidden"></CellStyle>
                                             <FooterCellStyle CssClass="hidden"></FooterCellStyle>
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="domain" Width="0px">
                                             <HeaderStyle CssClass="hidden" />
                                             <CellStyle CssClass="hidden"></CellStyle>
                                             <FooterCellStyle CssClass="hidden"></FooterCellStyle>
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="pn" Width="0px">
                                             <HeaderStyle CssClass="hidden" />
                                             <CellStyle CssClass="hidden"></CellStyle>
                                             <FooterCellStyle CssClass="hidden"></FooterCellStyle>
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="pgi_no" Width="0px">
                                             <HeaderStyle CssClass="hidden" />
                                             <CellStyle CssClass="hidden"></CellStyle>
                                             <FooterCellStyle CssClass="hidden"></FooterCellStyle>
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="ver" Width="0px">
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
                    <%--<strong>申请(变更)说明</strong>--%>
                </div>
                <div class="panel-body " id="bgjl">
                    <table border="0"  width="100%"  >
                        <tr>
                            <td width="100px" ><font color="red">*</font><label>申请类别：</label></td>
                            <td>
                                <asp:DropDownList ID="applytype" CssClass="linewrite" runat="server" Width="130px" Height="25px">
                                    <asp:ListItem Value="" Text=""></asp:ListItem>
                                    <asp:ListItem Value="新增/删除工序" Text="新增/删除工序"></asp:ListItem>
                                    <asp:ListItem Value="工作中心变更" Text="工作中心变更"></asp:ListItem>
                                    <asp:ListItem Value="是否报工变更" Text="是否报工变更"></asp:ListItem>
                                    <asp:ListItem Value="人员逻辑更改" Text="人员逻辑更改"></asp:ListItem>
                                    <asp:ListItem Value="仅更改工时" Text="仅更改工时"></asp:ListItem>
                                    <asp:ListItem Value="新增工艺" Text="新增工艺"></asp:ListItem>
                                    <asp:ListItem Value="仅修改扣料工序" Text="仅修改扣料工序"></asp:ListItem>
                                    <asp:ListItem Value="删除工艺" Text="删除工艺"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td width="100px" ><font color="red">*</font><label>申请(变更)说明：</label></td>
                            <td>
                                <%--<textarea id="modifyremark" cols="20" rows="2"  class="form-control" runat="server"></textarea>--%>
                                <asp:TextBox ID="modifyremark" runat="server" TextMode="MultiLine" class="form-control"></asp:TextBox>
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





