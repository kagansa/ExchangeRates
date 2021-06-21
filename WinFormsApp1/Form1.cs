using Business;
using System;
using System.Windows.Forms;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private ExchangeRateManager openExchangeRates = new ExchangeRateManager();
        private static string Base = "USD";

        private void btnExchange_Click(object sender, EventArgs e)
        {
            Exchange();
        }

        private void Exchange()
        {
            string from = txtFrom.Text;
            string to = txtTo.Text;
            decimal value = Convert.ToDecimal(txtValue.Text);

            if (txtFrom.Text != "USD")
            {
                MessageBox.Show($"API izini sadece {Base} olduğu için çevirilecek kur {Base}'ye dönüştürüldü");
                from = Base;
            }

            txtResult.Text = openExchangeRates.Get(from, to, value).ToString();
        }
    }
}