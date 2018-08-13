<%@ Page Title="BOM申请单" Language="C#" AutoEventWireup="true" CodeFile="BOM.aspx.cs" Inherits="BOM" MaintainScrollPositionOnPostback="True" ValidateRequest="true" %>

<%@ Register Assembly="DevExpress.Web.v17.2, Version=17.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.ASPxTreeList.v17.2, Version=17.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxTreeList" TagPrefix="dx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script src="../../../Content/js/jquery.min.js"></script>
    <script src="../../../Content/js/bootstrap.min.js"></script>
    <script src="../../Content/js/layer/layer.js"></script>
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
        select{border-left:none ;border-right:none;border-top:none;border-bottom:1px solid gray  ;height:25px}
        .name{width:80px}
        .bordernone{border:none; }
        .alpha100{ background:rgba(0, 0, 0, 0); }
        .width100 { width:100px  }.width50 { width:50px  }
    </style>
    <script src="../../../Content/js/plugins/bootstrap-select/js/bootstrap-select.min.js"></script>
    <script src="../../Scripts/RFlow.js"></script>
    <script type="text/javascript">
        var paramMap;
        $(document).ready(function () {
            $("#mestitle").html("【BOM申请单】<a href='/userguide/reviewGuide.pptx' target='_blank' class='h4' style='display:none'>使用说明</a>");// 
             
            SetButtons();

            ////工厂选择更改
            //$("select[id*='domain']").change(function(){
            //    bindSelect("applydept");
            //    $("#deptm").val("");
            //})
            ////申请部门
            //$("select[id*='applydept']").change(function(){
            //    var domain=$("#domain").val();
            //    var dept=$("#applydept").val();
            //    getDeptLeader(domain,dept);              
            //})
               $('table[id*=treeList_D] th:last a').find("span").remove();         
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

            $("a span[text=更新]").click(function(){
                validUpdate()
            })

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
                    if( statu.indexOf("1_")!="-1" && (ctype=="text"||ctype=="textarea") ){
                        $("#"+id).attr("readonly","readonly");
                    }
                    else if( statu.indexOf("1_")!="-1" && ( ctype=="checkbox"||ctype=="radio"||ctype=="select"||ctype=="file" ) ){
                        $("#"+id).attr("disabled","disabled");                 
                    }

                    if( statu.indexOf("1_")!="-1" ){
                        //设定treelist 编辑性
                        $('table[id*=treeList] th:last').hide();
                        $('table[id*=treeList] tbody tr').each(function(index,item)
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
                    if( statu.indexOf("1_")!="-1" && (ctype=="text"||ctype=="textarea") ){
                        $(obj).attr("readonly","readonly");
                        $(obj).removeAttr("onclick");$(obj).removeAttr("ondblclick");
                      //  $(obj).css("border","none");
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

    </script>




</head>
<body>
    <script type="text/javascript">
	   <%-- var initData = <%=BWorkFlow.GetFormDataJsonString(initData)%>;--%>
        var fieldStatus = "1"=="<%=Request.QueryString["isreadonly"]%>"? {} : <%=fieldStatus%>;
        var fieldSet=<%=fieldStatus%>;
        var displayModel = '<%=DisplayModel%>';
        $(window).load(function (){
            SetControlStatus(<%=fieldStatus%>,"eng_bom_main_form");
            SetControlStatus2(<%=fieldStatus%>,"eng_bom_dtl_form");  
            SetControlStatus2(<%=fieldStatus%>,"eng_bom_verrec_form");                
        });

        //验证
        function validate(id){
             
            if($("#deptm").val()==""){
                layer.alert("直属主管未获取到，请尝试重新打开申请单。请联系IT设定.");
                return false;
            }
            <%=ValidScript%>
            var flag=true;
            var msg="";
             
            //validate wlh
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
        
       //选择物料号
        function openwind(){  
            var ctrl0="pt_part";
            var ctrl1="pt_desc1";
            var ctrl2="pt_desc2";
           // window.open( '/forms/open/select.aspx?windowid=bom&ctrl0='+ctrl0+'&ctrl1='+ctrl1+'&ctrl2='+ctrl2);
            layer.open({
                shade: [0.5, '#000', false],
                type: 2,
                offset: '100px',
                area: ['600px', '450px'],
                fix: false, //不固定
                maxmin: false,
                title: ['<i class="fa fa-dedent"></i> 请选择物料', false],
                closeBtn: 1,
                content: '/forms/open/select.aspx?windowid=bom&ctrl0='+ctrl0+'&ctrl1='+ctrl1+'&ctrl2='+ctrl2,
                end: function () {

                }
            });
        }
        //选择项目号
        function selectpgino(){  
            var ctrl0="pgino";
            var ctrl1="txt_ptdesc1";
            var ctrl2="txt_ptdesc2";
            var ctrl3="domain";
            // window.open( '/forms/open/select.aspx?windowid=bom&ctrl0='+ctrl0+'&ctrl1='+ctrl1+'&ctrl2='+ctrl2);
            layer.open({
                shade: [0.5, '#000', false],
                type: 2,
                offset: '100px',
                area: ['600px', '450px'],
                fix: false, //不固定
                maxmin: false,
                title: ['<i class="fa fa-dedent"></i> 请选择物料', false],
                closeBtn: 1,
                content: '/forms/open/select.aspx?windowid=pgino&ctrl0='+ctrl0+'&ctrl1='+ctrl1+'&ctrl2='+ctrl2+'&ctrl3='+ctrl3,
                end: function () {
                    if($("#pgino").val()!=""){
                        $("#btnFuZhu").click();
                    }

                }
            });
        }
    </script>

    <form id="form1" runat="server" enctype="multipart/form-data">

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
                                                   // // 部门主管
                                                   // var domain=$("#domain").val();
                                                   // var dept=$("#applydept").val();
                                                   //// getDeptLeader(domain,dept);                                                   
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
                                                        <asp:TextBox ID="CreateDate"  runat="server" class="input input-readonly" Style=" width: 200px" ReadOnly="True"  />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="background-color:">申请人：</td>
                                                    <td>
                                                        <div class="form-inline">
                                                            <asp:TextBox runat="server" ID="CreateById" class="input input-readonly"  Style=" width: 70px" ReadOnly="True"></asp:TextBox>
                                                            <asp:TextBox runat="server" ID="CreateByName"  class="input input-readonly" Style=" width: 70px" ReadOnly="True"></asp:TextBox>
                                                            <asp:TextBox runat="server" ID="DeptName" class="input input-readonly"  Style=" width: 100px" ReadOnly="True" />
                                                        </div>
                                                    </td>
                                                    <td style="display: ">电话（分机）：
                                                    </td>
                                                    <td style="display: ">
                                                        <asp:TextBox ID="phone" runat="server" class="input input-readonly" Style=" width: 200px"  />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>当前登陆人：</td>
                                                    <td>
                                                        <div class="form-inline">
                                                            <input id="txt_LogUserId" class="input input-readonly" style=" width: 70px" runat="server" readonly="True" />
                                                            <input id="txt_LogUserName" class="input input-readonly"  style=" width: 70px" runat="server" readonly="True" />
                                                            <input id="txt_LogUserDept"  class="input input-readonly" style=" width: 100px" runat="server" readonly="True" />
                                                        </div>
                                                    </td>
                                                    <td hidden>申请公司</td>
                                                    <td hidden>  
                                                        <asp:TextBox ID="deptm" Style="width: 20px; display: none" runat="server" />
                                                        <asp:TextBox ID="deptmfg" Style="width: 20px; display: none" runat="server" />
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
                            <strong>项目版本</strong>
                        </div>
                        <div class="panel-body " id="PA">
                            <div class="col-xs-12 col-sm-12  col-md-12 col-lg-12">
                                <div>
                                    <table style="" border="0" runat="server" id="tblWLLeibie">
                                        <tr>
                                            <td>项目号：</td>
                                            <td>
                                                 
                                                <input type="text" id="pgino" runat="server"   onclick="selectpgino()"  class="input input-edit width100" />
                                                客户零件号:<input  type="text" id="txt_ptdesc1" readonly="true" runat="server" class="input input-edit "   />
                                                零件名称:<input  type="text" id="txt_ptdesc2" readonly="true" runat="server" class="input input-edit "   />
                                            </td>
                                            <td hidden>工程版本：</td><td hidden>  </td>                              
                                            <td>生产工厂：</td>
                                            <td >                                                
                                                <asp:TextBox  ID="domain"   runat="server" Width="120px" ToolTip="0|1"   class="input input-edit"  ></asp:TextBox>
                                            </td>
                                            <td> BOM版本：</td>
                                            <td>
                                                <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <script type="text/jscript">
                                                            var prm = Sys.WebForms.PageRequestManager.getInstance();
                                                            prm.add_endRequest(function () {
                                              
                                                              //  SetControlStatus2(<%=fieldStatus%>,"eng_bom_dtl_form");
                                                            });
                                                        </script>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                                <asp:TextBox ID="bomver" runat="server" readonly="true" class="input input-edit" Width="120px" ToolTip="0|0" /><asp:Button runat="server" ID="btnFuZhu" OnClick="btnFuZhu_Click" CssClass="hidden" />


                                            </td>
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
                        <div class="panel-heading" data-toggle="collapse" data-target="#PB">
                            <strong style="padding-right: 100px">BOM<span id="warning" style="color: red; display: none"> </span></strong>
                        </div>
                        <div class="panel-body  collapse in" id="PB">
                            <div class="col-xs-12 col-sm-12  col-md-12 col-lg-12">
                                <div>
                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <script type="text/jscript">
                                                var prm = Sys.WebForms.PageRequestManager.getInstance();
                                                prm.add_endRequest(function () {
                                               
                                                  <%--//  SetControlStatus2(<%=fieldStatus%>,"eng_bom_dtl_form");--%>
                                                });
                                            </script>

                                            <div class="options" style="display:none">
                                                <div class="options-item">
                                                    <dx:ASPxComboBox ID="cmbMode" runat="server" AutoPostBack="true" OnSelectedIndexChanged="cmbMode_SelectedIndexChanged"
                                                        ValueType="System.Int32" Caption="Mode" />
                                                </div>
                                                <div class="options-item">
                                                    <dx:ASPxCheckBox ID="chkDragging" runat="server" AutoPostBack="true" Checked="true" Text="Allow node dragging" Wrap="false" />
                                                </div>
                                            </div>
                                            <dx:ASPxTreeList ID="treeList" runat="server" AutoGenerateColumns="False" Width="100%" 
                                                KeyFieldName="id" ParentFieldName="PID" OnProcessDragNode="treeList_ProcessDragNode" OnNodeInserting="treeList_NodeInserting"
                                                OnNodeUpdating="treeList_NodeUpdating" OnNodeDeleting="treeList_NodeDeleting" ViewStateMode="Enabled" SettingsBehavior-AllowDragDrop="true"
                                                 OnInitNewNode="treeList_InitNewNode" OnNodeValidating="treeList_NodeValidating"  OnHtmlRowPrepared="treeList_HtmlRowPrepared"
                                                
                                                 OnCommandColumnButtonInitialize="treeList_CommandColumnButtonInitialize"  OnHtmlDataCellPrepared="treeList_HtmlDataCellPrepared" >
                                                  <ClientSideEvents   EndCallback="function(s, e) {
	                                                    $('table[id*=treeList_D] th:last a').find('span').remove();                                                      
                                                    }
                                                    "    Init=""/>
                                                <Settings GridLines="Both"  />
                                                <SettingsBehavior ExpandCollapseAction="NodeDblClick" AllowFocusedNode="True" AllowDragDrop="true"   AutoExpandAllNodes="true"/>
                                                <SettingsCustomizationWindow PopupHorizontalAlign="RightSides" PopupVerticalAlign="BottomSides" />
                                                <SettingsEditing Mode="EditFormAndDisplayNode" AllowNodeDragDrop="True" />
                                                <SettingsPopupEditForm Width="500" />
                                                <SettingsText CommandEdit="编辑" RecursiveDeleteError="该节点有子节点，不能删除" CommandNew="添加x" ToolbarNew="PPP"
                                                    ConfirmDelete="确定要删除吗?" CommandUpdate="更新" CommandDelete="删除" CommandCancel="取消" />
                                                
                                                <SettingsPopup>
                                                    <EditForm VerticalOffset="-1" Width="500px">
                                                    </EditForm>
                                                </SettingsPopup>
                                                
                                                <Templates>                                                    
                                                    <EditForm>
                                                        <table class="treeListCard" >
                                                            <tr>                                                                
                                                                <td class="name">物料号:</td>
                                                                <td>
                                                                    <input type="text" id="pt_partold" runat="server" value='<%# Eval("pt_part") %>'  class="hidden"  />
                                                                     <input type="text" id="id" runat="server"  value='<%# Eval("id") %>' class="hidden"/>
                                                                     <input type="text" id="pid" runat="server" value='<%# Eval("pid") %>' class="hidden" />
                                                                    <input type="text" id="pt_part" runat="server" value='<%# Eval("pt_part") %>' readonly='<%# Eval("pt_part")==pgino.Value?"true":"false" %>' onclick='<%# Eval("pt_part")==null ||Eval("pt_part").ToString()!=pgino.Value?"openwind()":"" %>'  class='<%# Eval("pt_part")==null || Eval("pt_part").ToString()!=pgino.Value?"width100":"bordernone alpha100" %>' />
                                                                </td>
                                                                <td class="name">描述(零件号):</td>
                                                                <td>
                                                                    <input type="text" id="pt_desc1" runat="server" value='<%# Eval("pt_desc1") %>'  class="bordernone alpha100 " readonly="true"/>
                                                                </td>
                                                                <td class="name">描述2(零件名称):</td>
                                                                <td >
                                                                    <input type="text" id="pt_desc2"  runat="server" value='<%# Eval("pt_desc2") %>' class="bordernone alpha100 " readonly="true" />
                                                                </td>
                                                                <td class="name">图纸号:</td>
                                                                <td >
                                                                    <input type="text" id="drawno" runat="server" value='<%# Eval("drawno") %>' class="width100" />
                                                                </td>                                                                
                                                                <td class="name">单位用量(Kg):</td>
                                                                <td >
                                                                    <input type="text" id="ps_qty_per"  runat="server" value='<%# Eval("ps_qty_per") %>'  class="width50" />
                                                                    <input type="text" id="pt_net_wt"  runat="server" value='<%# Eval("pt_net_wt") %>'  class="width100 hidden" />
                                                                </td> 
                                                                <td class="name">单位:</td>
                                                                <td >
                                                                    <dx:ASPxComboBox ID="unit" runat="server" Width="50px"  class="width50" >
                                                                    <Items>
                                                                            <dx:ListEditItem Text="KG" Value="KG" />
                                                                            <dx:ListEditItem Text="EA" Value="EA"  />
                                                                            <dx:ListEditItem Text="" Value=""/>                                                                            
                                                                        </Items>
                                                                     </dx:ASPxComboBox>
                                                                    <%--<input type="text" id="unit" runat="server" value='<%# Eval("unit")  %>' class="width50" />--%>
                                                                    <%--<dx:ASPxComboBox ID="unit" runat="server"  Width="50px"  class="width50" SelectedIndex='<%# Eval("unit").ToString()=="KG"?0:1  %>'>
                                                                        <Items>
                                                                            <dx:ListEditItem Text="KG" Value="KG" />
                                                                            <dx:ListEditItem Text="EA" Value="EA"  />
                                                                            <dx:ListEditItem Text="" Value=""/>                                                                            
                                                                        </Items>
                                                                    </dx:ASPxComboBox>--%>
                                                                    
                                                                </td>
                                                            </tr>
                                                            <tr>                                                                
                                                                
                                                                <td class="name">材料:</td>
                                                                <td >
                                                                    <input type="text" id="material" runat="server" value='<%# Eval("material") %>'  class="width100" />
                                                                    <dx:ASPxComboBox ID="material2" runat="server"  Visible="false" >
                                                                        <Items>
                                                                            <dx:ListEditItem Text="A380" Value="A380"/>
                                                                            <dx:ListEditItem Text="A380" Value="A380"/>
                                                                            <dx:ListEditItem Text="A380" Value="A380"/>
                                                                            <dx:ListEditItem Text="A380" Value="A380"/>
                                                                        </Items>
                                                                    </dx:ASPxComboBox>
                                                                </td>
                                                                <%--<td class="name">供应商:</td>
                                                                <td >
                                                                    <input type="text" id="vendor"  runat="server" value='<%# Eval("vendor") %>'  class="width100" />
                                                                </td>--%>
                                                                <td class="name">消耗工序:</td>
                                                                <td >
                                                                    <input type="text" id="ps_op"  runat="server" value='<%# Eval("ps_op") %>'  class="width100" />
                                                                </td>
                                                                <td class="name">备注:</td>
                                                                <td >
                                                                    <input type="text" id="note"  runat="server" value='<%# Eval("note") %>'    />
                                                                </td>
                                                                                                                              
                                                                 
                                                            </tr>
                                                        </table>
                                                        <div style="text-align: right; padding-top: 8px">
                                                            <dx:ASPxTreeListTemplateReplacement ID="ASPxTreeListTemplateReplacement1" runat="server"
                                                                ReplacementType="UpdateButton" />
                                                            <dx:ASPxTreeListTemplateReplacement ID="ASPxTreeListTemplateReplacement2" runat="server"
                                                                ReplacementType="CancelButton" />
                                                        </div>
                                                    </EditForm>
                                                </Templates>
                                                <Columns>
                                                    <dx:TreeListTextColumn FieldName="pt_part" Caption="存货编号" EditCellStyle-CssClass="" VisibleIndex="0">
                                                        <EditFormSettings VisibleIndex="0" ColumnSpan="1" />
                                                    </dx:TreeListTextColumn>
                                                    <dx:TreeListTextColumn FieldName="pt_desc1" Caption="描述(零件号)" VisibleIndex="1">
                                                        <EditFormSettings VisibleIndex="1" ColumnSpan="1" />
                                                    </dx:TreeListTextColumn>
                                                    <dx:TreeListDataColumn FieldName="pt_desc2" Caption="描述2(零件名称)" VisibleIndex="2">
                                                        <EditFormSettings VisibleIndex="2" ColumnSpan="1" />
                                                    </dx:TreeListDataColumn>
                                                    <dx:TreeListTextColumn FieldName="drawno" Caption="图纸号" VisibleIndex="3" >
                                                        <EditFormSettings VisibleIndex="3" ColumnSpan="1"  />
                                                    </dx:TreeListTextColumn>
                                                    <dx:TreeListTextColumn FieldName="ps_qty_per" Caption="单件用量" VisibleIndex="4"  PropertiesTextEdit-ValidationSettings-RequiredField-IsRequired="true">
                                                        <EditFormSettings VisibleIndex="4" ColumnSpan="1"  />
                                                    </dx:TreeListTextColumn>
                                                    <dx:TreeListTextColumn FieldName="unit" Caption="单位" VisibleIndex="5">
                                                        <EditFormSettings VisibleIndex="5" ColumnSpan="1" />
                                                    </dx:TreeListTextColumn>
                                                    <dx:TreeListTextColumn FieldName="pt_net_wt" Caption="单件重量" VisibleIndex="6">
                                                        <EditFormSettings VisibleIndex="6" ColumnSpan="1" />
                                                    </dx:TreeListTextColumn>
                                                    <dx:TreeListTextColumn FieldName="material" Caption="材料" VisibleIndex="7">
                                                        <EditFormSettings VisibleIndex="7" ColumnSpan="1" />
                                                    </dx:TreeListTextColumn>
                                                    <dx:TreeListTextColumn FieldName="vendor" Caption="供应商" VisibleIndex="8" Visible="false">
                                                        <EditFormSettings VisibleIndex="8" ColumnSpan="1" />
                                                    </dx:TreeListTextColumn>
                                                    <dx:TreeListTextColumn FieldName="ps_op" Caption="消耗工序" VisibleIndex="9">
                                                        <EditFormSettings VisibleIndex="9" ColumnSpan="1" />
                                                    </dx:TreeListTextColumn>
                                                    <dx:TreeListTextColumn FieldName="note" Caption="备注" VisibleIndex="10">
                                                        <EditFormSettings VisibleIndex="10" ColumnSpan="1" />
                                                    </dx:TreeListTextColumn>
                                                    <dx:TreeListTokenBoxColumn FieldName="PID" Caption="pid" Visible="false">
                                                        <PropertiesTokenBox AllowMouseWheel="True" Tokens="">
                                                        </PropertiesTokenBox>
                                                        <EditFormSettings VisibleIndex="11" Visible="False"  />
                                                    </dx:TreeListTokenBoxColumn>
                                                    <dx:TreeListTokenBoxColumn FieldName="id" Caption="id" Visible="false" VisibleIndex="12">
                                                        <PropertiesTokenBox AllowMouseWheel="True" Tokens="">
                                                        </PropertiesTokenBox>
                                                        <EditFormSettings VisibleIndex="12" Visible="False"  />
                                                    </dx:TreeListTokenBoxColumn>
                                                    <dx:TreeListCommandColumn ShowNewButtonInHeader="true" Caption="x" VisibleIndex="13">
                                                        <EditButton Visible="true" Text="修改" />
                                                        <NewButton Visible="true" Text="添加" />
                                                        <DeleteButton Visible="true" Text="删除" />
                                                    </dx:TreeListCommandColumn>

                                                </Columns>
                                            </dx:ASPxTreeList>
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
            <div class="row  row-container">
                <div class="col-md-12">
                    <div class="panel panel-info">
                        <div class="panel-heading" data-toggle="collapse" data-target="#rec">
                            <strong>变更记录</strong>
                        </div>
                        <div class="panel-body " id="rec">
                            <div class="col-xs-12 col-sm-12  col-md-12 col-lg-12">
                                <div>
                                     
                                    <asp:GridView ID="gvVerRec" runat="server" CellPadding="4" BorderWidth="1px" BorderColor="Silver" ForeColor="#333333" GridLines="Horizontal" ShowHeaderWhenEmpty="True" Width="100%" 
                                         AutoGenerateColumns="False" EditIndex="0" OnRowDataBound="gvVerRec_RowDataBound" SelectedIndex="0">
                                        <AlternatingRowStyle BackColor="White" />
                                        <Columns>
                                            <asp:BoundField DataField="id" HeaderText="id"  Visible="false"/>
                                            <asp:TemplateField HeaderText="序号">
                                                <EditItemTemplate>
                                                    <asp:Label ID="Label0" runat="server" Text='<%# Bind("id") %>'></asp:Label>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("id") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                             
                                            <asp:TemplateField HeaderText="版本">
                                                <EditItemTemplate>
                                                    <asp:Label ID="TextBox2" runat="server" Text='<%# Bind("version") %>'></asp:Label>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("version") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="变更前">
                                                <EditItemTemplate>
                                                    <asp:TextBox TextMode="MultiLine"   Width="100%" ID="beforupdate" runat="server"   Text='<%# Bind("beforupdate") %>'></asp:TextBox>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="Label3" runat="server" Text='<%# Bind("beforupdate") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="变更后">
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="afterupdate" TextMode="MultiLine" Width="100%" runat="server" Text='<%# Bind("afterupdate") %>'></asp:TextBox>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="Label4" runat="server" Text='<%# Bind("afterupdate") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="变更原因">
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="updatereason" TextMode="MultiLine"  Width="100%" runat="server" Text='<%# Bind("updatereason") %>'></asp:TextBox>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="Label5" runat="server" Text='<%# Bind("updatereason") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="图纸版本">
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="drawver" TextMode="MultiLine"  Width="100%" runat="server" Text='<%# Bind("drawver") %>'></asp:TextBox>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="Label6" runat="server" Text='<%# Bind("drawver") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <EditRowStyle BackColor="white" />
                                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                        <RowStyle BackColor="#EFF3FB" />
                                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                        <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                        <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                        <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                        <SortedDescendingHeaderStyle BackColor="#4870BE" />
                                        <EmptyDataTemplate >  <div style="text-align:center">暂无变更记录</div></EmptyDataTemplate>
                                    </asp:GridView>
                                     
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

