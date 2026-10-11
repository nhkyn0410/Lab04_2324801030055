namespace PolicyService.Api.Commands;

public class CreateOfferResult
{
    public string OfferNumber { get; set; } = string.Empty;
    public decimal TotalPrice { get; set; }
    public Dictionary<string, decimal> CoversPrices { get; set; } = new();
}
