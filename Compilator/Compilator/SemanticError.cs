using System;

namespace Compilator
{
    public class SemanticError : CompilerError
    {
        public SemanticError(string message, int line, int column)
            : base(message, line, column)
        {
        }

        public override string ToString()
        {
            return $"Semantic Error at line {Line}, column {Column}: {Message}";
        }
    }
}
