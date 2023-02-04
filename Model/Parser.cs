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
    }

    public Node Parse() => Expr();

    // factor : Int | Float | LParenthesis expr RParenthesis
    private Node? Factor()
    {
        var token = CurrentToken;

        if (token.Type == TokenType.Int || token.Type == TokenType.Float)
        {
            MoveNext();
            return new Node(token);
        }
        else if (token.Type == TokenType.LParenthesis)
        {
            MoveNext(); // пропускаем первую скобку '('
            var res = Expr();
            MoveNext(); // пропускаем вторую скобку ')' (сейчас будет работать любой знак, кроме: INT, FLOAT, LParenthesis, Div...)
            return res;
        }
        return null;
    }

    // term : factor ((Multiply | Divide) factor)*
    private Node Term() => BinaryOp(Factor, TokenType.Multiply, TokenType.Divide);

    // expr   : term ((Plus | Minus) term)*
    private Node Expr() => BinaryOp(Term, TokenType.Plus, TokenType.Minus);

    private Node BinaryOp(Func<Node> func, params TokenType[] ops)
    {
        Node? left = func();

        while (ops.Any(t => t == CurrentToken.Type))
        {
            Token opToken = CurrentToken;
            MoveNext();

            Node right = func();
            left = new Node(left, opToken, right);
        }
        return left;
    }
}