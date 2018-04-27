<%@ Page Title="MES生产管理系统" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="Yangjian.aspx.cs" Inherits="YangJian" MaintainScrollPositionOnPostback="True"   ValidateRequest="true"%>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <link href="../Content/css/bootstrap.min.css" rel="stylesheet" />
    <script src="../Content/js/jquery.min.js"></script>
    <script src="../Content/js/bootstrap.min.js"></script>
    <script src="../Content/js/plugins/layer/layer.min.js"></script>
    <script src="../Content/js/plugins/layer/laydate/laydate.js"></script>

    <style type="text/css">
        .auto-style1 {
            height: 69px;
        }

        .row {
            margin-right: 2px;
            margin-left: 2px;
        }

        /*.row-container {
            padding-left: 2px;
            padding-right: 2px;
        }*/

        fieldset {
            background: rgba(255,255,255,.3);
            border-color: lightblue;
            border-style: solid;
            border-width: 2px;
            -moz-border-radius: 5px;
            -webkit-border-radius: 5px;
            -khtml-border-radius: 5px;
            border-radius: 5px;
            line-height: 30px;
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
        fieldset{padding:.35em .625em .75em;margin:0 2px;border:1px solid lightblue}
        legend{padding:5px;border:0;width:auto;margin-bottom: 2px;}
        .panel
        {
            margin-bottom: 5px;
        }
        .panel-heading
        {
            padding: 5px 5px 5px 5px;
        }
        .panel-body
        {
            padding: 5px 5px 5px 5px;
        }
        body
        {
            margin-left: 5px;
            margin-right: 5px;
        }
        .col-lg-1, .col-lg-10, .col-lg-11, .col-lg-12, .col-lg-2, .col-lg-3, .col-lg-4, .col-lg-5, .col-lg-6, .col-lg-7, .col-lg-8, .col-lg-9, .col-md-1, .col-md-10, .col-md-11, .col-md-12, .col-md-2, .col-md-3, .col-md-4, .col-md-5, .col-md-6, .col-md-7, .col-md-8, .col-md-9, .col-sm-1, .col-sm-10, .col-sm-11, .col-sm-12, .col-sm-2, .col-sm-3, .col-sm-4, .col-sm-5, .col-sm-6, .col-sm-7, .col-sm-8, .col-sm-9, .col-xs-1, .col-xs-10, .col-xs-11, .col-xs-12, .col-xs-2, .col-xs-3, .col-xs-4, .col-xs-5, .col-xs-6, .col-xs-7, .col-xs-8, .col-xs-9
        {
            position: relative;
            min-height: 1px;
            padding-right: 1px;
            padding-left: 1px;
            margin-top: 0px;
            top: 0px;
            left: 0px;
        }
        .auto-style3 {
            width: 4px;
        }
        .auto-style4 {
            color: #00CCFF;
        }
        td {
             font-size:12px;
             vertical-align:top;
            padding-bottom: 5px;
        }
        p.MsoListParagraph
	{margin-bottom:.0001pt;
	text-align:justify;
	text-justify:inter-ideograph;
	text-indent:21.0pt;
	font-size:10.5pt;
	font-family:"Calibri","sans-serif";
	        margin-left: 0cm;
            margin-right: 0cm;
            margin-top: 0cm;
        }
        .auto-style6 {
            color: #6666FF;
        }
        .auto-style7 {
            color: #CC0000;
        }
        .auto-style8 {
            color: #66CCFF;
            font-size: x-large;
        }
        .auto-style9 {
            color: #FFCC00;
            font-size: large;
        }
        .auto-style10 {
            color: #FF0000;
        }
        </style>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#mestitle").text("【PGI样件发货流程】");
            //图片打开新窗口
            $("a[href]").click(function(){
                var url=this.href.toLowerCase();
                var id=this.id;
                var pathName=this.pathname;
                if(url.indexOf('.jpg')>0||url.indexOf('.bmp')>0||url.indexOf('.png')>0)
                {
                    $("#"+id).attr('href','javascript:void(0)');
                    layer.open({
                        type: 1,
                        skin: 'layui-layer-demo', //样式类名
                        closeBtn: 1, //显示关闭按钮
                        anim: 2,
                        area: ['1000px', '700px'],
                        shadeClose: true, //开启遮罩关闭
                        content: '<html><body><img src='+url+' style="width:800px;height:800px"></body></html>',
                        end: function () {        
                            $("#"+id).attr("href",url);
                        }
                    });
                }
            })

        })//end ready
        function openwind(code,flr){        
            layer.open({
                shade: [0.5, '#000', false],
                type: 2,
                offset: '2365px',
                area: ['750px', '550px'],
                fix: false, //不固定
                maxmin: false,
                title: ['在库分选单(如无法直接打印,可转成其他格式再打印.)', false],
                closeBtn: 1,
                content: 'PrintFenJianDan.aspx?Code=' + code+'&FLR='+flr,
                end: function () {                   
                }
            });
        }

    </script>

        <script type="text/javascript">
        $(document).ready(function () {
            $("#mestitle").text("【PGI样件发货流程】");

        })//end ready
        function openwindFord(requestid,dyym,PrintCount){        
            //layer.open({
            //    shade: [0.5, '#000', false],
            //    type: 2,
            //    offset: '20',
            //    area: ['1200px', '600px'],
            //    fix: false, //不固定
            //    maxmin: false,
            //    title: ['标签(如无法直接打印,可转成其他格式再打印.)', false],
            //    closeBtn: 1,
            //    content: ''+dyym+'?requestid='+requestid+'&PrintCount='+PrintCount,
            //    end: function () {                   
            //    }
            //});
            window.open(dyym+'?requestid='+requestid+'&PrintCount='+PrintCount)
        }

    </script>
          <script type="text/javascript">
        $(document).ready(function () {
            $("#mestitle").text("【PGI样件发货流程】");

        })//end ready
        function openwindother(requestid,dyym){        
            layer.open({
                shade: [0.5, '#000', false],
                type: 2,
                offset: '20px',
                area: ['1200px', '700px'],
                fix: false, //不固定
                maxmin: false,
                title: ['标签(如无法直接打印,可转成其他格式再打印.)', false],
                closeBtn: 1,
                content: ''+dyym+'?requestid='+requestid+'',
                end: function () {                   
                }
            });
        }

    </script>
    <script type="text/javascript">
        $().ready(function(){           

            $("input[id*='txt_yqfy_date']").click(function (e) {
                laydate({
                    choose: function (dates) { //选择好日期的回调                       
                        $("input[id*='txt_yqfy_date']").change();
                    }
                })
            })


             
        })
 
        var popupwindow = null;

        function GetXMH() {
 
            popupwindow = window.open('../Select/select_YJ.aspx', '_blank', 'height=500,width=1000,resizable=no,menubar=no,scrollbars =no,location=no');
        }
        function GetXMH2() {
            var xmh=document.getElementById('<%=this.txt_xmh.ClientID%>').value;
            var domain=document.getElementById('<%=this.txt_domain.ClientID%>').value
            popupwindow = window.open('../Select/select_YJ.aspx?ljh='+xmh+'&domain=' + domain , '_blank', 'height=500,width=1000,resizable=no,menubar=no,scrollbars =no,location=no');
        }
        function XMH_setvalue(formName, CP_ID) {
       
            // $("input[id*='txt_CP_ID']").val(CP_ID);
            ctl01.<%=txt_CP_ID.ClientID%>.value = CP_ID;
            document.getElementById('<%=txt_CP_ID.ClientID%>').onchange();
            //$("input[id*='txt_CP_ID_Text']").change();
            <%-- form1.<%=txt_ljh.ClientID%>.value = ljh;--%>
                <%--ctl01.<%=txt_sktj.ClientID%>.value = CP_ID;--%>
         
  
            popupwindow.close();
        }

         
  
    </script>

    <script type="text/javascript">
        var popupwindow = null;
        //收货地址
        function Getshdz() { 
            popupwindow = window.open('../Select/select_shdz.aspx', '_blank', 'height=500,width=1000,resizable=no,menubar=no,scrollbars =no,location=no');
        }
        function shdz_setvalue(shrxx, shdz) {       
            ctl01.<%=txt_shdz.ClientID%>.value = shdz;
            ctl01.<%=txt_shrxx.ClientID%>.value = shrxx;          
            popupwindow.close();
        }

    </script>
     <script type="text/javascript">
        
      function goTo() {
          $("html,body").animate({scrollTop: $("#txt_gysm_zl").offset().top}, 500);
    }

 </script>


    <div class="col-md-9">
        <%--操作日志--%>
        <div class="row row-container">
            <div class="col-md-12">
                <div class="panel panel-info">
                    <div class="panel-heading" data-toggle="collapse" data-target="#SQXX">
                        <strong>申请人信息</strong>
                    </div>
                    <%--  <div class="panel-body collapse" id="ZLGCS">--%>
                    <div class="panel-body <% =ViewState["lv"].ToString() == "SQXX" ? "" : "collapse" %>" id="SQXX">
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
                                                        <input id="txt_Userid" class="form-control input-s-sm" style="height: 35px; width:100px" runat="server" readonly="True" />
                                                        /
                                                    <input id="txt_UserName" class="form-control input-s-sm" style="height: 35px; width:100px" runat="server" readonly="True" />
                                                        /
                                                    <input id="txt_UserName_AD" class="form-control input-s-sm" style="height: 35px; width:100px" runat="server" readonly="True" />
                                                    </div>
                                                </td>
                                                <td>部门：
                                                </td>
                                                <td>
                                                    <input id="txt_dept" class="form-control input-s-sm" style="height: 35px; width: 100px" runat="server" readonly="True" />
                                                    &nbsp;</td>
                                                <td>部门经理：
                                                </td>
                                                <td>
                                                    <div class="form-inline">
                                                        <input id="txt_managerid" class="form-control input-s-sm" style="height: 35px; width:100px" runat="server" readonly="True" />
                                                        /<input id="txt_manager" class="form-control input-s-sm" style="height: 35px; width:100px" runat="server" readonly="True" />
                                                        /<input id="txt_manager_AD" class="form-control input-s-sm" style="height: 35px; width:100px" runat="server" readonly="True" />
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="word-break: ">样件订单编号：
                                                </td>
                                                <td>
                                                    <input id="txt_Code" class="form-control input-s-sm" style="height: 35px; width: 150px" runat="server" readonly="True" />
                                                </td>
                                                <td>&nbsp;</td>
                                                <td>

                                                    <input id="txt_update_user" class="input form-control input-s-sm" style="height: 35px; width: 90px" runat="server" readonly="True" />
                                                </td>
                                                <td>申请日期：
                                                </td>
                                                <td>
                                                    <input id="txt_CreateDate" class="form-control input-s-sm" style="height: 35px; width: 100px" runat="server" readonly="True" />
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

        <%--<input id="txt_status_id"  runat="server" readonly="true" />--%>
        <div class="row  row-container">
            <div class="col-md-12">
                <div class="panel panel-info">
                    <div class="panel-heading" data-toggle="collapse" data-target="#XSZL">
                        <strong>销售助理--》1.订单信息 2.订舱3.到货</strong>
                    </div>
                    <%--操作日志--%>
                    <div class="panel-body " id="XSZL">
                        <div class="col-xs-12 col-sm-12  col-md-12 col-lg-12">

                            <div>
                                <fieldset style="border-color: lightblue">
                                    <legend>一.订单输入</legend>
                                    <table style="height: 35px; width: 100%">
                                        <tr>
                                            <td>1.基础信息</td>
                                            <td></td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td>申请工厂：</td>
                                            <td>
                                                <div class="form-inline">
                                                    <input id="txt_domain" class="form-control input-s-sm" style="height: 35px; width: 50px" runat="server" readonly="True" /><input id="txt_sqgc" class="form-control input-s-sm" style="height: 35px; width: 135px" runat="server" readonly="True" />
                                                </div>
                                            </td>
                                            <td>制造工厂:</td>
                                            <td><div class="form-inline">
                                                    <input id="txt_domain_zzgc" class="form-control input-s-sm" style="height: 35px; width: 50px" runat="server" readonly="True" /><input id="txt_zzgc" class="form-control input-s-sm" style="height: 35px; width: 135px" runat="server" readonly="True" /> <asp:RequiredFieldValidator ID="yz56" runat="server" ControlToValidate="txt_domain_zzgc" ErrorMessage="不能为空" ValidationGroup="request" ForeColor="Red"></asp:RequiredFieldValidator>
                                                </div></td>
                                            <td>订单收到日期：
                                            </td>
                                            <td><div class="form-inline">
                                                <input id="txt_sddd_date" class="form-control input-s-sm" style="height: 35px; width: 150px" runat="server" onclick="laydate()" /><asp:RequiredFieldValidator ID="yz16" runat="server" ControlToValidate="txt_gkddh" ErrorMessage="不能为空" ValidationGroup="request" ForeColor="Red"></asp:RequiredFieldValidator>
                                                </div></td>
                                        </tr>
                                        <tr>
                                            <td>顾客订单号：
                                            </td>
                                            <td><div class="form-inline">
                                                <input id="txt_gkddh" class="form-control input-s-sm" style="height: 35px; width: 150px" runat="server" /><asp:RequiredFieldValidator ID="yz15" runat="server" ControlToValidate="txt_gkddh" ErrorMessage="不能为空" ValidationGroup="request" ForeColor="Red"></asp:RequiredFieldValidator>
                                             </div></td>
                                            <td>订单行:</td>
                                            <td>
                                                <input id="txt_Line_Code" class="form-control input-s-sm" style="height: 35px; width: 150px" runat="server" /></td>
                                            <td>&nbsp;</td>
                                            <td>
                                                &nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td>订单附件：
                                            </td>
                                            <td colspan="5">
                                                <div class="form-inline">
                                                    <asp:GridView ID="gvFile_ddfj" runat="server" AutoGenerateColumns="False" DataKeyNames="id" OnRowDeleting="gvFile_ddfj_RowDeleting" CellPadding="4" ForeColor="#333333" GridLines="None">
                                                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                                        <Columns>
                                                            <asp:BoundField ApplyFormatInEditMode="True" DataField="id" HeaderText="No." ShowHeader="False" />
                                                            <asp:TemplateField HeaderText="文件名称" ShowHeader="False">
                                                                <ItemTemplate>
                                                                    <asp:HyperLink ID="HyperLink1" runat="server" Target="_blank" NavigateUrl='<% #DataBinder.Eval(Container.DataItem, "File_lj")%>'  Text='<%#DataBinder.Eval(Container.DataItem,"File_name")%>'></asp:HyperLink>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField ShowHeader="False" HeaderText="">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="false" CommandName="Delete" Text="刪除"></asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <EditRowStyle BackColor="#999999" />
                                                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                                        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                                        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                                        <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                                        <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                                        <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                                        <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                                                    </asp:GridView>
                                                    <br />
                                                    <input id="txt_ddfj" type="file" class="form-control" style="width: 90px" multiple="multiple" runat="server" /><asp:Button ID="Btn_ddfj" runat="server" CssClass="form-control" Text="上传文件" OnClick="Btn_ddfj_Click" />
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>选择零件号：</td>
                                            <td>
                                                <div class="form-inline">
                                                    <img name="selectljh" style="border: 0px;" src="../images/fdj.gif" alt="select" onclick="GetXMH()" /><input id="txt_ljh" class="form-control input-s-sm" style="height: 35px; width: 135px" runat="server" readonly="True" />&nbsp;&nbsp;<asp:TextBox ID="txt_CP_ID" Style="height: 0px; width: 0px" runat="server" AutoPostBack="True" OnTextChanged="txt_CP_ID_TextChanged"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="yz46" runat="server" ControlToValidate="txt_ljh" ErrorMessage="请选择零件" ValidationGroup="request" ForeColor="Red"></asp:RequiredFieldValidator>
                                                   </div>                                               
                                            </td>
                                            <td>PG零件号：</td>
                                            <td><input id="txt_xmh" class="form-control input-s-sm" style="height: 35px; width: 150px" runat="server" readonly="True" />
                                            </td>
                                            <td>零件名称：</td>
                                            <td>
                                                <input id="txt_ljmc" class="form-control input-s-sm" style="height: 35px; width: 150px" runat="server" readonly="True" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>零件重量：</td>
                                            <td>
                                                <input id="txt_ljzl" class="form-control input-s-sm" style="height: 35px; width: 150px" runat="server" readonly="True" />
                                            </td>
                                            <td>发货至：</td>
                                            <td>
                                                <input id="txt_fhz" class="form-control input-s-sm" style="height: 35px; width: 150px" runat="server" readonly="True" />
                                            </td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td>要货数量：</td>
                                            <td>    <div class="form-inline">
                                                <asp:TextBox ID="txt_yhsl" class="form-control input-s-sm" style="height: 35px; width: 150px" runat="server" AutoPostBack="True" OnTextChanged="txt_yhsl_TextChanged"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="yz17" runat="server" ControlToValidate="txt_yhsl" ErrorMessage="不能为空" ValidationGroup="request" ForeColor="Red"></asp:RequiredFieldValidator>
                                               </div> </td>
                                            <td>QAD库存量：</td>
                                            <td>
                                                <input id="txt_kcl" class="form-control input-s-sm" style="height: 35px; width: 150px" runat="server" readonly="True" />
                                            </td>
                                            <td>库存日期：</td>
                                            <td>
                                                <input id="txt_kc_date" class="form-control input-s-sm" style="height: 35px; width: 150px" runat="server" readonly="True" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td >图纸日期：</td>
                                            <td >
                                                <input id="txt_tz_date" class="form-control input-s-sm" style="height: 35px; width: 150px" runat="server" onclick="laydate()" />&nbsp;</td>
                                            <td >要求到货日期：</td>
                                            <td > <div class="form-inline">
                                                <input id="txt_yqdh_date" class="form-control input-s-sm" style="height: 35px; width: 150px" runat="server" onclick="laydate()" /><asp:RequiredFieldValidator ID="yz19" runat="server" ControlToValidate="txt_yqdh_date" ErrorMessage="不能为空" ValidationGroup="request" ForeColor="Red"></asp:RequiredFieldValidator>
                                                </div></td>
                                            <td ></td>
                                            <td ></td>
                                        </tr>
                                        <tr>
                                            <td>2.商务信息</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                        </tr>

                                        <tr>
                                            <td>客户代码：</td>
                                            <td><div class="form-inline">
                                                <input id="txt_gkdm" class="form-control input-s-sm" style="height: 35px; width: 150px" runat="server" readonly="True" /><asp:RequiredFieldValidator ID="yz20" runat="server" ControlToValidate="txt_gkdm" ErrorMessage="不能为空" ValidationGroup="request" ForeColor="Red"></asp:RequiredFieldValidator>
                                                 </div> </td>
                                            <td>客户名称：
                                            </td>
                                            <td><div class="form-inline">
                                                <input id="txt_gkmc" class="form-control input-s-sm" style="height: 35px; width: 150px" runat="server" readonly="True" /><asp:RequiredFieldValidator ID="yz21" runat="server" ControlToValidate="txt_gkmc" ErrorMessage="不能为空" ValidationGroup="request" ForeColor="Red"></asp:RequiredFieldValidator>
                                                </div> </td>
                                            <td>&nbsp;</td>
                                            <td>
                                                &nbsp;</td>
                                        </tr>

                                        <tr>
                                            <td>QAD单价</td>
                                            <td>
                                                <asp:TextBox ID="txt_ljdj_qad" class="form-control input-s-sm" style="height: 35px; width: 150px" runat="server"  BorderColor="#00CCFF" readonly="True"></asp:TextBox>
                                                </td>
                                            <td>
                                                <asp:Label ID="Lab_ljdj_rq" runat="server" Text="价格到期日期" Visible="False"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txt_ljdj_rq" class="form-control input-s-sm" style="height: 35px; width: 150px" runat="server"  BorderColor="#00CCFF" readonly="True" Visible="False"></asp:TextBox>
                                                </td>
                                            <td>货币：</td>
                                             <td><div class="form-inline">
                                                <input id="txt_hb" class="form-control input-s-sm" style="height: 35px; width: 150px" runat="server" readonly="True" /><asp:RequiredFieldValidator ID="yz22" runat="server" ControlToValidate="txt_hb" ErrorMessage="不能为空" ValidationGroup="request" ForeColor="Red"></asp:RequiredFieldValidator>
                                                 </div> </td>
                                        </tr>
                                        <tr>
                                            <td class="auto-style4">&nbsp;实际单价：</td>
                                            <td><div class="form-inline">
                                                <asp:TextBox ID="txt_ljdj" class="form-control input-s-sm" style="height: 35px; width: 150px" runat="server" OnTextChanged="txt_ljdj_TextChanged" AutoPostBack="True" BorderColor="#00CCFF"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="yz23" runat="server" ControlToValidate="txt_ljdj" ErrorMessage="不能为空" ValidationGroup="request" ForeColor="Red"></asp:RequiredFieldValidator>
                                                </div> </td>
                                            <td >总价：</td>
                                             <td><div class="form-inline">
                                                <input id="txt_ljzj" class="form-control input-s-sm" style="height: 35px; width: 150px" runat="server" readonly="True" /><asp:RequiredFieldValidator ID="yz24" runat="server" ControlToValidate="txt_ljzj" ErrorMessage="不能为空" ValidationGroup="request" ForeColor="Red"></asp:RequiredFieldValidator>
                                                </div> </td>
                                            <td>&nbsp;</td>
                                            <td>
                                                &nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td>收款条件：</td>
                                           <td><div class="form-inline">
                                                <asp:DropDownList ID="txt_sktj" class="form-control input-s-sm" style="height: 35px; width: 150px" runat="server"></asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="yz25" runat="server" ControlToValidate="txt_sktj" ErrorMessage="不能为空" ValidationGroup="request" ForeColor="Red"></asp:RequiredFieldValidator>
                                            </div> </td>
                                            <td>运输方式：</td>
                                             <td><div class="form-inline">

                                                <asp:DropDownList ID="txt_ysfs" class="form-control input-s-sm" style="height: 35px; width: 150px" runat="server" AutoPostBack="True" OnSelectedIndexChanged="txt_ysfs_SelectedIndexChanged"></asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="yz26" runat="server" ControlToValidate="txt_ysfs" ErrorMessage="不能为空" ValidationGroup="request" ForeColor="Red"></asp:RequiredFieldValidator>
                                            </div> </td>
                                            <td>要求发运日期</td>
                                            <td><div class="form-inline">
                                                <asp:TextBox ID="txt_yqfy_date" class="form-control input-s-sm" style="height: 35px; width: 150px" runat="server" AutoPostBack="True" OnTextChanged="txt_yqfy_date_TextChanged"></asp:TextBox>

                                                <asp:RequiredFieldValidator ID="yz27" runat="server" ControlToValidate="txt_yqfy_date" ErrorMessage="不能为空" ValidationGroup="request" ForeColor="Red"></asp:RequiredFieldValidator>
                                           </div> </td>
                                        </tr>
                                        <tr>
                                            <td>运费支付方式：</td>
                                            <td><div class="form-inline">
                                                <asp:DropDownList ID="txt_yfzffs" class="form-control input-s-sm" style="height: 35px; width: 150px" runat="server"></asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="yz28" runat="server" ControlToValidate="txt_yfzffs" ErrorMessage="不能为空" ValidationGroup="request" ForeColor="Red"></asp:RequiredFieldValidator>
                                            </div> </td>
                                            <td>运费金额：</td>
                                             <td><div class="form-inline">
                                                <input id="txt_yfje" class="form-control input-s-sm" style="height: 35px; width: 150px" runat="server" /><asp:RequiredFieldValidator ID="yz55" runat="server" ControlToValidate="txt_yfje" ErrorMessage="不能为空" ValidationGroup="request" ForeColor="Red"></asp:RequiredFieldValidator>
                                              </div> </td>
                                            <td>运输条款：</td>
                                             <td><div class="form-inline">

                                                <asp:DropDownList ID="txt_ystk" class="form-control input-s-sm" style="height: 35px; width: 150px" runat="server"></asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="yz29" runat="server" ControlToValidate="txt_ystk" ErrorMessage="不能为空" ValidationGroup="request" ForeColor="Red"></asp:RequiredFieldValidator>
                                              </div> </td>
                                        </tr>
                                      
                                        <tr>
                                            <td>收货人信息(请选择)：
                                            </td>
                                            <td colspan=" 5 "><div class="form-inline">
                                                <div class="form-inline">
                                                    <img name="selectshxx" style="border: 0px;" src="../images/fdj.gif" alt="select" onclick="Getshdz()" />
                                                    <input id="txt_shrxx" class="form-control input-s-sm" style="height: 35px; width: 95%" runat="server" />
                                                </div>
                                                <asp:RequiredFieldValidator ID="yz30" runat="server" ControlToValidate="txt_shrxx" ErrorMessage="不能为空" ValidationGroup="request" ForeColor="Red"></asp:RequiredFieldValidator>
                                            </div> </td>

                                        </tr>
                                        <tr>
                                            <td>收货人地址：</td>
                                            <td colspan="5"><div class="form-inline">
                                                <input id="txt_shdz" class="form-control input-s-sm" style="height: 35px; width: 95%" runat="server" /><asp:RequiredFieldValidator ID="yz31" runat="server" ControlToValidate="txt_shdz" ErrorMessage="不能为空" ValidationGroup="request" ForeColor="Red"></asp:RequiredFieldValidator>
                                                </div> </td>
                                        </tr>
                                        <tr>
                                            <td>3.客户特殊要求</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td>要求</td>
                                            <td colspan="5">
                                                <input id="txt_yq" class="form-control input-s-sm" style="height: 35px; width: 95%" runat="server" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>要求附件：
                                            </td>
                                            <td colspan="5">
                                                <div class="form-inline">
                                                    &nbsp;<asp:GridView ID="gvFile_yqfj" runat="server" AutoGenerateColumns="False" DataKeyNames="id" OnRowDeleting="gvFile_yqfj_RowDeleting" CellPadding="4" ForeColor="#333333" GridLines="None">
                                                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                                        <Columns>
                                                            <asp:BoundField ApplyFormatInEditMode="True" DataField="id" HeaderText="No." ShowHeader="False" />
                                                            <asp:TemplateField HeaderText="文件名称" ShowHeader="False">
                                                                <ItemTemplate>
                                                                    <asp:HyperLink ID="HyperLink1" runat="server" Target="_blank" NavigateUrl='<% #DataBinder.Eval(Container.DataItem, "File_lj")%>' Text='<%#DataBinder.Eval(Container.DataItem,"File_name")%>'></asp:HyperLink>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField ShowHeader="False" HeaderText="">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="false" CommandName="Delete" Text="刪除"></asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <EditRowStyle BackColor="#999999" />
                                                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                                        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                                        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                                        <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                                        <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                                        <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                                        <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                                                    </asp:GridView>
                                                    <input id="txt_yqfj" type="file" class="form-control" style="width: 90px" multiple="multiple" runat="server" /><asp:Button ID="Btn_yqfj" runat="server" CssClass="form-control" Text="上传文件" OnClick="Btn_yqfj_Click" />
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="auto-style8" colspan="6"><strong>标签相关信息</strong></td>
                                        </tr>
                                             <tr>
                                            <td class="auto-style10" colspan="2"><strong>通用栏位</strong> </td>
                                            <td>供应商代码</td>
                                            <td>
                                               <input id="txt_gysdm" class="form-control input-s-sm" style="height: 35px; width: 150px" runat="server" /></td>
                                            <td>订单项目<br>名称阶段：</td>
                                            <td>
                                                <input id="txt_xmjd" class="form-control input-s-sm" style="height: 35px; width: 150px" runat="server" /></td>
                                        </tr>
                                          <tr>
                                            <td class="auto-style10" colspan="2">&nbsp;</td>
                                            <td>订单联系人</td>
                                            <td>
                                                <input id="txt_ddlxr" class="form-control input-s-sm" style="height: 35px; width: 150px" runat="server" /></td>
                                            <td>联系人电话</td>
                                            <td>
                                                <input id="txt_ddlxphone" class="form-control input-s-sm" style="height: 35px; width: 150px" runat="server" /></td>
                                        </tr>
                                          <tr>
                                            <td class="auto-style10" colspan="2">选择打印客户大类</td>
                                            <td colspan="4"> <div class="form-inline">
                                                <asp:DropDownList ID="DDL_DY" class="form-control input-s-sm" style="height: 35px; width: 150px" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DDL_DY_SelectedIndexChanged">
                                                    <asp:ListItem Value="101">FORD</asp:ListItem>
                                                    <asp:ListItem Value="102">Chrylser</asp:ListItem>
                                                    <asp:ListItem Value="117">GM</asp:ListItem>
                                                    <asp:ListItem Value="115">AAM</asp:ListItem>
                                                    <asp:ListItem Value="109">BBAC</asp:ListItem>
                                                    <asp:ListItem Value="103">Cooper KS</asp:ListItem>
                                                    <asp:ListItem Value="119">福田Dainler</asp:ListItem>
                                                    <asp:ListItem>其他</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="yz57" runat="server" ControlToValidate="DDL_DY" ErrorMessage="不能为空" ValidationGroup="request" ForeColor="Red"></asp:RequiredFieldValidator>
                                             </div> </td>
                                        </tr>
                                          <tr>
                                            <td class="auto-style10" colspan="2">&nbsp;</td>
                                            <td colspan="4">
                                                <asp:CheckBoxList ID="CBL_DY" runat="server">
                                                </asp:CheckBoxList>
                                              </td>
                                        </tr>
                                          <tr id="FORD" runat="server" >
                                            <td class="auto-style9" colspan="2"><strong>FORD</strong></td>
                                            <td>是否需要Serial_No</td>
                                            <td>
                                                <asp:DropDownList ID="txt_Serial_No" class="form-control input-s-sm" style="height: 35px; width: 150px" runat="server"></asp:DropDownList>
                                              </td>
                                            <td>&nbsp;</td>
                                            <td>
                                                &nbsp;</td>
                                        </tr>
                                          <tr id="Chrylser" runat="server" >
                                           
                                              <td class="auto-style9" colspan="2"><strong>Chrylser</strong></td>
                                            <td>lable_type_Chrylser</td>
                                            <td>
                                                <asp:DropDownList ID="DDL_lable_type" class="form-control input-s-sm" style="height: 35px; width: 150px" runat="server"></asp:DropDownList>
                                              </td>
                                            <td>DESTINATION PLANT</td>
                                            <td>
                                                <input id="txt_mdgc_Chrylser" class="form-control input-s-sm" style="height: 35px; width: 150px" runat="server" /></td>
                                        </tr>
                                           <tr id="GM" runat="server" >
                                            
                                              <td class="auto-style9" colspan="2"><strong>GM</strong></td>
                                            <td>ENG. DESIGN RECORD CHG LVL</td>
                                            <td>
                                                <input id="txt_ENG_GM" class="form-control input-s-sm" style="height: 35px; width: 150px" runat="server" /></td>
                                            <td>&nbsp;</td>
                                            <td>
                                                &nbsp;</td>
                                        </tr>
                                          <tr id="AAM" runat="server" >
                                           
                                                  <td class="auto-style9" colspan="2"><strong>AAM</strong></td>
                                            <td>产生的识别号</td>
                                            <td>

                                                <asp:DropDownList ID="DDL_sbh_AAM" class="form-control input-s-sm" style="height: 35px; width: 150px" runat="server"></asp:DropDownList>
                                              </td>
                                            <td>&nbsp;</td>
                                            <td>
                                                &nbsp;</td>
                                        </tr>
                                          <tr id="AAM2" runat="server">                                          
                                                  <td class="auto-style9" colspan="2">&nbsp;</td>
                                            <td>识别号</td>
                                            <td>
                                                <input id="txt_sbh_HM_AAM" class="form-control input-s-sm" style="height: 35px; width: 150px" runat="server" /></td>
                                            <td>NO号码</td>
                                            <td>
                                                <input id="txt_sbh_NO_AAM" class="form-control input-s-sm" style="height: 35px; width: 150px" runat="server" /></td>
                                        </tr>
                                              <tr id="BBAC" runat="server" >
                                          
                                              <td class="auto-style9" colspan="2"><strong>BBAC</strong></td>
                                            <td><span>零件级别</span><span lang="EN-US"> ZGS</span></td>
                                            <td>
                                                <input id="txt_ZGS_BBAC" class="form-control input-s-sm" style="height: 35px; width: 150px" runat="server" /></td>
                                            <td><span lang="EN-US">E/Q</span><span>级别</span></td>
                                            <td>
                                                <input id="txt_EQ_BBAC" class="form-control input-s-sm" style="height: 35px; width: 150px" runat="server" /></td>
                                        </tr>
                                          <tr id="Daimler" runat="server" >
                                           
                                              <td class="auto-style9" colspan="2"><strong>Daimler</strong></td>
                                            <td>Part Status<br>零件状态</td>
                                            <td>
                                                <asp:DropDownList ID="DDL_Part_Status_Daimler" class="form-control input-s-sm" style="height: 35px; width: 150px" runat="server"></asp:DropDownList>
                                              </td>
                                            <td>其他描述</td>
                                            <td>
                                                <input id="txt_Part_Status_Daimler_ms" class="form-control input-s-sm" style="height: 35px; width: 150px" runat="server" /></td>
                                        </tr>
                                          <tr id="Daimler2" runat="server" v>
                                            <td colspan="2">&nbsp;</td>
                                            <td>Part Change<br>零件变更</td>
                                            <td>
                                                <asp:DropDownList ID="DDL_Part_Change_Daimler" class="form-control input-s-sm" style="height: 35px; width: 150px" runat="server"></asp:DropDownList>
                                              </td>
                                            <td>&nbsp;</td>
                                            <td>
                                                &nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td>标签要求：</td>
                                            <td colspan="5">
                                                <input id="txt_bqyq" class="form-control input-s-sm" style="height: 35px; width: 95%" runat="server" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>要求附件：
                                            </td>
                                            <td colspan="5">
                                                <div class="form-inline">
                                                    &nbsp;<asp:GridView ID="gvFile_bqfj" runat="server" AutoGenerateColumns="False" DataKeyNames="id" OnRowDeleting="gvFile_bqfj_RowDeleting" CellPadding="4" ForeColor="#333333" GridLines="None">
                                                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                                        <Columns>
                                                            <asp:BoundField ApplyFormatInEditMode="True" DataField="id" HeaderText="No." ShowHeader="False" />
                                                            <asp:TemplateField HeaderText="文件名称" ShowHeader="False">
                                                                <ItemTemplate>
                                                                    <asp:HyperLink ID="HyperLink1" runat="server" Target="_blank" NavigateUrl='<% #DataBinder.Eval(Container.DataItem, "File_lj")%>' Text='<%#DataBinder.Eval(Container.DataItem,"File_name")%>'></asp:HyperLink>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField ShowHeader="False" HeaderText="">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="false" CommandName="Delete" Text="刪除"></asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                        </Columns>
                                                        <EditRowStyle BackColor="#999999" />
                                                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                                        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                                        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                                        <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                                        <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                                        <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                                        <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                                                    </asp:GridView>
                                                    <input id="txt_bqfj" type="file" class="form-control" style="width: 90px" multiple="multiple" runat="server" /><asp:Button ID="Btn_bqfj" runat="server" CssClass="form-control" Text="上传文件" OnClick="Btn_bqfj_Click" />
                                                </div>

                                            </td>
                                        </tr>

                                        <tr>
                                            <td>随货文件要求：</td>
                                            <td colspan="5">
                                                <input id="txt_shwjyq" class="form-control input-s-sm" style="height: 35px; width: 95%" runat="server" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>其他特殊要求：</td>
                                            <td colspan="5">
                                                <input id="txt_other_yq" class="form-control input-s-sm" style="height: 35px; width: 95%" runat="server" />
                                            </td>
                                        </tr>
                                        </Table>
                                        <Table>
                                        <tr>
                                            <td>&nbsp;</td>
                                            <td colspan="5">
                                                &nbsp;</td>
                                        </tr>

                                    
                                    <Table style=" width: 100%">
                                        <tr>
                                            <td class="auto-style4" colspan="7">注：要求完成时间计算公式为：</td>
                                        </tr>
                                   
                                        <tr>
                                            <td class="auto-style4">&nbsp;</td>
                                            <td>

                                                确认阶段</td>
                                            <td >&nbsp;</td>
                                            <td >&nbsp;</td>
                                            <td >&nbsp;</td>
                                            <td>实施阶段</td>
                                            <td><strong>(急单除外)</strong></td>
                                        </tr>
                                   <div id="hiddenDiv" style="display:none;">
                                        <tr>
                                            <td>&nbsp;</td>
                                            <td><span>确认阶段各相关工程师</span></td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>包装方案要求时间</td>
                                            <td>=要求发运日期-6</td>
                                        </tr>
                                        <tr>
                                            <td>&nbsp;</td>
                                            <td>要求发运日期-申请日期&gt;16天
                                            </td>
                                            <td>=要求发运日期-16天</td>
                                            <td>
                                                &nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>备货要求时间</td>
                                            <td>=要求发运日期-5</td>
                                        </tr>
                                        <tr>
                                            <td>&nbsp;</td>
                                            <td>要求发运日期 -申请日期<=16 天  </td>
                                            <td>=销售输入订单当天</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>检验要求时间</td>
                                            <td>=要求发运日期-2</td>
                                        </tr>
                                        <tr>
                                            <td>&nbsp;</td>
                                            <td>销售助理发货日期确认</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>客户特殊需求确认要求时间</td>
                                            <td>=要求发运日期</td>
                                        </tr>
                                        <tr>
                                            <td>&nbsp;</td>
                                            <td>要求发运日期 -申请日期>15 天</td>
                                            <td>=要求发运日期-15天</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>仓库发货要求时间</td>
                                            <td>=要求发运日期</td>
                                        </tr>
                                        <tr>
                                            <td>&nbsp;</td>
                                            <td>要求发运日期 -申请日期 <=15 天</td>
                                            <td>=销售输入订单当天</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>销售到货日期确认</td>
                                            <td>=要求到货日期</td>
                                        </tr>
                                        <tr>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>销售QAD订单对接</td>
                                            <td>通知发运日期-5天</td>
                                        </tr></div>
                                        <tr>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>销售订舱申请</td>
                                            <td>通知发运日期-5天</td>
                                        </tr>
                                        <tr>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>物流订舱处理</td>
                                            <td>销售申请日+3天(去除国假日)(注:比较小于3天，即为通知发运日期当天)</td>
                                        </tr>
                                        <tr>
                                            <td class="auto-style4" colspan="7">4.负责人</td>
                                        </tr>
                                        </table>
                                          <table style=" width: 100%">     
                                                
                         </table>
                                        <table style=" width: 100%">
                                   
                                            <tr>
                                            <td class="auto-style6">&nbsp;</td>
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                            <td>&nbsp;</td>
                                            <td>任务项目：</td>
                                            <td>要求完成日期</td>
                                            <td>实际完成日期</td>
                                        </tr>
                                            <td>销售助理</br></td>
                                            <td>
                                                <input id="txt_Assistant_job" class="form-control input-s-sm" style="height: 35px; width: 95px" runat="server" readonly="True" /></td>
                                            <td colspan="2">
                                                 <div class="form-inline">
                                                    <input id="txt_Assistant_id" class="input form-control input-s-sm" style="height: 35px; width: 80px" runat="server" readonly="True" />
                                                      / <input id="txt_Assistant" class="input form-control input-s-sm" style="height: 35px; width: 80px" runat="server" readonly="True" /> / <input id="txt_Assistant_AD" class="form-control input-s-sm" style="height: 35px; width:100px" runat="server" readonly="True" /></td>
                                            <td>
                                                &nbsp;</td>
                                            <td>
                                                发货日期确认：</td>
                                            </div><td>
                                                <input id="txt_Assistant_require" class="form-control input-s-sm" style="height: 35px; width:100px" runat="server" Disabled="True" onclick="laydate()"  /></td>
                                            <td>
                                                <input id="txt_Assistant_complete" class="form-control input-s-sm" style="height: 35px; width:100px" runat="server" readonly="True" /></td>
                                        </tr>
                                            
                                        <tr>
                                            <td>&nbsp;</td>
                                            <td>
                                                &nbsp;</td>
                                            <td colspan="2">
                                                 &nbsp;</td>
                                            <td>
                                                &nbsp;</td>
                                            <td>
                                                QAD订单对接：</td>
                                            <td>
                                                <input id="txt_qad_require" class="form-control input-s-sm" style="height: 35px; width:100px" runat="server" Disabled="True" onclick="laydate()"  /></td>
                                            <td>
                                                <input id="txt_qad_complete" class="form-control input-s-sm" style="height: 35px; width:100px" runat="server" Disabled="True" onclick="laydate()"  /></td>
                                        </tr>
                                        <tr>
                                            <td>&nbsp;</td>
                                            <td>
                                                &nbsp;</td>
                                            <td colspan="2">
                                                 &nbsp;</td>
                                            <td>
                                                &nbsp;</td>
                                            <td>
                                                订舱申请</td>
                                            <td>
                                                <input id="txt_dcsq_require" class="form-control input-s-sm" style="height: 35px; width:100px" runat="server" Disabled="True" onclick="laydate()"  /></td>
                                            <td>
                                                <input id="txt_dcsq_complete" class="form-control input-s-sm" style="height: 35px; width:100px" runat="server" Disabled="True" onclick="laydate()"  /></td>
                                        </tr>
                              <tr>
                                            <td>&nbsp;</td>
                                            <td>
                                                &nbsp;</td>
                                            <td colspan="2">
                                                 &nbsp;</td>
                                            <td>
                                                &nbsp;</td>
                                            <td>
                                                到货确认</td>
                                                  <td>
                                                <input id="txt_dh_date_require" class="form-control input-s-sm" style="height: 35px; width:100px" runat="server" Disabled="True" onclick="laydate()"  /></td>
                                            <td>
                                                <input id="txt_dh_date_complete" class="form-control input-s-sm" style="height: 35px; width:100px" runat="server" readonly="True" /></td>
                                        </tr>
                                        <tr>
                                            <td>物流工程师<br />
                                                </td>
                                            <td>
                                                <input id="txt_Logistics_job" class="form-control input-s-sm" style="height: 35px; width: 95px" runat="server" readonly="True" /></td>
                                            <td colspan="2"><div class="form-inline">
                                                    <input id="txt_Logistics_id" class="input form-control input-s-sm" style="height: 35px; width: 80px" runat="server" readonly="True" />/<input id="txt_Logistics" class="input form-control input-s-sm" style="height: 35px; width: 80px" runat="server" readonly="True" /></td>
                                           </div> <td>
                                                &nbsp;</td>
                                            <td>
                                                订舱处理</td>
                                            <td>
                                                <input id="txt_dccl_require" class="form-control input-s-sm" style="height: 35px; width:100px" runat="server" Disabled="True" onclick="laydate()"  /></td>
                                            <td>
                                                <input id="txt_dccl_complete" class="form-control input-s-sm" style="height: 35px; width:100px" runat="server" Disabled="True" onclick="laydate()"  /></td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td colspan="2">确认阶段</td>
                                            <td colspan="2">实施阶段</td>
                                        </tr>
                                        <tr>
                                            <td>&nbsp;</td>
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                            <td>要求完成日期</td>
                                            <td>确认完成日期</td>
                                            <td>要求完成日期</td>
                                            <td>实际完成日期</td>
                                        </tr>
                                        <tr>
                                            <td >销售工程师：</td>
                                            <td>
                                                <input id="txt_Sales_engineer_job" class="form-control input-s-sm" style="height: 35px; width: 95px" runat="server" readonly="True" />
                                            </td>
                                            <td colspan="2">
                                                <div class="form-inline">
                                                    <input id="txt_Sales_engineer_id" class="input form-control input-s-sm" style="height: 35px; width: 80px" runat="server" readonly="True" />
                                                    /
                                    <input id="txt_Sales_engineer" class="form-control input-s-sm" style="height: 35px; width:100px" runat="server" readonly="True" />
                                                    /
                                    <input id="txt_Sales_engineer_AD" class="form-control input-s-sm" style="height: 35px; width:100px" runat="server" readonly="True" /></div>
                                            </td>
                                            <td>
                                                &nbsp;</td>
                                            <td>
                                                &nbsp;</td>
                                            <td rowspan="2">&nbsp;</td>
                                            <td rowspan="2">&nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td >&nbsp;</td>
                                            <td>
                                                &nbsp;</td>
                                            <td colspan="2">
                                                <asp:RequiredFieldValidator ID="yz54" runat="server" ControlToValidate="txt_Sales_engineer_AD" ErrorMessage="不能为空" ValidationGroup="request" ForeColor="Red"></asp:RequiredFieldValidator>
                                            </td>
                                            <td>
                                                &nbsp;</td>
                                            <td>
                                                &nbsp;</td>
                                        </tr>
                                        
                                        <tr>
                                            <td>项目部</br>追踪(客户特殊要求)</td>
                                            <td>
                                                <input id="txt_project_engineer_job" class="form-control input-s-sm" style="height: 35px; width: 95px" runat="server" readonly="True" />
                                            </td>
                                            <td colspan="2">
                                                <div class="form-inline">
                                                    <input id="txt_project_engineer_id" class="input form-control input-s-sm" style="height: 35px; width: 80px" runat="server" readonly="True" />
                                                    /
                                    <input id="txt_project_engineer" class="form-control input-s-sm" style="height: 35px; width:100px" runat="server" readonly="True" />
                                                    /
                                    <input id="txt_project_engineer_AD" class="form-control input-s-sm" style="height: 35px; width:100px" runat="server" readonly="True" /></div>
                                            </td>
                                            <td>
                                                <input id="txt_special_require0" class="form-control input-s-sm" style="height: 35px; width:100px" runat="server" Disabled="True" onclick="laydate()"  /></td>
                                            <td>
                                                <input id="txt_customer_request_complete0" class="form-control input-s-sm" style="height: 35px; width:100px" runat="server" readonly="True" /></td>
                                            <td>
                                                <input id="txt_special_require" class="form-control input-s-sm" style="height: 35px; width:100px" runat="server" Disabled="True" onclick="laydate()"  /></td>
                                            <td>
                                                <input id="txt_customer_request_complete" class="form-control input-s-sm" style="height: 35px; width:100px" runat="server" readonly="True" /></td>
                                        </tr>
                                        <tr>
                                            <td>&nbsp;</td>
                                            <td>
                                                &nbsp;</td>
                                            <td colspan="2">
                                                <asp:RequiredFieldValidator ID="yz32" runat="server" ControlToValidate="txt_project_engineer_AD" ErrorMessage="不能为空" ValidationGroup="request" ForeColor="Red"></asp:RequiredFieldValidator>
                                            </td>
                                            <td>
                                                &nbsp;</td>
                                            <td>
                                                &nbsp;</td>
                                            <td>
                                                <asp:RequiredFieldValidator ID="yz39" runat="server" ControlToValidate="txt_special_require" ErrorMessage="不能为空" ValidationGroup="request" ForeColor="Red"></asp:RequiredFieldValidator>
                                            </td>
                                            <td>&nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td>物流部</br>包装方案&amp;包装备货</td>
                                            <td>
                                                <input id="txt_Packaging_engineer_job" class="form-control input-s-sm" style="height: 35px; width: 95px" runat="server" readonly="True" />
                                            </td>
                                            <td colspan="2">
                                                <div class="form-inline">
                                                    <input id="txt_Packaging_engineer_id" class="input form-control input-s-sm" style="height: 35px; width: 80px" runat="server" readonly="True" />
                                                    /
                                    <input id="txt_Packaging_engineer" class="form-control input-s-sm" style="height: 35px; width:100px" runat="server" readonly="True" />
                                                    /
                                    <input id="txt_Packaging_engineer_AD" class="form-control input-s-sm" style="height: 35px; width:100px" runat="server" readonly="True" /></div>
                                            </td>
                                            <td>
                                                <input id="txt_Packaging_require0" class="form-control input-s-sm" style="height: 35px; width:100px" runat="server" Disabled="True" onclick="laydate()" /></td>
                                            <td>
                                                <input id="txt_Packaging_complete0" class="form-control input-s-sm" style="height: 35px; width:100px" runat="server" readonly="True" /></td>
                                            <td>
                                                <input id="txt_Packaging_require" class="form-control input-s-sm" style="height: 35px; width:100px" runat="server" Disabled="True" onclick="laydate()" /></td>
                                            <td>
                                                <input id="txt_Packaging_complete" class="form-control input-s-sm" style="height: 35px; width:100px" runat="server" readonly="True" /></td>
                                        </tr>
                                        <tr>
                                            <td>&nbsp;</td>
                                            <td>
                                                &nbsp;</td>
                                            <td colspan="2">
                                                <asp:RequiredFieldValidator ID="yz33" runat="server" ControlToValidate="txt_Packaging_engineer_AD" ErrorMessage="不能为空" ValidationGroup="request" ForeColor="Red"></asp:RequiredFieldValidator>
                                            </td>
                                            <td>
                                                &nbsp;</td>
                                            <td>
                                                &nbsp;</td>
                                            <td>
                                                <asp:RequiredFieldValidator ID="yz40" runat="server" ControlToValidate="txt_Packaging_require" ErrorMessage="不能为空" ValidationGroup="request" ForeColor="Red"></asp:RequiredFieldValidator>
                                            </td>
                                            <td>&nbsp;</td>
                                        </tr>
                                        <tr id="cpgcs" runat="server">
                                            <td>工程部</br>备货（新产品）</td>
                                            <td>
                                                <input id="txt_product_engineer_job" class="form-control input-s-sm" style="height: 35px; width: 95px" runat="server" readonly="True" />
                                            </td>
                                            <td colspan="2">
                                                <div class="form-inline">
                                                    <input id="txt_product_engineer_id" class="input form-control input-s-sm" style="height: 35px; width: 80px" runat="server" readonly="True" />
                                                    /
                                    <input id="txt_product_engineer" class="form-control input-s-sm" style="height: 35px; width:100px" runat="server" readonly="True" />
                                                    /
                                    <input id="txt_product_engineer_AD" class="form-control input-s-sm" style="height: 35px; width:100px" runat="server" readonly="True" /></div>
                                            </td>
                                            <td>
                                                <input id="txt_goods_require0" class="form-control input-s-sm" style="height: 35px; width:100px" runat="server" Disabled="True" onclick="laydate()" /></td>
                                            <td>
                                                <input id="txt_goods_complete0" class="form-control input-s-sm" style="height: 35px; width:100px" runat="server" readonly="True" /></td>
                                            <td>
                                                <input id="txt_goods_require" class="form-control input-s-sm" style="height: 35px; width:100px" runat="server" Disabled="True" onclick="laydate()" /></td>
                                            <td>
                                                <input id="txt_goods_complete" class="form-control input-s-sm" style="height: 35px; width:100px" runat="server" readonly="True" /></td>
                                        </tr>
                                        <tr>
                                            <td>&nbsp;</td>
                                            <td>
                                                &nbsp;</td>
                                            <td colspan="2">
                                                <asp:RequiredFieldValidator ID="yz34" runat="server" ControlToValidate="txt_product_engineer_AD" ErrorMessage="不能为空" ValidationGroup="request" ForeColor="Red"></asp:RequiredFieldValidator>
                                            </td>
                                            <td>
                                                &nbsp;</td>
                                            <td>
                                                &nbsp;</td>
                                            <td>
                                                <asp:RequiredFieldValidator ID="yz41" runat="server" ControlToValidate="txt_goods_require" ErrorMessage="不能为空" ValidationGroup="request" ForeColor="Red"></asp:RequiredFieldValidator>
                                            </td>
                                            <td>&nbsp;</td>
                                        </tr>
                                        <tr id="wljh" runat="server">
                                            <td>物流部</br>备货（量产件）</td>
                                            <td>
                                                <input id="txt_planning_engineer_job" class="form-control input-s-sm" style="height: 35px; width: 95px" runat="server" readonly="True" />
                                            </td>
                                            <td colspan="2">
                                                <div class="form-inline">
                                                    <input id="txt_planning_engineer_id" class="input form-control input-s-sm" style="height: 35px; width: 80px" runat="server" readonly="True" />
                                                    /
                                    <input id="txt_planning_engineer" class="form-control input-s-sm" style="height: 35px; width:100px" runat="server" readonly="True" />
                                                    /
                                    <input id="txt_planning_engineer_AD" class="form-control input-s-sm" style="height: 35px; width:100px" runat="server" readonly="True" /></div>
                                            </td>
                                            <td>
                                                <input id="txt_goods_require_wl0" class="form-control input-s-sm" style="height: 35px; width:100px" runat="server" Disabled="True" onclick="laydate()"  /></td>
                                            <td>
                                                <input id="txt_goods_complete_wl0" class="form-control input-s-sm" style="height: 35px; width:100px" runat="server" readonly="True" /></td>
                                            <td>
                                                <input id="txt_goods_require_wl" class="form-control input-s-sm" style="height: 35px; width:100px" runat="server" Disabled="True" onclick="laydate()"  /></td>
                                            <td>
                                                <input id="txt_goods_complete_wl" class="form-control input-s-sm" style="height: 35px; width:100px" runat="server" readonly="True" /></td>
                                        </tr>
                                        <tr>
                                            <td>&nbsp;</td>
                                            <td>
                                                &nbsp;</td>
                                            <td colspan="2">
                                                <asp:RequiredFieldValidator ID="yz35" runat="server" ControlToValidate="txt_planning_engineer_AD" ErrorMessage="不能为空" ValidationGroup="request" ForeColor="Red"></asp:RequiredFieldValidator>
                                            </td>
                                            <td>
                                                &nbsp;</td>
                                            <td>
                                                &nbsp;</td>
                                            <td>
                                                <asp:RequiredFieldValidator ID="yz42" runat="server" ControlToValidate="txt_goods_require_wl" ErrorMessage="不能为空" ValidationGroup="request" ForeColor="Red"></asp:RequiredFieldValidator>
                                            </td>
                                            <td>&nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td>质量部</br>(检验及质量确认)</td>
                                            <td>
                                                <input id="txt_quality_engineer_job" class="form-control input-s-sm" style="height: 35px; width: 95px" runat="server" readonly="True" />
                                            </td>
                                            <td colspan="2">
                                                <div class="form-inline">
                                                    <input id="txt_quality_engineer_id" class="input form-control input-s-sm" style="height: 35px; width: 80px" runat="server" readonly="True" />
                                                    /
                                    <input id="txt_quality_engineer" class="form-control input-s-sm" style="height: 35px; width:100px" runat="server" readonly="True" />
                                                    /
                                    <input id="txt_quality_engineer_AD" class="form-control input-s-sm" style="height: 35px; width:100px" runat="server" readonly="True" /></div>
                                            </td>
                                            <td>
                                                <input id="txt_check_require0" class="form-control input-s-sm" style="height: 35px; width:100px" runat="server" Disabled="True" onclick="laydate()" /></td>
                                            <td>
                                                <input id="txt_check_complete0" class="form-control input-s-sm" style="height: 35px; width:100px" runat="server" readonly="True" /></td>
                                            <td>
                                                <input id="txt_check_require" class="form-control input-s-sm" style="height: 35px; width:100px" runat="server" Disabled="True" onclick="laydate()" /></td>
                                            <td>
                                                <input id="txt_check_complete" class="form-control input-s-sm" style="height: 35px; width:100px" runat="server" readonly="True" /></td>
                                        </tr>
                                        <tr>
                                            <td>&nbsp;</td>
                                            <td>
                                                &nbsp;</td>
                                            <td colspan="2">
                                                <asp:RequiredFieldValidator ID="yz36" runat="server" ControlToValidate="txt_quality_engineer_AD" ErrorMessage="不能为空" ValidationGroup="request" ForeColor="Red" EnableTheming="True"></asp:RequiredFieldValidator>
                                            </td>
                                            <td>
                                                &nbsp;</td>
                                            <td>
                                                &nbsp;</td>
                                            <td>
                                                <asp:RequiredFieldValidator ID="yz43" runat="server" ControlToValidate="txt_check_require" ErrorMessage="不能为空" ValidationGroup="request" ForeColor="Red"></asp:RequiredFieldValidator>
                                            </td>
                                            <td>&nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td>质量部</br>检验</td>
                                            <td>
                                                <input id="txt_checker_monitor_job" class="form-control input-s-sm" style="height: 35px; width: 95px" runat="server" readonly="True" />
                                            </td>
                                            <td colspan="2">
                                                <div class="form-inline">
                                                    <input id="txt_checker_monitor_id" class="input form-control input-s-sm" style="height: 35px; width: 80px" runat="server" readonly="True" />
                                                    /
                                    <input id="txt_checker_monitor" class="form-control input-s-sm" style="height: 35px; width:100px" runat="server" readonly="True" />
                                                    /
                                    <input id="txt_checker_monitor_AD" class="form-control input-s-sm" style="height: 35px; width:100px" runat="server" readonly="True" /></div>
                                            </td>
                                            <td>
                                                &nbsp;</td>
                                            <td>
                                                &nbsp;</td>
                                            <td>
                                                <input id="txt_check_require_jy" class="form-control input-s-sm" style="height: 35px; width:100px" runat="server" Disabled="True" onclick="laydate()"  /></td>
                                            <td>
                                                <input id="txt_check_complete_jy" class="form-control input-s-sm" style="height: 35px; width:100px" runat="server" readonly="True" /></td>
                                        </tr>
                                        <tr>
                                            <td>&nbsp;</td>
                                            <td>
                                                &nbsp;</td>
                                            <td colspan="2">
                                                &nbsp;</td>
                                            <td>
                                                &nbsp;</td>
                                            <td>
                                                &nbsp;</td>
                                            <td>
                                                <asp:RequiredFieldValidator ID="yz44" runat="server" ControlToValidate="txt_check_require_jy" ErrorMessage="不能为空" ValidationGroup="request" ForeColor="Red"></asp:RequiredFieldValidator>
                                            </td>
                                            <td>&nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td>物流部</br>仓库(包装发货)</td>
                                            <td>
                                                <input id="txt_warehouse_keeper_job" class="form-control input-s-sm" style="height: 35px; width: 95px" runat="server" readonly="True" />
                                            </td>
                                            <td colspan="2">
                                                <div class="form-inline">
                                                    <input id="txt_warehouse_keeper_id" class="input form-control input-s-sm" style="height: 35px; width: 80px" runat="server" readonly="True" />
                                                    /
                                    <input id="txt_warehouse_keeper" class="form-control input-s-sm" style="height: 35px; width:100px" runat="server" readonly="True" />
                                                    /
                                    <input id="txt_warehouse_keeper_AD" class="form-control input-s-sm" style="height: 35px; width:100px" runat="server" readonly="True" /></div>
                                            </td>
                                            <td>
                                                &nbsp;</td>
                                            <td>
                                                &nbsp;</td>
                                            <td>
                                                <input id="txt_shipping_require" class="form-control input-s-sm" style="height: 35px; width:100px" runat="server" Disabled="True" onclick="laydate()" /></td>
                                            <td>
                                                <input id="txt_shipping_complete" class="form-control input-s-sm" style="height: 35px; width:100px" runat="server" readonly="True" /></td>
                                        </tr>
                                        <tr>
                                            <td>送检</td>
                                            <td>
                                                <input id="txt_warehouse_keeper_job_sj" class="form-control input-s-sm" style="height: 35px; width: 95px" runat="server" readonly="True" /></td>
                                            <td colspan="2"><div class="form-inline">
                                                    <input id="txt_warehouse_keeper_id_sj" class="input form-control input-s-sm" style="height: 35px; width: 80px" runat="server" readonly="True" /> / <input id="txt_warehouse_keeper_sj" class="form-control input-s-sm" style="height: 35px; width:100px" runat="server" readonly="True" /> / <input id="txt_warehouse_keeper_AD_sj" class="form-control input-s-sm" style="height: 35px; width:100px" runat="server" readonly="True" />
                                                </div>
                                            </td>
                                            <td>
                                                &nbsp;</td>
                                            <td>
                                                &nbsp;</td>
                                            <td>
                                                <asp:RequiredFieldValidator ID="yz45" runat="server" ControlToValidate="txt_shipping_require" ErrorMessage="不能为空" ValidationGroup="request" ForeColor="Red"></asp:RequiredFieldValidator>
                                            </td>
                                            <td>
                                                &nbsp;</td>
                                        </tr>
                                  
                                 
                                        <tr>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>
                                                <asp:Button ID="BTN_Sales_Assistant1" runat="server" class="btn btn-primary" style="height: 35px; width: 135px" Text="提交" OnClick="BTN_Sales_Assistant1_Click" ValidationGroup="request" />
                                            </td>
                                        </tr>

                                    </Table>

                                </fieldset>
                                <fieldset style="border-color: lightblue">
                                    <legend>二.通知发运日期&amp;QAD订单对接&amp;订舱</legend>
                                    <table>
                                        <tr>
                                            <td>1.通知发运日期</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td>通知发运日期</td>
                                            <td>
                                               
                                                <input id="txt_tzfy_date" class="form-control input-s-sm" style="height: 35px; width:200px" runat="server"  onclick="laydate()"  /></td>
                                            <td>
                                                <asp:RequiredFieldValidator ID="yz50" runat="server" ControlToValidate="txt_tzfy_date" ErrorMessage="通知发运日期不能为空" ValidationGroup="Sales_Assistant2" ForeColor="Red"></asp:RequiredFieldValidator>
                                            </td>
                                        
                                            <td>
                                                <asp:Label ID="lab_bb" runat="server"></asp:Label>
                                            </td>
                                                <td class="auto-style7">
                                                    <strong>检查产品状态请点击右图</strong></td>
                                            <td>
                                                    <img name="selectljh0" style="border: 0px;" src="../images/fdj.gif" alt="select" onclick="GetXMH2()" /></td>
                                        </tr>
                                        <tr>
                                            <td>&nbsp;</td>
                                            <td>
                                               
                                                &nbsp;</td>
                                            <td>
                                                &nbsp;</td>
                                            <td>
                                                &nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>
                                                <asp:Button ID="BTN_Sales_Assistant2" runat="server" class="btn btn-primary" style="height: 35px; width: 135px" Text="提交" ValidationGroup="Sales_Assistant2" OnClick="BTN_Sales_Assistant2_Click" />
                                            </td>
                                        </tr>
                                           <tr>
                                            <td>2.生成QAD订单</td>
                                            <td>
                                            <asp:Button ID="BTN_Sales_Assistant2_CS" runat="server" class="btn btn-primary" Text="生成QAD订单" OnClick="BTN_Sales_Assistant2_CS_Click" Width="190px" />
                                            </td>
                                            <td>
                                            <asp:Label ID="txt_iscsdd" runat="server"></asp:Label>
                                                <asp:Label ID="txt_iscsdd_sm" runat="server" Text="Label"></asp:Label>
                                            </td>
                                            <td>
                                                <input id="txt_qaddh" class="form-control input-s-sm" style="height: 35px; width: 150px" runat="server"  readonly="True" /></td>
                                            <td>&nbsp;</td>
                                            <td>
                                                &nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td>3.订舱信息</td>
                                            <td>
                                                &nbsp;</td>
                                            <td>
                                                &nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td>订舱发运日期</td>
                                            <td>
                                                <input id="txt_dcfy_date" class="form-control input-s-sm" style="height: 35px; width: 150px" runat="server"  readonly="True" /></td>
                                            <td>
                                                订舱单号</td>
                                            <td>
                                                <input id="txt_dch" class="form-control input-s-sm" style="height: 35px; width: 150px" runat="server"  readonly="True" /></td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td>订舱处理日期</td>
                                            <td>
                                                <input id="txt_dccl_date" class="form-control input-s-sm" style="height: 35px; width: 150px" runat="server"  readonly="True" /></td>
                                            <td>
                                                处理人</td>
                                            <td>
                                                <input id="txt_dcr" class="form-control input-s-sm" style="height: 35px; width: 150px" runat="server"  readonly="True" /></td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                        </tr>
                                                                             
                                    </table>
                                </fieldset>
                                <fieldset style="border-color: lightblue">
                                    <legend>三.到货</legend>
                                    <table style=" width: 100%">
                                        <tr>
                                            <td>1.确认到货</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td>到货日期</td>
                                            <td>
                                                <input id="txt_dh_date" class="form-control input-s-sm" style="height: 35px; width: 150px" runat="server" disabled="disabled"  />
                                            </td>
                                            <td>
                                                <asp:RequiredFieldValidator ID="yz53" runat="server" ControlToValidate="txt_tzfy_date" ErrorMessage="到货日期不能为空" ValidationGroup="Sales_Assistant3" ForeColor="Red"></asp:RequiredFieldValidator>
                                            </td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>
                                                <asp:Button ID="BTN_Sales_Assistant3" runat="server" class="btn btn-primary" style="height: 35px; width: 135px" Text="提交" ValidationGroup="Sales_Assistant3" OnClick="BTN_Sales_Assistant3_Click" />
                                            </td>
                                        </tr>                                     
                                    </table>
                                </fieldset>
                                <div class="form-inline" style="margin-top:2px">
                                    <label>提交说明</label>                                         
                                    <input id="txt_content_Sales_Assistant" class="form-control input-s-sm" style="width: 80%" runat="server" /><asp:RequiredFieldValidator ID="yz47" runat="server" ControlToValidate="txt_content_Sales_Assistant" ErrorMessage="提交说明不能为空" ValidationGroup="request" ForeColor="Red"></asp:RequiredFieldValidator>
                                    <asp:RequiredFieldValidator ID="yz51" runat="server" ControlToValidate="txt_content_Sales_Assistant" ErrorMessage="提交说明不能为空" ValidationGroup="Sales_Assistant2" ForeColor="Red"></asp:RequiredFieldValidator>
                                    <asp:RequiredFieldValidator ID="yz52" runat="server" ControlToValidate="txt_content_Sales_Assistant" ErrorMessage="提交说明不能为空" ValidationGroup="Sales_Assistant3" ForeColor="Red"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <%--  <div class="panel-body collapse" id="ZLGCS">--%>
        <div class="row  row-container">
            <div class="col-md-12">
                <div class="panel panel-info">
                    <div class="panel-heading" data-toggle="collapse" data-target="#XMGCS">
                        <strong>项目工程师--》1.订单确认 2.客户特殊要求确认  </strong><span class="caret"></span>
                    </div>
                    <div class="panel-body <% =ViewState["lv"].ToString() == "XMGCS" ? "" : "collapse" %>" id="XMGCS">
                        <div class="col-xs-12 col-sm-12  col-md-12 col-lg-12">
                            <fieldset style="border-color: lightblue">
                                <legend>一.订单确认</legend>
                                <table style=" width: 100%">
                                    <tr>
                                        <td>选择产品状态 :</td>
                                        <td> <div class="form-inline">
                                            <asp:DropDownList ID="txt_cp_status" class="form-control input-s-sm" Style=" width: 200px" runat="server"></asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="yz48" runat="server" ControlToValidate="txt_cp_status" ErrorMessage="产品状态不能为空" ValidationGroup="Project_Engineer1" ForeColor="Red"></asp:RequiredFieldValidator>
                                       </div> </td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td>客户特殊要求：</td>
                                        <td colspan="5">
                                            <input id="txt_special_yq" class="form-control input-s-sm" style=" width: 95%" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>要求附件：
                                        </td>
                                        <td colspan="5">
                                            <div class="form-inline">
                                                &nbsp;<asp:GridView ID="gvFile_special_fj" runat="server" AutoGenerateColumns="False" DataKeyNames="id" OnRowDeleting="gvFile_special_fj_RowDeleting" CellPadding="4" ForeColor="#333333" GridLines="None">
                                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                                    <Columns>
                                                        <asp:BoundField ApplyFormatInEditMode="True" DataField="id" HeaderText="No." ShowHeader="False" />
                                                        <asp:TemplateField HeaderText="文件名称" ShowHeader="False">
                                                            <ItemTemplate>
                                                                <asp:HyperLink ID="HyperLink1" runat="server" Target="_blank" NavigateUrl='<% #DataBinder.Eval(Container.DataItem, "File_lj")%>' Text='<%#DataBinder.Eval(Container.DataItem,"File_name")%>'></asp:HyperLink>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField ShowHeader="False" HeaderText="">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="false" CommandName="Delete" Text="刪除"></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <EditRowStyle BackColor="#999999" />
                                                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                                    <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                                    <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                                    <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                                    <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                                                </asp:GridView>
                                                <input id="txt_special_fj" type="file" class="form-control" style="width: 90px" multiple="multiple" runat="server" />
                                                <asp:Button ID="Btn_special_fj" runat="server" CssClass="form-control" Text="上传文件" OnClick="Btn_special_fj_Click" />
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>
                                            <asp:Button ID="BTN_Project_Engineer1" runat="server" class="btn btn-primary" Style=" width: 100px" Text="提交" ValidationGroup="Project_Engineer1" OnClick="BTN_Project_Engineer1_Click" />
                                        </td>
                                        <td>&nbsp;</td>
                                    </tr>
                                </table>
                            </fieldset>
                            <fieldset>
                                <legend>二.包装发货</legend>
                                <table>
                                    <tr>
                                        <td>客户特殊要求确认</td>
                                        <td><div class="form-inline">
                                            <asp:DropDownList ID="txt_customer_request" class="form-control input-s-sm" style="height: 35px; width: 150px" runat="server"></asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="yz49" runat="server" ControlToValidate="txt_customer_request" ErrorMessage="客户特殊要求确认不能为空" ValidationGroup="Project_Engineer2" ForeColor="Red"></asp:RequiredFieldValidator>
                                        </div></td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>
                                            <asp:Button ID="BTN_Project_Engineer2" runat="server" class="btn btn-primary" style="height: 35px; width: 135px" Text="提交" ValidationGroup="Project_Engineer2" OnClick="BTN_Project_Engineer2_Click" />
                                        </td>
                                        <td>&nbsp;</td>
                                    </tr>                                    
                                </table>  
                            </fieldset>
                            
                            <div class="form-inline" style="margin-top:10px">
                                <label>提交说明</label>                                         
                                <input id="txt_content_Project_Engineer" class="form-control input-s-sm" style="width: 80%" runat="server" /><asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txt_content_Project_Engineer" ErrorMessage="提交说明不能为空" ValidationGroup="Project_Engineer1" ForeColor="Red"></asp:RequiredFieldValidator>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txt_content_Project_Engineer" ErrorMessage="提交说明不能为空" ValidationGroup="Project_Engineer2" ForeColor="Red"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <%--<input id="txt_status_id"  runat="server" readonly="true" />--%>
        <div class="row  row-container"  style="display:<% =ViewState["V_cpgcs"].ToString() %>">
            <div class="col-md-12">
                <div class="panel panel-info">
                    <div class="panel-heading" data-toggle="collapse" data-target="#CPGCS">
                        <strong>产品工程师--》1.订单确认 2.备货</strong>
                        <span class="caret"></span>
                    </div>
                    <div class="panel-body <% =ViewState["lv"].ToString() == "CPGCS" ? "" : "collapse" %>" id="CPGCS">
                        <div class="col-xs-12 col-sm-12  col-md-12 col-lg-12">
                            <fieldset>
                                <legend>一.订单确认</legend>
                                <table style="width: 100%">
                                    <tr>
                                        <td>1.检测要求（成品入库后对于此次发货的检验要求）</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td>检测项</td>
                                        <td>是否需要检测</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td>6件全尺寸测量</td>
                                        <td><asp:DropDownList ID="txt_is_check_qcc" class="form-control input-s-sm" style="height: 35px; width: 150px" runat="server"></asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txt_is_check_qcc" ErrorMessage="不能为空" ValidationGroup="Product_Engineer1" ForeColor="Red"></asp:RequiredFieldValidator>
                                        </td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td>三坐标全检</td>
                                        <td>
                                            <asp:DropDownList ID="txt_is_check_szb" class="form-control input-s-sm" style="height: 35px; width: 150px" runat="server"></asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ControlToValidate="txt_is_check_szb" ErrorMessage="不能为空" ValidationGroup="Product_Engineer1" ForeColor="Red"></asp:RequiredFieldValidator>
                                        </td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td>外观全检</td>
                                        <td>
                                            <asp:DropDownList ID="txt_is_check_wg" class="form-control input-s-sm" style="height: 35px; width: 150px" runat="server"></asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ControlToValidate="txt_is_check_wg" ErrorMessage="不能为空" ValidationGroup="Product_Engineer1" ForeColor="Red"></asp:RequiredFieldValidator>
                                        </td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td>检具全检</td>
                                        <td>
                                            <asp:DropDownList ID="txt_is_check_jj" class="form-control input-s-sm" style="height: 35px; width: 150px" runat="server"></asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" ControlToValidate="txt_is_check_jj" ErrorMessage="不能为空" ValidationGroup="Product_Engineer1" ForeColor="Red"></asp:RequiredFieldValidator>
                                        </td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td>其它描述</td>
                                        <td>
                                            <input id="txt_check_other_ms" class="form-control input-s-sm" style="height: 35px; width: 150px" runat="server" /></td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td>2.检验路径</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td>选择检验路径</td>
                                        <td>
                                            <asp:DropDownList ID="txt_check_lj" class="form-control input-s-sm" style="height: 35px; width: 150px" runat="server" AutoPostBack="True" OnSelectedIndexChanged="txt_check_lj_SelectedIndexChanged"></asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" ControlToValidate="txt_check_lj" ErrorMessage="不能为空" ValidationGroup="Product_Engineer1" ForeColor="Red"></asp:RequiredFieldValidator>
                                        </td>
                                        <td>&nbsp;</td>
                                        <td>确认备货完成日期：</td>
                                        <td><div class="form-inline">
                                            <asp:DropDownList ID="txt_Already_Product" class="form-control input-s-sm" style="height: 35px; width: 150px" runat="server"></asp:DropDownList>
                                           <asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server" ControlToValidate="txt_Already_Product" ErrorMessage="不能为空" ValidationGroup="Product_Engineer1" ForeColor="Red"></asp:RequiredFieldValidator>
                                        </div></td>
                                    </tr>
                                    <tr>
                                        <td>检验路径说明：</td>
                                        <td colspan="5">
                                            <asp:Label ID="lb_ljsm_cp" runat="server" ForeColor="Red"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td></td>
                                        <td>
                                            <asp:Button ID="BTN_Product_Engineer1" runat="server" class="btn btn-primary" style="height: 35px; width: 135px" Text="提交" ValidationGroup="Product_Engineer1" OnClick="BTN_Product_Engineer1_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                            <fieldset>
                                <legend>二.备货</legend>
                                <table>
                                    <tr>
                                        <td>参考号/批次号</td>
                                        <td><div class="form-inline">
                                            <asp:DropDownList ID="txt_reference_number" class="form-control input-s-sm" style="height: 35px; width: 150px" runat="server" AutoPostBack="True" OnSelectedIndexChanged="txt_reference_number_SelectedIndexChanged"></asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator19" runat="server" ControlToValidate="txt_reference_number" ErrorMessage="不能为空" ValidationGroup="Product_Engineer2" ForeColor="Red"></asp:RequiredFieldValidator>
                                            <asp:Label ID="Lb_txt_reference_number" runat="server" Text="Label"></asp:Label>
                                        </div></td>
                                        <td>备货数量</td>
                                        <td><div class="form-inline">
                                            <input id="txt_Stocking_quantity" class="form-control input-s-sm" style="height: 35px; width: 150px" runat="server" /><asp:RequiredFieldValidator ID="RequiredFieldValidator22" runat="server" ControlToValidate="txt_Stocking_quantity" ErrorMessage="不能为空" ValidationGroup="Product_Engineer2" ForeColor="Red"></asp:RequiredFieldValidator>
                                       </div> </td>
                                        <td>送检单号</td>
                                        <td>
                                          <div class="form-inline">
                                            <asp:TextBox id="txt_Check_number" class="form-control input-s-sm" style="height: 35px; width: 150px"  runat="server" AutoPostBack="True" OnTextChanged="txt_Check_number_TextChanged"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator21" runat="server" ControlToValidate="txt_Check_number" ErrorMessage="不能为空" ValidationGroup="Product_Engineer2" ForeColor="Red"></asp:RequiredFieldValidator>
                                        </div></td>
                                    </tr>
                                    <tr>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td></td>
                                        <td>
                                            <asp:Button ID="BTN_Product_Engineer2" runat="server" class="btn btn-primary" style="height: 35px; width: 135px" Text="提交" ValidationGroup="Product_Engineer2" OnClick="BTN_Product_Engineer2_Click" />
                                        </td>
                                    </tr>                                   
                                </table>
                            </fieldset>
                            <div class="fieldset form-inline" style="margin-top:10px">
                                <label>提交说明</label>                                  
                                    <input id="txt_content_Product_Engineer" class="form-control input-s-sm" style="width: 80%"  runat="server" /><asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txt_content_Product_Engineer" ErrorMessage="提交说明不能为空" ValidationGroup="Product_Engineer1" ForeColor="Red"></asp:RequiredFieldValidator>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator23" runat="server" ControlToValidate="txt_content_Product_Engineer" ErrorMessage="提交说明不能为空" ValidationGroup="Product_Engineer2" ForeColor="Red"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <%--操作日志--%>
        <div class="row  row-container">
            <div class="col-md-12">
                <div class="panel panel-info">
                    <div class="panel-heading" data-toggle="collapse" data-target="#BZGCS">
                        <strong>包装工程师--》1.订单确认 2.包材备货</strong>
                        <span class="caret"></span>
                    </div>
                    <div class="panel-body <% =ViewState["lv"].ToString() == "BZGCS" ? "" : "collapse" %>" id="BZGCS">

                        <div class="col-xs-12 col-sm-12  col-md-12 col-lg-12">
                            一.包材备货及包装方案
                        <fieldset>
                                <legend>1.包装方案</legend>
                            <table style="height: 35px; width: 100%">
                                <tr>
                                    <td>包装箱1</td>
                                    <td><div class="form-inline">
                                        <asp:DropDownList ID="txt_Box_specifications1" class="form-control input-s-sm" style="height: 35px; width: 135px" runat="server" AutoPostBack="True" OnSelectedIndexChanged="txt_Box_specifications1_SelectedIndexChanged"></asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator24" runat="server" ControlToValidate="txt_Box_specifications1" ErrorMessage="至少选择一只包装箱" ValidationGroup="Packaging_Engineer1" ForeColor="Red"></asp:RequiredFieldValidator>
</div>
                                    </td>
                                    <td>其他描述1</td>
                                    <td>
                                        <input id="txt_Other_box_dec1" class="form-control input-s-sm" style="height: 35px; width: 135px" runat="server" /></td>
                                    <td>重量1</td>
                                    <td><div class="form-inline">
                                        <input id="txt_Box_weight1" class="form-control input-s-sm" style="height: 35px; width: 80px" runat="server" /><asp:RequiredFieldValidator ID="RequiredFieldValidator25" runat="server" ControlToValidate="txt_Box_weight1" ErrorMessage="不可为空" ValidationGroup="Packaging_Engineer1" ForeColor="Red"></asp:RequiredFieldValidator>
                                        </div>
                                    </td>
                                    <td>
                                        每箱数量</td>
                                    <td><div class="form-inline">
                                        <input id="txt_Per_Crate_Qty" class="form-control input-s-sm" style="height: 35px; width: 80px" runat="server"  /><asp:RequiredFieldValidator ID="RequiredFieldValidator66" runat="server" ControlToValidate="txt_Per_Crate_Qty" ErrorMessage="不可为空" ValidationGroup="Packaging_Engineer1" ForeColor="Red"></asp:RequiredFieldValidator>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td>包装箱2</td>
                                    <td>
                                        <asp:DropDownList ID="txt_Box_specifications2" class="form-control input-s-sm" style="height: 35px; width: 135px" runat="server" AutoPostBack="True" OnSelectedIndexChanged="txt_Box_specifications2_SelectedIndexChanged"></asp:DropDownList>
                                    </td>
                                    <td>其他描述2</td>
                                    <td>
                                        <input id="txt_Other_box_dec2" class="form-control input-s-sm" style="height: 35px; width:135px" runat="server" /></td>
                                    <td>重量2</td>
                                    <td>
                                        <input id="txt_Box_weight2" class="form-control input-s-sm" style="height: 35px; width: 80px" runat="server" /></td>
                                    <td>
                                        每箱数量 &nbsp;</td>
                                    <td>
                                        <input id="txt_Per_Crate_Qty2" class="form-control input-s-sm" style="height: 35px; width:80px" runat="server"  /></td>
                                </tr>
                                <tr>
                                    <td>包装箱重量(合计)</td>
                                    <td><div class="form-inline">
                                        <input id="txt_Box_weight_total" class="form-control input-s-sm" style="height: 35px; width: 135px" runat="server" disabled="true" /><asp:RequiredFieldValidator ID="RequiredFieldValidator26" runat="server" ControlToValidate="txt_Box_weight_total" ErrorMessage="不可为空" ValidationGroup="Packaging_Engineer1" ForeColor="Red"></asp:RequiredFieldValidator>
                                        </div>
                                    </td>
                                    <td>箱子数量</td>
                                    <td><div class="form-inline">
                                        <input id="txt_Box_quantity" class="form-control input-s-sm" style="height: 35px; width: 135px" runat="server" readonly="true" /><asp:RequiredFieldValidator ID="RequiredFieldValidator27" runat="server" ControlToValidate="txt_Box_quantity" ErrorMessage="不可为空" ValidationGroup="Packaging_Engineer1" ForeColor="Red"></asp:RequiredFieldValidator>
                                        </div>
                                    </td>
                                    <td>&nbsp;</td>
                                    <td colspan="3">
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td>包装净重<br>(要货数量X零件重量)</td>
                                    <td><div class="form-inline">
                                        <input id="txt_Packing_net_weight" class="form-control input-s-sm" style="height: 35px; width: 135px" runat="server" readonly="true" /><asp:RequiredFieldValidator ID="RequiredFieldValidator28" runat="server" ControlToValidate="txt_Packing_net_weight" ErrorMessage="不可为空" ValidationGroup="Packaging_Engineer1" ForeColor="Red"></asp:RequiredFieldValidator>
                                    </div></td>
                                    <td>包装毛重<br>(包装净重+包装箱重量)</td>
                                    <td><div class="form-inline">
                                        <input id="txt_Package_weight" class="form-control input-s-sm" style="height: 35px; width: 135px" runat="server" disabled="true" /><asp:RequiredFieldValidator ID="RequiredFieldValidator29" runat="server" ControlToValidate="txt_Package_weight" ErrorMessage="不可为空" ValidationGroup="Packaging_Engineer1" ForeColor="Red"></asp:RequiredFieldValidator>
                                    </div></td>
                                    <td>&nbsp;</td>
                                    <td colspan="3">&nbsp;</td>
                                </tr>
                                <tr>
                                    <td>包装方案说明</td>
                                    <td colspan="7">
                                        <input id="txt_Packaging_scheme" class="form-control input-s-sm" style="height: 35px; width: 95%" runat="server" /></td>
                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td colspan="3">
                                        <asp:Button ID="BTN_Packaging_Engineer1" runat="server" class="btn btn-primary" style="height: 35px; width: 135px" Text="提交" ValidationGroup="Packaging_Engineer1" OnClick="BTN_Packaging_Engineer1_Click" />
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                            <fieldset>
                                <legend>2.包材备货</legend>
                                <table>
                                    <tr>
                                        <td></td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td>包材备货完成</td>
                                        <td> <asp:DropDownList ID="txt_Packing_goods_Already" class="form-control input-s-sm" style="height: 35px; width: 150px" runat="server"></asp:DropDownList>

                                        </td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator31" runat="server" ControlToValidate="txt_Packing_goods_Already" ErrorMessage="不可为空" ValidationGroup="Packaging_Engineer2" ForeColor="Red"></asp:RequiredFieldValidator>

                                        </td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>
                                            <asp:Button ID="BTN_Packaging_Engineer2" runat="server" class="btn btn-primary" style="height: 35px; width: 135px" Text="提交" ValidationGroup="Packaging_Engineer2" OnClick="BTN_Packaging_Engineer2_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                            <div class="fieldset form-inline" style="margin-top:10px">
                                <label>提交说明</label>                                  
                                   <input id="txt_content_Packaging_Engineer" class="form-control input-s-sm" style="width: 80%" runat="server" /><asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txt_content_Packaging_Engineer" ErrorMessage="提交说明不能为空" ValidationGroup="Packaging_Engineer1" ForeColor="Red"></asp:RequiredFieldValidator>
                                   <asp:RequiredFieldValidator ID="RequiredFieldValidator30" runat="server" ControlToValidate="txt_content_Packaging_Engineer" ErrorMessage="提交说明不能为空" ValidationGroup="Packaging_Engineer2" ForeColor="Red"></asp:RequiredFieldValidator>
                                    
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <%--  <div class="panel-body collapse" id="ZLGCS">--%>
        <div class="row  row-container" style="display:<% =ViewState["V_wljh"].ToString() %>">
            <div class="col-md-12">
                <div class="panel panel-info">
                    <div class="panel-heading" data-toggle="collapse" data-target="#WLJH">
                        <strong>物流计划--》1.订单确认2.备货</strong>
                        <span class="caret"></span>
                    </div>
                    <div class="panel-body <% =ViewState["lv"].ToString() == "WLJH" ? "" : "collapse" %>" id="WLJH">
                        <div class="col-xs-12 col-sm-12  col-md-12 col-lg-12">
                            <fieldset>
                                <legend>一.订单确认</legend>
                                <table style="height: 35px; width: 100%">
                                    <tr>
                                        <td>确认备货完成日期</td>
                                        <td>
                                            <asp:DropDownList ID="txt_Already_Planning" class="form-control input-s-sm" style="height: 35px; width: 150px" runat="server"></asp:DropDownList>

                                        </td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator32" runat="server" ControlToValidate="txt_Already_Planning" ErrorMessage="不可为空" ValidationGroup="Planning_Engineer1" ForeColor="Red"></asp:RequiredFieldValidator>

                                        </td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>
                                            <asp:Button ID="BTN_Planning_Engineer1" runat="server" class="btn btn-primary" style="height: 35px; width: 135px" Text="提交" ValidationGroup="Planning_Engineer1" OnClick="BTN_Planning_Engineer1_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                            <fieldset>
                                <legend>二.备货</legend>
                                <table>
                                    <tr>
                                        <td>库存数量</td>
                                        <td><div class="form-inline">
                                            <input id="txt_Current_inventory_quantity" class="form-control input-s-sm" style="height: 35px; width: 150px" runat="server" readonly="true" /><asp:RequiredFieldValidator ID="RequiredFieldValidator34" runat="server" ControlToValidate="txt_Current_inventory_quantity" ErrorMessage="不可为空" ValidationGroup="Planning_Engineer2" ForeColor="Red"></asp:RequiredFieldValidator>
                                            </div>
                                        </td>
                                        <td>库存日期</td>
                                        <td><div class="form-inline">
                                            <input id="txt_Current_inventory_date" class="form-control input-s-sm" style="height: 35px; width: 150px" runat="server" readonly="true" /><asp:RequiredFieldValidator ID="RequiredFieldValidator35" runat="server" ControlToValidate="txt_Current_inventory_date" ErrorMessage="不可为空" ValidationGroup="Planning_Engineer2" ForeColor="Red"></asp:RequiredFieldValidator>
</div>
                                        </td>
                                        <td>备货数量</td>
                                        <td><div class="form-inline">
                                            <input id="txt_Stocking_quantity_wl" class="form-control input-s-sm" style="height: 35px; width: 150px" runat="server"   readonly="true" /><asp:RequiredFieldValidator ID="RequiredFieldValidator36" runat="server" ControlToValidate="txt_Stocking_quantity_wl" ErrorMessage="不可为空" ValidationGroup="Planning_Engineer2" ForeColor="Red"></asp:RequiredFieldValidator>
                                            </div>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>
                                            <asp:Button ID="BTN_Planning_Engineer2" runat="server" class="btn btn-primary" style="height: 35px; width: 135px" Text="提交" ValidationGroup="Planning_Engineer2" OnClick="BTN_Planning_Engineer2_Click" />
                                        </td>
                                    </tr>                                    
                                </table>
                            </fieldset>
                            <div class="fieldset form-inline" style="margin-top:10px">
                                <label>提交说明</label>                                  
                                    <input id="txt_content_Planning_Engineer" class="form-control input-s-sm" style="width: 80%" runat="server" /><asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txt_content_Planning_Engineer" ErrorMessage="提交说明不能为空" ValidationGroup="Planning_Engineer1" ForeColor="Red"></asp:RequiredFieldValidator>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator33" runat="server" ControlToValidate="txt_content_Planning_Engineer" ErrorMessage="提交说明不能为空" ValidationGroup="Planning_Engineer2" ForeColor="Red"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <%--<input id="txt_status_id"  runat="server" readonly="true" />--%>
        <div class="row  row-container">
            <div class="col-md-12">
                <div class="panel panel-info">
                    <div class="panel-heading" data-toggle="collapse" data-target="#CKBZ">
                        <strong>仓库班长--&gt;1.送检2.检验完取货3.包装发货</strong>
                        <span class="caret"></span>
                    </div>
                    <div class="panel-body <% =ViewState["lv"].ToString() == "CKBZ" ? "" : "collapse" %>" id="CKBZ">
                        <div class="col-xs-12 col-sm-12  col-md-12 col-lg-12">
                            <fieldset>
                                <legend>一.送检</legend>
                                <table style="height: 35px; width: 100%">
                                    <tr>
                                        <td>在库分选单</td>
                                        <td><div class="form-inline">
                                            <input id="txt_Sorting_list" class="form-control input-s-sm" style="height: 35px; width: 150px" runat="server" readonly="true" />
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator37" runat="server" ControlToValidate="txt_Sorting_list" ErrorMessage="不可为空" ValidationGroup="Warehouse_Keeper1" ForeColor="Red"></asp:RequiredFieldValidator>
                                        </div></td>
                                        <td>
                                            <asp:Button ID="BTN_Warehouse_Keeper_DY" runat="server" class="btn btn-primary" style="height: 35px; width: 135px" Text="打印" OnClick="BTN_Warehouse_Keeper_DY_Click" />
                                            <asp:Label ID="txt_isdy" runat="server"></asp:Label>
                                        </td>
                                        <td>&nbsp;</td>
                                        <td>已送检验</td>
                                        <td><div class="form-inline">
                                            <asp:DropDownList ID="txt_Already_Check" class="form-control input-s-sm" style="height: 35px; width: 150px" runat="server"></asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator38" runat="server" ControlToValidate="txt_Already_Check" ErrorMessage="不可为空" ValidationGroup="Warehouse_Keeper1" ForeColor="Red"></asp:RequiredFieldValidator>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>
                                            <asp:Button ID="BTN_Warehouse_Keeper1" runat="server" class="btn btn-primary" style="height: 35px; width: 135px" Text="提交" ValidationGroup="Warehouse_Keeper1" OnClick="BTN_Warehouse_Keeper1_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                            <fieldset>
                                <legend>二.检验完取货</legend>
                                <table>
                                    <tr>
                                        <td>取回数量</td>
                                        <td>
                                            <input id="txt_Warehouse_quantity" class="form-control input-s-sm" style="height: 35px; width: 150px" runat="server" /><asp:RequiredFieldValidator ID="RequiredFieldValidator39" runat="server" ControlToValidate="txt_Warehouse_quantity" ErrorMessage="不可为空" ValidationGroup="Warehouse_Keeper2" ForeColor="Red"></asp:RequiredFieldValidator>

                                        </td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                    </tr>

                                    <tr>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>
                                            <asp:Button ID="BTN_Warehouse_Keeper2" runat="server" class="btn btn-primary" style="height: 35px; width: 135px" Text="提交" ValidationGroup="Warehouse_Keeper2" OnClick="BTN_Warehouse_Keeper2_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                            <fieldset>
                                <legend>三.包装发货</legend>
                                <table>
                                    <tr>
                                        <td>1.上传包装照片(外箱；产品；打包完成；单据 四张照片)：
                                        </td>
                                        <td colspan="5">
                                            <div class="form-inline">
                                                &nbsp;<asp:GridView ID="gvFile_Package_photo" runat="server" AutoGenerateColumns="False" DataKeyNames="id" OnRowDeleting="gvFile_Package_photo_RowDeleting" CellPadding="4" ForeColor="#333333" GridLines="None">
                                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                                    <Columns>
                                                        <asp:BoundField ApplyFormatInEditMode="True" DataField="id" HeaderText="No." ShowHeader="False" />
                                                        <asp:TemplateField HeaderText="文件名称" ShowHeader="False">
                                                            <ItemTemplate>
                                                                <asp:HyperLink ID="HyperLink1" runat="server"  NavigateUrl='<% #DataBinder.Eval(Container.DataItem, "File_lj")%>' Text='<%#DataBinder.Eval(Container.DataItem,"File_name")%>'></asp:HyperLink>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField ShowHeader="False" HeaderText="">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="false" CommandName="Delete" Text="刪除"></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                    </Columns>
                                                    <EditRowStyle BackColor="#999999" />
                                                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                                    <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                                    <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                                    <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                                    <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                                                </asp:GridView>
                                                <input id="txt_Package_photo" type="file" class="form-control" style="width: 90px" multiple="multiple" runat="server" /><asp:Button ID="Btn_Package_photo" runat="server" CssClass="form-control" Text="上传文件" OnClick="Btn_Package_photo_Click" />
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>2.上传发货照片(装车后照片&amp;装箱出库单签字照片&amp;在库分选单照片)：
                                        </td>
                                        <td colspan="5">
                                            <div class="form-inline">
                                                &nbsp;<asp:GridView ID="gvFile_Shipping_photos" runat="server" AutoGenerateColumns="False" DataKeyNames="id" OnRowDeleting="gvFile_Shipping_photos_RowDeleting" CellPadding="4" ForeColor="#333333" GridLines="None">
                                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                                    <Columns>
                                                        <asp:BoundField ApplyFormatInEditMode="True" DataField="id" HeaderText="No." ShowHeader="False" />
                                                        <asp:TemplateField HeaderText="文件名称" ShowHeader="False">
                                                            <ItemTemplate>
                                                                <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<% #DataBinder.Eval(Container.DataItem, "File_lj")%>' Text='<%#DataBinder.Eval(Container.DataItem,"File_name")%>'></asp:HyperLink>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField ShowHeader="False" HeaderText="">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="false" CommandName="Delete" Text="刪除"></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <EditRowStyle BackColor="#999999" />
                                                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                                    <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                                    <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                                    <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                                    <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                                                </asp:GridView>
                                                <input id="txt_Shipping_photos" type="file" class="form-control" style="width: 90px" multiple="multiple" runat="server" /><asp:Button ID="Btn_Shipping_photos" runat="server" CssClass="form-control" Text="上传文件" OnClick="Btn_Shipping_photos_Click" />
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>
                                            <asp:Button ID="BTN_Warehouse_Keeper3" runat="server" class="btn btn-primary" style="height: 35px; width: 135px" Text="提交" ValidationGroup="Warehouse_Keeper3" OnClick="BTN_Warehouse_Keeper3_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                              <fieldset>
                                <legend>四.标签打印</legend> <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional"><ContentTemplate>
                                   <table style="width: 100%">
                                    <tr>
                                        <td>打印标签，请选择
                                        </td>
                                        <td>
                                            <div class="form-inline">
                                            <asp:DropDownList ID="txt_bqdy" class="form-control input-s-sm" style="height: 35px; width: 150px" runat="server" AutoPostBack="True"></asp:DropDownList>
                                            </div>
                                        </td>
                                        <td>
                                            <asp:Label ID="Lab_dyslms" runat="server" Text="打印数量" Visible="False"></asp:Label>
                                        </td>
                                        <td>
                                            <input id="txt_dysl" class="form-control input-s-sm" style="height: 35px; width: 80px" runat="server" visible="False" /></td>
                                        <td>
                                            <asp:Button ID="Btn_dybq" runat="server" class="btn btn-primary" style="height: 35px; width: 135px" Text="打印" OnClick="Btn_dybq_Click" />
                                        </td>
                                    </tr>
                                    </table>
                                    </ContentTemplate> </asp:UpdatePanel>
                            </fieldset>
                            <div class="fieldset form-inline" style="margin-top:10px">
                                <label>提交说明</label>                                  
                                   <input id="txt_content_Warehouse_Keeper" class="form-control input-s-sm" style="width: 80%" runat="server" /><asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txt_content_Warehouse_Keeper" ErrorMessage="提交说明不能为空" ValidationGroup="Warehouse_Keeper1" ForeColor="Red"></asp:RequiredFieldValidator>
                                   <asp:RequiredFieldValidator ID="RequiredFieldValidator40" runat="server" ControlToValidate="txt_content_Warehouse_Keeper" ErrorMessage="提交说明不能为空" ValidationGroup="Warehouse_Keeper2" ForeColor="Red"></asp:RequiredFieldValidator>
                                   <asp:RequiredFieldValidator ID="RequiredFieldValidator41" runat="server" ControlToValidate="txt_content_Warehouse_Keeper" ErrorMessage="提交说明不能为空" ValidationGroup="Warehouse_Keeper3" ForeColor="Red"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <%--操作日志--%>
        <div class="row  row-container">
            <div class="col-md-12">
                <div class="panel panel-info">
                    <div class="panel-heading" data-toggle="collapse" data-target="#JYBZ">
                        <strong>检验班长--》1.检验</strong>
                        <span class="caret"></span>
                    </div>

                    <%--  <div class="panel-body collapse" id="ZLGCS">--%>
                    <div class="panel-body <% =ViewState["lv"].ToString() == "JYBZ" ? "" : "collapse" %>" id="JYBZ">

                        <div class="col-xs-12 col-sm-12  col-md-12 col-lg-12">
                            <table style="height: 35px; width: 100%">
                                <tr>
                                    <td>一.检验</td>
                                    <td></td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td>合格数量</td>
                                    <td><div class="form-inline">
                                        <input id="txt_Qualified_quantity" class="form-control input-s-sm" style="height: 35px; width: 150px" runat="server" /><asp:RequiredFieldValidator ID="RequiredFieldValidator42" runat="server" ControlToValidate="txt_Qualified_quantity" ErrorMessage="不可为空" ValidationGroup="Checker_Monitor" ForeColor="Red"></asp:RequiredFieldValidator>
                                        </div>
                                    </td>
                                    <td>不合格数量</td>
                                    <td><div class="form-inline">
                                        <input id="txt_Unqualified_quantity" class="form-control input-s-sm" style="height: 35px; width: 150px" runat="server" /><asp:RequiredFieldValidator ID="RequiredFieldValidator43" runat="server" ControlToValidate="txt_Unqualified_quantity" ErrorMessage="不可为空" ValidationGroup="Checker_Monitor" ForeColor="Red"></asp:RequiredFieldValidator>
                                        </div>
                                    </td>
                                    <td>描述</td>
                                    <td><div class="form-inline">
                                        <input id="txt_Unqualified_description" class="form-control input-s-sm" style="height: 35px; width: 150px" runat="server" /><asp:RequiredFieldValidator ID="RequiredFieldValidator44" runat="server" ControlToValidate="txt_Unqualified_description" ErrorMessage="不可为空" ValidationGroup="Checker_Monitor" ForeColor="Red"></asp:RequiredFieldValidator>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>
                                        <asp:Button ID="BTN_Checker_Monitor" runat="server" class="btn btn-primary" style="height: 35px; width: 135px" Text="提交" ValidationGroup="Checker_Monitor" OnClick="BTN_Checker_Monitor_Click" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>提交说明</td>
                                    <td colspan="5">
                                        <input id="txt_content_Checker_Monitor" class="form-control input-s-sm" style="width: 80%"  runat="server" /><asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txt_content_Checker_Monitor" ErrorMessage="提交说明不能为空" ValidationGroup="Checker_Monitor" ForeColor="Red"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <%--<input id="txt_status_id"  runat="server" readonly="true" />--%>
        <div class="row  row-container">
            <div class="col-md-12">
                <div class="panel panel-info">
                    <div class="panel-heading" data-toggle="collapse" data-target="#ZLGCS">
                        <strong>质量工程师--1.订单确认2.订单确认（检验交期）3.送检 4.检验报告</strong>
                        <span class="caret "></span>
                    </div>                   
                    <div class="panel-body <% =ViewState["lv"].ToString() == "ZLGCS" ? "" : "collapse" %>" id="ZLGCS">
                        <div class="col-xs-12 col-sm-12  col-md-12 col-lg-12">
                            <fieldset id="V_zlgcs1" runat="server" >
                                <legend>一.订单确认</legend>
                                <table style="width: 100%">
                                    <tr>
                                        <td>1.检测要求（成品入库后对于此次发货的检验要求）</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td>检测项</td>
                                        <td>是否需要检测</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td>6件全尺寸测量</td>
                                        <td>
                                            <asp:DropDownList ID="txt_is_check_qcc_zl" class="form-control input-s-sm" style="height: 35px; width: 150px" runat="server"></asp:DropDownList>

                                        </td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator45" runat="server" ControlToValidate="txt_is_check_qcc_zl" ErrorMessage="不可为空" ValidationGroup="Quality_Engineer1" ForeColor="Red"></asp:RequiredFieldValidator>

                                        </td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td>三坐标全检</td>
                                        <td>
                                            <asp:DropDownList ID="txt_is_check_szb_zl" class="form-control input-s-sm" style="height: 35px; width: 150px" runat="server"></asp:DropDownList>

                                        </td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator50" runat="server" ControlToValidate="txt_is_check_szb_zl" ErrorMessage="不可为空" ValidationGroup="Quality_Engineer1" ForeColor="Red"></asp:RequiredFieldValidator>

                                        </td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td>外观全检</td>
                                        <td>
                                            <asp:DropDownList ID="txt_is_check_wg_zl" class="form-control input-s-sm" style="height: 35px; width: 150px" runat="server"></asp:DropDownList>

                                        </td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator51" runat="server" ControlToValidate="txt_is_check_wg_zl" ErrorMessage="不可为空" ValidationGroup="Quality_Engineer1" ForeColor="Red"></asp:RequiredFieldValidator>

                                        </td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td>检具全检</td>
                                        <td>
                                            <asp:DropDownList ID="txt_is_check_jj_zl" class="form-control input-s-sm" style="height: 35px; width: 150px" runat="server"></asp:DropDownList>

                                        </td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator52" runat="server" ControlToValidate="txt_is_check_jj_zl" ErrorMessage="不可为空" ValidationGroup="Quality_Engineer1" ForeColor="Red"></asp:RequiredFieldValidator>

                                        </td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td>其它描述</td>
                                        <td>
                                            <input id="txt_check_other_ms_zl" class="form-control input-s-sm" style="height: 35px; width: 150px" runat="server" /></td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td>2.检验路径</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td>选择检验路径</td>
                                        <td>
                                            <asp:DropDownList ID="txt_check_lj_zl" class="form-control input-s-sm" style="height: 35px; width: 150px" runat="server" AutoPostBack="True" OnSelectedIndexChanged="txt_check_lj_zl_SelectedIndexChanged"></asp:DropDownList>

                                        </td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator49" runat="server" ControlToValidate="txt_check_lj_zl" ErrorMessage="不可为空" ValidationGroup="Quality_Engineer1" ForeColor="Red"></asp:RequiredFieldValidator>

                                        </td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td>检验路径说明</td>
                                        <td colspan="5">
                                            <asp:Label ID="lb_ljsm_zl" runat="server" ForeColor="Red"></asp:Label>

                                        </td>
                                    </tr>
                                    <tr>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td></td>
                                        <td>
                                            <asp:Button ID="BTN_Quality_Engineer1" runat="server" class="btn btn-primary" style="height: 35px; width: 135px" Text="提交" ValidationGroup="Quality_Engineer1" OnClick="BTN_Quality_Engineer1_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                            <fieldset>
                                <legend>二.确认检验完成日期：</legend>
                                <table>
                                    <tr>
                                        <td></td>
                                        <td>
                                            <asp:DropDownList ID="txt_Already_zl" class="form-control input-s-sm" style="height: 35px; width: 150px" runat="server"></asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator53" runat="server" ControlToValidate="txt_Already_zl" ErrorMessage="不可为空" ValidationGroup="Quality_Engineer2" ForeColor="Red"></asp:RequiredFieldValidator>
                                        </td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>
                                            <asp:Button ID="BTN_Quality_Engineer2" runat="server" class="btn btn-primary" style="height: 35px; width: 135px" Text="提交" ValidationGroup="Quality_Engineer2" OnClick="BTN_Quality_Engineer2_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                            <fieldset id="V_zlgcs3" runat="server">
                                <legend>三.检验--送检</legend>
                                <table style=" width: 100%">
                                    <tr>
                                        <td>参考号/批次号</td>
                                        <td><div class="form-inline">
                                            <asp:DropDownList ID="txt_reference_number_zl" class="form-control input-s-sm" style="height: 35px; width: 150px" runat="server"></asp:DropDownList>
                                            <asp:Label ID="Lb_txt_reference_number_zl" runat="server" Text="Label"></asp:Label>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator54" runat="server" ControlToValidate="txt_reference_number_zl" ErrorMessage="不可为空" ValidationGroup="Quality_Engineer3" ForeColor="Red"></asp:RequiredFieldValidator>
                                        </div></td>
                                        <td>送检单号</td>
                                        <td>
                                          <div class="form-inline">
                                            <asp:TextBox id="txt_Check_number_zl" class="form-control input-s-sm" style="height: 35px; width: 150px"  runat="server" AutoPostBack="True" OnTextChanged="txt_Check_number_zl_TextChanged"></asp:TextBox>
&nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator55" runat="server" ControlToValidate="txt_Check_number_zl" ErrorMessage="不可为空" ValidationGroup="Quality_Engineer3" ForeColor="Red"></asp:RequiredFieldValidator>
</div>
                                        </td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td></td>
                                        <td>
                                            <asp:Button ID="BTN_Quality_Engineer3" runat="server" class="btn btn-primary" style="height: 35px; width: 135px" Text="提交" ValidationGroup="Quality_Engineer3" OnClick="BTN_Quality_Engineer3_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                            <fieldset>
                                <legend>四.检验--检验报告</legend>
                                <table style=" width: 100%">
                                   <tr>
                                        <td>上传检测报告：
                                        </td>
                                        <td colspan="5">
                                            <div class="form-inline">
                                                &nbsp;<asp:GridView ID="gvFile_check_fj" runat="server" AutoGenerateColumns="False" DataKeyNames="id" OnRowDeleting="gvFile_check_fj_RowDeleting" CellPadding="4" ForeColor="#333333" GridLines="None">
                                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                                    <Columns>
                                                        <asp:BoundField ApplyFormatInEditMode="True" DataField="id" HeaderText="No." ShowHeader="False" />
                                                        <asp:TemplateField HeaderText="文件名称" ShowHeader="False">
                                                            <ItemTemplate>
                                                                <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<% #DataBinder.Eval(Container.DataItem, "File_lj")%>' Text='<%#DataBinder.Eval(Container.DataItem,"File_name")%>'></asp:HyperLink>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField ShowHeader="False" HeaderText="">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="false" CommandName="Delete" Text="刪除"></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                    </Columns>
                                                    <EditRowStyle BackColor="#999999" />
                                                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                                    <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                                    <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                                    <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                                    <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                                                </asp:GridView>
                                                <input id="txt_check_fj" type="file" class="form-control" style="width: 90px" multiple="multiple" runat="server" /><asp:Button ID="Btn_check_fj" runat="server" CssClass="form-control" Text="上传文件" OnClick="Btn_check_fj_Click" />
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>确认合格数量</td>
                                        <td><div class="form-inline">
                                            <input id="txt_Confirm_quantity" class="form-control input-s-sm" style="height: 35px; width: 150px" runat="server" /><asp:RequiredFieldValidator ID="RequiredFieldValidator60" runat="server" ControlToValidate="txt_Confirm_quantity" ErrorMessage="不可为空" ValidationGroup="Quality_Engineer4" ForeColor="Red"></asp:RequiredFieldValidator>
                                        </div></td>
                                        <td>
                                            不合格数量</td>
                                        <td><div class="form-inline">
                                            <input id="txt_Unqualified_quantity_zl" class="form-control input-s-sm" style="height: 35px; width: 150px" runat="server" /><asp:RequiredFieldValidator ID="RequiredFieldValidator61" runat="server" ControlToValidate="txt_Unqualified_quantity_zl" ErrorMessage="不可为空" ValidationGroup="Quality_Engineer4" ForeColor="Red"></asp:RequiredFieldValidator>
                                        </div></td>
                                        <td class="auto-style3">&nbsp;</td>
                                        <td>&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td>&nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                        <td class="auto-style3">&nbsp;</td>
                                        <td>
                                            <asp:Button ID="BTN_Quality_Engineer4" runat="server" class="btn btn-primary" style="height: 35px; width: 135px" Text="提交" ValidationGroup="Quality_Engineer4" OnClick="BTN_Quality_Engineer4_Click" />
                                        </td>
                                    </tr>
                                    <tr id="gy_zl" runat="server">
                                        <td>流程干预：</td>
                                        <td>
                                            <div class="form-inline">
                                            <asp:DropDownList ID="txt_Process_intervention_zl" class="form-control input-s-sm" style="height: 35px; width: 150px" runat="server"></asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator62" runat="server" ControlToValidate="txt_Process_intervention_zl" ErrorMessage="不可为空" ValidationGroup="gy_zl" ForeColor="Red"></asp:RequiredFieldValidator>
                                       </div> </td>
                                        <td>
                                            干预说明</td>
                                        <td colspan="3">
                                    <input id="txt_gysm_zl" class="form-control input-s-sm" style="height: 35px; width: 85%" runat="server" /><asp:RequiredFieldValidator ID="RequiredFieldValidator63" runat="server" ControlToValidate="txt_gysm_zl" ErrorMessage="不可为空" ValidationGroup="gy_zl" ForeColor="Red"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td class="auto-style3">&nbsp;</td>
                                        <td>
                                            <asp:Button ID="BTN_Process_intervention_zl" runat="server" class="btn btn-primary" style="height: 35px; width: 135px" Text="确认流程干预至销售" ValidationGroup="gy_zl" OnClick="BTN_Process_intervention_zl_Click" />
                                        </td>
                                    </tr>                                    
                                </table>
                            </fieldset>
                            <div class="fieldset form-inline" style="margin-top:10px">
                                <label>提交说明</label>                                  
                                    <input id="txt_content_Quality_Engineer" class="form-control input-s-sm" style="width: 80%" runat="server" /><asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txt_content_Quality_Engineer" ErrorMessage="提交说明不能为空" ValidationGroup="Quality_Engineer1" ForeColor="Red"></asp:RequiredFieldValidator>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator57" runat="server" ControlToValidate="txt_content_Quality_Engineer" ErrorMessage="提交说明不能为空" ValidationGroup="Quality_Engineer2" ForeColor="Red"></asp:RequiredFieldValidator>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator58" runat="server" ControlToValidate="txt_content_Quality_Engineer" ErrorMessage="提交说明不能为空" ValidationGroup="Quality_Engineer3" ForeColor="Red"></asp:RequiredFieldValidator>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator59" runat="server" ControlToValidate="txt_content_Quality_Engineer" ErrorMessage="提交说明不能为空" ValidationGroup="Quality_Engineer4" ForeColor="Red"></asp:RequiredFieldValidator>
                            </div>
                        </div>

                        &nbsp;</div>
                </div>
            </div>
        </div>
        <%--操作日志--%>
        <div class="row  row-container">
            <div class="col-md-12">
                <div class="panel panel-info">
                    <div class="panel-heading" data-toggle="collapse" data-target="#CZRZ">
                        <strong>操作日志</strong>
                        <span class="caret"></span>
                    </div>
                    <%--  <div class="panel-body collapse" id="ZLGCS">--%>
                    <div class="panel-body <% =ViewState["lv"].ToString() == "CZRZ" ? "" : "collapse" %>" id="CZRZ">

                        <div class="col-xs-12 col-sm-12  col-md-12 col-lg-12">
                            <table style="height: 35px; width: 100%">
                                <tr>
                                    <td>一.操作人员记录</td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:GridView ID="GridView1" Width="100%"
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
                                                <asp:BoundField DataField="status_ms" HeaderText="订单状态"
                                                    ReadOnly="True">
                                                    <HeaderStyle Wrap="True" />
                                                    <ItemStyle Width="90px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Update_Engineer_MS" HeaderText="操作节点"
                                                    ReadOnly="True">
                                                    <HeaderStyle Wrap="True" />
                                                    <ItemStyle Width="150px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="gcs_name" HeaderText="操作节点名称"
                                                    ReadOnly="True">
                                                    <HeaderStyle Wrap="True" />
                                                    <ItemStyle Width="100px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="lastname" HeaderText="操作人员"
                                                    ReadOnly="True">
                                                    <HeaderStyle Wrap="True" />
                                                    <ItemStyle Width="80px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Receive_time" HeaderText="接收时间"
                                                    ReadOnly="True">
                                                    <HeaderStyle Wrap="True" />
                                                    <ItemStyle Width="150px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Commit_time" HeaderText="处理时间">
                                                    <HeaderStyle Wrap="False" />
                                                    <ItemStyle Width="150px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Operation_time" HeaderText="耗时">
                                                    <HeaderStyle Wrap="False" />
                                                    <ItemStyle Width="100px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Update_content" HeaderText="提交说明">
                                                    <HeaderStyle Wrap="False" />
                                                </asp:BoundField>
                                            </Columns>
                                        </asp:GridView>
                                        &nbsp;</td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div class="col-md-3 ">
        <div class="panel panel-info">
            <div class="panel-heading">
                <strong>订单当前阶段:</strong>
            </div>
            <div class="panel-body  " id="DDXX">
                <div>
                    <div class="">
                        <table border="1" width="100%">
                            <tr>
                                <td colspan="2">
                                    <div class="form-inline" style="background-color: lightblue">
                                        <%--<input id="txt_status_id"  runat="server" readonly="true" />--%>
                        当前阶段:
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:Label ID="txt_status_id" runat="server"></asp:Label>
                                    :<asp:Label ID="txt_status_name" runat="server" Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Size="Large" Font-Strikeout="False" Font-Underline="True" ForeColor="#6600CC"></asp:Label></td>
                            </tr>
                            <tr>
                                <td colspan="2" style="background-color: lightblue">具体信息:</td>
                            </tr>
                            <tr>
                                <td colspan="2">

                                    <asp:GridView ID="GridView2" Width="100%"
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
                                            <asp:BoundField DataField="status_id" HeaderText="ID"
                                                ReadOnly="True" Visible="False">
                                                <HeaderStyle Wrap="True" />
                                                <ItemStyle Width="10%" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="status_ms" HeaderText="订单信息">
                                                <HeaderStyle Wrap="False" />
                                                <ItemStyle Width="20%" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Commit_time" HeaderText="完成日期">
                                                <HeaderStyle Wrap="False" />
                                                <ItemStyle Width="50%" />
                                            </asp:BoundField>
                                        </Columns>
                                    </asp:GridView>
                                </td>

                            </tr>
                            </table>
                            <table border="1" style="width:100%">
                            <tr>
                                <td style="background-color: lightblue">

                                    <asp:Label ID="LB_Process_intervention" runat="server" Text="销售干预流程"></asp:Label>
                                </td>

                            </tr>

                            <tr>
                                <td>

                                    <asp:DropDownList ID="txt_Process_intervention" class="form-control input-s-sm" style="height: 35px; width: 150px" runat="server"></asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator64" runat="server" ControlToValidate="txt_Process_intervention" ErrorMessage="不可为空" ValidationGroup="gy" ForeColor="Red"></asp:RequiredFieldValidator>
                                </td>

                            </tr>

                            <tr>
                                <td>

                                    <input id="txt_gysm" class="form-control input-s-sm" style="height: 35px; width: 85%" runat="server" /><asp:RequiredFieldValidator ID="RequiredFieldValidator65" runat="server" ControlToValidate="txt_gysm" ErrorMessage="干预原因不可为空" ValidationGroup="gy" ForeColor="Red"></asp:RequiredFieldValidator>
                                </td>

                            </tr>

                            <tr>
                                <td>

                                    <asp:Button ID="BTN_Process_intervention" runat="server" class="btn btn-primary" style="height: 35px; width: 135px" Text="确认" OnClick="BTN_Process_intervention_Click" ValidationGroup="gy" />
                                </td>

                            </tr>

                            <tr>
                                <td>

                                        <asp:GridView ID="GridView3" Width="100%"
                                            AllowMultiColumnSorting="True" AllowPaging="True"
                                            AllowSorting="True" AutoGenerateColumns="False"
                                            runat="server" Font-Size="Small" GridLines="None" OnRowDataBound="GridView3_RowDataBound">
                                            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                            <PagerSettings FirstPageText="首页" LastPageText="尾页"
                                                Mode="NextPreviousFirstLast" NextPageText="下页" PreviousPageText="上页" />
                                            <PagerStyle ForeColor="Red" />
                                            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                            <EditRowStyle BackColor="#999999" />
                                            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />

                                            <Columns>
                                              
                                                        <asp:TemplateField HeaderText="订单状态" >  
                   <HeaderStyle Width="10px" Wrap="False" HorizontalAlign="Left" />              
                    <ItemTemplate>
                         <asp:ImageButton ID="Image1" runat="server" ImageUrl='~/Images/downloads.png' Width="30"  Height="30" onmouseover="this.style.cursor='hand'" Visible="true" OnClick="Image1_Click" />
                         <asp:ImageButton ID="Image2" runat="server" ImageUrl='~/Images/exclamation-red.png' Width="20"  Height="20" onmouseover="this.style.cursor='hand'" Visible="true" OnClick="Image2_Click"  />
                    </ItemTemplate>
                       <ItemStyle Width="10px" Height="40px" VerticalAlign="Bottom" HorizontalAlign="Left" />

                </asp:TemplateField>
                                                   <asp:BoundField DataField="Update_Engineer_MS2" HeaderText="订单状态"
                                                    ReadOnly="True">
                                                    <HeaderStyle Wrap="True" HorizontalAlign="Right" />
                                                    <ItemStyle Width="160px" VerticalAlign="Bottom" HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Commit_time" HeaderText="时间"
                                                    ReadOnly="True">
                                                    <HeaderStyle Wrap="True" HorizontalAlign="Right" />
                                                    <ItemStyle Width="160px" VerticalAlign="Bottom" HorizontalAlign="Right" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="lastname" HeaderText="人员"
                                                    ReadOnly="True">
                                                    <HeaderStyle Wrap="True" HorizontalAlign="Right" />
                                                    <ItemStyle Width="70px" VerticalAlign="Bottom" HorizontalAlign="Right" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="RequireDate" HeaderText="要求时间"
                                                    ReadOnly="True">
                                                    <HeaderStyle Wrap="True" HorizontalAlign="Right" />
                                                    <ItemStyle Width="160px" VerticalAlign="Bottom" HorizontalAlign="Right" />
                                                </asp:BoundField>
                                                    <asp:BoundField DataField="Update_Status" HeaderText="状态"
                                                    ReadOnly="True">
                                                    <HeaderStyle Wrap="True" HorizontalAlign="Right" />
                                                    <ItemStyle Width="160px" VerticalAlign="Bottom" HorizontalAlign="Right" />
                                                </asp:BoundField>
                                                 <asp:BoundField DataField="lv" HeaderText="窗口"
                                                    ReadOnly="True">
                                                    <HeaderStyle Wrap="True" HorizontalAlign="Right" />
                                                    <ItemStyle Width="160px" VerticalAlign="Bottom" HorizontalAlign="Right" />
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
