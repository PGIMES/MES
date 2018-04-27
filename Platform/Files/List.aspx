<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="List.aspx.cs" Inherits="WebForm.Platform.Files.List" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div class="toolbar" style="margin-top:0; border-top:none 0; position:fixed; top:0; left:0; right:0; margin-left:auto; z-index:999; width:100%; margin-right:auto;">
            <%if("1"==Request.QueryString["isselect"]){ %>
            <a href="javascript:void(0);" onclick="confirmSelect();return false;"><i class='fa fa-check-square-o' style='font-size:16px;vertical-align:middle;margin-right:3px;'></i><label>确定选择</label></a>
            <span class="toolbarsplit">&nbsp;</span>
            <%} %>
            <%if(!ParentDir.IsNullOrEmpty()){ %>
            <a href="javascript:void(0);" onclick="parentDir('<%=ParentDir.DesEncrypt() %>');return false;"><i class='fa fa-arrow-up' style='font-size:16px;vertical-align:middle;margin-right:3px;'></i><label>返回上级</label></a>
            <%} %>
            <a href="javascript:void(0);" onclick="addDir();return false;"><i class='fa fa-folder-open-o' style='font-size:16px;vertical-align:middle;margin-right:3px;'></i><label>添加文件夹</label></a>
            <a href="javascript:void(0);" onclick="addFile();return false;"><i class='fa fa-file-word-o' style='font-size:16px;vertical-align:middle;margin-right:3px;'></i><label>添加文件</label></a>
            <span class="toolbarsplit">&nbsp;</span>
            <asp:LinkButton ID="LinkButton2" runat="server" OnClientClick="return delCurrentDir();" OnClick="LinkButton2_Click"><i class='fa fa-close (alias)' style='font-size:16px;vertical-align:middle;margin-right:3px;'></i><label>删除当前文件夹</label></asp:LinkButton>
            <asp:LinkButton ID="LinkButton1" runat="server" OnClientClick="return delSelectDir();" OnClick="LinkButton1_Click"><i class='fa fa-close (alias)' style='font-size:16px;vertical-align:middle;margin-right:3px;'></i><label>删除所选</label></asp:LinkButton>
        </div>
        <div style="margin-top:35px;"></div>
        
        <div>
            <table class="listtable">
                <thead>
                    <tr>
                        <th style="width:3%"><input type="checkbox" onclick="$('input[name=\'file\'][disabled!=\'disabled\']').prop('checked', this.checked);" style="vertical-align:middle;" /></th>
                        <th>名称</th>
                        <th>类型</th>
                        <th>大小</th>
                        <th>创建时间</th>
                        <th>修改时间</th>
                    </tr>
                </thead>
                <tbody>
                    <%foreach(var fi in FilesList){
                          string id = fi.FullName.DesEncrypt();
                          string id1 = fi.FullName.Replace1(BFiles.GetRootPath(), "").DesEncrypt();   
                     %>
                    <tr>
                        <td><input type="checkbox" name="file" <%=IsSelect && fi.Type==0 ? "disabled='disabled'": "" %> <%=fi.Type==1 && IsSelect?"onclick='fileClick(\""+fi.Name.UrlEncode()+"\",\""+id+"\",\""+id1+"\",\""+(fi.Length.HasValue?decimal.Round((fi.Length.Value/1024),0).ToFormatString()+"KB":"")+"\", this.checked);'":"" %> value="<%=id %>"/></td>
                        <td style="word-break:break-all;"><a href="javascript:void(0);" onclick="show(<%=fi.Type %>,'<%=id %>');return false;"><i class='fa <%=fi.Type==0?"fa-folder":"fa-file-o" %>' style='font-size:16px;vertical-align:middle;margin-right:3px;'></i><label><%=fi.Name %></label></a></td>
                        <td><%=fi.Type==0?"文件夹": System.IO.Path.GetExtension(fi.FullName).Replace(".","").ToUpper() + "文件" %></td>
                        <td style="text-align:right;padding-right:6px;"><%=fi.Length.HasValue?decimal.Round((fi.Length.Value/1024),0).ToFormatString()+"KB":"" %></td>
                        <td><%=fi.CreateTime.ToDateTimeStringS() %></td>
                        <td><%=fi.ModifyTime.ToDateTimeStringS() %></td>
                    </tr>
                    <%} %>
                </tbody>
            </table>
        </div>
        <br /><br />
    </form>
    <script type="text/javascript">
        var query = "<%=Request.Url.Query%>";
        var isselect = "1" == "<%=Request.QueryString["isselect"]%>";
        function parentDir(id)
        {
            window.location = "List.aspx?id=" + id + "<%=Query%>";
        }
        function show(type, id)
        {
            if (type == 0)
            {
                window.location = "List.aspx?id=" + id + "<%=Query%>";
            }
            else
            {
                RoadUI.Core.open("Show.ashx?id=" + id + "<%=Query%>", 800, 500);
            }
        }
        function addDir()
        {
            var url = 'Dir_Add.aspx' + query;
            window.location = url;
        }
        function addFile()
        {
            var url = 'File_Add.aspx' + query;
            if (isselect)
            {
                window.location = url;
            }
            else
            {
                new RoadUI.Window().open({ url: url, width: 600, height: 400, title: "添加文件", opener: parent, openerid: "Iframe2_FilesList", showclose: true });
            }
        }
        function delCurrentDir()
        {
            return confirm('删除文件夹会删除其所有子目录和文件且不可恢复!您确定要删除吗?');
        }
        function delSelectDir()
        {
            if ($(":checked[name='file']").size() == 0)
            {
                alert('您没有选择要删除的目录或文件!');
                return false;
            }
            return confirm('文件夹及文件删除不可恢复!您确定要删除吗?');
        }

        function fileClick(name, id, id1, size, checked)
        {
            var name1 = decodeURIComponent(name);
            if (checked)
            {
                parent.parent.addFile(name1, id, id1, size, 1);
            }
            else
            {
                parent.parent.delFile(id);
            }
        }

        function confirmSelect()
        {
            parent.parent.confirm1();
        }
    </script>
</body>
</html>
