using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using PDFReport;
public partial class a_iTextSharp : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        addWater();
    }
    private void addWater()
    {
        string source = @"D:\Sample\sourcepdf.pdf"; //模板路径
        string output = @"D:\Sample\newfile"+DateTime.Now.ToString("HHmmss")+".pdf"; //导出水印背景后的PDF
        string watermark = @"D:\Sample\pgi.gif";   // 水印图片

       // PDFWatermark(source, output, watermark, 100, 100);
       
       // writerText(source, output, watermark);
        SetWatermark(source, output, "严禁外泄水印0916", watermark);
    }

    /// <summary>
    /// 添加普通偏转角度文字水印
    /// </summary>
    public static void SetWatermark(string filePath,string output, string text,string waterImg)
    {
        PdfReader pdfReader = null;
        PdfStamper pdfStamper = null;
        string tempPath = Path.GetDirectoryName(filePath) + Path.GetFileNameWithoutExtension(filePath) + "_temp.pdf";

        try
        {
            pdfReader = new PdfReader(filePath);
            pdfStamper = new PdfStamper(pdfReader, new FileStream(tempPath, FileMode.Create));
            int total = pdfReader.NumberOfPages + 1;
            iTextSharp.text.Rectangle psize = pdfReader.GetPageSize(1);
            float width = psize.Width;
            float height = psize.Height;
            PdfContentByte content;
            BaseFont font = BaseFont.CreateFont(@"C:\WINDOWS\Fonts\SIMFANG.TTF", BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
            PdfGState gs = new PdfGState();
            for (int i = 1; i < total; i++)
            {
                content = pdfStamper.GetOverContent(i);//在内容上方加水印
                                                       //content = pdfStamper.GetUnderContent(i);//在内容下方加水印
                                                       //透明度
                gs.FillOpacity = 0.3f;
                content.SetGState(gs);
                //content.SetGrayFill(0.3f);
                //开始写入文本
                content.BeginText();
                content.SetColorFill(Color.RED);
                content.SetFontAndSize(font, 30);
                content.SetTextMatrix(0, 0);
                content.ShowTextAligned(Element.ALIGN_CENTER, text, width - 120, height - 120, -45);
                //content.SetColorFill(BaseColor.BLACK);
                //content.SetFontAndSize(font, 8);
                //content.ShowTextAligned(Element.ALIGN_CENTER, waterMarkName, 0, 0, 0);
                content.EndText();


            }
            //加入底部文字
            for (int i = 1; i < total; i++)
            {
                AddFooter(pdfStamper, i, "编辑(Edited By): Fish1   审核(Aproved By): Mr Tao    发行日(Published Date): 2020-09-22" );
            }

            PdfContentByte waterMarkContent;
            iTextSharp.text.Image image = iTextSharp.text.Image.GetInstance(waterImg);

            //image.GrayFill = 50;//透明度，灰色填充
                                //image.Rotation//旋转
                                //image.RotationDegrees//旋转角度
            image.SetAbsolutePosition(200, 100); //水印的位置 
            //if (left < 0)
            //{
            //    // left = width - image.Width + left;   //h////////////
            //}
            //image.SetAbsolutePosition(left, (height - image.Height) - top);     //h////////////
            
            //每一页加水印,也可以设置某一页加水印 
            for (int i = 1; i < total; i++)
            {
                waterMarkContent = pdfStamper.GetOverContent(i);
                waterMarkContent.AddImage(image);
            }


        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            if (pdfStamper != null)
                pdfStamper.Close();

            if (pdfReader != null)
                pdfReader.Close();
            System.IO.File.Copy(tempPath, output, true);
            //System.IO.File.Delete(tempPath);
        }
    }

    
    /// <summary>
    /// 加图片水印
    /// </summary>
    /// <param name="inputfilepath"></param>
    /// <param name="outputfilepath"></param>
    /// <param name="ModelPicName"></param>
    /// <param name="top"></param>
    /// <param name="left"></param>
    /// <returns></returns>
    public static bool PDFWatermark(string inputfilepath, string outputfilepath, string ModelPicName, float top, float left)
    {
        //throw new NotImplementedException();
        PdfReader pdfReader = null;
        PdfStamper pdfStamper = null;
        try
        {
            pdfReader = new PdfReader(inputfilepath);
            int numberOfPages = pdfReader.NumberOfPages;
            iTextSharp.text.Rectangle psize = pdfReader.GetPageSize(1);
            float width = psize.Width;
            float height = psize.Height;
            pdfStamper = new PdfStamper(pdfReader, new FileStream(outputfilepath, FileMode.Create));
            PdfContentByte waterMarkContent;
            iTextSharp.text.Image image = iTextSharp.text.Image.GetInstance(ModelPicName);
            image.GrayFill = 20;//透明度，灰色填充
                                //image.Rotation//旋转
                                //image.RotationDegrees//旋转角度
                                //水印的位置 
            if (left < 0)
            {
                left = width / 2 - image.Width + left;
            }
            //image.SetAbsolutePosition(left, (height - image.Height) - top);
            image.SetAbsolutePosition(left, (height / 2 - image.Height) - top);
            //每一页加水印,也可以设置某一页加水印 
            for (int i = 1; i <= numberOfPages; i++)
            {
                //waterMarkContent = pdfStamper.GetUnderContent(i);//内容下层加水印
                waterMarkContent = pdfStamper.GetOverContent(i);//内容上层加水印
                waterMarkContent.AddImage(image);
            }
            //strMsg = "success";
            return true;
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            if (pdfStamper != null)
                pdfStamper.Close();
            if (pdfReader != null)
                pdfReader.Close();
        }
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="outputfilepath"></param>
    public void writerText(string inputfilepath, string outputfilepath, string ModelPicName)
    {
        PdfReader pdfReader = new PdfReader(inputfilepath);
        
        Rectangle pageSize = new Rectangle(1000, 500);
        Document document = new Document(pageSize, 10, 10, 120, 80);
        PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(outputfilepath, FileMode.Create));
        document.Open();

        addImg(ModelPicName, writer);

        document.Add(new iTextSharp.text.Paragraph("Hello World! Hello People! " +
                    "Hello Sky! Hello Sun! Hello Moon! Hello Stars!"));

        writeInfo(document);
        
        document.Close();
        writer.Close();
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="document"></param>
    public void writeInfo(iTextSharp.text.Document document)
    {
        document.AddTitle("这里是标题");
        document.AddSubject("主题");
        document.AddKeywords("关键字");
        document.AddCreator("创建者");
        document.AddAuthor("作者");
    }

    public void addImg(string imgPath, PdfWriter writer)
    {
        string imgurl = imgPath;// @System.Web.HttpContext.Current.Server.MapPath(imgPath);
        iTextSharp.text.Image img = iTextSharp.text.Image.GetInstance(imgurl);
        img.SetAbsolutePosition(0, 0);
        writer.DirectContent.AddImage(img);
    }

    static void AddHeader(PdfStamper stamp, int pageNum, int pageCount)
    {

        PdfContentByte cb = stamp.GetOverContent(pageNum);

        Color green = new Color(0, 131, 34);
        BaseFont bf = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);

        var header = new Rectangle(50, 795, 570, 815);
        header.BackgroundColor = green;
        header.BorderColor = green;
        cb.Rectangle(header);
        cb.Stroke();

        var footer = new Rectangle(50, 35, 570, 55);
        footer.BackgroundColor = green;
        footer.BorderColor = green;
        cb.Rectangle(footer);
        cb.Stroke();

        cb.SetRGBColorFill(255, 255, 255);

        cb.BeginText();
        cb.SetFontAndSize(bf, 12);
        cb.SetTextMatrix(70, 800);

        cb.ShowText("U als ondernemer");
        cb.SetTextMatrix(550 - bf.GetWidthPoint("Resultaten", 12), 800);
        cb.ShowText("Resultaten");
        cb.EndText();

        String pageText = string.Format("Page {0} of  {1}", pageNum, pageCount);
        float pageLen = bf.GetWidthPoint(pageText, 12);
        cb.BeginText();
        cb.SetFontAndSize(bf, 12);
        cb.SetTextMatrix(280, 40);
        cb.ShowText(pageText);
        cb.EndText();
        cb.ResetRGBColorFill();
    }

    static void AddFooter(PdfStamper stamp, int pageNum, string strText)
    {

        PdfContentByte cb = stamp.GetOverContent(pageNum);

        Color green = new Color(0, 131, 34);
        BaseFont bf = BaseFont.CreateFont(@"C:\WINDOWS\Fonts\SIMFANG.TTF", BaseFont.IDENTITY_H, BaseFont.EMBEDDED);// BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);

        //var header = new Rectangle(50, 795, 570, 815);
        ////header.BackgroundColor = green;
        //header.BorderColor = green;
        //cb.Rectangle(header);
        //cb.Stroke();

        var footer = new Rectangle(50, 35, 570, 55);
        //footer.BackgroundColor = green;
        footer.BorderColor = green;
        footer.BorderWidth = 1;
        cb.Rectangle(footer);
        cb.Stroke();

        cb.SetRGBColorFill(0, 0, 0);

        //cb.BeginText();
        //cb.SetFontAndSize(bf, 12);
        //cb.SetTextMatrix(70, 800);

        //cb.ShowText("U als ondernemer");
        //cb.SetTextMatrix(550 - bf.GetWidthPoint("Resultaten", 12), 800);
        //cb.ShowText("Resultaten");
        //cb.EndText();

        String pageText = string.Format("{0}", strText);
        float pageLen = bf.GetWidthPoint(pageText, 12);
        cb.BeginText();
        cb.SetFontAndSize(bf, 12);
        cb.SetTextMatrix(120, 40);
        cb.ShowText(pageText);
        cb.EndText();
        //cb.ResetRGBColorFill();
    }

}