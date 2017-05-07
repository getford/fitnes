using System;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

namespace fitnes
{
    /// <summary>
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        const string connSTR = "Data Source=Vladimir-pc;Initial Catalog=PRODUCT;Integrated Security=True";
        const string pattern = "^[0-9]+$";
        char idPol = '-';
        int idExercise = 0;

        public Window1()
        {
            InitializeComponent();
            getExercises();

            pol.Items.Add("Мужской");
            pol.Items.Add("Женский");
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {


            if (pol.SelectedIndex < 0)
                MessageBox.Show("Пол не выбран.", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
            else
            {
                if (vid_sp.SelectedIndex < 0)
                    MessageBox.Show("Вид спорта не выбран.", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
                else
                {
                    getIdExercise();

                    switch (pol.SelectedItem.ToString())
                    {
                        case "Мужской":
                            idPol = 'm';
                            break;
                        case "Женский":
                            idPol = 'f';
                            break;
                    }

                    if (age.Text.ToString() == string.Empty || ves.Text.ToString() == string.Empty || rost.Text.ToString() == string.Empty)
                        MessageBox.Show("Заполните все поля", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    else
                    {
                        if (!Regex.IsMatch(age.Text.ToString(), pattern) || !Regex.IsMatch(ves.Text.ToString(), pattern) || !Regex.IsMatch(rost.Text.ToString(), pattern))
                            MessageBox.Show("Не корректо заполнены поля\nПроверте возраст, рост, вес.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        else
                        {
                            SqlConnection conn = new SqlConnection(connSTR);
                            try
                            {
                                string sql = $"update NewUser set age='{ Convert.ToInt32(age.Text.ToString())}'," +
                                    $"weight='{Convert.ToInt32(ves.Text.ToString())}',exercises='{Convert.ToInt32(idExercise.ToString())}'," +
                                    $"height='{Convert.ToInt32(rost.Text)}',sex='{idPol}'where id_user = '{text_box_name.Text}'";

                                conn.Open();
                                SqlCommand C = new SqlCommand(sql, conn);

                                C.ExecuteNonQuery(); //метод позволяет выполнять операции с каталогом или вносить изменения в базу данных, не используя DataSet, с помощью операторов UPDATE, INSERT или DELETE
                                Window2 w2 = new Window2();
                                w2.Show();
                                w2.id_user.Text = text_box_name.Text.ToString();
                            }
                            catch (SqlException ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                            finally { conn.Close(); }
                            this.Close();

                        }
                    }
                }
            }
        }

        private void getExercises()
        {
            SqlConnection connect = new SqlConnection(connSTR);
            string sql = $"select exercises from ex";
            try
            {
                connect.Open();
                SqlCommand cmd = new SqlCommand(sql, connect);
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    vid_sp.Items.Add(dr.GetString(0));
                }
                dr.Close();
            }
            catch (SqlException ex) { MessageBox.Show(ex.Message); }
            finally { connect.Close(); }

        }

        private void getIdExercise()
        {
            SqlConnection connect = new SqlConnection(connSTR);
            string sql = $"select nomer from ex where exercises = '{vid_sp.SelectedItem.ToString()}'";
            try
            {
                connect.Open();
                SqlCommand cmd = new SqlCommand(sql, connect);
                idExercise = Convert.ToInt32(cmd.ExecuteScalar());
            }
            catch (SqlException ex) { MessageBox.Show(ex.Message); }
            finally { connect.Close(); }

        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {

        }

        private void TextBox_TextChanged_2(object sender, TextChangedEventArgs e)
        {

        }

        private void TextBox_TextChanged_3(object sender, TextChangedEventArgs e)
        {

        }

        private void TextBox_TextChanged_4(object sender, TextChangedEventArgs e)
        {

        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

    }
}