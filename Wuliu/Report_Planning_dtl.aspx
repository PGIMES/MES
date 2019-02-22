<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Report_Planning_dtl.aspx.cs" Inherits="Wuliu_Report_Planning_dtl" %>

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
<body style="background: url(../images/bg.jpg) repeat-x;">
    <form id="form1" runat="server">
    <div>
        <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="年："></dx:ASPxLabel><dx:ASPxLabel ID="lbl_year" runat="server" Text="" ForeColor="Blue"></dx:ASPxLabel>
        &nbsp;&nbsp;
        <dx:ASPxLabel ID="ASPxLabel2" runat="server" Text="周："></dx:ASPxLabel><dx:ASPxLabel ID="lbl_week" runat="server" Text="" ForeColor="Blue"></dx:ASPxLabel>
         &nbsp;&nbsp;
        <dx:ASPxLabel ID="ASPxLabel3" runat="server" Text="部门："></dx:ASPxLabel><dx:ASPxLabel ID="lbl_dept" runat="server" Text="" ForeColor="Blue"></dx:ASPxLabel>

        <dx:aspxgridview ID="gv_tr_hist" runat="server" AutoGenerateColumns="False" KeyFieldName="" Theme="MetropolisBlue"  ClientInstanceName="grid"  EnableTheming="True"
                Visible="false" OnPageIndexChanged="gv_tr_hist_PageIndexChanged">
            <SettingsPager PageSize="1000"></SettingsPager>
            <SettingsBehavior AllowSelectByRowClick="True" AllowDragDrop="False" AllowSort="False" />
            <Settings VerticalScrollBarMode="Visible" VerticalScrollBarStyle="Standard" VerticalScrollableHeight="400" ShowFilterRow="true" ShowFilterRowMenu="true" ShowFooter="True"  />
            <Columns>
                <dx:GridViewDataDateColumn Caption="生效日期" FieldName="tr_effdate" Width="100px" VisibleIndex="1"></dx:GridViewDataDateColumn>                                                               
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

        <dx:aspxgridview ID="gv_xx_wo_mstr" runat="server" AutoGenerateColumns="False" KeyFieldName="" Theme="MetropolisBlue"  ClientInstanceName="grid"  EnableTheming="True"
                Visible="false" OnPageIndexChanged="gv_xx_wo_mstr_PageIndexChanged">
            <SettingsPager PageSize="1000"></SettingsPager>
            <SettingsBehavior AllowSelectByRowClick="True" AllowDragDrop="False" AllowSort="False" />
            <Settings VerticalScrollBarMode="Visible" VerticalScrollBarStyle="Standard" VerticalScrollableHeight="400" ShowFilterRow="true" ShowFilterRowMenu="true" ShowFooter="True"  />
            <Columns>
                <dx:GridViewDataTextColumn Caption="区域" FieldName="scx_area" Width="100px" VisibleIndex="1"></dx:GridViewDataTextColumn>                                                               
                <dx:GridViewDataTextColumn Caption="生产线" FieldName="scx" Width="200px" VisibleIndex="2"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="父工单号" FieldName="xxwo_nbr" Width="100px" VisibleIndex="3"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="物料号" FieldName="xxwo_part" Width="100px" VisibleIndex="4"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="零件号" FieldName="pt_desc1" Width="150px" VisibleIndex="5"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="生产线(QAD)" FieldName="xxwo_line" Width="90px" VisibleIndex="6"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="数量" FieldName="xxwo_qty_ord" Width="80px" VisibleIndex="7">
                    <PropertiesTextEdit DisplayFormatString="{0:N0}"></PropertiesTextEdit>
                </dx:GridViewDataTextColumn>
            </Columns>    
            <TotalSummary>
                <%--<dx:aspxsummaryitem DisplayFormat="<font color='red'>小计</font>" FieldName="rowid" ShowInColumn="rowid" ShowInGroupFooterColumn="rowid" SummaryType="Sum" />--%>
                <dx:aspxsummaryitem DisplayFormat="<font color='red'>{0:N0}</font>" FieldName="xxwo_qty_ord" ShowInColumn="xxwo_qty_ord" ShowInGroupFooterColumn="xxwo_qty_ord" SummaryType="Sum" />
            </TotalSummary>                                          
            <Styles>
                <Header BackColor="#99CCFF"  ></Header>        
                <FocusedRow BackColor="#99CCFF" ForeColor="#0000CC"></FocusedRow>    
            </Styles>                                          
        </dx:aspxgridview>

        <dx:aspxgridview ID="gv_workorder_touchan" runat="server" AutoGenerateColumns="False" KeyFieldName="" Theme="MetropolisBlue"  ClientInstanceName="grid_touchan"  EnableTheming="True"
                Visible="false" OnPageIndexChanged="gv_workorder_touchan_PageIndexChanged">
            <SettingsPager PageSize="1000"></SettingsPager>
            <SettingsBehavior AllowSelectByRowClick="True" AllowDragDrop="False" AllowSort="False"  AllowEllipsisInText="true" />
            <Settings VerticalScrollBarMode="Visible" VerticalScrollBarStyle="Standard" VerticalScrollableHeight="400" ShowFilterRow="true" ShowFilterRowMenu="true" ShowFooter="True"  />
            <Columns>
               <dx:GridViewDataDateColumn Caption="归属日期" FieldName="rep_date" Width="80px" VisibleIndex="1"></dx:GridViewDataDateColumn>
                <dx:GridViewDataTextColumn Caption="操作时间" FieldName="create_date" Width="110px" VisibleIndex="2"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="班别" FieldName="banbie" Width="60px" VisibleIndex="3"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="区域" FieldName="area" Width="70px" VisibleIndex="4"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="生产线" FieldName="line" Width="100px" VisibleIndex="5"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="工单号" FieldName="workorder" Width="70px" VisibleIndex="6"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="物料号" FieldName="pgino" Width="70px" VisibleIndex="7"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="零件号" FieldName="partno" Width="100px" VisibleIndex="8"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="状态" FieldName="xxwo_status" Width="40px" VisibleIndex="8"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="工序" FieldName="op" Width="40px" VisibleIndex="9"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="工序描述" FieldName="op_desc" Width="100px" VisibleIndex="10"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="工号" FieldName="emp" Width="60px" VisibleIndex="11"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="姓名" FieldName="emp_name" Width="60px" VisibleIndex="12"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="合格数量" FieldName="hege_qty" Width="70px" VisibleIndex="13"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="报废数量" FieldName="baofei_qty" Width="70px" VisibleIndex="14"></dx:GridViewDataTextColumn>
            </Columns>    
            <TotalSummary>
                <%--<dx:aspxsummaryitem DisplayFormat="<font color='blue'>小计</font>" FieldName="rowid" ShowInColumn="rowid" ShowInGroupFooterColumn="rowid" SummaryType="Sum" />--%>
                <dx:aspxsummaryitem DisplayFormat="<font color='blue'>{0:N0}</font>" FieldName="hege_qty" ShowInColumn="hege_qty" ShowInGroupFooterColumn="hege_qty" SummaryType="Sum" />
                <dx:aspxsummaryitem DisplayFormat="<font color='blue'>{0:N0}</font>" FieldName="baofei_qty" ShowInColumn="baofei_qty" ShowInGroupFooterColumn="baofei_qty" SummaryType="Sum" />
            </TotalSummary>                                          
            <Styles>
                <Header BackColor="#99CCFF"  ></Header>        
                <FocusedRow BackColor="#99CCFF" ForeColor="#0000CC"></FocusedRow>    
            </Styles>                                          
        </dx:aspxgridview>

        <dx:aspxgridview ID="gv_workorder_ruku" runat="server" AutoGenerateColumns="False" KeyFieldName="" Theme="MetropolisBlue"  ClientInstanceName="grid_ruku"  EnableTheming="True"
                Visible="false" OnPageIndexChanged="gv_workorder_ruku_PageIndexChanged">
            <SettingsPager PageSize="1000"></SettingsPager>
            <SettingsBehavior AllowSelectByRowClick="True" AllowDragDrop="False" AllowSort="False"   AllowEllipsisInText="true"/>
            <Settings VerticalScrollBarMode="Visible" VerticalScrollBarStyle="Standard" VerticalScrollableHeight="400" ShowFilterRow="true" ShowFilterRowMenu="true" ShowFooter="True"  />
            <Columns>
                <dx:GridViewDataDateColumn Caption="归属日期" FieldName="rep_date" Width="80px" VisibleIndex="1"></dx:GridViewDataDateColumn>
                <dx:GridViewDataTextColumn Caption="操作时间" FieldName="create_date" Width="110px" VisibleIndex="2"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="班别" FieldName="banbie" Width="60px" VisibleIndex="3"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="区域" FieldName="area" Width="70px" VisibleIndex="4"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="生产线" FieldName="line" Width="100px" VisibleIndex="5"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="工单号" FieldName="workorder" Width="70px" VisibleIndex="6"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="物料号" FieldName="pgino" Width="70px" VisibleIndex="7"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="零件号" FieldName="partno" Width="100px" VisibleIndex="8"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="状态" FieldName="xxwo_status" Width="40px" VisibleIndex="8"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="工序" FieldName="op" Width="40px" VisibleIndex="9"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="工序描述" FieldName="op_desc" Width="100px" VisibleIndex="10"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="工号" FieldName="emp" Width="60px" VisibleIndex="11"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="姓名" FieldName="emp_name" Width="60px" VisibleIndex="12"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="合格数量" FieldName="hege_qty" Width="70px" VisibleIndex="13"></dx:GridViewDataTextColumn>
            </Columns>    
            <TotalSummary>
                <%--<dx:aspxsummaryitem DisplayFormat="<font color='blue'>小计</font>" FieldName="rowid" ShowInColumn="rowid" ShowInGroupFooterColumn="rowid" SummaryType="Sum" />--%>
                <dx:aspxsummaryitem DisplayFormat="<font color='blue'>{0:N0}</font>" FieldName="baofei_qty" ShowInColumn="baofei_qty" ShowInGroupFooterColumn="baofei_qty" SummaryType="Sum" />
            </TotalSummary>                                          
            <Styles>
                <Header BackColor="#99CCFF"  ></Header>        
                <FocusedRow BackColor="#99CCFF" ForeColor="#0000CC"></FocusedRow>    
            </Styles>                                          
        </dx:aspxgridview>

        <dx:aspxgridview ID="gv_workorder_feipin" runat="server" AutoGenerateColumns="False" KeyFieldName="" Theme="MetropolisBlue"  ClientInstanceName="grid_feipin"  EnableTheming="True"
                Visible="false" OnPageIndexChanged="gv_workorder_feipin_PageIndexChanged">
            <SettingsPager PageSize="1000"></SettingsPager>
            <SettingsBehavior AllowSelectByRowClick="True" AllowDragDrop="False" AllowSort="False"  AllowEllipsisInText="true"/>
            <Settings VerticalScrollBarMode="Visible" VerticalScrollBarStyle="Standard" VerticalScrollableHeight="400" ShowFilterRow="true" ShowFilterRowMenu="true" ShowFooter="True"  />
            <Columns>
                <dx:GridViewDataDateColumn Caption="归属日期" FieldName="rep_date" Width="80px" VisibleIndex="1"></dx:GridViewDataDateColumn>
                <dx:GridViewDataTextColumn Caption="操作时间" FieldName="create_date" Width="110px" VisibleIndex="2"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="班别" FieldName="banbie" Width="60px" VisibleIndex="3"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="区域" FieldName="area" Width="70px" VisibleIndex="4"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="生产线" FieldName="line" Width="100px" VisibleIndex="5"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="工单号" FieldName="workorder" Width="70px" VisibleIndex="6"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="物料号" FieldName="pgino" Width="70px" VisibleIndex="7"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="零件号" FieldName="partno" Width="100px" VisibleIndex="8"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="状态" FieldName="xxwo_status" Width="40px" VisibleIndex="8"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="工序" FieldName="op" Width="40px" VisibleIndex="9"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="工序描述" FieldName="op_desc" Width="100px" VisibleIndex="10"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="工号" FieldName="emp" Width="60px" VisibleIndex="11"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="姓名" FieldName="emp_name" Width="60px" VisibleIndex="12"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="报废数量" FieldName="baofei_qty" Width="70px" VisibleIndex="14"></dx:GridViewDataTextColumn>
            </Columns>    
            <TotalSummary>
                <%--<dx:aspxsummaryitem DisplayFormat="<font color='blue'>小计</font>" FieldName="rowid" ShowInColumn="rowid" ShowInGroupFooterColumn="rowid" SummaryType="Sum" />--%>
                <dx:aspxsummaryitem DisplayFormat="<font color='blue'>{0:N0}</font>" FieldName="baofei_qty" ShowInColumn="baofei_qty" ShowInGroupFooterColumn="baofei_qty" SummaryType="Sum" />
            </TotalSummary>                                          
            <Styles>
                <Header BackColor="#99CCFF"  ></Header>        
                <FocusedRow BackColor="#99CCFF" ForeColor="#0000CC"></FocusedRow>    
            </Styles>                                          
        </dx:aspxgridview>

        <dx:aspxgridview ID="gv_workorder_shangque" runat="server" AutoGenerateColumns="False" KeyFieldName="" Theme="MetropolisBlue"  ClientInstanceName="grid"  EnableTheming="True"
                Visible="false" OnPageIndexChanged="gv_workorder_shangque_PageIndexChanged">
            <SettingsPager PageSize="1000"></SettingsPager>
            <SettingsBehavior AllowSelectByRowClick="True" AllowDragDrop="False" AllowSort="False" />
            <Settings VerticalScrollBarMode="Visible" VerticalScrollBarStyle="Standard" VerticalScrollableHeight="400" ShowFilterRow="true" ShowFilterRowMenu="true" ShowFooter="True"  />
            <Columns>
                <dx:GridViewDataTextColumn Caption="区域" FieldName="area" Width="100px" VisibleIndex="1"></dx:GridViewDataTextColumn>                                                               
                <dx:GridViewDataTextColumn Caption="生产线" FieldName="line" Width="200px" VisibleIndex="2"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="工单号" FieldName="workorder" Width="100px" VisibleIndex="3"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="物料号" FieldName="pgino" Width="100px" VisibleIndex="4"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="零件号" FieldName="partno" Width="150px" VisibleIndex="5"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="状态" FieldName="xxwo_status" Width="40px" VisibleIndex="6"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="当前工序" FieldName="xxwo_current_op" Width="70px" VisibleIndex="7"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="数量" FieldName="qty" Width="80px" VisibleIndex="8">
                    <PropertiesTextEdit DisplayFormatString="{0:N0}"></PropertiesTextEdit>
                </dx:GridViewDataTextColumn>
            </Columns>    
            <TotalSummary>
                <%--<dx:aspxsummaryitem DisplayFormat="<font color='red'>小计</font>" FieldName="rowid" ShowInColumn="rowid" ShowInGroupFooterColumn="rowid" SummaryType="Sum" />--%>
                <dx:aspxsummaryitem DisplayFormat="<font color='red'>{0:N0}</font>" FieldName="qty" ShowInColumn="qty" ShowInGroupFooterColumn="qty" SummaryType="Sum" />
            </TotalSummary>                                          
            <Styles>
                <Header BackColor="#99CCFF"  ></Header>        
                <FocusedRow BackColor="#99CCFF" ForeColor="#0000CC"></FocusedRow>    
            </Styles>                                          
        </dx:aspxgridview>

        <dx:aspxgridview ID="gv_workorder_GP" runat="server" AutoGenerateColumns="False" KeyFieldName="" Theme="MetropolisBlue"  ClientInstanceName="grid"  EnableTheming="True"
                Visible="false" OnPageIndexChanged="gv_workorder_GP_PageIndexChanged">
            <SettingsPager PageSize="1000"></SettingsPager>
            <SettingsBehavior AllowSelectByRowClick="True" AllowDragDrop="False" AllowSort="False" />
            <Settings VerticalScrollBarMode="Visible" VerticalScrollBarStyle="Standard" VerticalScrollableHeight="400" ShowFilterRow="true" ShowFilterRowMenu="true" ShowFooter="True"  />
            <Columns>
                <dx:GridViewDataTextColumn Caption="区域" FieldName="area" Width="100px" VisibleIndex="1"></dx:GridViewDataTextColumn>                                                               
                <dx:GridViewDataTextColumn Caption="生产线" FieldName="line" Width="200px" VisibleIndex="2"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="工单号" FieldName="workorder" Width="100px" VisibleIndex="3"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="物料号" FieldName="pgino" Width="100px" VisibleIndex="4"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="零件号" FieldName="partno" Width="150px" VisibleIndex="5"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="状态" FieldName="xxwo_status" Width="40px" VisibleIndex="6"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="当前工序" FieldName="xxwo_current_op" Width="70px" VisibleIndex="7"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="数量" FieldName="qty" Width="80px" VisibleIndex="8">
                    <PropertiesTextEdit DisplayFormatString="{0:N0}"></PropertiesTextEdit>
                </dx:GridViewDataTextColumn>
            </Columns>    
            <TotalSummary>
                <%--<dx:aspxsummaryitem DisplayFormat="<font color='red'>小计</font>" FieldName="rowid" ShowInColumn="rowid" ShowInGroupFooterColumn="rowid" SummaryType="Sum" />--%>
                <dx:aspxsummaryitem DisplayFormat="<font color='red'>{0:N0}</font>" FieldName="qty" ShowInColumn="qty" ShowInGroupFooterColumn="qty" SummaryType="Sum" />
            </TotalSummary>                                          
            <Styles>
                <Header BackColor="#99CCFF"  ></Header>        
                <FocusedRow BackColor="#99CCFF" ForeColor="#0000CC"></FocusedRow>    
            </Styles>                                          
        </dx:aspxgridview>

        <dx:aspxgridview ID="gv_workorder_N_GP" runat="server" AutoGenerateColumns="False" KeyFieldName="" Theme="MetropolisBlue"  ClientInstanceName="grid"  EnableTheming="True"
                Visible="false" OnPageIndexChanged="gv_workorder_N_GP_PageIndexChanged">
            <SettingsPager PageSize="1000"></SettingsPager>
            <SettingsBehavior AllowSelectByRowClick="True" AllowDragDrop="False" AllowSort="False" />
            <Settings VerticalScrollBarMode="Visible" VerticalScrollBarStyle="Standard" VerticalScrollableHeight="400" ShowFilterRow="true" ShowFilterRowMenu="true" ShowFooter="True"  />
            <Columns>
                <dx:GridViewDataTextColumn Caption="区域" FieldName="area" Width="100px" VisibleIndex="1"></dx:GridViewDataTextColumn>                                                               
                <dx:GridViewDataTextColumn Caption="生产线" FieldName="line" Width="200px" VisibleIndex="2"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="工单号" FieldName="workorder" Width="100px" VisibleIndex="3"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="物料号" FieldName="pgino" Width="100px" VisibleIndex="4"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="零件号" FieldName="partno" Width="150px" VisibleIndex="5"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="状态" FieldName="xxwo_status" Width="40px" VisibleIndex="6"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="当前工序" FieldName="xxwo_current_op" Width="70px" VisibleIndex="7"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="数量" FieldName="qty" Width="80px" VisibleIndex="8">
                    <PropertiesTextEdit DisplayFormatString="{0:N0}"></PropertiesTextEdit>
                </dx:GridViewDataTextColumn>
            </Columns>    
            <TotalSummary>
                <%--<dx:aspxsummaryitem DisplayFormat="<font color='red'>小计</font>" FieldName="rowid" ShowInColumn="rowid" ShowInGroupFooterColumn="rowid" SummaryType="Sum" />--%>
                <dx:aspxsummaryitem DisplayFormat="<font color='red'>{0:N0}</font>" FieldName="qty" ShowInColumn="qty" ShowInGroupFooterColumn="qty" SummaryType="Sum" />
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
