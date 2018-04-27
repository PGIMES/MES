<%@ Page Title="【产品组分类维护】" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Product_GroupMatain.aspx.cs" Inherits="GongCheng_Product_GroupMatain" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <script src="../Content/js/jquery.min.js" type="text/javascript"></script>
    <script src="../Content/js/bootstrap.min.js" type="text/javascript"></script>
    <script src="../Content/js/plugins/layer/layer.min.js" type="text/javascript"></script>
     <script type="text/javascript" language="javascript">

         $("#mestitle").text("【产品组分类维护】");
        </script>
    <style type="text/css">
        .auto-style1
        {
            height: 69px;
        }
        
        .row
        {
            margin-right: 2px;
            margin-left: 2px;
        }
        
        /*.row-container {
            padding-left: 2px;
            padding-right: 2px;
        }*/
        
        fieldset
        {
            background: rgba(255,255,255,.3);
            border-color: lightblue;
            border-style: solid;
            border-width: 2px;
            -moz-border-radius: 5px;
            -webkit-border-radius: 5px;
            -khtml-border-radius: 5px;
            border-radius: 5px;
            line-height: 30px;
            list-style: none;
            padding: 5px 10px;
            margin-bottom: 2px;
        }
        
        legend
        {
            color: #302A2A;
            font: bold 16px/2 Verdana, Geneva, sans-serif;
            font-weight: bold;
            text-align: left; /*text-shadow: 2px 2px 2px rgb(88, 126, 156);*/
        }
        fieldset
        {
            padding: .35em .625em .75em;
            margin: 0 2px;
            border: 1px solid lightblue;
        }
        legend
        {
            border-style: none;
            border-color: inherit;
            border-width: 0;
            padding: 5px;
            width: 108px;
            margin-bottom: 2px;
        }
        .panel
        {
            margin-bottom: 5px;
        }
        .panel-heading
        {
            padding: 5px 5px 5px 5px;
        }
        .panel-body
        {
            padding: 5px 5px 5px 5px;
        }
        body
        {
            margin-left: 5px;
            margin-right: 5px;
        }
        .col-lg-1, .col-lg-10, .col-lg-11, .col-lg-12, .col-lg-2, .col-lg-3, .col-lg-4, .col-lg-5, .col-lg-6, .col-lg-7, .col-lg-8, .col-lg-9, .col-md-1, .col-md-10, .col-md-11, .col-md-12, .col-md-2, .col-md-3, .col-md-4, .col-md-5, .col-md-6, .col-md-7, .col-md-8, .col-md-9, .col-sm-1, .col-sm-10, .col-sm-11, .col-sm-12, .col-sm-2, .col-sm-3, .col-sm-4, .col-sm-5, .col-sm-6, .col-sm-7, .col-sm-8, .col-sm-9, .col-xs-1, .col-xs-10, .col-xs-11, .col-xs-12, .col-xs-2, .col-xs-3, .col-xs-4, .col-xs-5, .col-xs-6, .col-xs-7, .col-xs-8, .col-xs-9
        {
            position: relative;
            min-height: 1px;
            padding-right: 1px;
            padding-left: 1px;
            margin-top: 10px;
        }
        .auto-style2
        {
            height: 20px;
        }
    </style>

  

     <script type="text/javascript" >
    var popupwindow = null;
    function Getzy() {
 
        popupwindow = window.open('../Select/select_XMH.aspx?id=zy', '_blank', 'height=400,width=600,resizable=no,menubar=no,scrollbars =no,location=no');
    }
    function zy_setvalue(formName, xmh, ljh, gxh) {
          ctl01.<%=txt_xm.ClientID%>.value = xmh;
   
  
       popupwindow.close();
    }


</script>    

 <script type="text/javascript" >
    var popupwindow = null;
    function Getbz() {
 
        popupwindow = window.open('../Select/select_XMH.aspx', '_blank', 'height=400,width=600,resizable=no,menubar=no,scrollbars =no,location=no');
    }
    function bz_setvalue(formName, xmh, ljh, gxh) {
          ctl01.<%=txt_bzdj.ClientID%>.value = xmh;
   
  
       popupwindow.close();
    }


</script>    


     
    <div class="row row-container">
        <div class="col-md-12 ">
            <div class="panel panel-info">
                 <div class="panel-heading" data-toggle="collapse" data-target="#XSZL">
                    <strong>产品组分类</strong> <span class="caret"></span>
                </div>
                 <div class="panel-body ">
                    <div class="col-xs-12 col-sm-12  col-md-12 col-lg-12">
                    <div>
                        <table style=" width:50%">
                            <tr>
                                <td>公司别:
                                </td>
                                <td>
                               
                                    <asp:DropDownList ID="ddl_site" runat="server" class="form-control input-s-sm ">
                                        <asp:ListItem>200</asp:ListItem>
                                        <asp:ListItem>100</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td>产品组:
                                </td>
                                <td>
                                    <asp:TextBox ID="txt_ProGroup" class="form-control input-s-sm" runat="server" Width="150px" ></asp:TextBox>
                                </td>
                      
                                <td>
                                    <asp:Button ID="btn_Group_confirm" runat="server" Text="确认维护"
                                        class="btn btn-large btn-primary"
                                        Width="100px" onclick="btn_Group_confirm_Click"  />

                                </td>
                               
                                <td>
                                    <asp:Button ID="btn_Group_query" runat="server" Text="查询"
                                        class="btn btn-large btn-primary"
                                        Width="100px" onclick="btn_Group_query_Click"  />

                                </td>
                               
                            </tr>
                            <tr>
                                <td colspan="8" style="text-align: center">
                                    &nbsp;</td>
                                 
                            </tr>

                                <tr>
                                        <td colspan="7">
                                            <asp:Panel ID="Panel1" runat="server" GroupingText="产品组清单">
                                                <asp:GridView ID="gv_Group" runat="server" 
                                                    AutoGenerateColumns="False" Width="100%" 
                                                    DataKeyNames="id" onrowediting="gv_Group_RowEditing" 
                                                    onrowupdating="gv_Group_RowUpdating" 
                                                    onrowcancelingedit="gv_Group_RowCancelingEdit" 
                                                    onrowdeleting="gv_Group_RowDeleting">
                                                    <Columns>
                                                        <asp:BoundField DataField="id">
                                                        <ControlStyle CssClass="hidden" Width="0px" />
                                                        <FooterStyle CssClass="hidden" Width="0px" />
                                                        <HeaderStyle CssClass="hidden" Width="0px" />
                                                        <ItemStyle CssClass="hidden" Width="0px" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="comp" HeaderText="公司">
                                                        <HeaderStyle BackColor="#C1E2EB" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="DJ_Group" HeaderText="产品组" >
                                                        <HeaderStyle BackColor="#C1E2EB" />
                                                        </asp:BoundField>
                                                        <asp:CommandField HeaderText="编辑" ShowEditButton="True">
                                                        <HeaderStyle BackColor="#C1E2EB" />
                                                        </asp:CommandField>
                                                        <asp:CommandField HeaderText="删除" ShowDeleteButton="True">
                                                        <HeaderStyle BackColor="#C1E2EB" />
                                                        </asp:CommandField>
                                                    </Columns>
                                                    <EmptyDataTemplate>
                                                        查无产品组明细，请选择条件重新查询.
                                                    </EmptyDataTemplate>
                                                </asp:GridView>
                                            </asp:Panel>
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
        <div class="col-md-12 ">
            <div class="panel panel-info">
               <div class="panel-heading" data-toggle="collapse" data-target="#XSXM">
                    <strong>产品组项目及类别</strong> <span class="caret"></span>
                </div>
                <div class="panel-body " id="XSXM">
                    <div class="col-xs-12 col-sm-12  col-md-12 col-lg-12">
                        <table style=" width:50%">
                            <tr>
                                <td>公司别:
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlcomp" runat="server" class="form-control input-s-sm ">
                                        <asp:ListItem>200</asp:ListItem>
                                        <asp:ListItem>100</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td>组名:
                                </td>
                                
                                 <td>
                                    <asp:DropDownList ID="ddlproduct" runat="server" 
                                        class="form-control input-s-sm " >
                                    </asp:DropDownList>
                                </td>
                                <td>组员：
                                </td>
                                <td>
                                 <div class="form-inline" style="color: #FF0000">
                                    <input id="txt_xm" runat="server" 
                                        class="input form-control input-s-sm" 
                                        style="height: 35px; width: 150px" 
                                         /> <img name="selectxmh" style="border: 0px;" src="../images/fdj.gif"
                                                alt="select" onclick="Getzy()" /></div></td>
                                <td style=" display:none">标准刀具清单项目号:
                                </td>
                              <td style=" display:none">
                                 <div class="form-inline" style="color: #FF0000">
                                    <input id="txt_bzdj" runat="server" 
                                        class="input form-control input-s-sm" 
                                        style="height: 35px; width: 150px" 
                                         /> <img name="selectxmh" style="border: 0px;" src="../images/fdj.gif"
                                                alt="select" onclick="Getbz()" /></div></td>
                                
                                
                               
                                
                                
                            </tr>
                            
                            <tr>
                                <td>&nbsp;</td>
                                <td>
                                    &nbsp;</td>
                                <td>&nbsp;</td>
                                
                                  <td>                                    
                                    <asp:Button ID="btn_xm_confirm" runat="server" Text="确认维护"
                                        class="btn btn-large btn-primary"
                                        Width="100px" onclick="btn_xm_confirm_Click"  />

                                </td>
                                <td>&nbsp;</td>
                                <td> 
                                    <asp:Button ID="Button2" runat="server" Text="查询"
                                        class="btn btn-large btn-primary"
                                        Width="100px" onclick="Button2_Click"  /> </td>
                                <td>&nbsp;</td>
                              <td>
                                  &nbsp;</td>
                                
                                
                                <td>                                    
                                    &nbsp;</td>
                                <td> 
                                    &nbsp;</td>
                                
                            </tr>
                            
                              <tr>
                                        <td colspan="8">
                                            <asp:Panel ID="Panel2" runat="server" 
                                                GroupingText="产品组明细"  Width="1300px">
                                                <asp:GridView ID="gv_xmh" runat="server" 
                                                    AutoGenerateColumns="False" Width="100%" 
                                                    DataKeyNames="id" onrowediting="gv_xmh_RowEditing" 
                                                    onrowupdating="gv_xmh_RowUpdating" 
                                                    onrowcancelingedit="gv_xmh_RowCancelingEdit" 
                                                    onrowdeleting="gv_xmh_RowDeleting">
                                                    <Columns>
                                                        <asp:BoundField DataField="id">
                                                        <ControlStyle CssClass="hidden" Width="0px" />
                                                        <FooterStyle CssClass="hidden" Width="0px" />
                                                        <HeaderStyle CssClass="hidden" Width="0px" />
                                                        <ItemStyle CssClass="hidden" Width="0px" />
                                                        </asp:BoundField>
                                                        <asp:TemplateField HeaderText="公司">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="comp" runat="server"  class="form-control input-s-sm" Width="200px" ReadOnly="True"
                                                                    Text='<%# Bind("comp") %>'></asp:TextBox>
                                                            </ItemTemplate>
                                                            <HeaderStyle BackColor="#C1E2EB" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="组员">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="xmh" runat="server"   class="form-control input-s-sm" Width="200px"
                                                                    Text='<%# Bind("xmh") %>' ReadOnly="True"></asp:TextBox>
                                                            </ItemTemplate>
                                                            <HeaderStyle BackColor="#C1E2EB" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="组名">
                                                           
                                                            <ItemTemplate>
                                                                <asp:DropDownList ID="ddl_prod" runat="server"  class="form-control input-s-sm " 
                                                                >
                                                                </asp:DropDownList>
                                                            </ItemTemplate>
                                                            <HeaderStyle BackColor="#C1E2EB" />
                                                        </asp:TemplateField>
                                                        <asp:CommandField HeaderText="编辑" ShowEditButton="True">
                                                        <HeaderStyle BackColor="#C1E2EB" />
                                                        </asp:CommandField>
                                                        <asp:CommandField HeaderText="删除" ShowDeleteButton="True">
                                                        <HeaderStyle BackColor="#C1E2EB" />
                                                        </asp:CommandField>
                                                    </Columns>
                                                    <EmptyDataTemplate>
                                                        查无产品组项目明细.
                                                    </EmptyDataTemplate>
                                                </asp:GridView>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                        </table>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <br />
    </asp:Content>
