<%@ Page Title="【工艺工时审批单】" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="GYGS.aspx.cs" Inherits="Forms_PgiOp_GYGS" 
     MaintainScrollPositionOnPostback="True" ValidateRequest="false"  enableEventValidation="false" %>

<%@ Register assembly="DevExpress.Web.v17.2, Version=17.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web" tagprefix="dx" %>

<asp:Content ID="HeaderContent" ContentPlaceHolderID="HeadContent" Runat="Server">
    <link href="../../Content/css/bootstrap.min.css" rel="stylesheet" />
    <script src="/Content/js/jquery.min.js"></script>
    <script src="../../Content/js/bootstrap.min.js"></script>
    <script src="../../Content/js/layer/layer.js"></script>

    <script type="text/javascript">
        function getQueryString(name) {
            var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)", "i");
            var r = decodeURI(window.location.search).substr(1).match(reg);
            if (r != null) return unescape(r[2]); return null;
        }
        var option = getQueryString("option");

        $(document).ready(function () {
            $("#mestitle").html("【工艺工时审批单】");

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


            $("input[id*='rbltypeno']").change(function () {  
                  gv_d.PerformCallback();
            }); 

        });

        //提出自定流程 JS 
        function setComment(val) {
            $('#comment', parent.document).val(val);
        }

    </script>
    <script type="text/javascript">
        function GetPgi_Product() 
        {
            popupwindow = window.open('../../Select/select_product.aspx?ctrl1=pgi_no', '_blank', 'height=500,width=800,resizable=no,menubar=no,scrollbars =no,location=no');
        }
        function setvalue_product(lspgi_no, lspn, lspn_desc, lsdomain, lsproduct_user, lsproduct_dept, lsstatus, lscailiao, lsnyl, lsline, lsver) 
        {
            $("input[id*='pn']").val(lspn);
            $("input[id*='pn_desc']").val(lspn_desc);
            $("input[id*='domain']").val(lsdomain);
            $("input[id*='product_user']").val(lsproduct_user);
            $("input[id*='dept']").val(lsproduct_dept);
            $("input[id*='status']").val(lsstatus);
            $("input[id*='sku']").val(lscailiao);
            $("input[id*='year_num']").val(lsnyl);
            $("input[id*='line']").val(lsline);
            if ($("input[id*='ver']").val() == "" || (lspgi_no != $("input[id*='pgi_no']").val())) {
                $("input[id*='ver']").val(lsver);
            }
            $("input[id*='pgi_no']").val(lspgi_no);
            popupwindow.close();

            gv_d.PerformCallback();
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
        #CPXX label{
            font-weight:400;
        }
        .lineread{
            height: 30px; width: 200px;font-size:9px; border:none; border-bottom:1px solid black;
        }
        .linewrite{
            height: 30px; width: 200px;font-size:9px; border:none; border-bottom:1px solid black;background-color:#FDF7D9;/*EFEFEF*/
        }
        
    </style>
</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
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

    <div class="col-md-12" >  

        <div class="row row-container">
            <div class="panel panel-info">
                <div class="panel-heading" data-toggle="collapse" data-target="#SQXX">
                    <strong>申请人信息</strong>
                </div>
                <div class="panel-body <% =ViewState["lv"].ToString() == "SQXX" ? "" : "collapse" %>" id="SQXX">
                    <div class="col-xs-12 col-sm-12  col-md-12 col-lg-12" style="width:1000px;">
                        <asp:UpdatePanel ID="UpdatePanel_request" runat="server">
                            <ContentTemplate>
                                <table style="height: 30px; width: 100%">
                                    <tr>
                                        <td style="width:80px">申请人</td><%--class="form-control input-s-sm"--%>
                                        <td style="width:250px">
                                            <div class="form-inline">
                                                <input id="txt_CreateById" class="lineread"  style="height: 30px; width: 90px;font-size:12px;" runat="server" readonly="True"  />
                                                <input id="txt_CreateByName" class="lineread" style="height: 30px; width: 90px;font-size:12px;" runat="server" readonly="True" />
                                            </div>
                                        </td>
                                        <td style="width:80px">申请部门</td>
                                        <td style="width:250px">
                                            <div class="form-inline">                                                
                                                <input id="txt_CreateByDept" class="lineread" style="height: 30px; width: 180px;font-size:12px;" runat="server" readonly="True" />
                                            </div>
                                        </td>
                                        <td style="width:80px">申请时间</td>
                                        <td style="width:250px">
                                            <input id="txt_CreateDate" class="lineread" style="height: 30px; width: 180px;font-size:12px;" runat="server" readonly="True" />
                                        </td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
        </div>


        <div class="row  row-container">
            <div class="panel panel-info">
                <div class="panel-heading" data-toggle="collapse" data-target="#CPXX">
                        <strong>产品信息</strong>
                </div>
                <div class="panel-body " id="CPXX">
                    <div class="col-xs-12 col-sm-12  col-md-12 col-lg-12" style="width:1000px;">
                        <div>
                            <%--<asp:UpdatePanel ID="UpdatePanel_cpxx" runat="server">
                                <ContentTemplate>--%>
                                    <asp:Table Style="width: 100%;" border="0" runat="server" ID="tblCPXX" Font-Size="12px" >  
                                    </asp:Table>
                                <%--</ContentTemplate>
                        </asp:UpdatePanel>--%>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div class="row  row-container">
            <div class="panel panel-info">
                <div class="panel-heading" data-toggle="collapse" data-target="#GYGS">
                        <strong>工艺工时信息</strong>
                </div>
                <div class="panel-body " id="GYGS">
                    <div class="col-xs-12 col-sm-12  col-md-12 col-lg-12">
                        <div style="padding: 2px 5px 5px 0px">                
                            
                        </div>
                        <asp:UpdatePanel runat="server" ID="p1" UpdateMode="Conditional">
                            <ContentTemplate>
                                <dx:ASPxTextBox ID="JgNum2" Width="50px" runat="server"  AutoPostBack="true"  OnValueChanged="JgNum_ValueChanged" ></dx:ASPxTextBox>

                                <asp:Button ID="btndel" runat="server" Text="删除" class="btn btn-default" style="width:60px; height:32px;"  OnClick="btndel_Click" />
                                 <dx:aspxgridview ID="gv_d" runat="server" AutoGenerateColumns="False" KeyFieldName="numid" Theme="MetropolisBlue" OnCustomCallback="gv_d_CustomCallback" 
                                      OnRowCommand="gv_d_RowCommand" ClientInstanceName="gv_d"  EnableTheming="True"  >                                   
                                    <SettingsPager PageSize="1000"></SettingsPager>
                                    <Settings ShowFooter="True" />
                                    <SettingsBehavior AllowSelectByRowClick="True" AllowDragDrop="False" AllowSort="False" />
                                    <Columns>
                                        <dx:GridViewCommandColumn SelectAllCheckboxMode="Page" ShowClearFilterButton="true" ShowSelectCheckbox="true" Name="Sel" Width="40" VisibleIndex="1"></dx:GridViewCommandColumn>
                                        <dx:GridViewDataTextColumn Caption="工艺段" FieldName="typeno" Width="60px" VisibleIndex="2"></dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="工艺流程" FieldName="pgi_no" Width="80px" VisibleIndex="3"></dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="工序号" FieldName="op" Width="60px" VisibleIndex="4">
                                            <Settings AllowCellMerge="False" />
                                            <DataItemTemplate>                
                                                <dx:ASPxTextBox ID="op" Width="60px" runat="server" Value='<%# Eval("op")%>' ></dx:ASPxTextBox>                
                                            </DataItemTemplate>   
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="工序名称" FieldName="op_desc" Width="140px" VisibleIndex="5">
                                            <Settings AllowCellMerge="False" />
                                            <DataItemTemplate>                
                                                <dx:ASPxTextBox ID="op_desc" Width="140px" runat="server" Value='<%# Eval("op_desc")%>' ></dx:ASPxTextBox>                
                                            </DataItemTemplate>        
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="工序说明" FieldName="op_remark" Width="140px" VisibleIndex="6">
                                            <Settings AllowCellMerge="False" />
                                            <DataItemTemplate>                
                                                <dx:ASPxTextBox ID="op_remark" Width="140px" runat="server" Value='<%# Eval("op_remark")%>' ></dx:ASPxTextBox>                
                                            </DataItemTemplate>        
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="设备" FieldName="gzzx_desc" Width="80px" VisibleIndex="7"></dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="工作中心<br />代码" FieldName="gzzx" Width="50px" VisibleIndex="8"></dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="是否报工<br />(Y/N)" FieldName="IsBg" Width="50px" VisibleIndex="9"></dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="每次加工<br />数量" FieldName="JgNum" Width="50px" VisibleIndex="10">
                                            <Settings AllowCellMerge="False" />
                                            <DataItemTemplate>                
                                                <dx:ASPxTextBox ID="JgNum" Width="50px" runat="server" Value='<%# Eval("JgNum")%>' AutoPostBack="true"  OnValueChanged="JgNum_ValueChanged" ></dx:ASPxTextBox>
                                            </DataItemTemplate>        
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="加工时长<br />(秒)" FieldName="JgSec" Width="50px" VisibleIndex="12">
                                            <Settings AllowCellMerge="False" />
                                            <DataItemTemplate>                
                                                <dx:ASPxTextBox ID="JgSec" Width="50px" runat="server" Value='<%# Eval("JgSec")%>' OnValueChanged="JgSec_TextChanged" ></dx:ASPxTextBox>                
                                            </DataItemTemplate>        
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="设备等待<br />时间(秒)" FieldName="WaitSec" Width="50px" VisibleIndex="12">
                                            <Settings AllowCellMerge="False" />
                                            <DataItemTemplate>                
                                                <dx:ASPxTextBox ID="WaitSec" Width="50px" runat="server" Value='<%# Eval("WaitSec")%>' OnValueChanged="WaitSec_TextChanged" ></dx:ASPxTextBox>                
                                            </DataItemTemplate>        
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="装夹时间<br />(秒)" FieldName="ZjSecc" Width="50px" VisibleIndex="13">
                                            <Settings AllowCellMerge="False" />
                                            <DataItemTemplate>                
                                                <dx:ASPxTextBox ID="ZjSecc" Width="50px" runat="server" Value='<%# Eval("ZjSecc")%>' OnValueChanged="ZjSecc_TextChanged" ></dx:ASPxTextBox>                
                                            </DataItemTemplate>        
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="机器台数" FieldName="JtNum" Width="50px" VisibleIndex="14">
                                            <Settings AllowCellMerge="False" />
                                            <DataItemTemplate>                
                                                <dx:ASPxTextBox ID="JtNum" Width="50px" runat="server" Value='<%# Eval("JtNum")%>' OnValueChanged="JtNum_TextChanged" ></dx:ASPxTextBox>                
                                            </DataItemTemplate>        
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="单台单件<br />工序工时(秒)" FieldName="TjOpSec" Width="60px" VisibleIndex="15">
                                            <PropertiesTextEdit DisplayFormatString="{0:N2}"></PropertiesTextEdit>
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="单件工时<br />(秒)" FieldName="JSec" Width="50px" VisibleIndex="16">
                                            <PropertiesTextEdit DisplayFormatString="{0:N2}"></PropertiesTextEdit>
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="单件工时<br />(小时)" FieldName="JHour" Width="50px" VisibleIndex="17">
                                             <PropertiesTextEdit DisplayFormatString="{0:N5}"></PropertiesTextEdit>
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="单台<br />需要人数" FieldName="col1" Width="50px" VisibleIndex="18">
                                            <Settings AllowCellMerge="False" />
                                            <DataItemTemplate>                
                                                <dx:ASPxTextBox ID="col1" Width="50px" runat="server" Value='<%# Eval("col1")%>' ></dx:ASPxTextBox>                
                                            </DataItemTemplate>   
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="本工序一人<br />操作台数" FieldName="col2" Width="60px" VisibleIndex="19">
                                            <Settings AllowCellMerge="False" />
                                            <DataItemTemplate>                
                                                <dx:ASPxTextBox ID="col2" Width="60px" runat="server" Value='<%# Eval("col2")%>' ></dx:ASPxTextBox>                
                                            </DataItemTemplate>   
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="单台85%<br />产量" FieldName="col3" Width="60px" VisibleIndex="20"> </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="一人85%<br />产量" FieldName="col4" Width="60px" VisibleIndex="21"></dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="整线班产量" FieldName="col5" Width="60px" VisibleIndex="22"></dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="完工工时" FieldName="FinishHour" Width="50px" VisibleIndex="23">
                                            <PropertiesTextEdit DisplayFormatString="{0:N2}"></PropertiesTextEdit>
                                        </dx:GridViewDataTextColumn>
                                        
                                        <%--<dx:GridViewDataTextColumn Caption="单台100%<br />产量" FieldName="TSumNum" Width="70px" VisibleIndex="18"></dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="单台80%<br />产量" FieldName="TPec8Num" Width="70px" VisibleIndex="19"></dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="操作人数" FieldName="OpNum" Width="70px" VisibleIndex="25">
                                            <Settings AllowCellMerge="False" />
                                            <DataItemTemplate>                
                                                <dx:ASPxTextBox ID="OpNum" Width="70px" runat="server" Value='<%# Eval("OpNum")%>' ></dx:ASPxTextBox>                
                                            </DataItemTemplate>        
                                        </dx:GridViewDataTextColumn>--%>

                                        <dx:GridViewDataTextColumn FieldName="" Caption=" " VisibleIndex="30" >
                                            <Settings AllowCellMerge="False" />
                                            <DataItemTemplate>                
                                                <dx:ASPxButton ID="btn" runat="server" Text="新增行"   CommandName="Add"  ></dx:ASPxButton>          
                                            </DataItemTemplate>                        
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="numid" Width="0px">
                                             <HeaderStyle CssClass="hidden" />
                                             <CellStyle CssClass="hidden"></CellStyle>
                                             <FooterCellStyle CssClass="hidden"></FooterCellStyle>
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="id" Width="0px">
                                             <HeaderStyle CssClass="hidden" />
                                             <CellStyle CssClass="hidden"></CellStyle>
                                             <FooterCellStyle CssClass="hidden"></FooterCellStyle>
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="GYGSNo" Width="0px">
                                             <HeaderStyle CssClass="hidden" />
                                             <CellStyle CssClass="hidden"></CellStyle>
                                             <FooterCellStyle CssClass="hidden"></FooterCellStyle>
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="UpdateById" Width="0px">
                                             <HeaderStyle CssClass="hidden" />
                                             <CellStyle CssClass="hidden"></CellStyle>
                                             <FooterCellStyle CssClass="hidden"></FooterCellStyle>
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="UpdateByName" Width="0px">
                                             <HeaderStyle CssClass="hidden" />
                                             <CellStyle CssClass="hidden"></CellStyle>
                                             <FooterCellStyle CssClass="hidden"></FooterCellStyle>
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="UpdateDate" Width="0px"> 
                                             <HeaderStyle CssClass="hidden" />
                                             <CellStyle CssClass="hidden"></CellStyle>
                                             <FooterCellStyle CssClass="hidden"></FooterCellStyle>
                                        </dx:GridViewDataTextColumn>
                                    </Columns>                                                
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

