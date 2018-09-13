<%@ Page Title="私车公用申请单" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="FIN_CA.aspx.cs" Inherits="Forms_Finance_FIN_CA" 
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
            $("#mestitle").html("【私车公用申请单】<a href='/userguide/CAGuide.pps' target='_blank' class='h5' style='color:red'>使用说明</a>");

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

            SetGirdDateDrop_Read();

        });

        function SetGirdDateDrop_Read(){
            $("[id$=gv_d] tr[class*=DataRow]").each(function (index, item) { 
                var StartDateTime = eval('StartDateTime' + index);
                var EndDateTime = eval('EndDateTime' + index);
                if (!StartDateTime.GetEnabled()) {
                    $(item).find("td[id*=StartDateTime]").css("display","none");
                }
                if (!EndDateTime.GetEnabled()) {
                    $(item).find("td[id*=EndDateTime]").css("display","none");
                }
            });
        }

        //提出自定流程 JS 
        function setComment(val) {
            $('#comment', parent.document).val(val);
        }

        //设定表字段状态（可编辑性）
        var tabName="pgi_fin_ca_main_form";//表名
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
        var tabName2="pgi_fin_ca_dtl_form";//表名
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

        #SQXX label{
            font-weight:400;padding-bottom:3px;
        }
        .lineread{
            font-size:12px; border:none; border-bottom:1px solid #ccc; height: 23px;
        }
        .linewrite{
            font-size:12px; border:none; border-bottom:1px solid #ccc;background-color:#FDF7D9;height: 23px;
        }
        .i_hidden{
            display:none;
        }
        .i_show{
            display:inline-block;
        }
    </style>

    <script type="text/javascript">
        function con_sure(){
            if (gv_d.GetSelectedRowCount() <= 0) { layer.alert("请选择要删除的记录!"); return false; }
            //询问框
            return confirm('确认要删除吗？');
        }

        function Get_ApplyId(){
            var url = "/select/select_ApplyId.aspx?para=car";

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

            $("#SQXX input[id*='ApplyId']").val(workcode);
            $("#SQXX input[id*='ApplyName']").val(lastname);
            $("#SQXX input[id*='ApplyTelephone']").val(telephone);
            $("#SQXX input[id*='ApplyJobTitleName']").val(jobtitlename);
            $("#SQXX input[id*='ApplyDomain']").val(domain);
            $("#SQXX input[id*='ApplyDomainName']").val(gc);
            $("#SQXX input[id*='ApplyDeptId']").val(ITEMVALUE);
            $("#SQXX input[id*='ApplyDeptName']").val(dept_name);
            $("#SQXX input[id*='CarNo']").val(car);
            
        }

        function RefreshRow() {
            var BTC=0;
            $("[id$=gv_d] tr[class*=DataRow]").each(function (index, item) { 
                var Mileage = eval('Mileage' + index);
                if (Mileage.GetText()!="") {
                    BTC=BTC+Number(Mileage.GetText());
                }
            });
            //grid底部total值更新
            $("[id$=gv_d] tr[id*=DXFooterRow]").find('td').each(function () {
                if($.trim($(this).text())!=""){
                    $(this).html("<b>合计:"+BTC.toFixed(2)+"</b>");//$(this).text("<b>合计:"+BTC.toFixed(2)+"</b>");
                }   
            });
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

            if($("#SQXX input[id*='CarNo']").val()==""){
                msg+="【申请人车牌号】不可为空.<br />";
            }           

            if($("[id$=gv_d] input[id*=StartDateTime]").length==0){
                msg+="【申请明细】不可为空.<br />";
            }else {
                if (!ASPxClientEdit.ValidateGroup("ValueValidationGroup")) {
                    msg+="【申请明细】格式必须正确.<br />";
                }else {
                    if(action=='submit'){

                        $("[id$=gv_d] tr[class*=DataRow]").each(function (index, item) { 
                            var StartDateTime = eval('StartDateTime' + index);
                            var EndDateTime = eval('EndDateTime' + index);
                            var TravelRoute = eval('TravelRoute' + index);
                            var Mileage = eval('Mileage' + index);

                            if (StartDateTime.GetText()=="0100/01/01 00:00") { msg+="【开始时间】不可为空.<br />"; }
                            if (EndDateTime.GetText()=="0100/01/01 00:00") { msg+="【结束时间】不可为空.<br />"; }
                            if (TravelRoute.GetText()=="") { msg+="【行程路线】不可为空.<br />"; }
                            if (Mileage.GetText()=="") { msg+="【用车公里数】不可为空.<br />"; }

                            if (msg!="") {
                                return false;
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
            
            //----------------------------------------------------------------------------逻辑验证
            if(action=='submit'){

                $("[id$=gv_d] tr[class*=DataRow]").each(function (index, item) { 
                    var start_date_time= $(item).find("input[id*=StartDateTime]").val();
                    var end_date_time= $(item).find("input[id*=EndDateTime]").val();
                    if(compareDate(start_date_time,end_date_time)){
                        msg+="【结束时间】必须大于【开始时间】.<br />";
                    }
                });
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

            if(flag){

                var ApplyId=$("#SQXX input[id*='ApplyId']").val();

                $.ajax({
                    type: "post",
                    url: "FIN_T.aspx/CheckData",
                    data: "{'appuserid':'" + ApplyId + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    async: false,//默认是true，异步；false为同步，此方法执行完在执行下面代码
                    success: function (data) {
                        var obj=eval(data.d);

                        if(obj[0].manager_flag!=""){ msg+=obj[0].manager_flag; }

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

        function compareDate(s1,s2){
            return ((new Date(s1.replace(/-/g,"\/")))>=(new Date(s2.replace(/-/g,"\/"))));
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
                <div class="panel-heading" data-toggle="collapse" data-target="#DQXX">
                    <strong>填单人信息</strong>
                </div>
                <div class="panel-body collapse" id="DQXX">
                    <div class="col-xs-12 col-sm-12  col-md-12 col-lg-12" style="width:1000px;">
                        <table style="width: 100%">
                            <tr>
                                <td><font color="red">&nbsp;</font>填单人</td>
                                <td>
                                    <div class="form-inline">
                                        <asp:TextBox runat="server" ID="CreateId" class="lineread" ReadOnly="True" Width="50px"></asp:TextBox>
                                        <asp:TextBox runat="server" ID="CreateName"  class="lineread" ReadOnly="True" Width="82px"></asp:TextBox>
                                        <asp:TextBox runat="server" ID="CreateTelephone" class="lineread" Width="120px"/>
                                    </div>
                                </td>
                                <td><font color="red">&nbsp;</font>填单人职位</td>
                                <td><asp:TextBox runat="server" ID="CreateJobTitleName" class="lineread" ReadOnly="True" Width="260px"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td><font color="red">&nbsp;</font>填单人公司</td>
                                <td>
                                    <div class="form-inline">
                                        <asp:TextBox runat="server" ID="CreateDomain" class="lineread" ReadOnly="True" Width="60px"></asp:TextBox>
                                        <asp:TextBox runat="server" ID="CreateDomainName"  class="lineread" ReadOnly="True" Width="196px"></asp:TextBox>
                                    </div>
                                </td>
                                <td><font color="red">&nbsp;</font>填单人部门</td>
                                <td>
                                    <div class="form-inline">
                                        <asp:TextBox runat="server" ID="CreateDeptId" class="lineread" ReadOnly="True" Width="60px"></asp:TextBox>
                                        <asp:TextBox runat="server" ID="CreateDeptName"  class="lineread" ReadOnly="True" Width="196px"></asp:TextBox>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
        </div>
          
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
                                <td><asp:TextBox ID="FormNo" runat="server" class="lineread"  ReadOnly="true" placeholder="自动产生" Width="260px" ToolTip="1|0" /></td>
                                <td><font color="red">&nbsp;</font>申请日期</td>
                                <td><asp:TextBox ID="ApplyDate"  runat="server" class="lineread" ReadOnly="True" Width="260px" /></td>
                            </tr>
                            <tr>
                                <td><font color="red">*</font>申请人</td>
                                <td>
                                    <div class="form-inline">
                                        <asp:TextBox runat="server" ID="ApplyId" class="lineread" ReadOnly="True" Width="50px"></asp:TextBox>
                                        <asp:TextBox runat="server" ID="ApplyName"  class="lineread" ReadOnly="True" Width="82px"></asp:TextBox>
                                        <asp:TextBox runat="server" ID="ApplyTelephone" class="lineread" Width="120px"/>
                                        <i id="ApplyId_i" class="fa fa-search <% =ViewState["ApplyId_i"].ToString() == "Y" ? "i_hidden" : "i_show" %>" onclick="Get_ApplyId()"></i>
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
                            <tr>
                                <td><font color="red">*</font>申请人车牌号</td>
                                <td><asp:TextBox ID="CarNo" runat="server" class="lineread"  ReadOnly="true" Width="260px" /></td>
                                <td></td>
                                <td></td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
        </div>

        <div id="div_dtl" class="row  row-container">
            <div class="panel panel-info">
                <div class="panel-heading" data-toggle="collapse" data-target="#fin_ca_dtl">
                    <strong>申请明细</strong>
                </div>
                <div class="panel-body " id="fin_ca_dtl">
                    <div class="col-xs-12 col-sm-12  col-md-12 col-lg-12">
                        <div style="padding: 2px 5px 5px 0px">                
                            
                        </div>
                        <asp:UpdatePanel runat="server" ID="p1" UpdateMode="Conditional" >
                            <ContentTemplate>
                                
                                <asp:Button ID="btnadd" runat="server" Text="新增" class="btn btn-default" style="width:60px; height:32px;"  OnClick="btnadd_Click" />
                                <asp:Button ID="btndel" runat="server" Text="删除" class="btn btn-default" style="width:60px; height:32px;"  OnClick="btndel_Click" OnClientClick="return con_sure()" />

                                 <dx:aspxgridview ID="gv_d" runat="server" AutoGenerateColumns="False" KeyFieldName="numid" Theme="MetropolisBlue" 
                                     ClientInstanceName="gv_d"  EnableTheming="True">
                                    <SettingsPager PageSize="1000"></SettingsPager>
                                    <Settings ShowFooter="True"  />
                                    <SettingsBehavior AllowSelectByRowClick="false" AllowDragDrop="False" AllowSort="False" />
                                    <Columns>
                                        <dx:GridViewCommandColumn SelectAllCheckboxMode="Page" ShowClearFilterButton="true" ShowSelectCheckbox="true" Name="Sel" Width="30" VisibleIndex="0"></dx:GridViewCommandColumn>
                                        <dx:GridViewDataTextColumn  Caption="#" FieldName="numid" Width="30px" VisibleIndex="1"></dx:GridViewDataTextColumn> 
                                        <dx:GridViewDataTextColumn Caption="开始时间" FieldName="StartDateTime" Width="130px" VisibleIndex="2">
                                            <Settings AllowCellMerge="False"/>
                                            <DataItemTemplate>
                                                <dx:ASPxDateEdit ID="StartDateTime" runat="server" EditFormat="Custom" Width="130"  UseMaskBehavior="true" EditFormatString="yyyy/MM/dd HH:mm"
                                                    ClientInstanceName='<%# "StartDateTime"+Container.VisibleIndex.ToString() %>'
                                                    DisabledStyle-Border-BorderStyle="None" DisabledStyle-ForeColor="Black">
                                                    <TimeSectionProperties Visible="true">
                                                        <TimeEditProperties EditFormatString="HH:mm" />
                                                    </TimeSectionProperties>
                                                    <CalendarProperties><FastNavProperties DisplayMode="Inline" /></CalendarProperties>
                                                </dx:ASPxDateEdit>
                                            </DataItemTemplate>        
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="结束时间" FieldName="EndDateTime" Width="130px" VisibleIndex="4">
                                            <Settings AllowCellMerge="False"/>
                                            <DataItemTemplate>
                                                <dx:ASPxDateEdit ID="EndDateTime" runat="server" EditFormat="Custom" Width="130"  UseMaskBehavior="true" EditFormatString="yyyy/MM/dd HH:mm"
                                                    ClientInstanceName='<%# "EndDateTime"+Container.VisibleIndex.ToString() %>'
                                                    DisabledStyle-Border-BorderStyle="None" DisabledStyle-ForeColor="Black" >
                                                    <TimeSectionProperties Visible="true">
                                                        <TimeEditProperties EditFormatString="HH:mm" />
                                                    </TimeSectionProperties>
                                                    <CalendarProperties><FastNavProperties DisplayMode="Inline" /></CalendarProperties>
                                                </dx:ASPxDateEdit>
                                            </DataItemTemplate>        
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="行程路线" FieldName="TravelRoute" Width="190px" VisibleIndex="6">
                                            <Settings AllowCellMerge="False"/>
                                            <DataItemTemplate>
                                                <dx:ASPxTextBox ID="TravelRoute" Width="180px" runat="server" Value='<%# Eval("TravelRoute")%>' 
                                                    ClientInstanceName='<%# "TravelRoute"+Container.VisibleIndex.ToString() %>'>
                                                </dx:ASPxTextBox>
                                            </DataItemTemplate>        
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="用车公里数" FieldName="Mileage" Width="100px" VisibleIndex="7">
                                            <Settings AllowCellMerge="False"/>
                                            <DataItemTemplate>
                                                <dx:ASPxTextBox ID="Mileage" Width="90px" runat="server" Value='<%# Eval("Mileage")%>' 
                                                    ClientSideEvents-ValueChanged='<%# "function(s,e){RefreshRow();}" %>'
                                                    ClientInstanceName='<%# "Mileage"+Container.VisibleIndex.ToString() %>'  HorizontalAlign="Right">
                                                    <ValidationSettings ValidationGroup="ValueValidationGroup" Display="Dynamic" ErrorTextPosition="Bottom">
                                                        <RegularExpression ErrorText="请输入正数！" ValidationExpression="^[+]{0,1}(\d+)$|^[+]{0,1}(\d+\.\d+)$" />
                                                    </ValidationSettings>
                                                </dx:ASPxTextBox>
                                            </DataItemTemplate>        
                                        </dx:GridViewDataTextColumn> 
                                        <dx:GridViewDataTextColumn Caption="备注" FieldName="Remark" Width="230px" VisibleIndex="8">
                                            <Settings AllowCellMerge="False"/>
                                            <DataItemTemplate>
                                                <dx:ASPxTextBox ID="Remark" Width="220px" runat="server" Value='<%# Eval("Remark")%>' 
                                                    ClientInstanceName='<%# "Remark"+Container.VisibleIndex.ToString() %>'>
                                                </dx:ASPxTextBox>
                                            </DataItemTemplate>        
                                        </dx:GridViewDataTextColumn>

                                        <dx:GridViewDataTextColumn FieldName="id" Width="0px">
                                            <HeaderStyle CssClass="hidden" />
                                            <CellStyle CssClass="hidden"></CellStyle>
                                            <FooterCellStyle CssClass="hidden"></FooterCellStyle>
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="FIN_CA_No" Width="0px">
                                            <HeaderStyle CssClass="hidden" />
                                            <CellStyle CssClass="hidden"></CellStyle>
                                            <FooterCellStyle CssClass="hidden"></FooterCellStyle>
                                        </dx:GridViewDataTextColumn>
                                    </Columns>       
                                    <TotalSummary>
                                        <dx:ASPxSummaryItem DisplayFormat="<b>合计:{0:N2}</b>" FieldName="Mileage" SummaryType="Sum" />
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

