﻿using Bybit.Api.Models.Account;

namespace Bybit.Api.Clients.RestApi;

/// <summary>
/// Bybit Rest API Account Client
/// </summary>
public class BybitRestApiAccountClient
{
    // Account Endpoints
    private const string _v5AccountWalletBalanceEndpoint = "v5/account/wallet-balance";
    private const string _v5AccountUpgradeToUtaEndpoint = "v5/account/upgrade-to-uta";
    private const string _v5AccountBorrowHistoryEndpoint = "v5/account/borrow-history";
    private const string _v5AccountQuickRepaymentEndpoint = "v5/account/quick-repayment"; // TODO
    private const string _v5AccountSetCollateralSwitchEndpoint = "v5/account/set-collateral-switch"; // TODO
    private const string _v5AccountSetCollateralSwitchBatchEndpoint = "v5/account/set-collateral-switch-batch"; // TODO
    private const string _v5AccountCollateralInfoEndpoint = "v5/account/collateral-info";
    private const string _v5AssetCoinGreeksEndpoint = "v5/asset/coin-greeks";
    private const string _v5AccountFeeRateEndpoint = "v5/account/fee-rate";
    private const string _v5AccountInfoEndpoint = "v5/account/info";
    private const string _v5AccountTransactionLogEndpoint = "v5/account/transaction-log";
    private const string _v5AccountContractTransactionLogEndpoint = "v5/account/contract-transaction-log"; // TODO
    private const string _v5AccountSmpGroupEndpoint = "v5/account/smp-group"; // TODO
    private const string _v5AccountSetMarginModeEndpoint = "v5/account/set-margin-mode";
    private const string _v5AccountSetHedgingModeEndpoint = "v5/account/set-hedging-mode"; // TODO
    private const string _v5AccountMmpModifyEndpoint = "v5/account/mmp-modify";
    private const string _v5AccountMmpResetEndpoint = "v5/account/mmp-reset";
    private const string _v5AccountMmpStateEndpoint = "v5/account/mmp-state";

    #region Internal
    internal BybitRestApiBaseClient MainClient { get; }
    internal BybitRestApiAccountClient(BybitRestApiClient root)
    {
        this.MainClient = root.BaseClient;
    }
    #endregion

    #region Account Methods
    /// <summary>
    /// Obtain wallet balance, query asset information of each currency, and account risk rate information. By default, currency information with assets or liabilities of 0 is not returned.
    /// </summary>
    /// <param name="account">Account type</param>
    /// <param name="asset">Coin name</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public async Task<BybitRestCallResult<List<BybitBalance>>> GetBalancesAsync(BybitAccount account, string asset = null, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>
        {
            { "accountType", account.GetLabel() }
        };
        parameters.AddOptionalParameter("coin", asset);

        var result = await MainClient.SendBybitRequest<BybitListResponse<BybitBalance>>(MainClient.BuildUri(_v5AccountWalletBalanceEndpoint), HttpMethod.Get, ct, true, queryParameters: parameters).ConfigureAwait(false);
        if (!result) return result.As<List<BybitBalance>>(null);
        return result.As(result.Data.Payload);
    }

    /// <summary>
    /// Upgrade Unified Account
    /// </summary>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public async Task<BybitRestCallResult<BybitUnifiedUpgrade>> UpgradeToUnifiedAccountAsync(CancellationToken ct = default)
    {
        return await MainClient.SendBybitRequest<BybitUnifiedUpgrade>(MainClient.BuildUri(_v5AccountUpgradeToUtaEndpoint), HttpMethod.Post, ct, true).ConfigureAwait(false);
    }

    /// <summary>
    /// Get Borrow History
    /// </summary>
    /// <param name="asset">Currency</param>
    /// <param name="startTime">The start timestamp (ms)</param>
    /// <param name="endTime">The end time. timestamp (ms)</param>
    /// <param name="limit">Limit for data size per page. [1, 50]. Default: 20</param>
    /// <param name="cursor">Cursor. Use the nextPageCursor token from the response to retrieve the next page of the result set</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public async Task<BybitRestCallResult<List<BybitBorrowHistory>>> GetBorrowHistoryAsync(string asset = null, long? startTime = null, long? endTime = null, int? limit = null, string cursor = null, CancellationToken ct = default)
    {
        limit?.ValidateIntValues(nameof(limit), 1, 50);
        var parameters = new Dictionary<string, object>();
        parameters.AddOptionalParameter("currency", asset);
        parameters.AddOptionalParameter("startTime", startTime);
        parameters.AddOptionalParameter("endTime", endTime);
        parameters.AddOptionalParameter("limit", limit);
        parameters.AddOptionalParameter("cursor", cursor);

        var result = await MainClient.SendBybitRequest<BybitCursorResponse<BybitBorrowHistory>>(MainClient.BuildUri(_v5AccountBorrowHistoryEndpoint), HttpMethod.Get, ct, true, queryParameters: parameters).ConfigureAwait(false);
        if (!result) return result.As<List<BybitBorrowHistory>>(null);
        return result.AsWithCursor(result.Data.Payload, result.Data.NextPageCursor);
    }

    public async Task<BybitRestCallResult<List<BybitCollateralInfo>>> GetCollateralInfoAsync(string asset = null, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>();
        parameters.AddOptionalParameter("currency", asset);

        var result = await MainClient.SendBybitRequest<BybitListResponse<BybitCollateralInfo>>(MainClient.BuildUri(_v5AccountCollateralInfoEndpoint), HttpMethod.Get, ct, true, queryParameters: parameters).ConfigureAwait(false);
        if (!result) return result.As<List<BybitCollateralInfo>>(null);
        return result.As(result.Data.Payload);
    }

    public async Task<BybitRestCallResult<List<BybitGreeks>>> GetAssetGreeksAsync(string baseAsset = null, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>();
        parameters.AddOptionalParameter("baseCoin", baseAsset);

        var result = await MainClient.SendBybitRequest<BybitListResponse<BybitGreeks>>(MainClient.BuildUri(_v5AssetCoinGreeksEndpoint), HttpMethod.Get, ct, true, queryParameters: parameters).ConfigureAwait(false);
        if (!result) return result.As<List<BybitGreeks>>(null);
        return result.As(result.Data.Payload);
    }

    public async Task<BybitRestCallResult<List<BybitFeeRate>>> GetFeeRateAsync(BybitCategory category, string symbol = null, string baseAsset = null, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>
        {
            { "category", category.GetLabel() },
        };
        parameters.AddOptionalParameter("symbol", symbol);
        parameters.AddOptionalParameter("baseCoin", baseAsset);

        var result = await MainClient.SendBybitRequest<BybitListResponse<BybitFeeRate>>(MainClient.BuildUri(_v5AccountFeeRateEndpoint), HttpMethod.Get, ct, true, queryParameters: parameters).ConfigureAwait(false);
        if (!result) return result.As<List<BybitFeeRate>>(null);
        return result.As(result.Data.Payload);
    }

    public async Task<BybitRestCallResult<BybitAccountInfo>> GetAccountInfoAsync(CancellationToken ct = default)
    {
        return await MainClient.SendBybitRequest<BybitAccountInfo>(MainClient.BuildUri(_v5AccountInfoEndpoint), HttpMethod.Get, ct, true).ConfigureAwait(false);
    }

    public async Task<BybitRestCallResult<List<BybitTransaction>>> GetTransactionHistoryAsync(BybitAccount? account = null, BybitCategory? category = null, string asset = null, string baseAsset = null, BybitTransactionType? type = null, long? startTime = null, long? endTime = null, int? limit = null, string cursor = null, CancellationToken ct = default)
    {
        limit?.ValidateIntValues(nameof(limit), 1, 50);

        var parameters = new Dictionary<string, object>();
        parameters.AddOptionalParameter("accountType", account?.GetLabel());
        parameters.AddOptionalParameter("category", category?.GetLabel());
        parameters.AddOptionalParameter("currency", asset);
        parameters.AddOptionalParameter("baseCoin", baseAsset);
        parameters.AddOptionalParameter("type", type?.GetLabel());
        parameters.AddOptionalParameter("startTime", startTime);
        parameters.AddOptionalParameter("endTime", endTime);
        parameters.AddOptionalParameter("limit", limit);
        parameters.AddOptionalParameter("cursor", cursor);

        var result = await MainClient.SendBybitRequest<BybitCursorResponse<BybitTransaction>>(MainClient.BuildUri(_v5AccountTransactionLogEndpoint), HttpMethod.Get, ct, true, queryParameters: parameters).ConfigureAwait(false);
        if (!result) return result.As<List<BybitTransaction>>(null);
        return result.AsWithCursor(result.Data.Payload, result.Data.NextPageCursor);
    }

    public async Task<BybitRestCallResult<List<BybitReason>>> SetMarginModeAsync(BybitMarginMode marginMode, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>()
        {
            { "setMarginMode", marginMode.GetLabel() }
        };

        return await MainClient.SendBybitRequest<List<BybitReason>>(MainClient.BuildUri(_v5AccountSetMarginModeEndpoint), HttpMethod.Post, ct, true, bodyParameters: parameters).ConfigureAwait(false);
    }

    public async Task<BybitRestCallResult> SetMarketMakerProtectionAsync(string baseAsset, int window, int frozenPeriod, decimal quantityLimit, decimal deltaLimit, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>()
        {
            { "baseCoin", baseAsset },
            { "window", window },
            { "frozenPeriod", frozenPeriod },
            { "qtyLimit", quantityLimit },
            { "deltaLimit", deltaLimit },
        };

        return await MainClient.SendBybitRequest(MainClient.BuildUri(_v5AccountMmpModifyEndpoint), HttpMethod.Post, ct, true, bodyParameters: parameters).ConfigureAwait(false);
    }

    public async Task<BybitRestCallResult> ResetMarketMakerProtectionAsync(string baseAsset, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>()
        {
            { "baseCoin", baseAsset },
        };

        return await MainClient.SendBybitRequest(MainClient.BuildUri(_v5AccountMmpResetEndpoint), HttpMethod.Post, ct, true, bodyParameters: parameters).ConfigureAwait(false);
    }

    public async Task<BybitRestCallResult<List<BybitMmpState>>> GetMarketMakerProtectionAsync(string baseAsset, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>()
        {
            { "baseCoin", baseAsset },
        };

        var result = await MainClient.SendBybitRequest<BybitResultResponse<BybitMmpState>>(MainClient.BuildUri(_v5AccountMmpStateEndpoint), HttpMethod.Get, ct, true, queryParameters: parameters).ConfigureAwait(false);
        if (!result) return result.As<List<BybitMmpState>>(null);
        return result.As(result.Data.Payload);
    }
    #endregion

}