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

namespace FolderMenu
{
    public partial class Form1 : Form
    {
        string root;
        string newFolderName;
        
        string[] args;
        public Form1(string[] args)
        {
            InitializeComponent();
            this.args = args;
            this.root = Path.GetDirectoryName(this.args[0]);
            this.newFolderName = Path.GetFileName(this.args[0]).Split("-")[0].Trim();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            textBox1.Text = this.root;
            textBox2.Text = this.newFolderName;
            foreach (var arg in this.args)
            {
                Path.GetDirectoryName(arg);
                checkedListBox1.Items.Add(arg);
            }
            for (int i = 0; i < checkedListBox1.Items.Count; i++)
            {
                checkedListBox1.SetItemChecked(i, true);
               
            }
            
        }
        private void button1_Click(object sender, EventArgs e)
        {
            string argList = "";
            string notCopiedFiles = "";
            int copiedCounter = 0;
            int notCopiedCounter = 0;
            char sep = Path.DirectorySeparatorChar;
            string destFolder = textBox1.Text + sep + textBox2.Text;

            if (!Directory.Exists(destFolder))
            {
                Directory.CreateDirectory(destFolder);
            }
            CheckedListBox.ObjectCollection it = checkedListBox1.Items;
            for (int i = 0; i < it.Count; i++)
            {
                if (checkedListBox1.GetItemChecked(i))
                {
                    string originFilePath = checkedListBox1.GetItemText(it[i]);
                    FileAttributes attr = File.GetAttributes(originFilePath);

                    if (attr.HasFlag(FileAttributes.Directory))
                    {
                        MessageBox.Show("Its a directory. function to be implemented.");
                        try
                        {
                            Directory.Move(originFilePath, destFolder );
                            argList += "\n" + destFolder;
                            copiedCounter++;
                        }
                        catch (Exception ex)
                        {
                            notCopiedFiles += "\n[" + destFolder + "] " + ex.Message;
                            notCopiedCounter++;
                        }
                    }
                    else
                    {
                        string fileName = Path.GetFileName(originFilePath);
                        try
                        {
                            File.Move(originFilePath, destFolder+sep+ fileName) ;
                            argList += "\n" + fileName;
                            copiedCounter++;
                        } 
                        catch(Exception ex)
                        {
                            notCopiedFiles += "\n["+fileName+"] "+ex.Message ;
                            notCopiedCounter++;
                        }

                    }
                }

            }

            this.Visible = false;
            DialogResult result = MessageBox.Show(
                "Files copied: " + copiedCounter + argList + 
                "\nFiles not copied: " + notCopiedCounter + notCopiedFiles, 
                "Process finished", MessageBoxButtons.OK, MessageBoxIcon.Information);

            this.Close();
        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

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
