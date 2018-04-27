<%@ Page Title="MES【报价跟踪记录】" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    ViewStateMode="Enabled" MaintainScrollPositionOnPostback="true" CodeFile="Review_Remark.aspx.cs"
    Inherits="Review_Remark" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script src="../Content/js/jquery.min.js" type="text/javascript"></script>    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <style>
        td {
            padding-left: 5px;
            padding-bottom: 3px;
        }

        .font-10 {
            font-size: 14px;
            color: Red;
            font-weight: bold;
        }

        .classdiv {
            display: inline;
        }
    </style>
    <style type="text/css">
        .datable {
            background-color: #9FD6FF;
            color: #333333;
            font-size: 12px;
        }

            .datable tr {
                height: 20px;
            }

            .datable .lup {
                background-color: #C8E1FB;
                font-size: 12px;
                color: #014F8A;
            }

                .datable .lup th {
                    border-top: 1px solid #FFFFFF;
                    border-left: 1px solid #FFFFFF;
                    font-weight: normal;
                }

            .datable .lupbai {
                background-color: #FFFFFF;
            }

            .datable .trnei {
                background-color: #F2F9FF;
            }

            .datable td {
                border-top: 1px solid #FFFFFF;
                border-left: 1px solid #FFFFFF;
            }

        .panel-heading {
            padding-bottom: 3px;
            padding-top: 3px;
        }

        .tblCondition td {
            white-space: nowrap;
        }
       
    </style>
   <%-- <script src="../Content/js/jquery-ui-1.10.4.min.js" type="text/javascript"></script>--%>
    <script src="../Content/js/layer/layer.js"></script>
   
    <script type="text/javascript">
        $(document).ready(function () {
            $("#mestitle").text("");
            $(".h3").remove();
            //关闭
            $("#btnCancel").bind("click", function (e) {
                var index = parent.layer.getFrameIndex(window.name); //获取窗口索引                
                parent.layer.close(index);
            });

            //评价
            $("[param]").click(function () {
                var id = $(this).attr("param");
                layer.prompt({ title: '输入改善结果意见', formType: 2 }, function (desc, index) {
                    $.ajax({
                        type: "Post",
                        url: "Review_Remark.aspx/Evaluate",//?desc=+ desc+"&slnid="+slnid
                        //方法传参的写法一定要对，str为形参的名字,str2为第二个形参的名字
                        data: "{'assessments':'" + desc + "','id':'" + id + "'}",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (data) {//返回的数据用data.d获取内容//                        
                            // alert(data.d)
                            $.each(eval(data.d), function (i, item) {

                                if (data.d == "0") {
                                    layer.alert("失败.");
                                }
                                else {
                                    layer.alert("成功.");
                                    layer.close(index);
                                    location.reload();

                                }
                            })

                        },
                        error: function (err) {
                            layer.alert(err);
                        }
                    });


                });
            })
        })//endready        

    </script>
    <asp:ScriptManager ID="ScriptManager" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
       <%-- <Triggers>
            <asp:PostBackTrigger ControlID="btnSave" />
        </Triggers>--%>
        <ContentTemplate>
             </ContentTemplate>
    </asp:UpdatePanel>   
            <div class="row" style="margin: 0px 2px 1px 2px;display:none" >
                <div class="col-sm-12  col-md-12">
                    <div class="panel panel-info">
                        <div class="panel-heading">
                            <strong>报价跟踪记录维护</strong>
                        </div>
                        <div class="panel-body">
                            <div class="col-sm-12 col-md-10">
                                <table style="" class="tblCondition">
                                    <tr>
                                        <td>报价号：
                                        </td>
                                        <td>
                                            <input id="txtBaoJia_no" class="form-control input-s-sm" runat="server" readonly />
                                        </td>
                                        <td>轮次：
                                        </td>
                                        <td>
                                            <input id="txtTurns" class="form-control input-s-sm" runat="server" readonly />
                                        </td>
                                        <td>日期：
                                        </td>
                                        <td>
                                            <input id="txtRiQi" class="form-control input-s-sm" runat="server" readonly />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>工号：
                                        </td>
                                        <td>
                                            <input id="txtEmpId" class="form-control input-s-sm" runat="server" readonly />
                                        </td>
                                        <td>姓名：
                                        </td>
                                        <td>
                                            <input id="txtXingMing" class="form-control input-s-sm" runat="server" readonly />
                                        </td>
                                    </tr>

                                </table>
                            </div>
                        </div>
                        

                    </div>
                </div>
            </div>
            <div class="col-sm-12 col-md-12">
                <div class="panel panel-info">
                    <div class="panel-heading">
                        <strong>结果改善跟踪记录</strong>
                    </div>
                    <div class="panel-body">
                        <div style="text-align: right; padding-bottom: 2px">
                            <asp:Button ID="btnSave" class="btn btn-primary" Style="height: 35px; width: 100px" OnClick="btnSave_Click"
                                Text="保 存" runat="server" />
                            <input id="btnCancel"  type="button" value="关闭"  class="btn btn-primary" />
                        </div>
                        <asp:GridView ID="GridView1" runat="server" Width="100%" CssClass="datable" border="1"
                            CellPadding="2" CellSpacing="1" AutoGenerateColumns="False" 
                            OnRowDataBound="GridView1_RowDataBound">
                            <RowStyle CssClass="lupbai" />
                            <HeaderStyle CssClass="lup" />
                            <AlternatingRowStyle CssClass="trnei" />
                            <Columns>
                                <asp:BoundField DataField="" HeaderText="No.">
                                    <HeaderStyle Width="20px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="create_date" HeaderText="创建时间">
                                    <HeaderStyle Width="120px" />
                                </asp:BoundField>
                                 <asp:BoundField DataField="create_by_empid" HeaderText="工号" HtmlEncode="False">
                                    <HeaderStyle Width="50px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="create_by_empname" HeaderText="姓名">
                                    <HeaderStyle Width="50px" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="状态描述">                                    
                                    <ItemTemplate>
                                        <asp:Label ID="lblRemarks" runat="server" Text='<%# Bind("remarks") %>'></asp:Label>
                                        <asp:TextBox ID="txtValue" runat="server" Height="50px"  TextMode="MultiLine" Width="300px" text='<%# Bind("remarks") %>'></asp:TextBox>
                                    </ItemTemplate>
                                    <HeaderStyle Width="300px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="附件">
                                    <ItemTemplate>
                                        <a href='<%# Eval("file_path")%>' target="_blank"><%# Eval("file_name")%></a>
                                    </ItemTemplate>
                                    <HeaderStyle BackColor="#C1E2EB" Width="100px" Wrap="False" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="上传附件">
                                    <ItemTemplate>
                                        <asp:FileUpload ID="txtFile" runat="server" Width="100px" />
                                    </ItemTemplate>
                                     <HeaderStyle BackColor="#C1E2EB" Width="50px" Wrap="False" />
                                </asp:TemplateField>
                                <asp:boundfield HeaderText="id" DataField="id" >                                                                         
                                </asp:boundfield>
                                <asp:TemplateField HeaderText="评价" Visible="false">
                                    <ItemTemplate>
                                          <%# Eval("Assessments")%>
                                        <asp:Label ID="lblAssessments" runat="server" Visible="false"><i class="fa fa-comments-o fa-lg" aria-hidden="true" style="cursor: pointer" title="评价" param='<%# Eval("Id")%>'  ></i></asp:Label>
                                    </ItemTemplate>                                    
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>

                    </div>

                </div>
            </div>
    
</asp:Content>
