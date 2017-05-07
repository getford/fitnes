using System;
using System.Collections;
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

namespace fitnes
{
    /// <summary>
    /// Логика взаимодействия для Window5.xaml
    /// </summary>
    public partial class Window5 : Window
    {
        const string connSTR = "Data Source=Vladimir-pc;Initial Catalog=PRODUCT;Integrated Security=True";
        int id_user = 0;

        public Window5()
        {
            InitializeComponent();

            getLastId();

            historyBreakfast();
            historyLunch();
            historyDinner();
        }

        private void historyBreakfast()
        {
            SqlConnection connect = new SqlConnection(connSTR);
            string sql = $"select date, product, kalorii from breakfast where id_user = {id_user}";
            try
            {
                connect.Open();
                SqlCommand cmd = new SqlCommand(sql, connect);
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    History his = new History
                    {
                        Date = dr.GetString(0).ToString(),
                        Product = dr.GetString(1).ToString(),
                        Kalorii = dr.GetString(2).ToString()
                    };
                    ((ArrayList)listViewBreakfast.Resources["historyBreakfast"]).Add(his);
                }
                dr.Close();
            }
            catch (SqlException ex) { MessageBox.Show(ex.Message); }
            finally { connect.Close(); }
        }

        private void historyLunch()
        {
            SqlConnection connect = new SqlConnection(connSTR);
            string sql = $"select date, product, kalorii from lunch where id_user = {id_user}";
            try
            {
                connect.Open();
                SqlCommand cmd = new SqlCommand(sql, connect);
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    History his = new History
                    {
                        Date = dr.GetString(0).ToString(),
                        Product = dr.GetString(1).ToString(),
                        Kalorii = dr.GetString(2).ToString()
                    };
                    ((ArrayList)listViewLunch.Resources["historyLunch"]).Add(his);
                }
                dr.Close();
            }
            catch (SqlException ex) { MessageBox.Show(ex.Message); }
            finally { connect.Close(); }
        }

        private void historyDinner()
        {
            SqlConnection connect = new SqlConnection(connSTR);
            string sql = $"select date, product, kalorii from dinner where id_user = {id_user}";
            try
            {
                connect.Open();
                SqlCommand cmd = new SqlCommand(sql, connect);
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    History his = new History
                    {
                        Date = dr.GetString(0).ToString(),
                        Product = dr.GetString(1).ToString(),
                        Kalorii = dr.GetString(2).ToString()
                    };
                    ((ArrayList)listViewDinner.Resources["historyDinner"]).Add(his);
                }
                dr.Close();
            }
            catch (SqlException ex) { MessageBox.Show(ex.Message); }
            finally { connect.Close(); }
        }

        private void getLastId()
        {
            SqlConnection conn = new SqlConnection(connSTR);
            string sqlSelectId = "SELECT MAX(id_user) from NewUser";       // получить последний добавленный id
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sqlSelectId, conn);
                id_user = Convert.ToInt32(cmd.ExecuteScalar());
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally { conn.Close(); }
        }
    }

    public class History
    {
        public string Date { get; set; }
        public string Product { get; set; }
        public string Kalorii { get; set; }
    }
}