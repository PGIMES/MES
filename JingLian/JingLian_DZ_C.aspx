<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="JingLian_DZ_C.aspx.cs" Inherits="JingLian_JingLian_DZ_C" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
<script src="../Content/js/jquery.min.js" type="text/javascript"></script>
    <script src="../Content/js/plugins/layer/layer.min.js" type="text/javascript"></script>
 <script type="text/javascript" language="javascript">
     $(document).ready(function () {
     });
     $("#mestitle").text("【转运包加液记录】");
    
</script> 
 <script type="text/javascript" language="javascript">
     $(document).ready(function () {

         $("#tst").click(function () {

         });

     });
     // $("div[class='h3']").text($("div[class='h3']").text() + "【精炼测氢】");
     $("#mestitle").text("【熔炼精炼打渣记录】");
     function show_cur_times() {
         //获取当前日期
         var date_time = new Date();
         //年
         var year = date_time.getFullYear();
         //判断小于10，前面补0
         if (year < 10) {
             year = "0" + year;
         }
         //月
         var month = date_time.getMonth() + 1;
         //判断小于10，前面补0
         if (month < 10) {
             month = "0" + month;
         }
         //日
         var day = date_time.getDate();
         //判断小于10，前面补0
         if (day < 10) {
             day = "0" + day;
         }
         //时
         var hours = date_time.getHours();
         //判断小于10，前面补0
         if (hours < 10) {
             hours = "0" + hours;
         }
         //分
         var minutes = date_time.getMinutes();
         //判断小于10，前面补0
         if (minutes < 10) {
             minutes = "0" + minutes;
         }

         //秒
         var seconds = date_time.getSeconds();

         if (seconds < 10) {
             seconds = "0" + seconds;
         }

         var date_str = year + "-" + month + "-" + day + " " + hours + ":" + minutes + ":" + seconds + " ";

         $("span[id*='lb_end']").text(date_str);


     }

     //window.onload = 
     // setInterval("show_cur_times()", 100);
    </script>
    <div class="row row-container">
        <div class="col-sm-12 ">
            <div class="panel panel-info">
                <div class="panel-heading">
                    <strong>基本信息</strong>
                    <asp:ScriptManager ID="ScriptManager1" runat="server">
                    </asp:ScriptManager>
                </div>
                <div class="panel-body">
                    <div class="col-sm-12">
                        
                                <table>
                                    <tr>
                                        <td>
                                            日期:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txt_date" class="form-control input-s-sm" runat="server"
                                                ReadOnly="True"></asp:TextBox>
                                        </td>
                                        <td>
                                            时间:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txt_time" class="form-control input-s-sm " runat="server"
                                                ReadOnly="True"></asp:TextBox>
                                        </td>
                                        <td>
                                            班别:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txt_shift" class="form-control input-s-sm" runat="server"
                                                ReadOnly="True"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            工号:
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="txt_gh" runat="server" class="form-control input-s-sm"
                                                AutoPostBack="True" OnSelectedIndexChanged="txt_gh_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            姓名:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txt_name" class="form-control input-s-sm" runat="server"
                                                ReadOnly="True"></asp:TextBox>
                                        </td>
                                        <td>
                                            班组:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txt_banzu" class="form-control input-s-sm" runat="server"
                                                ReadOnly="True"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            熔炼炉号：</td>
                                        <td>
                                            <asp:TextBox ID="txt_luhao" runat="server" class="form-control input-s-sm"></asp:TextBox>
                                        </td>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                    </tr>
                                </table>
                                </div>
                       
                    </div>
                </div>
            </div>
        </div>

 
    <div class="row row-container" style=" margin:10px">
        <div class="col-sm-12 ">
            
            <table border="1" >
             <tr>
                           <th colspan="3" 
                               
                               style="background-color: #9097A9; font-weight: bold; font-size: large;" />
                              除渣内容
                       </tr>
                <tr>
                    <td width="40%">
                     
                        <strong>1、精炼前铝液温度(℃)</strong></td>
                    <td colspan="2">
                        <asp:TextBox ID="txt_before_wendu" runat="server" 
                            class="form-control input-s-sm" ></asp:TextBox>
                    </td>
                    
                </tr>
              
                <tr>  <td><strong>2、<span>精炼剂使用量</span><span lang="EN-US">(6~8Kg)</span></strong></td>
                <asp:UpdatePanel runat="server"><ContentTemplate>
                    <td>
                        <asp:TextBox ID="txt_jljuse" runat="server" 
                            class="form-control input-s-sm" 
                            ontextchanged="txt_jljuse_TextChanged" 
                            AutoPostBack="True" ></asp:TextBox>
                    </td>
                    </ContentTemplate></asp:UpdatePanel>
                    <td><strong>(已精炼5-10分钟)</strong></td>
                </tr>
             
                <tr><td><strong>3、<span>除渣剂使用量</span><span lang="EN-US">(10~15Kg)</span></strong></td>
                <asp:UpdatePanel  runat="server"><ContentTemplate>
                    <td>
                        <asp:TextBox ID="txt_czjuse" runat="server" 
                            class="form-control input-s-sm" 
                            ontextchanged="txt_czjuse_TextChanged" 
                            AutoPostBack="True" ></asp:TextBox>
                    </td>
                    </ContentTemplate></asp:UpdatePanel>
                    <td><strong>(已除渣5-10分钟)</strong></td>
                </tr>
                <tr><td><strong>4、已静置10分钟，熔解室，保温室，放料室已无残渣</strong></td>
                    <td colspan="2">
                        <asp:DropDownList ID="ddl_check" runat="server" class="form-control input-s-sm" >
                            <asp:ListItem></asp:ListItem>
                            <asp:ListItem>OK</asp:ListItem>
                            <asp:ListItem>NG</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr><td><strong>5、精炼后炉膛气氛温度(℃)</strong></td>
                    <td colspan="2">
                        <asp:TextBox ID="txt_after_wendu" runat="server" class="form-control input-s-sm" ></asp:TextBox>
                    </td>
                </tr>
            </table>
            
        </div>

    </div>
     <div class="row row-container">
        <div class="col-sm-12 ">
        
             <div id="Div1" runat="server" class="col-sm-3 " style="padding: 0;
                                        position: relative; float: left; top: 0px; left: 0px; width: 200px;
                                        height: 70px;">
                                        <asp:Button ID="btn_begin" runat="server" Font-Size="X-Large"
                                            class="btn btn-primary" Style="position: absolute; left: -1;
                                            right: -18px; width: 200px; top: -1px; height: 70px;" Text="开始精炼"
                                            OnClick="btn_begin_Click" />
                                        <div id="div2" runat="server">
                                            <asp:Label ID="lb_start" runat="server" 
                                                Style="position: absolute;
                                                right: -15px; left: 40px; width: 160px; top: 45px; height: 20px;"></asp:Label>
                                        </div>
                                    </div>
            
               <div id="Div5" runat="server" class="col-sm-3 col-md-offset-1" style="padding: 0;
                                        position: relative; float: left; top: 0px; left: 0px; width: 200px;
                                        height: 70px;">
                                        <asp:Button ID="btn_end" runat="server" Font-Size="X-Large" class="btn btn-primary"
                                            Style="position: absolute; left: -1; right: -18px; width: 200px;
                                            top: -1px; height: 70px;" Text="完成精炼" 
                                            OnClick="btn_end_Click" />
                                        <div id="div6" runat="server">
                                            <asp:Label ID="lb_end" runat="server" Style="position: absolute;
                                                right: 0px; left: 40px; width: 160px; top: 45px"></asp:Label>
                                        </div>
                                    </div>
              <div id="Div3" runat="server" class="col-sm-3 col-md-offset-1" style="padding: 0;
                                        position: relative; float: left; top: 0px; left: 0px; width: 200px;
                                        height: 70px;">
                                        <asp:Button ID="btn_return" runat="server" 
                                            Font-Size="X-Large" class="btn btn-primary"
                                            Style="position: absolute; left: -1; right: -18px; width: 200px;
                                            top: -1px; height: 70px;" Text="返回" onclick="btn_return_Click" 
                                            />
                                      
                                    </div>
        </div></div>

           <div class="row row-container" style=" margin:20px">
        <div class="col-sm-12 ">
        </div></div>
</asp:Content>

