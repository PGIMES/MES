﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.IO.Ports;
using System.Text;
public partial class test : System.Web.UI.Page
{
    SerialPort sp;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            sp = new SerialPort("COM8", 9600, Parity.None, 8, StopBits.One);
           // sp.DataReceived += new SerialDataReceivedEventHandler(sp_DataReceived);
           // 获取所有的串口
           string[]  PortsName = SerialPort.GetPortNames();
            //Array.Sort(PortsName);
            //foreach (string s in PortsName)
            //{
            //    this.ddlPortName.Items.Add(s);
            //}
           //sp.ReceivedBytesThreshold = 1;
           //sp.Close();
           //sp.Open();
         
            /////////////

          // sp.Close();
          // sp.ReadBufferSize = 512;
          // sp.ReceivedBytesThreshold = 1;
          // sp.Open();
          //byte[] bytes = new byte[1024];

          // string data = "";
          // if (sp.IsOpen)
          // {
          //     try
          //     {
          //         if (sp.BytesToRead > 0)
          //         {
          //             byte[] readBuffer = new byte[sp.ReadBufferSize];
          //             sp.Read(readBuffer, 0, readBuffer.Length);


          //            // int DataLength = sp.Read(bytes, 0, sp.BytesToRead);

          //             //string[] a = new string[] { Encoding.Unicode.GetString(readBuffer) };

          //             string test=sp.ReadLine();

          //             //string data1 = Encoding.Unicode.GetString(readBuffer);
          //             //data = data1 + data;
          //             //TextBox2.Text = data;
          //         }
          //     }
          //     catch (Exception ex)
          //     {
          //         Session["value"] = "发生异常错误:" + ex.Message;
          //     }
          // }
            /////////////
        }
    }

    public void sp_DataReceived(object sender, EventArgs e)
    {
        byte[] bytes = new byte[1024];
        string data="";
        if (sp.IsOpen)
        {
            try
            {
                if (sp.BytesToRead > 0)
                {
                    //int DataLength = sp.Read(bytes, 0, sp.BytesToRead);
                    //string data1= Encoding.Unicode.GetString(bytes);
                    //data=data1+data;
                    //TextBox2.Text = data;
                    TextBox2.Text = sp.ReadLine();
                }
            }
            catch (Exception ex)
            {
                Session["value"] = "发生异常错误:" + ex.Message;
            }
        }
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        SerialPort sp = new SerialPort("COM8", 9600, Parity.None, 8, StopBits.One);
        sp.Close();
        sp.ReadBufferSize = 4096;
        sp.ReceivedBytesThreshold = 1;
        sp.Open();
        byte[] bytes = new byte[1024];

        if (sp.IsOpen)
        {
            try
            {
                if (sp.BytesToRead > 0)
                {
                    //int DataLength = sp.Read(bytes, 0, sp.BytesToRead);
                    //TextBox2.Text = Encoding.Unicode.GetString(bytes);
                    TextBox2.Text = sp.ReadLine();
                }

                
            }
            catch (Exception ex)
            {
                Session["value"] = "发生异常错误:" + ex.Message;
            }
            finally
            {
                sp.Close();
            }
        }

        //while (true)
        //{
        //    try
        //    {
        //        Databuffer += sp.ReadExisting();
        //    }
        //    catch (TimeoutException) { }
        //}  

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "GPSGetDataTimer()", true);

    }

    protected void TextBox3_TextChanged(object sender, EventArgs e)
    {
        decimal midu_value = 0; decimal f = 0;
        if (decimal.TryParse(TextBox3.Text,out f) == true && decimal.TryParse(TextBox4.Text, out f) == true)
        {
            //midu_value = Math.Round(decimal.Parse(txt_kq.Text) *decimal.Parse( lb_ms.Text.Replace("ρ=", "")) / (decimal.Parse(txt_kq.Text) - decimal.Parse(txt_water.Text)), 2);
            //modify by 20190329:数值的区别就在小数点第三位，只取两位不够精确
            //midu_value = Math.Round(decimal.Parse(TextBox3.Text) * decimal.Parse("0.9824100") / (decimal.Parse(TextBox3.Text) - decimal.Parse(TextBox4.Text)), 3, MidpointRounding.AwayFromZero);
            midu_value = Math.Round(decimal.Parse(TextBox3.Text) * decimal.Parse("0.9842100") / (decimal.Parse(TextBox3.Text) - decimal.Parse(TextBox4.Text)), 3);
            TextBox5.Text = midu_value.ToString();
        }
    }
}