<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="Login.aspx.cs" Inherits="Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContent" runat="Server">
    <style type="text/css">
        td
        {
            padding-left:0px;
            padding-right:5px;
            padding-top:5px;
            text-align:right;
        }
       
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <script type="text/javascript">

        $(document).ready(function () {
            $("div[class='h3']").hide();

            $("#btnBack").click(function () {
                //                var index = parent.layer.getFrameIndex(window.name); //获取窗口索引                       
                //                parent.layer.close(index);
                closelogin();
            })
            $("#btnLogin").click(function () {

                //                emp_no = $("#txtEmpNo").val();
                //                emp_name =$("#txtEmpName").val();
                //                emp_banbie =$("#dropBanBie").val();//.SelectedValue;
                //                emp_banzhu =$("#txtBanZhu").val();
                //                emp_gongwei = "";
                //                hejing = $("#dropHeJin").val();//.SelectedValue;                
                //                //logindate = DateTime.Now;
                //                logindemo = "";
                //                Status = "登入";
                //                emp_gongwei = Request["gongwei"];
                //                emp_shebei = "";

                //                $.post("Login.aspx", { emp_no:emp_no,emp_name:emp_name ,emp_banbie:emp_banbie
                //                                        ,emp_banzhu:emp_banzhu,logindemo:logindemo,Status:Status
                //                }, function (result) {
                ////                    $("span").html(result);
                //                    
                //                });
            })

            $("#btnPost").click(function () {
                AjaxRquest();
            })

            function closelogin() {
                var index = parent.parent.layer.getFrameIndex(window.name); //获取窗口索引                       
                parent.parent.layer.close(index);

            }


        })
        function AjaxRquest() {
            emp_no = $("#txtEmpNo").val();
            emp_name =$("#txtEmpName").val();
            emp_banbie =$("#dropBanBie").val();//.SelectedValue;
            emp_banzhu =$("#txtBanZhu").val();
            emp_gongwei = "";
            hejing = $("#dropHeJin").val();//.SelectedValue;                
            //logindate = DateTime.Now;
            logindemo = "";
            Status = "登入";
            emp_gongwei = Request["gongwei"];
            emp_shebei = "";
            if (emp_no == "") {
                layer.alert("请输入工号！");
                return;
            }

//                            $.post("Login.aspx", { emp_no:emp_no,emp_name:emp_name ,emp_banbie:emp_banbie
//                                                    ,emp_banzhu:emp_banzhu,logindemo:logindemo,Status:Status
//                            }, function (result) {
//            //                    $("span").html(result);

            //                            });
            var ids=[] ;
            ids.push({ "id":1,"name": "name1"});
            ids.push({ "id":2,"name": "name2"});

            $.ajax({
                url: location.href,
                type: "POST",
                data: { "RequestType": "AjaxRequest", "id": "1","msg":"","ids":ids }, //模拟个数据
                success: function (data) {
                    if (data != "") {
                        layer.alert(data);
                    }
                }
            });
        }

            
    </script>
    <script type="text/javascript" >
           function load()
           { Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler); }
           function EndRequestHandler() {               
               $("#btnBack").click(function () {
                   var index = parent.layer.getFrameIndex(window.name); //获取窗口索引                       
                   parent.layer.close(index);

               })

           }
   </script>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

    <div style="padding-left: 5px; padding-right: 5px">
        <div class="panel panel-info ">
            <div class="panel-heading" style="vertical-align: middle;" align="center">
                <strong>员工登入</strong>
            </div>

            <div class="form-group" style="vertical-align: middle; text-align: center;">
                <div>
                    <table style="text-align: left; vertical-align: inherit;width:100%">
                        <tr>
                            <td>
                                车间:
                            </td>
                            <td >
                                <asp:DropDownList ID="dropCheJian" class="form-control  input-sm" Enabled="false" runat="server">
                                </asp:DropDownList>
                            </td>
                            <td class="td">
                                岗位:
                            </td>
                            <td >
                                <asp:textbox ID="txtGangWei" class="form-control  input-sm disabled"  Enabled=false  runat="server">
                                </asp:textbox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                日期:
                            </td>
                            <td class="style1">
                                <asp:TextBox ID="txtDate" class="form-control input-sm disabled" Enabled=false runat="server"></asp:TextBox>
                            </td>
                            <td>
                                时间:
                            </td>
                            <td>
                                <asp:TextBox ID="txtTime" class="form-control input-sm disabled " Enabled=false  runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                工号:
                            </td>
                            <td >
                                <asp:TextBox ID="txtEmpNo" class="form-control input-sm" runat="server" 
                                    ontextchanged="txtEmpNo_TextChanged" AutoPostBack="True" BackColor="#FFFFCC"></asp:TextBox>
                            </td>
                            <td>
                                姓名:
                            </td>
                            <td>
                                <asp:TextBox ID="txtEmpName" class="form-control input-sm disabled"  Enabled=false  runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                班别:
                            </td>
                            <td >
                                <asp:DropDownList ID="dropBanBie" class="form-control  input-sm disabled"    runat="server">
                                    <asp:ListItem Value="0">-请选择班别-</asp:ListItem>
                                     <asp:ListItem Value="白班">白班</asp:ListItem>
                                      <asp:ListItem Value="晚班">晚班</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td>
                                班组:
                            </td>
                            <td>
                                <asp:TextBox ID="txtBanZhu" class="form-control  input-sm disabled"  Enabled="false"   runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                操作设备:
                            </td>
                           
                            <td colspan="3" style="text-align:left">
                                <asp:CheckBoxList ID="chkE"  runat="server" RepeatDirection="Horizontal" RepeatColumns="5" RepeatLayout="Flow"></asp:CheckBoxList>
                            </td>
                        </tr>
                        <tr>
                            <td  style="vertical-align: top">
                                交班状态:
                            </td>
                           
                            <td colspan="3"  style="font-size:8px;text-align:left">
                                <asp:Label ID="lblDemo" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td >
                                接班状态:
                            </td>
                            <td colspan="3">
                                <asp:DropDownList ID="dropLoginstatus" class="form-control  input-sm"  runat="server">
                                </asp:DropDownList>
                            </td>
                            
                        </tr>
                        <tr>
                            <td>
                                异常说明:
                            </td>                           
                            <td colspan="3">
                                <asp:textbox ID="txtDemo" class="form-control " Height="80px" runat="server"  placeholder="请输入设备状态，交接班状态,信息等."
                                    TextMode="MultiLine"  ></asp:textbox>
                            </td>
                        </tr>
                    </table>
                </div>  
                <table style="width: 100%">
                    <tr>
                        <td style="text-align: right">
                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                <ContentTemplate>
                                    <asp:Button ID="btnLogin" runat="server" class="btn btn-primary" Text="登入" OnClick="btnLogin_Click" />&nbsp;&nbsp;<input
                    id="btnPost" type="button" value="button" hidden="hidden" /></ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                        <td style="text-align: left">
                           <input id="btnBack" class="btn btn-primary" type="button" value="返回" />
                        </td>
                    </tr>
                </table>
              
                
                
            </div>
        </div>

    </div>
</asp:Content>
