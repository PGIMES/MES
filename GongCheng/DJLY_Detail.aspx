<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="DJLY_Detail.aspx.cs" Inherits="GongCheng_DJLY_Detail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
<script src="../Content/js/jquery.min.js" type="text/javascript"></script>
    <script src="../Content/js/plugins/layer/layer.min.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            $("#mestitle").text("【刀具领用查询】");


        });
       
        
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
                            <td>公司</td>
                            <td><asp:DropDownList ID="ddl_comp" runat="server" class="form-control input-s-sm ">
                                          <asp:ListItem>200</asp:ListItem>
                                          <asp:ListItem>100</asp:ListItem>
                                           </asp:DropDownList></td>
                                
                                <td>产品大类</td>
                            <td><asp:DropDownList ID="ddl_dl" runat="server" class="form-control input-s-sm ">
                                          <asp:ListItem></asp:ListItem>
                                          <asp:ListItem>铁</asp:ListItem>
                                          <asp:ListItem>铝</asp:ListItem>
                                           </asp:DropDownList></td>
                                <td>项目号</td>
                                <td><asp:TextBox ID="txt_xmh" runat="server" class="form-control input-s-sm"  />
                                    
                                    </td>
                                    <td>物料号</td>
                                <td><asp:TextBox ID="txt_part" runat="server" class="form-control input-s-sm"  />
                                    
                                    </td>
                                    <td>领用日期区间</td>
                                <td>
                                    <asp:TextBox ID="txt_startdate" runat="server" Width="100" />
                                    <ajaxToolkit:CalendarExtender ID="txt_startdate_CalendarExtender" runat="server"
                                        PopupButtonID="Image2" Format="yyyy/MM/dd" TargetControlID="txt_startdate" />
                                    ~&nbsp;<asp:TextBox ID="txt_enddate" runat="server" Width="100" />
                                    <ajaxToolkit:CalendarExtender ID="txt_enddate_CalendarExtender" runat="server" PopupButtonID="Image2"
                                        Format="yyyy/MM/dd" TargetControlID="txt_enddate" />
                                </td>
                                <td align="right">
                                    <asp:Button ID="Button1" runat="server" Text="查询" class="btn btn-large btn-primary"
                                        OnClick="Button1_Click" Width="100px" />
                                   
                                   
                                </td>
                                <td width=50px></td>
                                  <td>
                                    <asp:Button ID="Button2" runat="server" Text="导出" class="btn btn-large btn-primary"
                                        OnClick="Button2_Click" Width="100px" />
                                   
                                   
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
        </div>
    </div>
   
    <div  id="DIV1" style="margin-left: 4px">
       
                        <asp:GridView ID="GridView1" runat="server" 
                            AllowPaging="True"  OnPageIndexChanging="GridView1_PageIndexChanging"
                            PageSize="100" Width="1300px"   
                            BorderColor="LightBlue">
                            <PagerSettings FirstPageText="首页" LastPageText="尾页" Mode="NextPreviousFirstLast" 
                                NextPageText="下页" PreviousPageText="上页"  />
                            <PagerStyle ForeColor="Red" />
                        </asp:GridView>
            
    </div>
</asp:Content>
