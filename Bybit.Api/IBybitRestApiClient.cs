using Bybit.Api.Account;
using Bybit.Api.Asset;
using Bybit.Api.Loan;
using Bybit.Api.Margin;
using Bybit.Api.Market;
using Bybit.Api.Tokens;
using Bybit.Api.Trading;
using Bybit.Api.User;

namespace Bybit.Api;
public interface IBybitRestApiClient
{
    BybitAccountRestApiClient Account { get; }
    BybitAssetRestApiClient Asset { get; }
    BybitLeverageTokenRestApiClient LeverageToken { get; }
    BybitLoanRestApiClient Loan { get; }
    BybitMarginRestApiClient Margin { get; }
    IBybitMarketRestApiClient Market { get; }
    BybitPositionRestApiClient Position { get; }
    IBybitTradeRestApiClient Trade { get; }
    BybitUserRestApiClient User { get; }

    void SetApiCredentials(ApiCredentials credentials);
    void SetApiCredentials(string apiKey, string apiSecret);
}