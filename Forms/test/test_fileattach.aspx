<%@ Page Title="测试申请单" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="test_fileattach.aspx.cs" Inherits="Forms_test_test_fileattach" 
    MaintainScrollPositionOnPostback="True" %>

<%@ Register assembly="DevExpress.Web.v17.2, Version=17.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web" tagprefix="dx" %>

<asp:Content ID="HeaderContent" ContentPlaceHolderID="HeadContent" Runat="Server">
    <link href="../../Content/css/bootstrap.min.css" rel="stylesheet" />

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

          .auto-style1 {
              position: relative;
              min-height: 1px;
              float: left;
              width: 100%;
              top: -5px;
              left: 0px;
              margin-top: 0px;
              padding-left: 1px;
              padding-right: 1px;
          }

    </style>

    <script src="../../Content/js/jquery.min.js"></script>
    <script src="../../Content/js/bootstrap.min.js"></script>
    <script src="../../Content/js/layer/layer.js"></script>

    <script type="text/javascript">
        $(document).ready(function () {
            $("#mestitle").html("测试申请单");

            ////提出自定流程 JS 
            //if ($('#3B271F67-0433-4082-AD1A-8DF1B967B879', parent.document).length == 0) {
            //    //alert("保存")
            //    $("input[id*=btnSave]").hide();
            //}
            //if ($('#8982B97C-ADBA-4A3A-AFD9-9A3EF6FF12D8', parent.document).length == 0) {
            //    //alert("发送")
            //    $("input[id*=btnflowSend]").hide();
            //}
            //if ($('#9A9C93B2-F77E-4EEE-BDF1-0E7D1A855B8D', parent.document).length == 0) {
            //    //alert("加签")
            //    $("#btnaddWrite").hide();
            //}
            //if ($('#86B7FA6C-891F-4565-9309-81672D3BA80A', parent.document).length == 0) {
            //    // alert("退回");
            //    $("#btnflowBack").hide();
            //}
            //if ($('#B8A7AF17-7AD5-4699-B679-D421691DD737', parent.document).length == 0) {
            //    //alert("查看流程");
            //    //  $("#btnshowProcess").hide();
            //}
            //if ($('#347B811C-7568-4472-9A61-6C31F66980AE', parent.document).length == 0) {
            //    //alert("转交");
            //    $("#btnflowRedirect").hide();
            //}
            //if ($('#954EFFA8-03B8-461A-AAA8-8727D090DCB9', parent.document).length == 0) {
            //    //alert("完成");
            //    $("#btnflowCompleted").hide();
            //}
            //if ($('#BC3172E5-08D2-449A-BFF0-BD6F4DE797B0', parent.document).length == 0) {
            //    //alert("终止");
            //    $("#btntaskEnd").hide();
            //}
        });

        //提出自定流程 JS 
        function setComment(val) {
            $('#comment', parent.document).val(val);
        }


    </script>
</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

    <div class="col-md-12  ">
        <div class="col-md-10  ">
            <div class="form-inline " style="text-align:right">
                <asp:Button ID="btnSave" runat="server" Text="保存" CssClass="btn btn-default btn-xs btnSave" OnClientClick="return validate();" OnClick="btnSave_Click" />
                <asp:Button ID="btnflowSend" runat="server" Text="申请" CssClass="btn btn-default btn-xs btnflowSend"  OnClientClick="return validate();" OnClick="btnflowSend_Click" />
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
                    <strong>申请人基础信息</strong>
                </div>
                <div class="panel-body <% =ViewState["lv"].ToString() == "SQXX" ? "" : "collapse" %>" id="SQXX">
                    <div class="col-xs-12 col-sm-12  col-md-12 col-lg-12" style="width:1000px;">
                        <asp:UpdatePanel ID="UpdatePanel_request" runat="server">
                            <ContentTemplate>
                                <table style="width: 100%; line-height:40px;">
                                    <tr>
                                        <td>表单单号</td>
                                        <td>
                                            <div class="form-inline">
                                                    <input id="txt_testno" class="form-control input-s-sm" style="height: 30px; width: 200px;font-size:12px;" runat="server" readonly="True"  />
                                            </div>
                                        </td>
                                        <td>申请时间</td>
                                        <td>
                                            <div class="form-inline">
                                                    <input id="txt_CreateDate" class="form-control input-s-sm" style="height: 30px; width: 200px;font-size:12px;" runat="server" readonly="True" />
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>创建人</td>
                                        <td>
                                            <div class="form-inline">
                                                <input id="txt_CreateById" class="form-control input-s-sm" style="height: 30px; width: 100px;font-size:12px;" runat="server" readonly="True" />
                                                <input id="txt_CreateByName" class="form-control input-s-sm" style="height: 30px; width: 100px;font-size:12px;" runat="server" readonly="True" />
                                            </div>
                                        </td>
                                        <td>申请人</td>
                                        <td>
                                            <div class="form-inline">
                                                <input id="txt_AppById" class="form-control input-s-sm" style="height: 30px; width: 100px;font-size:12px;" runat="server" readonly="True"  />
                                                <input id="txt_AppByName" class="form-control input-s-sm" style="height: 30px; width: 100px;font-size:12px;" runat="server" readonly="True" />
                                            </div>
                                        </td>                                        
                                    </tr>
                                </table>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
        </div>

        <div class="row row-container">
            <div class="panel panel-info">
                <div class="panel-heading" data-toggle="collapse" data-target="#FJSC">
                        <strong>供应商报价单，报价分析，技术协议，合同</strong>
                </div>
                <div class="panel-body collapse in" id="FJSC">
                    <div class="col-xs-12 col-sm-12  col-md-12 col-lg-12" style="width:1000px;">
                            
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

