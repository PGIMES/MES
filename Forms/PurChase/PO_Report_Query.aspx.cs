using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Maticsoft.DBUtility;
using System.Text;
using DevExpress.Web;
using System.Drawing;
using System.Data.SqlClient;
using Maticsoft.DBUtility;
using System.Configuration;
using Pgi.Auto;

public partial class Forms_PurChase_PO_Report_Query : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        LoginUser LogUserModel = InitUser.GetLoginUserInfo("", Request.ServerVariables["LOGON_USER"]);
        ViewState["empname"] = LogUserModel.UserName;
        if (Session["empid"] == null)
        {
            InitUser.GetLoginUserInfo(Page, Request.ServerVariables["LOGON_USER"]);
        }
        if (!IsPostBack)
        {   
            //初始化日期           
            txtDateFrom.Text = DateTime.Now.AddDays(-30).ToString("yyyy-MM-dd");
            txtDateTo.Text = DateTime.Now.ToString("yyyy-MM-dd");           
        }
        QueryASPxGridView();
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        QueryASPxGridView();
    }

    public void QueryASPxGridView()
    {
        DataTable dt = DbHelperSQL.Query("exec Pur_PO_Query_New '" + drop_type.SelectedValue + "', '" + txtDateFrom.Text + "','" + txtDateTo.Text 
            + "','" + (string)ViewState["empname"] + "','" + txtUserFor.Text + "'").Tables[0];
        this.GV_PART.Columns.Clear();

        string form_div = "";
        if (drop_type.SelectedValue == "PO")
        {
            form_div = "Query_PO";
        }
        if (drop_type.SelectedValue == "合同")
        {
            form_div = "Query_HT";
        }
        //Pgi.Auto.Control.SetGrid("Pur_PO_Query", form_div, this.GV_PART, dt);
        SetGrid_new("Pur_PO_Query", form_div, this.GV_PART, dt);
    }

    protected void GV_PART_HtmlRowCreated(object sender, ASPxGridViewTableRowEventArgs e)
    {

        //if (e.RowType == GridViewRowType.Data)
        //{
        //    if (this.GV_PART.DataColumns["PoNo"] != null)
        //    {
        //        int index = ((DevExpress.Web.GridViewDataHyperLinkColumn)this.GV_PART.DataColumns["PoNo"]).VisibleIndex;
        //        string PoNo = Convert.ToString(e.GetValue("PoNo"));
        //        string groupid = Convert.ToString(e.GetValue("GroupID"));
        //        string stepid = Convert.ToString(e.GetValue("StepID"));
        //        //e.Row.Cells[index].Text = "<a href='http://172.16.5.26:8030/Forms/MaterialBase/ToolKnife.aspx?instanceid=" + e.GetValue("wlh") + "&domain=" + site + "' target='_blank'>" + value.ToString() + "</a>";
        //        e.Row.Cells[index].Text = "<a href='/Platform/WorkFlowRun/Default.aspx?flowid=ce701853-e13b-4c39-9cd6-b97e18656d31&appid=7d6cf334-0227-4fcd-9faf-c2536d10cf8e&instanceid="
        //            + e.GetValue("PoNo") + "&stepid=" + stepid + "&groupid=" + groupid + "&display=1' target='_blank'>" + PoNo.ToString() + "</a>";
        //    }
        //}

        if (e.RowType != GridViewRowType.Data)
        {
            return;
        }

        int pono_index = 0; int isprint_index = 0;
        for (int i = 0; i < this.GV_PART.DataColumns.Count; i++)
        {
            if (this.GV_PART.DataColumns[i].FieldName == "PoNo")
            {
                pono_index = i;
            }
            if (this.GV_PART.DataColumns[i].FieldName == "IsPrint")
            {
                isprint_index = i;
            }
        }

        string PoNo = Convert.ToString(e.GetValue("PoNo"));
        string groupid = Convert.ToString(e.GetValue("GroupID"));
        string stepid = Convert.ToString(e.GetValue("StepID"));
        e.Row.Cells[pono_index].Text = "<a href='/Platform/WorkFlowRun/Default.aspx?flowid=ce701853-e13b-4c39-9cd6-b97e18656d31&appid=7d6cf334-0227-4fcd-9faf-c2536d10cf8e&instanceid="
                    + e.GetValue("PoNo") + "&stepid=" + stepid + "&groupid=" + groupid + "&display=1' target='_blank'>" + PoNo.ToString() + "</a>";

        e.Row.Cells[isprint_index].Style.Add("color", "blue");
        e.Row.Cells[isprint_index].Attributes.Add("onclick", "show_his('" + PoNo + "')");

    }
    protected void GV_PART_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
    {
        if (e.RowType != GridViewRowType.Data) return;

        DateTime plan_date = Convert.ToDateTime(e.GetValue("PlanReceiveDate").ToString());
        DateTime deliveryDate = Convert.ToDateTime(e.GetValue("deliveryDate").ToString());
        string tr_effdate = e.GetValue("tr_effdate").ToString();
        TimeSpan ts = plan_date - deliveryDate;
        int minutes = Convert.ToInt32(e.GetValue("TOPTime").ToString());

        int jh = 0, sj = 0, top1 = 0;
        if (drop_type.SelectedValue == "PO")
        {
            jh = 23;//计划到货期
            sj = 26;//实际到货日期
            top1 = 25;//TOP1时间
        }
        else
        {
            jh = 18;//计划到货期
            sj = 21;//实际到货日期
            top1 = 20;//TOP1时间
        }

        if (tr_effdate == "" && ts.Days > 3)
        {
            e.Row.Cells[jh].Style.Add("background-color", "red");//计划到货期
        }
        if (tr_effdate != "")
        {
            if (drop_type.SelectedValue == "PO")
            {
                DateTime tr_eff = Convert.ToDateTime(tr_effdate);
                TimeSpan tsday = tr_eff - plan_date;
                if (tsday.Days > 3)
                {
                    e.Row.Cells[sj].Style.Add("background-color", "red");//实际到货日期
                }
            }
            else
            {
                string s1 = "";

                tr_effdate = tr_effdate.Replace("<br>", ",");
                string[] ss = tr_effdate.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < ss.Length; i++)
                {
                    if (ss[i].Replace("数量1 ", "") != "")
                    {
                        DateTime tr_eff = Convert.ToDateTime(ss[i].Replace("数量1 ", ""));
                        TimeSpan tsday = tr_eff - plan_date;

                        if (tsday.Days > 3)
                        {
                            s1 += "数量1 <font color=red>" + ss[i].Replace("数量1 ", "") + "</font><br>";
                        }
                        else
                        {
                            s1 += ss[i] + "<br>";
                        }
                    }
                    else
                    {
                        s1 += ss[i] + "<br>";
                    }

                }
                e.Row.Cells[sj].Text = s1;
            }
        }
        if (minutes > 24 * 60)
        {
            e.Row.Cells[top1].Style.Add("background-color", "yellow");//TOP1时间
        }

    }
    protected void GV_PART_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e)
    {
        if (e.DataColumn.Caption == "编号")
        {
            if (Convert.ToInt16(ViewState["i"]) == 0)
            {
                ViewState["i"] = 1;
            }
            int i = Convert.ToInt16(ViewState["i"]);
            e.Cell.Text = i.ToString();
            i++;
            ViewState["i"] = i;
        }
        
    }
    protected void Bt_Export_Click(object sender, EventArgs e)
    {
        QueryASPxGridView();
        GV_PART.ExportXlsxToResponse("采购单" + System.DateTime.Now.ToString("yyyyMMdd"), new DevExpress.XtraPrinting.XlsxExportOptionsEx { ExportType = DevExpress.Export.ExportType.WYSIWYG });//导出到Excel

    }

    protected void GV_PART_PageIndexChanged(object sender, EventArgs e)
    {
        QueryASPxGridView();
        ScriptManager.RegisterStartupScript(this, e.GetType(), "", "setHeight() ;", true);
    }


    public  void SetGrid_new(string lsform_type, string lsform_div, DevExpress.Web.ASPxGridView lgrid, DataTable ldt_data, int b_flag = 0)
    {
        if (ldt_data == null)
        {
            return;
        }
        lgrid.AutoGenerateColumns = false;
        int lnwidth = 0;

        //判断是否增加选择列
        if (b_flag == 1)
        {
            if (lgrid.Columns.Count == 0)
            {
                DevExpress.Web.GridViewCommandColumn lcolumn1 = new DevExpress.Web.GridViewCommandColumn();
                DevExpress.Web.GridViewCommandColumnCustomButton lbtn = new DevExpress.Web.GridViewCommandColumnCustomButton();
                lcolumn1.Caption = "选择";
                lcolumn1.Name = "Sel";
                lcolumn1.Width = 50;
                lbtn.ID = "btnid";
                lbtn.Text = "选择";
                lcolumn1.CustomButtons.Add(lbtn);
                lgrid.Columns.Insert(0, lcolumn1);
                lnwidth += 50;

            }


        }
        else if (b_flag == 2)
        {
            if (lgrid.Columns.Count == 0)
            {
                DevExpress.Web.GridViewCommandColumn lcolumn1 = new DevExpress.Web.GridViewCommandColumn();
                lcolumn1.SelectAllCheckboxMode = DevExpress.Web.GridViewSelectAllCheckBoxMode.Page;
                lcolumn1.ShowClearFilterButton = true;
                lcolumn1.ShowSelectCheckbox = true;

                lcolumn1.Name = "Sel";
                lcolumn1.Width = 50;
                lgrid.Columns.Add(lcolumn1);
                lnwidth += 50;

            }
        }


        string lsconn = ConfigurationSettings.AppSettings["ConnectionMES"];
        string lswhere = "";
        if (lsform_type != "")
        {
            lswhere += " and form_type=@form_type";
        }
        if (lsform_div != "")
        {
            lswhere += " and form_div=@form_div";
        }
        DataTable ldt = Pgi.Auto.SQLHelper.ExecuteDataSet(lsconn, CommandType.Text, "select * from Auto_Form where  list_fieldname<>'' " + lswhere + " order by list_order",
            new SqlParameter[]{
              new SqlParameter("@form_type",lsform_type)
                ,new SqlParameter("@form_div",lsform_div)}).Tables[0];


        for (int j = 0; j < ldt.Rows.Count; j++)
        {
            int lnflag = 0;
            int lnflag_width = 0;
            for (int i = 0; i < lgrid.DataColumns.Count; i++)
            {
                //判断GV列是否存在
                if (lgrid.DataColumns[i].FieldName == ldt.Rows[j]["list_fieldname"].ToString())
                {
                    lnflag = 1;
                    lnflag_width = Convert.ToInt32(lgrid.AllColumns[i].Width.Value);
                    continue;
                }
            }
            if (lnflag == 0)
            {

                //不存在添加
                for (int i = 0; i < ldt_data.Columns.Count; i++)
                {

                    if (ldt.Rows[j]["list_fieldname"].ToString().Trim() == ldt_data.Columns[i].ColumnName.ToString().Trim())
                    {

                        if (ldt.Rows[j]["list_link"].ToString() != "" && ldt.Rows[j]["list_type"].ToString() == "")
                        {

                            //DevExpress.Web.GridViewDataTextColumn lcolumn = new DevExpress.Web.GridViewDataTextColumn();
                            //HyperLinkField hl = new HyperLinkField();
                            //lcolumn.
                            //lcolumn.DataItemTemplate = hl;


                            DevExpress.Web.GridViewDataHyperLinkColumn lcolumn = new DevExpress.Web.GridViewDataHyperLinkColumn();
                            lcolumn.Caption = ldt.Rows[j]["list_caption"].ToString();
                            lcolumn.Name = ldt.Rows[j]["list_fieldname"].ToString();
                            lcolumn.FieldName = ldt.Rows[j]["list_link_fields"].ToString();
                            lcolumn.HeaderStyle.Wrap = DevExpress.Utils.DefaultBoolean.True;
                            lcolumn.CellStyle.Wrap = DevExpress.Utils.DefaultBoolean.True;
                            lcolumn.Width = Convert.ToInt32(ldt.Rows[j]["list_width"].ToString());
                            lcolumn.ExportWidth = Convert.ToInt32(ldt.Rows[j]["list_width"].ToString());
                            lcolumn.PropertiesHyperLinkEdit.NavigateUrlFormatString = ldt.Rows[j]["list_link"].ToString();
                            lcolumn.PropertiesHyperLinkEdit.TextField = ldt.Rows[j]["list_fieldname"].ToString();
                            lcolumn.PropertiesHyperLinkEdit.Target = "_blank";
                            if (ldt.Rows[j]["list_sortorder"].ToString() == "")
                            {
                                lcolumn.SortOrder = DevExpress.Data.ColumnSortOrder.None;

                            }
                            else if (ldt.Rows[j]["list_sortorder"].ToString().ToUpper() == "DESCENDING")
                            {
                                lcolumn.SortOrder = DevExpress.Data.ColumnSortOrder.Descending;
                            }
                            else
                            {
                                lcolumn.SortOrder = DevExpress.Data.ColumnSortOrder.Ascending;
                            }

                            //对齐方式
                            if (ldt.Rows[j]["list_style_align"].ToString().ToUpper() == "RIGHT")
                            {
                                lcolumn.CellStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Right;
                            }
                            else if (ldt.Rows[j]["list_style_align"].ToString().ToUpper() == "CENTER")
                            {
                                lcolumn.CellStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Center;
                            }
                            else if (ldt.Rows[j]["list_style_align"].ToString().ToUpper() == "LEFT")
                            {
                                lcolumn.CellStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Left;
                            }
                            else
                            {
                                lcolumn.CellStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.NotSet;
                            }


                            if (ldt.Rows[j]["list_width"].ToString() == "0")
                            {
                                lcolumn.HeaderStyle.CssClass = "hidden";
                                lcolumn.CellStyle.CssClass = "hidden";
                                lcolumn.FooterCellStyle.CssClass = "hidden";
                                //lcolumn.Width = 100;
                            }
                            //设置查询
                            lcolumn.Settings.AutoFilterCondition = DevExpress.Web.AutoFilterCondition.Contains;
                            //显示格式
                            if (ldt.Rows[j]["list_fieldname_format"].ToString() != "")
                            {
                                lcolumn.PropertiesEdit.DisplayFormatString = ldt.Rows[j]["list_fieldname_format"].ToString();
                                lcolumn.Settings.SortMode = DevExpress.XtraGrid.ColumnSortMode.Value;
                            }

                            //设置ToolTip
                            if (ldt.Rows[j]["list_column_tooltip"].ToString() != "")
                            {
                                lcolumn.ToolTip = ldt.Rows[j]["list_column_tooltip"].ToString();
                            }

                            lgrid.Columns.Add(lcolumn);

                            //lcolumn.PropertiesHyperLinkEdit.TextFormatString = "00,11";
                        }
                        else
                        {
                            DevExpress.Web.GridViewDataTextColumn lcolumn = new DevExpress.Web.GridViewDataTextColumn();
                            if (ldt.Rows[j]["list_type"].ToString() != "")
                            {
                                //自定义显示grid列类型
                                lcolumn.DataItemTemplate = new ColumnTemplate(ldt.Rows[j]);
                            }
                            lcolumn.Name = ldt.Rows[j]["list_fieldname"].ToString();
                            lcolumn.Caption = ldt.Rows[j]["list_caption"].ToString();
                            lcolumn.FieldName = ldt.Rows[j]["list_fieldname"].ToString();
                            lcolumn.HeaderStyle.Wrap = DevExpress.Utils.DefaultBoolean.True;
                            lcolumn.CellStyle.Wrap = DevExpress.Utils.DefaultBoolean.True;
                            lcolumn.Width = Convert.ToInt32(ldt.Rows[j]["list_width"].ToString());
                            lcolumn.ExportWidth = Convert.ToInt32(ldt.Rows[j]["list_width"].ToString());
                            if (ldt.Rows[j]["list_fieldname"].ToString() == "tr_effdate")
                            {
                                lcolumn.PropertiesTextEdit.EncodeHtml = false;
                            }
                            //默认排序
                            if (ldt.Rows[j]["list_sortorder"].ToString() == "")
                            {
                                lcolumn.SortOrder = DevExpress.Data.ColumnSortOrder.None;

                            }
                            else if (ldt.Rows[j]["list_sortorder"].ToString().ToUpper() == "DESCENDING")
                            {
                                lcolumn.SortOrder = DevExpress.Data.ColumnSortOrder.Descending;
                            }
                            else
                            {
                                lcolumn.SortOrder = DevExpress.Data.ColumnSortOrder.Ascending;
                            }

                            //对齐方式
                            if (ldt.Rows[j]["list_style_align"].ToString().ToUpper() == "RIGHT")
                            {
                                lcolumn.CellStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Right;
                            }
                            else if (ldt.Rows[j]["list_style_align"].ToString().ToUpper() == "CENTER")
                            {
                                lcolumn.CellStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Center;
                            }
                            else if (ldt.Rows[j]["list_style_align"].ToString().ToUpper() == "LEFT")
                            {
                                lcolumn.CellStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Left;
                            }
                            else
                            {
                                lcolumn.CellStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.NotSet;
                            }

                            if (ldt.Rows[j]["list_width"].ToString() == "0")
                            {
                                lcolumn.HeaderStyle.CssClass = "hidden";
                                lcolumn.CellStyle.CssClass = "hidden";
                                lcolumn.FooterCellStyle.CssClass = "hidden";
                                if (ldt.Rows[j]["list_type_ref"].ToString() != "HIDDEN")
                                {
                                    lcolumn.Visible = false;
                                }

                                // lcolumn.Width = 100;
                            }

                            //设置查询
                            lcolumn.Settings.AutoFilterCondition = DevExpress.Web.AutoFilterCondition.Contains;

                            //显示格式
                            if (ldt.Rows[j]["list_fieldname_format"].ToString() != "")
                            {
                                lcolumn.PropertiesEdit.DisplayFormatString = ldt.Rows[j]["list_fieldname_format"].ToString();
                                lcolumn.Settings.SortMode = DevExpress.XtraGrid.ColumnSortMode.Value;
                            }

                            //设置ToolTip
                            if (ldt.Rows[j]["list_column_tooltip"].ToString() != "")
                            {
                                lcolumn.ToolTip = ldt.Rows[j]["list_column_tooltip"].ToString();
                            }
                            lgrid.Columns.Add(lcolumn);
                        }



                        lnwidth += Convert.ToInt32(ldt.Rows[j]["list_width"].ToString());


                    }

                }
            }
            else
            {


                if (ldt.Rows[j]["list_type"].ToString() == "HYPERLINK")
                {
                    DevExpress.Web.GridViewDataTextColumn lcolumn = (DevExpress.Web.GridViewDataTextColumn)lgrid.Columns[j];
                    //自定义显示grid列类型
                    lcolumn.DataItemTemplate = new ColumnTemplate(ldt.Rows[j]);

                }
                lnwidth += lnflag_width;
            }

        }




        lgrid.Width = lnwidth;
        lgrid.DataSource = ldt_data;
        lgrid.DataBind();



    }


    protected void GV_PART_ExportRenderBrick(object sender, ASPxGridViewExportRenderingEventArgs e)
    {
        if (e.RowType == GridViewRowType.Data)
        {
            if (e.Column.Caption == "实际到货日期")
            {
                if (e.Value != DBNull.Value)
                {
                    e.TextValue = Convert.ToString(e.Value).Replace("<font color=red>","").Replace("</font>", "").Replace("<br>", "\r\n");
                }
            }

        }
    }
}