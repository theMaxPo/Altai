namespace Altai
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            while (true)
            {
                Console.Write("Altai > ");

                var text = Console.ReadLine();

                (List<Token> result, Error? err) tuple = Run("no_file.txt", text);

                if (tuple.err != null)
                {
                    Console.WriteLine(tuple.err.ToString());
                }
            }
        }

        private static (List<Token>, Error?) Run(string filrName, string text)
        {
            var lexer = new Lexer(filrName, text);

            (List<Token> tokens, Error? err) res = lexer.MakeTokens();

            if (res.err != null) return res;
            var parser = new Parser(res.tokens);
            var ast = parser.Parse();

            if (ast.Error != null)
            {
                Console.WriteLine(ast.Error.ToString());
                return res;
            }
            Console.WriteLine(ast.Node.ToString());

            return res;
        }
    }
}