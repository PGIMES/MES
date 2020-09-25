<%@ Page Title="文件收发申请单" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Doc_Apply.aspx.cs" Inherits="Forms_Document_Doc_Apply" %>

<%@ Register assembly="DevExpress.Web.v17.2, Version=17.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web" tagprefix="dx" %>

<asp:Content ID="HeaderContent" ContentPlaceHolderID="HeadContent" Runat="Server">
    <%--<link href="/Content/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <script src="/Content/js/jquery.min.js" type="text/javascript"></script>
     <script src="/Content/js/bootstrap.min.js" type="text/javascript"></script>
    <script src="/Content/js/layer/layer.js" type="text/javascript"></script>
    <script src="/Content/js/plugins/layer/laydate/laydate.js"type="text/javascript"></script>
    <link href="/Content/css/custom.css?t=20190516" rel="stylesheet" />
    --%>
    <link href="../../Content/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <script src="../../Content/js/jquery.min.js" type="text/javascript"></script>
    <script src="../../Content/js/bootstrap.min.js" type="text/javascript"></script>
    <script src="../../Content/js/layer/layer.js" type="text/javascript"></script>
    <script src="../../Content/js/plugins/layer/laydate/laydate.js"type="text/javascript"></script>
    <link href="/Content/css/custom.css?t=20190516" rel="stylesheet" />
    <script src="../../Content/js/bootstrap-select.min.js" type="text/javascript"></script>
    <link href="../../Content/js/bootstrap-select.min.css" rel="stylesheet"  type="text/css" />
  <script type="text/javascript">
    var StepID = '<%=StepID%>';
  function getQueryString(name) {
            var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)", "i");
            var r = decodeURI(window.location.search).substr(1).match(reg);
            if (r != null) return unescape(r[2]); return null;
        }
 
        var stepid = getQueryString("stepid");
        var state = getQueryString("state");

        $(document).ready(function () {
            $("#mestitle").html("【文件收发申请单】");//<a href='/userguide/TGuide.pps' target='_blank' class='h5' style='color:red'>使用说明</a>

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

              

           
            if (state=='edit') {
            
//               $("input[id*='type']").removeAttr("disabled").removeClass("lineread").addClass("linewrite");
                //2019.10.30注释，修改的时候可以修改
                //$("#zxXX i[id*=bzx_part_i]").removeClass("i_show").addClass("i_hidden");
            
                 $("input[id*=btnadd]").hide();

                    if($("#<%=ip_filelist.ClientID%>").val()!=""){
                    var s=$("#<%=ip_filelist.ClientID%>").val().split(';');
                    for(var i=0;i<s.length;i++){
                        uploadedFiles.push(s[i]);
                        bind_table(s[i].split(','));
                    }
                }

            }
        });

        //提出自定流程 JS 
        function setComment(val) {
            $('#comment', parent.document).val(val);
        }


        

        //设定表字段状态（可编辑性）
        var tabName="PGI_File_Transceiver_Main_Form";//表名
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
        var tabName2="PGI_File_Transceiver_Dtl_Form";//表名
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
            var textSeparator = ";";
            function updateText() {
                var selectedItems = checkListBox.GetSelectedItems();
                checkComboBox.SetText(getSelectedItemsText(selectedItems));
            }
            function synchronizeListBoxValues(dropDown, args) {
                checkListBox.UnselectAll();
                var texts = dropDown.GetText().split(textSeparator);
                var values = getValuesByTexts(texts);
                checkListBox.SelectValues(values);
                updateText(); // for remove non-existing texts
            }
            function getSelectedItemsText(items) {
                var texts = [];
                for (var i = 0; i < items.length; i++)
                    texts.push(items[i].text);
                return texts.join(textSeparator);
            }
            function getValuesByTexts(texts) {
                var actualValues = [];
                var item;
                for (var i = 0; i < texts.length; i++) {
                    item = checkListBox.FindItemByText(texts[i]);
                    if (item != null)
                        actualValues.push(item.value);
                }
                return actualValues;
            }


            function updateText2() {
                var selectedItems = checkListBox2.GetSelectedItems();
                checkComboBox2.SetText(getSelectedItemsText(selectedItems));
            }
            function synchronizeListBoxValues2(dropDown, args) {
                checkListBox2.UnselectAll();
                var texts = dropDown.GetText().split(textSeparator);
                var values = getValuesByTexts(texts);
                checkListBox2.SelectValues(values);
                updateText2(); // for remove non-existing texts
            }

//            function updateText3() {
//                var selectedItems = checkListBox3.GetSelectedItems();
//                checkComboBox3.SetText(getSelectedItemsText(selectedItems));
//            }
//            function synchronizeListBoxValues3(dropDown, args) {
//                checkListBox3.UnselectAll();
//                var texts = dropDown.GetText().split(textSeparator);
//                var values = getValuesByTexts(texts);
//                checkListBox3.SelectValues(values);
//                updateText3(); // for remove non-existing texts
//            }






    </script>



      <script type="text/javascript">
          function Get_ApplyId() {
              var url = "/select/select_ApplyId.aspx?para=travel";

              layer.open({
                  title: '申请人选择',
                  type: 2,
                  area: ['700px', '450px'],
                  fixed: false, //不固定
                  maxmin: true,
                  content: url
              });
          }

          function setvalue_ApplyId(workcode, lastname, ITEMVALUE, dept_name, domain, gc, jobtitlename, telephone, car) {

              $("#DQXX input[id*='ApplyId']").val(workcode);
              $("#DQXX input[id*='ApplyName']").val(lastname);
              $("#DQXX input[id*='ApplyTelephone']").val(telephone);
              $("#DQXX input[id*='ApplyDeptName']").val(dept_name);

          }

          //选择物料号
          function openwind() {
              var ctrl0 = "part";
              var ctrl1 = "Pn";
             // var ctrl2 = "make_factory";
             var _domain = "";
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
                  content: '/forms/open/select.aspx?windowid=doc_pgino&domain=' + _domain + '&changekey=' + ctrl0 + '&ctrl0=' + ctrl0 + '&ctrl1=' + ctrl1,
                  end: function (e) {

                  }
              });


          }


                 


          //选择文件类型
          function Get_FileType(vi) {
              var type = $("input[id*='type']").val();
              var File_lb = $("input[id*='File_lb']").val();
              if (type == "") {
                  alert("申请类别不可为空");
                  return;
              }
              var url = "/select/select_File_Type.aspx?file_lb="+File_lb+"&vi=" + vi;

              layer.open({
                  title: '选择文件类型',
                  type: 2,
                  area: ['1000px', '500px'],
                  fixed: false, //不固定
                  maxmin: true,
                  content: url
              });
          }
          
          function setvalue_FileType(vi,FileType_v) {

                  var file_type = eval('FileType' + vi); file_type.SetText(FileType_v);
                 
              }


              //选择多个部门
              function Get_multi_Dept(vi) {
                  var url = "Doc_Multi_Dept.aspx?vi=" + vi;
                  layer.open({
                      title: '选择文件类型',
                      type: 2,
                      area: ['1000px', '500px'],
                      fixed: false, //不固定
                      maxmin: true,
                      content: url
                  });
              }

              function setvalue_dept(vi, tz_dept_v) {
                  var tz_dept = eval('tz_dept' + vi); tz_dept.SetText(tz_dept_v);
              }

          function con_sure() {
              if (gv.GetSelectedRowCount() <= 0) { layer.alert("请选择要删除的记录!"); return false; }
              //询问框
              return confirm('确认要删除吗？');
          }

//          function Get_PlanAttendant() {
//              var File_language = eval('File_language' + vi);
//              var url = "/select/select_language.aspx?PA=" + $("#input[id*='File_language']").val();

//              layer.open({
//                  title: '语言选择',
//                  type: 2,
//                  area: ['450px', '550px'],
//                  fixed: false, //不固定
//                  maxmin: true,
//                  content: url
//              });
//          }


          function Get_Filelanguage(vi) {

              var url = "/select/select_language.aspx?vi=" + vi ;

              layer.open({
                  title: '语言选择',
                  type: 2,
                  area: ['300px', '350px'],
                  fixed: false, //不固定
                  maxmin: true,
                  content: url
              });
          }
          function setvalue_PlanAttendant(vindex,values) {
              var str = "";
            //  alert(values);
              var file_language = eval('File_Language' + vindex); file_language.SetText(values);
          }


          function GetVerno(vi) {

              var type = $("input[id*='type']").val();
              var File_lb = $("input[id*='File_lb']").val();
              if (type == "") {
                  alert("申请类别不可为空");
                  return;
              }
              var FileType = eval('FileType' + vi);
              var oldpath = eval('File_Path_Orig' + vi);
              var oldname = eval('File_Name_Orig' + vi);
              var FileName = eval('FileName' + vi);
              var Verno = eval('Verno' + vi);
              FileType_Value = $.trim(FileType.GetValue());
              FileName_Value = $.trim(FileName.GetValue());
   

              FileType.SetText(FileType_Value);
              FileName.SetText(FileName_Value);

              if (FileType == "") {
                  alert("文件类型不可为空");
              }
              
              $.ajax({
                  type: "post",
                  url: "Doc_Apply.aspx/GetVerno_ByFile",
                  data: "{'file_type':'" + FileType_Value + "','file_name':'" + FileName_Value + "'}",
                  contentType: "application/json; charset=utf-8",
                  dataType: "json",
                  async: false, //默认是true，异步；false为同步，此方法执行完在执行下面代码
                  success: function (data) {//返回的数据用data.d获取内容// 
                      var obj = eval(data.d);
                      var ver_no = obj[0].ver_no;
                      var old_path = obj[0].oldpath;
                      var old_name = obj[0].oldname;
                      Verno.SetText(ver_no);
                      oldpath.SetText(old_path);
                      oldname.SetText(old_name);
                      eval('Verno' + vi).SetEnabled(false);
                      eval('oldname' + vi).SetEnabled(false);
                      eval('oldpath' + vi).SetEnabled(false);
                      
                  },
                  error: function (err) {
                      layer.alert(err);
                  }
              });

          }
    </script>

     <script type="text/javascript">
         var uploadedFiles = [];
         function onFileUploadComplete(s, e) {
             if (e.callbackData) {
                 var fileData = e.callbackData.split('|'); uploadedFiles.push(fileData); $("#<%=ip_filelist.ClientID%>").val(uploadedFiles.join(";"));
                 bind_table(fileData);
             }
         }

         function bind_table(fileData) {
             var fileName = fileData[0],
                 fileUrl = fileData[1],
                 fileSize = fileData[2];

             var eqno = uploadedFiles.length - 1;

             var tbody_tr = '<tr id="tr_' + eqno + '"><td Width="400px"><a href="' + fileUrl + '" target="_blank">' + fileName + '</a></td>'
                    + '<td Width="60px">' + fileSize + '</td>'
                    + '<td><span style="color:blue;cursor:pointer" id="tbl_delde" onclick ="del_data(tr_' + eqno + ',' + eqno + ')" >删除</span></td>'
                    + '</tr>';

             $('#tbl_filelist').append(tbody_tr);
         }


         function del_data(a, eno) {
             $(a).remove();
             uploadedFiles[eno] = null;
             $("#<%=ip_filelist.ClientID%>").val(uploadedFiles.join(";"));
         }
        
    </script>



      <script type="text/javascript">
    //    alert($('#<%= ASPxDropDownEdit1.ClientID %>').val();)
        function validate(action){
            var flag=true;var msg="";
             var applyid=$("#DQXX input[id*='ApplyId']").val();
           //  alert($("#<%=ip_filelist.ClientID%>").val());
            <%=ValidScript%>
            if($("#DQXX input[id*='ApplyId']").val()=="" || $("#DQXX input[id*='ApplyName']").val()==""){
                msg+="【申请人】不可为空.<br />";
            }
            
            if($("input[id*='type']").val()==""){
                msg+="【申请类别】不可为空.<br />";
            }

            if($("input[id*='ASPxDropDownEdit1']").val()==""){
                msg+="【发放/通知的部门】不可为空.<br />";
            }

             if($("[id$=gv] input[id*=FileType]").length==0){
                    msg+="【文件明细】不可为空.<br />";
                }
            else {
            $("[id$=gv] tr[class*=DataRow]").each(function (index, item) {     
                   var file_type=eval('FileType' + index);
                    var filenumber=eval('File_Number' + index);
                    var file_name=eval('FileName' + index);
                    var path_old=eval('File_Path_Orig' + index);
                    if(file_type.GetText()==""){
                        msg+="【文件明细】的【文件类型】不可为空.<br />";
                    }
                    if(filenumber=="")
                   {
                        msg+="【文件明细】的【文件编号】不可为空.<br />";
                    }

                     if(file_name.GetText()==""){
                        msg+="【文件明细】的【文件名称】不可为空.<br />";
                    }
                    if($("input[id*='type']").val()=="版本更新")
                    {
                          if(path_old.GetText()==""){
                            msg+="【文件明细】的【原文件路径】不可为空.<br />";
                        }

                         if(name_old.GetText()==""){
                            msg+="【文件明细】的【原文件名称】不可为空.<br />";
                        }
                    }

             });
            }
//              if($("#comment").val()==""){
//                msg+="【处理意见】请填写";         
//                }

    
            
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
             $.ajax({
            type: "post",
            url: "Doc_Apply.aspx/CheckData",
            data: "{'applyid':'" + applyid + "'}",
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
                <div class="panel-headings" data-toggle="collapse" data-target="#WJXX">
                    <strong>文件信息</strong>
                </div>
                <div class="panel-body" id="WJXX">
                    <div class="col-xs-12 col-sm-12  col-md-12 col-lg-12" style="width:1200px;">
                        <table style="width: 100%; font-size: 12px;" border="0" >
                            <tr>
                              <td><font color="red">&nbsp;</font>申请工厂</td>
                                <td>
                                 <%--<label class="radio-inline">
                                    <input id="domain" type="radio" name="domain" value="200" runat="server"   checked />昆山工厂  
                                </label>--%>
                                    <asp:RadioButtonList ID="domain" runat="server">
                                        <asp:ListItem Selected>昆山工厂 </asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>    
                                  <td><font color="red">&nbsp;</font>申请类别</td>
                                <td>
                                     <dx:ASPxComboBox ID="type" runat="server" 
                                         ValueType="System.String" CssClass="linewrite" 
                                         Width="160px"  Height="27px" 
                                     BackColor="#FDF7D9" ForeColor="#31708f"     
                                         ClientInstanceName="ver_c" >
                                        <DisabledStyle CssClass="lineread"  ForeColor="#31708f" BackColor="#FFFFFF"></DisabledStyle>
                                        <ClientSideEvents SelectedIndexChanged="function(s, e) {  gv.PerformCallback();}"  />  
                                    </dx:ASPxComboBox>
                                </td> 
                                
                                  <td><font color="red">&nbsp;</font>文件类别</td>
                                <td>
                                     <dx:ASPxComboBox ID="File_lb" runat="server" CssClass="linewrite" 
                                         Width="160px"  Height="27px" 
                                     BackColor="#FDF7D9" ForeColor="#31708F"     
                                         ClientInstanceName="File_lb"  >
                                         <Items>
                                             <dx:ListEditItem Text="内部发文" Value="内部发文" />
                                             <dx:ListEditItem Selected="True" Text="外来文件" Value="外来文件" />
                                         </Items>
                                        <DisabledStyle CssClass="lineread"  ForeColor="#31708f" BackColor="#FFFFFF"></DisabledStyle>
                                        <ClientSideEvents SelectedIndexChanged="function(s, e) {  gv.PerformCallback();}"  />  
                                    </dx:ASPxComboBox>
                                </td> 
                                </tr><tr>    
                                <td style="width:100px;"><font color="red">*</font>项目号</td>
                                <td>
                                    <div class="form-inline">
                                        <asp:TextBox ID="part" runat="server" class="lineread"  ReadOnly="true" Width="160px" />
                                        <i id="part_i" class="fa fa-search <% =ViewState["ApplyId_i"].ToString() == "Y" ? "i_hidden" : "i_show" %>" 
                                            onclick="openwind()"></i>

                                    </div>
                                </td>
                                <td>
                                    <font color="red">&nbsp;</font>零件号
                                </td>
                                <td>
                                    <asp:TextBox ID="Pn" runat="server" class="lineread" ReadOnly="True"
                                        Width="160px" />
                                </td>
                     
                            </tr>
                            <tr>
                                <td>
                                    <font color="red">&nbsp;</font>特别说明
                                </td>
                                <td colspan="8">
                                    <asp:TextBox ID="Remark" runat="server" TextMode="MultiLine" Rows="2"
                                        class="form-control"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
        </div>

          <div class="row row-container">
            <div class="panel panel-infos">
                <div class="panel-headings" data-toggle="collapse" data-target="#ffbm">
                    <strong>发放部门/人员</strong>
                </div>
                <div class="panel-body" id="#ffbm">
                    <div class="col-xs-12 col-sm-12  col-md-12 col-lg-12" style="width:1200px;">
                        <table style="width: 100%; font-size: 12px;" border="0" >

                            <tr>
                                <td width="200px">
                                    <font color="red">*</font>选择通知部门(电子)
                                </td>
                                <td >
                                     <dx:ASPxDropDownEdit ClientInstanceName="checkComboBox" 
                                         ID="ASPxDropDownEdit1" Width="500px" runat="server" 
                                         AnimationType="None" CssClass="form-control input-s-md " 
                                         ClientEnabled="True" >
                                        <DropDownWindowStyle BackColor="#EDEDED" />
                                        <DropDownWindowTemplate>
                                            <dx:ASPxListBox Width="100%" ID="listBox" ClientInstanceName="checkListBox" SelectionMode="CheckColumn"
                                                runat="server" Height="200" EnableSelectAll="true">
                                                <FilteringSettings ShowSearchUI="true"/>
                                                <Border BorderStyle="None" />
                                                <BorderBottom BorderStyle="Solid" BorderWidth="1px" BorderColor="#DCDCDC" />
                                                <Items> 
                                   
                                                </Items>
                                                <ClientSideEvents SelectedIndexChanged="updateText" />
                                            </dx:ASPxListBox>
                                            <table style="width: 100%">
                                                <tr>
                                                    <td style="padding: 4px">
                                                        <dx:ASPxButton ID="ASPxButton1" AutoPostBack="False" runat="server" Text="Close" style="float: right">
                                                            <ClientSideEvents Click="function(s, e){ checkComboBox.HideDropDown(); }" />
                                                        </dx:ASPxButton>
                                                    </td>
                                                </tr>
                                            </table>
                                        </DropDownWindowTemplate>
                                        <ClientSideEvents TextChanged="synchronizeListBoxValues" DropDown="synchronizeListBoxValues" />
                                    </dx:ASPxDropDownEdit>
                                </td>
                               <td>  <asp:TextBox ID="deliver_dept" runat="server" class="lineread" ReadOnly="True"
                                        Width="0px" /></td>
                            </tr>

                              <tr>
                                <td width="200px">
                                    <font color="red">&nbsp;</font>选择通知的人员
                                </td>
                                <td >
                                     <dx:ASPxDropDownEdit ClientInstanceName="checkComboBox2" ID="ASPxDropDownEdit2" Width="500px" runat="server" AnimationType="None" CssClass="form-control input-s-md ">
                                        <DropDownWindowStyle BackColor="#EDEDED" />
                                        <DropDownWindowTemplate>
                                            <dx:ASPxListBox Width="100%" ID="listBox2" ClientInstanceName="checkListBox2" SelectionMode="CheckColumn"
                                                runat="server" Height="200" EnableSelectAll="true">
                                                <FilteringSettings ShowSearchUI="true"/>
                                                <Border BorderStyle="None" />
                                                <BorderBottom BorderStyle="Solid" BorderWidth="1px" BorderColor="#DCDCDC" />
                                                <Items> 
                                   
                                                </Items>
                                                <ClientSideEvents SelectedIndexChanged="updateText2" />
                                            </dx:ASPxListBox>
                                            <table style="width: 100%">
                                                <tr>
                                                    <td style="padding: 4px">
                                                        <dx:ASPxButton ID="ASPxButton2" AutoPostBack="False" runat="server" Text="Close" style="float: right">
                                                            <ClientSideEvents Click="function(s, e){ checkComboBox2.HideDropDown(); }" />
                                                        </dx:ASPxButton>
                                                    </td>
                                                </tr>
                                            </table>
                                        </DropDownWindowTemplate>
                                        <ClientSideEvents TextChanged="synchronizeListBoxValues2" DropDown="synchronizeListBoxValues2" />
                                    </dx:ASPxDropDownEdit>
                                </td>
                               <td>  <asp:TextBox ID="deliver_user" runat="server" class="lineread" ReadOnly="True"
                                        Width="0px" /></td>

                                <td> <font color="red">&nbsp;</font>是否需采购会签</td>
                                <td style=" width:10%"> 
                                   <input id="Cb_caigou" type="checkbox" value="200" runat="server"  name="Cb_caigou" />
                                     <asp:TextBox ID="hq_caigou" runat="server" ToolTip="0|0" Width="40"
                                                          CssClass=" hidden" />
                                  </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
        </div>
        
     


        <div class="row row-container">
            <div class="panel panel-infos">
                <div class="panel-headings" data-toggle="collapse" data-target="#bzclXX">
                    <strong>文件明细</strong>
                </div>
                <div class="panel-body" id="bzclXX">
                    <div class="col-xs-12 col-sm-12  col-md-12 col-lg-12" style="width:1200px;">
                        <div style="padding: 2px 5px 5px 0px">                
                            
                        </div>
                        <asp:UpdatePanel runat="server" ID="p1" UpdateMode="Conditional" >
                            <ContentTemplate>
                                
                                <asp:Button ID="btnadd" runat="server" Text="新增" class="btn btn-default btn-sm"  OnClick="btnadd_Click" />
                                <asp:Button ID="btndel" runat="server" Text="删除" class="btn btn-default btn-sm"  OnClick="btndel_Click" OnClientClick="return con_sure()" />

                                 <dx:aspxgridview ID="gv" runat="server" 
                                    AutoGenerateColumns="False" KeyFieldName="numid" Theme="MetropolisBlue" 
                                     ClientInstanceName="gv"  EnableTheming="True" 
                                    OnDataBound="gv_DataBound"  OnCustomCallback="gv_type_CustomCallback"
                                    onhtmlrowcreated="gv_HtmlRowCreated">
                                      
                                    <SettingsPager PageSize="1000"></SettingsPager>
                                    <Settings ShowFooter="True" />
                                    <SettingsBehavior AllowSelectByRowClick="false" AllowDragDrop="False" AllowSort="False" />
                                    <Styles>
                                    <SelectedRow BackColor="#FDF7D9"></SelectedRow> 
                                        <Header BackColor="#31708F" Border-BorderStyle="None" 
                                            Font-Bold="True" ForeColor="White" HorizontalAlign="Left" 
                                            VerticalAlign="Top">
                                        </Header>
                                    <AlternatingRow BackColor="#f2f3f2"></AlternatingRow>  
                                        <Footer Font-Bold="True" ForeColor="Red" 
                                            HorizontalAlign="Right">
                                        </Footer>
                                    </Styles>
                                    <Columns>
                                        <dx:GridViewCommandColumn SelectAllCheckboxMode="Page" ShowClearFilterButton="true" ShowSelectCheckbox="true" Name="Sel" Width="30" VisibleIndex="0"></dx:GridViewCommandColumn>
                                        <dx:GridViewDataTextColumn  Caption="#" FieldName="numid" 
                                            Width="30px" VisibleIndex="1"></dx:GridViewDataTextColumn>         
                                                <dx:GridViewDataTextColumn Caption="文件类型" 
                                            FieldName="FileType" Width="190px" VisibleIndex="3">
                                            <Settings AllowCellMerge="False" />
                                            <DataItemTemplate>
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <dx:ASPxTextBox ID="FileType" Width="180px" runat="server" Value='<%# Eval("FileType")%>' 
                                                                ClientInstanceName='<%# "FileType"+Container.VisibleIndex.ToString() %>' Border-BorderWidth="0" ReadOnly="true">
                                                            </dx:ASPxTextBox>
                                                        </td>
                                                        <td><i id="bm_i_<%#Container.VisibleIndex.ToString() %>" 
                                                            class="fa fa-search <% =ViewState["ApplyId_i"].ToString() == "Y" ? "i_hidden" : "i_show" %>" 
                                                            onclick="Get_FileType(<%# Container.VisibleIndex %>)"></i>
                                                        </td>
                                                    </tr>
                                                </table>       
                                            </DataItemTemplate>
                                        </dx:GridViewDataTextColumn>     
                                         

                                           <dx:GridViewDataTextColumn Caption="文件编号" 
                                            FieldName="File_Number" Width="80px" VisibleIndex="4">
                                            <Settings AllowCellMerge="False"/>
                                            <DataItemTemplate>
                                                <dx:ASPxTextBox ID="File_Number" Width="80px" runat="server" Value='<%# Eval("File_Number")%>' 
                                                    ClientInstanceName='<%# "File_Number"+Container.VisibleIndex.ToString() %>'>
                                                </dx:ASPxTextBox>
                                            </DataItemTemplate>        
                                        </dx:GridViewDataTextColumn>



        
                                        <dx:GridViewDataTextColumn Caption="文件名称" 
                                            FieldName="FileName" Width="260px" VisibleIndex="5">
                                            <Settings AllowCellMerge="False"/>
                                            <DataItemTemplate>
                                                <dx:ASPxTextBox ID="FileName" Width="250px" runat="server" Value='<%# Eval("FileName")%>' 
                                                    ClientInstanceName='<%# "FileName"+Container.VisibleIndex.ToString() %>'  >
                                                </dx:ASPxTextBox>
                                            </DataItemTemplate>        
                                        </dx:GridViewDataTextColumn>
                                                 <dx:GridViewDataTextColumn Caption="文件版本号" 
                                            FieldName="Verno" Width="60px" VisibleIndex="6">
                                            <Settings AllowCellMerge="False"/>
                                            <DataItemTemplate>
                                                <dx:ASPxTextBox ID="Verno" Width="60px" runat="server" Value='<%# Eval("Verno")%>' 
                                                    ClientInstanceName='<%# "Verno"+Container.VisibleIndex.ToString() %>'>
                                                </dx:ASPxTextBox>
                                            </DataItemTemplate>        
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="原文件路径" 
                                            FieldName="File_Path_Orig" Width="200px" VisibleIndex="7">
                                            <Settings AllowCellMerge="False"/>
                                            <DataItemTemplate>
                                                 <dx:ASPxTextBox ID="File_Path_Orig" Width="180px" runat="server" Value='<%# Eval("File_Path_Orig")%>' 
                                                    ClientInstanceName='<%# "File_Path_Orig"+Container.VisibleIndex.ToString() %>'>
                                                </dx:ASPxTextBox>
                                            </DataItemTemplate>
                                        </dx:GridViewDataTextColumn> 

                                        <dx:GridViewDataTextColumn Caption="选择发放部门(纸质)" 
                                            FieldName="tz_dept" Width="190px" VisibleIndex="8">
                                            <Settings AllowCellMerge="False" />
                                            <DataItemTemplate>
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <dx:ASPxTextBox ID="tz_dept" Width="180px" runat="server" Value='<%# Eval("tz_dept")%>' 
                                                                ClientInstanceName='<%# "tz_dept"+Container.VisibleIndex.ToString() %>' Border-BorderWidth="0" ReadOnly="true">
                                                            </dx:ASPxTextBox>
                                                        </td>
                                                        <td><i id="bm_i_<%#Container.VisibleIndex.ToString() %>" 
                                                            class="fa fa-search <% =ViewState["ApplyId_i"].ToString() == "Y" ? "i_hidden" : "i_show" %>" 
                                                            onclick="Get_multi_Dept(<%# Container.VisibleIndex %>)"></i>
                                                        </td>
                                                    </tr>
                                                </table>       
                                            </DataItemTemplate>
                                        </dx:GridViewDataTextColumn>     


                                           <dx:GridViewDataTextColumn Caption="顾客版本/接收日期" 
                                            FieldName="Customer_Verno" Width="100px" VisibleIndex="9">
                                            <Settings AllowCellMerge="False"/>
                                            <DataItemTemplate>
                                                <dx:ASPxTextBox ID="Customer_Verno" Width="100px" runat="server" Value='<%# Eval("Customer_Verno")%>' 
                                                    ClientInstanceName='<%# "Customer_Verno"+Container.VisibleIndex.ToString() %>'>
                                                </dx:ASPxTextBox>
                                            </DataItemTemplate>        
                                        </dx:GridViewDataTextColumn>

                                           

                                        
                                         <dx:GridViewDataTextColumn Caption="失效日期" 
                                            FieldName="expiry_date" Width="100px" VisibleIndex="10">
                                            <Settings AllowCellMerge="False"/>
                                            <DataItemTemplate>
                                            <dx:ASPxTextBox ID="expiry_date" Width="80px" runat="server" Value='<%# Eval("expiry_date")%>' 
                                            onclick="laydate({type: 'date',format: 'YYYY/MM/DD',choose: function(dates){}});"
                                                                ClientInstanceName='<%# "expiry_date"+Container.VisibleIndex.ToString() %>' Border-BorderWidth="0" ReadOnly="true">
                                            </dx:ASPxTextBox>
                                            </DataItemTemplate>   
                                            <PropertiesTextEdit DisplayFormatString="{0:yyyy-MM-DD}"></PropertiesTextEdit>     
                                        </dx:GridViewDataTextColumn>

                               
                                        <dx:GridViewDataTextColumn Caption="文件语言" 
                                            FieldName="File_Language" Width="190px" VisibleIndex="11">
                                            <Settings AllowCellMerge="False" />
                                            <DataItemTemplate>
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <dx:ASPxTextBox ID="File_Language" Width="100px" runat="server" Value='<%# Eval("File_Language")%>' 
                                                                ClientInstanceName='<%# "File_Language"+Container.VisibleIndex.ToString() %>' Border-BorderWidth="0" ReadOnly="true">
                                                            </dx:ASPxTextBox>
                                                        </td>
                                                        <td><i id="bm_i_<%#Container.VisibleIndex.ToString() %>" 
                                                            class="fa fa-search <% =ViewState["ApplyId_i"].ToString() == "Y" ? "i_hidden" : "i_show" %>" 
                                                            onclick="Get_Filelanguage(<%# Container.VisibleIndex %>)"></i>
                                                        </td>
                                                    </tr>
                                                </table>       
                                            </DataItemTemplate>
                                        </dx:GridViewDataTextColumn>    



                                              <dx:GridViewDataTextColumn Caption="文件检索来源" 
                                            FieldName="File_Source" Width="100px" VisibleIndex="12">
                                            <Settings AllowCellMerge="False"/>
                                            <DataItemTemplate>
                                                <dx:ASPxTextBox ID="File_Source" Width="100px" runat="server" Value='<%# Eval("File_Source")%>' 
                                                    ClientInstanceName='<%# "File_Source"+Container.VisibleIndex.ToString() %>'>
                                                </dx:ASPxTextBox>
                                            </DataItemTemplate>        
                                        </dx:GridViewDataTextColumn>

                                          <dx:GridViewDataTextColumn Caption="法律法规目录" 
                                            FieldName="Law_List" Width="100px" VisibleIndex="13">
                                            <Settings AllowCellMerge="False"/>
                                            <DataItemTemplate>
                                                <dx:ASPxTextBox ID="Law_List" Width="100px" runat="server" Value='<%# Eval("Law_List")%>' 
                                                    ClientInstanceName='<%# "Law_List"+Container.VisibleIndex.ToString() %>'>
                                                </dx:ASPxTextBox>
                                            </DataItemTemplate>        
                                        </dx:GridViewDataTextColumn>

                                                   <dx:GridViewDataTextColumn Caption="发布部门" 
                                            FieldName="Law_Dept" Width="100px" VisibleIndex="14">
                                            <Settings AllowCellMerge="False"/>
                                            <DataItemTemplate>
                                                <dx:ASPxComboBox ID="Law_Dept"   runat="server" ValueType="System.String" 
                                               Value= '<%# DataBinder.Eval(Container.DataItem,"Law_Dept")%>'
                                                 CssClass="linewrite" Width="100px" Height="27px" BackColor="#FDF7D9" ForeColor="#31708f">
                                        </dx:ASPxComboBox>

                                            </DataItemTemplate>        
                                        </dx:GridViewDataTextColumn>

                                           <dx:GridViewDataTextColumn Caption="大分类" 
                                            FieldName="B_fenlei" Width="80px" VisibleIndex="15">
                                            <Settings AllowCellMerge="False"/>
                                            <DataItemTemplate>
                                                <dx:ASPxComboBox ID="B_fenlei"   runat="server" ValueType="System.String"    
                                                Value= '<%# DataBinder.Eval(Container.DataItem,"B_fenlei")%>'
                                                 CssClass="linewrite" Width="80px" Height="27px" BackColor="#FDF7D9" ForeColor="#31708f">
                                        </dx:ASPxComboBox>
                                        </DataItemTemplate>        
                                        </dx:GridViewDataTextColumn>

                                        <dx:GridViewDataTextColumn Caption="中分类" 
                                            FieldName="M_fenlei" Width="80px" VisibleIndex="16">
                                            <Settings AllowCellMerge="False"/>
                                            <DataItemTemplate>
                                                <dx:ASPxComboBox ID="M_fenlei"   runat="server" ValueType="System.String"
                                                 Value= '<%# DataBinder.Eval(Container.DataItem,"M_fenlei")%>'
                                                 CssClass="linewrite" Width="80px" Height="27px" BackColor="#FDF7D9" ForeColor="#31708f">
                                        </dx:ASPxComboBox>
                                            </DataItemTemplate>        
                                        </dx:GridViewDataTextColumn>

                                        <dx:GridViewDataTextColumn Caption="小分类" 
                                            FieldName="S_fenlei" Width="80px" VisibleIndex="17">
                                            <Settings AllowCellMerge="False"/>
                                            <DataItemTemplate>
                                                <dx:ASPxComboBox ID="S_fenlei"   runat="server" ValueType="System.String"
                                                 Value= '<%# DataBinder.Eval(Container.DataItem,"S_fenlei")%>'
                                                 CssClass="linewrite" Width="80px" Height="27px" BackColor="#FDF7D9" ForeColor="#31708f">
                                        </dx:ASPxComboBox>

                                            </DataItemTemplate>        
                                        </dx:GridViewDataTextColumn>


                                        <dx:GridViewDataTextColumn Caption="发布日期" 
                                            FieldName="deliver_date" Width="100px" VisibleIndex="18">
                                            <Settings AllowCellMerge="False"/>
                                            <DataItemTemplate>
                                            <dx:ASPxTextBox ID="deliver_date" Width="80px" runat="server" Value='<%#Eval("deliver_date","{0:yyyy-MM-dd}")%>' 
                                          
                                            onclick="laydate({type: 'date',format: 'YYYY/MM/DD',choose: function(dates){}});"
                                                                ClientInstanceName='<%# "deliver_date"+Container.VisibleIndex.ToString() %>' Border-BorderWidth="0" ReadOnly="true">
                                            </dx:ASPxTextBox>
                                            </DataItemTemplate>          
                                        </dx:GridViewDataTextColumn>


                                        <dx:GridViewDataTextColumn Caption="实施日期" 
                                            FieldName="material_date" Width="100px" VisibleIndex="19">
                                            <Settings AllowCellMerge="False"/>
                                            <DataItemTemplate>
                                            <dx:ASPxTextBox ID="material_date" Width="80px" runat="server" Value='<%#Eval("material_date","{0:yyyy-MM-dd}")%>' 
                                            onclick="laydate({type: 'date',format: 'YYYY/MM/DD',choose: function(dates){}});"
                                                                ClientInstanceName='<%# "material_date"+Container.VisibleIndex.ToString() %>' Border-BorderWidth="0" ReadOnly="true">
                                            </dx:ASPxTextBox>
                                            </DataItemTemplate>        
                                        </dx:GridViewDataTextColumn>

                                        <dx:GridViewDataTextColumn Caption="收集日期" 
                                            FieldName="collect_date" Width="100px" VisibleIndex="20">
                                            <Settings AllowCellMerge="False"/>
                                            <DataItemTemplate>
                                            <dx:ASPxTextBox ID="collect_date" Width="80px" runat="server" Value='<%#Eval("collect_date","{0:yyyy-MM-dd}")%>' 
                                            onclick="laydate({type: 'date',format: 'YYYY/MM/DD',choose: function(dates){}});"
                                                                ClientInstanceName='<%# "collect_date"+Container.VisibleIndex.ToString() %>' Border-BorderWidth="0" ReadOnly="true">
                                            </dx:ASPxTextBox>
                                            </DataItemTemplate>        
                                        </dx:GridViewDataTextColumn>

                                           <dx:GridViewDataTextColumn Caption="备注" FieldName="bz" 
                                            Width="100px" VisibleIndex="21">
                                            <Settings AllowCellMerge="False"/>
                                            <DataItemTemplate>
                                                <dx:ASPxTextBox ID="bz" Width="100px" runat="server" Value='<%# Eval("bz")%>' 
                                                    ClientInstanceName='<%# "bz"+Container.VisibleIndex.ToString() %>'>
                                                </dx:ASPxTextBox>
                                            </DataItemTemplate>        
                                        </dx:GridViewDataTextColumn>

                                        <dx:GridViewDataTextColumn Caption="浏览" Width="100px" 
                                            VisibleIndex="22">
                                            <Settings AllowCellMerge="False"/>
                                            <DataItemTemplate>
                                                <asp:HyperLink ID="hl_url" runat="server" Target="_blank" Text='<%#Eval("FileName") %>'>HyperLink</asp:HyperLink>
                                            </DataItemTemplate>        
                                        </dx:GridViewDataTextColumn>
                                     
                                        <dx:GridViewDataTextColumn FieldName="id" Width="0px" 
                                            VisibleIndex="2">
                                            <HeaderStyle CssClass="hidden" />
                                            <CellStyle CssClass="hidden"></CellStyle>
                                            <FooterCellStyle CssClass="hidden"></FooterCellStyle>
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="formno" Width="0px" 
                                            VisibleIndex="23">
                                            <HeaderStyle CssClass="hidden" />
                                            <CellStyle CssClass="hidden"></CellStyle>
                                            <FooterCellStyle CssClass="hidden"></FooterCellStyle>
                                        </dx:GridViewDataTextColumn>
                                             <dx:GridViewDataTextColumn FieldName="pgino" 
                                            Width="0px" VisibleIndex="24">
                                            <HeaderStyle CssClass="hidden" />
                                            <CellStyle CssClass="hidden"></CellStyle>
                                            <FooterCellStyle CssClass="hidden"></FooterCellStyle>
                                        </dx:GridViewDataTextColumn>
                                           <dx:GridViewDataTextColumn FieldName="File_Serialno" 
                                            Width="0px" VisibleIndex="25">
                                            <HeaderStyle CssClass="hidden" />
                                            <CellStyle CssClass="hidden"></CellStyle>
                                            <FooterCellStyle CssClass="hidden"></FooterCellStyle>
                                        </dx:GridViewDataTextColumn>
                                   

                                    </Columns>       
                                                                        
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

               <div class="row row-container"  >
            <div class="panel panel-infos" id="upload_fj" runat="server" style=" display:none">
                <div class="panel-headings" data-toggle="collapse" data-target="#fjXX">
                    <strong>附件信息</strong>
                </div>
           <div class="panel-body" id="fjXX" >
                            <div class="col-xs-12 col-sm-12  col-md-12 col-lg-12" style="width: 1000px;">
                                <div style="margin-top: 5px;">
                                    <dx:ASPxUploadControl ID="uploadcontrol" runat="server" Width="500px"
                                        BrowseButton-Text="浏览" Visible="true" ClientInstanceName="UploadControl"
                                        ShowAddRemoveButtons="True" RemoveButton-Text="删除" UploadMode="Advanced"
                                        AutoStartUpload="true" ShowUploadButton="false" ShowProgressPanel="true"
                                        OnFileUploadComplete="uploadcontrol_FileUploadComplete">
                                        <AdvancedModeSettings EnableDragAndDrop="True" EnableFileList="True"
                                            EnableMultiSelect="True">
                                        </AdvancedModeSettings>
                                        <ClientSideEvents FileUploadComplete="onFileUploadComplete" />
                                    </dx:ASPxUploadControl>
                                    <input type="hidden" id="ip_filelist" name="ip_filelist" runat="server" />
                                    <table id="tbl_filelist" width="500px">
                                    </table>
                                    <asp:UpdatePanel runat="server" ID="p11" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <textarea id="ip_filelist_db" name="ip_filelist" runat="server"
                                                cols="200" rows="2" visible="false"></textarea>
                                            <asp:Table ID="tab1" Width="500px" runat="server">
                                            </asp:Table>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
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


