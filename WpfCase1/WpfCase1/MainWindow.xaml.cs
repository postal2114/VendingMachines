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
using System.Windows.Threading;

namespace WpfCase1
{
    public class getdrink
    {
        public string Name { get; set; }
        public string Cost { get; set; }
    }
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Drinks_list.ItemsSource = drinks.Drinks.ToList();
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(2);
            timer.Tick += UpdateData;
            timer.Start();
        }

        public void UpdateData(object sender, object e)
        {
            VendingMachinesEntities database = new VendingMachinesEntities();
            Drinks_list.ItemsSource = database.Drinks.ToList();
            List<Button> CoinButtons = new List<Button>() { One_button, two_button, three_button, four_button };
            for (int i = 0; i < CoinButtons.Count; i++)
            {
                var coindem = CoinButtons[i].Content;
                var u = database.Coins.Single(a => a.Denomination.ToString() == coindem.ToString());
                var id = u.Id;

                var q = database.VendingMachineCoins.Single(d => d.CoinsId == id);
                var active = q.IsActive;
                if (active == 0)
                {
                    CoinButtons[i].IsEnabled = false;
                }
                else
                {
                    CoinButtons[i].IsEnabled = true;
                }
            }
        }
        public VendingMachinesEntities drinks = new VendingMachinesEntities();
        public int[] coins_insert = new int[4];
        private void perehod(object sender, RoutedEventArgs e)
        {
            Window1 Admin = new Window1();
            Admin.Show();
        }

        private void One_button_Click(object sender, RoutedEventArgs e)
        {
            Money_label1.Content = Convert.ToInt32(Money_label1.Content) + Convert.ToInt32((sender as Button).Content);

        }

        private void count_button_Click(object sender, RoutedEventArgs e)
        {
            int idmax = drinks.VendingMachineCoins.Max(id => id.CoinsId);
            int[] coins = new int[4];
            for (int i = 0; i < idmax; i++)
            {
                VendingMachineCoins nowCoin = drinks.VendingMachineCoins.Single(id => id.CoinsId == (i + 1));
                coins[i] = nowCoin.Count;
            }
            MessageBox.Show($"1 = {coins[0]}\n 2 = {coins[1]}\n 5 = {coins[2]}\n 10 = {coins[3]}\n");

            // расчёт 
            int money_out = Convert.ToInt32(Money_label1.Content);
            Money_label1.Content = "0";
            int tens_out = money_out / 10;
            if (coins[3] < tens_out)
            {
                money_out -= coins[3] * 10;
                tens_out = coins[3];
            }
            else
            {
                money_out -= tens_out * 10;
            }

            int fives_out = money_out / 5;
            if (coins[2] < fives_out)
            {
                money_out -= coins[2] * 5;
                fives_out = coins[2];
            }
            else
            {
                money_out -= fives_out * 5;
            }


            int twos_out = money_out / 2;
            if (coins[1] < twos_out)
            {
                money_out -= coins[1] * 2;
                twos_out = coins[1];
            }
            else
            {
                money_out -= twos_out * 2;
            }

            int ones_out = money_out / 1;
            if (coins[0] < ones_out)
            {
                money_out -= coins[0] * 1;
                ones_out = coins[0];
            }
            else
            {
                money_out -= ones_out * 1;
            }

            coins[0] -= ones_out;
            coins[1] -= twos_out;
            coins[2] -= fives_out;
            coins[3] -= tens_out;

            MessageBox.Show($"Сдача:\n10 = {tens_out}\n5 = {fives_out}\n2 = {twos_out}\n1 = {ones_out}\nОстаток = {money_out}");

            for (int i = 0; i < idmax; i++)
            {
                VendingMachineCoins nowCoin = drinks.VendingMachineCoins.Single(id => id.CoinsId == (i + 1));
                nowCoin.Count = coins[i];
            }

            //MessageBox.Show("итог = " + Money_label1.Content);
            // Money_label1.Content = '0';

        }

        private void List_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            if (Drinks_list.SelectedItem != null)
            {
                var a = new VendingMachineCoins();
                for (int i = 0; i < a.CoinsId; i++)
                {
                    var nowCoin = drinks.VendingMachineCoins.Single(id => id.CoinsId == i + 1);
                    nowCoin.Count += coins_insert[i];
                }
                int money = Convert.ToInt32(Money_label1.Content),
                    cost = Convert.ToInt32((Drinks_list.SelectedItem as Drinks).Cost);
                if (money >= cost)
                {
                    Money_label1.Content = money - cost;
                }
                else
                {
                    MessageBox.Show("Недостаточно денег");
                }
                Drinks_list.SelectedItem = null;
            }
        }
    }
}


