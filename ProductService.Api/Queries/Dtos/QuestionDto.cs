namespace ProductService.Api.Queries.Dtos;

public enum QuestionType
{
    Choice,
    Date,
    Numeric
}

public abstract class QuestionDto
{
    public string QuestionCode { get; set; } = string.Empty;
    public int Index { get; set; }
    public string Text { get; set; } = string.Empty;
    public abstract QuestionType QuestionType { get; }
}

public class ChoiceQuestionDto : QuestionDto
{
    public IList<ChoiceDto> Choices { get; set; } = new List<ChoiceDto>();
    public override QuestionType QuestionType => QuestionType.Choice;
}

public class DateQuestionDto : QuestionDto
{
    public override QuestionType QuestionType => QuestionType.Date;
}

public class NumericQuestionDto : QuestionDto
{
    public override QuestionType QuestionType => QuestionType.Numeric;
}
