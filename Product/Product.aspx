<%@ Page Title="MES生产管理系统" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="Product.aspx.cs" Inherits="Product" MaintainScrollPositionOnPostback="True" ValidateRequest="true" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <link href="../Content/css/bootstrap.min.css" rel="stylesheet" />
    <script src="../Content/js/jquery.min.js"></script>
    <script src="../Content/js/bootstrap.min.js"></script>
    <script src="../Content/js/plugins/layer/layer.min.js"></script>
    <script src="../Content/js/plugins/layer/laydate/laydate.js"></script>
    <link href="../Content/js/plugins/bootstrap-select/css/bootstrap-select.min.css" rel="stylesheet" />

    <script src="../Content/js/plugins/bootstrap-select/js/bootstrap-select.min.js"></script>

    <script type="text/javascript">
        $(document).ready(function () {
            $("#mestitle").text("【产品信息维护】");
            $('.selectpicker').change(function(){               
                $("input[id*='DDL_sqe_user22']").val($(".selectpicker").val());
            });
        })
        
    </script>
      <script type="text/javascript">
   
          $().ready(function(){      
          $("input[QTY='QTY']").click(function(){
              var ID=$(this).attr("rowvalue");
              var year=$(this).attr("headvalue");
                 
              $.post("Product.ashx?id="+ID+"&year="+year, { id: ID,year:year }, function (result) {
                  if (result != "") {                         
                       
                      $("div[id*='divShowNumByMonth']").html(year+"年每月产量："+ result); 
                        
                  }
                    
                    
              });
          })
          })

          $().ready(function(){           

              $("input[id*='dingdian_date']").click(function (e) {
                  laydate({
                      choose: function (dates) { //选择好日期的回调                       
                          $("input[id*='dingdian_date']").change();
                      }
                  })
              })    
              
          })
        $().ready(function(){           

            $("input[id*='pc_date']").click(function (e) {
                laydate({
                    choose: function (dates) { //选择好日期的回调                       
                        $("input[id*='pc_date']").change();
                    }
                })
            })           
        })
        $().ready(function(){           

            $("input[id*='end_date']").click(function (e) {
                laydate({
                    choose: function (dates) { //选择好日期的回调                       
                        $("input[id*='end_date']").change();
                    }
                })
            })


             
        }) 

        var popupwindow = null;

        function GetLJH() {
 
            popupwindow = window.open('../Select/select_product_ljh.aspx', '_blank', 'height=500,width=1000,resizable=no,menubar=no,scrollbars =no,location=no');
        }

        function XMH_setvalue(formName, ljh,baojia_no) {
       
            // $("input[id*='txt_CP_ID']").val(CP_ID);
            ctl01.<%=txt_CP_ID.ClientID%>.value = ljh;
            ctl01.<%=txt_baojia_no.ClientID%>.value = baojia_no;  
            document.getElementById('<%=txt_CP_ID.ClientID%>').onchange();
            //$("input[id*='txt_CP_ID_Text']").change();
            <%-- form1.<%=txt_ljh.ClientID%>.value = ljh;--%>
                <%--ctl01.<%=txt_sktj.ClientID%>.value = CP_ID;--%>
         
  
            popupwindow.close();
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
            font-size:12px;
            padding-bottom: 5px;
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
        .auto-style1 {
            height: 49px;
        }
        .auto-style2 {
            height: 54px;
        }
        .tbl td
{ border:1px solid black;
                 padding-left:3px;
                 padding-right:3px;
                 padding-top:3px;
        }

        </style>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div class="col-md-10">
   
        <div class="row row-container">
            <div class="col-md-12">
                <div class="panel panel-info">
                    <div class="panel-heading" data-toggle="collapse" data-target="#SQXX">
                        <strong>申请人信息</strong>
                    </div>
           
                    <div class="panel-body <% =ViewState["lv"].ToString() == "SQXX" ? "" : "collapse" %>" id="SQXX">
                        <div class="col-xs-12 col-sm-12  col-md-12 col-lg-12">
                            <div class="">
                                <asp:UpdatePanel ID="UpdatePanel_request" runat="server">
                                    <ContentTemplate>
                                        <table style="height: 35px; width: 100% ">
                                            <tr>
                                                <td>申请人：
                                                </td>
                                                <td>
                                                    <div class="form-inline">
                                                        <input id="txt_create_by_empid" class="form-control input-s-sm" style="height: 35px; width: 100px" runat="server" readonly="True" />
                                                        /
                                                    <input id="txt_create_by_name" class="form-control input-s-sm" style="height: 35px; width: 100px" runat="server" readonly="True" />
                                                        /
                                                    <input id="txt_create_by_ad" class="form-control input-s-sm" style="height: 35px; width: 100px" runat="server" readonly="True" />
                                                    </div>
                                                </td>
                                                <td>部门：
                                                </td>
                                                <td>
                                                    <input id="txt_create_by_dept" class="form-control input-s-sm" style="height: 35px; width: 100px" runat="server" readonly="True" />
                                                   </td>
                                                <td>部门经理：
                                                </td>
                                                <td>
                                                    <div class="form-inline">
                                                        <input id="txt_managerid" class="form-control input-s-sm" style="height: 35px; width: 100px" runat="server" readonly="True" />
                                                        /<input id="txt_manager" class="form-control input-s-sm" style="height: 35px; width: 100px" runat="server" readonly="True" />
                                                        /<input id="txt_manager_AD" class="form-control input-s-sm" style="height: 35px; width: 100px" runat="server" readonly="True" />
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td >当前登陆人员</td>
                                                <td>
                                                    <div class="form-inline">
                                                        <input id="txt_update_user" class="form-control input-s-sm" style="height: 35px; width: 100px" runat="server" readonly="True" />
                                                        /<input id="txt_update_user_name" class="form-control input-s-sm" style="height: 35px; width: 100px" runat="server" readonly="True" />/
                                                        <input id="txt_update_user_job" class="form-control input-s-sm" style="height: 35px; width: 100px" runat="server" readonly="True" />
                                                        /<input id="txt_update_user_dept" class="form-control input-s-sm" style="height: 35px; width: 100px" runat="server" readonly="True" />
                                                    </div>
                                                </td>
                                                <td>申请日期：</td>
                                                <td>
                                                    <input id="txt_CreateDate" class="form-control input-s-sm" style="height: 35px; width: 100px" runat="server" readonly="True" />
                                                </td>
                                                <td>Code：</td>
                                                <td>
                                                    <input id="txt_Code" class="form-control input-s-sm" style="height: 35px; width: 200px" runat="server" readonly="True" />
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
                        <strong>产品信息</strong>
                    </div>
                   
                    <div class="panel-body " id="XSGCS">
                        <div class="col-xs-12 col-sm-12  col-md-12 col-lg-12">

                            <div>
                                <fieldset style="border-color: lightblue">
                                    <legend>一.基础信息</legend>
                                    <table style="width: 100%">
                                        <tr>
                                            <td class="auto-style2">从定点报价中选择:</td>
                                            <td class="auto-style2" >
                                                    <img id="select_ljh" runat="server" name="selectljh" style="border: 0px;" src="../images/fdj.gif" alt="select" onclick="GetLJH()" /><asp:TextBox ID="txt_CP_ID" Style="height: 0px; width: 0px" runat="server" AutoPostBack="True" OnTextChanged="txt_CP_ID_TextChanged"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="yz46" runat="server" ControlToValidate="txt_productcode" ErrorMessage="请选择零件" ValidationGroup="request" ForeColor="Red"></asp:RequiredFieldValidator>
                                            </td>
                                            <td class="auto-style2">报价单号：</td>
                                            <td class="auto-style2">
                                                <input id="txt_baojia_no" class="form-control input-s-sm" style="height: 35px; width: 150px" runat="server" readonly="True" /></td>
                                            <td rowspan="13"><asp:Panel ID=Panel4 runat =server GroupingText="产品图片"   Font-Size=Medium   BodyPadding="5">
                            <table>
                                            <tr>
                                            <td>
                                                <asp:Image ID="Image2" runat="server" Width="300px" Height="300px" 
                                                    />
                                            </td>
                                            </tr>
                                             <tr>
                                            <td class="auto-style1">
                                                <asp:FileUpload ID="txtupload" runat="server" />
                                                &nbsp;<asp:Button ID="btnImg" runat="server" Text=" 上传 " 
                                                    onclick="btnImg_Click" />
                                                <asp:TextBox ID="txtproduct_img" runat="server" Width=200px Enabled="False" ></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="yz61" runat="server" ControlToValidate="txtproduct_img" ErrorMessage="必须选择产品图片" ForeColor="Red" ValidationGroup="request"></asp:RequiredFieldValidator>
                                            </td>
                                            </tr>
                                            </table>
                            
                            </asp:Panel>
                                                &nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td>项目号：</td>
                                            <td>
                                                <div class="form-inline">
                                                <input id="txt_pgino" class="form-control input-s-sm" style="height: 35px; width: 150px" runat="server" readonly="True" /></div>
                                            </td>
                                            <td>报价负责人：</td>
                                            <td>
                                                    <input id="txt_Sales_engineers" class="form-control input-s-sm" style="height: 35px; width: 150px" runat="server" readonly="True" /></td>
                                        </tr>
                                        <tr>
                                            <td>零件当前版本:</td>
                                            <td><div class="form-inline">
                                                <input id="txt_version" class="form-control input-s-sm" style="height: 35px; width: 150px" runat="server" readonly="True" /><asp:RequiredFieldValidator ID="yz48" runat="server" ControlToValidate="txt_pgino" ErrorMessage="不能为空" ValidationGroup="request" ForeColor="Red"></asp:RequiredFieldValidator>
                                            </div> </td>
                                            <td>&nbsp;</td>
                                            <td  runat="server">
                                                    &nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td>客户零件号：</td>
                                            <td><div class="form-inline">
                                                <input id="txt_productcode" class="form-control input-s-sm" style="height: 35px; width: 150px" runat="server" readonly="True" /><asp:RequiredFieldValidator ID="yz49" runat="server" ControlToValidate="txt_productcode" ErrorMessage="不能为空" ValidationGroup="request" ForeColor="Red"></asp:RequiredFieldValidator>
                                              </div> </td>
                                            <td>零件名称：</td>
                                            <td><div class="form-inline">
                                                <input id="txt_productname" class="form-control input-s-sm" style="height: 35px; width: 150px" runat="server"  /><asp:RequiredFieldValidator ID="yz51" runat="server" ControlToValidate="txt_productname" ErrorMessage="不能为空" ValidationGroup="request" ForeColor="Red"></asp:RequiredFieldValidator>
                                                <asp:RequiredFieldValidator ID="yz67" runat="server" ControlToValidate="txt_productname" ErrorMessage="不能为空" ValidationGroup="update1" ForeColor="Red"></asp:RequiredFieldValidator>
                                             </div> </td>
                                        </tr>
                                        <tr>
                                            <td>制造地点：</td>
                                            <td><div class="form-inline">
                                                <input id="txt_make_factory" class="form-control input-s-sm" style="height: 35px; width: 150px" runat="server" readonly="True" /><asp:RequiredFieldValidator ID="yz50" runat="server" ControlToValidate="txt_make_factory" ErrorMessage="不能为空" ValidationGroup="request" ForeColor="Red"></asp:RequiredFieldValidator>
                                                </div> </td>
                                            <td>产品大类：</td>
                                            <td><div class="form-inline">
                                                <asp:DropDownList ID="DDL_product_leibie" class="form-control input-s-sm" style="height: 35px; width: 150px" runat="server"   >
                                                </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="yz47" runat="server" ControlToValidate="DDL_product_leibie" ErrorMessage="不能为空" ValidationGroup="request" ForeColor="Red"></asp:RequiredFieldValidator>
                                                    <asp:RequiredFieldValidator ID="yz62" runat="server" ControlToValidate="DDL_product_leibie" ErrorMessage="不能为空" ValidationGroup="update1" ForeColor="Red"></asp:RequiredFieldValidator>
                                             </div></td>
                                        </tr>
                                        <tr>
                                            <td>直接顾客：</td>
                                             <td><div class="form-inline">
                                                <asp:DropDownList ID="DDL_customer_name" class="form-control input-s-sm" style="height: 35px; width: 150px" runat="server"    >
                                                </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="yz52" runat="server" ControlToValidate="DDL_customer_name" ErrorMessage="不能为空" ValidationGroup="request" ForeColor="Red"></asp:RequiredFieldValidator>
                                             </div> </td>
                                            <td>最终顾客：</td>
                                           <td><div class="form-inline">
                                                <asp:DropDownList ID="DDL_end_customer_name" class="form-control input-s-sm" style="height: 35px; width: 150px" runat="server"   >
                                                </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="yz53" runat="server" ControlToValidate="DDL_end_customer_name" ErrorMessage="不能为空" ValidationGroup="request" ForeColor="Red"></asp:RequiredFieldValidator>
                                            </div> </td>
                                        </tr>
                                        <tr>
                                            <td>定点日期(最早)：</td>
                                             <td><div class="form-inline">
                                                <input id="txt_dingdian_date"  class="form-control input-s-sm" style="height: 35px; width: 150px" runat="server" readonly="True" /><asp:RequiredFieldValidator ID="yz55" runat="server" ControlToValidate="txt_dingdian_date" ErrorMessage="不能为空" ValidationGroup="request" ForeColor="Red"></asp:RequiredFieldValidator>
                                                </div></td>
                                            <td>项目取消日期:</td>
                                            <td> 
                                                <input id="txt_delete_date" class="form-control input-s-sm" style="height: 35px; width: 150px" runat="server" onclick="laydate()"  /></td>
                                        </tr>
                                        <tr>
                                            <td>批产日期(最早)：</td>
                                           <td><div class="form-inline">
                                                <input id="txt_pc_date" class="form-control input-s-sm" style="height: 35px; width: 150px" runat="server" onclick="laydate()" readonly="True" /></div> </td>
                                          <td>&nbsp;</td>
                                            <td> </td>
                                        </tr>
                                        <tr>
                                            <td>停产日期(最晚)：</td>
                                            <td> 
                                               <div class="form-inline">
                                                <input id="txt_end_date" class="form-control input-s-sm" style="height: 35px; width: 150px" runat="server" onclick="laydate()" readonly="True" /></div></td>
                                            <td>项目状态：</td>
                                            <td><div class="form-inline">
                                                <asp:DropDownList ID="DDL_product_status" class="form-control input-s-sm" style="height: 35px; width: 150px" runat="server"  Enabled="False"  readonly="True" BackColor="#CCCCCC"  >
                                                </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="yz59" runat="server" ControlToValidate="DDL_product_status" ErrorMessage="不能为空" ValidationGroup="request" ForeColor="Red"></asp:RequiredFieldValidator>
                                            </div> </td>
                                        </tr>
                                        <tr>
                                            <td>客户要求产能：</td>
                                            <td> <div class="form-inline">
                                                <input id="txt_customer_requestCN" class="form-control input-s-sm" style="height: 35px; width: 150px" runat="server"  /></div>  </td>
                                            <td>&nbsp;</td>
                                            <td>
                                                &nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td>销售负责人(全)</td>
                                            <td colspan="3"> 
                                                    <input id="txt_Sales_engineers_all" class="form-control input-s-sm" style="height: 35px; width: 550px" runat="server" readonly="True" /></td>
                                        </tr>
                                        <tr>
                                            <td>顾客项目(全)：</td>
                                            <td colspan="3"> 
                                                <input id="txt_customer_project" class="form-control input-s-sm" style="height: 35px; width: 550px" runat="server" readonly="True" /><asp:RequiredFieldValidator ID="yz54" runat="server" ControlToValidate="txt_customer_project" ErrorMessage="不能为空" ForeColor="Red" ValidationGroup="request"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>说明:</td>
                                            <td colspan="3"> <div class="form-inline">
                                                <asp:TextBox ID="txt_customer_requestSM" class="form-control input-s-sm" style="height: 75px; width: 550px" runat="server" TextMode="MultiLine"></asp:TextBox>
                                                
                                                             </div>
                                                </td>
                                        </tr>
                                        </table>
                                    <table width="100%">
                                        


                                        <tr>
                                          
                                             <td align="right">

                                            <asp:Button ID="BTN_product_update" runat="server" class="btn btn-primary " Style="height: 35px; width: 130px" Text="修改基础信息" ValidationGroup="update1" OnClick="BTN_product_update_Click" />

                                                  </td>
                                        </tr>



                                    </table>

                                </fieldset>
                                <table style="width: 100%">
                                     
                                    <tr>
                                        <td>
                                            <asp:Panel ID="Panel1" runat="server" GroupingText="版本信息">
                                      
                                     <div class="form-inline" style="margin-top: 10px">
                                    <label> </label>
                                    </div>
                                                <asp:GridView ID="gv_Product_version" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                                    ForeColor="#333333" GridLines="None" ShowFooter="True"
                                                    OnRowCommand="gv_Product_version_RowCommand" Width="100%">
                                                    <RowStyle BackColor="#EFF3FB" />
                                                    <Columns>
                                                                <asp:TemplateField HeaderText="项目号">
                                                         
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txt_pgino" runat="server" Width="70px" Text='<%#DataBinder.Eval(Container,"DataItem.pgino") %>' Enabled="False" ReadOnly="True" ></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="零件号">
                                                       
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txt_productcode" runat="server" Width="120px" Text='<%#DataBinder.Eval(Container,"DataItem.productcode") %>' AutoPostBack="True" OnTextChanged="txt_productcode_TextChanged" ></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="工程版本号">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txt_version" runat="server" Width="30px" Text='<%#DataBinder.Eval(Container,"DataItem.version") %>' Enabled="False" ReadOnly="True"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                           <asp:TemplateField HeaderText="说明">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txt_Desc_sm" runat="server" Width="250px" Text='<%#DataBinder.Eval(Container,"DataItem.Desc_sm") %>'></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                         <asp:TemplateField HeaderText="操作时间">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txt_update_date" runat="server" Width="60px" Text='<%#DataBinder.Eval(Container,"DataItem.update_date") %>' Enabled="False" ReadOnly="True"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                      <asp:TemplateField HeaderText="操作人">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txt_update_User" runat="server" Width="70px" Text='<%#DataBinder.Eval(Container,"DataItem.update_User") %>' Enabled="False" ReadOnly="True"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <FooterTemplate>
                                                                <asp:Button ID="btnAdd" runat="server" CommandName="add" ForeColor="#3333FF" Text="添加" />
                                                            </FooterTemplate>
                                                            <ItemTemplate>
                                                                <asp:Button ID="btnDel" runat="server" Text="删除" CommandName="del" CommandArgument='<%#Container.DataItemIndex %>' ForeColor="#6600FF" Font-Size="Smaller" Enabled="False" Visible="False" />
                                                            </ItemTemplate>

                                                            <FooterStyle HorizontalAlign="Right" />

                                                            <ItemStyle HorizontalAlign="Right" />

                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <FooterStyle BackColor="White" Font-Bold="True" ForeColor="Black" />
                                                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                    <EditRowStyle BackColor="#2461BF" />
                                                    <AlternatingRowStyle BackColor="White" />
                                                </asp:GridView>

                                            </asp:Panel>
                                        </td>
                                    </tr>

                                    

                                    <tr>
                                        <td align="right">
                                            <asp:Button ID="BTN_version_update" runat="server" class="btn btn-primary" Style="height: 35px; width: 130px" Text="修改版本信息" OnClick="BTN_version_update_Click"  />
                                        </td>
                                    </tr>
                                    </table>
                            
                                <div style="width: 100%">
                                </div>

                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
           
        <div class="row row-container">
            <div class="col-md-12">
                <div class="panel panel-info">
                    <div class="panel-heading" data-toggle="collapse" data-target="#CPFZR">
                        <strong>产品负责人</strong>
                    </div>
                    <div class="panel-body <% =ViewState["lv"].ToString() == "CPFZR" ? "" : "collapse" %>" id="CPFZR">
                        <div class="col-xs-12 col-sm-12  col-md-12 col-lg-12">
                            <div class="">
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                                        <table style="height: 35px; width: 100% ">
                                            <tr>
                                                <td >项目工程师：</td>
                                                <td><div class="form-inline">
                                                    <asp:DropDownList ID="DDL_project_user" runat="server"  class="form-control input-s-sm" style="height: 35px; width: 150px">
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="yz60" runat="server" ControlToValidate="txt_pgino" ErrorMessage="不能为空" ForeColor="Red" ValidationGroup="request"></asp:RequiredFieldValidator>
                                                </div></td>
                                                <td>产品工程师：</td>
                                                <td>
                                                    <asp:DropDownList ID="DDL_product_user" runat="server"  class="form-control input-s-sm" style="height: 35px; width: 150px">
                                                    </asp:DropDownList>
                                                </td>
                                                <td>模具工程师：</td>
                                                <td>
                                                    <asp:DropDownList ID="DDL_moju_user" runat="server"  class="form-control input-s-sm" style="height: 35px; width: 150px">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>压铸工艺工程师:</td>
                                                <td>
                                                    <div class="form-inline">
                                                        <asp:DropDownList ID="DDL_yz_user" runat="server"  class="form-control input-s-sm" style="height: 35px; width: 150px">
                                                        </asp:DropDownList>
                                                    </div>
                                                </td>
                                                <td>机加调试工程师：</td>
                                                <td>
                                                    <asp:DropDownList ID="DDL_jj_user" runat="server"  class="form-control input-s-sm" style="height: 35px; width: 150px">
                                                    </asp:DropDownList>
                                                </td>
                                                <td>质量工程师：</td>
                                                <td>
                                                    <asp:DropDownList ID="DDL_zl_user" runat="server"  class="form-control input-s-sm" style="height: 35px; width: 150px">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>包装工程师：</td>
                                                <td>
                                                    <asp:DropDownList ID="DDL_bz_user" runat="server"  class="form-control input-s-sm" style="height: 35px; width: 150px">
                                                    </asp:DropDownList>
                                                </td>
                                                <td>计划工程师：</td>
                                                <td>
                                                    <asp:DropDownList ID="DDL_wl_user" runat="server"  class="form-control input-s-sm" style="height: 35px; width: 150px">
                                                    </asp:DropDownList>
                                                </td>
                                                <td>质量主管：</td>
                                                <td>
                                                    <asp:DropDownList ID="DDL_zhiliangzhuguan_user" runat="server"  class="form-control input-s-sm" style="height: 35px; width: 150px">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>供应商质量工程师：</td>
                                                <td>
                                                    <asp:DropDownList ID="DDL_sqe_user1" runat="server"  class="form-control input-s-sm" style="height: 35px; width: 150px">
                                                    </asp:DropDownList>
                                                </td>
                                                <td>物流工程师：</td>
                                                <td>
                                                    <asp:DropDownList ID="DDL_sqe_user2" runat="server"  class="form-control input-s-sm" style="height: 35px; width: 250px" Enabled="False" >
                                                    </asp:DropDownList><br>
                                                      <asp:TextBox ID="DDL_sqe_user22" class="form-control input-s-sm" runat="server" style="display:none"  ></asp:TextBox>
                                    <select id="selectwl" name="selectwl" class="selectpicker " multiple  data-live-search="true" runat="server" style="width:250px" >                                          
                                    </select>
                                                </td>
                                                <td>采购工程师：</td>
                                                <td>
                                                    <asp:DropDownList ID="DDL_caigou" runat="server"  class="form-control input-s-sm" style="height: 35px; width: 150px">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>销售工程师：</td>
                                                <td>
                                                    <asp:DropDownList ID="DDL_sale" runat="server"  class="form-control input-s-sm" style="height: 35px; width: 150px">
                                                    </asp:DropDownList>
                                                </td>
                                                <td>夹具工程师：</td>
                                                <td>
                                                    <asp:DropDownList ID="DDL_jiaju_egnieer" runat="server"  class="form-control input-s-sm" style="height: 35px; width: 150px">
                                                    </asp:DropDownList>
                                                </td>
                                                <td>刀具工程师：</td>
                                                <td>
                                                    <asp:DropDownList ID="DDL_daoju_egnieer" runat="server"  class="form-control input-s-sm" style="height: 35px; width: 150px">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>检具工程师：</td>
                                                <td>
                                                    <asp:DropDownList ID="DDL_jianju_egnieer" runat="server"  class="form-control input-s-sm" style="height: 35px; width: 150px">
                                                    </asp:DropDownList>
                                                </td>
                                                <td>模具管理工程师：</td>
                                                <td>
                                                    <asp:DropDownList ID="DDL_mojugl_egnieer" runat="server"  class="form-control input-s-sm" style="height: 35px; width: 150px">
                                                    </asp:DropDownList>
                                                </td>
                                                <td>&nbsp;</td>
                                                <td>
                                                    &nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td>&nbsp;</td>
                                                <td>&nbsp;</td>
                                                <td>&nbsp;</td>
                                                <td>&nbsp;</td>
                                                <td>&nbsp;</td>
                                                <td align="right">
                                                    <asp:Button ID="BTN_fzr_update" runat="server" class="btn btn-primary" Style="height: 35px; width: 130px" Text="修改产品负责人" OnClick="BTN_fzr_update_Click" />
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
                    <div class="panel-heading" data-toggle="collapse" data-target="#cpyc">
                        <strong>销售预测</strong>
                    </div>
                    <div class="panel-body <% =ViewState["lv"].ToString() == "cpyc" ? "" : "collapse" %>" id="cpyc">
                        <div class="col-xs-12 col-sm-12  col-md-12 col-lg-12">
                            <table style="width: 100%">
                                <tr>
                                    <td colspan="2">
                                        <asp:Panel ID="Panel3" runat="server" GroupingText="销售预测">
                                            <asp:GridView ID="gv_Product_Quantity" runat="server" 
                                                AutoGenerateColumns="False" CellPadding="4" 
                                                ForeColor="#333333" GridLines="None" ShowFooter="True" 
                                                Width="100%" OnRowCommand="gv_Product_Quantity_RowCommand" 
                                                OnRowDataBound="gv_Product_Quantity_RowDataBound" OnRowCreated="gv_Product_Quantity_RowCreated">
                                                <RowStyle BackColor="#EFF3FB" />
                                                <Columns>
                                                        <asp:TemplateField HeaderText="ID" Visible="False">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="ID" runat="server" Text='<%#DataBinder.Eval(Container,"DataItem.ID") %>' Width="80px" Enabled="False" ReadOnly="True"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>  
                                                        <asp:TemplateField HeaderText="colour" Visible="False">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="colour" runat="server" Text='<%#DataBinder.Eval(Container,"DataItem.colour") %>' Width="80px" Enabled="False" ReadOnly="True"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>  
                                                   <asp:TemplateField HeaderText="产品状态">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="product_status_dt" runat="server" Text='<%#DataBinder.Eval(Container,"DataItem.product_status_dt") %>' Width="80px" Enabled="False" ReadOnly="True"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>                                            
                                                         <asp:TemplateField HeaderText="Ship_from">
                                                        <ItemTemplate>
                                                            <asp:DropDownList ID="ddl_Ship_from" runat="server" Style="width: 120px">
                                                                <asp:ListItem></asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:TextBox ID="Ship_from" runat="server" Text='<%#DataBinder.Eval(Container,"DataItem.Ship_from") %>' Visible="false" Width="100px"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="Ship_to">
                                                        <ItemTemplate>
                                                                     <asp:DropDownList ID="ddl_Ship_to" runat="server" Style="width: 250px">
                                                                <asp:ListItem></asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:TextBox ID="Ship_to" runat="server" Text='<%#DataBinder.Eval(Container,"DataItem.Ship_to") %>' Visible="false" Width="100px" ></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                               <asp:TemplateField HeaderText="顾客项目">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="customer_project" runat="server" Text='<%#DataBinder.Eval(Container,"DataItem.customer_project") %>' Width="100px" ></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                       <asp:TemplateField HeaderText="客户代码">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="khdm" runat="server" Text='<%#DataBinder.Eval(Container,"DataItem.khdm") %>' Width="80px" AutoPostBack="True" OnTextChanged="khdm_TextChanged"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="定点日期">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="dingdian_date" runat="server" Text='<%#DataBinder.Eval(Container,"DataItem.dingdian_date","{0:yyyy-MM-dd}") %>' Width="80px"  AutoPostBack="True" OnTextChanged="dingdian_date_TextChanged"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="批产日期">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="pc_date" runat="server" Text='<%#DataBinder.Eval(Container,"DataItem.pc_date","{0:yyyy-MM-dd}") %>' Width="80px"  AutoPostBack="True" OnTextChanged="pc_date_TextChanged"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                      <asp:TemplateField HeaderText="停产日期">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="end_date" runat="server" Text='<%#DataBinder.Eval(Container,"DataItem.end_date","{0:yyyy-MM-dd}") %>' Width="80px" AutoPostBack="True" OnTextChanged="end_date_TextChanged" ></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="初始单价<br>(本币)">                                                        
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="pc_dj" runat="server" Width="60px" Text='<%#DataBinder.Eval(Container,"DataItem.pc_dj","{0:N2}") %>' CssClass="textalign" AutoPostBack="True" OnTextChanged="pc_dj_TextChanged"   ></asp:TextBox>
                                                            </ItemTemplate>
                                                            <ItemStyle />
                                                        </asp:TemplateField>
                                                          <asp:TemplateField HeaderText="当前单价<br>(本币)">   
                                                                    <FooterTemplate>
                                                               <asp:Label ID="Label6" runat="server" ForeColor="Red" Text="合计最大年用量:"></asp:Label><br>        
                                                            </FooterTemplate>                                                     
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="pc_dj_QAD" runat="server" Width="60px" Text='<%#DataBinder.Eval(Container,"DataItem.pc_dj_QAD","{0:N2}") %>' CssClass="textalign" Enabled="False"  ></asp:TextBox>
                                                            </ItemTemplate>
                                                            <ItemStyle />
                                                        </asp:TemplateField>
                                                       <asp:TemplateField HeaderText="最大年用量">
                                                            <FooterTemplate>
                                                                  <asp:TextBox ID="Lab_quantity_year" runat="server" ForeColor="Red" Width="80px" CssClass="textalign" BackColor="#CCCCCC" ReadOnly="True"></asp:TextBox>
                                                                <asp:TextBox ID="Lab_quantity_year_hj" runat="server" ForeColor="Red" Width="80px" CssClass="textalign" BackColor="#CCCCCC" ReadOnly="True"></asp:TextBox>
                                                            </FooterTemplate>
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="quantity_year" runat="server" Width="80px" Text='<%#DataBinder.Eval(Container,"DataItem.quantity_year","{0:N0}") %>' CssClass="textalign" Enabled="False"></asp:TextBox>
                                                            </ItemTemplate>
                                                            <FooterStyle />
                                                            <ItemStyle />
                                                        </asp:TemplateField>
                                                           <asp:TemplateField HeaderText="最大年销售额(本币)<br>(当前单价*最大年用量)">
                                                            <FooterTemplate>                                             
                                                                <asp:TextBox ID="Lab_price_year" runat="server" ForeColor="Red" Width="90px" CssClass="textalign" ReadOnly="True" BackColor="#CCCCCC"></asp:TextBox>
                                                                 <asp:TextBox ID="Lab_price_year_hj" runat="server" ForeColor="Red" Width="90px" CssClass="textalign" ReadOnly="True" BackColor="#CCCCCC"></asp:TextBox>
                                                            </FooterTemplate>
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="price_year" runat="server" Width="90px" Text='<%#DataBinder.Eval(Container,"DataItem.price_year","{0:N0}") %>' CssClass="textalign" ReadOnly="True" Enabled="False"></asp:TextBox>
                                                            </ItemTemplate>
                                                            <FooterStyle  />
                                                            <ItemStyle />
                                                        </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="PSW产能">
                                                            <FooterTemplate>
                                                                  <asp:TextBox ID="Lab_psw_quantity" runat="server" ForeColor="Red" Width="80px" CssClass="textalign" BackColor="#CCCCCC" ReadOnly="True"></asp:TextBox>
                                                            </FooterTemplate>
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="psw_quantity" runat="server" Width="80px" Text='<%#DataBinder.Eval(Container,"DataItem.psw_quantity","{0:N0}") %>' CssClass="textalign"></asp:TextBox>
                                                            </ItemTemplate>
                                                            <FooterStyle />
                                                            <ItemStyle />
                                                        </asp:TemplateField>
                                                         <asp:TemplateField HeaderText="销售负责人">
                                                           
                                                        <ItemTemplate>
                                                            <asp:DropDownList ID="ddl_Sales" runat="server" Style="width: 100px" AutoPostBack="True" OnSelectedIndexChanged="ddl_Sales_SelectedIndexChanged">
                                                                <asp:ListItem></asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:TextBox ID="txtSales" runat="server" Text='<%#DataBinder.Eval(Container,"DataItem.Sales") %>' Visible="False"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="操作时间" Visible="False">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="update_date" runat="server" Width="60px" Text='<%#DataBinder.Eval(Container,"DataItem.update_date") %>' Enabled="False" ReadOnly="True"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="操作人" Visible="False">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="update_User" runat="server" Width="50px" Text='<%#DataBinder.Eval(Container,"DataItem.update_User") %>' ReadOnly="True" Enabled="False"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="2012">
                                                            <FooterTemplate>
                                                                <table>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:TextBox ID="Lab_QTY_A_2012" runat="server" 
                                                                                BackColor="#CCCCCC" CssClass="textalign" ForeColor="Red" 
                                                                                ReadOnly="True" Width="80px"></asp:TextBox>
                                                                        </td>
                                                                       
                                                                        <td>
                                                                            <asp:TextBox ID="Lab_QTY_2012" runat="server" 
                                                                                BackColor="#CCCCCC" CssClass="textalign" ForeColor="Red" 
                                                                                ReadOnly="True" Width="80px"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </FooterTemplate>
                                                            <%--   <ItemTemplate>
                                                                <asp:TextBox ID="QTY_2012" runat="server" Width="80px"  Text='<%#DataBinder.Eval(Container,"DataItem.QTY_2012","{0:N0}") %>' CssClass="textalign" AutoPostBack="True" OnTextChanged="QTY_2012_TextChanged"></asp:TextBox>
                                                            </ItemTemplate>--%>
                                                            <FooterStyle />
                                                            <ItemStyle />
                                                            <HeaderTemplate>
                                                                <table align="center" width="100%">
                                                                    <tr>
                                                                        <td align="center" width="100%">
                                                                            <asp:Label ID="Label_2012" runat="server" Text="2012"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="text-align: center">
                                                                            <asp:Label ID="lb_A_2012" runat="server" Text="A" 
                                                                                Width="80px"></asp:Label>
                                                                            
                                                                            <asp:Label ID="lb_F_2012" runat="server" Text="F" 
                                                                                Width="80px"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <table width="100%">
                                                                    <tr>
                                                                        <td>
                                                                            <asp:TextBox ID="QTY_A_2012" runat="server" 
                                                                                CssClass="textalign" ReadOnly="true" 
                                                                                Text='<%#DataBinder.Eval(Container,"DataItem.A_2012","{0:N0}") %>' 
                                                                                Width="80px"></asp:TextBox>
                                                                            
                                                                            <asp:TextBox ID="QTY_2012" runat="server" 
                                                                                AutoPostBack="True" CssClass="textalign" 
                                                                                OnTextChanged="QTY_2012_TextChanged" 
                                                                                Text='<%#DataBinder.Eval(Container,"DataItem.QTY_2012","{0:N0}") %>' 
                                                                                Width="80px"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="2013">
                                                            <FooterTemplate>
                                                                <table>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:TextBox ID="Lab_QTY_A_2013" runat="server" 
                                                                                BackColor="#CCCCCC" CssClass="textalign" ForeColor="Red" 
                                                                                ReadOnly="True" Width="80px"></asp:TextBox>
                                                                        </td>
                                                                       
                                                                        <td>
                                                                            <asp:TextBox ID="Lab_QTY_2013" runat="server" 
                                                                                BackColor="#CCCCCC" CssClass="textalign" ForeColor="Red" 
                                                                                ReadOnly="True" Width="80px"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </FooterTemplate>
                                                            <%-- <ItemTemplate>
                                                                <asp:TextBox ID="QTY_2013" runat="server" Width="70px" Text='<%#DataBinder.Eval(Container,"DataItem.QTY_2013","{0:N0}") %>' CssClass="textalign" AutoPostBack="True" OnTextChanged="QTY_2012_TextChanged"></asp:TextBox>
                                                            </ItemTemplate>--%>
                                                            <FooterStyle />
                                                            <ItemStyle />
                                                            <HeaderTemplate>
                                                                <table align="center" width="100%">
                                                                    <tr>
                                                                        <td align="center" colspan="3" width="100%">
                                                                            <asp:Label ID="Label_2013" runat="server" Text="2013"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="text-align: center">
                                                                            <asp:Label ID="lb_A_2013" runat="server" Text="A" 
                                                                                Width="80px"></asp:Label>
                                                                          
                                                                            <asp:Label ID="lb_F_2013" runat="server" Text="F" 
                                                                                Width="80px"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <table width="100%">
                                                                    <tr>
                                                                        <td>
                                                                            <asp:TextBox ID="QTY_A_2013" runat="server" 
                                                                                CssClass="textalign" ReadOnly="true" 
                                                                                Text='<%#DataBinder.Eval(Container,"DataItem.A_2013","{0:N0}") %>' 
                                                                                Width="80px"></asp:TextBox>
                                                                           
                                                                            <asp:TextBox ID="QTY_2013" runat="server" 
                                                                                AutoPostBack="True" CssClass="textalign" 
                                                                                OnTextChanged="QTY_2012_TextChanged" 
                                                                                Text='<%#DataBinder.Eval(Container,"DataItem.QTY_2013","{0:N0}") %>' 
                                                                                Width="80px"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="2014">
                                                            <FooterTemplate>
                                                                <table>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:TextBox ID="Lab_QTY_A_2014" runat="server" 
                                                                                BackColor="#CCCCCC" CssClass="textalign" ForeColor="Red" 
                                                                                ReadOnly="True" Width="80px"></asp:TextBox>
                                                                        </td>
                                                                       
                                                                        <td>
                                                                            <asp:TextBox ID="Lab_QTY_2014" runat="server" 
                                                                                BackColor="#CCCCCC" CssClass="textalign" ForeColor="Red" 
                                                                                ReadOnly="True" Width="80px"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </FooterTemplate>
                                                            <%--   <ItemTemplate>
                                                                <asp:TextBox ID="QTY_2014" runat="server" Width="70px" Text='<%#DataBinder.Eval(Container,"DataItem.QTY_2014","{0:N0}") %>' CssClass="textalign" AutoPostBack="True" OnTextChanged="QTY_2012_TextChanged"></asp:TextBox>
                                                            </ItemTemplate>--%>
                                                            <FooterStyle />
                                                            <ItemStyle />
                                                            <HeaderTemplate>
                                                                <table align="center" width="100%">
                                                                    <tr>
                                                                        <td align="center" width="100%">
                                                                            <asp:Label ID="Label_2014" runat="server" Text="2014"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="text-align: center">
                                                                            <asp:Label ID="lb_A_2014" runat="server" Text="A" 
                                                                                Width="80px"></asp:Label>
                                                                           
                                                                            <asp:Label ID="lb_F_2014" runat="server" Text="F" 
                                                                                Width="80px"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <table width="100%">
                                                                    <tr>
                                                                        <td>
                                                                            <asp:TextBox ID="QTY_A_2014" runat="server" 
                                                                                BackColor="#CCCCCC" CssClass="textalign" ReadOnly="true" 
                                                                                Text='<%#DataBinder.Eval(Container,"DataItem.A_2014","{0:N0}") %>' 
                                                                                Width="80px"></asp:TextBox>
                                                                            
                                                                            <asp:TextBox ID="QTY_2014" runat="server" 
                                                                                AutoPostBack="True" CssClass="textalign" 
                                                                                OnTextChanged="QTY_2012_TextChanged" 
                                                                                Text='<%#DataBinder.Eval(Container,"DataItem.QTY_2014","{0:N0}") %>' 
                                                                                Width="80px"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="2015">
                                                            <FooterTemplate>
                                                                <table>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:TextBox ID="Lab_QTY_A_2015" runat="server" 
                                                                                BackColor="#CCCCCC" CssClass="textalign" ForeColor="Red" 
                                                                                ReadOnly="True" Width="80px"></asp:TextBox>
                                                                        </td>
                                                                       
                                                                        <td>
                                                                            <asp:TextBox ID="Lab_QTY_2015" runat="server" 
                                                                                BackColor="#CCCCCC" CssClass="textalign" ForeColor="Red" 
                                                                                ReadOnly="True" Width="80px"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </FooterTemplate>
                                                            <%-- <ItemTemplate>
                                                                <asp:TextBox ID="QTY_2015" runat="server" Width="70px" Text='<%#DataBinder.Eval(Container,"DataItem.QTY_2015","{0:N0}") %>' CssClass="textalign" AutoPostBack="True" OnTextChanged="QTY_2012_TextChanged"></asp:TextBox>
                                                            </ItemTemplate>--%>
                                                            <FooterStyle />
                                                            <ItemStyle />
                                                            <HeaderTemplate>
                                                                <table align="center" border="1" 
                                                                    style=" border-color:White" width="100%">
                                                                    <tr>
                                                                        <td align="center" colspan="3" width="100%">
                                                                            <asp:Label ID="Label_2015" runat="server" Text="2015"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="text-align: center">
                                                                            <asp:Label ID="lb_A_2015" runat="server" Text="A" 
                                                                                Width="80px"></asp:Label>
                                                                           
                                                                            <asp:Label ID="lb_F_2015" runat="server" Text="F" 
                                                                                Width="80px"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <table width="100%">
                                                                    <tr>
                                                                        <td>
                                                                            <asp:TextBox ID="QTY_A_2015" runat="server" 
                                                                                BackColor="#CCCCCC" CssClass="textalign" ReadOnly="true" 
                                                                                Text='<%#DataBinder.Eval(Container,"DataItem.A_2015","{0:N0}") %>' 
                                                                                Width="80px"></asp:TextBox>
                                                                           
                                                                            <asp:TextBox ID="QTY_2015" runat="server" 
                                                                                AutoPostBack="True" CssClass="textalign" 
                                                                                OnTextChanged="QTY_2012_TextChanged" 
                                                                                Text='<%#DataBinder.Eval(Container,"DataItem.QTY_2015","{0:N0}") %>' 
                                                                                Width="80px"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="2016">
                                                            <FooterTemplate>
                                                                <table>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:TextBox ID="Lab_QTY_A_2016" runat="server" 
                                                                                BackColor="#CCCCCC" CssClass="textalign" ForeColor="Red" 
                                                                                ReadOnly="True" Width="80px"></asp:TextBox>
                                                                        </td>
                                                                       
                                                                        <td>
                                                                            <asp:TextBox ID="Lab_QTY_2016" runat="server" 
                                                                                BackColor="#CCCCCC" CssClass="textalign" ForeColor="Red" 
                                                                                ReadOnly="True" Width="80px"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </FooterTemplate>
                                                            <%-- <ItemTemplate>
                                                                <asp:TextBox ID="QTY_2016" runat="server" Width="70px" Text='<%#DataBinder.Eval(Container,"DataItem.QTY_2016","{0:N0}") %>' CssClass="textalign" AutoPostBack="True" OnTextChanged="QTY_2012_TextChanged"></asp:TextBox>
                                                            </ItemTemplate>--%>
                                                            <FooterStyle />
                                                            <ItemStyle />
                                                            <HeaderTemplate>
                                                                <table align="center" border="1" 
                                                                    style=" border-color:White" width="100%">
                                                                    <tr>
                                                                        <td align="center" colspan="3" width="100%">
                                                                            <asp:Label ID="Label_2016" runat="server" Text="2016"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="text-align: center">
                                                                            <asp:Label ID="lb_A_2016" runat="server" Text="A" 
                                                                                Width="80px"></asp:Label>
                                                                           
                                                                            <asp:Label ID="lb_F_2016" runat="server" Text="F" 
                                                                                Width="80px"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <table width="100%">
                                                                    <tr>
                                                                        <td>
                                                                            <asp:TextBox ID="QTY_A_2016" runat="server" 
                                                                                BackColor="#CCCCCC" CssClass="textalign" ReadOnly="true" 
                                                                                Text='<%#DataBinder.Eval(Container,"DataItem.A_2016","{0:N0}") %>' 
                                                                                Width="80px"></asp:TextBox>
                                                                           
                                                                            <asp:TextBox ID="QTY_2016" runat="server" 
                                                                                AutoPostBack="True" CssClass="textalign" 
                                                                                OnTextChanged="QTY_2012_TextChanged" 
                                                                                Text='<%#DataBinder.Eval(Container,"DataItem.QTY_2016","{0:N0}") %>' 
                                                                                Width="80px"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="2017">
                                                            <FooterTemplate>
                                                                <table>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:TextBox ID="Lab_QTY_A_2017" runat="server" 
                                                                                BackColor="#CCCCCC" CssClass="textalign" ForeColor="Red" 
                                                                                ReadOnly="True" Width="80px"></asp:TextBox>
                                                                        </td>
                                                                       
                                                                        <td>
                                                                            <asp:TextBox ID="Lab_QTY_2017" runat="server" 
                                                                                BackColor="#CCCCCC" CssClass="textalign" ForeColor="Red" 
                                                                                ReadOnly="True" Width="80px"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </FooterTemplate>
                                                            <%--<ItemTemplate>
                                                                <asp:TextBox ID="QTY_2017" runat="server" Width="70px" Text='<%#DataBinder.Eval(Container,"DataItem.QTY_2017","{0:N0}") %>' CssClass="textalign" AutoPostBack="True" OnTextChanged="QTY_2012_TextChanged"></asp:TextBox>
                                                            </ItemTemplate>--%>
                                                            <FooterStyle />
                                                            <ItemStyle />
                                                            <HeaderTemplate>
                                                                <table align="center" border="1" 
                                                                    style=" border-color:White" width="100%">
                                                                    <tr>
                                                                        <td align="center" colspan="3" width="100%">
                                                                            <asp:Label ID="Label_2017" runat="server" Text="2017"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="text-align: center">
                                                                            <asp:Label ID="lb_A_2017" runat="server" Text="A" 
                                                                                Width="80px"></asp:Label>
                                                                           
                                                                            <asp:Label ID="lb_F_2017" runat="server" Text="F" 
                                                                                Width="80px"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <table width="100%">
                                                                    <tr>
                                                                        <td>
                                                                            <asp:TextBox ID="QTY_A_2017" runat="server" 
                                                                                BackColor="#CCCCCC" CssClass="textalign" ReadOnly="true" 
                                                                                Text='<%#DataBinder.Eval(Container,"DataItem.A_2017","{0:N0}") %>' 
                                                                                Width="80px"></asp:TextBox>
                                                                          
                                                                            <asp:TextBox ID="QTY_2017" runat="server" 
                                                                                AutoPostBack="True" CssClass="textalign" 
                                                                                OnTextChanged="QTY_2012_TextChanged" 
                                                                                Text='<%#DataBinder.Eval(Container,"DataItem.QTY_2017","{0:N0}") %>' 
                                                                                Width="80px"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="2018">
                                                            <FooterTemplate>
                                                                <table>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:TextBox ID="Lab_QTY_A_2018" runat="server" 
                                                                                BackColor="#CCCCCC" CssClass="textalign" ForeColor="Red" 
                                                                                ReadOnly="True" Width="80px"></asp:TextBox>
                                                                        </td>
                                                                      
                                                                        <td>
                                                                            <asp:TextBox ID="Lab_QTY_2018" runat="server" 
                                                                                BackColor="#CCCCCC" CssClass="textalign" ForeColor="Red" 
                                                                                ReadOnly="True" Width="80px"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </FooterTemplate>
                                                            <%--  <ItemTemplate>
                                                                <asp:TextBox ID="QTY_2018" runat="server" Width="70px" Text='<%#DataBinder.Eval(Container,"DataItem.QTY_2018","{0:N0}") %>' CssClass="textalign" AutoPostBack="True" OnTextChanged="QTY_2012_TextChanged"></asp:TextBox>
                                                            </ItemTemplate>--%>
                                                            <FooterStyle />
                                                            <ItemStyle />
                                                            <HeaderTemplate>
                                                                <table align="center" border="1" 
                                                                    style=" border-color:White" width="100%">
                                                                    <tr>
                                                                        <td align="center" colspan="3" width="100%">
                                                                            <asp:Label ID="Label_2018" runat="server" Text="2018"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="text-align: center">
                                                                            <asp:Label ID="lb_A_2018" runat="server" Text="A" 
                                                                                Width="80px"></asp:Label>
                                                                         
                                                                            <asp:Label ID="lb_F_2018" runat="server" Text="F" 
                                                                                Width="80px"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <table width="100%">
                                                                    <tr>
                                                                        <td>
                                                                            <asp:TextBox ID="QTY_A_2018" runat="server" 
                                                                                BackColor="#CCCCCC" CssClass="textalign" ReadOnly="true" 
                                                                                Text='<%#DataBinder.Eval(Container,"DataItem.A_2018","{0:N0}") %>' 
                                                                                Width="80px"></asp:TextBox>
                                                                           
                                                                            <asp:TextBox ID="QTY_2018" runat="server" 
                                                                                AutoPostBack="True" CssClass="textalign" 
                                                                                OnTextChanged="QTY_2012_TextChanged" 
                                                                                Text='<%#DataBinder.Eval(Container,"DataItem.QTY_2018","{0:N0}") %>' 
                                                                                Width="80px"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="2019">
                                                            <FooterTemplate>
                                                                           <table>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:TextBox ID="Lab_QTY_A_2019" runat="server" 
                                                                                BackColor="#CCCCCC" CssClass="textalign" ForeColor="Red" 
                                                                                ReadOnly="True" Width="80px"></asp:TextBox>
                                                                        </td>
                                                                      
                                                                        <td>
                                                                            <asp:TextBox ID="Lab_QTY_2019" runat="server" 
                                                                                BackColor="#CCCCCC" CssClass="textalign" ForeColor="Red" 
                                                                                ReadOnly="True" Width="80px"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </FooterTemplate>
                                                            <%--    <ItemTemplate>
                                                                <asp:TextBox ID="QTY_2019" runat="server" Width="70px" Text='<%#DataBinder.Eval(Container,"DataItem.QTY_2019","{0:N0}") %>' CssClass="textalign" AutoPostBack="True" OnTextChanged="QTY_2012_TextChanged"></asp:TextBox>
                                                            </ItemTemplate>--%>
                                                            <FooterStyle />
                                                            <ItemStyle />
                                                            <HeaderTemplate>
                                                                <table align="center" border="1" 
                                                                    style=" border-color:White" width="100%">
                                                                    <tr>
                                                                        <td align="center" width="100%">
                                                                            <asp:Label ID="Label_2019" runat="server" Text="2019"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="text-align: center">
                                                                            <asp:Label ID="lb_A_2019" runat="server" Text="A" 
                                                                                Width="80px"></asp:Label>
                                                                           
                                                                            <asp:Label ID="lb_F_2019" runat="server" Text="F" 
                                                                                Width="80px"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <table width="100%">
                                                                    <tr>
                                                                        <td>
                                                                            <asp:TextBox ID="QTY_A_2019" runat="server" 
                                                                                BackColor="#CCCCCC" CssClass="textalign" ReadOnly="true" 
                                                                                Text='<%#DataBinder.Eval(Container,"DataItem.A_2019","{0:N0}") %>' 
                                                                                Width="80px"></asp:TextBox>
                                                                           
                                                                            <asp:TextBox ID="QTY_2019" runat="server" 
                                                                                AutoPostBack="True" CssClass="textalign" 
                                                                                OnTextChanged="QTY_2012_TextChanged" 
                                                                                Text='<%#DataBinder.Eval(Container,"DataItem.QTY_2019","{0:N0}") %>' 
                                                                                Width="80px"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="2020">
                                                            <FooterTemplate>
                                                                <table>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:TextBox ID="Lab_QTY_A_2020" runat="server" 
                                                                                BackColor="#CCCCCC" CssClass="textalign" ForeColor="Red" 
                                                                                ReadOnly="True" Width="80px"></asp:TextBox>
                                                                        </td>
                                                                       
                                                                        <td>
                                                                            <asp:TextBox ID="Lab_QTY_2020" runat="server" 
                                                                                BackColor="#CCCCCC" CssClass="textalign" ForeColor="Red" 
                                                                                ReadOnly="True" Width="80px"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </FooterTemplate>
                                                            <%--   <ItemTemplate>
                                                                <asp:TextBox ID="QTY_2020" runat="server" Width="70px" Text='<%#DataBinder.Eval(Container,"DataItem.QTY_2020","{0:N0}") %>' CssClass="textalign" AutoPostBack="True" OnTextChanged="QTY_2012_TextChanged"></asp:TextBox>
                                                            </ItemTemplate>--%>
                                                            <FooterStyle />
                                                            <ItemStyle />
                                                            <HeaderTemplate>
                                                                <table align="center" border="1" 
                                                                    style=" border-color:White" width="100%">
                                                                    <tr>
                                                                        <td align="center" colspan="3" width="100%">
                                                                            <asp:Label ID="Label_2020" runat="server" Text="2020"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="text-align: center">
                                                                            <asp:Label ID="lb_A_2020" runat="server" Text="A" 
                                                                                Width="80px"></asp:Label>
                                                                           
                                                                            <asp:Label ID="lb_F_2020" runat="server" Text="F" 
                                                                                Width="80px"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <table width="100%">
                                                                    <tr>
                                                                        <td>
                                                                            <asp:TextBox ID="QTY_A_2020" runat="server" 
                                                                                BackColor="#CCCCCC" CssClass="textalign" ReadOnly="true" 
                                                                                Text='<%#DataBinder.Eval(Container,"DataItem.A_2020","{0:N0}") %>' 
                                                                                Width="80px"></asp:TextBox>
                                                                           
                                                                            <asp:TextBox ID="QTY_2020" runat="server" 
                                                                                AutoPostBack="True" CssClass="textalign" 
                                                                                OnTextChanged="QTY_2012_TextChanged" 
                                                                                Text='<%#DataBinder.Eval(Container,"DataItem.QTY_2020","{0:N0}") %>' 
                                                                                Width="80px"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="2021">
                                                            <FooterTemplate>
                                                                <table>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:TextBox ID="Lab_QTY_A_2021" runat="server" 
                                                                                BackColor="#CCCCCC" CssClass="textalign" ForeColor="Red" 
                                                                                ReadOnly="True" Width="80px"></asp:TextBox>
                                                                        </td>
                                                                       
                                                                        <td>
                                                                            <asp:TextBox ID="Lab_QTY_2021" runat="server" 
                                                                                BackColor="#CCCCCC" CssClass="textalign" ForeColor="Red" 
                                                                                ReadOnly="True" Width="80px"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </FooterTemplate>
                                                            <%--  <ItemTemplate>
                                                                <asp:TextBox ID="QTY_2021" runat="server" Width="70px" Text='<%#DataBinder.Eval(Container,"DataItem.QTY_2021","{0:N0}") %>' CssClass="textalign" AutoPostBack="True" OnTextChanged="QTY_2012_TextChanged"></asp:TextBox>
                                                            </ItemTemplate>--%>
                                                            <FooterStyle />
                                                            <ItemStyle />
                                                            <HeaderTemplate>
                                                                <table align="center" border="1" 
                                                                    style=" border-color:White" width="100%">
                                                                    <tr>
                                                                        <td align="center" colspan="3" width="100%">
                                                                            <asp:Label ID="Label_2021" runat="server" Text="2021"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="text-align: center">
                                                                            <asp:Label ID="lb_A_2021" runat="server" Text="A" 
                                                                                Width="80px"></asp:Label>
                                                                           
                                                                            <asp:Label ID="lb_F_2021" runat="server" Text="F" 
                                                                                Width="80px"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <table width="100%">
                                                                    <tr>
                                                                        <td>
                                                                            <asp:TextBox ID="QTY_A_2021" runat="server" 
                                                                                BackColor="#CCCCCC" CssClass="textalign" ReadOnly="true" 
                                                                                Text='<%#DataBinder.Eval(Container,"DataItem.A_2021","{0:N0}") %>' 
                                                                                Width="80px"></asp:TextBox>
                                                                          
                                                                            <asp:TextBox ID="QTY_2021" runat="server" 
                                                                                AutoPostBack="True" CssClass="textalign" 
                                                                                OnTextChanged="QTY_2012_TextChanged" 
                                                                                Text='<%#DataBinder.Eval(Container,"DataItem.QTY_2021","{0:N0}") %>' 
                                                                                Width="80px"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="2022">
                                                            <FooterTemplate>
                                                                <table>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:TextBox ID="Lab_QTY_A_2022" runat="server" 
                                                                                BackColor="#CCCCCC" CssClass="textalign" ForeColor="Red" 
                                                                                ReadOnly="True" Width="80px"></asp:TextBox>
                                                                        </td>
                                                                        
                                                                        <td>
                                                                            <asp:TextBox ID="Lab_QTY_2022" runat="server" 
                                                                                BackColor="#CCCCCC" CssClass="textalign" ForeColor="Red" 
                                                                                ReadOnly="True" Width="80px"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </FooterTemplate>
                                                            <%--  <ItemTemplate>
                                                                <asp:TextBox ID="QTY_2022" runat="server" Width="70px" Text='<%#DataBinder.Eval(Container,"DataItem.QTY_2022","{0:N0}") %>' CssClass="textalign" AutoPostBack="True" OnTextChanged="QTY_2012_TextChanged"></asp:TextBox>
                                                            </ItemTemplate>--%>
                                                            <FooterStyle />
                                                            <ItemStyle />
                                                            <HeaderTemplate>
                                                                <table align="center" border="1" 
                                                                    style=" border-color:White" width="100%">
                                                                    <tr>
                                                                        <td align="center" colspan="3" width="100%">
                                                                            <asp:Label ID="Label_2022" runat="server" Text="2022"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="text-align: center">
                                                                            <asp:Label ID="lb_A_2022" runat="server" Text="A" 
                                                                                Width="80px"></asp:Label>
                                                                            
                                                                            <asp:Label ID="lb_F_2022" runat="server" Text="F" 
                                                                                Width="80px"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <table width="100%">
                                                                    <tr>
                                                                        <td>
                                                                            <asp:TextBox ID="QTY_A_2022" runat="server" 
                                                                                BackColor="#CCCCCC" CssClass="textalign" ReadOnly="true" 
                                                                                Text='<%#DataBinder.Eval(Container,"DataItem.A_2022","{0:N0}") %>' 
                                                                                Width="80px"></asp:TextBox>
                                                                           
                                                                            <asp:TextBox ID="QTY_2022" runat="server" 
                                                                                AutoPostBack="True" CssClass="textalign" 
                                                                                OnTextChanged="QTY_2012_TextChanged" 
                                                                                Text='<%#DataBinder.Eval(Container,"DataItem.QTY_2022","{0:N0}") %>' 
                                                                                Width="80px"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="2023">
                                                            <FooterTemplate>
                                                                <table>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:TextBox ID="Lab_QTY_A_2023" runat="server" 
                                                                                BackColor="#CCCCCC" CssClass="textalign" ForeColor="Red" 
                                                                                ReadOnly="True" Width="80px"></asp:TextBox>
                                                                        </td>
                                                                       
                                                                        <td>
                                                                            <asp:TextBox ID="Lab_QTY_2023" runat="server" 
                                                                                BackColor="#CCCCCC" CssClass="textalign" ForeColor="Red" 
                                                                                ReadOnly="True" Width="80px"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </FooterTemplate>
                                                            <%-- <ItemTemplate>
                                                                <asp:TextBox ID="QTY_2023" runat="server" Width="70px" Text='<%#DataBinder.Eval(Container,"DataItem.QTY_2023","{0:N0}") %>' CssClass="textalign" AutoPostBack="True" OnTextChanged="QTY_2012_TextChanged"></asp:TextBox>
                                                            </ItemTemplate>--%>
                                                            <FooterStyle />
                                                            <ItemStyle />
                                                            <HeaderTemplate>
                                                                <table align="center" border="1" 
                                                                    style=" border-color:White" width="100%">
                                                                    <tr>
                                                                        <td align="center" colspan="3" width="100%">
                                                                            <asp:Label ID="Label_2023" runat="server" Text="2023"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="text-align: center">
                                                                            <asp:Label ID="lb_A_2023" runat="server" Text="A" 
                                                                                Width="80px"></asp:Label>
                                                                            
                                                                            <asp:Label ID="lb_F_2023" runat="server" Text="F" 
                                                                                Width="80px"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <table width="100%">
                                                                    <tr>
                                                                        <td>
                                                                            <asp:TextBox ID="QTY_A_2023" runat="server" 
                                                                                BackColor="#CCCCCC" CssClass="textalign" ReadOnly="true" 
                                                                                Text='<%#DataBinder.Eval(Container,"DataItem.A_2023","{0:N0}") %>' 
                                                                                Width="80px"></asp:TextBox>
                                                                            
                                                                            <asp:TextBox ID="QTY_2023" runat="server" 
                                                                                AutoPostBack="True" CssClass="textalign" 
                                                                                OnTextChanged="QTY_2012_TextChanged" 
                                                                                Text='<%#DataBinder.Eval(Container,"DataItem.QTY_2023","{0:N0}") %>' 
                                                                                Width="80px"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="2024">
                                                            <FooterTemplate>
                                                                <table>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:TextBox ID="Lab_QTY_A_2024" runat="server" 
                                                                                BackColor="#CCCCCC" CssClass="textalign" ForeColor="Red" 
                                                                                ReadOnly="True" Width="80px"></asp:TextBox>
                                                                        </td>
                                                                       
                                                                        <td>
                                                                            <asp:TextBox ID="Lab_QTY_2024" runat="server" 
                                                                                BackColor="#CCCCCC" CssClass="textalign" ForeColor="Red" 
                                                                                ReadOnly="True" Width="80px"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </FooterTemplate>
                                                            <%-- <ItemTemplate>
                                                                <asp:TextBox ID="QTY_2024" runat="server" Width="70px" Text='<%#DataBinder.Eval(Container,"DataItem.QTY_2024","{0:N0}") %>' CssClass="textalign" AutoPostBack="True" OnTextChanged="QTY_2012_TextChanged"></asp:TextBox>
                                                            </ItemTemplate>--%>
                                                            <FooterStyle />
                                                            <ItemStyle />
                                                            <HeaderTemplate>
                                                                <table align="center" border="1" 
                                                                    style=" border-color:White" width="100%">
                                                                    <tr>
                                                                        <td align="center" colspan="3" width="100%">
                                                                            <asp:Label ID="Label_2024" runat="server" Text="2024"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="text-align: center">
                                                                            <asp:Label ID="lb_A_2024" runat="server" Text="A" 
                                                                                Width="80px"></asp:Label>
                                                                          
                                                                            <asp:Label ID="lb_F_2024" runat="server" Text="F" 
                                                                                Width="80px"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <table width="100%">
                                                                    <tr>
                                                                        <td>
                                                                            <asp:TextBox ID="QTY_A_2024" runat="server" 
                                                                                BackColor="#CCCCCC" CssClass="textalign" ReadOnly="true" 
                                                                                Text='<%#DataBinder.Eval(Container,"DataItem.A_2024","{0:N0}") %>' 
                                                                                Width="80px"></asp:TextBox>
                                                                           
                                                                            <asp:TextBox ID="QTY_2024" runat="server" 
                                                                                AutoPostBack="True" CssClass="textalign" 
                                                                                OnTextChanged="QTY_2012_TextChanged" 
                                                                                Text='<%#DataBinder.Eval(Container,"DataItem.QTY_2024","{0:N0}") %>' 
                                                                                Width="80px"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="2025">
                                                            <FooterTemplate>
                                                                <table>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:TextBox ID="Lab_QTY_A_2025" runat="server" 
                                                                                BackColor="#CCCCCC" CssClass="textalign" ForeColor="Red" 
                                                                                ReadOnly="True" Width="80px"></asp:TextBox>
                                                                        </td>
                                                                        
                                                                        <td>
                                                                            <asp:TextBox ID="Lab_QTY_2025" runat="server" 
                                                                                BackColor="#CCCCCC" CssClass="textalign" ForeColor="Red" 
                                                                                ReadOnly="True" Width="80px"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </FooterTemplate>
                                                            <%-- <ItemTemplate>
                                                                <asp:TextBox ID="QTY_2025" runat="server" Width="70px" Text='<%#DataBinder.Eval(Container,"DataItem.QTY_2025","{0:N0}") %>' CssClass="textalign" AutoPostBack="True" OnTextChanged="QTY_2012_TextChanged"></asp:TextBox>
                                                            </ItemTemplate>--%>
                                                            <FooterStyle />
                                                            <ItemStyle />
                                                            <HeaderTemplate>
                                                                <table align="center" border="1" 
                                                                    style=" border-color:White" width="100%">
                                                                    <tr>
                                                                        <td align="center" colspan="3" width="100%">
                                                                            <asp:Label ID="Label_2025" runat="server" Text="2025"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="text-align: center">
                                                                            <asp:Label ID="lb_A_2025" runat="server" Text="A" 
                                                                                Width="80px"></asp:Label>
                                                                         
                                                                            <asp:Label ID="lb_F_2025" runat="server" Text="F" 
                                                                                Width="80px"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <table width="100%">
                                                                    <tr>
                                                                        <td>
                                                                            <asp:TextBox ID="QTY_A_2025" runat="server" 
                                                                                BackColor="#CCCCCC" CssClass="textalign" ReadOnly="true" 
                                                                                Text='<%#DataBinder.Eval(Container,"DataItem.A_2025","{0:N0}") %>' 
                                                                                Width="80px"></asp:TextBox>
                                                                           
                                                                            <asp:TextBox ID="QTY_2025" runat="server" 
                                                                                AutoPostBack="True" CssClass="textalign" 
                                                                                OnTextChanged="QTY_2012_TextChanged" 
                                                                                Text='<%#DataBinder.Eval(Container,"DataItem.QTY_2025","{0:N0}") %>' 
                                                                                Width="80px"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="2026">
                                                            <FooterTemplate>
                                                                <table>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:TextBox ID="Lab_QTY_A_2026" runat="server" 
                                                                                BackColor="#CCCCCC" CssClass="textalign" ForeColor="Red" 
                                                                                ReadOnly="True" Width="80px"></asp:TextBox>
                                                                        </td>
                                                                        
                                                                        <td>
                                                                            <asp:TextBox ID="Lab_QTY_2026" runat="server" 
                                                                                BackColor="#CCCCCC" CssClass="textalign" ForeColor="Red" 
                                                                                ReadOnly="True" Width="80px"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </FooterTemplate>
                                                            <%--<ItemTemplate>
                                                                <asp:TextBox ID="QTY_2026" runat="server" Width="70px" Text='<%#DataBinder.Eval(Container,"DataItem.QTY_2026","{0:N0}") %>' CssClass="textalign" AutoPostBack="True" OnTextChanged="QTY_2012_TextChanged"></asp:TextBox>
                                                            </ItemTemplate>--%>
                                                            <FooterStyle />
                                                            <ItemStyle />
                                                            <HeaderTemplate>
                                                                <table align="center" border="1" 
                                                                    style=" border-color:White" width="100%">
                                                                    <tr>
                                                                        <td align="center" colspan="3" width="100%">
                                                                            <asp:Label ID="Label_2026" runat="server" Text="2026"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="text-align: center">
                                                                            <asp:Label ID="lb_A_2026" runat="server" Text="A" 
                                                                                Width="80px"></asp:Label>
                                                                           
                                                                            <asp:Label ID="lb_F_2026" runat="server" Text="F" 
                                                                                Width="80px"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <table width="100%">
                                                                    <tr>
                                                                        <td>
                                                                            <asp:TextBox ID="QTY_A_2026" runat="server" 
                                                                                BackColor="#CCCCCC" CssClass="textalign" ReadOnly="true" 
                                                                                Text='<%#DataBinder.Eval(Container,"DataItem.A_2026","{0:N0}") %>' 
                                                                                Width="80px"></asp:TextBox>
                                                                           
                                                                            <asp:TextBox ID="QTY_2026" runat="server" 
                                                                                AutoPostBack="True" CssClass="textalign" 
                                                                                OnTextChanged="QTY_2012_TextChanged" 
                                                                                Text='<%#DataBinder.Eval(Container,"DataItem.QTY_2026","{0:N0}") %>' 
                                                                                Width="80px"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="2027">
                                                            <FooterTemplate>
                                                                <table>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:TextBox ID="Lab_QTY_A_2027" runat="server" 
                                                                                BackColor="#CCCCCC" CssClass="textalign" ForeColor="Red" 
                                                                                ReadOnly="True" Width="80px"></asp:TextBox>
                                                                        </td>
                                                                       
                                                                        <td>
                                                                            <asp:TextBox ID="Lab_QTY_2027" runat="server" 
                                                                                BackColor="#CCCCCC" CssClass="textalign" ForeColor="Red" 
                                                                                ReadOnly="True" Width="80px"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </FooterTemplate>
                                                            <%--  <ItemTemplate>
                                                                <asp:TextBox ID="QTY_2027" runat="server" Width="70px" Text='<%#DataBinder.Eval(Container,"DataItem.QTY_2027","{0:N0}") %>' CssClass="textalign" AutoPostBack="True" OnTextChanged="QTY_2012_TextChanged"></asp:TextBox>
                                                            </ItemTemplate>--%>
                                                            <FooterStyle />
                                                            <ItemStyle />
                                                            <HeaderTemplate>
                                                                <table align="center" border="1" 
                                                                    style=" border-color:White" width="100%">
                                                                    <tr>
                                                                        <td align="center" colspan="3" width="100%">
                                                                            <asp:Label ID="Label_2027" runat="server" Text="2027"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="text-align: center">
                                                                            <asp:Label ID="lb_A_2027" runat="server" Text="A" 
                                                                                Width="80px"></asp:Label>
                                                                          
                                                                            <asp:Label ID="lb_F_2027" runat="server" Text="F" 
                                                                                Width="80px"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <table width="100%">
                                                                    <tr>
                                                                        <td>
                                                                            <asp:TextBox ID="QTY_A_2027" runat="server" 
                                                                                BackColor="#CCCCCC" CssClass="textalign" ReadOnly="true" 
                                                                                Text='<%#DataBinder.Eval(Container,"DataItem.A_2027","{0:N0}") %>' 
                                                                                Width="80px"></asp:TextBox>
                                                                           
                                                                            <asp:TextBox ID="QTY_2027" runat="server" 
                                                                                AutoPostBack="True" CssClass="textalign" 
                                                                                OnTextChanged="QTY_2012_TextChanged" 
                                                                                Text='<%#DataBinder.Eval(Container,"DataItem.QTY_2027","{0:N0}") %>' 
                                                                                Width="80px"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="2028">
                                                            <FooterTemplate>
                                                                <table>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:TextBox ID="Lab_QTY_A_2028" runat="server" 
                                                                                BackColor="#CCCCCC" CssClass="textalign" ForeColor="Red" 
                                                                                ReadOnly="True" Width="80px"></asp:TextBox>
                                                                        </td>
                                                                       
                                                                        <td>
                                                                            <asp:TextBox ID="Lab_QTY_2028" runat="server" 
                                                                                BackColor="#CCCCCC" CssClass="textalign" ForeColor="Red" 
                                                                                ReadOnly="True" Width="80px"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </FooterTemplate>
                                                            <%--   <ItemTemplate>
                                                                <asp:TextBox ID="QTY_2028" runat="server" Width="70px" Text='<%#DataBinder.Eval(Container,"DataItem.QTY_2028","{0:N0}") %>' CssClass="textalign" AutoPostBack="True" OnTextChanged="QTY_2012_TextChanged"></asp:TextBox>
                                                            </ItemTemplate>--%>
                                                            <FooterStyle />
                                                            <ItemStyle />
                                                            <HeaderTemplate>
                                                                <table align="center" border="1" 
                                                                    style=" border-color:White" width="100%">
                                                                    <tr>
                                                                        <td align="center" colspan="3" width="100%">
                                                                            <asp:Label ID="Label_2028" runat="server" Text="2028"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="text-align: center">
                                                                            <asp:Label ID="lb_A_2028" runat="server" Text="A" 
                                                                                Width="80px"></asp:Label>
                                                                          
                                                                            <asp:Label ID="lb_F_2028" runat="server" Text="F" 
                                                                                Width="80px"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <table width="100%">
                                                                    <tr>
                                                                        <td>
                                                                            <asp:TextBox ID="QTY_A_2028" runat="server" 
                                                                                BackColor="#CCCCCC" CssClass="textalign" ReadOnly="true" 
                                                                                Text='<%#DataBinder.Eval(Container,"DataItem.A_2028","{0:N0}") %>' 
                                                                                Width="80px"></asp:TextBox>
                                                                           
                                                                            <asp:TextBox ID="QTY_2028" runat="server" 
                                                                                AutoPostBack="True" CssClass="textalign" 
                                                                                OnTextChanged="QTY_2012_TextChanged" 
                                                                                Text='<%#DataBinder.Eval(Container,"DataItem.QTY_2028","{0:N0}") %>' 
                                                                                Width="80px"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <FooterTemplate>
                                                            <asp:Button ID="btnAddQTY" runat="server" CommandName="addQTY" ForeColor="#3333FF" Text="添加" />
                                                        </FooterTemplate>
                                                        <ItemTemplate>
                                                            <asp:Button ID="btnDelQTY" runat="server" Text="删除" 
                                                                CommandName="delQTY" 
                                                                CommandArgument='<%#Container.DataItemIndex %>' 
                                                                ForeColor="#6600FF"  />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="Black" />
                                                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                <EditRowStyle BackColor="#2461BF" />
                                                <AlternatingRowStyle BackColor="White" />
                                            </asp:GridView>
                                            <div>
                                                <div id="ShowNumHeader" style="width:50%; display:inline; float:left">.</div>
                                                <div id="divShowNumByMonth" style="width:50%; text-align:left ;display:inline; float:left"  ></div>
                                            </div>
                                        </asp:Panel>
                                        
                                       
                                    </td>
                                </tr>


                                   <tr>
                                    <td align="right"  Width="900px">
                                        <asp:Button ID="BTN_Quantity_update" runat="server" class="btn btn-primary" Style="height: 35px; width: 130px" Text="修改产品预测信息" OnClick="BTN_Quantity_update_Click"  />
                                    </td>
                                    <td align="right">
                                        &nbsp;</td>
                                </tr>


                                   <tr>
                                    <td align="right"  Width="900px">
                                            <asp:Button ID="BTN_Sales_submit" runat="server" class="btn btn-primary " Style="height: 35px; width: 130px" Text="提交" ValidationGroup="request" OnClick="BTN_Sales_sub_Click" />
                                    </td>
                                    <td align="right">
                                        &nbsp;</td>
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
                        <strong>操作日志</strong><span class="caret"></span>
                    </div>
                    <%-- <ItemTemplate>
                                                                <asp:TextBox ID="QTY_2015" runat="server" Width="70px" Text='<%#DataBinder.Eval(Container,"DataItem.QTY_2015","{0:N0}") %>' CssClass="textalign" AutoPostBack="True" OnTextChanged="QTY_2012_TextChanged"></asp:TextBox>
                                                            </ItemTemplate>--%>
                    <div class="panel-body <% =ViewState["lv"].ToString() == "CZRZ" ? "" : "collapse" %>" id="CZRZ">

                        <div class="col-xs-12 col-sm-12  col-md-12 col-lg-12">
                            <table style="height: 35px; width: 100%">
                                <tr>
                                    <td>一.操作人员记录</td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:GridView ID="gv_rz1" Width="100%"
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
                                                <asp:BoundField DataField="Update_Engineer" HeaderText="操作角色"
                                                    ReadOnly="True">
                                                    <HeaderStyle Wrap="True" />
                                                    <ItemStyle Width="80px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Update_user" HeaderText="操作工号"
                                                    ReadOnly="True">
                                                    <HeaderStyle Wrap="True" />
                                                    <ItemStyle Width="80px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Update_username" HeaderText="姓名"
                                                    ReadOnly="True">
                                                    <HeaderStyle Wrap="True" />
                                                    <ItemStyle Width="80px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Update_LB" HeaderText="操作类别"
                                                    ReadOnly="True">
                                                    <HeaderStyle Wrap="True" />
                                                    <ItemStyle Width="80px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Commit_time" HeaderText="处理时间"
                                                    ReadOnly="True">
                                                    <HeaderStyle Wrap="True" />
                                                    <ItemStyle Width="120px" />
                                                </asp:BoundField>                                              
                                                <asp:BoundField DataField="Update_content" HeaderText="操作事项">
                                                    <HeaderStyle Wrap="False" />
                                                </asp:BoundField>
                                            </Columns>
                                        </asp:GridView>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div class="col-md-2 ">
        <div class="panel panel-info">
            <div class="panel-heading">
                <strong>日志:</strong>
            </div>
            <div class="panel-body  " id="DDXX">
                <div>
                    <div class="">
                        <table border="1" width="100%">
                    
                            <tr>
                                <td colspan="2" style="background-color: lightblue"><asp:Label ID="Lab_htzt" runat="server" Font-Size="Large" Font-Underline="False" ForeColor="#6600CC" Visible="False"></asp:Label>
                                    </td>
                            </tr>
                            <tr>
                                <td colspan="2">

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
                                            <asp:BoundField DataField="Commit_time" HeaderText="操作时间">
                                                <HeaderStyle Wrap="False" />
                                                <ItemStyle Width="30%" />
                                            </asp:BoundField>
                                        </Columns>
                                    </asp:GridView>
                                </td>

                            </tr>
                        </table>
    
                        <br />
                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
