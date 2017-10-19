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

        private const string _realPair = "ASTETH";

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
            List<Order> orders = await Api.GetAllOrders(_realPair);
            responseBox.Text = JsonConvert.SerializeObject(orders, Formatting.Indented);
            UpdateUi(orders);
        }

        private async void getOpenOrders(object sender, RoutedEventArgs e) {
            List<Order> orders = await Api.GetOpenOrders(GetPair());
            responseBox.Text = JsonConvert.SerializeObject(orders, Formatting.Indented); ;
            UpdateUi(orders);
        }

        private async void GetOrder(object sender, RoutedEventArgs e) {
            try {
                Order order = await Api.GetOrder(_realPair, null, GetOrderId());
                responseBox.Text = JsonConvert.SerializeObject(order, Formatting.Indented);
                UpdateUi(new List<Order>() { order });

            } catch (BinanceFailedRequest ex) {
                responseBox.Text = ex.ToString();
            }
        }

        private async void CancelOrder(object sender, RoutedEventArgs e) {
            try {
                CancelledOrder order = await Api.CancelOrder(_realPair, null, GetOrderId());
                responseBox.Text = JsonConvert.SerializeObject(order, Formatting.Indented);
                UpdateUi(new List<CancelledOrder>() { order });

            } catch (BinanceFailedRequest ex) {
                responseBox.Text = ex.ToString();
            }
        }

        private async void PlaceOrder(object sender, RoutedEventArgs e) {
            var order = await Api.PlaceNewOrder(_realPair, OrderSide.Buy, OrderType.Limit, TimeInForce.GoodTillCancelled, 100, 0.0001m);

            responseBox.Text = JsonConvert.SerializeObject(order, Formatting.Indented);
            orderIdTb.Text = order.OrderId.ToString();

            UpdateUi(new List<Order>() { order });
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
            if (resp != null)
                gbox.Header = $"{resp.Method} {resp.URL}";
        }

        private long GetOrderId() {
            if (long.TryParse(orderIdTb.Text, out long orderId)) {
                return orderId;
            }

            return -1;

        }
    }
}
