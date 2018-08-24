<%@ Page Title="【差旅/私车公用申请单查询】" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="T_CA_Report_Query.aspx.cs" Inherits="Forms_Finance_T_CA_Report_Query" %>

<%@ Register Assembly="DevExpress.Web.v17.2, Version=17.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <script type="text/javascript" src="/Content/js/jquery.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#mestitle").text("【差旅/私车公用申请单查询】");
             setHeight();

             $(window).resize(function () {
                 setHeight();
             });
                 
        });

        function setHeight() {
            $("div[class=dxgvCSD]").css("height", ($(window).height() - $("#div_p").height() - 130) + "px");
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

    <div class="col-md-12" id="div_p"  style="margin-bottom:5px">
        <table style=" border-collapse: collapse;">
            <tr>
                <td style="width:70px;">申请公司</td>
                <td style="width:125px;">
                    <asp:DropDownList ID="ddl_domain" runat="server" class="form-control input-s-md " Width="120px">
                        <asp:ListItem Value="">ALL</asp:ListItem>
                        <asp:ListItem Value="200">昆山工厂</asp:ListItem>
                        <asp:ListItem Value="100">上海工厂</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td style="width:60px;">类别</td>
                <td style="width:155px;"> 
                    <asp:DropDownList ID="ddl_typeno" runat="server" class="form-control input-s-md " Width="150px">
                        <asp:ListItem Value="差旅">差旅申请单</asp:ListItem>
                        <asp:ListItem Value="私车公用">私车公用申请单</asp:ListItem>
                    </asp:DropDownList>
                </td>            
                <td id="td_btn">  
                    <button id="btn_search" type="button" class="btn btn-primary btn-large" runat="server" onserverclick="btn_search_Click"><i class="fa fa-search fa-fw"></i>&nbsp;查询</button>  
                    <button id="btn_import" type="button" class="btn btn-primary btn-large" runat="server" onserverclick="btn_import_Click"><i class="fa fa-download fa-fw"></i>&nbsp;导出</button>
                </td>
            </tr>                      
        </table>                   
    </div>

    <div class="col-sm-12">
        <table>
            <tr>
                <td>
                    <dx:ASPxGridView ID="gv" runat="server" KeyFieldName="id" AutoGenerateColumns="False" Width="1500px" OnPageIndexChanged="gv_PageIndexChanged"  ClientInstanceName="grid">
                        <ClientSideEvents EndCallback="function(s, e) {setHeight();}"  />
                        <SettingsPager PageSize="1000" ></SettingsPager>
                        <Settings ShowFilterRow="True" ShowGroupPanel="false" ShowFilterRowMenu="True" ShowFilterRowMenuLikeItem="True" AutoFilterCondition="Contains" 
                            VerticalScrollBarMode="Visible" VerticalScrollBarStyle="Standard" VerticalScrollableHeight="600"  />
                        <SettingsBehavior AllowFocusedRow="True" ColumnResizeMode="Control"  />
                        <Columns>                        
                            <dx:GridViewDataTextColumn Caption="报销单号" FieldName="FormNo" Width="140px" VisibleIndex="1" >
                                <DataItemTemplate>
                                    <dx:ASPxHyperLink ID="hpl_FormNo" runat="server" Text='<%# Eval("FormNo")%>' Cursor="pointer" ClientInstanceName='<%# "FormNo"+Container.VisibleIndex.ToString() %>'
                                         NavigateUrl='<%# "/Platform/WorkFlowRun/Default.aspx?flowid=24f321ee-b4e3-4c2c-a0a4-f51cafdf526f&appid=90A30D34-F40F-4A0B-BCFD-3AD9786FF757&display=1&GroupID="+ Eval("GroupID")+"&InstanceID="+ Eval("FormNo") %>'  
                                         Target="_blank">                                        
                                    </dx:ASPxHyperLink>
                                </DataItemTemplate> 
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="申请日期" FieldName="ApplyDate" Width="80px" VisibleIndex="2"></dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="申请人" FieldName="ApplyName" Width="80px" VisibleIndex="3"></dx:GridViewDataTextColumn>                            
                            <dx:GridViewDataTextColumn Caption="申请部门" FieldName="ApplyDept" Width="120px" VisibleIndex="4"></dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="申请人公司" FieldName="ApplyDomainName" Width="80px" VisibleIndex="5"></dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="差旅类型" FieldName="TravelType" Width="70px" VisibleIndex="6"></dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="差旅地点" FieldName="TravelPlace" Width="80px" VisibleIndex="7"></dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="差旅行程期间" FieldName="StartToEnd" Width="160px" VisibleIndex="8"></dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="出差事由" FieldName="TravelReason" Width="250px" VisibleIndex="9"></dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="随行人员" FieldName="PlanAttendant" Width="200px" VisibleIndex="10"></dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="额度/预算" FieldName="BudgetTotalCostByForm" Width="80px" VisibleIndex="11"></dx:GridViewDataTextColumn>

                            <dx:GridViewDataTextColumn Caption="已申请天数" FieldName="GoDays" Width="80px" VisibleIndex="12"></dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="审批状态" FieldName="GoSatus" Width="100px" VisibleIndex="13"></dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="批准日期" FieldName="ApproveDate" Width="80px" VisibleIndex="14"></dx:GridViewDataTextColumn>
                        </Columns>
                        <Styles>
                            <Header BackColor="#99CCFF"></Header>
                            <FocusedRow BackColor="#99CCFF" ForeColor="#0000CC"></FocusedRow>
                            <Footer HorizontalAlign="Right"></Footer>
                        </Styles>
                    </dx:ASPxGridView>
                    <dx:ASPxGridViewExporter ID="ASPxGridViewExporter1" runat="server" GridViewID="gv">
                    </dx:ASPxGridViewExporter>
                </td>
            </tr>
        </table>
    </div>

    <div class="col-sm-12">
        <table>
            <tr>
                <td>
                    <dx:ASPxGridView ID="gv_sj" runat="server" KeyFieldName="id" AutoGenerateColumns="False" Width="820px" OnPageIndexChanged="gv_sj_PageIndexChanged"  ClientInstanceName="grid_sj">
                        <ClientSideEvents EndCallback="function(s, e) {setHeight();}"  />
                        <SettingsPager PageSize="1000" ></SettingsPager>
                        <Settings ShowFilterRow="True" ShowGroupPanel="false" ShowFilterRowMenu="True" ShowFilterRowMenuLikeItem="True" AutoFilterCondition="Contains" 
                            VerticalScrollBarMode="Visible" VerticalScrollBarStyle="Standard" VerticalScrollableHeight="600"  />
                        <SettingsBehavior AllowFocusedRow="True" ColumnResizeMode="Control"  />
                        <Columns>                        
                            <dx:GridViewDataTextColumn Caption="表单单号" FieldName="FormNo" Width="140px" VisibleIndex="1" >
                                <DataItemTemplate>
                                    <dx:ASPxHyperLink ID="hpl_FormNo" runat="server" Text='<%# Eval("FormNo")%>' Cursor="pointer" ClientInstanceName='<%# "FormNo"+Container.VisibleIndex.ToString() %>'
                                         NavigateUrl='<%# "/Platform/WorkFlowRun/Default.aspx?flowid=3f8de2dd-9229-4517-90a6-c13cb10a5c07&appid=A9EE1086-F066-41FD-A5C5-0BF50F272EB2&display=1&GroupID="+ Eval("GroupID")+"&InstanceID="+ Eval("FormNo") %>'  
                                         Target="_blank">                                        
                                    </dx:ASPxHyperLink>
                                </DataItemTemplate> 
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="申请日期" FieldName="ApplyDate" Width="80px" VisibleIndex="2"></dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="申请人" FieldName="ApplyName" Width="80px" VisibleIndex="3"></dx:GridViewDataTextColumn>                            
                            <dx:GridViewDataTextColumn Caption="申请部门" FieldName="ApplyDept" Width="120px" VisibleIndex="4"></dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="申请人公司" FieldName="ApplyDomainName" Width="100px" VisibleIndex="5"></dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="申请人车牌号" FieldName="CarNo" Width="100px" VisibleIndex="5"></dx:GridViewDataTextColumn>

                            <dx:GridViewDataTextColumn Caption="已申请天数" FieldName="GoDays" Width="100px" VisibleIndex="12"></dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="审批状态" FieldName="GoSatus" Width="100px" VisibleIndex="13"></dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="批准日期" FieldName="ApproveDate" Width="100px" VisibleIndex="14"></dx:GridViewDataTextColumn>
                        </Columns>
                        <Styles>
                            <Header BackColor="#99CCFF"></Header>
                            <FocusedRow BackColor="#99CCFF" ForeColor="#0000CC"></FocusedRow>
                            <Footer HorizontalAlign="Right"></Footer>
                        </Styles>
                    </dx:ASPxGridView>
                    <dx:ASPxGridViewExporter ID="ASPxGridViewExporter2" runat="server" GridViewID="gv_sj">
                    </dx:ASPxGridViewExporter>
                </td>
            </tr>
        </table>
    </div>

</asp:Content>

