<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Bmw_M1452_Test1.aspx.cs" Inherits="Production_Bmw_M1452_Test1"  EnableEventValidation="true"  %>
<%@ Register assembly="DevExpress.Web.v17.2, Version=17.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web" tagprefix="dx" %>
<%--<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>--%>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
     <script src="../Content/js/jquery.min.js" type="text/javascript"></script>
    <script type="text/javascript" src="../Content/js/jquery.cookie.min.js"></script>
    <script src="../Content/js/bootstrap.min.js" type="text/javascript"></script>
    <script src="../Content/js/plugins/layer/layer.min.js" type="text/javascript"></script>

    <script type="text/javascript" language="javascript">

        $("#mestitle").text("【BMW/装配测漏/1#测漏-85/81记录】");
    </script>
    <style type="text/css">
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
            text-align: left; /*text-shadow: 2px 2px 2px rgb(88, 126, 156);*/
        }

        fieldset {
            padding: .35em .625em .75em;
            margin: 0 2px;
            border: 1px solid lightblue;
        }

        legend {
            border-style: none;
            border-color: inherit;
            border-width: 0;
            padding: 5px;
            width: 108px;
            margin-bottom: 2px;
        }

        .panel {
            margin-bottom: 3px;
        }

        .panel-heading {
            padding: 2px 5px 2px 5px;
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
            margin-top: 2px;
        }
    </style>




    <script type="text/javascript">
           
            var prevselitem = null;
            function selectx(row) {
                if (prevselitem != null) {
                    prevselitem.style.backgroundColor = '#ffffff';
                }
                row.style.backgroundColor = 'PeachPuff';
                prevselitem = row;

            }
          
           


 </script>
    


   
      
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    
    <div class="col-md-12" >
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
                            <td style="width:80px;">日期</td>
                            <td style="width:110px;">
                                <dx:ASPxDateEdit ID="txtdate1" runat="server"  class="form-control input-s-sm" Width="100"></dx:ASPxDateEdit>
                              </td>
                            <td style="width:110px;"> <dx:ASPxDateEdit ID="txtdate2" runat="server"  class="form-control input-s-sm" Width="100"></dx:ASPxDateEdit></td>
                            <td>
                                &nbsp;</td>
                            <td>  &nbsp;&nbsp;<asp:Button ID="btnsearch" runat="server" Text="查询" class="btn btn-large btn-primary" Width="70px"  Height="32px" Font-Size="12px" />
                                  &nbsp;&nbsp;&nbsp;
                                   <%--容器1开始--%>&nbsp;<asp:Button ID="btnimport" runat="server" Text="导出Excel"  class="btn btn-primary" Font-Size="12px" OnClick="btnimport_Click" />
                                <%--容器1结束--%>&nbsp;&nbsp;
                                   <%--容器1开始--%>

                            </td>
                        </tr>

                                <%--容器1结束--%>
                      
                                </table>
                           </div>
                    </div>
                </div>
            </div>
        </div>
        </div>
     <%--容器1开始--%><%--容器1结束--%>
     <asp:UpdatePanel runat="server" ID="p1" UpdateMode="Conditional">
                                    <ContentTemplate>
                                       
       <%--容器1开始--%>
       <div class="row  row-container">
            <div class="col-md-12">
                <div class="panel panel-info">
                    <div class="panel-heading" >
                        <table style="width:100%;">
                            <tr>
                                <td><strong   data-toggle="collapse" data-target="#gscs">记录清单</strong></td>
                                <td style=" text-align:right;"></td>
                            </tr>
                        </table>
                       
                       
		
	
                         
                    </div>
                    <div class="panel-body  collapse in" id="gscs" >                           
                         <div runat="server" id="DIV1" style="margin-left: 5px; margin-right: 5px; margin-bottom: 10px; ">
            <table>
                <tr>
                    <td valign="top"  >
                         <dx:aspxgridview ID="gv1" runat="server" EnableCallBacks="false"   KeyFieldName="id"     Theme="MetropolisBlue" OnHtmlDataCellPrepared="gv1_HtmlDataCellPrepared" 
                               >

                                   <SettingsPager PageSize="1000" >

                             </SettingsPager>
                          
                           
                                   <Settings ShowFilterRow="True" ShowGroupPanel="True" ShowFilterRowMenu="True" ShowFilterRowMenuLikeItem="True" />
                                   <SettingsBehavior AllowFocusedRow="True" />
                                   <SettingsDataSecurity AllowDelete="False" AllowEdit="False" AllowInsert="False" />
                                   <Columns>
                                      
                                   </Columns>
                          
                           
                                   <Styles>
                                       <Header BackColor="#1E82CD" ForeColor="White" Font-Size="12px">
                                       </Header>
                                       <DetailRow Font-Size="12px">
                                       </DetailRow>
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
            <%--容器1结束--%>

                                                   
   </ContentTemplate>
         
        <Triggers>
            <asp:PostBackTrigger ControlID="btnimport"  />
        </Triggers>
     </asp:UpdatePanel>
    
                  <dx:ASPxGridViewExporter ID="gve1" runat="server" GridViewID="gv1" ExportEmptyDetailGrid="True">
                            </dx:ASPxGridViewExporter>                        
                                     
</asp:Content>
                                     