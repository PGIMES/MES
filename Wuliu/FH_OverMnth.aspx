<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="FH_OverMnth.aspx.cs" Inherits="Wuliu_FH_OverMnth" %>

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

        .GridView1 td {
            word-wrap: break-word;
            padding-bottom: 2px;
            /*word-break: break-all;*/
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
        $("#mestitle").text("【超一个月无交货报表】");
       
            
    </script>

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

    <div class="row row-container">
        <div class="col-sm-12 ">
            <div class="panel panel-info">
                <div class="panel-heading">
                    <strong>查询</strong>
                </div>
                <div class="panel-body">
                    <div class="col-sm-12">
                        <table class="tblCondition" style="width: 100%">
                            <tr>
                                <td>公司别:</td>
                                <td>
                                    <asp:DropDownList ID="ddl_comp" runat="server" class="form-control input-s-md " Width="120px">
                                        <asp:ListItem>200</asp:ListItem>
                                        <asp:ListItem>100</asp:ListItem>
                                        <asp:ListItem>ALL</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td>物料号:</td>
                                <td>
                                    <asp:TextBox ID="txt_part" class="form-control input-s-sm" runat="server" Width="100px"></asp:TextBox>
                                </td>
                                <td>描述:</td>
                                <td>
                                    <asp:TextBox ID="txt_desc" class="form-control input-s-sm" 
                                        runat="server" Width="100px"></asp:TextBox>
                                </td>
                                <td>Ship_From:</td>
                                <td>
                                    <asp:DropDownList ID="txt_shipfrom" runat="server" class="form-control input-s-md " Width="120px">
                                    </asp:DropDownList>
                                    
                                </td>
                                <td>Ship_to:</td>
                                <td>
                                    <asp:TextBox ID="txt_shipto" class="form-control input-s-sm" 
                                        runat="server" Width="100px"></asp:TextBox>
                                </td>
                                <td style="text-align: center; padding-top: 10px; padding-right: 50px">
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional" RenderMode="Block">
                                        <ContentTemplate>
                                            <div style="width: 100%; text-align: center">
                                                <asp:Button ID="Button1" runat="server" Text="查询" class="btn btn-large btn-primary" OnClick="Button1_Click" Width="100px" /> 
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>

                                <td style="text-align: center; padding-top: 10px; padding-right: 50px">
                                    
                                            <div style="width: 100%; text-align: center">
                                                <asp:Button ID="Button2" runat="server" Text="导出" class="btn btn-large btn-primary" OnClick="Button2_Click" Width="100px"  />
                                            </div>
                                        
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
                            <asp:GridView ID="GridView1" runat="server" 
                                CssClass="GridView1" 
                                AutoGenerateColumns="true" BorderColor="LightGray" 
                                HeaderStyle-BackColor="LightBlue" 
                                OnRowDataBound="GridView1_RowDataBound" 
                                OnPageIndexChanging="GridView1_PageIndexChanging" 
                                Width="1300px" 
                                OnSorting="GridView1_Sorting" 
                                PageSize="100" AllowSorting="True">
                                <EmptyDataTemplate>no data found</EmptyDataTemplate>
                                <HeaderStyle BackColor="LightBlue" />
                                <PagerSettings FirstPageText="首页" LastPageText="尾页" Mode="NextPreviousFirstLast" NextPageText="下页" PreviousPageText="上页" />
                                <PagerStyle />
                            </asp:GridView>
                        </td>
                    </tr>
                    
                    <tr>
                        <td valign="top">
                            &nbsp;</td>
                    </tr>
                </table>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>



