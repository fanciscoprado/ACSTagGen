using PdfSharp.Drawing;
using PdfSharp.Pdf;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MangerTagGen
{
    internal class Algo
    {
        BarcodeLib.Barcode b = new BarcodeLib.Barcode();
        List<string> history = new List<string>();
        string[,] array = new string[3, 6];
        
        //Constructor
        public Algo()
        {
            int col = 72;
            for(int i = 0; i < 3; i++)
            {
                if (i == 0)
                    ;
                else
                    col += 154; //154 og
                int row = 64;
                for(int j = 0; j < 6; j++)
                {
                    if (j == 0)
                        ;
                    else
                    {
                        row += 130; //104 ot
                    }
                    array[i, j] = col + "," + row;
                }
            }
        }

        public bool printBarcode(PictureBox p, string opNumber, string psswd)
        {
            if(opNumber.Length < 3)
            {
                if(opNumber.Length < 2)
                {
                    opNumber = '0' + '0' + opNumber;
                }else
                    opNumber = '0' + opNumber;
            }
            if (opNumber.Length > 3 || opNumber.Length < 3 || opNumber.Length == 0)
                return false;
            if (psswd.Length > 2 || psswd.Length <= 0)
                return false;
            if (psswd.Length < 2 ) 
                psswd = "0" + psswd;

            if (IsDigitsOnly(psswd) && IsDigitsOnly(opNumber))
            {
                history.Add(opNumber + "," + psswd);
                string upc = "412" + opNumber + psswd + "000";
                Image img = b.Encode(BarcodeLib.TYPE.UPCA, upc, Color.Black, Color.White, 401, 192);
                p.Image = img;
                return true;
            }
            return false; 
        }

        public void updateList(int index)
        {

            history.RemoveAt(index);
        }
        
        public void updateBarcode(PictureBox p, int index)
        {

            string[] selected = history[index].Split(',');
            string upc = "412" + selected[0] + selected[1] + "000";
            Image img = b.Encode(BarcodeLib.TYPE.UPCA, upc, Color.Black, Color.White, 401, 192);
            p.Image = img;
        }

        public bool IsDigitsOnly(string str)
        {
            foreach (char c in str)
            {
                if (c < '0' || c > '9')
                    return false;
            }

            return true;
        }

        public void creatPDF(bool showNum, bool saveAll, int labelIndex = 0 )
        {
            // Create a new PDF document
            PdfDocument document = new PdfDocument();
            document.Info.Title = "Manager Tags";

            // Create an empty page
            PdfPage page = document.AddPage();

            // Get an XGraphics object for drawing
            XGraphics gfx = XGraphics.FromPdfPage(page);

            // Create a font
            XFont font = new XFont("Verdana", 10, XFontStyle.BoldItalic);

            if (saveAll)
            {
                int lIndex = 0;
                int i = 0;
                for(i = 0; i < 6; i++)
                {
                    int j = 0;

                    for(j = 0; j < 3; j++)
                    {
                        if (lIndex == history.Count)
                            break;
                        else
                        {
                            string[] location = array[j, i].Split(',');
                            string[] selected = history[lIndex].Split(',');
                            string upc = "412" + selected[0] + selected[1] + "000";
                            Image img = b.Encode(BarcodeLib.TYPE.UPCA, upc, Color.Black, Color.White, 401, 192);
                            MemoryStream strm = new MemoryStream();
                            img.Save(strm, System.Drawing.Imaging.ImageFormat.Png);
                            XImage xfoto = XImage.FromStream(strm);
                            if (showNum)
                            {
                                gfx.DrawString(selected[0], font, XBrushes.Black,
                                new XRect(Int16.Parse(location[0]), Int16.Parse(location[1]) + 50, 114, 50),
                                XStringFormats.TopCenter);
                            }
                            gfx.DrawImage(xfoto, Int16.Parse(location[0]), Int16.Parse(location[1]), 114, 51);
                            lIndex++;
                        }

                    }
                }

            }
            else
            {
                
                string[] selected = history[labelIndex].Split(',');
                string upc = "412" + selected[0] + selected[1] + "000";
                Image img = b.Encode(BarcodeLib.TYPE.UPCA, upc, Color.Black, Color.White, 401, 192);
                MemoryStream strm = new MemoryStream();
                img.Save(strm, System.Drawing.Imaging.ImageFormat.Png);
                XImage xfoto = XImage.FromStream(strm);
                if (showNum)
                {
                    gfx.DrawString(selected[0], font, XBrushes.Black,
                    new XRect(72, 64 + 50, 114, 50),
                    XStringFormats.TopCenter);
                }
                gfx.DrawImage(xfoto, 72, 64, 114, 51);
            }
                       
            // Save the document...
            const string filename = "Manager Tags";
            document.Save(filename);
            // ...and start a viewer.
            Process.Start(filename);

        }
        
    }
}
