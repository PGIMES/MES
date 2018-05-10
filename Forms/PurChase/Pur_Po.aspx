<%@ Page Title="MES生产管理系统" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" 
    CodeFile="Pur_Po.aspx.cs" Inherits="Pur_Po" MaintainScrollPositionOnPostback="True" ValidateRequest="true"  enableEventValidation="false"%>
<%@ Register assembly="DevExpress.Web.v17.2, Version=17.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web" tagprefix="dx" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <link href="../../Content/css/bootstrap.min.css" rel="stylesheet" />
    <script src="../../Content/js/jquery.min.js"></script>
    <script src="../../Content/js/bootstrap.min.js"></script>
    <script src="../../Content/js/layer/layer.js"></script>
    <script src="../../Content/js/plugins/layer/laydate/laydate.js"></script>
    <link href="../../Content/js/plugins/bootstrap-select/css/bootstrap-select.min.css" rel="stylesheet" />
    <script src="../../Content/js/plugins/bootstrap-select/js/bootstrap-select.min.js"></script>

<%--       <div class="row row-container" >
            <div class="col-md-12">
                <div class="panel panel-info">
                    <div class="panel-heading" data-toggle="collapse" data-target="#SQXX">
                         <strong>申请人信息</strong>
                    </div>
                    <div class="panel-body <% =ViewState["lv"].ToString() == "SQXX" ? "" : "collapse" %>" id="SQXX">
                        <div class="col-xs-12 col-sm-12  col-md-12 col-lg-12" style="width:1000px;">
                            <div class="">
                                <asp:UpdatePanel ID="UpdatePanel_request" runat="server">
                                    <ContentTemplate>
                                        <table style="height: 30px; width: 100%">
                                            <tr>
                                                <td>申请人
                                                </td>
                                                <td>
                                                    <div class="form-inline">
                                                        <input id="txt_CreateById" class="form-control input-s-sm" style="height: 30px; width: 100px;font-size:12px;" runat="server" readonly="True"  />
                                                        <input id="txt_CreateByName" class="form-control input-s-sm" style="height: 30px; width: 100px;font-size:12px;" runat="server" readonly="True" />
                                                        <input id="txt_CreateByAd" class="form-control input-s-sm" style="height: 30px; width: 100px; font-size:12px;display: none;font-size:12px;" runat="server" readonly="True" />
                                                    </div>
                                                </td>
                                                <td>部门
                                                </td>
                                                <td>
                                                    <input id="txt_CreateByDept" class="form-control input-s-sm" style="height: 30px; width: 100px;font-size:12px;" runat="server" readonly="True" />
                                                </td>
                                               <td style="display: none">部门经理
                                                </td>
                                                <td>
                                                    <div class="form-inline" style="display: none">
                                                        <input id="txt_managerid" class="form-control input-s-sm" style="height: 30px; width: 100px;font-size:12px;" runat="server" readonly="True" />
                                                        <input id="txt_manager" class="form-control input-s-sm" style="height: 30px; width: 100px;font-size:12px;" runat="server" readonly="True" />
                                                        <input id="txt_manager_AD" class="form-control input-s-sm" style="height: 30px; width: 100px;font-size:12px;" runat="server" readonly="True" />
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                              <td>当前登陆人员</td>
                                                <td>
                                                    <div class="form-inline">
                                                        <input id="txt_LogUserId" class="form-control input-s-sm" style="height: 30px; width: 100px;font-size:12px;" runat="server" readonly="True" />
                                                        <input id="txt_LogUserName" class="form-control input-s-sm" style="height: 30px; width: 100px;font-size:12px;" runat="server" readonly="True" />/
                                                        <input id="txt_LogUserJob" class="form-control input-s-sm" style="height: 30px; width: 100px;font-size:12px;" runat="server" readonly="True" />
                                                        <input id="txt_LogUserDept" class="form-control input-s-sm" style="height: 30px; width: 100px;font-size:12px;" runat="server" readonly="True" />
                                                    </div>
                                                </td>
                                                 <td>申请日期：</td>
                                                <td>
                                                    <input id="txt_CreateDate" class="form-control input-s-sm" style="height: 30px; width: 100px;font-size:12px;" runat="server" readonly="True" />
                                                </td>
                                                <td></td>
                                                <td>
                                                    <input id="txt_Code" class="form-control input-s-sm" style="height: 30px; width: 200px; display: none; font-size:12px;" runat="server" readonly="True" />
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
        </div>--%>

    <script type="text/javascript">
      

        $(document).ready(function () {
            $("#mestitle").html("【PO采购审批单】<a href='/userguide/reviewGuide.pptx' target='_blank' class='h4' style='display:none'>使用说明</a>");
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

        })

        //提出自定流程 JS 
        function setComment(val) {
            $('#comment', parent.document).val(val);            
        }


          //设定表字段状态（可编辑性）
        var tabName="PUR_PO_Main_Form";//表名
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
        var tabName2="PUR_PO_Dtl_Form";//表名
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
       
        //验证
        function validate(id){
            <%=ValidScript%>
           
        }
         var fieldStatus = "1"=="<%=Request.QueryString["isreadonly"]%>"? {} : <%=fieldStatus%>;
	    var displayModel = '<%=DisplayModel%>';
        $(window).load(function (){
            SetControlStatus(<%=fieldStatus%>);
            SetControlStatus2(<%=fieldStatus%>);
	        
            //特殊控件处理
            if($("#MainContent_pgi_no").attr("readonly")=="readonly")
            {$("#MainContent_pgi_no").removeAttr("onclick")};
	    });
    </script>
    <script type="text/javascript">
        var popupwindow = null;
        function GetXMH() {
            popupwindow = window.open('../../Select/select_XMLJ.aspx', '_blank', 'height=500,width=800,resizable=no,menubar=no,scrollbars =no,location=no');
        }
        function GetPgi_Product() {
            popupwindow = window.open('../../Select/select_product.aspx?ctrl1=pgi_no', '_blank', 'height=500,width=800,resizable=no,menubar=no,scrollbars =no,location=no');
        }
        function setvalue_product(lspgi_no, lspn, lspn_desc, lsdomain, lsproduct_user, lsproduct_dept, lsstatus, lscailiao, lsnyl, lsline, lsver) {

            $("input[id*='pgi_no']").val(lspgi_no);
            $("input[id*='pn']").val(lspn);
            $("input[id*='pn_desc']").val(lspn_desc);
            $("input[id*='domain']").val(lsdomain);
            $("input[id*='product_user']").val(lsproduct_user);
            $("input[id*='dept']").val(lsproduct_dept);
            $("input[id*='status']").val(lsstatus);
            $("input[id*='sku']").val(lscailiao);
            $("input[id*='year_num']").val(lsnyl);
            $("input[id*='line']").val(lsline);
            $("input[id*='ver']").val(lsver);
            popupwindow.close();
            //$("input[id*='" + ctrl0 + "']").change();

            $.ajax({
                type: "post",
                url: "daoju.aspx/GetGx",
                data: "{'lspgi_no':'" + lspgi_no + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    if (data.d != "") //返回的数据用data.d获取内容Logaction + "成功."
                    {
                       
                        $('#MainContent_op').html("");
                        $('#MainContent_op').append("<option value=''> </option>");
                        $.each(eval(data.d), function (i) {
                           
                            $('#MainContent_op').append("<option value=" + eval(data.d)[i].value + ">" + eval(data.d)[i].value + "</option>");
                          
                        });
                    }
                    else {
                        alert("失败.");
                    }
                }

            });
        }


        function Getgx_name() {

           // alert('xxxx');
           // $("input[id*='MainContent_op_desc']").val("aaaaaa");
            //  $("input[id*='MainContent_op_desc']").val($("#MainContent_op").find("option:selected").val());

            $.ajax({
                type: "post",
                url: "daoju.aspx/GetGxms",
                data: "{'lspgi_no':'" + $("input[id*='pgi_no']").val() + "','lsop':'" + $('#MainContent_op').val() + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    if (data.d != "") //返回的数据用data.d获取内容Logaction + "成功."
                    {

                       // alert(data.d);
                        $("input[id*='MainContent_op_desc']").val(data.d);
                    }
                    else {
                        alert("失败.");
                    }
                }

            });
            
        }
        function Getdaoju(e)
        {
            
            var ss = e.id.split("_");// 在每个逗号(,)处进行分解。
            var url = "../../select/sz_report_dev_select.aspx?id=" + ss[6] + "&domain=" + $("input[id*='domain']").val()
           
            popupwindow = window.open(url, '_blank', 'height=500,width=1000,resizable=yes,menubar=no,scrollbars =yes,location=no');
        }
        function setvalue_dj(id, wlh, ms, lx, js, sm, pp, gys)
        {
            $("input[id*='MainContent_gv_cell" + id + "_3_daoju_no1_" + id + "_I']").val(wlh);
            $("input[id*='MainContent_gv_cell" + id + "_4_daoju_desc1_" + id + "_I']").val(ms);
            $("input[id*='MainContent_gv_cell" + id + "_5_daoju_type_" + id + "_I']").val(lx);
            $("input[id*='MainContent_gv_cell" + id + "_6_jiaoshu_" + id + "_I']").val(js);
            $("input[id*='MainContent_gv_cell" + id + "_8_edsm_" + id + "_I']").val(sm);
            $("input[id*='MainContent_gv_cell" + id + "_9_smtzxs_" + id + "_I']").val(1);
            $("input[id*='MainContent_gv_cell" + id + "_12_brand_" + id + "_I']").val(pp);
            $("input[id*='MainContent_gv_cell" + id + "_13_supplier_" + id + "_I']").val(gys);
            popupwindow.close();
        }

        function setvalue(ctrl0, keyValue0, ctrl1, keyValue1, ctrl2, keyValue2) {

            $("input[id*='MainContent_gv_cell0_3_daoju_no1_0_I']").val(keyValue0);
            //$("input[id*='" + ctrl1 + "']").val(keyValue1);
            //$("input[id*='" + ctrl2 + "']").val(keyValue2);
            popupwindow.close();
           // $("input[id*='" + ctrl0 + "']").change();
        }

        function openSelect()
        {
            
            //if ( $("input[id*='povendorid']").val()=="") {
            //    layer.alert("请先选择供应商！");
               
            //    return;
            //}
            
            var url = "../../select/select_pr.aspx?domain="+$("[id*='podomai']").val()+"";

            layer.open({
                type: 2,
                area: ['1000px', '500px'],
                fixed: false, //不固定
                maxmin: true,
                content: url
            });
          
          
           
            //popupwindow = window.open(url, '_blank', 'height=500,width=1000,resizable=yes,menubar=no,scrollbars =yes,location=no');
        }

        function potype(){
            if ($("[id*='potype']").val()=="存货") {
               
                $("[id*='fqfk_div']").hide();
            }else {
                
                $("[id*='fqfk_div']").show();
            }
        }

        function vendorid(s){
        
           // alert(s.GetValue());
           grid.PerformCallback(s.GetValue());
        }


        function test(){
        
            alert('xxxxx');
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

          .auto-style1 {
              position: relative;
              min-height: 1px;
              float: left;
              width: 100%;
              top: -5px;
              left: 0px;
              margin-top: 0px;
              padding-left: 1px;
              padding-right: 1px;
          }

    </style>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent" enctype="multipart/form-data">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
      <div class="col-md-12  ">
            <div class="col-md-10  ">
                <div class="form-inline " style="text-align:right">
                   <asp:Button ID="btnSave" runat="server" Text="保存" CssClass="btn btn-default btn-xs btnSave" OnClientClick="return validate();" OnClick="btnSave_Click" />
                    <asp:Button ID="btnflowSend" runat="server" Text="申请" CssClass="btn btn-default btn-xs btnflowSend"  OnClientClick="return validate();" OnClick="btnflowSend_Click" />
                    <input id="btnaddWrite" type="button" value="加签" onclick="parent.addWrite(true);" class="btn btn-default btn-xs btnaddWrite" />
                    <input id="btnflowBack" type="button" value="退回" onclick="parent.flowBack(true);" class="btn btn-default btn-xs btnflowBack" />
                    <input id="btnflowCompleted" type="button" value="完成" onclick="parent.flowCompleted(true);" class="btn btn-default btn-xs btnflowCompleted" />
                    <input id="btnshowProcess" type="button" value="查看流程" onclick="parent.showProcess(true);" class="btn btn-default btn-xs btnshowProcess" />
                </div>
            </div>
        </div>
    <div class="col-md-12">
 <%--       <div class="row row-container" >
            <div class="col-md-12">
                <div class="panel panel-info">
                    <div class="panel-heading" data-toggle="collapse" data-target="#SQXX">
                         <strong>申请人信息</strong>
                    </div>
                    <div class="panel-body <% =ViewState["lv"].ToString() == "SQXX" ? "" : "collapse" %>" id="SQXX">
                        <div class="col-xs-12 col-sm-12  col-md-12 col-lg-12" style="width:1000px;">
                            <div class="">
                                <asp:UpdatePanel ID="UpdatePanel_request" runat="server">
                                    <ContentTemplate>
                                        <table style="height: 30px; width: 100%">
                                            <tr>
                                                <td>申请人
                                                </td>
                                                <td>
                                                    <div class="form-inline">
                                                        <input id="txt_CreateById" class="form-control input-s-sm" style="height: 30px; width: 100px;font-size:12px;" runat="server" readonly="True"  />
                                                        <input id="txt_CreateByName" class="form-control input-s-sm" style="height: 30px; width: 100px;font-size:12px;" runat="server" readonly="True" />
                                                        <input id="txt_CreateByAd" class="form-control input-s-sm" style="height: 30px; width: 100px; font-size:12px;display: none;font-size:12px;" runat="server" readonly="True" />
                                                    </div>
                                                </td>
                                                <td>部门
                                                </td>
                                                <td>
                                                    <input id="txt_CreateByDept" class="form-control input-s-sm" style="height: 30px; width: 100px;font-size:12px;" runat="server" readonly="True" />
                                                </td>
                                               <td style="display: none">部门经理
                                                </td>
                                                <td>
                                                    <div class="form-inline" style="display: none">
                                                        <input id="txt_managerid" class="form-control input-s-sm" style="height: 30px; width: 100px;font-size:12px;" runat="server" readonly="True" />
                                                        <input id="txt_manager" class="form-control input-s-sm" style="height: 30px; width: 100px;font-size:12px;" runat="server" readonly="True" />
                                                        <input id="txt_manager_AD" class="form-control input-s-sm" style="height: 30px; width: 100px;font-size:12px;" runat="server" readonly="True" />
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                              <td>当前登陆人员</td>
                                                <td>
                                                    <div class="form-inline">
                                                        <input id="txt_LogUserId" class="form-control input-s-sm" style="height: 30px; width: 100px;font-size:12px;" runat="server" readonly="True" />
                                                        <input id="txt_LogUserName" class="form-control input-s-sm" style="height: 30px; width: 100px;font-size:12px;" runat="server" readonly="True" />/
                                                        <input id="txt_LogUserJob" class="form-control input-s-sm" style="height: 30px; width: 100px;font-size:12px;" runat="server" readonly="True" />
                                                        <input id="txt_LogUserDept" class="form-control input-s-sm" style="height: 30px; width: 100px;font-size:12px;" runat="server" readonly="True" />
                                                    </div>
                                                </td>
                                                 <td>申请日期：</td>
                                                <td>
                                                    <input id="txt_CreateDate" class="form-control input-s-sm" style="height: 30px; width: 100px;font-size:12px;" runat="server" readonly="True" />
                                                </td>
                                                <td></td>
                                                <td>
                                                    <input id="txt_Code" class="form-control input-s-sm" style="height: 30px; width: 200px; display: none; font-size:12px;" runat="server" readonly="True" />
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
        </div>--%>
        <div class="row  row-container">
            <div class="col-md-12" >
                <div class="panel panel-info">
                    <div class="panel-heading" data-toggle="collapse" data-target="#CPXX">
                         <strong>审批记录基本信息</strong>
                    </div>
                    <div class="panel-body " id="XSGCS">
                        <div class="col-xs-12 col-sm-12  col-md-12 col-lg-12" style="width:1000px;">
                            <div>
                                <asp:Table Style="width: 100%" border="0" runat="server" ID="tblWLLeibie" Font-Size="12px">
                                </asp:Table>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

       
                                <asp:UpdatePanel runat="server" ID="p1" UpdateMode="Conditional">
                                    <ContentTemplate>
       <div class="row  row-container" >
            <div class="col-md-12" >
                <div class="panel panel-info">
                    <div class="panel-heading" data-toggle="collapse" data-target="#gscs">
                        <strong style="padding-right: 100px">采购清单</strong>
                    </div>
                    <div class="panel-body  collapse in" id="gscs">
                        <div class="col-xs-12 col-sm-12  col-md-12 col-lg-12">
                            <div> 
                                <div style="padding: 2px 5px 5px 0px">
                
                                     <input runat="server" id="btnadd" type="button" value="新增" class="btn btn-default" style="width:60px; height:32px;"  onclick="openSelect()"/>
                                     <asp:Button ID="btndel" runat="server" Text="删除" class="btn btn-default" style="width:60px; height:32px;" OnClick="btndel_Click"  />
                                </div>
                               
                              
                                <dx:aspxgridview ID="gv" runat="server" AutoGenerateColumns="False" Width="525px" KeyFieldName="rowid" OnRowCommand="gv_RowCommand" Theme="MetropolisBlue" OnCustomCallback="gv_CustomCallback" 
                                     ClientInstanceName="grid"  EnableTheming="True" onhtmlrowcreated="gv_HtmlRowCreated" >
                                   
            <SettingsPager PageSize="1000">
            </SettingsPager>
                                    <Settings ShowFooter="True" />
            <SettingsBehavior AllowSelectByRowClick="True" AllowDragDrop="False" AllowSort="False" />
            <Columns>
            </Columns>
                                    <TotalSummary>
                                        
                                         <dx:aspxsummaryitem DisplayFormat="合计:{0:N4}" FieldName="targetTotalPrice" ShowInColumn="targetTotalPrice" ShowInGroupFooterColumn="targetTotalPrice" SummaryType="Sum" />
                                        <dx:aspxsummaryitem DisplayFormat="合计:{0:N4}" FieldName="TotalPrice" ShowInColumn="TotalPrice" ShowInGroupFooterColumn="TotalPrice" SummaryType="Sum" />
                                         
                                    </TotalSummary>
            <Styles>
                <Header BackColor="#1E82CD" ForeColor="White" >
                </Header>
              
            </Styles>
                     <Columns>
           <%-- <dx:gridviewcommandcolumn ShowSelectCheckbox="True" ShowClearFilterButton="true" SelectAllCheckboxMode="Page" Width="50" />--%>
                         </Columns>   
                                          
        </dx:aspxgridview>
                                
                                    
                                 <table style="width:100%;"><tr style="display: block; margin: 10px 0; text-align:right;" >
                                    <td style="display: block; margin: 10px 0; text-align:right;">
                                        
                                        <asp:UpdatePanel runat="server" ID="UpdatePanel1" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        
                                        </ContentTemplate></asp:UpdatePanel>
                                        </td>
                                       </tr></table>
                            </div>
                           
                        </div>
                    </div>
                </div>
            </div>
        </div>

                               </ContentTemplate>
                                    
                                </asp:UpdatePanel>
                                

   <div id="fqfk_div" style="display:none;">
         <div class="row  row-container">
            <div class="col-md-12" >
                <div class="panel panel-info">
                    <div class="panel-heading" data-toggle="collapse" data-target="#FKXX">
                         <strong>付款信息strong>
                    </div>
                    <div class="panel-body collapse in" id="FKXX">
                        
                        <div class="col-xs-12 col-sm-12  col-md-12 col-lg-12" style="width:1000px;">
                            <div>
                                <asp:Table  border="0" runat="server" ID="tablePay" Font-Size="12px">
                                </asp:Table>
                                
                            </div>
                           
                                 
                                <asp:UpdatePanel runat="server" ID="UpdatePanel2" UpdateMode="Conditional">
                                    <ContentTemplate>
                                <dx:aspxgridview ID="gv2" runat="server" AutoGenerateColumns="False" Width="525px" KeyFieldName="id"  Theme="MetropolisBlue" TabIndex="1000" >
            <SettingsPager PageSize="1000">
            </SettingsPager>
            <SettingsBehavior AllowSelectByRowClick="True" AllowDragDrop="False" AllowSort="False" />
            <Columns>
            </Columns>
            <Styles>
                <Header BackColor="#1E82CD" ForeColor="White" >
                </Header>
                <SelectedRow BackColor="Red">
                </SelectedRow>
            </Styles>
        </dx:aspxgridview>
                                       </ContentTemplate>
                                    
                                </asp:UpdatePanel>

                            
                            </div>
                        </div>
                    </div>
                    </div>
                </div>
             </div>

         <div class="row  row-container">
            <div class="auto-style1" >
                <div class="panel panel-info">
                    <div class="panel-heading" data-toggle="collapse" data-target="#FJSC">
                         <strong>供应商报价单，报价分析，技术协议，合同</strong>
                    </div>
                    <div class="panel-body collapse in" id="FJSC">
                        <div class="col-xs-12 col-sm-12  col-md-12 col-lg-12" style="width:1000px;">
                            <div>
                              <%-- <table style="width:100px;">
                                   <tr>
                                       <td> <asp:FileUpload ID="FileUpload1" runat="server" />
                                       </td>
                                       <td><asp:HyperLink ID="txtfile" runat="server" Visible="false" Target="_blank">文件浏览</asp:HyperLink>
                                           
                                       </td>
                                   </tr>
                               </table>--%>
                                 <dx:ASPxUploadControl ID="uploadcontrol" runat="server" Width="500px" UploadMode="Auto" Visible="false" >
                                     <AdvancedModeSettings EnableDragAndDrop="True" EnableFileList="True" EnableMultiSelect="True">
                                     </AdvancedModeSettings>
                                </dx:ASPxUploadControl>
                                <asp:Table ID="tab1" runat="server">
                                    <asp:TableRow ID="tab1_row" runat="server">
                                        <asp:TableCell ID="tab1_col" runat="server"></asp:TableCell>
                                    </asp:TableRow>
                                </asp:Table>
                            </div>
                           
                            </div>
                        <br />
                        </div>
                    </div>
                </div>
            
             </div>


    </div>
    <asp:Button ID="Button2" runat="server" Text="test" class="btn btn-large btn-primary" Width="100px" OnClick="Button2_Click"  Visible="false" />
    <asp:Button ID="Button1" runat="server" Text="提交" class="btn btn-large btn-primary" Width="100px" OnClick="Button1_Click" Visible="true" />
       
       <div class="row  row-container" style="display: ">
            <div class="col-md-12">
                <div class="panel panel-info">
                    <div class="panel-heading" data-toggle="collapse" data-target="#CZRZ">
                        
                    </div>
                    <div class="panel-body ">
                        <table border="0"  width="100%" class="bg-info" >
                        <tr><td width="100px" ><label>处理意见：</label></td>
                        <td> <input id="comment" type="text" placeholder="请在此处输入处理意见" class="form-control" onchange="setComment(this.value)" /></td>
                       </tr>

                    </table>
                     <table>
                         <tr>
                             <td>
                                  <%--<dx:aspxgridview ID="gv3" runat="server" AutoGenerateColumns="False" Width="525px" KeyFieldName="id"  Theme="MetropolisBlue" TabIndex="1000" >
            <SettingsPager PageSize="1000">
            </SettingsPager>
            <SettingsBehavior AllowSelectByRowClick="True" AllowDragDrop="False" AllowSort="False" />
            <Columns>
            </Columns>
            <Styles>
                <Header BackColor="#1E82CD" ForeColor="White" >
                </Header>
                <SelectedRow BackColor="Red">
                </SelectedRow>
            </Styles>
        </dx:aspxgridview>--%>
                             </td>
                         </tr>
                     </table>
                    </div>
                </div>
            </div>
        </div>
</asp:Content>
