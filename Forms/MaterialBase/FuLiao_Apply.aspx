<%@ Page Title="辅料申请" Language="C#" AutoEventWireup="true" CodeFile="FuLiao_Apply.aspx.cs" Inherits="Forms_MaterialBase_FuLiao_Apply" MaintainScrollPositionOnPostback="True" ValidateRequest="true"  %>

<%@ Register Assembly="DevExpress.Web.v17.2, Version=17.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script src="../../Content/js/jquery.min.js"></script>
    <script src="../../Content/js/bootstrap.min.js"></script>
    <script src="../../Content/js/layer/layer.js"></script>
    <link href="../../Content/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../../Content/css/custom.css" rel="stylesheet" type="text/css" />
    <script src="../../Content/js/layer/layer.js" type="text/javascript"></script>
    <link href="../../Content/js/plugins/bootstrap-select/css/bootstrap-select.min.css" rel="stylesheet" />
    <script src="../../Content/js/plugins/bootstrap-select/js/bootstrap-select.min.js"></script>
    <script src="../../Scripts/RFlow.js" type="text/javascript"></script>

      <script type="text/javascript">
        var paramMap;
        $(document).ready(function () {
            $("#mestitle").html("【辅料申请】<a href='/userguide/reviewGuide.pptx' target='_blank' class='h4' style='display:none'>使用说明</a>");
            SetButtons();
            $("input[id='unit_I']").css("background-color", "#FDF7D9");
            var type = $("#fltype").find("option:selected").text();
            if (type != "" && type != "包材") {  
                $("#cailiao1").css("display", "none");
                $("#lbl_FL_Main_Main_10").css("display", "none");
                $("#cailiao2").css("display", "none");
                $("#lbl_FL_Main_Main_11").css("display", "none");
            }
            //工厂多选
            $("input[id*='ddldomain']").click(function () {
                var val = "";
                $("input[id*='ddldomain']").each(function (i, item) {
                    if ($(item).prop("checked") == true) {
                        val = val + $(item).val() + ";";
                    }
                })
                if (val.length > 0) {
                    val = val.substring(0, val.length - 1);
                }
                cgy = val.replace("100", "S01").replace("200", "K01");
                $("#buyer_planner").val(cgy);
                $("#domain").val(val);
            })

            //设置默认值
            $("input[id*='cpleibie']").val($("#cpleibie").val() == "" ? "Z" : $("#cpleibie").val());

            //获取参数名值对集合Json格式
            var url = window.parent.document.URL;
            paramMap = getURLParams(url);
            //            if (paramMap.wlh != NaN && paramMap.wlh != "" && paramMap.wlh != undefined) {
            //                $("#wlh").val(paramMap.wlh);
            //                //修改时，下拉不可选
            //                $("#type").attr("disabled", "disabled");
            //                $("#class").attr("disabled", "disabled");
            //            }
            //            if (paramMap.stepid != "") {
            //                $("#ddldomain").attr("disabled", "disabled");
            //            }
            //如果为修改，显示红色
            if ((paramMap.state == "edit") || ($("#formstate").val().indexOf("edit") != -1))
            { $("#warning").css("display", ""); }

        });      // end ready


        //提出自定流程 JS 
        function setComment(val) {
            $('#comment', parent.document).val(val);
        }

        //设定表字段状态（可编辑性）
        var tabName = "PGI_FLMstr_DATA_Form"; //表名
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


            </script>


            <style type="text/css">       
        .lineread{
            font-size:12px; border:none; border-bottom:1px solid lightgray;background-color:#ffffff;width:90%;
        }
         .line{
            font-size:12px; border:none; border-bottom:1px solid lightgray; width:90%;
        }
        .linewrite{
            font-size:12px; border:none; border-radius:0.5em;  border-bottom:1px solid #ccc;background-color:#FDF7D9;width:100%;/*EFEFEF*/
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
    </style>
</head>
<body>
<script type="text/javascript">
    var fieldStatus = "1"=="<%=Request.QueryString["isreadonly"]%>"? {} : <%=fieldStatus%>;
    var displayModel = '<%=DisplayModel%>';
     var fltype = '<%=ViewState["fltype"].ToString() %>';
     var UidRole = '<%=ViewState["UidRole"].ToString() %>'; 
      $(window).load(function (){
      SetControlStatus(<%=fieldStatus%>);
      });
         
 
          function validate(action){
           var flag=true;var msg="";
            <%=ValidScript%>
         if($('#IsAttachment').is(':checked')==false) 
         {
           if($("#domain").val()=="") 
             { msg+="请选择【申请工厂】.<br />";} 
           
           var type=$("#fltype").find("option:selected").text();
          // alert(type);
             if(type=="")
             { msg+="请选择【辅料类型】.<br />";} 
         
            if($("#wlmc").val()=="" ||  $("#ms").val()=="" )
             { msg+="请填写【描述一】或【描述二】.<br />";} 
            if(type=="包材" && ( $("#cailiao1").val()=="" ||  $("#cailiao2").val()==""))
            {
            msg+="包材类请填写【材料一】和【材料二】.<br />";
            }

            var wlmc=$("#wlmc").val();
            var ms=$("#ms").val();
            var result_mc = wlmc .replace(/[^\x00-\xff]/g, '**');
             var result_ms = ms .replace(/[^\x00-\xff]/g, '**');

             if(result_mc.length>24)
             {msg+="描述一字符长度大于24，请修改.<br />";}

             if(result_ms.length>24)
             { msg+="描述二字符长度大于24，请修改.<br />"; }

             if($("#unit").val()=="")
             { msg+="请选择【单位】.<br />";  } 

//            if($("#pt_status").val()=="")
//            {msg+="请选择【产品状态(申请)】.<br />"; } 

           if($("#remark").val()=="")
            { msg+="【提交说明】不可为空.<br />";} 

             if( $("#quantity_min").attr("readonly")!="readonly" && $("#quantity_min").val()=="" )
             {  msg+="请填写【最小订单量】.<br />"};   
             
             if( $("#quantity_max").attr("readonly")!="readonly" && $("#quantity_max").val()=="" )
            {  msg+="请填写【最大订单量】.<br />"};   

            if( $("#status").attr("readonly")!="readonly" && $("#status").val()=="" )
            {  msg+="请选择【产品状态(财务)】.<br />"};   

             if( $("#jzweight").attr("readonly")!="readonly" && $("#jzweight").val()=="" )
            {  msg+="请填写【净重】.<br />"};   

             if( $("#aqkc").attr("readonly")!="readonly" && $("#aqkc").val()=="" )
            {  msg+="请填写【安全库存】.<br />"};   

            if( $("#ddbs").attr("readonly")!="readonly" && $("#ddbs").val()=="" )
            {  msg+="请填写【订单倍数】.<br />"};   

            if( $("#ddsl").attr("readonly")!="readonly" && $("#ddsl").val()==""  )
               {  msg+="请填写【订单数量】.<br />"};   

            if( $("#dhperiod").attr("readonly")!="readonly" && $("#dhperiod").val()=="" )
               {  msg+="请填写【订货周期】.<br />"};   

           if( $("#purchase_days").attr("readonly")!="readonly" && $("#purchase_days").val()=="" )
             {  msg+="请填写主地点【采购提前期】.<br />"};  
           if($("#comment").val()=="")
            { msg+="请填写【处理意见】.<br />";} 
         }
         else
         {
           var type=$("#fltype").find("option:selected").text();
             if(type=="")
             { msg+="请选择【辅料类型】.<br />";} 
              if($("#domain").val()=="") 
             { msg+="请选择【申请工厂】.<br />";} 

               if($("#upload").val()=="" && (UidRole=="") && $("#upload").length>0 ){
               {  
               msg+="请选择文件上传.<br />"};  
                }   
                if($("#comment").val()=="")
            { msg+="请填写【处理意见】.<br />";} 
             var totalRow = $("#<%=gv.ClientID %> tr").length ;
              if(totalRow<1 && UidRole=="Purchaser")
              {
              msg+="请选择文件上传.<br />";
              }         
         }


           if(msg!="")
           {  
                flag=false;
                layer.alert(msg);
                return flag;
            }
          }

</script>
<form id="form1" runat="server" enctype="multipart/form-data">

        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
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
                 <%--   <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>--%>
                            <script type="text/jscript">
                                var prm = Sys.WebForms.PageRequestManager.getInstance();
                                prm.add_endRequest(function () {
                                    // re-bind your jquery events here
                                    SetButtons();
                                });
                            </script>
                            <asp:Button ID="btnSave" runat="server" Text="保存" 
                                CssClass="btn btn-default btn-xs btnSave"   
                                OnClick="btnSave_Click" ToolTip="临时保存此流程" Height="23px" />
                            <asp:Button ID="btnflowSend" runat="server" Text="发送" CssClass="btn btn-default btn-xs btnflowSend" OnClientClick=" return validate('submit');" OnClick="btnflowSend_Click" />
                            <input id="btnaddWrite" type="button" value="加签" onclick="parent.addWrite(true);" class="btn btn-default btn-xs btnaddWrite" />
                            <input id="btnflowBack" type="button" value="退回" onclick="parent.flowBack(true);" class="btn btn-default btn-xs btnflowBack" />
                            <input id="btnflowCompleted" type="button" value="完成" onclick="parent.flowCompleted(true);" class="btn btn-default btn-xs btnflowCompleted" />
                            <input id="btnshowProcess" type="button" value="查看流程" onclick="parent.showProcess(true);" class="btn btn-default btn-xs btnshowProcess" />
                            <input id="btntaskEnd" type="button" value="终止流程" onclick="parent.taskEnd(true);" class="btn btn-default btn-xs btntaskEnd" />
                       <%-- </ContentTemplate>
                    </asp:UpdatePanel>--%>

                </div>
            </div>
        </div>
        <asp:TextBox ID="txtInstanceID" runat="server" CssClass=" hidden" ToolTip="0|0" Width="40" />
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
                                        <td style="width:80px">申请人</td>
                                        <td style="width:250px">
                                            <div class="form-inline">
                                                <asp:TextBox runat="server" ID="CreateById" class="line"  Style=" width: 90px; font-size:12px" ReadOnly="True"></asp:TextBox>
                                                <asp:TextBox runat="server" ID="CreateByName" class="line"  Style=" width: 90px; font-size:12px" ReadOnly="True"></asp:TextBox>
                                            </div>
                                        </td>
                                        <td style="width:80px">申请部门</td>
                                        <td style="width:250px">
                                            <div class="form-inline">                                                
                                                 <asp:TextBox runat="server" ID="CreateByDept" class="line"  Style=" width: 90px; font-size:12px" ReadOnly="True"></asp:TextBox>
                                            </div>
                                        </td>
                                        <td style="width:80px">申请时间</td>
                                        <td style="width:250px">
                                            <asp:TextBox runat="server" ID="CreateDate" class="line"  Style=" width: 180px; font-size:12px" ReadOnly="True"></asp:TextBox>
                                        </td>
                                          <td style="width:80px">表单号</td>
                                        <td style="width:250px">
                                             <asp:TextBox runat="server" ID="Formno" class="line"  Style=" width: 180px; font-size:12px" ReadOnly="True" placeholder="自动产生" ></asp:TextBox>
                                             <asp:TextBox ID="formstate" runat="server" CssClass=" hidden" ToolTip="0|0" Width="40" />
                                               <asp:TextBox ID="formsite" runat="server" CssClass=" hidden" ToolTip="0|0" Width="40" />
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
                                    <table style="width: 80%" border="0" runat="server" id="tblWLLeibie">
                                        <tr>
                                            <td style=" width:80px">申请工厂</td>
                                                <td  style=" width:300px">
                                                <asp:CheckBoxList ID="ddldomain" runat="server" CssClass="form-control " RepeatDirection="Horizontal" Width="200px"></asp:CheckBoxList>
                                                <asp:TextBox ID="domain" runat="server" CssClass=" hidden" ToolTip="0|1" Width="40" /></td>
                                                 <td style=" width:100px">辅料类型</td>
                                                <td  style=" width:150px">
                                                <asp:DropDownList ID="fltype" runat="server" CssClass="form-control disabled" ToolTip="0|1" AutoPostBack="True" OnSelectedIndexChanged="fltype_SelectedIndexChanged" Width="200px" BackColor="#FDF7D9"></asp:DropDownList>
                                            </td>
                                             <td  >
                                                <asp:CheckBox ID="IsAttachment" runat="server" 
                                                     AutoPostBack="True" 
                                                     oncheckedchanged="IsAttachment_CheckedChanged"></asp:CheckBox>附件上传
                                           <asp:TextBox ID="Attachment" runat="server" CssClass=" hidden" Text="是" ToolTip="0|0" Width="40" />
                                           <asp:HyperLink ID="HyperLink2" Visible="false" runat="server"  NavigateUrl="~/Forms/MaterialBase/辅料上传范例.xlsx" ForeColor="Red">(上传格式)</asp:HyperLink>
                                            </td>
                                           
                                        </tr>
                                    </table>

                             
                            </div>
                        </div>
                
                    </div>

                    
                                      
                </div>
            </div>
            <div class="row  row-container" id="div_base" runat="server">
                <div class="col-md-12">
                    <div class="panel panel-info">
                        <div class="panel-heading" data-toggle="collapse" data-target="#PB">
                            <strong style="padding-right: 100px">物料属性<span id="warning" style="color: red; display: none">此单为物料主数据修改，请谨慎.</span></strong>
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

              <div class="row  row-container"  id="div_main" runat="server">
                    <div class="panel panel-info">
                        <div class="panel-heading" data-toggle="collapse" data-target="#PC">
                            <strong style="padding-right: 100px">物料主地点数据/计划数据</strong>
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
      

         <div class="row  row-container" >
             <div class="auto-style1" id="div_sc" runat="server"  style=" display:none">
                 <div class="panel panel-info">
                     <div class="panel-heading" data-toggle="collapse" data-target="#FJSC">
                         <strong>物料基础数据上传
                        
                             <span id="warnmsg" style="color: red;" /></strong>
                     </div>
                     <div class="panel-body   collapse in" id="FJSC">
                         <div class="col-xs-12 col-sm-12  col-md-12 col-lg-12">
                             <div>
                                 <asp:Table Style="width: 100%" border="0" runat="server" ID="tblWLSC">
                                 </asp:Table>
                             </div>
                         </div>
                     </div>
                 </div>
                 </div>
             </div>
             <div class="auto-style1" id="div2" runat="server" style="display: none">
                 <div class="panel panel-info">
                     <div class="panel-heading" data-toggle="collapse" data-target="#FJimport">
                         <strong>物料基础数据汇入</strong>
                     </div>
                     <div class="panel-body   collapse in" id="FJimport">
                         <div class="col-xs-12 col-sm-12  col-md-12 col-lg-12">
                            <%-- <div id="DivImport" runat="server">
                                 <table style="width: 80%" border="0" runat="server" id="tblWLImport">
                                     <tr>
                                         <td style="width: 27%">
                                             批量导入
                                         </td>
                                         <td style="width: 500px">
                                             <input id="File1" style="width: 300px" type="file" runat="server" />
                                         </td>
                                         <td>
                                             <asp:Button ID="Button2" runat="server" Text="导入预览" class="btn  btn-primary"
                                                 Width="100px" OnClick="Button2_Click" />
                                         </td>
                                     </tr>
                                 </table>
                             
                             </div>--%>
                                            <div class="form-inline">
                                               
                                                <br />
                                                <table runat="server">
                                                <tr><td style="width: 35%">Excel附件上传 <asp:HyperLink ID="HyperLink1" runat="server"  NavigateUrl="~/Forms/MaterialBase/辅料上传范例.xlsx" ForeColor="Red">(上传格式)</asp:HyperLink></td>
                                                    <td>
                                                        <asp:GridView ID="gvFile_ddfj" runat="server" AutoGenerateColumns="False"
                                                            ForeColor="#333333" GridLines="None">
                                                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                                            <Columns>
                                                                <asp:TemplateField ShowHeader="False">
                                                                    <ItemTemplate>
                                                                        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<% #DataBinder.Eval(Container.DataItem, "File_lj")%>'
                                                                            Text='<%#DataBinder.Eval(Container.DataItem,"File_name")%>'></asp:HyperLink>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </td>
                                                <td style="width:25%"> 
                                                    <input id="upload_fj" type="file" class="form-control" style="width: 90px"
                                                        multiple="multiple" runat="server" />
                                                    <asp:Button ID="Btn_ddfj" runat="server" CssClass="form-control"
                                                        Text="上传文件" OnClick="Btn_ddfj_Click" />
                                                    <button id="btn_import" type="button" class="btn btn-primary btn-large" runat="server" onserverclick="btn_import_Click" style=" display:none"><i class="fa fa-download fa-fw"></i>&nbsp;导出</button>
                                                 </td>
                                                </tr>
                                                </table>
                                                <dx:ASPxGridView ID="gv" runat="server">
                                                    <SettingsPager PageSize="50">
                                                    </SettingsPager>
                                                    <SettingsBehavior AllowSort="False" />
                                                </dx:ASPxGridView>
                                                 <dx:ASPxGridViewExporter ID="ASPxGridViewExporter1" runat="server">
                                           </dx:ASPxGridViewExporter>
                                            </div>

                         </div>
                     </div>
                 </div>
             </div>

             <div class="auto-style1" id="divwarn" runat="server" style=" display:none">
                 <div class="panel panel-info">
                     <div class="panel-heading" data-toggle="collapse" data-target="#Div_warn">
                         <strong>附件上传时维护的注意事项</strong>
                     </div>
                     <div class="panel-body   collapse in" id="Div_warn">
                         <div class="col-xs-12 col-sm-12  col-md-12 col-lg-12">
                             <div id="Div4" runat="server">
                                 <table border="0" width="100%">
                                     <tr>
                                      <td width="100px">
                                             <label>
                                                申请人填单时需维护Excel中的<font  style="color: red;" >描述一、描述二、产品类、单位、产品状态、净重、净重单位、安全库存</font></label>
                                         </td></tr>
                                        <tr> <td width="100px">
                                             <label>
                                                 仓库人员签核时需确认Excel中的<font  style="color: red;" >安全库存</font></label>
                                         </td>
                                     </tr>
                                     <tr>
                                         <td width="100px">
                                             <label>
                                                 采购人员签核时需维护Excel中的<font  style="color: red;" >订货周期、最小订单量、最大订单量、订单倍数、采购提前期</font></label>
                                         </td>
                                     </tr>
                                 </table>
                             </div>
                         </div>
                     </div>
                 </div>
             </div>

            <div class="row  row-container">
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
                                        <textarea id="comment" type="text" placeholder="请在此处输入处理意见" class="form-control" onchange="setComment(this.value)" ></textarea></td>
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
