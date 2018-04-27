<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DJ_List.aspx.cs"
    Inherits="KanBan_DJ_List" %>

<html>
<head id="Head1" runat="server">
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="C#" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5"
        name="vs_targetSchema">
    <script src="../Content/js/jquery.min.js" type="text/javascript"></script>
    <script type="text/javascript" src="../Content/js/ScrollableTable.js"></script>
    <link href="../Content/js/superTables_Default.css" rel="Stylesheet"
        type="text/css" />
    <style type="text/css">
        .hidden
        {
            display: none;
        }
        body
        {
            font-size: 22px;
            background-color: white;
            font-weight: bold;
        }
        td
        {
            font-size: 22px;
            color: black;
            padding-bottom: 5px;
            font-weight: bold;
            word-break: break-all;
        }
        .headfont
        {
            font-size: 22px;
            background-color: #cccccc;
            font-weight: bold;
        }
        .tdfont
        {
            font-size: 22px;
            background-color: #cccccc;
            font-weight: bold;
            color: Black;
        }
        
        .blk_02
        {
            margin-top: 0px;
        }
        
        .blk_02 .table_title table
        {
            border-left: 0px solid #b3d3ec;
            border-top: 0px solid #b3d3ec;
            background: #e0f0fd;
            color: #5198cc;
        }
        
        .blk_02 .table_title table th
        {
            border-right: 0px solid #b3d3ec;
            border-bottom: 0px solid #b3d3ec;
            height: 24px;
            font-weight: normal;
            text-align: center;
        }
        
        .blk_02 .table_data
        {
            height: 500px;
            overflow: auto;
        }
        .blk_02 .table_data table
        {
            border-left: 0px solid #b3d3ec;
        }
        
        .blk_02 .table_data table td
        {
            border-right: 0px solid #b3d3ec;
            border-bottom: 0px solid #b3d3ec;
            height: 24px;
            font-weight: normal;
            text-align: left;
        }
    </style>
   
</head>
<body>

    <form id="Form1" method="post" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
 
        </asp:ScriptManager>
    <font color="black">【待检看板】</font><span style="color: Yellow;
        font-size: 22px"></span>
  
            <div id="demo1">
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False"
                    OnRowDataBound="GridView1_RowDataBound" 
                    AllowPaging="True" 
                    onpageindexchanging="GridView1_PageIndexChanging" 
                    PageSize="13">
                    <Columns>
                     <asp:BoundField DataField="dh" HeaderText="单号">
                            <HeaderStyle Wrap="true" CssClass="headfont" 
                            Width="140px" />
                            <ItemStyle Width="140px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="xmh" HeaderText="项目号">
                            <HeaderStyle Wrap="true" CssClass="headfont" 
                            Width="140px" />
                            <ItemStyle Width="140px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="sysl" HeaderText="数量" HtmlEncode="False">
                            <HeaderStyle Wrap="true" CssClass="headfont" Width="50px" />
                            <ItemStyle Width="50px" CssClass="break" />
                        </asp:BoundField>
                        <asp:BoundField DataField="sjlb" HeaderText="委托类别"
                            HtmlEncode="False">
                            <HeaderStyle Wrap="True" CssClass="headfont" 
                            Width="185px" />
                            <ItemStyle Width="185px" CssClass="break" />
                        </asp:BoundField>
                        <asp:BoundField DataField="sjtime" HeaderText="申请日期" DataFormatString="{0:MM/dd HH:mm}">
                            <HeaderStyle Wrap="True" CssClass="headfont" 
                            Width="135px" />
                            <ItemStyle Width="135px" Wrap="True" />
                        </asp:BoundField>
                        <asp:BoundField DataField="yqwcsj" HeaderText="要求时间"
                            DataFormatString="{0:MM/dd HH:mm}" HtmlEncode="False">
                            <HeaderStyle Wrap="True" CssClass="headfont" 
                            Width="135px" />
                            <ItemStyle Width="135px" Wrap="True" CssClass="break" />
                        </asp:BoundField>
                        <asp:BoundField DataField="wtr_name" HeaderText="委托人">
                            <HeaderStyle Wrap="True" CssClass="headfont" 
                            Width="75px" />
                            <ItemStyle Width="75px" CssClass="break" />
                        </asp:BoundField>
                        <asp:BoundField DataField="wt_dept" HeaderText="部门" HtmlEncode="False">
                            <HeaderStyle Wrap="True" CssClass="headfont" 
                            Width="60px" />
                            <ItemStyle Width="60px" CssClass="break" />
                        </asp:BoundField>
                        <asp:BoundField DataField="djtime" HeaderText="时长(H)" HtmlEncode="False">
                            <HeaderStyle CssClass="headfont" Width="60px" />
                            <ItemStyle Width="60px" CssClass="break" />
                        </asp:BoundField>
                        <asp:BoundField DataField="status">
                            <ControlStyle CssClass="hidden" Width="0px" />
                            <FooterStyle CssClass="hidden" Width="0px" />
                            <HeaderStyle CssClass="hidden" Width="0px" />
                            <ItemStyle CssClass="hidden" Width="0px" />
                        </asp:BoundField>
                    </Columns>
                  <%--  <PagerSettings FirstPageText="首页" LastPageText="尾页" Mode="NextPreviousFirstLast"
                        NextPageText="下页" PreviousPageText="上页" />
                    <PagerStyle ForeColor="Red" />--%>
                </asp:GridView>
            </div>
      
   
      <asp:Timer ID="Timer1" runat="server" OnTick="Timer1_Tick">
 
    </asp:Timer>
    </form>
</body>
</html>
