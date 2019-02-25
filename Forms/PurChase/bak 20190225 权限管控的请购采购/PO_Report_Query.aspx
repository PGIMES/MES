<%@ Page Title="采购单查询" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="PO_Report_Query.aspx.cs" Inherits="Forms_PurChase_PO_Report_Query" %>

<%@ Register Assembly="DevExpress.Web.v17.2, Version=17.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>





<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <style type="text/css">
        /*td {
            padding-left: 5px;
            padding-right: 5px;
        }*/
        .tblCondition td{ white-space:nowrap }
        /*.dxgvHeader td {
            white-space:normal; 
        }*/
        .dx-wrap{
     white-space:  inherit; 
    line-height: normal;
    padding: 0;
}
        /*自动隐藏文字*/=====1行
.public-ellipsis-1 {
    overflow: hidden;
    white-space: nowrap;
    text-overflow: ellipsis;
}
/*自动隐藏文字*/=====2行
.public-ellipsis-2 {
    display: -webkit-box !important;
    overflow: hidden !important;
    text-overflow: ellipsis !important;
    word-wrap: break-word !important;
    white-space: normal !important;
    -webkit-line-clamp: 2 !important;
    -webkit-box-orient: vertical !important;
}
/*自动隐藏文字*/=====3行
.public-ellipsis-3 {
    display: -webkit-box !important;
    overflow: hidden !important;
    text-overflow: ellipsis !important;
    word-wrap: break-word !important;
    white-space: normal !important;
    -webkit-line-clamp: 3 !important;
    -webkit-box-orient: vertical !important;

}
          border{border:solid 1px red}       

             .hidden { display:none;}
		     .hidden1
            {
            	border:0px; 
            	overflow:hidden
            	
            	}
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">

    <script src="../../Content/js/jquery.min.js"  type="text/javascript"></script>
    <script src="../../Content/js/bootstrap.min.js"></script>
    <script src="../../Content/js/plugins/layer/layer.min.js" type="text/javascript"></script>
    <script src="../../Content/js/plugins/layer/laydate/laydate.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#mestitle").text("【采购单查询】");
            setHeight();
            $(window).resize(function () {
                setHeight();
            });

        });


        function setHeight() {
            $("div[class=dxgvCSD]").css("height", ($(window).height() - $("#div_p").height() - 300) + "px");
        }
        	
    </script>
            <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
     
<%--     <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>--%>
                        <div class="panel-body" id="div_p">
                            <div class="col-sm-12">

                                <table>

                                    <tr>
                                        <td>采购类别:
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="drop_type" runat="server" class="form-control input-s-sm ">
                                                <asp:ListItem>存货</asp:ListItem>
                                                <asp:ListItem>设备</asp:ListItem>
                                                <asp:ListItem>设施</asp:ListItem>
                                                <asp:ListItem>工夹模具</asp:ListItem>
                                                <asp:ListItem>IT硬件/软件</asp:ListItem>
                                                <asp:ListItem>服务及其他</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        

                                      

                                        <td>创建日期;</td>
                                        <td >
                                   <asp:TextBox ID="txtDateFrom" class="form-control" onclick="laydate()" runat="server" Width="100px"></asp:TextBox>
                                </td>
                                <td>~</td>
                                <td >
                                   <asp:TextBox ID="txtDateTo" class="form-control" onclick="laydate()" runat="server" Width="100px"></asp:TextBox>
                                </td>
                                      
                                       
                                        <td> 
                                            &nbsp;<td>
                                    <asp:Button ID="Bt_select" runat="server" Text="查询" class="btn btn-large btn-primary" OnClick="Button1_Click" Width="100px" />   
                                               
                                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                <asp:Button ID="Bt_Export" runat="server" 
                                                    class="btn btn-large btn-primary" OnClick="Bt_Export_Click" 
                                                    Text="导出" Width="100px" />
                                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;   
                                               
                                            </td>
                                    </tr>

                                     

                                    


                                </table>
                            </div>

                        </div>
                   <%-- </ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger ControlID="Bt_select" />

                    </Triggers>
                </asp:UpdatePanel>--%>

    <div runat="server" id="DIV1" style="margin-left: 5px; margin-right: 5px; margin-bottom: 10px">
          <table>
            <tr>
                <td>

                 

                     <dx:ASPxGridView ID="GV_PART" runat="server" 
                         KeyFieldName="PRNo" AutoGenerateColumns="False"  
                         
                         OnHtmlRowPrepared="GV_PART_HtmlRowPrepared" 
                         OnHtmlRowCreated="GV_PART_HtmlRowCreated" 
                      
                         onhtmldatacellprepared="GV_PART_HtmlDataCellPrepared" 
                         onpageindexchanged="GV_PART_PageIndexChanged">
                          <ClientSideEvents EndCallback="function(s, e) {           
                            setHeight();
        	                    }" />
                         <SettingsBehavior AllowDragDrop="TRUE" AllowFocusedRow="True" AllowSelectByRowClick="True" ColumnResizeMode="Control" />
              <SettingsPager PageSize="1000">
                     
                </SettingsPager>
                  <Styles>
            <AlternatingRow Enabled="true" />
           
        </Styles>
                  <Settings ShowFilterRow="True" ShowFilterRowMenu="True"   VerticalScrollBarMode="Visible" VerticalScrollBarStyle="Standard" VerticalScrollableHeight="500"
                             ShowFilterRowMenuLikeItem="True"   ShowGroupPanel="True" 
                             ShowFooter="True"/>
            <SettingsSearchPanel Visible="True" />

<Settings ShowFilterRow="True" ShowFilterRowMenu="True" 
                             ShowFilterRowMenuLikeItem="True" ShowGroupPanel="True" 
                             ShowFooter="True" showgroupedcolumns="True"></Settings>

<SettingsBehavior ColumnResizeMode="Control" AllowFocusedRow="True" 
                             AllowSelectByRowClick="True" autoexpandallgroups="True" 
                             mergegroupsmode="Always" sortmode="Value"></SettingsBehavior>

<SettingsSearchPanel Visible="True"></SettingsSearchPanel>

            <SettingsFilterControl AllowHierarchicalColumns="True">
            </SettingsFilterControl>
            <Columns>
               
            </Columns>
                              <TotalSummary>
                                  <dx:ASPxSummaryItem DisplayFormat="{0:N4}" FieldName="TotalPrice" 
                                      SummaryType="Sum" />
                                       
                         </TotalSummary>
                              <Styles>
                <Header BackColor="#99CCFF">
                </Header>
                                  <FocusedRow BackColor="#99CCFF" ForeColor="#0000CC">
                                  </FocusedRow>
                                  <Footer HorizontalAlign="Right">
                                  </Footer>
            </Styles>
        </dx:ASPxGridView>
                </td>
            </tr>
                <tr>
                <td>
                     <dx:ASPxGridViewExporter ID="ASPxGridViewExporter1" 
                        runat="server">
                    </dx:ASPxGridViewExporter>
                    </td>
            </tr>
               <tr>
                <td>
                    &nbsp;</td>
            </tr>
        </table>
    
    </div>
</asp:Content>


