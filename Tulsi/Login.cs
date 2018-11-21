using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tulsi.BusinessLayer;
using Tulsi.BusinessObject;

namespace Tulsi
{
    public partial class Login : MetroFramework.Forms.MetroForm
    {
        public Login()
        {
            InitializeComponent();
        }

        private void Login_Load(object sender, EventArgs e)
        {

        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            User u = new User();
            u.Username = txtUsername.Text;
            u.Password = txtPassword.Text;

            UserBLL user = new UserBLL();
            u = user.Login(u);
            if (u != null)
            {
                switch(u.DesignationId) 
                {
                    case 1:
                        POS pos = new POS();
                        this.Hide();
                        pos.Show();
                        break;
                    case 2:
                        //
                        break;
                    default:
                        lblLoginError.SetBounds(190, 92, 39, 19);
                        lblLoginError.Visible = true;
                        lblLoginError.Text = "Please provide correct credentials.";
                        break;
                }
 
                
            }
        }

        private void lnkCreateAccount_Click(object sender, EventArgs e)
        {
            RegistrationForm reg = new RegistrationForm();
            this.Hide();
            reg.Show();
        }
    }
}
