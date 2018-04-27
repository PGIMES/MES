<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="demo.aspx.cs" Inherits="demo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
        <div class="panel panel-info">
            <div class="panel-heading" style="vertical-align: middle;" align="center">
                <strong>员工登入</strong>
            </div>
            <div class="row row-container">
                <div class="">
                        <label class="col-sm-3 control-label">xx：</label>
                        <div class="col-sm-3"> 
                           <asp:TextBox ID="TextBox17" class="form-control input-sm" runat="server"></asp:TextBox>
                        </div>
                    </div>
                <div class="row">
                    <div class="col-sm-2 ">
                        <label class="control-label">
                            日期:</label>
                    </div>
                    <div class="col-sm-3">
                        <asp:TextBox ID="TextBox9" class="form-control input-sm" runat="server"></asp:TextBox>
                    </div>
                </div>
                <div class="row col-sm-12">
                    <div class="col-sm-2">
                        时间:
                    </div>
                    <div class="col-sm-4">
                        <asp:TextBox ID="TextBox10" class="form-control input-sm" runat="server"></asp:TextBox>
                    </div>
               
                    <div class="col-sm-2">
                        工号:
                    </div>
                    <div class="col-sm-4">
                        <asp:TextBox ID="TextBox11" class="form-control input-sm" runat="server"></asp:TextBox>
                    </div>
                </div>
                <div class="row">
                    <div class="col-sm-2">
                        姓名:
                    </div>
                    <div class="col-sm-3">
                        <asp:TextBox ID="TextBox12" class="form-control input-sm" runat="server"></asp:TextBox>
                    </div>
                </div>
                <div class="row">
                    <div class="col-sm-2">
                        班别:
                    </div>
                    <div class="col-sm-3">
                        <asp:DropDownList ID="DropDownList3" class="form-control input-sm" runat="server">
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="row">
                    <div class="col-sm-2">
                        班组:
                    </div>
                    <div class="col-sm-3">
                        <asp:TextBox ID="TextBox13" class="form-control input-sm" runat="server"></asp:TextBox>
                    </div>
                </div>
                <div class="row">
                    <div class="col-sm-2">
                        设备号:
                    </div>
                    <div class="col-sm-3">
                        <asp:TextBox ID="TextBox14" class="form-control input-sm" runat="server"></asp:TextBox>
                    </div>
                </div>
                <div class="row">
                    <div class="col-sm-2">
                        设备简称:
                    </div>
                    <div class="col-sm-3">
                        <asp:TextBox ID="TextBox15" class="form-control input-sm" runat="server"></asp:TextBox>
                    </div>
                </div>
                <div class="row">
                    <div class="col-sm-2">
                        合金:
                    </div>
                    <div class="col-sm-4">
                        <asp:DropDownList ID="DropDownList4" class="form-control input-sm" runat="server">
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="row">
                    <div class="col-sm-2">
                        设备规格:
                    </div>
                    <div class="col-sm-4">
                        <asp:TextBox ID="TextBox16" class="form-control input-sm" runat="server"></asp:TextBox>
                    </div>
                </div>
            </div>
        </div>
</asp:Content>

