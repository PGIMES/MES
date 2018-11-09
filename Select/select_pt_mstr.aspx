<%@ Page Language="C#" AutoEventWireup="true" CodeFile="select_pt_mstr.aspx.cs" Inherits="Select_select_pt_mstr" %>

<%@ Register Assembly="DevExpress.Web.v17.2, Version=17.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>【辅料查询】</title>
    <script src="/Content/js/jquery.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        function rerurn_data(){
            grid.GetSelectedFieldValues('pt_part;pt_desc1;pt_desc2', function GetVal(values) {
                //for (var i = 0; i < values.length; i++) { values[i][1]}
                if (values.length > 0) {
                    var lswlh = values[0][0];
                    var lswlmc = values[0][1];
                    var lsms = values[0][2];
                    var lslx=""; var lsjs=""; var lsdjedsm=""; var lspp=""; var lsgys="";

                    //if (pt_status == "OBS" || pt_status == "DEAD") {
                    //    layer.alert("物料号" + pgi_no + "已经失效，不能修改申请！");
                    //    return;
                    //}

                    window.opener.setvalue_dj(<%=nid.ToString()%>, lswlh, lsms,lslx, lsjs, lsdjedsm, lspp, lsgys,lswlmc);
                }
            });
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>

        <div class="panel-body">
            <div class="col-sm-12">
                <table class="tblCondition"  >
                    <tr>
                        <td>用于工厂：</td>
                        <td> 
                            <asp:DropDownList ID="DDL_domain" class="form-control input-s-sm" Style="height: 25px; width: 110px" runat="server">
                                <asp:ListItem Value="200">昆山工厂</asp:ListItem>
                                <asp:ListItem Value="100">上海工厂</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>&nbsp;&nbsp;物料号：</td>
                        <td >
                            <input id="txtwlh" class="form-control input-s-sm" style="height: 25px; width: 150px" runat="server"  />
                        </td>
                        <td>&nbsp;&nbsp;用于零件号：</td>
                        <td><input id="txtljh" class="form-control input-s-sm" style="height: 25px; width: 150px" runat="server"  /></td>
                        <td>&nbsp;&nbsp;
                            <asp:Button ID="Bt_select" runat="server" Text="查询" class="btn btn-large btn-primary" OnClick="Button1_Click" Width="100px" Height="30px" />
                        </td>
                    </tr>
                    <tr>  
                        <td colspan="6">&nbsp;&nbsp;<span style="font-size:x-small;color:red;">选择方式：1、双击行；2、单击行后，点击首列"选择"链接。</span></td>
                    </tr>
                </table>
            </div>
        </div>

        <div id="DIV1" style="margin-left: 5px; margin-right: 5px; margin-bottom: 10px">
            <table>
                <tr>
                    <td>
                        <dx:ASPxGridView ID="GV_PART" runat="server" KeyFieldName="pt_part" Width="650px" ClientInstanceName="grid" >
                            <SettingsBehavior AllowDragDrop="TRUE" AllowFocusedRow="True" AllowSelectByRowClick="True" ColumnResizeMode="Control" />
                            <ClientSideEvents CustomButtonClick="function(s, e) {rerurn_data();}" RowDblClick="function(s, e) {rerurn_data();}"  />
                            <SettingsPager PageSize="20"></SettingsPager>
                            <Settings ShowFilterRow="True" ShowFilterRowMenu="True" ShowFilterRowMenuLikeItem="True"  ShowFooter="True"/>
                            <SettingsCommandButton>
                                 <SelectButton ButtonType="Button" RenderMode="Button"></SelectButton>
                             </SettingsCommandButton>
                            <SettingsSearchPanel Visible="True" />
                            <SettingsFilterControl AllowHierarchicalColumns="True"></SettingsFilterControl>
                            <Columns>
                                <dx:GridViewCommandColumn Caption="选择" Name="Sel" Width="50px">
                                    <CustomButtons>
                                        <dx:GridViewCommandColumnCustomButton ID="btnid" Text="选择"></dx:GridViewCommandColumnCustomButton>
                                    </CustomButtons>
                                </dx:GridViewCommandColumn>
                                <dx:GridViewDataTextColumn Caption="物料号" FieldName="pt_part" Width="100px" VisibleIndex="2"></dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="客户零件号" FieldName="pt_desc1" Width="140px" VisibleIndex="3"></dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="物料描述" FieldName="pt_desc2" Width="160px" VisibleIndex="4"></dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="产品类" FieldName="pt_prod_line" Width="100px" VisibleIndex="5"></dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="产品状态" FieldName="pt_status" Width="100px" VisibleIndex="5"></dx:GridViewDataTextColumn>
                            </Columns>
                            <Styles>
                                <Header BackColor="#99CCFF"></Header>
                                <Footer HorizontalAlign="Right"></Footer>
                            </Styles>
                        </dx:ASPxGridView>
                    </td>
                </tr>
            </table>
        </div>

    </div>
    </form>
</body>
</html>
