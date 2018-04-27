<%@ Page Title="MES��������ϵͳ" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="baojia.aspx.cs" Inherits="baojia" MaintainScrollPositionOnPostback="True" ValidateRequest="true" %>

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
            $("#mestitle").text("���������롿");
            $('.selectpicker').change(function(){               
                $("input[id*='txt_wl_tk']").val($(".selectpicker").val());
            });
        })
        
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
            font-weight: 700;
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
        .auto-style3 {
            width: 100%;
        }
        </style>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div class="col-md-9">
        <%-- <asp:Label ID="Lab_price_year" runat="server" ForeColor="Red"></asp:Label>--%>
        <div class="row row-container">
            <div class="col-md-12">
                <div class="panel panel-info">
                    <div class="panel-heading" data-toggle="collapse" data-target="#SQXX">
                        <strong>��������Ϣ</strong>
                    </div>
           
                    <div class="panel-body <% =ViewState["lv"].ToString() == "SQXX" ? "" : "collapse" %>" id="SQXX">
                        <div class="col-xs-12 col-sm-12  col-md-12 col-lg-12">
                            <div class="">
                                <asp:UpdatePanel ID="UpdatePanel_request" runat="server">
                                    <ContentTemplate>
                                        <table style="height: 35px; width: 100% ">
                                            <tr>
                                                <td>�����ˣ�
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
                                                <td>���ţ�
                                                </td>
                                                <td>
                                                    <input id="txt_create_by_dept" class="form-control input-s-sm" style="height: 35px; width: 100px" runat="server" readonly="True" />
                                                   </td>
                                                <td>���ž���
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
                                                <td >��ǰ��½��Ա</td>
                                                <td>
                                                    <div class="form-inline">
                                                        <input id="txt_update_user" class="form-control input-s-sm" style="height: 35px; width: 100px" runat="server" readonly="True" />
                                                        /<input id="txt_update_user_name" class="form-control input-s-sm" style="height: 35px; width: 100px" runat="server" readonly="True" />
                                                    </div>
                                                </td>
                                                <td>&nbsp;</td>
                                                <td>&nbsp;</td>
                                                <td>
                                                    <asp:Label ID="Label8" runat="server" Text="ֱ������" Visible="False"></asp:Label>
                                                </td>
                                                <td>
                                                    <input id="txt_ZG_empid" class="form-control input-s-sm" style="height: 35px; width: 100px" runat="server" readonly="True" />
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
                    <div class="panel-heading" data-toggle="collapse" data-target="#XSGCS">
                        <strong>���۹���ʦ--��1.������Ŀ��Ϣ/������ϸ</strong>
                    </div>
                   
                    <div class="panel-body " id="XSGCS">
                        <div class="col-xs-12 col-sm-12  col-md-12 col-lg-12">

                            <div>
                                <fieldset style="border-color: lightblue">
                                    <legend>һ.������Ŀ��Ϣ</legend>
                                    <table style="width: 100%">
                                   
                                        <tr>
                                            <td>���ۺţ�</td>
                                            <td><div class="form-inline">
                                                     <input id="txt_baojia_no" class="form-control input-s-sm" style="height: 35px; width: 150px" runat="server" __designer:mapid="2d6" /><asp:RequiredFieldValidator ID="yz67" runat="server" ControlToValidate="txt_baojia_no" ErrorMessage="����Ϊ��" ValidationGroup="request" ForeColor="Red"></asp:RequiredFieldValidator></div> </td>
                                            <td>�ڼ���:</td>
                                            <td>
                                                <asp:DropDownList ID="DDL_turns" class="form-control input-s-sm" Style="height: 35px; width: 150px" runat="server" BackColor="WhiteSmoke" Enabled="False" ></asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="yz68" runat="server" ControlToValidate="DDL_turns" ErrorMessage="����Ϊ��" ValidationGroup="request" ForeColor="Red"></asp:RequiredFieldValidator>
                                      </td>
                                        </tr>
                                          <tr>
                                            <td>&nbsp;</td>
                                            <td>
                                                &nbsp;</td>
                                            <td>����״̬��</td>
                                            <td>
                                                <asp:DropDownList ID="DDL_baojia_status" class="form-control input-s-sm" Style="height: 35px; width: 150px" runat="server" BackColor="WhiteSmoke" Enabled="False"></asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>���빤��:</td>
                                            <td>
                                                     <asp:DropDownList ID="DDL_domain" class="form-control input-s-sm" Style="height: 35px; width: 150px" runat="server">
                                                    <asp:ListItem></asp:ListItem>
                                                    <asp:ListItem>�Ϻ�����</asp:ListItem>
                                                    <asp:ListItem>��ɽ����</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="yz63" runat="server" ControlToValidate="DDL_domain" ErrorMessage="����Ϊ��" ValidationGroup="request" ForeColor="Red"></asp:RequiredFieldValidator>
                                           </td>
                                            <td>������Ŀ��ʼʱ�䣺</td>
                                            <td>
                                               <input id="txt_baojia_start_date" class="form-control input-s-sm" style="height: 35px; width: 150px" runat="server" readonly="True" /><asp:RequiredFieldValidator ID="yz70" runat="server" ControlToValidate="txt_baojia_start_date" ErrorMessage="����Ϊ��" ValidationGroup="request" ForeColor="Red"></asp:RequiredFieldValidator></td>
                                        </tr>
                                 
                                 
                                        <tr>
                                            <td>ֱ�ӹ˿ͣ�</td>
                                            <td>
                                                <asp:DropDownList ID="DDL_customer_name" class="form-control input-s-sm" Style="height: 35px; width: 150px" runat="server"></asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="yz59" runat="server" ControlToValidate="DDL_customer_name" ErrorMessage="����Ϊ��" ValidationGroup="request" ForeColor="Red"></asp:RequiredFieldValidator>
                                                  </td>
                                            <td>���չ˿ͣ�</td>
                                            <td>
                                                                <asp:DropDownList ID="DDL_end_customer_name" runat="server" class="form-control input-s-sm" Style="height: 35px; width: 150px">
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="yz60" runat="server" ControlToValidate="DDL_end_customer_name" ErrorMessage="����Ϊ��" ForeColor="Red" ValidationGroup="request"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                 
                                           <tr>
                                            <td>������:</td>
                                            <td>
                                                <input id="txt_total_quantity_year" class="form-control input-s-sm" style="height: 35px; width: 150px" runat="server" readonly="True" /></td>
                                            <td>�����۶�:</td>
                                            <td>
                                                <input id="txt_total_price_year" class="form-control input-s-sm" style="height: 35px; width: 150px" runat="server" readonly="True" /></td>
                                        </tr>
                                        <tr>
                                            <td>�˿���Ŀ��ȫ����</td>
                                            <td>
                                                <input id="txt_customer_project" class="form-control input-s-sm" style="height: 35px; width: 260px" runat="server"  readonly="True" /></td>
                                            <td>����ʱ�䣺</td>
                                            <td>
                                                <input id="txt_pc_date" class="form-control input-s-sm" style="height: 35px; width: 150px" runat="server" readonly="True" /></td>
                                        </tr>
                                     
                                         <tr>
                                            <td>��Ŀ��С��</td>
                                            <td>
                                                                <asp:DropDownList ID="DDL_project_size" runat="server" AutoPostBack="True" class="form-control input-s-sm" OnSelectedIndexChanged="DDL_project_size_SelectedIndexChanged" Style="height: 35px; width: 150px">
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="yz61" runat="server" ControlToValidate="DDL_project_size" ErrorMessage="����Ϊ��" ForeColor="Red" ValidationGroup="request"></asp:RequiredFieldValidator>
                                                            </td>
                                            <td>��ȡ����</td>
                                            <td>
                                                                <asp:DropDownList ID="DDL_project_level" runat="server" class="form-control input-s-sm" Style="height: 35px; width: 150px">
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="yz62" runat="server" ControlToValidate="DDL_project_level" ErrorMessage="����Ϊ��" ForeColor="Red" ValidationGroup="request"></asp:RequiredFieldValidator>
                                                            </td>
                                            <td>
                                                &nbsp;</td>
                                        </tr>
                                     
                                        <tr>
                                            <td>&nbsp;</td>
                                            <td>
                                                                &nbsp;</td>
                                            <td>
                                                <asp:Label ID="Lab_Sales_project_level" runat="server" Text="�޸���ȡ����˵��:" Visible="False"></asp:Label>
                                            </td>
                                            <td>
                                                            <asp:TextBox ID="txt_BTN_Sales_project_level" class="form-control input-s-sm " style="height: 35px; width: 150px;display:none-" runat="server" Visible="False"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="yz76" runat="server" ControlToValidate="txt_BTN_Sales_project_level" ErrorMessage="����Ϊ��" ForeColor="Red" ValidationGroup="project_level"></asp:RequiredFieldValidator>
                                                            </td>
                                            <td>
                                                <asp:Button ID="BTN_Sales_project_level" runat="server" class="btn btn-primary" Style="height: 35px; width: 100px" Text="�޸���ȡ����" OnClick="BTN_Sales_project_level_Click" ValidationGroup="project_level" Visible="False"  />
                                                            </td>
                                        </tr>
                                         <tr>
                                            <td>��Ŀ˵����</td>
                                            <td colspan="4">
                                                                <input id="txt_baojia_desc" class="form-control input-s-sm" style="height: 35px; width: 90%" runat="server" __designer:mapid="c820" /><asp:RequiredFieldValidator ID="yz65" runat="server" ControlToValidate="txt_baojia_desc" ErrorMessage="����Ϊ��" ForeColor="Red" ValidationGroup="request"></asp:RequiredFieldValidator>
                                                            </td>
                                        </tr>
                                        <tr>
                                            <td>����·����</td>
                                            <td colspan="3">
                                                <asp:TextBox ID="txt_baojia_file_path" runat="server" 
                                                    class="form-control input-s-sm" 
                                                    Style="height: 35px; width: 90%"     ></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="yz64" runat="server" ControlToValidate="txt_baojia_file_path" ErrorMessage="����Ϊ��" ForeColor="Red" ValidationGroup="request"></asp:RequiredFieldValidator>
                                                                <asp:LinkButton ID="Link_baojia_file_path" runat="server" OnClick="Link_baojia_file_path_Click" Visible="False">���ӵ����۰�</asp:LinkButton>
                                                            </td>
                                        </tr>
                                     
                                        <tr>
                                            <td>&nbsp;</td>
                                            <td><div class="form-inline">
                                                <asp:DropDownList ID="DDL_sfxy_bjfx" class="form-control input-s-sm" style="height: 35px; width: 150px" runat="server"   OnSelectedIndexChanged="DDL_sfxy_bjfx_SelectedIndexChanged" AutoPostBack="True" Visible="False">
                                                    <asp:ListItem></asp:ListItem>
                                                    <asp:ListItem Value="��Ҫ��ϸ�۸����">��Ҫ��ϸ�۸����</asp:ListItem>
                                                    <asp:ListItem Value="����۸��ʵ">����۸��ʵ</asp:ListItem>
                                                    <asp:ListItem>����Ҫ�۸����</asp:ListItem>
                                                </asp:DropDownList>
                                                </div>
                                                <asp:Label ID="Label3" runat="server" ForeColor="Red"></asp:Label>
                                            </td>
                                            <td><asp:Label ID="Label2" runat="server" Text="������ת������:" Visible="False"></asp:Label>
                                                <asp:DropDownList ID="DDL_liuchengyizhuan" class="form-control input-s-sm" Style="height: 35px; width: 130px" runat="server" Visible="False">
                                                    <asp:ListItem></asp:ListItem>
                                                    <asp:ListItem>��</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:Button ID="BTN_Sales_yizhuan" runat="server" class="btn btn-primary" Style="height: 35px; width: 130px" Text="ȷ��" OnClick="BTN_Sales_yizhuan_Click" Visible="False" />
                                            </td>
                                            <td>
                                                            <asp:TextBox ID="txt_wl_tk" class="form-control input-s-sm " style="height: 35px; width: 220px;display:none-" runat="server"  Visible="False"></asp:TextBox>
                                    <select id="selectwl" name="selectwl" class="selectpicker " multiple  data-live-search="true" runat="server" style="width:100px" visible="False">                                          
                                    </select>
                                             
                                                <asp:DropDownList ID="DDL_is_stop" class="form-control input-s-sm" Style="height: 35px; width: 30px" runat="server" Visible="False"></asp:DropDownList>
                                                <input id="txt_dingdian_date" class="form-control input-s-sm" style="height: 35px; width: 30px" runat="server" readonly="True" Visible="False"/><input id="txt_hetong_complet_date" class="form-control input-s-sm" style="height: 35px; width: 30px" runat="server" readonly="True" Visible="False"/><asp:Button ID="BTN_Sales_tingzhi" runat="server" class="btn btn-primary" Style="height: 35px; width: 150px" Text="ֹͣ����׷��ȷ��" OnClick="BTN_Sales_tingzhi_Click" Visible="False" />
                                                
                                             
                                            </td>
                                        </tr>
                                    </table>
                           

                                <table style="width: 100%">
                                    <tr>
                                        <td>
                                            <fieldset style="border-color: lightblue">
                                                <legend>������ʷ��¼</legend>
                                                <div>
                                                    <asp:Label ID="lblHisMsg" runat="server"></asp:Label>
                                                    <asp:DataList ID="DataMst" runat="server" OnItemDataBound="DataMst_ItemDataBound" Width="100%">
                                                        <ItemTemplate>


                                                            <div id='<%# "TRClick"+Eval("turns")%>' data-target='<%# "#TRShow"+Eval("turns")%>' data-toggle="collapse" style="float: left; width: 100%; padding-right: 5px; margin-bottom: 1px;" turns='<%# Eval("turns")%>'>
                                                                <div id="TDClick" style="background-color: lightblue; padding-left: 5px; padding-right: 15px">
                                                                    �� <%#Eval("turns")%> ��(����鿴��ϸ) ------���ֱ���ԭ��<%#Eval("sign_desc")%></div>
                                                            </div>
                                                            <div id='<%# "TRShow"+Eval("turns")%>' class="collapse" style="float: left; margin-bottom: 3px; width: 100%;">
                                                                <asp:GridView ID="GridViewD" runat="server" CellPadding="8" AutoGenerateColumns="false"
                                                                    ForeColor="#333333" GridLines="None" ShowFooter="True"
                                                                    Width="100%" OnRowDataBound="GridViewD_RowDataBound">
                                                                    <RowStyle BackColor="#EFF3FB" />
                                                                    <FooterStyle BackColor="White" Font-Bold="True" ForeColor="Black" />
                                                                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                                                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                                    <EditRowStyle BackColor="#2461BF" />
                                                                    <AlternatingRowStyle BackColor="White" />
                                                                    <Columns>
                                                                        <asp:BoundField HeaderText="�����" DataField="�����" >
                                                                         <HeaderStyle Width="100px" />
                                                                         </asp:BoundField>
                                                                        <asp:BoundField HeaderText="�������" DataField="�������" >
                                                                         <HeaderStyle Width="100px" />
                                                                         </asp:BoundField>
                                                                          <asp:BoundField HeaderText="�˿���Ŀ" DataField="�˿���Ŀ" >
                                                                         <HeaderStyle Width="300px" />
                                                                         </asp:BoundField>
                                                                         <asp:BoundField HeaderText="Ship_from" DataField="Ship_from" >
                                                                         <HeaderStyle Width="100px" />
                                                                         </asp:BoundField>
                                                                         <asp:BoundField HeaderText="Ship_to" DataField="Ship_to" >
                                                                         <HeaderStyle Width="100px" />
                                                                         </asp:BoundField>
                                                                        <asp:BoundField HeaderText="������" DataField="������" DataFormatString="{0:N0}">
                                                                            <ItemStyle CssClass="alignRight" />
                                                                            <FooterStyle CssClass="alignRight" />
                                                                            <HeaderStyle CssClass="alignRight" Width="100px" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField HeaderText="����" DataField="����">
                                                                            <ItemStyle CssClass="alignRight" />
                                                                            <HeaderStyle CssClass="alignRight" Width="100px" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField HeaderText="��������" DataField="��������">
                                                                            <ItemStyle CssClass="alignRight" />
                                                                            <HeaderStyle CssClass="alignRight" Width="100px" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField HeaderText="��������" DataField="��������" DataFormatString="{0:N2}">
                                                                            <ItemStyle CssClass="alignRight" />
                                                                           <HeaderStyle CssClass="alignRight" Width="100px" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField HeaderText="�����۶�" DataField="�����۶�" DataFormatString="{0:N0}">
                                                                            <HeaderStyle CssClass="alignRight" Width="100px" />
                                                                            <ItemStyle CssClass="alignRight" />
                                                                            <FooterStyle HorizontalAlign="Right" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField HeaderText="����ģ�߼۸�" DataField="����ģ�߼۸�" DataFormatString="{0:N2}">
                                                                            <ItemStyle CssClass="alignRight" />
                                                                           <HeaderStyle CssClass="alignRight" Width="100px" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField HeaderText="��������" DataField="��������" DataFormatString="{0:N2}">
                                                                            <ItemStyle CssClass="alignRight" />
                                                                           <HeaderStyle CssClass="alignRight" Width="100px" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField HeaderText="����ģ�߼۸�" DataField="����ģ�߼۸�" DataFormatString="{0:N2}">
                                                                            <ItemStyle CssClass="alignRight" />
                                                                            <HeaderStyle CssClass="alignRight" Width="100px" />
                                                                        </asp:BoundField>
                                                                    </Columns>
                                                                </asp:GridView>
                                                                             <asp:GridView ID="GridView1" runat="server" CellPadding="8" AutoGenerateColumns="false"
                                                                    ForeColor="#333333" GridLines="None" ShowFooter="True"
                                                                    Width="100%" OnRowDataBound="GridViewD_RowDataBound">
                                                                    <RowStyle BackColor="#EFF3FB" />
                                                                    <FooterStyle BackColor="White" Font-Bold="True" ForeColor="Black" />
                                                                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                                                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                                    <EditRowStyle BackColor="#2461BF" />
                                                                    <AlternatingRowStyle BackColor="White" />
                                                                    <Columns>
                                                                        <asp:BoundField HeaderText="�����" DataField="�����" >
                                                                         <HeaderStyle Width="100px" />
                                                                         </asp:BoundField>
                                                                        <asp:BoundField HeaderText="�������" DataField="�������" >
                                                                         <HeaderStyle Width="100px" />
                                                                         </asp:BoundField>
                                                                          <asp:BoundField HeaderText="�˿���Ŀ" DataField="�˿���Ŀ" >
                                                                         <HeaderStyle Width="300px" />
                                                                         </asp:BoundField>
                                                                         <asp:BoundField HeaderText="Ship_from" DataField="Ship_from" >
                                                                         <HeaderStyle Width="100px" />
                                                                         </asp:BoundField>
                                                                         <asp:BoundField HeaderText="Ship_to" DataField="Ship_to" >
                                                                         <HeaderStyle Width="100px" />
                                                                         </asp:BoundField>
                                                                        <asp:BoundField HeaderText="������" DataField="������" DataFormatString="{0:N0}">
                                                                            <ItemStyle CssClass="alignRight" />
                                                                            <FooterStyle CssClass="alignRight" />
                                                                            <HeaderStyle CssClass="alignRight" Width="100px" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField HeaderText="����" DataField="����">
                                                                            <ItemStyle CssClass="alignRight" />
                                                                            <HeaderStyle CssClass="alignRight" Width="100px" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField HeaderText="��������" DataField="��������">
                                                                            <ItemStyle CssClass="alignRight" />
                                                                            <HeaderStyle CssClass="alignRight" Width="100px" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField HeaderText="��������" DataField="��������" DataFormatString="{0:N2}">
                                                                            <ItemStyle CssClass="alignRight" />
                                                                           <HeaderStyle CssClass="alignRight" Width="100px" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField HeaderText="�����۶�" DataField="�����۶�" DataFormatString="{0:N0}">
                                                                            <HeaderStyle CssClass="alignRight" Width="100px" />
                                                                            <ItemStyle CssClass="alignRight" />
                                                                            <FooterStyle HorizontalAlign="Right" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField HeaderText="����ģ�߼۸�" DataField="����ģ�߼۸�" DataFormatString="{0:N2}">
                                                                            <ItemStyle CssClass="alignRight" />
                                                                           <HeaderStyle CssClass="alignRight" Width="100px" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField HeaderText="��������" DataField="��������" DataFormatString="{0:N2}">
                                                                            <ItemStyle CssClass="alignRight" />
                                                                           <HeaderStyle CssClass="alignRight" Width="100px" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField HeaderText="����ģ�߼۸�" DataField="����ģ�߼۸�" DataFormatString="{0:N2}">
                                                                            <ItemStyle CssClass="alignRight" />
                                                                            <HeaderStyle CssClass="alignRight" Width="100px" />
                                                                        </asp:BoundField>
                                                                    </Columns>
                                                                </asp:GridView>
                                                            </div>
                                                        </ItemTemplate>
                                                    </asp:DataList>
                                                </div>
                                            </fieldset>
                                        </td>
                                    </tr>
                                </table>

                                </fieldset>
                                <table style="width: 100%">
                                     
                                    <tr>
                                        <td colspan="8">
                                            <asp:Panel ID="Panel1" runat="server" GroupingText="��һ�ֱ���">
                                      
                                                <div class="form-inline" style="margin-top: 10px">
                                                    <table class="auto-style3">
                                                        <tr>
                                                            <td colspan="11">             <div class="form-inline" style="margin-top: 10px">
                                    <label>���ֱ���ԭ��</label>
                                    <input id="txt_content" class="form-control input-s-sm" style="width:90%" runat="server" /><asp:RequiredFieldValidator ID="yz47" runat="server" ControlToValidate="txt_content" ErrorMessage="����ԭ����Ϊ��" ValidationGroup="request" ForeColor="Red"></asp:RequiredFieldValidator>
                                </div></td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="2">���ֱ��ۿ�ʼʱ�䣺</td>
                                                            <td>
                                                                <input id="txt_create_date" class="form-control input-s-sm" style="height: 35px; width: 150px" runat="server"   readonly="True" />
                           
                                                            </td>
                                                            <td>���ֱ��۽���ʱ�䣺</td>
                                                            <td>      
                                                                <input id="txt_baojia_end_date" class="form-control input-s-sm" style="height: 35px; width: 150px" runat="server"   readonly="True" />
                                                            </td>
                                                            <td>���۸����ˣ�</td>
                                                            <td colspan="5">        <div class="form-inline">
                                                    <input id="txt_sales_empid" class="form-control input-s-sm" style="height: 35px; width: 80px" runat="server" readonly="True" />/<input id="txt_sales_name" class="form-control input-s-sm" style="height: 35px; width: 80px" runat="server" readonly="True" />/<input id="txt_sales_ad" class="form-control input-s-sm" style="height: 35px; width: 90px" runat="server" readonly="True" />
                                                </div></td>
                                                        </tr>
                                                        <tr>
                                                            <td>���۷���Ҫ��</td>
                                                            <td>�������ۣ�</td>
                                                            <td>
                                                                <asp:DropDownList ID="DDL_wl_tk" runat="server" AutoPostBack="True" class="form-control input-s-sm" OnSelectedIndexChanged="DDL_wl_tk_SelectedIndexChanged" Style="height: 35px; width: 150px">
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="yz71" runat="server" ControlToValidate="DDL_wl_tk" ErrorMessage="����Ϊ��" ForeColor="Red" ValidationGroup="request"></asp:RequiredFieldValidator>
                                                            </td>
                                                            <td>��װ���ۣ�</td>
                                                            <td>
                                                                <asp:DropDownList ID="DDL_bz_tk" runat="server" AutoPostBack="True" class="form-control input-s-sm" OnSelectedIndexChanged="DDL_wl_tk_SelectedIndexChanged" Style="height: 35px; width: 150px">
                                                                    <asp:ListItem></asp:ListItem>
                                                                    <asp:ListItem>��Ҫ</asp:ListItem>
                                                                    <asp:ListItem>����Ҫ</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="yz73" runat="server" ControlToValidate="DDL_bz_tk" ErrorMessage="����Ϊ��" ForeColor="Red" ValidationGroup="request"></asp:RequiredFieldValidator>
                                                            </td>
                                                            <td>�ɹ�ѯ�ۣ� </td>
                                                            <td>
                                                                <asp:DropDownList ID="DDL_sfxj_cg" runat="server" AutoPostBack="True" class="form-control input-s-sm" OnSelectedIndexChanged="DDL_wl_tk_SelectedIndexChanged" style="height: 35px; width: 150px">
                                                                    <asp:ListItem></asp:ListItem>
                                                                    <asp:ListItem>��Ҫ</asp:ListItem>
                                                                    <asp:ListItem Value="����Ҫ">����Ҫ</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="yz72" runat="server" ControlToValidate="DDL_sfxj_cg" ErrorMessage="����Ϊ��" ForeColor="Red" ValidationGroup="request"></asp:RequiredFieldValidator>
                                                            </td>
                                                            <td>���ӱ��ۣ�</td>
                                                            <td>
                                                                <asp:DropDownList ID="DDL_jijia_tk" runat="server" AutoPostBack="True" class="form-control input-s-sm" OnSelectedIndexChanged="DDL_wl_tk_SelectedIndexChanged" style="height: 35px; width: 150px">
                                                                    <asp:ListItem></asp:ListItem>
                                                                    <asp:ListItem>��Ҫ</asp:ListItem>
                                                                    <asp:ListItem Value="����Ҫ">����Ҫ</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="yz74" runat="server" ControlToValidate="DDL_jijia_tk" ErrorMessage="����Ϊ��" ForeColor="Red" ValidationGroup="request"></asp:RequiredFieldValidator>
                                                                &nbsp;</td>
                                                            <td>ѹ������</td>
                                                            <td>
                                                                <asp:DropDownList ID="DDL_yz_tk" runat="server" AutoPostBack="True" class="form-control input-s-sm" OnSelectedIndexChanged="DDL_wl_tk_SelectedIndexChanged" style="height: 35px; width: 150px">
                                                                    <asp:ListItem></asp:ListItem>
                                                                    <asp:ListItem>��Ҫ</asp:ListItem>
                                                                    <asp:ListItem Value="����Ҫ">����Ҫ</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="yz75" runat="server" ControlToValidate="DDL_yz_tk" ErrorMessage="����Ϊ��" ForeColor="Red" ValidationGroup="request"></asp:RequiredFieldValidator>
                                                            </td>
                                                        </tr>
                                                      
                                                     
                                                     
                                                        <tr>
                                                            <td>&nbsp;</td>
                                                            <td colspan="10"  align="right">
                                                                <asp:Button ID="BTN_Sales_sub_update" runat="server" class="btn btn-primary " OnClick="BTN_Sales_sub_update_Click" Style="height: 10px; width: 130px" Text="�޸�" ValidationGroup="request" Visible="False" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                </div>
                                     <div class="form-inline" style="margin-top: 10px">
                                    <label> </label>
                                    </div>
                                                <asp:GridView ID="gv_bjmx" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                                    ForeColor="#333333" GridLines="None" ShowFooter="True"
                                                    OnRowCommand="gv_bjmx_RowCommand" Width="100%" OnRowDataBound="gv_bjmx_RowDataBound">
                                                    <RowStyle BackColor="#EFF3FB" />
                                                    <Columns>
                                                               <asp:TemplateField HeaderText="ԭ�����" Visible="False">                                                      
                                                        <ItemTemplate>
                                                            <asp:DropDownList ID="ddl_ljh_dt" runat="server" Style="width: 120px" AutoPostBack="True" OnSelectedIndexChanged="ddl_ljh_dt_SelectedIndexChanged">
                                                                <asp:ListItem></asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:TextBox ID="txtljh_dt" runat="server" Text='<%#DataBinder.Eval(Container,"DataItem.xz_ljh") %>' Visible="False"></asp:TextBox>
                                                        </ItemTemplate>
                                                                  
                                                    </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="�����">
                                                            <FooterTemplate>
                                                                <asp:Label ID="Label1" runat="server" ForeColor="Red" Text="�ϼƣ�"></asp:Label>
                                                            </FooterTemplate>
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txt_ljh" runat="server" Width="120px" Text='<%#DataBinder.Eval(Container,"DataItem.ljh") %>' AutoPostBack="True" OnTextChanged="txt_ljh_TextChanged"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="�������">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txt_lj_name" runat="server" Width="200px" Text='<%#DataBinder.Eval(Container,"DataItem.lj_name") %>'></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                          <asp:TemplateField HeaderText="�˿���Ŀ">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txt_customer_project" runat="server" Width="150px" Text='<%#DataBinder.Eval(Container,"DataItem.customer_project") %>'></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                           <asp:TemplateField HeaderText="ship_from">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txt_ship_from" runat="server" Width="70px" Text='<%#DataBinder.Eval(Container,"DataItem.ship_from") %>'></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                         <asp:TemplateField HeaderText="Ship_to">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txt_ship_to" runat="server" Width="70px" Text='<%#DataBinder.Eval(Container,"DataItem.ship_to") %>'></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="������">
                                                            <FooterTemplate>
                                                               
                                                                  <asp:TextBox ID="Lab_quantity_year" runat="server" ForeColor="Red" Width="70px" CssClass="textalign" BackColor="#CCCCCC" ReadOnly="True"></asp:TextBox>
                                                            </FooterTemplate>
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txt_quantity_year" runat="server" Width="70px" Text='<%#DataBinder.Eval(Container,"DataItem.quantity_year","{0:N0}") %>' CssClass="textalign"></asp:TextBox>
                                                            </ItemTemplate>
                                                            <FooterStyle />
                                                            <ItemStyle />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="����">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txt_material" runat="server" Width="80px" Text='<%#DataBinder.Eval(Container,"DataItem.material") %>'></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="��������">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txt_pc_date" runat="server" Width="80px" onclick="laydate()" Text='<%#DataBinder.Eval(Container,"DataItem.pc_date","{0:yyyy-MM-dd}") %>'></asp:TextBox>

                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="��������">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txt_pc_per_price" runat="server" Width="70px" Text='<%#DataBinder.Eval(Container,"DataItem.pc_per_price","{0:N2}") %>' CssClass="textalign" AutoPostBack="True" OnTextChanged="txt_pc_per_price_TextChanged"></asp:TextBox>
                                                            </ItemTemplate>
                                                            <ItemStyle />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="�����۶�">
                                                            <FooterTemplate>
                                                               <%-- <asp:Label ID="Lab_price_year" runat="server" ForeColor="Red"></asp:Label>--%>
                                                                <asp:TextBox ID="Lab_price_year" runat="server" ForeColor="Red" Width="80px" CssClass="textalign" ReadOnly="True" BackColor="#CCCCCC"></asp:TextBox>
                                                            </FooterTemplate>
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txt_price_year" runat="server" Width="80px" Text='<%#DataBinder.Eval(Container,"DataItem.price_year","{0:N0}") %>' CssClass="textalign" ReadOnly="True"></asp:TextBox>
                                                            </ItemTemplate>
                                                            <FooterStyle  />
                                                            <ItemStyle />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="����ģ�߼۸�">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txt_pc_mj_price" runat="server" Width="70px" Text='<%#DataBinder.Eval(Container,"DataItem.pc_mj_price","{0:N0}") %>' CssClass="textalign"></asp:TextBox>
                                                            </ItemTemplate>
                                                            <ItemStyle />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="��������">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txt_yj_per_price" runat="server" Width="70px" Text='<%#DataBinder.Eval(Container,"DataItem.yj_per_price","{0:N2}") %>' CssClass="textalign"></asp:TextBox>
                                                            </ItemTemplate>
                                                            <ItemStyle />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="����ģ�߼۸�">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txt_yj_mj_price" runat="server" Width="70px" Text='<%#DataBinder.Eval(Container,"DataItem.yj_mj_price","{0:N0}") %>' CssClass="textalign"></asp:TextBox>
                                                            </ItemTemplate>
                                                            <ItemStyle />
                                                        </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="��ע">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txt_back_up" runat="server" Width="270px" Text='<%#DataBinder.Eval(Container,"DataItem.back_up","{0:N0}") %>' CssClass="textalign"></asp:TextBox>
                                                            </ItemTemplate>
                                                            <ItemStyle />
                                                        </asp:TemplateField>
                                                             <asp:TemplateField HeaderText="old_ljh" Visible="False">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txt_old_ljh" runat="server" Width="70px" Text='<%#DataBinder.Eval(Container,"DataItem.old_ljh") %>'></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <FooterTemplate>
                                                                <asp:Button ID="btnAdd" runat="server" CommandName="add" ForeColor="#3333FF" Text="���" />
                                                            </FooterTemplate>
                                                            <ItemTemplate>
                                                                <asp:Button ID="btnDel" runat="server" Text="ɾ��" CommandName="del" CommandArgument='<%#Container.DataItemIndex %>' ForeColor="#6600FF" Font-Size="Smaller" />
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
                                        <td colspan="8">

                                            <asp:Panel ID="Panel2" runat="server" GroupingText="���۽��ȿ���(���չ��)" data-toggle="collapse" data-target="#ProControl">
                                            </asp:Panel>
                                            <div id="ProControl" class="panel-body <% =ViewState["lv"].ToString() == "BJJD" ? "" : "collapse" %>">
                                                <asp:GridView ID="gv_bjjd" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" ShowFooter="True" OnRowCommand="gv_bjjd_RowCommand">
                                                    <RowStyle BackColor="#EFF3FB" />
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="ID" Visible="False">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="id" runat="server" Enabled="False" Text='<%# DataBinder.Eval(Container, "DataItem.id")%>' Width="100px"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="flowid" Visible="False">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="flowid" runat="server" Enabled="False" Text='<%# DataBinder.Eval(Container, "DataItem.flowid")%>' Width="100px"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="big_flowid" Visible="False">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="big_flowid" runat="server" Enabled="False" Text='<%# DataBinder.Eval(Container, "DataItem.big_flowid")%>' Width="100px"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="stepid" Visible="False">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="stepid" runat="server" Enabled="False" Text='<%# DataBinder.Eval(Container, "DataItem.stepid")%>' Width="100px"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="����">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="flow" runat="server" Text='<%#DataBinder.Eval(Container,"DataItem.flow") %>' Width="50px"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="��ɫ">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="step" runat="server" Text='<%#DataBinder.Eval(Container,"DataItem.step") %>' Width="100px"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="����">
                                                            <ItemTemplate>
                                                                <asp:DropDownList ID="ddl_empid" runat="server" Style="width: 70px">
                                                                    <asp:ListItem></asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:Button ID="btn_tj" runat="server" CommandArgument="<%#Container.DataItemIndex %>" CommandName="tj" ForeColor="#6600FF" Text="���" Visible="False" />
                                                                <asp:TextBox ID="txt" runat="server" Text='<%#DataBinder.Eval(Container,"DataItem.empid") %>' ReadOnly="True" Visible="False"></asp:TextBox>
                                                                <asp:Button ID="bt_delete" runat="server" CommandArgument="<%#Container.DataItemIndex %>" CommandName="delete_qk" ForeColor="#6600FF" Text="���" Visible="False" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="���ۿ�ʼʱ��">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="receive_date" runat="server" ReadOnly="True" Text='<%#DataBinder.Eval(Container,"DataItem.receive_date","{0:yyyy-MM-dd}") %>' Width="80px"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Ҫ�����ʱ��">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="require_date" runat="server" Width="80px" onclick="laydate()" Text='<%#DataBinder.Eval(Container,"DataItem.require_date","{0:yyyy-MM-dd}") %>' ReadOnly="True"></asp:TextBox>

                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="ʵ�����ʱ��">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="sign_date" runat="server" Text='<%#DataBinder.Eval(Container,"DataItem.sign_date","{0:yyyy-MM-dd}") %>' Width="80px"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="ʵ����ʱ">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="Operation_time" runat="server" Text='<%#DataBinder.Eval(Container,"DataItem.Operation_time") %>' Width="100px"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="�ύ˵��">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Lab_yz" runat="server" Font-Size="Larger" ForeColor="Red" Text="*" Visible="False"></asp:Label>
                                                                <asp:TextBox ID="txt_sign_desc" runat="server" Width="400px" Text='<%#DataBinder.Eval(Container,"DataItem.sign_desc") %>'></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:Button ID="btncomit" runat="server" CommandArgument="<%#Container.DataItemIndex %>" CommandName="comit" ForeColor="#6600FF" Text="ȷ��" />
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
                                            </div>


                                        </td>
                                    </tr>

                                    <tr>
                                        <td colspan="8" align="right">
                                            <asp:Button ID="BTN_Sales_3" runat="server" class="btn btn-primary" Style="height: 35px; width: 130px" Text="����" OnClick="BTN_Sales_3_Click" />
                                        </td>
                                    </tr>
                                    <tr>
                                       <td colspan="8" align="right">
                                            <asp:Button ID="BTN_Sales_sub" runat="server" class="btn btn-primary " Style="height: 35px; width: 130px" Text="�ύ" ValidationGroup="request" OnClick="BTN_Sales_sub_Click" />
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
        <div class="row  row-container">
            <div class="col-md-12">
                <div class="panel panel-info">
                    <div class="panel-heading" data-toggle="collapse" data-target="#htgz">
                        <strong>�������״̬���� </strong><span class="caret"></span>
                    </div>
                    <div class="panel-body <% =ViewState["lv"].ToString() == "htgz" ? "" : "collapse" %>" id="htgz">
                        <div class="col-xs-12 col-sm-12  col-md-12 col-lg-12">
                            <table style="width: 100%">
                                <tr>
                                    <td colspan="5">
                                        <asp:Panel ID="Panel3" runat="server" GroupingText="�������״̬����">
                                            <asp:GridView ID="gv_htgz" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" OnRowCommand="gv_htgz_RowCommand" ShowFooter="True" Width="100%" OnRowDataBound="gv_htgz_RowDataBound">
                                                <RowStyle BackColor="#EFF3FB" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="id">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="id" runat="server" Text='<%#DataBinder.Eval(Container,"DataItem.id") %>' Width="30px" Enabled="False" ReadOnly="True"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="ѡ�������" Visible="False">                                                      
                                                        <ItemTemplate>
                                                            <asp:DropDownList ID="ddl_ljh" runat="server" Style="width: 200px" AutoPostBack="True" OnSelectedIndexChanged="ddl_ljh_SelectedIndexChanged">
                                                                <asp:ListItem></asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:TextBox ID="txtljh" runat="server" Text='<%#DataBinder.Eval(Container,"DataItem.xz_ljh") %>' Visible="False"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="�����">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="ljh" runat="server" Text='<%#DataBinder.Eval(Container,"DataItem.ljh") %>' Width="150px" Enabled="False" ReadOnly="True"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="�˿���Ŀ">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txt_customer_project" runat="server" Width="150px" Text='<%#DataBinder.Eval(Container,"DataItem.customer_project") %>' Enabled="False" ReadOnly="True"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                         <asp:TemplateField HeaderText="Ship_from">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="Ship_from" runat="server" Text='<%#DataBinder.Eval(Container,"DataItem.Ship_from") %>' Width="100px" Enabled="False" ReadOnly="True"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="Ship_to">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="Ship_to" runat="server" Text='<%#DataBinder.Eval(Container,"DataItem.Ship_to") %>' Width="100px" Enabled="False" ReadOnly="True"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="��������">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="dingdian_date" runat="server" Text='<%#DataBinder.Eval(Container,"DataItem.dingdian_date","{0:yyyy-MM-dd}") %>' Width="80px" onclick="laydate()"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                          <asp:TemplateField HeaderText="��������">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="pc_date" runat="server" Text='<%#DataBinder.Eval(Container,"DataItem.pc_date","{0:yyyy-MM-dd}") %>' Width="80px" onclick="laydate()"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                       <asp:TemplateField HeaderText="��������">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="end_date" runat="server" Text='<%#DataBinder.Eval(Container,"DataItem.end_date","{0:yyyy-MM-dd}") %>' Width="80px" onclick="laydate()" ></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>    
                                                       <asp:TemplateField HeaderText="������">
                                                            <FooterTemplate>
                                                               <asp:Label ID="Label7" runat="server" ForeColor="Red" Text="�ϼ�:"></asp:Label><br>
                                                                  <asp:TextBox ID="Lab_quantity_year" runat="server" ForeColor="Red" Width="80px" CssClass="textalign" BackColor="#CCCCCC" ReadOnly="True"></asp:TextBox>
                                                            </FooterTemplate>
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txt_quantity_year" runat="server" Width="80px" Text='<%#DataBinder.Eval(Container,"DataItem.quantity_year","{0:N0}") %>' CssClass="textalign" AutoPostBack="True" OnTextChanged="txt_pc_per_price_TextChanged1"></asp:TextBox>
                                                            </ItemTemplate>
                                                            <FooterStyle />
                                                            <ItemStyle />
                                                        </asp:TemplateField>
                                                         <asp:TemplateField HeaderText="�ұ�">
                                                        <ItemTemplate>
                                                            <asp:DropDownList ID="ddl_currency" runat="server" Style="width: 60px">
                                                                <asp:ListItem></asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:TextBox ID="txtcurrency" runat="server" Text='<%#DataBinder.Eval(Container,"DataItem.currency") %>' Visible="False"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="����">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txt_exchange_rate" runat="server" Width="60px" Text='<%#DataBinder.Eval(Container,"DataItem.exchange_rate","{0:N4}") %>' CssClass="textalign" AutoPostBack="True" OnTextChanged="txt_pc_per_price_TextChanged1" ></asp:TextBox>
                                                            </ItemTemplate>
                                                            <ItemStyle />
                                                        </asp:TemplateField>
                                                      <asp:TemplateField HeaderText="��������">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txt_pc_per_price" runat="server" Width="60px" Text='<%#DataBinder.Eval(Container,"DataItem.pc_per_price","{0:N2}") %>' CssClass="textalign" AutoPostBack="True" OnTextChanged="txt_pc_per_price_TextChanged1"></asp:TextBox>
                                                            </ItemTemplate>
                                                            <ItemStyle />
                                                        </asp:TemplateField>
                                                      
                                                       <asp:TemplateField HeaderText="�����۶�(����)">
                                                            <FooterTemplate>
                                                           <asp:Label ID="Label6" runat="server" ForeColor="Red" Text="���Һϼ�:"></asp:Label><br>
                                                                <asp:TextBox ID="Lab_price_year" runat="server" ForeColor="Red" Width="80px" CssClass="textalign" ReadOnly="True" BackColor="#CCCCCC"></asp:TextBox>
                                                            </FooterTemplate>
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txt_price_year" runat="server" Width="80px" Text='<%#DataBinder.Eval(Container,"DataItem.price_year","{0:N0}") %>' CssClass="textalign" ReadOnly="True"></asp:TextBox>
                                                            </ItemTemplate>
                                                            <FooterStyle  />
                                                            <ItemStyle />
                                                        </asp:TemplateField>
                                                      <asp:TemplateField HeaderText="ģ�߼۸�(ԭ��)">
                                                             <FooterTemplate>
                                                           
                                                                 <asp:Label ID="Label5" runat="server" ForeColor="Red" Text="���Һϼ�:"></asp:Label><br>
                                                           
                                                                <asp:TextBox ID="Lab_pc_mj_price" runat="server" ForeColor="Red" Width="80px" CssClass="textalign" ReadOnly="True" BackColor="#CCCCCC"></asp:TextBox>
                                                            </FooterTemplate>
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txt_pc_mj_price" runat="server" Width="80px" Text='<%#DataBinder.Eval(Container,"DataItem.pc_mj_price","{0:N0}") %>' CssClass="textalign"></asp:TextBox>
                                                            </ItemTemplate>
                                                            <ItemStyle />
                                                        </asp:TemplateField>                                                                                                                                                              
                                                    <asp:TemplateField HeaderText="�յ���Ϣ" Visible="False">
                                                        <ItemTemplate>
                                                            <asp:DropDownList ID="ddl_sdxx" runat="server" Style="width: 100px">
                                                                <asp:ListItem></asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:TextBox ID="txtsdxx" runat="server" Text='<%#DataBinder.Eval(Container,"DataItem.sdxx") %>' Visible="False"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                         <asp:TemplateField HeaderText="���״̬">
                                                        <ItemTemplate>
                                                            <asp:DropDownList ID="ddl_lj_status" runat="server" Style="width: 100px">
                                                                <asp:ListItem></asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:TextBox ID="txtlj_status" runat="server" Text='<%#DataBinder.Eval(Container,"DataItem.lj_status") %>' Visible="False"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="˵��">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="Description" runat="server" Text='<%#DataBinder.Eval(Container,"DataItem.Description") %>' Width="150px"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="������">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="create_by_empid" runat="server" Width="50px" Text='<%#DataBinder.Eval(Container,"DataItem.create_by_empid") %>' ReadOnly="True" Enabled="False"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <FooterTemplate>
                                                            <asp:Button ID="btnAddht" runat="server" CommandName="addht" ForeColor="#3333FF" Text="���" />
                                                        </FooterTemplate>
                                                        <ItemTemplate>
                                                            <asp:Button ID="btnDelht" runat="server" Text="ɾ��" CommandName="delht" CommandArgument='<%#Container.DataItemIndex %>' ForeColor="#6600FF" />
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
                                        </asp:Panel>
                                    </td>
                                </tr>
                                   <tr>
                                    <td align="right" colspan="5">
                                        <asp:Button ID="BTN_Sales_2" runat="server" class="btn btn-primary" Style="height: 35px; width: 130px" Text="�������϶�����Ϣ" OnClick="BTN_Sales_2_Click" />
                                    </td>
                                </tr>
                                 <tr>
                                    <td colspan="5">
                                        <asp:Panel ID="Panel4" runat="server" GroupingText="���״̬����" Visible="False">
                                            <asp:GridView ID="gv_ljztgz" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None"   Width="100%" OnRowCommand="gv_ljztgz_RowCommand" >
                                                <RowStyle BackColor="#EFF3FB" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="id">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txt_id" runat="server" Width="30px" Text='<%#DataBinder.Eval(Container,"DataItem.id") %>'  Enabled="false"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="�����">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txt_ljh" runat="server" Width="150px" Text='<%#DataBinder.Eval(Container,"DataItem.ljh") %>' ReadOnly="True" Enabled="False"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="�˿���Ŀ">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txt_customer_project" runat="server" Width="150px" Text='<%#DataBinder.Eval(Container,"DataItem.customer_project") %>' ReadOnly="True" Enabled="False"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="ship_from">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txt_ship_from" runat="server" Width="120px" Text='<%#DataBinder.Eval(Container,"DataItem.ship_from") %>' ReadOnly="True" Enabled="False"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="ship_to">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txt_ship_to" runat="server" Width="120px" Text='<%#DataBinder.Eval(Container,"DataItem.ship_to") %>' ReadOnly="True" Enabled="False"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="��������">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txt_jieshu_date" runat="server" Text='<%#DataBinder.Eval(Container,"DataItem.end_date","{0:yyyy-MM-dd}") %>' Width="80px" onclick="laydate()" ></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>                                                                                                                                                        
                                                         <asp:TemplateField HeaderText="���״̬">
                                                        <ItemTemplate>
                                                            <asp:DropDownList ID="ddl_lj_status" runat="server" Style="width: 150px" >
                                                                <asp:ListItem></asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:TextBox ID="txtlj_status" runat="server" Text='<%#DataBinder.Eval(Container,"DataItem.lj_status") %>' Visible="False"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="˵��">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txt_ztgz_description" runat="server" Text='<%#DataBinder.Eval(Container,"DataItem.ztgz_description") %>' Width="200px" ></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="������">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="ztgz_empid" runat="server" Width="50px" Text='<%#DataBinder.Eval(Container,"DataItem.ztgz_empid") %>' ReadOnly="True" Enabled="False"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>  
                                                     <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:Button ID="btncommit" runat="server" Text="����"  CommandName="ztgz_commit" CommandArgument='<%#Container.DataItemIndex %>' ForeColor="#6600FF"  />
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
                                        </asp:Panel>


                                    </td>
                                </tr>
                             
                             
                                   <tr>
                                    <td >
                                        &nbsp;<td >
                                                <input id="txt_hetong_complet_date_qr" class="form-control input-s-sm" style="height: 35px; width: 150px" runat="server" Visible="False" onclick="laydate()" /></td>
                                    <td >
                                        <asp:Label ID="Label4" runat="server" Text="���ж��������ȫ���յ�����ͬ�����" Visible="False"></asp:Label>
                                    </td>
                                    <td >
                                        <asp:Button ID="BTN_Sales_4" runat="server" class="btn btn-primary" Style="height: 35px; width: 130px" Text="ȷ�Ϻ�ͬ�����" OnClick="BTN_Sales_4_Click" Visible="False" />
                                    </td>
                                    <td >
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
                        <strong>������־                   <span class="caret"></span>
                    </div>
                    <%#Eval("turns")%>
                    <div class="panel-body <% =ViewState["lv"].ToString() == "CZRZ" ? "" : "collapse" %>" id="CZRZ">

                        <div class="col-xs-12 col-sm-12  col-md-12 col-lg-12">
                            <table style="height: 35px; width: 100%">
                                <tr>
                                    <td>һ.������Ա��¼</td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:GridView ID="gv_rz1" Width="100%"
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
                                                <asp:BoundField DataField="Flow" HeaderText="�����ڵ�"
                                                    ReadOnly="True">
                                                    <HeaderStyle Wrap="True" />
                                                    <ItemStyle Width="80px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Step" HeaderText="�����ڵ�����"
                                                    ReadOnly="True">
                                                    <HeaderStyle Wrap="True" />
                                                    <ItemStyle Width="80px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="lastname" HeaderText="������Ա"
                                                    ReadOnly="True">
                                                    <HeaderStyle Wrap="True" />
                                                    <ItemStyle Width="80px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="receive_date" HeaderText="����ʱ��"
                                                    ReadOnly="True">
                                                    <HeaderStyle Wrap="True" />
                                                    <ItemStyle Width="150px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="require_date" HeaderText="Ҫ��ʱ��"
                                                    ReadOnly="True" DataFormatString="{0:yyyy-MM-dd}">
                                                    <HeaderStyle Wrap="True" />
                                                    <ItemStyle Width="80px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="sign_date" HeaderText="����ʱ��">
                                                    <HeaderStyle Wrap="False" />
                                                    <ItemStyle Width="120px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Operation_time" HeaderText="��ʱ">
                                                    <HeaderStyle Wrap="False" />
                                                    <ItemStyle Width="100px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="sign_desc" HeaderText="�ύ˵��">
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
                                        <%#Eval("turns")%>��ǰ�׶�:
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:Label ID="txt_status_id" runat="server"></asp:Label>
                                    :<asp:Label ID="txt_status_name" runat="server" Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Size="Large" Font-Strikeout="False" Font-Underline="False" ForeColor="#6600CC"></asp:Label></td>
                            </tr>
                            <tr>
                                <td colspan="2" style="background-color: lightblue"><asp:Label ID="Lab_htzt" runat="server" Font-Size="Large" Font-Underline="False" ForeColor="#6600CC" Visible="False"></asp:Label>
                                    </td>
                            </tr>
                            <tr>
                                <td colspan="2">

                                    <asp:GridView ID="gv_rz2" Width="100%"
                                        AllowMultiColumnSorting="True" AllowPaging="True"
                                        AllowSorting="True" AutoGenerateColumns="False"
                                        runat="server" Font-Size="Small" Visible="False">
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
                                            <asp:BoundField DataField="lookup_code" HeaderText="lookup_code"
                                                ReadOnly="True" Visible="False">
                                                <HeaderStyle Wrap="True" />
                                                <ItemStyle Width="10%" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="״̬" HeaderText="״̬">
                                                <HeaderStyle Wrap="False" />
                                                <ItemStyle Width="20%" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="�������" HeaderText="�������">
                                                <HeaderStyle Wrap="False" />
                                                <ItemStyle Width="50%" />
                                            </asp:BoundField>
                                        </Columns>
                                    </asp:GridView>
                                </td>

                            </tr>
                        </table>
                        <table border="1" style="width: 100%">

                            <tr>
                                <td>&nbsp;</td>

                            </tr>

                            <tr>
                                <td>
                                    <div>
                                        <asp:Label runat="server" ID="lblHisMsg_log"></asp:Label>
                                        <asp:DataList ID="DataMst_log" runat="server" OnItemDataBound="DataMst_log_ItemDataBound" Width="100%">
                                            <ItemTemplate>
                                                <div id='<%# "TRClickLog"+Eval("turns")%>' turns='<%# Eval("turns")%>' data-toggle="collapse" data-target='<%# "#TRShowlog"+Eval("turns")%>' style="float: left; width: 100%; padding-right: 5px; margin-bottom: 1px;">
                                                    <div id="TDClicklog" style="background-color: padding-left: 5px; padding-right: 15px; vertical-align: middle"><i class="fa fa-arrow-circle-down " style="font-size: 20px; color: blue"></i>�� <%#Eval("turns")%> ��(����鿴��ϸ)</div>
                                                </div>
                                                <div id='<%# "TRShowlog"+Eval("turns")%>' style="float: left; margin-bottom: 3px; margin-left: 10px" class="collapse">
                                                    <asp:GridView ID="GridViewD_log" runat="server" GridLines="None" ShowHeader="false" AutoGenerateColumns="false" Font-Size="12px" OnRowDataBound="GridViewD_log_RowDataBound">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="">
                                                                <ItemTemplate>
                                                                    <i id="MyStyleSheet" class="fa fa-arrow-circle-down" runat="server"></i>
                                                                </ItemTemplate>
                                                                <ItemStyle Width="10px" VerticalAlign="Middle" CssClass="his" HorizontalAlign="Left" />
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="sign_date">
                                                                <ItemStyle CssClass="his" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="lastname">
                                                                <ItemStyle CssClass="his" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="step">
                                                                <ItemStyle CssClass="his" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="require_date">
                                                                <ItemStyle CssClass="his" />
                                                            </asp:BoundField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                            </ItemTemplate>
                                        </asp:DataList>
                                    </div>
                                     <div class="" style="display:none">
                                        <i class="fa fa-arrow-circle-down " style="font-size: 20px; color: blue"></i>
                                         <strong>��ͬ״̬��</strong>
                                    </div>
                                    <div>
                                        <asp:GridView ID="Gridview_hetong_log" Width="100%"
                                            AllowMultiColumnSorting="True" AllowPaging="True"
                                            AllowSorting="True" AutoGenerateColumns="False"
                                            runat="server" Font-Size="Small" GridLines="None" ShowHeader="False" Visible="False">
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
                                                <asp:BoundField DataField="ljh" HeaderText="�����"
                                                    ReadOnly="True">
                                                    <HeaderStyle Wrap="True" HorizontalAlign="Right" />
                                                    <ItemStyle Width="160px" VerticalAlign="Bottom" HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="sdxx" HeaderText="�յ���Ϣ"
                                                    ReadOnly="True">
                                                    <HeaderStyle Wrap="True" HorizontalAlign="Right" />
                                                    <ItemStyle Width="160px" VerticalAlign="Bottom" HorizontalAlign="left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="dingdian_date" HeaderText="�յ�����"
                                                    ReadOnly="True" DataFormatString="{0:yyyy-MM-dd}">
                                                    <HeaderStyle Wrap="True" HorizontalAlign="Right" />
                                                    <ItemStyle Width="70px" VerticalAlign="Bottom" HorizontalAlign="left" />
                                                </asp:BoundField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                     <div class="" style="display:none">
                                        <i class="fa fa-arrow-circle-down " style="font-size: 20px; color: blue"></i>
                                         ���<strong>״̬��</strong>
                                    </div>
                                    <div>
                                        <asp:GridView ID="gv_ljzt" Width="100%"
                                            AllowMultiColumnSorting="True" AllowPaging="True"
                                            AllowSorting="True" AutoGenerateColumns="False"
                                            runat="server" Font-Size="Small" GridLines="None" ShowHeader="False" Visible="False">
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
                                                <asp:BoundField DataField="ljh" HeaderText="�����"
                                                    ReadOnly="True">
                                                    <HeaderStyle Wrap="True" HorizontalAlign="Right" />
                                                    <ItemStyle Width="160px" VerticalAlign="Bottom" HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="lj_status" HeaderText="���״̬"
                                                    ReadOnly="True">
                                                    <HeaderStyle Wrap="True" HorizontalAlign="Right" />
                                                    <ItemStyle Width="160px" VerticalAlign="Bottom" HorizontalAlign="left" />
                                                </asp:BoundField>                                      
                                            </Columns>
                                        </asp:GridView>
                                    </div>
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
