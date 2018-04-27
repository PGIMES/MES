<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TableEdit_Oracle.aspx.cs" Inherits="WebForm.Platform.DBConnection.TableEdit_Oracle" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div class="toolbar" style="margin-top:0; border-top:none 0; position:fixed; top:0; left:0; right:0; margin-left:auto; z-index:999; width:100%; margin-right:auto; height:30px;">
        <a href="javascript:void(0);" onclick="return1(); return false;">
            <span style="background:url(../../Images/ico/back.gif) no-repeat left center;">返回前页</span>
        </a>
        <span class="toolbarsplit">&nbsp;</span>
        <a href="javascript:void(0);" onclick="addRow(); return false;">
            <span style="background:url(../../Images/ico/add.gif) no-repeat left center;">添加列</span>
        </a>
        <asp:LinkButton ID="LinkButton1" OnClientClick="return check();" runat="server" OnClick="LinkButton1_Click"><span style="background:url(../../Images/ico/save.gif) no-repeat left center;">保存表</span></asp:LinkButton>
    </div>
    <div>
        <div style="margin:44px 0 8px 5px;">表名：
            <input type="text" class="mytext" name="tablename" value="<%=tableName %>" validate="empty" style="width:300px;" />
            <input type="hidden" name="oldtablename" value="<%=tableName %>" />
        </div>
        <input type="hidden" name="delfield" id="delfield" value="" />
        <table class="listtable" style="table-layout:fixed;WORD-BREAK:break-all;WORD-WRAP:break-word;">
            <thead>
                <tr>
                    <th style="width:50px; text-align:center;">序号</th>
                    <th>字段</th>
                    <th>类型</th>
                    <th>长度</th>
                    <th style="width:80px; text-align:center;">可空</th>
                    <th style="width:80px; text-align:center;">自增</th>
                    <th style="width:80px; text-align:center;">主键</th>
                    <th>默认值</th>
                    <th>操作</th>
                </tr>
            </thead>
            <tbody>
                <%
                    int i=1;
                    foreach(System.Data.DataRow dr in SchemaDt.Rows){
                        string length = dr["length"].ToString();
                        if ("0" == length)
                        {
                            length = "";
                        }
                        if ("NUMBER" == dr["T_NAME"].ToString())
                        {
                            length = dr["SCALE"].ToString();
                        }
                %>
                <tr>
                    <td style="text-align:center;"><label><%=i++ %></label><input type="hidden" name="f_name" value="<%=dr["f_name"] %>" /><input type="hidden" value="<%=IsAddTable?"1":"0" %>" name="<%=dr["f_name"] %>_isadd" /></td>
                    <td><input type="text" class="mytext" name="<%=dr["f_name"] %>_name1" value="<%=dr["f_name"] %>" style="width:90%" /></td>
                    <td><select class="myselect" name="<%=dr["f_name"] %>_type" onchange="typeChange(this)" style="width:90%"><%=DBConn.GetFieldDataTypeOptions(dr["t_name"].ToString(), "oracle") %></select></td>
                    <td><input type="text" class="mytext" name="<%=dr["f_name"] %>_length" value="<%=length %>" style="width:90%" /></td>
                    <td style="text-align:center;"><input type="checkbox" name="<%=dr["f_name"] %>_isnull" <%="1"==dr["is_null"].ToString() && !PrimaryKeyList.Contains(dr["f_name"].ToString())?"checked=\"checked\"":"" %> <%=PrimaryKeyList.Contains(dr["f_name"].ToString()) ?"disabled=\"disabled\"":"" %> value="1" /></td>
                    <td style="text-align:center;"><input type="checkbox" onclick="identityClick(this);" name="<%=dr["f_name"] %>_isidentity" <%="int"==dr["t_name"].ToString()?"":"disabled=\"disabled\"" %> <%="1"==dr["isidentity"].ToString()?"checked=\"checked\"":"" %> value="1" /></td>
                    <td style="text-align:center;"><input type="checkbox" onclick="primarykeyClick(this);" name="<%=dr["f_name"] %>_primarykey" <%=PrimaryKeyList.Contains(dr["f_name"].ToString()) ?"checked=\"checked\"":"" %> value="1" /></td>
                    <td><input type="text" class="mytext" name="<%=dr["f_name"] %>_defaultvalue" value="<%=dr["defaultvalue"] %>" style="width:90%" /></td>
                    <td>
                        <a href="javascript:void(0);" onclick="del(this);return false;"><span style="background:url(../../Images/ico/cross.png) no-repeat left center; padding-left:18px;">删除</span></a>
                    </td>
                </tr>
                <%} %>
            </tbody>
        </table>
    </div>
        <br />
    </form>
    <script type="text/javascript">
        function check()
        {
            if (new RoadUI.Validate().validateForm(document.forms[0]))
            {
                return confirm('真的要保存所作更改吗?');
            }
            else
            {
                return false;
            }
        }
        function return1()
        {
            window.location = "Table.aspx<%=Request.Url.Query%>";
        }
        function typeChange(obj)
        {
            var length = "";
            $(obj).find("option").each(function ()
            {
                if ($(this).val() == $(obj).val())
                {
                    length = $(this).attr('data-length');
                    return false;
                }
            });
            var $identity = $(obj).parent().parent().find("[name$='_isidentity']");
            if ("int" != $(obj).val())
            {
                $identity.prop("disabled", true).prop("checked", false);
            }
            else
            {
                $identity.prop("disabled", false);
            }
            $("input[name$='_length']", $(obj).parent().parent()).val(length);
        }
        function identityClick(obj)
        {
            $(".listtable tbody tr").each(function (index)
            {
                if ($(obj).parent().parent().get(0) != $(this).get(0))
                {
                    $("input[name$='_isidentity']", $(this)).prop("checked", false);
                }
                else
                {
                    //$("input[name$='_isnull']", $(this)).prop("checked", false);
                }
            });
        }
        function primarykeyClick(obj)
        {
            if (obj.checked)
            {
                $("input[name$='_isnull']", $(obj).parent().parent()).prop("disabled", true).prop("checked", false);
            }
            else
            {
                $("input[name$='_isnull']", $(obj).parent().parent()).prop("disabled", false);
            }
        }
        function addRow()
        {
            var fname = "f_name_" + RoadUI.Core.newid(false);
            var $tr = $(".listtable tbody tr:last");
            var $tr1 = $tr.clone();
            $(".listtable tbody").append($tr1);
            $("input[type='hidden'][name='f_name']", $tr1).val(fname);
            $("input[type='hidden'][name$='_isadd']", $tr1).attr('name', fname + '_isadd').val("1");
            $("input[name$='_name1']", $tr1).attr('name', fname + '_name1').val('');
            $("select[name$='_type']", $tr1).attr('name', fname + '_type').val('varchar');
            $("input[name$='_length']", $tr1).attr('name', fname + '_length').val('');
            $("input[name$='_isnull']", $tr1).attr('name', fname + '_isnull').val('1').prop('checked', true);
            $("input[name$='_isidentity']", $tr1).attr('name', fname + '_isidentity').val('0').prop('checked', false);
            $("input[name$='_primarykey']", $tr1).attr('name', fname + '_primarykey').val('0').prop('checked', false);
            $("input[name$='_defaultvalue']", $tr1).attr('name', fname + '_defaultvalue').val('');
            $(".listtable tbody tr").each(function (index)
            {
                $("td:first label", $(this)).text((index + 1).toString());
            });
        }
        function del(a)
        {
            var $tr = $(a).parent().parent();
            var field = $("input[type='hidden'][name='f_name']", $tr).val();
            $("#delfield").val($("#delfield").val() + field + ",");
            $tr.remove();
        }
    </script>
</body>
</html>
