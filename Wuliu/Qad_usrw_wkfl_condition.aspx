<%@ Page Title="【福特标签浏览】" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Qad_usrw_wkfl_condition.aspx.cs" Inherits="Wuliu_Qad_usrw_wkfl_condition" %>

<%@ Register Assembly="DevExpress.Web.v17.2, Version=17.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <script type="text/javascript" src="/Content/js/jquery.min.js"></script>
    <script src="/Content/js/plugins/layer/laydate/laydate.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#mestitle").text("【福特标签浏览】");
             setHeight();

             $(window).resize(function () {
                 setHeight();
             });
                 
        });

        function setHeight() {
            $("div[class=dxgvCSD]").css("height", ($(window).height() - $("#div_p").height() - 150) + "px");
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

    <div class="col-md-12" id="div_p"  style="margin-bottom:5px">
        <table style=" border-collapse: collapse;">
            <tr>
                <td style="width:30px;">域</td>
                <td style="width:125px;">
                    <asp:DropDownList ID="ddl_domain" runat="server" class="form-control input-s-md " Width="120px">
                        <asp:ListItem Value="200">200</asp:ListItem>
                        <asp:ListItem Value="100">100</asp:ListItem>
                    </asp:DropDownList>
                </td>     
                <td style="width:65px;">生效日期</td>
                <td style="width:125px;">
                    <asp:TextBox ID="StartDate" runat="server" class="form-control" Width="120px" 
                        onclick="laydate({type: 'date',format: 'YYYY/MM/DD',min:'2018/11/01',choose: function(dates){}});" /><%--start:'2018/11/01',--%>
                </td>
                <td style="width:15px;">~</td>
                <td style="width:125px;">
                    <asp:TextBox ID="EndDate" runat="server" class="form-control" Width="120px"
                        onclick="laydate({type: 'date',format: 'YYYY/MM/DD',choose: function(dates){}});" />
                </td>
                <td id="td_btn">  
                    <button id="btn_search" type="button" class="btn btn-primary btn-large" runat="server" onserverclick="btn_search_Click"><i class="fa fa-search fa-fw"></i>&nbsp;查询</button>  
                    <button id="btn_export" type="button" class="btn btn-primary btn-large" runat="server" onserverclick="btn_export_Click"><i class="fa fa-download fa-fw"></i>&nbsp;导出</button>
                </td>
            </tr>                      
        </table>                   
    </div>
    
    <div class="col-sm-12">
        <table>
            <tr>
                <td>
                    <dx:ASPxGridView ID="gv" runat="server" KeyFieldName="id" AutoGenerateColumns="False" Width="830px" OnPageIndexChanged="gv_PageIndexChanged"  ClientInstanceName="grid">
                        <ClientSideEvents EndCallback="function(s, e) {setHeight();}"  />
                        <SettingsPager PageSize="100" ></SettingsPager>
                        <Settings ShowFilterRow="True" ShowGroupPanel="false" ShowFilterRowMenu="True" ShowFilterRowMenuLikeItem="True" AutoFilterCondition="Contains" 
                            VerticalScrollBarMode="Visible" VerticalScrollBarStyle="Standard" VerticalScrollableHeight="600"  />
                        <SettingsBehavior AllowFocusedRow="True" ColumnResizeMode="Control" />
                        <Columns>       
                            <dx:GridViewDataDateColumn Caption="生效日期" FieldName="tr_effdate" Width="120px" VisibleIndex="1">
                                <PropertiesDateEdit DisplayFormatString="yyyy/MM/dd"></PropertiesDateEdit>
                            </dx:GridViewDataDateColumn> 
                            <dx:GridViewDataTextColumn Caption="订单号" FieldName="tr_nbr" Width="100px" VisibleIndex="2"></dx:GridViewDataTextColumn>    
                            <dx:GridViewDataTextColumn Caption="参考号" FieldName="tr_ref" Width="100px" VisibleIndex="3"></dx:GridViewDataTextColumn> 
                            <dx:GridViewDataTextColumn Caption="数量" FieldName="tr_qty_loc" Width="80px" VisibleIndex="4">
                                <PropertiesTextEdit DisplayFormatString="{0:N0}"></PropertiesTextEdit>
                            </dx:GridViewDataTextColumn>   
                            <dx:GridViewDataTextColumn Caption="物料号" FieldName="tr_part" Width="100px" VisibleIndex="5"></dx:GridViewDataTextColumn> 
                            <dx:GridViewDataTextColumn Caption="客户零件号" FieldName="pt_desc1" Width="150px" VisibleIndex="6"></dx:GridViewDataTextColumn>   
                            <dx:GridViewDataTextColumn Caption="标签号" FieldName="usrw_key2_part" Width="90px" VisibleIndex="7"></dx:GridViewDataTextColumn>    
                            <dx:GridViewDataTextColumn Caption="标签数量" FieldName="usrw_decfld[2]" Width="80px" VisibleIndex="8">
                                <PropertiesTextEdit DisplayFormatString="{0:N0}"></PropertiesTextEdit>
                            </dx:GridViewDataTextColumn> 
                                          
                            
                        </Columns>
                        <Styles>
                            <Header BackColor="#99CCFF"></Header>
                            <FocusedRow BackColor="#99CCFF" ForeColor="#0000CC"></FocusedRow>
                            <Footer HorizontalAlign="Right"></Footer>
                        </Styles>
                    </dx:ASPxGridView>
                    <%--<dx:ASPxGridViewExporter ID="ASPxGridViewExporter1" runat="server" GridViewID="gv">
                    </dx:ASPxGridViewExporter>--%>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>

