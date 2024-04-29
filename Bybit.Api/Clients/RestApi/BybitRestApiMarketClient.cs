﻿using Bybit.Api.Models.Market;

namespace Bybit.Api.Clients.RestApi;

/// <summary>
/// Bybit Rest API Market Client
/// </summary>
public class BybitRestApiMarketClient
{
    // Market Endpoints
    private const string _v5PublicTimeEndpoint = "v5/public/time";
    private const string _v5MarketKlineEndpoint = "v5/market/kline";
    private const string _v5MarketMarkPriceKlineEndpoint = "v5/market/mark-price-kline";
    private const string _v5MarketIndexPriceKlineEndpoint = "v5/market/index-price-kline";
    private const string _v5MarketPremiumIndexPriceKlineEndpoint = "v5/market/premium-index-price-kline";
    private const string _v5InstrumentsInfoEndpoint = "v5/market/instruments-info";
    private const string _v5MarketOrderbookEndpoint = "v5/market/orderbook";
    private const string _v5MarketTickersEndpoint = "v5/market/tickers";
    private const string _v5MarketFundingHistoryEndpoint = "v5/market/funding/history";
    private const string _v5MarketRecentTradeEndpoint = "v5/market/recent-trade";
    private const string _v5MarketOpenInterestEndpoint = "v5/market/open-interest";
    private const string _v5MarketHistoricalVolatilityEndpoint = "v5/market/historical-volatility";
    private const string _v5MarketInsuranceEndpoint = "v5/market/insurance";
    private const string _v5MarketRiskLimitEndpoint = "v5/market/risk-limit";
    private const string _v5MarketDeliveryPriceEndpoint = "v5/market/delivery-price";
    private const string _v5MarketAccountRatioEndpoint = "/v5/market/account-ratio";

    #region Internal
    internal BybitRestApiBaseClient MainClient { get; }
    internal BybitRestApiMarketClient(BybitRestApiClient root)
    {
        this.MainClient = root.BaseClient;
    }
    #endregion

    /// <summary>
    /// Get Bybit Server Time
    /// </summary>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public async Task<BybitRestCallResult<BybitTime>> GetServerTimeAsync(CancellationToken ct = default)
        => await MainClient.SendBybitRequest<BybitTime>(MainClient.BuildUri(_v5PublicTimeEndpoint), HttpMethod.Get, ct).ConfigureAwait(false);

    /// <summary>
    /// Query for historical klines (also known as candles/candlesticks). Charts are returned in groups based on the requested interval.
    /// </summary>
    /// <param name="category">Product type. spot,linear,inverse. When category is not passed, use linear by default</param>
    /// <param name="symbol">Symbol name</param>
    /// <param name="interval">Kline interval. 1,3,5,15,30,60,120,240,360,720,D,M,W</param>
    /// <param name="start">The start timestamp (ms)</param>
    /// <param name="end">The end timestamp (ms)</param>
    /// <param name="limit">Limit for data size per page. [1, 1000]. Default: 200</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public async Task<BybitRestCallResult<List<BybitKline>>> GetKlinesAsync(BybitCategory category, string symbol, BybitKlineInterval interval, DateTime start, DateTime end, int limit = 200, CancellationToken ct = default)
        => await GetKlinesAsync(category, symbol, interval, start.ConvertToMilliseconds(), end.ConvertToMilliseconds(), limit, ct).ConfigureAwait(false);

    /// <summary>
    /// Query for historical klines (also known as candles/candlesticks). Charts are returned in groups based on the requested interval.
    /// </summary>
    /// <param name="category">Product type. spot,linear,inverse. When category is not passed, use linear by default</param>
    /// <param name="symbol">Symbol name</param>
    /// <param name="interval">Kline interval. 1,3,5,15,30,60,120,240,360,720,D,M,W</param>
    /// <param name="start">The start timestamp (ms)</param>
    /// <param name="end">The end timestamp (ms)</param>
    /// <param name="limit">Limit for data size per page. [1, 1000]. Default: 200</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    /// <exception cref="NotSupportedException"></exception>
    public async Task<BybitRestCallResult<List<BybitKline>>> GetKlinesAsync(BybitCategory category, string symbol, BybitKlineInterval interval, long? start = null, long? end = null, int limit = 200, CancellationToken ct = default)
    {
        if (category.IsNotIn(BybitCategory.Spot, BybitCategory.Linear, BybitCategory.Inverse))
            throw new NotSupportedException($"{category} is not supported for this endpoint.");

        limit.ValidateIntBetween(nameof(limit), 1, 1000);
        var parameters = new Dictionary<string, object>
        {
            { "category", category.GetLabel() },
            { "symbol", symbol },
            { "interval", interval.GetLabel() },
        };
        parameters.AddOptionalParameter("start", start);
        parameters.AddOptionalParameter("end", end);
        parameters.AddOptionalParameter("limit", limit);

        var result = await MainClient.SendBybitRequest<BybitListResponse<BybitKline>>(MainClient.BuildUri(_v5MarketKlineEndpoint), HttpMethod.Get, ct, queryParameters: parameters).ConfigureAwait(false);
        if (!result) return result.As<List<BybitKline>>(null);
        return result.As(result.Data.Payload);
    }

    /// <summary>
    /// Query for historical mark price klines. Charts are returned in groups based on the requested interval.
    /// </summary>
    /// <param name="category">Product type. spot,linear,inverse. When category is not passed, use linear by default</param>
    /// <param name="symbol">Symbol name</param>
    /// <param name="interval">Kline interval. 1,3,5,15,30,60,120,240,360,720,D,M,W</param>
    /// <param name="start">The start timestamp (ms)</param>
    /// <param name="end">The end timestamp (ms)</param>
    /// <param name="limit">Limit for data size per page. [1, 1000]. Default: 200</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public async Task<BybitRestCallResult<List<BybitMarkPriceKline>>> GetMarkKlinesAsync(BybitCategory category, string symbol, BybitKlineInterval interval, DateTime start, DateTime end, int limit = 200, CancellationToken ct = default)
        => await GetMarkKlinesAsync(category, symbol, interval, start.ConvertToMilliseconds(), end.ConvertToMilliseconds(), limit, ct).ConfigureAwait(false);

    /// <summary>
    /// Query for historical mark price klines. Charts are returned in groups based on the requested interval.
    /// </summary>
    /// <param name="category">Product type. spot,linear,inverse. When category is not passed, use linear by default</param>
    /// <param name="symbol">Symbol name</param>
    /// <param name="interval">Kline interval. 1,3,5,15,30,60,120,240,360,720,D,M,W</param>
    /// <param name="start">The start timestamp (ms)</param>
    /// <param name="end">The end timestamp (ms)</param>
    /// <param name="limit">Limit for data size per page. [1, 1000]. Default: 200</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    /// <exception cref="NotSupportedException"></exception>
    public async Task<BybitRestCallResult<List<BybitMarkPriceKline>>> GetMarkKlinesAsync(BybitCategory category, string symbol, BybitKlineInterval interval, long? start = null, long? end = null, int limit = 200, CancellationToken ct = default)
    {
        if (category.IsNotIn(BybitCategory.Linear, BybitCategory.Inverse))
            throw new NotSupportedException($"{category} is not supported for this endpoint.");

        limit.ValidateIntBetween(nameof(limit), 1, 1000);
        var parameters = new Dictionary<string, object>
        {
            { "category", category.GetLabel() },
            { "symbol", symbol },
            { "interval", interval.GetLabel() },
        };
        parameters.AddOptionalParameter("start", start);
        parameters.AddOptionalParameter("end", end);
        parameters.AddOptionalParameter("limit", limit);

        var result = await MainClient.SendBybitRequest<BybitListResponse<BybitMarkPriceKline>>(MainClient.BuildUri(_v5MarketMarkPriceKlineEndpoint), HttpMethod.Get, ct, queryParameters: parameters).ConfigureAwait(false);
        if (!result) return result.As<List<BybitMarkPriceKline>>(null);
        return result.As(result.Data.Payload);
    }

    /// <summary>
    /// Query for historical index price klines. Charts are returned in groups based on the requested interval.
    /// </summary>
    /// <param name="category">Product type. spot,linear,inverse. When category is not passed, use linear by default</param>
    /// <param name="symbol">Symbol name</param>
    /// <param name="interval">Kline interval. 1,3,5,15,30,60,120,240,360,720,D,M,W</param>
    /// <param name="start">The start timestamp (ms)</param>
    /// <param name="end">The end timestamp (ms)</param>
    /// <param name="limit">Limit for data size per page. [1, 1000]. Default: 200</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public async Task<BybitRestCallResult<List<BybitIndexPriceKline>>> GetIndexKlinesAsync(BybitCategory category, string symbol, BybitKlineInterval interval, DateTime start, DateTime end, int limit = 200, CancellationToken ct = default)
        => await GetIndexKlinesAsync(category, symbol, interval, start.ConvertToMilliseconds(), end.ConvertToMilliseconds(), limit, ct).ConfigureAwait(false);

    /// <summary>
    /// Query for historical index price klines. Charts are returned in groups based on the requested interval.
    /// </summary>
    /// <param name="category">Product type. spot,linear,inverse. When category is not passed, use linear by default</param>
    /// <param name="symbol">Symbol name</param>
    /// <param name="interval">Kline interval. 1,3,5,15,30,60,120,240,360,720,D,M,W</param>
    /// <param name="start">The start timestamp (ms)</param>
    /// <param name="end">The end timestamp (ms)</param>
    /// <param name="limit">Limit for data size per page. [1, 1000]. Default: 200</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    /// <exception cref="NotSupportedException"></exception>
    public async Task<BybitRestCallResult<List<BybitIndexPriceKline>>> GetIndexKlinesAsync(BybitCategory category, string symbol, BybitKlineInterval interval, long? start = null, long? end = null, int limit = 200, CancellationToken ct = default)
    {
        if (category.IsNotIn(BybitCategory.Linear, BybitCategory.Inverse))
            throw new NotSupportedException($"{category} is not supported for this endpoint.");

        limit.ValidateIntBetween(nameof(limit), 1, 1000);
        var parameters = new Dictionary<string, object>
        {
            { "category", category.GetLabel() },
            { "symbol", symbol },
            { "interval", interval.GetLabel() },
        };
        parameters.AddOptionalParameter("start", start);
        parameters.AddOptionalParameter("end", end);
        parameters.AddOptionalParameter("limit", limit);

        var result = await MainClient.SendBybitRequest<BybitListResponse<BybitIndexPriceKline>>(MainClient.BuildUri(_v5MarketIndexPriceKlineEndpoint), HttpMethod.Get, ct, queryParameters: parameters).ConfigureAwait(false);
        if (!result) return result.As<List<BybitIndexPriceKline>>(null);
        return result.As(result.Data.Payload);
    }

    /// <summary>
    /// Query for historical premium index klines. Charts are returned in groups based on the requested interval.
    /// </summary>
    /// <param name="category">Product type. linear</param>
    /// <param name="symbol">Symbol name</param>
    /// <param name="interval">Kline interval. 1,3,5,15,30,60,120,240,360,720,D,M,W</param>
    /// <param name="start">The start timestamp (ms)</param>
    /// <param name="end">The end timestamp (ms)</param>
    /// <param name="limit">Limit for data size per page. [1, 1000]. Default: 200</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public async Task<BybitRestCallResult<List<BybitPremiumIndexPriceKline>>> GetPremiumIndexKlinesAsync(BybitCategory category, string symbol, BybitKlineInterval interval, DateTime start, DateTime end, int limit = 200, CancellationToken ct = default)
        => await GetPremiumIndexKlinesAsync(category, symbol, interval, start.ConvertToMilliseconds(), end.ConvertToMilliseconds(), limit, ct).ConfigureAwait(false);

    /// <summary>
    /// Query for historical premium index klines. Charts are returned in groups based on the requested interval.
    /// </summary>
    /// <param name="category"></param>
    /// <param name="symbol"></param>
    /// <param name="interval"></param>
    /// <param name="start"></param>
    /// <param name="end"></param>
    /// <param name="limit"></param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    /// <exception cref="NotSupportedException"></exception>
    public async Task<BybitRestCallResult<List<BybitPremiumIndexPriceKline>>> GetPremiumIndexKlinesAsync(BybitCategory category, string symbol, BybitKlineInterval interval, long? start = null, long? end = null, int limit = 200, CancellationToken ct = default)
    {
        if (category.IsNotIn(BybitCategory.Linear))
            throw new NotSupportedException($"{category} is not supported for this endpoint.");

        limit.ValidateIntBetween(nameof(limit), 1, 1000);
        var parameters = new Dictionary<string, object>
        {
            { "category", category.GetLabel() },
            { "symbol", symbol },
            { "interval", interval.GetLabel() },
        };
        parameters.AddOptionalParameter("start", start);
        parameters.AddOptionalParameter("end", end);
        parameters.AddOptionalParameter("limit", limit);

        var result = await MainClient.SendBybitRequest<BybitListResponse<BybitPremiumIndexPriceKline>>(MainClient.BuildUri(_v5MarketPremiumIndexPriceKlineEndpoint), HttpMethod.Get, ct, queryParameters: parameters).ConfigureAwait(false);
        if (!result) return result.As<List<BybitPremiumIndexPriceKline>>(null);
        return result.As(result.Data.Payload);
    }

    /// <summary>
    /// Query for the instrument specification of online trading pairs.
    /// </summary>
    /// <param name="symbol">Symbol name</param>
    /// <param name="status">Symbol status filter. spot/linear/inverse has Trading only</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public async Task<BybitRestCallResult<List<BybitSpotInstrument>>> GetSpotInstrumentsAsync(string symbol = null, BybitInstrumentStatus? status = null, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>
        {
            { "category", BybitCategory.Spot.GetLabel() },
        };
        parameters.AddOptionalParameter("symbol", symbol);
        parameters.AddOptionalParameter("status", status?.GetLabel());

        var result = await MainClient.SendBybitRequest<BybitCategoryResponse<BybitSpotInstrument>>(MainClient.BuildUri(_v5InstrumentsInfoEndpoint), HttpMethod.Get, ct, queryParameters: parameters).ConfigureAwait(false);
        if (!result) return result.As<List<BybitSpotInstrument>>(null);
        return result.As(result.Data.Payload);
    }

    /// <summary>
    /// Query for the instrument specification of online trading pairs.
    /// </summary>
    /// <param name="symbol">Symbol name</param>
    /// <param name="status">Symbol status filter. spot/linear/inverse has Trading only</param>
    /// <param name="baseAsset">Base coin. Apply tolinear,inverse,option only. option: it returns BTC by default</param>
    /// <param name="limit">Limit for data size per page. [1, 1000]. Default: 500</param>
    /// <param name="cursor">Cursor. Use the nextPageCursor token from the response to retrieve the next page of the result set</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public async Task<BybitRestCallResult<List<BybitLinearInverseInstrument>>> GetLinearInstrumentsAsync(string symbol = null, BybitInstrumentStatus? status = null, string baseAsset = null, int limit = 500, string cursor = null, CancellationToken ct = default)
    {
        limit.ValidateIntBetween(nameof(limit), 1, 1000);
        var parameters = new Dictionary<string, object>
        {
            { "category", BybitCategory.Linear.GetLabel() },
        };
        parameters.AddOptionalParameter("symbol", symbol);
        parameters.AddOptionalParameter("status", status?.GetLabel());
        parameters.AddOptionalParameter("baseCoin", baseAsset);
        parameters.AddOptionalParameter("limit", limit);
        parameters.AddOptionalParameter("cursor", cursor);

        var result = await MainClient.SendBybitRequest<BybitCursorResponse<BybitLinearInverseInstrument>>(MainClient.BuildUri(_v5InstrumentsInfoEndpoint), HttpMethod.Get, ct, queryParameters: parameters).ConfigureAwait(false);
        if (!result) return result.As<List<BybitLinearInverseInstrument>>(null);
        return result.AsWithCursor(result.Data.Payload, result.Data.NextPageCursor);
    }

    /// <summary>
    /// Query for the instrument specification of online trading pairs.
    /// </summary>
    /// <param name="symbol">Symbol name</param>
    /// <param name="status">Symbol status filter. spot/linear/inverse has Trading only</param>
    /// <param name="baseAsset">Base coin. Apply tolinear,inverse,option only. option: it returns BTC by default</param>
    /// <param name="limit">Limit for data size per page. [1, 1000]. Default: 500</param>
    /// <param name="cursor">Cursor. Use the nextPageCursor token from the response to retrieve the next page of the result set</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public async Task<BybitRestCallResult<List<BybitLinearInverseInstrument>>> GetInverseInstrumentsAsync(string symbol = null, BybitInstrumentStatus? status = null, string baseAsset = null, int limit = 500, string cursor = null, CancellationToken ct = default)
    {
        limit.ValidateIntBetween(nameof(limit), 1, 1000);
        var parameters = new Dictionary<string, object>
        {
            { "category", BybitCategory.Inverse.GetLabel() },
        };
        parameters.AddOptionalParameter("symbol", symbol);
        parameters.AddOptionalParameter("status", status?.GetLabel());
        parameters.AddOptionalParameter("baseCoin", baseAsset);
        parameters.AddOptionalParameter("limit", limit);
        parameters.AddOptionalParameter("cursor", cursor);

        var result = await MainClient.SendBybitRequest<BybitCursorResponse<BybitLinearInverseInstrument>>(MainClient.BuildUri(_v5InstrumentsInfoEndpoint), HttpMethod.Get, ct, queryParameters: parameters).ConfigureAwait(false);
        if (!result) return result.As<List<BybitLinearInverseInstrument>>(null);
        return result.AsWithCursor(result.Data.Payload, result.Data.NextPageCursor);
    }

    /// <summary>
    /// Query for the instrument specification of online trading pairs.
    /// </summary>
    /// <param name="symbol">Symbol name</param>
    /// <param name="status">Symbol status filter. spot/linear/inverse has Trading only</param>
    /// <param name="baseAsset">Base coin. Apply tolinear,inverse,option only. option: it returns BTC by default</param>
    /// <param name="limit">Limit for data size per page. [1, 1000]. Default: 500</param>
    /// <param name="cursor">Cursor. Use the nextPageCursor token from the response to retrieve the next page of the result set</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public async Task<BybitRestCallResult<List<BybitOptionInstrument>>> GetOptionInstrumentsAsync(string symbol = null, BybitInstrumentStatus? status = null, string baseAsset = null, int limit = 500, string cursor = null, CancellationToken ct = default)
    {
        limit.ValidateIntBetween(nameof(limit), 1, 1000);
        var parameters = new Dictionary<string, object>
        {
            { "category", BybitCategory.Option.GetLabel() },
        };
        parameters.AddOptionalParameter("symbol", symbol);
        parameters.AddOptionalParameter("status", status?.GetLabel());
        parameters.AddOptionalParameter("baseCoin", baseAsset);
        parameters.AddOptionalParameter("limit", limit);
        parameters.AddOptionalParameter("cursor", cursor);

        var result = await MainClient.SendBybitRequest<BybitCursorResponse<BybitOptionInstrument>>(MainClient.BuildUri(_v5InstrumentsInfoEndpoint), HttpMethod.Get, ct, queryParameters: parameters).ConfigureAwait(false);
        if (!result) return result.As<List<BybitOptionInstrument>>(null);
        return result.AsWithCursor(result.Data.Payload, result.Data.NextPageCursor);
    }

    /// <summary>
    /// Query for orderbook depth data.
    /// Covers: Spot / USDT perpetual / USDC contract / Inverse contract / Option
    /// Contract: 500-level of orderbook data
    /// Spot: 200-level of orderbook data
    /// Option: 25-level of orderbook data
    /// </summary>
    /// <param name="category">Product type. spot, linear, inverse, option</param>
    /// <param name="symbol">Symbol name</param>
    /// <param name="limit">Limit size for each bid and ask
    /// spot: [1, 200]. Default: 1.
    /// linear &amp; inverse: [1, 500]. Default: 25.
    /// option: [1, 25]. Default: 1.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public async Task<BybitRestCallResult<BybitOrderbook>> GetOrderbookAsync(BybitCategory category, string symbol, int? limit = null, CancellationToken ct = default)
    {
        if (category == BybitCategory.Spot) limit?.ValidateIntBetween(nameof(limit), 1, 200);
        if (category == BybitCategory.Linear) limit?.ValidateIntBetween(nameof(limit), 1, 500);
        if (category == BybitCategory.Inverse) limit?.ValidateIntBetween(nameof(limit), 1, 500);
        if (category == BybitCategory.Option) limit?.ValidateIntBetween(nameof(limit), 1, 25);

        var parameters = new Dictionary<string, object>
        {
            { "category", category.GetLabel() },
            { "symbol", symbol },
        };
        parameters.AddOptionalParameter("limit", limit);

        return await MainClient.SendBybitRequest<BybitOrderbook>(MainClient.BuildUri(_v5MarketOrderbookEndpoint), HttpMethod.Get, ct, queryParameters: parameters).ConfigureAwait(false);
    }

    /// <summary>
    /// Query for the latest price snapshot, best bid/ask price, and trading volume in the last 24 hours.
    /// Covers: Spot / USDT perpetual / USDC contract / Inverse contract / Option
    /// If category=option, symbol or baseCoin must be passed.
    /// </summary>
    /// <param name="symbol">Symbol name</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public async Task<BybitRestCallResult<List<BybitSpotTicker>>> GetSpotTickersAsync(string symbol = null, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>
        {
            { "category", BybitCategory.Spot.GetLabel() },
        };
        parameters.AddOptionalParameter("symbol", symbol);

        var result = await MainClient.SendBybitRequest<BybitCategoryResponse<BybitSpotTicker>>(MainClient.BuildUri(_v5MarketTickersEndpoint), HttpMethod.Get, ct, queryParameters: parameters).ConfigureAwait(false);
        if (!result) return result.As<List<BybitSpotTicker>>(null);
        return result.As(result.Data.Payload);
    }

    /// <summary>
    /// Query for the latest price snapshot, best bid/ask price, and trading volume in the last 24 hours.
    /// Covers: Spot / USDT perpetual / USDC contract / Inverse contract / Option
    /// If category=option, symbol or baseCoin must be passed.
    /// </summary>
    /// <param name="symbol">Symbol name</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public async Task<BybitRestCallResult<List<BybitLinearInverseTicker>>> GetLinearTickersAsync(string symbol = null, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>
        {
            { "category", BybitCategory.Linear.GetLabel() },
        };
        parameters.AddOptionalParameter("symbol", symbol);

        var result = await MainClient.SendBybitRequest<BybitCategoryResponse<BybitLinearInverseTicker>>(MainClient.BuildUri(_v5MarketTickersEndpoint), HttpMethod.Get, ct, queryParameters: parameters).ConfigureAwait(false);
        if (!result) return result.As<List<BybitLinearInverseTicker>>(null);
        return result.As(result.Data.Payload);
    }

    /// <summary>
    /// Query for the latest price snapshot, best bid/ask price, and trading volume in the last 24 hours.
    /// Covers: Spot / USDT perpetual / USDC contract / Inverse contract / Option
    /// If category=option, symbol or baseCoin must be passed.
    /// </summary>
    /// <param name="symbol">Symbol name</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public async Task<BybitRestCallResult<List<BybitLinearInverseTicker>>> GetInverseTickersAsync(string symbol = null, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>
        {
            { "category", BybitCategory.Inverse.GetLabel() },
        };
        parameters.AddOptionalParameter("symbol", symbol);

        var result = await MainClient.SendBybitRequest<BybitCategoryResponse<BybitLinearInverseTicker>>(MainClient.BuildUri(_v5MarketTickersEndpoint), HttpMethod.Get, ct, queryParameters: parameters).ConfigureAwait(false);
        if (!result) return result.As<List<BybitLinearInverseTicker>>(null);
        return result.As(result.Data.Payload);
    }

    /// <summary>
    /// Query for the latest price snapshot, best bid/ask price, and trading volume in the last 24 hours.
    /// Covers: Spot / USDT perpetual / USDC contract / Inverse contract / Option
    /// If category=option, symbol or baseCoin must be passed.
    /// </summary>
    /// <param name="symbol">Symbol name</param>
    /// <param name="baseAsset">Base coin. Apply to option only</param>
    /// <param name="expDate">Expiry date. e.g., 25DEC22. Apply to option only</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public async Task<BybitRestCallResult<List<BybitOptionTicker>>> GetOptionTickersAsync(string symbol = null, string baseAsset = null, string expDate = null, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>
        {
            { "category", BybitCategory.Option.GetLabel() },
        };
        parameters.AddOptionalParameter("symbol", symbol);
        parameters.AddOptionalParameter("baseCoin", baseAsset);
        parameters.AddOptionalParameter("expDate", expDate);

        var result = await MainClient.SendBybitRequest<BybitCategoryResponse<BybitOptionTicker>>(MainClient.BuildUri(_v5MarketTickersEndpoint), HttpMethod.Get, ct, queryParameters: parameters).ConfigureAwait(false);
        if (!result) return result.As<List<BybitOptionTicker>>(null);
        return result.As(result.Data.Payload);
    }

    /// <summary>
    /// Query for historical funding rates. Each symbol has a different funding interval. For example, if the interval is 8 hours and the current time is UTC 12, then it returns the last funding rate, which settled at UTC 8.
    /// To query the funding rate interval, please refer to the instruments-info endpoint.
    /// Covers: USDT and USDC perpetual / Inverse perpetual
    /// 
    /// INFO
    /// Passing only startTime returns an error.
    /// Passing only endTime returns 200 records up till endTime.
    /// Passing neither returns 200 records up till the current time.
    /// </summary>
    /// <param name="category">Product type. linear,inverse</param>
    /// <param name="symbol">Symbol name</param>
    /// <param name="startTime">The start timestamp (ms)</param>
    /// <param name="endTime">The end timestamp (ms)</param>
    /// <param name="limit">Limit for data size per page. [1, 200]. Default: 200</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public async Task<BybitRestCallResult<List<BybitFundingHistory>>> GetFundingHistoryAsync(BybitCategory category, string symbol, DateTime startTime, DateTime endTime, int limit = 200, CancellationToken ct = default)
        => await GetFundingHistoryAsync(category, symbol, startTime.ConvertToMilliseconds(), endTime.ConvertToMilliseconds(), limit, ct).ConfigureAwait(false);

    /// <summary>
    /// Query for historical funding rates. Each symbol has a different funding interval. For example, if the interval is 8 hours and the current time is UTC 12, then it returns the last funding rate, which settled at UTC 8.
    /// To query the funding rate interval, please refer to the instruments-info endpoint.
    /// Covers: USDT and USDC perpetual / Inverse perpetual
    /// 
    /// INFO
    /// Passing only startTime returns an error.
    /// Passing only endTime returns 200 records up till endTime.
    /// Passing neither returns 200 records up till the current time.
    /// </summary>
    /// <param name="category">Product type. linear,inverse</param>
    /// <param name="symbol">Symbol name</param>
    /// <param name="startTime">The start timestamp (ms)</param>
    /// <param name="endTime">The end timestamp (ms)</param>
    /// <param name="limit">Limit for data size per page. [1, 200]. Default: 200</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    /// <exception cref="NotSupportedException"></exception>
    public async Task<BybitRestCallResult<List<BybitFundingHistory>>> GetFundingHistoryAsync(BybitCategory category, string symbol, long? startTime = null, long? endTime = null, int? limit = null, CancellationToken ct = default)
    {
        if (category.IsNotIn(BybitCategory.Linear, BybitCategory.Inverse))
            throw new NotSupportedException($"{category} is not supported for this endpoint.");

        limit?.ValidateIntBetween(nameof(limit), 1, 200);
        var parameters = new Dictionary<string, object>
        {
            { "category", category.GetLabel() },
            { "symbol", symbol },
        };
        parameters.AddOptionalParameter("startTime", startTime);
        parameters.AddOptionalParameter("endTime", endTime);
        parameters.AddOptionalParameter("limit", limit);

        var result = await MainClient.SendBybitRequest<BybitCategoryResponse<BybitFundingHistory>>(MainClient.BuildUri(_v5MarketFundingHistoryEndpoint), HttpMethod.Get, ct, queryParameters: parameters).ConfigureAwait(false);
        if (!result) return result.As<List<BybitFundingHistory>>(null);
        return result.As(result.Data.Payload);
    }

    /// <summary>
    /// Query recent public trading data in Bybit.
    /// Covers: Spot / USDT perpetual / USDC contract / Inverse contract / Option
    /// You can download archived historical trades from the website
    /// </summary>
    /// <param name="category">Product type. spot,linear,inverse,option</param>
    /// <param name="symbol">Symbol name
    /// required for spot/linear/inverse
    /// optional for option</param>
    /// <param name="baseAsset">Base coin
    /// Apply to option only
    /// If the field is not passed, return BTC data by default</param>
    /// <param name="optionType">Option type. Call or Put. Apply to option only</param>
    /// <param name="limit">Limit for data size per page
    /// spot: [1,60], default: 60
    /// others: [1,1000], default: 500</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public async Task<BybitRestCallResult<List<BybitPublicTrade>>> GetTradingHistoryAsync(BybitCategory category, string symbol = null, string baseAsset = null, BybitOptionType? optionType = null, int? limit = null, CancellationToken ct = default)
    {
        if (category == BybitCategory.Spot) limit?.ValidateIntBetween(nameof(limit), 1, 60);
        if (category == BybitCategory.Linear) limit?.ValidateIntBetween(nameof(limit), 1, 1000);
        if (category == BybitCategory.Inverse) limit?.ValidateIntBetween(nameof(limit), 1, 1000);
        if (category == BybitCategory.Option) limit?.ValidateIntBetween(nameof(limit), 1, 1000);

        var parameters = new Dictionary<string, object>
        {
            { "category", category.GetLabel() },
        };
        parameters.AddOptionalParameter("symbol", symbol);
        parameters.AddOptionalParameter("baseCoin", baseAsset);
        parameters.AddOptionalParameter("optionType", optionType?.GetLabel());
        parameters.AddOptionalParameter("limit", limit);

        var result = await MainClient.SendBybitRequest<BybitCategoryResponse<BybitPublicTrade>>(MainClient.BuildUri(_v5MarketRecentTradeEndpoint), HttpMethod.Get, ct, queryParameters: parameters).ConfigureAwait(false);
        if (!result) return result.As<List<BybitPublicTrade>>(null);
        return result.As(result.Data.Payload);
    }

    /// <summary>
    /// Get the open interest of each symbol.
    /// Covers: USDT perpetual / USDC contract / Inverse contract
    /// 
    /// INFO
    /// The upper limit time you can query is the launch time of the symbol.
    /// </summary>
    /// <param name="category">Product type. linear,inverse</param>
    /// <param name="symbol">Symbol name</param>
    /// <param name="period">Interval time. 5min,15min,30min,1h,4h,1d</param>
    /// <param name="startTime">The start timestamp (ms)</param>
    /// <param name="endTime">The end timestamp (ms)</param>
    /// <param name="limit">Limit for data size per page. [1, 200]. Default: 50</param>
    /// <param name="cursor">Cursor. Used to paginate</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public async Task<BybitRestCallResult<List<BybitOpenInterest>>> GetOpenInterestAsync(BybitCategory category, string symbol, BybitRecordPeriod period, DateTime startTime, DateTime endTime, int limit = 50, string cursor = null, CancellationToken ct = default)
        => await GetOpenInterestAsync(category, symbol, period, startTime.ConvertToMilliseconds(), endTime.ConvertToMilliseconds(), limit, cursor, ct).ConfigureAwait(false);

    /// <summary>
    /// Get the open interest of each symbol.
    /// Covers: USDT perpetual / USDC contract / Inverse contract
    /// 
    /// INFO
    /// The upper limit time you can query is the launch time of the symbol.
    /// </summary>
    /// <param name="category">Product type. linear,inverse</param>
    /// <param name="symbol">Symbol name</param>
    /// <param name="period">Interval time. 5min,15min,30min,1h,4h,1d</param>
    /// <param name="startTime">The start timestamp (ms)</param>
    /// <param name="endTime">The end timestamp (ms)</param>
    /// <param name="limit">Limit for data size per page. [1, 200]. Default: 50</param>
    /// <param name="cursor">Cursor. Used to paginate</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    /// <exception cref="NotSupportedException"></exception>
    public async Task<BybitRestCallResult<List<BybitOpenInterest>>> GetOpenInterestAsync(BybitCategory category, string symbol, BybitRecordPeriod period, long? startTime = null, long? endTime = null, int limit = 50, string cursor = null, CancellationToken ct = default)
    {
        if (category.IsNotIn(BybitCategory.Linear, BybitCategory.Inverse))
            throw new NotSupportedException($"{category} is not supported for this endpoint.");

        limit.ValidateIntBetween(nameof(limit), 1, 200);
        var parameters = new Dictionary<string, object>
        {
            { "category", category.GetLabel() },
            { "symbol", symbol },
            { "intervalTime", period.GetLabel() },
        };
        parameters.AddOptionalParameter("startTime", startTime);
        parameters.AddOptionalParameter("endTime", endTime);
        parameters.AddOptionalParameter("cursor", cursor);
        parameters.AddOptionalParameter("limit", limit);

        var result = await MainClient.SendBybitRequest<BybitCursorResponse<BybitOpenInterest>>(MainClient.BuildUri(_v5MarketOpenInterestEndpoint), HttpMethod.Get, ct, queryParameters: parameters).ConfigureAwait(false);
        if (!result) return result.As<List<BybitOpenInterest>>(null);
        return result.AsWithCursor(result.Data.Payload, result.Data.NextPageCursor);
    }

    /// <summary>
    /// Get Historical Volatility
    /// Query option historical volatility
    /// 
    /// Covers: Option
    /// 
    /// INFO
    /// The data is hourly.
    /// If both startTime and endTime are not specified, it will return the most recent 1 hours worth of data.
    /// startTime and endTime are a pair of params. Either both are passed or they are not passed at all.
    /// This endpoint can query the last 2 years worth of data, but make sure [endTime - startTime] &lt;= 30 days.
    /// </summary>
    /// <param name="category">Product type. option</param>
    /// <param name="startTime"></param>
    /// <param name="endTime"></param>
    /// <param name="baseAsset">Base coin. Default: return BTC data</param>
    /// <param name="period">Period. If not specified, it will return data with a 7-day average by default</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public async Task<BybitRestCallResult<List<BybitVolatility>>> GetVolatilityAsync(BybitCategory category, DateTime startTime, DateTime endTime, string baseAsset = null, BybitOptionPeriod? period = null, CancellationToken ct = default)
        => await GetVolatilityAsync(category, startTime.ConvertToMilliseconds(), endTime.ConvertToMilliseconds(), baseAsset, period, ct).ConfigureAwait(false);

    /// <summary>
    /// Get Historical Volatility
    /// Query option historical volatility
    /// 
    /// Covers: Option
    /// 
    /// INFO
    /// The data is hourly.
    /// If both startTime and endTime are not specified, it will return the most recent 1 hours worth of data.
    /// startTime and endTime are a pair of params. Either both are passed or they are not passed at all.
    /// This endpoint can query the last 2 years worth of data, but make sure [endTime - startTime] &lt;= 30 days.
    /// </summary>
    /// <param name="category">Product type. option</param>
    /// <param name="startTime"></param>
    /// <param name="endTime"></param>
    /// <param name="baseAsset">Base coin. Default: return BTC data</param>
    /// <param name="period">Period. If not specified, it will return data with a 7-day average by default</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public async Task<BybitRestCallResult<List<BybitVolatility>>> GetVolatilityAsync(BybitCategory category, long? startTime = null, long? endTime = null, string baseAsset = null, BybitOptionPeriod? period = null, CancellationToken ct = default)
    {
        if (category.IsNotIn(BybitCategory.Option))
            throw new NotSupportedException($"{category} is not supported for this endpoint.");

        var parameters = new Dictionary<string, object>
        {
            { "category", category.GetLabel() },
        };
        parameters.AddOptionalParameter("baseCoin", baseAsset);
        parameters.AddOptionalParameter("period", period?.GetLabel());
        parameters.AddOptionalParameter("startTime", startTime);
        parameters.AddOptionalParameter("endTime", endTime);

        return await MainClient.SendBybitRequest<List<BybitVolatility>>(MainClient.BuildUri(_v5MarketHistoricalVolatilityEndpoint), HttpMethod.Get, ct, queryParameters: parameters).ConfigureAwait(false);
    }

    /// <summary>
    /// Query for Bybit insurance pool data (BTC/USDT/USDC etc). The data is updated every 24 hours.
    /// 
    /// INFO
    /// Since the insurance pool data is updated every 24 hours, it is possible that you get ADL trade but the insruance pool still has sufficient funds.
    /// </summary>
    /// <param name="asset">coin. Default: return all insurance coins</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public async Task<BybitRestCallResult<List<BybitInsurance>>> GetInsuranceAsync(string asset = null, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>();
        parameters.AddOptionalParameter("coin", asset);

        var result = await MainClient.SendBybitRequest<BybitUpdateResponse<BybitInsurance>>(MainClient.BuildUri(_v5MarketInsuranceEndpoint), HttpMethod.Get, ct, queryParameters: parameters).ConfigureAwait(false);
        if (!result) return result.As<List<BybitInsurance>>(null);
        return result.As(result.Data.Payload);
    }

    /// <summary>
    /// Get Risk Limit
    /// Query for the risk limit.
    /// Covers: USDT perpetual / USDC contract / Inverse contract
    /// </summary>
    /// <param name="category">Product type. linear,inverse</param>
    /// <param name="symbol">Symbol name</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    /// <exception cref="NotSupportedException"></exception>
    public async Task<BybitRestCallResult<List<BybitRiskLimit>>> GetRiskLimitAsync(BybitCategory category, string symbol = null, CancellationToken ct = default)
    {
        if (category.IsNotIn(BybitCategory.Linear, BybitCategory.Inverse))
            throw new NotSupportedException($"{category} is not supported for this endpoint.");

        var parameters = new Dictionary<string, object>
        {
            { "category", category.GetLabel() },
        };
        parameters.AddOptionalParameter("symbol", symbol);

        var result = await MainClient.SendBybitRequest<BybitCategoryResponse<BybitRiskLimit>>(MainClient.BuildUri(_v5MarketRiskLimitEndpoint), HttpMethod.Get, ct, queryParameters: parameters).ConfigureAwait(false);
        if (!result) return result.As<List<BybitRiskLimit>>(null);
        return result.As(result.Data.Payload);
    }

    /// <summary>
    /// Get the delivery price.
    /// Covers: USDC futures / Inverse futures / Option
    /// 
    /// INFO
    /// Option: only returns those symbols are being "DELIVERING" (UTC8~UTC12) when "symbol" is not specified;
    /// </summary>
    /// <param name="category">Product type. linear, inverse, option</param>
    /// <param name="symbol">Symbol name</param>
    /// <param name="baseAsset">Base coin. Default: BTC. valid for option only</param>
    /// <param name="limit">Limit for data size per page. [1, 200]. Default: 50</param>
    /// <param name="cursor">Cursor. Use the nextPageCursor token from the response to retrieve the next page of the result set</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    /// <exception cref="NotSupportedException"></exception>
    public async Task<BybitRestCallResult<List<BybitDeliveryPrice>>> GetDeliveryPriceAsync(BybitCategory category, string symbol = null, string baseAsset = null, int? limit = null, string cursor = null, CancellationToken ct = default)
    {
        if (category.IsNotIn(BybitCategory.Linear, BybitCategory.Inverse, BybitCategory.Option))
            throw new NotSupportedException($"{category} is not supported for this endpoint.");

        limit?.ValidateIntBetween(nameof(limit), 1, 200);
        var parameters = new Dictionary<string, object>
        {
            { "category", category.GetLabel() },
        };
        parameters.AddOptionalParameter("symbol", symbol);
        parameters.AddOptionalParameter("baseCoin", baseAsset);
        parameters.AddOptionalParameter("limit", limit);
        parameters.AddOptionalParameter("cursor", cursor);

        var result = await MainClient.SendBybitRequest<BybitCursorResponse<BybitDeliveryPrice>>(MainClient.BuildUri(_v5MarketDeliveryPriceEndpoint), HttpMethod.Get, ct, queryParameters: parameters).ConfigureAwait(false);
        if (!result) return result.As<List<BybitDeliveryPrice>>(null);
        return result.AsWithCursor(result.Data.Payload, result.Data.NextPageCursor);
    }

    /// <summary>
    /// Get Long Short Ratio
    /// </summary>
    /// <param name="category">Product type. linear(USDT Perpetual),inverse</param>
    /// <param name="symbol">Symbol name</param>
    /// <param name="period">Data recording period. 5min, 15min, 30min, 1h, 4h, 1d</param>
    /// <param name="limit">Limit for data size per page. [1, 500]. Default: 50</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    /// <exception cref="NotSupportedException"></exception>
    public async Task<BybitRestCallResult<List<BybitLongShortRatio>>> GetLongShortRatioAsync(BybitCategory category, string symbol, BybitRecordPeriod period, int limit = 50, CancellationToken ct = default)
    {
        if (category.IsNotIn(BybitCategory.Linear, BybitCategory.Inverse))
            throw new NotSupportedException($"{category} is not supported for this endpoint.");

        limit.ValidateIntBetween(nameof(limit), 1, 500);
        var parameters = new Dictionary<string, object>
        {
            { "category", category.GetLabel() },
            { "symbol", symbol },
            { "period", period.GetLabel() },
            { "limit", limit },
        };

        var result = await MainClient.SendBybitRequest<BybitListResponse<BybitLongShortRatio>>(MainClient.BuildUri(_v5MarketAccountRatioEndpoint), HttpMethod.Get, ct, queryParameters: parameters).ConfigureAwait(false);
        if (!result) return result.As<List<BybitLongShortRatio>>(null);
        return result.As(result.Data.Payload);
    }

}
