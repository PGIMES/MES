using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

public partial class GetFilesFromServer : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        string sourcePath=@"\\172.16.9.22\c$\Spectro Smart Studio\Sample Results\" ;
        string bakPath = @"\\172.16.9.22\c$\Spectro Smart Studio\Sample Results Report\";
        string destPath = @"D:\Project\MES\MES\guangpuDoc\";//D:\MES\MES\guangpuDoc\
        Copy_data("192.168.2.87", destPath, sourcePath,bakPath, "pgi", "pgi");
    }


    private void Copy_data(string client_ip, string destPath, string sourcePath,string bakPath, string  user, string  password)
    {
        ValidateUser connect = new ValidateUser();

        bool isImpersonated = false;
        try
        {
            if (connect.impersonateValidUser(user, client_ip, password))
            {
                isImpersonated = true;
                //do what you want now, as the special user                
                DirectoryInfo di = new DirectoryInfo(sourcePath);
                FileInfo[] rgFiles = di.GetFiles("*.xml");
                foreach (FileInfo fi in rgFiles)
                {                  
                    if(Convert.ToInt32(fi.CreationTime.ToString("yyyyMMdd"))>=Convert.ToInt32(DateTime.Now.AddDays(-1).ToString("yyyyMMdd")))
                    {
                        if (File.Exists(fi.FullName))
                        {
                            string filename =  DateTime.Now.ToString("yyyyMMddHHmmssff")+"_"+fi.Name;
                            File.Copy(fi.FullName, destPath + filename);
                            bakPath = bakPath + filename;//fi.Name.Substring(0, fi.Name.LastIndexOf("."))+ ".xml"
                            File.Move(fi.FullName, bakPath);
                        }
                      
                    }                    
                }
            }
        }
        catch
        {           
            //    BLL.path_config.path_configUpdate(client_ip, "异常");
           
        }
        finally
        {
            if (isImpersonated)
                connect.undoImpersonation();
        }



    }

}