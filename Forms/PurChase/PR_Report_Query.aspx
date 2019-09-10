<%@ Page Title="请购单查询" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="PR_Report_Query.aspx.cs" Inherits="Forms_PurChase_PR_Report_Query" %>

<%@ Register Assembly="DevExpress.Web.v17.2, Version=17.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>





<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <style type="text/css">
        /*td {
            padding-left: 5px;
            padding-right: 5px;
        }*/
        .tblCondition td{ white-space:nowrap }
        /*.dxgvHeader td {
            white-space:normal; 
        }*/
        .dx-wrap{
     white-space:  inherit; 
    line-height: normal;
    padding: 0;
}
        /*自动隐藏文字*/=====1行
.public-ellipsis-1 {
    overflow: hidden;
    white-space: nowrap;
    text-overflow: ellipsis;
}
/*自动隐藏文字*/=====2行
.public-ellipsis-2 {
    display: -webkit-box !important;
    overflow: hidden !important;
    text-overflow: ellipsis !important;
    word-wrap: break-word !important;
    white-space: normal !important;
    -webkit-line-clamp: 2 !important;
    -webkit-box-orient: vertical !important;
}
/*自动隐藏文字*/=====3行
.public-ellipsis-3 {
    display: -webkit-box !important;
    overflow: hidden !important;
    text-overflow: ellipsis !important;
    word-wrap: break-word !important;
    white-space: normal !important;
    -webkit-line-clamp: 3 !important;
    -webkit-box-orient: vertical !important;

}
          border{border:solid 1px red}       

             .hidden { display:none;}
		     .hidden1
            {
            	border:0px; 
            	overflow:hidden
            	
            	}
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">

    <script src="../../Content/js/jquery.min.js"  type="text/javascript"></script>
    <script src="../../Content/js/bootstrap.min.js"></script>
    <%--<script src="../../Content/js/plugins/layer/layer.min.js" type="text/javascript"></script>--%>
    <script src="/Content/js/layer/layer.js"></script>
    <script src="../../Content/js/plugins/layer/laydate/laydate.js"></script>
    <script type="text/javascript">
        var UserId = '<%=UserId%>';
        var UserName = '<%=UserName%>';
        var DeptName = '<%=DeptName%>';
        
        $(document).ready(function () {
            $("#mestitle").text("【请购单查询】");

            //尹姣、余啸琳
            if (DeptName.indexOf("IT") != -1 || UserId == "01190" || UserId == "02085") {
                $('#btn_syr').show();
            } else {
                $('#btn_syr').hide();
            }

            setHeight();
            $(window).resize(function () {
                setHeight();
            });

            $('#btn_syr').click(function () {
                grid.GetRowValues(grid.GetFocusedRowIndex(), 'PRNo;rowid', function OnGetRowValues(values) {
                    layer.open({
                        title: '请购使用人维护',
                        closeBtn: 2,
                        type: 2,
                        area: ['500px', '480px'],
                        fixed: false, //不固定
                        maxmin: true, //开启最大化最小化按钮
                        content: "PR_syr_maintain.aspx?PRNo=" + values[0] + "&rowid=" + values[1],
                        cancel: function () {
                            //右上角关闭回调
                            parent.location.reload();
                        }
                    });
                });
            });

        });


        function setHeight() {
            $("div[class=dxgvCSD]").css("height", ($(window).height() - $("#div_p").height()-300 ) + "px");
        }
        	
    </script>
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
     
    <div class="panel-body" id="div_p">
        <div class="col-sm-12">
            <table>
                <tr>
                    <td>采购类别:</td>
                    <td>
                        <asp:DropDownList ID="drop_type" runat="server" class="form-control input-s-sm ">
                            <asp:ListItem>刀具类</asp:ListItem>
                            <asp:ListItem>非刀具辅料类</asp:ListItem>
                            <asp:ListItem>原材料</asp:ListItem>
                            <asp:ListItem>费用服务类</asp:ListItem>
                            <asp:ListItem>合同类</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td>创建日期:</td>
                    <td >
                        <asp:TextBox ID="txtDateFrom" class="form-control" onclick="laydate()" runat="server" Width="100px"></asp:TextBox>
                    </td>
                    <td>~</td>
                    <td>
                        <asp:TextBox ID="txtDateTo" class="form-control" onclick="laydate()" runat="server" Width="100px"></asp:TextBox>
                    </td>  
                    <td>用于产品/项目:</td>
                    <td>
                        <asp:TextBox ID="txtUserFor" class="form-control" runat="server" Width="150px"></asp:TextBox>
                    </td>      
                    <td> 
                        &nbsp;
                        <asp:Button ID="Bt_select" runat="server" Text="查询" class="btn btn-large btn-primary" OnClick="Button1_Click" Width="100px" />   
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="Bt_Export" runat="server" Text="导出" class="btn btn-large btn-primary" OnClick="Bt_Export_Click" Width="100px" />   
                        &nbsp;&nbsp;&nbsp;&nbsp;   
                        <asp:Button ID="btnNext" runat="server" OnClick="btnNext_Click" Style="display: none" Text="Next" />
                         &nbsp;&nbsp;&nbsp;&nbsp;   
                        <button id="btn_syr" type="button" class="btn btn-primary btn-large"><i class="fa fa-pencil-square-o fa-fw"></i>&nbsp;使用人维护</button> 
                    </td>
                </tr>
            </table>
        </div>
    </div>

    <div runat="server" id="DIV1" style="margin-left: 5px; margin-right: 5px; margin-bottom: 10px">
          <table>
            <tr>
                <td>
                    <dx:ASPxGridView ID="GV_PART" runat="server" KeyFieldName="PRNo;rowid" AutoGenerateColumns="False"   ClientInstanceName="grid"
                        OnHtmlRowPrepared="GV_PART_HtmlRowPrepared" OnHtmlRowCreated="GV_PART_HtmlRowCreated" onrowcommand="GV_PART_RowCommand" onpageindexchanged="GV_PART_PageIndexChanged"
                        OnExportRenderBrick="GV_PART_ExportRenderBrick">
                        <ClientSideEvents EndCallback="function(s, e) {setHeight();}" />
                        <SettingsBehavior AllowDragDrop="TRUE" AllowFocusedRow="True" AllowSelectByRowClick="True" ColumnResizeMode="Control" 
                            autoexpandallgroups="True" mergegroupsmode="Always" sortmode="Value" />
                        <SettingsPager PageSize="1000"></SettingsPager>
                        <Settings ShowFilterRow="True" ShowFilterRowMenu="True" VerticalScrollBarMode="Visible" VerticalScrollBarStyle="Standard" VerticalScrollableHeight="600"
                                ShowFilterRowMenuLikeItem="True" ShowGroupPanel="True" ShowFooter="True" showgroupedcolumns="True"/>
                        <SettingsSearchPanel Visible="True" />
                        <SettingsFilterControl AllowHierarchicalColumns="True"></SettingsFilterControl>
                        <Columns>
                        </Columns>
                        <TotalSummary>
                            <dx:ASPxSummaryItem DisplayFormat="{0:N0}" FieldName="targetzj" SummaryType="Sum" />                                       
                        </TotalSummary>
                        <Styles>
                            <AlternatingRow Enabled="true" />
                            <Header BackColor="#99CCFF"></Header>
                            <FocusedRow BackColor="#99CCFF" ForeColor="#0000CC"></FocusedRow>
                            <Footer HorizontalAlign="Right"></Footer>
                        </Styles>
                    </dx:ASPxGridView>
                </td>
            </tr>
        </table>
    
    </div>
</asp:Content>





