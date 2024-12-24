using System;
using System.IO;
using Antlr4.Runtime;

class Program
{
    static void Main(string[] args)
    {
        // 1. Citește codul sursă din fișierul "ProgramExemple.txt"
        string filePath = "../../../ProgramExemple.txt";

        if (!File.Exists(filePath))
        {
            Console.WriteLine($"Fișierul {filePath} nu există!");
            return;
        }

        string sourceCode = File.ReadAllText(filePath);

        // 2. Creează un Lexer pentru a genera tokens
        var lexer = new MiniLangLexer(new AntlrInputStream(sourceCode));
        var tokens = new CommonTokenStream(lexer);

        // 3. Afișează lista de tokens
        Console.WriteLine("Tokens:");
        tokens.Fill();
        foreach (var token in tokens.GetTokens())
        {
            Console.WriteLine($"<{MiniLangLexer.DefaultVocabulary.GetSymbolicName(token.Type)}, {token.Text}>");
        }

        // 4. Creează un Parser pentru a genera arborele de sintaxă
        var parser = new MiniLangParser(tokens);
        var tree = parser.program();

        // 5. Afișează arborele de sintaxă (parse tree)
        Console.WriteLine("\nParse Tree:");
        Console.WriteLine(tree.ToStringTree(parser));
    }
}
