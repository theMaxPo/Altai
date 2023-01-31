using System;

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

    public override string ToString(){
        var result = $"{ErrorName}: {Details}";
        result += $"\nФайл {PosStart.FileName}, строка {PosStart.Line + 1}";
        return result;
    }
}

public class IllegalCharError : Error
{
    public IllegalCharError(Position posStart, Position posEnd, string details) : base(posStart, posEnd, "Недопустимый символ", details) { }
} 