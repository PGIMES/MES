﻿<%@ Page Title="MES生产管理系统" Language="C#"  AutoEventWireup="true"
    CodeFile="ToolKnife.aspx.cs" Inherits="ToolKnife" MaintainScrollPositionOnPostback="True" ValidateRequest="true" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
     <script src="../../../Content/js/jquery.min.js"></script>
    <script src="../../../Content/js/bootstrap.min.js"></script>    
    <script src="../../Content/js/layer/layer.js"></script>
    <link href="../../Content/css/bootstrap.min.css" rel="stylesheet" />        
    <link href="../../../Content/js/plugins/bootstrap-select/css/bootstrap-select.min.css" rel="stylesheet" />
    <script src="../../../Content/js/plugins/bootstrap-select/js/bootstrap-select.min.js"></script>

    <script type="text/javascript">
        $(document).ready(function () {
            $("#mestitle").html("【刀具申请】<a href='/userguide/reviewGuide.pptx' target='_blank' class='h4' style='display:none'>使用说明</a>");
             

            //提出自定流程 JS 
            if ($('#3B271F67-0433-4082-AD1A-8DF1B967B879', parent.document).length == 0) {
                //alert("保存")
               // $("#btnSave").hide();
            }
            if ($('#8982B97C-ADBA-4A3A-AFD9-9A3EF6FF12D8', parent.document).length == 0) {
                //alert("发送")
                $("#btnflowSend").hide();
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
                $("#btnshowProcess").hide();
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
            
            //工厂多选
            $("input[id*='ddldomain']").click(function(){
                $("#domaindomain").val("");
                var val="";
                $("input[id*='ddldomain']").each(function(i,item){
                    if($(item).prop("checked")==true)
                    {
                        val=val+$(item).val()+";";
                    }
                })
                if(val.length>0){
                    val=val.substring(0,val.length-1);
                }
                $("#domain").val(val);
            })



        })// end ready
        //提出自定流程 JS 
        function setComment(val) {
            $('#comment', parent.document).val(val);            
        }
        //设定表字段状态（可编辑性）
        var tabName="PGI_BASE_PART_DATA_FORM";//表名
        function SetControlStatus(fieldStatus)
        {  // tabName_columnName:1_0
            var flag=true;
            for(var item in fieldStatus){
                var id=item.replace(tabName.toLowerCase()+"_","");
                
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
        //验证
        function validate(id){
            <%=ValidScript%>
            //var p2=$("#type").val();
            //$.ajax({
            //    type: "Post",
            //    url: "ToolKnife.aspx/GetWLH" , 
            //    //方法传参的写法一定要对，str为形参的名字,str2为第二个形参的名字
            //    //P1: 申请类别 如刀具类 P2：申请类型 如钻头，拉刀等
            //    data: "{'P1':'01','P2':'"+p2+"'}",
            //    contentType: "application/json; charset=utf-8",
            //    dataType: "json",
            //    success: function (data) {//返回的数据用data.d获取内容//                        
            //         alert(data.d)
            //        //$.each(eval(data.d), function (i, item) {  })                              
            //            if (data.d == "") {
            //                layer.alert("获取物料号失败.");                            
            //            }
            //            else {
            //                $("#wlh").val(data.d);
            //            }                   
            //    },
            //    error: function (err) {
            //        layer.alert(err);
            //    }
            //});

        }
    </script>
    <script type="text/javascript">
	   <%-- var initData = <%=BWorkFlow.GetFormDataJsonString(initData)%>;--%>
	    var fieldStatus = "1"=="<%=Request.QueryString["isreadonly"]%>"? {} : <%=fieldStatus%>;
	    var displayModel = '<%=DisplayModel%>';
        $(window).load(function (){
            SetControlStatus(<%=fieldStatus%>);
	        //formrun.initData(initData, "rf_PGI_BASE_PART_DATA", fieldStatus, displayModel);
	    });

    </script>
    <script type="text/javascript">
        var popupwindow = null;
        function GetXMH() {
            popupwindow = window.open('../Select/select_XMLJ.aspx', '_blank', 'height=500,width=800,resizable=no,menubar=no,scrollbars =no,location=no');
        }
        function GetEmp() {
            popupwindow = window.open('../Select/select_Emp.aspx?ctrl1=txtProbEmp', '_blank', 'height=500,width=800,resizable=no,menubar=no,scrollbars =no,location=no');
        }
        function setvalue(ctrl0, keyValue0, ctrl1, keyValue1, ctrl2, keyValue2) {

            $("input[id*='" + ctrl0 + "']").val(keyValue0);
            $("input[id*='" + ctrl1 + "']").val(keyValue1);
            $("input[id*='" + ctrl2 + "']").val(keyValue2);
            popupwindow.close();
            $("input[id*='" + ctrl0 + "']").change();
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
            margin-bottom: 3px;
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
            padding-bottom: 2px;
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
    </head>
<body>
    
    <form id="form1" runat="server"  >
        
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <div class="h4" style="margin-left: 10px" id="headTitle">
            >>MES生产管理系统
            <span id="mestitle" ></span> 
                       
            <div style="float: right; margin-right: 10px; font-size: 10px">
                <label id="logUser"><%=Session["UserAD"].ToString() %></label>
                <label id="logUser">(<%=Session["UserId"].ToString() %>)</label>                
            </div>
           
        </div>
        <div class="col-md-12  ">
            <div class="col-md-10  ">
                <div class="form-inline " style="text-align:right">
                    <asp:Button ID="btnSave" runat="server" Text="保存" CssClass="btn btn-default btn-xs btnSave" OnClientClick="return validate();" OnClick="btnSave_Click" />
                    <asp:Button ID="btnflowSend" runat="server" Text="发送" CssClass="btn btn-default btn-xs btnflowSend"  OnClientClick="return validate();" OnClick="btnflowSend_Click" />
                    <input id="btnaddWrite" type="button" value="加签" onclick="parent.addWrite(true);" class="btn btn-default btn-xs btnaddWrite" />
                    <input id="btnflowBack" type="button" value="退回" onclick="parent.flowBack(true);" class="btn btn-default btn-xs btnflowBack" />
                    <input id="btnflowCompleted" type="button" value="完成" onclick="parent.flowCompleted(true);" class="btn btn-default btn-xs btnflowCompleted" />
                    <input id="btnshowProcess" type="button" value="查看流程" onclick="parent.showProcess(true);" class="btn btn-default btn-xs btnshowProcess" />
                </div>
            </div>
        </div>
         
    <div class="col-md-10">
        <div class="row row-container">
            <div class="col-md-12">
                <div class="panel panel-info">
                    <div class="panel-heading" data-toggle="collapse" data-target="#SQXX">
                        <strong>申请人信息</strong>
                    </div>
                    <div class="panel-body collapse out"  id="SQXX">
                        <div class="col-xs-12 col-sm-12  col-md-12 col-lg-12">
                            <div class="">
                                <asp:UpdatePanel ID="UpdatePanel_request" runat="server">
                                    <ContentTemplate>
                                        <table style="height: 35px; width: 100%">
                                            <tr>
                                                <td>申请人：
                                                </td>
                                                <td>
                                                    <div class="form-inline">
                                                        <asp:TextBox runat="server"  id="CreateById" cssclass="form-control input-s-sm" style="height: 35px; width: 70px"   readonly="True" ></asp:TextBox>
                                                        <asp:TextBox runat="server" id="CreateByName" cssclass="form-control input-s-sm" style="height: 35px; width: 70px"   readonly="True" ></asp:TextBox>
                                                        <input runat="server" id="txt_CreateByAd" class="form-control input-s-sm" style="height: 35px; width: 70px; display: none"   readonly="True" />
                                                    </div>
                                                </td>
                                                <td>部门：
                                                </td>
                                                <td>
                                                    <input id="txt_CreateByDept" class="form-control input-s-sm" style="height: 35px; width: 100px" runat="server" readonly="True" />
                                                </td>
                                                <td >部门经理：
                                                </td>
                                                <td>
                                                    <div class="form-inline" style="display:none ">
                                                        <input id="txt_managerid" class="form-control input-s-sm" style="height: 35px; width: 70px" runat="server" readonly="True" />
                                                        <input id="txt_manager" class="form-control input-s-sm" style="height: 35px; width: 70px" runat="server" readonly="True" />
                                                        <input id="txt_manager_AD" class="form-control input-s-sm" style="height: 35px; width: 70px" runat="server" readonly="True" />
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>当前登陆人员</td>
                                                <td>
                                                    <div class="form-inline">
                                                        <input id="txt_LogUserId" class="form-control input-s-sm" style="height: 35px; width: 70px" runat="server" readonly="True" />
                                                        <input id="txt_LogUserName" class="form-control input-s-sm" style="height: 35px; width: 70px" runat="server" readonly="True" />
                                                        <input id="txt_LogUserJob" class="form-control input-s-sm" style="height: 35px; width: 70px" runat="server" readonly="True" />
                                                        <input id="txt_LogUserDept" class="form-control input-s-sm" style="height: 35px; width: 70px" runat="server" readonly="True" />
                                                    </div>
                                                </td>
                                                <td>申请日期：</td>
                                                <td>
                                                    <asp:TextBox   id="CreateDate" cssclass="form-control input-s-sm" style="height: 35px; width: 100px" runat="server" readonly="True" />
                                                </td>
                                                <td></td>
                                                <td>
                                                    <input id="txt_Code" class="form-control input-s-sm" style="height: 35px; width: 200px; display: none" runat="server" readonly="True" />
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
                    <div class="panel-heading" data-toggle="collapse" data-target="#CPXX">
                        <strong>物料类别</strong>
                    </div>
                    <div class="panel-body " id="XSGCS">
                        <div class="col-xs-12 col-sm-12  col-md-12 col-lg-12">
                            <div>
                                <table style="width: 100%" border="0" runat="server" id="tblWLLeibie">
                                    <tr>
                                        <td>申请工厂</td>
                                        <td><asp:CheckBoxList ID="ddldomain" runat="server" CssClass="form-control" RepeatDirection="Horizontal" ></asp:CheckBoxList>
                                            <asp:textbox ID="domain" runat="server"  CssClass=" hidden"  ToolTip="0|1"   Width="40" /></td>
                                        <td>申请类别</td>
                                        <td><asp:DropDownList ID="class" runat="server" CssClass="form-control" ToolTip="0|1" AutoPostBack="True" OnSelectedIndexChanged="class_SelectedIndexChanged"></asp:DropDownList></td>
                                        <td>申请类型</td>
                                        <td>
                                            <asp:DropDownList ID="type" runat="server"  CssClass="form-control"  ToolTip="0|1" AutoPostBack="True" OnSelectedIndexChanged="ddltype_SelectedIndexChanged"></asp:DropDownList>
                                            <asp:textbox ID="purchaser" runat="server"  CssClass=" hidden"  ToolTip="0|1"   Width="40" />
                                        </td>
                                        <td>分类说明</td>
                                        <td>
                                            <asp:TextBox ID="typedesc" runat="server"  CssClass="form-control"  ></asp:TextBox></td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="row  row-container">
            <div class="col-md-12">
                <div class="panel panel-info">
                    <div class="panel-heading" data-toggle="collapse" data-target="#gscs">
                        <strong style="padding-right: 100px">物料属性</strong>
                    </div>
                    <div class="panel-body  collapse in" id="gscs">
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
            <div class="col-md-12">
                <div class="panel panel-info">
                    <div class="panel-heading" data-toggle="collapse" data-target="#gsjg">
                        <strong style="padding-right: 100px">物料数据/计划数据</strong>
                    </div>
                    <div class="panel-body   collapse in" id="gsjg">
                        <div class="col-xs-12 col-sm-12  col-md-12 col-lg-12">

                            <div>
                                <asp:Table Style="width: 100%" border="0" runat="server" ID="tblWLShuJu">
                                </asp:Table>
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
                        <table border="0"  width="100%" class="bg-info" >
                        <tr><td width="100px" ><label>处理意见：</label></td>
                        <td> <input id="comment" type="text" placeholder="请在此处输入处理意见" class="form-control" onchange="setComment(this.value)" /></td>
                       </tr>

                    </table>
                    </div>
                </div>
            </div>
        </div>
         
        
    </div>

    <div class="col-md-2  hidden">
        <div class="panel panel-info">
            <div class="panel-heading">
                <strong>记录:</strong>
            </div>
            <div class="panel-body  " id="DDXX">
                <div>
                    <div class="">
                        <asp:GridView ID="gv_rz2" Width="100%"
                            AllowMultiColumnSorting="True" AllowPaging="True"
                            AllowSorting="True" AutoGenerateColumns="False"
                            runat="server" Font-Size="Small">
                            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <PagerSettings FirstPageText="首页" LastPageText="尾页"
                                Mode="NextPreviousFirstLast" NextPageText="下页" PreviousPageText="上页" />
                            <PagerStyle ForeColor="Red" />
                            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                            <EditRowStyle BackColor="#999999" />
                            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />

                            <Columns>
                                <asp:BoundField DataField="Update_username" HeaderText="姓名">
                                    <HeaderStyle Wrap="False" />
                                    <ItemStyle Width="10%" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Update_content" HeaderText="操作事项">
                                    <HeaderStyle Wrap="False" />
                                    <ItemStyle Width="10%" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Commit_time" HeaderText="操作时间" DataFormatString="{0:yy/MM/dd}">
                                    <HeaderStyle Wrap="False" />
                                    <ItemStyle Width="30%" />
                                </asp:BoundField>
                            </Columns>
                        </asp:GridView>


                        <br />
                    </div>
                </div>
            </div>
        </div>
    </div>
        
        
    </form>
</body>
</html>

