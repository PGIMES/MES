<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PO_Contract_Modify.aspx.cs" Inherits="Forms_PurChase_PO_Contract_Modify" %>

<%@ Register Assembly="DevExpress.Web.v17.2, Version=17.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>【修改合同计划】</title>
    <link href="/Content/css/bootstrap.min.css" rel="stylesheet" />
    <script src="/Content/js/jquery.min.js"></script>
    <script src="/Content/js/bootstrap.min.js"></script>
    <script src="/Content/js/layer/layer.js"></script>
    <script type="text/javascript">

        $(document).ready(function () {
            grid_read();
        });
        function validate() {
            if (SignDate_C.GetValue() == "" || SignDate_C.GetValue() == null) {
                layer.alert("【签订日期】不可为空");
                return false;
            }
            return true;
        }

        function grid_read() {
            var bf = true;
            $("[id$=gv] tr[class*=DataRow]").each(function (index, item) {
                var fkamt = $(item).find("td").last().text();//已付款金额
                if (Number(fkamt) > 0) {//已付款金额>0,设置只读
                    $(item).find("table[id*=PayRate]").addClass("dxeTextBox_read");
                    $(item).find("input[id*=PayRate]").attr("readOnly", "readOnly").addClass("dxeTextBox_read");

                    $(item).find("table[id*=FPAmount]").addClass("dxeTextBox_read");
                    $(item).find("input[id*=FPAmount]").attr("readOnly", "readOnly").addClass("dxeTextBox_read");

                    $(item).find("table[id*=Remark]").addClass("dxeTextBox_read");
                    $(item).find("input[id*=Remark]").attr("readOnly", "readOnly").addClass("dxeTextBox_read");

                    (eval('PlanPayDate' + index)).SetEnabled(false);
                    (eval('FPDate' + index)).SetEnabled(false);

                    (eval('PayClause' + index)).SetEnabled(false);
                    (eval('PayFunc' + index)).SetEnabled(false);
                    (eval('PayFile' + index)).SetEnabled(false);

                } else {
                    bf = false;
                }
            });

            if (bf == false) {//在判断合同状态：关闭or作废
                $.ajax({
                    type: "post",
                    url: "PO_Contract_Modify.aspx/check_data",
                    data: "{'nbr':'" + $("#SysContractNo").val() + "','domain':'" + $("#txt_domain").val() + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    async: false,//默认是true，异步；false为同步，此方法执行完在执行下面代码
                    success: function (data) {
                        var obj = eval(data.d);
                        if (obj[0].re_flag != "") {
                            bf = true;
                        }
                    }

                });
            }

            if (bf) {
                $("#btn_save").hide(); 
                $("#btnadd").hide(); $("#btndel").hide(); $("#btnsave").hide();
            }
        }

    </script>
    <style type="text/css">
        .row {
            margin-right: 2px;
            margin-left: 2px;
        }

        .textalign {
            text-align: right;
        }

        .alignRight {
            padding-right: 4px;
            text-align: right;
        }

        .row-container {
            padding-left: 2px;
            padding-right: 2px;
        }

        fieldset {
            background: rgba(255,255,255,.3);
            border-color: lightblue;
            border-style: solid;
            border-width: 2px;
            -moz-border-radius: 5px;
            -webkit-border-radius: 5px;
            -khtml-border-radius: 5px;
            border-radius: 5px;
            /*line-height: 30px;*/
            list-style: none;
            padding: 5px 10px;
            margin-bottom: 2px;
        }

        legend {
            color: #302A2A;
            font: bold 16px/2 Verdana, Geneva, sans-serif;
            font-weight: bold;
            text-align: left;
            /*text-shadow: 2px 2px 2px rgb(88, 126, 156);*/
        }

        fieldset {
            padding: .35em .625em .75em;
            margin: 0 2px;
            border: 1px solid lightblue;
        }

        legend {
            padding: 5px;
            border: 0;
            width: auto;
            margin-bottom: 2px;
        }

        .panel {
            margin-bottom: 5px;
        }

        .panel-heading {
            padding: 5px 5px 5px 5px;
        }

        .panel-body {
            padding: 5px 5px 5px 5px;
        }

        body {
            margin-left: 5px;
            margin-right: 5px;
        }

        .col-lg-1, .col-lg-10, .col-lg-11, .col-lg-12, .col-lg-2, .col-lg-3, .col-lg-4, .col-lg-5, .col-lg-6, .col-lg-7, .col-lg-8, .col-lg-9, .col-md-1, .col-md-10, .col-md-11, .col-md-12, .col-md-2, .col-md-3, .col-md-4, .col-md-5, .col-md-6, .col-md-7, .col-md-8, .col-md-9, .col-sm-1, .col-sm-10, .col-sm-11, .col-sm-12, .col-sm-2, .col-sm-3, .col-sm-4, .col-sm-5, .col-sm-6, .col-sm-7, .col-sm-8, .col-sm-9, .col-xs-1, .col-xs-10, .col-xs-11, .col-xs-12, .col-xs-2, .col-xs-3, .col-xs-4, .col-xs-5, .col-xs-6, .col-xs-7, .col-xs-8, .col-xs-9 {
            position: relative;
            min-height: 1px;
            padding-right: 1px;
            padding-left: 1px;
            margin-top: 0px;
            top: 0px;
            left: 0px;
        }

        td {
            /* vertical-align: top;
            font-weight: 600;*/
            font-size: 12px;
            padding-bottom: 5px;
            padding-left: 3px;
            white-space: nowrap;
        }

        p.MsoListParagraph {
            margin-bottom: .0001pt;
            text-align: justify;
            text-justify: inter-ideograph;
            text-indent: 21.0pt;
            font-size: 10.5pt;
            font-family: "Calibri","sans-serif";
            margin-left: 0cm;
            margin-right: 0cm;
            margin-top: 0cm;
        }

        .his {
            padding-left: 8px;
            padding-right: 8px;
        }

        .tbl td {
            border: 1px solid black;
            padding-left: 3px;
            padding-right: 3px;
            padding-top: 3px;
        }

        .wrap {
            word-break: break-all;
            white-space: normal;
        }
         .hidden { display:none;}
        .hidden1 {
            border: 0px;
            overflow: hidden;
        }
    </style>
    
    <style>
        .lineread{
            /*font-size:9px;*/ border:none; border-bottom:1px solid #ccc;
        }
        .linewrite{
            /*font-size:9px;*/ border:none; border-bottom:1px solid #ccc;background-color:#FDF7D9;/*EFEFEF*/
        }
        .dxeTextBox_read{
            border:none !important ; background-color:transparent;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

    <div class="col-md-12" >

        <div class="row  row-container">
            <div class="panel panel-info">
                <div class="panel-heading" data-toggle="collapse" data-target="#CPXX">
                    <strong>签订日期</strong>
                </div>
                <div class="panel-body" id="CPXX">
                    <div class="col-xs-12 col-sm-12  col-md-12 col-lg-12" style="width:490px;">
                        <div>
                            <table style="width:100%; font-size:12px; line-height:35px;" border="0" id="tblWLLeibie">
                                <tr>
                                    <td>域</td>
                                    <td><asp:TextBox ID="txt_domain" runat="server" ReadOnly="true" CssClass="lineread" Width="40px" Height="27px"></asp:TextBox></td>
                                    <td>系统合同号</td>
                                    <td><asp:TextBox ID="SysContractNo" runat="server" ReadOnly="true" CssClass="lineread" Width="100px" Height="27px"></asp:TextBox></td>
                                    <td>合同总金额(原币)</td>
                                    <td><asp:TextBox ID="TotalPay" runat="server" ReadOnly="true" CssClass="lineread" Width="100px" Height="27px"></asp:TextBox></td>
                                    <td>签订日期</td>
                                    <td>
                                        <dx:ASPxDateEdit ID="SignDate" runat="server" EditFormat="Custom" Width="100px" Height="27px" UseMaskBehavior="true" EditFormatString="yyyy/MM/dd"
                                            ClientInstanceName="SignDate_C" 
                                            Border-BorderStyle="None" BorderBottom-BorderStyle="Solid" ButtonStyle-BorderBottom-BorderColor="#ccc" BackColor="#FDF7D9" >
                                            <CalendarProperties>
                                                <FastNavProperties DisplayMode="Inline" />
                                            </CalendarProperties>
                                        </dx:ASPxDateEdit>
                                    </td>
                                    <td>
                                        <asp:Button ID="btn_save" runat="server" Text="保存" class="btn btn-large btn-primary" OnClientClick="if(validate()==false)return false;" Width="50px"
                                             OnClick="btn_save_Click" /> 
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div class="row  row-container">
            <div class="panel panel-info">
                <div class="panel-heading" data-toggle="collapse" data-target="#CPXX1">
                    <strong>计划付款信息</strong>
                </div>
                <div class="panel-body " id="CPXX1">
                    <div class="col-xs-12 col-sm-12  col-md-12 col-lg-12">
                        <asp:UpdatePanel runat="server" ID="p1" UpdateMode="Conditional" >
                            <ContentTemplate>
                                <script type="text/jscript">
                                    var prm = Sys.WebForms.PageRequestManager.getInstance();
                                    prm.add_endRequest(function () {
                                        grid_read();
                                    });
                                </script>

                                <asp:Button ID="btnadd" runat="server" Text="新增" class="btn btn-default btn-primary" Width="50px" OnClick="btnadd_Click" />
                                <asp:Button ID="btndel" runat="server" Text="删除" class="btn btn-default btn-primary" Width="50px"  OnClick="btndel_Click" />
                                <asp:Button ID="btnsave" runat="server" Text="保存" class="btn btn-large btn-primary" Width="50px"  OnClick="btnsave_Click" /> 

                                <dx:aspxgridview ID="gv" runat="server" AutoGenerateColumns="False"  KeyFieldName="ContractLine"  Theme="MetropolisBlue"
                                    ClientInstanceName="gv"  EnableTheming="True" Border-BorderColor="#DCDCDC">
                                    <SettingsPager PageSize="1000" />
                                    <Settings ShowFooter="True"/>
                                    <SettingsBehavior AllowSelectByRowClick="True" AllowDragDrop="False" AllowSort="False" />
                                    <Columns>
                                        <dx:GridViewCommandColumn SelectAllCheckboxMode="Page" ShowClearFilterButton="true" ShowSelectCheckbox="true" Name="Sel" Width="30" VisibleIndex="0"></dx:GridViewCommandColumn>
                                        <dx:GridViewDataTextColumn Caption="行号" FieldName="ContractLine" Width="40px" VisibleIndex="1"></dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="计划付款日期" FieldName="PlanPayDate" Width="90px" VisibleIndex="2">
                                            <Settings AllowCellMerge="False" />
                                            <DataItemTemplate>                
                                                <dx:ASPxDateEdit ID="PlanPayDate" runat="server" EditFormat="Custom" Width="100px"  UseMaskBehavior="true" EditFormatString="yyyy/MM/dd"
                                                    ClientInstanceName='<%# "PlanPayDate"+Container.VisibleIndex.ToString() %>' 
                                                    Border-BorderStyle="None" BorderBottom-BorderStyle="Solid" ButtonStyle-BorderBottom-BorderColor="#ccc" BackColor="#FDF7D9"
                                                    DisabledStyle-BackColor="Transparent" DisabledStyle-BorderBottom-BorderStyle="None">
                                                    <CalendarProperties><FastNavProperties DisplayMode="Inline" /></CalendarProperties>
                                                </dx:ASPxDateEdit>          
                                            </DataItemTemplate>        
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="付款比例" FieldName="PayRate" Width="50px" VisibleIndex="3">
                                            <Settings AllowCellMerge="False"/>
                                            <DataItemTemplate>
                                                <asp:TextBox ID="PayRate" Width="50px" runat="server" Text='<%# Eval("PayRate")%>' AutoPostBack="true" OnTextChanged="PayRate_TextChanged"></asp:TextBox>
                                            </DataItemTemplate>        
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="付款金额" FieldName="PayMoney" Width="80px" VisibleIndex="6" CellStyle-HorizontalAlign="Left"></dx:GridViewDataTextColumn> 
                                        <dx:GridViewDataTextColumn Caption="付款条款" FieldName="PayClause" Width="80px" VisibleIndex="7">
                                            <Settings AllowCellMerge="False"/>
                                            <DataItemTemplate>
                                                <dx:ASPxComboBox ID="PayClause" runat="server" ValueType="System.String"
                                                    Width="80px" ClientInstanceName='<%# "PayClause"+Container.VisibleIndex.ToString() %>'>
                                                </dx:ASPxComboBox>
                                            </DataItemTemplate>        
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="付款方式" FieldName="PayFunc" Width="80px" VisibleIndex="8">
                                            <Settings AllowCellMerge="False"/>
                                            <DataItemTemplate>
                                                <dx:ASPxComboBox ID="PayFunc" runat="server" ValueType="System.String"
                                                    Width="80px" ClientInstanceName='<%# "PayFunc"+Container.VisibleIndex.ToString() %>'>
                                                </dx:ASPxComboBox>
                                            </DataItemTemplate> 
                                        </dx:GridViewDataTextColumn> 
                                        <dx:GridViewDataTextColumn Caption="付款凭据" FieldName="PayFile" Width="80px" VisibleIndex="9">
                                            <Settings AllowCellMerge="False"/>
                                            <DataItemTemplate>
                                                <dx:ASPxComboBox ID="PayFile" runat="server" ValueType="System.String"
                                                    Width="80px" ClientInstanceName='<%# "PayFile"+Container.VisibleIndex.ToString() %>'>
                                                </dx:ASPxComboBox>
                                            </DataItemTemplate> 
                                        </dx:GridViewDataTextColumn> 
                                        <dx:GridViewDataTextColumn Caption="发票日期" FieldName="FPDate" Width="90px" VisibleIndex="10">
                                            <Settings AllowCellMerge="False" />
                                            <DataItemTemplate>                
                                                <dx:ASPxDateEdit ID="FPDate" runat="server" EditFormat="Custom" Width="100px"  UseMaskBehavior="true" EditFormatString="yyyy/MM/dd"
                                                    ClientInstanceName='<%# "FPDate"+Container.VisibleIndex.ToString() %>'
                                                    Border-BorderStyle="None" BorderBottom-BorderStyle="Solid" ButtonStyle-BorderBottom-BorderColor="#ccc" BackColor="#FDF7D9" 
                                                    DisabledStyle-BackColor="Transparent" DisabledStyle-BorderBottom-BorderStyle="None">
                                                    <CalendarProperties><FastNavProperties DisplayMode="Inline" /></CalendarProperties>
                                                </dx:ASPxDateEdit>          
                                            </DataItemTemplate>        
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="发票金额" FieldName="FPAmount" Width="80px" VisibleIndex="11">
                                            <Settings AllowCellMerge="False"/>
                                            <DataItemTemplate>
                                                 <asp:TextBox ID="FPAmount" Width="80px" runat="server" Text='<%# Eval("FPAmount")%>'></asp:TextBox>
                                            </DataItemTemplate>        
                                        </dx:GridViewDataTextColumn> 
                                        <dx:GridViewDataTextColumn Caption="备注" FieldName="Remark" Width="150px" VisibleIndex="12">
                                            <Settings AllowCellMerge="False"/>
                                            <DataItemTemplate>
                                                <asp:TextBox ID="Remark" Width="150px" runat="server" Text='<%# Eval("Remark")%>'></asp:TextBox>
                                            </DataItemTemplate>        
                                        </dx:GridViewDataTextColumn>   
                                        <dx:GridViewDataTextColumn Caption="实际付款日期" FieldName="fkdate" Width="90px" VisibleIndex="13"></dx:GridViewDataTextColumn> 
                                        <dx:GridViewDataTextColumn Caption="已付款金额" FieldName="fkamt" Width="80px" VisibleIndex="14"></dx:GridViewDataTextColumn> 
                                    </Columns>
                                    <TotalSummary>
                                        <dx:aspxsummaryitem DisplayFormat="<font color='red'>小计</font>" FieldName="rowid" ShowInColumn="rowid" ShowInGroupFooterColumn="rowid" SummaryType="Sum" />
                                        <dx:aspxsummaryitem DisplayFormat="<font color='red'>{0:N0}</font>" FieldName="PayRate" ShowInColumn="PayRate" ShowInGroupFooterColumn="PayRate" SummaryType="Sum" />
                                        <dx:aspxsummaryitem DisplayFormat="<font color='red'>{0:N2}</font>" FieldName="PayMoney" ShowInColumn="PayMoney" ShowInGroupFooterColumn="PayMoney" SummaryType="Sum" />
                                        <dx:aspxsummaryitem DisplayFormat="<font color='red'>{0:N2}</font>" FieldName="FPAmount" ShowInColumn="FPAmount" ShowInGroupFooterColumn="FPAmount" SummaryType="Sum" />
                                    </TotalSummary>
                                    <Styles>
                                        <Header BackColor="#E4EFFA" Border-BorderColor="#DCDCDC" HorizontalAlign="Left" VerticalAlign="Top"></Header>   
                                        <SelectedRow BackColor="#FDF7D9"></SelectedRow>   
                                        <AlternatingRow BackColor="#f2f3f2"></AlternatingRow>
                                        <Cell Border-BorderColor="#DCDCDC" BorderLeft-BorderWidth="0"  BorderRight-BorderWidth="0" BorderTop-BorderWidth="0"></Cell>
                                        <CommandColumn Border-BorderColor="#DCDCDC" BorderRight-BorderStyle="None"></CommandColumn>
                                    </Styles>
                                </dx:aspxgridview>
                               
                             </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
        </div>


        <div class="row  row-container">
            <div class="panel panel-info">
                <div class="panel-heading" data-toggle="collapse" data-target="#CPXX2">
                    <strong>历史清单</strong>
                </div>
                <div class="panel-body " id="CPXX2">
                    <div class="col-xs-12 col-sm-12  col-md-12 col-lg-12">
                        <dx:ASPxGridView ID="gv_his" runat="server" KeyFieldName="id"
                            AutoGenerateColumns="False" Width="990px" OnPageIndexChanged="gv_his_PageIndexChanged"  ClientInstanceName="gv_his">
                            <SettingsPager PageSize="100" ></SettingsPager>
                            <Settings ShowFilterRow="True" ShowGroupPanel="false" ShowFilterRowMenu="True" ShowFilterRowMenuLikeItem="True" AutoFilterCondition="Contains" 
                                VerticalScrollBarMode="Visible" VerticalScrollBarStyle="Standard" VerticalScrollableHeight="150"  />
                            <SettingsBehavior AllowFocusedRow="false" AllowSelectByRowClick="false"  ColumnResizeMode="Control"/>
                            <Columns>                     
                                <dx:GridViewDataTextColumn Caption="行号" FieldName="ContractLine" Width="50px" VisibleIndex="2"></dx:GridViewDataTextColumn> 
                                <dx:GridViewDataDateColumn Caption="计划付款日期" FieldName="PlanPayDate" Width="100px" VisibleIndex="4">
                                    <PropertiesDateEdit DisplayFormatString="yyyy/MM/dd"></PropertiesDateEdit>
                                </dx:GridViewDataDateColumn> 
                                <dx:GridViewDataTextColumn Caption="付款比例" FieldName="PayRate" Width="90px" VisibleIndex="5"></dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="付款金额" FieldName="PayMoney" Width="100px" VisibleIndex="6">
                                    <PropertiesTextEdit DisplayFormatString="{0:N2}"></PropertiesTextEdit>
                                </dx:GridViewDataTextColumn>  
                                <dx:GridViewDataTextColumn Caption="付款条款" FieldName="PayClause" Width="80px" VisibleIndex="7"></dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="付款方式" FieldName="PayFunc" Width="80px" VisibleIndex="8"></dx:GridViewDataTextColumn> 
                                <dx:GridViewDataTextColumn Caption="付款凭据" FieldName="PayFile" Width="80px" VisibleIndex="9"></dx:GridViewDataTextColumn> 
                                <dx:GridViewDataDateColumn Caption="发票日期" FieldName="FPDate" Width="100px" VisibleIndex="10">
                                    <PropertiesDateEdit DisplayFormatString="yyyy/MM/dd"></PropertiesDateEdit>
                                </dx:GridViewDataDateColumn> 
                                <dx:GridViewDataTextColumn Caption="发票金额" FieldName="FPAmount" Width="100px" VisibleIndex="11">
                                    <PropertiesTextEdit DisplayFormatString="{0:N2}"></PropertiesTextEdit>
                                </dx:GridViewDataTextColumn> 
                                <dx:GridViewDataTextColumn Caption="备注" FieldName="Remark" Width="200px" VisibleIndex="12"></dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="创建人" FieldName="CreateName" Width="100px" VisibleIndex="13"></dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="id" FieldName="id" VisibleIndex="99"
                                 HeaderStyle-CssClass="hidden" CellStyle-CssClass="hidden" FooterCellStyle-CssClass="hidden"></dx:GridViewDataTextColumn>
                            </Columns>
                            <Styles>
                                <Header BackColor="#99CCFF"></Header>
                                <FocusedRow BackColor="#99CCFF" ForeColor="#0000CC"></FocusedRow>
                                <Footer HorizontalAlign="Right"></Footer>
                            </Styles>
                        </dx:ASPxGridView>
                    </div>
                </div>
            </div>
        </div>

    </div>
    </form>
</body>
</html>
