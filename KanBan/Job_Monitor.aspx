<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeFile="Job_Monitor.aspx.cs" Inherits="JingLian_Job_Monitor" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head  runat="server">
    <title></title>
</head>
<style>
     .hidden { display:none;}
     body{font-size:35px; background-color:Black; font-weight:bold}
     td{font-size:35px;  color:White; padding-bottom:5px; padding-top:5px; padding-left:5px; font-weight:bold}
     .headfont{font-size:35px; background-color:White; font-weight:bold}
     img{ width:30px; height:30px}
    
</style>
<body>
    <form id="form1" runat="server" style="color: #FFFFFF">
<script src="../Content/js/jquery.min.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript">
        $(document).ready(function () {

            setInterval(refresh, 60000);

        });
       
        </script>

        <script type="text/javascript">
            function refresh() {
                this.location = this.location;
            }
	</script>
    【设备报修记录】
                                <asp:GridView ID="GridView1" runat="server" 
                                    AllowPaging="True" AllowSorting="True" 
                                    AutoGenerateColumns="False" 
                                    onpageindexchanging="GridView1_PageIndexChanging" 
                                    onrowdatabound="GridView1_RowDataBound" PageSize="100" 
                                    Width="1600px">
                                    <PagerSettings FirstPageText="首页" LastPageText="尾页" 
                                        Mode="NextPreviousFirstLast" NextPageText="下页" 
                                        PreviousPageText="上页" />
                                    <Columns>
                                        　 
                                         
                                        <asp:BoundField DataField="job_type" HeaderText="作业类型">
                                        <HeaderStyle BackColor="White" CssClass="headfont" 
                                            Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="status" HeaderText="状态">
                                        <ControlStyle CssClass="hidden" Width="0px" />
                                        <FooterStyle CssClass="hidden" Width="0px" />
                                        <HeaderStyle BackColor="White" Width="0px" 
                                            CssClass="hidden" />
                                        <ItemStyle CssClass="hidden" Width="0px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="status_desc" HeaderText="状态说明">
                                        <HeaderStyle BackColor="White" CssClass="headfont" />
                                        <ItemStyle Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Mo_Name" HeaderText="维护对象名称">
                                        <HeaderStyle BackColor="White" CssClass="headfont" />
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="停机">
                                            <ItemTemplate>
                                                <asp:Image ID="Image2" runat="server" />
                                            </ItemTemplate>
                                            <HeaderStyle BackColor="White"   CssClass="headfont"/>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="createdate" HeaderText="创建时间" 
                                            DataFormatString="{0:MM/dd HH:mm}">
                                        <HeaderStyle BackColor="White" CssClass="headfont" />
                                        <ItemStyle Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Mo_Key" HeaderText="维护对象">
                                        <HeaderStyle BackColor="White" CssClass="headfont" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Stopped" HeaderText="停机">
                                        <ControlStyle CssClass="hidden" Width="0px" />
                                        <FooterStyle CssClass="hidden" Width="0px" />
                                        <HeaderStyle BackColor="White" CssClass="hidden" 
                                            Width="0px" />
                                        <ItemStyle CssClass="hidden" Width="0px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Line_stopped" HeaderText="停线">
                                        <ControlStyle CssClass="hidden" Width="0px" />
                                        <FooterStyle CssClass="hidden" Width="0px" />
                                        <HeaderStyle BackColor="White" CssClass="hidden" 
                                            Width="0px" />
                                        <ItemStyle CssClass="hidden" Width="0px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Ordered_by" HeaderText="发起人">
                                        <HeaderStyle BackColor="White" CssClass="headfont" 
                                            Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="truename" HeaderText="接单人">
                                        <HeaderStyle BackColor="White" CssClass="headfont" 
                                            Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="jg" HeaderText="时长(H)">
                                        <HeaderStyle BackColor="White" CssClass="headfont" />
                                        </asp:BoundField>
                                    </Columns>
                                </asp:GridView>
                       
                
                  </form>
</body>
</html>