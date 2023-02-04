public class Error
{
    protected string ErrorName { get; set; }
    protected string Details { get; set; }
    protected Position PosStart { get; set; }
    protected Position PosEnd { get; set; }

    public Error(Position posStart, Position posEnd, string errorName, string details)
    {
        if (errorName == null) throw new ArgumentException("Название ошибки не может быть пустым.", nameof(errorName));
        if (details == null) throw new ArgumentException("Информация об ошибки не может быть пустой.", nameof(details));

        PosStart = posStart;
        PosEnd = posEnd;
        ErrorName = errorName;
        Details = details;
    }

    public override string ToString()
    {
        var result = $"{ErrorName}: {Details}";
        result += $"\nФайл {PosStart.FileName}, строка {PosStart.Line + 1}";
        result += $"\n\n{stringWithArrows(PosStart.FileText, PosStart, PosEnd)}";
        return result;
    }

    private string stringWithArrows(string text, Position posStart, Position posEnd)
    {
        var result = "";
        var textLength = text.Length;

        var indexStart = Math.Max(text.LastIndexOf('\n', posStart.Index), 0);       // Находим начало строки в которой ошибка
                                                                                    // Если '\n' не найдем => это первая строка(начинается с 0)

        var indexEnd = Math.Max(text.IndexOf('\n', indexStart + 1), textLength);    // Находим конец текущей строки
                                                                                    // Если '\n' не найдем => последняя линия(равно длине текста)

        var lineCount = posEnd.Line - posStart.Line + 1;
        for (var i = 0; i < lineCount; i++)
        {
            var line = text[indexStart..indexEnd];
            var colStart = (i == 0) ? posStart.Column : 0;
            var colEnd = (i == lineCount - 1) ? posEnd.Column : line.Length - 1;

            result += line + '\n';
            result += new string(' ', colStart) + new string('^', (colEnd - colStart));

            // Пересчитываем начало и конец для следующей строки(Line)
            indexStart = indexEnd;
            // indexEnd = Math.Max(text.IndexOf('\n', indexStart + 1), textLength);
            // При многомерных строках может потребоваться верхний, закомменчиный вариант!!!!
            indexEnd = Math.Max(text.IndexOf('\n', indexStart), textLength);
        }
        return result.Replace('\t', ' ');
    }
}

public class IllegalCharError : Error
{
    public IllegalCharError(Position posStart, Position posEnd, string details) : base(posStart, posEnd, "Недопустимый символ", details) { }
}

public class InvalidSyntaxError : Error
{
    public InvalidSyntaxError(Position posStart, Position posEnd, string details = "") : base(posStart, posEnd, "Неверный синтаксис", details) { }
}

public class RunTimeError : Error
{
    public RunTimeError(Position posStart, Position posEnd, string details = "") : base(posStart, posEnd, "Ошибка во время выполнения", details) { }
}