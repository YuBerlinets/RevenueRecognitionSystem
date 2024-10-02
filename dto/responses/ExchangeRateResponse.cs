using Newtonsoft.Json;

namespace apbd_project.dto.responses;

public class ExchangeRateResponse
{
    [JsonProperty("table")] public string Table { get; set; }

    [JsonProperty("currency")] public string Currency { get; set; }

    [JsonProperty("code")] public string Code { get; set; }

    [JsonProperty("rates")] public ExchangeRate[] Rates { get; set; }
}

public class ExchangeRate
{
    [JsonProperty("no")] public string No { get; set; }

    [JsonProperty("effectiveDate")] public DateTime EffectiveDate { get; set; }
    [JsonProperty("mid")] public double Mid { get; set; }
}