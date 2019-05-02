<%@ Page Title="【压射头查询】" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="YST_Query.aspx.cs" Inherits="YaSheTou_YST_Query" %>

<%@ Register Assembly="DevExpress.Web.v17.2, Version=17.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
     <script type="text/javascript" src="/Content/js/jquery.min.js"></script>
    <script src="/Content/js/layer/layer.js"></script>
    <script src="/Content/js/plugins/layer/laydate/laydate.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#mestitle").html("【压射头查询】");
            setHeight();

            $(window).resize(function () {
                setHeight();
            });

            $('#btn_back').click(function () {
                window.location.href = "/Default.aspx";
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
                <td style="width:75px;">更换日期</td>
                <td >
                    <asp:TextBox ID="txt_startdate" class="form-control" onclick="laydate()" runat="server" Width="100px"></asp:TextBox>
                </td>
                <td>~</td>
                <td >
                    <asp:TextBox ID="txt_enddate" class="form-control" onclick="laydate()" runat="server" Width="100px"></asp:TextBox>
                </td>   
                <td style="width:35px;">位置</td>
                <td style="width:135px;">
                   <asp:TextBox ID="txt_sbjc" class="form-control input-s-sm" runat="server" Width="130px"></asp:TextBox>
                </td>              
                <td id="td_btn">  
                    <button id="btn_search" type="button" class="btn btn-primary btn-large" runat="server" onserverclick="btn_search_ServerClick"><i class="fa fa-search fa-fw"></i>&nbsp;查询</button> 
                    <button id="btn_back" type="button" class="btn btn-primary btn-large"><i class="fa fa-backward fa-fw"></i>&nbsp;返回</button> 
                </td>
            </tr>                      
        </table>                   
    </div>

    <div class="col-sm-12">
        <table>
            <tr>
                <td>
                    <dx:ASPxGridView ID="gv" runat="server" KeyFieldName="id" 
                        AutoGenerateColumns="False" Width="1100px" OnPageIndexChanged="gv_PageIndexChanged"  ClientInstanceName="grid">
                        <ClientSideEvents EndCallback="function(s, e) {setHeight();}"  />
                        <SettingsPager PageSize="100" ></SettingsPager>
                        <Settings ShowFilterRow="True" ShowGroupPanel="false" ShowFilterRowMenu="True" ShowFilterRowMenuLikeItem="True" AutoFilterCondition="Contains" 
                            VerticalScrollBarMode="Visible" VerticalScrollBarStyle="Standard" VerticalScrollableHeight="600"  />
                        <SettingsBehavior AllowFocusedRow="false" AllowSelectByRowClick="false"  ColumnResizeMode="Control"/>
                        <Columns>     
                            <dx:GridViewDataTextColumn Caption="压射头编码" FieldName="code" Width="120px" VisibleIndex="0" ></dx:GridViewDataTextColumn>       
                            <dx:GridViewDataDateColumn Caption="更换日期" FieldName="CreateTime" Width="90px" VisibleIndex="1" >
                                <PropertiesDateEdit DisplayFormatString="yyyy/MM/dd"></PropertiesDateEdit>
                            </dx:GridViewDataDateColumn>
                            <dx:GridViewDataTextColumn Caption="位置" FieldName="equip_name" Width="130px" VisibleIndex="2"></dx:GridViewDataTextColumn> 
                            <%--<dx:GridViewDataTextColumn Caption="类别" FieldName="change_type" Width="70px" VisibleIndex="3"></dx:GridViewDataTextColumn>--%> 
                            <dx:GridViewDataTextColumn Caption="开始使用模次" FieldName="start_mc" Width="80px" VisibleIndex="4"></dx:GridViewDataTextColumn> 
                            <dx:GridViewDataTextColumn Caption="本次使用模次" FieldName="deal_mc" Width="80px" VisibleIndex="5"></dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="结束使用模次" FieldName="end_mc" Width="80px" VisibleIndex="6"></dx:GridViewDataTextColumn> 
                            <dx:GridViewDataTextColumn Caption="压射头状态" FieldName="yzt_status" Width="80px" VisibleIndex="7"></dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="备注" FieldName="remark" Width="130px" VisibleIndex="8"></dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="操作人" FieldName="emp_name" Width="80px" VisibleIndex="9"></dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="班别" FieldName="emp_banbie" Width="50px" VisibleIndex="10"></dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="班组" FieldName="emp_banzhu" Width="50px" VisibleIndex="11"></dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="下位置" FieldName="xwz" Width="130px" VisibleIndex="11"></dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="id" FieldName="id" VisibleIndex="99"
                                HeaderStyle-CssClass="hidden" CellStyle-CssClass="hidden" FooterCellStyle-CssClass="hidden"></dx:GridViewDataTextColumn>
                        </Columns>
                        <Styles>
                            <Header BackColor="#99CCFF"></Header>
                            <Footer HorizontalAlign="Right"></Footer>
                            <AlternatingRow BackColor="#F2F3F2"></AlternatingRow>   
                        </Styles>
                    </dx:ASPxGridView>
                </td>
            </tr>
        </table>
    </div>

</asp:Content>

