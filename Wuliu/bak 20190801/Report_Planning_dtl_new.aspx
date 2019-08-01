<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Report_Planning_dtl_new.aspx.cs" Inherits="Wuliu_Report_Planning_dtl_new" %>

<%@ Register assembly="DevExpress.Web.v17.2, Version=17.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web" tagprefix="dx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <script type="text/javascript" src="/Content/js/jquery.min.js"></script>
    <script src="/Content/js/layer/layer.js"></script>
    <link href="/Content/css/font-awesome.min.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server" style="background: url(../images/bg.jpg) repeat-x;">
    <div>
        <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="年："></dx:ASPxLabel><dx:ASPxLabel ID="lbl_year" runat="server" Text="" ForeColor="Blue"></dx:ASPxLabel>
        &nbsp;&nbsp;
        <dx:ASPxLabel ID="ASPxLabel2" runat="server" Text="周："></dx:ASPxLabel><dx:ASPxLabel ID="lbl_week" runat="server" Text="" ForeColor="Blue"></dx:ASPxLabel>
         &nbsp;&nbsp;
        <dx:ASPxLabel ID="ASPxLabel3" runat="server" Text="部门："></dx:ASPxLabel><dx:ASPxLabel ID="lbl_dept" runat="server" Text="" ForeColor="Blue"></dx:ASPxLabel>
         &nbsp;&nbsp;
        <asp:Button ID="btnimport" runat="server" Text="导出Excel"  style="font-size:12px;" OnClick="btnimport_Click" />
        <dx:aspxgridview ID="gv_tr_hist" runat="server" AutoGenerateColumns="False" KeyFieldName="tr_trnbr" Theme="MetropolisBlue"  ClientInstanceName="grid_tr_hist"  EnableTheming="True"
                Visible="false" OnPageIndexChanged="gv_tr_hist_PageIndexChanged">
            <SettingsPager PageSize="1000"></SettingsPager>
            <SettingsBehavior AllowSelectByRowClick="True" AllowDragDrop="False" AllowSort="False" />
            <Settings VerticalScrollBarMode="Visible" VerticalScrollBarStyle="Standard" VerticalScrollableHeight="350" ShowFilterRow="true" ShowFilterRowMenu="true" ShowFooter="True"  />
            <Columns>
                <dx:GridViewDataDateColumn Caption="生效日期" FieldName="tr_effdate" Width="100px" VisibleIndex="1">
                    <PropertiesDateEdit DisplayFormatString="yyyy-MM-dd"></PropertiesDateEdit>
                </dx:GridViewDataDateColumn>                                                               
                <dx:GridViewDataTextColumn Caption="事务号" FieldName="tr_trnbr" Width="70px" VisibleIndex="2"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="事务类型" FieldName="tr_type" Width="80px" VisibleIndex="3"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="物料号" FieldName="tr_part" Width="80px" VisibleIndex="4"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="地点" FieldName="tr_site" Width="80px" VisibleIndex="5"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="库位数量更改" FieldName="tr_qty_loc" Width="100px" VisibleIndex="6">
                    <PropertiesTextEdit DisplayFormatString="{0:N0}"></PropertiesTextEdit>
                </dx:GridViewDataTextColumn>
            </Columns>    
            <TotalSummary>
                <%--<dx:aspxsummaryitem DisplayFormat="<font color='red'>小计</font>" FieldName="rowid" ShowInColumn="rowid" ShowInGroupFooterColumn="rowid" SummaryType="Sum" />--%>
                <dx:aspxsummaryitem DisplayFormat="<font color='red'>{0:N0}</font>"  FieldName="tr_qty_loc" ShowInColumn="tr_qty_loc" ShowInGroupFooterColumn="tr_qty_loc" SummaryType="Sum" />
            </TotalSummary>                                          
            <Styles>
                <Header BackColor="#99CCFF"  ></Header>        
                <FocusedRow BackColor="#99CCFF" ForeColor="#0000CC"></FocusedRow>    
            </Styles>                                          
        </dx:aspxgridview>

        <dx:aspxgridview ID="gv_xx_wo_mstr" runat="server" AutoGenerateColumns="False" KeyFieldName="xxwo_nbr" Theme="MetropolisBlue"  ClientInstanceName="grid_xx_wo_mstr"  EnableTheming="True"
                Visible="false" OnPageIndexChanged="gv_xx_wo_mstr_PageIndexChanged">
            <SettingsPager PageSize="1000"></SettingsPager>
            <SettingsBehavior AllowSelectByRowClick="True" AllowDragDrop="False" AllowSort="true" AllowEllipsisInText="true" SortMode="Value"  />
            <Settings VerticalScrollBarMode="Visible" VerticalScrollBarStyle="Standard" VerticalScrollableHeight="350" ShowFilterRow="true" ShowFilterRowMenu="true" ShowFooter="True"  />
            <Columns>
                <dx:GridViewDataTextColumn Caption="序号" FieldName="rownum" Width="40px" VisibleIndex="0"></dx:GridViewDataTextColumn> 
                <dx:GridViewDataTextColumn Caption="区域" FieldName="scx_area" Width="80px" VisibleIndex="1"></dx:GridViewDataTextColumn>  
                <dx:GridViewDataTextColumn Caption="生产线" FieldName="scx" Width="110px" VisibleIndex="2"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="父工单号" FieldName="xxwo_nbr" Width="70px" VisibleIndex="3"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="使用部门" FieldName="sydept" Width="70px" VisibleIndex="3"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="物料号" FieldName="xxwo_part" Width="70px" VisibleIndex="4"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="零件号" FieldName="pt_desc1" Width="110px" VisibleIndex="5"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="生产线(QAD)" FieldName="xxwo_line" Width="75px" VisibleIndex="6"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="开始数量" FieldName="kaishi_qty" Width="70px" VisibleIndex="7">
                    <PropertiesTextEdit DisplayFormatString="{0:N0}"></PropertiesTextEdit>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="拆分数量" FieldName="chaifei_qty" Width="70px" VisibleIndex="8">
                    <PropertiesTextEdit DisplayFormatString="{0:N0}"></PropertiesTextEdit>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="投产数量" FieldName="touchan_qty" Width="70px" VisibleIndex="9">
                    <PropertiesTextEdit DisplayFormatString="{0:N0}"></PropertiesTextEdit>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="入库数量" FieldName="ruku_qty" Width="70px" VisibleIndex="10">
                    <PropertiesTextEdit DisplayFormatString="{0:N0}"></PropertiesTextEdit>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="生产计划" FieldName="jihua_qty" Width="70px" VisibleIndex="11">
                    <PropertiesTextEdit DisplayFormatString="{0:N0}"></PropertiesTextEdit>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="差异" FieldName="chayi_qty" Width="70px" VisibleIndex="12" SortOrder="Descending"
                    ToolTip="二车间：数量=生产计划-拆分数量；其他车间：开始数量-投产数量；">
                    <PropertiesTextEdit DisplayFormatString="{0:N0}"></PropertiesTextEdit>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="重量" FieldName="ps_qty_per" Width="70px" VisibleIndex="13">
                    <PropertiesTextEdit DisplayFormatString="{0:N3}"></PropertiesTextEdit>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="总重量" FieldName="ps_qty_per_qty" Width="70px" VisibleIndex="14">
                    <PropertiesTextEdit DisplayFormatString="{0:N0}"></PropertiesTextEdit>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataDateColumn Caption="日期" FieldName="xxwo_ord_date" Width="80px" VisibleIndex="15">
                    <PropertiesDateEdit DisplayFormatString="yyyy-MM-dd"></PropertiesDateEdit>
                </dx:GridViewDataDateColumn>  
            </Columns>    
            <TotalSummary>
                <%--<dx:aspxsummaryitem DisplayFormat="<font color='red'>小计</font>" FieldName="rowid" ShowInColumn="rowid" ShowInGroupFooterColumn="rowid" SummaryType="Sum" />--%>
                <dx:aspxsummaryitem DisplayFormat="<font color='red'>{0:N0}</font>" FieldName="kaishi_qty" ShowInColumn="kaishi_qty" ShowInGroupFooterColumn="kaishi_qty" SummaryType="Sum" />
                <dx:aspxsummaryitem DisplayFormat="<font color='red'>{0:N0}</font>" FieldName="chaifei_qty" ShowInColumn="chaifei_qty" ShowInGroupFooterColumn="chaifei_qty" SummaryType="Sum" />
                <dx:aspxsummaryitem DisplayFormat="<font color='red'>{0:N0}</font>" FieldName="touchan_qty" ShowInColumn="touchan_qty" ShowInGroupFooterColumn="touchan_qty" SummaryType="Sum" />
                <dx:aspxsummaryitem DisplayFormat="<font color='red'>{0:N0}</font>" FieldName="ruku_qty" ShowInColumn="ruku_qty" ShowInGroupFooterColumn="ruku_qty" SummaryType="Sum" />
                <dx:aspxsummaryitem DisplayFormat="<font color='red'>{0:N0}</font>" FieldName="jihua_qty" ShowInColumn="jihua_qty" ShowInGroupFooterColumn="jihua_qty" SummaryType="Sum" />
                <dx:aspxsummaryitem DisplayFormat="<font color='red'>{0:N0}</font>" FieldName="ps_qty_per_qty" ShowInColumn="ps_qty_per_qty" ShowInGroupFooterColumn="ps_qty_per_qty" SummaryType="Sum" />
            </TotalSummary>                                          
            <Styles>
                <Header BackColor="#99CCFF"  ></Header>        
                <FocusedRow BackColor="#99CCFF" ForeColor="#0000CC"></FocusedRow>    
            </Styles>                                          
        </dx:aspxgridview>

        <dx:aspxgridview ID="gv_workorder" runat="server" AutoGenerateColumns="False" KeyFieldName="workorder" Theme="MetropolisBlue"  ClientInstanceName="grid_xx_wo_mstr"  EnableTheming="True"
                Visible="false" OnPageIndexChanged="gv_workorder_PageIndexChanged">
            <SettingsPager PageSize="1000"></SettingsPager>
            <SettingsBehavior AllowSelectByRowClick="True" AllowDragDrop="False" AllowSort="False" AllowEllipsisInText="true" />
            <Settings VerticalScrollBarMode="Visible" VerticalScrollBarStyle="Standard" VerticalScrollableHeight="350" ShowFilterRow="true" ShowFilterRowMenu="true" ShowFooter="True"  />
            <Columns>
                <dx:GridViewDataTextColumn Caption="序号" FieldName="rownum" Width="40px" VisibleIndex="0"></dx:GridViewDataTextColumn> 
                <dx:GridViewDataTextColumn Caption="区域" FieldName="area" Width="80px" VisibleIndex="1"></dx:GridViewDataTextColumn>   
                <dx:GridViewDataTextColumn Caption="生产线" FieldName="line" Width="110px" VisibleIndex="2"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="工单号" FieldName="workorder" Width="70px" VisibleIndex="3"></dx:GridViewDataTextColumn>
                <%--<dx:GridViewDataTextColumn Caption="产品大类" FieldName="" Width="70px" VisibleIndex="4"></dx:GridViewDataTextColumn>--%>
                <dx:GridViewDataTextColumn Caption="物料号" FieldName="pgino" Width="70px" VisibleIndex="5"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="零件号" FieldName="partno" Width="110px" VisibleIndex="6"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="状态" FieldName="xxwo_status" Width="40px" VisibleIndex="6"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="订单数量" FieldName="dingdan_qty" Width="70px" VisibleIndex="7">
                    <PropertiesTextEdit DisplayFormatString="{0:N0}"></PropertiesTextEdit>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="投产数量" FieldName="touchan_qty" Width="70px" VisibleIndex="8">
                    <PropertiesTextEdit DisplayFormatString="{0:N0}"></PropertiesTextEdit>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="已完成数量" FieldName="yiwan_qty" Width="70px" VisibleIndex="9">
                    <PropertiesTextEdit DisplayFormatString="{0:N0}"></PropertiesTextEdit>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="尚缺数量" FieldName="sque_qty" Width="70px" VisibleIndex="10" 
                    ToolTip="投产数量-报废数量-入库数量">
                    <PropertiesTextEdit DisplayFormatString="{0:N0}"></PropertiesTextEdit>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="尚缺数量2" FieldName="sque_qty2" Width="70px" VisibleIndex="10"
                    ToolTip="若投产：数量=[生产3部(投产数量)][其他车间(订单数量)]-已完成数量；若没投产：数量=0">
                    <PropertiesTextEdit DisplayFormatString="{0:N0}"></PropertiesTextEdit>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="报废入库数量" FieldName="baofei_qty" Width="70px" VisibleIndex="11">
                    <PropertiesTextEdit DisplayFormatString="{0:N0}"></PropertiesTextEdit>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="工废入库数量" FieldName="gongfei_qty" Width="70px" VisibleIndex="12">
                    <PropertiesTextEdit DisplayFormatString="{0:N0}"></PropertiesTextEdit>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="料废入库数量" FieldName="liaofei_qty" Width="70px" VisibleIndex="13">
                    <PropertiesTextEdit DisplayFormatString="{0:N0}"></PropertiesTextEdit>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="合格入库数量" FieldName="hege_qty" Width="70px" VisibleIndex="13">
                    <PropertiesTextEdit DisplayFormatString="{0:N0}"></PropertiesTextEdit>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="开始报工日期" FieldName="start_bg_date" Width="120px" VisibleIndex="13"></dx:GridViewDataTextColumn>  
                <dx:GridViewDataTextColumn Caption="最后报工日期" FieldName="end_bg_date" Width="120px" VisibleIndex="14"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="时长" FieldName="xiaohao_times" Width="60px" VisibleIndex="15"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="当前工序" FieldName="xxwo_current_op" Width="70px" VisibleIndex="16"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="父工单号" FieldName="xxwo_nbr" Width="70px" VisibleIndex="17"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="生产线(QAD)" FieldName="xxwo_line" Width="75px" VisibleIndex="18"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="类型" FieldName="xxwo__chr01_desc" Width="75px" VisibleIndex="19"></dx:GridViewDataTextColumn>
            </Columns>    
            <TotalSummary>
                <%--<dx:aspxsummaryitem DisplayFormat="<font color='red'>小计</font>" FieldName="rowid" ShowInColumn="rowid" ShowInGroupFooterColumn="rowid" SummaryType="Sum" />--%>
                <dx:aspxsummaryitem DisplayFormat="<font color='red'>{0:N0}</font>" FieldName="dingdan_qty" ShowInColumn="dingdan_qty" ShowInGroupFooterColumn="dingdan_qty" SummaryType="Sum" />
                <dx:aspxsummaryitem DisplayFormat="<font color='red'>{0:N0}</font>" FieldName="touchan_qty" ShowInColumn="touchan_qty" ShowInGroupFooterColumn="touchan_qty" SummaryType="Sum" />
                <dx:aspxsummaryitem DisplayFormat="<font color='red'>{0:N0}</font>" FieldName="yiwan_qty" ShowInColumn="yiwan_qty" ShowInGroupFooterColumn="yiwan_qty" SummaryType="Sum" />
                <dx:aspxsummaryitem DisplayFormat="<font color='red'>{0:N0}</font>" FieldName="sque_qty" ShowInColumn="sque_qty" ShowInGroupFooterColumn="sque_qty" SummaryType="Sum" />
                <dx:aspxsummaryitem DisplayFormat="<font color='red'>{0:N0}</font>" FieldName="sque_qty2" ShowInColumn="sque_qty2" ShowInGroupFooterColumn="sque_qty2" SummaryType="Sum" />
                <dx:aspxsummaryitem DisplayFormat="<font color='red'>{0:N0}</font>" FieldName="baofei_qty" ShowInColumn="baofei_qty" ShowInGroupFooterColumn="baofei_qty" SummaryType="Sum" />
                <dx:aspxsummaryitem DisplayFormat="<font color='red'>{0:N0}</font>" FieldName="gongfei_qty" ShowInColumn="gongfei_qty" ShowInGroupFooterColumn="gongfei_qty" SummaryType="Sum" />
                <dx:aspxsummaryitem DisplayFormat="<font color='red'>{0:N0}</font>" FieldName="liaofei_qty" ShowInColumn="liaofei_qty" ShowInGroupFooterColumn="liaofei_qty" SummaryType="Sum" />
                <dx:aspxsummaryitem DisplayFormat="<font color='red'>{0:N0}</font>" FieldName="hege_qty" ShowInColumn="hege_qty" ShowInGroupFooterColumn="hege_qty" SummaryType="Sum" />
            </TotalSummary>                                          
            <Styles>
                <Header BackColor="#99CCFF"  ></Header>        
                <FocusedRow BackColor="#99CCFF" ForeColor="#0000CC"></FocusedRow>    
            </Styles>                                          
        </dx:aspxgridview>

    </div>
    </form>
</body>
</html>
