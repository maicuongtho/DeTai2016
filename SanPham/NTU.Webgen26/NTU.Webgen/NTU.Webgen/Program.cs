﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NTU.Webgen
{
    static class Program
    {
     
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
           // ChuongTrinh c = new ChuongTrinh();
            Application.Run(new ChuongTrinh());
        }
     
        
    }
}
