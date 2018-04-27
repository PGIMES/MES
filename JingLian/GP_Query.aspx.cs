using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using System.Data.SqlClient;

public partial class JingLian_GP_Query : System.Web.UI.Page
{
    Function_Jinglian Function_Jinglian = new Function_Jinglian();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            int year = Convert.ToInt16(DateTime.Now.ToString("yyyy"));
            for (int i = 0; i < 5; i++)
            {
                txt_year.Items.Add(new ListItem((year - i).ToString(), (year - i).ToString()));
            }
            for (int i = 1; i <= 12; i++)
            {
                this.txt_month.Items.Add(new ListItem(i.ToString() + "月", i.ToString()));
            }
            txt_month.SelectedValue = DateTime.Now.Month.ToString();
            txt_startdate.Text = DateTime.Now.ToString("yyyy/MM/dd");
            txt_enddate.Text = DateTime.Now.ToString("yyyy/MM/dd");
            string time = DateTime.Now.ToString("HHmm");
            string banbie = "0";

            if (string.Compare(time, "0800") > 0 && string.Compare(time, "2000") < 0)
            {
                banbie = "白班";
            }
            else
            { banbie = "晚班"; }
            txt_banbie.SelectedValue = banbie;
            DataTable dt = Function_Jinglian.GPQuery(1, txt_year.Text, txt_month.Text, txt_startdate.Text, txt_enddate.Text, txt_dh.Text, ddl_source.Text, "", "", ddl_hejin.Text, "", txt_banbie.Text);
            gv1.DataSource = dt;
            gv1.DataBind();
            divchart.Style.Add("display","none");
           // SetMap(ddl_hejin.Text);
        }
    }
    protected void btn_query_Click(object sender, EventArgs e)
    {
       
        DataTable dt = Function_Jinglian.GPQuery(1, txt_year.Text, txt_month.Text, txt_startdate.Text, txt_enddate.Text, txt_dh.Text, ddl_source.Text, "", "", ddl_hejin.Text, "", txt_banbie.Text);
        gv1.DataSource = dt;
        gv1.DataBind();
        divchart.Style.Add("display", "none");
       // SetMap(ddl_hejin.Text);
    
    }
    protected void gv1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            if (e.Row.RowIndex != -1)
            {
                int indexID = this.gv1.PageIndex * this.gv1.PageSize + e.Row.RowIndex + 1;
                e.Row.Cells[0].Text = indexID.ToString();
            }

            for (int i = 0; i < 14; i++)
            {
                if (e.Row.Cells[23 + i].Text.ToUpper() == "RED")
                {

                    e.Row.Cells[9 + i].BackColor = System.Drawing.Color.Red;
                }
                if (e.Row.Cells[23 + i].Text.ToUpper() == "YELLOW")
                {
                    e.Row.Cells[9 + i].BackColor = System.Drawing.Color.Yellow;
                }
                if (e.Row.Cells[23 + i].Text.ToUpper() == "GREEN")
                {
                    e.Row.Cells[9 + i].BackColor = System.Drawing.Color.Green;
                }

            }


        }
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Default.aspx");
    }
    private void SetMap(string hejin)
    {

       // DataTable hj_dt = Function_Jinglian.GPList_Query(7, "", "", "", "", "", "", "", "", ddl_hejin.Text,"");
       
        if (this.ddl_hejin.Text == "A380")
        {
            this.C1.ChartAreas[0].AxisY.Minimum = 7.5;
            this.C1.ChartAreas[0].AxisY.Maximum = 10;
            this.C1.ChartAreas[0].AxisY.StripLines[0].IntervalOffset = 8.01;
            this.C1.ChartAreas[0].AxisY.StripLines[0].Text = "8.01";
            this.C1.ChartAreas[0].AxisY.StripLines[0].ToolTip = "8.01";


            this.C1.ChartAreas[0].AxisY.StripLines[1].IntervalOffset = 8.19;
            this.C1.ChartAreas[0].AxisY.StripLines[1].Text = "8.19";
            this.C1.ChartAreas[0].AxisY.StripLines[1].ToolTip = "8.19";

            this.C1.ChartAreas[0].AxisY.StripLines[2].IntervalOffset = 9.3;
            this.C1.ChartAreas[0].AxisY.StripLines[2].Text = "9.31";
            this.C1.ChartAreas[0].AxisY.StripLines[2].ToolTip = "9.31";

            this.C1.ChartAreas[0].AxisY.StripLines[3].IntervalOffset = 9.49;
            this.C1.ChartAreas[0].AxisY.StripLines[3].Text = "9.49";
            this.C1.ChartAreas[0].AxisY.StripLines[3].ToolTip = "9.49";

            this.C2.ChartAreas[0].AxisY.Minimum = 0.7;
            this.C2.ChartAreas[0].AxisY.Maximum = 1.1;
            this.C2.ChartAreas[0].AxisY.StripLines[0].IntervalOffset = 0.801;
            this.C2.ChartAreas[0].AxisY.StripLines[0].Text = "0.801";
            this.C2.ChartAreas[0].AxisY.StripLines[0].ToolTip = "0.801";

            this.C2.ChartAreas[0].AxisY.StripLines[1].IntervalOffset = 0.809;
            this.C2.ChartAreas[0].AxisY.StripLines[1].Text = "0.809";
            this.C2.ChartAreas[0].AxisY.StripLines[1].ToolTip = "0.809";

            this.C2.ChartAreas[0].AxisY.StripLines[2].IntervalOffset = 0.951;
            this.C2.ChartAreas[0].AxisY.StripLines[2].Text = "0.951";
            this.C2.ChartAreas[0].AxisY.StripLines[2].ToolTip = "0.951";

            this.C2.ChartAreas[0].AxisY.StripLines[3].IntervalOffset = 0.999;
            this.C2.ChartAreas[0].AxisY.StripLines[3].Text = "0.999";
            this.C2.ChartAreas[0].AxisY.StripLines[3].ToolTip = "0.999";

            this.C3.ChartAreas[0].AxisY.Minimum = 3.1;
            this.C3.ChartAreas[0].AxisY.Maximum = 4.1;
            this.C3.ChartAreas[0].AxisY.StripLines[0].IntervalOffset = 3.26;
            this.C3.ChartAreas[0].AxisY.StripLines[0].Text = "3.26";
            this.C3.ChartAreas[0].AxisY.StripLines[0].ToolTip = "3.26";

            this.C3.ChartAreas[0].AxisY.StripLines[1].IntervalOffset = 3.29;
            this.C3.ChartAreas[0].AxisY.StripLines[1].Text = "3.29";
            this.C3.ChartAreas[0].AxisY.StripLines[1].ToolTip = "3.29";

            this.C3.ChartAreas[0].AxisY.StripLines[2].IntervalOffset = 3.9;
            this.C3.ChartAreas[0].AxisY.StripLines[2].Text = "3.9";
            this.C3.ChartAreas[0].AxisY.StripLines[2].ToolTip = "3.9";

            this.C3.ChartAreas[0].AxisY.StripLines[3].IntervalOffset = 3.99;
            this.C3.ChartAreas[0].AxisY.StripLines[3].Text = "3.99";
            this.C3.ChartAreas[0].AxisY.StripLines[3].ToolTip = "3.99";

            this.C4.ChartAreas[0].AxisY.Minimum = 0;
            this.C4.ChartAreas[0].AxisY.Maximum = 0.4;
            this.C4.ChartAreas[0].AxisY.StripLines[0].IntervalOffset = 0.151;
            this.C4.ChartAreas[0].AxisY.StripLines[0].Text = "0.151";
            this.C4.ChartAreas[0].AxisY.StripLines[0].ToolTip = "0.151";

            this.C4.ChartAreas[0].AxisY.StripLines[1].IntervalOffset = 0.169;
            this.C4.ChartAreas[0].AxisY.StripLines[1].Text = "0.169";
            this.C4.ChartAreas[0].AxisY.StripLines[1].ToolTip = "0.169";

            this.C4.ChartAreas[0].AxisY.StripLines[2].IntervalOffset = 0.281;
            this.C4.ChartAreas[0].AxisY.StripLines[2].Text = "0.281";
            this.C4.ChartAreas[0].AxisY.StripLines[2].ToolTip = "0.281";

            this.C4.ChartAreas[0].AxisY.StripLines[3].IntervalOffset = 0.299;
            this.C4.ChartAreas[0].AxisY.StripLines[3].Text = "0.299";
            this.C4.ChartAreas[0].AxisY.StripLines[3].ToolTip = "0.299";


            this.C5.ChartAreas[0].AxisY.Minimum = 0;
            this.C5.ChartAreas[0].AxisY.Maximum = 2.5;
            this.C5.ChartAreas[0].AxisY.StripLines[0].IntervalOffset = 0.4;
            this.C5.ChartAreas[0].AxisY.StripLines[0].Text = "0.4";
            this.C5.ChartAreas[0].AxisY.StripLines[0].ToolTip = "0.4";

            this.C5.ChartAreas[0].AxisY.StripLines[1].IntervalOffset = 0.59;
            this.C5.ChartAreas[0].AxisY.StripLines[1].Text = "0.59";
            this.C5.ChartAreas[0].AxisY.StripLines[1].ToolTip = "0.59";

            this.C5.ChartAreas[0].AxisY.StripLines[2].IntervalOffset = 1.81;
            this.C5.ChartAreas[0].AxisY.StripLines[2].Text = "1.81";
            this.C5.ChartAreas[0].AxisY.StripLines[2].ToolTip = "1.81";

            this.C5.ChartAreas[0].AxisY.StripLines[3].IntervalOffset = 2.0;
            this.C5.ChartAreas[0].AxisY.StripLines[3].Text = "2.0";
            this.C5.ChartAreas[0].AxisY.StripLines[3].ToolTip = "2.0";

            //this.C6.ChartAreas[0].AxisY.Minimum = 0;
            //this.C6.ChartAreas[0].AxisY.Maximum = 0.2;
            //this.C6.ChartAreas[0].AxisY.StripLines[0].IntervalOffset = 0.1;
            //this.C6.ChartAreas[0].AxisY.StripLines[0].Text = "0.1";
            //this.C6.ChartAreas[0].AxisY.StripLines[0].ToolTip = "0.1";

            //this.C6.ChartAreas[0].AxisY.StripLines[1].IntervalOffset = 0.2;
            //this.C6.ChartAreas[0].AxisY.StripLines[1].Text = "0.2";
            //this.C6.ChartAreas[0].AxisY.StripLines[1].ToolTip = "0.2";



        }
        else if (this.ddl_hejin.Text == "EN46000")
        {
            this.C1.ChartAreas[0].AxisY.Minimum = 7;
            this.C1.ChartAreas[0].AxisY.Maximum = 11;
            this.C1.ChartAreas[0].AxisY.StripLines[0].IntervalOffset = 8.01;
            this.C1.ChartAreas[0].AxisY.StripLines[0].Text = "8.01";
            this.C1.ChartAreas[0].AxisY.StripLines[0].ToolTip = "8.01";

            this.C1.ChartAreas[0].AxisY.StripLines[1].IntervalOffset = 8.74;
            this.C1.ChartAreas[0].AxisY.StripLines[1].Text = "8.74";
            this.C1.ChartAreas[0].AxisY.StripLines[1].ToolTip = "8.74";

            this.C1.ChartAreas[0].AxisY.StripLines[2].IntervalOffset = 10.26;
            this.C1.ChartAreas[0].AxisY.StripLines[2].Text = "10.26";
            this.C1.ChartAreas[0].AxisY.StripLines[2].ToolTip = "10.26";

            this.C1.ChartAreas[0].AxisY.StripLines[3].IntervalOffset = 10.99;
            this.C1.ChartAreas[0].AxisY.StripLines[3].Text = "10.99";
            this.C1.ChartAreas[0].AxisY.StripLines[3].ToolTip = "10.99";

            this.C2.ChartAreas[0].AxisY.Minimum = 0.4;
            this.C2.ChartAreas[0].AxisY.Maximum = 1.4;
            this.C2.ChartAreas[0].AxisY.StripLines[0].IntervalOffset = 0.601;
            this.C2.ChartAreas[0].AxisY.StripLines[0].Text = "0.601";
            this.C2.ChartAreas[0].AxisY.StripLines[0].ToolTip = "0.601";

            this.C2.ChartAreas[0].AxisY.StripLines[1].IntervalOffset = 0.699;
            this.C2.ChartAreas[0].AxisY.StripLines[1].Text = "0.699";
            this.C2.ChartAreas[0].AxisY.StripLines[1].ToolTip = "0.699";

            this.C2.ChartAreas[0].AxisY.StripLines[2].IntervalOffset = 1.101;
            this.C2.ChartAreas[0].AxisY.StripLines[2].Text = "1.101";
            this.C2.ChartAreas[0].AxisY.StripLines[2].ToolTip = "1.101";

            this.C2.ChartAreas[0].AxisY.StripLines[3].IntervalOffset = 1.3;
            this.C2.ChartAreas[0].AxisY.StripLines[3].Text = "1.3";
            this.C2.ChartAreas[0].AxisY.StripLines[3].ToolTip = "1.3";


            this.C3.ChartAreas[0].AxisY.Minimum = 1.5;
            this.C3.ChartAreas[0].AxisY.Maximum = 4.5;
            this.C3.ChartAreas[0].AxisY.StripLines[0].IntervalOffset = 2.01;
            this.C3.ChartAreas[0].AxisY.StripLines[0].Text = "2.01";
            this.C3.ChartAreas[0].AxisY.StripLines[0].ToolTip = "2.01";

            this.C3.ChartAreas[0].AxisY.StripLines[1].IntervalOffset = 2.49;
            this.C3.ChartAreas[0].AxisY.StripLines[1].Text = "2.49";
            this.C3.ChartAreas[0].AxisY.StripLines[1].ToolTip = "2.49";

            this.C3.ChartAreas[0].AxisY.StripLines[2].IntervalOffset = 3.51;
            this.C3.ChartAreas[0].AxisY.StripLines[2].Text = "3.51";
            this.C3.ChartAreas[0].AxisY.StripLines[2].ToolTip = "3.51";

            this.C3.ChartAreas[0].AxisY.StripLines[3].IntervalOffset = 3.99;
            this.C3.ChartAreas[0].AxisY.StripLines[3].Text = "3.99";
            this.C3.ChartAreas[0].AxisY.StripLines[3].ToolTip = "3.99";

            this.C4.ChartAreas[0].AxisY.Minimum = 0;
            this.C4.ChartAreas[0].AxisY.Maximum = 0.7;
            this.C4.ChartAreas[0].AxisY.StripLines[0].IntervalOffset = 0.051;
            this.C4.ChartAreas[0].AxisY.StripLines[0].Text = "0.051";
            this.C4.ChartAreas[0].AxisY.StripLines[0].ToolTip = "0.051";

            this.C4.ChartAreas[0].AxisY.StripLines[1].IntervalOffset = 0.174;
            this.C4.ChartAreas[0].AxisY.StripLines[1].Text = "0.174";
            this.C4.ChartAreas[0].AxisY.StripLines[1].ToolTip = "0.174";

            this.C4.ChartAreas[0].AxisY.StripLines[2].IntervalOffset = 0.426;
            this.C4.ChartAreas[0].AxisY.StripLines[2].Text = "0.426";
            this.C4.ChartAreas[0].AxisY.StripLines[2].ToolTip = "0.426";

            this.C4.ChartAreas[0].AxisY.StripLines[3].IntervalOffset = 0.549;
            this.C4.ChartAreas[0].AxisY.StripLines[3].Text = "0.549";
            this.C4.ChartAreas[0].AxisY.StripLines[3].ToolTip = "0.549";

            this.C5.ChartAreas[0].AxisY.Minimum = 0;
            this.C5.ChartAreas[0].AxisY.Maximum = 2.5;
            this.C5.ChartAreas[0].AxisY.StripLines[0].IntervalOffset = 0.4;
            this.C5.ChartAreas[0].AxisY.StripLines[0].Text = "0.4";
            this.C5.ChartAreas[0].AxisY.StripLines[0].ToolTip = "0.4";

            this.C5.ChartAreas[0].AxisY.StripLines[1].IntervalOffset = 0.59;
            this.C5.ChartAreas[0].AxisY.StripLines[1].Text = "0.59";
            this.C5.ChartAreas[0].AxisY.StripLines[1].ToolTip = "0.59";

            this.C5.ChartAreas[0].AxisY.StripLines[2].IntervalOffset = 1.81;
            this.C5.ChartAreas[0].AxisY.StripLines[2].Text = "1.81";
            this.C5.ChartAreas[0].AxisY.StripLines[2].ToolTip = "1.81";

            this.C5.ChartAreas[0].AxisY.StripLines[3].IntervalOffset = 2.0;
            this.C5.ChartAreas[0].AxisY.StripLines[3].Text = "2.0";
            this.C5.ChartAreas[0].AxisY.StripLines[3].ToolTip = "2.0";


        }
        else if (this.ddl_hejin.Text == "EN47100")
        {
            this.C1.ChartAreas[0].AxisY.Minimum = 10.5;
            this.C1.ChartAreas[0].AxisY.Maximum = 13.5;


            this.C1.ChartAreas[0].AxisY.StripLines[1].IntervalOffset = 10.8;
            this.C1.ChartAreas[0].AxisY.StripLines[1].Text = "10.8";
            this.C1.ChartAreas[0].AxisY.StripLines[1].ToolTip = "10.8";

            this.C1.ChartAreas[0].AxisY.StripLines[2].IntervalOffset = 13.2;
            this.C1.ChartAreas[0].AxisY.StripLines[2].Text = "13.2";
            this.C1.ChartAreas[0].AxisY.StripLines[2].ToolTip = "13.2";


            this.C2.ChartAreas[0].AxisY.Minimum = 0.5;
            this.C2.ChartAreas[0].AxisY.Maximum = 1.2;

            this.C2.ChartAreas[0].AxisY.StripLines[0].IntervalOffset = 0;
            this.C2.ChartAreas[0].AxisY.StripLines[0].Text = "";
            this.C2.ChartAreas[0].AxisY.StripLines[0].ToolTip = "";

            this.C2.ChartAreas[0].AxisY.StripLines[1].IntervalOffset = 0.7;
            this.C2.ChartAreas[0].AxisY.StripLines[1].Text = "0.7";
            this.C2.ChartAreas[0].AxisY.StripLines[1].ToolTip = "0.7";

            this.C2.ChartAreas[0].AxisY.StripLines[2].IntervalOffset = 1.1;
            this.C2.ChartAreas[0].AxisY.StripLines[2].Text = "1.1";
            this.C2.ChartAreas[0].AxisY.StripLines[2].ToolTip = "1.1";

            this.C2.ChartAreas[0].AxisY.StripLines[3].IntervalOffset = 0;
            this.C2.ChartAreas[0].AxisY.StripLines[3].Text = "";
            this.C2.ChartAreas[0].AxisY.StripLines[3].ToolTip = "";

            this.C3.ChartAreas[0].AxisY.Minimum = 0.5;
            this.C3.ChartAreas[0].AxisY.Maximum = 2;
            this.C3.ChartAreas[0].AxisY.StripLines[0].IntervalOffset = 0;
            this.C3.ChartAreas[0].AxisY.StripLines[0].Text = "";
            this.C3.ChartAreas[0].AxisY.StripLines[0].ToolTip = "";

            this.C3.ChartAreas[0].AxisY.StripLines[1].IntervalOffset = 0.75;
            this.C3.ChartAreas[0].AxisY.StripLines[1].Text = "0.75";
            this.C3.ChartAreas[0].AxisY.StripLines[1].ToolTip = "0.75";

            this.C3.ChartAreas[0].AxisY.StripLines[2].IntervalOffset = 1.15;
            this.C3.ChartAreas[0].AxisY.StripLines[2].Text = "1.15";
            this.C3.ChartAreas[0].AxisY.StripLines[2].ToolTip = "1.15";

            this.C3.ChartAreas[0].AxisY.StripLines[3].IntervalOffset = 0;
            this.C3.ChartAreas[0].AxisY.StripLines[3].Text = "";
            this.C3.ChartAreas[0].AxisY.StripLines[3].ToolTip = "";

            this.C4.ChartAreas[0].AxisY.Minimum = 0.1;
            this.C4.ChartAreas[0].AxisY.Maximum = 1;
            this.C4.ChartAreas[0].AxisY.StripLines[0].IntervalOffset = 0;
            this.C4.ChartAreas[0].AxisY.StripLines[0].Text = "0";
            this.C4.ChartAreas[0].AxisY.StripLines[0].ToolTip = "0";

            this.C4.ChartAreas[0].AxisY.StripLines[1].IntervalOffset = 0.549;
            this.C4.ChartAreas[0].AxisY.StripLines[1].Text = "0.549";
            this.C4.ChartAreas[0].AxisY.StripLines[1].ToolTip = "0.549";

            this.C4.ChartAreas[0].AxisY.StripLines[2].IntervalOffset = 0;
            this.C4.ChartAreas[0].AxisY.StripLines[2].Text = "";
            this.C4.ChartAreas[0].AxisY.StripLines[2].ToolTip = "";

            this.C4.ChartAreas[0].AxisY.StripLines[3].IntervalOffset = 0;
            this.C4.ChartAreas[0].AxisY.StripLines[3].Text = "";
            this.C4.ChartAreas[0].AxisY.StripLines[3].ToolTip = "";

            this.C5.Visible = false;


        }

        else if (this.ddl_hejin.Text == "ADC12")
        {
            this.C1.ChartAreas[0].AxisY.Minimum = 9.5;
            this.C1.ChartAreas[0].AxisY.Maximum = 13;
            this.C1.ChartAreas[0].AxisY.StripLines[0].IntervalOffset = 9.61;
            this.C1.ChartAreas[0].AxisY.StripLines[0].Text = "9.61";
            this.C1.ChartAreas[0].AxisY.StripLines[0].ToolTip = "9.61";


            this.C1.ChartAreas[0].AxisY.StripLines[1].IntervalOffset = 10.39;
            this.C1.ChartAreas[0].AxisY.StripLines[1].Text = "10.39";
            this.C1.ChartAreas[0].AxisY.StripLines[1].ToolTip = "10.39";

            this.C1.ChartAreas[0].AxisY.StripLines[2].IntervalOffset = 11.2;
            this.C1.ChartAreas[0].AxisY.StripLines[2].Text = "11.2";
            this.C1.ChartAreas[0].AxisY.StripLines[2].ToolTip = "11.2";

            this.C1.ChartAreas[0].AxisY.StripLines[3].IntervalOffset = 11.99;
            this.C1.ChartAreas[0].AxisY.StripLines[3].Text = "11.99";
            this.C1.ChartAreas[0].AxisY.StripLines[3].ToolTip = "11.99";

            this.C2.ChartAreas[0].AxisY.Minimum = 0.5;
            this.C2.ChartAreas[0].AxisY.Maximum = 1.5;
            this.C2.ChartAreas[0].AxisY.StripLines[0].IntervalOffset = 0.601;
            this.C2.ChartAreas[0].AxisY.StripLines[0].Text = "0.601";
            this.C2.ChartAreas[0].AxisY.StripLines[0].ToolTip = "0.601";

            this.C2.ChartAreas[0].AxisY.StripLines[1].IntervalOffset = 0.699;
            this.C2.ChartAreas[0].AxisY.StripLines[1].Text = "0.699";
            this.C2.ChartAreas[0].AxisY.StripLines[1].ToolTip = "0.699";

            this.C2.ChartAreas[0].AxisY.StripLines[2].IntervalOffset = 1.1;
            this.C2.ChartAreas[0].AxisY.StripLines[2].Text = "1.1";
            this.C2.ChartAreas[0].AxisY.StripLines[2].ToolTip = "1.1";

            this.C2.ChartAreas[0].AxisY.StripLines[3].IntervalOffset = 1.101;
            this.C2.ChartAreas[0].AxisY.StripLines[3].Text = "1.101";
            this.C2.ChartAreas[0].AxisY.StripLines[3].ToolTip = "1.101";

            this.C3.ChartAreas[0].AxisY.Minimum = 2;
            this.C3.ChartAreas[0].AxisY.Maximum = 5;
            this.C3.ChartAreas[0].AxisY.StripLines[0].IntervalOffset = 1.51;
            this.C3.ChartAreas[0].AxisY.StripLines[0].Text = "1.51";
            this.C3.ChartAreas[0].AxisY.StripLines[0].ToolTip = "1.51";

            this.C3.ChartAreas[0].AxisY.StripLines[1].IntervalOffset = 1.79;
            this.C3.ChartAreas[0].AxisY.StripLines[1].Text = "1.79";
            this.C3.ChartAreas[0].AxisY.StripLines[1].ToolTip = "1.79";

            this.C3.ChartAreas[0].AxisY.StripLines[2].IntervalOffset = 2.84;
            this.C3.ChartAreas[0].AxisY.StripLines[2].Text = "2.84";
            this.C3.ChartAreas[0].AxisY.StripLines[2].ToolTip = "2.84";

            this.C3.ChartAreas[0].AxisY.StripLines[3].IntervalOffset = 3.49;
            this.C3.ChartAreas[0].AxisY.StripLines[3].Text = "3.49";
            this.C3.ChartAreas[0].AxisY.StripLines[3].ToolTip = "3.49";

            this.C4.ChartAreas[0].AxisY.Minimum = 0.1;
            this.C4.ChartAreas[0].AxisY.Maximum = 1;
            this.C4.ChartAreas[0].AxisY.StripLines[0].IntervalOffset = 0;
            this.C4.ChartAreas[0].AxisY.StripLines[0].Text = "0";
            this.C4.ChartAreas[0].AxisY.StripLines[0].ToolTip = "0";

            this.C4.ChartAreas[0].AxisY.StripLines[1].IntervalOffset = 0.499;
            this.C4.ChartAreas[0].AxisY.StripLines[1].Text = "0.499";
            this.C4.ChartAreas[0].AxisY.StripLines[1].ToolTip = "0.499";

            this.C4.ChartAreas[0].AxisY.StripLines[2].IntervalOffset = 0.5;
            this.C4.ChartAreas[0].AxisY.StripLines[2].Text = "0.5";
            this.C4.ChartAreas[0].AxisY.StripLines[2].ToolTip = "0.5";

            this.C4.ChartAreas[0].AxisY.StripLines[3].IntervalOffset = 1;
            this.C4.ChartAreas[0].AxisY.StripLines[3].Text = "1";
            this.C4.ChartAreas[0].AxisY.StripLines[3].ToolTip = "1";


            this.C5.ChartAreas[0].AxisY.Minimum = 0.2;
            this.C5.ChartAreas[0].AxisY.Maximum = 2;
            this.C5.ChartAreas[0].AxisY.StripLines[0].IntervalOffset = 0.41;
            this.C5.ChartAreas[0].AxisY.StripLines[0].Text = "0.41";
            this.C5.ChartAreas[0].AxisY.StripLines[0].ToolTip = "0.41";

            this.C5.ChartAreas[0].AxisY.StripLines[1].IntervalOffset = 0.59;
            this.C5.ChartAreas[0].AxisY.StripLines[1].Text = "0.59";
            this.C5.ChartAreas[0].AxisY.StripLines[1].ToolTip = "0.59";

            this.C5.ChartAreas[0].AxisY.StripLines[2].IntervalOffset = 1.81;
            this.C5.ChartAreas[0].AxisY.StripLines[2].Text = "1.81";
            this.C5.ChartAreas[0].AxisY.StripLines[2].ToolTip = "1.81";

            this.C5.ChartAreas[0].AxisY.StripLines[3].IntervalOffset = 1.99;
            this.C5.ChartAreas[0].AxisY.StripLines[3].Text = "1.99";
            this.C5.ChartAreas[0].AxisY.StripLines[3].ToolTip = "1.99";




        }
      

        //图1
        //DataTable ldt = this.getDT(lsvalue, "si");
    
        DataTable ldt = Function_Jinglian.GPQuery(2, txt_year.Text, txt_month.Text, txt_startdate.Text, txt_enddate.Text, txt_dh.Text, ddl_source.Text, "", "", ddl_hejin.Text, "", txt_banbie.Text);
        this.C1.DataSource = ldt;
        this.C1.Series["Series1"].XValueMember = "xh";
        this.C1.Series["Series1"].YValueMembers = "si";
        this.C1.ChartAreas[0].AxisX.LabelStyle.Angle = 90;

        this.C1.DataBind();//绑定数据
        for (int i = 0; i < ldt.Rows.Count; i++)
        {

            this.C1.Series["Series1"].Points[i].AxisLabel = ldt.Rows[i]["xh"].ToString();

            this.C1.Series["Series1"].Points[i].ToolTip = ldt.Rows[i]["si"].ToString();

        }

        //图2
     
        this.C2.DataSource = ldt;
        this.C2.Series["Series1"].XValueMember = "xh";
        this.C2.Series["Series1"].YValueMembers = "fe";
        this.C2.ChartAreas[0].AxisX.LabelStyle.Angle = 90;

        this.C2.DataBind();//绑定数据
        for (int i = 0; i < ldt.Rows.Count; i++)
        {

            this.C2.Series["Series1"].Points[i].AxisLabel = ldt.Rows[i]["xh"].ToString();

            this.C2.Series["Series1"].Points[i].ToolTip = ldt.Rows[i]["fe"].ToString();

        }

        ////图3
        ////DataTable ldt3 = this.getDT(lsvalue, "cu");
        ////ldt3 = Getno(ldt3, lsvalue);
        this.C3.DataSource = ldt;
        this.C3.Series["Series1"].XValueMember = "xh";
        this.C3.Series["Series1"].YValueMembers = "cu";
        this.C3.ChartAreas[0].AxisX.LabelStyle.Angle = 90;

        this.C3.DataBind();//绑定数据
        for (int i = 0; i < ldt.Rows.Count; i++)
        {

            this.C3.Series["Series1"].Points[i].AxisLabel = ldt.Rows[i]["xh"].ToString();

            this.C3.Series["Series1"].Points[i].ToolTip = ldt.Rows[i]["cu"].ToString();

        }

        ////图4
        ////DataTable ldt4 = this.getDT(lsvalue, "mg");
        ////ldt4 = Getno(ldt4, lsvalue);
        this.C4.DataSource = ldt;
        this.C4.Series["Series1"].XValueMember = "xh";
        this.C4.Series["Series1"].YValueMembers = "mg";
        this.C4.ChartAreas[0].AxisX.LabelStyle.Angle = 90;

        this.C4.DataBind();//绑定数据
        for (int i = 0; i < ldt.Rows.Count; i++)
        {

            this.C4.Series["Series1"].Points[i].AxisLabel = ldt.Rows[i]["xh"].ToString();

            this.C4.Series["Series1"].Points[i].ToolTip = ldt.Rows[i]["mg"].ToString();

        }

        ////图5
        this.C5.DataSource = ldt;
        this.C5.Series["Series1"].XValueMember = "xh";
        this.C5.Series["Series1"].YValueMembers = "sf";
        this.C5.ChartAreas[0].AxisX.LabelStyle.Angle = 90;

        this.C5.DataBind();//绑定数据
        for (int i = 0; i < ldt.Rows.Count; i++)
        {

            this.C5.Series["Series1"].Points[i].AxisLabel = ldt.Rows[i]["xh"].ToString();

            this.C5.Series["Series1"].Points[i].ToolTip = ldt.Rows[i]["sf"].ToString();

        }

        //    //图6
        //    DataTable ldt6 = DAL.ZhiLiang_Report.ReportQuery1(this.txttype.Text,this.txtyear.Text,this.txtdate.Text).Tables[0];

        //    this.C6.DataSource = ldt6;
        //    this.C6.Series["Series1"].XValueMember = "n";
        //    this.C6.Series["Series1"].YValueMembers = "ResultValue";
        //    this.C6.ChartAreas[0].AxisX.LabelStyle.Angle = -45;

        //    this.C6.DataBind();//绑定数据
        //    for (int i = 0; i < ldt6.Rows.Count; i++)
        //    {

        //        this.C6.Series["Series1"].Points[i].AxisLabel = ldt6.Rows[i]["n"].ToString();

        //        this.C6.Series["Series1"].Points[i].ToolTip = ldt6.Rows[i]["ResultValue"].ToString() + "(" + ldt6.Rows[i]["n"].ToString() + ")";

        //    }
    }
}