using BinanceDotNet.clients;
using BinanceDotNet.models;
using BinanceDotNet.models.converters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace BinanceDotNetExamples {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        private BinanceClient _api;
        private BinanceSocketClient _socketApi;
        

        public MainWindow() {
            InitializeComponent();

            _api = new BinanceClient();
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
    }
}
