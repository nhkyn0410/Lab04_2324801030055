namespace PricingService.Domain;

public class BasePremiumCalculationRule
{
    public BasePremiumCalculationRule(string coverCode, string? applyIfFormula, string basePriceFormula)
    {
        CoverCode = coverCode;
        ApplyIfFormula = applyIfFormula;
        BasePriceFormula = basePriceFormula;
    }

    public string CoverCode { get; }
    public string? ApplyIfFormula { get; }
    public string BasePriceFormula { get; }

    public bool Applies(string coverCode, IDictionary<string, object> subject) =>
        CoverCode == coverCode && Formula.IsTrue(ApplyIfFormula, subject);

    public decimal CalculateBasePrice(IDictionary<string, object> subject) =>
        Formula.Evaluate<decimal>(BasePriceFormula, subject);
}

public class BasePremiumCalculationRuleList
{
    private readonly List<BasePremiumCalculationRule> rules = new();

    public void AddBasePriceRule(string coverCode, string? applyIfFormula, string basePriceFormula) =>
        rules.Add(new BasePremiumCalculationRule(coverCode, applyIfFormula, basePriceFormula));

    public decimal CalculateBasePriceFor(string coverCode, IDictionary<string, object> subject)
    {
        var rule = rules.FirstOrDefault(r => r.Applies(coverCode, subject))
            ?? throw new InvalidOperationException($"No base price rule for cover {coverCode}");
        return rule.CalculateBasePrice(subject);
    }
}
