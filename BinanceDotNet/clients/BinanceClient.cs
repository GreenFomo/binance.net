using BinanceDotNet.exceptions;
using BinanceDotNet.models;
using BinanceDotNet.models.converters;
using BinanceDotNet.models.enums;
using BinanceDotNet.models.requests;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BinanceDotNet.clients {
    public class BinanceClient {
        private BinanceConnecter _connecter;
        private RawResponse lastResponse;
        public RawResponse LastResponse { get { return lastResponse; } }

        public BinanceClient() {
            _connecter = new BinanceConnecter();
        }

        public BinanceClient(ClientConfig _cfg) {
            _connecter = new BinanceConnecter(_cfg);
        }

        public void SetApiDetails(string _apiKey, string _apiSecret) {
            _connecter.SetApiDetails(_apiKey, _apiSecret);
        }

        public async Task<RawResponse> Ping() {
            lastResponse = await _connecter.PublicRequest(new PingRequest());
            return lastResponse;
        }

        public async Task<RawResponse> GetTime() {
            lastResponse = await _connecter.PublicRequest(new TimeRequest());
            return lastResponse;
        }

        public async Task<Depth> GetDepth(string pair, int limit = 100) {
            DepthRequest req = new DepthRequest() { Symbol = pair, Limit = limit };
            
            var resp = await _connecter.PublicRequest(req);
            lastResponse = resp;

            return JsonConvert.DeserializeObject<Depth>(resp.Content, new CustomDepthConverter());
        }

        //
        public async Task<List<AggregateTrade>> GetAggTrades(string pair, long fromId = 0, long startTime = 0, long endTime = 0, int limit = 500) {
            var req = new AggregateTradeRequest() { Symbol = pair };
            var resp = await _connecter.PublicRequest(req);
            lastResponse = resp;

            return JsonConvert.DeserializeObject<List<AggregateTrade>>(resp.Content);
        }

        //
        public async Task<List<Candlestick>> GetCandlesticks(string pair) {
            var interval = KlineInterval.Minutes15;
            var req = new CandlestickRequest() { Symbol = pair, Interval = interval};
            
            Console.WriteLine(interval);

            var resp = await _connecter.PublicRequest(req);
            lastResponse = resp;

            return JsonConvert.DeserializeObject<List<Candlestick>>(resp.Content, new CustomCandlestickConverter());
        }

        public async Task<Ticker24hr> GetTicker24h(string pair) {
            var req = new Ticker24hrRequest() { Symbol = pair };
            var resp = await _connecter.PublicRequest(req);

            lastResponse = resp;

            return JsonConvert.DeserializeObject<Ticker24hr>(resp.Content);
        }

        public async Task<List<Ticker>> GetAllTickers() {
            var req = new AllPricesRequest();
            var resp = await _connecter.PublicRequest(req);

            lastResponse = resp;

            return JsonConvert.DeserializeObject<List<Ticker>>(resp.Content);
        }

        public async Task<List<BookTicker>> GetBookTickers() {
            var req = new BookTickersRequest();
            var resp = await _connecter.PublicRequest(req);

            lastResponse = resp;

            return JsonConvert.DeserializeObject<List<BookTicker>>(resp.Content);

        }

        //
        public async Task<AccountInfo> GetAccountInfo() {
            var req = new AccountInfoRequest();
            var resp = await _connecter.PrivateRequest(req);

            lastResponse = resp;

            return JsonConvert.DeserializeObject<AccountInfo>(resp.Content);
        }

        public async Task<List<Order>> GetAllOrders(string symbol) {
            var req = new GetAllOrdersRequest() { Symbol = symbol };

            var resp = await _connecter.PrivateRequest(req);
            lastResponse = resp;

            return JsonConvert.DeserializeObject<List<Order>>(resp.Content);
        }

        public async Task<List<Order>> GetOpenOrders(string symbol) {
            var req = new GetOpenOrdersRequest() { Symbol = symbol };

            var resp = await _connecter.PrivateRequest(req);
            lastResponse = resp;

            return JsonConvert.DeserializeObject<List<Order>>(resp.Content);
        }

        public async Task<Order> GetOrder(string symbol, string origClientOrderId = null, long? orderId = null) {
            var req = new GetOrderRequest() { Symbol = symbol, OrigClientOrderId = origClientOrderId, OrderId = orderId };
            var resp = await _connecter.PrivateRequest(req);
            lastResponse = resp;

            return JsonConvert.DeserializeObject<Order>(resp.Content);
        }

        public async Task<CancelledOrder> CancelOrder(string symbol, string origClientOrderId = null, long orderId = -1) {
            var req = new CancelOrderRequest() { Symbol = symbol, OrigClientOrderId = origClientOrderId, OrderId = orderId };
            var resp = await _connecter.PrivateRequest(req);

            lastResponse = resp;

            return JsonConvert.DeserializeObject<CancelledOrder>(resp.Content);
        }

        public async Task<List<MyTrade>> GetMyTrades(string symbol) {
            var req = new GetMyTradesRequest() { Symbol = symbol };

            var resp = await _connecter.PrivateRequest(req);
            lastResponse = resp;

            return JsonConvert.DeserializeObject<List<MyTrade>>(resp.Content);
        }

        public async Task<RawResponse> TestNewOrder(string symbol, OrderSide side, OrderType type, TimeInForce timeInForce, decimal qty, decimal price) {
            var req = new PlaceOrderTestRequest() {
                Symbol = symbol,
                Side =side,
                Type = type,
                TimeInForce = timeInForce,
                Quantity = qty,
                Price = price
            };

            lastResponse = await _connecter.PrivateRequest(req);

            return lastResponse;
        }


    }
}
