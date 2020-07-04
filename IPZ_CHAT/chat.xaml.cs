using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
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


namespace IPZ_CHAT_KOVALENKO
{
    /// <summary>
    /// Логика взаимодействия для chat.xaml
    /// </summary>
    public partial class chat : Window
    {
        public chat()
        {
            InitializeComponent();
            update_Click(null, null);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
            DB db = new DB();
            MySqlCommand command = new MySqlCommand("SELECT `message` FROM `chat`", db.GetConnection());
            db.openConnection();
            string messages = command.ExecuteScalar().ToString();
            db.closeConnection();
            MySqlCommand command_2 = new MySqlCommand("UPDATE `chat` SET `message` = @message", db.GetConnection());
            string mes = mess.Text;
            if ((mess.Text.Length>0)&&(mess.Text != " ")) {
                string messchat = messages + "\n" + username.Text + ":  " + mes;
                command_2.Parameters.AddWithValue("@message", messchat);
                db.openConnection();
                command_2.ExecuteNonQuery();
                db.closeConnection();
                messag.Text = messchat;
                mess.Text = "";
            }
        }



        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            this.Close();
            main.Show();
        }

        private void update_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                loginpass idus = new loginpass();
                int iduser = idus.getiduser();
                string id = Convert.ToString(iduser);
                DB db = new DB();
               

                MySqlCommand command = new MySqlCommand("SELECT `nicname` FROM `users` WHERE `id` = @ID", db.GetConnection());
                command.Parameters.Add("@ID", MySqlDbType.VarChar).Value = id;

                db.openConnection();
                string nic = command.ExecuteScalar().ToString();
                username.Text = nic;
                db.closeConnection();

                MySqlCommand command_2 = new MySqlCommand("SELECT `message` FROM `chat`", db.GetConnection());

                db.openConnection();
                messag.Text = command_2.ExecuteScalar().ToString();
                db.closeConnection();

            }
            catch (MySql.Data.MySqlClient.MySqlException)
            {
                MessageBox.Show("Не має підключення до БД");
            }
        }

        private void masseg_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
