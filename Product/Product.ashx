<%@ WebHandler Language="C#" Class="Product" %>

using System;
using System.Web;
using System.Data;
using Maticsoft.DBUtility;

public class Product : IHttpHandler {

    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";

        string year = context.Request.QueryString["year"];
        string id=context.Request.QueryString["id"];
        string result = ShowNumByYearXMH(year,id);
        context.Response.Write(result);
    }


    public bool IsReusable {
        get {
            return false;
        }
    }

    public static string ShowNumByYearXMH(string year,string id)
    {
        System.Text.StringBuilder  result = new System.Text.StringBuilder();
        result.Append("");
        result.Append("<table class='tbl' ><tr style='text-align:center;background-color:#507CD1;color:white;font-weight:bold'><td>M1</td><td>M2</td><td>M3</td><td>M4</td><td>M5</td><td>M6</td><td>M7</td><td>M8</td><td>M9</td><td>M10</td><td>M11</td><td>M12</td></tr>");
        DataSet ds = DbHelperSQL.Query("exec form3_Getnum_ByYear_XMH '"+year+"','"+id+"'");
        if (ds.Tables[0].Rows.Count > 0)
        {
            DataRow dr = ds.Tables[0].Rows[0];
            result.Append("<tr><td>"+ dr["M1"] + "</td><td>" + dr["M2"] + "</td><td>" + dr["M3"] + "</td><td>" + dr["M4"] + "</td><td>" + dr["M5"] + "</td><td>" + dr["M6"] + "</td><td>" + dr["M7"] + "</td><td>" + dr["M8"] + "</td><td>" + dr["M9"] + "</td><td>" + dr["M10"] + "</td><td>" + dr["M11"] + "</td><td>" + dr["M12"] + "</td></tr>");
        }
        result.Append( "</table>") ;
        return result.ToString();
    }



}