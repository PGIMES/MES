<%@ Page Language="C#" AutoEventWireup="true" CodeFile="select_Pr.aspx.cs" Inherits="Select_select_Pr" %>

<%@ Register Assembly="DevExpress.Web.v17.2, Version=17.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
        <link href="../../Content/css/bootstrap.min.css" rel="stylesheet" />
     <script src="../Content/js/jquery.min.js" type="text/javascript"></script>
    <script src="../Content/js/bootstrap.min.js"></script>
    <script src="../Content/js/plugins/layer/layer.js" type="text/javascript"></script>
     <script src="../Content/js/plugins/layer/layer.js" type="text/javascript"></script>
    
</head>
<body>
    <style type="text/css">
          .hidden { display:none;}
		     .hidden1
            {
            	border:0px; 
            	overflow:hidden
            	
            	}

    </style>
    <form id="form1" runat="server">
    <div>
     <div class="panel-body">
                    <div class="col-sm-12">

                         <div runat="server" id="DIV1" style="margin-left: 5px; margin-right: 5px; margin-bottom: 10px">
        <table>
            <tr>
                <td style="padding:0px 5px 5px 0px;">
                    <asp:Button ID="btnselect" runat="server" Text="选择" class="btn btn-large btn-default" Width="100px" OnClick="btnselect_Click"  />
                </td>
            </tr>
            <tr>
                <td>
                    <dx:ASPxGridView ID="gv" runat="server"  Width="1000px" ClientInstanceName="grid"  KeyFieldName="id"  EnableTheming="True" >
                         <SettingsBehavior AllowDragDrop="TRUE" AllowFocusedRow="false" AllowSelectByRowClick="false" AllowEllipsisInText="true" />
              <SettingsPager PageSize="1000">
                     
                </SettingsPager>
                  <Settings ShowFilterRow="True" ShowFilterRowMenu="True" ShowFilterRowMenuLikeItem="True"  ShowFooter="True"
                      VerticalScrollBarMode="Visible" VerticalScrollBarStyle="Standard" VerticalScrollableHeight="260"/>
                         <SettingsCommandButton>
                             <SelectButton ButtonType="Button" RenderMode="Button">
                             </SelectButton>
                         </SettingsCommandButton>
            <SettingsSearchPanel Visible="True" />
            <SettingsFilterControl AllowHierarchicalColumns="True">
            </SettingsFilterControl>
            
                            
                              <Styles>
                                <%--<Header BackColor="#1E82CD" ForeColor="White" ></Header>--%>
                                  <%--<Footer HorizontalAlign="Right"></Footer>--%>

                                  <Header BackColor="#31708f" Font-Bold="True" ForeColor="white" Border-BorderStyle="None" HorizontalAlign="Left" VerticalAlign="Top"></Header>
                                  <Footer HorizontalAlign="Right" BackColor="#cfcfcf" Font-Bold="True" ForeColor="red" Font-Size="11pt"></Footer>
                                <Cell Border-BorderColor="#DCDCDC" BorderLeft-BorderWidth="0"  BorderRight-BorderWidth="0" BorderTop-BorderWidth="0"></Cell>
                                <CommandColumn Border-BorderColor="#DCDCDC" BorderRight-BorderStyle="None" HorizontalAlign="Left"></CommandColumn>
                            </Styles>

                        <Columns>
           <%-- <dx:GridViewDataTextColumn FieldName="SelectAll" VisibleIndex="0" Caption="选择" >
                     <Settings AllowCellMerge="False" />
                    <DataItemTemplate>                
                        <asp:CheckBox ID="txtcb" runat="server" />
                    </DataItemTemplate>                        
                 </dx:GridViewDataTextColumn>--%>
            </Columns>
        </dx:ASPxGridView>
                </td>
            </tr>
                <tr>
                <td>
                    &nbsp;</td>
            </tr>
               <tr>
                <td>
                    
                   </td>
            </tr>
        </table>
    </div>
                    </div>
         </div>
    </div>
    </form>
</body>
</html>
