using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PriorityEditor
{
    static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Task.Run(async delegate
            {
                await Task.Delay(500);
                cautionMessage();
            });
            Application.Run(new Form1());
        }
        private static void cautionMessage()
        {
            MessageBox.Show("Editing thread priority may cause instability of your system.\n" +
                            "Instead of increasing priority, try decrease.\n" +
                            "You have been warned.","Caution");
        }
    }
}