﻿
namespace Bybit.Api.Trading;

public interface IBybitTradeRestApiClient
{
    Task<BybitRestCallResult<BybitTradingOrderId>> AmendOrderAsync(BybitCategory category, string symbol, string orderId = null, string clientOrderId = null, decimal? orderIv = null, decimal? quantity = null, decimal? price = null, decimal? triggerPrice = null, BybitTriggerPrice? triggerBy = null, BybitTakeProfitStopLossMode? takeProfitStopLossMode = null, BybitTriggerPrice? takeProfitTriggerBy = null, decimal? takeProfitPrice = null, decimal? takeProfitLimitPrice = null, BybitTriggerPrice? stopLossTriggerBy = null, decimal? stopLossPrice = null, decimal? stopLossLimitPrice = null, CancellationToken ct = default);
    Task<BybitRestCallResult<BybitRestApiResponse<List<BybitTradingBatchAmendOrder>, List<BybitTradingBatchError>>>> AmendOrdersAsync(BybitCategory category, IEnumerable<BybitBatchAmendOrderRequest> requests, CancellationToken ct = default);
    Task<BybitRestCallResult<BybitTradingOrderId>> CancelOrderAsync(BybitCategory category, string symbol, string orderId = null, string clientOrderId = null, BybitOrderFilter? orderFilter = null, CancellationToken ct = default);
    Task<BybitRestCallResult<List<BybitTradingOrderId>>> CancelOrdersAsync(BybitCategory category, string symbol = null, string baseAsset = null, string settleAsset = null, BybitOrderFilter? orderFilter = null, BybitStopOrderType? stopOrderType = null, CancellationToken ct = default);
    Task<BybitRestCallResult<BybitRestApiResponse<List<BybitTradingBatchCancelOrder>, List<BybitTradingBatchError>>>> CancelOrdersAsync(BybitCategory category, IEnumerable<BybitBatchCancelOrderRequest> requests, CancellationToken ct = default);
    Task<BybitRestCallResult<BybitTradingBorrowQuota>> GetBorrowQuotaAsync(BybitCategory category, string symbol, BybitOrderSide side, CancellationToken ct = default);
    Task<BybitRestCallResult<List<BybitTradingOrder>>> GetOrderHistoryAsync(BybitCategory category, string symbol = null, string baseAsset = null, string settleAsset = null, string orderId = null, string clientOrderId = null, BybitOrderFilter? orderFilter = null, BybitOrderStatus? orderStatus = null, long? startTime = null, long? endTime = null, int? limit = null, string cursor = null, CancellationToken ct = default);
    Task<BybitRestCallResult<List<BybitTradingOrder>>> GetOrdersAsync(BybitCategory category, string symbol = null, string baseAsset = null, string settleAsset = null, string orderId = null, string clientOrderId = null, BybitQueryOpenOnly? openOnly = null, BybitOrderFilter? orderFilter = null, int? limit = null, string cursor = null, CancellationToken ct = default);
    Task<BybitRestCallResult<List<BybitTradingExecution>>> GetTradeHistoryAsync(BybitCategory category, string symbol = null, string orderId = null, string clientOrderId = null, string baseAsset = null, long? startTime = null, long? endTime = null, BybitExecutionType? executionType = null, int? limit = null, string cursor = null, CancellationToken ct = default);
    Task<BybitRestCallResult<BybitTradingOrderId>> PlaceOrderAsync(BybitCategory category, string symbol, BybitOrderSide side, BybitOrderType type, decimal quantity, BybitUnit? marketUnit = null, decimal? price = null, string clientOrderId = null, bool? isLeverage = null, BybitTriggerDirection? triggerDirection = null, BybitOrderFilter? orderFilter = null, decimal? triggerPrice = null, BybitTriggerPrice? triggerBy = null, decimal? orderIv = null, BybitTimeInForce? timeInForce = null, BybitPositionIndex? positionIndex = null, bool? reduceOnly = null, bool? closeOnTrigger = null, bool? mmp = null, BybitSelfMatchPrevention? smp = null, BybitTakeProfitStopLossMode? takeProfitStopLossMode = null, BybitOrderType? takeProfitOrderType = null, BybitTriggerPrice? takeProfitTriggerBy = null, decimal? takeProfitPrice = null, decimal? takeProfitLimitPrice = null, BybitOrderType? stopLossOrderType = null, BybitTriggerPrice? stopLossTriggerBy = null, decimal? stopLossPrice = null, decimal? stopLossLimitPrice = null, CancellationToken ct = default);
    Task<BybitRestCallResult<BybitRestApiResponse<List<BybitTradingBatchPlaceOrder>, List<BybitTradingBatchError>>>> PlaceOrdersAsync(BybitCategory category, IEnumerable<BybitBatchPlaceOrderRequest> requests, CancellationToken ct = default);
    Task<BybitRestCallResult<object>> SetDisconnectProtectionAsync(int timeWindow, CancellationToken ct = default);
}