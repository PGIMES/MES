using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

namespace Pgi.Auto
{
    public class ColumnTemplate : ITemplate
    {
        //string ctype = "";
        //string cid = "";
        //string cwidth = "";
        DataRow dr;
        public ColumnTemplate(DataRow ldr)
        {
            this.dr = ldr;
        }
        public void InstantiateIn(System.Web.UI.Control container) //关键是这个方法
        {
            
            if (this.dr["list_type"].ToString() == "ASPXTEXTBOX")
            {
                var ltxt = new DevExpress.Web.ASPxTextBox();
                ltxt.ID = this.dr["list_fieldname"].ToString();
                ltxt.Width = int.Parse(this.dr["list_width"].ToString());
                ltxt.ForeColor = System.Drawing.Color.Black;
                container.Controls.Add(ltxt);
            }
            else if (this.dr["list_type"].ToString() == "DROPDOWNLIST")
            {
                var ltxt = new DropDownList();
                ltxt.ID = this.dr["list_fieldname"].ToString();
                ltxt.Width = int.Parse(this.dr["list_width"].ToString());
                ltxt.ForeColor = System.Drawing.Color.Black;
                container.Controls.Add(ltxt);
            }
            else if (this.dr["list_type"].ToString() == "TEXTBOX")
            {
                var ltxt = new TextBox();
                ltxt.ID = this.dr["list_fieldname"].ToString();
                ltxt.Width = int.Parse(this.dr["list_width"].ToString());
                ltxt.ForeColor = System.Drawing.Color.Black;
                if (dr["list_type_ref"].ToString() == "BORDERSTYLE.NONE")
                {
                    ltxt.BorderStyle = BorderStyle.None;
                }
                
                if (dr["list_type_ref"].ToString()== "AutoPostBack")
                {
                    ltxt.AutoPostBack = true;
                }
             

                container.Controls.Add(ltxt);
            }
            else if (this.dr["list_type"].ToString() == "ASPXBUTTON")
            {
                var ltxt = new DevExpress.Web.ASPxButton();
                ltxt.ID = this.dr["list_fieldname"].ToString();
                ltxt.Width = int.Parse(this.dr["list_width"].ToString());
                ltxt.ForeColor = System.Drawing.Color.Black;
                ltxt.CommandName = this.dr["list_type_ref"].ToString();
                ltxt.Text= this.dr["list_caption"].ToString();
                container.Controls.Add(ltxt);
            }
            else if (this.dr["list_type"].ToString() == "ASPXDATEEDIT")
            {
                var ltxt = new DevExpress.Web.ASPxDateEdit();
                ltxt.ID = this.dr["list_fieldname"].ToString();
                ltxt.Width = int.Parse(this.dr["list_width"].ToString());
                ltxt.ForeColor = System.Drawing.Color.Black;
                ltxt.Text = this.dr["list_caption"].ToString();
               
                ltxt.EditFormat = DevExpress.Web.EditFormat.Date;
                container.Controls.Add(ltxt);
            }
            else if (this.dr["list_type"].ToString() == "ASPXCOMBOBOX")
            {
                var ltxt = new DevExpress.Web.ASPxComboBox();
                ltxt.ID = this.dr["list_fieldname"].ToString();
               // ltxt.Width = int.Parse(this.dr["list_width"].ToString());
                ltxt.ForeColor = System.Drawing.Color.Black;
                ltxt.Text = this.dr["list_caption"].ToString();
                if (dr["list_type_ref"].ToString() == "DROPDOWN")
                {
                    ltxt.DropDownStyle = DevExpress.Web.DropDownStyle.DropDown;
                }
                container.Controls.Add(ltxt);
            }
            else if (this.dr["list_type"].ToString() == "HYPERLINK")
            {
                var ltxt = new HyperLink();
                ltxt.ID = this.dr["list_fieldname"].ToString();
                ltxt.ForeColor = System.Drawing.Color.Black;
                ltxt.Text = this.dr["list_caption"].ToString();
                ltxt.Target = "blank";
                container.Controls.Add(ltxt);
            }
            else if (this.dr["list_type"].ToString() == "LABEL")
            {
                var ltxt = new Label();

                ltxt.ID = this.dr["list_fieldname"].ToString();
                ltxt.Width = int.Parse(this.dr["list_width"].ToString());
                ltxt.ForeColor = System.Drawing.Color.Black;
                container.Controls.Add(ltxt);
            }






        }
        //
    }
}
