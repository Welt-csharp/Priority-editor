using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PriorityEditor
{
    partial class Form1
    {
        DataGridView dataGridView = new DataGridView();
        private int sortDirection = 1;
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
            
            update.Click += new EventHandler(refresh);
            tablePanel();
        }

        private void refresh(Object obj, EventArgs e)
        {
            Thread temporaryThread = new Thread(() => refresh(this.dataGridView));
            temporaryThread.Priority = ThreadPriority.AboveNormal;
            temporaryThread.Start();
        }

        private void refresh(DataGridView temp)
        {
            temp.Rows.Clear();
            foreach (var process in Process.GetProcesses())
            {
                temp.Rows.Add(new Object[]
                {   process.ProcessName, 
                    Math.Round(process.PagedMemorySize64 / Math.Pow(2, 20), 1),
                    process.Id });
            }
            temp.Sort(this.dataGridView.SortedColumn, (ListSortDirection)(dataGridView.SortedColumn.HeaderCell.SortGlyphDirection-1));
            this.dataGridView = temp;
            /*Task.Run(async delegate
            {
                await Task.Delay(2500);
                refresh(this.dataGridView);
            });*/
        }
        private void tablePanel()
        {
            this.Controls.Add(dataGridView);
            this.Controls.Add(update);
            dataGridView.ReadOnly = true;
            dataGridView.ColumnCount = 3;
            dataGridView.Columns[0].Name = "Process";
            dataGridView.Columns[1].Name = "Memory usage (MB)";
            dataGridView.Columns[1].MinimumWidth = 125;
            dataGridView.Columns[2].Name = "Id";

            dataGridView.Size = new System.Drawing.Size(400, 300);
            dataGridView.Location = new System.Drawing.Point(30,10);
            

            foreach (var process in Process.GetProcesses())
            {
                dataGridView.Rows.Add(new Object[]
                    {   process.ProcessName, 
                        Math.Round(process.PagedMemorySize64 / Math.Pow(2, 20), 1),
                        process.Id });
            }
            dataGridView.Sort(dataGridView.Columns[1], (ListSortDirection)sortDirection );
            Task.Run(async delegate
                {
                    await Task.Delay(2500);
                    refresh(dataGridView);
                });
        }
        #endregion
    }
}