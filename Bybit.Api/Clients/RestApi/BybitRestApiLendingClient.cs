﻿using Bybit.Api.Models.Lending;

namespace Bybit.Api.Clients.RestApi;

public class BybitRestApiLendingClient
{
    // Institutional Lending Endpoints
    protected const string v5InsLoanProductInfosEndpoint = "v5/ins-loan/product-infos";
    protected const string v5InsLoanEnsureTokensEndpoint = "v5/ins-loan/ensure-tokens"; // TODO: Remove
    protected const string v5InsLoanEnsureTokensConvertEndpoint = "v5/ins-loan/ensure-tokens-convert"; // TODO
    protected const string v5InsLoanLoanOrderEndpoint = "v5/ins-loan/loan-order";
    protected const string v5InsLoanRepaidHistoryEndpoint = "v5/ins-loan/repaid-history";
    protected const string v5InsLoanLtvConvertEndpoint = "v5/ins-loan/ltv-convert"; // TODO
    protected const string v5InsLoanAssociationUidEndpoint = "v5/ins-loan/association-uid"; // TODO

    #region Internal
    internal BybitRestApiBaseClient MainClient { get; }
    internal BybitRestApiLendingClient(BybitRestApiClient root)
    {
        this.MainClient = root.BaseClient;
    }
    #endregion

    public async Task<BybitRestCallResult<List<BybitLendingProduct>>> GetLendingProductsAsync(string productId = null, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>();
        parameters.AddOptionalParameter("productId", productId);

        var result = await MainClient.SendBybitRequest<BybitLendingProductContainer>(MainClient.BuildUri(v5InsLoanProductInfosEndpoint), HttpMethod.Get, ct, queryParameters: parameters).ConfigureAwait(false);
        if (!result) return result.As<List<BybitLendingProduct>>(null);
        return result.As(result.Data.Payload);
    }

    public async Task<BybitRestCallResult<List<BybitLendingToken>>> GetLendingTokensAsync(string token = null, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>();
        parameters.AddOptionalParameter("ltCoin", token);

        var result = await MainClient.SendBybitRequest<BybitLendingTokenContainer>(MainClient.BuildUri(v5InsLoanEnsureTokensEndpoint), HttpMethod.Get, ct, queryParameters: parameters).ConfigureAwait(false);
        if (!result) return result.As<List<BybitLendingToken>>(null);
        return result.As(result.Data.Payload);
    }

    public async Task<BybitRestCallResult<List<BybitLendingLoanOrder>>> GetLoanOrdersAsync(string orderId=null, long? startTime=null, long? endTime= null, int? limit=null, CancellationToken ct = default)
    {
        limit?.ValidateIntBetween(nameof(limit), 1, 100);

        var parameters = new Dictionary<string, object>();
        parameters.AddOptionalParameter("orderId", orderId);
        parameters.AddOptionalParameter("startTime", startTime);
        parameters.AddOptionalParameter("endTime", endTime);
        parameters.AddOptionalParameter("limit", limit);

        var result = await MainClient.SendBybitRequest<BybitLendingLoanOrderContainer>(MainClient.BuildUri(v5InsLoanLoanOrderEndpoint), HttpMethod.Get, ct, true, queryParameters: parameters).ConfigureAwait(false);
        if (!result) return result.As<List<BybitLendingLoanOrder>>(null);
        return result.As(result.Data.Payload);
    }

    public async Task<BybitRestCallResult<List<BybitLendingRepayOrder>>> GetRepayOrdersAsync(long? startTime = null, long? endTime = null, int? limit = null, CancellationToken ct = default)
    {
        limit?.ValidateIntBetween(nameof(limit), 1, 100);

        var parameters = new Dictionary<string, object>();
        parameters.AddOptionalParameter("startTime", startTime);
        parameters.AddOptionalParameter("endTime", endTime);
        parameters.AddOptionalParameter("limit", limit);

        var result = await MainClient.SendBybitRequest<BybitLendingRepayOrderContainer>(MainClient.BuildUri(v5InsLoanRepaidHistoryEndpoint), HttpMethod.Get, ct, true, queryParameters: parameters).ConfigureAwait(false);
        if (!result) return result.As<List<BybitLendingRepayOrder>>(null);
        return result.As(result.Data.Payload);
    }

    // LTV: Customer Lifetime Value

}