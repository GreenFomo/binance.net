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
        public BinanceSocketClient Api { get; set; }
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

        private async void startDepth(object sender, RoutedEventArgs e) {
            outDg.ItemsSource = depthDataSource;

            Api.StartDepth(GetPair(), (wsDepth) => {
                depthDataSource.Add(wsDepth);
                outDg.Items.Refresh();
            });
            ToggleStartButtons(false);
        }

        private async void startKline(object sender, RoutedEventArgs e) {
            outDg.ItemsSource = klineDataSource;

            Api.StartKline(GetPair(), "kline_1m", (wsKline) => {
                klineDataSource.Add(wsKline);
                outDg.Items.Refresh();
            });

            ToggleStartButtons(false);
        }

        private async void startTrades(object sender, RoutedEventArgs e) {
            outDg.ItemsSource = tradesDataSource;

            Api.StartTrades(GetPair(), (wsTrade) => {
                tradesDataSource.Add(wsTrade);
                outDg.Items.Refresh();
            });
            ToggleStartButtons(false);
        }

        private async void stopWs(object sender, RoutedEventArgs e) {
            Api.Stop();
            ToggleStartButtons(true);
        }

        private void ToggleStartButtons(bool value) {
            startDepthBtn.IsEnabled = value;
            startKlineBtn.IsEnabled = value;
            startTradesBtn.IsEnabled = value;

            stopBtn.IsEnabled = !value;
        }

    }
}
