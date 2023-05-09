using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PriorityEditor
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        private Button update = new Button()
        {
            Text = "Update",
            Size = new Size(100, 50),
            Location = new Point(30, 315)
        };
        
        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            ((ContainerControl)this).AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.CenterToScreen();
            if (File.Exists("31.ico"))
                this.Icon = new Icon("31.ico",128,128);
            
            this.Text = "Priority editor";
            
            this.Controls.Add(update);
            update.Click += new EventHandler(tablePanel);
            tablePanel();
        }
        private void tablePanel()
        {
            ListBox listBox1 = new ListBox();
            listBox1.MultiColumn = true;
            // Set the size and location of the ListBox.
            ProcessThreadCollection currentThreads = Process.GetCurrentProcess().Threads;
            foreach (ProcessThread thread in currentThreads)
            {

                listBox1.Items.Add(currentThreads.IndexOf(thread));
            }
            listBox1.Size = new System.Drawing.Size(500, 300);
            listBox1.Location = new System.Drawing.Point(30,10);
            this.Controls.Add(listBox1);        
            
            /*Task.Run(async delegate
            {
                await Task.Delay(1500);
                tablePanel();
            });*/
        }
        private void tablePanel(Object sender, EventArgs e)
        {
            tablePanel();
        }
        #endregion
    }
}