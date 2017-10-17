using BinanceDotNet.clients;
using BinanceDotNet.exceptions;
using BinanceDotNet.models;
using BinanceDotNet.models.enums;
using Newtonsoft.Json;
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

namespace BinanceDotNetExamples.controls {
    /// <summary>
    /// Interaction logic for SignedTab.xaml
    /// </summary>
    public partial class SignedTab : UserControl {
        public BinanceClient Api { get; set; }
        public Func<string> GetPair;

        public SignedTab() {
            InitializeComponent();
        }

        private async void getAccountInfo(object sender, RoutedEventArgs e) {
            AccountInfo accInfo = await Api.GetAccountInfo();
            responseBox.Text = JsonConvert.SerializeObject(accInfo, Formatting.Indented);

            UpdateUi(accInfo.Balances);
        }

        private async void getMyTrades(object sender, RoutedEventArgs e) {
            List<MyTrade> trades = await Api.GetMyTrades(GetPair());

            responseBox.Text = JsonConvert.SerializeObject(trades, Formatting.Indented);

            UpdateUi(trades);
        }

        private async void getAllOrders(object sender, RoutedEventArgs e) {
            List<Order> orders = await Api.GetAllOrders(GetPair());
            responseBox.Text = $"Orders retrieved: {orders.Count}";
            UpdateUi(orders);
        }

        private async void getOpenOrders(object sender, RoutedEventArgs e) {
            List<Order> orders = await Api.GetOpenOrders(GetPair());
            responseBox.Text = $"Orders retrieved: {orders.Count}";
            UpdateUi(orders);
        }

        private async void getOrder(object sender, RoutedEventArgs e) {
            try {
                Order order = await Api.GetOrder(GetPair(), "1234");
                Console.WriteLine(JsonConvert.SerializeObject(order, Formatting.Indented));
            } catch (BinanceFailedRequest ex) {
                responseBox.Text = ex.ToString();
            }

            UpdateUi<List>(null);
        }

        private async void cancelOrder(object sender, RoutedEventArgs e) {
            try {
                CancelledOrder order = await Api.CancelOrder(GetPair(), "1234");
                Console.WriteLine(JsonConvert.SerializeObject(order, Formatting.Indented));
            } catch (BinanceFailedRequest ex) {
                responseBox.Text = ex.ToString();
            }

            UpdateUi<List>(null);
        }

        private async void testNewOrder(object sender, RoutedEventArgs e) {
            var resp = await Api.TestNewOrder(GetPair(), OrderSide.Buy, OrderType.Limit, TimeInForce.GoodTillCancelled, 100, 0.1m);
            responseBox.Text = resp.Content;
            UpdateUi<List>(null);
        }

        private void UpdateUi<T>(IEnumerable<T> datasource) {
            outDg.ItemsSource = datasource;
            outDg.Items.Refresh();

            var resp = Api.LastResponse;
            gbox.Header = $"{resp.Method} {resp.URL}";
        }
    }
}
