﻿namespace Bybit.Api.Models.Trade;

/// <summary>
/// Bybit Take Profit Stop Loss State
/// </summary>
public class BybitTakeProfitStopLossState
{
    /// <summary>
    /// Take Profit Stop Loss Mode
    /// </summary>
    [JsonProperty("tpSlMode")]
    public BybitTakeProfitStopLossMode TakeProfitStopLossMode { get; set; }
}
