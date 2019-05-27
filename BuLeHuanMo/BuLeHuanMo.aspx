<%@ Page Title="MES【换模记录】" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    ViewStateMode="Enabled" MaintainScrollPositionOnPostback="true" CodeFile="BuLeHuanMo.aspx.cs"
    Inherits="BuLeHuanMo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <script src="../Content/js/jquery.min.js" type="text/javascript"></script>
    <script src="../Content/js/plugins/layer/layer.min.js" type="text/javascript"></script>
   <%--  <script type="text/javascript">

         $(document).ready(function () {
             $("#btnBack").click(function () {
                 closelogin();
             })
         })
        


    </script>--%>



    <script type="text/javascript" language="javascript">
        function updateStock(id) {
            //iframe层
            parent.layer.open({
                type: 2,
                title: '修改',
                shadeClose: false, //点击遮罩关闭
                shade: 0.8,
                area: ['30%', '45%'],
                maxmin: true,
                closeBtn: 1,
                content: ['Moju_RK.aspx?id=' + id, 'yes'], //iframe的url，yes是否有滚动条
                //yes: function (index, layero) {
                //    alert(index);
                //    alert(layero);
                //},
                end: function () {
                    location.reload();
                }

            });
        }
    </script>

    <style>
        td
        {
            padding-left: 5px;
            padding-bottom: 3px;
        }
        .font-10
        {
            font-size: 14px;
            color: Red;
            font-weight: bold;
        }
        .classdiv
        {
            display: inline;
        }
    </style>
    <script src="Content/js/jquery-ui-1.10.4.min.js" type="text/javascript"></script>
    
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <script type="text/javascript">
        function getQueryString(name) {
            var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)", "i");
            var r = decodeURI(window.location.search).substr(1).match(reg);
            if (r != null) return unescape(r[2]); return null;
        }

        $(document).ready(function () {
            $("#mestitle").text("【换模记录】");

            $("select[id*='selLeiBie']").change(function () {

                if ($("select[id*='selLeiBie']").val() == "下模") {
                    disableXM("div[id*='ShangMo']");
                    enableXM("div[id*='divXiaMo']")
                } else if ($("select[id*='selLeiBie']").val() == "上模") {
                    disableXM("div[id*='divXiaMo']");
                    enableXM("div[id*='ShangMo']")
                } else if ($("select[id*='selLeiBie']").val() == "") {
                    disableXM("div[id*='divXiaMo']");
                    disableXM("div[id*='ShangMo']")
                } else { 
                    enableXM("div[id*='divXiaMo']");
                    enableXM("div[id*='ShangMo']")
                }

            });
            
            $("#tt").attr("src","/YaSheTou/YST_Record.aspx?deviceid="+getQueryString("deviceid"));


        })//endready



        $('input[id=txt_pizhong_1]').change(function () {
            var text = $(this).val();
            $("input[name='txt_touliao_1']").val(text);
        });

         function disableXM(id) {
             $(id+" :input").attr('disabled', true);  
         } 
         function enableXM(id) {
             $(id+" :input").removeAttr('disabled');  
         }
        

    </script>

    <asp:UpdatePanel runat="server" ID="p1" UpdateMode="Conditional">
            <ContentTemplate>
                <div class="row" style="margin: 0px 2px 1px 2px">
                    <div class="col-sm-12  col-md-10">
                        <div class="panel panel-info">
                            <div class="panel-heading">
                                <strong>基本信息</strong>
                            </div>
                            <div class="panel-body">
                                <div class="col-sm-12 col-md-10">
                                    <table style="width: 100%">
                                        <tr>
                                            <td>
                                                日期：
                                            </td>
                                            <td>
                                                <input id="txtRiQi" class="form-control input-s-sm"   ReadOnly="True" runat="server" />
                                            </td>
                                            <td>
                                                时间：
                                            </td>
                                            <td>
                                                <input id="txtShiJian" class="form-control input-s-sm"  ReadOnly="True"  runat="server" />
                                            </td>
                                            <td>
                                                班别：
                                            </td>
                                            <td>
                                                <input id="txtBanBie" class="form-control input-s-sm"   ReadOnly="True" runat="server" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                工号：
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="dropGongHao" class="form-control input-s-sm" runat="server"
                                                    AutoPostBack="True" 
                                                    OnSelectedIndexChanged="dropGongHao_SelectedIndexChanged" BackColor="Yellow" />
                                            </td>
                                            <td>
                                                姓名：
                                            </td>
                                            <td>
                                                <input id="txtXingMing" class="form-control input-s-sm"  ReadOnly="True"  runat="server" />
                                            </td>
                                            <td>
                                                班组：
                                            </td>
                                            <td>
                                                <input id="txtBanZu" class="form-control input-s-sm"   ReadOnly="True" runat="server" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                设备号：
                                            </td>
                                            <td>
                                                <input id="txtSheBeiHao" class="form-control input-s-sm" runat="server"   ReadOnly="True"/>
                                            </td>
                                            <td>
                                                设备简称：
                                            </td>
                                            <td>
                                                <input id="txtSheBeiJianCheng" class="form-control input-s-sm"   ReadOnly="True" runat="server" />
                                            </td>
                                            <td>
                                    
                                            </td>
                                            <td>
                                    
                                            </td>
                                        </tr>
                                        <tr>
                               
                                            <td>
                                                换模原因说明：
                                            </td>
                                            <td>
                                    

                                                <asp:DropDownList ID=selYuanYin  class="form-control input-s-sm" runat="server" 
                                                    onselectedindexchanged="selYuanYin_SelectedIndexChanged" 
                                                    AutoPostBack="True" BackColor="Yellow">
                                                      <asp:ListItem Value ="">--请选择--</asp:ListItem>
                                                 <asp:ListItem Value ="仅上模---开始新的产品生产">仅上模---开始新的产品生产</asp:ListItem>
                                                 <asp:ListItem Value ="仅上模---开始新的模具调试">仅上模---开始新的模具调试</asp:ListItem>
                                                 <asp:ListItem Value ="仅下模---结束生产，停机">仅下模---结束生产，停机</asp:ListItem>
                                                 <asp:ListItem Value ="仅下模---结束调试，停机">仅下模---结束调试，停机</asp:ListItem>
                                                 <asp:ListItem Value ="仅下模---故障，或有质量问题，停机">仅下模---故障，或有质量问题，停机</asp:ListItem>
                                                 <asp:ListItem Value ="先下模再上模---换一个产品生产">先下模再上模---换一个产品生产</asp:ListItem>
                                                 <asp:ListItem Value ="先下模再上模---停止生产，改调试模具">先下模再上模--停止生产，改调试模具</asp:ListItem>
                                                 <asp:ListItem Value ="先下模再上模---停止调试模具，改生产">先下模再上模--停止调试模具，改生产</asp:ListItem>
                                                 <asp:ListItem Value ="先下模再上模---故障，或有质量问题，改生产调试模具">先下模再上模----故障，或有质量问题，改生产调试模具</asp:ListItem>

                                                </asp:DropDownList>

                                            </td>

                                             <td>
                                                换模类别：
                                            </td>
                                            <td>
                                    
                                                <asp:TextBox ID=txtleibie runat =server class="form-control input-s-sm" ReadOnly="True"></asp:TextBox>
                                                 <asp:DropDownList ID=selLeiBie  class="btn btn-large btn-primary disabled" runat="server"  AutoPostBack=true 
                                                     onselectedindexchanged="selLeiBie_SelectedIndexChanged"   Enabled =false   Visible=false  >
                                       
                                                 <asp:ListItem Value ="上模">上模</asp:ListItem>
                                                   <asp:ListItem Value ="下模">下模</asp:ListItem>
                                                     <asp:ListItem Value ="先卸模再上模">先卸模再上模</asp:ListItem>
                                                 </asp:DropDownList> 
                                            </td>



                                            <td>
                                                原因类别：
                                            </td>
                                            <td>
                                     
                                                 <asp:TextBox ID=selYuanYinLeiBie runat=server   class="form-control input-s-sm" ReadOnly="True" ></asp:TextBox>
                                        
                                   
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row" style="margin: 0px 2px 1px 2px">
                    <div class="row">
                        <div class="col-sm-12">
                            <div class="col-sm-6 col-md-5" id="divXiaMo" runat =server  >
                                <div class="panel panel-info">
                                    <div class="panel-heading">
                                        <strong>下模（卸）</strong>
                                    </div>
                                    <div class="panel-body">
                                        <div class="form-group">
                                            <span class="col-sm-4">模具号：</span>
                                            <div class="col-sm-8">
                                    
                                                    <asp:DropDownList ID=ddlmoju_down runat =server AutoPostBack="True"  class="form-control input-s-sm" 
                                                        onselectedindexchanged="ddlmoju_down_SelectedIndexChanged"  ></asp:DropDownList>
                                            </div>
                                        </div>
                                         <div class="form-group">
                                            <span class="col-sm-4">模具类型：</span>
                                            <div class="col-sm-8">
                                                <asp:TextBox ID="txtmojutype_down" class="form-control" runat="server" 
                                                    ReadOnly="True"></asp:TextBox>
                                            </div>
                                        </div>


                                        <div class="form-group">
                                            <span class="col-sm-4">零件名称：</span>
                                            <div class="col-sm-8">
                                                <asp:TextBox ID="txtLingJianMing" class="form-control" runat="server"  ReadOnly="True"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <span class="col-sm-4">模号：</span>
                                            <div class="col-sm-8">
                                                <asp:TextBox ID="txtMoHao" class="form-control" runat="server"  ReadOnly="True"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <span class="col-sm-4">库位：</span>
                                            <div class="col-sm-8">
                                                <asp:TextBox ID="txtKuWei" class="form-control" runat="server"  ReadOnly="True"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <span class="col-sm-4">模具状态：</span>
                                            <div class="col-sm-8">
                                                <asp:DropDownList ID="dropZhuangTai" class="form-control input-s-sm" 
                                                    runat="server" BackColor="Yellow">
                                                    <asp:ListItem Value="正常">正常</asp:ListItem>
                                                    <asp:ListItem Value="异常">异常</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <span class="col-sm-4" style="padding-right:2px">模具状况说明：</span>
                                            <div class="col-sm-8">
                                                <asp:TextBox ID="txtShuoMing" class="form-control" runat="server" 
                                                    BackColor="Yellow"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-6  col-md-5" id="divShangMo"  runat =server >
                                <div class="panel panel-info">
                                    <div class="panel-heading">
                                        <strong>上模（装）</strong>
                                    </div>
                                    <div class="panel-body">
                                        <div class="form-group">
                                            <span class="col-sm-4">模具号：</span>
                                            <div class="col-sm-8">
                                                <asp:TextBox ID="txtMoJuHaoS" class="form-control" runat="server" 
                                                    AutoPostBack="True" ontextchanged="txtMoJuHaoS_TextChanged" 
                                                    BackColor="Yellow"></asp:TextBox>
                                            </div>
                                        </div>

                                          <div class="form-group">
                                            <span class="col-sm-4">模具类型：</span>
                                            <div class="col-sm-8">
                                                <asp:TextBox ID="txtmojutype_up" class="form-control" runat="server" 
                                                    ReadOnly="True"></asp:TextBox>
                                            </div>
                                        </div>



                                        <div class="form-group">
                                            <span class="col-sm-4">零件名称：</span>
                                            <div class="col-sm-8">
                                                <asp:TextBox ID="txtLingJianMingS" class="form-control" runat="server" ReadOnly="True" ></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <span class="col-sm-4">模号：</span>
                                            <div class="col-sm-8">
                                                <asp:TextBox ID="txtMoHaoS" class="form-control" runat="server"  ReadOnly="True" ></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <span class="col-sm-4">库位：</span>
                                            <div class="col-sm-8">
                                                <asp:TextBox ID="txtKuWeiS" class="form-control" runat="server"  ReadOnly="True" ></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <span class="col-sm-4" style="padding-right:2px">上模后是否开始生产：</span>
                                            <div class="col-sm-8">
                                                <asp:DropDownList ID="dropShengChang" class="form-control input-s-sm" 
                                                    runat="server" BackColor="Yellow">
                                                    <asp:ListItem Value="是">是</asp:ListItem>
                                                    <asp:ListItem Value="否">否</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <span class="col-sm-5"></span>
                                            <div class="col-sm-7">
                                                &nbsp;
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-12 col-md-10">
                   
                                    <div class="row row-container">
                                        <div class="col-sm-12">
                               


                                                 <div id="Div3" runat="server" class="col-sm-4 " style="padding: 0;
                                                    position: relative; float: left; top: 0px; left: 0px; width: 200px;
                                                    height: 70px;">
                                                    <asp:Button ID="btn_Start" runat="server" Font-Size="X-Large"
                                                        class="btn btn-primary" Style="position: absolute; left: -1;
                                                        right: 1px; width: 200px; top: -1px; height: 70px;" Text="开始换模"  onclick="btn_Start_Click"
                                                         />
                                                    <div id="div4" runat="server">
                                                        <asp:Label ID="lblStart_time" runat="server" 
                                                            Style="position: absolute;
                                                            right: -15px; left: 35px; width: 160px; top: 45px; height: 20px;"></asp:Label>
                                                    </div>
                                                </div>

                                                <div ></div>

                              

                                
                                                 <div id="Div1" runat="server" class="col-sm-4 " style="padding: 0;
                                                    position: relative; float: left; top: 0px; left: 0px; width: 200px;
                                                    height: 70px;">
                                                    <asp:Button ID="btn_End" runat="server" Font-Size="X-Large"
                                                        class="btn btn-primary" Style="position: absolute; left: -1;
                                                        right: 1px; width: 200px; top: -1px; height: 70px;" Text="结束换模"  onclick="btn_End_Click"
                                                         />
                                                    <div id="div2" runat="server">
                                                        <asp:Label ID="labendtime" runat="server" 
                                                            Style="position: absolute;
                                                            right: -15px; left: 35px; width: 160px; top: 45px; height: 20px;"></asp:Label>
                                                    </div>
                                                </div>


                                                  <div id="Div7" runat="server" class="col-sm-4  col-md-offset-2" style="padding: 0;
                                                    position: relative; float: left; top: 0px; left: 0px; width: 200px;
                                                    height: 70px; display:none">
                                                    <asp:Button ID="btn_Return" runat="server" Font-Size="X-Large"
                                                        class="btn btn-primary" Style="width: 200px; top: -1px; height: 70px;" 
                                                        Text="返回" onclick="btnReturn_Click"
                                                      />
                                       
                                                </div>

                            

                            


                                        </div>
                                    </div>
                          </div>
                            </div>
                        </div>
                    </div>
            </ContentTemplate>
    </asp:UpdatePanel>

    <iframe id="tt" name=foot marginwidth=0 marginheight=0 src="/YaSheTou/YST_Record.aspx" frameborder=0 scrolling="no" width="85%" height="800"></iframe>
</asp:Content>