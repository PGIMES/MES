<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Emp_LoginLog_Query.aspx.cs" Inherits="Production_Emp_LoginLog_Query" %>

<%@ Register Assembly="DevExpress.Web.v17.2, Version=17.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
     <script src="../Content/js/jquery.min.js" type="text/javascript"></script>
     <script type="text/javascript">
        $("#mestitle").text("【BMW/<%=this.location%>/<%=this.m_slocation%>/登入查询】");
    </script>

    
    <div class="row  row-container">
        <div class="col-md-12">
            <div class="panel panel-info">
                <div class="panel-heading" data-toggle="collapse" data-target="#CPXX">
                    <strong>查询条件</strong>
                </div>
                <div class="panel-body  collapse in" id="CPXX" >
                    <div class="col-xs-12 col-sm-12  col-md-12 col-lg-12" style="font-size:13px;;">
                        <table class="tblCondition" style=" border-collapse: collapse;">
                            <tr>
                                <td style="width:70px;">登入日期</td>
                                <td style="width:110px;">
                                    <dx:ASPxDateEdit ID="txtdate1" runat="server"  class="form-control input-s-sm" Width="100px"></dx:ASPxDateEdit>
                                </td>
                                <td style="width:110px;">
                                    <dx:ASPxDateEdit ID="txtdate2" runat="server"  class="form-control input-s-sm" Width="100px"></dx:ASPxDateEdit>
                                </td>
                                <td style="width:40px;">工位</td>
                                <td style="width:110px;">
                                    <dx:ASPxTextBox ID="txt_order_id" runat="server" Width="100px"></dx:ASPxTextBox>
                                </td>
                                <td style="width:40px;">工号</td>
                                <td style="width:110px;">
                                    <dx:ASPxTextBox ID="txt_emp" runat="server" Width="100px"></dx:ASPxTextBox>
                                </td>
                                <td>  &nbsp;&nbsp;
                                    <asp:Button ID="btn_Search" runat="server" Text="查询" class="btn btn-large btn-primary" Width="70px"  Height="32px" Font-Size="12px" OnClick="btn_Search_Click" />
                                        &nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:Button ID="btn_Export" runat="server" Text="导出Excel"  class="btn btn-primary" Font-Size="12px" OnClick="btn_Export_Click" />
                                        &nbsp;&nbsp;
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
                <div class="panel-heading" >
                    <table style="width:100%;">
                        <tr>
                            <td><strong data-toggle="collapse" data-target="#gscs">登入清单</strong></td>
                            <td style=" text-align:right;"></td>
                        </tr>
                    </table>
                </div>
                <div class="panel-body  collapse in" id="gscs" >                           
                    <div runat="server" id="DIV1" style="margin-left: 5px; margin-right: 5px; margin-bottom: 10px; ">
                        <table>
                            <tr>
                                <td valign="top"  >
                                    <dx:aspxgridview ID="gv1"  runat="server" EnableCallBacks="true"   KeyFieldName="id"  Theme="MetropolisBlue"
                                         OnHtmlDataCellPrepared="gv1_HtmlDataCellPrepared" OnPageIndexChanged="gv1_PageIndexChanged"> <%--OnHtmlRowCreated="gv1_HtmlRowCreated"--%>
                                    <SettingsPager PageSize="1000" ></SettingsPager>
                                    <Settings ShowFilterRow="True" ShowGroupPanel="false" ShowFilterRowMenu="True" ShowFilterRowMenuLikeItem="True" AutoFilterCondition="Contains"  />
                                    <SettingsBehavior AllowFocusedRow="True" ColumnResizeMode="Control" />
                                    <Columns></Columns>
                                    <Styles>
                                        <Header BackColor="#1E82CD" ForeColor="White" Font-Size="12px">
                                        </Header>
                                        <DetailRow Font-Size="12px"></DetailRow>
                                    </Styles>
                                    </dx:aspxgridview>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
        </div>
    </div>
    
    <dx:ASPxGridViewExporter ID="gve1" runat="server" GridViewID="gv1" ExportEmptyDetailGrid="True">
    </dx:ASPxGridViewExporter>  

</asp:Content>

