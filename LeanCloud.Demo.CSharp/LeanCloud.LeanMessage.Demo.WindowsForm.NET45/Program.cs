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
            AVClient.Initialize("ompowtl995nshu39q4pxpwojedm5xo5kqyu6c35qh14bwkwz", "puts6uhv8onlgtm4yj3njcmow03doagysoig5c7twnn2ef4g");
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}
