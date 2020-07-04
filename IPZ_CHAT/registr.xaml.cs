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
using System.Windows.Shapes;

namespace IPZ_CHAT_KOVALENKO
{
    /// <summary>
    /// Логика взаимодействия для registr.xaml
    /// </summary>
    public partial class registr : Window
    {
        public registr()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if ((userlogin.Text.Length < 3) || (userpass.Password.Length < 5))
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

                    MySqlCommand command_3 = new MySqlCommand("SELECT * FROM `users` WHERE `nicname` = @NN ", db.GetConnection());
                    command_3.Parameters.Add("@NN", MySqlDbType.VarChar).Value = userlog;

                    adapter.SelectCommand = command_3;
                    adapter.Fill(table);

                    if (table.Rows.Count > 0)
                    {
                        MessageBox.Show("Користувач з таким логіном вже існує!");
                    }
                    else
                    {
                        MySqlCommand command = new MySqlCommand("INSERT INTO `users`(`nicname`, `password`) VALUES (@nicname,@password)", db.GetConnection());
                        command.Parameters.Add("@nicname", MySqlDbType.VarChar).Value = userlog;
                        command.Parameters.Add("@password", MySqlDbType.VarChar).Value = userpas;

                        db.openConnection();
                        if (command.ExecuteNonQuery() == 1)
                        {
                            MessageBox.Show("Аккаунт створено!");
                            MainWindow main = new MainWindow();
                            this.Close();
                            main.Show();
                        }
                        else
                        {
                            MessageBox.Show("Аккаунт не створено!");
                            userlogin.Text = "";
                            userpass.Password = "";
                        }
                        db.closeConnection();
                    }
                }
            }
            catch (MySql.Data.MySqlClient.MySqlException)
            {
                MessageBox.Show("Не має підключення до БД");
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            this.Close();
            main.Show();
        }
    }
}
