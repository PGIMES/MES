<%@ Page Title="MES生产管理系统" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="Daoju.aspx.cs" Inherits="Daoju_Daoju" MaintainScrollPositionOnPostback="True" ValidateRequest="false"  enableEventValidation="false"%>
<%@ Register assembly="DevExpress.Web.v17.2, Version=17.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web" tagprefix="dx" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <link href="../Content/css/bootstrap.min.css" rel="stylesheet" />
    <script src="../Content/js/jquery.min.js"></script>
    <script src="../Content/js/bootstrap.min.js"></script>
    <script src="../Content/js/layer/layer.js"></script>
    <script src="../Content/js/plugins/layer/laydate/laydate.js"></script>
    <link href="../Content/js/plugins/bootstrap-select/css/bootstrap-select.min.css" rel="stylesheet" />
    <script src="../Content/js/plugins/bootstrap-select/js/bootstrap-select.min.js"></script>

    <script type="text/javascript">
      

        $(document).ready(function () {
            $("#mestitle").html("【PGI刀具清单申表】<a href='/userguide/reviewGuide.pptx' target='_blank' class='h4' style='display:none'>使用说明</a>");


        })
    </script>
    <script type="text/javascript">
        var popupwindow = null;
        function GetXMH() {
            popupwindow = window.open('../Select/select_XMLJ.aspx', '_blank', 'height=500,width=800,resizable=no,menubar=no,scrollbars =no,location=no');
        }
        function GetPgi_Product() {
            popupwindow = window.open('../Select/select_product.aspx?ctrl1=pgi_no', '_blank', 'height=500,width=800,resizable=no,menubar=no,scrollbars =no,location=no');
        }
        function setvalue_product(lspgi_no, lspn, lspn_desc, lsdomain, lsproduct_user, lsproduct_dept, lsstatus, lscailiao, lsnyl, lsline, lsver) {

            $("input[id*='pgi_no']").val(lspgi_no);
            $("input[id*='pn']").val(lspn);
            $("input[id*='pn_desc']").val(lspn_desc);
            $("input[id*='domain']").val(lsdomain);
            $("input[id*='product_user']").val(lsproduct_user);
            $("input[id*='dept']").val(lsproduct_dept);
            $("input[id*='status']").val(lsstatus);
            $("input[id*='sku']").val(lscailiao);
            $("input[id*='year_num']").val(lsnyl);
            $("input[id*='line']").val(lsline);
            $("input[id*='ver']").val(lsver);
            popupwindow.close();
            //$("input[id*='" + ctrl0 + "']").change();

            $.ajax({
                type: "post",
                url: "daoju.aspx/GetGx",
                data: "{'lspgi_no':'" + lspgi_no + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    if (data.d != "") //返回的数据用data.d获取内容Logaction + "成功."
                    {
                       
                        $('#MainContent_op').html("");
                        $('#MainContent_op').append("<option value=''> </option>");
                        $.each(eval(data.d), function (i) {
                           
                            $('#MainContent_op').append("<option value=" + eval(data.d)[i].value + ">" + eval(data.d)[i].value + "</option>");
                          
                        });
                    }
                    else {
                        alert("失败.");
                    }
                }

            });
        }


        function Getgx_name() {

           // alert('xxxx');
           // $("input[id*='MainContent_op_desc']").val("aaaaaa");
            //  $("input[id*='MainContent_op_desc']").val($("#MainContent_op").find("option:selected").val());

            $.ajax({
                type: "post",
                url: "daoju.aspx/GetGxms",
                data: "{'lspgi_no':'" + $("input[id*='pgi_no']").val() + "','lsop':'" + $('#MainContent_op').val() + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    if (data.d != "") //返回的数据用data.d获取内容Logaction + "成功."
                    {

                       // alert(data.d);
                        $("input[id*='MainContent_op_desc']").val(data.d);
                    }
                    else {
                        alert("失败.");
                    }
                }

            });
            
        }
        function Getdaoju(e)
        {
            
            var ss = e.id.split("_");// 在每个逗号(,)处进行分解。
            var url = "../select/sz_report_dev_select.aspx?id=" + ss[6] + "&domain=" + $("input[id*='domain']").val()
           
            popupwindow = window.open(url, '_blank', 'height=500,width=1000,resizable=yes,menubar=no,scrollbars =yes,location=no');
        }
        function setvalue_dj(id, wlh, ms, lx, js, sm, pp, gys)
        {
            $("input[id*='MainContent_gv_cell" + id + "_3_daoju_no1_" + id + "_I']").val(wlh);
            $("input[id*='MainContent_gv_cell" + id + "_4_daoju_desc1_" + id + "_I']").val(ms);
            $("input[id*='MainContent_gv_cell" + id + "_5_daoju_type_" + id + "_I']").val(lx);
            $("input[id*='MainContent_gv_cell" + id + "_6_jiaoshu_" + id + "_I']").val(js);
            $("input[id*='MainContent_gv_cell" + id + "_8_edsm_" + id + "_I']").val(sm);
            $("input[id*='MainContent_gv_cell" + id + "_9_smtzxs_" + id + "_I']").val(1);
            $("input[id*='MainContent_gv_cell" + id + "_12_brand_" + id + "_I']").val(pp);
            $("input[id*='MainContent_gv_cell" + id + "_13_supplier_" + id + "_I']").val(gys);
            popupwindow.close();
        }

        function setvalue(ctrl0, keyValue0, ctrl1, keyValue1, ctrl2, keyValue2) {

            $("input[id*='MainContent_gv_cell0_3_daoju_no1_0_I']").val(keyValue0);
            //$("input[id*='" + ctrl1 + "']").val(keyValue1);
            //$("input[id*='" + ctrl2 + "']").val(keyValue2);
            popupwindow.close();
           // $("input[id*='" + ctrl0 + "']").change();
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

</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div class="col-md-12">
        <div class="row row-container" >
            <div class="col-md-12">
                <div class="panel panel-info">
                    <div class="panel-heading" data-toggle="collapse" data-target="#SQXX">
                        <strong>申请人信息</strong>
                    </div>
                    <div class="panel-body <% =ViewState["lv"].ToString() == "SQXX" ? "" : "collapse" %>" id="SQXX">
                        <div class="col-xs-12 col-sm-12  col-md-12 col-lg-12" style="width:1000px;">
                            <div class="">
                                <asp:UpdatePanel ID="UpdatePanel_request" runat="server">
                                    <ContentTemplate>
                                        <table style="height: 30px; width: 100%">
                                            <tr>
                                                <td>申请人
                                                </td>
                                                <td>
                                                    <div class="form-inline">
                                                        <input id="txt_CreateById" class="form-control input-s-sm" style="height: 30px; width: 100px;font-size:12px;" runat="server" readonly="True"  />
                                                        <input id="txt_CreateByName" class="form-control input-s-sm" style="height: 30px; width: 100px;font-size:12px;" runat="server" readonly="True" />
                                                        <input id="txt_CreateByAd" class="form-control input-s-sm" style="height: 30px; width: 100px; font-size:12px;display: none;font-size:12px;" runat="server" readonly="True" />
                                                    </div>
                                                </td>
                                                <td>部门
                                                </td>
                                                <td>
                                                    <input id="txt_CreateByDept" class="form-control input-s-sm" style="height: 30px; width: 100px;font-size:12px;" runat="server" readonly="True" />
                                                </td>
                                                <td style="display: none">部门经理
                                                </td>
                                                <td>
                                                    <div class="form-inline" style="display: none">
                                                        <input id="txt_managerid" class="form-control input-s-sm" style="height: 30px; width: 100px;font-size:12px;" runat="server" readonly="True" />
                                                        <input id="txt_manager" class="form-control input-s-sm" style="height: 30px; width: 100px;font-size:12px;" runat="server" readonly="True" />
                                                        <input id="txt_manager_AD" class="form-control input-s-sm" style="height: 30px; width: 100px;font-size:12px;" runat="server" readonly="True" />
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>当前登陆人员</td>
                                                <td>
                                                    <div class="form-inline">
                                                        <input id="txt_LogUserId" class="form-control input-s-sm" style="height: 30px; width: 100px;font-size:12px;" runat="server" readonly="True" />
                                                        <input id="txt_LogUserName" class="form-control input-s-sm" style="height: 30px; width: 100px;font-size:12px;" runat="server" readonly="True" />/
                                                        <input id="txt_LogUserJob" class="form-control input-s-sm" style="height: 30px; width: 100px;font-size:12px;" runat="server" readonly="True" />
                                                        <input id="txt_LogUserDept" class="form-control input-s-sm" style="height: 30px; width: 100px;font-size:12px;" runat="server" readonly="True" />
                                                    </div>
                                                </td>
                                                <td>申请日期：</td>
                                                <td>
                                                    <input id="txt_CreateDate" class="form-control input-s-sm" style="height: 30px; width: 100px;font-size:12px;" runat="server" readonly="True" />
                                                </td>
                                                <td></td>
                                                <td>
                                                    <input id="txt_Code" class="form-control input-s-sm" style="height: 30px; width: 200px; display: none; font-size:12px;" runat="server" readonly="True" />
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
            <div class="col-md-12" >
                <div class="panel panel-info">
                    <div class="panel-heading" data-toggle="collapse" data-target="#CPXX">
                        <strong>产品工序信息</strong>
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


        <div class="row  row-container">
            <div class="col-md-12" >
                <div class="panel panel-info">
                    <div class="panel-heading" data-toggle="collapse" data-target="#gscs">
                        <strong style="padding-right: 100px">刀具清单信息</strong>
                    </div>
                    <div class="panel-body  collapse in" id="gscs">
                        <div class="col-xs-12 col-sm-12  col-md-12 col-lg-12">
                            <div> 
                                <asp:UpdatePanel runat="server" ID="p1" UpdateMode="Conditional">
                                    <ContentTemplate>
                                <dx:aspxgridview ID="gv" runat="server" AutoGenerateColumns="False" Width="525px" KeyFieldName="id" OnRowCommand="gv_RowCommand" Theme="MetropolisBlue" TabIndex="100" >
            <SettingsPager PageSize="100">
            </SettingsPager>
            <SettingsBehavior AllowCellMerge="true"  AllowFocusedRow="True" AllowSelectByRowClick="True" AllowDragDrop="False" AllowSort="False" />
            <Columns>
               
                 <dx:gridviewdatatextcolumn FieldName="daoju_no" VisibleIndex="4"  Caption="刀具号">
                     <Settings AllowCellMerge="True" />
                    <DataItemTemplate>                
                        <dx:ASPxTextBox ID="daoju_no" Width="40" runat="server" Value='<%# Eval("daoju_no")%>'></dx:ASPxTextBox>                
                    </DataItemTemplate>                        
                 </dx:GridViewDataTextColumn>

                <dx:gridviewdatatextcolumn FieldName="daoju_desc" VisibleIndex="5"  Caption="加工描述">
                    <Settings AllowCellMerge="True" />
                    <DataItemTemplate>                
                        <dx:ASPxTextBox ID="daoju_desc" Width="80" runat="server" Value='<%# Eval("daoju_desc")%>' ></dx:ASPxTextBox>                
                    </DataItemTemplate>                        
                 </dx:GridViewDataTextColumn>


                  <dx:gridviewdatatextcolumn FieldName="length" VisibleIndex="6" Caption="总加工<br>长度(mm)" >
                      <Settings AllowCellMerge="True" />
                    <DataItemTemplate>              
                        <dx:ASPxTextBox ID="length" Width="55" runat="server" Value='<%# Eval("length")%>' AutoPostBack="true"   OnValueChanged="length_TextChanged">
                      </dx:ASPxTextBox>                
                    </DataItemTemplate>                        
                 </dx:GridViewDataTextColumn>

                 <dx:gridviewdatatextcolumn FieldName="daoju_no1" VisibleIndex="7" Caption="刀具编号">
                     <Settings AllowCellMerge="False" />
                    <DataItemTemplate>                
                        <dx:ASPxTextBox ID="daoju_no1" Width="75" runat="server" Value='<%# Eval("daoju_no1")%>'    onclick='Getdaoju(this);'></dx:ASPxTextBox>                
                    </DataItemTemplate>                        
                 </dx:GridViewDataTextColumn>

                 <dx:gridviewdatatextcolumn FieldName="daoju_desc1" VisibleIndex="8" Caption="刀具规格描述">
                     <Settings AllowCellMerge="False" />
                    <DataItemTemplate>                
                        <dx:ASPxTextBox ID="daoju_desc1" Width="140" runat="server" Value='<%# Eval("daoju_desc1")%>'  ReadOnly="true" BackColor="LightGray"></dx:ASPxTextBox>                
                    </DataItemTemplate>                        
                 </dx:GridViewDataTextColumn>

                 <dx:gridviewdatatextcolumn FieldName="daoju_type" VisibleIndex="8" Caption="刀具类型">
                     <Settings AllowCellMerge="False" />
                    <DataItemTemplate>                
                        <dx:ASPxTextBox ID="daoju_type" Width="60" runat="server" Value='<%# Eval("daoju_type")%>'  ReadOnly="true" BackColor="LightGray"></dx:ASPxTextBox>                
                    </DataItemTemplate>                        
                 </dx:GridViewDataTextColumn>

                 <dx:gridviewdatatextcolumn FieldName="jiaoshu" VisibleIndex="9" Caption="角数">
                     <Settings AllowCellMerge="False" />
                    <DataItemTemplate>                
                        <dx:ASPxTextBox ID="jiaoshu" Width="40" runat="server" Value='<%# Eval("jiaoshu")%>' ReadOnly="true" BackColor="LightGray"></dx:ASPxTextBox>                
                    </DataItemTemplate>                        
                 </dx:GridViewDataTextColumn>

                 <dx:gridviewdatatextcolumn FieldName="dyl" VisibleIndex="10" Caption="刀具<br>用量">
                     <Settings AllowCellMerge="False" />
                    <DataItemTemplate>                
                        <dx:ASPxTextBox ID="dyl" Width="40" runat="server" Value='<%# Eval("dyl")%>'  AutoPostBack="true"   OnValueChanged="length_TextChanged"></dx:ASPxTextBox>                
                    </DataItemTemplate>                        
                 </dx:GridViewDataTextColumn>

                 <dx:gridviewdatatextcolumn FieldName="edsm" VisibleIndex="10" Caption="额定寿命(m)">
                     <Settings AllowCellMerge="False" />
                    <DataItemTemplate>                
                        <dx:ASPxTextBox ID="edsm" Width="60" runat="server" Value='<%# Eval("edsm")%>' ReadOnly="true" BackColor="LightGray"></dx:ASPxTextBox>                
                    </DataItemTemplate>                        
                 </dx:GridViewDataTextColumn>

                   <dx:gridviewdatatextcolumn FieldName="smtzxs" VisibleIndex="11" Caption="寿命调<br>整系数">
                       <Settings AllowCellMerge="False" />
                    <DataItemTemplate>                
                        <dx:ASPxTextBox ID="smtzxs" Width="55" runat="server" Value='<%# Eval("smtzxs")%>'  AutoPostBack="true"   OnValueChanged="length_TextChanged"></dx:ASPxTextBox>                
                    </DataItemTemplate>                        
                 </dx:GridViewDataTextColumn>

                 <dx:gridviewdatatextcolumn FieldName="djedsm" VisibleIndex="12" Caption="单角额<br>定寿命">
                     <Settings AllowCellMerge="False" />
                    <DataItemTemplate>                
                        <dx:ASPxTextBox ID="djedsm" Width="55" runat="server" Value='<%# Eval("djedsm")%>'  ReadOnly="true" BackColor="LightGray"></dx:ASPxTextBox>                
                    </DataItemTemplate>                        
                 </dx:GridViewDataTextColumn>

                 <dx:gridviewdatatextcolumn FieldName="djedsm_old" VisibleIndex="12" Caption="原单角额<br>定寿命" Visible="false">
                     <Settings AllowCellMerge="False" />
                    <DataItemTemplate>                
                        <dx:ASPxTextBox ID="djedsm_old" Width="60" runat="server" Value='<%# Eval("djedsm_old")%>'  ReadOnly="true" BackColor="LightGray"></dx:ASPxTextBox>                
                    </DataItemTemplate>                        
                 </dx:GridViewDataTextColumn>

                <dx:gridviewdatatextcolumn FieldName="brand" VisibleIndex="13" Caption="品牌" >
                    <Settings AllowCellMerge="False" />
                    <DataItemTemplate>                
                        <dx:ASPxTextBox ID="brand" Width="70" runat="server" Value='<%# Eval("brand")%>'  ReadOnly="true" BackColor="LightGray"></dx:ASPxTextBox>                
                    </DataItemTemplate>                        
                 </dx:GridViewDataTextColumn>

                 <dx:gridviewdatatextcolumn FieldName="supplier" VisibleIndex="14" Caption="供应商">
                     <Settings AllowCellMerge="False" />
                    <DataItemTemplate>                
                        <dx:ASPxTextBox ID="supplier" Width="210" runat="server" Value='<%# Eval("supplier")%>'  ReadOnly="true" BackColor="LightGray"></dx:ASPxTextBox>                
                    </DataItemTemplate>                        
                 </dx:GridViewDataTextColumn>

                 
               

                 <dx:GridViewDataTextColumn FieldName="" VisibleIndex="20" Caption=" " >
                     <Settings AllowCellMerge="False" />
                    <DataItemTemplate>                
                        <dx:ASPxButton ID="btn" runat="server" Text="新增行"   CommandName="Add"  ></dx:ASPxButton>          
                    </DataItemTemplate>                        
                 </dx:GridViewDataTextColumn>
               

                <dx:GridViewDataTextColumn FieldName="daoju_no" VisibleIndex="21" Caption=" "  >
                    <Settings AllowCellMerge="True" />
                    <DataItemTemplate>                
                        <dx:ASPxButton ID="btn1" runat="server" Text="新增组"   CommandName="Add1"  ></dx:ASPxButton>          
                    </DataItemTemplate>                        
                 </dx:GridViewDataTextColumn>


                <%-- <dx:GridViewDataTextColumn FieldName="txt4" VisibleIndex="8" Caption="新增" Width="1px" >
                    <DataItemTemplate>                
                        <asp:HyperLink ID="HyperLink1" runat="server">HyperLink</asp:HyperLink>       
                    </DataItemTemplate>                        
                 </dx:GridViewDataTextColumn>--%>
               
                 <dx:GridViewDataTextColumn FieldName="id" VisibleIndex="15" Width="0px">
                     <HeaderStyle CssClass="hidden" />
                     <CellStyle CssClass="hidden">
                     </CellStyle>
                     <FooterCellStyle CssClass="hidden">
                     </FooterCellStyle>
                 </dx:GridViewDataTextColumn>

                <dx:GridViewDataTextColumn FieldName="daoju_id" VisibleIndex="16" Width="0px">
                     <HeaderStyle CssClass="hidden" />
                     <CellStyle CssClass="hidden">
                     </CellStyle>
                     <FooterCellStyle CssClass="hidden">
                     </FooterCellStyle>
                 </dx:GridViewDataTextColumn>

                <dx:GridViewDataTextColumn FieldName="pgi_no" VisibleIndex="17" Width="0px">
                     <HeaderStyle CssClass="hidden" />
                     <CellStyle CssClass="hidden">
                     </CellStyle>
                     <FooterCellStyle CssClass="hidden">
                     </FooterCellStyle>
                 </dx:GridViewDataTextColumn>

                <dx:GridViewDataTextColumn FieldName="op" VisibleIndex="18" Width="0px">
                     <HeaderStyle CssClass="hidden" />
                     <CellStyle CssClass="hidden">
                     </CellStyle>
                     <FooterCellStyle CssClass="hidden">
                     </FooterCellStyle>
                 </dx:GridViewDataTextColumn>

                <dx:GridViewDataTextColumn FieldName="ver" VisibleIndex="19" Width="0px">
                     <HeaderStyle CssClass="hidden" />
                     <CellStyle CssClass="hidden">
                     </CellStyle>
                     <FooterCellStyle CssClass="hidden">
                     </FooterCellStyle>
                 </dx:GridViewDataTextColumn>

                <dx:GridViewDataTextColumn FieldName="xh" VisibleIndex="20" Width="0px">
                     <HeaderStyle CssClass="hidden" />
                     <CellStyle CssClass="hidden">
                     </CellStyle>
                     <FooterCellStyle CssClass="hidden">
                     </FooterCellStyle>
                 </dx:GridViewDataTextColumn>

                  <dx:GridViewDataTextColumn FieldName="pt_group_part" VisibleIndex="21" Width="0px">
                     <HeaderStyle CssClass="hidden" />
                     <CellStyle CssClass="hidden">
                     </CellStyle>
                     <FooterCellStyle CssClass="hidden">
                     </FooterCellStyle>
                 </dx:GridViewDataTextColumn>
               
            </Columns>
            <Styles>
                <Header BackColor="#1E82CD" ForeColor="White" >
                </Header>
                <SelectedRow BackColor="Red">
                </SelectedRow>
            </Styles>
        </dx:aspxgridview>
                                       </ContentTemplate>
                                    
                                </asp:UpdatePanel>
                                
                                 <table style="width:100%;"><tr style="display: block; margin: 10px 0; text-align:right;">
                                    <td style="display: block; margin: 10px 0; text-align:right;">
                                        
                                        <asp:UpdatePanel runat="server" ID="UpdatePanel1" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:Button ID="Button1" runat="server" Text="提交" class="btn btn-large btn-primary" Width="100px" OnClick="Button1_Click" />
                                        </ContentTemplate></asp:UpdatePanel>
                                        </td>
                                       </tr></table>
                            </div>
                           
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <%--<div class="row  row-container">
            <div class="col-md-12">
                <div class="panel panel-info">
                    <div class="panel-heading" data-toggle="collapse" data-target="#gsjg">
                        <strong style="padding-right: 100px">审批信息</strong>
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
        </div>--%>
       <%-- <div class="row  row-container">
            <div class="col-md-12">
                <div class="panel panel-info">
                    <div class="panel-heading" data-toggle="collapse" data-target="#jgqr">
                        <strong>物料价格数据</strong>
                    </div>
                    <div class="panel-body  collapse in" id="jgqr">
                        <div class="col-xs-12 col-sm-12  col-md-12 col-lg-12">
                            <div>
                                <asp:Table Style="width: 100%" border="0" runat="server" ID="tblWLJiaGe">
                                </asp:Table>
                            </div>
                            <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <script type="text/javascript">
                                        //// function load() {
                                        // Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
                                        // //}

                                        // function EndRequestHandler() {
                                        //     AddSolution();
                                        // }
                                    </script>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
            </div>
        </div>--%>

        <div class="row  row-container">
            <div class="col-md-12" >
                <div class="panel panel-info">
                    <div class="panel-heading" data-toggle="collapse" data-target="#CZRZ">
                        <strong>审批信息</strong>
                    </div>
                    <div class="panel-body collapse" id="CZRZ">
                        <div class="col-xs-12 col-sm-12  col-md-12 col-lg-12">
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

   <%-- <div class="col-md-2 ">
        <div class="panel panel-info">
            <div class="panel-heading">
                <strong>日志:</strong>
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
    </div>--%>



</asp:Content>
