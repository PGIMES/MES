<%@ Page Title="【客户日程单流程状态查询】" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="CS_Report_flow_Query.aspx.cs" Inherits="Forms_Sale_CS_Report_flow_Query" %>

<%@ Register Assembly="DevExpress.Web.v17.2, Version=17.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <script src="/Content/js/jquery.min.js"  type="text/javascript"></script>
    <script src="/Content/js/bootstrap.min.js"></script>
    <script type="text/javascript" src="/Content/js/jquery.cookie.min.js"></script>
    <script src="/Content/js/layer/layer.js"></script>
    <script type="text/javascript">
        <%--var UserId = '<%=UserId%>';
        var UserName = '<%=UserName%>';
        var DeptName = '<%=DeptName%>';--%>

        $(document).ready(function () {
            $("#mestitle").text("【客户日程单流程状态查询】");

            setHeight();
            $(window).resize(function () {
                setHeight();
            });


        });

        function setHeight() {
            $("div[class=dxgvCSD]").css("height", ($(window).height() - $("#div_p").height() - 250) + "px");
        }
        	
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

    <div class="panel-body" id="div_p">
        <div class="col-sm-12">
            <table style="line-height:40px">
                <tr>  
                    <td style="text-align:right;">&nbsp;&nbsp;物料号：</td>
                    <td colspan="3">
                        <asp:TextBox ID="txt_wlh" class="form-control input-s-md " runat="server"></asp:TextBox>
                    </td>  
                    <td style="text-align:right;">&nbsp;&nbsp;客户物料号：</td>
                    <td colspan="3">
                        <asp:TextBox ID="txt_cust_part" class="form-control input-s-md " runat="server"></asp:TextBox>
                    </td>  
                    <td style="width:30px;">版本</td>
                    <td style="width:85px;"> 
                        <asp:DropDownList ID="ddl_ver" runat="server" class="form-control input-s-md " Width="80px">
                            <asp:ListItem Value="">ALL</asp:ListItem>
                            <asp:ListItem Value="当前" Selected="True">当前</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td> 
                        &nbsp;
                        <asp:Button ID="Bt_select" runat="server" Text="查询" class="btn btn-large btn-primary" OnClick="Bt_select_Click" Width="70px" /> 
                        <asp:Button ID="Bt_Export" runat="server" class="btn btn-large btn-primary" OnClick="Bt_Export_Click" Text="导出" Width="70px" /> 
                    </td> 
                </tr>
            </table>
        </div>
    </div>
    <div runat="server" id="DIV1" style="margin-left: 5px; margin-right: 5px; margin-bottom: 10px">
        <table>
            <tr>
                <td>
                    <dx:ASPxGridView ID="GV_PART" ClientInstanceName="grid" runat="server" KeyFieldName="FormNo" AutoGenerateColumns="False"  
                             OnPageIndexChanged="GV_PART_PageIndexChanged" Width="570px"><%--3030--%>
                        <ClientSideEvents EndCallback="function(s, e) { setHeight(); }" />
                        <SettingsBehavior AllowDragDrop="TRUE" AllowFocusedRow="false" AllowSelectByRowClick="false" ColumnResizeMode="Control" AutoExpandAllGroups="true" MergeGroupsMode="Always" SortMode="Value" />
                        <SettingsPager PageSize="100"></SettingsPager>
                        <Settings ShowFilterRow="True" ShowFilterRowMenu="True"   VerticalScrollBarMode="Visible" VerticalScrollBarStyle="Standard" VerticalScrollableHeight="500"
                                 ShowFilterRowMenuLikeItem="True"  ShowFooter="True"  HorizontalScrollBarMode="Auto" />
                        <Columns>
                            <%--<dx:GridViewCommandColumn   ShowClearFilterButton="true" ShowSelectCheckbox="true" Name="Sel" Width="40" VisibleIndex="0"  SelectAllCheckboxMode="Page"  >
                            </dx:GridViewCommandColumn> --%>
                            <dx:GridViewDataTextColumn Caption="域" FieldName="domain" Width="40px" VisibleIndex="1" />
                            <dx:GridViewDataTextColumn Caption="物料号" FieldName="part" Width="70px" VisibleIndex="2" />
                            <dx:GridViewDataTextColumn Caption="客户项目" FieldName="cust_part" Width="120px" VisibleIndex="3" />
                            <dx:GridViewDataTextColumn Caption="申请人" FieldName="ApplyId" Width="80px" VisibleIndex="3" />
                            <dx:GridViewDataTextColumn Caption="申请人" FieldName="ApplyName" Width="80px" VisibleIndex="3" />
                            <dx:GridViewDataTextColumn Caption="审批状态" FieldName="GoSatus" Width="120px" VisibleIndex="3" />
                            <dx:GridViewDataDateColumn Caption="签核完成日" FieldName="ApproveDate" Width="90px" VisibleIndex="4" >
                                <PropertiesDateEdit DisplayFormatString="yyyy/MM/dd"></PropertiesDateEdit>
                            </dx:GridViewDataDateColumn>
                            <dx:GridViewDataTextColumn Caption="表单编号" FieldName="FormNo" Width="90px" VisibleIndex="5">
                                <Settings AllowCellMerge="True" />
                            </dx:GridViewDataTextColumn>
                        </Columns>
                        <Styles>
                            <Header BackColor="#99CCFF"></Header>
                            <FocusedRow BackColor="#99CCFF" ForeColor="#0000CC"></FocusedRow>
                            <Footer HorizontalAlign="Right"></Footer>
                            <AlternatingRow Enabled="true" />
                        </Styles>
                    </dx:ASPxGridView>
                </td>
            </tr>
        </table>
    </div>

</asp:Content>
