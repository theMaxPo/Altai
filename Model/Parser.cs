public class Parser
{
    private List<Token> Tokens { get; set; }
    private int TokenIndex { get; set; }
    private Token CurrentToken { get; set; }

    public Parser(List<Token> tokens)
    {
        Tokens = tokens;
        TokenIndex = -1;
        MoveNext();
    }

    private Token MoveNext()
    {
        TokenIndex++;
        if (TokenIndex < Tokens.Count)
        {
            CurrentToken = Tokens[TokenIndex];
        }

        return CurrentToken;
        // return new Node(CurrentToken);
    }

    public ParseResult Parse()
    {
        var result = Expr();

        if (result.Error == null && CurrentToken.Type != TokenType.EOF)
        {
            return result.Failure(new InvalidSyntaxError(CurrentToken.PosStart, CurrentToken.PosEnd, "Ожидалось '+', '-', '*' или '/'"));
        }
        return result;
    }

    // factor : Int | Float | LParenthesis expr RParenthesis
    private ParseResult Factor()
    {
        var pRes = new ParseResult();

        var token = CurrentToken;

        if (token.Type == TokenType.Plus || token.Type == TokenType.Minus)
        {
            MoveNext(); //нужно зарегать => pRes.Register(MoveNext())
            Node factor = pRes.Register(Factor());
            if (pRes.Error != null) return pRes;

            factor = new Node(null, token, factor);
            return pRes.Success(factor);
        }
        else if (token.Type == TokenType.Int || token.Type == TokenType.Float)
        {
            MoveNext(); //нужно зарегать => pRes.Register(MoveNext())
            return pRes.Success(new Node(token));
        }
        else if (token.Type == TokenType.LParenthesis)
        {
            //нужно зарегать => pRes.Register(MoveNext())
            MoveNext(); // пропускаем первую скобку '('
            var expr = pRes.Register(Expr());
            if (pRes.Error != null) return pRes;

            if (CurrentToken.Type == TokenType.RParenthesis)
            {
                //нужно зарегать => pRes.Register(MoveNext())
                MoveNext(); // пропускаем вторую скобку ')' (сейчас будет работать любой знак, кроме: INT, FLOAT, LParenthesis, Div...)
                return pRes.Success(expr);
            }
            return pRes.Failure(new InvalidSyntaxError(CurrentToken.PosStart, CurrentToken.PosEnd, "Ожидалось ')'"));
        }
        return pRes.Failure(new InvalidSyntaxError(token.PosStart, token.PosEnd, "Ожидали Int или Float"));
    }

    // term : factor ((Multiply | Divide) factor)*
    private ParseResult Term() => BinaryOp(Factor, TokenType.Multiply, TokenType.Divide);

    // expr   : term ((Plus | Minus) term)*
    private ParseResult Expr() => BinaryOp(Term, TokenType.Plus, TokenType.Minus);

    private ParseResult BinaryOp(Func<ParseResult> func, params TokenType[] ops)
    {
        var pRes = new ParseResult();

        Node? left = pRes.Register(func());
        if (pRes.Error != null) return pRes;

        while (ops.Any(t => t == CurrentToken.Type))
        {
            Token opToken = CurrentToken;
            MoveNext(); //нужно зарегать => pRes.Register(MoveNext())

            Node right = pRes.Register(func());
            if (pRes.Error != null) return pRes;

            left = new Node(left, opToken, right);
        }
        return pRes.Success(left);
    }
}