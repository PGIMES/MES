<%@ Page Title="MES��������ϵͳ" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
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
            $("#mestitle").text("��PGI�����������̡�");
            //ͼƬ���´���
            $("a[href]").click(function(){
                var url=this.href.toLowerCase();
                var id=this.id;
                var pathName=this.pathname;
                if(url.indexOf('.jpg')>0||url.indexOf('.bmp')>0||url.indexOf('.png')>0)
                {
                    $("#"+id).attr('href','javascript:void(0)');
                    layer.open({
                        type: 1,
                        skin: 'layui-layer-demo', //��ʽ����
                        closeBtn: 1, //��ʾ�رհ�ť
                        anim: 2,
                        area: ['1000px', '700px'],
                        shadeClose: true, //�������ֹر�
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
                fix: false, //���̶�
                maxmin: false,
                title: ['�ڿ��ѡ��(���޷�ֱ�Ӵ�ӡ,��ת��������ʽ�ٴ�ӡ.)', false],
                closeBtn: 1,
                content: 'PrintFenJianDan.aspx?Code=' + code+'&FLR='+flr,
                end: function () {                   
                }
            });
        }

    </script>

        <script type="text/javascript">
        $(document).ready(function () {
            $("#mestitle").text("��PGI�����������̡�");

        })//end ready
        function openwindFord(requestid,dyym,PrintCount){        
            //layer.open({
            //    shade: [0.5, '#000', false],
            //    type: 2,
            //    offset: '20',
            //    area: ['1200px', '600px'],
            //    fix: false, //���̶�
            //    maxmin: false,
            //    title: ['��ǩ(���޷�ֱ�Ӵ�ӡ,��ת��������ʽ�ٴ�ӡ.)', false],
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
            $("#mestitle").text("��PGI�����������̡�");

        })//end ready
        function openwindother(requestid,dyym){        
            layer.open({
                shade: [0.5, '#000', false],
                type: 2,
                offset: '20px',
                area: ['1200px', '700px'],
                fix: false, //���̶�
                maxmin: false,
                title: ['��ǩ(���޷�ֱ�Ӵ�ӡ,��ת��������ʽ�ٴ�ӡ.)', false],
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
                    choose: function (dates) { //ѡ������ڵĻص�                       
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
        //�ջ���ַ
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
        <%--������־--%>
        <div class="row row-container">
            <div class="col-md-12">
                <div class="panel panel-info">
                    <div class="panel-heading" data-toggle="collapse" data-target="#SQXX">
                        <strong>��������Ϣ</strong>
                    </div>
                    <%--  <div class="panel-body collapse" id="ZLGCS">--%>
                    <div class="panel-body <% =ViewState["lv"].ToString() == "SQXX" ? "" : "collapse" %>" id="SQXX">
                        <div class="col-xs-12 col-sm-12  col-md-12 col-lg-12">
                            <div class="">
                                <asp:UpdatePanel ID="UpdatePanel_request" runat="server">
                                    <ContentTemplate>
                                        <table style="height: 35px; width: 100%">
                                            <tr>
                                                <td>�����ˣ�
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
                                                <td>���ţ�
                                                </td>
                                                <td>
                                                    <input id="txt_dept" class="form-control input-s-sm" style="height: 35px; width: 100px" runat="server" readonly="True" />
                                                    &nbsp;</td>
                                                <td>���ž���
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
                                                <td style="word-break: ">����������ţ�
                                                </td>
                                                <td>
                                                    <input id="txt_Code" class="form-control input-s-sm" style="height: 35px; width: 150px" runat="server" readonly="True" />
                                                </td>
                                                <td>&nbsp;</td>
                                                <td>

                                                    <input id="txt_update_user" class="input form-control input-s-sm" style="height: 35px; width: 90px" runat="server" readonly="True" />
                                                </td>
                                                <td>�������ڣ�
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
                        <strong>��������--��1.������Ϣ 2.����3.����</strong>
                    </div>
                    <%--������־--%>
                    <div class="panel-body " id="XSZL">
                        <div class="col-xs-12 col-sm-12  col-md-12 col-lg-12">

                            <div>
                                <fieldset style="border-color: lightblue">
                                    <legend>һ.��������</legend>
                                    <table style="height: 35px; width: 100%">
                                        <tr>
                                            <td>1.������Ϣ</td>
                                            <td></td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td>���빤����</td>
                                            <td>
                                                <div class="form-inline">
                                                    <input id="txt_domain" class="form-control input-s-sm" style="height: 35px; width: 50px" runat="server" readonly="True" /><input id="txt_sqgc" class="form-control input-s-sm" style="height: 35px; width: 135px" runat="server" readonly="True" />
                                                </div>
                                            </td>
                                            <td>���칤��:</td>
                                            <td><div class="form-inline">
                                                    <input id="txt_domain_zzgc" class="form-control input-s-sm" style="height: 35px; width: 50px" runat="server" readonly="True" /><input id="txt_zzgc" class="form-control input-s-sm" style="height: 35px; width: 135px" runat="server" readonly="True" /> <asp:RequiredFieldValidator ID="yz56" runat="server" ControlToValidate="txt_domain_zzgc" ErrorMessage="����Ϊ��" ValidationGroup="request" ForeColor="Red"></asp:RequiredFieldValidator>
                                                </div></td>
                                            <td>�����յ����ڣ�
                                            </td>
                                            <td><div class="form-inline">
                                                <input id="txt_sddd_date" class="form-control input-s-sm" style="height: 35px; width: 150px" runat="server" onclick="laydate()" /><asp:RequiredFieldValidator ID="yz16" runat="server" ControlToValidate="txt_gkddh" ErrorMessage="����Ϊ��" ValidationGroup="request" ForeColor="Red"></asp:RequiredFieldValidator>
                                                </div></td>
                                        </tr>
                                        <tr>
                                            <td>�˿Ͷ����ţ�
                                            </td>
                                            <td><div class="form-inline">
                                                <input id="txt_gkddh" class="form-control input-s-sm" style="height: 35px; width: 150px" runat="server" /><asp:RequiredFieldValidator ID="yz15" runat="server" ControlToValidate="txt_gkddh" ErrorMessage="����Ϊ��" ValidationGroup="request" ForeColor="Red"></asp:RequiredFieldValidator>
                                             </div></td>
                                            <td>������:</td>
                                            <td>
                                                <input id="txt_Line_Code" class="form-control input-s-sm" style="height: 35px; width: 150px" runat="server" /></td>
                                            <td>&nbsp;</td>
                                            <td>
                                                &nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td>����������
                                            </td>
                                            <td colspan="5">
                                                <div class="form-inline">
                                                    <asp:GridView ID="gvFile_ddfj" runat="server" AutoGenerateColumns="False" DataKeyNames="id" OnRowDeleting="gvFile_ddfj_RowDeleting" CellPadding="4" ForeColor="#333333" GridLines="None">
                                                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                                        <Columns>
                                                            <asp:BoundField ApplyFormatInEditMode="True" DataField="id" HeaderText="No." ShowHeader="False" />
                                                            <asp:TemplateField HeaderText="�ļ�����" ShowHeader="False">
                                                                <ItemTemplate>
                                                                    <asp:HyperLink ID="HyperLink1" runat="server" Target="_blank" NavigateUrl='<% #DataBinder.Eval(Container.DataItem, "File_lj")%>'  Text='<%#DataBinder.Eval(Container.DataItem,"File_name")%>'></asp:HyperLink>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField ShowHeader="False" HeaderText="">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="false" CommandName="Delete" Text="�h��"></asp:LinkButton>
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
                                                    <input id="txt_ddfj" type="file" class="form-control" style="width: 90px" multiple="multiple" runat="server" /><asp:Button ID="Btn_ddfj" runat="server" CssClass="form-control" Text="�ϴ��ļ�" OnClick="Btn_ddfj_Click" />
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>ѡ������ţ�</td>
                                            <td>
                                                <div class="form-inline">
                                                    <img name="selectljh" style="border: 0px;" src="../images/fdj.gif" alt="select" onclick="GetXMH()" /><input id="txt_ljh" class="form-control input-s-sm" style="height: 35px; width: 135px" runat="server" readonly="True" />&nbsp;&nbsp;<asp:TextBox ID="txt_CP_ID" Style="height: 0px; width: 0px" runat="server" AutoPostBack="True" OnTextChanged="txt_CP_ID_TextChanged"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="yz46" runat="server" ControlToValidate="txt_ljh" ErrorMessage="��ѡ�����" ValidationGroup="request" ForeColor="Red"></asp:RequiredFieldValidator>
                                                   </div>                                               
                                            </td>
                                            <td>PG����ţ�</td>
                                            <td><input id="txt_xmh" class="form-control input-s-sm" style="height: 35px; width: 150px" runat="server" readonly="True" />
                                            </td>
                                            <td>������ƣ�</td>
                                            <td>
                                                <input id="txt_ljmc" class="form-control input-s-sm" style="height: 35px; width: 150px" runat="server" readonly="True" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>���������</td>
                                            <td>
                                                <input id="txt_ljzl" class="form-control input-s-sm" style="height: 35px; width: 150px" runat="server" readonly="True" />
                                            </td>
                                            <td>��������</td>
                                            <td>
                                                <input id="txt_fhz" class="form-control input-s-sm" style="height: 35px; width: 150px" runat="server" readonly="True" />
                                            </td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td>Ҫ��������</td>
                                            <td>    <div class="form-inline">
                                                <asp:TextBox ID="txt_yhsl" class="form-control input-s-sm" style="height: 35px; width: 150px" runat="server" AutoPostBack="True" OnTextChanged="txt_yhsl_TextChanged"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="yz17" runat="server" ControlToValidate="txt_yhsl" ErrorMessage="����Ϊ��" ValidationGroup="request" ForeColor="Red"></asp:RequiredFieldValidator>
                                               </div> </td>
                                            <td>QAD�������</td>
                                            <td>
                                                <input id="txt_kcl" class="form-control input-s-sm" style="height: 35px; width: 150px" runat="server" readonly="True" />
                                            </td>
                                            <td>������ڣ�</td>
                                            <td>
                                                <input id="txt_kc_date" class="form-control input-s-sm" style="height: 35px; width: 150px" runat="server" readonly="True" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td >ͼֽ���ڣ�</td>
                                            <td >
                                                <input id="txt_tz_date" class="form-control input-s-sm" style="height: 35px; width: 150px" runat="server" onclick="laydate()" />&nbsp;</td>
                                            <td >Ҫ�󵽻����ڣ�</td>
                                            <td > <div class="form-inline">
                                                <input id="txt_yqdh_date" class="form-control input-s-sm" style="height: 35px; width: 150px" runat="server" onclick="laydate()" /><asp:RequiredFieldValidator ID="yz19" runat="server" ControlToValidate="txt_yqdh_date" ErrorMessage="����Ϊ��" ValidationGroup="request" ForeColor="Red"></asp:RequiredFieldValidator>
                                                </div></td>
                                            <td ></td>
                                            <td ></td>
                                        </tr>
                                        <tr>
                                            <td>2.������Ϣ</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                        </tr>

                                        <tr>
                                            <td>�ͻ����룺</td>
                                            <td><div class="form-inline">
                                                <input id="txt_gkdm" class="form-control input-s-sm" style="height: 35px; width: 150px" runat="server" readonly="True" /><asp:RequiredFieldValidator ID="yz20" runat="server" ControlToValidate="txt_gkdm" ErrorMessage="����Ϊ��" ValidationGroup="request" ForeColor="Red"></asp:RequiredFieldValidator>
                                                 </div> </td>
                                            <td>�ͻ����ƣ�
                                            </td>
                                            <td><div class="form-inline">
                                                <input id="txt_gkmc" class="form-control input-s-sm" style="height: 35px; width: 150px" runat="server" readonly="True" /><asp:RequiredFieldValidator ID="yz21" runat="server" ControlToValidate="txt_gkmc" ErrorMessage="����Ϊ��" ValidationGroup="request" ForeColor="Red"></asp:RequiredFieldValidator>
                                                </div> </td>
                                            <td>&nbsp;</td>
                                            <td>
                                                &nbsp;</td>
                                        </tr>

                                        <tr>
                                            <td>QAD����</td>
                                            <td>
                                                <asp:TextBox ID="txt_ljdj_qad" class="form-control input-s-sm" style="height: 35px; width: 150px" runat="server"  BorderColor="#00CCFF" readonly="True"></asp:TextBox>
                                                </td>
                                            <td>
                                                <asp:Label ID="Lab_ljdj_rq" runat="server" Text="�۸�������" Visible="False"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txt_ljdj_rq" class="form-control input-s-sm" style="height: 35px; width: 150px" runat="server"  BorderColor="#00CCFF" readonly="True" Visible="False"></asp:TextBox>
                                                </td>
                                            <td>���ң�</td>
                                             <td><div class="form-inline">
                                                <input id="txt_hb" class="form-control input-s-sm" style="height: 35px; width: 150px" runat="server" readonly="True" /><asp:RequiredFieldValidator ID="yz22" runat="server" ControlToValidate="txt_hb" ErrorMessage="����Ϊ��" ValidationGroup="request" ForeColor="Red"></asp:RequiredFieldValidator>
                                                 </div> </td>
                                        </tr>
                                        <tr>
                                            <td class="auto-style4">&nbsp;ʵ�ʵ��ۣ�</td>
                                            <td><div class="form-inline">
                                                <asp:TextBox ID="txt_ljdj" class="form-control input-s-sm" style="height: 35px; width: 150px" runat="server" OnTextChanged="txt_ljdj_TextChanged" AutoPostBack="True" BorderColor="#00CCFF"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="yz23" runat="server" ControlToValidate="txt_ljdj" ErrorMessage="����Ϊ��" ValidationGroup="request" ForeColor="Red"></asp:RequiredFieldValidator>
                                                </div> </td>
                                            <td >�ܼۣ�</td>
                                             <td><div class="form-inline">
                                                <input id="txt_ljzj" class="form-control input-s-sm" style="height: 35px; width: 150px" runat="server" readonly="True" /><asp:RequiredFieldValidator ID="yz24" runat="server" ControlToValidate="txt_ljzj" ErrorMessage="����Ϊ��" ValidationGroup="request" ForeColor="Red"></asp:RequiredFieldValidator>
                                                </div> </td>
                                            <td>&nbsp;</td>
                                            <td>
                                                &nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td>�տ�������</td>
                                           <td><div class="form-inline">
                                                <asp:DropDownList ID="txt_sktj" class="form-control input-s-sm" style="height: 35px; width: 150px" runat="server"></asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="yz25" runat="server" ControlToValidate="txt_sktj" ErrorMessage="����Ϊ��" ValidationGroup="request" ForeColor="Red"></asp:RequiredFieldValidator>
                                            </div> </td>
                                            <td>���䷽ʽ��</td>
                                             <td><div class="form-inline">

                                                <asp:DropDownList ID="txt_ysfs" class="form-control input-s-sm" style="height: 35px; width: 150px" runat="server" AutoPostBack="True" OnSelectedIndexChanged="txt_ysfs_SelectedIndexChanged"></asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="yz26" runat="server" ControlToValidate="txt_ysfs" ErrorMessage="����Ϊ��" ValidationGroup="request" ForeColor="Red"></asp:RequiredFieldValidator>
                                            </div> </td>
                                            <td>Ҫ��������</td>
                                            <td><div class="form-inline">
                                                <asp:TextBox ID="txt_yqfy_date" class="form-control input-s-sm" style="height: 35px; width: 150px" runat="server" AutoPostBack="True" OnTextChanged="txt_yqfy_date_TextChanged"></asp:TextBox>

                                                <asp:RequiredFieldValidator ID="yz27" runat="server" ControlToValidate="txt_yqfy_date" ErrorMessage="����Ϊ��" ValidationGroup="request" ForeColor="Red"></asp:RequiredFieldValidator>
                                           </div> </td>
                                        </tr>
                                        <tr>
                                            <td>�˷�֧����ʽ��</td>
                                            <td><div class="form-inline">
                                                <asp:DropDownList ID="txt_yfzffs" class="form-control input-s-sm" style="height: 35px; width: 150px" runat="server"></asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="yz28" runat="server" ControlToValidate="txt_yfzffs" ErrorMessage="����Ϊ��" ValidationGroup="request" ForeColor="Red"></asp:RequiredFieldValidator>
                                            </div> </td>
                                            <td>�˷ѽ�</td>
                                             <td><div class="form-inline">
                                                <input id="txt_yfje" class="form-control input-s-sm" style="height: 35px; width: 150px" runat="server" /><asp:RequiredFieldValidator ID="yz55" runat="server" ControlToValidate="txt_yfje" ErrorMessage="����Ϊ��" ValidationGroup="request" ForeColor="Red"></asp:RequiredFieldValidator>
                                              </div> </td>
                                            <td>�������</td>
                                             <td><div class="form-inline">

                                                <asp:DropDownList ID="txt_ystk" class="form-control input-s-sm" style="height: 35px; width: 150px" runat="server"></asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="yz29" runat="server" ControlToValidate="txt_ystk" ErrorMessage="����Ϊ��" ValidationGroup="request" ForeColor="Red"></asp:RequiredFieldValidator>
                                              </div> </td>
                                        </tr>
                                      
                                        <tr>
                                            <td>�ջ�����Ϣ(��ѡ��)��
                                            </td>
                                            <td colspan=" 5 "><div class="form-inline">
                                                <div class="form-inline">
                                                    <img name="selectshxx" style="border: 0px;" src="../images/fdj.gif" alt="select" onclick="Getshdz()" />
                                                    <input id="txt_shrxx" class="form-control input-s-sm" style="height: 35px; width: 95%" runat="server" />
                                                </div>
                                                <asp:RequiredFieldValidator ID="yz30" runat="server" ControlToValidate="txt_shrxx" ErrorMessage="����Ϊ��" ValidationGroup="request" ForeColor="Red"></asp:RequiredFieldValidator>
                                            </div> </td>

                                        </tr>
                                        <tr>
                                            <td>�ջ��˵�ַ��</td>
                                            <td colspan="5"><div class="form-inline">
                                                <input id="txt_shdz" class="form-control input-s-sm" style="height: 35px; width: 95%" runat="server" /><asp:RequiredFieldValidator ID="yz31" runat="server" ControlToValidate="txt_shdz" ErrorMessage="����Ϊ��" ValidationGroup="request" ForeColor="Red"></asp:RequiredFieldValidator>
                                                </div> </td>
                                        </tr>
                                        <tr>
                                            <td>3.�ͻ�����Ҫ��</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td>Ҫ��</td>
                                            <td colspan="5">
                                                <input id="txt_yq" class="form-control input-s-sm" style="height: 35px; width: 95%" runat="server" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Ҫ�󸽼���
                                            </td>
                                            <td colspan="5">
                                                <div class="form-inline">
                                                    &nbsp;<asp:GridView ID="gvFile_yqfj" runat="server" AutoGenerateColumns="False" DataKeyNames="id" OnRowDeleting="gvFile_yqfj_RowDeleting" CellPadding="4" ForeColor="#333333" GridLines="None">
                                                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                                        <Columns>
                                                            <asp:BoundField ApplyFormatInEditMode="True" DataField="id" HeaderText="No." ShowHeader="False" />
                                                            <asp:TemplateField HeaderText="�ļ�����" ShowHeader="False">
                                                                <ItemTemplate>
                                                                    <asp:HyperLink ID="HyperLink1" runat="server" Target="_blank" NavigateUrl='<% #DataBinder.Eval(Container.DataItem, "File_lj")%>' Text='<%#DataBinder.Eval(Container.DataItem,"File_name")%>'></asp:HyperLink>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField ShowHeader="False" HeaderText="">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="false" CommandName="Delete" Text="�h��"></asp:LinkButton>
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
                                                    <input id="txt_yqfj" type="file" class="form-control" style="width: 90px" multiple="multiple" runat="server" /><asp:Button ID="Btn_yqfj" runat="server" CssClass="form-control" Text="�ϴ��ļ�" OnClick="Btn_yqfj_Click" />
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="auto-style8" colspan="6"><strong>��ǩ�����Ϣ</strong></td>
                                        </tr>
                                             <tr>
                                            <td class="auto-style10" colspan="2"><strong>ͨ����λ</strong> </td>
                                            <td>��Ӧ�̴���</td>
                                            <td>
                                               <input id="txt_gysdm" class="form-control input-s-sm" style="height: 35px; width: 150px" runat="server" /></td>
                                            <td>������Ŀ<br>���ƽ׶Σ�</td>
                                            <td>
                                                <input id="txt_xmjd" class="form-control input-s-sm" style="height: 35px; width: 150px" runat="server" /></td>
                                        </tr>
                                          <tr>
                                            <td class="auto-style10" colspan="2">&nbsp;</td>
                                            <td>������ϵ��</td>
                                            <td>
                                                <input id="txt_ddlxr" class="form-control input-s-sm" style="height: 35px; width: 150px" runat="server" /></td>
                                            <td>��ϵ�˵绰</td>
                                            <td>
                                                <input id="txt_ddlxphone" class="form-control input-s-sm" style="height: 35px; width: 150px" runat="server" /></td>
                                        </tr>
                                          <tr>
                                            <td class="auto-style10" colspan="2">ѡ���ӡ�ͻ�����</td>
                                            <td colspan="4"> <div class="form-inline">
                                                <asp:DropDownList ID="DDL_DY" class="form-control input-s-sm" style="height: 35px; width: 150px" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DDL_DY_SelectedIndexChanged">
                                                    <asp:ListItem Value="101">FORD</asp:ListItem>
                                                    <asp:ListItem Value="102">Chrylser</asp:ListItem>
                                                    <asp:ListItem Value="117">GM</asp:ListItem>
                                                    <asp:ListItem Value="115">AAM</asp:ListItem>
                                                    <asp:ListItem Value="109">BBAC</asp:ListItem>
                                                    <asp:ListItem Value="103">Cooper KS</asp:ListItem>
                                                    <asp:ListItem Value="119">����Dainler</asp:ListItem>
                                                    <asp:ListItem>����</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="yz57" runat="server" ControlToValidate="DDL_DY" ErrorMessage="����Ϊ��" ValidationGroup="request" ForeColor="Red"></asp:RequiredFieldValidator>
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
                                            <td>�Ƿ���ҪSerial_No</td>
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
                                            <td>������ʶ���</td>
                                            <td>

                                                <asp:DropDownList ID="DDL_sbh_AAM" class="form-control input-s-sm" style="height: 35px; width: 150px" runat="server"></asp:DropDownList>
                                              </td>
                                            <td>&nbsp;</td>
                                            <td>
                                                &nbsp;</td>
                                        </tr>
                                          <tr id="AAM2" runat="server">                                          
                                                  <td class="auto-style9" colspan="2">&nbsp;</td>
                                            <td>ʶ���</td>
                                            <td>
                                                <input id="txt_sbh_HM_AAM" class="form-control input-s-sm" style="height: 35px; width: 150px" runat="server" /></td>
                                            <td>NO����</td>
                                            <td>
                                                <input id="txt_sbh_NO_AAM" class="form-control input-s-sm" style="height: 35px; width: 150px" runat="server" /></td>
                                        </tr>
                                              <tr id="BBAC" runat="server" >
                                          
                                              <td class="auto-style9" colspan="2"><strong>BBAC</strong></td>
                                            <td><span>�������</span><span lang="EN-US"> ZGS</span></td>
                                            <td>
                                                <input id="txt_ZGS_BBAC" class="form-control input-s-sm" style="height: 35px; width: 150px" runat="server" /></td>
                                            <td><span lang="EN-US">E/Q</span><span>����</span></td>
                                            <td>
                                                <input id="txt_EQ_BBAC" class="form-control input-s-sm" style="height: 35px; width: 150px" runat="server" /></td>
                                        </tr>
                                          <tr id="Daimler" runat="server" >
                                           
                                              <td class="auto-style9" colspan="2"><strong>Daimler</strong></td>
                                            <td>Part Status<br>���״̬</td>
                                            <td>
                                                <asp:DropDownList ID="DDL_Part_Status_Daimler" class="form-control input-s-sm" style="height: 35px; width: 150px" runat="server"></asp:DropDownList>
                                              </td>
                                            <td>��������</td>
                                            <td>
                                                <input id="txt_Part_Status_Daimler_ms" class="form-control input-s-sm" style="height: 35px; width: 150px" runat="server" /></td>
                                        </tr>
                                          <tr id="Daimler2" runat="server" v>
                                            <td colspan="2">&nbsp;</td>
                                            <td>Part Change<br>������</td>
                                            <td>
                                                <asp:DropDownList ID="DDL_Part_Change_Daimler" class="form-control input-s-sm" style="height: 35px; width: 150px" runat="server"></asp:DropDownList>
                                              </td>
                                            <td>&nbsp;</td>
                                            <td>
                                                &nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td>��ǩҪ��</td>
                                            <td colspan="5">
                                                <input id="txt_bqyq" class="form-control input-s-sm" style="height: 35px; width: 95%" runat="server" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Ҫ�󸽼���
                                            </td>
                                            <td colspan="5">
                                                <div class="form-inline">
                                                    &nbsp;<asp:GridView ID="gvFile_bqfj" runat="server" AutoGenerateColumns="False" DataKeyNames="id" OnRowDeleting="gvFile_bqfj_RowDeleting" CellPadding="4" ForeColor="#333333" GridLines="None">
                                                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                                        <Columns>
                                                            <asp:BoundField ApplyFormatInEditMode="True" DataField="id" HeaderText="No." ShowHeader="False" />
                                                            <asp:TemplateField HeaderText="�ļ�����" ShowHeader="False">
                                                                <ItemTemplate>
                                                                    <asp:HyperLink ID="HyperLink1" runat="server" Target="_blank" NavigateUrl='<% #DataBinder.Eval(Container.DataItem, "File_lj")%>' Text='<%#DataBinder.Eval(Container.DataItem,"File_name")%>'></asp:HyperLink>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField ShowHeader="False" HeaderText="">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="false" CommandName="Delete" Text="�h��"></asp:LinkButton>
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
                                                    <input id="txt_bqfj" type="file" class="form-control" style="width: 90px" multiple="multiple" runat="server" /><asp:Button ID="Btn_bqfj" runat="server" CssClass="form-control" Text="�ϴ��ļ�" OnClick="Btn_bqfj_Click" />
                                                </div>

                                            </td>
                                        </tr>

                                        <tr>
                                            <td>����ļ�Ҫ��</td>
                                            <td colspan="5">
                                                <input id="txt_shwjyq" class="form-control input-s-sm" style="height: 35px; width: 95%" runat="server" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>��������Ҫ��</td>
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
                                            <td class="auto-style4" colspan="7">ע��Ҫ�����ʱ����㹫ʽΪ��</td>
                                        </tr>
                                   
                                        <tr>
                                            <td class="auto-style4">&nbsp;</td>
                                            <td>

                                                ȷ�Ͻ׶�</td>
                                            <td >&nbsp;</td>
                                            <td >&nbsp;</td>
                                            <td >&nbsp;</td>
                                            <td>ʵʩ�׶�</td>
                                            <td><strong>(��������)</strong></td>
                                        </tr>
                                   <div id="hiddenDiv" style="display:none;">
                                        <tr>
                                            <td>&nbsp;</td>
                                            <td><span>ȷ�Ͻ׶θ���ع���ʦ</span></td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>��װ����Ҫ��ʱ��</td>
                                            <td>=Ҫ��������-6</td>
                                        </tr>
                                        <tr>
                                            <td>&nbsp;</td>
                                            <td>Ҫ��������-��������&gt;16��
                                            </td>
                                            <td>=Ҫ��������-16��</td>
                                            <td>
                                                &nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>����Ҫ��ʱ��</td>
                                            <td>=Ҫ��������-5</td>
                                        </tr>
                                        <tr>
                                            <td>&nbsp;</td>
                                            <td>Ҫ�������� -��������<=16 ��  </td>
                                            <td>=�������붩������</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>����Ҫ��ʱ��</td>
                                            <td>=Ҫ��������-2</td>
                                        </tr>
                                        <tr>
                                            <td>&nbsp;</td>
                                            <td>��������������ȷ��</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>�ͻ���������ȷ��Ҫ��ʱ��</td>
                                            <td>=Ҫ��������</td>
                                        </tr>
                                        <tr>
                                            <td>&nbsp;</td>
                                            <td>Ҫ�������� -��������>15 ��</td>
                                            <td>=Ҫ��������-15��</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>�ֿⷢ��Ҫ��ʱ��</td>
                                            <td>=Ҫ��������</td>
                                        </tr>
                                        <tr>
                                            <td>&nbsp;</td>
                                            <td>Ҫ�������� -�������� <=15 ��</td>
                                            <td>=�������붩������</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>���۵�������ȷ��</td>
                                            <td>=Ҫ�󵽻�����</td>
                                        </tr>
                                        <tr>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>����QAD�����Խ�</td>
                                            <td>֪ͨ��������-5��</td>
                                        </tr></div>
                                        <tr>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>���۶�������</td>
                                            <td>֪ͨ��������-5��</td>
                                        </tr>
                                        <tr>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>�������մ���</td>
                                            <td>����������+3��(ȥ��������)(ע:�Ƚ�С��3�죬��Ϊ֪ͨ�������ڵ���)</td>
                                        </tr>
                                        <tr>
                                            <td class="auto-style4" colspan="7">4.������</td>
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
                                            <td>������Ŀ��</td>
                                            <td>Ҫ���������</td>
                                            <td>ʵ���������</td>
                                        </tr>
                                            <td>��������</br></td>
                                            <td>
                                                <input id="txt_Assistant_job" class="form-control input-s-sm" style="height: 35px; width: 95px" runat="server" readonly="True" /></td>
                                            <td colspan="2">
                                                 <div class="form-inline">
                                                    <input id="txt_Assistant_id" class="input form-control input-s-sm" style="height: 35px; width: 80px" runat="server" readonly="True" />
                                                      / <input id="txt_Assistant" class="input form-control input-s-sm" style="height: 35px; width: 80px" runat="server" readonly="True" /> / <input id="txt_Assistant_AD" class="form-control input-s-sm" style="height: 35px; width:100px" runat="server" readonly="True" /></td>
                                            <td>
                                                &nbsp;</td>
                                            <td>
                                                ��������ȷ�ϣ�</td>
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
                                                QAD�����Խӣ�</td>
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
                                                ��������</td>
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
                                                ����ȷ��</td>
                                                  <td>
                                                <input id="txt_dh_date_require" class="form-control input-s-sm" style="height: 35px; width:100px" runat="server" Disabled="True" onclick="laydate()"  /></td>
                                            <td>
                                                <input id="txt_dh_date_complete" class="form-control input-s-sm" style="height: 35px; width:100px" runat="server" readonly="True" /></td>
                                        </tr>
                                        <tr>
                                            <td>��������ʦ<br />
                                                </td>
                                            <td>
                                                <input id="txt_Logistics_job" class="form-control input-s-sm" style="height: 35px; width: 95px" runat="server" readonly="True" /></td>
                                            <td colspan="2"><div class="form-inline">
                                                    <input id="txt_Logistics_id" class="input form-control input-s-sm" style="height: 35px; width: 80px" runat="server" readonly="True" />/<input id="txt_Logistics" class="input form-control input-s-sm" style="height: 35px; width: 80px" runat="server" readonly="True" /></td>
                                           </div> <td>
                                                &nbsp;</td>
                                            <td>
                                                ���մ���</td>
                                            <td>
                                                <input id="txt_dccl_require" class="form-control input-s-sm" style="height: 35px; width:100px" runat="server" Disabled="True" onclick="laydate()"  /></td>
                                            <td>
                                                <input id="txt_dccl_complete" class="form-control input-s-sm" style="height: 35px; width:100px" runat="server" Disabled="True" onclick="laydate()"  /></td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td colspan="2">ȷ�Ͻ׶�</td>
                                            <td colspan="2">ʵʩ�׶�</td>
                                        </tr>
                                        <tr>
                                            <td>&nbsp;</td>
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                            <td>Ҫ���������</td>
                                            <td>ȷ���������</td>
                                            <td>Ҫ���������</td>
                                            <td>ʵ���������</td>
                                        </tr>
                                        <tr>
                                            <td >���۹���ʦ��</td>
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
                                                <asp:RequiredFieldValidator ID="yz54" runat="server" ControlToValidate="txt_Sales_engineer_AD" ErrorMessage="����Ϊ��" ValidationGroup="request" ForeColor="Red"></asp:RequiredFieldValidator>
                                            </td>
                                            <td>
                                                &nbsp;</td>
                                            <td>
                                                &nbsp;</td>
                                        </tr>
                                        
                                        <tr>
                                            <td>��Ŀ��</br>׷��(�ͻ�����Ҫ��)</td>
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
                                                <asp:RequiredFieldValidator ID="yz32" runat="server" ControlToValidate="txt_project_engineer_AD" ErrorMessage="����Ϊ��" ValidationGroup="request" ForeColor="Red"></asp:RequiredFieldValidator>
                                            </td>
                                            <td>
                                                &nbsp;</td>
                                            <td>
                                                &nbsp;</td>
                                            <td>
                                                <asp:RequiredFieldValidator ID="yz39" runat="server" ControlToValidate="txt_special_require" ErrorMessage="����Ϊ��" ValidationGroup="request" ForeColor="Red"></asp:RequiredFieldValidator>
                                            </td>
                                            <td>&nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td>������</br>��װ����&amp;��װ����</td>
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
                                                <asp:RequiredFieldValidator ID="yz33" runat="server" ControlToValidate="txt_Packaging_engineer_AD" ErrorMessage="����Ϊ��" ValidationGroup="request" ForeColor="Red"></asp:RequiredFieldValidator>
                                            </td>
                                            <td>
                                                &nbsp;</td>
                                            <td>
                                                &nbsp;</td>
                                            <td>
                                                <asp:RequiredFieldValidator ID="yz40" runat="server" ControlToValidate="txt_Packaging_require" ErrorMessage="����Ϊ��" ValidationGroup="request" ForeColor="Red"></asp:RequiredFieldValidator>
                                            </td>
                                            <td>&nbsp;</td>
                                        </tr>
                                        <tr id="cpgcs" runat="server">
                                            <td>���̲�</br>�������²�Ʒ��</td>
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
                                                <asp:RequiredFieldValidator ID="yz34" runat="server" ControlToValidate="txt_product_engineer_AD" ErrorMessage="����Ϊ��" ValidationGroup="request" ForeColor="Red"></asp:RequiredFieldValidator>
                                            </td>
                                            <td>
                                                &nbsp;</td>
                                            <td>
                                                &nbsp;</td>
                                            <td>
                                                <asp:RequiredFieldValidator ID="yz41" runat="server" ControlToValidate="txt_goods_require" ErrorMessage="����Ϊ��" ValidationGroup="request" ForeColor="Red"></asp:RequiredFieldValidator>
                                            </td>
                                            <td>&nbsp;</td>
                                        </tr>
                                        <tr id="wljh" runat="server">
                                            <td>������</br>��������������</td>
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
                                                <asp:RequiredFieldValidator ID="yz35" runat="server" ControlToValidate="txt_planning_engineer_AD" ErrorMessage="����Ϊ��" ValidationGroup="request" ForeColor="Red"></asp:RequiredFieldValidator>
                                            </td>
                                            <td>
                                                &nbsp;</td>
                                            <td>
                                                &nbsp;</td>
                                            <td>
                                                <asp:RequiredFieldValidator ID="yz42" runat="server" ControlToValidate="txt_goods_require_wl" ErrorMessage="����Ϊ��" ValidationGroup="request" ForeColor="Red"></asp:RequiredFieldValidator>
                                            </td>
                                            <td>&nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td>������</br>(���鼰����ȷ��)</td>
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
                                                <asp:RequiredFieldValidator ID="yz36" runat="server" ControlToValidate="txt_quality_engineer_AD" ErrorMessage="����Ϊ��" ValidationGroup="request" ForeColor="Red" EnableTheming="True"></asp:RequiredFieldValidator>
                                            </td>
                                            <td>
                                                &nbsp;</td>
                                            <td>
                                                &nbsp;</td>
                                            <td>
                                                <asp:RequiredFieldValidator ID="yz43" runat="server" ControlToValidate="txt_check_require" ErrorMessage="����Ϊ��" ValidationGroup="request" ForeColor="Red"></asp:RequiredFieldValidator>
                                            </td>
                                            <td>&nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td>������</br>����</td>
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
                                                <asp:RequiredFieldValidator ID="yz44" runat="server" ControlToValidate="txt_check_require_jy" ErrorMessage="����Ϊ��" ValidationGroup="request" ForeColor="Red"></asp:RequiredFieldValidator>
                                            </td>
                                            <td>&nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td>������</br>�ֿ�(��װ����)</td>
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
                                            <td>�ͼ�</td>
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
                                                <asp:RequiredFieldValidator ID="yz45" runat="server" ControlToValidate="txt_shipping_require" ErrorMessage="����Ϊ��" ValidationGroup="request" ForeColor="Red"></asp:RequiredFieldValidator>
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
                                                <asp:Button ID="BTN_Sales_Assistant1" runat="server" class="btn btn-primary" style="height: 35px; width: 135px" Text="�ύ" OnClick="BTN_Sales_Assistant1_Click" ValidationGroup="request" />
                                            </td>
                                        </tr>

                                    </Table>

                                </fieldset>
                                <fieldset style="border-color: lightblue">
                                    <legend>��.֪ͨ��������&amp;QAD�����Խ�&amp;����</legend>
                                    <table>
                                        <tr>
                                            <td>1.֪ͨ��������</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td>֪ͨ��������</td>
                                            <td>
                                               
                                                <input id="txt_tzfy_date" class="form-control input-s-sm" style="height: 35px; width:200px" runat="server"  onclick="laydate()"  /></td>
                                            <td>
                                                <asp:RequiredFieldValidator ID="yz50" runat="server" ControlToValidate="txt_tzfy_date" ErrorMessage="֪ͨ�������ڲ���Ϊ��" ValidationGroup="Sales_Assistant2" ForeColor="Red"></asp:RequiredFieldValidator>
                                            </td>
                                        
                                            <td>
                                                <asp:Label ID="lab_bb" runat="server"></asp:Label>
                                            </td>
                                                <td class="auto-style7">
                                                    <strong>����Ʒ״̬������ͼ</strong></td>
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
                                                <asp:Button ID="BTN_Sales_Assistant2" runat="server" class="btn btn-primary" style="height: 35px; width: 135px" Text="�ύ" ValidationGroup="Sales_Assistant2" OnClick="BTN_Sales_Assistant2_Click" />
                                            </td>
                                        </tr>
                                           <tr>
                                            <td>2.����QAD����</td>
                                            <td>
                                            <asp:Button ID="BTN_Sales_Assistant2_CS" runat="server" class="btn btn-primary" Text="����QAD����" OnClick="BTN_Sales_Assistant2_CS_Click" Width="190px" />
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
                                            <td>3.������Ϣ</td>
                                            <td>
                                                &nbsp;</td>
                                            <td>
                                                &nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td>���շ�������</td>
                                            <td>
                                                <input id="txt_dcfy_date" class="form-control input-s-sm" style="height: 35px; width: 150px" runat="server"  readonly="True" /></td>
                                            <td>
                                                ���յ���</td>
                                            <td>
                                                <input id="txt_dch" class="form-control input-s-sm" style="height: 35px; width: 150px" runat="server"  readonly="True" /></td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td>���մ�������</td>
                                            <td>
                                                <input id="txt_dccl_date" class="form-control input-s-sm" style="height: 35px; width: 150px" runat="server"  readonly="True" /></td>
                                            <td>
                                                ������</td>
                                            <td>
                                                <input id="txt_dcr" class="form-control input-s-sm" style="height: 35px; width: 150px" runat="server"  readonly="True" /></td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                        </tr>
                                                                             
                                    </table>
                                </fieldset>
                                <fieldset style="border-color: lightblue">
                                    <legend>��.����</legend>
                                    <table style=" width: 100%">
                                        <tr>
                                            <td>1.ȷ�ϵ���</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td>��������</td>
                                            <td>
                                                <input id="txt_dh_date" class="form-control input-s-sm" style="height: 35px; width: 150px" runat="server" disabled="disabled"  />
                                            </td>
                                            <td>
                                                <asp:RequiredFieldValidator ID="yz53" runat="server" ControlToValidate="txt_tzfy_date" ErrorMessage="�������ڲ���Ϊ��" ValidationGroup="Sales_Assistant3" ForeColor="Red"></asp:RequiredFieldValidator>
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
                                                <asp:Button ID="BTN_Sales_Assistant3" runat="server" class="btn btn-primary" style="height: 35px; width: 135px" Text="�ύ" ValidationGroup="Sales_Assistant3" OnClick="BTN_Sales_Assistant3_Click" />
                                            </td>
                                        </tr>                                     
                                    </table>
                                </fieldset>
                                <div class="form-inline" style="margin-top:2px">
                                    <label>�ύ˵��</label>                                         
                                    <input id="txt_content_Sales_Assistant" class="form-control input-s-sm" style="width: 80%" runat="server" /><asp:RequiredFieldValidator ID="yz47" runat="server" ControlToValidate="txt_content_Sales_Assistant" ErrorMessage="�ύ˵������Ϊ��" ValidationGroup="request" ForeColor="Red"></asp:RequiredFieldValidator>
                                    <asp:RequiredFieldValidator ID="yz51" runat="server" ControlToValidate="txt_content_Sales_Assistant" ErrorMessage="�ύ˵������Ϊ��" ValidationGroup="Sales_Assistant2" ForeColor="Red"></asp:RequiredFieldValidator>
                                    <asp:RequiredFieldValidator ID="yz52" runat="server" ControlToValidate="txt_content_Sales_Assistant" ErrorMessage="�ύ˵������Ϊ��" ValidationGroup="Sales_Assistant3" ForeColor="Red"></asp:RequiredFieldValidator>
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
                        <strong>��Ŀ����ʦ--��1.����ȷ�� 2.�ͻ�����Ҫ��ȷ��  </strong><span class="caret"></span>
                    </div>
                    <div class="panel-body <% =ViewState["lv"].ToString() == "XMGCS" ? "" : "collapse" %>" id="XMGCS">
                        <div class="col-xs-12 col-sm-12  col-md-12 col-lg-12">
                            <fieldset style="border-color: lightblue">
                                <legend>һ.����ȷ��</legend>
                                <table style=" width: 100%">
                                    <tr>
                                        <td>ѡ���Ʒ״̬ :</td>
                                        <td> <div class="form-inline">
                                            <asp:DropDownList ID="txt_cp_status" class="form-control input-s-sm" Style=" width: 200px" runat="server"></asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="yz48" runat="server" ControlToValidate="txt_cp_status" ErrorMessage="��Ʒ״̬����Ϊ��" ValidationGroup="Project_Engineer1" ForeColor="Red"></asp:RequiredFieldValidator>
                                       </div> </td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td>�ͻ�����Ҫ��</td>
                                        <td colspan="5">
                                            <input id="txt_special_yq" class="form-control input-s-sm" style=" width: 95%" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Ҫ�󸽼���
                                        </td>
                                        <td colspan="5">
                                            <div class="form-inline">
                                                &nbsp;<asp:GridView ID="gvFile_special_fj" runat="server" AutoGenerateColumns="False" DataKeyNames="id" OnRowDeleting="gvFile_special_fj_RowDeleting" CellPadding="4" ForeColor="#333333" GridLines="None">
                                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                                    <Columns>
                                                        <asp:BoundField ApplyFormatInEditMode="True" DataField="id" HeaderText="No." ShowHeader="False" />
                                                        <asp:TemplateField HeaderText="�ļ�����" ShowHeader="False">
                                                            <ItemTemplate>
                                                                <asp:HyperLink ID="HyperLink1" runat="server" Target="_blank" NavigateUrl='<% #DataBinder.Eval(Container.DataItem, "File_lj")%>' Text='<%#DataBinder.Eval(Container.DataItem,"File_name")%>'></asp:HyperLink>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField ShowHeader="False" HeaderText="">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="false" CommandName="Delete" Text="�h��"></asp:LinkButton>
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
                                                <asp:Button ID="Btn_special_fj" runat="server" CssClass="form-control" Text="�ϴ��ļ�" OnClick="Btn_special_fj_Click" />
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>
                                            <asp:Button ID="BTN_Project_Engineer1" runat="server" class="btn btn-primary" Style=" width: 100px" Text="�ύ" ValidationGroup="Project_Engineer1" OnClick="BTN_Project_Engineer1_Click" />
                                        </td>
                                        <td>&nbsp;</td>
                                    </tr>
                                </table>
                            </fieldset>
                            <fieldset>
                                <legend>��.��װ����</legend>
                                <table>
                                    <tr>
                                        <td>�ͻ�����Ҫ��ȷ��</td>
                                        <td><div class="form-inline">
                                            <asp:DropDownList ID="txt_customer_request" class="form-control input-s-sm" style="height: 35px; width: 150px" runat="server"></asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="yz49" runat="server" ControlToValidate="txt_customer_request" ErrorMessage="�ͻ�����Ҫ��ȷ�ϲ���Ϊ��" ValidationGroup="Project_Engineer2" ForeColor="Red"></asp:RequiredFieldValidator>
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
                                            <asp:Button ID="BTN_Project_Engineer2" runat="server" class="btn btn-primary" style="height: 35px; width: 135px" Text="�ύ" ValidationGroup="Project_Engineer2" OnClick="BTN_Project_Engineer2_Click" />
                                        </td>
                                        <td>&nbsp;</td>
                                    </tr>                                    
                                </table>  
                            </fieldset>
                            
                            <div class="form-inline" style="margin-top:10px">
                                <label>�ύ˵��</label>                                         
                                <input id="txt_content_Project_Engineer" class="form-control input-s-sm" style="width: 80%" runat="server" /><asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txt_content_Project_Engineer" ErrorMessage="�ύ˵������Ϊ��" ValidationGroup="Project_Engineer1" ForeColor="Red"></asp:RequiredFieldValidator>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txt_content_Project_Engineer" ErrorMessage="�ύ˵������Ϊ��" ValidationGroup="Project_Engineer2" ForeColor="Red"></asp:RequiredFieldValidator>
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
                        <strong>��Ʒ����ʦ--��1.����ȷ�� 2.����</strong>
                        <span class="caret"></span>
                    </div>
                    <div class="panel-body <% =ViewState["lv"].ToString() == "CPGCS" ? "" : "collapse" %>" id="CPGCS">
                        <div class="col-xs-12 col-sm-12  col-md-12 col-lg-12">
                            <fieldset>
                                <legend>һ.����ȷ��</legend>
                                <table style="width: 100%">
                                    <tr>
                                        <td>1.���Ҫ�󣨳�Ʒ������ڴ˴η����ļ���Ҫ��</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td>�����</td>
                                        <td>�Ƿ���Ҫ���</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td>6��ȫ�ߴ����</td>
                                        <td><asp:DropDownList ID="txt_is_check_qcc" class="form-control input-s-sm" style="height: 35px; width: 150px" runat="server"></asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txt_is_check_qcc" ErrorMessage="����Ϊ��" ValidationGroup="Product_Engineer1" ForeColor="Red"></asp:RequiredFieldValidator>
                                        </td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td>������ȫ��</td>
                                        <td>
                                            <asp:DropDownList ID="txt_is_check_szb" class="form-control input-s-sm" style="height: 35px; width: 150px" runat="server"></asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ControlToValidate="txt_is_check_szb" ErrorMessage="����Ϊ��" ValidationGroup="Product_Engineer1" ForeColor="Red"></asp:RequiredFieldValidator>
                                        </td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td>���ȫ��</td>
                                        <td>
                                            <asp:DropDownList ID="txt_is_check_wg" class="form-control input-s-sm" style="height: 35px; width: 150px" runat="server"></asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ControlToValidate="txt_is_check_wg" ErrorMessage="����Ϊ��" ValidationGroup="Product_Engineer1" ForeColor="Red"></asp:RequiredFieldValidator>
                                        </td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td>���ȫ��</td>
                                        <td>
                                            <asp:DropDownList ID="txt_is_check_jj" class="form-control input-s-sm" style="height: 35px; width: 150px" runat="server"></asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" ControlToValidate="txt_is_check_jj" ErrorMessage="����Ϊ��" ValidationGroup="Product_Engineer1" ForeColor="Red"></asp:RequiredFieldValidator>
                                        </td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td>��������</td>
                                        <td>
                                            <input id="txt_check_other_ms" class="form-control input-s-sm" style="height: 35px; width: 150px" runat="server" /></td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td>2.����·��</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td>ѡ�����·��</td>
                                        <td>
                                            <asp:DropDownList ID="txt_check_lj" class="form-control input-s-sm" style="height: 35px; width: 150px" runat="server" AutoPostBack="True" OnSelectedIndexChanged="txt_check_lj_SelectedIndexChanged"></asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" ControlToValidate="txt_check_lj" ErrorMessage="����Ϊ��" ValidationGroup="Product_Engineer1" ForeColor="Red"></asp:RequiredFieldValidator>
                                        </td>
                                        <td>&nbsp;</td>
                                        <td>ȷ�ϱ���������ڣ�</td>
                                        <td><div class="form-inline">
                                            <asp:DropDownList ID="txt_Already_Product" class="form-control input-s-sm" style="height: 35px; width: 150px" runat="server"></asp:DropDownList>
                                           <asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server" ControlToValidate="txt_Already_Product" ErrorMessage="����Ϊ��" ValidationGroup="Product_Engineer1" ForeColor="Red"></asp:RequiredFieldValidator>
                                        </div></td>
                                    </tr>
                                    <tr>
                                        <td>����·��˵����</td>
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
                                            <asp:Button ID="BTN_Product_Engineer1" runat="server" class="btn btn-primary" style="height: 35px; width: 135px" Text="�ύ" ValidationGroup="Product_Engineer1" OnClick="BTN_Product_Engineer1_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                            <fieldset>
                                <legend>��.����</legend>
                                <table>
                                    <tr>
                                        <td>�ο���/���κ�</td>
                                        <td><div class="form-inline">
                                            <asp:DropDownList ID="txt_reference_number" class="form-control input-s-sm" style="height: 35px; width: 150px" runat="server" AutoPostBack="True" OnSelectedIndexChanged="txt_reference_number_SelectedIndexChanged"></asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator19" runat="server" ControlToValidate="txt_reference_number" ErrorMessage="����Ϊ��" ValidationGroup="Product_Engineer2" ForeColor="Red"></asp:RequiredFieldValidator>
                                            <asp:Label ID="Lb_txt_reference_number" runat="server" Text="Label"></asp:Label>
                                        </div></td>
                                        <td>��������</td>
                                        <td><div class="form-inline">
                                            <input id="txt_Stocking_quantity" class="form-control input-s-sm" style="height: 35px; width: 150px" runat="server" /><asp:RequiredFieldValidator ID="RequiredFieldValidator22" runat="server" ControlToValidate="txt_Stocking_quantity" ErrorMessage="����Ϊ��" ValidationGroup="Product_Engineer2" ForeColor="Red"></asp:RequiredFieldValidator>
                                       </div> </td>
                                        <td>�ͼ쵥��</td>
                                        <td>
                                          <div class="form-inline">
                                            <asp:TextBox id="txt_Check_number" class="form-control input-s-sm" style="height: 35px; width: 150px"  runat="server" AutoPostBack="True" OnTextChanged="txt_Check_number_TextChanged"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator21" runat="server" ControlToValidate="txt_Check_number" ErrorMessage="����Ϊ��" ValidationGroup="Product_Engineer2" ForeColor="Red"></asp:RequiredFieldValidator>
                                        </div></td>
                                    </tr>
                                    <tr>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td></td>
                                        <td>
                                            <asp:Button ID="BTN_Product_Engineer2" runat="server" class="btn btn-primary" style="height: 35px; width: 135px" Text="�ύ" ValidationGroup="Product_Engineer2" OnClick="BTN_Product_Engineer2_Click" />
                                        </td>
                                    </tr>                                   
                                </table>
                            </fieldset>
                            <div class="fieldset form-inline" style="margin-top:10px">
                                <label>�ύ˵��</label>                                  
                                    <input id="txt_content_Product_Engineer" class="form-control input-s-sm" style="width: 80%"  runat="server" /><asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txt_content_Product_Engineer" ErrorMessage="�ύ˵������Ϊ��" ValidationGroup="Product_Engineer1" ForeColor="Red"></asp:RequiredFieldValidator>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator23" runat="server" ControlToValidate="txt_content_Product_Engineer" ErrorMessage="�ύ˵������Ϊ��" ValidationGroup="Product_Engineer2" ForeColor="Red"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <%--������־--%>
        <div class="row  row-container">
            <div class="col-md-12">
                <div class="panel panel-info">
                    <div class="panel-heading" data-toggle="collapse" data-target="#BZGCS">
                        <strong>��װ����ʦ--��1.����ȷ�� 2.���ı���</strong>
                        <span class="caret"></span>
                    </div>
                    <div class="panel-body <% =ViewState["lv"].ToString() == "BZGCS" ? "" : "collapse" %>" id="BZGCS">

                        <div class="col-xs-12 col-sm-12  col-md-12 col-lg-12">
                            һ.���ı�������װ����
                        <fieldset>
                                <legend>1.��װ����</legend>
                            <table style="height: 35px; width: 100%">
                                <tr>
                                    <td>��װ��1</td>
                                    <td><div class="form-inline">
                                        <asp:DropDownList ID="txt_Box_specifications1" class="form-control input-s-sm" style="height: 35px; width: 135px" runat="server" AutoPostBack="True" OnSelectedIndexChanged="txt_Box_specifications1_SelectedIndexChanged"></asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator24" runat="server" ControlToValidate="txt_Box_specifications1" ErrorMessage="����ѡ��һֻ��װ��" ValidationGroup="Packaging_Engineer1" ForeColor="Red"></asp:RequiredFieldValidator>
</div>
                                    </td>
                                    <td>��������1</td>
                                    <td>
                                        <input id="txt_Other_box_dec1" class="form-control input-s-sm" style="height: 35px; width: 135px" runat="server" /></td>
                                    <td>����1</td>
                                    <td><div class="form-inline">
                                        <input id="txt_Box_weight1" class="form-control input-s-sm" style="height: 35px; width: 80px" runat="server" /><asp:RequiredFieldValidator ID="RequiredFieldValidator25" runat="server" ControlToValidate="txt_Box_weight1" ErrorMessage="����Ϊ��" ValidationGroup="Packaging_Engineer1" ForeColor="Red"></asp:RequiredFieldValidator>
                                        </div>
                                    </td>
                                    <td>
                                        ÿ������</td>
                                    <td><div class="form-inline">
                                        <input id="txt_Per_Crate_Qty" class="form-control input-s-sm" style="height: 35px; width: 80px" runat="server"  /><asp:RequiredFieldValidator ID="RequiredFieldValidator66" runat="server" ControlToValidate="txt_Per_Crate_Qty" ErrorMessage="����Ϊ��" ValidationGroup="Packaging_Engineer1" ForeColor="Red"></asp:RequiredFieldValidator>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td>��װ��2</td>
                                    <td>
                                        <asp:DropDownList ID="txt_Box_specifications2" class="form-control input-s-sm" style="height: 35px; width: 135px" runat="server" AutoPostBack="True" OnSelectedIndexChanged="txt_Box_specifications2_SelectedIndexChanged"></asp:DropDownList>
                                    </td>
                                    <td>��������2</td>
                                    <td>
                                        <input id="txt_Other_box_dec2" class="form-control input-s-sm" style="height: 35px; width:135px" runat="server" /></td>
                                    <td>����2</td>
                                    <td>
                                        <input id="txt_Box_weight2" class="form-control input-s-sm" style="height: 35px; width: 80px" runat="server" /></td>
                                    <td>
                                        ÿ������ &nbsp;</td>
                                    <td>
                                        <input id="txt_Per_Crate_Qty2" class="form-control input-s-sm" style="height: 35px; width:80px" runat="server"  /></td>
                                </tr>
                                <tr>
                                    <td>��װ������(�ϼ�)</td>
                                    <td><div class="form-inline">
                                        <input id="txt_Box_weight_total" class="form-control input-s-sm" style="height: 35px; width: 135px" runat="server" disabled="true" /><asp:RequiredFieldValidator ID="RequiredFieldValidator26" runat="server" ControlToValidate="txt_Box_weight_total" ErrorMessage="����Ϊ��" ValidationGroup="Packaging_Engineer1" ForeColor="Red"></asp:RequiredFieldValidator>
                                        </div>
                                    </td>
                                    <td>��������</td>
                                    <td><div class="form-inline">
                                        <input id="txt_Box_quantity" class="form-control input-s-sm" style="height: 35px; width: 135px" runat="server" readonly="true" /><asp:RequiredFieldValidator ID="RequiredFieldValidator27" runat="server" ControlToValidate="txt_Box_quantity" ErrorMessage="����Ϊ��" ValidationGroup="Packaging_Engineer1" ForeColor="Red"></asp:RequiredFieldValidator>
                                        </div>
                                    </td>
                                    <td>&nbsp;</td>
                                    <td colspan="3">
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td>��װ����<br>(Ҫ������X�������)</td>
                                    <td><div class="form-inline">
                                        <input id="txt_Packing_net_weight" class="form-control input-s-sm" style="height: 35px; width: 135px" runat="server" readonly="true" /><asp:RequiredFieldValidator ID="RequiredFieldValidator28" runat="server" ControlToValidate="txt_Packing_net_weight" ErrorMessage="����Ϊ��" ValidationGroup="Packaging_Engineer1" ForeColor="Red"></asp:RequiredFieldValidator>
                                    </div></td>
                                    <td>��װë��<br>(��װ����+��װ������)</td>
                                    <td><div class="form-inline">
                                        <input id="txt_Package_weight" class="form-control input-s-sm" style="height: 35px; width: 135px" runat="server" disabled="true" /><asp:RequiredFieldValidator ID="RequiredFieldValidator29" runat="server" ControlToValidate="txt_Package_weight" ErrorMessage="����Ϊ��" ValidationGroup="Packaging_Engineer1" ForeColor="Red"></asp:RequiredFieldValidator>
                                    </div></td>
                                    <td>&nbsp;</td>
                                    <td colspan="3">&nbsp;</td>
                                </tr>
                                <tr>
                                    <td>��װ����˵��</td>
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
                                        <asp:Button ID="BTN_Packaging_Engineer1" runat="server" class="btn btn-primary" style="height: 35px; width: 135px" Text="�ύ" ValidationGroup="Packaging_Engineer1" OnClick="BTN_Packaging_Engineer1_Click" />
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                            <fieldset>
                                <legend>2.���ı���</legend>
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
                                        <td>���ı������</td>
                                        <td> <asp:DropDownList ID="txt_Packing_goods_Already" class="form-control input-s-sm" style="height: 35px; width: 150px" runat="server"></asp:DropDownList>

                                        </td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator31" runat="server" ControlToValidate="txt_Packing_goods_Already" ErrorMessage="����Ϊ��" ValidationGroup="Packaging_Engineer2" ForeColor="Red"></asp:RequiredFieldValidator>

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
                                            <asp:Button ID="BTN_Packaging_Engineer2" runat="server" class="btn btn-primary" style="height: 35px; width: 135px" Text="�ύ" ValidationGroup="Packaging_Engineer2" OnClick="BTN_Packaging_Engineer2_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                            <div class="fieldset form-inline" style="margin-top:10px">
                                <label>�ύ˵��</label>                                  
                                   <input id="txt_content_Packaging_Engineer" class="form-control input-s-sm" style="width: 80%" runat="server" /><asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txt_content_Packaging_Engineer" ErrorMessage="�ύ˵������Ϊ��" ValidationGroup="Packaging_Engineer1" ForeColor="Red"></asp:RequiredFieldValidator>
                                   <asp:RequiredFieldValidator ID="RequiredFieldValidator30" runat="server" ControlToValidate="txt_content_Packaging_Engineer" ErrorMessage="�ύ˵������Ϊ��" ValidationGroup="Packaging_Engineer2" ForeColor="Red"></asp:RequiredFieldValidator>
                                    
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
                        <strong>�����ƻ�--��1.����ȷ��2.����</strong>
                        <span class="caret"></span>
                    </div>
                    <div class="panel-body <% =ViewState["lv"].ToString() == "WLJH" ? "" : "collapse" %>" id="WLJH">
                        <div class="col-xs-12 col-sm-12  col-md-12 col-lg-12">
                            <fieldset>
                                <legend>һ.����ȷ��</legend>
                                <table style="height: 35px; width: 100%">
                                    <tr>
                                        <td>ȷ�ϱ����������</td>
                                        <td>
                                            <asp:DropDownList ID="txt_Already_Planning" class="form-control input-s-sm" style="height: 35px; width: 150px" runat="server"></asp:DropDownList>

                                        </td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator32" runat="server" ControlToValidate="txt_Already_Planning" ErrorMessage="����Ϊ��" ValidationGroup="Planning_Engineer1" ForeColor="Red"></asp:RequiredFieldValidator>

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
                                            <asp:Button ID="BTN_Planning_Engineer1" runat="server" class="btn btn-primary" style="height: 35px; width: 135px" Text="�ύ" ValidationGroup="Planning_Engineer1" OnClick="BTN_Planning_Engineer1_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                            <fieldset>
                                <legend>��.����</legend>
                                <table>
                                    <tr>
                                        <td>�������</td>
                                        <td><div class="form-inline">
                                            <input id="txt_Current_inventory_quantity" class="form-control input-s-sm" style="height: 35px; width: 150px" runat="server" readonly="true" /><asp:RequiredFieldValidator ID="RequiredFieldValidator34" runat="server" ControlToValidate="txt_Current_inventory_quantity" ErrorMessage="����Ϊ��" ValidationGroup="Planning_Engineer2" ForeColor="Red"></asp:RequiredFieldValidator>
                                            </div>
                                        </td>
                                        <td>�������</td>
                                        <td><div class="form-inline">
                                            <input id="txt_Current_inventory_date" class="form-control input-s-sm" style="height: 35px; width: 150px" runat="server" readonly="true" /><asp:RequiredFieldValidator ID="RequiredFieldValidator35" runat="server" ControlToValidate="txt_Current_inventory_date" ErrorMessage="����Ϊ��" ValidationGroup="Planning_Engineer2" ForeColor="Red"></asp:RequiredFieldValidator>
</div>
                                        </td>
                                        <td>��������</td>
                                        <td><div class="form-inline">
                                            <input id="txt_Stocking_quantity_wl" class="form-control input-s-sm" style="height: 35px; width: 150px" runat="server"   readonly="true" /><asp:RequiredFieldValidator ID="RequiredFieldValidator36" runat="server" ControlToValidate="txt_Stocking_quantity_wl" ErrorMessage="����Ϊ��" ValidationGroup="Planning_Engineer2" ForeColor="Red"></asp:RequiredFieldValidator>
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
                                            <asp:Button ID="BTN_Planning_Engineer2" runat="server" class="btn btn-primary" style="height: 35px; width: 135px" Text="�ύ" ValidationGroup="Planning_Engineer2" OnClick="BTN_Planning_Engineer2_Click" />
                                        </td>
                                    </tr>                                    
                                </table>
                            </fieldset>
                            <div class="fieldset form-inline" style="margin-top:10px">
                                <label>�ύ˵��</label>                                  
                                    <input id="txt_content_Planning_Engineer" class="form-control input-s-sm" style="width: 80%" runat="server" /><asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txt_content_Planning_Engineer" ErrorMessage="�ύ˵������Ϊ��" ValidationGroup="Planning_Engineer1" ForeColor="Red"></asp:RequiredFieldValidator>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator33" runat="server" ControlToValidate="txt_content_Planning_Engineer" ErrorMessage="�ύ˵������Ϊ��" ValidationGroup="Planning_Engineer2" ForeColor="Red"></asp:RequiredFieldValidator>
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
                        <strong>�ֿ�೤--&gt;1.�ͼ�2.������ȡ��3.��װ����</strong>
                        <span class="caret"></span>
                    </div>
                    <div class="panel-body <% =ViewState["lv"].ToString() == "CKBZ" ? "" : "collapse" %>" id="CKBZ">
                        <div class="col-xs-12 col-sm-12  col-md-12 col-lg-12">
                            <fieldset>
                                <legend>һ.�ͼ�</legend>
                                <table style="height: 35px; width: 100%">
                                    <tr>
                                        <td>�ڿ��ѡ��</td>
                                        <td><div class="form-inline">
                                            <input id="txt_Sorting_list" class="form-control input-s-sm" style="height: 35px; width: 150px" runat="server" readonly="true" />
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator37" runat="server" ControlToValidate="txt_Sorting_list" ErrorMessage="����Ϊ��" ValidationGroup="Warehouse_Keeper1" ForeColor="Red"></asp:RequiredFieldValidator>
                                        </div></td>
                                        <td>
                                            <asp:Button ID="BTN_Warehouse_Keeper_DY" runat="server" class="btn btn-primary" style="height: 35px; width: 135px" Text="��ӡ" OnClick="BTN_Warehouse_Keeper_DY_Click" />
                                            <asp:Label ID="txt_isdy" runat="server"></asp:Label>
                                        </td>
                                        <td>&nbsp;</td>
                                        <td>���ͼ���</td>
                                        <td><div class="form-inline">
                                            <asp:DropDownList ID="txt_Already_Check" class="form-control input-s-sm" style="height: 35px; width: 150px" runat="server"></asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator38" runat="server" ControlToValidate="txt_Already_Check" ErrorMessage="����Ϊ��" ValidationGroup="Warehouse_Keeper1" ForeColor="Red"></asp:RequiredFieldValidator>
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
                                            <asp:Button ID="BTN_Warehouse_Keeper1" runat="server" class="btn btn-primary" style="height: 35px; width: 135px" Text="�ύ" ValidationGroup="Warehouse_Keeper1" OnClick="BTN_Warehouse_Keeper1_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                            <fieldset>
                                <legend>��.������ȡ��</legend>
                                <table>
                                    <tr>
                                        <td>ȡ������</td>
                                        <td>
                                            <input id="txt_Warehouse_quantity" class="form-control input-s-sm" style="height: 35px; width: 150px" runat="server" /><asp:RequiredFieldValidator ID="RequiredFieldValidator39" runat="server" ControlToValidate="txt_Warehouse_quantity" ErrorMessage="����Ϊ��" ValidationGroup="Warehouse_Keeper2" ForeColor="Red"></asp:RequiredFieldValidator>

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
                                            <asp:Button ID="BTN_Warehouse_Keeper2" runat="server" class="btn btn-primary" style="height: 35px; width: 135px" Text="�ύ" ValidationGroup="Warehouse_Keeper2" OnClick="BTN_Warehouse_Keeper2_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                            <fieldset>
                                <legend>��.��װ����</legend>
                                <table>
                                    <tr>
                                        <td>1.�ϴ���װ��Ƭ(���䣻��Ʒ�������ɣ����� ������Ƭ)��
                                        </td>
                                        <td colspan="5">
                                            <div class="form-inline">
                                                &nbsp;<asp:GridView ID="gvFile_Package_photo" runat="server" AutoGenerateColumns="False" DataKeyNames="id" OnRowDeleting="gvFile_Package_photo_RowDeleting" CellPadding="4" ForeColor="#333333" GridLines="None">
                                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                                    <Columns>
                                                        <asp:BoundField ApplyFormatInEditMode="True" DataField="id" HeaderText="No." ShowHeader="False" />
                                                        <asp:TemplateField HeaderText="�ļ�����" ShowHeader="False">
                                                            <ItemTemplate>
                                                                <asp:HyperLink ID="HyperLink1" runat="server"  NavigateUrl='<% #DataBinder.Eval(Container.DataItem, "File_lj")%>' Text='<%#DataBinder.Eval(Container.DataItem,"File_name")%>'></asp:HyperLink>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField ShowHeader="False" HeaderText="">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="false" CommandName="Delete" Text="�h��"></asp:LinkButton>
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
                                                <input id="txt_Package_photo" type="file" class="form-control" style="width: 90px" multiple="multiple" runat="server" /><asp:Button ID="Btn_Package_photo" runat="server" CssClass="form-control" Text="�ϴ��ļ�" OnClick="Btn_Package_photo_Click" />
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>2.�ϴ�������Ƭ(װ������Ƭ&amp;װ����ⵥǩ����Ƭ&amp;�ڿ��ѡ����Ƭ)��
                                        </td>
                                        <td colspan="5">
                                            <div class="form-inline">
                                                &nbsp;<asp:GridView ID="gvFile_Shipping_photos" runat="server" AutoGenerateColumns="False" DataKeyNames="id" OnRowDeleting="gvFile_Shipping_photos_RowDeleting" CellPadding="4" ForeColor="#333333" GridLines="None">
                                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                                    <Columns>
                                                        <asp:BoundField ApplyFormatInEditMode="True" DataField="id" HeaderText="No." ShowHeader="False" />
                                                        <asp:TemplateField HeaderText="�ļ�����" ShowHeader="False">
                                                            <ItemTemplate>
                                                                <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<% #DataBinder.Eval(Container.DataItem, "File_lj")%>' Text='<%#DataBinder.Eval(Container.DataItem,"File_name")%>'></asp:HyperLink>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField ShowHeader="False" HeaderText="">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="false" CommandName="Delete" Text="�h��"></asp:LinkButton>
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
                                                <input id="txt_Shipping_photos" type="file" class="form-control" style="width: 90px" multiple="multiple" runat="server" /><asp:Button ID="Btn_Shipping_photos" runat="server" CssClass="form-control" Text="�ϴ��ļ�" OnClick="Btn_Shipping_photos_Click" />
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
                                            <asp:Button ID="BTN_Warehouse_Keeper3" runat="server" class="btn btn-primary" style="height: 35px; width: 135px" Text="�ύ" ValidationGroup="Warehouse_Keeper3" OnClick="BTN_Warehouse_Keeper3_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                              <fieldset>
                                <legend>��.��ǩ��ӡ</legend> <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional"><ContentTemplate>
                                   <table style="width: 100%">
                                    <tr>
                                        <td>��ӡ��ǩ����ѡ��
                                        </td>
                                        <td>
                                            <div class="form-inline">
                                            <asp:DropDownList ID="txt_bqdy" class="form-control input-s-sm" style="height: 35px; width: 150px" runat="server" AutoPostBack="True"></asp:DropDownList>
                                            </div>
                                        </td>
                                        <td>
                                            <asp:Label ID="Lab_dyslms" runat="server" Text="��ӡ����" Visible="False"></asp:Label>
                                        </td>
                                        <td>
                                            <input id="txt_dysl" class="form-control input-s-sm" style="height: 35px; width: 80px" runat="server" visible="False" /></td>
                                        <td>
                                            <asp:Button ID="Btn_dybq" runat="server" class="btn btn-primary" style="height: 35px; width: 135px" Text="��ӡ" OnClick="Btn_dybq_Click" />
                                        </td>
                                    </tr>
                                    </table>
                                    </ContentTemplate> </asp:UpdatePanel>
                            </fieldset>
                            <div class="fieldset form-inline" style="margin-top:10px">
                                <label>�ύ˵��</label>                                  
                                   <input id="txt_content_Warehouse_Keeper" class="form-control input-s-sm" style="width: 80%" runat="server" /><asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txt_content_Warehouse_Keeper" ErrorMessage="�ύ˵������Ϊ��" ValidationGroup="Warehouse_Keeper1" ForeColor="Red"></asp:RequiredFieldValidator>
                                   <asp:RequiredFieldValidator ID="RequiredFieldValidator40" runat="server" ControlToValidate="txt_content_Warehouse_Keeper" ErrorMessage="�ύ˵������Ϊ��" ValidationGroup="Warehouse_Keeper2" ForeColor="Red"></asp:RequiredFieldValidator>
                                   <asp:RequiredFieldValidator ID="RequiredFieldValidator41" runat="server" ControlToValidate="txt_content_Warehouse_Keeper" ErrorMessage="�ύ˵������Ϊ��" ValidationGroup="Warehouse_Keeper3" ForeColor="Red"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <%--������־--%>
        <div class="row  row-container">
            <div class="col-md-12">
                <div class="panel panel-info">
                    <div class="panel-heading" data-toggle="collapse" data-target="#JYBZ">
                        <strong>����೤--��1.����</strong>
                        <span class="caret"></span>
                    </div>

                    <%--  <div class="panel-body collapse" id="ZLGCS">--%>
                    <div class="panel-body <% =ViewState["lv"].ToString() == "JYBZ" ? "" : "collapse" %>" id="JYBZ">

                        <div class="col-xs-12 col-sm-12  col-md-12 col-lg-12">
                            <table style="height: 35px; width: 100%">
                                <tr>
                                    <td>һ.����</td>
                                    <td></td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td>�ϸ�����</td>
                                    <td><div class="form-inline">
                                        <input id="txt_Qualified_quantity" class="form-control input-s-sm" style="height: 35px; width: 150px" runat="server" /><asp:RequiredFieldValidator ID="RequiredFieldValidator42" runat="server" ControlToValidate="txt_Qualified_quantity" ErrorMessage="����Ϊ��" ValidationGroup="Checker_Monitor" ForeColor="Red"></asp:RequiredFieldValidator>
                                        </div>
                                    </td>
                                    <td>���ϸ�����</td>
                                    <td><div class="form-inline">
                                        <input id="txt_Unqualified_quantity" class="form-control input-s-sm" style="height: 35px; width: 150px" runat="server" /><asp:RequiredFieldValidator ID="RequiredFieldValidator43" runat="server" ControlToValidate="txt_Unqualified_quantity" ErrorMessage="����Ϊ��" ValidationGroup="Checker_Monitor" ForeColor="Red"></asp:RequiredFieldValidator>
                                        </div>
                                    </td>
                                    <td>����</td>
                                    <td><div class="form-inline">
                                        <input id="txt_Unqualified_description" class="form-control input-s-sm" style="height: 35px; width: 150px" runat="server" /><asp:RequiredFieldValidator ID="RequiredFieldValidator44" runat="server" ControlToValidate="txt_Unqualified_description" ErrorMessage="����Ϊ��" ValidationGroup="Checker_Monitor" ForeColor="Red"></asp:RequiredFieldValidator>
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
                                        <asp:Button ID="BTN_Checker_Monitor" runat="server" class="btn btn-primary" style="height: 35px; width: 135px" Text="�ύ" ValidationGroup="Checker_Monitor" OnClick="BTN_Checker_Monitor_Click" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>�ύ˵��</td>
                                    <td colspan="5">
                                        <input id="txt_content_Checker_Monitor" class="form-control input-s-sm" style="width: 80%"  runat="server" /><asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txt_content_Checker_Monitor" ErrorMessage="�ύ˵������Ϊ��" ValidationGroup="Checker_Monitor" ForeColor="Red"></asp:RequiredFieldValidator>
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
                        <strong>��������ʦ--1.����ȷ��2.����ȷ�ϣ����齻�ڣ�3.�ͼ� 4.���鱨��</strong>
                        <span class="caret "></span>
                    </div>                   
                    <div class="panel-body <% =ViewState["lv"].ToString() == "ZLGCS" ? "" : "collapse" %>" id="ZLGCS">
                        <div class="col-xs-12 col-sm-12  col-md-12 col-lg-12">
                            <fieldset id="V_zlgcs1" runat="server" >
                                <legend>һ.����ȷ��</legend>
                                <table style="width: 100%">
                                    <tr>
                                        <td>1.���Ҫ�󣨳�Ʒ������ڴ˴η����ļ���Ҫ��</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td>�����</td>
                                        <td>�Ƿ���Ҫ���</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td>6��ȫ�ߴ����</td>
                                        <td>
                                            <asp:DropDownList ID="txt_is_check_qcc_zl" class="form-control input-s-sm" style="height: 35px; width: 150px" runat="server"></asp:DropDownList>

                                        </td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator45" runat="server" ControlToValidate="txt_is_check_qcc_zl" ErrorMessage="����Ϊ��" ValidationGroup="Quality_Engineer1" ForeColor="Red"></asp:RequiredFieldValidator>

                                        </td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td>������ȫ��</td>
                                        <td>
                                            <asp:DropDownList ID="txt_is_check_szb_zl" class="form-control input-s-sm" style="height: 35px; width: 150px" runat="server"></asp:DropDownList>

                                        </td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator50" runat="server" ControlToValidate="txt_is_check_szb_zl" ErrorMessage="����Ϊ��" ValidationGroup="Quality_Engineer1" ForeColor="Red"></asp:RequiredFieldValidator>

                                        </td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td>���ȫ��</td>
                                        <td>
                                            <asp:DropDownList ID="txt_is_check_wg_zl" class="form-control input-s-sm" style="height: 35px; width: 150px" runat="server"></asp:DropDownList>

                                        </td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator51" runat="server" ControlToValidate="txt_is_check_wg_zl" ErrorMessage="����Ϊ��" ValidationGroup="Quality_Engineer1" ForeColor="Red"></asp:RequiredFieldValidator>

                                        </td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td>���ȫ��</td>
                                        <td>
                                            <asp:DropDownList ID="txt_is_check_jj_zl" class="form-control input-s-sm" style="height: 35px; width: 150px" runat="server"></asp:DropDownList>

                                        </td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator52" runat="server" ControlToValidate="txt_is_check_jj_zl" ErrorMessage="����Ϊ��" ValidationGroup="Quality_Engineer1" ForeColor="Red"></asp:RequiredFieldValidator>

                                        </td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td>��������</td>
                                        <td>
                                            <input id="txt_check_other_ms_zl" class="form-control input-s-sm" style="height: 35px; width: 150px" runat="server" /></td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td>2.����·��</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td>ѡ�����·��</td>
                                        <td>
                                            <asp:DropDownList ID="txt_check_lj_zl" class="form-control input-s-sm" style="height: 35px; width: 150px" runat="server" AutoPostBack="True" OnSelectedIndexChanged="txt_check_lj_zl_SelectedIndexChanged"></asp:DropDownList>

                                        </td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator49" runat="server" ControlToValidate="txt_check_lj_zl" ErrorMessage="����Ϊ��" ValidationGroup="Quality_Engineer1" ForeColor="Red"></asp:RequiredFieldValidator>

                                        </td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td>����·��˵��</td>
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
                                            <asp:Button ID="BTN_Quality_Engineer1" runat="server" class="btn btn-primary" style="height: 35px; width: 135px" Text="�ύ" ValidationGroup="Quality_Engineer1" OnClick="BTN_Quality_Engineer1_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                            <fieldset>
                                <legend>��.ȷ�ϼ���������ڣ�</legend>
                                <table>
                                    <tr>
                                        <td></td>
                                        <td>
                                            <asp:DropDownList ID="txt_Already_zl" class="form-control input-s-sm" style="height: 35px; width: 150px" runat="server"></asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator53" runat="server" ControlToValidate="txt_Already_zl" ErrorMessage="����Ϊ��" ValidationGroup="Quality_Engineer2" ForeColor="Red"></asp:RequiredFieldValidator>
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
                                            <asp:Button ID="BTN_Quality_Engineer2" runat="server" class="btn btn-primary" style="height: 35px; width: 135px" Text="�ύ" ValidationGroup="Quality_Engineer2" OnClick="BTN_Quality_Engineer2_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                            <fieldset id="V_zlgcs3" runat="server">
                                <legend>��.����--�ͼ�</legend>
                                <table style=" width: 100%">
                                    <tr>
                                        <td>�ο���/���κ�</td>
                                        <td><div class="form-inline">
                                            <asp:DropDownList ID="txt_reference_number_zl" class="form-control input-s-sm" style="height: 35px; width: 150px" runat="server"></asp:DropDownList>
                                            <asp:Label ID="Lb_txt_reference_number_zl" runat="server" Text="Label"></asp:Label>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator54" runat="server" ControlToValidate="txt_reference_number_zl" ErrorMessage="����Ϊ��" ValidationGroup="Quality_Engineer3" ForeColor="Red"></asp:RequiredFieldValidator>
                                        </div></td>
                                        <td>�ͼ쵥��</td>
                                        <td>
                                          <div class="form-inline">
                                            <asp:TextBox id="txt_Check_number_zl" class="form-control input-s-sm" style="height: 35px; width: 150px"  runat="server" AutoPostBack="True" OnTextChanged="txt_Check_number_zl_TextChanged"></asp:TextBox>
&nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator55" runat="server" ControlToValidate="txt_Check_number_zl" ErrorMessage="����Ϊ��" ValidationGroup="Quality_Engineer3" ForeColor="Red"></asp:RequiredFieldValidator>
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
                                            <asp:Button ID="BTN_Quality_Engineer3" runat="server" class="btn btn-primary" style="height: 35px; width: 135px" Text="�ύ" ValidationGroup="Quality_Engineer3" OnClick="BTN_Quality_Engineer3_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                            <fieldset>
                                <legend>��.����--���鱨��</legend>
                                <table style=" width: 100%">
                                   <tr>
                                        <td>�ϴ���ⱨ�棺
                                        </td>
                                        <td colspan="5">
                                            <div class="form-inline">
                                                &nbsp;<asp:GridView ID="gvFile_check_fj" runat="server" AutoGenerateColumns="False" DataKeyNames="id" OnRowDeleting="gvFile_check_fj_RowDeleting" CellPadding="4" ForeColor="#333333" GridLines="None">
                                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                                    <Columns>
                                                        <asp:BoundField ApplyFormatInEditMode="True" DataField="id" HeaderText="No." ShowHeader="False" />
                                                        <asp:TemplateField HeaderText="�ļ�����" ShowHeader="False">
                                                            <ItemTemplate>
                                                                <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<% #DataBinder.Eval(Container.DataItem, "File_lj")%>' Text='<%#DataBinder.Eval(Container.DataItem,"File_name")%>'></asp:HyperLink>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField ShowHeader="False" HeaderText="">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="false" CommandName="Delete" Text="�h��"></asp:LinkButton>
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
                                                <input id="txt_check_fj" type="file" class="form-control" style="width: 90px" multiple="multiple" runat="server" /><asp:Button ID="Btn_check_fj" runat="server" CssClass="form-control" Text="�ϴ��ļ�" OnClick="Btn_check_fj_Click" />
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>ȷ�Ϻϸ�����</td>
                                        <td><div class="form-inline">
                                            <input id="txt_Confirm_quantity" class="form-control input-s-sm" style="height: 35px; width: 150px" runat="server" /><asp:RequiredFieldValidator ID="RequiredFieldValidator60" runat="server" ControlToValidate="txt_Confirm_quantity" ErrorMessage="����Ϊ��" ValidationGroup="Quality_Engineer4" ForeColor="Red"></asp:RequiredFieldValidator>
                                        </div></td>
                                        <td>
                                            ���ϸ�����</td>
                                        <td><div class="form-inline">
                                            <input id="txt_Unqualified_quantity_zl" class="form-control input-s-sm" style="height: 35px; width: 150px" runat="server" /><asp:RequiredFieldValidator ID="RequiredFieldValidator61" runat="server" ControlToValidate="txt_Unqualified_quantity_zl" ErrorMessage="����Ϊ��" ValidationGroup="Quality_Engineer4" ForeColor="Red"></asp:RequiredFieldValidator>
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
                                            <asp:Button ID="BTN_Quality_Engineer4" runat="server" class="btn btn-primary" style="height: 35px; width: 135px" Text="�ύ" ValidationGroup="Quality_Engineer4" OnClick="BTN_Quality_Engineer4_Click" />
                                        </td>
                                    </tr>
                                    <tr id="gy_zl" runat="server">
                                        <td>���̸�Ԥ��</td>
                                        <td>
                                            <div class="form-inline">
                                            <asp:DropDownList ID="txt_Process_intervention_zl" class="form-control input-s-sm" style="height: 35px; width: 150px" runat="server"></asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator62" runat="server" ControlToValidate="txt_Process_intervention_zl" ErrorMessage="����Ϊ��" ValidationGroup="gy_zl" ForeColor="Red"></asp:RequiredFieldValidator>
                                       </div> </td>
                                        <td>
                                            ��Ԥ˵��</td>
                                        <td colspan="3">
                                    <input id="txt_gysm_zl" class="form-control input-s-sm" style="height: 35px; width: 85%" runat="server" /><asp:RequiredFieldValidator ID="RequiredFieldValidator63" runat="server" ControlToValidate="txt_gysm_zl" ErrorMessage="����Ϊ��" ValidationGroup="gy_zl" ForeColor="Red"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td class="auto-style3">&nbsp;</td>
                                        <td>
                                            <asp:Button ID="BTN_Process_intervention_zl" runat="server" class="btn btn-primary" style="height: 35px; width: 135px" Text="ȷ�����̸�Ԥ������" ValidationGroup="gy_zl" OnClick="BTN_Process_intervention_zl_Click" />
                                        </td>
                                    </tr>                                    
                                </table>
                            </fieldset>
                            <div class="fieldset form-inline" style="margin-top:10px">
                                <label>�ύ˵��</label>                                  
                                    <input id="txt_content_Quality_Engineer" class="form-control input-s-sm" style="width: 80%" runat="server" /><asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txt_content_Quality_Engineer" ErrorMessage="�ύ˵������Ϊ��" ValidationGroup="Quality_Engineer1" ForeColor="Red"></asp:RequiredFieldValidator>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator57" runat="server" ControlToValidate="txt_content_Quality_Engineer" ErrorMessage="�ύ˵������Ϊ��" ValidationGroup="Quality_Engineer2" ForeColor="Red"></asp:RequiredFieldValidator>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator58" runat="server" ControlToValidate="txt_content_Quality_Engineer" ErrorMessage="�ύ˵������Ϊ��" ValidationGroup="Quality_Engineer3" ForeColor="Red"></asp:RequiredFieldValidator>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator59" runat="server" ControlToValidate="txt_content_Quality_Engineer" ErrorMessage="�ύ˵������Ϊ��" ValidationGroup="Quality_Engineer4" ForeColor="Red"></asp:RequiredFieldValidator>
                            </div>
                        </div>

                        &nbsp;</div>
                </div>
            </div>
        </div>
        <%--������־--%>
        <div class="row  row-container">
            <div class="col-md-12">
                <div class="panel panel-info">
                    <div class="panel-heading" data-toggle="collapse" data-target="#CZRZ">
                        <strong>������־</strong>
                        <span class="caret"></span>
                    </div>
                    <%--  <div class="panel-body collapse" id="ZLGCS">--%>
                    <div class="panel-body <% =ViewState["lv"].ToString() == "CZRZ" ? "" : "collapse" %>" id="CZRZ">

                        <div class="col-xs-12 col-sm-12  col-md-12 col-lg-12">
                            <table style="height: 35px; width: 100%">
                                <tr>
                                    <td>һ.������Ա��¼</td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:GridView ID="GridView1" Width="100%"
                                            AllowMultiColumnSorting="True" AllowPaging="True"
                                            AllowSorting="True" AutoGenerateColumns="False"
                                            runat="server" Font-Size="Small">
                                            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                            <PagerSettings FirstPageText="��ҳ" LastPageText="βҳ"
                                                Mode="NextPreviousFirstLast" NextPageText="��ҳ" PreviousPageText="��ҳ" />
                                            <PagerStyle ForeColor="Red" />
                                            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                            <EditRowStyle BackColor="#999999" />
                                            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
                                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />

                                            <Columns>
                                                <asp:BoundField DataField="status_ms" HeaderText="����״̬"
                                                    ReadOnly="True">
                                                    <HeaderStyle Wrap="True" />
                                                    <ItemStyle Width="90px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Update_Engineer_MS" HeaderText="�����ڵ�"
                                                    ReadOnly="True">
                                                    <HeaderStyle Wrap="True" />
                                                    <ItemStyle Width="150px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="gcs_name" HeaderText="�����ڵ�����"
                                                    ReadOnly="True">
                                                    <HeaderStyle Wrap="True" />
                                                    <ItemStyle Width="100px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="lastname" HeaderText="������Ա"
                                                    ReadOnly="True">
                                                    <HeaderStyle Wrap="True" />
                                                    <ItemStyle Width="80px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Receive_time" HeaderText="����ʱ��"
                                                    ReadOnly="True">
                                                    <HeaderStyle Wrap="True" />
                                                    <ItemStyle Width="150px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Commit_time" HeaderText="����ʱ��">
                                                    <HeaderStyle Wrap="False" />
                                                    <ItemStyle Width="150px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Operation_time" HeaderText="��ʱ">
                                                    <HeaderStyle Wrap="False" />
                                                    <ItemStyle Width="100px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Update_content" HeaderText="�ύ˵��">
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
                <strong>������ǰ�׶�:</strong>
            </div>
            <div class="panel-body  " id="DDXX">
                <div>
                    <div class="">
                        <table border="1" width="100%">
                            <tr>
                                <td colspan="2">
                                    <div class="form-inline" style="background-color: lightblue">
                                        <%--<input id="txt_status_id"  runat="server" readonly="true" />--%>
                        ��ǰ�׶�:
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:Label ID="txt_status_id" runat="server"></asp:Label>
                                    :<asp:Label ID="txt_status_name" runat="server" Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Size="Large" Font-Strikeout="False" Font-Underline="True" ForeColor="#6600CC"></asp:Label></td>
                            </tr>
                            <tr>
                                <td colspan="2" style="background-color: lightblue">������Ϣ:</td>
                            </tr>
                            <tr>
                                <td colspan="2">

                                    <asp:GridView ID="GridView2" Width="100%"
                                        AllowMultiColumnSorting="True" AllowPaging="True"
                                        AllowSorting="True" AutoGenerateColumns="False"
                                        runat="server" Font-Size="Small">
                                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                        <PagerSettings FirstPageText="��ҳ" LastPageText="βҳ"
                                            Mode="NextPreviousFirstLast" NextPageText="��ҳ" PreviousPageText="��ҳ" />
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
                                            <asp:BoundField DataField="status_ms" HeaderText="������Ϣ">
                                                <HeaderStyle Wrap="False" />
                                                <ItemStyle Width="20%" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Commit_time" HeaderText="�������">
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

                                    <asp:Label ID="LB_Process_intervention" runat="server" Text="���۸�Ԥ����"></asp:Label>
                                </td>

                            </tr>

                            <tr>
                                <td>

                                    <asp:DropDownList ID="txt_Process_intervention" class="form-control input-s-sm" style="height: 35px; width: 150px" runat="server"></asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator64" runat="server" ControlToValidate="txt_Process_intervention" ErrorMessage="����Ϊ��" ValidationGroup="gy" ForeColor="Red"></asp:RequiredFieldValidator>
                                </td>

                            </tr>

                            <tr>
                                <td>

                                    <input id="txt_gysm" class="form-control input-s-sm" style="height: 35px; width: 85%" runat="server" /><asp:RequiredFieldValidator ID="RequiredFieldValidator65" runat="server" ControlToValidate="txt_gysm" ErrorMessage="��Ԥԭ�򲻿�Ϊ��" ValidationGroup="gy" ForeColor="Red"></asp:RequiredFieldValidator>
                                </td>

                            </tr>

                            <tr>
                                <td>

                                    <asp:Button ID="BTN_Process_intervention" runat="server" class="btn btn-primary" style="height: 35px; width: 135px" Text="ȷ��" OnClick="BTN_Process_intervention_Click" ValidationGroup="gy" />
                                </td>

                            </tr>

                            <tr>
                                <td>

                                        <asp:GridView ID="GridView3" Width="100%"
                                            AllowMultiColumnSorting="True" AllowPaging="True"
                                            AllowSorting="True" AutoGenerateColumns="False"
                                            runat="server" Font-Size="Small" GridLines="None" OnRowDataBound="GridView3_RowDataBound">
                                            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                            <PagerSettings FirstPageText="��ҳ" LastPageText="βҳ"
                                                Mode="NextPreviousFirstLast" NextPageText="��ҳ" PreviousPageText="��ҳ" />
                                            <PagerStyle ForeColor="Red" />
                                            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                            <EditRowStyle BackColor="#999999" />
                                            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />

                                            <Columns>
                                              
                                                        <asp:TemplateField HeaderText="����״̬" >  
                   <HeaderStyle Width="10px" Wrap="False" HorizontalAlign="Left" />              
                    <ItemTemplate>
                         <asp:ImageButton ID="Image1" runat="server" ImageUrl='~/Images/downloads.png' Width="30"  Height="30" onmouseover="this.style.cursor='hand'" Visible="true" OnClick="Image1_Click" />
                         <asp:ImageButton ID="Image2" runat="server" ImageUrl='~/Images/exclamation-red.png' Width="20"  Height="20" onmouseover="this.style.cursor='hand'" Visible="true" OnClick="Image2_Click"  />
                    </ItemTemplate>
                       <ItemStyle Width="10px" Height="40px" VerticalAlign="Bottom" HorizontalAlign="Left" />

                </asp:TemplateField>
                                                   <asp:BoundField DataField="Update_Engineer_MS2" HeaderText="����״̬"
                                                    ReadOnly="True">
                                                    <HeaderStyle Wrap="True" HorizontalAlign="Right" />
                                                    <ItemStyle Width="160px" VerticalAlign="Bottom" HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Commit_time" HeaderText="ʱ��"
                                                    ReadOnly="True">
                                                    <HeaderStyle Wrap="True" HorizontalAlign="Right" />
                                                    <ItemStyle Width="160px" VerticalAlign="Bottom" HorizontalAlign="Right" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="lastname" HeaderText="��Ա"
                                                    ReadOnly="True">
                                                    <HeaderStyle Wrap="True" HorizontalAlign="Right" />
                                                    <ItemStyle Width="70px" VerticalAlign="Bottom" HorizontalAlign="Right" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="RequireDate" HeaderText="Ҫ��ʱ��"
                                                    ReadOnly="True">
                                                    <HeaderStyle Wrap="True" HorizontalAlign="Right" />
                                                    <ItemStyle Width="160px" VerticalAlign="Bottom" HorizontalAlign="Right" />
                                                </asp:BoundField>
                                                    <asp:BoundField DataField="Update_Status" HeaderText="״̬"
                                                    ReadOnly="True">
                                                    <HeaderStyle Wrap="True" HorizontalAlign="Right" />
                                                    <ItemStyle Width="160px" VerticalAlign="Bottom" HorizontalAlign="Right" />
                                                </asp:BoundField>
                                                 <asp:BoundField DataField="lv" HeaderText="����"
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
