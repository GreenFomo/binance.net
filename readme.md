# Binance.net
This intends to be a full-featured Binance API client (both HTTP and Websockets).

The repo also contains a WPF desktop app that demonstrates the usage of the Binance API clients.

## How to use
First of all, you'll have to restore the dependent NuGet package(s). Right now, there's only one dependency - Netwonsoft Json.Net.

To restore the packages, please follow the instructions at https://docs.microsoft.com/en-us/nuget/quickstart/restore

After, run the desktop app (F5 / Start) and play around.

## TODO
- [ ] Write docs on how to use the clients
- [ ] Add enums for each API enum.
- [ ] Add missing optional params from requests
- [ ] Validate request params (encapsulate validator logic)
- [ ] 