using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Imaging;

namespace PDF_reader
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "PDFfiles|*.pdf";
            if(ofd.ShowDialog() == DialogResult.OK)
            {
                axAcroPDF1.LoadFile(ofd.FileName);
                PDFAnlyer.ConvertPDF2Image(ofd.FileName, Application.StartupPath,"captrue", 1, 500, ImageFormat.Jpeg, PDFAnlyer.Definition.Seven);
                for(int i=1 ; i<PDFAnlyer.PageCount;i++)
                {
                    PictureBox pb = new PictureBox();
                    //pb.Load(Application.StartupPath + "capture" + i + "." + ImageFormat.Jpeg);
                    //pb.Image = Image.FromFile(Application.StartupPath + "capture" + i + "." + ImageFormat.Jpeg);
                    pb.ImageLocation = Application.StartupPath + "capture" + i + "." + ImageFormat.Jpeg;
                    pb.SizeMode = PictureBoxSizeMode.AutoSize;
                    flowLayoutPanel1.Controls.Add(pb);
                }
                
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            _SCREEN_CAPTURE.FrmCapture capture = new _SCREEN_CAPTURE.FrmCapture();
            capture.Show();
        }
    }
}
