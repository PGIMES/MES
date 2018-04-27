<%@ Page Title="【刀具结转】" Language="C#"  MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="DJ_JZ.aspx.cs" Inherits="DJ_DJ_JZ" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">

<style>
    .gvHeader th{    background: #C1E2EB;    color:  Brown;    border: solid 1px #333333;    padding:0px 5px 0px 5px; font-size:12px;}
   
</style>
    <script src="../Content/js/jquery.min.js" type="text/javascript"></script>
    <script src="../Content/js/plugins/layer/layer.min.js" type="text/javascript"></script>
 <script type="text/javascript" language="javascript">

     $("#mestitle").text("【刀具结转】");
        </script>
    <div class="row row-container">
        <div class="col-sm-12 ">
            <div class="panel panel-info">
                <div class="panel-heading">
                    <strong>刀具结转</strong>
                    <asp:ScriptManager ID="ScriptManager1" runat="server">
                    </asp:ScriptManager>
                </div>
                
                <div class="panel-body">
                    <div class="col-sm-12">
                       
                                <table>
                                    
                                    <tr>
                                      <td>公司</td>
                                      <td> 
                                      <asp:DropDownList ID="ddl_comp" runat="server" class="form-control input-s-sm ">
                                          <asp:ListItem>200</asp:ListItem>
                                          <asp:ListItem>100</asp:ListItem>
                                           </asp:DropDownList></td>  
                                        <td>
                                            年份:
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="txt_year" runat="server" class="form-control input-s-sm " >
                                            </asp:DropDownList>
                                        </td>
                                          <td> 结转月： </td>
                                        <td>
                    <asp:DropDownList ID="txt_mnth" runat="server" 
                        class="form-control input-s-sm" >
                    </asp:DropDownList>
                </td><td> 项目号:</td>
                                        <td>
                                            <input id="txt_xmh" runat="server" 
                                                class="form-control input-s-sm" style=" width:100px" /></td>
                                        <td>
                                            物料号:</td>
                                        <td>
                                            <input id="txt_part" runat="server" 
                                                class="form-control input-s-sm" style=" width:100px" /></td>
                        <td align="right">
                                            <asp:Button ID="Button1" runat="server" Text="结转"  
                                              class="btn btn-large btn-primary disabled"  Visible=false
                                              onclick="Button1_Click" Width="100px" />
                                        </td>
                                       
                                   <td style=" width:50px"> 
                                       <asp:Button ID="btnNext" runat="server" 
                                           onclick="btnNext_Click" style="display: none" Text="Next" />
                                        </td>
                        <td align="right">
                                            <asp:Button ID="Button2" runat="server" Text="查询"  
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
           <div  runat="server" id="DIV1"  style=" margin-left:5px" >
        
                       
                                <asp:Panel ID="Panel2" runat="server" Height="100%" >
                                    <table style=" background-color: #FFFFFF;" 
                                      >
                                      
                                        <tr>
                                            <td valign="top">
                                                <asp:GridView ID="GridView1" runat="server"  CssClass="gvHeader"
                                                    AllowPaging="True" 
                                                  
                                                     Width="1000px" 
                                                    ShowFooter="True" 
                                                    PageSize="100" 
                                                    onpageindexchanging="GridView1_PageIndexChanging" >
                                                    <PagerSettings FirstPageText="首页" LastPageText="尾页" 
                                                        Mode="NextPreviousFirstLast" NextPageText="下页" 
                                                        PreviousPageText="上页" />
                                                    <PagerStyle ForeColor="Red" />
                                                   
                                                </asp:GridView>
                                                <br />
                                                <br />
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                                </div>
</asp:Content>

