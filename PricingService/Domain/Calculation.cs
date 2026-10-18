namespace PricingService.Domain;

public class Calculation
{
    public Calculation(string productCode, DateTimeOffset policyFrom, DateTimeOffset policyTo,
        IEnumerable<string> selectedCovers, IDictionary<string, object> subject)
    {
        ProductCode = productCode;
        PolicyFrom = policyFrom;
        PolicyTo = policyTo;
        Subject = subject;
        Covers = selectedCovers.ToDictionary(code => code, code => new Cover(code));
    }

    public string ProductCode { get; }
    public DateTimeOffset PolicyFrom { get; }
    public DateTimeOffset PolicyTo { get; }
    public IDictionary<string, object> Subject { get; }
    public Dictionary<string, Cover> Covers { get; }
    public decimal TotalPremium { get; private set; }

    public void UpdateTotal() => TotalPremium = Covers.Values.Sum(c => c.Price);
}

public class Cover
{
    public Cover(string code)
    {
        Code = code;
    }

    public string Code { get; }
    public decimal Price { get; set; }
}
