<%@ Page Title="登出" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="Logout.aspx.cs" Inherits="Logout" %>


<asp:Content ID="Content1" ContentPlaceHolderID="headContent" runat="Server">
    <style type="text/css">
        td
        {
            padding-left:15px;
            padding-right:15px;
            padding-top:10px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <script type="text/javascript">

        $(document).ready(function () {
            $("div[class='h3']").hide();

            $("#btnBack").click(function () {
                var index = parent.layer.getFrameIndex(window.name); //获取窗口索引                       
                parent.layer.close(index);

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




        })

    </script>
     <script type="text/javascript" >
        
      function slide()
     {  } 
     function load() 
     { Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler); }
     function EndRequestHandler() {
         slide();
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
                <strong>员工登出</strong>
            </div>

            <div class="form-group" style="vertical-align: middle; text-align: center;">
                <div>
                    <table style="text-align: left; vertical-align: inherit;width:100%">
                        <tr>
                            <td>
                                日期:
                            </td>
                            <td class="style1">
                                <asp:TextBox ID="txtDate" class="form-control input-sm disabled" runat="server"></asp:TextBox>
                            </td>
                            <td>
                               
                            </td>
                            <td>
                               
                            </td>
                        </tr>
                        <tr>
                            <td>
                                工号:
                            </td>
                            <td >
                                <asp:DropDownList ID="dropGongHao" class="form-control input-sm" runat="server" 
                                     AutoPostBack="True" 
                                    onselectedindexchanged="dropGongHao_SelectedIndexChanged"></asp:DropDownList>
                            </td>
                            <td>
                                
                            </td>
                            <td>
                                
                            </td>
                        </tr>
                       
                        <tr>
                            <td>
                                已登入设备:
                            </td>
                           
                            <td colspan="3">
                                <asp:CheckBoxList ID="chkE" class="form-control  input-sm" runat="server" RepeatDirection="Horizontal" RepeatColumns="5" RepeatLayout="Flow"></asp:CheckBoxList>
                            </td>
                        </tr>
                        <tr>
                            <td >
                                交班状态:
                            </td>
                            <td colspan="3">
                                <asp:DropDownList ID="dropLogoffstatus" class="form-control  input-sm" runat="server">
                                </asp:DropDownList>
                            </td>
                            
                        </tr>
                        <tr>
                            <td>
                                异常说明:
                            </td>                           
                            <td colspan="3">
                                <asp:textbox ID="txtDemo" class="form-control  input-sm" Height="80px" runat="server" 
                                    TextMode="MultiLine"   ></asp:textbox>
                            </td>
                        </tr>
                    </table>
                </div>
                <hr />
                <%--<button lay-submit lay-filter="go" onclick="document.getElementById('btnBack').click();">提交</button>--%>
                
                <table style=" width:100%"><tr><td style=" text-align:right"> <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <asp:Button ID="btnLogout" runat="server" class="btn btn-primary" Text="登出" OnClick="btnLogout_Click" /></ContentTemplate>
                            </asp:UpdatePanel></td><td style="text-align:left">&nbsp;&nbsp; &nbsp;<input id="btnBack" class="btn btn-primary" type="button" value="返回" /></td></tr></table>
               
            </div>
        </div>

    </div>
</asp:Content>
