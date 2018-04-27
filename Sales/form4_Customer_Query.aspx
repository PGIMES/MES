<%@ Page Title="报价系统【报价查询】" Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.master" CodeFile="form4_Customer_Query.aspx.cs" Inherits="form4_Customer_Query" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <style type="text/css">
        td {
            padding-left: 5px;
            padding-right: 5px;
        }

        .auto-style1 {
            width: 100px;
        }

        .tblCondition td {
            white-space: nowrap;
            padding-bottom: 2px;
        }
        .GridView1 td {
             word-wrap:break-word;
            padding-bottom: 2px;
            /*word-break: break-all;*/
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <script src="../Content/js/jquery.min.js" type="text/javascript"></script>
    <script src="../Content/js/bootstrap.min.js"></script>
    <script src="../Content/js/plugins/layer/laydate/laydate.js"></script>
    <script src="../Content/js/plugins/layer/layer.min.js"></script>
    <link href="../Content/js/plugins/bootstrap-select/css/bootstrap-select.min.css" rel="stylesheet" />
    <script src="../Content/js/plugins/bootstrap-select/js/bootstrap-select.min.js"></script>
<%--    <link href="../Content/js/plugins/bootstrap-select/css/bootstrap-select-2.0.css" rel="stylesheet" />
    <script src="../Content/js/plugins/bootstrap-select/js/bootstrap-select-2.0.js"></script>--%>
    <script type="text/javascript">
        $("#mestitle").text("【客户查询】");
        $(document).ready(function () {      
            $("[param]").click(function () {
                param = $(this).attr('param');
                layer.open({
                    type: 2,
                    skin: 'layui-layer-demo', //样式类名
                    closeBtn: 1, //显示关闭按钮
                    anim: 2,
                    title: ['报价跟踪记录', false],
                    area: ['800px', '650px'],
                    shadeClose: true, //开启遮罩关闭
                    content: 'BaoJia_Remark_flow.aspx' + param,
                    end: function () { }
                });

            });

            //var str='3,4,5,6';
            //var arr=str.split(',');
            //$('.selectpicker').selectpicker('val', arr);//初始化选择 项
            // $('.selectpicker').selectpicker('val', 2);//
            $('.selectpicker').change(function(){               
                $("input[id*='txtcm_addr']").val($(".selectpicker").val());
            });
        });
        

    </script>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

    <div class="row row-container">
        <div class="col-sm-12 ">
            <div class="panel panel-info">
                <div class="panel-heading">
                    <strong>客户查询</strong>
                </div>
                <div class="panel-body">
                    <div class="col-sm-12">
                        <table class="tblCondition" border="0" >
                            <tr>
                                <td>业务关系代码:
                                </td>
                                <td style="width: 100px; float: left; white-space: nowrap">
                                    <div style="float: left; white-space: nowrap">
                                        <asp:TextBox ID="txtBusinessRelationCode" class="form-control "  runat="server" Width="100px"></asp:TextBox></div>
                                    
                                </td>
                                <td>业务关系名称:
                                </td>
                                <td  >
                                    <asp:TextBox ID="txtBusinessRelationName1" class="form-control " runat="server" Width="100px"></asp:TextBox>
                                </td>
                                <td >搜索名称:
                                </td>
                                <td  style="width:100px">
                                    <asp:TextBox ID="txtAddressSearchName" class="form-control input-s-sm" runat="server" Width="100px"></asp:TextBox>
                                </td>
                                <td>地址:
                                </td>
                                <td class="auto-style1">
                                    <asp:TextBox ID="txtAddressStreet1" class="form-control input-s-sm" runat="server"></asp:TextBox>
                                </td>
                                <td>国家: </td>
                                <td>
                                    <asp:TextBox ID="txtctry_country" class="form-control input-s-sm" runat="server" Width="100px"></asp:TextBox>                                     
                                </td>
                                <td>含税:</td>
                                <td>
                                    <asp:DropDownList ID="ddlAddressIsTaxIncluded" runat="server" class="form-control input-s-sm " Width="80px">
                                        <asp:ListItem Value="" Selected="True"></asp:ListItem>
                                        <asp:ListItem Value="1">含税</asp:ListItem>
                                        <asp:ListItem Value="0">不含税</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>发货终点名称:</td>
                                <td>
                                    <asp:TextBox ID="txtDebtorShipToName" class="form-control input-s-sm" runat="server" Width="100px"></asp:TextBox>                                     
                                </td>
                                <td>PO工厂:
                                </td>
                                <td>
                                    <asp:TextBox ID="txtcm_addr" class="form-control " runat="server" Width="100px"></asp:TextBox><div  style="display:none">
                                    <select id="selectcm_addr" name="selectcm_addr" class="selectpicker " multiple  data-live-search="true" runat="server" style="display:none" >                                          
                                    </select></div>
                                </td>
                                <td> 
                                </td>
                                <td>
                                     
                                </td>
                                <td> 
                                </td>
                                <td colspan="1">
                                     
                                </td>
                                <td> 
                                </td>
                                <td>
                                     
                                </td>
                                <td> 
                                </td>
                                <td>
                                     
                                </td>
                            </tr>
                            <tr>
                                <td> </td>
                                <td>
                                     
                                </td>
                                <td colspan="10" style="text-align: right; padding-top: 10px; padding-right: 100px">
                                    <asp:Button ID="Button1" runat="server" Text="查询" class="btn btn-large btn-primary" OnClick="Button1_Click" Width="100px" /></td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div runat="server" id="DIV1" style="margin-left: 5px; margin-right: 5px; margin-bottom: 10px">
        <table>
            <tr>
                <td valign="top">
                    <asp:GridView ID="GridView1" runat="server" CssClass="GridView1" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" BorderColor="LightGray" PageSize="20" OnRowDataBound="GridView1_RowDataBound" OnPageIndexChanging="GridView1_PageIndexChanging">
                        <Columns>
                            <asp:BoundField DataField="requestid" HeaderText="No.">
                                <HeaderStyle BackColor="#C1E2EB" Width="50px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="requestid" HeaderText="报价号">
                                <HeaderStyle BackColor="#C1E2EB" Width="60px" Wrap="false" />
                            </asp:BoundField>
                            <asp:BoundField DataField="cmClassName" HeaderText="客户大类">
                                <HeaderStyle BackColor="#C1E2EB" Width="100px" Wrap="false" />
                            </asp:BoundField>
                            <asp:BoundField DataField="BusinessRelationCode" HeaderText="业务关系代码">
                                <HeaderStyle BackColor="#C1E2EB" Width="100px" Wrap="false" />
                            </asp:BoundField>
                            <asp:BoundField DataField="BusinessRelationName1" HeaderText="业务关系名称">
                                <HeaderStyle BackColor="#C1E2EB" Width="100px" Wrap="false" />
                            </asp:BoundField>
                            <asp:BoundField DataField="AddressSearchName" HeaderText="搜索名称">
                                <HeaderStyle BackColor="#C1E2EB" Width="90px" Wrap="false" />
                            </asp:BoundField>
                            <asp:BoundField DataField="AddressTypeCode" HeaderText="地址类型">
                                <HeaderStyle BackColor="#C1E2EB" Width="80px" Wrap="false" />
                            </asp:BoundField>
                            <asp:BoundField DataField="AddressStreet1" HeaderText="地址">
                                <HeaderStyle BackColor="#C1E2EB" Width="300px" Wrap="false" />
                            </asp:BoundField>
                            <asp:BoundField DataField="cm_lang" HeaderText="语言代码">
                                <HeaderStyle BackColor="#C1E2EB" Width="120px" Wrap="false" />
                                <ItemStyle />
                            </asp:BoundField>
                            <asp:BoundField DataField="ctry_country" HeaderText="国家">
                                <HeaderStyle BackColor="#C1E2EB" Width="180px" Wrap="false" />
                                <ItemStyle />
                            </asp:BoundField>
                            <asp:BoundField DataField="cm_region" HeaderText="地区">
                                <HeaderStyle BackColor="#C1E2EB" Width="60px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="AddressZip" HeaderText="邮编"  >
                                <HeaderStyle BackColor="#C1E2EB" Width="150px" Wrap="false" />
                                <ItemStyle />
                            </asp:BoundField>
                            <asp:BoundField DataField="AddressCity" HeaderText="城市"  >
                                <HeaderStyle BackColor="#C1E2EB" Width="80px" />
                                <ItemStyle  ></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="ad_phone" HeaderText="电话"  >
                                <HeaderStyle BackColor="#C1E2EB" Width="100px" Wrap="false" />
                                <ItemStyle />
                            </asp:BoundField>
                            <asp:BoundField DataField="DebtorShipToCode" HeaderText="发货终点代码"  >
                                <HeaderStyle BackColor="#C1E2EB" Width="100px" Wrap="false" />
                                <ItemStyle />
                            </asp:BoundField>
                            <asp:BoundField DataField="DebtorShipToName" HeaderText="发货终点名称"  >
                                <HeaderStyle BackColor="#C1E2EB" Width="70px" Wrap="false" />
                            </asp:BoundField>
                            <asp:BoundField DataField="cm_addr" HeaderText="PO工厂"  >
                                <HeaderStyle BackColor="#C1E2EB" Width="70px" Wrap="false" />
                            </asp:BoundField>
                            <asp:BoundField DataField="dAddressstreet" HeaderText="发往地址"  >
                                <HeaderStyle BackColor="#C1E2EB" Width="70px" Wrap="false" />
                            </asp:BoundField>
                            <asp:BoundField DataField="dctry_country" HeaderText="发往国家"  >
                                <HeaderStyle BackColor="#C1E2EB" Width="70px" Wrap="false" />
                            </asp:BoundField>
                            <asp:BoundField DataField="dcm_region" HeaderText="发往地区"  >
                                <HeaderStyle BackColor="#C1E2EB" Width="70px" Wrap="false" />
                            </asp:BoundField>
                            <asp:BoundField DataField="dAddressCity" HeaderText="发往城市"  >
                                <HeaderStyle BackColor="#C1E2EB" Width="70px" Wrap="false" />
                            </asp:BoundField>
                            <asp:BoundField DataField="debtor_domain" HeaderText="域"  >
                                <HeaderStyle BackColor="#C1E2EB" Width="150px" Wrap="false" />
                                <ItemStyle ></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="IsEffective" HeaderText="是否有效"  >
                                <HeaderStyle BackColor="#C1E2EB" Width="100px" Wrap="false" />
                                <ItemStyle ></ItemStyle>
                            </asp:BoundField>                            
                            <%--<asp:BoundField DataField="hetong_status" HeaderText="零件状态">
                                <HeaderStyle BackColor="#C1E2EB" Width="200px" Wrap="false" />
                            </asp:BoundField>
                            <asp:BoundField DataField="project_size" HeaderText="项目大小">
                                <HeaderStyle BackColor="#C1E2EB" Width="200px" Wrap="false" />
                            </asp:BoundField>
                            <asp:BoundField DataField="project_level" HeaderText="争取级别">
                                <HeaderStyle BackColor="#C1E2EB" Width="200px" Wrap="false" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Create_date" HeaderText="报价开始日期" DataFormatString="{0:yyyy/MM/dd}">
                                <HeaderStyle BackColor="#C1E2EB" Width="110px" Wrap="false" />
                                <ItemStyle />
                            </asp:BoundField>
                            <asp:BoundField DataField="baojia_end_date" HeaderText="报出日期" DataFormatString="{0:yyyy/MM/dd}">
                                <HeaderStyle BackColor="#C1E2EB" Width="200px" Wrap="false" />
                            </asp:BoundField>
                           

                            <asp:BoundField DataField="dingdian_date" HeaderText="定点日期" DataFormatString="{0:yyyy/MM/dd}" HtmlEncode="False" >
                                <HeaderStyle BackColor="#C1E2EB" Width="150px" Wrap="false"  />
                                <ItemStyle />
                            </asp:BoundField>
                            <asp:BoundField DataField="hetong_complet_date" HeaderText="结束跟踪日" DataFormatString="{0:yyyy/MM/dd}" HeaderStyle-Width="150">
                                <HeaderStyle BackColor="#C1E2EB" Width="150px" Wrap="True" />
                                <ItemStyle Width="150px" />
                            </asp:BoundField>
                            
                            <asp:BoundField DataField="sales_name" HeaderText="负责人"  >
                                <HeaderStyle BackColor="#C1E2EB" Width="100px" Wrap="false" />
                                <ItemStyle />
                            </asp:BoundField>
                            <asp:BoundField DataField="requestid" HeaderText="requestid" ShowHeader="false">
                                <HeaderStyle BackColor="#C1E2EB" Width="70px" Wrap="false" />
                            </asp:BoundField>--%>


                        </Columns>
                        <EmptyDataTemplate>no data found</EmptyDataTemplate>
                        <PagerSettings FirstPageText="首页" LastPageText="尾页" Mode="NextPreviousFirstLast" NextPageText="下页" PreviousPageText="上页" />
                        <PagerStyle ForeColor="lightblue" />
                    </asp:GridView>
                </td>
            </tr>


        </table>
    </div>
</asp:Content>
