<%@ Page Title="【费用报销单查询】" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="OES_Report_Query.aspx.cs" Inherits="Forms_Finance_OES_Report_Query" %>

<%@ Register Assembly="DevExpress.Web.v17.2, Version=17.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <script type="text/javascript" src="/Content/js/jquery.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#mestitle").text("【费用报销单查询】");
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
                    <dx:ASPxGridView ID="gv" runat="server" KeyFieldName="id" AutoGenerateColumns="False" Width="1660px" OnPageIndexChanged="gv_PageIndexChanged"  ClientInstanceName="grid"
                        OnCustomCellMerge="gv_CustomCellMerge">
                        <ClientSideEvents EndCallback="function(s, e) {setHeight();}"  />
                        <SettingsPager PageSize="20" ></SettingsPager>
                        <Settings ShowFilterRow="True" ShowGroupPanel="false" ShowFilterRowMenu="True" ShowFilterRowMenuLikeItem="True" AutoFilterCondition="Contains" 
                            VerticalScrollBarMode="Visible" VerticalScrollBarStyle="Standard" VerticalScrollableHeight="600"  />
                        <SettingsBehavior AllowFocusedRow="True" ColumnResizeMode="Control"  />
                        <Columns>                        
                            <dx:GridViewDataTextColumn Caption="报销单号" FieldName="FormNo" Width="100px" VisibleIndex="1" >
                                <Settings AllowCellMerge="True" />
                                <DataItemTemplate>
                                    <dx:ASPxHyperLink ID="hpl_FormNo" runat="server" Text='<%# Eval("FormNo")%>' Cursor="pointer" ClientInstanceName='<%# "FormNo"+Container.VisibleIndex.ToString() %>'
                                         NavigateUrl='<%# "/Platform/WorkFlowRun/Default.aspx?flowid=4d085987-9001-48f7-b189-ffee43a7da71&appid=11305A6A-CC6A-4AEC-8841-8EF2B8E57FAD&display=1&stepid="+ Eval("StepID")+"&GroupID="+ Eval("GroupID")+"&InstanceID="+ Eval("FormNo") %>'  
                                         Target="_blank">                                        
                                    </dx:ASPxHyperLink>
                                </DataItemTemplate> 
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="申请日期" FieldName="ApplyDate" Width="75px" VisibleIndex="2">
                                <Settings AllowCellMerge="True" />
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="申请人" FieldName="ApplyName" Width="55px" VisibleIndex="3">
                                <Settings AllowCellMerge="True" />
                            </dx:GridViewDataTextColumn>                            
                            <dx:GridViewDataTextColumn Caption="申请部门" FieldName="ApplyDept" Width="105px" VisibleIndex="4">
                                <Settings AllowCellMerge="True" />
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="申请人公司" FieldName="ApplyDomainName" Width="60px" VisibleIndex="5">
                                <Settings AllowCellMerge="True" />
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="费用类别和费用项目" FieldName="CostCodeDesc" Width="180px" VisibleIndex="6"></dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="预算来源" FieldName="instanceid" Width="120px" VisibleIndex="7">
                                <DataItemTemplate>
                                    <dx:ASPxHyperLink ID="hpl_instanceid" runat="server" Text='<%# Eval("instanceid")%>' Cursor="pointer" ClientInstanceName='<%# "instanceid"+Container.VisibleIndex.ToString() %>'
                                         NavigateUrl='<%# Eval("budgetsour") %>'   ToolTip='<%# Eval("instanceid")%>'
                                         Target="_blank">                                        
                                    </dx:ASPxHyperLink>
                                </DataItemTemplate> 
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="发生日期/期间" FieldName="feedate" Width="85px" VisibleIndex="8"></dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="费用说明" FieldName="feenote" Width="240px" VisibleIndex="9"></dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="额度/预算" FieldName="limit" Width="70px" VisibleIndex="10" CellStyle-HorizontalAlign="Right">
                                <PropertiesTextEdit DisplayFormatString="{0:N1}"></PropertiesTextEdit>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="报销金额" FieldName="amount" Width="70px" VisibleIndex="11">
                                <PropertiesTextEdit DisplayFormatString="{0:N1}"></PropertiesTextEdit>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="已申请天数" FieldName="GoDays" Width="70px" VisibleIndex="12">
                                <Settings AllowCellMerge="True" />
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="审批状态" FieldName="GoSatus" Width="90px" VisibleIndex="13">
                                <Settings AllowCellMerge="True" />
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="批准日期" FieldName="ApproveDate" Width="80px" VisibleIndex="14">
                                <Settings AllowCellMerge="True" />
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="科目" FieldName="KeMuId" Width="65px" VisibleIndex="15">                                
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="科目描述" FieldName="KeMuMs" Width="120px" VisibleIndex="16">                                
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="项目" FieldName="KeMu2" Width="50px" VisibleIndex="17">
                               
                            </dx:GridViewDataTextColumn>
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

</asp:Content>

