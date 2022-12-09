using AutoMaster.Classes;
using AutoMaster.Pages;
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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AutoMaster
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            BaseClass.ME = new AntonEntities();
            FrameClass.MainFrame = fMain;
            FrameClass.MainFrame.Navigate(new CreateRepairPage());
        }

        private void btnReg_Click(object sender, RoutedEventArgs e)
        {
            FrameClass.MainFrame.Navigate(new RegPage());
        }

        private void btnLog_Click(object sender, RoutedEventArgs e)
        {
            FrameClass.MainFrame.Navigate(new LogPage());
        }

        private void btnRek_Click(object sender, RoutedEventArgs e)
        {


            FrameClass.MainFrame.Navigate(new ReklamaPage());
        }
    }
}
