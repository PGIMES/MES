<%@ Page Title="【应付类合同执行进度查询】" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="xxcontract_Report.aspx.cs" Inherits="Fin_xxcontract_Report" %>

<%@ Register Assembly="DevExpress.Web.v17.2, Version=17.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <script type="text/javascript" src="/Content/js/jquery.min.js"></script>
    <script src="/Content/js/layer/layer.js"></script>
    <script src="/Content/js/plugins/layer/laydate/laydate.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#mestitle").html("【应付类合同执行进度查询】");
            setHeight();

            $(window).resize(function () {
                setHeight();
            });

        });

        function setHeight() {
            $("div[class=dxgvCSD]").css("height", ($(window).height() - $("#div_p").height() - 160) + "px");
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

    <div class="col-md-12" id="div_p"  style="margin-bottom:5px">
        <table style=" border-collapse: collapse;">
            <tr>
                <td style="width:35px;">域</td>
                <td style="width:125px;">
                    <asp:DropDownList ID="ddl_domain" runat="server" class="form-control input-s-md " Width="120px" AutoPostBack="true">
                        <asp:ListItem Value="200">昆山工厂</asp:ListItem>
                        <asp:ListItem Value="100">上海工厂</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td style="width:65px;">签订日期</td>
                <td style="width:125px;">
                    <asp:TextBox ID="StartDate" runat="server" class="form-control" Width="120px" 
                        onclick="laydate({type: 'date',format: 'YYYY/MM/DD',choose: function(dates){}});" />
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
                    <dx:ASPxGridView ID="gv" runat="server" KeyFieldName="id" AutoGenerateColumns="False" Width="" OnPageIndexChanged="gv_PageIndexChanged"  ClientInstanceName="grid">
                        <ClientSideEvents EndCallback="function(s, e) {setHeight();}"  />
                        <SettingsPager PageSize="100" ></SettingsPager>
                        <Settings ShowFilterRow="True" ShowGroupPanel="false" ShowFilterRowMenu="True" ShowFilterRowMenuLikeItem="True" AutoFilterCondition="Contains" 
                            VerticalScrollBarMode="Visible" VerticalScrollBarStyle="Standard" VerticalScrollableHeight="600"  />
                        <SettingsBehavior AllowFocusedRow="True" ColumnResizeMode="Control"/>
                        <Columns>                        
                            <dx:GridViewDataTextColumn Caption="合同类型" FieldName="xxcontract_charfld[1]" Width="55px" VisibleIndex="1" ></dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="系统合同号" FieldName="xxcontractd_nbr" Width="80px" VisibleIndex="2"></dx:GridViewDataTextColumn>   
                            <dx:GridViewDataTextColumn Caption="行号" FieldName="xxcontractd_line" Width="40px" VisibleIndex="3"></dx:GridViewDataTextColumn>   
                            <dx:GridViewDataTextColumn Caption="实际合同号" FieldName="xxcontract_charfld[4]" Width="80px" VisibleIndex="4"></dx:GridViewDataTextColumn>     
                            <dx:GridViewDataTextColumn Caption="供应商编码" FieldName="xxcontract_charfld[10]" Width="65px" VisibleIndex="5"></dx:GridViewDataTextColumn>     
                            <dx:GridViewDataTextColumn Caption="合同对方单位" FieldName="xxcontract_charfld[5]" Width="210px" VisibleIndex="6"></dx:GridViewDataTextColumn>   
                            <dx:GridViewDataTextColumn Caption="合同名称" FieldName="xxcontract_charfld[6]" Width="210px" VisibleIndex="7"></dx:GridViewDataTextColumn>     
                            <dx:GridViewDataTextColumn Caption="产品信息" FieldName="xxcontract_charfld[7]" Width="160px" VisibleIndex="8"></dx:GridViewDataTextColumn>  
                            <dx:GridViewDataTextColumn Caption="模具属性" FieldName="xxcontract_charfld[8]" Width="110px" VisibleIndex="9"></dx:GridViewDataTextColumn>     
                            <dx:GridViewDataTextColumn Caption="条款摘要" FieldName="xxcontractd_charfld[1]" Width="100px" VisibleIndex="10"></dx:GridViewDataTextColumn> 
                            <dx:GridViewDataDateColumn Caption="签订日期" FieldName="xxcontract_datefld[1]" Width="80px" VisibleIndex="11">
                                <PropertiesDateEdit DisplayFormatString="yyyy/MM/dd"></PropertiesDateEdit>
                            </dx:GridViewDataDateColumn>                          
                            <dx:GridViewDataDateColumn Caption="计划到货日期" FieldName="xxcontract_datefld[2]" Width="80px" VisibleIndex="12">
                                <PropertiesDateEdit DisplayFormatString="yyyy/MM/dd"></PropertiesDateEdit>
                            </dx:GridViewDataDateColumn> 
                            <dx:GridViewDataDateColumn Caption="实际到货日期" FieldName="xxcontract_datefld[5]" Width="80px" VisibleIndex="13">
                                <PropertiesDateEdit DisplayFormatString="yyyy/MM/dd"></PropertiesDateEdit>
                            </dx:GridViewDataDateColumn> 
                            <dx:GridViewDataTextColumn Caption="合同币种" FieldName="xxcontract_charfld[9]" Width="55px" VisibleIndex="14"></dx:GridViewDataTextColumn> 
                            <dx:GridViewDataTextColumn Caption="汇率" FieldName="ExchangeRate" Width="70px" VisibleIndex="15">
                                <PropertiesTextEdit DisplayFormatString="{0:N5}"></PropertiesTextEdit>
                            </dx:GridViewDataTextColumn> 
                            <dx:GridViewDataTextColumn Caption="合同原币总金额" FieldName="xxcontract_decfld[2]" Width="90px" VisibleIndex="16">
                                <PropertiesTextEdit DisplayFormatString="{0:N2}"></PropertiesTextEdit>
                            </dx:GridViewDataTextColumn> 
                            <dx:GridViewDataTextColumn Caption="合同计划原币总金额" FieldName="xxcontract_decfld_2_plan" Width="130px" VisibleIndex="17">
                                <PropertiesTextEdit DisplayFormatString="{0:N3}"></PropertiesTextEdit>
                            </dx:GridViewDataTextColumn> 
                            <dx:GridViewDataDateColumn Caption="计划付款日期" FieldName="xxcontractd_datefld[1]" Width="80px" VisibleIndex="18">
                                <PropertiesDateEdit DisplayFormatString="yyyy/MM/dd"></PropertiesDateEdit>
                            </dx:GridViewDataDateColumn> 
                            <dx:GridViewDataTextColumn Caption="计划付款金额(本币)" FieldName="fkamt__plan_cur" Width="130px" VisibleIndex="19">
                                <PropertiesTextEdit DisplayFormatString="{0:N6}"></PropertiesTextEdit>
                            </dx:GridViewDataTextColumn>  
                            <dx:GridViewDataTextColumn Caption="计划付款比例" FieldName="xxcontractd_decfld_02_percent" Width="90px" VisibleIndex="20"></dx:GridViewDataTextColumn> 
                            <dx:GridViewDataDateColumn Caption="实际付款日期" FieldName="fkdate" Width="80px" VisibleIndex="21">
                                <PropertiesDateEdit DisplayFormatString="yyyy/MM/dd"></PropertiesDateEdit>
                            </dx:GridViewDataDateColumn> 
                            <dx:GridViewDataTextColumn Caption="付款金额(原币)" FieldName="fkamt" Width="100px" VisibleIndex="22">
                                <PropertiesTextEdit DisplayFormatString="{0:N2}"></PropertiesTextEdit>
                            </dx:GridViewDataTextColumn> 
                            <dx:GridViewDataTextColumn Caption="付款金额(本币)" FieldName="fkamt_cur" Width="100px" VisibleIndex="23">
                                <PropertiesTextEdit DisplayFormatString="{0:N2}"></PropertiesTextEdit>
                            </dx:GridViewDataTextColumn> 
                            <dx:GridViewDataTextColumn Caption="付款比例" FieldName="fkamt_rate" Width="70px" VisibleIndex="24"></dx:GridViewDataTextColumn>  
                            <dx:GridViewDataTextColumn Caption="累计付款比例" FieldName="fkrate" Width="70px" VisibleIndex="25"></dx:GridViewDataTextColumn> 
                            <dx:GridViewDataTextColumn Caption="余额(原币)" FieldName="ye_oricur" Width="100px" VisibleIndex="26">
                                <PropertiesTextEdit DisplayFormatString="{0:N2}"></PropertiesTextEdit>
                            </dx:GridViewDataTextColumn> 
                            <dx:GridViewDataTextColumn Caption="余额(本币)" FieldName="ye_cur" Width="100px" VisibleIndex="27">
                                <PropertiesTextEdit DisplayFormatString="{0:N2}"></PropertiesTextEdit>
                            </dx:GridViewDataTextColumn> 
                            <dx:GridViewDataTextColumn Caption="余额比例" FieldName="yerate" Width="70px" VisibleIndex="28"></dx:GridViewDataTextColumn> 
                            <dx:GridViewDataDateColumn Caption="验收日期" FieldName="xxcontract_datefld[3]" Width="80px" VisibleIndex="29">
                                <PropertiesDateEdit DisplayFormatString="yyyy/MM/dd"></PropertiesDateEdit>
                            </dx:GridViewDataDateColumn> 
                            <dx:GridViewDataDateColumn Caption="收到发票日期" FieldName="xxcontractd_datefld[2]" Width="80px" VisibleIndex="30">
                                <PropertiesDateEdit DisplayFormatString="yyyy/MM/dd"></PropertiesDateEdit>
                            </dx:GridViewDataDateColumn> 
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

