public class Lexer
{
    public readonly string text;
    public readonly string fileName;

    private Position position;
    private char? сurrentSymbol = null;

    public Lexer(string fileName, string text)
    {
        this.fileName = fileName;
        this.text = text;
        position = new Position(-1, 0, -1, fileName, text);
        MoveNext();
    }

    private void MoveNext()
    {
        position.MoveNext(сurrentSymbol);

        if (position.Index < text.Length)
        {
            сurrentSymbol = text[position.Index];
        }
        else
        {
            сurrentSymbol = null;
        }
    }

    public (List<Token>, Error?) MakeTokens()
    {
        var token = new List<Token>();

        while (сurrentSymbol != null)
        {
            if (сurrentSymbol == '\t' || сurrentSymbol == ' ')
            {
                MoveNext();
            }
            else if (Char.IsDigit(сurrentSymbol.Value))
            {
                token.Add(MakeNumber());
            }
            else if (сurrentSymbol == '+')
            {
                token.Add(new Token(TokenType.Plus, сurrentSymbol.Value.ToString(), position));
                MoveNext();
            }
            else if (сurrentSymbol == '-')
            {
                token.Add(new Token(TokenType.Minus, сurrentSymbol.Value.ToString(), position));
                MoveNext();
            }
            else if (сurrentSymbol == '*')
            {
                token.Add(new Token(TokenType.Multiply, сurrentSymbol.Value.ToString(), position));
                MoveNext();
            }
            else if (сurrentSymbol == '/')
            {
                token.Add(new Token(TokenType.Divide, сurrentSymbol.Value.ToString(), position));
                MoveNext();
            }
            else if (сurrentSymbol == '(')
            {
                token.Add(new Token(TokenType.LParenthesis, сurrentSymbol.Value.ToString(), position));
                MoveNext();
            }
            else if (сurrentSymbol == ')')
            {
                token.Add(new Token(TokenType.RParenthesis, сurrentSymbol.Value.ToString(), position));
                MoveNext();
            }
            else
            {
                var posStart = position.Copy();
                var symbol = сurrentSymbol;
                MoveNext();
                return (new List<Token>(), new IllegalCharError(posStart, position, $"'{symbol}'"));
            }
        }
        token.Add(new Token(TokenType.EOF, "EOF", position));
        return (token, null);
    }

    private Token MakeNumber()
    {
        var numStr = string.Empty;
        bool isDot = false;
        var posStart = position.Copy();

        while (сurrentSymbol != null && (Char.IsDigit(сurrentSymbol.Value) || сurrentSymbol == '.'))
        {
            if (сurrentSymbol == '.')
            {
                if (isDot) break;
                isDot = true;
            }
            numStr += сurrentSymbol;
            MoveNext();
        }
        if (isDot == false)
        {
            return new Token(TokenType.Int, numStr, posStart, position);
        }
        return new Token(TokenType.Float, numStr, posStart, position);
    }
}