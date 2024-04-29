﻿namespace Bybit.Api.Models.Account;

/// <summary>
/// Bybit Universal Transfer
/// </summary>
public class BybitUniversalTransfer
{
    /// <summary>
    /// Transfer Id
    /// </summary>
    public string TransferId { get; set; }

    /// <summary>
    /// Asset
    /// </summary>
    [JsonProperty("coin")]
    public string Asset { get; set; }

    /// <summary>
    /// Quantity
    /// </summary>
    [JsonProperty("amount")]
    public decimal Quantity { get; set; }

    /// <summary>
    /// From UID
    /// </summary>
    public string FromMemberId { get; set; }

    /// <summary>
    /// To UID
    /// </summary>
    public string ToMemberId { get; set; }

    /// <summary>
    /// From Account
    /// </summary>
    [JsonProperty("fromAccountType"), JsonConverter(typeof(LabelConverter<BybitAccount>))]
    public BybitAccount FromAccount { get; set; }

    /// <summary>
    /// To Account
    /// </summary>
    [JsonProperty("toAccountType"), JsonConverter(typeof(LabelConverter<BybitAccount>))]
    public BybitAccount ToAccount { get; set; }

    /// <summary>
    /// Timestamp
    /// </summary>
    public long Timestamp { get; set; }

    /// <summary>
    /// Time
    /// </summary>
    public DateTime Time { get => Timestamp.ConvertFromMilliseconds(); }

    /// <summary>
    /// Status
    /// </summary>
    [JsonProperty("status"), JsonConverter(typeof(LabelConverter<BybitTransferStatus>))]
    public BybitTransferStatus Status { get; set; }
}
