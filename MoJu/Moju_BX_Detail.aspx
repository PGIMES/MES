<%@ Page Title="MES【维修单】" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Moju_BX_Detail.aspx.cs" Inherits="MoJu_Moju_BX_Detail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
 <script src="../Content/js/jquery.min.js" type="text/javascript"></script>
    <script src="../Content/js/plugins/layer/layer.min.js" type="text/javascript"></script>
 <script type="text/javascript" language="javascript">
     $(document).ready(function () {

         $("#tst").click(function () {

         });

         if ($("input[id*='txt_wx_result']").val() == "需下模维修") 
         {
             $("td[id='div_cs_down']").show();
             $("td[id='div_cs_text']").show();
         }
         else 
             {
                 $("td[id='div_cs_down']").hide();
                 $("td[id='div_cs_text']").hide();
             }
         

     });

     $("#mestitle").text("【维修单】");
 </script>
  <script  type="text/javascript">
      function goBack() {
          window.history.back();
          return false;
   
      } 
</script> 



    <div id="Div_BX"  runat="server">
      <div class="row" style="margin: 0px 2px 1px 2px">
        <div class="col-sm-12  col-md-10">
            <div class="panel panel-info">
                <div class="panel-heading">
                    <strong>报修</strong>
                    <asp:Label ID="lb_dh" runat="server" Text="Label"></asp:Label>
                </div>
                <div class="panel-body">
                    <div class="col-sm-12 col-md-10">
                        <table style="width: 100%">
                            <tr>
                                <td>
                                     模具号：
                                </td>
                                <td>
                                    <input id="txt_moju_no" class="form-control input-s-sm"   ReadOnly="True" runat="server" />
                                </td>
                                <td>
                                    设备简称：
                                </td>
                                <td>
                                    <input id="txt_sbname" class="form-control input-s-sm"  ReadOnly="True"  runat="server" />
                                </td>
                                <td>
                                    模具类型：
                                </td>
                                <td>
                                    <input id="txt_moju_type" class="form-control input-s-sm"   ReadOnly="True" runat="server" />
                                </td>
                               
                                
                            </tr>
                            <tr>
                            
                              <td>
                                    零件号：
                                </td>
                                <td>
                                    <input id="txt_part" class="form-control input-s-sm"   ReadOnly="True" runat="server" />
                                </td>

                                 <td>
                                    模号：
                                </td>
                                <td>
                                    <input id="txt_mo_no" class="form-control input-s-sm"   ReadOnly="True" runat="server" />
                                </td>

                                 <td>
                                    班别：
                                </td>
                                <td>
                                    <input id="txt_banbie" class="form-control input-s-sm"   ReadOnly="True" runat="server" />
                                </td>
                            </tr>
                            <tr>
                           
                            <td>
                                    报修日期：
                                </td>
                                <td>
                                    <input id="txt_bx_date" class="form-control input-s-sm"  ReadOnly="True"  runat="server" />
                                </td>
                                 <td>
                                    报修时间：
                                </td>
                                <td>
                                    <input id="txt_bx_time" class="form-control input-s-sm"  ReadOnly="True"  runat="server" />
                                </td>
                                <td>
                                    报修人：
                                </td>
                                <td>
                                    <input id="txt_bxr" class="form-control input-s-sm"  ReadOnly="True"  runat="server" />
                                </td>
                            
                            </tr>
                     <tr>
                                <td>
                                    故障类型：
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddl_gz_type" 
                                        class="form-control input-s-sm" runat="server" AutoPostBack="True"  
                                        
                                        />
                                </td>
                                
                              
                            </tr>
                           <tr>
                           <td>
                                    故障描述：
                                </td>
                                <td colspan=5>
                                    <textarea id="txt_gz_desc"   class="form-control " rows=3  runat="server" readonly></textarea>
                                </td>
                           </tr>
                        </table>
                    </div>
                </div>
            </div>
        </div>
    </div>
    </div>
    <div id="Div_WX" runat="server">
    <div class="row" style="margin: 0px 2px 1px 2px">
        <div class="col-sm-12  col-md-10">
            <div class="panel panel-info">
                <div class="panel-heading">
                    <strong>维修</strong>
                </div>
                <div class="panel-body">
                    <div class="col-sm-12 col-md-10">
                        <table style="width: 100%"  id="table1">
                            <tr>
                                <td>
                                     维修措施：
                                </td>
                                <td>
                                    <input id="txt_wx_cs" class="form-control input-s-sm"   ReadOnly="True" runat="server" />
                                </td>
                                <td>
                                    维修结果：
                                </td>
                                <td>
                                    <input id="txt_wx_result" class="form-control input-s-sm"  ReadOnly="True"  runat="server"  />
                                </td>
                               
                                <td  id="div_cs_down">
                                    下模后还需采取的措施：
                                </td>
                                <td  id="div_cs_text">
                                    <input id="txt_xm_cs" class="form-control input-s-sm"   ReadOnly="True" runat="server" />
                                </td>

                                <td></td>
                                 <td></td>
                                
                            </tr>
                            
                            <tr>
                           
                            <td>
                                    维修开始时间：
                                </td>
                                <td>
                                    <input id="txt_wx_begindate" class="form-control input-s-sm"  ReadOnly="True"  runat="server" />
                                </td>
                                 <td>
                                    维修结束时间：
                                </td>
                                <td>
                                    <input id="txt_wx_enddate" class="form-control input-s-sm"  ReadOnly="True"  runat="server" />
                                </td>
                                <td>
                                    维修人：
                                </td>
                                <td>
                                    <input id="txt_wxr" class="form-control input-s-sm"  ReadOnly="True"  runat="server" />
                                </td>
                            
                            </tr>
                   
                        </table>
                    </div>
                </div>
            </div>
        </div>
    </div>
    </div>
    <div id="Div_Qr" runat="server">
       <div class="row" style="margin: 0px 2px 1px 2px">
        <div class="col-sm-12  col-md-10">
            <div class="panel panel-info">
                <div class="panel-heading">
                    <strong>维修确认</strong>
                </div>
                <div class="panel-body">
                    <div class="col-sm-12 col-md-10">
                        <table style="width: 100%">
                            <tr>
                                <td>
                                     工号：
                                </td>
                                <td>
                                    <input id="txt_qr_gh" class="form-control input-s-sm"   ReadOnly="True" runat="server" />
                                </td>
                                <td>
                                    姓名：
                                </td>
                                <td>
                                    <input id="txt_qr_name" class="form-control input-s-sm"  ReadOnly="True"  runat="server" />
                                </td>
                               
                            </tr>
                            
                            <tr>
                           
                            <td>
                                    确认完成日期：
                                </td>
                                <td>
                                    <input id="txt_qr_date" class="form-control input-s-sm"  ReadOnly="True"  runat="server" />
                                </td>
                                 <td>
                                    确认完成时间：
                                </td>
                                <td>
                                    <input id="txt_qr_time" class="form-control input-s-sm"  ReadOnly="True"  runat="server" />
                                </td>
                               
                            
                            </tr>
                            <tr>
                            
                             <td>
                                    对维修结果的补充说明：
                                </td>
                                <td colspan=3>
                                    <textarea id="txt_qr_remark"   class="form-control " rows=3 runat="server" readonly ></textarea>
                                </td>
                            </tr>
                   
                        </table>
                    </div>
                </div>
            </div>
        </div>
    </div>
    </div>
    
              
</asp:Content>

