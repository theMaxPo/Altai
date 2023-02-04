public class Token
{
    public TokenType Type { get; private set; }

    private string Value { get; set; }
    private Position PosStart { get; set; }
    private Position PosEnd { get; set; }

    public Token(TokenType type, string value)
    {
        Type = type;
        Value = value;
    }
    public Token(TokenType type, string value, Position posStart, Position posEnd) : this(type, value, posStart)
    {
        PosEnd = posEnd;
    }
    public Token(TokenType type, string value, Position posStart) : this(type, value)
    {
        PosStart = posStart.Copy();
        PosEnd = posStart.Copy();
        PosEnd.MoveNext();
    }

    public override string ToString() => $"{Type}: {Value}";
}