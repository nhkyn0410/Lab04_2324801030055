namespace PricingService.Domain;

public class PercentMarkupRule
{
    public PercentMarkupRule(string? applyIfFormula, decimal markup)
    {
        ApplyIfFormula = applyIfFormula;
        Markup = markup;
    }

    public string? ApplyIfFormula { get; }
    public decimal Markup { get; }

    public void ApplyTo(Calculation calculation)
    {
        if (!Formula.IsTrue(ApplyIfFormula, calculation.Subject))
        {
            return;
        }

        foreach (var cover in calculation.Covers.Values)
        {
            cover.Price = Math.Round(cover.Price * Markup, 2);
        }
    }
}

public class DiscountMarkupRuleList
{
    private readonly List<PercentMarkupRule> rules = new();

    public void AddPercentMarkup(string? applyIfFormula, decimal markup) =>
        rules.Add(new PercentMarkupRule(applyIfFormula, markup));

    public void ApplyTo(Calculation calculation)
    {
        foreach (var rule in rules)
        {
            rule.ApplyTo(calculation);
        }
    }
}
