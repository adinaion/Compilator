using System;

namespace Compilator
{
    public class SyntaxError : CompilerError
    {
        public SyntaxError(string message, int line, int column)
            : base(message, line, column)
        {
        }

        public override string ToString()
        {
            return $"Syntax Error at line {Line}, column {Column}: {Message}";
        }
    }
}
