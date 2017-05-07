using System;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

namespace fitnes
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        const string connSTR = "Data Source=Vladimir-pc;Initial Catalog=PRODUCT;Integrated Security=True";
        const string pattern = "^.*[^A-zА-яЁё].*$";
        int id = 0;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (name_us.Text.ToString() != string.Empty)
            {
                if (!Regex.IsMatch(name_us.Text.ToString(), pattern))
                {
                    SqlConnection conn = new SqlConnection(connSTR);
                    try
                    {
                        string sqlInsert = $"insert into NewUser(name) Values(@name)";
                        conn.Open();
                        SqlCommand C = new SqlCommand(sqlInsert, conn);
                        C.Parameters.AddWithValue("@name", name_us.Text);
                        C.ExecuteNonQuery(); //метод позволяет выполнять операции с каталогом или вносить изменения в базу данных, 
                                             // не используя DataSet, с помощью операторов UPDATE, INSERT или DELETE

                        getLastId();
                        Window1 w1 = new Window1();
                        w1.Show();
                        w1.text_box_name.Text = id.ToString();

                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    finally { conn.Close(); }
                    this.Close();
                }
                else
                    MessageBox.Show("Имя не должно содержать цифр", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
                MessageBox.Show("Поле не может быть пустым.\nВведите имя.", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        private void getLastId()
        {
            SqlConnection conn = new SqlConnection(connSTR);
            //   string sqlSelectId = "SELECT IDENT_CURRENT('NewUser')";       // получить последний добавленный id
            string sqlSelectId = "SELECT MAX(id_user) from NewUser";       // получить последний добавленный id
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sqlSelectId, conn);
                id = Convert.ToInt32(cmd.ExecuteScalar());
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally { conn.Close(); }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {






        }
    }
}
