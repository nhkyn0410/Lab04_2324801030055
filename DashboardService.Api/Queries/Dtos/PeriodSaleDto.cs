namespace DashboardService.Api.Queries.Dtos;

public class PeriodSaleDto
{
    public DateTime PeriodDate { get; set; }
    public string Period { get; set; } = string.Empty;
    public SalesDto Sales { get; set; } = new();
}
