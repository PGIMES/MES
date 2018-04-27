<%@ Page Language="C#" %>

<% 
    string value = Request.Form["value"];//这是第一个控件的值
    string options = "<option value='1'>"+value+"</option>";
    options += "<option value='2'>测试2</option>";
    options += "<option value='3'>测试3</option>";
    Response.Write(options);  
%>