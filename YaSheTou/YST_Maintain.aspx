<%@ Page Title="【压射头清单】" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="YST_Maintain.aspx.cs" Inherits="YaSheTou_YST_Maintain" %>

<%@ Register Assembly="DevExpress.Web.v17.2, Version=17.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <script type="text/javascript" src="/Content/js/jquery.min.js"></script>
    <script src="/Content/js/layer/layer.js"></script>
    <script src="/Content/js/plugins/layer/laydate/laydate.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#mestitle").html("【压射头清单】");
            setHeight();

            $(window).resize(function () {
                setHeight();
            });

            $('#btn_add').click(function () {
                var url = '/YaSheTou/YST_Maintain_Modify.aspx?'

                //alert(url);
                layer.open({
                    title: '新增-压射头',
                    closeBtn: 2,
                    type: 2,
                    area: ['400px', '300px'],
                    fixed: false, //不固定
                    maxmin: true, //开启最大化最小化按钮
                    content: url
                });
            });

            $('#btn_del').click(function () {
                if (grid.GetSelectedRowCount() != 1) { layer.alert("请选择一条记录!"); return; }

                grid.GetSelectedFieldValues('id;code', function GetVal(values) {
                    if (values.length > 0) {
                        var id = values[0][0];
                        var code = values[0][1];

                        $.ajax({
                            type: "post",
                            url: "YST_Maintain.aspx/del_data",
                            data: "{'code':'" + code + "'}",
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            async: false,//默认是true，异步；false为同步，此方法执行完在执行下面代码
                            success: function (data) {
                                var obj = eval(data.d);
                                layer.alert(obj[0].re_flag, function (index) { location.reload(); });
                                return;
                            }

                        });

                    }
                });
            });
            

        });

        function setHeight() {
            $("div[class=dxgvCSD]").css("height", ($(window).height() - $("#div_p").height() - 160) + "px");
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

    <div class="col-md-12" id="div_p"  style="margin-bottom:5px">
        <table style=" border-collapse: collapse;">
            <tr>
                <td style="width:35px;">编码</td>
                <td style="width:155px;">
                   <asp:TextBox ID="txt_code" runat="server" class="form-control" Width="150px"  />
                </td>              
                <td id="td_btn">  
                    <button id="btn_search" type="button" class="btn btn-primary btn-large" runat="server" onserverclick="btn_search_ServerClick"><i class="fa fa-search fa-fw"></i>&nbsp;查询</button> 
                    <button id="btn_add" type="button" class="btn btn-primary btn-large"><i class="fa fa-plus fa-fw"></i>&nbsp;新增</button>
                    <button id="btn_del" type="button" class="btn btn-primary btn-large"><i class="fa fa-remove fa-fw"></i>&nbsp;删除</button>
                    <asp:Button ID="btnimport" runat="server" Text="导出Excel"  class="btn btn-primary" Width="100px" OnClick="btnimport_Click" Visible="false" />
                </td>
            </tr>                      
        </table>                   
    </div>

    <div class="col-sm-12">
        <table>
            <tr>
                <td>
                    <dx:ASPxGridView ID="gv" runat="server" KeyFieldName="id" 
                        AutoGenerateColumns="False" Width="950px" OnPageIndexChanged="gv_PageIndexChanged"  ClientInstanceName="grid" OnHtmlRowCreated="gv_HtmlRowCreated">
                        <ClientSideEvents EndCallback="function(s, e) {setHeight();}"  />
                        <SettingsPager PageSize="100" ></SettingsPager>
                        <Settings ShowFilterRow="True" ShowGroupPanel="false" ShowFilterRowMenu="True" ShowFilterRowMenuLikeItem="True" AutoFilterCondition="Contains" 
                            VerticalScrollBarMode="Visible" VerticalScrollBarStyle="Standard" VerticalScrollableHeight="600"  />
                        <SettingsBehavior AllowFocusedRow="false" AllowSelectByRowClick="false"  ColumnResizeMode="Control"/>
                        <Columns>     
                            <dx:GridViewCommandColumn ShowClearFilterButton="true" ShowSelectCheckbox="true" Name="Sel" Width="40" VisibleIndex="0">
                            </dx:GridViewCommandColumn>    
                            <dx:GridViewDataTextColumn Caption="压射头编码" FieldName="code" Width="120px" VisibleIndex="1" ></dx:GridViewDataTextColumn>                
                            <dx:GridViewDataTextColumn Caption="压射头直径" FieldName="zj" Width="80px" VisibleIndex="2" ></dx:GridViewDataTextColumn> 
                            <dx:GridViewDataTextColumn Caption="额定模次" FieldName="mc" Width="80px" VisibleIndex="3"></dx:GridViewDataTextColumn>    
                            <dx:GridViewDataTextColumn Caption="供应商" FieldName="gys_name" Width="120px" VisibleIndex="4"></dx:GridViewDataTextColumn>  
                            <dx:GridViewDataTextColumn Caption="物料号" FieldName="part" Width="80px" VisibleIndex="5"></dx:GridViewDataTextColumn>   
                            <dx:GridViewDataTextColumn Caption="累计模次" FieldName="lj_mc" Width="80px" VisibleIndex="6"></dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="位置" FieldName="wz" Width="130px" VisibleIndex="7"></dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="状态" FieldName="yzt_status" Width="80px" VisibleIndex="7"></dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="创建时间" FieldName="CreateTime" Width="140px" VisibleIndex="7">
                                <PropertiesTextEdit DisplayFormatString="yyyy/MM/dd HH:mm:ss"></PropertiesTextEdit>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="id" FieldName="id" VisibleIndex="99"
                                HeaderStyle-CssClass="hidden" CellStyle-CssClass="hidden" FooterCellStyle-CssClass="hidden"></dx:GridViewDataTextColumn>
                        </Columns>
                        <Styles>
                            <Header BackColor="#99CCFF"></Header>
                            <Footer HorizontalAlign="Right"></Footer>
                            <SelectedRow BackColor="#FDF7D9" ForeColor="Black"></SelectedRow>   
                        </Styles>
                    </dx:ASPxGridView>
                </td>
            </tr>
        </table>
    </div>

</asp:Content>

