<%@ Page Title="MES【精炼测氢查询】" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="JinLian_Query.aspx.cs" Inherits="JingLian_JinLian_Query" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <script src="../Content/js/jquery.min.js" type="text/javascript"></script>
    <script src="../Content/js/plugins/layer/layer.min.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript">
        $(document).ready(function () {

            $("#tst").click(function () {

            });

        });
        // $("div[class='h3']").text($("div[class='h3']").text() + "【精炼测氢查询】");
        $("#mestitle").text("【精炼测氢查询】");
    </script>
    <div class="row row-container">
        <div class="col-sm-12 ">
            <div class="panel panel-info">
                <div class="panel-heading">
                    <strong>查询</strong>
                    <asp:ScriptManager ID="ScriptManager1" runat="server">
                    </asp:ScriptManager>
                </div>
                <div class="panel-body">
                    <div class="col-sm-12">

                        <table>

                            <tr>
                                <td>转运包序列号:
                                </td>
                                <td>
                                    <asp:TextBox ID="txt_zybno" class="form-control input-s-sm" runat="server"></asp:TextBox>
                                </td>
                                <td>合金:
                                </td>
                                <td>

                                    <asp:DropDownList ID="txt_hejin" runat="server" class="form-control input-s-sm " Width="130px">
                                        <asp:ListItem></asp:ListItem>
                                        <asp:ListItem>A380</asp:ListItem>
                                        <asp:ListItem>EN46000</asp:ListItem>
                                        <asp:ListItem>ADC12</asp:ListItem>
                                        <asp:ListItem>EN47100</asp:ListItem>

                                    </asp:DropDownList>

                                </td>
                                <td>日期:
                                </td>
                                <td class="form-inline">
                                    <asp:TextBox ID="txt_startdate" runat="server" Width="100"  CssClass="form-control"/>
                                    <ajaxToolkit:CalendarExtender ID="txt_startdate_CalendarExtender"
                                        runat="server" PopupButtonID="Image2" Format="yyyy/MM/dd"
                                        TargetControlID="txt_startdate" />
                                    ~&nbsp;<asp:TextBox ID="txt_enddate" runat="server" CssClass="form-control"
                                        Width="100" />
                                    <ajaxToolkit:CalendarExtender ID="txt_enddate_CalendarExtender"
                                        runat="server" PopupButtonID="Image2" Format="yyyy/MM/dd"
                                        TargetControlID="txt_enddate" />
                                </td>
                                <td>设备</td>
                                <td><asp:DropDownList ID="ddlsbno" runat="server" class="form-control input-small">
                                        <asp:ListItem></asp:ListItem>
                                        <asp:ListItem Value="精炼机1#">精炼机1#</asp:ListItem>
                                        <asp:ListItem Value="精炼机2#">精炼机2#</asp:ListItem>
                                         
                                    </asp:DropDownList></td>
                            </tr>
                            <tr>
                                <td>班别:
                                </td>
                                <td>
                                    <asp:DropDownList ID="txt_banzu" runat="server" class="form-control input-s-sm ">
                                        <asp:ListItem></asp:ListItem>
                                        <asp:ListItem>白班</asp:ListItem>
                                        <asp:ListItem>晚班</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td>炉号:
                                </td>
                                <td>
                                    <asp:DropDownList ID="txt_luhao" runat="server" class="form-control input-small">
                                        <asp:ListItem></asp:ListItem>
                                        <asp:ListItem>A</asp:ListItem>
                                        <asp:ListItem>B</asp:ListItem>
                                        <asp:ListItem>C</asp:ListItem>
                                        <asp:ListItem>D</asp:ListItem>
                                        <asp:ListItem>E</asp:ListItem>
                                        <asp:ListItem>F</asp:ListItem>
                                        <asp:ListItem>X</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td>操作工:
                                </td>
                                <td>
                                    <asp:TextBox ID="txt_czg" class="form-control input-s-sm" runat="server"></asp:TextBox>
                                </td>
                            </tr>

                            <tr>

                                <td colspan="5" align="right">
                                    <asp:Button ID="Button1" runat="server" Text="查询"
                                        class="btn btn-large btn-primary"
                                        OnClick="Button1_Click" Width="100px" />
                                </td>
                                <td>&nbsp;
                                            <asp:Button ID="Button2" runat="server" Text="返回"
                                                class="btn btn-large btn-primary"
                                                Width="100px" OnClick="Button2_Click" />
                                </td>
                            </tr>


                        </table>
                    </div>

                </div>
            </div>
        </div>
    </div>
    
    <div runat="server" id="DIV1" style="margin: 10px">
        <asp:Panel ID="Panel2" runat="server" Height="100%">
            <table style="background-color: #FFFFFF;">
                <tr>
                    <td valign="top">
                        <asp:GridView ID="GridView1" runat="server"
                            AllowPaging="True" AllowSorting="True"
                            AutoGenerateColumns="False"
                            OnPageIndexChanging="GridView1_PageIndexChanging"
                            OnRowDataBound="GridView1_RowDataBound"
                            OnSorting="GridView1_Sorting" PageSize="100" Width="1500px">
                            <Columns>
                                <asp:BoundField DataField="Hd_zybno" HeaderText="转运包序列号">
                                    <HeaderStyle BackColor="#C1E2EB" />
                                </asp:BoundField>
                                <asp:BoundField DataField="hd_baohao" HeaderText="转运包号">
                                    <HeaderStyle BackColor="#C1E2EB" />
                                </asp:BoundField>
                                <asp:BoundField DataField="date"
                                    DataFormatString="{0:yyyy-MM-dd}" HeaderText="日期">
                                    <HeaderStyle BackColor="#C1E2EB" />
                                </asp:BoundField>
                                <asp:BoundField DataField="HD_banbie" HeaderText="班别">
                                    <HeaderStyle BackColor="#C1E2EB" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Hd_start_time"
                                    HeaderText="开始精炼时间" SortExpression="Hd_start_time">
                                    <HeaderStyle BackColor="#C1E2EB" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Hd_time" HeaderText="精炼完成时间">
                                    <HeaderStyle BackColor="#C1E2EB" />
                                </asp:BoundField>
                                <asp:BoundField DataField="jljg"
                                    HeaderText="精炼间隔&lt;br&gt;(Min)" HtmlEncode="False">
                                    <HeaderStyle BackColor="#C1E2EB" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="timer"
                                    HeaderText="精炼时长&lt;br&gt;(Min)" HtmlEncode="False">
                                    <FooterStyle HorizontalAlign="Right" />
                                    <HeaderStyle BackColor="#C1E2EB" HorizontalAlign="Right"
                                        Wrap="True" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Hd_Hejin" HeaderText="合金">
                                    <HeaderStyle BackColor="#C1E2EB" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Hd_banzu" HeaderText="班组">
                                    <HeaderStyle BackColor="#C1E2EB" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Hd_luhao" HeaderText="炉号">
                                    <HeaderStyle BackColor="#C1E2EB" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Hd_kqzl"
                                    HeaderText="空气中重量&lt;br&gt;(g)" HtmlEncode="False">
                                    <HeaderStyle BackColor="#C1E2EB" HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Hd_waterzl"
                                    HeaderText="水中重量&lt;br&gt;(g)" HtmlEncode="False">
                                    <HeaderStyle BackColor="#C1E2EB" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="result" HeaderText="密度">
                                    <HeaderStyle BackColor="#C1E2EB" />
                                </asp:BoundField>
                                <asp:BoundField DataField="hd_bf_wd"
                                    HeaderText="精炼前温度&lt;br&gt;(℃)" HtmlEncode="False">
                                    <HeaderStyle BackColor="#C1E2EB" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="HD_af_wd"
                                    HeaderText="精炼后温度&lt;br&gt;(℃)" HtmlEncode="False">
                                    <HeaderStyle BackColor="#C1E2EB" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Hd_jz"
                                    HeaderText="净重&lt;br&gt;(kg)" HtmlEncode="False">
                                    <HeaderStyle BackColor="#C1E2EB" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="jg" HeaderText="结果">
                                    <ControlStyle CssClass="hidden" Width="0px" />
                                    <FooterStyle CssClass="hidden" Width="0px" />
                                    <HeaderStyle BackColor="#C1E2EB" CssClass="hidden"
                                        Width="0px" />
                                    <ItemStyle CssClass="hidden" Width="0px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="HD_cqtime" HeaderText="测氢时间">
                                    <HeaderStyle BackColor="#C1E2EB" />
                                </asp:BoundField>
                                <asp:BoundField DataField="cqtimer"
                                    HeaderText="测氢时长&lt;br&gt;(Min)" HtmlEncode="False">
                                    <HeaderStyle BackColor="#C1E2EB" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Hd_name" HeaderText="操作工">
                                    <HeaderStyle BackColor="#C1E2EB" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Hd_sbno" HeaderText="设备">
                                    <HeaderStyle BackColor="#C1E2EB" />
                                </asp:BoundField>
                            </Columns>
                            <PagerSettings FirstPageText="首页" LastPageText="尾页"
                                Mode="NextPreviousFirstLast" NextPageText="下页"
                                PreviousPageText="上页" />
                            <PagerStyle ForeColor="Red" />
                        </asp:GridView>
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </div>



</asp:Content>

