<%@ Page Language="C#" AutoEventWireup="true" CodeFile="select_PlanAttendant.aspx.cs" Inherits="select_PlanAttendant" %>
<%@ Register assembly="DevExpress.Web.v17.2, Version=17.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web" tagprefix="dx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript" src="/Content/js/jquery.min.js"></script>
    <script src="/Content/js/layer/layer.js"></script>
    <link href="/Content/css/font-awesome.min.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        #btn_cj {
            height: 23px;
            width: 60px;
        }
    </style>
     <script type="text/javascript">
        $(document).ready(function () {

            $('#btn_cj').click(function () {
                //if (grid.GetSelectedRowCount() <=0) { layer.alert("请选择随行人员!"); return; }
                
                if (grid.GetSelectedRowCount() <=0) { 
                   if (confirm("随行人员为空,确定传送吗？")==true){
                        parent.setvalue_PlanAttendant("");
                        var index = parent.layer.getFrameIndex(window.name);
                        parent.layer.close(index);
                    }; 
                    return;
                }

                grid.GetSelectedFieldValues("workcode;lastname", function GetVal(values) {
                    //for (var i = 0; i < values.length; i++) { values[i][1]}
                    parent.setvalue_PlanAttendant(values);
                    var index = parent.layer.getFrameIndex(window.name);
                    parent.layer.close(index);
                });

            });
            
        });
    </script>
</head>
<body style="background: url(../images/bg.jpg) repeat-x;">
    <form id="form1" runat="server">
        <div>
            <table>
                <tr style="height:30px;">
                    <td>
                        <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="关键字："></dx:ASPxLabel>
                        <asp:TextBox ID="txtKeywords" runat="server" Width="200px"></asp:TextBox>
                        <asp:Button ID="BtnStartSearch" runat="server" Text="查询" OnClick="BtnStartSearch_Click" />
                    </td>
                    <td align="right">&nbsp;</td>
                </tr>
                <tr>
                    <td align="center" colspan="2">

                        <asp:Label ID="lb_msg" runat="server" Text="" ForeColor="Red"></asp:Label>

                        <dx:aspxgridview ID="gv" runat="server" AutoGenerateColumns="False" KeyFieldName="workcode" Theme="MetropolisBlue"  ClientInstanceName="grid"  EnableTheming="True">
                            <SettingsPager PageSize="1000"></SettingsPager>
                            <SettingsBehavior AllowSelectByRowClick="True" AllowDragDrop="False" AllowSort="False" />
                            <Settings VerticalScrollBarMode="Visible" VerticalScrollBarStyle="Standard" VerticalScrollableHeight="400"  />
                            <Columns>
                                <dx:GridViewCommandColumn SelectAllCheckboxMode="Page" ShowClearFilterButton="true" ShowSelectCheckbox="true" Name="Sel" Width="30" VisibleIndex="0"></dx:GridViewCommandColumn>    
                                <dx:GridViewDataTextColumn Caption="序号" FieldName="numid" Width="50px" VisibleIndex="1"></dx:GridViewDataTextColumn>                                                               
                                <dx:GridViewDataTextColumn Caption="工号" FieldName="workcode" Width="100px" VisibleIndex="2"></dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="姓名" FieldName="lastname" Width="100px" VisibleIndex="3"></dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="部门" FieldName="dept_name" Width="100px" VisibleIndex="4"></dx:GridViewDataTextColumn>
                            </Columns>                                              
                            <Styles>
                                <Header BackColor="#E4EFFA"  ></Header>        
                                <SelectedRow BackColor="#FDF7D9"></SelectedRow>      
                            </Styles>                                          
                        </dx:aspxgridview>

                    </td>
                </tr>
                <tr style="height:30px;">
                    <td></td>
                    <td align="right">
                        <button id="btn_cj" type="button" class="btn btn-primary btn-large"><font color="blue"<i class="fa fa-send"></i>&nbsp;传送</font></button>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
