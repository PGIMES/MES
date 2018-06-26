<%@ Page Language="C#" AutoEventWireup="true" CodeFile="1.aspx.cs" Inherits="Forms_PgiOp_1" %>
<%@ Register assembly="DevExpress.Web.v17.2, Version=17.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web" tagprefix="dx" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <script src="/Content/js/jquery.min.js"></script>
    <style>
        .hidden { display:none;}
    </style>
    <script>
        //function a(s,e) {
        //    alert('1');
        //}
        function RefreshRow(vi) {
            var op = eval('op' + vi);
            var num = op.GetText();
            //alert(num);

            //var pgi_no = eval('pgi_no' + vi);
            //pgi_no.SetText(num);
            $('#gv_d_cell' + vi + '_2_pgi_no_' + vi).html(num);

            //var codes = grid.cpCodes;
            //var used = 0;
            //var cboxes = new Array(codes.length);
            //for (var i = 0; i < codes.length; i++) {
            //    var cbx = eval('cbx_' + codes.charAt(i) + vi);
            //    cboxes[i] = cbx;
            //    if (cbx.GetValue()) used++;
            //}
            //for (var i = 0; i < codes.length; i++) {
            //    var cbx = cboxes[i];
            //    cbx.SetEnabled(cbx.GetValue() || (used < num));
            //}
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Button ID="Button2" runat="server" Text="Button" OnClick="Button2_Click" />
        <dx:aspxgridview ID="gv_d" runat="server" AutoGenerateColumns="False" KeyFieldName="numid" Theme="MetropolisBlue" ClientInstanceName="gv_d" OnRowCommand="gv_d_RowCommand"  EnableTheming="True"  >                                   
            <SettingsPager PageSize="1000"></SettingsPager>
            <Settings ShowFooter="True" />
            <SettingsBehavior AllowSelectByRowClick="True" AllowDragDrop="False" AllowSort="False" />
            <Columns>
                <dx:GridViewCommandColumn SelectAllCheckboxMode="Page" ShowClearFilterButton="true" ShowSelectCheckbox="true" Name="Sel" Width="40" VisibleIndex="1"></dx:GridViewCommandColumn>
                <dx:GridViewDataTextColumn Caption="工艺段" FieldName="typeno" Width="60px" VisibleIndex="2"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="工艺流程" FieldName="pgi_no" Width="80px" VisibleIndex="3">
                    <DataItemTemplate>
                        <%--<dx:ASPxLabel ID="pgi_no" runat="server" Text='<%# Eval("pgi_no") %>'></dx:ASPxLabel>--%>
                        <asp:Label ID="pgi_no" runat="server" Text='<%# Eval("pgi_no") %>' ClientInstanceName='<%# "pgi_no"+Container.VisibleIndex.ToString() %>'></asp:Label>
                    </DataItemTemplate>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="工序号" FieldName="op" Width="60px" VisibleIndex="4">
                    <Settings AllowCellMerge="False" />
                    <DataItemTemplate>                <%--AutoPostBack="True"  OnValueChanged="op_ValueChanged" OnTextChanged="op_TextChanged"--%> 
                        <dx:ASPxTextBox ID="op" Width="60px" runat="server" Value='<%# Eval("op") %>'  
                            ClientSideEvents-ValueChanged='<%# "function(s,e){RefreshRow("+Container.VisibleIndex+");}" %>' 
                            ClientInstanceName='<%# "op"+Container.VisibleIndex.ToString() %>'>
                            <%--<ClientSideEvents TextChanged="function(s, e) { a(s,e);}" />--%>
                             <ValidationSettings ValidationGroup="ValueValidationGroup" Display="Dynamic" ErrorTextPosition="Bottom">
                                <RegularExpression ErrorText="请输入数字！" ValidationExpression="^-?[1-9]+(\.\d+)?$|^-?0(\.\d+)?$|^-?[1-9]+[0-9]*(\.\d+)?$" />
                            </ValidationSettings>
                        </dx:ASPxTextBox>  
                       
                         <br /><asp:Button ID="Button1" runat="server" Text="Button" OnClick="Button1_Click" />      
                    </DataItemTemplate>   
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="" Caption=" " VisibleIndex="5" >
                    <Settings AllowCellMerge="False" />
                    <DataItemTemplate>                
                        <dx:ASPxButton ID="btn" runat="server" Text="新增行"   CommandName="Add"  ></dx:ASPxButton>          
                    </DataItemTemplate>                        
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="numid" Width="0px">
                        <HeaderStyle CssClass="hidden" />
                        <CellStyle CssClass="hidden"></CellStyle>
                        <FooterCellStyle CssClass="hidden"></FooterCellStyle>
                </dx:GridViewDataTextColumn>
                                        
            </Columns>                                                
            <Styles>
                <Header BackColor="#E4EFFA"  ></Header>        
                <SelectedRow BackColor="#FDF7D9"></SelectedRow>      
            </Styles>                                          
        </dx:aspxgridview>
    </div>
    </form>
</body>
</html>
