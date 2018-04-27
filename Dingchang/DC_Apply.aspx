<%@ Page Title="【订舱申请单】" Language="C#" MasterPageFile="~/Site.master"
    AutoEventWireup="true" CodeFile="DC_Apply.aspx.cs" Inherits="DC_DC_Apply" MaintainScrollPositionOnPostback="true" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <link href="../Content/css/bootstrap.min.css" rel="stylesheet" />
    <script src="../Content/js/jquery.min.js"></script>
    <script src="../Content/js/bootstrap.min.js"></script>  
    <script src="../Content/js/plugins/layer/layer.min.js"></script>
    <script src="../Content/js/plugins/layer/laydate/laydate.js"></script>
    <style type="text/css">
        .auto-style1
        {
            height: 69px;
        }
        
        .row
        {
            margin-right: 2px;
            margin-left: 2px;
        }
        
        /*.row-container {
            padding-left: 2px;
            padding-right: 2px;
        }*/
        
        fieldset
        {
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
        
        legend
        {
            color: #302A2A;
            font: bold 16px/2 Verdana, Geneva, sans-serif;
            font-weight: bold;
            text-align: left; /*text-shadow: 2px 2px 2px rgb(88, 126, 156);*/
        }
        fieldset
        {
            padding: .35em .625em .75em;
            margin: 0 2px;
            border: 1px solid lightblue;
        }
        legend
        {
            border-style: none;
            border-color: inherit;
            border-width: 0;
            padding: 5px;
            width: 108px;
            margin-bottom: 2px;
        }
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
            margin-top: 10px;
        }
        .auto-style2
        {
            height: 20px;
        }
    </style>
    <script>
       


    </script>

</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#mestitle").text("【订舱申请单】");

            $("input[id*='txt_fayun_date']").click(function (e) {
                laydate({
                    choose: function (dates) { //选择好日期的回调                       
                        $("input[id*='txt_fayun_date']").change();
                    }
                })
            })


        })//end ready

        function sendmail(requestid, status) {
            $.ajax({
                type: "post", //要用post方式                 
                url: "DC_Apply.aspx/SendEmail", //方法所在页面和方法名
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                data: "{requestid:'" + requestid + "',status:'" + status + "'}",
                success: function (data) {
                    if (data.d == "") //返回的数据用data.d获取内容
                    {
                        alert(data.d);
                    }
//                    else {
//                        alert("失败."); 
//                    }
                },
                error: function (err) {
                    alert(err);
                }
            });

        }
    </script>

        <script type="text/javascript">
        var popupwindow = null;
        //收货地址
        function Getshdz() { 
            popupwindow = window.open('../Select/select_shdz.aspx', '_blank', 'height=500,width=1000,resizable=no,menubar=no,scrollbars =no,location=no');
        }
        function shdz_setvalue(shrxx, shdz,requestid) {       
            //ctl01.<%=txt_shdz.ClientID%>.value = shdz;     
            ctl01.<%=txt_CP_ID.ClientID%>.value = requestid;  
            document.getElementById('<%=txt_CP_ID.ClientID%>').onchange(); 
            popupwindow.close();
        }

    </script>
    <div class="col-md-10" >
        <%-- <div class="panel-body <% =Session["lv"].ToString() == "XMGCS" ? "" : "collapse" %>" id="XMGCS">--%>
        <div class="row row-container">
            <div class="col-md-12">
                <div class="panel panel-info">
                    <div class="panel-heading" data-toggle="collapse" data-target="#XMGCS">
                        <strong>申请人信息</strong>
                    </div>
                    <%--<div class="panel-body " id="SQXX">--%>
                         <div class="panel-body <% =Session["lv"].ToString() == "XMGCS" ? "" : "collapse" %>" id="XMGCS">
                        <div class="col-xs-12 col-sm-12  col-md-12 col-lg-12">
                            <div class="">
                                <asp:UpdatePanel ID="UpdatePanel_request" runat="server">
                                    <ContentTemplate>
                                        <table style="height: 35px; width: 100%">
                                            <tr>
                                                <td>
                                                    申请人：
                                                </td>
                                                <td>
                                                    <div class="form-inline">
                                                        <input id="txt_Userid" class="input form-control input-s-sm"
                                                            style="height: 35px; width: 100px" runat="server" readonly="True" />
                                                        /
                                                        <input id="txt_UserName" class="form-control input-s-sm" style="height: 35px;
                                                            width: 100px" runat="server" readonly="True" />
                                                        /
                                                        <input id="txt_UserName_AD" class="form-control input-s-sm" style="height: 35px;
                                                            width: 100px" runat="server" readonly="True" />
                                                    </div>
                                                </td>
                                                <td>
                                                    部门：
                                                </td>
                                                <td>
                                                    <input id="txt_dept" class="form-control input-s-sm" style="height: 35px;
                                                        width: 200px" runat="server" readonly="True" />
                                                   
                                                </td>
                                                <td>
                                                    部门经理：
                                                </td>
                                                <td>
                                                    <div class="form-inline">
                                                        <input id="txt_managerid" class="form-control input-s-sm" style="height: 35px;
                                                            width: 100px" runat="server" readonly="True" />
                                                        /<input id="txt_manager" class="form-control input-s-sm" style="height: 35px;
                                                            width: 100px" runat="server" readonly="True" />
                                                        /<input id="txt_manager_AD" class="form-control input-s-sm" style="height: 35px;
                                                            width: 100px" runat="server" readonly="True" />
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="word-break: ">
                                                    订舱编号：
                                                </td>
                                                <td>
                                                    <input id="txt_Code" class="form-control input-s-sm" style="height: 35px;
                                                        width: 200px" runat="server" readonly="True" />
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <input id="txt_update_user" class="input form-control input-s-sm"
                                                        style="height: 35px; width: 100px" runat="server" readonly="True"
                                                        visible="false" />
                                                </td>
                                                <td>
                                                    申请日期：
                                                </td>
                                                <td>
                                                    <input id="txt_CreateDate" class="form-control input-s-sm" style="height: 35px;
                                                        width: 200px" runat="server" readonly="True" />
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
                    <div class="panel-heading" data-toggle="collapse" data-target="#XSZL">
                        <strong>销售申请订舱</strong>
                    </div>
                    <div class="panel-body " id="XSZL">
                        <div class="col-xs-12 col-sm-12  col-md-12 col-lg-12">
                            <div>
                                <table style="height: 35px; width: 100%">
                                    <tr>
                                        <td>
                                            物流订舱人员：
                                        </td>
                                        <td>
                                            <div class="form-inline" style="color: #FF0000">
                                              
                                                <asp:DropDownList ID="DropDC_Uid"  class="form-control input-s-sm" Style="height: 35px; width: 200px" runat="server">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="yz_uid" runat="server" 
                                                    ControlToValidate="DropDC_Uid" ErrorMessage="不能为空" 
                                                    ForeColor="Red" ValidationGroup="request"></asp:RequiredFieldValidator>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            发运日期：
                                        </td>
                                        <td>
                                        <div class="form-inline" style="color: #FF0000">
                                            
                                            <asp:TextBox ID="txt_fayun_date" runat="server" class="form-control input-s-sm"
                                                style="height: 35px; width: 200px" 
                                                AutoPostBack="True" 
                                                ontextchanged="txt_fayun_date_TextChanged"></asp:TextBox>
                                                         <asp:RequiredFieldValidator ID="yz_fyrq" runat="server" 
                                                ControlToValidate="txt_fayun_date" ErrorMessage="不能为空" 
                                                ForeColor="Red" ValidationGroup="request"></asp:RequiredFieldValidator>
                                                </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            收货人地址：
                                        </td>
                                        <td>
                                         <div class="form-inline" style="color: #FF0000">
                                            <input id="txt_shdz" runat="server" class="form-control input-s-sm"
                                                style="height: 35px; width: 90%" readonly="readonly" /><asp:TextBox 
                                                        ID="txt_CP_ID" runat="server" AutoPostBack="True" 
                                                        OnTextChanged="txt_CP_ID_TextChanged" Width="0px"></asp:TextBox>
                                            <img name="selectshxx" style="border: 0px;" src="../images/fdj.gif"
                                                alt="select" onclick="Getshdz()" />
                                            <asp:RequiredFieldValidator ID="yz_shdz" runat="server" ControlToValidate="txt_shdz"
                                                ErrorMessage="不能为空" ForeColor="Red" ValidationGroup="request"></asp:RequiredFieldValidator>
                                                </div>
                                        </td>
                                    </tr>
                                  
                                   
                                    <tr>
                                        <td colspan="2">
                                            <asp:Panel ID="Panel1" runat="server" GroupingText="订单明细">
                                                <asp:GridView ID="gv_ddmx" runat="server" 
                                                    AutoGenerateColumns="False" Width="100%" 
                                                    DataKeyNames="requestId" 
                                                    onrowdeleting="gv_ddmx_RowDeleting" 
                                                    onrowdatabound="gv_ddmx_RowDataBound">
                                                    <Columns>
                                                        <asp:BoundField DataField="requestid" HeaderText="请求ID">
                                                        <ControlStyle CssClass="hidden" Width="0px" />
                                                        <FooterStyle CssClass="hidden" Width="0px" />
                                                        <HeaderStyle CssClass="hidden" Width="0px" />
                                                        <ItemStyle CssClass="hidden" Width="0px" />
                                                        </asp:BoundField>
                                                        <asp:TemplateField HeaderText="样件订单号" SortExpression="pgi_no" >                   
                    <ItemTemplate>
                        <asp:HyperLink ID="HyperLink1" runat="server" Text='<%#Eval("code") %>' Width="130"></asp:HyperLink>
                    </ItemTemplate>
                                                                                        
                                                            <HeaderStyle BackColor="#C1E2EB" />
                                                                                        
                       <ItemStyle Width="50px" Wrap="False"  ForeColor="Blue" CssClass="size1"/>
                </asp:TemplateField> 
                                                        <asp:BoundField DataField="qadso" HeaderText="QAD单号">
                                                        <HeaderStyle BackColor="#C1E2EB" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="xmh" HeaderText="项目号" >
                                                        <HeaderStyle BackColor="#C1E2EB" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="gkddh" 
                                                            HeaderText="顾客&lt;br&gt;订单号" HtmlEncode="False" >
                                                        <HeaderStyle BackColor="#C1E2EB" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="gkmc" 
                                                            HeaderText="顾客&lt;br&gt;名称" HtmlEncode="False" >
                                                        <HeaderStyle BackColor="#C1E2EB" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="ljh" HeaderText="零件号" >
                                                        <HeaderStyle BackColor="#C1E2EB" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="yhsl" 
                                                            HeaderText="要货&lt;br&gt;数量" HtmlEncode="False" >
                                                        <HeaderStyle BackColor="#C1E2EB" Width="50px" />
                                                        <ItemStyle Wrap="True" />
                                                        </asp:BoundField>
                                                        <asp:TemplateField HeaderText="箱子&lt;br&gt;数量">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="box_quantity" runat="server" Text='<%#Eval("box_quantity")%>' Width="80px"  Height="25px"></asp:TextBox>
                                                            </ItemTemplate>
                                                            <HeaderStyle BackColor="#C1E2EB" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="装箱&lt;br&gt;方案">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="zxfa" runat="server" Text='<%#Eval("zxfa")%>'  Height="25px"></asp:TextBox>
                                                            </ItemTemplate>
                                                            <HeaderStyle BackColor="#C1E2EB" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="毛重">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="mz" runat="server" Text='<%#Eval("mz")%>' Width="80px"  Height="25px"></asp:TextBox>
                                                            </ItemTemplate>
                                                            <HeaderStyle BackColor="#C1E2EB" />
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="ystk" 
                                                            HeaderText="运输条款" >
                                                        <HeaderStyle BackColor="#C1E2EB" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="yqdh_date" HeaderText="要求到货&lt;br&gt;日期" 
                                                            DataFormatString="{0:yyyy-MM-dd}" HtmlEncode="False" >
                                                        <HeaderStyle BackColor="#C1E2EB" />
                                                        <ItemStyle Width="80px" />
                                                        </asp:BoundField>
                                                        <asp:TemplateField HeaderText="操作">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="false"
                                                                    CommandName="Delete" Text="刪除"></asp:LinkButton>
                                                            </ItemTemplate>
                                                            <HeaderStyle BackColor="#C1E2EB" />
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="domain" HeaderText="公司">
                                                        <ControlStyle CssClass="hidden" Width="0px" />
                                                        <FooterStyle CssClass="hidden" Width="0px" />
                                                        <HeaderStyle CssClass="hidden" Width="0px" />
                                                        <ItemStyle CssClass="hidden" Width="0px" />
                                                        </asp:BoundField>
                                                    </Columns>
                                                    <EmptyDataTemplate>
                                                        查无订单明细或已申请，请选择条件重新查询.
                                                    </EmptyDataTemplate>
                                                </asp:GridView>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                      <tr>
                                        <td>
                                            订单信息附件：
                                        </td>
                                        <td>
                                            <div class="form-inline">
                                                &nbsp;<asp:GridView ID="gvFile_ddfj" runat="server" AutoGenerateColumns="False"
                                                    DataKeyNames="id" OnRowDeleting="gvFile_ddfj_RowDeleting"
                                                    CellPadding="4" ForeColor="#333333" GridLines="None">
                                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                                    <Columns>
                                                        <asp:BoundField ApplyFormatInEditMode="True" DataField="id" HeaderText="No."
                                                            ShowHeader="False" />
                                                        <asp:TemplateField HeaderText="文件名称" ShowHeader="False">
                                                            <ItemTemplate>
                                                                <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<% #DataBinder.Eval(Container.DataItem, "File_lj")%>'
                                                                    Text='<%#DataBinder.Eval(Container.DataItem,"File_name")%>'></asp:HyperLink>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField ShowHeader="False" HeaderText="">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="false"
                                                                    CommandName="Delete" Text="刪除"></asp:LinkButton>
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
                                                <input id="txt_ddfj" type="file" class="form-control" style="width: 90px"
                                                    multiple="multiple" runat="server" /><asp:Button ID="Btn_ddfj"
                                                        runat="server" CssClass="form-control" Text="上传文件" 
                                                    onclick="Btn_ddfj_Click1" />
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>提交说明</td>
                                        <td>
                                         <div class="form-inline" style="color: #FF0000">
                                            <input id="txtdesc_sale" runat="server" 
                                                class="form-control input-s-sm" style="width: 90%" />
                                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator1" 
                                                 runat="server" ControlToValidate="txtdesc_sale"
                                                ErrorMessage="不能为空" ForeColor="Red" 
                                                 ValidationGroup="request"></asp:RequiredFieldValidator>
                                                </div>
                                                </td>
                                                
                                    </tr>
                                    <tr>
                                        <td colspan="2" align="right">
                                            <asp:Button ID="btn_sale" runat="server" class="btn btn-primary" ValidationGroup="request"
                                                Style="height: 35px; width: 100px" Text="提交" 
                                                onclick="btn_sale_Click" />
                                        </td>
                                    </tr>
                                </table>
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
                    <div class="panel-heading" data-toggle="collapse" data-target="#wuliu">
                        <strong>物流订舱 </strong><span class="caret"></span>
                    </div>
                    <div class="panel-body <% =Session["lv"].ToString() == "wuliu" ? "" : "collapse" %>" id="wuliu">
                  
                   <%-- <div class="panel-body " id="wuliu">--%>
                        <div class="col-xs-12 col-sm-12  col-md-12 col-lg-12">
                            <table style="width: 100%">
                                <tr>
                                    <td>
                                        货运单号：
                                    </td>
                                    <td >
                                    <div class="form-inline" style="color: #FF0000">
                                        <input id="txt_huoyun_no" class="form-control input-s-sm" style="width: 200px"
                                            runat="server" />
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" 
                                                 runat="server" ControlToValidate="txt_huoyun_no"
                                                ErrorMessage="不能为空" ForeColor="Red" 
                                                 ValidationGroup="request_wuliu"></asp:RequiredFieldValidator>
                                                 </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        装箱出库单附件：
                                    </td>
                                    <td >
                                        <div class="form-inline">
                                            <asp:GridView ID="gvFile_zkck_fj" runat="server" AutoGenerateColumns="False"
                                                DataKeyNames="id" CellPadding="4" ForeColor="#333333" 
                                                GridLines="None" onrowdeleting="gvFile_zkck_fj_RowDeleting">
                                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                                <Columns>
                                                    <asp:BoundField ApplyFormatInEditMode="True" DataField="id" HeaderText="No."
                                                        ShowHeader="False" />
                                                    <asp:TemplateField HeaderText="文件名称" ShowHeader="False">
                                                        <ItemTemplate>
                                                            <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<% #DataBinder.Eval(Container.DataItem, "File_lj")%>'
                                                                Text='<%#DataBinder.Eval(Container.DataItem,"File_name")%>'></asp:HyperLink>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField ShowHeader="False" HeaderText="">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="false"
                                                                CommandName="Delete" Text="刪除"></asp:LinkButton>
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
                                            <input id="txt_zkck_fj" type="file" class="form-control" style="width: 90px"
                                                multiple="multiple" runat="server" />
                                            <asp:Button ID="Btn_zkck_fj" runat="server" CssClass="form-control"
                                                Text="上传文件" OnClick="Btn_zkck_fj_Click" />
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        形式发票附件：
                                    </td>
                                    <td>
                                        <div class="form-inline">
                                            &nbsp;<asp:GridView 
                                                ID="gvFile_xsfp_fj" runat="server" AutoGenerateColumns="False"
                                                DataKeyNames="id" CellPadding="4" ForeColor="#333333" 
                                                GridLines="None" onrowdeleting="gvFile_xsfp_fj_RowDeleting">
                                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                                <Columns>
                                                    <asp:BoundField ApplyFormatInEditMode="True" DataField="id" HeaderText="No."
                                                        ShowHeader="False" />
                                                    <asp:TemplateField HeaderText="文件名称" ShowHeader="False">
                                                        <ItemTemplate>
                                                            <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<% #DataBinder.Eval(Container.DataItem, "File_lj")%>'
                                                                Text='<%#DataBinder.Eval(Container.DataItem,"File_name")%>'></asp:HyperLink>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField ShowHeader="False" HeaderText="">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="false"
                                                                CommandName="Delete" Text="刪除"></asp:LinkButton>
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
                                            <input id="txt_xsfp_fj" type="file" class="form-control" style="width: 90px"
                                                multiple="multiple" runat="server" />
                                            <asp:Button ID="Btn_xsfp_fj" runat="server" CssClass="form-control"
                                                Text="上传文件" OnClick="Btn_xsfp_fj_Click" />
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        预计外部提货日期</td>
                                    <td>
                                      <div class="form-inline" style="color: #FF0000">
                                        <input id="txt_yjthdate" runat="server" onclick="laydate()"
                                            class="form-control input-s-sm"  style="height: 35px;
                                                        width: 200px" />
                                                         <asp:RequiredFieldValidator ID="RequiredFieldValidator3" 
                                                 runat="server" ControlToValidate="txt_yjthdate"
                                                ErrorMessage="不能为空" ForeColor="Red" 
                                                 ValidationGroup="request_wuliu"></asp:RequiredFieldValidator>
                                                 </div>
                                                        </td>
                                    
                                </tr>
                                <tr>
                                    <td>
                                        加急预计到货日期</td>
                                    <td>
                                      <div class="form-inline" style="color: #FF0000">
                                        <input id="txt_dhdate" runat="server" onclick="laydate()"
                                            class="form-control input-s-sm"  style="height: 35px;
                                                        width: 200px" />
                                                 </div>
                                                 </td>
                                    
                                </tr>
                                <tr>
                                    <td>
                                        货运代理信息</td>
                                    <td>
                                      <div class="form-inline" style="color: #FF0000">
                                        <input id="txt_hydl" runat="server" 
                                            class="form-control input-s-sm"  style="height: 35px;
                                                        width: 90%" />
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" 
                                                 runat="server" ControlToValidate="txt_hydl"
                                                ErrorMessage="不能为空" ForeColor="Red" 
                                                 ValidationGroup="request_wuliu"></asp:RequiredFieldValidator>
                                                 </div>
                                                        </td>
                                    
                                </tr>
                                 <tr>
                                        <td>提交说明</td>
                                        <td>
                                          <div class="form-inline" style="color: #FF0000">
                                            <input id="tjdesc_wuliu" runat="server" 
                                                class="form-control input-s-sm" style="width: 90%" />
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" 
                                                 runat="server" ControlToValidate="tjdesc_wuliu"
                                                ErrorMessage="不能为空" ForeColor="Red" 
                                                 ValidationGroup="request_wuliu"></asp:RequiredFieldValidator>
                                                 </div>
                                                </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" align="right">
                                            <asp:Button ID="btn_wuliu" runat="server" 
                                                class="btn btn-primary" ValidationGroup="request_wuliu"
                                                Style="height: 35px; width: 100px" Text="提交" 
                                                onclick="btn_wuliu_Click" />
                                        </td>
                                    </tr>
                            </table>
                        
                        </div>
                    </div>
                </div>
            </div>
        </div>
      <div class="row  row-container"   style=" display:none">
            <div class="col-md-12">
                <div class="panel panel-info">
                    <div class="panel-heading" data-toggle="collapse" data-target="#ware">
                        <strong>仓库 </strong><span class="caret"></span>
                    </div>       
                    <div class="panel-body <% =Session["lv"].ToString() == "ware" ? "" : "collapse" %>" id="ware">             
                   <%-- <div class="panel-body " id="Div1">--%>
                        <div class="col-xs-12 col-sm-12  col-md-12 col-lg-12">
                            <table style="width: 100%">
                                <tr>
                                    <td>
                                        确认出货已完成日期：
                                    </td>
                                    <td>
                                     <div class="form-inline" style="color: #FF0000">
                                        <input id="txt_chuodate" class="form-control input-s-sm" style="width: 200px" onclick="laydate()"
                                            runat="server" controltovalidate="txt_chuodate" /> 
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" 
                                                 runat="server" ControlToValidate="txt_chuodate"
                                                ErrorMessage="不能为空" ForeColor="Red" 
                                                 ValidationGroup="request_ware"></asp:RequiredFieldValidator>
                                                </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        提货日期</td>
                                    <td>
                                     <div class="form-inline" style="color: #FF0000">
                                        <input id="txt_thdate" runat="server" onclick="laydate()"
                                            class="form-control input-s-sm"  style="height: 35px; 
                                                        width: 200px" />
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" 
                                                 runat="server" ControlToValidate="txt_thdate"
                                                ErrorMessage="不能为空" ForeColor="Red" 
                                                 ValidationGroup="request_ware"></asp:RequiredFieldValidator> 
                                                 </div>
                                                        </td>
                                    
                                </tr>
                                     <tr>
                                    <td>
                                        上传出货照片：
                                    </td>
                                    <td>
                                        <div class="form-inline">
                                            <asp:GridView ID="gv_chuo" runat="server" AutoGenerateColumns="False"
                                                DataKeyNames="id" CellPadding="4" ForeColor="#333333" 
                                                GridLines="None" onrowdeleting="gv_chuo_RowDeleting">
                                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                                <Columns>
                                                    <asp:BoundField ApplyFormatInEditMode="True" DataField="id" HeaderText="No."
                                                        ShowHeader="False" />
                                                    <asp:TemplateField HeaderText="文件名称" ShowHeader="False">
                                                        <ItemTemplate>
                                                            <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<% #DataBinder.Eval(Container.DataItem, "File_lj")%>'
                                                                Text='<%#DataBinder.Eval(Container.DataItem,"File_name")%>'></asp:HyperLink>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField ShowHeader="False" HeaderText="">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="false"
                                                                CommandName="Delete" Text="刪除"></asp:LinkButton>
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
                                            <input id="txt_ch_fj" type="file" class="form-control" style="width: 90px"
                                                multiple="multiple" runat="server" />
                                            <asp:Button ID="Btn_ch_fj" runat="server" CssClass="form-control"
                                                Text="上传文件" OnClick="Btn_ch_fj_Click" />
                                        </div>
                                    </td>
                                </tr>
                                 <tr>
                                        <td>提交说明</td>
                                        <td>
                                         <div class="form-inline" style="color: #FF0000">
                                            <input id="tjdesc_ware" runat="server" 
                                                class="form-control input-s-sm" style="width: 90%" />
                                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator9" 
                                                 runat="server" ControlToValidate="tjdesc_ware"
                                                ErrorMessage="不能为空" ForeColor="Red" 
                                                 ValidationGroup="request_ware"></asp:RequiredFieldValidator>
                                                 </div>
                                                </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" align="right">
                                            <asp:Button ID="btn_ck" runat="server" class="btn btn-primary" ValidationGroup="request_ware"
                                                Style="height: 35px; width: 100px" Text="提交" 
                                                onclick="btn_ck_Click" />
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
                <strong>订单当前阶段:</strong>
            </div>
            <div class="panel-body  " id="DDXX">
                <div>
                    <div class="">
                        <table border="1">
                            <tr>
                                <td colspan="3">
                                    <div class="form-inline" style="background-color: lightblue">
                                        <%--<input id="txt_status_id"  runat="server" readonly="true" />--%>
                                        当前阶段:</div>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3">
                                    <asp:Label ID="txt_status_id" runat="server"></asp:Label>
                                    <asp:Label ID="txt_status_name" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3" style="background-color: lightblue">
                                    具体信息:
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label3" runat="server" Font-Size="Small" Text="序号"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="Label1" runat="server" Font-Size="Small" Text="订单信息"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="Label2" runat="server" Font-Size="Small" Text="完成时间"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    0
                                </td>
                                <td>
                                    销售申请</td>
                                <td>
                                    <asp:Label ID="lb_ddqr" runat="server" Font-Size="Small"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-style2">
                                    1
                                </td>
                                <td class="auto-style2">
                                    物流订舱</td>
                                <td class="auto-style2">
                                    <asp:Label ID="lb_bh" runat="server" Font-Size="Small"></asp:Label>
                                </td>
                            </tr>
                            <tr style=" display:none">
                                <td>
                                    2
                                </td>
                                <td>
                                    仓库发货</td>
                                <td>
                                    <asp:Label ID="lb_jy" runat="server" Font-Size="Small"></asp:Label>
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
