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

            init_pay();
        });

        function init_pay(){
            if (getType_Contain()) {
                $("[id*='fqfk_div']").show();
                if ($("[id*='paytype']").val()!="" && $("[id*='paytype']").val().indexOf("一次性")<0){
                    $("[id*='btnadd_contract']").show();$("[id*='btndel_contract']").show();
                }else{
                    $("[id*='btnadd_contract']").hide();$("[id*='btndel_contract']").hide();
                }                
            }
            else {                        
                $("[id*='fqfk_div']").hide();
                $("[id*='btnadd_contract']").hide();$("[id*='btndel_contract']").hide();
            }    
        }

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
        
        function openSelect()
        {
            //if ( $("input[id*='povendorid']").val()=="") {
            //    layer.alert("请先选择供应商！");               
            //    return;
            //}
            
            var url = "../../select/select_pr.aspx?domain="+$("[id*='podomai']").val()+"&buyername="+$("input[type!=hidden][id*='buyername']").val()+"&potype="+$("input[type!=hidden][id*='potype']").val();

            layer.open({
                type: 2,
                area: ['1000px', '500px'],
                fixed: false, //不固定
                maxmin: true,
                content: url
            });
          
        }

        function vendorid(s){
           // alert(s.GetValue());
           grid.PerformCallback(s.GetValue());
        }

        function buyername_potype_change(type,s){ 
            if (type=="buyer") {
                grid.PerformCallback($("input[type!=hidden][id*='buyername']").val());
            }
            if (type=="potype") {
                grid.PerformCallback($("input[type!=hidden][id*='buyername']").val());                
                potype();
            }
        }

        function potype(){
            if (getType_Contain()) {   
                $("[id*='fqfk_div']").show();
                $("[id*='paytype']").val("");    
                paytype();
            }else {            
               
                $("[id*='fqfk_div']").hide();
                $("[id*='btnadd_contract']").hide();$("[id*='btndel_contract']").hide();
            }
        }

        function paytype(){
            if ($("[id*='paytype']").val()!="" && $("[id*='paytype']").val().indexOf("一次性")<0){
                $("[id*='btnadd_contract']").show();$("[id*='btndel_contract']").show();
            }else{
                $("[id*='btnadd_contract']").hide();$("[id*='btndel_contract']").hide();
            }

            grid2.PerformCallback("change");
        }

        //获取 是否是 合同模块
        function getType_Contain(){   
            var str="";
            $.ajax({
                type: "Post",async: false,
                url: "Pur_Po.aspx/getType_Contain" , 
                data: "{'potype':'"+$("input[type!=hidden][id*='potype']").val()+"'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {                   
                    str=data.d;            
                },
                error: function (err) {
                    layer.alert(err);
                }
            });
            if (str=="合同模块") {
                return true;
            }else {
                return false;
            }
        };

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
    <script type="text/javascript">
        function validate(action){
            var flag=true;var msg="";
            <%=ValidScript%>
            
            //if(action=='submit'){
                if($("input[type!=hidden][id*='buyername']").val()==""){
                    msg+="【采购负责人】不可为空.<br />";
                }

                if($("input[type!=hidden][id*='povendorid']").val()==""){
                    msg+="【采购供应商】不可为空.<br />";
                }
            //}
           
            if($("[id$=gv] tr[id*=DataRow]").length==0){
                msg+="【采购清单】不可为空.<br />";
            }

            $("[id$=gv] tr[id*=DataRow]").each(function (index, item) { 
                //alert($(item).find("input[type!=hidden][id*=PlanReceiveDate]").val());
                if($(item).find("input[id*=PlanReceiveDate]").val()==""){
                    msg+="第"+(index+1)+"行【计划到货日期】不可为空.<br />";
                }

            });

            if ($("input[type!=hidden][id*='potype']").val()=="合同"){
                if ($("[id*='paytype']").val()==""){                   
                    msg+="【付款类型】不可为空.<br />";
                }
                if($("[id$=gv2] tr[id*=DataRow]").length==0){
                    msg+="【付款信息】不可为空.<br />";
                }else{
                    $("[id$=gv2] tr[id*=DataRow]").each(function (index, item) { 
                        if($(item).find("input[id*=PayRate]").val()==""){
                            msg+="第"+(index+1)+"行【付款比列】不可为空.<br />";
                        }
                        if($(item).find("input[id*=PlanPayDate]").val()==""){
                            msg+="第"+(index+1)+"行【计划付款日期】不可为空.<br />";
                        }
                    });
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

           return flag; 
        }
    </script>
    <style>body{overflow-x:auto; overflow-y:hidden}
        hidden { display:none
        }
    </style>

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

    <style>
        .lineread{
            /*font-size:9px;*/ border:none; border-bottom:1px solid #ccc;
        }
        .linewrite{
            /*font-size:9px;*/ border:none; border-bottom:1px solid #ccc;background-color:#FDF7D9;/*EFEFEF*/
        }
        .dxeButtonDisabled{
            display:none;
        }
        .dxgvDataRow_MetropolisBlue>td:nth-child(3){
            word-break:break-all;
        }
        .dxgvDataRow_MetropolisBlue>td:nth-child(6){/*为了给 物料名称[描述] 强行换行*/
            word-break:break-all;
        }
		.dxgvDataRow_MetropolisBlue>td:nth-child(11){
            word-break:break-all;
        }
    </style>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent" enctype="multipart/form-data">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div class="col-md-12  ">
        <div class="col-md-10  ">
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
    <div class="col-md-12">

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
                                <strong>采购清单</strong>
                               <%-- &nbsp;&nbsp;<font style="color:red; font-size:9px;">
                                            提示：行背景橙色：采购单价(未税) 大于 历史单价(未税)
                                &nbsp;&nbsp;采购总价(未税) 单元格 红色：多出 目标总价(未税) 的20%，黄色：在 目标总价(未税) 的20% 范围内
                                &nbsp;&nbsp;推荐供应商 黄色：与采购供应商不一致
                                            </font>--%>
                            </div>
                            <div class="panel-body  collapse in" id="gscs">
                                <div class="col-xs-12 col-sm-12  col-md-12 col-lg-12">
                                    <div> 
                                        <div style="padding: 2px 5px 5px 0px">                
                                            <input runat="server" id="btnadd" type="button" value="新增" class="btn btn-primary btn-sm" onclick="openSelect()"/>
                                            <asp:Button ID="btndel" runat="server" Text="删除" class="btn btn-primary btn-sm" OnClick="btndel_Click"  />
                                        </div>
                                        <dx:aspxgridview ID="gv" runat="server" AutoGenerateColumns="False"  KeyFieldName="rowid" OnRowCommand="gv_RowCommand" Theme="MetropolisBlue" 
                                            OnCustomCallback="gv_CustomCallback" ClientInstanceName="grid"  EnableTheming="True" onhtmlrowcreated="gv_HtmlRowCreated" Border-BorderColor="#DCDCDC"
                                            OnDataBound="gv_DataBound" >
                                            <ClientSideEvents EndCallback="function(s, e) {  grid2.PerformCallback('load');}" />
                                            <SettingsPager PageSize="1000"></SettingsPager>
                                            <Settings ShowFooter="True" />
                                            <SettingsBehavior AllowSelectByRowClick="True" AllowDragDrop="False" AllowSort="False" />
                                            <Columns>
                                            </Columns>
                                            <TotalSummary>
                                                <dx:aspxsummaryitem DisplayFormat="<font color='red'>合计:{0:N0}</font>" FieldName="PRNo" ShowInColumn="PRNo" ShowInGroupFooterColumn="PRNo" SummaryType="Sum" />
                                                <dx:aspxsummaryitem DisplayFormat="<font color='red'>{0:N2}</font>" FieldName="notax_targetTotalPrice" ShowInColumn="notax_targetTotalPrice" ShowInGroupFooterColumn="notax_targetTotalPrice" SummaryType="Sum" />
                                                <dx:aspxsummaryitem DisplayFormat="<font color='red'>{0:N2}</font>" FieldName="TotalPrice" ShowInColumn="TotalPrice" ShowInGroupFooterColumn="TotalPrice" SummaryType="Sum" />
                                                <dx:aspxsummaryitem DisplayFormat="<font color='red'>{0:N2}</font>" FieldName="notax_TotalPrice" ShowInColumn="notax_TotalPrice" ShowInGroupFooterColumn="notax_TotalPrice" SummaryType="Sum" />
                                            </TotalSummary>
                                            <Styles>
                                                <Header BackColor="#E4EFFA" Border-BorderColor="#DCDCDC" HorizontalAlign="Left" VerticalAlign="Top"></Header>   
                                                <SelectedRow BackColor="#FDF7D9"></SelectedRow>   
                                                <AlternatingRow BackColor="#f2f3f2"></AlternatingRow>
                                                <Cell Border-BorderColor="#DCDCDC" BorderLeft-BorderWidth="0"  BorderRight-BorderWidth="0" BorderTop-BorderWidth="0"></Cell>
                                                <CommandColumn Border-BorderColor="#DCDCDC" BorderRight-BorderStyle="None" HorizontalAlign="Left"></CommandColumn>
                                            </Styles>
                                        </dx:aspxgridview>
                                        <table style="width:100%;">
                                            <tr style="display: block; margin: 10px 0; text-align:right;" >
                                                <td style="display: block; margin: 10px 0; text-align:right;">
                                                    <asp:UpdatePanel runat="server" ID="UpdatePanel1" UpdateMode="Conditional">
                                                        <ContentTemplate></ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>                                

                <div id="fqfk_div" hidden="hidden">
                    <div class="row  row-container">
                        <div class="col-md-12" >
                            <div class="panel panel-info">
                                <div class="panel-heading" data-toggle="collapse" data-target="#FKXX">
                                     <strong>付款信息<strong>
                                </div>
                                <div class="panel-body collapse in" id="FKXX">
                                    <div class="col-xs-12 col-sm-12  col-md-12 col-lg-12" style="width:1000px;">
                                        <div>
                                            <asp:Table border="0" runat="server" ID="tablePay" Font-Size="12px">
                                            </asp:Table>                                
                                        </div>
                                        <div style="padding: 2px 5px 5px 0px">                
                                            <asp:Button ID="btnadd_contract" runat="server" Text="新增" class="btn btn-primary btn-sm" OnClick="btnadd_contract_Click" />
                                            <asp:Button ID="btndel_contract" runat="server" Text="删除" class="btn btn-primary btn-sm" OnClick="btndel_contract_Click" />
                                        </div>
                                        <dx:aspxgridview ID="gv2" runat="server" AutoGenerateColumns="False"  KeyFieldName="rowid"  Theme="MetropolisBlue" TabIndex="1000"  
                                            ClientInstanceName="grid2"  EnableTheming="True" Border-BorderColor="#DCDCDC" OnCustomCallback="gv2_CustomCallback">
                                            <SettingsPager PageSize="1000" />
                                            <Settings ShowFooter="True" />
                                            <SettingsBehavior AllowSelectByRowClick="True" AllowDragDrop="False" AllowSort="False" />
                                            <Columns>
                                            </Columns>
                                            <TotalSummary>
                                                <dx:aspxsummaryitem DisplayFormat="<font color='red'>小计</font>" FieldName="rowid" ShowInColumn="rowid" ShowInGroupFooterColumn="rowid" SummaryType="Sum" />
                                                <dx:aspxsummaryitem DisplayFormat="<font color='red'>{0:N0}</font>" FieldName="PayRate" ShowInColumn="PayRate" ShowInGroupFooterColumn="PayRate" SummaryType="Sum" />
                                                <dx:aspxsummaryitem DisplayFormat="<font color='red'>{0:N2}</font>" FieldName="PayMoney" ShowInColumn="PayMoney" ShowInGroupFooterColumn="PayMoney" SummaryType="Sum" />
                                            </TotalSummary>
                                            <Styles>
                                                <Header BackColor="#E4EFFA" Border-BorderColor="#DCDCDC" HorizontalAlign="Left" VerticalAlign="Top"></Header>   
                                                <SelectedRow BackColor="#FDF7D9"></SelectedRow>   
                                                <AlternatingRow BackColor="#f2f3f2"></AlternatingRow>
                                                <Cell Border-BorderColor="#DCDCDC" BorderLeft-BorderWidth="0"  BorderRight-BorderWidth="0" BorderTop-BorderWidth="0"></Cell>
                                                <CommandColumn Border-BorderColor="#DCDCDC" BorderRight-BorderStyle="None"></CommandColumn>
                                            </Styles>
                                        </dx:aspxgridview>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>                                    
        </asp:UpdatePanel> 
              
         <div class="row  row-container">
            <div class="auto-style1" >
                <div class="panel panel-info">
                    <div class="panel-heading" data-toggle="collapse" data-target="#FJSC">
                         <strong>供应商报价单，报价分析，技术协议，合同</strong>
                    </div>
                    <div class="panel-body collapse in" id="FJSC">
                        <div class="col-xs-12 col-sm-12  col-md-12 col-lg-12" style="width:1000px;">
                            <div>
                               <%--<table style="width:100px;">
                                   <tr>
                                       <td> <asp:FileUpload ID="FileUpload1" runat="server" />
                                       </td>
                                       <td><asp:HyperLink ID="txtfile" runat="server" Visible="false" Target="_blank">文件浏览</asp:HyperLink>
                                           
                                       </td>
                                   </tr>
                               </table>--%>
                                 <dx:aspxuploadcontrol ID="uploadcontrol" runat="server" Width="500px" BrowseButton-Text="浏览"  Visible="true" ClientInstanceName="UploadControl" 
                                     ShowAddRemoveButtons="True" RemoveButton-Text="删除" UploadMode="Advanced"   AutoStartUpload="true" ShowUploadButton="false" ShowProgressPanel="true"
                                     onfileuploadcomplete="uploadcontrol_FileUploadComplete" >
                                     <AdvancedModeSettings EnableDragAndDrop="True" EnableFileList="True" EnableMultiSelect="True">
                                     </AdvancedModeSettings>
                                     <ClientSideEvents FileUploadComplete="onFileUploadComplete" /> <%--FilesUploadStart="function(s, e) { DXUploadedFilesContainer.Clear(); }" --%>
                                </dx:aspxuploadcontrol>
                                <input type="hidden" id="ip_filelist" name="ip_filelist" runat="server" />                              
                                <table id="tbl_filelist"  Width="500px">  
                                </table>
                                <%--<dx:UploadedFilesContainer ID="FileContainer" runat="server" Width="380" Height="180" NameColumnWidth="240" SizeColumnWidth="70" HeaderText="Uploaded files" />--%>

                                <asp:UpdatePanel runat="server" ID="p11" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <textarea id="ip_filelist_db" name="ip_filelist" runat="server" cols="200" rows="2" visible="false"></textarea>
                                        <asp:Table ID="tab1" Width="500px" runat="server">
                                            <%--<asp:TableRow ID="tab1_row" runat="server">
                                                <asp:TableCell ID="tab1_col" runat="server"></asp:TableCell>
                                            </asp:TableRow>--%>
                                        </asp:Table>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                        <br />
                    </div>
                </div>
            </div>
        </div>

    </div>
       
    <div class="row  row-container" style="display: ">
        <div class="col-md-12">
            <div class="panel panel-info">
                <div class="panel-heading" data-toggle="collapse" data-target="#CZRZ"></div>
                <div class="panel-body ">
                    <table border="0"  width="100%" class="bg-info" >
                        <tr>
                            <td width="100px" ><label>处理意见：</label></td>
                            <td> <%--<input id="comment" type="text" placeholder="请在此处输入处理意见" class="form-control" onchange="setComment(this.value)" />--%>
                                <textarea id="comment" cols="20" rows="2" placeholder="请在此处输入处理意见" class="form-control" onchange="setComment(this.value)" ></textarea>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
