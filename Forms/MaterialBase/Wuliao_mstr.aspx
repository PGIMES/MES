<%@ Page Title="生产性物料申请" Language="C#" AutoEventWireup="true"
    CodeFile="Wuliao_mstr.aspx.cs" Inherits="Forms_MaterialBase_Wuliao_mstr"
    MaintainScrollPositionOnPostback="True" ValidateRequest="true" %>

<%@ Register Assembly="DevExpress.Web.v17.2, Version=17.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script src="../../Content/js/jquery.min.js"></script>
    <script src="../../Content/js/bootstrap.min.js"></script>
    <script src="../../Content/js/layer/layer.js"></script>
    <link href="../../Content/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../../Content/css/custom.css" rel="stylesheet" type="text/css" />
    <script src="../../Content/js/layer/layer.js" type="text/javascript"></script>
    <link href="../../Content/js/plugins/bootstrap-select/css/bootstrap-select.min.css"
        rel="stylesheet" />
    <script src="../../Content/js/plugins/bootstrap-select/js/bootstrap-select.min.js"></script>
    <script src="../../Scripts/RFlow.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#mestitle").html("【生产性物料申请】<a href='/userguide/生产性物料申请手册.pps' target='_blank' class='h5' style='color:red'>使用说明</a>"); //
            //控制编辑画面栏位样式

            $("select[id*='type']").change(function () {
                gv_d.PerformCallback($(this).val());
                if ($("select[id*='type']").val() == "数据管理员修改") {
                    setRead($("div[id=PB]").find("input,select"), true);
                    setRead($("div[id=PC]").find("input,select"), true);
                }

            });


            var val = "";
            $("input[id*='Checkbox']").each(function () {
                $(this).click(function () {

                    if ($(this).prop('checked')) {
                        $("input[id*='Checkbox']").removeAttr('checked');
                        $(this).prop('checked', 'checked');
                        $("#comp").val("");
                        val = $(this).val();
                        $("#comp").val(val);
                        bindSelect($("#comp").val(), "N");

                    }
                });
            });

            $("input[id*='ddlopinion']").click(function () {
                $("#ddlopinion").val("");
                var val = ""; var cgy = "";
                $("input[id*='ddlopinion']").each(function (i, item) {
                    if ($(item).prop("checked") == true) {
                        val = val + $(item).val() + ";";
                    }
                })
                if (val.length > 0) {
                    val = val.substring(0, val.length - 1);
                }
                $("#opinion").val(val);
            });

            SetButtons();
            SetGridbtn();



            if ($("#formsite").val() != "") {
                var arr = $("#formsite").val().split('/');
                var formsite = arr[0];
                var formvalue = arr[1];
                $("#site").empty();

                $("#site").append($("<option>").val(formvalue).text(formsite));
            }
            $("#remark").attr("placeholder", "请输入提交说明；");
            $("input[id*='unit']").val($("#unit").val() == "" ? "EA" : $("#unit").val());

            $("#dhperiod").val($("#dhperiod").val() == "" ? "7" : $("#dhperiod").val());
            $("#purchase_days ").val($("#purchase_days ").val() == "" && $("#pt_pm_code").val() == "L" ? "0" : $("#purchase_days ").val());
            $("#cpleibie").val($("#cpleibie").val() == "" ? "A" : $("#cpleibie").val());
            //            $("#ischeck").prop('checked', 'checked');
            //            $("#ischeck").attr("disabled", "disabled");
            $("#part_no").attr("disabled", "disabled");
            if ($("#Formno").val() == "") {
                $("#part_no").css("background-color", "Wheat");
            }


            $("#caigou_uid").css("display", "none");
            $("#bz_uid").css("display", "none");


            //    获取参数名值对集合Json格式
            var url = window.parent.document.URL;
            paramMap = getURLParams(url);
            if (paramMap.wlh != NaN && paramMap.wlh != "" && paramMap.wlh != undefined) {
                $("#wlh").remove("ondblclick");

            }

            var arr = $("#line").find("option:selected").text().split('-');

            if ((paramMap.state == "edit")) {
                $("#parthz").attr("disabled", "disabled");
                $("#parthz ").removeClass("linewrite").addClass("line");
                $("#gc_version").attr("disabled", "disabled");
                $("#bz_version").attr("disabled", "disabled");
            }

            //如果为修改，显示红色
            //            if ((paramMap.state == "edit") || ($("#formstate").val().indexOf("edit") != -1))
            //            { $("#warning").css("display", ""); }
            //项目号变更
            $("#wlh").change(function () {

                $("#wlh").val("");
                $("#ms").val("");
                $("#wlmc").val("");
            });
            $("#cpgroup").change(function () {
                if ($("#line").val() == "")
                { layer.alert("请先选择产品类"); return false; }
            });

//            $("#lilun_jzweight").change(function () { 
//            $("#jzweight").val($("#lilun_jzweight").val())
//            });
            //获取物料号
            $("select[id*='parthz']").change(function () {
                if ($(this).val() != "") {
                    if ($("#wlh").val() == "" || $("#line").val() == "") {
                        layer.alert("请先选择项目号和产品类!");
                        $("#parthz").val("");
                        return false;
                    }
                    else {
                        $("#part_no").val($("#wlh").val() + $("#gc_version").val() + $("#bz_version").val() + "-" + $(this).val());
                    }
                }

            });

            //获取分销网代码下拉值
            $("select[id*='isfx']").change(function () {
                if ($("#isfx").attr("readonly") != "readonly") {
                    var company = "";
                    if ($("#checkbox1").prop("checked") == true)
                    { company = "200" } else { company = "100" }
                    // $("#site").val(company);
                    $("#comp").val(company);
                    $("#fxcode").val("");
                    bindSelect(company, $("#isfx").val());
                }
            })
            //根据产品大类，自动带出采购/制造和组
            $("select[id*='line']").change(function () {
                $("#part_no").val($("#wlh").val() + $("#gc_version").val() + $("#bz_version").val());
                var arr = $("#line").find("option:selected").text().split('-');
                $("#pt_pm_code").empty();
                if (arr[1] == "铁") {
                    $("#cpgroup").get(0).selectedIndex = 2
                    $("#cpgroup").attr("disabled", "disabled");
                }
                else if (arr[1] == "铝") {
                    $("#cpgroup").get(0).selectedIndex = 1
                    $("#cpgroup").attr("disabled", "disabled");
                }
                else {
                    $("#cpgroup").get(0).selectedIndex = 1
                    $("#cpgroup").removeAttr("disabled");
                }
                if (arr[0] == "原材料") {
                    $("#pt_pm_code").append($("<option>").val("P").text("P"));
                    $("#div_FX").css("display", "none");
                }
                else {
                    $("#pt_pm_code").append($("<option>").val("L").text("L"));
                    $("#div_FX").css("display", "inline-block");

                }
                if (arr[0] == "产成品") {
                    $("#picversion ").removeAttr("readonly").removeClass("lineread").addClass("linewrite");
                    $("#parthz").val("");
                    $("#parthz").attr("disabled", "disabled");
                    $("#parthz ").removeClass("linewrite").addClass("line");

                }
                else {
                    $("#parthz").removeAttr("disabled").removeClass("line").addClass("linewrite");
                }
            })


            $("select[id*='pt_pm_code']").change(function () {
                var pm_code = $("#pt_pm_code").find("option:selected").val();

                if (pm_code.substring(0, 1) == "P") {
                    $("#purchase_days").removeAttr("readonly").removeClass("lineread").addClass("linewrite");
                    $("#make_days").attr("readonly", "readonly").removeClass("linewrite").addClass("lineread");
                }
                else if (pm_code.substring(0, 1) == "L") {
                    $("#make_days").removeAttr("readonly").removeClass("lineread").addClass("linewrite");
                    $("#purchase_days ").attr("readonly", "readonly").removeClass("linewrite").addClass("lineread");

                }
                else {
                    $("#make_days").removeAttr("readonly");
                    $("#purchase_days").removeAttr("readonly");
                    $("#purchase_days").removeClass("lineread").removeClass("linewrite");
                    $("#make_days").removeClass("lineread").removeClass("linewrite");
                    $("#purchase_days").addClass("linewrite");
                    $("#make_days").addClass("linewrite");
                }

            })

            $("select[id*='gc_version']").change(function () {
                if ($("#parthz").val() != "") {
                    $("#part_no").val($("#wlh").val() + $("#gc_version").val() + $("#bz_version").val() + "-" + $("#parthz").val());
                }
                else {
                    $("#part_no").val($("#wlh").val() + $("#gc_version").val() + $("#bz_version").val());
                }
            });

            $("select[id*='bz_version']").change(function () {
                if ($("#parthz").val() != "") {
                    $("#part_no").val($("#wlh").val() + $("#gc_version").val() + $("#bz_version").val() + "-" + $("#parthz").val());
                }
                else {
                    $("#part_no").val($("#wlh").val() + $("#gc_version").val() + $("#bz_version").val());
                }
            });


            $("select[id*='site']").change(function () {
                if ($("#isfx").val() == "Y") {
                    $("#fxcode").val($("#site").val());
                }

                var fxcode = $("#isfx").val();
                if (fxcode == "") {

                    layer.alert("请先选择是否分销点！");
                    $("#fxcode").val("");
                    $("#site").val("");
                    return false;
                }


            })


        });



        function BindBuyer(company,pmcode) {
            //var domain = $("#domain").val();
          //  alert(pmcode);
            $.ajax({
                type: "Post", async: false,
                url: "Wuliao_mstr.aspx/Getbuyer",
                //方法传参的写法一定要对，str为形参的名字,str2为第二个形参的名字
                //P1:wlh P2： 
                data: "{'P1':'" + company + "','P2':'" + pmcode + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {//返回的数据用data.d获取内容//                        
                    //   alert(data.d)
                    //$("#buyer_planner").empty();
                    $.each(eval(data.d), function (i, item) {
                        if (data.d == "") {
                            layer.msg("未获取到计划员.");
                        }
                        else {
                            var option = $("<option>").val(item.value).text(item.text);
                            $("#buyer_planner").append(option);

                        }
                    })
                },
                error: function (err) {
                    layer.alert(err);
                }
            });

        }
     

        function bindSelect(company,sel) {
            //var domain = $("#domain").val();
            $.ajax({
                type: "Post", async: false,
                url: "Wuliao_mstr.aspx/GetFxCode",
                //方法传参的写法一定要对，str为形参的名字,str2为第二个形参的名字
                //P1:wlh P2： 
                data: "{'P1':'" + company + "','P2':'" + sel + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {//返回的数据用data.d获取内容//                        
                   // alert(data.d)
                    $("#site").empty();
                    if (sel == "Y") {
                        $("#site").append($("<option>").val("").text(""));
                    }
                    $.each(eval(data.d), function (i, item) {
                        if (data.d == "") {
                            layer.msg("未获取到分销网代码.");
                        }
                        else {
                            var option = $("<option>").val(item.value).text(item.text);
                            $("#site").append(option);
                        }
                    })
                },
                error: function (err) {
                    layer.alert(err);
                }
            });

        }
        //提出自定流程 JS 
        function setComment(val) {
            $('#comment', parent.document).val(val);
        }

        //设定表字段状态（可编辑性）
        var tabName = "PGI_PARTMSTR_DATA_FORM"; //表名
        function SetControlStatus(fieldStatus) {  // tabName_columnName:1_0
            var flag = true;
            for (var item in fieldStatus) {
                var id = item.replace(tabName.toLowerCase() + "_", "");

                if ($("#" + id).length > 0) {
                    var ctype = "";
                    if ($("#" + id).prop("tagName").toLowerCase() == "select") {
                        ctype = "select"
                    } else if ($("#" + id).prop("tagName").toLowerCase() == "textarea") {
                        ctype = "textarea"
                    } else if ($("#" + id).prop("tagName").toLowerCase() == "input") {
                        ctype = $("#" + id).prop("type");

                    }

                    //ctype=(ctype).toLowerCase();

                    var statu = fieldStatus[item];
                    if (statu.indexOf("1_") != "-1" && (ctype == "text" || ctype == "textarea")) {
                        $("#" + id).attr("readonly", "readonly").removeClass("linewrite").addClass("lineread");
                    }
                    else if (statu.indexOf("1_") != "-1" && (ctype == "checkbox" || ctype == "radio" || ctype == "file")) {
                        $("#" + id).attr("disabled", "disabled").removeClass("linewrite").addClass("lineread");
                    }
                    else if (statu.indexOf("1_") != "-1" && ctype == "select")//
                    {
                        $("#" + id).attr("readonly", "readonly").removeClass("linewrite").addClass("lineread");
                        $("#" + id).focus(function () {
                            this.defaultIndex = this.selectedIndex;
                        }).change(function () {
                            this.selectedIndex = this.defaultIndex;
                        })
                    }
                }
            }
        }

     var tabName2="PGI_PartDtl_DATA_Form";//表名
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
             function Get_Isfxcode(vi,ty){
           
            var url = "/select/select_isfx.aspx?vi="+vi+"&ty="+ty;

            layer.open({
                title:'是否分销点选择',
                type: 2,
                area: ['250px', '150px'],
                fixed: false, //不固定
                maxmin: true,
                content: url
            }); 
        }

                //验证

        function setRead(isReadonly) {
            var objs = $("div[id=PB]").find("input,select");
            $.each(objs, function (i, obj) {

                var ctype = "";
                if ($(obj).prop("tagName").toLowerCase() == "select") {
                    ctype = "select"
                } else if ($(obj).prop("tagName").toLowerCase() == "textarea") {
                    ctype = "textarea"
                } else if ($(obj).prop("tagName").toLowerCase() == "input") {
                    ctype = $(obj).prop("type");
                }

                var statu = isReadonly;
                if (statu == true) {
                    if (statu == true && (ctype == "text" || ctype == "textarea")) {
                        $(obj).attr("readonly", "readonly");
                        $(obj).removeAttr("onclick").removeAttr("ondblclick").removeAttr("onfocus").attr("data-toggle", "");
                        //$(obj).css("border", "none").css("background", "transparent");
                         $(obj).removeClass("linewrite").addClass("lineread");
                    }
                    else if (statu == true && (ctype == "checkbox" || ctype == "radio" || ctype == "select" || ctype == "file" )) {
                        $(obj).attr("disabled", "disabled");
                        $(obj).removeClass("linewrite").addClass("lineread");
                    }
                    else if (statu == true && (ctype == "input")) {
                        $(obj).attr("type", "hidden");
                    }
                }
                else {
                    window.location.reload();
                }

            });
        }

        function setRead(objs,isReadonly) {
           // var objs = $("div[id=PB]").find("input,select");
            $.each(objs, function (i, obj) {

                var ctype = "";
                if ($(obj).prop("tagName").toLowerCase() == "select") {
                    ctype = "select"
                } else if ($(obj).prop("tagName").toLowerCase() == "textarea") {
                    ctype = "textarea"
                } else if ($(obj).prop("tagName").toLowerCase() == "input") {
                    ctype = $(obj).prop("type");
                }

                var statu = isReadonly;
                if (statu == true) {
                    if (statu == true && (ctype == "text" || ctype == "textarea")) {
                        $(obj).attr("readonly", "readonly");
                        $(obj).removeAttr("onclick").removeAttr("ondblclick").removeAttr("onfocus").attr("data-toggle", "");
                        //$(obj).css("border", "none").css("background", "transparent");
                        $(obj).removeClass("linewrite").addClass("lineread");
                    }
                    else if (statu == true && (ctype == "checkbox" || ctype == "radio" || ctype == "select" || ctype == "file")) {
                        $(obj).attr("disabled", "disabled");
                        $(obj).removeClass("linewrite").addClass("lineread");
                    }
                    else if (statu == true && (ctype == "input")) {
                        $(obj).attr("type", "hidden");
                    }
                }
                else {
                    window.location.reload();
                }

            });
        }

            

        
    
    </script>
    <script type="text/javascript">
        function SetGridbtn() {

            var url = window.parent.document.URL;
            paramMap = getURLParams(url);
            var role = $("#role").val();
            //alert(role);
            if (paramMap.state != "edit" && role != "jihua") {
                $("[id$=gv_d] input[id*=btn]").each(function () {
                    $(this).prop("disabled", "disabled");
                    $("[id$=gv_d] input[type='text']").each(function () {

                        $(this).attr("readonly", "readonly").addClass("lineread");

                        $("[id$=gv_d] input[id*=ddbs]").removeAttr("readonly").removeClass("lineread").addClass("linewrite");
                    });

                    $("[id$=gv_d] tr[class*=DataRow]").each(function (index, item) {
                        $("[id$=gv_d] input[type!=hidden][id*=buyer_planner]").each(function () {
                            (eval('buyer_planner' + index)).SetEnabled(false);
                        });
                        $("[id$=gv_d] input[type!=hidden][id*=site]").each(function () {
                            (eval('site' + index)).SetEnabled(false);
                        });
                    });


                });
            }
            else if (paramMap.state != "edit" && role == "jihua") {
                $("[id$=gv_d] tr[class*=DataRow]").each(function (index, item) {

                    $("[id$=gv_d] input[type!=hidden][id*=site]").each(function () {
                        var site = eval('site' + index);
                        var codevalue = $.trim(site.GetValue());
                        if (codevalue == "100" || codevalue == "200")
                        { (eval('site' + index)).SetEnabled(false); }

                    });
                });
            }

            if (paramMap.state == "edit") {
                var type = $("#type").find("option:selected").val();
                if (type == "主数据修改" && type != "") {
                    $("#warning").css("display", "");
                    $("#warnmsg").css("display", "none");
                    $("#div_FX").css("display", "none");


                    $("#PB input[type='text']").removeAttr("readonly").removeClass("lineread").addClass("linewrite");
                    $("#PB select[id!='type'] ").removeAttr("readonly").removeClass("lineread").addClass("linewrite");
                    $("#PC input[type='text']").removeAttr("readonly").removeClass("lineread").addClass("linewrite");
                    $("#PC select[id!='type'] ").removeAttr("readonly").removeClass("lineread").addClass("linewrite");
                    $("#status").attr("readonly", "readonly").removeClass("linewrite").addClass("lineread");
                    $("#fyweight").attr("readonly", "readonly").removeClass("linewrite").addClass("lineread");
                    $("#unit").attr("readonly", "readonly").removeClass("linewrite").addClass("lineread");
                    $("#dl").attr("readonly", "readonly").removeClass("linewrite").addClass("lineread");
                }
                else if (type == "数据管理员修改" && type != "") {

                    $("#div_FX").css("display", "none");
                    $("#div_pc").css("display", "none");
                    // setRead(true);
                    setRead($("div[id=PB]").find("input,select"), true);
                    setRead($("div[id=PC]").find("input,select"), true);
                    $("#remark").removeAttr("readonly").removeClass("lineread").addClass("linewrite");
                }
                else if (type == "分销点修改" || type == "") {
                    $("#div_FX").css("display", "block");
                    $("[id$=gv_d] input[type='text']").css("background-color", "#FDF7D9");
                    $("[id$=gv_d] input[id*='ddbs']").css("background-color", "LightGray").attr("readonly", "readonly");

                    //   $("[id$=gv_d] input[type!=hidden][id*=buyer_planner]")
                    $("#warning").css("display", "none");
                    $("#warnmsg").css("display", "");
                    //                         $("[id$=gv_d] input[id*=btn]").each(function () {
                    //   $(this).removeAttr("disabled");
                    $("#PB input[type='text']").attr("readonly", "readonly").removeClass("linewrite").addClass("lineread");
                    $("#PB select[id!='type'] ").attr("readonly", "readonly").removeClass("linewrite").addClass("lineread");
                    $("#PC input[type='text']").attr("readonly", "readonly").removeClass("linewrite").addClass("lineread");
                    $("#PC select[id!='type'] ").attr("readonly", "readonly").removeClass("linewrite").addClass("lineread");
                    $("#remark").removeAttr("readonly").removeClass("lineread").addClass("linewrite");
                    $("[id$=gv_d] tr[class*=DataRow]").each(function (index, item) {

                        $("[id$=gv_d] input[type!=hidden][id*=site]").each(function () {
                            var site = eval('site' + index);
                            var codevalue = $.trim(site.GetValue());
                            if (codevalue == "100" || codevalue == "200")
                            { (eval('site' + index)).SetEnabled(false); }

                        });
                    });
                    //                         });
                }
            }
        }
        //取项目号
        function GetWLH_Product() {
            var url = "../../select/select_pgino.aspx";

            layer.open({
                title: '产品项目号选择',
                type: 2,
                area: ['1000px', '600px'],
                fixed: false, //不固定
                maxmin: true,
                content: url
            });
        }



        //自动获取分销点代码和分销点提前期
        function Getcode(vi) {
            // alert(vi);
            //            var site = eval('site' + vi);
            //            var code = eval('fxcode' + vi);
            //            var aqtj = eval('aqtj_wuliu' + vi);
            //            code.SetText($.trim(site.GetValue()));
            //            var comp = $("#comp").val();
            //            code = $.trim(site.GetValue());
            var site = eval('site' + vi);
            var code = eval('fxcode' + vi);
            var aqtj = eval('aqtj_wuliu' + vi);
            codevalue = $.trim(site.GetValue());
            if (($.trim(site.GetValue())) == "100" || ($.trim(site.GetValue())) == "200") {
                codevalue = "";
            }
            code.SetText(codevalue);
            // code.SetText($.trim(site.GetValue()));
            var comp = $("#comp").val();
            code = $.trim(site.GetValue());

            $.ajax({
                type: "post",
                url: "Wuliao_mstr.aspx/GetPeriod",
                data: "{'fxcode':'" + code + "','comp':'" + comp + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false, //默认是true，异步；false为同步，此方法执行完在执行下面代码
                success: function (data) {//返回的数据用data.d获取内容// 
                    var obj = eval(data.d);
                    var aqtqvalue = obj[0].aqtqvalue;
                    //alert(aqtqvalue);
                    aqtj.SetText(aqtqvalue);
                },
                error: function (err) {
                    layer.alert(err);
                }
            });

        }
        function setvalue_product(lspgino, lsproductcode, lsproductname, lsdl, bz, caigou) {
            $("input[id*='wlh']").val(lspgino);
            $("input[id*='wlmc']").val(lsproductcode);
            $("input[id='ms']").val(lsproductname);
            $("input[id='dl']").val(lsdl);
            $("input[id='bz_uid']").val(bz);
            $("input[id='caigou_uid']").val(caigou);

            var dl = $("#dl").val();
            var comp = $("#comp").val();


            $.ajax({
                type: "Post", async: false,
                url: "Wuliao_mstr.aspx/GetDL",
                //方法传参的写法一定要对，str为形参的名字,str2为第二个形参的名字
                //P1:wlh P2： 
                data: "{'P1':'" + dl + "','P2':'" + comp + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {//返回的数据用data.d获取内容//                        
                    // alert(data.d)
                    $("#line").empty();
                    $("#line").append($("<option>").val("").text(""));
                    $.each(eval(data.d), function (i, item) {
                        if (data.d == "") {
                            layer.msg("未获取到产品类.");
                        }
                        else {
                            var option = $("<option>").val(item.value).text(item.text);
                            $("#line").append(option);
                        }
                    })
                },
                error: function (err) {
                    layer.alert(err);
                }
            });
        }



       
    </script>
    <style type="text/css">
        .lineread
        {
            font-size: 12px;
            border: none;
            border-bottom: 1px solid lightgray;
            background-color: #ffffff;
            width: 90%;
        }
        .line
        {
            font-size: 12px;
            border: none;
            border-bottom: 1px solid lightgray;
            width: 90%;
        }
        .linewrite
        {
            font-size: 12px;
            border: none;
            border-bottom: 1px solid #ccc;
            background-color: #FDF7D9;
            width: 90%; /*EFEFEF*/
        }
        /*.dxeTextBox .dxeEditArea{
            background-color:#FDF7D9;
        }*/
        .i_hidden
        {
            display: none;
        }
        .i_show
        {
            display: inline-block;
        }
    </style>
</head>
<body>
    <script type="text/javascript">	  
        var fieldStatus = "1"=="<%=Request.QueryString["isreadonly"]%>"? {} : <%=fieldStatus%>;
        var displayModel = '<%=DisplayModel%>';
        var comp = '<%=ViewState["comp"].ToString() %>';
        var UidRole = '<%=ViewState["UidRole"].ToString() %>'; 
        $(window).load(function (){
            SetControlStatus(<%=fieldStatus%>);
            SetControlStatus2(<%=fieldStatus%>);
            
              if ((comp != "" )) { BindBuyer(comp,$("#pt_pm_code").val());}      
            
              if( '<%=Session["lv"].ToString() %>'!="caigou" && '<%=Session["lv"].ToString() %>'!="product")
              {
                  $("input[id*='ddlopinion']").attr("disabled", "true");
              }
              if('<%=Session["lv"].ToString() %>'=="caigou" )
              {
              $("#ddlopinion").find('input:checkbox').eq(1).attr("disabled", "true");
              $("#ddlopinion").find('input:checkbox').eq(2).attr("disabled", "true");
              }
                if('<%=Session["lv"].ToString() %>'=="product" )
              {
              $("#ddlopinion").find('input:checkbox').eq(0).attr("disabled", "true");
              }
        });




            function validate(action){
           var flag=true;var msg="";
        
            <%=ValidScript%>

            var wlmc=$("#wlmc").val();
            var ms=$("#ms").val();
            var result_mc = wlmc .replace(/[^\x00-\xff]/g, '**');
             var result_ms = ms .replace(/[^\x00-\xff]/g, '**');
//            alert(result.length);
//            return false;
             if(result_mc.length>24){
               msg+="零件号字符长度大于24，请修改.<br />";
             }

               if(result_ms.length>24){
               msg+="物料描述字符长度大于24，请修改.<br />";
             }
            var jzvalue=$("#jzweight").val();
//             var re = /^(-?\d+)(\.\d+)?$/

//            if(re.test(jzvalue)==false)
//            {
//              msg+="【净重】请输入数值型.<br />";
//            }

             if($("#type").val()==""){
             msg+="请选择【物料申请类型】.<br />";
            } 
            if($("#remark").val()==""){
             msg+="【提交说明】不可为空.<br />";
            } 
                 if($("#pt_status").val()==""){
             msg+="请选择【产品状态(申请)】.<br />";
            } 
                 if($("#line").val()==""){
             msg+="请选择【产品类】.<br />";
            } 

             if( $("#buyer_planner").attr("readonly")!="readonly"&& $("#buyer_planner").val()=="")          
             {  msg+="请选择主地点【采购员/计划员】.<br />"};

            if( $("#isfx").attr("readonly")!="readonly"&& $("#isfx").val()=="")
              {  msg+="请选择【是否分销点】.<br />"};

             if( $("#site").attr("readonly")!="readonly"&& $("#site").val()=="")
                {  msg+="请选择【地点】.<br />"};

             if($("#pt_pm_code").attr("readonly")!="readonly"  && $("#pt_pm_code").val()=="")
              {  msg+="请选择【采购/制造】.<br />"};

              if( $("#aqtj_wuliu").attr("readonly")!="readonly"&& $("#aqtj_wuliu").val()=="")
                 {  msg+="请填写主地点【安全提前期】.<br />"};

  

             if( $("#quantity_min").attr("readonly")!="readonly" && $("#quantity_min").val()=="" )
             {  msg+="请填写主地点【最小订单量】.<br />"};   
             
             if( $("#quantity_max").attr("readonly")!="readonly" && $("#quantity_max").val()=="" )
                {  msg+="请填写主地点【最大订单量】.<br />"};   

               
                     if( $("#status").attr("readonly")!="readonly" && $("#status").val()=="" )
                {  msg+="请选择【产品状态(财务)】.<br />"};   

              if( $("#ddbs").attr("readonly")!="readonly" && $("#ddbs").val()=="" )
               {  msg+="请填写主地点【订单倍数】.<br />"};   

 
                if( $("#ddsl").attr("readonly")!="readonly" && $("#ddsl").val()==""  )
               {  msg+="请填写正确主地点【订单数量】.<br />"};   


                     if( $("#bzl").attr("readonly")!="readonly" && $("#bzl").val()==""  )
               {  msg+="请填写主地点【小包装量】.<br />"};   


              if( $("#dhperiod").attr("readonly")!="readonly" && $("#dhperiod").val()=="" )
               {  msg+="请填写主地点【订货周期】.<br />"};   

               if( $("#make_days").attr("readonly")!="readonly"&& $("#pt_pm_code").find("option:selected").val().substring(0,1)=="L"
             && $("#make_days").val()=="" )
             {  msg+="请填写主地点【制造提前期】.<br />"};  

               if( $("#purchase_days").attr("readonly")!="readonly" && $("#purchase_days").val()=="" )
             {  msg+="请填写主地点【采购提前期】.<br />"};  

             if( $("#fyweight").attr("readonly")!="readonly" && $("#fyweight").val()=="" )
             {  msg+="请填写主地点【发运重量】.<br />"};  


//             if( $("#fyunit").attr("readonly")!="readonly" && $("#fyunit").val()=="" )
//              {  msg+="请填写主地点【发运单位】.<br />"};  


             if( $("#lilun_jzweight").attr("readonly")!="readonly" && $("#lilun_jzweight").val()=="" )  
             {  msg+="请填写主地点【理论净重】.<br />"};  


             if( $("#jzweight").attr("readonly")!="readonly" && $("#jzweight").val()=="" )  
             {  msg+="请填写主地点【实际净重】.<br />"};  

//              if( $("#jzunit").attr("readonly")!="readonly" && $("#jzunit").val()=="" )
//              {  msg+="请填写【净重单位】.<br />"};  


             if($("#bz_uid").val()=="")
              {  msg+="产品清单中【包装工程师】不存在，请确认.<br />"}; 


             if($("#line").val()!="")
             {
             var line=$("#line").val();
             if(line.substring(0,1)=="1" && $("#caigou").val()=="") 
             {
            msg+="产品清单中【采购工程师】不存在，请确认.<br />"
             }
             } 
        //     var UidRole = '<%=ViewState["UidRole"].ToString() %>'; 

                if(action=='submit' && UidRole=="jihua"){
                    $("[id$=gv_d] input[type!=hidden][id*=buyer_planner]").each(function (){
                  //   alert($(this).val())
                        if( $(this).val()==""){
                       
                            msg+="请选择分销点【采购员/计划员】.<br />";
                            return false;
                        }
                    });
                    $("[id$=gv_d] input[type!=hidden][id*=site]").each(function (){
                        if( $(this).val()==""){
                            msg+="请选择分销点【地点】.<br />";
                            return false;
                        }
                    });
                     $("[id$=gv_d] input[id*=dhperiod]").each(function (){
                        if( $(this).val()==""){
                            msg+="请填写分销点【订货周期】.<br />";
                            return false;
                        }
                    });
                    $("[id$=gv_d] input[id*=aqtj_wuliu]").each(function (){
                        if( $(this).val()==""){
                            msg+="请填写分销点【安全提前期】.<br />";
                            return false;
                        }
                    });
                        $("[id$=gv_d] input[id*=make_days]").each(function (){
                        if( $(this).val()==""){
                            msg+="请填写分销点【制造提前期】.<br />";
                            return false;
                        }
                    });
                    }
                    
                  else if(action=='submit' && (UidRole=="caigou" )){
                    $("[id$=gv_d] input[id*=quantity_min]").each(function (){
                        if( $(this).val()==""){
                            msg+="请填写分销点【最小订单量】<br />";
                            return false;
                        }
                    });

                        if ($("#ddlopinion").find('input:checkbox').eq(0).prop("checked") == false)
                        { 
                          msg+="请确认采购单价格是否已完成，若完成请勾选完再提交<br />";
                        }

                    $("[id$=gv_d] input[id*=quantity_max]").each(function (){
                        if( $(this).val()==""){
                            msg+="请填写分销点【最大订单量】.<br />";
                            return false;
                        }
                    });
                     $("[id$=gv_d] input[id*=ddbs]").each(function (){
                        if( $(this).val()==""){
                            msg+="请填写分销点【订单倍数】.<br />";
                            return false;
                        }
                    });
                       $("[id$=gv_d] input[id*=ddsl]").each(function (){
                        if( $(this).val()==""){
                            msg+="请填写分销点【订单数量】.<br />";
                            return false;
                        }
                    });
                     $("[id$=gv_d] input[id*=purchase_days]").each(function (){
                        if( $(this).val()==""){
                            msg+="请填写分销点【采购提前期】.<br />";
                            return false;
                        }
                    });
                    }

                     else if(action=='submit' && (UidRole=="bz" )){
                    
                     $("[id$=gv_d] input[id*=ddbs]").each(function (){
                        if( $(this).val()==""){
                            msg+="请填写分销点【订单倍数】.<br />";
                            return false;
                        }
                    });

                  
                    }
                         else if(action=='submit' && (UidRole=="product" )){
                     if ($("#ddlopinion").find('input:checkbox').eq(1).prop("checked") == false || 
                     $("#ddlopinion").find('input:checkbox').eq(2).prop("checked") == false)
                        { 
                          msg+="请确认工艺和BOM是否已完成，若完成请勾选完再提交<br />";
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

                var wlh=$("#wlh").val();
                var line=$("#line").val().substring(0,1);
                var gc=$("#gc_version").val();
                var bz=$("#bz_version").val();
                var comp=$("#comp").val();
                var formno=$("#Formno").val();
                var part_no=$("#part_no").val();
                 var status=$("#type").val();
                var state=paramMap.state;
                var createuid=$("#CreateById").val();
                var pt_pm_code=$("#pt_pm_code").val();
                var aprover='<%=Session["lv"].ToString() %>';
               // alert(createuid);
               // alert(paramMap.state);
                // return false;

               $.ajax({
                   type: "post",
                    url: "Wuliao_mstr.aspx/CheckData",
                    data: "{'xmh':'" + wlh + "','cpleibie':'" + line + "','gc':'" + gc + "' ,'bz':'" + bz + "' , 'comp':'" + comp + "', 'formno':'" + formno + "', 'part_no':'" + part_no + 
                    "', 'state':'" + state + "', 'status':'" + status + "', 'aprover':'" + aprover + "', 'createuid':'" + createuid + "', 'pt_pm_code':'" + pt_pm_code + "'}",
                   contentType: "application/json; charset=utf-8",
                   dataType: "json",
                    async: false,//默认是true，异步；false为同步，此方法执行完在执行下面代码
                    success: function (data) {
                       var obj=eval(data.d);

                       if(obj[0].check_flag!=""){ msg+=obj[0].check_flag; }
                        
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
    <form id="form1" runat="server" enctype="multipart/form-data">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="h4" style="margin-left: 10px" id="headTitle">
        PGI管理系统<div class="btn-group">
            <div class="area_drop" data-toggle="dropdown">
                <span class="caret"></span>
            </div>
            <ul class="dropdown-menu" style="color: Black" role="menu">
                <li><a href="#">铁机加区</a></li>
                <li><a href="#">铝机加区</a></li>
                <li><a href="#">压铸区</a></li>
            </ul>
        </div>
        <span id="mestitle"></span>
        <div style="float: right; margin-right: 10px; font-size: 10px">
        </div>
    </div>
    <div class="col-md-12  ">
        <div class="col-md-10  ">
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
                        <asp:Button ID="btnSave" runat="server" Text="保存" CssClass="btn btn-default btn-xs btnSave"
                            OnClientClick="if($('#cpleibie').val()=='' || $(':checkbox').is(':checked')==false){ layer.alert('请选择【申请工厂】和【产品类】.');return false; }"
                            OnClick="btnSave_Click" ToolTip="临时保存此流程" Height="23px" />
                        <asp:Button ID="btnflowSend" runat="server" Text="发送" CssClass="btn btn-default btn-xs btnflowSend"
                            OnClientClick="return validate('submit');" OnClick="btnflowSend_Click" />
                        <input id="btnaddWrite" type="button" value="加签" onclick="parent.addWrite(true);"
                            class="btn btn-default btn-xs btnaddWrite" />
                        <input id="btnflowBack" type="button" value="退回" onclick="parent.flowBack(true);"
                            class="btn btn-default btn-xs btnflowBack" />
                        <input id="btnflowCompleted" type="button" value="完成" onclick="parent.flowCompleted(true);"
                            class="btn btn-default btn-xs btnflowCompleted" />
                        <input id="btnshowProcess" type="button" value="查看流程" onclick="parent.showProcess(true);"
                            class="btn btn-default btn-xs btnshowProcess" />
                        <input id="btntaskEnd" type="button" value="终止流程" onclick="parent.taskEnd(true);"
                            class="btn btn-default btn-xs btntaskEnd" />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
    <asp:TextBox ID="txtInstanceID" runat="server" CssClass=" hidden"
        ToolTip="0|0" Width="40" />
    <div class="col-md-10">
        <div class="row row-container">
            <div class="col-md-12">
                <div class="panel panel-info">
                    <div class="panel-heading" data-toggle="collapse" data-target="#SQXX">
                        <strong>申请人信息</strong>
                    </div>
                    <div class="panel-body collapse out" id="SQXX">
                        <div class="col-xs-12 col-sm-12  col-md-12 col-lg-12">
                            <div class="">
                                <asp:UpdatePanel ID="UpdatePanel_request" runat="server">
                                    <ContentTemplate>
                                        <table style="height: 30px; width: 100%">
                                            <tr>
                                                <td style="width: 80px">
                                                    申请人
                                                </td>
                                                <td style="width: 250px">
                                                    <div class="form-inline">
                                                        <asp:TextBox runat="server" ID="CreateById" class="line" Style="width: 90px;
                                                            font-size: 12px" ReadOnly="True"></asp:TextBox>
                                                        <asp:TextBox runat="server" ID="CreateByName" class="line" Style="width: 90px;
                                                            font-size: 12px" ReadOnly="True"></asp:TextBox>
                                                    </div>
                                                </td>
                                                <td style="width: 80px">
                                                    申请部门
                                                </td>
                                                <td style="width: 250px">
                                                    <div class="form-inline">
                                                        <asp:TextBox runat="server" ID="CreateByDept" class="line" Style="width: 90px;
                                                            font-size: 12px" ReadOnly="True"></asp:TextBox>
                                                    </div>
                                                </td>
                                                <td style="width: 80px">
                                                    申请时间
                                                </td>
                                                <td style="width: 250px">
                                                    <asp:TextBox runat="server" ID="CreateDate" class="line" Style="width: 180px;
                                                        font-size: 12px" ReadOnly="True"></asp:TextBox>
                                                </td>
                                                <td style="width: 80px">
                                                    表单号
                                                </td>
                                                <td style="width: 250px">
                                                    <asp:TextBox runat="server" ID="Formno" class="line" Style="width: 180px;
                                                        font-size: 12px" ReadOnly="True"></asp:TextBox>
                                                    <asp:TextBox ID="formstate" runat="server" CssClass=" hidden"
                                                        ToolTip="0|0" Width="40" />
                                                    <asp:TextBox ID="formsite" runat="server" CssClass=" hidden"
                                                        ToolTip="0|0" Width="40" />
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
                        <strong>物料类别</strong>
                    </div>
                    <div class="panel-body " id="PA">
                        <div class="col-xs-12 col-sm-12  col-md-12 col-lg-12">
                            <div class="">
                                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                    <ContentTemplate>
                                        <script type="text/jscript">
                                            var prm = Sys.WebForms.PageRequestManager.getInstance();
                                            prm.add_endRequest(function () {
                                                $("select[id*='type']").change(function () {
                                                    //  alert($(this).val());
                                                    gv_d.PerformCallback($(this).val());

                                                    if ($("select[id*=type]").val() == "数据管理员修改") {
                                                        // setRead(true);
                                                        setRead($("div[id=PB]").find("input,select"), true);
                                                        setRead($("div[id=PC]").find("input,select"), true);
                                                    }
                                                });
                                                SetGridbtn();

                                            });
                                        </script>
                                        <table style="width: 80%" border="0" runat="server" id="tblWLLeibie">
                                            <tr>
                                                <td>
                                                    申请工厂
                                                </td>
                                                <td>
                                                    <input id="Checkbox1" type="checkbox" value="200" runat="server"
                                                        name="fac" />昆山工厂 &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                    <input id="Checkbox2" type="checkbox" value="100" runat="server"
                                                        name="fac" />上海工厂
                                                    <asp:TextBox ID="comp" runat="server" ToolTip="0|0" Width="40"
                                                        CssClass=" hidden" />
                                                </td>
                                                <td>
                                                    物料申请类型
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="type" runat="server" CssClass="form-control"
                                                        ToolTip="0|1" Width="180px">
                                                        <asp:ListItem Value="新增">新增</asp:ListItem>
                                                        <asp:ListItem Value="主数据修改">主数据修改</asp:ListItem>
                                                        <asp:ListItem Value="分销点修改">分销点修改</asp:ListItem>
                                                        <asp:ListItem Value="数据管理员修改">数据管理员修改</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:TextBox ID="applytype" runat="server" ToolTip="0|0" Width="40"
                                                        CssClass=" hidden" />
                                                    <asp:TextBox ID="role" runat="server" ToolTip="0|0" Width="40"
                                                        CssClass=" hidden" />
                                                    <asp:TextBox ID="opinion" runat="server" ToolTip="0|0" Width="40"
                                                        CssClass=" hidden" />
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
            <div class="row  row-container">
                <div class="col-md-12">
                    <div class="panel panel-info">
                        <div class="panel-heading" data-toggle="collapse" data-target="#PB">
                            <strong style="padding-right: 100px">物料属性(工程)<span id="warning"
                                style="color: red; display: none">此单为物料主数据修改，请谨慎.</span></strong>
                        </div>
                        <div class="panel-body  collapse in" id="PB">
                            <div class="col-xs-12 col-sm-12  col-md-12 col-lg-12">
                                <div>
                                    <asp:Table Style="width: 100%" border="0" runat="server" ID="tblWLShuXing">
                                    </asp:Table>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row  row-container">
                <div class="panel panel-info">
                    <div class="panel-heading" data-toggle="collapse" data-target="#PC">
                        <strong style="padding-right: 100px">物料计划维护(1.4.7)</strong>
                    </div>
                    <div class="panel-body   collapse in" id="PC">
                        <div class="col-xs-12 col-sm-12  col-md-12 col-lg-12">
                            <div>
                                <asp:Table Style="width: 100%" border="0" runat="server" ID="tblWLZShuJu">
                                </asp:Table>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row  row-container" id="div_FX" runat="server">
                <div class="col-md-12">
                    <div class="panel panel-info">
                        <div class="panel-heading" data-toggle="collapse" data-target="#PD">
                            <strong>物料-地点计划维护(1.4.17)<span id="warnmsg" style="color: red; display: none">此单为分销点数据修改，请谨慎.</strong>
                        </div>
                        <div class="panel-body collapse in" id="PD">
                            <div class="col-xs-12 col-sm-12  col-md-12 col-lg-12">
                                <div style="padding: 2px 5px 5px 0px">
                                </div>
                                <asp:UpdatePanel runat="server" ID="p1" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <script type="text/jscript">
                                            var prm = Sys.WebForms.PageRequestManager.getInstance();
                                            prm.add_endRequest(function () {
                                                $("select[id*='type']").change(function () {
                                                    //  alert($(this).val());
                                                    gv_d.PerformCallback($(this).val());
                                                });
                                                SetGridbtn();

                                            });
                                        </script>
                                        <asp:Button ID="btnAdd" runat="server" Text="新增" class="btn btn-default"
                                            Style="width: 60px; height: 32px;" OnClick="btnAdd_Click" />
                                        <asp:Button ID="btndel" runat="server" Text="删除" class="btn btn-default"
                                            Style="width: 60px; height: 32px;" OnClick="btndel_Click" />
                                        <dx:ASPxGridView ID="gv_d" runat="server" AutoGenerateColumns="False"
                                            KeyFieldName="numid" Theme="MetropolisBlue" OnRowCommand="gv_d_RowCommand"
                                            ClientInstanceName="gv_d" EnableTheming="True" OnHtmlRowCreated="gv_d_HtmlRowCreated"
                                            OnCustomCallback="gv_d_CustomCallback">
                                            <ClientSideEvents EndCallback="function(s, e) { SetGridbtn();   }" />
                                            <SettingsPager PageSize="1000">
                                            </SettingsPager>
                                            <Settings ShowFooter="True" />
                                            <SettingsBehavior AllowSelectByRowClick="True" AllowDragDrop="False"
                                                AllowSort="False" />
                                            <Columns>
                                                <dx:GridViewCommandColumn SelectAllCheckboxMode="Page" ShowClearFilterButton="true"
                                                    ShowSelectCheckbox="true" Name="Sel" Width="30" VisibleIndex="1">
                                                </dx:GridViewCommandColumn>
                                                <dx:GridViewDataTextColumn Caption="采购员/计划员" FieldName="buyer_planner"
                                                    Width="140px" VisibleIndex="3">
                                                    <Settings AllowCellMerge="False" />
                                                    <DataItemTemplate>
                                                        <dx:ASPxComboBox ID="buyer_planner" runat="server" ValueType="System.String"
                                                            Width="140px" ClientInstanceName='<%# "buyer_planner"+Container.VisibleIndex.ToString() %>'>
                                                        </dx:ASPxComboBox>
                                                    </DataItemTemplate>
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn Caption="是否分销点" FieldName="isfx" Width="60px"
                                                    VisibleIndex="4">
                                                    <Settings AllowCellMerge="False" />
                                                    <DataItemTemplate>
                                                        <dx:ASPxLabel ID="isfx" runat="server" Text="Y" Width="60px">
                                                        </dx:ASPxLabel>
                                                    </DataItemTemplate>
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn Caption="地点" FieldName="site" Width="80px"
                                                    VisibleIndex="5">
                                                    <Settings AllowCellMerge="False" />
                                                    <DataItemTemplate>
                                                        <dx:ASPxComboBox ID="site" runat="server" ValueType="System.String"
                                                            Width="80px" ClientSideEvents-SelectedIndexChanged='<%# "function(s,e){Getcode("+Container.VisibleIndex+");}" %>'
                                                            ClientInstanceName='<%# "site"+Container.VisibleIndex.ToString() %>'>
                                                        </dx:ASPxComboBox>
                                                    </DataItemTemplate>
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn Caption="分销网代码" FieldName="fxcode"
                                                    Width="80px" VisibleIndex="6">
                                                    <Settings AllowCellMerge="False" />
                                                    <DataItemTemplate>
                                                        <dx:ASPxTextBox ID="fxcode" Width="80px" runat="server" Value='<%# Eval("fxcode")%>'
                                                            ReadOnly="true" ClientInstanceName='<%# "fxcode"+Container.VisibleIndex.ToString() %>'>
                                                        </dx:ASPxTextBox>
                                                    </DataItemTemplate>
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn Caption="采购/制造" FieldName="pt_pm_code"
                                                    Width="40px" VisibleIndex="7">
                                                    <Settings AllowCellMerge="False" />
                                                    <DataItemTemplate>
                                                        <dx:ASPxLabel ID="pt_pm_code" runat="server"  Width="40px" Value='<%# Eval("pt_pm_code")%>' >
                                                        </dx:ASPxLabel>
                                                       
                                                    </DataItemTemplate>
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn Caption="订货方法" FieldName="dhff" Width="60px"
                                                    VisibleIndex="8">
                                                    <Settings AllowCellMerge="False" />
                                                    <DataItemTemplate>
                                                        <dx:ASPxComboBox ID="dhff" runat="server" SelectedIndex="0" Width="60px">
                                                            <Items>
                                                                <dx:ListEditItem Selected="True" Text="POQ" Value="POQ" />
                                                            </Items>
                                                        </dx:ASPxComboBox>
                                                    </DataItemTemplate>
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn Caption="订货周期" FieldName="dhperiod"
                                                    Width="60px" VisibleIndex="9">
                                                    <Settings AllowCellMerge="False" />
                                                    <DataItemTemplate>
                                                        <dx:ASPxTextBox ID="dhperiod" Width="60px" runat="server" Value='<%# Eval("dhperiod")%>'
                                                            ClientSideEvents-ValueChanged='<%# "function(s,e){RefreshRow("+Container.VisibleIndex+");}" %>'
                                                            ClientInstanceName='<%# "dhperiod"+Container.VisibleIndex.ToString() %>'>
                                                            <ValidationSettings ValidationGroup="ValueValidationGroup" Display="Dynamic"
                                                                ErrorTextPosition="Bottom">
                                                                <RegularExpression ErrorText="请输入数字！" ValidationExpression="^-?[1-9]+(\.\d+)?$|^-?0(\.\d+)?$|^-?[1-9]+[0-9]*(\.\d+)?$" />
                                                            </ValidationSettings>
                                                        </dx:ASPxTextBox>
                                                    </DataItemTemplate>
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn Caption="安全提前期" FieldName="aqtj_wuliu"
                                                    Width="60px" VisibleIndex="10">
                                                    <DataItemTemplate>
                                                        <dx:ASPxTextBox ID="aqtj_wuliu" Width="60px" runat="server" Value='<%# Eval("aqtj_wuliu")%>'
                                                            ClientSideEvents-ValueChanged='<%# "function(s,e){RefreshRow("+Container.VisibleIndex+");}" %>'
                                                            ClientInstanceName='<%# "aqtj_wuliu"+Container.VisibleIndex.ToString() %>'>
                                                            <ValidationSettings ValidationGroup="ValueValidationGroup" Display="Dynamic"
                                                                ErrorTextPosition="Bottom">
                                                                <RegularExpression ErrorText="请输入数字！" ValidationExpression="^-?[1-9]+(\.\d+)?$|^-?0(\.\d+)?$|^-?[1-9]+[0-9]*(\.\d+)?$" />
                                                            </ValidationSettings>
                                                        </dx:ASPxTextBox>
                                                    </DataItemTemplate>
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn Caption="最小订单量" FieldName="quantity_min"
                                                    Width="60px" VisibleIndex="12">
                                                    <DataItemTemplate>
                                                        <dx:ASPxTextBox ID="quantity_min" Width="60px" runat="server"
                                                            Value='<%# Eval("quantity_min")%>' ClientSideEvents-ValueChanged='<%# "function(s,e){RefreshRow("+Container.VisibleIndex+");}" %>'
                                                            ClientInstanceName='<%# "quantity_min"+Container.VisibleIndex.ToString() %>'>
                                                            <ValidationSettings ValidationGroup="ValueValidationGroup" Display="Dynamic"
                                                                ErrorTextPosition="Bottom">
                                                                <RegularExpression ErrorText="请输入数字！" ValidationExpression="^-?[1-9]+(\.\d+)?$|^-?0(\.\d+)?$|^-?[1-9]+[0-9]*(\.\d+)?$" />
                                                            </ValidationSettings>
                                                        </dx:ASPxTextBox>
                                                    </DataItemTemplate>
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn Caption="最大订单量" FieldName="quantity_max"
                                                    Width="60px" VisibleIndex="13">
                                                    <Settings AllowCellMerge="False" />
                                                    <DataItemTemplate>
                                                        <dx:ASPxTextBox ID="quantity_max" Width="60px" runat="server"
                                                            Value='<%# Eval("quantity_max")%>' ClientSideEvents-ValueChanged='<%# "function(s,e){RefreshRow("+Container.VisibleIndex+");}" %>'
                                                            ClientInstanceName='<%# "quantity_max"+Container.VisibleIndex.ToString() %>'>
                                                            <ValidationSettings ValidationGroup="ValueValidationGroup" Display="Dynamic"
                                                                ErrorTextPosition="Bottom">
                                                                <RegularExpression ErrorText="请输入数字！" ValidationExpression="^-?[1-9]+(\.\d+)?$|^-?0(\.\d+)?$|^-?[1-9]+[0-9]*(\.\d+)?$" />
                                                            </ValidationSettings>
                                                        </dx:ASPxTextBox>
                                                    </DataItemTemplate>
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn Caption="订单倍数<br>(工单数量/厂内周转包装-仅对于主地点)" FieldName="ddbs" Width="60px"
                                                    VisibleIndex="14">
                                                    <Settings AllowCellMerge="False" />
                                                    <DataItemTemplate>
                                                        <dx:ASPxTextBox ID="ddbs" Width="100px" runat="server" Value='<%# Eval("ddbs")%>'
                                                            ClientSideEvents-ValueChanged='<%# "function(s,e){RefreshRow("+Container.VisibleIndex+");}" %>'
                                                            ClientInstanceName='<%# "ddbs"+Container.VisibleIndex.ToString() %>'>
                                                            <ValidationSettings ValidationGroup="ValueValidationGroup" Display="Dynamic"
                                                                ErrorTextPosition="Bottom">
                                                                <RegularExpression ErrorText="请输入数字！" ValidationExpression="^-?[1-9]+(\.\d+)?$|^-?0(\.\d+)?$|^-?[1-9]+[0-9]*(\.\d+)?$" />
                                                            </ValidationSettings>
                                                        </dx:ASPxTextBox>
                                                    </DataItemTemplate>
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn Caption="订单数量" FieldName="ddsl" Width="60px"
                                                    VisibleIndex="16">
                                                    <Settings AllowCellMerge="False" />
                                                    <DataItemTemplate>
                                                        <dx:ASPxTextBox ID="ddsl" Width="60px" runat="server" Value='<%# Eval("ddsl")%>'
                                                            ClientSideEvents-ValueChanged='<%# "function(s,e){RefreshRow("+Container.VisibleIndex+");}" %>'
                                                            ClientInstanceName='<%# "ddsl"+Container.VisibleIndex.ToString() %>'>
                                                            <ValidationSettings ValidationGroup="ValueValidationGroup" Display="Dynamic"
                                                                ErrorTextPosition="Bottom">
                                                                <RegularExpression ErrorText="请输入数字！" ValidationExpression="^-?[1-9]+(\.\d+)?$|^-?0(\.\d+)?$|^-?[1-9]+[0-9]*(\.\d+)?$" />
                                                            </ValidationSettings>
                                                        </dx:ASPxTextBox>
                                                    </DataItemTemplate>
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn Caption="采购提前期" FieldName="purchase_days"
                                                    Width="60px" VisibleIndex="18">
                                                    <DataItemTemplate>
                                                        <dx:ASPxTextBox ID="purchase_days" Width="60px" runat="server"
                                                            Value='<%# Eval("purchase_days")%>' ClientSideEvents-ValueChanged='<%# "function(s,e){RefreshRow("+Container.VisibleIndex+");}" %>'
                                                            ClientInstanceName='<%# "purchase_days"+Container.VisibleIndex.ToString() %>'>
                                                            <ValidationSettings ValidationGroup="ValueValidationGroup" Display="Dynamic"
                                                                ErrorTextPosition="Bottom">
                                                                <RegularExpression ErrorText="请输入数字！" ValidationExpression="^-?[1-9]+(\.\d+)?$|^-?0(\.\d+)?$|^-?[1-9]+[0-9]*(\.\d+)?$" />
                                                            </ValidationSettings>
                                                        </dx:ASPxTextBox>
                                                    </DataItemTemplate>
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn Caption="制造提前期" FieldName="make_days"
                                                    Width="60px" VisibleIndex="11">
                                                    <DataItemTemplate>
                                                        <dx:ASPxTextBox ID="make_days" Width="60px" runat="server" Value='<%# Eval("make_days")%>'
                                                            ClientSideEvents-ValueChanged='<%# "function(s,e){RefreshRow("+Container.VisibleIndex+");}" %>'
                                                            ClientInstanceName='<%# "make_days"+Container.VisibleIndex.ToString() %>'>
                                                            <ValidationSettings ValidationGroup="ValueValidationGroup" Display="Dynamic"
                                                                ErrorTextPosition="Bottom">
                                                                <RegularExpression ErrorText="请输入数字！" ValidationExpression="^-?[1-9]+(\.\d+)?$|^-?0(\.\d+)?$|^-?[1-9]+[0-9]*(\.\d+)?$" />
                                                            </ValidationSettings>
                                                        </dx:ASPxTextBox>
                                                    </DataItemTemplate>
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="numid" Width="0px">
                                                    <HeaderStyle CssClass="hidden" />
                                                    <CellStyle CssClass="hidden">
                                                    </CellStyle>
                                                    <FooterCellStyle CssClass="hidden">
                                                    </FooterCellStyle>
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="domain" Width="0px">
                                                    <HeaderStyle CssClass="hidden" />
                                                    <CellStyle CssClass="hidden">
                                                    </CellStyle>
                                                    <FooterCellStyle CssClass="hidden">
                                                    </FooterCellStyle>
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="id" Width="0px">
                                                    <HeaderStyle CssClass="hidden" />
                                                    <CellStyle CssClass="hidden">
                                                    </CellStyle>
                                                    <FooterCellStyle CssClass="hidden">
                                                    </FooterCellStyle>
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="formno" Width="0px" VisibleIndex="15">
                                                    <HeaderStyle CssClass="hidden" />
                                                    <CellStyle CssClass="hidden">
                                                    </CellStyle>
                                                    <FooterCellStyle CssClass="hidden">
                                                    </FooterCellStyle>
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="" Caption=" " VisibleIndex="99">
                                                    <Settings AllowCellMerge="False" />
                                                    <DataItemTemplate>
                                                        <dx:ASPxButton ID="btn" runat="server" Text="新增行" CommandName="Add">
                                                        </dx:ASPxButton>
                                                    </DataItemTemplate>
                                                </dx:GridViewDataTextColumn>
                                            </Columns>
                                            <Styles>
                                                <Header BackColor="#E4EFFA">
                                                </Header>
                                                <SelectedRow BackColor="#FDF7D9">
                                                </SelectedRow>
                                            </Styles>
                                        </dx:ASPxGridView>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row  row-container">
                <div class="col-md-12">
                    <div class="panel panel-info">
                        <div class="panel-heading" data-toggle="collapse" data-target="#PE">
                            <strong>签核完成并对接QAD才算完成.</strong><span style="color: Red; font-size: small">(产品状态从Dead修改为AC或Sample的话,产品工程师可直接勾选并签核!)</span>
                        </div>
                        <div class="panel-body  <% =Session["lv"].ToString() != "" ? "" : "collapse" %>"
                            id="PE">
                            <div class="col-xs-12 col-sm-12  col-md-12 col-lg-12">
                                <div>
                                    <table style="width: 80%; height: 100px" border="0" runat="server"
                                        id="Table1">
                                        <tr>
                                            <td style="width: 150px; color: Red;">
                                                处理完请勾选 :
                                            </td>
                                            <td>
                                                <asp:CheckBoxList ID="ddlopinion" runat="server" CssClass="form-control "
                                                    RepeatDirection="Vertical" Width="200px" Height="100px">
                                                </asp:CheckBoxList>
                                            </td>
                                        </tr>
                                    </table>
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
                                        <label>
                                            处理意见：</label>
                                    </td>
                                    <td>
                                        <textarea id="comment" type="text" placeholder="请在此处输入处理意见" class="form-control"
                                            onchange="setComment(this.value)"></textarea>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
