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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data;
using MySql.Data.MySqlClient;
using System.Windows;



namespace WpfApp2
{
    
    public partial class MainWindow : Window
    {
        
        public MainWindow()
        {
            InitializeComponent();
        }
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
        
        public static string inorder;
        public static string preorder ;
        public static string postorder ;
        public static string b_result ;
        public static string d_result ;
        bool op_bool = true;
        
        private void Btn_num_click(object sender, RoutedEventArgs e)
        {   //sender 是觸發事件的button物件，類別為object，將sender類型轉換後，就可以對button的屬性進行更改            Button buttonNum=sender as Button
            Button btn_num = (Button)sender;
            op_bool = false;
            if (TextBox1.Text == "0")
            {
                TextBox1.Text = (String)btn_num.Content;
            }
            else
            {
                TextBox1.Text += btn_num.Content;
            }
        }

        private void Btn_op_click(object sender, RoutedEventArgs e)
        {
            Button btn_op = (Button)sender;
            if (op_bool == false)
            {

                string mathematical = (String)btn_op.Content;
                string beforevalue = TextBox1.Text;
                TextBox1.Text += btn_op.Content;
                op_bool = true;

            }
            else
            {
                if (TextBox1.Text == "")
                {
                    TextBox1.Text = TextBox1.Text;
                }
                else
                {
                    TextBox1.Text = TextBox1.Text.Substring(0, TextBox1.Text.Length - 1);
                    TextBox1.Text += btn_op.Content;
                }

            }


        }
        

        private void Btn_dot_click(object sender, RoutedEventArgs e)
        {
            TextBox1.Text += ".";
        }
        public void Btn_equal_click(object sender, RoutedEventArgs e)
        {
            TextBox1_inorder.Text = TextBox1.Text;

            string infix = TextBox1.Text;

            string postfix = "";
            convert_topostorder(ref infix, out postfix);
            TextBox1_postorder.Text = postfix;

            string prefix = "";
            convert_topreorder(ref infix, out prefix);
            TextBox1_preorder.Text = Reverse(prefix.ToCharArray());


            DataTable table = new DataTable();
            object result = table.Compute(TextBox1.Text, "");
            TextBox1.Text = Convert.ToString(result);

            TextBox1_decimal.Text = Convert.ToString(result);

            int a = int.Parse(Convert.ToString(result));
            string binary_result = Convert.ToString(a, 2);
            TextBox1_binary.Text = binary_result;

             inorder = TextBox1_inorder.Text;
             preorder = TextBox1_preorder.Text;
             postorder = TextBox1_postorder.Text;
             b_result = TextBox1_decimal.Text;
             d_result = TextBox1_binary.Text;
            


        }


        
        private void Btn_ac_click(object sender, RoutedEventArgs e)
        {
            TextBox1.Text = "";
        }
        
        static bool convert_topostorder(ref string infix, out string postfix)
        {

            int prio = 0;
            postfix = "";
            Stack<Char> s1 = new Stack<char>();
            for (int i = 0; i < infix.Length; i++)
            {
                char ch = infix[i];
                if (ch == '+' || ch == '-' || ch == '*' || ch == '/')
                {
                    if (s1.Count <= 0)
                        s1.Push(ch);
                    else
                    {
                        if (s1.Peek() == '*' || s1.Peek() == '/')
                            prio = 1;
                        else
                            prio = 0;
                        if (prio == 1)
                        {
                            if (ch == '+' || ch == '-')
                            {
                                postfix += s1.Pop();
                                i--;
                            }
                            else
                            {
                                postfix += s1.Pop();
                                i--;
                            }
                        }
                        else
                        {
                            if (ch == '+' || ch == '-')
                            {
                                postfix += s1.Pop();
                                s1.Push(ch);

                            }
                            else
                                s1.Push(ch);
                        }
                    }
                }
                else
                {
                    postfix += ch;
                }
            }
            int len = s1.Count;
            for (int j = 0; j < len; j++)
                postfix += s1.Pop();
            return true;
        }
        string Reverse(char[] arg)
        {
            int i, j;
            char c = '\0';
            for (i = 0, j = arg.Length - 1; i < j; ++i, --j)
            {
                c = arg[i];
                arg[i] = arg[j];
                arg[j] = c;

            }
            string s = new string(arg);
            return s;
        }
        static bool convert_topreorder(ref string infix, out string prefix)
        {

            int prio = 0;
            prefix = "";
            Stack<Char> s1 = new Stack<char>();
            for (int i = infix.Length - 1; i > -1; i--)
            {
                char ch = infix[i];
                if (ch == '+' || ch == '-' || ch == '*' || ch == '/')
                {
                    if (s1.Count <= 0)
                        s1.Push(ch);
                    else
                    {
                        if (s1.Peek() == '*' || s1.Peek() == '/')
                            prio = 1;
                        else
                            prio = 0;
                        if (prio == 1)
                        {
                            if (ch == '+' || ch == '-')
                            {
                                prefix += s1.Pop();
                                i++;
                            }
                            else
                            {
                                prefix += s1.Pop();
                                i++;
                            }
                        }
                        else
                        {
                            if (ch == '+' || ch == '-')
                            {
                                prefix += s1.Pop();
                                s1.Push(ch);

                            }
                            else
                                s1.Push(ch);
                        }
                    }
                }
                else
                {
                    prefix += ch;
                }
            }
            int len = s1.Count;
            for (int j = 0; j < len; j++)
                prefix += s1.Pop();
            return true;
        }
        
        public void Btn_insert_click(object sender, RoutedEventArgs e)
        {
            
            string connString = "server=127.0.0.1;user id=root;password=1111;database=calculator";
            MySqlConnection conn = new MySqlConnection(connString);
            conn.Open();
           
            string sql = "SELECT * FROM result";
            MySqlCommand cmdSel = new MySqlCommand(sql, conn);
            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter(cmdSel);
            da.Fill(dt);

            string[] arr = new string[20];
            List<string> list = new List<string>(arr.ToList());
            for(int i = 0; i < dt.Rows.Count; i++)
            {
                list.Add(dt.Rows[i][1].ToString());
            }
            arr = list.ToArray();
            if(Array.IndexOf(arr, inorder) < 0)
            {
                string sq2 = "INSERT INTO `result` VALUES (0,'" + inorder + "','" + preorder + "','" + postorder + "','" + b_result + "','" + d_result + "')";
                MySqlCommand cmd = new MySqlCommand(sq2, conn);

                MessageBox.Show("上傳資料庫成功!");
                int index = cmd.ExecuteNonQuery();
            }
            else
            {
                MessageBox.Show("資料庫中已有該筆資料");
            }


            


            //

        }

        private void Btn_query_click(object sender, RoutedEventArgs e)
        {
            Window1 f = new Window1();
            f.Show();
           

        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
    
}




