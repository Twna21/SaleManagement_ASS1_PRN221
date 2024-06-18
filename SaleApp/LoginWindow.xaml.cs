using DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;

using Newtonsoft.Json;
using BusinessObject;
using System.Text.RegularExpressions;

namespace SaleApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private IMemberRepository memberRepository = new MemberRepository();
        private bool isAdmin = false;
        private bool isMember = false;
        public LoginWindow()
        {
            InitializeComponent();

        }



        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            string emailInput = txtEmail.Text;
            string passwordInput = txtPass.Password;

            if (!IsValidEmail(emailInput))
            {
                MessageBox.Show("Please enter a valid email address.", "Invalid Email");
                return;
            }

            string json = string.Empty;

            // Read JSON file
            using (StreamReader reader = new StreamReader("appsettings.json"))
            {
                json = reader.ReadToEnd();
            }

            // Deserialize JSON string to dynamic type using Newtonsoft.Json
            var obj = JsonConvert.DeserializeObject<dynamic>(json);

            // Get contents
            string email = obj["Default Account"]["Email"];
            string password = obj["Default Account"]["Pass"];

            if (emailInput.Equals(email) && passwordInput.Equals(password))
            {
                isAdmin = true;
            }
            else
            {
                var members = memberRepository.ReadAll();
                foreach (var i in members)
                {
                    if (i.Email.Equals(emailInput) && i.Password.Equals(passwordInput))
                    {
                        isAdmin = false;
                        isMember = true;
                        break; 
                    }
                }
            }

            if (isMember || isAdmin)
            {
                MessageBox.Show("Login Successfully");
                NegativeToHome();
            }
            else
            {
                MessageBox.Show("Wrong username or password, please try again", "Login Failed!!!");
            }
        }

        private bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            try
            {
          
                return Regex.IsMatch(email,
                    @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
                    RegexOptions.IgnoreCase);
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }
    

    private void LoginWindow_Loaded(object sender, RoutedEventArgs e)
        {
            txtEmail.Text = "admin@fstore.com";
            txtPass.Password = "admin@@";
        }
        private void NegativeToHome()
        {
            WindowMain mainWindow = new WindowMain(this,isMember , isAdmin,txtEmail.Text);
            mainWindow.Show();
            this.Hide();
        }
    }
}
