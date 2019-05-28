<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Jiaju_LY.aspx.cs" Inherits="JiaJu_Jiaju_LY" %>

<%@ Register Assembly="DevExpress.Web.v17.2, Version=17.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <script src="../Content/js/jquery.min.js" type="text/javascript"></script>
    <script src="../Content/js/plugins/layer/layer.min.js" type="text/javascript"></script>
    <script src="../Content/js/jquery-ui-1.10.4.min.js" type="text/javascript"></script>
    
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#mestitle").text("【夹具出入库】");
            var SelectdValue = $(':radio[id*=Rd_lytype]:checked').val();
            if(SelectdValue=="0")
            {
                $("div[id*=divLY]").show();
                 $("div[id*=divRK]").hide();
              }
               else   if(SelectdValue=="1")
                {
                 $("div[id*=divLY]").hide();
                 $("div[id*=divRK]").show();
                }
                else
                {
                 $("div[id*=divRK]").show();
                 $("div[id*=divLY]").show();
                }

            // alert(SelectdValue);
             $("input[id*='MainContent_txt_lyjiajuno']").attr("readonly","true");
             $("input[id*='MainContent_txt_lypn']").attr("readonly","true");
             $("input[id*='MainContent_txt_lyname']").attr("readonly","true");
//             $("input[id*='MainContent_txt_rkpn']").attr("readonly","true");
//             $("input[id*='MainContent_txt_rkname']").attr("readonly","true");
//             $("input[id*='MainContent_txt_rkjiajuno']").attr("readonly","true");
            //人员变更时
            $("input[id*='MainContent_txt_lyuid']").change(function () {
                var uid = $("input[id*='MainContent_txt_lyuid']").val();
                 bindUid( $("input[id*='MainContent_txt_lyuid']"),uid);
             
            })

             $("input[id*='MainContent_txt_rkuid']").change(function () {
                var uid = $("input[id*='MainContent_txt_rkuid']").val();
                bindUid( $("input[id*='MainContent_txt_rkuid']"),uid);
              
            })

     
    

             $("img").bind("click",function(){
               $("input[id*='btn_Start']").removeClass("disabled");
               
            })

            $("Image1").bind("click",function(){
               $("input[id*='btn_Start']").removeClass("disabled");
               
            })


            //夹具号变更时
               $("select[id*='MainContent_Drop_Jiajuno']").change(function () {
                var jiajuno = $("select[id*='MainContent_Drop_Jiajuno']").val();
               SelJiaju(jiajuno);
            })


                $("input[id*='btn_Start']").click(function () {
                $("input[id*='btn_Start']").val("处理中...");
             //   $("input[id*='btn_Start']").prop("disabled", true);
            })


//       //选择类型时
              $("input[id*='MainContent_Rd_lytype']").click(function () {
                 var uid='<%=uid %>';
                var type = $(':radio[id*=Rd_lytype]:checked').val();
                if(type=="1")
                {
                 $("div[id*=divLY]").hide();
                 $("div[id*=divRK]").show();
                 bindDrop_jiaju(uid);
                }
                else   if(type=="0")
                {
                 $("div[id*=divLY]").show();
                 $("div[id*=divRK]").hide();
                }
                else
                {
                 $("div[id*=divRK]").show();
                 $("div[id*=divLY]").show();
                 bindDrop_jiaju(uid);
                }
                $("input[type=text]").val("");
                $("select").val("");
               
            })
        

        })//endready


                function bindDrop_jiaju(uid) {
            $.ajax({
                type: "Post", async: false,
                url: "Jiaju_LY.aspx/GetRK_Jiaju",
                //方法传参的写法一定要对，str为形参的名字,str2为第二个形参的名字
                //P1:wlh P2： 
                data: "{'uid':'" + uid + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {//返回的数据用data.d获取内容//   
                 $("#MainContent_Drop_Jiajuno").empty();                     
                   $.each(eval(data.d), function (i, item) {
                       var option = $("<option>").val(item.value).text(item.text);
                            $("#MainContent_Drop_Jiajuno").append(option);
                    })
                            
                }
            });

        }



          function bindUid(objs,uid) {
            $.ajax({
                type: "post",
                url: "Jiaju_LY.aspx/GetUid",
                data: "{'uid':'" + uid + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false, //默认是true，异步；false为同步，此方法执行完在执行下面代码
                success: function (data) {
                    var obj = eval(data.d);
                    if (obj[0].UserId != "") {
                        objs.val(obj[0].UserId);
                    }
                    else {
                        alert("请填写正确的员工工号!");
                       objs.val("");
                       objs.focus();
                    }
                }

            });
        }
 
      function SelJiaju(jiajuno) {
           $.ajax({
                type: "post",
                url: "Jiaju_LY.aspx/GetPN",
                data: "{'jiajuno':'" + jiajuno + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false, //默认是true，异步；false为同步，此方法执行完在执行下面代码
                success: function (data) {
                    var obj = eval(data.d);
                    if (obj[0].pn != "") {
                        $("input[id='MainContent_txt_rkpn']").val(obj[0].pn);
                        $("input[id='MainContent_txt_rkname']").val(obj[0].pnname);
                    }
                    
                }

            });
        }

   
   function validate(action){
           var flag=true;var msg="";
            <%=ValidScript%>
             var domain='<%=domain %>';
           
            if($(':radio[id*=Rd_lytype]:checked').val()=="0")
            {
                  var jiajuno=$("#MainContent_txt_lyjiajuno").val();
                  var lytype=$("#MainContent_Drop_lytype").val();
                  var lyuid=$("#MainContent_txt_lyuid").val();
                 var lysb=$("#MainContent_txt_lysbno").val();
                 if(jiajuno==""){msg+="夹具号不可为空.<br />";}
                 if(lytype==""){msg+="领用类型不可为空.<br />";}
                 if(lyuid==""){msg+="领用人不可为空.<br />";}
                  if(lysb==""){msg+="领用机台号不可为空.<br />";}
                 if(lysb.substring(0,1)!="M" ||  lysb.length!=5)
                  {msg+="领用机台号不正确.<br />";}
              }
              else if ($(':radio[id*=Rd_lytype]:checked').val()=="1")//入库
              {
                alert(domain);
                  var selrk_jiaju=$("#MainContent_Drop_Jiajuno").val();
                  var rktype=$("#MainContent_Drop_rktype").val();
                  var rkloc=$("#MainContent_txt_rkloc").val().toUpperCase();
                  var rkuid=$("#MainContent_txt_rkuid").val();
                  var rkpn=$("#MainContent_txt_rkpn").val();
                  var rkname=$("#MainContent_txt_rkname").val();
                  if(selrk_jiaju==""){msg+="入库夹具号不可为空.<br />";}
                  if(rkpn==""){msg+="入库零件号不可为空.<br />";}
                  if(rkname==""){msg+="入库零件名称不可为空.<br />";}
                  if(rktype==""){msg+="入库类型不可为空.<br />";}
                  if(rkloc==""){msg+="入库库位不可为空.<br />";}
                  if(rkuid==""){msg+="入库人不可为空.<br />";}
                  if( (domain=="200" && rkloc.substring(0,2)!="KA") ||  rkloc.length!=5  || (domain=="100" && rkloc.substring(0,1)!="H")   )
                  {msg+= "入库库位不正确,需以KA或H打头,长度5码.<br />";}
                 
              }
              else
              {
              var jiajuno=$("#MainContent_txt_lyjiajuno").val();
                  var lytype=$("#MainContent_Drop_lytype").val();
                  var lyuid=$("#MainContent_txt_lyuid").val();
                  var lysb=$("#MainContent_txt_lysbno").val();
                 if(jiajuno==""){msg+="领用夹具号不可为空.<br />";}
                 if(lytype==""){msg+="领用类型不可为空.<br />";}
                 if(lyuid==""){msg+="领用人不可为空.<br />";}
                  if(lysb==""){msg+="领用机台号不可为空.<br />";}
                  var selrk_jiaju=$("#MainContent_Drop_Jiajuno").val();
                  var rktype=$("#MainContent_Drop_rktype").val();
                  var rkloc=$("#MainContent_txt_rkloc").val().toUpperCase();
                  var rkuid=$("#MainContent_txt_rkuid").val();
                  if(selrk_jiaju==""){msg+="入库夹具号不可为空.<br />";}
                  if(rktype==""){msg+="入库类型不可为空.<br />";}
                  if(rkloc==""){msg+="入库库位不可为空.<br />";}
                  if(rkuid==""){msg+="入库人不可为空.<br />";}
                  if(lysb.substring(0,1)!="M" ||  lysb.length!=5)
                  {msg+="领用机台号不正确.<br />";}
                  //if(rkloc.substring(0,2)!="KA" ||  rkloc.length!=5)
                    if( (domain=="200" && rkloc.substring(0,2)!="KA") ||  rkloc.length!=5  || (domain=="100" && rkloc.substring(0,1)!="H")   )
                  {msg+="入库库位不正确,需以KA或H打头,长度5码.<br />";}
              }
              if(msg!=""){  
                flag=false;
                layer.alert(msg);
                return flag; }

              
            }


          //取夹具号
        function GetJiaju_no(type) {
         var domain='<%=domain %>';
            var url = "../select/select_jiajuno.aspx?type="+type+"&domain="+domain+"";

            layer.open({
                title: '夹具号选择',
                type: 2,
                area: ['1000px', '600px'],
                fixed: false, //不固定
                maxmin: true,
                content: url
            });
        }

        function setvalue_product(lsjiajuno, lspn, lspnname,lstype) {
        if(lstype=="LY")
        {
            $("input[id*='txt_lyjiajuno']").val(lsjiajuno);
            $("input[id*='txt_lypn']").val(lspn);
            $("input[id*='txt_lyname']").val(lspnname);
         }
         else
         {
          $("input[id*='txt_rkjiajuno']").val(lsjiajuno);
          $("input[id*='txt_rkpn']").val(lspn);
          $("input[id*='txt_rkname']").val(lspnname);
         }

        }




    </script>
    <div class="row" style="margin: 0px 2px 1px 2px">
        <div class="col-sm-12  col-md-10">
            <div class="panel panel-info">
                <div class="panel-heading">
                    <strong>夹具出入库类型</strong>
                </div>
                <div class="panel-body">
                    <div class="col-sm-12 col-md-10">
                        <table style="width: 100%">
                            <tr>
                                <td  style=" width:200px">
                                    出入库类型：
                                </td>
                                <td>
                             
                                    <asp:RadioButtonList ID="Rd_lytype" runat="server"   
                                        RepeatDirection="Horizontal"
                                        
                      
                                        Font-Size="12pt" Font-Names="仿宋" 
                                        
                                       >
                                        <asp:ListItem Value="0" Selected="True" >领用</asp:ListItem>
                                        <asp:ListItem Value="1" >入库</asp:ListItem>
                                        <asp:ListItem Value="2">领用&amp;入库</asp:ListItem>
                                    </asp:RadioButtonList>
                             
                                </td>
                               
                            </tr>
                            </table>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="row" style="margin: 0px 2px 1px 2px">
        <div class="row">
            <div class="col-sm-12">
                <div class="col-sm-6 col-md-5" id="divLY" runat ="server" >
                    <div class="panel panel-info">
                        <div class="panel-heading">
                            领用</div>
                        <div class="panel-body">
                            <div class="form-group">
                                <span class="col-sm-4">夹具号：</span>
                                <div class="col-sm-8">
                                    
                                     <asp:TextBox ID="txt_lyjiajuno" class="form-control" runat="server" ></asp:TextBox>
                                        <asp:Image ID="img" runat="server" ImageUrl='~/Images/fdj.gif' Width="14"  Height="14" onmouseover="this.style.cursor='hand'"   onclick=" GetJiaju_no('LY')"/>
                                </div>
                            </div>
                             <div class="form-group">
                                <span class="col-sm-4">零件号：</span>
                                <div class="col-sm-8">
                                    <asp:TextBox ID="txt_lypn" class="form-control" runat="server"  ></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group">
                                <span class="col-sm-4">零件名称：</span>
                                <div class="col-sm-8">
                                    <asp:TextBox ID="txt_lyname" class="form-control" runat="server"  ></asp:TextBox>
                                </div>
                            </div>
                             <div class="form-group">
                                <span class="col-sm-4">领用类型：</span>
                                <div class="col-sm-8">
                                    <asp:DropDownList ID="Drop_lytype" class="form-control input-s-sm" runat="server" >
                                    </asp:DropDownList>
                                </div>
                            </div>
                       <%--     <div class="form-group">
                                <span class="col-sm-4">领用时间：</span>
                                <div class="col-sm-8">
                                    <asp:TextBox ID="txt_lydate" class="form-control" runat="server"  ReadOnly="True"></asp:TextBox>
                                </div>
                            </div>--%>
                            <div class="form-group">
                                <span class="col-sm-4">领用人：</span>
                                <div class="col-sm-8">
                                    <asp:TextBox ID="txt_lyuid" class="form-control" runat="server" placeholder="请输入工号或姓名"  ></asp:TextBox>
                                </div>
                            </div>
                           <div class="form-group">
                                <span class="col-sm-4">领用机台号：</span>
                                <div class="col-sm-8">
                                 <%--   <asp:DropDownList ID="Drop_lysbno" class="form-control input-s-sm" runat="server" >
                                    </asp:DropDownList>--%>
                                     <asp:TextBox ID="txt_lysbno" class="form-control" runat="server" ></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group">
                                <span class="col-sm-4" style="padding-right:2px">领用备注：</span>
                                <div class="col-sm-8">
                                    <asp:TextBox ID="txt_lyremark" class="form-control" runat="server" 
                                      ></asp:TextBox>
                                </div>
                            </div>
                           
                        </div>
                    </div>
                </div>
                <div class="col-sm-6  col-md-5" id="divRK"  runat ="server" >
                    <div class="panel panel-info">
                        <div class="panel-heading">
                            &nbsp;入库</div>
                        <div class="panel-body">
                            <div class="form-group">
                                <span class="col-sm-4">夹具号：</span>
                                <div class="col-sm-8">
                               <asp:TextBox ID="txt_rkjiajuno" class="form-control" runat="server" ></asp:TextBox>
                                        <asp:Image ID="Image1" runat="server" ImageUrl='~/Images/fdj.gif' Width="14"  Height="14" onmouseover="this.style.cursor='hand'"   onclick=" GetJiaju_no('RK')"/>
                                    
                                </div>
                            </div>

                              <div class="form-group">
                                <span class="col-sm-4">零件号：</span>
                                <div class="col-sm-8">
                                    <asp:TextBox ID="txt_rkpn" class="form-control" runat="server" ></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group">
                                <span class="col-sm-4">零件名称：</span>
                                <div class="col-sm-8">
                                    <asp:TextBox ID="txt_rkname" class="form-control" runat="server"  ></asp:TextBox>
                                </div>
                            </div>

                            <div class="form-group">
                                <span class="col-sm-4" style="padding-right:2px">入库类型：</span>
                                <div class="col-sm-8">
                                    <asp:DropDownList ID="Drop_rktype" class="form-control input-s-sm" runat="server" >
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="form-group">
                                <span class="col-sm-4">入库库位：</span>
                                <div class="col-sm-8">
                                    <asp:TextBox ID="txt_rkloc" class="form-control" runat="server"   ></asp:TextBox>
                                </div>
                            </div>
                          <%-- <div class="form-group">
                                <span class="col-sm-4">入库时间：</span>
                                <div class="col-sm-8">
                                    <asp:TextBox ID="txt_rkdate" class="form-control" runat="server"  ReadOnly="True" ></asp:TextBox>
                                </div>
                            </div>--%>
                              <div class="form-group">
                                <span class="col-sm-4">入库人：</span>
                                <div class="col-sm-8">
                                    <asp:TextBox ID="txt_rkuid" class="form-control" runat="server"  placeholder="请输入工号或姓名"  ></asp:TextBox>
                                </div>
                            </div>
                             <div class="form-group">
                                <span class="col-sm-4">入库描述：</span>
                                <div class="col-sm-8">
                                    <asp:TextBox ID="txt_rkremark" class="form-control" runat="server"  ></asp:TextBox>
                                </div>
                            </div>

                            <div class="form-group">
                                <span class="col-sm-5"></span>
                                <div class="col-sm-7">
                                    &nbsp;
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-sm-12 col-md-10">
                   
                        <div class="row row-container">
                            <div class="col-sm-12">
                                     <div    align="center">
                                        <asp:Button ID="btn_Start" runat="server" Font-Size="X-Large"  Width="120px"
                                            class="btn btn-primary" 
                                             Text="确认" onclick="btn_Start_Click"    OnClientClick="return validate('submit');"
                                             />
                                    </div>

                                    <div ></div>

                              

                                
                                      <div id="Div7" runat="server" class="col-sm-4  col-md-offset-2" style="padding: 0;
                                        position: relative; float: left; top: 0px; left: 0px; width: 200px;
                                        height: 70px; display:none">
                                        <asp:Button ID="btn_Return" runat="server" Font-Size="X-Large"
                                            class="btn btn-primary" Style="width: 200px; top: -1px; height: 70px;" 
                                            Text="返回"
                                          />
                                       
                                    </div>

                            

                            


                            </div>
                        </div>
              </div>
                </div>
            </div>
        </div>
   
</asp:Content>


