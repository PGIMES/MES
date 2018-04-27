<%@ Page Title="MES【光谱测量】" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="GP_CS.aspx.cs" Inherits="JingLian_GP_CS" %>

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
    
     $("#mestitle").text("【光谱测量】");
        </script>

      <div class="row row-container">
        <div class="col-sm-12 ">
            <div class="panel panel-info">
                <div class="panel-heading">
                    <strong>基本信息</strong>
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
                                            <asp:TextBox ID="txt_date" class="form-control input-s-sm" runat="server"
                                                ReadOnly="True"></asp:TextBox>
                                        </td>
                                        <td>
                                            时间:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txt_time" class="form-control input-s-sm " runat="server"
                                                ReadOnly="True"></asp:TextBox>
                                        </td>
                                        <td>
                                            班别:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txt_shift" class="form-control input-s-sm" runat="server"
                                                ReadOnly="True"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            工号:
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="txt_gh" runat="server" class="form-control input-s-sm"
                                                AutoPostBack="True" onselectedindexchanged="txt_gh_SelectedIndexChanged" 
                                                >
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            姓名:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txt_name" class="form-control input-s-sm" runat="server"
                                                ReadOnly="True"></asp:TextBox>
                                        </td>
                                        <td>
                                            班组:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txt_banzu" class="form-control input-s-sm" runat="server"
                                                ReadOnly="True"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                                </div>
                       
                    </div>
                </div>
            </div>
        </div>

            <div class="row row-container">
        <div class="col-sm-12 ">
            <div class="panel panel-info">
                <div class="panel-heading">
                    <strong>光谱测量</strong>
                </div>
                <div class="panel-body">
                      
                    <div class="col-sm-12">
                        <div class=" col-sm-1">样件来自</div>
                        <div class=" col-sm-2 ">
                            <asp:DropDownList ID="ddl_source" runat="server" 
                                class="form-control input-small" AutoPostBack="True" onselectedindexchanged="ddl_source_SelectedIndexChanged" 
                              >
                                <asp:ListItem></asp:ListItem>
                                <asp:ListItem>精炼机</asp:ListItem>
                                <asp:ListItem>保温炉</asp:ListItem>
                                <asp:ListItem>进货</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                          <div class=" col-sm-1">样件序列号</div>
                          <div class=" col-sm-2">
                              <asp:DropDownList ID="ddl_dh" runat="server" 
                                  class="form-control input-small" AutoPostBack="True" 
                                  onselectedindexchanged="ddl_dh_SelectedIndexChanged">
                              </asp:DropDownList> </div>   
                          <div class=" col-sm-1">
                             设备号</div>
                         <div class=" col-sm-2">
                             <asp:DropDownList ID="ddl_sbno" runat="server" 
                                 class="form-control input-small" AutoPostBack="True" 
                                 onselectedindexchanged="ddl_sbno_SelectedIndexChanged">
                             </asp:DropDownList>
                         </div>   
                          <div class=" col-sm-1">
                             供应商</div>
                         <div class=" col-sm-2">
                             <asp:DropDownList ID="ddl_gys" runat="server" 
                                 class="form-control input-small" AutoPostBack="True" onselectedindexchanged="ddl_gys_SelectedIndexChanged" 
                                >
                                 <asp:ListItem></asp:ListItem>
                                 <asp:ListItem Value="ZF">ZF-上海众福</asp:ListItem>
                                 <asp:ListItem Value="SYC">SYC-帅翼驰</asp:ListItem>
                                 <asp:ListItem Value="TL">TL-泰龙</asp:ListItem>
                             </asp:DropDownList>
                         </div>   
                    </div>

                     <div class="col-sm-12">
                        <div class=" col-sm-1">合金</div>
                        <div class=" col-sm-2 ">
                            <asp:DropDownList ID="ddl_hejin" runat="server" class="form-control input-small">
                            </asp:DropDownList>
                        </div>
                          <div class=" col-sm-1">熔炼炉号</div>
                          <div class=" col-sm-2">
                              <asp:DropDownList ID="ddl_luhao" runat="server" class="form-control input-small">
                              </asp:DropDownList> </div>
                        <div class=" col-sm-1">原材料批号</div>
                          <div class=" col-sm-2">
                              <asp:TextBox ID="txt_bihao" runat="server" class="form-control input-small"></asp:TextBox>
                              </div>
                              <div id="DIV2" runat="server">
                         <div class=" col-sm-1">测试文件名</div>
                          <div class=" col-sm-2">
                              <asp:DropDownList ID="ddl_file" runat="server" class="form-control input-small">
                              </asp:DropDownList> </div></div>
                     </div>
                   </div>       
                     
                
            </div>
        </div>
       
   
    </div>

     <div class="row row-container">
        <div class="col-sm-12 ">
         <div id="Div3" runat="server" class="col-sm-4 " style="padding: 0;
                                        position: relative; float: left; top: 0px; left: 0px; width: 200px;
                                        height: 70px;">
                                        <asp:Button ID="btn_begin" runat="server" Font-Size="X-Large"
                                            class="btn btn-primary" Style="position: absolute; left: -1;
                                            right: 1px; width: 200px; top: -1px; height: 70px;" Text="开始测量"
                                            OnClick="btn_begin_Click" />
                                        <div id="div4" runat="server">
                                            <asp:Label ID="lb_start" runat="server" 
                                                Style="position: absolute;
                                                right: -15px; left: 35px; width: 160px; top: 45px; height: 20px;"></asp:Label>
                                        </div>
                                    </div>
              
              <div id="Div5" runat="server" class="col-sm-4  col-md-offset-2" style="padding: 0;
                                        position: relative; float: left; top: 0px; left: 0px; width: 200px;
                                        height: 70px;">
                                        <asp:Button ID="btn_confirm" runat="server" Font-Size="X-Large"
                                            class="btn btn-primary" Style="position: absolute; left: -1;
                                            right: 1px; width: 200px; top: -1px; height: 70px;" 
                                            Text="确认测量" onclick="btn_confirm_Click" 
                                          />
                                       <div id="div6" runat="server">
                                            <asp:Label ID="lb_end" runat="server"   Visible="false" Style="position: absolute;
                                                right: 55px; left: 35px; width: 160px; top: 45px"></asp:Label>
                                        </div>
                                    </div>
                <div id="Div7" runat="server" class="col-sm-4  col-md-offset-2" style="padding: 0;
                                        position: relative; float: left; top: 0px; left: 0px; width: 200px;
                                        height: 70px;">
                                        <asp:Button ID="btn_return" runat="server" Font-Size="X-Large"
                                            class="btn btn-primary" Style="width: 200px; top: -1px; height: 70px;" 
                                            Text="返回" onclick="btn_return_Click"
                                          />
                                       
                                    </div>
        </div></div>

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
                                                       
                                                      
                                                          <asp:BoundField DataField="standarditem" HeaderText="项目" SortExpression="standarditem">
                                                        <HeaderStyle BackColor="#C1E2EB" />
                                                        </asp:BoundField>
                                                         <asp:BoundField DataField="si" HeaderText="Si" 
                                                              DataFormatString="{0:N3}"  >
                                                        <HeaderStyle BackColor="#C1E2EB" />
                                                          <ItemStyle HorizontalAlign="Right" />
                                                        </asp:BoundField>
                                                                               <asp:BoundField DataField="fe" 
                                                              HeaderText="Fe" DataFormatString="{0:N3}">
                                                        <HeaderStyle BackColor="#C1E2EB" />
                                                          <ItemStyle HorizontalAlign="Right" />
                                                        </asp:BoundField>


                                                         <asp:BoundField DataField="cu" HeaderText="Cu" 
                                                              DataFormatString="{0:N3}">
                                                        <HeaderStyle BackColor="#C1E2EB" />
                                                          <ItemStyle HorizontalAlign="Right" />
                                                        </asp:BoundField>
                                                       
                                                         <asp:BoundField DataField="mg" HeaderText="Mg" 
                                                              DataFormatString="{0:N3}">
                                                        <HeaderStyle BackColor="#C1E2EB" />
                                                          <ItemStyle HorizontalAlign="Right" />
                                                        </asp:BoundField>

                                                         <asp:BoundField DataField="mn" HeaderText="Mn" 
                                                              DataFormatString="{0:N3}">
                                                        <HeaderStyle BackColor="#C1E2EB" />
                                                          <ItemStyle HorizontalAlign="Right" />
                                                        </asp:BoundField>



                                                         <asp:BoundField DataField="cr" HeaderText="Cr" 
                                                              ItemStyle-HorizontalAlign=Right DataFormatString="{0:N3}">
                                                        <HeaderStyle BackColor="#C1E2EB" />
                                                          <ItemStyle HorizontalAlign="Right" />
                                                        </asp:BoundField>
                                                         <asp:BoundField DataField="ni" HeaderText="Ni"  
                                                              ItemStyle-HorizontalAlign=Right DataFormatString="{0:N3}">
                                                        <HeaderStyle BackColor="#C1E2EB" />
                                                          <ItemStyle HorizontalAlign="Right" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="zn" HeaderText="Zn" 
                                                              ItemStyle-HorizontalAlign=Right DataFormatString="{0:N3}">
                                                        <HeaderStyle BackColor="#C1E2EB" />
                                                          <ItemStyle HorizontalAlign="Right" />
                                                        </asp:BoundField>

                                                       
                                                        <asp:BoundField DataField="ti" HeaderText="Ti" 
                                                              ItemStyle-HorizontalAlign=Right DataFormatString="{0:N3}">
                                                        <HeaderStyle BackColor="#C1E2EB" />
                                                          <ItemStyle HorizontalAlign="Right" />
                                                        </asp:BoundField>
                                                         <asp:BoundField DataField="pb" HeaderText="Pb" 
                                                              ItemStyle-HorizontalAlign=Right DataFormatString="{0:N3}">
                                                        <HeaderStyle BackColor="#C1E2EB" />
                                                          <ItemStyle HorizontalAlign="Right" />
                                                        </asp:BoundField>
                                                          <asp:BoundField DataField="sn" HeaderText="Sn" 
                                                              ItemStyle-HorizontalAlign=Right DataFormatString="{0:N3}">
                                                        <HeaderStyle BackColor="#C1E2EB" />
                                                          <ItemStyle HorizontalAlign="Right" />
                                                        </asp:BoundField>
                                                          <asp:BoundField DataField="al" HeaderText="Al" 
                                                              ItemStyle-HorizontalAlign=Right DataFormatString="{0:N3}">
                                                        <HeaderStyle BackColor="#C1E2EB" />
                                                          <ItemStyle HorizontalAlign="Right" />
                                                        </asp:BoundField>

                                                        <asp:BoundField DataField="sr" HeaderText="Sr" 
                                                              ItemStyle-HorizontalAlign=Right DataFormatString="{0:N3}">
                                                        <HeaderStyle BackColor="#C1E2EB" />
                                                          <ItemStyle HorizontalAlign="Right" />
                                                        </asp:BoundField>
                                                        

                                                         <asp:BoundField DataField="sf" HeaderText="SF" 
                                                              ItemStyle-HorizontalAlign=Right DataFormatString="{0:N3}">
                                                        <HeaderStyle BackColor="#C1E2EB" />
                                                          <ItemStyle HorizontalAlign="Right" />
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

