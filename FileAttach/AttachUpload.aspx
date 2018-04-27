<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AttachUpload.aspx.cs" Inherits="FileAttach_AttachUpload" MaintainScrollPositionOnPostback="true" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8;IE=Edge, chrome=1;"/>
    <title></title>
    <script src="../Content/js/jquery.min.js"></script>
    <script src="../Content/js/plugins/layer/layer.min.js"></script>
    <script>
        function getQueryString(name) {
            var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)", "i");
            var r = decodeURI(window.location.search).substr(1).match(reg);
            if (r != null) return unescape(r[2]); return null;
        }

        var filesource = getQueryString("filesource");
        $("#span_upload").hide(); $("#span_copy").hide();

        $(document).ready(function () {
            if (filesource == "fileupload") {
                $("#span_upload").show(); $("#span_copy").hide();
            }
            if (filesource == "filecopy") {
                $("#span_upload").hide(); $("#span_copy").show();
            }
        });

    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div>
        <br />
        <span id="span_upload" style="display:none;">
            <input id="txt_file" type="file" runat="server" multiple="multiple" />
            <asp:Button ID="btn_upload" runat="server" Text="上传文件" OnClick="btn_upload_Click" /> 
        </span>
        <span id="span_copy" style="display:none;">
            <input id="txt_file_copy" type="text" runat="server" style="width:300px;" />
            <asp:Button ID="btn_copy" runat="server" Text="上传文件" OnClick="btn_copy_Click" /> 
        </span>
        <asp:Button ID="btn_delete" runat="server" Text="删除" OnClick="btn_delete_Click" OnClientClick="return confirm('确定需要删除吗?');"  />
        <br />
        <br />
        <asp:GridView ID="gv_AttachList" runat="server" AutoGenerateColumns="False" DataKeyNames="id" 
             CellPadding="4" ForeColor="#333333" GridLines="None" 
            OnPageIndexChanging="gv_AttachList_PageIndexChanging" PageSize="100"><%--OnRowDeleting="gv_AttachList_RowDeleting" --%>
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            <Columns>
                <asp:BoundField ApplyFormatInEditMode="True" DataField="id" HeaderText="id" ShowHeader="False" Visible="False"  />
                <asp:BoundField ApplyFormatInEditMode="True" DataField="FileSource" HeaderText="FileSource" ShowHeader="False" Visible="False"  />
                <asp:TemplateField ShowHeader="False" HeaderText="选择" >
                    <HeaderTemplate>

                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:CheckBox ID="cbk_select" runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>                
                <asp:BoundField ApplyFormatInEditMode="True" DataField="rownum" HeaderText="序号" ShowHeader="False" />
                <asp:TemplateField HeaderText="文件名称" ShowHeader="False">
                    <ItemTemplate>
                        <asp:HyperLink ID="HyperLink1" runat="server" Target="_blank"
                             NavigateUrl='<%#DataBinder.Eval(Container.DataItem, "FilePath")%>' 
                             Text='<%#DataBinder.Eval(Container.DataItem,"OriginalFile")%>'></asp:HyperLink>
                    </ItemTemplate>
                </asp:TemplateField>                
                <asp:BoundField ApplyFormatInEditMode="True" DataField="CreateDate" HeaderText="创建时间" ShowHeader="False" />
                <%--<asp:TemplateField ShowHeader="False" HeaderText="">
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="false" CommandName="Delete" Text="刪除"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>--%>
            </Columns>
            <EditRowStyle BackColor="#999999" />
            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
            <SortedAscendingCellStyle BackColor="#E9E7E2" />
            <SortedAscendingHeaderStyle BackColor="#506C8C" />
            <SortedDescendingCellStyle BackColor="#FFFDF8" />
            <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
        </asp:GridView>        
    </div>
    </form>
</body>
</html>
