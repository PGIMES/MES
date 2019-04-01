<%@ Page Title="MES【精炼测氢】" Language="C#" MasterPageFile="~/Site.master"
    AutoEventWireup="true" CodeFile="JinLian_Hydrogen.aspx.cs"
    Inherits="JingLian_JinLian_Hydrogen" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent"
    runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent"
    runat="Server">
    <script src="../Content/js/jquery.min.js" type="text/javascript"></script>
    <script src="../Content/js/plugins/layer/layer.min.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript">
        $(document).ready(function () {


            if ($("input[id*='txt_temperature']").val() == "50") {
                $("#MainContent_lb_ms").text("ρ=0.98804");
            }

            $("input[id*='txt_temperature']").change(function () {
                var temperature = $("input[id*='txt_temperature']").val();
                GetMDValue(temperature);


            });

            setInterval(show, 3000);
            function show() {
                var url = "http://localhost:8080/fnChengZhong.aspx?flag=chengzhong";
                // var url = "http://172.16.7.81/fnChengZhong.aspx?flag=chengzhong";
                $.ajax({
                    url: url,
                    type: 'GET',
                    dataType: 'JSONP', //here
                    jsonp: "callback", //传递给请求处理程序或页面的，用以获得jsonp回调函数名的参数名(一般默认为:callback)
                    success: function (json) {
                        setWeight(json.value);
                        $("input[id*='txt_mz']").addClass("disabled").attr("readonly", "true");

                    },
                    error: function () {
                        $("input[id*='txt_mz']").removeClass("disabled").removeAttr("readonly");
                        //alert('fail');  
                    },
                    jsonpCallback: "weightHandler" //自定义的jsonp回调函数名称，默认为jQuery自动生成的随机函数名，也可以写"?"，jQuery会自动为你处理数据

                });
            }



        });           //endready




        function setWeight(result) {
                    if (result != "") {
                        $("input[id*='txt_mz']").val(result);
                    }
                    if ($("input[id*='txt_mz']").val() != "" && $("input[id*='txt_pz']").val() != "") {
                        $("input[id*='txt_jz']").val($("input[id*='txt_mz']").val() - $("input[id*='txt_pz']").val())
                    }

        }
        function GetResult() {
            var mdvalue = "";
            if ($("input[id*='txt_kq']").val() == "" || $("input[id*='txt_water']").val() == "") {
                alert("请分别填写空气中重量和水中重量");
            }
            else {
                //mdvalue = Number(($("input[id*='txt_kq']").val() * $("#MainContent_lb_ms").text().replace("ρ=", "")) / ($("input[id*='txt_kq']").val() - $("input[id*='txt_water']").val())).toFixed(2);
				mdvalue = Number(($("input[id*='txt_kq']").val() * $("#MainContent_lb_ms").text().replace("ρ=", "")) / ($("input[id*='txt_kq']").val() - $("input[id*='txt_water']").val())).toFixed(3);
                if (mdvalue >= 2.65) {
                    $("input[id*='MainContent_txt_midu']").val("ρ=" + mdvalue + "" + "  " + "OK");
                    $("input[id*='MainContent_txt_midu']").css("background", "Green");
                }
                else {
                    $("input[id*='MainContent_txt_midu']").val("ρ=" + mdvalue + "" + "  " + "NG");
                    $("input[id*='MainContent_txt_midu']").css("background", "Red");
                }
            }
           
        }

        //猎取密度

        function GetMDValue( temperature) {
            $.ajax({
                type: "post",
                url: "JinLian_Hydrogen.aspx/GetMD",
                data: "{'temperature':'" + temperature + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false, //默认是true，异步；false为同步，此方法执行完在执行下面代码
                success: function (data) {
                    var obj = eval(data.d);
                    if (obj[0].temperature != "") {
                        // objs.val(" ρ=" + obj[0].temperature);
                        //alert(obj[0].temperature);
                        $("#MainContent_lb_ms").text(" ρ=" + obj[0].temperature);
                    }
                    else {
                        alert("请填写正确的水温!");
                        // objs.val("");
                        $("#MainContent_lb_ms").text("");
                        $("#MainContent_lb_ms").focus();
                        // objs.focus();
                    }
                }

            });
        }


        $("#mestitle").text("【精炼测氢】");
        var dtime = "";
        function show_cur_times() {
            //获取当前日期
            
            var date_time = new Date(dtime);
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
            dtime = add(new Date(date_str), 's', 1);
            $("span[id*='lb_end']").text(date_str);
           // alert(seconds)

        }
        function add(date_time,part, value) {
            value *= 1;  
            if (isNaN(value)) {  
                value = 0;  
            }  
            switch (part) {  
                case "y":  
                    return date_time.setFullYear(date_time.getFullYear() + value);
                    break;  
                case "m":  
                    return date_time.setMonth(date_time.getMonth() + value);
                    break;  
                case "d":  
                    return date_time.setDate(date_time.getDate() + value);
                    break;  
                case "h":  
                    return date_time.setHours(date_time.getHours() + value);
                    break;  
                case "m":  
                    return date_time.setMinutes(date_time.getMinutes() + value);
                    break;  
                case "s":  
                    return date_time.setSeconds(date_time.getSeconds() + value);
                    break;  
                default:  
   
            }  
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
                    <div class="col-sm-8">
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
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
                                </table>
                                </div></ContentTemplate>
                        </asp:UpdatePanel>
                        <div class="col-sm-4">
                            <div class="area area_border_gray" >
                                <div class="">
                                    <asp:Label ID="lb_jlno" runat="server" Font-Size="xx-large" class="control-label"></asp:Label>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="row row-container">
            <div class="col-sm-8">
                <div class="panel panel-info">
                    <div class="panel-heading">
                        <strong>精炼</strong>
                    </div>
                    <div class="panel-body">
                        <div class="col-sm-3">
                            转运包序列号：</div>
                        <div class="col-sm-3">
                            <asp:DropDownList ID="txt_zybno" runat="server" 
                                class="form-control input-small" AutoPostBack="True" onselectedindexchanged="txt_zybno_SelectedIndexChanged"
                               >
                            </asp:DropDownList>
                        </div>
                          <div class="col-sm-3">
                                        转运包号：</div>
                                    <div class="col-sm-3">
                                     <asp:DropDownList ID="txt_zyno" runat="server"  BackColor="Yellow"
                                            class="form-control input-small" AutoPostBack="True" 
                                            onselectedindexchanged="txt_zyno_SelectedIndexChanged">
                                    </asp:DropDownList></div>
                        <div class="col-sm-3">
                            熔炼<br />
                            炉号：</div>
                        <div class="col-sm-3">
                            <asp:DropDownList ID="txt_luhao" runat="server" 
                                class="form-control input-small" 
                                
                                onselectedindexchanged="txt_luhao_SelectedIndexChanged" 
                                AutoPostBack="True">
                                <asp:ListItem></asp:ListItem>
                                <asp:ListItem>A</asp:ListItem>
                                <asp:ListItem>B</asp:ListItem>
                                <asp:ListItem>C</asp:ListItem>
                                <asp:ListItem>D</asp:ListItem>
                                <asp:ListItem>E</asp:ListItem>
                                <asp:ListItem>F</asp:ListItem>
                                <asp:ListItem>X</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="col-sm-3">
                            合金：</div>
                        <div class="col-sm-3">
                            <asp:DropDownList ID="txt_hejin" runat="server" class="form-control input-small">
                                <asp:ListItem></asp:ListItem>
                                <asp:ListItem>A380</asp:ListItem>
                                <asp:ListItem>EN46000</asp:ListItem>
                                <asp:ListItem>ADC12</asp:ListItem>
                                <asp:ListItem>EN47100</asp:ListItem>
                                
                            </asp:DropDownList>
                        </div>
                        <div class="row">
                            <div class="col-sm-6">
                            </div>
                            <div class="col-sm-6">
                            </div>
                        </div>
                        <div class="col-sm-6">
                            <div class="panel panel-info">
                                <div class="panel-heading">
                                    <strong>测温</strong>
                                </div>
                                <div class="panel-body">
                                    <div>
                                        <asp:UpdatePanel runat="server" ID="UpdatePanelJL">
                                            <ContentTemplate>
                                                <div class="col-sm-5">
                                                    出料时炉内铝液温度(℃)：</div>
                                                <div class="col-sm-6">
                                                    <asp:TextBox ID="txt_before_wd" class="form-control input-small"
                                                        runat="server" ReadOnly="True"></asp:TextBox></div>
                                                <div class="col-sm-12" style="color: #FF0000">
                                                    *出料时炉内铝液温度要求730至780</div>
                                                   
                                                <div class="col-sm-12  col-md-offset-5">
                                                    <div id="Div8" runat="server" style="padding: 0; position: relative;
                                                        float: left; top: 0px; left: 0px; width: 200px; height: 70px;">
                                                        <asp:Button ID="btn_bf_time" runat="server" Font-Size="Medium"
                                                            class="btn btn-primary" Style="position: absolute; left: -4;
                                                            right: -15px; width: 200px; top: 0px; height: 70px;" Text="出料时炉内铝液温度确认"
                                                            OnClick="btn_bf_time_Click" /> 
                                                        <div id="div9" runat="server">
                                                            <asp:Label ID="lb_bf_time" runat="server" Style="position: absolute; 
                                                                right: 55px; left: 20px; width: 160px; top: 45px" ></asp:Label>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-sm-12" style=" padding-top:15px"></div>
                                                <div class="col-sm-12 col-md-offset-5"> <div  runat="server" style="padding: 0; position: relative;
                                                        float: left; top: 0px; left: 0px; width: 200px; height: 70px;">
                                                        <asp:Button ID="btn_gpcs" runat="server" Font-Size="Large"
                                                            class="btn btn-primary" Style="position: absolute; left: -4;
                                                            right: -15px; width: 200px; top: 0px; height: 70px;" 
                                                            Text="本包需做光谱样件" onclick="btn_gpcs_Click"
                                                           /> <input  id="gp_flag" type="text"  runat="server" style=" display:none"  />
                                                        <div  runat="server">
                                                            <asp:Label ID="Label1" runat="server"  Style="position: absolute;  
                                                                 left: 70px; width: 50px; top: 45px" Text="1次/4包"></asp:Label>
                                                        </div>
                                                    </div> </div>
                                                
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:PostBackTrigger ControlID="btn_bf_time" />
                                                <asp:PostBackTrigger ControlID="btn_gpcs" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                                <div class="panel-body">
                                    <div class="col-sm-5">
                                        精炼后温度(℃)：</div>
                                    <div class="col-sm-6">
                                        <asp:TextBox ID="txt_after_wd" class="form-control input-small"
                                            runat="server" ReadOnly="True"></asp:TextBox></div>
                                    <div class="col-sm-12" style="color: #FF0000">
                                        *精炼后温度要求680至730<br />*精炼后必须除渣，方可做测氢样件<br />*石墨转轴残铝必须清理干净</div>
                                    <div class="col-sm-12 col-md-offset-5">
                                        <div id="Div10" runat="server" style="padding: 0; position: relative;
                                            float: left; top: 0px; left: 0px; width: 200px; height: 70px;">
                                            <asp:Button ID="btn_af_time" runat="server" Font-Size="Large"
                                                class="btn btn-primary" Style="position: absolute; left: -4;
                                                right: -15px; width: 200px; top: -1px; height: 70px;" Text="精炼后温度确认"
                                                OnClick="btn_af_time_Click" />
                                            <div id="div11" runat="server">
                                                <asp:Label ID="lb_af_time" runat="server" Style="position: absolute;
                                                    right: 55px; left: 20px; width: 160px; top: 45px"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-6">
                            <div class="panel panel-info">
                                <div class="panel-heading">
                                    <strong>称重</strong>
                                </div>
                                <div class="panel-body">
                               
                                  
                                    <div class="col-sm-6">
                                        转运包皮重：</div>
                                    <div class="col-sm-6">
                                        <asp:TextBox ID="txt_pz" class="form-control input-small" runat="server"
                                            ReadOnly="True"></asp:TextBox></div>
                                          
                                    <div class="col-sm-6">
                                        毛重(kg)：</div>
                                    <div class="col-sm-6">
                                       <input id="txt_mz" type="text" runat="server"  class="form-control input-small disabled"
                                            readonly="readonly" /><asp:Label ID="lb_mz" runat="server" Text="lb_mz" Visible="false"></asp:Label></div>
                                    <div class="col-sm-6">
                                        净重(kg)：</div>
                                    <div class="col-sm-6">
                                        <input id="txt_jz" type="text" runat="server" 
                                            class="form-control input-small disabled"  readonly="readonly"/><asp:Label ID="lb_jz" runat="server" Text="lb_jz" Visible="false"></asp:Label></div>
                                     
                                    <div class="col-sm-12">
                                        <div id="Div12" runat="server" style="padding: 0; position: relative;
                                            float: left; top: 0px; left: 0px; width: 170px; height: 70px;">
                                         
                                            <asp:Button ID="btn_zl_confirm" runat="server" Font-Size="Large"
                                                class="btn btn-primary" Style="position: absolute; left: -1;
                                                right: -18px; width: 170px; top: -1px; height: 70px;" 
                                                Text="重量确认" onclick="btn_zl_confirm_Click" />
                                            <div id="div13" runat="server">
                                                <asp:Label ID="Label3" runat="server" Style="position: absolute;
                                                    right: 55px; left: 20px; width: 160px; top: 45px"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                                       <div class="row  row-container">
                            <div class="col-sm-12">
                                <div class="panel-body">
                                    <div id="Div1" runat="server" class="col-sm-4" style="padding: 0;
                                        position: relative; float: left; top: 0px; left: 0px; width: 170px;
                                        height: 70px;">
                                        <asp:Button ID="btn_begin" runat="server" Font-Size="X-Large"
                                            class="btn btn-primary" Style="position: absolute; left: -1;
                                            right: -18px; width: 170px; top: -1px; height: 70px;" Text="开始精炼"
                                            OnClick="btn_begin_Click" />
                                        <div id="div2" runat="server">
                                            <asp:Label ID="lb_start" runat="server" 
                                                Style="position: absolute;
                                                right: -15px; left: 20px; width: 150px; top: 45px; height: 20px;"></asp:Label>
                                        </div>
                                    </div>
                                    <div id="Div3" runat="server" class="col-sm-4" style="padding: 0;
                                        position: relative; float: left; top: 0px; left: 0px; width: 170px;
                                        height: 70px;">
                                        <asp:Button ID="btn_again" runat="server" Font-Size="X-Large"
                                            class="btn btn-primary" Style="position: absolute; left: -1;
                                            right: -18px; width: 170px; top: -1px; height: 70px;" 
                                            Text="再次精炼" onclick="btn_again_Click" />
                                        <div id="div4" runat="server">
                                            <asp:Label ID="lb_again" runat="server" Style="position: absolute;
                                                right: 55px; left: 20px; width: 150px; top: 45px"></asp:Label>
                                        </div>
                                    </div>
                                    <div id="Div5" runat="server" class="col-sm-4" style="padding: 0;
                                        position: relative; float: left; top: 0px; left: 0px; width: 170px;
                                        height: 70px;">
                                        <asp:Button ID="btn_end" runat="server" Font-Size="X-Large" class="btn btn-primary"
                                            Style="position: absolute; left: -1; right: -18px; width: 170px;
                                            top: -1px; height: 70px;" Text="结束精炼" 
                                            OnClick="btn_end_Click" />
                                             <asp:Button ID="btnNext" runat="server" Text="Next" 
                onclick="btnNext_Click"   style=" display:none"  />
                                        <div id="div6" runat="server">
                                            <asp:Label ID="lb_end" runat="server" Style="position: absolute;
                                                right: 0px; left: 20px; width: 150px; top: 45px"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        </div>
             
                    </div>
                </div>

                      <div >
        <asp:Chart ID="C3" runat="server" BackColor="#F3DFC1" BackGradientStyle="TopBottom"
            BorderColor="181, 64, 1" BorderDashStyle="Solid" BorderWidth="2" Height="200px"
            ImageLocation="~/TempImages/ChartPic_#SEQ(300,3)" ImageType="Png" Palette="none"
            Width="1500px">
              <Titles>
                  <asp:Title Alignment="MiddleLeft" Name="Title1" Text="密度预控图" Font="微软雅黑, 10pt">
                  </asp:Title>
              </Titles>
            <Legends>
                <asp:Legend BackColor="Transparent" Font="Trebuchet MS, 8.25pt, style=Bold" IsTextAutoFit="False"
                    MaximumAutoSize="20" Name="Default" TitleAlignment="Center">
                </asp:Legend>
            </Legends>
            <%-- <Titles>
                        <asp:Title Font="微软雅黑, 10pt" Name="TitleMonth" Text="">
                        </asp:Title>
                    </Titles>--%>
            <BorderSkin SkinStyle="Emboss" />
            <Series>
                <asp:Series BorderWidth="3" ChartArea="ChartArea1" 
                    ChartType="Line" LabelBorderWidth="9"
                    Legend="Default" LegendText="密度" MarkerSize="8" 
                    MarkerStyle="Circle" Name="密度"
                    ShadowOffset="3" XValueMember="Days" 
                    YValueMembers="weight">
                </asp:Series>
            </Series>
            <ChartAreas>
                <asp:ChartArea BackColor="64, 165, 191, 228" BackGradientStyle="TopBottom" BackSecondaryColor="White"
                    BorderColor="64, 64, 64, 64" BorderDashStyle="Solid" Name="ChartArea1" ShadowColor="Transparent">
                    <Area3DStyle Inclination="15" IsClustered="False" IsRightAngleAxes="False" Perspective="10"
                        Rotation="10" WallWidth="0" />
                    <AxisY LineColor="64, 64, 64, 64" TitleAlignment="Far">
                        <LabelStyle Font="Trebuchet MS, 8.25pt, style=Bold" />
                        <MajorGrid LineColor="64, 64, 64, 64" />
                       <StripLines>
                               
                                   <asp:StripLine BorderColor="Red" IntervalOffset="2.65" 
                                    IntervalOffsetType="Number" IntervalType="Number" Text="2.65" 
                                    ToolTip="2.65" />
                                <asp:StripLine BorderColor="Yellow" IntervalOffset="2.68" 
                                    IntervalOffsetType="Number" IntervalType="Number" Text="2.68" 
                                    ToolTip="2.68" />
                              
                               
                            </StripLines>
                    </AxisY>
                    <AxisX Interval="1" LineColor="64, 64, 64, 64">
                        <LabelStyle Font="Trebuchet MS, 8.25pt, style=Bold" />
                        <MajorGrid LineColor="64, 64, 64, 64" />
                    </AxisX>
                </asp:ChartArea>
            </ChartAreas>
        </asp:Chart>
     
    </div>
            </div>
            <div class="col-sm-4">
             <asp:UpdatePanel runat="server" ID="UpdatePanel1" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                 <script type="text/jscript">
                                                     var prm = Sys.WebForms.PageRequestManager.getInstance();
                                                     prm.add_endRequest(function () {
                                                         $("input[id*='txt_temperature']").change(function () {
                                                             var temperature = $("input[id*='txt_temperature']").val();
                                                             GetMDValue(temperature);
                                                             GetResult();
                                                         });

                                                         $("input[id*='txt_kq']").change(function () {
                                                             if ($("input[id*='txt_temperature']").val() != "" && $("input[id*='txt_kq']").val() != "") {
                                                                 var temperature = $("input[id*='txt_temperature']").val();
                                                                 GetMDValue(temperature);
                                                                 GetResult();
                                                             }
                                                         });

                                                     });
                                        </script>

                <div class="panel panel-info">
                    <div class="panel-heading">
                        <strong>测氢</strong>
                    </div>
                    <div class="panel-body">
                        <div class="col-sm-6">
                            转运包序列号：</div>
                        <div class="col-sm-6">
                            <asp:DropDownList ID="txt_zybselect" runat="server" class="form-control input-small">
                            </asp:DropDownList>
                        </div>
                        <div class="col-sm-6">
                            空气中重量(g)：</div>
                        <div class="col-sm-6">
                            <asp:TextBox ID="txt_kq" class="form-control input-small" runat="server"></asp:TextBox></div>
                        <div class="col-sm-6">
                            水中重量(g)：</div>
                        <div class="col-sm-6">
                            <asp:TextBox ID="txt_water" class="form-control input-small"
                                runat="server" AutoPostBack="True" OnTextChanged="txt_water_TextChanged"></asp:TextBox></div>
                                 <div class="col-sm-6">
                            水温(℃)：  
                                     <asp:Label ID="lb_ms" runat="server" Text="" style="color: #FF0000" ></asp:Label>
                                         </div>
                        <div class="col-sm-6">
                            <asp:TextBox ID="txt_temperature" class="form-control input-small"   Text="50"
                                runat="server"  ></asp:TextBox></div>
                            <div class="col-sm-12" style="color: #FF0000">
                            默认温度为50℃,可手动填写当前实际温度</div>
                        <div class="col-sm-6">
                            实际密度(g/cm3)：</div>
                        <div class="col-sm-6">
                            <asp:TextBox ID="txt_midu" runat="server" class="form-control input-small"></asp:TextBox></div>
                        <div class="col-sm-12" style="color: #FF0000">
                            *实际密度值要求≥2.65</div>
                        <div class="col-sm-6">
                            测氢样块表面状态：</div>
                        <div class="col-sm-6">
                            <asp:DropDownList ID="txt_bmzt" runat="server" class="form-control input-small">
                                <asp:ListItem></asp:ListItem>
                                <asp:ListItem>含氢量极少，上表面平整</asp:ListItem>
                                <asp:ListItem>含氢量少，上表面略有突起</asp:ListItem>
                                <asp:ListItem>含氢量多，上表面鼓包严重</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="col-sm-12">
                            <asp:Button ID="Button4" runat="server" Text="测氢完成" class="btn btn-large btn-primary"
                                Height="70px" Width="170px" OnClick="Button4_Click" />
                        </div>
                    </div>
                </div>
                </ContentTemplate>
                                          
                </asp:UpdatePanel>
                <div class="row">
                    <div class="col-sm-12">
                        <div class="panel-body">
                            <div id="Div7" runat="server" class="col-sm-4" style="padding: 0;
                                position: relative; float: left; top: 0px; left: 0px; width: 170px;
                                height: 70px;">
                                <asp:Button ID="Button5" runat="server" Font-Size="X-Large" class="btn btn-primary"
                                    Style="position: absolute; left: -1; right: -18px; width: 170px; 
                                    top: -1px; height: 70px;" Text="返回" OnClick="Button5_Click" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    
</asp:Content>
