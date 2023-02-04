public class Interpreter
{
    public Number Visit(Node node)
    {
        if (node.Right != null && node.Left != null)
        {
            return VisitBinaryOpNode(node);
        }
        else if (node.Right != null && node.Left == null)
        {
            return VisitUnaruOpNode(node);
        }
        return VisitNumberNode(node);
    }

    private Number VisitNumberNode(Node node)
    {
        // Console.WriteLine($"Число!");
        return new Number(Int32.Parse(node.Token.Value));
    }

    private Number VisitBinaryOpNode(Node node)
    {
        // Console.WriteLine($"Двоичный оператор!");
        var left = Visit(node.Left);
        var right = Visit(node.Right);
        Number result = new Number(0);

        if (node.Token.Type == TokenType.Plus)
        {
            result = left.AddedTo(right);
        }
        else if (node.Token.Type == TokenType.Minus)
        {
            result = left.SubbedBy(right);
        }
        else if (node.Token.Type == TokenType.Multiply)
        {
            result = left.MultedBy(right);
        }
        else if (node.Token.Type == TokenType.Divide)
        {
            result = left.DivedBy(right);
        }

        return result;
    }

    private Number VisitUnaruOpNode(Node node)
    {
        // Console.WriteLine($"Унарный оператор!");
        var number = Visit(node.Right);
       
        if (node.Token.Type == TokenType.Minus)
        {
            number = number.MultedBy(new Number(-1));
        }
        return number;
    }
}