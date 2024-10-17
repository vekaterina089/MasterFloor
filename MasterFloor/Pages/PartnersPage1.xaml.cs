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

namespace MasterFloor.Pages
{
    /// <summary>
    /// Логика взаимодействия для PartnersPage1.xaml
    /// </summary>
    public partial class PartnersPage1 : Page
    {
        DBEntities db=new DBEntities();
        public PartnersPage1()
        {
            InitializeComponent();
            var partners = from p in db.Partners
                           join t in db.Partners_types
                           on p.id_type_partners equals t.id__type_partners
                           select new
                           {
                               type_partners = t.name_type_partners,
                               name_partners = p.name_partners,
                               director = p.director,
                               phone = p.phone,
                               rating = p.rating

                           };
            DGPartners.ItemsSource=partners.ToList();
        }

    }
}
