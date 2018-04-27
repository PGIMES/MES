<%@ Page Language="C#" AutoEventWireup="true" CodeFile="select_YJ.aspx.cs" Inherits="Select_select_XMH" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style1 {
            text-align: left;
        }
    </style>
</head>
<body  style="background: url(../images/bg.jpg) repeat-x;">
    <form id="form1" runat="server">
    <div><asp:Panel ID="Panel1" runat="server">
                                       <table style="width:100%">
                                              <tr>
                             <td style="width: 431px">
                               
                                 本样件订单的零件号为:<asp:Label ID="Lab_ljh" runat="server" Text="Label" BackColor="Aqua"></asp:Label>
                               
                             </td>
                              <td class="auto-style1">
                                  发货工厂为:<asp:Label ID="Lab_Domain" runat="server" BackColor="Aqua" Text="Label"></asp:Label>
                                                  </td>
                          </tr>
                                              <tr>
                             <td style="width: 431px">
                               
                                 当前仓库已存的零件清单如下，请选择你所需要的零件号:</td>
                              <td align="right">
                                  &nbsp;</td>
                          </tr>
                                              <tr>
                                                  <td style="width: 431px">
                                                      <asp:RadioButtonList ID="Rab_list" runat="server">
                                                      </asp:RadioButtonList>
                                                  </td>
                                                  <td align="right" style="text-align: left">
                                                      <asp:Label ID="Lab_ts" runat="server" ForeColor="#FF3300" style="text-align: left"></asp:Label>
                                                  </td>
                                              </tr>
                                              <tr>
                                                  <td style="width: 431px; text-align: center;">
                                                      <asp:Button ID="Btnqr" runat="server" OnClick="Btnqr_Click" Text="确认" />
                                                  </td>
                                                  <td align="right" style="text-align: center">
                                                      <asp:Button ID="BtnStartSearch1" runat="server" OnClick="Btnqx" Text="取消" />
                                                  </td>
                                              </tr>
                                     </table>
                                 </asp:Panel>
        <asp:Panel ID="Panel2" runat="server">
      <table style="width:100%">
                          <tr>
                             <td >
                               <asp:DropDownList ID="DDL" runat="server" Width="120px">
                               <asp:ListItem Value="1" Text="PGI零件号"></asp:ListItem>
                                    
                                   <asp:ListItem Value="2">客户物料号</asp:ListItem>
                                    
                                 </asp:DropDownList>
                                 <asp:TextBox ID="txt_xmh" runat="server" Width="100px"></asp:TextBox>
                             </td>
                             <td >
                                 发货至</td>
                             <td >
                                 <asp:TextBox ID="txt_fhz" runat="server" Width="100px"></asp:TextBox>
                             </td>
                              <td align="right">
                                <asp:Button ID="BtnStartSearch" runat="server" Text="查询"  OnClick="BtnStartSearch_Click"  />
                              </td>
                          </tr>
                          <tr>
                             <td align="center" colspan="4">
                                 
                                     <asp:Label ID="lb_msg" runat="server" Text="" ForeColor="Red"></asp:Label>
                                        <asp:GridView ID="GridView1"  Width="100%"
                                        AllowMultiColumnSorting="True" AllowPaging="True"
                                    AllowSorting="True" AutoGenerateColumns="False" 
                                    
                                     OnPageIndexChanging="GridView1_PageIndexChanging"
                                     OnRowDataBound="GridView1_RowDataBound"
                                          runat="server" 
                                         onselectedindexchanged="GridView1_SelectedIndexChanged" Font-Size="Small" 
                                          
                                          >
                                           <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                             <PagerSettings FirstPageText="首页" LastPageText="尾页" 
                                Mode="NextPreviousFirstLast" NextPageText="下页" PreviousPageText="上页" />
                           <PagerStyle ForeColor="Red" />
                                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                <EditRowStyle BackColor="#999999" />
                                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />    
                                
                                 <Columns>
                       <asp:BoundField DataField="CP_ID" HeaderText="客户物料id" 
                                       ReadOnly="True" >  
                                       <HeaderStyle Wrap="True" />
                                        <ItemStyle Width="10%" />
                                    </asp:BoundField>
                                        <asp:BoundField DataField="sqgc" HeaderText="申请工厂" 
                                       ReadOnly="True" >  
                                       <HeaderStyle Wrap="True" />
                                        <ItemStyle Width="10%" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="pt_part" HeaderText="PGI零件号" 
                                       ReadOnly="True" >  
                                       <HeaderStyle Wrap="True" />
                                        <ItemStyle Width="10%" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="cp_cust_part" HeaderText="客户零件号" 
                                        ReadOnly="True">
                                        <HeaderStyle Wrap="True" />
                                        <ItemStyle Width="10%" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="ljmc" HeaderText="零件名称">
                                        <HeaderStyle Wrap="False" />
                                        <ItemStyle Width="10%" />
                                    </asp:BoundField>
                                     <asp:BoundField DataField="DebtorCode" HeaderText="客户代码">
                                        <HeaderStyle Wrap="False" />
                                        <ItemStyle Width="10%" />
                                    </asp:BoundField>
                                     <asp:BoundField DataField="cp_cust" HeaderText="发货至">
                                        <HeaderStyle  Wrap="false" />
                                        <ItemStyle Width="10%" />
                                    </asp:BoundField> 
                                     <asp:BoundField DataField="DebtorShipToName" HeaderText="客户名称">
                                        <HeaderStyle Wrap="False" />
                                        <ItemStyle Width="30%" />
                                    </asp:BoundField>
                                     <asp:CommandField ButtonType="Button" SelectText="选择" 
                                         ShowSelectButton="True" HeaderText="选择" >
                                     <ItemStyle Width="10%" />
                                     </asp:CommandField>
                                </Columns>
                                        </asp:GridView>
                                        
                             </td>
                          </tr>
                        
                </table>  </asp:Panel>
    </div>
    </form>
</body>
</html>
