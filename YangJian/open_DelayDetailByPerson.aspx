<%@ Page Title="MES【人员超时及未完成明细】" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="open_DelayDetailByPerson.aspx.cs" Inherits="YangJian_open_DelayDetailByPerson" EnableViewState="True"
    MaintainScrollPositionOnPostback="true" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <style type="text/css">
        td
        {
            padding-left: 5px;
            padding-right: 5px;
            padding-top: 5px;
        }
        th
        {
            text-align: center;
            padding-left: 5px;
            padding-right: 5px;
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
        <%--人员未完成及完成明细--%>
    
    <div class=" panel panel-info  col-lg-12 ">
        <div class="panel panel-heading">
         <asp:Label ID="lblName" runat="server" Text="" ForeColor="Orange" Font-Size="17px" Font-Bold="true"></asp:Label>   <asp:Label ID="Label1" runat="server" Text=" "></asp:Label>未完成(未逾时,逾时)+已完成(逾时)明细
        </div>
        <div class="panel panel-body">        
            <asp:GridView ID="gvDetail" runat="server" BorderColor="lightgray" OnRowDataBound="gvDetail_RowDataBound" >
                <EmptyDataTemplate>暂无数据</EmptyDataTemplate>
            </asp:GridView>
        </div>
    </div>
    
</asp:Content>
