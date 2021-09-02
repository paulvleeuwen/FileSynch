using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FileSynch
{
    public partial class Form1 : Form
    {
        public string sourcePath = "C:\\Users\\%user%\\Pictures";
        public string targetPath = "C:\\Data Science Team\\Projects\\FileSynch";

        public Form1()
        {
            InitializeComponent();

            MessageBox.Show("1");

            // Set the source path to the user specific directory.
            string userNameFull = System.Security.Principal.WindowsIdentity.GetCurrent().Name;

            MessageBox.Show("2");

            // Keep the characters after the symbols \\. For else you could have, for example, 'Company\\Mike01' instead of 'Mike01'.
            string userName = Regex.Match(userNameFull, @"[^\\]*$").Value;

            MessageBox.Show("3");

            sourcePath = sourcePath.Replace("%user%", userName);

            MessageBox.Show("4");

            label1.Text = label1.Text + " " + sourcePath + " to " + targetPath + ":";

            // Get the content of the source path and list it to the Form.
            List<string> sourcePathContent = ListContentDirectory(sourcePath);
            ListBox listBoxSourcePathContent = new ListBox();
            listBoxSourcePathContent.Name = "listBox1";
            listBoxSourcePathContent.Size = new System.Drawing.Size(245, 200);
            listBoxSourcePathContent.Location = new Point(13, 122);
            Controls.Add(listBoxSourcePathContent);
            foreach (string pathName in sourcePathContent)
            {
                listBoxSourcePathContent.Items.Add(pathName);
            }
        }

        static List<string> ListContentDirectory(string pathDirectory)
        {
            // Get files of the target directory.
            List<string> contentDirectory = Directory.GetFiles(pathDirectory).ToList();
            try
            {   
                foreach (string directory in Directory.GetDirectories(pathDirectory))
                {
                    contentDirectory.AddRange(ListContentDirectory(directory));
                }
            }
            catch (System.Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
            return contentDirectory;
        }

        private static void CopyFilesRecursively(string sourcePath, string targetPath)
        {
            //Now Create all of the directories
            foreach (string dirPath in Directory.GetDirectories(sourcePath, "*", SearchOption.AllDirectories))
            {
                Directory.CreateDirectory(dirPath.Replace(sourcePath, targetPath));
            }

            //Copy all the files & Replaces any files with the same name
            foreach (string newPath in Directory.GetFiles(sourcePath, "*.*", SearchOption.AllDirectories))
            {
                File.Copy(newPath, newPath.Replace(sourcePath, targetPath), true);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CopyFilesRecursively(sourcePath, targetPath);
            label1.Text = "Files are succesfully copied to " + targetPath + ".";
        }

        private void ContentSourceDirectory_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
