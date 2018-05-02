using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Collections;

namespace Pgi.Auto
{
    public class Control
    {

        #region 在页面中显示要显示的字段
        /// <summary>
        /// 在页面中显示要显示的字段
        /// </summary>
        /// <param name="lsform_type">要显示字段大类</param>
        /// <param name="lsform_div">要显示字段小类</param>
        /// <param name="lncolumn">设置每行显示列的数量</param>
        /// <param name="lsrow_style">设置行样式</param>
        /// <param name="lscolumn_style">设置列样式</param>
        /// <param name="lscontrol_style">设置显示控件样式（默认统一）</param>
        /// <param name="ldt_head">赋值数据源（可选参数，默认抓取Datatable中第一行）</param>
        /// <returns></returns>
        public static List<TableRow> ShowControl(string lsform_type, string lsform_div, int lncolumn, string lsrow_style, string lscolumn_style, string lscontrol_style, DataTable ldt_head = null)
        {
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
            DataTable ldt = Pgi.Auto.SQLHelper.ExecuteDataSet(lsconn, CommandType.Text, "select * from auto_form where (control_id<>'' or control_id is null) " + lswhere + " order by control_order",
                new SqlParameter[]{
              new SqlParameter("@form_type",lsform_type)
                ,new SqlParameter("@form_div",lsform_div)}).Tables[0];

            List<TableRow> ls = new List<TableRow>();
            int ln = 1;
            TableRow lrow = null;
            for (int i = 0; i < ldt.Rows.Count; i++)
            {

                if ((i % lncolumn) == 0)
                {
                    lrow = new TableRow();
                    if (lsrow_style != "")
                    {
                        lrow.CssClass = lsrow_style;
                    }
                }
                if (ldt.Rows[i]["control_onlyrow"].ToString() == "1")
                {

                    ls.Add(lrow);
                    lrow = new TableRow();
                    //行样式
                    if (lsrow_style != "")
                    {
                        lrow.CssClass = lsrow_style;
                    }
                }

                //列样式
                TableCell lcellHead = new TableCell();
                if (lscolumn_style != "")
                {
                    lcellHead.CssClass = lscolumn_style;
                }
                TableCell lcellContent = new TableCell();
                if (lscolumn_style != "")
                {
                    lcellContent.CssClass = lscolumn_style;
                }
                Label lbl = new Label();
                lbl.ID = "lbl_" + lsform_type + "_" + lsform_div + "_" + ln.ToString();
                lbl.Text = ldt.Rows[i]["control_dest"].ToString();

                //-----------------------------------控件判断开始----------------------------------------
                //文本控件
                if (ldt.Rows[i]["control_type"].ToString() == "TEXTBOX")
                {
                    #region "TextBox"
                    TextBox ltxt = new TextBox();
                    ltxt.AutoPostBack = false;
                    ltxt.ID = ldt.Rows[i]["control_id"].ToString().ToLower();
                    //ToopTip
                    ltxt.ToolTip = ldt.Rows[i]["control_key"].ToString() + "|" + ldt.Rows[i]["control_empty"].ToString();
                    //是否服务器运行
                    ltxt.Attributes.Add("AutoPostBack", "false");
                    //事件
                    if (ldt.Rows[i]["control_event"].ToString() != "" && ldt.Rows[i]["control_event1"].ToString() != "")
                    {
                        ltxt.Attributes.Add(ldt.Rows[i]["control_event1"].ToString(), ldt.Rows[i]["control_event"].ToString());
                    }
                    else if (ldt.Rows[i]["control_event"].ToString() != "" && ldt.Rows[i]["control_event1"].ToString() == "")
                    {
                        ltxt.Attributes.Add("onchange", ldt.Rows[i]["control_event"].ToString());
                    }
                    //宽度
                    if (ldt.Rows[i]["control_width"].ToString() != "" && ldt.Rows[i]["control_width"].ToString() != "0")
                    {
                        ltxt.Width = Convert.ToInt32(ldt.Rows[i]["control_width"].ToString());
                    }
                    //高度
                    if (ldt.Rows[i]["control_height"].ToString() != "" && ldt.Rows[i]["control_height"].ToString() != "0")
                    {
                        ltxt.Height = Convert.ToInt32(ldt.Rows[i]["control_height"].ToString());
                    }
                    //字体
                    if (ldt.Rows[i]["control_fontsize"].ToString() != "" && ldt.Rows[i]["control_fontsize"].ToString() != "0")
                    {
                        ltxt.Font.Size = Convert.ToInt32(ldt.Rows[i]["control_fontsize"].ToString());
                    }

                    //只读
                    if (ldt.Rows[i]["control_readonly"].ToString() == "1")
                    {
                        ltxt.ReadOnly = true;
                        ltxt.Attributes.Add("contenteditable", "false");
                    }
                    //样式
                    if (lscontrol_style != "")
                    {
                        ltxt.CssClass = lscontrol_style;
                    }
                    if (ldt_head != null)
                    {
                        ltxt.Text = ldt_head.Rows[0][ldt.Rows[i]["control_id"].ToString().ToLower()].ToString();
                    }



                    lcellContent.Controls.Add(ltxt);
                    #endregion
                }
                //下拉控件
                else if (ldt.Rows[i]["control_type"].ToString() == "DROPDOWNLIST")
                {
                    #region "DropDownList"
                    DropDownList ltxt = new DropDownList();
                    ltxt.AutoPostBack = false;
                    ltxt.ID = ldt.Rows[i]["control_id"].ToString().ToLower();
                    //ToolTip
                    ltxt.ToolTip = ldt.Rows[i]["control_key"].ToString() + "|" + ldt.Rows[i]["control_empty"].ToString();
                    //是否服务器运行
                    ltxt.Attributes.Add("AutoPostBack", "false");
                    //事件
                    if (ldt.Rows[i]["control_event"].ToString() != "")
                    {
                        ltxt.Attributes.Add("onchange", ldt.Rows[i]["control_event"].ToString());
                    }
                    //宽度
                    if (ldt.Rows[i]["control_width"].ToString() != "" && ldt.Rows[i]["control_width"].ToString() != "0")
                    {
                        ltxt.Width = Convert.ToInt32(ldt.Rows[i]["control_width"].ToString());
                    }
                    //高度
                    if (ldt.Rows[i]["control_height"].ToString() != "" && ldt.Rows[i]["control_height"].ToString() != "0")
                    {
                        ltxt.Height = Convert.ToInt32(ldt.Rows[i]["control_height"].ToString());
                    }
                    //字体
                    if (ldt.Rows[i]["control_fontsize"].ToString() != "" && ldt.Rows[i]["control_fontsize"].ToString() != "0")
                    {
                        ltxt.Font.Size = Convert.ToInt32(ldt.Rows[i]["control_fontsize"].ToString());
                    }

                    //只读
                    if (ldt.Rows[i]["control_readonly"].ToString() == "1")
                    {
                        ltxt.Enabled = false;
                    }
                    //样式
                    if (lscontrol_style != "")
                    {
                        ltxt.CssClass = lscontrol_style;
                    }
                    //赋值
                    if (ldt.Rows[i]["control_type_source"].ToString() == "")
                    {
                        //直接给定值
                        if (ldt.Rows[i]["control_type_text"].ToString() != "" && ldt.Rows[i]["control_type_value"].ToString() != "")
                        {
                            string[] ls1 = ldt.Rows[i]["control_type_text"].ToString().Split(',');
                            string[] ls2 = ldt.Rows[i]["control_type_value"].ToString().Split(',');
                            if (ls1.Length == ls2.Length)
                            {
                                for (int j = 0; j < ls1.Length; j++)
                                {
                                    ltxt.Items.Add(new ListItem(ls1[j], ls2[j]));
                                }
                            }

                        }
                    }

                    else
                    {
                        //通过数据源获取
                        DataTable ldt_source = Pgi.Auto.SQLHelper.ExecuteDataSet(ldt.Rows[i]["control_type_source"].ToString(), CommandType.Text, ldt.Rows[i]["control_type_sql"].ToString()
                            , new SqlParameter[] { new SqlParameter("", "") }).Tables[0];
                        for (int j = 0; j < ldt_source.Rows.Count; j++)
                        {
                            ltxt.Items.Add(new ListItem(ldt_source.Rows[j][ldt.Rows[i]["control_type_text"].ToString()].ToString(), ldt_source.Rows[j][ldt.Rows[i]["control_type_value"].ToString()].ToString()));
                        }
                    }

                    if (ldt_head != null)
                    {
                        if (ltxt.Items.Count > 0)
                        {
                            ltxt.Text = ldt_head.Rows[0][ldt.Rows[i]["control_id"].ToString().ToLower()].ToString();
                        }

                    }

                    lcellContent.Controls.Add(ltxt);
                    #endregion

                }

                //CheckBoxList控件
                else if (ldt.Rows[i]["control_type"].ToString() == "CHECKBOXLIST")
                {
                    #region "CheckBoxList"
                    CheckBoxList ltxt = new CheckBoxList();
                    HiddenField htxt = new HiddenField();
                    htxt.ID = ldt.Rows[i]["control_id"].ToString().ToLower();
                    ltxt.AutoPostBack = false;
                    ltxt.ID = "chk" + ldt.Rows[i]["control_id"].ToString().ToLower();
                    //ToolTip
                    ltxt.ToolTip = ldt.Rows[i]["control_key"].ToString() + "|" + ldt.Rows[i]["control_empty"].ToString();
                    //是否服务器运行
                    ltxt.Attributes.Add("AutoPostBack", "false");
                    ltxt.RepeatLayout = RepeatLayout.Flow;
                    ltxt.RepeatColumns = 3;
                    //事件
                    var script = "var val='';$(\"input[id*='" + ldt.Rows[i]["control_id"].ToString().ToLower() + "']\").each(function(){   val+=$(this).val();   }); ";
                    if (ldt.Rows[i]["control_event"].ToString() != "")
                    {
                        ltxt.Attributes.Add("onclick", ldt.Rows[i]["control_event"].ToString());
                    }
                    //宽度
                    if (ldt.Rows[i]["control_width"].ToString() != "" && ldt.Rows[i]["control_width"].ToString() != "0")
                    {
                        ltxt.Width = Convert.ToInt32(ldt.Rows[i]["control_width"].ToString());
                    }
                    //高度
                    if (ldt.Rows[i]["control_height"].ToString() != "" && ldt.Rows[i]["control_height"].ToString() != "0")
                    {
                        ltxt.Height = Convert.ToInt32(ldt.Rows[i]["control_height"].ToString());
                    }
                    //字体
                    if (ldt.Rows[i]["control_fontsize"].ToString() != "" && ldt.Rows[i]["control_fontsize"].ToString() != "0")
                    {
                        ltxt.Font.Size = Convert.ToInt32(ldt.Rows[i]["control_fontsize"].ToString());
                    }
                    //只读
                    if (ldt.Rows[i]["control_readonly"].ToString() == "1")
                    {
                        ltxt.Enabled = false;
                    }
                    //样式
                    if (lscontrol_style != "")
                    {
                        ltxt.CssClass = lscontrol_style;
                    }
                    //赋值
                    if (ldt.Rows[i]["control_type_source"].ToString() == "")
                    {
                        //直接给定值
                        if (ldt.Rows[i]["control_type_text"].ToString() != "" && ldt.Rows[i]["control_type_value"].ToString() != "")
                        {
                            string[] ls1 = ldt.Rows[i]["control_type_text"].ToString().Split(',');
                            string[] ls2 = ldt.Rows[i]["control_type_value"].ToString().Split(',');
                            if (ls1.Length == ls2.Length)
                            {
                                for (int j = 0; j < ls1.Length; j++)
                                {
                                    ltxt.Items.Add(new ListItem(ls1[j], ls2[j]));
                                }
                            }

                        }
                    }

                    else
                    {
                        //通过数据源获取
                        DataTable ldt_source = Pgi.Auto.SQLHelper.ExecuteDataSet(ldt.Rows[i]["control_type_source"].ToString(), CommandType.Text, ldt.Rows[i]["control_type_sql"].ToString()
                            , new SqlParameter[] { new SqlParameter("", "") }).Tables[0];
                        for (int j = 0; j < ldt_source.Rows.Count; j++)
                        {
                            ltxt.Items.Add(new ListItem(ldt_source.Rows[j][ldt.Rows[i]["control_type_text"].ToString()].ToString(), ldt_source.Rows[j][ldt.Rows[i]["control_type_value"].ToString()].ToString()));
                        }
                    }

                    if (ldt_head != null)
                    {
                        if (ltxt.Items.Count > 0)
                        {
                            ltxt.Text = ldt_head.Rows[0][ldt.Rows[i]["control_id"].ToString().ToLower()].ToString();
                        }

                    }

                    lcellContent.Controls.Add(ltxt);
                    #endregion

                }

                else if (ldt.Rows[i]["control_type"].ToString() == "CHECKBOX")
                {
                    #region "CheckBox"
                    CheckBox ltxt = new CheckBox();
                    ltxt.AutoPostBack = false;
                    ltxt.ID = ldt.Rows[i]["control_id"].ToString().ToLower();
                    //ToopTip
                    ltxt.ToolTip = ldt.Rows[i]["control_key"].ToString() + "|" + ldt.Rows[i]["control_empty"].ToString();
                    ////是否服务器运行
                    //ltxt.Attributes.Add("AutoPostBack", "false");
                    //事件
                    if (ldt.Rows[i]["control_event"].ToString() != "")
                    {
                        ltxt.Attributes.Add("onchange", ldt.Rows[i]["control_event"].ToString());
                    }
                    //宽度
                    if (ldt.Rows[i]["control_width"].ToString() != "" && ldt.Rows[i]["control_width"].ToString() != "0")
                    {
                        ltxt.Width = Convert.ToInt32(ldt.Rows[i]["control_width"].ToString());
                    }
                    //高度
                    if (ldt.Rows[i]["control_height"].ToString() != "" && ldt.Rows[i]["control_height"].ToString() != "0")
                    {
                        ltxt.Height = Convert.ToInt32(ldt.Rows[i]["control_height"].ToString());
                    }
                    //字体
                    if (ldt.Rows[i]["control_fontsize"].ToString() != "" && ldt.Rows[i]["control_fontsize"].ToString() != "0")
                    {
                        ltxt.Font.Size = Convert.ToInt32(ldt.Rows[i]["control_fontsize"].ToString());
                    }
                    //只读
                    if (ldt.Rows[i]["control_readonly"].ToString() == "1")
                    {
                        ltxt.Enabled = false;
                        // ltxt.Attributes.Add("contenteditable", "false");
                    }
                    //样式
                    if (lscontrol_style != "")
                    {
                        ltxt.CssClass = lscontrol_style;
                    }
                    if (ldt.Rows[i]["control_type_value"].ToString() != "")
                    {
                        ltxt.Checked = true;
                    }

                    if (ldt_head != null)
                    {
                        if (ldt_head.Rows[0][ldt.Rows[i]["control_id"].ToString().ToLower()].ToString() == "1" || ldt_head.Rows[0][ldt.Rows[i]["control_id"].ToString().ToLower()].ToString().ToUpper() == "TRUE")
                        {
                            ltxt.Checked = true;
                        }
                    }

                    lcellContent.Controls.Add(ltxt);
                    #endregion
                }
                else if (ldt.Rows[i]["control_type"].ToString() == "FILEUPLOAD")
                {
                    #region "FileUpLoad"
                    FileUpload ltxt = new FileUpload();
                    HyperLink lnk = new HyperLink();
                    ltxt.ID = ldt.Rows[i]["control_id"].ToString().ToLower();
                    lnk.ID = "link_" + ldt.Rows[i]["control_id"].ToString().ToLower();
                    //ToopTip
                    ltxt.ToolTip = ldt.Rows[i]["control_key"].ToString() + "|" + ldt.Rows[i]["control_empty"].ToString();
                    ////是否服务器运行
                    //ltxt.Attributes.Add("AutoPostBack", "false");
                    //事件
                    //if (ldt.Rows[i]["control_event"].ToString() != "")
                    //{
                    //    ltxt.Attributes.Add("onchange", ldt.Rows[i]["control_event"].ToString());
                    //}
                    //宽度
                    if (ldt.Rows[i]["control_width"].ToString() != "" && ldt.Rows[i]["control_width"].ToString() != "0")
                    {
                        ltxt.Width = Convert.ToInt32(ldt.Rows[i]["control_width"].ToString());
                    }
                    //高度
                    if (ldt.Rows[i]["control_height"].ToString() != "" && ldt.Rows[i]["control_height"].ToString() != "0")
                    {
                        ltxt.Height = Convert.ToInt32(ldt.Rows[i]["control_height"].ToString());
                    }
                    //字体
                    if (ldt.Rows[i]["control_fontsize"].ToString() != "" && ldt.Rows[i]["control_fontsize"].ToString() != "0")
                    {
                        ltxt.Font.Size = Convert.ToInt32(ldt.Rows[i]["control_fontsize"].ToString());
                    }
                    //只读
                    if (ldt.Rows[i]["control_readonly"].ToString() == "1")
                    {
                        ltxt.Enabled = false;
                        // ltxt.Attributes.Add("contenteditable", "false");
                    }
                    //样式
                    if (lscontrol_style != "")
                    {
                        ltxt.CssClass = lscontrol_style;
                    }



                    lcellContent.Controls.Add(ltxt);
                    lcellContent.Controls.Add(lnk);
                    #endregion
                }
                else if (ldt.Rows[i]["control_type"].ToString() == "ASPXGRIDLOOKUP")
                {
                    #region ASPxGridLookup
                    DevExpress.Web.ASPxGridLookup ltxt = new DevExpress.Web.ASPxGridLookup();
                    ltxt.ID = ldt.Rows[i]["control_id"].ToString().ToLower();
                    //ToolTip
                    ltxt.ToolTip = ldt.Rows[i]["control_key"].ToString() + "|" + ldt.Rows[i]["control_empty"].ToString();
                    //是否服务器运行
                    // ltxt.Attributes.Add("AutoPostBack", "false");
                    //事件
                    //if (ldt.Rows[i]["control_event"].ToString() != "" && ldt.Rows[i]["control_event1"].ToString() != "")
                    //{
                    //    ltxt.Attributes.Add(ldt.Rows[i]["control_event1"].ToString(), ldt.Rows[i]["control_event"].ToString());
                    //}
                    //else if (ldt.Rows[i]["control_event"].ToString() != "" && ldt.Rows[i]["control_event1"].ToString() == "")
                    //{
                    //    ltxt.Attributes.Add("onchange", ldt.Rows[i]["control_event"].ToString());
                    //}
                    if (ldt.Rows[i]["control_event1"].ToString() == "")
                    {
                        ltxt.ClientSideEvents.QueryCloseUp = "function(s, e) {" + ldt.Rows[i]["control_event"].ToString() + "}";
                    }

                    //宽度
                    if (ldt.Rows[i]["control_width"].ToString() != "" && ldt.Rows[i]["control_width"].ToString() != "0")
                    {
                        ltxt.Width = Convert.ToInt32(ldt.Rows[i]["control_width"].ToString());
                    }
                    //高度
                    if (ldt.Rows[i]["control_height"].ToString() != "" && ldt.Rows[i]["control_height"].ToString() != "0")
                    {
                        ltxt.Height = Convert.ToInt32(ldt.Rows[i]["control_height"].ToString());
                    }
                    //字体
                    if (ldt.Rows[i]["control_fontsize"].ToString() != "" && ldt.Rows[i]["control_fontsize"].ToString() != "0")
                    {
                        ltxt.Font.Size = Convert.ToInt32(ldt.Rows[i]["control_fontsize"].ToString());
                    }

                    //只读
                    if (ldt.Rows[i]["control_readonly"].ToString() == "1")
                    {
                        ltxt.Enabled = false;
                    }
                    //样式
                    if (lscontrol_style != "")
                    {
                        ltxt.CssClass = lscontrol_style;
                    }
                    //赋值
                    if (ldt.Rows[i]["control_type_source"].ToString() == "")
                    {
                        //直接给定值
                        //if (ldt.Rows[i]["control_type_text"].ToString() != "" && ldt.Rows[i]["control_type_value"].ToString() != "")
                        //{
                        //    string[] ls_column= ldt.Rows[i]["control_type_text"].ToString().Split(',');
                        //    string[] ls1 = ldt.Rows[i]["control_type_text"].ToString().Split(',');
                        //    string[] ls2 = ldt.Rows[i]["control_type_value"].ToString().Split(',');
                        //    if (ls1.Length == ls2.Length)
                        //    {
                        //        for (int j = 0; j < ls1.Length; j++)
                        //        {
                        //            ltxt.Items.Add(new ListItem(ls1[j], ls2[j]));
                        //        }
                        //    }

                        //}
                    }

                    else
                    {
                        //通过数据源获取
                        DataTable ldt_source = Pgi.Auto.SQLHelper.ExecuteDataSet(ldt.Rows[i]["control_type_source"].ToString(), CommandType.Text, ldt.Rows[i]["control_type_sql"].ToString()
                            , new SqlParameter[] { new SqlParameter("", "") }).Tables[0];
                        string lspara = "";
                        for (int j = 0; j < ldt_source.Columns.Count; j++)
                        {
                            if (lspara != "")
                            {
                                lspara += " ";
                            }
                            lspara += "{" + j + "}";
                            DevExpress.Web.GridViewDataTextColumn lcom = new DevExpress.Web.GridViewDataTextColumn();
                            lcom.Name = ldt_source.Columns[j].ColumnName;
                            lcom.FieldName = ldt_source.Columns[j].ColumnName; ;
                            ltxt.Columns.Add(lcom);
                        }
                        if (ldt.Rows[i]["control_type_value"].ToString() != "")
                        {
                            ltxt.KeyFieldName = ldt.Rows[i]["control_type_value"].ToString();
                        }
                        else
                        {
                            ltxt.KeyFieldName = ldt_source.Columns[0].ColumnName;
                        }

                        if (ldt.Rows[i]["control_type_text"].ToString() != "")
                        {
                            ltxt.TextFormatString = ldt.Rows[i]["control_type_text"].ToString();
                        }
                        else
                        {
                            ltxt.TextFormatString = lspara;
                        }
                        ltxt.GridViewProperties.SettingsBehavior.AllowFocusedRow = true;
                        ltxt.GridViewProperties.SettingsBehavior.AllowSelectSingleRowOnly = true;
                        ltxt.GridViewProperties.SettingsBehavior.AllowDragDrop = false;
                        ltxt.GridViewProperties.SettingsBehavior.EnableRowHotTrack = false;
                        ltxt.GridViewProperties.Settings.AutoFilterCondition = DevExpress.Web.AutoFilterCondition.Contains;
                        ltxt.GridViewProperties.Settings.ShowColumnHeaders = false;


                        ltxt.DataSource = ldt_source;
                        ltxt.DataBind();
                        //for (int j = 0; j < ldt_source.Rows.Count; j++)
                        //{
                        //    ltxt.Items.Add(new ListItem(ldt_source.Rows[j][ldt.Rows[i]["control_type_text"].ToString()].ToString(), ldt_source.Rows[j][ldt.Rows[i]["control_type_value"].ToString()].ToString()));
                        //}
                    }

                    if (ldt_head != null)
                    {

                        ltxt.Value = ldt_head.Rows[0][ldt.Rows[i]["control_id"].ToString().ToLower()].ToString();


                    }

                    lcellContent.Controls.Add(ltxt);

                    #endregion
                }
                else if (ldt.Rows[i]["control_type"].ToString() == "ASPXCOMBOBOX")
                {
                    #region ASPxComboBox
                    DevExpress.Web.ASPxComboBox ltxt = new DevExpress.Web.ASPxComboBox();
                    ltxt.ID = ldt.Rows[i]["control_id"].ToString().ToLower();
                    //ToolTip
                    ltxt.ToolTip = ldt.Rows[i]["control_key"].ToString() + "|" + ldt.Rows[i]["control_empty"].ToString();
                   
                    if (ldt.Rows[i]["control_event1"].ToString() == "")
                    {
                        ltxt.ClientSideEvents.QueryCloseUp = "function(s, e) {" + ldt.Rows[i]["control_event"].ToString() + "}";
                    }

                    //宽度
                    if (ldt.Rows[i]["control_width"].ToString() != "" && ldt.Rows[i]["control_width"].ToString() != "0")
                    {
                        ltxt.Width = Convert.ToInt32(ldt.Rows[i]["control_width"].ToString());
                    }
                    //高度
                    if (ldt.Rows[i]["control_height"].ToString() != "" && ldt.Rows[i]["control_height"].ToString() != "0")
                    {
                        ltxt.Height = Convert.ToInt32(ldt.Rows[i]["control_height"].ToString());
                    }
                    //字体
                    if (ldt.Rows[i]["control_fontsize"].ToString() != "" && ldt.Rows[i]["control_fontsize"].ToString() != "0")
                    {
                        ltxt.Font.Size = Convert.ToInt32(ldt.Rows[i]["control_fontsize"].ToString());
                    }

                    //只读
                    if (ldt.Rows[i]["control_readonly"].ToString() == "1")
                    {
                        ltxt.Enabled = false;
                    }
                    //样式
                    if (lscontrol_style != "")
                    {
                        ltxt.CssClass = lscontrol_style;
                    }
                    //赋值
                    if (ldt.Rows[i]["control_type_source"].ToString() == "")
                    {
                        //直接给定值
                      
                    }

                    else
                    {
                        //通过数据源获取
                        DataTable ldt_source = Pgi.Auto.SQLHelper.ExecuteDataSet(ldt.Rows[i]["control_type_source"].ToString(), CommandType.Text, ldt.Rows[i]["control_type_sql"].ToString()
                            , new SqlParameter[] { new SqlParameter("", "") }).Tables[0];
                        string lspara = "";
                        for (int j = 0; j < ldt_source.Columns.Count; j++)
                        {
                            if (lspara != "")
                            {
                                lspara += " ";
                            }
                            lspara += "{" + j + "}";
                            DevExpress.Web.ListBoxColumn lcom = new DevExpress.Web.ListBoxColumn();
                            lcom.Name = ldt_source.Columns[j].ColumnName;
                            lcom.FieldName = ldt_source.Columns[j].ColumnName; ;
                            ltxt.Columns.Add(lcom);
                        }
                      

                        if (ldt.Rows[i]["control_type_text"].ToString() != "")
                        {
                            ltxt.TextFormatString = ldt.Rows[i]["control_type_text"].ToString();
                        }
                        else
                        {
                            ltxt.TextFormatString = lspara;
                        }
                      


                        ltxt.DataSource = ldt_source;
                        ltxt.DataBind();
                        //for (int j = 0; j < ldt_source.Rows.Count; j++)
                        //{
                        //    ltxt.Items.Add(new ListItem(ldt_source.Rows[j][ldt.Rows[i]["control_type_text"].ToString()].ToString(), ldt_source.Rows[j][ldt.Rows[i]["control_type_value"].ToString()].ToString()));
                        //}
                    }

                    if (ldt_head != null)
                    {

                        ltxt.Value = ldt_head.Rows[0][ldt.Rows[i]["control_id"].ToString().ToLower()].ToString();


                    }

                    lcellContent.Controls.Add(ltxt);

                    #endregion
                }
                else if (ldt.Rows[i]["control_type"].ToString() == "ASPXDATEEDIT")
                {
                    #region "ASPXDATEEDIT"
                    DevExpress.Web.ASPxDateEdit ltxt = new DevExpress.Web.ASPxDateEdit();
                    ltxt.AutoPostBack = false;
                    ltxt.ID = ldt.Rows[i]["control_id"].ToString().ToLower();
                    //ToopTip
                    ltxt.ToolTip = ldt.Rows[i]["control_key"].ToString() + "|" + ldt.Rows[i]["control_empty"].ToString();
                    //是否服务器运行
                    ltxt.Attributes.Add("AutoPostBack", "false");
                    //事件
                    if (ldt.Rows[i]["control_event"].ToString() != "" && ldt.Rows[i]["control_event1"].ToString() != "")
                    {
                        ltxt.Attributes.Add(ldt.Rows[i]["control_event1"].ToString(), ldt.Rows[i]["control_event"].ToString());
                    }
                    else if (ldt.Rows[i]["control_event"].ToString() != "" && ldt.Rows[i]["control_event1"].ToString() == "")
                    {
                        ltxt.Attributes.Add("onchange", ldt.Rows[i]["control_event"].ToString());
                    }
                    //宽度
                    if (ldt.Rows[i]["control_width"].ToString() != "" && ldt.Rows[i]["control_width"].ToString() != "0")
                    {
                        ltxt.Width = Convert.ToInt32(ldt.Rows[i]["control_width"].ToString());
                    }
                    //高度
                    if (ldt.Rows[i]["control_height"].ToString() != "" && ldt.Rows[i]["control_height"].ToString() != "0")
                    {
                        ltxt.Height = Convert.ToInt32(ldt.Rows[i]["control_height"].ToString());
                    }
                    //字体
                    if (ldt.Rows[i]["control_fontsize"].ToString() != "" && ldt.Rows[i]["control_fontsize"].ToString() != "0")
                    {
                        ltxt.Font.Size = Convert.ToInt32(ldt.Rows[i]["control_fontsize"].ToString());
                    }

                    //只读
                    if (ldt.Rows[i]["control_readonly"].ToString() == "1")
                    {
                        ltxt.ReadOnly = true;
                        ltxt.Attributes.Add("contenteditable", "false");
                    }
                    //样式
                    if (lscontrol_style != "")
                    {
                        ltxt.CssClass = lscontrol_style;
                    }
                    if (ldt_head != null)
                    {
                        ltxt.Text = ldt_head.Rows[0][ldt.Rows[i]["control_id"].ToString().ToLower()].ToString();
                    }



                    lcellContent.Controls.Add(ltxt);
                    #endregion
                }
                //-----------------------------------控件判断结束----------------------------------------
                //判断下个字段是否独立
                if (i + 1 < ldt.Rows.Count)
                {
                    if (ldt.Rows[i + 1]["control_onlyrow"].ToString() == "1")
                    {
                        int lnspan = i % lncolumn + 1;
                        lcellContent.ColumnSpan = lnspan * 2;
                    }
                }
                if (ldt.Rows[i]["control_onlyrow"].ToString() == "1")
                {
                    lcellContent.ColumnSpan = lncolumn * 2 - 1;
                }
                lcellHead.Controls.Add(lbl);
                lrow.Cells.Add(lcellHead);
                lrow.Cells.Add(lcellContent);


                if ((i % lncolumn) == 0 || ldt.Rows[i]["control_onlyrow"].ToString() == "1")
                {
                    ls.Add(lrow);
                }
                ln += 1;
            }


            return ls;
        }
        #endregion

        #region 设置要显示Grid样式及数据源
        /// <summary>
        /// 设置要显示Grid样式及数据源
        /// </summary>
        /// <param name="lsform_type">要显示字段大类</param>
        /// <param name="lsform_div">要显示字段小类</param>
        /// <param name="lgrid">Grid控件(目前只支持DEV17.2)</param>
        /// <param name="ldt_data">显示数据源</param>
        /// <param name="b_flag">是否在Grid首列插入选择列</param>
        public static void SetGrid(string lsform_type, string lsform_div, DevExpress.Web.ASPxGridView lgrid, DataTable ldt_data, int b_flag = 0)
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
            else if (b_flag==2)
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

                            if (ldt.Rows[j]["list_link"].ToString() != "" && ldt.Rows[j]["list_type"].ToString()=="")
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


            //获取模版数据源
            DataSet lds = new DataSet();
            if (ldt_data.Rows.Count>0)
            {

                for (int i = 0; i < lgrid.DataColumns.Count; i++)
                {
                    DataRow[] ldrs = ldt.Select(" list_fieldname='" + lgrid.DataColumns[i].FieldName + "'");
                    if (ldrs.Length>0)
                    {
                        if (ldrs[0]["control_type_source"].ToString() != "" && ldrs[0]["control_type_sql"].ToString() != "")
                        {
                            if (lds.Tables.IndexOf(lgrid.DataColumns[i].FieldName)<1)
                            {
                                DataTable ldt_source = Pgi.Auto.SQLHelper.ExecuteDataSet(ldrs[0]["control_type_source"].ToString(), CommandType.Text, ldrs[0]["control_type_sql"].ToString()
                                   , new SqlParameter[] { new SqlParameter("", "") }).Tables[0];
                                //  ldt_source.TableName = lgrid.DataColumns[i].FieldName;
                                DataTable ldt_copay = ldt_source.Copy();
                                lds.Tables.Add(ldt_copay);
                               
                                lds.Tables[lds.Tables.Count - 1].TableName= lgrid.DataColumns[i].FieldName;

                            }
                         
                        }
                    }
                }

                for (int i = 0; i < ldt_data.Rows.Count; i++)
                {
                    if (ldt_data.Columns.IndexOf("flag") > 0)
                    {
                        if (ldt_data.Rows[i]["flag"].ToString() == "1")
                        {
                            lgrid.Selection.SetSelection(i, true);
                        }
                    }

                    for (int j = 0; j < lgrid.DataColumns.Count; j++)
                    {


                        if (lgrid.FindRowCellTemplateControl(i, lgrid.DataColumns[j], lgrid.DataColumns[j].FieldName) is DevExpress.Web.ASPxTextBox)
                        {
                            DevExpress.Web.ASPxTextBox ltxt = ((DevExpress.Web.ASPxTextBox)lgrid.FindRowCellTemplateControl(i, lgrid.DataColumns[j], lgrid.DataColumns[j].FieldName));
                            DataRow[] ldrs = ldt.Select(" list_fieldname='" + lgrid.DataColumns[j].FieldName + "'");
                            //增加属性
                            if (ldrs.Length > 0)
                            {
                                if (ldrs[0]["control_event1"].ToString() != "" && ldrs[0]["control_event"].ToString() != "")
                                {
                                    ltxt.Attributes.Add(ldrs[0]["control_event1"].ToString(), ldrs[0]["control_event"].ToString());
                                }
                                else if (ldrs[0]["control_event"].ToString() != "")
                                {
                                    ltxt.Attributes.Add("onchange", ldrs[0]["control_event"].ToString());
                                }
                            }
                            ltxt.Text = ldt_data.Rows[i][lgrid.DataColumns[j].FieldName].ToString();
                        }
                        else if (lgrid.FindRowCellTemplateControl(i, lgrid.DataColumns[j], lgrid.DataColumns[j].FieldName) is Label)
                        {
                            Label ltxt = ((Label)lgrid.FindRowCellTemplateControl(i, lgrid.DataColumns[j], lgrid.DataColumns[j].FieldName));
                            
                            ltxt.Text = ldt_data.Rows[i][lgrid.DataColumns[j].FieldName].ToString();

                        }

                        else if (lgrid.FindRowCellTemplateControl(i, lgrid.DataColumns[j], lgrid.DataColumns[j].FieldName) is TextBox)
                        {
                            TextBox ltxt = ((TextBox)lgrid.FindRowCellTemplateControl(i, lgrid.DataColumns[j], lgrid.DataColumns[j].FieldName));
                            DataRow[] ldrs = ldt.Select(" list_fieldname='" + lgrid.DataColumns[j].FieldName + "'");
                            //增加属性
                            if (ldrs.Length > 0)
                            {
                                if (ldrs[0]["control_event1"].ToString() != "" && ldrs[0]["control_event"].ToString() != "")
                                {
                                    ltxt.Attributes.Add(ldrs[0]["control_event1"].ToString(), ldrs[0]["control_event"].ToString());
                                }
                                else if (ldrs[0]["control_event"].ToString() != "")
                                {
                                    ltxt.Attributes.Add("onchange", ldrs[0]["control_event"].ToString());
                                }
                            }

                            ltxt.Text = ldt_data.Rows[i][lgrid.DataColumns[j].FieldName].ToString();

                        }
                        else if (lgrid.FindRowCellTemplateControl(i, lgrid.DataColumns[j], lgrid.DataColumns[j].FieldName) is CheckBox)
                        {
                            CheckBox ltxt = ((CheckBox)lgrid.FindRowCellTemplateControl(i, lgrid.DataColumns[j], lgrid.DataColumns[j].FieldName));
                            DataRow[] ldrs = ldt.Select(" list_fieldname='" + lgrid.DataColumns[j].FieldName + "'");
                            //增加属性
                            if (ldrs.Length > 0)
                            {
                                if (ldrs[0]["control_event1"].ToString() != "" && ldrs[0]["control_event"].ToString() != "")
                                {
                                    ltxt.Attributes.Add(ldrs[0]["control_event1"].ToString(), ldrs[0]["control_event"].ToString());
                                }
                                else if (ldrs[0]["control_event"].ToString() != "")
                                {
                                    ltxt.Attributes.Add("onchange", ldrs[0]["control_event"].ToString());
                                }
                            }

                            if (ldt_data.Rows[i][lgrid.DataColumns[j].FieldName].ToString() == "1")
                            {
                                ltxt.Checked = true;
                            }
                        }
                        else if (lgrid.FindRowCellTemplateControl(i, lgrid.DataColumns[j], lgrid.DataColumns[j].FieldName) is DevExpress.Web.ASPxDateEdit)
                        {
                            DevExpress.Web.ASPxDateEdit ltxt = ((DevExpress.Web.ASPxDateEdit)lgrid.FindRowCellTemplateControl(i, lgrid.DataColumns[j], lgrid.DataColumns[j].FieldName));
                            DataRow[] ldrs = ldt.Select(" list_fieldname='" + lgrid.DataColumns[j].FieldName + "'");
                            //增加属性
                            if (ldrs.Length > 0)
                            {
                                if (ldrs[0]["control_event1"].ToString() != "" && ldrs[0]["control_event"].ToString() != "")
                                {
                                    ltxt.Attributes.Add(ldrs[0]["control_event1"].ToString(), ldrs[0]["control_event"].ToString());
                                }
                                else if (ldrs[0]["control_event"].ToString() != "")
                                {
                                    ltxt.Attributes.Add("onchange", ldrs[0]["control_event"].ToString());
                                }
                            }

                            if (ldt_data.Rows[i][lgrid.DataColumns[j].FieldName].ToString()!="")
                            {
                                ltxt.Value =Convert.ToDateTime(ldt_data.Rows[i][lgrid.DataColumns[j].FieldName].ToString()).ToShortDateString();
                            }
                              
                          
                            
                        }
                        else if (lgrid.FindRowCellTemplateControl(i, lgrid.DataColumns[j], lgrid.DataColumns[j].FieldName) is HyperLink)
                        {
                            HyperLink ltxt = ((HyperLink)lgrid.FindRowCellTemplateControl(i, lgrid.DataColumns[j], lgrid.DataColumns[j].FieldName));
                            DataRow[] ldrs = ldt.Select(" list_fieldname='" + lgrid.DataColumns[j].FieldName + "'");
                            //增加属性
                            if (ldrs.Length > 0)
                            {
                                if (ldrs[0]["control_event1"].ToString() != "" && ldrs[0]["control_event"].ToString() != "")
                                {
                                    ltxt.Attributes.Add(ldrs[0]["control_event1"].ToString(), ldrs[0]["control_event"].ToString());
                                }
                                else if (ldrs[0]["control_event"].ToString() != "")
                                {
                                    ltxt.Attributes.Add("onchange", ldrs[0]["control_event"].ToString());
                                }
                            }
                            if (ldrs[0]["list_link"].ToString() != "" && ldrs[0]["list_link_fields"].ToString() == "")
                            {
                                ltxt.NavigateUrl = ldrs[0]["list_link"].ToString();
                            }
                            else if (ldrs[0]["list_link"].ToString() != "" && ldrs[0]["list_link_fields"].ToString() != "")
                            {
                                string[] lstrs = ldrs[0]["list_link_fields"].ToString().Split(',');
                                string lsvalue = ldrs[0]["list_link"].ToString();
                                for (int ii = 0; ii < lstrs.Length; ii++)
                                {
                                    lsvalue = lsvalue.Replace("{" + ii.ToString() + "}", ldt_data.Rows[i][lstrs[ii]].ToString());
                                }
                                ltxt.NavigateUrl = lsvalue;
                            }
                            else
                            {
                                ltxt.NavigateUrl = ldt_data.Rows[i][lgrid.DataColumns[j].FieldName].ToString();
                            }

                            
                            ltxt.Text = ldt_data.Rows[i][lgrid.DataColumns[j].FieldName].ToString();
                        }
                        else if (lgrid.FindRowCellTemplateControl(i, lgrid.DataColumns[j], lgrid.DataColumns[j].FieldName) is DropDownList)
                        {
                            DropDownList ltxt = ((DropDownList)lgrid.FindRowCellTemplateControl(i, lgrid.DataColumns[j], lgrid.DataColumns[j].FieldName));
                            DataRow[] ldrs = ldt.Select(" list_fieldname='" + lgrid.DataColumns[j].FieldName + "'");
                            //增加属性
                            if (ldrs.Length > 0)
                            {
                                if (ldrs[0]["control_event1"].ToString() != "" && ldrs[0]["control_event"].ToString() != "")
                                {
                                    ltxt.Attributes.Add(ldrs[0]["control_event1"].ToString(), ldrs[0]["control_event"].ToString());
                                }
                                else if (ldrs[0]["control_event"].ToString() != "")
                                {
                                    ltxt.Attributes.Add("onchange", ldrs[0]["control_event"].ToString());
                                }
                            }
                            if (ldrs.Length > 0)
                            {

                                //赋值
                                if (ldrs[0]["control_type_source"].ToString() == "")
                                {
                                    //直接给定值
                                    if (ldrs[0]["control_type_text"].ToString() != "" && ldrs[0]["control_type_value"].ToString() != "")
                                    {
                                        string[] ls1 = ldrs[0]["control_type_text"].ToString().Split(',');
                                        string[] ls2 = ldrs[0]["control_type_value"].ToString().Split(',');
                                        if (ls1.Length == ls2.Length)
                                        {
                                            for (int k = 0; k < ls1.Length; k++)
                                            {
                                                ltxt.Items.Add(new ListItem(ls1[k], ls2[k]));
                                            }
                                        }

                                    }
                                }
                                else
                                {
                                    //通过数据源获取
                                    //DataTable ldt_source = Pgi.Auto.SQLHelper.ExecuteDataSet(ldrs[0]["control_type_source"].ToString(), CommandType.Text, ldrs[0]["control_type_sql"].ToString()
                                    //    , new SqlParameter[] { new SqlParameter("", "") }).Tables[0];
                                    if (lds.Tables[lgrid.DataColumns[j].FieldName]!=null)
                                    {
                                        for (int k = 0; k < lds.Tables[lgrid.DataColumns[j].FieldName].Rows.Count; k++)
                                        {
                                            ltxt.Items.Add(new ListItem(lds.Tables[lgrid.DataColumns[j].FieldName].Rows[k][ldrs[0]["control_type_text"].ToString()].ToString(), lds.Tables[lgrid.DataColumns[j].FieldName].Rows[k][ldrs[0]["control_type_value"].ToString()].ToString()));
                                        }
                                    }
                                    
                                }
                                ltxt.Text = ldt_data.Rows[i][lgrid.DataColumns[j].FieldName].ToString();
                            }
                            ltxt.Text = ldt_data.Rows[i][lgrid.DataColumns[j].FieldName].ToString();
                        }
                        else if (lgrid.FindRowCellTemplateControl(i, lgrid.DataColumns[j], lgrid.DataColumns[j].FieldName) is DevExpress.Web.ASPxComboBox)
                        {
                            DevExpress.Web.ASPxComboBox ltxt = ((DevExpress.Web.ASPxComboBox)lgrid.FindRowCellTemplateControl(i, lgrid.DataColumns[j], lgrid.DataColumns[j].FieldName));
                            DataRow[] ldrs = ldt.Select(" list_fieldname='" + lgrid.DataColumns[j].FieldName + "'");
                            //增加属性
                            if (ldrs.Length > 0)
                            {
                                if (ldrs[0]["control_event1"].ToString() != "" && ldrs[0]["control_event"].ToString() != "")
                                {
                                    ltxt.Attributes.Add(ldrs[0]["control_event1"].ToString(), ldrs[0]["control_event"].ToString());
                                }
                                else if (ldrs[0]["control_event"].ToString() != "")
                                {
                                    ltxt.Attributes.Add("onchange", ldrs[0]["control_event"].ToString());
                                }
                            }
                            if (ldrs.Length > 0)
                            {

                                //赋值
                                if (ldrs[0]["control_type_source"].ToString() == "")
                                {
                                    //直接给定值

                                }
                                else
                                {
                                    //通过数据源获取
                                    if (lds.Tables[lgrid.DataColumns[j].FieldName] != null)
                                    {
                                        string lspara = "";
                                        for (int k = 0; k < lds.Tables[lgrid.DataColumns[j].FieldName.ToString()].Columns.Count; k++)
                                        {
                                            if (lspara != "")
                                            {
                                                lspara += " ";
                                            }
                                            lspara += "{" + k + "}";
                                            DevExpress.Web.ListBoxColumn lcom = new DevExpress.Web.ListBoxColumn();
                                            lcom.Name = lds.Tables[lgrid.DataColumns[j].FieldName.ToString()].Columns[k].ColumnName;
                                            lcom.FieldName = lds.Tables[lgrid.DataColumns[j].FieldName.ToString()].Columns[k].ColumnName; ;
                                            ltxt.Columns.Add(lcom);
                                        }


                                        if (ldrs[0]["control_type_text"].ToString() != "")
                                        {
                                            ltxt.TextFormatString = ldrs[0]["control_type_text"].ToString();
                                        }
                                        else
                                        {
                                            ltxt.TextFormatString = lspara;
                                        }



                                        ltxt.DataSource = lds.Tables[lgrid.DataColumns[j].FieldName.ToString()];
                                        ltxt.DataBind();
                                    }


                                  
                                }
                                ltxt.Text = ldt_data.Rows[i][lgrid.DataColumns[j].FieldName].ToString();
                            }
                            ltxt.Text = ldt_data.Rows[i][lgrid.DataColumns[j].FieldName].ToString();
                        }

                    }
                }

                //回收
                lds = null;




                ////第二种方式，解决排序后模板列消失问题
                //for (int i = 0; i < ldt_data.Rows.Count; i++)
                //{
                //    for (int j = 0; j < lgrid.DataColumns.Count; j++)
                //    {
                //        DataRow[] ldrs = ldt.Select(" list_fieldname='" + lgrid.DataColumns[j].FieldName + "'");
                //        if (ldrs.Length>0)
                //        {
                //            if (ldrs[0]["list_type"].ToString()== "HYPERLINK")
                //            {
                //                DevExpress.Web.GridViewDataTextColumn lcolumn = new DevExpress.Web.GridViewDataTextColumn();
                //                    //自定义显示grid列类型
                //                lcolumn.DataItemTemplate = new ColumnTemplate(ldrs[0]);
                                
                //            }
                //        }
                //    }
                //}
            }
            
        }




        public static void SetGrid(DevExpress.Web.ASPxGridView lgrid, DataTable ldt_data, Int32 lnw)
        {
            if (ldt_data == null)
            {
                return;
            }
           
            lgrid.AutoGenerateColumns = false;
            int lnwidth = 0;
            lgrid.Columns.Clear();
            for (int i = 0; i < ldt_data.Columns.Count; i++)
            {

                DevExpress.Web.GridViewDataTextColumn lcolumn = new DevExpress.Web.GridViewDataTextColumn();
                lcolumn.Name = ldt_data.Columns[i].ColumnName.ToString();
                lcolumn.Caption = ldt_data.Columns[i].ColumnName.ToString();
                lcolumn.FieldName = ldt_data.Columns[i].ColumnName.ToString();
                lcolumn.HeaderStyle.Wrap = DevExpress.Utils.DefaultBoolean.True;
                lcolumn.CellStyle.Wrap = DevExpress.Utils.DefaultBoolean.True;

                if (ldt_data.Columns[i].MaxLength>0)
                {
                    lcolumn.Width = ldt_data.Columns[i].MaxLength;
                    lcolumn.ExportWidth = ldt_data.Columns[i].MaxLength;
                }
                else
                {
                    lcolumn.Width = lnw;
                    lcolumn.ExportWidth = lnw;
                }
              
                //设置查询
                lcolumn.Settings.AutoFilterCondition = DevExpress.Web.AutoFilterCondition.Contains;

                lgrid.Columns.Add(lcolumn);

                if (ldt_data.Columns[i].MaxLength > 0)
                {
                    lnwidth += ldt_data.Columns[i].MaxLength;
                }
                else
                {
                    lnwidth += Convert.ToInt32(lnw);
                }
                    
            }

            lgrid.Width = lnwidth;
            lgrid.DataSource = ldt_data;
            lgrid.DataBind();

            //return lgrid;
        }

        #endregion


        #region 将TableRow栏位值赋值给页面中的控件

        //把表中值初始化给控件 added by fish:
        /// <summary>
        /// 将TableRow栏位值赋值给页面中的控件
        /// </summary>
        /// <param name="lsform_type">页面识别参数</param>
        /// <param name="lsform_div">div显示范围识别参数</param>
        /// <param name="p">page</param>
        /// <param name="dr">a DataRow</param>
        public static void SetControlValue(string lsform_type, string lsform_div, System.Web.UI.Page p, DataRow dr,string lscontrolformat="")
        {
            string lswhere = "";
            if (lsform_type != "")
            {
                lswhere += " and form_type=@form_type";
            }
            if (lsform_div != "")
            {
                lswhere += " and form_div=@form_div";
            }
            string lsconn = ConfigurationSettings.AppSettings["ConnectionMES"];
            DataTable ldt = Pgi.Auto.SQLHelper.ExecuteDataSet(lsconn, CommandType.Text, "select * from auto_form where 1=1 and isnull(control_id,'')<>''" + lswhere + "",
                new SqlParameter[]{
                                    new SqlParameter("@form_type",lsform_type)
                                    ,new SqlParameter("@form_div",lsform_div)}).Tables[0];


            foreach (DataRow row in ldt.Rows)
            {
                if (p.FindControl(lscontrolformat+row["control_id"].ToString().ToLower()) != null)
                {
                    var columnValue = dr[row["control_id"].ToString().ToLower()].ToString();
                    if (row["control_type"].ToString() == "TEXTBOX")
                    {
                        ((TextBox)p.FindControl(lscontrolformat + row["control_id"].ToString().ToLower())).Text = columnValue;
                    }
                    else if (row["control_type"].ToString() == "DROPDOWNLIST")
                    {
                        var drop = (DropDownList)p.FindControl(lscontrolformat + row["control_id"].ToString());
                        if (drop != null)
                        {
                            ListItem item = drop.Items.FindByValue(columnValue);
                            if (item != null)
                            {
                                // drop.ClearSelection();
                                //  item.Selected = true;
                                drop.SelectedValue = columnValue;
                            }
                        }
                    }
                    else if (row["control_type"].ToString() == "ASPXCOMBOBOX")
                    {
                        var drop = (DevExpress.Web.ASPxComboBox)p.FindControl(lscontrolformat + row["control_id"].ToString());
                        if (drop != null)
                        {
                           
                                drop.Value = columnValue;

                        }
                    }
                    else if (row["control_type"].ToString() == "FILEUPLOAD")
                    {
                        var upload = (FileUpload)p.FindControl(lscontrolformat + row["control_id"].ToString().ToLower());
                        var link = (HyperLink)p.FindControl(lscontrolformat + "link_" + row["control_id"].ToString().ToLower());
                        if (link != null && columnValue != "")
                        {
                            link.NavigateUrl = columnValue;
                            var name = columnValue.Substring(columnValue.LastIndexOf(@"\") + 1);
                            link.Text = name;
                            link.Target = "_blank";
                        }
                    }

                }
            }
            // return ls;
        }

        #endregion


        #region 将界面中控件值统计到List中

        /// <summary>
        /// 将界面中控件值统计到List中
        /// </summary>
        /// <param name="lsform_type">要显示字段大类</param>
        /// <param name="lsform_div">要显示字段小类</param>
        /// <param name="p">要统计的界面Page</param>
        /// <param name="lscontrol_format">界面控件中要套用的控件ID格式</param>
        /// <returns></returns>
        public static List<Pgi.Auto.Common> GetControlValue(string lsform_type, string lsform_div, System.Web.UI.Page p, string lscontrol_format = "")
        {
            string lswhere = "";
            if (lsform_type != "")
            {
                lswhere += " and form_type=@form_type";
            }
            if (lsform_div != "")
            {
                lswhere += " and form_div=@form_div";
            }
            string lsconn = ConfigurationSettings.AppSettings["ConnectionMES"];
            DataTable ldt = Pgi.Auto.SQLHelper.ExecuteDataSet(lsconn, CommandType.Text, "select * from auto_form where 1=1 " + lswhere + "",
                new SqlParameter[]{
              new SqlParameter("@form_type",lsform_type)
                ,new SqlParameter("@form_div",lsform_div)}).Tables[0];

            List<Pgi.Auto.Common> ls = new List<Common>();
            for (int i = 0; i < ldt.Rows.Count; i++)
            {
                string lscontrol_id = ldt.Rows[i]["control_id"].ToString().ToLower();
                if (lscontrol_format != "")
                {
                    lscontrol_id = lscontrol_format.Replace("{0}", lscontrol_id);
                }
                if (p.FindControl(lscontrol_id) != null)
                {



                    Pgi.Auto.Common com = new Common();
                    com.Code = ldt.Rows[i]["control_id"].ToString().ToLower();
                    string lstr = "0|0";
                    string initlstr = "0|0";//初始lstr字符，以便忘记配置值而报错
                    //-----------------------------------控件判断开始----------------------------------
                    if (ldt.Rows[i]["control_type"].ToString() == "TEXTBOX")
                    {
                        // ((TextBox)p.FindControl(ldt.Rows[i]["control_id"].ToString())).Enabled = true;
                        // com.Value = ((TextBox)p.FindControl(ldt.Rows[i]["control_id"].ToString())).Text;
                        com.Value = p.Request.Form[lscontrol_id].ToString();
                        ((TextBox)p.FindControl(lscontrol_id)).Text = p.Request.Form[lscontrol_id].ToString();
                        lstr = ((TextBox)p.FindControl(lscontrol_id)).ToolTip.ToString();
                        lstr = lstr == "" ? initlstr : lstr;
                    }
                    else if (ldt.Rows[i]["control_type"].ToString() == "DROPDOWNLIST")
                    {
                        com.Value = ((DropDownList)p.FindControl(lscontrol_id)).SelectedValue;
                        if (com.Value == "")
                        {
                            if (p.Request.Form[lscontrol_id] != null)
                            {
                                com.Value = p.Request.Form[lscontrol_id].ToString();//无法获取
                            }

                        }
                        lstr = ((DropDownList)p.FindControl(lscontrol_id)).ToolTip.ToString();
                        lstr = lstr == "" ? initlstr : lstr;
                    }
                    else if (ldt.Rows[i]["control_type"].ToString() == "ASPXCOMBOBOX")
                    {
                        //com.Value = ((DevExpress.Web.ASPxComboBox)p.FindControl(lscontrol_id)).Text;
                        com.Value = "";
                        if (com.Value == "")
                        {
                            if (p.Request.Form[lscontrol_id] != null)
                            {
                                com.Value = p.Request.Form[lscontrol_id].ToString();//无法获取
                            }

                        }
                        lstr = ((DevExpress.Web.ASPxComboBox)p.FindControl(lscontrol_id)).ToolTip.ToString();
                        lstr = lstr == "" ? initlstr : lstr;
                    }
                    else if (ldt.Rows[i]["control_type"].ToString() == "ASPXDATEEDIT")
                    {
                        com.Value = ((DevExpress.Web.ASPxDateEdit)p.FindControl(lscontrol_id)).Text;
                        if (com.Value == "")
                        {
                            if (p.Request.Form[lscontrol_id] != null)
                            {
                                com.Value = p.Request.Form[lscontrol_id].ToString();//无法获取
                            }

                        }
                        lstr = ((DevExpress.Web.ASPxDateEdit)p.FindControl(lscontrol_id)).ToolTip.ToString();
                        lstr = lstr == "" ? initlstr : lstr;
                    }
                    else if (ldt.Rows[i]["control_type"].ToString() == "ASPXDROPDOWNEDIT")
                    {
                        com.Value = ((DevExpress.Web.ASPxDropDownEdit)p.FindControl(lscontrol_id)).Text;
                        if (com.Value == "")
                        {
                            if (p.Request.Form[lscontrol_id] != null)
                            {
                                com.Value = p.Request.Form[lscontrol_id].ToString();//无法获取
                            }

                        }
                        lstr = ((DevExpress.Web.ASPxDropDownEdit)p.FindControl(lscontrol_id)).ToolTip.ToString();
                        lstr = lstr == "" ? initlstr : lstr;
                    }
                    //-----------------------------------控件判断结束----------------------------------

                    string[] ls1 = lstr.Split('|');
                    com.Key = ls1[0];
                    if (ls1[1] == "1" && com.Value == "")
                    {
                        com.Value = ldt.Rows[i]["control_dest"].ToString();
                        com.Code = "";
                    }
                    ls.Add(com);
                }
            }
            return ls;
        }
        #endregion

        #region  无参数插入一笔记录(单笔新增)

        /// <summary>
        /// 无参数插入一笔记录(单笔插入)
        /// </summary>
        /// <param name="lsconn">链接参数</param>
        /// <param name="ls">画面控件对应的栏位List</param>
        /// <param name="lstable">table</param>
        /// <returns></returns>
        private static string Insert(string lsconn, List<Pgi.Auto.Common> ls, string lstable)
        {

            string lscode = "";
            string lsvalue = "";
            for (int i = 0; i < ls.Count; i++)
            {
                lscode += ls[i].Code;
                lsvalue += ls[i].Value == "" ? "NULL" : "'" + ls[i].Value + "'";
                if (i < ls.Count - 1)
                {
                    lscode += ",";
                    lsvalue += ",";
                }
            }
            string lssql = "insert into " + lstable + " (" + lscode + " ) values (" + lsvalue + ")";
            lssql = lssql + "; SELECT SCOPE_IDENTITY(); ";
            object result = Pgi.Auto.SQLHelper.ExecuteScalar(lsconn, CommandType.Text, lssql, new SqlParameter[] { new SqlParameter("", "") });
            string ln = "";
            if (result != null)
            {
                ln = result.ToString();
            }
            else
            { ln = ""; }

            return ln;
        }

        #endregion

        //public static bool IsNullOrEmpty(this string str)
        //{
        //    return string.IsNullOrWhiteSpace(str) || string.IsNullOrEmpty(str);
        //}

        #region 无参数更新（单笔修改）

        /// <summary>
        /// 无参数更新（单笔修改）
        /// </summary>
        /// <param name="lsconn">链接参数</param>
        /// <param name="ls">画面控件对应的栏位List</param>
        /// <param name="lstable">table</param>
        /// <returns></returns>
        private static int Update(string lsconn, List<Pgi.Auto.Common> ls, string lstable)
        {

            string lssql = "update " + lstable + " set ";
            string lskey = "";
            int ln = 0;
            for (int i = 0; i < ls.Count; i++)
            {
                //附件栏位不更新，除非有新附件
                if (ls[i].Code.ToUpper() != "UPLOAD")
                {
                    var value = ls[i].Value == "" ? "=NULL" : "='" + ls[i].Value + "'";
                    lssql += "" + ls[i].Code + value;
                    if (i < ls.Count - 1)
                    {
                        lssql += ",";
                    }
                    if (ls[i].Key.ToString().Trim() == "1")
                    {
                        lskey += " and " + ls[i].Code + value;
                    }
                }
                else if (ls[i].Code.ToUpper() == "UPLOAD")
                {
                    // if ( ls[i].Value != "" && ls[i].Value != null){
                    var value = string.IsNullOrWhiteSpace(ls[i].Value) == true ? "=" + ls[i].Code : "='" + ls[i].Value + "'";
                    lssql += "" + ls[i].Code + value;

                    if (i < ls.Count - 1)
                    {
                        lssql += ",";
                    }

                    if (ls[i].Key.ToString().Trim() == "1")
                    {
                        lskey += " and " + ls[i].Code + value;
                    }

                    //  }

                }
            }
            lssql += " where 1=1 " + lskey;
            lssql = lssql + "; SELECT SCOPE_IDENTITY(); ";
            if (lskey != "")
            {
                ln = Pgi.Auto.SQLHelper.ExecuteNonQuery(lsconn, CommandType.Text, lssql, new SqlParameter[] { new SqlParameter("", "") });
            }

            return ln;
        }

        #endregion


        #region  单笔更新（自动判断是新增还是修改）

        /// <summary>
        /// 单笔更新（自动判断是新增还是修改）
        /// </summary>
        /// <param name="ls">要更新字段的List</param>
        /// <param name="lstable">要更新的表(Tabel)</param>
        /// <returns></returns>
        public static int UpdateValues(List<Pgi.Auto.Common> ls, string lstable)
        {

            string lsconn = ConfigurationSettings.AppSettings["ConnectionMES"];
            int ln = 0;
            int lnkey = 0;
            string lskey = "";

            if (ls.Count < 1)
            {
                return ln;
            }
            for (int i = 0; i < ls.Count; i++)
            {
                if (ls[i].Key == "1")
                {
                    lskey = lskey + " and  " + ls[i].Code + "='" + ls[i].Value + "'";
                }
            }
            if (lskey != "")
            {
                string lssql = "select count(*) from " + lstable + " where 1=1 " + lskey + "";
                lnkey = Convert.ToInt32(Pgi.Auto.SQLHelper.ExecuteScalar(lsconn, CommandType.Text, lssql, new SqlParameter[] { new SqlParameter("", "") }));

            }
            if (lnkey == 0)
            {
                ln = Convert.ToInt32(Pgi.Auto.Control.Insert(lsconn, ls, lstable));

            }
            else
            {
                ln = Pgi.Auto.Control.Update(lsconn, ls, lstable);
            }

            return ln;

        }

        #endregion


        #region 通过List中字段自动生成SQL语句(单笔，一般适用于表头)

        /// <summary>
        /// 通过List中字段自动生成SQL语句(单笔，一般适用于表头)
        /// </summary>
        /// <param name="ls">要生成的字段List</param>
        /// <param name="lstable">要更新的表</param>
        /// <returns></returns>
        public static Pgi.Auto.Common GetList(List<Pgi.Auto.Common> ls, string lstable)
        {
            Pgi.Auto.Common lreturn = new Common();

            string lsconn = ConfigurationSettings.AppSettings["ConnectionMES"];
            int lnkey = 0;
            string lskey = "";

            if (ls.Count < 1)
            {
                return lreturn;
            }
            for (int i = 0; i < ls.Count; i++)
            {
                if (ls[i].Key == "1")
                {
                    lskey += " and  " + ls[i].Code + "='" + ls[i].Value + "'";
                }
            }
            if (lskey != "")
            {
                string lssql = "select count(*) from " + lstable + " where 1=1 " + lskey + "";
                lnkey = Convert.ToInt32(Pgi.Auto.SQLHelper.ExecuteScalar(lsconn, CommandType.Text, lssql, new SqlParameter[] { new SqlParameter("", "") }));

            }
            if (lnkey > 0)
            {

                lreturn.Sql = "update " + lstable + " set ";
                string lskey1 = "";
                for (int i = 0; i < ls.Count; i++)
                {
                    lreturn.Sql += "" + ls[i].Code + "=@" + ls[i].Code + "";
                    if (i < ls.Count - 1)
                    {
                        lreturn.Sql += ",";
                    }
                    if (ls[i].Key != null)
                    {
                        if (ls[i].Key.ToString().Trim() == "1")
                        {
                            lskey1 += "" + ls[i].Code + "=@" + ls[i].Code + "";
                        }
                    }


                    lreturn.Paras.Add(new SqlParameter("@" + ls[i].Code, ls[i].Value.Trim()));
                }


                lreturn.Sql += " where " + lskey1;

            }
            else
            {
                List<SqlParameter> lparas = new List<SqlParameter>();
                string lscode = "";
                string lsvalue = "";

                for (int i = 0; i < ls.Count; i++)
                {

                    lscode += ls[i].Code;
                    lsvalue += "@" + ls[i].Code + "";


                    if (i < ls.Count - 1)
                    {
                        lscode += ",";
                        lsvalue += ",";
                    }
                    lreturn.Paras.Add(new SqlParameter("@" + ls[i].Code, ls[i].Value));
                }
                lreturn.Sql = "insert into " + lstable + " (" + lscode + " ) values (" + lsvalue + ")";
            }



            return lreturn;

        }

        #endregion


        #region 通过DataTable自动生成SQL语句（多笔，一般适用于表体）
        /// <summary>
        /// 通过DataTable自动生成SQL语句（多笔，一般适用于表体）
        /// </summary>
        /// <param name="ldt">要生成SQL的DataTable</param>
        /// <param name="lstable">要更新的表名(Table)</param>
        /// <param name="lskey">要更新的主键(Key)</param>
        /// <param name="lsremove">从DataTable中要删除的字段</param>
        /// <returns></returns>
        public static List<Pgi.Auto.Common> GetList(DataTable ldt, string lstable, string lskey, string lsremove)
        {
            List<Pgi.Auto.Common> ls_return = new List<Common>();
            //for (int i = 0; i < lagv.VisibleRowCount; i++)
            //{
            //    for (int j = 0; j < lagv.DataColumns.Count; j++)
            //    {
            //        string lsql = "";
            //        if (lagv.DataColumns[j].FieldName==lskey)
            //        {
            //            lsql="update "
            //        }

            //        DevExpress.Web.GridViewDataColumn lc = lagv.Columns[lagv.DataColumns[j].FieldName] as DevExpress.Web.GridViewDataColumn;
            //        DevExpress.Web.ASPxTextBox txt = lagv.FindRowCellTemplateControl(i, lc, lagv.DataColumns[j].FieldName) as DevExpress.Web.ASPxTextBox;
            //        if (txt!=null)
            //        {

            //        }
            //    }
            //}
            string[] lremove = lsremove.Split(',');

            for (int i = 0; i < ldt.Rows.Count; i++)
            {
                Pgi.Auto.Common com = new Common();

                if (ldt.Rows[i][lskey].ToString() != "")
                {
                    int ln_del = 0;
                    for (int j = 0; j < ldt.Columns.Count; j++)
                    {
                        if (ldt.Rows[i][ldt.Columns[j].ColumnName].ToString() != "" && ldt.Columns[j].ColumnName != lskey)
                        {
                            ln_del = 1;
                            continue;
                        }
                    }

                    if (ln_del == 1)
                    {
                        com.Sql = "update " + lstable + " set";
                        //修改
                        for (int j = 0; j < ldt.Columns.Count; j++)
                        {
                            
                            if (((IList)lremove).Contains(ldt.Columns[j].ColumnName) == false && ldt.Columns[j].ColumnName != lskey)
                            {
                                com.Sql += " " + ldt.Columns[j].ColumnName + "=@" + ldt.Columns[j].ColumnName + "";
                                com.Paras.Add(new SqlParameter("@" + ldt.Columns[j].ColumnName, ldt.Rows[i][ldt.Columns[j].ColumnName]));
                                if (j < ldt.Columns.Count - 1)
                                {
                                    com.Sql += ",";
                                }
                            }

                        }
                        if (com.Sql.Substring(com.Sql.Length - 1, 1) == ",")
                        {
                            com.Sql = com.Sql.Substring(0, com.Sql.Length - 1);
                        }
                        com.Sql += " where " + lskey + "=@" + lskey + "";
                        com.Paras.Add(new SqlParameter("@" + lskey, ldt.Rows[i][lskey].ToString()));
                    }
                    else
                    {
                        com.Sql = "delete " + lstable + " where " + lskey + "=@" + lskey + "";
                        com.Paras.Add(new SqlParameter("@" + lskey, ldt.Rows[i][lskey].ToString()));
                    }

                }
                else
                {

                    //新增
                    com.Sql = "insert into " + lstable + " (";
                    for (int j = 0; j < ldt.Columns.Count; j++)
                    {
                        if (ldt.Columns[j].ColumnName != lskey && ((IList)lremove).Contains(ldt.Columns[j].ColumnName) == false)
                        {
                            com.Sql += "" + ldt.Columns[j].ColumnName + "";
                            if (j < ldt.Columns.Count - 1)
                            {
                                com.Sql += ",";
                            }

                        }
                    }
                    if (com.Sql.Substring(com.Sql.Length - 1, 1) == ",")
                    {
                        com.Sql = com.Sql.Substring(0, com.Sql.Length - 1);
                    }
                    com.Sql += " ) values (";
                    for (int j = 0; j < ldt.Columns.Count; j++)
                    {
                        if (ldt.Columns[j].ColumnName != lskey && ((IList)lremove).Contains(ldt.Columns[j].ColumnName) == false)
                        {
                            com.Sql += "@" + ldt.Columns[j].ColumnName;
                            com.Paras.Add(new SqlParameter("@" + ldt.Columns[j].ColumnName, ldt.Rows[i][ldt.Columns[j].ColumnName]));
                            if (j < ldt.Columns.Count - 1)
                            {
                                com.Sql += ",";
                            }

                        }

                    }
                    if (com.Sql.Substring(com.Sql.Length - 1, 1) == ",")
                    {
                        com.Sql = com.Sql.Substring(0, com.Sql.Length - 1);
                    }
                    com.Sql += " ) ";
                }
                ls_return.Add(com);
            }

            return ls_return;
        }
        #endregion


        #region 批量更新SQL
        /// <summary>
        /// 批量更新SQL
        /// </summary>
        /// <param name="ls">多笔SQL</param>
        /// <returns></returns>
        public static int UpdateListValues(List<Pgi.Auto.Common> ls)
        {
            string lsconn = ConfigurationSettings.AppSettings["ConnectionMES"];
            SqlConnection lconn = new SqlConnection(lsconn);
            lconn.Open();
            int ln_return = 0;
            SqlTransaction ltrans = lconn.BeginTransaction();
            try
            {
                for (int i = 0; i < ls.Count; i++)
                {
                    ln_return = Pgi.Auto.SQLHelper.ExecuteNonQuery(ltrans, CommandType.Text, ls[i].Sql, ls[i].Paras.ToArray());

                }
                ltrans.Commit();
            }
            catch (Exception ex)
            {
                ltrans.Rollback();
                throw;
            }

            return ln_return;
        }
        #endregion


        #region AspxGridView转DatatTable

        public static DataTable AgvToDt(DevExpress.Web.ASPxGridView lgrid)
        {
            DataTable ldt = new DataTable();
            for (int i = 0; i < lgrid.DataColumns.Count; i++)
            {

                ldt.Columns.Add(lgrid.DataColumns[i].FieldName);

            }
            //增加标识位
            if (ldt.Columns.IndexOf("flag") <1)
            {
                ldt.Columns.Add("flag");
            }

            for (int i = 0; i < lgrid.VisibleRowCount; i++)
            {

                DataRow ldr = ldt.NewRow();

                
                    if (lgrid.Selection.IsRowSelected(i) == true)
                    {
                        ldr["flag"] = 1;
                    }

                for (int j = 0; j < lgrid.DataColumns.Count; j++)
                {
                   
                    if (lgrid.FindRowCellTemplateControl(i, lgrid.DataColumns[j], lgrid.DataColumns[j].FieldName) is DevExpress.Web.ASPxTextBox)
                    {
                        DevExpress.Web.ASPxTextBox tb1 = (DevExpress.Web.ASPxTextBox)lgrid.FindRowCellTemplateControl(i, lgrid.DataColumns[j], lgrid.DataColumns[j].FieldName);
                        if (tb1.Text != "")
                        {
                            ldr[lgrid.DataColumns[j].FieldName] = tb1.Text;
                        }
                        else
                        {
                            ldr[lgrid.DataColumns[j].FieldName] = DBNull.Value;
                        }

                    }
                    else if (lgrid.FindRowCellTemplateControl(i, lgrid.DataColumns[j], lgrid.DataColumns[j].FieldName) is DevExpress.Web.ASPxGridLookup)
                    {
                        DevExpress.Web.ASPxGridLookup tb1 = (DevExpress.Web.ASPxGridLookup)lgrid.FindRowCellTemplateControl(i, lgrid.DataColumns[j], lgrid.DataColumns[j].FieldName);
                        if (tb1.Text != "")
                        {
                            ldr[lgrid.DataColumns[j].FieldName] = tb1.Text;
                        }
                        else
                        {
                            ldr[lgrid.DataColumns[j].FieldName] = DBNull.Value;
                        }
                    }
                    
                    else if (lgrid.FindRowCellTemplateControl(i, lgrid.DataColumns[j], lgrid.DataColumns[j].FieldName) is TextBox)
                    {
                        TextBox tb1 = (TextBox)lgrid.FindRowCellTemplateControl(i, lgrid.DataColumns[j], lgrid.DataColumns[j].FieldName);
                        if (tb1.Text != "")
                        {
                            ldr[lgrid.DataColumns[j].FieldName] = tb1.Text;
                        }
                        else
                        {
                            ldr[lgrid.DataColumns[j].FieldName] = DBNull.Value;
                        }
                    }
                    else if (lgrid.FindRowCellTemplateControl(i, lgrid.DataColumns[j], lgrid.DataColumns[j].FieldName) is Label)
                    {
                        Label tb1 = (Label)lgrid.FindRowCellTemplateControl(i, lgrid.DataColumns[j], lgrid.DataColumns[j].FieldName);
                        if (tb1.Text != "")
                        {
                            ldr[lgrid.DataColumns[j].FieldName] = tb1.Text;
                        }
                        else
                        {
                            ldr[lgrid.DataColumns[j].FieldName] = DBNull.Value;
                        }
                    }
                    else if (lgrid.FindRowCellTemplateControl(i, lgrid.DataColumns[j], lgrid.DataColumns[j].FieldName) is DropDownList)
                    {
                        DropDownList tb1 = (DropDownList)lgrid.FindRowCellTemplateControl(i, lgrid.DataColumns[j], lgrid.DataColumns[j].FieldName);
                        if (tb1.Text != "")
                        {
                            ldr[lgrid.DataColumns[j].FieldName] = tb1.Text;
                        }
                        else
                        {
                            ldr[lgrid.DataColumns[j].FieldName] = DBNull.Value;
                        }
                    }
                    else if (lgrid.FindRowCellTemplateControl(i, lgrid.DataColumns[j], lgrid.DataColumns[j].FieldName) is HyperLink)
                    {
                        HyperLink tb1 = (HyperLink)lgrid.FindRowCellTemplateControl(i, lgrid.DataColumns[j], lgrid.DataColumns[j].FieldName);
                        if (tb1.Text != "")
                        {
                            ldr[lgrid.DataColumns[j].FieldName] = tb1.Text;
                        }
                        else
                        {
                            ldr[lgrid.DataColumns[j].FieldName] = DBNull.Value;
                        }
                    }
                    else if (lgrid.FindRowCellTemplateControl(i, lgrid.DataColumns[j], lgrid.DataColumns[j].FieldName) is DevExpress.Web.ASPxDateEdit)
                    {
                        DevExpress.Web.ASPxDateEdit tb1 = (DevExpress.Web.ASPxDateEdit)lgrid.FindRowCellTemplateControl(i, lgrid.DataColumns[j], lgrid.DataColumns[j].FieldName);
                        if (tb1.Text != "")
                        {
                            ldr[lgrid.DataColumns[j].FieldName] = tb1.Text;
                        }
                        else
                        {
                            ldr[lgrid.DataColumns[j].FieldName] = DBNull.Value;
                        }
                    }
                    else if (lgrid.FindRowCellTemplateControl(i, lgrid.DataColumns[j], lgrid.DataColumns[j].FieldName) is DevExpress.Web.ASPxComboBox)
                    {
                        DevExpress.Web.ASPxComboBox tb1 = (DevExpress.Web.ASPxComboBox)lgrid.FindRowCellTemplateControl(i, lgrid.DataColumns[j], lgrid.DataColumns[j].FieldName);
                        if (tb1.Text != "")
                        {
                            ldr[lgrid.DataColumns[j].FieldName] = tb1.Text;
                        }
                        else
                        {
                            ldr[lgrid.DataColumns[j].FieldName] = DBNull.Value;
                        }
                    }
                    else
                    {
                        if (ldt.Columns[j].ColumnName != "SelectAll")
                        {
                            if (lgrid.GetRowValues(i, lgrid.DataColumns[j].FieldName) != null)
                            {
                                if (lgrid.GetRowValues(i, lgrid.DataColumns[j].FieldName).ToString() == "")
                                {
                                    ldr[lgrid.DataColumns[j].FieldName] = DBNull.Value;
                                }
                                else
                                {
                                    ldr[lgrid.DataColumns[j].FieldName] = lgrid.GetRowValues(i, lgrid.DataColumns[j].FieldName);
                                }

                            }
                        }
                        else
                        {
                            CheckBox cb = (CheckBox)lgrid.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)lgrid.Columns["SelectAll"], "txtcb");
                            if (cb.Checked)
                            {
                                ldr[ldt.Columns[j].ColumnName] = 1;
                            }
                            else
                            {
                                ldr[ldt.Columns[j].ColumnName] = 0;
                            }

                        }


                    }
                }
                ldt.Rows.Add(ldr);
            }
            return ldt;
        }

        #endregion

    }
}
