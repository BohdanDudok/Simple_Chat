using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
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

namespace IPZ_CHAT_KOVALENKO
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            registr reg = new registr();
            this.Close();
            reg.Show();
        }

        private void log_in_Click(object sender, RoutedEventArgs e)
        {
            if ((userlogin.Text.Length < 3) && (userpass.Password.Length < 5))
            {
                MessageBox.Show("Логін або пароль занадто короті!");
            }
            else
            {
                String userlog = userlogin.Text;
                String userpas = userpass.Password;

                DB db = new DB();
                DataTable table = new DataTable();

                MySqlDataAdapter adapter = new MySqlDataAdapter();
                try
                {
                    MySqlCommand command = new MySqlCommand("SELECT `id` FROM `users` WHERE `nicname` = @NN AND `password` = @UP", db.GetConnection());
                    command.Parameters.Add("@NN", MySqlDbType.VarChar).Value = userlog;
                    command.Parameters.Add("@UP", MySqlDbType.VarChar).Value = userpas;

                    adapter.SelectCommand = command;
                    adapter.Fill(table);

                    if (table.Rows.Count > 0)
                    {
                        db.openConnection();
                        string id = command.ExecuteScalar().ToString();
                        int iduser = Convert.ToInt32(id);
                        loginpass idus = new loginpass();
                        idus.setiduser(iduser);
                        db.closeConnection();
                        MessageBox.Show("Успішний вхід!", "Повідомлення");
                        chat chat = new chat();
                        this.Close();
                        chat.Show();
                    }
                    else
                    {
                        MessageBox.Show("Не правильний логін або пароль!", "Повідомлення");
                        userpass.Password = "";
                        userlogin.Text = "";
                    }
                }
                catch (MySql.Data.MySqlClient.MySqlException)
                {
                    MessageBox.Show("Не має підключення до БД");
                }
            }
            
        }
    }
}
