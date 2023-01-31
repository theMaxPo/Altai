public class Token
{
    private TokenType Type { get; set; }

    private string Value { get; set; }

    public Token(TokenType type, string value)
    {
        Type = type;
        Value = value;
    }

    public override string ToString() => $"{Type}: {Value}";
}