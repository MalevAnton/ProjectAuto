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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AutoMaster.Pages
{
    /// <summary>
    /// Логика взаимодействия для ReklamaPage.xaml
    /// </summary>
    public partial class ReklamaPage : Page
    {
        public ReklamaPage()
        {
            InitializeComponent();

            DoubleAnimation FSA = new DoubleAnimation();
            FSA.From = 20;
            FSA.To = 30;
            FSA.Duration = TimeSpan.FromSeconds(2);
            FSA.RepeatBehavior = RepeatBehavior.Forever;
            FSA.AutoReverse = true;
            tbZakaz.BeginAnimation(FontSizeProperty, FSA);
        }

        public void btnClick_Rekl(object sender, RoutedEventArgs e)
        {

            ColorAnimation BA = new ColorAnimation();
            Color Cstart = Color.FromRgb(255, 0, 0); // присваивание начального цвета эл-ту
            btnRekl.Background = new SolidColorBrush(Cstart);
            BA.From = Cstart;
            BA.To = Color.FromRgb(0, 255, 0);
            BA.Duration = TimeSpan.FromSeconds(5);
            BA.RepeatBehavior = RepeatBehavior.Forever;
            BA.AutoReverse = true;
            btnRekl.Background.BeginAnimation(SolidColorBrush.ColorProperty, BA);
        }
    }
}
