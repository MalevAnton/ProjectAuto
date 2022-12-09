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
    /// Логика взаимодействия для ShowAutoPage.xaml
    /// </summary>
    public partial class ShowAutoPage : Page
    {
        PageChange pc = new PageChange();
        List<TableApplication> AutoFilter = new List<TableApplication>();

        public ShowAutoPage()
        {
            InitializeComponent();
            BaseClass.ME = new AntonEntities();
            AutoFilter = BaseClass.ME.TableApplication.ToList();
            listAuto.ItemsSource = BaseClass.ME.TableApplication.ToList();
            pc.CountPage = BaseClass.ME.TableApplication.ToList().Count;
            DataContext = pc;
            List<TableBrand> TB = BaseClass.ME.TableBrand.ToList();
            cmbBrand.Items.Add("Все марки");

            for (int i = 0; i < TB.Count; i++)
            {
                cmbBrand.Items.Add(TB[i].Brand);
            }

            cmbBrand.SelectedIndex = 0;
            cmbSort.SelectedIndex = 0;

            tbCount.Text = "Количество записей: " + BaseClass.ME.TableApplication.ToList().Count;

        }

        private void txtPageCount_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                pc.CountPage = Convert.ToInt32(txtPageCount.Text);
            }

            catch
            {
                pc.CountPage = AutoFilter.Count;
            }

            pc.Countlist = AutoFilter.Count;
            listAuto.ItemsSource = AutoFilter.Skip(0).Take(pc.CountPage).ToList();
            pc.CurrentPage = 1;
        }

        private void GoPage_MouseDown(object sender, MouseButtonEventArgs e)
        {
            TextBlock tb = (TextBlock)sender;

            switch (tb.Uid)
            {
                case "prev":
                    pc.CurrentPage--;
                    break;

                case "next":
                    pc.CurrentPage++;
                    break;

                default:
                    pc.CurrentPage = Convert.ToInt32(tb.Text);
                    break;
            }

            listAuto.ItemsSource = AutoFilter.Skip(pc.CurrentPage * pc.CountPage - pc.CountPage).Take(pc.CountPage).ToList();
        }

        private void btn_Click(object sender, RoutedEventArgs e)
        {
            pc.CurrentPage = 1;

            try
            {
                pc.CountPage = Convert.ToInt32(txtPageCount.Text);
            }

            catch
            {
                pc.CountPage = AutoFilter.Count;
            }
            
            pc.Countlist = AutoFilter.Count;
            listAuto.ItemsSource = AutoFilter.Skip(0).Take(pc.CountPage).ToList();
        }

        private void tbMoney_Loaded(object sender, RoutedEventArgs e)
        {
            TextBlock tb = (TextBlock)sender;
            int index = Convert.ToInt32(tb.Uid);
            List<TableRepairApp> TRA = BaseClass.ME.TableRepairApp.Where(x => x.idRepair == index).ToList();
            int sum = new int();

            foreach (TableRepairApp tra in TRA)
            {
                sum = Convert.ToInt32(tra.TableRepair.Price);
            }

            tb.Text = "Затраты на ремонт: " + sum.ToString() + " руб.";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            int index = Convert.ToInt32(btn.Uid);
            TableApplication application = BaseClass.ME.TableApplication.FirstOrDefault(x => x.idApplication == index);
            BaseClass.ME.TableApplication.Remove(application);
            BaseClass.ME.SaveChanges();
            FrameClass.MainFrame.Navigate(new ShowAutoPage());
        }

        private void btnupdate_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            int index = Convert.ToInt32(btn.Uid);
            TableApplication application = BaseClass.ME.TableApplication.FirstOrDefault(x => x.idApplication == index);
            FrameClass.MainFrame.Navigate(new CreateRepairPage(application));
        }

        private void btnCreateRepair_Click(object sender, RoutedEventArgs e)
        {
            FrameClass.MainFrame.Navigate(new CreateRepairPage());
        }


        void Filter()
        {
            List<TableApplication> applicationList = new List<TableApplication>();
            string brand = cmbBrand.SelectedValue.ToString();
            int index = cmbBrand.SelectedIndex;

            if (index != 0)
            {
                applicationList = BaseClass.ME.TableApplication.Where(x => x.TableBrand.Brand == brand).ToList();
            }

            else
            {
                applicationList = BaseClass.ME.TableApplication.ToList();
            }

            if (!string.IsNullOrWhiteSpace(tbSurname.Text))
            {
                applicationList = applicationList.Where(x => x.Surname.ToLower().Contains(tbSurname.Text.ToLower())).ToList();
            }

            if (cbPhoto.IsChecked == true)
            {
                applicationList = applicationList.Where(x => x.Photo != null).ToList();
            }

            switch (cmbSort.SelectedIndex)
            {
                case 1:
                    {
                        applicationList.Sort((x, y) => x.Surname.CompareTo(y.Surname));
                    }
                    break;
                case 2:
                    {
                        applicationList.Sort((x, y) => x.Surname.CompareTo(y.Surname));
                        applicationList.Reverse();
                    }
                    break;
            }

            listAuto.ItemsSource = applicationList;

            if (applicationList.Count == 0)
            {
                MessageBox.Show("нет записей");
            }

            tbCount.Text = "Количество записей " + applicationList.Count;
        }

        private void tbSurname_TextChanged(object sender, TextChangedEventArgs e)
        {
            Filter();
        }

        private void cbPhoto_Checked(object sender, RoutedEventArgs e)
        {
            Filter();
        }

        private void cmbBrand_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            Filter();
        }

        private void cmbSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Filter();
        }

    }

}
