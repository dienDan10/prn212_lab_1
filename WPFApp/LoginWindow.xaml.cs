using BusinessObjects;
using Service;
using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace WPFApp
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private IAccountService iAccountService;
        public LoginWindow()
        {
            InitializeComponent();
            iAccountService = new AccountService();
        }

        private void btnLogin_Clicked(object sender, RoutedEventArgs e)
        {
            AccountMember account = iAccountService.GetAccountById(txtUser.Text);
            if (account != null && account.MemberPassword.Equals(txtPass.Password)) {
                this.Hide();
                MainWindow main = new MainWindow();
                main.Show();
                return;
            }

            MessageBox.Show("Username or Password incorrect", "Error");
        }

        private void btnCancel_Clicked(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
