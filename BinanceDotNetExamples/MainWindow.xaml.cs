using BinanceDotNet.clients;
using BinanceDotNet.models;
using BinanceDotNet.models.converters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace BinanceDotNetExamples {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        private BinanceClient _api;
        private BinanceSocketClient _socketApi;
        private ClientConfig _clientCfg;


        public MainWindow() {
            InitializeComponent();
            var path = "./secrets.json";

            if (File.Exists(path)) {
                using (StreamReader r = new StreamReader(path)) {
                    string json = r.ReadToEnd();
                    Console.WriteLine(json);
                    var dict = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
                    _clientCfg = new ClientConfig(dict["ApiKey"], dict["ApiSecret"]);
                    apiPanel.Visibility = Visibility.Collapsed;
                    apiInfo.Visibility = Visibility.Visible;
                }
            }

            _api = new BinanceClient(_clientCfg);
            _socketApi = new BinanceSocketClient();


            PublicTabControl.Api = _api;
            SignedTabControl.Api = _api;
            WebsocketsTabControl.Api = _socketApi;

            PublicTabControl.GetPair = GetSelectedPair;
            SignedTabControl.GetPair = GetSelectedPair;
            WebsocketsTabControl.GetPair = GetSelectedPair;



        }

        private string GetSelectedPair() {
            return ((ComboBoxItem)pairSelector.SelectedValue).Content.ToString().Replace("/", "");
        }

        private async void setApi(object sender, RoutedEventArgs e) {
            _api.SetApiDetails(apiKeyBox.Text, apiSecretBox.Text);
        }
    }
}
