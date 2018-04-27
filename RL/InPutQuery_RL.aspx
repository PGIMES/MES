<%@ Page Title="MES【投料查询】" Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.master" CodeFile="InPutQuery_RL.aspx.cs" Inherits="RL_InPutQuery_RL" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
<script src="../Content/js/jquery.min.js" type="text/javascript"></script>
    <script src="../Content/js/plugins/layer/layer.min.js" type="text/javascript"></script>
 <script type="text/javascript" language="javascript">
     $(document).ready(function () {

         $("#tst").click(function () {

         });

     });
     // $("div[class='h3']").text($("div[class='h3']").text() + "【精炼测氢查询】");
     $("#mestitle").text("【投料查询】");
        </script>
    <div class="row row-container">
        <div class="col-sm-12 ">
            <div class="panel panel-info">
                <div class="panel-heading">
                    <strong>查询</strong>
                    <asp:ScriptManager ID="ScriptManager1" runat="server">
                    </asp:ScriptManager>
                </div>
                <div class="panel-body">
                    <div class="col-sm-12">
                       
                                <table>
                                    
                                    <tr>
                                        
                                        
                                        <td>
                                            日期:
                                        </td>
                                        <td>
                                           <asp:TextBox ID="txt_startdate" runat="server" Width="100" />
             <ajaxtoolkit:calendarextender ID="txt_startdate_CalendarExtender" 
                 runat="server" PopupButtonID="Image2"  Format="yyyy/MM/dd"
                 TargetControlID="txt_startdate" />
             ~&nbsp;<asp:TextBox ID="txt_enddate" runat="server" 
                 Width="100" />
             <ajaxtoolkit:calendarextender ID="txt_enddate_CalendarExtender" 
                 runat="server" PopupButtonID="Image2"  Format="yyyy/MM/dd"
                 TargetControlID="txt_enddate" />
                                        </td>

                                        <td>
                                            设备简称:
                                        </td>
                                        <td>
                                          

                                                 <asp:DropDownList ID="txt_equip_name" runat="server"  class="form-control input-s-sm " Width="130px">
                                             <asp:ListItem Value=""></asp:ListItem>
                                             <asp:ListItem Value ="A">熔炼炉A</asp:ListItem>
                                             <asp:ListItem Value ='B'>熔炼炉B</asp:ListItem>
                                             <asp:ListItem Value ="C">熔炼炉C</asp:ListItem>
                                             <asp:ListItem Value ="D">熔炼炉D</asp:ListItem>
                                             <asp:ListItem Value ="E">熔炼炉E</asp:ListItem>
                                         </asp:DropDownList>



                                        </td>

                                        <td>
                                            合金:
                                        </td>
                                        <td>

                                          <asp:DropDownList ID="txt_hejin" runat="server"  class="form-control input-s-sm " Width="130px">
                                             <asp:ListItem Value =""></asp:ListItem>
                                             <asp:ListItem Value ="A380">A380</asp:ListItem>
                                             <asp:ListItem Value ="EN46000">EN46000</asp:ListItem>
                                             <asp:ListItem Value ="ADC12">ADC12</asp:ListItem>
                                             <asp:ListItem Value ="EN47100">EN47100</asp:ListItem>
                                         </asp:DropDownList>
                                           
                                        </td>
                                       
                                    </tr>
                                    <tr>
                                        <td>
                                            班别:
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="txt_banbie" runat="server" class="form-control input-s-sm ">
                                                <asp:ListItem Value =""></asp:ListItem>
                                                <asp:ListItem Value ="白班">白班</asp:ListItem>
                                                <asp:ListItem Value ="晚班">晚班</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                       
                                        <td>
                                            操作工:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txt_czg" class="form-control input-s-sm" runat="server"
                                               ></asp:TextBox>
                                        </td>
                                    </tr>
                                     
                                    <tr>
                                        
                                        <td colspan=5 align="right">
                                            <asp:Button ID="Button1" runat="server" Text="查询"  
                                              class="btn btn-large btn-primary" 
                                              onclick="Button1_Click" Width="100px" />
                                        </td>
                                        <td>
                                            &nbsp;
                                            <asp:Button ID="Button2" runat="server" Text="返回"  
                                              class="btn btn-large btn-primary" 
                                               Width="100px" onclick="Button2_Click" />
                                        </td>
                                    </tr>
                                   
                                     
                                </table>
                                </div>
                     
                    </div>
                </div>
            </div>
        </div>
        <br />
         <div  runat="server" id="DIV1"  style=" margin:20px">
        
                       
                                <asp:Panel ID="Panel2" runat="server" Height="100%" >
                                    <table style=" background-color: #FFFFFF;" 
                                      >
                                      
                                        <tr>
                                            <td valign="top">
                                                <asp:GridView ID="GridView1" runat="server" 
                                                    AllowPaging="True" AllowSorting="True" 
                                                    AutoGenerateColumns="False" 
                                                    onpageindexchanging="GridView1_PageIndexChanging" 
                                                    onrowdatabound="GridView1_RowDataBound" 
                                                    onsorting="GridView1_Sorting" PageSize="100" Width="1500px">
                                                    <Columns>
                                                       
                                                        <asp:BoundField DataField="mixdate" 
                                                            DataFormatString="{0:yyyy-MM-dd}" HeaderText="日期"  SortExpression="mixdate">
                                                        <HeaderStyle BackColor="#C1E2EB" />
                                                        </asp:BoundField>

                                                        <asp:BoundField DataField="emp_banbie" HeaderText="班别" SortExpression="emp_banbie">
                                                        <HeaderStyle BackColor="#C1E2EB" />
                                                        </asp:BoundField>
                                                          <asp:BoundField DataField="lot" HeaderText="批号" SortExpression="lot">
                                                        <HeaderStyle BackColor="#C1E2EB" />
                                                        </asp:BoundField>
                                                         <asp:BoundField DataField="equip_name" HeaderText="设备简称"  SortExpression="equip_name">
                                                        <HeaderStyle BackColor="#C1E2EB" />
                                                        </asp:BoundField>
                                                                               <asp:BoundField DataField="mixtime" HeaderText="投料时间">
                                                        <HeaderStyle BackColor="#C1E2EB" />
                                                        </asp:BoundField>

                                                         <asp:BoundField DataField="jiange" HeaderText="投料间隔" ItemStyle-HorizontalAlign=Right>
                                                        <HeaderStyle BackColor="#C1E2EB" />
                                                        </asp:BoundField>


                                                         <asp:BoundField DataField="Hejing" HeaderText="合金">
                                                        <HeaderStyle BackColor="#C1E2EB" />
                                                        </asp:BoundField>
                                                       
                                                         <asp:BoundField DataField="material_type" HeaderText="材料类别">
                                                        <HeaderStyle BackColor="#C1E2EB" />
                                                        </asp:BoundField>

                                                         <asp:BoundField DataField="material_lot" HeaderText="原材料批号">
                                                        <HeaderStyle BackColor="#C1E2EB" />
                                                        </asp:BoundField>



                                                         <asp:BoundField DataField="crossweight" HeaderText="毛重" ItemStyle-HorizontalAlign=Right>
                                                        <HeaderStyle BackColor="#C1E2EB" />
                                                        </asp:BoundField>
                                                         <asp:BoundField DataField="tareweight" HeaderText="皮重"  ItemStyle-HorizontalAlign=Right>
                                                        <HeaderStyle BackColor="#C1E2EB" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="netweight" HeaderText="净重" ItemStyle-HorizontalAlign=Right>
                                                        <HeaderStyle BackColor="#C1E2EB" />
                                                        </asp:BoundField>

                                                       
                                                        <asp:BoundField DataField="a_weight" HeaderText="铝锭累计" ItemStyle-HorizontalAlign=Right>
                                                        <HeaderStyle BackColor="#C1E2EB" />
                                                        </asp:BoundField>
                                                         <asp:BoundField DataField="b_weight" HeaderText="一级回炉累计" ItemStyle-HorizontalAlign=Right>
                                                        <HeaderStyle BackColor="#C1E2EB" />
                                                        </asp:BoundField>
                                                          <asp:BoundField DataField="c_weight" HeaderText="二级回炉累计" ItemStyle-HorizontalAlign=Right>
                                                        <HeaderStyle BackColor="#C1E2EB" />
                                                        </asp:BoundField>
                                                          <asp:BoundField DataField="d_weight" HeaderText="三级回炉累计" ItemStyle-HorizontalAlign=Right>
                                                        <HeaderStyle BackColor="#C1E2EB" />
                                                        </asp:BoundField>

                                                        <asp:BoundField DataField="a_rate" HeaderText="铝锭占比" ItemStyle-HorizontalAlign=Right>
                                                        <HeaderStyle BackColor="#C1E2EB" />
                                                        </asp:BoundField>
                                                        

                                                         <asp:BoundField DataField="b_rate" HeaderText="一级回炉占比" ItemStyle-HorizontalAlign=Right>
                                                        <HeaderStyle BackColor="#C1E2EB" />
                                                        </asp:BoundField>

                                                        

                                                         <asp:BoundField DataField="c_rate" HeaderText="二级回炉占比" ItemStyle-HorizontalAlign=Right>
                                                        <HeaderStyle BackColor="#C1E2EB" />
                                                        </asp:BoundField>

                                                       

                                                         <asp:BoundField DataField="d_rate" HeaderText="三级回炉占比" ItemStyle-HorizontalAlign=Right>
                                                        <HeaderStyle BackColor="#C1E2EB" />
                                                        </asp:BoundField>

                                                      
                                                        <asp:BoundField DataField="emp_name" HeaderText="操作工">
                                                        <HeaderStyle BackColor="#C1E2EB" />
                                                        </asp:BoundField>
                                                    </Columns>
                                                    <PagerSettings FirstPageText="首页" LastPageText="尾页" 
                                                        Mode="NextPreviousFirstLast" NextPageText="下页" 
                                                        PreviousPageText="上页" />
                                                    <PagerStyle ForeColor="Red" />
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                                </div>
                     
          
        
</asp:Content>

