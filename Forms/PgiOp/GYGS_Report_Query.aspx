<%@ Page Title="工艺工时查询" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="GYGS_Report_Query.aspx.cs" Inherits="Forms_PgiOp_GYGS_Report_Query" %>

<%@ Register Assembly="DevExpress.Web.v17.2, Version=17.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <script type="text/javascript" src="/Content/js/jquery.min.js"></script>
    <script type="text/javascript" src="/Content/js/jquery.cookie.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#mestitle").text("【工艺工时查询】");

            $('#btn_add').click(function () {
                window.open('/Platform/WorkFlowRun/Default.aspx?flowid=a7ec8bec-1f81-4a81-81d2-a9c7385dedb7&appid=13093704-4425-4713-B3E1-81851C6F96CD')
            });

            $('#btn_edit').click(function () {

            });           
        });

    </script>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

    <div class="col-md-12" >
        <div class="row  row-container">            
            <div class="panel panel-info">
                <div class="panel-heading" data-toggle="collapse" data-target="#CPXX">
                    <strong>工艺工时查询</strong>
                </div>
                <div class="panel-body collapse in" id="CPXX" >
                    <div class="col-xs-12 col-sm-12  col-md-12 col-lg-12" style="font-size:13px;">
                        <table class="tblCondition" style=" border-collapse: collapse;">
                            <tr>
                                <td style="width:70px;">PGI项目号</td>
                                <td style="width:130px;">
                                    <asp:TextBox ID="txt_pgi_no" class="form-control input-s-sm" runat="server" Width="120px"></asp:TextBox>
                                </td>
                                <td style="width:70px;">客户零件号</td>
                                <td style="width:130px;">
                                    <asp:TextBox ID="txt_pn" class="form-control input-s-sm" runat="server" Width="120px"></asp:TextBox>
                                </td>    
                                <td style="width:70px;">当前版本</td>
                                <td style="width:130px;"> 
                                    <asp:DropDownList ID="ddl_ver" runat="server" class="form-control input-s-md " Width="120px">
                                        <asp:ListItem Value="">ALL</asp:ListItem>
                                        <asp:ListItem Value="当前" Selected="True">当前</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td style="width:70px;">工艺段</td>
                                <td style="width:130px;"> 
                                    <asp:DropDownList ID="ddl_typeno" runat="server" class="form-control input-s-md " Width="120px">
                                        <asp:ListItem Value="">ALL</asp:ListItem>
                                        <asp:ListItem Value="压铸">压铸</asp:ListItem>
                                        <asp:ListItem Value="机加">机加</asp:ListItem>
                                        <asp:ListItem Value="质量">质量</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td>  
                                    &nbsp;&nbsp;
                                    <button id="btn_search" type="button" class="btn btn-primary btn-large" runat="server" onserverclick="btn_search_Click"><i class="fa fa-search fa-fw"></i>&nbsp;查询</button>    
                                    <button id="btn_add" type="button" class="btn btn-primary btn-large"><i class="fa fa-plus fa-fw"></i>&nbsp;新增</button>  
                                    <button id="btn_edit" type="button" class="btn btn-primary btn-large"><i class="fa fa-pencil-square-o fa-fw"></i>&nbsp;编辑</button> 
                                    <button id="btn_import" type="button" class="btn btn-primary btn-large" runat="server" onserverclick="btn_import_Click"><i class="fa fa-download fa-fw"></i>&nbsp;导出</button>
                                </td>
                            </tr>                      
                        </table>
                    </div>
                </div>
            </div>            
        </div>
    </div>

    <div class="col-sm-12">
        <table>
            <tr>
                <td><%-- OnHtmlDataCellPrepared="gv_HtmlDataCellPrepared"--%>
                    <dx:ASPxGridView ID="gv" runat="server" KeyFieldName="" AutoGenerateColumns="False" Width="2090px" OnPageIndexChanged="gv_PageIndexChanged" >
                        <SettingsPager PageSize="1000" ></SettingsPager>
                        <Settings ShowFilterRow="True" ShowGroupPanel="false" ShowFilterRowMenu="True" ShowFilterRowMenuLikeItem="True" AutoFilterCondition="Contains"  />
                        <SettingsBehavior AllowFocusedRow="True" ColumnResizeMode="Control" />
                        <Columns>
                            <%--<dx:GridViewCommandColumn SelectAllCheckboxMode="Page" ShowClearFilterButton="true" ShowSelectCheckbox="true" Name="Sel" Width="40" VisibleIndex="1"></dx:GridViewCommandColumn>--%>
                          <%--  <dx:GridViewDataTextColumn Caption="项目号" FieldName="pgi_no" Width="60px" VisibleIndex="2">
                                <Settings AllowCellMerge="True" />--%>
                                <%--<DataItemTemplate>
                                    <dx:ASPxHyperLink ID="hpl_pgi_no" runat="server" Text='<%# Eval("pgi_no")%>' Cursor="pointer" ClientInstanceName='<%# "pgi_no"+Container.VisibleIndex.ToString() %>'
                                         NavigateUrl="" >
                                    </dx:ASPxHyperLink>
                                </DataItemTemplate>--%>
                            <%--</dx:GridViewDataTextColumn>--%>
                            <dx:GridViewDataTextColumn Caption="项目号" FieldName="pgi_no_t" Width="100px" VisibleIndex="1">
                                <Settings AllowCellMerge="True" />  
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="零件号" FieldName="pn" Width="120px" VisibleIndex="2">
                                <Settings AllowCellMerge="True" />
                            </dx:GridViewDataTextColumn>
                            
                            <dx:GridViewDataTextColumn Caption="工艺路<br />线版本" FieldName="ver" Width="50px" VisibleIndex="2"></dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="域" FieldName="domain" Width="40px" VisibleIndex="2"></dx:GridViewDataTextColumn>
                            <%--<dx:GridViewDataTextColumn Caption="工艺段" FieldName="typeno" Width="50px" VisibleIndex="2">--%>
                                <%--<Settings AllowCellMerge="True" />--%>
                            <%--</dx:GridViewDataTextColumn>--%>
                            <%--<dx:GridViewDataTextColumn Caption="工艺流程" FieldName="pgi_no_t" Width="80px" VisibleIndex="3">
                                <Settings AllowCellMerge="True" />  
                            </dx:GridViewDataTextColumn>--%>
                            <dx:GridViewDataTextColumn Caption="工序号" FieldName="op" Width="60px" VisibleIndex="4"></dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="工序名称" FieldName="op_desc" Width="140px" VisibleIndex="5"></dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="工序说明" FieldName="op_remark" Width="140px" VisibleIndex="6"></dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="设备<br />(工作中心名称)" FieldName="gzzx_desc" Width="100px" VisibleIndex="7"></dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="工作中心<br />代码" FieldName="gzzx" Width="55px" VisibleIndex="8"></dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="是否报工<br />(Y/N)" FieldName="IsBg" Width="55px" VisibleIndex="9"></dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="每次加工<br />数量" FieldName="JgNum" Width="55px" VisibleIndex="10"></dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="加工时长<br />(秒)" FieldName="JgSec" Width="55px" VisibleIndex="12"></dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="设备等待<br />时间(秒)" FieldName="WaitSec" Width="55px" VisibleIndex="12"></dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="装夹时间<br />(秒)" FieldName="ZjSecc" Width="55px" VisibleIndex="13"></dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="机器台数" FieldName="JtNum" Width="55px" VisibleIndex="14"></dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="单台单件<br />工序工时(秒)" FieldName="TjOpSec" Width="70px" VisibleIndex="15">
                                <PropertiesTextEdit DisplayFormatString="{0:N2}"></PropertiesTextEdit>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="单件工时<br />(秒)" FieldName="JSec" Width="55px" VisibleIndex="16">
                                <PropertiesTextEdit DisplayFormatString="{0:N2}"></PropertiesTextEdit>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="单件工时<br />(小时)" FieldName="JHour" Width="65px" VisibleIndex="17">
                                    <PropertiesTextEdit DisplayFormatString="{0:N5}"></PropertiesTextEdit>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="单台<br />需要人数" FieldName="col1" Width="55px" VisibleIndex="18"></dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="本工序一人<br />操作台数" FieldName="col2" Width="65px" VisibleIndex="19"></dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="单台85%<br />产量" FieldName="col3" Width="65px" VisibleIndex="20"></dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="一人85%<br />产量" FieldName="col4" Width="65px" VisibleIndex="21"></dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="整线班产量" FieldName="col5" Width="65px" VisibleIndex="22"></dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="单人报工<br />数量" FieldName="col6" Width="65px" VisibleIndex="23"></dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="单人产出<br />工时" FieldName="col7" Width="55px" VisibleIndex="24">
                                <PropertiesTextEdit DisplayFormatString="{0:N2}"></PropertiesTextEdit>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="流程状态" FieldName="" Width="80px" VisibleIndex="25"></dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="创建时间" FieldName="createdate" Width="100px" VisibleIndex="26"></dx:GridViewDataTextColumn>
                        </Columns>
                        <Styles>
                            <Header BackColor="#99CCFF"></Header>
                            <FocusedRow BackColor="#99CCFF" ForeColor="#0000CC"></FocusedRow>
                            <Footer HorizontalAlign="Right"></Footer>
                        </Styles>
                    </dx:ASPxGridView>
                    <dx:ASPxGridViewExporter ID="ASPxGridViewExporter1" runat="server">
                    </dx:ASPxGridViewExporter>
                </td>
            </tr>
        </table>
    </div>
    


</asp:Content>

