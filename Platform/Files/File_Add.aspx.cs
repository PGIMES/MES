﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebForm.Platform.Files
{
    public partial class File_Add : Common.BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected override bool CheckApp()
        {
            return true;// base.CheckApp();
        }
    }
}