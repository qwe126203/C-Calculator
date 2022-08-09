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
using MySql.Data.MySqlClient;
using System.Windows;
using System.Data;

namespace WpfApp2
{
    /// <summary>
    /// Window1.xaml 的互動邏輯
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            
                InitializeComponent();
                string connString = "server=127.0.0.1;user id=root;password=1111;database=calculator";
                MySqlConnection conn = new MySqlConnection(connString);
                string sql = "SELECT * FROM result";
                MySqlCommand cmdSel = new MySqlCommand(sql, conn);
                DataTable dt = new DataTable();
                MySqlDataAdapter da = new MySqlDataAdapter(cmdSel);
                da.Fill(dt);
                datagrid1.ItemsSource = dt.DefaultView;
           
            
           
            

           



        }




        private void DataGrid_SelectionChanged_2(object sender, SelectionChangedEventArgs e)
        {

        }

        private void delete_click(object sender, RoutedEventArgs e)
        {
            int num1 = datagrid1.SelectedIndex;
            
            string str = (datagrid1.Columns[2].GetCellContent(datagrid1.Items[num1]) as TextBlock).Text;
            string connString = "server=127.0.0.1;user id=root;password=1111;database=calculator";
            MySqlConnection conn = new MySqlConnection(connString);
            conn.Open();
            string sq3 = "DELETE FROM `result` WHERE `Inorder`='" + str + "'";
            MessageBox.Show("DELETE FROM `result` WHERE `Inorder`='" + str + "'");
            MySqlCommand cmd = new MySqlCommand(sq3, conn);
            int n = cmd.ExecuteNonQuery();

            var itemSource = datagrid1.ItemsSource as DataView;
            
            itemSource.Delete(num1);
            
            datagrid1.ItemsSource = itemSource;
            
            
            

        }


    }
}

