# Binance.net
This intends to be a full-featured Binance API client (both HTTP and Websockets).

The repo also contains a WPF desktop app that demonstrates the usage of the Binance API clients.

# TODO
- [ ] Write docs on how to use the clients (partially done)
- [X] Test real orders.
- [ ] Caching
- [X] Add enums for each API enum.
- [X] Add missing optional params from requests
- [X] Validate request params (encapsulate validator logic)


# How to use
First of all, you'll have to restore the dependent NuGet package(s). Right now, there's only one dependency - Netwonsoft Json.Net.

To restore the packages, please follow the instructions at https://docs.microsoft.com/en-us/nuget/quickstart/restore

After, run the desktop app (F5 / Start) and play around.

# Client documentation
## HTTP client
All calls below assume a `BinanceClient` instance, e.g.:

```csharp
BinanceClient httpClient = new BinanceClient(
	new ClientConfig("<API KEY>", "<API SECRET>")
);
//OR
BinanceClient httpClient = new BinanceClient();
//...later
httpClient.SetApiDetails("<API KEY>", "<API SECRET>");
```

## General endpoints
#### `GET /api/v1/ping`
```csharp
RawResponse response = await httpClient.Ping();
response.Content;
```

#### `GET /api/v1/time`
```csharp
RawResponse response = await httpClient.GetTime();
response.Content;
``` 

### Market data endpoints

#### `GET /api/v1/depth`
```csharp
Depth depthResponse = await httpClient.GetDepth("ETHBTC", 5);
List<Ask> asks = depthResponse.Asks;
List<Bid> bids = depthResponse.Bids;
``` 

#### `GET /api/v1/aggTrades`
```csharp
//See BinanceClient#GetAggTrades for the order of the optional parameters
List<AggregateTrade> trades = await httpClient.GetAggTrades("ETHBTC");

AggregateTrade trade = trades[0];
decimal price = trade.Price;
decimal qty = trade.Qty;
long tradeId = trade.AggregateTradeId;
//etc.
``` 

#### `GET /api/v1/klines`
```csharp
//See BinanceClient#GetCandelsticks for the order of the optional parameters

List<Candlestick> candles = await httpClient.GetCandlesticks("ETHBTC", KlineInterval.Minutes15);
Candlestick candle = candles[0];

long time = candle.OpenTime;
decimal open = candle.Open;
decimal high = candle.High;
//etc.
``` 

#### `GET /api/v1/ticker/24hr `
```csharp
Ticker24hr ticker = await httpClient.GetTicker24h("ETHBTC");

decimal priceChange = ticker.PriceChange;
decimal priceChangePercent = ticker.PriceChangePercent;
decimal wavgPrice = ticker.WeightedAvgPrice;
//etc
```

#### `GET /api/v1/ticker/allPrices`
```csharp
List<Ticker> tickers = await httpClient.GetAllTickers();

Ticker ticker = tickers[0];
string symbol = ticker.Symbol;
decimal price = ticker.Price;
```

#### `GET /api/v1/ticker/allBookTickers`
```csharp

List<BookTicker> tickers = await httpClient.GetBookTickers();

BookTicker ticker = tickers[0];
string symbol = ticker.Symbol;
decimal bidPrice = ticker.BidPrice;
decimal bidQty = ticker.BidQty;
//etc
```

### Account endpoints
#### `POST /api/v3/order/test`
```csharp
//See BinanceClient#TestNewOrder for the order of the optional parameters
RawResponse response = await httpClient.TestNewOrder(
  "ETHBTC", OrderSide.Buy, OrderType.Limit, 
  TimeInForce.GoodTillCancelled, 100, 0.01
);
```

#### `POST /api/v3/order/test`
```csharp
Order order = await Api.PlaceNewOrder(
  "ASTETH", OrderSide.Buy, OrderType.Limit, 
  TimeInForce.GoodTillCancelled, 100, 0.0001m
);

long orderId = order.OrderId;
//Use orderId with further GET/DELETE requests, as indicated below.
```

#### `GET /api/v3/order`
```csharp
//See BinanceClient#GetOrder for the order of the optional parameters
Order order  = await httpClient.GetOrder("ETHBTC", "1234");

string symbol = order.Symbol;
OrderStatus status = order.Status;
```

#### `DELETE /api/v3/order`
```csharp
CancelledOrder order = await httpClient.CancelOrder("ETHBTC", "1234");

string clientOrderId = order.ClientOrderId;
```

#### `GET /api/v3/openOrders`
```csharp
List<Order> orders = await httpClient.GetOpenOrders("ETHBTC");

Order order = orders[0];
```
See above at `GET /api/v3/order` for usage of `Order`.

#### `GET /api/v3/allOrders`
```csharp
List<Order> orders = await httpClient.GetAllOrders("ETHBTC");

Order order = orders[0];
```
See above at `GET /api/v3/order` for usage of `Order`.

#### `GET /api/v3/account`
```csharp
AccountInfo accInfo = await httpClient.GetAccountInfo();

float makerComm = accInfo.MakerCommission;
float takerComm = accInfo.TakerCommission;
//etc
```

#### `GET /api/v3/myTrades`
```csharp
//See BinanceClient#GetMyTrades for the order of the optional parameters
List<MyTrade> trades = await httpClient.GetMyTrades("ETHBTC");

MyTrade trade = trades[0];
long id = trade.Id;
decimal price = trade.Price; 
decimal qty = trade.Qty;
//etc
```

### User stream endpoints
The requests described below can be used manually to interact with user stream-related endpoints.

However, it is easier to use the `BinanceUserStream` class, which encapsulates the logic for starting, keeping alive and stopping a user data stream.

```csharp
BinanceSocketClient socketApi = new BinanceSocketClient();
BinanceClient httpApi = new BinanceClient(
	new ClientConfig("<API KEY>", "<API SECRET>")
);

BinanceUserStream streamApi = new BinanceUserStream() {
	HttpApi = httpApi,
	SocketApi = socketApi
};
await streamApi.Start((line) => {
	//line is the raw string containing the response from the User stream websocket
});

await streamApi.Stop();
```

This takes care of keeping alive the connection as well, by regularly pinging the appropriate endpoint.
When stopping, the `listenKey` will also be deleted via the `delete` request.

#### `POST /api/v1/userDataStream`
```csharp
UserDataEndpoint endpoint = await httpClient.StartUserStream();

string listenKey = endpoint.ListenKey;
```

#### `PUT /api/v1/userDataStream`
```csharp
RawResponse response = await httpClient.PingUserStream("<Listen Key>");
```

#### `DELETE /api/v1/userDataStream`
```csharp
RawResponse response = await httpClient.DeleteUserStream("<Listen Key>");
```