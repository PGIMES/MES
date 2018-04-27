<%@ Page Title="MES【转运包清理】" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="ZYB_Clear.aspx.cs" Inherits="JingLian_ZYB_Clear" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
<script src="../Content/js/jquery.min.js" type="text/javascript"></script>
    <script src="../Content/js/plugins/layer/layer.min.js" type="text/javascript"></script>
 <script type="text/javascript" language="javascript">
     $(document).ready(function () {

//         $('#tst').click(function () {
//             $("#MainContent_btnNext").click();
//         });

     });
     //$("div[class='h3']").text($("div[class='h3']").text() + "【转运包清理】");
     $("#mestitle").text("【转运包清理】");
     //if (confirm('是否需要清理下一个转运包！') == true) { $("#MainContent_btnNext").click(); }
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

    </div>

    <div class="row row-container">
        <div class="col-sm-12 ">
            <div class="panel panel-info">
                <div class="panel-heading">
                    <strong>转运包清理</strong>
                </div>
                <div class="panel-body">
                    <div class="col-sm-12">
                       <div class=" col-sm-2">转运包号</div>
                       <div class=" col-sm-2">
                           <asp:DropDownList ID="ddl_zybh" runat="server" 
                               class="form-control input-s-sm" AutoPostBack="True" 
                               onselectedindexchanged="ddl_zybh_SelectedIndexChanged"  >
                           </asp:DropDownList>
                       </div>
                      <div runat="server" id="DIV1">
                       <div  class="col-sm-2" > <asp:Button ID="btn_zyb_1" runat="server" Text="Button"  class="btn btn-default"/></div>   
                   
                    <div class="col-sm-2"> <asp:Button ID="btn_zyb_2" runat="server" Text="Button"  class="btn btn-default" /></div>
                     <div class="col-sm-2"> <asp:Button ID="btn_zyb_3" runat="server" Text="Button"  class="btn btn-default" /></div>
                      <div class="col-sm-2"> <asp:Button ID="btn_zyb_4" runat="server" Text="Button"   class="btn btn-default"/></div>
                     </div></div>
                       <div class="col-sm-12" style="color: #FF0000">
                       *所有需使用的转运包需逐一确认清理，未清理转运包不可用于测氢工序！
                       </div>
                </div>

                 <div class="panel-body">
                    <div class="col-sm-12">
                        <asp:GridView ID="GridView1" runat="server" 
                            AutoGenerateColumns="False" Height="30px" Width="1032px">
                            <Columns>
                                <asp:BoundField DataField="CheckDescription" 
                                    HeaderText="转运包清理项">
                                <HeaderStyle BackColor="#C1E2EB" Height="30px" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="清理结果">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddl_result" runat="server" 
                                            Width="100px" Height="30px">
                                            <asp:ListItem></asp:ListItem>
                                            <asp:ListItem>OK</asp:ListItem>
                                            <asp:ListItem>NG</asp:ListItem>
                                        </asp:DropDownList>
                                    </ItemTemplate>
                                    <HeaderStyle BackColor="#C1E2EB" />
                                    <ItemStyle Width="200px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="说明">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txt_remark" runat="server" Width="380px" 
                                            Height="30px"></asp:TextBox>
                                    </ItemTemplate>
                                    <HeaderStyle BackColor="#C1E2EB" />
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                       </div>
                       
                       
                </div>

                 <div class="panel-body">
                    <div class="col-sm-12">
                       <div  class="col-sm-2">本班转运包称重(Kg)</div>
                       <div  class="col-sm-2">
                           <asp:TextBox ID="txt_cz" runat="server" Height="30px" class="form-control input-s-sm"></asp:TextBox></div>
                       </div>
                       
                       
                </div>
            </div>
        </div>

    </div>
     <div class="row row-container">
        <div class="col-sm-12 ">
        <div class="col-sm-3">
            <asp:Button ID="btn_confirm" runat="server" Text="确认清理"  
                class="btn btn-primary col-md-offset-2" Font-Size="X-Large" style="width: 200px;
                                height: 70px;" 
                onclick="btn_confirm_Click"/> 
            <asp:Button ID="btnNext" runat="server" Text="Next" 
                onclick="btnNext_Click"   style=" display:none"  />
            </div>
              <div class="col-sm-3">
            <asp:Button ID="btn_return" runat="server" Text="返回"  
                      class="btn btn-primary col-md-offset-2" Font-Size="X-Large" style="width: 200px;
                                height: 70px;" onclick="btn_return_Click" /> </div>
        </div></div>
          <div class="row row-container" style=" margin:20px">
        <div class="col-sm-12 ">
            <asp:GridView ID="GridView2" runat="server" 
                AllowPaging="True" AllowSorting="True" 
                AutoGenerateColumns="False" PageSize="100" 
                Width="1050px">
                <Columns>
                    <asp:BoundField DataField="equip_no" HeaderText="转运包" 
                        HtmlEncode="False">
                    <HeaderStyle BackColor="#C1E2EB" Height="30px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="check1" HeaderText="包内壁清理">
                    <HeaderStyle BackColor="#C1E2EB" />
                    </asp:BoundField>
                    <asp:BoundField DataField="check1demo" HeaderText="说明项">
                    <HeaderStyle BackColor="#C1E2EB" />
                    </asp:BoundField>
                    <asp:BoundField DataField="check2" HeaderText="包底部清理">
                    <HeaderStyle BackColor="#C1E2EB" />
                    </asp:BoundField>
                    <asp:BoundField DataField="check2demo" HeaderText="说明项">
                    <HeaderStyle BackColor="#C1E2EB" />
                    </asp:BoundField>
                    <asp:BoundField DataField="check3" HeaderText="转运包检查">
                    <HeaderStyle BackColor="#C1E2EB" />
                    </asp:BoundField>
                    <asp:BoundField DataField="check3demo" HeaderText="说明项">
                    <HeaderStyle BackColor="#C1E2EB" />
                    </asp:BoundField>
                    <asp:BoundField DataField="check4" HeaderText="转运包被履" 
                        HtmlEncode="False">
                    <HeaderStyle BackColor="#C1E2EB" />
                    </asp:BoundField>
                    <asp:BoundField DataField="check4demo" HeaderText="说明项">
                    <HeaderStyle BackColor="#C1E2EB" />
                    </asp:BoundField>
                    <asp:BoundField DataField="weight" HeaderText="称重" 
                        HtmlEncode="False">
                    <HeaderStyle BackColor="#C1E2EB" />
                    </asp:BoundField>
                    <asp:BoundField DataField="checkdate" 
                        DataFormatString="{0:HH:mm:ss}" HeaderText="清理时间">
                    <HeaderStyle BackColor="#C1E2EB" />
                    </asp:BoundField>
                </Columns>
                <PagerSettings FirstPageText="首页" LastPageText="尾页" 
                    Mode="NextPreviousFirstLast" NextPageText="下页" 
                    PreviousPageText="上页" />
                <PagerStyle ForeColor="Red" />
            </asp:GridView>
        </div></div>
</asp:Content>

