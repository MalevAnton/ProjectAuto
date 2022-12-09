using AutoMaster.Classes;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
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
    /// Логика взаимодействия для PersonalPage.xaml
    /// </summary>
    public partial class PersonalPage : Page
    {
        TableMaster master;

        void showImage(byte[] Barray, System.Windows.Controls.Image img)
        {
            BitmapImage BI = new BitmapImage();
            using (MemoryStream m = new MemoryStream(Barray))
            {
                BI.BeginInit();
                BI.StreamSource = m;
                BI.CacheOption = BitmapCacheOption.OnLoad;
                BI.EndInit();
            }
            img.Source = BI;
            img.Stretch = Stretch.Uniform;
        }

        public PersonalPage(TableMaster master)
        {
            InitializeComponent();
            this.master = master;
            tbName.Text = master.Name;
            tbSurname.Text = master.Surname;
            tbFatherland.Text = master.Fatherland;

            List<MasterPhoto> m = BaseClass.ME.MasterPhoto.Where(x => x.idMaster == master.idMaster).ToList();

            if (m.Count > 0)
            {
                byte[] Bar = m[m.Count - 1].PhotoBinary;
                showImage(Bar, imMaster);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            WindowPersonal windowPersonal = new WindowPersonal(master);
            windowPersonal.ShowDialog();
            FrameClass.MainFrame.Navigate(new PersonalPage(master));
        }

        private void ButtonLog_Click(object sender, RoutedEventArgs e)
        {
            WindowLogin windowLogin = new WindowLogin(master);
            windowLogin.ShowDialog();
            FrameClass.MainFrame.Navigate(new WindowLogin(master));
        }

        private void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
            WindowPersonal windowPersonal = new WindowPersonal(master);
            windowPersonal.ShowDialog();
            FrameClass.MainFrame.Navigate(new PersonalPage(master));
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                MasterPhoto u = new MasterPhoto();

                OpenFileDialog OFD = new OpenFileDialog();

                OFD.ShowDialog();

                string path = OFD.FileName;

                System.Drawing.Image SDI = System.Drawing.Image.FromFile(path);

                ImageConverter IC = new ImageConverter();

                byte[] Barray = (byte[])IC.ConvertTo(SDI, typeof(byte[]));
                u.PhotoBinary = Barray;
                BaseClass.ME.MasterPhoto.Add(u);
                BaseClass.ME.SaveChanges();
                MessageBox.Show("Фото добавлено");
                FrameClass.MainFrame.Navigate(new PersonalPage(master));

            }
            catch
            {
                MessageBox.Show("Что-то пошло не так");
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            try
            {
                OpenFileDialog OFD = new OpenFileDialog();
                OFD.Multiselect = true;
                if (OFD.ShowDialog() == true)
                {
                    foreach (string file in OFD.FileNames)
                    {
                        MasterPhoto u = new MasterPhoto();
                        u.idMaster = master.idMaster;
                        string path = file;
                        System.Drawing.Image SDI = System.Drawing.Image.FromFile(file);
                        ImageConverter IC = new ImageConverter();
                        byte[] Barray = (byte[])IC.ConvertTo(SDI, typeof(byte[]));
                        u.PhotoBinary = Barray;
                        BaseClass.ME.MasterPhoto.Add(u);
                    }
                    BaseClass.ME.SaveChanges();
                    MessageBox.Show("Фото добавлены");
                }
            }
            catch
            {
                MessageBox.Show("Что-то пошло не так");
            }
        }

        int n = 0;
        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            spGallery.Visibility = Visibility.Visible;
            List<MasterPhoto> u = BaseClass.ME.MasterPhoto.Where(x => x.idMaster == master.idMaster).ToList();
            if (u != null)
            {

                byte[] Bar = u[n].PhotoBinary;
                showImage(Bar, imgGallery);
            }
        }

        private void Next_Click(object sender, RoutedEventArgs e)
        {
            List<MasterPhoto> u = BaseClass.ME.MasterPhoto.Where(x => x.idMaster == master.idMaster).ToList();
            n++;
            if (Back.IsEnabled == false)
            {
                Back.IsEnabled = true;
            }
            if (u != null)
            {

                byte[] Bar = u[n].PhotoBinary;
                showImage(Bar, imgGallery);
            }
            if (n == u.Count - 1)
            {
                Next.IsEnabled = false;
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            List<MasterPhoto> u = BaseClass.ME.MasterPhoto.Where(x => x.idMaster == master.idMaster).ToList();
            n--;
            if (Next.IsEnabled == false)
            {
                Next.IsEnabled = true;
            }
            if (u != null)
            {

                byte[] Bar = u[n].PhotoBinary;
                BitmapImage BI = new BitmapImage();
                showImage(Bar, imgGallery);
            }
            if (n == 0)
            {
                Back.IsEnabled = false;
            }
        }

        private void btnOld_Click(object sender, RoutedEventArgs e)
        {
            List<MasterPhoto> u = BaseClass.ME.MasterPhoto.Where(x => x.idMaster == master.idMaster).ToList();
            byte[] Bar = u[n].PhotoBinary;
            showImage(Bar, imMaster);
        }

    }
}
