using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace MangerTagGen
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        
        static void Main()
        {
            //Algo algo = new Algo();
            //algo.initializeRefrence();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());

        }
    }
}
