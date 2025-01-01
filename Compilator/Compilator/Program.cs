using System;
using System.IO;
using Antlr4.Runtime;
using MiniLang;

public class Program
{
    public static void Main()
    {
        // Citește fișierul sursă
        Console.WriteLine("Se citește fișierul sursă...");
        string filePath = "../../../ProgramExemple.txt";

        if (!File.Exists(filePath))
        {
            Console.WriteLine($"Fișierul {filePath} nu există!");
            return;
        }

        string sourceCode = File.ReadAllText(filePath);

        // Creează lexer și parser
        Console.WriteLine("Se analizează unitățile lexicale...");
        var lexer = new MiniLangLexer(new AntlrInputStream(sourceCode));
        var tokens = new CommonTokenStream(lexer);

        Console.WriteLine("Se creează arborele de sintaxă...");
        var parser = new MiniLangParser(tokens);
        var tree = parser.program();

        // Inițializează ProgramData
        var programData = new ProgramData();

        // Colectează unitățile lexicale
        foreach (var token in tokens.GetTokens())
        {
            programData.AddLexicalUnit(
                token.Type.ToString(),
                token.Text,
                token.Line
            );
        }

        // Analiza semantică
        Console.WriteLine("Se efectuează analiza semantică...");
        var visitor = new LanguageVisitor(programData);
        visitor.Visit(tree);

        // Afișează informațiile colectate
        PrintResults(programData);

        // Salvează datele în fișiere
        SaveProgramData(programData);

        Console.WriteLine("Analiza lexicală, sintactică și semantică s-a încheiat cu succes!");
    }

    private static void PrintResults(ProgramData programData)
    {
        Console.WriteLine("Unități lexicale:");
        foreach (var unit in programData.LexicalUnits)
        {
            Console.WriteLine(unit);
        }

        Console.WriteLine("\nVariabile globale:");
        foreach (var variable in programData.GlobalVariables)
        {
            Console.WriteLine(variable);
        }

        Console.WriteLine("\nFuncții:");
        foreach (var function in programData.Functions)
        {
            Console.WriteLine(function);
        }
    }

    private static void SaveProgramData(ProgramData programData)
    {
        programData.SaveLexicalUnits("LexicalUnits.txt");
        programData.SaveGlobalVariables("GlobalVariables.txt");
        programData.SaveFunctions("Functions.txt");
        Console.WriteLine("Datele au fost salvate cu succes în fișiere.");
    }
}
