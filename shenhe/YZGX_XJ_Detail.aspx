<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="YZGX_XJ_Detail.aspx.cs" Inherits="shenhe_YZGX_XJ_Detail"  MaintainScrollPositionOnPostback="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <style type="text/css">
        td
        {
            padding-left: 2px;
            padding-right: 2px;
            padding-top: 2px;
        }
        th
        {
            text-align: center;
            padding-left: 2px;
            padding-right: 2px;
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
            margin-top: 0px;
        }
    </style>
    <script src="../Content/js/jquery.min.js" type="text/javascript"></script>

    <script type="text/javascript">
        $(document).ready(function () {
            $("#headTitle").remove();

        })//endready     
        
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">   
    <script src="../Content/js/jquery.min.js" type="text/javascript"></script>
    <script src="../Content/js/plugins/layer/layer.min.js" type="text/javascript"></script>      


    <div class=" panel panel-info  col-lg-12 ">
        <div class="panel panel-heading">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
                    </asp:ScriptManager>
         <asp:Label ID="lblName" runat="server" Text="检测类别："></asp:Label>   
            <asp:Label ID="LbMsg" runat="server" ForeColor="Red" 
                Text="请选择检测类别：末件/过程"></asp:Label>
        </div>
         <div class="panel-body">
                    <div class="col-sm-6 col-md-6">
                        <table style="width: 50%">
                            <tr>
                               
                                <td>
                                       <asp:DropDownList ID="dropjclb" 
                                        class="form-control input-s-sm" runat="server"
                                        AutoPostBack="True" onselectedindexchanged="dropjclb_SelectedIndexChanged" 
                                        >
                                        <asp:ListItem></asp:ListItem>
                                        <asp:ListItem>过程</asp:ListItem>
                                        <asp:ListItem>末件</asp:ListItem>
                                    </asp:DropDownList>
                               </td>

                            </tr>
                          
                            </table>
                    </div>

                </div>
                <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>--%>
        <div class="panel panel-body">        
            <asp:GridView ID="gvDetail" runat="server" 
                BorderColor="LightGray" 
                AutoGenerateColumns="False" 
                onrowdatabound="gvDetail_RowDataBound" >
                <Columns>
                    <asp:BoundField DataField="xuhao" HeaderText="序号" >
                    <HeaderStyle BackColor="#C1E2EB" />
                    </asp:BoundField>
                    <asp:BoundField DataField="jcxm" HeaderText="检测项目" >
                    <HeaderStyle BackColor="#C1E2EB" />
                    </asp:BoundField>
                    <asp:BoundField DataField="bzyq" HeaderText="标准要求" >
                    <HeaderStyle BackColor="#C1E2EB" />
                    <ItemStyle Width="250px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="sx" HeaderText="上限">
                    <HeaderStyle BackColor="#C1E2EB" />
                    </asp:BoundField>
                    <asp:BoundField DataField="xx" HeaderText="下限">
                    <HeaderStyle BackColor="#C1E2EB" />
                    </asp:BoundField>
                    <asp:BoundField DataField="pl" HeaderText="频次/时间" >
                    <HeaderStyle BackColor="#C1E2EB" />
                    </asp:BoundField>
                    <asp:BoundField DataField="xuehao" HeaderText="穴号" >
                    <HeaderStyle BackColor="#C1E2EB" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="检测类别">
                        <ItemTemplate>
                            <asp:TextBox ID="TextBox1" runat="server" Width="50px"  Text='<%#Eval("jg1_value") %>'
                                ontextchanged="TextBox1_TextChanged" 
                                AutoPostBack="True"></asp:TextBox>
                            <asp:TextBox ID="TextBox2" runat="server" Width="50px"   Text='<%#Eval("jg2_value") %>'
                                AutoPostBack="True" 
                                ontextchanged="TextBox2_TextChanged"></asp:TextBox>
                            <asp:TextBox ID="TextBox3" runat="server" Width="50px" Text='<%#Eval("jg3_value") %>' 
                                AutoPostBack="True" 
                                ontextchanged="TextBox3_TextChanged"></asp:TextBox>
                        </ItemTemplate>
                        <HeaderStyle BackColor="#C1E2EB" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="输入类型">
                        <ItemTemplate>
                            <asp:TextBox ID="input_type" runat="server" Text='<%#Eval("input_type") %>'></asp:TextBox>
                        </ItemTemplate>
                        <ControlStyle CssClass="hidden" Width="0px" />
                        <FooterStyle CssClass="hidden" Width="0px" />
                        <HeaderStyle CssClass="hidden" Width="0px" />
                        <ItemStyle CssClass="hidden" Width="0px" />
                    </asp:TemplateField>
                    <asp:BoundField DataField="jg1_result" HeaderText="result1">
                    <ControlStyle CssClass="hidden" Width="0px" />
                    <FooterStyle CssClass="hidden" Width="0px" />
                    <HeaderStyle CssClass="hidden" Width="0px" />
                    <ItemStyle CssClass="hidden" Width="0px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="jg2_result" 
                        HeaderText="result2" >
                    <ControlStyle CssClass="hidden" Width="0px" />
                    <FooterStyle CssClass="hidden" Width="0px" />
                    <HeaderStyle CssClass="hidden" Width="0px" />
                    <ItemStyle CssClass="hidden" Width="0px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="jg3_result" 
                        HeaderText="result3" >
                    <ControlStyle CssClass="hidden" Width="0px" />
                    <FooterStyle CssClass="hidden" Width="0px" />
                    <HeaderStyle CssClass="hidden" Width="0px" />
                    <ItemStyle CssClass="hidden" Width="0px" />
                    </asp:BoundField>
                </Columns>
                <EmptyDataTemplate>暂无数据</EmptyDataTemplate>
            </asp:GridView>
        </div>

          <div 
                                           style="position:absolute;width: 50px;height: 50px;">
                                           <asp:Button ID="txtsave" runat="server" 
                                               class="btn btn-primary" onclick="txtsave_Click" 
                                               Style="height: 35px; width: 100px" Text="提交" Width="70px" />
                                       </div>
       <%-- </ContentTemplate></asp:UpdatePanel>--%>
    </div>
    
</asp:Content>
