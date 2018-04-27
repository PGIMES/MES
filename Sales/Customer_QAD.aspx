<%@ Page Title="MES��������ϵͳ" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="Customer_QAD.aspx.cs" Inherits="Customer_QAD" MaintainScrollPositionOnPostback="True" ValidateRequest="true" %>

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
            $("#mestitle").text("���ͻ���Ϣά����");
            $('.selectpicker').change(function () {
                $("input[id*='txt_Domain2']").val($(".selectDomain").val());
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
            font-size: 12px;
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

        .auto-style2 {
            height: 54px;
        }

        .tbl td {
            border: 1px solid black;
            padding-left: 3px;
            padding-right: 3px;
            padding-top: 3px;
        }

        .auto-style3 {
            height: 35px;
        }

        .auto-style4 {
            height: 22px;
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
                        <strong>��������Ϣ</strong>
                    </div>

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
                                                        <input id="txtUserid" class="form-control input-s-sm" style="height: 35px; width: 100px" runat="server" readonly="True" />
                                                        /
                                                    <input id="txtUserName" class="form-control input-s-sm" style="height: 35px; width: 100px" runat="server" readonly="True" />
                                                        /
                                                    <input id="txtUserName_AD" class="form-control input-s-sm" style="height: 35px; width: 100px" runat="server" readonly="True" />
                                                    </div>
                                                </td>
                                                <td>���ţ�
                                                </td>
                                                <td>
                                                    <input id="txtdept" class="form-control input-s-sm" style="height: 35px; width: 100px" runat="server" readonly="True" />
                                                </td>
                                                <td>���ž���
                                                </td>
                                                <td>
                                                    <div class="form-inline">
                                                        <input id="txtmanagerid" class="form-control input-s-sm" style="height: 35px; width: 100px" runat="server" readonly="True" />
                                                        /<input id="txtmanager" class="form-control input-s-sm" style="height: 35px; width: 100px" runat="server" readonly="True" />
                                                        /<input id="txtmanager_AD" class="form-control input-s-sm" style="height: 35px; width: 100px" runat="server" readonly="True" />
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>�ִΣ�</td>
                                                <td>
                                                    <asp:Label ID="Lab_turns" runat="server" Font-Size="Small" Font-Underline="False" ForeColor="#6600CC"></asp:Label>
                                                </td>
                                                <td>�������ڣ�</td>
                                                <td>
                                                    <input id="txtCreateDate" class="form-control input-s-sm" style="height: 35px; width: 100px" runat="server" readonly="True" />
                                                </td>
                                                <td>Code��</td>
                                                <td>     
                                                    <input id="txtCode" class="form-control input-s-sm" style="height: 35px; width: 200px" runat="server" readonly="True" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>��ǰ��½��Ա</td>
                                                <td>
                                                    <div class="form-inline">
                                                        <input id="txt_update_user" class="form-control input-s-sm" style="height: 35px; width: 100px" runat="server" readonly="True" />
                                                        /<input id="txt_update_user_name" class="form-control input-s-sm" style="height: 35px; width: 100px" runat="server" readonly="True" />/
                                                        <input id="txt_update_user_job" class="form-control input-s-sm" style="height: 35px; width: 100px" runat="server" readonly="True" />
                                                        /<input id="txt_update_user_dept" class="form-control input-s-sm" style="height: 35px; width: 100px" runat="server" readonly="True" />
                                                    </div>
                                                </td>
                                                <td>ֱ�����ܣ�</td>
                                                <td>
                                                       <div class="form-inline">
                                                    <input id="txt_ZG_empid" class="form-control input-s-sm" style="height: 35px; width: 100px" runat="server" readonly="True" /> 
                                                    /<input id="txt_ZG" class="form-control input-s-sm" style="height: 35px; width: 100px" runat="server" readonly="True" />
                                                        </div></td>
                                                <td>&nbsp;</td>
                                                <td>
                                                    &nbsp;</td>
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
                    <div class="panel-heading" data-toggle="collapse" data-target="#khdl">
                        �ͻ����� (���۲�����д)
                    </div>
                    <div class="panel-body " id="khdl">
                        <div class="col-xs-12 col-sm-12  col-md-12 col-lg-12">

                            <div>
                                <fieldset style="border-color: lightblue">
                                    <legend>һ.������Ϣ</legend>
                                    <table style="width: 100%">
                                        <tr>
                                            <td class="auto-style2">���</td>
                                            <td class="auto-style2">
                                                <asp:DropDownList ID="DDL_Class" class="form-control input-s-sm" Style="height: 35px; width: 150px" runat="server" OnSelectedIndexChanged="DDL_Class_SelectedIndexChanged" AutoPostBack="True">
                                                    <asp:ListItem>���пͻ�����</asp:ListItem>
                                                    <asp:ListItem>�����ͻ�����</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="yz61" runat="server" ControlToValidate="DDL_Class" ErrorMessage="����Ϊ��" ValidationGroup="request" ForeColor="Red"></asp:RequiredFieldValidator>
                                            </td>
                                            <td class="auto-style2">
                                                <asp:Label ID="lab_custumer_name_new" runat="server" Text="�����ͻ��������ƣ�"></asp:Label>
                                            </td>
                                            <td class="auto-style2">
                                                <input id="txt_ExistsClass" class="form-control input-s-sm" style="height: 35px; width: 150px; background-color: #FF0000;" runat="server" disabled="True" /></td>

                                        </tr>
                                        <tr>
                                            <td class="auto-style2">
                                                <asp:Label ID="lab_custumer_name" runat="server" Text="���пͻ���������:"></asp:Label>
                                            </td>
                                            <td class="auto-style2">
                                                <asp:DropDownList ID="DDL_cmClassName" class="form-control input-s-sm" Style="height: 35px; width: 150px" runat="server" OnSelectedIndexChanged="DDL_custumer_name_SelectedIndexChanged" AutoPostBack="True">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="yz48" runat="server" ControlToValidate="DDL_cmClassName" ErrorMessage="����Ϊ��" ValidationGroup="request" ForeColor="Red"></asp:RequiredFieldValidator>
                                            </td>
                                            <td class="auto-style2">�ͻ��������</td>
                                            <td class="auto-style2">
                                                <input id="txtCmClassID" class="form-control input-s-sm" style="height: 35px; width: 150px" runat="server" readonly="True" /></td>

                                        </tr>
                                    </table>


                                </fieldset>


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
                    <div class="panel-heading" data-toggle="collapse" data-target="#YWGX">
                        ҵ���ϵ&�ͻ�����(���۲�����д)&nbsp;&nbsp;&nbsp; ��������Ϊ��<asp:Label ID="Lab_update" runat="server" Font-Bold="True" Font-Size="Large" ForeColor="#FF3300"></asp:Label>
                    </div>
                    <div class="panel-body <% =ViewState["lv"].ToString() == "YWGX" ? "" : "collapse" %>" id="YWGX">
                           <%#Eval("turns")%>
                        <div class="col-xs-12 col-sm-12  col-md-12 col-lg-12">
                            <div class="">
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>      </ContentTemplate>
                                </asp:UpdatePanel>
                                        <table style="height: 35px; width: 100%">
                                            <tr>
                                                <td>���빤����</td>
                                                <td>
                                                    <asp:CheckBoxList ID="CBL_cm_domain" runat="server" RepeatDirection="Horizontal" Font-Size="Small">
                                                        <asp:ListItem>�Ϻ�����</asp:ListItem>
                                                        <asp:ListItem>��ɽ����</asp:ListItem>
                                                    </asp:CheckBoxList>
                                                </td>
                                                <td>�ͻ����ͣ�</td>
                                                <td>
                                                    <asp:DropDownList ID="DDL_DebtorTypeCode" runat="server" class="form-control input-s-sm" Style="height: 35px; width: 150px">
                                                        <asp:ListItem></asp:ListItem>
                                                        <asp:ListItem Value="10">���ڿͻ�</asp:ListItem>
                                                        <asp:ListItem Value="20">����ͻ�</asp:ListItem>
                                                        <asp:ListItem Value="30">�ڲ�����</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="yz82" runat="server" ControlToValidate="DDL_DebtorTypeCode" ErrorMessage="����Ϊ��" ForeColor="Red" ValidationGroup="request"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="LabBusinessRelationCode" runat="server" Text="ҵ���ϵ(�ͻ�)����"></asp:Label>
                                                    ��</td>
                                                <td>
                                                    <div class="form-inline">
                                                        <input id="txtBusinessRelationCode" class="form-control input-s-sm" style="height: 35px; width: 150px" runat="server" readonly="True" />
                                                        <asp:RequiredFieldValidator ID="yz60" runat="server" ControlToValidate="txtBusinessRelationCode" ErrorMessage="����Ϊ��" ForeColor="Red" ValidationGroup="request"></asp:RequiredFieldValidator>
                                                    </div>
                                                </td>
                                                <td>
                                                    <asp:Label ID="LabBusinessRelationName1" runat="server" Text="ҵ���ϵ(�ͻ�)����"></asp:Label>
                                                    ��</td>
                                                <td>
                                                    <input id="txtBusinessRelationName1" class="form-control input-s-sm" style="height: 35px; width: 350px" runat="server" />
                                                    <asp:RequiredFieldValidator ID="yz62" runat="server" ControlToValidate="txtBusinessRelationName1" ErrorMessage="����Ϊ��" ForeColor="Red" ValidationGroup="request"></asp:RequiredFieldValidator>
                                                    &nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td>�������ƣ�</td>
                                                <td>
                                                    <div class="form-inline">
                                                        <input id="txtAddressSearchName" class="form-control input-s-sm" style="height: 35px; width: 150px" runat="server" />
                                                        <asp:RequiredFieldValidator ID="yz63" runat="server" ControlToValidate="txtAddressSearchName" ErrorMessage="����Ϊ��" ForeColor="Red" ValidationGroup="request"></asp:RequiredFieldValidator>
                                                        &nbsp;
                                                    </div>
                                                </td>
                                                <td>��ַ���ͣ�</td>
                                                <td>
                                                    <input id="txtAddressTypeCode" class="form-control input-s-sm" style="height: 35px; width: 150px" runat="server" readonly="True" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>��ַ��</td>
                                                <td colspan="3">
                                                    <input id="txtAddressStreet1" class="form-control input-s-sm" style="height: 35px; width: 90%" runat="server" />
                                                    <asp:RequiredFieldValidator ID="yz64" runat="server" ControlToValidate="txtAddressStreet1" ErrorMessage="����Ϊ��" ForeColor="Red" ValidationGroup="request"></asp:RequiredFieldValidator>
                                                    &nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td>���Դ��룺</td>
                                                <td>
                                                    <asp:DropDownList ID="DDLcm_lang" runat="server" class="form-control input-s-sm" Style="height: 35px; width: 150px">
                                                        <asp:ListItem></asp:ListItem>
                                                        <asp:ListItem>ch</asp:ListItem>
                                                        <asp:ListItem>us</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="yz65" runat="server" ControlToValidate="DDLcm_lang" ErrorMessage="����Ϊ��" ForeColor="Red" ValidationGroup="request"></asp:RequiredFieldValidator>
                                                    &nbsp;</td>
                                                <td>����/������</td>
                                                <td>
                                                    <div class="form-inline">
                                                        <asp:DropDownList ID="DDLtry_country" runat="server" class="form-control input-s-sm" Style="height: 35px; width: 150px" AutoPostBack="True" OnSelectedIndexChanged="DDL_try_country_SelectedIndexChanged">
                                                        </asp:DropDownList>
                                                        <input id="txtcm_region" class="form-control input-s-sm" style="height: 35px; width: 150px" runat="server" readonly="True" />
                                                        <asp:RequiredFieldValidator ID="yz66" runat="server" ControlToValidate="DDLtry_country" ErrorMessage="����Ϊ��" ForeColor="Red" ValidationGroup="request"></asp:RequiredFieldValidator>
                                                        &nbsp;
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="auto-style3">�ʱࣺ</td>
                                                <td class="auto-style3">
                                                    <input id="txtAddressZip" class="form-control input-s-sm" style="height: 35px; width: 150px" runat="server" />
                                                    <asp:RequiredFieldValidator ID="yz67" runat="server" ControlToValidate="txtAddressZip" ErrorMessage="����Ϊ��" ForeColor="Red" ValidationGroup="request"></asp:RequiredFieldValidator>
                                                    &nbsp;</td>
                                                <td class="auto-style3">���У�</td>
                                                <td class="auto-style3">
                                                    <input id="txtAddressCity" class="form-control input-s-sm" style="height: 35px; width: 150px" runat="server" />
                                                    <asp:RequiredFieldValidator ID="yz68" runat="server" ControlToValidate="txtAddressCity" ErrorMessage="����Ϊ��" ForeColor="Red" ValidationGroup="request"></asp:RequiredFieldValidator>
                                                    &nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td>ʡ�ݣ�</td>
                                                <td>
                                                    <input id="txtAddressState" class="form-control input-s-sm" style="height: 35px; width: 150px" runat="server" />
                                                    <asp:RequiredFieldValidator ID="yz69" runat="server" ControlToValidate="txtAddressState" ErrorMessage="����Ϊ��" ForeColor="Red" ValidationGroup="request"></asp:RequiredFieldValidator>
                                                    &nbsp;</td>
                                                <td>���棺</td>
                                                <td>
                                                    <input id="txtad_fax" class="form-control input-s-sm" style="height: 35px; width: 150px" runat="server" />
                                                    <asp:RequiredFieldValidator ID="yz70" runat="server" ControlToValidate="txtad_fax" ErrorMessage="����Ϊ��" ForeColor="Red" ValidationGroup="request"></asp:RequiredFieldValidator>
                                                    &nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td>�绰��</td>
                                                <td>
                                                    <input id="txtad_phone" class="form-control input-s-sm" style="height: 35px; width: 150px" runat="server" />
                                                    <asp:RequiredFieldValidator ID="yz71" runat="server" ControlToValidate="txtad_phone" ErrorMessage="����Ϊ��" ForeColor="Red" ValidationGroup="request"></asp:RequiredFieldValidator>
                                                    &nbsp;</td>
                                                <td>�ʼ���ַ(��˾�ٷ�)��</td>
                                                <td>
                                                    <input id="txtAddressEMail" class="form-control input-s-sm" style="height: 35px; width: 150px" runat="server" />
                                                    <asp:RequiredFieldValidator ID="yz72" runat="server" ControlToValidate="txtAddressEMail" ErrorMessage="����Ϊ��" ForeColor="Red" ValidationGroup="request"></asp:RequiredFieldValidator>
                                                    &nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td>��ϵ��������</td>
                                                <td>
                                                    <input id="txtContactName" class="form-control input-s-sm" style="height: 35px; width: 150px" runat="server" />
                                                    <asp:RequiredFieldValidator ID="yz73" runat="server" ControlToValidate="txtContactName" ErrorMessage="����Ϊ��" ForeColor="Red" ValidationGroup="request"></asp:RequiredFieldValidator>
                                                    &nbsp;</td>
                                                <td>�Ա�</td>
                                                <td>
                                                    <asp:DropDownList ID="DDLContactGender" runat="server" class="form-control input-s-sm" Style="height: 35px; width: 150px">
                                                        <asp:ListItem></asp:ListItem>
                                                        <asp:ListItem>��</asp:ListItem>
                                                        <asp:ListItem>Ů</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="yz74" runat="server" ControlToValidate="DDLContactGender" ErrorMessage="����Ϊ��" ForeColor="Red" ValidationGroup="request"></asp:RequiredFieldValidator>
                                                    &nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td>�ֻ����룺</td>
                                                <td>
                                                    <input id="txtContactTelephone" class="form-control input-s-sm" style="height: 35px; width: 150px" runat="server" />
                                                    <asp:RequiredFieldValidator ID="yz75" runat="server" ControlToValidate="txtContactTelephone" ErrorMessage="����Ϊ��" ForeColor="Red" ValidationGroup="request"></asp:RequiredFieldValidator>
                                                    &nbsp;</td>
                                                <td>�����ʼ�(����)��</td>
                                                <td>
                                                    <input id="txtContactEmail" class="form-control input-s-sm" style="height: 35px; width: 150px" runat="server" />
                                                    <asp:RequiredFieldValidator ID="yz76" runat="server" ControlToValidate="txtContactEmail" ErrorMessage="����Ϊ��" ForeColor="Red" ValidationGroup="request"></asp:RequiredFieldValidator>
                                                    &nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td>��˰��ַ��</td>
                                                <td>
                                                    <asp:CheckBox ID="CBAddressIsTaxable" runat="server" />
                                                    &nbsp;</td>
                                                <td>��˰��</td>
                                                <td>
                                                    <asp:CheckBox ID="CBAddressIsTaxIncluded" runat="server" />
                                                    &nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td>����˰��</td>
                                                <td>
                                                    <asp:CheckBox ID="CBAddressIsTaxInCity" runat="server" />
                                                </td>
                                                <td>&nbsp;</td>
                                                <td>&nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td>˰����</td>
                                                <td>
                                                    <asp:DropDownList ID="DDLTxzTaxZone" runat="server" class="form-control input-s-sm" Style="height: 35px; width: 150px">
                                                        <asp:ListItem></asp:ListItem>
                                                        <asp:ListItem>ch</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="yz77" runat="server" ControlToValidate="DDLTxzTaxZone" ErrorMessage="����Ϊ��" ForeColor="Red" ValidationGroup="request"></asp:RequiredFieldValidator>
                                                    &nbsp;</td>
                                                <td>˰�֣�</td>
                                                <td>
                                                    <asp:DropDownList ID="DDLTxclTaxCls" runat="server" class="form-control input-s-sm"  Style="height: 35px; width: 150px">
                                                        <asp:ListItem></asp:ListItem>
                                                        <asp:ListItem>0</asp:ListItem>
                                                        <asp:ListItem>3</asp:ListItem>
                                                        <asp:ListItem>5</asp:ListItem>
                                                        <asp:ListItem>6</asp:ListItem>
                                                        <asp:ListItem>11</asp:ListItem>
                                                        <asp:ListItem>13</asp:ListItem>
                                                        <asp:ListItem>17</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="yz78" runat="server" ControlToValidate="DDLTxclTaxCls" ErrorMessage="����Ϊ��" ForeColor="Red" ValidationGroup="request"></asp:RequiredFieldValidator>
                                                    &nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td>���ڣ�</td>
                                                <td>
                                                    <asp:DropDownList ID="DDLLedgerDays" runat="server" class="form-control input-s-sm" Style="height: 35px; width: 150px">
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="yz79" runat="server" ControlToValidate="DDLLedgerDays" ErrorMessage="����Ϊ��" ForeColor="Red" ValidationGroup="request"></asp:RequiredFieldValidator>
                                                    &nbsp;</td>
                                                <td>�̶��۸�</td>
                                                <td>
                                                    <asp:CheckBox ID="CBcm_fix_pr" runat="server" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="auto-style4">����Ա��</td>
                                                <td class="auto-style4">
                                                    <asp:DropDownList ID="DDLcm_slspsn" runat="server" class="form-control input-s-sm" Style="height: 35px; width: 150px">
                                                        <asp:ListItem></asp:ListItem>
                                                        <asp:ListItem>Stuart</asp:ListItem>
                                                        <asp:ListItem>Tim</asp:ListItem>
                                                    </asp:DropDownList>
                                                    &nbsp;</td>
                                                <td class="auto-style4">�ۿ۱�</td>
                                                <td class="auto-style4">
                                                    <input id="txtcm_pr_list" class="form-control input-s-sm" style="height: 35px; width: 150px" runat="server" readonly="True"/>
                                                    <asp:RequiredFieldValidator ID="yz81" runat="server" ControlToValidate="txtcm_pr_list" ErrorMessage="����Ϊ��" ForeColor="Red" ValidationGroup="request"></asp:RequiredFieldValidator>
                                                    &nbsp;</td>
                                            </tr>
                                        </table>

                              
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="row  row-container">
            <div class="col-md-12">
                <div class="panel panel-info">
                    <div class="panel-heading" data-toggle="collapse" data-target="#HWFW">
                        �ͻ����﷢���ͻ����﷢��(���۲�����д)
                       </div>
                    <div class="panel-body <% =ViewState["lv"].ToString() == "HWFW" ? "" : "collapse" %>" id="HWFW">
                       <%-- <div class="panel-body " id="HWFW">--%>
                        <div class="col-xs-12 col-sm-12  col-md-12 col-lg-12">
                            <table style="width: 100%">
                                <tr>
                                    <td colspan="2">
                                        <asp:Panel ID="Panel3" runat="server" GroupingText="�ͻ����﷢��:1.����Ϊ��ɫ2.ʧЧΪ���ɫ">
                                            <asp:GridView ID="gv_DebtorShipTo" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" ShowFooter="True" Width="100%" OnRowCommand="gv_DebtorShipTo_RowCommand" OnRowDataBound="gv_DebtorShipTo_RowDataBound">
                                                <RowStyle BackColor="#EFF3FB" />
                                                <Columns>
                                                          <asp:TemplateField HeaderText="turns" Visible="False">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="turns" runat="server" Text='<%#DataBinder.Eval(Container,"DataItem.turns") %>' Width="80px" Enabled="False" ReadOnly="True"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="ID" Visible="False">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="ID" runat="server" Text='<%#DataBinder.Eval(Container,"DataItem.ID") %>' Width="80px" Enabled="False" ReadOnly="True"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="�ͻ�����">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="BusinessRelationCode" runat="server" Text='<%#DataBinder.Eval(Container,"DataItem.BusinessRelationCode") %>' Style="height: 30px; width: 50px"   Enabled="False"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="�����յ���ˮ��&lt;br&gt;�Զ�����">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="ShipToNum" runat="server" Text='<%#DataBinder.Eval(Container,"DataItem.ShipToNum") %>' Style="height: 30px; Width:90px" Enabled="False"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                           <asp:TemplateField HeaderText="��ַ����">
                                                        <ItemTemplate>
                                                           <asp:DropDownList ID="ddl_AddressTypeCode" runat="server" Style="height: 30px;width: 80px" >
                                                                <asp:ListItem></asp:ListItem>
                                                                <asp:ListItem>����</asp:ListItem>
                                                                <asp:ListItem>����</asp:ListItem>
                                                               <asp:ListItem>����/����</asp:ListItem>
                                                                <asp:ListItem>�ۺ��</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:TextBox ID="AddressTypeCode" runat="server" Text='<%#DataBinder.Eval(Container,"DataItem.AddressTypeCode") %>' Style="height: 30px;Width:70px" Enabled="False"  Visible="False" ></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="�����յ����">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="DebtorShipToCode" runat="server" Text='<%#DataBinder.Eval(Container,"DataItem.DebtorShipToCode") %>' Style="height: 30px;Width:80px"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="�����յ�����">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="DebtorShipToName" runat="server" Text='<%#DataBinder.Eval(Container,"DataItem.DebtorShipToName") %>' Style="height: 30px;Width:150px"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="��������">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="cm_addr" runat="server" Text='<%#DataBinder.Eval(Container,"DataItem.cm_addr") %>' Style="height: 30px;Width:70px"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                             

                                                    <asp:TemplateField HeaderText="�ʱ�">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="AddressZip" runat="server" Text='<%#DataBinder.Eval(Container,"DataItem.AddressZip") %>' Style="height: 30px;Width:50px"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="����">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="AddressCity" runat="server" Text='<%#DataBinder.Eval(Container,"DataItem.AddressCity") %>' Style="height: 30px;Width:50px"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                         <asp:TemplateField HeaderText="����">
                                             
                                                                    <ItemTemplate>
                                                            <asp:DropDownList ID="ddl_Lctry_country" runat="server" Style="height: 30px;width: 70px" AutoPostBack="True" OnSelectedIndexChanged="ddl_Lctry_country_SelectedIndexChanged">
                                                                <asp:ListItem></asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:TextBox ID="txt_ctry_country" runat="server" Text='<%#DataBinder.Eval(Container,"DataItem.ctry_country") %>'  Style="height: 30px;Width:70px" Visible="False"  ></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="����">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="cm_region" runat="server" Text='<%#DataBinder.Eval(Container,"DataItem.cm_region") %>' Style="height: 30px;Width:50px" Enabled="False"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                  
                                                    <asp:TemplateField HeaderText="����">             
                                                                <ItemTemplate>
                                                            <asp:DropDownList ID="ddl_LngCode" runat="server" Style="height: 30px;width: 50px" >
                                                                <asp:ListItem>ch</asp:ListItem>
                                                                <asp:ListItem>us</asp:ListItem>
                                                            </asp:DropDownList>
                                                                
                                                            <asp:TextBox ID="txt_LngCode" runat="server" Text='<%#DataBinder.Eval(Container,"DataItem.LngCode") %>'  Style="height: 30px;Width:50px" Visible="False" ></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="��">
                                                        <ItemTemplate>
                                                            <asp:CheckBoxList ID="CBL_Debtor_Domain" runat="server" Style="height: 30px;Width:100px" RepeatDirection="Horizontal">
                                                                <asp:ListItem Value="100">�Ϻ�</asp:ListItem>
                                                                <asp:ListItem Value="200">��ɽ</asp:ListItem>
                                                            </asp:CheckBoxList>
                                                            <asp:TextBox ID="txt_Debtor_Domain" runat="server" Text='<%#DataBinder.Eval(Container,"DataItem.Debtor_Domain") %>' Style="height: 30px;Width:100px" Visible="False"  ></asp:TextBox>
                                                            
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                                                                        <asp:TemplateField HeaderText="��ַ">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="AddressStreet1" runat="server" Text='<%#DataBinder.Eval(Container,"DataItem.AddressStreet1") %>' Style="height: 30px;Width:350px"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="�Ƿ���Ч">
                                                  
                                                           <ItemTemplate>
                                                            <asp:DropDownList ID="ddl_IsEffective" runat="server" Style="height: 30px;width: 60px">
                                                                <asp:ListItem>��Ч</asp:ListItem>
                                                                <asp:ListItem>��Ч</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:TextBox ID="txt_IsEffective" runat="server" Text='<%#DataBinder.Eval(Container,"DataItem.IsEffective") %>'  Style="height: 30px;Width:60px" Visible="False" ></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="����ʱ��">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="UpdateDate" runat="server" Style="height: 30px;Width:70px" Text='<%#DataBinder.Eval(Container,"DataItem.UpdateDate") %>' Enabled="False" ReadOnly="True"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                      <asp:TemplateField HeaderText="�����˹���" Visible="False">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="UpdateById" runat="server" Style="height: 30px;Width:50px" Text='<%#DataBinder.Eval(Container,"DataItem.UpdateById") %>' ReadOnly="True" Enabled="False"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="������">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="UpdateByName" runat="server" Style="height: 30px;Width:50px" Text='<%#DataBinder.Eval(Container,"DataItem.UpdateByName") %>' ReadOnly="True" Enabled="False"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <FooterTemplate>
                                                            <asp:Button ID="btnAddQTY" runat="server" CommandName="addQTY" ForeColor="#3333FF" Text="���" />
                                                        </FooterTemplate>
                                                        <ItemTemplate>
                                                            <asp:Button ID="btnDelQTY" runat="server" Text="ɾ��" CommandName="delQTY" CommandArgument='<%#Container.DataItemIndex %>' ForeColor="#6600FF" />
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
                                                <div id="ShowNumHeader" style="width: 50%; display: inline; float: left">.</div>
                                                <div id="divShowNumByMonth" style="width: 50%; text-align: left; display: inline; float: left"></div>
                                            </div>
                                        </asp:Panel>


                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" width="900px">
                                        <asp:Button ID="BTN_Sales_submit" runat="server" class="btn btn-primary " Style="height: 35px; width: 130px" Text="�ύ" ValidationGroup="request" OnClick="BTN_Sales_sub_Click" Visible="False" />
                                    </td>
                                    <td align="right">&nbsp;</td>
                                </tr>


                            </table>

                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div class="row row-container">
            <div class="col-md-12">
                <div class="panel panel-info">
                    <div class="panel-heading" data-toggle="collapse" data-target="#CWSJ">
                     �ͻ���������(��������д)
                    </div>         
                    <div class="panel-body <% =ViewState["lv"].ToString() == "CWSJ" ? "" : "collapse" %>" id="CWSJ">
                        <div class="col-xs-12 col-sm-12  col-md-12 col-lg-12">
                            <div class="">
                                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                    <ContentTemplate>
                                        <table style="height: 35px; width: 100%">
                                            <tr>
                                                <td>����</td>
                                                <td>
                                                    <div class="form-inline">
                                                        <asp:DropDownList ID="DDLBankNumberIsActive" runat="server" class="form-control input-s-sm" Style="height: 35px; width: 150px">
                                                            <asp:ListItem>��</asp:ListItem>
                                                            <asp:ListItem>��</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="yz83" runat="server" ControlToValidate="DDLBankNumberIsActive" ErrorMessage="����Ϊ��" ForeColor="Red" ValidationGroup="FIN"></asp:RequiredFieldValidator>
                                                        &nbsp;
                                                    </div>
                                                </td>
                                                <td>&nbsp;</td>
                                                <td>&nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td>�������������ļ�(��Ʊ)��</td>
                                                <td>
                                                    <div class="form-inline">
                                                        <input id="txtInvControlGLProfileCode" class="form-control input-s-sm" style="height: 35px; width: 150px" runat="server" />
                                                        <asp:RequiredFieldValidator ID="yz84" runat="server" ControlToValidate="txtInvControlGLProfileCode" ErrorMessage="����Ϊ��" ForeColor="Red" ValidationGroup="FIN"></asp:RequiredFieldValidator>
                                                        &nbsp;</div>
                                                </td>
                                                <td>�������������ļ�������Ʊ�ݣ���</td>
                                                <td>
                                                    <div class="form-inline">
                                                    <input id="txtCnControlGLProfileCode" class="form-control input-s-sm" style="height: 35px; width: 150px" runat="server" />
                                                    <asp:RequiredFieldValidator ID="yz85" runat="server" ControlToValidate="txtCnControlGLProfileCode" ErrorMessage="����Ϊ��" ForeColor="Red" ValidationGroup="FIN"></asp:RequiredFieldValidator>
                                                    &nbsp;</div></td>
                                            </tr>
                                            <tr>
                                                <td>�������������ļ���Ԥ����)��</td>
                                                <td>
                                                    <div class="form-inline">
                                                    <input id="txtPrepayControlGLProfileCode" class="form-control input-s-sm" style="height: 35px; width: 150px" runat="server" />
                                                    <asp:RequiredFieldValidator ID="yz86" runat="server" ControlToValidate="txtPrepayControlGLProfileCode" ErrorMessage="����Ϊ��" ForeColor="Red" ValidationGroup="FIN"></asp:RequiredFieldValidator>
                                                    &nbsp;</div></td>
                                                <td>�����˻����������ļ���</td>
                                                <td>  <div class="form-inline">
                                                    <input id="txtSalesAccountGLProfileCode" class="form-control input-s-sm" style="height: 35px; width: 150px" runat="server" />
                                                    <asp:RequiredFieldValidator ID="yz87" runat="server" ControlToValidate="txtSalesAccountGLProfileCode" ErrorMessage="����Ϊ��" ForeColor="Red" ValidationGroup="FIN"></asp:RequiredFieldValidator>
                                                    &nbsp;</div></td> 
                                            </tr>
                                            <tr>
                                                <td class="auto-style3">��Ʊ״̬��</td>
                                                <td class="auto-style3">
                                                    <div class="form-inline">
                                                    <asp:DropDownList ID="DDLReasonCode" runat="server" class="form-control input-s-sm" Style="height: 35px; width: 150px">
                                                        <asp:ListItem>Sales </asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="yz88" runat="server" ControlToValidate="DDLReasonCode" ErrorMessage="����Ϊ��" ForeColor="Red" ValidationGroup="FIN"></asp:RequiredFieldValidator>
                                                </div></td>
                                                <td class="auto-style3">�Ŵ����ޣ�</td>
                                                <td class="auto-style3">   <div class="form-inline">
                                                    <asp:DropDownList ID="DDLConditionCode" runat="server" class="form-control input-s-sm" Style="height: 35px; width: 150px">
                                                </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="yz89" runat="server" ControlToValidate="DDLConditionCode" ErrorMessage="����Ϊ��" ForeColor="Red" ValidationGroup="FIN"></asp:RequiredFieldValidator>
                                                 </div></td>
                                            </tr>
                                            <tr>
                                                <td>���Ҵ��룺</td>
                                                <td> <div class="form-inline">
                                                    <asp:DropDownList ID="DDLCurrencyCode" runat="server" class="form-control input-s-sm" Style="height: 35px; width: 150px">
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="yz90" runat="server" ControlToValidate="DDLCurrencyCode" ErrorMessage="����Ϊ��" ForeColor="Red" ValidationGroup="FIN"></asp:RequiredFieldValidator>
                                                   </div></td>
                                                <td>���и�ʽ��</td>
                                                <td><div class="form-inline">
                                                    <input id="txtBankAccFormatCode" class="form-control input-s-sm" style="height: 35px; width: 150px" runat="server" />
                                                    <asp:Label ID="LabBankAccFormatCode" runat="server" Text=""></asp:Label>
                                                    &nbsp;<asp:RequiredFieldValidator ID="yz91" runat="server" ControlToValidate="txtBankAccFormatCode" ErrorMessage="����Ϊ��" ForeColor="Red" ValidationGroup="FIN"></asp:RequiredFieldValidator>
                                                   </div></td>
                                            </tr>
                                            <tr>
                                                <td>�ͻ������˺ţ�</td>
                                                <td><div class="form-inline">
                                                    <input id="txtBankNumberFormatted" class="form-control input-s-sm" style="height: 35px; width: 150px" runat="server" />
                                                    <asp:RequiredFieldValidator ID="yz92" runat="server" ControlToValidate="txtBankNumberFormatted" ErrorMessage="����Ϊ��" ForeColor="Red" ValidationGroup="FIN"></asp:RequiredFieldValidator>
                                                    </div></td>
                                                <td>���������˺ţ�</td>
                                                 <td><div class="form-inline">
                                                    <input id="txtOwnBankNumber" class="form-control input-s-sm" style="height: 35px; width: 150px" runat="server" />
                                                    <asp:RequiredFieldValidator ID="yz93" runat="server" ControlToValidate="txtOwnBankNumber" ErrorMessage="����Ϊ��" ForeColor="Red" ValidationGroup="FIN"></asp:RequiredFieldValidator>
                                                    </div></td>
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
                    <div class="panel-heading" data-toggle="collapse" data-target="#approve">
                        ����ģ��</div>
                    <div class="panel-body <% =ViewState["lv"].ToString() == "approve" ? "" : "collapse" %>" id="approve">
                        <div class="col-xs-12 col-sm-12  col-md-12 col-lg-12">
                            <table style="height: 35px; width: 100%">
                                <tr>
                                    <td>һ.ǩ�˼�¼</td>
                                </tr>
                                <tr>
                                    <td>
                                                <asp:GridView ID="gv_approve" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" ShowFooter="True" OnRowCommand="gv_approve_RowCommand">
                                                    <RowStyle BackColor="#EFF3FB" />
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="ID" Visible="False">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="ID" runat="server" Enabled="False" Text='<%# DataBinder.Eval(Container, "DataItem.ID")%>' Width="20px"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="status_id" Visible="False">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="status_id" runat="server" Enabled="False" Text='<%# DataBinder.Eval(Container, "DataItem.status_id")%>' Width="20px"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="����">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="dept" runat="server" Enabled="False" Text='<%#DataBinder.Eval(Container,"DataItem.dept") %>' Width="50px"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="��������" >
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="status_ms" runat="server" Enabled="False" Text='<%# DataBinder.Eval(Container, "DataItem.status_ms")%>' Width="100px"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="��ɫ">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="Update_Engineer" runat="server" Enabled="False" Text='<%#DataBinder.Eval(Container,"DataItem.Update_Engineer") %>' Width="100px"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                           <asp:TemplateField HeaderText="����">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="Update_user" runat="server" Enabled="False" Text='<%#DataBinder.Eval(Container,"DataItem.Update_user") %>' Width="100px"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                           <asp:TemplateField HeaderText="����">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="Update_username" runat="server"  Enabled="False" Text='<%#DataBinder.Eval(Container,"DataItem.Update_username") %>' Width="70px"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                   <%--     <asp:TemplateField HeaderText="����ʱ��">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="Receive_time" runat="server" Enabled="False" ReadOnly="True" Text='<%#DataBinder.Eval(Container,"DataItem.Receive_time","{0:yyyy-MM-dd}") %>' Width="80px"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="���ʱ��">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="Commit_time" runat="server" Enabled="False" Text='<%#DataBinder.Eval(Container,"DataItem.Commit_time","{0:yyyy-MM-dd}") %>' Width="80px"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>--%>
                                                             <asp:TemplateField HeaderText="����ʱ��">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="Receive_time" runat="server" Enabled="False" ReadOnly="True" Text='<%#DataBinder.Eval(Container,"DataItem.Receive_time") %>' Width="65px"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="���ʱ��">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="Commit_time" runat="server" Enabled="False" Text='<%#DataBinder.Eval(Container,"DataItem.Commit_time") %>' Width="65px"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="�ύ˵��">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Lab_yz" runat="server" Font-Size="Larger" ForeColor="Red" Text="*" Visible="False"></asp:Label>
                                                                <asp:TextBox ID="Update_content" runat="server" Width="400px" Text='<%#DataBinder.Eval(Container,"DataItem.Update_content") %>' Enabled="False"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                              <asp:TemplateField HeaderText="���" Visible="False">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="Update_LB" runat="server" Enabled="False" Text='<%#DataBinder.Eval(Container,"DataItem.Update_LB") %>' Width="100px"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:Button ID="btncomit" runat="server" CommandArgument="<%#Container.DataItemIndex %>" CommandName="comit" ForeColor="#6600FF" Text="ȷ��" Enabled="False" ValidationGroup="request" />
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
                                    </td>
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
                        <strong>������־</strong><span class="caret"></span>
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
                                                <asp:BoundField DataField="Update_Engineer" HeaderText="������ɫ"
                                                    ReadOnly="True">
                                                    <HeaderStyle Wrap="True" />
                                                    <ItemStyle Width="80px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Update_user" HeaderText="��������"
                                                    ReadOnly="True">
                                                    <HeaderStyle Wrap="True" />
                                                    <ItemStyle Width="80px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Update_username" HeaderText="����"
                                                    ReadOnly="True">
                                                    <HeaderStyle Wrap="True" />
                                                    <ItemStyle Width="80px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Update_LB" HeaderText="�������"
                                                    ReadOnly="True">
                                                    <HeaderStyle Wrap="True" />
                                                    <ItemStyle Width="80px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Commit_time" HeaderText="����ʱ��"
                                                    ReadOnly="True">
                                                    <HeaderStyle Wrap="True" />
                                                    <ItemStyle Width="120px" />
                                                </asp:BoundField>                                              
                                                <asp:BoundField DataField="Update_content" HeaderText="��������">
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
                <strong>��־:</strong>
            </div>
            <div class="panel-body  " id="DDXX">
                <div>
                    <div class="">
                        <table border="1" width="100%">
                                                        <tr>
                                <td colspan="2" style="background-color: lightblue">
                                    <asp:Label ID="Lab_ms" runat="server" Font-Size="Small" Font-Underline="False" ForeColor="#6600CC">��ǰ״̬</asp:Label>
                                    <asp:Label ID="Lab_Status_id" runat="server" Font-Size="Small" Font-Underline="False" ForeColor="#6600CC"></asp:Label>
                                    .<asp:Label ID="Lab_Status" runat="server" Font-Size="Small" Font-Underline="False" ForeColor="#6600CC"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" style="background-color: lightblue">
                                    <asp:Label ID="Lab_htzt" runat="server" Font-Size="Large" Font-Underline="False" ForeColor="#6600CC" Visible="False"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">

                                    <asp:GridView ID="gv_rz2" Width="100%"
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
                                            <asp:BoundField DataField="Update_username" HeaderText="����">
                                                <HeaderStyle Wrap="False" />
                                                <ItemStyle Width="10%" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="status_ms" HeaderText="��������">
                                                <HeaderStyle Wrap="False" />
                                                <ItemStyle Width="10%" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Commit_time" HeaderText="����ʱ��">
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
