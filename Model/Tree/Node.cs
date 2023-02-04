public class Node
{
    public Node? Left { get; private set; }
    public Node? Right { get; private set; }
    public Token Token { get; }

    public Node(Token token)
    {
        Token = token;
    }
    public Node(Node left, Token token, Node right) : this(token)
    {
        Left = left;
        Right = right;
    }

    public override string ToString()
    {
        var result = "";
        if (Left != null) result += "(" + Left.ToString() + ", ";
        result += Token.ToString();
        if (Right != null) result += $", {Right.ToString()})";
        return result;
    }
}
