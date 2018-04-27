<%@ Page Title="产品系统【查询】" Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.master" CodeFile="Chanpin_Query.aspx.cs" Inherits="Product_Chanpin_Query" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <style type="text/css">
        td {
            padding-left: 5px;
            padding-right: 5px;
            
        }

        .auto-style1 {
            width: 100px;
        }

        .tblCondition td {
            white-space: nowrap;
            padding-bottom: 2px;
        }

        .GridView td,  .GridView th  {
            word-wrap: break-word;
            padding-bottom: 2px;
            /*word-break: break-all;*/
            border-right:1px solid lightgray;
                border-bottom:1px solid lightgray;
                border-left:1px solid lightgray;
                border-top:1px solid lightgray
        }
        .danger{color:red;
                border-right:1px solid lightgray;
                border-bottom:1px solid lightgray;
                border-left:1px solid lightgray;
                border-top:1px solid lightgray
                }
    </style>
    <script src="../Content/js/jquery.min.js" type="text/javascript"></script>
    <script src="../Content/js/bootstrap.min.js"></script>
    <script src="../Content/js/plugins/layer/laydate/laydate.js"></script>
    <script src="../Content/js/plugins/layer/layer.min.js"></script>
    <link href="../Content/js/plugins/bootstrap-select/css/bootstrap-select.min.css" rel="stylesheet" />
    <script src="../Content/js/plugins/bootstrap-select/js/bootstrap-select.min.js"></script>
    <style>
        .bootstrap-select > .dropdown-toggle {
            width: 150px;
        }

        .bootstrap-select:not([class*=col-]):not([class*=form-control]):not(.input-group-btn) {
            width: 120px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">

    <script type="text/javascript">
        $("#mestitle").text("【产品查询】");
        $(document).ready(function () {
            $('.selectpicker').change(function () {
                if (this.id.indexOf("Status") >= 0) {
                    $("input[id*='txt_product_status']").val($("[class='selectpicker A']").val());

                }
                else if (this.id.indexOf("Cust") >= 0) {
                    var val = $("[class='selectpicker C']").val();
                    $("input[id*='txtCustomer_name']").val($("[class='selectpicker C']").val());
                
                    $.ajaxSettings.async = false;
                    SetShipFrom(val);
                    SetShipTo(val);
                    $.ajaxSettings.async = true;
                }
                else if (this.id.indexOf("Leibie") >= 0) {
                    $("input[id*='txt_p_leibie']").val($("[class='selectpicker B']").val());
                }


            });

            $('[id*=ddl_dept]').change(function () {
                var val = $("[id*='ddl_dept']").val();

                $.ajaxSettings.async = false;
                Setddl_cpfzr(val);                    
                $.ajaxSettings.async = true;

            });

        });
        function SetShipFrom(cust) {
            $.ajax({
                type: "Post",
                url: "Chanpin_Query.aspx/ship_fromBy?cust=" + cust,
                //方法传参的写法一定要对，str为形参的名字,str2为第二个形参的名字
                data: "{'cust':'" + cust + "','year':''}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    //返回的数据用data.d获取内容// 
                   // alert(data.d);
                    var ddl = $("[id*='ddl_ship_from']");
                    ddl.empty();
                    $("<option></option>").val("-1").text("全部").appendTo(ddl);
                    $.each(eval(data.d), function (i, item) {
                        $("<option></option>").val(item["status_id"]).text(item["status"]).appendTo(ddl);
                    })
                },
                error: function (err) {
                    alert(err);
                }
            });
        }
        function SetShipTo(cust) {
            $.ajax({
                type: "Post",
                url: "Chanpin_Query.aspx/ship_toBy?cust=" + cust,
                //方法传参的写法一定要对，str为形参的名字,str2为第二个形参的名字
                data: "{'cust':'" + cust + "','year':''}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    //返回的数据用data.d获取内容// 
                   // alert(data.d);
                    var ddl = $("[id*='ddl_ship_to']");
                    ddl.empty();
                    $("<option></option>").val("-1").text("全部").appendTo(ddl);
                    $.each(eval(data.d), function (i, item) {
                        $("<option></option>").val(item["status_id"]).text(item["status"]).appendTo(ddl);
                    })
                },
                error: function (err) {
                    alert(err);
                }
            });
        }
        //产品负责人
        function Setddl_cpfzr(dept) {
            $.ajax({
                type: "Post",
                url: "Chanpin_Query.aspx/Setddl_cpfzr?dept=" + dept,
                //方法传参的写法一定要对，str为形参的名字,str2为第二个形参的名字
                data: "{'dept':'" + dept + "' }",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    //返回的数据用data.d获取内容// 
                    // alert(data.d);
                    var ddl = $("[id*='ddl_cpfzr']");
                    ddl.empty();
                    $("<option></option>").val("-1").text("全部").appendTo(ddl);
                    $.each(eval(data.d), function (i, item) {
                        $("<option></option>").val(item["status_id"]).text(item["status"]).appendTo(ddl);
                    })
                },
                error: function (err) {
                    alert(err);
                }
            });
        }
    </script>

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

    <div class="row row-container">
        <div class="col-sm-12 " >
            <div class="panel panel-info" >
                <div class="panel-heading" >
                    <strong>查询</strong>
                </div>
                <div class="panel-body">
                    <div class="col-sm-12">
                        <table class="tblCondition" style="width: 100%">
                            <tr>
                                <td>按:
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddl_type" runat="server" class="form-control input-s-sm " Width="120px">
                                        <asp:ListItem Value="1">基本信息</asp:ListItem>
                                        <asp:ListItem Value="2">数量</asp:ListItem>
                                        <asp:ListItem Value="3">收入</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td>产品状态:
                                </td>
                                <td>
                                    <asp:TextBox ID="txt_product_status" class="form-control input-s-sm" runat="server" Style="display: none; " ></asp:TextBox>
                                    <select id="selectPStatus" name="selectPStatus" class="selectpicker A" multiple data-live-search="true" runat="server" style="width: 50px">
                                    </select>
                                </td>
                                <td>项目号:
                                </td>
                                <td style="width: 100px">
                                    <asp:TextBox ID="txt_pgino" class="form-control input-s-sm" runat="server" Width="100px"></asp:TextBox>
                                </td>
                                <td>零件号:
                                </td>
                                <td class="auto-style1">
                                    <asp:TextBox ID="txt_productcode" class="form-control input-s-sm" runat="server"></asp:TextBox>
                                </td>
                                <td>产品大类:
                                </td>
                                <td>
                                    <asp:TextBox ID="txt_p_leibie" class="form-control input-s-sm" runat="server" Style="display: none"></asp:TextBox>
                                    <select id="selectPLeibie" name="selectPLeibie" class="selectpicker B" multiple data-live-search="true" runat="server" style="width: 100px">
                                    </select>
                                </td>
                                <td>项目:</td>
                                <td>
                                    <asp:TextBox ID="txt_customer_project" class="form-control input-s-sm" runat="server" Width="120px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>生产地点:</td>
                                <td>
                                    <asp:DropDownList ID="ddl_make_factory" runat="server" class="form-control input-s-md " Width="120px">
                                    </asp:DropDownList>
                                </td>
                                <td>发运地点:</td>
                                <td>

                                    <div style="float: left">
                                        <asp:DropDownList ID="ddl_ship_from" runat="server" class="form-control input-s-md " Width="120px">
                                        </asp:DropDownList>
                                    </div>
                                   
                                        

                                </td>
                                <td>发往地点:
                                </td>
                                <td>

                                    <div style="float: left">
                                        <asp:DropDownList ID="ddl_ship_to" runat="server" class="form-control input-s-md " Width="120px">
                                        </asp:DropDownList>
                                    </div>
                                    
                                       
                                </td>
                                <td>部门:
                                </td>
                                <td colspan="1">
                                    <asp:DropDownList ID="ddl_dept" runat="server" class="form-control input-s-md " Width="120px" OnSelectedIndexChanged="ddlDepart_SelectedIndexChanged" AutoPostBack="false">
                                    </asp:DropDownList>
                                </td>
                                <td>产品负责人:
                                </td>
                                <td colspan="1">
                                    <asp:DropDownList ID="ddl_cpfzr" runat="server" class="form-control input-s-md " Width="120px">
                                    </asp:DropDownList>
                                </td>
                                <td>直接顾客:
                                </td>
                                <td>
                                    <asp:TextBox ID="txtCustomer_name" class="form-control input-s-sm" runat="server" Style="display: none"></asp:TextBox>
                                    <select id="selectCust" name="selectCust" class="selectpicker C" multiple data-live-search="true" runat="server" style="width: 100px">
                                    </select>
                                </td>
                            </tr>
                            <tr>
                                <td>定点日期:</td>
                                <td>
                                    <asp:DropDownList ID="ddl_dingdian_year" runat="server" class="form-control input-s-md " Width="120px">
                                    </asp:DropDownList>
                                </td>
                                <td>批产日期:</td>
                                <td>
                                    <asp:DropDownList ID="ddl_pc_year" runat="server" class="form-control input-s-md " Width="120px">
                                    </asp:DropDownList>
                                </td>
                                <td>停产日期:</td>
                                <td>
                                    <asp:DropDownList ID="ddl_end_year" runat="server" class="form-control input-s-md " Width="120px">
                                    </asp:DropDownList>
                                </td>
                                <td>更新日期:</td>
                                <td>
                                    <asp:DropDownList ID="ddlUpdate_date_type" runat="server" class="form-control input-s-md " Width="120px">
                                        <asp:ListItem Value="A">全部</asp:ListItem>
                                        <asp:ListItem Value="D">当天</asp:ListItem>
                                        <asp:ListItem Value="W">一周内</asp:ListItem>
                                        <asp:ListItem Value="M">一月内</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td colspan="10" style="text-align: center; padding-top: 10px; padding-right: 50px">
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional" RenderMode="Block">
                                        <ContentTemplate>
                                             <div style="width: 100%; text-align: center" >
                                                <asp:Button ID="Button1" runat="server" Text="查询" class="btn btn-large btn-primary" OnClick="Button1_Click" Width="100px" />
                                            </div></ContentTemplate>
                                    </asp:UpdatePanel>
                                       
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Always">
        <ContentTemplate>
           
            <div runat="server" id="DIV1" style="margin-left: 5px; margin-right: 5px; margin-bottom: 10px">

                <table>
                    <tr>
                        <td valign="top">
                            <asp:GridView ID="GridView1" runat="server"  AllowPaging="True" CssClass="GridView"
                                AutoGenerateColumns="true" BorderColor="silver" HeaderStyle-BackColor="LightBlue" OnRowDataBound="gv1_RowDataBound" OnPageIndexChanging="GridView1_PageIndexChanging" AllowSorting="true" Width="1900px" OnSorting="GridView1_Sorting" OnRowCreated="GridView1_RowCreated" ShowFooter="True">
                                <EmptyDataTemplate>no data found</EmptyDataTemplate>
                                <PagerSettings FirstPageText="首页" LastPageText="尾页" Mode="NextPreviousFirstLast" NextPageText="下页" PreviousPageText="上页" />
                                <PagerStyle ForeColor="lightblue" />
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td valign="top">
                            <asp:GridView ID="GridView2" runat="server"  AllowPaging="True" CssClass="GridView"
                                AutoGenerateColumns="true" BorderColor="silver" HeaderStyle-BackColor="LightBlue" OnRowDataBound="gv2_RowDataBound" OnPageIndexChanging="GridView2_PageIndexChanging" ShowFooter="true" AllowSorting="true" OnSorting="GridView2_Sorting" Width="2200px" OnRowCreated="GridView2_RowCreated">
                                <EmptyDataTemplate>no data found</EmptyDataTemplate>
                                <PagerSettings FirstPageText="首页" LastPageText="尾页" Mode="NextPreviousFirstLast" NextPageText="下页" PreviousPageText="上页" />
                                <PagerStyle ForeColor="lightblue" />
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td valign="top">
                            <asp:GridView ID="GridView3" runat="server"  AllowPaging="True" CssClass="GridView"
                                AutoGenerateColumns="true" BorderColor="silver" HeaderStyle-BackColor="LightBlue" OnRowDataBound="gv3_RowDataBound" OnPageIndexChanging="GridView3_PageIndexChanging" ShowFooter="true" AllowSorting="true" OnSorting="GridView3_Sorting" OnRowCreated="GridView3_RowCreated" Width="2200px">
                                <EmptyDataTemplate>no data found</EmptyDataTemplate>
                                <PagerSettings FirstPageText="首页" LastPageText="尾页" Mode="NextPreviousFirstLast" NextPageText="下页" PreviousPageText="上页" />
                                <PagerStyle ForeColor="lightblue" />
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel> 
</asp:Content>



