using BinanceDotNet.clients;
using BinanceDotNet.models;
using BinanceDotNet.models.converters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace BinanceDotNetExamples.controls {
    /// <summary>
    /// Interaction logic for PublicTab.xaml
    /// </summary>
    public partial class PublicTab : UserControl {
        public BinanceClient Api;
        public Func<string> GetPair;

        public PublicTab() {
            InitializeComponent();
        }

        //
        private async void genericHandler(object sender, RoutedEventArgs e) {
            Button btn = (Button)sender;

            outDg.ItemsSource = null;

            Console.WriteLine(btn.Content);

            switch (btn.Content) {
                case "Ping":
                    responseBox.Text = (await Api.Ping()).Content;
                    break;
                case "Time":
                    responseBox.Text = (await Api.GetTime()).Content;
                    break;
                default:
                    break;
            }

            var x = GetPair();
            Console.WriteLine(x);
        }

        private async void getDepth(object sender, RoutedEventArgs e) {
            Depth depth = await Api.GetDepth(GetPair(), 5);

            List<PriceQty> combined = new List<PriceQty>();
            combined.AddRange(depth.Asks);

            combined.Add(new PriceQty(0, 0));

            combined.AddRange(depth.Bids);

            UpdateUi(combined);

            responseBox.Text = JsonConvert.SerializeObject(depth, Formatting.Indented);
        }

        private async void getAggTrades(object sender, RoutedEventArgs e) {
            List<AggregateTrade> trades = await Api.GetAggTrades(GetPair());

            JsonSerializerSettings settings = new JsonSerializerSettings {
                Formatting = Formatting.Indented,
                ContractResolver = new OriginalPropertyContractResolver()
            };

            UpdateUi(trades);

            responseBox.Text = JsonConvert.SerializeObject(trades, settings);
        }

        private async void getCandlesticks(object sender, RoutedEventArgs e) {
            List<Candlestick> candles = await Api.GetCandlesticks(GetPair());

            UpdateUi(candles);

            responseBox.Text = JsonConvert.SerializeObject(candles, Formatting.Indented);
        }

        private async void getTicker24h(object sender, RoutedEventArgs e) {
            Ticker24hr ticker = await Api.GetTicker24h(GetPair());

            UpdateUi(new List<Ticker24hr>() { ticker });

            responseBox.Text = JsonConvert.SerializeObject(ticker, Formatting.Indented);
        }

        private async void getAllTickers(object sender, RoutedEventArgs e) {
            List<Ticker> tickers = await Api.GetAllTickers();

            UpdateUi(tickers);

            responseBox.Text = JsonConvert.SerializeObject(tickers, Formatting.Indented);
        }

        private async void getBookTickers(object sender, RoutedEventArgs e) {
            List<BookTicker> tickers = await Api.GetBookTickers();

            UpdateUi(tickers);

            responseBox.Text = JsonConvert.SerializeObject(tickers, Formatting.Indented);
        }

        private void UpdateUi<T>(IEnumerable<T> datasource) {
            outDg.ItemsSource = datasource;
            outDg.Items.Refresh();

            var resp = Api.LastResponse;
            gbox.Header = $"{resp.Method} {resp.URL}";
        }
    }
}
