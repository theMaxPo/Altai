﻿using System;
using System.Collections.Generic;

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
                else
                {
                    foreach (var res in tuple.result)
                    {
                        Console.Write($"{res} ");
                    }
                    Console.WriteLine("");
                }
            }
        }

        private static (List<Token>, Error?) Run(string filrName, string text)
        {
            var lexer = new Lexer(filrName, text);

            (List<Token> tokens, Error? err) res = lexer.MakeTokens();

            if (res.err == null)
            {
                var parser = new Parser(res.tokens);
                var ast = parser.Parse();

                Console.WriteLine(ast.ToString());
            }
            return res;
        }
    }
}