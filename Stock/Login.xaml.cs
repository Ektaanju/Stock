using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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

namespace Stock
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
        }

        private void btn_clear_Click(object sender, RoutedEventArgs e)
        {
            txtbx_username.Text = "";
            txtbox_password.Clear();
            txtbx_username.Focus();
        }

        private void btn_login_Click(object sender, RoutedEventArgs e)
        {
            // TO-DO: Check Login username & password

            SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=Stock;Integrated Security=True");
            SqlDataAdapter sda = new SqlDataAdapter(@"SELECT [UserName], [Password] FROM [Stock].[dbo].[Login] 
                                                    WHERE UserName ='" +txtbx_username.Text +"' and Password = '" +txtbox_password.Password.ToString() +"';", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);

            if(dt.Rows.Count ==1)
            {
                this.Hide();
                MainWindow main = new MainWindow();
                main.Show();
            }
            else
            {
                MessageBox.Show("Invalid username & password", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                btn_clear_Click(sender, e);
            }
        }
    }
}
