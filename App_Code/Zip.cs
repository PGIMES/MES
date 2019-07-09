using ICSharpCode.SharpZipLib.Core;
using ICSharpCode.SharpZipLib.Zip;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

/// <summary>
/// Zip 的摘要说明
/// </summary>
public class Zip
{
    public Zip()
    {
        //
        // TODO: 在此处添加构造函数逻辑
        //
    }

    //全部下载
    public string GetFile(string files, string MapPath, string m_sid)
    {
        string[] ls_files = files.Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries);

        //string wjj = m_sid == "" ? DateTime.Now.ToString("yyyyMMdd") : m_sid;
        //string tmp_dir = MapPath + @"\UploadFile\Purchase\" + wjj + @"\zip_temp\";


        string wjj = m_sid == "" ? DateTime.Now.ToString("yyyyMMdd") : m_sid;
        string tmp_dir = MapPath + @"\ExportFile\Zip\" + wjj + @"\";

        if (!Directory.Exists(tmp_dir))
        {
            Directory.CreateDirectory(tmp_dir);
        }
        string filename = DateTime.Now.ToString("yyyyMMddhhmmssff") + ".zip";
        string filepath_zip = tmp_dir + filename;

        string newfilename = string.Empty;
        string sourcefile = string.Empty;

        ZipEntryFactory zipEntryFactory = new ZipEntryFactory();
        using (ZipOutputStream outPutStream = new ZipOutputStream(System.IO.File.Create(filepath_zip)))
        {
            outPutStream.SetLevel(5);
            ZipEntry entry = null;
            byte[] buffer = null;

            for (int i = 0; i < ls_files.Length; i++)
            {
                string[] ls_files_2 = ls_files[i].Split(',');
                if (ls_files_2.Length == 3)
                {
                    newfilename = ls_files_2[0].ToString();
                    sourcefile = MapPath + ls_files_2[1].ToString();
                }
                else//之前的文件，只有一个路径
                {

                    string s = ls_files_2[0].ToString();
                    string[] ss = s.Split(new string[] { @"\" }, StringSplitOptions.RemoveEmptyEntries);
                    newfilename = ss[ss.Length - 1].ToString(); //"文件浏览";

                    sourcefile = MapPath + ls_files_2[0].ToString();
                }

                buffer = new byte[4096];
                entry = zipEntryFactory.MakeFileEntry(newfilename);
                outPutStream.PutNextEntry(entry);
                using (FileStream fileStream = File.OpenRead(sourcefile))
                {
                    StreamUtils.Copy(fileStream, outPutStream, buffer);
                }
            }
            outPutStream.Finish();
            outPutStream.Close();
        }

        return filepath_zip;
    }
}