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

                if (tuple.err != null) Console.WriteLine(tuple.err.ToString());
                // else Console.WriteLine(tuple.result);
            }
        }

        private static (List<Token>, Error?) Run(string filrName, string text)
        {
            // Создаем токены
            var lexer = new Lexer(filrName, text);
            (List<Token> tokens, Error? error) res = lexer.MakeTokens();
            if (res.error != null) return (null, res.error);

            // Создаем AST
            var parser = new Parser(res.tokens);
            var ast = parser.Parse();
            if (ast.Error != null) return (null, ast.Error);

            // Выполнение программы
            var interpreter = new Interpreter();
            var result = interpreter.Visit(ast.Node);

            Console.WriteLine(result);
            // Console.WriteLine(ast.Node.ToString());

            return (null, null);
        }
    }
}