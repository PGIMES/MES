using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Select_select_yn : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        string lsyn = RadioButtonList1.SelectedValue;

        string temp = @"<script>parent.setvalue_IsBg('" + lsyn + "','" + Request["vi"] + "','" + Request.QueryString["ty"] + "'); var index = parent.layer.getFrameIndex(window.name);parent.layer.close(index);</script>";


        Response.Write(temp.Trim());
    }
}