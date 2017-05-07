using System;
using System.Data.SqlClient;
using System.Windows;

namespace fitnes
{
    /// <summary>
    /// Логика взаимодействия для Window2.xaml
    /// </summary>
    public partial class Window2 : Window
    {
        const string connSTR = "Data Source=Vladimir-pc;Initial Catalog=PRODUCT;Integrated Security=True";

        int wYW = 0;            // число в зависимости от выбора чего ты хочешь
        char idPol = '-';
        int weight = 0;         // вес
        int height = 0;         // рост
        int kalorii = 0;        // калории в зависимости от спорта
        int idExercise = 0;     // id упражнения
        int age = 0;            // возраст

        double resKallorii = 0.0;        // Результат подсчета каллорий

        public Window2()
        {
            InitializeComponent();
            whatYouWant.Items.Add("Похудеть");
            whatYouWant.Items.Add("Набрать вес");
            whatYouWant.Items.Add("Оставить вес");
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (whatYouWant.SelectedIndex < 0)
                MessageBox.Show("Скажите мне, чего вы хотите :)", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
            else
            {
                selectInfo();
                wyw();

                if (idPol == 'm')
                {
                    switch (whatYouWant.SelectedItem.ToString())
                    {
                        case "Похудеть":
                            wYW = -200;
                            break;
                        case "Набрать вес":
                            wYW = 0;
                            break;
                        case "Оставить вес":
                            wYW = 500;
                            break;
                    }
                    resKallorii = (655.1 + 9.6 * weight + 1.85 * height - 4.68 * age + kalorii * weight) + wYW;
                }

                if (idPol == 'f')
                {
                    switch (whatYouWant.SelectedItem.ToString())
                    {
                        case "Похудеть":
                            wYW = -250;
                            break;
                        case "Набрать вес":
                            wYW = 0;
                            break;
                        case "Оставить вес":
                            wYW = 300;
                            break;
                    }

                    resKallorii = (9.99 * weight + 6.25 * height - 4.92 * age + kalorii * weight) + wYW;
                }


                Window3 w3 = new Window3();
                w3.resKalorii.Text = resKallorii.ToString();
                w3.Show();
                this.Close();
            }
        }

        private void selectInfo()
        {
            SqlConnection connect = new SqlConnection(connSTR);
            try
            {
                connect.Open();

                string sqlSelectSex = $"select sex from NewUser where id_user = {id_user.Text.ToString()}";
                SqlCommand cmdSS = new SqlCommand(sqlSelectSex, connect);
                idPol = Convert.ToChar(cmdSS.ExecuteScalar());

                string sqlSelectWeight = $"select weight from NewUser where id_user = {id_user.Text.ToString()}";
                SqlCommand cmdSW = new SqlCommand(sqlSelectWeight, connect);
                weight = Convert.ToInt32(cmdSW.ExecuteScalar());

                string sqlSelectHeight = $"select height from NewUser where id_user = {id_user.Text.ToString()}";
                SqlCommand cmdSH = new SqlCommand(sqlSelectHeight, connect);
                height = Convert.ToInt32(cmdSH.ExecuteScalar());

                string sqlSelectAge = $"select age from NewUser where id_user = {id_user.Text.ToString()}";
                SqlCommand cmdSA = new SqlCommand(sqlSelectAge, connect);
                age = Convert.ToInt32(cmdSA.ExecuteScalar());

                string sqlSelectIdExercise = $"select exercises from NewUser where id_user = {id_user.Text.ToString()}";
                SqlCommand cmdSIE = new SqlCommand(sqlSelectIdExercise, connect);
                idExercise = Convert.ToInt32(cmdSIE.ExecuteScalar());

                string sqlSelectKalorii = $"select kalorii from ex where nomer  = {idExercise.ToString()}";
                SqlCommand cmdSK = new SqlCommand(sqlSelectKalorii, connect);
                kalorii = Convert.ToInt32(cmdSK.ExecuteScalar());
            }
            catch (SqlException ex) { MessageBox.Show(ex.Message); }
            finally { connect.Close(); }

        }

        private void wyw()
        {
            SqlConnection conn = new SqlConnection(connSTR);
            string sqlSelectId = "SELECT MAX(id_user) from NewUser";

            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sqlSelectId, conn);
                int id = Convert.ToInt32(cmd.ExecuteScalar());

                string sql = $"update NewUser set wyw = '{whatYouWant.SelectedItem.ToString()}' where id_user = {id}";
                SqlCommand C = new SqlCommand(sql, conn);
                C.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally { conn.Close(); }
        }

    }
}
