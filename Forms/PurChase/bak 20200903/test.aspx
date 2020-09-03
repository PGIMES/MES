<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="test.aspx.cs" Inherits="Forms_PurChase_test" %>

<%@ Register Assembly="DevExpress.Web.v17.2, Version=17.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <dx:ASPxFileManager ID="ASPxFileManager1" runat="server">
        <Settings RootFolder="~/UploadFile/Purchase/" ThumbnailFolder="~/Thumb/" EnableMultiSelect="True" />
        <SettingsEditing AllowDownload="true" />
        <SettingsUpload Enabled="false"></SettingsUpload>
        <SettingsToolbar ShowPath="false"></SettingsToolbar>
        <SettingsFolders Visible="false" />
        <SettingsFileList View="Details" ShowFolders="true" ShowParentFolder="true" ></SettingsFileList>
    </dx:ASPxFileManager>
</asp:Content>

