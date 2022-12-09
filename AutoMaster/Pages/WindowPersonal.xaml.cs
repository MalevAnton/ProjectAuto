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
    /// Логика взаимодействия для WindowPersonal.xaml
    /// </summary>
    public partial class WindowPersonal : Window
    {
        TableMaster master;
        public WindowPersonal(TableMaster master)
        {
            InitializeComponent();
            this.master = master;
            tbName.Text = master.Name;
            tbSurname.Text = master.Surname;
            tbFatherland.Text = master.Fatherland;
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            master.Name = tbName.Text; 
            master.Surname = tbSurname.Text; 
            master.Fatherland = tbFatherland.Text;
            BaseClass.ME.SaveChanges();
            this.Close();
        }
    }
}
