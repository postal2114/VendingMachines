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

namespace WpfCase1.Pages
{
    /// <summary>
    /// Логика взаимодействия для drinks.xaml
    /// </summary>
    public partial class drinks : Page
    {
        public drinks()
        {
            InitializeComponent();
            VendingMachinesEntities database = new VendingMachinesEntities();
            lv.ItemsSource = database.Drinks.ToList();

        }
        public VendingMachinesEntities database = new VendingMachinesEntities();


        private void lv_SelectionChanged(object sender, SelectionChangedEventArgs e)
        { 
           
            if (lv.SelectedItem != null)
            {
                var a = new VendingMachineDrinks();
                int check = Convert.ToInt32((lv.SelectedItem as Drinks).Id);
                var selectdrink = database.Drinks.Where(c => c.Id == check).FirstOrDefault();
                var selectdrink2 = database.VendingMachineDrinks.Where(c => c.DrinksId == check).FirstOrDefault();
                string name = selectdrink.Name;
                int cost = Convert.ToInt32(selectdrink.Cost);
                int count = Convert.ToInt32(selectdrink2.Count);
                int id = Convert.ToInt32(selectdrink.Id);
                new drinksadd(name, cost, count, id).ShowDialog();

            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {

            new drinksadd().ShowDialog();


        }

        private void Frame_Navigated(object sender, NavigationEventArgs e)
        {

        }
    }
}
