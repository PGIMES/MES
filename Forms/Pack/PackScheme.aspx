<%@ Page Title="包装方案申请单" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="PackScheme.aspx.cs" Inherits="Forms_Pack_PackScheme" %>

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

        function getQueryString(name) {
            var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)", "i");
            var r = decodeURI(window.location.search).substr(1).match(reg);
            if (r != null) return unescape(r[2]); return null;
        }
        var stepid = getQueryString("stepid");
        var state = getQueryString("state");

        $(document).ready(function () {
            $("#mestitle").html("【包装方案申请单】");//<a href='/userguide/TGuide.pps' target='_blank' class='h5' style='color:red'>使用说明</a>

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

            ////数量/层
            //$("#zxXX input[id*='bzx_sl_c']").change(function(){
            //    RefreshMain();
            //});
            ////层数/箱
            //$("#zxXX input[id*='bzx_cs_x']").change(function(){
            //    RefreshMain();
            //});
            ////箱数/层
            //$("#zxXX input[id*='bzx_xs_c']").change(function(){
            //    RefreshMain();
            //});
            ////层/托
            //$("#zxXX input[id*='bzx_c_t']").change(function(){
            //    RefreshMain();
            //});

            var typeno=$("#ljXX input[id*='typeno']").val();
            if (state=='edit'|| (typeno!="" && typeno!="新增")) {
                RefreshMain();
                RefreshRow();
                $("#ljXX i[id*=part_i]").removeClass("i_show").addClass("i_hidden");
                //2019.10.30注释，修改的时候可以修改
                //$("#zxXX i[id*=bzx_part_i]").removeClass("i_show").addClass("i_hidden");
            }

            if (state=='edit'){//加载附件
                if($("#<%=ip_filelist.ClientID%>").val()!=""){
                    var s=$("#<%=ip_filelist.ClientID%>").val().split(';');
                    for(var i=0;i<s.length;i++){
                        uploadedFiles.push(s[i]);
                        bind_table(s[i].split(','));
                    }
                }
            
                if($("#<%=ip_filelist_2.ClientID%>").val()!=""){
                    var s_2=$("#<%=ip_filelist_2.ClientID%>").val().split(';');
                    for(var i=0;i<s_2.length;i++){
                        uploadedFiles_2.push(s_2[i]);
                        bind_table_2(s_2[i].split(','));
                    }
                }
    
                if($("#<%=ip_filelist_3.ClientID%>").val()!=""){
                    var s_3=$("#<%=ip_filelist_3.ClientID%>").val().split(';');
                    for(var i=0;i<s_3.length;i++){
                        uploadedFiles_3.push(s_3[i]);
                        bind_table_3(s_3[i].split(','));
                    }
                }
            }
        });

        //提出自定流程 JS 
        function setComment(val) {
            $('#comment', parent.document).val(val);
        }

        function gv_SelectionChanged(s, e) {
            gv_color();
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
        var tabName="pgi_packscheme_main_form";//表名
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
        var tabName2="pgi_packscheme_dtl_form";//表名
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
            var url = "/select/select_pack_part.aspx";

            layer.open({
                title:'零件选择',
                type: 2,
                area: ['950px', '500px'],
                fixed: false, //不固定
                maxmin: true,
                content: url
            }); 
        }

        function setvalue_part(domain, part, site, ship, ad_name, custpart, ljzl,nyl,xs_price,klgx) 
        {            
            $("#ljXX input[type!=hidden][id*='ver']").val('A0');
            $("#ljXX input[id*='typeno']").val('新增');

            $("#ljXX [id*='domain']").val(domain);
            $("#ljXX input[id*='part']").val(part);
            $("#ljXX input[id*='site']").val(site);
            $("#ljXX input[id*='ship']").val(ship);
            $("#ljXX input[id*='custname']").val(ad_name);
            $("#ljXX input[id*='custpart']").val(custpart);
            $("#ljXX input[id*='ljzl']").val(ljzl);
            $("#ljXX input[id*='nyl']").val(nyl);
            $("#cbXX input[id*='cbfx_xs_price']").val(xs_price);
            $("#ljXX input[id*='klgx']").val(klgx);

            RefreshMain();
        }

        function Get_bzx_part(){
            var url = "/select/select_pack_part_bzx.aspx";

            layer.open({
                title:'装箱选择',
                type: 2,
                area: ['800px', '500px'],
                fixed: false, //不固定
                maxmin: true,
                content: url
            }); 
        }

        function setvalue_bzx_part(domain, part, gg, cc, zl, XC, CT,XT) 
        {    
            if(zl==""){zl=0}
            $("#zxXX input[id*='bzx_part']").val(part);
            $("#zxXX input[id*='bzx_w']").val(zl);//包装箱子重量
            $("#zxXX input[id*='bzx_gg']").val(gg);
            $("#zxXX input[id*='bzx_cc']").val(cc);
            $("#zxXX input[id*='bzx_xs_c']").val(XC);//箱数/层
            $("#zxXX input[id*='bzx_c_t']").val(CT);//层/托
            $("#zxXX input[id*='bzx_xs_t']").val(XT);//箱数/托=(箱数/层*层/托)

            RefreshMain();
        }

        function Get_bm(vi){
            var url = "/select/select_pack_part_bc.aspx?vi="+vi;

            layer.open({
                title:'包装材料明细',
                type: 2,
                area: ['1000px', '500px'],
                fixed: false, //不固定
                maxmin: true,
                content: url
            }); 
        }

        function setvalue_bm(vi,domain_v, bm_v, mc_v, cc_v, dz_v, cl_v, gys_v,dj_v,bclb_v) {
            if ($.trim(bclb_v)=="") {
                layer.alert("包装箱编码"+bm_v+"对应的包材类别为空，请重新选择！"); 
            }else {    
                var domain= eval('domain' + vi);domain.SetText(domain_v);
                var bm= eval('bm' + vi);bm.SetText(bm_v);
                var mc= eval('mc' + vi);mc.SetText(mc_v);
                var cc= eval('cc' + vi);cc.SetText(cc_v);
                var dz= eval('dz' + vi);dz.SetText(dz_v);
                var cl= eval('cl' + vi);cl.SetText(cl_v);
                var gys= eval('gys' + vi);gys.SetText(gys_v);
                var dj= eval('dj' + vi);dj.SetText(dj_v);
                var bclb= eval('bclb' + vi);bclb.SetText(bclb_v);

                RefreshRow();
            }

        }

        function RefreshMain(){                 
            var ljzl=Number($.trim($("#ljXX input[id*='ljzl']").val()) == "" ? 0 : $.trim($("#ljXX input[id*='ljzl']").val())); // 零件重量  

            var zl=Number($.trim($("#zxXX input[id*='bzx_w']").val()) == "" ? 0 : $.trim($("#zxXX input[id*='bzx_w']").val())); // 包装箱子重量  
            
            var sl_c=Number($.trim($("#zxXX input[id*='bzx_sl_c']").val()) == "" ? 0 : $.trim($("#zxXX input[id*='bzx_sl_c']").val())); //数量/层
            var cs_x=Number($.trim($("#zxXX input[id*='bzx_cs_x']").val()) == "" ? 0 : $.trim($("#zxXX input[id*='bzx_cs_x']").val())); //层数/箱

            var xs_c=Number($.trim($("#zxXX input[id*='bzx_xs_c']").val()) == "" ? 0 : $.trim($("#zxXX input[id*='bzx_xs_c']").val())); //箱数/层
            var c_t=Number($.trim($("#zxXX input[id*='bzx_c_t']").val()) == "" ? 0 : $.trim($("#zxXX input[id*='bzx_c_t']").val())); // 层/托      

            //箱数/托=(箱数/层*层/托)   
            var xs_t=xs_c*c_t;
            $("#zxXX input[id*='bzx_xs_t']").val(xs_t);

            //数量/箱=(数量/层*层数/箱)   
            var sl_x=sl_c*cs_x;
            $("#zxXX input[id*='bzx_sl_x']").val(sl_x);

            //数量/托=(箱数/托*数量/箱)
            var sl_t=xs_t*sl_x;
            $("#zxXX input[id*='bzx_sl_t']").val(sl_t);

            //净重/箱(KG)=(单件重量*数量/箱)	
            var jz_x=(ljzl*sl_x).toFixed(2);
            $("#zxXX input[id*='bzx_jz_x']").val(jz_x);

            //毛重/箱(KG)=(净重+箱子重量)
            var mz_x=(Number(jz_x)+zl).toFixed(2);
            $("#zxXX input[id*='bzx_mz_x']").val(mz_x);
            
            //净重/托(KG)=(单件重量*数量/托)	
            var jz_t=(ljzl*sl_t).toFixed(2);
            $("#zxXX input[id*='bzx_jz_t']").val(jz_t);

            Refresh_Main_Row();
        }

        function Refresh_Main_Row(){
            var nyl=Number($.trim($("#ljXX input[id*='nyl']").val()) == "" ? 0 : $.trim($("#ljXX input[id*='nyl']").val())); // 年用量  
            var jz_t=Number($.trim($("#zxXX input[id*='bzx_jz_t']").val()) == "" ? 0 : $.trim($("#zxXX input[id*='bzx_jz_t']").val()));//净重/托(KG)=(单件重量*数量/托)
            var sl_t=Number($.trim($("#zxXX input[id*='bzx_sl_t']").val()) == "" ? 0 : $.trim($("#zxXX input[id*='bzx_sl_t']").val())); //数量/托=(箱数/托*数量/箱)

            var xs_price=Number($.trim($("#cbXX input[id*='cbfx_xs_price']").val()) == "" ? 0 : $.trim($("#cbXX input[id*='cbfx_xs_price']").val())); //销售价格 

            //包装材料总重
            var zz_value_sum=$("[id$=gv] tr[id*=DXFooterRow]").find("td:eq(12)").text();//包装材料总重
            var bc_w_total=(Number($.trim(zz_value_sum) == "" ? 0 : $.trim(zz_value_sum))).toFixed(2);
            $("#cbXX input[id*='cbfx_bc_w_total']").val(bc_w_total);

            //成本/托==包装明细总价,包材类别=E的总价  
            var zj_value_sum=0; 
            if($("[id$=gv] input[id*=sl]").length>0){
                $("[id$=gv] tr[class*=DataRow]").each(function (index, item) { 
                    var zj = eval('zj' + index);var bclb = eval('bclb' + index);           
                    if (bclb.GetText()=="E") {
                        var zj_value=Number($.trim(zj.GetText()) == "" ? 0 : $.trim(zj.GetText()));//总价=(单价*数量)                    
                        zj_value_sum=zj_value_sum+Number(zj_value);//合计总价     
                    }                    
                });
            }
            var cb_t_total=zj_value_sum.toFixed(2);
            $("#cbXX input[id*='cbfx_cb_t_total']").val(cb_t_total);
            
            //毛重/托(KG)=(净重/托+包材总重)	
            var mz_t=(jz_t+Number(bc_w_total)).toFixed(2);
            $("#zxXX input[id*='bzx_mz_t']").val(mz_t);

            // 零件发运重量=(毛重/托÷数量/托)  
            var ljfyzl=0;
            if(sl_t!=0){ljfyzl=(Number(mz_t)/sl_t);}
            ljfyzl=ljfyzl.toFixed(10);
            $("#zxXX input[id*='bzx_ljfyzl']").val(ljfyzl);

            //实际成本/件=(成本/托÷数量/托) 
            var sj_j=0;
            if(sl_t!=0){sj_j=(Number(cb_t_total)/sl_t);}
            //sj_j=sj_j.toFixed(2);
            //$("#cbXX input[id*='cbfx_sj_j']").val(sj_j);     
            $("#cbXX input[id*='cbfx_sj_j']").val(sj_j.toFixed(2)); 

            //包装成本比例=(实际成本/销售价格)
            var cb_rate=0;
            if(xs_price!=0){cb_rate=(Number(sj_j)/xs_price*100);}
            cb_rate=(cb_rate.toFixed(2)).toString()+'%';
            $("#cbXX input[id*='cbfx_cb_rate']").val(cb_rate); 
            
            //年总价(包装)=(实际成本/件*年用量))
            var nzj=(Number(sj_j)*nyl).toFixed(2);
            $("#ljXX input[id*='nzj']").val(nzj); 
        }

        function RefreshRow() {
            var zz_value_sum=0;var zj_value_sum=0;            
            var sl_t=Number($.trim($("#zxXX input[id*='bzx_sl_t']").val()) == "" ? 0 : $.trim($("#zxXX input[id*='bzx_sl_t']").val()));//数量/托=(箱数/托*数量/箱)

            if($("[id$=gv] input[id*=sl]").length>0){
                $("[id$=gv] tr[class*=DataRow]").each(function (index, item) { 
                    var sl = eval('sl' + index);var djyl=eval('djyl' + index);
                    var dz = eval('dz' + index);var zz = eval('zz' + index);
                    var dj = eval('dj' + index);var zj = eval('zj' + index);
                    var bclb = eval('bclb' + index);

                    var sl_value=Number($.trim(sl.GetText()) == "" ? 0 : $.trim(sl.GetText()));//数量
                    var dz_value=Number($.trim(dz.GetText()) == "" ? 0 : $.trim(dz.GetText()));//单重2
                    var dj_value=Number($.trim(dj.GetText()) == "" ? 0 : $.trim(dj.GetText()));//单价

                    var djyl_value=0;//单件用量=(数量/每托的数量)
                    if(sl_t!=0){djyl_value=sl_value/sl_t;}
                    djyl_value=djyl_value.toFixed(10);
                	
                    var zz_value=(dz_value*sl_value).toFixed(4);//总重2(KG)=(单重*数量)
                    var zj_value=(dj_value*sl_value).toFixed(2);//总价=(单价*数量)

                    djyl.SetValue(djyl_value);                
                    zz.SetValue(zz_value);               
                    zj.SetValue(zj_value);

                    //合计总重，总价
                    zz_value_sum=zz_value_sum+Number(zz_value);
                    zj_value_sum=zj_value_sum+Number(zj_value);     
                });
            }

            //grid底部total值更新
            $("[id$=gv] tr[id*=DXFooterRow]").each(function (index, item) {
                $(item).find("td:eq(12)").text(zz_value_sum.toFixed(4)); 
                $(item).find("td:eq(14)").text(zj_value_sum.toFixed(2)); 
            });

            Refresh_Main_Row();
        }

        function con_sure(){
            if (gv.GetSelectedRowCount() <= 0) { layer.alert("请选择要删除的记录!"); return false; }
            //询问框
            return confirm('确认要删除吗？');
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
        var uploadedFiles_2 = [];
        function onFileUploadComplete_2(s, e) {
            if(e.callbackData) {
                var fileData = e.callbackData.split('|');uploadedFiles_2.push(fileData);$("#<%=ip_filelist_2.ClientID%>").val(uploadedFiles_2.join(";"));
                bind_table_2(fileData);
            }
        }

        function bind_table_2(fileData){
            var fileName = fileData[0],
                fileUrl = fileData[1],
                fileSize = fileData[2];                
            var eqno=uploadedFiles_2.length-1;

            var tbody_tr='<tr id="tr_2_'+eqno+'"><td Width="400px"><a href="'+fileUrl+'" target="_blank">'+fileName+'</a></td>'
                    +'<td Width="60px">'+fileSize+'</td>'
                    +'<td><span style="color:blue;cursor:pointer" id="tbl_delde_2" onclick ="del_data_2(tr_2_'+eqno+','+eqno+')" >删除</span></td>'
                    +'</tr>';

            $('#tbl_filelist_2').append(tbody_tr);
        }

        function del_data_2(a,eno){
            $(a).remove();
            uploadedFiles_2[eno]=null;
            $("#<%=ip_filelist_2.ClientID%>").val(uploadedFiles_2.join(";"));
        }
        
    </script>
    <script type="text/javascript">
        var uploadedFiles_3 = [];
        function onFileUploadComplete_3(s, e) {
            if(e.callbackData) {
                var fileData = e.callbackData.split('|');uploadedFiles_3.push(fileData);$("#<%=ip_filelist_3.ClientID%>").val(uploadedFiles_3.join(";"));
                bind_table_3(fileData);
            }
        }
        
        function bind_table_3(fileData){
            var fileName = fileData[0],
                fileUrl = fileData[1],
                fileSize = fileData[2];                
            var eqno=uploadedFiles_3.length-1;

            var tbody_tr='<tr id="tr_3_'+eqno+'"><td Width="400px"><a href="'+fileUrl+'" target="_blank">'+fileName+'</a></td>'
                    +'<td Width="60px">'+fileSize+'</td>'
                    +'<td><span style="color:blue;cursor:pointer" id="tbl_delde_3" onclick ="del_data_3(tr_3_'+eqno+','+eqno+')" >删除</span></td>'
                    +'</tr>';

            $('#tbl_filelist_3').append(tbody_tr);
        }

        function del_data_3(a,eno){
            $(a).remove();
            uploadedFiles_3[eno]=null;
            $("#<%=ip_filelist_3.ClientID%>").val(uploadedFiles_3.join(";"));
        }
        
    </script>

    <script type="text/javascript">
        //function clearNoNum(obj){
        //    obj.value = obj.value.replace(/[^\d.]/g,""); //清除"数字"和"."以外的字符
        //    obj.value = obj.value.replace(/^\./g,""); //验证第一个字符是数字
        //    obj.value = obj.value.replace(/\.{2,}/g,"."); //只保留第一个, 清除多余的
        //    obj.value = obj.value.replace(".","$#$").replace(/\./g,"").replace("$#$",".");
        //    obj.value = obj.value.replace(/^(\-)*(\d+)\.(\d\d).*$/,'$1$2.$3'); //只能输入两个小数
        //    if(obj.value.indexOf(".")< 0 && obj.value !=""){//以上已经过滤，此处控制的是如果没有小数点，首位不能为类似于 01、02的金额 
        //        if(obj.value.substr(0,1) == '0' && obj.value.length == 2){ 
        //            obj.value= parseFloat(obj.value);     
        //        } 
        //    }
        //    if (obj.value=="") {
        //        obj.value=parseFloat("0");
        //    }
        //}
        //function formatNum (obj){ 
        //    clearNoNum(obj);
        //    var numMatch = String(obj.value).match(/\d*(\.\d{0,2})?/); 
        //    return (numMatch[0] += numMatch[1] ? '00'.substr(0, 3 - numMatch[1].length) : '.00'); 
        //}

        function clearNoNum_dev_textbox_int(obj){
            obj.SetValue((obj.GetValue()==null?"":obj.GetValue()).replace(/[^0-9]/g,'')); //清除"数字"和"."以外的字符

            if((obj.GetValue()==null?"":obj.GetValue()) !=""){//以上已经过滤，此处控制的是首位不能为类似于 01、02的金额 
                if((obj.GetValue()==null?"":obj.GetValue()).substr(0,1) == '0'){ 
                    obj.SetValue(parseInt(obj.GetValue()));     
                } 
            }else {
                obj.SetValue(parseInt("0"));
            }
        }

        function clearNoNum_dev_textbox(obj){
            var numMatch = String(obj.GetValue()==null?"":obj.GetValue()).match(/\d*(\.\d{0,2})?/); 
            obj.SetValue( (numMatch[0] += numMatch[1] ? '00'.substr(0, 3 - numMatch[1].length) : '.00') ); 

            obj.SetValue((obj.GetValue()==null?"":obj.GetValue()).replace(/[^\d.]/g,"")); //清除"数字"和"."以外的字符
            obj.SetValue((obj.GetValue()==null?"":obj.GetValue()).replace(/^\./g,"")); //验证第一个字符是数字
            obj.SetValue((obj.GetValue()==null?"":obj.GetValue()).replace(/\.{2,}/g,".")); //只保留第一个, 清除多余的
            obj.SetValue((obj.GetValue()==null?"":obj.GetValue()).replace(".","$#$").replace(/\./g,"").replace("$#$","."));
            obj.SetValue((obj.GetValue()==null?"":obj.GetValue()).replace(/^(\-)*(\d+)\.(\d\d).*$/,'$1$2.$3')); //只能输入两个小数
            //if((obj.GetValue()==null?"":obj.GetValue()).indexOf(".")< 0 && obj.GetValue() !=""){//以上已经过滤，此处控制的是如果没有小数点，首位不能为类似于 01、02的金额 
            //    if((obj.GetValue()==null?"":obj.GetValue()).substr(0,1) == '0' && (obj.GetValue()==null?"":obj.GetValue()).length == 2){ 
            //        obj.SetValue(parseFloat(obj.GetValue()));     
            //    } 
            //}
            if(obj.GetValue() !=""){//以上已经过滤，此处控制的是首位不能为类似于 01、02的金额 
                if((obj.GetValue()==null?"":obj.GetValue()).substr(0,1) == '0'){ 
                    obj.SetValue(parseFloat(obj.GetValue()));     
                } 
            }
            if (obj.GetValue()=="") {
                obj.SetValue(parseFloat("0"));
            }
            var numMatch = String(obj.GetValue()==null?"":obj.GetValue()).match(/\d*(\.\d{0,2})?/); 
            obj.SetValue( (numMatch[0] += numMatch[1] ? '00'.substr(0, 3 - numMatch[1].length) : '.00') );
        }
    </script>
    <script type="text/javascript">

        function validate(action){
            var flag=true;var msg="";
            
            <%=ValidScript%>

            if($("#DQXX input[id*='ApplyId']").val()=="" || $("#DQXX input[id*='ApplyName']").val()==""){
                msg+="【申请人】不可为空.<br />";
            }
            if($("#DQXX input[id*='ApplyDeptId']").val()=="" || $("#DQXX input[id*='ApplyDeptName']").val()==""){
                msg+="【申请人部门】不可为空.<br />";
            }

            if($("#ljXX input[id*='part']").val()==""){
                msg+="【PGI_零件号】不可为空.<br />";
            }
            if($("#ljXX input[type!=hidden][id*='ver']").val()==""){
                msg+="【版本】不可为空.<br />";
            }
            if($("#ljXX input[id*='domain']").val()==""){
                msg+="【申请工厂】不可为空.<br />";
            }
            if($("#ljXX input[id*='site']").val()==""){
                msg+="【发自】不可为空.<br />";
            }
            if($("#ljXX input[id*='ship']").val()==""){
                msg+="【发至】不可为空.<br />";
            }
            if($("input[type!=hidden][id*='typeno']").val()==""){
                msg+="【申请类型】不可为空.<br />";
            }
            if($("#ljXX input[id*='ljzl']").val()==""){
                msg+="【零件重量】不可为空.<br />";
            }
            if ($("#cbXX input[id*='cbfx_xs_price']").val()=="") {
                msg+="【销售价格】不可为空.<br />";
            }

            if(action=='submit'){
                //if($("#ljXX select[id*='bzlb']").val()==""){
                if($("input[type!=hidden][id*='bzlb']").val()==""){
                    msg+="【包装类别】不可为空.<br />";
                }
                if($("#ljXX input[id*='ljcc_l']").val()==""){
                    msg+="【零件尺寸(L)】不可为空.<br />";
                }else if(Number($("#ljXX input[id*='ljcc_l']").val())<=0){
                    msg+="【零件尺寸(L)】不可小于等于0.<br />";
                }
                if($("#ljXX input[id*='ljcc_w']").val()==""){
                    msg+="【零件尺寸(W)】不可为空.<br />";
                }else if(Number($("#ljXX input[id*='ljcc_w']").val())<=0){
                    msg+="【零件尺寸(W)】不可小于等于0.<br />";
                }
                if($("#ljXX input[id*='ljcc_h']").val()==""){
                    msg+="【零件尺寸(H)】不可为空.<br />";
                }else if(Number($("#ljXX input[id*='ljcc_h']").val())<=0){
                    msg+="【零件尺寸(H)】不可小于等于0.<br />";
                }
                if($("#ljXX input[id*='gdsl_cp']").val()==""){
                    msg+="【工单数量(成品)】不可为空.<br />";
                }else if(Number($("#ljXX input[id*='gdsl_cp']").val())<=0){
                    msg+="【工单数量(成品)】不可小于等于0.<br />";
                }
                if($("#ljXX input[id*='gdsl_bcp']").val()==""){
                    msg+="【工单数量(半成品)】不可为空.<br />";
                }else if(Number($("#ljXX input[id*='gdsl_bcp']").val())<=0){
                    msg+="【工单数量(半成品)】不可小于等于0.<br />";
                }
                if($("#ljXX input[id*='klgx']").val()==""){
                    msg+="【扣料工序】不可为空.<br />";
                }else if(Number($("#ljXX input[id*='klgx']").val())<=0){
                    msg+="【扣料工序】不可小于等于0.<br />";
                }


                if($("#zxXX input[id*='bzx_part']").val()==""){
                    msg+="【包装箱】不可为空.<br />";
                }
                if($("#zxXX input[id*='bzx_w']").val()==""){
                    msg+="【包装箱子重量】不可为空.<br />";
                }
                if($("#zxXX input[id*='bzx_cc']").val()==""){
                    msg+="【箱尺寸(L*W*H)】不可为空.<br />";
                }
                if($("#zxXX input[id*='bzx_sl_c']").val()==""){
                    msg+="【数量/层】不可为空.<br />";
                }else if(Number($("#zxXX input[id*='bzx_sl_c']").val())<=0){
                    msg+="【数量/层】不可小于等于0.<br />";
                }
                if($("#zxXX input[id*='bzx_cs_x']").val()==""){
                    msg+="【层数/箱】不可为空.<br />";
                }else if(Number($("#zxXX input[id*='bzx_cs_x']").val())<=0){
                    msg+="【层数/箱】不可小于等于0.<br />";
                }
                if($("#zxXX input[id*='bzx_xs_c']").val()==""){
                    msg+="【箱数/层】不可为空.<br />";
                }else if(Number($("#zxXX input[id*='bzx_xs_c']").val())<=0){
                    msg+="【箱数/层)】不可小于等于0.<br />";
                }
                if($("#zxXX input[id*='bzx_c_t']").val()==""){
                    msg+="【层/托】不可为空.<br />";
                }else if(Number($("#zxXX input[id*='bzx_c_t']").val())<=0){
                    msg+="【托尺寸(L)】不可小于等于0.<br />";
                }
                if($("#zxXX input[id*='bzx_xs_t']").val()==""){
                    msg+="【箱数/托】不可为空.<br />";
                }
                if($("#zxXX input[id*='bzx_sl_t']").val()==""){
                    msg+="【数量/托】不可为空.<br />";
                }
                if($("#zxXX input[id*='bzx_dzcs']").val()==""){
                    msg+="【动载层数】不可为空.<br />";
                }
                if($("#zxXX input[id*='bzx_jzcs']").val()==""){
                    msg+="【静载层数】不可为空.<br />";
                }
                if($("#zxXX input[id*='bzx_t_l']").val()==""){
                    msg+="【托尺寸(L)】不可为空.<br />";
                }else if(Number($("#zxXX input[id*='bzx_t_l']").val())<=0){
                    msg+="【托尺寸(L)】不可小于等于0.<br />";
                }
                if($("#zxXX input[id*='bzx_t_w']").val()==""){
                    msg+="【托尺寸(W)】不可为空.<br />";
                }else if(Number($("#zxXX input[id*='bzx_t_w']").val())<=0){
                    msg+="【托尺寸(W)】不可小于等于0.<br />";
                }
                if($("#zxXX input[id*='bzx_t_h']").val()==""){
                    msg+="【托尺寸(H)】不可为空.<br />";
                }else if(Number($("#zxXX input[id*='bzx_t_h']").val())<=0){
                    msg+="【托尺寸(H)】不可小于等于0.<br />";
                }

                
                if($("#cbXX input[id*='cbfx_mb_j']").val()==""){
                    msg+="【目标成本/件】不可为空.<br />";
                }else if(Number($("#cbXX input[id*='cbfx_mb_j']").val())<=0){
                    msg+="【目标成本/件】不可小于等于0.<br />";
                }


                if($("[id$=gv] input[id*=sl]").length==0){
                    msg+="【包装材料明细】不可为空.<br />";
                }else {
                    if (!ASPxClientEdit.ValidateGroup("ValueValidationGroup")) {
                        msg+="【包装材料明细】格式必须正确.<br />";
                    } else {
                        $("[id$=gv] tr[class*=DataRow]").each(function (index, item) {     
                            var bclb=eval('bclb' + index);
                            var sl=eval('sl' + index);

                            if(bclb.GetText()==""){
                                msg+="【包装材料明细】的【包材类别】不可为空.<br />";
                            }

                            if(Number(sl.GetText())<=0){
                                msg+="【包装材料明细】的【数量】不可小于等于0.<br />";
                            }
                        });
                    }
                }
            }
            

            if(msg!=""){  
                flag=false;
                layer.alert(msg);
                return flag;
            }

            if(action=='submit'){
                if(!parent.checkSign()){
                    flag=false;return flag;
                }
            }

            if(flag){
                var applyid=$("#DQXX input[id*='ApplyId']").val();

                var formno=$("#CPXX input[id*='FormNo']").val();
                var part=("#ljXX input[id*='part']").val();
                var domain=$("#ljXX input[id*='domain']").val();
                var site=$("#ljXX input[id*='site']").val();
                var ship=$("#ljXX input[id*='ship']").val();
                var typeno=$("#ljXX input[id*='typeno']").val();

                $.ajax({
                    type: "post",
                    url: "PackScheme.aspx/CheckData",
                    data: "{'applyid':'" + applyid + "','formno':'" + formno + "','part':'" + part + "','domain':'" + domain + "','site':'" + site + "','ship':'" + ship + "','typeno':'" + typeno + "'}",
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
            }

            return flag;
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
            background-color: #FDF7D9; /*EFEFEF*/
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
                                <td><asp:TextBox ID="FormNo" runat="server" class="lineread"  ReadOnly="true" placeholder="自动产生" Width="260px" ToolTip="1|0" /></td>
                                <td style="width:105px;"><font color="red">&nbsp;</font>申请日期</td>
                                <td><asp:TextBox ID="ApplyDate"  runat="server" class="lineread" ReadOnly="True" Width="260px" /></td>
                                <td style="width:100px;"><font color="red">&nbsp;</font>填单人</td>
                                <td>
                                    <div class="form-inline">
                                        <asp:TextBox runat="server" ID="CreateId" class="lineread" ReadOnly="True" Width="60px"></asp:TextBox>
                                        <asp:TextBox runat="server" ID="CreateName"  class="lineread" ReadOnly="True" Width="198px"></asp:TextBox>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td><font color="red">*</font>申请人</td>
                                <td>
                                    <div class="form-inline">
                                        <asp:TextBox runat="server" ID="ApplyId" class="lineread" ReadOnly="True" Width="60px"></asp:TextBox>
                                        <asp:TextBox runat="server" ID="ApplyName"  class="lineread" ReadOnly="True" Width="198px"></asp:TextBox>
                                        <i id="ApplyId_i" class="fa fa-search <% =ViewState["ApplyId_i"].ToString() == "Y" ? "i_hidden" : "i_show" %>" onclick="Get_ApplyId()"></i>
                                    </div>
                                </td>
                                <td><font color="red">&nbsp;</font>申请人部门</td>
                                <td>
                                    <asp:TextBox runat="server" ID="ApplyDeptName"  class="lineread" ReadOnly="True" Width="260px"></asp:TextBox>
                                </td>
                                <td><font color="red">&nbsp;</font>电话(分机)</td>
                                <td>
                                    <asp:TextBox runat="server" ID="ApplyTelephone" class="lineread" Width="260px"/>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
        </div>

        <div class="row row-container">
            <div class="panel panel-infos">
                <div class="panel-headings" data-toggle="collapse" data-target="#ljXX">
                    <strong>零件信息</strong>
                </div>
                <div class="panel-body" id="ljXX">
                    <div class="col-xs-12 col-sm-12  col-md-12 col-lg-12" style="width:1200px;">
                        <table style="width: 100%; font-size: 12px;" border="0" >
                            <tr>
                                <td style="width:100px;"><font color="red">*</font>PGI_零件号</td>
                                <td>
                                    <div class="form-inline">
                                        <asp:TextBox ID="part" runat="server" class="lineread"  ReadOnly="true" Width="260px" />
                                        <i id="part_i" class="fa fa-search <% =ViewState["ApplyId_i"].ToString() == "Y" ? "i_hidden" : "i_show" %>" 
                                            onclick="Get_part()"></i>
                                    </div>
                                </td>
                                <td><font color="red">&nbsp;</font>申请工厂</td>
                                <td>
                                    <asp:TextBox ID="domain"  runat="server" class="lineread" ReadOnly="True" Width="260px" />
                                </td>   
                                <td><font color="red">&nbsp;</font>申请类型</td>
                                <td>
                                    <asp:TextBox ID="typeno"  runat="server" class="lineread" ReadOnly="True" Width="260px" />
                                </td>                       
                            </tr>
                            <tr>   
                                <td style="width:105px;"><font color="red">*</font>版本</td>
                                <td>
                                    <%--<asp:TextBox ID="ver"  runat="server" class="lineread" ReadOnly="True" Width="260px" />--%>
                                    <dx:ASPxComboBox ID="ver" runat="server" ValueType="System.String" CssClass="linewrite" Width="260px"  Height="27px" BackColor="#FDF7D9" ForeColor="#31708f"              ClientInstanceName="ver_c">
                                        <DisabledStyle CssClass="lineread"  ForeColor="#31708f" BackColor="#FFFFFF"></DisabledStyle>
                                    </dx:ASPxComboBox>
                                    <%--<asp:Label ID="lbl_m" runat="server" Text="新增时默认A0,修改时默认最新生效版本" ></asp:Label>--%>
                                </td>                        
                                <td><font color="red">&nbsp;</font>发自</td>
                                <td>
                                    <asp:TextBox runat="server" ID="site" class="lineread" ReadOnly="True" Width="260px"></asp:TextBox>
                                </td>
                                <td><font color="red">&nbsp;</font>发至</td>
                                <td>
                                    <asp:TextBox runat="server" ID="ship" class="lineread" ReadOnly="True" Width="260px"/>
                                </td>   
                            </tr>
                            <tr>
                                <td style="width:100px;"><font color="red">*</font>包装类别</td>
                                <td>
                                    <dx:ASPxComboBox ID="bzlb" runat="server" ValueType="System.String" CssClass="linewrite" Width="260px"  Height="27px" BackColor="#FDF7D9" ForeColor="#31708f" ClientInstanceName="bzlb_c">
                                        <DisabledStyle CssClass="lineread"  ForeColor="#31708f" BackColor="#FFFFFF"></DisabledStyle>
                                    </dx:ASPxComboBox>
                                   <%-- <asp:DropDownList ID="bzlb" runat="server" class="linewrite"  style="width:260px" Height="27px"> 
                                        <asp:ListItem Text="" Value=""></asp:ListItem>                            
                                        <asp:ListItem Text="成品可回用" Value="成品可回用"></asp:ListItem>                                                                               
                                        <asp:ListItem Text="成品一次性" Value="成品一次性"></asp:ListItem>
                                        <asp:ListItem Text="原材料包装" Value="原材料包装"></asp:ListItem>
                                        <asp:ListItem Text="内包装" Value="内包装"></asp:ListItem>
                                    </asp:DropDownList>--%>
                                </td>
                                <td><font color="red">&nbsp;</font>顾客</td>
                                <td>
                                    <asp:TextBox ID="custname"  runat="server" class="lineread" ReadOnly="True" Width="260px" />
                                </td>
                                <td><font color="red">&nbsp;</font>顾客零件号</td>
                                <td>
                                    <asp:TextBox runat="server" ID="custpart" class="lineread" ReadOnly="True" Width="260px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td><font color="red">*</font>零件尺寸(L)mm</td>
                                <td><%--限制文本框只能输入正数，小数--%>
                                    <%--<asp:TextBox ID="ljcc_l"  runat="server" class="linewrite" Width="260px"/>--%>
                                     <%--onkeyup="value=value.replace(/[^\d.]/g,'')" onafterpaste="value=value.replace(/[^\d.]/g,'')" onblur="value=value.replace(/[^\d.]/g,'')" --%>
                                    <%--<input id="ljcc_l" type="text" runat="server" class="linewrite" style="width:260px;" 
                                        onkeyup="clearNoNum(this)" onafterpaste="clearNoNum(this)" onblur="value=formatNum(this)" />--%>

                                    <dx:ASPxTextBox ID="ljcc_l" runat="server" Width="260px"  Height="25px"
                                            BackColor="#FDF7D9" ForeColor="#31708f" ClientInstanceName="clt_ljcc_l"
                                            Border-BorderStyle="None" BorderBottom-BorderStyle="Solid" BorderBottom-BorderColor="#cccccc" BorderBottom-BorderWidth="1px">
                                        <ClientSideEvents LostFocus="function(s, e) {clearNoNum_dev_textbox(s);}"  />
                                        <DisabledStyle BackColor="#FFFFFF" ></DisabledStyle>
                                    </dx:ASPxTextBox>
                                </td>
                                <td><font color="red">*</font>零件尺寸(W)mm</td>
                                <td><%--限制文本框只能输入正数，小数--%>
                                   <%-- <asp:TextBox ID="ljcc_w" runat="server"  class="linewrite" Width="260px"></asp:TextBox>--%>
                                    <%--<input id="ljcc_w" type="text" runat="server" class="linewrite" style="width:260px;" 
                                        onkeyup="clearNoNum(this)" onafterpaste="clearNoNum(this)" onblur="value=formatNum(this)" />--%>

                                    <dx:ASPxTextBox ID="ljcc_w" runat="server" Width="260px"  Height="25px"
                                            BackColor="#FDF7D9" ForeColor="#31708f"
                                            Border-BorderStyle="None" BorderBottom-BorderStyle="Solid" BorderBottom-BorderColor="#cccccc" BorderBottom-BorderWidth="1px">
                                        <ClientSideEvents LostFocus="function(s, e) {clearNoNum_dev_textbox(s);}"   />
                                        <DisabledStyle BackColor="#FFFFFF" ></DisabledStyle>
                                    </dx:ASPxTextBox>
                                </td>
                                <td><font color="red">*</font>零件尺寸(H)mm</td>
                                <td>
                                    <%--<asp:TextBox ID="ljcc_h" runat="server" class="linewrite" Width="260px"/>--%>
                                    <%--<input id="ljcc_h" type="text" runat="server" class="linewrite" style="width:260px;" 
                                        onkeyup="clearNoNum(this)" onafterpaste="clearNoNum(this)" onblur="value=formatNum(this)" />--%>

                                    <dx:ASPxTextBox ID="ljcc_h" runat="server" Width="260px"  Height="25px"
                                            BackColor="#FDF7D9" ForeColor="#31708f"
                                            Border-BorderStyle="None" BorderBottom-BorderStyle="Solid" BorderBottom-BorderColor="#cccccc" BorderBottom-BorderWidth="1px">
                                        <ClientSideEvents LostFocus="function(s, e) {clearNoNum_dev_textbox(s);}"   />
                                        <DisabledStyle BackColor="#FFFFFF" ></DisabledStyle>
                                    </dx:ASPxTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td><font color="red">&nbsp;</font>零件重量</td>
                                <td>
                                    <asp:TextBox ID="ljzl"  runat="server" class="lineread" ReadOnly="True" Width="260px" />
                                </td>
                                <td><font color="red">&nbsp;</font>年用量</td>
                                <td>
                                    <asp:TextBox ID="nyl" runat="server" class="lineread" ReadOnly="True" Width="260px"></asp:TextBox>
                                </td>
                                <td><font color="red">&nbsp;</font>年总价(包装)<br />(实际成本/件*年用量))</td>
                                <td>
                                    <asp:TextBox ID="nzj" runat="server" class="lineread" ReadOnly="True" Width="260px"/>
                                </td>
                            </tr>
                            <tr>
                                <td><font color="red">*</font>工单数量(成品)</td>
                                <td>
                                    <%--<asp:TextBox ID="gdsl_cp"  runat="server" class="linewrite" Width="260px" />--%>
                                   <%-- <input id="gdsl_cp" type="text" runat="server" class="linewrite" style="width:260px;" 
                                        onkeyup="value=value.replace(/[^1-9]/g,'')" onafterpaste="value=value.replace(/[^1-9]/g,'')" onblur="value=value.replace(/[^1-9]/g,'')" />--%>

                                    <dx:ASPxTextBox ID="gdsl_cp" runat="server" Width="260px"  Height="25px"
                                            BackColor="#FDF7D9" ForeColor="#31708f"
                                            Border-BorderStyle="None" BorderBottom-BorderStyle="Solid" BorderBottom-BorderColor="#cccccc" BorderBottom-BorderWidth="1px">
                                        <ClientSideEvents LostFocus="function(s, e) {clearNoNum_dev_textbox_int(s);}"  />
                                        <DisabledStyle BackColor="#FFFFFF" ></DisabledStyle>
                                    </dx:ASPxTextBox>

                                </td>
                                <td><font color="red">*</font>工单数量(半成品)</td>
                                <td>
                                    <%--<asp:TextBox ID="gdsl_bcp" runat="server" class="linewrite" Width="260px"></asp:TextBox>--%>
                                    <%--<input id="gdsl_bcp" type="text" runat="server" class="linewrite" style="width:260px;" 
                                        onkeyup="value=value.replace(/[^1-9]/g,'')" onafterpaste="value=value.replace(/[^1-9]/g,'')" onblur="value=value.replace(/[^1-9]/g,'')" />--%>

                                    <dx:ASPxTextBox ID="gdsl_bcp" runat="server" Width="260px"  Height="25px"
                                            BackColor="#FDF7D9" ForeColor="#31708f"
                                            Border-BorderStyle="None" BorderBottom-BorderStyle="Solid" BorderBottom-BorderColor="#cccccc" BorderBottom-BorderWidth="1px">
                                        <ClientSideEvents LostFocus="function(s, e) {clearNoNum_dev_textbox_int(s);}" />
                                        <DisabledStyle BackColor="#FFFFFF" ></DisabledStyle>
                                    </dx:ASPxTextBox>
                                </td>
                                <td><font color="red">*</font>扣料工序</td>
                                <td>
                                    <%--<asp:TextBox ID="klgx" runat="server" class="linewrite" Width="260px"/>--%>
                                    <%--<input id="klgx" type="text" runat="server" class="linewrite" style="width:260px;" 
                                        onkeyup="value=value.replace(/[^1-9]/g,'')" onafterpaste="value=value.replace(/[^1-9]/g,'')" onblur="value=value.replace(/[^1-9]/g,'')" />--%>

                                    <%--<dx:ASPxTextBox ID="klgx" runat="server" Width="260px"  Height="25px"
                                            BackColor="#FDF7D9" ForeColor="#31708f"
                                            Border-BorderStyle="None" BorderBottom-BorderStyle="Solid" BorderBottom-BorderColor="#cccccc" BorderBottom-BorderWidth="1px">
                                        <ClientSideEvents LostFocus="function(s, e) {clearNoNum_dev_textbox_int(s);}" />
                                        <DisabledStyle BackColor="#FFFFFF" ></DisabledStyle>
                                    </dx:ASPxTextBox>--%>

                                    <asp:TextBox ID="klgx" runat="server" class="lineread" ReadOnly="true" Width="260px"/>

                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
        </div>

        <div class="row row-container">
            <div class="panel panel-infos">
                <div class="panel-headings" data-toggle="collapse" data-target="#zxXX">
                    <strong>装箱数据</strong>
                </div>
                <div class="panel-body" id="zxXX">
                    <div class="col-xs-12 col-sm-12  col-md-12 col-lg-12" style="width:1200px;">
                        <table style="width: 100%; font-size: 12px;" border="0" >
                            <tr>
                                <td style="width:100px;"><font color="red">*</font>选择包装箱</td>
                                <td>
                                    <div class="form-inline">
                                        <asp:TextBox ID="bzx_part" runat="server" class="lineread"  ReadOnly="true" Width="260px" />
                                        <i id="bzx_part_i" class="fa fa-search <% =ViewState["ApplyId_i"].ToString() == "Y" ? "i_hidden" : "i_show" %>" 
                                            onclick="Get_bzx_part()"></i>
                                    </div>
                                </td>
                                <td style="width:105px;"><font color="red">*</font>包装箱子重量</td>
                                <td>
                                    <asp:TextBox ID="bzx_w" runat="server" class="lineread" ReadOnly="True" Width="260px"></asp:TextBox>
                                </td>
                                <td style="width:100px;"><font color="red">*</font>包装箱规格</td>
                                <td>
                                   <asp:TextBox ID="bzx_gg" runat="server" class="lineread" ReadOnly="True" Width="260px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td><font color="red">*</font>箱尺寸(L*W*H)</td>
                                <td>
                                    <asp:TextBox ID="bzx_cc" runat="server" class="linewrite" Width="260px"/>
                                </td>
                                <td><font color="red">*</font>数量/层</td>
                                <td>
                                    <%--<asp:TextBox ID="bzx_sl_c" runat="server" class="linewrite" Width="260px"/>--%>                                    
                                    <%--<input id="bzx_sl_c" type="text" runat="server" class="linewrite" style="width:260px;" 
                                        onkeyup="value=value.replace(/[^1-9]/g,'')" onafterpaste="value=value.replace(/[^1-9]/g,'')" onblur="value=value.replace(/[^1-9]/g,'')" />--%>

                                    <dx:ASPxTextBox ID="bzx_sl_c" runat="server" Width="260px"  Height="25px"
                                            BackColor="#FDF7D9" ForeColor="#31708f"
                                            Border-BorderStyle="None" BorderBottom-BorderStyle="Solid" BorderBottom-BorderColor="#cccccc" BorderBottom-BorderWidth="1px">
                                        <ClientSideEvents LostFocus="function(s, e) {clearNoNum_dev_textbox_int(s);}"  ValueChanged="function(s, e) {RefreshMain();}" />
                                        <DisabledStyle BackColor="#FFFFFF" ></DisabledStyle>
                                    </dx:ASPxTextBox>
                                </td>
                                <td><font color="red">*</font>层数/箱</td>
                                <td>
                                    <%--<asp:TextBox ID="bzx_cs_x" runat="server" class="linewrite" Width="260px"/>--%>                                    
                                    <%--<input id="bzx_cs_x" type="text" runat="server" class="linewrite" style="width:260px;" 
                                        onkeyup="value=value.replace(/[^1-9]/g,'')" onafterpaste="value=value.replace(/[^1-9]/g,'')" onblur="value=value.replace(/[^1-9]/g,'')" />--%>

                                    <dx:ASPxTextBox ID="bzx_cs_x" runat="server" Width="260px"  Height="25px"
                                            BackColor="#FDF7D9" ForeColor="#31708f"
                                            Border-BorderStyle="None" BorderBottom-BorderStyle="Solid" BorderBottom-BorderColor="#cccccc" BorderBottom-BorderWidth="1px">
                                        <ClientSideEvents LostFocus="function(s, e) {clearNoNum_dev_textbox_int(s);}" ValueChanged="function(s, e) {RefreshMain();}" />
                                        <DisabledStyle BackColor="#FFFFFF" ></DisabledStyle>
                                    </dx:ASPxTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td><font color="red">&nbsp;</font>数量/箱<br />(数量/层*层数/箱)</td>
                                <td>
                                    <asp:TextBox ID="bzx_sl_x"  runat="server" class="lineread" ReadOnly="True" Width="260px" />
                                </td>
                                <td><font color="red">&nbsp;</font>净重/箱(KG)<br />(单件重量*数量/箱)</td>
                                <td>
                                    <asp:TextBox ID="bzx_jz_x" runat="server" class="lineread" ReadOnly="True" Width="260px"></asp:TextBox>
                                </td>
                                <td><font color="red">&nbsp;</font>毛重/箱(KG)<br />(净重+箱子重量)</td><%--(净重/箱(KG)+包装箱子重量) (净重+箱子重量)--%>
                                <td>
                                    <asp:TextBox ID="bzx_mz_x" runat="server" class="lineread" ReadOnly="True" Width="260px"/>
                                </td>
                            </tr>
                            <tr>
                                <td><font color="red">*</font>箱数/层</td>
                                <td>
                                    <%--<asp:TextBox ID="bzx_xs_c" runat="server" class="linewrite" Width="260px" />--%>                                    
                                    <%--<input id="bzx_xs_c" type="text" runat="server" class="linewrite" style="width:260px;" 
                                        onkeyup="value=value.replace(/[^1-9]/g,'')" onafterpaste="value=value.replace(/[^1-9]/g,'')" onblur="value=value.replace(/[^1-9]/g,'')" />--%>

                                    <dx:ASPxTextBox ID="bzx_xs_c" runat="server" Width="260px"  Height="25px"
                                            BackColor="#FDF7D9" ForeColor="#31708f"
                                            Border-BorderStyle="None" BorderBottom-BorderStyle="Solid" BorderBottom-BorderColor="#cccccc" BorderBottom-BorderWidth="1px">
                                        <ClientSideEvents LostFocus="function(s, e) {clearNoNum_dev_textbox_int(s);}" ValueChanged="function(s, e) {RefreshMain();}" />
                                        <DisabledStyle BackColor="#FFFFFF" ></DisabledStyle>
                                    </dx:ASPxTextBox>
                                </td>
                                <td><font color="red">*</font>层/托</td>
                                <td>
                                    <%--<asp:TextBox ID="bzx_c_t" runat="server" class="linewrite" Width="260px"/>--%>
                                    <%--<input id="bzx_c_t" type="text" runat="server" class="linewrite" style="width:260px;" 
                                        onkeyup="value=value.replace(/[^1-9]/g,'')" onafterpaste="value=value.replace(/[^1-9]/g,'')" onblur="value=value.replace(/[^1-9]/g,'')" />--%>

                                    <dx:ASPxTextBox ID="bzx_c_t" runat="server" Width="260px"  Height="25px"
                                            BackColor="#FDF7D9" ForeColor="#31708f"
                                            Border-BorderStyle="None" BorderBottom-BorderStyle="Solid" BorderBottom-BorderColor="#cccccc" BorderBottom-BorderWidth="1px">
                                        <ClientSideEvents LostFocus="function(s, e) {clearNoNum_dev_textbox_int(s);}" ValueChanged="function(s, e) {RefreshMain();}"  />
                                        <DisabledStyle BackColor="#FFFFFF" ></DisabledStyle>
                                    </dx:ASPxTextBox>
                                </td>
                                <td><font color="red">&nbsp;</font>箱数/托<br />(箱数/层*层/托)</td>
                                <td>
                                    <asp:TextBox ID="bzx_xs_t" runat="server" class="lineread" ReadOnly="True" Width="260px"/>
                                </td>
                            </tr>
                            <tr>
                                <td><font color="red">&nbsp;</font>数量/托<br />(箱数/托*数量/箱)</td>
                                <td>
                                    <asp:TextBox ID="bzx_sl_t" runat="server" class="lineread" ReadOnly="True" Width="260px"/>
                                </td>
                                <td><font color="red">*</font>动载层数<br />(<font color="red">运输堆码层数</font>)</td>
                                <td>
                                    <%--<asp:TextBox ID="bzx_dzcs" runat="server" class="linewrite" Width="260px"/>--%>
                                    <%--<input id="bzx_dzcs" type="text" runat="server" class="linewrite" style="width:260px;" 
                                        onkeyup="value=value.replace(/[^1-9]/g,'')" onafterpaste="value=value.replace(/[^1-9]/g,'')" onblur="value=value.replace(/[^1-9]/g,'')" />--%>

                                    <dx:ASPxTextBox ID="bzx_dzcs" runat="server" Width="260px"  Height="25px"
                                            BackColor="#FDF7D9" ForeColor="#31708f"
                                            Border-BorderStyle="None" BorderBottom-BorderStyle="Solid" BorderBottom-BorderColor="#cccccc" BorderBottom-BorderWidth="1px">
                                        <ClientSideEvents LostFocus="function(s, e) {clearNoNum_dev_textbox_int(s);}"  />
                                        <DisabledStyle BackColor="#FFFFFF" ></DisabledStyle>
                                    </dx:ASPxTextBox>
                                </td>
                                <td><font color="red">*</font>静载层数<br />(<font color="red">仓储堆码层数</font>)</td>
                                <td>
                                    <%--<asp:TextBox ID="bzx_jzcs" runat="server" class="linewrite" Width="260px"/>--%>
                                    <%--<input id="bzx_jzcs" type="text" runat="server" class="linewrite" style="width:260px;" 
                                        onkeyup="value=value.replace(/[^1-9]/g,'')" onafterpaste="value=value.replace(/[^1-9]/g,'')" onblur="value=value.replace(/[^1-9]/g,'')" />--%>

                                    <dx:ASPxTextBox ID="bzx_jzcs" runat="server" Width="260px"  Height="25px"
                                            BackColor="#FDF7D9" ForeColor="#31708f"
                                            Border-BorderStyle="None" BorderBottom-BorderStyle="Solid" BorderBottom-BorderColor="#cccccc" BorderBottom-BorderWidth="1px">
                                        <ClientSideEvents LostFocus="function(s, e) {clearNoNum_dev_textbox_int(s);}" />
                                        <DisabledStyle BackColor="#FFFFFF" ></DisabledStyle>
                                    </dx:ASPxTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td><font color="red">&nbsp;</font>净重/托(KG)<br />(单件重量*数量/托)</td>
                                <td>
                                    <asp:TextBox ID="bzx_jz_t" runat="server" class="lineread" ReadOnly="True" Width="260px"/>
                                </td>
                                <td><font color="red">&nbsp;</font>毛重/托(KG)<br />(净重/托+包材总重)</td>
                                <td>
                                    <asp:TextBox ID="bzx_mz_t" runat="server" class="lineread" ReadOnly="True" Width="260px"/>
                                </td>
                                <td><font color="red">&nbsp;</font>零件发运重量<br />(毛重/托÷数量/托)</td>
                                <td>
                                    <asp:TextBox ID="bzx_ljfyzl" runat="server" class="lineread" ReadOnly="True" Width="260px"/>
                                </td>
                            </tr>
                            <tr>
                                <td><font color="red">*</font>托尺寸(L)mm</td>
                                <td>
                                    <%--<asp:TextBox ID="bzx_t_l"  runat="server" class="linewrite" Width="260px" />--%>
                                    <%--<input id="bzx_t_l" type="text" runat="server" class="linewrite" style="width:260px;" 
                                        onkeyup="clearNoNum(this)" onafterpaste="clearNoNum(this)" onblur="value=formatNum(this)" />--%>

                                    <dx:ASPxTextBox ID="bzx_t_l" runat="server" Width="260px"  Height="25px"
                                            BackColor="#FDF7D9" ForeColor="#31708f"
                                            Border-BorderStyle="None" BorderBottom-BorderStyle="Solid" BorderBottom-BorderColor="#cccccc" BorderBottom-BorderWidth="1px">
                                        <ClientSideEvents LostFocus="function(s, e) {clearNoNum_dev_textbox(s);}"   />
                                        <DisabledStyle BackColor="#FFFFFF" ></DisabledStyle>
                                    </dx:ASPxTextBox>
                                </td>
                                <td><font color="red">*</font>托尺寸(W)mm</td>
                                <td>
                                    <%--<asp:TextBox ID="bzx_t_w" runat="server" class="linewrite" Width="260px" />--%>
                                    <%--<input id="bzx_t_w" type="text" runat="server" class="linewrite" style="width:260px;" 
                                        onkeyup="clearNoNum(this)" onafterpaste="clearNoNum(this)" onblur="value=formatNum(this)" />--%>

                                    <dx:ASPxTextBox ID="bzx_t_w" runat="server" Width="260px"  Height="25px"
                                            BackColor="#FDF7D9" ForeColor="#31708f"
                                            Border-BorderStyle="None" BorderBottom-BorderStyle="Solid" BorderBottom-BorderColor="#cccccc" BorderBottom-BorderWidth="1px">
                                        <ClientSideEvents LostFocus="function(s, e) {clearNoNum_dev_textbox(s);}" />
                                        <DisabledStyle BackColor="#FFFFFF" ></DisabledStyle>
                                    </dx:ASPxTextBox>
                                </td>
                                <td><font color="red">*</font>托尺寸(H)mm</td>
                                <td>
                                    <%--<asp:TextBox ID="bzx_t_h" runat="server" class="linewrite" Width="260px"/>--%>
                                    <%--<input id="bzx_t_h" type="text" runat="server" class="linewrite" style="width:260px;" 
                                        onkeyup="clearNoNum(this)" onafterpaste="clearNoNum(this)" onblur="value=formatNum(this)" />--%>

                                    <dx:ASPxTextBox ID="bzx_t_h" runat="server" Width="260px"  Height="25px"
                                            BackColor="#FDF7D9" ForeColor="#31708f"
                                            Border-BorderStyle="None" BorderBottom-BorderStyle="Solid" BorderBottom-BorderColor="#cccccc" BorderBottom-BorderWidth="1px">
                                        <ClientSideEvents LostFocus="function(s, e) {clearNoNum_dev_textbox(s);}"  />
                                        <DisabledStyle BackColor="#FFFFFF" ></DisabledStyle>
                                    </dx:ASPxTextBox>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
        </div>

        <div class="row row-container">
            <div class="panel panel-infos">
                <div class="panel-headings" data-toggle="collapse" data-target="#cbXX">
                    <strong>成本分析</strong>
                </div>
                <div class="panel-body" id="cbXX">
                    <div class="col-xs-12 col-sm-12  col-md-12 col-lg-12" style="width:1200px;">
                        <table style="width: 100%; font-size: 12px;" border="0" >
                            <tr>
                                <td><font color="red">&nbsp;</font>实际成本/件<br />(成本/托除数量/托)</td>
                                <td>
                                    <asp:TextBox ID="cbfx_sj_j" runat="server" class="lineread" ReadOnly="True" Width="260px" />
                                </td>
                                <td><font color="red">*</font>目标成本/件</td>
                                <td>
                                    <%--<asp:TextBox ID="cbfx_mb_j" runat="server" class="linewrite" Width="260px" />--%>
                                    <%--<input id="cbfx_mb_j" type="text" runat="server" class="linewrite" style="width:260px;" 
                                        onkeyup="clearNoNum(this)" onafterpaste="clearNoNum(this)" onblur="value=formatNum(this)" />--%>

                                    <dx:ASPxTextBox ID="cbfx_mb_j" runat="server" Width="260px"  Height="25px"
                                            BackColor="#FDF7D9" ForeColor="#31708f"
                                            Border-BorderStyle="None" BorderBottom-BorderStyle="Solid" BorderBottom-BorderColor="#cccccc" BorderBottom-BorderWidth="1px">
                                        <ClientSideEvents LostFocus="function(s, e) {clearNoNum_dev_textbox(s);}"  />
                                        <DisabledStyle BackColor="#FFFFFF" ></DisabledStyle>
                                    </dx:ASPxTextBox>
                                </td>
                                <td><font color="red">&nbsp;</font>销售价格</td>
                                <td>
                                    <asp:TextBox ID="cbfx_xs_price" runat="server" class="lineread" ReadOnly="True" Width="260px"/>
                                </td>
                            </tr>
                            <tr>
                                <td><font color="red">&nbsp;</font>包装材料总重</td>
                                <td>
                                    <asp:TextBox ID="cbfx_bc_w_total" runat="server" class="lineread" ReadOnly="True" Width="260px" />
                                </td>
                                <td><font color="red">&nbsp;</font>成本/托<br />(包装明细总价)</td>
                                <td>
                                    <asp:TextBox ID="cbfx_cb_t_total" runat="server" class="lineread" ReadOnly="True" Width="260px" />
                                </td>
                                <td><font color="red">&nbsp;</font>包装成本比例<br />(实际成本/销售价格)</td>
                                <td>
                                    <asp:TextBox ID="cbfx_cb_rate" runat="server" class="lineread" ReadOnly="True" Width="260px"/>
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
                        <table style="width: 100%; font-size: 12px;">
                            <tr>
                                <td style="width:100px;"><font color="red">*</font>零件图片</td><%--border-bottom-style:solid; border-bottom-width:1px;--%>
                                <td colspan="5">
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
                                </td>
                            </tr>
                            <tr>
                                <td style="width:100px;"><font color="red">*</font>包装箱内部</td>
                                <td colspan="5">
                                     <dx:ASPxUploadControl ID="uploadcontrol_2" runat="server" Width="500px" BrowseButton-Text="浏览" Visible="true" ClientInstanceName="UploadControl"
                                        ShowAddRemoveButtons="True" RemoveButton-Text="删除" UploadMode="Advanced" AutoStartUpload="true" ShowUploadButton="false" ShowProgressPanel="true"
                                        OnFileUploadComplete="uploadcontrol_2_FileUploadComplete">
                                        <AdvancedModeSettings EnableDragAndDrop="True" EnableFileList="True" EnableMultiSelect="True">
                                        </AdvancedModeSettings>
                                        <ClientSideEvents FileUploadComplete="onFileUploadComplete_2" />
                                    </dx:ASPxUploadControl>                       
                                    <input type="hidden" id="ip_filelist_2" name="ip_filelist_2" runat="server" />
                                    <table id="tbl_filelist_2" width="500px">
                                    </table>
                                    <asp:UpdatePanel runat="server" ID="p11_2" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <textarea id="ip_filelist_db_2" name="ip_filelist_2" runat="server" cols="200" rows="2" visible="false"></textarea>
                                            <asp:Table ID="tab1_2" Width="500px" runat="server">
                                            </asp:Table>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                            <tr>
                                <td style="width:100px;"><font color="red">*</font>包装箱外观</td>
                                <td colspan="5">
                                     <dx:ASPxUploadControl ID="uploadcontrol_3" runat="server" Width="500px" BrowseButton-Text="浏览" Visible="true" ClientInstanceName="UploadControl"
                                        ShowAddRemoveButtons="True" RemoveButton-Text="删除" UploadMode="Advanced" AutoStartUpload="true" ShowUploadButton="false" ShowProgressPanel="true"
                                        OnFileUploadComplete="uploadcontrol_3_FileUploadComplete">
                                        <AdvancedModeSettings EnableDragAndDrop="True" EnableFileList="True" EnableMultiSelect="True">
                                        </AdvancedModeSettings>
                                        <ClientSideEvents FileUploadComplete="onFileUploadComplete_3" />
                                    </dx:ASPxUploadControl>                       
                                    <input type="hidden" id="ip_filelist_3" name="ip_filelist_3" runat="server" />
                                    <table id="tbl_filelist_3" width="500px">
                                    </table>
                                    <asp:UpdatePanel runat="server" ID="UpdatePanel1" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <textarea id="ip_filelist_db_3" name="ip_filelist_3" runat="server" cols="200" rows="2" visible="false"></textarea>
                                            <asp:Table ID="tab1_3" Width="500px" runat="server">
                                            </asp:Table>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                        </table>
                        <%--<table style="width: 100%; font-size: 12px;" border="0" >
                            <tr>
                                <td style="width:100px;"><font color="red">*</font>附件类别</td>
                                <td style="width:260px;">
                                     <dx:ASPxComboBox ID="files_type" runat="server" ValueType="System.String" CssClass="linewrite" Width="260px"  Height="27px" BackColor="#FDF7D9" ForeColor="#31708f" ClientInstanceName="files_type_c">
                                        <DisabledStyle CssClass="lineread" ForeColor="#31708f" BackColor="#FFFFFF"></DisabledStyle>
                                    </dx:ASPxComboBox>
                                </td>
                                <td colspan="4">
                                    
                                </td>
                            </tr>
                        </table>
                        <div>     
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
                        </div>--%>
                    </div>
                </div>
            </div>
        </div>

        <div class="row row-container">
            <div class="panel panel-infos">
                <div class="panel-headings" data-toggle="collapse" data-target="#bzclXX">
                    <strong>包装材料明细信息</strong>
                </div>
                <div class="panel-body" id="bzclXX">
                    <div class="col-xs-12 col-sm-12  col-md-12 col-lg-12" style="width:1200px;">
                        <div style="padding: 2px 5px 5px 0px">                
                            
                        </div>
                        <asp:UpdatePanel runat="server" ID="p1" UpdateMode="Conditional" >
                            <ContentTemplate>
                                
                                <asp:Button ID="btnadd" runat="server" Text="新增" class="btn btn-default btn-sm"  OnClick="btnadd_Click" />
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
                                        <dx:GridViewDataTextColumn Caption="域" FieldName="domain" Width="40px" VisibleIndex="1">
                                            <Settings AllowCellMerge="False"/>
                                            <DataItemTemplate>
                                                <dx:ASPxTextBox ID="domain" Width="40px" runat="server" Value='<%# Eval("domain")%>' 
                                                    ClientInstanceName='<%# "domain"+Container.VisibleIndex.ToString() %>' Border-BorderWidth="0"   ReadOnly="true">
                                                </dx:ASPxTextBox>
                                            </DataItemTemplate>        
                                        </dx:GridViewDataTextColumn>  
                                        <dx:GridViewDataTextColumn Caption="包装箱编码" FieldName="bm" Width="90px" VisibleIndex="2">
                                            <Settings AllowCellMerge="False" />
                                            <DataItemTemplate>
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <dx:ASPxTextBox ID="bm" Width="80px" runat="server" Value='<%# Eval("bm")%>' 
                                                                ClientInstanceName='<%# "bm"+Container.VisibleIndex.ToString() %>' Border-BorderWidth="0" ReadOnly="true">
                                                            </dx:ASPxTextBox>
                                                        </td>
                                                        <td><i id="bm_i_<%#Container.VisibleIndex.ToString() %>" 
                                                            class="fa fa-search <% =ViewState["ApplyId_i"].ToString() == "Y" ? "i_hidden" : "i_show" %>" 
                                                            onclick="Get_bm(<%# Container.VisibleIndex %>)"></i>
                                                        </td>
                                                    </tr>
                                                </table>       
                                            </DataItemTemplate>
                                        </dx:GridViewDataTextColumn>                                       
                                        <dx:GridViewDataTextColumn Caption="包装箱名称" FieldName="mc" Width="150px" VisibleIndex="2">
                                            <Settings AllowCellMerge="False"/>
                                            <DataItemTemplate>
                                                <dx:ASPxTextBox ID="mc" Width="150px" runat="server" Value='<%# Eval("mc")%>' 
                                                    ClientInstanceName='<%# "mc"+Container.VisibleIndex.ToString() %>' Border-BorderWidth="0" ReadOnly="true">
                                                </dx:ASPxTextBox>
                                            </DataItemTemplate>        
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="包材类别<br />（E/R）" FieldName="bclb" Width="60px" VisibleIndex="3">
                                            <Settings AllowCellMerge="False"/>
                                            <DataItemTemplate>
                                                <dx:ASPxTextBox ID="bclb" Width="60px" runat="server" Value='<%# Eval("bclb")%>' 
                                                    ClientInstanceName='<%# "bclb"+Container.VisibleIndex.ToString() %>' Border-BorderWidth="0" ReadOnly="true">
                                                </dx:ASPxTextBox> 
                                            </DataItemTemplate>        
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="数量" FieldName="sl" Width="60px" VisibleIndex="4">
                                            <Settings AllowCellMerge="False"/>
                                            <DataItemTemplate>
                                                <dx:ASPxTextBox ID="sl" Width="60px" runat="server" Value='<%# Eval("sl")%>' 
                                                    ClientSideEvents-ValueChanged='<%# "function(s,e){RefreshRow("+Container.VisibleIndex+");}" %>' 
                                                    ClientInstanceName='<%# "sl"+Container.VisibleIndex.ToString() %>'>
                                                    <ClientSideEvents LostFocus="function(s, e) {clearNoNum_dev_textbox(s);}"  />
                                                </dx:ASPxTextBox>
                                            </DataItemTemplate>        
                                             <PropertiesTextEdit DisplayFormatString="{0:N2}"></PropertiesTextEdit>
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="单件用量<br />(数量/每托的数量)" FieldName="djyl" Width="90px" VisibleIndex="6">
                                            <Settings AllowCellMerge="False"/>
                                            <DataItemTemplate>
                                                <dx:ASPxTextBox ID="djyl" Width="90px" runat="server" Value='<%# Eval("djyl")%>' HorizontalAlign="Right"
                                                    ClientInstanceName='<%# "djyl"+Container.VisibleIndex.ToString() %>' Border-BorderWidth="0" ReadOnly="true">
                                                </dx:ASPxTextBox> 
                                            </DataItemTemplate>
                                             <PropertiesTextEdit DisplayFormatString="{0:N10}"></PropertiesTextEdit>
                                        </dx:GridViewDataTextColumn> 
                                         <dx:GridViewDataTextColumn Caption="材料" FieldName="cl" Width="150px" VisibleIndex="7">
                                            <Settings AllowCellMerge="False"/>
                                            <DataItemTemplate>
                                                <dx:ASPxTextBox ID="cl" Width="150px" runat="server" Value='<%# Eval("cl")%>' 
                                                    ClientInstanceName='<%# "cl"+Container.VisibleIndex.ToString() %>' Border-BorderWidth="0" ReadOnly="true">
                                                </dx:ASPxTextBox>
                                            </DataItemTemplate>        
                                        </dx:GridViewDataTextColumn>
                                         <dx:GridViewDataTextColumn Caption="尺寸" FieldName="cc" Width="150px" VisibleIndex="8">
                                            <Settings AllowCellMerge="False"/>
                                            <DataItemTemplate>
                                                <dx:ASPxTextBox ID="cc" Width="150px" runat="server" Value='<%# Eval("cc")%>' 
                                                    ClientInstanceName='<%# "cc"+Container.VisibleIndex.ToString() %>' Border-BorderWidth="0" ReadOnly="true">
                                                </dx:ASPxTextBox>
                                            </DataItemTemplate>        
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="单重2(KG)" FieldName="dz" Width="60px" VisibleIndex="9">
                                            <Settings AllowCellMerge="False"/>
                                            <DataItemTemplate>
                                                <dx:ASPxTextBox ID="dz" Width="60px" runat="server" Value='<%# Eval("dz")%>'  HorizontalAlign="Right"
                                                    ClientInstanceName='<%# "dz"+Container.VisibleIndex.ToString() %>' Border-BorderWidth="0" ReadOnly="true">
                                                </dx:ASPxTextBox> 
                                            </DataItemTemplate>
                                             <PropertiesTextEdit DisplayFormatString="{0:N4}"></PropertiesTextEdit>
                                        </dx:GridViewDataTextColumn> 
                                        <dx:GridViewDataTextColumn Caption="总重2(KG)<br />(单重*数量)" FieldName="zz" Width="80px" VisibleIndex="10">
                                            <Settings AllowCellMerge="False"/>
                                            <DataItemTemplate>
                                                <dx:ASPxTextBox ID="zz" Width="80px" runat="server" Value='<%# Eval("zz")%>' HorizontalAlign="Right"
                                                    ClientInstanceName='<%# "zz"+Container.VisibleIndex.ToString() %>' Border-BorderWidth="0" ReadOnly="true">
                                                </dx:ASPxTextBox> 
                                            </DataItemTemplate>
                                             <PropertiesTextEdit DisplayFormatString="{0:N4}"></PropertiesTextEdit>
                                        </dx:GridViewDataTextColumn> 
                                        <dx:GridViewDataTextColumn Caption="单价" FieldName="dj" Width="50px" VisibleIndex="11">
                                            <Settings AllowCellMerge="False"/>
                                            <DataItemTemplate>
                                                <dx:ASPxTextBox ID="dj" Width="50px" runat="server" Value='<%# Eval("dj")%>' HorizontalAlign="Right"
                                                    ClientInstanceName='<%# "dj"+Container.VisibleIndex.ToString() %>' Border-BorderWidth="0" ReadOnly="true">
                                                </dx:ASPxTextBox> 
                                            </DataItemTemplate>
                                             <PropertiesTextEdit DisplayFormatString="{0:N2}"></PropertiesTextEdit>
                                        </dx:GridViewDataTextColumn> 
                                        <dx:GridViewDataTextColumn Caption="总价<br />(单价*数量)" FieldName="zj" Width="80px" VisibleIndex="12">
                                            <Settings AllowCellMerge="False"/>
                                            <DataItemTemplate>
                                                <dx:ASPxTextBox ID="zj" Width="80px" runat="server" Value='<%# Eval("zj")%>' HorizontalAlign="Right"
                                                    ClientInstanceName='<%# "zj"+Container.VisibleIndex.ToString() %>' Border-BorderWidth="0" ReadOnly="true">
                                                </dx:ASPxTextBox> 
                                            </DataItemTemplate>
                                             <PropertiesTextEdit DisplayFormatString="{0:N2}"></PropertiesTextEdit>
                                        </dx:GridViewDataTextColumn> 
                                         <dx:GridViewDataTextColumn Caption="供应商" FieldName="gys" Width="150px" VisibleIndex="13">
                                            <Settings AllowCellMerge="False"/>
                                            <DataItemTemplate>
                                                <dx:ASPxTextBox ID="gys" Width="150px" runat="server" Value='<%# Eval("gys")%>' 
                                                    ClientInstanceName='<%# "gys"+Container.VisibleIndex.ToString() %>' Border-BorderWidth="0" ReadOnly="true">
                                                </dx:ASPxTextBox>
                                            </DataItemTemplate>        
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="id" Width="0px">
                                            <HeaderStyle CssClass="hidden" />
                                            <CellStyle CssClass="hidden"></CellStyle>
                                            <FooterCellStyle CssClass="hidden"></FooterCellStyle>
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="PackNo" Width="0px">
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

