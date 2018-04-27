<%@ Page Title="MES【投料记录】" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    ViewStateMode="Enabled" MaintainScrollPositionOnPostback="true" CodeFile="InputMat.aspx.cs"
    Inherits="InputMat" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
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
    </style>
    <script src="Content/js/jquery-ui-1.10.4.min.js" type="text/javascript"></script>
    <script src="../Content/js/jquery.min.js" type="text/javascript"></script>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <script type="text/javascript">
        $(document).ready(function () {

            $("select[id*='selHeJin']").change(function () {

                $("#hejin").text($("select[id*='selHeJin']").val());
            })

            $("#mestitle").text("【投料记录】");
            //初始化不可输入投料重量
            $("input[id*='txt_maozhong_1']").attr("readonly", "true");
            $("input[id*='txt_pizhong_1']").attr("readonly", "true");
            $("input[id*='txt_touliao_1']").attr("readonly", "true");
            show();
            setInterval(show, 2000);
            function show() {
                var url = "http://localhost:8080/fnChengZhong.aspx?flag=chengzhong";

                $.ajax({
                    url: url,
                    type: 'GET',
                    dataType: 'JSONP', //here
                    jsonp: "callback", //传递给请求处理程序或页面的，用以获得jsonp回调函数名的参数名(一般默认为:callback)
                    success: function (json) {
                        setWeight(json.value);
                    },
                    error: function (XMLHttpRequest, textStatus, errorThrown) {
                        //alert(textStatus);
                        // $("input[id*='txt_maozhong_1']").removeClass("disabled").removeAttr("readonly").removeAttr("disabled");
                        // $("input[id*='txt_pizhong_1']").removeClass("disabled").removeAttr("readonly").removeAttr("disabled");

                    },
                    jsonpCallback: "weightHandler" //自定义的jsonp回调函数名称，默认为jQuery自动生成的随机函数名，也可以写"?"，jQuery会自动为你处理数据

                });

            }

            function setWeight(result) {
                var mzclass = $("input[id*='txt_maozhong_1']").attr("class");
                if ($("input[id*='txt_maozhong_1']").attr("disabled") != "disabled") {
                    $("input[id*='txt_maozhong_1']").val(result);
                    $("input[id*='txt_maozhong_1']").addClass("disabled").attr("readonly", "true");
                }

                if ($("input[id*='txt_pizhong_1']").attr("disabled") != "disabled") {
                    $("input[id*='txt_pizhong_1']").val(result);
                    $("input[id*='txt_pizhong_1']").addClass("disabled").attr("readonly", "true");
                }

                if ($("input[id*='txt_maozhong_1']").val() != "" && $("input[id*='txt_pizhong_1']").val() != "") {
                    var mz = $("input[id*='txt_maozhong_1']").val();
                    var pz = $("input[id*='txt_pizhong_1']").val();
                    $("input[id*='txt_touliao_1']").val((mz - pz).toFixed(1))
                }

            }


        })//endready

        
        $('input[id=txt_pizhong_1]').change(function () {
            var text = $(this).val();
            $("input[name='txt_touliao_1']").val(text);
        });       

    </script>
    <div class="row" style="margin: 0px 2px 1px 2px">
        <div class="col-sm-12 ">
            <div class="panel panel-info">
                <div class="panel-heading">
                    <strong>基本信息</strong>
                </div>
                <div class="panel-body">
                    <div class="col-sm-10">
                        <table style="width: 100%">
                            <tr>
                                <td>
                                    日期：
                                </td>
                                <td>
                                    <input id="txtRiQi" class="form-control input-s-sm" runat="server" />
                                </td>
                                <td>
                                    时间：
                                </td>
                                <td>
                                    <input id="txtShiJian" class="form-control input-s-sm" runat="server" />
                                </td>
                                <td>
                                    班别：
                                </td>
                                <td>
                                    <input id="txtBanBie" class="form-control input-s-sm" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    工号：
                                </td>
                                <td>
                                    <asp:DropDownList ID="dropGongHao" class="form-control input-s-sm" runat="server"
                                        AutoPostBack="True" OnSelectedIndexChanged="dropGongHao_SelectedIndexChanged" />
                                </td>
                                <td>
                                    姓名：
                                </td>
                                <td>
                                    <input id="txtXingMing" class="form-control input-s-sm" runat="server" />
                                </td>
                                <td>
                                    班组：
                                </td>
                                <td>
                                    <input id="txtBanZu" class="form-control input-s-sm" runat="server" />
                                </td>
                            </tr>
                            <tr hidden="hidden">
                                <td>
                                    设备号：
                                </td>
                                <td>
                                    <input id="txtSheBeiHao" class="form-control input-s-sm" runat="server" />
                                </td>
                                <td>
                                    设备简称：
                                </td>
                                <td>
                                    <input id="txtSheBeiJianCheng" class="form-control input-s-sm" runat="server" />
                                </td>
                                <td>
                                    设备规格：
                                </td>
                                <td>
                                    <input id="txtSheBeiGuiGe" class="form-control input-s-sm" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                               <td>
                                    <input id="txttype" class="form-control input-s-sm" runat="server" visible=false  />
                                </td>
                                <td>
                                    批号：
                                </td>
                                <td>
                                    <input id="txtPiHao" class="form-control input-s-sm" runat="server" />
                                </td>
                                <td>
                                    合金：
                                </td>
                                <td>
                                 

                                    <asp:DropDownList ID=ddlhejin runat =server class="form-control input-s-sm" 
                                        onselectedindexchanged="ddlhejin_SelectedIndexChanged"  AutoPostBack="True" >
                                   

                                    </asp:DropDownList>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="col-sm-2" style="float: left">
                        <div class="area area_border_gray" style="width: 100%; height: 100%">
                            <label class="control-label">
                                合金种类</label>
                            <div style="width: 100%; height: 100%">
                               
                                    <asp:Label ID="hejing" runat =server style="font-size: xx-large" ></asp:Label>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="row" style="margin: 0px 2px 1px 2px">
        <div class="col-sm-12">
            <div class="panel panel-info">
                <%--<div class="panel-heading">
                    <strong>投料1</strong>
                </div>--%>
                <div class="panel-body">
                    <div class="row">
                        <div class="col-sm-12">
                            <div class="panel panel-info">
                                <div class="panel-heading">
                                    <strong>第一步：选择设备</strong>
                                </div>
                                <div class="panel-body">
                                    <div class="area_block">
                                        <div class="btn btn-large btn-success">
                                            <div class="btn-group">
                                                <asp:Button ID="A" runat="server" Text="熔炼炉A(0.6T)" class="btn btn-primary" OnClick="A_Click">
                                                </asp:Button>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="area_block">
                                        <div class="btn btn-large btn-success">
                                            <div class="btn-group">
                                                <asp:Button ID="B" runat="server" Text="熔炼炉B(1.2T)" class="btn btn-primary" OnClick="B_Click">
                                                </asp:Button>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="area_block">
                                        <div class="btn btn-large btn-success">
                                            <div class="btn-group">
                                                <asp:Button ID="C" runat="server" Text="熔炼炉C(2.5T)" class="btn btn-primary" OnClick="C_Click">
                                                </asp:Button>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="area_block">
                                        <div class="btn btn-large btn-success">
                                            <div class="btn-group">
                                                <asp:Button ID="D" runat="server" Text="熔炼炉D(1.5T)" class="btn btn-primary" OnClick="D_Click">
                                                </asp:Button>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="area_block">
                                        <div class="btn btn-large btn-success">
                                            <div class="btn-group">
                                                <asp:Button ID="E" runat="server" Text="熔炼炉E(0.5T)" class="btn btn-primary" OnClick="E_Click">
                                                </asp:Button>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-12">
                            <div class="panel panel-info">
                                <div class="panel-heading">
                                    <strong>第二步:选择投料类别</strong>
                                </div>
                                <div class="panel-body">
                                    <div class="col-sm-12">
                                        <div class="area_block"  >
                                            <div class="btn-padding-s btn-success " >
                                                <div class="area_x_lg">
                                                    <asp:Button ID="btn_jialiao1" runat="server" Text="铝锭加料" class="btn btn-large btn-primary"
                                                        Style="width: 100%" OnClick="btn_jialiao1_Click" />
                                                    
                                                    <div style="padding-left:8px">
                                                    已加:<label id="lbl_0Zhong" class="font-10" runat="server" style="width: 50px;text-align:right">600</label>kg<br />
                                                    占比:<label id="lbl_0BiLi" class="font-10" runat="server" style="width: 50px;text-align:right"></label>%<br />
                                                    要求: ≥ 45%</div>
                                                    
                                                </div>
                                            </div>
                                        </div>
                                        <div class="area_block">
                                            <div class="btn-padding-s btn-success" id=div1 runat =server>
                                                <div class="area_x_lg">
                                                    <asp:Button ID="btn_jialiao2" runat="server" Text="一级回炉料加料" class="btn btn-large btn-primary"
                                                        Style="padding-left: 0px; padding-right: 0px; width: 100%" OnClick="btn_jialiao2_Click" />
                                                    <div style="padding-left:8px">
                                                    已加:<label id="lbl_1Zhong" class="font-10" runat="server" style="width: 50px;text-align:right">350</label>kg<br />
                                                    占比:<label id="lbl_1BiLi" class="font-10" runat="server" style="width: 50px;text-align:right"></label>%<br />
                                                    要求: ≤ 35%
                                                   </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="area_block">
                                            <div class="btn-padding-s btn-success" id=div2 runat =server>
                                                <div class="area_x_lg "  >
                                                    <asp:Button ID="btn_jialiao3" runat="server" Text="二级回炉料加料" class="btn btn-large btn-primary"
                                                        Style="padding-left: 0px; padding-right: 0px; width: 100%" OnClick="btn_jialiao3_Click" />
                                                    <div style="padding-left:8px">
                                                    已加:<label id="lbl_2Zhong" class="font-10" runat="server" style="width: 50px;text-align:right">50</label>kg<br />
                                                    占比:<label id="lbl_2BiLi" class="font-10" runat="server" style="width: 50px;text-align:right"></label>%<br />
                                                    要求: ≤ 20%
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="area_block"  runat =server >
                                            <div class="btn-padding-s btn-success" id=div3 runat =server>
                                                <div class="area_x_lg">
                                                    <asp:Button ID="btn_jialiao4" runat="server" Text="三级回炉料加料" class="btn btn-large btn-primary"
                                                        Style="padding-left: 0px; padding-right: 0px; width: 100%" OnClick="btn_jialiao4_Click" />
                                                    <div style="padding-left:8px">
                                                    已加:<label id="lbl_3Zhong" class="font-10" runat="server" style="width: 50px;text-align:right">50</label>kg<br />
                                                    占比:<label id="lbl_3BiLi" 
                                                            class="font-10" runat="server"  style="width: 50px;text-align:right"></label>%<br />
                                                    要求: ≤ 5%
                                                    </div>
                                                </div>
                                            </div>

                                        </div>

                                          <div class="area_block"  ><label  class="font-10" >1.加料优先级应依次为铝锭，一级回炉料，二级回炉料，三级回炉料。</br>2.显示绿色颜色材料优先加料，黄色次之，灰色不能加料。</br>3.一级回炉料<30%为绿色，30%-35%为黄色，>35为灰色</br>4.二级回炉料<15%为绿色，15%-20%为黄色，>20%为灰色</br></label></div>
                                        
                                       

                                    </div>

                                  


                                    <div class="col-sm-12">
                                        <label style="float: left">
                                            铝锭的原材料批号:</label>
                                        <div style="float: left">
                                            <asp:TextBox ID="txtmaterial_lot" class="form-control input-s-sm" runat="server"></asp:TextBox></div>
                                        <div style="float: left">
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-3 disabled">
                                        <div class="panel panel-info">
                                            <div class="panel-heading">
                                                <strong>第三步：确认毛重</strong>
                                            </div>
                                            <div class="panel-body">
                                                <div class="form-group">
                                                    <div class="input-group">
                                                        <asp:TextBox ID="txt_maozhong_1" class="input form-control" runat="server"  ></asp:TextBox>
                                                        <div class="input-group-btn">
                                                            <asp:Button ID="btnMaoZhong_confirm_1" class="btn btn btn-primary" Text="确认毛重" runat="server"
                                                                OnClick="btnMaoZhong_confirm_1_Click" />
                                                        </div>
                                                    </div>
                                                    <asp:Label ID="lblMaoZhong_confirm_time" runat="server" Text=""></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-3">
                                        <div class="panel panel-info">
                                            <div class="panel-heading">
                                                <strong>第四步：确认皮重</strong>
                                            </div>
                                            <div class="panel-body">
                                                <div class="form-group">
                                                    <div class="input-group">
                                                        <asp:TextBox ID="txt_pizhong_1" class="input form-control" runat="server"   OnTextChanged="txt_pizhong_1_TextChanged"></asp:TextBox>
                                                        <div class="input-group-btn">
                                                            <asp:Button ID="btnPiZhong_confirm_1" class="btn btn btn-primary" Text="确认皮重" runat="server"
                                                                OnClick="btnPiZhong_confirm_1_Click" />
                                                        </div>
                                                    </div>
                                                    <asp:Label ID="lblPiZhong_confirm_time" runat="server" Text=""></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-3">
                                        <div class="panel panel-info">
                                            <div class="panel-heading">
                                                <strong>第五步：确认净重</strong>
                                            </div>
                                            <div class="panel-body">
                                                <div class="form-group">
                                                    <div class="input-group">
                                                        <asp:TextBox ID="txt_touliao_1" class="input form-control" runat="server"></asp:TextBox>
                                                        <div class="input-group-btn">
                                                            <asp:Button ID="btnTouLiao_confirm_1" class="btn btn btn-primary" Text="确认净重" runat="server"
                                                                OnClick="btnTouLiao_confirm_1_Click" />
                                                        </div>
                                                    </div>
                                                    <asp:Label ID="lblTouLiao_confirm_time" runat="server" Text=""></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-3">
                                        <div class="panel panel-info">
                                            <div class="panel">
                                                <strong></strong>
                                            </div>
                                            <div class="panel-body">
                                                <div class="form-group">
                                                    <div class="input-group">
                                                        <div class="input-group-btn" style="text-align: center">
                                                            <asp:Button ID="btn_Return" class="btn btn btn-primary" style="height:55px ;width=80px" text="返回" runat="server"
                                                                OnClick="btnReturn_Click" Width="120px" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
