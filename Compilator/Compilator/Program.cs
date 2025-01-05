using System;
using System.Collections.Generic;
using System.IO;
using Antlr4.Runtime;
using MiniLang;

public class Program
{
    public static void Main()
    {
        Console.WriteLine("Se citeste fisierul sursa...");
        string filePath = "../../../ProgramExemple.txt";

        if (!File.Exists(filePath))
        {
            Console.WriteLine($"Fisierul {filePath} nu exista!");
            return;
        }

        string sourceCode = File.ReadAllText(filePath);

        Console.WriteLine("Se analizeaza unitatile lexicale...");
        var lexer = new MiniLangLexer(new AntlrInputStream(sourceCode));
        var tokens = new CommonTokenStream(lexer);

        Console.WriteLine("Se creeaza arborele de sintaxa...");
        var parser = new MiniLangParser(tokens);
        var tree = parser.program();

        var programData = new ProgramData();

        foreach (var token in tokens.GetTokens())
        {
            programData.AddLexicalUnit(
                token.Type.ToString(),
                token.Text,
                token.Line
            );
        }

        Console.WriteLine("Se efectueaza analiza semantica...");
        var visitor = new LanguageVisitor(programData);
        visitor.Visit(tree);

        PrintResults(programData);

        SaveProgramData(programData);

        Console.WriteLine("Analiza lexicala, sintactica si semantica s-a încheiat cu succes!");
    }
    private static void PrintResults(ProgramData programData)
    {
        Console.WriteLine("\nUnitati lexicale:\n");
        foreach (var unit in programData.LexicalUnits)
        {
            Console.WriteLine(unit);
        }

        Console.WriteLine("\nVariabile globale:\n");
        foreach (var variable in programData.GlobalVariables)
        {
            Console.WriteLine(variable);
        }

        Console.WriteLine("\nFunctii:\n");
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
        Console.WriteLine("\nDatele au fost salvate cu succes în fisiere.");
    }
}
