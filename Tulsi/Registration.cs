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
    public partial class RegistrationForm : MetroFramework.Forms.MetroForm
    {
        public RegistrationForm()
        {
            InitializeComponent();
        }

        private void Registration_Load(object sender, EventArgs e)
        {
            populateCboDesignation();
        }

        private void populateCboDesignation()
        {
            DesignationBLL designation = new DesignationBLL();
            List<Designation> des = new List<Designation>();
            des = designation.GetAll();
            foreach (Designation d in des)
            {
                cboDesignation.Items.Add(d.Position);
            } 
            
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            
            string reference = txtReferenceId.Text;
            User user = new User();
            user.FullName = txtFullName.Text;
            user.Username = txtUsername.Text;
            user.Password = txtPassword.Text;
            user.DesignationId = Convert.ToInt32(cboDesignation.SelectedIndex + 1);
            user.JoiningDate = DateTime.Now;
            UserBLL u = new UserBLL();

            if (String.IsNullOrWhiteSpace(reference))
            {
                lblRegMsg.Visible = true;
                lblRegMsg.Text = "All fields are required";
            }
            else if (u.ValidateUser(user) && u.Get(reference) != null)
            {
                u.Insert(user);
                lblRegMsg.Visible = true;
                lblRegMsg.Text = "Registration successful!";
                this.Hide();
                Login login = new Login();
                login.Show();
            }            
        }
    }
}
