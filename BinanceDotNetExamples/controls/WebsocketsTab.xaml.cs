using BinanceDotNet.clients;
using BinanceDotNet.models;
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
    /// Interaction logic for WebsocketsTab.xaml
    /// </summary>
    public partial class WebsocketsTab : UserControl {
        public BinanceUserStream UserStreamApi { get; set; }
        public BinanceSocketClient SocketApi { get; set; }
        public bool SocketRunning { get; set; }
        public Func<string> GetPair;

        private List<Depth> depthDataSource;
        private List<WsKline> klineDataSource;
        private List<WsTrade> tradesDataSource;

        public WebsocketsTab() {
            InitializeComponent();
            depthDataSource = new List<Depth>();
            klineDataSource = new List<WsKline>();
            tradesDataSource = new List<WsTrade>();
        }

        private void StartDepth(object sender, RoutedEventArgs e) {
            outDg.ItemsSource = depthDataSource;

            SocketApi.StartDepth(GetPair(), (wsDepth) => {
                depthDataSource.Add(wsDepth);
                outDg.Items.Refresh();
            });

            gbox.Header = SocketApi.LastUrl;
            ToggleStartButtons(false);
        }

        private void StartKline(object sender, RoutedEventArgs e) {
            outDg.ItemsSource = klineDataSource;

            SocketApi.StartKline(GetPair(), "kline_1m", (wsKline) => {
                klineDataSource.Add(wsKline);
                outDg.Items.Refresh();
            });

            gbox.Header = SocketApi.LastUrl;

            ToggleStartButtons(false);
        }

        private void StartTrades(object sender, RoutedEventArgs e) {
            outDg.ItemsSource = tradesDataSource;

            SocketApi.StartTrades(GetPair(), (wsTrade) => {
                tradesDataSource.Add(wsTrade);
                outDg.Items.Refresh();
            });

            gbox.Header = SocketApi.LastUrl;
            ToggleStartButtons(false);
        }

        private void StopWs(object sender, RoutedEventArgs e) {
            SocketApi.Stop();
            UserStreamApi.Stop();
            ToggleStartButtons(true);
        }

        private void ToggleStartButtons(bool value) {
            startDepthBtn.IsEnabled = value;
            startKlineBtn.IsEnabled = value;
            startTradesBtn.IsEnabled = value;
            startUserStreamBtn.IsEnabled = value;

            stopBtn.IsEnabled = !value;
        }

        private async void StartUserStream(object sender, RoutedEventArgs e) {
            ToggleStartButtons(false);

            await UserStreamApi.Start((line) => {
                Console.WriteLine("In ws: " + line);
            });

            gbox.Header = SocketApi.LastUrl;
        }

    }
}
