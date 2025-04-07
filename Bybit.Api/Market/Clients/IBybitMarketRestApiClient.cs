
namespace Bybit.Api.Market;

public interface IBybitMarketRestApiClient
{
    Task<BybitRestCallResult<List<BybitMarketDeliveryPrice>>> GetDeliveryPriceAsync(BybitCategory category, string symbol = null, string baseAsset = null, int? limit = null, string cursor = null, CancellationToken ct = default);
    Task<BybitRestCallResult<List<BybitMarketFundingHistory>>> GetFundingHistoryAsync(BybitCategory category, string symbol, long? startTime = null, long? endTime = null, int? limit = null, CancellationToken ct = default);
    Task<BybitRestCallResult<List<BybitMarketFundingHistory>>> GetFundingHistoryAsync(BybitCategory category, string symbol, DateTime startTime, DateTime endTime, int limit = 200, CancellationToken ct = default);
    Task<BybitRestCallResult<List<BybitMarketIndexPriceKline>>> GetIndexKlinesAsync(BybitCategory category, string symbol, BybitInterval interval, long? start = null, long? end = null, int limit = 200, CancellationToken ct = default);
    Task<BybitRestCallResult<List<BybitMarketIndexPriceKline>>> GetIndexKlinesAsync(BybitCategory category, string symbol, BybitInterval interval, DateTime start, DateTime end, int limit = 200, CancellationToken ct = default);
    Task<BybitRestCallResult<List<BybitMarketInsurance>>> GetInsuranceAsync(string asset = null, CancellationToken ct = default);
    Task<BybitRestCallResult<List<BybitMarketFuturesInstrument>>> GetInverseInstrumentsAsync(string symbol = null, BybitInstrumentStatus? status = null, string baseAsset = null, int limit = 500, string cursor = null, CancellationToken ct = default);
    Task<BybitRestCallResult<List<BybitMarketFuturesTicker>>> GetInverseTickersAsync(string symbol = null, CancellationToken ct = default);
    Task<BybitRestCallResult<List<BybitMarketKline>>> GetKlinesAsync(BybitCategory category, string symbol, BybitInterval interval, long? start = null, long? end = null, int limit = 200, CancellationToken ct = default);
    Task<BybitRestCallResult<List<BybitMarketKline>>> GetKlinesAsync(BybitCategory category, string symbol, BybitInterval interval, DateTime start, DateTime end, int limit = 200, CancellationToken ct = default);
    Task<BybitRestCallResult<List<BybitMarketFuturesInstrument>>> GetLinearInstrumentsAsync(string symbol = null, BybitInstrumentStatus? status = null, string baseAsset = null, int limit = 500, string cursor = null, CancellationToken ct = default);
    Task<BybitRestCallResult<List<BybitMarketFuturesTicker>>> GetLinearTickersAsync(string symbol = null, CancellationToken ct = default);
    Task<BybitRestCallResult<List<BybitMarketLongShortRatio>>> GetLongShortRatioAsync(BybitCategory category, string symbol, BybitRecordPeriod period, int limit = 50, CancellationToken ct = default);
    Task<BybitRestCallResult<List<BybitMarketMarkPriceKline>>> GetMarkKlinesAsync(BybitCategory category, string symbol, BybitInterval interval, long? start = null, long? end = null, int limit = 200, CancellationToken ct = default);
    Task<BybitRestCallResult<List<BybitMarketMarkPriceKline>>> GetMarkKlinesAsync(BybitCategory category, string symbol, BybitInterval interval, DateTime start, DateTime end, int limit = 200, CancellationToken ct = default);
    Task<BybitRestCallResult<List<BybitMarketOpenInterest>>> GetOpenInterestAsync(BybitCategory category, string symbol, BybitRecordPeriod period, long? startTime = null, long? endTime = null, int limit = 50, string cursor = null, CancellationToken ct = default);
    Task<BybitRestCallResult<List<BybitMarketOpenInterest>>> GetOpenInterestAsync(BybitCategory category, string symbol, BybitRecordPeriod period, DateTime startTime, DateTime endTime, int limit = 50, string cursor = null, CancellationToken ct = default);
    Task<BybitRestCallResult<List<BybitMarketOptionInstrument>>> GetOptionInstrumentsAsync(string symbol = null, BybitInstrumentStatus? status = null, string baseAsset = null, int limit = 500, string cursor = null, CancellationToken ct = default);
    Task<BybitRestCallResult<List<BybitMarketOptionTicker>>> GetOptionTickersAsync(string symbol = null, string baseAsset = null, string expDate = null, CancellationToken ct = default);
    Task<BybitRestCallResult<BybitMarketOrderbook>> GetOrderbookAsync(BybitCategory category, string symbol, int? limit = null, CancellationToken ct = default);
    Task<BybitRestCallResult<List<BybitMarketPremiumIndexPriceKline>>> GetPremiumIndexKlinesAsync(BybitCategory category, string symbol, BybitInterval interval, long? start = null, long? end = null, int limit = 200, CancellationToken ct = default);
    Task<BybitRestCallResult<List<BybitMarketPremiumIndexPriceKline>>> GetPremiumIndexKlinesAsync(BybitCategory category, string symbol, BybitInterval interval, DateTime start, DateTime end, int limit = 200, CancellationToken ct = default);
    Task<BybitRestCallResult<List<BybitMarketRiskLimit>>> GetRiskLimitAsync(BybitCategory category, string symbol = null, CancellationToken ct = default);
    Task<BybitRestCallResult<BybitMarketTime>> GetServerTimeAsync(CancellationToken ct = default);
    Task<BybitRestCallResult<List<BybitMarketSpotInstrument>>> GetSpotInstrumentsAsync(string symbol = null, BybitInstrumentStatus? status = null, int limit = 500, string cursor = null, CancellationToken ct = default);
    Task<BybitRestCallResult<List<BybitMarketSpotTicker>>> GetSpotTickersAsync(string symbol = null, CancellationToken ct = default);
    Task<BybitRestCallResult<List<BybitMarketTrade>>> GetTradingHistoryAsync(BybitCategory category, string symbol = null, string baseAsset = null, BybitOptionType? optionType = null, int? limit = null, CancellationToken ct = default);
    Task<BybitRestCallResult<List<BybitMarketVolatility>>> GetVolatilityAsync(BybitCategory category, long? startTime = null, long? endTime = null, string baseAsset = null, BybitOptionPeriod? period = null, CancellationToken ct = default);
    Task<BybitRestCallResult<List<BybitMarketVolatility>>> GetVolatilityAsync(BybitCategory category, DateTime startTime, DateTime endTime, string baseAsset = null, BybitOptionPeriod? period = null, CancellationToken ct = default);
}