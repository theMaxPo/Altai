public class Interpreter
{
    public void Visit(Node node)
    {
        if (node.Right != null && node.Left != null)
        {
            VisitBinOpNode(node);
        }
        else if (node.Right != null && node.Left == null )
        {
            VisitUnaruOpNode(node);
        }
        else
        {
            VisitNumberNode(node);
        }
    }

    private void VisitNumberNode(Node node)
    {
        Console.WriteLine($"Число!");
    }

    private void VisitBinOpNode(Node node)
    {
        Console.WriteLine($"Двоичный оператор!");
        Visit(node.Left);
        Visit(node.Right);
    }

    private void VisitUnaruOpNode(Node node)
    {
        Console.WriteLine($"Унарный оператор!");
        Visit(node.Right);
    }
}