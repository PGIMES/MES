<%@ Page Language="C#" %>
<% 
    string json = "[";
    json += "{\"id\":\"1\",\"title\":\"测试1\",\"ico\":\"\",\"link\":\"\",\"hasChilds\":1,\"childs\":[]},";
    json += "{\"id\":\"2\",\"title\":\"测试2\",\"ico\":\"\",\"link\":\"\",\"hasChilds\":1,\"childs\":[]},";
    json += "{\"id\":\"3\",\"title\":\"测试3\",\"ico\":\"\",\"link\":\"\",\"hasChilds\":1,\"childs\":[]}";
    json += "]";
    Response.Write(json);
%>