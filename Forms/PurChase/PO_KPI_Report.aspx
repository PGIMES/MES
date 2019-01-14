<%@ Page Title="【采购工程师KPI查询】" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="PO_KPI_Report.aspx.cs" Inherits="Forms_PurChase_PO_KPI_Report" %>

<%@ Register Assembly="DevExpress.Web.v17.2, Version=17.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <script type="text/javascript" src="/Content/js/jquery.min.js"></script>
    <script src="/Content/js/layer/layer.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#mestitle").text("【采购工程师KPI查询】");
             setHeight();

             $(window).resize(function () {
                 setHeight();
             });
        });

        function Bind_Month(workcode, year, lastname) {
            //alert(workcode); alert(year);alert(lastname);
            
            $("#div_month").css("display", "inline-block");
            $("#span_workcode").text(workcode); $("#span_lastname").text(lastname);
            $("#span_year").text(year);
            grid_EmpYear.PerformCallback(workcode + '|' + year);
        }

        function Bind_Day(month) {
            $("#div_day").css("display", "inline-block");
            $("#span_workcode_day").text($("#span_workcode").text()); $("#span_lastname_day").text($("#span_lastname").text());
            $("#span_year_day").text($("#span_year").text()); $("#span_month_day").text(month);
            grid_EmpMonth.PerformCallback($("#span_workcode_day").text() + '|' + $("#span_year_day").text() + '|' + month);
        }

        function setHeight() {
            $("#MainContent_gv div[class=dxgvCSD]").css("height", "210px");
            $("#MainContent_gv_EmpYear div[class=dxgvCSD]").css("height", "160px");
            $("#MainContent_gv_EmpMonth div[class=dxgvCSD]").css("height", "160px");
        }

        function checkAuth() {
            //不关闭页面
            //layer.alert("您没有权限查询此页面，请知悉！");

            //关闭页面
            layer.open({
                content: '您没有权限查询此页面，请知悉！',
                yes: function (index, layero) {
                    layer.close(index);
                    window.close();
                }
            });
        }
        	
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

    <div class="col-md-12" id="div_p"  style="margin-bottom:5px">
        <table style=" border-collapse: collapse;">
            <tr>
                <td style="width:35px;">年</td>
                <td style="width:100px;">
                    <asp:DropDownList ID="ddl_year" runat="server" class="form-control input-s-md " Width="90px">
                        <asp:ListItem Value="">All</asp:ListItem>
                        <asp:ListItem Value="2018">2018</asp:ListItem>
                        <asp:ListItem Value="2019">2019</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td style="width:85px;">采购工程师</td>
                <td style="width:125px;">  
                    <asp:DropDownList ID="ddl_buyname" runat="server" class="form-control input-s-md " Width="120px">
                    </asp:DropDownList>
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
                    <dx:ASPxGridView ID="gv" runat="server" KeyFieldName="" AutoGenerateColumns="False" Width="900px" ClientInstanceName="grid">
                        <ClientSideEvents EndCallback="function(s, e) {setHeight();}"  />
                        <SettingsPager PageSize="100" ></SettingsPager>

                        <%--ShowFilterRow="True" ShowGroupPanel="false" ShowFilterRowMenu="True" ShowFilterRowMenuLikeItem="True" AutoFilterCondition="Contains" --%>
                     <%--   <Settings VerticalScrollBarMode="Visible" VerticalScrollBarStyle="Standard" VerticalScrollableHeight="600"  />    --%>                    
                        <SettingsBehavior AllowFocusedRow="True" ColumnResizeMode="Control"/>
                        <Columns>                        
                            <dx:GridViewDataTextColumn Caption="采购工程师" FieldName="lastname" Width="120px" VisibleIndex="1" ></dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="年份" FieldName="year" Width="60px" VisibleIndex="2">
                                <DataItemTemplate>
                                    <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text='<%# Eval("year")%>' 
                                        ClientSideEvents-Click='<%# "function(s,e){Bind_Month(\""+Eval("workcode")+"\",\""+Eval("year")+"\",\""+Eval("lastname")+"\");}" %>' 
                                        ForeColor="Blue" Font-Underline="true" Cursor="pointer">
                                    </dx:ASPxLabel>
                                </DataItemTemplate>
                            </dx:GridViewDataTextColumn> 
                            <dx:GridViewDataTextColumn Caption="总零星采购行数" FieldName="pur_rows" Width="120px" VisibleIndex="3"></dx:GridViewDataTextColumn>   
                            <dx:GridViewDataTextColumn Caption="完成数" FieldName="complete_rows" Width="120px" VisibleIndex="3"></dx:GridViewDataTextColumn>   
                            <dx:GridViewDataTextColumn Caption="完成数(及时)" FieldName="complete_rows_timely" Width="120px" VisibleIndex="4"></dx:GridViewDataTextColumn>     
                            <dx:GridViewDataTextColumn Caption="完成数(未及时)" FieldName="complete_rows_no_timely" Width="120px" VisibleIndex="5"></dx:GridViewDataTextColumn>     
                            <dx:GridViewDataTextColumn Caption="未完成数" FieldName="no_complete_rows" Width="120px" VisibleIndex="6"></dx:GridViewDataTextColumn>   
                            <dx:GridViewDataTextColumn Caption="及时率" FieldName="TimelyRate" Width="120px" VisibleIndex="7"></dx:GridViewDataTextColumn>  
                        </Columns>
                        <Styles>
                            <Header BackColor="#99CCFF"></Header>
                            <FocusedRow BackColor="#EDEDED" ForeColor="#000000"></FocusedRow>
                            <Footer HorizontalAlign="Right"></Footer>
                        </Styles>
                    </dx:ASPxGridView>
                </td>
            </tr>
        </table>
    </div>

    <div id="div_month" class="col-sm-12" style="margin-top:10px;display:none;">
        <table>
            <tr>
                <td id="td_month_title" style="font-weight:800;">
                    采购工程师：<span id="span_workcode" style="display:none;"></span><span id="span_lastname" style=" color:#008B8B"></span>
                    年份：<span id="span_year" style=" color:#008B8B"></span>
                </td>
            </tr>
            <tr>
                <td>
                    <dx:ASPxGridView ID="gv_EmpYear" runat="server" KeyFieldName="type" AutoGenerateColumns="False" Width="950px" ClientInstanceName="grid_EmpYear" OnCustomCallback="gv_EmpYear_CustomCallback"
                         OnHtmlRowCreated="gv_EmpYear_HtmlRowCreated">
                        <ClientSideEvents EndCallback="function(s, e) {setHeight();}"  />
                        <SettingsPager PageSize="100" ></SettingsPager>
                        <SettingsBehavior AllowFocusedRow="True" ColumnResizeMode="Control" AllowSort="false"/>
                        <Columns>                        
                            <dx:GridViewDataTextColumn Caption="统计明细项" FieldName="type" Width="100px" VisibleIndex="0"></dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="1" FieldName="1" Width="70px" VisibleIndex="1">
                                <HeaderCaptionTemplate>
                                    <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text='1'
                                        ClientSideEvents-Click='<%# "function(s,e){Bind_Day(1);}" %>' 
                                        ForeColor="Blue" Font-Underline="true" Cursor="pointer">
                                    </dx:ASPxLabel>
                                </HeaderCaptionTemplate>
                            </dx:GridViewDataTextColumn> 
                            <dx:GridViewDataTextColumn Caption="2" FieldName="2" Width="70px" VisibleIndex="2">
                                <HeaderCaptionTemplate>
                                    <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text='2'
                                        ClientSideEvents-Click='<%# "function(s,e){Bind_Day(2);}" %>' 
                                        ForeColor="Blue" Font-Underline="true" Cursor="pointer">
                                    </dx:ASPxLabel>
                                </HeaderCaptionTemplate>
                            </dx:GridViewDataTextColumn>   
                            <dx:GridViewDataTextColumn Caption="3" FieldName="3" Width="70px" VisibleIndex="3">
                                <HeaderCaptionTemplate>
                                    <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text='3'
                                        ClientSideEvents-Click='<%# "function(s,e){Bind_Day(3);}" %>' 
                                        ForeColor="Blue" Font-Underline="true" Cursor="pointer">
                                    </dx:ASPxLabel>
                                </HeaderCaptionTemplate>
                            </dx:GridViewDataTextColumn>   
                            <dx:GridViewDataTextColumn Caption="4" FieldName="4" Width="70px" VisibleIndex="4">
                                <HeaderCaptionTemplate>
                                    <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text='4'
                                        ClientSideEvents-Click='<%# "function(s,e){Bind_Day(4);}" %>' 
                                        ForeColor="Blue" Font-Underline="true" Cursor="pointer">
                                    </dx:ASPxLabel>
                                </HeaderCaptionTemplate>
                            </dx:GridViewDataTextColumn>     
                            <dx:GridViewDataTextColumn Caption="5" FieldName="5" Width="70px" VisibleIndex="5">
                                <HeaderCaptionTemplate>
                                    <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text='5'
                                        ClientSideEvents-Click='<%# "function(s,e){Bind_Day(5);}" %>' 
                                        ForeColor="Blue" Font-Underline="true" Cursor="pointer">
                                    </dx:ASPxLabel>
                                </HeaderCaptionTemplate>
                            </dx:GridViewDataTextColumn>     
                            <dx:GridViewDataTextColumn Caption="6" FieldName="6" Width="70px" VisibleIndex="6">
                                <HeaderCaptionTemplate>
                                    <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text='6'
                                        ClientSideEvents-Click='<%# "function(s,e){Bind_Day(6);}" %>' 
                                        ForeColor="Blue" Font-Underline="true" Cursor="pointer">
                                    </dx:ASPxLabel>
                                </HeaderCaptionTemplate>
                            </dx:GridViewDataTextColumn>   
                            <dx:GridViewDataTextColumn Caption="7" FieldName="7" Width="70px" VisibleIndex="7">
                                <HeaderCaptionTemplate>
                                    <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text='7'
                                        ClientSideEvents-Click='<%# "function(s,e){Bind_Day(7);}" %>' 
                                        ForeColor="Blue" Font-Underline="true" Cursor="pointer">
                                    </dx:ASPxLabel>
                                </HeaderCaptionTemplate>
                            </dx:GridViewDataTextColumn>  
                            <dx:GridViewDataTextColumn Caption="8" FieldName="8" Width="70px" VisibleIndex="8">
                                <HeaderCaptionTemplate>
                                    <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text='8'
                                        ClientSideEvents-Click='<%# "function(s,e){Bind_Day(8);}" %>' 
                                        ForeColor="Blue" Font-Underline="true" Cursor="pointer">
                                    </dx:ASPxLabel>
                                </HeaderCaptionTemplate>
                            </dx:GridViewDataTextColumn>   
                            <dx:GridViewDataTextColumn Caption="9" FieldName="9" Width="70px" VisibleIndex="9">
                                <HeaderCaptionTemplate>
                                    <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text='9'
                                        ClientSideEvents-Click='<%# "function(s,e){Bind_Day(9);}" %>' 
                                        ForeColor="Blue" Font-Underline="true" Cursor="pointer">
                                    </dx:ASPxLabel>
                                </HeaderCaptionTemplate>
                            </dx:GridViewDataTextColumn>     
                            <dx:GridViewDataTextColumn Caption="10" FieldName="10" Width="70px" VisibleIndex="10">
                                <HeaderCaptionTemplate>
                                    <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text='10'
                                        ClientSideEvents-Click='<%# "function(s,e){Bind_Day(10);}" %>' 
                                        ForeColor="Blue" Font-Underline="true" Cursor="pointer">
                                    </dx:ASPxLabel>
                                </HeaderCaptionTemplate>
                            </dx:GridViewDataTextColumn>     
                            <dx:GridViewDataTextColumn Caption="11" FieldName="11" Width="70px" VisibleIndex="11">
                                <HeaderCaptionTemplate>
                                    <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text='11'
                                        ClientSideEvents-Click='<%# "function(s,e){Bind_Day(11);}" %>' 
                                        ForeColor="Blue" Font-Underline="true" Cursor="pointer">
                                    </dx:ASPxLabel>
                                </HeaderCaptionTemplate>
                            </dx:GridViewDataTextColumn>   
                            <dx:GridViewDataTextColumn Caption="12" FieldName="12" Width="70px" VisibleIndex="12">
                                <HeaderCaptionTemplate>
                                    <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text='12'
                                        ClientSideEvents-Click='<%# "function(s,e){Bind_Day(12);}" %>' 
                                        ForeColor="Blue" Font-Underline="true" Cursor="pointer">
                                    </dx:ASPxLabel>
                                </HeaderCaptionTemplate>
                            </dx:GridViewDataTextColumn>  
                        </Columns>
                        <Styles>
                            <Header BackColor="#99CCFF"></Header>
                            <FocusedRow BackColor="#EDEDED" ForeColor="#000000"></FocusedRow>
                            <Footer HorizontalAlign="Right"></Footer>
                        </Styles>
                    </dx:ASPxGridView>
                </td>
            </tr>
        </table>
    </div>

    <div id="div_day" class="col-sm-12" style="margin-top:10px; display:none;">
        <table>
            <tr>
                <td id="td_day_title" style="font-weight:800;">
                    采购工程师：<span id="span_workcode_day" style="display:none;"></span><span id="span_lastname_day" style=" color:#008B8B"></span>
                    年份：<span id="span_year_day" style=" color:#008B8B"></span>
                    月份：<span id="span_month_day" style=" color:#008B8B"></span>
                </td>
            </tr>
            <tr>
                <td>
                    <dx:ASPxGridView ID="gv_EmpMonth" runat="server" KeyFieldName="统计明细项"  ClientInstanceName="grid_EmpMonth" OnCustomCallback="gv_EmpMonth_CustomCallback" Width="2580px"
                        OnHtmlRowCreated="gv_EmpMonth_HtmlRowCreated">
                        <ClientSideEvents EndCallback="function(s, e) {setHeight();}"  />
                        <SettingsPager PageSize="100" ></SettingsPager>
                        <SettingsBehavior AllowFocusedRow="True" ColumnResizeMode="Control" AllowSort="false"/>
                        <Columns>                        
                        </Columns>
                        <Styles>
                            <Header BackColor="#99CCFF"></Header>
                            <FocusedRow BackColor="#EDEDED" ForeColor="#000000"></FocusedRow>
                            <Footer HorizontalAlign="Right"></Footer>
                        </Styles>
                    </dx:ASPxGridView>
                </td>
            </tr>
        </table>
    </div>
     
    
</asp:Content>

