namespace ProductService.Api.Queries.Dtos;

public class ProductDto
{
    public string Code { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Image { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public IList<CoverDto> Covers { get; set; } = new List<CoverDto>();
    public IList<QuestionDto> Questions { get; set; } = new List<QuestionDto>();
    public int MaxNumberOfInsured { get; set; }
    public string Icon { get; set; } = string.Empty;
}
