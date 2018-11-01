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
    /// Interaction logic for Products.xaml
    /// </summary>
    public partial class Products : Window
    {
        public Products()
        {
            InitializeComponent();

            cmb_active.Items.Add("Active");
            cmb_active.Items.Add("Deactive");
            cmb_active.SelectedIndex = 0;

            /*
            DataGridTextColumn textColumn = new DataGridTextColumn();
            DataGridTextColumn textColumn2 = new DataGridTextColumn();
            DataGridTextColumn textColumn3 = new DataGridTextColumn();

            textColumn.Header = "Product Code";
            textColumn.Binding = new Binding("ProductCode");
            dataGrid.Columns.Add(textColumn);

            textColumn2.Header = "Product Name";
            textColumn2.Binding = new Binding("ProductName");
            dataGrid.Columns.Add(textColumn2);

            textColumn3.Header = "Status";
            textColumn3.Binding = new Binding("Status");
            dataGrid.Columns.Add(textColumn3);
            */
        }

        //form load event??
        private void Products_Load(object sender, EventArgs e)
        {
            cmb_active.SelectedIndex = 0;
        }

        private void btn_add_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=Stock;Integrated Security=True");

            con.Open();

            bool status = false;
            if(cmb_active.SelectedIndex == 0)
            {
                status = true;
            }
            else
            {
                status = false;
            }


            var sqlQuery = "";

            if (IfProductExists(con, txtbx_productcode.Text))
            {
                sqlQuery = @"UPDATE[Stock].[dbo].[Products] SET[ProductName] = '" + txtbx_productname.Text +"',[ProductStatus] = '" +status 
                            +"'WHERE[ProductCode] = '" +txtbx_productcode.Text +"'";
            }
            else
            {
                sqlQuery = @"INSERT INTO [Stock].[dbo].[Products] ([ProductCode], [ProductName], [ProductStatus])
                           VALUES('" + txtbx_productcode.Text + "', '" + txtbx_productname.Text + "', '" + status + "')";
            }


            // Insert logic
            /*SqlCommand cmd = new SqlCommand(@"INSERT INTO [Stock].[dbo].[Products]
                                            ([ProductCode], [ProductName], [ProductStatus])
                                            VALUES('" + txtbx_productcode.Text + "', '" + txtbx_productname.Text + "', '" + status + "')", con);
            */
            SqlCommand cmd = new SqlCommand(sqlQuery, con);        
            cmd.ExecuteNonQuery();

            con.Close();

            LoadData();
        }


        private void btn_delete_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=Stock;Integrated Security=True");

            var sqlQuery = "";

            if (IfProductExists(con, txtbx_productcode.Text))
            {
                con.Open();
                sqlQuery = @"DELETE FROM [Stock].[dbo].[Products] WHERE[ProductCode] = '" + txtbx_productcode.Text + "'";
                SqlCommand cmd = new SqlCommand(sqlQuery, con);
                cmd.ExecuteNonQuery();
                con.Close();
            }
            else
            {
                MessageBox.Show("Record Not Exists...!");
            }

            LoadData();
        }

        private bool IfProductExists(SqlConnection con, string productCode)
        {
            SqlDataAdapter sda = new SqlDataAdapter("SELECT 1 FROM [Stock].[dbo].[Products] WHERE[ProductCode]= '" + txtbx_productcode.Text +"';", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            
            if(dt.Rows.Count >0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void LoadData()
        {
            SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=Stock;Integrated Security=True");

            // Reading Data
            SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM [Stock].[dbo].[Products]", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            dataGrid.ItemsSource = dt.DefaultView;
            dataGrid.AutoGenerateColumns = true;
            dataGrid.CanUserAddRows = false;
        }

        private void dataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            // same as dataGrid_SelectionChanged
        }

        private void dataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid gd = (DataGrid)sender;
            DataRowView row_selected = gd.SelectedItem as DataRowView;
            if (row_selected != null)
            {
                txtbx_productcode.Text = row_selected[0].ToString();
                txtbx_productname.Text = row_selected[1].ToString();
                
                bool IsActive = Convert.ToBoolean(row_selected[2].ToString());     //returns true if active, false otherwise

                if (IsActive)
                {
                    cmb_active.SelectedIndex = 0;
                }
                else
                {
                    cmb_active.SelectedIndex = 1;
                }
            }
        }


    }
}
