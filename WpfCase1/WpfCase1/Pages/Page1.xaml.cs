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
    /// Логика взаимодействия для Page1.xaml
    /// </summary>
    public partial class Page1 : Page
    {
        public Page1()
        {
            InitializeComponent();
            VendingMachinesEntities database = new VendingMachinesEntities();
            List<TextBox> CoinTxt = new List<TextBox>() { textBox, textBox_Copy, textBox_Copy1, textBox_Copy2 };
            List<CheckBox> CoinCh = new List<CheckBox>() { checkBox, checkBox_Copy, checkBox_Copy1, checkBox_Copy2 };
            List<Label> CoinLbl = new List<Label>() { lbl1rubles, lbl2rubles, lbl5rubles, lbl10rubles };
            for (int i = 0; i < CoinCh.Count; i++)
            {
                var coindem = CoinLbl[i].Content;
                var u = database.Coins.Single(a => a.Denomination.ToString() == coindem.ToString());
                var id = u.Id;

                var q = database.VendingMachineCoins.Single(d => d.CoinsId == id);
                var coincount = q.Count;
                CoinTxt[i].Text = coincount.ToString();
                var active = q.IsActive;
                if (active == 1)
                {
                    CoinCh[i].IsChecked = true;
                }
                else
                {
                    CoinCh[i].IsChecked = false;
                }
            }
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            VendingMachinesEntities database = new VendingMachinesEntities();
            List<TextBox> CoinTxt = new List<TextBox>() { textBox, textBox_Copy, textBox_Copy1, textBox_Copy2 };
            List<CheckBox> CoinCh = new List<CheckBox>() { checkBox, checkBox_Copy, checkBox_Copy1, checkBox_Copy2 };
            List<Label> CoinLbl = new List<Label>() { lbl1rubles, lbl2rubles, lbl5rubles, lbl10rubles };
            for (int i = 0; i < CoinCh.Count; i++)
            {
                var coindem = CoinLbl[i].Content;
                var u = database.Coins.Single(a => a.Denomination.ToString() == coindem.ToString());
                var id = u.Id;

                var q = database.VendingMachineCoins.Single(d => d.CoinsId == id);
                q.Count = Convert.ToInt32(CoinTxt[i].Text);
                if (CoinCh[i].IsChecked == true)
                {
                    q.IsActive = 1;
                }
                else
                {
                    q.IsActive = 0;
                }
                database.SaveChanges();
            }
        }
    }
}
