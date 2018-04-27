<%@ Page Language="C#" %>
<% 
    string linkagesource = Request["linkagesource"];
    string linkagesourcetext = Request["linkagesourcetext"];
    string linkagesourceconn = Request["linkagesourceconn"];
    string value = Request["value"];

    if ("sql" == linkagesource)
    {
        Guid connID;
        if (!linkagesourceconn.IsGuid(out connID))
        {
            Response.Write("");
            Response.End();
        }
        RoadFlow.Platform.DBConnection DBConn = new RoadFlow.Platform.DBConnection();
        Response.Write(DBConn.GetOptionsFromSql(connID, linkagesourcetext));
        
    }
    else if ("dict" == linkagesource)
    {
        Response.Write(new RoadFlow.Platform.Dictionary().GetOptionsByID(value.ToGuid(), RoadFlow.Platform.Dictionary.OptionValueField.ID, "", false));
    } 
%>