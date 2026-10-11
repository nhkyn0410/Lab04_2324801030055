namespace ProductService.Api.Queries.Dtos;

public class CoverDto
{
    public string Code { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public bool Optional { get; set; }
    public decimal? SumInsured { get; set; }
}
