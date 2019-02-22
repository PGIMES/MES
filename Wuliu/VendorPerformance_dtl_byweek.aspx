<%@ Page Title="供应商超欠交明细ByWeek" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="VendorPerformance_dtl_byweek.aspx.cs" Inherits="VendorPerformance_dtl_byweek" %>

<%@ Register Assembly="DevExpress.Web.v17.2, Version=17.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <style>
        th, td {
            padding-left: 3px;
            padding-right: 3px;
        }
    </style>
    <script src="../Content/js/jquery.min.js" type="text/javascript"></script>
    <script src="../Content/js/plugins/layer/layer.min.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            $("#mestitle").text("【<%=Title%>】");
            $(".mainTop,.mainTopRight").hide();
        });
    </script>
    <div class="row row-container">
        <div class="col-sm-12 ">
            <div class="panel panel-info">
                <div class="panel-heading">

                    <dx:ASPxPageControl ID="ASPxPageControl1" runat="server" ActiveTabIndex="0">
                        <TabPages>
                            <dx:TabPage Text="欠交明细">
                                <ContentCollection>
                                    <dx:ContentControl runat="server">
                                        <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AllowSorting="True"
                                            AutoGenerateColumns="False" 
                                            PageSize="100" Width="1100px" BorderColor="LightBlue" OnRowDataBound="GridView1_RowDataBound" ShowFooter="true">
                                            <EmptyDataTemplate>查无数据 </EmptyDataTemplate>
                                            <Columns>
                                                <asp:BoundField DataField="" HeaderText="No">
                                                    <HeaderStyle BackColor="#C1E2EB" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="域" HeaderText="域">
                                                    <HeaderStyle BackColor="#C1E2EB" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="采购订单号" HeaderText="采购订单号">
                                                    <HeaderStyle BackColor="#C1E2EB" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="订单项" HeaderText="订单项" ItemStyle-HorizontalAlign="Right">
                                                    <HeaderStyle BackColor="#C1E2EB" />

                                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="物料号" HeaderText="物料号">
                                                    <HeaderStyle BackColor="#C1E2EB" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="描述" HeaderText="描述">
                                                    <HeaderStyle BackColor="#C1E2EB" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="状态" HeaderText="状态">
                                                    <HeaderStyle BackColor="#C1E2EB" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="版本ID" HeaderText="版本ID">
                                                    <HeaderStyle BackColor="#C1E2EB" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="先前累计需求量" HeaderText="先前累计需求量" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:N1}">
                                                    <HeaderStyle BackColor="#C1E2EB" />
                                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="今天之前的计划累计" HeaderText="今天之前计划累计" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:N1}">
                                                    <HeaderStyle BackColor="#C1E2EB" Width="100px" />
                                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="累计收货" HeaderText="累计收货" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:N1}">
                                                    <HeaderStyle BackColor="#C1E2EB" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="结果QAD" HeaderText="结果(QAD)" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:N1}">
                                                    <HeaderStyle BackColor="#C1E2EB" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:BoundField>

                                                <asp:BoundField DataField="累计收货6004" HeaderText="累计收货(不含6004)" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:N1}" Visible="false">
                                                    <HeaderStyle BackColor="#C1E2EB" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="累计退货6004" HeaderText="累计退货(6004)" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:N1}" Visible="false">
                                                    <HeaderStyle BackColor="#C1E2EB" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="结果" HeaderText="结果" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:N1}" Visible="false">
                                                    <HeaderStyle BackColor="#C1E2EB" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:BoundField>

                                                <asp:BoundField DataField="责任人" HeaderText="责任人">
                                                    <HeaderStyle BackColor="#C1E2EB" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="供应商" HeaderText="供应商">
                                                    <HeaderStyle BackColor="#C1E2EB" />
                                                </asp:BoundField>

                                            </Columns>
                                            <PagerSettings FirstPageText="首页" LastPageText="尾页" Mode="NextPreviousFirstLast"
                                                NextPageText="下页" PreviousPageText="上页" />
                                            <PagerStyle ForeColor="Red" />
                                        </asp:GridView>
                                    </dx:ContentControl>
                                </ContentCollection>
                            </dx:TabPage>
                            <dx:TabPage Text="超交明细">
                                <ContentCollection>
                                    <dx:ContentControl runat="server">
                                        <asp:GridView ID="GridView2" runat="server" AllowPaging="True" AllowSorting="True"
                                            AutoGenerateColumns="False"  ShowFooter="true"
                                            PageSize="100" Width="1100px" BorderColor="LightBlue" OnRowDataBound="GridView2_RowDataBound">
                                            <EmptyDataTemplate>查无数据 </EmptyDataTemplate>
                                            <Columns>
                                                <asp:BoundField DataField="" HeaderText="No">
                                                    <HeaderStyle BackColor="#C1E2EB" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="域" HeaderText="域">
                                                    <HeaderStyle BackColor="#C1E2EB" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="采购订单号" HeaderText="采购订单号">
                                                    <HeaderStyle BackColor="#C1E2EB" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="订单项" HeaderText="订单项" ItemStyle-HorizontalAlign="Right">
                                                    <HeaderStyle BackColor="#C1E2EB" />

                                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="物料号" HeaderText="物料号">
                                                    <HeaderStyle BackColor="#C1E2EB" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="描述" HeaderText="描述">
                                                    <HeaderStyle BackColor="#C1E2EB" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="状态" HeaderText="状态">
                                                    <HeaderStyle BackColor="#C1E2EB" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="版本ID" HeaderText="版本ID">
                                                    <HeaderStyle BackColor="#C1E2EB" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="先前累计需求量" HeaderText="先前累计需求量" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:N1}">
                                                    <HeaderStyle BackColor="#C1E2EB" />

                                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="今天之前的计划累计" HeaderText="今天之前计划累计" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:N1}">
                                                    <HeaderStyle BackColor="#C1E2EB" Width="100px" />

                                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="累计收货" HeaderText="累计收货" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:N1}">
                                                    <HeaderStyle BackColor="#C1E2EB" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="结果QAD" HeaderText="结果(QAD)" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:N1}">
                                                    <HeaderStyle BackColor="#C1E2EB" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:BoundField>

                                                <asp:BoundField DataField="累计收货6004" HeaderText="累计收货(不含6004)" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:N1}" Visible="false">
                                                    <HeaderStyle BackColor="#C1E2EB" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="累计退货6004" HeaderText="累计退货(6004)" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:N1}" Visible="false">
                                                    <HeaderStyle BackColor="#C1E2EB" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="结果" HeaderText="结果" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:N1}" Visible="false">
                                                    <HeaderStyle BackColor="#C1E2EB" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:BoundField>

                                                <asp:BoundField DataField="责任人" HeaderText="责任人">
                                                    <HeaderStyle BackColor="#C1E2EB" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="供应商" HeaderText="供应商">
                                                    <HeaderStyle BackColor="#C1E2EB" />
                                                </asp:BoundField>

                                            </Columns>
                                            <PagerSettings FirstPageText="首页" LastPageText="尾页" Mode="NextPreviousFirstLast"
                                                NextPageText="下页" PreviousPageText="上页" />
                                            <PagerStyle ForeColor="Red" />
                                        </asp:GridView>
                                    </dx:ContentControl>
                                </ContentCollection>
                            </dx:TabPage>
                            <dx:TabPage Text="周需求明细" >
                                <ContentCollection>
                                    <dx:ContentControl runat="server">
                                        <asp:GridView ID="GridView3" runat="server" AllowPaging="True" AllowSorting="True"
                                            AutoGenerateColumns="False"  ShowFooter="true"
                                            PageSize="100"  BorderColor="LightBlue" OnRowDataBound="GridView3_RowDataBound">
                                            <EmptyDataTemplate>查无数据 </EmptyDataTemplate>
                                            <Columns>
                                                <asp:BoundField DataField="" HeaderText="No">
                                                    <HeaderStyle BackColor="#C1E2EB" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="域" HeaderText="域">
                                                    <HeaderStyle BackColor="#C1E2EB" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="采购订单号" HeaderText="采购订单号">
                                                    <HeaderStyle BackColor="#C1E2EB" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="订单项" HeaderText="订单项" ItemStyle-HorizontalAlign="Right">
                                                    <HeaderStyle BackColor="#C1E2EB" />

                                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="物料号" HeaderText="物料号">
                                                    <HeaderStyle BackColor="#C1E2EB" />
                                                </asp:BoundField>
                                                
                                                 
                                                <asp:BoundField DataField="版本ID" HeaderText="版本ID">
                                                    <HeaderStyle BackColor="#C1E2EB" />
                                                </asp:BoundField>                                             
                                               
                                                <asp:BoundField DataField="订单数量" HeaderText="订单数量" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:N1}">
                                                    <HeaderStyle BackColor="#C1E2EB" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:BoundField>
                                                
 
                                                <asp:BoundField DataField="订单日期" HeaderText="订单日期" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:yyyy-MM-dd}" Visible="false">
                                                    <HeaderStyle BackColor="#C1E2EB" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:BoundField>

                                               

                                            </Columns>
                                             
                                        </asp:GridView>
                                    </dx:ContentControl>
                                </ContentCollection>
                            </dx:TabPage>
                        </TabPages>

                    </dx:ASPxPageControl>


                </div>

            </div>
        </div>
    </div>





</asp:Content>
