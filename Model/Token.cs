public class Token
{
    public TokenType Type { get; private set; }
    private string Value { get; set; }
    public Position PosStart { get; private set; }
    public Position PosEnd { get; private set; }

    private Token(TokenType type, string value)
    {
        Type = type;
        Value = value;
    }

    public Token(TokenType type, string value, Position posStart) : this(type, value)
    {
        PosStart = posStart.Copy();
        PosEnd = posStart.Copy();
        PosEnd.MoveNext();
    }

    public Token(TokenType type, string value, Position posStart, Position posEnd) : this(type, value, posStart)
    {
        PosEnd = posEnd;
    }

    public override string ToString() => $"{Type}: {Value}";
}