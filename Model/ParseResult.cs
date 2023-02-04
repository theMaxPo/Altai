public class ParseResult
{
    public Error? Error { get; private set; }
    public Node Node { get; private set; }

    public Node Register(ParseResult res)
    {
        if(res.Error != null) Error = res.Error;
        return res.Node;
    }
    
    public ParseResult Success(Node node)
    {
        Node = node;
        return this;
    }

    public ParseResult Failure(Error error)
    {
        Error = error;
        return this;
    }
}