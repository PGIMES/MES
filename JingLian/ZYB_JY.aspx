<%@ Page Title="MES【转运包加液记录】" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="ZYB_JY.aspx.cs" Inherits="JingLian_ZYB_JY" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
<script src="../Content/js/jquery.min.js" type="text/javascript"></script>
    <script src="../Content/js/plugins/layer/layer.min.js" type="text/javascript"></script>
 <script type="text/javascript" language="javascript">
     $(document).ready(function () {
     });
     $("#mestitle").text("【转运包加液记录】");
    
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
                                                AutoPostBack="True" OnSelectedIndexChanged="txt_gh_SelectedIndexChanged">
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
                    <strong>转运包加液</strong>
                </div>
                <div class="panel-body">
                    <div class="col-sm-12">
                       <div class=" col-sm-1">转运包<br />序列号</div>
                       <div class=" col-sm-2">
                           <asp:DropDownList ID="ddl_zybh" runat="server" 
                               class="form-control input-s-sm" AutoPostBack="True" onselectedindexchanged="ddl_zybh_SelectedIndexChanged" 
                                 >
                           </asp:DropDownList>
                       </div>
                       <div class=" col-sm-1">
                           <asp:Button ID="btn_refresh" runat="server" Text="刷新" 
                               onclick="btn_refresh_Click" /> </div>
                     <div class="col-sm-1">转运包号 </div>
                     <div class=" col-sm-2">
                         <asp:DropDownList ID="ddl_zyb" runat="server" class="form-control input-s-sm">
                         </asp:DropDownList> </div>
                      <div class="col-sm-1">合金 </div>
                      <div class=" col-sm-1">
                          <asp:DropDownList ID="ddl_hejin" runat="server" class="form-control input-s-sm">
                          </asp:DropDownList>
                      </div>
                      <div class="col-sm-1"> 加至保温炉号</div>
                      <div class="col-sm-2">
                          <asp:DropDownList ID="ddl_luhao" runat="server" class="form-control input-s-sm">
                          </asp:DropDownList>
                      </div>
                     </div>
                     
                </div>

                
            </div>
        </div>

    </div>
     <div class="row row-container">
        <div class="col-sm-12 ">
        <div class="col-sm-3">
         
  <asp:Button ID="btn_return" runat="server" Text="返回"  
                      class="btn btn-primary col-md-offset-1" 
                       Font-Size="X-Large" style="width: 200px;
                                height: 70px;" onclick="btn_return_Click"  />
            <asp:Button ID="btnNext" runat="server" Text="Next" 
                style=" display:none"  />
            </div>
              <div class="col-sm-3">
            <asp:Button ID="btn_finish" runat="server" Text="整包加液完成"  
                      class="btn btn-primary col-md-offset-1" 
                      Font-Size="X-Large" style="width: 200px;
                                height: 70px;" onclick="btn_finish_Click"  /> </div>
               <div class="col-sm-3">
            <asp:Button ID="btn_confirm" runat="server" Text="确认加液"  
                class="btn btn-primary col-md-offset-1" 
                Font-Size="X-Large" style="width: 200px;
                                height: 70px;" onclick="btn_confirm_Click" 
                />  </div>
        </div></div>

           <div class="row row-container" style=" margin:20px">
        <div class="col-sm-12 ">
            <asp:GridView ID="GridView1" runat="server" 
                AutoGenerateColumns="False" Width="1000px" 
                Height="30px">
                <Columns>
                    <asp:BoundField DataField="MaterialNo" HeaderText="转运包序列号">
                    <HeaderStyle BackColor="#C1E2EB" Font-Bold="True" />
                    </asp:BoundField>
                    <asp:BoundField DataField="zybh" HeaderText="转运包号">
                    <HeaderStyle BackColor="#C1E2EB" Font-Bold="True" />
                    </asp:BoundField>
                    <asp:BoundField DataField="hejing" HeaderText="合金">
                    <HeaderStyle BackColor="#C1E2EB" Font-Bold="True" />
                    </asp:BoundField>
                    <asp:BoundField DataField="equip_no" HeaderText="加至保温炉号">
                    <HeaderStyle BackColor="#C1E2EB" Font-Bold="True" />
                    </asp:BoundField>
                    <asp:BoundField DataField="inputdate" 
                        DataFormatString="{0:yyyy-MM-dd HH:mm:ss}" 
                        HeaderText="加液时间">
                    <HeaderStyle BackColor="#C1E2EB" />
                    </asp:BoundField>
                </Columns>
            </asp:GridView>
        </div></div>
</asp:Content>

