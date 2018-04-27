<%@ Page Title="请购申请单(PR)" Language="C#" AutoEventWireup="true" CodeFile="PUR_PR.aspx.cs" Inherits="PUR_PR" MaintainScrollPositionOnPostback="True" ValidateRequest="true" %>
 
<%@ Register assembly="DevExpress.Web.v17.2, Version=17.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web" tagprefix="dx" %>
 
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
    <script src="../../../Content/js/plugins/bootstrap-select/js/bootstrap-select.min.js"></script>
    <script src="../../Scripts/RFlow.js"></script>
    <script type="text/javascript">
        var paramMap;
        $(document).ready(function () {
            $("#mestitle").html("【请购申请单(PR)】<a href='/userguide/reviewGuide.pptx' target='_blank' class='h4' style='display:none'>使用说明</a>");// 
             
          //  SetButtons();
            
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
            //是否需采购询价
            $("input[id*='chkaskprice']").click(function(){
                $("#askprice").val("");
                var val="是" 
                $("input[id*='chkaskprice']").each(function(i,item){
                    if($(item).prop("checked")==true){                       
                        val="是";
                    }
                    else{
                        val="否";
                    }
                })
                $("#askprice").val(val);                 
            })



            //var chkState=$("#askprice").val()=="否"?false:true;            
            //$("input[id*='chkaskprice']").prop("checked",chkState);
            //获取参数名值对集合Json格式
            var url =window.parent.document.URL;        
            paramMap = getURLParams(url); 
            if(paramMap.wlh!=NaN&&paramMap.wlh!=""&&paramMap.wlh!=undefined)
            {
                $("#wlh").val(paramMap.wlh);
                //修改时，下拉不可选
                $("#type").attr("disabled","disabled");
                $("#class").attr("disabled","disabled");
            }       

           

        })// end ready


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
                    else if( statu.indexOf("1_")!="-1" && ( ctype=="checkbox"||ctype=="radio" ||ctype=="file" ) ){
                        $("#"+id).attr("disabled","disabled");
                    }
                    else if(statu.indexOf("1_")!="-1" && ctype=="select")//
                    {  
                        $("#"+id).attr("readonly","readonly");
                        $("#"+id).focus(function () {
                            this.defaultIndex = this.selectedIndex;
                        }).change( function () {
                            this.selectedIndex = this.defaultIndex;
                        })
                    }
                }
            }
        }
        
    </script>
   

    <link href="../../Content/css/custom.css" rel="stylesheet" />
     
</head>
<body>
    <script type="text/javascript">
	   <%-- var initData = <%=BWorkFlow.GetFormDataJsonString(initData)%>;--%>
        var fieldStatus = "1"=="<%=Request.QueryString["isreadonly"]%>"? {} : <%=fieldStatus%>;
        var displayModel = '<%=DisplayModel%>';
        $(window).load(function (){
            SetControlStatus(<%=fieldStatus%>);
 
            for(var item in fieldStatus){
                var id=item.replace(tabName.toLowerCase()+"_","");
                if(id=="pt_status"){
                    var statu=fieldStatus[item];
                    //在可编辑状态下
                    if(statu.indexOf("0_")>-1){ 
                        //pt_status 在新增wlh时状态不可选择
                        if((paramMap.wlh==NaN||paramMap.wlh==""||paramMap.wlh==undefined)||($("#formstate").val().indexOf("new")>-1))
                        { 
                            $("#pt_status").attr("readonly",true).focus(function () {
                                this.defaultIndex = this.selectedIndex;
                            }).change( function () {
                                this.selectedIndex = this.defaultIndex;
                            });
                        }
                        
                    }
                }
            }

          
                
        });

        //验证
        function validate(id){
            if($("#type").val()==""){
                layer.alert("请选择【申请类型】.");
                return false;
            } 
            if( ($("#pt_status").val()!="AC" && $("#pt_status").val()!="OBS") && ($("#pt_status").attr("readonly")==false||$("#pt_status").attr("readonly")==undefined) ){
                layer.alert("请选择【状态】. AC/OBS 选其一.（说明 AC:启用；OBS:物料停用清库存；Dead:库存为0,物料停用.）");
                return false;
            }

            <%=ValidScript%>

            if($("#upload").val()==""&& $("#link_upload").text()==""){
                layer.alert("请选择【图纸附件】");
                return false;
            }           
            if($("#typedesc").val()==""){
                layer.alert("请输入【提交说明】.用于项目号，零件号等提交申请原因.");
                return false;
            } 

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
        
        function Getdaoju(e)
        {            
            var ss = e.id.split("_");// 在每个逗号(,)处进行分解。
            var url = "../../select/sz_report_dev_select.aspx?id=" + ss[4] + "&domain=" + $("input[id*='domain']").val()           
            popupwindow = window.open(url, '_blank', 'height=500,width=1000,resizable=yes,menubar=no,scrollbars =yes,location=no');
        }
        function setvalue_dj(id, wlh, ms, lx, js, sm, pp, gys,wlmc)
        { //gvdtl_cell0_5_wlh_0
            $("input[id*='gvdtl_cell" + id + "_4_wlh_" + id+ "']").val(wlh);
            $("input[id*='gvdtl_cell" + id + "_5_wlmc_" + id + "']").val(wlmc);
            $("input[id*='gvdtl_cell" + id + "_6_wlms_" + id + "']").val(ms);
            //$("input[id*='MainContent_gv_cell" + id + "_6_jiaoshu_" + id + "_I']").val(js);
            //$("input[id*='MainContent_gv_cell" + id + "_8_edsm_" + id + "_I']").val(sm);
            //$("input[id*='MainContent_gv_cell" + id + "_9_smtzxs_" + id + "_I']").val(1);
            //$("input[id*='MainContent_gv_cell" + id + "_12_brand_" + id + "_I']").val(pp);
            //$("input[id*='MainContent_gv_cell" + id + "_13_supplier_" + id + "_I']").val(gys);
            popupwindow.close();
        }

    </script>
     <script type="text/javascript"> 
        $(function () { 
            $("#<%=gvdtl.ClientID%>").find("tr td input[id*=qty]").each(function () { 
                $(this).bind("change", function () { 
                    //if (this.checked) { 
                        var price = $(this).parent().parent().find("input[id*=targetPrice]").text(); 
                        var qty = $(this).parent().parent().find("input[id*=qty]").text(); 
                       // var total = $(this).parent().parent().find("input[id*=targetTotal]").text(); 
  
                        var result = (parseFloat(price) * parseFloat(qty)) ; 
                        $(this).parent().parent().find("input[id*=targetTotal]").val(result); 
                    //} else { 
                    //    $(this).parent().parent().find("input[id*=avg_value]").val(""); 
                    //} 
                }); 
            }); 
        }); 
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
                            <asp:Button ID="btnSave" runat="server" Text="保存" CssClass="btn btn-default btn-xs btnSave" OnClientClick="" OnClick="btnSave_Click" ToolTip="临时保存此流程" />
                            <asp:Button ID="btnflowSend" runat="server" Text="发送" CssClass="btn btn-default btn-xs btnflowSend" OnClientClick="return validate();" OnClick="btnflowSend_Click" />
                            <input id="btnaddWrite" type="button" value="加签" onclick="parent.addWrite(true);" class="btn btn-default btn-xs btnaddWrite" />
                            <input id="btnflowBack" type="button" value="退回" onclick="parent.flowBack(true);" class="btn btn-default btn-xs btnflowBack" />
                            <input id="btnflowCompleted" type="button" value="完成" onclick="parent.flowCompleted(true);" class="btn btn-default btn-xs btnflowCompleted" />
                            <input id="btnshowProcess" type="button" value="查看流程" onclick="parent.showProcess(true);" class="btn btn-default btn-xs btnshowProcess" />
                            <input id="btntaskEnd" type="button" value="终止流程" onclick="parent.taskEnd(true);" class="btn btn-default btn-xs btntaskEnd" />
                        </ContentTemplate>
                    </asp:UpdatePanel>

                </div>
            </div>
        </div>
        <asp:TextBox ID="txtInstanceID" runat="server" CssClass=" hiddens" ToolTip="0|0" Width="40" />
        <div class="col-md-10">
            <div class="row row-container">
                <div class="col-md-12">
                    <div class="panel panel-info">
                        <div class="panel-heading" data-toggle="collapse" data-target="#SQXX">
                            <strong>申请记录基础信息</strong>
                        </div>
                        <div class="panel-body collapse in" id="SQXX">
                            <div class="col-xs-12 col-sm-12  col-md-12 col-lg-12">
                                <div class="">
                                    <asp:UpdatePanel ID="UpdatePanel_request" runat="server">
                                        <ContentTemplate>
                                            <table style="height: 35px; width: 100%">
                                                <tr>
                                                    <td>请购单号</td>
                                                    <td>
                                                        <asp:TextBox ID="prno" runat="server" CssClass="form-control input-s-sm  " readonly="true" Width="250px" ToolTip="0|0" />
                                                    </td>
                                                    <td>申请日期：</td>
                                                    <td>
                                                        <asp:TextBox ID="CreateDate" CssClass="form-control input-s-sm" Style="height: 30px; width: 100px" runat="server" ReadOnly="True" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>申请人：</td>
                                                    <td>
                                                        <div class="form-inline">
                                                            <asp:TextBox runat="server" ID="CreateById" CssClass="form-control input-s-sm" Style="height: 35px; width: 70px" ReadOnly="True"></asp:TextBox>
                                                            <asp:TextBox runat="server" ID="CreateByName" CssClass="form-control input-s-sm" Style="height: 35px; width: 70px" ReadOnly="True"></asp:TextBox>                                                           
                                                            <asp:TextBox runat="server" id="CreateByDept" class="form-control input-s-sm" style="height: 35px; width: 100px" readonly="True" />
                                                        </div>
                                                    </td>                                                    
                                                    <td style="display:">电话（分机）：
                                                    </td>
                                                    <td style="display:">                                                        
                                                            <input id="phone" class="form-control input-s-sm" style="height: 35px; width: 100px" runat="server"  />                                                            
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>当前登陆人</td>
                                                    <td>
                                                        <div class="form-inline">
                                                            <input id="txt_LogUserId" class="form-control input-s-sm" style="height: 35px; width: 70px" runat="server" readonly="True" />
                                                            <input id="txt_LogUserName" class="form-control input-s-sm" style="height: 35px; width: 70px" runat="server" readonly="True" />
                                                            <input id="txt_LogUserDept" class="form-control input-s-sm" style="height: 35px; width: 100px" runat="server" readonly="True" />                                                           
                                                        </div>
                                                    </td>                                                    
                                                    <td>申请人公司</td>
                                                    <td>                                                       
                                                        <asp:TextBox ID="domain" runat="server" CssClass="form-control input-s-sm" Width="100px" ToolTip="0|1"  ReadOnly="true" /></td>
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
                            <strong>用途信息</strong>
                        </div>
                        <div class="panel-body " id="PA">
                            <div class="col-xs-12 col-sm-12  col-md-12 col-lg-12">
                                <div>
                                    <table style="" border="0" runat="server" id="tblWLLeibie" >
                                        <tr>
                                            <td>采购类别</td>
                                            <td >
                                                <asp:DropDownList ID="prtype" runat="server" CssClass="form-control" ToolTip="0|1" AutoPostBack="True"  Width="200px"></asp:DropDownList>

                                            </td>
                                        </tr>                                        
                                        <tr>
                                            <td>申请原因描述</td>
                                            <td style="width:800px">
                                                <asp:TextBox ID="prreason" runat="server" CssClass="form-control input-s-sm" Width="100%" ToolTip="0|0"   />                                               
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
                            <strong style="padding-right: 100px">采购物品信息<span id="warning" style="color: red; display: none"> </span></strong>
                        </div>
                        <div class="panel-body  collapse in" id="PB">
                            <div class="col-xs-12 col-sm-12  col-md-12 col-lg-12">
                                <div>                                    
                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional"><ContentTemplate>
                                        <script type="text/jscript">
                                            var prm = Sys.WebForms.PageRequestManager.getInstance();
                                            prm.add_endRequest(function () {
                                                // re-bind your jquery events here                                                
                                                $("#gvdtl").find("tr td input[id*=qty],tr td input[id*=targetPrice]").each(function () { 
                                                    $(this).bind("change", function () {                                                          
                                                        var price = $(this).parent().parent().find("input[id*=targetPrice]").val(); 
                                                        var qty = $(this).parent().parent().find("input[id*=qty]").val();                                                          
                                                        if(price!=null&&qty!="")
                                                        {   
                                                            var result = (parseFloat(price) * parseFloat(qty)) ; 
                                                            $(this).parent().parent().find("input[id*=targetTotal]").val(result); 
                                                        }else{  $(this).parent().parent().find("input[id*=targetTotal]").val("")}
                                                       
                                                    }); 
                                                }); 
                                                $("#gvdtl").find("tr td input[id*=wlh]").each(function () { 
                                                    //$(this).bind("click", function () {  
                                                    //    wlh=this.id;
                                                    //    wlms=$(this).parent().parent().find("input[id*=wlms]").id;
                                                    //    alert(wlms)
                                                    //      window.open("../open/select.aspx?windowid=mat&ctrl1="+wlh)
                                                       
                                                    //}); 
                                                });

                                            });
                                        </script>
                                        
                                                                                      
                                    <asp:Button ID="btnAddDetl" runat="server" Text="添加"  OnClick="btnAddDetl_Click"/>
                                    <asp:Button ID="btnDelete" runat="server" Text="删除"  OnClick="btnDelete_Click"/>
                                    <%--<dx:ASPxGridView ID="gvdtl" runat="server" EnableCallBacks="False" KeyFieldName="prno" Theme="MetropolisBlue" ClientInstanceName="grid">
                                        <SettingsPager PageSize="1000" PageSizeItemSettings-Visible="true" Visible="False">
                                            <PageSizeItemSettings Items="100, 200, 500, 1000" ShowAllItem="True"></PageSizeItemSettings>
                                        </SettingsPager>
                                        <Settings ShowFilterBar="Auto"
                                            ShowFilterRowMenu="True" ShowFilterRowMenuLikeItem="True" VerticalScrollBarMode="Visible" VerticalScrollBarStyle="VirtualSmooth" VerticalScrollableHeight="170" AutoFilterCondition="BeginsWith" ShowFooter="True" />
                                        <SettingsBehavior AllowFocusedRow="false"   ColumnResizeMode="Control"  />
                                        <Styles>
                                            <Header BackColor="#1E82CD" ForeColor="White" Font-Size="12px">
                                            </Header>
                                            <DetailRow Font-Size="12px">
                                            </DetailRow>
                                        </Styles>
                                        <Columns>
                                            <dx:GridViewDataTextColumn FieldName="SelectAll" VisibleIndex="0" Caption="选择">
                                                <Settings AllowCellMerge="False" />
                                                <DataItemTemplate>
                                                    <asp:CheckBox ID="txtcb" runat="server" />
                                                </DataItemTemplate>
                                            </dx:GridViewDataTextColumn>
                                        </Columns>                                         
                                    </dx:ASPxGridView>--%>
                                        <dx:ASPxGridView ID="gvdtl" runat="server" Width="1000px" ClientInstanceName="grid" KeyFieldName="id" EnableTheming="True">
                                            <SettingsBehavior AllowDragDrop="false" AllowFocusedRow="True" AllowSelectByRowClick="True" AllowSort="false" />
                                            <SettingsPager PageSize="20">
                                            </SettingsPager>
                                            <Settings ShowFilterRow="false" ShowFilterRowMenu="false" ShowFilterRowMenuLikeItem="false" ShowFooter="True" />
                                            <SettingsCommandButton>
                                                <SelectButton ButtonType="Button" RenderMode="Button">
                                                </SelectButton>
                                            </SettingsCommandButton>
                                            <SettingsSearchPanel Visible="false" />
                                            <SettingsFilterControl AllowHierarchicalColumns="True">
                                            </SettingsFilterControl>
                                            <Styles>
                                                <Header BackColor="#99CCFF">
                                                </Header>
                                                <Footer HorizontalAlign="Right">
                                                </Footer>
                                            </Styles>
                                            <Columns>
                                                <dx:GridViewDataTextColumn FieldName="SelectAll" VisibleIndex="0" Caption="选择">
                                                    <Settings AllowCellMerge="False" />
                                                    <DataItemTemplate>
                                                        <asp:CheckBox ID="txtcb" runat="server" />
                                                    </DataItemTemplate>
                                                </dx:GridViewDataTextColumn>
                                            </Columns>
                                        </dx:ASPxGridView>

                                      </ContentTemplate></asp:UpdatePanel>
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
                                        <input id="comment" type="text" placeholder="请在此处输入处理意见" class="form-control" onchange="setComment(this.value)" /></td>
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

