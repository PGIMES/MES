<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Moju_RK.aspx.cs" Inherits="BuLeHuanMo_Moju_RK" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContent" runat="Server">
    <style type="text/css">
        td
        {
            padding-left:0px;
            padding-right:5px;
            padding-top:5px;
            text-align:right;
        }
       
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
  <script src="../Content/js/jquery.min.js" type="text/javascript"></script>
    <script src="../Content/js/plugins/layer/layer.min.js" type="text/javascript"></script>
     <script type="text/javascript" >
         
          $(document).ready(function () {
            $("div[class='h3']").hide();
            })

          function closewin() {
             var index = parent.layer.getFrameIndex(window.name); //获取窗口索引                       
             parent.layer.close(index);
         }
   </script>
   

    <div style="padding-left: 5px; padding-right: 5px">
        <div class="panel panel-info ">
            <div class="panel-heading" style="vertical-align: middle;" align="center">
                模具入库
            </div>

            <div class="form-group" style="vertical-align: middle; text-align: center;">
                <div>
                    <table style="text-align: left; vertical-align: inherit;width:100%">
                        <tr>
                            <td>
                                模具号:
                            </td>
                            <td >
                                <asp:TextBox ID="txtrk_id" runat="server" Visible="False" 
                                    Width="20"></asp:TextBox>
                                <asp:TextBox ID="txtrk_weizhi" runat="server" 
                                    Visible="False" Width="20"></asp:TextBox>
                                <asp:TextBox ID="txtmoju_id" runat="server" Visible="False" 
                                    Width="20"></asp:TextBox>
                                <asp:TextBox ID="txt_Mojuno" 
                                    class="form-control input-sm disabled" Enabled=false  Width =150px 
                                    runat="server"></asp:TextBox>
                            </td>
                            <td class="td">
                                入库日期:
                            </td>
                            <td >
                                <asp:TextBox ID="txt_rkDate" 
                                    class="form-control input-sm disabled" Enabled=false Width =150px
                                    runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                入库类型:
                            </td>
                            <td class="style1">
                                <asp:TextBox ID="txt_rktype" class="form-control input-sm disabled" Enabled=false runat="server" Width =150px ></asp:TextBox>
                            </td>
                            <td >
                                                               入库人:</td>
                                                           <td>
                                                               

                                                                 <asp:DropDownList ID="txtrk_user1" runat="server"    Width="150px"/>
    
                                                            </td>
                        </tr>
                        <tr><td>
                                                                    模具库经手人:</td>
                                                           <td>
                                                               <asp:DropDownList ID="txtjs_user" runat="server"    Width="150px"/>



                                                            </td>
                       
                            
                            <td>
                                本次生产模次:
                            </td>
                            <td>
                                <asp:TextBox ID="txt_moci"  runat="server" Width =150px 
                                    ontextchanged="txt_moci_TextChanged" 
                                    AutoPostBack="True" ></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                入库时累计模次:
                            </td>
                            <td >
                                <asp:TextBox ID="txt_SumMoci" 
                                    class="form-control input-sm disabled" Enabled=false  Width =150px
                                    runat="server"></asp:TextBox>
                            </td>
                            <td>
                                用于压铸机号:
                            </td>
                            <td>
                                <asp:TextBox ID="txt_sbno" 
                                    class="form-control input-sm disabled" Enabled=false  Width =150px
                                    runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                入库状态说明:
                            </td>                           
                            <td colspan="3">
                                <asp:textbox ID="txtDemo" class="form-control " Height="80px" runat="server" 
                                    TextMode="MultiLine"  ></asp:textbox>
                            </td>
                        </tr>
                    </table>
                </div>  
                <table style="width: 100%">
                    <tr>
                        <td style="text-align: right">
                            
                                    <asp:Button ID="btn_rk" runat="server" 
                                        class="btn btn-primary" Text="入库" 
                                        onclick="btn_rk_Click"  />&nbsp;&nbsp;<input
                    id="btnPost" type="button" value="button" hidden="hidden" />
                        </td>
                        <td style="text-align: left">
                           <input id="btnBack" class="btn btn-primary" onclick="closewin();" type="button" value="返回" />
                        </td>
                    </tr>
                </table>
              
                
                
            </div>
        </div>

    </div>
</asp:Content>

