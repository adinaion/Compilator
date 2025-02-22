﻿using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Ports;
using System.Linq;

namespace MiniLang
{
    public class ProgramData
    {
        public class LexicalUnit
        {
            public string Token { get; set; }
            public string Lexeme { get; set; }
            public int Line { get; set; }

            public override string ToString() => $"<{Token}, {Lexeme}, Line {Line}>";
        }

        public class Variable
        {
            public enum Type
            {
                Int,
                Float,
                Double,
                String,
                Void,
            }

            public Type VariableType { get; set; }
            public string Name { get; set; }
            public dynamic? Value { get; set; }

            public override string ToString() => $"{VariableType} {Name} = {Value}";
        }

        public class Function
        {
            public string Name { get; set; }
            public Variable.Type ReturnType { get; set; }
            public List<Variable> Parameters { get; set; } = new List<Variable>();
            public List<Variable> LocalVariables { get; set; } = new List<Variable>();
            public List<string> ControlStructures { get; set; } = new List<string>();
            public MiniLangParser.BlockContext Body { get; set; } 
            public bool IsRecursive { get; set; } = false;

            public override string ToString()
            {
                string parameters = Parameters.Count > 0
                    ? string.Join(", ", Parameters.Select(p => $"{p.VariableType} {p.Name} {(p.Value != null ? $"= {p.Value}" : "(uninitialized)")}"))
                    : "None";

                string localVariables = LocalVariables.Count > 0
                    ? string.Join(", ", LocalVariables.Select(v => $"{v.VariableType} {v.Name} {(v.Value != null ? $"= {v.Value}" : "(uninitialized)")}"))
                    : "None";

                string controlStructures = ControlStructures.Count > 0
                    ? string.Join("\n  ", ControlStructures)
                    : "None";

                return $"Function {Name} ({(IsRecursive ? "Recursive" : "Iterative")})\n" +
                       $"Return: {ReturnType}\n" +
                       $"Parameters: {parameters}\n" +
                       $"Local Variables: {localVariables}\n" +
                       $"Control Structures:\n  {controlStructures}\n";
            }
        }

        public List<LexicalUnit> LexicalUnits { get; set; } = new List<LexicalUnit>();
        public List<Variable> GlobalVariables { get; set; } = new List<Variable>();
        public List<Function> Functions { get; set; } = new List<Function>();

        public Stack<Dictionary<string, Variable>> CallStack { get; set; } = new Stack<Dictionary<string, Variable>>();

        public Function CurrentFunction { get; set; }

        public void AddLexicalUnit(string token, string lexeme, int line)
        {
            LexicalUnits.Add(new LexicalUnit { Token = token, Lexeme = lexeme, Line = line });
        }

        public void SaveLexicalUnits(string filePath)
        {
            using var writer = new StreamWriter(filePath);
            foreach (var unit in LexicalUnits)
            {
                writer.WriteLine(unit.ToString());
            }
        }

        public void SaveGlobalVariables(string filePath)
        {
            using var writer = new StreamWriter(filePath);
            writer.WriteLine("Global Variables:");
            foreach (var variable in GlobalVariables)
            {
                writer.WriteLine(variable.ToString());
            }
        }

        public void SaveFunctions(string filePath)
        {
            using var writer = new StreamWriter(filePath);
            foreach (var function in Functions)
            {
                writer.WriteLine($"Function {function.Name} ({(function.IsRecursive ? "Recursive" : "Iterative")})");
                writer.WriteLine($"Return: {function.ReturnType}");

                var parameters = function.Parameters.Count > 0
                    ? string.Join(", ", function.Parameters.Select(p => $"{p.VariableType} {p.Name} {(p.Value != null ? $"= {p.Value}" : "(uninitialized)")}"))
                    : "None";
                writer.WriteLine($"Parameters: {parameters}");

                var localVariables = function.LocalVariables.Count > 0
                    ? string.Join(", ", function.LocalVariables.Select(v => $"{v.VariableType} {v.Name} {(v.Value != null ? $"= {v.Value}" : "(uninitialized)")}"))
                    : "None";
                writer.WriteLine($"Local Variables: {localVariables}");

                var controlStructures = function.ControlStructures.Count > 0
                    ? string.Join(", ", function.ControlStructures)
                    : "None";
                writer.WriteLine($"Control Structures: {controlStructures}");

                writer.WriteLine();
            }
        }
    }
}
