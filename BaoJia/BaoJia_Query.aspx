<%@ Page Title="报价系统【报价查询】" Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.master" CodeFile="BaoJia_Query.aspx.cs" Inherits="BaoJia_Query" %>

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
        $("#mestitle").text("【报价查询】");
        $(document).ready(function () {
          // GetCustomerList();
           // getCustomerList();
            

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
                $("input[id*='txtCustomer_name']").val($(".selectpicker").val());
            });
        });
        function GetCustomerList()
        {
            $.ajax({
                type: "post", //要用post方式                 
                url: "BaoJia_Query.aspx/getCustomerList", //方法所在页面和方法名SayHello
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                data: "",
                // data: "{'equipno':'" + Equipno + "','logaction':'" + Logaction + "','actionmark':'" + Actionmark +"','actionreason':'" + Actionreason+"' }",
                success: function (data) {
                    if (data.d != "") //返回的数据用data.d获取内容Logaction + "成功."
                    {                       
                        $.each(eval(data.d), function (i) {
                            //                    alert(i);
                            //                    $("<option value='" + data.data[i].schoolno + "'>" + data.data[i].schoolname + "</option>")
                            //                        .appendTo("#schoolno.selectpicker");
                            $('.selectpicker').append("<option value=" + eval(data.d)[i].value + ">" + eval(data.d)[i].text + "</option>");

                        });
                    }
                    else {
                        alert("失败.");
                    }
                },
                error: function (err) {
                    alert(err);
                }
            });
        }
        function getCustomerList() {//获取下拉列表
            $.ajax({
                url: "BaoJia_Query.aspx/getCustomerList",//写你自己的方法，返回map，我返回的map包含了两个属性：data：集合，total：集合记录数量，所以后边会有data.data的写法。。。
                // 数据发送方式
                type: "get",
                // 接受数据格式
                dataType: "json",
                // 要传递的数据
                data: '',
                // 回调函数，接受服务器端返回给客户端的值，即result值
                success: function (data) {//alert(data.data);                    
                    $.each(data.data, function (i) {
                        //                    alert(i);
                        //                    $("<option value='" + data.data[i].schoolno + "'>" + data.data[i].schoolname + "</option>")
                        //                        .appendTo("#schoolno.selectpicker");
                        $('.selectpicker').append("<option value=" + data.data[i].value + ">" + data.data[i].value + "</option>");

                    });

                    $('.selectpicker').selectpicker('refresh');
                },
                error: function (data) {
                    alert("获取下拉顾客失败" + data);
                }
            })
        }
         

    </script>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>



    <div class="row row-container">
        <div class="col-sm-12 ">
            <div class="panel panel-info">
                <div class="panel-heading">
                    <strong>报价查询</strong>
                </div>
                <div class="panel-body">
                    <div class="col-sm-12">
                        <table class="tblCondition" style="width: 100%">
                            <tr>
                                <td>报价开始日期:
                                </td>
                                <td style="width: 220px; float: left; white-space: nowrap">
                                    <div style="float: left; white-space: nowrap">
                                        <asp:TextBox ID="txtCreate_dateF" class="form-control " onclick="laydate()" runat="server" Width="100px"></asp:TextBox></div>
                                    <div style="float: left; white-space: nowrap">~</div>
                                    <div style="float: left; white-space: nowrap">
                                        <asp:TextBox ID="txtCreate_dateT" class="form-control" onclick="laydate()" runat="server" Width="100px"></asp:TextBox></div>
                                </td>
                                <td>报价号:
                                </td>
                                <td  class="auto-style1">
                                    <asp:TextBox ID="txtBaojia_no" class="form-control input-s-sm" runat="server"></asp:TextBox>
                                </td>
                                <td >零件号:
                                </td>
                                <td  style="width:100px">
                                    <asp:TextBox ID="txtLjh" class="form-control input-s-sm" runat="server" Width="100px"></asp:TextBox>
                                </td>
                                <td>零件名称:
                                </td>
                                <td class="auto-style1">
                                    <asp:TextBox ID="txtLj_Name" class="form-control input-s-sm" runat="server"></asp:TextBox>
                                </td>
                                <td>第几轮: </td>
                                <td>
                                    <asp:DropDownList ID="ddlTurns" runat="server" class="form-control input-s-sm " Width="130px">
                                        <asp:ListItem Value="">全部</asp:ListItem>
                                        <asp:ListItem Value="1">最后一轮</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td>报价状态:</td>
                                <td>
                                    <asp:DropDownList ID="ddlbaojia_status" runat="server" class="form-control input-s-sm " Width="130px">
                                        <asp:ListItem Value=""></asp:ListItem>
                                        <asp:ListItem Value="报价中" Selected="True">报价中</asp:ListItem>
                                        <asp:ListItem Value="已报出">已报出</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>销售负责人:</td>
                                <td>
                                    <asp:DropDownList ID="dropSales_Name" runat="server" class="form-control input-s-md " width="200px">
                                        <asp:ListItem Value="" Selected="True">全部</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td>直接顾客:
                                </td>
                                <td>
                                    <asp:TextBox ID="txtCustomer_name" class="form-control input-s-sm" runat="server" style="display:none"></asp:TextBox>
                                    <select id="selectCust" name="selectCust" class="selectpicker " multiple  data-live-search="true" runat="server" style="width:100px" >                                          
                                    </select>
                                </td>
                                <td>顾客项目:
                                </td>
                                <td>
                                    <asp:TextBox ID="txtCustomer_project" class="form-control input-s-sm" runat="server" Width="100px"></asp:TextBox>
                                </td>
                                <td>项目大小:
                                </td>
                                <td colspan="1">
                                    <asp:DropDownList ID="ddlProject_size" runat="server" class="form-control input-s-md " Width="135px">
                                        <asp:ListItem Value="" Selected="True">全部</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td>争取级别:
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlProject_level" runat="server" class="form-control input-s-sm " Width="130px">
                                        <asp:ListItem Value=""></asp:ListItem>
                                        <asp:ListItem Value="重点">重点</asp:ListItem>
                                        <asp:ListItem Value="一般">一般</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td>零件状态:
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlHetong_status" runat="server" class="form-control input-s-sm " Width="130px">
                                        <asp:ListItem Value="">全部</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>报价完成日期</td>
                                <td>
                                    <div style="float: left; white-space: nowrap">
                                        <asp:TextBox ID="txtBaojia_end_dateFrom" class="form-control " onclick="laydate()" runat="server" Width="100px"></asp:TextBox></div>
                                    <div style="float: left; white-space: nowrap">~</div>
                                    <div style="float: left; white-space: nowrap">
                                        <asp:TextBox ID="txtBaojia_end_dateTo" class="form-control" onclick="laydate()" runat="server" Width="100px"></asp:TextBox></div>
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
                            <asp:BoundField DataField="Baojia_no" HeaderText="No.">
                                <HeaderStyle BackColor="#C1E2EB" Width="50px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Baojia_no" HeaderText="报价号">
                                <HeaderStyle BackColor="#C1E2EB" Width="60px" Wrap="false" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="路径">                                
                                <ItemTemplate>               
                                   <a class="fa fa-folder-open" href='<%# Eval("baojia_file_path")%>' target="_blank"></a>                            
                                </ItemTemplate>
                                <HeaderStyle BackColor="#C1E2EB" Width="40px" Wrap="False" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="baojia_start_Date" HeaderText="项目开始日期" DataFormatString="{0:yyyy/MM/dd}">
                                <HeaderStyle BackColor="#C1E2EB" Width="110px" Wrap="false" />
                                <ItemStyle />
                            </asp:BoundField>
                            <asp:BoundField DataField="turns" HeaderText="轮次">
                                <HeaderStyle BackColor="#C1E2EB" Width="60px" Wrap="false" />
                            </asp:BoundField>
                            <asp:BoundField DataField="customer_name" HeaderText="直接顾客">
                                <HeaderStyle BackColor="#C1E2EB" Width="90px" Wrap="false" />
                            </asp:BoundField>
                            <asp:BoundField DataField="end_customer_name" HeaderText="最终顾客">
                                <HeaderStyle BackColor="#C1E2EB" Width="80px" Wrap="false" />
                            </asp:BoundField>
                            <asp:BoundField DataField="customer_project" HeaderText="顾客项目">
                                <HeaderStyle BackColor="#C1E2EB" Width="300px" Wrap="false" />
                            </asp:BoundField>
                            <asp:BoundField DataField="ljh" HeaderText="零件号">
                                <HeaderStyle BackColor="#C1E2EB" Width="120px" Wrap="false" />
                                <ItemStyle />
                            </asp:BoundField>
                            <asp:BoundField DataField="lj_name" HeaderText="零件名称">
                                <HeaderStyle BackColor="#C1E2EB" Width="180px" Wrap="false" />
                                <ItemStyle />
                            </asp:BoundField>

                            <asp:BoundField DataField="Ship_From" HeaderText="ShipFrom"  >
                                <HeaderStyle BackColor="#C1E2EB" Width="100px" Wrap="false" />
                                <ItemStyle />
                            </asp:BoundField>
                            <asp:BoundField DataField="Ship_To" HeaderText="ShipTo"  >
                                <HeaderStyle BackColor="#C1E2EB" Width="70px" Wrap="false" />
                            </asp:BoundField>

                            <asp:BoundField DataField="material" HeaderText="材料">
                                <HeaderStyle BackColor="#C1E2EB" Width="60px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="pc_date" HeaderText="批产时间" DataFormatString="{0:yyyy/MM/dd}">
                                <HeaderStyle BackColor="#C1E2EB" Width="150px" Wrap="false" />
                                <ItemStyle />
                            </asp:BoundField>
                            <asp:BoundField DataField="quantity_year" HeaderText="年用量" DataFormatString="{0:N0}" ItemStyle-HorizontalAlign="Right">
                                <HeaderStyle BackColor="#C1E2EB" Width="80px" />
                                <ItemStyle HorizontalAlign="Right"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="pc_per_price" HeaderText="批产单价" DataFormatString="{0:N2}" ItemStyle-HorizontalAlign="Right">
                                <HeaderStyle BackColor="#C1E2EB" Width="100px" Wrap="false" />
                                <ItemStyle />
                            </asp:BoundField>
                            <asp:BoundField DataField="price_year" HeaderText="年销售额" DataFormatString="{0:N0}" ItemStyle-HorizontalAlign="Right">
                                <HeaderStyle BackColor="#C1E2EB" Width="150px" Wrap="false" />
                                <ItemStyle HorizontalAlign="Right"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="pc_mj_price" HeaderText="批产模具价格" DataFormatString="{0:N0}" ItemStyle-HorizontalAlign="Right">
                                <HeaderStyle BackColor="#C1E2EB" Width="100px" Wrap="false" />
                                <ItemStyle HorizontalAlign="Right"></ItemStyle>
                            </asp:BoundField>                            
                            <asp:BoundField DataField="hetong_status" HeaderText="零件状态">
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
                            <asp:TemplateField HeaderText="跟进" ItemStyle-BackColor="LightBlue">
                                <ItemTemplate>
                                    <i class="fa fa-comments-o fa-lg" aria-hidden="true" style="cursor: pointer" param='<%# "?requestid="+Eval("requestid")+"&baojia_no="+Eval("Baojia_No")+"&turns="+Eval("turns")%>'></i>
                                                                          
                                </ItemTemplate>
                                <HeaderStyle BackColor="#C1E2EB" Width="200px" Wrap="False" />
                                <ItemStyle BackColor="LightBlue"></ItemStyle>
                            </asp:TemplateField>
                            <asp:BoundField DataField="sales_name" HeaderText="负责人"  >
                                <HeaderStyle BackColor="#C1E2EB" Width="100px" Wrap="false" />
                                <ItemStyle />
                            </asp:BoundField>
                            <asp:BoundField DataField="requestid" HeaderText="requestid" ShowHeader="false">
                                <HeaderStyle BackColor="#C1E2EB" Width="70px" Wrap="false" />
                            </asp:BoundField>


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
