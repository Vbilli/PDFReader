using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.IO;

namespace PDF_reader
{
    public partial class Form1 : Form
    {

        string PicPath = @"e:\picture\";
        public Form1()
        {
            InitializeComponent();
            pictureBox2.Image = Resource1.Delete;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DirectoryInfo dir = new DirectoryInfo(Application.StartupPath);
            List<string> imageLists = new List<string>();
            string ImageLocation = "";
            imageList1.Images.Clear();
            listView1.Items.Clear();
            imageLists.Clear();

            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "PDFfiles|*.pdf";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                axAcroPDF1.LoadFile(ofd.FileName);
                PDFAnlyer.ConvertPDF2Image(ofd.FileName, PicPath, "captrue", 1, 500, ImageFormat.Jpeg, PDFAnlyer.Definition.Two);
                for (int i = 1; i < PDFAnlyer.PageCount; i++)
                {
                    PictureBox pb = new PictureBox();
                    //pb.Load(Application.StartupPath + "capture" + i + "." + ImageFormat.Jpeg);
                    //Image imagefile = Image.FromFile(Application.StartupPath + "capture" + i + "." + ImageFormat.Jpeg);
                    //pb.Image = imagefile;
                    ImageLocation = PicPath + "captrue" + i + "." + ImageFormat.Jpeg;
                    pb.SizeMode = PictureBoxSizeMode.AutoSize;
                    imageLists.Add(ImageLocation);
                }

            }


            for (int i = 0; i < imageLists.Count; i++)
            {

                imageList1.Images.Add(System.Drawing.Image.FromFile(imageLists[i].ToString()));
                imageList1.ImageSize = new Size(100, 100);
                listView1.LargeImageList = imageList1;
                listView1.Items.Add(System.IO.Path.GetFileName(imageLists[i].ToString()), i);
                listView1.Items[i].ImageIndex = i;
                listView1.Items[i].Name = imageLists[i].ToString();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            _SCREEN_CAPTURE.FrmCapture capture = new _SCREEN_CAPTURE.FrmCapture();
            capture.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            MessageBox.Show(this.listView1.SelectedItems[0].Name);
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                 pictureBox1.Image = Image.FromFile(listView1.SelectedItems[0].Name);
            }
        }

        private void listView1_MouseHover(object sender, EventArgs e)
        {
            if(listView1.SelectedItems.Count>0)
            {
                int listX =listView1.Location.X +listView1.SelectedItems[0].Position.X ;
                int listY =listView1.Location.Y + listView1.SelectedItems[0].Position.Y;
                pictureBox2.Location = new Point(listX, listY);
                 //listView1.SelectedItems[0].Position.X
            }
             
        }


    }
}
