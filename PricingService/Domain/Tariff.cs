namespace PricingService.Domain;

public class Tariff
{
    public Tariff(string code)
    {
        Code = code;
    }

    public string Code { get; }
    public BasePremiumCalculationRuleList BasePremiumRules { get; } = new();
    public DiscountMarkupRuleList DiscountMarkupRules { get; } = new();

    public Calculation CalculatePrice(Calculation calculation)
    {
        foreach (var cover in calculation.Covers.Values)
        {
            cover.Price = BasePremiumRules.CalculateBasePriceFor(cover.Code, calculation.Subject);
        }

        DiscountMarkupRules.ApplyTo(calculation);
        calculation.UpdateTotal();
        return calculation;
    }
}
