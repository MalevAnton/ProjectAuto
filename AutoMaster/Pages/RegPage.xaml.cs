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
    /// Логика взаимодействия для RegPage.xaml
    /// </summary>
    public partial class RegPage : Page
    {
        public RegPage()
        {
            InitializeComponent();

            cbRole.ItemsSource = BaseClass.ME.TableRole.ToList();
            cbRole.SelectedValuePath = "idRole";
            cbRole.DisplayMemberPath = "Role";
            cbRole.SelectedIndex = 1;
        }

        private void btnReg_Click(object sender, RoutedEventArgs e)
        {
            int g = 0;
            if (rbMen.IsChecked == true) g = 1;
            if(rbWomen.IsChecked == true) g = 2;

            TableMaster tableMaster = new TableMaster()
            {
                Surname = tboxSurname.Text,
                Name = tboxName.Text,
                Fatherland = tboxFatherland.Text,
                Birthday = Convert.ToDateTime(dpBirthday.SelectedDate),
                Login = tboxLogin.Text,
                Password = pbPassword.Password.GetHashCode(),
                idGender = g,
                idRole = cbRole.SelectedIndex + 1
            };

            BaseClass.ME.TableMaster.Add(tableMaster);
            BaseClass.ME.SaveChanges();
            MessageBox.Show("Пользователь добавлен");
            FrameClass.MainFrame.Navigate(new LogPage());
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            FrameClass.MainFrame.Navigate(new CreateRepairPage());
        }
    }
}
