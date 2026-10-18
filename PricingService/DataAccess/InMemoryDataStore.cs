using PricingService.Domain;

namespace PricingService.DataAccess;

public class InMemoryDataStore : IDataStore
{
    public InMemoryDataStore(IEnumerable<Tariff> tariffs)
    {
        Tariffs = new InMemoryTariffRepository(tariffs);
    }

    public ITariffRepository Tariffs { get; }

    private class InMemoryTariffRepository : ITariffRepository
    {
        private readonly Dictionary<string, Tariff> tariffs;

        public InMemoryTariffRepository(IEnumerable<Tariff> tariffs)
        {
            this.tariffs = tariffs.ToDictionary(t => t.Code);
        }

        public Task<Tariff> this[string code] =>
            tariffs.TryGetValue(code, out var tariff)
                ? Task.FromResult(tariff)
                : throw new KeyNotFoundException($"Tariff {code} not found");
    }
}
