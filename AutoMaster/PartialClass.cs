using AutoMaster.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace AutoMaster
{
    public partial class TableApplication
    {
        public string SurnameName
        {
            get
            {
                return "Заказчик: " + Surname + " " + Name + " " + Fatherland;
            }
        }

        public string Brand
        {
            get
            {
                return "Марка: " + TableBrand.Brand; 
            }
        }

        public string Master
        {
            get
            {
                return "Мастер: " + TableMaster.Name;
            }
        }

    }

}
