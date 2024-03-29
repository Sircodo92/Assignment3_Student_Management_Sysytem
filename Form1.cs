using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Assignment3_Student_Management_Sysytem
{
    public partial class Form1 : Form
    {
        private const string userFilePath = "../../user.txt";
        private const string adminUsername = "ADMIN";
        private const string adminPassword = "Password";

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (!File.Exists(userFilePath))
            {
                // Create initial admin user
                File.WriteAllText(userFilePath, $"{adminUsername},{adminPassword}");
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Text;

            if (AuthenticateUser(username, password))
            {
                Form2 studentManagementForm = new Form2();
                studentManagementForm.ShowDialog();
            }
            else
            {
                MessageBox.Show("Invalid username or password", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool AuthenticateUser(string username, string password)
        {
            if (File.Exists(userFilePath))
            {
                string[] userData = File.ReadAllLines(userFilePath);
                return userData.Any(line => line.Split(',')[0] == username && line.Split(',')[1] == password);
            }
            else
            {
                MessageBox.Show("User file not found.");
                return false;
            }
        }
    }
}
