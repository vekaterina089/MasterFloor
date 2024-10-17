using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Policy;
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
    /// Логика взаимодействия для PartnersPage.xaml
    /// </summary>
    public partial class PartnersPage : Page
    {
        public DBEntities db=new DBEntities();
        public PartnersPage()
        {
            InitializeComponent();
            var partners = from p in db.Partners
                           join t in db.Partners_types 
                           on p.id_type_partners equals t.id__type_partners 
                           join pp in db.Partner_poducts
                           on p.id_partner equals pp.id_partner
                           group pp by new
                           {
                               p.id_partner,
                               t.name_type_partners,
                               p.name_partners,
                               p.director,
                               p.phone,
                               p.rating

                           } into g
                           select new
                           {
                               type_partners = g.Key.name_type_partners,
                               name_partners = g.Key.name_partners,
                               discount = g.Sum(x=> x.quantity),
                               director = g.Key.director,
                               phone = g.Key.phone,
                               rating = g.Key.rating

                           };
            var partners1 = partners.AsEnumerable().Select(p=> new
            {
                type_partners=p.type_partners,
                name_partners=p.name_partners,
                discount = Discount(Convert.ToInt32(p.discount)),
                director=p.director,
                phone=p.phone ,
                rating=p.rating 
            }).ToList();
            listPartners.ItemsSource= partners1;
        }
   
            public string Discount(int quantity)
            {
                if (quantity < 10000)
                {
                    return "0%";
                }
                else if (quantity >= 10000 && quantity < 50000)
                {
                    return "5%";
                }
                else if (quantity >= 50000 && quantity < 300000)
                {
                    return "10%";
                }
                else
                {
                    return "15%";
                }
            }
}
}
