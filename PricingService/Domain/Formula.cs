using DynamicExpresso;

namespace PricingService.Domain;

internal static class Formula
{
    public static bool IsTrue(string? formula, IDictionary<string, object> subject) =>
        string.IsNullOrWhiteSpace(formula) || Evaluate<bool>(formula, subject);

    public static T Evaluate<T>(string formula, IDictionary<string, object> subject)
    {
        var interpreter = new Interpreter();
        foreach (var (name, value) in subject)
        {
            interpreter.SetVariable(name, value);
        }

        return interpreter.Eval<T>(formula);
    }
}
