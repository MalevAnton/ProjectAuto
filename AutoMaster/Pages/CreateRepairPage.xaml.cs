using AutoMaster.Classes;
using Microsoft.Win32;
using System;
using System.Drawing;
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
    /// Логика взаимодействия для CreateRepairPage.xaml
    /// </summary>
    public partial class CreateRepairPage : Page
    {
        TableApplication APPLICATION;
        bool flagUpgrade = false;
        string path;

        public void uploadFields()
        {
            cmbBrand.ItemsSource = BaseClass.ME.TableBrand.ToList();
            cmbBrand.SelectedValuePath = "idBrand";
            cmbBrand.DisplayMemberPath = "Brand";

            cmbMaster.ItemsSource = BaseClass.ME.TableMaster.ToList();
            cmbMaster.SelectedValuePath = "idMaster";
            cmbMaster.DisplayMemberPath = "Name";

            lbRepair.ItemsSource = BaseClass.ME.TableRepair.ToList();
        }

        public CreateRepairPage(TableApplication application)
        {
            InitializeComponent();
            uploadFields();
            flagUpgrade = true;
            APPLICATION = application;
            tbSurname.Text = application.Surname;
            tbName.Text = application.Name;
            tbFatherland.Text = application.Fatherland;
            cmbBrand.SelectedIndex = application.idBrand - 1;
            cmbMaster.SelectedIndex = application.idMaster - 1;

            List<TableRepairApp> ra = BaseClass.ME.TableRepairApp.Where(x => x.idRepair == application.idRepair).ToList(); 

            foreach (TableRepair r in lbRepair.Items)
            {
                if (ra.FirstOrDefault(x => x.idRepair == r.idRepair) != null)
                {
                    lbRepair.SelectedItems.Add(r);
                }
            }

            if (application.Photo != null)
            {
                BitmapImage img = new BitmapImage(new Uri(application.Photo, UriKind.RelativeOrAbsolute));
                photoAuto.Source = img;
            }
        }

        public CreateRepairPage()
        {
            InitializeComponent();
            uploadFields();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        { 
            try
            {
                if (flagUpgrade == false)
                {
                    APPLICATION = new TableApplication();
                }

                APPLICATION.Surname = tbSurname.Text;
                APPLICATION.Name = tbName.Text;
                APPLICATION.Fatherland = tbFatherland.Text;
                APPLICATION.idBrand = cmbBrand.SelectedIndex + 1;
                APPLICATION.idMaster = cmbMaster.SelectedIndex + 1;
                APPLICATION.Photo = path;

                if (flagUpgrade == false)
                {
                    BaseClass.ME.TableApplication.Add(APPLICATION);
                }

                List<TableRepairApp> repapp = BaseClass.ME.TableRepairApp.Where(x => APPLICATION.idRepair == x.idRepair).ToList();

                if (repapp.Count > 0)
                {
                    foreach (TableRepairApp app in repapp)
                    {
                        BaseClass.ME.TableRepairApp.Remove(app);
                    }
                }

                foreach (TableRepair app in lbRepair.SelectedItems)
                {
                    TableRepairApp RA = new TableRepairApp()
                    {
                        idApplication = APPLICATION.idApplication,
                        idRepair = app.idRepair,
                    };

                    BaseClass.ME.TableRepairApp.Add(RA);
                }

                BaseClass.ME.SaveChanges();
                MessageBox.Show("Информация добавлена");
            }
            
            catch
            {
                MessageBox.Show("Что-то пошло не по плану");
            }

        }

        private void btnPhto_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog OFD = new OpenFileDialog();
            OFD.ShowDialog();
            path = OFD.FileName;
            string[] arrayPath = path.Split('\\');
            path = "\\" + arrayPath[arrayPath.Length - 2] + "\\" + arrayPath[arrayPath.Length - 1];
            // MessageBox.Show(path);
        }
    }
}
