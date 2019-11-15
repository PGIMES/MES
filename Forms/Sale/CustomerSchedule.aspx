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
        });

        //提出自定流程 JS 
        function setComment(val) {
            $('#comment', parent.document).val(val);
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

        function validate(action){
            var flag=true;var msg="";
            
            <%=ValidScript%>

            if($("#DQXX input[id*='ApplyId']").val()=="" || $("#DQXX input[id*='ApplyName']").val()==""){
                msg+="【申请人】不可为空.<br />";
            }
            if($("#DQXX input[id*='ApplyDeptId']").val()=="" || $("#DQXX input[id*='ApplyDeptName']").val()==""){
                msg+="【申请人部门】不可为空.<br />";
            }

            if(action=='submit'){
                
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
                /*var applyid=$("#DQXX input[id*='ApplyId']").val();

                var formno=$("#CPXX input[id*='FormNo']").val();
                var part=("#ljXX input[id*='part']").val();
                var domain=$("#ljXX input[id*='domain']").val();
                var site=$("#ljXX input[id*='site']").val();
                var ship=$("#ljXX input[id*='ship']").val();
                var typeno=$("#ljXX input[id*='typeno']").val();

                $.ajax({
                    type: "post",
                    url: "CustomerSchedule.aspx/CheckData",
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

                });*/
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



    </div>

</asp:Content>

