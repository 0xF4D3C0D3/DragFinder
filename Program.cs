using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DragFinder
{
    static class Program
    {

        public static Form1 myForm;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            bool result;
            var mutex = new System.Threading.Mutex(true, "DragFinder_DONGHO", out result);

            if (!result)
                return;

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            myForm = new Form1();
            Application.Run(myForm);

            GC.KeepAlive(mutex);
        }
    }
}
