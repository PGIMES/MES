<%@ Page Title="工艺工时查询" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="GYGS_Report_Query.aspx.cs" Inherits="Forms_PgiOp_GYGS_Report_Query" %>

<%@ Register Assembly="DevExpress.Web.v17.2, Version=17.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <script type="text/javascript" src="/Content/js/jquery.min.js"></script>
    <script type="text/javascript" src="/Content/js/jquery.cookie.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#mestitle").text("【工艺工时查询】");
            getUser();
            $("#MainContent_txt_dept").change(getUser);
            $("#MainContent_txt_product_user").change(function () {
                $.cookie('user', $("#MainContent_txt_product_user").val());
            });

            $('#btn_add').click(function () {
                window.open('/Platform/WorkFlowRun/Default.aspx?flowid=a7ec8bec-1f81-4a81-81d2-a9c7385dedb7&appid=13093704-4425-4713-B3E1-81851C6F96CD')
            });

            $('#btn_edit').click(function () {

            });           
        });

        function getUser() {
            $.ajax({
                type: "post",
                url: "GYGS_Report_Query.aspx/Getuser",
                data: "{'lsdept':'" + $("#MainContent_txt_dept").val() + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    if (data.d != "") //返回的数据用data.d获取内容Logaction + "成功."
                    {
                        $('#MainContent_txt_product_user').html("");
                        $('#MainContent_txt_product_user').append("<option value=''>ALL</option>");
                        $.each(eval(data.d), function (i) {
                            // alert(eval(data.d)[i].value);
                            if ($.cookie("user") == eval(data.d)[i].value) {
                                $('#MainContent_txt_product_user').append("<option value=" + eval(data.d)[i].value + " selected>" + eval(data.d)[i].value + "</option>")

                            } else {
                                $('#MainContent_txt_product_user').append("<option value=" + eval(data.d)[i].value + ">" + eval(data.d)[i].value + "</option>");
                            }
                        });
                    }
                    else {
                        alert("失败.");
                    }
                }
            });
        }

    </script>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

    <div class="col-md-12" >
        <div class="row  row-container">            
            <div class="panel panel-info">
                <div class="panel-heading" data-toggle="collapse" data-target="#CPXX">
                    <strong>工艺工时查询</strong>
                </div>
                <div class="panel-body collapse in" id="CPXX" >
                    <div class="col-xs-12 col-sm-12  col-md-12 col-lg-12" style="font-size:13px;">
                        <table class="tblCondition" style=" border-collapse: collapse;">
                            <tr>
                                <td style="width:70px;">PGI项目号</td>
                                <td style="width:130px;">
                                    <asp:TextBox ID="txt_pgi_no" class="form-control input-s-sm" runat="server" Width="120px"></asp:TextBox>
                                </td>
                                <td style="width:70px;">客户零件号</td>
                                <td style="width:130px;">
                                    <asp:TextBox ID="txt_pn" class="form-control input-s-sm" runat="server" Width="120px"></asp:TextBox>
                                </td>                   
                                <%--<td style="width:70px;">工程部门</td>
                                <td style="width:130px;">
                                    <asp:DropDownList ID="txt_dept" runat="server" class="form-control input-s-md " Width="120px" >
                                        <asp:ListItem Value="" Selected="True">ALL</asp:ListItem>
                                        <asp:ListItem Value="工程一部">工程一部</asp:ListItem>
                                        <asp:ListItem Value="工程二部">工程二部</asp:ListItem>
                                        <asp:ListItem Value="产品一组">&nbsp;&nbsp;&nbsp;&nbsp;产品一组</asp:ListItem>
                                        <asp:ListItem Value="产品三组">&nbsp;&nbsp;&nbsp;&nbsp;产品三组</asp:ListItem>
                                        <asp:ListItem Value="调试组">&nbsp;&nbsp;&nbsp;&nbsp;调试组</asp:ListItem>
                                        <asp:ListItem Value="工程三部">工程三部</asp:ListItem>
                                        <asp:ListItem Value="产品二组">&nbsp;&nbsp;&nbsp;&nbsp;产品二组</asp:ListItem>
                                        <asp:ListItem Value="产品四组">&nbsp;&nbsp;&nbsp;&nbsp;产品四组</asp:ListItem>
                                    </asp:DropDownList>
                                </td>        
                                <td style="width:70px;">产品工程师</td>
                                <td style="width:130px;">
                                    <asp:DropDownList ID="txt_product_user" runat="server" class="form-control input-s-md " Width="120px"></asp:DropDownList>
                                </td>--%>
                                <td style="width:70px;">当前版本</td>
                                <td style="width:130px;"> 
                                    <asp:DropDownList ID="txt_ver" runat="server" class="form-control input-s-md " Width="120px">
                                        <asp:ListItem Value="">ALL</asp:ListItem>
                                        <asp:ListItem Value="当前" Selected="True">当前</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td style="width:70px;">工艺段</td>
                                <td style="width:130px;"> 
                                    <asp:DropDownList ID="DropDownList1" runat="server" class="form-control input-s-md " Width="120px">
                                        <asp:ListItem Value="">ALL</asp:ListItem>
                                        <asp:ListItem Value="压铸">压铸</asp:ListItem>
                                        <asp:ListItem Value="机加">机加</asp:ListItem>
                                        <asp:ListItem Value="质量">质量</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td>  
                                    &nbsp;&nbsp;
                                    <button id="btn_search" type="button" class="btn btn-primary btn-large" runat="server" onserverclick="btn_search_Click"><i class="fa fa-search fa-fw"></i>&nbsp;查询</button>    
                                    <button id="btn_add" type="button" class="btn btn-primary btn-large"><i class="fa fa-plus fa-fw"></i>&nbsp;新增</button>  
                                    <button id="btn_edit" type="button" class="btn btn-primary btn-large"><i class="fa fa-pencil-square-o fa-fw"></i>&nbsp;编辑</button> 
                                    <button id="btn_import" type="button" class="btn btn-primary btn-large" runat="server" onserverclick="btn_import_Click"><i class="fa fa-download fa-fw"></i>&nbsp;导出</button>
                                </td>
                            </tr>                      
                        </table>
                    </div>
                </div>
            </div>            
        </div>
    </div>

    <div class="col-sm-12">
        <table>
            <tr>
                <td><%--Width="1750px" OnPageIndexChanged="gv_PageIndexChanged" OnHtmlDataCellPrepared="gv_HtmlDataCellPrepared"--%>
                    <dx:ASPxGridView ID="gv" runat="server" KeyFieldName="" AutoGenerateColumns="False" >
                        <SettingsPager PageSize="1000" ></SettingsPager>
                        <Settings ShowFilterRow="True" ShowGroupPanel="false" ShowFilterRowMenu="True" ShowFilterRowMenuLikeItem="True" AutoFilterCondition="Contains"  />
                        <SettingsBehavior AllowFocusedRow="True" ColumnResizeMode="Control" />
                        <Columns></Columns>
                        <Styles>
                            <Header BackColor="#99CCFF"></Header>
                            <FocusedRow BackColor="#99CCFF" ForeColor="#0000CC"></FocusedRow>
                            <Footer HorizontalAlign="Right"></Footer>
                        </Styles>
                    </dx:ASPxGridView>
                    <dx:ASPxGridViewExporter ID="ASPxGridViewExporter1" runat="server">
                    </dx:ASPxGridViewExporter>
                </td>
            </tr>
        </table>
    </div>
    


</asp:Content>

