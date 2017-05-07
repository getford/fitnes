using System;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

namespace fitnes
{
    /// <summary>
    /// Логика взаимодействия для Window4.xaml
    /// </summary>
    public partial class Window4 : Window
    {
        const string connSTR = "Data Source=Vladimir-pc;Initial Catalog=PRODUCT;Integrated Security=True";
        const string pattern = "^[0-9]+$";
        int resKallorii = 0;            // калории сумма
        int kaloriiProd = 0;            // калории продукта
        int id = 0;                     // id user
        string name = string.Empty;     // имя пользователя
        string wyw = string.Empty;      // чего хочет пользователь
        string dateNow = string.Empty;

        public Window4()
        {
            InitializeComponent();

            if (checkFirstStart() != 0)     // проверка если пользователей нет
            {
                eatDate.Items.Add("Завтрак");
                eatDate.Items.Add("Обед");
                eatDate.Items.Add("Ужин");

                getProducts();
                getLastUser();

                labelUser.Content = $"Приятной работы: {name}.";
                labelWhatUWant.Content = $"Вы хотите: {wyw}";
            }
            else
            {
                MessageBox.Show($"Пользователей в бд нет.\n Вы будете перенаправлены для регистрации.\nЗакройте это сообщение.", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);

                MainWindow mw = new MainWindow();
                mw.Show();
                this.Close();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            dateNow = DateTime.Now.ToString("dd MMMM yyyy");

            if (prod.SelectedIndex < 0)
                MessageBox.Show("Пожалуйста, выберите продукт.", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
            else
            {
                if (eatDate.SelectedIndex < 0)
                    MessageBox.Show("Пожалуйста, выберите время приема пищи.", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
                else
                {
                    if (weightProd.Text.ToString() == string.Empty)
                        MessageBox.Show("Пожалуйста, введите вес продукта.", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
                    else
                    {
                        if (!Regex.IsMatch(weightProd.Text.ToString(), pattern))
                            MessageBox.Show("Введите корректные данные", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        else
                        {
                            getKalloriiProd();

                            if (eatDate.SelectedItem.ToString() == "Завтрак")
                            {
                                //  (Вес продукта/100)*калории продукта
                                int zKallorii = (Convert.ToInt32(weightProd.Text.ToString()) / 100) * kaloriiProd;
                                zavtrak.Text = zKallorii.ToString();
                                resKallorii += zKallorii;

                                SqlConnection conn = new SqlConnection(connSTR);
                                try
                                {
                                    string sqlInsert = $"insert into breakfast(id_user, date, product, kalorii) " +
                                        $"Values(@id_user, @date, @product, @kalorii)";
                                    conn.Open();
                                    SqlCommand C = new SqlCommand(sqlInsert, conn);
                                    C.Parameters.AddWithValue("@id_user", id.ToString());
                                    C.Parameters.AddWithValue("@date", dateNow.ToString());
                                    C.Parameters.AddWithValue("@product", prod.SelectedItem.ToString());
                                    C.Parameters.AddWithValue("@kalorii", zKallorii.ToString());
                                    C.ExecuteNonQuery();
                                }
                                catch (SqlException ex)
                                {
                                    MessageBox.Show(ex.Message);
                                }
                                finally { conn.Close(); }
                            }

                            if (eatDate.SelectedItem.ToString() == "Обед")
                            {
                                int oKalorii = (Convert.ToInt32(weightProd.Text.ToString()) / 100) * kaloriiProd;
                                obed.Text = oKalorii.ToString();
                                resKallorii += oKalorii;

                                SqlConnection conn = new SqlConnection(connSTR);
                                try
                                {
                                    string sqlInsert = $"insert into lunch(id_user, date, product, kalorii) " +
                                        $"Values(@id_user, @date, @product, @kalorii)";
                                    conn.Open();
                                    SqlCommand C = new SqlCommand(sqlInsert, conn);
                                    C.Parameters.AddWithValue("@id_user", id.ToString());
                                    C.Parameters.AddWithValue("@date", dateNow.ToString());
                                    C.Parameters.AddWithValue("@product", prod.SelectedItem.ToString());
                                    C.Parameters.AddWithValue("@kalorii", oKalorii.ToString());
                                    C.ExecuteNonQuery();
                                }
                                catch (SqlException ex)
                                {
                                    MessageBox.Show(ex.Message);
                                }
                                finally { conn.Close(); }
                            }

                            if (eatDate.SelectedItem.ToString() == "Ужин")
                            {
                                int yKalorii = (Convert.ToInt32(weightProd.Text.ToString()) / 100) * kaloriiProd;
                                yzin.Text = yKalorii.ToString();
                                resKallorii += yKalorii;

                                SqlConnection conn = new SqlConnection(connSTR);
                                try
                                {
                                    string sqlInsert = $"insert into dinner(id_user, date, product, kalorii) " +
                                        $"Values(@id_user, @date, @product, @kalorii)";
                                    conn.Open();
                                    SqlCommand C = new SqlCommand(sqlInsert, conn);
                                    C.Parameters.AddWithValue("@id_user", id.ToString());
                                    C.Parameters.AddWithValue("@date", dateNow.ToString());
                                    C.Parameters.AddWithValue("@product", prod.SelectedItem.ToString());
                                    C.Parameters.AddWithValue("@kalorii", yKalorii.ToString());
                                    C.ExecuteNonQuery();
                                }
                                catch (SqlException ex)
                                {
                                    MessageBox.Show(ex.Message);
                                }
                                finally { conn.Close(); }
                            }
                            sumKallorii.Text = resKallorii.ToString();
                        }
                    }
                }
            }
        }

        private void getProducts()
        {
            SqlConnection connect = new SqlConnection(connSTR);
            string sql = $"select product from pr";
            try
            {
                connect.Open();
                SqlCommand cmd = new SqlCommand(sql, connect);
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    prod.Items.Add(dr.GetString(0));
                }
                dr.Close();
            }
            catch (SqlException ex) { MessageBox.Show(ex.Message); }
            finally { connect.Close(); }

        }

        private void getKalloriiProd()
        {
            SqlConnection connect = new SqlConnection(connSTR);
            string sql = $"select kalorii from pr where product = '{prod.SelectedItem.ToString()}'";
            try
            {
                connect.Open();
                SqlCommand cmd = new SqlCommand(sql, connect);
                kaloriiProd = Convert.ToInt32(cmd.ExecuteScalar());
            }
            catch (SqlException ex) { MessageBox.Show(ex.Message); }
            finally { connect.Close(); }

        }

        private void getLastUser()
        {
            SqlConnection conn = new SqlConnection(connSTR);
            string sqlSelectId = "SELECT MAX(id_user) from NewUser";       // получить последний добавленный id
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sqlSelectId, conn);
                id = Convert.ToInt32(cmd.ExecuteScalar());

                string sqlSelectName = $"select name from NewUser where id_user = {id}";
                SqlCommand c = new SqlCommand(sqlSelectName, conn);
                name = Convert.ToString(c.ExecuteScalar());

                string sqlSelectWYW = $"select wyw from NewUser where id_user = {id}";
                SqlCommand cwyw = new SqlCommand(sqlSelectWYW, conn);
                wyw = Convert.ToString(cwyw.ExecuteScalar());
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally { conn.Close(); }
        }

        private int checkFirstStart()
        {
            int count = 0;
            SqlConnection conn = new SqlConnection(connSTR);
            try
            {
                string sql = $"select count(*) from NewUser";

                conn.Open();
                SqlCommand C = new SqlCommand(sql, conn);

                count = Convert.ToInt32(C.ExecuteScalar());
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally { conn.Close(); }

            return count;
        }       // проверка на первый запуск

        private void Button_History_Click(object sender, RoutedEventArgs e)     // история еды
        {
            Window5 w5 = new Window5();
            w5.Show();
            //   this.Close();
        }

        private void Button_cnu_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow();
            mw.Show();
            this.Close();
        }       // создать нового пользователя

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ComboBox_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
