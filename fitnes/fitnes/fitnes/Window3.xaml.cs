using System;
using System.Data.SqlClient;
using System.Windows;

namespace fitnes
{
    /// <summary>
    /// Логика взаимодействия для Window3.xaml
    /// </summary>
    public partial class Window3 : Window
    {
        const string connSTR = "Data Source=Vladimir-pc;Initial Catalog=PRODUCT;Integrated Security=True";
        int id = 0;

        public Window3()
        {
            InitializeComponent();
            getLastId();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection conn = new SqlConnection(connSTR);
            try
            {
                string sql = $"update NewUser set kalorii = '{resKalorii.Text.ToString()}' where id_user = {id}";

                conn.Open();
                SqlCommand C = new SqlCommand(sql, conn);

                C.ExecuteNonQuery(); //метод позволяет выполнять операции с каталогом или вносить изменения в базу данных, не используя DataSet, с помощью операторов UPDATE, INSERT или DELETE
                Window4 w4 = new Window4();
                w4.Show();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally { conn.Close(); }
            this.Close();
        }

        private void getLastId()
        {
            SqlConnection conn = new SqlConnection(connSTR);
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
    }
}
