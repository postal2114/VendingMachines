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
using Microsoft.Win32;
using System.IO;
using System.Data.SqlClient;


namespace WpfCase1
{
    /// <summary>
    /// Логика взаимодействия для drinksadd.xaml
    /// </summary>
    public partial class drinksadd : Window
    {
        public drinksadd(string name, int coust, int count, int id)
        {
            InitializeComponent();
            VendingMachinesEntities database = new VendingMachinesEntities();
            Nazvanie.Text = name;
            price.Text = Convert.ToString(coust);
            kolvo.Text = Convert.ToString(count);
            ID.Content = id;
        }
        public drinksadd()
        {
            InitializeComponent();
        }

            private void fail_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog img = new OpenFileDialog();

            img.Filter = "Image Files(*.JPG; .PNG)|*.JPG; .PNG | All file (*.*)|*.*";
            img.ShowDialog();
            string image1 = img.FileName.ToString();
            image.Source = new BitmapImage(new Uri(image1));
        }
        VendingMachinesEntities database = new VendingMachinesEntities();

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int check = Convert.ToInt32(ID.Content);
            var drinks = database.Drinks.Where(c => c.Id == check).FirstOrDefault();
            var drinks1 = database.VendingMachineDrinks.Where(c => c.DrinksId == check).FirstOrDefault();
            drinks.Name = Nazvanie.Text;
            drinks1.Count = Convert.ToInt32(kolvo.Text);
            drinks.Cost = Convert.ToInt32(kolvo.Text);
            database.SaveChanges();
        }
    }
}
