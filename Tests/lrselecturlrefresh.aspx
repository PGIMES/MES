<%@ Page Language="C#"  %>
<% 
    string refreshid = Request["refreshid"];//点击加载项的ID，根据这个ID去加载它的下级
    string json = "[";
    json += "{\"id\":\"1\",\"title\":\"测试1\",\"ico\":\"\",\"link\":\"\",\"hasChilds\":0,\"childs\":[]},";
    json += "{\"id\":\"2\",\"title\":\"测试2\",\"ico\":\"\",\"link\":\"\",\"hasChilds\":0,\"childs\":[]},";
    json += "{\"id\":\"3\",\"title\":\"测试3\",\"ico\":\"\",\"link\":\"\",\"hasChilds\":0,\"childs\":[]}";
    json += "]";
    Response.Write(json);
%>