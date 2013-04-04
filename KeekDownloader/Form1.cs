using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;

namespace KeekDownloader
{
    public partial class Form1 : Form
    {
        public static String filename;
        public static Boolean multi = false;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        void client_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            if (!multi)
            {
                System.Diagnostics.Process.Start(filename);
                textBox1.Focus();
            }
        }

        void client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            progressBar1.Maximum = (int)e.TotalBytesToReceive / 100;
            progressBar1.Value = (int)e.BytesReceived / 100;
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            multi = false;
            progressBar1.Value = 0;
            String download_dir = @"" + Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\keek_downloads\\";
            bool IsExists = System.IO.Directory.Exists(download_dir);

            if (!IsExists)
                System.IO.Directory.CreateDirectory(download_dir);

            filename = download_dir + textBox1.Text + ".flv";
            WebClient client = new WebClient();
            client.DownloadProgressChanged += new DownloadProgressChangedEventHandler(client_DownloadProgressChanged);
            client.DownloadFileCompleted += new AsyncCompletedEventHandler(client_DownloadFileCompleted);
            client.DownloadFileAsync(new Uri("http://cdn.keek.com/keek/video/" + textBox1.Text + "/flv"), filename);
        }


        private void button2_Click(object sender, EventArgs e)
        {
            multi = true;
            String[] s = textBox2.Text.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
                for (int i = 0; i < s.Length; i++)
                {
                    progressBar1.Value = 0;
                    String download_dir = @"" + Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\keek_downloads\\";
                    bool IsExists = System.IO.Directory.Exists(download_dir);
                    if (!IsExists)
                        System.IO.Directory.CreateDirectory(download_dir);
                    filename = download_dir + s[i] + ".flv";
                    //MessageBox.Show(new Uri("http://cdn.keek.com/keek/video/" + textBox2.Text + "/flv").ToString());
                    WebClient client = new WebClient();
                    client.DownloadProgressChanged += new DownloadProgressChangedEventHandler(client_DownloadProgressChanged);
                    client.DownloadFileCompleted += new AsyncCompletedEventHandler(client_DownloadFileCompleted);
                    client.DownloadFileAsync(new Uri("http://cdn.keek.com/keek/video/" + s[i] + "/flv"), filename);
                }
                
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start("mailto:mohannad.otaibi@gmail.com");
            }
            catch { }
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start("http://www.twitter.com/BuFai7an");
            }
            catch { }
        }
    }
}
