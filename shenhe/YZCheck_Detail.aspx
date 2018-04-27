<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="YZCheck_Detail.aspx.cs" Inherits="shenhe_YZCheck_Detail" EnableViewState="True"  EnableEventValidation="false"
    MaintainScrollPositionOnPostback="true" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <style type="text/css">
        td
        {
            padding-left: 2px;
            padding-right: 2px;
            padding-top: 2px;
        }
        th
        {
            text-align: center;
            padding-left: 2px;
            padding-right: 2px;
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
            margin-top: 0px;
        }
    </style>
    <script src="../Content/js/jquery.min.js" type="text/javascript"></script>

    <script type="text/javascript">
        $(document).ready(function () {
            $("#headTitle").remove();

        })//endready     
        
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">   
         
 <asp:ScriptManager ID="ScriptManager1" runat="server">
                    </asp:ScriptManager>

    <div class=" panel panel-info  col-lg-12 ">
        <div class="panel panel-heading">
         <strong>点检内容</strong>   
        </div>
        <div class="panel panel-body">   
          <asp:UpdatePanel ID="UpdatePanel_request" runat="server">
                                    <ContentTemplate> 
          <asp:GridView ID="gvDetail" runat="server" 
                BorderColor="LightGray" AutoGenerateColumns="False"  >
                <Columns>
                    <asp:BoundField DataField="question_neirong" 
                        HeaderText="检查项" />
                    <asp:TemplateField HeaderText="OK">
                        <ItemTemplate>
                            <asp:CheckBox ID="ck_OK" runat="server" 
                                oncheckedchanged="ck_OK_CheckedChanged" 
                                AutoPostBack="True" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="NG">
                        <ItemTemplate>
                            <asp:CheckBox ID="ck_NG" runat="server" 
                                oncheckedchanged="ck_NG_CheckedChanged" 
                                AutoPostBack="True" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="NA">
                        <ItemTemplate>
                            <asp:CheckBox ID="ck_NA" runat="server" AutoPostBack="True" 
                                oncheckedchanged="ck_NA_CheckedChanged" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <EmptyDataTemplate>暂无数据</EmptyDataTemplate>
            </asp:GridView>
            </ContentTemplate>
            </asp:UpdatePanel> 
        </div>
    </div>


<div class=" panel panel-info  col-lg-12 ">
        <div class="panel panel-heading">
         <strong>处理方法</strong>   
        </div>
        <div class="panel panel-body">        
            <div id="DIV1" runat="server" >
                <asp:Panel ID="Panel2" runat="server" >
                   
                                <div id="Div2">
                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                        <ContentTemplate>
                                            <asp:GridView ID="gv_solve" runat="server" AutoGenerateColumns="False"
                                                ShowHeader="False"><RowStyle BackColor="#EFF3FB" />
                                                <Columns>
                                                    <asp:BoundField DataField="solve_neirong" 
                                                        HeaderText="检查和处理方法" />
                                                </Columns>
                                            </asp:GridView>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                           
                    </asp:Panel>
                    <div>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                        <ContentTemplate>
                        <asp:GridView ID="gv_pic" runat="server" 
        AutoGenerateColumns="False" ShowHeader="False" >
        <Columns>
            <asp:TemplateField>
                <ItemTemplate>
                   <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl='<%#Eval("pic_filepath") %>'  />
                    
                    
                </ItemTemplate>
            </asp:TemplateField>
         
        </Columns>
    </asp:GridView></ContentTemplate></asp:UpdatePanel></div>
            </div>
            
        </div>
   
    </div>
        
 
</asp:Content>
