using System;
using System.Collections.Generic;
using System.IO;

namespace MiniLang
{
    public class ProgramData
    {
        // Structură pentru unități lexicale
        public class LexicalUnit
        {
            public string Token { get; set; }
            public string Lexeme { get; set; }
            public int Line { get; set; }

            public override string ToString() => $"<{Token}, {Lexeme}, Line {Line}>";
        }

        // Structură pentru variabile
        public class Variable
        {
            public enum Type
            {
                Int,
                Float,
                String,
                Void
            }

            public Type VariableType { get; set; }
            public string Name { get; set; }
            public dynamic? Value { get; set; }

            public override string ToString() => $"{VariableType} {Name} = {Value}";
        }

        // Structură pentru funcții
        public class Function
        {
            public string Name { get; set; }
            public Variable.Type ReturnType { get; set; }
            public List<Variable> Parameters { get; set; } = new List<Variable>();
            public List<Variable> LocalVariables { get; set; } = new List<Variable>();
            public List<string> ControlStructures { get; set; } = new List<string>();
            public bool IsRecursive { get; set; } = false;

            public override string ToString() =>
                $"Function {Name} ({(IsRecursive ? "Recursive" : "Iterative")})\n" +
                $"Return: {ReturnType}\n" +
                $"Parameters: {string.Join(", ", Parameters)}\n" +
                $"Local Variables: {string.Join(", ", LocalVariables)}\n" +
                $"Control Structures: {string.Join(", ", ControlStructures)}";
        }

        // Date globale
        public List<LexicalUnit> LexicalUnits { get; set; } = new List<LexicalUnit>();
        public List<Variable> GlobalVariables { get; set; } = new List<Variable>();
        public List<Function> Functions { get; set; } = new List<Function>();

        // Funcția curentă
        public Function CurrentFunction { get; set; }

        // Adăugare unitate lexicală
        public void AddLexicalUnit(string token, string lexeme, int line)
        {
            LexicalUnits.Add(new LexicalUnit { Token = token, Lexeme = lexeme, Line = line });
        }

        // Salvare unități lexicale într-un fișier
        public void SaveLexicalUnits(string filePath)
        {
            using var writer = new StreamWriter(filePath);
            foreach (var unit in LexicalUnits)
            {
                writer.WriteLine(unit.ToString());
            }
        }

        // Salvare variabile globale într-un fișier
        public void SaveGlobalVariables(string filePath)
        {
            using var writer = new StreamWriter(filePath);
            writer.WriteLine("Global Variables:");
            foreach (var variable in GlobalVariables)
            {
                writer.WriteLine(variable.ToString());
            }
        }

        // Salvare funcții într-un fișier
        public void SaveFunctions(string filePath)
        {
            using var writer = new StreamWriter(filePath);
            foreach (var function in Functions)
            {
                writer.WriteLine(function.ToString());
                writer.WriteLine();
            }
        }
    }
}
