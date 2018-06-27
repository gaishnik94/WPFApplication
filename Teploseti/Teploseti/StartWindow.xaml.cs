using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MahApps.Metro.Controls;
using System.Net;
using System.Security.Cryptography;

namespace Teploseti
{
    /// <summary>
    /// Логика взаимодействия для StartWindow.xaml
    /// </summary>
    public partial class StartWindow : MetroWindow
    {
        DBase context;
        string connection;
        public StartWindow()
        {
            InitializeComponent();
            textBox.Text = Properties.Settings.Default.login_db;
            textBox1.Text = Properties.Settings.Default.ip_db;
            checkBox.IsChecked = Properties.Settings.Default.mode;
            textBox1_Copy.Text = Properties.Settings.Default.login;
            passwordBox1.Password = Properties.Settings.Default.password;
        }

        private void textBox1_Copy_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            if (textBox1.Text == "127.0.0.1")
            {
                if (checkBox.IsChecked == true)
                {
                    connection = "Data Source=localhost; Database=Teploseti; User Id=" + textBox1_Copy.Text + "; password=" + passwordBox1.Password;
                }
                else
                    connection = "Data Source=localhost;Database=Teploseti; Trusted_Connection=True";
            }
            else
            {
                if (checkBox.IsChecked == true)
                {
                    connection = "Data Source=" + textBox1.Text + ";Database=Teploseti; User Id=" + textBox1_Copy.Text + "; password=" + passwordBox1.Password;
                }
                else
                    connection = "Data Source=" + textBox1.Text + ";Database=Teploseti; Trusted_Connection=True";
            }
            try
            {
                context = new DBase(connection);
                var acc = context.Аккаунты.Where(c => c.Логин == textBox.Text).ToList();
                if (acc.Count == 0)
                {
                    MessageBox.Show("Login dosn't exist!");
                    return;
                }
                else
                {
                    if ((acc[0].Логин == textBox.Text) && (acc[0].Хэш_пароль == getMD5String(passwordBox.Password)))
                    {
                        Properties.Settings.Default.login_db = textBox.Text;
                        Properties.Settings.Default.ip_db = textBox1.Text;
                        if (checkBox.IsChecked == true)
                            Properties.Settings.Default.mode = true;
                        else
                            Properties.Settings.Default.mode = false;
                        Properties.Settings.Default.login = textBox1_Copy.Text;
                        Properties.Settings.Default.password = passwordBox1.Password;
                        Properties.Settings.Default.Save();
                        if (acc[0].Администратор)
                        {
                            AdminWindow form = new AdminWindow(context, acc[0]);
                            this.Hide();
                            form.ShowDialog();
                            this.Close();
                        }
                        else
                        {
                            StaffWindow form = new StaffWindow(context, acc[0]);
                            this.Hide();
                            form.ShowDialog();
                            this.Close();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Wrong password!");
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error! " + ex.Message);
                return;
            }
        }

        private void textBox1_LostFocus(object sender, RoutedEventArgs e)
        {
            /*
            IPAddress ip = null;
            if (!IPAddress.TryParse(textBox1.Text, out ip))
            {
                textBox1.Text = "127.0.0.1";
            }
            */
        }

        private void checkBox_Click(object sender, RoutedEventArgs e)
        {
            if (checkBox.IsChecked == true)
            {
                textBox1_Copy.IsEnabled = passwordBox1.IsEnabled = true;
            }
            else
            {
                textBox1_Copy.IsEnabled = passwordBox1.IsEnabled = false;
            }
        }
        string getMD5String(string inputString)
        {
            byte[] byteArray = Encoding.ASCII.GetBytes(inputString);
            MD5CryptoServiceProvider md5provider = new MD5CryptoServiceProvider();
            byte[] byteArrayHash = md5provider.ComputeHash(byteArray);
            return BitConverter.ToString(byteArrayHash);
        }
    }
}
