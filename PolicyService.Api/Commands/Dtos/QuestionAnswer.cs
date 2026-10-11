namespace PolicyService.Api.Commands.Dtos;

public enum QuestionType
{
    Text,
    Numeric,
    Choice
}

public abstract class QuestionAnswer
{
    public string QuestionCode { get; set; } = string.Empty;
    public abstract QuestionType QuestionType { get; }
    public abstract object? GetAnswer();
}

public abstract class QuestionAnswer<T> : QuestionAnswer
{
    public T? Answer { get; set; }
    public override object? GetAnswer() => Answer;
}

public class TextQuestionAnswer : QuestionAnswer<string>
{
    public override QuestionType QuestionType => QuestionType.Text;
}

public class NumericQuestionAnswer : QuestionAnswer<decimal>
{
    public override QuestionType QuestionType => QuestionType.Numeric;
}

public class ChoiceQuestionAnswer : QuestionAnswer<string>
{
    public override QuestionType QuestionType => QuestionType.Choice;
}
