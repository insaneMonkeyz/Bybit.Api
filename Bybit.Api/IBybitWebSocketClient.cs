using Bybit.Api.Account;
using Bybit.Api.Market;
using Bybit.Api.Models.Socket;
using Bybit.Api.Trading;

namespace Bybit.Api;
public interface IBybitWebSocketClient
{
    void SetApiCredentials(ApiCredentials credentials);
    Task<CallResult<WebSocketUpdateSubscription>> SubscribeToExecutionUpdatesAsync(Action<WebSocketDataEvent<BybitExecutionUpdate>> handler, CancellationToken ct = default);
    Task<CallResult<WebSocketUpdateSubscription>> SubscribeToGreekUpdatesAsync(Action<WebSocketDataEvent<BybitGreekUpdate>> handler, CancellationToken ct = default);
    Task<CallResult<WebSocketUpdateSubscription>> SubscribeToInverseTickersAsync(IEnumerable<string> symbols, Action<WebSocketDataEvent<BybitFuturesTickerStream>> handler, CancellationToken ct = default);
    Task<CallResult<WebSocketUpdateSubscription>> SubscribeToInverseTickersAsync(string symbol, Action<WebSocketDataEvent<BybitFuturesTickerStream>> handler, CancellationToken ct = default);
    Task<CallResult<WebSocketUpdateSubscription>> SubscribeToKlinesAsync(BybitCategory category, string symbol, BybitInterval interval, Action<WebSocketDataEvent<BybitKlineUpdate>> handler, CancellationToken ct = default);
    Task<CallResult<WebSocketUpdateSubscription>> SubscribeToLeverageTokenKlinesAsync(string symbol, BybitInterval interval, Action<WebSocketDataEvent<BybitKlineUpdate>> handler, CancellationToken ct = default);
    Task<CallResult<WebSocketUpdateSubscription>> SubscribeToLeverageTokenNetAssetValuesAsync(string symbol, Action<WebSocketDataEvent<BybitNetAssetValueUpdate>> handler, CancellationToken ct = default);
    Task<CallResult<WebSocketUpdateSubscription>> SubscribeToLeverageTokenTickersAsync(string symbol, Action<WebSocketDataEvent<BybitLeverageTokenTickerStream>> handler, CancellationToken ct = default);
    Task<CallResult<WebSocketUpdateSubscription>> SubscribeToLinearTickersAsync(IEnumerable<string> symbols, Action<WebSocketDataEvent<BybitFuturesTickerStream>> handler, CancellationToken ct = default);
    Task<CallResult<WebSocketUpdateSubscription>> SubscribeToLinearTickersAsync(string symbol, Action<WebSocketDataEvent<BybitFuturesTickerStream>> handler, CancellationToken ct = default);
    Task<CallResult<WebSocketUpdateSubscription>> SubscribeToLiquidationsAsync(BybitCategory category, string symbol, Action<WebSocketDataEvent<BybitLiquidationUpdate>> handler, CancellationToken ct = default);
    Task<CallResult<WebSocketUpdateSubscription>> SubscribeToOptionTickersAsync(IEnumerable<string> symbols, Action<WebSocketDataEvent<BybitOptionTickerStream>> handler, CancellationToken ct = default);
    Task<CallResult<WebSocketUpdateSubscription>> SubscribeToOptionTickersAsync(string symbol, Action<WebSocketDataEvent<BybitOptionTickerStream>> handler, CancellationToken ct = default);
    Task<CallResult<WebSocketUpdateSubscription>> SubscribeToOrderBookAsync(BybitCategory category, string symbol, int depth, Action<WebSocketDataEvent<BybitOrderBookUpdate>> handler, CancellationToken ct = default);
    Task<CallResult<WebSocketUpdateSubscription>> SubscribeToOrderUpdatesAsync(Action<WebSocketDataEvent<BybitOrderUpdate>> handler, CancellationToken ct = default);
    Task<CallResult<WebSocketUpdateSubscription>> SubscribeToPositionUpdatesAsync(Action<WebSocketDataEvent<BybitPositionUpdate>> handler, CancellationToken ct = default);
    Task<CallResult<WebSocketUpdateSubscription>> SubscribeToSpotTickersAsync(IEnumerable<string> symbols, Action<WebSocketDataEvent<BybitSpotTickerStream>> handler, CancellationToken ct = default);
    Task<CallResult<WebSocketUpdateSubscription>> SubscribeToSpotTickersAsync(string symbol, Action<WebSocketDataEvent<BybitSpotTickerStream>> handler, CancellationToken ct = default);
    Task<CallResult<WebSocketUpdateSubscription>> SubscribeToTradesAsync(BybitCategory category, string symbol, Action<WebSocketDataEvent<BybitTradeUpdate>> handler, CancellationToken ct = default);
    Task<CallResult<WebSocketUpdateSubscription>> SubscribeToWalletUpdatesAsync(Action<WebSocketDataEvent<BybitAccountBalance>> handler, CancellationToken ct = default);
}