using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SendToFolder
{
    public partial class Form1 : Form
    {
        char sep = Path.DirectorySeparatorChar;
        string[] args;
        public Form1(string[] args)
        {
            InitializeComponent();
            
            this.args = args;
            textBox1.Text = Path.GetDirectoryName(this.args[0]);
            textBox2.Text = Path.GetFileName(this.args[0]).Split("-")[0].Trim();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            foreach (var arg in this.args)
            {
                Path.GetDirectoryName(arg);
                int addedItemIndex = checkedListBox1.Items.Add(arg);
                checkedListBox1.SetItemChecked(addedItemIndex, true);
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            StringBuilder copiedFiles = new StringBuilder();
            StringBuilder notCopiedFiles = new StringBuilder();
            int copiedCounter = 0;
            int notCopiedCounter = 0;
            string destFolder = string.Concat(textBox1.Text, sep, textBox2.Text);

            if (!Directory.Exists(destFolder))
                Directory.CreateDirectory(destFolder);
            
            CheckedListBox.ObjectCollection it = checkedListBox1.Items;
            for (int i = 0; i < it.Count; i++)
            {
                if (!checkedListBox1.GetItemChecked(i))
                    continue;

                string originFilePath = checkedListBox1.GetItemText(it[i]);
                FileAttributes attr = File.GetAttributes(originFilePath);

                if (attr.HasFlag(FileAttributes.Directory))
                {
                    String finalFolderName = $"{destFolder}{sep}{originFilePath.Split(sep).Last()}";
                    try
                    {
                        Directory.Move(originFilePath, finalFolderName);
                        copiedFiles.Append($"\n{finalFolderName}");
                        copiedCounter++;
                    }
                    catch (Exception ex)
                    {
                        notCopiedFiles.Append($"\n[{finalFolderName}] {ex.Message}");
                        notCopiedCounter++;
                    }
                    continue;
                }

                string fileName = Path.GetFileName(originFilePath);
                try
                {
                    File.Move(originFilePath, destFolder + sep + fileName);
                    copiedFiles.Append($"\n{fileName}");
                    copiedCounter++;
                }
                catch (Exception ex)
                {
                    notCopiedFiles.Append($"\n[{fileName}] {ex.Message}");
                    notCopiedCounter++;
                }
            }

            this.Visible = false;
            MessageBox.Show(
                $"Files copied: {copiedCounter}{copiedFiles}\nFiles not copied: {notCopiedCounter}{notCopiedFiles}",
                "Process finished", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }

        private void tb_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button1_Click(sender, e);
            }
        }
    }
}
