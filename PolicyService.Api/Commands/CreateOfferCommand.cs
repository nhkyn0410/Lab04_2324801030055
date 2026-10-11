using MediatR;
using PolicyService.Api.Commands.Dtos;

namespace PolicyService.Api.Commands;

public class CreateOfferCommand : IRequest<CreateOfferResult>
{
    public string ProductCode { get; set; } = string.Empty;
    public DateTime PolicyFrom { get; set; }
    public DateTime PolicyTo { get; set; }
    public List<string> SelectedCovers { get; set; } = new();
    public List<QuestionAnswer> Answers { get; set; } = new();
}
