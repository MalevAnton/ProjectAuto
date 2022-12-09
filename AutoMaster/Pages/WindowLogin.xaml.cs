using AutoMaster.Classes;
using System;
using System.Collections.Generic;
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

namespace AutoMaster.Pages
{
    /// <summary>
    /// Логика взаимодействия для WindowLogin.xaml
    /// </summary>
    public partial class WindowLogin : Window
    {
        TableMaster master;
        public WindowLogin(TableMaster master)
        {
            InitializeComponent();
            this.master = master;
            tbLogin.Text = master.Login;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            master.Login = tbLogin.Text;
            master.Password = pbPassword.Password.GetHashCode();
            BaseClass.ME.SaveChanges();
            this.Close();
        }
    }
}
