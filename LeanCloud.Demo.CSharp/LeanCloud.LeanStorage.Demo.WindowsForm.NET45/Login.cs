using AVOSCloud;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LeanCloud.LeanStorage.Demo.WindowsForm.NET45
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void btn_login_Click(object sender, EventArgs e)
        {
            AVUser.LogInAsync(txb_username.Text.Trim(), txb_password.Text.Trim());
        }
    }
}
