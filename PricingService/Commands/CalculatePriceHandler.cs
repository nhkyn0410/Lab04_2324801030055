using FluentValidation;
using MediatR;
using PricingService.Api.Commands;
using PricingService.Domain;

namespace PricingService.Commands;

public class CalculatePriceHandler : IRequestHandler<CalculatePriceCommand, CalculatePriceResult>
{
    private readonly IDataStore dataStore;

    public CalculatePriceHandler(IDataStore dataStore)
    {
        this.dataStore = dataStore;
    }

    public async Task<CalculatePriceResult> Handle(CalculatePriceCommand request, CancellationToken cancellationToken)
    {
        await new CalculatePriceCommandValidator().ValidateAndThrowAsync(request, cancellationToken);

        var tariff = await dataStore.Tariffs[request.ProductCode];
        var calculation = tariff.CalculatePrice(new Calculation(
            request.ProductCode,
            request.PolicyFrom,
            request.PolicyTo,
            request.SelectedCovers,
            request.Answers.ToDictionary(a => a.QuestionCode, a => a.GetAnswer())));

        return new CalculatePriceResult
        {
            TotalPrice = calculation.TotalPremium,
            CoverPrices = calculation.Covers.ToDictionary(c => c.Key, c => c.Value.Price)
        };
    }
}
