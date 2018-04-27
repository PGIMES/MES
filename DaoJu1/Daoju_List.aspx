<%@ Page Title="【刀具清单】" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Daoju_List.aspx.cs" Inherits="Daoju_Daoju_List"  EnableEventValidation="true" %>

<%@ Register assembly="DevExpress.Web.v17.2, Version=17.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web" tagprefix="dx" %>

<%--<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>--%>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
     <script src="../Content/js/jquery.min.js" type="text/javascript"></script>
    <script type="text/javascript" src="../Content/js/jquery.cookie.min.js"></script>
    <script src="../Content/js/bootstrap.min.js" type="text/javascript"></script>
    <script src="../Content/js/plugins/layer/layer.min.js" type="text/javascript"></script>

    <script type="text/javascript" language="javascript">

         $("#mestitle").text("【刀具清单】");
    </script>
    <style type="text/css">
        .row {
            margin-right: 2px;
            margin-left: 2px;
        }

        /*.row-container {
            padding-left: 2px;
            padding-right: 2px;
        }*/

        fieldset {
            background: rgba(255,255,255,.3);
            border-color: lightblue;
            border-style: solid;
            border-width: 2px;
            -moz-border-radius: 5px;
            -webkit-border-radius: 5px;
            -khtml-border-radius: 5px;
            border-radius: 5px;
            line-height: 30px;
            list-style: none;
            padding: 5px 10px;
            margin-bottom: 2px;
        }

        legend {
            color: #302A2A;
            font: bold 16px/2 Verdana, Geneva, sans-serif;
            font-weight: bold;
            text-align: left; /*text-shadow: 2px 2px 2px rgb(88, 126, 156);*/
        }

        fieldset {
            padding: .35em .625em .75em;
            margin: 0 2px;
            border: 1px solid lightblue;
        }

        legend {
            border-style: none;
            border-color: inherit;
            border-width: 0;
            padding: 5px;
            width: 108px;
            margin-bottom: 2px;
        }

        .panel {
            margin-bottom: 5px;
        }

        .panel-heading {
            padding: 5px 5px 5px 5px;
        }

        .panel-body {
            padding: 5px 5px 5px 5px;
        }

        body {
            margin-left: 5px;
            margin-right: 5px;
        }

        .col-lg-1, .col-lg-10, .col-lg-11, .col-lg-12, .col-lg-2, .col-lg-3, .col-lg-4, .col-lg-5, .col-lg-6, .col-lg-7, .col-lg-8, .col-lg-9, .col-md-1, .col-md-10, .col-md-11, .col-md-12, .col-md-2, .col-md-3, .col-md-4, .col-md-5, .col-md-6, .col-md-7, .col-md-8, .col-md-9, .col-sm-1, .col-sm-10, .col-sm-11, .col-sm-12, .col-sm-2, .col-sm-3, .col-sm-4, .col-sm-5, .col-sm-6, .col-sm-7, .col-sm-8, .col-sm-9, .col-xs-1, .col-xs-10, .col-xs-11, .col-xs-12, .col-xs-2, .col-xs-3, .col-xs-4, .col-xs-5, .col-xs-6, .col-xs-7, .col-xs-8, .col-xs-9 {
            position: relative;
            min-height: 1px;
            padding-right: 1px;
            padding-left: 1px;
            margin-top: 10px;
        }
    </style>




    <script type="text/javascript">
           
            var prevselitem = null;
            function selectx(row) {
                if (prevselitem != null) {
                    prevselitem.style.backgroundColor = '#ffffff';
                }
                row.style.backgroundColor = 'PeachPuff';
                prevselitem = row;

            }
          
            $(document).ready(function () {

                getUser();

                $("#MainContent_txtdept").change(getUser)
                $("#MainContent_txtproduct_user").change(function () {
                $.cookie('user', $("#MainContent_txtproduct_user").val());
                })
                    
             

            });

            function getUser()
            {
              // alert($("#MainContent_txtdept").val());
               
                $.ajax({
                  
                    type: "post",
                    url: "Daoju_List.aspx/Getuser",
                    data: "{'lsdept':'" + $("#MainContent_txtdept").val() + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (data) {
                        if (data.d != "") //返回的数据用data.d获取内容Logaction + "成功."
                        {
                           
                            $('#MainContent_txtproduct_user').html("");
                            $('#MainContent_txtproduct_user').append("<option value=''>ALL</option>");
                            $.each(eval(data.d), function (i) {
                               // alert(eval(data.d)[i].value);
                                if ($.cookie("user") == eval(data.d)[i].value) {
                                    $('#MainContent_txtproduct_user').append("<option value=" + eval(data.d)[i].value + " selected>" + eval(data.d)[i].value + "</option>")
                                   
                                } else {
                                    $('#MainContent_txtproduct_user').append("<option value=" + eval(data.d)[i].value + ">" + eval(data.d)[i].value + "</option>");
                                }
                                


                            });
                        }
                        else {
                            alert("失败.");
                        }
                    }
                    
                });
                //alert('xxxx');
            }




 </script>
    
    <script type="text/javascript">
    var popupwindow = null;
    function Getzy() {
 
        popupwindow = window.open('../Select/select_XMH.aspx?id=zy', '_blank', 'height=400,width=600,resizable=no,menubar=no,scrollbars =no,location=no');
    }
    function zy_setvalue(formName, xmh, ljh, gxh) {
         <%-- ctl01.<%=txt_xm.ClientID%>.value = xmh;
   
  
       popupwindow.close();--%>
    }


    </script>

    <script type="text/javascript">
    var popupwindow = null;
    function Getbz() {
 
        popupwindow = window.open('../Select/select_XMH.aspx', '_blank', 'height=400,width=600,resizable=no,menubar=no,scrollbars =no,location=no');
    }
    function bz_setvalue(formName, xmh, ljh, gxh) {
        <%--  ctl01.<%=txt_bzdj.ClientID%>.value = xmh;
   
  --%>
       popupwindow.close();
    }

         //function GetSelectedFieldValues_Callback(result) {
         //    alert(result);
         //   if (result.length > 0) {
         //       alert(result);
         //   }
            
         //}
         
         function edit()
         {
            
             var a=grid.GetRowValues(grid.GetFocusedRowIndex()
            , 'id'       //多个字段之间用分号；隔开，如果只有一个字段要有分号；结束
            , OnGetRowValues); //获取对应字段的值的回调函数
             
         }
       
        //function OnGridFocusedRowChanged() {
        //    var a=grid.GetRowValues(grid.GetFocusedRowIndex()
        //    , 'pgi_no;pn'       //多个字段之间用分号；隔开，如果只有一个字段要有分号；结束
        //    , OnGetRowValues); //获取对应字段的值的回调函数

        //   // alert(a);
        //}

         function OnGetRowValues(values) {
             var lsid = values;    //  与字段索引取值
             window.open('daoju.aspx?id=' + lsid);
         }
        
    </script>
      
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
       <%--容器查询开始--%>
   <%-- <asp:UpdatePanel runat="server" ID="UpdatePanel1" UpdateMode="Conditional">
                                    <ContentTemplate>--%>
    <div class="col-md-12" >
     <div class="row  row-container">
            <div class="col-md-12">
                <div class="panel panel-info">
                    <div class="panel-heading" data-toggle="collapse" data-target="#CPXX">
                        <strong>刀具清单查询</strong>
                    </div>
                    <div class="panel-body  collapse in" id="CPXX" >
                       <div class="col-xs-12 col-sm-12  col-md-12 col-lg-12" style="font-size:13px;;">
                            <table class="tblCondition" style=" border-collapse: collapse;">
                        <tr>
                            <td style="width:60px;">PGI编号</td>
                            <td style="width:70px;">
                                <asp:TextBox ID="txtpgi_no" class="form-control input-s-sm" runat="server" Width="60px" Height="29px" Font-Size="12px"></asp:TextBox></td>
                            <td style="width:60px;">零件号</td>
                            <td style="width:130px;">
                                <asp:TextBox ID="txtpn" class="form-control input-s-sm" runat="server" Width="120px" Height="29px" Font-Size="12px"></asp:TextBox></td>
                            <td style="width:40px;">部门</td>
                            <td style="width:120px;">
                                <asp:DropDownList ID="txtdept" runat="server" class="form-control input-s-md " Width="100px" Height="29px" Font-Size="12px">
                                        <asp:ListItem Value="" Selected="True">ALL</asp:ListItem>
                                        <asp:ListItem Value="工程一部">工程一部</asp:ListItem>
                                        <asp:ListItem Value="工程二部">工程二部</asp:ListItem>
                                        <asp:ListItem Value="产品一组">&nbsp;&nbsp;&nbsp;&nbsp;产品一组</asp:ListItem>
                                        <asp:ListItem Value="产品三组">&nbsp;&nbsp;&nbsp;&nbsp;产品三组</asp:ListItem>
                                        <asp:ListItem Value="调试组">&nbsp;&nbsp;&nbsp;&nbsp;调试组</asp:ListItem>
                                        <asp:ListItem Value="工程三部">工程三部</asp:ListItem>
                                        <asp:ListItem Value="产品二组">&nbsp;&nbsp;&nbsp;&nbsp;产品二组</asp:ListItem>
                                        <asp:ListItem Value="产品四组">&nbsp;&nbsp;&nbsp;&nbsp;产品四组</asp:ListItem>
                                    </asp:DropDownList></td>
                            <td style="width:80px;">产品工程师</td>
                            <td style="width:120px;">
                                <asp:DropDownList ID="txtproduct_user" runat="server" class="form-control input-s-md " Width="100px" Height="29px" Font-Size="12px"></asp:DropDownList></td>
                            <td style="width:80px;">当前版本
                            </td>
                            <td style="display: block; margin: 10px 0; width:100px;"> <asp:DropDownList ID="txtver" runat="server" class="form-control input-s-md " Height="29px" Font-Size="12px">
                              <%--  <asp:ListItem Value="">ALL</asp:ListItem>--%>
                                <asp:ListItem Value="当前" Selected="True">当前</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td>  &nbsp;&nbsp;<asp:Button ID="btnsearch" runat="server" Text="查询" class="btn btn-large btn-primary" Width="80px"  Height="32px" Font-Size="12px"/>
                                  &nbsp;&nbsp; &nbsp;&nbsp;
                                
                                      <input id="btnadd" type="button" value="新增" class="btn btn-large btn-primary" style="width:60px; height:32px; font-size:12px;" onclick="window.open('daoju.aspx')" />
                                   <%--<asp:Button ID="btnadd" runat="server" Text="新增" class="btn btn-large btn-primary" Width="80px" OnClick="btnadd_Click"  />--%>
                                     &nbsp;&nbsp; 
                                 <input id="btnedit" type="button" value="编辑" class="btn btn-large btn-primary" style="width:60px;height:32px; font-size:12px;" onclick="edit()"  Height="30px"/>
                               <%-- <asp:Button ID="btnedit" runat="server" Text="编辑" class="btn btn-large btn-primary" Width="80px" OnClick="btnedit_Click" />--%>&nbsp;&nbsp;
                                    <asp:Button ID="Button4" runat="server" Text="导出" class="btn btn-large btn-primary" Width="60px"  Height="32px" Font-Size="12px"/>&nbsp;&nbsp;
                                    <asp:Button ID="Button5" runat="server" Text="打印" class="btn btn-large btn-primary" Width="60px"  Height="32px" Font-Size="12px"/></td>
                        </tr>

<%--                        <tr>
                            <td ></td>
                            <td>
                               </td>
                            <td></td>
                            <td></td>
                            <td></td>
                           
                            <td colspan="5" style="text-align:right">
                                
                            </td>

                        </tr>--%>
                    </table>
                           </div>
                    </div>
                </div>
            </div>
        </div>
        </div>
                  <%-- </ContentTemplate></asp:UpdatePanel>   --%>                  
    <%--容器查询结束--%>
     <asp:UpdatePanel runat="server" ID="p1" UpdateMode="Conditional">
                                    <ContentTemplate>
       <%--容器1开始--%>
       <div class="row  row-container">
            <div class="col-md-12">
                <div class="panel panel-info">
                    <div class="panel-heading" data-toggle="collapse" data-target="#gscs">
                        <strong style="padding-right: 100px">刀具清单</strong>
                    </div>
                    <div class="panel-body  collapse in" id="gscs" >                           
                         <div runat="server" id="DIV1" style="margin-left: 5px; margin-right: 5px; margin-bottom: 10px; ">
            <table>
                <tr>
                    <td valign="top" >


                 
                        <dx:aspxgridview ID="gv1" runat="server" EnableCallBacks="false"   KeyFieldName="pgi_no"   OnFocusedRowChanged="gv1_FocusedRowChanged"  Theme="MetropolisBlue" OnHtmlRowPrepared="gv1_HtmlRowPrepared" ClientInstanceName="grid"
                               >

                                   <SettingsPager PageSize="5"  PageSizeItemSettings-Visible="true" Visible="False">
<PageSizeItemSettings Items="100, 200, 500, 1000" ShowAllItem="True"></PageSizeItemSettings>
                             </SettingsPager>
                          
                             <Settings ShowFilterBar="Auto" 
                    ShowFilterRowMenu="True" ShowFilterRowMenuLikeItem="True"   VerticalScrollBarMode="Visible" VerticalScrollBarStyle="VirtualSmooth" VerticalScrollableHeight="100" AutoFilterCondition="BeginsWith"/>
                          <SettingsBehavior AllowFocusedRow="True"   ProcessFocusedRowChangedOnServer="True"  ColumnResizeMode="Control" MergeGroupsMode="Always"/>
                                   <Styles>
                                       <Header BackColor="#1E82CD" ForeColor="White" Font-Size="12px">
                                       </Header>
                                       <DetailRow Font-Size="12px">
                                       </DetailRow>
                                   </Styles>

                        </dx:aspxgridview>
                                       
                    </td>
                </tr>
            </table>
        </div>

                            </div>
                        </div>
                    </div>
                </div>
            <%--容器1结束--%>

      <%--容器2开始--%>   
       <div class="row  row-container">
            <div class="col-md-12">
                <div class="panel panel-info">
                    <div class="panel-heading" data-toggle="collapse" data-target="#mx">
                        <strong style="padding-right: 60px">刀具清单明细</strong>
                        <asp:Label ID="lblpgi_no" runat="server" Text="Label"></asp:Label>
                        &nbsp;&nbsp;
                        <asp:Label ID="lblop" runat="server" Text="Label"></asp:Label>
                        &nbsp;&nbsp;
                        <asp:Label ID="lblver" runat="server" Text="Label"></asp:Label>
                    </div>
                    <div class="panel-body  collapse in" id="mx">
                        <div class="col-xs-12 col-sm-12  col-md-12 col-lg-12">
                            <dx:ASPxGridView ID="gv2" runat="server" KeyFieldName="id" Theme="MetropolisBlue">
                             <SettingsPager PageSize="100"  PageSizeItemSettings-Visible="true">
<PageSizeItemSettings Items="100, 200, 500, 1000" ShowAllItem="True"></PageSizeItemSettings>
                             </SettingsPager>
                               
                             <Settings ShowFilterBar="Visible" ShowFilterRow="True" 
                    ShowFilterRowMenu="True" ShowFilterRowMenuLikeItem="True" ShowFooter="True"  VerticalScrollBarMode="Visible" VerticalScrollBarStyle="VirtualSmooth" VerticalScrollableHeight="550" AutoFilterCondition="BeginsWith"/>
                          <SettingsBehavior   ColumnResizeMode="Control"/>
                                <Styles>
                                    <Header BackColor="#1E82CD" ForeColor="White" Font-Size="12px">
                                    </Header>
                                    <DetailRow Font-Size="12px">
                                    </DetailRow>
                                </Styles>
                            </dx:ASPxGridView>

                            <div> 
                             
                               
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
     <%--容器2结束--%>                                                      
   </ContentTemplate></asp:UpdatePanel>
                                          

</asp:Content>
