<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="YZQB_SH.aspx.cs" Inherits="JingLian_YZQB_SH" %>

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
     $("#mestitle").text("【压铸&切边一级分层审核表单】");
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



    <div class="row row-container">
        <div class="col-sm-12 ">
            <div class="panel panel-info">
                <div class="panel-heading">
                    <strong>压铸&切边审核</strong>
                </div>
               <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                            <div class="panel-body">
                    <div class="col-sm-12">
                        <asp:GridView ID="GridView1" runat="server" 
                            AutoGenerateColumns="False" Height="30px" 
                            Width="500px">
                            <Columns>
                                <asp:BoundField DataField="JC_item" 
                                    HeaderText="检查项">
                                <HeaderStyle BackColor="#C1E2EB" />
                                <ItemStyle Wrap="False" />
                                </asp:BoundField>
                                <asp:BoundField DataField="JC_Remark" HeaderText="过程审核 ">
                                <HeaderStyle BackColor="#C1E2EB" Height="30px" />
                                <ItemStyle Wrap="False" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="是">
                                    <ItemTemplate>      
                                        <asp:CheckBox ID="cb1" runat="server" AutoPostBack="True" 
                                            oncheckedchanged="cb1_CheckedChanged"  />
                                    </ItemTemplate>
                                    <HeaderStyle BackColor="#C1E2EB" Width="100px"  />
                                    
                                    <ItemStyle Width="100px" />
                                    
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="否">
                                    <ItemTemplate>
                                         <asp:CheckBox ID="cb2" runat="server" AutoPostBack="True" 
                                             oncheckedchanged="cb2_CheckedChanged"  />
                                    </ItemTemplate>
                                    <HeaderStyle BackColor="#C1E2EB" Width="100px" />
                                    <ItemStyle Width="100px" />
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                       </div>
                       
                       
                </div>
                </ContentTemplate>
               
                </asp:UpdatePanel>
            </div>
        </div>

    </div>
     <div class="row row-container">
        <div class="col-sm-12 ">
        <div class="col-sm-12 col-md-offset-3">
            <asp:Button ID="btn_confirm" runat="server" Text="提交"  
                class="btn btn-primary col-md-offset-2" Font-Size="X-Large" style="width: 200px;
                                height: 70px;" 
                onclick="btn_confirm_Click"/> 
            <asp:Button ID="btnNext" runat="server" Text="Next" 
                onclick="btnNext_Click"   style=" display:none"  />
            </div>
             
        </div></div>
       
</asp:Content>


