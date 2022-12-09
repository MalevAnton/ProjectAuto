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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AutoMaster.Pages
{
    /// <summary>
    /// Логика взаимодействия для LogPage.xaml
    /// </summary>
    public partial class LogPage : Page
    {
        public LogPage()
        {
            InitializeComponent();
        }

        public void Button_Click(object sender, RoutedEventArgs e)
        {
            int p = pbPassword.Password.GetHashCode();

            TableMaster logMaster = BaseClass.ME.TableMaster.FirstOrDefault(x => x.Login == tboxLogin.Text && x.Password == p);

            if (logMaster == null)
            {
                MessageBox.Show("Пользователя не существует");
            }

            else
            {
                switch(logMaster.idRole)
                {
                    case 1:
                        FrameClass.MainFrame.Navigate(new PersonalPage(logMaster));
                        MessageBox.Show("Вы вошли как пользователь");
                        break;
                    case 2:
                        FrameClass.MainFrame.Navigate(new AdminPage(logMaster));
                        MessageBox.Show("Вы вошли как администратор");
                        break;
                    default:
                        MessageBox.Show("Пока");
                        break;
                }
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            FrameClass.MainFrame.Navigate(new CreateRepairPage());
        }
    }
}

//    Авторизация

//    Администратор - Anton 111111
//    Пользователь - Ivan 222222