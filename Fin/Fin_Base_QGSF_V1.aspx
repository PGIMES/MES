<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Fin_Base_QGSF_V1.aspx.cs" Inherits="Fin_Fin_Base_QGSF_V1" %>

<%@ Register Assembly="DevExpress.Web.v17.2, Version=17.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <script src="/Content/js/jquery.min.js"  type="text/javascript"></script>
    <script src="/Content/js/bootstrap.min.js"></script>
    <script src="/Content/js/layer/layer.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#mestitle").text("【产品税率】");

            setHeight();
            $(window).resize(function () {
                setHeight();
            });

            $('#btn_edit').click(function () {
                var url = '/Fin/Fin_Base_QGSF_V1_Maintain.aspx';
                layer.open({
                    title: '新增&修改：基率&清关税<font color="red">【Base Rate】【301 Rate】请填写小数.</font>',
                    closeBtn: 2,
                    type: 2,
                    area: ['750px', '300px'],
                    fixed: false, //不固定
                    maxmin: true, //开启最大化最小化按钮
                    content: url,
                    cancel: function (index, layero) {//取消事件
                    },
                    end: function () {//无论是确认还是取消，只要层被销毁了，end都会执行，不携带任何参数。layer.open关闭事件
                        location.reload();　　//layer.open关闭刷新
                    }

                });
            });
        });

        function setHeight() {
            $("div[class=dxgvCSD]").css("height", ($(window).height() - $("#div_p").height() - 200) + "px");
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
                    <td style="text-align:right;">项目号：</td>
                    <td>
                        <asp:TextBox ID="txt_wlh" class="form-control input-s-md " runat="server"></asp:TextBox>
                    </td>
                    <td> 
                        &nbsp;
                        <asp:Button ID="Bt_select" runat="server" Text="查询" class="btn btn-large btn-primary" OnClick="Bt_select_Click" Width="70px" />
                        &nbsp;
                        <button id="btn_edit" type="button" class="btn btn-primary btn-large"><i class="fa fa-pencil-square-o fa-fw"></i>&nbsp;维护</button> 
                        &nbsp;
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
                    <dx:ASPxGridView ID="GV_PART" ClientInstanceName="grid" runat="server" KeyFieldName="domain;wlh" AutoGenerateColumns="False"  
                             OnPageIndexChanged="GV_PART_PageIndexChanged" OnCustomCellMerge="GV_PART_CustomCellMerge" Width="1190px" >
                        <ClientSideEvents EndCallback="function(s, e) { setHeight(); }" />
                        <SettingsBehavior AllowDragDrop="TRUE" AllowFocusedRow="false" AllowSelectByRowClick="false" ColumnResizeMode="Control" AutoExpandAllGroups="true" MergeGroupsMode="Always" SortMode="Value" 
                                     AllowCellMerge="true"/>
                        <SettingsPager PageSize="100"></SettingsPager>
                        <Settings ShowFilterRow="True" ShowFilterRowMenu="True"   VerticalScrollBarMode="Visible" VerticalScrollBarStyle="Standard" VerticalScrollableHeight="500"
                                 ShowFilterRowMenuLikeItem="True"  ShowFooter="True" />
                        <Columns>
                            <dx:GridViewDataTextColumn Caption="域" FieldName="domain" Width="40px" VisibleIndex="1"  />
                            <dx:GridViewDataTextColumn Caption="项目号" FieldName="wlh" Width="90px" VisibleIndex="2"  />
                            <dx:GridViewDataTextColumn Caption="零件号" FieldName="productcode" Width="200px" VisibleIndex="3" />
                            <dx:GridViewDataTextColumn Caption="HTS" FieldName="com_comm_code" Width="150px" VisibleIndex="4"  />
                            <dx:GridViewDataTextColumn Caption="HTS描述" FieldName="com_desc" Width="150px" VisibleIndex="5"/>
                            <dx:GridViewDataTextColumn Caption="基础税率" FieldName="BaseRate" Width="80px" VisibleIndex="6" >
                                <PropertiesTextEdit DisplayFormatString="{0:P1}"></PropertiesTextEdit>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="301 Code" FieldName="301code" Width="90px" VisibleIndex="7" />
                            <dx:GridViewDataTextColumn Caption="301关税" FieldName="301Rate" Width="80px" VisibleIndex="8" >
                                <PropertiesTextEdit DisplayFormatString="{0:P1}"></PropertiesTextEdit>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="是否豁免" FieldName="immunity" Width="70px" VisibleIndex="9"  />
                            <dx:GridViewDataDateColumn Caption="生效日期" FieldName="Effective_date" Width="90px" VisibleIndex="10" >
                                <PropertiesDateEdit DisplayFormatString="yyyy/MM/dd"></PropertiesDateEdit>
                            </dx:GridViewDataDateColumn>     
                            <dx:GridViewDataDateColumn Caption="截至日期" FieldName="End_date" Width="90px" VisibleIndex="11" >
                                <PropertiesDateEdit DisplayFormatString="yyyy/MM/dd"></PropertiesDateEdit>
                            </dx:GridViewDataDateColumn>     
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

