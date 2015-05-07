using AVOSCloud;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SDK.Test.WinForm
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            AVClient.Initialize("ih6v94x2o6t338rshsn7vr4mlsv9hcwjff28oll2ejwj3k7h", "ifdkojmxiolexkwblvk6mdw2pzczslmhwen171seykdk3in5");
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}
