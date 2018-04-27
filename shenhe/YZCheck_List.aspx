<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="YZCheck_List.aspx.cs" Inherits="shenhe_YZCheck_List"  EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">

 <script src="../Content/js/jquery.min.js" type="text/javascript"></script>

    <script type="text/javascript">
        $(document).ready(function () {
            $("#headTitle").remove();

        })//endready     
        
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <h2 style=" background-color:Yellow">压铸机点检 </h2>
    <asp:Label ID="Label1" runat="server" Text="请选择需点检的图片" ForeColor="Red"></asp:Label>
    <asp:GridView ID="GridView1" runat="server" 
        AutoGenerateColumns="False" ShowHeader="False">
        <Columns>
            <asp:BoundField DataField="pic_id" HeaderText="序号">
            <ControlStyle CssClass="hidden" Width="0px" />
            </asp:BoundField>
            <asp:TemplateField HeaderText="图片">
                <ItemTemplate>
                    <asp:ImageButton ID="ImageButton1" runat="server" 
                        
    ImageUrl='<%#Eval("pic_filepath") %>' onclick="ImageButton1_Click" />

                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</asp:Content>

