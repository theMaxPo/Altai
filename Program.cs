using System;

internal class Program
{
    private static void Main(string[] args)
    {
        while (true)
        {
            Console.Write("Altai > ");

            var text = Console.ReadLine();

            Console.WriteLine(text);
        }
    }
}